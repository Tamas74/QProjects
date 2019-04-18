Imports gloEMRGeneralLibrary
Imports System.Windows.Controls
Imports System.Windows
Imports System.Windows.Media.Animation
Imports System.Windows.Media


Public Class LabUC_ResultSet

    Public Delegate Sub OpenDMS(ByVal TestID As Int64, ByVal DMSId As Int64)
    Public Event OpenDocument As OpenDMS

    Public Delegate Sub OpenDicom(ByVal PatID As Int64, ByVal DocID As Int64)
    Public Event OpenDicomFile As OpenDicom


    Public Delegate Sub ShowAckButton(ByVal Show As Boolean)
    Public Event ShowAck As ShowAckButton

    Public PatientID As Int64
    Public DMSID As Int64
    Public OrderID As Int64 = 0
    Public TestID As Int64 = 0
    Public _SelectedOrderID As Int64 = 0
    Public _TaskRelatedOrderID As Int64 = 0
    Private _InternalSelectedOrderID As Int64 = 0
    Private _dtLaburl As DataTable = Nothing  ''added for url document
    Public _SelectedOrderDate As String = ""
    Public _IsLoading As Boolean = False
    ' Dim frmLabURL As frmLabURLdocument
    Dim oslectedBorder As Border
    Dim ControlWidth As Int16 = 1260
    Dim WithEvents alink As System.Windows.Documents.Hyperlink = Nothing 'added by manoj on 20121127 for adding result value as hyperlinks

    Public Delegate Sub ContextAcknowledge()
    Public Event Acknowlegeclick As ContextAcknowledge
    Public Event AcknowlegeNormalclick As ContextAcknowledge
    Public Event MarkUnAcknowlegeclick As ContextAcknowledge

    Public Delegate Sub contextmnuOpen(sender As System.Object, e As System.Windows.Controls.ContextMenuEventArgs)
    Public Event customMenuOpening As contextmnuOpen

    Public Delegate Sub CnxtMnuResultSet()
    Public Event EvtCnxtMnuResult_SNOMED As CnxtMnuResultSet
    Public Event EvtCnxtMnuResult_ICD As CnxtMnuResultSet
    Public Event EvtCnxtMnuResult_LOINC As CnxtMnuResultSet

    Public Delegate Sub Codification(ByVal nOrderId As Int64, ByVal nTestId As Int64, ByVal sResultName As String, ByVal sCodeType As String)
    Public Event ViewCodification As Codification
    Public Event RemoveCodification As Codification

    Private _OrderIdForCode As Int64
    Private _DefaultColor = New BrushConverter().ConvertFromString("#FF1F497D")

    Private _LabResulSetCommentFontName As String
    Public Property LabResultSetCommentFontName() As String
        Get
            Return _LabResulSetCommentFontName
        End Get
        Set(ByVal value As String)
            _LabResulSetCommentFontName = value
        End Set
    End Property

    Private _LabResulSetCommentFontSize As Double
    Public Property LabResulSetCommentFontSize() As Double
        Get
            Return _LabResulSetCommentFontSize
        End Get
        Set(ByVal value As Double)
            _LabResulSetCommentFontSize = value
        End Set
    End Property

    Public Property OrderIdForCode() As Int64
        Get
            Return _OrderIdForCode
        End Get
        Set(ByVal value As Int64)
            _OrderIdForCode = value
        End Set
    End Property

    Private _TestIdForCode As Int64
    Public Property TestIdForCode() As Int64
        Get
            Return _TestIdForCode
        End Get
        Set(ByVal value As Int64)
            _TestIdForCode = value
        End Set
    End Property

    Private _ResultNameForCode As String
    Public Property ResultNameForCode() As String
        Get
            Return _ResultNameForCode
        End Get
        Set(ByVal value As String)
            _ResultNameForCode = value
        End Set
    End Property

    Private _SnomedCode As String = String.Empty
    Public Property SnomedCode() As String
        Get
            Return _SnomedCode
        End Get
        Set(ByVal value As String)
            _SnomedCode = value
        End Set
    End Property

    Private _ICDCode As String = String.Empty
    Public Property ICDCode() As String
        Get
            Return _ICDCode
        End Get
        Set(ByVal value As String)
            _ICDCode = value
        End Set
    End Property

    Private _LoincCode As String = String.Empty
    Public Property LoincCode() As String
        Get
            Return _LoincCode
        End Get
        Set(ByVal value As String)
            _LoincCode = value
        End Set
    End Property

    Private _ICDType As String
    Public Property ICDType() As String
        Get
            Return _ICDType
        End Get
        Set(ByVal value As String)
            _ICDType = value
        End Set
    End Property

    Public Sub New()

        InitializeComponent()

    End Sub
    Public Sub New(ByVal PatID As Int64, ByVal DMSId As Int64)

        InitializeComponent()

        PatientID = PatID
        DMSId = DMSId

    End Sub

    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Try

            Dim omenu As MenuItem
            omenu = DirectCast(sender, MenuItem)
            Dim DMSID As Int64 = Convert.ToInt64(omenu.Tag)

            If DMSID > 0 Then
                RaiseEvent OpenDocument(TestID, DMSID)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Sub UserControl_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        rbtnAll.IsChecked = "True"

        Try
            _IsLoading = True
            dtpFrom.IsEnabled = False
            dtpTo.IsEnabled = False
            If Not _SelectedOrderDate = "" Then
                dtpFrom.Text = _SelectedOrderDate
                _SelectedOrderDate = ""
            Else
                'Developer:Sanjog Dhamke
                'Date:2 March 2012
                'PRD Name: MWC 7.0 Feedback on Labs
                'Reason: From date should be 9 month before of the current date.
                dtpFrom.Text = Now.Date.AddMonths(-9)
            End If
            dtpTo.Text = Now.Date
            _IsLoading = False
            ShowTestData()
            'Add this condition to show Order Specific records only once after any filter is changed grid will show all data

            If _SelectedOrderID > 0 Then
                _SelectedOrderID = 0
            End If
        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), False)
        End Try
    End Sub

    Public Sub ShowTestData()
        Try
            If _IsLoading = False Then
                If dtpFrom.Text = "" Or dtpTo.Text = "" Then
                    Return
                Else
                    lstLabTest.DataContext = Nothing
                    Dim dt As DataTable
                    dt = GetLabTestData(PatientID)

                    ''added for url document functionality
                    Dim strOrderid As String = ""
                    For Each dr As DataRow In dt.Rows
                        strOrderid = strOrderid & dr("OrderID").ToString() & ","
                    Next
                    If (strOrderid.Length > 0) Then
                        strOrderid = strOrderid.Substring(0, strOrderid.Length - 1)
                    End If
                    If Not IsNothing(_dtLaburl) Then
                        _dtLaburl.Dispose()
                        _dtLaburl = Nothing
                    End If
                    If strOrderid.Length > 0 Then
                        _dtLaburl = Lab_GetURLDocument(strOrderid) '' function added for url document
                    End If
                    CreateListBoxItem(dt)

                    'If dt.Rows.Count = 0 Then
                    '    ClearField()
                    '    RaiseEvent ShowAck(False)
                    '    mydatagrid.Visibility = Windows.Visibility.Collapsed
                    'Else
                    '    RaiseEvent ShowAck(True)
                    '    mydatagrid.Visibility = Windows.Visibility.Visible
                    'End If

                    If dt.Rows.Count = 0 Then
                        ClearField()
                        CreateHeader()
                    End If

                    RaiseEvent ShowAck(True)
                    ShowAcknowledgeDetails(OrderID)

                    dt.Dispose()
                    dt = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Function GetLabTestData(ByVal PatientId As Int64) As DataTable

        Dim ds As DataSet = Nothing
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = PatientId
                oPara.Name = "@PatientId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = dtpFrom.Text
                oPara.Name = "@FromDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.DateTime
                oPara.Direction = ParameterDirection.Input
                oPara.Value = dtpTo.Text
                oPara.Name = "@ToDate"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Int
                oPara.Direction = ParameterDirection.Input

                If rbtnAll.IsChecked = True Then
                    oPara.Value = 2
                ElseIf rbtnAcknowledged.IsChecked = True Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@IsAcknowledgement"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Bit
                oPara.Direction = ParameterDirection.Input

                If chkDate.IsChecked = True Then
                    oPara.Value = 1
                Else
                    oPara.Value = 0
                End If
                oPara.Name = "@IsDateSelected"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                ds = .GetDataSet("Lab_OrderTests")

            End With
            Return ds.Tables(0).Copy()
            ' ds = Nothing
        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), True)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function

    ''function added for url document
    Private Function Lab_GetURLDocument(ByVal OrderID As String) As DataTable

        Dim dtURLdocument As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Try
            '' //// <> Fill Order Test Result Object 

            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = 0
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = 0
                oPara.Name = "@TestID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.VarChar
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@ListofOrderID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                dtURLdocument = .GetDataTable("Lab_GetURLDocument")

            End With
            Return dtURLdocument

        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), True)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If

        End Try

    End Function



    Private Sub ShowtestDeatails(ByVal patID As Int64, ByVal OrderID As Int64, ByVal testID As Int64)
        Dim dt As DataTable = Nothing
        Try
            lstTestDetails.DataContext = Nothing
            dt = GetLabTestDetails(patID, OrderID, testID)

            OrderIdForCode = OrderID


            CreateTestListBoxItem(dt)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                TestIdForCode = Convert.ToInt64(dt.Rows(0)("TestID"))

                'Show Other details 
                lblOrderDateVal.Content = Convert.ToString(dt.Rows(0)("OrderedDate"))
                lblReportedVal.Content = Convert.ToString(dt.Rows(0)("ReportedDate"))
                lblProviderVal.Content = Convert.ToString(dt.Rows(0)("ProviderName"))
                'lblPerformingVal.Content = Convert.ToString(dt.Rows(0)("LabName"))
                lblCollectedVal.Content = Convert.ToString(dt.Rows(0)("SpecimenCollectionDate"))
                lblSpecimenVal.Content = Convert.ToString(dt.Rows(0)("SpecimenName"))

                lblSourceVal.Text = Convert.ToString(dt.Rows(0)("SpecimenSource"))
                lblRequisitionVal.Text = Convert.ToString(dt.Rows(0)("Requisition"))
                lblFastingVal.Content = Convert.ToString(dt.Rows(0)("Fasting"))
                DesignResultNotesTexBlock(lblOrderCommentVal, Convert.ToString(dt.Rows(0)("OrderComments"))) '20140407 manoj PRD: View Lab Order Comments :added Order comments
                If Not Convert.ToString(dt.Rows(0)("FlagLEgend")) = "" Then
                    lblFlagLegend.Text = "*Flag Legend: " + Convert.ToString(dt.Rows(0)("FlagLEgend"))
                    lblFlagLegend.Visibility = Windows.Visibility.Visible
                Else
                    lblFlagLegend.Visibility = Windows.Visibility.Collapsed
                End If

            Else
                ClearField(False)
            End If
        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), True)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Sub

    Public Sub ClearField(Optional ByVal ClearID As Boolean = True)
        imgDMS.Visibility = Windows.Visibility.Collapsed
        imgDicom.Visibility = Windows.Visibility.Collapsed
        lstTestDetails.DataContext = Nothing
        lblOrderDateVal.Content = ""

        lblReportedVal.Content = ""
        lblProviderVal.Content = ""
        'lblPerformingVal.Content = ""
        lblCollectedVal.Content = ""
        lblSpecimenVal.Content = ""
        lblSourceVal.Text = ""
        lblRequisitionVal.Text = ""
        lblFlagLegend.Text = ""
        'lblTestNote.Text = ""
        lblFastingVal.Content = ""
        lblOrderCommentVal.Text = "" '20140407 manoj PRD: View Lab Order Comments :clear order comments

        If ClearID = True Then OrderID = 0
        TestID = 0
    End Sub

    Public Function GetLabTestDetails(ByVal patID As Int64, ByVal OrderID As Int64, ByVal TestID As Int64) As DataTable

        Dim ds As DataSet = Nothing
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer

        Try
            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = patID
                oPara.Name = "@PatientID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@OrderID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = TestID
                oPara.Name = "@TestID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                ds = .GetDataSet("LabUC_TestDetails")

            End With

            Return ds.Tables(0).Copy()
            ' ds = Nothing

        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), True)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function


    Private Sub lstLabTest_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) 'Handles lstLabTest.SelectionChanged
        Try
            If lstLabTest.Items.Count > 0 Then
                If oslectedBorder IsNot Nothing Then
                    ''Added for Orders that have tests pending results should have a clear indication that results are still pending. - White background on 20140305
                    If oslectedBorder.Child IsNot Nothing Then
                        Dim otxt As TextBlock
                        otxt = DirectCast(DirectCast(oslectedBorder.Child, System.Windows.Controls.StackPanel).Children(0), System.Windows.Controls.StackPanel).Children(0)
                        If otxt.Tag = "BackgroundColor" Then
                            oslectedBorder.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                        Else
                            oslectedBorder.Style = DirectCast(FindResource("WhiteBorderBackroundStyle"), Style)
                        End If
                    End If
                    ''End White background
                End If

                Dim oitem As Border = DirectCast(lstLabTest.SelectedItem, Border)
                oitem.Style = DirectCast(FindResource("BorderSelectedStyle"), Style)
                oslectedBorder = oitem
                OrderID = Convert.ToInt64(oitem.Tag)
                ShowtestDeatails(PatientID, OrderID, TestID)
                ShowAcknowledgeDetails(OrderID)
            End If

        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), False)
        End Try
    End Sub


    Private Sub Image1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles imgDMS.MouseDown
        Try
            MenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub cmbAckowledge_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)
        ShowTestData()
    End Sub

    Private Sub dtpFrom_SelectedDateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles dtpFrom.SelectedDateChanged
        ShowTestData()
    End Sub

    Private Sub dtpTo_SelectedDateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles dtpTo.SelectedDateChanged
        ShowTestData()
    End Sub

    Private Sub MenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Try

            Dim omenu As MenuItem
            omenu = DirectCast(sender, MenuItem)
            Dim DicomID As Int64 = Convert.ToInt64(omenu.Tag)
            If DicomID > 0 Then
                RaiseEvent OpenDicomFile(PatientID, DicomID)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Sub imgDicom_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles imgDicom.MouseDown
        Try
            MenuItem_Click_1(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub imgDMS_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles imgDMS.MouseEnter
        brdDMS.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub imgDicom_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles imgDicom.MouseEnter
        brdDicom.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub imgDicom_MouseLeave(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles imgDicom.MouseLeave
        brdDicom.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Sub imgDMS_MouseLeave(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles imgDMS.MouseLeave
        brdDMS.Visibility = Windows.Visibility.Collapsed
    End Sub
    Private Shared YesBitmapImage As System.Windows.Media.Imaging.BitmapImage = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Yes.png", UriKind.Relative))
    Private Shared Dicom01BitmapImage As System.Windows.Media.Imaging.BitmapImage = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Dicom01.ico", UriKind.Relative))
    Private Shared AttachmentBitmapImage As System.Windows.Media.Imaging.BitmapImage = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
    Private Shared DocumentBitmapImage As System.Windows.Media.Imaging.BitmapImage = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result Document.ico", UriKind.Relative))
    Private Shared URLBitmapImage As System.Windows.Media.Imaging.BitmapImage = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result URL Doc.ico", UriKind.Relative))
    Private Shared BulletBitmapImage As System.Windows.Media.Imaging.BitmapImage = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))

    Public Sub CreateListBoxItem(ByVal dt As DataTable)
        If lstLabTest.Items.Count > 0 Then
            Dim oitem As Border = DirectCast(lstLabTest.SelectedItem, Border)
            _InternalSelectedOrderID = Convert.ToInt64(oitem.Tag)
        End If

        lstLabTest.Items.Clear()
        lstTestDetails.Items.Clear()
        Try
            Dim odv As DataView

            Dim otxtblock As TextBlock
            Dim ostackMain As StackPanel, ostackInner As StackPanel
            Dim olbl As TextBlock
            Dim oimgAckw As Image
            Dim oimgDicom As Image
            Dim oimgDMS As Image
            Dim obrd As Border
            Dim selectedIndex As Int16 = 0
            Dim internalOrderID As Int64 = 0
            Dim subitempresent As Boolean = False
            Try
                RemoveHandler lstLabTest.SelectionChanged, AddressOf lstLabTest_SelectionChanged
            Catch ex As Exception

            End Try

            AddHandler lstLabTest.SelectionChanged, AddressOf lstLabTest_SelectionChanged

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If _SelectedOrderID > 0 Then
                    odv = dt.DefaultView
                    odv.RowFilter = "OrderID = " & Convert.ToString(_SelectedOrderID)

                    If odv.Count > 0 Then
                        obrd = New Border()
                        obrd.Tag = Convert.ToString(odv(0)("OrderID"))
                        'obrd.Background = SetLenearGradientColor();
                        If Convert.ToString(odv(0)("TestDateTime")).Trim() <> "" Then
                            obrd.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                        Else
                            obrd.Style = DirectCast(FindResource("WhiteBorderBackroundStyle"), Style)
                        End If

                        obrd.BorderThickness = New Thickness(1)
                        obrd.CornerRadius = New CornerRadius(6)
                        obrd.BorderBrush = Brushes.Black
                        AddHandler obrd.MouseEnter, AddressOf obrd_MouseEnter
                        'AddHandler obrd.MouseLeave, AddressOf obrd_MouseLeave

                        ostackMain = New StackPanel()
                        ostackMain.Orientation = Orientation.Vertical
                        ostackMain.Margin = New Thickness(2)

                        ostackInner = New StackPanel()
                        ostackInner.Orientation = Orientation.Horizontal
                        ostackInner.Margin = New Thickness(1)

                        'Test/OrderDate
                        olbl = New TextBlock()
                        olbl.Margin = New Thickness(2, 0, 0, 0)
                        olbl.TextWrapping = TextWrapping.Wrap
                        olbl.Width = 145
                        olbl.Text = Convert.ToString(odv(0)("TestDateTime"))
                        ostackInner.Children.Add(olbl)

                        'OrderName
                        olbl = New TextBlock()
                        olbl.Margin = New Thickness(2, 0, 0, 0)
                        olbl.TextWrapping = TextWrapping.Wrap
                        olbl.Width = 215
                        olbl.Text = Convert.ToString(odv(0)("OrderName"))
                        ostackInner.Children.Add(olbl)

                        ostackMain.Children.Add(ostackInner)

                        ostackInner = New StackPanel()
                        ostackInner.Margin = New Thickness(1)
                        ostackInner.Orientation = Orientation.Horizontal

                        'STAUS
                        olbl = New TextBlock()
                        olbl.TextWrapping = TextWrapping.Wrap
                        olbl.Width = 125
                        olbl.Text = Convert.ToString(odv(0)("STATUS"))
                        ostackInner.Children.Add(olbl)

                        'Acknowledgement
                        If Convert.ToString(odv(0)("AckwFlag")) = "Visible" Then
                            oimgAckw = New Image()
                            oimgAckw.Margin = New Thickness(5, 0, 5, 0)
                            oimgAckw.Source = YesBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Yes.png", UriKind.Relative))
                            oimgAckw.Height = 12
                            oimgAckw.Width = 12
                            ostackInner.Children.Add(oimgAckw)
                        Else
                            olbl = New TextBlock()
                            olbl.Width = 22
                            ostackInner.Children.Add(olbl)
                        End If

                        'Provide Name
                        otxtblock = New TextBlock()
                        olbl.Margin = New Thickness(2, 0, 0, 0)
                        otxtblock.Width = 215
                        otxtblock.TextWrapping = TextWrapping.Wrap
                        otxtblock.Text = Convert.ToString(odv(0)("ProviderName"))
                        ostackInner.Children.Add(otxtblock)

                        ostackMain.Children.Add(ostackInner)

                        'TestDetails
                        For j As Integer = 0 To odv.Count - 1
                            ostackInner = New StackPanel()
                            ostackInner.Margin = New Thickness(1, 1, 1, 1)
                            ostackInner.Orientation = Orientation.Horizontal

                            'STAUS
                            otxtblock = New TextBlock()
                            otxtblock.Style = DirectCast(FindResource("TestNameStyle"), Style)
                            olbl.Margin = New Thickness(2, 0, 0, 0)
                            otxtblock.Width = 282
                            otxtblock.TextWrapping = TextWrapping.Wrap
                            otxtblock.Text = Convert.ToString(odv(j)("TestName"))

                            ostackInner.Children.Add(otxtblock)


                            Dim oConmenu As ContextMenu = Nothing
                            'DICOM
                            If Convert.ToString(odv(j)("DICOMF")) = "Visible" Then
                                oimgDicom = New Image()
                                'oimgDicom.Margin = New Thickness(10, 0, 0, 0)
                                oimgDicom.Margin = New Thickness(1, 0, 1, 0)
                                oimgDicom.Source = Dicom01BitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Dicom01.ico", UriKind.Relative))

                                oimgDicom.Height = 15
                                oimgDicom.Width = 15


                                oConmenu = New ContextMenu()
                                Dim omenu As MenuItem = New MenuItem()
                                omenu.Header = "View DICOM File"
                                omenu.Tag = Convert.ToString(odv(j)("DICOMID"))
                                oimgAckw = New Image()
                                oimgAckw.Width = 15
                                oimgAckw.Height = 15
                                oimgAckw.Source = Dicom01BitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Dicom01.ico", UriKind.Relative))
                                omenu.Icon = oimgAckw
                                AddHandler omenu.Click, AddressOf MenuItem_Click_1
                                oConmenu.Items.Add(omenu)
                                If (IsNothing(oimgDicom.ContextMenu) = False) Then
                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(oimgDicom.ContextMenu)
                                    oimgDicom.ContextMenu.Items.Clear()
                                    oimgDicom.ContextMenu = Nothing
                                End If
                                oimgDicom.ContextMenu = oConmenu

                                Dim obr As Border = New Border()
                                obr.Margin = New Thickness(5, 0, 0, 0)
                                obr.Width = 17
                                obr.Height = 17
                                obr.Style = DirectCast(FindResource("ImgBorderStyle"), Style)
                                obr.Child = oimgDicom

                                ostackInner.Children.Add(obr)
                            Else
                                olbl = New TextBlock()
                                olbl.Width = 22
                                ostackInner.Children.Add(olbl)
                            End If


                            'DMS
                            If Convert.ToString(odv(j)("DMSF")) = "Visible" Then
                                oimgDMS = New Image()

                                'oimgDMS.Margin = New Thickness(10, 0, 3, 0)
                                oimgDMS.Margin = New Thickness(1, 0, 1, 0)
                                oimgDMS.Source = AttachmentBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
                                oimgDMS.Tag = Convert.ToString(odv(j)("DMSID"))


                                oConmenu = New ContextMenu()
                                Dim omenu As MenuItem = New MenuItem()
                                Dim oMnuSubItem As MenuItem
                                omenu.Header = "View Result Document"

                                Dim dtDoc As DataTable = Get_DocumentDetails(PatientID, _SelectedOrderID, Convert.ToInt64(odv(j)("TestID")))

                                oimgAckw = New Image()
                                oimgAckw.Width = 15
                                oimgAckw.Height = 15
                                oimgAckw.Source = DocumentBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result Document.ico", UriKind.Relative))
                                ''icon image added 
                                omenu.Icon = oimgAckw
                                'omenu.Tag = Convert.ToString(odv(j)("DMSID"))
                                'omenu.Icon = oimgAckw
                                'AddHandler omenu.Click, AddressOf MenuItem_Click

                                '  omenu = Nothing

                                Dim i As Integer = 0
                                If Not IsNothing(dtDoc) Then
                                    If dtDoc.Rows.Count > 0 Then
                                        For i = 0 To dtDoc.Rows.Count - 1
                                            oMnuSubItem = New MenuItem
                                            oMnuSubItem.Header = dtDoc.Rows(i)("DocumentName")
                                            oMnuSubItem.Tag = dtDoc.Rows(i)("DocumentID")
                                            oimgAckw = New Image()
                                            oimgAckw.Width = 15
                                            oimgAckw.Height = 15
                                            oimgAckw.Source = BulletBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))
                                            ''icon image added 
                                            oMnuSubItem.Icon = oimgAckw

                                            omenu.Items.Add(oMnuSubItem)
                                            AddHandler oMnuSubItem.Click, AddressOf MenuItem_Click
                                            oMnuSubItem = Nothing
                                        Next
                                        subitempresent = True
                                        oConmenu.Items.Add(omenu)
                                    End If
                                End If

                                omenu = Nothing
                                oMnuSubItem = Nothing
                                If Not IsNothing(dtDoc) Then
                                    dtDoc.Dispose()
                                    dtDoc = Nothing
                                End If

                                ''code added for url document
                                If Not IsNothing(_dtLaburl) Then
                                    Dim drdturl As DataRow() = _dtLaburl.Select("OrderID='" + _SelectedOrderID.ToString() + "' and TestID='" + odv(j)("TestID").ToString() + "'")
                                    If Not IsNothing(drdturl) Then
                                        If (drdturl.Length > 0) Then
                                            Dim oMnuItemViewURLdoc As New MenuItem()
                                            oMnuItemViewURLdoc.Header = "View Result URL Document"
                                            oimgAckw = New Image()
                                            oimgAckw.Margin = New Thickness(1, 0, 1, 0)
                                            oimgAckw.Width = 15
                                            oimgAckw.Height = 15
                                            oimgAckw.Source = URLBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result URL Doc.ico", UriKind.Relative))
                                            ''icon image added 
                                            oMnuItemViewURLdoc.Icon = oimgAckw

                                            For Len As Integer = 0 To drdturl.Length - 1
                                                oMnuSubItem = New MenuItem
                                                oMnuSubItem.Header = drdturl(Len)("URLDisplayName")
                                                oMnuSubItem.Tag = drdturl(Len)("URLName") + "||" + Convert.ToString(drdturl(Len)("TestID"))
                                                oimgAckw = New Image()
                                                oimgAckw.Margin = New Thickness(1, 0, 1, 0)

                                                oimgAckw.Width = 15
                                                oimgAckw.Height = 15
                                                oimgAckw.Source = BulletBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))
                                                ''icon image added 
                                                oMnuSubItem.Icon = oimgAckw

                                                oMnuItemViewURLdoc.Items.Add(oMnuSubItem)
                                                AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewURLDocument

                                            Next
                                            subitempresent = True
                                            oConmenu.Items.Add(oMnuItemViewURLdoc)
                                        End If
                                    End If
                                End If
                                If (IsNothing(oimgDMS.ContextMenu) = False) Then
                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(oimgDMS.ContextMenu)
                                    oimgDMS.ContextMenu.Items.Clear()
                                    oimgDMS.ContextMenu = Nothing
                                End If


                                oimgDMS.ContextMenu = oConmenu

                                'AddHandler oimgDMS.MouseDown, AddressOf oimgDMS_MouseDown
                                oimgDMS.Height = 15
                                oimgDMS.Width = 15
                                If subitempresent = True Then
                                    Dim obr As Border = New Border()
                                    obr.Margin = New Thickness(10, 0, 30, 0)
                                    obr.Width = 17
                                    obr.Height = 17
                                    obr.Style = DirectCast(FindResource("ImgBorderStyle"), Style)
                                    obr.Child = oimgDMS

                                    ostackInner.Children.Add(obr)
                                Else
                                    oimgDMS = Nothing
                                End If

                            End If

                            If Convert.ToString(odv(j)("DMSF")) = "Hidden" Then
                                If Not IsNothing(_dtLaburl) Then
                                    ''added to solve bugid 65634
                                    Dim _orderid As Long = internalOrderID
                                    If (internalOrderID = 0) Then
                                        _orderid = _SelectedOrderID
                                    End If
                                    Dim drdturl As DataRow() = _dtLaburl.Select("OrderID='" + _orderid.ToString() + "' and TestID='" + odv(j)("TestID").ToString() + "'")
                                    If Not IsNothing(drdturl) Then
                                        If (drdturl.Length > 0) Then
                                            oimgDMS = New Image()
                                            oimgDMS.Margin = New Thickness(1, 0, 1, 0)
                                            oimgDMS.Source = AttachmentBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
                                            oimgDMS.Tag = Convert.ToString(odv(j)("DMSID"))

                                            Dim oMnuItemViewURLdoc As New MenuItem()
                                            oConmenu = New ContextMenu()
                                            oMnuItemViewURLdoc.Header = "View Result URL Document"
                                            oimgAckw = New Image()
                                            oimgAckw.Margin = New Thickness(0, 0, 0, 0)

                                            oimgAckw.Width = 15
                                            oimgAckw.Height = 15
                                            oimgAckw.Source = URLBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result URL Doc.ico", UriKind.Relative))
                                            oMnuItemViewURLdoc.Icon = oimgAckw

                                            Dim oMnuSubItem As MenuItem
                                            For Len As Integer = 0 To drdturl.Length - 1
                                                oMnuSubItem = New MenuItem
                                                oMnuSubItem.Header = drdturl(Len)("URLDisplayName")
                                                oMnuSubItem.Tag = drdturl(Len)("URLName") + "||" + Convert.ToString(drdturl(Len)("TestID"))
                                                oimgAckw = New Image()
                                                oimgAckw.Width = 15
                                                oimgAckw.Height = 15
                                                oimgAckw.Margin = New Thickness(0, 0, 0, 0)

                                                oimgAckw.Source = BulletBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))
                                                oMnuSubItem.Icon = oimgAckw


                                                oMnuItemViewURLdoc.Items.Add(oMnuSubItem)
                                                AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewURLDocument

                                            Next
                                            subitempresent = True
                                            oConmenu.Items.Add(oMnuItemViewURLdoc)
                                            '   oimgDMS.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
                                            If (IsNothing(oimgDMS.ContextMenu) = False) Then
                                                gloGlobal.cEventHelper.RemoveAllEventHandlers(oimgDMS.ContextMenu)
                                                oimgDMS.ContextMenu.Items.Clear()
                                                oimgDMS.ContextMenu = Nothing
                                            End If
                                            oimgDMS.ContextMenu = oConmenu

                                            'AddHandler oimgDMS.MouseDown, AddressOf oimgDMS_MouseDown
                                            oimgDMS.Height = 15
                                            oimgDMS.Width = 15
                                            Dim obr As Border = New Border()
                                            obr.Margin = New Thickness(10, 0, 0, 0)
                                            obr.Width = 17
                                            obr.Height = 17
                                            obr.Style = DirectCast(FindResource("ImgBorderStyle"), Style)
                                            obr.Child = oimgDMS
                                            ostackInner.Children.Add(obr)
                                        End If
                                    End If
                                End If


                            End If

                            ostackMain.Children.Add(ostackInner)

                        Next

                        obrd.Child = ostackMain


                        lstLabTest.Items.Add(obrd)

                        lstLabTest.SelectedIndex = 0
                    Else
                        ClearField()
                    End If

                Else

                    odv = dt.DefaultView
                    Dim oConmenu As ContextMenu = Nothing
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If internalOrderID <> Convert.ToInt64(dt.Rows(i)("OrderID")) Then

                            internalOrderID = Convert.ToInt64(dt.Rows(i)("OrderID"))

                            odv.RowFilter = "OrderID = " & Convert.ToString(dt.Rows(i)("OrderID"))

                            obrd = New Border()
                            obrd.Tag = Convert.ToString(dt.Rows(i)("OrderID"))

                            ''Added for Orders that have tests pending results should have a clear indication that results are still pending. - White background on 20140305
                            If Convert.ToString(odv(0)("TestDateTime")).Trim() <> "" Then
                                obrd.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                            Else
                                obrd.Style = DirectCast(FindResource("WhiteBorderBackroundStyle"), Style)
                            End If
                            ''End White background

                            obrd.BorderThickness = New Thickness(1)
                            obrd.CornerRadius = New CornerRadius(6)
                            obrd.BorderBrush = Brushes.Black
                            AddHandler obrd.MouseEnter, AddressOf obrd_MouseEnter
                            'AddHandler obrd.MouseLeave, AddressOf obrd_MouseLeave

                            ostackMain = New StackPanel()
                            ostackMain.Orientation = Orientation.Vertical
                            ostackMain.Margin = New Thickness(2)

                            ostackInner = New StackPanel()
                            ostackInner.Orientation = Orientation.Horizontal
                            ostackInner.Margin = New Thickness(1)

                            'Test/OrderDate
                            olbl = New TextBlock()
                            olbl.Margin = New Thickness(2, 0, 0, 0)
                            olbl.TextWrapping = TextWrapping.Wrap
                            olbl.Width = 145
                            olbl.Text = Convert.ToString(odv(0)("TestDateTime"))

                            ''Added for Orders that have tests pending results should have a clear indication that results are still pending. - White background on 20140305
                            If Convert.ToString(odv(0)("TestDateTime")).Trim() <> "" Then
                                olbl.Tag = "BackgroundColor"
                            Else
                                olbl.Tag = "BackgroundWhite"
                            End If
                            ''End White background
                            ostackInner.Children.Add(olbl)

                            'OrderName
                            olbl = New TextBlock()
                            olbl.Margin = New Thickness(2, 0, 0, 0)
                            olbl.TextWrapping = TextWrapping.Wrap
                            olbl.Width = 215
                            olbl.Text = Convert.ToString(odv(0)("OrderName"))
                            ostackInner.Children.Add(olbl)

                            ostackMain.Children.Add(ostackInner)

                            ostackInner = New StackPanel()
                            ostackInner.Margin = New Thickness(1)
                            ostackInner.Orientation = Orientation.Horizontal

                            'STAUS
                            olbl = New TextBlock()
                            olbl.TextWrapping = TextWrapping.Wrap
                            olbl.Width = 125
                            olbl.Text = Convert.ToString(odv(0)("STATUS"))
                            ostackInner.Children.Add(olbl)

                            'Acknowledgement
                            If Convert.ToString(odv(0)("AckwFlag")) = "Visible" Then
                                oimgAckw = New Image()
                                oimgAckw.Margin = New Thickness(5, 0, 5, 0)
                                oimgAckw.Source = YesBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Yes.png", UriKind.Relative))
                                oimgAckw.Height = 12
                                oimgAckw.Width = 12
                                ostackInner.Children.Add(oimgAckw)
                            Else
                                olbl = New TextBlock()
                                olbl.Width = 22
                                ostackInner.Children.Add(olbl)
                            End If

                            'Provide Name
                            otxtblock = New TextBlock()
                            olbl.Margin = New Thickness(2, 0, 0, 0)
                            otxtblock.Width = 215
                            otxtblock.TextWrapping = TextWrapping.Wrap
                            otxtblock.Text = Convert.ToString(odv(0)("ProviderName"))
                            ostackInner.Children.Add(otxtblock)

                            ostackMain.Children.Add(ostackInner)

                            'TestDetails
                            For j As Integer = 0 To odv.Count - 1
                                ostackInner = New StackPanel()
                                ostackInner.Margin = New Thickness(1, 1, 1, 1)
                                ostackInner.Orientation = Orientation.Horizontal

                                'STAUS
                                otxtblock = New TextBlock()
                                otxtblock.Style = DirectCast(FindResource("TestNameStyle"), Style)
                                olbl.Margin = New Thickness(2, 0, 0, 0)
                                otxtblock.Width = 282
                                otxtblock.TextWrapping = TextWrapping.Wrap
                                otxtblock.Text = Convert.ToString(odv(j)("TestName"))

                                ostackInner.Children.Add(otxtblock)



                                'DICOM
                                If Convert.ToString(odv(j)("DICOMF")) = "Visible" Then
                                    oimgDicom = New Image()
                                    'oimgDicom.Margin = New Thickness(10, 0, 0, 0)
                                    oimgDicom.Margin = New Thickness(1, 0, 1, 0)
                                    oimgDicom.Source = Dicom01BitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Dicom01.ico", UriKind.Relative))

                                    oimgDicom.Height = 15
                                    oimgDicom.Width = 15


                                    oConmenu = New ContextMenu()
                                    Dim omenu As MenuItem = New MenuItem()
                                    omenu.Header = "View DICOM File"
                                    omenu.Tag = Convert.ToString(odv(j)("DICOMID"))
                                    oimgAckw = New Image()
                                    oimgAckw.Width = 15
                                    oimgAckw.Height = 15
                                    oimgAckw.Source = Dicom01BitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Dicom01.ico", UriKind.Relative))
                                    omenu.Icon = oimgAckw
                                    AddHandler omenu.Click, AddressOf MenuItem_Click_1
                                    oConmenu.Items.Add(omenu)
                                    If (IsNothing(oimgDicom.ContextMenu) = False) Then
                                        gloGlobal.cEventHelper.RemoveAllEventHandlers(oimgDicom.ContextMenu)
                                        oimgDicom.ContextMenu.Items.Clear()
                                        oimgDicom.ContextMenu = Nothing
                                    End If
                                    oimgDicom.ContextMenu = oConmenu

                                    Dim obr As Border = New Border()
                                    obr.Margin = New Thickness(5, 0, 0, 0)
                                    obr.Width = 17
                                    obr.Height = 17
                                    obr.Style = DirectCast(FindResource("ImgBorderStyle"), Style)
                                    obr.Child = oimgDicom

                                    ostackInner.Children.Add(obr)
                                Else
                                    olbl = New TextBlock()
                                    olbl.Width = 22
                                    ostackInner.Children.Add(olbl)
                                End If


                                'DMS
                                Dim oMnuSubItem As MenuItem
                                '   Dim oConmenu As ContextMenu
                                If Convert.ToString(odv(j)("DMSF")) = "Visible" Then
                                    oimgDMS = New Image()

                                    'oimgDMS.Margin = New Thickness(10, 0, 3, 0)
                                    oimgDMS.Margin = New Thickness(1, 0, 1, 0)
                                    oimgDMS.Source = AttachmentBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
                                    oimgDMS.Tag = Convert.ToString(odv(j)("DMSID"))


                                    oConmenu = New ContextMenu()
                                    Dim omenu As MenuItem = New MenuItem()

                                    omenu.Header = "View Result Document"

                                    Dim oimgAckw4 As New Image()
                                    oimgAckw4.Width = 15
                                    oimgAckw4.Height = 15
                                    oimgAckw4.Source = DocumentBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result Document.ico", UriKind.Relative))
                                    omenu.Icon = oimgAckw4
                                    'omenu.Tag = Convert.ToString(odv(j)("DMSID"))
                                    'omenu.Icon = oimgAckw
                                    'AddHandler omenu.Click, AddressOf MenuItem_Click
                                    Dim dtDoc As DataTable = Get_DocumentDetails(PatientID, internalOrderID, Convert.ToInt64(odv(j)("TestID")))

                                    Dim k As Integer = 0

                                    If Not IsNothing(dtDoc) Then
                                        If dtDoc.Rows.Count > 0 Then
                                            For k = 0 To dtDoc.Rows.Count - 1
                                                oMnuSubItem = New MenuItem
                                                oMnuSubItem.Header = dtDoc.Rows(k)("DocumentName")
                                                oMnuSubItem.Tag = dtDoc.Rows(k)("DocumentID")
                                                Dim oimgAckw5 As New Image()
                                                oimgAckw5.Margin = New Thickness(0, 0, 0, 0)
                                                oimgAckw5.Width = 15
                                                oimgAckw5.Height = 15
                                                oimgAckw5.Source = BulletBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))

                                                oMnuSubItem.Icon = oimgAckw5

                                                omenu.Items.Add(oMnuSubItem)
                                                AddHandler oMnuSubItem.Click, AddressOf MenuItem_Click
                                                oMnuSubItem = Nothing
                                            Next
                                            subitempresent = True
                                            oConmenu.Items.Add(omenu)

                                        End If
                                    End If

                                    oMnuSubItem = Nothing
                                    If Not IsNothing(dtDoc) Then
                                        dtDoc.Dispose()
                                        dtDoc = Nothing
                                    End If
                                    ''code added for url document
                                    If Not IsNothing(_dtLaburl) Then
                                        ''added to solve bugid 65634
                                        Dim _orderid As Long = internalOrderID
                                        If (internalOrderID = 0) Then
                                            _orderid = _SelectedOrderID
                                        End If

                                        Dim drdturl As DataRow() = _dtLaburl.Select("OrderID='" + _orderid.ToString() + "' and TestID='" + odv(j)("TestID").ToString() + "'")
                                        If Not IsNothing(drdturl) Then
                                            If (drdturl.Length > 0) Then
                                                Dim oMnuItemViewURLdoc As New MenuItem()
                                                oMnuItemViewURLdoc.Header = "View Result URL Document"
                                                Dim oimgAckw2 As New Image()
                                                oimgAckw2.Width = 15
                                                oimgAckw2.Height = 15
                                                oimgAckw2.Source = URLBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result URL Doc.ico", UriKind.Relative))
                                                oMnuItemViewURLdoc.Icon = oimgAckw2

                                                For Len As Integer = 0 To drdturl.Length - 1
                                                    oMnuSubItem = New MenuItem
                                                    oMnuSubItem.Header = drdturl(Len)("URLDisplayName")
                                                    oMnuSubItem.Tag = drdturl(Len)("URLName") + "||" + Convert.ToString(drdturl(Len)("TestID"))
                                                    Dim oimgAckw3 As New Image()
                                                    oimgAckw3.Width = 15
                                                    oimgAckw3.Height = 15
                                                    oimgAckw3.Source = BulletBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))
                                                    oMnuSubItem.Icon = oimgAckw3

                                                    oMnuItemViewURLdoc.Items.Add(oMnuSubItem)
                                                    AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewURLDocument

                                                Next
                                                subitempresent = True
                                                oConmenu.Items.Add(oMnuItemViewURLdoc)
                                                '   oimgDMS.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))

                                            End If
                                        End If
                                    End If

                                    If (IsNothing(oimgDMS.ContextMenu) = False) Then
                                        gloGlobal.cEventHelper.RemoveAllEventHandlers(oimgDMS.ContextMenu)
                                        oimgDMS.ContextMenu.Items.Clear()
                                        oimgDMS.ContextMenu = Nothing
                                    End If

                                    oimgDMS.ContextMenu = oConmenu

                                    'AddHandler oimgDMS.MouseDown, AddressOf oimgDMS_MouseDown
                                    oimgDMS.Height = 15
                                    oimgDMS.Width = 15

                                    If (subitempresent = True) Then
                                        Dim obr As Border = New Border()
                                        obr.Margin = New Thickness(10, 0, 0, 0)
                                        obr.Width = 17
                                        obr.Height = 17
                                        obr.Style = DirectCast(FindResource("ImgBorderStyle"), Style)
                                        obr.Child = oimgDMS

                                        ostackInner.Children.Add(obr)
                                    Else
                                        oimgDMS = Nothing
                                    End If
                                End If
                                If Convert.ToString(odv(j)("DMSF")) = "Hidden" Then
                                    If Not IsNothing(_dtLaburl) Then
                                        ''added to solve bugid 65634
                                        Dim _orderid As Long = internalOrderID
                                        If (internalOrderID = 0) Then
                                            _orderid = _SelectedOrderID
                                        End If
                                        Dim drdturl As DataRow() = _dtLaburl.Select("OrderID='" + _orderid.ToString() + "' and TestID='" + odv(j)("TestID").ToString() + "'")
                                        If Not IsNothing(drdturl) Then
                                            If (drdturl.Length > 0) Then
                                                oimgDMS = New Image()
                                                oimgDMS.Margin = New Thickness(1, 0, 1, 0)
                                                oimgDMS.Source = AttachmentBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
                                                oimgDMS.Tag = Convert.ToString(odv(j)("DMSID"))

                                                Dim oMnuItemViewURLdoc As New MenuItem()
                                                oConmenu = New ContextMenu()
                                                oMnuItemViewURLdoc.Header = "View Result URL Document"
                                                Dim oimgAckw2 As New Image()
                                                oimgAckw2.Width = 15
                                                oimgAckw2.Height = 15
                                                oimgAckw2.Source = URLBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/View Result URL Doc.ico", UriKind.Relative))
                                                oMnuItemViewURLdoc.Icon = oimgAckw2

                                                For Len As Integer = 0 To drdturl.Length - 1
                                                    oMnuSubItem = New MenuItem
                                                    oMnuSubItem.Header = drdturl(Len)("URLDisplayName")
                                                    oMnuSubItem.Tag = drdturl(Len)("URLName") + "||" + Convert.ToString(drdturl(Len)("TestID"))
                                                    Dim oimgAckw3 As New Image()
                                                    oimgAckw3.Width = 15
                                                    oimgAckw3.Height = 15
                                                    oimgAckw3.Source = BulletBitmapImage 'New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Bullet06.ico", UriKind.Relative))
                                                    oMnuSubItem.Icon = oimgAckw3
                                                    oMnuItemViewURLdoc.Items.Add(oMnuSubItem)
                                                    AddHandler oMnuSubItem.Click, AddressOf Set_Menu_ViewURLDocument

                                                Next
                                                subitempresent = True
                                                oConmenu.Items.Add(oMnuItemViewURLdoc)
                                                '   oimgDMS.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Attachment.png", UriKind.Relative))
                                                If (IsNothing(oimgDMS.ContextMenu) = False) Then
                                                    gloGlobal.cEventHelper.RemoveAllEventHandlers(oimgDMS.ContextMenu)
                                                    oimgDMS.ContextMenu.Items.Clear()
                                                    oimgDMS.ContextMenu = Nothing
                                                End If
                                                oimgDMS.ContextMenu = oConmenu

                                                'AddHandler oimgDMS.MouseDown, AddressOf oimgDMS_MouseDown
                                                oimgDMS.Height = 15
                                                oimgDMS.Width = 15
                                                Dim obr As Border = New Border()
                                                obr.Margin = New Thickness(10, 0, 0, 0)
                                                obr.Width = 17
                                                obr.Height = 17
                                                obr.Style = DirectCast(FindResource("ImgBorderStyle"), Style)
                                                obr.Child = oimgDMS
                                                ostackInner.Children.Add(obr)
                                            End If
                                        End If
                                    End If


                                End If

                                ostackMain.Children.Add(ostackInner)
                            Next

                            obrd.Child = ostackMain


                            lstLabTest.Items.Add(obrd)

                            If _InternalSelectedOrderID = internalOrderID Then
                                selectedIndex = lstLabTest.Items.Count - 1
                                _InternalSelectedOrderID = 0
                            End If

                            ' To set Order selected for related Task.
                            If (_TaskRelatedOrderID <> 0) Then
                                If _TaskRelatedOrderID = internalOrderID Then
                                    selectedIndex = lstLabTest.Items.Count - 1
                                    _TaskRelatedOrderID = 0
                                End If
                            End If

                        End If
                    Next

                    lstLabTest.SelectedIndex = selectedIndex

                End If

            End If

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Set_Menu_ViewURLDocument(ByVal sender As Object, ByVal e As System.EventArgs)  '' added for url document
        Try
            Dim strurl As String()
            Dim mnu As MenuItem = CType(sender, MenuItem)
            If (Not IsNothing(mnu.Tag)) Then
                strurl = mnu.Tag.ToString().Split("||")
                If (strurl.Length > 0) Then
                    gloGlobal.gloLabGenral.RunInSystemDefaultBrowser(strurl(0))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CreateTestListBoxItem(ByVal dt As DataTable)
        CreateHeader()
        lstTestDetails.Items.Clear()

        Dim odv As DataView
        Dim strflag() As String
        Dim ostackMain As StackPanel, ostackInner As StackPanel, ostackResult, ostackOuterMain As StackPanel
        Dim otxtb As TextBlock
        
        Dim objRichTextBox As System.Windows.Forms.RichTextBox = Nothing 'added by manoj on 20121127 for test comment as hyperlink

        GetWidthOfCOntrol()
        Dim TestlistWidth As [Double] = ControlWidth - 380 - 20

        Dim internalTestID As Int64 = 0

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            odv = dt.DefaultView
            For i As Integer = 0 To dt.Rows.Count - 1
                If internalTestID <> Convert.ToInt64(dt.Rows(i)("TestID")) Then
                    internalTestID = Convert.ToInt64(dt.Rows(i)("TestID"))

                    odv.RowFilter = "TestID = " & Convert.ToString(dt.Rows(i)("TestID"))

                    ostackOuterMain = New StackPanel()
                    ostackOuterMain.Orientation = Orientation.Vertical
                    ostackOuterMain.Margin = New Thickness(1)


                    ostackMain = New StackPanel()
                    ostackMain.Orientation = Orientation.Horizontal
                    ostackMain.Margin = New Thickness(0, 2, 0, 5)
                    ostackMain.Style = DirectCast(FindResource("PanelBackgroundWhiteStyle"), Style)
                    'obrd = new Border();


                    'Test Name
                    otxtb = New TextBlock()
                    otxtb.Margin = New Thickness(2, 0, 0, 0)
                    otxtb.TextWrapping = TextWrapping.Wrap
                    otxtb.Width = TestlistWidth * 0.22
                    otxtb.Text = Convert.ToString(odv(0)("TestName"))
                    otxtb.Style = DirectCast(FindResource("TestNameStyle"), Style)
                    ostackMain.Children.Add(otxtb)



                    ostackInner = New StackPanel()
                    ostackInner.Orientation = Orientation.Vertical
                    ostackInner.Margin = New Thickness(0, 2, 0, 2)

                    For j As Integer = 0 To odv.Count - 1

                        ostackResult = New StackPanel()
                        ostackResult.Orientation = Orientation.Horizontal
                        ostackResult.Margin = New Thickness(1)

                        'Result Name
                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(2, 0, 0, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = TestlistWidth * 0.2
                        otxtb.Text = Convert.ToString(odv(j)("ResultName"))
                        otxtb.Tag = Convert.ToString(odv(0)("TestId"))
                        If (Not String.IsNullOrEmpty(Convert.ToString(odv(j)("SnomedCode"))) Or Not String.IsNullOrEmpty(Convert.ToString(odv(j)("SnomedCode"))) Or Not String.IsNullOrEmpty(Convert.ToString(odv(j)("ICD9Code"))) Or Not String.IsNullOrEmpty(Convert.ToString(odv(j)("ICD10Code"))) Or Not String.IsNullOrEmpty(Convert.ToString(odv(j)("LoincCode")))) Then
                            otxtb.Foreground = Brushes.Blue
                        Else
                            otxtb.Foreground = _DefaultColor
                        End If
                        ostackResult.Children.Add(otxtb)

                        'value
                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(2, 0, 0, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = TestlistWidth * 0.2
                        'otxtb.Text = Convert.ToString(odv(j)("ResultValue")) commented by manoj on 20121127 

                        'start code added by manoj on 20121127 for processing making result value as hyperlink
                        If gloGlobal.gloLabGenral.IsResultisHyperLink(odv(j)("ResultValue")) Then
                            alink = New System.Windows.Documents.Hyperlink()
                            alink.Tag = "0"
                            alink.Inlines.Clear()
                            alink.Inlines.Add(Convert.ToString(odv(j)("ResultValue")))
                            AddHandler alink.Click, AddressOf Hyperlink_Click
                            otxtb.Inlines.Add(alink)
                        Else
                            otxtb.Text = Convert.ToString(odv(j)("ResultValue")) & " " & Convert.ToString(odv(j)("ResultUnit"))
                        End If
                        'end of code added by manoj on 20121127 for processing making result value as hyperlink

                        strflag = (Convert.ToString(odv(j)("Flag")).Split(","))
                        If strflag.Length > 1 Then
                            If strflag(0) = "A" Or strflag(0) = "L" Or strflag(0) = "H" Or strflag(0) = "LL" Or strflag(0) = "HH" Or strflag(0) = "AA" Or strflag(0) = ",A" Or strflag(0) = ",L" Or strflag(0) = ",H" Or strflag(0) = ",LL" Or strflag(0) = ",HH" Or strflag(0) = ",AA" Or strflag(1) = "A" Or strflag(1) = "L" Or strflag(1) = "H" Or strflag(1) = "LL" Or strflag(1) = "HH" Or strflag(1) = "AA" Or strflag(1) = ",A" Or strflag(1) = ",L" Or strflag(1) = ",H" Or strflag(1) = ",LL" Or strflag(1) = ",HH" Or strflag(1) = ",AA" Then
                                otxtb.Style = DirectCast(FindResource("AbnormalStyle"), Style)
                            End If
                        Else
                            If strflag(0) = "A" Or strflag(0) = "L" Or strflag(0) = "H" Or strflag(0) = "LL" Or strflag(0) = "HH" Or strflag(0) = "AA" Or strflag(0) = ",A" Or strflag(0) = ",L" Or strflag(0) = ",H" Or strflag(0) = ",LL" Or strflag(0) = ",HH" Or strflag(0) = ",AA" Then
                                otxtb.Style = DirectCast(FindResource("AbnormalStyle"), Style)
                            End If
                        End If

                        ostackResult.Children.Add(otxtb)

                        'value
                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(2, 0, 0, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = TestlistWidth * 0.12
                        otxtb.Text = Convert.ToString(odv(j)("ResultRange"))
                        ostackResult.Children.Add(otxtb)

                        'value
                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(2, 0, 0, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = TestlistWidth * 0.06
                        otxtb.Text = Convert.ToString(odv(j)("ResultType"))
                        ostackResult.Children.Add(otxtb)

                        'Lab
                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(2, 0, 0, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = TestlistWidth * 0.06
                        otxtb.Text = Convert.ToString(odv(j)("LabName"))
                        ostackResult.Children.Add(otxtb)

                        ostackInner.Children.Add(ostackResult)

                        If Convert.ToString(odv(j)("ShowComment")) = "Visible" Then

                            ostackResult = New StackPanel()
                            ostackResult.Orientation = Orientation.Horizontal
                            ostackResult.Margin = New Thickness(2)

                            otxtb = New TextBlock()
                            otxtb.Margin = New Thickness(7, 0, 0, 0)
                            otxtb.TextWrapping = TextWrapping.Wrap
                            otxtb.Width = 60
                            otxtb.OverridesDefaultStyle = True
                            otxtb.Text = "Comment: "

                            If (Not String.IsNullOrEmpty(_LabResulSetCommentFontName)) Then
                                otxtb.FontFamily = New FontFamily(_LabResulSetCommentFontName)
                            End If

                            If (_LabResulSetCommentFontSize > 0) Then
                                otxtb.FontSize = _LabResulSetCommentFontSize
                            End If

                            'otxtb.FontFamily = New FontFamily("Courier New")
                            'otxtb.FontSize = 10.5
                            ostackResult.Children.Add(otxtb)
                            ostackInner.Children.Add(ostackResult)
                            
                            ostackResult = New StackPanel()
                            ostackResult.Orientation = Orientation.Horizontal
                            ostackResult.Margin = New Thickness(2)


                            otxtb = New TextBlock()
                            otxtb.Margin = New Thickness(14, 0, 0, 0)
                            otxtb.TextWrapping = TextWrapping.Wrap
                            otxtb.Width = TestlistWidth * 0.63
                            otxtb.OverridesDefaultStyle = True
                            DesignResultNotesTexBlock(otxtb, Convert.ToString(odv(j)("ResultComment")))

                            If (Not String.IsNullOrEmpty(_LabResulSetCommentFontName)) Then
                                otxtb.FontFamily = New FontFamily(_LabResulSetCommentFontName)
                            End If

                            If (_LabResulSetCommentFontSize > 0) Then
                                otxtb.FontSize = _LabResulSetCommentFontSize
                            End If

                            'otxtb.FontFamily = New FontFamily("Courier New")
                            'otxtb.FontSize = 10.5

                            ostackResult.Children.Add(otxtb)

                            ostackInner.Children.Add(ostackResult)

                        End If
                    Next

                    ostackMain.Children.Add(ostackInner)

                    ostackOuterMain.Children.Add(ostackMain)

                    If Not Convert.ToString(odv(0)("TestNote")).Trim() = "" Then

                        ostackResult = New StackPanel()
                        ostackResult.Orientation = Orientation.Horizontal
                        ostackResult.Margin = New Thickness(2)

                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(10, 0, 2, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = 90
                        otxtb.Text = "Test Comment: "
                        ostackResult.Children.Add(otxtb)

                        otxtb = New TextBlock()
                        otxtb.Margin = New Thickness(2, 0, 0, 0)
                        otxtb.TextWrapping = TextWrapping.Wrap
                        otxtb.Width = TestlistWidth * 0.75
                        otxtb.Text = Convert.ToString(odv(0)("testNote"))


                        ostackResult.Children.Add(otxtb)


                        ostackOuterMain.Children.Add(ostackResult)

                    End If

                    lstTestDetails.Items.Add(ostackOuterMain)
                End If
            Next
        End If

    End Sub

    Private Sub CreateHeader()
        pnlHeader.Children.Clear()
        Dim olbl As Label
        GetWidthOfCOntrol()
        Dim TestlistWidth As [Double] = ControlWidth - 380 - 20

        olbl = New Label()
        olbl.Style = DirectCast(FindResource("HeaderLabelStyle"), Style)
        olbl.Margin = New Thickness(2, 0, 0, 0)
        olbl.Width = TestlistWidth * 0.22
        olbl.Content = "Test"
        pnlHeader.Children.Add(olbl)

        olbl = New Label()
        olbl.Style = DirectCast(FindResource("HeaderLabelStyle"), Style)
        olbl.Margin = New Thickness(2, 0, 0, 0)
        olbl.Width = TestlistWidth * 0.2
        olbl.Content = "Result"
        pnlHeader.Children.Add(olbl)

        olbl = New Label()
        olbl.Style = DirectCast(FindResource("HeaderLabelStyle"), Style)
        olbl.Margin = New Thickness(2, 0, 0, 0)
        olbl.Width = TestlistWidth * 0.2
        olbl.Content = "Value"
        pnlHeader.Children.Add(olbl)

        olbl = New Label()
        olbl.Style = DirectCast(FindResource("HeaderLabelStyle"), Style)
        olbl.Margin = New Thickness(2, 0, 0, 0)
        olbl.Width = TestlistWidth * 0.12
        olbl.Content = "Ref. Range"
        pnlHeader.Children.Add(olbl)

        olbl = New Label()
        olbl.Style = DirectCast(FindResource("HeaderLabelStyle"), Style)
        olbl.Margin = New Thickness(2, 0, 0, 0)
        olbl.Width = TestlistWidth * 0.06
        olbl.Content = "Flag"
        pnlHeader.Children.Add(olbl)

        olbl = New Label()
        olbl.Style = DirectCast(FindResource("HeaderLabelStyle"), Style)
        olbl.Margin = New Thickness(2, 0, 0, 0)
        olbl.Width = TestlistWidth * 0.06
        olbl.Content = "Lab"
        pnlHeader.Children.Add(olbl)

    End Sub


    Private Sub obrd_MouseEnter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim obd As Border = DirectCast(sender, Border)
        Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
        Storyboard.SetTarget(sb, obd)
        sb.Begin()
    End Sub

    Private Sub oimgDMS_MouseDown(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles imgDMS.MouseDown
        Dim oimg As Image = DirectCast(sender, Image)
        RaiseEvent OpenDocument(TestID, Convert.ToInt64(oimg.Tag))
    End Sub

    Private Sub GetWidthOfCOntrol()
        Dim Srn As Screen = Screen.PrimaryScreen
        ControlWidth = Srn.Bounds.Width
    End Sub

    Private Sub chkDate_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkDate.Click
        Try
            If chkDate.IsChecked = True Then
                dtpFrom.IsEnabled = True
                dtpTo.IsEnabled = True
            Else
                dtpFrom.IsEnabled = False
                dtpTo.IsEnabled = False
            End If
            ShowTestData()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbtnAll_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbtnAll.Checked
        ShowTestData()
    End Sub

    Private Sub rbtnAcknowledged_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbtnAcknowledged.Checked
        ShowTestData()
    End Sub

    Private Sub rbtnNotAcknowledged_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rbtnNotAcknowledged.Checked
        ShowTestData()
    End Sub


    'added by manoj jadhav on 20121207 for result value as hyperlink implementation
    Private Sub alink_RequestNavigate(sender As Object, e As System.Windows.Navigation.RequestNavigateEventArgs) Handles alink.RequestNavigate
        Try
            If Not e.Uri Is Nothing Then
                e.Source.Tag = "1"
                System.Diagnostics.Process.Start(e.Uri.ToString())
                e.Handled = True
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'added by manoj jadhav on 20121207 for result value as hyperlink implementation
    Private Sub Hyperlink_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            If Not e.Source Is Nothing Then
                gloGlobal.gloLabGenral.OpenLinkInBrowser(e.Source.Inlines.FirstInline().Text)
                alink = e.Source
                alink.Tag = "1"
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'added by manoj jadhav on 20121207 for result value as hyperlink implementation
    Private Sub txtTestResultComment_LinkClicked(sender As Object, e As System.Windows.Forms.LinkClickedEventArgs)
        Try
            If Not String.IsNullOrEmpty(e.LinkText.Trim()) Then
                gloGlobal.gloLabGenral.OpenLinkInBrowser(e.LinkText.Trim())
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'added by manoj jadhav on 20121207 for result value as hyperlink implementation
    Private Sub DesignResultNotesTexBlock(ByRef ResulttextBlock As TextBlock, ByVal ResultComment As String)
        Dim IOfWww As Integer = 0
        Dim IOfHttp As Integer = 0
        Dim IOfHttps As Integer = 0
        Dim IOfHttp1 As Integer = 0
        Dim IOfHttps1 As Integer = 0
        Dim iIndex As Integer = 0
        Dim EIndex As Integer = 0
        Try
            ResulttextBlock.Inlines.Clear()
            If String.IsNullOrEmpty(ResultComment) Then
                Exit Try
            End If
            While (ResultComment <> String.Empty)
                iIndex = -1
                EIndex = -1
                IOfWww = ResultComment.ToLower().IndexOf("www.")
                IOfHttp = ResultComment.ToLower().IndexOf("http://")
                IOfHttps = ResultComment.ToLower().IndexOf("https://")
                IOfHttp1 = ResultComment.ToLower().IndexOf("http:\\")
                IOfHttps1 = ResultComment.ToLower().IndexOf("https:\\")
                If IOfWww > -1 Then
                    iIndex = IOfWww
                ElseIf IOfHttp > -1 Then
                    iIndex = IOfHttp
                ElseIf IOfHttps > -1 Then
                    iIndex = IOfHttps
                ElseIf IOfHttp1 > -1 Then
                    iIndex = IOfHttp1
                ElseIf IOfHttps1 > -1 Then
                    iIndex = IOfHttps1
                End If
                If (IOfWww > -1) AndAlso ((IOfHttp > -1 AndAlso IOfHttp >= IOfWww) Or (IOfHttps > -1 AndAlso IOfHttps >= IOfWww) Or _
                            (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfWww) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfWww)) Then
                    iIndex = IOfWww
                ElseIf (IOfHttp > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttp) Or (IOfHttps > -1 AndAlso IOfHttps >= IOfHttp) Or _
                       (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttp) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttp)) Then
                    iIndex = IOfHttp
                ElseIf (IOfHttps > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttps) Or (IOfHttp > -1 AndAlso IOfHttp >= IOfHttps) Or _
                   (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttps) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttps)) Then
                    iIndex = IOfHttps
                ElseIf (IOfHttp1 > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttp1) Or (IOfHttp > -1 AndAlso IOfHttp >= IOfHttp1) Or _
                      (IOfHttps > -1 AndAlso IOfHttps >= IOfHttp1) Or (IOfHttps1 > -1 AndAlso IOfHttps1 >= IOfHttp1)) Then
                    iIndex = IOfHttp1
                ElseIf (IOfHttps1 > -1) AndAlso ((IOfWww > -1 AndAlso IOfWww >= IOfHttps1) Or (IOfHttp > -1 AndAlso IOfHttp >= IOfHttps1) Or _
                      (IOfHttps > -1 AndAlso IOfHttps >= IOfHttps1) Or (IOfHttp1 > -1 AndAlso IOfHttp1 >= IOfHttps1)) Then
                    iIndex = IOfHttp1
                End If
                If iIndex <= -1 Then
                    ResulttextBlock.Inlines.Add(ResultComment)
                    Exit While
                End If
                For k As Integer = iIndex To Len(ResultComment) - 1
                    If ResultComment.Substring(k, 1) = " " Or ResultComment.Substring(k, 1) = vbCr Or ResultComment.Substring(k, 1) = vbCrLf Then
                        EIndex = (k - iIndex)
                        Exit For
                    End If
                    If k = Len(ResultComment) - 1 Then
                        EIndex = (k - iIndex) + 1
                    End If
                Next
                If EIndex <= -1 Then
                    EIndex = Len(ResultComment) - 1
                End If

                If EIndex <= -1 Then
                    ResulttextBlock.Inlines.Add(ResultComment)
                    Exit While
                End If
                ResulttextBlock.Inlines.Add(ResultComment.Substring(0, iIndex))
                alink = New System.Windows.Documents.Hyperlink()
                alink.Inlines.Clear()
                Dim s As String = ResultComment.Substring(iIndex, EIndex)
                alink.Inlines.Add(s)
                AddHandler alink.Click, AddressOf Hyperlink_Click1
                ResulttextBlock.Inlines.Add(alink)
                If (iIndex + EIndex) < Len(ResultComment) Then
                    ResultComment = ResultComment.Substring((iIndex + EIndex), (Len(ResultComment) - (iIndex + EIndex)))
                Else
                    ResultComment = String.Empty
                End If
            End While
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'added by manoj jadhav on 20121207 for result value as hyperlink implementation
    Private Sub Hyperlink_Click1(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            If Not e.Source Is Nothing Then
                gloGlobal.gloLabGenral.OpenLinkInBrowser(e.Source.Inlines.FirstInline().Text)
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuAcknowledge_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent Acknowlegeclick()
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuAcknowledgeNormal_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent AcknowlegeNormalclick()
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub lstLabTest_ContextMenuOpening(sender As System.Object, e As System.Windows.Controls.ContextMenuEventArgs)
        Try
            If lstLabTest Is Nothing OrElse lstLabTest.SelectedValue Is Nothing OrElse lstLabTest.SelectedItems.Count = 0 Then
                e.Handled = True
            Else
                RaiseEvent customMenuOpening(sender, e)
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuUnAcknowledge_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent MarkUnAcknowlegeclick()
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Show Hide Acknowledge Menu from Contexttrip 
    ''' </summary>
    ''' <param name="ShowHide"></param>
    ''' <remarks>true to show False to Hide</remarks>
    Public Sub ShowHideAckcontextMenuTrip(ShowHide As Boolean)
        Try
            Dim mnuTrip As ContextMenu = DirectCast(FindResource("AckContextMenu"), ContextMenu)
            If mnuTrip IsNot Nothing Then
                If mnuTrip.Items.Count > 0 Then
                    Dim menu As MenuItem = DirectCast(mnuTrip.Items(0), MenuItem)
                    If ShowHide = True Then
                        menu.Visibility = Visibility.Visible
                    Else
                        menu.Visibility = Visibility.Collapsed
                    End If
                End If
            End If
        Catch generatedExceptionName As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Show Hide Acknowledge Normal Menu from Contexttrip 
    ''' </summary>
    ''' <param name="ShowHide"></param>
    ''' <remarks>true to show False to Hide</remarks>
    Public Sub ShowHideAckNormalcontextMenuTrip(ShowHide As Boolean)
        Try
            Dim mnuTrip As ContextMenu = DirectCast(FindResource("AckContextMenu"), ContextMenu)
            If mnuTrip IsNot Nothing Then
                If mnuTrip.Items.Count = 3 Then
                    Dim menu As MenuItem = DirectCast(mnuTrip.Items(1), MenuItem)
                    If ShowHide = True Then
                        menu.Visibility = Visibility.Visible
                    Else
                        menu.Visibility = Visibility.Collapsed
                    End If
                End If
            End If
        Catch generatedExceptionName As Exception
        End Try
    End Sub


    ''' <summary>
    ''' Show Hide Un-Acknowledge Menu from Contexttrip 
    ''' </summary>
    ''' <param name="ShowHide"></param>
    ''' <remarks>true to show False to Hide</remarks>
    Public Sub ShowHideUnAckNormalcontextMenuTrip(ShowHide As Boolean)
        Try
            Dim mnuTrip As ContextMenu = DirectCast(FindResource("AckContextMenu"), ContextMenu)
            If mnuTrip IsNot Nothing Then
                If mnuTrip.Items.Count = 3 Then
                    Dim menu As MenuItem = DirectCast(mnuTrip.Items(2), MenuItem)
                    If ShowHide = True Then
                        menu.Visibility = Visibility.Visible
                    Else
                        menu.Visibility = Visibility.Collapsed
                    End If
                End If
            End If
        Catch generatedExceptionName As Exception
        End Try
    End Sub

#Region "Show Acknowledgement"
    Private Sub ShowAcknowledgeDetails(ByVal OrderID As Long)
        Try
            AcknowledgeDtlsGrid(OrderID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub AcknowledgeDtlsGrid(ByVal OrderID As Long)

        Dim dtAck As DataTable = GetAcknowledgement(OrderID)

        If dtAck IsNot Nothing AndAlso dtAck.Rows.Count > 0 Then
            stkAcknowledge.Visibility = Windows.Visibility.Visible
            mydatagrid.ItemsSource = dtAck.DefaultView
            mydatagrid.CanUserAddRows = False
        Else
            stkAcknowledge.Visibility = Windows.Visibility.Collapsed
        End If

    End Sub

    Private Function GetAcknowledgement(ByVal OrderID As Long) As DataTable

        Dim ds As DataSet = Nothing
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer

        Try
            With odb
                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()

                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = OrderID
                oPara.Name = "@nOrderId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing

                ds = .GetDataSet("Lab_GetAcknowledgementByOrderId")

            End With

            Return ds.Tables(0).Copy()
            ' ds = Nothing

        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), True)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function


#End Region

#Region " Lab Result - Codification "

    Private Sub MenuItemSNOMED_Clicked(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent EvtCnxtMnuResult_SNOMED()

            If (Not String.IsNullOrEmpty(SnomedCode)) Then
                ShowtestDeatails(PatientID, OrderIdForCode, TestIdForCode)
            End If
        Catch ex As Exception
            ex = Nothing
        End Try

    End Sub

    Private Sub MenuItemICD_Clicked(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent EvtCnxtMnuResult_ICD()

            If (Not String.IsNullOrEmpty(ICDCode)) Then
                ShowtestDeatails(PatientID, OrderIdForCode, TestIdForCode)
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub MenuItemLOINC_Clicked(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent EvtCnxtMnuResult_LOINC()

            If (Not String.IsNullOrEmpty(LoincCode)) Then
                ShowtestDeatails(PatientID, OrderIdForCode, TestIdForCode)
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub MenuItemRemoveSNOMED_Clicked(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent RemoveCodification(OrderIdForCode, TestIdForCode, ResultNameForCode, "SNOMED")
            ShowtestDeatails(PatientID, OrderIdForCode, TestIdForCode)
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub MenuItemRemoveICD_Clicked(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent RemoveCodification(OrderIdForCode, TestIdForCode, ResultNameForCode, mnu_R_ICD.Header.ToString())
            ShowtestDeatails(PatientID, OrderIdForCode, TestIdForCode)
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub MenuItemRemoveLOINC_Clicked(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            RaiseEvent RemoveCodification(OrderIdForCode, TestIdForCode, ResultNameForCode, "LOINC")
            ShowtestDeatails(PatientID, OrderIdForCode, TestIdForCode)
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    Private Sub lstTestDetails_ContextMenuOpening(sender As Object, e As System.Windows.Controls.ContextMenuEventArgs) Handles lstTestDetails.ContextMenuOpening

        Try
            If e.Source.GetType.Name = "TextBlock" Then

                If Not String.IsNullOrEmpty((DirectCast(e.Source, TextBlock)).Tag) Then
                    ResultNameForCode = (DirectCast(e.Source, TextBlock)).Text
                    TestIdForCode = (DirectCast(e.Source, TextBlock)).Tag

                    mnu_S_SNOMED.Visibility = Windows.Visibility.Collapsed
                    mnu_S_ICD.Visibility = Windows.Visibility.Collapsed
                    mnu_S_LOINC.Visibility = Windows.Visibility.Collapsed

                    mnu_V_SNOMED.Visibility = Windows.Visibility.Collapsed
                    mnu_V_ICD.Visibility = Windows.Visibility.Collapsed
                    mnu_V_LOINC.Visibility = Windows.Visibility.Collapsed

                    mnu_R_SNOMED.Visibility = Windows.Visibility.Collapsed
                    mnu_R_ICD.Visibility = Windows.Visibility.Collapsed
                    mnu_R_LOINC.Visibility = Windows.Visibility.Collapsed

                    mnu_SelectCode.Visibility = Windows.Visibility.Collapsed
                    mnu_ViewCode.Visibility = Windows.Visibility.Collapsed
                    mnu_RemoveCode.Visibility = Windows.Visibility.Collapsed


                    RaiseEvent ViewCodification(OrderIdForCode, TestIdForCode, ResultNameForCode, "")

                    If String.IsNullOrEmpty(SnomedCode) And String.IsNullOrEmpty(ICDCode) And String.IsNullOrEmpty(LoincCode) Then
                        mnu_SelectCode.Visibility = Windows.Visibility.Visible
                        mnu_S_SNOMED.Visibility = Windows.Visibility.Visible
                        mnu_S_ICD.Visibility = Windows.Visibility.Visible
                        mnu_S_LOINC.Visibility = Windows.Visibility.Visible
                    ElseIf Not String.IsNullOrEmpty(SnomedCode) And Not String.IsNullOrEmpty(ICDCode) And Not String.IsNullOrEmpty(LoincCode) Then
                        mnu_ViewCode.Visibility = Windows.Visibility.Visible
                        mnu_RemoveCode.Visibility = Windows.Visibility.Visible

                        mnu_V_SNOMED.Visibility = Windows.Visibility.Visible
                        mnu_V_ICD.Visibility = Windows.Visibility.Visible
                        mnu_V_LOINC.Visibility = Windows.Visibility.Visible

                        mnu_R_SNOMED.Visibility = Windows.Visibility.Visible
                        mnu_R_ICD.Visibility = Windows.Visibility.Visible
                        mnu_R_LOINC.Visibility = Windows.Visibility.Visible

                        mnu_V_SNOMED.Header = "SNOMED CT : " & SnomedCode
                        mnu_R_SNOMED.Header = "SNOMED CT : " & SnomedCode
                        mnu_V_ICD.Header = "ICD " & ICDType & " : " & ICDCode
                        mnu_R_ICD.Header = "ICD " & ICDType & " : " & ICDCode
                        mnu_V_LOINC.Header = "LOINC : " & LoincCode
                        mnu_R_LOINC.Header = "LOINC : " & LoincCode
                    Else

                        If Not String.IsNullOrEmpty(SnomedCode) Then
                            mnu_V_SNOMED.Header = "SNOMED CT : " & SnomedCode
                            mnu_R_SNOMED.Header = "SNOMED CT : " & SnomedCode

                            mnu_ViewCode.Visibility = Windows.Visibility.Visible
                            mnu_RemoveCode.Visibility = Windows.Visibility.Visible
                            mnu_SelectCode.Visibility = Windows.Visibility.Visible

                            mnu_V_SNOMED.Visibility = Windows.Visibility.Visible
                            mnu_R_SNOMED.Visibility = Windows.Visibility.Visible

                        Else
                            mnu_V_SNOMED.Header = "SNOMED CT"
                            mnu_R_SNOMED.Header = "SNOMED CT"
                            mnu_S_SNOMED.Header = "SNOMED CT"

                            mnu_S_SNOMED.Visibility = Windows.Visibility.Visible
                        End If

                        If Not String.IsNullOrEmpty(ICDCode) Then
                            mnu_V_ICD.Header = "ICD " & ICDType & " : " & ICDCode
                            mnu_R_ICD.Header = "ICD " & ICDType & " : " & ICDCode

                            mnu_ViewCode.Visibility = Windows.Visibility.Visible
                            mnu_RemoveCode.Visibility = Windows.Visibility.Visible
                            mnu_SelectCode.Visibility = Windows.Visibility.Visible

                            mnu_V_ICD.Visibility = Windows.Visibility.Visible
                            mnu_R_ICD.Visibility = Windows.Visibility.Visible
                        Else
                            mnu_V_ICD.Header = "ICD"
                            mnu_R_ICD.Header = "ICD"
                            mnu_S_ICD.Header = "ICD"

                            mnu_S_ICD.Visibility = Windows.Visibility.Visible
                        End If

                        If Not String.IsNullOrEmpty(LoincCode) Then
                            mnu_V_LOINC.Header = "LOINC : " & LoincCode
                            mnu_R_LOINC.Header = "LOINC : " & LoincCode

                            mnu_ViewCode.Visibility = Windows.Visibility.Visible
                            mnu_RemoveCode.Visibility = Windows.Visibility.Visible
                            mnu_SelectCode.Visibility = Windows.Visibility.Visible

                            mnu_V_LOINC.Visibility = Windows.Visibility.Visible
                            mnu_R_LOINC.Visibility = Windows.Visibility.Visible
                        Else
                            mnu_V_LOINC.Header = "LOINC"
                            mnu_R_LOINC.Header = "LOINC"
                            mnu_S_LOINC.Header = "LOINC"

                            mnu_S_LOINC.Visibility = Windows.Visibility.Visible
                        End If

                    End If
                Else
                    MessageBox.Show("Right click on result name for codification.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    e.Handled = True
                End If
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        End Try

    End Sub

#End Region

End Class
