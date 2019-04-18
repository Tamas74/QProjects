Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloEMR.gloStream.DiseaseManagement
Imports System.Drawing
Imports gloEMRGeneralLibrary
Imports System.Data.SqlClient
Imports gloAuditTrail

Public Class frmTreatmentPlan

    Private PatientID As Long
    Private VisitID As Long
    Private ProviderID As Long
    Private PlanOfTreatmentId As Long = 0
    Private PlanStatus As Boolean = True
    Private PlanDeleteStatus As Boolean = False
    Private ExamID As Long = 0
    Private CallingForm As String = ""
    Private SearchString As String = ""
    Private TestType As String = ""
    Private oLabNode As TreeNode = Nothing
    Private dtPOTMST As New DataTable()
    Private dtPOTDTL As New DataTable()
    Private drDrugFromRxMedScreen As DataRow = Nothing
    Private enmSelectedButton As Integer = 0

    Private col_ActivityType As Int16 = 0
    Private col_ActivityDetails As Int16 = 1
    Private col_ActivityTreatmentName As Int16 = 2
    Private col_ActivityCode As Int16 = 3
    Private col_ActivityCodeSystem As Int16 = 4
    Private col_ActivityFrom As Int16 = 5
    Private col_ActivityTo As Int16 = 6
    Private col_ActivityStatus As Int16 = 7
    Private col_ActivityReason As Int16 = 8
    Private col_ActivityReasonCode As Int16 = 9
    Private col_ActivityTreatmentID As Int16 = 10



    Private Col_eExamID As Integer = 0
    Private Col_eVistitID As Integer = 1
    Private Col_eDos As Integer = 2
    Private Col_eExamName As Integer = 3
    Private Col_eTemplateName As Integer = 4
    Private Col_eFinished As Integer = 5
    Private Col_eProviderName As Integer = 6
    Private Col_eReviewedBy As Integer = 7
    Private Col_eSpeciality As Integer = 8
    Private Col_eCheck As Integer = 9
    Private Col_eCount As Integer = 10


    Public enmselectedbuttonfrmRx As SelectedActivity
    ' Public enmselectedbutton As SelectedActivity
    Public enmselectedLab As SelectedLabType
    Private enmType As POTType
    Private _CarePlanPlanOfTreatmentID As Int64 = 0
    Private MessageBoxCaption As String = "gloEMR"
    Private sDrugSearch As String = ""
    Private WithEvents _PatientStrip As New gloUC_PatientStrip
    Private WithEvents _RxListUserCtrl As gloDrugListRevised
    Private WithEvents dgCustomGridselectExam As CustomTask


    Public Enum POTType
        POT = 1
        CarePlan = 2
    End Enum

    Enum SelectedActivity
        Medication = 1
        LabOrder = 2
        Encounter = 3
        Nutrition = 4
    End Enum

    Enum SelectedLabType
        Radiolody = 1
        Referral = 2
        Other = 3
        Group = 4
        LabTests = 5
    End Enum

    Enum ActivityStatus
        Active = 1
        Complete = 2
        InActive = 3
    End Enum


    Public Property CurrentList() As SelectedActivity
        Get
            Return enmselectedbutton
        End Get
        Set(ByVal value As SelectedActivity)
            enmselectedbutton = value
            SetCurrentCodeList()
        End Set
    End Property

    Public Property Type() As POTType
        Get
            Return enmType
        End Get
        Set(ByVal value As POTType)
            enmType = value
        End Set
    End Property

    Public Property CarePlanPlanOfTreatmentID() As Int64
        Get
            Return _CarePlanPlanOfTreatmentID
        End Get
        Set(ByVal value As Int64)
            _CarePlanPlanOfTreatmentID = value
        End Set
    End Property

    Public Property CurrentLab() As SelectedLabType
        Get
            Return enmselectedLab
        End Get
        Set(ByVal value As SelectedLabType)
            enmselectedLab = value
            SetCurrentLab()
        End Set
    End Property

    'Public Sub New(ByVal _patientID As Long)
    '    MyBase.New()
    '    InitializeComponent()
    '    PatientID = _patientID
    'End Sub
    Public Sub New(ByVal _patientID As Long, ByVal _CallingForm As String, ByVal SelectedDrug As DataRow, ByVal sDrugSearchText As String, ByVal enmSelectedButton As Integer, Optional ByVal _potID As Long = 0)
        MyBase.New()
        InitializeComponent()
        PatientID = _patientID
        enmType = frmTreatmentPlan.POTType.POT
        CallingForm = _CallingForm
        If _potID = 0 Then
            Dim oTreatment As New clsTreatmentPlan()
            PlanOfTreatmentId = oTreatment.GetActivePlanOfTreatmentId(PatientID)
            oTreatment.Dispose()
            oTreatment = Nothing
        Else
            PlanOfTreatmentId = _potID
        End If
        drDrugFromRxMedScreen = SelectedDrug
        sDrugSearch = sDrugSearchText
        Me.enmselectedbuttonfrmRx = enmSelectedButton
        If drDrugFromRxMedScreen IsNot Nothing Then
            _RxListUserCtrl_DrugListClicked(drDrugFromRxMedScreen, Nothing)
        End If
    End Sub

    Public Sub New(ByVal _patientID As Long, Optional ByVal _potID As Long = 0, Optional ByVal PotType As POTType = frmTreatmentPlan.POTType.POT)
        MyBase.New()
        InitializeComponent()
        PatientID = _patientID
        PlanOfTreatmentId = _potID
        enmType = PotType
        ''enmType = PotType.CarePlan
    End Sub

    Public Sub New(ByVal _patientID As Long, ByVal _CallingForm As String, Optional ByVal _potID As Long = 0)
        MyBase.New()
        InitializeComponent()
        PatientID = _patientID
        enmType = frmTreatmentPlan.POTType.POT
        CallingForm = _CallingForm
        If _potID = 0 Then
            Dim oTreatment As New clsTreatmentPlan()
            PlanOfTreatmentId = oTreatment.GetActivePlanOfTreatmentId(PatientID)
            oTreatment.Dispose()
            oTreatment = Nothing
        Else
            PlanOfTreatmentId = _potID
        End If
    End Sub

    Public Sub New(ByVal _patientID As Long, ByVal _CallingForm As String, ByVal oNode As TreeNode, ByVal _TestType As String, ByVal _SearchString As String)
        MyBase.New()
        InitializeComponent()
        PatientID = _patientID
        enmType = frmTreatmentPlan.POTType.POT
        CallingForm = _CallingForm
        SearchString = _SearchString
        TestType = _TestType
        oLabNode = oNode
        Dim oTreatment As New clsTreatmentPlan()
        PlanOfTreatmentId = oTreatment.GetActivePlanOfTreatmentId(PatientID)
        oTreatment.Dispose()
        oTreatment = Nothing
    End Sub

    Private Sub frmTreatmentPlan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadPatientStrip()
        DesignActivityGrid()

        ProviderID = gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(PatientID)
        If enmselectedbuttonfrmRx = 0 Then
            _RxListUserCtrl = New gloDrugListRevised(gnDrugListButton, gblnAllowAddDrugs, gblnAllowDrugConfig, ProviderID, gnClinicID, gnLoginID, PatientID, sDrugSearch, "POT")
        Else
            _RxListUserCtrl = New gloDrugListRevised(enmselectedbuttonfrmRx, gblnAllowAddDrugs, gblnAllowDrugConfig, ProviderID, gnClinicID, gnLoginID, PatientID, sDrugSearch, "POT")
        End If
        AddHandler _RxListUserCtrl.DrugListClicked, AddressOf _RxListUserCtrl_DrugListClicked
        _RxListUserCtrl.Padding = New Padding(0, 3, 0, 0)

        If CallingForm = "" Or CallingForm = "Exam" Or CallingForm = "RxMed" Then
            CurrentList = SelectedActivity.Medication
        ElseIf CallingForm = "Lab" Then
            CurrentList = SelectedActivity.LabOrder
            If Not IsNothing(oLabNode) Then
                oLabNode = Nothing
            End If
        End If

        If PlanOfTreatmentId <> 0 Then
            LoadTreatmentPlan()
        Else
            dtpPlanEffectiveTo.Value = dtpPlanEffectiveTo.Value.AddMinutes(5)
        End If


        c1PlanActivity.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None


        Dim scheme As gloBilling.Cls_TabIndexSettings.TabScheme = gloBilling.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New gloBilling.Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

        If enmType = POTType.CarePlan Then
            pnlHeader.Visible = False
            txtPlantitle.Text = "CarePlan for Patient ID = " & Convert.ToString(PatientID)
            txtPlanAssesment.Text = "CarePlan"
            'dtpPlanEffectiveFrom.Value = Date.MinValue
            'dtpPlanEffectiveTo.Value = Date.MaxValue
            ExamID = 0
        End If
        txtPlanAssesment.Focus()
    End Sub


    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnPOTClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnPOTSaveClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnPOTSaveClose.Click
        If Not ValidateValue() Then
            Return
        End If
        Dim oClsPot As New clsTreatmentPlan()
        Dim bResult As Long = 0
        Try
            FillPOTDetailsData()
            bResult = oClsPot.INUP_PlanOfTreatment(PlanOfTreatmentId, PatientID, txtPlantitle.Text, txtPlanAssesment.Text, dtpPlanEffectiveFrom.Value, dtpPlanEffectiveTo.Value, PlanStatus, PlanDeleteStatus, ExamID, dtPOTDTL, enmType)
            If bResult <> -1 Then
                If enmType = POTType.CarePlan Then
                    _CarePlanPlanOfTreatmentID = bResult
                End If
                MessageBox.Show("Plan of treatment saved successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If PlanOfTreatmentId = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Plan of Treatment Added.", PatientID, bResult, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Plan of Treatment Modified.", PatientID, PlanOfTreatmentId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

                Me.Close()
            End If
        Catch ex As Exception

            If PlanOfTreatmentId = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Plan of Treatment Added.", PatientID, bResult, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Plan of Treatment Modified.", PatientID, PlanOfTreatmentId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Not IsNothing(oClsPot) Then
                oClsPot.Dispose()
                oClsPot = Nothing
            End If
        End Try
    End Sub


    Private Sub ButtonHover(sender As Object, e As System.EventArgs)
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongYellow
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub ButtonMouseLeave(sender As Object, e As System.EventArgs)
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongButton
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub ButtonClick(sender As System.Object, e As System.EventArgs) Handles btnAllCode.Click, btnEncounter.Click, btnMedication.Click, btnLabs.Click, btnNutrition.Click
        If CType(sender, Button).Tag <> "" Then
            If Convert.ToInt32(CType(sender, Button).Tag) <> 0 Then
                CurrentList = CType(sender, Button).Tag
                pnlActivityDetail.Visible = False
                c1PlanActivity.Select(-1, -1)
            End If
        End If

    End Sub

    Private Sub ButtonLabClick(sender As System.Object, e As System.EventArgs) Handles btnRefTest.Click, btnRadiologyImaging.Click, btnOthers.Click, btnGroups.Click, btnLabTests.Click
        If CType(sender, Button).Tag <> "" Then
            If Convert.ToInt32(CType(sender, Button).Tag) <> 0 Then
                CurrentLab = CType(sender, Button).Tag
            End If
        End If

    End Sub

    Private Sub RadioButtonCheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_Complete.CheckedChanged, rbt_Inactive.CheckedChanged, rbt_Active.CheckedChanged
        If CType(sender, RadioButton).Checked Then
            CType(sender, RadioButton).Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            CType(sender, RadioButton).Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub


    Private Sub c1PlanActivity_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PlanActivity.MouseClick
        Dim selectedRow As Int16 = c1PlanActivity.RowSel
        Try
            If selectedRow > 0 Then
                If pnlActivityDetail.Visible = False Then
                    pnlActivityDetail.Visible = True
                End If
                txtActivity.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityDetails))
                txtSelectedCode.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityTreatmentName))
                'Dim fromDate As String = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityFrom))

                'dtpActivityEffectiveTo.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityTo))
                EnableDisableDateControl(c1PlanActivity.RowSel)
                Select Case Convert.ToInt32(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityStatus))
                    Case ActivityStatus.Active
                        rbt_Active.Checked = True
                    Case ActivityStatus.InActive
                        rbt_Inactive.Checked = True
                    Case ActivityStatus.Complete
                        rbt_Complete.Checked = True
                End Select

                txtReason.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReason))
                Dim IDs As String() = Nothing
                cmbProblemList.Items.Clear()
                cmbProblemList.Text = ""
                If Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReasonCode)) <> "" Then
                    IDs = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReasonCode)).Split(New Char() {","c})
                End If
                Dim ID As String
                If IDs IsNot Nothing Then
                    For Each ID In IDs

                        Dim oComboBoxItem As New ComboboxItem()
                        oComboBoxItem.displayMember = Convert.ToString(GetProblemDescription(Convert.ToInt64(ID)))
                        oComboBoxItem.valueMember = Convert.ToInt64(ID)
                        cmbProblemList.Items.Add(oComboBoxItem)
                        cmbProblemList.SelectedIndex = 0
                    Next
                End If
                If CallingForm <> "RxMed" And CallingForm <> "Lab" Then
                    Select Case Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType))
                        Case "Planned Medication"
                            lblMedication.Visible = True
                            lblLabOrders.Visible = False
                            lblEncounter.Visible = False
                            lblNurtitionRecommendation.Visible = False
                            CurrentList = SelectedActivity.Medication
                        Case "Planned Lab Orders"
                            lblMedication.Visible = False
                            lblLabOrders.Visible = True
                            lblEncounter.Visible = False
                            lblNurtitionRecommendation.Visible = False
                            CurrentList = SelectedActivity.LabOrder
                        Case "Planned Encounter"
                            lblMedication.Visible = False
                            lblLabOrders.Visible = False
                            lblEncounter.Visible = True
                            lblNurtitionRecommendation.Visible = False
                            CurrentList = SelectedActivity.Encounter
                        Case "Nutrition Recommendation"
                            lblMedication.Visible = False
                            lblLabOrders.Visible = False
                            lblEncounter.Visible = False
                            lblNurtitionRecommendation.Visible = True
                            CurrentList = SelectedActivity.Nutrition
                    End Select
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            ex = Nothing
        End Try


    End Sub

    Private Sub c1PlanActivity_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PlanActivity.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub c1PlanActivity_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PlanActivity.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                Dim _nRow As Integer = c1PlanActivity.HitTest(e.X, e.Y).Row
                If _nRow > 0 Then
                    c1PlanActivity.Row = _nRow
                End If
                Dim nRow As Integer
                nRow = c1PlanActivity.HitTest(e.X, e.Y).Row
                If nRow > 0 Then
                    oMnuRemovePlannedActivity.ForeColor = Color.FromArgb(31, 73, 125)
                    ''oMnuRemovePlannedActivity.Image = imgTreeView.Images(13)
                    c1PlanActivity.ContextMenuStrip = CmnuPannedActivity
                Else
                    c1PlanActivity.ContextMenuStrip = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub c1PlanActivity_SelChange(sender As System.Object, e As System.EventArgs) Handles c1PlanActivity.SelChange
        Try
            Dim _nRow As Int16 = c1PlanActivity.RowSel
            If _nRow > 0 Then
                c1PlanActivity.Row = _nRow
                txtActivity.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityDetails))
                txtSelectedCode.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityTreatmentName))
                'Dim fromDate As String = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityFrom))
                'dtpActivityEffectiveFrom.Value = Convert.ToDateTime(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityFrom))
                'dtpActivityEffectiveTo.Value = Convert.ToDateTime(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityTo))
                EnableDisableDateControl(c1PlanActivity.RowSel)
                Select Case Convert.ToInt32(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityStatus))
                    Case ActivityStatus.Active
                        rbt_Active.Checked = True
                    Case ActivityStatus.InActive
                        rbt_Inactive.Checked = True
                    Case ActivityStatus.Complete
                        rbt_Complete.Checked = True

                End Select

                txtReason.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReason))
                Dim IDs As String() = Nothing
                If Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReasonCode)) <> "" Then
                    IDs = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReasonCode)).Split(New Char() {","c})
                End If
                Dim ID As String
                If IDs IsNot Nothing Then
                    For Each ID In IDs
                        cmbProblemList.Items.Clear()
                        cmbProblemList.Text = ""
                        Dim oComboBoxItem As New ComboboxItem()
                        oComboBoxItem.displayMember = Convert.ToString(GetProblemDescription(Convert.ToInt64(ID)))
                        oComboBoxItem.valueMember = Convert.ToInt64(ID)
                        cmbProblemList.Items.Add(oComboBoxItem)
                        cmbProblemList.SelectedIndex = 0
                    Next
                End If
                ''cmbProblemList.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityReasonCode))
                Select Case Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType))
                    Case "Planned Medication"
                        lblMedication.Visible = True
                        lblLabOrders.Visible = False
                        lblEncounter.Visible = False
                        lblNurtitionRecommendation.Visible = False
                    Case "Planned Lab Orders"
                        lblMedication.Visible = False
                        lblLabOrders.Visible = True
                        lblEncounter.Visible = False
                        lblNurtitionRecommendation.Visible = False
                    Case "Planned Encounter"
                        lblMedication.Visible = False
                        lblLabOrders.Visible = False
                        lblEncounter.Visible = True
                        lblNurtitionRecommendation.Visible = False
                    Case "Nutrition Recommendation"
                        lblMedication.Visible = False
                        lblLabOrders.Visible = False
                        lblEncounter.Visible = False
                        lblNurtitionRecommendation.Visible = True
                End Select
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub oMnuRemovePlannedActivity_Click(sender As System.Object, e As System.EventArgs) Handles oMnuRemovePlannedActivity.Click
        If c1PlanActivity.RowSel > 0 Then
            c1PlanActivity.RemoveItem(c1PlanActivity.RowSel)
            c1PlanActivity_SelChange(Nothing, Nothing)
        End If
    End Sub

    Private Sub tlsp_btnActivitySave_Click(sender As System.Object, e As System.EventArgs) Handles tlsp_btnActivitySave.Click

        Try

            If c1PlanActivity IsNot Nothing AndAlso c1PlanActivity.RowSel > 0 Then
                'If (dtpActivityEffectiveFrom.Checked = True And dtpActivityEffectiveTo.Checked = False) Or (dtpActivityEffectiveFrom.Checked = False And dtpActivityEffectiveTo.Checked = True) Then
                '    MessageBox.Show("Please select both effective from and to date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    dtpActivityEffectiveTo.Focus()
                'Else
                Dim dtStartDate As Date = Date.MinValue
                Dim dtEndDate As Date = Date.MaxValue
                If dtpActivityEffectiveFrom.Checked Then
                    dtStartDate = dtpActivityEffectiveFrom.Value.Date
                End If
                If dtpActivityEffectiveTo.Checked Then
                    dtEndDate = dtpActivityEffectiveTo.Value.Date
                End If

                If (DateTime.Compare(dtEndDate.Date, dtStartDate.Date) < 0) Then
                    MessageBox.Show("Please check activity effective dates.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dtpActivityEffectiveTo.Focus()
                Else
                    c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityDetails, txtActivity.Text)
                    c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityTreatmentName, txtSelectedCode.Text)
                    If c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType) = "Planned Medication" Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityType, "Planned Medication")
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityCodeSystem, "2.16.840.1.113883.6.88") ''"2.16.840.1.113883.6.88") 'RxNorm
                    ElseIf c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType) = "Planned Lab Orders" Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityType, "Planned Lab Orders")
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityCodeSystem, "2.16.840.1.113883.6.1") '' "2.16.840.1.113883.6.1") 'Loinc
                    ElseIf c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType) = "Planned Encounter" Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityType, "Planned Encounter")
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityCodeSystem, "2.16.840.1.113883.6.96") ''"2.16.840.1.113883.6.96") 'Snomed
                    ElseIf c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType) = "Nutrition Recommendation" Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityType, "Nutrition Recommendation")
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityCodeSystem, "2.16.840.1.113883.6.96") ''"2.16.840.1.113883.6.96") 'Snomed
                    End If
                    If dtpActivityEffectiveFrom.Checked Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityFrom, dtpActivityEffectiveFrom.Value)
                    Else
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityFrom, "-")
                    End If

                    If dtpActivityEffectiveTo.Checked Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityTo, dtpActivityEffectiveTo.Value)
                    Else
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityTo, "-")
                    End If

                    If rbt_Active.Checked Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityStatus, ActivityStatus.Active)
                    ElseIf rbt_Complete.Checked Then
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityStatus, ActivityStatus.Complete)
                    Else
                        c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityStatus, ActivityStatus.InActive)
                    End If

                    c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityReason, txtReason.Text)
                    Dim sCommaSeparatedProblemIDs As String = ""
                    If cmbProblemList IsNot Nothing AndAlso cmbProblemList.Items.Count > 0 Then
                        For i As Integer = 0 To cmbProblemList.Items.Count - 1
                            If cmbProblemList.Items IsNot Nothing Then
                                If sCommaSeparatedProblemIDs = "" Then
                                    sCommaSeparatedProblemIDs = Convert.ToString(DirectCast(cmbProblemList.Items(i), gloEMR.ComboboxItem).valueMember)
                                Else
                                    sCommaSeparatedProblemIDs = sCommaSeparatedProblemIDs + "," + Convert.ToString(DirectCast(cmbProblemList.Items(i), gloEMR.ComboboxItem).valueMember)
                                End If
                            End If
                        Next
                    End If

                    c1PlanActivity.SetData(c1PlanActivity.RowSel, col_ActivityReasonCode, sCommaSeparatedProblemIDs)

                    pnlActivityDetail.Visible = False
                    txtActivity.Text = ""
                    txtSelectedCode.Text = ""
                    txtReason.Text = ""
                    cmbProblemList.Items.Clear()
                    dtpActivityEffectiveFrom.Value = System.DateTime.Now
                    dtpActivityEffectiveTo.Value = System.DateTime.Now
                    rbt_Active.Checked = False
                    rbt_Complete.Checked = False
                    rbt_Inactive.Checked = False
                End If
                ''End If
            Else
                MessageBox.Show("Select planned activity to save details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                c1PlanActivity.Focus()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub tlsp_btnActivityCancel_Click(sender As System.Object, e As System.EventArgs) Handles tlsp_btnActivityCancel.Click
        pnlActivityDetail.Visible = False
    End Sub

#Region "Private Methods"



    Private Sub loadPatientStrip()
        Try
            gloC1FlexStyle.Style(c1PlanActivity)
            _PatientStrip.ShowDetail(PatientID, gloUC_PatientStrip.enumFormName.HealthPlan)
            If Me.Controls.Contains(_PatientStrip) = False Then
                _PatientStrip.Dock = DockStyle.Top
                _PatientStrip.Padding = New Padding(3, 0, 3, 0)
                _PatientStrip.BringToFront()
                Me.Controls.Add(_PatientStrip)
                pnlToolStrip.SendToBack()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub DesignActivityGrid()
        Try
            c1PlanActivity.Cols(col_ActivityType).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityDetails).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityCode).AllowEditing = True
            c1PlanActivity.Cols(col_ActivityCodeSystem).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityFrom).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityTo).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityStatus).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityReason).AllowEditing = False
            c1PlanActivity.Cols(col_ActivityReasonCode).AllowEditing = False

            c1PlanActivity.Cols(col_ActivityType).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityDetails).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityCodeSystem).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityFrom).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityTo).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityStatus).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityReason).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1PlanActivity.Cols(col_ActivityReasonCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            c1PlanActivity.Cols(col_ActivityType).Visible = True
            c1PlanActivity.Cols(col_ActivityTreatmentName).Visible = True
            c1PlanActivity.Cols(col_ActivityDetails).Visible = True
            c1PlanActivity.Cols(col_ActivityCode).Visible = True
            c1PlanActivity.Cols(col_ActivityStatus).Visible = True

            c1PlanActivity.Cols(col_ActivityCodeSystem).Visible = False
            c1PlanActivity.Cols(col_ActivityFrom).Visible = False
            c1PlanActivity.Cols(col_ActivityTo).Visible = False
            c1PlanActivity.Cols(col_ActivityReason).Visible = False
            c1PlanActivity.Cols(col_ActivityReasonCode).Visible = False
            c1PlanActivity.Cols(col_ActivityTreatmentID).Visible = False


            c1PlanActivity.Cols(col_ActivityType).Width = (pnlActivityList.Width * 15) / 100
            c1PlanActivity.Cols(col_ActivityDetails).Width = (pnlActivityList.Width * 35) / 100
            c1PlanActivity.Cols(col_ActivityTreatmentName).Width = (pnlActivityList.Width * 30) / 100
            c1PlanActivity.Cols(col_ActivityCode).Width = (pnlActivityList.Width * 10) / 100
            c1PlanActivity.Cols(col_ActivityStatus).Width = (pnlActivityList.Width * 5) / 100

            c1PlanActivity.Cols(col_ActivityCodeSystem).Width = 0
            c1PlanActivity.Cols(col_ActivityFrom).Width = 0
            c1PlanActivity.Cols(col_ActivityTo).Width = 0
            c1PlanActivity.Cols(col_ActivityReason).Width = 0
            c1PlanActivity.Cols(col_ActivityReasonCode).Width = 0
            c1PlanActivity.Cols(col_ActivityTreatmentID).Width = 0

            c1PlanActivity.Cols(col_ActivityCode).AllowResizing = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub SetCurrentCodeList()
        Try
            pnlbtnMedication.Visible = True
            pnlbntLabs.Visible = True
            pnlbtnEncounter.Visible = True
            SetButtonBackgroundImage(0)

            btnEncounter.Enabled = True
            btnNutrition.Enabled = True
            btnLabs.Enabled = True
            btnMedication.Enabled = True

            Select Case enmselectedbutton
                Case SelectedActivity.Medication
                    SetButtonBackgroundImage(1)
                    lblMedication.Visible = True
                    lblLabOrders.Visible = False
                    lblEncounter.Visible = False
                    lblNurtitionRecommendation.Visible = False
                    btnAllCode.Text = "Planned Medication"
                    If CallingForm = "RxMed" Then
                        btnEncounter.Enabled = False
                        btnNutrition.Enabled = False
                        btnLabs.Enabled = False
                        btnMedication.Enabled = True
                    End If
                Case SelectedActivity.LabOrder
                    SetButtonBackgroundImage(1)
                    btnAllCode.Text = "Planned Lab Orders"
                    lblMedication.Visible = False
                    lblLabOrders.Visible = True
                    lblEncounter.Visible = False
                    lblNurtitionRecommendation.Visible = False
                    If CallingForm = "Lab" Then
                        btnEncounter.Enabled = False
                        btnNutrition.Enabled = False
                        btnLabs.Enabled = True
                        btnMedication.Enabled = False
                    End If

                Case SelectedActivity.Encounter
                    SetButtonBackgroundImage(1)
                    btnAllCode.Text = "Planned Encounter"
                    lblMedication.Visible = False
                    lblLabOrders.Visible = False
                    lblEncounter.Visible = True
                    lblNurtitionRecommendation.Visible = False
                Case SelectedActivity.Nutrition
                    SetButtonBackgroundImage(1)
                    btnAllCode.Text = "Nutrition Recommendation"
                    lblMedication.Visible = False
                    lblLabOrders.Visible = False
                    lblEncounter.Visible = False
                    lblNurtitionRecommendation.Visible = True
            End Select
            LoadCodeList()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub LoadCodeList()
        Try
            Select Case enmselectedbutton
                Case SelectedActivity.Medication
                    pnlLabOrdersList.Visible = False
                    pnlEncounterSnomed.Visible = False
                    pnlNutrition.Visible = False
                    _RxListUserCtrl.Dock = DockStyle.Fill
                    pnlControlContainer.Controls.Add(_RxListUserCtrl)
                    _RxListUserCtrl.Focus()
                Case SelectedActivity.LabOrder
                    If pnlControlContainer.Controls.Contains(_RxListUserCtrl) Then
                        pnlControlContainer.Controls.Remove(_RxListUserCtrl)
                    End If
                    pnlLabOrdersList.Visible = True
                    pnlEncounterSnomed.Visible = False
                    pnlNutrition.Visible = False
                    pnlLabOrdersList.Dock = DockStyle.Fill
                    CurrentLab = SelectedLabType.LabTests

                Case SelectedActivity.Encounter
                    pnlLabOrdersList.Visible = False
                    pnlEncounterSnomed.Visible = True
                    pnlNutrition.Visible = False
                    If pnlControlContainer.Controls.Contains(_RxListUserCtrl) Then
                        pnlControlContainer.Controls.Remove(_RxListUserCtrl)
                    End If
                    pnlEncounterSnomed.Dock = DockStyle.Fill
                    gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                    If gstrSMDBConnstr <> String.Empty Then
                        objclsgeneral.IsConnect(gstrSMDBConnstr, GetConnectionString())
                    End If
                    txtSMSearch.Focus()
                Case SelectedActivity.Nutrition
                    pnlLabOrdersList.Visible = False
                    pnlEncounterSnomed.Visible = False
                    pnlNutrition.Visible = True
                    If pnlControlContainer.Controls.Contains(_RxListUserCtrl) Then
                        pnlControlContainer.Controls.Remove(_RxListUserCtrl)
                    End If
                    pnlNutrition.Dock = DockStyle.Fill
                    gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                    If gstrSMDBConnstr <> String.Empty Then
                        objNurtclsgeneral.IsConnect(gstrSMDBConnstr, GetConnectionString())
                    End If
                    txtSMSearch.Focus()
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Public Sub SetCurrentLab()
        Try
            pnl_btnRadiologyImaging.Visible = True
            pnl_btnGroups.Visible = True
            pnl_btnOthers.Visible = True
            pnl_btnRefTest.Visible = True
            pnlLabTests.Visible = True
            If Not IsNothing(oLabNode) And TestType <> "" Then
                Select Case TestType
                    Case "Radiology/Imaging"
                        enmselectedLab = SelectedLabType.Radiolody
                    Case "Referrals"
                        enmselectedLab = SelectedLabType.Referral
                    Case "Other"
                        enmselectedLab = SelectedLabType.Other
                    Case "Groups"
                        enmselectedLab = SelectedLabType.Group
                    Case "Lab Tests"
                        enmselectedLab = SelectedLabType.LabTests
                End Select
            End If

            Select Case enmselectedLab
                Case SelectedLabType.Radiolody
                    pnl_btnRadiologyImaging.Visible = False
                    btnTests.Text = "Radiology/Imaging"
                    FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging)
                Case SelectedLabType.Referral
                    pnl_btnRefTest.Visible = False
                    btnTests.Text = "Referrals"
                    FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.Referrals)
                Case SelectedLabType.Other
                    pnl_btnOthers.Visible = False
                    btnTests.Text = "Other"
                    FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.Other)
                Case SelectedLabType.Group
                    pnl_btnGroups.Visible = False
                    btnTests.Text = "Groups"
                    FillGroups_NEW()
                Case SelectedLabType.LabTests
                    pnlLabTests.Visible = False
                    btnTests.Text = "Lab Tests"
                    FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.LabTests)
            End Select

            If Not IsNothing(oLabNode) And TestType <> "" Then
                GloUC_trvTest.txtsearch.Text = SearchString
                GloUC_trvTest.SelectedNode = oLabNode
                GloUC_trvTest.txtsearch.Focus()
                AddTestToTransactionGrid(DirectCast(oLabNode, gloUserControlLibrary.myTreeNode))
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub LoadTreatmentPlan()

        Dim oTreatment As New clsTreatmentPlan()
        Dim dtPOT As DataTable = oTreatment.GetTreatmentPlans(PlanOfTreatmentId, PatientID)
        Dim dtExam As DataTable
        Try

            If Not IsNothing(dtPOT.Rows.Count) AndAlso dtPOT.Rows.Count > 0 Then

                txtPlantitle.Text = Convert.ToString(dtPOT.Rows(0)("sTitle"))
                txtPlanAssesment.Text = Convert.ToString(dtPOT.Rows(0)("sAssesment"))
                dtpPlanEffectiveFrom.Text = Convert.ToString(dtPOT.Rows(0)("dtEffectiveStartDate"))
                dtpPlanEffectiveTo.Text = Convert.ToString(dtPOT.Rows(0)("dtEffectiveEndDate"))
                ExamID = Convert.ToInt64(dtPOT.Rows(0)("ExamID"))
                dtExam = GetExamDetails(ExamID)
                If Not IsNothing(dtExam) Then
                    If dtExam.Rows.Count > 0 Then
                        lblExamName.Text = Convert.ToDateTime(dtExam.Rows(0)("DOS")).ToString("MM/dd/yyyy") & " - " & Convert.ToString(dtExam.Rows(0)("Exam Name"))
                    End If
                End If
                If Convert.ToString(dtPOT.Rows(0)("Status")) = "Active" Then
                    PlanStatus = True
                Else
                    PlanStatus = False
                End If
                PlanDeleteStatus = Convert.ToBoolean(dtPOT.Rows(0)("bIsDeleted"))

                If c1PlanActivity IsNot Nothing Then
                    For i As Integer = 0 To dtPOT.Rows.Count - 1 Step 1
                        If Convert.ToString(dtPOT.Rows(i)("PoTDetailID")) <> "" Then
                            c1PlanActivity.Rows.Add()
                            If Convert.ToString(dtPOT.Rows(i)("ActivityCodeSystem")) = "2.16.840.1.113883.6.88" And Convert.ToString(dtPOT.Rows(i)("ActivityType")) = "Planned Medication" Then
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Medication")
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.88")
                            ElseIf Convert.ToString(dtPOT.Rows(i)("ActivityCodeSystem")) = "2.16.840.1.113883.6.1" And Convert.ToString(dtPOT.Rows(i)("ActivityType")) = "Planned Lab Orders" Then
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Lab Orders")
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.1")
                            ElseIf Convert.ToString(dtPOT.Rows(i)("ActivityCodeSystem")) = "2.16.840.1.113883.6.96" And Convert.ToString(dtPOT.Rows(i)("ActivityType")) = "Planned Encounter" Then
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Encounter")
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.96")
                            ElseIf Convert.ToString(dtPOT.Rows(i)("ActivityCodeSystem")) = "2.16.840.1.113883.6.96" And Convert.ToString(dtPOT.Rows(i)("ActivityType")) = "Nutrition Recommendation" Then
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Nutrition Recommendation")
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.96")
                            End If

                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, Convert.ToString(dtPOT.Rows(i)("ActivityDetails")))
                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, Convert.ToString(dtPOT.Rows(i)("ActivityTreatmentName")))
                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, Convert.ToString(dtPOT.Rows(i)("ActivityCode")))
                            '' c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, Convert.ToString(dtPOT.Rows(i)("ActivityFrom")))
                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, IIf(Convert.ToString(dtPOT.Rows(i)("ActivityFrom")) <> "", Convert.ToString(dtPOT.Rows(i)("ActivityFrom")), "-"))
                            'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, Convert.ToString(dtPOT.Rows(i)("ActivityTo")))
                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, IIf(Convert.ToString(dtPOT.Rows(i)("ActivityTo")) <> "", Convert.ToString(dtPOT.Rows(i)("ActivityTo")), "-"))
                            If Convert.ToString(dtPOT.Rows(i)("ActivityStatus")) <> "" Then
                                If Convert.ToInt16(dtPOT.Rows(i)("ActivityStatus")) = ActivityStatus.Active Then
                                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
                                ElseIf Convert.ToInt16(dtPOT.Rows(i)("ActivityStatus")) = ActivityStatus.InActive Then
                                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.InActive)
                                ElseIf Convert.ToInt16(dtPOT.Rows(i)("ActivityStatus")) = ActivityStatus.Complete Then
                                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Complete)
                                End If
                            Else
                                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
                            End If

                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, Convert.ToString(dtPOT.Rows(i)("ActivityReason")))
                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, Convert.ToString(dtPOT.Rows(i)("ActivityReasonCode")))
                            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, Convert.ToString(dtPOT.Rows(i)("ActivityTreatmentID")))
                        End If
                    Next

                    If c1PlanActivity.Rows.Count > 1 Then
                        c1PlanActivity.Select(1, col_ActivityTreatmentID, Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID)))
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub FillPOTMasterData()
        Dim Col_nPlanOfTreatmentID As New DataColumn("nPlanOfTreatmentID")
        Dim Col_nPatientID As New DataColumn("nPatientID")
        Dim Col_sTitle As New DataColumn("sTitle")
        Dim Col_sAssesment As New DataColumn("sAssesment")
        Dim Col_dtEffectiveStartDate As New DataColumn("dtEffectiveStartDate")
        Dim Col_dtEffectiveEndDate As New DataColumn("dtEffectiveEndDate")
        Dim Col_bIsActive As New DataColumn("bIsActive")
        Dim Col_bIsDeleted As New DataColumn("bIsDeleted")
        Dim Col_nLoginSessionID As New DataColumn("nLoginSessionID")
        Dim Col_dtCreatedOn As New DataColumn("dtCreatedOn")

        Try
            If dtPOTMST.Columns.Count < 1 Then
                dtPOTMST.Columns.Add(Col_nPlanOfTreatmentID)
                dtPOTMST.Columns.Add(Col_nPatientID)
                dtPOTMST.Columns.Add(Col_sTitle)
                dtPOTMST.Columns.Add(Col_sAssesment)
                dtPOTMST.Columns.Add(Col_dtEffectiveStartDate)
                dtPOTMST.Columns.Add(Col_dtEffectiveEndDate)
                dtPOTMST.Columns.Add(Col_bIsActive)
                dtPOTMST.Columns.Add(Col_bIsDeleted)
                dtPOTMST.Columns.Add(Col_nLoginSessionID)
                dtPOTMST.Columns.Add(Col_dtCreatedOn)
            End If


            dtPOTMST.Rows.Clear()
            dtPOTMST.Rows.Add()
            dtPOTMST.Rows(0)("nPlanOfTreatmentID") = PlanOfTreatmentId
            dtPOTMST.Rows(0)("nPatientID") = PatientID
            dtPOTMST.Rows(0)("sTitle") = txtPlantitle.Text
            dtPOTMST.Rows(0)("sAssesment") = txtPlanAssesment.Text
            dtPOTMST.Rows(0)("dtEffectiveStartDate") = dtpPlanEffectiveFrom.Value
            dtPOTMST.Rows(0)("dtEffectiveEndDate") = dtpPlanEffectiveTo.Value
            dtPOTMST.Rows(0)("bIsActive") = True
            dtPOTMST.Rows(0)("bIsDeleted") = False
            dtPOTMST.Rows(0)("nLoginSessionID") = 0
            dtPOTMST.Rows(0)("dtCreatedOn") = System.DateTime.Now()
            dtPOTMST.AcceptChanges()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub FillPOTDetailsData()
        Dim Col_sActivityType As New DataColumn("sActivityType")
        Dim Col_sActivityDetails As New DataColumn("sActivityDetails")
        Dim Col_sActivityTreatmentId As New DataColumn("sActivityTreatmentId")
        Dim Col_sActivityTreatmentName As New DataColumn("sActivityTreatmentName")
        Dim Col_sActivityCode As New DataColumn("sActivityCode")
        Dim Col_sActivityCodeSystem As New DataColumn("sActivityCodeSystem")
        Dim Col_sActivityEffectiveFrom As New DataColumn("sActivityEffectiveFrom")
        Dim Col_sActivityEffectiveTo As New DataColumn("sActivityEffectiveTo")
        Dim Col_sActivityStatus As New DataColumn("sActivityStatus")
        Dim Col_sActivityReason As New DataColumn("sActivityReason")
        Dim Col_sActivityReasonCode As New DataColumn("sActivityReasonCode")

        Try
            If dtPOTDTL.Columns.Count < 1 Then
                dtPOTDTL.Columns.Add(Col_sActivityType)
                dtPOTDTL.Columns.Add(Col_sActivityDetails)
                dtPOTDTL.Columns.Add(Col_sActivityTreatmentId)
                dtPOTDTL.Columns.Add(Col_sActivityTreatmentName)
                dtPOTDTL.Columns.Add(Col_sActivityCode)
                dtPOTDTL.Columns.Add(Col_sActivityCodeSystem)
                dtPOTDTL.Columns.Add(Col_sActivityEffectiveFrom)
                dtPOTDTL.Columns.Add(Col_sActivityEffectiveTo)
                dtPOTDTL.Columns.Add(Col_sActivityStatus)
                dtPOTDTL.Columns.Add(Col_sActivityReason)
                dtPOTDTL.Columns.Add(Col_sActivityReasonCode)
            End If

            dtPOTDTL.Rows.Clear()
            For i As Int16 = 1 To c1PlanActivity.Rows.Count - 1 Step 1
                dtPOTDTL.Rows.Add()
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityType") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityType))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityDetails") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityDetails))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityTreatmentId") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityTreatmentID))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityTreatmentName") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityTreatmentName))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityCode") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityCode))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityCodeSystem") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityCodeSystem))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityEffectiveFrom") = IIf(Convert.ToString(c1PlanActivity.GetData(i, col_ActivityFrom)) <> "-", Convert.ToString(c1PlanActivity.GetData(i, col_ActivityFrom)), DBNull.Value)
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityEffectiveTo") = IIf(Convert.ToString(c1PlanActivity.GetData(i, col_ActivityTo)) <> "-", Convert.ToString(c1PlanActivity.GetData(i, col_ActivityTo)), DBNull.Value)
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityStatus") = Convert.ToInt16(c1PlanActivity.GetData(i, col_ActivityStatus))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityReason") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityReason))
                dtPOTDTL.Rows(dtPOTDTL.Rows.Count - 1)("sActivityReasonCode") = Convert.ToString(c1PlanActivity.GetData(i, col_ActivityReasonCode))
                dtPOTDTL.AcceptChanges()
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Function ValidateValue() As Boolean
        If txtPlantitle.Text.Trim() = "" Then
            MessageBox.Show("Enter plan title.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPlantitle.Focus()
            Return False
        End If
        If DateTime.Compare(dtpPlanEffectiveTo.Value, dtpPlanEffectiveFrom.Value) <> 0 Then
            If DateTime.Compare(dtpPlanEffectiveTo.Value, dtpPlanEffectiveFrom.Value) < 0 Then
                MessageBox.Show("Please check treatment plan effective dates.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpPlanEffectiveTo.Focus()
                Return False
            End If
        End If
        If CallingForm <> "Exam" Then
            If c1PlanActivity.Rows.Count <= 1 Then
                MessageBox.Show("Select atleast one Planned activity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub SetButtonBackgroundImage(nBackgroundID As Integer)
        'nBackgroundID=0: blue image
        'nBackgroundID=1: orange image
        If nBackgroundID = 0 Then
            Me.btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
            Me.btnMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.btnMedication.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnMedication.ForeColor = Color.White

            Me.btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
            Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnLabs.ForeColor = Color.White


            Me.btnEncounter.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
            Me.btnEncounter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.btnEncounter.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnEncounter.ForeColor = Color.White


            Me.btnNutrition.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
            Me.btnNutrition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.btnNutrition.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnNutrition.ForeColor = Color.White

        End If
        Select Case enmselectedbutton
            Case SelectedActivity.Medication
                Me.btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                Me.btnMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                Me.btnMedication.Dock = System.Windows.Forms.DockStyle.Fill
                Me.btnMedication.ForeColor = Color.Black

            Case SelectedActivity.LabOrder
                Me.btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
                Me.btnLabs.ForeColor = Color.Black

            Case SelectedActivity.Encounter
                Me.btnEncounter.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                Me.btnEncounter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                Me.btnEncounter.Dock = System.Windows.Forms.DockStyle.Fill
                Me.btnEncounter.ForeColor = Color.Black

            Case SelectedActivity.Nutrition
                Me.btnNutrition.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                Me.btnNutrition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                Me.btnNutrition.Dock = System.Windows.Forms.DockStyle.Fill
                Me.btnNutrition.ForeColor = Color.Black
        End Select

    End Sub

    Private Sub AddNodeToTransactionGrid(myNode As gloSnoMed.myTreeNode)
        RemoveHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
        Try
            Dim sActivityType As String = Nothing
            Select Case enmselectedbutton
                Case SelectedActivity.Encounter
                    sActivityType = "Planned Encounter"
                Case SelectedActivity.Nutrition
                    sActivityType = "Nutrition Recommendation"
            End Select

            If (myNode IsNot Nothing) Then
                Dim TestName As String = Convert.ToString(myNode.ConceptID)
                ''Dim DrugId As Long = Convert.ToInt64(Row(0))
                pnlActivityDetail.Visible = True
                '' txtSelectedCode.Text = Convert.ToString(DrugId) & " - " & DrugName
                txtSelectedCode.Text = TestName
                c1PlanActivity.Rows.Add()
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, sActivityType)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, Convert.ToString(myNode.Text))
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, TestName)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.96")
                'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, System.DateTime.Now.Date)
                'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, System.DateTime.Now.Date)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, "-")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, "-")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, TestName)
                Dim _Isgroup As Boolean = False
                If (myNode.Parent Is Nothing) Then
                    _Isgroup = True
                End If
                c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
                c1PlanActivity_SelChange(Nothing, Nothing)
                cmbProblemList.Items.Clear()
                cmbProblemList.Text = ""
            End If
            AddHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub AddNodeToTransactionGrid(treeNode As TreeNode)
        RemoveHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
        Try
            Dim sActivityType As String = Nothing
            Select Case enmselectedbutton
                Case SelectedActivity.Encounter
                    sActivityType = "Planned Encounter"
                Case SelectedActivity.Nutrition
                    sActivityType = "Nutrition Recommendation"
            End Select

            If (treeNode IsNot Nothing) Then
                Dim TestName As String = Convert.ToString(treeNode.Tag)
                ''Dim DrugId As Long = Convert.ToInt64(Row(0))
                pnlActivityDetail.Visible = True
                '' txtSelectedCode.Text = Convert.ToString(DrugId) & " - " & DrugName
                txtSelectedCode.Text = "" 'TestName
                c1PlanActivity.Rows.Add()
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, sActivityType)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, Convert.ToString(treeNode.Text))
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, TestName)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.96")
                'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, System.DateTime.Now.Date)
                'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, System.DateTime.Now.Date)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, "-")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, "-")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
                c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, TestName)
                Dim _Isgroup As Boolean = False
                If (treeNode.Parent Is Nothing) Then
                    _Isgroup = True
                End If
                c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
                c1PlanActivity_SelChange(Nothing, Nothing)
                cmbProblemList.Items.Clear()
                cmbProblemList.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        AddHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
    End Sub

