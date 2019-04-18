Public Class frmAssociateProvider

    Private _PatientID As Int64 = 0
    Public isAssociated As Boolean = False

    Public Sub New(ByVal patientID As Int64)
        InitializeComponent()
        _PatientID = patientID
    End Sub

    Private Sub frmAssociateProvider_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillProvider()
    End Sub

    Private Sub FillProvider()
        Try
            Dim oProvider As New clsProvider
            Dim dtProvider As DataTable
            dtProvider = oProvider.GetProviders(True)
            oProvider.Dispose()
            oProvider = Nothing
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

    Private Sub tls_AssociateProvider_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_AssociateProvider.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "SaveAndClose"
                Dim oPatient As New clsPatient
                If oPatient.ChangePatientProvider(_PatientID, cmbProvider.SelectedValue) = True Then
                    isAssociated = True
                Else
                    isAssociated = False
                End If
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "Close"
                isAssociated = False
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

End Class