Public Class frmMedReconciliation

    Private _dtMed As DataTable = Nothing
    Public dtTemp As DataTable
    Private CheckStatus As String = "A"
    Private _reconcialtiontype As Int16
    Public RecUpdated As Boolean = False

    Public Property dtMed() As DataTable
        Get
            Return _dtMed
        End Get
        Set(ByVal value As DataTable)
            _dtMed = value
        End Set
    End Property
    
    Public Property Reconcialtiontype As Int16
        Get
            Return _reconcialtiontype
        End Get
        Set(value As Int16)
            _reconcialtiontype = value
        End Set
    End Property

    Dim _PatientID As Long = 0
    'Public Property patientID As Long
    '    Get
    '        Return _patientID
    '    End Get
    '    Set(ByVal Value As Long)
    '        _patientID = Value
    '    End Set
    'End Property
    Public Sub New(ByVal PatientID As Long)

        InitializeComponent()
        _patientID = PatientID


    End Sub
    Private Sub frmMedReconciliation_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       
        If Not IsNothing(dtMed) Then
            If (dtMed.Rows.Count > 0) Then

                CheckStatus = "M"
                If dtMed.Rows(0)("SummaryCheckBox") = True Then
                    chkSummary.Checked = True
                End If

                If dtMed.Rows(0)("MedicationCheckBox") = True Then
                    chkMedication.Checked = True
                    chkSummary.Checked = True
                End If

                dtMedDate.Value = Convert.ToDateTime(dtMed.Rows(0)("MedDate"))
                txtNotes.Text = dtMed.Rows(0)("Notes").ToString()



                If Reconcialtiontype = 1 Then
                    dtMed.Rows(0)("ReconcillationType") = gloEMRGeneralLibrary.clsInfobutton.MedReconciliationenumSource.ProblemList.GetHashCode()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ClinicalReconciliation, gloAuditTrail.ActivityType.View, "Medication Reconciliation View From Problem List", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                ElseIf Reconcialtiontype = 2 Then

                    dtMed.Rows(0)("ReconcillationType") = gloEMRGeneralLibrary.clsInfobutton.MedReconciliationenumSource.Medication.GetHashCode()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ClinicalReconciliation, gloAuditTrail.ActivityType.View, "Medication Reconciliation View From Medication", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                ElseIf Reconcialtiontype = 3 Then

                    dtMed.Rows(0)("ReconcillationType") = gloEMRGeneralLibrary.clsInfobutton.MedReconciliationenumSource.History.GetHashCode()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.ClinicalReconciliation, gloAuditTrail.ActivityType.View, "Medication Reconciliation View From History", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                End If



            End If
        End If
        If Reconcialtiontype = 1 Then
            Me.Icon = gloEMR.My.Resources.ProblemListReconciliation
        ElseIf Reconcialtiontype = 3 Then
            Me.Icon = gloEMR.My.Resources.HistoryReconciliation
        End If

    End Sub




    Private Sub tlsbtnSave_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnSave.Click

        Dim dtTemp As New DataTable()

        Dim cPatientID As New DataColumn("PatientID", GetType(Long))
        Dim cVisitID As New DataColumn("VisitID", GetType(Long))
        Dim cSummaryCheckBox As New DataColumn("SummaryCheckBox", GetType(Boolean))
        Dim cMedicationCheckBox As New DataColumn("MedicationCheckBox", GetType(Boolean))
        Dim cMedDate As New DataColumn("MedDate", GetType(DateTime))
        Dim cNotes As New DataColumn("Notes", GetType(String))
        Dim cRowState As New DataColumn("RowState", GetType(String))
        Dim cReconcillationType As New DataColumn("ReconcillationType", GetType(Int16))

        dtTemp.Columns.Add(cPatientID)
        dtTemp.Columns.Add(cVisitID)
        dtTemp.Columns.Add(cSummaryCheckBox)
        dtTemp.Columns.Add(cMedicationCheckBox)
        dtTemp.Columns.Add(cMedDate)
        dtTemp.Columns.Add(cNotes)
        dtTemp.Columns.Add(cRowState)
        dtTemp.Columns.Add(cReconcillationType)

        Dim drTemp As DataRow = dtTemp.NewRow()

        drTemp("PatientID") = 0
        drTemp("VisitID") = 0

        If chkSummary.Checked = True Then
            drTemp("SummaryCheckBox") = True
           
        Else
            drTemp("SummaryCheckBox") = False

        End If

        If chkMedication.Checked = True Then
            drTemp("MedicationCheckBox") = True
        Else
            drTemp("MedicationCheckBox") = False
        End If

        drTemp("MedDate") = dtMedDate.Value
        drTemp("Notes") = txtNotes.Text.Trim()

        drTemp("RowState") = CheckStatus

        RecUpdated = True ''if reconciliation upadted
        If Reconcialtiontype = 1 Then
            drTemp("ReconcillationType") = gloEMRGeneralLibrary.clsInfobutton.MedReconciliationenumSource.ProblemList.GetHashCode()

        ElseIf Reconcialtiontype = 2 Then

            drTemp("ReconcillationType") = gloEMRGeneralLibrary.clsInfobutton.MedReconciliationenumSource.Medication.GetHashCode()

        ElseIf Reconcialtiontype = 3 Then

            drTemp("ReconcillationType") = gloEMRGeneralLibrary.clsInfobutton.MedReconciliationenumSource.History.GetHashCode()
        End If

      
      


        dtTemp.Rows.Add(drTemp)

        dtMed = dtTemp

        Me.Close()

    End Sub

    Private Sub tlsbtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnClose.Click
        RecUpdated = False
        Me.Close()

    End Sub

 

    Private Sub chkMedication_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMedication.CheckedChanged
        If chkMedication.Checked = True Then
            chkSummary.Checked = True
        Else
            chkSummary.Checked = False
        End If
    End Sub
End Class