#End Region

#Region "Medication List Control"

    Private Sub _RxListUserCtrl_DrugListClicked(Row As DataRow, DrugRoutes As List(Of String))
        Try
            RemoveHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
            Dim DrugName As String = Convert.ToString(Row(1))
            Dim DrugId As Long = Convert.ToInt64(Row(0))
            pnlActivityDetail.Visible = True
            txtSelectedCode.Text = DrugName
            c1PlanActivity.Rows.Add()
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Medication")
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, DrugName)
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, Convert.ToString(Row(6)))
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.88")
            'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, System.DateTime.Now.Date)
            'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, System.DateTime.Now.Date)
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, "-")
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, "-")
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, DrugId)

            'c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, DrugId)
            c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
            c1PlanActivity_SelChange(Nothing, Nothing)
            cmbProblemList.Items.Clear()
            cmbProblemList.Text = ""
        Catch ex As gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        AddHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
    End Sub

#End Region

#Region "Lab List Control"

    Private Sub GloUC_trvTest_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles GloUC_trvTest.NodeMouseDoubleClick
        Try
            AddTestToTransactionGrid(DirectCast(e.Node, gloUserControlLibrary.myTreeNode))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub chkIncludeTestCode_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeTestCode.CheckedChanged

        Select Case enmselectedLab
            Case SelectedLabType.Radiolody
                pnl_btnRadiologyImaging.Visible = False
                btnTests.Text = "Radiology/Imaging"
                FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.RadiologyImaging)
            Case SelectedLabType.Referral
                pnl_btnRefTest.Visible = False
                btnTests.Text = "Referrals"
                FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.Referrals)
            Case SelectedLabType.Other
                pnl_btnOthers.Visible = False
                btnTests.Text = "Other"
                FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.Other)
            Case SelectedLabType.Group
                pnl_btnRefTest.Visible = False
                btnTests.Text = "Groups"
                FillGroups_NEW()
            Case SelectedLabType.LabTests
                pnlLabTests.Visible = False
                btnTests.Text = "Lab Tests"
                FillTestsByType(gloEMRLab.gloEMRLabTest.OrderTestType.LabTests)
        End Select

    End Sub


    Private Sub AddTestToTransactionGrid(mynode As gloUserControlLibrary.myTreeNode)
        RemoveHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
        Try

            If (mynode IsNot Nothing) Then
                If mynode.Level = 0 AndAlso mynode.Nodes.Count > 0 Then
                    For Each cNode As gloUserControlLibrary.myTreeNode In mynode.Nodes
                        Dim TestName As String = Convert.ToString(cNode.Code)
                        ''Dim DrugId As Long = Convert.ToInt64(Row(0))
                        pnlActivityDetail.Visible = True
                        '' txtSelectedCode.Text = Convert.ToString(DrugId) & " - " & DrugName
                        txtSelectedCode.Text = TestName
                        c1PlanActivity.Rows.Add()
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Lab Orders")
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, TestName)
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, Convert.ToString(cNode.Tag))
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.1")
                        'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, System.DateTime.Now.Date)
                        'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, System.DateTime.Now.Date)
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, "-")
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, "-")
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
                        c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, Convert.ToInt64(cNode.ID))
                        c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
                        c1PlanActivity_SelChange(Nothing, Nothing)
                        Dim _Isgroup As Boolean = False
                        If (mynode.Parent Is Nothing) Then
                            _Isgroup = True
                        End If
                        cmbProblemList.Items.Clear()
                        cmbProblemList.Text = ""
                    Next
                Else
                    Dim TestName As String = Convert.ToString(mynode.Code)
                    ''Dim DrugId As Long = Convert.ToInt64(Row(0))
                    pnlActivityDetail.Visible = True
                    '' txtSelectedCode.Text = Convert.ToString(DrugId) & " - " & DrugName
                    txtSelectedCode.Text = TestName
                    c1PlanActivity.Rows.Add()
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Lab Orders")
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, TestName)
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, Convert.ToString(mynode.Tag))
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.1")
                    'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, System.DateTime.Now.Date)
                    'c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, System.DateTime.Now.Date)
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, "-")
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, "-")
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
                    c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, Convert.ToInt64(mynode.ID))
                    c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
                    c1PlanActivity_SelChange(Nothing, Nothing)
                    Dim _Isgroup As Boolean = False
                    If (mynode.Parent Is Nothing) Then
                        _Isgroup = True
                    End If
                    cmbProblemList.Items.Clear()
                    cmbProblemList.Text = ""
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        AddHandler c1PlanActivity.SelChange, AddressOf c1PlanActivity_SelChange
    End Sub

    Public Sub FillTestsByType(TestType As gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest.OrderTestType, Optional PreferredID As Int64 = 0)
        Try
            Dim oLabTests As gloEMRGeneralLibrary.gloEMRActors.LabActor.Tests = Nothing
            Dim oTest As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest()
            Dim oLabOrderRequest As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder()
            Dim oLabActor_Order As gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder = Nothing
            Dim dtTests As New DataTable()

            dtTests = GetTestStructure()


            If oLabTests Is Nothing Then
                If PreferredID <> 0 Then
                    oLabTests = oTest.GetTestsByType(TestType, PreferredID)
                Else
                    oLabTests = oTest.GetTestsByType(TestType)
                End If
            End If


            If oLabTests IsNot Nothing Then
                If oLabTests.Count > 0 Then
                    Dim row As DataRow = Nothing
                    For i As Integer = 0 To oLabTests.Count - 1
                        Dim oLabTest As gloEMRGeneralLibrary.gloEMRActors.LabActor.Test = oLabTests(i)
                        If oLabTest IsNot Nothing Then
                            row = dtTests.NewRow()

                            row("TestName") = oLabTest.Name
                            row("TestID") = oLabTest.TestID
                            row("TestCodeTestName") = oLabTest.Code + " - " + oLabTest.Name
                            row("LONICCode") = oLabTest.LOINCId
                            dtTests.Rows.Add(row)
                        End If
                    Next
                End If
            End If

            GloUC_trvTest.ParentMember = Nothing
            If (dtTests IsNot Nothing) Then

                GloUC_trvTest.ImageIndex = 0
                GloUC_trvTest.SelectedImageIndex = 0
                GloUC_trvTest.DataSource = dtTests
                GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvTest.CodeMember = "TestName"
                GloUC_trvTest.ValueMember = "TestID"
                GloUC_trvTest.Tag = "LONICCode"
                If chkIncludeTestCode.Checked = False Then
                    GloUC_trvTest.DescriptionMember = "TestName"
                Else
                    GloUC_trvTest.DescriptionMember = "TestCodeTestName"
                End If

                GloUC_trvTest.FillTreeView()
            End If


            If oLabTests IsNot Nothing Then
                oLabTests.Dispose()
                oLabTests = Nothing
            End If
            oTest.Dispose()
            oTest = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function GetTestStructure() As DataTable
        Dim dtTestStructure As New DataTable()
        Dim Col2 As New DataColumn("TestID")
        Col2.DataType = System.Type.[GetType]("System.Decimal")
        dtTestStructure.Columns.Add(Col2)

        Dim Col3 As New DataColumn("TestName")
        Col3.DataType = System.Type.[GetType]("System.String")
        dtTestStructure.Columns.Add(Col3)

        Dim Col4 As New DataColumn("TestCodeTestName")
        Col4.DataType = System.Type.[GetType]("System.String")
        dtTestStructure.Columns.Add(Col4)

        Dim Col5 As New DataColumn("LONICCode")
        Col5.DataType = System.Type.[GetType]("System.String")
        dtTestStructure.Columns.Add(Col5)

        Return dtTestStructure
    End Function

    Private Function GetProblemDescription(ByVal Id As Int64) As String

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim oResult As Object = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = " SELECT pl.sCheifComplaint FROM  dbo.ProblemList pl WHERE pl.nProblemID=" & Id
            objCmd.Connection = objCon
            oResult = objCmd.ExecuteScalar()
            Return Convert.ToString(oResult)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            oResult = 0
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return Convert.ToString(oResult)
    End Function

    Private Sub FillGroups_NEW()

        Dim dt As New DataTable()
        Dim Col0 As New DataColumn("GroupID")
        Col0.DataType = System.Type.[GetType]("System.Decimal")
        dt.Columns.Add(Col0)
        Dim Col1 As New DataColumn("GroupName")
        Col1.DataType = System.Type.[GetType]("System.String")
        dt.Columns.Add(Col1)
        Dim Col2 As New DataColumn("TestID")
        Col2.DataType = System.Type.[GetType]("System.Decimal")
        dt.Columns.Add(Col2)
        Dim Col3 As New DataColumn("TestName")
        Col3.DataType = System.Type.[GetType]("System.String")
        dt.Columns.Add(Col3)
        Dim Col4 As New DataColumn("TestCodeTestName")
        Col4.DataType = System.Type.[GetType]("System.String")
        dt.Columns.Add(Col4)
        Dim Col5 As New DataColumn("LONICCode")
        Col5.DataType = System.Type.[GetType]("System.String")
        dt.Columns.Add(Col5)


        Try
            Dim oGroup As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabGroup()
            Dim oLabGroup As New gloEMRGeneralLibrary.gloEMRActors.LabActor.LabGroups()
            Dim oLabGroups As New gloEMRGeneralLibrary.gloEMRActors.LabActor.LabGroups()
            Dim oLabOrderRequest As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder()
            Dim oLabActor_Order As gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder = Nothing


            oLabGroups = oGroup.GetGroups()

            Dim row As DataRow = Nothing
            If oLabGroups.Count > 0 Then
                For i As Integer = 0 To oLabGroups.Count - 1
                    Dim _groupName As String = Nothing
                    Dim _groupID As Int64 = Nothing

                    _groupName = oLabGroups.Item(i).LabGroupName

                    _groupID = oLabGroups.Item(i).LabGroupID


                    For j As Integer = 0 To oLabGroups.Item(i).Tests.Count - 1
                        row = dt.NewRow()
                        row("GroupName") = _groupName
                        row("GroupID") = _groupID
                        row("TestName") = oLabGroups.Item(i).Tests.Item(j).Name
                        row("TestID") = oLabGroups.Item(i).Tests.Item(j).TestID
                        row("TestCodeTestName") = oLabGroups.Item(i).Tests.Item(j).Code + " - " + oLabGroups.Item(i).Tests.Item(j).Name
                        row("LONICCode") = oLabGroups.Item(i).Tests(j).LOINCId
                        dt.Rows.Add(row)
                    Next
                Next
            End If
            If (dt IsNot Nothing) Then
                GloUC_trvTest.ImageIndex = 0
                GloUC_trvTest.SelectedImageIndex = 0
                GloUC_trvTest.ParentImageIndex = 1
                GloUC_trvTest.SelectedParentImageIndex = 1
                GloUC_trvTest.DataSource = dt
                GloUC_trvTest.ParentMember = "GroupName"
                GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

                GloUC_trvTest.CodeMember = "TestName"
                GloUC_trvTest.ValueMember = "TestID"
                GloUC_trvTest.Tag = "LONICCode"
                If chkIncludeTestCode.Checked = False Then
                    GloUC_trvTest.DescriptionMember = "TestName"
                Else
                    GloUC_trvTest.DescriptionMember = "TestCodeTestName"
                End If

                GloUC_trvTest.FillTreeView()
            End If

            oLabGroups.Dispose()
            oLabGroups = Nothing
            oGroup.Dispose()
            oGroup = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
