Imports System.Data.SqlClient

Public Class frmTreatmentPlan_View

    Dim nPlanOfTreatmentID As Int64 = 0

    Private Const Col_PlanOfTreatmentID As Integer = 0
    Private Const Col_PatientID As Integer = 1
    Private Const Col_Title As Integer = 2
    Private Const Col_Assesment As Integer = 3
    Private Const Col_EffectiveStartDate As Integer = 4
    Private Const Col_EffectiveEndDate As Integer = 5
    Private Const Col_IsActive As Integer = 6
    Private Const Col_IsDeleted As Integer = 7
    Private Const Col_UserName As Integer = 8
    Private Const COL_ExamID As Integer = 9
    Private Const Col_CreatedOn As Integer = 10

    Public Enum ActiveStatus
        InActive = 0
        Active = 1
    End Enum
    Private nPatientID As Long


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(gnPatientID As Long)
        ' TODO: Complete member initialization 
        InitializeComponent()
        nPatientID = gnPatientID
    End Sub


    Private Sub frmTreatmentPlan_View_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            gloC1FlexStyle.Style(C1TreatmentPlan)
            Designgrid()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Designgrid()
        Try
            Dim dt As DataTable

            txtSearch.Focus()
            Dim oTreatment As New clsTreatmentPlan()

            dt = oTreatment.GetTreatmentPlans(0, nPatientID)
            If Not IsNothing(oTreatment) Then
                oTreatment.Dispose()
                oTreatment = Nothing
            End If

            C1TreatmentPlan.DataSource = Nothing
            If dt IsNot Nothing Then

                Dim _dv As DataView = dt.Copy().DefaultView
                C1TreatmentPlan.Visible = True

                C1TreatmentPlan.DataSource = _dv
                C1TreatmentPlan.Rows.Fixed = 1


                C1TreatmentPlan.Cols(Col_PlanOfTreatmentID).Caption = "PlanOfTreatmentID"
                C1TreatmentPlan.Cols(Col_PatientID).Caption = "PatientID"
                C1TreatmentPlan.Cols(Col_Title).Caption = "Title"
                C1TreatmentPlan.Cols(Col_Assesment).Caption = "Assessment"
                C1TreatmentPlan.Cols(Col_EffectiveStartDate).Caption = "Effective Start Date"
                C1TreatmentPlan.Cols(Col_EffectiveEndDate).Caption = "Effective End Date"
                C1TreatmentPlan.Cols(Col_IsActive).Caption = "Status"
                C1TreatmentPlan.Cols(Col_IsDeleted).Caption = "Deleted"
                C1TreatmentPlan.Cols(Col_UserName).Caption = "User Name"
                C1TreatmentPlan.Cols(Col_CreatedOn).Caption = "Created On"

                C1TreatmentPlan.Cols(Col_PlanOfTreatmentID).Visible = False
                C1TreatmentPlan.Cols(Col_PatientID).Visible = False
                C1TreatmentPlan.Cols(Col_Title).Visible = True
                C1TreatmentPlan.Cols(Col_Assesment).Visible = True
                C1TreatmentPlan.Cols(Col_EffectiveStartDate).Visible = True
                C1TreatmentPlan.Cols(Col_EffectiveEndDate).Visible = True
                C1TreatmentPlan.Cols(Col_IsActive).Visible = True
                C1TreatmentPlan.Cols(Col_IsDeleted).Visible = False
                C1TreatmentPlan.Cols(Col_UserName).Visible = True
                C1TreatmentPlan.Cols(Col_CreatedOn).Visible = True
                C1TreatmentPlan.Cols(COL_ExamID).Visible = False

                ''C1TaxID.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_PlanOfTreatmentID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_PatientID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_Title).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_Assesment).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_EffectiveStartDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_EffectiveEndDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_IsActive).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_IsDeleted).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_UserName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TreatmentPlan.Cols(Col_CreatedOn).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                Dim nWidth As Integer = Pnl_grid.Width

                C1TreatmentPlan.Cols(Col_PlanOfTreatmentID).Width = 0
                C1TreatmentPlan.Cols(Col_PatientID).Width = 0
                C1TreatmentPlan.Cols(Col_Title).Width = CInt((0.15 * (nWidth)))
                C1TreatmentPlan.Cols(Col_Assesment).Width = CInt((0.34 * (nWidth)))
                C1TreatmentPlan.Cols(Col_EffectiveStartDate).Width = CInt((0.13 * (nWidth)))
                C1TreatmentPlan.Cols(Col_EffectiveEndDate).Width = CInt((0.13 * (nWidth)))
                C1TreatmentPlan.Cols(Col_IsActive).Width = CInt((0.07 * (nWidth)))
                C1TreatmentPlan.Cols(Col_IsDeleted).Width = CInt((0.1 * (nWidth)))
                C1TreatmentPlan.Cols(Col_UserName).Width = CInt((0.07 * (nWidth)))
                C1TreatmentPlan.Cols(Col_CreatedOn).Width = CInt((0.1 * (nWidth)))

                C1TreatmentPlan.Cols(Col_EffectiveStartDate).DataType = GetType(DateTime)
                C1TreatmentPlan.Cols(Col_EffectiveStartDate).Format = "MM/dd/yyyy hh:mm:ss tt"
                C1TreatmentPlan.Cols(Col_EffectiveEndDate).DataType = GetType(DateTime)
                C1TreatmentPlan.Cols(Col_EffectiveEndDate).Format = "MM/dd/yyyy hh:mm:ss tt"
                C1TreatmentPlan.Cols(Col_CreatedOn).DataType = GetType(DateTime)
                C1TreatmentPlan.Cols(Col_CreatedOn).Format = "MM/dd/yyyy"
                C1TreatmentPlan.Cols(Col_IsActive).DataType = GetType(String)
                C1TreatmentPlan.Cols(Col_IsDeleted).DataType = GetType(String)

                ''C1TaxID.ShowCellLabels = True
                C1TreatmentPlan.AllowEditing = False

                dt.Dispose()
                dt = Nothing
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try

            Select Case e.ClickedItem.Tag
                Case "Add"
                    Try

                        Me.Cursor = Cursors.WaitCursor

                        If Not IsActivePOT(nPatientID) Then
                            Dim oPlanOfTreatment As New frmTreatmentPlan(nPatientID)
                            oPlanOfTreatment.ShowInTaskbar = False
                            oPlanOfTreatment.ShowDialog(Me)
                        Else
                            MessageBox.Show("Patient can have only one active Plan of Treatement. To create new plan first de-activate any active plan.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Designgrid()


                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Plan of Treatment not created due to ERROR.", gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case "Modify"
                    ModifyTreatmentPlan()
                Case "Delete"
                    If (C1TreatmentPlan.Rows.Count > 1 AndAlso C1TreatmentPlan.RowSel > 0) Then
                        nPlanOfTreatmentID = Convert.ToInt64(C1TreatmentPlan.GetData(C1TreatmentPlan.RowSel, Col_PlanOfTreatmentID))
                        nPatientID = Convert.ToString(C1TreatmentPlan.GetData(C1TreatmentPlan.RowSel, Col_PatientID))
                        'If MessageBox.Show("Are you sure you want to delete this record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        '    DeleteTaxID(nPlanOfTreatmentID, nPatientID)
                        '    Designgrid()
                        'End If
                    End If
                Case "Refresh"
                    Me.Cursor = Cursors.WaitCursor
                    Designgrid()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Query, gloAuditTrail.ActivityType.Query, "Plan of Treatment List Refreshed.", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                Case "Close"
                    Me.Close()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Plan of Treatment List Closed.", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ModifyTreatmentPlan()
        Try

            Me.Cursor = Cursors.WaitCursor
            If (C1TreatmentPlan.Rows.Count > 1) Then
                nPlanOfTreatmentID = Convert.ToInt64(C1TreatmentPlan.GetData(C1TreatmentPlan.RowSel, Col_PlanOfTreatmentID))
                nPatientID = Convert.ToString(C1TreatmentPlan.GetData(C1TreatmentPlan.RowSel, Col_PatientID))
                Dim oTreatmetnPlan As New frmTreatmentPlan(nPatientID, nPlanOfTreatmentID)
                oTreatmetnPlan.ShowInTaskbar = False
                oTreatmetnPlan.ShowDialog(Me)
                Designgrid()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Plan of Treatment Modified ", nPatientID, nPlanOfTreatmentID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteTaxID(nPlanOfTreatmentID As Long, nPatientID As Long)
        Throw New NotImplementedException
    End Sub

    Private Function IsActivePOT(nPatientID As Long) As Boolean
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim oResult As Object

        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "SELECT count(*) FROM dbo.POT_MST AS PM	WITH (NOLOCK) WHERE  [Type]=1 AND  PM.bIsActive =1 AND  PM.nPatientID = " & nPatientID

            objCmd.Connection = objCon
            oResult = objCmd.ExecuteScalar()
            If oResult IsNot Nothing AndAlso Convert.ToInt64(oResult) > 0 Then
                Return True
            Else
                Return False
            End If


        Catch gex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), True)
            Return False
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
    End Function

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.Clear()
        txtSearch.Focus()
    End Sub

    Private Sub tsbtn_Act_Deact_Rule_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_Act_Deact_Rule.Click
        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
            If IsActivePOT(nPatientID) Then
                MessageBox.Show("Patient can have only one active Plan of Treatement. To activate this plan first de-activate the active plan.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                PerformActivateDeActivate()
            End If
        Else
            PerformActivateDeActivate()
        End If

    End Sub
    Private Sub PerformActivateDeActivate()

        Dim _SelectedlPOTID As Int64 = 0
        Dim _SelectedPatientID As String = ""
        Dim _nSelectedRowIndex As Integer = -1
        Dim _sActivationDeActivationNote As String = ""

        Try
            If Not IsNothing(C1TreatmentPlan) AndAlso C1TreatmentPlan.Rows.Count > 0 AndAlso C1TreatmentPlan.RowSel > 0 Then
                With C1TreatmentPlan
                    _SelectedlPOTID = .GetData(.RowSel, Col_PlanOfTreatmentID)
                    _SelectedPatientID = .GetData(.RowSel, Col_PatientID)
                    If _SelectedlPOTID > 0 Then

                        If tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then
                            If Not MessageBox.Show("Are you sure you want to de-activate the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If



                        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                            If Not MessageBox.Show("Are you sure you want to activate the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If


                        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                            If UpdateActiveStatus(_SelectedlPOTID, _SelectedPatientID, ActiveStatus.Active.GetHashCode()) > 0 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Activate, "Plan of Treatment successfully Activated.", nPatientID, _SelectedlPOTID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If

                        Else
                            If UpdateActiveStatus(_SelectedlPOTID, _SelectedPatientID, ActiveStatus.InActive.GetHashCode()) > 0 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.DeActivate, "Plan of Treatment successfully De-activated.", nPatientID, _SelectedlPOTID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If

                        End If
                        Designgrid()
                        ''_nSelectedRowIndex = .RowSel
                        _nSelectedRowIndex = .FindRow(_SelectedlPOTID.ToString(), 0, 0, False, False, False)

                        If (_nSelectedRowIndex <> -1 And _nSelectedRowIndex > 0) Then
                            .RowSel = _nSelectedRowIndex
                            .Select(_nSelectedRowIndex, 0)
                        End If
                    End If

                End With

                'If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then

                '    tsbtn_Act_Deact_Rule.Text = "De-&activate"
                '    tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                '    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate

                'ElseIf tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then

                '    tsbtn_Act_Deact_Rule.Text = "&Activate"
                '    tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                '    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate

                'End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Function UpdateActiveStatus(ByVal Id As Int64, ByVal sTaxID As String, ByVal ActiveStatus As Integer) As Integer

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim nResult As Integer = 0
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "UPDATE [dbo].[POT_MST] SET [bIsActive] = " & ActiveStatus & "  ,[nLoginSessionID] = dbo.Get_AuditSessionID() ,[dtCreatedOn] = dbo.gloGetDate() WHERE [dbo].[POT_MST].nPlanOfTreatmentID =" & Id & ""
            objCmd.Connection = objCon
            nResult = objCmd.ExecuteNonQuery()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Plan of Treatment '" & Id & "' successfully activated", 0, Id, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            nResult = 0
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
        Return nResult
    End Function

    Private Sub C1TreatmentPlan_RowColChange(sender As System.Object, e As System.EventArgs) Handles C1TreatmentPlan.RowColChange
        Dim _sStatus As String
        If C1TreatmentPlan.RowSel > 0 Then
            If IsNothing(C1TreatmentPlan.GetData(C1TreatmentPlan.RowSel, Col_IsActive)) = False Then
                _sStatus = C1TreatmentPlan.GetData(C1TreatmentPlan.RowSel, Col_IsActive).ToString()
                If _sStatus.ToUpper() = "ACTIVE" Then
                    tsbtn_Act_Deact_Rule.Text = "De-&activate"
                    tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
                Else
                    tsbtn_Act_Deact_Rule.Text = "&Activate"
                    tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
                End If
            End If
        End If
    End Sub

    Private Sub Pnl_grid_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pnl_grid.Resize
        Dim nWidth As Integer = Pnl_grid.Width
        C1TreatmentPlan.Cols.Count = 11
        C1TreatmentPlan.Cols(Col_PlanOfTreatmentID).Width = 0
        C1TreatmentPlan.Cols(Col_PatientID).Width = CInt((0.2 * (nWidth)))
        C1TreatmentPlan.Cols(Col_Title).Width = CInt((0.15 * (nWidth)))
        C1TreatmentPlan.Cols(Col_Assesment).Width = CInt((0.34 * (nWidth)))
        C1TreatmentPlan.Cols(Col_EffectiveStartDate).Width = CInt((0.1 * (nWidth)))
        C1TreatmentPlan.Cols(Col_EffectiveEndDate).Width = CInt((0.1 * (nWidth)))
        C1TreatmentPlan.Cols(Col_IsActive).Width = CInt((0.1 * (nWidth)))
        C1TreatmentPlan.Cols(Col_IsDeleted).Width = CInt((0.1 * (nWidth)))
        C1TreatmentPlan.Cols(Col_UserName).Width = CInt((0.1 * (nWidth)))
        C1TreatmentPlan.Cols(COL_ExamID).Width = CInt((0.1 * (nWidth)))
        C1TreatmentPlan.Cols(Col_CreatedOn).Width = CInt((0.1 * (nWidth)))

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As DataView = DirectCast(C1TreatmentPlan.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            Try
                Dim strSearch As String = txtSearch.Text.Trim()

                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")


                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns(Col_Title).ColumnName & " Like '%") + strSearch & "%'"
                Else
                    _dv.RowFilter = (_dv.Table.Columns(Col_Title).ColumnName & " Like '%") + strSearch & "%' OR " _
                            & (_dv.Table.Columns(Col_Assesment).ColumnName & " Like '%") + strSearch & "%'"

                End If
                C1TreatmentPlan.DataSource = _dv
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Searched Tax ID criteria having substring  " & txtSearch.Text.Trim, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Catch Ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PlanOfTreatment, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Could not search Tax ID criteria having substring  " & txtSearch.Text.Trim, gloAuditTrail.ActivityOutCome.Failure)

                MessageBox.Show(Ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub C1TreatmentPlan_DoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1TreatmentPlan.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1TreatmentPlan.HitTest(ptPoint)
        With C1TreatmentPlan
            If .Rows.Count > 1 And htInfo.Row > 0 Then
                ModifyTreatmentPlan()
            End If
        End With

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub C1TreatmentPlan_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1TreatmentPlan.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class