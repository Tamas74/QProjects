Imports gloCommon

Public Class frmPatientGoal

    Dim _PatientID As Long = 0
    Dim _VisitID As Long = 0
    Dim _GoalID As Long = 0
    Dim oListControl As gloListControl.gloListControl
    Dim ofrmCPTList As frmViewListControl
    Dim dtGoalAssociationTVP As DataTable
    Dim _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Other
    Dim bIsProbRefChanged As Boolean = False
    Dim bIsHCRefChanged As Boolean = False

    Enum GoalAssociationType
        HealthConcern = 1
        Problem = 2
    End Enum

#Region "Constructor"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, Optional ByVal GoalID As Long = 0)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        _GoalID = GoalID
        ' Add any initialization after the InitializeComponent() call.  
    End Sub
#End Region

    Private Sub frmPatientGoal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        txtGoalName.Focus()
       
        setGoalDT()
        chkGoalDate.Checked = False
        chkTargetDate.Checked = False
        loadGoalUnits()
        If _GoalID <> 0 Then
            loadGoal()
        End If
        dtpGoalDate.Enabled = chkGoalDate.Checked
        dtpGoalTargetDate.Enabled = chkTargetDate.Checked

        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

    End Sub

    Private Sub setGoalDT()
        'Table used for CP_TVPHealthConcernAssociation TVP parameter values
        dtGoalAssociationTVP = New DataTable()
        dtGoalAssociationTVP.Columns.Add("nGoalAssociationID")
        dtGoalAssociationTVP.Columns.Add("nGoalID")
        dtGoalAssociationTVP.Columns.Add("nAssociationID")
        dtGoalAssociationTVP.Columns.Add("nAssociationType")
        dtGoalAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtGoalAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub loadGoalUnits()
        Dim dtLoincUnits As DataTable = Nothing
        Dim objPatientCarePlan As New ClsPatientCarePlan()
        dtLoincUnits = objPatientCarePlan.GetUnits()
        If Not IsNothing(dtLoincUnits) Then
            cmbGoalUnit.DataSource = dtLoincUnits
            cmbGoalUnit.DisplayMember = dtLoincUnits.Columns("UCUMUnits").ToString()
            cmbGoalUnit.ValueMember = ""
        End If
    End Sub

    Private Sub loadGoal()
        Try
            Dim dsGoalDetails As DataSet = Nothing
            Using objPatientGoal As New ClsGoal()
                dsGoalDetails = objPatientGoal.GetPatientGoal(_PatientID, _GoalID)
            End Using

            If dsGoalDetails IsNot Nothing Then
                If dsGoalDetails.Tables("GoalDetail").Rows.Count > 0 Then
                    _VisitID = dsGoalDetails.Tables("GoalDetail").Rows(0)("nVisitId")
                    txtGoalName.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("sGoalName")
                    txtGoalLoinc.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("sGoalLoin")
                    txtGoalValue.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("sGoalValue")

                    'Health concern auther
                    Dim sConcernFrom As String = dsGoalDetails.Tables("GoalDetail").Rows(0)("sGoalAuthor")
                    If sConcernFrom = "Provider" Then
                        rbt_FromProvider.Checked = True
                    ElseIf sConcernFrom = "Patient" Then
                        rbt_FromPatient.Checked = True
                    ElseIf sConcernFrom = "Both" Then
                        rbt_FromBoth.Checked = True
                    End If

                    cmbGoalUnit.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("sGoalUnit")
                    txtGoalNotes.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("sGoalNotes")

                    If IsDBNull(dsGoalDetails.Tables("GoalDetail").Rows(0)("dtGoalDate")) Then
                        chkGoalDate.Checked = False
                    Else
                        chkGoalDate.Checked = True
                        dtpGoalDate.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("dtGoalDate")
                    End If
                    If IsDBNull(dsGoalDetails.Tables("GoalDetail").Rows(0)("dtGoalTargetDate")) Then
                        chkTargetDate.Checked = False
                    Else
                        chkTargetDate.Checked = True
                        dtpGoalTargetDate.Text = dsGoalDetails.Tables("GoalDetail").Rows(0)("dtGoalTargetDate")
                    End If
                End If

                If Not dsGoalDetails.Tables("GoalAssociation") Is Nothing Then
                    Dim oProblemTable As New DataTable()
                    oProblemTable.Columns.Add("ID")
                    oProblemTable.Columns.Add("DispName")

                    Dim oHealthConcernTable As New DataTable()
                    oHealthConcernTable.Columns.Add("ID")
                    oHealthConcernTable.Columns.Add("DispName")

                    Dim AssociationType As Int16 = 0
                    dtGoalAssociationTVP.Rows.Clear()
                    For h As Int32 = 0 To dsGoalDetails.Tables("GoalAssociation").Rows.Count - 1
                        dtGoalAssociationTVP.Rows.Add()
                        dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalAssociationID") = dsGoalDetails.Tables("GoalAssociation").Rows(h)("nGoalAssociationID")
                        dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalID") = _GoalID
                        dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nAssociationID") = dsGoalDetails.Tables("GoalAssociation").Rows(h)("nAssociationID")
                        AssociationType = dsGoalDetails.Tables("GoalAssociation").Rows(h)("nAssociationType")
                        dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nAssociationType") = AssociationType
                        dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = dsGoalDetails.Tables("GoalAssociation").Rows(h)("dtTransactiondatetime")
                        dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("RowState") = "Retrieved"

                        Dim oRow As DataRow
                        If AssociationType = 1 Then
                            'Table for Health Concern combobox datasource
                            oRow = oHealthConcernTable.NewRow()
                            oRow(0) = dsGoalDetails.Tables("GoalAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsGoalDetails.Tables("GoalAssociation").Rows(h)("sAssociationDesc")
                            oHealthConcernTable.Rows.Add(oRow)
                        ElseIf AssociationType = 2 Then
                            'Table for problem combobox datasource
                            oRow = oProblemTable.NewRow()
                            oRow(0) = dsGoalDetails.Tables("GoalAssociation").Rows(h)("nAssociationID")
                            oRow(1) = dsGoalDetails.Tables("GoalAssociation").Rows(h)("sAssociationDesc")
                            oProblemTable.Rows.Add(oRow)
                        End If
                    Next

                    If oProblemTable.Rows.Count > 0 Then
                        cmbProblem.DataSource = oProblemTable
                        cmbProblem.DisplayMember = "DispName"
                        cmbProblem.ValueMember = "ID"
                    End If

                    If oHealthConcernTable.Rows.Count > 0 Then
                        cmbHealthConcern.DataSource = oHealthConcernTable
                        cmbHealthConcern.DisplayMember = "DispName"
                        cmbHealthConcern.ValueMember = "ID"
                    End If
                End If
            End If
            If dsGoalDetails IsNot Nothing Then
                dsGoalDetails.Clear()
                dsGoalDetails.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_PatientInjuryDate_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientInjuryDate.ItemClicked
        Select Case e.ClickedItem.Name
            Case ts_btnOk.Name
                If ValidateEntries() Then
                    SaveGoal()
                End If
            Case ts_btnCancel.Name
                Me.Close()
        End Select
    End Sub

    Private Function ValidateEntries() As Boolean
        If String.IsNullOrWhiteSpace(txtGoalName.Text) Then
            MessageBox.Show("Please enter a name for goal.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtGoalName.Focus()
            Return False
        End If
        If chkGoalDate.Checked And chkTargetDate.Checked Then
            If dtpGoalTargetDate.Value.Date < dtpGoalDate.Value.Date Then
                MessageBox.Show("Target Date should be greater than or equal to Goal Date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpGoalTargetDate.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub SaveGoal()
        Dim objGoal As New ClsGoal()
        Try
            Dim sConcernFrom As String = String.Empty

            'Goal auther
            If rbt_FromProvider.Checked Then
                sConcernFrom = "Provider"
            ElseIf rbt_FromPatient.Checked Then
                sConcernFrom = "Patient"
            ElseIf rbt_FromBoth.Checked Then
                sConcernFrom = "Both"
            End If

            'Goal Loinc
            Dim goalLoincCode As String = ""
            Dim goalLoincDesc As String = ""
            Dim goalLoinc As String() = txtGoalLoinc.Text.Split(":")
            If goalLoinc.Length >= 2 Then
                goalLoincCode = goalLoinc(0)
                goalLoincDesc = goalLoinc(1)
            ElseIf goalLoinc.Length >= 1 Then
                goalLoincCode = goalLoinc(0)
            End If

            objGoal.nPatientId = _PatientID
            objGoal.nVisitId = _VisitID
            objGoal.sGoalName = txtGoalName.Text.Trim()
            objGoal.nGoalID = _GoalID
            objGoal.sGoalLoincCode = goalLoincCode.Trim()
            objGoal.sGoalLoincDescription = goalLoincDesc.Trim()
            objGoal.sGoalValue = txtGoalValue.Text.Trim()
            objGoal.sGoalUnit = cmbGoalUnit.Text
            objGoal.sGoalAuthor = sConcernFrom
            objGoal.sGoalNotes = txtGoalNotes.Text.Trim()
            objGoal.dtGoalDate = dtpGoalDate.Text
            objGoal.dtTargetDate = dtpGoalTargetDate.Text
            objGoal.bIsGoalDate = chkGoalDate.Checked
            objGoal.bIsTargetDate = chkTargetDate.Checked

            If bIsProbRefChanged Or bIsHCRefChanged Then
                fillTVPs()
            End If

            Dim sRowState As String = ""
            If _GoalID = 0 Then
                sRowState = "Added"
            Else
                sRowState = "Updated"
            End If


            Dim nReturnId As Int64 = objGoal.SaveGoal(dtGoalAssociationTVP, sRowState)

            If _GoalID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Add, "Patient Goal added", _PatientID, nReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Modify, "Patient Goal Modified", _PatientID, _GoalID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objGoal IsNot Nothing Then
                objGoal.Dispose()
                objGoal = Nothing
            End If
        End Try
    End Sub

    Private Sub fillTVPs()
        Try
            Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(cmbProblem.Items.Count + cmbHealthConcern.Items.Count)
            If bIsProbRefChanged Then
                If cmbProblem.Items.Count > 0 Then
                    For j As Integer = 0 To cmbProblem.Items.Count - 1
                        cmbProblem.Text = ""
                        cmbProblem.SelectedIndex = j

                        If Not checkAndUpdate(cmbProblem.SelectedValue, GoalAssociationType.Problem) Then
                            dtGoalAssociationTVP.Rows.Add()
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalAssociationID") = dtUniqueIds.Rows(j)(0)
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalID") = "0"
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbProblem.SelectedValue
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(GoalAssociationType.Problem)
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                        End If
                    Next
                End If
            End If

            If bIsHCRefChanged Then
                If cmbHealthConcern.Items.Count > 0 Then
                    Dim IdCnt As Int32 = cmbProblem.Items.Count
                    For j As Integer = 0 To cmbHealthConcern.Items.Count - 1
                        cmbHealthConcern.Text = ""
                        cmbHealthConcern.SelectedIndex = j

                        If Not checkAndUpdate(cmbHealthConcern.SelectedValue, GoalAssociationType.HealthConcern) Then
                            dtGoalAssociationTVP.Rows.Add()
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalAssociationID") = dtUniqueIds.Rows(IdCnt + j)(0)
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nGoalID") = "0"
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nAssociationID") = cmbHealthConcern.SelectedValue
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("nAssociationType") = Convert.ToInt32(GoalAssociationType.HealthConcern)
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                            dtGoalAssociationTVP.Rows(dtGoalAssociationTVP.Rows.Count - 1)("RowState") = "Added"
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

#Region "Radio Button Events"

    Private Sub rbt_FromProvider_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_FromProvider.CheckedChanged
        If rbt_FromProvider.Checked = True Then
            rbt_FromProvider.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_FromProvider.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_FromPatient_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_FromPatient.CheckedChanged
        If rbt_FromPatient.Checked = True Then
            rbt_FromPatient.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_FromPatient.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_FromBoth_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_FromBoth.CheckedChanged
        If rbt_FromBoth.Checked = True Then
            rbt_FromBoth.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_FromBoth.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

#End Region

    Private Sub btnClearProblems_Click(sender As System.Object, e As System.EventArgs) Handles btnClearProblems.Click
        cmbProblem.DataSource = Nothing
        cmbProblem.Items.Clear()
        markCurrRefAsDeleted(GoalAssociationType.Problem)
    End Sub

    Private Sub btn_LoadProblem_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadProblem.Click
        Try
            markCurrRefAsDeleted(GoalAssociationType.Problem)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CarePlanProblemList, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.CarePlanProblemList
            oListControl.PatientID = _PatientID
            oListControl.ControlHeader = "Problem List"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)

            If cmbProblem.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbProblem.Items.Count - 1
                    cmbProblem.SelectedIndex = i
                    cmbProblem.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbProblem.SelectedValue), cmbProblem.Text)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)
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

                cmbProblem.DataSource = Nothing
                cmbProblem.Items.Clear()
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

                    cmbProblem.DataSource = oBindTable
                    cmbProblem.DisplayMember = "DispName"
                    cmbProblem.ValueMember = "ID"
                End If
            Case gloListControl.gloListControlType.HealthConcern

                cmbHealthConcern.DataSource = Nothing
                cmbHealthConcern.Items.Clear()
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

                    cmbHealthConcern.DataSource = oBindTable
                    cmbHealthConcern.DisplayMember = "DispName"
                    cmbHealthConcern.ValueMember = "ID"
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearGoalLoinc_Click(sender As System.Object, e As System.EventArgs) Handles btnClearGoalLoinc.Click
        txtGoalLoinc.Text = ""
    End Sub

    Private Sub btn_LoadHealthConcern_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadHealthConcern.Click
        Try
            markCurrRefAsDeleted(GoalAssociationType.HealthConcern)
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.HealthConcern, True, Me.Width)
            _CurrentControlType = gloListControl.gloListControlType.HealthConcern
            oListControl.PatientID = _PatientID
            oListControl.ControlHeader = "Health Concern"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)

            If cmbHealthConcern.DataSource IsNot Nothing Then
                For i As Integer = 0 To cmbHealthConcern.Items.Count - 1
                    cmbHealthConcern.SelectedIndex = i
                    cmbHealthConcern.Refresh()
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbHealthConcern.SelectedValue), cmbHealthConcern.Text)
                Next
            End If

            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Health Concern"
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearHealthConcern_Click(sender As System.Object, e As System.EventArgs) Handles btnClearHealthConcern.Click
        cmbHealthConcern.DataSource = Nothing
        cmbHealthConcern.Items.Clear()
        markCurrRefAsDeleted(GoalAssociationType.HealthConcern)
    End Sub

    Private Sub markCurrRefAsDeleted(ByVal AssType As GoalAssociationType)
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)

            Select Case AssType
                Case GoalAssociationType.HealthConcern
                    bIsHCRefChanged = True
                Case GoalAssociationType.Problem
                    bIsProbRefChanged = True
            End Select

            If dtGoalAssociationTVP.Rows.Count > 0 Then
                dtGoalAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("nAssociationType") = nAssType).ToList().ForEach(Sub(r) r.SetField("RowState", "Deleted"))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Function checkAndUpdate(ByVal sAssociationId As String, ByVal AssType As GoalAssociationType) As Boolean
        Dim exists As Boolean = False
        Try
            Dim nAssType As Int32 = Convert.ToInt32(AssType)
            If dtGoalAssociationTVP.Rows.Count > 0 Then
                exists = dtGoalAssociationTVP.AsEnumerable().Where(Function(c) (c.Field(Of String)("nAssociationID") = sAssociationId) And (c.Field(Of String)("nAssociationType") = nAssType)).Count() > 0
                If exists Then
                    dtGoalAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) (r.Item("nAssociationID") = sAssociationId) And (r.Item("nAssociationType") = nAssType)).ToList().ForEach(Sub(r) r.SetField("RowState", "NoChange"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientGoal, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return exists
    End Function

    Private Sub chkTargetDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTargetDate.CheckedChanged
        dtpGoalTargetDate.Enabled = chkTargetDate.Checked
    End Sub

    Private Sub chkGoalDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGoalDate.CheckedChanged
        dtpGoalDate.Enabled = chkGoalDate.Checked
    End Sub
End Class