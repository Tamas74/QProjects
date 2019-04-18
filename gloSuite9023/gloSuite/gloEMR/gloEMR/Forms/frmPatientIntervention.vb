Imports gloCommon

Public Class frmPatientIntervention
    Dim _PatientID As Long = 0
    Dim _VisitID As Long = 0
    Dim _InterventionID As Long = 0
    Dim _POTID As Long = 0
    Dim dtInterventionAssociationTVP As DataTable
    Dim oListControl As gloListControl.gloListControl
    Dim ofrmCPTList As frmViewListControl
    Dim _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Other
    Dim bIsGoalRefChanged As Boolean = False
    Dim bIsNutritionRefChanged As Boolean = False
    Dim bIsMedicationRefChanged As Boolean = False
    Dim bIsLabOrderRefChanged As Boolean = False
    Dim bIsImmunizationRefChanged As Boolean = False
    Dim bIsEncounterRefChanged As Boolean = False
    Dim bIsEducationRefChanged As Boolean = False

    Enum InterventionAssociationType
        Goal = 1
        Nutrition = 2
        Medication = 3
        LabOrder = 4
        Immunization = 5
        Encounter = 6
        Education = 7
    End Enum

#Region "Constructor"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, Optional ByVal InterventionID As Long = 0)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        _InterventionID = InterventionID
        ' Add any initialization after the InitializeComponent() call.  
    End Sub
