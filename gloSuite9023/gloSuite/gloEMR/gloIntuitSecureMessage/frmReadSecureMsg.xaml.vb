Imports System.Windows.Media
Imports System.IO
Imports System.Windows.Media.Animation
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms
Imports gloUserControlLibrary
Imports gloPatientPortalCommon

Public Class frmReadSecureMsg
    Implements System.IDisposable
    Private WithEvents uc As New gloUserControlLibrary.gloUC_PatientStrip()

#Region "Variable Declaration"
    Dim oslectedBorder As Border

    Public ht As Integer
    Private _lngPatientID As Int64
    Private _lngCommDetailId As Int64

    Private _strConnectionString As String = ""
    Dim _strTempFolder As String
    Dim _strStartUpPath As String
    Private _strClientMachineID As String
    Private _strClientMachineName As String
    Private _blnIncludeMessageInReply As Boolean = False
    Private _blnUserRight As Boolean
    Private _strLoginName As String = String.Empty
    Private _gstrSQLServerName As String = String.Empty
    Private _gstrDatabaseName As String = String.Empty
    Private _gblnSQLAuthentication As String = String.Empty
    Private _gstrSQLUserEMR As String = String.Empty
    Private _gstrSQLPasswordEMR As String = String.Empty
    Private _gblnDefaultPrinter As Boolean = False
    Private _dtMessage As New DataTable
    Dim _appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _strserviceType As String = ""

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

    Dim _blnIntuitCommunication As Boolean = False
    Dim _strPatientPortalSiteNm As String = ""
    Dim _strServiceURI As String = ""
    Dim _nClinicID As Long = 0
    ''Added for MU2 Patient Portal 
    Private _PortalType As String = String.Empty
    ''End

#End Region

#Region "Property Procedures"

    Public Property IncludeMessageInReply() As Boolean
        Get
            Return _blnIncludeMessageInReply
        End Get
        Set(ByVal value As Boolean)
            _blnIncludeMessageInReply = value
        End Set
    End Property

    Public Property IntuitUserRight() As Boolean
        Get
            Return _blnUserRight
        End Get
        Set(ByVal value As Boolean)
            _blnUserRight = value
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

#Region "Constants"
    Private Const COL_From As Int16 = 0
    ' Private Const COL_To As Int16 = 1
    Private Const COL_Subject As Int16 = 1
    Private Const COL_Message As Int16 = 2
    Private Const COL_Attachment1 As Int16 = 3
    Private Const COL_Attachment2 As Int16 = 4
    Private Const COL_Attachment3 As Int16 = 5
    Private Const COL_IAttachment1 As Int16 = 6
    Private Const COL_IAttachment2 As Int16 = 7
    Private Const COL_IAttachment3 As Int16 = 8
    Private Const COL_Date As Int16 = 9
    Private Const COL_ReadFlag As Int16 = 10
    Private Const COL_MessageId As Int16 = 11
    Private Const COL_MessageType As Int16 = 12
    Private Const COL_StaffID As Int16 = 13
    Private Const COL_StaffDescription As Int16 = 14

#End Region

#Region "IDisposable Support"
    Private disposed As Boolean ' To detect redundant calls 
    ' This code added by Visual Basic to correctly implement the disposable pattern.     
    Public Sub Dispose() Implements IDisposable.Dispose


        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.    
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)


        If Not Me.disposed Then
            If disposing Then

                'dispose managed state (managed objects).  
                RemoveHandler Me.Loaded, AddressOf frmReadSecureMsg_Loaded
                If IsNothing(lstAttachment) = False Then
                    RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
                    lstAttachment = Nothing
                End If
                If Not IsNothing(Wfh) Then
                    RemoveHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
                    Wfh.Dispose()
                    Wfh = Nothing
                End If

                If IsNothing(_dtMessage) = False Then
                    _dtMessage.Dispose()
                    _dtMessage = Nothing
                End If

                Me.Icon = Nothing
                Try
                    If (IsNothing(uc) = False) Then
                        uc.Dispose()
                        uc = Nothing
                    End If
                Catch ex As Exception

                End Try

                Me.Resources.Clear()
                'GC.Collect()
            End If
        End If

        Me.disposed = True
    End Sub
    Protected Overrides Sub Finalize()         ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.         Dispose(False)         MyBase.Finalize()     
        Dispose(False)
        MyBase.Finalize()

    End Sub
#End Region

