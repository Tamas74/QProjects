Imports System.Windows.Media
Imports System.IO
Imports System.Windows.Media.Animation
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.SqlClient

Public Class UC_ReadSecureMsg
    Dim oslectedBorder As Border
    Public Event SetFormHeight()
    Public Event GetFormHeight()
    Public Event CloseIntuitForm()
    Public Event OpenSendSecureMsgForm()
    Public ht As Integer
    Private _PatientID As Int64
    Private _CommDetailId As Int64

    Private _ConnectionString As String = ""
    Dim _TempFolder As String
    Dim _StartUpPath As String

   

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
    Dim dtMessage As New DataTable
#End Region
    Private Sub UC_ReadSecureMsg_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded

        Try
           
            WaitCursor.Visibility = Windows.Visibility.Hidden
            AddControls()
            ReadSecureMessage()
            FillListAttachment()

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        End Try
    End Sub
    Private Sub ReadSecureMessage()

        Try
            dtMessage = ReadMessageFunctionality(0)
            If IsNothing(dtMessage) = False Then
                If dtMessage.Rows.Count > 0 Then
                    txtFrom.Text = dtMessage.Rows(0)(COL_From)
                    txtSubject.Text = dtMessage.Rows(0)(COL_Subject)
                    txtMessage.Text = dtMessage.Rows(0)(COL_Message)
                    dtpDate.SelectedDate = dtMessage.Rows(0)(COL_Date)
                    If dtMessage.Rows(0)(COL_MessageType) = False Then
                        lblReply.Content = "Send Again"
                        ImgReply.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Symbol Ok.png", UriKind.Relative))

                    Else
                        lblReply.Content = "Reply"
                        ImgReply.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Symbol Cancel.png", UriKind.Relative))

                    End If
                    
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        End Try
    End Sub
    Private Function ReadMessageFunctionality(ByVal _FlagType As Integer) As DataTable
        Dim _sqlconn As SqlConnection = New SqlConnection(_ConnectionString)
        Dim _sqlcmd As SqlCommand
        Dim _sqlda As SqlDataAdapter
        Dim dt As New DataTable
        Dim _sqlparam As SqlParameter
        Try

            _sqlcmd = New SqlCommand("Intuit_ReadSecureMessage", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure
            _sqlda = New SqlDataAdapter(_sqlcmd)
            ''
            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = _PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@CommDetailID", SqlDbType.BigInt)
            _sqlparam.Value = _CommDetailId
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@FlagType", SqlDbType.Int)
            _sqlparam.Value = _FlagType
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing



            _sqlda.Fill(dt)


            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
            Return Nothing
        Finally
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
            If IsNothing(_sqlconn) = False Then
                _sqlconn.Dispose()
                _sqlconn = Nothing
            End If

        End Try

    End Function
    Private Function DeleteMessageFunctionality(ByVal _FlagType As Integer) As Boolean
        Dim _sqlconn As SqlConnection = New SqlConnection(_ConnectionString)
        Dim _sqlcmd As SqlCommand
        Dim _sqlda As SqlDataAdapter

        Dim _sqlparam As SqlParameter
        Try

            _sqlcmd = New SqlCommand("Intuit_ReadSecureMessage", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure
            _sqlda = New SqlDataAdapter(_sqlcmd)
            ''
            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = _PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@CommDetailID", SqlDbType.BigInt)
            _sqlparam.Value = _CommDetailId
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@FlagType", SqlDbType.Int)
            _sqlparam.Value = _FlagType
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing
            _sqlconn.Open()
            _sqlcmd.ExecuteNonQuery()
            _sqlconn.Close()



        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
            Return Nothing
        Finally

            If IsNothing(_sqlconn) = False Then
                _sqlconn.Dispose()
                _sqlconn = Nothing
            End If

        End Try

    End Function
    Private Sub AddControls()
        Wfh.Child.Dock = DockStyle.Top
        TryCast(Wfh.Child, gloUC_PatientStrip).ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.Intuit)
        Wfh.Child.BringToFront()
        TryCast(Wfh.Child, gloUC_PatientStrip).DTPValue = Format(Date.Now, "MM/dd/yyyy")

        TryCast(Wfh.Child, gloUC_PatientStrip).DTPFormat = DateTimePickerFormat.Short
        TryCast(Wfh.Child, gloUC_PatientStrip).DTPEnabled = False
        RemoveHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
        AddHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
        PatientStrip.Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
        ht = PatientStrip.Height

        RaiseEvent GetFormHeight()
    End Sub
    Private Sub FillListAttachment()
        ' Dim colAttachment As Int16
        If IsNothing(dtMessage) = False Then
            If dtMessage.Rows.Count > 0 Then

            
        For colAttachment As Int16 = COL_Attachment1 To COL_Attachment3

            If dtMessage.Rows(0)(colAttachment) <> "" Then

               
                Dim ostackMain As StackPanel, ostackInner As StackPanel
                Dim olbl As TextBlock
                Dim oimgAckw As Image

            
                Dim obrd As Border
                Dim selectedIndex As Int16 = 0

                obrd = New Border()

                obrd.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                obrd.BorderThickness = New Thickness(1)
                obrd.CornerRadius = New CornerRadius(6)
                obrd.BorderBrush = Brushes.Black


                RemoveHandler obrd.MouseEnter, AddressOf obrd_MouseEnter
                AddHandler obrd.MouseEnter, AddressOf obrd_MouseEnter

                RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
                AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged


                ostackMain = New StackPanel()

                ostackMain.Orientation = Orientation.Vertical
                ostackMain.Margin = New Thickness(2)
                ostackMain.CanVerticallyScroll = True
                ostackInner = New StackPanel()
                ostackInner.Orientation = Orientation.Horizontal
                ostackInner.Margin = New Thickness(1)
                ostackInner.CanVerticallyScroll = True
                '' ostackInner.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                oimgAckw = New Image()
                oimgAckw.Margin = New Thickness(5, 0, 5, 0)
                oimgAckw.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Forward Email.png", UriKind.Relative))
                oimgAckw.Height = 20
                oimgAckw.Width = 20
                oimgAckw.Stretch = Stretch.Fill
                oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
                ostackInner.Children.Add(oimgAckw)

                'OrderName
                olbl = New TextBlock()
                        olbl.Margin = New Thickness(5, 2, 5, 0)
                olbl.TextWrapping = TextWrapping.Wrap
                olbl.Width = 215
                olbl.Foreground = Brushes.Black
                        olbl.FontFamily = New FontFamily("Calibri")

                        olbl.FontSize = 12
                If colAttachment = COL_Attachment1 Then
                    olbl.Text = dtMessage.Rows(0)(colAttachment)
                            olbl.Tag = dtMessage.Rows(0)(COL_IAttachment1)
                ElseIf colAttachment = COL_Attachment2 Then
                    olbl.Text = dtMessage.Rows(0)(colAttachment)
                            olbl.Tag = dtMessage.Rows(0)(COL_IAttachment2)
                ElseIf colAttachment = COL_Attachment3 Then
                    olbl.Text = dtMessage.Rows(0)(colAttachment)
                            olbl.Tag = dtMessage.Rows(0)(COL_IAttachment3)

                End If
                
                olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
                ostackInner.Children.Add(olbl)

                ostackMain.Children.Add(ostackInner)

                obrd.Child = ostackMain

                lstAttachment.ScrollIntoView(lstAttachment)
                lstAttachment.Items.Add(obrd)



            End If
        Next

       
        lstAttachment.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub ControlSizeChanged()
        Try
            TryCast(Wfh.Child, gloUC_PatientStrip).Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
            PatientStrip.Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height

            ht = PatientStrip.Height
            RaiseEvent SetFormHeight()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub obrdinner_Mouseenter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Try


            Dim obd As Border = DirectCast(sender, Border)
            Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
            Storyboard.SetTarget(sb, obd)
            sb.Begin()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub obrd_MouseEnter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim obd As Border = DirectCast(sender, Border)
        Dim sb As New Storyboard
        Try
            sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
            Storyboard.SetTarget(sb, obd)
            sb.Begin()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
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
                If oslectedBorder IsNot Nothing Then
                    oslectedBorder.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                End If
                If IsNothing(lstAttachment.SelectedItem) = False Then
                    Dim oitem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                    oitem.Style = DirectCast(FindResource("BorderSelectedStyle"), Style)
                    oslectedBorder = oitem
                End If               
            End If

        Catch exc As Exception
            MessageBox.Show(exc.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK)
        End Try
    End Sub

   
  
   

    Public Sub New(ByVal PatientID As Int64, ByVal CommDetailID As Int64, ByVal ConnectionString As String, ByVal StartUpPath As String, ByVal TempFolder As String)

        ' This call is required by the designer.
        InitializeComponent()
        _PatientID = PatientID
        _CommDetailId = CommDetailID
        _ConnectionString = ConnectionString
        _StartUpPath = StartUpPath
        _TempFolder = TempFolder
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub lstAttachment_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lstAttachment.MouseDoubleClick

        Dim AttachmentData() As Byte
        Dim strFileName As String
        '  WaitCursor.Visibility = Windows.Visibility.Visible
        Try

            '  WaitCursor.Visibility = Windows.Visibility.Visible
        Me.Cursor = System.Windows.Input.Cursors.Wait
        If IsNothing(lstAttachment.SelectedItem) = False Then


                Dim oSelectedItem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                Dim oStackPnl As StackPanel = oSelectedItem.Child
                '''''''''

                Dim oinnerpnl As StackPanel = oStackPnl.Children(0)
                Dim oSelectedText As TextBlock = oinnerpnl.Children(1)
                Dim _Extension() As String
                Dim _FileExt As String = ""
                'oSelectedText.Text = "Ma.yuri.docx"
                _Extension = oSelectedText.Text.Split(".")
                If _Extension.Length > 1 Then
                    _FileExt = _Extension(_Extension.Length - 1)
                End If

                AttachmentData = oSelectedText.Tag
                If IsDBNull(AttachmentData) = False Then
                    strFileName = ExamNewFaxFileName(_StartUpPath & _TempFolder, "." & _FileExt)
                    '' generate Physical file
                    strFileName = GenerateFile(AttachmentData, strFileName)

                    'Dim newProc As Diagnostics.Process
                    'newProc = Diagnostics.Process.Start(strFileName)
                    Dim startInfo As System.Diagnostics.ProcessStartInfo
                    Dim pStart As New System.Diagnostics.Process

                    startInfo = New System.Diagnostics.ProcessStartInfo(strFileName)
                    'startInfo.Arguments = "Some Args"
                    startInfo.UseShellExecute = True
                    startInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
                    startInfo.CreateNoWindow = False

                    Process.Start(startInfo)



                End If




        End If
        Catch ex As Exception
        Finally

            Me.Cursor = System.Windows.Input.Cursors.Arrow
            WaitCursor.Visibility = Windows.Visibility.Hidden
        End Try
        ' Create a file and write the byte data to a file.

        '   Me.Cursor = System.Windows.Input.Cursors.Arrow

    End Sub

    Public ReadOnly Property ExamNewFaxFileName(ByVal _path As String, ByVal extension As String) As String
        Get
            Dim _NewDocumentName As String = ""
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            Dim i As Integer = 0
            _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & extension
            While File.Exists(_path & _NewDocumentName) = True
                i = i + 1
                _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & extension
            End While
            Return _path & _NewDocumentName
        End Get
    End Property





    Private Function GenerateFile(ByVal oResult() As Byte, ByVal strFileName As String) As String
        Try
            If IsNothing(oResult) = False Then

                Dim content As Byte() = CType(oResult, Byte())
                Dim stream As MemoryStream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                stream.WriteTo(oFile)
                oFile.Close()
                Return strFileName
            Else
                Return Nothing
            End If
           
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Function

    Private Sub btnMarkUnread_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnMarkUnread.Click
        ReadMessageFunctionality(1)
        RaiseEvent CloseIntuitForm()
    End Sub

    Private Sub btnReply_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnReply.Click
        WaitCursor.Visibility = Windows.Visibility.Visible
    End Sub



  
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDelete.Click


        Dim message As String = ""
        message = Environment.NewLine + "From : " + txtFrom.Text.ToString().Trim
        message = message + Environment.NewLine

        message = message + "Subject : " + txtSubject.Text.ToString().Trim
        message = message + Environment.NewLine
        message = message + "Date : " + dtpDate.ToString().Trim
        Dim dResult As DialogResult = MessageBox.Show("Are you sure you want to delete this secure message?" + message, "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dResult.ToString() = "Yes" Then
            DeleteMessageFunctionality(2)
            RaiseEvent CloseIntuitForm()
        End If


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        RaiseEvent CloseIntuitForm()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnNew.Click
        RaiseEvent OpenSendSecureMsgForm()
    End Sub

    Private Sub btnCreateTask_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnCreateTask.Click

    End Sub
End Class
