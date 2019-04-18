Imports System.Data
Imports System.Data.SqlClient

Public Class frmVWFamilyMember

    'Private objDBLayer As New ClsDBLayer
    'Dim dvMember As DataView = Nothing
    Dim dtMember As DataTable = Nothing
    Private _blnSearch As Boolean = True
    Private blnIsTextchanged As Boolean = False



 
    Private Sub frmVWFamilyMember_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Cursor = Cursors.WaitCursor
            dgMemberList.AllowSorting = True
            FetchMember()
            Me.Cursor = Cursors.Default
            txtSearch.Clear()
            _blnSearch = True
            If Not dgMemberList.VisibleRowCount = 0 Then
                dgMemberList.CurrentRowIndex = 0
                dgMemberList.Select(dgMemberList.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmVWFamilyMember_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        HideColumn()
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked

        Select Case e.ClickedItem.Tag

            Case "Add"
                Call AddFamilyMember()
            Case "Modify"
                If dgMemberList.CurrentRowIndex >= 0 Then
                    Dim strMemberId As Long = dgMemberList.Item(dgMemberList.CurrentRowIndex, 0)
                    Call ModifyFamilyMember(strMemberId)
                Else
                    MessageBox.Show("Relation not selected. Select a relation to continue.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Case "Delete"
                If dgMemberList.CurrentRowIndex >= 0 Then
                    Dim strMemberId As Long = dgMemberList.Item(dgMemberList.CurrentRowIndex, 0)
                    Call DeleteFamilyMember(strMemberId)
                Else
                    MessageBox.Show("Relation not selected. Select a relation to continue.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Case "Refresh"
                Call RefreshFamilyMember()
            Case "Close"
                Call FormClose()

        End Select

        txtSearch.Clear()
        _blnSearch = True


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Use Clear Button to Clear Search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub dgMemberList_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgMemberList.MouseUp
        Try
            If dgMemberList.CurrentRowIndex >= 0 Then
                dgMemberList.Select(dgMemberList.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgMemberList_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgMemberList.KeyUp
        Try
            If dgMemberList.CurrentRowIndex >= 0 Then
                dgMemberList.Select(dgMemberList.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgMemberList_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgMemberList.MouseDoubleClick
        Try
            ' dgCategoryList.AllowSorting = False
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgMemberList.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                Dim strMemberId As Long = dgMemberList.Item(dgMemberList.CurrentRowIndex, 0)
                ModifyFamilyMember(strMemberId)
            End If
            ' dgCategoryList.AllowSorting = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            mdlGeneral.ValidateText(txtSearch.Text, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor

                If Not blnIsTextchanged Then
                    dtMember = dgMemberList.DataSource()
                    blnIsTextchanged = True
                End If

                If IsNothing(dgMemberList.DataSource()) Then
                    dgMemberList.DataSource() = dtMember
                End If
                If (IsNothing(dgMemberList.DataSource())) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                Dim dvPatient As New DataView(dgMemberList.DataSource())
                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Dim strPatientSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''") & ""
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                    dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                        & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' " 'OR "_

                    If dvPatient.Count > 0 Then

                        Dim dtTemp As DataTable = dvPatient.ToTable()
                        dgMemberList.DataSource = dtTemp
                        dgMemberList.Select(0)
                    Else

                        dgMemberList.DataSource = Nothing
                    End If
                Else
                    strPatientSearchDetails = ""
                    dgMemberList.DataSource = dtMember
                    If dtMember.Rows.Count > 0 Then
                        dgMemberList.Select(0)
                    End If
                End If
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub


    Public Sub FetchMember()

        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Dim Conn As New System.Data.SqlClient.SqlConnection(sqlconn)
        Dim da As New System.Data.SqlClient.SqlDataAdapter
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        dgMemberList.DataSource = Nothing
        dtMember = Nothing
        Dim dt As New DataTable()
        Dim objParam As SqlParameter
        Try

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_viewFamilyMember_MST", Conn)
            Cmd.CommandType = CommandType.StoredProcedure           
            objParam = Cmd.Parameters.Add("@nMemberID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0
            da.SelectCommand = Cmd
            da.Fill(dt)
            dt.Columns.Add("SnoMed", GetType(String), " Conceptid + ' - ' + Description ")  ''8020 prd changes conceptid and snomed description concatnated ,chetan 
            If (IsNothing(dtMember) = False) Then
                dtMember.Dispose()
                dtMember = Nothing
            End If
            dtMember = dt
            dgMemberList.DataSource = dtMember
            
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Conn.Close()

            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If

            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            'If Not IsNothing(dt) Then : SLR:Don't dispose, it is a refernce to dtmember which is a reference to datasource
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            objParam = Nothing
        End Try

    End Sub

    Private Sub HideColumn()
        Dim ts As New clsDataGridTableStyle(dtMember.TableName)
        Dim dgId As New DataGridTextBoxColumn
        Dim dgRelation As New DataGridTextBoxColumn
        Dim dgDescription As New DataGridTextBoxColumn
        Dim dgConceptId As New DataGridTextBoxColumn
        Dim dgSnomedId As New DataGridTextBoxColumn
        Dim dgsSnomedDescription As New DataGridTextBoxColumn
        Dim dgDescriptionID As New DataGridTextBoxColumn
        Dim dgSnomed As New DataGridTextBoxColumn  ''8020 prd changes conceptid and snomed description concatnated ,chetan 


        Try

        
        With dgId
            .MappingName = dtMember.Columns(0).ColumnName
            .Alignment = HorizontalAlignment.Center
            .NullText = "Id"
            .Width = 0
        End With

        With dgRelation
            .MappingName = dtMember.Columns(1).ColumnName
            .HeaderText = "Relation"
            .NullText = ""
                .Width = 0.2 * dgMemberList.Width
            End With


            With dgDescription
                .MappingName = dtMember.Columns(2).ColumnName
                .HeaderText = "Description"
                .NullText = ""
                .Width = 0.6 * dgMemberList.Width
            End With


            With dgConceptId
                .MappingName = dtMember.Columns(3).ColumnName
                '09-May-13 Aniket: Updating Caption to Concept ID
                .HeaderText = "Concept ID"
                .NullText = ""
                .Width = 0
            End With


        With dgSnomedId
            .MappingName = dtMember.Columns(4).ColumnName
            .HeaderText = "SnomedID"
            .NullText = ""
                .Width = 0
        End With



        With dgsSnomedDescription
            .MappingName = dtMember.Columns(5).ColumnName
            .HeaderText = "sSnomedDescription"
            .NullText = ""
            .Width = 0
        End With



        With dgDescriptionID
            .MappingName = dtMember.Columns(6).ColumnName
            .HeaderText = "DescriptionID"
            .NullText = ""
                .Width = 0
            End With
            ''chetan added 8020 prd changes 
            With dgSnomed
                .MappingName = dtMember.Columns(7).ColumnName
                .HeaderText = "Snomed"
                .NullText = ""
                .Width = 0.2 * dgMemberList.Width - 40
            End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgId, dgRelation, dgDescription, dgConceptId, dgSnomedId, dgsSnomedDescription, dgDescriptionID, dgSnomed})
        dgMemberList.TableStyles.Clear()
            dgMemberList.TableStyles.Add(ts)

        If Not dgMemberList.VisibleRowCount = 0 Then
            dgMemberList.CurrentRowIndex = 0
            dgMemberList.Select(dgMemberList.CurrentRowIndex)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ts) Then
                ts.Dispose()
                ts = Nothing
            End If

            If Not IsNothing(ts) Then
                dgId.Dispose()
                dgId = Nothing
            End If

            If Not IsNothing(ts) Then
                dgRelation.Dispose()
                dgRelation = Nothing
            End If

            If Not IsNothing(ts) Then
                dgDescription.Dispose()
                dgDescription = Nothing
            End If

            If Not IsNothing(ts) Then
                dgConceptId.Dispose()
                dgConceptId = Nothing
            End If

            If Not IsNothing(ts) Then
                dgSnomedId.Dispose()
                dgSnomedId = Nothing
            End If

            If Not IsNothing(ts) Then
                dgsSnomedDescription.Dispose()
                dgsSnomedDescription = Nothing
            End If

            If Not IsNothing(ts) Then
                dgDescriptionID.Dispose()
                dgDescriptionID = Nothing
            End If
            If Not IsNothing(ts) Then
                dgSnomed.Dispose()
                dgSnomed = Nothing
            End If
        End Try


    End Sub

    Private Sub AddFamilyMember()
        Dim oAddMember As New frmFamilyMemberSettings()
        oAddMember.ShowDialog(IIf(IsNothing(oAddMember.Parent), Me, oAddMember.Parent))
        oAddMember.Dispose()
        oAddMember = Nothing
        FetchMember()
    End Sub

    Private Sub ModifyFamilyMember(ByVal mId As Long)

        Dim Con As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        Dim da As New SqlDataAdapter()
        Dim dt As New DataTable()

        Try
            Dim strSql = " Declare @id as  integer " +
                     " SELECT @id = COUNT ( SUBSTRING(History.sReaction,charindex(':',History.sReaction)+1,charindex('|',History.sReaction)+1))  " +
                     " FROM History " +
                     " WHERE sHistoryCategory='Family History' and charindex(':',History.sReaction)>1 and  charindex('|',History.sReaction) >1 " +
                     " and History.sReaction like  '%'+Convert(nvarchar, " & mId & ")+'%' " +
                     " SELECT @id "

            cmd = New SqlCommand(strSql, Con)
            Con.Open()
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            Con.Close()

            If Not IsNothing(dt) Then
                If Convert.ToInt32(dt.Rows(0)(0)) > 0 Then
                    MessageBox.Show("The selected relation cannot be modified as it is associated with Family History.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim oAddMember As New frmFamilyMemberSettings(mId)
                    oAddMember.ShowDialog(IIf(IsNothing(oAddMember.Parent), Me, oAddMember.Parent))
                    oAddMember.Dispose()
                    oAddMember = Nothing
                End If
            End If


            FetchMember()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Sub

    Private Sub DeleteFamilyMember(ByVal mId As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim Con As New SqlConnection(GetConnectionString())
        Try
            Dim dResult As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If dResult.ToString() = "Yes" Then
                cmd = New SqlCommand("gsp_DeleteFamilyMember_Mst", Con)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.AddWithValue("@nMemberId", mId)
                sqlParam.Direction = ParameterDirection.Input

                Con.Open()
                Dim result As Integer = cmd.ExecuteScalar()
                Con.Close()

                If result > 0 Then
                    MessageBox.Show("Relation deleted successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("The selected relation cannot be deleted as it is associated with Family History.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(Con) = False Then
                Con.Dispose()
                Con = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

            FetchMember()
        End Try
    End Sub

    Private Sub RefreshFamilyMember()
        Me.Cursor = Cursors.WaitCursor
        FetchMember()
        Me.Cursor = Cursors.Default
        txtSearch.Clear()
        _blnSearch = True
        If Not dgMemberList.VisibleRowCount = 0 Then
            dgMemberList.CurrentRowIndex = 0
            dgMemberList.Select(dgMemberList.CurrentRowIndex)
        End If
    End Sub

    Private Sub FormClose()
        Me.Close()

    End Sub

End Class