Imports gloCommon

Public Class frmPatientOutcome
    Dim _PatientID As Long = 0
    Dim _VisitID As Long = 0
    Dim _OutcomeID As Long = 0
    Dim dtOutcomeAssociationTVP As DataTable
    Dim oListControl As gloListControl.gloListControl
    Dim ofrmCPTList As frmViewListControl
    Dim _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Other
    Dim bIsGoalRefChanged As Boolean = False
    Dim bIsInterventionRefChanged As Boolean = False

    Enum OutcomeAssociationType
        Goal = 1
        Intervention = 2
    End Enum

#Region "Constructor"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, Optional ByVal OutcomeID As Long = 0)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        _OutcomeID = OutcomeID
        ' Add any initialization after the InitializeComponent() call.  
    End Sub
#End Region

    Private Sub frmPatientOutcome_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtOutcomeName.Focus()

        setOutcomeDT()
        chkDate.Checked = False
        loadUnits()
        fillStatusCombo()
        If _OutcomeID <> 0 Then
            loadOutcome()
        End If
        dtpOutcomeDate.Enabled = chkDate.Checked

        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

    End Sub

    Private Sub setOutcomeDT()
        'Table used for CP_TVPOutcomeAssociation TVP parameter values
        dtOutcomeAssociationTVP = New DataTable()
        dtOutcomeAssociationTVP.Columns.Add("nOutcomeAssociationID")
        dtOutcomeAssociationTVP.Columns.Add("nOutcomeID")
        dtOutcomeAssociationTVP.Columns.Add("nAssociationID")
        dtOutcomeAssociationTVP.Columns.Add("nAssociationType")
        dtOutcomeAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtOutcomeAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub fillStatusCombo()
        cmbStatus.Items.Clear()
        cmbStatus.Items.Add("")
        cmbStatus.Items.Add("Achieved")
        cmbStatus.Items.Add("Not Achieved")
    End Sub

    Private Sub loadUnits()
        Dim dtLoincUnits As DataTable = Nothing
        Dim objPatientCarePlan As New ClsPatientCarePlan()
        dtLoincUnits = objPatientCarePlan.GetUnits()
        If Not IsNothing(dtLoincUnits) Then
            cmbUnit.DataSource = dtLoincUnits
            cmbUnit.DisplayMember = dtLoincUnits.Columns("UCUMUnits").ToString()
            cmbUnit.ValueMember = ""
        End If
    End Sub

    Private Sub loadOutcome()
        Try
            Dim dsOutcomeDetails As DataSet = Nothing
            Using objPatientOutcome As New ClsOutcome()
                dsOutcomeDetails = objPatientOutcome.GetPatientOutcome(_PatientID, _OutcomeID)
            End Using

            If dsOutcomeDetails IsNot Nothing Then
                If dsOutcomeDetails.Tables("OutcomeDetail").Rows.Count > 0 Then
                    _VisitID = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("nVisitID")
                    txtOutcomeName.Text = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sOutcomeName")
                    cmbStatus.Text = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sOutcomeStatus")
                    txtOutcomeNotes.Text = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sOutcomeNotes")
                    If IsDBNull(dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sValue")) Then
                        txtlValue.Text = ""
                    Else
                        txtlValue.Text = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sValue")
                    End If
                    If IsDBNull(dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sUnit")) Then
                        cmbUnit.Text = ""
                    Else
                        cmbUnit.Text = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("sUnit")
                    End If

                    If IsDBNull(dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("dtOutcomeDate")) Then
                        chkDate.Checked = False
                    Else
                        chkDate.Checked = True
                        dtpOutcomeDate.Text = dsOutcomeDetails.Tables("OutcomeDetail").Rows(0)("dtOutcomeDate")
                    End If
                End If

                If Not dsOutcomeDetails.Tables("OutcomeAssociation") Is Nothing Then
                    Dim oGoalTable As New DataTable()
                    oGoalTable.Columns.Add("ID")
                    oGoalTable.Columns.Add("DispName")

                    Dim oInterventionTable As New DataTable()
                    oInterventionTable.Columns.Add("ID")
                    oInterventionTable.Columns.Add("DispName")

                    Dim AssociationType As Int16 = 0
                    dtOutcomeAssociationTVP.Rows.Clear()
                    For h As Int32 = 0 To dsOutcomeDetails.Tables("OutcomeAssociation").Rows.Count - 1
                        dtOutcomeAssociationTVP.Rows.Add()
                        dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeAssociationID") = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("nOutcomeAssociationID")
                        dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeID") = _OutcomeID
                        dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nAssociationID") = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("nAssociationID")
                        AssociationType = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("nAssociationType")
                        dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nAssociationType") = AssociationType
                        dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("dtTransactiondatetime")
                        dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("RowState") = "Retrieved"

                        Dim oRow As DataRow
                        If AssociationType = 1 Then
                            'Table for Goal combobox datasource
                            oRow = oGoalTable.NewRow()
                            oRow(0) = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("sAssociationDesc")
                            oGoalTable.Rows.Add(oRow)
                        ElseIf AssociationType = 2 Then
                            'Table for Intervention combobox datasource
                            oRow = oInterventionTable.NewRow()
                            oRow(0) = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsOutcomeDetails.Tables("OutcomeAssociation").Rows(h)("sAssociationDesc")
                            oInterventionTable.Rows.Add(oRow)
                        End If
                    Next

                    If oGoalTable.Rows.Count > 0 Then
                        cmbGoals.DataSource = oGoalTable
                        cmbGoals.DisplayMember = "DispName"
                        cmbGoals.ValueMember = "ID"
                    End If

                    If oInterventionTable.Rows.Count > 0 Then
                        cmbIntervention.DataSource = oInterventionTable
                        cmbIntervention.DisplayMember = "DispName"
                        cmbIntervention.ValueMember = "ID"
                    End If
                End If
            End If
            If dsOutcomeDetails IsNot Nothing Then
                dsOutcomeDetails.Clear()
                dsOutcomeDetails.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_PatientOutcome_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientOutcome.ItemClicked
        Select Case e.ClickedItem.Name
            Case ts_btnOk.Name
                If ValidateEntries() Then
                    SaveOutcome()
                End If
            Case ts_btnCancel.Name
                Me.Close()
        End Select
    End Sub

    Private Function ValidateEntries() As Boolean
        If String.IsNullOrWhiteSpace(txtOutcomeName.Text) Then
            MessageBox.Show("Please enter a name for outcome.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtOutcomeName.Focus()
            Return False
        End If
        If (cmbGoals.Items.Count < 1) AndAlso (cmbIntervention.Items.Count < 1) Then
            MessageBox.Show("Outcome requires Intervention or Goal reference.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnIntervention.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub SaveOutcome()
        Dim objOutcome As New ClsOutcome()
        Try
            objOutcome.nPatientId = _PatientID
            objOutcome.nVisitId = _VisitID
            objOutcome.sOutcomeName = txtOutcomeName.Text.Trim()
            objOutcome.nOutcomeID = _OutcomeID
            objOutcome.sOutcomeNotes = txtOutcomeNotes.Text.Trim()
            objOutcome.sOutcomeStatus = cmbStatus.Text
            objOutcome.dtOutcomeDate = dtpOutcomeDate.Text
            objOutcome.sOutcomeUnit = cmbUnit.Text
            objOutcome.sOutcomeValue = txtlValue.Text.Trim()
            objOutcome.bIsDate = chkDate.Checked

            If bIsGoalRefChanged Or bIsInterventionRefChanged Then
                fillTVPs()
            End If

            Dim sRowState As String = ""
            If _OutcomeID = 0 Then
                sRowState = "Added"
            Else
                sRowState = "Updated"
            End If


            Dim nReturnId As Int64 = objOutcome.SaveOutcome(dtOutcomeAssociationTVP, sRowState)

            If _OutcomeID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Add, "Patient Outcome added", _PatientID, nReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Modify, "Patient Outcome Modified", _PatientID, _OutcomeID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objOutcome IsNot Nothing Then
                objOutcome.Dispose()
                objOutcome = Nothing
            End If
        End Try
    End Sub

    Private Sub fillTVPs()
        Try
            Dim IdCnt As Int32 = 0
            Dim nUCnt As Int32 = cmbGoals.Items.Count + cmbIntervention.Items.Count
            Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(nUCnt)
            If bIsGoalRefChanged Then
                If cmbGoals.Items.Count > 0 Then
                    For j As Integer = 0 To cmbGoals.Items.Count - 1
                        cmbGoals.Text = ""
                        cmbGoals.SelectedIndex = j

                        If Not checkAndUpdate(cmbGoals.SelectedValue, OutcomeAssociationType.Goal) Then
                            dtOutcomeAssociationTVP.Rows.Add()
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeID") = "0"
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbGoals.SelectedValue
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(OutcomeAssociationType.Goal)
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
            End If

            If bIsInterventionRefChanged Then
                If cmbIntervention.Items.Count > 0 Then
                    For j As Integer = 0 To cmbIntervention.Items.Count - 1
                        cmbIntervention.Text = ""
                        cmbIntervention.SelectedIndex = j

                        If Not checkAndUpdate(cmbIntervention.SelectedValue, OutcomeAssociationType.Intervention) Then
                            dtOutcomeAssociationTVP.Rows.Add()
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeAssociationID") = dtUniqueIds.Rows(IdCnt)(0)
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nOutcomeID") = "0"
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbIntervention.SelectedValue
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(OutcomeAssociationType.Intervention)
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtOutcomeAssociationTVP.Rows(dtOutcomeAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                            IdCnt += 1
                        End If
                    Next
                End If
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

    Private Sub markCurrRefAsDeleted(ByVal AssType As OutcomeAssociationType)
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)

            Select Case AssType
                Case OutcomeAssociationType.Goal
                    bIsGoalRefChanged = True
                Case OutcomeAssociationType.Intervention
                    bIsInterventionRefChanged = True
            End Select

            If dtOutcomeAssociationTVP.Rows.Count > 0 Then
                dtOutcomeAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("nAssociationType") = nAssType).ToList().ForEach(Sub(r) r.SetField("RowState", "Deleted"))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Function checkAndUpdate(ByVal sAssociationId As String, ByVal AssType As OutcomeAssociationType) As Boolean
        Dim exists As Boolean = False
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)
            If dtOutcomeAssociationTVP.Rows.Count > 0 Then
                exists = dtOutcomeAssociationTVP.AsEnumerable().Where(Function(c) (c.Field(Of String)("nAssociationID") = sAssociationId) And (c.Field(Of String)("nAssociationType") = nAssType)).Count() > 0
                If exists Then
                    dtOutcomeAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) (r.Item("nAssociationID") = sAssociationId) And (r.Item("nAssociationType") = nAssType)).ToList().ForEach(Sub(r) r.SetField("RowState", "NoChange"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return exists
    End Function

    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)
        Select Case _CurrentControlType
            Case gloListControl.gloListControlType.CarePlanGoals
                cmbGoals.DataSource = Nothing
                cmbGoals.Items.Clear()
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

                    cmbGoals.DataSource = oBindTable
                    cmbGoals.DisplayMember = "DispName"
                    cmbGoals.ValueMember = "ID"
                End If
            Case gloListControl.gloListControlType.Intervention
                cmbIntervention.DataSource = Nothing
                cmbIntervention.Items.Clear()
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

                    cmbIntervention.DataSource = oBindTable
                    cmbIntervention.DisplayMember = "DispName"
                    cmbIntervention.ValueMember = "ID"
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

    Private Sub btnClearGoals_Click(sender As System.Object, e As System.EventArgs) Handles btnClearGoals.Click
        cmbGoals.DataSource = Nothing
        cmbGoals.Items.Clear()
        markCurrRefAsDeleted(OutcomeAssociationType.Goal)
    End Sub

    Private Sub btnGoals_Click(sender As System.Object, e As System.EventArgs) Handles btnGoals.Click
        Try
            markCurrRefAsDeleted(OutcomeAssociationType.Goal)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.CarePlanGoals, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanGoals
            oListControl.ControlHeader = "Patient Goals"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)

            If cmbGoals.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbGoals.Items.Count - 1
                    cmbGoals.SelectedIndex = i
                    cmbGoals.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbGoals.SelectedValue), cmbGoals.Text)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearIntervention_Click(sender As System.Object, e As System.EventArgs) Handles btnClearIntervention.Click
        cmbIntervention.DataSource = Nothing
        cmbIntervention.Items.Clear()
        markCurrRefAsDeleted(OutcomeAssociationType.Intervention)
    End Sub

    Private Sub btnIntervention_Click(sender As System.Object, e As System.EventArgs) Handles btnIntervention.Click
        Try
            markCurrRefAsDeleted(OutcomeAssociationType.Intervention)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(_PatientID, _VisitID, GetConnectionString(), gloListControl.gloListControlType.Intervention, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.Intervention
            oListControl.ControlHeader = "Patient Intervention"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)

            If cmbIntervention.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbIntervention.Items.Count - 1
                    cmbIntervention.SelectedIndex = i
                    cmbIntervention.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbIntervention.SelectedValue), cmbIntervention.Text)
                Next
            End If

            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Patient Intervention"
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientOutcome, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDate.CheckedChanged
        dtpOutcomeDate.Enabled = chkDate.Checked
    End Sub
End Class