#End Region

#Region "Encounter Snomed Selector"
    Private objclsgeneral As gloSnoMed.ClsGeneral = New gloSnoMed.ClsGeneral()
    Private _isFormLoading As Boolean = False
    Private _SearchBy As String = "ConceptID"
    Public strConceptID As String = ""
    Public strProblem As String = ""
    Public strSelectedConceptID As String = ""
    Public strSelectedDescriptionID As String = ""
    Public strSelectedDescription As String = ""
    Public strConceptDesc As String = ""
    Public strCodeSystem As String = "SNOMED"
    Dim _CurrentTime As System.DateTime
    Private Sub FillDefination()
        Dim oNode As TreeNode = Nothing
        Dim dsSnomed As New DataSet()
        'trvSubtype.Nodes.Clear()
        'trvSnoMed.Nodes.Clear()

        oNode = trvFindings.SelectedNode
        If (oNode IsNot Nothing) Then
            If (oNode.Tag IsNot Nothing) Then
                strConceptID = oNode.Tag.ToString()
                Dim ICD9Description As String = ""

                If String.IsNullOrEmpty(oNode.Name) Then
                    oNode.Name = oNode.Text
                End If
                ICD9Description = oNode.Name.Trim()
                oNode = Nothing
                dsSnomed = objclsgeneral.Fill_SnomedDetails(strConceptID, "False", ICD9Description, True)

                'If (dsSnomed Is Nothing) = False Then



                '    objclsgeneral.Fill_snomedDescription(strConceptID, trvSnoMed, dsSnomed)

                '    If dsSnomed.Tables("IsDefinition").Rows.Count > 0 Then
                '        strSelectedDescription = dsSnomed.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME").ToString()



                '    End If
                'End If
                If dsSnomed IsNot Nothing Then
                    dsSnomed.Dispose()
                    dsSnomed = Nothing

                End If
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Cursor = Cursors.WaitCursor

        Try

            If Not String.IsNullOrEmpty(Me.Text.Trim()) Then
                If DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100 Then
                    Timer1.[Stop]()


                    mnuFindings_Click(Nothing, Nothing)
                    'If pnlmiddle.Visible Then
                    '    FillDefination()

                    'End If
                    FillDefination()
                End If
            Else
                Timer1.[Stop]()

                mnuFindings_Click(Nothing, Nothing)
                'If pnlmiddle.Visible Then
                '    FillDefination()

                'End If
                FillDefination()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub trvFindings_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvFindings.AfterSelect
        If Timer1.Enabled = False Then
            Timer1.[Stop]()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub trvFindings_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles trvFindings.BeforeSelect
        _CurrentTime = DateTime.Now
        Timer1.[Stop]()
        Timer1.Interval = 700
        Timer1.Enabled = True
    End Sub

    Private Sub trvFindings_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles trvFindings.BeforeExpand
        Try
            Me.Cursor = Cursors.WaitCursor

            If (e.Node Is Nothing) = False Then

                ' TreeNode eNode = new TreeNode();
                Dim eNode As TreeNode = e.Node
                Dim dsTreeview As DataSet = Nothing
                ' new DataSet();
                If (eNode IsNot Nothing) Then
                    If (e.Node.Parent Is Nothing) = False Then
                        If e.Node.Nodes(0).Tag.ToString() = "TempNode999*" Then
                            trvFindings.Nodes.Remove(e.Node.Nodes(0))
                            If chkCOREProblem.Checked = False Then
                                'If RbICD9.Checked OrElse RbICD10.Checked Then
                                '    If e.Node.Parent.Nodes(0).Tag.ToString() IsNot Nothing Then
                                '        ' dsTreeview = objclsgeneral.Fill_SubTypes(e.Node.Parent.Nodes[0].Tag .ToString(), false);
                                '        Dim ICD9Desc As String = objclsgeneral.Fill_ICD9(e.Node.Parent.Text.ToString())

                                '        'Pass rootnode conceptid while expanding child nodes  ''e.Node.Parent.Nodes[0].Tag
                                '        dsTreeview = objclsgeneral.Fill_TreeOnExpand_ICD(e.Node.Parent.Nodes(0).Tag.ToString(), _SearchBy, ICD9Desc)


                                '        objclsgeneral.FillSubtypeHierarchy_New(e.Node.Parent.Nodes(0).Tag.ToString(), dsTreeview, eNode)
                                '    End If
                                'Else
                                '    dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), False)
                                '    objclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode)
                                'End If
                                dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), False)
                                objclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode)

                            End If
                        End If
                    ElseIf e.Node.Nodes(0).Tag.ToString() = "TempNode9999*" Then

                        dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Tag.ToString(), True)
                        objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview, chkCOREProblem.Checked)
                        If eNode.Tag IsNot Nothing Then
                            strProblem = eNode.Name
                            strSelectedConceptID = eNode.Tag.ToString()

                            If (dsTreeview Is Nothing) = False Then
                                If dsTreeview.Tables("Parent").Rows.Count > 0 Then
                                    'strDescriptionID = dsTreeview.Tables("Parent").Rows(0)("DESCRIPTIONID").ToString()
                                    'StrSnoMedID = dsTreeview.Tables("Parent").Rows(0)("SNOMEDID").ToString()
                                End If
                                'If dsTreeview.Tables("IsDefinition").Rows.Count > 0 Then
                                '    strSelectedDescription = dsTreeview.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME").ToString()
                                'End If
                            End If
                            '   string ICD9Desc = "";
                            If Not String.IsNullOrEmpty(eNode.Name.Trim()) Then
                                'ICD9Desc = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim());
                                'lblConceptID.Text = eNode.Tag + " - " + eNode.Name.Trim()
                            Else
                                ' lblConceptID.Text = eNode.Tag.ToString()
                            End If
                        End If


                        'lblSnoMedID.Text = strSelectedSnoMedID

                        'lblDescriptionID.Text = strSelectedDescriptionID
                    ElseIf e.Node.Nodes(0).Tag.ToString() = "ICDTempNode9999*" Then
                        'trvFindings.Nodes.Remove(e.Node.Nodes(0))
                        'Dim ICD9Desc As [String] = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim())
                        'Dim ICDCode As String = objclsgeneral.Fill_ICD9(eNode.Name.Trim())
                        'If chkCOREProblem.Checked Then
                        '    dsTreeview = objclsgeneral.Fill_CORESearchICD(ICD9Desc, _SearchBy, ICDCode)
                        'Else
                        '    dsTreeview = objclsgeneral.Fill_SnomedDetailsByConceptID(ICD9Desc, _SearchBy, ICDCode)
                        'End If



                        'If dsTreeview IsNot Nothing Then
                        '    If dsTreeview.Tables.Count > 0 Then
                        '        ' objclsgeneral.FillSubtypeHierarchy_ByICD9(dsTreeview.Tables[1], eNode);
                        '        dsTreeview.Tables(1).TableName = "Parent"
                        '        objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview, chkCOREProblem.Checked)
                        '    End If
                        'End If



                    End If
                End If


                If dsTreeview IsNot Nothing Then
                    dsTreeview.Dispose()
                    dsTreeview = Nothing
                End If

                If (eNode IsNot Nothing) Then
                    eNode = Nothing
                End If
            End If
        Catch
            ' (Exception ex)
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub trvFindings_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFindings.NodeMouseDoubleClick
        Try

            Dim obj As Object
            Try
                obj = DirectCast(e.Node, gloSnoMed.myTreeNode)
            Catch ex As Exception
                obj = Nothing
            End Try


            If Not IsNothing(obj) Then
                AddNodeToTransactionGrid(DirectCast(e.Node, gloSnoMed.myTreeNode))
            Else
                AddNodeToTransactionGrid(e.Node)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtSMSearch_SearchFired() Handles txtSMSearch.SearchFired
        Dim _Term As String = Nothing

        Try

            If txtSMSearch.Text.Length > 1 Then
                trvFindings.BeginUpdate()
                Me.Cursor = Cursors.WaitCursor
                _Term = ""
                If chkCOREProblem.Checked Then


                    objclsgeneral.SearchCORESnomed(txtSMSearch.Text.Trim(), trvFindings, _SearchBy)
                Else


                    objclsgeneral.SearchSnomed(txtSMSearch.Text.Trim(), False, trvFindings, _SearchBy)
                End If

                _Term = strConceptDesc

                If chkCOREProblem.Checked Then

                    If trvFindings.Nodes.Count > 0 Then

                        If trvFindings.Nodes(0).Nodes.Count > 0 Then
                            If trvFindings.Nodes.Count = 1 Then
                                Dim oNode__1 As TreeNode = trvFindings.Nodes(0)
                                trvFindings.SelectedNode = oNode__1
                                trvFindings.Nodes(0).ExpandAll()
                                For Each onode__2 As TreeNode In trvFindings.Nodes(0).Nodes
                                    If (onode__2 Is Nothing) = False Then

                                        If Convert.ToString(onode__2.Tag) = strConceptID Then
                                            If Convert.ToString(onode__2.Tag) = strConceptID And onode__2.Text.Trim() = _Term.Trim() Then
                                                trvFindings.SelectedNode = onode__2
                                                ' TODO: might not be correct. Was : Exit For
                                                Exit For
                                            End If
                                        End If

                                    End If
                                Next

                            End If

                        End If
                    End If
                Else

                    For Each onode__2 As TreeNode In trvFindings.Nodes
                        If (onode__2 Is Nothing) = False Then
                            If Convert.ToString(onode__2.Tag) = strConceptID Then
                                If Convert.ToString(onode__2.Tag) = strConceptID And onode__2.Name.Trim() = _Term.Trim() Then
                                    trvFindings.SelectedNode = onode__2
                                    ' TODO: might not be correct. Was : Exit For
                                    Exit For
                                End If
                            End If

                        End If
                    Next
                End If
                ' }
                Me.Cursor = Cursors.[Default]


                trvFindings.EndUpdate()
            Else
                trvFindings.Nodes.Clear()
                'trvSubtype.Nodes.Clear()
                'trvSnoMed.Nodes.Clear()
                'trICD9.Nodes.Clear()
                'trICD10.Nodes.Clear()

                'Sanjog
                'lblConceptID.Text = ""
                'lblSnoMedID.Text = ""
                'lblDescriptionID.Text = ""
                'strSelectedSnoMedID = ""
                ''Sanjog
                'strSelectedDescriptionID = ""
            End If
        Catch
            '(Exception ex)
        Finally

            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

