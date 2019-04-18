Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices
Public Class frmPTProtocols

    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements IHotKey
    Implements gloVoice
    Implements IPatientContext



    'Instantiate voice class
    Private ogloVoice As ClsVoice

    'Addendum user control defined here 
    Private blnIsAddendum As Boolean
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    'Private m_VisitID As Long
    Public MyCaller As frmVWPTProtocols

    Private _ProtocolID As Long
    Private _TemplateID As Long
    Public _IsFinished As Boolean = False
    Private _PatientID As Long

    Public CancelClick As Boolean
    Private Imagepath As String
    Dim nVisitId As Long

    Dim objclsPTProtocols As New clsPTProtocols
    Private WithEvents oCurDoc As Wd.Document
    '  Private WithEvents oTempDoc As Wd.Document
    Private blnCmbSelTemplate As Boolean = False
    Private Arrlist As ArrayList

    Private WithEvents oWordApp As Wd.Application

    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Private WithEvents tlsPTProtocol As WordToolStrip.gloWordToolStrip
    Friend WithEvents pnlwdPTProtocols As System.Windows.Forms.Panel
    Friend WithEvents wdPTProtocols As AxDSOFramer.AxFramerControl
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Public Shared strAddendum As String
    Dim ObjWord As clsWordDocument
    Dim objCriteria As DocCriteria
    Dim myidx As Int32
    Dim _IsRecordLock As Boolean = False
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents pnlcombo As System.Windows.Forms.Panel
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public Ismodify As Boolean = False
    Private blnSignClick As Boolean = False
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Friend WithEvents pnlGloUC_PastWordNotes As System.Windows.Forms.Panel
    Friend WithEvents GloUC_PastWordNotes1 As gloUserControlLibrary.gloUC_PastWordNotes
    Friend WithEvents pnl_chkShowPreview As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlGloUC_TemplateTreeControl As System.Windows.Forms.Panel
    Friend WithEvents GloUC_TemplateTreeControl_PTProtocols As gloUserControlLibrary.gloUC_TemplateTreeControl
    Friend WithEvents pnl_cmdPastExam As System.Windows.Forms.Panel
    Friend WithEvents cmdPastExam As System.Windows.Forms.Button
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private strPatientMaritalStatus As String
    Private bnlIsFaxOpened As Boolean

