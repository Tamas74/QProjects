Public Class frmViewListControl
    Private Sub frmViewListControl_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Dim ogloListControl As gloListControl.gloListControl = CType(Me.Controls(0), gloListControl.gloListControl)
            If IsNothing(ogloListControl) = False Then
                ogloListControl.OpenControl()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class