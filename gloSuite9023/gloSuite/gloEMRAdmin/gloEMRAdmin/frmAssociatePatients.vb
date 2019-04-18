Public Class frmAssociatePatients

    Private _OldProviderID As Int64
    Dim oProvider As New clsProvider

    Public Sub New(ByVal providerID As Int64)
        InitializeComponent()
        _OldProviderID = providerID
    End Sub

    Private Sub frmAssosiatePatients_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillProvider()
    End Sub

    Private Sub FillProvider()
        Try
            Dim dtProvider As DataTable
            dtProvider = oProvider.GetProviders(True)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    cmbProvider.DataSource = dtProvider
                    cmbProvider.ValueMember = dtProvider.Columns("nProviderID").ColumnName
                    cmbProvider.DisplayMember = dtProvider.Columns("ProviderName").ColumnName
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Dim _NewProviderID As Int64 = cmbProvider.SelectedValue
            If oProvider.AssociatePatients(_OldProviderID, _NewProviderID) = True Then
                Me.Close()
            Else
                MessageBox.Show("Error while associating patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class