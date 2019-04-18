Imports System.Windows.Forms
Imports RxSniffer.RxGeneral

Public Class frmViewDBCredentials

    Dim dtDatabaseCredentials As DataTable
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        CleareValues()
        Dim objSetting As frmDBCredentials = New frmDBCredentials
        objSetting.ShowDialog()
        BindGrid()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If dgDatabseConnection.RowCount > 0 Then
            Setvalues()
            Dim objSetting As frmDBCredentials = New frmDBCredentials
            objSetting.ShowDialog()
            BindGrid()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (dtDatabaseCredentials.Rows.Count > 0) Then
            ''fixed the spell mistake bug 7753
            If (MessageBox.Show(Me, "Are you sure you want to delete selected database credentials?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                Setvalues()
                Dim objClsDbCredentials As ClsDbCredentials = New ClsDbCredentials
                objClsDbCredentials.deleteDataBaseCredentials(Convert.ToInt64(dgDatabseConnection.SelectedRows(0).Cells(0).Value))
                BindGrid()
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmViewDBCredentials_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindGrid()
    End Sub

    Private Sub BindGrid()
        dtDatabaseCredentials = New DataTable
        Dim objClsDbCredentials As ClsDbCredentials = New ClsDbCredentials
        dtDatabaseCredentials = objClsDbCredentials.getDataBaseCredentials(0)
        dgDatabseConnection.DataSource = dtDatabaseCredentials
        DesignDataGrid()
        If (dtDatabaseCredentials.Rows.Count > 0) Then
            dgDatabseConnection.Rows(0).Selected = True
            dgDatabseConnection.Columns(0).Visible = False
        End If
    End Sub

    Private Sub DesignDataGrid()
        dgDatabseConnection.Columns(0).HeaderText = "Database ID"
        dgDatabseConnection.Columns(0).Visible = False
        dgDatabseConnection.Columns(0).ReadOnly = True
        dgDatabseConnection.Columns(1).HeaderText = "Server Name"
        dgDatabseConnection.Columns(1).Width = 200
        dgDatabseConnection.Columns(1).Visible = True
        dgDatabseConnection.Columns(1).ReadOnly = True
        dgDatabseConnection.Columns(2).HeaderText = "Database Name"
        dgDatabseConnection.Columns(2).Width = 200
        dgDatabseConnection.Columns(2).Visible = True
        dgDatabseConnection.Columns(2).ReadOnly = True
        dgDatabseConnection.Columns(3).HeaderText = "Sql User Name"
        dgDatabseConnection.Columns(3).Width = 200
        dgDatabseConnection.Columns(3).Visible = False
        dgDatabseConnection.Columns(3).ReadOnly = True
        dgDatabseConnection.Columns(4).HeaderText = "Password"
        dgDatabseConnection.Columns(4).Visible = False
        dgDatabseConnection.Columns(4).ReadOnly = True
    End Sub

    Private Sub dgDatabseConnection_Sorted(ByVal sender As Object, ByVal e As EventArgs) Handles dgDatabseConnection.Sorted
        If dgDatabseConnection.RowCount > 0 Then
            dgDatabseConnection.CurrentRow.Selected = True
        End If
    End Sub

    Private Sub CleareValues()
        mdlGeneral.DatabaseID = 0
    End Sub

    Private Sub Setvalues()
        If (dgDatabseConnection.SelectedRows.Count > 0) Then
            mdlGeneral.DatabaseID = Convert.ToInt32(dgDatabseConnection.SelectedRows(0).Cells(0).Value)
        End If
    End Sub

    Private Sub dgDatabseConnection_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDatabseConnection.CellContentClick
        If dgDatabseConnection.RowCount > 0 Then
            dgDatabseConnection.CurrentRow.Selected = True
        End If
    End Sub

    Private Sub dgDatabseConnection_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDatabseConnection.CellClick
        If dgDatabseConnection.RowCount > 0 Then
            dgDatabseConnection.CurrentRow.Selected = True
        End If
    End Sub

    Private Sub dgDatabseConnection_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgDatabseConnection.ColumnHeaderMouseClick

    End Sub
End Class