#Region "CORE Problem"
    Private Sub chkCOREProblem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCOREProblem.CheckedChanged

        If _isFormLoading = False Then
            ClearData()

            txtSMSearch_SearchFired()
        End If

    End Sub
#End Region

    Private Sub ClearData()
        trvFindings.Nodes.Clear()
        txtSMSearch.Clear()
    End Sub

    Private Sub mnuFindings_Click(sender As Object, e As EventArgs)
        Dim oNode As TreeNode = Nothing
        Try

            'trICD9.Nodes.Clear()
            'trICD10.Nodes.Clear()

            'lblConceptID.Text = ""
            'lblSnoMedID.Text = ""
            'lblDescriptionID.Text = ""
            'strSelectedSnoMedID = ""
            strSelectedDescriptionID = ""
            oNode = trvFindings.SelectedNode
            If (oNode IsNot Nothing) Then
                If (oNode.Tag IsNot Nothing) Then
                    strConceptID = oNode.Tag.ToString()
                    Dim ICD9Description As String = ""

                    If String.IsNullOrEmpty(oNode.Name) Then
                        oNode.Name = oNode.Text
                    End If
                    ICD9Description = oNode.Name.Trim()
                    If chkCOREProblem.Checked Then
                        Dim dsICD As DataSet = Nothing
                        If oNode.Parent IsNot Nothing Then
                            Dim ICDCode As [String] = objclsgeneral.Fill_ICD9(oNode.Parent.Text)
                            'If RbICD9.Checked Then
                            '    dsICD = objclsgeneral.GetCOREICDData(strConceptID, ICDCode, "ICD9")
                            'Else
                            '    dsICD = objclsgeneral.GetCOREICDData(strConceptID, ICDCode, "ICD10")



                            'End If
                        Else
                            ' String ICDCode = objclsgeneral.Fill_ICD9(oNode.Parent.Text);
                            dsICD = objclsgeneral.GetCOREICDData(strConceptID, "")
                        End If
                        'If dsICD IsNot Nothing Then
                        '    objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsICD, trICD10)
                        '    objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsICD, trICD9)
                        '    If dsICD.Tables("RxNormNDC").Rows.Count > 0 Then
                        '        strNDCCode = dsICD.Tables("RxNormNDC").Rows(0)("NDCCode").ToString()
                        '        strRxNormCode = dsICD.Tables("RxNormNDC").Rows(0)("RxNorm").ToString()
                        '    Else
                        '        strNDCCode = ""
                        '        strRxNormCode = ""
                        '    End If
                        '    dsICD.Dispose()
                        '    dsICD = Nothing
                        'Else
                        '    strNDCCode = ""

                        '    strRxNormCode = ""
                        'End If
                    Else
                        Dim dsSnomed As DataSet = objclsgeneral.Fill_SnomedDetails(strConceptID, "False", ICD9Description, False)
                        'If dsSnomed IsNot Nothing Then
                        'objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsSnomed, trICD10)
                        'objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsSnomed, trICD9)
                        '    If dsSnomed.Tables("RxNormNDC").Rows.Count > 0 Then
                        '        strNDCCode = dsSnomed.Tables("RxNormNDC").Rows(0)("NDCCode").ToString()
                        '        strRxNormCode = dsSnomed.Tables("RxNormNDC").Rows(0)("RxNorm").ToString()
                        '    Else
                        '        strNDCCode = ""
                        '        strRxNormCode = ""
                        '    End If
                        '    dsSnomed.Dispose()
                        '    dsSnomed = Nothing
                        'Else
                        '    strNDCCode = ""
                        '    strRxNormCode = ""

                        'End If
                    End If

                    strSelectedConceptID = strConceptID
                    If Not String.IsNullOrEmpty(oNode.Name) Then
                        'ICD9Desc = objclsgeneral.Fill_ICD9Description(oNode.Name);
                        'lblConceptID.Text = strSelectedConceptID + " - " + oNode.Name
                    Else
                        'lblConceptID.Text = strSelectedConceptID
                    End If



                    strProblem = oNode.Name
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If oNode IsNot Nothing Then
                oNode = Nothing
            End If
        End Try

    End Sub

    'Private Sub AddEncounterToTransactionGrid(mynode As gloSnoMed.myTreeNode)
    '    Try

    '        If (mynode IsNot Nothing) Then
    '            Dim TestName As String = Convert.ToString(mynode.ConceptID)
    '            ''Dim DrugId As Long = Convert.ToInt64(Row(0))
    '            pnlActivityDetail.Visible = True
    '            '' txtSelectedCode.Text = Convert.ToString(DrugId) & " - " & DrugName
    '            txtSelectedCode.Text = TestName
    '            c1PlanActivity.Rows.Add()
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Planned Encounter")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, Convert.ToString(mynode.Text))
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, TestName)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.96")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, dtpActivityEffectiveFrom.Value)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, dtpActivityEffectiveTo.Value)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, TestName)
    '            Dim _Isgroup As Boolean = False
    '            If (mynode.Parent Is Nothing) Then
    '                _Isgroup = True
    '            End If
    '            c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
    '            cmbProblemList.Items.Clear()
    '            cmbProblemList.Text = ""
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
    '    End Try
    'End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        ClearData()
    End Sub
