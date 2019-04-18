Imports System.Windows.Forms.Integration
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows
Imports System.Windows.Controls
Imports System.IO
Imports gloSettings
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports gloUserControlLibrary
Imports gloPatientPortalCommon
Imports gloGlobal


Public Class frmSendSecureMsg

#Region "Variable Declaration"

    Public ht As Integer
    Dim _isMouseDown As Boolean = False
    Dim oslectedBorder As Border
    Private PatientID As Int64
    Private ConnectionString As String
    Private ClientMachineID As String
    Private ClientMachineName As String
    Private CommDetailID As Long
    Private _SubjectFromReadMessage As String = ""
    Private _MessageFromReadMessage As String = ""
    Private _StaffDescriptionFromReadMessage As String = ""
    Private _IncludeMessageInReply As Boolean = False
    Private _IsReplyMessage As Boolean = False
    Private _PatientStatus As Int16 = 1
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Dim gstrMessageBoxCaption As String = ""
    Private _lngContainerID As Int64 = 0
    Private _strserviceType As String = ""
    Private _AppnDate As DateTime?
    Private _AppnTime As String = ""
    Private _Status As Int16
    Private _flag As Int16
    Private _AppointmentDate As DateTime?
    Dim _OriginalMessage As String = ""

    'using for split screen
    Private m_objPatientExam As Object
    Private m_objPatientLetters As Object
    Private m_objPatientMessages As Object
    Private m_objNurseNotes As Object
    Private m_objHistory As Object
    Private m_objLabs As Object
    Private m_objDMS As Object
    Private m_objRxmed As Object
    Private m_objOrders As Object
    Private m_objProblemList As Object
    Private m_objCriteria As Object
    Private m_objWord As Object
    'MU2 Patient Portal
    Dim _blnIntuitCommunication As Boolean = False
    Dim _strPatientPortalSiteNm As String = ""
    Dim _strServiceURI As String = ""
    Dim _nClinicID As Long = 0
    ''Using for MU2 Patient Portal 
    Private _PortalType As String = String.Empty
    '18-Aug-2015 Aniket: Resolving Bug #84080: EMR: Send Portal- Send portal window goes back as user unlock EMR screen
    Private Shared IntPRT As System.IntPtr = System.IntPtr.Zero

    Private intEducationID As Int64
    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
    Dim nProviderID As Int64 = 0
    ''End
#End Region

#Region "Property Procedures"

    Public Shared Property IntMyHandle() As System.IntPtr
        Get
            Return IntPRT
        End Get
        Set(ByVal value As System.IntPtr)
            IntPRT = value
        End Set
    End Property


    Public Property IsReplyMessage() As Boolean
        Get
            Return _IsReplyMessage
        End Get
        Set(ByVal value As Boolean)
            _IsReplyMessage = value
        End Set
    End Property

    Public Property IncludeMessageInReply() As Boolean
        Get
            Return _IncludeMessageInReply
        End Get
        Set(ByVal value As Boolean)
            _IncludeMessageInReply = value
        End Set
    End Property

    'using for split screen

    Public Property objPatientExam() As Object
        Get
            Return m_objPatientExam
        End Get
        Set(value As Object)
            m_objPatientExam = value
        End Set
    End Property

    Public Property objPatientLetters() As Object
        Get
            Return m_objPatientLetters
        End Get
        Set(value As Object)
            m_objPatientLetters = value
        End Set
    End Property

    Public Property objPatientMessages() As Object
        Get
            Return m_objPatientMessages
        End Get
        Set(value As Object)
            m_objPatientMessages = value
        End Set
    End Property

    Public Property objNurseNotes() As Object
        Get
            Return m_objNurseNotes
        End Get
        Set(value As Object)
            m_objNurseNotes = value
        End Set
    End Property

    Public Property objHistory() As Object
        Get
            Return m_objHistory
        End Get
        Set(value As Object)
            m_objHistory = value
        End Set
    End Property

    Public Property objLabs() As Object
        Get
            Return m_objLabs
        End Get
        Set(value As Object)
            m_objLabs = value
        End Set
    End Property

    Public Property objDMS() As Object
        Get
            Return m_objDMS
        End Get
        Set(value As Object)
            m_objDMS = value
        End Set
    End Property

    Public Property objRxmed() As Object
        Get
            Return m_objRxmed
        End Get
        Set(value As Object)
            m_objRxmed = value
        End Set
    End Property

    Public Property objOrders() As Object
        Get
            Return m_objOrders
        End Get
        Set(value As Object)
            m_objOrders = value
        End Set
    End Property

    Public Property objProblemList() As Object
        Get
            Return m_objProblemList
        End Get
        Set(value As Object)
            m_objProblemList = value
        End Set
    End Property

    Public Property objCriteria() As Object
        Get
            Return m_objCriteria
        End Get
        Set(value As Object)
            m_objCriteria = value
        End Set
    End Property

    Public Property objWord() As Object
        Get
            Return m_objWord
        End Get
        Set(value As Object)
            m_objWord = value
        End Set
    End Property

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Open Send Secure Message screen in New mode.
    ''' </summary>
    ''' <param name="nPatientID">Current patient ID</param>
    ''' <param name="sConnectionString">Database connection string</param>
    ''' <param name="sClientMachineID">User machine ID</param>
    ''' <param name="sClientMachineName">Unique Id of Message</param>
    ''' <param name="PatientStatus">Patient Status (0 or 1 or 2)</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nPatientID As Int64, ByVal sConnectionString As String, ByVal sClientMachineID As String, ByVal sClientMachineName As String, Optional ByVal PatientStatus As Int16 = 1, Optional ByVal IsFrom As String = "", Optional ByVal strServiceURI As String = "", Optional ByVal strPatientPortalSiteNm As String = "", Optional ByVal nClinicID As Long = 0, Optional ByVal blnIntuitCommunication As Boolean = False, Optional ByVal nPatientEducationID As Long = 0)
        InitializeComponent()
        PatientID = nPatientID
        ConnectionString = sConnectionString
        ClientMachineID = sClientMachineID
        ClientMachineName = sClientMachineName
        _PatientStatus = PatientStatus
        _PortalType = IsFrom
        'MU2 Patient Portal
        _strServiceURI = strServiceURI
        _strPatientPortalSiteNm = strPatientPortalSiteNm
        _nClinicID = nClinicID
        _blnIntuitCommunication = blnIntuitCommunication
        'MU2 Patient Portal
        ''Added for MU2 Patinet Portal on 20130827
        If _PortalType = "gloPatientPortal" Then
            'pnlAttach.Visibility = Windows.Visibility.Hidden
            'chkAllowtoReply.Visibility = Windows.Visibility.Hidden
            pnPatientRepresentative.Visibility = Windows.Visibility.Visible
            'MainPanel.Height = 830 - 200
            txtMessage.MaxLength = 8000
            If CommDetailID = 0 Then
                chkAllowtoReply.IsChecked = True
            End If



        Else
            ''pnlAttach.Visibility = Windows.Visibility.Visible
            pnlPatientRequest.Visibility = Windows.Visibility.Visible
            pnlButtons.Visibility = Windows.Visibility.Visible
            pnllstAttachment.Visibility = Windows.Visibility.Visible
            lblIntuitAttachmentNote.Visibility = Windows.Visibility.Visible
            pnlVisitSummary.Visibility = Windows.Visibility.Visible
            pnlPatientRecord.Visibility = Windows.Visibility.Visible
            chkAllowtoReply.Visibility = Windows.Visibility.Visible
            pnPatientRepresentative.Visibility = Windows.Visibility.Collapsed
            'MainPanel.Height = 830
            txtMessage.MaxLength = 4000
        End If
        ''End
        AddPatientStrip()
        GetMessageCaption()

        If nPatientEducationID > 0 Then
            AttachPatientEducationMaterial(nPatientEducationID)
            intEducationID = nPatientEducationID
        End If

    End Sub

    ''' <summary>
    ''' Set nCommDetailID to open existing messages in read or resend mode.
    ''' </summary>
    ''' <param name="nPatientID">Current patient ID</param>
    ''' <param name="sConnectionString">Database connection string</param>
    ''' <param name="sClientMachineID">User machine ID</param>
    ''' <param name="sClientMachineName">Users machine name </param>
    ''' <param name="nCommDetailID">Unique Id of Message</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal nPatientID As Int64, ByVal sConnectionString As String, ByVal sClientMachineID As String, ByVal sClientMachineName As String, ByVal nCommDetailID As Long, Optional ByVal IsFrom As String = "", Optional ByVal strServiceURI As String = "", Optional ByVal strPatientPortalSiteNm As String = "", Optional ByVal nClinicID As Long = 0, Optional ByVal blnIntuitCommunication As Boolean = False)
        InitializeComponent()
        PatientID = nPatientID
        ConnectionString = sConnectionString
        ClientMachineID = sClientMachineID
        ClientMachineName = sClientMachineName
        CommDetailID = nCommDetailID
        _PortalType = IsFrom
        'MU2 Patient Portal
        _strServiceURI = strServiceURI
        _strPatientPortalSiteNm = strPatientPortalSiteNm
        _nClinicID = nClinicID
        _blnIntuitCommunication = blnIntuitCommunication
        'MU2 Patient Portal
        ''Added for MU2 Patinet Portal on 20130827
        If _PortalType = "gloPatientPortal" Then
            'pnlAttach.Visibility = Windows.Visibility.Hidden
            'chkAllowtoReply.Visibility = Windows.Visibility.Hidden
            pnPatientRepresentative.Visibility = Windows.Visibility.Visible
            '' MainPanel.Height = 830 - 200
            txtMessage.MaxLength = 8000
            If CommDetailID = 0 Then
                chkAllowtoReply.IsChecked = True
            End If
        Else
            ''pnlAttach.Visibility = Windows.Visibility.Visible
            pnlPatientRequest.Visibility = Windows.Visibility.Visible
            pnlButtons.Visibility = Windows.Visibility.Visible
            pnllstAttachment.Visibility = Windows.Visibility.Visible
            lblIntuitAttachmentNote.Visibility = Windows.Visibility.Visible
            pnlVisitSummary.Visibility = Windows.Visibility.Visible
            pnlPatientRecord.Visibility = Windows.Visibility.Visible
            chkAllowtoReply.Visibility = Windows.Visibility.Visible
            pnPatientRepresentative.Visibility = Windows.Visibility.Collapsed
            '' MainPanel.Height = 830
            txtMessage.MaxLength = 4000
        End If
        ''End
        AddPatientStrip()
        GetMessageCaption()



    End Sub


    ''' <summary>
    ''' Set nCommDetailID to open existing messages in read or resend mode.
    ''' </summary>
    ''' <param name="nPatientID">Current patient ID</param>
    ''' <param name="sConnectionString">Database connection string</param>
    ''' <param name="sClientMachineID">User machine ID</param>
    ''' <param name="sClientMachineName">Users machine name </param>
    ''' <param name="nCommDetailID">Unique Id of Message</param>
    ''' <remarks></remarks>
    ''' 
    Public Sub New(ByVal nPatientID As Int64, ByVal sConnectionString As String, ByVal sClientMachineID As String, ByVal sClientMachineName As String, ByVal nCommDetailID As Long, ByVal strServiceURI As String, ByVal strPatientPortalSiteNm As String, ByVal nClinicID As Long, ByVal blnIntuitCommunication As Boolean, Optional ByVal IsFrom As String = "")
        InitializeComponent()
        PatientID = nPatientID
        ConnectionString = sConnectionString
        ClientMachineID = sClientMachineID
        ClientMachineName = sClientMachineName
        CommDetailID = nCommDetailID
        _PortalType = IsFrom
        'MU2 Patient Portal
        _strServiceURI = strServiceURI
        _strPatientPortalSiteNm = strPatientPortalSiteNm
        _nClinicID = nClinicID
        _blnIntuitCommunication = blnIntuitCommunication
        'MU2 Patient Portal
        ''Added for MU2 Patinet Portal on 20130827
        If _PortalType = "gloPatientPortal" Then
            'pnlAttach.Visibility = Windows.Visibility.Hidden
            'chkAllowtoReply.Visibility = Windows.Visibility.Hidden
            pnPatientRepresentative.Visibility = Windows.Visibility.Visible
            '' MainPanel.Height = 830 - 200
            txtMessage.MaxLength = 8000
            If CommDetailID = 0 Then
                chkAllowtoReply.IsChecked = True
            End If


        Else
            '' pnlAttach.Visibility = Windows.Visibility.Visible
            pnlPatientRequest.Visibility = Windows.Visibility.Visible
            pnlButtons.Visibility = Windows.Visibility.Visible
            pnllstAttachment.Visibility = Windows.Visibility.Visible
            lblIntuitAttachmentNote.Visibility = Windows.Visibility.Visible
            pnlVisitSummary.Visibility = Windows.Visibility.Visible
            pnlPatientRecord.Visibility = Windows.Visibility.Visible
            chkAllowtoReply.Visibility = Windows.Visibility.Visible
            pnPatientRepresentative.Visibility = Windows.Visibility.Collapsed
            '' MainPanel.Height = 830
            txtMessage.MaxLength = 4000
        End If
        ''End
        AddPatientStrip()
        GetMessageCaption()
    End Sub




#End Region

