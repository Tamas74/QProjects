Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmLab_CollectionMaster

    Public nEditID As Int64
    Public sEditName As String
    Public sEditCode As String
    Public blnIsModify As Boolean = False


    Private Sub frmLab_CollectionMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

      
            If nEditID > 0 Then
                'Modify mode
                Dim oLabCollection As LabActor.LabCollectionContainer
                Dim oCollection As New gloEMRLabCollectionContainer

                oLabCollection = oCollection.GetCollectionContainer(nEditID)
                oCollection.Dispose()
                oCollection = Nothing
                If (IsNothing(oLabCollection) = False) Then
                    txtCode.Text = oLabCollection.LabCollectionContainerCode
                    txtContainer.Text = oLabCollection.LabCollectionContainerName
                    oLabCollection.Dispose()
                    oLabCollection = Nothing
                End If
                
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub saveLabCollection()

        Dim oLabContainer As New gloEMRLabCollectionContainer

        Try
            'Do the Result grid validations

            'validation for blank entries
            If txtCode.Text.Trim.Replace("'", "") = "" And txtContainer.Text.Trim.Replace("'", "") = "" Then
                MessageBox.Show("Please enter Collection Container Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtCode.Text.Trim.Replace("'", "") = "" Then
                MessageBox.Show("Please enter Collection Container code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtContainer.Text.Trim.Replace("'", "") = "" Then
                MessageBox.Show("Please enter Collection  Container name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtContainer.Text = ""
                txtContainer.Select()
                Exit Sub
            End If

            If blnIsModify = True Then

                'validation for duplicate entries for add
                If oLabContainer.IsCodeExists(txtCode.Text.Trim.Replace("'", "")) = True And oLabContainer.IsExists(txtContainer.Text.Trim.Replace("'", "")) = True Then
                    MessageBox.Show("Duplicate Collection Container Code and Name.Please enter another Collection Container Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                If oLabContainer.IsCodeExists(txtCode.Text.Trim.Replace("'", "")) = True Then
                    MessageBox.Show("Duplicate Collection Container Code.Please enter another Collection Container Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                If oLabContainer.IsExists(txtContainer.Text) = True Then
                    MessageBox.Show("Duplicate Collection Container Name.Please enter another Collection Container Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtContainer.Text = ""
                    txtContainer.Select()
                    Exit Sub
                End If

                With oLabContainer.LabCollectionContainer
                    .LabCollectionContainerID = 0
                    .LabCollectionContainerCode = txtCode.Text
                    .LabCollectionContainerName = txtContainer.Text
                End With
                oLabContainer.Add()
            Else
                'validation for duplicate entries for modify
                If UCase(txtCode.Text) <> UCase(sEditCode) And UCase(txtContainer.Text) <> UCase(sEditName) Then
                    If oLabContainer.IsCodeExists(txtCode.Text) = True And oLabContainer.IsExists(txtContainer.Text) = True Then
                        MessageBox.Show("Duplicate Collection Container Code and Name.Please enter another Collection Container Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtContainer.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If
                If UCase(txtCode.Text) <> UCase(sEditCode) Then
                    If oLabContainer.IsCodeExists(txtCode.Text) = True Then
                        MessageBox.Show("Duplicate Collection Container Code.Please enter another Collection Container Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If
                If UCase(txtContainer.Text) <> UCase(sEditName) Then
                    If oLabContainer.IsExists(txtContainer.Text) = True Then
                        MessageBox.Show("Duplicate Collection Container Name.Please enter another Collection Container Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtContainer.Text = ""
                        txtContainer.Select()
                        Exit Sub
                    End If
                End If

                With oLabContainer.LabCollectionContainer
                    .LabCollectionContainerID = nEditID
                    .LabCollectionContainerCode = txtCode.Text
                    .LabCollectionContainerName = txtContainer.Text
                End With

                oLabContainer.Modify(nEditID)
            End If

            'if blnIsModify = true ---- means add mode
            If blnIsModify = True Then

                txtCode.Text = ""
                txtContainer.Text = ""
                txtCode.Select()
                ' Else
                ' Me.Close()
            End If

            'sarika 18th june 07

            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub tlsp_LabCollection_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_LabCollection.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                saveLabCollection()

            Case "Close"
                Me.Close()

        End Select
    End Sub
End Class