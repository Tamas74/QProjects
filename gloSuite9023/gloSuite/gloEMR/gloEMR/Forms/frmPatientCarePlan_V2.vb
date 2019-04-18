Imports C1.Win.C1FlexGrid
Imports System.Linq

Public Class frmPatientCarePlan_V2

    Public strConceptID As String = String.Empty
    Public strDescription As String = String.Empty
    Public strInterventionConceptID As String = String.Empty
    Public strInterventionDescription As String = String.Empty

    Dim sConcernStatus As String = String.Empty
    Dim sConcernFrom As String = String.Empty
    Dim sGoalFrom As String = String.Empty
    Dim sOutcomeStatus As String = String.Empty

    Dim _CarePlanID As Long = 0
    Dim _PatientID As Long = 0
    Dim _VisitID As Long = 0

    Dim dtHealthConcern, dtHealthConcernTVP, dtHealthConcernAssociationTVP As DataTable
    Dim dtGoal, dtGoalAssociation, dtGoalTVP, dtGoalAssociationTVP As DataTable
    Dim dtIntervention, dtInterventionTVP, dtInterventionAssociationTVP As DataTable
    Dim dtOutcome, dtOutcomeTVP, dtOutcomeAssociationTVP As DataTable

    Private WithEvents dgCustomGridProblem As CustomTask

    Dim COL_SELECT As Integer = 0
    Dim COL_PROBLEMID As Integer = 1
    Dim COL_VISITID As Integer = 2
    Dim COL_ProblemSatus As Integer = 3
    Dim COL_DOS As Integer = 4
    Dim COL_COMPLAINTS As Integer = 5
    Dim COL_ConceptID As Integer = 6
    Dim COL_DIAGNOSIS As Integer = 7
    Dim COL_INFOBUTTON As Integer = 8
    Dim COL_DIAGNOSISBUTTON = 9
    Dim COL_PRESCRIPTION As Integer = 10

    Dim COL_RxBUTTON = 11
    Dim Col_UserName As Integer = 12
    Dim COL_RsDt As Integer = 13
    Dim COL_USER As Integer = 14
    Dim COL_EXAMNAMEBUTTON As Integer = 15
    Dim COL_Immediacy As Integer = 16
    Dim COL_Provider As Integer = 17
    Dim COL_Location As Integer = 18
    Dim COL_LastModified As Integer = 19
    Dim COL_ExamID As Integer = 20

    Dim COL_ProblemType As Integer = 21
    Dim COL_DescriptionID As Integer = 22
    Dim COL_SnoMedID As Integer = 23
    Dim COL_Defination As Integer = 24
    Dim Col_HiddedPrescription As Integer = 25
    Dim Col_EncounterDiagnosis As Integer = 26
    Dim Col_IsModifed As Integer = 27
    Dim Col_ICDRevision As Int16 = 28
    Dim Col_ReasonConceptID As Integer = 29
    Dim Col_ReasonConceptDesc As Integer = 30

    Dim Col_DischargeDate As Integer = 31
    Dim COLUMN_COUNT As Int16 = 32

    Enum Status
        Resolved = 1
        Active = 2
        Inactive = 3
        Chronic = 4
        All = 5
    End Enum
    Enum EnmImmediacy
        Acute = 1
        Chronic = 2
        unknown = 3
    End Enum

    Enum ConcernStatus
        Active = 1
        Completed = 2
        Inactive = 3
    End Enum

    Enum ConcernFrom
        Provider = 1
        Patient = 2
        Both = 3
    End Enum

    Enum GoalAssociationType
        HealthConcern = 1
        Problem = 2
    End Enum

    Enum GoalFrom
        Provider = 1
        Patient = 2
        Both = 3
    End Enum

    Enum OutcomeStatus
        Active = 1
        Completed = 2
        Inactive = 3
    End Enum


    Dim dtLoincUnits As DataTable = Nothing ''used on Goals table

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, Optional ByVal CarePlanID As Long = 0)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        _CarePlanID = CarePlanID
        ' Add any initialization after the InitializeComponent() call.  
    End Sub

    Private Sub frmPatientCarePlan_V2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        setHealthConcernDT()
        setGoalDT()
        setInterventionDT()
        setOutcomeDT()
        If _CarePlanID <> 0 Then
            loadCarePlanDetails()
        End If

    End Sub

    Private Sub setHealthConcernDT()
        'Table used for grid
        dtHealthConcern = New DataTable()
        For Each column As DataGridViewColumn In dgHealthConcern.Columns
            dtHealthConcern.Columns.Add(column.Name)
        Next

        'Table used for CP_TVPHealthConcerns TVP parameter values
        dtHealthConcernTVP = New DataTable()
        dtHealthConcernTVP.Columns.Add("nHealthConcernID")
        dtHealthConcernTVP.Columns.Add("nCarePlanId")
        dtHealthConcernTVP.Columns.Add("nPatientId")
        dtHealthConcernTVP.Columns.Add("sHealthConcernSnomedCode")
        dtHealthConcernTVP.Columns.Add("sHealthConcernSnomedDescription")
        dtHealthConcernTVP.Columns.Add("sHealthStatusDescription")
        dtHealthConcernTVP.Columns.Add("sHealthConcernAuthor")
        dtHealthConcernTVP.Columns.Add("sHealthConcernStatus")
        dtHealthConcernTVP.Columns.Add("dtHealthConcernStartDate")
        dtHealthConcernTVP.Columns.Add("dtHealthConcernEndDate")
        dtHealthConcernTVP.Columns.Add("sHealthConcernNotes")
        dtHealthConcernTVP.Columns.Add("dtHealthConcernRecordedDate")
        dtHealthConcernTVP.Columns.Add("sHealthConcernSectionLoincCode")
        dtHealthConcernTVP.Columns.Add("sHealthConcernLoincCode")
        dtHealthConcernTVP.Columns.Add("sHealthConcernLoincDesc")
        dtHealthConcernTVP.Columns.Add("RowState")

        'Table used for CP_TVPHealthConcernAssociation TVP parameter values
        dtHealthConcernAssociationTVP = New DataTable()
        dtHealthConcernAssociationTVP.Columns.Add("nHealthConcernAssociationID")
        dtHealthConcernAssociationTVP.Columns.Add("nCarePlanId")
        dtHealthConcernAssociationTVP.Columns.Add("nHealthConcernID")
        dtHealthConcernAssociationTVP.Columns.Add("nAssociatedConcernId")
        dtHealthConcernAssociationTVP.Columns.Add("sAssociatedConcernType")
        dtHealthConcernAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtHealthConcernAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub setGoalDT()
        dtGoal = New DataTable()
        For Each column As DataGridViewColumn In dgGoal.Columns
            dtGoal.Columns.Add(column.Name)
        Next

        dtGoalAssociation = New DataTable()
        dtGoalAssociation.Columns.Add("nGoalId")
        dtGoalAssociation.Columns.Add("AssociationType")
        dtGoalAssociation.Columns.Add("AssociationID")
        dtGoalAssociation.Columns.Add("AssociationName")

        'Table used for CP_TVPGoal TVP parameter values
        dtGoalTVP = New DataTable()
        dtGoalTVP.Columns.Add("nGoalID")
        dtGoalTVP.Columns.Add("nCarePlanId")
        dtGoalTVP.Columns.Add("nPatientId")
        dtGoalTVP.Columns.Add("sGoalLoincCode")
        dtGoalTVP.Columns.Add("sGoalLoincDescription")
        dtGoalTVP.Columns.Add("sGoalValue")
        dtGoalTVP.Columns.Add("sGoalUnit")
        dtGoalTVP.Columns.Add("sGoalNotes")
        dtGoalTVP.Columns.Add("sGoalAuthor")
        dtGoalTVP.Columns.Add("dtGoalRecordedDate")
        dtGoalTVP.Columns.Add("sGoalSectionLoincCode")
        dtGoalTVP.Columns.Add("RowState")

        'Table used for CP_TVPGoalAssociation TVP parameter values
        dtGoalAssociationTVP = New DataTable()
        dtGoalAssociationTVP.Columns.Add("nGoalAssociationID")
        dtGoalAssociationTVP.Columns.Add("nCarePlanId")
        dtGoalAssociationTVP.Columns.Add("nGoalId")
        dtGoalAssociationTVP.Columns.Add("nHealthConcernID")
        dtGoalAssociationTVP.Columns.Add("nHealthConcernAssociationID")
        dtGoalAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtGoalAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub setInterventionDT()
        dtIntervention = New DataTable()
        For Each column As DataGridViewColumn In dgIntervention.Columns
            dtIntervention.Columns.Add(column.Name)
        Next

        'Table used for CP_TVPInterventions TVP parameter values
        dtInterventionTVP = New DataTable()
        dtInterventionTVP.Columns.Add("nInterventionId")
        dtInterventionTVP.Columns.Add("nCarePlanId")
        dtInterventionTVP.Columns.Add("nPatientId")
        dtInterventionTVP.Columns.Add("sInterventionSnomedCode")
        dtInterventionTVP.Columns.Add("sInterventionSnomedDescription")
        dtInterventionTVP.Columns.Add("sInterventionNotes")
        dtInterventionTVP.Columns.Add("dtInterventionRecordedDate")
        dtInterventionTVP.Columns.Add("RowState")

        'Table used for CP_TVPInterventionAssociation TVP parameter values
        dtInterventionAssociationTVP = New DataTable()
        dtInterventionAssociationTVP.Columns.Add("nInterventionAssociationID")
        dtInterventionAssociationTVP.Columns.Add("nCarePlanId")
        dtInterventionAssociationTVP.Columns.Add("nInterventionId")
        dtInterventionAssociationTVP.Columns.Add("nGoalID")
        dtInterventionAssociationTVP.Columns.Add("nGoalAssociationID")
        dtInterventionAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtInterventionAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub setOutcomeDT()
        dtOutcome = New DataTable()
        For Each column As DataGridViewColumn In dgOutcome.Columns
            dtOutcome.Columns.Add(column.Name)
        Next

        'Table used for CP_TVPOutcomes TVP parameter values
        dtOutcomeTVP = New DataTable()
        dtOutcomeTVP.Columns.Add("nOutcomeID")
        dtOutcomeTVP.Columns.Add("nCarePlanId")
        dtOutcomeTVP.Columns.Add("nPatientId")
        dtOutcomeTVP.Columns.Add("sOutcomeStatus")
        dtOutcomeTVP.Columns.Add("sOutcomeNotes")
        dtOutcomeTVP.Columns.Add("dtOutcomeRecordedDate")
        dtOutcomeTVP.Columns.Add("RowState")

        'Table used for CP_TVPOutcomeAssociation TVP parameter values
        dtOutcomeAssociationTVP = New DataTable()
        dtOutcomeAssociationTVP.Columns.Add("nOutcomeAssociationID")
        dtOutcomeAssociationTVP.Columns.Add("nCarePlanId")
        dtOutcomeAssociationTVP.Columns.Add("nOutcomeID")
        dtOutcomeAssociationTVP.Columns.Add("nInterventionId")
        dtOutcomeAssociationTVP.Columns.Add("nInterventionAssociationID")
        dtOutcomeAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtOutcomeAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub loadCarePlanDetails()
        Try
            Dim dsCarePlanDetails As DataSet = Nothing
            Using objPatientCarePlan As New ClsCarePlan_V2()
                dsCarePlanDetails = objPatientCarePlan.GetCarePlanDetails_V2(_CarePlanID)
            End Using

            If dsCarePlanDetails IsNot Nothing Then
                If dsCarePlanDetails.Tables("PatientCarePlan").Rows.Count > 0 Then
                    _VisitID = dsCarePlanDetails.Tables("PatientCarePlan").Rows(0)("nVisitId")
                    _PatientID = dsCarePlanDetails.Tables("PatientCarePlan").Rows(0)("nPatientId")
                    txtCarePlanName.Text = dsCarePlanDetails.Tables("PatientCarePlan").Rows(0)("sCarePlanName")
                    txtCarePlanReason.Text = dsCarePlanDetails.Tables("PatientCarePlan").Rows(0)("sCarePlanReason")
                End If

                If Not dsCarePlanDetails.Tables("PatientHealthConcern") Is Nothing Then
                    dtHealthConcern.Rows.Clear()
                    For h As Int32 = 0 To dsCarePlanDetails.Tables("PatientHealthConcern").Rows.Count - 1
                        dtHealthConcern.Rows.Add()
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_RowNo") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("nHealthConcernAssociationID")
                        'dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_isHealthConcern") = 1
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthConcernNo") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("nHealthConcernID")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConsernId") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernSnomedCode")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernDesc") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernSnomedDescription")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthStatus") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernSnomedCode") + " - " + dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernSnomedDescription")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthStatusDesc") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthStatusDescription")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_Author") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernAuthor")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernStatus") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernStatus")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernStartDate") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("dtHealthConcernStartDate")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernEndDate") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("dtHealthConcernEndDate")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernNotes") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("sHealthConcernNotes")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ProbelmRefId") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("nAssociatedConcernId")
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ProblemRef") = dsCarePlanDetails.Tables("PatientHealthConcern").Rows(h)("AssociationRef")
                    Next
                End If

                If Not dsCarePlanDetails.Tables("PatientGoals") Is Nothing Then
                    dtGoal.Rows.Clear()
                    For h As Int32 = 0 To dsCarePlanDetails.Tables("PatientGoals").Rows.Count - 1
                        dtGoal.Rows.Add()
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Goal_RowNo") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("nGoalAssociationID")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalNo") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("nGoalId")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_GL_HealthConcernRowNo") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("nHealthConcernAssociationID")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_GL_HealthConcernNo") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("nHealthConcernID")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Loinc") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sGoalLoincCode") + " : " + dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sGoalLoincDescription")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalValue") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sGoalValue")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalUnit") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sGoalUnit")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalAuthoer") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sGoalAuthor")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Notes") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sGoalNotes")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_GL_HealthStatus") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sHealthStatus")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_GL_ProbelmRefId") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("nAssociatedConcernId")
                        dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_GL_ProblemRef") = dsCarePlanDetails.Tables("PatientGoals").Rows(h)("sAssociatedConcernType")
                    Next
                End If

                If Not dsCarePlanDetails.Tables("PatientIntervention") Is Nothing Then
                    dtIntervention.Rows.Clear()
                    For h As Int32 = 0 To dsCarePlanDetails.Tables("PatientIntervention").Rows.Count - 1
                        dtIntervention.Rows.Add()
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_RowNo") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nInterventionAssociationID")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_InterventionNo") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nInterventionId")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalRowNo") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nGoalAssociationID")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalNo") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nGoalID")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_HealthConcernRowNo") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nHealthConcernAssociationID")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_HealthConcernNo") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nHealthConcernID")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_IntSnomed") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sInterventionSnomedCode") + " - " + dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sInterventionSnomedDescription")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_IntNotes") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sInterventionNotes")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalLoinc") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sGoalLoincCode") + " : " + dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sGoalLoincDescription")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalValue") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sGoalValue")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalUnit") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sGoalUnit")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalNotes") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sGoalNotes")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_HealthStatus") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sHealthStatus")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_ProbelmRefId") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("nAssociatedConcernId")
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_ProblemRef") = dsCarePlanDetails.Tables("PatientIntervention").Rows(h)("sAssociatedConcernType")
                    Next
                End If

                If Not dsCarePlanDetails.Tables("PatientOutcome") Is Nothing Then
                    dtOutcome.Rows.Clear()
                    For h As Int32 = 0 To dsCarePlanDetails.Tables("PatientOutcome").Rows.Count - 1
                        dtOutcome.Rows.Add()
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_RowNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nOutcomeAssociationID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_OutcomeNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nOutcomeID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_IntRowNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nInterventionAssociationID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_InterventionNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nInterventionId")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalRowNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nGoalAssociationID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nGoalID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_HealthConcernRowNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nHealthConcernAssociationID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_HealthConcernNo") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nHealthConcernID")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_OutcomeStatus") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sOutcomeStatus")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_OutcomeNotes") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sOutcomeNotes")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_IntSnomed") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sInterventionSnomedCode") + " - " + dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sInterventionSnomedCode")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_IntNotes") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sInterventionNotes")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalLoinc") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sGoalLoincCode") + " - " + dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sGoalLoincCode")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalValue") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sGoalValue")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalUnit") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sGoalUnit")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalNotes") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sGoalNotes")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_HealthStatus") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sHealthStatus")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_ProbelmRefId") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("nAssociatedConcernId")
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_ProblemRef") = dsCarePlanDetails.Tables("PatientOutcome").Rows(h)("sAssociatedConcernType")
                    Next
                End If

                refreshHealthConcernGrid()
                refreshGoalGrid()
                refreshInterventionGrid()
                refreshOutcomeGrid()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBrowserSnomedCode_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowserSnomedCode.Click
        Dim strtxtSnomed As String = ""
        Dim strOldConcept As String = ""
        Dim strOldDesc As String = ""

        'Dim dtICD As DataTable = Nothing
        Dim frm As gloSnoMed.FrmSelectProblem = Nothing
        Try
            strtxtSnomed = txtSnomed.Text ' CmbSnomedcode.Text
            'strtxtICD = txtICD9.Text
            strOldConcept = strConceptID
            strOldDesc = strDescription

            gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
            frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())
            Dim arrIcd() As String = txtSnomed.Text.Split("-")

            frm.strCodeSystem = "SNOMED"
            frm.txtSMSearch.Text = strConceptID  'lblConceptID.Text.Trim

            frm.blnIsProblem = True
            frm.strConceptID = strConceptID 'lblConceptID.Text.Trim()
            frm.strConceptDesc = strDescription 'txt_Problem.Text.Trim()
            ' frm.strDescriptionID = lblDescriptionID.Text.Trim()
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            'If _blnclNewProblem = True Then
            '    _blnclNewProblem = False
            '    Me.Close()
            'End If

            If frm._DialogResult = True Then
                'strSelectedProblem = frm.strProblem


                frm.strProblem = Replace(Replace(frm.strProblem, " (disorder)", ""), " (finding)", "")


                'strProblem = frm.strProblem 'txt_Problem.Text ''swaraj 20100612
                strConceptID = frm.strSelectedConceptID

                '08-Oct-14 Aniket: Bug #74799 ( Modified): gloEMR - Problem List - Application is showing 'finding' and 'Disorder' when user modify new problem.
                strDescription = Replace(Replace(frm.strSelectedDescription, " (disorder)", ""), " (finding)", "")

                ''strSnoMedId = frm.strSelectedSnoMedID
                ''strDescriptionID = frm.strSelectedDescriptionID
                'lblConceptID.Text = frm.strSelectedConceptID
                'lblDescriptionID.Text = frm.strSelectedDescriptionID
                ''lblSnoMedID.Text = frm.strSelectedSnoMedID

                'If strSelectedProblem <> "" Then
                '    txtSnomed.Text = frm.strProblem
                'Else
                '    txtSnomed.Text = strProblem ''swaraj 20100612 ''strSelectedProblem
                'End If
                If frm.strProblem <> "" Then
                    txtSnomed.Text = strConceptID + " - " + frm.strProblem
                    If txtHealthStatusDesc.Text.Length = 0 Then
                        txtHealthStatusDesc.Text = frm.strProblem
                    End If
                Else
                    txtSnomed.Text = ""
                End If
                'End If

                If frm.strProblem.Trim() <> "" Then
                    'txt_Problem.Text = frm.strProblem.Trim()
                    strDescription = frm.strProblem.Trim()
                End If
            Else
                strConceptID = ""
                strDescription = ""
                If strtxtSnomed <> "" Then
                    txtSnomed.Text = strtxtSnomed
                End If
                'Bug No. 71034 Resolved::Problem List - Application does not save SNOMED code when new problem is created.
                If strOldConcept <> "" Then
                    strConceptID = strOldConcept
                End If
                If strOldDesc <> "" Then
                    strDescription = strOldDesc
                End If
                '
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Finally
            strtxtSnomed = Nothing
            strOldConcept = Nothing
            strOldDesc = Nothing
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
    End Sub

    Private Sub btnClearSnomed_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSnomed.Click
        txtSnomed.Text = ""
        'strCurrentSnomedCode = ""
        'strCurrentSnomedDesc = ""
        'Resolving Bug No. 71081:: Problem List->SnoMed Code is saved even after it is removed
        strConceptID = ""
        strDescription = ""
    End Sub

    Private Sub btn_LoadProblem_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadProblem.Click
        LoadUserGridProblem()
        dgCustomGridProblem.Label1.Visible = False
        dgCustomGridProblem.txtsearch.Visible = False
        dgCustomGridProblem.Panel2.Visible = False
        pnlcustomTask.Visible = True
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub LoadUserGridProblem()
        Try
            AddControlExam()
            If Not IsNothing(dgCustomGridProblem) Then
                dgCustomGridProblem.Visible = True
                dgCustomGridProblem.Width = pnlcustomTask.Width
                dgCustomGridProblem.Height = pnlcustomTask.Height
                dgCustomGridProblem.BringToFront()
                dgCustomGridProblem.SetVisible = False
                BindUserGridProblem()
                dgCustomGridProblem.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddControlExam()

        If Not IsNothing(dgCustomGridProblem) Then
            RemoveControlExam()
        End If
        dgCustomGridProblem = New CustomTask
        pnlcustomTask.Controls.Add(dgCustomGridProblem)
        pnlcustomTask.BringToFront()

    End Sub

    Private Sub RemoveControlExam()
        If Not IsNothing(dgCustomGridProblem) Then
            pnlcustomTask.Controls.Remove(dgCustomGridProblem)
            dgCustomGridProblem.Visible = False
            dgCustomGridProblem.Dispose()
            dgCustomGridProblem = Nothing
        End If
    End Sub

    Private Sub BindUserGridProblem()
        Try
            Dim objProblemList As New clsPatientProblemList
            Dim dtProblems As DataTable
            dtProblems = objProblemList.Get_PatientProblemList(_PatientID)
            objProblemList.Dispose()
            objProblemList = Nothing

            If IsNothing(dtProblems) = False Then
                Call SetGridStyle(dtProblems)
                setProblemValues()
                'btn_ActiveProblem.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                'btn_ActiveProblem.BackgroundImageLayout = ImageLayout.Center
                dtProblems.Dispose()
                dtProblems = Nothing
            End If
            'setProblemValues()

            'Dim objProblemList As New clsPatientProblemList
            'Dim dtProblems As DataTable
            'dtProblems = objProblemList.Get_PatientProblemList(_PatientID)
            'objProblemList.Dispose()
            'objProblemList = Nothing

            'CustomDrugsGridStyleExam()
            'Dim col As New DataColumn
            'col.ColumnName = "Select"
            'col.DataType = System.Type.GetType("System.Boolean")

            'col.DefaultValue = CBool("False")
            'dtProblems.Columns.Add(col)
            ''col.Dispose()
            ''col = Nothing

            'If Not IsNothing(dtProblems) Then
            '    dgCustomGridProblem.datasource(dtProblems.DefaultView)
            'End If
            ' ''Sandip Darade 20090410
            ' ''Reset the grid
            'Dim _TotalWidth As Single = dgCustomGridProblem.C1Task.Width - 5
            'dgCustomGridProblem.C1Task.Cols.Move(dgCustomGridProblem.C1Task.Cols.Count - 1, 0)
            'dgCustomGridProblem.C1Task.AllowEditing = True

            'dgCustomGridProblem.C1Task.Cols(Col_ProblemID + 1).AllowEditing = True
            'dgCustomGridProblem.C1Task.Cols(Col_ProblemID + 1).Width = _TotalWidth * 0
            'dgCustomGridProblem.C1Task.Cols(Col_VistitID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_VistitID + 1).Width = _TotalWidth * 0

            'dgCustomGridProblem.C1Task.Cols(Col_Dos + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_DischargeDate + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Diagnosis + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Complaint + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Status + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ResolvedDt + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ResolvedComment + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Prescription + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_UserID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_UserID + 1).Width = _TotalWidth * 0

            'dgCustomGridProblem.C1Task.Cols(Col_Immediacy + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ProblemComments + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ProblemStatus + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Provider + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Location + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ModifiedDate + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ExamID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ExamID + 1).Width = _TotalWidth * 0

            'dgCustomGridProblem.C1Task.Cols(Col_SnoMedID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_DescriptionID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_Description + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_TransactionID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_UserName + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_IsEncounterDiagnosis + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ICDRevision + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ReasonConceptID + 1).AllowEditing = False
            'dgCustomGridProblem.C1Task.Cols(Col_ReasonConceptDesc + 1).AllowEditing = False
            'setExamValues()
            ''  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub setProblemValues()
        For j As Integer = 0 To cmbProblem.Items.Count - 1
            cmbProblem.Text = ""
            cmbProblem.SelectedIndex = j

            For i As Int32 = 1 To dgCustomGridProblem.C1Task.Rows.Count - 1
                If dgCustomGridProblem.GetItem(i, COL_PROBLEMID).ToString.Trim = Convert.ToString(cmbProblem.SelectedValue) Then
                    dgCustomGridProblem.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If
            Next
        Next
    End Sub

    Private Sub SetGridStyle(ByVal dt As DataTable)

        Try
            ' _isFormLoad = True
            Dim _strComplaints As String
            Dim _Comments() As String
            Dim _nCommentCount As Integer
            With dgCustomGridProblem.C1Task

                Dim i As Int16
                .Redraw = False
                .Dock = DockStyle.Fill

                Dim _TotalWidth As Single = 0
                'PrevGridWidth = .Width

                '_TotalWidth = (.Width - 20) / 12
                _TotalWidth = .Width / 8
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing

                .Cols.Count = COLUMN_COUNT
                .Rows.Count = 1
                .Rows.Fixed = 1
                .AllowEditing = True
                .AllowResizing = AllowDraggingEnum.Both ''True

                .Styles.ClearUnused()

                .Cols(COL_VISITID).Width = 0
                .Cols(COL_VISITID).AllowEditing = False
                .Cols(COL_VISITID).Visible = False
                .SetData(0, COL_VISITID, "VisitID")

                .Cols(COL_PROBLEMID).Width = 0
                .Cols(COL_PROBLEMID).AllowEditing = False
                .Cols(COL_PROBLEMID).Visible = False

                .Cols(COL_DOS).Width = _TotalWidth * 1.2 ''1.2
                .SetData(0, COL_DOS, "Date")
                If (gblnEnableCQMCypressTesting) Then
                    .Cols(COL_DOS).DataType = GetType(DateTime)
                Else
                    .Cols(COL_DOS).DataType = GetType(Date)
                End If
                .Cols(COL_DOS).AllowEditing = False

                .Cols(COL_COMPLAINTS).Width = _TotalWidth * 3.0  ''2.7
                .SetData(0, COL_COMPLAINTS, "Description")
                .Cols(COL_COMPLAINTS).AllowEditing = False
                .Cols(COL_COMPLAINTS).Visible = True

                'If gblnEducationMaterialEnabled Then
                '    .Cols(COL_INFOBUTTON).Width = _TotalWidth * 0.3
                'Else
                .Cols(COL_INFOBUTTON).Width = 0
                'End If

                .Cols(Col_DischargeDate).Width = 0
                .SetData(0, Col_DischargeDate, "Discharge Date")
                If (gblnEnableCQMCypressTesting) Then
                    .Cols(Col_DischargeDate).DataType = GetType(DateTime)
                Else
                    .Cols(Col_DischargeDate).DataType = GetType(Date)
                End If
                .Cols(Col_DischargeDate).Visible = False
                .Cols(Col_DischargeDate).AllowEditing = False


                .Cols(COL_DIAGNOSIS).Width = _TotalWidth * 4.5  ''2.7
                .SetData(0, COL_DIAGNOSIS, "Diagnosis")
                .Cols(COL_DIAGNOSIS).AllowEditing = False

                .Cols(COL_DIAGNOSISBUTTON).Width = _TotalWidth * 0.4
                .Cols(COL_DIAGNOSISBUTTON).Visible = False

                .Cols(COL_PRESCRIPTION).Width = _TotalWidth * 3.5  ''2.7
                .SetData(0, COL_PRESCRIPTION, "Prescription")
                .Cols(COL_PRESCRIPTION).AllowEditing = False

                .Cols(COL_RxBUTTON).Width = _TotalWidth * 0.4
                .Cols(COL_RxBUTTON).Visible = False

                .Cols(Col_UserName).Width = _TotalWidth * 1.5  ''2.7
                .SetData(0, Col_UserName, "User")
                .Cols(Col_UserName).AllowEditing = False

                .Cols(COL_RsDt).Width = _TotalWidth * 1.4
                .SetData(0, COL_RsDt, "Resolved Date")
                .Cols(COL_RsDt).DataType = GetType(Date)
                .Cols(COL_RsDt).AllowEditing = False
                .Cols(COL_RsDt).Visible = True


                .Cols(COL_USER).Width = 0
                .SetData(0, COL_USER, "UserID")
                .Cols(COL_USER).AllowEditing = False
                .Cols(COL_USER).Visible = False

                .Cols(COL_EXAMNAMEBUTTON).Width = _TotalWidth * 1.0
                .SetData(0, COL_EXAMNAMEBUTTON, "Open Exam")
                '.Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
                '''' 
                'If blnOpenFromExam Then
                '    .Cols(COL_EXAMNAMEBUTTON).Width = Width * 0
                'End If


                .Cols(COL_Immediacy).Width = _TotalWidth * 1.2
                .SetData(0, COL_Immediacy, "Immediacy")
                .Cols(COL_Immediacy).AllowEditing = False

                .Cols(COL_ProblemSatus).Width = _TotalWidth * 1.0  ''1.2
                .SetData(0, COL_ProblemSatus, "Status")
                .Cols(COL_ProblemSatus).AllowEditing = False

                .Cols(COL_Provider).Width = _TotalWidth * 1.2
                .SetData(0, COL_Provider, "Provider")
                .Cols(COL_Provider).AllowEditing = False

                .Cols(COL_Location).Width = _TotalWidth * 1.2
                .SetData(0, COL_Location, "Location")
                .Cols(COL_Location).AllowEditing = False

                .Cols(COL_LastModified).Width = _TotalWidth * 1.2
                .SetData(0, COL_LastModified, "Last Update")
                .Cols(COL_LastModified).AllowEditing = False

                .Cols(COL_ExamID).Width = 0
                .SetData(0, COL_ExamID, "ExamID")
                .Cols(COL_ExamID).AllowEditing = False
                .Cols(COL_ExamID).Visible = False

                .Cols(Col_HiddedPrescription).Width = 0
                .SetData(0, Col_HiddedPrescription, "HiddenPres")
                .Cols(Col_HiddedPrescription).AllowEditing = False
                .Cols(Col_HiddedPrescription).Visible = False

                'If blnRxMedToProblem = True Then
                .Cols(COL_SELECT).Width = _TotalWidth * 0.6
                .SetData(0, COL_SELECT, "Select")
                .Cols(COL_SELECT).DataType = GetType(Boolean)
                .Cols(COL_SELECT).AllowEditing = True
                'Else
                '    .Cols(COL_SELECT).Visible = False

                'End If

                .Cols(COL_ProblemType).Width = _TotalWidth * 2.2
                .SetData(0, COL_ProblemType, "Problem Type")
                .Cols(COL_ProblemType).AllowEditing = False

                .Cols(COL_ConceptID).Width = _TotalWidth * 1.4
                .SetData(0, COL_ConceptID, "SnoMed CT ID") ' Header changed to 
                .Cols(COL_ConceptID).AllowEditing = False

                .Cols(COL_SnoMedID).Width = 0
                .SetData(0, COL_SnoMedID, "SnoMed ID")
                .Cols(COL_SnoMedID).AllowEditing = False

                .Cols(COL_DescriptionID).Width = _TotalWidth * 1.2
                .SetData(0, COL_DescriptionID, "Desc. ID")
                .Cols(COL_DescriptionID).AllowEditing = False
                .Cols(COL_DescriptionID).Visible = False

                .Cols(COL_Defination).Width = 0
                .SetData(0, COL_Defination, "Defination")
                .Cols(COL_Defination).AllowEditing = False

                .Cols(Col_ReasonConceptID).Width = 0
                .SetData(0, Col_ReasonConceptID, "Reason Concept ID")
                .Cols(Col_ReasonConceptID).AllowEditing = False
                .Cols(Col_ReasonConceptID).Visible = False

                .Cols(Col_ReasonConceptDesc).Width = _TotalWidth * 3
                .SetData(0, Col_ReasonConceptDesc, "Reason Concept Description")
                .Cols(Col_ReasonConceptDesc).AllowEditing = False
                .Cols(Col_ReasonConceptDesc).Visible = False

                .Cols(Col_EncounterDiagnosis).DataType = GetType(Boolean)
                .SetData(0, Col_EncounterDiagnosis, "Encounter/Diagnosis")
                .Cols(Col_EncounterDiagnosis).Width = _TotalWidth * 1.2
                .Cols(Col_EncounterDiagnosis).Visible = False

                .Cols(Col_IsModifed).Width = 0
                .SetData(0, Col_IsModifed, "IsModified")

                .Cols(Col_ICDRevision).Width = 0
                .SetData(0, Col_ICDRevision, "ICDRevision")


                For i = 0 To dt.Rows.Count - 1
                    .Rows.Add()



                    .SetData(i + 1, COL_VISITID, dt.Rows(i)("VisitID"))

                    If (gblnEnableCQMCypressTesting) Then
                        .SetData(i + 1, COL_DOS, Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy hh:mm:ss tt"))
                    Else
                        .SetData(i + 1, COL_DOS, Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy"))
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(i)("dtDischargeDate"))) Then
                        If (gblnEnableCQMCypressTesting) Then
                            .SetData(i + 1, Col_DischargeDate, Format(dt.Rows(i)("dtDischargeDate"), "MM/dd/yyyy hh:mm:ss tt"))
                        Else
                            .SetData(i + 1, Col_DischargeDate, Format(dt.Rows(i)("dtDischargeDate"), "MM/dd/yyyy"))
                        End If
                    End If

                    .SetData(i + 1, COL_PROBLEMID, dt.Rows(i)("ProblemID"))

                    .SetData(i + 1, COL_DIAGNOSIS, dt.Rows(i)("Diagnosis").ToString.Trim() & "")
                    .SetData(i + 1, Col_UserName, dt.Rows(i)("UserName").ToString.Trim() & "")

                    'Added Infobutton
                    .SetCellImage(i + 1, COL_INFOBUTTON, My.Resources.infobutton)
                    '.SetCellImage(i + 1, COL_PROVIDEREDUCATIONBUTTON, My.Resources.I)
                    '.SetCellImage(i + 1, COL_PATIENTEDUCATIONBUTTON, My.Resources.I)

                    .SetData(i + 1, COL_PRESCRIPTION, dt.Rows(i)("Prescription").ToString().Replace("!", ","))

                    .SetData(i + 1, Col_HiddedPrescription, dt.Rows(i)("Prescription"))


                    If Trim(dt.Rows(i)("ResolvedDt").ToString()) <> "" Then
                        .SetData(i + 1, COL_RsDt, dt.Rows(i)("ResolvedDt"))
                    Else
                        .SetData(i + 1, COL_RsDt, DBNull.Value)
                    End If

                    .SetData(i + 1, COL_USER, dt.Rows(i)("nUserID"))


                    .SetData(i + 1, COL_DIAGNOSISBUTTON, "")
                    Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_DIAGNOSISBUTTON, i + 1, COL_DIAGNOSISBUTTON)
                    rgDig.Style = cStyle

                    .SetData(i + 1, COL_DIAGNOSIS, dt.Rows(i)("Diagnosis").ToString.Trim() & "")

                    .SetData(i + 1, COL_RxBUTTON, "")
                    Dim rgRx As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_RxBUTTON, i + 1, COL_RxBUTTON)
                    rgRx.Style = cStyle


                    If dt.Rows(i)("Immediacy") = EnmImmediacy.Acute Then
                        .SetData(i + 1, COL_Immediacy, "Acute")

                    ElseIf dt.Rows(i)("Immediacy") = EnmImmediacy.Chronic Then
                        .SetData(i + 1, COL_Immediacy, "Chronic")
                    ElseIf dt.Rows(i)("Immediacy") = EnmImmediacy.unknown Then
                        .SetData(i + 1, COL_Immediacy, "unknown")
                    End If


                    If dt.Rows(i)("nStatus") = Status.Resolved Then
                        .SetData(i + 1, COL_ProblemSatus, "Resolved")
                    ElseIf dt.Rows(i)("nStatus") = Status.Active Then
                        .SetData(i + 1, COL_ProblemSatus, "Active")
                    ElseIf dt.Rows(i)("nStatus") = Status.Inactive Then
                        .SetData(i + 1, COL_ProblemSatus, "Inactive")
                    ElseIf dt.Rows(i)("nStatus") = Status.Resolved Then
                        .SetData(i + 1, COL_ProblemSatus, "Resolved")
                    ElseIf dt.Rows(i)("nStatus") = Status.Chronic Then
                        .SetData(i + 1, COL_ProblemSatus, "Chronic")
                    End If

                    .SetData(i + 1, COL_Provider, dt.Rows(i)("Provider"))
                    .SetData(i + 1, COL_Location, dt.Rows(i)("Location"))
                    If Not IsNothing(dt.Rows(i)("ModifiedDate")) Then
                        If Not dt.Rows(i)("ModifiedDate").ToString().Contains("4/12/1900") Then
                            .SetData(i + 1, COL_LastModified, dt.Rows(i)("ModifiedDate"))
                        End If
                    End If
                    .SetData(i + 1, COL_ExamID, dt.Rows(i)("ExamID"))
                    If Not IsNothing(dt.Rows(i)("Comments")) Then
                        If dt.Rows(i)("Comments") <> "" Then
                            _strComplaints = dt.Rows(i)("Complaint") & vbNewLine & dt.Rows(i)("Comments")
                            _Comments = Split(dt.Rows(i)("Comments"), vbNewLine)
                            _nCommentCount = _Comments.Length + 1
                        Else
                            _strComplaints = dt.Rows(i)("Complaint")
                        End If
                    Else
                        _strComplaints = dt.Rows(i)("Complaint")
                    End If
                    .Rows(.Row).AllowResizing = AllowDraggingEnum.Both
                    .Rows(.Row).AllowDragging = DrawModeEnum.OwnerDraw
                    If _nCommentCount <> 0 Then
                        .Rows(.Rows.Count - 1).Height = dgCustomGridProblem.C1Task.Rows.DefaultSize * _nCommentCount
                    End If
                    .Cols(COL_COMPLAINTS).TextAlign = TextAlignEnum.LeftCenter
                    .Cols(COL_DIAGNOSIS).TextAlign = TextAlignEnum.LeftCenter
                    .Cols(COL_ConceptID).TextAlign = TextAlignEnum.LeftCenter

                    .SetData(i + 1, COL_COMPLAINTS, _strComplaints.Trim)
                    .SetData(i + 1, COL_ProblemType, dt.Rows(i)("sTransactionID1").ToString.Trim())
                    .SetData(i + 1, COL_ConceptID, dt.Rows(i)("sConceptID").ToString.Trim())
                    .SetData(i + 1, COL_SnoMedID, dt.Rows(i)("sSnoMedID").ToString.Trim())
                    .SetData(i + 1, COL_DescriptionID, dt.Rows(i)("sDescriptionID").ToString.Trim())
                    .SetData(i + 1, COL_Defination, dt.Rows(i)("sDescription").ToString.Trim())
                    .SetData(i + 1, Col_IsModifed, False)
                    .SetData(i + 1, Col_ICDRevision, dt.Rows(i)("nICDRevision")) ''added for ICD10 implementation
                    .SetData(i + 1, Col_EncounterDiagnosis, dt.Rows(i)("IsEncounterDiagnosis"))
                    .SetData(i + 1, Col_ReasonConceptID, dt.Rows(i)("sReasonConceptID").ToString.Trim())
                    .SetData(i + 1, Col_ReasonConceptDesc, dt.Rows(i)("sReasonConceptDesc").ToString.Trim())
                Next

                .Redraw = True
                dgCustomGridProblem.C1Task.SelectionMode = SelectionModeEnum.Row  'swaraj 20100629
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally
            '_isFormLoad = False
        End Try

    End Sub

    Public Sub CustomDrugsGridStyleExam()
        'Dim _TotalWidth As Single = dgCustomGridProblem.C1Task.Width - 5
        '' '' Show Drugs Info
        'With dgCustomGridProblem.C1Task
        '    .Redraw = False
        '    .Cols.Fixed = 0
        '    .Rows.Fixed = 1
        '    .Cols.Count = Col_eCount
        '    .AllowEditing = True
        '    .SetData(0, Col_eCheck, "Select")
        '    '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        '    .Cols(Col_eCheck).Width = _TotalWidth * 0.1
        '    .Cols(Col_eCheck).AllowEditing = True
        '    .Cols(Col_eCheck).DataType = System.Type.GetType("System.Boolean")
        '    .SetData(0, Col_ProblemID, "ProblemID")
        '    .Cols(Col_ProblemID).Width = _TotalWidth * 0.0
        '    .Cols(Col_ProblemID).Visible = False
        '    .Cols(Col_ProblemID).AllowEditing = False
        '    .SetData(0, Col_VistitID, "VisitID")
        '    .Cols(Col_VistitID).Width = _TotalWidth * 0
        '    .Cols(Col_VistitID).AllowEditing = False
        '    .Cols(Col_VistitID).Visible = False
        '    .SetData(0, Col_eDos, "DOS")
        '    .Cols(Col_eDos).Width = _TotalWidth * 0.45
        '    .Cols(Col_eDos).AllowEditing = False
        '    .SetData(0, Col_eExamName, "Exam Name")
        '    .Cols(Col_eExamName).Width = _TotalWidth * 0.45
        '    .Cols(Col_eExamName).AllowEditing = False
        '    .SetData(0, Col_eTemplateName, "Template Name")
        '    .Cols(Col_eTemplateName).Width = _TotalWidth * 0.45
        '    .Cols(Col_eTemplateName).AllowEditing = False
        '    .SetData(0, Col_eFinished, "Finished")
        '    .Cols(Col_eFinished).Width = _TotalWidth * 0.45
        '    .Cols(Col_eFinished).AllowEditing = False
        '    .SetData(0, Col_eProviderName, "Provider Name")
        '    .Cols(Col_eProviderName).Width = _TotalWidth * 0.45
        '    .Cols(Col_eProviderName).AllowEditing = False
        '    .SetData(0, Col_eReviewedBy, "ReviewedBy")
        '    .Cols(Col_eReviewedBy).Width = _TotalWidth * 0
        '    .Cols(Col_eReviewedBy).AllowEditing = False
        '    .Cols(Col_eReviewedBy).Visible = False
        '    .Redraw = True
        'End With
    End Sub

    Private Sub dgCustomGridExam_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridProblem.CloseClick
        dgCustomGridProblem.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Private Sub dgCustomGridExam_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridProblem.OKClick
        Try
            'Resolving Bug No 71305 :: Problem list - Application is showing an error message
            'If Not IsNothing(dtExam) Then
            '    dtExam.Dispose()
            '    dtExam = Nothing
            'End If
            Dim dtProblem As DataTable = New DataTable()
            dtProblem.TableName = "Exam"
            ' Dim Row1 As DataRow

            dtProblem.Columns.Add("ProblemID")
            dtProblem.Columns.Add("VisitID")
            dtProblem.Columns.Add("Comments")

            cmbProblem.Text = ""

            cmbProblem.DataSource = Nothing
            cmbProblem.Items.Clear()

            For i As Int32 = 0 To dgCustomGridProblem.C1Task.Rows.Count - 1
                If dgCustomGridProblem.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    Dim dr As DataRow = dtProblem.NewRow()
                    dr(0) = dgCustomGridProblem.C1Task.GetData(i, COL_PROBLEMID).ToString()
                    dr(1) = dgCustomGridProblem.C1Task.GetData(i, COL_VISITID).ToString()
                    dr(2) = dgCustomGridProblem.C1Task.GetData(i, COL_COMPLAINTS).ToString()
                    dtProblem.Rows.Add(dr)
                End If
            Next

            If dtProblem.Rows.Count > 0 Then
                cmbProblem.DataSource = dtProblem
                cmbProblem.DisplayMember = "Comments"
                cmbProblem.ValueMember = "ProblemID"
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlcustomTask.Visible = False
        End Try
    End Sub

    Private Function ValidateHealthConcern() As Boolean
        Return True
    End Function

    Private Sub refreshHealthConcernGrid()
        Try
            dgHealthConcern.Rows.Clear()
            dgSelectHealthConcern.Rows.Clear()
            Dim currHealthConcernId As String = ""
            For i As Int32 = 0 To dtHealthConcern.Rows.Count - 1
                dgHealthConcern.Rows.Add()
                If currHealthConcernId <> dtHealthConcern(i)("Col_HealthConcernNo") Then
                    currHealthConcernId = dtHealthConcern(i)("Col_HealthConcernNo")
                    dgSelectHealthConcern.Rows.Add()
                    dgSelectHealthConcern.Item("Col_HealthConcernNo_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_HealthConcernNo")
                    dgSelectHealthConcern.Item("Col_HealthConcernName_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_HealthConcernName")
                    dgSelectHealthConcern.Item("Col_Author_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_Author")
                    dgSelectHealthConcern.Item("Col_ConcernStatus_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_ConcernStatus")
                    dgSelectHealthConcern.Item("Col_ConcernStartDate_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_ConcernStartDate")
                    dgSelectHealthConcern.Item("Col_ConcernEndDate_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_ConcernEndDate")
                    dgSelectHealthConcern.Item("Col_ConcernNotes_Sel", dgSelectHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)("Col_ConcernNotes")
                End If

                For j As Int32 = 0 To dtHealthConcern.Columns.Count - 1
                    dgHealthConcern.Item(dtHealthConcern.Columns(j).ColumnName, dgHealthConcern.RowCount - 1).Value = dtHealthConcern.Rows(i)(j)
                Next
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Add_Click(sender As System.Object, e As System.EventArgs) Handles btn_Add.Click
        Try
            If ValidateHealthConcern() Then
                Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(cmbProblem.Items.Count + 1)
                Dim nHealthConcernId As Int64 = dtUniqueIds.Rows(0)(0)
                Dim nIdCount As Int32 = 0

                If cmbProblem.Items.Count > 0 Then
                    For j As Integer = 0 To cmbProblem.Items.Count - 1
                        dtHealthConcern.Rows.Add()
                        nIdCount = nIdCount + 1
                        cmbProblem.Text = ""
                        cmbProblem.SelectedIndex = j
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_RowNo") = dtUniqueIds.Rows(nIdCount)(0) 'nHealthConcernRowNo
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthConcernName") = txtHealthConcernName.Text
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthConcernNo") = nHealthConcernId 'nHealthConcernCnt
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConsernId") = strConceptID
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernDesc") = strDescription
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthStatus") = txtSnomed.Text
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthStatusDesc") = txtHealthStatusDesc.Text
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_Author") = sConcernFrom
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernStatus") = sConcernStatus
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernStartDate") = dtpConcernStartDate.Text
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernEndDate") = dtpConcernEndDate.Text
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernNotes") = txtHealthConcernNotes.Text
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ProbelmRefId") = cmbProblem.SelectedValue
                        dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ProblemRef") = cmbProblem.GetItemText(cmbProblem.Items(j))
                    Next
                Else
                    dtHealthConcern.Rows.Add()
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_RowNo") = nHealthConcernId 'nHealthConcernRowNo
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthConcernName") = txtHealthConcernName.Text
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthConcernNo") = nHealthConcernId 'nHealthConcernCnt
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConsernId") = strConceptID
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernDesc") = strDescription
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthStatus") = txtSnomed.Text
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_HealthStatusDesc") = txtHealthStatusDesc.Text
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_Author") = sConcernFrom
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernStatus") = sConcernStatus
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernStartDate") = dtpConcernStartDate.Text
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernEndDate") = dtpConcernEndDate.Text
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ConcernNotes") = txtHealthConcernNotes.Text
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ProbelmRefId") = 0
                    dtHealthConcern.Rows(dtHealthConcern.Rows.Count - 1)("Col_ProblemRef") = ""
                End If



                refreshHealthConcernGrid()

                If Not dtUniqueIds Is Nothing Then
                    dtUniqueIds.Rows.Clear()
                    dtUniqueIds.Dispose()
                    dtUniqueIds = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Remove_Click(sender As System.Object, e As System.EventArgs) Handles btn_Remove.Click
        Try
            If Not IsNothing(dgHealthConcern.CurrentRow) Then  ''added for bugid 70999 if no row exist
                If dgHealthConcern.CurrentRow.Index >= 0 Then
                    If MessageBox.Show(" Are you sure you want to delete selected health concern?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        deleteProblemRef(dgHealthConcern.CurrentRow.Cells("Col_RowNo").Value)
                        refreshHealthConcernGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub deleteHealthConcern(ByVal sHealthConcernNo As String)
        Try
            dtHealthConcern.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("Col_HealthConcernNo") = sHealthConcernNo).ToList().ForEach(Sub(r) r.Delete())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub deleteProblemRef(ByVal sRowNo As String)
        Try
            dtHealthConcern.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("Col_RowNo") = sRowNo).ToList().ForEach(Sub(r) r.Delete())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Function getProblemRefCount(ByVal sHealthConcernNo As String) As Integer
        Dim nProblemRefCount As Integer = 0
        Try
            nProblemRefCount = dtHealthConcern.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("Col_HealthConcernNo") = sHealthConcernNo).Count()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return nProblemRefCount
    End Function

    Private Sub tlsp_PatientInjuryDate_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientInjuryDate.ItemClicked

        Select Case e.ClickedItem.Name
            Case ts_btnOk.Name
                SaveCarePlan()
            Case ts_btnCancel.Name
                Me.Close()
        End Select
    End Sub

    Dim oListControl As gloListControl.gloListControl
    Dim ofrmCPTList As frmViewListControl
    Dim _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Other

    Private Sub btnGoalLoinc_Click(sender As System.Object, e As System.EventArgs) Handles btnGoalLoinc.Click
        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CarePlanLoincs, False, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanLoincs
            oListControl.ControlHeader = "LOINC"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "LOINC"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)
        Try

            Select Case _CurrentControlType
                Case gloListControl.gloListControlType.CarePlanLoincs
                    Dim strloinc As String = ""
                    If oListControl.SelectedItems.Count > 0 Then
                        For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                            strloinc = oListControl.SelectedItems(i).Code + " : " + oListControl.SelectedItems(i).Description
                            If (strloinc.Trim().Length > 3) Then
                                txtGoalLoinc.Text = strloinc


                            Else
                                txtGoalLoinc.Text = ""
                            End If
                        Next
                    Else
                        txtGoalLoinc.Text = ""
                    End If
                Case gloListControl.gloListControlType.CarePlanProblemList
                    cmbGoalProblem.DataSource = Nothing
                    cmbGoalProblem.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbGoalProblem.DataSource = oBindTable
                        cmbGoalProblem.DisplayMember = "DispName"
                        cmbGoalProblem.ValueMember = "ID"
                    End If
                Case gloListControl.gloListControlType.CarePlanMedication
                    cmbInterventionMedication.DataSource = Nothing
                    cmbInterventionMedication.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionMedication.DataSource = oBindTable
                        cmbInterventionMedication.DisplayMember = "DispName"
                        cmbInterventionMedication.ValueMember = "ID"
                    End If

                Case gloListControl.gloListControlType.CarePlanEncounter
                    cmbInterventionEncounter.DataSource = Nothing
                    cmbInterventionEncounter.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionEncounter.DataSource = oBindTable
                        cmbInterventionEncounter.DisplayMember = "DispName"
                        cmbInterventionEncounter.ValueMember = "ID"
                    End If
                Case gloListControl.gloListControlType.CarePlanImmunization
                    cmbInterventionImmunization.DataSource = Nothing
                    cmbInterventionImmunization.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionImmunization.DataSource = oBindTable
                        cmbInterventionImmunization.DisplayMember = "DispName"
                        cmbInterventionImmunization.ValueMember = "ID"
                    End If
                Case gloListControl.gloListControlType.CarePlanEducation
                    cmbInterventionPatientEducation.DataSource = Nothing
                    cmbInterventionPatientEducation.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionPatientEducation.DataSource = oBindTable
                        cmbInterventionPatientEducation.DisplayMember = "DispName"
                        cmbInterventionPatientEducation.ValueMember = "ID"
                    End If
                Case gloListControl.gloListControlType.CarePlanNutrition
                    cmbInterventionNutrition.DataSource = Nothing
                    cmbInterventionNutrition.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionNutrition.DataSource = oBindTable
                        cmbInterventionNutrition.DisplayMember = "DispName"
                        cmbInterventionNutrition.ValueMember = "ID"
                    End If
                Case gloListControl.gloListControlType.CarePlanLabOrder
                    cmbInterventionLabOrder.DataSource = Nothing
                    cmbInterventionLabOrder.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionLabOrder.DataSource = oBindTable
                        cmbInterventionLabOrder.DisplayMember = "DispName"
                        cmbInterventionLabOrder.ValueMember = "ID"
                    End If
                Case gloListControl.gloListControlType.CarePlanGoals
                    cmbInterventionGoals.DataSource = Nothing
                    cmbInterventionGoals.Items.Clear()
                    If oListControl.SelectedItems.Count > 0 Then
                        Dim oBindTable As New DataTable()

                        oBindTable.Columns.Add("ID")
                        oBindTable.Columns.Add("DispName")

                        For cnt As Int32 = 0 To oListControl.SelectedItems.Count - 1
                            Dim oRow As DataRow
                            oRow = oBindTable.NewRow()
                            oRow(0) = oListControl.SelectedItems(cnt).ID
                            oRow(1) = oListControl.SelectedItems(cnt).Description
                            oBindTable.Rows.Add(oRow)
                        Next

                        cmbInterventionGoals.DataSource = oBindTable
                        cmbInterventionGoals.DisplayMember = "DispName"
                        cmbInterventionGoals.ValueMember = "ID"
                    End If
                Case Else

            End Select

            ofrmCPTList.Close()
            'If IsNothing(ofrmCPTList) = False Then
            '    ofrmCPTList = Nothing
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub oListControl_ItemClosedClick(sender As System.Object, e As System.EventArgs)
        ofrmCPTList.Close()
        'If IsNothing(ofrmCPTList) = False Then
        '    ofrmCPTList = Nothing
        'End If
    End Sub

    Private Sub btnClearGoalLoinc_Click(sender As System.Object, e As System.EventArgs) Handles btnClearGoalLoinc.Click
        txtGoalLoinc.Text = ""
    End Sub

    Private Sub btnGoalProblem_Click(sender As System.Object, e As System.EventArgs) Handles btnGoalProblem.Click
        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CarePlanProblemList, True, Me.Width)
            oListControl.PatientID = _PatientID
            oListControl.ControlHeader = "Problem List"
            _CurrentControlType = gloListControl.gloListControlType.CarePlanProblemList
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)

            If cmbGoalProblem.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbGoalProblem.Items.Count - 1
                    cmbGoalProblem.SelectedIndex = i
                    cmbGoalProblem.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbGoalProblem.SelectedValue), cmbGoalProblem.Text)
                Next
            End If

            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Problem List"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearGoalProblem_Click(sender As System.Object, e As System.EventArgs) Handles btnClearGoalProblem.Click
        ' cmbGoalProblem.Items.Clear();
        cmbGoalProblem.DataSource = Nothing
        cmbGoalProblem.Items.Clear()
        cmbGoalProblem.Refresh()
    End Sub

    Private Sub btnAddGoal_Click(sender As System.Object, e As System.EventArgs) Handles btnAddGoal.Click
        Try
            If ValidateHealthConcern() Then
                'Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(dgSelectHealthConcern.Rows.Count + 1)
                Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(1)
                Dim nGoalId As Int64 = dtUniqueIds.Rows(0)(0)

                dtGoal.Rows.Add()
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Goal_RowNo") = nGoalId
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalNo") = nGoalId
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Goal_Name") = txtGoalName.Text
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Loinc") = txtGoalLoinc.Text
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalValue") = txtGoalValue.Text
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalUnit") = cmbGoalUnit.Text
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_GoalAuthoer") = sGoalFrom
                dtGoal.Rows(dtGoal.Rows.Count - 1)("Col_Gl_Notes") = txtGoalNotes.Text
                For j As Integer = 0 To dgSelectHealthConcern.Rows.Count - 1
                    If dgSelectHealthConcern.Item("Col_SelectRef_Sel", j).Value = 1 Then
                        dtGoalAssociation.Rows.Add()
                        dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("nGoalId") = nGoalId
                        dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("AssociationType") = GoalAssociationType.HealthConcern
                        dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("AssociationID") = dgSelectHealthConcern.Item("Col_HealthConcernNo_Sel", j).Value
                        dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("AssociationName") = dgSelectHealthConcern.Item("Col_HealthConcernName_Sel", j).Value
                    End If
                Next
                For j As Integer = 0 To cmbGoalProblem.Items.Count - 1
                    cmbGoalProblem.Text = ""
                    cmbGoalProblem.SelectedIndex = j
                    dtGoalAssociation.Rows.Add()
                    dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("nGoalId") = nGoalId
                    dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("AssociationType") = GoalAssociationType.Problem
                    dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("AssociationID") = cmbGoalProblem.SelectedValue
                    dtGoalAssociation.Rows(dtGoalAssociation.Rows.Count - 1)("AssociationName") = cmbGoalProblem.GetItemText(cmbGoalProblem.Items(j))
                Next

                refreshGoalGrid()

                If Not dtUniqueIds Is Nothing Then
                    dtUniqueIds.Rows.Clear()
                    dtUniqueIds.Dispose()
                    dtUniqueIds = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub refreshGoalGrid()
        Try
            dgGoal.Rows.Clear()
            dgSelectGoal.Rows.Clear()
            For i As Int32 = 0 To dtGoal.Rows.Count - 1
                dgGoal.Rows.Add()
                ' dgSelectGoal.Rows.Add()
                For j As Int32 = 0 To dtGoal.Columns.Count - 1
                    If dtGoal.Columns(j).ColumnName = "Col_GL_HealthConcernName" Then
                        Dim CBCell As DataGridViewComboBoxCell = New DataGridViewComboBoxCell()
                        CBCell = dgGoal.Rows(i).Cells("Col_GL_HealthConcernName")
                        For assn As Int32 = 0 To dtGoalAssociation.Rows.Count - 1
                            CBCell.Items.Add(dtGoalAssociation.Rows(assn)("AssociationName"))
                        Next
                        dgGoal.Rows(i).Cells("Col_GL_HealthConcernName").Value = CBCell.Items.Add(dtGoalAssociation.Rows(0)("AssociationName"))
                    ElseIf dtGoal.Columns(j).ColumnName = "Col_GL_ProblemRef" Then
                        Dim CBCell As DataGridViewComboBoxCell = New DataGridViewComboBoxCell()
                        CBCell = dgGoal.Rows(i).Cells("Col_GL_ProblemRef")
                        For assn As Int32 = 0 To dtGoalAssociation.Rows.Count - 1
                            CBCell.Items.Add(dtGoalAssociation.Rows(assn)("AssociationName"))
                        Next
                        dgGoal.Rows(i).Cells("Col_GL_ProblemRef").Value = CBCell.Items.Add(dtGoalAssociation.Rows(0)("AssociationName"))
                    Else
                        dgGoal.Item(dtGoal.Columns(j).ColumnName, dgGoal.RowCount - 1).Value = dtGoal.Rows(i)(j)
                        '  dgSelectGoal.Item(dtGoal.Columns(j).ColumnName + "_Sel", dgGoal.RowCount - 1).Value = dtGoal.Rows(i)(j)
                    End If
                Next
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub deleteGoal(ByVal sRowNo As String)
        Try
            dtGoal.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("Col_Gl_Goal_RowNo") = sRowNo).ToList().ForEach(Sub(r) r.Delete())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub btnGoalRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnGoalRemove.Click
        Try
            If Not IsNothing(dgGoal.CurrentRow) Then  ''added for bugid 70999 if no row exist
                If dgGoal.CurrentRow.Index >= 0 Then
                    If MessageBox.Show(" Are you sure you want to delete selected goal?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        deleteGoal(dgGoal.CurrentRow.Cells("Col_Gl_Goal_RowNo").Value)
                        refreshGoalGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub btnInterventionSnomed_Click(sender As System.Object, e As System.EventArgs)
        Dim strtxtSnomed As String = ""
        Dim strOldConcept As String = ""
        Dim strOldDesc As String = ""

        Dim frm As gloSnoMed.FrmSelectProblem = Nothing
        Try
            strtxtSnomed = "" 'txtInterventionSnomed.Text
            strOldConcept = strInterventionConceptID
            strOldDesc = strInterventionDescription

            gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
            frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())
            Dim arrIcd() As String = txtHealthStatusDesc.Text.Split("-")

            frm.strCodeSystem = "SNOMED"
            frm.txtSMSearch.Text = strInterventionConceptID

            frm.blnIsProblem = True
            frm.strConceptID = strInterventionConceptID
            frm.strConceptDesc = strInterventionDescription
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            If frm._DialogResult = True Then

                frm.strProblem = Replace(Replace(frm.strProblem, " (disorder)", ""), " (finding)", "")

                strInterventionConceptID = frm.strSelectedConceptID
                strInterventionDescription = Replace(Replace(frm.strSelectedDescription, " (disorder)", ""), " (finding)", "")

                If frm.strProblem <> "" Then
                    'txtInterventionSnomed.Text = strInterventionConceptID + " - " + frm.strProblem
                Else
                    ' txtInterventionSnomed.Text = ""
                End If
                'End If

                If frm.strProblem.Trim() <> "" Then
                    'txt_Problem.Text = frm.strProblem.Trim()
                    strInterventionDescription = frm.strProblem.Trim()
                End If
            Else
                strInterventionConceptID = ""
                strInterventionDescription = ""
                If strtxtSnomed <> "" Then
                    'txtInterventionSnomed.Text = strtxtSnomed
                End If
                'Bug No. 71034 Resolved::Problem List - Application does not save SNOMED code when new problem is created.
                If strOldConcept <> "" Then
                    strInterventionConceptID = strOldConcept
                End If
                If strOldDesc <> "" Then
                    strInterventionDescription = strOldDesc
                End If
                '
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Finally
            strtxtSnomed = Nothing
            strOldConcept = Nothing
            strOldDesc = Nothing
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
    End Sub

    Private Sub btnAddIntervention_Click(sender As System.Object, e As System.EventArgs) Handles btnAddIntervention.Click
        Try
            If ValidateHealthConcern() Then
                Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(dgSelectGoal.Rows.Count + 1)
                Dim nIntId As Int64 = dtUniqueIds.Rows(0)(0)
                Dim nIdCount As Int32 = 0
                For j As Integer = 0 To dgSelectGoal.Rows.Count - 1
                    If dgSelectGoal.Item("Col_Gl_SelectGoal_Sel", j).Value = 1 Then
                        dtIntervention.Rows.Add()
                        nIdCount += 1
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_RowNo") = dtUniqueIds.Rows(nIdCount)(0)
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_InterventionNo") = nIntId
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalRowNo") = dgSelectGoal.Item("Col_Gl_Goal_RowNo_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalNo") = dgSelectGoal.Item("Col_Gl_GoalNo_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_HealthConcernRowNo") = dgSelectGoal.Item("Col_GL_HealthConcernRowNo_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_HealthConcernNo") = dgSelectGoal.Item("Col_GL_HealthConcernNo_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_IntSnomed") = "" 'txtInterventionSnomed.Text
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_IntNotes") = txtInterventionNotes.Text
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalLoinc") = dgSelectGoal.Item("Col_Gl_Loinc_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalValue") = dgSelectGoal.Item("Col_Gl_GoalValue_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalUnit") = dgSelectGoal.Item("Col_Gl_GoalUnit_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_GoalNotes") = dgSelectGoal.Item("Col_Gl_Notes_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_HealthStatus") = dgSelectGoal.Item("Col_GL_HealthStatus_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_ProbelmRefId") = dgSelectGoal.Item("Col_GL_ProbelmRefId_Sel", j).Value
                        dtIntervention.Rows(dtIntervention.Rows.Count - 1)("Col_Int_ProblemRef") = dgSelectGoal.Item("Col_GL_ProblemRef_Sel", j).Value
                    End If
                Next

                refreshInterventionGrid()

                If Not dtUniqueIds Is Nothing Then
                    dtUniqueIds.Rows.Clear()
                    dtUniqueIds.Dispose()
                    dtUniqueIds = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub refreshInterventionGrid()
        Try
            dgIntervention.Rows.Clear()
            dgSelectIntervention.Rows.Clear()
            For i As Int32 = 0 To dtIntervention.Rows.Count - 1
                dgIntervention.Rows.Add()
                dgSelectIntervention.Rows.Add()
                For j As Int32 = 0 To dtIntervention.Columns.Count - 1
                    dgIntervention.Item(dtIntervention.Columns(j).ColumnName, dgIntervention.RowCount - 1).Value = dtIntervention.Rows(i)(j)
                    dgSelectIntervention.Item(dtIntervention.Columns(j).ColumnName + "_Sel", dgSelectIntervention.RowCount - 1).Value = dtIntervention.Rows(i)(j)
                Next
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRemoveIntervention_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveIntervention.Click
        Try
            If Not IsNothing(dgIntervention.CurrentRow) Then  ''added for bugid 70999 if no row exist
                If dgIntervention.CurrentRow.Index >= 0 Then
                    If MessageBox.Show(" Are you sure you want to delete selected intervention?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        deleteIntervention(dgIntervention.CurrentRow.Cells("Col_Int_RowNo").Value)
                        refreshInterventionGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub deleteIntervention(ByVal sRowNo As String)
        Try
            dtIntervention.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("Col_Int_RowNo") = sRowNo).ToList().ForEach(Sub(r) r.Delete())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub btnAddOutcome_Click(sender As System.Object, e As System.EventArgs) Handles btnAddOutcome.Click
        Try
            If ValidateHealthConcern() Then
                Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(dgSelectIntervention.Rows.Count + 1)
                Dim nOutId As Int64 = dtUniqueIds.Rows(0)(0)
                Dim nIdCount As Int32 = 0
                For j As Integer = 0 To dgSelectIntervention.Rows.Count - 1
                    If dgSelectIntervention.Item("Col_Int_SelectInt_Sel", j).Value = 1 Then
                        dtOutcome.Rows.Add()
                        nIdCount += 1
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_RowNo") = dtUniqueIds.Rows(nIdCount)(0)
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_OutcomeNo") = nOutId
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_IntRowNo") = dgSelectIntervention.Item("Col_Int_RowNo_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_InterventionNo") = dgSelectIntervention.Item("Col_Int_InterventionNo_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalRowNo") = dgSelectIntervention.Item("Col_Int_GoalRowNo_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalNo") = dgSelectIntervention.Item("Col_Int_GoalNo_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_HealthConcernRowNo") = dgSelectIntervention.Item("Col_Int_HealthConcernRowNo_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_HealthConcernNo") = dgSelectIntervention.Item("Col_Int_HealthConcernNo_Sel", j).Value

                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_OutcomeStatus") = sOutcomeStatus
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_OutcomeNotes") = txtOutcome.Text

                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_IntSnomed") = dgSelectIntervention.Item("Col_Int_IntSnomed_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_IntNotes") = dgSelectIntervention.Item("Col_Int_IntNotes_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalLoinc") = dgSelectIntervention.Item("Col_Int_GoalLoinc_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalValue") = dgSelectIntervention.Item("Col_Int_GoalValue_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalUnit") = dgSelectIntervention.Item("Col_Int_GoalUnit_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_GoalNotes") = dgSelectIntervention.Item("Col_Int_GoalNotes_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_HealthStatus") = dgSelectIntervention.Item("Col_Int_HealthStatus_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_ProbelmRefId") = dgSelectIntervention.Item("Col_Int_ProbelmRefId_Sel", j).Value
                        dtOutcome.Rows(dtOutcome.Rows.Count - 1)("Col_Out_ProblemRef") = dgSelectIntervention.Item("Col_Int_ProblemRef_Sel", j).Value
                    End If
                Next

                refreshOutcomeGrid()
                If Not dtUniqueIds Is Nothing Then
                    dtUniqueIds.Rows.Clear()
                    dtUniqueIds.Dispose()
                    dtUniqueIds = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub refreshOutcomeGrid()
        Try
            dgOutcome.Rows.Clear()
            For i As Int32 = 0 To dtOutcome.Rows.Count - 1
                dgOutcome.Rows.Add()
                For j As Int32 = 0 To dtOutcome.Columns.Count - 1
                    dgOutcome.Item(dtOutcome.Columns(j).ColumnName, dgOutcome.RowCount - 1).Value = dtOutcome.Rows(i)(j)
                Next
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRemoveOutcome_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveOutcome.Click
        Try
            If Not IsNothing(dgOutcome.CurrentRow) Then  ''added for bugid 70999 if no row exist
                If dgOutcome.CurrentRow.Index >= 0 Then
                    If MessageBox.Show(" Are you sure you want to delete selected outcome?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        deleteOutcome(dgOutcome.CurrentRow.Cells("Col_Out_RowNo").Value)
                        refreshOutcomeGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub deleteOutcome(ByVal sRowNo As String)
        Try
            dtOutcome.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("Col_Out_RowNo") = sRowNo).ToList().ForEach(Sub(r) r.Delete())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub SaveCarePlan()
        Dim objCarePlan As New ClsCarePlan_V2()
        Try
            Dim nReturnId As Int64
            Dim nID As Int64 = 0
            fillTVPs()
            nReturnId = objCarePlan.SaveCarePlan(nID, _VisitID, _PatientID, txtCarePlanName.Text, txtCarePlanReason.Text, dtHealthConcernTVP, dtHealthConcernAssociationTVP, dtGoalTVP, dtGoalAssociationTVP, dtInterventionTVP, dtInterventionAssociationTVP, dtOutcomeTVP, dtOutcomeAssociationTVP)

            If nID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Care Plan_V2 added", _PatientID, nReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Patient Care Plan_V2 Modified", _PatientID, nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objCarePlan IsNot Nothing Then
                objCarePlan.Dispose()
                objCarePlan = Nothing
            End If
        End Try

    End Sub

    Private Sub fillTVPs()
        Try
            'values for CP_TVPHealthConcerns and CP_TVPHealthConcernAssociation TVP
            dtHealthConcernTVP.Rows.Clear()
            dtHealthConcernAssociationTVP.Rows.Clear()

            For Each row As DataRow In dtHealthConcern.Rows
                Dim healthConcernNo As String = row("Col_HealthConcernNo")
                Dim contains As Boolean = dtHealthConcernTVP.AsEnumerable().Any(Function(h) healthConcernNo = h.Field(Of [String])("nHealthConcernID"))
                If Not contains Then
                    dtHealthConcernTVP.Rows.Add()
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("nHealthConcernID") = row("Col_HealthConcernNo")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("nCarePlanId") = "0"
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("nPatientId") = _PatientID
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernSnomedCode") = row("Col_ConsernId")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernSnomedDescription") = row("Col_ConcernDesc")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthStatusDescription") = row("Col_HealthStatusDesc")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernAuthor") = row("Col_Author")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernStatus") = row("Col_ConcernStatus")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("dtHealthConcernStartDate") = row("Col_ConcernStartDate")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("dtHealthConcernEndDate") = row("Col_ConcernEndDate")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernNotes") = row("Col_ConcernNotes")
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("dtHealthConcernRecordedDate") = ""
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernSectionLoincCode") = ""
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernLoincCode") = ""
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("sHealthConcernLoincDesc") = ""
                    dtHealthConcernTVP.Rows(dtHealthConcernTVP.Rows.Count - 1)("RowState") = "Added"
                End If
                If row("Col_ProbelmRefId").ToString() <> "0" Then
                    dtHealthConcernAssociationTVP.Rows.Add()
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nHealthConcernAssociationID") = row("Col_RowNo")
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nCarePlanId") = "0"
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nHealthConcernID") = row("Col_HealthConcernNo")
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nAssociatedConcernId") = row("Col_ProbelmRefId")
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("sAssociatedConcernType") = "Problem"
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                    dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                End If
            Next

            'values for CP_TVPGoal and CP_TVPGoalAssociation TVP
            dtGoalTVP.Rows.Clear()
            dtGoalAssociationTVP.Rows.Clear()
            For Each row As DataRow In dtGoal.Rows
                Dim goalNo As String = row("Col_Gl_GoalNo")
                Dim contains As Boolean = dtGoalTVP.AsEnumerable().Any(Function(g) goalNo = g.Field(Of [String])("nGoalID"))
                If Not contains Then
                    Dim goalLoincCode As String = ""
                    Dim goalLoincDesc As String = ""
                    Dim goalLoinc As String() = row("Col_Gl_Loinc").ToString().Split(":")
                    If goalLoinc.Length >= 2 Then
                        goalLoincCode = goalLoinc(0)
                        goalLoincDesc = goalLoinc(1)
                    ElseIf goalLoinc.Length >= 1 Then
                        goalLoincCode = goalLoinc(0)
                    End If

                    dtGoalTVP.Rows.Add()
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("nGoalID") = row("Col_Gl_GoalNo")
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("nCarePlanId") = "0"
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("nPatientId") = _PatientID
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalLoincCode") = goalLoincCode
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalLoincDescription") = goalLoincDesc
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalValue") = row("Col_Gl_GoalValue")
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalUnit") = row("Col_Gl_GoalUnit")
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalNotes") = row("Col_Gl_Notes")
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalAuthor") = row("Col_Gl_GoalAuthoer")
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("dtGoalRecordedDate") = ""
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("sGoalSectionLoincCode") = ""
                    dtGoalTVP.Rows(dtGoalTVP.Rows.Count - 1)("RowState") = "Added"
                End If
                dtGoalAssociationTVP.Rows.Add()
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalAssociationID") = row("Col_Gl_Goal_RowNo")
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nCarePlanId") = "0"
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalId") = row("Col_Gl_GoalNo")
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nHealthConcernID") = row("Col_GL_HealthConcernNo")
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nHealthConcernAssociationID") = row("Col_GL_HealthConcernRowNo")
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("RowState") = "Added"
            Next

            'values for CP_TVPInterventions and CP_TVPInterventionAssociation TVP
            dtInterventionTVP.Rows.Clear()
            dtInterventionAssociationTVP.Rows.Clear()
            For Each row As DataRow In dtIntervention.Rows
                Dim interventionNo As String = row("Col_Int_InterventionNo")
                Dim contains As Boolean = dtInterventionTVP.AsEnumerable().Any(Function(i) interventionNo = i.Field(Of [String])("nInterventionId"))
                If Not contains Then
                    Dim interventionSnomedCode As String = ""
                    Dim interventionSnomedDesc As String = ""
                    Dim interventionSnomed As String() = row("Col_Int_IntSnomed").ToString().Split("-")
                    If interventionSnomed.Length >= 2 Then
                        interventionSnomedCode = interventionSnomed(0)
                        interventionSnomedDesc = interventionSnomed(1)
                    ElseIf interventionSnomed.Length >= 1 Then
                        interventionSnomedCode = interventionSnomed(0)
                    End If

                    dtInterventionTVP.Rows.Add()
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("nInterventionId") = row("Col_Int_InterventionNo")
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("nCarePlanId") = "0"
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("nPatientId") = _PatientID
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("sInterventionSnomedCode") = interventionSnomedCode
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("sInterventionSnomedDescription") = interventionSnomedDesc
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("sInterventionNotes") = row("Col_Int_IntNotes")
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("dtInterventionRecordedDate") = ""
                    dtInterventionTVP.Rows(dtInterventionTVP.Rows.Count - 1)("RowState") = "Added"
                End If
                dtInterventionAssociationTVP.Rows.Add()
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = row("Col_Int_RowNo")
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nCarePlanId") = "0"
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = row("Col_Int_InterventionNo")
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nGoalID") = row("Col_Int_GoalNo")
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nGoalAssociationID") = row("Col_Int_GoalRowNo")
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
            Next

            'values for CP_TVPOutcomes and CP_TVPOutcomeAssociation TVP
            dtOutcomeTVP.Rows.Clear()
            dtOutcomeAssociationTVP.Rows.Clear()
            For Each row As DataRow In dtOutcome.Rows
                Dim outcomeNo As String = row("Col_Out_OutcomeNo")
                Dim contains As Boolean = dtOutcomeTVP.AsEnumerable().Any(Function(o) outcomeNo = o.Field(Of [String])("nOutcomeID"))
                If Not contains Then
                    dtOutcomeTVP.Rows.Add()
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("nOutcomeID") = row("Col_Out_OutcomeNo")
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("nCarePlanId") = "0"
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("nPatientId") = _PatientID
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("sOutcomeStatus") = row("Col_Out_OutcomeStatus")
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("sOutcomeNotes") = row("Col_Out_OutcomeNotes")
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("dtOutcomeRecordedDate") = ""
                    dtOutcomeTVP.Rows(dtOutcomeTVP.Rows.Count - 1)("RowState") = "Added"
                End If
                dtOutcomeAssociationTVP.Rows.Add()
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeAssociationID") = row("Col_Out_RowNo")
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nCarePlanId") = "0"
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeID") = row("Col_Out_OutcomeNo")
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nInterventionId") = row("Col_Out_InterventionNo")
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = row("Col_Out_IntRowNo")
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("RowState") = "Added"
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbt_StatusInactive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_StatusInactive.CheckedChanged
        If rbt_StatusInactive.Checked = True Then
            rbt_StatusInactive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sConcernStatus = ConcernStatus.Inactive.ToString()
        Else
            rbt_StatusInactive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_StatusCompleted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_StatusCompleted.CheckedChanged
        If rbt_StatusCompleted.Checked = True Then
            rbt_StatusCompleted.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sConcernStatus = ConcernStatus.Completed.ToString()
        Else
            rbt_StatusCompleted.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_StatusActive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_StatusActive.CheckedChanged
        If rbt_StatusActive.Checked = True Then
            rbt_StatusActive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sConcernStatus = ConcernStatus.Active.ToString()
        Else
            rbt_StatusActive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_FromProvider_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_FromProvider.CheckedChanged
        If rbt_FromProvider.Checked = True Then
            rbt_FromProvider.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sConcernFrom = ConcernFrom.Provider.ToString()
        Else
            rbt_FromProvider.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_FromPatient_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_FromPatient.CheckedChanged
        If rbt_FromPatient.Checked = True Then
            rbt_FromPatient.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sConcernFrom = ConcernFrom.Patient.ToString()
        Else
            rbt_FromPatient.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_FromBoth_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_FromBoth.CheckedChanged
        If rbt_FromBoth.Checked = True Then
            rbt_FromBoth.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sConcernFrom = ConcernFrom.Both.ToString()
        Else
            rbt_FromBoth.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_GoalFromProvider_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_GoalFromProvider.CheckedChanged
        If rbt_GoalFromProvider.Checked = True Then
            rbt_GoalFromProvider.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sGoalFrom = GoalFrom.Provider.ToString()
        Else
            rbt_GoalFromProvider.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_GoalFromPatient_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_GoalFromPatient.CheckedChanged
        If rbt_GoalFromPatient.Checked = True Then
            rbt_GoalFromPatient.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sGoalFrom = GoalFrom.Patient.ToString()
        Else
            rbt_GoalFromPatient.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_GoalFromBoth_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_GoalFromBoth.CheckedChanged
        If rbt_GoalFromBoth.Checked = True Then
            rbt_GoalFromBoth.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sGoalFrom = GoalFrom.Both.ToString()
        Else
            rbt_GoalFromBoth.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_OutcomeStatusActive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_OutcomeStatusActive.CheckedChanged
        If rbt_OutcomeStatusActive.Checked = True Then
            rbt_OutcomeStatusActive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sOutcomeStatus = OutcomeStatus.Active.ToString()
        Else
            rbt_OutcomeStatusActive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_OutcomeStatusCompleted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_OutcomeStatusCompleted.CheckedChanged
        If rbt_OutcomeStatusCompleted.Checked = True Then
            rbt_OutcomeStatusCompleted.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sOutcomeStatus = OutcomeStatus.Completed.ToString()
        Else
            rbt_OutcomeStatusCompleted.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_OutcomeStatusInctive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_OutcomeStatusInctive.CheckedChanged
        If rbt_OutcomeStatusInctive.Checked = True Then
            rbt_OutcomeStatusInctive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            sOutcomeStatus = OutcomeStatus.Inactive.ToString()
        Else
            rbt_OutcomeStatusInctive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnClearProblems_Click(sender As System.Object, e As System.EventArgs) Handles btnClearProblems.Click
        cmbProblem.Items.Clear()
    End Sub

    Private Sub cmbGoalUnit_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs) Handles cmbGoalUnit.SelectionChangeCommitted
        ''cmbGoalUnit
    End Sub


    Private Sub tbCarePlan_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tbCarePlan.SelectedIndexChanged
        Try
            If (tbCarePlan.SelectedTab.Text = "Goals") Then
                If Not IsNothing(dtLoincUnits) Then
                    cmbGoalUnit.DataSource = dtLoincUnits
                    cmbGoalUnit.DisplayMember = dtLoincUnits.Columns("UCUMUnits").ToString()
                    cmbGoalUnit.ValueMember = ""
                Else
                    ''get loinc units from database
                    Dim objPatientCarePlan As New ClsPatientCarePlan()
                    dtLoincUnits = objPatientCarePlan.GetUnits()
                    If Not IsNothing(dtLoincUnits) Then
                        cmbGoalUnit.DataSource = dtLoincUnits
                        cmbGoalUnit.DisplayMember = dtLoincUnits.Columns("UCUMUnits").ToString()
                        cmbGoalUnit.ValueMember = ""
                    End If
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnInterventionMedication_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionMedication.Click
        Try
            Try

                ofrmCPTList = New frmViewListControl
                oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanMedication, True, Me.Width)
                _CurrentControlType = gloListControl.gloListControlType.CarePlanMedication
                oListControl.ControlHeader = "Medications"
                AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Add(oListControl)
                oListControl.Dock = DockStyle.Fill
                oListControl.BringToFront()

                oListControl.ShowHeaderPanel(False)
                If cmbInterventionMedication.DataSource IsNot Nothing Then
                    For i As Integer = 0 To cmbInterventionMedication.Items.Count - 1
                        cmbInterventionMedication.SelectedIndex = i
                        cmbInterventionMedication.Refresh()
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionMedication.SelectedValue), cmbInterventionMedication.Text)
                    Next
                End If
                oListControl.OpenControl()
                ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                ofrmCPTList.Text = "Medications"
                ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

                If IsNothing(ofrmCPTList) = False Then
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                    ofrmCPTList.Controls.Remove(oListControl)
                    oListControl.Dispose()
                    oListControl = Nothing
                    ofrmCPTList.Dispose()
                    ofrmCPTList = Nothing
                    'End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnInterventionEncounter_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionEncounter.Click
        Try
            Try

                ofrmCPTList = New frmViewListControl
                oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanEncounter, True, Me.Width)
                _CurrentControlType = gloListControl.gloListControlType.CarePlanEncounter
                oListControl.ControlHeader = "Encounter"
                AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Add(oListControl)
                oListControl.Dock = DockStyle.Fill
                oListControl.BringToFront()

                oListControl.ShowHeaderPanel(False)
                If cmbInterventionEncounter.DataSource IsNot Nothing Then
                    For i As Integer = 0 To cmbInterventionEncounter.Items.Count - 1
                        cmbInterventionEncounter.SelectedIndex = i
                        cmbInterventionEncounter.Refresh()
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionEncounter.SelectedValue), cmbInterventionEncounter.Text)
                    Next
                End If
                oListControl.OpenControl()
                ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                ofrmCPTList.Text = "Encounter"
                ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

                If IsNothing(ofrmCPTList) = False Then
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                    ofrmCPTList.Controls.Remove(oListControl)
                    oListControl.Dispose()
                    oListControl = Nothing
                    ofrmCPTList.Dispose()
                    ofrmCPTList = Nothing
                    'End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnInterventionLabOrder_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionLabOrder.Click

        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanLabOrder, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanLabOrder
            oListControl.ControlHeader = "Lab Order"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            If cmbInterventionLabOrder.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbInterventionLabOrder.Items.Count - 1
                    cmbInterventionLabOrder.SelectedIndex = i
                    cmbInterventionLabOrder.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionLabOrder.SelectedValue), cmbInterventionLabOrder.Text)
                Next
            End If
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Lab Order"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnInterventionImmunization_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionImmunization.Click

        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanImmunization, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanImmunization
            oListControl.ControlHeader = "Immunization"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            If cmbInterventionImmunization.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbInterventionImmunization.Items.Count - 1
                    cmbInterventionImmunization.SelectedIndex = i
                    cmbInterventionImmunization.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionImmunization.SelectedValue), cmbInterventionImmunization.Text)
                Next
            End If
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Immunization"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnInterventionPatientEducation_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionPatientEducation.Click

        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanEducation, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanEducation
            oListControl.ControlHeader = "Education"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            If cmbInterventionPatientEducation.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbInterventionPatientEducation.Items.Count - 1
                    cmbInterventionPatientEducation.SelectedIndex = i
                    cmbInterventionPatientEducation.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionPatientEducation.SelectedValue), cmbInterventionPatientEducation.Text)
                Next
            End If
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Education"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub btnInterventionNutrition_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionNutrition.Click
        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanNutrition, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanNutrition
            oListControl.ControlHeader = "Nutrition"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            If cmbInterventionNutrition.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbInterventionNutrition.Items.Count - 1
                    cmbInterventionNutrition.SelectedIndex = i
                    cmbInterventionNutrition.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionNutrition.SelectedValue), cmbInterventionNutrition.Text)
                Next
            End If
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Nutrition"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

   
    Private Sub btnInterventionGoals_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionGoals.Click
        Try

            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanGoals, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanGoals
            oListControl.ControlHeader = "Goals"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            If cmbInterventionGoals.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbInterventionGoals.Items.Count - 1
                    cmbInterventionGoals.SelectedIndex = i
                    cmbInterventionGoals.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbInterventionGoals.SelectedValue), cmbInterventionGoals.Text)
                Next
            End If
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Goals"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
