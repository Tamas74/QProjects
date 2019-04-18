Public Class frmViewListControl
    Private Sub frmViewListControl_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Dim ogloListControl As gloListControl.gloListControl = CType(Me.Controls(0), gloListControl.gloListControl)
            If IsNothing(ogloListControl) = False Then
                ogloListControl.OpenControl()
                If Not IsNothing(ogloListControl.dgListView) Then
                    If ogloListControl.dgListView.Columns.Count > 0 Then
                        If ogloListControl.dgListView.Columns(0).Visible = True Then
                            ogloListControl.dgListView.Columns(0).Width = 50
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class