#End Region

#Region "Problem Selector"
    Private oListControl As New gloListControl.gloListControl
    Private Sub btnGetPatientProblem_Click(sender As System.Object, e As System.EventArgs) Handles btnGetPatientProblem.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
                Try
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                Catch ex As Exception

                End Try

                oListControl.Dispose()
                oListControl = Nothing
            End If


            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.PatientProblemList, True, Me.Width)
            ''oListControl.ControlHeader = "Care Plan"
            oListControl.ControlHeader = "Patient Problem"
            oListControl.PatientID = PatientID
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            'If dtMasterData IsNot Nothing AndAlso dtMasterData.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtMasterData.Rows.Count - 1
            '        oListControl.SelectedItems.Add(Convert.ToInt64(dtMasterData.Rows(i)("Id")), "", dtMasterData.Rows(i)("Problem").ToString())
            '    Next
            'End If

            If cmbProblemList IsNot Nothing AndAlso cmbProblemList.Items.Count > 0 Then
                For i As Integer = 0 To cmbProblemList.Items.Count - 1
                    If cmbProblemList.Items IsNot Nothing Then
                        oListControl.SelectedItems.Add(Convert.ToInt64(DirectCast(cmbProblemList.Items(i), gloEMR.ComboboxItem).valueMember), "")
                    End If
                Next
            End If

            pnlActivityList.Controls.Add(oListControl)
            oListControl.OpenControl()
            pnlActivityDetail.Visible = False
            If oListControl.IsDisposed = False Then
                oListControl.Dock = DockStyle.Fill
                oListControl.BringToFront()
            End If
            Me.Cursor = Cursors.[Default]
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListControl_ItemClosedClick(sender As Object, e As EventArgs)
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = pnlActivityList.Controls.Count - 1 To 0 Step -1
                    If pnlActivityList.Controls(i).Name = oListControl.Name Then
                        pnlActivityList.Controls.Remove(pnlActivityList.Controls(i))
                        Exit For
                    End If
                Next
                Try
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                Catch ex As Exception

                End Try

                oListControl.Dispose()
                oListControl = Nothing
            End If

            pnlActivityDetail.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub oListControl_SelectedClick(sender As Object, e As EventArgs)
        Dim objCarePlan As New ClsCarePlanMST()
        Dim dtData As DataTable = Nothing
        Try

            If oListControl.SelectedItems.Count > 0 Then
                cmbProblemList.Items.Clear()
                cmbProblemList.Text = ""
                For i As Int16 = 0 To oListControl.SelectedItems.Count - 1

                    Dim oComboBoxItem As New ComboboxItem()
                    oComboBoxItem.displayMember = Convert.ToString(oListControl.SelectedItems(i).Description)
                    oComboBoxItem.valueMember = Convert.ToInt64(oListControl.SelectedItems(i).ID)
                    cmbProblemList.Items.Add(oComboBoxItem)
                    cmbProblemList.SelectedIndex = 0
                    ''End If
                    'If dtMasterData.Rows.Find(key) Is Nothing Then

                    '    Dim drTemp As DataRow = dtMasterData.NewRow()
                    '    drTemp("Id") = oListControl.SelectedItems(i).ID
                    '    drTemp("Problem") = oListControl.SelectedItems(i).Description
                    '    dtMasterData.Rows.Add(drTemp)

                    '    Dim nID As Long = Convert.ToInt64(key)
                    '    If nID > 0 Then
                    '        dtData = objCarePlan.GetCarePlanMST(nID)
                    '        If (Not IsNothing(dtData)) Then
                    '            If (dtData.Rows.Count > 0) Then
                    '                If txtProblem.Text.Trim() <> "" Then
                    '                    txtProblem.Text = txtProblem.Text + "," + dtData.Rows(0)("sProblem").ToString()
                    '                Else
                    '                    txtProblem.Text = dtData.Rows(0)("sProblem").ToString()
                    '                End If

                    '                If txtGoal.Text.Trim() <> "" Then
                    '                    txtGoal.Text = txtGoal.Text + Environment.NewLine + dtData.Rows(0)("sGoal").ToString()
                    '                Else
                    '                    txtGoal.Text = dtData.Rows(0)("sGoal").ToString()
                    '                End If

                    '                If txtNotes.Text.Trim() <> "" Then
                    '                    txtNotes.Text = txtNotes.Text + Environment.NewLine + dtData.Rows(0)("sNote").ToString()
                    '                Else
                    '                    txtNotes.Text = dtData.Rows(0)("sNote").ToString()
                    '                End If

                    '                If txtInstruction.Text.Trim() <> "" Then
                    '                    txtInstruction.Text = txtInstruction.Text + Environment.NewLine + dtData.Rows(0)("sInstruction").ToString()
                    '                Else
                    '                    txtInstruction.Text = dtData.Rows(0)("sInstruction").ToString()
                    '                End If

                    '            End If
                    '        End If
                    '    End If
                    'End If
                Next
            Else
                cmbProblemList.Items.Clear()
                cmbProblemList.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlActivityDetail.Visible = True
            If objCarePlan IsNot Nothing Then
                objCarePlan.Dispose()
                objCarePlan = Nothing
            End If
            If dtData IsNot Nothing Then
                dtData.Dispose()
                dtData = Nothing
            End If

        End Try
    End Sub
