Imports System.Data.SqlClient



Public Class frmProviderEducation_View

    Private providerID As Long = 0
    Private Const Col_ProviderEducationId As Integer = 0
    Private Const Col_ProviderId As Integer = 1
    Private Const Col_Provider As Integer = 2
    Private Const Col_VisitId As Integer = 3
    Private Const Col_CodeSystem As Integer = 4
    Private Const Col_Code As Integer = 5
    Private Const Col_CodeDesc As Integer = 6
    Private Const Col_DocumentTitle As Integer = 7
    Private Const Col_DocumentURl As Integer = 8
    'Private Const Col_Document As Integer = 6
    Private Const Col_CreatedDateTime As Integer = 9

    Public Sub New()
        InitializeComponent()
    End Sub



    Private Sub frmProviderEducation_View_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            gloC1FlexStyle.Style(C1ProviderEducation)
            FillProviders()
            Designgrid()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Designgrid()
        Try
            Dim dt As DataTable

            txtSearch.Focus()

            Dim oProviderEducation As New clsProviderEducation(GetConnectionString())
            If providerID = 0 Then
                dt = oProviderEducation.GetProviderEducation(0, 0, 0)
            Else
                dt = oProviderEducation.GetProviderEducation(providerID, 1)
            End If



            C1ProviderEducation.DataSource = Nothing
            If dt IsNot Nothing Then

                Dim _dv As DataView = dt.Copy().DefaultView
                C1ProviderEducation.Visible = True

                C1ProviderEducation.DataSource = _dv
                C1ProviderEducation.Rows.Fixed = 1


                C1ProviderEducation.Cols(Col_ProviderEducationId).Caption = "ProviderEducationID"
                C1ProviderEducation.Cols(Col_ProviderId).Caption = "ProviderId"
                C1ProviderEducation.Cols(Col_Provider).Caption = "Provider"
                C1ProviderEducation.Cols(Col_VisitId).Caption = "VisitiD"
                C1ProviderEducation.Cols(Col_Code).Caption = "Code"
                C1ProviderEducation.Cols(Col_CodeSystem).Caption = "Code System"
                C1ProviderEducation.Cols(Col_CodeDesc).Caption = "Desc"
                C1ProviderEducation.Cols(Col_DocumentTitle).Caption = "Document Title"
                C1ProviderEducation.Cols(Col_DocumentURl).Caption = "Document URL"
                'C1ProviderEducation.Cols(Col_Document).Caption = "Document"
                C1ProviderEducation.Cols(Col_CreatedDateTime).Caption = "Created On"

                C1ProviderEducation.Cols(Col_ProviderEducationId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_ProviderId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_Provider).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_VisitId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_CodeSystem).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_CodeDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_DocumentTitle).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_DocumentURl).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1ProviderEducation.Cols(Col_CreatedDateTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1ProviderEducation.Cols(Col_ProviderEducationId).Visible = False
                C1ProviderEducation.Cols(Col_ProviderId).Visible = False
                C1ProviderEducation.Cols(Col_Provider).Visible = True
                C1ProviderEducation.Cols(Col_VisitId).Visible = False
                C1ProviderEducation.Cols(Col_Code).Visible = True
                C1ProviderEducation.Cols(Col_CodeSystem).Visible = True
                C1ProviderEducation.Cols(Col_CodeDesc).Visible = True
                C1ProviderEducation.Cols(Col_DocumentTitle).Visible = True
                C1ProviderEducation.Cols(Col_DocumentURl).Visible = True
                'C1ProviderEducation.Cols(Col_Document).Visible = False
                C1ProviderEducation.Cols(Col_CreatedDateTime).Visible = True

                Dim nWidth As Integer = Me.Width - 10

                C1ProviderEducation.Cols(Col_ProviderEducationId).Width = 0
                C1ProviderEducation.Cols(Col_ProviderId).Width = 0
                C1ProviderEducation.Cols(Col_Provider).Width = CInt((0.12 * (nWidth)))
                C1ProviderEducation.Cols(Col_VisitId).Width = 0

                C1ProviderEducation.Cols(Col_CodeSystem).Width = CInt((0.07 * (nWidth)))
                C1ProviderEducation.Cols(Col_Code).Width = CInt((0.07 * (nWidth)))
                C1ProviderEducation.Cols(Col_CodeDesc).Width = CInt((0.15 * (nWidth)))

                C1ProviderEducation.Cols(Col_DocumentTitle).Width = CInt((0.2 * (nWidth)))
                C1ProviderEducation.Cols(Col_DocumentURl).Width = CInt((0.4 * (nWidth)))
                'C1ProviderEducation.Cols(Col_Document).Width = 0
                C1ProviderEducation.Cols(Col_CreatedDateTime).Width = CInt((0.08 * (nWidth)))


                C1ProviderEducation.Cols(Col_CreatedDateTime).DataType = GetType(DateTime)
                C1ProviderEducation.Cols(Col_CreatedDateTime).Format = "MM/dd/yyyy"

                C1ProviderEducation.AllowEditing = False
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub ts_btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnAdd.Click
        Try
            Dim oProviderEducation As New frmProviderEducation(providerID, 0)
            oProviderEducation.ShowInTaskbar = False
            oProviderEducation.WindowState = FormWindowState.Maximized
            oProviderEducation.ShowDialog()
            Designgrid()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnModify_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnModify.Click
        Try

            providerID = Convert.ToInt64(C1ProviderEducation.GetData(C1ProviderEducation.Row, Col_ProviderId))
            Dim EducationId As Long = Convert.ToInt64(C1ProviderEducation.GetData(C1ProviderEducation.Row, Col_ProviderEducationId))

            Dim oProviderEducation As New frmProviderEducation(providerID, 0, EducationId)
            oProviderEducation.ShowInTaskbar = False
            oProviderEducation.WindowState = FormWindowState.Maximized
            oProviderEducation.ShowDialog()
            Designgrid()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnDelete.Click

    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        gloC1FlexStyle.Style(C1ProviderEducation)
        Designgrid()
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub


    Private Sub FillProviders()
        Try
            Dim dt As DataTable
            dt = gloGlobal.gloPMMasters.GetProviders()

            If dt IsNot Nothing Then
                Dim dr As DataRow = dt.NewRow()
                dr("nProviderID") = 0
                dr("sProviderName") = "All"
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()

                RemoveHandler cmbProvider.SelectedIndexChanged, AddressOf cmbProvider_SelectedIndexChanged
                cmbProvider.DataSource = dt
                cmbProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbProvider.DisplayMember = dt.Columns("sProviderName").ColumnName
                cmbProvider.Refresh()
                cmbProvider.SelectedIndex = -1

                providerID = gloGlobal.gloPMGlobal.LoginProviderID
                cmbProvider.SelectedValue = providerID
                gloC1FlexStyle.Style(C1ProviderEducation)
                Designgrid()
                AddHandler cmbProvider.SelectedIndexChanged, AddressOf cmbProvider_SelectedIndexChanged
            End If
            dt = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub cmbProvider_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        If cmbProvider.SelectedIndex > 0 Then
            providerID = Convert.ToInt64(cmbProvider.SelectedValue)
        Else
            providerID = 0
        End If
        gloC1FlexStyle.Style(C1ProviderEducation)
        Designgrid()
    End Sub

    Private Sub C1ProviderEducation_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1ProviderEducation.MouseDoubleClick
        Dim r As Integer = C1ProviderEducation.HitTest(e.X, e.Y).Row
        Try
            providerID = Convert.ToInt64(C1ProviderEducation.GetData(r, Col_ProviderId))
            Dim EducationId As Long = Convert.ToInt64(C1ProviderEducation.GetData(r, Col_ProviderEducationId))
            Dim oProviderEducation As New frmProviderEducation(providerID, 0, EducationId)
            oProviderEducation.ShowInTaskbar = False
            oProviderEducation.WindowState = FormWindowState.Maximized
            oProviderEducation.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class