#Region "Form Event"
    '18-Aug-2015 Aniket: Resolving Bug #84080: EMR: Send Portal- Send portal window goes back as user unlock EMR screen
    Private Sub frmSendSecureMsg_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

        IntPRT = gloWord.WordDialogBoxBackgroundCloser.GetMyActivatedHandle()

    End Sub

    Private Sub frmSendSecureMsg_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Try

            _flag = 0
            'TryCast(Wfh.Child, gloUC_PatientStrip).Height = PatientStrip.Height

            GetCommPreferenceList()

            SetContolEnableDisable()

            ''Added for MU2 Patient Portal - Send Secure message to Patient Representative on 20131224
            If _PortalType = "gloPatientPortal" Then
                GetPatRepresentativeList()
                cmbMessageSendTo.SelectedItem = DirectCast(cbiPatient, Object)
                cmbPRList.Visibility = Windows.Visibility.Collapsed
                'pnlAttach.Visibility = Windows.Visibility.Hidden
                pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
                pnlPatientRecord.Visibility = Windows.Visibility.Collapsed
                pnlPatientRequest.Visibility = Windows.Visibility.Collapsed
                '' pnlPatientRequest.Height = 0
                '' pnlPatientRecord.Height = 0

                ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                If cmbPRList.Items.Count = 0 Then
                    cmbMessageSendTo.Visibility = Windows.Visibility.Collapsed
                    lblPR.Visibility = Windows.Visibility.Collapsed
                    ''  pnPatientRepresentative.Height = 0
                End If
                cmbStaff.Visibility = Windows.Visibility.Collapsed
                lblFrom.Visibility = Windows.Visibility.Visible
                lblPortalDisplayName.Visibility = Windows.Visibility.Visible
                lblPortalDisplayNameValue.Visibility = Windows.Visibility.Visible
                ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
                ''Set check box unchecked by default and hide the label.
                chkCommPrefernce.IsChecked = False
                lblPatientPrefers.Visibility = Windows.Visibility.Collapsed

                'lblFrom.Width = "300"
                'lblPortalDisplayName.Width = "140"
                'lblPortalDisplayNameValue.Width = "300"
                'cmbStaff.Width = "0"

                '' ResizeMode = Windows.ResizeMode.CanResize
                '' SizeToContent = Windows.SizeToContent.Manual
                'Height = 600
                '' ResizeMode = Windows.ResizeMode.NoResize

                Dim _nuserID As String = appSettings("UserID").ToString()
                Dim dt As DataTable = Nothing
                dt = getLoginUserDetails(_nuserID)
                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    lblFrom.Content = dt.Rows(0)("LoginName").ToString()
                    lblFrom.Tag = dt.Rows(0)("nUserID").ToString()
                    ''Bug #68439: Portal Display Name not reflecting properly in EMR 
                    ''Replace label to textblock so change content to text.
                    lblPortalDisplayNameValue.Text = dt.Rows(0)("PortalDisplayName").ToString()
                End If
            Else
                ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                ''Fill the combo when patient portal is not enable.
                GetStaffList()
            End If

            ''End MU2 Patient Portal - Send Secure message to Patient Representative
            If _PatientStatus = 1 Then
                StackPanelNote.Visibility = Windows.Visibility.Collapsed
                _flag = 0
            Else
                GetPatientNameAndemail()
                _flag = 1
            End If

            'It opens selected message details
            If IsReplyMessage = True Then
                RetriveMessagedetails()
            Else
                If CommDetailID > 0 Then
                    cmbStaff.IsEnabled = False
                    btnSend.IsEnabled = False
                    txtSubject.IsReadOnly = True
                    chkAllowtoReply.IsEnabled = False
                    txtMessage.IsEnabled = True
                    txtMessage.IsReadOnly = True
                    chkvisitSummary.IsEnabled = False
                    chkPtRecord.IsEnabled = False
                    chkPerPtRequest.IsEnabled = False
                    btnAttachment.IsEnabled = False
                    btnDMS.IsEnabled = False
                    rbApprove.IsEnabled = False
                    rbCancel.IsEnabled = False
                    dtAppointmentDate.IsEnabled = False
                    cbHH.IsEnabled = False
                    cbMM.IsEnabled = False
                    cbMeridan.IsEnabled = False
                    If btnAttachment.IsEnabled = False Then
                        lblIntuitAttachmentNote.Visibility = Visibility.Collapsed
                    End If
                    RetriveMessagedetails()
                Else
                    ControlToHiddenForPatientMessaging()
                    pnlType.Visibility = Windows.Visibility.Collapsed
                End If

            End If
            If (chkCommPrefernce.IsChecked = True) Then
                cmbCommPrefrence.Visibility = Windows.Visibility.Visible
            Else
                cmbCommPrefrence.Visibility = Windows.Visibility.Collapsed
            End If
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        End Try

    End Sub

    Private Sub frmSendSecureMsg_KeyUp(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.KeyUp
        Dim frm As New Form
        Dim strPath As String
        Try
            If e.Key = Key.F1 Then
                strPath = System.Windows.Forms.Application.StartupPath & "\Help\gloEMR_User_Manual.chm"
                System.Windows.Forms.Help.ShowHelp(frm, strPath, "Work_with_Send_Secure_Message_to_Patient.htm")
            End If
        Catch ex As Exception
        Finally
            strPath = Nothing
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub

    Private Sub frmSendSecureMsg_Unloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Unloaded

        RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged

        If Not IsNothing(Wfh) Then
            RemoveHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
            Wfh.Dispose()
            Wfh = Nothing
        End If

        'set nothing to all declared variables
        _isMouseDown = Nothing
        oslectedBorder = Nothing
        PatientID = Nothing
        ConnectionString = Nothing
        ClientMachineID = Nothing
        ClientMachineName = Nothing
        CommDetailID = Nothing
        _SubjectFromReadMessage = Nothing
        _MessageFromReadMessage = Nothing
        _StaffDescriptionFromReadMessage = Nothing
        _IncludeMessageInReply = Nothing
        _IsReplyMessage = Nothing
        _PatientStatus = Nothing
        appSettings = Nothing
        gstrMessageBoxCaption = Nothing
        _lngContainerID = Nothing
        _strserviceType = Nothing
        IntPRT = System.IntPtr.Zero

    End Sub

#End Region

#Region "CheckBox Click Event"

    Private Sub chkvisitSummary_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkvisitSummary.Click
        If chkvisitSummary.IsChecked Then
            dtExamCCD.IsEnabled = True
            btnExamCCD.IsEnabled = True
            dtExamCCD.SelectedDate = DateTime.Now.Date
        Else
            dtExamCCD.IsEnabled = False
            btnExamCCD.IsEnabled = False
        End If
    End Sub

    Private Sub chkPtRecord_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkPtRecord.Click
        If chkPtRecord.IsChecked Then
            dtFromPtCCD.IsEnabled = True
            dtToPtCCD.IsEnabled = True
            btnPtCCD.IsEnabled = True
            chkPerPtRequest.IsEnabled = True
            dtFromPtCCD.SelectedDate = DateTime.Now.Date
            dtToPtCCD.SelectedDate = DateTime.Now.Date
        Else
            dtFromPtCCD.IsEnabled = False
            dtToPtCCD.IsEnabled = False
            chkPerPtRequest.IsEnabled = False
            btnPtCCD.IsEnabled = False
        End If
    End Sub

#End Region

#Region "SelectionChange Event"

    Private Sub lstAttachment_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) 'Handles lstLabTest.SelectionChanged
        Try
            If lstAttachment.Items.Count > 0 Then


                Dim oitem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                oslectedBorder = oitem
            End If
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        End Try
    End Sub

#End Region

