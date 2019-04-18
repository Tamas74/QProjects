Imports gloEMRGeneralLibrary
Imports gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer
''Imports gloEMRGeneralLibrary.gloEMRPrescription

Public Class gloFormularlyToolBarUserCtrl


    Public Event tStrpMedicationHistoryClick(ByVal ButtonType As Int16)

    'RXHUBPBM
    Public Event tlStrpRXHUBPBMClick(ByVal ButtonType As Int16)
    'PBMA
    Public Event tlStrpPBMAClick(ByVal ButtonType As Int16)
    'PBMB
    Public Event tlStrpPBMBClick(ByVal ButtonType As Int16)
    'PBMC
    Public Event tlStrpPBMCClick(ByVal ButtonType As Int16)
    'Cobo item 
    Public Event tlStrpPBMComboClick(ByVal ButtonType As Int16)

    'RxHub eligiblity button click
    'Public Event tlbbtnRxHubClick(ByVal ButtonType As Int16)

    'Public Enum EligibilityStatus
    '    NotChecked
    '    Passed
    '    Failed
    'End Enum
    Public Property CurrentEligibilityStatus As EligibilityStatus


    Public ReadOnly Property SelectedPBM() As String
        Get
            Dim pbm As String = Nothing

            If tlStrpPBMCombo.SelectedIndex >= 0 Then
                Dim _strPBMInfo As String() = Split(tlStrpPBMCombo.Text, "-")
                If _strPBMInfo.Length = 1 Then
                    pbm = _strPBMInfo(0)
                ElseIf _strPBMInfo.Length > 1 Then
                    pbm = _strPBMInfo(0)
                End If
            End If
            Return pbm
        End Get
    End Property

    Public ReadOnly Property SelectedPlan() As String
        Get
            Dim plan As String = Nothing

            If tlStrpPBMCombo.SelectedIndex >= 0 Then
                Dim _strPBMInfo As String() = Split(tlStrpPBMCombo.Text, "-")
                If _strPBMInfo.Length = 1 Then
                    plan = ""
                ElseIf _strPBMInfo.Length > 1 Then
                    plan = _strPBMInfo(1)
                End If
            End If
            Return plan
        End Get
    End Property

    Public Property SenderId As String
    Public Property FormularyId As String
    Public Property CopayId As String
    Public Property CoverageID As String
    Public Property AlternativeID As String
    Public Property nRxModulePatientID As Long

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal _CurrentEligibilityStatus As EligibilityStatus, ByVal PatientID As Long)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        CurrentEligibilityStatus = _CurrentEligibilityStatus
        Me.nRxModulePatientID = PatientID
        SetEligibilityStatus()
    End Sub

    Public Property PayerID As String

    Public Property ProcessorIdentificationNumber As String
    Public Property BINLocationNumber As String
    Public Property MutuallyDefined As String
    Public Property PayerName As String
    Public Property CardHolderID As String

    Public Property GroupID As String
    Public Property GroupName As String
    Public Property PBMMemberID As String
    Public Property PersonCode As String
    Public Property RelatesToMessageID As String

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub tStrpMedicationHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpMedicationHistory.Click
        RaiseEvent tStrpMedicationHistoryClick(1)
    End Sub

    Private Sub tStrpFormularly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpFormularly.Click
        RaiseEvent tStrpMedicationHistoryClick(2)
    End Sub

    Private Sub tStrpCoverage_Copay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tStrpMedicationHistoryClick(3)
    End Sub

    Private Sub tlStrpRXHUBPBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tlStrpRXHUBPBMClick(4)
    End Sub

    Private Sub tlStrpPBMA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tlStrpPBMAClick(5)
    End Sub

    Private Sub tlStrpPBMB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tlStrpPBMBClick(5)
    End Sub

    Private Sub tlStrpPBMC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent tlStrpPBMCClick(6)
    End Sub

    Public Sub UpdatePBM()
        Try
            If (tlStrpPBMCombo.SelectedIndex >= 0) Then
                Using _RxBusinessLayer As New gloEMRPrescription.RxBusinesslayer(nRxModulePatientID)
                    Using dtPBMData As DataTable = _RxBusinessLayer.Get271Information(nRxModulePatientID)
                        If Not IsNothing(dtPBMData) AndAlso dtPBMData.Rows.Count > 0 Then
                            For Each DataRow As DataRow In dtPBMData.Rows
                                If Convert.ToString(DataRow("spbm_payermemberid")) = tlStrpPBMCombo.ComboBox.SelectedValue Then
                                    Me.SenderId = DataRow("sPBM_PayerParticipantID")
                                    Me.FormularyId = DataRow("sFormularyListID")
                                    Me.CoverageID = DataRow("sCoverageId")
                                    Me.CopayId = DataRow("sCopayID")
                                    Me.AlternativeID = DataRow("sAlternativeListID")

                                    Me.PayerID = Convert.ToString(DataRow("sPBM_PayerParticipantID"))
                                    Me.MutuallyDefined = Convert.ToString(DataRow("MutuallyDefined"))
                                    Me.PayerName = Convert.ToString(DataRow("sPBM_PayerName"))
                                    Me.CardHolderID = Convert.ToString(DataRow("sCardHolderID"))
                                    Me.GroupID = Convert.ToString(DataRow("sGroupID"))
                                    Me.GroupName = Convert.ToString(DataRow("sGroupName"))
                                    Me.PBMMemberID = Convert.ToString(DataRow("sPBM_PayerMemberID"))
                                    Me.RelatesToMessageID = Convert.ToString(DataRow("RelatesToMessageID")) ''''ISA13 271

                                    Dim sBIN_PCN As String = Convert.ToString(DataRow("BINLocationNumber"))
                                    If sBIN_PCN.Contains("/") Then
                                        Dim sValues As String() = sBIN_PCN.Split("/")
                                        Me.BINLocationNumber = sValues(0)

                                        If sValues.Length = 2 Then
                                            Me.ProcessorIdentificationNumber = sValues(1)
                                        End If
                                    Else
                                        Me.BINLocationNumber = Convert.ToString(DataRow("BINLocationNumber"))
                                    End If
                                    Me.PersonCode = Convert.ToString(DataRow("sPersonCode"))
                                    Exit For
                                End If
                            Next
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Eligibility, gloAuditTrail.ActivityType.General, "FS3_UpdatePBM : " + ex.InnerException.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Property LastSelectedIndex As Integer

    Public Sub tlStrpPBMCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlStrpPBMCombo.SelectedIndexChanged
        If LastSelectedIndex <> tlStrpPBMCombo.SelectedIndex Then
            If (CurrentEligibilityStatus <> gloEMRPrescription.RxBusinesslayer.EligibilityStatus.NotChecked) Then
                UpdatePBM()
            End If
            RaiseEvent tlStrpPBMComboClick(7)
            LastSelectedIndex = tlStrpPBMCombo.SelectedIndex
        End If
    End Sub

    Private Sub tlbbtn_RxHub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_RxHub.Click
        RaiseEvent tStrpMedicationHistoryClick(8)
    End Sub

    Private Sub tlStrpPBMCombo_MouseEnter(sender As System.Object, e As System.EventArgs) Handles tlStrpPBMCombo.MouseEnter
        tlStrpPBMCombo.ToolTipText = tlStrpPBMCombo.Text
    End Sub

    Private Sub tlStrpPBMCombo_MouseLeave(sender As System.Object, e As System.EventArgs) Handles tlStrpPBMCombo.MouseLeave
        tlStrpPBMCombo.ToolTipText = ""
    End Sub



    Public Sub SetEligibilityStatus()
        Try
            If Not IsNothing(CurrentEligibilityStatus) Then
                If CurrentEligibilityStatus = EligibilityStatus.Passed Then
                    '"Passed"
                    tlbbtn_RxHub.Image = gloUserControlLibrary.My.Resources.Resources.RX_eligibilityPassed
                ElseIf CurrentEligibilityStatus = EligibilityStatus.Failed Then
                    '"Failed"
                    tlbbtn_RxHub.Image = gloUserControlLibrary.My.Resources.Resources.RX_eligibilityFailed
                ElseIf CurrentEligibilityStatus = EligibilityStatus.NotChecked Then
                    '"NotChecked"
                    tlbbtn_RxHub.Image = gloUserControlLibrary.My.Resources.Resources.RX_eligibilityNotChecked
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tlbbtn_ePA_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtn_ePA.Click
        RaiseEvent tStrpMedicationHistoryClick(9)
    End Sub

    Private Sub tlbbtn_PDR_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtn_PDR.Click

    End Sub

    Private Sub tlsPDMP_Click(sender As System.Object, e As System.EventArgs) Handles tlsPDMP.Click
        RaiseEvent tStrpMedicationHistoryClick(10)
    End Sub
End Class