#Region "Form Event"

    Private Sub frmReadSecureMsg_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.KeyUp
        Dim frm As New Form
        Dim strPath As String
        Try
            If e.Key = Key.F1 Then
                strPath = System.Windows.Forms.Application.StartupPath & "\Help\gloEMR_User_Manual.chm"
                System.Windows.Forms.Help.ShowHelp(frm, strPath, "Communication_(View_Sent_and_Received_messages).htm")
            End If
        Catch ex As Exception
        Finally
            strPath = Nothing
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub

    Private Sub frmReadSecureMsg_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Try
            If Me.IsLoaded Then
                'TryCast(Wfh.Child, gloUC_PatientStrip).Height = PatientStrip.Height

                txtMessage.IsEnabled = True
                txtFrom.IsEnabled = True
                txtFrom.IsReadOnly = True
                txtSubject.IsEnabled = True
                txtSubject.IsReadOnly = True
                txtTo.IsEnabled = True
                txtTo.IsReadOnly = True
                txtMessage.IsReadOnly = True

                ReadSecureMessage()
                FillListAttachment()
                'Wfh.Child = uc
                'uc.ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.Intuit)
                ''  Wfh.Child.BringToFront()
                'uc.DTPValue = Format(Date.Now, "MM/dd/yyyy")

                'uc.DTPFormat = DateTimePickerFormat.Short
                'uc.DTPEnabled = False
                'RemoveHandler uc.ControlSizeChanged, AddressOf ControlSizeChanged
                'AddHandler uc.ControlSizeChanged, AddressOf ControlSizeChanged




                Select Case _strserviceType.ToUpper

                    Case "APPOINTMENT REQUEST"
                        pnllstAttachment.Visibility = Windows.Visibility.Collapsed
                       

                    Case "PRESCRIPTION RENEWAL"
                        pnllstAttachment.Visibility = Windows.Visibility.Collapsed
                       

                    Case "ONLINE BILL PAY"
                        pnllstAttachment.Visibility = Windows.Visibility.Collapsed
                      

                End Select

                'If _PortalType = "gloPatientPortal" Then
                '    ResizeMode = Windows.ResizeMode.CanResize
                '    SizeToContent = Windows.SizeToContent.Manual
                '    Height = 600
                '    ResizeMode = Windows.ResizeMode.NoResize
                'End If


            End If
            '  WaitCursor.Visibility = Windows.Visibility.Hidden
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)

        End Try
    End Sub
#End Region

