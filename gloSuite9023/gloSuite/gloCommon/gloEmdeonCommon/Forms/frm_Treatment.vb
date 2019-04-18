Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary
Imports System.Windows.Forms

Public Class frm_Treatment

#Region " Private Variables "
    Private _VisitID As Int64
    Private _ExamID As Int64
    Private _PatientID As Int64

    Private _DataOfService As DateTime
    Private _PrimaryDiagnosis As String
    Private _isReadOnly As Boolean

    Private WithEvents _PatientStrip As gloUserControlLibrary.gloUC_PatientStrip
    Private isC1Loading As Boolean = False
    Private isFormLoading As Boolean = False
    Private isGridListOpen As Boolean
    Private isSaveClicked As Boolean = False

    Private oToolTip As New ToolTip

#End Region

    '' added by Abhijeet on date 20100514
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private gloICD9To10SearchUC As gloUIControlLibrary.WPFUserControl.ICD10.gloICD9To10MappingSearch = Nothing
    Dim dtActiveCPTTable As DataTable

#Region " Public Properties "


    Public ReadOnly Property PrimaryDiagnosis() As String
        Get
            Return _PrimaryDiagnosis
        End Get
    End Property
#End Region

#Region " Constructor "
    Public Sub New(ByVal nExamID As Int64, ByVal nVisitID As Int64, ByVal dtDateOfService As DateTime, ByVal sPrimaryDiagnosis As String, ByVal isReadOnly As Boolean, ByVal nPatientID As Int64)
        InitializeComponent()
        gloUCTreatment.DatabaseConnectionString = GetConnectionString()
        _ExamID = nExamID
        _VisitID = nVisitID

        'If nPatientID = 0 Then
        '    _PatientID = gnPatientID
        'Else
        _PatientID = nPatientID
        'End If
        'gnPatientID = _PatientID

        _DataOfService = dtDateOfService.Date
        gloUCTreatment.DOS = _DataOfService
        _PrimaryDiagnosis = sPrimaryDiagnosis

        _isReadOnly = isReadOnly

        If gblnShow8ICD9 Then
            gloUCTreatment.ICD9Count = gloUserControlLibrary.enumIC9Count.Show_8_ICD9
        Else
            gloUCTreatment.ICD9Count = gloUserControlLibrary.enumIC9Count.Show_4_ICD9
        End If

        If gblnShow4Modifier Then
            gloUCTreatment.ModifierCount = gloUserControlLibrary.enumModifierCount.Show_4_Modifier
        Else
            gloUCTreatment.ModifierCount = gloUserControlLibrary.enumModifierCount.Show_2_MOdifier
        End If

        '' Checking examid as to disable PT_Billing feature  from order & results / order templates
        If nExamID <> 0 Then
            If gblnIsExamPTBillingEnabled And Not isReadOnly Then
                gloUCTreatment.IsExamPTBillingEnabled = gblnIsExamPTBillingEnabled
            End If
        End If

        '' Code by : Abhijeet Farkande on date : 20100514
        '' changes : accessing the message box caption from app setting 
        If gstrMessageBoxCaption = "" Then
            If Not IsNothing(appSettings) Then
                If Not IsNothing(appSettings("MessageBOXCaption")) Then
                    If appSettings("") <> "" Then
                        gstrMessageBoxCaption = appSettings("MessageBOXCaption").ToString()
                    Else
                        gstrMessageBoxCaption = "gloEMR"
                    End If
                Else
                    gstrMessageBoxCaption = "gloEMR"
                End If
            Else
                gstrMessageBoxCaption = "gloEMR"
            End If
        End If
        '' End of code by Abhijeet for accessing message box caption                

        Me.gloICD9To10SearchUC = New gloUIControlLibrary.WPFUserControl.ICD10.gloICD9To10MappingSearch(GetConnectionString())
        AddHandler gloICD9To10SearchUC.ControlClosed, AddressOf gloICD9To10SearchUC_ControlClosed
        elementHostSearch.Child = gloICD9To10SearchUC

    End Sub
#End Region

#Region " C1 Constants "
    Private Const COL_PRIMARY = 0
    Private Const COL_LINE = 1
    Private Const COL_CODE = 2
    Private Const COL_DESC = 3
    Private Const COL_COUNT = 4
#End Region

