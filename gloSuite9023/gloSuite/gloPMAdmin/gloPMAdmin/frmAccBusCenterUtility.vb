Public Class frmAccBusCenterUtility
    Private dtAccounts As New DataTable()
    Private Sub frmAccBusCenterUtility_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
   
    End Sub

    Private Sub ProcessAccounts(ByVal dtAccounts As DataTable)

        If dtAccounts IsNot Nothing AndAlso dtAccounts.Rows.Count > 0 Then
            Dim objBusinessCenter As New ClsBusinessCenter()
           
            Me.Cursor = Cursors.WaitCursor
            Dim count As Int64 = dtAccounts.Rows.Count
            For i As Integer = 0 To dtAccounts.Rows.Count - 1
                lblStatus.Text = "Processing Account " & (i + 1).ToString() & " out of " & count.ToString()
                objBusinessCenter.ProcessAccount(Convert.ToInt64(dtAccounts.Rows(i)(0).ToString()))
                ProgressBar.Value = i + 1
                Application.DoEvents()
            Next
        End If
        lblStatus.Text = "Completed"
        Application.DoEvents()
        FinishProcess()

    End Sub

    Private Sub GetDataToProcess()

        Dim objBusinessCenter As New ClsBusinessCenter()
        dtAccounts = objBusinessCenter.GetAccountsWithoutBC()
        'ProcessAccounts(dtAccounts);         
    End Sub


    Private Sub FinishProcess()
        Cursor.Current = Cursors.[Default]
        System.Threading.Thread.Sleep(2000)
        Me.Close()
    End Sub

    Private Sub frmAccBusCenterUtility_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        GetDataToProcess()
        If dtAccounts IsNot Nothing AndAlso dtAccounts.Rows.Count > 0 Then
            ProgressBar.Maximum = dtAccounts.Rows.Count
            lblStatus.Text = "Processing Accounts"
            ProcessAccounts(dtAccounts)
        Else
            ProgressBar.Value = ProgressBar.Maximum
            lblStatus.Text = "Completed"
            Application.DoEvents()
            FinishProcess()
        End If

    End Sub
End Class