#Region "Constructor"

    Public Sub New(ByVal PatientID As Int64, ByVal CommDetailID As Int64, ByVal ConnectionString As String, ByVal TempFolder As String, ByVal sClientMachineID As String, ByVal sClientMachineName As String, Optional ByVal IsFrom As String = "")

        ' This call is required by the designer.
        InitializeComponent()


        _lngPatientID = PatientID
        _lngCommDetailId = CommDetailID
        _strConnectionString = ConnectionString
        '_strStartUpPath = StartUpPath
        _strTempFolder = TempFolder
        _strClientMachineID = sClientMachineID
        _strClientMachineName = sClientMachineName

        ''Added for MU2 Patient Portal on 20130827
        _PortalType = IsFrom
        If _PortalType = "gloPatientPortal" Then
			 pnllstAttachment.Visibility = Windows.Visibility.Collapsed
           '' pnllstAttachment.Visibility = Windows.Visibility.Hidden
            '' FillMainPanel.Height = 830 - 250
            txtMessage.MaxLength = 8000
        Else
            pnllstAttachment.Visibility = Windows.Visibility.Visible
            '' FillMainPanel.Height = 830
            txtMessage.MaxLength = 4000
        End If

        ''End
        ' Add any initialization after the InitializeComponent() call.
        If _appSettings("UserName") IsNot Nothing Then
            If _appSettings("UserName") <> "" Then
                _strLoginName = Convert.ToString(_appSettings("UserName"))
            End If
        End If
        If _appSettings("SQLServerName") IsNot Nothing Then
            If _appSettings("SQLServerName") <> "" Then
                _gstrSQLServerName = Convert.ToString(_appSettings("SQLServerName"))
            End If
        End If
        If _appSettings("DatabaseName") IsNot Nothing Then
            If _appSettings("DatabaseName") <> "" Then
                _gstrDatabaseName = Convert.ToString(_appSettings("DatabaseName"))
            End If
        End If
        If _appSettings("SQLLoginName") IsNot Nothing Then
            If _appSettings("SQLLoginName") <> "" Then
                _gstrSQLUserEMR = Convert.ToString(_appSettings("SQLLoginName"))
            End If
        End If
        If _appSettings("SQLPassword") IsNot Nothing Then
            If _appSettings("SQLPassword") <> "" Then
                _gstrSQLPasswordEMR = Convert.ToString(_appSettings("SQLPassword"))
            End If
        End If
        If _appSettings("DefaultPrinter") IsNot Nothing Then
            If _appSettings("DefaultPrinter") <> "" Then
                _gblnDefaultPrinter = Not Convert.ToBoolean(_appSettings("DefaultPrinter"))
            End If
        End If
        If _appSettings("WindowAuthentication") IsNot Nothing Then
            If _appSettings("WindowAuthentication") <> "" Then
                _gblnSQLAuthentication = Not Convert.ToBoolean(_appSettings("WindowAuthentication"))
            End If
        End If
        AddControls()


    End Sub
    Public Sub New(ByVal PatientID As Int64, ByVal CommDetailID As Int64, ByVal ConnectionString As String, ByVal TempFolder As String, ByVal sClientMachineID As String, ByVal sClientMachineName As String, ByVal IsFrom As String, ByVal strServiceURI As String, ByVal strPatientPortalSiteNm As String, ByVal nClinicID As Long, ByVal blnIntuitCommunication As Boolean)

        ' This call is required by the designer.
        InitializeComponent()


        _lngPatientID = PatientID
        _lngCommDetailId = CommDetailID
        _strConnectionString = ConnectionString
        '_strStartUpPath = StartUpPath
        _strTempFolder = TempFolder
        _strClientMachineID = sClientMachineID
        _strClientMachineName = sClientMachineName

        'MU2 Patient Portal
        _strServiceURI = strServiceURI
        _strPatientPortalSiteNm = strPatientPortalSiteNm
        _nClinicID = nClinicID
        _blnIntuitCommunication = blnIntuitCommunication
        'MU2 Patient Portal
        ''Added for MU2 Patient Portal on 20130827
        _PortalType = IsFrom
        If _PortalType = "gloPatientPortal" Then
			 pnllstAttachment.Visibility = Windows.Visibility.Collapsed
           '' pnllstAttachment.Visibility = Windows.Visibility.Hidden
            '' FillMainPanel.Height = 830 - 250
            txtMessage.MaxLength = 8000
        Else
            pnllstAttachment.Visibility = Windows.Visibility.Visible
            ''FillMainPanel.Height = 830
            txtMessage.MaxLength = 4000
        End If

        ''End
        ' Add any initialization after the InitializeComponent() call.
        If _appSettings("UserName") IsNot Nothing Then
            If _appSettings("UserName") <> "" Then
                _strLoginName = Convert.ToString(_appSettings("UserName"))
            End If
        End If
        If _appSettings("SQLServerName") IsNot Nothing Then
            If _appSettings("SQLServerName") <> "" Then
                _gstrSQLServerName = Convert.ToString(_appSettings("SQLServerName"))
            End If
        End If
        If _appSettings("DatabaseName") IsNot Nothing Then
            If _appSettings("DatabaseName") <> "" Then
                _gstrDatabaseName = Convert.ToString(_appSettings("DatabaseName"))
            End If
        End If
        If _appSettings("SQLLoginName") IsNot Nothing Then
            If _appSettings("SQLLoginName") <> "" Then
                _gstrSQLUserEMR = Convert.ToString(_appSettings("SQLLoginName"))
            End If
        End If
        If _appSettings("SQLPassword") IsNot Nothing Then
            If _appSettings("SQLPassword") <> "" Then
                _gstrSQLPasswordEMR = Convert.ToString(_appSettings("SQLPassword"))
            End If
        End If
        If _appSettings("DefaultPrinter") IsNot Nothing Then
            If _appSettings("DefaultPrinter") <> "" Then
                _gblnDefaultPrinter = Not Convert.ToBoolean(_appSettings("DefaultPrinter"))
            End If
        End If
        If _appSettings("WindowAuthentication") IsNot Nothing Then
            If _appSettings("WindowAuthentication") <> "" Then
                _gblnSQLAuthentication = Not Convert.ToBoolean(_appSettings("WindowAuthentication"))
            End If
        End If
        AddControls()


    End Sub
#End Region

