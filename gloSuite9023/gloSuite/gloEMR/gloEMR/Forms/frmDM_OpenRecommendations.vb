Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloEMR.gloStream.DiseaseManagement
Imports System.Drawing
Imports System.IO
Imports C1.Win.C1FlexGrid


Public Class frmDM_OpenRecommendations
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

#Region " Private and Public variable declarations "

    Private COL_PatientId As Int16 = 0
    Private COL_PatientCode As Int16 = 1
    Private COL_PatientFName As Int16 = 2
    Private COL_PatientMName As Int16 = 3
    Private COL_PatientLName As Int16 = 4
    Private COL_RuleId As Int16 = 5
    Private COL_ProviderName As Int16 = 6
    Private COL_RecommendationID As Int16 = 7
    Private COL_Announced As Int16 = 8
    Private COL_Name As Int16 = 9
    Private COL_Description As Int16 = 10
    Private COL_Status As Int16 = 11
    Private COL_nStatus As Int16 = 12
    Private COL_sNote As Int16 = 13
    Private COL_sMobile As Int16 = 14
    Private COL_sPhone As Int16 = 15
    Private COL_Email As Int16 = 16
    Private COL_ProviderID As Int16 = 17


    Private m_PatientID As Long
    Dim _dtRecommendation As DataTable = Nothing
    Dim _dv As DataView = Nothing
    Dim _dt As DataTable = Nothing
#End Region ' Private and Public variable declarations '

#Region " Property Procedures "

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID   'Curent patient variable(Local variable) for this module 
        End Get
    End Property

#End Region ' Property Procedures '

#Region " Constructor "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

#End Region ' Constructor '

#Region " Form load, activated and closing event "

    Private Sub frmDM_PatientSpecific_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, "Open Recommendation Opened", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''bgWorkerOpenRecommendations.WorkerSupportsCancellation = True
            'AddHandler bgWorkerOpenRecommendations.DoWork, AddressOf bgWorkerOpenRecommendations_DoWork
            'AddHandler bgWorkerOpenRecommendations.RunWorkerCompleted, AddressOf bgWorkerOpenRecommendations_RunWorkerCompleted
            BindHealthPlan()
            FillRulesDropDown()
            FillProviderCombo()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmDM_DisplayRecommendations_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If bgWorkerOpenRecommendations.IsBusy Then
            e.Cancel = True
        Else
            e.Cancel = False
        End If

        If _dtRecommendation IsNot Nothing Then
            _dtRecommendation.Dispose()
            _dtRecommendation = Nothing
        End If

        If _dv IsNot Nothing Then
            _dv.Dispose()
            _dv = Nothing
        End If

        If _dt IsNot Nothing Then
            _dt.Dispose()
            _dt = Nothing
        End If
    End Sub

#End Region ' Form load, activated and closing event '

