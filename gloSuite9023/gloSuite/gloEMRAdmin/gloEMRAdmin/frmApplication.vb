Imports System.Data.SqlClient
Public Class frmApplication

    Private Sub TsCancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsCancelBtn.Click
        Me.Close()
    End Sub

    Private Sub TsSaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsSaveBtn.Click

        If (Validationform()) Then
            Dim objCon As New SqlConnection
            Try

                objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
                Dim objCmd As New SqlCommand

                objCmd.CommandType = CommandType.Text
                Dim sQuery As String
                sQuery = "INSERT INTO WS_Applications (nApplicationId ,sApplicationCode,sApplicationName,sVendorCode ,sVendorName,bIsActive,dtCreated) " +
                                    " VALUES(dbo.GetUniqueID_V2(),'" + Convert.ToString(txtApplicationCode.Text.Trim()) + "','" + txtApplicationName.Text.Trim() + "','" + txtVendorcode.Text.Trim() + "','" +
                                    txtVendorName.Text.Trim() + "'," + chkActive.Checked.GetHashCode().ToString() + ",dbo.gloGetDate() )"
                objCmd.CommandText = sQuery

                objCmd.Connection = objCon
                objCon.Open()
                objCmd.ExecuteNonQuery()

                Me.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)                
            Finally
                If Not IsNothing(objCon) Then
                    objCon.Dispose()
                    objCon = Nothing
                End If
            End Try

        End If

    End Sub

    Public Function Validationform() As Boolean
        If txtApplicationCode.Text.Trim() = "" Then
            MessageBox.Show("Enter Application Code ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtApplicationCode.Focus()
            Return False
        End If
        If txtApplicationName.Text.Trim() = "" Then
            MessageBox.Show("Enter Application Name  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtApplicationName.Focus()
            Return False
        End If
        If IsAppCode() = True Then
            MessageBox.Show("Application code already exists, enter new application code ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtApplicationCode.Focus()
            Return False
        End If
        Return True
    End Function

    Public Function IsAppCode() As Boolean
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.Text
            Dim sQuery As String
            sQuery = "select count(sApplicationCode) from WS_Applications where sApplicationCode='" + txtApplicationCode.Text.Trim + "'"
            objCmd.CommandText = sQuery

            objCmd.Connection = objCon
            objCon.Open()
            Dim AppCodecount As Int16
            AppCodecount = Convert.ToInt16(objCmd.ExecuteScalar())

            If (AppCodecount > 0) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

End Class