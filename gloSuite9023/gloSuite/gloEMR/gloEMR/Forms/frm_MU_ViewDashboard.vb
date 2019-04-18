#Region "Comment"
'Created By  : Shubhangi Gujar
'Created Date: 20100907
'Purpose     : View Form of MUDashboard.
#End Region

Public Class frm_MU_ViewDashboard

    'Declaration of private variables
#Region "Private Variables"

    Dim _GridWidth As Int32
    Dim strsortorder As String
    Dim _PatientID As Long
    Dim _blnSearch As Boolean = True
    Dim objclsMeasures As New cls_MU_Measures
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim sortOrder As String
    Dim strSearchstring As String

#End Region

    Private Sub frmViewMUDashboard_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewMUDashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtSearch.Select()
            grdMUDashboard.Enabled = False
            Dim dv As DataView
            ' Dim dt As New DataTable
            dv = objclsMeasures.FillMUMeasures()
            grdMUDashboard.DataSource = dv

            grdMUDashboard.Enabled = True
            CustomGridStyle()
            '  Dim i As Int64
            'For i = 0 To grdMUDashboard.VisibleRowCount - 1
            '    dv.Table.Columns(6).ColumnName.Format("mm/dd/yyyy")
            'Next
          

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Call RefreshMeasure()
            Case "Close"
                Call Me.Close()
        End Select
    End Sub

    Private Sub AddMeasure()
        Try
            Dim objfrmMeasures As New frm_MU_Dashboard()
            ' Try
            '  blnModify = False

            With objfrmMeasures
                .Text = "Meaningful Use"
                .MdiParent = Me.MdiParent
                ' .MyCaller = Me
                ' .MdiParent = Me.ParentForm
                ' CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                .ShowDialog(IIf(IsNothing(objfrmMeasures.Parent), Me, objfrmMeasures.Parent))
                .WindowState = FormWindowState.Maximized
                .BringToFront()
                .ShowInTaskbar = False
            End With


            grdMUDashboard.Enabled = False
            Dim dv As DataView
            dv = objclsMeasures.FillMUMeasures()
            grdMUDashboard.DataSource = dv
            grdMUDashboard.Enabled = True
            objfrmMeasures.Dispose()

            'If (grdMUDashboard.VisibleRowCount > 0) Then
            '    grdMUDashboard.CurrentRowIndex = 0
            '    grdMUDashboard.Select(0)
            'End If

            'CODE FOR SELECT THE ROW ADDED NEWLY 
            ' Dim objfrmMU As New frm_MU_Reports
            'Dim i As Integer
            'For i = 0 To CType(grdMUDashboard.DataSource, DataView).Table.Rows.Count - 1
            '    '''' when ID Found select that matching Row
            '    If objfrmMU._ReportName = grdMUDashboard.Item(i, 1) Then
            '        grdMUDashboard.CurrentRowIndex = i
            '        grdMUDashboard.Select(i)
            '        Exit For
            '    End If
            'Next


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Sub UpdateMeasure()

        Dim ID As Long
        Dim StartDate As Date
        Dim objfrmMU As frm_MU_Dashboard

        Try

            If grdMUDashboard.VisibleRowCount >= 1 Then

                ID = grdMUDashboard.Item(grdMUDashboard.CurrentRowIndex, 0).ToString
                StartDate = grdMUDashboard.Item(grdMUDashboard.CurrentRowIndex, 6).ToString


                ' '' <><><> Record Level Locking <><><><> 
                'Dim blnRecordLock As Boolean = False
                'If gblnRecordLocking = True Then
                '    Dim mydt As New mytable
                '    mydt = Scan_n_Lock_Transaction(TrnType.MUReport, ID, 0, StartDate)
                '    If mydt.Description <> gstrClientMachineName Then
                '        If MessageBox.Show("This Report is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            blnRecordLock = True
                '        Else
                '            Exit Sub
                '        End If
                '    End If

                'End If

                Dim grdIndex As Integer = grdMUDashboard.CurrentRowIndex
                objfrmMU = New frm_MU_Dashboard(ID)
                'objfrmMU.ShowDialog(Me)
                Dim myDataView As DataView = CType(grdMUDashboard.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    sortOrder = myDataView.Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)

                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                    ''''''''''''''''''''''
                End If
                With objfrmMU
                    .Text = "Modify Meaningful Use"
                    '.MdiParent = Me.ParentForm
                    .MdiParent = Me.ParentForm
                    .ShowInTaskbar = False
                    '.IsModify = True
                    ' CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    '.MyCaller = Me
                    .ShowDialog(IIf(IsNothing(objfrmMU.Parent), Me, objfrmMU.Parent))
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With
                objfrmMU.Dispose()
            End If
            grdMUDashboard.Enabled = False
            Dim dv As DataView
            dv = objclsMeasures.FillMUMeasures()
            grdMUDashboard.DataSource = dv
            grdMUDashboard.Enabled = True



        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True


        Finally
            objfrmMU = Nothing
        End Try
    End Sub
    Private Sub DeleteMeasure()
        Try
            Dim ID As Long
            Dim StartDate As Date
            If grdMUDashboard.VisibleRowCount >= 1 Then

                If grdMUDashboard.IsSelected(grdMUDashboard.CurrentRowIndex) = False Then
                    Exit Sub
                End If

                'blnModify = True
                ID = grdMUDashboard.Item(grdMUDashboard.CurrentRowIndex, 0).ToString
                StartDate = grdMUDashboard.Item(grdMUDashboard.CurrentRowIndex, 6).ToString

                '' '' <><><> Record Level Locking <><><><> 
                'Dim blnRecordLock As Boolean = False
                'If gblnRecordLocking = True Then
                '    Dim mydt As New mytable
                '    mydt = Scan_n_Lock_Transaction(TrnType.MUReport, ID, 0, StartDate)
                '    If mydt.Description <> gstrClientMachineName Then
                '        MessageBox.Show("This Report is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End If
                '''' <><><> Record Level Locking <><><><> 
                If MessageBox.Show("Do you want to delete selected Report?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    objclsMeasures.DeleteMeasures(ID)
                    txtSearch.ResetText()

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.Delete, "Report deleted", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    grdMUDashboard.Enabled = False
                    Dim dv As DataView
                    '     Dim dt As New DataTable
                    dv = objclsMeasures.FillMUMeasures()

                    grdMUDashboard.DataSource = dv
                    grdMUDashboard.Enabled = True
                    grdMUDashboard.Enabled = True
                    Dim myDataView As DataView = CType(grdMUDashboard.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort

                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                    End If

                End If
            End If

            If (grdMUDashboard.VisibleRowCount > 0) Then
                grdMUDashboard.CurrentRowIndex = 0
                grdMUDashboard.Select(0)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshMeasure()
        Try
            grdMUDashboard.Enabled = False
            grdMUDashboard.DataSource = objclsMeasures.FillMUMeasures()
            grdMUDashboard.Enabled = True
            CustomGridStyle()
            If grdMUDashboard.VisibleRowCount > 0 Then
                grdMUDashboard.CurrentRowIndex = 0
                grdMUDashboard.Select(0)
            End If
            txtSearch.Text = ""
            txtSearch.Focus()
            _blnSearch = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try
            Dim dv As DataView
            dv = objclsMeasures.GetDataView
            Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

            Dim nID As New DataGridTextBoxColumn
            With nID
                .Width = 0
                .MappingName = dv.Table.Columns(0).ColumnName
                .HeaderText = "nID"
            End With

            Dim ReportingName As New DataGridTextBoxColumn
            With ReportingName
                .Width = 0.2 * _GridWidth
                .MappingName = dv.Table.Columns(1).ColumnName
                .HeaderText = "Report Name"
            End With

            Dim nProviderID As New DataGridTextBoxColumn
            With nProviderID
                .Width = 0.0 * _GridWidth
                .MappingName = dv.Table.Columns(2).ColumnName
                .HeaderText = "nProviderID"
                .NullText = ""
            End With

            Dim ProviderName As New DataGridTextBoxColumn
            With ProviderName
                .Width = 0.2 * _GridWidth
                .MappingName = dv.Table.Columns(3).ColumnName
                .HeaderText = "Provider Name"
                .NullText = ""
            End With

            Dim ReportingYear As New DataGridTextBoxColumn
            With ReportingYear
                .Width = 0.175 * _GridWidth
                .MappingName = dv.Table.Columns(4).ColumnName
                .HeaderText = "Reporting Year"
                .NullText = ""
            End With

            Dim blnIsFirstYear As New DataGridTextBoxColumn
            With blnIsFirstYear
                .Width = 0.0 * _GridWidth
                .MappingName = dv.Table.Columns(5).ColumnName
                .HeaderText = "blnIsFirstYear"
                .NullText = ""
            End With


            Dim StartDate As New DataGridTextBoxColumn
            With StartDate
                .Width = CInt(0.175 * _GridWidth)
                .MappingName = dv.Table.Columns(6).ColumnName
                .HeaderText = "Start Date"
                .NullText = ""
                '.Format = Format(StartDate, "mm/dd/yyyy")
            End With


            Dim EndDate As New DataGridTextBoxColumn
            With EndDate
                .Width = CInt(0.175 * _GridWidth)
                .MappingName = dv.Table.Columns(7).ColumnName
                .HeaderText = "End Date"
                .NullText = ""
                '.Format = Format(EndDate, "mm/dd/yyyy")
                '.Format = "mm/dd/yyyy"
            End With

            Dim ReporingStatus As New DataGridTextBoxColumn
            With ReporingStatus
                .Width = 0.0 * _GridWidth
                .MappingName = dv.Table.Columns(8).ColumnName
                .HeaderText = "Reporing Status"
                .NullText = ""
            End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {nID, ReportingName, nProviderID, ProviderName, ReportingYear, blnIsFirstYear, StartDate, EndDate, ReporingStatus})
            grdMUDashboard.TableStyles.Clear()
            grdMUDashboard.TableStyles.Add(ts)

            txtSearch.Text = ""
            txtSearch.Text = strsearchtxt
            If IsNothing(strcolumnName) OrElse strcolumnName = "" Then
                '  dv.Sort = "[" & dv.Table.Columns(3).ColumnName & "]" & strsortorder
                ' dv.Sort = "[" & dv.Table.Columns(1).ColumnName & "]" & strsortorder
            Else
                Dim strColumn As String = Replace(strcolumnName, "[", "")
                dv.Sort = "[" & strColumn & "]" & strSortBy
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmViewMUDashboard_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            _GridWidth = grdMUDashboard.Width
            CustomGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdMUDashboard.CurrentRowIndex >= 0 Then
                    grdMUDashboard.Select(0)
                    grdMUDashboard.CurrentRowIndex = 0
                End If
            End If
            mdlGeneral.ValidateText(txtSearch.Text, e)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvMeasures As DataView
                dvMeasures = CType(grdMUDashboard.DataSource(), DataView)
                If IsNothing(dvMeasures) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                grdMUDashboard.Enabled = False
                grdMUDashboard.DataSource = dvMeasures
                grdMUDashboard.Enabled = True
                Dim strMeasureSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strMeasureSearchDetails = Replace(txtSearch.Text, "'", "''")

                    strMeasureSearchDetails = Replace(strMeasureSearchDetails, "[", "") & ""
                    strMeasureSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strMeasureSearchDetails)
                Else
                    strMeasureSearchDetails = ""
                End If
                Dim strSearch As String = txtSearch.Text.Trim()

                If Val(strMeasureSearchDetails) > 0 Then
                    dvMeasures.RowFilter = dvMeasures.Table.Columns(1).ColumnName & " Like '%" & strMeasureSearchDetails & "%' OR " _
                                          & dvMeasures.Table.Columns(3).ColumnName & " Like '%" & strMeasureSearchDetails & "%' OR " _
                                          & dvMeasures.Table.Columns(4).ColumnName & " = " & strMeasureSearchDetails
                Else
                    dvMeasures.RowFilter = dvMeasures.Table.Columns(1).ColumnName & " Like '%" & strMeasureSearchDetails & "%' OR " _
                                          & dvMeasures.Table.Columns(3).ColumnName & " Like '%" & strMeasureSearchDetails & "%' "
                End If

                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class