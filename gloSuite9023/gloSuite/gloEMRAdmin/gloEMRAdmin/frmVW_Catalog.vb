Public Class frmVW_Catalog

    Private Sub C1CatalogDetails_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CatalogDetails.DoubleClick
        Try
            If C1CatalogDetails.RowSel > 0 Then
                Dim ofrm As New frmAddCatalogCode
                ofrm.CatalogID = C1CatalogDetails.GetData(C1CatalogDetails.RowSel, 0)
                ofrm.CatalogCode = C1CatalogDetails.GetData(C1CatalogDetails.RowSel, "Catalog Code")
                ofrm.StartPosition = FormStartPosition.CenterScreen
                ofrm.Text = "Modify Catalog Code"
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
            Dim ofrm As New frmAddCatalogCode()
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

    Private Sub frmVW_Catalog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillGrid()
        Try
            Dim dt As DataTable
            dt = GetCatalogslist()

            If Not IsNothing(dt) Then
                C1CatalogDetails.DataSource = dt.DefaultView
                If C1CatalogDetails.Rows.Count > 1 Then
                    C1CatalogDetails.Select(1, 1)
                End If
            End If
            SetGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SetGridStyle()
        'Width
        C1CatalogDetails.Cols(0).Width = 0
        C1CatalogDetails.Cols(1).Width = C1CatalogDetails.Width * 0.7
        'visible
        C1CatalogDetails.Cols(0).Visible = False
        C1CatalogDetails.Cols(1).Visible = True


        C1CatalogDetails.AllowEditing = False

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If C1CatalogDetails.RowSel > 0 Then
            If (MessageBox.Show("Are you sure you want to delete this record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                Dim CatalogID As Int64 = C1CatalogDetails.GetData(C1CatalogDetails.RowSel, 0)
                Dim CatalogCode As String = C1CatalogDetails.GetData(C1CatalogDetails.RowSel, "Catalog Code")
                If (DeleteCatalog(CatalogCode, CatalogID) = True) Then
                End If
                FillGrid()
            End If
        End If

    End Sub

    Public Function DeleteCatalog(ByVal CatalogCode As String, ByVal CatalogId As Int64) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim Result As Object
        oDB.Connect(False)
        Try
            oDBParameters.Add("@CatalogID", CatalogId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@CatalogCode", CatalogCode, ParameterDirection.Input, SqlDbType.VarChar)
            Result = oDB.ExecuteScalar("gsp_DeleteCatalog", oDBParameters)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Private Function GetCatalogslist() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim dtResult As DataTable
        oDB.Connect(False)
        Try
            oDB.Retrive("gsp_GetCDS_Catalog", dtResult)
            Return dtResult
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dtResult
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        C1CatalogDetails_MouseDoubleClick(Nothing, Nothing)
    End Sub
End Class