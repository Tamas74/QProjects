Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices


Public Class frmMessages

    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements IHotKey
    Implements gloVoice
    Implements IWord
    Implements IPatientContext

    Private ogloVoice As ClsVoice 'Voice Code added by Supriya 29/05/2008

    Private blnIsAddendum As Boolean 'Addendum user control defined here     
    Private WithEvents ouctlgloUC_Addendum As gloUC_Addendum

    Public myCaller As frmVWMessages 'set when caller form is frmVWMessages
    Public MyDayCaller As frmMyDay

    Public myCallerMain As MainMenu 'Sets when Caller Form is MainMenu
    Public CancelClick As Boolean
    Public IsModify As Boolean = False
    Public m_IsReadOnly As Boolean = False 'To check if message is finished or does not belong to current user
    Public Shared IsOpen As Boolean = False 'If Users are to fill in Custum grid =TRUE  If Patient are to fill in Custum grid =FALSE    
    ''Bug #70506: 00000722: Patient messages issue.
    Public blnReply As Boolean = False 'On Reply Button Click blnReply = True this, we r doing here so that even if User click on Repply Btn it does not make any affect
    Private blnCmbSelTemplate As Boolean = False
    Private blnSelectTemplate As Boolean

    Private Reply_MsgID As Long 'The message ID for which User is Replying is stored in Reply_MsgID Before Making m_MsgID =0  make Reply_MsgID = m_MsgID 
    Public m_MsgID As Long
    Private m_UserID As Long
    Public m_PatientID As Long

    Public Shared m_MsgDate As Date 'For Message Date  
    Private m_Flag As Int16 'Value of this flag = 1 when Message form is Opend from Main Menu 'else i.e if it open from its View Form then flag=0

    Public Shared ProviderUserID As Long
    Private objclsMessage As New clsMessage
    Private WithEvents dgCustomGrid As CustomTask
    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oCurDoc2 As Wd.Document

    '  Private WithEvents oTempDoc As Wd.Document
    Dim UserCount As Integer
    Private WithEvents oWordApp As Wd.Application
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    Private WithEvents tlsMessages As WordToolStrip.gloWordToolStrip
    Friend WithEvents wdPastMessages As AxDSOFramer.AxFramerControl
    Dim objCriteria As DocCriteria
    Private ObjWord As New clsWordDocument

    Dim myidx As Int32
    Private Col_Check As Integer = 5
    Private Col_UserID As Integer = 0
    Private Col_LoginName As Integer = 1
    Private Col_Column1 As Integer = 2
    Private Col_Column2 As Integer = 3
    Private Col_ProviderID As Integer = 4
    Private m_IsRecordLock As Boolean = False
    Private blnSignClick As Boolean = False


    Dim ofrmList As frmViewListControl
    Private oListUsers As gloListControl.gloListControl
    Private ToList As gloGeneralItem.gloItems = Nothing

    Friend WithEvents cmbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label


    Dim strTemp As String
    Public Shared strAddendum As String 'Message Addendum from Message Addendum Form  
    Private ImagePath As String

    'variable for date of service
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String

    'Friend WithEvents UiPanelManager1_PatientMessages As Janus.Windows.UI.Dock.UIPanelManager
    Public WithEvents uiPanSplitScreen_PatientMessages As Janus.Windows.UI.Dock.UIPanelGroup
    Dim clsSplit_PatientMessages As New gloEMRGeneralLibrary.clsSplitScreen

    Dim dtPriority As DataTable
    Private bnlIsFaxOpened As Boolean

    Dim tooltipsub As ToolTip
    '18-Mar-14 Aniket: Resolving Memory Leaks
    Dim bIncludeAllUsers As Boolean = False
    Private _dtTemplates As DataTable
    Public Property dtTemplates As DataTable
        Get
            Return _dtTemplates
        End Get
        Set(value As DataTable)
            _dtTemplates = value
        End Set
    End Property



    Enum enumMessagePriority
        None = 0
        Low = 1
        Normal = 2
        High = 3
    End Enum

    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

#Region " Windows Controls "
    Friend WithEvents wdMessages As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlFromTo As System.Windows.Forms.Panel
    Friend WithEvents cmbTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtMessage As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents btnUser As System.Windows.Forms.Button
    '  Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
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
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnAddFields As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents chkReminderforUnscheduledCare As System.Windows.Forms.CheckBox
    Friend WithEvents cmbReminderforUnscheduledCare As System.Windows.Forms.ComboBox
    Friend WithEvents chkIsFinished As System.Windows.Forms.CheckBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents lblPatientPrefers As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtSub As System.Windows.Forms.TextBox
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Call loadPatientStrip()
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        m_PatientID = PatientID
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal MsgID As Long, ByVal UserID As Long, ByVal dtMsgDate As Date, ByVal nPaitentID As Long, Optional ByVal Flag As Int16 = 0, Optional ByVal blnIsReadOnly As Boolean = False, Optional ByVal IsRecordLock As Boolean = False)
        MyBase.New()

        m_MsgID = MsgID
        m_UserID = UserID
        m_Flag = Flag

        'Problem 00000024 : date column displays the date that the message was last "Saved" rather than "Created." 
        m_MsgDate = Format(dtMsgDate, "MM/dd/yyyy hh:mm tt") '& " " & Format(dtMsgDate, "Medium Time")   'Massage Date

        m_IsReadOnly = blnIsReadOnly
        m_IsRecordLock = IsRecordLock
        blnReply = False
        m_PatientID = nPaitentID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Problem 00000024 : date column displays the date that the message was last "Saved" rather than "Created." 
        Label14.Text = "Message Date :"
        Call loadPatientStrip()
        'Add any initialization after the InitializeComponent() call
    End Sub


#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    Private Shared frm As frmMessages

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
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
                    If (IsNothing(dtMessage) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtMessage)
                        Catch ex As Exception

                        End Try


                        dtMessage.Dispose()
                        dtMessage = Nothing
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
                If IsNothing(ogloVoice) = False Then
                    ogloVoice.Dispose() : ogloVoice = Nothing
                End If

                m_PatientID = Nothing
                blnIsAddendum = Nothing

                If IsNothing(ouctlgloUC_Addendum) = False Then
                    If (Me.Controls.Contains(ouctlgloUC_Addendum)) Then
                        Me.Controls.Remove(ouctlgloUC_Addendum)
                    End If

                    ouctlgloUC_Addendum.Dispose() : ouctlgloUC_Addendum = Nothing
                End If

                ImagePath = Nothing
                CancelClick = Nothing
                IsModify = Nothing
                IsOpen = Nothing
                blnReply = Nothing
                blnCmbSelTemplate = Nothing
                blnSelectTemplate = Nothing
                Reply_MsgID = Nothing
                m_MsgID = Nothing
                m_UserID = Nothing
                m_MsgDate = Nothing
                m_Flag = Nothing
                m_IsReadOnly = Nothing
                strAddendum = Nothing
                ProviderUserID = Nothing

                If IsNothing(objclsMessage) = False Then
                    objclsMessage.Dispose() : objclsMessage = Nothing
                End If

                'Dim UserCount = Nothing
                If (IsNothing(objCriteria) = False) Then
                    objCriteria.Dispose()
                    objCriteria = Nothing
                End If
               
                ObjWord = Nothing

                myidx = Nothing
                Col_Check = Nothing
                Col_UserID = Nothing
                Col_LoginName = Nothing
                Col_Column1 = Nothing
                Col_Column2 = Nothing
                Col_ProviderID = Nothing
                m_IsRecordLock = Nothing
                blnSignClick = Nothing

                Dim strTemp = Nothing

                If IsNothing(ToList) = False Then
                    ToList.Clear()
                    ToList.Dispose() : ToList = Nothing
                End If



                strPatientCode = Nothing
                strPatientFirstName = Nothing
                strPatientLastName = Nothing
                strPatientDOB = Nothing
                strPatientAge = Nothing
                strPatientGender = Nothing
                strPatientMaritalStatus = Nothing

                If IsNothing(dtPriority) = False Then
                    dtPriority.Dispose() : dtPriority = Nothing
                End If






            End If
        End If

        '08-May-13 Aniket: Resolving Memory Leaks
        '08-May-13 Aniket: Dispose cannot be called here
        'frm.Dispose()
        frm = Nothing
        Me.blnDisposed = True

    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue to prevent finalization code for this object from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal MsgID As Long, ByVal UserID As Long, ByVal dtMsgDate As Date, ByVal PatientID As Long, Optional ByVal Flag As Int16 = 0, Optional ByVal blnIsReadOnly As Boolean = False, Optional ByVal IsRecordLock As Boolean = False) As frmMessages
        Try
            IsOpen = False
            For Each f As Form In Application.OpenForms
                If f.Name = "frmMessages" Then
                    If (CType(f, frmMessages).m_MsgID = MsgID) And (CType(f, frmMessages).m_PatientID = PatientID) Then
                        IsOpen = True
                        frm = f
                    End If
                End If
            Next
            If (IsOpen = False) Then
                frm = New frmMessages(MsgID, UserID, dtMsgDate, PatientID, Flag, blnIsReadOnly, IsRecordLock)
            End If
        Finally
        End Try
        Return frm
    End Function

