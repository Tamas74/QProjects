Public Class frmAddCatalogCode
    Public CatalogID As Int64
    Public CatalogCode As String = ""

    Private Sub ts_btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSave.Click
        If (txtCatalogCode.Text.Trim() = "") Then
            MessageBox.Show("Enter Catalog Code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCatalogCode.Focus()
            Exit Sub
        End If
        If (IsCatalogExist(txtCatalogCode.Text, CatalogID) = False) Then
            SaveCDSCatalog(CatalogID, txtCatalogCode.Text)
            Me.Close()
        Else
            If (CatalogID > 0) And (CatalogCode.Trim() = txtCatalogCode.Text.Trim()) Then
                Me.Close()
            Else
                MessageBox.Show("Duplicate Catalog Code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCatalogCode.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub
    Public Function SaveCDSCatalog(ByVal _CatalogID As Int64, ByVal CatalogCode As String) As Boolean

        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        oDB.Connect(False)
        Try

            oDBParameters.Add("@nCDS_CatalogID", CatalogID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@CatalogCode", CatalogCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Execute("gsp_InUpCDS_CatalogMaster", oDBParameters)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function
    Public Function IsCatalogExist(ByVal CatalogCode As String, ByVal CatalogId As Int64) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim Result As Object
        oDB.Connect(False)
        Try
            oDBParameters.Add("@CatalogCode", CatalogCode, ParameterDirection.Input, SqlDbType.VarChar)
            Result = oDB.ExecuteScalar("gsp_IsCatalogExist", oDBParameters)
            If (Convert.ToInt64(Result) > 0) Then
                Return True
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function
    ' 
    
    Private Sub frmAddCatalogCode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCatalogCode.Text = CatalogCode
    End Sub
End Class