Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmLab_StorageTemperature

    Public nEditID As Int64
    Public sEditName As String
    Public sEditCode As String
    Public blnIsModify As Boolean = False

    Private Sub SaveStTemperature()

        Dim oLabStorageTemp As New gloEMRLabStorageTemperature

        Try
            If txtCode.Text = "" And txtTemperature.Text = "" Then
                MessageBox.Show("Please enter Storage Temperature Code and Temperature Value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtCode.Text = "" Then
                MessageBox.Show("Please enter Storage Temperature code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtTemperature.Text = "" Then
                MessageBox.Show("Please enter Storage Temperature Value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtTemperature.Text = ""
                txtTemperature.Select()
                Exit Sub
            End If

            'Do the Result grid validations
            If blnIsModify = True Then
                If oLabStorageTemp.IsCodeExists(txtCode.Text) = True And oLabStorageTemp.IsExists(txtTemperature.Text) = True Then
                    MessageBox.Show("Duplicate Storage Temperature Code and Value.Please enter another Code and Value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtTemperature.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                'check for duplicate entries for add
                If oLabStorageTemp.IsCodeExists(txtCode.Text) = True Then
                    MessageBox.Show("Duplicate Storage Temperature Code.Please enter another Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                If oLabStorageTemp.IsExists(txtTemperature.Text) = True Then
                    MessageBox.Show("Duplicate Storage Temperature Value.Please enter another Value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtTemperature.Text = ""
                    txtTemperature.Select()
                    Exit Sub
                End If

                With oLabStorageTemp.LabStorageTemperature
                    .LabStorageTemperatureID = 0
                    .LabStorageTemperatureCode = txtCode.Text
                    .LabStorageTemperatureName = txtTemperature.Text
                End With
                oLabStorageTemp.Add()
            Else

                'check for duplicate entries for modify
                If txtCode.Text <> sEditCode And txtTemperature.Text <> sEditName Then
                    If oLabStorageTemp.IsCodeExists(txtCode.Text) = True And oLabStorageTemp.IsExists(txtTemperature.Text) = True Then
                        MessageBox.Show("Duplicate Storage Temperature Code and Value.Please enter another Code and Value.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If
                If txtCode.Text <> sEditCode Then
                    If oLabStorageTemp.IsCodeExists(txtCode.Text) = True Then
                        MessageBox.Show("Duplicate Storage Temperature Code.Please enter another Code", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If

                If txtTemperature.Text <> sEditName Then
                    If oLabStorageTemp.IsExists(txtTemperature.Text) = True Then
                        MessageBox.Show("Duplicate Storage Temperature Name.Please enter another Name", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtTemperature.Text = ""
                        txtTemperature.Select()
                        Exit Sub
                    End If
                End If

                With oLabStorageTemp.LabStorageTemperature
                    .LabStorageTemperatureID = nEditID
                    .LabStorageTemperatureCode = txtCode.Text
                    .LabStorageTemperatureName = txtTemperature.Text
                End With

                oLabStorageTemp.Modify(nEditID)
            End If

            If blnIsModify = True Then
                '//Write cleanup code for next test setup
                txtCode.Text = ""
                txtTemperature.Text = ""
                'Else
                '   Me.Close()
            End If

            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmLab_StorageTemperature_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If nEditID > 0 Then
            'Modify mode
            Dim oLabStorageTemp As LabActor.LabStorageTemperature
            Dim oStorageTemp As New gloEMRLabStorageTemperature

            oLabStorageTemp = oStorageTemp.GetStorageTemperature(nEditID)
            If (IsNothing(oLabStorageTemp) = False) Then
                txtCode.Text = oLabStorageTemp.LabStorageTemperatureCode
                txtTemperature.Text = oLabStorageTemp.LabStorageTemperatureName
                oLabStorageTemp.Dispose()
                oLabStorageTemp = Nothing
            End If
            oStorageTemp.Dispose()
            oStorageTemp = Nothing
        End If

    End Sub


   
    Private Sub tlsp_StorageTemperature_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_StorageTemperature.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveStTemperature()
                Case "Close"
                    Me.Close()

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub
End Class