Imports C1.Win.C1FlexGrid
Imports System.Windows.Forms
Imports System.Drawing

#Region "Comment"
'Created By  : Shubhangi Gujar
'Created Date: 20100907
'Purpose     : View Form of MUDashboard.
#End Region

Public Class frm_MU_ViewDashboard_Stage1

    'Declaration of private v Private Col_ICD9Code As Integer = 1ariables
#Region "Private Variables"
    Dim dv As DataView
    Dim _GridWidth As Int32
    Dim strsortorder As String
    Dim _PatientID As Long
    Dim _blnSearch As Boolean = True
    Dim objclsMeasures As New cls_MU_Measures
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
    Public mycaller As frm_MU_ViewDashboard





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
            'grdMUDashboard.Enabled = False
            ' C1MUDashboard.Enabled = False
            Dim dv As New DataView
            Dim dt As New DataTable
            CustomGridStyle()
            dv = objclsMeasures.FillMUMeasures_Stage1()


            'grdMUDashboard.DataSource = dv
            C1MUDashboard.DataSource = dv
            ' C1MUDashboard.Row = 1

            ' C1MUDashboard.Select(1, True)

            'grdMUDashboard.Enabled = True
            'C1MUDashboard.Enabled = True


            'For i = 0 To grdMUDashboard.VisibleRowCount - 1
            '    dv.Table.Columns(6).ColumnName.Format("mm/dd/yyyy")
            'Next


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
            Dim objfrmMeasures As New gloMU.frm_MU_Dashboard_Stage1()
            ' Dim ID As Int64

            With objfrmMeasures
                .Text = "Meaningful Use 2014 Stage 1"
                .mycaller = Me
                ' .MdiParent = Me.MdiParent
                ' .MyCaller = Me
                ' .MdiParent = Me.ParentForm
                ' CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
                .MdiParent = Me.MdiParent
                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()
                .ShowInTaskbar = False
            End With

            AddHandler objfrmMeasures.FormClosed, AddressOf On_frmMU_Closed

            'grdMUDashboard.Enabled = False
            ' C1MUDashboard.Enabled = False

            '' Code Commented --20101204 , Verify it by Shubhangi
            'dv = objclsMeasures.FillMUMeasures()
            ''grdMUDashboard.DataSource = dv
            ''grdMUDashboard.Enabled = True
            'C1MUDashboard.DataSource = dv
            'CustomGridStyle()
            '' Code Commented --20101204 , Verify it by Shubhangi


            ' ID = objfrmMeasures.

            ' 'To refresh list after performing add/update/delete operation


            '' To Select The Newly Added row in the ZIP Table
            'Dim rowIndex As Int64
            'rowIndex = C1MUDashboard.FindRow(ID, 1, 0, False, True, False)
            'C1MUDashboard.Select(rowIndex, 0, True)
            'C1MUDashboard.Enabled = True

            'If (grdMUDashboard.VisibleRowCount > 0) Then
            '    grdMUDashboard.CurrentRowIndex = 0
            '    grdMUDashboard.Select(0)
            'End If

            ' ''CODE FOR SELECT THE ROW ADDED NEWLY 
            'Dim objfrmMU As New frm_MU_Dashboard
            'Dim i As Integer
            'For i = 0 To CType(C1MUDashboard.DataSource, DataView).Table.Rows.Count - 1
            '    '''' when ID Found select that matching Row
            '    If objfrmMU.ID = C1MUDashboard.Item(i, 0) Then
            '        Dim nID As Int64 = objfrmMU.ID
            '        rowIndex = C1MUDashboard.FindRow(nID, 1, 0, False, True, False)
            '        C1MUDashboard.Select(rowIndex, 0, True)
            '        Exit For
            '    End If
            'Next

            ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "Report Added", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateMeasure()

        Dim ID As Long
        Dim StartDate As Date
        Dim objfrmMU As frm_MU_Dashboard_Stage1

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
                objfrmMU = New frm_MU_Dashboard_Stage1(ID)

                With objfrmMU
                    .Text = "Modify Meaningful Use Stage 1 2014+"
                    '.MdiParent = Me.ParentForm
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

        
                ''''' <><><> Record Level Locking <><><><> 
                If MessageBox.Show("Do you want to delete selected Report?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    objclsMeasures.DeleteMeasures_Stage1(ID)
                    txtSearch.ResetText()

                    '  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.Delete, "Report deleted", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.Delete, "Stage 1 2014+ dashboard with the name '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_REPORTNAME)) & "' for the Provider '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_PROVIDERNAME)) & "' with the measurement period From '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_STARTDATE)) & "' to '" & Convert.ToString(C1MUDashboard.GetData(C1MUDashboard.Row, Col_ENDDATE)) & "' deleted.", 0, ID, 0, gloAuditTrail.ActivityOutCome.Success)


                    Dim dv As New DataView
                    Dim dt As New DataTable
                    dv = objclsMeasures.FillMUMeasures_Stage1()
                    C1MUDashboard.DataSource = dv

                    C1MUDashboard.Enabled = False
                    C1MUDashboard.DataSource = objclsMeasures.FillMUMeasures_Stage1()
                    C1MUDashboard.Enabled = True
                    CustomGridStyle()

                    If C1MUDashboard.Rows.Count > 0 Then
                        'C1MUDashboard.CurrentRowIndex = 0
                        'C1MUDashboard.Select(0)
                    End If

                    txtSearch.Text = ""
                    txtSearch.Focus()
                    _blnSearch = True
                    ''
                    'sortOrder = CType(grdMUDashboard.DataSource, DataView).Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                    ' CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
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
            'grdMUDashboard.Enabled = False
            'grdMUDashboard.DataSource = objclsMeasures.FillMUMeasures()
            'grdMUDashboard.Enabled = True
            C1MUDashboard.Enabled = False
            C1MUDashboard.DataSource = objclsMeasures.FillMUMeasures_Stage1()
            C1MUDashboard.Enabled = True
            CustomGridStyle()
            'If grdMUDashboard.VisibleRowCount > 0 Then
            ''    grdMUDashboard.CurrentRowIndex = 0
            '    grdMUDashboard.Select(0)
            'End If
            If C1MUDashboard.Rows.Count > 0 Then
                'C1MUDashboard.CurrentRowIndex = 0
                'C1MUDashboard.Select(0)
            End If
            txtSearch.Text = ""
            txtSearch.Focus()
            _blnSearch = True
            ''CODE FOR SELECT THE ROW ADDED NEWLY 
            ''Dim objfrmMU As New frm_MU_Dashboard
            Dim rowIndex As Int64
            Dim myDataView As DataView = CType(C1MUDashboard.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                Dim i As Integer
                For i = 1 To CType(C1MUDashboard.DataSource, DataView).Table.Rows.Count - 1
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
        C1MUDashboard.Cols(Col_ID).Width =  _GridWidth * 0.0
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
    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    Try
    '        dv = objclsMeasures.GetDataView
    '        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

    '        Dim nID As New DataGridTextBoxColumn
    '        With nID
    '            .Width = 0
    '            .MappingName = dv.Table.Columns(0).ColumnName
    '            .HeaderText = "nID"
    '        End With

    '        Dim ReportingName As New DataGridTextBoxColumn
    '        With ReportingName
    '            .Width = 0.2 * _GridWidth
    '            .MappingName = dv.Table.Columns(1).ColumnName
    '            .HeaderText = "Report Name"
    '        End With

    '        Dim nProviderID As New DataGridTextBoxColumn
    '        With nProviderID
    '            .Width = 0.0 * _GridWidth
    '            .MappingName = dv.Table.Columns(2).ColumnName
    '            .HeaderText = "nProviderID"
    '            .NullText = ""
    '        End With

    '        Dim ProviderName As New DataGridTextBoxColumn
    '        With ProviderName
    '            .Width = 0.2 * _GridWidth
    '            .MappingName = dv.Table.Columns(3).ColumnName
    '            .HeaderText = "Provider Name"
    '            .NullText = ""
    '        End With

    '        Dim ReportingYear As New DataGridTextBoxColumn
    '        With ReportingYear
    '            .Width = 0.175 * _GridWidth
    '            .MappingName = dv.Table.Columns(4).ColumnName
    '            .HeaderText = "Reporting Year"
    '            .NullText = ""
    '        End With

    '        Dim blnIsFirstYear As New DataGridTextBoxColumn
    '        With blnIsFirstYear
    '            .Width = 0.0 * _GridWidth
    '            .MappingName = dv.Table.Columns(5).ColumnName
    '            .HeaderText = "blnIsFirstYear"
    '            .NullText = ""
    '        End With


    '        Dim StartDate As New DataGridTextBoxColumn
    '        With StartDate
    '            .Width = CInt(0.175 * _GridWidth)
    '            .MappingName = dv.Table.Columns(6).ColumnName
    '            .HeaderText = "Start Date"
    '            .NullText = ""
    '            '.Format = Format(StartDate, "mm/dd/yyyy")
    '        End With


    '        Dim EndDate As New DataGridTextBoxColumn
    '        With EndDate
    '            .Width = CInt(0.175 * _GridWidth)
    '            .MappingName = dv.Table.Columns(7).ColumnName
    '            .HeaderText = "End Date"
    '            .NullText = ""
    '            '.Format = Format(EndDate, "mm/dd/yyyy")
    '            '.Format = "mm/dd/yyyy"
    '        End With

    '        Dim ReporingStatus As New DataGridTextBoxColumn
    '        With ReporingStatus
    '            .Width = 0.0 * _GridWidth
    '            .MappingName = dv.Table.Columns(8).ColumnName
    '            .HeaderText = "Reporing Status"
    '            .NullText = ""
    '        End With

    '        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {nID, ReportingName, nProviderID, ProviderName, ReportingYear, blnIsFirstYear, StartDate, EndDate, ReporingStatus})
    '        grdMUDashboard.TableStyles.Clear()
    '        grdMUDashboard.TableStyles.Add(ts)

    '        txtSearch.Text = ""
    '        txtSearch.Text = strsearchtxt
    '        If strcolumnName = "" Or IsNothing(strcolumnName) Then
    '            '  dv.Sort = "[" & dv.Table.Columns(3).ColumnName & "]" & strsortorder
    '            ' dv.Sort = "[" & dv.Table.Columns(1).ColumnName & "]" & strsortorder
    '        Else
    '            Dim strColumn As String = Replace(strcolumnName, "[", "")
    '            dv.Sort = "[" & strColumn & "]" & strSortBy
    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

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
                    'grdMUDashboard.CurrentRowIndex = 0
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
                ' grdMUDashboard.Enabled = False
                C1MUDashboard.DataSource = dvMeasures
                CustomGridStyle()
                ' grdMUDashboard.Enabled = True
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
        Dim frm As gloMU.frm_MU_Dashboard_Stage1 = Nothing

        Try
            frm = DirectCast(sender, gloMU.frm_MU_Dashboard_Stage1)
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
        'For Each f As Form In Application.OpenForms
        '    If f.Name = "frm_MU_ViewDashboard" Then
        '        RefreshMeasure()

        '    End If
        'Next
        'RefreshMeasure(ID)
        ''CODE FOR SELECT THE ROW ADDED NEWLY 
        'Dim objfrmMU As New frm_MU_Dashboard
        'Dim rowIndex As Int64
        'Dim i As Integer
        'For i = 1 To CType(C1MUDashboard.DataSource, DataView).Table.Rows.Count - 1
        '    '''' when ID Found select that matching Row
        '    If objfrmMU.ID = C1MUDashboard.Item(i, 0) Then
        '        Dim nID As Int64 = objfrmMU.ID
        '        rowIndex = C1MUDashboard.FindRow(nID, 1, 0, False, True, False)
        '        C1MUDashboard.Select(rowIndex, 0, True)
        '        Exit For
        '    End If
        'Next


    End Sub
End Class