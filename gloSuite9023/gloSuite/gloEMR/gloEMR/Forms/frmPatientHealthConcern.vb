Imports gloCommon

Public Class frmPatientHealthConcern

    Dim _PatientID As Long = 0
    Dim _VisitID As Long = 0
    Dim _HealthConcernID As Long = 0
    Dim strConceptID As String = String.Empty
    Dim strDescription As String = String.Empty
    Dim dtHealthConcernAssociationTVP As DataTable
    Dim oListControl As gloListControl.gloListControl
    Dim ofrmCPTList As frmViewListControl
    Dim bIsRefChanged As Boolean = False

#Region "Constructor"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, Optional ByVal HealthConcernID As Long = 0)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        _HealthConcernID = HealthConcernID
        ' Add any initialization after the InitializeComponent() call.  
    End Sub
#End Region

    Private Sub frmPatientHealthConcern_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtHealthConcernName.Focus()

        setHealthConcernDT()
        chkStartDate.Checked = False
        chkEndDate.Checked = False
        If _HealthConcernID <> 0 Then
            loadHealthConcern()
        End If
        dtpConcernStartDate.Enabled = chkStartDate.Checked
        dtpConcernEndDate.Enabled = chkEndDate.Checked
        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)

    End Sub

    Private Sub setHealthConcernDT()
        'Table used for CP_TVPHealthConcernAssociation TVP parameter values
        dtHealthConcernAssociationTVP = New DataTable()
        dtHealthConcernAssociationTVP.Columns.Add("nHealthConcernAssociationID")
        dtHealthConcernAssociationTVP.Columns.Add("nHealthConcernID")
        dtHealthConcernAssociationTVP.Columns.Add("nAssociatedConcernId")
        dtHealthConcernAssociationTVP.Columns.Add("sAssociatedConcernType")
        dtHealthConcernAssociationTVP.Columns.Add("dtTransactiondatetime")
        dtHealthConcernAssociationTVP.Columns.Add("RowState")
    End Sub

    Private Sub loadHealthConcern()
        Try
            Dim dsHealthConcernDetails As DataSet = Nothing
            Using objPatientHealthConcern As New ClsHealthConcern()
                dsHealthConcernDetails = objPatientHealthConcern.GetPatientHealthConcern(_PatientID, _HealthConcernID)
            End Using

            If dsHealthConcernDetails IsNot Nothing Then
                If dsHealthConcernDetails.Tables("HealthConcernDetail").Rows.Count > 0 Then
                    _VisitID = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("nVisitId")
                    txtHealthConcernName.Text = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthConcernName")
                    strConceptID = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthConcernSnomedCode")
                    strDescription = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthConcernSnomedDescription")
                    If String.IsNullOrEmpty(strConceptID) Then
                        txtStatus.Text = ""
                    Else
                        txtStatus.Text = strConceptID + " - " + strDescription
                    End If
                    txtHealthStatusDesc.Text = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthStatusDescription")

                    'Health concern auther
                    Dim sConcernFrom As String = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthConcernAuthor")
                    If sConcernFrom = "Provider" Then
                        rbt_FromProvider.Checked = True
                    ElseIf sConcernFrom = "Patient" Then
                        rbt_FromPatient.Checked = True
                    ElseIf sConcernFrom = "Both" Then
                        rbt_FromBoth.Checked = True
                    End If

                    'Health Concern Status
                    Dim sConcernStatus As String = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthConcernStatus")
                    If sConcernStatus = "Active" Then
                        rbt_StatusActive.Checked = True
                    ElseIf sConcernStatus = "Completed" Then
                        rbt_StatusCompleted.Checked = True
                    ElseIf sConcernStatus = "Suspended" Then
                        rbt_StatusInactive.Checked = True
                    End If

                    If IsDBNull(dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("dtHealthConcernStartDate")) Then
                        chkStartDate.Checked = False
                    Else
                        chkStartDate.Checked = True
                        dtpConcernStartDate.Text = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("dtHealthConcernStartDate")
                    End If
                    If IsDBNull(dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("dtHealthConcernEndDate")) Then
                        chkEndDate.Checked = False
                    Else
                        chkEndDate.Checked = True
                        dtpConcernEndDate.Text = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("dtHealthConcernEndDate")
                    End If
                    txtHealthConcernNotes.Text = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("sHealthConcernNotes")
                    If Not IsDBNull(dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("dtHealthConcernDate")) Then
                        dtpConcernDate.Text = dsHealthConcernDetails.Tables("HealthConcernDetail").Rows(0)("dtHealthConcernDate")
                    End If
                End If

                If Not dsHealthConcernDetails.Tables("HealthConcernAssociation") Is Nothing Then
                    Dim oBindTable As New DataTable()
                    oBindTable.Columns.Add("ID")
                    oBindTable.Columns.Add("DispName")

                    dtHealthConcernAssociationTVP.Rows.Clear()
                    For h As Int32 = 0 To dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows.Count - 1
                        dtHealthConcernAssociationTVP.Rows.Add()
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nHealthConcernAssociationID") = dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows(h)("nHealthConcernAssociationID")
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nHealthConcernID") = _HealthConcernID
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nAssociatedConcernId") = dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows(h)("nAssociatedConcernId")
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("sAssociatedConcernType") = dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows(h)("sAssociatedConcernType")
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows(h)("dtTransactiondatetime")
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("RowState") = "Retrieved"

                        'Table for problem combobox datasource
                        Dim oRow As DataRow
                        oRow = oBindTable.NewRow()
                        oRow(0) = dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows(h)("nAssociatedConcernId")
                        oRow(1) = dsHealthConcernDetails.Tables("HealthConcernAssociation").Rows(h)("nAssociatedConcernDesc")
                        oBindTable.Rows.Add(oRow)
                    Next

                    If oBindTable.Rows.Count > 0 Then
                        cmbProblem.DataSource = oBindTable
                        cmbProblem.DisplayMember = "DispName"
                        cmbProblem.ValueMember = "ID"
                    End If
                End If
            End If
            If dsHealthConcernDetails IsNot Nothing Then
                dsHealthConcernDetails.Clear()
                dsHealthConcernDetails.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_PatientInjuryDate_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientInjuryDate.ItemClicked

        Select Case e.ClickedItem.Name
            Case ts_btnOk.Name
                If ValidateEntries() Then
                    SaveHealthConcern()
                End If
            Case ts_btnCancel.Name
                Me.Close()
        End Select

    End Sub

    Private Function ValidateEntries() As Boolean
        If String.IsNullOrWhiteSpace(txtHealthConcernName.Text) Then
            MessageBox.Show("Please enter a name for health concern.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtHealthConcernName.Focus()
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtHealthConcernNotes.Text) AndAlso (cmbProblem.Items.Count < 1) Then
            MessageBox.Show("Health concern requires Notes or Problem reference.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtHealthConcernNotes.Focus()
            Return False
        End If
        If chkStartDate.Checked And chkEndDate.Checked Then
            If dtpConcernEndDate.Value.Date < dtpConcernStartDate.Value.Date Then
                MessageBox.Show("End Date should be greater than or equal to Start Date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpConcernEndDate.Focus()
                Return False
            End If
        End If
        Return True
    End Function


    Private Sub SaveHealthConcern()
        Dim objCarePlan As New ClsHealthConcern()
        Try
            Dim sConcernStatus As String = String.Empty
            Dim sConcernFrom As String = String.Empty

            'Health concern auther
            If rbt_FromProvider.Checked Then
                sConcernFrom = "Provider"
            ElseIf rbt_FromPatient.Checked Then
                sConcernFrom = "Patient"
            ElseIf rbt_FromBoth.Checked Then
                sConcernFrom = "Both"
            End If

            'Health Concern Status
            If rbt_StatusActive.Checked Then
                sConcernStatus = "Active"
            ElseIf rbt_StatusCompleted.Checked Then
                sConcernStatus = "Completed"
            ElseIf rbt_StatusInactive.Checked Then
                sConcernStatus = "Suspended"
            End If

            objCarePlan.nPatientId = _PatientID
            objCarePlan.nVisitId = _VisitID
            objCarePlan.sHealthConcernName = txtHealthConcernName.Text.Trim()
            objCarePlan.nHealthConcernID = _HealthConcernID
            objCarePlan.sHealthConcernSnomedCode = strConceptID
            objCarePlan.sHealthConcernSnomedDescription = strDescription
            objCarePlan.sHealthStatusDescription = IIf(strConceptID.Trim() = "", "", txtHealthStatusDesc.Text.Trim())
            objCarePlan.sHealthConcernAuthor = sConcernFrom
            objCarePlan.sHealthConcernStatus = sConcernStatus
            objCarePlan.dtHealthConcernStartDate = dtpConcernStartDate.Text
            objCarePlan.dtHealthConcernEndDate = dtpConcernEndDate.Text
            objCarePlan.sHealthConcernNotes = txtHealthConcernNotes.Text.Trim()
            objCarePlan.dtHealthConcernDate = dtpConcernDate.Value.Date
            objCarePlan.bIsStartDate = chkStartDate.Checked
            objCarePlan.bIsEndDate = chkEndDate.Checked

            If bIsRefChanged Then
                fillTVPs()
            End If

            Dim sRowState As String = ""
            If _HealthConcernID = 0 Then
                sRowState = "Added"
            Else
                sRowState = "Updated"
            End If


            Dim nReturnId As Int64 = objCarePlan.SaveHealthConcern(dtHealthConcernAssociationTVP, sRowState)

            If _HealthConcernID = 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Add, "Patient Health Concern added", _PatientID, nReturnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Modify, "Patient Health Concern Modified", _PatientID, _HealthConcernID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            Dim dtUniqueIds As DataTable = ClsCarePlan_V2.GetUniqueIDs(cmbProblem.Items.Count)
            If cmbProblem.Items.Count > 0 Then
                For j As Integer = 0 To cmbProblem.Items.Count - 1
                    cmbProblem.Text = ""
                    cmbProblem.SelectedIndex = j

                    If Not checkAndUpdate(cmbProblem.SelectedValue) Then
                        dtHealthConcernAssociationTVP.Rows.Add()
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nHealthConcernAssociationID") = dtUniqueIds.Rows(j)(0)
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nHealthConcernID") = "0"
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("nAssociatedConcernId") = cmbProblem.SelectedValue
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("sAssociatedConcernType") = "Problem"
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("dtTransactiondatetime") = ""
                        dtHealthConcernAssociationTVP.Rows(dtHealthConcernAssociationTVP.Rows.Count - 1)("RowState") = "Added"
                    End If
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

#Region "Problem List Buttons"

    Private Sub btnClearProblems_Click(sender As System.Object, e As System.EventArgs) Handles btnClearProblems.Click
        cmbProblem.DataSource = Nothing
        cmbProblem.Items.Clear()
        markCurrRefAsDeleted()
    End Sub

    Private Sub btn_LoadProblem_Click(sender As System.Object, e As System.EventArgs) Handles btn_LoadProblem.Click
        Try
            markCurrRefAsDeleted()
            ofrmCPTList = New frmViewListControl
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CarePlanProblemList, True, Me.Width)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)

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

    Private Sub markCurrRefAsDeleted()
        Try
            bIsRefChanged = True
            If dtHealthConcernAssociationTVP.Rows.Count > 0 Then
                dtHealthConcernAssociationTVP.Rows.Cast(Of DataRow)().ToList().ForEach(Sub(r) r.SetField("RowState", "Deleted"))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Function checkAndUpdate(ByVal sAssociationId As String) As Boolean
        Dim exists As Boolean = False
        Try
            If dtHealthConcernAssociationTVP.Rows.Count > 0 Then
                exists = dtHealthConcernAssociationTVP.AsEnumerable().Where(Function(c) c.Field(Of String)("nAssociatedConcernId") = sAssociationId).Count() > 0
                If exists Then
                    dtHealthConcernAssociationTVP.Rows.Cast(Of DataRow)().Where(Function(r) r.Item("nAssociatedConcernId") = sAssociationId).ToList().ForEach(Sub(r) r.SetField("RowState", "NoChange"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return exists
    End Function
#End Region

#Region "Snomed Code Buttons"

    Private Sub btnBrowserSnomedCode_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowserSnomedCode.Click
        Dim strtxtSnomed As String = ""
        Dim strOldConcept As String = ""
        Dim strOldDesc As String = ""

        Dim frm As gloSnoMed.FrmSelectProblem = Nothing
        Try
            strtxtSnomed = txtStatus.Text
            strOldConcept = strConceptID
            strOldDesc = strDescription

            gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
            frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())
            Dim arrIcd() As String = txtStatus.Text.Split("-")

            frm.strCodeSystem = "SNOMED"
            frm.txtSMSearch.Text = strConceptID  'lblConceptID.Text.Trim

            frm.blnIsProblem = True
            frm.strConceptID = strConceptID
            frm.strConceptDesc = strDescription
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            If frm._DialogResult = True Then
                frm.strProblem = Replace(Replace(frm.strProblem, " (disorder)", ""), " (finding)", "")

                'strDescription = Replace(Replace(frm.strSelectedDescription, " (disorder)", ""), " (finding)", "")

                If frm.strProblem <> "" Then
                    strConceptID = frm.strSelectedConceptID
                    strDescription = frm.strProblem.Trim()
                    txtStatus.Text = strConceptID + " - " + strDescription
                    'If txtHealthStatusDesc.Text.Length = 0 Then ''Commencted condition to change desc. when status changes
                    txtHealthStatusDesc.Text = frm.strProblem
                    'End If
                Else
                    txtStatus.Text = ""
                    strConceptID = ""
                    strDescription = ""
                End If
            Else
                strConceptID = ""
                strDescription = ""
                If strtxtSnomed <> "" Then
                    txtStatus.Text = strtxtSnomed
                End If
                If strOldConcept <> "" Then
                    strConceptID = strOldConcept
                End If
                If strOldDesc <> "" Then
                    strDescription = strOldDesc
                End If
                '
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.PatientHealthConcern, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
        txtStatus.Text = ""
        strConceptID = ""
        strDescription = ""
        txtHealthStatusDesc.Text = ""
    End Sub

#End Region

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

    Private Sub rbt_StatusActive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_StatusActive.CheckedChanged
        If rbt_StatusActive.Checked = True Then
            rbt_StatusActive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_StatusActive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_StatusCompleted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_StatusCompleted.CheckedChanged
        If rbt_StatusCompleted.Checked = True Then
            rbt_StatusCompleted.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_StatusCompleted.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbt_StatusInactive_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbt_StatusInactive.CheckedChanged
        If rbt_StatusInactive.Checked = True Then
            rbt_StatusInactive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_StatusInactive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

#End Region

    Private Sub chkEndDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEndDate.CheckedChanged
        dtpConcernEndDate.Enabled = chkEndDate.Checked
    End Sub

    Private Sub chkStartDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkStartDate.CheckedChanged
        dtpConcernStartDate.Enabled = chkStartDate.Checked
    End Sub
End Class