Public Class frmDASTestsList

    Private Sub C1TestDetails_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1TestDetails.MouseDoubleClick
        Try
            If C1TestDetails.RowSel > 0 Then
                Dim TestID As Int64 = C1TestDetails.GetData(C1TestDetails.RowSel, 0)
                Dim ofrm As New frmDASSettings(TestID)
                ofrm.TestName = C1TestDetails.GetData(C1TestDetails.RowSel, 1)
                ofrm.SelectedESRCRP = C1TestDetails.GetData(C1TestDetails.RowSel, 2)
                ofrm.StartPosition = FormStartPosition.CenterScreen
                ofrm.ShowDialog()
                FillGrid()
                ofrm.Dispose()
                ofrm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Dim ofrm As New frmDASSettings()
            ofrm.StartPosition = FormStartPosition.CenterScreen
            ofrm.ShowDialog()
            FillGrid()
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmDASTestsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillGrid()
        Try
            Dim dt As DataTable
            Dim ocls As New clsDASSettings
            dt = ocls.GetDASTest()

            If Not IsNothing(dt) Then
                C1TestDetails.DataSource = dt.DefaultView
                If C1TestDetails.Rows.Count > 1 Then
                    C1TestDetails.Select(1, 1)
                End If
            End If
            SetGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SetGridStyle()
        'Width
        C1TestDetails.Cols(0).Width = 0
        C1TestDetails.Cols(1).Width = C1TestDetails.Width * 0.7
        C1TestDetails.Cols(2).Width = C1TestDetails.Width * 0.3

        'visible
        C1TestDetails.Cols(0).Visible = False
        C1TestDetails.Cols(1).Visible = True
        C1TestDetails.Cols(2).Visible = True

        C1TestDetails.AllowEditing = False

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteTest()

    End Sub

    Private Sub DeleteTest()
        Try
            If C1TestDetails.Rows.Count > 1 And C1TestDetails.RowSel > 0 Then
                If MessageBox.Show("Are you sure, you want to delete this test?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                    Dim ocls As New clsDASSettings
                    ocls.DeleteDASTest(C1TestDetails.GetData(C1TestDetails.RowSel, 0))
                    FillGrid()
                    ocls.Dispose()
                    ocls = Nothing
                End If
            Else
                MessageBox.Show("Please select test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class