#Region " Form Toolstrip button click events "



    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click

        Me.Cursor = Cursors.WaitCursor
        txtSearch.Text = ""

        Try
            If MessageBox.Show("Recommendation refresh action may take more time to complete, we" & Environment.NewLine & "recommend to perform this action after hours." & Environment.NewLine & "Continue ?", "gloEMR", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                pnlProgressIndication.BringToFront()
                c1OpenRecommendations.SuspendLayout()
                c1OpenRecommendations.AutoResize = False
                c1OpenRecommendations.Redraw = False
                ts_btnRefresh.Visible = False
                If Not bgWorkerOpenRecommendations.IsBusy Then

                    bgWorkerOpenRecommendations.RunWorkerAsync()

                Else
                    bgWorkerOpenRecommendations.CancelAsync()

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        Finally

        End Try

        Me.Cursor = Cursors.Default
    End Sub

#End Region

#Region " c1 Events "

    Private Sub c1OpenRecommendations_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1OpenRecommendations.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1OpenRecommendations.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim PatientID As Int64 = -1
                PatientID = Convert.ToInt64(c1OpenRecommendations.GetData(c1OpenRecommendations.Row, 0).ToString())
                If PatientID > 0 Then
                    Try
                        CType(Me.ParentForm, Object).SetGnPatientID = PatientID
                        CType(Me.ParentForm, Object).mnu_DashBoard_Click(Nothing, Nothing)
                    Catch ex As Exception
                        '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        Catch ex As Exception
            ''gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1OpenRecommendations_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1OpenRecommendations.MouseMove
        Try

            If c1OpenRecommendations.HitTest(e.X, e.Y).Column = COL_sNote Or c1OpenRecommendations.HitTest(e.X, e.Y).Column = COL_Description Or c1OpenRecommendations.HitTest(e.X, e.Y).Column = COL_Name Then
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub
#End Region ' c1 Events '


    Private Sub BindHealthPlan(Optional bIsFiltered As Boolean = False, Optional nFilterRuleId As Int64 = 0)


        Dim _oDM As gloStream.DiseaseManagement.DiseaseManagement = Nothing


        Try
            Me.Cursor = Cursors.WaitCursor
          

            '' If Not IsNothing(_dtRecommendation) And _dtRecommendation.Rows.Count > 0 Then
            'c1OpenRecommendations.Clear()
            c1OpenRecommendations.DataSource = Nothing
            c1OpenRecommendations.BeginUpdate()

            If bIsFiltered = True Then

                If c1OpenRecommendations.Rows.Count > 0 Then

                    _dt = CType(c1OpenRecommendations.DataSource, DataTable)
                    If (IsNothing(_dt) = False) Then
                        _dv = _dt.DefaultView

                        _dv.RowFilter = (_dv.Table.Columns("CriteriaID").ColumnName + "Like '" & nFilterRuleId & "'")
                    End If
                  



                End If

            Else
                _oDM = New gloStream.DiseaseManagement.DiseaseManagement()
                _dtRecommendation = _oDM.GetOpenRecommendations()
                c1OpenRecommendations.DataSource = _dtRecommendation
            End If


            c1OpenRecommendations.EndUpdate()
            DesignGrid()
            '' End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            If Not IsNothing(_dtRecommendation) Then
                _dtRecommendation.Dispose()
                _dtRecommendation = Nothing
            End If

            If _oDM IsNot Nothing Then
                _oDM.Dispose()
                _oDM = Nothing
            End If
        Finally
            Me.Cursor = Cursors.Default
            If _oDM IsNot Nothing Then
                _oDM.Dispose()
                _oDM = Nothing
            End If
        End Try


    End Sub

    Private Sub FillRulesDropDown()

        Dim dtCriteria As DataTable

        Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing
        Try
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)

            dtCriteria = oDB.ReadData("gsp_DM_GetRulesActiveAndNew")
            Dim _dr As DataRow = dtCriteria.NewRow()
            _dr("dm_mst_CriteriaName") = ""
            _dr("dm_mst_Id") = 0

            dtCriteria.Rows.InsertAt(_dr, 0)
            dtCriteria.AcceptChanges()


            cmbRules.BeginUpdate()
            cmbRules.DisplayMember = dtCriteria.Columns("dm_mst_CriteriaName").ColumnName
            cmbRules.ValueMember = dtCriteria.Columns("dm_mst_Id").ColumnName
            cmbRules.DataSource = dtCriteria
            cmbRules.EndUpdate()


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Sub

    Private Sub DesignGrid()

        Try
            c1OpenRecommendations.Cols(COL_PatientId).Visible = False
            c1OpenRecommendations.Cols(COL_PatientCode).Visible = True
            c1OpenRecommendations.Cols(COL_PatientFName).Visible = True
            c1OpenRecommendations.Cols(COL_PatientMName).Visible = False
            c1OpenRecommendations.Cols(COL_PatientLName).Visible = True
            c1OpenRecommendations.Cols(COL_RuleId).Visible = False
            c1OpenRecommendations.Cols(COL_RecommendationID).Visible = False
            c1OpenRecommendations.Cols(COL_Announced).Visible = True
            c1OpenRecommendations.Cols(COL_Name).Visible = True
            c1OpenRecommendations.Cols(COL_Description).Visible = False
            c1OpenRecommendations.Cols(COL_Status).Visible = True
            c1OpenRecommendations.Cols(COL_nStatus).Visible = False
            c1OpenRecommendations.Cols(COL_sNote).Visible = True
            c1OpenRecommendations.Cols(COL_ProviderName).Visible = True
            c1OpenRecommendations.Cols(COL_ProviderID).Visible = False

            c1OpenRecommendations.Cols(COL_Announced).Format = "MM/dd/yyyy"


            With c1OpenRecommendations

                If c1OpenRecommendations.Rows.Count > 1 Then

                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                    '  cStyle = .Styles.Add("WordWrap")
                    Try
                        If (.Styles.Contains("WordWrap")) Then
                            cStyle = .Styles("WordWrap")
                        Else
                            cStyle = .Styles.Add("WordWrap")

                        End If
                    Catch ex As Exception
                        cStyle = .Styles.Add("WordWrap")

                    End Try
                    cStyle.WordWrap = True
                    cStyle.Trimming = StringTrimming.EllipsisCharacter

                    Dim rgOperator As C1.Win.C1FlexGrid.CellRange = Nothing
                    rgOperator = .GetCellRange(1, .Cols(COL_sNote).Index, .Rows.Count - 1, .Cols(COL_sNote).Index)
                    rgOperator.Style = cStyle

                    rgOperator = .GetCellRange(1, .Cols(COL_Name).Index, .Rows.Count - 1, .Cols(COL_Name).Index)
                    rgOperator.Style = cStyle

                    rgOperator = .GetCellRange(1, .Cols(COL_Description).Index, .Rows.Count - 1, .Cols(COL_Description).Index)
                    rgOperator.Style = cStyle

                End If

                For i As Int16 = 0 To .Cols.Count - 1
                    .Cols(i).AllowEditing = False
                Next
                .Cols(COL_PatientCode).Width = Width * 0.15
                .Cols(COL_PatientFName).Width = Width * 0.12
                .Cols(COL_PatientLName).Width = Width * 0.15
                .Cols(COL_Name).Width = Width * 0.3

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try




    End Sub

    Private Sub Text_Changes(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.TextChanged, cmbRules.SelectedIndexChanged
        Dim sFilter As String = ""


        Dim strSearch As String = txtSearch.Text.Trim()
        Dim strSearchArray() As String = Nothing

        Try
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If strSearch.Trim() <> "" Then
                strSearchArray = strSearch.Split(","c)
            End If

            If c1OpenRecommendations.Rows.Count > 0 Then
                _dt = CType(c1OpenRecommendations.DataSource, DataTable)
                _dv = _dt.DefaultView
                If strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        'For Single value search 
                        strSearch = strSearchArray(0).Trim()
                        If strSearch.Length > 1 Then
                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) & str
                        End If

                        _dv.RowFilter = ((_dv.Table.Columns("PatientCode").ColumnName + " Like '" & strSearch & "%' OR ") + _dv.Table.Columns("FirstName").ColumnName & " Like '" & strSearch & "%' OR ") + _dv.Table.Columns("LastName").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Recommendation").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Status").ColumnName & " Like '" & strSearch & "%'"
                    Else
                        'For Comma separated  value search
                        For i As Integer = 0 To strSearchArray.Length - 1
                            strSearch = strSearchArray(i).Trim()
                            If strSearch.Length > 1 Then
                                Dim str As String = strSearch.Substring(1).Replace("%", "")
                                strSearch = strSearch.Substring(0, 1) & str
                            End If
                            If strSearch.Trim() <> "" Then


                                If sFilter = "" Then
                                    '(i == 0)
                                    sFilter = ((" ( " + _dv.Table.Columns("PatientCode").ColumnName & " Like '" & strSearch & "%' OR ") + _dv.Table.Columns("FirstName").ColumnName & " Like '" & strSearch & "%' OR ") + _dv.Table.Columns("LastName").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Recommendation").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Status").ColumnName & " Like '" & strSearch & "%')"
                                Else
                                    sFilter = (((sFilter & " AND (") + _dv.Table.Columns("PatientCode").ColumnName & " Like '" & strSearch & "%' OR ") + _dv.Table.Columns("FirstName").ColumnName & " Like '" & strSearch & "%' OR ") + _dv.Table.Columns("LastName").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Recommendation").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Status").ColumnName & " Like '" & strSearch & "%')"

                                End If
                            End If
                        Next

                        _dv.RowFilter = sFilter
                    End If
                Else
                    _dv.RowFilter = ""
                End If
                If cmbRules.Text.Length > 0 Then
                    sFilter = _dv.Table.Columns("CriteriaID").ColumnName + " = " + cmbRules.SelectedValue.ToString()
                    If _dv.RowFilter.Length > 0 Then
                        _dv.RowFilter = "(" + _dv.RowFilter + ") AND (" + sFilter + ")"
                    Else
                        _dv.RowFilter = sFilter
                    End If



                End If

                If CmbProvider.Text.Length > 0 Then
                    sFilter = _dv.Table.Columns("ProviderID").ColumnName + " = " + CmbProvider.SelectedValue.ToString()
                    If _dv.RowFilter.Length > 0 Then
                        _dv.RowFilter = "(" + _dv.RowFilter + ") AND (" + sFilter + ")"
                    Else
                        _dv.RowFilter = sFilter
                    End If



                End If
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            If strSearchArray IsNot Nothing Then
                strSearchArray = Nothing
            End If
            If _dt IsNot Nothing Then
                _dt.Dispose()
                _dt = Nothing
            End If
            If _dv IsNot Nothing Then
                _dv.Dispose()
                _dv = Nothing
            End If
        Finally
            If strSearchArray IsNot Nothing Then
                strSearchArray = Nothing
            End If


        End Try


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
        txtSearch.Focus()
    End Sub


    Private Sub bgWorkerOpenRecommendations_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerOpenRecommendations.DoWork
        Dim _oDM As New gloStream.DiseaseManagement.DiseaseManagement
        ''''Refresh All Patient Recommendation alert 
        Try
            _oDM.RefreshAllPatientRecommendationsAlerts()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            If _oDM IsNot Nothing Then
                _oDM.Dispose()
                _oDM = Nothing
            End If
        Finally

            If _oDM IsNot Nothing Then
                _oDM.Dispose()
                _oDM = Nothing
            End If
        End Try

    End Sub

    Private Sub bgWorkerOpenRecommendations_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorkerOpenRecommendations.RunWorkerCompleted
        BindHealthPlan()
        pnlProgressIndication.SendToBack()
        c1OpenRecommendations.ResumeLayout()
        c1OpenRecommendations.AutoResize = True
        c1OpenRecommendations.Redraw = True
        ts_btnRefresh.Visible = True

    End Sub

    Private Sub bgWorkerOpenRecommendations_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgWorkerOpenRecommendations.ProgressChanged

    End Sub

    Private Sub tls_Refresh_old_Click(sender As System.Object, e As System.EventArgs) Handles tls_Refresh_old.Click
        Me.Cursor = Cursors.WaitCursor
        txtSearch.Text = ""
        cmbRules.SelectedValue = 0
        CmbProvider.SelectedValue = 0
        BindHealthPlan()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmbRules_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRules.SelectedIndexChanged
        Try

            '  BindHealthPlan(True, cmbRules.SelectedValue)

        Catch ex As Exception

        End Try
    End Sub



    Private Sub tls_btnExportToExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tls_btnExportToExcel.Click
        If ((Not (c1OpenRecommendations) Is Nothing) _
                    AndAlso (c1OpenRecommendations.Rows.Count > 1)) Then
            ExportReportToExcel()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, "Open Recommendation Exported", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If
    End Sub

    Private Sub ExportReportToExcel()
        Dim oSettings As gloSettings.DatabaseSetting.DataBaseSetting = Nothing
        Dim _DefaultLocationPath As String = ""
        Dim _FilePath As String = ""
        Dim _Checked As Boolean = False
        Try
            oSettings = New gloSettings.DatabaseSetting.DataBaseSetting
            If (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) <> "") Then
                _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"))
            Else
                _Checked = False
            End If
            _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"))

            Dim saveFileDialog As FileDialog = New SaveFileDialog
            saveFileDialog.Filter = "Excel File(.xls)|*.xls"
            saveFileDialog.DefaultExt = ".xls"
            saveFileDialog.AddExtension = True
            If ((_DefaultLocationPath <> "") _
                        AndAlso (_Checked = True)) Then
                If _DefaultLocationPath.EndsWith("\") Then
                    Dim trimChars As Char() = {"\"c}
                    _DefaultLocationPath = _DefaultLocationPath.TrimEnd(trimChars)
                End If

                ' If not exist create directory
                If (Directory.Exists(_DefaultLocationPath) = False) Then
                    Directory.CreateDirectory(_DefaultLocationPath)
                End If
                saveFileDialog.InitialDirectory = _DefaultLocationPath
            End If
            If (saveFileDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) <> DialogResult.OK) Then
                saveFileDialog.Dispose()
                Return
            End If
            _FilePath = saveFileDialog.FileName
            saveFileDialog.Dispose()
            c1OpenRecommendations.SaveExcel(_FilePath, "sheet1", FileFlags.IncludeFixedCells Or FileFlags.SaveMergedRanges Or FileFlags.VisibleOnly)
            MessageBox.Show("File saved successfully.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ioEx As IOException
            MessageBox.Show("File in use. Fail to export report.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ioEx.ToString()
            ioEx = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        Finally
            If oSettings IsNot Nothing Then
                oSettings.Dispose()
                oSettings = Nothing
            End If
        End Try

    End Sub


    Public Sub FillProviderCombo()
        Dim _oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim dt As DataTable
        Try
            dt = _oDM.GetActiveProvider()
            Dim _dr As DataRow = dt.NewRow()
            _dr("ProviderName") = ""
            _dr("nProviderID") = 0

            dt.Rows.InsertAt(_dr, 0)
            dt.AcceptChanges()
            'Dim strProviderName As String = ""

            CmbProvider.DisplayMember = dt.Columns("ProviderName").ToString()
            CmbProvider.ValueMember = dt.Columns("nProviderID").ToString()
            CmbProvider.DataSource = dt
            CmbProvider.SelectedValue = gloGlobal.gloPMGlobal.LoginProviderID

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            'dispose objects here..
            If _oDM IsNot Nothing Then
                _oDM.Dispose()
                _oDM = Nothing
            End If
        End Try
    End Sub

    Private Sub CmbProvider_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbProvider.SelectedIndexChanged
        Text_Changes(sender, e)
    End Sub

    Private Sub ts_GoToPatient_Click(sender As System.Object, e As System.EventArgs) Handles ts_GoToPatient.Click
        Try
            If c1OpenRecommendations.RowSel > 0 Then
                Dim PatientID As Int64 = -1
                PatientID = Convert.ToInt64(c1OpenRecommendations.GetData(c1OpenRecommendations.Row, 0).ToString())
                If PatientID > 0 Then
                    Try
                        CType(Me.ParentForm, Object).SetGnPatientID = PatientID
                        CType(Me.ParentForm, Object).mnu_DashBoard_Click(Nothing, Nothing)
                    Catch ex As Exception
                        '' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        Catch ex As Exception
            ''gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
