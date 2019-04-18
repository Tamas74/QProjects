Imports System.Windows.Forms.Integration
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows
Imports System.Windows.Controls
Imports System.IO
Imports gloCCDLibrary
Imports gloSettings

Public Class UC_SendIntuitSecureMsg

    Public Event SetFormHeight()
    Public Event GetFormHeight()

    'used to close the frmIntuitSecureMessage form
    Public Event CloseIntuitForm()


    Public ht As Integer
    Dim oslectedBorder As Border

    Private PatientID As Int64
    Private ConnectionString As String
    Private ClientMachineID As String
    Private ClientMachineName As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal nPatientID As Int64, sConnectionString As String, sClientMachineID As String, sClientMachineName As String)
        InitializeComponent()
        PatientID = nPatientID
        ConnectionString = sConnectionString
        ClientMachineID = sClientMachineID
        ClientMachineName = sClientMachineName
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Wfh.Child.Dock = DockStyle.Top

        TryCast(Wfh.Child, gloUC_PatientStrip).ShowDetail(PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.History)
        Wfh.Child.BringToFront()
        TryCast(Wfh.Child, gloUC_PatientStrip).DTPValue = Format(Date.Now, "MM/dd/yyyy")

        TryCast(Wfh.Child, gloUC_PatientStrip).DTPFormat = DateTimePickerFormat.Short
        TryCast(Wfh.Child, gloUC_PatientStrip).DTPEnabled = False
        '' RemoveHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
        AddHandler TryCast(Wfh.Child, gloUC_PatientStrip).ControlSizeChanged, AddressOf ControlSizeChanged
        PatientStrip.Height = TryCast(Wfh.Child, gloUC_PatientStrip).Height
        ht = PatientStrip.Height

        'lstAttachment.Items.Add("Attachment 1")
        RaiseEvent GetFormHeight()
        WaitCursor.Visibility = Windows.Visibility.Hidden




        'For i As Integer = 0 To 2

        '    Dim ostackMain As StackPanel, ostackInner As StackPanel
        '    Dim olbl As TextBlock
        '    Dim oimgAckw As Image


        '    Dim obrd As Border
        '    Dim selectedIndex As Int16 = 0

        '    obrd = New Border()

        '    obrd.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
        '    obrd.BorderThickness = New Thickness(1)
        '    obrd.CornerRadius = New CornerRadius(6)
        '    obrd.BorderBrush = Brushes.Black

        '    RemoveHandler obrd.MouseEnter, AddressOf obrd_MouseEnter
        '    AddHandler obrd.MouseEnter, AddressOf obrd_MouseEnter

        '    RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
        '    AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged

        '    ostackMain = New StackPanel()
        '    ostackMain.Orientation = Orientation.Vertical
        '    ostackMain.Margin = New Thickness(2)
        '    ostackMain.CanVerticallyScroll = True
        '    ostackInner = New StackPanel()
        '    ostackInner.Orientation = Orientation.Horizontal
        '    ostackInner.Margin = New Thickness(1)
        '    ostackInner.CanVerticallyScroll = True
        '    '' ostackInner.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
        '    oimgAckw = New Image()
        '    oimgAckw.Margin = New Thickness(5, 0, 5, 0)
        '    oimgAckw.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Forward Email.png", UriKind.Relative))
        '    oimgAckw.Height = 20
        '    oimgAckw.Width = 20
        '    oimgAckw.Stretch = Stretch.Fill
        '    oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
        '    ostackInner.Children.Add(oimgAckw)

        '    'OrderName
        '    olbl = New TextBlock()
        '    olbl.Margin = New Thickness(2, 0, 0, 0)
        '    olbl.TextWrapping = TextWrapping.Wrap
        '    olbl.Width = 215
        '    olbl.Foreground = Brushes.Black
        '    olbl.FontFamily = New FontFamily("Tahoma")

        '    olbl.Text = "Attachment 1"
        '    olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
        '    ostackInner.Children.Add(olbl)

        '    ostackMain.Children.Add(ostackInner)

        '    obrd.Child = ostackMain

        '    lstAttachment.ScrollIntoView(lstAttachment)
        '    lstAttachment.Items.Add(obrd)

        '    lstAttachment.SelectedIndex = 0

        'Next



        SetContolEnableDisable()

        '  GIFCtrl.Visibility = Windows.Visibility.Hidden
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

    Private Sub btnAttachment_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAttachment.Click

        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFileName As String = ""
        Dim strFilePath As String = ""

        Dim ogloInterface As New gloCCDInterface

        fd.Title = "Open File Dialog"
        fd.InitialDirectory = "C:\"
        fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True
        fd.Multiselect = False
        If fd.ShowDialog() = DialogResult.OK Then
            strFilePath = fd.FileName
            strFileName = fd.SafeFileName
        End If

        AddListBoxItem(strFileName, strFilePath)

        Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)

        Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
        Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
        Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
        Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
        TBlock.Tag = arrByte
    End Sub

    Private Sub btnExamCCD_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnExamCCD.Click
        WaitCursor.Visibility = Windows.Visibility.Visible
        Dim strCCDSection As String = String.Empty
        Dim strFilePath As String
        Dim strCCDfilePath As String
        Dim ogloInterface As New gloCCDInterface

        Try
            strCCDSection = CheckVisitCCDSection()
            strCCDfilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath.ToString()

            If Directory.Exists(strCCDfilePath) = True Then
                strCCDfilePath = strCCDfilePath + "Visit Summary " & Format(dtExamCCD.SelectedDate, "MM-dd-yyyy") & ".xml"
                strFilePath = ogloInterface.GenerateClinicalInformation(PatientID, 1, strCCDSection, 0, dtExamCCD.SelectedDate, dtExamCCD.SelectedDate, strCCDfilePath)

                AddListBoxItem("Visit Summary " & Format(Date.Today, "MM-dd-yyyy") & ".xml", strCCDfilePath)

                Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)

                Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
                Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
                TBlock.Tag = arrByte
            Else
                MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            strCCDSection = Nothing
            strFilePath = Nothing
            strCCDfilePath = Nothing
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
            WaitCursor.Visibility = Windows.Visibility.Hidden
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

                Dim oitem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                oitem.Style = DirectCast(FindResource("BorderSelectedStyle"), Style)
                oslectedBorder = oitem
            End If
        Catch exc As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), False)
        End Try
    End Sub

    Private Sub btnSend_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSend.Click
        'MessageBox.Show(lstAttachment.Items.CurrentItem.tag)
        'Dim obd As Border = DirectCast(lstAttachment.Items.CurrentItem, Border)
        'Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
        'Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
        'Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
        'MessageBox.Show(TBlock.Tag)

        'Dim arrByte As Byte()
        'Dim encoding As New System.Text.UTF8Encoding()
        'arrByte = encoding.GetBytes(TBlock.Tag)

        Dim dtIntuitCommAttachments As DataTable
        dtIntuitCommAttachments = New DataTable("TVP_IntuitCommAttachments")

        Dim dtGl_Messagequeue As DataTable
        dtGl_Messagequeue = New DataTable("TVP_Gl_Messagequeue")

        Dim dtIntuitCommDetails As DataTable
        dtIntuitCommDetails = New DataTable("TVP_IntuitCommDetails")

        Dim MessageID As Int64 = 0
        Try
            If txtSubject.Text = "" Then
                txtSubject.Focus()
                MessageBox.Show("Please enter subject.     ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Master Entry of Message
            dtGl_Messagequeue = GetGl_Messagequeue()

            'Get Intuit Message Details
            dtIntuitCommDetails = GetIntuitCommDetails()

            'Get Intuit Attachment Record
            dtIntuitCommAttachments = GetIntuitCommAttachments()

            MessageID = SaveMessage(dtGl_Messagequeue, dtIntuitCommDetails, dtIntuitCommAttachments)

            If MessageID > 0 Then
                MessageBox.Show("Record saved successfully.      ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RaiseEvent CloseIntuitForm()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            dtGl_Messagequeue.Dispose()
            dtGl_Messagequeue = Nothing
            dtIntuitCommDetails.Dispose()
            dtIntuitCommDetails = Nothing
            MessageID = Nothing
        End Try
    End Sub

    Private Function GetIntuitCommAttachments()
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommAttachments")

        'declaring one row for the table
        Dim Row1 As DataRow

        Try

            'declare a column for first attachment name
            Dim sCommAttachOneName As DataColumn = New DataColumn("sCommAttachOneName")
            sCommAttachOneName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachOneName)


            'declare a column for second attachment name
            Dim sCommAttachTwoName As DataColumn = New DataColumn("sCommAttachTwoName")
            sCommAttachTwoName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachTwoName)


            'declare a column for third attachment name 
            Dim sCommAttachThreeName As DataColumn = New DataColumn("sCommAttachThreeName")
            sCommAttachThreeName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachThreeName)


            'declare a column for first attachment
            Dim ICommAttachOne As DataColumn = New DataColumn("ICommAttachOne")
            ICommAttachOne.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachOne)


            'declare a column for second attachment
            Dim ICommAttachTwo As DataColumn = New DataColumn("ICommAttachTwo")
            ICommAttachTwo.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachTwo)


            'declare a column for third attachment
            Dim ICommAttachThree As DataColumn = New DataColumn("ICommAttachThree")
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
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            dt.Dispose()
            dt = Nothing
            Row1 = Nothing
        End Try
    End Function

    Private Function GetAttachmentFileName(i As Integer) As String
        Dim obd As Border = DirectCast(lstAttachment.Items.Item(i), Border)
        Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
        Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
        Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
        Return TBlock.Text
    End Function

    Private Function GetAttachmentFile(i As Integer) As Byte()
        Dim obd As Border = DirectCast(lstAttachment.Items.Item(i), Border)
        Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
        Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
        Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)

        Dim arrByte As Byte()

        Dim encoding As New System.Text.UTF8Encoding()
        arrByte = encoding.GetBytes(TBlock.Tag)
        Return arrByte
    End Function


    Private Function GetGl_Messagequeue() As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_Gl_Messagequeue")

        'declaring one row for the table
        Dim Row1 As DataRow

        Try
            'declare a column 
            Dim contactID As DataColumn = New DataColumn("sMachineID")
            contactID.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(contactID)

            Dim fname As DataColumn = New DataColumn("sMachineName")
            fname.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(fname)

            Dim lname As DataColumn = New DataColumn("nPatientID")
            lname.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(lname)

            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sMachineID") = ClientMachineID
            Row1.Item("sMachineName") = ClientMachineName
            Row1.Item("nPatientID") = PatientID

            'adding the completed row to the table
            dt.Rows.Add(Row1)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            dt.Dispose()
            dt = Nothing
            Row1 = Nothing
        End Try
    End Function

    Private Function GetIntuitCommDetails() As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommDetails")

        'declaring one row for the table
        Dim Row1 As DataRow

        Try
            'declare a column 
            Dim sCommSubject As DataColumn = New DataColumn("sCommSubject")
            sCommSubject.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommSubject)

            Dim sCommMessage As DataColumn = New DataColumn("sCommMessage")
            sCommMessage.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommMessage)

            Dim bCommMemberReply As DataColumn = New DataColumn("bCommMemberReply")
            bCommMemberReply.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bCommMemberReply)

            Dim nPatientID As DataColumn = New DataColumn("nPatientID")
            nPatientID.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(nPatientID)


            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sCommSubject") = txtSubject.Text
            Row1.Item("sCommMessage") = txtMessage.Text
            Row1.Item("bCommMemberReply") = chkAllowtoReply.IsChecked
            Row1.Item("nPatientID") = PatientID

            'adding the completed row to the table
            dt.Rows.Add(Row1)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            dt.Dispose()
            dt = Nothing
            Row1 = Nothing
        End Try
    End Function

    Private Function SaveMessage(Gl_Messagequeue As DataTable, IntuitCommDetails As DataTable, IntuitCommAttachments As DataTable) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim MessageID As Int64 = 0
        Try
            oDB.Connect(False)

            oDBParameters.Add("@TVP_Gl_Messagequeue", Gl_Messagequeue, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_IntuitCommDetails", IntuitCommDetails, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_int_IntuitCommAttachments", IntuitCommAttachments, ParameterDirection.Input, SqlDbType.Structured)

            oDBParameters.Add("@nMessageID", 0, ParameterDirection.Output, SqlDbType.Decimal)
            oDB.Execute("Intuit_SaveMessage", oDBParameters, MessageID)
            oDB.Disconnect()

            oDBParameters.Dispose()
            oDBParameters = Nothing

            oDB.Dispose()
            oDB = Nothing

            Return MessageID

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function


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
            MessageBox.Show(ex.Message)
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
            MessageBox.Show(ex.Message)
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

    Private Sub btnPtCCD_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnPtCCD.Click
        WaitCursor.Visibility = Windows.Visibility.Visible
        Dim strCCDSection As String = String.Empty
        Dim strFilePath As String
        Dim strCCDfilePath As String
        Dim ogloInterface As New gloCCDInterface

        Try
            strCCDSection = CheckPatientCCDSections()
            strCCDfilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath.ToString()

            If Directory.Exists(strCCDfilePath) = True Then

                strCCDfilePath = strCCDfilePath + "Patient Record " & Format(Date.Today, "MM-dd-yyyy") & ".xml"

                AddListBoxItem("Patient Record " & Format(Date.Today, "MM-dd-yyyy") & ".xml", strCCDfilePath)

                strFilePath = ogloInterface.GenerateClinicalInformation(PatientID, 1, strCCDSection, 0, dtFromPtCCD.SelectedDate, dtToPtCCD.SelectedDate, strCCDfilePath)

                Dim arrByte As Byte() = ogloInterface.ConvertFiletoBinary(strFilePath)

                Dim obd As Border = DirectCast(lstAttachment.Items.Item(lstAttachment.Items.Count - 1), Border)
                Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)
                TBlock.Tag = arrByte
                '   MessageBox.Show(strFilePath)

            Else
                MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            strCCDSection = Nothing
            strFilePath = Nothing
            strCCDfilePath = Nothing
            If Not IsNothing(ogloInterface) Then
                ogloInterface.Dispose()
                ogloInterface = Nothing
            End If
            WaitCursor.Visibility = Windows.Visibility.Hidden
        End Try
    End Sub

    Private Sub btnDMS_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDMS.Click
        Try


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SetContolEnableDisable()
        dtExamCCD.IsEnabled = False
        dtFromPtCCD.IsEnabled = False
        dtToPtCCD.IsEnabled = False
        chkPerPtRequest.IsEnabled = False
        btnExamCCD.IsEnabled = False
        btnPtCCD.IsEnabled = False
    End Sub

    Private Sub chkvisitSummary_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles chkvisitSummary.Click
        If chkvisitSummary.IsChecked Then
            dtExamCCD.IsEnabled = True
            btnExamCCD.IsEnabled = True
        Else
            dtExamCCD.IsEnabled = False
            btnExamCCD.IsEnabled = False
        End If
    End Sub

    Private Sub chkPtRecord_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles chkPtRecord.Click
        If chkPtRecord.IsChecked Then
            dtFromPtCCD.IsEnabled = True
            dtToPtCCD.IsEnabled = True
            chkPerPtRequest.IsEnabled = True
            btnPtCCD.IsEnabled = True
        Else
            dtFromPtCCD.IsEnabled = False
            dtToPtCCD.IsEnabled = False
            chkPerPtRequest.IsEnabled = False
            btnPtCCD.IsEnabled = False
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        RaiseEvent CloseIntuitForm()
    End Sub

    Private Sub AddListBoxItem(FileName As String, FilePath As String)

        If lstAttachment.Items.Count = 0 Or lstAttachment.Items.Count < 3 Then

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

            'set filepath
            obrd.Tag = FilePath

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


            oimgAckw = New Image()
            oimgAckw.Margin = New Thickness(5, 0, 5, 0)
            oimgAckw.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Forward Email.png", UriKind.Relative))
            oimgAckw.Height = 20
            oimgAckw.Width = 20
            oimgAckw.Stretch = Stretch.Fill
            oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
            ostackInner.Children.Add(oimgAckw)

            olbl = New TextBlock()
            olbl.Margin = New Thickness(2, 0, 0, 0)
            olbl.TextWrapping = TextWrapping.Wrap
            olbl.Width = 215
            olbl.Foreground = Brushes.Black
            olbl.FontFamily = New FontFamily("Tahoma")

            'set filename
            olbl.Text = FileName

            olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
            ostackInner.Children.Add(olbl)

            ostackMain.Children.Add(ostackInner)

            obrd.Child = ostackMain

            lstAttachment.ScrollIntoView(lstAttachment)
            lstAttachment.Items.Add(obrd)

            lstAttachment.SelectedIndex = 0
        Else
            MessageBox.Show("You can attach only three items.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub


    Private Sub lstAttachment_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lstAttachment.MouseDoubleClick

        Dim AttachmentData As Byte()
        Dim strFileName As String = ""
        Try
            Me.Cursor = System.Windows.Input.Cursors.Wait
            If IsNothing(lstAttachment.SelectedItem) = False Then

                Dim obd As Border = DirectCast(lstAttachment.SelectedItem, Border)
                Dim stac As StackPanel = obd.Child
                Dim Innerstac As StackPanel = stac.Children(0)
                Dim TBlock As TextBlock = Innerstac.Children(1)


                'Dim obd As Border = DirectCast(lstAttachment.Items.Item(i), Border)
                'Dim stac As StackPanel = DirectCast(obd.Child, StackPanel)
                'Dim Innerstac As StackPanel = DirectCast(stac.Children(0), StackPanel)
                'Dim TBlock As TextBlock = DirectCast(Innerstac.Children(1), TextBlock)


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
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = System.Windows.Input.Cursors.Arrow
            WaitCursor.Visibility = Windows.Visibility.Hidden
        End Try

    End Sub

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

   
End Class
