Imports System.Data
Imports System.Data.SqlClient
Imports gloAppointmentBook

Public Class frmAdd_ClinicalInstruction


    Private Sub frmAdd_ClinicalInstruction_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Fill_ClinicalInstruction(0)
        C1ClinInst.AllowEditing = False
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked

       Select e.ClickedItem.Tag.ToString()
            Case "New"
                Dim ofrm As New frmMstClinicalInstruction(0)
                ofrm.tsb_Save.Visible = False
                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                Fill_ClinicalInstruction(0)
                ofrm.Dispose()
            Case "Modify"
                Dim nID As Int64 = 0
                If C1ClinInst.Rows.Count > 0 Then
                    If C1ClinInst.GetData(C1ClinInst.Row, 0).ToString() <> "" Or C1ClinInst.GetData(C1ClinInst.Row, 0).ToString() <> "0" Then
                        nID = Convert.ToInt64(C1ClinInst.GetData(C1ClinInst.Row, 0).ToString())

                        Dim ofrm As New frmMstClinicalInstruction(nID)

                        ofrm.tsb_Save.Visible = False
                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        Fill_ClinicalInstruction(0)
                        ofrm.Dispose()
                    End If
                End If

            Case "Delete"
                If (MessageBox.Show("Are you sure to delete this clinical instruction?  ", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes) Then
                    Dim nID As Int64 = 0
                    If C1ClinInst.Rows.Count > 0 Then

                        If C1ClinInst.GetData(C1ClinInst.Row, 0).ToString() <> "" Or C1ClinInst.GetData(C1ClinInst.Row, 0).ToString() <> "0" Then
                            nID = Convert.ToInt64(C1ClinInst.GetData(C1ClinInst.Row, 0).ToString())
                            Dim objClinicalInstruction As New ClsClinicalInstruction()
                            objClinicalInstruction.DeleteClinicalInstruction(nID)
                            objClinicalInstruction.Dispose()
                        End If
                        Fill_ClinicalInstruction(0)
                    End If
                End If

            Case "Close"
                Me.Close()
        End Select
        If (txtSearch.Text.Trim() <> "") Then
            filterData()
        End If


    End Sub
    Dim _dv As DataView
    Private Sub Fill_ClinicalInstruction(nid As Int64)
        Dim objClinicalInstruction As New ClsClinicalInstruction()
        Try

            Dim _dtMstTable As DataTable = objClinicalInstruction.GetClinicalInstruction(nid)

            If _dtMstTable IsNot Nothing Then
                _dv = _dtMstTable.Copy().DefaultView
                _dtMstTable.Dispose()
                _dtMstTable = Nothing

            Else
                _dv = New DataView()
            End If
            C1ClinInst.DataSource = _dv
            C1ClinInst.Cols(0).Visible = False

            ''  C1ClinInst.[ReadOnly] = True
            C1ClinInst.Cols(1).Width = 150
           



            If _dv.Table.Rows.Count > 0 Then
                ts_btnModify.Enabled = True

                ts_btnDelete.Enabled = True
            End If
            If _dv.Table.Rows.Count <= 0 Then
                ts_btnModify.Enabled = False
                ts_btnDelete.Enabled = False


            End If
        Catch ex As gloDatabaseLayer.DBException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        Finally
            objClinicalInstruction.Dispose()
            objClinicalInstruction = Nothing
        End Try

        'throw new NotImplementedException();
    End Sub
    Private Sub filterData()
        Try
            Me.Cursor = Cursors.WaitCursor
            _dv = C1ClinInst.DataSource
            If (IsNothing(_dv)) Then
                Exit Sub
            End If
            C1ClinInst.DataSource = _dv

            Dim strSearchArray() As String = Nothing
            Dim sFilter As String = ""

            Dim strSearch As String = txtSearch.Text.Trim()


            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If (strSearch.Trim() <> "") Then

                strSearchArray = strSearch.Split(",")
            End If


            sFilter = ""
            If strSearch.Trim() <> "" Then
                If strSearchArray.Length = 1 Then
                    'For Single value search 
                    strSearch = strSearchArray(0)
                    strSearch = strSearch.Trim()
                    If strSearch.Length > 1 Then
                        Dim str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) + str
                    End If
                    _dv.RowFilter = _dv.Table.Columns("Instruction").ColumnName + " Like '" + strSearch + "%' OR [" + _dv.Table.Columns("Instruction Description").ColumnName + "] Like '" + strSearch + "%'"
                Else
                    'For Comma separated  value search
                    Dim i As Integer
                    For i = 0 To strSearchArray.Length - 1 Step i + 1
                        strSearch = strSearchArray(i)
                        strSearch = strSearch.Trim()
                        If strSearch.Length > 1 Then
                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) + str
                        End If
                        If strSearch.Trim() <> "" Then
                            If sFilter = "" Then
                                sFilter = " ( " + _dv.Table.Columns("Instruction").ColumnName + " Like '" + strSearch + "%' OR [" +
                                          _dv.Table.Columns("Instruction Description").ColumnName + "] Like '" + strSearch + "%' ) "
                            Else
                                sFilter = sFilter + " AND (" + _dv.Table.Columns("Instruction").ColumnName + " Like '" + strSearch + "%' OR [" +
                                         _dv.Table.Columns("Instruction Description").ColumnName + "] Like '" + strSearch + "%' ) "
                            End If

                        End If
                    Next
                    _dv.RowFilter = sFilter

                End If
            Else
                _dv.RowFilter = ""
            End If



            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged

        filterData()

    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
    End Sub

   

    Private Sub C1ClinInst_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1ClinInst.MouseDoubleClick
        ''made change against bugid 65834
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1ClinInst.HitTest(ptPoint)

        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
            Dim nID As Int64 = 0
            If C1ClinInst.Rows.Count > 0 Then
                If C1ClinInst.GetData(C1ClinInst.Row, 0).ToString() <> "" Or C1ClinInst.GetData(C1ClinInst.Row, 0).ToString() <> "0" Then
                    nID = Convert.ToInt64(C1ClinInst.GetData(C1ClinInst.Row, 0).ToString())

                    Dim ofrm As New frmMstClinicalInstruction(nID)

                    ofrm.tsb_Save.Visible = False
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    Fill_ClinicalInstruction(0)
                    ofrm.Dispose()
                End If
                If (txtSearch.Text.Trim() <> "") Then
                    filterData()
                End If
            End If
        End If
    End Sub

    Private Sub C1ClinInst_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1ClinInst.MouseMove
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1ClinInst.HitTest(ptPoint)

        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then

            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        End If
    End Sub
End Class