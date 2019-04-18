Imports C1.Win.C1FlexGrid
Imports System.Windows.Forms
Imports System.Drawing

Public Class frm_ViewStage3

    'Declaration of private v Private Col_ICD9Code As Integer = 1ariables
#Region "Private Variables"
    Dim dv As DataView
    Dim _GridWidth As Int32
    Dim strsortorder As String
    Dim _PatientID As Long
    Dim _blnSearch As Boolean = True
    Dim objclsMeasures As New cls_MU_Measures_Stage3
    Dim objclsgeneral As New cls_MU_General
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _databaseConnectionString As String = String.Empty
    Private _MessageBoxCaption As String = String.Empty

#End Region
    Private Col_ID As Integer = 0
    Private Col_REPORTNAME As Integer = 1
    Private Col_PROVIDERID As Integer = 2
    Private Col_PROVIDERNAME As Integer = 3
    Private Col_REPORTINGYEAR As Integer = 4
    Private Col_ISFIRSTYEAR As Integer = 5
    Private Col_STARTDATE As Integer = 6
    Private Col_ENDDATE As Integer = 7
    Private Col_REPORTINGPERIODSTATUS As Integer = 8

    Private Sub frmViewMUDashboard_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewMUDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtSearch.Select()
            Dim dv As New DataView
            Dim dt As New DataTable
            CustomGridStyle()
            dv = objclsMeasures.FillMUMeasures_Stage3()

            C1MUDashboard.DataSource = dv

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        'Manipulation buttons for view Meaningful Use 
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddMeasure()
            Case "Modify"
                Call UpdateMeasure()
            Case "Delete"
                Call DeleteMeasure()
            Case "Refresh"

                Call RefreshMeasure(0)

            Case "Close"
                Call Me.Close()
        End Select
    End Sub

    Private Sub AddMeasure()
        Try
            Dim objfrmMeasures As New gloMU.frm_Stage3()
            ' Dim ID As Int64

            With objfrmMeasures
                .Text = "MU Stage 3"
                .mycaller = Me
                .MdiParent = Me.MdiParent
                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()
                .ShowInTaskbar = False
            End With

            AddHandler objfrmMeasures.FormClosed, AddressOf On_frmMU_Closed

            '  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "Report Added", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateMeasure()

        Dim ID As Long
        Dim StartDate As Date
        Dim objfrmMU As frm_Stage3

        Try
            If C1MUDashboard.Row >= 1 Then
                ID = C1MUDashboard.GetData(C1MUDashboard.Row, Col_ID)
                StartDate = C1MUDashboard.GetData(C1MUDashboard.Row, Col_STARTDATE)

                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
                ''''''''''''''''''''''
                objfrmMU = New frm_Stage3(ID)

                With objfrmMU
                    .Text = "MU Stage 3 2017+"
                    .mycaller = Me
                    .MdiParent = Me.MdiParent
                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                    .ShowInTaskbar = False
                End With
                AddHandler objfrmMU.FormClosed, AddressOf On_frmMU_Closed
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            objfrmMU = Nothing
        End Try

    End Sub
    Private Sub DeleteMeasure()
        Try
            Dim ID As Long
            Dim StartDate As Date
            If C1MUDashboard.Row >= 1 Then

                ID = C1MUDashboard.GetData(C1MUDashboard.Row, Col_ID)
                StartDate = C1MUDashboard.GetData(C1MUDashboard.Row, Col_STARTDATE)

                If MessageBox.Show("Do you want to delete selected Report?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    objclsMeasures.DeleteMeasures_Stage3(ID)
                    txtSearch.ResetText()

                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.Delete, "Report deleted", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.Delete, "Stage 2 Modified 2015+ dashboard with the name '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_REPORTNAME)) & "' for the Provider '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_PROVIDERNAME)) & "' with the measurement period From '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_STARTDATE)) & "' to '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_ENDDATE)) & "' deleted.", 0, ID, 0, gloAuditTrail.ActivityOutCome.Success)

                    C1MUDashboard.Enabled = False
                    C1MUDashboard.DataSource = objclsMeasures.FillMUMeasures_Stage3()
                    C1MUDashboard.Enabled = True
                    CustomGridStyle()

                    txtSearch.Text = ""
                    txtSearch.Focus()
                    _blnSearch = True
                    ''
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                End If
            End If

            If (grdMUDashboard.VisibleRowCount > 0) Then
                grdMUDashboard.CurrentRowIndex = 0
                grdMUDashboard.Select(0)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub RefreshMeasure(ByVal ID As Int64)
        Try
            C1MUDashboard.Enabled = False
            C1MUDashboard.DataSource = objclsMeasures.FillMUMeasures_Stage3()
            C1MUDashboard.Enabled = True
            CustomGridStyle()

            txtSearch.Text = ""
            txtSearch.Focus()
            _blnSearch = True
            ''CODE FOR SELECT THE ROW ADDED NEWLY 
            Dim rowIndex As Int64
            Dim myDataView As DataView = CType(C1MUDashboard.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then

                Dim i As Integer
                For i = 1 To myDataView.Table.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If ID = C1MUDashboard.Item(i, 0) Then
                        Dim nID As Int64 = ID
                        rowIndex = C1MUDashboard.FindRow(nID, 1, 0, False, True, False)
                        C1MUDashboard.Select(rowIndex, 0, True)
                        Exit For
                    End If
                Next

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub CustomGridStyle()

        C1MUDashboard.Cols.Count = 9
        gloC1FlexStyle.Style(C1MUDashboard)

        C1MUDashboard.SetData(0, Col_ID, "ID")
        C1MUDashboard.SetData(0, Col_REPORTNAME, "Report Name")
        C1MUDashboard.SetData(0, Col_PROVIDERID, "Provider Id")
        C1MUDashboard.SetData(0, Col_PROVIDERNAME, "Provider Name")
        C1MUDashboard.SetData(0, Col_REPORTINGYEAR, "Reporting Year")
        C1MUDashboard.SetData(0, Col_ISFIRSTYEAR, "Is First Year")
        C1MUDashboard.SetData(0, Col_STARTDATE, "Start Date")
        C1MUDashboard.SetData(0, Col_ENDDATE, "End Date")
        C1MUDashboard.SetData(0, Col_REPORTINGPERIODSTATUS, "Reporting Period Status")


        C1MUDashboard.Cols(Col_ID).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_REPORTNAME).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_PROVIDERID).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_PROVIDERNAME).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_REPORTINGYEAR).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_ISFIRSTYEAR).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_STARTDATE).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_ENDDATE).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1MUDashboard.Cols(Col_REPORTINGPERIODSTATUS).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        ' ''Width
        C1MUDashboard.Cols(Col_ID).Width = _GridWidth * 0.0
        C1MUDashboard.Cols(Col_REPORTNAME).Width = _GridWidth * 0.2
        C1MUDashboard.Cols(Col_PROVIDERID).Width = _GridWidth * 0.0
        C1MUDashboard.Cols(Col_PROVIDERNAME).Width = _GridWidth * 0.2
        C1MUDashboard.Cols(Col_REPORTINGYEAR).Width = _GridWidth * 0.175
        C1MUDashboard.Cols(Col_ISFIRSTYEAR).Width = _GridWidth * 0.0
        C1MUDashboard.Cols(Col_STARTDATE).Width = _GridWidth * 0.175
        C1MUDashboard.Cols(Col_ENDDATE).Width = _GridWidth * 0.175
        C1MUDashboard.Cols(Col_REPORTINGPERIODSTATUS).Width = _GridWidth * 0.0


        ' ''Editing
        C1MUDashboard.Cols(Col_ID).AllowEditing = False
        C1MUDashboard.Cols(Col_REPORTNAME).AllowEditing = False
        C1MUDashboard.Cols(Col_PROVIDERID).AllowEditing = False
        C1MUDashboard.Cols(Col_PROVIDERNAME).AllowEditing = False
        C1MUDashboard.Cols(Col_REPORTINGYEAR).AllowEditing = False
        C1MUDashboard.Cols(Col_ISFIRSTYEAR).AllowEditing = False
        C1MUDashboard.Cols(Col_STARTDATE).AllowEditing = False
        C1MUDashboard.Cols(Col_ENDDATE).AllowEditing = False
        C1MUDashboard.Cols(Col_REPORTINGPERIODSTATUS).AllowEditing = True
        C1MUDashboard.Cols(Col_REPORTINGYEAR).DataType = GetType(String)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmViewMUDashboard_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            _GridWidth = C1MUDashboard.Width
            CustomGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If C1MUDashboard.Rows.Count >= 0 Then
                    C1MUDashboard.Select()
                End If
            End If
            objclsgeneral.ValidateText(txtSearch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvMeasures As DataView
                dvMeasures = CType(C1MUDashboard.DataSource(), DataView)
                If IsNothing(dvMeasures) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                C1MUDashboard.DataSource = dvMeasures
                CustomGridStyle()
                Dim strMeasureSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strMeasureSearchDetails = Replace(txtSearch.Text, "'", "''")

                    strMeasureSearchDetails = Replace(strMeasureSearchDetails, "[", "") & ""
                    strMeasureSearchDetails = objclsgeneral.ReplaceSpecialCharacters(strMeasureSearchDetails)
                Else
                    strMeasureSearchDetails = ""
                End If
                Dim strSearch As String = txtSearch.Text.Trim()

                If Val(strMeasureSearchDetails) > 0 Then
                    dvMeasures.RowFilter = dvMeasures.Table.Columns(1).ColumnName & " Like '%" & strMeasureSearchDetails & "%' OR " _
                                          & dvMeasures.Table.Columns(3).ColumnName & " Like '%" & strMeasureSearchDetails & "%' OR " _
                                          & dvMeasures.Table.Columns(4).ColumnName & " Like '%" & strMeasureSearchDetails & "%' "
                Else
                    dvMeasures.RowFilter = dvMeasures.Table.Columns(1).ColumnName & " Like '%" & strMeasureSearchDetails & "%' OR " _
                                          & dvMeasures.Table.Columns(3).ColumnName & " Like '%" & strMeasureSearchDetails & "%' "
                End If
                Me.Cursor = Cursors.Default
                C1MUDashboard.DataSource = dvMeasures
                CustomGridStyle()
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub grdMUDashboard_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMUDashboard.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdMUDashboard.HitTest(ptPoint)

            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
                '''''''''''''''''''''''''''''''''''''
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                UpdateMeasure()
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdMUDashboard_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMUDashboard.MouseUp
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdMUDashboard.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                grdMUDashboard.Select(htInfo.Row)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If

    End Sub

    Private Sub C1MUDashboard_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1MUDashboard.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As HitTestInfo = C1MUDashboard.HitTest(ptPoint)

            If htInfo.Type = HitTestTypeEnum.ColumnHeader Then
                If txtSearch.Text = "" Then

                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = HitTestTypeEnum.Cell Then
                _blnSearch = True
                UpdateMeasure()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1MUDashboard_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1MUDashboard.MouseUp
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As HitTestInfo = C1MUDashboard.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                'C1MUDashboard.Select(htInfo.Row)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub On_frmMU_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As gloMU.frm_Stage3 = Nothing

        Try
            frm = DirectCast(sender, gloMU.frm_Stage3)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_frmMU_Closed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class