#Region "variable Declaration for UC treeview"
    Dim bIsPastPTprotocolClick As Boolean = False
    'Dim obj_UCclsPTProtocols As clsPTProtocols
    'Dim Obj_UCWord As clsWordDocument
    'Dim obj_UCCriteria As DocCriteria
    Friend WithEvents pnlgloUC_Addendum As System.Windows.Forms.Panel
    'Dim clsPatientExams As clsPatientExams
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'm_hotKeys = New HotKeyCollection(Me)

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'line commented as we are not using gn patient id
        '   _PatientID = gnPatientID
        _PatientID = PatientID
        'Add any initialization after the InitializeComponent() call

    End Sub
    'constructor commented as not in use for removing reffrances of 
    'Public Sub New(ByVal PTProtocolID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean)
    '    MyBase.New()
    '    'm_hotKeys = New HotKeyCollection(Me)

    '    _ProtocolID = PTProtocolID
    '    _TemplateID = TemplateID
    '    _IsFinished = IsFinished
    '    _PatientID = gnPatientID
    '    _IsRecordLock = IsRecordLock
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub
    Public Sub New(ByVal PTProtocolID As Long, ByVal TemplateID As Long, ByVal IsFinished As Boolean, ByVal IsRecordLock As Boolean, ByVal nPatientID As Long)
        MyBase.New()
        'm_hotKeys = New HotKeyCollection(Me)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        _ProtocolID = PTProtocolID
        _TemplateID = TemplateID
        _IsFinished = IsFinished
        _PatientID = nPatientID
        _IsRecordLock = IsRecordLock

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
            If (IsNothing(objclsPTProtocols) = False) Then
                objclsPTProtocols.Dispose()
                objclsPTProtocols = Nothing
            End If
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If
            Try
                If (IsNothing(GloUC_TemplateTreeControl_PTProtocols) = False) Then
                    If (IsNothing(GloUC_TemplateTreeControl_PTProtocols.DocCriteria) = False) Then
                        DirectCast(GloUC_TemplateTreeControl_PTProtocols.DocCriteria, DocCriteria).Dispose()
                        GloUC_TemplateTreeControl_PTProtocols.DocCriteria = Nothing
                    End If
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
            Try
                If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                    If (IsNothing(GloUC_AddRefreshDic1) = False) Then
                        DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()
                        GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
                    End If
                End If

            Catch

            End Try
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim dtpControls As DateTimePicker() = {dtPTProtocol}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
            Catch ex As Exception

            End Try
            
           
           
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlcomboHeader As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtPTProtocol As System.Windows.Forms.DateTimePicker
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPTProtocols))
        Me.pnlcomboHeader = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtPTProtocol = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlwdPTProtocols = New System.Windows.Forms.Panel()
        Me.wdPTProtocols = New AxDSOFramer.AxFramerControl()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlcombo = New System.Windows.Forms.Panel()
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
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlGloUC_TemplateTreeControl = New System.Windows.Forms.Panel()
        Me.GloUC_TemplateTreeControl_PTProtocols = New gloUserControlLibrary.gloUC_TemplateTreeControl()
        Me.pnl_cmdPastExam = New System.Windows.Forms.Panel()
        Me.cmdPastExam = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlgloUC_Addendum = New System.Windows.Forms.Panel()
        Me.pnlcomboHeader.SuspendLayout()
        Me.pnlwdPTProtocols.SuspendLayout()
        CType(Me.wdPTProtocols, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlcombo.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlGloUC_PastWordNotes.SuspendLayout()
        Me.pnl_chkShowPreview.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlGloUC_TemplateTreeControl.SuspendLayout()
        Me.pnl_cmdPastExam.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlcomboHeader
        '
        Me.pnlcomboHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlcomboHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlcomboHeader.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.pnlcomboHeader.Controls.Add(Me.Label2)
        Me.pnlcomboHeader.Controls.Add(Me.dtPTProtocol)
        Me.pnlcomboHeader.Controls.Add(Me.Label8)
        Me.pnlcomboHeader.Controls.Add(Me.cmbTemplate)
        Me.pnlcomboHeader.Controls.Add(Me.Label1)
        Me.pnlcomboHeader.Controls.Add(Me.Label3)
        Me.pnlcomboHeader.Controls.Add(Me.Label4)
        Me.pnlcomboHeader.Controls.Add(Me.Label5)
        Me.pnlcomboHeader.Controls.Add(Me.Label6)
        Me.pnlcomboHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlcomboHeader.Location = New System.Drawing.Point(0, 3)
        Me.pnlcomboHeader.Name = "pnlcomboHeader"
        Me.pnlcomboHeader.Size = New System.Drawing.Size(546, 24)
        Me.pnlcomboHeader.TabIndex = 0
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(339, 2)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 20)
        Me.GloUC_AddRefreshDic1.TabIndex = 39
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(235, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label2.Size = New System.Drawing.Size(104, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Protocol Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtPTProtocol
        '
        Me.dtPTProtocol.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtPTProtocol.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtPTProtocol.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtPTProtocol.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtPTProtocol.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtPTProtocol.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtPTProtocol.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPTProtocol.Location = New System.Drawing.Point(339, 1)
        Me.dtPTProtocol.Name = "dtPTProtocol"
        Me.dtPTProtocol.Size = New System.Drawing.Size(178, 22)
        Me.dtPTProtocol.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(517, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label8.Size = New System.Drawing.Size(28, 22)
        Me.Label8.TabIndex = 9
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.DropDownWidth = 600
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.Location = New System.Drawing.Point(117, 1)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(213, 22)
        Me.cmbTemplate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(116, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Template :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(544, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(545, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 23)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(546, 1)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "label1"
        '
        'tmrDocProtect
        '
        '
        'pnlwdPTProtocols
        '
        Me.pnlwdPTProtocols.BackColor = System.Drawing.Color.Transparent
        Me.pnlwdPTProtocols.Controls.Add(Me.wdPTProtocols)
        Me.pnlwdPTProtocols.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlwdPTProtocols.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlwdPTProtocols.Controls.Add(Me.lbl_RightBrd)
        Me.pnlwdPTProtocols.Controls.Add(Me.lbl_TopBrd)
        Me.pnlwdPTProtocols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlwdPTProtocols.Location = New System.Drawing.Point(0, 30)
        Me.pnlwdPTProtocols.Name = "pnlwdPTProtocols"
        Me.pnlwdPTProtocols.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlwdPTProtocols.Size = New System.Drawing.Size(549, 548)
        Me.pnlwdPTProtocols.TabIndex = 2
        '
        'wdPTProtocols
        '
        Me.wdPTProtocols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPTProtocols.Enabled = True
        Me.wdPTProtocols.Location = New System.Drawing.Point(1, 1)
        Me.wdPTProtocols.Name = "wdPTProtocols"
        Me.wdPTProtocols.OcxState = CType(resources.GetObject("wdPTProtocols.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPTProtocols.Size = New System.Drawing.Size(544, 543)
        Me.wdPTProtocols.TabIndex = 6
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 544)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(544, 1)
        Me.lbl_BottomBrd.TabIndex = 10
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 544)
        Me.lbl_LeftBrd.TabIndex = 9
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(545, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 544)
        Me.lbl_RightBrd.TabIndex = 8
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(546, 1)
        Me.lbl_TopBrd.TabIndex = 7
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlcombo
        '
        Me.pnlcombo.Controls.Add(Me.pnlcomboHeader)
        Me.pnlcombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlcombo.Location = New System.Drawing.Point(0, 0)
        Me.pnlcombo.Name = "pnlcombo"
        Me.pnlcombo.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlcombo.Size = New System.Drawing.Size(549, 30)
        Me.pnlcombo.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.Label7)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(989, 30)
        Me.pnlToolStrip.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label7.Size = New System.Drawing.Size(86, 20)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "pnlToolStrip"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label7.Visible = False
        '
        'pnlGloUC_PastWordNotes
        '
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.GloUC_PastWordNotes1)
        Me.pnlGloUC_PastWordNotes.Controls.Add(Me.pnl_chkShowPreview)
        Me.pnlGloUC_PastWordNotes.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_PastWordNotes.Location = New System.Drawing.Point(0, 30)
        Me.pnlGloUC_PastWordNotes.Name = "pnlGloUC_PastWordNotes"
        Me.pnlGloUC_PastWordNotes.Size = New System.Drawing.Size(217, 578)
        Me.pnlGloUC_PastWordNotes.TabIndex = 14
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
        Me.Label9.Size = New System.Drawing.Size(162, 22)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Past PTProtocols"
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
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(217, 30)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 578)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'pnlGloUC_TemplateTreeControl
        '
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.GloUC_TemplateTreeControl_PTProtocols)
        Me.pnlGloUC_TemplateTreeControl.Controls.Add(Me.pnl_cmdPastExam)
        Me.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGloUC_TemplateTreeControl.Location = New System.Drawing.Point(220, 30)
        Me.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl"
        Me.pnlGloUC_TemplateTreeControl.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGloUC_TemplateTreeControl.Size = New System.Drawing.Size(217, 578)
        Me.pnlGloUC_TemplateTreeControl.TabIndex = 16
        '
        'GloUC_TemplateTreeControl_PTProtocols
        '
        Me.GloUC_TemplateTreeControl_PTProtocols.DocCriteria = Nothing
        Me.GloUC_TemplateTreeControl_PTProtocols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TemplateTreeControl_PTProtocols.ExpandConsent = False
        Me.GloUC_TemplateTreeControl_PTProtocols.Location = New System.Drawing.Point(0, 27)
        Me.GloUC_TemplateTreeControl_PTProtocols.Name = "GloUC_TemplateTreeControl_PTProtocols"
        Me.GloUC_TemplateTreeControl_PTProtocols.ObjClsWord = Nothing
        Me.GloUC_TemplateTreeControl_PTProtocols.ProviderId = CType(0, Long)
        Me.GloUC_TemplateTreeControl_PTProtocols.Size = New System.Drawing.Size(217, 548)
        Me.GloUC_TemplateTreeControl_PTProtocols.TabIndex = 0
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
        Me.cmdPastExam.Text = "Show Past PTProtocols"
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
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(437, 30)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 578)
        Me.Splitter2.TabIndex = 17
        Me.Splitter2.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlwdPTProtocols)
        Me.pnlMain.Controls.Add(Me.pnlcombo)
        Me.pnlMain.Controls.Add(Me.pnlgloUC_Addendum)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(440, 30)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(549, 578)
        Me.pnlMain.TabIndex = 18
        '
        'pnlgloUC_Addendum
        '
        Me.pnlgloUC_Addendum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlgloUC_Addendum.Location = New System.Drawing.Point(0, 0)
        Me.pnlgloUC_Addendum.Name = "pnlgloUC_Addendum"
        Me.pnlgloUC_Addendum.Size = New System.Drawing.Size(549, 578)
        Me.pnlgloUC_Addendum.TabIndex = 3
        Me.pnlgloUC_Addendum.Visible = False
        '
        'frmPTProtocols
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(989, 608)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlGloUC_TemplateTreeControl)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlGloUC_PastWordNotes)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPTProtocols"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PT Protocols"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlcomboHeader.ResumeLayout(False)
        Me.pnlcomboHeader.PerformLayout()
        Me.pnlwdPTProtocols.ResumeLayout(False)
        CType(Me.wdPTProtocols, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlcombo.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.pnlGloUC_PastWordNotes.ResumeLayout(False)
        Me.pnl_chkShowPreview.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlGloUC_TemplateTreeControl.ResumeLayout(False)
        Me.pnl_cmdPastExam.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'code commented by supriya on 13/11/2006
    ''Private m_hotKeys As HotKeyCollection

    ''Public Event HotKeyPressed As HotKeyPressedEventHandler
    ''Public Event PrintWindowPressed As PrintWindowPressedEventHandler
    ''Public Event PrintDesktopPressed As PrintDesktopPressedEventHandler

    ''Public ReadOnly Property HotKeys() As HotKeyCollection
    ''    Get
    ''        HotKeys = m_hotKeys
    ''    End Get
    ''End Property

    ''Public Sub RestoreAndActivate()
    ''    If Not (UnmanagedMethods.IsWindowVisible(Me.Handle)) Then
    ''        UnmanagedMethods.ShowWindow(Me.Handle, UnmanagedMethods.SW_SHOW)
    ''    End If
    ''    If (UnmanagedMethods.IsIconic(Me.Handle)) Then
    ''        UnmanagedMethods.SendMessage(Me.Handle, UnmanagedMethods.WM_SYSCOMMAND, _
    ''            UnmanagedMethods.SC_RESTORE, IntPtr.Zero)
    ''    End If
    ''    UnmanagedMethods.SetForegroundWindow(Me.Handle)
    ''End Sub
    'code commented by supriya on 13/11/2006

    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        ' HotKeys.Clear()
        MyBase.OnClosed(e)
    End Sub

    Private Sub frmPTProtocols_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Try
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If


            'Developer: Yatin N.Bhagat
            'Date:12/26/2011
            'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
            'Reason: Handler For DDLCBEvent is Not Added while activating the form
            If Not (IsNothing(wdPTProtocols)) Then
                If Not (IsNothing(wdPTProtocols.DocumentName)) Then
                    If Not (IsNothing(wdPTProtocols.ActiveDocument)) Then
                        oCurDoc = wdPTProtocols.ActiveDocument
                        oWordApp = oCurDoc.Application
                        Try
                            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                        Try
                            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        isHandlerRemoved = False
                        oCurDoc.ActiveWindow.SetFocus()
                        wdPTProtocols.Focus()
                    End If
                End If
            End If

            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdPTProtocols
            End If
        End Try
    End Sub

    Private Sub frmPTProtocols_CursorChanged(sender As Object, e As System.EventArgs) Handles Me.CursorChanged

    End Sub

    Private Sub frmPTProtocols_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
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
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        '[ Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmPTProtocols_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            If _IsRecordLock = False Then
                UnLock_Transaction(TrnType.PTProtocol, _ProtocolID, 0, Now)
            End If

            '' <><><> Unlock the Record <><><>
            'If Not oWordApp Is Nothing Then
            '    oWordApp.RecentFiles.Maximum = 0
            '    oWordApp.DisplayRecentFiles = False
            '    Marshal.FinalReleaseComObject(oWordApp)
            'End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                If Not IsNothing(ogloVoice) Then
                    TurnoffMicrophone()
                    ogloVoice.UnInitializeVoiceComponents()
                End If
            End If
            Try
                If (IsNothing(Me.ParentForm) = False) Then
                    CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
                End If
            Catch ex2 As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Close, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex2 = Nothing
            End Try
          
            Dispose_Object()

            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''--------------

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
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

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
    End Sub

    Private Sub frmPTProtocols_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''Get The Patient Details


            Dim dt As DataTable

            dtPTProtocol.Format = DateTimePickerFormat.Custom
            dtPTProtocol.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtPTProtocol.Value = Now
            Call Get_PatientDetails()
            ' '' Patient Details
            'lblPatientName.Text = gstrPatientFirstName & Space(2) & gstrPatientLastName
            'lblPatientCode.Text = gstrPatientCode
            'lblPatientCode.Tag = gnPatientID
            ''


            objclsPTProtocols.fill_widthofExam(pnlGloUC_TemplateTreeControl) ''''added to load width of panel

            Dim objword As New clsWordDocument

            If _ProtocolID <> 0 Then
                ''Added by Mayuri:20100517-To fix issue:#6641-Delete nurse note template = lost names of created nurse notes associated with template
                Dim objclsNotes As New clsNurseNotes
                dt = objclsNotes.FillTemplate(enumTemplateFlag.PTProtocol, _ProtocolID, _TemplateID).Copy()
                objclsNotes.Dispose()
                objclsNotes = Nothing

            Else
                dt = objword.FillTemplates(enumTemplateFlag.PTProtocol)
            End If

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    cmbTemplate.DataSource = dt
                    cmbTemplate.DisplayMember = dt.Columns(1).ColumnName
                    cmbTemplate.ValueMember = dt.Columns(0).ColumnName
                    cmbTemplate.SelectedIndex = 0
                End If
            End If
            loadPatientStrip()
            If _ProtocolID <> 0 Then
                '' for Update 
                Dim dtProtocol As DataTable
                dtProtocol = objclsPTProtocols.ScanPTProtocol(_ProtocolID)
                Fill_Protocol(dtProtocol)
            Else
                '' for New Letter fill by default one letter Template  
                If cmbTemplate.SelectedValue > 0 Then
                    Fill_TemplateGallery()
                Else
                    wdPTProtocols.CreateNew("Word.Document")
                End If
            End If

            '''''--------------------
            'AddHandler Me.HotKeyPressed, AddressOf hotKey_Pressed

            '' set the hotkey:
            ''Dim htk As HotKey = New HotKey("My HotKey", Keys.Up, HotKey.HotKeyModifiers.MOD_CONTROL Or HotKey.HotKeyModifiers.MOD_SHIFT)
            ''Dim htk As HotKey = New HotKey("My HotKey", Keys.F2, HotKey.HotKeyModifiers.MOD_SHIFT)
            ''Dim htk As HotKey = New HotKey("F4",) ', HotKey.HotKeyModifiers.MOD_CONTROL + HotKey.HotKeyModifiers.MOD_SHIFT)

            ''This is a modified logic to add Hotkey for exam form - changed on 09/18/2006  by Pravin

            ''''' Microphone Turn on / Turn off
            ''Dim htk As HotKey = New HotKey("My HotKey", Keys.F2, HotKey.HotKeyModifiers.MOD_NONE)
            ''Me.HotKeys.Add(htk)
            '' Navigation
            'Dim htk1 As HotKey = New HotKey("Navigate", Keys.F5, HotKey.HotKeyModifiers.MOD_NONE)
            'Me.HotKeys.Add(htk1)
            ''HPI
            'Dim htk2 As HotKey = New HotKey("HPI", Keys.F6, HotKey.HotKeyModifiers.MOD_NONE)
            'Me.HotKeys.Add(htk2)
            ''X-ray
            'Dim htk3 As HotKey = New HotKey("Xray", Keys.F7, HotKey.HotKeyModifiers.MOD_NONE)
            'Me.HotKeys.Add(htk3)
            ''MRI
            'Dim htk4 As HotKey = New HotKey("MRI", Keys.F8, HotKey.HotKeyModifiers.MOD_NONE)
            'Me.HotKeys.Add(htk4)
            ''PLAN
            'Dim htk5 As HotKey = New HotKey("PLAN", Keys.F9, HotKey.HotKeyModifiers.MOD_NONE)
            'Me.HotKeys.Add(htk5)
            '''''--------------------
            If Ismodify = True Then
                dtPTProtocol.Enabled = False
                cmbTemplate.Enabled = False
            End If
            If _IsRecordLock Then
                tlsPTProtocol.MyToolStrip.Items("Save").Enabled = False

                If tlsPTProtocol.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                    tlsPTProtocol.MyToolStrip.Items("Add Addendum").Enabled = False
                End If

                If tlsPTProtocol.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
                    tlsPTProtocol.MyToolStrip.Items("Save & Close").Enabled = False
                End If

                If tlsPTProtocol.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                    tlsPTProtocol.MyToolStrip.Items("Save & Finish").Enabled = False
                End If

            End If

            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            pnlGloUC_PastWordNotes.Hide()
            InitiliseTemplateTreeControl()
            CallPastData()
            calltoAddRefreshButtonControl()

            If _IsRecordLock Or _IsFinished Then
                GloUC_AddRefreshDic1.Visible = False
                pnlGloUC_TemplateTreeControl.Hide()
                pnlGloUC_PastWordNotes.Show()
                Splitter1.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    'code commented by supriya on 13/11/2006
    'Private Sub hotKey_Pressed(ByVal sender As System.Object, ByVal e As HotKeyPressedEventArgs)
    '    ' ensure form is shown:
    '    Me.RestoreAndActivate()

    '    'show a messagebox:
    '    'code commented by supriya
    '    'MessageBox.Show(Me, _
    '    '          String.Format("HotKey Pressed:" + vbCrLf + "Name: {0} " + vbCrLf + "KeyCode: {1}" + vbCrLf + "Modifiers: {2}", _
    '    '              e.HotKey.Name, _
    '    '              e.HotKey.KeyCode.ToString(), _
    '    '              e.HotKey.Modifiers.ToString()), _
    '    '                  ".NET Hot Key Demonstration", _
    '    '                  MessageBoxButtons.OK, _
    '    '                  MessageBoxIcon.Information)
    '    'code commented by supriya

    '    'code commented as chnages in mic on/off logic
    '    ' If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '    'Dim frm As MainMenu
    '    'frm = Me.MdiParent
    '    Try
    '        Select Case CType(e.HotKey, HotKey).KeyCode.ToString
    '            Case "F5" ' Navigation
    '                Navigate("[]")
    '            Case "F6"
    '                Navigate("[HPI]")
    '            Case "F7"
    '                Navigate("[Xray]")
    '            Case "F8"
    '                Navigate("[MRI]")
    '            Case "F9"
    '                Navigate("[PLAN]")
    '            Case "F2"
    '                'UpdateLog("------------------------Mic Switched On/Off started at hotkey press----------------------")
    '                'If frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
    '                '    frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
    '                'ElseIf frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
    '                '    frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
    '                'End If
    '                'UpdateLog("------------------------Mic Switched On/Off completed at hotkey press----------------------")

    '        End Select

    '    Catch ex As Exception

    '    End Try
    'End Sub

    ' Navigation code added on 20061108 by Mahesh Copied from Patient Exam
    'code commented by supriya on 13/11/2006

    'code commented by supriya on 13/11/2006
    'Private Sub Navigate(ByVal strFindString As String)
    '    oCurDoc.Application.Selection.Find.ClearFormatting()
    '    With oCurDoc.Application.Selection.Find
    '        .Text = strFindString
    '        .Replacement.Text = " "
    '        .Forward = True
    '        .Wrap = Wd.WdFindWrap.wdFindContinue
    '        .Format = False
    '        .MatchCase = False
    '        .MatchWholeWord = False
    '        .MatchWildcards = False
    '        .MatchSoundsLike = False
    '        .MatchAllWordForms = False
    '    End With
    '    oCurDoc.Application.Selection.Find.Execute()

    'End Sub
    'code commented by supriya on 13/11/2006

    '''' for exiting Letters

    Private Sub Fill_Protocol(ByVal dt As DataTable)
        ''dtLetter(0) = Letter Date
        ''dtLetter(1) = PatientLetter (Object)

        If Not IsNothing(dt) Then

            cmbTemplate.SelectedValue = _TemplateID

            If dt.Rows.Count > 0 Then

                dtPTProtocol.Value = Format(dt.Rows(0)(0), "MM/dd/yyyy hh:mm tt")

                ObjWord = New clsWordDocument
                Dim strFileName As String
                strFileName = ExamNewDocumentName
                strFileName = ObjWord.GenerateFile(dt.Rows(0)(1), strFileName)
                ObjWord = Nothing
                LoadWordUserControl(strFileName, False)
                oCurDoc.Saved = True
                'Set the Start postion of the cursor in documents
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)

            Else
                wdPTProtocols.Close()
            End If
        End If
    End Sub

    Private Sub Fill_TemplateGallery()
        Try


            Dim strFileName As String
            Try
                dtPTProtocol.Tag = GenerateVisitID(dtPTProtocol.Value, _PatientID)
            Catch ex As Exception

            End Try

            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = cmbTemplate.SelectedValue

            ObjWord.DocumentCriteria = objCriteria
            ''//Retrieving the Referrals from DB and Save it as Physical File
            strFileName = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            If (IsNothing(strFileName) = False) Then

                If strFileName <> "" Then
                    LoadWordUserControl(strFileName, True)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                End If

            End If
        Catch ex As Exception
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub SetWordObjectEntry(ByVal IsFinished As Boolean)
        Try
            oCurDoc = wdPTProtocols.ActiveDocument
        Catch ex As Exception

        End Try
        If (IsNothing(oCurDoc)) Then
            Return
        End If
        oWordApp = oCurDoc.Application

        If IsFinished = True Then
            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If
            'oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)

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
            If _IsFinished = True And blnIsAddendum = False Then
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrDocProtect.Enabled = True
        End Try
    End Sub

    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.PTProtocol)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(0, 0, 0, 0)
        _PatientStrip.SendToBack()
        'pnlcombo.BringToFront()
        'pnlwdPTProtocols.BringToFront()
        pnlMain.Controls.Add(_PatientStrip)
        If _IsFinished = True Then
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Undo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Redo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        'Set focus to Wd object
        oCurDoc.ActiveWindow.SetFocus()
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
                'End of code added by Rahul Patel on 26-10-2010.

                'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.General, "Deleting PT Protocols Page Header", gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally

        End Try
    End Sub

    ''Private Sub tblPatientLetter1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tblPatientLetter1.ButtonClick
    ''    Try
    ''        Select Case e.Button.Tag
    ''            Case "Print"
    ''                oCurDoc.ActiveWindow.PrintOut()
    ''            Case "Fax"
    ''                Try
    ''                    Me.Cursor = Cursors.WaitCursor
    ''                    Call FaxOrder()
    ''                    Me.Cursor = Cursors.Default
    ''                Catch ex As Exception
    ''                    Me.Cursor = Cursors.Default
    ''                End Try
    ''            Case "Save"
    ''                Call SavePatientLetter()
    ''                Me.Close()
    ''            Case "Close"
    ''                oCurDoc = Nothing
    ''                WdPatientLetter.Close()
    ''                Me.Close()
    ''        End Select
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message, "Patient Letters", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub

    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType)
        oCurDoc.ActiveWindow.SetFocus()
        ObjWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Others
        objCriteria.PatientID = _PatientID
        ''objCriteria.VisitID = dtPTProtocol.Tag 'nVisitId
        If (IsNothing(dtPTProtocol) = False) Then
            If IsNothing(dtPTProtocol.Tag) Then ''condition added for bugid 87061  
                objCriteria.VisitID = GenerateVisitID(dtPTProtocol.Value, _PatientID)
            Else
                objCriteria.VisitID = dtPTProtocol.Tag
            End If
        End If
      
        objCriteria.PrimaryID = 0
        ObjWord.DocumentCriteria = objCriteria
        ObjWord.CurDocument = oCurDoc
        ObjWord.GetFormFieldData(_DocType)
        oCurDoc = ObjWord.CurDocument
        objCriteria.Dispose()
        objCriteria = Nothing
        ObjWord = Nothing


    End Sub

    Private Function SaveProtocol(ByVal IsFinished As Boolean, Optional ByVal IsClose As Boolean = False) As Boolean
        Try
            If oCurDoc Is Nothing Then
                Return False
            End If
            SaveProtocol = False


            If pnlGloUC_TemplateTreeControl.Width.ToString <> "" Then
                objclsPTProtocols.SaveWidthInDatabase(gnLoginID, pnlGloUC_TemplateTreeControl.Width) ''''save width of panel in DB
            End If


            '' IF IsFinished = True Then Remove the Unused Fields 
            If IsFinished = True Then

                'gloWord.LoadAndCloseWord.LockFields(oCurDoc)

                If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    oCurDoc.Application.ActiveDocument.Unprotect()
                End If
                'Dim objword As New clsWordDocument
                'objword.CurDocument = oCurDoc
                'objword.CleanupDoc()
                'oCurDoc = objword.CurDocument
                'objword = Nothing
                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                gloWord.LoadAndCloseWord.LockFields(oCurDoc)
            End If

            ''Dim strTempFileName1 As String = Application.StartupPath & "\Temp\Temp12.doc"
            'Dim strFileName As String
            'strFileName = ExamNewDocumentName
            '' wdPTProtocols.Save(strFileName, True, "", "")
            'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            'wdPTProtocols.Close()
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPTProtocols, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If
            _ProtocolID = objclsPTProtocols.SavePTProtocolBytes(_ProtocolID, _PatientID, cmbTemplate.SelectedValue, Format(dtPTProtocol.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, cmbTemplate.Text, IsFinished, cmbTemplate.Text)

            If _ProtocolID > 0 Then
                If IsClose Then

                    If (IsNothing(oCurDoc) = False) Then
                        Try
                            Marshal.ReleaseComObject(oCurDoc)
                        Catch ex As Exception


                        End Try
                        oCurDoc = Nothing

                    End If
                Else
                    ' LoadWordUserControl(strFileName, False)
                    '--
                    dtPTProtocol.Enabled = False
                    cmbTemplate.Enabled = False
                    '-- sarika 20081201 template name editable after save
                    'Set the Start postion of the cursor in documents
                    'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
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
                    If (IsNothing(oCurDoc) = False) Then
                        oCurDoc.Saved = False
                    End If
                    'oCurDoc.Saved = False
                End If

            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function

    'Private Sub tblPTProtocols_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Tag
    '            Case "Print"
    '                Call Print()
    '            Case "Fax"
    '                Try
    '                    Me.Cursor = Cursors.WaitCursor
    '                    Call FaxPTProtocol()
    '                    Me.Cursor = Cursors.Default
    '                Catch ex As Exception
    '                    Me.Cursor = Cursors.Default
    '                End Try

    '            Case "Save"
    '                Try
    '                    Me.Cursor = Cursors.WaitCursor

    '                    Call SaveProtocol(False)

    '                    Me.Cursor = Cursors.Default
    '                Catch ex As Exception
    '                    Me.Cursor = Cursors.Default
    '                End Try

    '            Case "Save&Close"
    '                Try
    '                    Me.Cursor = Cursors.WaitCursor

    '                    Call SaveProtocol(False, True)
    '                    Me.Close()

    '                    Me.Cursor = Cursors.Default
    '                Catch ex As Exception
    '                    Me.Cursor = Cursors.Default
    '                End Try

    '            Case "Finish"
    '                Try
    '                    Me.Cursor = Cursors.WaitCursor

    '                    Call SaveProtocol(True, True)
    '                    Me.Close()

    '                    Me.Cursor = Cursors.Default
    '                Catch ex As Exception
    '                    Me.Cursor = Cursors.Default
    '                End Try

    '            Case "Close"
    '                Me.Close()

    '                '''' New
    '            Case "Undo"
    '                Call Undo()

    '            Case "Redo"
    '                Call Redo()

    '            Case "Provider Signature"
    '                Call InsertProviderSignature()

    '            Case "Capture Signature"
    '                Call InsertSignature()
    '            Case "Prescription"
    '                ' MsgBox("Prescription")
    '                Call GetPrescription()

    '            Case "Orders"
    '                ' MsgBox("Orders")
    '                Call GetOrders()

    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PTProtocol", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub




    Private Sub Print()

        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "PT Protocol Printed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "PT Protocol Printed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "PT Protocol Printed", gstrLoginName, gstrClientMachineName, gnPatientID)
        End If

    End Sub


    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send PT Protocol using Secure Message", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Public Sub GetPrescription()

        '' VisitID 
        Try
            dtPTProtocol.Tag = GenerateVisitID(dtPTProtocol.Value, _PatientID)
        Catch ex As Exception

        End Try

        Dim frmRxMeds As frmPrescription
        frmRxMeds = frmPrescription.GetInstance(0, CType(_PatientID, Long))
        If IsNothing(frmRxMeds) = True Then
            Exit Sub
        End If

        If frmPrescription.IsOpen = False Then
            frmRxMeds.ShowMedication()

            If frmRxMeds.blncancel = True Then
                With frmRxMeds
                    .WindowState = FormWindowState.Maximized
                    .myCallerPT = Me
                    .blnOpenFromPTProtocol = True
                    .ShowDialog(IIf(IsNothing(frmRxMeds.Parent), Me, frmRxMeds.Parent))
                    .Dispose()
                End With
            End If
        Else
            MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Public Sub GetOrders()
        '' VisitID 
        Try
            dtPTProtocol.Tag = GenerateVisitID(dtPTProtocol.Value, _PatientID)
        Catch ex As Exception

        End Try


        'If mgnVisitID <> 0 Then
        If Trim(strPatientFirstName) <> "" Then
            'Dim frmOrders As New frmVWOrders
            'Dim frmOrders As New frmVWOrders(dtPTProtocol.Tag, dtPTProtocol.Value)
            Dim frmOrders As frm_LM_Orders
            frmOrders = frm_LM_Orders.GetInstance(dtPTProtocol.Tag, dtPTProtocol.Value, _PatientID, 0, False)

            If IsNothing(frmOrders) Then
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

    Public Sub InsertCoSignature()
        Try
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = _PatientID
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
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Load, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, "Co-Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
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
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from PTProtocols", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
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
        '        blnResult = oclsProvider.CheckpatientProviderStatus(_PatientID, ProviderID)
        '        If blnSignClick = False Then
        '            If blnResult Then
        '                ''Selected Provider Is Exam Provider
        '            Else
        '                Pat_Provider = oclsProvider.GetPatientProviderName(_PatientID)
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

        '            _ProviderID = oclsProvider.GetPatientProvider(_PatientID)
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
        '                        Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(_PatientID)
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
        '    objCriteria.PatientID = _PatientID
        '    objCriteria.VisitID = nVisitId
        '    objCriteria.PrimaryID = 0
        '    objCriteria.ProviderID = ProviderID
        '    objWord.DocumentCriteria = objCriteria
        '    ''// Get the path of the image of Provider Signature
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
        '        ''''' Add Date Time When Signature is Inserted
        '        ''Added on 20101008 by snajog for signature
        '        Dim clsExam As New clsPatientExams
        '        Dim strProviderName As String
        '        If ProviderID <> 0 Then
        '            strProviderName = clsExam.GetProvidernameforExam(ProviderID)
        '        Else
        '            strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
        '        End If



        '        ' ''Added on 20101008 by snajog for signature
        '        'oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")

        '        'Developer: Yatin N.Bhagat
        '        'Date:01/20/2012
        '        'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
        '        'Reason: If Condition is added to check the Setting
        '        If oclsProvider.AddUserNameInProviderSignature() Then
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
        '        Else
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")) '& " (" & gstrLoginName & ")"
        '        End If


        '        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Load, "Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
        '        ''Added Rahul P on 20101011
        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Load, "Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        '        ''
        '        '''''
        '        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
        '    End If

        'Catch objErr As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            ' dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
            Dim objWord As New clsWordDocument
            Dim oclsProvider As New clsProvider
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, _PatientID, nVisitId, blnSignClick)
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
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try

    End Sub

    Private Sub frmPTProtocol_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            Me.Hide()
            If (IsNothing(MyCaller) = False) Then
                Call MyCaller.RefreshPTProtocols(_ProtocolID)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            oCurDoc = Nothing
            Try
                wdPTProtocols.Close()
            Catch ex As Exception

            End Try

        End Try
    End Sub

    Private Sub frmPTProtocol_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Do you want to save the changes to PT Protocol?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Result = Windows.Forms.DialogResult.Yes Then
                    If _IsFinished Then
                        Call SaveProtocol(True, True)
                    Else
                        Call SaveProtocol(False, True)
                    End If
                    e.Cancel = False
                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    '' Nothing to here
                    e.Cancel = True
                ElseIf Result = Windows.Forms.DialogResult.No Then
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "PTProtocol Viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    e.Cancel = False
                End If
            Else
                e.Cancel = False
            End If
        Else
            e.Cancel = False
        End If
        'Try
        '    If IsNothing(oCurDoc) = False Then
        '        If oCurDoc.Saved = False And _IsFinished = False Then
        '            Dim Result As Int16

        '            Result = MessageBox.Show("Do you want to save the changes in this Protocol?", "PTProtocol", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        '            If Result = DialogResult.Yes Then

        '                If SaveProtocol(False, True) = False Then
        '                    e.Cancel = True
        '                End If

        '            ElseIf Result = DialogResult.Cancel Then
        '                'Do nothing
        '                e.Cancel = True
        '                Exit Sub
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub FaxPTProtocol(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PTProtocols, _PatientID, "", "", cmbTemplate.Text, 0, 0, 0, True, Me) = False Then
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
        '    'UpdateLog("Deleting PTProtocols Page Header")
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Deleting PT Protocols Page Header", gloAuditTrail.ActivityOutCome.Success)
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader
        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            'UpdateLog("PTProtocols Page Header deleted")
        '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Deleting PT Protocols Page Header", gloAuditTrail.ActivityOutCome.Success)
        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Error Deleting PT Protocols Page Header - ", gloAuditTrail.ActivityOutCome.Failure)
        '        'UpdateVoiceLog("Error Deleting PTProtocols Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try
        'End If
        'End Commenting

        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, _PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.Text, clsPrintFAX.enmFAXType.PTProtocols) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        ' oTempDoc = Nothing
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    Public Sub InsertSignature()
        Try
            Imagepath = ""

            Dim frm As New FrmSignature
            frm.ShowInTaskbar = False
            frm.Owner = Me

            '20-May-14 Aniket: Resolving Bug #68711:
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()

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
            '        ''''' Add Date Time When Signature is Inserted
            '        oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
            '        '''''

            '    End If
            'End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub
    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            Imagepath = Value
        End Set
    End Property

    Public Sub Navigate1(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsPTProtocol.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPTProtocol.MyToolStrip.Items("Mic").Visible = True
                        tlsPTProtocol.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        tlsPTProtocol.MyToolStrip.ButtonsToHide.Remove(tlsPTProtocol.MyToolStrip.Items("Mic").Name)
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
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsPTProtocol.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPTProtocol.MyToolStrip.Items("Mic").Visible = True
                        tlsPTProtocol.MyToolStrip.ButtonsToHide.Remove(tlsPTProtocol.MyToolStrip.Items("Mic").Name)
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
                    If Not tlsPTProtocol.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsPTProtocol.MyToolStrip.Items("Mic").Visible = False
                        If tlsPTProtocol.MyToolStrip.ButtonsToHide.Contains(tlsPTProtocol.MyToolStrip.Items("Mic").Name) = False Then
                            tlsPTProtocol.MyToolStrip.ButtonsToHide.Add(tlsPTProtocol.MyToolStrip.Items("Mic").Name)
                        End If
                    End If


                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsPTProtocol.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsPTProtocol.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
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
                        Catch ex2 As Exception
                            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex2 = Nothing
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
                                    ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub InsertAddendum()
        Try
            If _IsFinished = True Then
                'Dim f As New frmMsg_Addendum(4)
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

                Me.SuspendLayout()
                ouctlgloUC_Addendum = New gloUC_Addendum(0, _TemplateID, _PatientID)
                blnIsAddendum = True
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Visible = False
                End If

                pnlcombo.Visible = True
                pnlgloUC_Addendum.Controls.Add(ouctlgloUC_Addendum)
                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                pnlgloUC_Addendum.Visible = True
                pnlgloUC_Addendum.BringToFront()
                pnlToolStrip.Visible = False
                pnlGloUC_PastWordNotes.Visible = False
                Me.ResumeLayout()

                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If
            End If
        Catch objErr As Exception
            Me.ResumeLayout()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.InsertFile, gloAuditTrail.ActivityType.Add, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try

        'oCurDoc1.Application.Selection.InsertRows(1)

    End Sub

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
            '   Dim sFileName As String = ExamNewDocumentName

            '  wdPTProtocols.Save(sFileName, True, "", "")
            'Dim wordRefresh As New WordRefresh()
            'Dim WDocViewType As Wd.WdViewType

            'If IsPrintFlag Then
            '    'Ashish added on 1st November 
            '    'to prevent screen from refreshing
            '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
            '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
            'End If
            If IsNothing(wdPTProtocols) = False AndAlso IsNothing(oWordApp) = False Then
                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdPTProtocols, oCurDoc, oWordApp)
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
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    Try
                '        oCurDoc.Save()
                '    Catch ex1 As Exception

                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                '    End Try
                'End Try
                '       oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)


                'If Not File.Exists(sFileName) Then
                '    Try
                '        File.Copy(oCurDoc.FullName, sFileName)
                '    Catch ex As Exception
                '        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '        ex = Nothing
                '    End Try
                'End If

                'If Not File.Exists(sFileName) Then
                '    Exit Sub
                'End If

                'wdPTProtocols.Close()


                'wdTemp.Open(sFileName)
                'oTempDoc = wdTemp.ActiveDocument
                'oTempDoc.ActiveWindow.SetFocus()
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Try
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, _PatientID, AddressOf FaxPTProtocol, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

                'If IsPrintFlag Then
                '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '    '    oTempDoc.Unprotect()
                '    'End If
                '    Dim oPrint As New clsPrintFAX
                '    oPrint.PrintDoc(oTempDoc, _PatientID)
                '    oPrint.Dispose()
                '    oPrint = Nothing
                'Else
                '    Call FaxPTProtocol(myLoadWord, oTempDoc)
                '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "PT Protocol Fax", gloAuditTrail.ActivityOutCome.Success)
                '    ''Added Rahul P on 20101011
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "PT Protocol Fax", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                '    ''
                '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "PT Protocol Fax", gstrLoginName, gstrClientMachineName, gnPatientID)
                'End If
                'wdTemp.Close()
                'Me.Controls.Remove(wdTemp)
                'wdTemp.Dispose()
                'wdTemp = Nothing
                'myLoadWord.CloseWordApplication(oTempDoc)
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

    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdPTProtocols, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try
        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '     Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        'wdPTProtocols.Close()

        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)


        'wdTemp.Open(sFileName)
        'oTempDoc = wdTemp.ActiveDocument
        'oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim osenddox As String = String.Empty
        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, _PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Dim oSendDoc As New clsPrintFAX
        'Dim osenddox As String
        'osenddox = oSendDoc.SendDoc(oTempDoc, _PatientID)
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
        If osenddox.Length > 0 Then
            If File.Exists(osenddox) Then
                Dim ofrmSendNewMail As New InBox.NewMail(_PatientID, osenddox)
                AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromPatientExam

                If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                    gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(_PatientStrip.ProviderID)
                    ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                End If
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()
                RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromPatientExam
                ofrmSendNewMail.Close()
                ofrmSendNewMail = Nothing
            Else
                MessageBox.Show("Error While generating attachment. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Error While generating attachment. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        'LoadWordUserControl(sFileName, False)
        'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        ''Set the Start postion of the cursor in documents
        'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        'oCurDoc.Saved = _SaveFlag
    End Sub


    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Try


            If blnCmbSelTemplate = False Then
                loadToolStrip()

            End If
            '    wdPTProtocols.Open(strFileName)
            ' Dim oWordApp As Wd.Application = Nothing
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdPTProtocols, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, strError, gloAuditTrail.ActivityOutCome.Failure)

            Else


                If blnGetData Then
                    ''//To retrieve the Form fields for the Word document
                    ObjWord = New clsWordDocument
                    objCriteria = New DocCriteria
                    objCriteria.DocCategory = enumDocCategory.Others
                    objCriteria.PatientID = _PatientID
                    objCriteria.VisitID = dtPTProtocol.Tag 'nVisitId
                    objCriteria.PrimaryID = 0
                    ObjWord.DocumentCriteria = objCriteria
                    ObjWord.CurDocument = oCurDoc
                    
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
                SetWordObjectEntry(_IsFinished)
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdPTProtocols_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPTProtocols.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    isHandlerRemoved = True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
              gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
             
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPTProtocols_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPTProtocols.OnDocumentClosed
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        Finally
            'GC.Collect()  ''slr for memory management
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub wdPTProtocols_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPTProtocols.OnDocumentOpened
        oCurDoc = wdPTProtocols.ActiveDocument
        oWordApp = oCurDoc.Application

        Try
            RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        Try
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            isHandlerRemoved = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

                        'Dim om As Object = System.Reflection.Missing.Value

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

        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    Private Sub loadToolStrip()
        If Not tlsPTProtocol Is Nothing Then
            tlsPTProtocol.Dispose()
        End If

        tlsPTProtocol = New WordToolStrip.gloWordToolStrip
        tlsPTProtocol.Dock = DockStyle.Top
        tlsPTProtocol.ConnectionString = GetConnectionString()
        tlsPTProtocol.UserID = gnLoginID
        ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
        tlsPTProtocol.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsPTProtocol.ptProvider = oclsProvider.GetPatientProviderName(_PatientID)
        tlsPTProtocol.ptProviderId = oclsProvider.GetPatientProvider(_PatientID)
        ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
        oclsProvider.Dispose()
        oclsProvider = Nothing
        tlsPTProtocol.BringToFront()
        If _IsFinished Then

            tlsPTProtocol.FormType = WordToolStrip.enumControlType.PTProtocolAddendum
            cmbTemplate.Enabled = False
            dtPTProtocol.Enabled = False
        Else
            tlsPTProtocol.IsCoSignEnabled = gblnCoSignFlag
            tlsPTProtocol.FormType = WordToolStrip.enumControlType.PTProtocols
            cmbTemplate.Enabled = True
            dtPTProtocol.Enabled = True
        End If

        '<<<<<<<<<<<<<<<<<<Ojeswini03032009>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'To add word toolstrip in panel.
        Me.pnlToolStrip.Controls.Add(tlsPTProtocol)
        Me.pnlToolStrip.Size = New System.Drawing.Size(940, 56)
        pnlToolStrip.SendToBack()
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsPTProtocol
                ShowMicroPhone()
            End If
        End If
        'If gnLoginProviderID = 0 And gblnAssociatedProviderSignature And _IsFinished = False And _IsRecordLock = False Then
        '    tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
        '    tlsPTProtocol.MyToolStrip.ButtonsToHide.Remove(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        'Else
        '    tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
        '    If (tlsPTProtocol.MyToolStrip.ButtonsToHide.Contains(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
        '        tlsPTProtocol.MyToolStrip.ButtonsToHide.Add(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        '    End If

        'End If
        If gblnAssociatedProviderSignature And _IsFinished = False Then
            tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsPTProtocol.MyToolStrip.ButtonsToHide.Remove(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        ElseIf _IsFinished = True Then

            tlsPTProtocol.MyToolStrip.Items("Save").Visible = False
            If (tlsPTProtocol.MyToolStrip.ButtonsToHide.Contains(tlsPTProtocol.MyToolStrip.Items("Save").Name) = False) Then
                tlsPTProtocol.MyToolStrip.ButtonsToHide.Add(tlsPTProtocol.MyToolStrip.Items("Save").Name)
            End If
            tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPTProtocol.MyToolStrip.ButtonsToHide.Contains(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPTProtocol.MyToolStrip.ButtonsToHide.Add(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        ElseIf gblnAssociatedProviderSignature = False Then
            tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsPTProtocol.MyToolStrip.ButtonsToHide.Contains(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsPTProtocol.MyToolStrip.ButtonsToHide.Add(tlsPTProtocol.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If


        End If

        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsPTProtocol.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsPTProtocol.MyToolStrip.ButtonsToHide.Contains(tlsPTProtocol.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsPTProtocol.MyToolStrip.ButtonsToHide.Add(tlsPTProtocol.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsPTProtocol.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsPTProtocol.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    Private Sub tlsPTProtocol_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsPTProtocol.ToolStripButtonClick
        If IsNothing(oCurDoc) = False Then
            InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
        End If
    End Sub
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE


    Private Sub tlsPTProtocol_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsPTProtocol.ToolStripClick
        Try

            ''******Shweta 20090828 *********'
            ''To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            ''End Shweta

            Select Case e.ClickedItem.Name
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in PTProtocols when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
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
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "witchOff Mic Completed from tblButtons_ButtonClick in PTProtocols when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                Case "Save"
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        If _IsFinished Then
                            Call SaveProtocol(True)
                        Else
                            Call SaveProtocol(False)
                        End If

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try

                Case "Save & Close"
                    Try
                        Me.Cursor = Cursors.WaitCursor

                        Call SaveProtocol(False, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                    dtPTProtocol.Enabled = False
                    cmbTemplate.Enabled = False

                Case "FAX"
                    bnlIsFaxOpened = True
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        Call GeneratePrintFaxDocument(False)
                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
                    dtPTProtocol.Enabled = False
                    cmbTemplate.Enabled = False
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
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "PTProtocol Viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    Me.Close()
                Case "Prescription"
                    TurnoffMicrophone()
                    ' MsgBox("Prescription")
                    Call GetPrescription()

                Case "OrderTemplates"
                    TurnoffMicrophone()
                    ' MsgBox("Orders")
                    Call GetOrders()
                Case "Save & Finish"
                    Try
                        Me.Cursor = Cursors.WaitCursor

                        Call SaveProtocol(True, True)
                        Me.Close()

                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
                Case "Add Addendum"
                    Call InsertAddendum()

                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "PT Protocol", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    ' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "SecureMsg"
                    If strProviderDirectAddress <> "" OrElse gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_PatientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            TurnoffMicrophone()
                            Call SendSecureMsg()
                            dtPTProtocol.Enabled = False
                            cmbTemplate.Enabled = False
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
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
                        If _IsFinished = True Then
                            oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)

                        End If


                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If _IsFinished = True Then
                tmrDocProtect.Enabled = True
            End If

        End Try
    End Sub



    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        'Dim WDocViewType As Wd.WdViewType
        'Dim wordRefresh As New WordRefresh()

        Try
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                'Ashish added on 1st October 
                'to prevent screen from refreshing

                '   WDocViewType = oCurDoc.ActiveWindow.View.Type
                'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
                'wordRefresh.ShowPanel(Me.pnlwdPTProtocols)

                Fill_TemplateGallery()

                blnCmbSelTemplate = False
            Else
                wdPTProtocols.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            blnCmbSelTemplate = False
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Ashish added on 31st October 
            'to prevent screen from refreshing
            'wordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
            'wordRefresh.HidePanel(Me.pnlwdPTProtocols)
            'wordRefresh.Dispose()
            'wordRefresh = Nothing
            'WDocViewType = Nothing
        End Try
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    'Private Const WM_GETMINMAXINFO As Integer = &H24

    'Private Structure POINTAPI
    '    Dim x As Integer
    '    Dim y As Integer
    'End Structure

    'Private Structure MINMAXINFO
    '    Dim ptReserved As POINTAPI
    '    Dim ptMaxSize As POINTAPI
    '    Dim ptMaxPosition As POINTAPI
    '    Dim ptMinTrackSize As POINTAPI
    '    Dim ptMaxTrackSize As POINTAPI
    'End Structure


    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '    If Me.IsMdiChild Then
    '        Select Case m.Msg
    '            Case WM_GETMINMAXINFO
    '                Dim mmi As MINMAXINFO = CType(m.GetLParam(GetType(MINMAXINFO)), MINMAXINFO)
    '                mmi.ptMinTrackSize.x = Me.MinimumSize.Width
    '                mmi.ptMinTrackSize.y = Me.MinimumSize.Height

    '                If Not (Me.MaximumSize.Width = 0 And Me.MaximumSize.Height = 0) Then
    '                    mmi.ptMaxTrackSize.x = Me.MaximumSize.Width
    '                    mmi.ptMaxTrackSize.y = Me.MaximumSize.Height
    '                End If
    '                System.Runtime.InteropServices.Marshal.StructureToPtr(mmi, m.LParam, True)
    '        End Select
    '    End If
    '    MyBase.WndProc(m)
    'End Sub

    ''' <summary>
    ''' ActivateBasic Voice commands
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
    ''' Add Voice Commands
    ''' </summary>
    ''' <remarks></remarks>    ''' 
    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
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
    ''' Turn on microphone button
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
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
    ''' Turn off microphone button
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
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
    ''' Initialise Voice Object
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)

        ogloVoice.MyWordToolStrip = Me.tlsPTProtocol
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "PTProtocols"
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.PTProtocol
        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsPTProtocol_ToolStripClick)
    End Sub
    ''' <summary>
    ''' Add basic voice commands
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save PTProtocols", "Save")
        oHashtable.Add("Print PTProtocols", "Print")
        oHashtable.Add("Fax PTProtocols", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close PTProtocols", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close PTProtocols", "Close")
        oHashtable.Add("Finish PTProtocols", "Save & Finish")
        oHashtable.Add("Prescription", "Prescription")


        Return oHashtable
    End Function
    ''' <summary>
    ''' Activate Voice commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsFinished = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        End If
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
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.PTProtocol
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "PTProtocols"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()
    End Sub
    ''' <summary>
    ''' Close Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose
        pnlgloUC_Addendum.Controls.Remove(ouctlgloUC_Addendum)
        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        pnlGloUC_PastWordNotes.Visible = True
        pnlwdPTProtocols.Visible = True
        pnlgloUC_Addendum.Visible = False
        pnlwdPTProtocols.BringToFront()
        blnIsAddendum = False
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Save Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

        pnlGloUC_PastWordNotes.Visible = True
        pnlwdPTProtocols.Visible = True
        pnlgloUC_Addendum.Visible = False
        pnlwdPTProtocols.BringToFront()

        blnIsAddendum = False
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
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub InitiliseTemplateTreeControl()
        Dim objDocriteria As New DocCriteria
        Dim objClsWord_TemplateTree As New clsWordDocument
        GloUC_TemplateTreeControl_PTProtocols.InitiliseControlParameter(GetConnectionString())
        GloUC_TemplateTreeControl_PTProtocols.DocCriteria = objDocriteria
        GloUC_TemplateTreeControl_PTProtocols.ObjClsWord = objClsWord_TemplateTree
        GloUC_TemplateTreeControl_PTProtocols.ProviderId = gnSelectedProviderID
        GloUC_TemplateTreeControl_PTProtocols.Fill_ExamTemplates(0)


    End Sub
    Public Sub CallPastData()
        Dim obj_UCclsPTProtocols As clsPTProtocols = New clsPTProtocols
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
        GloUC_PastWordNotes1.PATIENTIDs = _PatientID
        GloUC_PastWordNotes1.OBJCLSPTPROTOCOLs = obj_UCclsPTProtocols
        GloUC_PastWordNotes1.FromForms = "PTProtocols"
        GloUC_PastWordNotes1.CLSPATIENTEXAMSs = clsPatientExams
        GloUC_PastWordNotes1.ShowHide_PastExam()
    End Sub


    Private Sub Treeview_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs, sFilename As System.String) Handles GloUC_TemplateTreeControl_PTProtocols.Treeview_NodeMouseDoubleClick
        '   Dim WDocViewType As Wd.WdViewType
        '  Dim wordOptimizer As New WordRefresh()
        Dim Node As TreeNode = Nothing
        Dim TreeView As TreeView = Nothing
        Dim blnRefreshDocument As Boolean
        Try

            oCurDoc = wdPTProtocols.ActiveDocument
            ' oCurDoc.Application.ScreenUpdating = False
            oCurDoc.ActiveWindow.SetFocus()

            '04-Jan-2016 Aniket: Warn the user if the full document is being replaced by tags
            If gloWord.LoadAndCloseWord.ValidateTagsSelectedRange(oCurDoc) = True Then
                oCurDoc.Application.Selection.InsertFile(sFilename, "", False, False, False)
                blnRefreshDocument = True
            End If

            wdPTProtocols.Select()

            If TypeOf (sender) Is TreeView Then
                TreeView = DirectCast(sender, TreeView)

                If TreeView.SelectedNode IsNot Nothing Then
                    Node = TreeView.SelectedNode

                    If Node IsNot Nothing AndAlso Node.Parent IsNot Nothing AndAlso Node.Parent.Text.ToLower() <> "tags" Then
                        'WDocViewType = oCurDoc.ActiveWindow.View.Type
                        'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)
                        'wordOptimizer.ShowPanel(Me.pnlwdPTProtocols)
                    End If
                End If
            End If

            If blnRefreshDocument = True Then
                GetdataFromOtherForms(enumDocType.None)
            End If



            ' oCurDoc.Application.ScreenUpdating = True

            If IO.File.Exists(sFilename) Then
                IO.File.Delete(sFilename)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If TypeOf (sender) Is TreeView Then
                TreeView = DirectCast(sender, TreeView)

                If TreeView.SelectedNode IsNot Nothing Then
                    Node = TreeView.SelectedNode

                    If Node IsNot Nothing AndAlso Node.Parent IsNot Nothing AndAlso Node.Parent.Text.ToLower() <> "tags" Then
                        'wordOptimizer.HidePanel(Me.pnlwdPTProtocols)
                        'wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
                    End If
                End If
            End If

            TreeView = Nothing
            Node = Nothing
            'wordOptimizer.Dispose()
            'wordOptimizer = Nothing
        End Try
    End Sub

    Public Sub calltoAddRefreshButtonControl()

        ObjWord = New clsWordDocument
        ObjWord.WaitControlPanel = Me.pnlwdPTProtocols
        objCriteria = New DocCriteria
        objCriteria.PatientID = _PatientID
        Try
            objCriteria.VisitID = GenerateVisitID(dtPTProtocol.Value, _PatientID)
        Catch ex As Exception

        End Try

        GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
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

        GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
        GloUC_AddRefreshDic1.M_PATIENTIDs = _PatientID
        GloUC_AddRefreshDic1.ObjFrom = Me
        GloUC_AddRefreshDic1.DTLETTERDATEs = dtPTProtocol
        GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
        GloUC_AddRefreshDic1.wdPatientWordDocs = wdPTProtocols
    End Sub

    Private Sub cmdPastExam_Click(sender As System.Object, e As System.EventArgs) Handles cmdPastExam.Click
        Try

            If cmdPastExam.Text = "Show Past PTProtocols" Then
                pnlGloUC_PastWordNotes.Show()
                cmdPastExam.Text = "Hide Past PTProtocols"
                bIsPastPTprotocolClick = True
            ElseIf cmdPastExam.Text = "Hide Past PTProtocols" Then
                pnlGloUC_PastWordNotes.Hide()
                cmdPastExam.Text = "Show Past PTProtocols"
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub Dispose_Object()
        Try

            'If Not IsNothing(obj_UCclsPTProtocols) Then
            '    obj_UCclsPTProtocols = Nothing
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
            If (IsNothing(GloUC_TemplateTreeControl_PTProtocols) = False) Then
                GloUC_TemplateTreeControl_PTProtocols.FinalizeControlParameter("")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Dispose, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    'Public Delegate Sub GenerateCDAFromPTProtocols(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromPTProtocols(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromPatientExam(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromPTProtocols(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

   
End Class
