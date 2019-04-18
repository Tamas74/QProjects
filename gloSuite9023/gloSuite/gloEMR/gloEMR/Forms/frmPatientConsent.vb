Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices

Public Class frmPatientConsent
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

    Public MyCaller As frmVWPatientConsent
    Private ImagePath As String
    Private m_ConsentID As Long
    Private m_TemplateID As Long
    Private m_PatientID As Long
    Private m_visitID As Long
    Public m_IsFinished As Boolean = False
    Dim objCriteria As DocCriteria
    Dim objword As clsWordDocument
    Private blnCmbSelTemplate As Boolean = False
    Friend WithEvents pnlWdConsent As System.Windows.Forms.Panel
    Friend WithEvents wdPatientConsent As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlComboMian As System.Windows.Forms.Panel
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents lblLetterDt As System.Windows.Forms.Label
    Friend WithEvents dtLetterDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSelectTem As System.Windows.Forms.Label
    Private WithEvents tlsPatientConsent As WordToolStrip.gloWordToolStrip
    '   Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Dim myidx As Int32
    Dim m_IsRecordLock As Boolean
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlCombo As System.Windows.Forms.Panel
    Public IsModify As Boolean = False
    Private blnSignClick As Boolean = False
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Friend WithEvents pnlGloUC_PastWordNotes As System.Windows.Forms.Panel
    Friend WithEvents GloUC_PastWordNotes1 As gloUserControlLibrary.gloUC_PastWordNotes
    Friend WithEvents pnl_chkShowPreview As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents pnlGloUC_TemplateTreeControl As System.Windows.Forms.Panel
    Friend WithEvents GloUC_TemplateTreeControl_PatientConcent As gloUserControlLibrary.gloUC_TemplateTreeControl
    Friend WithEvents pnl_cmdPastExam As System.Windows.Forms.Panel
    Friend WithEvents cmdPastExam As System.Windows.Forms.Button
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents pnlgloUC_Addendum As System.Windows.Forms.Panel
    Private strPatientMaritalStatus As String
    Public isExpandConsent As Boolean = False
    Private bnlIsFaxOpened As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        m_PatientID = PatientID
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable.
    'constructor is not in use.
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
    'Public Sub New(ByVal ConsentID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean)
    '    MyBase.New()

    '    m_ConsentID = ConsentID
    '    m_TemplateID = TemplateID
    '    m_IsFinished = IsFinished
    '    m_PatientID = gnPatientID
    '    m_IsRecordLock = IsRecordLock
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    'Add any initialization after the InitializeComponent() call
    'End Sub
    'end modification

    Public Sub New(ByVal ConsentID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean, ByVal nPatientID As Long)
        MyBase.New()

        m_ConsentID = ConsentID
        m_TemplateID = TemplateID
        m_IsFinished = IsFinished
        m_PatientID = nPatientID
        m_IsRecordLock = IsRecordLock
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
            Dim dtpControls As DateTimePicker() = {dtLetterDate}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
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
            Try

                If (IsNothing(GloUC_TemplateTreeControl_PatientConcent) = False) Then
                    If (IsNothing(GloUC_TemplateTreeControl_PatientConcent.DocCriteria) = False) Then
                        DirectCast(GloUC_TemplateTreeControl_PatientConcent.DocCriteria, DocCriteria).Dispose()
                        GloUC_TemplateTreeControl_PatientConcent.DocCriteria = Nothing
                    End If
                End If
            Catch

            End Try
            Try
                If (IsNothing(GloUC_PastWordNotes1) = False) Then
                    If (IsNothing(GloUC_PastWordNotes1.OBJCRITERIAs) = False) Then
                        DirectCast(GloUC_PastWordNotes1.OBJCRITERIAs, DocCriteria).Dispose()
                    End If
                End If

            Catch

            End Try
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientConsent))
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlWdConsent = New System.Windows.Forms.Panel()
        Me.wdPatientConsent = New AxDSOFramer.AxFramerControl()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnlComboMian = New System.Windows.Forms.Panel()
        Me.lblLetterDt = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.lblSelectTem = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dtLetterDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlCombo = New System.Windows.Forms.Panel()
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
        Me.GloUC_TemplateTreeControl_PatientConcent = New gloUserControlLibrary.gloUC_TemplateTreeControl()
        Me.pnl_cmdPastExam = New System.Windows.Forms.Panel()
        Me.cmdPastExam = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlgloUC_Addendum = New System.Windows.Forms.Panel()
        Me.pnlWdConsent.SuspendLayout()
        CType(Me.wdPatientConsent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlComboMian.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlCombo.SuspendLayout()
        Me.pnlGloUC_PastWordNotes.SuspendLayout()
        Me.pnl_chkShowPreview.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlGloUC_TemplateTreeControl.SuspendLayout()
        Me.pnl_cmdPastExam.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrDocProtect
        '
        '
        'pnlWdConsent
        '
        Me.pnlWdConsent.Controls.Add(Me.wdPatientConsent)
        Me.pnlWdConsent.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlWdConsent.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlWdConsent.Controls.Add(Me.lbl_pnlRight)
        Me.pnlWdConsent.Controls.Add(Me.lbl_pnlTop)
        Me.pnlWdConsent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWdConsent.Location = New System.Drawing.Point(0, 30)
        Me.pnlWdConsent.Name = "pnlWdConsent"
        Me.pnlWdConsent.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlWdConsent.Size = New System.Drawing.Size(836, 582)
        Me.pnlWdConsent.TabIndex = 3
        '
        'wdPatientConsent
        '
        Me.wdPatientConsent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientConsent.Enabled = True
        Me.wdPatientConsent.Location = New System.Drawing.Point(1, 1)
        Me.wdPatientConsent.Name = "wdPatientConsent"
        Me.wdPatientConsent.OcxState = CType(resources.GetObject("wdPatientConsent.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientConsent.Size = New System.Drawing.Size(831, 577)
        Me.wdPatientConsent.TabIndex = 5
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 578)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(831, 1)
        Me.lbl_pnlBottom.TabIndex = 9
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 578)
        Me.lbl_pnlLeft.TabIndex = 8
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(832, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 578)
        Me.lbl_pnlRight.TabIndex = 7
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(833, 1)
        Me.lbl_pnlTop.TabIndex = 6
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlComboMian
        '
        Me.pnlComboMian.BackColor = System.Drawing.Color.Transparent
        Me.pnlComboMian.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlComboMian.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlComboMian.Controls.Add(Me.lblLetterDt)
        Me.pnlComboMian.Controls.Add(Me.Panel4)
        Me.pnlComboMian.Controls.Add(Me.Panel3)
        Me.pnlComboMian.Controls.Add(Me.Label1)
        Me.pnlComboMian.Controls.Add(Me.Label2)
        Me.pnlComboMian.Controls.Add(Me.Label3)
        Me.pnlComboMian.Controls.Add(Me.Label4)
        Me.pnlComboMian.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlComboMian.Location = New System.Drawing.Point(0, 3)
        Me.pnlComboMian.Name = "pnlComboMian"
        Me.pnlComboMian.Size = New System.Drawing.Size(833, 24)
        Me.pnlComboMian.TabIndex = 4
        '
        'lblLetterDt
        '
        Me.lblLetterDt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLetterDt.AutoSize = True
        Me.lblLetterDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLetterDt.Location = New System.Drawing.Point(517, 4)
        Me.lblLetterDt.Name = "lblLetterDt"
        Me.lblLetterDt.Size = New System.Drawing.Size(99, 14)
        Me.lblLetterDt.TabIndex = 32
        Me.lblLetterDt.Text = "Consent Date :"
        Me.lblLetterDt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.Panel4.Controls.Add(Me.cmbTemplate)
        Me.Panel4.Controls.Add(Me.lblSelectTem)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(1, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(400, 22)
        Me.Panel4.TabIndex = 40
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(335, 1)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 19)
        Me.GloUC_AddRefreshDic1.TabIndex = 38
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.Location = New System.Drawing.Point(136, 0)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(194, 22)
        Me.cmbTemplate.TabIndex = 30
        '
        'lblSelectTem
        '
        Me.lblSelectTem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSelectTem.AutoSize = True
        Me.lblSelectTem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectTem.Location = New System.Drawing.Point(13, 4)
        Me.lblSelectTem.Name = "lblSelectTem"
        Me.lblSelectTem.Size = New System.Drawing.Size(120, 14)
        Me.lblSelectTem.TabIndex = 29
        Me.lblSelectTem.Text = "  Select Template :"
        Me.lblSelectTem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dtLetterDate)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(580, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(252, 22)
        Me.Panel3.TabIndex = 39
        '
        'dtLetterDate
        '
        Me.dtLetterDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtLetterDate.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtLetterDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtLetterDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtLetterDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtLetterDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtLetterDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtLetterDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtLetterDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtLetterDate.Location = New System.Drawing.Point(42, 0)
        Me.dtLetterDate.Name = "dtLetterDate"
        Me.dtLetterDate.Size = New System.Drawing.Size(183, 22)
        Me.dtLetterDate.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(831, 1)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(832, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(833, 1)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "label1"
        '
        'pnlCombo
        '
        Me.pnlCombo.Controls.Add(Me.pnlComboMian)
        Me.pnlCombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCombo.Location = New System.Drawing.Point(0, 0)
        Me.pnlCombo.Name = "pnlCombo"
        Me.pnlCombo.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlCombo.Size = New System.Drawing.Size(836, 30)
        Me.pnlCombo.TabIndex = 5
        '
        'pnlGloUC_PastWordNotes
        '
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.GloUC_PastWordNotes1)
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.pnl_chkShowPreview)
        Me.pnlGloUC_PastWordNotes.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_PastWordNotes.Location = New System.Drawing.Point(0, 0)
        Me.pnlGloUC_PastWordNotes.Name = "pnlGloUC_PastWordNotes"
        Me.pnlGloUC_PastWordNotes.Size = New System.Drawing.Size(217, 612)
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
        Me.GloUC_PastWordNotes1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_PastWordNotes1.PATIENTIDs = CType(0, Long)
        Me.GloUC_PastWordNotes1.SHOWPASTs = False
        Me.GloUC_PastWordNotes1.Size = New System.Drawing.Size(217, 585)
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
        Me.pnl_chkShowPreview.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
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
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(214, 24)
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
        Me.Label9.Text = "Past Patient Consent"
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
        'pnlGloUC_TemplateTreeControl
        '
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.GloUC_TemplateTreeControl_PatientConcent)
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.pnl_cmdPastExam)
        Me.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_TemplateTreeControl.Location = New System.Drawing.Point(220, 0)
        Me.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl"
        Me.pnlGloUC_TemplateTreeControl.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGloUC_TemplateTreeControl.Size = New System.Drawing.Size(217, 612)
        Me.pnlGloUC_TemplateTreeControl.TabIndex = 14
        '
        'GloUC_TemplateTreeControl_PatientConcent
        '
        Me.GloUC_TemplateTreeControl_PatientConcent.DocCriteria = Nothing
        Me.GloUC_TemplateTreeControl_PatientConcent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TemplateTreeControl_PatientConcent.ExpandConsent = False
        Me.GloUC_TemplateTreeControl_PatientConcent.Location = New System.Drawing.Point(0, 27)
        Me.GloUC_TemplateTreeControl_PatientConcent.Name = "GloUC_TemplateTreeControl_PatientConcent"
        Me.GloUC_TemplateTreeControl_PatientConcent.ObjClsWord = Nothing
        Me.GloUC_TemplateTreeControl_PatientConcent.ProviderId = CType(0, Long)
        Me.GloUC_TemplateTreeControl_PatientConcent.Size = New System.Drawing.Size(217, 582)
        Me.GloUC_TemplateTreeControl_PatientConcent.TabIndex = 0
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
        Me.cmdPastExam.Text = "Show Past Patient Consent"
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
        Me.Splitter1.Location = New System.Drawing.Point(217, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 612)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(437, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 612)
        Me.Splitter2.TabIndex = 16
        Me.Splitter2.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlWdConsent)
        Me.pnlMain.Controls.Add(Me.pnlgloUC_Addendum)
        Me.pnlMain.Controls.Add(Me.pnlCombo)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(440, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(836, 612)
        Me.pnlMain.TabIndex = 17
        '
        'pnlgloUC_Addendum
        '
        Me.pnlgloUC_Addendum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlgloUC_Addendum.Location = New System.Drawing.Point(0, 30)
        Me.pnlgloUC_Addendum.Name = "pnlgloUC_Addendum"
        Me.pnlgloUC_Addendum.Size = New System.Drawing.Size(836, 582)
        Me.pnlgloUC_Addendum.TabIndex = 2
        Me.pnlgloUC_Addendum.Visible = False
        '
        'frmPatientConsent
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1276, 612)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlGloUC_TemplateTreeControl)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlGloUC_PastWordNotes)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientConsent"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Consent"
        Me.pnlWdConsent.ResumeLayout(False)
        CType(Me.wdPatientConsent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlComboMian.ResumeLayout(False)
        Me.pnlComboMian.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnlCombo.ResumeLayout(False)
        Me.pnlGloUC_PastWordNotes.ResumeLayout(False)
        Me.pnl_chkShowPreview.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlGloUC_TemplateTreeControl.ResumeLayout(False)
        Me.pnl_cmdPastExam.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
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

    Dim objclsPatientConsent As New clsPatientConsent
    Private WithEvents oCurDoc As Wd.Document
    ' Private WithEvents oTempDoc As Wd.Document

    Private _Arrlist As ArrayList
    Private _strNotes As String
    Public Shared strAddendum As String
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    Private WithEvents oWordApp As Wd.Application
    Dim opnl As Panel

#Region "variable Declaration for UC treeview"
    Dim bIsPastConcentClick As Boolean = False
    'Dim obj_UCclsPatientConcent As clsPatientConsent
    'Dim Obj_UCWord As clsWordDocument
    'Dim obj_UCCriteria As DocCriteria
    'Dim clsPatientExams As clsPatientExams
#End Region
    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If

        End Try
    End Sub

    Private Sub frmPatientConsent_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdPatientConsent
            End If
        End Try
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If

        'Developer: Yatin N.Bhagat
        'Date:12/24/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
        'Reason: Handler For DDLCBEvent is Not Added while activating the form
        If Not (IsNothing(wdPatientConsent)) Then
            If Not (IsNothing(wdPatientConsent.DocumentName)) Then
                If Not (IsNothing(wdPatientConsent.ActiveDocument)) Then
                    oCurDoc = wdPatientConsent.ActiveDocument
                    oWordApp = oCurDoc.Application
                    Try
                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    Try
                        AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    isHandlerRemoved = False
                    oCurDoc.ActiveWindow.SetFocus()
                    wdPatientConsent.Focus()
                End If
            End If
        End If
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmPatientConsent_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If

        'Developer: Yatin N.Bhagat
        'Date:12/24/2011
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
        'Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmPatientConsent_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            If m_IsRecordLock = False Then
                UnLock_Transaction(TrnType.PatientConsent, m_ConsentID, 0, Now)
            End If

            '' <><><> Unlock the Record <><><>
            If IsNothing(MyCaller) = False Then
                If MyCaller.CanSelect = True Then
                    MyCaller.RefreshConsent(m_ConsentID)
                End If
            End If
            'If Not oWordApp Is Nothing Then
            '    oWordApp.RecentFiles.Maximum = 0
            '    oWordApp.DisplayRecentFiles = False
            '    Marshal.FinalReleaseComObject(oWordApp)
            'End If
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
            End If
            Dispose_Object()

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmPatientConsent_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If m_IsRecordLock Then
                Exit Sub
            End If

            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta

            If IsNothing(oCurDoc) = False Then
                If oCurDoc.Saved = False Then
                    Dim Result As DialogResult
                    Result = MessageBox.Show("Do you want to save the changes to Patient Consent?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If Result = DialogResult.Yes Then
                        If m_IsFinished Then
                            Call SavePatientConsent(True, True)
                        Else
                            Call SavePatientConsent(False, True)
                        End If
                        e.Cancel = False
                    ElseIf Result = DialogResult.Cancel Then
                        '' Nothing to here
                        e.Cancel = True
                    ElseIf Result = DialogResult.No Then
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient Consent viewed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                        e.Cancel = False
                    End If
                Else
                    e.Cancel = False
                End If
            Else
                e.Cancel = False
            End If
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


    End Sub

    Private Sub frmPatientConsent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dt As New DataTable
            Call Get_PatientDetails()
            dtLetterDate.Format = DateTimePickerFormat.Custom
            dtLetterDate.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtLetterDate.Value = Now

            objclsPatientConsent.fill_widthofExam(pnlGloUC_TemplateTreeControl) ''''added to load width of panel

            ' '' Patient Details
            'lblPatientName.Text = gstrPatientFirstName & Space(2) & gstrPatientLastName
            'lblPatientCode.Text = gstrPatientCode
            'lblPatientCode.Tag = gnPatientID
            ''
            'Dim objword As New clsWordDocument
            ''Added by Mayuri:20100517-To fix issue:#6641-Delete nurse note template = lost names of created nurse notes associated with template
            If m_ConsentID <> 0 Then
                Dim objclsNotes As New clsNurseNotes
                dt = objclsNotes.FillTemplate(enumTemplateFlag.PatientConsent, m_ConsentID, m_TemplateID).Copy()
                objclsNotes.Dispose()
                objclsNotes = Nothing
                'dt = objclsPatientConsent.FillTemplate(m_ConsentID, m_TemplateID) '' To fill Patient Consent Templates
            Else
                dt = objclsPatientConsent.FillTemplates() '' To fill Patient Consent Templates
            End If
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
            If m_ConsentID <> 0 Then
                '' for Update 
                Dim dtLetter As DataTable
                dtLetter = objclsPatientConsent.ScanPatientConsent(m_ConsentID)
                Fill_PatientConsent(dtLetter)
            Else
                '' for New Consent fill by default one Consent Template  
                If cmbTemplate.SelectedValue > 0 Then
                    Call Fill_TemplateGallery()
                    'If IsNothing(_Arrlist) = False Then
                    '    Call Fill_Restrictions()
                Else
                    wdPatientConsent.CreateNew("Word.Application")

                End If
            End If
            If IsModify Then
                cmbTemplate.Enabled = False
                dtLetterDate.Enabled = False
            End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()
                ShowMicroPhone()

            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

                wdPatientConsent.Close()
            End If
        Else
            wdPatientConsent.Close()
        End If

    End Sub

    Private Sub Fill_TemplateGallery()
        Dim strFileName As String

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
                'Set the Start postion of the cursor in documents
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
            Else
                wdPatientConsent.Close()

            End If
        Else
            wdPatientConsent.Close()
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
        objCriteria.VisitID = dtLetterDate.Tag
        objCriteria.PrimaryID = 0
        objword.DocumentCriteria = objCriteria
        objword.CurDocument = oCurDoc
        objword.WaitControlPanel = Me.pnlWdConsent
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
        If Trim(strPatientFirstName) <> "" Then
            'Dim frmOrders As New frmVWOrders
            Dim frmOrders As frm_LM_Orders

            frmOrders = frm_LM_Orders.GetInstance(dtLetterDate.Tag, dtLetterDate.Value, m_PatientID, 0, False)
            If IsNothing(frmOrders) = True Then
                Exit Sub
            End If
            With frmOrders
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Maximized
                '' .myCaller = Me
                ''.blnOpenFromExam = True
                ''.BringToFront()
                'COMMNETED BY SHUBHANGI
                '.ShowDialog(Me)
                .myCaller1 = Me
                .Show()
                ' GetdataFromOtherForms(enumDocType.RadiologyOrders)
            End With
        Else
            MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        'Else
        'MessageBox.Show("Please select the Visit", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End If
    End Sub

    Private Sub tmrDocProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDocProtect.Tick
        Try
            tmrDocProtect.Enabled = False
            If m_IsFinished = True And blnIsAddendum = False Then
                If Not oCurDoc Is Nothing Then
                    'Bug #85105: 00000876: Document in procted mode while faxing
                    ' oCurDoc.ActiveWindow.SetFocus()
                    Dim protectPane As Wd.TaskPane = oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                    If (IsNothing(protectPane) = False) Then
                        protectPane.Visible = False
                        Marshal.ReleaseComObject(protectPane)
                        protectPane = Nothing
                    End If
                    ' oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrDocProtect.Enabled = True
        End Try
    End Sub

    ''To load dynamically Patient Details and form specific Controls
    Private Sub loadPatientStrip()
        If Not IsNothing(_PatientStrip) Then
            pnlMain.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.PatientConsent)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(0, 0, 0, 0)
        _PatientStrip.BringToFront()
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                'Dim oEDocument As New gloEDocument.gloEDocumentManagement()
                'gloEDocument.gloEDocumentAdmin.Connect(GetConnectionString(), gDMSV2TempPath, gDMSScanDocumentView, Convert.ToInt32(gnLoginID), gClinicID, Application.StartupPath, DMSRootPath)
                'oEDocument.ShowEScannerForImages(m_PatientID, oFiles)
                'oEDocument.Dispose()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010

                'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'oEDocument.ShowEScannerForImages(gnPatientID, oFiles)
                oEDocument.ShowEScannerForImages(m_PatientID, oFiles)
                'end modification
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Public Sub GetPrescription()        
        Try
            dtLetterDate.Tag = GenerateVisitID(dtLetterDate.Value, m_PatientID)
        Catch ex As Exception

        End Try

        Dim frmRxMeds As frmPrescription
        frmRxMeds = frmPrescription.GetInstance(0, CType(m_PatientID, Long))
        If IsNothing(frmRxMeds) = True Then
            Exit Sub
        End If
        
        If frmPrescription.IsOpen = False Then
            frmRxMeds.ShowMedication()
            If frmRxMeds.blncancel = True Then
                With frmRxMeds
                    .WindowState = FormWindowState.Maximized
                    .myCallerC = Me
                    .blnOpenFromPatientConsent = True                    
                    .ShowDialog(IIf(IsNothing(frmRxMeds.Parent), Me, frmRxMeds.Parent))                    
                End With
            End If
        Else
            MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If        
    End Sub

    Public Sub InsertSignature()
        Try
            ImagePath = ""

            Dim frm As New FrmSignature
            frm.ShowInTaskbar = False
            frm.Owner = Me
            'frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification
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
                ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Co-Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                    'oCurDoc.Application.Selection.Move(1)
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Patient Consent", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        'Try
        '    ''Integrated by Mayuri:20101021-Provider sign
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
        '    ''End 20101021
        '    Dim objWord As New clsWordDocument
        '    Dim objCriteria As DocCriteria
        '    objCriteria = New DocCriteria
        '    objCriteria.DocCategory = enumDocCategory.Others
        '    'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        '    'objCriteria.PatientID = gnPatientID
        '    objCriteria.PatientID = m_PatientID
        '    'end modification

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
        '        oCurDoc.Application.Selection.TypeParagraph()
        '        '' By Mahesh Signature With Date - 20070113
        '        '''' Add Date Time When Signature is Inserted
        '        ''Added on 20101008 by snajog for signature
        '        Dim clsExam As New clsPatientExams
        '        Dim strProviderName As String
        '        If ProviderID <> 0 Then
        '            strProviderName = clsExam.GetProvidernameforExam(ProviderID)
        '        Else
        '            strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
        '        End If
        '        ''Added on 20101008 by snajog for signature


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
        '        ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
        '        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
        '        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
        '        ''Added Rahul P on 20101011
        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        '        ''
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    Private Sub tlsPatientConsent_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsPatientConsent.ToolStripButtonClick
        If IsNothing(oCurDoc) = False Then
            InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
        End If
    End Sub
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE


    Private Sub FaxPatientConsent(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientConsent, m_PatientID, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

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
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Deleting Patient Consent Page Header", gloAuditTrail.ActivityOutCome.Success)
        '    'UpdateLog("Deleting Patient Consent Page Header")
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader

        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            'UpdateLog("Patient Consent Page Header deleted")
        '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.DeleteBookMark, gloAuditTrail.ActivityType.Fax, "Patient Consent Page Header deleted", gloAuditTrail.ActivityOutCome.Success)
        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Error Deleting Patient Consent Page Header - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '        'UpdateVoiceLog("Error Deleting Patient Consent Page Header - " & ex.ToString)
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
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        '  wdTemp.Activate()
        ' wdTemp.Select()
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    Private Function SavePatientConsent(ByVal IsFinished As Boolean, Optional ByVal IsClose As Boolean = False) As Boolean
        Try
            If oCurDoc Is Nothing Then
                Return False
            End If
            SavePatientConsent = False


            If pnlGloUC_TemplateTreeControl.Width.ToString <> "" Then
                objclsPatientConsent.SaveWidthInDatabase(gnLoginID, pnlGloUC_TemplateTreeControl.Width) ''''save width of panel in DB
            End If

            '' IF IsFinished = True Then Remove the Unused Fields 
            If IsFinished Then
                '  gloWord.LoadAndCloseWord.LockFields(oCurDoc)

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
            'If (IsNothing(oCurDoc) = False) Then
            '    oCurDoc.Save()
            'Else
            'wdPatientConsent.Save()
            'gloWord.LoadAndCloseWord.SaveDSO(wdPatientConsent, oCurDoc, oWordApp)
            ''End If

            'Dim strFileName As String
            'Dim isExceptionWhileCopingFile As Boolean = False
            'strFileName = ExamNewDocumentName
            '' wdPatientConsent.Save(strFileName, True, "", "")
            'Try
            '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)
            'Catch ex As Exception
            '    'UpdateLog("ERROR WHILE COPING FILE IN MESSAGE :" & ex.ToString())
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            '    isExceptionWhileCopingFile = True
            'End Try
            'If (isExceptionWhileCopingFile) Then
            '    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '    wdPatientConsent.Close()
            'End If
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPatientConsent, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If

            m_ConsentID = objclsPatientConsent.SavePatientConsentBytes(m_ConsentID, m_PatientID, cmbTemplate.SelectedValue, Format(dtLetterDate.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, cmbTemplate.Text, IsFinished)

            If m_ConsentID > 0 Then
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
                    'sarika 20081201 template name editable after save
                    cmbTemplate.Enabled = False
                    dtLetterDate.Enabled = False
                    '--
                    'Set the Start postion of the cursor in documents
                    ' oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
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
                    If Not IsNothing(oCurDoc) Then
                        oCurDoc.Saved = False
                    End If
                End If

            End If
            'If IO.File.Exists(strFileName) Then
            '    Try
            '        IO.File.Delete(strFileName)
            '    Catch ex As Exception
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        ex = Nothing
            '    End Try
            'End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function

    Private Sub Print()

        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()

            ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Consent Printed", gstrLoginName, gstrClientMachineName, gnPatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Patient Consent Printed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Patient Consent Printed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        End If

    End Sub

    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        Try
            Dim PageNo As Integer = 0
            Dim totalPages As Integer = 0
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            Dim Missing As Object = System.Reflection.Missing.Value

            Dim _SaveFlag As Boolean = False
            If IsNothing(oCurDoc) Then
                Exit Sub
            End If
            If oCurDoc.Saved Then
                _SaveFlag = True
            End If
            '  Dim sFileName As String = ExamNewDocumentName

            'Ashish added on 31st October 
            'to prevent screen from refreshing
            'Dim wordRefresh As New WordRefresh()
            'Dim WDocViewType As Wd.WdViewType
            'If IsPrintFlag Then
            '    'Ashish added on 31st October 
            '    'to prevent screen from refreshing
            '    'WDocViewType = oCurDoc.ActiveWindow.View.Type                
            'End If

            'wdCtrlPatientConsent.SaveDocument(sFileName)
            ' wdPatientConsent.Save(sFileName, True, "", "")
            If IsNothing(wdPatientConsent) = False AndAlso IsNothing(oWordApp) = False Then
                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdPatientConsent, oCurDoc, oWordApp)
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
                'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)
                Try
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FaxPatientConsent, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                'If IsPrintFlag Then
                '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '    '    oTempDoc.Unprotect()
                '    'End If
                '    Dim oPrint As New clsPrintFAX
                '    oPrint.PrintDoc(oTempDoc, m_PatientID)
                '    oPrint.Dispose()
                '    oPrint = Nothing
                'Else
                '    Call FaxPatientConsent(myLoadWord, oTempDoc)
                '    ''gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Consent Fax", gstrLoginName, gstrClientMachineName, gnPatientID)
                '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Fax, "Patient Consent Fax", gloAuditTrail.ActivityOutCome.Success)
                '    ''Added Rahul P on 20101011
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Fax, "Patient Consent Fax", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                '    ''
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
                ''Set the Start postion of the cursor in documents
                'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)


                'If IsPrintFlag Then
                '    'Ashish added on 31st October 
                '    'to prevent screen from refreshing
                '    'WordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
                '    'WDocViewType = Nothing
                'End If

                'wordRefresh.Dispose()
                'wordRefresh = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Fax, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    Private Sub frmPatientConsent_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try

            'Developer:Yatin N. Bhagat
            'Date:12/13/2011
            'Bug ID/PRD Name/Salesforce Case:GLO2011-0015304 Check boxes not consistently working in patient exam module
            'Reason: Invalid Code

            'Me.Hide()
            'Call MyCaller.RefreshConsent(gnPatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            oCurDoc = Nothing
            'wdCtrlPatientConsent.CloseControl()
            wdPatientConsent.Close()
        End Try
    End Sub

    Private Sub InsertAddendum()
        Try
            If m_IsFinished = True Then
                'Dim f As New frmMsg_Addendum(6)
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

                opnl.Visible = False

                pnlCombo.Visible = True
                ouctlgloUC_Addendum = New gloUC_Addendum(0, m_ConsentID, m_PatientID)
                blnIsAddendum = True
                pnlgloUC_Addendum.Controls.Add(ouctlgloUC_Addendum)

                GloUC_TemplateTreeControl_PatientConcent.Visible = False
                pnlGloUC_PastWordNotes.Visible = False
                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                ouctlgloUC_Addendum.BringToFront()
                pnlgloUC_Addendum.Visible = True
                pnlgloUC_Addendum.BringToFront()
                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'oCurDoc1.Application.Selection.InsertRows(1)

    End Sub

    Public Sub Navigate1(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsPatientConsent.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPatientConsent.MyToolStrip.Items("Mic").Visible = True
                        tlsPatientConsent.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        tlsPatientConsent.MyToolStrip.ButtonsToHide.Remove(tlsPatientConsent.MyToolStrip.Items("Mic").Name)
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
                If gblnVoiceEnabled = True And gblnSpeakerExists = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsPatientConsent.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPatientConsent.MyToolStrip.Items("Mic").Visible = True
                        tlsPatientConsent.MyToolStrip.ButtonsToHide.Remove(tlsPatientConsent.MyToolStrip.Items("Mic").Name)
                    End If



                ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic") Is Nothing Then
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Visible = True
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
                    End If

                    Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.ButtonsToHide.Remove(Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Name)
                    Exit Sub
                Else

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsPatientConsent.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPatientConsent.MyToolStrip.Items("Mic").Visible = False
                        If tlsPatientConsent.MyToolStrip.ButtonsToHide.Contains(tlsPatientConsent.MyToolStrip.Items("Mic").Name) = False Then
                            tlsPatientConsent.MyToolStrip.ButtonsToHide.Add(tlsPatientConsent.MyToolStrip.Items("Mic").Name)
                        End If
                    End If

                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsPatientConsent.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsPatientConsent.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
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
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        If Not blnCmbSelTemplate Then
            loadToolStrip()
        End If
        '   wdPatientConsent.Open(strFileName)
        'Dim oWordApp As Wd.Application = Nothing
        gloWord.LoadAndCloseWord.OpenDSO(wdPatientConsent, strFileName, oCurDoc, oWordApp)

        If blnGetData Then

            ''//To retrieve the Form fields for the Word document
            objword = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification

            ''If gnVisitID = 0 Then '' condition commented by Sandip Darade for the the flow to not to use incorrect visit id 
            'code commented and modified by dipak to remove referance of gnVisitID
            'gnVisitID = GenerateVisitID(m_PatientID)
            ' ''End If
            'objCriteria.VisitID = gnVisitID

            'm_visitID = GenerateVisitID(m_PatientID)
            'Bug #73113: 00000498 : Liquid Link
            'while resolveing this issue we identified the issue in patient consent
            Try
                dtLetterDate.Tag = GenerateVisitID(dtLetterDate.Value, m_PatientID)
                m_visitID = dtLetterDate.Tag
            Catch ex As Exception

            End Try
          

            ''End If
            objCriteria.VisitID = m_visitID
            'end changes by dipak
            objCriteria.PrimaryID = 0
            objword.DocumentCriteria = objCriteria
            objword.CurDocument = oCurDoc

            ''Replace Form fields with Concerned data
            'objword.DisableWordRefresh = True
            'objword.GetFormFieldDataDBCalls(enumDocType.None)
            objword.GetFormFieldData(enumDocType.None)
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
            Try
                dtLetterDate.Tag = GenerateVisitID(dtLetterDate.Value, m_PatientID) ''added for bugid 87031 liquid data cleared out
            Catch ex As Exception

            End Try

        End If

        SetWordObjectEntry(m_IsFinished)

    End Sub
    ' ''To re-initialize the word User Control if any Word disaster occurs
    'Private Sub ReInitUserControl()

    '    If Not (wdCtrlPatientConsent Is Nothing) Then
    '        'If Not wdCtrlPatientEducation.wd Is Nothing Then
    '        '    Marshal.ReleaseComObject(wdCtrlPatientEducation.wd)
    '        '    wdCtrlNewExam.wd = Nothing
    '        ' End If

    '        wdCtrlPatientConsent = Nothing
    '        Me.Panel1.Controls.Remove(wdCtrlPatientConsent)
    '    End If
    '    wdCtrlPatientConsent = New WordCtrl.WordControl
    '    Me.Panel1.Controls.Add(wdCtrlPatientConsent)
    '    wdCtrlPatientConsent.Dock = DockStyle.Fill
    '    ''// Add Event Handler for ToolStrip Item click
    '    AddHandler wdCtrlPatientConsent.ToolStripClick, AddressOf wdCtrlPatientConsent_ToolStripClick

    'End Sub

    Private Sub loadToolStrip()
        If Not tlsPatientConsent Is Nothing Then
            tlsPatientConsent.Dispose()
        End If
        If Not IsNothing(opnl) Then
            Me.Controls.Remove(opnl)
            opnl.Dispose()
        End If
        'add wordtoolstrip in panel <<<<<<<<<<<<<<<Ojeswini_17Feb2009>>>>>>>>>>>>>>>>>>>
        opnl = New Panel
        Me.Controls.Add(opnl)
        opnl.Dock = DockStyle.Top
        opnl.Size = New System.Drawing.Size(940, 56)
        opnl.SendToBack()

        tlsPatientConsent = New WordToolStrip.gloWordToolStrip
        tlsPatientConsent.Dock = DockStyle.Top
        tlsPatientConsent.ConnectionString = GetConnectionString()
        tlsPatientConsent.UserID = gnLoginID
        tlsPatientConsent.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsPatientConsent.ptProvider = oclsProvider.GetPatientProviderName(m_PatientID)
        tlsPatientConsent.ptProviderId = oclsProvider.GetPatientProvider(m_PatientID)

        oclsProvider.Dispose()
        oclsProvider = Nothing

        tlsPatientConsent.BringToFront()

        If m_IsFinished Then

            tlsPatientConsent.FormType = WordToolStrip.enumControlType.Addendum
            cmbTemplate.Enabled = False
            dtLetterDate.Enabled = False
        Else
            tlsPatientConsent.IsCoSignEnabled = gblnCoSignFlag
            tlsPatientConsent.FormType = WordToolStrip.enumControlType.PatientConsent
            cmbTemplate.Enabled = True
            dtLetterDate.Enabled = True
        End If


        opnl.Controls.Add(tlsPatientConsent)
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsPatientConsent
                ShowMicroPhone()
            End If
        End If
        'If gblnAssociatedProviderSignature And m_IsFinished = False And m_IsRecordLock = False Then
        '    tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
        '    tlsPatientConsent.MyToolStrip.ButtonsToHide.Remove(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        'Else
        '    tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
        '    If (tlsPatientConsent.MyToolStrip.ButtonsToHide.Contains(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
        '        tlsPatientConsent.MyToolStrip.ButtonsToHide.Add(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        '    End If

        'End If
        If gblnAssociatedProviderSignature And m_IsFinished = False Then
            tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsPatientConsent.MyToolStrip.ButtonsToHide.Remove(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            ''commented by Mayuri:20101018-This condition is checked in glotoolstrip control
            ''If user has authority to insert provider sign then only it will able to insert
            'tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            'If dt.Rows.Count > 0 Then
            '    tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = True
            'Else
            '    tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = False
            'End If
            'tlsTriage.MyToolStrip.ButtonsToHide.Remove(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        ElseIf m_IsFinished = True Then

            tlsPatientConsent.MyToolStrip.Items("Save").Visible = False
            If (tlsPatientConsent.MyToolStrip.ButtonsToHide.Contains(tlsPatientConsent.MyToolStrip.Items("Save").Name) = False) Then
                tlsPatientConsent.MyToolStrip.ButtonsToHide.Add(tlsPatientConsent.MyToolStrip.Items("Save").Name)
            End If
            tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPatientConsent.MyToolStrip.ButtonsToHide.Contains(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPatientConsent.MyToolStrip.ButtonsToHide.Add(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        ElseIf gblnAssociatedProviderSignature = False Then
            tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPatientConsent.MyToolStrip.ButtonsToHide.Contains(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPatientConsent.MyToolStrip.ButtonsToHide.Add(tlsPatientConsent.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If


        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsPatientConsent.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsPatientConsent.MyToolStrip.ButtonsToHide.Contains(tlsPatientConsent.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsPatientConsent.MyToolStrip.ButtonsToHide.Add(tlsPatientConsent.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsPatientConsent.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsPatientConsent.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
        ''GLO2011-0015182 : Nurse Note 
        ''It is created into the function , its main purpose is to load the tool strip data after refresh also 
        ''Call is provided in LoadToolStrip in LoadWordUserControl.
        isRecordLocked()
    End Sub

    Private Sub wdPatientConsent_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientConsent.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    isHandlerRemoved = True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                    
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientConsent_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientConsent.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    ' Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If


        Catch ex As Exception
            'UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'GC.Collect()  ''move from try to finally slr problem 00000591
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub wdPatientConsent_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientConsent.OnDocumentOpened
        oCurDoc = wdPatientConsent.ActiveDocument
        oWordApp = oCurDoc.Application

        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                    '   r.SetRange(Sel.Start, Sel.End + 1)

                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then

                        '  Dim om As Object = System.Reflection.Missing.Value

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

                        '        Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, True), oOffice.CommandBarComboBox)

                        '        oDD.Style = oOffice.MsoComboStyle.msoComboLabel

                        '        oDD.DropDownLines = dd.ListEntries.Count

                        '        Dim le As Wd.ListEntry
                        '        For Each le In dd.ListEntries

                        '            oDD.AddItem(le.Name, om)

                        '        Next

                        '        oDD.ListIndex = iCurSel

                        '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                        '        dd.Value = oDD.ListIndex

                        '    Else

                        '        myidx = dd.Value

                        '        Dim iter As Integer = 1

                        '        Dim le As Wd.ListEntry
                        '        For Each le In dd.ListEntries

                        '            Dim btn As oOffice.CommandBarButton
                        '            '     Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                        '            btn = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                        '            btn.Style = oOffice.MsoButtonStyle.msoButtonAutomatic

                        '            btn.Caption = le.Name

                        '            btn.Enabled = True

                        '            If iter = myidx Then

                        '                btn.State = oOffice.MsoButtonState.msoButtonDown
                        '            End If

                        '            iter = iter + 1

                        '            ' btn.Click += New Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btn_Click)
                        '            AddHandler btn.Click, AddressOf btn_Click
                        '        Next

                        '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

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
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    Private Sub tlsPatientConsent_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPatientConsent.ToolStripClick
        Try

            ''******Shweta 20090828 *********'
            ''To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            ''End Shweta

            Select Case e.ClickedItem.Name
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in PatientConsent when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
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
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic Completed from tblButtons_ButtonClick in PatientConsent when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                Case "Save"
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        If m_IsFinished Then
                            Call SavePatientConsent(True)
                        Else
                            Call SavePatientConsent(False)
                        End If

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                Case "Save & Close"
                    Try
                        Me.Cursor = Cursors.WaitCursor

                        Call SavePatientConsent(False, True)
                        Me.Close()
                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    cmbTemplate.Enabled = False
                    dtLetterDate.Enabled = False
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
                    TurnoffMicrophone()
                    ImportDocument(1)
                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)
                Case "Close"
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

                        Call SavePatientConsent(True, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                Case "Add Addendum"
                    Call InsertAddendum()
                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()

                Case "Export"
                    '' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient Consent", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    '' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Patient Consent using Secure Message", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdPatientConsent, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try
        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        'Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        ' oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                ' wdPatientConsent.Close()

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        ' Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

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
                ofrmSendNewMail.Close()
                'ofrmInbox.Dispose()
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If m_IsFinished = True Then
                tmrDocProtect.Enabled = True
            End If

        End Try
    End Sub
    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        'Dim WDocViewType As Wd.WdViewType
        'Dim wordOptimizer As New WordRefresh()

        Try
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                'Ashish added on 1st October 
                'to prevent screen from refreshing

                'WDocViewType = oCurDoc.ActiveWindow.View.Type
                'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)
                'wordOptimizer.ShowPanel(Me.pnlWdConsent)

                Fill_TemplateGallery()
                blnCmbSelTemplate = False
            Else
                wdPatientConsent.Close()
            End If
        Catch ex As Exception
            blnCmbSelTemplate = False
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'Ashish added on 3rd November
            'to prevent screen from refreshing
            'wordOptimizer.HidePanel(Me.pnlWdConsent)
            'wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
            'wordOptimizer.Dispose()
            'wordOptimizer = Nothing

            'WDocViewType = Nothing
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
    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateBasicVoiceCmds(VoiceCol)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Activate Voice commands
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
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.PatientConsent
        ogloVoice.MyWordToolStrip = Me.tlsPatientConsent
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "PatientConsent"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsPatientConsent_ToolStripClick)
    End Sub
    ''' <summary>
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save PatientConsent", "Save")
        oHashtable.Add("Print PatientConsent", "Print")
        oHashtable.Add("Fax PatientConsent", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close PatientConsent", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close PatientConsent", "Close")
        oHashtable.Add("Finish PatientConsent", "Save & Finish")
        oHashtable.Add("Prescription", "Prescription")

        Return oHashtable
    End Function
    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
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
        ogloVoice.eVoiceAddendum = VoiceAddendum.eAddendum
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.PatientConsent
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "PatientConsent"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()
    End Sub

    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose

        pnlgloUC_Addendum.Controls.Remove(ouctlgloUC_Addendum)
        opnl.Visible = True
        pnlgloUC_Addendum.Visible = False
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlGloUC_PastWordNotes.Visible = True
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
        opnl.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlGloUC_PastWordNotes.Visible = True
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
            Return m_PatientID    'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub InitiliseTemplateTreeControl()
        Try
            Dim objDocriteria As New DocCriteria
            Dim objClsWord_TemplateTree As New clsWordDocument
            GloUC_TemplateTreeControl_PatientConcent.InitiliseControlParameter(GetConnectionString())
            GloUC_TemplateTreeControl_PatientConcent.DocCriteria = objDocriteria
            GloUC_TemplateTreeControl_PatientConcent.ObjClsWord = objClsWord_TemplateTree
            GloUC_TemplateTreeControl_PatientConcent.ProviderId = gnSelectedProviderID
            GloUC_TemplateTreeControl_PatientConcent.ExpandConsent = isExpandConsent
            GloUC_TemplateTreeControl_PatientConcent.Fill_ExamTemplates(0)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    Public Sub CallPastData()
        Try
            Dim obj_UCclsPatientConcent As clsPatientConsent = New clsPatientConsent
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
            GloUC_PastWordNotes1.OBJCLSPATIENTCONSENTs = obj_UCclsPatientConcent
            GloUC_PastWordNotes1.FromForms = "PatientConsent"
            GloUC_PastWordNotes1.CLSPATIENTEXAMSs = clsPatientExams
            GloUC_PastWordNotes1.ShowHide_PastExam()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub Treeview_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs, ByVal sFilename As System.String) Handles GloUC_TemplateTreeControl_PatientConcent.Treeview_NodeMouseDoubleClick
        Dim blnRefreshDocument As Boolean

        Try
            oCurDoc = wdPatientConsent.ActiveDocument
            '     oCurDoc.Application.ScreenUpdating = False
            oCurDoc.ActiveWindow.SetFocus()

            '04-Jan-2016 Aniket: Warn the user if the full document is being replaced by tags
            If gloWord.LoadAndCloseWord.ValidateTagsSelectedRange(oCurDoc) = True Then
                oCurDoc.Application.Selection.InsertFile(sFilename, "", False, False, False)
                blnRefreshDocument = True
            End If

            wdPatientConsent.Select()
            If blnRefreshDocument = True Then
                GetdataFromOtherForms(enumDocType.None)
            End If

            '  oCurDoc.Application.ScreenUpdating = True
            If IO.File.Exists(sFilename) Then
                IO.File.Delete(sFilename)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()
        Try
            objword = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientID
            objCriteria.VisitID = dtLetterDate.Tag
            objCriteria.PrimaryID = 0
            objword.DocumentCriteria = objCriteria
            objword.WaitControlPanel = Me.pnlWdConsent
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = objword
            Try
                If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                    CType(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                End If
            Catch

            End Try
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = m_PatientID
            GloUC_AddRefreshDic1.ObjFrom = Me
            GloUC_AddRefreshDic1.DTLETTERDATEs = dtLetterDate
            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdPatientConsent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub cmdPastExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPastExam.Click
        Try

            If cmdPastExam.Text = "Show Past Patient Consent" Then
                pnlGloUC_PastWordNotes.Show()
                cmdPastExam.Text = "Hide Past Patient Consent"
                bIsPastConcentClick = True
            ElseIf cmdPastExam.Text = "Hide Past Patient Consent" Then
                pnlGloUC_PastWordNotes.Hide()
                cmdPastExam.Text = "Show Past Patient Consent"
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub
    Private Sub Dispose_Object()
        Try

            'If Not IsNothing(obj_UCclsPatientConcent) Then
            '    obj_UCclsPatientConcent = Nothing
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
            If Not IsNothing(_PatientStrip) Then
                pnlMain.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
            If (IsNothing(GloUC_TemplateTreeControl_PatientConcent) = False) Then
                GloUC_TemplateTreeControl_PatientConcent.FinalizeControlParameter("")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''GLO2011-0015182 : Nurse Note 
    ''It is created into the function , its main purpose is to load the tool strip data after refresh also 
    ''Call is provided in LoadToolStrip in LoadWordUserControl.
    Private Sub isRecordLocked()
        If m_IsRecordLock Then
            If tlsPatientConsent.MyToolStrip.Items.ContainsKey("Save") = True Then
                tlsPatientConsent.MyToolStrip.Items("Save").Enabled = False
            End If
            If tlsPatientConsent.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
                tlsPatientConsent.MyToolStrip.Items("Save & Close").Enabled = False
            End If
            If tlsPatientConsent.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                tlsPatientConsent.MyToolStrip.Items("Save & Finish").Enabled = False
            End If
            If tlsPatientConsent.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                tlsPatientConsent.MyToolStrip.Items("Add Addendum").Enabled = False
            End If
        End If
    End Sub
End Class
