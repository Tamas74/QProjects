Public Class frmViewCarePlan_V2
    Dim _nPatientId As Int64
    Private WithEvents gloUC_PatientStrip As gloUserControlLibrary.gloUC_PatientStrip
    Dim dtData As DataTable


    Dim sSelectedTabName As String = "" ''variable to hold selected tab name which will be used in all select case statements


    Public WriteOnly Property OpenAddWindow() As Boolean

        Set(ByVal value As Boolean)

            If value = True Then

                'Call Add only if no existing care plan present
                'If IsNothing(C1_CarePlan) = True OrElse C1_CarePlan.Rows.Count <= 1 Then

                '    Call ts_btnAdd_Click(Nothing, Nothing)

                'End If

            End If

        End Set
    End Property

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID
    End Sub


    Private Sub frmViewCarePlan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        sSelectedTabName = tbCarePlan.SelectedTab.Text
        ts_btnHealtConcern.Visible = False
        ts_btnGoal.Visible = False
        ts_btnIntervention.Visible = False
        ts_btnOutcome.Visible = False
        Set_PatientDetailStrip()

        FillHealthConcerns()
        FillGoals()
        FillInterventions()
        FillOutcomesAndEvaluations()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        Try
            gloUC_PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip

            With gloUC_PatientStrip
                .Dock = DockStyle.Top
                .Padding = New Padding(3, 0, 3, 0)

                .ShowDetail(_nPatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ChiefComplaint)

                .SendToBack()
                '.DTPValue = Format(Now, "MM/dd/yyyy")
                .DTPEnabled = False
            End With
            Me.Controls.Add(gloUC_PatientStrip)
            pnlToolStrip.SendToBack()
            Pnl_main.BringToFront()
            'C1_CarePlan.BringToFront()
            tbCarePlan.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewCarePlan_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not IsNothing(gloUC_PatientStrip) Then
                gloUC_PatientStrip.Dispose()
                gloUC_PatientStrip = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillHealthConcerns()
        Try
            dtData = Nothing
            Using objPatientHealthConcern As New ClsHealthConcern()
                dtData = objPatientHealthConcern.GetPatientHealthConcern(_nPatientId).Tables(0)
            End Using

            If dtData IsNot Nothing Then
                'C1_HealthConcern.Clear()
                C1_HealthConcern.DataSource = Nothing
                C1_HealthConcern.DataSource = dtData.DefaultView
                DesignHealthConcernGrid()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillGoals()
        Try
            dtData = Nothing
            Using objPatientGoal As New ClsGoal()
                dtData = objPatientGoal.GetPatientGoal(_nPatientId).Tables(0)
            End Using

            If dtData IsNot Nothing Then
                'C1_Goals.Clear()
                C1_Goals.DataSource = Nothing
                C1_Goals.DataSource = dtData.DefaultView
                DesignGoalsGrid()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillInterventions()
        Try
            dtData = Nothing
            Using objPatientIntervention As New ClsIntervention()
                dtData = objPatientIntervention.GetPatientIntervention(_nPatientId).Tables(0)
            End Using

            If dtData IsNot Nothing Then
                'C1_Interventions.Clear()
                C1_Interventions.DataSource = Nothing
                C1_Interventions.DataSource = dtData.DefaultView
                DesignInterventionsGrid()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillOutcomesAndEvaluations()
        Try
            dtData = Nothing
            Using objPatientOutcome As New ClsOutcome()
                dtData = objPatientOutcome.GetPatientOutcome(_nPatientId).Tables(0)
            End Using

            If dtData IsNot Nothing Then
                'C1_Outcomes.Clear()
                C1_Outcomes.DataSource = Nothing
                C1_Outcomes.DataSource = dtData.DefaultView
                DesignOutcomesAndEvaluatiosGrid()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub DesignHealthConcernGrid()
        Try

            gloC1FlexStyle.Style(C1_HealthConcern)
            C1_HealthConcern.Rows.Fixed = 1


            C1_HealthConcern.Cols("nHealthConcernID").Visible = False
            C1_HealthConcern.Cols("dtHealthConcernDate").Visible = False

            C1_HealthConcern.Cols("sHealthConcernName").Caption = "Concern Name"
            C1_HealthConcern.Cols("sHealthConcernAuthor").Caption = "Concern From"
            C1_HealthConcern.Cols("sHealthConcernStatus").Caption = "Concern Status"
            C1_HealthConcern.Cols("dtHealthConcernStartDate").Caption = "Start Date"
            C1_HealthConcern.Cols("dtHealthConcernStartDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_HealthConcern.Cols("dtHealthConcernEndDate").Caption = "End Date"
            C1_HealthConcern.Cols("dtHealthConcernEndDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_HealthConcern.Cols("sHealthConcernNotes").Caption = "Notes"
            C1_HealthConcern.Cols("dtHealthConcernDate").Caption = "Date"
            C1_HealthConcern.Cols("dtcreateddate").Caption = "Created Date"
            C1_HealthConcern.Cols("dtcreateddate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_HealthConcern.Cols("dtHealthConcernRecordedDate").Caption = "Last Modified"
            C1_HealthConcern.Cols("dtHealthConcernRecordedDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_HealthConcern.Cols("sUsername").Caption = "User"

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 56
            C1_HealthConcern.Cols("sHealthConcernName").Width = wdth * 0.13
            C1_HealthConcern.Cols("sHealthConcernAuthor").Width = wdth * 0.08
            C1_HealthConcern.Cols("sHealthConcernStatus").Width = wdth * 0.08
            C1_HealthConcern.Cols("dtHealthConcernStartDate").Width = wdth * 0.12
            C1_HealthConcern.Cols("dtHealthConcernEndDate").Width = wdth * 0.12
            C1_HealthConcern.Cols("sHealthConcernNotes").Width = wdth * 0.15
            C1_HealthConcern.Cols("dtcreateddate").Width = wdth * 0.12
            C1_HealthConcern.Cols("dtHealthConcernRecordedDate").Width = wdth * 0.12
            C1_HealthConcern.Cols("sUsername").Width = wdth * 0.08

            'C1_HealthConcern.ShowCellLabels = True
            C1_HealthConcern.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_HealthConcern.Redraw = True
            C1_HealthConcern.Cols("sHealthConcernName").AllowEditing = False
            C1_HealthConcern.Cols("sHealthConcernAuthor").AllowEditing = False
            C1_HealthConcern.Cols("sHealthConcernStatus").AllowEditing = False
            C1_HealthConcern.Cols("dtHealthConcernStartDate").AllowEditing = False
            C1_HealthConcern.Cols("dtHealthConcernEndDate").AllowEditing = False
            C1_HealthConcern.Cols("sHealthConcernNotes").AllowEditing = False
            C1_HealthConcern.Cols("dtcreateddate").AllowEditing = False
            C1_HealthConcern.Cols("dtHealthConcernRecordedDate").AllowEditing = False
            C1_HealthConcern.Cols("sUsername").AllowEditing = False
            C1_HealthConcern.Cols("dtHealthConcernDate").AllowEditing = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub DesignGoalsGrid()
        Try

            gloC1FlexStyle.Style(C1_Goals)
            C1_Goals.Rows.Fixed = 1


            C1_Goals.Cols("nGoalId").Visible = False
            C1_Goals.Cols("sGoalName").Caption = "Name"
            C1_Goals.Cols("sGoalLoin").Caption = "LOINC"
            C1_Goals.Cols("sGoalValue").Caption = "Value"
            C1_Goals.Cols("sGoalUnit").Caption = "Unit"
            C1_Goals.Cols("sGoalAuthor").Caption = "Goal From"
            C1_Goals.Cols("dtGoalRecordedDate").Caption = "Last Modified"
            C1_Goals.Cols("dtGoalRecordedDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Goals.Cols("sGoalNotes").Caption = "Notes"
            C1_Goals.Cols("dtGoalDate").Caption = "Goal Date"
            C1_Goals.Cols("dtGoalDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Goals.Cols("dtGoalTargetDate").Caption = "Target Date"
            C1_Goals.Cols("dtGoalTargetDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Goals.Cols("dtcreateddate").Caption = "Created Date"
            C1_Goals.Cols("dtcreateddate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Goals.Cols("sUsername").Caption = "User"

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 56
            C1_Goals.Cols("sGoalName").Width = wdth * 0.11
            C1_Goals.Cols("sGoalAuthor").Width = wdth * 0.06
            C1_Goals.Cols("sGoalLoin").Width = wdth * 0.11
            C1_Goals.Cols("sGoalValue").Width = wdth * 0.05
            C1_Goals.Cols("sGoalUnit").Width = wdth * 0.05
            C1_Goals.Cols("sGoalNotes").Width = wdth * 0.15
            C1_Goals.Cols("dtcreateddate").Width = wdth * 0.12
            C1_Goals.Cols("dtGoalRecordedDate").Width = wdth * 0.12
            C1_Goals.Cols("dtGoalDate").Width = wdth * 0.12
            C1_Goals.Cols("dtGoalTargetDate").Width = wdth * 0.12
            C1_Goals.Cols("sUsername").Width = wdth * 0.1

            'C1_Goals.ShowCellLabels = True
            C1_Goals.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_Goals.Redraw = True
            C1_Goals.Cols("sGoalName").AllowEditing = False
            C1_Goals.Cols("sGoalAuthor").AllowEditing = False
            C1_Goals.Cols("sGoalLoin").AllowEditing = False
            C1_Goals.Cols("sGoalValue").AllowEditing = False
            C1_Goals.Cols("sGoalUnit").AllowEditing = False
            C1_Goals.Cols("sGoalNotes").AllowEditing = False
            C1_Goals.Cols("dtcreateddate").AllowEditing = False
            C1_Goals.Cols("dtGoalRecordedDate").AllowEditing = False
            C1_Goals.Cols("dtGoalDate").AllowEditing = False
            C1_Goals.Cols("dtGoalTargetDate").AllowEditing = False
            C1_Goals.Cols("sUsername").AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DesignInterventionsGrid()
        Try

            gloC1FlexStyle.Style(C1_Interventions)
            C1_Interventions.Rows.Fixed = 1


            C1_Interventions.Cols("nInterventionId").Visible = False
            C1_Interventions.Cols("sInterventionName").Caption = "Name"
            C1_Interventions.Cols("sInterventionType").Caption = "Intervention Type"
            C1_Interventions.Cols("sInterventionNotes").Caption = "Notes"
            C1_Interventions.Cols("dtInterventionRecordedDate").Caption = "Last Modified"
            C1_Interventions.Cols("dtInterventionRecordedDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Interventions.Cols("dtcreateddate").Caption = "Created Date"
            C1_Interventions.Cols("dtcreateddate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Interventions.Cols("sUsername").Caption = "User"
            C1_Interventions.Cols("dtInterventionDate").Caption = "Intervention Date"
            C1_Interventions.Cols("dtInterventionDate").Format = "MM/dd/yyyy h:mm:ss tt"

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 56
            C1_Interventions.Cols("sInterventionName").Width = wdth * 0.15
            C1_Interventions.Cols("sInterventionType").Width = wdth * 0.1
            C1_Interventions.Cols("sInterventionNotes").Width = wdth * 0.2
            C1_Interventions.Cols("dtInterventionRecordedDate").Width = wdth * 0.15
            C1_Interventions.Cols("dtcreateddate").Width = wdth * 0.15
            C1_Interventions.Cols("sUsername").Width = wdth * 0.1
            C1_Interventions.Cols("dtInterventionDate").Width = wdth * 0.15

            'C1_Interventions.ShowCellLabels = True
            C1_Interventions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_Interventions.Redraw = True
            C1_Interventions.Cols("sInterventionName").AllowEditing = False
            C1_Interventions.Cols("sInterventionType").AllowEditing = False
            C1_Interventions.Cols("sInterventionNotes").AllowEditing = False
            C1_Interventions.Cols("dtInterventionRecordedDate").AllowEditing = False
            C1_Interventions.Cols("dtcreateddate").AllowEditing = False
            C1_Interventions.Cols("sUsername").AllowEditing = False
            C1_Interventions.Cols("dtInterventionDate").AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DesignOutcomesAndEvaluatiosGrid()
        Try

            gloC1FlexStyle.Style(C1_Outcomes)
            C1_Outcomes.Rows.Fixed = 1


            C1_Outcomes.Cols("nOutcomeID").Visible = False
            C1_Outcomes.Cols("sOutcomeName").Caption = "Name"
            C1_Outcomes.Cols("sOutcomeStatus").Caption = "Status"
            C1_Outcomes.Cols("sOutcomeNotes").Caption = "Notes"
            C1_Outcomes.Cols("dtcreateddate").Caption = "Created Date"
            C1_Outcomes.Cols("dtcreateddate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Outcomes.Cols("dtOutcomeRecordedDate").Caption = "Last Modified"
            C1_Outcomes.Cols("dtOutcomeRecordedDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Outcomes.Cols("sUsername").Caption = "User"
            C1_Outcomes.Cols("dtOutcomeDate").Caption = "Outcome Date"
            C1_Outcomes.Cols("dtOutcomeDate").Format = "MM/dd/yyyy h:mm:ss tt"
            C1_Outcomes.Cols("sUnit").Caption = "Unit"
            C1_Outcomes.Cols("sValue").Caption = "Value"

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 56
            C1_Outcomes.Cols("sOutcomeName").Width = wdth * 0.12
            C1_Outcomes.Cols("sOutcomeStatus").Width = wdth * 0.1
            C1_Outcomes.Cols("sOutcomeNotes").Width = wdth * 0.14
            C1_Outcomes.Cols("dtcreateddate").Width = wdth * 0.12
            C1_Outcomes.Cols("dtOutcomeRecordedDate").Width = wdth * 0.12
            C1_Outcomes.Cols("sUsername").Width = wdth * 0.1
            C1_Outcomes.Cols("dtOutcomeDate").Width = wdth * 0.12
            C1_Outcomes.Cols("sUnit").Width = wdth * 0.09
            C1_Outcomes.Cols("sValue").Width = wdth * 0.09

            'C1_Outcomes.ShowCellLabels = True
            C1_Outcomes.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_Outcomes.Redraw = True
            C1_Outcomes.Cols("sOutcomeName").AllowEditing = False
            C1_Outcomes.Cols("sOutcomeStatus").AllowEditing = False
            C1_Outcomes.Cols("sOutcomeNotes").AllowEditing = False
            C1_Outcomes.Cols("dtcreateddate").AllowEditing = False
            C1_Outcomes.Cols("dtOutcomeRecordedDate").AllowEditing = False
            C1_Outcomes.Cols("sUsername").AllowEditing = False
            C1_Outcomes.Cols("dtOutcomeDate").AllowEditing = False
            C1_Outcomes.Cols("sUnit").AllowEditing = False
            C1_Outcomes.Cols("sValue").AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnAdd.Click
        Try

            Select Case sSelectedTabName
                Case "Health Concerns"
                    Dim ofrm As New frmPatientHealthConcern(_nPatientId, GenerateVisitID(Now.Date, _nPatientId))
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillHealthConcerns()
                Case "Goals"
                    Dim ofrm As New frmPatientGoal(_nPatientId, GenerateVisitID(Now.Date, _nPatientId))
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillGoals()
                Case "Interventions"
                    Dim ofrm As New frmPatientIntervention(_nPatientId, GenerateVisitID(Now.Date, _nPatientId))
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillInterventions()
                Case "Evaluations and Outcomes"
                    Dim ofrm As New frmPatientOutcome(_nPatientId, GenerateVisitID(Now.Date, _nPatientId))
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillOutcomesAndEvaluations()

            End Select


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnModify_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnModify.Click
        Try
            Dim _nID As Int64

            Select Case sSelectedTabName
                Case "Health Concerns"
                    If (C1_HealthConcern.Rows.Count > 1) Then
                        If (C1_HealthConcern.RowSel > 0) Then
                            _nID = Convert.ToInt64(C1_HealthConcern.GetData(C1_HealthConcern.RowSel, 0))
                            Dim ofrm As New frmPatientHealthConcern(_nPatientId, 0, _nID)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            FillHealthConcerns()
                        Else
                            MessageBox.Show("Please select record to modify", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                Case "Goals"
                    If (C1_Goals.Rows.Count > 1) Then
                        If (C1_Goals.RowSel > 0) Then
                            _nID = Convert.ToInt64(C1_Goals.GetData(C1_Goals.RowSel, 0))
                            Dim ofrm As New frmPatientGoal(_nPatientId, 0, _nID)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            FillGoals()
                        Else
                            MessageBox.Show("Please select record to modify", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                Case "Interventions"
                    If (C1_Interventions.Rows.Count > 1) Then
                        If (C1_Interventions.RowSel > 0) Then
                            _nID = Convert.ToInt64(C1_Interventions.GetData(C1_Interventions.RowSel, 0))
                            Dim ofrm As New frmPatientIntervention(_nPatientId, 0, _nID)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            FillInterventions()
                        Else
                            MessageBox.Show("Please select record to modify", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If


                Case "Evaluations and Outcomes"
                    If (C1_Outcomes.Rows.Count > 1) Then
                        If (C1_Outcomes.RowSel > 0) Then
                            _nID = Convert.ToInt64(C1_Outcomes.GetData(C1_Outcomes.RowSel, 0))
                            Dim ofrm As New frmPatientOutcome(_nPatientId, 0, _nID)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            FillOutcomesAndEvaluations()
                        Else
                            MessageBox.Show("Please select record to modify", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnDelete.Click
        Try
            Dim _nID As Int64
            Dim objCarePlan As ClsCarePlan_V2 = New ClsCarePlan_V2()
            Select Case sSelectedTabName
                Case "Health Concerns"
                    If (C1_HealthConcern.Rows.Count > 1) Then
                        If (C1_HealthConcern.RowSel > 0) Then
                            If MessageBox.Show("Are you sure you want to Delete the current record ?", "QEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                _nID = Convert.ToInt64(C1_HealthConcern.GetData(C1_HealthConcern.RowSel, 0))
                                If objCarePlan.canDelete(_nID, "HealthConcern") Then
                                    Using ObjHealthConcern As New ClsHealthConcern()
                                        ObjHealthConcern.DeletePatientHealthConcern(_nID, _nPatientId)
                                    End Using
                                    FillHealthConcerns()
                                End If
                            End If
                        Else
                            MessageBox.Show("Please select record to delete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Case "Goals"
                    If (C1_Goals.Rows.Count > 1) Then
                        If (C1_Goals.RowSel > 0) Then
                            If MessageBox.Show("Are you sure you want to Delete the current record ?", "QEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                _nID = Convert.ToInt64(C1_Goals.GetData(C1_Goals.RowSel, 0))
                                If objCarePlan.canDelete(_nID, "Goal") Then
                                    Using ObjGoal As New ClsGoal()
                                        ObjGoal.DeletePatientGoal(_nID, _nPatientId)
                                    End Using
                                    FillGoals()
                                End If
                            End If
                        Else
                            MessageBox.Show("Please select record to delete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Case "Interventions"
                    If (C1_Interventions.Rows.Count > 1) Then
                        If (C1_Interventions.RowSel > 0) Then
                            If MessageBox.Show("Are you sure you want to Delete the current record ?", "QEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                _nID = Convert.ToInt64(C1_Interventions.GetData(C1_Interventions.RowSel, 0))
                                If objCarePlan.canDelete(_nID, "Intervention") Then
                                    Using ObjIntervention As New ClsIntervention()
                                        ObjIntervention.DeletePatientIntervention(_nID, _nPatientId)
                                    End Using
                                    FillInterventions()
                                End If
                            End If
                        Else
                            MessageBox.Show("Please select record to delete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Case "Evaluations and Outcomes"
                    If (C1_Outcomes.Rows.Count > 1) Then
                        If (C1_Outcomes.RowSel > 0) Then
                            If MessageBox.Show("Are you sure you want to Delete the current record ?", "QEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                _nID = Convert.ToInt64(C1_Outcomes.GetData(C1_Outcomes.RowSel, 0))
                                Using ObjOutcome As New ClsOutcome()
                                    ObjOutcome.DeletePatientOutcome(_nID, _nPatientId)
                                End Using
                                FillOutcomesAndEvaluations()
                            End If
                        Else
                            MessageBox.Show("Please select record to delete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

            End Select

            If objCarePlan IsNot Nothing Then
                objCarePlan.Dispose()
                objCarePlan = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            txtSearch.Text = ""

            Select Case sSelectedTabName
                Case "Health Concerns"
                    FillHealthConcerns()
                Case "Goals"
                    FillGoals()
                Case "Interventions"
                    FillInterventions()
                Case "Evaluations and Outcomes"
                    FillOutcomesAndEvaluations()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click, btnClear.TextChanged
        txtSearch.Text = ""
    End Sub
    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As New DataView()

        Select Case sSelectedTabName
            Case "Health Concerns"
                _dv = DirectCast(C1_HealthConcern.DataSource, DataView)
                If (IsNothing(_dv) = False) Then

                    C1_HealthConcern.DataSource = _dv
                    Try
                        Dim strSearch As String = txtSearch.Text.Trim()
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
                        If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                            _dv.RowFilter = (_dv.Table.Columns("sHealthConcernName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sHealthConcernAuthor").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sHealthConcernStatus").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sHealthConcernNotes").ColumnName & " Like '%") + strSearch & "%'"
                        Else
                            _dv.RowFilter = (_dv.Table.Columns("sHealthConcernName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sHealthConcernAuthor").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sHealthConcernStatus").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sHealthConcernNotes").ColumnName & " Like '%") + strSearch & "%'"
                        End If
                    Catch Ex As Exception
                        MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If

            Case "Goals"
                _dv = DirectCast(C1_Goals.DataSource, DataView)
                If (IsNothing(_dv) = False) Then


                    C1_Goals.DataSource = _dv
                    Try
                        Dim strSearch As String = txtSearch.Text.Trim()
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
                        If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                            _dv.RowFilter = (_dv.Table.Columns("sGoalName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoalLoin").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoalAuthor").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoalNotes").ColumnName & " Like '%") + strSearch & "%'"
                        Else
                            _dv.RowFilter = (_dv.Table.Columns("sGoalName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoalLoin").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoalAuthor").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoalNotes").ColumnName & " Like '%") + strSearch & "%'"
                        End If
                    Catch Ex As Exception
                        MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Case "Interventions"
                _dv = DirectCast(C1_Interventions.DataSource, DataView)
                If (IsNothing(_dv) = False) Then


                    C1_Interventions.DataSource = _dv
                    Try
                        Dim strSearch As String = txtSearch.Text.Trim()
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
                        If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                            _dv.RowFilter = (_dv.Table.Columns("sInterventionName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInterventionType").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInterventionNotes").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sUsername").ColumnName & " Like '%") + strSearch & "%'"
                        Else
                            _dv.RowFilter = (_dv.Table.Columns("sInterventionName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInterventionType").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInterventionNotes").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sUsername").ColumnName & " Like '%") + strSearch & "%'"
                        End If
                    Catch Ex As Exception
                        MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Case "Evaluations and Outcomes"
                _dv = DirectCast(C1_Outcomes.DataSource, DataView)
                If (IsNothing(_dv) = False) Then


                    C1_Outcomes.DataSource = _dv
                    Try
                        Dim strSearch As String = txtSearch.Text.Trim()
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
                        If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                            _dv.RowFilter = (_dv.Table.Columns("sOutcomeName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sOutcomeStatus").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sOutcomeNotes").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sUsername").ColumnName & " Like '%") + strSearch & "%'"
                        Else
                            _dv.RowFilter = (_dv.Table.Columns("sOutcomeName").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sOutcomeStatus").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sOutcomeNotes").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sUsername").ColumnName & " Like '%") + strSearch & "%'"
                        End If
                    Catch Ex As Exception
                        MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If

        End Select

    End Sub

    Private Sub ts_btnLegacyPlans_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnLegacyPlans.Click
        Try
            'If gnPatientID > 0 Then

            '    If IsAccess(False) = False Then
            '        Exit Sub
            '    End If

            '    'Dashboard Care Plan context menu option to call the patient view clinical instructions form

            Dim frm As New frmViewCarePlan(_nPatientId)

            frm.MdiParent = Me.MdiParent
            frm.ShowInTaskbar = False
            frm.WindowState = FormWindowState.Maximized
            frm.Show()

            'pnlMainToolBar.Visible = False
            'ShowHideMainMenu(False, False)
            frm.OpenAddWindow = False 'True

            'Else
            'MessageBox.Show("Select the patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'End If

            'Dim frm As New frmPatientCarePlan_V2(_nPatientId, GenerateVisitID(Now.Date, _nPatientId))
            'frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            'If Not IsNothing(frm) Then
            '    frm.Dispose()
            '    frm = Nothing
            'End If
            'SetClinicalInstructions()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnHealtConcern_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnHealtConcern.Click
        Try
            If (C1_HealthConcern.Rows.Count > 1) Then
                If (C1_HealthConcern.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_HealthConcern.GetData(C1_HealthConcern.RowSel, 0))
                    Dim ofrm As New frmPatientHealthConcern(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillHealthConcerns()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnGoal_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnGoal.Click
        Try
            If (C1_Goals.Rows.Count > 1) Then
                If (C1_Goals.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_Goals.GetData(C1_Goals.RowSel, 0))
                    Dim ofrm As New frmPatientGoal(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillGoals()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub C1_Goals_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_Goals.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then ''fixed bug 109823
                Exit Sub
            End If
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_Goals.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_Goals.Row = -1
            End If

            If (C1_Goals.Row = -1) Then
                Return
            End If

            If (C1_Goals.Rows.Count > 1) Then
                If (C1_Goals.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_Goals.GetData(C1_Goals.RowSel, 0))
                    Dim ofrm As New frmPatientGoal(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillGoals()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1_HealthConcern_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_HealthConcern.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then ''fixed bug 109823
                Exit Sub
            End If
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_HealthConcern.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_HealthConcern.Row = -1
            End If

            If (C1_HealthConcern.Row = -1) Then
                Return
            End If

            If (C1_HealthConcern.Rows.Count > 1) Then
                If (C1_HealthConcern.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_HealthConcern.GetData(C1_HealthConcern.RowSel, 0))
                    Dim ofrm As New frmPatientHealthConcern(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillHealthConcerns()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub ts_btnIntervention_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnIntervention.Click
        Try
            If (C1_Interventions.Rows.Count > 1) Then
                If (C1_Interventions.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_Interventions.GetData(C1_Interventions.RowSel, 0))
                    Dim ofrm As New frmPatientIntervention(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillInterventions()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1_Interventions_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_Interventions.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then ''fixed bug 109823
                Exit Sub
            End If
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_Interventions.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_Interventions.Row = -1
            End If

            If (C1_Interventions.Row = -1) Then
                Return
            End If

            If (C1_Interventions.Rows.Count > 1) Then
                If (C1_Interventions.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_Interventions.GetData(C1_Interventions.RowSel, 0))
                    Dim ofrm As New frmPatientIntervention(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillInterventions()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub C1_Outcomes_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_Outcomes.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then ''fixed bug 109823
                Exit Sub
            End If
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_Outcomes.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_Outcomes.Row = -1
            End If

            If (C1_Outcomes.Row = -1) Then
                Return
            End If

            If (C1_Outcomes.Rows.Count > 1) Then
                If (C1_Outcomes.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_Outcomes.GetData(C1_Outcomes.RowSel, 0))
                    Dim ofrm As New frmPatientOutcome(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillOutcomesAndEvaluations()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub tbCarePlan_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tbCarePlan.SelectedIndexChanged
        Try
            ''''whenever the tab is changed then select the first row in respective grid.

            sSelectedTabName = tbCarePlan.SelectedTab.Text ''set the selected tab name here so that it can be used everywhere in select case statement

            Select Case sSelectedTabName
                Case "Health Concerns"
                    If (C1_HealthConcern.Rows.Count > 1) Then
                        C1_HealthConcern.Row = 1
                    End If
                Case "Goals"
                    If (C1_Goals.Rows.Count > 1) Then
                        C1_Goals.Row = 1
                    End If
                Case "Interventions"
                    If (C1_Interventions.Rows.Count > 1) Then
                        C1_Interventions.Row = 1
                    End If
                Case "Evaluations and Outcomes"
                    If (C1_Outcomes.Rows.Count > 1) Then
                        C1_Outcomes.Row = 1
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub ts_btnOutcome_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnOutcome.Click
        Try
            If (C1_Outcomes.Rows.Count > 1) Then
                If (C1_Outcomes.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_Outcomes.GetData(C1_Outcomes.RowSel, 0))
                    Dim ofrm As New frmPatientOutcome(_nPatientId, 0, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    FillOutcomesAndEvaluations()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnHistory_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnHistory.Click
        Try
            Dim nID As Int64 = 0
            Dim sModule As String = ""
            Select Case sSelectedTabName
                Case "Health Concerns"
                    If (C1_HealthConcern.Rows.Count > 1) Then
                        If (C1_HealthConcern.RowSel > 0) Then
                            nID = Convert.ToInt64(C1_HealthConcern.GetData(C1_HealthConcern.RowSel, 0))
                            sModule = "HealthConcern"
                        End If
                    End If
                Case "Goals"
                    If (C1_Goals.Rows.Count > 1) Then
                        If (C1_Goals.RowSel > 0) Then
                            nID = Convert.ToInt64(C1_Goals.GetData(C1_Goals.RowSel, 0))
                            sModule = "Goal"
                        End If
                    End If
                Case "Interventions"
                    If (C1_Interventions.Rows.Count > 1) Then
                        If (C1_Interventions.RowSel > 0) Then
                            nID = Convert.ToInt64(C1_Interventions.GetData(C1_Interventions.RowSel, 0))
                            sModule = "Intervention"
                        End If
                    End If
                Case "Evaluations and Outcomes"
                    If (C1_Outcomes.Rows.Count > 1) Then
                        If (C1_Outcomes.RowSel > 0) Then
                            nID = Convert.ToInt64(C1_Outcomes.GetData(C1_Outcomes.RowSel, 0))
                            sModule = "Outcome"
                        End If
                    End If

            End Select
            If nID <> 0 Then
                Dim ofrm As New frmCarePlanHistory(_nPatientId, nID, sModule)
                ofrm.MdiParent = Me.MdiParent
                ofrm.ShowInTaskbar = False
                ofrm.WindowState = FormWindowState.Maximized
                ofrm.Show()
            Else
                MessageBox.Show("Please select record to view history", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1_HealthConcern_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_HealthConcern.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1_Goals_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_Goals.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1_Interventions_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_Interventions.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1_Outcomes_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_Outcomes.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class