Public Class frmPatientCarePlan

    Dim _nPatientId As Int64
    Dim _nID As Int64 = 0
    Private oListControl As gloListControl.gloListControl
    Dim dtMasterData As New DataTable()

    Public Sub New(ByVal PatientId As Int64, ByVal nID As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _nPatientId = PatientId 'If incoming PatientId = zero then we will consider it as adding for care plan master table
        _nID = nID
    End Sub

    Private Sub frmPatientCarePlan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            If _nPatientId = 0 Then
                LoadFormForMasterEntry()
            Else
                LoadFormForPatientCarePlan()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ts_btnOk_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnOk.Click
        Try
            If (ValidateData()) Then
                If _nPatientId = 0 Then
                    SaveCarePlanMST()
                Else
                    SavePatientCarePlan()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnCancel.Click
        Me.Close()
    End Sub

    Private Function ValidateData() As Boolean
        Dim _IsValid As Boolean = True
        Try
            'If (txtInstruction.Text.Trim() = "") Then
            '    MessageBox.Show("Please enter instruction description. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    _IsValid = False
            '    Return _IsValid
            'Else
            If (txtProblem.Text.Trim() = "") Then
                MessageBox.Show("Please enter problem. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                Return _IsValid
                'ElseIf (txtGoal.Text.Trim() = "") Then
                '    MessageBox.Show("Please enter goal. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    _IsValid = False
                '    Return _IsValid
                'ElseIf (txtNotes.Text.Trim() = "") Then
                '    MessageBox.Show("Please enter note. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    _IsValid = False
                '    Return _IsValid
                'ElseIf (txtInstruction.Text.Trim() = "") Then
                '    MessageBox.Show("Please enter instruction. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    _IsValid = False
                '    Return _IsValid
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return _IsValid
    End Function

    Private Sub SavePatientCarePlan()

        Try
            Dim _ReturnId As Int64

            Using objPatientCarePlan As New ClsPatientCarePlan()
                _ReturnId = objPatientCarePlan.SavePatientCarePlan(_nID, _nPatientId, txtProblem.Text.Trim(), txtGoal.Text.Trim(), txtNotes.Text.Trim(), txtInstruction.Text.Trim())
            End Using

            If _nID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Care Plan added", _nPatientId, _ReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Patient Care Plan Modified", _nPatientId, _nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            If (_ReturnId = 99999) Then
                MessageBox.Show("Clinical Instruction present for same date. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LoadFormForMasterEntry()
        Dim objCarePlan As New ClsCarePlanMST()
        Dim dtData As DataTable = Nothing
        Try
            btnMst.Visible = False
            If _nID > 0 Then
                dtData = objCarePlan.GetCarePlanMST(_nID)

                If (Not IsNothing(dtData)) Then
                    If (dtData.Rows.Count > 0) Then
                        _nID = Convert.ToInt64(dtData.Rows(0)("nId").ToString())
                        txtProblem.Text = dtData.Rows(0)("sProblem").ToString()
                        txtGoal.Text = dtData.Rows(0)("sGoal").ToString()
                        txtNotes.Text = dtData.Rows(0)("sNote").ToString()
                        txtInstruction.Text = dtData.Rows(0)("sInstruction").ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
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

    Private Sub LoadFormForPatientCarePlan()
        Try
            Dim dcId As New DataColumn("Id")
            Dim dcProblem As New DataColumn("Problem")

            dtMasterData.Columns.Add(dcId)
            dtMasterData.Columns.Add(dcProblem)
            dtMasterData.PrimaryKey = {dcId}

            If _nID > 0 Then
                Dim dtData As DataTable

                Using objPatientCarePlan As New ClsPatientCarePlan()

                    dtData = objPatientCarePlan.GetPatientCarePlan(_nPatientId, _nID)

                End Using


                If (Not IsNothing(dtData)) Then
                    If (dtData.Rows.Count > 0) Then
                        _nID = Convert.ToInt64(dtData.Rows(0)("nId").ToString())
                        txtProblem.Text = dtData.Rows(0)("sProblem").ToString()
                        txtGoal.Text = dtData.Rows(0)("sGoal").ToString()
                        txtNotes.Text = dtData.Rows(0)("sNote").ToString()
                        txtInstruction.Text = dtData.Rows(0)("sInstruction").ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveCarePlanMST()
        Dim objCarePlan As New ClsCarePlanMST()
        Try
            Dim _ReturnId As Int64

            _ReturnId = objCarePlan.SaveCarePlanMST(_nID, txtProblem.Text.Trim(), txtGoal.Text.Trim(), txtNotes.Text.Trim(), txtInstruction.Text.Trim())

            If _nID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Care Plan added", _nPatientId, _ReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Patient Care Plan Modified", _nPatientId, _nID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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

    Private Sub btnMst_Click(sender As System.Object, e As System.EventArgs) Handles btnMst.Click
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


            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CarePlan, True, Me.Width)
            oListControl.ControlHeader = "Care Plan"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            If dtMasterData IsNot Nothing AndAlso dtMasterData.Rows.Count > 0 Then
                For i As Integer = 0 To dtMasterData.Rows.Count - 1
                    oListControl.SelectedItems.Add(Convert.ToInt64(dtMasterData.Rows(i)("Id")), "", dtMasterData.Rows(i)("Problem").ToString())
                Next
            End If


            Me.Controls.Add(oListControl)
            oListControl.OpenControl()
            pnlMiddle.Visible = False
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
            pnlMiddle.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub oListControl_SelectedClick(sender As Object, e As EventArgs)
        Dim objCarePlan As New ClsCarePlanMST()
        Dim dtData As DataTable = Nothing
        Try

            If oListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oListControl.SelectedItems.Count - 1

                    Dim key As Object = oListControl.SelectedItems(i).ID
                    If dtMasterData.Rows.Find(key) Is Nothing Then

                        Dim drTemp As DataRow = dtMasterData.NewRow()
                        drTemp("Id") = oListControl.SelectedItems(i).ID
                        drTemp("Problem") = oListControl.SelectedItems(i).Description
                        dtMasterData.Rows.Add(drTemp)

                        Dim nID As Long = Convert.ToInt64(key)
                        If nID > 0 Then
                            dtData = objCarePlan.GetCarePlanMST(nID)
                            If (Not IsNothing(dtData)) Then
                                If (dtData.Rows.Count > 0) Then
                                    If txtProblem.Text.Trim() <> "" Then
                                        txtProblem.Text = txtProblem.Text + "," + dtData.Rows(0)("sProblem").ToString()
                                    Else
                                        txtProblem.Text = dtData.Rows(0)("sProblem").ToString()
                                    End If

                                    If txtGoal.Text.Trim() <> "" Then
                                        txtGoal.Text = txtGoal.Text + Environment.NewLine + dtData.Rows(0)("sGoal").ToString()
                                    Else
                                        txtGoal.Text = dtData.Rows(0)("sGoal").ToString()
                                    End If

                                    If txtNotes.Text.Trim() <> "" Then
                                        txtNotes.Text = txtNotes.Text + Environment.NewLine + dtData.Rows(0)("sNote").ToString()
                                    Else
                                        txtNotes.Text = dtData.Rows(0)("sNote").ToString()
                                    End If

                                    If txtInstruction.Text.Trim() <> "" Then
                                        txtInstruction.Text = txtInstruction.Text + Environment.NewLine + dtData.Rows(0)("sInstruction").ToString()
                                    Else
                                        txtInstruction.Text = dtData.Rows(0)("sInstruction").ToString()
                                    End If

                                End If
                            End If
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlMiddle.Visible = True
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
End Class