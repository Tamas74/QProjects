Imports gloEMRGeneralLibrary.gloEMRActors
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmProviderNADEAN

    '------
    'Dim oAddresscontrol As gloAddress.gloAddressControl = Nothing
    
    Public Property NADEANNumber As String = ""

    Private Sub frmLab_ContactInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
         
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub tlsp_ContactInformation_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ContactInformation.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"

                If Len(Trim(txtNADEAN.Text)) > 0 And Len(Trim(txtNADEAN.Text)) < 9 Then
                    MessageBox.Show("Please enter valid NADEAN number", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNADEAN.Focus()
                    Exit Sub
                End If

                NADEANNumber = txtNADEAN.Text.Trim()

            Case "Close"
                NADEANNumber = ""
        End Select

        Me.Close()

    End Sub


End Class