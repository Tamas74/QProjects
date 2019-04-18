Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmLab_LOINCOrderCodeMst
    Public blnIsModify As Boolean = False
    Public nEditID As Int64
    Public sEditName As String
    Public sEditCode As String
    Private Sub frmLab_LOINCOrderCodeMst_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ogloEMRLabLoincMst As New gloEMRLabLoincMst
        Try


            If nEditID > 0 Then

                Dim oLabLoincMst As LabActor.LabLoincMst


                oLabLoincMst = ogloEMRLabLoincMst.GetLabLoincMstInfo(nEditID)
                If (IsNothing(oLabLoincMst) = False) Then
                    txtCode.Text = oLabLoincMst.LabLoinc_Code
                    txtLOINCName.Text = oLabLoincMst.LabLoinc_Name
                    oLabLoincMst.Dispose()
                    oLabLoincMst = Nothing
                End If
               

            End If
            txtCode.Focus()
            txtCode.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ogloEMRLabLoincMst) Then
                ogloEMRLabLoincMst.Dispose()
                ogloEMRLabLoincMst = Nothing
            End If
        End Try
    End Sub

    Private Sub tlsp_LOINCOrderCode_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_LOINCOrderCode.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveLOINCOrderCodeMST()
                Case "Close"
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub

    Private Sub SaveLOINCOrderCodeMST()


        Dim ogloEMRLabLoincMst As New gloEMRLabLoincMst

        Try

            If txtCode.Text = "" Then
                MessageBox.Show("Please enter LOINC order code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtLOINCName.Text = "" Then

                MessageBox.Show("Please enter LOINC long name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtLOINCName.Select()
                Exit Sub
            End If



            'Do the Result grid validations
            If blnIsModify = True Then
                'check for duplicate entries for add
                If ogloEMRLabLoincMst.IsLOINCCodeExists(txtCode.Text) = True And ogloEMRLabLoincMst.IsExistsLOINCName(txtLOINCName.Text) = True Then

                    MessageBox.Show("Duplicate Code and Name. Please enter another Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)


                    txtCode.Text = ""
                    txtLOINCName.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If

                If ogloEMRLabLoincMst.IsLOINCCodeExists(txtCode.Text) = True Then
                    MessageBox.Show("Duplicate Code. Please enter another Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If

                If ogloEMRLabLoincMst.IsExistsLOINCName(txtLOINCName.Text) = True Then

                    MessageBox.Show("Duplicate Name. Please enter another Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    txtLOINCName.Text = ""
                    txtLOINCName.Select()
                    Exit Sub
                End If

                With ogloEMRLabLoincMst.LabLoinc
                    .LabLoinc_ID = 0
                    .LabLoinc_Code = txtCode.Text
                    .LabLoinc_Name = txtLOINCName.Text

                End With
                ogloEMRLabLoincMst.AddModifyLOINCCode(nEditID)
            Else

                'check for duplicate entries for modify
                If txtCode.Text <> sEditCode And txtLOINCName.Text <> sEditName Then
                    If ogloEMRLabLoincMst.IsLOINCCodeExists(txtCode.Text) = True And ogloEMRLabLoincMst.IsExistsLOINCName(txtLOINCName.Text) = True Then
                        MessageBox.Show("Duplicate Code and Name. Please enter another Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtLOINCName.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If
                If txtCode.Text <> sEditCode Then
                    If ogloEMRLabLoincMst.IsLOINCCodeExists(txtCode.Text) = True Then
                        MessageBox.Show("Duplicate Code.Please enter another Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If

                If txtLOINCName.Text <> sEditName Then
                    If ogloEMRLabLoincMst.IsExistsLOINCName(txtLOINCName.Text) = True Then

                        MessageBox.Show("Duplicate Name. Please enter another Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        txtLOINCName.Text = ""
                        txtLOINCName.Select()
                        Exit Sub
                    End If
                End If

                With ogloEMRLabLoincMst.LabLoinc
                    .LabLoinc_ID = nEditID
                    .LabLoinc_Code = txtCode.Text
                    .LabLoinc_Name = txtLOINCName.Text

                End With

                ogloEMRLabLoincMst.AddModifyLOINCCode(nEditID)

            End If

            If blnIsModify = True Then
                '//Write cleanup code for next test setup
                txtCode.Text = ""
                txtLOINCName.Text = ""
                'Else
                ' Me.Close()
            End If

            '            Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)


        Catch ex As Exception
            If blnIsModify = True Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            End If
        Finally
            If Not IsNothing(ogloEMRLabLoincMst) Then
                ogloEMRLabLoincMst.Dispose()
                ogloEMRLabLoincMst = Nothing
            End If
        End Try


    End Sub
End Class