#Region "Button Click"

    ''' <summary>
    ''' Saving message record in Gl_Messagequeue, IntuitCommDetails, IntuitCommAttachments tables.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSend.Click
        ''Added for MU2 Patient Portal - If Intuit setting is turn on then send secure message to Intuit Portal else gloStream Patient Portal on 20130823
        nProviderID = Convert.ToInt64(Global.gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(PatientID))
        If Not getProviderTaxID(nProviderID) Then
            Exit Sub
        End If

        If _PortalType = String.Empty Then
            IntuitSendSecureMsg()
        ElseIf _PortalType = "gloPatientPortal" Then
            PatientPortalSendSecureMsg()
        End If

    End Sub

    Private Sub IntuitSendSecureMsg()
        Dim dtIntuitCommAttachments As DataTable
        dtIntuitCommAttachments = New DataTable("TVP_IntuitCommAttachments")

        Dim dtGl_Messagequeue As DataTable
        dtGl_Messagequeue = New DataTable("TVP_Gl_Messagequeue")

        Dim dtIntuitCommDetails As DataTable
        dtIntuitCommDetails = New DataTable("TVP_IntuitCommDetails")

        Dim MessageID As Int64 = 0

        Try

            If GetPracticeID() = 0 Then
                System.Windows.MessageBox.Show("There is no Practice ID added in the system which is needed to send a mail. This can be added from the Patient Portal service configuration settings or gloEMR admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If cmbStaff.SelectedValue = 0 Then
                System.Windows.MessageBox.Show("Please select the Staff ID.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If txtSubject.Text = "" Then
                txtSubject.Focus()
                System.Windows.MessageBox.Show("Please enter the subject.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Len(txtSubject.Text) > 100 Then
                txtSubject.Focus()
                System.Windows.MessageBox.Show("The current subject length is " + Len(txtSubject.Text).ToString + ". The subject length must be less than 100 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            '06-Dec-13 Aniket: Resolving Bug #59613:
            If cmbCommPrefrence.Visibility <> Windows.Visibility.Collapsed Then

                If cmbCommPrefrence.Text = "" Then
                    System.Windows.MessageBox.Show("Please select the Patient Communication.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbCommPrefrence.Focus()
                    Exit Sub
                End If

            End If

            If txtMessage.Text = "" Then
                txtMessage.Focus()
                System.Windows.MessageBox.Show("Please enter the message.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Len(txtMessage.Text) > 4000 Then
                txtMessage.Focus()
                System.Windows.MessageBox.Show("The current message length is " + Len(txtMessage.Text).ToString + ". The message length must be less than 4000 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If _strserviceType.ToUpper() = "APPOINTMENT REQUEST" Then
                If (rbApprove.IsChecked) Then
                    _Status = 1
                    If ((cbHH.SelectedIndex = 0) Or (cbMM.SelectedIndex = 0) Or (cbMeridan.SelectedIndex = 0)) Then
                        System.Windows.MessageBox.Show("Please select appointment time.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    If dtAppointmentDate.SelectedDate.HasValue = False Then
                        System.Windows.MessageBox.Show("Please select appointment date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If
                If (rbCancel.IsChecked) Then
                    _Status = 2
                End If
            End If

            If _strserviceType.ToUpper() = "PRESCRIPTION RENEWAL" Then
                If (rbApprove.IsChecked) Then
                    _Status = 1
                Else
                    _Status = 2
                End If
            End If


            'Master Entry of Message
            dtGl_Messagequeue = GetGl_Messagequeue()

            'Get Intuit Message Details
            dtIntuitCommDetails = GetIntuitCommDetails()

            'Get Intuit Attachment Record
            dtIntuitCommAttachments = GetIntuitCommAttachments()

            MessageID = SaveMessage(dtGl_Messagequeue, dtIntuitCommDetails, dtIntuitCommAttachments)

            If MessageID > 0 Then
                Dim strMessage As String = ""
                UpdateEducationSendToPortal(intEducationID)

                If _PortalType = "gloPatientPortal" Then
                    Select Case _strserviceType.ToUpper

                        Case "APPOINTMENT REQUEST"
                            strMessage = "Patient Portal appointment request message sent"
                            'If _Status = 1 Then
                            '    strMessage = "Intuit appointment request approved"
                            'ElseIf _Status = 2 Then
                            '    strMessage = "Intuit appointment request denied"
                            'End If
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case "PRESCRIPTION RENEWAL"
                            If _Status = 1 Then
                                strMessage = "Patient Portal prescription renewal approved"
                            ElseIf _Status = 2 Then
                                strMessage = "Patient Portal prescription renewal denied"
                            End If
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case "ONLINE BILL PAY"
                            strMessage = "Patient Portal online bill pay message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            '15-Apr-13 Aniket: Showing 'Ask A Doctor' subtype
                        Case "ASK A DOCTOR", "ASK A STAFF", "ASK A QUESTION", "ASK A BILLER", "ASK A NURSE"
                            strMessage = "Patient Portal ask a doctor message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case "PATIENT MESSAGING"
                            strMessage = "Patient Portal patient message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case Else
                            strMessage = "Patient Portal patient message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End Select
                Else
                    Select Case _strserviceType.ToUpper

                        Case "APPOINTMENT REQUEST"
                            If _Status = 1 Then
                                strMessage = "Intuit appointment request approved"
                            ElseIf _Status = 2 Then
                                strMessage = "Intuit appointment request denied"
                            End If
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case "PRESCRIPTION RENEWAL"
                            If _Status = 1 Then
                                strMessage = "Intuit prescription renewal approved"
                            ElseIf _Status = 2 Then
                                strMessage = "Intuit prescription renewal denied"
                            End If
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case "ONLINE BILL PAY"
                            strMessage = "Intuit online bill pay message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            '15-Apr-13 Aniket: Showing 'Ask A Doctor' subtype
                        Case "ASK A DOCTOR", "ASK A STAFF", "ASK A QUESTION", "ASK A BILLER", "ASK A NURSE"
                            strMessage = "Intuit ask a doctor message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Reply, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case "PATIENT MESSAGING"
                            strMessage = "Intuit patient message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Case Else
                            strMessage = "Intuit patient message sent"
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, strMessage, PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End Select
                End If
                If MessageID <> 0 Then
                    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MessageID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.SendSecureMessageMessageID.GetHashCode())
                    oclsselectProviderTaxID = Nothing
                End If
                Me.Close()
            End If
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, ex.ToString(), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

            If Not IsNothing(dtIntuitCommAttachments) Then
                dtIntuitCommAttachments.Dispose() : dtIntuitCommAttachments = Nothing
            End If

            If Not IsNothing(dtGl_Messagequeue) Then
                dtGl_Messagequeue.Dispose() : dtGl_Messagequeue = Nothing
            End If

            If Not IsNothing(dtIntuitCommDetails) Then
                dtIntuitCommDetails.Dispose() : dtIntuitCommDetails = Nothing
            End If

            MessageID = Nothing
        End Try
    End Sub
#Region "MU2 Patient Portal"
    Private Sub PatientPortalSendSecureMsg()
        Dim dtIntuitCommAttachments As DataTable
        dtIntuitCommAttachments = New DataTable("TVP_IntuitCommAttachments")

        Dim dtGl_Messagequeue As DataTable
        dtGl_Messagequeue = New DataTable("TVP_Gl_Messagequeue")

        Dim dtIntuitCommDetails As DataTable
        dtIntuitCommDetails = New DataTable("TVP_IntuitCommDetails")

        Dim MessageID As Int64 = 0

        Try

            If GetPracticeID() = 0 Then
                System.Windows.MessageBox.Show("There is no Practice ID added in the system which is needed to send a mail. This can be added from the Patient Portal service configuration settings or gloEMR admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Commented the validation for combobox as it is not used for Portal.
            'If cmbStaff.SelectedValue = 0 Then
            '    System.Windows.MessageBox.Show("Please select the Staff ID.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Exit Sub
            'End If

            If txtSubject.Text.ToString().Trim() = "" Then
                txtSubject.Focus()
                System.Windows.MessageBox.Show("Please enter the subject.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Len(txtSubject.Text) > 100 Then
                txtSubject.Focus()
                System.Windows.MessageBox.Show("The current subject length is " + Len(txtSubject.Text).ToString + ". The subject length must be less than 100 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            If txtMessage.Text.ToString().Trim() = "" Then
                txtMessage.Focus()
                System.Windows.MessageBox.Show("Please enter the message.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Len(txtMessage.Text) > 8000 Then
                txtMessage.Focus()
                System.Windows.MessageBox.Show("The current message length is " + Len(txtMessage.Text).ToString + ". The message length must be less than 8000 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Master Entry of Message
            dtGl_Messagequeue = GetGl_Messagequeue()

            'Get Patient portal Message Details
            dtIntuitCommDetails = GetPatientPortalCommDetails()

            'Get Patient portal Attachment Record
            dtIntuitCommAttachments = GetIntuitCommAttachments()
            Dim nPRID As Long = 0
            Dim selectedTag = DirectCast(cmbMessageSendTo.SelectedItem, ComboBoxItem).Tag.ToString()
            If (selectedTag = 1 Or selectedTag = 2) Then
                nPRID = cmbPRList.SelectedValue
            End If
            MessageID = Portal_SaveMessage(dtGl_Messagequeue, dtIntuitCommDetails, dtIntuitCommAttachments, nPRID, selectedTag)

            If MessageID > 0 Then
                UpdateEducationSendToPortal(intEducationID)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, "Patient Portal Secure Message Sent", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            If (selectedTag = 0 Or selectedTag = 2) Then

                If _PortalType = "gloPatientPortal" And _blnIntuitCommunication Then
                    Dim clsgloPatientPortalEmail As New gloPatientPortalCommon.clsgloPatientPortalEmail(ConnectionString) ''Convert.ToString(System.Configuration.ConfigurationManager.AppSettings.Get("EmailSeviceUri"))
                    Dim MailSend As Boolean = clsgloPatientPortalEmail.SendEmail(PatientID, _strServiceURI, _strPatientPortalSiteNm, _nClinicID, "MESSAGING", False)
                    Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then
                            Try
                                Dim str As String = ""
                                If MailSend Then
                                    str = " update gl_MessageQueue set nstatus = 0,dtEmailSent = '" + DateTime.Now.ToString() + "'  WHERE nMessageID=  " + MessageID.ToString()
                                Else
                                    str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + MessageID.ToString()
                                End If
                                oDB.Execute_Query(str)
                            Catch ex As gloDatabaseLayer.DBException
                                ex.ERROR_Log(ex.ToString())
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                            Finally
                                If oDB IsNot Nothing Then
                                    oDB.Disconnect()
                                    oDB.Dispose()
                                    oDB = Nothing
                                End If
                            End Try
                        End If
                    End If


                End If

            End If

            If (selectedTag = 1 Or selectedTag = 2) Then

                If _PortalType = "gloPatientPortal" And _blnIntuitCommunication Then
                    Dim clsgloPatientPortalEmail As New gloPatientPortalCommon.clsgloPatientPortalEmail(ConnectionString) ''Convert.ToString(System.Configuration.ConfigurationManager.AppSettings.Get("EmailSeviceUri"))
                    Dim MailSend As Boolean = clsgloPatientPortalEmail.SendEmail_PR(PatientID, _strServiceURI, _strPatientPortalSiteNm, _nClinicID, "MESSAGING", nPRID, True)
                    Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
                    If oDB IsNot Nothing Then
                        If oDB.Connect(False) Then
                            Try
                                Dim str As String = ""
                                If MailSend Then
                                    str = " update gl_MessageQueue set nstatus = 0,dtEmailSent = '" + DateTime.Now.ToString() + "'  WHERE nMessageID=  " + MessageID_PR.ToString()
                                Else
                                    str = " update gl_MessageQueue set nstatus = 2  WHERE nMessageID=  " + MessageID_PR.ToString()
                                End If
                                oDB.Execute_Query(str)
                            Catch ex As gloDatabaseLayer.DBException
                                ex.ERROR_Log(ex.ToString())
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                            Finally
                                If oDB IsNot Nothing Then
                                    oDB.Disconnect()
                                    oDB.Dispose()
                                    oDB = Nothing
                                End If
                            End Try
                        End If
                    End If


                End If

            End If
            If MessageID <> 0 Then
                Dim nCommDetailID = getCommDetailsID(MessageID)
                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MessageID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.SendSecureMessageMessageID.GetHashCode())
                oclsselectProviderTaxID = Nothing

                oclsselectProviderTaxID = New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, nCommDetailID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.SendSecureMessageCommDetailID.GetHashCode())
                oclsselectProviderTaxID = Nothing
            End If

            Me.Close()


        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.SecureMessage, gloAuditTrail.ActivityType.Send, ex.ToString(), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

            If Not IsNothing(dtIntuitCommAttachments) Then
                dtIntuitCommAttachments.Dispose() : dtIntuitCommAttachments = Nothing
            End If

            If Not IsNothing(dtGl_Messagequeue) Then
                dtGl_Messagequeue.Dispose() : dtGl_Messagequeue = Nothing
            End If

            If Not IsNothing(dtIntuitCommDetails) Then
                dtIntuitCommDetails.Dispose() : dtIntuitCommDetails = Nothing
            End If

            MessageID = Nothing
        End Try
    End Sub

    Private Sub UpdateEducationSendToPortal(EducationID As Int64)

        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)

            oDBParameters.Add("@nEducationID", EducationID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Execute("gsp_UpdateEducationSendToPortal", oDBParameters)
            oDB.Disconnect()

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)

        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

        End Try

    End Sub

    Private Function GetPatientPortalCommDetails() As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommDetails")

        'declaring one row for the table
        Dim Row1 As DataRow

        Dim sCommSubject As DataColumn
        Dim sCommMessage As DataColumn
        Dim bCommMemberReply As DataColumn
        Dim nPatientID As DataColumn
        Dim nCommStaffID As DataColumn
        Dim sCommServiceType As DataColumn
        Dim nCommContainerID As DataColumn
        Dim dAppDateTime As DataColumn
        Dim nStatus As DataColumn
        Dim nCommunicationType As DataColumn
        Dim bisUnscheduledCare As DataColumn
        ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
        ''Added new column to pass value for TVP_IntuitCommDetails.
        Dim nUserID As DataColumn

        Try
            'declare a column 
            sCommSubject = New DataColumn("sCommSubject")
            sCommSubject.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommSubject)

            sCommMessage = New DataColumn("sCommMessage")
            sCommMessage.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommMessage)

            bCommMemberReply = New DataColumn("bCommMemberCannotReply")
            bCommMemberReply.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bCommMemberReply)

            nPatientID = New DataColumn("nPatientID")
            nPatientID.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(nPatientID)

            nCommStaffID = New DataColumn("nCommStaffID")
            nCommStaffID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommStaffID)

            sCommServiceType = New DataColumn("sCommServiceType")
            sCommServiceType.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommServiceType)

            nCommContainerID = New DataColumn("nCommContainerID")
            nCommContainerID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommContainerID)

            dAppDateTime = New DataColumn("dAppDateTime")
            dAppDateTime.DataType = System.Type.GetType("System.DateTime")
            dt.Columns.Add(dAppDateTime)

            nStatus = New DataColumn("nStatus")
            nStatus.DataType = System.Type.GetType("System.Int16")
            dt.Columns.Add(nStatus)

            bisUnscheduledCare = New DataColumn("bisUnscheduledCare")
            bisUnscheduledCare.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bisUnscheduledCare)

            nCommunicationType = New DataColumn("nCommunicationType")
            nCommunicationType.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommunicationType)

            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            nUserID = New DataColumn("nUserID")
            nCommStaffID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nUserID)


            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sCommSubject") = txtSubject.Text
            Row1.Item("sCommMessage") = txtMessage.Text
            Row1.Item("bCommMemberCannotReply") = chkAllowtoReply.IsChecked
            Row1.Item("nPatientID") = PatientID
            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Pass value as 0 as nCommStaffID is not used for portal.
            Row1.Item("nCommStaffID") = 0
            'Row1.Item("nCommStaffID") = lblFrom.Tag
            Row1.Item("bisUnscheduledCare") = chkCommPrefernce.IsChecked

            If chkCommPrefernce.IsChecked = True Then
                Row1.Item("nCommunicationType") = cmbCommPrefrence.SelectedValue
            Else
                Row1.Item("nCommunicationType") = 0
            End If

            If IsReplyMessage = False Then
                Row1.Item("sCommServiceType") = "Patient Messaging"
                Row1.Item("nCommContainerID") = 0
            Else
                Row1.Item("sCommServiceType") = _strserviceType
                Row1.Item("nCommContainerID") = _lngContainerID
            End If
            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            Row1.Item("nUserID") = lblFrom.Tag

            ''Appointment Request
            'If _strserviceType.ToUpper() = "APPOINTMENT REQUEST" Then
            '    If (rbApprove.IsChecked = True) Then
            '        Dim sDates As String = dtAppointmentDate.SelectedDate & " " & cbHH.Text.ToString() & ":" & cbMM.Text.ToString() & " " & cbMeridan.Text.ToString()
            '        _AppnDate = DateTime.Parse(sDates)
            '        sDates = String.Empty
            '        Row1.Item("dAppDateTime") = _AppnDate
            '        Dim sAttach As String = String.Empty
            '        sAttach = txtMessage.Text
            '        If (_AppnDate.ToString() <> "") Then

            '            sAttach = sAttach.Replace("[DAY]", Format(dtAppointmentDate.SelectedDate, "MM/dd/yyyy"))
            '            sAttach = sAttach.Replace("[TIME]", cbHH.Text.ToString() & ":" & cbMM.Text.ToString() & " " & cbMeridan.Text.ToString())
            '            If (sAttach.Length > 4000) Then
            '                sAttach = sAttach.Substring(0, 4000)
            '            End If
            '            Row1.Item("sCommMessage") = sAttach.Trim()
            '        End If
            '    Else
            '        Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
            '    End If
            '    Row1.Item("nStatus") = _Status
            'End If

            'If _strserviceType.ToUpper() = "PRESCRIPTION RENEWAL" Then
            '    Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
            '    Row1.Item("nStatus") = _Status

            'End If

            'If _strserviceType.ToUpper() = "ONLINE BILL PAY" Then
            '    Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
            '    Row1.Item("nStatus") = 0
            '    Row1.Item("sCommServiceType") = "Patient Messaging"
            '    Row1.Item("nCommContainerID") = 0

            'End If

            '15-Apr-13 Aniket: Showing 'Ask A Doctor' subtype
            If (_strserviceType.ToUpper() = "PATIENT MESSAGING" Or _strserviceType.ToUpper() = "ASK A PROVIDER" Or _strserviceType.ToUpper() = "ASK A STAFF" Or _strserviceType.ToUpper() = "REFILL REQUEST" Or _strserviceType.ToUpper() = "APPOINTMENT REQUEST") Then
                'Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
                Row1.Item("nStatus") = 0
            End If

            'adding the completed row to the table
            dt.Rows.Add(Row1)

            If Not IsNothing(sCommSubject) Then
                sCommSubject.Dispose() : sCommSubject = Nothing
            End If

            If Not IsNothing(sCommMessage) Then
                sCommMessage.Dispose() : sCommMessage = Nothing
            End If

            If Not IsNothing(bCommMemberReply) Then
                bCommMemberReply.Dispose() : bCommMemberReply = Nothing
            End If

            If Not IsNothing(nPatientID) Then
                nPatientID.Dispose() : nPatientID = Nothing
            End If

            If Not IsNothing(nCommStaffID) Then
                nCommStaffID.Dispose() : nCommStaffID = Nothing
            End If

            If Not IsNothing(sCommServiceType) Then
                sCommServiceType.Dispose() : sCommServiceType = Nothing
            End If

            If Not IsNothing(nCommContainerID) Then
                nCommContainerID.Dispose() : nCommContainerID = Nothing
            End If

            If Not IsNothing(dAppDateTime) Then
                dAppDateTime.Dispose() : dAppDateTime = Nothing
            End If

            If Not IsNothing(nStatus) Then
                nStatus.Dispose() : nStatus = Nothing
            End If

            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            If Not IsNothing(nUserID) Then
                nUserID.Dispose() : nUserID = Nothing
            End If
            Return dt


        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function
#End Region
    ''' <summary>
    ''' Creates CCD file of Visit and Insert it in Listbox item.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExamCCD_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnExamCCD.Click
        Dim strCCDSection As String = String.Empty
        Dim strFilePath As String
        Dim strCCDfilePath As String
        Dim ogloInterface As New gloCCDLibrary.gloCCDInterface

        Try

            If dtExamCCD.SelectedDate.HasValue = False Then
                System.Windows.MessageBox.Show("Please select the date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


            strCCDSection = CheckVisitCCDSection()
            strCCDfilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath.ToString()

            If Directory.Exists(strCCDfilePath) = True Then
                strCCDfilePath = strCCDfilePath + "Visit Summary " & Format(dtExamCCD.SelectedDate, "MM-dd-yyyy") & ".xml"


                'generating ccd file on path stored in [Admin - CCD Setting - CCD file path]
                'strFilePath = ogloInterface.GenerateClinicalInformation(PatientID, 1, strCCDSection, 0, dtExamCCD.SelectedDate, dtExamCCD.SelectedDate, strCCDfilePath)
                strFilePath = ogloInterface.GenerateClinicalInformationold(PatientID, 1, strCCDSection, 0, dtFromPtCCD.SelectedDate, dtToPtCCD.SelectedDate, strCCDfilePath)
                ''Added old method for case CAS-22862-B2C8D4
                'inserting record in CCD_Exported_Files table 
                ogloInterface.SaveExportedCCD(PatientID, strCCDfilePath, "CCD File Saved", "", False)

                Dim info As New FileInfo(strFilePath)
                ' Get the size of the file in bytes.
                Dim Bytes As Long = info.Length

                Dim dblConvertSize As Double
                dblConvertSize = GetConvertedBytesSize(Bytes)

                If dblConvertSize > 2.0 Then
                    System.Windows.MessageBox.Show("File size of '" & "Visit Summary " & Format(dtExamCCD.SelectedDate, "MM-dd-yyyy") & "' is " & SetBytes(Bytes) & ". File size should not exceed 2 MB.", gstrMessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information)
                    Exit Sub
                End If

                AddListBoxItem("Visit Summary " & Format(dtExamCCD.SelectedDate, "MM-dd-yyyy") & ".xml", strFilePath)
                Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)
                Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
                Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
                TBlock.Tag = arrByte

                info = Nothing
                arrByte = Nothing
                dblConvertSize = Nothing
                TBlock = Nothing
                stac = Nothing
                Innerstac = Nothing
                obd = Nothing

            Else
                System.Windows.MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            strCCDSection = Nothing
            strFilePath = Nothing
            strCCDfilePath = Nothing

            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose() : ogloInterface = Nothing
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' Opens 'Open File Dialog box to select file and adds it in listbox item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAttachment_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAttachment.Click
        Dim strFileName As String = ""
        Dim strFilePath As String = ""
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim ogloInterface As New gloCCDLibrary.gloCCDInterface

        Try

            fd.Title = "Open File Dialog"
            fd.InitialDirectory = "C:\"
            fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
            fd.FilterIndex = 2
            fd.RestoreDirectory = True
            fd.Multiselect = False
            fd.ShowDialog()

            If fd.FileName <> "" Then
                strFilePath = fd.FileName
                strFileName = fd.SafeFileName

                Dim info As New FileInfo(strFilePath)
                ' Get the size of the file in bytes.
                Dim Bytes As Long = info.Length

                Dim dblConvertSize As Double
                dblConvertSize = GetConvertedBytesSize(Bytes)

                If dblConvertSize > 2.0 Then
                    System.Windows.MessageBox.Show("File size of '" & strFileName & "' is " & SetBytes(Bytes) & ". File size should not exceed 2 MB.", gstrMessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information)
                    Exit Sub
                End If

                AddListBoxItem(strFileName, strFilePath)

                Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)

                Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
                Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
                TBlock.Tag = arrByte

                info = Nothing
                arrByte = Nothing
                dblConvertSize = Nothing
                TBlock = Nothing
                stac = Nothing
                Innerstac = Nothing
                obd = Nothing

            End If
            btnAttachment.Focus()

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            strFileName = Nothing
            strFilePath = Nothing

            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose() : ogloInterface = Nothing
            End If

            If Not IsNothing(fd) Then
                fd.Dispose() : fd = Nothing
            End If

        End Try

    End Sub

    ''' <summary>
    '''  Creates CCD file of patient all record and Insert it in Listbox item.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPtCCD_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPtCCD.Click
        Dim strCCDSection As String = String.Empty
        Dim strFilePath As String
        Dim strCCDfilePath As String
        Dim ogloInterface As New gloCCDLibrary.gloCCDInterface

        Try

            If dtFromPtCCD.SelectedDate.HasValue = False And dtToPtCCD.SelectedDate.HasValue = False Then
                System.Windows.MessageBox.Show("Please select From and To date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            If dtFromPtCCD.SelectedDate.HasValue = False Then
                System.Windows.MessageBox.Show("Please select from date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            If dtToPtCCD.SelectedDate.HasValue = False Then
                System.Windows.MessageBox.Show("Please select to date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            strCCDSection = CheckPatientCCDSections()
            strCCDfilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath.ToString()

            If Directory.Exists(strCCDfilePath) = True Then
                strCCDfilePath = strCCDfilePath + "Patient Record " & Format(dtFromPtCCD.SelectedDate, "MM-dd-yyyy") & " To " & Format(dtToPtCCD.SelectedDate, "MM-dd-yyyy") & ".xml"


                'generating ccd file on path stored in [Admin - CCD Setting - CCD file path]
                strFilePath = ogloInterface.GenerateClinicalInformation(PatientID, 1, strCCDSection, 0, dtFromPtCCD.SelectedDate, dtToPtCCD.SelectedDate, strCCDfilePath)


                'inserting record in CCD_Exported_Files table 
                ogloInterface.SaveExportedCCD(PatientID, strCCDfilePath, "CCD File Saved", "", chkPerPtRequest.IsChecked)


                Dim info As New FileInfo(strFilePath)
                ' Get the size of the file in bytes.
                Dim Bytes As Long = info.Length

                Dim dblConvertSize As Double
                dblConvertSize = GetConvertedBytesSize(Bytes)

                If dblConvertSize > 2.0 Then
                    System.Windows.MessageBox.Show("File size of '" & "Patient Record " & Format(dtFromPtCCD.SelectedDate, "MM-dd-yyyy") & " To " & Format(dtToPtCCD.SelectedDate, "MM-dd-yyyy") & "' is " & SetBytes(Bytes) & ". File size should not exceed 2 MB.", gstrMessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information)
                    Exit Sub
                End If

                AddListBoxItem("Patient Record " & Format(dtFromPtCCD.SelectedDate, "MM-dd-yyyy") & " To " & Format(dtToPtCCD.SelectedDate, "MM-dd-yyyy") & ".xml", strFilePath)
                Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)
                Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
                Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
                TBlock.Tag = arrByte

                info = Nothing
                arrByte = Nothing
                dblConvertSize = Nothing
                TBlock = Nothing
                stac = Nothing
                Innerstac = Nothing
                obd = Nothing

            Else
                System.Windows.MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            strCCDSection = Nothing
            strFilePath = Nothing
            strCCDfilePath = Nothing

            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose() : ogloInterface = Nothing
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub btnDMS_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDMS.Click
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim ogloInterface As New gloCCDLibrary.gloCCDInterface
        Try
            Dim _FilePaths As ArrayList = Nothing
            Dim _DocumentNames As ArrayList = Nothing
            If (lstAttachment.Items.Count = 3) Then
                System.Windows.MessageBox.Show("You can attach only three files.", gstrMessageBoxCaption, Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information)
                Exit Sub
            End If


            'using for split screen
            oScanDocument.oPatientExam = m_objPatientExam
            oScanDocument.oPatientMessages = m_objPatientMessages
            oScanDocument.oPatientLetters = m_objPatientLetters
            oScanDocument.oNurseNotes = m_objNurseNotes
            oScanDocument.oHistory = m_objHistory
            oScanDocument.oLabs = m_objLabs
            oScanDocument.oDMS = m_objDMS
            oScanDocument.oRxmed = m_objRxmed
            oScanDocument.oOrders = m_objOrders
            oScanDocument.oProblemList = m_objProblemList
            oScanDocument.oCriteria = m_objCriteria
            oScanDocument.oWord = m_objWord

            oScanDocument.ShowEDocument_IntuitMessage(PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.IntuitMessage, lstAttachment.Items.Count, _FilePaths, _DocumentNames)

            If Not IsNothing(_FilePaths) Then
                For i As Int16 = 0 To _FilePaths.Count - 1
                    If File.Exists(_FilePaths(i).ToString()) = True Then
                        'Addded Ext. to open the file in PDF format when double click on list item.

                        If _DocumentNames(i).ToString.Contains(".pdf") Then
                            AddListBoxItem(_DocumentNames(i).ToString(), _FilePaths(i).ToString())
                        Else
                            AddListBoxItem(_DocumentNames(i).ToString() & ".pdf", _FilePaths(i).ToString())
                        End If

                        Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(_FilePaths(i).ToString())
                        Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
                        Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                        Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                        Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
                        TBlock.Tag = arrByte

                        arrByte = Nothing
                        TBlock = Nothing
                        stac = Nothing
                        Innerstac = Nothing
                        obd = Nothing

                    End If

                Next
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose() : ogloInterface = Nothing
            End If
            If Not IsNothing(oScanDocument) Then
                oScanDocument.Dispose() : oScanDocument = Nothing
            End If
            Dim sFilepath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\IntuitDocs"
            If Directory.Exists(sFilepath) = True Then
                Directory.Delete(sFilepath, True)
            End If
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region



#Region "Function"

    ''' <summary>
    ''' Creates a data table match with tvp structure to insert attachemnt record in table.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIntuitCommAttachments()
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommAttachments")

        'declaring one row for the table
        Dim Row1 As DataRow
        Dim sCommAttachOneName As DataColumn
        Dim sCommAttachTwoName As DataColumn
        Dim sCommAttachThreeName As DataColumn
        Dim ICommAttachOne As DataColumn
        Dim ICommAttachTwo As DataColumn
        Dim ICommAttachThree As DataColumn

        Try
            'declare a column for first attachment name
            sCommAttachOneName = New DataColumn("sCommAttachOneName")
            sCommAttachOneName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachOneName)

            'declare a column for second attachment name
            sCommAttachTwoName = New DataColumn("sCommAttachTwoName")
            sCommAttachTwoName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachTwoName)

            'declare a column for third attachment name 
            sCommAttachThreeName = New DataColumn("sCommAttachThreeName")
            sCommAttachThreeName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachThreeName)

            'declare a column for first attachment
            ICommAttachOne = New DataColumn("ICommAttachOne")
            ICommAttachOne.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachOne)

            'declare a column for second attachment
            ICommAttachTwo = New DataColumn("ICommAttachTwo")
            ICommAttachTwo.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachTwo)

            'declare a column for third attachment
            ICommAttachThree = New DataColumn("ICommAttachThree")
            ICommAttachThree.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachThree)

            If lstAttachment.Items.Count > 0 Then
                Dim i As Integer
                'declaring a new row
                Row1 = dt.NewRow()
                For i = 0 To lstAttachment.Items.Count - 1

                    If i = 0 Then
                        Row1.Item("sCommAttachOneName") = GetAttachmentFileName(i)
                        Row1.Item("ICommAttachOne") = GetAttachmentFile(i)
                    End If

                    If i = 1 Then
                        Row1.Item("sCommAttachTwoName") = GetAttachmentFileName(i)
                        Row1.Item("ICommAttachTwo") = GetAttachmentFile(i)
                    End If

                    If i = 2 Then
                        Row1.Item("sCommAttachThreeName") = GetAttachmentFileName(i)
                        Row1.Item("ICommAttachThree") = GetAttachmentFile(i)
                    End If

                Next
                'adding the completed row to the table
                dt.Rows.Add(Row1)
            End If

            If Not IsNothing(sCommAttachOneName) Then
                sCommAttachOneName.Dispose() : sCommAttachOneName = Nothing
            End If

            If Not IsNothing(sCommAttachTwoName) Then
                sCommAttachTwoName.Dispose() : sCommAttachTwoName = Nothing
            End If

            If Not IsNothing(sCommAttachThreeName) Then
                sCommAttachThreeName.Dispose() : sCommAttachThreeName = Nothing
            End If

            If Not IsNothing(ICommAttachOne) Then
                ICommAttachOne.Dispose() : ICommAttachOne = Nothing
            End If

            If Not IsNothing(ICommAttachTwo) Then
                ICommAttachTwo.Dispose() : ICommAttachTwo = Nothing
            End If

            If Not IsNothing(ICommAttachThree) Then
                ICommAttachThree.Dispose() : ICommAttachThree = Nothing
            End If

            Return dt

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Retunes attached file name.
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAttachmentFileName(ByVal i As Integer) As String
        Dim strFileName As String
        Try
            Dim obd As Border = DirectCast(lstAttachment.Items.Item(i), Border)
            Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
            Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
            Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
            strFileName = TBlock.Text

            TBlock = Nothing
            stac = Nothing
            Innerstac = Nothing
            obd = Nothing

            Return strFileName

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally
            strFileName = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Retunes attached file in byte() format.
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAttachmentFile(ByVal i As Integer) As Byte()
        Dim arrByte As Byte()

        Try
            Dim obd As Border = DirectCast(lstAttachment.Items.Item(i), Border)
            Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
            Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
            Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
            arrByte = TBlock.Tag

            TBlock = Nothing
            stac = Nothing
            Innerstac = Nothing
            obd = Nothing

            Return arrByte

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally
            arrByte = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Creates a data table match with tvp structure to insert Message master record in table.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGl_Messagequeue() As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_Gl_Messagequeue")

        'declaring one row for the table
        Dim Row1 As DataRow

        Dim contactID As DataColumn
        Dim fname As DataColumn
        Dim lname As DataColumn

        Try
            'declare a column 
            contactID = New DataColumn("sMachineID")
            contactID.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(contactID)

            fname = New DataColumn("sMachineName")
            fname.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(fname)

            lname = New DataColumn("nPatientID")
            lname.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(lname)

            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sMachineID") = ClientMachineID
            Row1.Item("sMachineName") = ClientMachineName
            Row1.Item("nPatientID") = PatientID

            'adding the completed row to the table
            dt.Rows.Add(Row1)

            If Not IsNothing(contactID) Then
                contactID.Dispose() : contactID = Nothing
            End If

            If Not IsNothing(fname) Then
                fname.Dispose() : fname = Nothing
            End If

            If Not IsNothing(lname) Then
                lname.Dispose() : lname = Nothing
            End If

            Return dt

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Creates a data table match with tvp structure to insert Message record in table.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIntuitCommDetails() As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommDetails")

        'declaring one row for the table
        Dim Row1 As DataRow

        Dim sCommSubject As DataColumn
        Dim sCommMessage As DataColumn
        Dim bCommMemberReply As DataColumn
        Dim nPatientID As DataColumn
        Dim nCommStaffID As DataColumn
        Dim sCommServiceType As DataColumn
        Dim nCommContainerID As DataColumn
        Dim dAppDateTime As DataColumn
        Dim nStatus As DataColumn
        Dim nCommunicationType As DataColumn
        Dim bisUnscheduledCare As DataColumn
        ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
        ''Added new column to pass value for TVP_IntuitCommDetails.
        Dim nUserID As DataColumn

        Try
            'declare a column 
            sCommSubject = New DataColumn("sCommSubject")
            sCommSubject.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommSubject)

            sCommMessage = New DataColumn("sCommMessage")
            sCommMessage.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommMessage)

            bCommMemberReply = New DataColumn("bCommMemberCannotReply")
            bCommMemberReply.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bCommMemberReply)

            nPatientID = New DataColumn("nPatientID")
            nPatientID.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(nPatientID)

            nCommStaffID = New DataColumn("nCommStaffID")
            nCommStaffID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommStaffID)

            sCommServiceType = New DataColumn("sCommServiceType")
            sCommServiceType.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommServiceType)

            nCommContainerID = New DataColumn("nCommContainerID")
            nCommContainerID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommContainerID)

            dAppDateTime = New DataColumn("dAppDateTime")
            dAppDateTime.DataType = System.Type.GetType("System.DateTime")
            dt.Columns.Add(dAppDateTime)

            nStatus = New DataColumn("nStatus")
            nStatus.DataType = System.Type.GetType("System.Int16")
            dt.Columns.Add(nStatus)

            bisUnscheduledCare = New DataColumn("bisUnscheduledCare")
            bisUnscheduledCare.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bisUnscheduledCare)

            nCommunicationType = New DataColumn("nCommunicationType")
            nCommunicationType.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommunicationType)

            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            nUserID = New DataColumn("nUserID")
            nCommStaffID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nUserID)



            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sCommSubject") = txtSubject.Text
            Row1.Item("sCommMessage") = txtMessage.Text
            Row1.Item("bCommMemberCannotReply") = chkAllowtoReply.IsChecked
            Row1.Item("nPatientID") = PatientID
            Row1.Item("nCommStaffID") = cmbStaff.SelectedValue
            Row1.Item("bisUnscheduledCare") = chkCommPrefernce.IsChecked

            If chkCommPrefernce.IsChecked = True Then
                Row1.Item("nCommunicationType") = cmbCommPrefrence.SelectedValue
            Else
                Row1.Item("nCommunicationType") = 0
            End If

            If IsReplyMessage = False Then
                Row1.Item("sCommServiceType") = "Patient Messaging"
                Row1.Item("nCommContainerID") = 0
            Else
                Row1.Item("sCommServiceType") = _strserviceType
                Row1.Item("nCommContainerID") = _lngContainerID
            End If
            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Pass value as 1 as nUserID is used for portal only.
            Row1.Item("nUserID") = 1

            'Appointment Request
            If _strserviceType.ToUpper() = "APPOINTMENT REQUEST" Then
                If (rbApprove.IsChecked = True) Then
                    Dim sDates As String = dtAppointmentDate.SelectedDate & " " & cbHH.Text.ToString() & ":" & cbMM.Text.ToString() & " " & cbMeridan.Text.ToString()
                    _AppnDate = DateTime.Parse(sDates)
                    sDates = String.Empty
                    Row1.Item("dAppDateTime") = _AppnDate
                    Dim sAttach As String = String.Empty
                    sAttach = txtMessage.Text
                    If (_AppnDate.ToString() <> "") Then

                        sAttach = sAttach.Replace("[DAY]", Format(dtAppointmentDate.SelectedDate, "MM/dd/yyyy"))
                        sAttach = sAttach.Replace("[TIME]", cbHH.Text.ToString() & ":" & cbMM.Text.ToString() & " " & cbMeridan.Text.ToString())

                        If _PortalType = "gloPatientPortal" Then
                            If (sAttach.Length > 8000) Then
                                sAttach = sAttach.Substring(0, 8000)
                            End If
                        Else

                            If (sAttach.Length > 4000) Then
                                sAttach = sAttach.Substring(0, 4000)
                            End If

                        End If



                        Row1.Item("sCommMessage") = sAttach.Trim()
                    End If
                Else
                    Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
                End If
                Row1.Item("nStatus") = _Status
            End If

            If _strserviceType.ToUpper() = "PRESCRIPTION RENEWAL" Then
                Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
                Row1.Item("nStatus") = _Status

            End If

            If _strserviceType.ToUpper() = "ONLINE BILL PAY" Then
                Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
                Row1.Item("nStatus") = 0
                Row1.Item("sCommServiceType") = "Patient Messaging"
                Row1.Item("nCommContainerID") = 0

            End If

            '15-Apr-13 Aniket: Showing 'Ask A Doctor' subtype
            If (_strserviceType.ToUpper() = "PATIENT MESSAGING" Or _strserviceType.ToUpper() = "ASK A DOCTOR" Or _strserviceType.ToUpper() = "ASK A STAFF" Or _strserviceType.ToUpper() = "ASK A QUESTION" Or _strserviceType.ToUpper() = "ASK A BILLER" Or _strserviceType.ToUpper() = "ASK A NURSE") Then
                Row1.Item("dAppDateTime") = Convert.ToDateTime("1/1/2001")
                Row1.Item("nStatus") = 0
            End If

            'adding the completed row to the table
            dt.Rows.Add(Row1)

            If Not IsNothing(sCommSubject) Then
                sCommSubject.Dispose() : sCommSubject = Nothing
            End If

            If Not IsNothing(sCommMessage) Then
                sCommMessage.Dispose() : sCommMessage = Nothing
            End If

            If Not IsNothing(bCommMemberReply) Then
                bCommMemberReply.Dispose() : bCommMemberReply = Nothing
            End If

            If Not IsNothing(nPatientID) Then
                nPatientID.Dispose() : nPatientID = Nothing
            End If

            If Not IsNothing(nCommStaffID) Then
                nCommStaffID.Dispose() : nCommStaffID = Nothing
            End If

            If Not IsNothing(sCommServiceType) Then
                sCommServiceType.Dispose() : sCommServiceType = Nothing
            End If

            If Not IsNothing(nCommContainerID) Then
                nCommContainerID.Dispose() : nCommContainerID = Nothing
            End If

            If Not IsNothing(dAppDateTime) Then
                dAppDateTime.Dispose() : dAppDateTime = Nothing
            End If

            If Not IsNothing(nStatus) Then
                nStatus.Dispose() : nStatus = Nothing
            End If


            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            If Not IsNothing(nUserID) Then
                nUserID.Dispose() : nUserID = Nothing
            End If
            Return dt


        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Save Message record
    ''' </summary>
    ''' <param name="Gl_Messagequeue"></param>
    ''' <param name="IntuitCommDetails"></param>
    ''' <param name="IntuitCommAttachments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveMessage(ByVal Gl_Messagequeue As DataTable, ByVal IntuitCommDetails As DataTable, ByVal IntuitCommAttachments As DataTable, Optional ByVal nPRID As Long = 0, Optional ByVal nSendMessageTo As Int16 = 0) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim MessageID As Int64 = 0
        Try
            oDB.Connect(False)

            oDBParameters.Add("@TVP_Gl_Messagequeue", Gl_Messagequeue, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_IntuitCommDetails", IntuitCommDetails, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_int_IntuitCommAttachments", IntuitCommAttachments, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@nPRID", nPRID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nSendMessageTo", nSendMessageTo, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@nMessageID", 0, ParameterDirection.Output, SqlDbType.Decimal)
            oDB.Execute("Intuit_SaveMessage", oDBParameters, MessageID)
            oDB.Disconnect()

            Return MessageID

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            MessageID = Nothing
        End Try

    End Function
    Dim MessageID As Int64 = 0
    Dim MessageID_PR As Int64 = 0
    Private Function Portal_SaveMessage(ByVal Gl_Messagequeue As DataTable, ByVal IntuitCommDetails As DataTable, ByVal IntuitCommAttachments As DataTable, Optional ByVal nPRID As Long = 0, Optional ByVal nSendMessageTo As Int16 = 0) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@TVP_Gl_Messagequeue", Gl_Messagequeue, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_IntuitCommDetails", IntuitCommDetails, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_int_IntuitCommAttachments", IntuitCommAttachments, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@nPRID", nPRID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nSendMessageTo", nSendMessageTo, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@nMessageID", 0, ParameterDirection.Output, SqlDbType.Decimal)
            oDBParameters.Add("@nMessageID_PR", 0, ParameterDirection.Output, SqlDbType.Decimal)
            oDB.Execute("Portal_SaveMessage", oDBParameters, MessageID, MessageID_PR)
            oDB.Disconnect()

            Return MessageID

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            MessageID = Nothing
        End Try

    End Function
    ''' <summary>
    ''' Retriving which record show for visit CCD file.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckVisitCCDSection()
        Dim oSettings As New gloSettings.GeneralSettings(ConnectionString)
        Dim DefaultFullCCD() As String
        Dim i As Int16
        Dim ccdStr As String = ""
        Dim dt As New DataTable
        Try
            dt = oSettings.GetSetting("VISITCCDDEFAULTSECTIONS")
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("sSettingsValue") <> "" Then
                        DefaultFullCCD = Convert.ToString(dt.Rows(0)("sSettingsValue")).Trim.Split(",")
                        For i = 0 To DefaultFullCCD.Length - 1
                            If DefaultFullCCD.Length > 0 Then
                                Select Case DefaultFullCCD(i)
                                    Case "VisitVitals"
                                        ccdStr = String.Concat(ccdStr, "Vitals")
                                    Case "VisitFamHis"
                                        ccdStr = String.Concat(ccdStr, "FamilyHistory")
                                    Case "VisitAdDir"
                                        ccdStr = String.Concat(ccdStr, "AdvanceDirectives")
                                    Case "VisitLabs"
                                        ccdStr = String.Concat(ccdStr, "Results")
                                    Case "visitImmu"
                                        ccdStr = String.Concat(ccdStr, "Immunization")
                                    Case "VisitProc"
                                        ccdStr = String.Concat(ccdStr, "Procedures")
                                    Case "VisitMed"
                                        ccdStr = String.Concat(ccdStr, "Medications")
                                    Case "VisitEncounter"
                                        ccdStr = String.Concat(ccdStr, "Encounter")
                                    Case "VisitSocHis"
                                        ccdStr = String.Concat(ccdStr, "SocialHistory")
                                    Case "VisitAllegy"
                                        ccdStr = String.Concat(ccdStr, "Allergy")
                                    Case "VisitProb"
                                        ccdStr = String.Concat(ccdStr, "Problems")
                                End Select
                            End If
                        Next
                    End If
                End If
            End If
            Return ccdStr
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            DefaultFullCCD = Nothing
            ccdStr = Nothing
            i = Nothing

            If IsNothing(oSettings) = False Then
                oSettings.Dispose()
                oSettings = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Function

    ''' <summary>
    ''' Retriving which record show for Patient CCD file.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckPatientCCDSections() As String
        Dim oSettings As New gloSettings.GeneralSettings(ConnectionString)
        Dim DefaultFullCCD() As String
        Dim i As Int16
        Dim ccdStr As String = ""
        Dim dt As New DataTable
        Try
            dt = oSettings.GetSetting("FULLCCDDEFAULTSECTIONS")
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("sSettingsValue") <> "" Then
                        DefaultFullCCD = Convert.ToString(dt.Rows(0)("sSettingsValue")).Trim.Split(",")
                        For i = 0 To DefaultFullCCD.Length - 1
                            If DefaultFullCCD.Length > 0 Then
                                Select Case DefaultFullCCD(i)
                                    Case "Fullvitals"
                                        ccdStr = String.Concat(ccdStr, "Vitals")
                                    Case "FullFamHis"
                                        ccdStr = String.Concat(ccdStr, "FamilyHistory")
                                    Case "FullAdDir"
                                        ccdStr = String.Concat(ccdStr, "AdvanceDirectives")
                                    Case "FullLabs"
                                        ccdStr = String.Concat(ccdStr, "Results")
                                    Case "FullImmu"
                                        ccdStr = String.Concat(ccdStr, "Immunization")
                                    Case "FullProc"
                                        ccdStr = String.Concat(ccdStr, "Procedures")
                                    Case "FullMed"
                                        ccdStr = String.Concat(ccdStr, "Medications")
                                    Case "Fullencounter"
                                        ccdStr = String.Concat(ccdStr, "Encounter")
                                    Case "FullSocHis"
                                        ccdStr = String.Concat(ccdStr, "SocialHistory")
                                    Case "FullAllergy"
                                        ccdStr = String.Concat(ccdStr, "Allergy")
                                    Case "FullProb"
                                        ccdStr = String.Concat(ccdStr, "Problems")
                                End Select
                            End If
                        Next
                    End If
                End If
            End If
            Return ccdStr
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally
            ccdStr = Nothing
            DefaultFullCCD = Nothing
            If IsNothing(oSettings) = False Then
                oSettings.Dispose()
                oSettings = Nothing
            End If
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Generate file application folder.
    ''' </summary>
    ''' <param name="oResult"></param>
    ''' <param name="strFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateFile(ByVal oResult() As Byte, ByVal strFileName As String) As String
        Try
            If IsNothing(oResult) = False Then
                Dim content As Byte() = CType(oResult, Byte())
                '  Dim stream As MemoryStream = New MemoryStream(content)
                'Bug #42390 : Temp - Intuit - Application is saving file in gloEMR folder instead of Temp folder.
                strFileName = gloSettings.FolderSettings.AppTempFolderPath + strFileName
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)
                ' stream.WriteTo(oFile)
                oFile.Close()

                oFile.Dispose()
                oFile = Nothing

                'stream.Dispose()
                'stream = Nothing

                content = Nothing

                Return strFileName
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Convert bytes to MB or GB
    ''' </summary>
    ''' <param name="Bytes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            Return Nothing
        Finally
            dblValue = Nothing
            strarray = Nothing
            strreturn = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Convert bytes to MB or GB
    ''' </summary>
    ''' <param name="Bytes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            Return Nothing
        Finally
            dblValue = Nothing
            strarray = Nothing
            strreturn = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Retriving for validation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPracticeID() As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim PracticeID As Int64
        Try
            oDB.Connect(False)

            oDBParameters.Add("@PracticeID", 0, ParameterDirection.Output, SqlDbType.Decimal)

            oDB.Execute("Intuit_GetPracticeID", oDBParameters, PracticeID)
            oDB.Disconnect()

            Return PracticeID

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            PracticeID = Nothing
        End Try

    End Function


#End Region

#Region "SubProcedure"

    Private Sub AddPatientStrip()
        TryCast(Wfh.Child, gloUC_PatientStrip).ShowDetail(PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.Intuit)
        ' Wfh.Child.BringToFront()
        TryCast(Wfh.Child, gloUC_PatientStrip).DTPValue = Format(Date.Now, "MM/dd/yyyy")

        TryCast(Wfh.Child, gloUC_PatientStrip).DTPFormat = DateTimePickerFormat.Short
        TryCast(Wfh.Child, gloUC_PatientStrip).DTPEnabled = False
        Try
            RemoveHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
        Catch ex As Exception

        End Try

        AddHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
        PatientStrip.Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height

    End Sub

    Private Sub SetContolEnableDisable()
        dtExamCCD.IsEnabled = False
        dtFromPtCCD.IsEnabled = False
        dtToPtCCD.IsEnabled = False
        chkPerPtRequest.IsChecked = True
        chkPerPtRequest.IsEnabled = False
        btnExamCCD.IsEnabled = False
        btnPtCCD.IsEnabled = False
    End Sub

    'Private Sub SetFormHeight()

    '    If (_strserviceType.ToUpper <> "") Then


    '        Select Case _strserviceType.ToUpper

    '            Case "APPOINTMENT REQUEST"
    '                If (_flag = 1) Then
    '                    MainPanel.Height = 860
    '                Else
    '                    MainPanel.Height = 801
    '                End If
    '                Me.Height = MainPanel.Height
    '                If (PatientStrip.Height = 217) Then
    '                    txtMessage.Height = 220
    '                Else
    '                    Dim _height As Int16 = 217 - PatientStrip.Height
    '                    txtMessage.Height = 220 + _height
    '                End If

    '            Case "PRESCRIPTION RENEWAL"
    '                If (_flag = 1) Then
    '                    MainPanel.Height = 830
    '                Else
    '                    MainPanel.Height = 771
    '                End If
    '                Me.Height = MainPanel.Height
    '                If (PatientStrip.Height = 217) Then
    '                    txtMessage.Height = 250
    '                Else
    '                    Dim _height As Int16 = 217 - PatientStrip.Height
    '                    txtMessage.Height = 250 + _height
    '                End If

    '            Case "ONLINE BILL PAY"
    '                'If (_flag = 1) Then
    '                '    MainPanel.Height = 1019
    '                'Else
    '                '    MainPanel.Height = 965
    '                'End If


    '                'MainPanel.Height = 650
    '                'Me.Height = MainPanel.Height + 20
    '                If _PatientStatus = 1 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height - 50

    '                Else
    '                    ' If PatientStrip.Height = 31 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height
    '                End If

    '                '15-Apr-13 Aniket: Showing 'Ask A Doctor' subtype
    '            Case "ASK A DOCTOR", "ASK A STAFF", "ASK A QUESTION", "ASK A BILLER", "ASK A NURSE"
    '                ControlToHiddenForPatientMessaging()
    '                If _PatientStatus = 1 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height - 50

    '                Else
    '                    ' If PatientStrip.Height = 31 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height
    '                End If
    '            Case "PATIENT MESSAGING"
    '                ControlToHiddenForPatientMessaging()
    '                If _PatientStatus = 1 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height - 50

    '                Else
    '                    ' If PatientStrip.Height = 31 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height
    '                End If

    '            Case Else
    '                ControlToHiddenForPatientMessaging()
    '                If _PatientStatus = 1 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height - 50

    '                Else
    '                    ' If PatientStrip.Height = 31 Then
    '                    Me.Height = 653 + 60 + PatientStrip.Height
    '                End If


    '        End Select

    '    Else

    '        If _PatientStatus = 1 Then

    '            Me.Height = 653 + 60 + PatientStrip.Height - 80

    '        Else
    '            ' If PatientStrip.Height = 31 Then
    '            Me.Height = 653 + 60 + PatientStrip.Height

    '        End If
    '    End If
    'End Sub

    'Private Sub GetFormHeight()
    '    Me.Height = 653 + 60 + PatientStrip.Height
    'End Sub

    Private Sub ControlSizeChanged()
        Try
            TryCast(Wfh.Child, gloUC_PatientStrip).Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
            PatientStrip.Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
            ht = PatientStrip.Height
            '' SetFormHeight()
        Catch ex As Exception
        End Try
    End Sub
    Private Shared CallibiriFontFamily As FontFamily = New FontFamily("Callibri")
    Private Shared TahomaFontFamily As FontFamily = New FontFamily("Tahoma")
    ''' <summary>
    ''' Add items in the list box using CCD and Attchment button.
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="FilePath"></param>
    ''' <remarks></remarks>
    Private Sub AddListBoxItem(ByVal FileName As String, ByVal FilePath As String)

        If lstAttachment.Items.Count = 0 Or lstAttachment.Items.Count < 3 Then

            Dim ostackMain As StackPanel, ostackInner As StackPanel
            Dim olbl As TextBlock
            Dim oimgAckw As Image
            Dim oimgDelete As Image

            Dim obrd As Border
            Dim selectedIndex As Int16 = 0

            obrd = New Border()

            Try
                RemoveHandler obrd.MouseLeftButtonDown, AddressOf Border_MouseClick
            Catch ex As Exception

            End Try

            AddHandler obrd.MouseLeftButtonDown, AddressOf Border_MouseClick
            '  obrd.BorderThickness = New Thickness(1)
            ''  obrd.CornerRadius = New CornerRadius(6)
            ''  obrd.BorderBrush = Brushes.Black
            '  obrd.Width = 350
            ''  obrd.BorderThickness = New Thickness(1)
            'set filepath
            obrd.Tag = FilePath


            Try
                RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
            Catch ex As Exception

            End Try

            AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged

            ostackMain = New StackPanel()
            ostackMain.Orientation = System.Windows.Controls.Orientation.Vertical
            ostackMain.Margin = New Thickness(0)
            ostackMain.CanVerticallyScroll = True
            ostackInner = New StackPanel()
            ostackInner.Orientation = System.Windows.Controls.Orientation.Horizontal
            '' ostackInner.Margin = New Thickness(0)
            ostackInner.CanVerticallyScroll = True


            oimgAckw = New Image()
            oimgAckw.Margin = New Thickness(5, 0, 5, 0)
            oimgAckw.Source = gloUserControlLibrary.gloBitmapResources.UserControlForwardBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Forward Email.png", UriKind.Relative))
            oimgAckw.Height = 20
            oimgAckw.Width = 20
            oimgAckw.Stretch = Stretch.Fill
            oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
            oimgAckw.VerticalAlignment = Windows.VerticalAlignment.Top
            ostackInner.Children.Add(oimgAckw)

            olbl = New TextBlock()
            olbl.Margin = New Thickness(2, 4, 0, 0)
            olbl.TextWrapping = TextWrapping.Wrap
            olbl.Width = 410
            olbl.Foreground = Brushes.Black
            olbl.FontFamily = CallibiriFontFamily 'New FontFamily("Callibri")
            olbl.FontSize = 12
            olbl.Text = FileName

            olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
            olbl.VerticalAlignment = Windows.VerticalAlignment.Top
            oimgDelete = New Image()
            oimgDelete.Height = 20
            oimgDelete.Width = 20
            oimgDelete.Source = gloUserControlLibrary.gloBitmapResources.IntuitCloseBlackBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloIntuitSecureMessage;component/Images/close-black.ico", UriKind.Relative))
            oimgDelete.Margin = New Thickness(0, 0, -20, -20)
            Dim tt As New System.Windows.Controls.ToolTip()
            tt.Content = "Remove Attachment"
            oimgDelete.ToolTip = tt
            oimgDelete.VerticalAlignment = Windows.VerticalAlignment.Top
            oimgDelete.HorizontalAlignment = Windows.HorizontalAlignment.Right
            'AddHandler oimgDelete.MouseEnter, AddressOf image_Mouseenter
            'AddHandler oimgDelete.MouseDown, AddressOf obtn_click


            'AddHandler oimgDelete.MouseDown, AddressOf lstAttachment_MouseDown
            'AddHandler oimgDelete.MouseUp, AddressOf lstAttachment_MouseLeftButtonUp

            AddHandler oimgDelete.MouseDown, AddressOf ImgDelete_MouseDown
            AddHandler oimgDelete.MouseLeftButtonUp, AddressOf ImgDelete_MouseUp

            ostackInner.Children.Add(olbl)
            ostackInner.Children.Add(oimgDelete)
            ostackMain.Children.Add(ostackInner)

            obrd.Child = ostackMain

            lstAttachment.ScrollIntoView(lstAttachment)
            lstAttachment.Items.Add(obrd)

            lstAttachment.SelectedIndex = 0
        Else
            System.Windows.MessageBox.Show("You can attach only three items.", gstrMessageBoxCaption, Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information)
        End If

    End Sub
    Private Sub Border_MouseClick(ByVal sender As System.Object, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then

            Dim AttachmentData As Byte()
            Dim strFileName As String = ""
            Try
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If IsNothing(lstAttachment.SelectedItem) = False Then

                    Dim obd As Border = DirectCast(lstAttachment.SelectedItem, Border)
                    Dim stac As StackPanel = obd.Child
                    Dim Innerstac As StackPanel = stac.Children(0)
                    Dim TBlock As TextBlock = Innerstac.Children(1)

                    Dim _Extension() As String
                    Dim _FileExt As String = ""

                    _Extension = TBlock.Text.Split(".")
                    If _Extension.Length > 1 Then
                        _FileExt = _Extension(_Extension.Length - 1)
                    End If

                    AttachmentData = TBlock.Tag

                    If IsDBNull(AttachmentData) = False Then
                        strFileName = GenerateFile(AttachmentData, TBlock.Text)
                        Dim startInfo As System.Diagnostics.ProcessStartInfo
                        Dim pStart As New System.Diagnostics.Process
                        startInfo = New System.Diagnostics.ProcessStartInfo(strFileName)
                        startInfo.UseShellExecute = True
                        startInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
                        startInfo.CreateNoWindow = False
                        Process.Start(startInfo)
                    End If

                    _Extension = Nothing
                    _FileExt = Nothing

                End If
            Catch ex As Exception
                System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Finally
                strFileName = Nothing
                AttachmentData = Nothing
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            End Try
        End If
    End Sub
    Private Sub ImgDelete_MouseDown()

        _isMouseDown = True
    End Sub
    Private Sub ImgDelete_MouseUp()

        If _isMouseDown = True Then

            _isMouseDown = False
            If lstAttachment.SelectedIndex >= 0 Then
                If lstAttachment.SelectedIndex >= 0 Then
                    If (System.Windows.MessageBox.Show("Are you sure you want to remove the '" & GetAttachmentFileName(lstAttachment.SelectedIndex) & "' file?", gstrMessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) = MessageBoxResult.Yes Then

                        RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
                        lstAttachment.Items.Remove(lstAttachment.SelectedItem)
                        AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged

                        If lstAttachment.Items.Count >= 0 Then
                            lstAttachment.SelectedIndex = 0
                        End If
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub obtn_click()
        If lstAttachment.SelectedIndex >= 0 Then
            If (System.Windows.MessageBox.Show("Are you sure you want to remove the '" & GetAttachmentFileName(lstAttachment.SelectedIndex) & "' file?", gstrMessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) = MessageBoxResult.Yes Then
                lstAttachment.Items.Remove(lstAttachment.SelectedItem)
                If lstAttachment.Items.Count >= 0 Then
                    lstAttachment.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub image_Mouseenter()

    End Sub

    Private Sub SetDataFromReadMessage()
        If Len(LTrim(_SubjectFromReadMessage)) > 4 Then
            If LCase(LTrim(_SubjectFromReadMessage).ToString.Substring(0, 3)) <> "re:" Then
                txtSubject.Text = "Re: " & _SubjectFromReadMessage
            Else
                txtSubject.Text = _SubjectFromReadMessage
            End If
        Else
            txtSubject.Text = "Re: " & _SubjectFromReadMessage
        End If


        If _IncludeMessageInReply = True Then
            txtMessage.Text = _MessageFromReadMessage

        End If

        cmbStaff.Text = _StaffDescriptionFromReadMessage
        cmbStaff.IsEnabled = False
    End Sub


    ''' <summary>
    ''' Retrives the message details.
    ''' </summary>
    Private Sub RetriveMessagedetails()
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dsIM As New DataSet

        Try

            oDB.Connect(False)
            oDBParameters.Add("@nCommDetailID", CommDetailID, ParameterDirection.Input, SqlDbType.Decimal)

            If _PortalType = "gloPatientPortal" Then
                oDB.Retrive("gsp_PortalRetriveMessageDetails", oDBParameters, dsIM)
            Else
                oDB.Retrive("IntuitRetriveMessageDetails", oDBParameters, dsIM)
            End If

            oDB.Disconnect()

            ControlToVisible()
            dtAppointmentDate.DisplayDateStart = System.DateTime.Now

            If dsIM.Tables(0).Rows.Count > 0 Then
                If IsReplyMessage = True Then
                    If Len(LTrim(dsIM.Tables(0).Rows.Item(0)("sCommSubject"))) > 4 Then
                        If LCase(LTrim(dsIM.Tables(0).Rows.Item(0)("sCommSubject")).ToString.Substring(0, 3)) <> "re:" Then
                            txtSubject.Text = "Re: " & dsIM.Tables(0).Rows.Item(0)("sCommSubject")
                        Else
                            txtSubject.Text = dsIM.Tables(0).Rows.Item(0)("sCommSubject")
                        End If
                    Else
                        txtSubject.Text = "Re: " & dsIM.Tables(0).Rows.Item(0)("sCommSubject")
                    End If
                    ''Added for MU2 Patient Portal on 20130828
                    If _PortalType = "gloPatientPortal" Then
                        _lngContainerID = dsIM.Tables(0).Rows.Item(0)("nCommDetailID")
                    Else
                        _lngContainerID = dsIM.Tables(0).Rows.Item(0)("nCommContainerID")
                    End If
                    ''End
                    _strserviceType = dsIM.Tables(0).Rows.Item(0)("sCommServiceType")

                Else
                    _strserviceType = dsIM.Tables(0).Rows.Item(0)("sCommServiceType")
                    txtSubject.Text = dsIM.Tables(0).Rows.Item(0)("sCommSubject")
                End If

                chkCommPrefernce.IsChecked = dsIM.Tables(0).Rows.Item(0)("bisUnscheduledCare")
                ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
                ''Added to show label if check box is check.
                If chkCommPrefernce.IsChecked Then
                    lblPatientPrefers.Visibility = Windows.Visibility.Visible
                End If

                If dsIM.Tables(0).Rows.Item(0)("nCommunicationType") > 0 Then
                    cmbCommPrefrence.SelectedValue = dsIM.Tables(0).Rows.Item(0)("nCommunicationType")
                End If
                chkAllowtoReply.IsChecked = dsIM.Tables(0).Rows.Item(0)("bCommMemberCannotReply")

                ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                ''Added code to show from name and portal display name for selected message.
                If _PortalType = "gloPatientPortal" Then
                    If IsReplyMessage = False Then
                        lblFrom.Content = dsIM.Tables(0).Rows.Item(0)("UserName")
                        ''Bug #68439: Portal Display Name not reflecting properly in EMR 
                        ''Replace label to textblock so change content to text.
                        lblPortalDisplayNameValue.Text = dsIM.Tables(0).Rows.Item(0)("PortalDisplayName")
                    End If
                Else
                    cmbStaff.SelectedValue = dsIM.Tables(0).Rows.Item(0)("nCommStaffID")
                End If
                _Status = dsIM.Tables(0).Rows.Item(0)("FlagStatus")
                If (_Status > 0) Then
                    If (_Status = 1) Then
                        rbApprove.IsChecked = True
                        rbCancel.IsChecked = False

                    End If
                    If (_Status = 2) Then
                        rbCancel.IsChecked = True
                        rbApprove.IsChecked = False
                    End If
                End If
                If Not IsNothing(dsIM.Tables(0).Rows.Item(0)("dAppointmentDate")) Then
                    If dsIM.Tables(0).Rows.Item(0)("dAppointmentDate").ToString() <> "" Then
                        _AppointmentDate = Convert.ToDateTime(dsIM.Tables(0).Rows.Item(0)("dAppointmentDate"))
                        If (_Status = 1) Then
                            Dim sDate() As String
                            sDate = _AppointmentDate.ToString().Split(" ")
                            If (sDate.Length >= 3) Then
                                dtAppointmentDate.Text = sDate(0).ToString()
                                Dim sTime() As String
                                sTime = sDate(1).ToString().Split(":")
                                If (sTime.Length >= 3) Then
                                    cbHH.Text = sTime(0).ToString()
                                    cbMM.Text = sTime(1).ToString()
                                    cbMeridan.Text = sDate(2).ToString()
                                End If
                            End If
                        End If
                    End If
                End If



                If IsReplyMessage = True Then
                    Dim sDefaultMessage As String = String.Empty
                    If _PortalType = "gloPatientPortal" Then
                        If dsIM.Tables.Count > 2 Then
                            If dsIM.Tables(2).Rows.Count > 0 Then
                                Dim dr() As DataRow = Nothing

                                If (_strserviceType.ToLower = "appointmentrequest") Then
                                    dr = dsIM.Tables(2).Select("sMessageCode='APPREPLYSM'")
                                ElseIf (_strserviceType.ToLower = "refillrequest") Then
                                    dr = dsIM.Tables(2).Select("sMessageCode='RXREPLYSM'")
                                End If

                                If Not IsNothing(dr) Then
                                    sDefaultMessage = dr(0)("sMessageBody")
                                End If
                                dr = Nothing
                            End If
                        End If
                    End If
                    If IncludeMessageInReply = True Then
                        txtMessage.Focus()
                        ''Added for MU2 Patient Portal on 20130827
                        If _PortalType = "gloPatientPortal" Then
                            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                            ''remove "dsIM.Tables(0).Rows(0)("nCommStaffID") & " - " &" from to as we remove it from textbox also.
                            ''_OriginalMessage = "" & Environment.NewLine & Environment.NewLine & " Thanks" & Environment.NewLine & Environment.NewLine & "---------------------------Original Message-----------------------------" & Environment.NewLine & "From: " & dsIM.Tables(0).Rows(0)("PatientName") & System.Environment.NewLine & "Sent: " & System.TimeZoneInfo.ConvertTime(dsIM.Tables(0).Rows(0)("dTransactionDate"), TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")) & Environment.NewLine & "To: " & dsIM.Tables(0).Rows(0)("nCommStaffID") & " - " & dsIM.Tables(0).Rows(0)("sDescription") & System.Environment.NewLine & "Subject: " & txtSubject.Text & Environment.NewLine & dsIM.Tables(0).Rows.Item(0)("sCommMessage")

                            ''Bug #89449: glo EMR > Time mismatch when user reply message from EMR
                            ''commented old code and remove time conversion from new line.
                            ''_OriginalMessage = "" & Environment.NewLine & Environment.NewLine & " Thanks" & Environment.NewLine & Environment.NewLine & "---------------------------Original Message-----------------------------" & Environment.NewLine & "From: " & dsIM.Tables(0).Rows(0)("PatientName") & System.Environment.NewLine & "Sent: " & System.TimeZoneInfo.ConvertTime(dsIM.Tables(0).Rows(0)("dTransactionDate"), TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")) & Environment.NewLine & "To: " & dsIM.Tables(0).Rows(0)("sDescription") & System.Environment.NewLine & "Subject: " & txtSubject.Text & Environment.NewLine & dsIM.Tables(0).Rows.Item(0)("sCommMessage")
                            _OriginalMessage = "" & Environment.NewLine & Environment.NewLine & " Thanks" & Environment.NewLine & Environment.NewLine & "---------------------------Original Message-----------------------------" & Environment.NewLine & "From: " & dsIM.Tables(0).Rows(0)("PatientName") & System.Environment.NewLine & "Sent: " & Convert.ToString(dsIM.Tables(0).Rows(0)("dTransactionDate")) & Environment.NewLine & "To: " & dsIM.Tables(0).Rows(0)("sDescription") & System.Environment.NewLine & "Subject: " & txtSubject.Text & Environment.NewLine & dsIM.Tables(0).Rows.Item(0)("sCommMessage")

                            If (_strserviceType.ToLower = "appointmentrequest") Then
                                If _Status = 0 Then
                                    ''txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]." & _OriginalMessage
                                    txtMessage.Text = sDefaultMessage & _OriginalMessage
                                End If
                            End If
                            If (_strserviceType.ToLower = "refillrequest") Then
                                If _Status = 0 Then
                                    ''txtMessage.Text = "Your prescription(s) has been renewed." & _OriginalMessage
                                    txtMessage.Text = sDefaultMessage & _OriginalMessage
                                End If
                            End If
                            If (_strserviceType.ToLower = "askaprovider" Or _strserviceType.ToLower = "askastaff" Or _strserviceType.ToLower = "patient messaging") Then
                                txtMessage.Text = _OriginalMessage
                            End If
                            ''End
                        Else
                            '15-Apr-13 Aniket: Display EST Time in reply message text
                            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                            ''remove "dsIM.Tables(0).Rows(0)("nCommStaffID") & " - " &" from to as we remove it from textbox also.
                            ''Revert the changed done.
                            _OriginalMessage = "" & Environment.NewLine & Environment.NewLine & " Thanks" & Environment.NewLine & Environment.NewLine & "---------------------------Original Message-----------------------------" & Environment.NewLine & "From: " & dsIM.Tables(0).Rows(0)("PatientName") & System.Environment.NewLine & "Sent: " & System.TimeZoneInfo.ConvertTime(dsIM.Tables(0).Rows(0)("dTransactionDate"), TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")) & Environment.NewLine & "To: " & dsIM.Tables(0).Rows(0)("nCommStaffID") & " - " & dsIM.Tables(0).Rows(0)("sDescription") & System.Environment.NewLine & "Subject: " & txtSubject.Text & Environment.NewLine & dsIM.Tables(0).Rows.Item(0)("sCommMessage")


                            If (_strserviceType.ToUpper = "APPOINTMENT REQUEST") Then
                                If _Status = 0 Then
                                    txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]." & _OriginalMessage
                                End If
                            End If
                            If (_strserviceType.ToUpper = "PRESCRIPTION RENEWAL") Then
                                If _Status = 0 Then
                                    txtMessage.Text = "Your prescription(s) has been renewed." & _OriginalMessage
                                End If
                            End If

                            '15-Apr-13 Aniket: Showing 'Ask A Doctor' subtype
                            If (_strserviceType.ToUpper = "ONLINE BILL PAY" Or _strserviceType.ToUpper = "ASK A DOCTOR" Or _strserviceType.ToUpper = "ASK A STAFF" Or _strserviceType.ToUpper = "ASK A QUESTION" Or _strserviceType.ToUpper = "ASK A BILLER" Or _strserviceType.ToUpper = "ASK A NURSE" Or _strserviceType.ToUpper = "PATIENT MESSAGING") Then
                                txtMessage.Text = _OriginalMessage
                            End If
                        End If

                    Else

                        txtMessage.Focus()

                        If _PortalType = "gloPatientPortal" Then

                            If (_strserviceType.ToUpper = "appointmentrequest") Then
                                If _Status = 0 Then
                                    ''txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]."
                                    txtMessage.Text = sDefaultMessage
                                End If
                            End If
                            If (_strserviceType.ToUpper = "refillrequest") Then
                                If _Status = 0 Then
                                    ''txtMessage.Text = "Your prescription(s) has been renewed."
                                    txtMessage.Text = sDefaultMessage
                                End If
                            End If

                        Else

                            If (_strserviceType.ToUpper = "APPOINTMENT REQUEST") Then
                                If _Status = 0 Then
                                    txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]."
                                End If
                            End If
                            If (_strserviceType.ToUpper = "PRESCRIPTION RENEWAL") Then
                                If _Status = 0 Then
                                    txtMessage.Text = "Your prescription(s) has been renewed."
                                End If
                            End If

                        End If

                    End If
                Else
                    txtMessage.Text = dsIM.Tables(0).Rows.Item(0)("sCommMessage")
                End If

            End If
            If IsReplyMessage = False Then
                If dsIM.Tables(1).Rows.Count > 0 Then

                    If dsIM.Tables(1).Rows.Item(0)("sCommAttachOneName").ToString.Length > 0 And dsIM.Tables(1).Rows.Item(0)("ICommAttachOne").ToString.Length > 0 Then
                        AddRetrivedListBoxItem(dsIM.Tables(1).Rows.Item(0)("sCommAttachOneName"), _
                                               dsIM.Tables(1).Rows.Item(0)("ICommAttachOne"))
                    End If

                    If dsIM.Tables(1).Rows.Item(0)("sCommAttachTwoName").ToString.Length > 0 And dsIM.Tables(1).Rows.Item(0)("ICommAttachTwo").ToString.Length > 0 Then
                        AddRetrivedListBoxItem(dsIM.Tables(1).Rows.Item(0)("sCommAttachTwoName"), _
                           dsIM.Tables(1).Rows.Item(0)("ICommAttachTwo"))
                    End If

                    If dsIM.Tables(1).Rows.Item(0)("sCommAttachThreeName").ToString.Length > 0 And dsIM.Tables(1).Rows.Item(0)("ICommAttachThree").ToString.Length > 0 Then
                        AddRetrivedListBoxItem(dsIM.Tables(1).Rows.Item(0)("sCommAttachThreeName"), _
                               dsIM.Tables(1).Rows.Item(0)("ICommAttachThree"))
                    End If

                End If

            End If


            'If IsReplyMessage = True Then

            ''Added for MU2 Patient Portal on 20130827
            If _PortalType = "gloPatientPortal" Then
                ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                ''Added if condition for Exception "There is no row at 0 position".
                If dsIM.Tables(0).Rows.Count > 0 Then
                    If dsIM.Tables(0).Rows.Item(0)("PRID") > 0 Then
                        cmbMessageSendTo.SelectedItem = DirectCast(cbiPatientRepresentative, Object)

                        cmbPRList.Visibility = Windows.Visibility.Visible
                        '' If cmbPRList.Items.Contains(dsIM.Tables(0).Rows.Item(0)("PRID").ToString()) Then
                        cmbPRList.SelectedValue = dsIM.Tables(0).Rows.Item(0)("PRID")
                        ''End If
                    Else
                        cmbMessageSendTo.SelectedItem = DirectCast(cbiPatient, Object)
                        cmbPRList.Visibility = Windows.Visibility.Collapsed
                    End If
                End If


                Select Case _strserviceType.ToLower

                    Case "appointmentRequest"
                        lblType.Content = "Appointment Request"
                        ControlToHiddenForAppointment()
                        'If (_flag = 1) Then
                        '    'added 50 for text box message
                        '    MainPanel.Height = 830 - 200
                        'Else
                        '    MainPanel.Height = 830 - 200
                        'End If
                        'Me.Height = MainPanel.Height
                        'If (PatientStrip.Height = 217) Then
                        '    txtMessage.Height = 220
                        'Else
                        '    Dim _height As Int16 = 217 - PatientStrip.Height
                        '    txtMessage.Height = 220 + _height
                        'End If

                    Case "refillrequest"
                        lblType.Content = "Refill Request"
                        ControlToHiddenForRx()
                        'If (_flag = 1) Then
                        '    'added 50 for text box message
                        '    MainPanel.Height = 830 - 200
                        'Else
                        '    MainPanel.Height = 830 - 200
                        'End If
                        'Me.Height = MainPanel.Height
                        'If (PatientStrip.Height = 217) Then
                        '    txtMessage.Height = 250
                        'Else
                        '    Dim _height As Int16 = 217 - PatientStrip.Height
                        '    txtMessage.Height = 250 + _height
                        'End If

                    Case "askaprovider"
                        lblType.Content = "Ask a Provider"
                        ControlToHiddenForPatientMessaging()
                        'MainPanel.Height = 830 - 200
                    Case "askastaff"
                        lblType.Content = "Ask a Staff"
                        ControlToHiddenForPatientMessaging()
                        'MainPanel.Height = 830 - 200
                    Case Else
                        lblType.Content = "Patient Messaging"
                        ControlToHiddenForPatientMessaging()
                        'MainPanel.Height = 830 - 200
                End Select
                ''End
            Else
                Select Case _strserviceType.ToUpper

                    Case "APPOINTMENT REQUEST"
                        lblType.Content = "Appointment Request"
                        ControlToHiddenForAppointment()
                        'If (_flag = 1) Then
                        '    'added 50 for text box message
                        '    MainPanel.Height = 860
                        'Else
                        '    MainPanel.Height = 801 - 90
                        'End If
                        'Me.Height = MainPanel.Height
                        'If (PatientStrip.Height = 217) Then
                        '    txtMessage.Height = 220
                        'Else
                        '    Dim _height As Int16 = 217 - PatientStrip.Height
                        '    txtMessage.Height = 220 + _height
                        'End If

                    Case "PRESCRIPTION RENEWAL"
                        lblType.Content = "Prescription Renewal"
                        ControlToHiddenForRx()
                        'If (_flag = 1) Then
                        '    'added 50 for text box message
                        '    MainPanel.Height = 830
                        'Else
                        '    MainPanel.Height = 771 - 90
                        'End If
                        'Me.Height = MainPanel.Height
                        'If (PatientStrip.Height = 217) Then
                        '    txtMessage.Height = 250
                        'Else
                        '    Dim _height As Int16 = 217 - PatientStrip.Height
                        '    txtMessage.Height = 250 + _height
                        'End If

                    Case "ONLINE BILL PAY"
                        lblType.Content = "Patient Messaging"
                        'ControlToHiddenForBill()
                        ControlToHiddenForPatientMessaging()
                        'If (_flag = 1) Then
                        '    MainPanel.Height = 1019
                        'Else
                        '    MainPanel.Height = 965
                        'End If


                        'MainPanel.Height = 650
                        'Me.Height = MainPanel.Height + 20
                    Case "ASK A DOCTOR"
                        lblType.Content = "Ask a Doctor"
                        ControlToHiddenForPatientMessaging()

                    Case "ASK A STAFF"
                        lblType.Content = "Ask a Staff"
                        ControlToHiddenForPatientMessaging()

                    Case "ASK A QUESTION"
                        lblType.Content = "Ask a Question"
                        ControlToHiddenForPatientMessaging()

                    Case "ASK A BILLER"
                        lblType.Content = "Ask a Biller"
                        ControlToHiddenForPatientMessaging()

                    Case "ASK A NURSE"
                        lblType.Content = "Ask a Nurse"
                        ControlToHiddenForPatientMessaging()


                    Case "PATIENT MESSAGING"
                        lblType.Content = "Patient Messaging"
                        ControlToHiddenForPatientMessaging()
                    Case Else
                        lblType.Content = "Patient Messaging"
                        ControlToHiddenForPatientMessaging()

                End Select
            End If




            'End If




        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(dsIM) Then
                dsIM.Dispose() : dsIM = Nothing
            End If

        End Try
    End Sub

    ''' <summary>
    ''' Controls to visible.
    ''' </summary>
    Private Sub ControlToVisible()
        pnlAllowtoReply.Visibility = Windows.Visibility.Visible
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Visible
        End If
        pnlPatientRecord.Visibility = Windows.Visibility.Visible
        pnlPatientRequest.Visibility = Windows.Visibility.Visible
        pnlButtons.Visibility = Windows.Visibility.Visible
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Visible
        End If
        pnllstAttachment.Visibility = Windows.Visibility.Visible
        pnlAppDate.Visibility = Windows.Visibility.Visible
        pnlType.Visibility = Windows.Visibility.Visible
        '' pnlAppointment.Visibility = Windows.Visibility.Visible
        pnlRadioBtn.Visibility = Windows.Visibility.Visible
        pnlAppDate.Visibility = Windows.Visibility.Visible
        pnlAppTime.Visibility = Windows.Visibility.Visible

    End Sub


    ''' <summary>
    ''' Controls to hidden for appointment.
    ''' </summary>
    Private Sub ControlToHiddenForAppointment()
        pnlAllowtoReply.Visibility = Windows.Visibility.Collapsed
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        End If
        pnlPatientRecord.Visibility = Windows.Visibility.Collapsed
        pnlPatientRequest.Visibility = Windows.Visibility.Collapsed
        pnlButtons.Visibility = Windows.Visibility.Collapsed
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        End If
        pnllstAttachment.Visibility = Windows.Visibility.Collapsed
        lblIntuitAttachmentNote.Visibility = Windows.Visibility.Collapsed
    End Sub


    Private Sub ControlToHiddenForBill()
        pnlAppDate.Visibility = Windows.Visibility.Collapsed
        pnlAppTime.Visibility = Windows.Visibility.Collapsed
        pnlAllowtoReply.Visibility = Windows.Visibility.Collapsed
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        End If
        pnlPatientRecord.Visibility = Windows.Visibility.Collapsed
        pnlPatientRequest.Visibility = Windows.Visibility.Collapsed
        pnlButtons.Visibility = Windows.Visibility.Collapsed
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        End If
        pnllstAttachment.Visibility = Windows.Visibility.Collapsed
    End Sub



    Private Sub ControlToHiddenForRx()
        pnlAppDate.Visibility = Windows.Visibility.Collapsed
        pnlAppTime.Visibility = Windows.Visibility.Collapsed
        pnlAllowtoReply.Visibility = Windows.Visibility.Collapsed
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        End If
        pnlPatientRecord.Visibility = Windows.Visibility.Collapsed
        pnlPatientRequest.Visibility = Windows.Visibility.Collapsed
        pnlButtons.Visibility = Windows.Visibility.Collapsed
        If _PortalType = "gloPatientPortal" Then
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        Else
            pnlVisitSummary.Visibility = Windows.Visibility.Collapsed
        End If
        pnllstAttachment.Visibility = Windows.Visibility.Collapsed
        lblIntuitAttachmentNote.Visibility = Windows.Visibility.Collapsed
    End Sub



    Private Sub ControlToHiddenForPatientMessaging()
        ''  pnlAppointment.Visibility = Windows.Visibility.Collapsed
        pnlRadioBtn.Visibility = Windows.Visibility.Collapsed
        pnlAppDate.Visibility = Windows.Visibility.Collapsed
        pnlAppTime.Visibility = Windows.Visibility.Collapsed
    End Sub


    ''' <summary>
    ''' Fill retrieved attached files in Listbox
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="FileInfo"></param>
    ''' <remarks></remarks>
    Private Sub AddRetrivedListBoxItem(ByVal FileName As String, ByVal FileInfo As Byte())

        If lstAttachment.Items.Count = 0 Or lstAttachment.Items.Count < 3 Then

            Dim ostackMain As StackPanel, ostackInner As StackPanel
            Dim olbl As TextBlock
            Dim oimgAckw As Image


            Dim obrd As Border
            Dim selectedIndex As Int16 = 0

            obrd = New Border()


            'obrd.BorderThickness = New Thickness(1)
            'obrd.CornerRadius = New CornerRadius(6)
            'obrd.BorderBrush = Brushes.Black
            Try
                RemoveHandler obrd.MouseLeftButtonDown, AddressOf Border_MouseClick
            Catch ex As Exception

            End Try

            AddHandler obrd.MouseLeftButtonDown, AddressOf Border_MouseClick
            'set filepath
            'obrd.Tag = FilePath


            Try
                RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
            Catch ex As Exception

            End Try

            AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged

            ostackMain = New StackPanel()
            ostackMain.Orientation = System.Windows.Controls.Orientation.Vertical
            ostackMain.Margin = New Thickness(2)
            ostackMain.CanVerticallyScroll = True
            ostackInner = New StackPanel()
            ostackInner.Orientation = System.Windows.Controls.Orientation.Horizontal
            ostackInner.Margin = New Thickness(1)
            ostackInner.CanVerticallyScroll = True


            oimgAckw = New Image()
            oimgAckw.Margin = New Thickness(5, 0, 5, 0)
            oimgAckw.Source = gloUserControlLibrary.gloBitmapResources.UserControlForwardBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Forward Email.png", UriKind.Relative))
            oimgAckw.Height = 20
            oimgAckw.Width = 20
            oimgAckw.Stretch = Stretch.Fill
            oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
            oimgAckw.VerticalAlignment = Windows.VerticalAlignment.Top
            ostackInner.Children.Add(oimgAckw)

            olbl = New TextBlock()
            olbl.Margin = New Thickness(5, 2, 5, 0)
            olbl.TextWrapping = TextWrapping.Wrap
            olbl.Width = 400
            olbl.Foreground = Brushes.Black
            olbl.FontFamily = TahomaFontFamily 'New FontFamily("Tahoma")
            olbl.FontSize = 12
            'set filename
            olbl.Text = FileName
            olbl.Tag = FileInfo

            olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
            '  olbl.VerticalAlignment = Windows.VerticalAlignment.Top
            ostackInner.Children.Add(olbl)

            ostackMain.Children.Add(ostackInner)

            obrd.Child = ostackMain

            lstAttachment.ScrollIntoView(lstAttachment)
            lstAttachment.Items.Add(obrd)

            lstAttachment.SelectedIndex = 0
        Else
            System.Windows.MessageBox.Show("You can attach only three items.", gstrMessageBoxCaption, Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information)
        End If

    End Sub

    ''' <summary>
    ''' Retriving Patient name and email. 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetPatientNameAndemail()
        Dim dtPatient As New DataTable
        Dim clsIntuit As New ClsIntuitSecureMessage(ConnectionString, PatientID, 0)

        Try
            dtPatient = clsIntuit.ToCheckPatientRegisterOrNot(PatientID)

            lblNote.Text = ""
            lblNote.Inlines.Add(New Bold(New Run("Note : ")))
            lblNote.Inlines.Add(New Run("Patient '"))
            lblNote.Inlines.Add(New Bold(New Run(dtPatient.Rows(0).Item("LastName"))))
            lblNote.Inlines.Add(New Bold(New Run(", ")))
            lblNote.Inlines.Add(New Bold(New Run(dtPatient.Rows(0).Item("FirstName"))))
            lblNote.Inlines.Add(New Run("' is not registered on the practice portal, but has an email '"))
            lblNote.Inlines.Add(New Bold(New Run(dtPatient.Rows(0).Item("Email"))))
            lblNote.Inlines.Add(New Run("' address. A message may still be sent, and the Patient will receive an invitation to sign up for the portal. The Patient must sign up to receive this and any future messages."))

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(dtPatient) Then
                dtPatient.Dispose() : dtPatient = Nothing
            End If

            If Not IsNothing(clsIntuit) Then
                clsIntuit.Dispose() : clsIntuit = Nothing
            End If

        End Try
    End Sub

    ''' <summary>
    ''' Retrive Staff ID list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetStaffList()
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim dt As New DataTable

        Try

            oDB.Connect(False)
            oDB.Retrive("IntuitGetStaffList", dt)
            oDB.Disconnect()

            cmbStaff.ItemsSource = dt.DefaultView
            cmbStaff.DisplayMemberPath = "Description"
            cmbStaff.SelectedValuePath = "nStaffID"

            If dt.Rows.Count > 0 Then
                cmbStaff.SelectedIndex = 0
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

        End Try
    End Sub

    Private Sub GetCommPreferenceList()
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As DataTable = Nothing
        Dim ds As New DataSet

        Try
            oDBParameters.Add("@sType", "SecureMessages", ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Retrive("gsp_FillReminderforUnscheduledCare", oDBParameters, ds)

            If IsNothing(ds) = False AndAlso ds.Tables.Count > 0 Then

                dt = ds.Tables(0)
                If IsNothing(dt) = False Then
                    cmbCommPrefrence.ItemsSource = Nothing
                    cmbCommPrefrence.ItemsSource = dt.DefaultView
                    cmbCommPrefrence.DisplayMemberPath = "sDescription"
                    cmbCommPrefrence.SelectedValuePath = "nCategoryID"

                    If dt.Rows.Count > 0 Then
                        Dim row() As DataRow = dt.Select(" isSelected=1")
                        If (row.Length > 0) Then
                            cmbCommPrefrence.SelectedValue = row(0)("nCategoryID")
                        Else
                            cmbCommPrefrence.SelectedValue = 0
                        End If

                    End If
                End If
            End If

            oDBParameters.Clear()
            oDBParameters.Add("@nPatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_checkPatitentPrefcomm", oDBParameters, dt)
            oDB.Disconnect()
            Dim _strPatitentCommunicationPrefers As String
            _strPatitentCommunicationPrefers = ""
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    _strPatitentCommunicationPrefers = dt(0)(0).ToString()
                End If
            End If



            If _strPatitentCommunicationPrefers.Length > 0 Then
                lblPatientPrefers.Visibility = Windows.Visibility.Visible
                lblPatientPrefers.Content = "Patient Prefers Communication via """ + _strPatitentCommunicationPrefers + """"
                lblPatientPrefers.Tag = _strPatitentCommunicationPrefers

            Else
                lblPatientPrefers.Visibility = Windows.Visibility.Collapsed
            End If
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            If Not IsNothing(ds) Then
                ds.Dispose() : ds = Nothing
            End If

        End Try
    End Sub
    Private Sub GetMessageCaption()
        If appSettings("MessageBOXCaption") IsNot Nothing Then
            If appSettings("MessageBOXCaption") <> "" Then
                gstrMessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            End If
        End If
    End Sub

    Private Sub lstAttachment_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lstAttachment.MouseDown
        ' _isMouseDown = True
    End Sub

    Private Sub lstAttachment_MouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lstAttachment.MouseLeftButtonUp
        'If _isMouseDown = True Then
        '    _isMouseDown = False

        '    If lstAttachment.SelectedIndex >= 0 Then
        '        If lstAttachment.SelectedIndex >= 0 Then
        '            If (System.Windows.MessageBox.Show("Are you sure you want to remove the '" & GetAttachmentFileName(lstAttachment.SelectedIndex) & "' file?", gstrMessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) = MessageBoxResult.Yes Then

        '                RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
        '                lstAttachment.Items.Remove(lstAttachment.SelectedItem)
        '                AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged

        '                If lstAttachment.Items.Count >= 0 Then
        '                    lstAttachment.SelectedIndex = 0
        '                End If
        '            End If
        '        End If

        '    End If
        'End If
    End Sub
    ''Added for MU2 Patient Portal - Send Secure message to Patient Representative on 20131224
    Private Sub GetPatRepresentativeList()
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As DataTable = Nothing
        Dim ds As DataSet = Nothing

        Try
            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("WS_GetPatRepresentativeByPatID", oDBParameters, ds)

            If IsNothing(ds) = False AndAlso ds.Tables.Count > 0 Then

                dt = ds.Tables(0)
                If IsNothing(dt) = False Then
                    cmbPRList.ItemsSource = Nothing
                    cmbPRList.ItemsSource = dt.DefaultView
                    cmbPRList.DisplayMemberPath = "PatRepresentativeName"
                    cmbPRList.SelectedValuePath = "PRID"

                    If dt.Rows.Count > 0 Then
                        cmbPRList.SelectedValue = dt.Rows(0)(0)
                    Else
                        ''Fixed Bug id 62551 on 20140127
                        cmbMessageSendTo.Items.Remove(cbiPatientRepresentative)
                        cmbMessageSendTo.Items.Remove(cbiBoth)
                        ''End
                    End If
                End If
            End If

            oDBParameters.Clear()

            oDB.Disconnect()

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            If Not IsNothing(ds) Then
                ds.Dispose() : ds = Nothing
            End If

        End Try
    End Sub
    ''End MU2 Patient Portal - Send Secure message to Patient Representative
#End Region

#Region "DatePicker Event"

    Private Sub dtFromPtCCD_SelectedDateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles dtFromPtCCD.SelectedDateChanged
        If dtFromPtCCD.SelectedDate > dtToPtCCD.SelectedDate Then
            dtToPtCCD.SelectedDate = dtFromPtCCD.SelectedDate.Value
        End If
    End Sub

#End Region




    'Private Sub rbApprove_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbApprove.Click

    '    Select Case _strserviceType.ToUpper

    '        Case "APPOINTMENT REQUEST"
    '            If (rbApprove.IsChecked = True) Then
    '                If IncludeMessageInReply = True Then
    '                    txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]." & _OriginalMessage
    '                Else
    '                    txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]."
    '                End If
    '            End If

    '        Case "PRESCRIPTION RENEWAL"
    '            If (rbApprove.IsChecked = True) Then
    '                If IncludeMessageInReply = True Then
    '                    txtMessage.Text = "Your prescription(s) has been renewed." & _OriginalMessage
    '                Else
    '                    txtMessage.Text = "Your prescription(s) has been renewed."
    '                End If
    '            End If

    '        Case Else

    '    End Select

    'End Sub

    'Private Sub rbCancel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbCancel.Click
    '    Select Case _strserviceType.ToUpper

    '        Case "APPOINTMENT REQUEST"
    '            If (rbApprove.IsChecked = True) Then
    '                If IncludeMessageInReply = True Then
    '                    txtMessage.Text = "Your appointment has been denied." & _OriginalMessage
    '                Else
    '                    txtMessage.Text = "Your appointment has been denied."
    '                End If
    '            End If

    '        Case "PRESCRIPTION RENEWAL"
    '            If (rbApprove.IsChecked = True) Then
    '                If IncludeMessageInReply = True Then
    '                    txtMessage.Text = "Your prescription(s) has been denied." & _OriginalMessage
    '                Else
    '                    txtMessage.Text = "Your prescription(s) has been denied."
    '                End If
    '            End If

    '        Case Else

    '    End Select
    'End Sub

    Private Sub rbApprove_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbApprove.Checked
        Dim chkflag = 0
        If (rbApprove.IsEnabled = True) Then
            Select Case _strserviceType.ToUpper
                Case "APPOINTMENT REQUEST"
                    If (rbApprove.IsChecked = True) Then
                        If IncludeMessageInReply = True Then
                            txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]." & _OriginalMessage
                        Else
                            txtMessage.Text = "You have been granted an appointment on [DAY] at [TIME]."
                        End If
                        chkflag = 1
                    End If

                Case "PRESCRIPTION RENEWAL"
                    If (rbApprove.IsChecked = True) Then
                        If IncludeMessageInReply = True Then
                            txtMessage.Text = "Your prescription(s) has been renewed." & _OriginalMessage
                        Else
                            txtMessage.Text = "Your prescription(s) has been renewed."
                        End If
                        chkflag = 1
                    End If
                Case Else
            End Select


            If (chkflag = 1) Then
                dtAppointmentDate.IsEnabled = True
                cbHH.IsEnabled = True
                cbMM.IsEnabled = True
                cbMeridan.IsEnabled = True
            End If

        End If
    End Sub

    Private Sub rbCancel_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbCancel.Checked
        Dim chkflag = 0
        If (rbCancel.IsEnabled = True) Then
            Select Case _strserviceType.ToUpper
                Case "APPOINTMENT REQUEST"
                    If (rbCancel.IsChecked = True) Then
                        If IncludeMessageInReply = True Then
                            txtMessage.Text = "Your appointment has been denied." & _OriginalMessage
                        Else
                            txtMessage.Text = "Your appointment has been denied."
                        End If
                        chkflag = 1
                    End If

                Case "PRESCRIPTION RENEWAL"
                    If (rbCancel.IsChecked = True) Then
                        If IncludeMessageInReply = True Then
                            txtMessage.Text = "Your prescription(s) has been denied." & _OriginalMessage
                        Else
                            txtMessage.Text = "Your prescription(s) has been denied."
                        End If
                    End If
                    chkflag = 1
                Case Else

            End Select

            If (chkflag = 1) Then
                dtAppointmentDate.IsEnabled = False
                dtAppointmentDate.Text = ""
                cbHH.IsEnabled = False
                cbHH.SelectedIndex = 0
                cbMM.IsEnabled = False
                cbMM.SelectedIndex = 0
                cbMeridan.IsEnabled = False
                cbMeridan.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub chkCommPrefernce_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkCommPrefernce.Click
        If (chkCommPrefernce.IsChecked = True) Then
            cmbCommPrefrence.Visibility = Windows.Visibility.Visible

            If lblPatientPrefers.Tag IsNot Nothing Then
                If lblPatientPrefers.Tag.Trim() <> "" And lblPatientPrefers.Tag.Trim() <> cmbCommPrefrence.Text.Trim() Then
                    lblPatientPrefers.Visibility = Windows.Visibility.Visible
                Else
                    lblPatientPrefers.Visibility = Windows.Visibility.Collapsed
                End If
            End If

        Else
            cmbCommPrefrence.Visibility = Windows.Visibility.Collapsed
            lblPatientPrefers.Visibility = Windows.Visibility.Collapsed
        End If


    End Sub

    Private Sub cmbCommPrefrence_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCommPrefrence.DropDownClosed
        If (chkCommPrefernce.IsChecked = True) Then
            cmbCommPrefrence.Visibility = Windows.Visibility.Visible

            If lblPatientPrefers.Tag IsNot Nothing Then
                If lblPatientPrefers.Tag.Trim() <> "" And lblPatientPrefers.Tag.Trim() <> cmbCommPrefrence.Text.Trim() Then
                    lblPatientPrefers.Visibility = Windows.Visibility.Visible
                Else
                    lblPatientPrefers.Visibility = Windows.Visibility.Collapsed
                End If
            End If

        Else
            cmbCommPrefrence.Visibility = Windows.Visibility.Collapsed
            lblPatientPrefers.Visibility = Windows.Visibility.Collapsed
        End If
    End Sub

    ''Added for MU2 Patient Portal - Send Secure message to Patient Representative on 20131224
    Private Sub cmbMessageSendTo_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmbMessageSendTo.SelectionChanged
        Dim selectedTag = DirectCast(cmbMessageSendTo.SelectedItem, ComboBoxItem).Tag.ToString()
        If (selectedTag = 1 Or selectedTag = 2) Then
            cmbPRList.Visibility = Windows.Visibility.Visible
            If lblPR.Tag IsNot Nothing Then
                If lblPatRepresentative.Tag.Trim() <> "" And lblPatRepresentative.Tag.Trim() <> cmbPRList.Text.Trim() Then
                    lblPatRepresentative.Visibility = Windows.Visibility.Visible
                Else
                    lblPatRepresentative.Visibility = Windows.Visibility.Collapsed
                End If
            End If

        Else
            cmbPRList.Visibility = Windows.Visibility.Collapsed
            lblPatRepresentative.Visibility = Windows.Visibility.Collapsed
        End If
    End Sub
    ''End MU2 Patient Portal - Send Secure message to Patient Representative

    ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
    ''Added new function to get logged in user information.
    Private Function getLoginUserDetails(nuserID As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim dt As DataTable = Nothing
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            'Dim str As String = "select nUserID,sFirstName+' '+sLastName as LoginName ,sPortalDisplayName as PortalDisplayName from User_MST where nUserID=" & nuserID
            oDBParameters.Add("@UserID", Convert.ToInt64(nuserID), ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("ws_getLoggedInUserdetails", oDBParameters, dt)
            'oDB.Retrive_Query(str, dt)
            oDB.Disconnect()

            Return dt
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose() : dt = Nothing
            'End If
        End Try

    End Function

    Private Sub AttachPatientEducationMaterial(nPatientEducationID As Long)

        Dim dtAttach As New DataTable
        Dim binaryData As Byte() = Nothing
        Dim DocumentType As Integer = 0
        Dim mainURL As String = String.Empty
        Dim FilePath As String = String.Empty
        Dim TemplateName As String = String.Empty

        Try

            dtAttach = GetEducationMaterialUsingEducationID(nPatientEducationID)

            If dtAttach.Rows.Count > 0 Then
                DocumentType = Convert.ToInt16(dtAttach.Rows(0)("DocType"))
                TemplateName = Convert.ToString(dtAttach.Rows(0)("TemplateName"))

                If DocumentType = 1 Then 'Word Document
                    If Not IsNothing(dtAttach.Rows(0)("BinaryData")) Then
                        binaryData = CType(dtAttach.Rows(0)("BinaryData"), Byte())
                        FilePath = GenerateAndGetFilePath(binaryData, TemplateName)
                        txtSubject.Text = "Reading Materials - '" + TemplateName + "'"
                        txtMessage.Text = "Please read the information found in the attachment below: " + TemplateName + ".docx"
                        If FilePath <> String.Empty Then
                            AttachFile(FilePath, TemplateName)
                        End If
                    End If
                End If
                If DocumentType = 2 Then 'Online Document
                    mainURL = Convert.ToString(dtAttach.Rows(0)("MainURL"))
                    txtSubject.Text = "Reading Materials - '" + TemplateName + "'"
                    txtMessage.Text = "Please read the information found in this link: " + mainURL
                    'txtMessage.Text = mainURL
                    'txtSubject.Text = TemplateName
                End If
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(dtAttach) Then
                dtAttach.Dispose() : dtAttach = Nothing
            End If
        End Try


    End Sub

    Private Function GetEducationMaterialUsingEducationID(EducationID As Long) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim dt As New DataTable
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try

            oDBParameters.Add("@nEducationID", Convert.ToInt64(EducationID), ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("gsp_PatientPortalGetEducationMaterialUsingID", oDBParameters, dt)
            oDB.Disconnect()

            Return dt
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Function GenerateAndGetFilePath(binaryData As Byte(), templateName As String) As String

        Dim strFileName As String
        strFileName = ExamNewDocumentName(templateName)

        '    Dim stream As MemoryStream = Nothing
        Dim oFile As FileStream = Nothing

        Try

            If binaryData IsNot Nothing Then
                '           stream = New MemoryStream(binaryData)
                oFile = New FileStream(strFileName, System.IO.FileMode.Create)
                '          stream.WriteTo(oFile)
                oFile.Write(binaryData, 0, binaryData.Length)
                oFile.Close()
            Else
                Return ""
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return ""
        Finally

            If Not IsNothing(oFile) Then
                oFile.Dispose() : oFile = Nothing
            End If

            'If Not IsNothing(stream) Then
            '    stream.Dispose() : stream = Nothing
            'End If
        End Try

        Return strFileName
    End Function


    Private Function ExamNewDocumentName(templateName) As String

        'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath

        'Dim _NewDocumentName As String = ""
        'Dim _Extension As String = ".docx"
        'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

        'Dim i As Integer = 0
        '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & _Extension
        'While File.Exists(_Path & "\" & _NewDocumentName) = True
        '    i = i + 1
        '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & "-" & i & _Extension
        'End While
        'Return _Path & "\" & _NewDocumentName
        Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff")


    End Function


    Private Sub AttachFile(FilePath As String, TemplateName As String)
        Dim strFileName As String = ""
        Dim strFilePath As String = ""
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim ogloInterface As New gloCCDLibrary.gloCCDInterface

        Try

            Dim info As New FileInfo(FilePath)

            strFilePath = info.FullName
            strFileName = TemplateName & ".docx"

            ' Get the size of the file in bytes.
            Dim Bytes As Long = info.Length

            Dim dblConvertSize As Double
            dblConvertSize = GetConvertedBytesSize(Bytes)

            If dblConvertSize > 2.0 Then
                System.Windows.MessageBox.Show("File size of '" & strFileName & "' is " & SetBytes(Bytes) & ". File size should not exceed 2 MB.", gstrMessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information)
                Exit Sub
            End If

            AddListBoxItem(strFileName, strFilePath)

            Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)

            Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
            Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
            Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
            Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
            TBlock.Tag = arrByte

            info = Nothing
            arrByte = Nothing
            dblConvertSize = Nothing
            TBlock = Nothing
            stac = Nothing
            Innerstac = Nothing
            obd = Nothing




        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            strFileName = Nothing
            strFilePath = Nothing

            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose() : ogloInterface = Nothing
            End If

            If Not IsNothing(fd) Then
                fd.Dispose() : fd = Nothing
            End If

        End Try
    End Sub
    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID))
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog()
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult
                nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
                oForm = Nothing
            Else
                nProviderAssociationID = 0
                sProviderTaxID = ""
            End If

            If oResult = Windows.Forms.DialogResult.OK Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False

        Finally
        End Try
    End Function
    Public Function getCommDetailsID(ByVal nMessageID As Int64) As Long

        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim dt As New DataTable
        Dim nCommDetailID As Int64 = 0

        Try

            oDB.Connect(False)
            oDB.Retrive_Query("SELECT ICD.nCommDetailID FROM  dbo.IntuitCommDetails AS ICD  WHERE ICD.nMessageID =" + nMessageID.ToString(), dt)
            oDB.Disconnect()

            If Not IsNothing(dt) And dt.Rows.Count > 0 Then
                nCommDetailID = Convert.ToInt64(dt.Rows(0)("nCommDetailID"))
            End If

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

        End Try
        Return nCommDetailID
    End Function

End Class
