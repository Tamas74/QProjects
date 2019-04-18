Public Class frmCarePlanHistory
    Dim _nPatientId As Int64
    Dim _nId As Int64
    Dim _sModule As String
    Dim dsHistory As DataSet
    Dim dtHistory As DataTable
    Dim dtAssociationHistory As DataTable

    Public Sub New(ByVal PatientID As Long, ByVal nID As Long, ByVal sModule As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID
        _nId = nID
        _sModule = sModule
    End Sub

    Private Sub frmCarePlanHistory_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCarePlanHistory))
        'Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Icon = Icon.Clone()
        LoadHistory()
    End Sub

    Private Sub LoadHistory()
        Try
            dsHistory = Nothing
            Using objCarePlan As New ClsCarePlan_V2()
                dsHistory = objCarePlan.GetCarePlanHistory_V2(_nId, _sModule)
            End Using

            If dsHistory IsNot Nothing Then
                If dsHistory.Tables("CarePlanHisory") IsNot Nothing Then
                    C1_CarePlanHistory.Enabled = False
                    C1_CarePlanHistory.DataSource = Nothing
                    C1_CarePlanHistory.Clear()
                    C1_CarePlanHistory.DataSource = dsHistory.Tables("CarePlanHisory").DefaultView
                    DesignGrid()
                    If C1_CarePlanHistory.Rows.Count > 0 Then
                        C1_CarePlanHistory.Row = 1
                    End If
                    'C1_CarePlanHistory.
                    C1_CarePlanHistory.Enabled = True
                    associateSelectedRow()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub loadAssociation(ByVal sRecordedDate As String)
        Try
            If dsHistory IsNot Nothing Then
                If dsHistory.Tables("AssociationHistory") IsNot Nothing Then
                    C1_AssociationHisotry.DataSource = Nothing

                    Dim bs As New BindingSource
                    bs.DataSource = dsHistory.Tables("AssociationHistory")

                    bs.Filter = "Convert(dtRecordedDate,System.String) ='" & sRecordedDate & "'"

                    'Dim dt As DataTable = dsHistory.Tables("AssociationHistory")
                    'Dim dv As DataView = New DataView()
                    'dv = dt.DefaultView()
                    'dv.RowFilter = "Convert(dtRecordedDate,System.String) ='" & sRecordedDate & "'"
                    ''dv.RowFilter = "nAssociationType ='Problem'"
                    C1_AssociationHisotry.DataSource = bs
                    DesignAssociationGrid()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignGrid()
        Try
            Select Case _sModule
                Case "HealthConcern"
                    lblMst.Text = "Health Concern History"
                    DesignHealthConcernGrid()
                Case "Goal"
                    lblMst.Text = "Goal History"
                    DesigGoalGrid()
                Case "Intervention"
                    lblMst.Text = "Intervention History"
                    DesignInterventionGrid()
                Case "Outcome"
                    lblMst.Text = "Outcome History"
                    DesignOutcomeGrid()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignHealthConcernGrid()
        Try
            gloC1FlexStyle.Style(C1_CarePlanHistory)
            C1_CarePlanHistory.Rows.Fixed = 1


            C1_CarePlanHistory.Cols("sHealthConcernName").Caption = "Concern Name"
            C1_CarePlanHistory.Cols("sHealthStatus").Caption = "Health Status"
            C1_CarePlanHistory.Cols("sHealthStatusDescription").Caption = "Description"
            C1_CarePlanHistory.Cols("sHealthConcernAuthor").Caption = "From"
            C1_CarePlanHistory.Cols("sHealthConcernStatus").Caption = "Status"
            C1_CarePlanHistory.Cols("dtHealthConcernStartDate").Caption = "Start Date"
            C1_CarePlanHistory.Cols("dtHealthConcernStartDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("dtHealthConcernEndDate").Caption = "End Date"
            C1_CarePlanHistory.Cols("dtHealthConcernEndDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("sHealthConcernNotes").Caption = "Notes"
            C1_CarePlanHistory.Cols("dtRecordedDate").Caption = "Recorded Date"
            C1_CarePlanHistory.Cols("dtRecordedDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("sUsername").Caption = "User"
            C1_CarePlanHistory.Cols("dtHealthConcernDate").Caption = "Concern Date"
            C1_CarePlanHistory.Cols("RowState").Caption = "Action"

            C1_CarePlanHistory.Cols("dtHealthConcernDate").Visible = False
            C1_CarePlanHistory.Cols("nVisitID").Visible = False
            C1_CarePlanHistory.Cols("nUserId").Visible = False

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 40
            C1_CarePlanHistory.Cols("sHealthConcernName").Width = wdth * 0.11
            C1_CarePlanHistory.Cols("sHealthStatus").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("sHealthStatusDescription").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("sHealthConcernAuthor").Width = wdth * 0.06
            C1_CarePlanHistory.Cols("sHealthConcernStatus").Width = wdth * 0.06
            C1_CarePlanHistory.Cols("dtHealthConcernStartDate").Width = wdth * 0.11
            C1_CarePlanHistory.Cols("dtHealthConcernEndDate").Width = wdth * 0.11
            C1_CarePlanHistory.Cols("sHealthConcernNotes").Width = wdth * 0.12
            C1_CarePlanHistory.Cols("sUsername").Width = wdth * 0.06
            C1_CarePlanHistory.Cols("RowState").Width = wdth * 0.06
            C1_CarePlanHistory.Cols("dtRecordedDate").Width = wdth * 0.11

            'C1_CarePlanHistory.ShowCellLabels = True
            C1_CarePlanHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_CarePlanHistory.Redraw = True
            C1_CarePlanHistory.AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesigGoalGrid()
        Try
            gloC1FlexStyle.Style(C1_CarePlanHistory)
            C1_CarePlanHistory.Rows.Fixed = 1


            C1_CarePlanHistory.Cols("sGoalName").Caption = "Goal Name"
            C1_CarePlanHistory.Cols("sGoalLoin").Caption = "LOINC"
            C1_CarePlanHistory.Cols("sGoalValue").Caption = "Value"
            C1_CarePlanHistory.Cols("sGoalUnit").Caption = "Unit"
            C1_CarePlanHistory.Cols("sGoalAuthor").Caption = "Goal From"
            C1_CarePlanHistory.Cols("sGoalNotes").Caption = "Notes"
            C1_CarePlanHistory.Cols("dtGoalDate").Caption = "Goal Date"
            C1_CarePlanHistory.Cols("dtGoalDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("dtGoalTargetDate").Caption = "Target Date"
            C1_CarePlanHistory.Cols("dtGoalTargetDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("sUsername").Caption = "User"
            C1_CarePlanHistory.Cols("dtRecordedDate").Caption = "Recorded Date"
            C1_CarePlanHistory.Cols("dtRecordedDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("RowState").Caption = "Action"

            C1_CarePlanHistory.Cols("nVisitID").Visible = False
            C1_CarePlanHistory.Cols("nUserId").Visible = False

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 40
            C1_CarePlanHistory.Cols("sGoalName").Width = wdth * 0.11
            C1_CarePlanHistory.Cols("sGoalLoin").Width = wdth * 0.12
            C1_CarePlanHistory.Cols("sGoalValue").Width = wdth * 0.05
            C1_CarePlanHistory.Cols("sGoalUnit").Width = wdth * 0.05
            C1_CarePlanHistory.Cols("sGoalAuthor").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("sGoalNotes").Width = wdth * 0.14
            C1_CarePlanHistory.Cols("dtGoalDate").Width = wdth * 0.11
            C1_CarePlanHistory.Cols("dtGoalTargetDate").Width = wdth * 0.11
            C1_CarePlanHistory.Cols("sUsername").Width = wdth * 0.05
            C1_CarePlanHistory.Cols("RowState").Width = wdth * 0.05
            C1_CarePlanHistory.Cols("dtRecordedDate").Width = wdth * 0.11

            'C1_CarePlanHistory.ShowCellLabels = True
            C1_CarePlanHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_CarePlanHistory.Redraw = True
            C1_CarePlanHistory.AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignInterventionGrid()
        Try
            gloC1FlexStyle.Style(C1_CarePlanHistory)
            C1_CarePlanHistory.Rows.Fixed = 1

            C1_CarePlanHistory.Cols("sInterventionName").Caption = "Intervention Name"
            C1_CarePlanHistory.Cols("sInterventionType").Caption = "Intervention Type"
            C1_CarePlanHistory.Cols("sInterventionNotes").Caption = "Notes"
            C1_CarePlanHistory.Cols("dtRecordedDate").Caption = "Recorded Date"
            C1_CarePlanHistory.Cols("dtRecordedDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("sUsername").Caption = "User"
            C1_CarePlanHistory.Cols("dtInterventionDate").Caption = "Intervention Date"
            C1_CarePlanHistory.Cols("dtInterventionDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("RowState").Caption = "Action"

            C1_CarePlanHistory.Cols("nPlanOfTreatmentID").Visible = False
            C1_CarePlanHistory.Cols("nVisitID").Visible = False
            C1_CarePlanHistory.Cols("nUserId").Visible = False

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 40
            C1_CarePlanHistory.Cols("sInterventionName").Width = wdth * 0.17
            C1_CarePlanHistory.Cols("sInterventionType").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("sInterventionNotes").Width = wdth * 0.23
            C1_CarePlanHistory.Cols("dtRecordedDate").Width = wdth * 0.15
            C1_CarePlanHistory.Cols("sUsername").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("dtInterventionDate").Width = wdth * 0.15
            C1_CarePlanHistory.Cols("RowState").Width = wdth * 0.1

            'C1_CarePlanHistory.ShowCellLabels = True
            C1_CarePlanHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_CarePlanHistory.Redraw = True
            C1_CarePlanHistory.AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignOutcomeGrid()
        Try
            gloC1FlexStyle.Style(C1_CarePlanHistory)
            C1_CarePlanHistory.Rows.Fixed = 1

            C1_CarePlanHistory.Cols("sOutcomeName").Caption = "Outcome Name"
            C1_CarePlanHistory.Cols("sOutcomeStatus").Caption = "Status"
            C1_CarePlanHistory.Cols("sOutcomeNotes").Caption = "Notes"
            C1_CarePlanHistory.Cols("svalue").Caption = "Value"
            C1_CarePlanHistory.Cols("sunit").Caption = "Unit"
            C1_CarePlanHistory.Cols("dtRecordedDate").Caption = "Recorded Date"
            C1_CarePlanHistory.Cols("dtRecordedDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("sUsername").Caption = "User"
            C1_CarePlanHistory.Cols("dtOutcomeDate").Caption = "Outcome Date"
            C1_CarePlanHistory.Cols("dtOutcomeDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_CarePlanHistory.Cols("RowState").Caption = "Action"

            C1_CarePlanHistory.Cols("nVisitID").Visible = False
            C1_CarePlanHistory.Cols("nUserId").Visible = False

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 40
            C1_CarePlanHistory.Cols("sOutcomeName").Width = wdth * 0.15
            C1_CarePlanHistory.Cols("sOutcomeStatus").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("sOutcomeNotes").Width = wdth * 0.19
            C1_CarePlanHistory.Cols("svalue").Width = wdth * 0.05
            C1_CarePlanHistory.Cols("sunit").Width = wdth * 0.05
            C1_CarePlanHistory.Cols("dtRecordedDate").Width = wdth * 0.13
            C1_CarePlanHistory.Cols("sUsername").Width = wdth * 0.1
            C1_CarePlanHistory.Cols("dtOutcomeDate").Width = wdth * 0.13
            C1_CarePlanHistory.Cols("RowState").Width = wdth * 0.1

            'C1_CarePlanHistory.ShowCellLabels = True
            C1_CarePlanHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_CarePlanHistory.Redraw = True
            C1_CarePlanHistory.AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignAssociationGrid()
        Try
            gloC1FlexStyle.Style(C1_AssociationHisotry)
            C1_AssociationHisotry.Rows.Fixed = 1


            C1_AssociationHisotry.Cols("sAssociationType").Caption = "Association Type"
            C1_AssociationHisotry.Cols("sAssociationDesc").Caption = "Association Description"
            C1_AssociationHisotry.Cols("dtRecordedDate").Caption = "Recorded Date"
            C1_AssociationHisotry.Cols("dtRecordedDate").Format = "MM/dd/yyyy hh:mm tt"
            C1_AssociationHisotry.Cols("RowState").Caption = "Action"

            C1_AssociationHisotry.Cols("nId").Visible = False
            C1_AssociationHisotry.Cols("nAssociationID").Visible = False

            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 40
            C1_AssociationHisotry.Cols("sAssociationType").Width = wdth * 0.2
            C1_AssociationHisotry.Cols("sAssociationDesc").Width = wdth * 0.5
            C1_AssociationHisotry.Cols("dtRecordedDate").Width = wdth * 0.2
            C1_AssociationHisotry.Cols("RowState").Width = wdth * 0.1

            'C1_CarePlanHistory.ShowCellLabels = True
            C1_AssociationHisotry.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_AssociationHisotry.Redraw = True
            C1_AssociationHisotry.AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            LoadHistory()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub C1_CarePlanHistory_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_CarePlanHistory.MouseDown
        Try

            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_CarePlanHistory.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_CarePlanHistory.Row = -1
            End If

            If (C1_CarePlanHistory.Row = -1) Then
                Return
            End If

            associateSelectedRow()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub associateSelectedRow()
        Try
            If (C1_CarePlanHistory.Rows.Count > 1) Then
                If (C1_CarePlanHistory.RowSel > 0) Then
                    Dim sRecordedDate As String = C1_CarePlanHistory.GetData(C1_CarePlanHistory.RowSel, "dtRecordedDate").ToString()
                    loadAssociation(sRecordedDate)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    'Private Sub C1_CarePlanHistory_SelChange(sender As Object, e As System.EventArgs) Handles C1_CarePlanHistory.SelChange
    '    Try
    '        If (C1_CarePlanHistory.Rows.Count > 1) Then
    '            If (C1_CarePlanHistory.RowSel > 0) Then
    '                Dim sRecordedDate As String = C1_CarePlanHistory.GetData(C1_CarePlanHistory.RowSel, "dtRecordedDate").ToString()
    '                loadAssociation(sRecordedDate)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub

    Private Sub C1_AssociationHisotry_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_AssociationHisotry.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1_CarePlanHistory_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_CarePlanHistory.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class