#End Region

#Region "Nutrition Snomed Selector"
    Private objNurtclsgeneral As gloSnoMed.ClsGeneral = New gloSnoMed.ClsGeneral()
    Private _NurtSearchBy As String = "ConceptID"
    Public strNurtConceptID As String = ""
    Public strNurtProblem As String = ""
    Public strNurtSelectedConceptID As String = ""
    Public strNurtSelectedDescriptionID As String = ""
    Public strNurtSelectedDescription As String = ""
    Public strNurtConceptDesc As String = ""
    Public strNurtCodeSystem As String = "SNOMED"
    Dim _NurtCurrentTime As System.DateTime
    Private Sub FillNurtDefination()
        Dim oNode As TreeNode = Nothing
        Dim dsSnomed As New DataSet()
        'trvSubtype.Nodes.Clear()
        'trvSnoMed.Nodes.Clear()

        oNode = trvNurtFindings.SelectedNode
        If (oNode IsNot Nothing) Then
            If (oNode.Tag IsNot Nothing) Then
                strNurtConceptID = oNode.Tag.ToString()
                Dim ICD9Description As String = ""

                If String.IsNullOrEmpty(oNode.Name) Then
                    oNode.Name = oNode.Text
                End If
                ICD9Description = oNode.Name.Trim()
                oNode = Nothing
                dsSnomed = objNurtclsgeneral.Fill_SnomedDetails(strNurtConceptID, "False", ICD9Description, True)

                'If (dsSnomed Is Nothing) = False Then



                '    objclsgeneral.Fill_snomedDescription(strConceptID, trvSnoMed, dsSnomed)

                '    If dsSnomed.Tables("IsDefinition").Rows.Count > 0 Then
                '        strSelectedDescription = dsSnomed.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME").ToString()



                '    End If
                'End If
                If dsSnomed IsNot Nothing Then
                    dsSnomed.Dispose()
                    dsSnomed = Nothing

                End If
            End If
        End If

    End Sub

    Private Sub TimerNurt_Tick(sender As Object, e As EventArgs) Handles TimerNurt.Tick
        Me.Cursor = Cursors.WaitCursor

        Try

            If Not String.IsNullOrEmpty(Me.Text.Trim()) Then
                If DateTime.Now.Subtract(_NurtCurrentTime).Milliseconds > 100 Then
                    TimerNurt.Stop()


                    mnuNurtFindings_Click(Nothing, Nothing)
                    'If pnlmiddle.Visible Then
                    '    FillDefination()

                    'End If
                    FillNurtDefination()
                End If
            Else
                TimerNurt.Stop()

                mnuNurtFindings_Click(Nothing, Nothing)
                'If pnlmiddle.Visible Then
                '    FillDefination()

                'End If
                FillNurtDefination()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub trvNurtFindings_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvNurtFindings.AfterSelect
        If TimerNurt.Enabled = False Then
            TimerNurt.[Stop]()
            TimerNurt.Enabled = True
        End If
    End Sub

    Private Sub trvNurtFindings_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles trvNurtFindings.BeforeSelect
        _NurtCurrentTime = DateTime.Now
        TimerNurt.[Stop]()
        TimerNurt.Interval = 700
        TimerNurt.Enabled = True
    End Sub

    Private Sub trvNurtFindings_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles trvNurtFindings.BeforeExpand
        Try
            Me.Cursor = Cursors.WaitCursor

            If (e.Node Is Nothing) = False Then

                ' TreeNode eNode = new TreeNode();
                Dim eNode As TreeNode = e.Node
                Dim dsTreeview As DataSet = Nothing
                ' new DataSet();
                If (eNode IsNot Nothing) Then
                    If (e.Node.Parent Is Nothing) = False Then
                        If e.Node.Nodes(0).Tag.ToString() = "TempNode999*" Then
                            trvNurtFindings.Nodes.Remove(e.Node.Nodes(0))
                            If chkNurtCOREProblem.Checked = False Then
                                'If RbICD9.Checked OrElse RbICD10.Checked Then
                                '    If e.Node.Parent.Nodes(0).Tag.ToString() IsNot Nothing Then
                                '        ' dsTreeview = objclsgeneral.Fill_SubTypes(e.Node.Parent.Nodes[0].Tag .ToString(), false);
                                '        Dim ICD9Desc As String = objclsgeneral.Fill_ICD9(e.Node.Parent.Text.ToString())

                                '        'Pass rootnode conceptid while expanding child nodes  ''e.Node.Parent.Nodes[0].Tag
                                '        dsTreeview = objclsgeneral.Fill_TreeOnExpand_ICD(e.Node.Parent.Nodes(0).Tag.ToString(), _SearchBy, ICD9Desc)


                                '        objclsgeneral.FillSubtypeHierarchy_New(e.Node.Parent.Nodes(0).Tag.ToString(), dsTreeview, eNode)
                                '    End If
                                'Else
                                '    dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), False)
                                '    objclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode)
                                'End If
                                dsTreeview = objNurtclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), False)
                                objNurtclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode)

                            End If
                        End If
                    ElseIf e.Node.Nodes(0).Tag.ToString() = "TempNode9999*" Then

                        dsTreeview = objNurtclsgeneral.Fill_SubTypes(eNode.Tag.ToString(), True)
                        objNurtclsgeneral.FillParentNodes(trvNurtFindings, eNode, dsTreeview, chkNurtCOREProblem.Checked)
                        If eNode.Tag IsNot Nothing Then
                            strNurtProblem = eNode.Name
                            strNurtSelectedConceptID = eNode.Tag.ToString()

                            If (dsTreeview Is Nothing) = False Then
                                If dsTreeview.Tables("Parent").Rows.Count > 0 Then
                                    'strDescriptionID = dsTreeview.Tables("Parent").Rows(0)("DESCRIPTIONID").ToString()
                                    'StrSnoMedID = dsTreeview.Tables("Parent").Rows(0)("SNOMEDID").ToString()
                                End If
                                'If dsTreeview.Tables("IsDefinition").Rows.Count > 0 Then
                                '    strNurtSelectedDescription = dsTreeview.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME").ToString()
                                'End If
                            End If
                            '   string ICD9Desc = "";
                            If Not String.IsNullOrEmpty(eNode.Name.Trim()) Then
                                'ICD9Desc = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim());
                                'lblConceptID.Text = eNode.Tag + " - " + eNode.Name.Trim()
                            Else
                                ' lblConceptID.Text = eNode.Tag.ToString()
                            End If
                        End If


                        'lblSnoMedID.Text = strSelectedSnoMedID

                        'lblDescriptionID.Text = strSelectedDescriptionID
                    ElseIf e.Node.Nodes(0).Tag.ToString() = "ICDTempNode9999*" Then
                        'trvFindings.Nodes.Remove(e.Node.Nodes(0))
                        'Dim ICD9Desc As [String] = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim())
                        'Dim ICDCode As String = objclsgeneral.Fill_ICD9(eNode.Name.Trim())
                        'If chkCOREProblem.Checked Then
                        '    dsTreeview = objclsgeneral.Fill_CORESearchICD(ICD9Desc, _SearchBy, ICDCode)
                        'Else
                        '    dsTreeview = objclsgeneral.Fill_SnomedDetailsByConceptID(ICD9Desc, _SearchBy, ICDCode)
                        'End If



                        'If dsTreeview IsNot Nothing Then
                        '    If dsTreeview.Tables.Count > 0 Then
                        '        ' objclsgeneral.FillSubtypeHierarchy_ByICD9(dsTreeview.Tables[1], eNode);
                        '        dsTreeview.Tables(1).TableName = "Parent"
                        '        objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview, chkCOREProblem.Checked)
                        '    End If
                        'End If



                    End If
                End If


                If dsTreeview IsNot Nothing Then
                    dsTreeview.Dispose()
                    dsTreeview = Nothing
                End If

                If (eNode IsNot Nothing) Then
                    eNode = Nothing
                End If
            End If
        Catch
            ' (Exception ex)
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub trvNurtFindings_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvNurtFindings.NodeMouseDoubleClick
        Try
            ''AddNurritionToTransactionGrid(DirectCast(e.Node, gloSnoMed.myTreeNode))
            Dim obj As Object
            Try
                obj = DirectCast(e.Node, gloSnoMed.myTreeNode)
            Catch ex As Exception
                obj = Nothing
            End Try

            If Not IsNothing(obj) Then
                AddNodeToTransactionGrid(DirectCast(e.Node, gloSnoMed.myTreeNode))
            Else
                AddNodeToTransactionGrid(e.Node)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtNurtSMSearch_SearchFired() Handles txtNurtSMSearch.SearchFired
        Dim _Term As String = Nothing

        Try

            If txtNurtSMSearch.Text.Length > 1 Then
                trvNurtFindings.BeginUpdate()
                Me.Cursor = Cursors.WaitCursor
                _Term = ""
                If chkNurtCOREProblem.Checked Then


                    objNurtclsgeneral.SearchCORESnomed(txtNurtSMSearch.Text.Trim(), trvNurtFindings, _NurtSearchBy)
                Else


                    objNurtclsgeneral.SearchSnomed(txtNurtSMSearch.Text.Trim(), False, trvNurtFindings, _NurtSearchBy)
                End If

                _Term = strNurtConceptDesc

                If chkNurtCOREProblem.Checked Then

                    If trvNurtFindings.Nodes.Count > 0 Then

                        If trvNurtFindings.Nodes(0).Nodes.Count > 0 Then
                            If trvNurtFindings.Nodes.Count = 1 Then
                                Dim oNode__1 As TreeNode = trvNurtFindings.Nodes(0)
                                trvNurtFindings.SelectedNode = oNode__1
                                trvNurtFindings.Nodes(0).ExpandAll()
                                For Each onode__2 As TreeNode In trvNurtFindings.Nodes(0).Nodes
                                    If (onode__2 Is Nothing) = False Then

                                        If Convert.ToString(onode__2.Tag) = strNurtConceptID Then
                                            If Convert.ToString(onode__2.Tag) = strNurtConceptID And onode__2.Text.Trim() = _Term.Trim() Then
                                                trvNurtFindings.SelectedNode = onode__2
                                                ' TODO: might not be correct. Was : Exit For
                                                Exit For
                                            End If
                                        End If

                                    End If
                                Next

                            End If

                        End If
                    End If
                Else

                    For Each onode__2 As TreeNode In trvNurtFindings.Nodes
                        If (onode__2 Is Nothing) = False Then
                            If Convert.ToString(onode__2.Tag) = strNurtConceptID Then
                                If Convert.ToString(onode__2.Tag) = strNurtConceptID And onode__2.Name.Trim() = _Term.Trim() Then
                                    trvNurtFindings.SelectedNode = onode__2
                                    ' TODO: might not be correct. Was : Exit For
                                    Exit For
                                End If
                            End If

                        End If
                    Next
                End If
                ' }
                Me.Cursor = Cursors.[Default]


                trvNurtFindings.EndUpdate()
            Else
                trvNurtFindings.Nodes.Clear()
                'trvSubtype.Nodes.Clear()
                'trvSnoMed.Nodes.Clear()
                'trICD9.Nodes.Clear()
                'trICD10.Nodes.Clear()

                'Sanjog
                'lblConceptID.Text = ""
                'lblSnoMedID.Text = ""
                'lblDescriptionID.Text = ""
                'strSelectedSnoMedID = ""
                ''Sanjog
                'strSelectedDescriptionID = ""
            End If
        Catch
            '(Exception ex)
        Finally

            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

