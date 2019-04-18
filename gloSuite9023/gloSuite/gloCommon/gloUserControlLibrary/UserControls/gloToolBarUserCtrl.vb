Imports gloEMRGeneralLibrary
Public Class gloToolBarUserCtrl

    ''this event wil save and close the Rx-Med form
    Public Event tStrpSaveClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ''this event wil only save the Rx-Med form
    Public Event tStrpSaveRxMedClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ''this event catch on prescription form to approve refill request
    Public Event tStripApproveClick(ByVal sender As System.Object, ByVal e As System.EventArgs)


    Public Event tStrpFinishClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event tStrpPrintClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event tStrpFaxButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event tStrpCloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event tStrpShowHideClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event tStrpPrvRxClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event SendFaxNormalPriorityToolStripMenuItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event SendFaxImmediatelyToolStripMenuItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpeRxClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpRxFillClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event tStrpRefillReqButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpSendRxButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpDenyButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'Added By Shweta 20091120
    Public Event tStrpQuickPrintClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpOpenPrintDialogClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'End Shweta

    Public Event tStrpVwDeniedReportClick(ByVal sender As System.Object, ByVal e As System.EventArgs) ''''view denied report
    Public Event tStrpblCCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpbleLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpblReconcile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpPlanOfTreatment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event tStrpNKMedication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
   
    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
   
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub tStrpSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpSave.Click
        RaiseEvent tStrpSaveClick(sender, e)
    End Sub

    Protected Sub tStrpFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tStrpFinishClick(sender, e)
    End Sub
    'Commeted By Shweta 20091128 against case no: 2767, 2818 
    'Added new button for Print
    'Protected Sub tStrpPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpPrint.Click
    '    RaiseEvent tStrpPrintClick(sender, e)
    'End Sub

    Protected Sub tStrpFax_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpFax.ButtonClick
        RaiseEvent tStrpFaxButtonClick(sender, e)
    End Sub

    Protected Sub tStrpClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpClose.Click
        RaiseEvent tStrpCloseClick(sender, e)
    End Sub

    Protected Sub tStrpShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpShowHide.Click
        RaiseEvent tStrpShowHideClick(sender, e)
    End Sub


    Protected Sub tStrpPrvRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpPrvRx.Click
        RaiseEvent tStrpPrvRxClick(sender, e)
    End Sub

    Protected Sub SendFaxNormalPriorityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendFaxNormalPriorityToolStripMenuItem.Click
        RaiseEvent SendFaxNormalPriorityToolStripMenuItemClick(sender, e)
    End Sub

    Protected Sub SendFaxImmediatelyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendFaxImmediatelyToolStripMenuItem.Click
        RaiseEvent SendFaxImmediatelyToolStripMenuItemClick(sender, e)
    End Sub

    Private Sub tStrpERx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpERx.Click
        RaiseEvent tStrpeRxClick(sender, e)
    End Sub

    Private Sub tStrpRefReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tStrpRefillReqButtonClick(sender, e)
    End Sub

    Private Sub tStrpSendRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpSendRx.Click
        RaiseEvent tStrpSendRxButtonClick(sender, e)
    End Sub

 
    Private Sub tStrpDeny_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tStrpDenyButtonClick(sender, e)
    End Sub

    Private Sub tStrpSaveRxMed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpSaveRxMed.Click
        RaiseEvent tStrpSaveRxMedClick(sender, e)
    End Sub

    Private Sub tStripApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent tStripApproveClick(sender, e)
    End Sub

    'Added By Shweta 20091128
    'against case no: 2767, 2818 
    Private Sub tStrPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpPrint.ButtonClick
        RaiseEvent tStrpPrintClick(sender, e)
    End Sub
    Private Sub tStrpQuickPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpQuickPrint.Click
        RaiseEvent tStrpQuickPrintClick(sender, e)
    End Sub
    Private Sub tStrpOpenPrintDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpOpenPrintDialog.Click
        RaiseEvent tStrpOpenPrintDialogClick(sender, e)
    End Sub
    'End 20091128

    Private Sub tStrpVwDeniedReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpVwDeniedReport.Click
        RaiseEvent tStrpVwDeniedReportClick(sender, e)
    End Sub

    Private Sub tblCCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblCCD.Click
        RaiseEvent tStrpblCCD_Click(sender, e)
    End Sub

   
    Private Sub tStrpeDrugLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpeDrugLink.Click
        Try
            RaiseEvent tStrpbleLink_Click(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlb_Reconcile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Reconcile.Click
        RaiseEvent tStrpblReconcile_Click(sender, e)
    End Sub

    Private Sub tStrpRxFill_Click(sender As Object, e As System.EventArgs) Handles tStrpRxFill.Click
        RaiseEvent tStrpRxFillClick(sender, e)
    End Sub

    Private Sub tlb_PlanOfTreatment_Click(sender As System.Object, e As System.EventArgs) Handles tlb_PlanOfTreatment.Click
        RaiseEvent tStrpPlanOfTreatment_Click(sender, e)
    End Sub

    Private Sub tlb_NKMedication_Click(sender As System.Object, e As System.EventArgs) Handles tStrpNKMedications.Click
        RaiseEvent tStrpNKMedication_Click(sender, e)
    End Sub
End Class