#Region " Form Load Event "
    Private Sub frm_Treatment_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isSaveClicked = False And _isReadOnly = False And gloUCTreatment.TreatmentModified Then
            Dim _Result As DialogResult = MessageBox.Show("Do you want to save changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If _Result = Windows.Forms.DialogResult.Yes Then
                If SaveTreatment() = False Then
                    e.Cancel = True
                    Exit Sub
                End If
            ElseIf _Result = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
                Exit Sub
            End If
        End If

        If oToolTip IsNot Nothing Then
            oToolTip.RemoveAll()
            oToolTip.Dispose()
            oToolTip = Nothing
        End If

        If gloUCTreatment IsNot Nothing Then
            gloUCTreatment.Dispose()
        End If

    End Sub
    Private Sub frm_Treatment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblCopyRight.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain
        lblCopyRight.Refresh()
        isFormLoading = True
        dtActiveCPTTable = New DataTable()
        Dim colCPTCode As New DataColumn("sCPTCode")
        Dim colFromDate As New DataColumn("dtFromDate")
        Dim colToDate As New DataColumn("dtToDate")

        dtActiveCPTTable.Columns.Add(colCPTCode)
        dtActiveCPTTable.Columns.Add(colFromDate)
        dtActiveCPTTable.Columns.Add(colToDate)
        oToolTip.SetToolTip(btnDown, " Move Line Down ")
        oToolTip.SetToolTip(btnUp, " Move Line Up ")
        oToolTip.SetToolTip(btnRight, " Move Right ")
        oToolTip.SetToolTip(btnLeft, " Move Left ")
        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIcd10Transition Then
            rbICD10.Checked = True
            rbICD9.Checked = False
            gloUCTreatment.ICDRevision = 10
        Else
            rbICD10.Checked = False
            rbICD9.Checked = True
            gloUCTreatment.ICDRevision = 9
        End If
        Try
            LoadPatientStrip()
            DesignDiagnosisGrid()

            If _VisitID > 0 Then
                FillTreatment()

                '' TO SELECT PRIMARY DIAGNOSIS FROM LIST ''
                If _PrimaryDiagnosis <> "" Then
                    Dim _Code() As String = _PrimaryDiagnosis.Split(" ")

                    If _Code.Length > 0 Then

                        For iRow As Integer = 1 To C1Diagnosis.Rows.Count - 1
                            If C1Diagnosis.GetData(iRow, COL_CODE) = _Code(0) Then
                                'C1Diagnosis.SetCellCheck(iRow, COL_PRIMARY, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                C1Diagnosis.Select(iRow, COL_PRIMARY)
                                Exit For
                            End If
                        Next

                    End If
                End If

                txtPrimaryDiagnosis.Text = _PrimaryDiagnosis


            End If

            If _isReadOnly = True Then
                gloUCTreatment.DisableGrid = False
                C1Diagnosis.Enabled = False
                tls_btnAddLine.Visible = False
                tls_btnRemoveLine.Visible = False
                tls_btnSaveNClose.Visible = False
                btnDown.Visible = False
                btnUp.Visible = False
                btnRight.Visible = False
                btnLeft.Visible = False
                pnlBottom.Visible = False
                txtPrimaryDiagnosis.Enabled = False
            Else
                gloUCTreatment.Focus()
                gloUCTreatment.Select()
                gloUCTreatment.SelectLastCPT()
            End If

            If _ExamID = 0 Then
                txtPrimaryDiagnosis.Visible = False
                Label1.Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isFormLoading = False
        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region

#Region " ToolStrip / Button Click Events "

    Private Sub tls_Top_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_Top.ItemClicked
        '' WHILE ENTERING DATA IN GRID, IF USER CLICK ON TOOLSTRIP BUTTONS. CHANGE FOCUS OF CELL TO CLOSE INTERNAL GRID CONTROL ''
        If isGridListOpen = True Then
            gloUCTreatment.SelectCurrentRowDOS()
        Else
            tls_Top.Focus()
        End If

        Select Case e.ClickedItem.Tag
            Case "Cancel"
                Me.Close()
            Case "AddLine"
                gloUCTreatment.AddLine()
            Case "RemoveLine"
                gloUCTreatment.RemoveLine()
            Case "SaveNClose"
                If SaveTreatment() = True Then
                    Me.Close()
                End If

        End Select
    End Sub

    Private Sub mnuAddLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddLine.Click
        tls_btnAddLine.PerformClick()
    End Sub
    Private Sub mnuRemoveLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveLine.Click
        tls_btnRemoveLine.PerformClick()
    End Sub
    Private Sub mnuSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        tls_btnSaveNClose.PerformClick()
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        MoveDown()
    End Sub
    Private Sub btnLeft_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeft.Click
        MoveLeft()
    End Sub
    Private Sub btnRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRight.Click
        MoveRight()
    End Sub
    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        MoveUp()
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallDown_Orange
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnDown_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallDown
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnUp_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallUp_Orange
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnUp_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallUp
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnLeft_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeft.MouseHover
        btnLeft.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallLeft_Orange
        btnLeft.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnLeft_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeft.MouseLeave
        btnLeft.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallLeft
        btnLeft.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnRight_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRight.MouseHover
        btnRight.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallRight_Orange
        btnRight.BackgroundImageLayout = ImageLayout.Center
    End Sub
    Private Sub btnRight_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRight.MouseLeave
        btnRight.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.SmallRight
        btnRight.BackgroundImageLayout = ImageLayout.Center
    End Sub

#End Region

#Region " Private Methods "
    Private Function SaveTreatment() As Boolean
        Try
            If gloUCTreatment.ValidateDiagnosisUnit() = False Then
                MessageBox.Show("Units should be less than 999.9999", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            '' VALIDATION FOR PRIMARY DIAGNOSIS ''
            Dim _FoundPrimary As Boolean = False

            If txtPrimaryDiagnosis.Text <> "" Then
                _FoundPrimary = True
                _PrimaryDiagnosis = txtPrimaryDiagnosis.Text
            Else
                _PrimaryDiagnosis = ""
            End If

            '' WHEN PRIMARY COLUMN IS HIDDEN '' DON'T VALIDATE FOR IT ''
            If _ExamID = 0 Then _FoundPrimary = True

            'If _FoundPrimary = False Then
            '    MessageBox.Show("Please select primary diagnosis.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Function
            'End If

            '' FETCH DATA FROM TREATMENT CONTROL AND SAVE IT ''
            Dim arrTreatment As ArrayList
            arrTreatment = gloUCTreatment.GetTreatment
            If arrTreatment IsNot Nothing Then
                Dim oDiagnosis As New ClsDiagnosisDBLayer
                If arrTreatment.Count > 0 Then

                    'Check Active CPT
                    For i As Integer = 0 To arrTreatment.Count - 1
                        If Convert.ToString(DirectCast(arrTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).Code) = "" And Convert.ToString(DirectCast(arrTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory) <> "" Then
                            Dim drRow As DataRow = dtActiveCPTTable.NewRow()
                            drRow("sCPTCode") = Convert.ToString(DirectCast(arrTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory)
                            drRow("dtFromDate") = gloDateMaster.gloDate.DateAsNumber(GetVisitdate(_VisitID))
                            drRow("dtToDate") = 0
                            dtActiveCPTTable.Rows.Add(drRow)
                        End If
                    Next
                    Dim CPTAlert As String = gloGlobal.gloPMGlobal.getCPTDeativatedCPT(dtActiveCPTTable)
                    dtActiveCPTTable.Clear()
                    If (CPTAlert <> "") Then
                        Dim dResult As DialogResult = MessageBox.Show(CPTAlert, "Diagnosis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                        If dResult.ToString() = "Cancel" Then
                            SaveTreatment = False
                            Exit Function
                        End If
                    End If
                    '

                    '' SAVE DATA IN ExamICDCPT TABLE ''
                    oDiagnosis.SaveDiagTreatmentAssociation(_ExamID, _PatientID, _VisitID, arrTreatment, True)
                    isSaveClicked = True
                    Return True
                Else
                    '' DELETE RECORDS FOR FOLLOWING PARAMETERS ''
                    oDiagnosis.CleanDiagTreatmentAssociation(_ExamID, _VisitID)
                    isSaveClicked = True
                    Return True
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub FillTreatment()
        Try
            Dim oDiagnosis As New ClsDiagnosisDBLayer()
            Dim arrTreatment As New ArrayList

            arrTreatment = oDiagnosis.GetCPTDrivenDiagnosis(_ExamID, _VisitID, _PatientID)

            If arrTreatment IsNot Nothing Then
                If arrTreatment.Count > 0 Then
                    gloUCTreatment.FillTreatment(arrTreatment)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPatientStrip()
        _PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip
        _PatientStrip.ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.PatientExam)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.BringToFront()

        pnlToolStrip.SendToBack()

        '  _PatientStrip.f()
        _PatientStrip.DTP.CustomFormat = "MM/dd/yyyy"

        _PatientStrip.DTPValue = Format(_DataOfService, "MM/dd/yyyy")

        Me.pnlMain.Controls.Add(_PatientStrip)

        _PatientStrip.DTPEnabled = False
    End Sub

    Private Sub DesignDiagnosisGrid()
        C1Diagnosis.Rows.Count = 1
        C1Diagnosis.Rows.Fixed = 1
        C1Diagnosis.Cols.Count = COL_COUNT
        C1Diagnosis.Cols.Fixed = 0

        C1Diagnosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        C1Diagnosis.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        C1Diagnosis.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None

        C1Diagnosis.Cols(COL_PRIMARY).DataType = Type.GetType("System.Boolean")

        C1Diagnosis.Cols(COL_PRIMARY).Width = 60
        C1Diagnosis.Cols(COL_CODE).Width = 60
        C1Diagnosis.Cols(COL_DESC).Width = C1Diagnosis.Width - 60

        C1Diagnosis.Cols(COL_LINE).Visible = False
        C1Diagnosis.Cols(COL_PRIMARY).Visible = False

        C1Diagnosis.SetData(0, COL_PRIMARY, "Primary")
        C1Diagnosis.SetData(0, COL_CODE, "Code")
        C1Diagnosis.SetData(0, COL_DESC, "Description")

        C1Diagnosis.Cols(COL_CODE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Diagnosis.Cols(COL_DESC).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        C1Diagnosis.Cols(COL_CODE).AllowEditing = False
        C1Diagnosis.Cols(COL_DESC).AllowEditing = False


    End Sub

    Private Sub MoveUp()
        gloUCTreatment.MoveUp()
    End Sub
    Private Sub MoveDown()
        gloUCTreatment.MoveDown()
    End Sub
    Private Sub MoveRight()
        gloUCTreatment.MoveRight()
    End Sub
    Private Sub MoveLeft()
        gloUCTreatment.MoveLeft()
    End Sub

    Private Sub RemoveICD9(ByVal oICD9 As gloGeneralItem.gloItem)
        Try
            For iRow As Integer = C1Diagnosis.Rows.Count - 1 To 1 Step -1
                If C1Diagnosis.GetData(iRow, COL_CODE) = oICD9.Code Then
                    C1Diagnosis.Rows.Remove(iRow)
                    If txtPrimaryDiagnosis.Text = oICD9.Code & " " & oICD9.Description Then
                        txtPrimaryDiagnosis.Clear()

                        '' IF PRIMARY DIAGNOSIS REMOVED, THEN MAKE FIRST ICD9 AS PRIMARY ''
                        If C1Diagnosis.Rows.Count > 1 Then
                            txtPrimaryDiagnosis.Text = C1Diagnosis.GetData(1, COL_CODE) & " " & C1Diagnosis.GetData(1, COL_DESC)
                        End If

                    End If
                    Exit Sub
                End If
            Next
        Catch
        End Try
    End Sub
#End Region

#Region " gloUC_Treatment Events "

    Private Sub gloUCTreatment_GridListClosed() Handles gloUCTreatment.GridListClosed
        btnDown.Enabled = True
        btnUp.Enabled = True
        btnRight.Enabled = True
        btnLeft.Enabled = True
        isGridListOpen = False
    End Sub

    Private Sub gloUCTreatment_GridListLoaded() Handles gloUCTreatment.GridListLoaded
        btnDown.Enabled = False
        btnUp.Enabled = False
        btnRight.Enabled = False
        btnLeft.Enabled = False
        isGridListOpen = True
    End Sub

    Private Sub gloUCTreatment_ICD9_Inserted(ByVal oICD9 As Object) Handles gloUCTreatment.ICD9_Inserted
        Try
            oICD9 = CType(oICD9, gloGeneralItem.gloItem)
            Dim _FoundRow As Integer = -1
            If C1Diagnosis.Rows.Count = 1 Then
                _FoundRow = -1
            Else
                _FoundRow = C1Diagnosis.FindRow(oICD9.Code, 1, COL_CODE, False, True, False)
            End If

            If _FoundRow = -1 Then '' ADD NEW ROW ''
                C1Diagnosis.Rows.Add()
                Dim _Row As Integer = C1Diagnosis.Rows.Count - 1

                C1Diagnosis.SetData(_Row, COL_CODE, oICD9.Code)
                C1Diagnosis.SetData(_Row, COL_DESC, oICD9.Description)
                If _Row = 1 And isFormLoading = False Then '' BY DEFAULT FIRST DIAGNOSIS WILL BE PRIMARY ''
                    'C1Diagnosis.SetCellCheck(_Row, COL_PRIMARY, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    txtPrimaryDiagnosis.Text = oICD9.Code & " " & oICD9.Description
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gloUCTreatment_ICD9_Removed(ByVal oICD9s As Object) Handles gloUCTreatment.ICD9_Removed
        Try
            oICD9s = CType(oICD9s, gloGeneralItem.gloItems)
            For i As Integer = 0 To oICD9s.Count - 1
                RemoveICD9(oICD9s(i))
            Next
        Catch
        End Try
    End Sub

#End Region

#Region " C1 Events "
    Private Sub C1Diagnosis_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Diagnosis.AfterEdit
        Try
            If isC1Loading = False Then
                isC1Loading = True
                For iRow As Integer = 1 To C1Diagnosis.Rows.Count - 1
                    If e.Row <> iRow Then
                        C1Diagnosis.SetCellCheck(iRow, COL_PRIMARY, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                Next
                isC1Loading = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub C1Diagnosis_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Diagnosis.MouseDoubleClick
        Try
            If C1Diagnosis.RowSel > 0 Then
                txtPrimaryDiagnosis.Text = C1Diagnosis.GetData(C1Diagnosis.RowSel, COL_CODE) & " " & C1Diagnosis.GetData(C1Diagnosis.RowSel, COL_DESC)
                gloUCTreatment.TreatmentModified = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Diagnosis_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Diagnosis.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim oHit As C1.Win.C1FlexGrid.HitTestInfo
                oHit = C1Diagnosis.HitTest(e.X, e.Y)
                If oHit.Row > 0 Then
                    C1Diagnosis.Row = oHit.Row
                End If
            End If
        Catch
        End Try
    End Sub
#End Region

    Private Sub rbICD10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD10.CheckedChanged
        If rbICD9.Checked Then
            gloUCTreatment.GetIcdRevision(9)
        End If
    End Sub

    Private Sub rbICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD9.CheckedChanged
        If rbICD10.Checked Then
            gloUCTreatment.GetIcdRevision(10)
        End If
    End Sub

   
   
    Private Sub gloUCTreatment_MouseDoubleClick() Handles gloUCTreatment.MouseDoubleClick
        If rbICD9.Checked And Not rbICD10.Checked Then
            gloUCTreatment.ICDRevision = 9
        ElseIf Not rbICD9.Checked And rbICD10.Checked Then
            gloUCTreatment.ICDRevision = 10
        End If
    End Sub

    Private Sub ShowCodingRule()

        Dim selectedDxCode As ArrayList = Nothing


        Try

            If Not IsNothing(gloUCTreatment) AndAlso gloUCTreatment.RowSel > 0 Then

                selectedDxCode = gloUCTreatment.GetSelectedCellDxCode()
                If Not IsNothing(selectedDxCode) AndAlso selectedDxCode.Count > 0 Then

                    Dim dxCode As String = ""
                    Dim dxDescription As String = ""

                    dxCode = Convert.ToString(selectedDxCode(0))
                    dxDescription = Convert.ToString(selectedDxCode(1))

                    If dxCode.Trim <> "" AndAlso dxDescription.Trim <> "" AndAlso gloUCTreatment.GetICDRevisionOfSelectedCell() = 10 Then

                        Dim objCodeRule As New gloUIControlLibrary.WPFForms.frmShowCodingRules(dxCode, dxDescription, GetConnectionString())
                        objCodeRule.LoadNotes()

                        If objCodeRule.NoData Then

                            MessageBox.Show("No coding rules available for code " + dxCode, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            objCodeRule.ShowDialog()
                            objCodeRule.Close()
                            objCodeRule = Nothing
                        End If
                    Else
                        MessageBox.Show("Please select ICD10 code to view coding rules.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("Please select ICD10 code to view coding rules.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If


            End If



        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)

        Finally

        End Try

    End Sub

    Private Sub tlsbtnCodingRule_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnCodingRule.Click
        Call ShowCodingRule()
    End Sub

#Region "ICD 9 to 10 control show hide"
    Private Sub btn_Right_Click(sender As System.Object, e As System.EventArgs) Handles btn_Right.Click, btnMappingClose.Click
        pnlElementHosts.Visible = True
        pnlSmallStrip.Visible = False
    End Sub

    Private Sub gloICD9To10SearchUC_ControlClosed()
        pnlElementHosts.Visible = False
        pnlSmallStrip.Visible = True
    End Sub
#End Region
End Class