Imports gloSnoMed
Public Class frmAddProblemList

    'Dim str As String = ""
    Public strSelectedProblem As String = String.Empty
    Public strSelectedICD9 As String = String.Empty
    Public strSelectedComment As String = String.Empty
    Public strProblem As String = String.Empty ''swaraj
    Public strConceptID As String = String.Empty
    Public strConceptID_Old As String = String.Empty
    Public strDescriptionID As String = String.Empty
    Public strSnoMedId As String = String.Empty
    Public strDescription As String = String.Empty
    'Public strSnomedCode As String = String.Empty
    Public bEncounterDiagnosis As Boolean = False
    Public c1Problemlist As C1.Win.C1FlexGrid.C1FlexGrid
    Public strLateralityCode As String = String.Empty
    Public strLateralityDesc As String = String.Empty
   






    Dim ofrmAddcomment As frmAddProblemListComment = Nothing
    Dim strComment As String = ""
    Public comm As String = ""

    Dim arrComp() As String
    Dim arrComment() As String
    Dim arrCommentText() As String
    Dim arrCommentDetail() As String
    Dim nCommentCount As Integer
    'Dim arrText() As String

    Dim _PatientID As Long = 0
    Dim _VisitID As Long = 0
    Public _ProviderID As Long = 0
    Dim _ISSmonedCodeMandatory As Boolean = False
    Private btnICD9Click As Boolean = False




    Private WithEvents dgCustomGrid As CustomTask
    Private WithEvents dgCustomGridExam As CustomTask
    Private Shared frmaddprb As frmAddProblemList
    Private Col_Check As Integer = 2
    Private Col_DrugName As Integer = 0
    Private Col_Dosage As Integer = 1
    Private Col_Count As Integer = 3

    Private Col_eExamID As Integer = 0
    Private Col_eVistitID As Integer = 1
    Private Col_eDos As Integer = 2
    Private Col_eExamName As Integer = 3
    Private Col_eTemplateName As Integer = 4
    Private Col_eFinished As Integer = 5
    Private Col_eProviderName As Integer = 6
    Private Col_eReviewedBy As Integer = 7
    Private Col_eCheck As Integer = 8
    Private Col_eCount As Integer = 9


    Public nICDRevision As Int16 = 9 ''added for ICD10 implementation
    Public Shared htIcdSnomed As New Hashtable
    Public nProblemExamId As Long = 0 'Assigned While Opening Problem From ProblemList Screen
    Public strCurrentICDCode As String = "" 'Assigned While Opening Problem From ProblemList Screen
    Public strCurrentICDDesc As String = "" 'Assigned While Opening Problem From ProblemList Screen
    Public strCurrentSnomedCode As String = "" 'Assigned While Opening Problem From ProblemList Screen
    Public strCurrentSnomedDesc As String = "" 'Assigned While Opening Problem From ProblemList Screen
    Public strCurrentICDRevision As String = "9" 'Assigned While Opening Problem From ProblemList Screen
    Public strCurrentLateralityCode As String = ""
    Public strCurrentLateralityDesc As String = ""

    Private toolTipSnomed As New System.Windows.Forms.ToolTip
    Private toolTipIcd As New System.Windows.Forms.ToolTip
    Private toolTipDescription As New System.Windows.Forms.ToolTip


    Public _ProblemId As Int64 = 0
    Private _Provider As String
    Private _Location As String
    Private _ICD9 As String
    Private _sconcernstatus As String = ""
    Private _ProblemType As String = ""

    Shared _blnclNewProblem As Boolean = False
    Dim _IsNewProblem As Boolean = False
    Dim dtsave As DataTable = Nothing
    Private ToolTipcmbProblemType As New ToolTip


    Public Shared Property blnclNewProblem() As Boolean
        Get
            Return _blnclNewProblem
        End Get
        Set(ByVal value As Boolean)
            _blnclNewProblem = value
        End Set
    End Property

    Public Property Provider() As String
        Get
            Return _Provider
        End Get
        Set(ByVal value As String)
            _Provider = value
        End Set
    End Property

    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Public Property ICD9() As String
        Get
            Return _ICD9
        End Get
        Set(ByVal value As String)
            _ICD9 = value
        End Set
    End Property

    Public Property IsNewProblem() As Boolean
        Get
            Return _IsNewProblem
        End Get
        Set(ByVal value As Boolean)
            _IsNewProblem = value
        End Set
    End Property

    Public Property ConcernStatus As String
        Get
            Return _sconcernstatus
        End Get
        Set(ByVal value As String)
            _sconcernstatus = value
        End Set
    End Property
    Public Property CDAProblemType As String
        Get
            Return _ProblemType
        End Get
        Set(ByVal value As String)
            _ProblemType = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        ' Add any initialization after the InitializeComponent() call.  
    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, ByVal c1ProblemList1 As C1.Win.C1FlexGrid.C1FlexGrid)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = VisitID
        c1Problemlist = c1ProblemList1
        ' Add any initialization after the InitializeComponent() call.  
    End Sub

    Private Sub frmAddProblemList_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If dtProblemType IsNot Nothing Then
            dtProblemType.Dispose()
            dtProblemType = Nothing
        End If
    End Sub



    Private Sub frmAddProblemList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load



        Dim oclsProblemList As clsPatientProblemList = Nothing
        Try
            '''' Fill Provider list
            '      gloSettings.gloEMRAdminSettings.globlnEnableCypressTesting

            If (gblnEnableCQMCypressTesting) Then
                dtpOnsetDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
                dtpDischargeDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
                'dtpConcernDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
                pnlDischargeDate.Visible = True
                'dtpDischargeDate.Visible = True

                dtpOnsetDate.Width = 177
                dtpDischargeDate.Width = 177
            Else
                dtpOnsetDate.CustomFormat = "MM/dd/yyyy"
                ' dtpDischargeDate.CustomFormat = "MM/dd/yyyy"
                'dtpConcernDate.CustomFormat = "MM/dd/yyyy"
                dtpOnsetDate.Width = 105
                '  dtpDischargeDate.Width = 105
                pnlDischargeDate.Visible = False
                'dtpDischargeDate.Visible = False
            End If

            oclsProblemList = New clsPatientProblemList
            Dim dt As DataTable
            dt = oclsProblemList.fillProvider()
            If Not IsNothing(dt) Then
                cmb_Provider.DataSource = dt
                cmb_Provider.DisplayMember = "ProviderName"
                cmb_Provider.ValueMember = "nProviderID"
            End If
            'dt = oclsProblemList.FillLocation()
            ''Checking Snomed Setting 
            _ISSmonedCodeMandatory = oclsProblemList.IsSnomedMandatory()

            If _ISSmonedCodeMandatory Then
                lblSnomedCodeMandatory.Visible = True
            Else
                lblSnomedCodeMandatory.Visible = False
            End If
            chkEncounter.Checked = bEncounterDiagnosis
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(oclsProblemList) Then
                oclsProblemList.Dispose()
                oclsProblemList = Nothing
            End If

            'cmbConcernStatus.Items.Add("")
            'cmbConcernStatus.Items.Add("Completed")
            'cmbConcernStatus.Items.Add("Active")
            'cmbConcernStatus.Items.Add("Aborted")
            'cmbConcernStatus.Items.Add("Suspended")

            Fill_ConcernStatus()
            Fill_ProblemType()





           


  End Try

        If IsNewProblem = True Then
            Dim frm As gloSnoMed.FrmSelectProblem
            Try
                gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                frm = New gloSnoMed.FrmSelectProblem("Problem List", gstrSMDBConnstr, GetConnectionString())
                strSelectedProblem = frm.strProblem
                If frm.strICD10 <> "" Then
                    strSelectedICD9 = frm.strICD10
                Else
                    strSelectedICD9 = frm.strICD9
                End If
                strConceptID = frm.strSelectedConceptID
                strDescriptionID = frm.strSelectedDescriptionID
                strSnoMedId = frm.strSelectedSnoMedID
                strDescription = frm.strSelectedDescription
                lblConceptID.Text = frm.strSelectedConceptID
                lblDescriptionID.Text = frm.strSelectedDescriptionID
                'lblSnoMedID.Text = frm.strSelectedSnoMedID
                frm.Dispose()
                frm = Nothing
                If strSelectedICD9 <> "" Then
                    txt_Problem.Text = strSelectedProblem.Trim()
                Else
                    txt_Problem.Text = strSelectedProblem.Trim()
                End If
                'Fill_Diagnosis()
                If strSelectedICD9 <> "" Then
                    txtICD9.Text = strSelectedICD9.Replace(":", "-")
                End If

                cmbProblemType.Text = "Problem"
                ''End
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        ElseIf IsNewProblem = False Then
            txtlocation.Text = Location
            cmb_Provider.Text = Provider.Trim() 'chetan commented
            arrComp = Split(comm, vbNewLine, 2)
            If txt_Problem.Text = "" Then
                If ICD9 <> "" Then
                    txt_Problem.Text = Convert.ToString(arrComp.GetValue(0)).Trim()
                Else
                    txt_Problem.Text = Convert.ToString(arrComp.GetValue(0)).Trim()
                End If
            End If
            If ICD9 <> "" Then
                txtICD9.Text = ICD9
            End If
            If strConceptID <> "" Then
                If strDescription.Contains("-") Then
                    Dim arrProblemType() As String = strDescription.Split("-")
                    If arrProblemType.Length > 1 Then
                        If arrProblemType(0).Trim = strConceptID.Trim Then
                            strDescription = arrProblemType(1).Trim
                        End If
                    End If
                End If
                txtSnomed.Text = strConceptID + "-" + strDescription ''arrComp.GetValue(0)
            End If
            strSelectedProblem = arrComp.GetValue(0)
            strSelectedICD9 = ICD9
            For i As Integer = 1 To arrComp.Length - 1
                arrComment = Split(arrComp.GetValue(i), vbNewLine)
                For j As Integer = 0 To arrComment.Length - 1
                    arrCommentText = Split(arrComment.GetValue(j), ":", 2)
                    If arrCommentText.Length > 1 Then
                        dgComments.Rows.Add()
                        arrCommentDetail = Split(arrCommentText.GetValue(1), "-", 2)
                        If (arrCommentDetail.Length > 1) Then
                            dgComments.Item(0, dgComments.Rows.Count - 1).Value = arrCommentDetail.GetValue(1)
                        End If
                        dgComments.Item(1, dgComments.Rows.Count - 1).Value = arrCommentDetail.GetValue(0)
                    Else
                    End If
                Next
            Next
            txtLaterality.Text = strLateralityCode + "-" + strLateralityDesc

        End If
        cmbConcernStatus.Text = ConcernStatus
        ' cmbProblemType.Text = CDAProblemType
        toolTipSnomed.SetToolTip(txtSnomed, txtSnomed.Text)
        toolTipIcd.SetToolTip(txtICD9, txtICD9.Text)
        toolTipDescription.SetToolTip(txt_Problem, txt_Problem.Text)

        dtResolved.CustomFormat = "MM/dd/yyyy"
        Dim scheme As gloBilling.Cls_TabIndexSettings.TabScheme = gloBilling.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New gloBilling.Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing
        cmbProblemType.DrawMode = DrawMode.OwnerDrawFixed


        AddHandler cmbProblemType.DrawItem, New DrawItemEventHandler(AddressOf cmbProblemType_DrawItem)
        AddHandler cmbProblemType.DropDownClosed, New EventHandler(AddressOf cmbProblemType_DropDownClosed)
        AddHandler cmbProblemType.Leave, New EventHandler(AddressOf cmbProblemType_Leave)

    End Sub


    Private Sub tls_comment_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_comment.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                Dim SelectedICD9 As String = ""
                Dim SelectedConceptID As String = ""
                SelectedICD9 = txtICD9.Text.Trim() 'cmbICD9.Text.Trim()
                SelectedConceptID = txtSnomed.Text.Trim() ' CmbSnomedcode.Text.Trim()
                nICDRevision = strCurrentICDRevision
                'Dim dtdata As DataTable = cmbICD9.DataSource
                'If Not IsNothing(dtdata) Then
                '    Try
                '        ''added for ICD10 implementation
                '        nICDRevision = Convert.ToInt16(dtdata.Rows(cmbICD9.SelectedIndex)("nICDRevision"))
                '    Catch ex As Exception
                '    End Try
                'End If
                ConcernStatus = cmbConcernStatus.Text
                CDAProblemType = cmbProblemType.SelectedValue
                bEncounterDiagnosis = chkEncounter.Checked
                If SelectedICD9 <> "" Then
                    SelectedICD9 = SelectedICD9.Split("-")(0).Trim
                End If
                If SelectedConceptID <> "" Then
                    SelectedConceptID = SelectedConceptID.Split("-")(0).Trim
                End If
                If _ISSmonedCodeMandatory Then
                    If txtSnomed.Text.Trim() = "" Then ' If CmbSnomedcode.Text.Trim() = "" Then
                        MessageBox.Show("Problems require SNOMED CT.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        btnBrowserSnomedCode.Focus()
                        Exit Sub
                    End If
                End If
                If SelectedICD9 = "" AndAlso SelectedConceptID = "" Then
                    MessageBox.Show("Problems require SNOMED CT or ICD9 Code.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnBrowserSnomedCode.Focus()
                    Exit Sub
                End If

                If txt_Problem.Text.Trim() = "" Then
                    MessageBox.Show("Please enter a description for the problem.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btn_Problem.Focus()
                    Exit Sub
                End If

                If rbt_Inactive.Checked <> False Then
                    If dtResolved.Value.Date < dtpOnsetDate.Value.Date Then
                        MessageBox.Show("Resolved Date should be greater than or equal to Onset Date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dtResolved.Focus()
                        Exit Sub
                    End If
                End If

                If SelectedICD9 <> "" Then
                    If ICD9 IsNot Nothing Then
                        If ICD9.Split("-")(0).Trim <> txtICD9.Text.Trim().Split("-")(0).Trim() Then ' cmbICD9.Text.Trim().Split("-")(0).Trim Then
                            For nrow As Integer = 1 To c1Problemlist.Rows.Count - 1
                                If Convert.ToString(c1Problemlist.GetData(nrow, 6)).Split("-")(0).Trim = SelectedICD9 AndAlso SelectedICD9 <> "" AndAlso Convert.ToString(c1Problemlist.GetData(nrow, 3)) = "Active" Then
                                    Dim strMessage As String
                                    Dim _result As DialogResult
                                    strMessage = "Problem with same ICD9/SNOMED code already exists. Do you want to" & vbNewLine & "continue?" & vbNewLine & vbNewLine & "YES - Save the problem " & vbNewLine & vbNewLine & "NO  - Do not save the problem and close screen" & vbNewLine & vbNewLine & "Cancel  - Do not save and do not close the screen"

                                    _result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                                    If _result = Windows.Forms.DialogResult.No Then
                                        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                        Me.Close()
                                        Exit Sub
                                    ElseIf _result = Windows.Forms.DialogResult.Cancel Then
                                        '' Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                        Exit Sub
                                    ElseIf _result = Windows.Forms.DialogResult.Yes Then
                                        strSelectedICD9 = txtICD9.Text.Trim() ' cmbICD9.Text.Trim()
                                        Me.DialogResult = Windows.Forms.DialogResult.OK
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End If
                    Else
                        For nrow As Integer = 1 To c1Problemlist.Rows.Count - 1
                            If Convert.ToString(c1Problemlist.GetData(nrow, 6)).Split("-")(0).Trim = SelectedICD9 AndAlso SelectedICD9 <> "" AndAlso Convert.ToString(c1Problemlist.GetData(nrow, 3)) = "Active" Then
                                Dim strMessage As String
                                Dim _result As DialogResult
                                strMessage = "Problem with same ICD9/SNOMED code already exists. Do you want to" & vbNewLine & "continue?" & vbNewLine & vbNewLine & "YES - Save the problem " & vbNewLine & vbNewLine & "NO  - Do not save the problem and close screen" & vbNewLine & vbNewLine & "Cancel  - Do not save and do not close the screen"

                                _result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                                If _result = Windows.Forms.DialogResult.No Then
                                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                    Me.Close()
                                    Exit Sub
                                ElseIf _result = Windows.Forms.DialogResult.Cancel Then
                                    '' Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                    Exit Sub
                                ElseIf _result = Windows.Forms.DialogResult.Yes Then
                                    strSelectedICD9 = txtICD9.Text.Trim() ' cmbICD9.Text.Trim()
                                    Me.DialogResult = Windows.Forms.DialogResult.OK
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If


                End If

                If SelectedConceptID <> "" Then
                    If strConceptID_Old IsNot Nothing Then
                        If strConceptID_Old.Split("-")(0).Trim <> txtSnomed.Text.Trim().Split("-")(0).Trim() Then ' CmbSnomedcode.Text.Trim().Split("-")(0).Trim Then
                            For nrow As Integer = 1 To c1Problemlist.Rows.Count - 1
                                If Convert.ToString(c1Problemlist.GetData(nrow, 20)).Split("-")(0).Trim = SelectedConceptID AndAlso SelectedConceptID <> "" AndAlso Convert.ToString(c1Problemlist.GetData(nrow, 3)) = "Active" Then
                                    Dim strMessage As String
                                    Dim _result As DialogResult
                                    strMessage = "Problem with same ICD9/SNOMED code already exists. Do you want to" & vbNewLine & "continue?" & vbNewLine & vbNewLine & "YES - Save the problem " & vbNewLine & vbNewLine & "NO  - Do not save the problem and close screen" & vbNewLine & vbNewLine & "Cancel  - Do not save and do not close the screen"

                                    _result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                                    If _result = Windows.Forms.DialogResult.No Then
                                        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                        Me.Close()
                                        Exit Sub
                                    ElseIf _result = Windows.Forms.DialogResult.Cancel Then
                                        '' Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                        Exit Sub
                                    ElseIf _result = Windows.Forms.DialogResult.Yes Then
                                        ''strSelectedICD9 = cmbICD9.Text.Trim()
                                        Me.DialogResult = Windows.Forms.DialogResult.OK
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End If
                    Else
                        For nrow As Integer = 1 To c1Problemlist.Rows.Count - 1
                            If Convert.ToString(c1Problemlist.GetData(nrow, 20)).Split("-")(0).Trim = SelectedConceptID AndAlso SelectedConceptID <> "" AndAlso Convert.ToString(c1Problemlist.GetData(nrow, 3)) = "Active" Then
                                Dim strMessage As String
                                Dim _result As DialogResult
                                strMessage = "Problem with same ICD9/SNOMED code already exists. Do you want to" & vbNewLine & "continue?" & vbNewLine & vbNewLine & "YES - Save the problem " & vbNewLine & vbNewLine & "NO  - Do not save the problem and close screen" & vbNewLine & vbNewLine & "Cancel  - Do not save and do not close the screen"

                                _result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                                If _result = Windows.Forms.DialogResult.No Then
                                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                    Me.Close()
                                    Exit Sub
                                ElseIf _result = Windows.Forms.DialogResult.Cancel Then
                                    '' Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                    Exit Sub
                                ElseIf _result = Windows.Forms.DialogResult.Yes Then
                                    ''strSelectedICD9 = cmbICD9.Text.Trim()
                                    Me.DialogResult = Windows.Forms.DialogResult.OK
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If


                End If

                strSelectedICD9 = txtICD9.Text.Trim() 'cmbICD9.Text.Trim()

                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "Close"
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
        End Select
    End Sub

    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private ofrmDiagnosisList As frmViewListControl
    Private oDiagnosisListControl As gloListControl.gloListControl

    Private Sub btnBrowseICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseICD9.Click
        btnICD9Click = True
        Try
            ofrmDiagnosisList = New frmViewListControl
            oDiagnosisListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Diagnosis, False, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            oDiagnosisListControl.gblnIcd10Transition = gblnIcd10MasterTransition 'gblnIcd10Transition   ''If true then ICD10 gets selected 
            AddHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
            AddHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
            ofrmDiagnosisList.Controls.Add(oDiagnosisListControl)
            oDiagnosisListControl.Dock = DockStyle.Fill
            oDiagnosisListControl.BringToFront()

            oDiagnosisListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog(IIf(IsNothing(CType(ofrmDiagnosisList, Control).Parent), Me, CType(ofrmDiagnosisList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmDiagnosisList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
                RemoveHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
                oDiagnosisListControl.Dispose()
                oDiagnosisListControl = Nothing
            End If

            If IsNothing(ofrmDiagnosisList) = False Then
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'If gblnICD9Driven Then
        '    Dim frmDiagnosis As New frm_Diagnosis(_VisitID, 0, _PatientID)
        '    With frmDiagnosis
        '        .ShowInTaskbar = False
        '        .ShowDialog(IIf(IsNothing(frmDiagnosis.Parent), Me, frmDiagnosis.Parent))
        '        nICDRevision = Convert.ToInt16(.SelectedICD)   ''added for ICD10 implementation
        '        If .DialogResult = Windows.Forms.DialogResult.Yes Then
        '            Fill_Diagnosis()
        '        End If
        '        .Dispose()
        '    End With
        '    frmDiagnosis = Nothing
        'Else
        '    Dim oTreatment As frm_Treatment
        '    If nProblemExamId = 0 Then
        '        oTreatment = New frm_Treatment(0, _VisitID, Now.Date, "", _PatientID)
        '    Else
        '        oTreatment = New frm_Treatment(nProblemExamId, _VisitID, Now.Date, "", _PatientID)
        '    End If


        '    oTreatment.ShowDialog(IIf(IsNothing(oTreatment.Parent), Me, oTreatment.Parent))
        '    If oTreatment.isSaved Then
        '        Fill_Diagnosis()
        '    End If
        '    oTreatment.Dispose()
        '    oTreatment = Nothing
        'End If
        btnICD9Click = False
    End Sub

    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                    txtICD9.Text = oDiagnosisListControl.SelectedItems(i).Code + " - " + oDiagnosisListControl.SelectedItems(i).Description
                    strCurrentICDCode = oDiagnosisListControl.SelectedItems(i).Code
                    strCurrentICDDesc = oDiagnosisListControl.SelectedItems(i).Description
                    strCurrentICDRevision = oDiagnosisListControl.IsICD9_10 ''  nICDType added for ICD10 implementation
                    LoadDefaultSnoomed()
                    strConceptID = strCurrentSnomedCode

                    '08-Oct-14 Aniket: Resolving Bug #74813 ( Modified): gloEMR - Problem List - Application does not omit words 'finding' and 'Disorder'
                    strDescription = Replace(Replace(strCurrentSnomedDesc, " (disorder)", ""), " (finding)", "")
                    'strDescription = strCurrentSnomedDesc

                    '08-Oct-14 Aniket: Resolving Bug #74813 ( Modified): gloEMR - Problem List - Application does not omit words 'finding' and 'Disorder'
                    txtSnomed.Text = strDescription

                    lblConceptID.Text = strConceptID
                    If (strCurrentSnomedDesc.ToString().Trim() <> "") Then  ''added condition for Bug 75107:
                        Dim arrProblem() As String = strDescription.Split("-")
                        If arrProblem.Length > 1 Then
                            txt_Problem.Text = arrProblem(1).ToString().Trim
                        Else
                            txt_Problem.Text = oDiagnosisListControl.SelectedItems(i).Description.ToString().Trim()
                        End If
                    Else
                        txt_Problem.Text = oDiagnosisListControl.SelectedItems(i).Description.ToString().Trim()
                    End If
                    Fill_Diagnosis()
                Next
                ofrmDiagnosisList.Close()
            Else
                txtICD9.Text = ""
                ofrmDiagnosisList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmDiagnosisList.Close()
        'If IsNothing(ofrmDiagnosisList) = False Then
        '    ofrmDiagnosisList = Nothing
        'End If
    End Sub

    Private Sub btnBrowserSnomedCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowserSnomedCode.Click
        Dim strtxtSnomed As String = ""
        Dim strtxtICD As String = ""
        Dim strOldConcept As String = ""
        Dim strOldDesc As String = ""
        Dim arrICD9() As String

        'Dim dtICD As DataTable = Nothing
        Dim frm As gloSnoMed.FrmSelectProblem = Nothing
        Try
            strtxtSnomed = txtSnomed.Text ' CmbSnomedcode.Text
            strtxtICD = txtICD9.Text
            strOldConcept = strConceptID
            strOldDesc = strDescription

            gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
            frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())
            Dim arrIcd() As String = txtSnomed.Text.Split("-")
            If txtSnomed.Text <> "" AndAlso txtSnomed.Text.Trim() <> "-" Then
                frm.strCodeSystem = "SNOMED"
                frm.txtSMSearch.Text = lblConceptID.Text.Trim
            Else
                If txtICD9.Text <> "" AndAlso txtICD9.Text.Trim() <> "-" Then
                    If strCurrentICDRevision = 9 Then
                        frm.strCodeSystem = "ICD9"
                    ElseIf strCurrentICDRevision = 10 Then
                        frm.strCodeSystem = "ICD10"
                    End If
                    frm.txtSMSearch.Text = strCurrentICDCode ' cmbICD9.SelectedValue
                Else
                    frm.strCodeSystem = "SNOMED"
                    frm.txtSMSearch.Text = lblConceptID.Text.Trim
                End If
            End If
            frm.blnIsProblem = True
            frm.strConceptID = lblConceptID.Text.Trim()
            frm.strConceptDesc = txt_Problem.Text.Trim()
            frm.strDescriptionID = lblDescriptionID.Text.Trim()
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            If _blnclNewProblem = True Then
                _blnclNewProblem = False
                Me.Close()
            End If

            If frm._DialogResult = True Then
                'strSelectedProblem = frm.strProblem
                If frm.strICD10 <> "" Then
                    strSelectedICD9 = frm.strICD10
                    strCurrentICDRevision = "10"
                Else
                    strSelectedICD9 = frm.strICD9
                    If (strSelectedICD9.Trim() <> "") Then
                        strCurrentICDRevision = "9"
                    End If
                    End If

                    If txtICD9.Text = "" Then
                        txtICD9.Text = strSelectedICD9.Replace(":", "-")
                    Else
                        If strSelectedICD9 <> "" Then
                            If txtICD9.Text <> strSelectedICD9 Then
                                txtICD9.Text = strSelectedICD9.Replace(":", "-")
                            End If
                            'Else
                            '    txtICD9.Text = strSelectedICD9.Replace(":", "-")
                        End If
                    End If


                    frm.strProblem = Replace(Replace(frm.strProblem, " (disorder)", ""), " (finding)", "")


                    'strProblem = frm.strProblem 'txt_Problem.Text ''swaraj 20100612
                    strConceptID = frm.strSelectedConceptID

                    '08-Oct-14 Aniket: Bug #74799 ( Modified): gloEMR - Problem List - Application is showing 'finding' and 'Disorder' when user modify new problem.
                    strDescription = Replace(Replace(frm.strSelectedDescription, " (disorder)", ""), " (finding)", "")

                    'strSnoMedId = frm.strSelectedSnoMedID
                    'strDescriptionID = frm.strSelectedDescriptionID
                    lblConceptID.Text = frm.strSelectedConceptID
                    lblDescriptionID.Text = frm.strSelectedDescriptionID
                    'lblSnoMedID.Text = frm.strSelectedSnoMedID

                    If strSelectedProblem <> "" Then
                        txtSnomed.Text = frm.strProblem
                    Else
                        txtSnomed.Text = strProblem ''swaraj 20100612 ''strSelectedProblem
                    End If
                    If frm.strProblem <> "" Then
                        txtSnomed.Text = strConceptID + " - " + frm.strProblem
                    Else
                        txtSnomed.Text = ""
                    End If
                    'End If

                    If strSelectedICD9 <> "" Then
                        arrICD9 = strSelectedICD9.Split(":")
                    Else
                        arrICD9 = strtxtICD.Split("-")
                    End If

                    If arrICD9.Length > 1 Then
                        'Resolving Bug No.71133::Problem List - Application displays old ICD code in search field of Select SNOMED screen.
                        strCurrentICDCode = arrICD9(0).Trim()
                        strCurrentICDDesc = arrICD9(1).Trim()
                        '
                        If htIcdSnomed.Count > 0 Then
                            If htIcdSnomed.ContainsKey(Convert.ToString(arrICD9(0).Trim)) Then
                                If htIcdSnomed.Item(Convert.ToString(arrICD9(0).Trim).Trim) <> strConceptID.Trim Then
                                    htIcdSnomed.Item(Convert.ToString(arrICD9(0).Trim)) = strConceptID.Trim
                                End If
                            Else
                                If arrICD9.Length > 1 Then
                                    If htIcdSnomed.ContainsKey(arrICD9(0).Trim) = False Then
                                        htIcdSnomed.Add(arrICD9(0).Trim, strConceptID.Trim)
                                    Else
                                        htIcdSnomed.Item(arrICD9(0).Trim) = strConceptID.Trim
                                    End If
                                End If
                            End If
                        Else
                            If arrICD9.Length > 1 Then
                                htIcdSnomed.Add(arrICD9(0).Trim, strConceptID.Trim)
                            End If
                        End If
                    Else
                        htIcdSnomed.Add(System.Guid.NewGuid.ToString(), strConceptID.Trim)
                    End If
                    If frm.strProblem.Trim() <> "" Then
                        txt_Problem.Text = frm.strProblem.Trim()
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
            strtxtICD = Nothing
            strOldConcept = Nothing
            strOldDesc = Nothing
            arrICD9 = Nothing
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
    End Sub

    Private Sub btnClearSnomed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSnomed.Click
        txtSnomed.Text = ""
        strCurrentSnomedCode = ""
        strCurrentSnomedDesc = ""
        'Resolving Bug No. 71081:: Problem List->SnoMed Code is saved even after it is removed
        strConceptID = ""
        strDescription = ""
        '
    End Sub

    Private Sub btnClearICD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearICD.Click
        txtICD9.Text = ""
        strCurrentICDCode = ""
        strCurrentICDDesc = ""
    End Sub


    ''Status
    Private Sub rbt_Inactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbt_Inactive.CheckedChanged
        If rbt_Inactive.Checked = True Then
            gb_Immediacy.Enabled = False
            dtResolved.Visible = True
            rbt_Inactive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            ' gb_Immediacy.Enabled = True
            rbt_Inactive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtn_Active_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtn_Active.CheckedChanged
        If rbtn_Active.Checked = True Then
            gb_Immediacy.Enabled = True
            dtResolved.Visible = False
            rbtn_Active.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)
        Else
            gb_Immediacy.Enabled = False
            rbtn_Active.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbInactive.CheckedChanged
        If rbInactive.Checked Then
            ''gb_Immediacy.Enabled = True
            dtResolved.Visible = False
            rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            ''gb_Immediacy.Enabled = False
            rbInactive.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub dtResolved_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtResolved.TextChanged
        If dtResolved.Value.Date < dtpOnsetDate.Value.Date Then
            MessageBox.Show("Resolved Date should be greater than or equal to OnsetDate", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtResolved.Focus()
        End If
    End Sub



    ''Immediacy
    Private Sub rbt_Acute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbt_Acute.CheckedChanged
        If rbt_Acute.Checked = True Then
            rbt_Acute.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbt_Acute.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtn_Chronic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtn_Chronic.CheckedChanged
        If rbtn_Chronic.Checked = True Then
            rbtn_Chronic.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_Chronic.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtn_Unknown_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtn_Unknown.CheckedChanged
        If rbtn_Unknown.Checked = True Then
            rbtn_Unknown.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtn_Unknown.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    ''Comments
    Private Sub btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add.Click
        Try
            Dim strComment() As String
            ofrmAddcomment = New frmAddProblemListComment
            ofrmAddcomment.StartPosition = FormStartPosition.CenterScreen
            ofrmAddcomment.ShowInTaskbar = False
            ofrmAddcomment._TitleText = "Add Comment"
            If ofrmAddcomment.ShowDialog(IIf(IsNothing(ofrmAddcomment.Parent), Me, ofrmAddcomment.Parent)) = Windows.Forms.DialogResult.OK Then
                strSelectedComment = ofrmAddcomment.strComment
                dgComments.Rows.Add()
                dgComments.Visible = True
                dgComments.Item(0, dgComments.Rows.Count - 1).Value = Date.Now
                strComment = Split(strSelectedComment, vbNewLine)
                nCommentCount = strComment.Length
                If nCommentCount > 0 Then
                    dgComments.Rows(dgComments.Rows.Count - 1).Height = dgComments.RowTemplate.Height * strComment.Length + 1
                End If
                dgComments.Item(1, dgComments.Rows.Count - 1).Value = strSelectedComment
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(ofrmAddcomment) = False) Then
                ofrmAddcomment.Dispose()
                ofrmAddcomment = Nothing
            End If
        End Try
    End Sub

    Private Sub btn_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Edit.Click
        Try
            If dgComments.RowCount > 0 Then
                If dgComments.CurrentRow.Index >= 0 Then
                    strComment = dgComments.Item(1, dgComments.CurrentRow.Index).Value
                    ofrmAddcomment = New frmAddProblemListComment(strComment)
                    ofrmAddcomment.StartPosition = FormStartPosition.CenterScreen
                    ofrmAddcomment.ShowInTaskbar = False
                    ofrmAddcomment._TitleText = "Edit Comment"
                    If ofrmAddcomment.ShowDialog(IIf(IsNothing(ofrmAddcomment.Parent), Me, ofrmAddcomment.Parent)) = Windows.Forms.DialogResult.OK Then
                        strSelectedComment = ofrmAddcomment.strComment
                        dgComments.Item(0, dgComments.CurrentRow.Index).Value = Date.Now
                        dgComments.Item(1, dgComments.CurrentRow.Index).Value = strSelectedComment
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(ofrmAddcomment) = False) Then
                ofrmAddcomment.Dispose()
                ofrmAddcomment = Nothing
            End If
            '  ofrmAddcomment.Dispose()
        End Try
    End Sub

    Private Sub btn_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Remove.Click
        Try
            If Not IsNothing(dgComments.CurrentRow) Then  ''added for bugid 70999 if no row exist
                If dgComments.CurrentRow.Index >= 0 Then
                    If MessageBox.Show(" Are you sure you want to delete the  Comment?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        dgComments.Rows.Remove(dgComments.Rows(dgComments.CurrentRow.Index))
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub dgComments_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgComments.CellDoubleClick
        Try
            Dim indx As Integer = e.RowIndex
            If indx >= 0 Then
                strComment = dgComments.Item(1, dgComments.CurrentRow.Index).Value
                ofrmAddcomment = New frmAddProblemListComment(strComment)
                ofrmAddcomment.StartPosition = FormStartPosition.CenterScreen
                ofrmAddcomment.ShowInTaskbar = False
                ofrmAddcomment._TitleText = "Edit Comment"
                If ofrmAddcomment.ShowDialog(IIf(IsNothing(ofrmAddcomment.Parent), Me, ofrmAddcomment.Parent)) = Windows.Forms.DialogResult.OK Then
                    strSelectedComment = ofrmAddcomment.strComment

                    dgComments.Item(0, dgComments.CurrentRow.Index).Value = Date.Now
                    dgComments.Item(1, dgComments.CurrentRow.Index).Value = strSelectedComment
                    dgComments.Visible = True

                End If
                If IsNothing(ofrmAddcomment) = False Then
                    ofrmAddcomment.Dispose()
                    ofrmAddcomment = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub


    Private Sub btn_Priscription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Priscription.Click
        LoadUserGrid()
        dgCustomGrid.Label1.Visible = False
        dgCustomGrid.txtsearch.Visible = False
        dgCustomGrid.Panel2.Visible = False
        pnlcustomTask.Visible = True
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try
            Dim strRx As String = String.Empty
            cmb_Priscription.Text = ""
            cmb_Priscription.Items.Clear()
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    strRx = ""
                    strRx = dgCustomGrid.GetItem(i, 1).ToString & " ~ " & dgCustomGrid.GetItem(i, 2).ToString
                    cmb_Priscription.Items.Add(strRx)
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (cmb_Priscription.Items.Count > 0) Then
                cmb_Priscription.SelectedIndex = 0
            End If
            pnlcustomTask.Visible = False
        End Try
    End Sub


    Private Sub btn_Exams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Exams.Click
        LoadUserGridExam()
        dgCustomGridExam.Label1.Visible = False
        dgCustomGridExam.txtsearch.Visible = False
        dgCustomGridExam.Panel2.Visible = False
        pnlcustomTask.Visible = True
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub dgCustomGridExam_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridExam.CloseClick
        dgCustomGridExam.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Private Sub dgCustomGridExam_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridExam.OKClick
        Try
            'Resolving Bug No 71305 :: Problem list - Application is showing an error message
            'If Not IsNothing(dtExam) Then
            '    dtExam.Dispose()
            '    dtExam = Nothing
            'End If
            Dim dtExam As DataTable = New DataTable()
            dtExam.TableName = "Exam"
            ' Dim Row1 As DataRow

            dtExam.Columns.Add("ExamID")
            dtExam.Columns.Add("VisitID")
            dtExam.Columns.Add("eExamName")

            cmbExams.Text = ""

            cmbExams.DataSource = Nothing
            cmbExams.Items.Clear()

            For i As Int32 = 0 To dgCustomGridExam.C1Task.Rows.Count - 1
                If dgCustomGridExam.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    Dim dr As DataRow = dtExam.NewRow()
                    dr(0) = dgCustomGridExam.C1Task.GetData(i, Col_eExamID + 1).ToString()
                    dr(1) = dgCustomGridExam.C1Task.GetData(i, Col_eVistitID + 1).ToString()
                    dr(2) = dgCustomGridExam.C1Task.GetData(i, Col_eExamName + 1).ToString()
                    dtExam.Rows.Add(dr)
                End If
            Next

            If dtExam.Rows.Count > 0 Then
                cmbExams.DataSource = dtExam
                cmbExams.DisplayMember = "eExamName"
                cmbExams.ValueMember = "ExamID"
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlcustomTask.Visible = False
        End Try
    End Sub


    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                dgCustomGrid.Width = pnlcustomTask.Width
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUserGridExam()
        Try
            AddControlExam()
            If Not IsNothing(dgCustomGridExam) Then
                dgCustomGridExam.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                dgCustomGridExam.Width = pnlcustomTask.Width
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGridExam.Height = pnlcustomTask.Height
                dgCustomGridExam.BringToFront()
                dgCustomGridExam.SetVisible = False
                BindUserGridExam()
                dgCustomGridExam.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddControlExam()

        If Not IsNothing(dgCustomGridExam) Then
            RemoveControlExam()
        End If
        dgCustomGridExam = New CustomTask
        'pnlWordObj.Controls.Add(dgCustomGrid)
        pnlcustomTask.Controls.Add(dgCustomGridExam)
        pnlcustomTask.BringToFront()

    End Sub

    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask

        pnlcustomTask.Controls.Add(dgCustomGrid)
        pnlcustomTask.BringToFront()
    End Sub

    Private Sub RemoveControlExam()
        If Not IsNothing(dgCustomGridExam) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlcustomTask.Controls.Remove(dgCustomGridExam)
            dgCustomGridExam.Visible = False
            dgCustomGridExam.Dispose()
            dgCustomGridExam = Nothing
        End If
    End Sub

    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlcustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    Private Sub BindUserGrid()
        Try

            Dim objclsProblist As New clsPatientProblemList
            Dim dtGrid As DataTable
            '' Fill Diagnosis&Rx Of the Patient for the Visit  in strDia
            dtGrid = objclsProblist.Get_ProblemListRx(_PatientID)
            objclsProblist.Dispose()
            objclsProblist = Nothing
            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dtGrid.Columns.Add(col)
            'col.Dispose()
            'col = Nothing

            If Not IsNothing(dtGrid) Then
                '' For DataBinding Users
                ' If dt.Rows.Count > 0 Then ''commented by Sandip Darade 20090527 to fix the issue regarding prescriptions  
                dtGrid.Columns("DrugName").Caption = "Drug Name"
                dgCustomGrid.datasource(dtGrid.DefaultView)
                ''End If
            End If
            ''Sandip Darade 20090410
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.45
            'dgCustomGrid.C1Task.Cols(2).AllowEditing = True
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.45

            SetDrugValues()
            '  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindUserGridExam()
        Try

            Dim objPatientDetail As New clsPatientDetails
            Dim dtExamgrid As DataTable
            dtExamgrid = objPatientDetail.Fill_PastExams(_PatientID)
            objPatientDetail.Dispose()
            objPatientDetail = Nothing
            CustomDrugsGridStyleExam()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dtExamgrid.Columns.Add(col)
            'col.Dispose()
            'col = Nothing

            If Not IsNothing(dtExamgrid) Then
                dgCustomGridExam.datasource(dtExamgrid.DefaultView)
            End If
            ''Sandip Darade 20090410
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGridExam.C1Task.Width - 5
            dgCustomGridExam.C1Task.Cols.Move(dgCustomGridExam.C1Task.Cols.Count - 1, 0)
            dgCustomGridExam.C1Task.AllowEditing = True

            dgCustomGridExam.C1Task.Cols(Col_eExamID + 1).AllowEditing = True
            dgCustomGridExam.C1Task.Cols(Col_eExamID + 1).Width = _TotalWidth * 0
            dgCustomGridExam.C1Task.Cols(Col_eVistitID + 1).AllowEditing = False
            dgCustomGridExam.C1Task.Cols(Col_eVistitID + 1).Width = _TotalWidth * 0
            'dgCustomGrid.C1Task.Cols(2).AllowEditing = True
            dgCustomGridExam.C1Task.Cols(Col_eReviewedBy + 1).AllowEditing = False
            dgCustomGridExam.C1Task.Cols(Col_eReviewedBy + 1).Width = _TotalWidth * 0

            'Disable Column Editing
            dgCustomGridExam.C1Task.Cols(Col_eDos + 1).AllowEditing = False
            dgCustomGridExam.C1Task.Cols(Col_eExamName + 1).AllowEditing = False
            dgCustomGridExam.C1Task.Cols(Col_eTemplateName + 1).AllowEditing = False
            dgCustomGridExam.C1Task.Cols(Col_eFinished + 1).AllowEditing = False
            dgCustomGridExam.C1Task.Cols(Col_eProviderName + 1).AllowEditing = False

            '13-May-13 Aniket: Resolved Bug 50519
            dgCustomGridExam.C1Task.Cols("Specialty").AllowEditing = False

            setExamValues()
            '  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub setExamValues()
        For j As Integer = 0 To cmbExams.Items.Count - 1
            cmbExams.Text = ""
            cmbExams.SelectedIndex = j

            For i As Int32 = 0 To dgCustomGridExam.C1Task.Rows.Count - 1
                If dgCustomGridExam.GetItem(i, Col_eExamID + 1).ToString.Trim = Convert.ToString(cmbExams.SelectedValue) Then
                    dgCustomGridExam.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If
            Next
        Next
    End Sub

    Private Sub SetDrugValues()

        For j As Integer = 0 To cmb_Priscription.Items.Count - 1
            cmb_Priscription.Text = ""
            cmb_Priscription.SelectedIndex = j

            Dim _Drugs As String = cmb_Priscription.SelectedItem
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                'For _index As Int32 = 0 To _Drugs.Length - 1
                Dim _drugname As String() = Split(_Drugs, "~")
                If dgCustomGrid.GetItem(i, 1).ToString.Trim = _drugname(0).Trim Then
                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If
                'Next
            Next
        Next
    End Sub

    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Redraw = False
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_Count
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")
            .SetData(0, Col_DrugName, "Drug Name")
            .Cols(Col_DrugName).Width = _TotalWidth * 0.45
            ' .Cols(Col_DrugName).AllowEditing = False
            .SetData(0, Col_Dosage, "Dosage")
            .Cols(Col_Dosage).Width = _TotalWidth * 0.45
            .Cols(Col_Dosage).AllowEditing = False
            .Redraw = True
        End With
    End Sub

    Public Sub CustomDrugsGridStyleExam()
        Dim _TotalWidth As Single = dgCustomGridExam.C1Task.Width - 5
        ' '' Show Drugs Info
        With dgCustomGridExam.C1Task
            .Redraw = False
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_eCount
            .AllowEditing = True
            .SetData(0, Col_eCheck, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_eCheck).Width = _TotalWidth * 0.1
            .Cols(Col_eCheck).AllowEditing = True
            .Cols(Col_eCheck).DataType = System.Type.GetType("System.Boolean")
            .SetData(0, Col_eExamID, "ExamID")
            .Cols(Col_eExamID).Width = _TotalWidth * 0.0
            .Cols(Col_eExamID).Visible = False
            .Cols(Col_eExamID).AllowEditing = False
            .SetData(0, Col_eVistitID, "VisitID")
            .Cols(Col_eVistitID).Width = _TotalWidth * 0
            .Cols(Col_eVistitID).AllowEditing = False
            .Cols(Col_eVistitID).Visible = False
            .SetData(0, Col_eDos, "DOS")
            .Cols(Col_eDos).Width = _TotalWidth * 0.45
            .Cols(Col_eDos).AllowEditing = False
            .SetData(0, Col_eExamName, "Exam Name")
            .Cols(Col_eExamName).Width = _TotalWidth * 0.45
            .Cols(Col_eExamName).AllowEditing = False
            .SetData(0, Col_eTemplateName, "Template Name")
            .Cols(Col_eTemplateName).Width = _TotalWidth * 0.45
            .Cols(Col_eTemplateName).AllowEditing = False
            .SetData(0, Col_eFinished, "Finished")
            .Cols(Col_eFinished).Width = _TotalWidth * 0.45
            .Cols(Col_eFinished).AllowEditing = False
            .SetData(0, Col_eProviderName, "Provider Name")
            .Cols(Col_eProviderName).Width = _TotalWidth * 0.45
            .Cols(Col_eProviderName).AllowEditing = False
            .SetData(0, Col_eReviewedBy, "ReviewedBy")
            .Cols(Col_eReviewedBy).Width = _TotalWidth * 0
            .Cols(Col_eReviewedBy).AllowEditing = False
            .Cols(Col_eReviewedBy).Visible = False
            .Redraw = True
        End With
    End Sub


    Public Sub Fill_Diagnosis()
        Dim objclsProblist As New clsPatientProblemList
        Dim dtICD9Immediancy As DataTable = Nothing
        Try
            If btnICD9Click Then
                If strCurrentICDCode <> "" AndAlso strCurrentICDDesc <> "" Then
                    dtICD9Immediancy = objclsProblist.Get_ICD9ImmediancyDefault(strCurrentICDCode, strCurrentICDDesc)
                    If dtICD9Immediancy IsNot Nothing Then
                        If dtICD9Immediancy.Rows.Count > 0 Then
                            Select Case Convert.ToInt64(dtICD9Immediancy.Rows(0)("nImmediacyDefault"))
                                Case 1
                                    rbt_Acute.Checked = True
                                Case 2
                                    rbtn_Chronic.Checked = True
                                Case 3
                                    rbtn_Unknown.Checked = True
                            End Select
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objclsProblist.Dispose()
            objclsProblist = Nothing
            If dtICD9Immediancy IsNot Nothing Then
                dtICD9Immediancy.Dispose()
                dtICD9Immediancy = Nothing
            End If
        End Try
    End Sub


    Private Sub LoadDefaultSnoomed()
        Dim dtProblemSnomed As DataTable = Nothing
        Dim objclsSnomed As New clsSnomedIcdMap
        Dim frm As gloSnoMed.FrmSelectProblem = Nothing
        Try
            If strCurrentICDCode <> "" Then
                dtProblemSnomed = objclsSnomed.Get_DefaultSnomedForICD(strCurrentICDCode, strCurrentICDDesc, strCurrentICDRevision, GetConnectionString())
                If dtProblemSnomed IsNot Nothing Then
                    If dtProblemSnomed.Rows.Count = 1 Then
                        strCurrentSnomedCode = dtProblemSnomed.Rows(0)("CONCEPTID")
                        strCurrentSnomedDesc = dtProblemSnomed.Rows(0)("TermDescription")
                        If htIcdSnomed.ContainsKey(strCurrentICDCode) Then
                            htIcdSnomed.Item(strCurrentICDCode) = strCurrentSnomedCode
                        Else
                            htIcdSnomed.Add(strCurrentICDCode, strCurrentSnomedCode)
                        End If
                    ElseIf dtProblemSnomed.Rows.Count > 1 Then
                        If _ISSmonedCodeMandatory Then
                            gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                            frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())
                            frm.blnIsProblem = True
                            If strCurrentICDRevision = 9 Then
                                frm.strCodeSystem = "ICD9"
                            ElseIf strCurrentICDRevision = 10 Then
                                frm.strCodeSystem = "ICD10"
                            End If
                            frm.txtSMSearch.Text = Convert.ToString(strCurrentICDCode)
                            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                            If frm._DialogResult = True Then
                                'Bug No. 71033 Resolved::Problem List - Application does not display selected ICD code in ICD9/10 field.
                                Dim strSelectedICD As String = ""
                                If frm.strICD10 <> "" Then
                                    strSelectedICD = frm.strICD10
                                ElseIf frm.strICD9 <> "" Then
                                    strSelectedICD = frm.strICD9
                                End If
                                If strSelectedICD <> "" Then
                                    txtICD9.Text = strSelectedICD.Replace(":", "-")
                                End If
                                strSelectedICD = Nothing
                                '
                                strCurrentSnomedCode = frm.strSelectedConceptID
                                If frm.strSelectedConceptID = "" AndAlso frm.strSelectedDescription = "" Then
                                    strCurrentSnomedDesc = ""
                                Else
                                    strCurrentSnomedDesc = frm.strSelectedConceptID + "-" + frm.strSelectedDescription
                                End If
                            Else
                                strCurrentSnomedCode = ""
                                strCurrentSnomedDesc = ""
                            End If

                            If htIcdSnomed.ContainsKey(strCurrentICDCode) Then
                                htIcdSnomed.Item(strCurrentICDCode) = strCurrentSnomedCode
                            Else
                                htIcdSnomed.Add(strCurrentICDCode, strCurrentSnomedCode)
                            End If
                        Else
                            strCurrentSnomedCode = ""
                            strCurrentSnomedDesc = ""
                        End If
                    ElseIf dtProblemSnomed.Rows.Count < 1 Then
                        strCurrentSnomedCode = ""
                        strCurrentSnomedDesc = ""
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtProblemSnomed) Then
                dtProblemSnomed.Dispose()
                dtProblemSnomed = Nothing
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
            objclsSnomed = Nothing

        End Try
    End Sub


    Private oLateralityListControl As gloListControl.gloListControl
    Private ofrmLateralityList As frmViewListControl
    Private Sub btnBrowseLaterality_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseLaterality.Click
        Try
            ofrmLateralityList = New frmViewListControl
            oLateralityListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.MUCQMRefusedCode, False, Me.Width)
            oLateralityListControl.ControlHeader = "Laterality"
            oLateralityListControl.bShowLateralityCodes = True
            AddHandler oLateralityListControl.ItemSelectedClick, AddressOf oLateralityListControl_ItemSelectedClick
            AddHandler oLateralityListControl.ItemClosedClick, AddressOf oLateralityListControl_ItemClosedClick
            ofrmLateralityList.Controls.Add(oLateralityListControl)
            oLateralityListControl.Dock = DockStyle.Fill
            oLateralityListControl.BringToFront()

            oLateralityListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmLateralityList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmLateralityList.Text = "Laterality Code"
            ofrmLateralityList.ShowDialog(IIf(IsNothing(CType(ofrmLateralityList, Control).Parent), Me, CType(ofrmLateralityList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmLateralityList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oLateralityListControl.ItemSelectedClick, AddressOf oLateralityListControl_ItemSelectedClick
                RemoveHandler oLateralityListControl.ItemClosedClick, AddressOf oLateralityListControl_ItemClosedClick
                oLateralityListControl.Dispose()
                oLateralityListControl = Nothing
            End If

            If IsNothing(ofrmLateralityList) = False Then
                ofrmLateralityList.Dispose()
                ofrmLateralityList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearLaterality_Click(sender As System.Object, e As System.EventArgs) Handles btnClearLaterality.Click
        txtLaterality.Clear()
        strCurrentLateralityCode = String.Empty
        strCurrentLateralityDesc = String.Empty

    End Sub

    Private Sub oLateralityListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmLateralityList.Close()
    End Sub

    Private Sub oLateralityListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oLateralityListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oLateralityListControl.SelectedItems.Count - 1
                    txtLaterality.Text = oLateralityListControl.SelectedItems(i).Code + " - " + oLateralityListControl.SelectedItems(i).Description
                    strLateralityCode = Convert.ToString(oLateralityListControl.SelectedItems(i).Code)
                    strLateralityDesc = Convert.ToString(oLateralityListControl.SelectedItems(i).Description)
                Next
                ofrmLateralityList.Close()
            Else
                txtLaterality.Clear()
                ofrmLateralityList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ''
    Public Sub Fill_ConcernStatus()
        Dim objclsProblist As New clsPatientProblemList
        Dim dtConcernStatus As DataTable = Nothing
        Try


            dtConcernStatus = objclsProblist.Get_CDACodeSystemtype("concernstatus")
            If dtConcernStatus IsNot Nothing Then
                If dtConcernStatus.Rows.Count > 0 Then
                    For Each dr As DataRow In dtConcernStatus.Rows
                        cmbConcernStatus.Items.Add(dr("sEMRDisplayName"))

                    Next

                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objclsProblist.Dispose()
            objclsProblist = Nothing
            If dtConcernStatus IsNot Nothing Then
                dtConcernStatus.Dispose()
                dtConcernStatus = Nothing
            End If
        End Try
    End Sub
    Dim dtProblemType As DataTable = Nothing
    Public Sub Fill_ProblemType()
        Dim objclsProblist As New clsPatientProblemList

        Try


            dtProblemType = objclsProblist.Get_CDACodeSystemtype("ProblemType")
            If dtProblemType IsNot Nothing Then
                If dtProblemType.Rows.Count > 0 Then

                    cmbProblemType.DataSource = dtProblemType
                    cmbProblemType.DisplayMember = "sEMRDisplayName"
                    cmbProblemType.ValueMember = "sCode"

                    cmbProblemType.SelectedValue = CDAProblemType


                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objclsProblist.Dispose()
            objclsProblist = Nothing
            
        End Try
    End Sub

    
    Private Sub cmbProblemType_Leave(sender As Object, e As EventArgs)
        ToolTipcmbProblemType.Hide(cmbProblemType)
    End Sub

    Private Sub cmbProblemType_DropDownClosed(sender As Object, e As EventArgs)
        ToolTipcmbProblemType.Hide(cmbProblemType)
    End Sub

    Private Sub cmbProblemType_DrawItem(sender As Object, e As DrawItemEventArgs)

        '12-Aug-15 Aniket: Resolving Bug #85824: EMR: Case- Tooltip should get displayed for large case's name
        Dim text As String

        Try

            If e.Index < 0 Then
                Return
            End If

            text = cmbProblemType.GetItemText(cmbProblemType.Items(e.Index))

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(text, e.Font, br, e.Bounds)
            End Using
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If Len(text) + 133 < cmbProblemType.Width Then
                    ToolTipcmbProblemType.Hide(cmbProblemType)

                Else
                    ToolTipcmbProblemType.Show(text, cmbProblemType, e.Bounds.Right, e.Bounds.Bottom)
                End If

            End If
            e.DrawFocusRectangle()

        Catch ex As Exception

        End Try

    End Sub

    
End Class
