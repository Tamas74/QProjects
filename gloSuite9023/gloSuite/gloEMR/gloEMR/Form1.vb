Public Class Form1


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim frm As New frmToolButtonSelection(tlbStripMain, frmToolButtonSelection.enumModuleName.Dashboard)
        frm.ShowButtonSelection()
        MessageBox.Show(gstrMessageBoxCaption)
    End Sub

    Private Sub Test_5040()
        Try
            MessageBox.Show("Test_5040", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub tlbStripMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tlbStripMain.MouseDown
        Try

            'If e.Button = Windows.Forms.MouseButtons.Right Then
            '    ContextMenu1.MenuItems.Clear()
            '    Dim cmnuCostomize As New MenuItem("Customize")
            '    ContextMenu1.MenuItems.Add(cmnuCostomize)
            '    AddHandler cmnuCostomize.Click, AddressOf cmnuCostomize_Click
            '    tlbStripMain.ContextMenu = ContextMenu1
            'Else
            '    tlbStripMain.ContextMenu = Nothing
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmnuCostomize_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim frm As New frmToolButtonSelection(tlbStripMain, frmToolButtonSelection.enumModuleName.Dashboard)
            'frm._ToolButtons = tblStrip_32.Items
            frm.ShowDialog(frm.Parent)
            frm.Dispose()
            frm = Nothing
            'If IsNothing(frm._ToolButtons) = False Then
            '    '' tblStrip_32.Items.Clear()
            '    For i As Integer = 0 To frm._ToolButtons.Count - 1
            '        tblStrip_32.Items.Add(frm._ToolButtons(i))
            '    Next
            'frm.ToolButtons

            'Call Set_ToolBarButtons()

            'tblStrip_32.CanOverflow = True
            'tblStrip_32.OverflowButton.Overflow = ToolStripItemOverflow.AsNeeded


        Catch ex As NullReferenceException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

End Class