#End Region

    Private Sub frmPatientIntervention_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        txtInterventionName.Focus()
        chkDate.Checked = True
        setInterventionDT()
        rbt_ActualIntervention.Checked = True
        If _InterventionID <> 0 Then
            loadIntervention()
        End If
        dtpInterventionDate.Enabled = chkDate.Checked
        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

    End Sub

    Private Sub setInterventionDT()
        'Table used for CP_TVPInterventionAssociation TVP parameter values
        dtInterventionAssociationTVP = New DataTable()
        dtInterventionAssociationTVP.Columns.Add("nInterventionAssociationID")
        dtInterventionAssociationTVP.Columns.Add("nInterventionId")
        dtInterventionAssociationTVP.Columns.Add("nAssociationID")
        dtInterventionAssociationTVP.Columns.Add("nAssociationType")
        dtInterventionAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtInterventionAssociationTVP.Columns.Add("RowState")
        dtInterventionAssociationTVP.Columns.Add("sInstruction")
        dtInterventionAssociationTVP.Columns.Add("nRelatedId")
    End Sub

    Private Sub loadIntervention()
        Try
            Dim dsInterventionDetails As DataSet = Nothing
            Using objPatientIntervention As New ClsIntervention()
                dsInterventionDetails = objPatientIntervention.GetPatientIntervention(_PatientID, _InterventionID)
            End Using

            If dsInterventionDetails IsNot Nothing Then
                If dsInterventionDetails.Tables("InterventionDetail").Rows.Count > 0 Then
                    _VisitID = dsInterventionDetails.Tables("InterventionDetail").Rows(0)("nVisitID")
                    txtInterventionName.Text = dsInterventionDetails.Tables("InterventionDetail").Rows(0)("sInterventionName")
                    _POTID = dsInterventionDetails.Tables("InterventionDetail").Rows(0)("nPlanOfTreatmentID")

                    'Intervention Type
                    Dim intType As String = dsInterventionDetails.Tables("InterventionDetail").Rows(0)("sInterventionType")
                    If intType = "Actual" Then
                        rbt_ActualIntervention.Checked = True
                    ElseIf intType = "Planned" Then
                        rbt_PlannedIntervention.Checked = True
                    End If

                    txtInterventionNotes.Text = dsInterventionDetails.Tables("InterventionDetail").Rows(0)("sInterventionNotes")
                    If IsDBNull(dsInterventionDetails.Tables("InterventionDetail").Rows(0)("dtInterventionDate")) Then
                        chkDate.Checked = False
                    Else
                        chkDate.Checked = True
                        dtpInterventionDate.Text = dsInterventionDetails.Tables("InterventionDetail").Rows(0)("dtInterventionDate")
                    End If
                End If

                If Not dsInterventionDetails.Tables("InterventionAssociation") Is Nothing Then
                    Dim oGoalTable As New DataTable()
                    oGoalTable.Columns.Add("ID")
                    oGoalTable.Columns.Add("DispName")

                    Dim oNutritionTable As New DataTable()
                    oNutritionTable.Columns.Add("ID")
                    oNutritionTable.Columns.Add("DispName")

                    Dim oMedicationTable As New DataTable()
                    oMedicationTable.Columns.Add("ID")
                    oMedicationTable.Columns.Add("DispName")

                    Dim oLabOrderTable As New DataTable()
                    oLabOrderTable.Columns.Add("ID")
                    oLabOrderTable.Columns.Add("DispName")

                    Dim oImmunizationTable As New DataTable()
                    oImmunizationTable.Columns.Add("ID")
                    oImmunizationTable.Columns.Add("DispName")

                    Dim oEncounterTable As New DataTable()
                    oEncounterTable.Columns.Add("ID")
                    oEncounterTable.Columns.Add("DispName")

                    Dim oEducationTable As New DataTable()
                    oEducationTable.Columns.Add("ID")
                    oEducationTable.Columns.Add("DispName")

                    Dim AssociationType As Int16 = 0
                    dtInterventionAssociationTVP.Rows.Clear()
                    For h As Int32 = 0 To dsInterventionDetails.Tables("InterventionAssociation").Rows.Count - 1
                        dtInterventionAssociationTVP.Rows.Add()
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nInterventionAssociationID")
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = _InterventionID
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                        AssociationType = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationType")
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = AssociationType
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("dtTransactiondatetime")
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Retrieved"
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("sInstruction") = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sInstruction")
                        dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nRelatedId") = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nRelatedId")

                        Dim oRow As DataRow
                        If AssociationType = 1 Then
                            'Table for Goal combobox datasource
                            oRow = oGoalTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oGoalTable.Rows.Add(oRow)
                        ElseIf AssociationType = 2 Then
                            'Table for Nutrition combobox datasource
                            oRow = oNutritionTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oNutritionTable.Rows.Add(oRow)

                            Dim strInstructions As String = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sInstruction").ToString()
                            If strInstructions <> "" Then
                                Dim aryIntrvnAssn As New ArrayList
                                aryIntrvnAssn.Add(InterventionAssociationType.Nutrition)
                                aryIntrvnAssn.Add(dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID").ToString())
                                aryIntrvnAssn.Add(strInstructions)

                                aryInstructions.Add(aryIntrvnAssn)
                            End If

                        ElseIf AssociationType = 3 Then
                            'Table for Medication combobox datasource
                            oRow = oMedicationTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oMedicationTable.Rows.Add(oRow)
                        ElseIf AssociationType = 4 Then
                            'Table for Lab Order combobox datasource
                            oRow = oLabOrderTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID") & "#" & dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nRelatedId")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oLabOrderTable.Rows.Add(oRow)
                        ElseIf AssociationType = 5 Then
                            'Table for Immunization combobox datasource
                            oRow = oImmunizationTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oImmunizationTable.Rows.Add(oRow)
                        ElseIf AssociationType = 6 Then
                            'Table for Lab Order combobox datasource
                            oRow = oEncounterTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oEncounterTable.Rows.Add(oRow)
                        ElseIf AssociationType = 7 Then
                            'Table for Lab Order combobox datasource
                            oRow = oEducationTable.NewRow()
                            oRow(0) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsInterventionDetails.Tables("InterventionAssociation").Rows(h)("sAssociationDesc")
                            oEducationTable.Rows.Add(oRow)
                        End If
                    Next

                    If oGoalTable.Rows.Count > 0 Then
                        cmbInterventionGoals.DataSource = oGoalTable
                        cmbInterventionGoals.DisplayMember = "DispName"
                        cmbInterventionGoals.ValueMember = "ID"
                    End If

                    If oNutritionTable.Rows.Count > 0 Then
                        cmbInterventionNutrition.DataSource = oNutritionTable
                        cmbInterventionNutrition.DisplayMember = "DispName"
                        cmbInterventionNutrition.ValueMember = "ID"
                    End If

                    If oMedicationTable.Rows.Count > 0 Then
                        cmbInterventionMedication.DataSource = oMedicationTable
                        cmbInterventionMedication.DisplayMember = "DispName"
                        cmbInterventionMedication.ValueMember = "ID"
                    End If

                    If oLabOrderTable.Rows.Count > 0 Then
                        cmbInterventionLabOrder.DataSource = oLabOrderTable
                        cmbInterventionLabOrder.DisplayMember = "DispName"
                        cmbInterventionLabOrder.ValueMember = "ID"
                    End If

                    If oImmunizationTable.Rows.Count > 0 Then
                        cmbInterventionImmunization.DataSource = oImmunizationTable
                        cmbInterventionImmunization.DisplayMember = "DispName"
                        cmbInterventionImmunization.ValueMember = "ID"
                    End If

                    If oEncounterTable.Rows.Count > 0 Then
                        cmbInterventionEncounter.DataSource = oEncounterTable
                        cmbInterventionEncounter.DisplayMember = "DispName"
                        cmbInterventionEncounter.ValueMember = "ID"
                    End If

                    If oEducationTable.Rows.Count > 0 Then
                        cmbInterventionPatientEducation.DataSource = oEducationTable
                        cmbInterventionPatientEducation.DisplayMember = "DispName"
                        cmbInterventionPatientEducation.ValueMember = "ID"
                    End If
                End If
            End If
            If dsInterventionDetails IsNot Nothing Then
                dsInterventionDetails.Clear()
                dsInterventionDetails.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_PatientIntervention_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientIntervention.ItemClicked
        Select Case e.ClickedItem.Name
            Case ts_btnOk.Name
                If ValidateEntries() Then
                    SaveIntervention()
                End If
            Case ts_btnCancel.Name
                Me.Close()
        End Select
    End Sub

    Private Function ValidateEntries() As Boolean
        If String.IsNullOrWhiteSpace(txtInterventionName.Text) Then
            MessageBox.Show("Please enter a name for intervention.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInterventionName.Focus()
            Return False
        End If
        If (cmbInterventionGoals.Items.Count < 1) Then
            MessageBox.Show("Intervention requires Goal reference.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnInterventionGoals.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub ReconcileInstructionArrayListItems(dt As DataTable)
        Try
            Dim RemoveItemsArrayList As New ArrayList
            'Dim copyInstructionlist As New ArrayList
            'copyInstructionlist = aryInstructions
            'Dim dt As New DataTable
            'If cmbInterventionNutrition.Items.Count > 0 Then
            '    dt = DirectCast(cmbInterventionNutrition.Items(0), System.Data.DataRowView).Row.Table
            'End If

            If aryInstructions.Count > 0 Then
                For arrayItems As Integer = 0 To aryInstructions.Count - 1
                    Dim blnItemPresent As Boolean = False
                    Dim nAssociationId As Long = aryInstructions.Item(arrayItems)(1)


                    For dtRows As Integer = 0 To dt.Rows.Count - 1
                        ''Dim sNutritionId = (New System.Collections.ArrayList.ArrayListDebugView((New System.Collections.ArrayList.ArrayListDebugView(aryInstructions)).Items(0))).Items(1)

                        If aryInstructions.Item(arrayItems)(1) = dt.Rows(dtRows)("ID") Then
                            blnItemPresent = True
                            Exit For
                        Else
                            blnItemPresent = False
                            nAssociationId = aryInstructions.Item(arrayItems)(1)
                        End If
                    Next

                    If blnItemPresent = False Then
                        RemoveItemsArrayList.Add(nAssociationId)
                        'copyInstructionlist.Remove(nAssociationId)
                    End If



                Next
            End If

            If (aryInstructions.Count > 0) And (RemoveItemsArrayList.Count > 0) Then
                For i As Integer = 0 To RemoveItemsArrayList.Count - 1
                    For j As Integer = 0 To aryInstructions.Count - 1
                        If RemoveItemsArrayList.Item(i) = aryInstructions.Item(j)(1) Then
                            aryInstructions.RemoveAt(j)
                            Exit For
                        End If
                    Next
                Next
                RemoveItemsArrayList.Clear()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SaveIntervention()
        Dim objIntervention As New ClsIntervention()
        Try
            Dim sIntType As String = String.Empty

            'Intervention Type
            If rbt_ActualIntervention.Checked Then
                sIntType = "Actual"
                _POTID = 0
            ElseIf rbt_PlannedIntervention.Checked Then
                sIntType = "Planned"
            End If

            objIntervention.nPatientId = _PatientID
            objIntervention.nVisitId = _VisitID
            objIntervention.sInterventionName = txtInterventionName.Text.Trim()
            objIntervention.nInterventionID = _InterventionID
            objIntervention.sInterventionNotes = txtInterventionNotes.Text.Trim()
            objIntervention.sInterventionType = sIntType
            objIntervention.nPOTID = _POTID
            objIntervention.dtInterventionDate = dtpInterventionDate.Text
            objIntervention.bIsDate = chkDate.Checked

            If bIsGoalRefChanged Or bIsNutritionRefChanged Or bIsMedicationRefChanged Or bIsMedicationRefChanged Or bIsLabOrderRefChanged Or bIsImmunizationRefChanged Or bIsEncounterRefChanged Or bIsEducationRefChanged Then
                fillTVPs()
            End If

            Dim sRowState As String = ""
            If _InterventionID = 0 Then
                sRowState = "Added"
            Else
                sRowState = "Updated"
            End If


            Dim nReturnId As Int64 = objIntervention.SaveIntervention(dtInterventionAssociationTVP, sRowState)

            If _InterventionID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, "Patient Intervention added", _PatientID, nReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Modify, "Patient Intervention Modified", _PatientID, _InterventionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objIntervention IsNot Nothing Then
                objIntervention.Dispose()
                objIntervention = Nothing
            End If
        End Try
    End Sub

    Private Sub fillTVPs()
        Try
            Dim IdCnt As Int32 = 0
            Dim nUCnt As Int32 = cmbInterventionGoals.Items.Count + cmbInterventionNutrition.Items.Count + cmbInterventionMedication.Items.Count + cmbInterventionLabOrder.Items.Count + cmbInterventionImmunization.Items.Count + cmbInterventionEncounter.Items.Count + cmbInterventionPatientEducation.Items.Count
            Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(nUCnt)
            If bIsGoalRefChanged Then
                If cmbInterventionGoals.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionGoals.Items.Count - 1
                        cmbInterventionGoals.Text = ""
                        cmbInterventionGoals.SelectedIndex = j

                        If Not checkAndUpdate(cmbInterventionGoals.SelectedValue, InterventionAssociationType.Goal) Then
                            dtInterventionAssociationTVP.Rows.Add()
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbInterventionGoals.SelectedValue
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.Goal)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            If bIsNutritionRefChanged Then
                If cmbInterventionNutrition.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionNutrition.Items.Count - 1
                        cmbInterventionNutrition.Text = ""
                        cmbInterventionNutrition.SelectedIndex = j

                        If Not checkAndUpdate(cmbInterventionNutrition.SelectedValue, InterventionAssociationType.Nutrition) Then
                            dtInterventionAssociationTVP.Rows.Add()
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbInterventionNutrition.SelectedValue
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.Nutrition)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            If bIsMedicationRefChanged Then
                If cmbInterventionMedication.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionMedication.Items.Count - 1
                        cmbInterventionMedication.Text = ""
                        cmbInterventionMedication.SelectedIndex = j

                        If Not checkAndUpdate(cmbInterventionMedication.SelectedValue, InterventionAssociationType.Medication) Then
                            dtInterventionAssociationTVP.Rows.Add()
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbInterventionMedication.SelectedValue
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.Medication)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            If bIsLabOrderRefChanged Then
                If cmbInterventionLabOrder.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionLabOrder.Items.Count - 1
                        cmbInterventionLabOrder.Text = ""
                        cmbInterventionLabOrder.SelectedIndex = j

                        Dim ids As String() = cmbInterventionLabOrder.SelectedValue.ToString().Split("#")
                        If ids.Length > 1 Then
                            If Not checkAndUpdateForLab(ids(0), InterventionAssociationType.LabOrder, ids(1)) Then
                                dtInterventionAssociationTVP.Rows.Add()
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = ids(0)
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.LabOrder)
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                                dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nRelatedId") = ids(1)
                                IdCnt += 1
                            End If
                        End If
                    Next
                End If
            End If

            If bIsImmunizationRefChanged Then
                If cmbInterventionImmunization.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionImmunization.Items.Count - 1
                        cmbInterventionImmunization.Text = ""
                        cmbInterventionImmunization.SelectedIndex = j

                        If Not checkAndUpdate(cmbInterventionImmunization.SelectedValue, InterventionAssociationType.Immunization) Then
                            dtInterventionAssociationTVP.Rows.Add()
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbInterventionImmunization.SelectedValue
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.Immunization)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            If bIsEncounterRefChanged Then
                If cmbInterventionEncounter.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionEncounter.Items.Count - 1
                        cmbInterventionEncounter.Text = ""
                        cmbInterventionEncounter.SelectedIndex = j

                        If Not checkAndUpdate(cmbInterventionEncounter.SelectedValue, InterventionAssociationType.Encounter) Then
                            dtInterventionAssociationTVP.Rows.Add()
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbInterventionEncounter.SelectedValue
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.Encounter)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            If bIsEducationRefChanged Then
                If cmbInterventionPatientEducation.Items.Count > 0 Then
                    For j As Integer = 0 To cmbInterventionPatientEducation.Items.Count - 1
                        cmbInterventionPatientEducation.Text = ""
                        cmbInterventionPatientEducation.SelectedIndex = j

                        If Not checkAndUpdate(cmbInterventionPatientEducation.SelectedValue, InterventionAssociationType.Education) Then
                            dtInterventionAssociationTVP.Rows.Add()
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nInterventionId") = "0"
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbInterventionPatientEducation.SelectedValue
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(InterventionAssociationType.Education)
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtInterventionAssociationTVP.Rows(dtInterventionAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            ''add nutrition instructions associated to the nutrition items
            If aryInstructions.Count > 0 Then
                For i As Integer = 0 To aryInstructions.Count - 1
                    UpdateInstructions(aryInstructions(i)(0), aryInstructions(i)(1), aryInstructions(i)(2))
                Next
            End If



            If Not dtUniqueIds Is Nothing Then
                dtUniqueIds.Rows.Clear()
                dtUniqueIds.Dispose()
                dtUniqueIds = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



#Region "Radio Button Events"

    Private Sub rbt_ActualIntervention_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_ActualIntervention.CheckedChanged
        If rbt_ActualIntervention.Checked = True Then
            rbt_ActualIntervention.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            btnAddPlanned.Enabled = False
            '_POTID = 0
        Else
            rbt_ActualIntervention.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_PlannedIntervention_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_PlannedIntervention.CheckedChanged
        If rbt_PlannedIntervention.Checked = True Then
            rbt_PlannedIntervention.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            btnAddPlanned.Enabled = True
        Else
            rbt_PlannedIntervention.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

#End Region

#Region "External reference events"

    Private Function UpdateInstructions(ByVal AssType As InterventionAssociationType, ByVal sAssociationId As String, ByVal Instructions As String) As Boolean
        Dim exists As Boolean = False
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)
            If dtInterventionAssociationTVP.Rows.Count > 0 Then
                'exists = dtInterventionAssociationTVP.AsEnumerable().Where(Function(c) (c.Field(Of String)("nAssociationID") = sAssociationId) And (c.Field(Of String)("nAssociationType") = nAssType)).Count() > 0
                'If exists Then
                ''dtInterventionAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) (r.Item("nAssociationID") = sAssociationId) And (r.Item("nAssociationType") = nAssType)).ToList().ForEach(Sub(r) r.SetField("sInstruction", Instructions))

                Dim joinID As IEnumerable(Of DataRow) = dtInterventionAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) (r.Item("nAssociationID") = sAssociationId) And (r.Item("nAssociationType") = nAssType))
                For Each row As DataRow In joinID
                    row.SetField("sInstruction", Instructions)
                    Dim currState As String = row("RowState").ToString()
                    If currState <> "Added" And currState <> "Deleted" Then
                        row.SetField("RowState", "Updated")
                    End If
                Next
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return exists
    End Function

    Private Sub markCurrRefAsDeleted(ByVal AssType As InterventionAssociationType)
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)

            Select Case AssType
                Case InterventionAssociationType.Goal
                    bIsGoalRefChanged = True
                Case InterventionAssociationType.Nutrition
                    bIsNutritionRefChanged = True
                Case InterventionAssociationType.Medication
                    bIsMedicationRefChanged = True
                Case InterventionAssociationType.LabOrder
                    bIsLabOrderRefChanged = True
                Case InterventionAssociationType.Immunization
                    bIsImmunizationRefChanged = True
                Case InterventionAssociationType.Encounter
                    bIsEncounterRefChanged = True
                Case InterventionAssociationType.Education
                    bIsEducationRefChanged = True
            End Select

            If dtInterventionAssociationTVP.Rows.Count > 0 Then
                dtInterventionAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("nAssociationType") = nAssType).ToList().ForEach(Sub(r) r.SetField("RowState", "Deleted"))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Function checkAndUpdate(ByVal sAssociationId As String, ByVal AssType As InterventionAssociationType) As Boolean
        Dim exists As Boolean = False
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)
            If dtInterventionAssociationTVP.Rows.Count > 0 Then
                exists = dtInterventionAssociationTVP.AsEnumerable().Where(Function(c) (c.Field(Of String)("nAssociationID") = sAssociationId) And (c.Field(Of String)("nAssociationType") = nAssType)).Count() > 0
                If exists Then
                    dtInterventionAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) (r.Item("nAssociationID") = sAssociationId) And (r.Item("nAssociationType") = nAssType)).ToList().ForEach(Sub(r) r.SetField("RowState", "NoChange"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return exists
    End Function

    Private Function checkAndUpdateForLab(ByVal sAssociationId As String, ByVal AssType As InterventionAssociationType, ByVal sRelativeId As String) As Boolean
        Dim exists As Boolean = False
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)
            If dtInterventionAssociationTVP.Rows.Count > 0 Then
                exists = dtInterventionAssociationTVP.AsEnumerable().Where(Function(c) (c.Field(Of String)("nAssociationID") = sAssociationId) And (c.Field(Of String)("nAssociationType") = nAssType) And (c.Field(Of String)("nRelatedId") = sRelativeId)).Count() > 0
                If exists Then
                    dtInterventionAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) (r.Item("nAssociationID") = sAssociationId) And (r.Item("nAssociationType") = nAssType) And (r.Item("nRelatedId") = sRelativeId)).ToList().ForEach(Sub(r) r.SetField("RowState", "NoChange"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return exists
    End Function

    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)
        Select Case _CurrentControlType
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



                    ReconcileInstructionArrayListItems(oBindTable)
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
                        oRow(0) = oListControl.SelectedItems(cnt).ID & "#" & oListControl.SelectedItems(cnt).Code
                        oRow(1) = oListControl.SelectedItems(cnt).Description
                        oBindTable.Rows.Add(oRow)
                    Next

                    cmbInterventionLabOrder.DataSource = oBindTable
                    cmbInterventionLabOrder.DisplayMember = "DispName"
                    cmbInterventionLabOrder.ValueMember = "ID"
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
            Case Else

        End Select
        ofrmCPTList.Close()
        'If IsNothing(ofrmCPTList) = False Then
        '    ofrmCPTList = Nothing
        'End If
    End Sub

    Private Sub oListControl_ItemClosedClick(sender As System.Object, e As System.EventArgs)
        ofrmCPTList.Close()
        'If IsNothing(ofrmCPTList) = False Then
        '    ofrmCPTList = Nothing
        'End If
    End Sub

    Private Sub btnClearInterventionGoals_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionGoals.Click
        cmbInterventionGoals.DataSource = Nothing
        cmbInterventionGoals.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.Goal)
    End Sub

    Private Sub btnInterventionGoals_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionGoals.Click
        Try
            markCurrRefAsDeleted(InterventionAssociationType.Goal)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanGoals, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanGoals
            oListControl.ControlHeader = "Patient Goals"
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
            ofrmCPTList.Text = "Patient Goals"
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearInterventionNutrition_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionNutrition.Click
        cmbInterventionNutrition.DataSource = Nothing
        cmbInterventionNutrition.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.Nutrition)
        aryInstructions.Clear()
    End Sub

    Private Sub btnInterventionNutrition_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionNutrition.Click
        Try
            markCurrRefAsDeleted(InterventionAssociationType.Nutrition)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanNutrition, True, Me.Width)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearInterventionMedication_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionMedication.Click
        cmbInterventionMedication.DataSource = Nothing
        cmbInterventionMedication.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.Medication)
    End Sub

    Private Sub btnInterventionMedication_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionMedication.Click
        Try
            markCurrRefAsDeleted(InterventionAssociationType.Medication)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanMedication, True, Me.Width)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearInterventionLabOrder_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionLabOrder.Click
        cmbInterventionLabOrder.DataSource = Nothing
        cmbInterventionLabOrder.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.LabOrder)
    End Sub

    Private Sub btnInterventionLabOrder_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionLabOrder.Click
        Try
            markCurrRefAsDeleted(InterventionAssociationType.LabOrder)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanLabOrder, True, Me.Width)
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
                    Dim SelectedLab As String() = cmbInterventionLabOrder.SelectedValue.ToString().Split("#")
                    If SelectedLab.Length > 1 Then
                        oListControl.SelectedItems.Add(Convert.ToInt64(SelectedLab(0)), SelectedLab(1), cmbInterventionLabOrder.Text)
                    End If
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClearInterventionImmunization_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionImmunization.Click
        cmbInterventionImmunization.DataSource = Nothing
        cmbInterventionImmunization.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.Immunization)
    End Sub

    Private Sub btnInterventionImmunization_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionImmunization.Click

        Try
            markCurrRefAsDeleted(InterventionAssociationType.Immunization)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanImmunization, True, Me.Width)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearInterventionEncounter_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionEncounter.Click
        cmbInterventionEncounter.DataSource = Nothing
        cmbInterventionEncounter.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.Encounter)
    End Sub

    Private Sub btnInterventionEncounter_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionEncounter.Click
        Try
            markCurrRefAsDeleted(InterventionAssociationType.Encounter)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanEncounter, True, Me.Width)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearInterventionPatientEducation_Click(sender As System.Object, e As System.EventArgs) Handles btnClearInterventionPatientEducation.Click
        cmbInterventionPatientEducation.DataSource = Nothing
        cmbInterventionPatientEducation.Items.Clear()
        markCurrRefAsDeleted(InterventionAssociationType.Education)
    End Sub

    Private Sub btnInterventionPatientEducation_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionPatientEducation.Click
        Try
            markCurrRefAsDeleted(InterventionAssociationType.Education)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, 0, GetConnectionString(), gloListControl.gloListControlType.CarePlanEducation, True, Me.Width)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddPlanned_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPlanned.Click
        Try
            Dim frm As New frmTreatmentPlan(_PatientID, _POTID, frmTreatmentPlan.POTType.CarePlan)
            frm.CarePlanPlanOfTreatmentID = _POTID
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            _POTID = frm.CarePlanPlanOfTreatmentID
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientIntervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


    Dim aryInstructions As New ArrayList()


    Private Sub cmbInterventionNutrition_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs) Handles cmbInterventionNutrition.SelectionChangeCommitted
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInterventionNutritionAssociationInstructions_Click(sender As System.Object, e As System.EventArgs) Handles btnInterventionNutritionAssociationInstructions.Click
        Try
            bIsNutritionRefChanged = True
            If aryInstructions.Count > 0 Then


                For i As Integer = 0 To aryInstructions.Count - 1

                    ''Dim sNutritionId = (New System.Collections.ArrayList.ArrayListDebugView((New System.Collections.ArrayList.ArrayListDebugView(aryInstructions)).Items(0))).Items(1)
                    If aryInstructions.Item(i)(1) = cmbInterventionNutrition.SelectedValue Then
                        Dim ofrmIntrvnAssnInstrctn As New frmInterrventionAssociationInstructionns()
                        ofrmIntrvnAssnInstrctn.Instruction = aryInstructions.Item(i)(2)
                        ofrmIntrvnAssnInstrctn.ShowDialog()
                        aryInstructions.Item(i)(2) = ofrmIntrvnAssnInstrctn.Instruction
                        Exit Sub
                    End If
                Next
            End If

            If cmbInterventionNutrition.Items.Count > 0 Then
                ''Dim ofrmIntrvnAssnInstrctn As New frmInterrventionAssociationInstructionns(InterventionAssociationType.Nutrition, Convert.ToInt64(cmbInterventionNutrition.SelectedValue))
                Dim ofrmIntrvnAssnInstrctn As New frmInterrventionAssociationInstructionns()

                ofrmIntrvnAssnInstrctn.ShowDialog()
                If Not IsNothing(ofrmIntrvnAssnInstrctn.Instruction) Then
                    Dim aryIntrvnAssn As New ArrayList
                    aryIntrvnAssn.Add(InterventionAssociationType.Nutrition)
                    aryIntrvnAssn.Add(cmbInterventionNutrition.SelectedValue)
                    aryIntrvnAssn.Add(ofrmIntrvnAssnInstrctn.Instruction)

                    aryInstructions.Add(aryIntrvnAssn)
                End If


                'aryIntrvnAssn.Clear()


                ofrmIntrvnAssnInstrctn.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub chkDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDate.CheckedChanged
        dtpInterventionDate.Enabled = chkDate.Checked
    End Sub
End Class