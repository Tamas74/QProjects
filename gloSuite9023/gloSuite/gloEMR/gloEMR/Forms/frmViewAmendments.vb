Public Class frmViewAmendments

    Dim _nPatientId As Int64
    Public gstrPatientCode As String
    Public gstrPatientName As String
    Dim dtData As DataTable

#Region " Constructor "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _nPatientId = PatientID
        If _nPatientId <= 0 Then
            tsbtn_NewAmendment.Visible = False
        End If
        'Add any initialization after the InitializeComponent() call

    End Sub

#End Region

#Region " Form Events "

    Private Sub frmViewAmendments_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If _nPatientId > 0 Then
            Me.Text = "View Health Record Amendments for Patient -" + gstrPatientName + " ( " + gstrPatientCode + " )"
        Else
            Me.Text = "View All Health Record Amendment Requests"
        End If
        RefreshGrid()
    End Sub

#End Region

#Region " Toolstrip button click events "

    Private Sub tsbtn_NewAmendment_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_NewAmendment.Click
        Try
            Dim ofrmAmendmentsSetup As New frmAmendmentsSetup(_nPatientId, 0)
            ofrmAmendmentsSetup.WindowState = FormWindowState.Normal
            ofrmAmendmentsSetup.StartPosition = FormStartPosition.CenterScreen
            ofrmAmendmentsSetup.ShowInTaskbar = False
            ofrmAmendmentsSetup.ShowDialog(IIf(IsNothing(ofrmAmendmentsSetup.Parent), Me, ofrmAmendmentsSetup.Parent))
            Me.Cursor = Cursors.Arrow
            ofrmAmendmentsSetup.Dispose()
            RefreshGrid()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try

    End Sub

    Private Sub tsbtn_ModifyAmendment_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_ModifyAmendment.Click

        Try
            With c1Amendments
                If .Rows.Count > 1 Then
                    Dim _AmedmentsID As Long
                    Dim _PatientID As Long
                    If .Row > 0 Then
                        _AmedmentsID = .GetData(.Row, 0)
                        _PatientID = .GetData(.Row, 1)

                        If _AmedmentsID > 0 And _PatientID > 0 Then
                            Me.Cursor = Cursors.WaitCursor
                            Dim oAmendmentsSetup As New frmAmendmentsSetup(_PatientID, _AmedmentsID)
                            oAmendmentsSetup.WindowState = FormWindowState.Normal
                            oAmendmentsSetup.StartPosition = FormStartPosition.CenterScreen
                            oAmendmentsSetup.ShowInTaskbar = False
                            oAmendmentsSetup.ShowDialog(IIf(IsNothing(oAmendmentsSetup.Parent), Me, oAmendmentsSetup.Parent))
                            Me.Cursor = Cursors.Arrow
                            oAmendmentsSetup.Dispose()
                            RefreshGrid()
                            txtSearch_TextChanged(sender, e)
                        Else
                            MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try
        
    End Sub

    Private Sub tsbtn_DeleteAmendment_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_DeleteAmendment.Click

        With c1Amendments
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _PatientID As Long
                ' Dim _EditName As String
                If .Row > 0 Then
                    _ID = .GetData(.Row, 0)
                    _PatientID = .GetData(.Row, 1)
                    Dim oClsAmendments As New ClsAmendments(_PatientID, _ID)
                    If MessageBox.Show("Are you sure you want to delete the current record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        oClsAmendments.DeleteAmedments()
                        RefreshGrid()
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub tsbtn_RefreshAmendment_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_RefreshAmendment.Click
        Try
            RefreshGrid()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.RefreshViewAmedments, gloAuditTrail.ActivityType.View, "View Amedments Screen Refreshed", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.RefreshViewAmedments, gloAuditTrail.ActivityType.View, "View Amedments Screen Refreshed", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

        End Try

    End Sub

    Private Sub tsbtn_CloseViewAmendment_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_CloseViewAmendment.Click
        Me.Close()
    End Sub

    Private Sub RefreshGrid()
        Dim oClsAmendments As New ClsAmendments(_nPatientId, 0)

        Try
            dtData = oClsAmendments.GetPatientAmedments(_nPatientId)
            If CmbAmedmendStatus.SelectedItem IsNot Nothing Then
                CmbAmedmendStatus.SelectedItem = ""
            End If
            'c1Amendments.Clear()
            c1Amendments.DataSource = Nothing
            c1Amendments.BeginUpdate()
            c1Amendments.DataSource = dtData.DefaultView
            c1Amendments.EndUpdate()
            DesignGrid()
            txtSearch.Text = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oClsAmendments IsNot Nothing Then
                oClsAmendments = Nothing
            End If
        End Try
       
    End Sub

    Private Sub c1Amendments_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1Amendments.DoubleClick
        Try
            With c1Amendments
                If .Rows.Count > 1 Then
                    Dim _AmedmentsID As Long
                    Dim _PatientID As Long
                    If .Row > 0 Then
                        _AmedmentsID = .GetData(.Row, 0)
                        _PatientID = .GetData(.Row, 1)

                        If _AmedmentsID > 0 And _PatientID > 0 Then
                            Me.Cursor = Cursors.WaitCursor
                            Dim oAmendmentsSetup As New frmAmendmentsSetup(_PatientID, _AmedmentsID)
                            oAmendmentsSetup.WindowState = FormWindowState.Normal
                            oAmendmentsSetup.StartPosition = FormStartPosition.CenterScreen
                            oAmendmentsSetup.ShowInTaskbar = False
                            oAmendmentsSetup.ShowDialog(IIf(IsNothing(oAmendmentsSetup.Parent), Me, oAmendmentsSetup.Parent))
                            Me.Cursor = Cursors.Arrow
                            oAmendmentsSetup.Dispose()
                            RefreshGrid()
                            txtSearch_TextChanged(sender, e)
                        Else
                            MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
       
        End Try
    End Sub
#End Region

#Region " Private & Public methods "
    Public Function DesignGrid()

        With c1Amendments

            For i As Int16 = 0 To .Cols.Count - 1
                .Cols(i).AllowEditing = False
                .Cols(i).AllowSorting = True
            Next



        End With
        c1Amendments.Cols(0).Visible = False
        c1Amendments.Cols(1).Visible = False
        c1Amendments.Cols(4).Format = "MM/dd/yyyy hh:mm:tt"
        c1Amendments.Cols(10).Format = "MM/dd/yyyy hh:mm:tt"
        c1Amendments.Cols(2).Width = 150
        c1Amendments.Cols(3).Width = 150
        c1Amendments.Cols(4).Width = 150
        c1Amendments.Cols(5).Width = 110
        c1Amendments.Cols(6).Width = 110
        c1Amendments.Cols(7).Width = 115
        c1Amendments.Cols(8).Width = 200
        c1Amendments.Cols(9).Width = 150
        c1Amendments.Cols(10).Width = 150
        Return Nothing
    End Function

#End Region
 
    Private Sub txtSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearch.TextChanged, CmbAmedmendStatus.SelectedIndexChanged
        Try
            Dim strSearch, sFilter As String

            With txtSearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With
            'Dim _dt As DataTable
            '_dt = dtData.Copy()

            Dim dvAmedments As DataView
            dvAmedments = Nothing
            dvAmedments = CType(c1Amendments.DataSource, DataView)
            If (IsNothing(dvAmedments) = False) Then


                sFilter = "([" & dvAmedments.Table.Columns(5).ColumnName & "]" & " Like '%" & strSearch.Trim.Replace("'", "''") & "%' OR " & "[" & dvAmedments.Table.Columns(6).ColumnName & "]" & " Like '%" & strSearch.Trim.Replace("'", "''") & "%'  OR " & "[" & dvAmedments.Table.Columns(2).ColumnName & "]" & " Like '%" & strSearch.Trim.Replace("'", "''") & "%' " & ") "
                If CmbAmedmendStatus.Text.Length > 0 And strSearch <> "" Then
                    sFilter = sFilter + " AND " + "[" & dvAmedments.Table.Columns(5).ColumnName & "]" & " LIKE '%" & CmbAmedmendStatus.Text.Trim.Replace("'", "''") & "%'"
                    dvAmedments.RowFilter = sFilter
                ElseIf CmbAmedmendStatus.Text.Length Then
                    dvAmedments.RowFilter = "[" & dvAmedments.Table.Columns(5).ColumnName & "]" & " LIKE '%" & CmbAmedmendStatus.Text.Trim.Replace("'", "''") & "%'"
                Else
                    dvAmedments.RowFilter = sFilter
                End If
            End If

            c1Amendments.BeginUpdate()
            'c1Amendments.Clear()
            c1Amendments.DataSource = Nothing
            c1Amendments.DataSource = dvAmedments
            c1Amendments.EndUpdate()
            DesignGrid()


        Catch ex As Exception

            If ex.Message.Contains("Error in Like operator") = True Then
                MessageBox.Show("Invalid search criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show(ex.Message)
            End If

        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.Query, gloAuditTrail.ActivityType.Query, "Amedments Query """ + txtSearch.Text + """", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

    End Sub
    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        txtSearch.Text = ""
    End Sub

End Class