#Region "CORE Problem"
    Private Sub chkNurtCOREProblem_CheckedChanged(sender As Object, e As EventArgs) Handles chkNurtCOREProblem.CheckedChanged

        If _isFormLoading = False Then
            ClearNurtData()

            txtNurtSMSearch_SearchFired()
        End If

    End Sub
#End Region

    Private Sub ClearNurtData()
        trvNurtFindings.Nodes.Clear()
        txtNurtSMSearch.Clear()
    End Sub

    Private Sub mnuNurtFindings_Click(sender As Object, e As EventArgs)
        Dim oNode As TreeNode = Nothing
        Try

            'trICD9.Nodes.Clear()
            'trICD10.Nodes.Clear()

            'lblConceptID.Text = ""
            'lblSnoMedID.Text = ""
            'lblDescriptionID.Text = ""
            'strSelectedSnoMedID = ""
            strNurtSelectedDescriptionID = ""
            oNode = trvNurtFindings.SelectedNode
            If (oNode IsNot Nothing) Then
                If (oNode.Tag IsNot Nothing) Then
                    strNurtConceptID = oNode.Tag.ToString()
                    Dim ICD9Description As String = ""

                    If String.IsNullOrEmpty(oNode.Name) Then
                        oNode.Name = oNode.Text
                    End If
                    ICD9Description = oNode.Name.Trim()
                    If chkNurtCOREProblem.Checked Then
                        Dim dsICD As DataSet = Nothing
                        If oNode.Parent IsNot Nothing Then
                            Dim ICDCode As [String] = objNurtclsgeneral.Fill_ICD9(oNode.Parent.Text)
                            'If RbICD9.Checked Then
                            '    dsICD = objclsgeneral.GetCOREICDData(strConceptID, ICDCode, "ICD9")
                            'Else
                            '    dsICD = objclsgeneral.GetCOREICDData(strConceptID, ICDCode, "ICD10")



                            'End If
                        Else
                            ' String ICDCode = objclsgeneral.Fill_ICD9(oNode.Parent.Text);
                            dsICD = objNurtclsgeneral.GetCOREICDData(strNurtConceptID, "")
                        End If
                        'If dsICD IsNot Nothing Then
                        '    objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsICD, trICD10)
                        '    objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsICD, trICD9)
                        '    If dsICD.Tables("RxNormNDC").Rows.Count > 0 Then
                        '        strNDCCode = dsICD.Tables("RxNormNDC").Rows(0)("NDCCode").ToString()
                        '        strRxNormCode = dsICD.Tables("RxNormNDC").Rows(0)("RxNorm").ToString()
                        '    Else
                        '        strNDCCode = ""
                        '        strRxNormCode = ""
                        '    End If
                        '    dsICD.Dispose()
                        '    dsICD = Nothing
                        'Else
                        '    strNDCCode = ""

                        '    strRxNormCode = ""
                        'End If
                    Else
                        Dim dsSnomed As DataSet = objNurtclsgeneral.Fill_SnomedDetails(strNurtConceptID, "False", ICD9Description, False)
                        'If dsSnomed IsNot Nothing Then
                        'objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsSnomed, trICD10)
                        'objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsSnomed, trICD9)
                        '    If dsSnomed.Tables("RxNormNDC").Rows.Count > 0 Then
                        '        strNDCCode = dsSnomed.Tables("RxNormNDC").Rows(0)("NDCCode").ToString()
                        '        strRxNormCode = dsSnomed.Tables("RxNormNDC").Rows(0)("RxNorm").ToString()
                        '    Else
                        '        strNDCCode = ""
                        '        strRxNormCode = ""
                        '    End If
                        '    dsSnomed.Dispose()
                        '    dsSnomed = Nothing
                        'Else
                        '    strNDCCode = ""
                        '    strRxNormCode = ""

                        'End If
                    End If

                    strNurtSelectedConceptID = strNurtConceptID
                    If Not String.IsNullOrEmpty(oNode.Name) Then
                        'ICD9Desc = objclsgeneral.Fill_ICD9Description(oNode.Name);
                        'lblConceptID.Text = strSelectedConceptID + " - " + oNode.Name
                    Else
                        'lblConceptID.Text = strSelectedConceptID
                    End If



                    strNurtProblem = oNode.Name
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If oNode IsNot Nothing Then
                oNode = Nothing
            End If
        End Try

    End Sub

    'Private Sub AddNurritionToTransactionGrid(mynode As gloSnoMed.myTreeNode)
    '    Try

    '        If (mynode IsNot Nothing) Then
    '            Dim TestName As String = Convert.ToString(mynode.ConceptID)
    '            ''Dim DrugId As Long = Convert.ToInt64(Row(0))
    '            pnlActivityDetail.Visible = True
    '            '' txtSelectedCode.Text = Convert.ToString(DrugId) & " - " & DrugName
    '            txtSelectedCode.Text = TestName
    '            c1PlanActivity.Rows.Add()
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityType, "Nutrition Recommendation")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityDetails, "")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentName, Convert.ToString(mynode.Text))
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCode, TestName)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityCodeSystem, "2.16.840.1.113883.6.96")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityFrom, dtpActivityEffectiveFrom.Value)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTo, dtpActivityEffectiveTo.Value)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityStatus, ActivityStatus.Active)
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReason, "")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityReasonCode, "")
    '            c1PlanActivity.SetData(c1PlanActivity.Rows.Count - 1, col_ActivityTreatmentID, TestName)
    '            Dim _Isgroup As Boolean = False
    '            If (mynode.Parent Is Nothing) Then
    '                _Isgroup = True
    '            End If
    '            c1PlanActivity.Select(c1PlanActivity.Rows.Count - 1, 0)
    '            cmbProblemList.Items.Clear()
    '            cmbProblemList.Text = ""
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
    '    End Try
    'End Sub

    Private Sub btnNurtClear_Click(sender As System.Object, e As System.EventArgs) Handles btnNurtClear.Click
        ClearNurtData()
    End Sub
