Imports System.Web.Mvc
Imports Word = Microsoft.Office.Interop.Word
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class frmAddVitalNorm
    Dim strVitalNorm As String
    Dim oldForm As Int16 = 0
    Dim oldTo As Int16 = 0
    Public newToAge As Int16 = 0
    Dim IsYears As Boolean
    Private Sub tblStrip_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip.ItemClicked
        tblStrip.Select()
        Select Case e.ClickedItem.Tag
            Case "Close"
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "Ok" 'Save + Close
                If (ValidateNorms() = True) Then
                    OBVitalsNorms()
                Else
                    Exit Sub
                End If
                DialogResult = Windows.Forms.DialogResult.OK
                '  Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub
    Private Sub frmAddVitalNorm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If (oldForm >= 12) Then
                txtOldFrom.Text = oldForm / 12
                lblOldFromAge.Text = "Years"
                lblNewFromAge.Text = "Years"
                IsYears = True
            Else
                txtOldFrom.Text = oldForm
                lblOldFromAge.Text = "Months"
                lblNewFromAge.Text = "Months"
            End If
            If (oldTo >= 12) Then
                txtOldTo.Text = oldTo / 12
                lblToAge.Text = "Years"
                IsYears = True
            Else
                txtOldTo.Text = oldTo
                lblToAge.Text = "Months"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New(ByVal _oldFrom As Int16, ByVal _oldTo As Int16)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        oldForm = _oldFrom
        oldTo = _oldTo
    End Sub

    Private Sub tblbtn_Ok_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Ok_32.Click

    End Sub
    Private Sub OBVitalsNorms()
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nFromAge "
            oParamater.Value = oldForm
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nNewToAge"
            oParamater.Value = newToAge
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nToAge"
            oParamater.Value = oldTo
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.ExecuteNon_Query("gsp_SaveVitalNorms")
                Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Patient Education Added", gstrLoginName, gstrClientMachineName, gnPatientID)
        Finally
            oDB.Dispose()
        End Try
    End Sub

    Private Function ValidateNorms() As Boolean
        If (oldForm >= 12) Then
            newToAge = Val(txtNewFrom.Text) * 12
        Else
            newToAge = Val(txtNewFrom.Text)
        End If
        If (oldForm >= newToAge) Then
            MessageBox.Show("Enter valid value for New to Age field", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewFrom.Focus()
            Return False
        End If
        If (newToAge >= oldTo) Then
            MessageBox.Show("Enter valid value for New to Age field", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewFrom.Focus()
            Return False
        End If
        If (newToAge >= 12) AndAlso (newToAge Mod 12 <> 0) Then
            MessageBox.Show("Enter valid value for New to Age field", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewFrom.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub txtNewFrom_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNewFrom.KeyPress
        ValidateInteger(CType(sender, TextBox).Text, e)
    End Sub
End Class