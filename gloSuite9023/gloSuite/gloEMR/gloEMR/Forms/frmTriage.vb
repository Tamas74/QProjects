' ********************************************************************************
'     Document    :  frmTriage.vb
'     Description :  [type_description_here]
' ********************************************************************************
Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices
' ********************************************************************************
'     Class       :  frmTriage
'     Description :  [type_description_here]
' ********************************************************************************
Public Class frmTriage
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Implements ISignature
    Implements IHotKey
    Implements gloVoice
    Implements IWord


    'Instantiate voice class
    Private ogloVoice As ClsVoice
    'implement interface for Voice --supriya 03/06/2009

    'Addendum user control defined here 
    Private blnIsAddendum As Boolean
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum


    '' set when caller form is frmVWMessages
    Public myCaller As frmVWTriage
    Public MyDayCaller As frmMyDay
    Private ImagePath As String
    '' Sets when Caller Form is MainMenu
    Public myCallerMain As MainMenu
    Public CancelClick As Boolean
    Public IsModify As Boolean = False
    ' If Users are to fill in Custum grid =TRUE
    ' If Patient are to fill in Custum grid =FALSE
    Public blnUserClick As Boolean
    Dim _blnRecordLock As Boolean
    Dim FieldValue As String ''// To get the field value from the Window Double click event of Word User Control 
    Private Const _History As String = "History"
    Private Const _Medication As String = "Medication"
    Private Const _Referrals As String = "Referrals"
    Private Const _Prescription As String = "Prescription"
    Private Const _Exam As String = "Exam"
    'Private Const _Order As String = "Order"
    Private Const _Order As String = "LM_Orders"
    Private Const _Message As String = "Message"
    Private Const _Diagnosis As String = "ExamICD9CPT"
    Private Const _Treatment As String = "ExamICD9CPT"
    Private Const _Vitals As String = "Vitals"
    Private Const _ROS As String = "ROS"
    Private Const _PatientEducation As String = "ExamEducation"
    Private Const _Flowsheet As String = "Flowsheet"
    Private Const _SmartDiagnosis As String = "SmartDiagnosis"
    Private Const _ProblemList As String = "ProblemList"
    Private Const _SmartTreatment As String = "SmartTreatment"
    Private Const _Tasks As String = "Tasks_MST"
    Private Const _ChiefComplaints As String = "sChiefComplaints"
    Private Const _PatientDemographics As String = "Patient"
    Private Const _PatientGuideline As String = "PatientGuideline"
    Private Const _Others As String = "Others"
    Private Const _None As String = "None"
    Private Const _Contacts As String = "Contacts_MST"
    Private Const _Narration As String = "Narration"
    Private Const _ProviderSign As String = "Provider_MST"
    '' On Reply Button Click blnReply = True 
    '' this, we r doing here so that even if User click on Repply Btn it does not make any affect
    Public Shared blnReply As Boolean = False
    Private blnCmbSelTemplate As Boolean = False
    Private blnSelectTemplate As Boolean

    '' The message ID for which User is Replying is stored in Reply_MsgID 
    '' Before Making m_MsgID =0  make Reply_MsgID = m_MsgID 
    Private Reply_MsgID As Long

    Private m_MsgID As Long
    Private m_UserID As Long

    '' For Message Date  
    Public Shared m_MsgDate As Date

    '' Value of this flag = 1 when Message form is Opend from Main Menu
    '' else i.e if it open from its View Form then flag=0
    Private m_Flag As Int16

    Public _IsReadOnly As Boolean = False 'To check if message is finished or does not belong to current user

    '' Message Addendum from Message Addendum Form  
    Public Shared strAddendum As String

    Public Shared ProviderUserID As Long

    'Private WithEvents dgCustomGrid As CustomDataGrid
    Private WithEvents dgCustomGrid As CustomTask
    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oCurDoc2 As Wd.Document

    'Private WithEvents oTempDoc As Wd.Document
    Dim UserCount As Integer
    Private WithEvents oWordApp As Wd.Application
    Dim _ShowPast As Boolean = True
    Dim objCriteria As DocCriteria
    Dim ObjWord As clsWordDocument
    Dim myidx As Int32

    Private Col_Check As Integer = 5
    Private Col_UserID As Integer = 0
    Private Col_LoginName As Integer = 1
    Private Col_Column1 As Integer = 2
    Private Col_Column2 As Integer = 3
    Private Col_ProviderID As Integer = 4
    Private m_IsRecordLock As Boolean = False
    Private MessagesVoicecol As DNSTools.DgnStrings
    Dim strTemp As String

    Private oClsTriage As New gloStream.gloEMR.Triage.clsTriage
    Private oTriage As gloStream.gloEMR.Triage.Supportings.Triage
    Private oDashBoard As New clsDoctorsDashBoard

    Public _PatientID As Int64 = 0
    Private _TriageID As Int64 = 0
    Private _TemplateID As Int64 = 0
    Private _FromID As Int64 = 0

    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String



    Private Const _MessageBoxCaption As String = "Triage"

    '' For ListControl for User
    Dim ofrmList As frmViewListControl
    Private oListControl As gloListControl.gloListControl
    Private oListUsers As gloListControl.gloListControl
    Private ToList As gloGeneralItem.gloItems
    ''
    Public Shared IsOpen As Boolean = False
    ''provider signature change:Integrated by Mayuri:20101021
    Private blnSignClick As Boolean = False
    Private bnlIsFaxOpened As Boolean

#Region " Windows Controls "
    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Private WithEvents tlsTriage As WordToolStrip.gloWordToolStrip
    Friend WithEvents wdPastMessages As AxDSOFramer.AxFramerControl
    Friend WithEvents wdMessages As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlFromTo As System.Windows.Forms.Panel
    Friend WithEvents cmbTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTriage As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents btnUser As System.Windows.Forms.Button
    'Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents chkIsFinished As System.Windows.Forms.CheckBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Friend WithEvents btnReply As System.Windows.Forms.Button
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnClearUser As System.Windows.Forms.Button
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
#End Region

#Region " Windows Form Designer generated code "
    'Constuctor commented by dipak 20100907 as constructor not in use.
    'Public Sub New()
    '    MyBase.New()
    '    'm_hotKeys = New HotKeyCollection(Me)
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    ''
    ''Mayuri:20090912
    ''For RecordLevel Locking in order to pass blnRecordLock parameter
    Public Sub New(ByVal triageID As Int64, ByVal patientID As Int64, ByVal flag As Int16, ByVal IsRecordLock As Boolean, ByVal templateID As Int64)
        MyBase.New()
        'm_hotKeys = New HotKeyCollection(Me)

        _PatientID = patientID
        _TriageID = triageID
        m_Flag = flag
        m_IsRecordLock = IsRecordLock
        _TemplateID = templateID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub



#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmTriage

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                Try

                    If (IsNothing(dgExams) = False) Then
                        dgExams.TableStyles.Clear()
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dgExams)
                        dgExams.Dispose()
                        dgExams = Nothing
                    End If
                Catch ex As Exception

                End Try
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpControls As DateTimePicker() = {dtpTriage}
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
                
                If (IsNothing(oClsTriage) = False) Then

                    oClsTriage = Nothing
                End If
                If (IsNothing(oTriage) = False) Then
                    oTriage.Dispose()
                    oTriage = Nothing
                End If
                If (IsNothing(oDashBoard) = False) Then
                    oDashBoard = Nothing
                End If
                If (IsNothing(objclsMessage) = False) Then
                    objclsMessage.Dispose()
                    objclsMessage = Nothing
                End If
                Try
                    If (IsNothing(ToList) = False) Then
                        ToList.Dispose()
                        ToList = Nothing
                    End If
                Catch ex As Exception

                End Try
                If (IsNothing(objCriteria) = False) Then
                    objCriteria.Dispose()
                    objCriteria = Nothing
                End If
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
    'GetInstance commented as not in use and we are not allow non parameterised constructor ad Patient id and visit required for case UC5070.003
    'Public Shared Function GetInstance() As frmTriage
    '    '_mu.WaitOne()
    '    Try
    '        If frm Is Nothing Then
    '            frm = New frmTriage
    '        End If
    '    Finally
    '        '_mu.ReleaseMutex()
    '    End Try
    '    Return frm
    'End Function

    Public Shared Function GetInstance(ByVal triageID As Int64, ByVal patientID As Int64, Optional ByVal flag As Int16 = 0, Optional ByVal IsRecordLock As Boolean = False, Optional ByVal templateID As Int64 = 0) As frmTriage
        '_mu.WaitOne()S
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmTriage" Then
                    If (CType(f, frmTriage)._TriageID = triageID) And (CType(f, frmTriage)._PatientID = patientID) Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmTriage(triageID, patientID, flag, IsRecordLock, templateID)
            End If

            'If frm Is Nothing Then
            '    frm = New frmTriage(triageID, patientID, flag, IsRecordLock, templateID)
            'End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region

    ''Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlExtraTop As System.Windows.Forms.Panel
    Friend WithEvents pnlExtraBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlWordObj As System.Windows.Forms.Panel
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnFAX As System.Windows.Forms.Button
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    Friend WithEvents pnlPastExam As System.Windows.Forms.Panel
    Friend WithEvents pnlExam1 As System.Windows.Forms.Panel
    Friend WithEvents pnlPastExamView As System.Windows.Forms.Panel
    Friend WithEvents dgExams As gloEMR.clsDataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tmrPastExamProtect As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTriage))
        Me.pnlExtraTop = New System.Windows.Forms.Panel
        Me.pnlExtraBottom = New System.Windows.Forms.Panel
        Me.btnFAX = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.pnlWordObj = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.wdMessages = New AxDSOFramer.AxFramerControl
        Me.pnlcustomTask = New System.Windows.Forms.Panel
        Me.pnlFromTo = New System.Windows.Forms.Panel
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnReply = New System.Windows.Forms.Button
        Me.txtFrom = New System.Windows.Forms.TextBox
        Me.lblFrom = New System.Windows.Forms.Label
        Me.cmbTo = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtpTriage = New System.Windows.Forms.DateTimePicker
        Me.cmbTemplate = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblTo = New System.Windows.Forms.Label
        Me.btnClearUser = New System.Windows.Forms.Button
        Me.btnUser = New System.Windows.Forms.Button
        '    Me.wdTemp = New AxDSOFramer.AxFramerControl
        Me.chkIsFinished = New System.Windows.Forms.CheckBox
        Me.txtPatient = New System.Windows.Forms.TextBox
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlPastExam = New System.Windows.Forms.Panel
        Me.pnlExam1 = New System.Windows.Forms.Panel
        Me.pnlPastExamView = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.wdPastMessages = New AxDSOFramer.AxFramerControl
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.dgExams = New gloEMR.clsDataGrid
        Me.Label19 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.tmrPastExamProtect = New System.Windows.Forms.Timer(Me.components)
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.pnlExtraBottom.SuspendLayout()
        Me.pnlWordObj.SuspendLayout()
        CType(Me.wdMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFromTo.SuspendLayout()
        '     CType(Me.wdTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPastExam.SuspendLayout()
        Me.pnlExam1.SuspendLayout()
        Me.pnlPastExamView.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.wdPastMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlExtraTop
        '
        Me.pnlExtraTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlExtraTop.Location = New System.Drawing.Point(0, 5)
        Me.pnlExtraTop.Name = "pnlExtraTop"
        Me.pnlExtraTop.Size = New System.Drawing.Size(909, 0)
        Me.pnlExtraTop.TabIndex = 3
        '
        'pnlExtraBottom
        '
        Me.pnlExtraBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlExtraBottom.Controls.Add(Me.btnFAX)
        Me.pnlExtraBottom.Controls.Add(Me.btnPrint)
        Me.pnlExtraBottom.Controls.Add(Me.btnCancel)
        Me.pnlExtraBottom.Controls.Add(Me.btnSend)
        Me.pnlExtraBottom.Location = New System.Drawing.Point(415, 555)
        Me.pnlExtraBottom.Name = "pnlExtraBottom"
        Me.pnlExtraBottom.Size = New System.Drawing.Size(314, 40)
        Me.pnlExtraBottom.TabIndex = 5
        Me.pnlExtraBottom.Visible = False
        '
        'btnFAX
        '
        Me.btnFAX.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFAX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFAX.Location = New System.Drawing.Point(178, 8)
        Me.btnFAX.Name = "btnFAX"
        Me.btnFAX.Size = New System.Drawing.Size(64, 24)
        Me.btnFAX.TabIndex = 3
        Me.btnFAX.Text = "&FAX"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Location = New System.Drawing.Point(114, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(64, 24)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "&Print"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(242, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Close"
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSend.Location = New System.Drawing.Point(51, 8)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(64, 24)
        Me.btnSend.TabIndex = 0
        Me.btnSend.Text = "&Save"
        '
        'pnlWordObj
        '
        Me.pnlWordObj.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlWordObj.Controls.Add(Me.Label5)
        Me.pnlWordObj.Controls.Add(Me.Label6)
        Me.pnlWordObj.Controls.Add(Me.Label7)
        Me.pnlWordObj.Controls.Add(Me.Label8)
        Me.pnlWordObj.Controls.Add(Me.wdMessages)
        Me.pnlWordObj.Controls.Add(Me.pnlcustomTask)
        Me.pnlWordObj.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWordObj.Location = New System.Drawing.Point(213, 91)
        Me.pnlWordObj.Name = "pnlWordObj"
        Me.pnlWordObj.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlWordObj.Size = New System.Drawing.Size(696, 539)
        Me.pnlWordObj.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 535)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(691, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 535)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(692, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 535)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(693, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'wdMessages
        '
        Me.wdMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdMessages.Enabled = True
        Me.wdMessages.Location = New System.Drawing.Point(0, 0)
        Me.wdMessages.Name = "wdMessages"
        Me.wdMessages.OcxState = CType(resources.GetObject("wdMessages.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdMessages.Size = New System.Drawing.Size(693, 536)
        Me.wdMessages.TabIndex = 4
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.Location = New System.Drawing.Point(258, 192)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(590, 259)
        Me.pnlcustomTask.TabIndex = 6
        Me.pnlcustomTask.Visible = False
        '
        'pnlFromTo
        '
        Me.pnlFromTo.BackColor = System.Drawing.Color.Transparent
        Me.pnlFromTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFromTo.Controls.Add(Me.Label24)
        Me.pnlFromTo.Controls.Add(Me.Label2)
        Me.pnlFromTo.Controls.Add(Me.Label9)
        Me.pnlFromTo.Controls.Add(Me.Label10)
        Me.pnlFromTo.Controls.Add(Me.Label11)
        Me.pnlFromTo.Controls.Add(Me.btnReply)
        Me.pnlFromTo.Controls.Add(Me.txtFrom)
        Me.pnlFromTo.Controls.Add(Me.lblFrom)
        Me.pnlFromTo.Controls.Add(Me.cmbTo)
        Me.pnlFromTo.Controls.Add(Me.Label4)
        Me.pnlFromTo.Controls.Add(Me.dtpTriage)
        Me.pnlFromTo.Controls.Add(Me.cmbTemplate)
        Me.pnlFromTo.Controls.Add(Me.Label3)
        Me.pnlFromTo.Controls.Add(Me.lblTo)
        Me.pnlFromTo.Controls.Add(Me.btnClearUser)
        Me.pnlFromTo.Controls.Add(Me.btnUser)
        '   Me.pnlFromTo.Controls.Add(Me.wdTemp)
        Me.pnlFromTo.Controls.Add(Me.chkIsFinished)
        Me.pnlFromTo.Controls.Add(Me.txtPatient)
        Me.pnlFromTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFromTo.Location = New System.Drawing.Point(213, 5)
        Me.pnlFromTo.Name = "pnlFromTo"
        Me.pnlFromTo.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlFromTo.Size = New System.Drawing.Size(696, 86)
        Me.pnlFromTo.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.Red
        Me.Label24.Location = New System.Drawing.Point(7, 50)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(14, 14)
        Me.Label24.TabIndex = 23
        Me.Label24.Text = "*"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(691, 1)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 79)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(692, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 79)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(693, 1)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "label1"
        '
        'btnReply
        '
        Me.btnReply.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnReply.BackgroundImage = CType(resources.GetObject("btnReply.BackgroundImage"), System.Drawing.Image)
        Me.btnReply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnReply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnReply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReply.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReply.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnReply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReply.ImageIndex = 2
        Me.btnReply.Location = New System.Drawing.Point(264, 15)
        Me.btnReply.Name = "btnReply"
        Me.btnReply.Size = New System.Drawing.Size(58, 24)
        Me.btnReply.TabIndex = 17
        Me.btnReply.Text = " Reply"
        Me.btnReply.UseVisualStyleBackColor = False
        Me.btnReply.Visible = False
        '
        'txtFrom
        '
        Me.txtFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtFrom.Location = New System.Drawing.Point(56, 16)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(201, 22)
        Me.txtFrom.TabIndex = 15
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFrom.Location = New System.Drawing.Point(8, 20)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblFrom.TabIndex = 14
        Me.lblFrom.Text = "From :"
        '
        'cmbTo
        '
        Me.cmbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTo.DropDownWidth = 200
        Me.cmbTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.cmbTo.Location = New System.Drawing.Point(56, 47)
        Me.cmbTo.Name = "cmbTo"
        Me.cmbTo.Size = New System.Drawing.Size(201, 22)
        Me.cmbTo.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(385, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 14)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Triage Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpTriage
        '
        Me.dtpTriage.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpTriage.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpTriage.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpTriage.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpTriage.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpTriage.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTriage.Location = New System.Drawing.Point(467, 47)
        Me.dtpTriage.Name = "dtpTriage"
        Me.dtpTriage.Size = New System.Drawing.Size(199, 22)
        Me.dtpTriage.TabIndex = 8
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.cmbTemplate.Location = New System.Drawing.Point(467, 16)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(199, 22)
        Me.cmbTemplate.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(355, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Triage Template : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTo.Location = New System.Drawing.Point(18, 50)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(34, 14)
        Me.lblTo.TabIndex = 1
        Me.lblTo.Text = "To : "
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClearUser
        '
        Me.btnClearUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnClearUser.BackgroundImage = CType(resources.GetObject("btnClearUser.BackgroundImage"), System.Drawing.Image)
        Me.btnClearUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClearUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClearUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearUser.Image = CType(resources.GetObject("btnClearUser.Image"), System.Drawing.Image)
        Me.btnClearUser.Location = New System.Drawing.Point(292, 48)
        Me.btnClearUser.Name = "btnClearUser"
        Me.btnClearUser.Size = New System.Drawing.Size(22, 22)
        Me.btnClearUser.TabIndex = 11
        Me.btnClearUser.UseVisualStyleBackColor = False
        '
        'btnUser
        '
        Me.btnUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnUser.BackgroundImage = CType(resources.GetObject("btnUser.BackgroundImage"), System.Drawing.Image)
        Me.btnUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnUser.Image = CType(resources.GetObject("btnUser.Image"), System.Drawing.Image)
        Me.btnUser.Location = New System.Drawing.Point(264, 48)
        Me.btnUser.Name = "btnUser"
        Me.btnUser.Size = New System.Drawing.Size(22, 22)
        Me.btnUser.TabIndex = 10
        Me.btnUser.TextAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnUser.UseVisualStyleBackColor = False
        '
        'wdTemp
        '
        'Me.wdTemp.Enabled = True
        'Me.wdTemp.Location = New System.Drawing.Point(672, 101)
        'Me.wdTemp.Name = "wdTemp"
        'Me.wdTemp.OcxState = CType(resources.GetObject("wdTemp.OcxState"), System.Windows.Forms.AxHost.State)
        'Me.wdTemp.Size = New System.Drawing.Size(9, 8)
        'Me.wdTemp.TabIndex = 18
        '
        'chkIsFinished
        '
        Me.chkIsFinished.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkIsFinished.Location = New System.Drawing.Point(608, 49)
        Me.chkIsFinished.Name = "chkIsFinished"
        Me.chkIsFinished.Size = New System.Drawing.Size(47, 18)
        Me.chkIsFinished.TabIndex = 13
        Me.chkIsFinished.Text = "Is Finished"
        Me.chkIsFinished.Visible = False
        '
        'txtPatient
        '
        Me.txtPatient.Enabled = False
        Me.txtPatient.ForeColor = System.Drawing.Color.Black
        Me.txtPatient.Location = New System.Drawing.Point(586, 47)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(80, 22)
        Me.txtPatient.TabIndex = 2
        Me.txtPatient.Visible = False
        '
        'tmrDocProtect
        '
        '
        'pnlPastExam
        '
        Me.pnlPastExam.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlPastExam.Controls.Add(Me.pnlExam1)
        Me.pnlPastExam.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPastExam.Location = New System.Drawing.Point(0, 5)
        Me.pnlPastExam.Name = "pnlPastExam"
        Me.pnlPastExam.Size = New System.Drawing.Size(210, 625)
        Me.pnlPastExam.TabIndex = 7
        Me.pnlPastExam.Visible = False
        '
        'pnlExam1
        '
        Me.pnlExam1.Controls.Add(Me.pnlPastExamView)
        Me.pnlExam1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExam1.Location = New System.Drawing.Point(0, 0)
        Me.pnlExam1.Name = "pnlExam1"
        Me.pnlExam1.Size = New System.Drawing.Size(210, 625)
        Me.pnlExam1.TabIndex = 1
        '
        'pnlPastExamView
        '
        Me.pnlPastExamView.BackColor = System.Drawing.Color.Transparent
        Me.pnlPastExamView.Controls.Add(Me.Panel3)
        Me.pnlPastExamView.Controls.Add(Me.Splitter1)
        Me.pnlPastExamView.Controls.Add(Me.Panel2)
        Me.pnlPastExamView.Controls.Add(Me.Panel4)
        Me.pnlPastExamView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPastExamView.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPastExamView.Location = New System.Drawing.Point(0, 0)
        Me.pnlPastExamView.Name = "pnlPastExamView"
        Me.pnlPastExamView.Size = New System.Drawing.Size(210, 625)
        Me.pnlPastExamView.TabIndex = 3
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.wdPastMessages)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 205)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(210, 420)
        Me.Panel3.TabIndex = 40
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 416)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(205, 1)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 416)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label4"
        '
        'wdPastMessages
        '
        Me.wdPastMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPastMessages.Enabled = True
        Me.wdPastMessages.Location = New System.Drawing.Point(3, 1)
        Me.wdPastMessages.Name = "wdPastMessages"
        Me.wdPastMessages.OcxState = CType(resources.GetObject("wdPastMessages.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPastMessages.Size = New System.Drawing.Size(206, 416)
        Me.wdPastMessages.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(209, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 416)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(207, 1)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 202)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(210, 3)
        Me.Splitter1.TabIndex = 42
        Me.Splitter1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.dgExams)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(210, 170)
        Me.Panel2.TabIndex = 40
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(4, 169)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(205, 1)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 169)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(209, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 169)
        Me.Label18.TabIndex = 10
        Me.Label18.Text = "label3"
        '
        'dgExams
        '
        Me.dgExams.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgExams.BackgroundColor = System.Drawing.Color.White
        Me.dgExams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgExams.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgExams.CaptionFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.CaptionForeColor = System.Drawing.Color.White
        Me.dgExams.CaptionVisible = False
        Me.dgExams.DataMember = ""
        Me.dgExams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgExams.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.ForeColor = System.Drawing.Color.Black
        Me.dgExams.FullRowSelect = True
        Me.dgExams.GridLineColor = System.Drawing.Color.Black
        Me.dgExams.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgExams.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.HeaderForeColor = System.Drawing.Color.White
        Me.dgExams.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgExams.Location = New System.Drawing.Point(3, 1)
        Me.dgExams.Name = "dgExams"
        Me.dgExams.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgExams.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgExams.ReadOnly = True
        Me.dgExams.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgExams.SelectionForeColor = System.Drawing.Color.Black
        Me.dgExams.Size = New System.Drawing.Size(207, 169)
        Me.dgExams.TabIndex = 37
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(207, 1)
        Me.Label19.TabIndex = 9
        Me.Label19.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(210, 32)
        Me.Panel4.TabIndex = 41
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(207, 26)
        Me.Panel1.TabIndex = 19
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(205, 1)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(205, 25)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "  Past Exams"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 25)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(206, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 25)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(207, 1)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "label1"
        '
        'tmrPastExamProtect
        '
        Me.tmrPastExamProtect.Enabled = True
        Me.tmrPastExamProtect.Interval = 10
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(210, 5)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 625)
        Me.Splitter2.TabIndex = 8
        Me.Splitter2.TabStop = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(909, 5)
        Me.pnlToolStrip.TabIndex = 40
        '
        'frmTriage
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(909, 630)
        Me.Controls.Add(Me.pnlWordObj)
        Me.Controls.Add(Me.pnlFromTo)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlPastExam)
        Me.Controls.Add(Me.pnlExtraBottom)
        Me.Controls.Add(Me.pnlExtraTop)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTriage"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Triage"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlExtraBottom.ResumeLayout(False)
        Me.pnlWordObj.ResumeLayout(False)
        CType(Me.wdMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFromTo.ResumeLayout(False)
        Me.pnlFromTo.PerformLayout()
        '  CType(Me.wdTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPastExam.ResumeLayout(False)
        Me.pnlExam1.ResumeLayout(False)
        Me.pnlPastExamView.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.wdPastMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''---------------
    'code commented by supriya on 13/11/2006
    'Private m_hotKeys As HotKeyCollection

    'Public Event HotKeyPressed As HotKeyPressedEventHandler
    'Public Event PrintWindowPressed As PrintWindowPressedEventHandler
    'Public Event PrintDesktopPressed As PrintDesktopPressedEventHandler
    'Public ReadOnly Property HotKeys() As HotKeyCollection
    '    Get
    '        HotKeys = m_hotKeys
    '    End Get
    'End Property

    'Public Sub RestoreAndActivate()
    '    If Not (UnmanagedMethods.IsWindowVisible(Me.Handle)) Then
    '        UnmanagedMethods.ShowWindow(Me.Handle, UnmanagedMethods.SW_SHOW)
    '    End If
    '    If (UnmanagedMethods.IsIconic(Me.Handle)) Then
    '        UnmanagedMethods.SendMessage(Me.Handle, UnmanagedMethods.WM_SYSCOMMAND, _
    '            UnmanagedMethods.SC_RESTORE, IntPtr.Zero)
    '    End If
    '    UnmanagedMethods.SetForegroundWindow(Me.Handle)
    'End Sub
    'code commented by supriya on 13/11/2006

    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        'HotKeys.Clear()
        MyBase.OnClosed(e)
    End Sub

    Private Sub frmTriage_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Try
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If
            If _IsReadOnly = False Then
                If Not IsNothing(wdMessages) Then
                    oCurDoc = wdMessages.ActiveDocument
                    oWordApp = oCurDoc.Application
                    If (ObjWord Is Nothing) Then
                        InitialiseWordObject()
                    End If

                    'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
                    'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    RemoveWordHandlers()

                    'AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    'AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
                    AddWordHandlers()

                    isHandlerRemoved = False
                    oCurDoc.ActiveWindow.SetFocus()
                    oCurDoc.FormFields.Shaded = False
                End If
            End If
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdMessages
            End If
        End Try
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''--------------

    Private Sub frmTriage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'UpdateVoiceLog("-------------------------Messages Load Started----------------------")

        'If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
        '    Try
        '        'UpdateVoiceLog("-------------------------Add Voice collection Started at Messages Load----------------------")
        '        MessagesVoicecol = New DNSTools.DgnStrings

        '        'Fill Voice Collections
        '        Call AddBasicVoiceCommands()

        '        'UpdateVoiceLog("-------------------------Add Voice collection Completed at Messages Load----------------------")

        '        Dim frm As MainMenu
        '        frm = CType(Me.MdiParent, MainMenu)
        '        'Add Voice Commands

        '        'UpdateVoiceLog("------------------------SetRecognition Started at Messages Load----------------------")
        '        frm.Vcmd.ExecuteScript("SetRecognitionMode 0", 0)
        '        'UpdateVoiceLog("------------------------SetRecognition Completed at Messages Load----------------------")

        '        frm = Nothing
        '    Catch ex As Exception
        '        MessageBox.Show("Error Initializing Voice in Patient Messages", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        'UpdateVoiceLog("-------------------------Error Initializing Voice in Patient Messages load " & ex.tostring & " ----------------------")
        '    End Try
        'End If


        Try
            'Dim dt As DataTable
            txtFrom.Enabled = False
            txtFrom.BackColor = SystemColors.Window
            txtFrom.ForeColor = Color.Black
            'Current Login Name who is sending Message

            '''' If Message is Finished Only then Addendum could add
            'tblbtnAddendum.Visible = False

            dtpTriage.Format = DateTimePickerFormat.Custom   '"dd/MM/yyyy hh:mm tt"
            dtpTriage.CustomFormat = "MM/dd/yyyy hh:mm tt"
            dtpTriage.Value = Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") 'MsgDate

            '' To fill Templates of type 'Messages to ComboBox ' 
            Call Get_PatientDetails()
           
            Call FillTemplateName()
            Call loadPatientStrip()
            If _TriageID = 0 Then
                '' Add new Messages
                IsModify = False
                _FromID = gnLoginID

                'If add then login name is from 
                txtFrom.Text = gstrLoginName
                txtFrom.Tag = m_UserID

                txtPatient.Tag = _PatientID
                txtPatient.Text = strPatientFirstName + Space(1) + strPatientLastName
                txtPatient.Enabled = False
                txtPatient.BackColor = SystemColors.Window
                txtPatient.ForeColor = Color.Black
                blnSelectTemplate = True
                ''''
                Fill_TemplateGallery()
            Else
                '' SUDHIR - LOADING TRIAGE ''
                IsModify = True
                ''Fixed issue:#7835
                Me.Text = "Modify Triage"
                oTriage = oClsTriage.GetTriage(_TriageID)
                txtPatient.Tag = _PatientID
                If Not IsNothing(oTriage) Then

                    _FromID = oTriage.FromID
                    txtFrom.Text = oDashBoard.GetUserName(oTriage.FromID)
                    cmbTemplate.Text = oTriage.TemplateName
                    dtpTriage.Value = Format(oTriage.TriageDate, "MM/dd/yyyy") & " " & Format(oTriage.TriageDate, "Medium Time")
                    _IsReadOnly = oTriage.IsFinished

                    ''Load Triage File
                    LoadWordUserControl(oTriage.TriageFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    SetWordObjectEntry()
                    oCurDoc.Saved = True

                    cmbTo.Items.Clear()
                    ''commendted on 20101028
                    ''Added on 20101028
                    Dim dtUsers As New DataTable()
                    Dim dcId As New DataColumn("ID")
                    Dim dcDescription As New DataColumn("Description")
                    dtUsers.Columns.Add(dcId)
                    dtUsers.Columns.Add(dcDescription)
                    ''
                    Try
                        If (IsNothing(ToList) = False) Then
                            ToList.Dispose()
                            ToList = Nothing
                        End If
                    Catch ex As Exception

                    End Try
                    ToList = New gloGeneralItem.gloItems()
                    Dim ToItem As gloGeneralItem.gloItem
                    Dim _user As String
                    For i As Integer = 1 To oTriage.TriageDetails.Count
                        _user = oDashBoard.GetUserName(oTriage.TriageDetails(i).ToID)
                        ''
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("ID") = oTriage.TriageDetails(i).ToID
                        drTemp("Description") = _user
                        dtUsers.Rows.Add(drTemp)

                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oTriage.TriageDetails(i).ToID
                        ToItem.Description = _user
                        ToList.Add(ToItem)
                        ToItem.Dispose()
                        ToItem = Nothing

                        ''ToList.Add(oTriage.TriageDetails(i).ToID, _user)

                        If _user <> "" Then
                            cmbTo.Items.Add(_user)
                            'cmbTo.Items.Add(oTriage.TriageDetails(i).ToID)
                        End If
                    Next
                    cmbTo.DataSource = dtUsers
                    cmbTo.ValueMember = dtUsers.Columns("ID").ColumnName
                    cmbTo.DisplayMember = dtUsers.Columns("Description").ColumnName
                    ''End code Added on 20101028

                    'ToList = New gloGeneralItem.gloItems
                    'Dim _user As String
                    'For i As Integer = 1 To oTriage.TriageDetails.Count
                    '    _user = oDashBoard.GetUserName(oTriage.TriageDetails(i).ToID)
                    '    ToList.Add(oTriage.TriageDetails(i).ToID, _user)

                    '    If _user <> "" Then
                    '        cmbTo.Items.Add(_user)
                    '        'cmbTo.Items.Add(oTriage.TriageDetails(i).ToID)
                    '    End If
                    'Next
                    ''''''''


                    ''''''''
                    If cmbTo.Items.Count > 0 Then
                        cmbTo.SelectedIndex = 0
                    End If
                End If


                '' END SUDHIR ''

                ''For Update Existing Message
                blnSelectTemplate = True
                ''
                '' When Message form Opened from MainMenu, Only then 'Reply' Button is Visible
                If m_Flag = 1 Then
                    btnReply.Visible = True
                End If
                ''                 
            End If
            ''''If the message is open for modification from dashbord then disable date and Template combobox
            If IsModify = True Then
                dtpTriage.Enabled = False
                cmbTemplate.Enabled = False
            End If
            If _IsReadOnly Then
                cmbTo.Enabled = False
                btnUser.Enabled = False
                btnClearUser.Enabled = False
            End If

            pnlcustomTask.Visible = False
            '  m_IsRecordLock = blnRecordLock
            If m_IsRecordLock Then
                If tlsTriage.MyToolStrip.Items.ContainsKey("Save") = True Then
                    tlsTriage.MyToolStrip.Items("Save").Enabled = False
                End If
                If tlsTriage.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                    tlsTriage.MyToolStrip.Items("Save & Finish").Enabled = False
                End If
                If tlsTriage.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                    tlsTriage.MyToolStrip.Items("Add Addendum").Enabled = False
                End If
            End If
            'tlsMessages.MyToolStrip.Items("Save & Close").Enabled = False
            'tlsMessages.MyToolStrip.Items("Save & Finish").Enabled = False


            'End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Triage Open", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Triage Open", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Messages Opened", gstrLoginName, gstrClientMachineName, gnPatientID)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
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



    '' to Fill all templates from Template-Gallery of type 'Message'  
    Private Sub FillTemplateName()
        Dim dt As DataTable
        If _TriageID <> 0 Then
            Dim objclsNotes As New clsNurseNotes
            dt = objclsNotes.FillTemplate(enumTemplateFlag.Triage, _TriageID, _TemplateID).Copy()
            objclsNotes.Dispose()
            objclsNotes = Nothing

        Else

            dt = oClsTriage.FillTemplates()
        End If


        cmbTemplate.Items.Clear()

        If IsNothing(dt) = False Then
            cmbTemplate.DataSource = dt
            cmbTemplate.ValueMember = dt.Columns(0).ColumnName
            cmbTemplate.DisplayMember = dt.Columns(1).ColumnName
            If dt.Rows.Count > 0 Then
                cmbTemplate.SelectedValue = dt.Rows(0)(0)
            End If
        End If
    End Sub
    Dim objclsMessage As New clsMessage
    Private Sub GetUserID(ByVal PatientID As Long)
        ProviderUserID = objclsMessage.GetProviderUserID(PatientID)
    End Sub

    'Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'If Trim(txtPatient.Text) = "" Then
    '        '    MessageBox.Show("Please select the Patient, who want to send the message", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    btnPatient_Click(sender, e)
    '        '    Exit Sub
    '        'End If

    '        If cmbTo.Items.Count <= 0 Then
    '            MessageBox.Show("Please select the User", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            cmbTo.Focus()
    '            Exit Sub
    '        End If

    '        If blnSelectTemplate = False Then
    '            MessageBox.Show("Please select the Message Template", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            cmbTemplate.Focus()
    '            Exit Sub
    '        End If

    '        Dim strFileName As String
    '        strFileName = ExamNewDocumentName
    '        wdMessages.Save(strFileName, True, "", "")
    '        wdMessages.Close()
    '        oCurDoc = Nothing
    '        oWordApp = Nothing

    '        Dim ArrUser(cmbTo.Items.Count - 1) As Long
    '        Dim mlist As myList
    '        Dim i As Integer

    '        For i = 0 To cmbTo.Items.Count - 1
    '            mlist = cmbTo.Items.Item(i)
    '            ArrUser(i) = mlist.Index()

    '        Next
    '        'dtMessage.Tag = objclsMessage.AddNewMessage(m_MsgID, nExamID, txtPatient.Tag, Now, 0, ArrUser)
    '        ''@ID,  @FromID,    @PatientID, @dtMsgDate,                                     @Result,@bIsFinished
    '        If objclsMessage.AddNewMessage(Reply_MsgID, m_MsgID, txtFrom.Tag, txtPatient.Tag, cmbTemplate.SelectedValue, Now.Date & " " & Format(Now, "Medium Time"), strFileName, chkIsFinished.CheckState, blnReply, ArrUser, cmbTemplate.Text) = False Then
    '            CancelClick = False
    '        End If
    '        LoadWordUserControl(strFileName, False)
    '        'Set the Start postion of the cursor in documents
    '        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
    '        oCurDoc.Saved = True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Function SaveTriage(Optional ByVal IsClose As Boolean = False) As Boolean
    '    Try
    '        SaveTriage = False
    '        If Trim(txtPatient.Text) = "" Then
    '            MessageBox.Show("Please select the Patient, who want to send the message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            'btnPatient_Click(sender, e)
    '            Exit Function
    '        End If

    '        If cmbTo.Items.Count <= 0 Then
    '            MessageBox.Show("Please select the User", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            cmbTo.Focus()
    '            Exit Function
    '        End If

    '        If blnSelectTemplate = False Then
    '            MessageBox.Show("Please select the Message Template", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            cmbTemplate.Focus()
    '            Exit Function
    '        End If

    '        If chkIsFinished.Checked = True Then
    '            '' IF IsFinished = True Then Remove the Unused Fields 
    '            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then ''= Wd.WdProtectionType.wdllowOnlyComments 
    '                oCurDoc.Application.ActiveDocument.Unprotect()
    '            End If
    '            Dim objword As New clsWordDocument
    '            objword.CurDocument = oCurDoc
    '            objword.CleanupDoc()
    '            oCurDoc = objword.CurDocument
    '            objword = Nothing
    '        End If

    '        Dim strFileName As String
    '        strFileName = ExamNewDocumentName
    '        oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
    '        'wdMessages.Save(strFileName, True, "", "")
    '        wdMessages.Close()

    '        Dim ArrUser(cmbTo.Items.Count - 1) As Long
    '        Dim mlist As myList
    '        Dim i As Integer

    '        For i = 0 To cmbTo.Items.Count - 1
    '            mlist = cmbTo.Items.Item(i)
    '            ArrUser(i) = mlist.Index()
    '        Next

    '        'dtMessage.Tag = objclsMessage.AddNewMessage(m_MsgID, nExamID, txtPatient.Tag, Now, 0, ArrUser)
    '        ''@ID,  @FromID,    @PatientID, @dtMsgDate,                                     @Result,@bIsFinished

    '        cmbTemplate.DropDownStyle = ComboBoxStyle.Simple
    '        If m_MsgID <> 0 Then
    '            m_MsgID = objclsMessage.AddNewMessage(Reply_MsgID, m_MsgID, txtFrom.Tag, txtPatient.Tag, 0, Format(dtMessage.Value, "MM/dd/yyyy hh:mm tt"), strFileName, chkIsFinished.CheckState, blnReply, ArrUser, strTemp)
    '        Else
    '            m_MsgID = objclsMessage.AddNewMessage(Reply_MsgID, m_MsgID, txtFrom.Tag, txtPatient.Tag, 0, Format(dtMessage.Value, "MM/dd/yyyy hh:mm tt"), strFileName, chkIsFinished.CheckState, blnReply, ArrUser, cmbTemplate.Text)
    '        End If

    '        If m_MsgID > 0 Then
    '            If IsClose = False Then
    '                LoadWordUserControl(strFileName, False)
    '                dtMessage.Enabled = False
    '                cmbTemplate.Enabled = False
    '                '-- sarika 20081201 template name editable after save
    '                'Set the Start postion of the cursor in documents
    '                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
    '                oCurDoc.Saved = True
    '            End If
    '        Else
    '            oCurDoc.Saved = False
    '        End If
    '        _PatientStrip.Dock = DockStyle.Top
    '        wdMessages.BringToFront()
    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function

    'Private Sub btnPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatient.Click
    '    Try
    '        blnUserClick = False
    '        LoadGrid()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Function SaveTriage(Optional ByVal IsClose As Boolean = False) As Boolean
        Dim oTriage As New gloStream.gloEMR.Triage.Supportings.Triage
        Dim oTriageDetails As New gloStream.gloEMR.Triage.Supportings.TriageDetails
        Dim oTriageDetail As gloStream.gloEMR.Triage.Supportings.TriageDetail

        Try
            SaveTriage = False
            'If Trim(txtPatient.Text) = "" Then
            '    MessageBox.Show("Please select the Patient, who want to send the message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'btnPatient_Click(sender, e)
            '    Exit Function
            'End If
            'If Not (ToList) Is Nothing Then
            'If ToList.Count <= 0 Then
            If cmbTo.Items.Count <= 0 Then
                MessageBox.Show("Please select the User", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbTo.Focus()
                Exit Function
                'End If
            End If


            If blnSelectTemplate = False Then
                MessageBox.Show("Please select the Message Template", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbTemplate.Focus()
                Exit Function
            End If
            Dim isExceptionWhileCopingFile As Boolean = False
            If chkIsFinished.Checked = True Then
                '' IF IsFinished = True Then Remove the Unused Fields 
                ''Sandip Darade 20090414
                ''If no doc is available 
                If Not (oCurDoc) Is Nothing Then
                    oCurDoc.ActiveWindow.SetFocus()
                    If wdMessages.ActiveDocument IsNot Nothing Then
                        'If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then ''= Wd.WdProtectionType.wdllowOnlyComments 

                        '' 
                        '' Ref of all word windows save method 
                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments Then
                            oCurDoc.Application.ActiveDocument.Unprotect()
                        End If
                    End If
                    'Dim objword As New clsWordDocument
                    'objword.CurDocument = oCurDoc
                    'objword.CleanupDoc()
                    'oCurDoc = objword.CurDocument
                    'objword = Nothing
                    gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                    gloWord.LoadAndCloseWord.LockFields(oCurDoc)
                End If

                'Return True
            End If
            ''Sandip Darade 20090413
            ''no templates available
            If (IsModify = False) Then
                If (cmbTemplate.Items.Count <= 0) Then
                    MessageBox.Show("There are no templates available.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Function
                End If
            End If
            'If (IsNothing(oCurDoc) = False) Then
            '    oCurDoc.Save()
            'Else
            ' wdMessages.Save()
            ' gloWord.LoadAndCloseWord.SaveDSO(wdMessages, oCurDoc, oWordApp)
            'End If

            'Dim strFileName As String
            'strFileName = ExamNewDocumentName
            'Try
            '    FileSystem.FileCopy(oCurDoc.FullName, strFileName)
            'Catch ex As Exception
            '    'UpdateLog("ERROR WHILE COPING FILE IN MESSAGE :" & ex.ToString())
            '    'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    isExceptionWhileCopingFile = True
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            'End Try
            'If (isExceptionWhileCopingFile) Then
            '    oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
            '    wdMessages.Close()
            'End If
            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdMessages, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If

            _TemplateID = Convert.ToInt64(cmbTemplate.SelectedValue)

            '' Create Triage Object for MasterRecords
            oTriage.TriageID = _TriageID
            oTriage.FromID = _FromID
            oTriage.PatientID = _PatientID
            oTriage.TemplateID = _TemplateID
            oTriage.TemplateName = cmbTemplate.Text.Trim()
            oTriage.TriageDate = Format(dtpTriage.Value, "MM/dd/yyyy hh:mm tt")
            oTriage.TriageObject = myBinaray
            oTriage.IsFinished = chkIsFinished.Checked

            '' Create TriageDetail object for DetailRecords
            For i As Integer = 0 To ToList.Count - 1
                oTriageDetail = New gloStream.gloEMR.Triage.Supportings.TriageDetail
                oTriageDetail.TriageID = _TriageID
                oTriageDetail.ToID = ToList.Item(i).ID
                oTriageDetail.FromID = _FromID
                oTriageDetail.TriageDate = Format(dtpTriage.Value, "MM/dd/yyyy hh:mm tt")
                oTriageDetail.IsReplied = False
                oTriageDetails.Add(oTriageDetail)
                oTriageDetail = Nothing
            Next

            oTriage.TriageDetails = oTriageDetails

            '' SAVE TRIAGE ''
            _TriageID = oClsTriage.SaveTriageBytes(oTriage)

            cmbTemplate.DropDownStyle = ComboBoxStyle.Simple

            If _TriageID > 0 Then
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
                    dtpTriage.Enabled = False
                    cmbTemplate.Enabled = False
                    '-- sarika 20081201 template name editable after save
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
            '        ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            '        ex = Nothing
            '    End Try
            'End If
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.Dock = DockStyle.Top
            End If

            wdMessages.BringToFront()
            oTriage.Dispose()
            oTriage = Nothing
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function

    Private Sub LoadGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Width = pnlcustomTask.Height
                dgCustomGrid.BringToFront()
                BindGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub BindGrid()

        Try
            Dim dt As DataTable

            dt = objclsMessage.GetAllPatients
            'dgCustomGrid.SetDatasource(dt.DefaultView)
            Dim col As New DataColumn

            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")
            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)


            If Not IsNothing(dt) Then

                '' For DataBinding Users
                dgCustomGrid.datasource(dt.Copy().DefaultView)
                ' Sort data view on Login Name
                ' ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName)
                objclsMessage.SortDataview(objclsMessage.GetDataview.Table.Columns(1).ColumnName)

            End If


            UserCount = dt.Rows.Count
            dt.Dispose()
            dt = Nothing
            'CustomUserGridStyle()
            CustomGridStyle()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
           
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

    'Add customGrid control to form 
    Private Sub AddControl()
        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask

        'pnlWordObj.Controls.Add(dgCustomGrid)
        pnlcustomTask.Controls.Add(dgCustomGrid)
        pnlcustomTask.BringToFront()
        pnlcustomTask.Location = New Point(258, 192)
        'pnlcustomTask.Top = btnUser.Bottom
        'Dim pt As New Point()
        'pt.X = btnUser.Location.X + 10
        'pt.Y = btnUser.Location.Y + 10
        'pnlcustomTask.Location = pt
        pnlcustomTask.Visible = True
        dgCustomGrid.Visible = True
        dgCustomGrid.BringToFront()

    End Sub

    Public Sub CustomGridStyle()


        'Dim dv As DataView
        'dv = objclsMessage.GetDataview
        'Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

        'Dim IDCol As New DataGridTextBoxColumn
        'IDCol.Width = 0
        'IDCol.MappingName = dv.Table.Columns(0).ColumnName
        'IDCol.HeaderText = "PatientID"

        'Dim CodeCol As New DataGridTextBoxColumn
        'With CodeCol
        '    .Width = 0.2 * (dgCustomGrid.Width)
        '    .MappingName = dv.Table.Columns(1).ColumnName
        '    .HeaderText = "Patient ID"  'Patient code
        '    .NullText = ""
        'End With

        'Dim FirstNameCol As New DataGridTextBoxColumn
        'With FirstNameCol
        '    .Width = 0.25 * (dgCustomGrid.Width)
        '    .MappingName = dv.Table.Columns(2).ColumnName
        '    .HeaderText = "First Name"
        '    .NullText = ""
        'End With

        'Dim LastNameCol As New DataGridTextBoxColumn
        'With LastNameCol
        '    .Width = 0.25 * (dgCustomGrid.Width)
        '    .MappingName = dv.Table.Columns(3).ColumnName
        '    .HeaderText = "Last Name"
        '    .NullText = ""
        'End With

        'Dim DoctorIDCol As New DataGridTextBoxColumn
        'With DoctorIDCol
        '    .Width = 0.0000001 * (dgCustomGrid.Width)
        '    .MappingName = dv.Table.Columns(4).ColumnName
        '    .HeaderText = "Doctor's ID" ' ProviderID
        '    .NullText = ""
        'End With

        'Dim DoctorNameCol As New DataGridTextBoxColumn
        'With DoctorNameCol
        '    .Width = 0.3 * (dgCustomGrid.Width)
        '    .MappingName = dv.Table.Columns(5).ColumnName
        '    .HeaderText = "Doctor's Name"  ' Provider Name
        '    .NullText = ""
        'End With

        'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, CodeCol, FirstNameCol, LastNameCol, DoctorNameCol})
        'dgCustomGrid.SetTableStyleCol(ts)
        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5


        ' '' Show User Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = 6
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.09
            .Cols(Col_Check).AllowEditing = True


            .SetData(0, Col_UserID, "UserID")
            '.Cols(Col_UserID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_UserID).Width = _TotalWidth * 0
            .Cols(Col_UserID).AllowEditing = False

            .SetData(0, Col_LoginName, "Login Name")
            '.Cols(Col_LoginName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_LoginName).Width = _TotalWidth * 0.44
            .Cols(Col_LoginName).AllowEditing = False

            .SetData(0, Col_Column1, "Name")
            '.Cols(Col_Column1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Column1).Width = _TotalWidth * 0.47
            .Cols(Col_Column1).AllowEditing = False

            .Cols(Col_ProviderID).Width = 0
            .Cols(Col_Column2).Width = 0

            'Move the last column select to first column
            .Cols.Move(.Cols.Count - 1, 0)
        End With

    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try
            ' If blnUserClick = True Then
            'User Selelcted 

            Dim i As Long
            For i = 0 To UserCount
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    '' The Message Can Not Send to HimSelf/Herself
                    '' 20071207 - Code Commented By Mahesh  IF Condition Commented
                    '' The Can Send to HimSelf/Herself
                    'If CType(dgCustomGrid.GetItem(i, 1), Long) <> txtFrom.Tag Then
                    If FindDuplicateTo(CType(dgCustomGrid.GetItem(i, 1), Long)) Then
                        If IsDBNull(dgCustomGrid.GetItem(i, 2)) Then
                            cmbTo.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 1), Long), CType(dgCustomGrid.GetItem(i, 2), System.String)))
                        Else
                            cmbTo.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 1), Long), CType(dgCustomGrid.GetItem(i, 2), System.String)))
                        End If

                        cmbTo.Text = CType(dgCustomGrid.GetItem(i, 2), System.String)
                    End If
                    'End If
                    ''
                End If
            Next
            'Else
            '    'Patient Selelcted 
            '    If dgCustomGrid.GetCurrentrowIndex >= 0 Then

            '        txtPatient.Text = CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2) & " " & dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 3), System.String)
            '        txtPatient.Tag = CType(dgCustomGrid.CurrentID, Long)

            '        GetUserID(txtPatient.Tag)
            '    End If
            ' End If

            pnlcustomTask.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub

    Private Function FindDuplicateTo(ByVal Id As Int64) As Boolean
        Dim i As Integer
        For i = 0 To cmbTo.Items.Count - 1
            Dim objmylist As myList
            objmylist = (CType(cmbTo.Items.Item(i), myList))
            If Id = objmylist.Index Then
                Return False
                Exit Function
            End If
        Next
        Return True
    End Function

    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub BindUserGrid()
        Try
            Dim dt As DataTable
            dt = objclsMessage.GetAllUser
            Dim col As New DataColumn

            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")
            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)


            If Not IsNothing(dt) Then

                '' For DataBinding Users
                dgCustomGrid.datasource(dt.Copy().DefaultView)
                ' Sort data view on Login Name
                ' ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName)
                objclsMessage.SortDataview(objclsMessage.GetDataview.Table.Columns(1).ColumnName)

            End If


            UserCount = dt.Rows.Count
            dt.Dispose()
            dt = Nothing
            CustomUserGridStyle()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub CustomUserGridStyle()
        'Dim dv As DataView
        'dv = objclsMessage.GetDataview
        'Dim ts As New clsDataGridTableStyle(dv.Table.TableName)
        'ts.RowHeadersVisible = True

        'Dim IDCol As New DataGridTextBoxColumn
        'IDCol.Width = 0
        'IDCol.MappingName = dv.Table.Columns(0).ColumnName
        'IDCol.HeaderText = "UserID"

        'Dim LoginCol As New DataGridTextBoxColumn
        'With LoginCol
        '    .Width = 0.33 * (dgCustomGrid.Width - 100)
        '    .MappingName = dv.Table.Columns(1).ColumnName
        '    .HeaderText = "Login Name"
        '    .NullText = ""
        'End With

        'Dim UserNameCol As New DataGridTextBoxColumn
        'With UserNameCol
        '    .Width = 0.34 * (dgCustomGrid.Width - 100)
        '    .MappingName = dv.Table.Columns(2).ColumnName
        '    .HeaderText = "User Name"
        '    .NullText = ""
        'End With

        'Dim DoctorIDCol As New DataGridTextBoxColumn
        'With DoctorIDCol
        '    .Width = 0 * (dgCustomGrid.Width - 100)
        '    .MappingName = dv.Table.Columns(3).ColumnName
        '    .HeaderText = "Doctor's ID"
        '    .NullText = ""
        'End With

        'Dim DoctorCol As New DataGridTextBoxColumn
        'With DoctorCol
        '    .Width = 0.33 * (dgCustomGrid.Width - 100)
        '    .MappingName = dv.Table.Columns(4).ColumnName
        '    .HeaderText = "Doctor's Name"
        '    .NullText = ""
        'End With
        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5


        '' Show User Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = 6
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.09
            .Cols(Col_Check).AllowEditing = True


            .SetData(0, Col_UserID, "UserID")
            '.Cols(Col_UserID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_UserID).Width = _TotalWidth * 0
            .Cols(Col_UserID).AllowEditing = False

            .SetData(0, Col_LoginName, "Login Name")
            '.Cols(Col_LoginName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_LoginName).Width = _TotalWidth * 0.44
            .Cols(Col_LoginName).AllowEditing = False

            .SetData(0, Col_Column1, "Name")
            '.Cols(Col_Column1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Column1).Width = _TotalWidth * 0.47
            .Cols(Col_Column1).AllowEditing = False

            .Cols(Col_ProviderID).Width = 0
            .Cols(Col_Column2).Width = 0

            'Move the last column select to first column
            .Cols.Move(.Cols.Count - 1, 0)
        End With

        'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, LoginCol, UserNameCol, DoctorIDCol, DoctorCol})
        'dgCustomGrid.SetTableStyleCol(ts)

    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Private Sub Fill_TemplateGallery()
        Try
            Try
                dtpTriage.Tag = GenerateVisitID(dtpTriage.Value, _PatientID)
            Catch ex As Exception

            End Try

            Dim strFileName As String
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = cmbTemplate.SelectedValue
            ObjWord.DocumentCriteria = objCriteria
            strFileName = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            If (IsNothing(strFileName) = False) Then
                ''//Open Template for processing in DSO Ctrl
                LoadWordUserControl(strFileName, True)
                If strFileName <> "" Then
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub SetWordObjectEntry()
        Try
            oCurDoc = wdMessages.ActiveDocument
        Catch ex As Exception

        End Try

        If (IsNothing(oCurDoc)) Then
            Return
        End If
        oWordApp = oCurDoc.Application
        oCurDoc.ActiveWindow.SetFocus()
        If _IsReadOnly = True Then
            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
            End If

            chkIsFinished.Checked = True
            tmrDocProtect.Enabled = True
            btnSend.Enabled = False
            btnSend.BackColor = Color.Gray
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
            If _IsReadOnly = True Then
                If blnIsAddendum = False Then
                    If Not oCurDoc Is Nothing Then
                        oCurDoc.Application.ActiveWindow.SetFocus()
                        Dim protectPane As Wd.TaskPane = oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                        If (IsNothing(protectPane) = False) Then
                            protectPane.Visible = False
                            Marshal.ReleaseComObject(protectPane)
                            protectPane = Nothing
                        End If
                        ' oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrDocProtect.Enabled = True
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub CloseMessage()
        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                Dim result As Integer
                result = MessageBox.Show("Do you want to save the changes in Triage?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If result = DialogResult.Yes Then
                    If _IsReadOnly Then
                        chkIsFinished.CheckState = CheckState.Checked
                    Else
                        chkIsFinished.CheckState = CheckState.Unchecked
                    End If
                    If SaveTriage(True) Then
                        wdMessages.Close()
                        Me.Close()
                    End If
                ElseIf result = DialogResult.No Then
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Triage viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    wdMessages.Close()
                    Me.Close()
                ElseIf result = DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                wdMessages.Close()

                Me.Close()
            End If
        Else
            wdMessages.Close()

            Me.Close()
        End If

    End Sub

    Private Sub dgCustomGrid_MouseUpClick(ByVal sender As Object, ByVal e As Object) Handles dgCustomGrid.MouseUpClick
        Try
            If dgCustomGrid.GetCurrentrowIndex >= 0 Then
                dgCustomGrid.GetSelect(dgCustomGrid.GetCurrentrowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub dgCustomGrid_Dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.Dblclick
        Try
            'If blnUserClick = False Then
            '    'Patient Selelcted 
            '    If dgCustomGrid.GetCurrentrowIndex >= 0 Then

            '        txtPatient.Text = CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2) & " " & dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 3), System.String)
            '        txtPatient.Tag = CType(dgCustomGrid.CurrentID, Long)
            '    End If
            '    dgCustomGrid.Visible = False
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Call PrintMessage()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub PrintMessage()
        Try
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient Messages'" & cmbTemplate.Text & "' Printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient Messages'" & cmbTemplate.Text & "' Printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Patient Messages'" & cmbTemplate.Text & "' Printed.", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ShowHide_PastExam()
        If tlsTriage.MyToolStrip.Items("Show/Hide").Text = "Show" Then
            '' then Show 
            tlsTriage.MyToolStrip.Items("Show/Hide").Text = "Hide"
            pnlPastExam.Show()
            FillPastExams()
            _ShowPast = False
        Else
            ''then Hide
            tlsTriage.MyToolStrip.Items("Show/Hide").Text = "Show"
            pnlPastExam.Hide()
            If IsNothing(oCurDoc) = False Then
                oCurDoc.ActiveWindow.SetFocus()
            End If
            _ShowPast = True
        End If
    End Sub
    'Procedure to fill past exams
    Private Sub FillPastExams()

        dgExams.DataSource = Nothing
        Dim dtExam As DataTable
        Dim clsPatientExams As New clsPatientExams
        'Pass patient id & get all exams for that patient
        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'dtExam = clsPatientExams.Fill_Exams(gnPatientID)
        dtExam = clsPatientExams.Fill_Exams(_PatientID)
        clsPatientExams.Dispose()
        'end modification by dipak
        clsPatientExams = Nothing
        dgExams.DataSource = dtExam.DefaultView
        'Grid Style
        Dim grdTableStyle As New clsDataGridTableStyle(dtExam.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Exam ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Visit ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(1).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "DOS"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(2).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3)
        End With

        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Exam Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(3).ColumnName
            .NullText = ""
            .Width = dgExams.Width / 3
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "Finished"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtExam.Columns(4).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With


        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True

    End Sub

    Private Sub Undo()
        Try
            If IsNothing(oCurDoc) = False Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Undo, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Redo()
        Try
            If IsNothing(oCurDoc) = False Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Redo, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub InsertAddendum()
        Try
            If _IsReadOnly Then
                'Dim f As New frmMsg_Addendum(8) '' 8 for frmTriage
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

                pnlToolStrip.Visible = False
                ouctlgloUC_Addendum = New gloUC_Addendum(0, _TriageID, _PatientID)
                blnIsAddendum = True
                Me.Controls.Add(ouctlgloUC_Addendum)
                ouctlgloUC_Addendum.Dock = DockStyle.Fill
                ouctlgloUC_Addendum.BringToFront()
                If gblnSpeakerExists = True And gblnVoiceEnabled = True Then
                    InitializeVoiceObjectForAddendum()
                    ShowMicroPhone()
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try

        'oCurDoc1.Application.Selection.InsertRows(1)

    End Sub

    Private Sub ReplyMessage()
        '''' When User Reply the message then he wants to send message to person who sends message to him

        chkIsFinished.CheckState = CheckState.Unchecked

        Dim objMylist As New myList(txtFrom.Tag, txtFrom.Text)
        cmbTo.Items.Clear()
        cmbTo.Items.Add(objMylist)
        cmbTo.Text = Trim(txtFrom.Text)

        txtFrom.Text = gstrLoginName
        txtFrom.Tag = gnLoginID '' CType(objclsMessage.GetUserID(gstrLoginName), Long)

        ''  
        m_MsgDate = dtpTriage.Value

        '' Change the Message-Date to Now(default)
        dtpTriage.Value = Now

        Me.Text = "Reply Message"

    End Sub

    Public Sub GetPrescription()
        TurnoffMicrophone()

        Try
            dtpTriage.Tag = GenerateVisitID(dtpTriage.Value, _PatientID)
        Catch ex As Exception

        End Try

        If Trim(strPatientFirstName) <> "" Then

            Dim frmRxMeds As frmPrescription
            frmRxMeds = frmPrescription.GetInstance(CType(dtpTriage.Tag, Long), CType(txtPatient.Tag, Long))
            If IsNothing(frmRxMeds) = True Then
                Exit Sub
            End If

            If frmPrescription.IsOpen = False Then
                frmRxMeds.ShowMedication()
            End If

            If frmRxMeds.blncancel = True Then
                With frmRxMeds
                    .WindowState = FormWindowState.Maximized
                    .myCaller1 = Me
                    .MdiParent = Me.ParentForm
                    .blnOpenFromMessage = True
                    .Show()
                End With
            End If
        Else
            MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    'Public Sub GetdataFromOtherForms(ByVal ListDoctType As List(Of gloEMRWord.enumDocType)) Implements IWord.GetdataFromOtherForms
    '    If Not oCurDoc Is Nothing Then
    '        oCurDoc.ActiveWindow.SetFocus()

    '        ObjWord = Nothing
    '        InitialiseWordObject()


    '        ObjWord.WaitControlPanel = Me.pnlWordObj

    '        ObjWord.CurDocument = oCurDoc
    '        ObjWord.DisableWordRefresh = True
    '        Try
    '            ObjWord.DisableDSORefresh()
    '            For Each element As gloEMRWord.enumDocType In ListDoctType
    '                ObjWord.GetFormFieldData(element)
    '            Next
    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '            ex = Nothing
    '        Finally
    '            ObjWord.EnableDSORefresh()
    '        End Try

    '        oCurDoc = ObjWord.CurDocument
    '        objCriteria = Nothing
    '        ObjWord.Dispose()
    '        ObjWord = Nothing

    '        oWordApp = oCurDoc.Application
    '        If Not oCurDoc Is Nothing Then
    '            Try
    '                If (isHandlerRemoved) Then
    '                    RemoveWordHandlers()
    '                    AddWordHandlers()
    '                    isHandlerRemoved = False
    '                End If
    '            Catch ex As Exception
    '                UpdateVoiceLog(ex.ToString)
    '                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '                ex = Nothing
    '            Finally
    '                objCriteria = Nothing
    '                ObjWord = Nothing
    '            End Try
    '        End If
    '    End If
    'End Sub

    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType) Implements IWord.GetdataFromOtherForms
        If Not IsNothing(oCurDoc) Then
            oCurDoc.ActiveWindow.SetFocus()
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Message
            objCriteria.PatientID = txtPatient.Tag
            'Resolved bug No.77804 ::Triage>Modfy Triage>Add value through liquid link
            If (IsNothing(dtpTriage) = False) Then
                If Not IsNothing(dtpTriage.Tag) Then
                    objCriteria.VisitID = dtpTriage.Tag
                Else
                    objCriteria.VisitID = GenerateVisitID(dtpTriage.Value, _PatientID)
                End If
            End If
           
            '
            objCriteria.PrimaryID = 0
            ObjWord.DocumentCriteria = objCriteria
            ObjWord.CurDocument = oCurDoc
            ObjWord.GetFormFieldData(_DocType)
            oCurDoc = ObjWord.CurDocument
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
        End If

    End Sub

    Public Sub GetOrders()
        TurnoffMicrophone()
        Try
            dtpTriage.Tag = GenerateVisitID(dtpTriage.Value, _PatientID)
        Catch ex As Exception

        End Try

        If Trim(strPatientFirstName) <> "" Then

            Dim frmOrders As frm_LM_Orders
            frmOrders = frm_LM_Orders.GetInstance(dtpTriage.Tag, dtpTriage.Value, _PatientID, 0, False)

            If IsNothing(frmOrders) = True Then
                Exit Sub
            End If

            With frmOrders
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Maximized
                '.lblPatientName.Text = txtPatient.Text
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
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = _PatientID
            'end modification by dipak
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
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Co-Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Co-Signature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
            'end modification by dipak 
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
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Triage", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
        End Try

    End Sub
    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        'Try
        '    If oCurDoc Is Nothing Then
        '        Exit Sub
        '    End If
        '    ''Integrated by Mayuri:20101021-Provider signature change
        '    Dim rslt As Boolean
        '    Dim oclsProvider As New clsProvider
        '    rslt = oclsProvider.CheckSignDelegateStatus()
        '    Dim _ProviderID As Int64
        '    If ProviderID <> 0 Then
        '        ' Dim oclsProvider As New clsProvider
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
        '                    ''Insert PAtient Provider Sign
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
        '    ''End 20101021
        '    '' VisitID 
        '    'dtMessage.Tag = objclsMessage.GenerateVisitID(txtPatient.Tag, dtMessage.Value)
        '    dtpTriage.Tag = GenerateVisitID(dtpTriage.Value, _PatientID)

        '    Dim objWord As New clsWordDocument
        '    Dim objCriteria As DocCriteria
        '    objCriteria = New DocCriteria
        '    objCriteria.DocCategory = enumDocCategory.Others
        '    'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        '    'objCriteria.PatientID = gnPatientID
        '    objCriteria.PatientID = _PatientID
        '    objCriteria.ProviderID = ProviderID
        '    'end modification by dipak
        '    objCriteria.VisitID = dtpTriage.Tag
        '    objCriteria.PrimaryID = 0
        '    objWord.DocumentCriteria = objCriteria

        '    ImagePath = objWord.getData_FromDB("Provider_MST.imgSignature", "Provider Signature")
        '    objCriteria = Nothing
        '    objWord = Nothing
        '    ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
        '    ''Integrated by Mayuri:20101021-Provider signature change
        '    If ImagePath = "" Then
        '        If blnSignClick = True Then
        '            MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Else
        '            MessageBox.Show("Selected Provider has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End If
        '        Return
        '    End If
        '    ''End 20101021
        '    If File.Exists(ImagePath) Then
        '        oCurDoc.ActiveWindow.SetFocus()

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
        '        ''Integrated by Mayuri:20101021-provider sign
        '        Dim clsExam As New clsPatientExams
        '        Dim strProviderName As String = ""
        '        If ProviderID <> 0 Then
        '            strProviderName = clsExam.GetProvidernameforExam(ProviderID)
        '        Else
        '            strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
        '        End If

        '        'Developer: Yatin N.Bhagat
        '        'Date:01/20/2012
        '        'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
        '        'Reason: If Condition is added to check the Setting
        '        If oclsProvider.AddUserNameInProviderSignature() Then
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
        '        Else
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")) '& " (" & gstrLoginName & ")"
        '        End If

        '        ''End 20101021
        '        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Signature Inserted", gloAuditTrail.ActivityOutCome.Success)
        '        ''Added Rahul P on 20101011
        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.None, "Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, _PatientID, dtpTriage.Tag, blnSignClick)
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
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            objErr = Nothing
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub frmTriage_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            '    Try
            '        'UpdateVoiceLog("------------------------Mic Switched On/Off started at Patient Messages Closed Event----------------------")

            '        If CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
            '            CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
            '        End If

            '        'UpdateVoiceLog("------------------------Mic Switched On/Off Completed at Patient Messages Closed Event----------------------")

            '        'CType(Me.MdiParent, MainMenu).tlbbtn_Microphone.Visible = False

            '        If Not IsNothing(vVoiceMenu) Then
            '            'UpdateLog("------------------------VoiceMenu Destruction started at Patient Messages Closed Event----------------------")
            '            vVoiceMenu.Remove(1)
            '            vVoiceMenu.Active = False
            '            System.Runtime.InteropServices.Marshal.ReleaseComObject(vVoiceMenu)
            '            vVoiceMenu = Nothing
            '            'UpdateVoiceLog("------------------------VoiceMenu Destruction Completed at Patient Messages Closed Event----------------------")
            '        End If


            '    Catch ex As Exception
            '        UpdateVoiceLog("------------------------Error UnInitializing Voice at Patient Messages Closed Event " & ex.ToString & " ----------------------")
            '    End Try
            'End If
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
            Try
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Triage Close", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Triage Close", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Messages Closed", gstrLoginName, gstrClientMachineName, gnPatientID)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            If m_Flag = 0 Then
                Me.Hide()
                If IsNothing(myCaller) = False Then
                    If myCaller.CanSelect = True Then
                        myCaller.RefreshTriage()
                    End If
                End If

                CType(Me.ParentForm, MainMenu).ShowTriage()
            ElseIf m_Flag = 1 Then
                '' if frmTriage is Opend from MainMenu then 
                Me.Hide()
                ''DB myCallerMain.Fill_Messages()
            ElseIf m_Flag = 2 Then
                MyDayCaller.DesignGrid(MyDayCaller.C1Triage, frmMyDay.GridType.Triage)
            ElseIf m_Flag = 3 Then
                CType(myCallerMain, MainMenu).ShowTriage()
            End If

            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            '''' if form is open in view i.e. Record lock then unlock the record and close the form.
            If m_IsRecordLock = False Then
                UnLock_Transaction(TrnType.Messages, m_MsgID, 0, m_MsgDate)
            End If
            ' CType(Me.ParentForm, MainMenu).ShowTriage()
            '' <><><> Unlock the Record <><><>
            'If Not oWordApp Is Nothing Then
            '    oWordApp.RecentFiles.Maximum = 0
            '    oWordApp.DisplayRecentFiles = False
            '    Marshal.FinalReleaseComObject(oWordApp)
            'End If


            'If m_IsRecordLock = False Then
            '    '' if the Locked by by the Current User & on Current Machine only
            '    UnLock_Transaction(TrnType.Triage, TriageID, 0, Now)
            'End If
            ''
            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
            End If
            Try
                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try
        End Try

    End Sub



    Private Sub FAXMessage(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientMessages, txtPatient.Tag, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0, True, Me) = False Then
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
        '    UpdateLog("Deleting Messages Page Header")
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader

        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            UpdateLog("Messages Page Header deleted")
        '        End If

        '    Catch ex As Exception
        '        UpdateVoiceLog("Error Deleting Messages Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try

        'End If
        'End Commenting


        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, txtPatient.Tag, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.SelectedItem(1), clsPrintFAX.enmFAXType.PatientMessages) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    Public Sub InsertSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            ImagePath = ""
            Dim frm As New FrmSignature
            frm.Owner = Me
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()

            ''commented by Dhruv 20091214 
            ''To not to save on form closing
            'If File.Exists(ImagePath) Then
            '    oCurDoc.ActiveWindow.SetFocus()

            '    '' SUDHIR 20090619 '' 
            '    Dim oWord As New clsWordDocument
            '    oWord.CurDocument = oCurDoc
            '    oWord.InsertImage(ImagePath)
            '    oWord = Nothing
            '    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
            '    '' END SUDHIR ''

            '    oCurDoc.Application.Selection.TypeParagraph()
            '    '' By Mahesh Signature With Date - 20070113
            '    '''' Add Date Time When Signature is Inserted
            '    oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
            '    ''''
            'End If
        Catch objErr As Exception
            MessageBox.Show(objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            ImagePath = Value
        End Set
    End Property

    Private Sub dgExams_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgExams.DoubleClick
        Try
            'Dim nPastExamId As Long
            If dgExams.CurrentRowIndex <> -1 Then
                FillPastExamContents(dgExams.Item(dgExams.CurrentRowIndex, 0))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    'Procedure to get the contents of Past exam by passimg exam id
    Private Sub FillPastExamContents(ByVal nPastExamId As Long)
        Dim strFileName As String
        ObjWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Exam
        objCriteria.PrimaryID = nPastExamId
        ObjWord.DocumentCriteria = objCriteria
        ''// Get the Docuemnt From DB
        strFileName = ObjWord.RetrieveDocumentFile()
        objCriteria.Dispose()
        objCriteria = Nothing
        ObjWord = Nothing
        If (IsNothing(strFileName)) Then
            Exit Sub
        End If
        If strFileName = "" Then
            Exit Sub
        End If
        ObjWord = Nothing

        ' wdPastMessages.Open(strFileName)
        ' Dim oWordApp As Wd.Application = Nothing
        gloWord.LoadAndCloseWord.OpenDSO(wdPastMessages, strFileName, oCurDoc2, oWordApp, True)

        oCurDoc2 = wdPastMessages.ActiveDocument
        oWordApp = oCurDoc2.Application

        SetWordObjectView()

    End Sub

    Private Sub SetWordObjectView()
        'oCurDoc2.ActiveWindow.SetFocus()
        'oCurDoc2.ActiveWindow.View.WrapToWindow = True
        'oCurDoc2.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
        'oCurDoc2.ActiveWindow.View.Type = 7
        tmrPastExamProtect.Enabled = False
        '''' to Hide Protection Bar when Document is Finished

        '' Save Btn is Always INVisible when doc is Finished
        '' Initalise Timer
        tmrPastExamProtect.Enabled = True
        tmrPastExamProtect.Interval = 10


    End Sub


    Private Sub tmrPastExamProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPastExamProtect.Tick
        Try
            tmrPastExamProtect.Enabled = False
            If IsNothing(oCurDoc2) = False Then
                Dim protectPane As Wd.TaskPane = oCurDoc2.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                If (IsNothing(protectPane) = False) Then
                    protectPane.Visible = False
                    Marshal.ReleaseComObject(protectPane)
                    protectPane = Nothing
                End If
                'oCurDoc2.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrPastExamProtect.Enabled = True
        End Try
    End Sub

    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Undo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Redo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        'Set focus to Wd object
        If oCurDoc Is Nothing Then
            Exit Sub
        End If

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
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
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
                'For DMS Hybrid Database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010

                'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'oEDocument.ShowEScannerForImages(gnPatientID, oFiles)
                oEDocument.ShowEScannerForImages(_PatientID, oFiles)
                'end modification by dipak
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        'Code Changed by Mayuri:20090918
        'In order to display Patient details on Triage Form,Replaced Patient Messages by Triage,
        'so that changes made from Tools->Patient control customization->triage will get reflected on Triage form
        ' _PatientStrip.ShowDetail(gnPatientID, gloUC_PatientStrip.enumFormName.PatientMessages)
        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        '_PatientStrip.ShowDetail(gnPatientID, gloUC_PatientStrip.enumFormName.Triage)
        _PatientStrip.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.Triage)
        'end modification by dipak 
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)
        '_PatientStrip.BringToFront()
        Me.Controls.Add(_PatientStrip)
        If _IsReadOnly Then
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.DTPEnabled = False
            End If

        End If
        ' wdMessages.BringToFront()
    End Sub
    Private Sub loadToolStrip()
        If Not tlsTriage Is Nothing Then
            tlsTriage.Dispose()
        End If

        tlsTriage = New WordToolStrip.gloWordToolStrip
        tlsTriage.Dock = DockStyle.Top
        tlsTriage.ConnectionString = GetConnectionString()
        tlsTriage.UserID = gnLoginID
        ''Integrated by Mayuri:20101021:provider sign
        tlsTriage.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsTriage.ptProvider = oclsProvider.GetPatientProviderName(_PatientID)
        tlsTriage.ptProviderId = oclsProvider.GetPatientProvider(_PatientID)
        oclsProvider.Dispose()
        oclsProvider = Nothing
        ''End 20101021
        'tlsMessages.Size = New System.Drawing.Size(940, 56)
        'tlsMessages.Height = 100
        If _IsReadOnly Then
            tlsTriage.FormType = WordToolStrip.enumControlType.TriageAddendum
            cmbTemplate.Enabled = False
            dtpTriage.Enabled = False
        Else
            tlsTriage.IsCoSignEnabled = gblnCoSignFlag
            tlsTriage.FormType = WordToolStrip.enumControlType.Triage
            cmbTemplate.Enabled = True
            dtpTriage.Enabled = True
        End If


        'Me.Controls.Add(tlsMessages)
        Me.pnlToolStrip.Controls.Add(tlsTriage)
        Me.pnlToolStrip.Size = New System.Drawing.Size(940, 56)
        pnlToolStrip.SendToBack()
        tlsTriage.Height = 100
        'If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
        '    If Not IsNothing(ogloVoice) Then
        '        ogloVoice.MyWordToolStrip = tlsTriage
        '        ShowMicroPhone()
        '    End If
        'End If
        'If gnLoginProviderID = 0 And gblnAssociatedProviderSignature And _IsReadOnly = False Then
        '    tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
        '    tlsTriage.MyToolStrip.ButtonsToHide.Remove(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        'Else
        '    tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
        '    If (tlsTriage.MyToolStrip.ButtonsToHide.Contains(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
        '        tlsTriage.MyToolStrip.ButtonsToHide.Add(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        '    End If
        'End If
        If gblnAssociatedProviderSignature And _IsReadOnly = False Then
            tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsTriage.MyToolStrip.ButtonsToHide.Remove(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            ''commented by Mayuri:20101018-This condition is checked in glotoolstrip control
            ''If user has authority to insert provider sign then only it will able to insert
            'tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            'If dt.Rows.Count > 0 Then
            '    tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = True
            'Else
            '    tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = False
            'End If
            'tlsTriage.MyToolStrip.ButtonsToHide.Remove(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        ElseIf _IsReadOnly = True Then

            tlsTriage.MyToolStrip.Items("Save").Visible = False
            If (tlsTriage.MyToolStrip.ButtonsToHide.Contains(tlsTriage.MyToolStrip.Items("Save").Name) = False) Then
                tlsTriage.MyToolStrip.ButtonsToHide.Add(tlsTriage.MyToolStrip.Items("Save").Name)
            End If
            tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsTriage.MyToolStrip.ButtonsToHide.Contains(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsTriage.MyToolStrip.ButtonsToHide.Add(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        ElseIf gblnAssociatedProviderSignature = False Then
            tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsTriage.MyToolStrip.ButtonsToHide.Contains(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsTriage.MyToolStrip.ButtonsToHide.Add(tlsTriage.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If


        End If

        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsTriage.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsTriage.MyToolStrip.ButtonsToHide.Contains(tlsTriage.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsTriage.MyToolStrip.ButtonsToHide.Add(tlsTriage.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If

            If tlsTriage.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsTriage.MyToolStrip.Items("SecureMsg").Visible = False
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
    Private Sub tlsTriage_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsTriage.ToolStripButtonClick
        Try
            If IsNothing(oCurDoc) = False Then
                InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''sanjog 20101015 

    'Private Sub loadToolStrip()
    '    If Not tlsMessages Is Nothing Then
    '        tlsMessages.Dispose()
    '        tlsMessages = Nothing
    '    End If

    '    tlsMessages = New WordToolStrip.gloWordToolStrip
    '    tlsMessages.Dock = DockStyle.Top
    '    If m_IsReadOnly Then
    '        tlsMessages.FormType = WordToolStrip.enumControlType.MessageAddendum
    '        cmbTemplate.Enabled = False
    '        dtMessage.Enabled = False
    '    Else
    '        tlsMessages.IsCoSignEnabled = gblnCoSignFlag
    '        tlsMessages.FormType = WordToolStrip.enumControlType.Messages
    '        cmbTemplate.Enabled = True
    '        dtMessage.Enabled = True
    '    End If
    '    Me.pnlWordObj.Controls.Add(tlsMessages)
    '    ShowMicrophone()
    'End Sub

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
            ' Dim sFileName As String = ExamNewDocumentName
            'Ashish added on 1st November 
            'to prevent screen from refreshing
            'Dim WDocViewType As Wd.WdViewType
            'Dim wordRefresh As New WordRefresh()
            'If IsPrintFlag Then
            '    'Ashish added on 31st October 
            '    'to prevent screen from refreshing
            '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
            '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
            'End If
            If IsNothing(wdMessages) = False AndAlso IsNothing(oWordApp) = False Then
                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdMessages, oCurDoc, oWordApp)
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
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    Try
                '        oCurDoc.Save()
                '    Catch ex1 As Exception

                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                '    End Try
                'End Try
                '  wdMessages.Save(sFileName, True, "", "")
                '      oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)


                'If Not File.Exists(sFileName) Then
                '    Try
                '        File.Copy(oCurDoc.FullName, sFileName)
                '    Catch ex As Exception
                '        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '        ex = Nothing
                '    End Try
                'End If

                'If Not File.Exists(sFileName) Then
                '    Exit Sub
                'End If


                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Try
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, txtPatient.Tag, AddressOf FAXMessage, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

                'If IsPrintFlag Then
                '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '    '    oTempDoc.Unprotect()
                '    'End If
                '    Dim oPrint As New clsPrintFAX
                '    oPrint.PrintDoc(oTempDoc, txtPatient.Tag)
                '    oPrint.Dispose()
                '    oPrint = Nothing
                'Else
                '    Call FAXMessage(myLoadWord, oTempDoc)
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

    '''' <summary>
    '''' Load the  Word Document in the Dso control
    '''' </summary>
    '''' <param name="strFileName"></param>
    '''' <param name="blnGetData"></param>
    '''' <remarks></remarks>
    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)

        If Not blnCmbSelTemplate Then
            loadToolStrip()
        End If

        If strFileName <> "" Then

            '    wdMessages.Open(strFileName)
            '  Dim oWordApp As Wd.Application = Nothing
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdMessages, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then

            Else


                If blnGetData Then
                    ObjWord = New clsWordDocument
                    ''//Mapping values for retrieving data from DB
                    objCriteria = New DocCriteria
                    objCriteria.DocCategory = enumDocCategory.Message
                    objCriteria.PatientID = txtPatient.Tag
                    objCriteria.VisitID = dtpTriage.Tag
                    objCriteria.PrimaryID = 0
                    ObjWord.DocumentCriteria = objCriteria
                    ObjWord.CurDocument = oCurDoc
                    ObjWord.DisableWordRefresh = True
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

                SetWordObjectEntry()
            End If
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
                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then

                        ' Dim om As Object = System.Reflection.Missing.Value

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    Private Sub wdMessages_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdMessages.BeforeDocumentClosed
        Try
            If (IsNothing(oWordApp)) Then
                Try
                    oWordApp = oCurDoc.Application
                Catch ex As Exception
                    UpdateVoiceLog(ex.ToString)
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End If

            If Not oWordApp Is Nothing Then
                If (ObjWord Is Nothing) Then
                    InitialiseWordObject()
                End If
                'code added by dipak 20091224 to 
                garrOpenDocument.Remove(oCurDoc.FullName)

                'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick 'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf ObjWord.OnFormClicked
                RemoveWordHandlers()

                isHandlerRemoved = True
                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    UpdateVoiceLog(ex.ToString)
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    '''' <summary>
    '''' To  implement liquid link for every form 
    '''' </summary>
    '''' <param name="Sel"></param>
    '''' <param name="Cancel"></param>
    '''' <remarks></remarks>
    'Public Sub OnFormClicked(ByVal Sel As Wd.Selection, ByRef Cancel As Boolean)
    '    Try
    '        Cancel = False
    '        Dim r As Wd.Range = Sel.Range
    '        r.SetRange(Sel.Start, Sel.End + 1)
    '        If r.FormFields.Count >= 1 Then
    '            Dim om As Object = System.Reflection.Missing.Value
    '            Dim f As Wd.FormField
    '            ' Dim o As Object = 1
    '            f = r.FormFields.Item(1)
    '            If f.Type = Wd.WdFieldType.wdFieldFormTextInput Then
    '                FieldValue = f.StatusText
    '                ''To implement liquid in same thread
    '                If Not m_IsReadOnly Then
    '                    AccessControl()
    '                    Cancel = True
    '                End If

    '            Else
    '                If f.Type = Wd.WdFieldType.wdFieldFormDropDown Then
    '                    Cancel = True
    '                Else
    '                    If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
    '                        Cancel = True
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    '''' <summary>
    '''' To open the form under same thread as we are opening the form using com object click events
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub AccessControl()
    '    If Me.InvokeRequired Then
    '        Me.Invoke(New MethodInvoker(AddressOf AccessControl))
    '    Else
    '        OpenLink(FieldValue)
    '    End If
    'End Sub

    '''' <summary>
    '''' To open the concerned form on Formfield value clicked
    '''' </summary>
    '''' <param name="strFormFieldResult"></param>
    '''' <remarks></remarks>
    'Private Sub OpenLink(ByVal strFormFieldResult As String)

    '    'Dim strTable = Split(strFormFieldResult, ".")
    '    'If strTable(1) = _ChiefComplaints Then
    '    '    GetCheifComplaints()
    '    'Else

    '    '    Select Case strTable(0)
    '    '        Case _PatientDemographics, _Contacts
    '    '            GetDemographics()
    '    '        Case _Prescription
    '    '            GetPrescription()
    '    '        Case _Medication
    '    '            GetMedication()
    '    '        Case _Vitals
    '    '            GetVitals()
    '    '        Case _History, _Narration
    '    '            GetHistory()
    '    '        Case _ProblemList
    '    '            GetProblemList()
    '    '        Case _Order
    '    '            GetOrders()
    '    '        Case _Referrals
    '    '            GetReferrals()
    '    '        Case _Diagnosis
    '    '            GetDiagnosis()
    '    '            '' 20070604
    '    '            '' No More Use in gloEMR
    '    '            'Case _Treatment
    '    '            '    GetTreatment()
    '    '        Case _ROS
    '    '            GetROS()
    '    '        Case _PatientEducation
    '    '            GetPatientEducation()
    '    '        Case _Flowsheet
    '    '            InsertFlowSheet()
    '    '        Case _Tasks
    '    '            GetTasks()

    '    '        Case _PatientGuideline
    '    '            GetPatientGuideLine()

    '    '        Case Else

    '    '    End Select
    '    'End If

    'End Sub

    Private Sub wdMessages_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdMessages.OnDocumentClosed
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'GC.Collect()  ''change against problem 00000591
            'GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub wdMessages_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdMessages.OnDocumentOpened
        ''//Assign control's document to Document object 
        oCurDoc = wdMessages.ActiveDocument
        oWordApp = oCurDoc.Application
        If Not (garrOpenDocument.Contains(oCurDoc.FullName)) Then
            garrOpenDocument.Add(oCurDoc.FullName)
        End If
        Try

            'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
            RemoveWordHandlers()

            'AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            'AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
            AddWordHandlers()

            isHandlerRemoved = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        Dim WDocViewType As Wd.WdViewType
        'Dim wordRefresh As New WordRefresh()

        Try
            TurnoffMicrophone()
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                'Ashish added on 1st October 
                'to prevent screen from refreshing

                WDocViewType = oCurDoc.ActiveWindow.View.Type
                'wordRefresh.ShowPanel(Me.pnlWordObj)
                'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
                Fill_TemplateGallery()
                'Ashish added on 31st October 
                'to prevent screen from refreshing
                
                blnCmbSelTemplate = False
                blnSelectTemplate = True
               
            Else
                wdMessages.Close()
            End If
        Catch ex As Exception
            blnCmbSelTemplate = False
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'wordRefresh.HidePanel(Me.pnlWordObj)
            'wordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
            'wordRefresh.Dispose()
            'wordRefresh = Nothing
            WDocViewType = Nothing
        End Try
    End Sub

    Private Sub btnReply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReply.Click
        Try
            If blnReply = False Then
                Call ReplyMessage()
                '' Keep Current Messageid in Reply_MsgID for Update message --> Finished
                Reply_MsgID = m_MsgID
                '' makes m_MsgID=0 bcoz Replied messages is as New Message 

                '' commented on 1/18/2006 3:26PM 
                '' m_MsgID = 0
            End If

            blnReply = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Reply, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUser.Click
        Try
            blnUserClick = True

            ''Remove ListControl if Present.
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
            ofrmList.Text = "Users"
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            If IsNothing(ofrmList) = False Then
                Try
                    ofrmList.Controls.Remove(oListUsers)
                    Try
                        RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                        RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    oListUsers.Dispose()
                    oListUsers = Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                ofrmList.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dtUsers As New DataTable()
            Dim dcId As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Description")
            dtUsers.Columns.Add(dcId)
            dtUsers.Columns.Add(dcDescription)
            Try
                If (IsNothing(ToList) = False) Then
                    ToList.Dispose()
                    ToList = Nothing
                End If
            Catch ex As Exception

            End Try
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
            cmbTo.DataSource = dtUsers
            cmbTo.ValueMember = dtUsers.Columns("ID").ColumnName
            cmbTo.DisplayMember = dtUsers.Columns("Description").ColumnName
            ofrmList.Close()

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub btnClearUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearUser.Click
        'COMMENTED BY SANJOG
        'Try
        '    Dim _userId As Int64
        '    Dim i As Int16
        '    'cmbTo.Items.Remove(CType(cmbTo.SelectedItem, myList))
        '    If cmbTo.SelectedIndex >= 0 Then


        '        If Not IsNothing(cmbTo.SelectedItem) Then
        '            If MessageBox.Show("Do you want to clear selected user?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

        '                ''commnetd on 20101028
        '                'cmbTo.DataSource = Nothing
        '                'ToList.Clear()
        '                ''End code commenetd on 20101028
        '                _userId = Convert.ToInt64(cmbTo.SelectedValue)

        '                For i = 0 To ToList.Count - 1

        '                    If (ToList(i).ID = _userId) Then

        '                        ToList.RemoveAt(i)
        '                        Exit For

        '                    End If
        '                Next
        '                If cmbTo.Items.Count = 0 Then
        '                    cmbTo.Text = ""

        '                End If



        '                Dim dtUsers As DataTable = DirectCast(cmbTo.DataSource, DataTable)
        '                dtUsers.Rows.RemoveAt(cmbTo.SelectedIndex)

        '                If cmbTo.SelectedIndex > 0 Then
        '                    cmbTo.SelectedIndex = 0
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
        ''Sanjog - Added on 20101108 to remove the items from the combobox 
        Try
            If cmbTo.SelectedIndex >= 0 Then
                Dim _userId As Int64 = 0
                Dim i As Integer
                _userId = Convert.ToInt64(cmbTo.SelectedValue)
                For i = 0 To ToList.Count - 1
                    If (ToList(i).ID = _userId) Then
                        ToList.RemoveAt(i)
                        Exit For
                    End If
                Next

                Dim dtUsers As DataTable = DirectCast(cmbTo.DataSource, DataTable)
                dtUsers.Rows.RemoveAt(cmbTo.SelectedIndex)
                If cmbTo.Items.Count = 0 Then
                    cmbTo.Text = ""
                Else
                    cmbTo.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        ''Sanjog - Added on 20101108 to remove the items from the combobox 
    End Sub

    ''' <summary>
    ''' To Implement tool strip items click for Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tlsMessages_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsTriage.ToolStripClick
        Try
            Select Case e.ClickedItem.Name
                Case "Show/Hide"
                    ShowHide_PastExam()
                Case "Mic"
                    UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Patient Triage when " & e.ClickedItem.Name & " is invoked------------")
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    UpdateVoiceLog("--------------SwitchOff Mic Completed from tblButtons_ButtonClick in Patient Triage when " & e.ClickedItem.Name & " is invoked------------")
                Case "Save"
                    TurnoffMicrophone()
                    If blnReply = False Then
                        chkIsFinished.CheckState = CheckState.Unchecked
                    End If
                    If _IsReadOnly Then
                        chkIsFinished.CheckState = CheckState.Checked
                    End If
                    Call SaveTriage()

                Case "Save & Close"
                    If blnReply = False Then
                        chkIsFinished.CheckState = CheckState.Unchecked
                    End If
                    If SaveTriage(True) = True Then
                        Call CloseMessage()
                    End If
                Case "Print"
                    TurnoffMicrophone()
                    Call PrintMessage()
                    dtpTriage.Enabled = False
                    cmbTemplate.Enabled = False

                Case "FAX"
                    bnlIsFaxOpened = True
                    TurnoffMicrophone()
                    Call GeneratePrintFaxDocument(False)
                    dtpTriage.Enabled = False
                    cmbTemplate.Enabled = False
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
                    End If
                    'case added by dipak 20100105 for ProviderSign 
                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then

                        InsertProviderSignature()

                    End If
                    'End If
                    'InsertProviderSignature()
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
                    Call CloseMessage()
                Case "Prescription"
                    TurnoffMicrophone()
                    ' MsgBox("Prescription")
                    Call GetPrescription()

                Case "OrderTemplates"
                    TurnoffMicrophone()
                    ' MsgBox("Orders")
                    Call GetOrders()
                Case "Save & Finish"
                    chkIsFinished.CheckState = CheckState.Checked
                    If SaveTriage(True) = True Then
                        Call CloseMessage()
                    End If
                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()

                Case "Add Addendum"
                    Call InsertAddendum()
                Case "Export"
                    'Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Triage", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    'Export Function for Word Docs Integrated by dipak  as on 26 oct 2010
                Case "SecureMsg"
                    If strProviderDirectAddress <> "" OrElse gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_PatientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            TurnoffMicrophone()
                            Call SendSecureMsg()
                            dtpTriage.Enabled = False
                            cmbTemplate.Enabled = False
                        End If
                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.ClinicalExchange, "Send Form Gallery using Secure Message", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdMessages, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try
        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '   Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        ' wdMessages.Close()
        'If (IsNothing(wdTemp) = False) Then
        '    Me.pnlFromTo.Controls.Remove(wdTemp)
        '    wdTemp.Dispose()
        '    wdTemp = Nothing
        'End If
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Generate, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        '  Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

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
        If (osenddox.Length > 0) Then
            If File.Exists(osenddox) Then
                ''Read Secure Messages settings and call Inbox form
                Dim ofrmSendNewMail As New InBox.NewMail(_PatientID, osenddox)
                AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromTriage

                If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                    gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                    ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                End If
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()

                RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromTriage
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
                        If _IsReadOnly = True Then
                            oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)


                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            If _IsReadOnly = True Then
                tmrDocProtect.Enabled = True
            End If

        End Try
    End Sub
    Public Sub ActivateBasicVoiceCmds(ByVal myVoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds

        'If myVoiceCol.Count > 0 Then
        '    Dim objSender As Object = Nothing
        '    Dim obje As EventArgs = Nothing
        '    'Dim objKeye As KeyEventArgs
        '    Dim objtblbtn As New ToolStripButton

        '    Select Case myVoiceCol.Item(1)

        '        Case "Finish Messages"
        '            objtblbtn.Name = "Save & Finish"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Save Messages"
        '            objtblbtn.Name = "Save"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Insert Signature"
        '            objtblbtn.Name = "Insert Sign"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Print Messages"
        '            objtblbtn.Name = "Print"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Save and Close", "Save and Close Messages"
        '            objtblbtn.Name = "Save & Close"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Fax Messages"
        '            objtblbtn.Name = "FAX"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Show Past Exam", "Hide Past Exam"
        '            objtblbtn.Name = "Show/Hide"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Close Messages"
        '            objtblbtn.Name = "Close"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Prescription"
        '            objtblbtn.Name = "Prescription"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '        Case "Orders", "Radiology Orders"
        '            objtblbtn.Name = "Orders"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsMessages_ToolStripClick(objSender, objtbl)
        '    End Select
        'End If
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateBasicVoiceCmds(myVoiceCol)
            End If
        End If
    End Sub

    'Private Sub AddBasicVoiceCommands()
    '    MessagesVoicecol.Clear()
    '    MessagesVoicecol.Add("Save Messages")
    '    MessagesVoicecol.Add("Print Messages")
    '    MessagesVoicecol.Add("Fax Messages")
    '    MessagesVoicecol.Add("Save and Close")
    '    MessagesVoicecol.Add("Save and Close Messages")
    '    MessagesVoicecol.Add("Insert Signature")
    '    MessagesVoicecol.Add("Close Messages")
    '    MessagesVoicecol.Add("Finish Messages")
    '    MessagesVoicecol.Add("Prescription")
    '    MessagesVoicecol.Add("Orders")
    '    MessagesVoicecol.Add("Radiology Orders")
    '    MessagesVoicecol.Add("Show Past Exam")
    '    MessagesVoicecol.Add("Hide Past Exam")
    'End Sub

    'Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds

    'End Sub

    'Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
    '    vVoiceMenu.Remove(1)
    '    If IsNothing(MessagesVoicecol) Then
    '        MessagesVoicecol = New DNSTools.DgnStrings
    '        Call AddBasicVoiceCommands()
    '    End If
    '    vVoiceMenu.ListSetStrings("Messages", MessagesVoicecol)
    '    vVoiceMenu.Add(1, "<Messages>", "", "")
    'End Sub

    'Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    'End Sub

    'Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    'End Sub

    'Private Sub ShowMicrophone()
    '    If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '        Try

    '            If Not _IsReadOnly Then
    '                tlsTriage.MyToolStrip.Items("Mic").Visible = True
    '                tlsTriage.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
    '            End If

    '        Catch ex As Exception

    '        End Try
    '    Else
    '        If Not _IsReadOnly Then
    '            tlsTriage.MyToolStrip.Items("Mic").Visible = False
    '        End If
    '    End If
    'End Sub

    'Private Sub TurnOffMicrophone()
    '    'code commented as logic for mic on/off has changed
    '    If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '        Try
    '            If CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
    '                CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
    '                tlsTriage.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
    '            End If

    '        Catch ex As Exception

    '        End Try
    '    Else
    '        If Not _IsReadOnly Then
    '            tlsTriage.MyToolStrip.Items("Mic").Visible = False
    '        End If
    '    End If

    'End Sub

    Private Sub frmTriage_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
        If Not oCurDoc Is Nothing Then
            If (isHandlerRemoved = False) Then

                'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                'RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
                RemoveWordHandlers()

                isHandlerRemoved = True
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
            End If
        End If
    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then


                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsTriage.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsTriage.MyToolStrip.Items("Mic").Visible = True
                        tlsTriage.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                        tlsTriage.MyToolStrip.ButtonsToHide.Remove(tlsTriage.MyToolStrip.Items("Mic").Name)
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
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsTriage.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsTriage.MyToolStrip.Items("Mic").Visible = True
                        tlsTriage.MyToolStrip.ButtonsToHide.Remove(tlsTriage.MyToolStrip.Items("Mic").Name)
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
                    If Not tlsTriage.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsTriage.MyToolStrip.Items("Mic").Visible = False
                        If tlsTriage.MyToolStrip.ButtonsToHide.Contains(tlsTriage.MyToolStrip.Items("Mic").Name) = False Then
                            tlsTriage.MyToolStrip.ButtonsToHide.Add(tlsTriage.MyToolStrip.Items("Mic").Name)
                        End If
                    End If

                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsTriage.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsTriage.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
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
                            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            ex2 = Nothing
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
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        End If
                    Next
                End If
                End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub frmTriage_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            '''''if Opened Messasges IsFinished(ReadOnly) then Exit sub
            ''If m_IsReadOnly = True Then
            ''    Exit Sub
            ''End If

            ' If blnSaved = False Then
            ''''If Record is open for only view then exit from function.
            If m_IsRecordLock Then
                Exit Sub
            End If

            If IsNothing(oCurDoc) = False Then
                '' When form closes, If User had done any change in Meassage then  
                If oCurDoc.Saved = False Then
                    Dim Result As Integer
                    Result = MessageBox.Show("Do you want to save the changes in this Triage?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If Result = DialogResult.Yes Then
                        'If blnReply = False Then
                        '    chkIsFinished.CheckState = CheckState.Unchecked
                        'End If
                        If SaveTriage() = False Then e.Cancel = True

                    ElseIf Result = DialogResult.Cancel Then
                        'Do nothing
                        e.Cancel = True
                        Exit Sub
                    ElseIf Result = DialogResult.No Then
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Triage viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                        e.Cancel = False
                    End If
                End If
            End If
            'End If
            'If (e.Cancel = False) Then
            '    Me.Visible = False
            '    Me.WindowState = FormWindowState.Normal
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
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
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Triage
        ogloVoice.MyWordToolStrip = Me.tlsTriage
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Triage"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsMessages_ToolStripClick)
    End Sub
    ''' <summary>
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Triage", "Save")
        oHashtable.Add("Print Triage", "Print")
        oHashtable.Add("Fax Triage", "Fax")
        oHashtable.Add("Save and Close", "Save and Close")
        oHashtable.Add("Save and Close Triage", "Save and Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Triage", "Close")
        oHashtable.Add("Finish Triage", "Save & Finish")
        oHashtable.Add("Prescription", "Prescription")
        oHashtable.Add("Orders", "Orders")
        oHashtable.Add("Radiology Orders", "Orders")
        oHashtable.Add("Show Past Exam", "Show/Hide")
        oHashtable.Add("Hide Past Exam", "Show/Hide")


        Return oHashtable
    End Function
    ''' <summary>
    ''' Activate Voice Commands 
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
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
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
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
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone, IWord.ShowMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
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
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And _IsReadOnly = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
    End Sub
    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property
    ''' <summary>
    ''' Initialise Voice object for Addendum
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
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Triage
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Triage"

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
        Me.Controls.Remove(ouctlgloUC_Addendum)
        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Save addendum changes to word template on triage
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

        Me.Controls.Remove(ouctlgloUC_Addendum)
        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        blnIsAddendum = False
        TurnoffMicrophone()
        If (IsNothing(ouctlgloUC_Addendum) = False) Then
            ouctlgloUC_Addendum.Dispose()
            ouctlgloUC_Addendum = Nothing
        End If
    End Sub

    Private Sub InitialiseWordObject()

        'Me.SetStyle(ControlStyles.ResizeRedraw, False)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        If IsNothing(ObjWord) Then
            ObjWord = New clsWordDocument
        End If
        ObjWord.myCallingForm = Me
        ObjWord.CurDocument = wdMessages.ActiveDocument
        If IsNothing(objCriteria) Then
            objCriteria = New DocCriteria
        End If
        'objCriteria.VisitID = GenerateVisitID(dtMessage.Value, gnPatientID)
        'objCriteria.DocCategory = enumDocCategory.Message
        'objCriteria.PatientID = txtPatient.Tag
        'objCriteria.VisitID = dtMessage.Tag
        objCriteria.PrimaryID = 0
        ObjWord.DocumentCriteria = objCriteria
        ObjWord.CurDocument = oCurDoc
    End Sub
    Private Sub oWordApp_WindowBeforeDoubleClick(ByVal Sel As Microsoft.Office.Interop.Word.Selection, ByRef Cancel As Boolean) 'Handles oWordApp.WindowBeforeDoubleClick
        If tmrDocProtect.Enabled = False Then '' Protected conditon 
            'need to check of read only triage
            Cancel = False
            ' If (ObjWord Is Nothing) Then
            InitialiseWordObject()
            'End If
            'call of comman event for liquid link
            ObjWord.PatientId = _PatientID
            ObjWord.Get_PatientDetails(_PatientID)
            ObjWord.OnFormClicked(Sel, Cancel)
        End If
    End Sub
    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientMiddleName = Convert.ToString(dtPatient.Rows(0)("sMiddleName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
            If IsNothing(oPatient) = False Then
                oPatient.Dispose()
                oPatient = Nothing
            End If

        End Try
    End Sub
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromTriage(ByVal PatientID As Int64)
    ' Public Event EvntGenerateCDAFromTriage(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromTriage(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromTriage(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
#End Region
End Class
