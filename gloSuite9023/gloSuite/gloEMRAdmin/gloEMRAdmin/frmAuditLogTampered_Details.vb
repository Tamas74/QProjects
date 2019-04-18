Imports C1.Win.C1FlexGrid
Imports gloDatabaseLayer
Imports System.Linq


Public Class frmAuditLogTamperedDetails
    Public stamperedid As String = ""
#Region "Form attributes"
    Private Shared frmAuditLogDetails As frmAuditLogTamperedDetails
#End Region

#Region "Form Events and initialization"



    Public Shared Function GetInstance() As frmAuditLogTamperedDetails
        Try
            If frmAuditLogDetails Is Nothing Then
                frmAuditLogDetails = New frmAuditLogTamperedDetails
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return frmAuditLogDetails
    End Function

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmAuditLogTamperedDetails_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If frmAuditLogDetails IsNot Nothing Then
                frmAuditLogDetails = Nothing
            End If
            Dim frmAuditLogTampered As frmAuditLogTampered = frmAuditLogTampered.GetInstance()
            frmAuditLogTampered.refreshgrid(stamperedid)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub


#End Region
    
#Region "Button"
    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Display"

    Public Sub ResetControls()
        Try
            lblActivityDateTime.Text = String.Empty
            lblAfterActivity.Text = String.Empty

            lblCategory.Text = String.Empty
            lblAfterCategory.Text = String.Empty

            lblDescp.Text = String.Empty
            lblAfterDesc.Text = String.Empty

            lblMachineName.Text = String.Empty
            lblAfterMachineName.Text = String.Empty

            lblSoftwareComponent.Text = String.Empty
            lblAfterSoftwareComponent.Text = String.Empty

            lblOutcome.Text = String.Empty
            lblAfterOutcome.Text = String.Empty

            lblModule.Text = String.Empty
            lblAfterModule.Text = String.Empty

            lblType.Text = String.Empty
            lblAfterType.Text = String.Empty

            lblTamperingDateTime.Text = String.Empty
            lblTamperingMachineName.Text = String.Empty
            lblTamperingUserName.Text = String.Empty
            lblActionUnderTaken.Text = String.Empty
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub DisplayAfterAlterationLabels(ByVal Row As DataRow)
        Try
            lblAfterActivity.Text = Row("dtActivityDateTime").ToString
            lblAfterCategory.Text = Row("sActivityCategory").ToString
            lblAfterDesc.Text = Row("sDescription").ToString
            lblAfterMachineName.Text = Row("sMachineName").ToString
            lblAfterSoftwareComponent.Text = Row("sSoftwareComponent").ToString
            lblAfterOutcome.Text = Row("sOutcome").ToString
            lblAfterModule.Text = Row("sActivityModule").ToString
            lblAfterType.Text = Row("sActivityType").ToString
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub DisplayTamperingUserDetails(ByVal Row As DataRow)
        Try
            With lblTamperingUserName
                .Text = Row("sTamperedUserName").ToString
                .ForeColor = Color.Red
            End With

            With lblActionUnderTaken
                If Row("sActionType").ToString.ToLower = "updated" Then
                    .Text = "Record changed"
                ElseIf Row("sActionType").ToString.ToLower = "deleted" Then
                    .Text = "Record deleted"
                End If


                .ForeColor = Color.Red
            End With


            With lblTamperingDateTime
                .Text = Row("dtTamperedDateTime").ToString
                .ForeColor = Color.Red
            End With

            With lblTamperingMachineName
                .Text = Row("sTamperedMachineName").ToString
                .ForeColor = Color.Red
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub



    Public Function ChangeTextColor(ByVal OriginalValue As Label, ByVal AlteredValue As Label) As Boolean
        Dim bChanged As Boolean = False
        Try
            If OriginalValue.Text.ToString.Trim <> AlteredValue.Text.ToString.Trim Then
                AlteredValue.ForeColor = Color.Red
                bChanged = True
            Else
                Dim color As New Color
                color.FromArgb(31, 73, 125)
                AlteredValue.ForeColor = color
                color = Nothing

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return bChanged
    End Function

    Public Sub DisplayCurrentLogLabels(ByVal Row As DataRow)
        Try
            lblActivityDateTime.Text = Row("dtActivityDateTime").ToString
            lblCategory.Text = Row("sActivityCategory").ToString
            lblDescp.Text = Row("sDescription").ToString
            lblMachineName.Text = Row("sMachineName").ToString
            lblSoftwareComponent.Text = Row("sSoftwareComponent").ToString
            lblOutcome.Text = Row("sOutcome").ToString
            lblModule.Text = Row("sActivityModule").ToString
            lblType.Text = Row("sActivityType").ToString
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub ChangeModifiedRecordColors()
        Try
            ChangeTextColor(lblActivityDateTime, lblAfterActivity)
            ChangeTextColor(lblModule, lblAfterModule)
            ChangeTextColor(lblOutcome, lblAfterOutcome)
            ChangeTextColor(lblSoftwareComponent, lblAfterSoftwareComponent)
            ChangeTextColor(lblMachineName, lblAfterMachineName)
            ChangeTextColor(lblCategory, lblAfterCategory)
            ChangeTextColor(lblDescp, lblAfterDesc)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub DisplayDeletedView()
        Try
            pnlRecordDeleted.Visible = True
            pnlAfterAlterationLabels.Visible = False

            lblRowDeleted.Text = "This log entry was deleted from the database."

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub DisplayAuditTrailIDUpdatedView()
        Try
            pnlRecordDeleted.Visible = True
            pnlAfterAlterationLabels.Visible = False

            lblRowDeleted.Text = "The internal identification tag of this entry was modified."

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub DisplayNormalUpdatedView()
        Try
            pnlRecordDeleted.Visible = False
            pnlAfterAlterationLabels.Visible = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
#End Region

    Private Sub frmAuditLogTamperedDetails_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            'If tamperedlogstatus = True Then
            UpdatetamperedDetailsViewedStatus()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub UpdatetamperedDetailsViewedStatus()
        Try
            Dim DatabaseLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            Dim objDBParameters As New gloDatabaseLayer.DBParameters()
            objDBParameters.Add(New gloDatabaseLayer.DBParameter("@logtampered", 1, ParameterDirection.Input, SqlDbType.Int))
            objDBParameters.Add(New gloDatabaseLayer.DBParameter("@ntamperedid", Convert.ToInt64(stamperedid), ParameterDirection.Input, SqlDbType.BigInt))
            DatabaseLayer.Connect(False)
            DatabaseLayer.Execute("gsp_UpdateTamperedViewedStatus", objDBParameters)
            DatabaseLayer.Disconnect()
            DatabaseLayer.Disconnect()
            DatabaseLayer = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class