#End Region


    Private Sub btnClearSelectedProblem_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSelectedProblem.Click
        cmbProblemList.Items.Clear()
        cmbProblemList.Text = ""
    End Sub

#Region "Exam Selecter"
    Private Sub btnExam_Click(sender As System.Object, e As System.EventArgs) Handles btnExam.Click
        Try
            LoadUserGridExam()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUserGridExam()
        Try
            AddControlExam()
            If Not IsNothing(dgCustomGridselectExam) Then
                dgCustomGridselectExam.Visible = True
                dgCustomGridselectExam.Width = pnlHeader.Width
                dgCustomGridselectExam.Height = pnlHeader.Height
                dgCustomGridselectExam.txtsearch.Width = 120
                dgCustomGridselectExam.SetVisible = False
                dgCustomGridselectExam.BringToFront()
                BindUserGridExam()
                dgCustomGridselectExam.Selectsearch(CustomDataGrid.enmcontrol.Search)
                pnlExam.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddControlExam()

        If Not IsNothing(dgCustomGridselectExam) Then
            RemoveControlExam()
        End If
        dgCustomGridselectExam = New CustomTask
        dgCustomGridselectExam.Dock = DockStyle.Fill
        pnlHeader.Controls.Add(dgCustomGridselectExam)
        dgCustomGridselectExam.BringToFront()

    End Sub

    Private Sub RemoveControlExam()
        If Not IsNothing(dgCustomGridselectExam) Then
            pnlHeader.Controls.Remove(dgCustomGridselectExam)
            dgCustomGridselectExam.Visible = False
            dgCustomGridselectExam.Dispose()
            dgCustomGridselectExam = Nothing
        End If
    End Sub

    Private Sub BindUserGridExam()
        Try
            Dim dt As DataTable
            Dim objPatientDetail As New clsPatientDetails

            dt = objPatientDetail.Fill_PastExams(PatientID)
            objPatientDetail.Dispose()
            objPatientDetail = Nothing
            CustomDrugsGridStyleExam()

            If Not IsNothing(dt) Then
                dgCustomGridselectExam.datasource(dt.DefaultView)
            End If

            Dim _width As Single = dgCustomGridselectExam.C1Task.Width - 5

            dgCustomGridselectExam.C1Task.ShowCellLabels = True
            dgCustomGridselectExam.C1Task.AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eExamID).Visible = False
            dgCustomGridselectExam.C1Task.Cols(Col_eVistitID).Visible = False
            dgCustomGridselectExam.C1Task.Cols(Col_eDos).Width = _width * 0.1
            dgCustomGridselectExam.C1Task.Cols(Col_eExamName).Width = _width * 0.24
            dgCustomGridselectExam.C1Task.Cols(Col_eTemplateName).Width = _width * 0.15
            dgCustomGridselectExam.C1Task.Cols(Col_eReviewedBy).Width = _width * 0.1
            dgCustomGridselectExam.C1Task.Cols(Col_eProviderName).Width = _width * 0.2
            dgCustomGridselectExam.C1Task.Cols(Col_eFinished).Width = _width * 0.1
            dgCustomGridselectExam.C1Task.Cols(Col_eSpeciality).Width = _width * 0.11

            '31-May-16 Aniket: Resolving Bug #95883: CDA -> No spacing in grid headers
            dgCustomGridselectExam.C1Task.Cols(Col_eProviderName).Caption = "Provider Name"
            dgCustomGridselectExam.C1Task.Cols(Col_eReviewedBy).Caption = "Reviewed By"

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub CustomDrugsGridStyleExam()

        Dim _TotalWidth As Single = dgCustomGridselectExam.C1Task.Width - 5


        With dgCustomGridselectExam.C1Task
            .Redraw = False
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_eCount
            .AllowEditing = False


            .Cols(Col_eExamID).Visible = False
            .Cols(Col_eVistitID).Visible = False
            .Cols(Col_eDos).Width = _TotalWidth * 0.1
            .Cols(Col_eExamName).Width = _TotalWidth * 0.24
            .Cols(Col_eTemplateName).Width = _TotalWidth * 0.15
            .Cols(Col_eReviewedBy).Width = _TotalWidth * 0.1
            .Cols(Col_eProviderName).Width = _TotalWidth * 0.2
            .Cols(Col_eFinished).Width = _TotalWidth * 0.1
            .Cols(Col_eSpeciality).Width = _TotalWidth * 0.11
            .Redraw = True

        End With

    End Sub

    Private Function GetExamDetails(ByVal ExamID As Long) As DataTable

        Dim daExamDetails As SqlDataAdapter = Nothing
        Dim dsExamDetails As DataSet = Nothing
        Dim connMain As SqlConnection = Nothing
        Dim cmdMain As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim dt As DataTable = Nothing
        Try


            connMain = New System.Data.SqlClient.SqlConnection(GetConnectionString)
            Dim strSql As String = "SELECT ISNULL(PatientExams.nExamID,0) as  nExamID,ISNULL(PatientExams.nVisitID,0) AS nVisitID,PatientExams.dtDOS AS DOS, " &
                                   "ISNULL(PatientExams.sExamName,'') AS [Exam Name],ISNULL(PatientExams.sTemplateName,'') AS [Template Name]     " &
                                   "FROM PatientExams " &
                                   "WHERE(nExamID = " & ExamID & " And PatientExams.sPatientNotes Is Not NULL) "



            cmdMain = New SqlCommand(strSql, connMain)
            cmdMain.CommandType = CommandType.Text
            connMain.Open()
            daExamDetails = New SqlDataAdapter(cmdMain)
            dsExamDetails = New DataSet
            daExamDetails.Fill(dsExamDetails)
            cmdMain.Parameters.Clear()
            cmdMain.Dispose() : cmdMain = Nothing
            daExamDetails.Dispose() : daExamDetails = Nothing
            connMain.Close()
            connMain.Dispose() : connMain = Nothing
            sqlParam = Nothing
            dt = dsExamDetails.Tables(0).Copy()
            Return dt

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            If Not IsNothing(dsExamDetails) Then  ''slr free dsExamDetails
                dsExamDetails.Dispose()
                dsExamDetails = Nothing
            End If

        End Try


    End Function

    Private Sub dgCustomGridselectExam_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.CloseClick
        dgCustomGridselectExam.Visible = False
        pnlExam.Visible = True
    End Sub

    Private Sub dgCustomGridselectExam_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.OKClick
        Try
            ExamID = System.Convert.ToInt64(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eExamID))
            VisitID = System.Convert.ToInt64(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eVistitID))
            lblExamName.Text = Convert.ToDateTime(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eDos)).ToString("MM/dd/yyyy") & " - " & System.Convert.ToString(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eExamName))
            dgCustomGridselectExam.Visible = False
            pnlExam.Visible = True
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub dgCustomGridselectExam_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As New DataView()
            dvPatient = CType(dgCustomGridselectExam.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGridselectExam.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGridselectExam.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(Col_eExamName).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(Col_eTemplateName).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(Col_eProviderName).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(Col_eSpeciality).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "

            dgCustomGridselectExam.Enabled = False
            dgCustomGridselectExam.datasource(dvPatient)
            dgCustomGridselectExam.Enabled = True
            Me.Cursor = Cursors.Default




            dgCustomGridselectExam.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region


    Private Sub c1PlanActivity_StartEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1PlanActivity.StartEdit
        Select Case Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityType))
            Case "Planned Medication"
                e.Cancel = True
            Case "Planned Encounter"
                e.Cancel = True
            Case "Nutrition Recommendation"
                e.Cancel = True
            Case "Planned Lab Orders"
                If c1PlanActivity.ColSel = col_ActivityCode Then
                    If Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityCode)).Length > 15 Then
                        e.Cancel = True
                    End If
                Else
                    e.Cancel = True
                End If



        End Select
    End Sub

    Private Sub btnExamClear_Click(sender As System.Object, e As System.EventArgs) Handles btnExamClear.Click
        ExamID = 0
        VisitID = 0
        lblExamName.Text = ""

    End Sub

    Private Sub EnableDisableDateControl(nRowSel As Integer)
        If Convert.ToString(c1PlanActivity.GetData(nRowSel, col_ActivityFrom)) = "-" Then
            dtpActivityEffectiveFrom.Checked = False
            dtpActivityEffectiveFrom.Text = ""
        Else
            dtpActivityEffectiveFrom.Checked = True
            dtpActivityEffectiveFrom.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityFrom))
        End If
        If Convert.ToString(c1PlanActivity.GetData(nRowSel, col_ActivityTo)) = "-" Then
            dtpActivityEffectiveTo.Checked = False
            dtpActivityEffectiveTo.Text = ""
        Else
            dtpActivityEffectiveTo.Checked = True
            dtpActivityEffectiveTo.Text = Convert.ToString(c1PlanActivity.GetData(c1PlanActivity.RowSel, col_ActivityTo))
        End If
    End Sub

End Class

Public Class ComboboxItem
    Dim _displayMember As String = ""
    Dim _valueMember As Int64 = 0

    Public Property displayMember() As String
        Get
            Return _displayMember
        End Get
        Set(ByVal value As String)
            _displayMember = value
        End Set
    End Property

    Public Property valueMember() As Int64
        Get
            Return _valueMember
        End Get
        Set(ByVal Value As Int64)
            _valueMember = Value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _displayMember
    End Function
End Class