#End Region

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMessages))
        Me.pnlExtraTop = New System.Windows.Forms.Panel()
        Me.pnlExtraBottom = New System.Windows.Forms.Panel()
        Me.btnFAX = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.pnlWordObj = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.wdMessages = New AxDSOFramer.AxFramerControl()
        Me.pnlcustomTask = New System.Windows.Forms.Panel()
        Me.pnlFromTo = New System.Windows.Forms.Panel()
        Me.txtSub = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblPatientPrefers = New System.Windows.Forms.Label()
        Me.chkIsFinished = New System.Windows.Forms.CheckBox()
        Me.txtPatient = New System.Windows.Forms.TextBox()
        Me.chkReminderforUnscheduledCare = New System.Windows.Forms.CheckBox()
        Me.cmbReminderforUnscheduledCare = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnReply = New System.Windows.Forms.Button()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.cmbTo = New System.Windows.Forms.ComboBox()
        Me.cmbTemplate = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.btnClearUser = New System.Windows.Forms.Button()
        Me.btnUser = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnAddFields = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbPriority = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtMessage = New System.Windows.Forms.DateTimePicker()
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.pnlPastExam = New System.Windows.Forms.Panel()
        Me.pnlExam1 = New System.Windows.Forms.Panel()
        Me.pnlPastExamView = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.wdPastMessages = New AxDSOFramer.AxFramerControl()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dgExams = New gloEMR.clsDataGrid()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.tmrPastExamProtect = New System.Windows.Forms.Timer(Me.components)
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlExtraBottom.SuspendLayout()
        Me.pnlWordObj.SuspendLayout()
        CType(Me.wdMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFromTo.SuspendLayout()
        Me.pnlPastExam.SuspendLayout()
        Me.pnlExam1.SuspendLayout()
        Me.pnlPastExamView.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.wdPastMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlExtraTop
        '
        Me.pnlExtraTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlExtraTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlExtraTop.Name = "pnlExtraTop"
        Me.pnlExtraTop.Size = New System.Drawing.Size(1276, 0)
        Me.pnlExtraTop.TabIndex = 3
        '
        'pnlExtraBottom
        '
        Me.pnlExtraBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlExtraBottom.Controls.Add(Me.btnFAX)
        Me.pnlExtraBottom.Controls.Add(Me.btnPrint)
        Me.pnlExtraBottom.Controls.Add(Me.btnCancel)
        Me.pnlExtraBottom.Controls.Add(Me.btnSend)
        Me.pnlExtraBottom.Location = New System.Drawing.Point(0, 0)
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
        Me.pnlWordObj.Location = New System.Drawing.Point(213, 124)
        Me.pnlWordObj.Name = "pnlWordObj"
        Me.pnlWordObj.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlWordObj.Size = New System.Drawing.Size(1063, 571)
        Me.pnlWordObj.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 567)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1058, 1)
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
        Me.Label6.Size = New System.Drawing.Size(1, 567)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1059, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 567)
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
        Me.Label8.Size = New System.Drawing.Size(1060, 1)
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
        Me.wdMessages.Size = New System.Drawing.Size(1060, 568)
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
        Me.pnlFromTo.Controls.Add(Me.txtSub)
        Me.pnlFromTo.Controls.Add(Me.Label26)
        Me.pnlFromTo.Controls.Add(Me.lblPatientPrefers)
        Me.pnlFromTo.Controls.Add(Me.chkIsFinished)
        Me.pnlFromTo.Controls.Add(Me.txtPatient)
        Me.pnlFromTo.Controls.Add(Me.chkReminderforUnscheduledCare)
        Me.pnlFromTo.Controls.Add(Me.cmbReminderforUnscheduledCare)
        Me.pnlFromTo.Controls.Add(Me.Label25)
        Me.pnlFromTo.Controls.Add(Me.Label2)
        Me.pnlFromTo.Controls.Add(Me.Label9)
        Me.pnlFromTo.Controls.Add(Me.Label10)
        Me.pnlFromTo.Controls.Add(Me.Label11)
        Me.pnlFromTo.Controls.Add(Me.btnReply)
        Me.pnlFromTo.Controls.Add(Me.txtFrom)
        Me.pnlFromTo.Controls.Add(Me.lblFrom)
        Me.pnlFromTo.Controls.Add(Me.cmbTo)
        Me.pnlFromTo.Controls.Add(Me.cmbTemplate)
        Me.pnlFromTo.Controls.Add(Me.Label3)
        Me.pnlFromTo.Controls.Add(Me.lblTo)
        Me.pnlFromTo.Controls.Add(Me.btnClearUser)
        Me.pnlFromTo.Controls.Add(Me.btnUser)
        Me.pnlFromTo.Controls.Add(Me.btnRefresh)
        Me.pnlFromTo.Controls.Add(Me.btnAddFields)
        Me.pnlFromTo.Controls.Add(Me.Label24)
        Me.pnlFromTo.Controls.Add(Me.cmbPriority)
        Me.pnlFromTo.Controls.Add(Me.Label4)
        Me.pnlFromTo.Controls.Add(Me.dtMessage)
        Me.pnlFromTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFromTo.Location = New System.Drawing.Point(213, 0)
        Me.pnlFromTo.Name = "pnlFromTo"
        Me.pnlFromTo.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlFromTo.Size = New System.Drawing.Size(1063, 124)
        Me.pnlFromTo.TabIndex = 0
        '
        'txtSub
        '
        Me.txtSub.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSub.ForeColor = System.Drawing.Color.Black
        Me.txtSub.Location = New System.Drawing.Point(468, 40)
        Me.txtSub.MaxLength = 500
        Me.txtSub.Name = "txtSub"
        Me.txtSub.Size = New System.Drawing.Size(337, 22)
        Me.txtSub.TabIndex = 37
        '
        'Label26
        '
        Me.Label26.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(409, 44)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(57, 14)
        Me.Label26.TabIndex = 36
        Me.Label26.Text = "Subject :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientPrefers
        '
        Me.lblPatientPrefers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPatientPrefers.AutoSize = True
        Me.lblPatientPrefers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientPrefers.ForeColor = System.Drawing.Color.Red
        Me.lblPatientPrefers.Location = New System.Drawing.Point(50, 76)
        Me.lblPatientPrefers.Name = "lblPatientPrefers"
        Me.lblPatientPrefers.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientPrefers.TabIndex = 34
        Me.lblPatientPrefers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkIsFinished
        '
        Me.chkIsFinished.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkIsFinished.Location = New System.Drawing.Point(1309, 15)
        Me.chkIsFinished.Name = "chkIsFinished"
        Me.chkIsFinished.Size = New System.Drawing.Size(47, 18)
        Me.chkIsFinished.TabIndex = 33
        Me.chkIsFinished.Text = "Is Finished"
        Me.chkIsFinished.Visible = False
        '
        'txtPatient
        '
        Me.txtPatient.Enabled = False
        Me.txtPatient.ForeColor = System.Drawing.Color.Black
        Me.txtPatient.Location = New System.Drawing.Point(1210, 13)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(80, 22)
        Me.txtPatient.TabIndex = 32
        Me.txtPatient.Visible = False
        '
        'chkReminderforUnscheduledCare
        '
        Me.chkReminderforUnscheduledCare.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkReminderforUnscheduledCare.Checked = True
        Me.chkReminderforUnscheduledCare.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReminderforUnscheduledCare.Location = New System.Drawing.Point(468, 69)
        Me.chkReminderforUnscheduledCare.Name = "chkReminderforUnscheduledCare"
        Me.chkReminderforUnscheduledCare.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkReminderforUnscheduledCare.Size = New System.Drawing.Size(198, 18)
        Me.chkReminderforUnscheduledCare.TabIndex = 31
        Me.chkReminderforUnscheduledCare.Text = "Reminder for Unscheduled Care"
        Me.chkReminderforUnscheduledCare.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbReminderforUnscheduledCare
        '
        Me.cmbReminderforUnscheduledCare.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbReminderforUnscheduledCare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbReminderforUnscheduledCare.ForeColor = System.Drawing.Color.Black
        Me.cmbReminderforUnscheduledCare.Location = New System.Drawing.Point(668, 67)
        Me.cmbReminderforUnscheduledCare.Name = "cmbReminderforUnscheduledCare"
        Me.cmbReminderforUnscheduledCare.Size = New System.Drawing.Size(137, 22)
        Me.cmbReminderforUnscheduledCare.TabIndex = 30
        '
        'Label25
        '
        Me.Label25.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(8, 46)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(14, 14)
        Me.Label25.TabIndex = 25
        Me.Label25.Text = "*"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1058, 1)
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
        Me.Label9.Size = New System.Drawing.Size(1, 117)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1059, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 117)
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
        Me.Label11.Size = New System.Drawing.Size(1060, 1)
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
        Me.btnReply.Location = New System.Drawing.Point(256, 12)
        Me.btnReply.Name = "btnReply"
        Me.btnReply.Size = New System.Drawing.Size(55, 24)
        Me.btnReply.TabIndex = 2
        Me.btnReply.Text = " Reply"
        Me.btnReply.UseVisualStyleBackColor = False
        Me.btnReply.Visible = False
        '
        'txtFrom
        '
        Me.txtFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFrom.ForeColor = System.Drawing.Color.Black
        Me.txtFrom.Location = New System.Drawing.Point(52, 13)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(201, 22)
        Me.txtFrom.TabIndex = 1
        '
        'lblFrom
        '
        Me.lblFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFrom.AutoSize = True
        Me.lblFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFrom.Location = New System.Drawing.Point(7, 17)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblFrom.TabIndex = 14
        Me.lblFrom.Text = "From :"
        '
        'cmbTo
        '
        Me.cmbTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTo.DropDownWidth = 200
        Me.cmbTo.ForeColor = System.Drawing.Color.Black
        Me.cmbTo.Location = New System.Drawing.Point(52, 42)
        Me.cmbTo.Name = "cmbTo"
        Me.cmbTo.Size = New System.Drawing.Size(201, 22)
        Me.cmbTo.TabIndex = 5
        '
        'cmbTemplate
        '
        Me.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplate.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplate.Location = New System.Drawing.Point(468, 13)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(337, 22)
        Me.cmbTemplate.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(398, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Template :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTo
        '
        Me.lblTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTo.AutoSize = True
        Me.lblTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTo.Location = New System.Drawing.Point(19, 46)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(30, 14)
        Me.lblTo.TabIndex = 1
        Me.lblTo.Text = "To :"
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
        Me.btnClearUser.Location = New System.Drawing.Point(282, 42)
        Me.btnClearUser.Name = "btnClearUser"
        Me.btnClearUser.Size = New System.Drawing.Size(22, 22)
        Me.btnClearUser.TabIndex = 6
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
        Me.btnUser.Location = New System.Drawing.Point(257, 42)
        Me.btnUser.Name = "btnUser"
        Me.btnUser.Size = New System.Drawing.Size(22, 22)
        Me.btnUser.TabIndex = 5
        Me.btnUser.TextAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnUser.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.Location = New System.Drawing.Point(1125, 43)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(20, 20)
        Me.btnRefresh.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh")
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnAddFields
        '
        Me.btnAddFields.BackgroundImage = CType(resources.GetObject("btnAddFields.BackgroundImage"), System.Drawing.Image)
        Me.btnAddFields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddFields.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddFields.FlatAppearance.BorderSize = 0
        Me.btnAddFields.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAddFields.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAddFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddFields.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddFields.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddFields.Location = New System.Drawing.Point(1102, 43)
        Me.btnAddFields.Name = "btnAddFields"
        Me.btnAddFields.Size = New System.Drawing.Size(20, 20)
        Me.btnAddFields.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.btnAddFields, "Add Fields")
        Me.btnAddFields.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(930, 46)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(52, 14)
        Me.Label24.TabIndex = 24
        Me.Label24.Text = "Priority :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPriority
        '
        Me.cmbPriority.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPriority.ForeColor = System.Drawing.Color.Black
        Me.cmbPriority.Location = New System.Drawing.Point(986, 42)
        Me.cmbPriority.Name = "cmbPriority"
        Me.cmbPriority.Size = New System.Drawing.Size(112, 22)
        Me.cmbPriority.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(891, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 14)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Message Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtMessage
        '
        Me.dtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtMessage.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtMessage.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtMessage.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtMessage.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtMessage.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtMessage.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtMessage.Location = New System.Drawing.Point(986, 13)
        Me.dtMessage.Name = "dtMessage"
        Me.dtMessage.Size = New System.Drawing.Size(159, 22)
        Me.dtMessage.TabIndex = 4
        '
        'tmrDocProtect
        '
        '
        'pnlPastExam
        '
        Me.pnlPastExam.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlPastExam.Controls.Add(Me.pnlExam1)
        Me.pnlPastExam.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPastExam.Location = New System.Drawing.Point(0, 0)
        Me.pnlPastExam.Name = "pnlPastExam"
        Me.pnlPastExam.Size = New System.Drawing.Size(210, 695)
        Me.pnlPastExam.TabIndex = 1
        Me.pnlPastExam.Visible = False
        '
        'pnlExam1
        '
        Me.pnlExam1.Controls.Add(Me.pnlPastExamView)
        Me.pnlExam1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExam1.Location = New System.Drawing.Point(0, 0)
        Me.pnlExam1.Name = "pnlExam1"
        Me.pnlExam1.Size = New System.Drawing.Size(210, 695)
        Me.pnlExam1.TabIndex = 0
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
        Me.pnlPastExamView.Size = New System.Drawing.Size(210, 695)
        Me.pnlPastExamView.TabIndex = 0
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
        Me.Panel3.Size = New System.Drawing.Size(210, 490)
        Me.Panel3.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 486)
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
        Me.Label13.Size = New System.Drawing.Size(1, 486)
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
        Me.wdPastMessages.Size = New System.Drawing.Size(206, 486)
        Me.wdPastMessages.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(209, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 486)
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
        Me.Panel2.TabIndex = 0
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
        Me.dgExams.TabIndex = 0
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
        Me.Splitter2.Location = New System.Drawing.Point(210, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 695)
        Me.Splitter2.TabIndex = 8
        Me.Splitter2.TabStop = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1276, 5)
        Me.pnlToolStrip.TabIndex = 40
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlWordObj)
        Me.pnlMain.Controls.Add(Me.pnlFromTo)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlPastExam)
        Me.pnlMain.Controls.Add(Me.pnlExtraBottom)
        Me.pnlMain.Controls.Add(Me.pnlExtraTop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 5)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1276, 695)
        Me.pnlMain.TabIndex = 13
        '
        'frmMessages
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1276, 700)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMessages"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Messages"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlExtraBottom.ResumeLayout(False)
        Me.pnlWordObj.ResumeLayout(False)
        CType(Me.wdMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFromTo.ResumeLayout(False)
        Me.pnlFromTo.PerformLayout()
        Me.pnlPastExam.ResumeLayout(False)
        Me.pnlExam1.ResumeLayout(False)
        Me.pnlPastExamView.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.wdPastMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        MyBase.OnClosed(e)
    End Sub

    Private Sub frmMessages_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Try
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Me.ResumeLayout()

            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.ShowMicroPhone()
                End If
            End If

            If Not IsNothing(oCurDoc) Then

                'oCurDoc = wdMessages.ActiveDocument                
                '   oWordApp = oCurDoc.Application

                If m_IsReadOnly = False Then
                    If (ObjWord Is Nothing) Then
                        InitialiseWordObject()
                    End If
                    Try
                        RemoveWordHandlers()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    AddWordHandlers()
                    isHandlerRemoved = False
                End If

                oCurDoc.ActiveWindow.SetFocus()

                'Added by Amit document is protected or message is finished then skipping below code bug 42027 7020
                If m_IsReadOnly = False Then
                    oCurDoc.FormFields.Shaded = False
                End If
            End If
            If Not IsNothing(uiPanSplitScreen_PatientMessages) Then
                If Not IsNothing(uiPanSplitScreen_PatientMessages.Parent) Then
                    If uiPanSplitScreen_PatientMessages.Parent.Text = "Split Screen" Then
                        uiPanSplitScreen_PatientMessages.Parent.Visible = True
                    ElseIf uiPanSplitScreen_PatientMessages.Text = "Split Screen" Then
                        uiPanSplitScreen_PatientMessages.Visible = True
                    End If
                End If
            End If

            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmMessages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateLog("Start: Load")
        Me.SuspendLayout()
        cmbTemplate.Focus()
       
        pnlFromTo.AutoScroll = True

        Dim dt As DataTable = Nothing
        Try

            txtFrom.Enabled = False
            txtFrom.BackColor = SystemColors.Window
            txtFrom.ForeColor = Color.Black
            'Current Login Name who is sending Message

            dtMessage.Format = DateTimePickerFormat.Custom
            dtMessage.CustomFormat = "MM/dd/yyyy hh:mm tt"
            dtMessage.Value = Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")

            'SUDHIR 20090622 '' MESSAGE PRIORITY

            cmbPriority.DataSource = Nothing
            cmbPriority.Items.Clear()
            If Not IsNothing(dtPriority) Then ''slr free prev memory
                dtPriority.Dispose()
                dtPriority = Nothing
            End If
            dtPriority = New DataTable
            Dim dtRow As DataRow

            dtPriority.Columns.Add("Priority")
            dtPriority.Columns.Add("ID")

            dtRow = dtPriority.NewRow
            dtRow("Priority") = enumMessagePriority.Low
            dtRow("ID") = enumMessagePriority.Low.GetHashCode
            dtPriority.Rows.Add(dtRow)

            dtRow = dtPriority.NewRow
            dtRow("Priority") = enumMessagePriority.Normal
            dtRow("ID") = enumMessagePriority.Normal.GetHashCode
            dtPriority.Rows.Add(dtRow)

            dtRow = dtPriority.NewRow
            dtRow("Priority") = enumMessagePriority.High
            dtRow("ID") = enumMessagePriority.High.GetHashCode
            dtPriority.Rows.Add(dtRow)

            cmbPriority.DataSource = dtPriority
            cmbPriority.DisplayMember = "Priority"
            cmbPriority.ValueMember = "ID"

            cmbPriority.SelectedIndex = 0
            
            'END SUDHIR

            If Not IsNothing(tooltipsub) Then
                Try
                    RemoveHandler tooltipsub.Popup, AddressOf toolTip1_Popup
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                tooltipsub.Dispose()
                tooltipsub = Nothing
            End If
            tooltipsub = New ToolTip()

            AddHandler tooltipsub.Popup, AddressOf toolTip1_Popup

            'To fill Templates of type 'Messages to ComboBox
            Call FillTemplateName()
            Call FillReminderforUnscheduledCare()
            'Call Get_PatientDetails()

            If m_MsgID = 0 Then
                'Add new Messages 
                'If add then login name is from 
                txtFrom.Text = gstrLoginName
                txtFrom.Tag = m_UserID
               
                blnSelectTemplate = True
                Fill_TemplateGallery()
            Else
                'For Update Existing Message
                blnSelectTemplate = True
                'When Message form Opened from MainMenu, Only then 'Reply' Button is Visible

                If m_Flag = 1 Then
                    btnReply.Visible = True
                End If

                'User_MST.nUserID ,User_MST.sLoginName, Patient.nPatientID , ISNULL(Patient.sFirstName,'')+Space(1) + ISNULL(Patient.sLastName,'') , 
                'Message.nMessageID , Message.dtMsgDate = 5 ,  Message.bIsFinished =6 , Message.sResult =7, Message.nTemplateID=8

                dt = objclsMessage.SelectMessage(m_MsgID, 0, m_MsgDate)

                If IsNothing(ToList) = False Then
                    ToList.Clear()
                    ToList.Dispose() : ToList = Nothing
                End If

                ToList = New gloGeneralItem.gloItems()

                If IsNothing(dt) = False Then

                    If dt.Rows.Count > 0 Then

                        Dim i As Integer
                        Dim blnMsgToLoginUser As Boolean = False

                        cmbTo.Items.Clear()

                        For i = 0 To dt.Rows.Count - 1
                            'Dim blnUserExists As Boolean = False
                            'If (cmbTo.FindStringExact(dt.Rows(i)(1).ToString()) <> -1) Then
                            '    blnUserExists = True
                            'End If
                            'If blnUserExists = False Then
                            Dim objMylist As New myList(dt.Rows(i)(0), dt.Rows(i)(1))
                            cmbTo.Items.Add(objMylist)
                            objMylist = Nothing
                            'End If

                            '20071025 Mahesh IF the message is send to current Login User then only set 'blnMsgToLoginUser' flag True
                            If dt.Rows(i)(0) = gnLoginID Then
                                blnMsgToLoginUser = True
                            End If
                            Dim ToItem As gloGeneralItem.gloItem
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = Convert.ToInt64(dt.Rows(i)(0))
                            ToItem.Description = Convert.ToString(dt.Rows(i)(1))
                            ToItem.bGloCollect = Convert.ToBoolean(dt.Rows(i)("bIsGloCollect"))
                            ToList.Add(ToItem)
                            ToItem.Dispose()
                            ToItem = Nothing
                        Next



                        '20071025 Mahesh IF the message is send to current Login User then only Make 'Reply' Button Visible Other wise make is InVisible
                        If blnMsgToLoginUser = True Then
                            btnReply.Visible = True
                        Else
                            btnReply.Visible = False
                        End If

                        'To Select the 1st User
                        If cmbTo.Items.Count > 0 Then
                            cmbTo.SelectedIndex = 0
                        End If
                        ''                         
                        'txtPatient.Tag = dt.Rows(0)(2) 'PatientID
                        'txtPatient.Text = dt.Rows(0)(3) 'PatientName

                        'Change the Patient Details
                        strPatientCode = dt.Rows(0)(11) 'PatientCode

                        m_MsgID = dt.Rows(0)(4) 'msgID

                        dtMessage.Value = Format(dt.Rows(0)(5), "MM/dd/yyyy") & " " & Format(dt.Rows(0)(5), "Medium Time")   'MsgDate

                        If dt.Rows(0)(6) = True Then   '' IsFinished
                            'If Messages Are Finished then there are some resriction to Access that Message
                            m_IsReadOnly = True

                            'if Message is Finished Only then Addendum could add
                            chkIsFinished.Checked = True

                            btnSend.Enabled = False
                            btnSend.BackColor = Color.Gray
                            '20071025 Mahesh IF the message is Finished then 'Reply' Button InVisible
                            btnReply.Visible = False
                        Else
                            chkIsFinished.Checked = False
                        End If

                        cmbTemplate.DropDownStyle = ComboBoxStyle.Simple
                        cmbTemplate.Text = dt.Rows(0)("sTemplateName")
                        strTemp = dt.Rows(0)("sTemplateName")

                        If Not IsDBNull(dt.Rows(0)(9)) Then
                            txtFrom.Tag = dt.Rows(0)(9) 'user ID
                        End If

                        If Not IsDBNull(dt.Rows(0)(10)) Then
                            txtFrom.Text = dt.Rows(0)(10) 'User name
                        End If

                        If dt.Rows(0)("nPriority") > 0 Then
                            cmbPriority.SelectedValue = dt.Rows(0)("nPriority")
                        Else
                            cmbPriority.SelectedValue = 1
                        End If

                        If dt.Rows(0)("bisUnscheduledCare") = True Then
                            chkReminderforUnscheduledCare.Checked = True
                        Else
                            chkReminderforUnscheduledCare.Checked = False
                        End If

                        If dt.Rows(0)("nCommunicationType") > 0 Then
                            cmbReminderforUnscheduledCare.SelectedValue = dt.Rows(0)("nCommunicationType")
                            'Else
                            '    cmbReminderforUnscheduledCare.SelectedValue = -1
                        End If
                        txtSub.Text = dt.Rows(0)("Subject")

                        'image Object
                        If IsDBNull(dt.Rows(0)(7)) = False Then
                            ''slr free prev memory
                            ObjWord = Nothing
                            ObjWord = New clsWordDocument
                            Dim strFileName As String
                            strFileName = ExamNewDocumentName
                            strFileName = ObjWord.GenerateFile(dt.Rows(0)(7), strFileName)
                            ObjWord = Nothing
                            LoadWordUserControl(strFileName, False)
                            'Set the Start postion of the cursor in documents
                            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                            Call SetWordObjectEntry()
                            oCurDoc.Saved = True
                        End If
                    End If
                End If
            End If

            'If the message is open for modification from dashbord then disable date and Template combobox
            If IsModify = True Then
                dtMessage.Enabled = False
                cmbTemplate.Enabled = False
            End If

            If m_IsReadOnly Then
                cmbTo.Enabled = False
                btnUser.Enabled = False
                btnClearUser.Enabled = False
                cmbPriority.Enabled = False
            End If

            pnlcustomTask.Visible = False
            If m_IsRecordLock Then
                '20090827 Added by Mayuri
                If tlsMessages.MyToolStrip.Items.ContainsKey("Save") = True Then
                    tlsMessages.MyToolStrip.Items("Save").Enabled = False
                End If
                If tlsMessages.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
                    tlsMessages.MyToolStrip.Items("Save & Close").Enabled = False
                End If
                If tlsMessages.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
                    tlsMessages.MyToolStrip.Items("Save & Finish").Enabled = False
                End If
                If tlsMessages.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
                    tlsMessages.MyToolStrip.Items("Add Addendum").Enabled = False
                End If

            End If
            If m_IsRecordLock = True Or m_IsReadOnly = True Then
                txtSub.ReadOnly = True
            End If

            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            'slr free prev memory
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If
            'slr free prev memory
            ObjWord = Nothing
            objCriteria = New DocCriteria
            ObjWord = New clsWordDocument
            clsSplit_PatientMessages.clsUCLabControl = New gloUserControlLibrary.gloUC_TransactionHistory()
            clsSplit_PatientMessages.clsPatientExams = New clsPatientExams()
            clsSplit_PatientMessages.clsPatientLetters = New clsPatientLetters()
            clsSplit_PatientMessages.clsPatientMessages = New clsMessage()
            clsSplit_PatientMessages.clsNurseNotes = New clsNurseNotes()
            clsSplit_PatientMessages.clsHistory = New clsPatientHistory()
            clsSplit_PatientMessages.clsLabs = New clsDoctorsDashBoard()
            clsSplit_PatientMessages.clsDMS = New gloEDocumentV3.eDocManager.eDocGetList()
            clsSplit_PatientMessages.clsRxmed = New clsPatientDetails()
            clsSplit_PatientMessages.clsOrders = New clsPatientDetails()
            clsSplit_PatientMessages.clsProblemList = New clsPatientProblemList()
            clsSplit_PatientMessages.blnShowSmokingStatusCol = gblnShowSmokingColumn
            uiPanSplitScreen_PatientMessages = clsSplit_PatientMessages.LoadSplitControl(Me, m_PatientID, GetVisitID(System.DateTime.Now, m_PatientID), "PatientMessages", objCriteria, ObjWord, gnClinicID, gnLoginID)
            uiPanSplitScreen_PatientMessages.BringToFront()

            If chkReminderforUnscheduledCare.Checked = True Then
                cmbReminderforUnscheduledCare.Visible = True
            Else
                cmbReminderforUnscheduledCare.Visible = False
            End If

            UpdateLog("End: Load")

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'obj dispose by Mitesh
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub FillTemplateName()
        'Dim dtTemplateName As DataTable
        If IsNothing(_dtTemplates) Then
            _dtTemplates = objclsMessage.FillTemplates()
        End If
        'dtTemplateName = objclsMessage.FillTemplates()

        cmbTemplate.DataSource = Nothing
        cmbTemplate.Items.Clear()
        If IsNothing(_dtTemplates) = False Then
            cmbTemplate.DataSource = _dtTemplates
            cmbTemplate.ValueMember = _dtTemplates.Columns(0).ColumnName
            cmbTemplate.DisplayMember = _dtTemplates.Columns(1).ColumnName
            If _dtTemplates.Rows.Count > 0 Then
                cmbTemplate.SelectedValue = _dtTemplates.Rows(0)(0)
            End If
        End If
    End Sub
    Private Sub txtSub_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSub.MouseHover
        If txtSub.Text.Length >= 65 Then
            If (IsNothing(tooltipsub) = False) Then
                tooltipsub.Show(txtSub.Text, txtSub, New Point(txtSub.Location.X - 140, txtSub.Location.Y - 20), 2000)
            End If

        End If
        ''tooltip added for subject 
    End Sub
    ' Dim f As Font = New Font("Tahoma", 9.0)

    Public Sub toolTip1_Popup(ByVal sender As System.Object, ByVal e As PopupEventArgs)

        Dim sz As System.Drawing.Size
        sz.Width = 200
        Dim hght As Int32 = 50
        If (txtSub.Text.Length > 100) Then
            hght = (txtSub.Text.Length / 2)

        Else
            hght = 50
        End If
        sz.Height = hght

        e.ToolTipSize = sz

    End Sub
    Private Sub FillReminderforUnscheduledCare()
        Dim oclspatient As New clsPatient
        Dim ds As DataSet = Nothing
        Dim dtCheckUnscheduledCare As DataTable = Nothing
        Dim _strPatitentCommunicationPrefers As String

        Try
            _strPatitentCommunicationPrefers = oclspatient.CheckPatientCommunicationPrefers(m_PatientID)

            If _strPatitentCommunicationPrefers.Length > 0 Then
                ''lblPatientPrefers.Visible = True
                lblPatientPrefers.Text = "Patient Prefers Communication via """ + _strPatitentCommunicationPrefers + """"
                lblPatientPrefers.Tag = _strPatitentCommunicationPrefers
                'Else
                '    ''lblPatientPrefers.Visible = False
            End If

            ds = objclsMessage.FillReminderforUnscheduledCare(Convert.ToInt64(cmbTemplate.SelectedValue))

            cmbReminderforUnscheduledCare.DataSource = Nothing
            cmbReminderforUnscheduledCare.Items.Clear()
            Dim dtReminderList As DataTable = Nothing
            If IsNothing(ds) = False Then


                dtReminderList = ds.Tables(0).Copy()
                dtCheckUnscheduledCare = ds.Tables(1)
            End If

            If IsNothing(dtReminderList) = False Then
                cmbReminderforUnscheduledCare.DataSource = dtReminderList
                cmbReminderforUnscheduledCare.ValueMember = dtReminderList.Columns(0).ColumnName
                cmbReminderforUnscheduledCare.DisplayMember = dtReminderList.Columns(1).ColumnName
                If dtReminderList.Rows.Count > 0 Then
                    Dim row() As DataRow = dtReminderList.Select(" isSelected=1")
                    If (row.Length > 0) Then
                        cmbReminderforUnscheduledCare.SelectedValue = row(0)("nCategoryID")
                    End If
                    row = Nothing
                End If
            End If
            If (IsNothing(dtCheckUnscheduledCare) = False) Then
                If IsDBNull(dtCheckUnscheduledCare) = False AndAlso dtCheckUnscheduledCare.Rows.Count > 0 Then
                    chkReminderforUnscheduledCare.Checked = True
                Else
                    chkReminderforUnscheduledCare.Checked = False

                End If
            Else
                chkReminderforUnscheduledCare.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            _strPatitentCommunicationPrefers = Nothing

            If IsNothing(dtCheckUnscheduledCare) = False Then
                dtCheckUnscheduledCare.Dispose() : dtCheckUnscheduledCare = Nothing
            End If

            If IsNothing(ds) = False Then
                ds.Dispose() : ds = Nothing
            End If

            If Not IsNothing(oclspatient) Then

                oclspatient = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            _strPatitentCommunicationPrefers = Nothing

            If IsNothing(dtCheckUnscheduledCare) = False Then
                dtCheckUnscheduledCare.Dispose() : dtCheckUnscheduledCare = Nothing
            End If

            If IsNothing(ds) = False Then
                ds.Dispose() : ds = Nothing
            End If

            If Not IsNothing(oclspatient) Then
                oclspatient = Nothing
            End If

        End Try
    End Sub

    Private Sub GetUserID(ByVal PatientID As Long)
        ProviderUserID = objclsMessage.GetProviderUserID(PatientID)
    End Sub

    Private Function SaveMessage(Optional ByVal IsClose As Boolean = False) As Boolean
        Dim sFileName As String = String.Empty
        Try
            Me.SuspendLayout()
            Dim isExceptionWhileCopingFile As Boolean = False
            SaveMessage = False
            sFileName = ExamNewDocumentName  ''added for incident 89224
            oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)


            ''Referral letter print issue Resolved #Referral letter was printing only selected exam:Mayuri 20140825

            If m_PatientID = 0 Then
                MessageBox.Show("Select the Patient, who want to send the message.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If

            If cmbTo.Items.Count <= 0 Then
                MessageBox.Show("Select the user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbTo.Focus()
                Exit Function
            End If

            If blnSelectTemplate = False Then
                MessageBox.Show("Please select the Message Template", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbTemplate.Focus()
                Exit Function
            End If

            wdMessages.Focus()

            If chkIsFinished.Checked = True Then
                'IF IsFinished = True Then Remove the Unused Fields 
                If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then ''= Wd.WdProtectionType.wdllowOnlyComments 
                    oCurDoc.Application.ActiveDocument.Unprotect()
                End If
                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                gloWord.LoadAndCloseWord.LockFields(oCurDoc)

            End If

            'Sandip Darade 20090413 no templates available
            If (IsModify = False) Then
                If (cmbTemplate.Items.Count <= 0) Then
                    MessageBox.Show("There are no message templates available.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Function
                End If
            End If


            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdMessages, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

            Dim myBinaray As Object = Nothing
            If (IsNothing(myByte) = False) Then
                myBinaray = CType(myByte, Object)
            End If
            Dim ArrUser(cmbTo.Items.Count - 1) As Long
            Dim mlist As myList
            Dim i As Integer

            For i = 0 To cmbTo.Items.Count - 1
                mlist = cmbTo.Items.Item(i)
                ArrUser(i) = mlist.Index()
            Next
            cmbTemplate.DropDownStyle = ComboBoxStyle.Simple
            If m_MsgID <> 0 Then
                m_MsgID = objclsMessage.AddNewMessageBytes(Reply_MsgID, m_MsgID, txtFrom.Tag, m_PatientID, 0, Format(dtMessage.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, chkIsFinished.CheckState, blnReply, ArrUser, strTemp, cmbPriority.SelectedValue, chkReminderforUnscheduledCare.Checked, cmbReminderforUnscheduledCare.SelectedValue, txtSub.Text.Trim().ToString())
            Else
                m_MsgID = objclsMessage.AddNewMessageBytes(Reply_MsgID, m_MsgID, txtFrom.Tag, m_PatientID, 0, Format(dtMessage.Value, "MM/dd/yyyy hh:mm tt"), myBinaray, chkIsFinished.CheckState, blnReply, ArrUser, cmbTemplate.Text, cmbPriority.SelectedValue, chkReminderforUnscheduledCare.Checked, cmbReminderforUnscheduledCare.SelectedValue, txtSub.Text.Trim().ToString())
                strTemp = cmbTemplate.Text
            End If


            If (myBinaray Is Nothing) Then  ''added for incident 89224

                MessageBox.Show("This document cannot be saved due to technical reasons. Please copy the contents of the document to an external word file to prevent data loss. Then open the message again and try pasting the contents in that document. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                wdMessages.Close()
                oCurDoc = Nothing
                LoadWordUserControl(sFileName, False)
                Exit Function
            End If
            If m_MsgID > 0 Then
                If IsClose Then


                    If (IsNothing(oCurDoc) = False) Then
                        Try
                            Marshal.ReleaseComObject(oCurDoc)
                        Catch ex As Exception


                        End Try
                        oCurDoc = Nothing

                    End If
                Else

                    dtMessage.Enabled = False
                    cmbTemplate.Enabled = False
                    oCurDoc.Saved = True

                    clsSplit_PatientMessages.loadSplitControlData(m_PatientID, GetVisitID(System.DateTime.Now, m_PatientID), uiPanSplitScreen_PatientMessages.SelectedPanel.Name, objCriteria, ObjWord, gnClinicID)

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

            wdMessages.BringToFront()
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            wdMessages.ResumeLayout()
            Me.ResumeLayout()
        End Try
    End Function

    Private Sub LoadGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Width = pnlcustomTask.Height
                dgCustomGrid.BringToFront()
                BindGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub BindGrid()

        Dim dt As DataTable = Nothing

        Try
            dt = objclsMessage.GetAllPatients
            Dim col As New DataColumn

            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")
            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                'For DataBinding Users
                dgCustomGrid.datasource(dt.DefaultView)
                'Sort data view on Login Name                
                objclsMessage.SortDataview(objclsMessage.GetDataview.Table.Columns(1).ColumnName)
            End If

            UserCount = dt.Rows.Count
            CustomGridStyle()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            '08-May-13 Aniket: Resolving Memory Leaks
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Sub

    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            pnlcustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            '08-May-13 Aniket: Resolving Memory Leaks
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
        pnlcustomTask.Controls.Add(dgCustomGrid)
        pnlcustomTask.BringToFront()
        pnlcustomTask.Location = New Point(258, 192)

        pnlcustomTask.Visible = True
        dgCustomGrid.Visible = True
        dgCustomGrid.BringToFront()
    End Sub

    Public Sub CustomGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        'Show User Info
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

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try
            Dim i As Long
            For i = 0 To UserCount
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                    If FindDuplicateTo(CType(dgCustomGrid.GetItem(i, 1), Long)) Then
                        If IsDBNull(dgCustomGrid.GetItem(i, 2)) Then
                            cmbTo.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 1), Long), CType(dgCustomGrid.GetItem(i, 2), System.String)))
                        Else
                            cmbTo.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 1), Long), CType(dgCustomGrid.GetItem(i, 2), System.String)))
                        End If

                        cmbTo.Text = CType(dgCustomGrid.GetItem(i, 2), System.String)
                    End If
                End If
            Next
            pnlcustomTask.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub

    Private Function FindDuplicateTo(ByVal Id As Integer) As Boolean
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
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                'For DataBinding Users
                dgCustomGrid.datasource(dt.DefaultView)
                'Sort data view on Login Name
                objclsMessage.SortDataview(objclsMessage.GetDataview.Table.Columns(1).ColumnName)
            End If
            UserCount = dt.Rows.Count
            CustomUserGridStyle()
            ''slr free dt
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub CustomUserGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        'Show User Info
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

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Private Sub Fill_TemplateGallery()
        Try
            If (IsNothing(dtMessage) = False) Then
                Try
                    dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
                Catch ex As Exception

                End Try
            End If

            Dim strFileName As String
            ''slr free prev memory 
            If Not IsNothing(ObjWord) Then
                ObjWord = Nothing

            End If
            ''slr free prev memory 
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If
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
                LoadWordUserControl(strFileName, True)
                If strFileName <> "" Then
                    'Open Template for processing in DSO Ctrl Set the Start postion of the cursor in documents
                    If (IsNothing(oCurDoc) = False) Then
                        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    End If

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
        Try
            'code added by :Dipak 20090917 for set instance of clsWordDocument class if objword is nothing
            If m_IsReadOnly = False Then
                If (isHandlerRemoved) Then
                    If IsNothing(ObjWord) Then
                        InitialiseWordObject()
                    End If
                    'end code added by :dipak 20090917

                    RemoveWordHandlers()
                    AddWordHandlers()

                    'end added dipak 20091012
                    isHandlerRemoved = False
                End If
                'Line added(moved) by dipak 20091020 due to Error message show for finish messahe when open from dashboard->Messages Tab
                oCurDoc.FormFields.Shaded = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try

        oCurDoc.ActiveWindow.SetFocus()
        If m_IsReadOnly = True Then
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
            If m_IsReadOnly = True Then
                If Not oCurDoc Is Nothing Then
                    Dim protectPane As Wd.TaskPane = oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
                    If (IsNothing(protectPane) = False) Then
                        protectPane.Visible = False
                        Marshal.ReleaseComObject(protectPane)
                        protectPane = Nothing
                    End If
                    '  oCurDoc.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            tmrDocProtect.Enabled = True

        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub CloseMessage(Optional ByVal IsSaved As Boolean = False)
        If (IsSaved) Then
            Me.Close()
        Else
            If m_IsRecordLock = True Then
                wdMessages.Close()

                Me.Close()
            Else
                If IsNothing(oCurDoc) = False Then

                    If oCurDoc.Saved = False Then
                        Dim result As Integer
                        result = MessageBox.Show("Do you want to save the changes in Messages?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        If result = DialogResult.Yes Then

                            If m_IsReadOnly Then
                                chkIsFinished.CheckState = CheckState.Checked
                            Else
                                chkIsFinished.CheckState = CheckState.Unchecked
                            End If
                            If SaveMessage(True) Then
                                wdMessages.Close()
                                Me.Close()
                            End If
                        ElseIf result = DialogResult.No Then
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
            End If
        End If

    End Sub

    Private Sub dgCustomGrid_MouseUpClick(ByVal sender As Object, ByVal e As Object) Handles dgCustomGrid.MouseUpClick
        Try
            If dgCustomGrid.GetCurrentrowIndex >= 0 Then
                dgCustomGrid.GetSelect(dgCustomGrid.GetCurrentrowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub dgCustomGrid_Dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.Dblclick
        Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Call PrintMessage()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub PrintMessage()
        Try
            GeneratePrintFaxDocument()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient Messages'" & cmbTemplate.Text & "' Printed.", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ShowHide_PastExam()
        If tlsMessages.MyToolStrip.Items("Show/Hide").Text = " Show " Then
            'then Show 
            tlsMessages.MyToolStrip.Items("Show/Hide").Text = "Hide"
            tlsMessages.MyToolStrip.Items("Show/Hide").ToolTipText = "Hide"
            pnlPastExam.Show()
            FillPastExams()
        Else
            'then Hide
            tlsMessages.MyToolStrip.Items("Show/Hide").Text = " Show "
            tlsMessages.MyToolStrip.Items("Show/Hide").ToolTipText = " Show "
            pnlPastExam.Hide()
            If IsNothing(oCurDoc) = False Then
                oCurDoc.ActiveWindow.SetFocus()
            End If
        End If
    End Sub

    'Procedure to fill past exams
    Private Sub FillPastExams()

        dgExams.DataSource = Nothing


        Dim clsPatientExams As New clsPatientExams

        '18-Mar-14 Aniket: Resolving Memory Leaks
        Dim dtExam As DataTable

        'Pass patient id & get all exams for that patient
        dtExam = clsPatientExams.Fill_Exams(m_PatientID)

        '08-May-13 Aniket: Resolving Memory Leaks
        clsPatientExams.Dispose()
        clsPatientExams = Nothing
        If (IsNothing(dtExam)) Then
            dgExams.DataSource = Nothing
            Exit Sub
        End If
        dgExams.DataSource = dtExam.Copy().DefaultView
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
            .MappingName = dtExam.Columns(5).ColumnName
            .NullText = ""
            .Width = (dgExams.Width / 3) - 20
        End With

        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName, grdColStylePatientSSNNo})
        dgExams.TableStyles.Clear()

        dgExams.TableStyles.Add(grdTableStyle)
        dgExams.ColumnHeadersVisible = True
        dgExams.RowHeadersVisible = True
        dtExam.Dispose()
        dtExam = Nothing
    End Sub

    Private Sub Undo()
        Try
            If IsNothing(oCurDoc) = False Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub Redo()
        Try
            If IsNothing(oCurDoc) = False Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub InsertAddendum()
        Try
            If m_IsReadOnly Then
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Visible = False
                End If
                pnlToolStrip.Visible = False
                If IsNothing(ouctlgloUC_Addendum) = False Then
                    If (Me.Controls.Contains(ouctlgloUC_Addendum)) Then
                        Me.Controls.Remove(ouctlgloUC_Addendum)
                    End If

                    ouctlgloUC_Addendum.Dispose() : ouctlgloUC_Addendum = Nothing
                End If

                ouctlgloUC_Addendum = New gloUC_Addendum(0, m_MsgID, m_PatientID)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReplyMessage()
        'When User Reply the message then he wants to send message to person who sends message to him

        chkIsFinished.CheckState = CheckState.Unchecked
        Dim objMylist As New myList(txtFrom.Tag, txtFrom.Text)

        If (cmbTo.FindStringExact(txtFrom.Text.Trim()) <> -1) Then

            '08-May-13 Aniket: Resolving Memory Leaks
            objMylist.Dispose()
            objMylist = Nothing
        Else
            cmbTo.Items.Add(objMylist)
            cmbTo.Sorted = True


        End If

        cmbTo.Text = Trim(txtFrom.Text)
        txtFrom.Text = gstrLoginName
        txtFrom.Tag = gnLoginID
        m_MsgDate = dtMessage.Value
        dtMessage.Value = Now
        Me.Text = "Reply Message"
    End Sub

    Public Sub GetPrescription()
        TurnoffMicrophone()
        '' VisitID 
        Try
            dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
        Catch ex As Exception

        End Try


        '12-Feb-16 Aniket: Resolving Bug #93404 ( Modified): gloEMR >Application forcing user to select Patient in New Message screen.
        If m_PatientID <> 0 Then
            Dim frmRxMeds As frmPrescription
            'Incident #55315: 00016572 : Carry forward issue
            frmRxMeds = frmPrescription.GetInstance(m_PatientID, True)
            If IsNothing(frmRxMeds) = True Then
                Exit Sub
            End If
            ''Start'GLO2010-0004511 :: The patient meds seemed to have alter from 2 exmas
            If frmPrescription.IsOpen = False Then
                frmRxMeds.ShowMedication()
                If frmRxMeds.blncancel = True Then
                    With frmRxMeds
                        .WindowState = FormWindowState.Maximized
                        .blnOpenFromMessage = True
                        .ShowInTaskbar = False
                        .ShowDialog(IIf(IsNothing(frmRxMeds.Parent), Me, frmRxMeds.Parent))
                        'Change made to solve memory Leak and word crash issue
                        .Close()
                        .Dispose()
                        GetdataFromOtherForms(gloEMRWord.enumDocType.Medication)
                        GetdataFromOtherForms(gloEMRWord.enumDocType.Prescription)
                    End With
                    frmRxMeds = Nothing
                End If
            Else
                MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ''End'GLO2010-0004511 :: The patient meds seemed to have alter from 2 exmas
        Else
            MessageBox.Show("Please select the Patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub



    Public Sub GetdataFromOtherForms(ByVal _DocType As gloEMRWord.enumDocType) Implements IWord.GetdataFromOtherForms
        oCurDoc.ActiveWindow.SetFocus()

        If (ObjWord Is Nothing) Then
            InitialiseWordObject()
        End If
        If Not (ObjWord Is Nothing) Then
            

            InitialiseWordObject()

            ObjWord.GetFormFieldData(_DocType)
            oCurDoc = ObjWord.CurDocument

           
        End If
        ''Added beacuse of handlers are removed when other word related forms are opened
        ' If _DocType = enumDocType.PatientEducation Or _DocType = enumDocType.RadiologyOrders Or _DocType = enumDocType.ProblemList Then
        If m_IsReadOnly = False Then
            If isHandlerRemoved Then
                oWordApp = oCurDoc.Application
                If Not oWordApp Is Nothing Then
                    Try

                        RemoveWordHandlers()
                        AddWordHandlers()

                        'end added by dipak 20091012 
                        isHandlerRemoved = False

                    Catch ex As Exception
                        UpdateVoiceLog(ex.ToString)
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    Finally
                    End Try

                End If
            End If
        End If
        objCriteria = Nothing
        'ObjWord.Dispose()
        ObjWord = Nothing
    End Sub

    Public Sub InsertCoSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            ''slr free prev memory 
            If Not IsNothing(ObjWord) Then
                ObjWord = Nothing

            End If
            ''slr free prev memory 
            If Not IsNothing(objCriteria) Then
                objCriteria.Dispose()
                objCriteria = Nothing
            End If

            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientID
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

                'SUDHIR 20090619
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                'END SUDHIR

                oCurDoc.Application.Selection.TypeParagraph()
                'By Mahesh Signature With Date - 20070113 Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Co-Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        Try
            'Developer:Yatin N. Bhagat Date:01/31/2012 Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case Reason: Comman Fucntionality is added 
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            Try
                dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
            Catch ex As Exception

            End Try

            Dim objWord As New clsWordDocument
            '   Dim oclsProvider As New clsProvider   slr not used 
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_PatientID, dtMessage.Tag, blnSignClick)
            objCriteria = Nothing
            objWord = Nothing
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    'SUDHIR 20090619
                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    oWord.InsertImage(pSign(0))
                    oWord = Nothing
                    'END SUDHIR 

                    'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                    Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                    If wdRng.Tables.Count > 0 Then
                        oCurDoc.Application.Selection.EndKey()
                    End If
                    'end code added by dipak 

                    oCurDoc.Application.Selection.TypeParagraph()
                    'By Mahesh Signature With Date - 20070113 Add Date Time When Signature is Inserted
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then

                '08-May-13 Aniket: Resolving Memory Leaks
                clsExam.Dispose()
                clsExam = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Messages", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmMessages_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            CType(Me.ParentForm, MainMenu).ShowMessages_New()

            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
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
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Close, "Patient Messages Closed", gloAuditTrail.ActivityOutCome.Success)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            DisposeGlobal()

            If m_Flag = 0 Then
                'Me.Hide()
                If Not IsNothing(myCaller) Then
                    If myCaller.CanSelect = True Then
                        myCaller.RefreshMessages(m_MsgID)
                    End If
                End If

            ElseIf m_Flag = 1 Then
                'if frmMessages is Opend from MainMenu then 
                Me.Hide()
            ElseIf m_Flag = 2 Then '' THIS CONDITION BY SUDHIR 20090406 '' IF frmMessages OPENED FROM frmMyDay ''
                If (IsNothing(MyDayCaller) = False) Then
                    MyDayCaller.DesignGrid(MyDayCaller.C1Message, frmMyDay.GridType.Messages)
                End If

            End If

            'Unlock the Record Mahesh - 20070718 if form is open in view i.e. Record lock then unlock the record and close the form.
            If m_IsRecordLock = False Then
                UnLock_Transaction(TrnType.Messages, m_MsgID, 0, m_MsgDate)
            End If
            'Unlock the Record
            If (IsNothing(mdlFAX.Owner) = False) Then
                mdlFAX.Owner = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(tooltipsub) Then
                Try
                    RemoveHandler tooltipsub.Popup, AddressOf toolTip1_Popup
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                tooltipsub.Dispose()
                tooltipsub = Nothing
            End If
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
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientMessages, m_PatientID, "", "", cmbTemplate.SelectedItem(1), 0, 0, 0, True, Me) = False Then
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
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, m_PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.SelectedItem(1), clsPrintFAX.enmFAXType.PatientMessages) = False Then
            'TIFF File has not been created
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

        '08-May-13 Aniket: Resolving Memory Leaks
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
            If Not IsNothing(frm) Then
                frm.Close() 'Change made to solve memory Leak and word crash issue
                frm.Dispose() : frm = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

        '08-May-13 Aniket: Resolving Memory Leaks

    End Sub

    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property

    Private Sub dgExams_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgExams.DoubleClick
        Try
            If dgExams.CurrentRowIndex <> -1 Then
                dgExams.Enabled = False ''added for bugid 75605
                FillPastExamContents(dgExams.Item(dgExams.CurrentRowIndex, 0))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            dgExams.Enabled = True  ''added for bugid 75605
        End Try
    End Sub

    'Procedure to get the contents of Past exam by passimg exam id
    Private Sub FillPastExamContents(ByVal nPastExamId As Long)
        Dim strFileName As String
        ''slr free prev memory 
        If Not IsNothing(ObjWord) Then
            ObjWord = Nothing

        End If
        ''slr free prev memory 
        If Not IsNothing(objCriteria) Then
            objCriteria.Dispose()
            objCriteria = Nothing
        End If

        ObjWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Exam
        objCriteria.PrimaryID = nPastExamId
        ObjWord.DocumentCriteria = objCriteria
        'Get the Docuemnt From DB
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
        '  wdPastMessages.Open(strFileName)
        '  Dim oWordApp As Wd.Application = Nothing
        gloWord.LoadAndCloseWord.OpenDSO(wdPastMessages, strFileName, oCurDoc2, oWordApp, True)
        oCurDoc2 = wdPastMessages.ActiveDocument
        oWordApp = oCurDoc2.Application
        SetWordObjectView()

    End Sub

    Private Sub SetWordObjectView()
        'oCurDoc2.ActiveWindow.SetFocus()
        'oCurDoc2.ActiveWindow.View.WrapToWindow = True
        'oCurDoc2.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
        tmrPastExamProtect.Enabled = False
        'to Hide Protection Bar when Document is Finished

        'Save Btn is Always INVisible when doc is Finished
        'Initalise Timer
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
                ' oCurDoc2.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
        Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
        Try
            If nInsertScan = 1 Then

                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False

                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)

                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then
                        'Insert file in Wd dobject
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If

                    oFile = Nothing
                End If

            ElseIf nInsertScan = 2 Then

                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()
                'Added by Rahul Patel on 26-10-2010 For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010

                oEDocument.ShowEScannerForImages(m_PatientID, oFiles)

                If Not IsNothing(oEDocument) Then
                    oEDocument.Dispose() : oEDocument = Nothing
                End If

                Dim i As Integer
                Dim firstFlag As Boolean = True
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then
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

                '08-May-13 Aniket: Resolving Memory Leaks
                oFiles.Clear()
                oFiles = Nothing

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(oFileDialogWindow) Then 'Dispose by mitesh
                oFileDialogWindow.Dispose() : oFileDialogWindow = Nothing
            End If
        End Try
    End Sub

    Private Sub loadPatientStrip()
        ''slr free previous memory
        If Not IsNothing(_PatientStrip) Then
            pnlMain.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.PatientMessages)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)
        pnlMain.Controls.Add(_PatientStrip)

        If m_IsReadOnly Then
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.DTPEnabled = False
            End If

        End If
    End Sub

    Private Sub loadToolStrip()
        If Not tlsMessages Is Nothing Then

            '08-May-13 Aniket: Resolving Memory Leaks
            Me.pnlToolStrip.Controls.Remove(tlsMessages)
            tlsMessages.Dispose()
        End If

        tlsMessages = New WordToolStrip.gloWordToolStrip
        tlsMessages.Dock = DockStyle.Top
        tlsMessages.ConnectionString = GetConnectionString()
        tlsMessages.UserID = gnLoginID

        'Added on 20101007 by sanjog for signature
        tlsMessages.dtInput = AddChildMenu()

        Dim oclsProvider As New clsProvider

        tlsMessages.ptProvider = oclsProvider.GetPatientProviderName(m_PatientID)
        tlsMessages.ptProviderId = oclsProvider.GetPatientProvider(m_PatientID)

        '08-May-13 Aniket: Resolving Memory Leaks
        oclsProvider.Dispose()
        oclsProvider = Nothing
        'Added on 20101007 by sanjog for signature

        If m_IsReadOnly Then
            tlsMessages.FormType = WordToolStrip.enumControlType.MessageAddendum
            cmbTemplate.Enabled = False
            dtMessage.Enabled = False
        Else
            tlsMessages.IsCoSignEnabled = gblnCoSignFlag
            tlsMessages.FormType = WordToolStrip.enumControlType.Messages
            cmbTemplate.Enabled = True
            dtMessage.Enabled = True
        End If

        Me.pnlToolStrip.Controls.Add(tlsMessages)
        Me.pnlToolStrip.Size = New System.Drawing.Size(940, 56)
        pnlToolStrip.SendToBack()
        tlsMessages.Height = 100

        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsMessages
                ShowMicroPhone()
            End If
        End If

        If gblnAssociatedProviderSignature And m_IsReadOnly = False Then
            tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsMessages.MyToolStrip.ButtonsToHide.Remove(tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        ElseIf m_IsReadOnly = True Or m_IsRecordLock = True Then

            tlsMessages.MyToolStrip.Items("Save").Visible = False
            If (tlsMessages.MyToolStrip.ButtonsToHide.Contains(tlsMessages.MyToolStrip.Items("Save").Name) = False) Then
                tlsMessages.MyToolStrip.ButtonsToHide.Add(tlsMessages.MyToolStrip.Items("Save").Name)
            End If
            tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsMessages.MyToolStrip.ButtonsToHide.Contains(tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsMessages.MyToolStrip.ButtonsToHide.Add(tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If

        ElseIf gblnAssociatedProviderSignature = False Then
            tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsMessages.MyToolStrip.ButtonsToHide.Contains(tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsMessages.MyToolStrip.ButtonsToHide.Add(tlsMessages.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If
        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsMessages.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsMessages.MyToolStrip.ButtonsToHide.Contains(tlsMessages.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsMessages.MyToolStrip.ButtonsToHide.Add(tlsMessages.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsMessages.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsMessages.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
        'If m_IsRecordLock Then
        '    '20090827 Added by Mayuri
        '    If tlsMessages.MyToolStrip.Items.ContainsKey("Save") = True Then
        '        tlsMessages.MyToolStrip.Items("Save").Enabled = False
        '    End If
        '    If tlsMessages.MyToolStrip.Items.ContainsKey("Save & Close") = True Then
        '        tlsMessages.MyToolStrip.Items("Save & Close").Enabled = False
        '    End If
        '    If tlsMessages.MyToolStrip.Items.ContainsKey("Save & Finish") = True Then
        '        tlsMessages.MyToolStrip.Items("Save & Finish").Enabled = False
        '    End If
        '    If tlsMessages.MyToolStrip.Items.ContainsKey("Add Addendum") = True Then
        '        tlsMessages.MyToolStrip.Items("Add Addendum").Enabled = False
        '    End If
        'End If
    End Sub

    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    Private Sub tlsMessages_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsMessages.ToolStripButtonClick
        Try
            If IsNothing(oCurDoc) = False Then
                InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function AddChildMenu() As DataTable

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim dt As DataTable = Nothing

        Try

            Dim oProvider As New clsProvider
            Dim rslt As Boolean

            rslt = oProvider.CheckSignDelegateStatus()
            If rslt Then

                dt = oProvider.GetAllAssignProviders(gnLoginID)

                '08-May-13 Aniket: Resolving Memory Leaks
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing

        Finally
            If Not IsNothing(dt) Then 'Disposed by mitesh
                dt = Nothing
            End If
        End Try

    End Function

    'Added on 20101007 by sanjog for signature
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

                    Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()

                    Try
                        PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FAXMessage, totalPages, PageNo:=PageNo, iOwner:=Me)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            MessageBox.Show(ex.ToString, "Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.NurseNotes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Load the  Word Document in the Dso control
    ''' </summary>
    ''' <param name="strFileName"></param>
    ''' <param name="blnGetData"></param>
    ''' <remarks></remarks>
    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        Try
            If Not blnCmbSelTemplate Then
                loadToolStrip()
            End If

            If strFileName <> "" Then
                ' wdMessages.Open(strFileName)
                ' Dim oWordApp As Wd.Application = Nothing

                Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdMessages, strFileName, oCurDoc, oWordApp)
                If (strError <> String.Empty) Then
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, strError, gloAuditTrail.ActivityOutCome.Failure)

                Else


                    If blnGetData Then
                        ''slr free previous memory
                        If Not IsNothing(ObjWord) Then
                            ObjWord = Nothing
                        End If
                        ''slr free prev memory 
                        If Not IsNothing(objCriteria) Then
                            objCriteria.Dispose()
                            objCriteria = Nothing
                        End If

                        ObjWord = New clsWordDocument
                        'Mapping values for retrieving data from DB
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Message
                        objCriteria.PatientID = m_PatientID
                        objCriteria.VisitID = dtMessage.Tag
                        objCriteria.PrimaryID = 0
                        ObjWord.DocumentCriteria = objCriteria
                        ObjWord.CurDocument = oCurDoc
                        ObjWord.DisableWordRefresh = True
                        ObjWord.GetFormFieldData(enumDocType.None)
                        'ObjWord.GetFormFieldDataDBCalls(enumDocType.None)
                        oCurDoc = ObjWord.CurDocument
                        oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                    Else
                        ''slr free previous memory
                        If Not IsNothing(ObjWord) Then
                            ObjWord = Nothing
                        End If
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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' To implemt the Dropdown and check Box selection change event
    ''' </summary>
    ''' <param name="Sel"></param>
    ''' <remarks></remarks>
    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)

        Dim r As Wd.Range = Nothing
        'Dim om As Object
        ' Dim o As Object = 1
        Dim oUnit As Object
        Dim oCnt As Object = 1
        Dim oMove As Object

        Try
            If IsNothing(Sel) Then
                Return
            End If
            If (Sel.Type <> Microsoft.Office.Interop.Word.WdSelectionType.wdNoSelection) Then
                If Sel.Start = Sel.End Then
                    Try
                        r = Sel.Range
                    Catch ex As Exception

                    End Try

                    If IsNothing(r) = False Then
                        r.SetRange(Sel.Start, Sel.End + 1)
                    Else

                        Return
                    End If
                    If IsNothing(r) Then
                        Return
                    End If
                    ' r.SetRange(Sel.Start, Sel.End + 1)

                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then

                        '  om = System.Reflection.Missing.Value
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
                                oUnit = Wd.WdUnits.wdCharacter

                                oMove = Wd.WdMovementType.wdMove
                                Sel.MoveRight(oUnit, oCnt, oMove)
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

            '08-May-13 Aniket: Resolving Memory Leaks
            '  om = Nothing

            oUnit = Nothing
            oCnt = Nothing
            oMove = Nothing

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

            If Not oCurDoc Is Nothing Then

                If (IsNothing(oWordApp)) Then
                    Try
                        oWordApp = oCurDoc.Application
                    Catch ex As Exception
                        UpdateVoiceLog(ex.ToString)
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                End If

                If (ObjWord Is Nothing) Then
                    InitialiseWordObject()
                End If
                'code added by dipak 20091224 to 
                If Not IsNothing(oCurDoc) Then
                    garrOpenDocument.Remove(oCurDoc.FullName)
                End If
                If (IsNothing(oWordApp) = False) Then


                    Try
                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf oWordApp_WindowBeforeDoubleClick
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    isHandlerRemoved = True
                    For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                        If (IsNothing(oFile) = False) Then
                            Try
                                If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                    Try
                                        oFile.Delete()
                                    Catch ex As Exception
                                        UpdateVoiceLog("Unable to Delete: " & ex.ToString)
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, "Unable to Delete: " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                                    End Try
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'GC.Collect()  ''code change problem 00000591
            'GC.WaitForPendingFinalizers()
        End Try

    End Sub

    Private Sub wdMessages_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdMessages.OnDocumentOpened
        'Assign control's document to Document object 
        oCurDoc = wdMessages.ActiveDocument
        oWordApp = oCurDoc.Application

        If Not (garrOpenDocument.Contains(oCurDoc.FullName)) Then
            garrOpenDocument.Add(oCurDoc.FullName)
        End If
        Try

            If (isHandlerRemoved) Then
                If (ObjWord Is Nothing) Then
                    InitialiseWordObject()
                End If
                RemoveWordHandlers()
                AddWordHandlers()

                isHandlerRemoved = False
            End If

            'Developer: Yatin N.Bhagat Date:01/04/2011 Bug ID/PRD Name/Salesforce Case:Bug No. 18134:Patient Messages >> Finished >> Addendum >> Check boxes is not working
            'Reason: Scope of If m_IsReadOnly = False Then is changed
            If m_IsReadOnly = False Then
                'Code added by dipak 20091020 to make visible for Not Finish Message
                btnRefresh.Visible = True
                btnAddFields.Visible = True
            Else
                'Code added by dipak 20091020 to make visible=false for Finish Message
                btnRefresh.Visible = False
                btnAddFields.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        Try
            TurnoffMicrophone()
            If cmbTemplate.SelectedValue > 0 Then
                blnCmbSelTemplate = True
                FillReminderforUnscheduledCare()
                ''Ashish added on 1st October 
                ''to prevent screen from refreshing
                'Dim WDocViewType As Wd.WdViewType
                'Dim wordRefresh As New WordRefresh()

                'Try
                '    WDocViewType = oCurDoc.ActiveWindow.View.Type
                '    wordRefresh.OptimizePerformance(False, oCurDoc, 0)
                '    wordRefresh.ShowPanel(Me.pnlWordObj)

                Fill_TemplateGallery()
                'Catch ex As Exception
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '    ex = Nothing
                'Finally
                '    'Ashish added on 31st October 
                '    'to prevent screen from refreshing
                '    wordRefresh.HidePanel(Me.pnlWordObj)
                '    wordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
                '    wordRefresh.Dispose()
                '    wordRefresh = Nothing
                'End Try

                'WDocViewType = Nothing
                blnCmbSelTemplate = False
                blnSelectTemplate = True

            Else
                wdMessages.Close()
            End If
        Catch ex As Exception
            blnCmbSelTemplate = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnReply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReply.Click
        Try
            If blnReply = False Then

                'Sandip Darade 20100729 on reply click clear the current user collection and add the user to be replied to the list                 
                Dim ToItem As gloGeneralItem.gloItem
                ToItem = New gloGeneralItem.gloItem()
                ToItem.ID = gnLoginID
                ToItem.Description = gstrLoginName
                ToList.Remove(ToItem)
                ToItem.Dispose()
                ToItem = Nothing

                If (cmbTo.FindStringExact(gstrLoginName) <> -1) Then
                    cmbTo.Items.RemoveAt(cmbTo.FindStringExact(gstrLoginName))
                End If


                ToItem = Nothing
                ToItem = New gloGeneralItem.gloItem()
                ToItem.ID = txtFrom.Tag
                ToItem.Description = txtFrom.Text
                ToList.Add(ToItem)
                ToItem.Dispose()
                ToItem = Nothing
























                Call ReplyMessage()
                'Keep Current Messageid in Reply_MsgID for Update message --> Finished
                Reply_MsgID = m_MsgID



            End If



            blnReply = True
            'Problem 00000024 : date column displays the date that the message was last "Saved" rather than "Created." 
            Label4.Text = "Reply Date :"
        Catch ex As Exception
            'Problem 00000024 : date column displays the date that the message was last "Saved" rather than "Created." 
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUser.Click
        Try

            ''slr free prev memory
            If Not IsNothing(ofrmList) Then
                ofrmList.Close()
                ofrmList.Dispose()
                ofrmList = Nothing
            End If
            ofrmList = New frmViewListControl
            ofrmList.Text = "Users"
            ''slr free prev memory
            If Not IsNothing(oListUsers) Then
                oListUsers.Dispose()
                oListUsers = Nothing
            End If
            oListUsers = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Users, True, ofrmList.Width)
            oListUsers.ControlHeader = "Users"
            oListUsers.IsgloCollectCustomer = True

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick

            'To Select already Added Users.
            If IsNothing(ToList) = False Then
                For i As Integer = 0 To ToList.Count - 1
                    If ToList(i).bGloCollect = True And m_MsgID > 0 Then
                        bIncludeAllUsers = True
                    End If
                    oListUsers.SelectedItems.Add(ToList(i))
                Next
            End If
            oListUsers.bchkIncludeAllUsers = bIncludeAllUsers
            ofrmList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.OpenControl()
            oListUsers.ShowHeaderPanel(False)
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

            'Change made to solve memory Leak and word crash issue
            If IsNothing(ofrmList) = False Then
                RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                ofrmList.Controls.Remove(oListUsers)
                oListUsers.Dispose()
                oListUsers = Nothing
                ofrmList.Dispose() : ofrmList = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try

            cmbTo.Items.Clear()

            If IsNothing(ToList) = False Then
                ToList.Clear()
                ToList.Dispose() : ToList = Nothing
            End If

            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem

            If IsNothing(oListUsers) = False Then

                If oListUsers.SelectedItems.Count > 0 Then

                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description
                        ToItem.bGloCollect = oListUsers.SelectedItems(i).bGloCollect
                        ToList.Add(ToItem)
                        ToItem.Dispose()
                        ToItem = Nothing
                        cmbTo.Items.Add(New myList(CType(oListUsers.SelectedItems(i).ID, Long), CType((oListUsers.SelectedItems(i).Description), System.String)))
                        cmbTo.Text = CType(oListUsers.SelectedItems(i).Description, System.String)

                    Next

                End If
            End If
            ofrmList.Close()
            oListUsers.IsgloCollectCustomer = False
            bIncludeAllUsers = oListUsers.bchkIncludeAllUsers
            If cmbTo.Items.Count > 0 Then
                cmbTo.Sorted = True
                cmbTo.SelectedIndex = cmbTo.Items.Count - 1
            End If


        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub btnClearUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearUser.Click
        Try

            If cmbTo.SelectedIndex >= 0 Then
                Dim _userId As Int64 = 0
                'Remove item from ToList and combo box
                Dim i As Integer

                _userId = Convert.ToInt64(CType(cmbTo.Items.Item(cmbTo.SelectedIndex), myList).Index)

                For i = 0 To ToList.Count - 1
                    If (ToList(i).ID = _userId) Then
                        ToList.RemoveAt(i)
                        Exit For
                    End If
                Next

                cmbTo.Items.RemoveAt(cmbTo.SelectedIndex)

                If cmbTo.Items.Count = 0 Then
                    cmbTo.Text = ""
                Else
                    cmbTo.SelectedIndex = 0
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' To Implement tool strip items click for Addendum
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tlsMessages_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsMessages.ToolStripClick
        Try

            Select Case e.ClickedItem.Name
                Case "Show/Hide"
                    ShowHide_PastExam()

                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "witchOff Mic Completed from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)

                Case "Save"
                    TurnoffMicrophone()
                    If blnReply = False Then
                        chkIsFinished.CheckState = CheckState.Unchecked
                    End If
                    If m_IsReadOnly Then
                        chkIsFinished.CheckState = CheckState.Checked
                    End If
                    Call SaveMessage()

                Case "Save & Close"
                    If blnReply = False Then
                        chkIsFinished.CheckState = CheckState.Unchecked
                    End If
                    If SaveMessage(True) = True Then
                        Call CloseMessage(True)
                    End If

                Case "Print"
                    TurnoffMicrophone()
                    Call PrintMessage()
                    cmbTemplate.Enabled = False
                    If IsNothing(dtMessage) = False Then
                        dtMessage.Enabled = False
                    End If


                Case "FAX"
                    bnlIsFaxOpened = True
                    TurnoffMicrophone()
                    Call GeneratePrintFaxDocument(False)
                    cmbTemplate.Enabled = False

                    If IsNothing(dtMessage) = False Then
                        dtMessage.Enabled = False
                    End If

                    bnlIsFaxOpened = False

                Case "Insert Sign"
                    'Call InsertProviderSignature() 'if user is provider then insert provider sign lese user's sign
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
                    Call CloseMessage()

                Case "Prescription"
                    TurnoffMicrophone()
                    Call GetPrescription()

                Case "OrderTemplates"
                    TurnoffMicrophone()

                    Call GetOrders()
                Case "Save & Finish"
                    chkIsFinished.CheckState = CheckState.Checked

                    If SaveMessage(True) = True Then
                        Call CloseMessage(True)
                    End If

                Case "Add Addendum"
                    Call InsertAddendum()

                Case "DateTimeStamp" 'SUDHIR 20091021
                    InsertTimeStamp()

                Case "tblbtn_StrikeThrough"
                    'chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()

                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient Message", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
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
                            cmbTemplate.Enabled = False
                            dtMessage.Enabled = False
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.ClinicalExchange, "Send Messages using Secure Message", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '  Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        'wdMessages.Close()
        ''slr free wdemp
        'If (IsNothing(wdTemp) = False) Then
        '    Me.pnlFromTo.Controls.Remove(wdTemp)
        '    wdTemp.Dispose()
        '    wdTemp = Nothing
        'End If
        'If Not IsNothing(wdTemp) Then
        '    Try
        '        Marshal.ReleaseComObject(wdTemp)
        '    Catch ex As System.Runtime.InteropServices.COMException

        '    End Try

        'End If
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Dim oSendDoc As New clsPrintFAX
        'Dim osenddox As String
        'osenddox = oSendDoc.SendDoc(oTempDoc, m_PatientID)

        ''08-May-13 Aniket: Resolving Memory Leaks
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

        ''Read Secure Messages settings and call Inbox form
        If osenddox.Length > 0 Then
            If File.Exists(osenddox) Then
                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientID, osenddox)
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                ''slr close it
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
        ''Set the Start postion of the cursor in documents
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

                        'SANJOG-ADDED ON 20101123 TO MAKE UNDO CHANGES IN DOCUMENT AFTER STRIKE THROUGH
                        If m_IsReadOnly = True Then
                            oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                        Else
                            oCurDoc.Application.ActiveDocument.Unprotect(Wd.WdProtectionType.wdAllowOnlyComments)
                        End If
                        'SANJOG-ADDED ON 20101123 TO MAKE UNDO CHANGES IN DOCUMENT AFTER STRIKE THROUGH
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If m_IsReadOnly = True Then
                tmrDocProtect.Enabled = True
            End If
        End Try
    End Sub

    Private Sub InsertTimeStamp()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            oCurDoc.ActiveWindow.SetFocus()
            oCurDoc.Application.Selection.TypeParagraph()

            'TFS Bug: 6509: message date and time stamp TEXT: date and time by userid 
            oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " by " & gstrLoginName)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.General, "Time Stamp Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub ActivateBasicVoiceCmds(ByVal myVoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateBasicVoiceCmds(myVoiceCol)
            End If
        End If
    End Sub

    Private Function AddBasicVoiceCommands() As Hashtable
        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Messages", "Save")
        oHashtable.Add("Print Messages", "Print")
        oHashtable.Add("Fax Messages", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Messages", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Messages", "Close")
        oHashtable.Add("Finish Messages", "Save & Finish")
        oHashtable.Add("Prescription", "Prescription")
        oHashtable.Add("Orders", "Orders")
        oHashtable.Add("Radiology Orders", "Orders")
        oHashtable.Add("Show Past Exam", "Show/Hide")
        oHashtable.Add("Hide Past Exam", "Show/Hide")
        Return oHashtable
    End Function

    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        End If
    End Sub

    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
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

    Private Sub frmMessages_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.TurnoffMicrophone()
                End If
            ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                If Not IsNothing(ogloVoice) Then
                    ogloVoice.TurnoffMicrophone()
                End If
            End If


            If Not oCurDoc Is Nothing Then
                RemoveWordHandlers()
                isHandlerRemoved = True
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
            End If

            If Not IsNothing(Me.Parent) Then
                If Not IsNothing(uiPanSplitScreen_PatientMessages) Then
                    If Not IsNothing(uiPanSplitScreen_PatientMessages.Parent) Then
                        If uiPanSplitScreen_PatientMessages.Parent IsNot Me Then
                            uiPanSplitScreen_PatientMessages.Parent.Visible = False
                            uiPanSplitScreen_PatientMessages.Parent.Hide()
                            uiPanSplitScreen_PatientMessages.Parent.Update()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsMessages.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsMessages.MyToolStrip.Items("Mic").Visible = True
                        tlsMessages.MyToolStrip.ButtonsToHide.Remove(tlsMessages.MyToolStrip.Items("Mic").Name)
                        tlsMessages.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                    End If

                ElseIf gblnVoiceEnabled = True And gblnSpeakerExists = True And blnIsAddendum = True Then
                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic") Is Nothing Then
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Visible = True
                    End If


                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsMessages.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsMessages.MyToolStrip.ButtonsToHide.Remove(tlsMessages.MyToolStrip.Items("Mic").Name)
                    End If

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic") Is Nothing Then
                        Me.ouctlgloUC_Addendum.tlsAddendum.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                    End If

                End If
            ElseIf strstring = "[TimeStamp]" Then       ''When alt+shift+d is pressed it shows the datatime for the form ''Dhruv 20100223
                Call InsertTimeStamp()

            ElseIf strstring = "OFF" Then
                If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then

                    '04-Jul-14 Aniket: Resolving Bug #67037
                    If Not tlsMessages.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsMessages.MyToolStrip.Items("Mic").Visible = True
                    End If

                    tlsMessages.MyToolStrip.ButtonsToHide.Remove(tlsMessages.MyToolStrip.Items("Mic").Name)

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
                    If Not tlsMessages.MyToolStrip.Items("Mic") Is Nothing Then
                        tlsMessages.MyToolStrip.Items("Mic").Visible = False
                        If tlsMessages.MyToolStrip.ButtonsToHide.Contains(tlsMessages.MyToolStrip.Items("Mic").Name) = False Then
                            tlsMessages.MyToolStrip.ButtonsToHide.Add(tlsMessages.MyToolStrip.Items("Mic").Name)
                        End If
                    End If



                End If

                '04-Jul-14 Aniket: Resolving Bug #67037
                If Not tlsMessages.MyToolStrip.Items("Mic") Is Nothing Then
                    tlsMessages.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
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
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmMessages_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

            'If Record is open for only view then exit from function.
            If m_IsRecordLock Then
                Exit Sub
            End If

            'Shweta 20090828 To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            'End Shweta

            If IsNothing(oCurDoc) = False Then
                'When form closes, If User had done any change in Meassage then  
                If oCurDoc.Saved = False Then
                    Dim Result As Integer
                    Result = MessageBox.Show("Do you want to save the changes in this Message?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If Result = DialogResult.Yes Then
                        If SaveMessage() = False Then e.Cancel = True

                    ElseIf Result = Windows.Forms.DialogResult.No Then
                        e.Cancel = False

                    ElseIf Result = DialogResult.Cancel Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If

            If Not e.Cancel Then
                If (IsNothing(clsSplit_PatientMessages) = False) Then
                    clsSplit_PatientMessages.SaveControlDisplaySettings()
                End If
                If (IsNothing(uiPanSplitScreen_PatientMessages) = False) Then
                    uiPanSplitScreen_PatientMessages.Dispose() : uiPanSplitScreen_PatientMessages = Nothing
                End If
                If (IsNothing(clsSplit_PatientMessages) = False) Then
                    clsSplit_PatientMessages.Dispose() : clsSplit_PatientMessages = Nothing
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Turn on Microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone, IWord.ShowMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
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
    ''' Turn off Microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone, IWord.TurnOffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True And m_IsReadOnly = False Then
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
    ''' Initialise voice object
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObject()

        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose() : ogloVoice = Nothing
        End If

        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)

        ogloVoice.MyWordToolStrip = Me.tlsMessages
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Messages
        ogloVoice.MessageName = "Messages"
        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsMessages_ToolStripClick)
        ShowMicroPhone()
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
            ogloVoice.Dispose() : ogloVoice = Nothing
        End If

        Dim oAddendumHashtable As ArrayList = ouctlgloUC_Addendum.FillTemplateCommands(True)
        ogloVoice = New ClsVoice(oAddendumHashtable)
        ogloVoice.gloTreeView = ouctlgloUC_Addendum.trvTemplates
        ogloVoice.eVoiceAddendum = VoiceAddendum.eAddendum
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Messages
        ogloVoice.MyWordToolStrip = Me.ouctlgloUC_Addendum.tlsAddendum
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Messages"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf Me.ouctlgloUC_Addendum.onToolStripClick)
        ogloVoice.AddVoiceCommands()
    End Sub

    ''' <summary>
    ''' on Addendum Close
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ouctlgloUC_Addendum_OnAddendumClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles ouctlgloUC_Addendum.OnAddendumClose
        Me.Controls.Remove(ouctlgloUC_Addendum)

        ouctlgloUC_Addendum.Dispose() : ouctlgloUC_Addendum = Nothing

        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        blnIsAddendum = False
        TurnoffMicrophone()
    End Sub

    ''' <summary>
    ''' On Addendum Save
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

        ouctlgloUC_Addendum.Dispose() : ouctlgloUC_Addendum = Nothing

        pnlToolStrip.Visible = True
        If (IsNothing(_PatientStrip) = False) Then
            _PatientStrip.Visible = True
        End If

        blnIsAddendum = False
        TurnoffMicrophone()
    End Sub

    Private Sub frmMessages_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            tlsMessages.MyToolStrip.Items("Show/Hide").ToolTipText = "Show"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReply.MouseHover, btnClearUser.MouseHover, btnUser.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReply.MouseLeave, btnClearUser.MouseLeave, btnUser.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Public Sub GetOrders()
        TurnoffMicrophone()
        Try
            dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
        Catch ex As Exception

        End Try


        '15-Feb-16 Aniket: Resolving Bug #93467 ( Modified): gloEMR: Messages: Application gives invalid message on order templates
        If m_PatientID <> 0 Then

            Dim frmOrders As frm_LM_Orders
            frmOrders = frm_LM_Orders.GetInstance(dtMessage.Tag, dtMessage.Value, m_PatientID, 0, False)

            If IsNothing(frmOrders) = True Then
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

    Private Sub InitialiseWordObject()
        If IsNothing(ObjWord) Then
            ObjWord = New clsWordDocument
        End If
        ObjWord.myCallingForm = Me
        ObjWord.CurDocument = oCurDoc  'wdMessages.ActiveDocument
        If IsNothing(objCriteria) Then
            objCriteria = New DocCriteria
        End If
        'Incident #55315: 00016572 : Carry forward issue
        If (IsNothing(_PatientStrip) = False) Then
            objCriteria.VisitID = GenerateVisitID(_PatientStrip.DTPValue, m_PatientID)
        End If

        objCriteria.DocCategory = enumDocCategory.Message
        objCriteria.PatientID = m_PatientID
        objCriteria.PrimaryID = 0
        ObjWord.DocumentCriteria = objCriteria
    End Sub

    ''' <summary>
    ''' Validate selected range is valid for inserting new field
    ''' </summary>
    ''' <remarks>added by dipak20091001</remarks>
    Private Sub ValidateSelection()

        Dim rTemp As Wd.Range = oCurDoc.Application.Selection.Range
        If Not rTemp.ParentContentControl Is Nothing Then
            If rTemp.ParentContentControl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                Dim cntctrl As Wd.ContentControl = rTemp.ParentContentControl
                oCurDoc.Application.Selection.EndKey(Unit:=Wd.WdUnits.wdLine)
                oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=2)
            End If
        End If
        ''slr free rtemp
        Try
            Marshal.ReleaseComObject(rTemp)
        Catch ex As COMException

        End Try

    End Sub

    Private Sub btnAddFields_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddFields.Click
        ''to check the Patient education form is open or not 
        If frmPatientEducation.Formopen_Info Then
            Exit Sub
        End If

        If ObjWord IsNot Nothing Then
            ObjWord.WaitControlPanel = Me.pnlWordObj
        End If

        oCurDoc = wdMessages.ActiveDocument
        '  If (ObjWord Is Nothing) Then
        InitialiseWordObject()
        ' End If
        ''Split Control Disabled for Bug No 47902::20130326
        uiPanSplitScreen_PatientMessages.Enabled = False
        If Not oCurDoc Is Nothing Then
            ValidateSelection()
            ''show frmAddDictionary to add DataDictionaryFields
            ObjWord.WaitControlPanel = Me.pnlWordObj
            Call ObjWord.AddDataDictionaryFields(Me)
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.SetFocus()
        End If
        ''Split Control Enabled for Bug No 47902::20130326
        uiPanSplitScreen_PatientMessages.Enabled = True
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If frmPatientEducation.Formopen_Info Then
            Exit Sub
        End If
        Call RefreshFields()
    End Sub

    ''' <summary>
    ''' Procedure refresh content of document.
    ''' </summary>
    ''' <remarks>added by dipak 20091001</remarks>
    Private Sub RefreshFields()
        oCurDoc.ActiveWindow.SetFocus()
        Me.Cursor = Cursors.WaitCursor
        GetdataFromOtherForms(enumDocType.None)
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' procedure added by dipak to handle WindowBeforeDoubleClick 
    ''' </summary>
    ''' <param name="Sel"></param>
    ''' <param name="Cancel"></param>
    ''' <remarks>Added by dipak 20091012</remarks>
    Private Sub oWordApp_WindowBeforeDoubleClick(ByVal Sel As Microsoft.Office.Interop.Word.Selection, ByRef Cancel As Boolean) ' Handles oWordApp.WindowBeforeDoubleClick
        If m_IsReadOnly = False And tmrDocProtect.Enabled = False Then '' Protected conditon
            Cancel = False
            ' If (ObjWord Is Nothing) Then
            InitialiseWordObject()
            'End If
            'call of comman event for liquid link
            ObjWord.PatientId = m_PatientID
            ObjWord.Get_PatientDetails(m_PatientID)
            ObjWord.OnFormClicked(Sel, Cancel)
        End If
    End Sub

    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)

        Try
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(oPatient) = False Then
                oPatient.Dispose() : oPatient = Nothing
            End If
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose() : dtPatient = Nothing
            End If
        End Try
    End Sub

    Private Sub DisposeGlobal()

        '18-Mar-14 Aniket: Resolving Memory Leaks


        If Not IsNothing(objclsMessage) Then
            objclsMessage.Dispose() : objclsMessage = Nothing
        End If

        If Not IsNothing(ToList) Then
            ToList.Clear()
            ToList.Dispose() : ToList = Nothing
        End If

        If Not IsNothing(oListUsers) Then
            oListUsers.Dispose() : oListUsers = Nothing
        End If

        If Not IsNothing(ofrmList) Then
            ofrmList.Dispose() : ofrmList = Nothing
        End If

        If Not IsNothing(objCriteria) Then
            objCriteria = Nothing
        End If

        If Not IsNothing(ObjWord) Then
            ObjWord = Nothing
        End If

        If Not IsNothing(tlsMessages) Then
            Me.pnlToolStrip.Controls.Remove(tlsMessages)
            tlsMessages.Dispose() : tlsMessages = Nothing
        End If

        If Not IsNothing(_PatientStrip) Then

            pnlMain.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing

        End If

        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose() : ogloVoice = Nothing
        End If

        'SLR: Marshal free oCurDoc, oCurDoc2, oTempDoc, oWordApp
        Try

            ''Slr Marshal Free objects
            If Not IsNothing(oCurDoc) Then
                Marshal.ReleaseComObject(oCurDoc)

            End If
            If Not IsNothing(oCurDoc2) Then
                Marshal.ReleaseComObject(oCurDoc2)
            End If
            'If Not IsNothing(oTempDoc) Then
            '    Marshal.ReleaseComObject(oTempDoc)
            'End If
            'If Not IsNothing(oWordApp) Then
            '    Marshal.ReleaseComObject(oWordApp)
            'End If
        Catch ex As COMException

        End Try
    End Sub

    Private Sub chkReminderforUnscheduledCare_CheckChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReminderforUnscheduledCare.CheckedChanged
        If chkReminderforUnscheduledCare.Checked = True Then
            cmbReminderforUnscheduledCare.Visible = True
            If lblPatientPrefers.Tag IsNot Nothing Then
                If lblPatientPrefers.Tag.Trim() <> "" And lblPatientPrefers.Tag.Trim() <> cmbReminderforUnscheduledCare.Text.Trim() Then
                    lblPatientPrefers.Visible = True
                Else
                    lblPatientPrefers.Visible = False
                End If
            End If

        Else
            cmbReminderforUnscheduledCare.Visible = False
            lblPatientPrefers.Visible = False
        End If
    End Sub




    Private Sub cmbReminderforUnscheduledCare_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbReminderforUnscheduledCare.SelectedIndexChanged
        If chkReminderforUnscheduledCare.Checked = True Then
            cmbReminderforUnscheduledCare.Visible = True
            If lblPatientPrefers.Tag IsNot Nothing Then
                If lblPatientPrefers.Tag.Trim() <> "" And lblPatientPrefers.Tag.Trim() <> cmbReminderforUnscheduledCare.Text.Trim() Then
                    lblPatientPrefers.Visible = True
                Else
                    lblPatientPrefers.Visible = False
                End If
            End If

        Else
            cmbReminderforUnscheduledCare.Visible = False
            lblPatientPrefers.Visible = False
        End If

        'If ((cmbReminderforUnscheduledCare.SelectedItem <> Nothing) Or (cmbReminderforUnscheduledCare.SelectedItem <> "")) Then
        '    ToolTip1.SetToolTip(cmbReminderforUnscheduledCare, Convert.ToString(cmbReminderforUnscheduledCare.Items(cmbReminderforUnscheduledCare.SelectedIndex)))
        'Else
        '    ToolTip1.SetToolTip(cmbReminderforUnscheduledCare, "")
        'End If



    End Sub





































































































































End Class


