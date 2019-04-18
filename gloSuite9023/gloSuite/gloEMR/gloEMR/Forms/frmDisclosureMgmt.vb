Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices

Public Class frmDisclosureMgmt
    Inherits System.Windows.Forms.Form
    Implements IHotKey
    Implements ISignature
    Implements gloVoice
    Implements IPatientContext


    'Instantiate voice class
    Private ogloVoice As ClsVoice
    'implement interface for Voice --supriya 03/06/2009

    'Addendum user control defined here 
    Private blnIsAddendum As Boolean
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum

    Public MyCaller As frmVWDisclosureMgmt
    Private ImagePath As String
    Private m_DisclosureID As Long
    Private m_TemplateID As Long
    Private m_PatientID As Long
    Public m_IsFinished As Boolean = False
    Public m_visitID As Long
    Dim objCriteria As DocCriteria
    Dim objword As clsWordDocument
    Private blnCmbSelTemplate As Boolean = False
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCombo As System.Windows.Forms.Panel
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents lblLetterDt As System.Windows.Forms.Label
    Friend WithEvents dtDisclosureDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSelectTem As System.Windows.Forms.Label
    Private WithEvents tlsDisclosureMgmt As WordToolStrip.gloWordToolStrip
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Dim myidx As Int32
    Dim m_IsRecordLock As Boolean
    Public IsModify As Boolean = False
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents pnlToolStripContainer As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlToolbar As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Dim FullSaveStatus As Int16
    Friend WithEvents pnlGloUC_PastWordNotes As System.Windows.Forms.Panel
    Friend WithEvents GloUC_PastWordNotes1 As gloUserControlLibrary.gloUC_PastWordNotes
    Friend WithEvents pnl_chkShowPreview As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlGloUC_TemplateTreeControl As System.Windows.Forms.Panel
    Friend WithEvents GloUC_TemplateTreeControl_DisclosureManagement As gloUserControlLibrary.gloUC_TemplateTreeControl
    Friend WithEvents pnl_cmdPastExam As System.Windows.Forms.Panel
    Friend WithEvents cmdPastExam As System.Windows.Forms.Button
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents pnlgloUC_Addendum As System.Windows.Forms.Panel 'To check if Disclosure is saved or not when opened first time and cancelled
    Private blnSignClick As Boolean = False
    Friend WithEvents wdDisclosureMgmt As AxDSOFramer.AxFramerControl
    Private bnlIsFaxOpened As Boolean
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'line commented
        'm_PatientID = gnPatientID
        m_PatientID = PatientID
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    'constructor commented as not use and remove local referances gnpatientID
    'Public Sub New(ByVal strPatientWorkStatus As String, Optional ByVal ArrList As ArrayList = Nothing, Optional ByVal strNotes As String = "")
    '    MyBase.New()


    '    _strPatientWorkStatus = strPatientWorkStatus
    '    _Arrlist = ArrList
    '    _strNotes = strNotes
    '    m_PatientID = gnPatientID
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub


    'Public Sub New(ByVal DisclosureID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean)
    '    MyBase.New()

    '    m_DisclosureID = DisclosureID
    '    m_TemplateID = TemplateID
    '    m_IsFinished = IsFinished
    '    m_PatientID = gnPatientID
    '    m_IsRecordLock = IsRecordLock
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub
    Public Sub New(ByVal DisclosureID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean, ByVal PatientID As Long)
        MyBase.New()

        m_DisclosureID = DisclosureID
        m_TemplateID = TemplateID
        m_IsFinished = IsFinished
        m_PatientID = PatientID
        m_IsRecordLock = IsRecordLock
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtDisclosureDate}
            Dim cntControls() As System.Windows.Forms.Control = {dtDisclosureDate}
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

            If Not IsNothing(ogloVoice) Then
                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch ex As Exception

            End Try
            If (IsNothing(GloUC_TemplateTreeControl_DisclosureManagement) = False) Then
                If (IsNothing(GloUC_TemplateTreeControl_DisclosureManagement.DocCriteria)) Then
                    Try
                        DirectCast(GloUC_TemplateTreeControl_DisclosureManagement.DocCriteria, DocCriteria).Dispose()
                    Catch

                    End Try
                    GloUC_TemplateTreeControl_DisclosureManagement.DocCriteria = Nothing
                End If
            End If
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                    GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                End If
            Catch

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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents WdPatientConsent As AxDSOFramer.AxFramerControl
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    ' Friend WithEvents wdTemp As AxDSOFramer.AxFramerControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDisclosureMgmt))
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlgloUC_Addendum = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlToolbar = New System.Windows.Forms.Panel()
        Me.pnlCombo = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.lblLetterDt = New System.Windows.Forms.Label()
        Me.dtDisclosureDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.lblSelectTem = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnlToolStripContainer = New System.Windows.Forms.Panel()
        Me.pnlGloUC_PastWordNotes = New System.Windows.Forms.Panel()
        Me.GloUC_PastWordNotes1 = New gloUserControlLibrary.gloUC_PastWordNotes()
        Me.pnl_chkShowPreview = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlGloUC_TemplateTreeControl = New System.Windows.Forms.Panel()
        Me.GloUC_TemplateTreeControl_DisclosureManagement = New gloUserControlLibrary.gloUC_TemplateTreeControl()
        Me.pnl_cmdPastExam = New System.Windows.Forms.Panel()
        Me.cmdPastExam = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.wdDisclosureMgmt = New AxDSOFramer.AxFramerControl()
        Me.pnlMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlToolbar.SuspendLayout()
        Me.pnlCombo.SuspendLayout()
        Me.pnlGloUC_PastWordNotes.SuspendLayout()
        Me.pnl_chkShowPreview.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlGloUC_TemplateTreeControl.SuspendLayout()
        Me.pnl_cmdPastExam.SuspendLayout()
        CType(Me.wdDisclosureMgmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrDocProtect
        '
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlgloUC_Addendum)
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.pnlToolbar)
        Me.pnlMain.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlMain.Controls.Add(Me.lbl_pnlTop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(461, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(756, 554)
        Me.pnlMain.TabIndex = 3
        '
        'pnlgloUC_Addendum
        '
        Me.pnlgloUC_Addendum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlgloUC_Addendum.Location = New System.Drawing.Point(0, 34)
        Me.pnlgloUC_Addendum.Name = "pnlgloUC_Addendum"
        Me.pnlgloUC_Addendum.Size = New System.Drawing.Size(753, 516)
        Me.pnlgloUC_Addendum.TabIndex = 12
        Me.pnlgloUC_Addendum.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.wdDisclosureMgmt)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 34)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(753, 516)
        Me.Panel2.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(752, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 515)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 515)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(753, 1)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "label2"
        Me.Label2.Visible = False
        '
        'pnlToolbar
        '
        Me.pnlToolbar.Controls.Add(Me.pnlCombo)
        Me.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolbar.Location = New System.Drawing.Point(0, 4)
        Me.pnlToolbar.Name = "pnlToolbar"
        Me.pnlToolbar.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlToolbar.Size = New System.Drawing.Size(753, 30)
        Me.pnlToolbar.TabIndex = 11
        '
        'pnlCombo
        '
        Me.pnlCombo.BackColor = System.Drawing.Color.Transparent
        Me.pnlCombo.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlCombo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCombo.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.pnlCombo.Controls.Add(Me.lblLetterDt)
        Me.pnlCombo.Controls.Add(Me.dtDisclosureDate)
        Me.pnlCombo.Controls.Add(Me.Label8)
        Me.pnlCombo.Controls.Add(Me.Label5)
        Me.pnlCombo.Controls.Add(Me.cmbTemplate)
        Me.pnlCombo.Controls.Add(Me.lblSelectTem)
        Me.pnlCombo.Controls.Add(Me.Label1)
        Me.pnlCombo.Controls.Add(Me.Label4)
        Me.pnlCombo.Controls.Add(Me.Label3)
        Me.pnlCombo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCombo.Location = New System.Drawing.Point(0, 3)
        Me.pnlCombo.Name = "pnlCombo"
        Me.pnlCombo.Size = New System.Drawing.Size(753, 24)
        Me.pnlCombo.TabIndex = 4
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(305, 2)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 19)
        Me.GloUC_AddRefreshDic1.TabIndex = 43
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'lblLetterDt
        '
        Me.lblLetterDt.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblLetterDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLetterDt.Location = New System.Drawing.Point(454, 1)
        Me.lblLetterDt.Name = "lblLetterDt"
        Me.lblLetterDt.Size = New System.Drawing.Size(94, 22)
        Me.lblLetterDt.TabIndex = 32
        Me.lblLetterDt.Text = "Letter Date :"
        Me.lblLetterDt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtDisclosureDate
        '
        Me.dtDisclosureDate.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDisclosureDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtDisclosureDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtDisclosureDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtDisclosureDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtDisclosureDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtDisclosureDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtDisclosureDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDisclosureDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDisclosureDate.Location = New System.Drawing.Point(548, 1)
        Me.dtDisclosureDate.Name = "dtDisclosureDate"
        Me.dtDisclosureDate.Size = New System.Drawing.Size(169, 22)
        Me.dtDisclosureDate.TabIndex = 31
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(717, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 22)
        Me.Label8.TabIndex = 42
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(752, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 22)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "label4"
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.Location = New System.Drawing.Point(126, 1)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(172, 22)
        Me.cmbTemplate.TabIndex = 30
        '
        'lblSelectTem
        '
        Me.lblSelectTem.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSelectTem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectTem.Location = New System.Drawing.Point(1, 1)
        Me.lblSelectTem.Name = "lblSelectTem"
        Me.lblSelectTem.Size = New System.Drawing.Size(125, 22)
        Me.lblSelectTem.TabIndex = 29
        Me.lblSelectTem.Text = "  Select Template :"
        Me.lblSelectTem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(752, 1)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(1, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(752, 1)
        Me.Label4.TabIndex = 39
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 24)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "label4"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(0, 550)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(753, 1)
        Me.lbl_pnlBottom.TabIndex = 9
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(753, 1)
        Me.lbl_pnlTop.TabIndex = 6
        Me.lbl_pnlTop.Text = "label1"
        Me.lbl_pnlTop.Visible = False
        '
        'pnlToolStripContainer
        '
        Me.pnlToolStripContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStripContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStripContainer.Name = "pnlToolStripContainer"
        Me.pnlToolStripContainer.Size = New System.Drawing.Size(1217, 54)
        Me.pnlToolStripContainer.TabIndex = 10
        '
        'pnlGloUC_PastWordNotes
        '
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.GloUC_PastWordNotes1)
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.pnl_chkShowPreview)
        Me.pnlGloUC_PastWordNotes.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_PastWordNotes.Location = New System.Drawing.Point(0, 54)
        Me.pnlGloUC_PastWordNotes.Name = "pnlGloUC_PastWordNotes"
        Me.pnlGloUC_PastWordNotes.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlGloUC_PastWordNotes.Size = New System.Drawing.Size(217, 554)
        Me.pnlGloUC_PastWordNotes.TabIndex = 13
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
        Me.GloUC_PastWordNotes1.Location = New System.Drawing.Point(0, 30)
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
        Me.GloUC_PastWordNotes1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_PastWordNotes1.PATIENTIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.SHOWPASTs = False
        Me.GloUC_PastWordNotes1.Size = New System.Drawing.Size(217, 524)
        Me.GloUC_PastWordNotes1.STRFORMNAMEs = "0"
        Me.GloUC_PastWordNotes1.TabIndex = 0
        Me.GloUC_PastWordNotes1.TLSMESSAGESs = Nothing
        '
        'pnl_chkShowPreview
        '
        Me.pnl_chkShowPreview.BackColor = System.Drawing.Color.Transparent
        Me.pnl_chkShowPreview.Controls.Add(Me.Panel4)
        Me.pnl_chkShowPreview.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_chkShowPreview.Location = New System.Drawing.Point(0, 3)
        Me.pnl_chkShowPreview.Name = "pnl_chkShowPreview"
        Me.pnl_chkShowPreview.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_chkShowPreview.Size = New System.Drawing.Size(217, 27)
        Me.pnl_chkShowPreview.TabIndex = 36
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(3, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(214, 24)
        Me.Panel4.TabIndex = 34
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(212, 22)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Past Disclosure Management"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(212, 1)
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
        Me.Label16.Location = New System.Drawing.Point(213, 1)
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
        Me.Label17.Size = New System.Drawing.Size(214, 1)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(217, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 554)
        Me.Splitter2.TabIndex = 15
        Me.Splitter2.TabStop = False
        '
        'pnlGloUC_TemplateTreeControl
        '
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.GloUC_TemplateTreeControl_DisclosureManagement)
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.pnl_cmdPastExam)
        Me.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_TemplateTreeControl.Location = New System.Drawing.Point(220, 54)
        Me.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl"
        Me.pnlGloUC_TemplateTreeControl.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlGloUC_TemplateTreeControl.Size = New System.Drawing.Size(238, 554)
        Me.pnlGloUC_TemplateTreeControl.TabIndex = 16
        '
        'GloUC_TemplateTreeControl_DisclosureManagement
        '
        Me.GloUC_TemplateTreeControl_DisclosureManagement.DocCriteria = Nothing
        Me.GloUC_TemplateTreeControl_DisclosureManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TemplateTreeControl_DisclosureManagement.ExpandConsent = False
        Me.GloUC_TemplateTreeControl_DisclosureManagement.Location = New System.Drawing.Point(0, 30)
        Me.GloUC_TemplateTreeControl_DisclosureManagement.Name = "GloUC_TemplateTreeControl_DisclosureManagement"
        Me.GloUC_TemplateTreeControl_DisclosureManagement.ObjClsWord = Nothing
        Me.GloUC_TemplateTreeControl_DisclosureManagement.ProviderId = CType(0, Long)
        Me.GloUC_TemplateTreeControl_DisclosureManagement.Size = New System.Drawing.Size(238, 521)
        Me.GloUC_TemplateTreeControl_DisclosureManagement.TabIndex = 0
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
        Me.pnl_cmdPastExam.Location = New System.Drawing.Point(0, 3)
        Me.pnl_cmdPastExam.Name = "pnl_cmdPastExam"
        Me.pnl_cmdPastExam.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnl_cmdPastExam.Size = New System.Drawing.Size(238, 27)
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
        Me.cmdPastExam.Size = New System.Drawing.Size(236, 22)
        Me.cmdPastExam.TabIndex = 3
        Me.cmdPastExam.Text = "Show Past Disclosure Management"
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
        Me.Label11.Size = New System.Drawing.Size(236, 1)
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
        Me.Label13.Location = New System.Drawing.Point(237, 1)
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
        Me.Label14.Size = New System.Drawing.Size(238, 1)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(458, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 554)
        Me.Splitter1.TabIndex = 17
        Me.Splitter1.TabStop = False
        '
        'wdDisclosureMgmt
        '
        Me.wdDisclosureMgmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdDisclosureMgmt.Enabled = True
        Me.wdDisclosureMgmt.Location = New System.Drawing.Point(0, 0)
        Me.wdDisclosureMgmt.Name = "wdDisclosureMgmt"
        Me.wdDisclosureMgmt.OcxState = CType(resources.GetObject("wdDisclosureMgmt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdDisclosureMgmt.Size = New System.Drawing.Size(753, 516)
        Me.wdDisclosureMgmt.TabIndex = 40
        '
        'frmDisclosureMgmt
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1217, 608)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlGloUC_TemplateTreeControl)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlGloUC_PastWordNotes)
        Me.Controls.Add(Me.pnlToolStripContainer)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDisclosureMgmt"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disclosure Management"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlToolbar.ResumeLayout(False)
        Me.pnlCombo.ResumeLayout(False)
        Me.pnlGloUC_PastWordNotes.ResumeLayout(False)
        Me.pnl_chkShowPreview.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlGloUC_TemplateTreeControl.ResumeLayout(False)
        Me.pnl_cmdPastExam.ResumeLayout(False)
        CType(Me.wdDisclosureMgmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '' 20070427
    '' If Form is Open from MainMenu Patient WorkStatus
    '' With/ WithOut Restriction 
    Private _strPatientWorkStatus As String = ""
    ''''
    Public CancelClick As Boolean
    Dim nVisitId As Long

    Dim objDisclosure As New clsDisclosureMgmt
    Private WithEvents oCurDoc As Wd.Document
    '  Private WithEvents oTempDoc As Wd.Document

    Private _Arrlist As ArrayList
    Private _strNotes As String
    Public Shared strAddendum As String
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    Private WithEvents oWordApp As Wd.Application
#Region "variable Declaration for UC treeview"
    Dim bIsPastDMClick As Boolean = False
    '  Dim obj_UCclsDisclosureMgmt As clsDisclosureMgmt
    ' Dim Obj_UCWord As clsWordDocument
    'Dim obj_UCCriteria As DocCriteria
    'Dim clsPatientExams As clsPatientExams
#End Region
    Private Sub frmDisclosureMgmt_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdDisclosureMgmt
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


        'Developer: Yatin N.Bhagat
        'Date:12/26/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
        'Reason: Handler For DDLCBEvent is Not Added while activating the form
        If Not (IsNothing(wdDisclosureMgmt)) Then
            If Not (IsNothing(wdDisclosureMgmt.DocumentName)) Then
                If Not (IsNothing(wdDisclosureMgmt.ActiveDocument)) Then
                    oCurDoc = wdDisclosureMgmt.ActiveDocument
                    oWordApp = oCurDoc.Application
                    Try
                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    Try
                        AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    isHandlerRemoved = False
                    oCurDoc.ActiveWindow.SetFocus()
                    wdDisclosureMgmt.Focus()
                End If
            End If
        End If


        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmDisclosureMgmt_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        'Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmDisclosureMgmt_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter

    End Sub

    Private Sub frmDisclosureMgmt_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            If m_IsRecordLock = False Then
                UnLock_Transaction(TrnType.DisclosureManagement, m_DisclosureID, 0, Now)
            End If

            '' <><><> Unlock the Record <><><>
            If IsNothing(MyCaller) = False Then
                MyCaller.RefreshDisclosure(m_DisclosureID)
            End If
            'If Not oWordApp Is Nothing Then
            '    oWordApp.RecentFiles.Maximum = 0
            '    oWordApp.DisplayRecentFiles = False
            '    ' Marshal.FinalReleaseComObject(oWordApp)
            'End If
            'Disposed UC objects
            Dispose_Object()
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
            End If

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmDisclosureMgmt_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If m_IsRecordLock Then
            Exit Sub
        End If

        ''By Shweta 20090827
        'If CheckWordForException() = False Then
        '    Exit Sub
        'End If
        ''End Shweta

        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Do you want to save the changes to Disclosure Management?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Result = DialogResult.Yes Then
                    If m_IsFinished Then
                        Call SaveDisclosure(True, True)
                    Else
                        Call SaveDisclosure(False, True)
                    End If

                    If IsModify Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", m_PatientID, m_DisclosureID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", m_PatientID, m_DisclosureID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If

                    e.Cancel = False
                ElseIf Result = DialogResult.Cancel Then
                    'Shubhangi 20091107
                    'It should not close the form on cancel click
                    e.Cancel = True
                    '' Nothing to here
                ElseIf Result = DialogResult.No Then
                    If FullSaveStatus = 1 And IsModify = False Then
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Disclosure opened and canceled", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Disclosure opened and canceled", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Disclosure opened and canceled", gstrLoginName, gstrClientMachineName, m_PatientID)
                        'Delete disclosure 
                        objDisclosure.DeleteDisclosureMgmt(m_DisclosureID, m_PatientID)

                    End If
                    e.Cancel = False
                End If
            Else
                If FullSaveStatus = 1 And IsModify = False Then
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Disclosure opened and canceled", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Disclosure opened and canceled", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Disclosure opened and canceled", gstrLoginName, gstrClientMachineName, m_PatientID)
                    'Delete disclosure 
                    objDisclosure.DeleteDisclosureMgmt(m_DisclosureID, m_PatientID)

                End If
                e.Cancel = False
            End If
        Else
            If FullSaveStatus = 1 And IsModify = False Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Disclosure opened and canceled", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Disclosure opened and canceled", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Disclosure opened and canceled", gstrLoginName, gstrClientMachineName, m_PatientID)
                'Delete disclosure 
                objDisclosure.DeleteDisclosureMgmt(m_DisclosureID, m_PatientID)

            End If
            e.Cancel = False
        End If
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.UnInitializeVoiceComponents()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.UnInitializeVoiceComponents()
            End If
        End If
    End Sub

    Private Sub frmDisclosureMgmt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''By Shweta 20090827
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            ''End Shweta

            Dim dt As New DataTable

            dtDisclosureDate.Format = DateTimePickerFormat.Custom
            dtDisclosureDate.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtDisclosureDate.Value = Now


            'Dim objword As New clsWordDocument
            ''
            objDisclosure.fill_widthofExam(pnlGloUC_TemplateTreeControl) ''''added to load width of DB

            If m_DisclosureID <> 0 Then
                Dim objclsNotes As New clsNurseNotes
                dt = objclsNotes.FillTemplate(enumTemplateFlag.DisclosureMangement, m_DisclosureID, m_TemplateID).Copy()
                objclsNotes.Dispose()
                objclsNotes = Nothing

            Else
                dt = objDisclosure.FillTemplates() '' To fill Disclosure Management Templates

            End If
            ''

            'objword = Nothing
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
            If m_DisclosureID <> 0 Then
                '' for Update 
                Dim dtLetter As DataTable
                dtLetter = objDisclosure.ScanDisclosure(m_DisclosureID)

                'If Not (dtLetter.Rows(0)(1)) Is DBNull.Value Then
                Fill_PatientConsent(dtLetter)
                'Else
                '    wdDisclosureMgmt.CreateNew("Word.Application")
                'End If
            Else
                '' for New Disclosure - Fill by default Template 
                OpenNewDisclosure()
                If cmbTemplate.SelectedValue > 0 Then
                    Call Fill_TemplateGallery()
                    'If IsNothing(_Arrlist) = False Then
                    '    Call Fill_Restrictions()
                Else
                    wdDisclosureMgmt.CreateNew("Word.Application")

                End If

            End If
            If IsModify Then
                cmbTemplate.Enabled = False
                dtDisclosureDate.Enabled = False
            End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If
            If gblnAssociatedProviderSignature And m_IsFinished = False And m_IsRecordLock = False Then
                tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
                tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Remove(tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            Else
                tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
                If (tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Contains(tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                    tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Add(tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Name)
                End If

            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            pnlGloUC_PastWordNotes.Hide()
            InitiliseTemplateTreeControl()
            CallPastData()
            calltoAddRefreshButtonControl()

            If m_IsRecordLock Or m_IsFinished Then
                GloUC_AddRefreshDic1.Visible = False
                pnlGloUC_TemplateTreeControl.Hide()
                pnlGloUC_PastWordNotes.Show()
                Splitter1.Hide()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    'Private Sub Fill_Restrictions()

    '    Dim strFindText As String = "Field"
    '    Dim nCount As Integer
    '    For i As Integer = 0 To _Arrlist.Count - 1
    '        Dim lst As New myList
    '        lst = CType(_Arrlist(i), myList)
    '        strFindText = "Field"
    '        strFindText = strFindText & i + 1

    '        With oCurDoc.Content.Find
    '            Do While .Execute(FindText:=strFindText, Forward:=True, Format:=True, MatchWholeWord:=True) = True
    '                nCount = nCount + 1
    '            Loop
    '        End With
    '        ''Replace all fields
    '        For j As Integer = 1 To nCount
    '            Find_n_ReplaceText(strFindText, lst.Value, lst.Type, i)
    '        Next
    '    Next

    '    '[Notes]
    '    With oCurDoc.Content.Find
    '        Do While .Execute(FindText:="[Notes]", Forward:=True, Format:=True, MatchWholeWord:=True) = True
    '            nCount = nCount + 1
    '        Loop
    '    End With
    '    ''Replace all fields
    '    For j As Integer = 1 To nCount
    '        Find_n_ReplaceText("[Notes]", _strNotes, False, 0)
    '    Next
    'End Sub

    '' for exiting Consent
    Private Sub Fill_PatientConsent(ByVal dtLetter As DataTable)
        ''dtLetter(0) = Letter Date
        ''dtLetter(1) = PatientLetter (Object)
        cmbTemplate.SelectedValue = m_TemplateID
        If Not IsNothing(dtLetter) Then
            If dtLetter.Rows.Count > 0 Then
                dtDisclosureDate.Value = Format(dtLetter.Rows(0)(0), "MM/dd/yyyy hh:mm tt")
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

                wdDisclosureMgmt.Close()
            End If
        Else
            wdDisclosureMgmt.Close()
        End If

    End Sub

    Private Sub Fill_TemplateGallery()



        Dim strFileName As String

        objword = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Template
        objCriteria.PrimaryID = cmbTemplate.SelectedValue

        objword.DocumentCriteria = objCriteria
        ''//Retrieving the DisclosureMgmt from DB and Save it as Physical File
        strFileName = objword.RetrieveDocumentFile()
        objCriteria.Dispose()
        objCriteria = Nothing
        objword = Nothing
        If (IsNothing(strFileName) = False) Then
            If strFileName <> "" Then

                LoadWordUserControl(strFileName, True)
                'Set the Start postion of the cursor in documents
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
            Else
                wdDisclosureMgmt.Close()

            End If
        Else
            wdDisclosureMgmt.Close()
        End If
       

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

            tmrDocProtect.Enabled = True
            tmrDocProtect.Interval = 1
        Else
            If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Unprotect()
            End If

            tmrDocProtect.Enabled = False
        End If

    End Sub

    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType)
        oCurDoc.ActiveWindow.SetFocus()
        Dim objword As New clsWordDocument
        Dim objCriteria As New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Others
        objCriteria.PatientID = m_PatientID
        '  objCriteria.VisitID = dtDisclosureDate.Tag
        If (IsNothing(dtDisclosureDate) = False) Then
            If IsNothing(dtDisclosureDate.Tag) Then ''condition added for bugid 87061  
                objCriteria.VisitID = GenerateVisitID(dtDisclosureDate.Value, m_PatientID)
            Else
                objCriteria.VisitID = dtDisclosureDate.Tag
            End If
        End If
        objCriteria.PrimaryID = m_DisclosureID
        ' objCriteria.ProviderID = gnPatientProviderID
        objword.DocumentCriteria = objCriteria
        objword.CurDocument = oCurDoc
        objword.WaitControlPanel = Me.Panel2
        objword.GetFormFieldData(_DocType)
        oCurDoc = objword.CurDocument
        'wdCtrlPatientConsent.document = objword.CurDocument
        objCriteria.Dispose()
        objCriteria = Nothing
        objword = Nothing


    End Sub

    Private Sub tmrDocProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDocProtect.Tick
        Try
            tmrDocProtect.Enabled = False
            If m_IsFinished = True And blnIsAddendum = False Then
                If Not oCurDoc Is Nothing Then
                    'Bug #85105: 00000876: Document in procted mode while faxing
                    'oCurDoc.ActiveWindow.SetFocus()
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

         Finally
            tmrDocProtect.Enabled = True

        End Try
    End Sub

    ''To load dynamically Patient Details and form specific Controls
    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.DisclosureManagement)
        _PatientStrip.Dock = DockStyle.Top
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
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
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then
                        'Set focus to Wd object
                        oCurDoc.ActiveWindow.SetFocus()
                        ''''' Statement to Go end of Selelcted Wd Document
                        '//oCurDoc1.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
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
                'End of code added by Rahul Patel on 26-10-2010

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
                        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=oFiles.Item(i), LinkToFile:=False, SaveWithDocument:=True)
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

                ''frmObj = Nothing
                i = Nothing
                ''_Files = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally

        End Try
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
            ''commented by Dhruv 20091214 
            ''To not to save on form closing
            'If File.Exists(ImagePath) Then
            '    If Not oCurDoc Is Nothing Then
            '        oCurDoc.ActiveWindow.SetFocus()

            '        '' SUDHIR 20090619 '' 
            '        Dim oWord As New clsWordDocument
            '        oWord.CurDocument = oCurDoc
            '        oWord.InsertImage(ImagePath)
            '        oWord = Nothing
            '        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
            '        '' END SUDHIR ''

            '        oCurDoc.Application.Selection.TypeParagraph()
            '        '' By Bipin - 20070427
            '        '''' Add Date Time When Signature is Inserted
            '        oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
            '        ''''
            '    End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub

    Public Sub InsertCoSignature()
        Try
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
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    'oCurDoc.Application.Selection.Move(1)
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Disclosure Management", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

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
        '        blnResult = oclsProvider.CheckpatientProviderStatus(m_PatientID, ProviderID)
        '        If blnSignClick = False Then
        '            If blnResult Then
        '                ''Selected Provider Is Exam Provider
        '            Else
        '                Pat_Provider = oclsProvider.GetPatientProviderName(m_PatientID)
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

        '            _ProviderID = oclsProvider.GetPatientProvider(m_PatientID)
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
        '                        Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_PatientID)
        '                        MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                        oclsProvider = Nothing
        '                        Return
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        '    Dim objWord As New clsWordDocument
        '    Dim objCriteria As DocCriteria
        '    objCriteria = New DocCriteria
        '    objCriteria.DocCategory = enumDocCategory.Others
        '    objCriteria.PatientID = m_PatientID
        '    objCriteria.VisitID = 0
        '    objCriteria.PrimaryID = 0
        '    objCriteria.ProviderID = ProviderID
        '    objWord.DocumentCriteria = objCriteria

        '    ImagePath = objWord.getData_FromDB("Provider_MST.imgSignature", "Provider Signature")
        '    objCriteria = Nothing
        '    objWord = Nothing
        '    ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
        '    If ImagePath = "" Then
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
        '    If System.IO.File.Exists(ImagePath) Then

        '        '' SUDHIR 20090619 '' 
        '        Dim oWord As New clsWordDocument
        '        oWord.CurDocument = oCurDoc
        '        oWord.InsertImage(ImagePath)
        '        oWord = Nothing
        '        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
        '        '' END SUDHIR ''

        '        'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
        '        Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
        '        If wdRng.Tables.Count > 0 Then
        '            'oCurDoc.Application.Selection.Move(1)
        '            oCurDoc.Application.Selection.EndKey()
        '        End If
        '        'end code added by dipak 
        '        Dim clsExam As New clsPatientExams
        '        Dim strProviderName As String = ""
        '        If ProviderID <> 0 Then
        '            strProviderName = clsExam.GetProvidernameforExam(ProviderID)
        '        Else
        '            strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
        '        End If

        '        oCurDoc.Application.Selection.TypeParagraph()
        '        '' By Mahesh Signature With Date - 20070113
        '        '''' Add Date Time When Signature is Inserted

        '        'Developer: Yatin N.Bhagat
        '        'Date:01/20/2012
        '        'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
        '        'Reason: If Condition is added to check the Setting
        '        If oclsProvider.AddUserNameInProviderSignature() Then
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
        '        Else
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")) '& " (" & gstrLoginName & ")"
        '        End If
        '        ''''
        '        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
        '        ''Added Rahul P on 20101011
        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        '        ''
        '        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
        '    End If
        'Catch objErr As Exception
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
            Dim objWord As New clsWordDocument
            Dim oclsProvider As New clsProvider
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_PatientID, 0, blnSignClick)
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
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub FaxDisclosure(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.DisclosureManagement, m_PatientID, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        'oTempDoc.ActiveWindow.SetFocus()
        ''Unprotect the document

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Commented by Shweta 20100201
        '''''''Against the bug id:5260 '''''''
        'Check the FAX Cover Page is enabled or not.
        'If the FAX Cover Page is enabled then Delete the Page Header from Exam
        'If gblnFAXCoverPage Then
        '    'To Delete Header
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Deleting Disclosure Management Page Header", gloAuditTrail.ActivityOutCome.Success)
        '    'UpdateLog("Deleting Disclosure Management Page Header")
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader

        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Disclosure Management Page Header deleted", gloAuditTrail.ActivityOutCome.Success)
        '            'UpdateLog("Disclosure Management Page Header deleted")
        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Error Deleting Disclosure Management Page Header - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '        'UpdateVoiceLog("Error Deleting Disclosure Management Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try
        'End If
        'End Commenting

        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, m_PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.Text, clsPrintFAX.enmFAXType.PatientLetters) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the Fax due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        '  wdTemp.Activate()
        ' wdTemp.Select()
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    'Private Sub invokeMySave()
    '    If Me.InvokeRequired Then
    '        Me.Invoke(New MethodInvoker(AddressOf invokeMySave))
    '    Else
    '        invokingMySave()
    '    End If
    'End Sub
    'Private Sub invokingMySave()
    '    Try
    '        wdDisclosureMgmt.Save()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function SaveDisclosure(ByVal IsFinished As Boolean, Optional ByVal IsClose As Boolean = False) As Boolean
        Try
            If oCurDoc Is Nothing Then
                Return False
            End If
            SaveDisclosure = False

            If pnlGloUC_TemplateTreeControl.Width.ToString <> "" Then
                objDisclosure.SaveWidthInDatabase(gnLoginID, pnlGloUC_TemplateTreeControl.Width) ''''save width of panel in DB
            End If


            '' IF IsFinished = True Then Remove the Unused Fields 
            If IsFinished Then

                gloWord.LoadAndCloseWord.LockFields(oCurDoc)

                oCurDoc.ActiveWindow.SetFocus()
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

            'Try
            '    '   
            '    'If (IsNothing(oCurDoc) = False) Then
            '    '    oCurDoc.Save()
            '    'Else
            '    ' wdDisclosureMgmt.Save()
            '    gloWord.LoadAndCloseWord.SaveDSO(wdDisclosureMgmt, oCurDoc, oWordApp)
            '    'End If

            '    'invokeMySave()
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            'End Try

            'Dim strFileName As String
            'Dim isExceptionWhileCopingFile As Boolean = False
            'strFileName = ExamNewDocumentName

            'Try
            '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    isExceptionWhileCopingFile = True
            '    ex = Nothing
            'End Try

            'If (isExceptionWhileCopingFile) Then
            '    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '    wdDisclosureMgmt.Close()
            'End If

            'strFileName = ExamNewDocumentName
            '' wdPatientConsent.Save(strFileName, True, "", "")
            'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            'wdDisclosureMgmt.Close()
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdDisclosureMgmt, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If
            m_DisclosureID = objDisclosure.SaveDisclosureBytes(m_DisclosureID, m_PatientID, cmbTemplate.SelectedValue, Format(dtDisclosureDate.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, cmbTemplate.Text, IsFinished)

            If m_DisclosureID > 0 Then
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

                    '-- sarika 20081201 template name editable after save
                    cmbTemplate.Enabled = False
                    dtDisclosureDate.Enabled = False
                    '--
                    'Set the Start postion of the cursor in documents
                    'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    'oCurDoc.Saved = True

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
                    oCurDoc.Saved = False
                End If

            End If
            FullSaveStatus = 2
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function
    Private Sub OpenNewDisclosure()
        Dim strFileName As String

        ''If gnVisitID = 0 Then '' condition commented by Sandip Darade for the the flow to not to use incorrect visit id 
        'come comented by dipak for remove referances of gnVisitID
        'gnVisitID = GenerateVisitID(m_PatientID)
        'dtDisclosureDate.Tag = gnVisitID
        m_visitID = GenerateVisitID(m_PatientID)
        dtDisclosureDate.Tag = m_visitID
        ''End If

        strFileName = ""
        '        clsobjPatExam.SaveExam(examid, mgnVisitID, PatientID, lblExamName.Text, strFileName, 0, dtDOS, 0, Date.Now, False)

        m_DisclosureID = objDisclosure.SaveDisclosure(m_DisclosureID, m_PatientID, cmbTemplate.SelectedValue, Format(dtDisclosureDate.Value, "MM/dd/yyyy hh:mm tt"), strFileName, cmbTemplate.Text, False)

        Dim blnRecordLock As Boolean = False
        If gblnRecordLocking = True Then
            Dim mydt As mytable
            mydt = Scan_n_Lock_Transaction(TrnType.DisclosureManagement, m_DisclosureID, 0, Now)
            If (IsNothing(mydt) = False) Then
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This Disclosure is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        blnRecordLock = True
                    Else
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                        Exit Sub
                    End If
                End If
                If (IsNothing(mydt) = False) Then
                    mydt.Dispose()
                    mydt = Nothing
                End If
            End If

        End If

        FullSaveStatus = 1

    End Sub
    Private Sub Print()

        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Disclosure Management Printed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Disclosure Management Printed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Disclosure Management Printed", gstrLoginName, gstrClientMachineName, gnPatientID)
        End If

    End Sub

    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        If Not oCurDoc Is Nothing Then
            Dim _SaveFlag As Boolean = False
            Dim PageNo As Integer = 0
            Dim totalPages As Integer = 0
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            Dim Missing As Object = System.Reflection.Missing.Value

            If oCurDoc.Saved Then
                _SaveFlag = True
            End If
            'Dim sFileName As String = ExamNewDocumentName
            'Dim wordRefresh As New WordRefresh()
            'Ashish added on 1st October 
            'to prevent screen from refreshing
            'Dim WDocViewType As Wd.WdViewType
            'If IsPrintFlag Then
            '    'Ashish added on 31st October 
            '    'to prevent screen from refreshing
            '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
            '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
            'End If
            If IsNothing(wdDisclosureMgmt) = False AndAlso IsNothing(oWordApp) = False Then
                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdDisclosureMgmt, oCurDoc, oWordApp)
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
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FaxDisclosure, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try




                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
                If Not IsNothing(oCurDoc) Then
                    oCurDoc.Saved = _SaveFlag
                End If
                'LoadWordUserControl(sFileName, False)
                'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                ''Set the Start postion of the cursor in documents
                'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                'oCurDoc.Saved = _SaveFlag

                'If IsPrintFlag Then
                '    'Ashish added on 31st October 
                '    'to prevent screen from refreshing
                '    'WordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
                '    'WDocViewType = Nothing
                'End If

                'wordRefresh.Dispose()
                'wordRefresh = Nothing
            End If
        End If

    End Sub

    Private Sub frmDisclosureMgmt_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            Me.Hide()
            'Call MyCaller.RefreshConsent(gnPatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            oCurDoc = Nothing
            'wdCtrlPatientConsent.CloseControl()
            wdDisclosureMgmt.Close()
        End Try
    End Sub

    Private Sub InsertAddendum()
        Try
            If m_IsFinished = True Then
                'Dim f As New frmMsg_Addendum(3)
                'f.ShowDialog(Me)
                'If strAddendum <> "" Then
                '    oCurDoc.ActiveWindow.SetFocus()
                '    If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '        oCurDoc.Application.ActiveDocument.Unprotect()
                '        oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                '        oCurDoc.Application.Selection.TypeParagraph()
                '        oCurDoc.Application.Selection.TypeParagraph()
                '        oCurDoc.Application.Selection.TypeText(Text:="Addendum - '" & gstrLoginName & "' " & Now)
                '        oCurDoc.Application.Selection.TypeParagraph()
                '        oCurDoc.Application.ActiveDocument.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=1, NumColumns:=1)
                '        oCurDoc.Application.Selection.Text = strAddendum
                '        oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                '    End If
                'End If
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Visible = False
                End If

                pnlToolStripContainer.Visible = False
                pnlToolbar.Visible = True

                GloUC_TemplateTreeControl_DisclosureManagement.Visible = False
                pnlGloUC_PastWordNotes.Visible = False
                ouctlgloUC_Addendum = New gloUC_Addendum(0, m_DisclosureID, m_PatientID)
                blnIsAddendum = True
                pnlgloUC_Addendum.Controls.Add(ouctlgloUC_Addendum)
                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                ouctlgloUC_Addendum.BringToFront()
                pnlgloUC_Addendum.Visible = True
                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'oCurDoc1.Application.Selection.InsertRows(1)

    End Sub

    Public Sub Navigate1(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsDisclosureMgmt.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsDisclosureMgmt.MyToolStrip.Items("Mic").Visible = True
                        tlsDisclosureMgmt.ButtonsToHide.Remove(tlsDisclosureMgmt.MyToolStrip.Items("Mic").Name)
                        tlsDisclosureMgmt.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                    End If

                ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic") Is Nothing Then
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Visible = True
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        Me.ouctlgloUC_Addendum.tlsAddendum.ButtonsToHide.Remove(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name)
                    End If

                End If
            ElseIf strstring = "OFF" Then

                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsDisclosureMgmt.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsDisclosureMgmt.MyToolStrip.Items("Mic").Visible = True
                        tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Remove(tlsDisclosureMgmt.MyToolStrip.Items("Mic").Name)
                        tlsDisclosureMgmt.ButtonsToHide.Remove(tlsDisclosureMgmt.MyToolStrip.Items("Mic").Name)
                    End If


                ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsDisclosureMgmt.MyToolStrip.Items("Mic") Is Nothing Then
                        If Not IsNothing(ouctlgloUC_Addendum) Then
                            Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Visible = True
                            Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
                            Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.ButtonsToHide.Remove(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name)
                        End If
                    End If

                    Exit Sub
                Else

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsDisclosureMgmt.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsDisclosureMgmt.MyToolStrip.Items("Mic").Visible = False
                        If Not IsNothing(ouctlgloUC_Addendum) Then
                            If Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.ButtonsToHide.Contains(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name) = False Then
                                Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.ButtonsToHide.Add(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name)
                            End If
                        End If
                    End If


                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsDisclosureMgmt.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsDisclosureMgmt.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
                End If

            Else
                If bnlIsFaxOpened = False Then
                    If Not IsNothing(ouctlgloUC_Addendum) Then
                        ouctlgloUC_Addendum.Navigate(strstring)
                    Else
                        If Not oCurDoc Is Nothing Then
                            oCurDoc.ActiveWindow.SetFocus()
                            Try
                                gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                ex = Nothing
                            End Try

                        End If
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

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        If Not blnCmbSelTemplate Then
            loadToolStrip()
        End If

        'wdDisclosureMgmt.Open(strFileName)
        'Dim oWordApp As Wd.Application = Nothing
        gloWord.LoadAndCloseWord.OpenDSO(wdDisclosureMgmt, strFileName, oCurDoc, oWordApp)

        If blnGetData Then

            ''//To retrieve the Form fields for the Word document
            objword = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientID
            ''If gnVisitID = 0 Then '' condition commented by Sandip Darade for the the flow to not to use incorrect visit id 
            'code commented and modified by dipak to remove refferances of gnVisitID
            'gnVisitID = GenerateVisitID(m_PatientID)
            'dtDisclosureDate.Tag = gnVisitID
            m_visitID = GenerateVisitID(m_PatientID)
            dtDisclosureDate.Tag = m_visitID

            'End modification by dipak

            '' End If
            objCriteria.VisitID = dtDisclosureDate.Tag
            objCriteria.PrimaryID = m_DisclosureID
            objword.DocumentCriteria = objCriteria
            objword.CurDocument = oCurDoc

            'Sagar Ghodke : 26Nov2014 - Reverting liquid link changes - START
            ''Replace Form fields with Concerned data
            'objword.GetFormFieldDataDBCalls(enumDocType.None)
            objword.GetFormFieldData(enumDocType.None)
            'Sagar Ghodke : 26Nov2014 - Reverting liquid link changes - END

            oCurDoc = objword.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria.Dispose()
            objCriteria = Nothing
            objword = Nothing
        Else
            objword = New clsWordDocument
            objword.CurDocument = oCurDoc
            objword.HighlightColor()
            oCurDoc = objword.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objword = Nothing
        End If

        SetWordObjectEntry(m_IsFinished)

    End Sub

    Private Sub loadToolStrip()
        If Not tlsDisclosureMgmt Is Nothing Then
            tlsDisclosureMgmt.Dispose()
        End If

        tlsDisclosureMgmt = New WordToolStrip.gloWordToolStrip
        tlsDisclosureMgmt.Dock = DockStyle.Top

        tlsDisclosureMgmt.ConnectionString = GetConnectionString()
        tlsDisclosureMgmt.UserID = gnLoginID
        tlsDisclosureMgmt.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsDisclosureMgmt.ptProvider = oclsProvider.GetPatientProviderName(m_PatientID)
        tlsDisclosureMgmt.ptProviderId = oclsProvider.GetPatientProvider(m_PatientID)
        oclsProvider.Dispose()
        oclsProvider = Nothing

        If m_IsFinished Then

            tlsDisclosureMgmt.FormType = WordToolStrip.enumControlType.DisclosureManagementAddendum
            cmbTemplate.Enabled = False
            dtDisclosureDate.Enabled = False
        Else
            tlsDisclosureMgmt.IsCoSignEnabled = gblnCoSignFlag
            tlsDisclosureMgmt.FormType = WordToolStrip.enumControlType.DisclosureManagement
            cmbTemplate.Enabled = True
            dtDisclosureDate.Enabled = True
        End If

        Me.pnlToolStripContainer.Controls.Add(tlsDisclosureMgmt)
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsDisclosureMgmt
                ShowMicroPhone()
            End If
        End If
        If gblnAssociatedProviderSignature And m_IsFinished = False And m_IsRecordLock = False Then
            tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Remove(tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        Else
            tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Contains(tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Add(tlsDisclosureMgmt.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsDisclosureMgmt.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Contains(tlsDisclosureMgmt.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsDisclosureMgmt.MyToolStrip.ButtonsToHide.Add(tlsDisclosureMgmt.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsDisclosureMgmt.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsDisclosureMgmt.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
        ''GLO2011-0015182 : Nurse Note 
        ''It is created into the function , its main purpose is to load the tool strip data after refresh also 
        ''Call is provided in LoadToolStrip in LoadWordUserControl.
        Call isRecordLocked()
    End Sub
    Private Function AddChildMenu() As DataTable
        Try
            Dim oProvider As New clsProvider
            Dim rslt As Boolean
            rslt = oProvider.CheckSignDelegateStatus()
            If rslt Then
                Dim dt As DataTable
                dt = oProvider.GetAllAssignProviders(gnLoginID)
                oProvider.Dispose()
                oProvider = Nothing
                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then

                        Return dt
                    Else
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
    Private Sub tlsDisclosureMgmt_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsDisclosureMgmt.ToolStripButtonClick
        InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
    End Sub

    Private Sub wdDisclosureMgmt_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdDisclosureMgmt.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    isHandlerRemoved = True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateVoiceLog(ex.ToString)

        End Try
    End Sub

    Private Sub wdDisclosureMgmt_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdDisclosureMgmt.OnDocumentClosed
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally  ''code change for problem 00000591
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub


    Private Sub wdDisclosureMgmt_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdDisclosureMgmt.OnDocumentOpened
        oCurDoc = wdDisclosureMgmt.ActiveDocument
        oWordApp = oCurDoc.Application
        Try
            If Not IsNothing(oCurDoc) Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                isHandlerRemoved = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
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
                    'r.SetRange(Sel.Start, Sel.End + 1)

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

                            End If
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
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

    Private Sub tlsDisclosure_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDisclosureMgmt.ToolStripClick
        Try


            ''******Shweta 20090828 *********'
            ''To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            ''End Shweta

            Select Case e.ClickedItem.Name
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in DisclosureManagement when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked------------")
                    If CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic Completed from tblButtons_ButtonClick in DisclosureManagement when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)

                Case "Save"
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        If m_IsFinished Then
                            Call SaveDisclosure(True)
                        Else
                            Call SaveDisclosure(False)
                        End If

                        If IsModify Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", m_PatientID, m_DisclosureID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", m_PatientID, m_DisclosureID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try

                Case "Save & Close"
                    Try
                        Me.Cursor = Cursors.WaitCursor

                        Call SaveDisclosure(False, True)

                        If IsModify Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Disclosure Management Modified", m_PatientID, m_DisclosureID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Disclosure Management Added", m_PatientID, m_DisclosureID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                    cmbTemplate.Enabled = False
                    dtDisclosureDate.Enabled = False

                Case "FAX"
                    Try
                        bnlIsFaxOpened = True
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        Call GeneratePrintFaxDocument(False)
                        Me.Cursor = Cursors.Default
                        cmbTemplate.Enabled = False
                        dtDisclosureDate.Enabled = False
                        bnlIsFaxOpened = False
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
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
                    TurnoffMicrophone()
                    ImportDocument(1)
                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)
                Case "Close"
                    Me.Close()
                    'Case "Prescription"
                    '    ' MsgBox("Prescription")
                    '    Call GetPrescription()

                    'Case "Orders"
                    '    ' MsgBox("Orders")
                    '    Call GetOrders()
                Case "Save & Finish"
                    Try
                        Me.Cursor = Cursors.WaitCursor

                        Call SaveDisclosure(True, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
                Case "Add Addendum"
                    Call InsertAddendum()
                Case "DisclosureSets"
                    TurnoffMicrophone()
                    OpenDisclosureSet()
                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Disclosure Management", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    ' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "SecureMsg"
                    Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_PatientID)
                    If sError <> "" Then
                        MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                        Return
                    Else
                        TurnoffMicrophone()
                        Call SendSecureMsg()
                        cmbTemplate.Enabled = False
                        dtDisclosureDate.Enabled = False
                    End If

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Disclosure Note using Secure Message", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Private Sub GenerateSecureMsgDocument()

        If Not strProviderDirectAddress.Any() AndAlso gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation Is Nothing Then

            MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        '  Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdDisclosureMgmt, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try

        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '  oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        'wdDisclosureMgmt.Close()


        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)


        'wdTemp.Open(sFileName)
        'oTempDoc = wdTemp.ActiveDocument
        'oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim osenddox As String = String.Empty

        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, m_PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        '  Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Dim oSendDoc As New clsPrintFAX
        'Dim osenddox As String
        'osenddox = oSendDoc.SendDoc(oTempDoc, m_PatientID)
        'oSendDoc.Dispose()
        'oSendDoc = Nothing
        'wdTemp.Close()
        'Me.Controls.Remove(wdTemp)
        'wdTemp.Dispose()
        'wdTemp = Nothing
        myLoadWord.CloseApplicationOnly()
        'myLoadWord.CloseWordApplication(oTempDoc)
        myLoadWord = Nothing
        oCurDoc.Saved = _SaveFlag

        gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString = GetConnectionString()
        gloSurescriptSecureMessage.SecureMessageProperties.UserID = System.Convert.ToInt64(appSettings("UserID"))
        gloSurescriptSecureMessage.SecureMessageProperties.UserName = appSettings("UserName")
        gloSurescriptSecureMessage.SecureMessageProperties.ProviderID = appSettings("ProviderID")
        gloSurescriptSecureMessage.SecureMessageProperties.IsStagingServerEnable = gblnIsSecureStagingsever
        gloSurescriptSecureMessage.SecureMessageProperties.StagingServerUrl = gstrSecureStagingUrl
        gloSurescriptSecureMessage.SecureMessageProperties.ProductionServerUrl = gstrSecureProductionUrl

        ''Read Secure Messages settings and call Inbox form
        If (osenddox.Length > 0) Then
            If File.Exists(osenddox) Then
                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientID, osenddox)
                AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromDisclosureManagement

                If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                    gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                    ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                End If

                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()
                RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromDisclosureManagement
                ofrmSendNewMail.Close()
                ofrmSendNewMail = Nothing
            Else
                MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ' LoadWordUserControl(sFileName, False)
        'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        'Set the Start postion of the cursor in documents
        'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        'oCurDoc.Saved = _SaveFlag
        Exit Sub

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If m_IsFinished = True Then
                tmrDocProtect.Enabled = True
            End If
        End Try
    End Sub


    Private Sub OpenDisclosureSet()
        ''m_DisclosureID ,        m_PatientID,
        Dim dt As New DataTable
        Dim oDisclosure As New clsDisclosureMgmt
        dt = oDisclosure.GetDisclosureSet()
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Dim frm As New frmDisclosureSet(m_DisclosureID, m_PatientID)
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.parent))
                oCurDoc.ActiveWindow.SetFocus()

                objword = New clsWordDocument
                objCriteria = New DocCriteria
                objCriteria.DocCategory = enumDocCategory.Exam
                objCriteria.PatientID = m_PatientID
                objCriteria.VisitID = dtDisclosureDate.Tag
                objCriteria.PrimaryID = m_DisclosureID
                objword.DocumentCriteria = objCriteria
                objword.CurDocument = oCurDoc
                objword.GetFormFieldData(enumDocType.DisclosureSet)
                oCurDoc = objword.CurDocument
                'wdCtrlPatientConsent.document = objword.CurDocument
                objCriteria.Dispose()
                objCriteria = Nothing
                objword = Nothing
                frm.Dispose()
                frm = Nothing
            Else
                MessageBox.Show("No Disclosure Sets are defined. Define the Disclosure Sets.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("No Disclosure Sets are defined. Define the Disclosure Sets.", "Disclosure Management", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        Try
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                Fill_TemplateGallery()
                blnCmbSelTemplate = False
            Else
                wdDisclosureMgmt.Close()
            End If
        Catch ex As Exception
            blnCmbSelTemplate = False
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property

    ''' <summary>
    ''' Trigger Voice commands
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
    ''' Add voice commands from custom collection to DgnStrings
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' Show microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
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
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
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
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.DisclosureManagement
        ogloVoice.MyWordToolStrip = Me.tlsDisclosureMgmt
        ogloVoice.MDIParentVoice = Me.MdiParent
        ogloVoice.MessageName = "Disclosure"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsDisclosure_ToolStripClick)
    End Sub
    ''' <summary>
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Disclosure", "Save")
        oHashtable.Add("Print Disclosure", "Print")
        oHashtable.Add("Fax Disclosure", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Disclosure", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Disclosure", "Close")
        oHashtable.Add("Finish Disclosure", "Save & Finish")
        Return oHashtable
    End Function

    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateBasicVoiceCmds(VoiceCol)
            End If
        End If
    End Sub
    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return Me.MdiParent
        End Get
    End Property
    Private Sub InitializeVoiceObjectForAddendum()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If


        Dim oAddendumHashtable As ArrayList = ouctlgloUC_Addendum.FillTemplateCommands(True)
        ogloVoice = New ClsVoice(oAddendumHashtable)
        ogloVoice.gloTreeView = ouctlgloUC_Addendum.trvTemplates
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.DisclosureManagement
        ogloVoice.eVoiceAddendum = VoiceAddendum.eAddendum
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = Me.MdiParent
        ogloVoice.MessageName = "Disclosure"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()
    End Sub

    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose
        pnlgloUC_Addendum.Controls.Remove(ouctlgloUC_Addendum)

        pnlToolStripContainer.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlgloUC_Addendum.Visible = False
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
        pnlToolStripContainer.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlgloUC_Addendum.Visible = False
        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub


    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID   'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub InitiliseTemplateTreeControl()
        Try
            Dim objDocriteria As New DocCriteria
            Dim objClsWord_TemplateTree As New clsWordDocument
            GloUC_TemplateTreeControl_DisclosureManagement.InitiliseControlParameter(GetConnectionString())
            GloUC_TemplateTreeControl_DisclosureManagement.DocCriteria = objDocriteria
            GloUC_TemplateTreeControl_DisclosureManagement.ObjClsWord = objClsWord_TemplateTree
            GloUC_TemplateTreeControl_DisclosureManagement.ProviderId = gnSelectedProviderID
            GloUC_TemplateTreeControl_DisclosureManagement.Fill_ExamTemplates(0)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    Public Sub CallPastData()
        Try







            Dim obj_UCclsDisclosureMgmt As clsDisclosureMgmt = New clsDisclosureMgmt
            Dim Obj_UCWord As clsWordDocument = New clsWordDocument
            Dim obj_UCCriteria As DocCriteria = New DocCriteria
            Dim clsPatientExams As clsPatientExams = New clsPatientExams

            If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                CType(GloUC_PastWordNotes1.OBJCRITERIAs, DocCriteria).Dispose()
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



            GloUC_PastWordNotes1.OBJCRITERIAs = obj_UCCriteria
            GloUC_PastWordNotes1.OBJWORDs = Obj_UCWord
            GloUC_PastWordNotes1.PATIENTIDs = m_PatientID
            GloUC_PastWordNotes1.OBJCLSDISCLOSUREs = obj_UCclsDisclosureMgmt
            GloUC_PastWordNotes1.FromForms = "DisclosureManagement"
            GloUC_PastWordNotes1.CLSPATIENTEXAMSs = clsPatientExams
            GloUC_PastWordNotes1.ShowHide_PastExam()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub Treeview_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs, sFilename As System.String) Handles GloUC_TemplateTreeControl_DisclosureManagement.Treeview_NodeMouseDoubleClick

        Dim blnRefreshDocument As Boolean

        Try

            oCurDoc = wdDisclosureMgmt.ActiveDocument
            oCurDoc.ActiveWindow.SetFocus()

            '04-Jan-2016 Aniket: Warn the user if the full document is being replaced by tags
            If gloWord.LoadAndCloseWord.ValidateTagsSelectedRange(oCurDoc) = True Then
                oCurDoc.Application.Selection.InsertFile(sFilename, "", False, False, False)
                blnRefreshDocument = True
            End If


            wdDisclosureMgmt.Select()

            If blnRefreshDocument = True Then
                GetdataFromOtherForms(enumDocType.None)
            End If


            If IO.File.Exists(sFilename) Then
                IO.File.Delete(sFilename)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()
        Try
            objword = New clsWordDocument
            objword.WaitControlPanel = Me.Panel2
            objCriteria = New DocCriteria
            objCriteria.PatientID = m_PatientID
            If (IsNothing(dtDisclosureDate) = False) Then
                objCriteria.VisitID = GenerateVisitID(dtDisclosureDate.Value, m_PatientID)
            End If
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = objword
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                End If
            Catch

            End Try
           
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = m_PatientID
            GloUC_AddRefreshDic1.ObjFrom = Me
            

            GloUC_AddRefreshDic1.DTLETTERDATEs = dtDisclosureDate
            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdDisclosureMgmt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub cmdPastExam_Click(sender As System.Object, e As System.EventArgs) Handles cmdPastExam.Click
        Try

            If cmdPastExam.Text = "Show Past Disclosure Management" Then
                pnlGloUC_PastWordNotes.Show()
                cmdPastExam.Text = "Hide Past Disclosure Management"
                bIsPastDMClick = True
            ElseIf cmdPastExam.Text = "Hide Past Disclosure Management" Then
                pnlGloUC_PastWordNotes.Hide()
                cmdPastExam.Text = "Show Past Disclosure Management"
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub
    Private Sub Dispose_Object()
        Try

            'If Not IsNothing(obj_UCclsDisclosureMgmt) Then
            '    obj_UCclsDisclosureMgmt = Nothing
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
            If Not IsNothing(objDisclosure) Then
                objDisclosure.Dispose()
                objDisclosure = Nothing
            End If
            If Not IsNothing(_PatientStrip) Then
                pnlMain.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
            If (IsNothing(GloUC_TemplateTreeControl_DisclosureManagement) = False) Then
                GloUC_TemplateTreeControl_DisclosureManagement.FinalizeControlParameter("")
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''GLO2011-0015182 : Nurse Note 
    ''It is created into the function , its main purpose is to load the tool strip data after refresh also 
    ''Call is provided in LoadToolStrip in LoadWordUserControl.
    Private Sub isRecordLocked()
        If m_IsRecordLock Then
            If tlsDisclosureMgmt.MyToolStrip.Items.ContainsKey("Save") = True Then
                tlsDisclosureMgmt.MyToolStrip.Items("Save").Enabled = False
            End If
            If tlsDisclosureMgmt.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
                tlsDisclosureMgmt.MyToolStrip.Items("Save & Close").Enabled = False
            End If

            If tlsDisclosureMgmt.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                tlsDisclosureMgmt.MyToolStrip.Items("Add Addendum").Enabled = False
            End If
            If tlsDisclosureMgmt.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                tlsDisclosureMgmt.MyToolStrip.Items("Save & Finish").Enabled = False
            End If
        End If
    End Sub

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromDisclosureManagement(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromDisclosureManagement(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromDisclosureManagement(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromDisclosureManagement(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            ex = Nothing
        End Try
    End Sub
#End Region
End Class