#Region "Procedures"

    Private Sub AddControls()

        TryCast(Wfh.Child, gloUC_PatientStrip).ShowDetail(_lngPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.Intuit)

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
    Private Sub ReadSecureMessage()
        Dim _oclsSecureMsg As New ClsIntuitSecureMessage(_strConnectionString, _lngPatientID, _lngCommDetailId)
        Try
            _dtMessage = _oclsSecureMsg.ReadMessageFunctionality(0)

            If IsNothing(_dtMessage) = False Then
                If _dtMessage.Rows.Count > 0 Then
                    txtFrom.Text = _dtMessage.Rows(0)(COL_From)
                    txtSubject.Text = _dtMessage.Rows(0)(COL_Subject)
                    txtMessage.Text = _dtMessage.Rows(0)(COL_Message)
                    dtpDate.SelectedDate = _dtMessage.Rows(0)(COL_Date)
                    lblTime.Content = Convert.ToDateTime(_dtMessage.Rows(0)(COL_Date)).ToShortTimeString()
                    If _dtMessage.Rows(0)(COL_StaffDescription) <> "" Then
                        ''''Task #67507: gloEMR & Patient Portal Send Message screen changes.
                        ''Only shows staff description not staffID
                        If _PortalType = "gloPatientPortal" Then
                            txtTo.Text = _dtMessage.Rows(0)(COL_StaffDescription)
                        Else
                            txtTo.Text = _dtMessage.Rows(0)(COL_StaffID) & " - " & _dtMessage.Rows(0)(COL_StaffDescription)
                        End If
                    End If

                    If Not IsNothing(_dtMessage.Rows(0)("sCommServiceType")) Then
                        If _dtMessage.Rows(0)("sCommServiceType") <> "" Then
                            _strserviceType = _dtMessage.Rows(0)("sCommServiceType")
                        End If
                    End If


                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        Finally
            If IsNothing(_oclsSecureMsg) = False Then
                _oclsSecureMsg.Dispose()
                _oclsSecureMsg = Nothing
            End If
        End Try
    End Sub
    Private Shared TahomaFontFamily As FontFamily = New FontFamily("Tahoma")
    Private Sub FillListAttachment()

        Try

            Dim obrdListItem As Border = Nothing
            Dim selectedIndex As Int16 = 0
            Dim ostackMain As StackPanel = Nothing, ostackInner As StackPanel = Nothing
            Dim olbl As TextBlock = Nothing
            Dim oimgAckw As Image = Nothing
            ' Dim colAttachment As Int16
            If IsNothing(_dtMessage) = False Then
                If _dtMessage.Rows.Count > 0 Then
                    For colAttachment As Int16 = COL_Attachment1 To COL_Attachment3
                        If _dtMessage.Rows(0)(colAttachment) <> "" Then
                            obrdListItem = New Border()

                            'obrdListItem.BorderThickness = New Thickness(1)
                            'obrdListItem.CornerRadius = New CornerRadius(6)
                            'obrdListItem.BorderBrush = Brushes.Black

                            Try
                                RemoveHandler obrdListItem.MouseLeftButtonDown, AddressOf Border_MouseClick
                            Catch ex As Exception

                            End Try

                            AddHandler obrdListItem.MouseLeftButtonDown, AddressOf Border_MouseClick
                            Try
                                RemoveHandler obrdListItem.MouseLeftButtonDown, AddressOf Border_MouseClick
                            Catch ex As Exception

                            End Try

                            AddHandler obrdListItem.MouseLeftButtonDown, AddressOf Border_MouseClick
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
                            oimgAckw.VerticalAlignment = Windows.VerticalAlignment.Top
                            oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
                            ostackInner.Children.Add(oimgAckw)

                            olbl = New TextBlock()
                            olbl.Margin = New Thickness(5, 2, 5, 0)
                            olbl.TextWrapping = TextWrapping.Wrap
                            olbl.Width = 400

                            olbl.Foreground = Brushes.Black
                            olbl.FontFamily = TahomaFontFamily 'New FontFamily("Tahoma")

                            olbl.FontSize = 12
                            If colAttachment = COL_Attachment1 Then
                                olbl.Text = _dtMessage.Rows(0)(colAttachment)
                                olbl.Tag = _dtMessage.Rows(0)(COL_IAttachment1)
                            ElseIf colAttachment = COL_Attachment2 Then
                                olbl.Text = _dtMessage.Rows(0)(colAttachment)
                                olbl.Tag = _dtMessage.Rows(0)(COL_IAttachment2)
                            ElseIf colAttachment = COL_Attachment3 Then
                                olbl.Text = _dtMessage.Rows(0)(colAttachment)
                                olbl.Tag = _dtMessage.Rows(0)(COL_IAttachment3)

                            End If

                            olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
                            ostackInner.Children.Add(olbl)
                            ostackMain.Children.Add(ostackInner)
                            obrdListItem.Child = ostackMain
                            lstAttachment.ScrollIntoView(lstAttachment)
                            lstAttachment.Items.Add(obrdListItem)
                       
                        End If
                    Next
                    lstAttachment.SelectedIndex = 0
                Else

                End If
            Else

            End If
            If IsNothing(obrdListItem) = False Then
                obrdListItem = Nothing
            End If
            If IsNothing(ostackMain) = False Then
                ostackMain = Nothing
            End If
            If IsNothing(ostackInner) = False Then
                ostackInner = Nothing
            End If
            If IsNothing(olbl) = False Then
                olbl = Nothing
            End If
            If IsNothing(oimgAckw) = False Then
                oimgAckw = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)

        End Try
    End Sub

    ''' <summary>
    ''' Controls the size changed.
    ''' </summary>
    Private Sub ControlSizeChanged()
        TryCast(Wfh.Child, gloUC_PatientStrip).Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
        PatientStrip.Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
        ht = PatientStrip.Height
        ''  SetFormHeight()
    End Sub
    'Private Sub obrdinner_Mouseenter(ByVal sender As Object, ByVal e As RoutedEventArgs)

    '    Dim obd As Border = DirectCast(sender, Border)
    '    Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
    '    Storyboard.SetTarget(sb, obd)
    '    sb.Begin()
    '    If IsNothing(obd) = False Then
    '        obd = Nothing
    '    End If
    '    If IsNothing(sb) = False Then
    '        sb = Nothing
    '    End If
    'End Sub
    Private Sub Border_MouseClick(ByVal sender As System.Object, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then

            Dim AttachmentData() As Byte
            Dim strFileName As String = ""

            Try


                If IsNothing(lstAttachment.SelectedItem) = False Then
                    Dim oSelectedItem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                    Dim oStackPnl As StackPanel = oSelectedItem.Child
                    Dim oinnerpnl As StackPanel = oStackPnl.Children(0)
                    Dim oSelectedText As TextBlock = oinnerpnl.Children(1)
                    Dim _Extension() As String
                    Dim _FileExt As String = ""
                    _Extension = oSelectedText.Text.Split(".")
                    If _Extension.Length > 1 Then
                        _FileExt = _Extension(_Extension.Length - 1)
                    End If
                    AttachmentData = oSelectedText.Tag
                    If IsDBNull(AttachmentData) = False Then
                        strFileName = ExamNewFileName(_strTempFolder, "." & _FileExt)
                        strFileName = GenerateFile(AttachmentData, strFileName)
                        If strFileName <> "" Then
                            Dim startInfo As System.Diagnostics.ProcessStartInfo
                            Dim pStart As New System.Diagnostics.Process
                            startInfo = New System.Diagnostics.ProcessStartInfo(strFileName)
                            startInfo.UseShellExecute = True
                            startInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
                            startInfo.CreateNoWindow = False
                            Process.Start(startInfo)
                        End If

                    End If

                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
            Finally

                ' WaitCursor.Visibility = Windows.Visibility.Hidden
            End Try

        End If
    End Sub
    Private Sub obrd_MouseEnter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim obd As Border = DirectCast(sender, Border)
        Dim sb As New Storyboard
        Try
            sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
            Storyboard.SetTarget(sb, obd)
            sb.Begin()
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.ToString())
        Finally
            If IsNothing(obd) = False Then
                obd = Nothing
            End If
            If IsNothing(sb) = False Then
                sb = Nothing
            End If
        End Try
    End Sub

    Private Sub lstAttachment_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) 'Handles lstLabTest.SelectionChanged
        Try
            If lstAttachment.Items.Count > 0 Then
              
                If IsNothing(lstAttachment.SelectedItem) = False Then
                    Dim oitem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                    ''  oitem.Style = DirectCast(FindResource("DataGridCellStyleIsSelectedBackground"), Style)
                    oslectedBorder = oitem
                End If
            End If

        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exc.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(exc.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        End Try
    End Sub

    'Private Sub lstAttachment_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lstAttachment.MouseDoubleClick
    '    Dim AttachmentData() As Byte
    '    Dim strFileName As String = ""

    '    Try


    '        If IsNothing(lstAttachment.SelectedItem) = False Then
    '            Dim oSelectedItem As Border = DirectCast(lstAttachment.SelectedItem, Border)
    '            Dim oStackPnl As StackPanel = oSelectedItem.Child
    '            Dim oinnerpnl As StackPanel = oStackPnl.Children(0)
    '            Dim oSelectedText As TextBlock = oinnerpnl.Children(1)
    '            Dim _Extension() As String
    '            Dim _FileExt As String = ""
    '            _Extension = oSelectedText.Text.Split(".")
    '            If _Extension.Length > 1 Then
    '                _FileExt = _Extension(_Extension.Length - 1)
    '            End If
    '            AttachmentData = oSelectedText.Tag
    '            If IsDBNull(AttachmentData) = False Then
    '                strFileName = ExamNewFileName(_strStartUpPath & _strTempFolder, "." & _FileExt)
    '                strFileName = GenerateFile(AttachmentData, strFileName)
    '                If strFileName <> "" Then
    '                    Dim startInfo As System.Diagnostics.ProcessStartInfo
    '                    Dim pStart As New System.Diagnostics.Process
    '                    startInfo = New System.Diagnostics.ProcessStartInfo(strFileName)
    '                    startInfo.UseShellExecute = True
    '                    startInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
    '                    startInfo.CreateNoWindow = False
    '                    Process.Start(startInfo)
    '                End If

    '            End If

    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
    '    Finally

    '        ' WaitCursor.Visibility = Windows.Visibility.Hidden
    '    End Try


    'End Sub
    Public ReadOnly Property ExamNewFileName(ByVal _path As String, ByVal extension As String) As String
        Get
            Dim _strDocumentName As String = System.Guid.NewGuid.ToString()
            Dim _strNewDocumentName As String
            _strNewDocumentName = _strDocumentName & extension
            Return _path & _strNewDocumentName
        End Get
    End Property
    'Private Sub SetFormHeight()

    '    If (_strserviceType.ToUpper <> "") Then

    '        Select Case _strserviceType.ToUpper
    '            Case "APPOINTMENT REQUEST"

    '                Me.Height = 780
    '                If (PatientStrip.Height = 217) Then
    '                    txtMessage.Height = 220
    '                Else
    '                    Dim _height As Int16 = 217 - PatientStrip.Height
    '                    txtMessage.Height = 220 + _height
    '                End If

    '            Case "PRESCRIPTION RENEWAL"
    '                Me.Height = 780
    '                If (PatientStrip.Height = 217) Then
    '                    txtMessage.Height = 220
    '                Else
    '                    Dim _height As Int16 = 217 - PatientStrip.Height
    '                    txtMessage.Height = 220 + _height
    '                End If

    '            Case "ONLINE BILL PAY"
    '                Me.Height = 780
    '                If (PatientStrip.Height = 217) Then
    '                    txtMessage.Height = 220
    '                Else
    '                    Dim _height As Int16 = 217 - PatientStrip.Height
    '                    txtMessage.Height = 220 + _height
    '                End If
    '            Case "ASK A DOCTOR"
    '                Me.Height = 610 + 60 + PatientStrip.Height
    '                txtMessage.Height = 170
    '            Case "PATIENT MESSAGING"
    '                Me.Height = 610 + 60 + PatientStrip.Height
    '                txtMessage.Height = 170




    '        End Select
    '    Else
    '        'Me.Height = 610 + 60 + PatientStrip.Height
    '        'txtMessage.Height = 170

    '    End If




    'End Sub

    'Private Sub GetFormHeight()

    '    Me.Height = 610 + 60 + PatientStrip.Height

    'End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPrint.Click
        Try

            Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
            clsPrntRpt = New gloSSRSApplication.clsPrintReport(_gstrSQLServerName, _gstrDatabaseName, _gblnSQLAuthentication, _gstrSQLUserEMR, _gstrSQLPasswordEMR)
            clsPrntRpt.PrintReport("rptIntuitSecureMsg", "PatientID,CommDetailID,User", _lngPatientID & "," & _lngCommDetailId & "," & _strLoginName, _gblnDefaultPrinter, "")
            If IsNothing(clsPrntRpt) = False Then
                clsPrntRpt = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        End Try


    End Sub

#End Region

#Region "Function"

    Private Function GenerateFile(ByVal oResult() As Byte, ByVal strFileName As String) As String
        Try
            If IsNothing(oResult) = False Then
                Dim content As Byte() = CType(oResult, Byte())
                ' Dim stream As MemoryStream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)
                'stream.WriteTo(oFile)
                oFile.Close()
                oFile.Dispose()
                oFile = Nothing
                If IsNothing(oResult) = False Then
                    oResult = Nothing
                End If
                If IsNothing(content) = False Then
                    content = Nothing
                End If
                Return strFileName
            Else
                Return Nothing
            End If



        Catch ex As Exception
            Return Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Function

#End Region

#Region "Button Events"


    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        Dim _oclsIntuitSecureMessage As New ClsIntuitSecureMessage(_strConnectionString, _lngPatientID, _lngCommDetailId)
        Try
            _oclsIntuitSecureMessage.ReadMessageFunctionality(3)
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        Finally
            If IsNothing(_oclsIntuitSecureMessage) = False Then
                _oclsIntuitSecureMessage.Dispose()
                _oclsIntuitSecureMessage = Nothing
            End If
        End Try

    End Sub

    Private Sub btnCreateTask_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnCreateTask.Click
        Try
            Dim ofrmTask As gloTaskMail.frmTask
            ofrmTask = New gloTaskMail.frmTask(_strConnectionString, 0, _lngCommDetailId, txtSubject.Text, txtMessage.Text, lstAttachment.Items.Count, True)
            ofrmTask.Text = "Tasks"
            ofrmTask.PatientID = _lngPatientID
            '  ofrmTask.TopMost = True
            ''Bug #68447: Patient portal communication Tab,Create task throwing Exception .
            ''Code changes to resolve exception due to ofrmTask.ShowDialog(Me) and replaced by ofrmTask.ShowDialog(ofrmTask.Parent)
            ofrmTask.ShowDialog(ofrmTask.Parent)
            ofrmTask.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
        ''added right for delete message.
        If _PortalType = "gloPatientPortal" Then
            If IntuitUserRight = False Then
                System.Windows.MessageBox.Show("This user does not have the rights to delete a secure message. Please contact your system administrator for the same.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If

        Dim _strmessage As String = ""
        Dim _oclsIntuitSecureMessage As New ClsIntuitSecureMessage(_strConnectionString, _lngPatientID, _lngCommDetailId)
        Try

            _strmessage = Environment.NewLine & Environment.NewLine & "From     : " & txtFrom.Text.ToString().Trim & Environment.NewLine & "To          : " & txtTo.Text.ToString().Trim & Environment.NewLine & "Subject  : " & txtSubject.Text.ToString().Trim & Environment.NewLine & "Date       : " & dtpDate.ToString().Trim
            Dim _dResult As DialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this secure message?" + _strmessage, "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If _dResult.ToString() = "Yes" Then
                _oclsIntuitSecureMessage.DeleteMessageFunctionality(2)
                Me.Close()
                Me.Dispose()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        Finally
            If IsNothing(_oclsIntuitSecureMessage) = False Then
                _oclsIntuitSecureMessage.Dispose()
                _oclsIntuitSecureMessage = Nothing
            End If
        End Try
    End Sub

    Private Sub btnMarkUnread_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnMarkUnread.Click
        Dim _oclsIntuitSecureMessage As New ClsIntuitSecureMessage(_strConnectionString, _lngPatientID, _lngCommDetailId)
        Try
            _oclsIntuitSecureMessage.ReadMessageFunctionality(1)
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        Finally
            If IsNothing(_oclsIntuitSecureMessage) = False Then
                _oclsIntuitSecureMessage.Dispose()
                _oclsIntuitSecureMessage = Nothing
            End If
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnNew.Click
        ''Bug #71619: Blocked patient is allowed to reply on message
        ''Added to restrict to reply or send new message to block user.
        If _PortalType = "gloPatientPortal" Then
            If IsPatientBlockOnPortal() Then
                Exit Sub
            End If
        End If

        If IntuitUserRight = False Then
            System.Windows.MessageBox.Show("This user does not have the rights to send a new secure message. Please contact your system administrator for the same.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
        ''Pass _nCommDetailsID =0 for portal to make form editable and user able to click on send.
        Dim _nCommDetailsID As Int64 = 0
        If _PortalType <> "gloPatientPortal" Then
            _nCommDetailsID = 1
        End If

        ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
        ''Pass _nCommDetailsID =0 for portal to make form editable and user able to click on send.
        Dim _ofrmSendMsg As New frmSendSecureMsg(_lngPatientID, _strConnectionString, _strClientMachineID, _strClientMachineName, _nCommDetailsID, _PortalType)
        Try
            _ofrmSendMsg.WindowStartupLocation = Windows.WindowStartupLocation.Manual
            Dim boundWidth As Int16 = Screen.PrimaryScreen.Bounds.Width
            Dim boundHeight As Int16 = Screen.PrimaryScreen.Bounds.Height
            Dim x As Int16 = boundWidth - Me.Width
            Dim y As Int16 = boundHeight - Me.Height
            _ofrmSendMsg.Left = Convert.ToInt16(boundHeight / 4)
            _ofrmSendMsg.Top = 25
            _ofrmSendMsg.ShowInTaskbar = False
            _ofrmSendMsg.Owner = Me


            'setting value for split screen
            _ofrmSendMsg.objPatientExam = m_objPatientExam
            _ofrmSendMsg.objPatientMessages = m_objPatientMessages
            _ofrmSendMsg.objPatientLetters = m_objPatientLetters
            _ofrmSendMsg.objNurseNotes = m_objNurseNotes
            _ofrmSendMsg.objHistory = m_objHistory
            _ofrmSendMsg.objLabs = m_objLabs
            _ofrmSendMsg.objDMS = m_objDMS
            _ofrmSendMsg.objRxmed = m_objRxmed
            _ofrmSendMsg.objOrders = m_objOrders
            _ofrmSendMsg.objProblemList = m_objProblemList
            _ofrmSendMsg.objCriteria = m_objCriteria
            _ofrmSendMsg.objWord = m_objWord

            _ofrmSendMsg.ShowDialog()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        Finally
            If IsNothing(_ofrmSendMsg) = False Then
                _ofrmSendMsg.Close()
                _ofrmSendMsg = Nothing
            End If
        End Try
    End Sub

    Private Sub btnReply_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnReply.Click
        ''Bug #71619: Blocked patient is allowed to reply on message
        ''Added to restrict to reply or send new message to block user.
        If _PortalType = "gloPatientPortal" Then
            If IsPatientBlockOnPortal() Then
                Exit Sub
            End If
        End If
        If IntuitUserRight = False Then
            ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
            ''change message text.
            System.Windows.MessageBox.Show("This user does not have the rights to reply a secure message. Please contact your system administrator for the same.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ''Added Portal Type for MU2 Patient Portal on 20130827
        Dim _ofrmSendMsg As New frmSendSecureMsg(_lngPatientID, _strConnectionString, _strClientMachineID, _strClientMachineName, _lngCommDetailId, _PortalType, _strServiceURI.ToString, _strPatientPortalSiteNm, _nClinicID, _blnIntuitCommunication)
        Dim _oClsIntuitSecureMessage As ClsIntuitSecureMessage = Nothing
        Dim _msgFlag As Int16 = 0
        Try
            _oClsIntuitSecureMessage = New ClsIntuitSecureMessage(_strConnectionString, _lngPatientID, _lngCommDetailId)
            _msgFlag = _oClsIntuitSecureMessage.IntuitCheckIsReplied(_lngCommDetailId)
            If (_msgFlag = 1) Then
                System.Windows.MessageBox.Show("Your practice has already responded to the appointment request. It is not possible to respond a second time. If there are any changes in the appointment request, please send the patient a new message.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If (_msgFlag = 2) Then
                System.Windows.MessageBox.Show("Your practice has already responded to the prescription renewal request. It is not possible to respond a second time. If there are any changes in the prescription renewal request, please send the patient a new message.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            _ofrmSendMsg.WindowStartupLocation = Windows.WindowStartupLocation.Manual
            Dim boundWidth As Int16 = Screen.PrimaryScreen.Bounds.Width
            Dim boundHeight As Int16 = Screen.PrimaryScreen.Bounds.Height
            Dim x As Int16 = boundWidth - Me.Width
            Dim y As Int16 = boundHeight - Me.Height
            _ofrmSendMsg.Left = Convert.ToInt16(boundHeight / 4)
            _ofrmSendMsg.Top = 25
            _ofrmSendMsg.IsReplyMessage = True
            _ofrmSendMsg.ShowInTaskbar = False
            _ofrmSendMsg.Owner = Me
            ' _ofrmSendMsg.WindowStartupLocation = WindowStartupLocation.CenterOwner
            '_ofrmSendMsg.SubjectFromReadMessage = txtSubject.Text.Trim()
            '_ofrmSendMsg.MessageFromReadMessage = txtMessage.Text.Trim()
            '_ofrmSendMsg.StaffDescriptionFromReadMessage = txtTo.Text
            _ofrmSendMsg.IncludeMessageInReply = IncludeMessageInReply


            'setting value for split screen
            _ofrmSendMsg.objPatientExam = m_objPatientExam
            _ofrmSendMsg.objPatientMessages = m_objPatientMessages
            _ofrmSendMsg.objPatientLetters = m_objPatientLetters
            _ofrmSendMsg.objNurseNotes = m_objNurseNotes
            _ofrmSendMsg.objHistory = m_objHistory
            _ofrmSendMsg.objLabs = m_objLabs
            _ofrmSendMsg.objDMS = m_objDMS
            _ofrmSendMsg.objRxmed = m_objRxmed
            _ofrmSendMsg.objOrders = m_objOrders
            _ofrmSendMsg.objProblemList = m_objProblemList
            _ofrmSendMsg.objCriteria = m_objCriteria
            _ofrmSendMsg.objWord = m_objWord

            _ofrmSendMsg.ShowDialog()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            System.Windows.MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        Finally
            If IsNothing(_oClsIntuitSecureMessage) = False Then
                _oClsIntuitSecureMessage.Dispose()
                _oClsIntuitSecureMessage = Nothing
            End If
            If IsNothing(_ofrmSendMsg) = False Then
                _ofrmSendMsg.Close()
                _ofrmSendMsg = Nothing
            End If
        End Try
    End Sub

    ''Bug #71619: Blocked patient is allowed to reply on message
    ''Added to restrict to reply or send new message to block user.

    Public Function IsPatientBlockOnPortal() As Boolean
        Dim bIsPatientBlock As Boolean = False
        Dim clsPatientPortal As New clsgloPatientPortalEmail(_strConnectionString)
        Try
            Dim dtPortalValidUser As DataTable = clsPatientPortal.ToCheckPatientRegisterOrNotOnPortal(_lngPatientID)
            If dtPortalValidUser IsNot Nothing AndAlso dtPortalValidUser.Rows.Count > 0 Then
                bIsPatientBlock = False
            Else
                bIsPatientBlock = True
                'MessageBox.Show("Only registered patients may receive messages on the portal. Patient is not currently registered on the practice portal. If you proceed, a request will be sent to the patient to register and the patient will receive your message after their registration has been accepted. Would you like to proceed? " + System.Environment.NewLine + " " + System.Environment.NewLine + "Note: If the patient has already registered and you are getting this message, please check that there is no pending unaccepted task for the patient’s registration. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                System.Windows.MessageBox.Show("Patient does not have an active portal account. No portal messages may be sent to this patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If Not IsNothing(dtPortalValidUser) Then  ''slr free dtPortalValidUser
                dtPortalValidUser.Dispose()
                dtPortalValidUser = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        Finally
            clsPatientPortal = Nothing
        End Try
        Return bIsPatientBlock
    End Function
#End Region





End Class
