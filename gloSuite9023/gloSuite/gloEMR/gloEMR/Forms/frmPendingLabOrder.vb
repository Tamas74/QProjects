Imports System.Data.SqlClient

Public Class frmPendingLabOrder
    Dim nLabOrderID As Int64
    Dim nPatientID As Int64
    Dim nVisitID As Int64
    Dim dtTransactionDate As Date

    Dim ds As New DataSet
    Dim dt As DataTable
    Dim WithEvents frmLabOrder As frmLab_RequestOrder
    Dim selectedRowNo As Int64 = -1


    Private Sub setDataGridStyle(ByVal dt As DataTable)
        Dim _grdWidth As Int16 = (DataGrid.Width - 15) / 10
        Try
            Dim ts As New clsDataGridTableStyle(dt.TableName)

            Dim PatientID As New DataGridTextBoxColumn
            With PatientID
                .Width = 0 'hide
                .MappingName = dt.Columns(0).ColumnName
                .NullText = ""
                .HeaderText = "Patient ID"
            End With

            Dim LabOrderID As New DataGridTextBoxColumn
            With LabOrderID
                .Width = 0 'hide
                .MappingName = dt.Columns(1).ColumnName
                .NullText = ""
                .HeaderText = "Lab Order ID"
            End With

            Dim VisitID As New DataGridTextBoxColumn
            With VisitID
                .Width = 0 'hide
                .MappingName = dt.Columns(2).ColumnName
                .NullText = ""
                .HeaderText = "Lab Order ID"
            End With

            Dim TransactionDate As New DataGridTextBoxColumn
            With TransactionDate
                .Width = _grdWidth * 1.5
                .MappingName = dt.Columns(3).ColumnName
                .NullText = ""
                .HeaderText = "Transaction Date"
            End With

            Dim OrderNoPrefixAndID As New DataGridTextBoxColumn
            With OrderNoPrefixAndID
                .Width = _grdWidth * 1.5
                .NullText = ""
                .MappingName = dt.Columns(4).ColumnName
                .HeaderText = "Order Number ID"
            End With

            'Dim OrderNoID As New DataGridTextBoxColumn
            'With OrderNoID
            '    .Width = _grdWidth * 1.3
            '    .NullText = ""
            '    .MappingName = dt.Columns(5).ColumnName
            '    .HeaderText = "Order No ID"
            'End With

            Dim PatientCode As New DataGridTextBoxColumn
            With PatientCode
                .Width = _grdWidth * 1.1
                .NullText = ""
                .MappingName = dt.Columns(5).ColumnName
                .HeaderText = "Patient Code"
            End With


            Dim PatientFirstName As New DataGridTextBoxColumn
            With PatientFirstName
                .Width = _grdWidth * 1.4
                .NullText = ""
                .MappingName = dt.Columns(6).ColumnName
                .HeaderText = "Patient First Name"
            End With


            Dim PatientMiddleName As New DataGridTextBoxColumn
            With PatientMiddleName
                .Width = _grdWidth * 1.4
                .NullText = ""
                .MappingName = dt.Columns(7).ColumnName
                .HeaderText = "Patient Middle Name"
            End With

            Dim PatientLastName As New DataGridTextBoxColumn
            With PatientLastName
                .Width = _grdWidth * 1.4
                .NullText = ""
                .MappingName = dt.Columns(8).ColumnName
                .HeaderText = "Patient Last Name"
            End With

            Dim ProviderName As New DataGridTextBoxColumn
            With ProviderName
                .Width = _grdWidth * 1.75
                .NullText = ""
                .MappingName = dt.Columns(9).ColumnName
                .HeaderText = "Provider Name"
            End With

            'Dim ProviderLastName As New DataGridTextBoxColumn
            'With ProviderLastName
            '    .Width = _grdWidth * 1.6
            '    .NullText = ""
            '    .MappingName = dt.Columns(11).ColumnName
            '    .HeaderText = "Provider Last Name"
            'End With

            'dim RowSelectStyle as New datagrid

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {PatientID, LabOrderID, VisitID, TransactionDate, OrderNoPrefixAndID, PatientCode, PatientFirstName, PatientMiddleName, PatientLastName, ProviderName})
            DataGrid.TableStyles.Clear()
            DataGrid.TableStyles.Add(ts)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Refill Request", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub FillGrid()
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select lm.labom_PatientID, pl.nLabOrderID, lm.labom_VisitID, lm.labom_TransactionDate, isnull(lm.labom_OrderNoPrefix,'')  + space(0) +  isnull(cast(lm.labom_OrderNoID as varchar(50)) ,'') as OrderNoPrefixAndID, pt.sPatientCode, pt.sFirstName as PatientFirstName, pt.sMiddleName as PatientMiddleName, pt.sLastName as PatientLastName, pr.sFirstName +' '+ pr.sLastName as ProviderName from dbo.Lab_Order_MST lm inner join dbo.HL7_PendingLabOrders pl on lm.labom_OrderID=pl.nLabOrderID inner join dbo.Patient pt on pt.nPatientID=lm.labom_patientID inner join dbo.Provider_MST pr on lm.labom_ProviderID=pr.nProviderID"
            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0)
            setDataGridStyle(dt)
            DataGrid.DataSource = ds.Tables(0)
            DataGrid.Show()
            Connection.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ts_btnConfirmLabOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnConfirmLabOrder.Click
        Try
            If selectedRowNo > -1 Then    'If Row from DataGrid is clicked/selected
                nPatientID = DataGrid.Item(selectedRowNo, 0)
                nLabOrderID = DataGrid.Item(selectedRowNo, 1)
                nVisitID = DataGrid.Item(selectedRowNo, 2)
                dtTransactionDate = DataGrid.Item(selectedRowNo, 3)


                CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails(nPatientID)

                frmLabOrder = New frmLab_RequestOrder(nPatientID, nLabOrderID)
                With frmLabOrder
                    .LabOrderParameter.OrderID = nLabOrderID
                    .LabOrderParameter.OrderNumberID = 0
                    .LabOrderParameter.OrderNumberPrefix = "ORD"
                    .LabOrderParameter.PatientID = nPatientID
                    .LabOrderParameter.VisitID = nVisitID
                    .LabOrderParameter.TransactionDate = dtTransactionDate
                    .WindowState = FormWindowState.Maximized
                    .MdiParent = CType(Me.MdiParent, MainMenu)
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)

                    .blnOpenFromTask = True
                    .Show()
                    selectedRowNo = -1

                    ''  Me.Close()
                End With

                'Dim frm As New frmLab_RequestOrder(PID, OID)

                'frmLab_RequestOrder.Show()
                'frm.Show()
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnDenay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnDenay.Click
        DenieLabOrderToolStripMenuItem.PerformClick()
    End Sub

    Private Sub ConfirmLabOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmLabOrderToolStripMenuItem.Click
        ts_btnConfirmLabOrder.PerformClick()
    End Sub

    Private Sub DenieLabOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DenieLabOrderToolStripMenuItem.Click
        'To Delete Entry From PendingLabOrders
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            If selectedRowNo > -1 Then
                Connection.Open()
                nLabOrderID = DataGrid.Item(selectedRowNo, 1)
                Dim cmd As New SqlCommand("Delete from HL7_PendingLabOrders where nLabOrderID=" & nLabOrderID & "", Connection)
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from Lab_Order_MST where labom_OrderID=" & nLabOrderID & ""
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from Lab_Order_Test_Result where labotr_OrderID=" & nLabOrderID & ""
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from Lab_Order_Test_ResultDtl where labotrd_OrderID=" & nLabOrderID & ""
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Delete from Lab_Order_TestDtl where labotd_OrderID=" & nLabOrderID & ""
                cmd.ExecuteNonQuery()
                FillGrid()   'Will Refresh Grid After Delete
                selectedRowNo = -1
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmPendingLabOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'FillGrid()
    End Sub

    Private Sub ts_btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click
        FillGrid()
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub DataGrid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid.MouseDown
        Try
            selectedRowNo = DataGrid.HitTest(e.X, e.Y).Row

            If e.Button = Windows.Forms.MouseButtons.Right Then   'Hiding and Showing Context Menu on DataGrid Records
                If selectedRowNo < 0 Then
                    DataGrid.ContextMenuStrip = Nothing
                    Exit Sub
                Else
                    DataGrid.ContextMenuStrip = Me.cntContextMenu
                    'PID = DataGrid.Item(selectedRowNo, 0)
                End If
            End If
            'DataGrid.UnSelect(selectedRowNo) 'this is test
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataGrid_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid.MouseUp
        ' to Highlight or unselect row from grid
        selectedRowNo = DataGrid.HitTest(e.X, e.Y).Row
        If selectedRowNo = -1 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                DataGrid.UnSelect(i)
            Next
        Else
            DataGrid.Select(selectedRowNo)
        End If

    End Sub

    Private Sub frmLabOrder_OrderConfirmed() Handles frmLabOrder.OrderConfirmed
        FillGrid()
    End Sub

    Private Sub frmPendingLabOrder_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        FillGrid()
    End Sub
End Class