
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports gloDatabaseLayer
Imports System.Data
Imports System.Data.Sql
Imports System.Drawing
Imports System.Windows.Forms
Imports gloCommon


Public Class frmAPIAccessActivationReport

    Dim iStartIndex As Int32 = 0
    Dim iEndIndex As Int32 = 0
    Dim iPageNumber As Int32 = 1
    Dim iTotalPages As Int32 = 1
    Dim iLogCount As Int32 = 0
    Dim iPageSize As Int32 = 0

    Dim iTotalRowCount As Int32 = 0
    Dim GetGridHeadingOnly As Boolean = False
    Dim _isFormLoaded As Boolean = False
    Dim dbConnectionstring As String = ""
    Dim LoggedInUserID As Long = 1
    Dim PreviousQuickFilterValue As Int32 = -1
    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
#Region "Constructor"

    Public Sub New(ByVal Connectionstring As String, ByVal _LoggedInUserID As Long)
        InitializeComponent()
        dbConnectionstring = Connectionstring
        LoggedInUserID = _LoggedInUserID
    End Sub

#End Region

    Private Sub frmPatientActivationReport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New Cls_TabIndexSettings(Me)
            tom.SetTabOrder(scheme)
            tom = Nothing
            cmbRole.SelectedIndex = 0
            _isFormLoaded = False
            FillProviderCOMBO()
            cmbProvider.SelectedValue = getDefaultProviderId()
            fillRecordSizeCombo()
            dtpfrom.Value = DateTime.Now.AddMonths(-1)
            dtpTo.Value = DateTime.Now
            dtpfrom.Checked = False
            dtpTo.Checked = False
            pnlFilterClear.BackColor = Color.Green
            GetGridHeadingOnly = True
            LoadGridData()
            chkSelectAll.Visible = False
            _isFormLoaded = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Sub FillProviderCOMBO()

        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim _dtProvider As DataTable = Nothing

        Try
            ' Dim strqry As String = "SELECT nProviderID,RTRIM(LTRIM(ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,''))) ProviderName FROM dbo.Provider_MST  UNION SELECT 0 , '' ORDER BY 1"
            Dim strqry As String = "SELECT nProviderID,RTRIM(LTRIM(ISNULL(sFirstName,'') + ' ' + ISNULL(sLastName,''))) ProviderName FROM dbo.Provider_MST   ORDER BY 1"
            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand(strqry, Con)
            cmd.CommandType = CommandType.Text
            _dataAdapter = New SqlDataAdapter(cmd)
            _dtProvider = New DataTable()
            _dataAdapter.Fill(_dtProvider)

            If _dtProvider IsNot Nothing Then
                cmbProvider.DataSource = _dtProvider
                cmbProvider.DisplayMember = "ProviderName"
                cmbProvider.ValueMember = "nProviderID"
            End If
            cmbProvider.SelectedIndex = 0 ' -1
            strqry = Nothing
        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, "Sql Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            'If Not IsNothing(_dtProvider) Then
            '    _dtProvider.Dispose()
            '    _dtProvider = Nothing
            'End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Sub

    Public Sub fillRecordSizeCombo()
        Try
            cmbPageSize.Items.Clear()
            cmbPageSize.Items.Add("1000")
            cmbPageSize.Items.Add("5000")
            cmbPageSize.Items.Add("10000")
            cmbPageSize.SelectedIndex = 0
            iEndIndex = Convert.ToInt32(cmbPageSize.Text)
            iPageSize = Convert.ToInt32(cmbPageSize.Text)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Sub LoadGridData()
        Dim Con As SqlConnection = Nothing
        Dim dt As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _dataAdapter As SqlDataAdapter = Nothing
        Dim ds As DataSet = Nothing

        Dim QuickFilter As Int64 = 0
        Dim ProviderID As Int64 = 0
        Dim strPortalAccountStatus As String = ""
        Dim strDateFilter As String = ""
        Dim strSearchFilter As String = ""
        Dim IsQuickFilter As Boolean = False
        Try




            If IsQuickFilter = False Then
                If Convert.ToInt64(cmbProvider.SelectedValue) <> 0 OrElse Convert.ToString(cmbProvider.SelectedText) <> "" Then
                    ProviderID = Convert.ToInt64(cmbProvider.SelectedValue)
                End If

                strPortalAccountStatus = Convert.ToString(cmbPortalAccountStatus.Text)

            End If

            'strDateFilter = Convert.ToString(cmbDateFilter.Text)
            strSearchFilter = Convert.ToString(txtSearch.Text).Trim().Replace("'", "''")

            'dtdata = New DataTable()

            Con = New SqlConnection(dbConnectionstring)
            cmd = New SqlCommand("WS_PatientAPIActivationReport", Con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add(New SqlParameter("@ProviderID", ProviderID))
            cmd.Parameters.Add(New SqlParameter("@SearchFilter", strSearchFilter))
            cmd.Parameters.Add(New SqlParameter("@PortalAccountStatus", strPortalAccountStatus))
            cmd.Parameters.Add(New SqlParameter("@DateType", strDateFilter))

            If dtpfrom.Checked = True Then
                cmd.Parameters.Add(New SqlParameter("@FromDate", dtpfrom.Value))
            Else
                cmd.Parameters.Add(New SqlParameter("@FromDate", Nothing))
            End If

            If dtpTo.Checked = True Then
                cmd.Parameters.Add(New SqlParameter("@ToDate", dtpTo.Value))
            Else
                cmd.Parameters.Add(New SqlParameter("@ToDate", Nothing))
            End If

            cmd.Parameters.Add(New SqlParameter("@FromRow", iStartIndex))
            cmd.Parameters.Add(New SqlParameter("@ToRow", iEndIndex))
            cmd.Parameters.Add(New SqlParameter("@GetHeadingOnly", GetGridHeadingOnly))
            cmd.Parameters.Add(New SqlParameter("@QuickFilter", QuickFilter))

            If cmbRole.SelectedIndex = 0 Then
                cmd.Parameters.Add(New SqlParameter("@RoleId", 1))
            ElseIf cmbRole.SelectedIndex = 1 Then
                cmd.Parameters.Add(New SqlParameter("@RoleId", 2))
            Else
                cmd.Parameters.Add(New SqlParameter("@RoleId", 3))
            End If

            ds = New DataSet()

            _dataAdapter = New SqlDataAdapter(cmd)
            _dataAdapter.Fill(ds)

            If ds IsNot Nothing AndAlso ds.Tables.Count = 2 AndAlso ds.Tables(1) IsNot Nothing Then
                iTotalRowCount = Convert.ToInt32(ds.Tables(0).Rows(0)(0))
                Dim dtdata As DataTable = ds.Tables(1).Copy()
                lblRowCount.Text = Convert.ToString(iTotalRowCount)

                gvData.DataSource = Nothing
                gvData.DataSource = dtdata
                gvData.AutoResize = True
                gvData.Rows(0).Height = 2 * Me.gvData.Rows.DefaultSize
                Me.gvData.Styles("Fixed").WordWrap = True
                formatGrid()
            Else

                gvData.DataSource = Nothing
            End If

        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, "Sql Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If

            If Not IsNothing(_dataAdapter) Then
                _dataAdapter.Dispose()
                _dataAdapter = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If

            strPortalAccountStatus = Nothing
            strDateFilter = Nothing
            strSearchFilter = Nothing
        End Try
    End Sub

    Public Sub formatGrid()
        Try
            If gvData.DataSource IsNot Nothing Then
                gvData.Rows.Frozen = 0
                gvData.Cols.Frozen = 1
                chkSelectAll.Visible = True
                chkSelectAll.Checked = False
                For i As Integer = 0 To gvData.Cols.Count - 1
                    gvData.Cols(i).AllowEditing = False

                    If gvData.Cols(i).Caption = "nPatientID" OrElse gvData.Cols(i).Caption = "RoleId" OrElse gvData.Cols(i).Caption = "nProviderID" OrElse gvData.Cols(i).Caption = "bIsQuickActivated" OrElse gvData.Cols(i).Caption = "RowNo" OrElse gvData.Cols(i).Caption = "Portal Access" Then
                        gvData.Cols(i).Visible = False
                    End If
                    If gvData.Cols(i).Caption = "Provider Name" Then
                        If cmbRole.SelectedIndex = 0 Then
                            gvData.Cols(i).Visible = True
                        ElseIf cmbRole.SelectedIndex = 1 Then
                            gvData.Cols(i).Visible = False

                        ElseIf cmbRole.SelectedIndex = 2 Then
                            gvData.Cols(i).Visible = False

                        End If
                    End If
                    If gvData.Cols(i).Caption = "Patient Code" Then
                        If cmbRole.SelectedIndex = 0 Then
                            gvData.Cols(i).Visible = True
                        ElseIf cmbRole.SelectedIndex = 1 Then
                            gvData.Cols(i).Visible = False

                        ElseIf cmbRole.SelectedIndex = 2 Then
                            gvData.Cols(i).Visible = True
                            gvData.Cols(i).Caption = "Role Name"

                        End If
                    End If
                    If gvData.Cols(i).Caption = "Days Since Last Invite Sent" OrElse gvData.Cols(i).Caption = "No of Invitation Sent" OrElse gvData.Cols(i).Caption = "Days Since Last Visit" OrElse gvData.Cols(i).Caption = "API Account Status" OrElse gvData.Cols(i).Caption = "Invitation Status" OrElse gvData.Cols(i).Caption = "Last Login Date" OrElse gvData.Cols(i).Caption = "Last Invitation Date" Then
                        gvData.Cols(i).Width = -1
                    End If
                    If gvData.Cols(i).Caption = "Select" Then
                        gvData.Cols(i).Caption = "  [    ]   Select "
                        gvData.Cols(i).AllowEditing = True
                        gvData.Cols(i).AllowSorting = False
                        gvData.Cols(i).AllowFiltering = AllowFiltering.None
                    End If
                    If gvData.Cols(i).Caption = "User Name" Then
                        gvData.Cols(i).Visible = False
                        
                    End If
                    If gvData.Cols(i).Caption = "Password" Then
                        gvData.Cols(i).Visible = False

                    End If

                Next

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Function Validation() As Boolean
        Try
            Dim IsValid As Boolean = True
            If (dtpfrom.Checked = True AndAlso dtpTo.Checked = True) AndAlso (dtpfrom.Value > dtpTo.Value) Then
                IsValid = False
                MessageBox.Show("From date Should be smaller then To date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return IsValid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        End Try
    End Function

    Private Sub btnViewReport_Click(sender As System.Object, e As System.EventArgs) Handles btnViewReport.Click
        Try
            If Validation() Then
                Call btnClearFilter_Click(Nothing, Nothing)
                GetGridHeadingOnly = False
                SetData()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub btnClearFilter_Click(sender As System.Object, e As System.EventArgs) Handles btnClearFilter.Click
        Try
            If gvData.DataSource IsNot Nothing Then
                For i As Integer = 0 To gvData.Cols.Count - 1
                    If gvData.Cols(i).Filter IsNot Nothing Then
                        If gvData.Cols(i).Filter.IsActive = True Then
                            gvData.ClearFilter(i)
                        End If
                    End If
                Next
            End If
            pnlFilterClear.BackColor = Color.Green
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub gvData_CellChecked(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles gvData.CellChecked
        Try
            If e.Col = 1 Then
                If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
                    Dim IsActivated As String = Convert.ToString(gvData.Cols("API Account Status")(e.Row)).ToUpper().Trim()
                    Dim EmailAddress As String = Convert.ToString(gvData.Cols("Email")(e.Row)).Trim()
                    Dim ZIP As String = Convert.ToString(gvData.Cols("ZIP")(e.Row)).Trim()

                    If IsActivated <> "ACTIVATED" AndAlso IsActivated <> "BLOCKED" AndAlso EmailAddress <> "" Then
                    Else
                        gvData.Cols("Select")(e.Row) = False
                    End If

                    IsActivated = Nothing
                    EmailAddress = Nothing
                    ZIP = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub gvData_AfterFilter(sender As System.Object, e As System.EventArgs) Handles gvData.AfterFilter
        Try
            Dim intCnt As Integer = 0
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
                For i As Integer = 0 To gvData.Cols.Count - 1
                    If gvData.Cols(i).Filter IsNot Nothing Then
                        If gvData.Cols(i).Filter.IsActive = True Then
                            intCnt += 1
                        End If
                    End If
                Next
                If intCnt = 0 Then
                    pnlFilterClear.BackColor = Color.Green
                Else
                    pnlFilterClear.BackColor = Color.Red
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub cmbPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPageSize.SelectedIndexChanged
        Try
            If _isFormLoaded Then
                'Assign page size value from combo box
                iPageSize = Convert.ToInt32(cmbPageSize.Text)
                If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
                    SetData()
                Else
                    SetData(False)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Try
            ' cmbProvider.SelectedIndex = 0
            cmbProvider.SelectedValue = getDefaultProviderId()
            cmbPortalAccountStatus.SelectedIndex = 0
            PreviousQuickFilterValue = -1
            txtSearch.Text = ""
            dtpfrom.Value = DateTime.Now.AddMonths(-1)
            dtpTo.Value = DateTime.Now
            dtpfrom.Checked = False
            dtpTo.Checked = False

            gvData.DataSource = Nothing
            GetGridHeadingOnly = True
            SetData()
            chkSelectAll.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Function ProceedNextPreviousWhileRowSelected(Move As Integer) As Boolean
        Try
            Dim IsAnySelected As Boolean = False
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then

                For i As Integer = 1 To gvData.Rows.Count - 2
                    If Convert.ToBoolean(gvData.Cols("Select")(i)) = True Then
                        IsAnySelected = True
                        Exit For
                    End If
                Next
            End If

            Dim IsProceed As Boolean = False
            Dim IsPrompt As Boolean = True

            If IsAnySelected = True Then
                Try
                    If ((iPageNumber = 1 And (Move = 0 Or Move = 1)) Or (iPageNumber = iTotalPages And (Move = 2 Or Move = 3))) Then
                        IsPrompt = False
                    End If
                Catch ex As Exception
                End Try
                If IsPrompt = True Then
                    If MessageBox.Show("Moving to next the page will lose the current selections." & vbCrLf & vbCrLf & "Do you want to proceed with the change of page ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
                        IsProceed = True
                    End If
                End If
            Else
                IsProceed = True
            End If
            Return IsProceed
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return True
        End Try
    End Function

#Region "Grid Paging"

    Private Sub GetPageSize()
        Try
            Dim nShowReport As Int32 = Convert.ToInt32(cmbPageSize.Text)
            iLogCount = iTotalRowCount
            iTotalPages = Convert.ToInt16(Math.Floor(Convert.ToDecimal(iLogCount / nShowReport)))
            If iLogCount > (nShowReport * iTotalPages) Then
                Dim iLogDifference As Int32 = iLogCount - (nShowReport * iTotalPages)
                iLogCount = iLogCount + (nShowReport - iLogDifference)
                iTotalPages = iTotalPages + 1
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Sub SetData(Optional IsLoad As Boolean = True)
        Try
            iPageNumber -= 1
            If iPageNumber < 1 Then
                iPageNumber = 1
                'return;
                lblSelected.Text = "Page: " & iPageNumber & " of " & iTotalPages
            End If
            iPageNumber = 1
            iStartIndex = 0
            iEndIndex = iPageSize
            If IsLoad = True Then
                LoadGridData()
            End If
            GetPageSize()
            lblSelected.Text = "Page: " & iPageNumber & " of " & iTotalPages
        Catch generatedExceptionName As Exception
        End Try
    End Sub

    Private Sub BtnFirst_Click(sender As System.Object, e As System.EventArgs) Handles BtnFirst.Click
        Try
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 1 Then
                If ProceedNextPreviousWhileRowSelected(0) = False Then
                    Return
                End If
                SetData()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub BtnPrev_Click(sender As System.Object, e As System.EventArgs) Handles BtnPrev.Click
        Try
            If ProceedNextPreviousWhileRowSelected(1) = False Then
                Return
            End If
            iPageNumber -= 1

            If iPageNumber < 1 Then
                iPageNumber = 1
                Return
            End If

            iStartIndex = iStartIndex - iPageSize
            iEndIndex = iEndIndex - iPageSize

            If iStartIndex < 0 Then
                iStartIndex = 0
            End If

            If iEndIndex < iPageSize Then
                iEndIndex = iPageSize
            End If

            LoadGridData()

            If iLogCount > 0 Then
                lblSelected.Text = "Page: " & iPageNumber & " of " & iTotalPages
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btn_Next_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Next.Click
        Try
            If ProceedNextPreviousWhileRowSelected(2) = False Then
                Return
            End If
            iPageNumber += 1

            If iPageNumber > iTotalPages Then
                iPageNumber = iTotalPages
                Return
            End If

            iStartIndex = iStartIndex + iPageSize
            iEndIndex = iEndIndex + iPageSize

            If iStartIndex > (iLogCount - iPageSize) Then
                iStartIndex = iLogCount - iPageSize
            End If

            If iEndIndex > iLogCount Then
                iEndIndex = iLogCount
            End If

            ' PopulateDatagrid();
            LoadGridData()

            If iLogCount > 0 Then
                lblSelected.Text = "Page: " & iPageNumber & " of " & iTotalPages
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnLast_Click(sender As System.Object, e As System.EventArgs) Handles BtnLast.Click
        Try
            If ProceedNextPreviousWhileRowSelected(3) = False Then
                Return
            End If
            If iPageNumber <> iTotalPages Then
                iPageNumber = iTotalPages
                iStartIndex = iLogCount - iPageSize
                iEndIndex = iLogCount
                'PopulateDatagrid();
                LoadGridData()

                If iLogCount > 0 Then
                    lblSelected.Text = "Page: " & iPageNumber & " of " & iTotalPages
                    iPageNumber = iTotalPages
                Else
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Private Sub btnSendEmail_Click(sender As System.Object, e As System.EventArgs) Handles btnActivation.Click
        Try
            Dim strProceed As String = IsPaitentSelectedForMailSent()
            If strProceed <> "" Then
                If MessageBox.Show(strProceed, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                    If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then

                        For i As Integer = 1 To gvData.Rows.Count - 1
                            If Convert.ToBoolean(gvData.Cols("Select")(i)) = True Then
                                Dim patientID As Int64 = Convert.ToInt64(gvData.Cols("nPatientID")(i))
                                'Dim IsResend As Boolean = False
                                '' Might Status may increase
                                'If Convert.ToString(gvData.Cols("API Account Status")(i)).ToUpper.Trim = "INVITED" Then
                                '    IsResend = True
                                'ElseIf Convert.ToString(gvData.Cols("API Account Status")(i)).ToUpper.Trim = "NOT INVITED" Then
                                '    IsResend = False
                                'End If

                                'oclsMessageQueue.dtLinkDate = DateTime.Now
                                'oclsMessageQueue.nPatientID = patientID
                                'oclsMessageQueue.SendPortalEmails("PatientPortal", True, IsResend, False)

                                Dim sEmail As String = Convert.ToString(gvData.Cols("Email")(i)).Trim()

                                Dim sFirstName As String = Convert.ToString(gvData.Cols("First Name")(i)).Trim()
                                Dim sLastName As String = Convert.ToString(gvData.Cols("Last Name")(i)).Trim()
                                Dim sRoleName As String = Convert.ToString(gvData.Cols("Patient Code")(i)).Trim()


                                Dim arrAPIAccess As APIAccess() = New APIAccess(0) {}
                                Dim objAPIAccess As New APIAccess()
                                objAPIAccess.APIUserID = patientID
                                objAPIAccess.UserName = sEmail
                                objAPIAccess.Password = ""

                                arrAPIAccess(0) = objAPIAccess

                                Dim objclsAPIAcceess As New clsAPIAcceess()

                                Dim _result As Int64 = -1
                                If cmbRole.Text = "Patient" Then
                                    Dim nProviderID As Int64 = Convert.ToInt64(cmbProvider.SelectedValue)
                                    If Not getProviderTaxID(nProviderID) Then
                                        Exit Sub
                                    End If

                                    _result = objclsAPIAcceess.APIAccessProceess(dbConnectionstring, arrAPIAccess, 1, 1, "", DateTime.Now)
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Activate, "API activated for patient", patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                                    Dim MessageQueueID As Long = GetMessageQueueOrPoratlAccessID(patientID, 0)
                                    If MessageQueueID <> 0 Then
                                        Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, MessageQueueID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.PatientPortalBulkInvitations.GetHashCode())
                                        oclsselectProviderTaxID = Nothing
                                    End If
                                ElseIf cmbRole.Text = "Patient Representative" Then
                                    _result = objclsAPIAcceess.APIAccessProceess(dbConnectionstring, arrAPIAccess, 1, 2, "", DateTime.Now)
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.Activate, "API activated for Patient Representative: " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                ElseIf cmbRole.Text = "Others" Then
                                    Dim Roleid As Int64 = Convert.ToInt64(gvData.Cols("RoleId")(i))

                                    _result = objclsAPIAcceess.APIAccessProceess(dbConnectionstring, arrAPIAccess, 1, Roleid, "", DateTime.Now)
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Activate, "API activated for " + sRoleName + ": " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                End If
                                objclsAPIAcceess = Nothing

                            End If
                        Next


                        Call btnViewReport_Click(Nothing, Nothing)
                    End If
                End If
            Else
                MessageBox.Show("Please select account to activate API access.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            strProceed = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Function IsPaitentSelectedForMailSent() As String
        Try
            Dim PatientSelected As String = ""
            Dim intSelected As Int64 = 0
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then

                For i As Integer = 1 To gvData.Rows.Count - 1
                    If Convert.ToBoolean(gvData.Cols("Select")(i)) = True Then
                        intSelected += 1
                    End If
                Next
            End If
            If intSelected > 0 Then
                PatientSelected = "Number of patients selected : " & intSelected & vbCrLf & vbCrLf & "Activate these users for API access now ?"
            End If
            Return PatientSelected
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return ""
        End Try
    End Function

    Public Function getClientMachineID(strClientMachineName As String) As String

        Dim oDBLayer As New gloDatabaseLayer.DBLayer(dbConnectionstring)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim ClientMachineID As String = ""

        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@MachineName", strClientMachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sProductCode", "1", ParameterDirection.Input, SqlDbType.VarChar)

            Dim machineid As Object = oDBLayer.ExecuteScalar("sp_CheckClientMachinePermission", oDBParameters)
            oDBLayer.Disconnect()

            ClientMachineID = Convert.ToString(machineid)

        Catch ex As SqlException
            System.Windows.Forms.MessageBox.Show(ex.ToString, "Sql Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return ClientMachineID
    End Function

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            txtSearch.Text = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub tlpClose_Click(sender As System.Object, e As System.EventArgs) Handles tlpClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub gvData_OwnerDrawCell(sender As System.Object, e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles gvData.OwnerDrawCell
        Dim IsActivated As String = Nothing
        Dim EmailAddress As String = Nothing
        Dim ZIP As String = Nothing
        Try
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
                ' Hiding Checkbox
                IsActivated = Convert.ToString(gvData.Cols("API Account Status")(e.Row)).ToUpper().Trim()
                EmailAddress = Convert.ToString(gvData.Cols("Email")(e.Row)).Trim()
                ZIP = Convert.ToString(gvData.Cols("ZIP")(e.Row)).Trim()

                If IsActivated <> "ACTIVATED" AndAlso IsActivated <> "BLOCKED" AndAlso EmailAddress <> "" Then
                    e.Style.Display = DisplayEnum.Overlay
                Else
                    e.Style.Display = DisplayEnum.TextOnly
                End If



                ' Setting grid cell BackColor



            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            IsActivated = Nothing
            EmailAddress = Nothing
            ZIP = Nothing
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
                If chkSelectAll.Checked = True Then
                    SelectUnselectAll(True)
                    If (iTotalPages > 1 AndAlso iPageNumber = 1) Then
                        MessageBox.Show("The ‘Select All’ option only selects users for the current page." & vbCrLf & vbCrLf & "After activations completed for this page, you may want to review the other pages for additional activations.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    SelectUnselectAll(False)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    ''' <summary>
    ''' True for Select All and False for UnSelect All
    ''' </summary>
    ''' <param name="IsSelect"></param>
    ''' <remarks></remarks>
    Private Sub SelectUnselectAll(IsSelect As Boolean)
        Dim IsActivated As String = Nothing
        Dim EmailAddress As String = Nothing
        Dim ZIP As String = Nothing
        Dim invitationStatus As String = Nothing
        Try
            If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
                For i As Integer = 1 To gvData.Rows.Count - 1
                    IsActivated = Convert.ToString(gvData.Cols("API Account Status")(i)).ToUpper().Trim()
                    EmailAddress = Convert.ToString(gvData.Cols("Email")(i)).Trim()
                    ZIP = Convert.ToString(gvData.Cols("ZIP")(i)).Trim()


                    If IsActivated <> "ACTIVATED" AndAlso IsActivated <> "BLOCKED" AndAlso EmailAddress <> "" Then
                        gvData.Cols("Select")(i) = IsSelect

                    End If


                Next
            End If
        Catch generatedExceptionName As Exception
        Finally
            IsActivated = Nothing
            EmailAddress = Nothing
            ZIP = Nothing
            invitationStatus = Nothing
        End Try
    End Sub





    Private Sub cmbPortalAccountStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPortalAccountStatus.SelectedIndexChanged
        Try
            If cmbPortalAccountStatus.SelectedIndex > 0 Then
                'ClearQuickFilter()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbProvider_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        Try
            If cmbProvider.SelectedIndex > 0 Then
                'ClearQuickFilter()
            End If
        Catch ex As Exception
        End Try
    End Sub




    Private Sub btnBlock_Click(sender As System.Object, e As System.EventArgs) Handles btnBlock.Click

        If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
            Dim index As Integer = gvData.Selection.r1
            If index < 0 Then
                MessageBox.Show("Please select an account to block for API access.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Else
                Dim IsActivated As String = Convert.ToString(gvData.Cols("API Account Status")(index)).ToUpper().Trim()
                If IsActivated = "BLOCKED" Then
                    'If IsActivated <> "ACTIVATED" AndAlso IsActivated <> "BLOCKED" Then
                    MessageBox.Show("Selected account is already blocked for API access", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                ElseIf IsActivated = "NOT ACTIVATED" Then
                    MessageBox.Show("Selected account is Not activated ", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    Dim dilogResult As DialogResult = MessageBox.Show("Do you want to block selected account for API access? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If dilogResult = Windows.Forms.DialogResult.Yes Then
                        Dim sEmail As String = Convert.ToString(gvData.Cols("Email")(index)).Trim()
                        Dim patientID As Int64 = Convert.ToInt64(gvData.Cols("nPatientID")(index))

                        Dim sFirstName As String = Convert.ToString(gvData.Cols("First Name")(index)).Trim()
                        Dim sLastName As String = Convert.ToString(gvData.Cols("Last Name")(index)).Trim()
                        Dim sRoleName As String = Convert.ToString(gvData.Cols("Patient Code")(index)).Trim()


                        Dim arrAPIAccess As APIAccess() = New APIAccess(0) {}
                        Dim objAPIAccess As New APIAccess()
                        objAPIAccess.APIUserID = patientID
                        objAPIAccess.UserName = sEmail
                        objAPIAccess.Password = ""
                        arrAPIAccess(0) = objAPIAccess
                        Dim objclsAPIAcceess As New clsAPIAcceess()
                        Dim _result As Int64 = -1
                        _result = objclsAPIAcceess.APIAccessProceess(dbConnectionstring, arrAPIAccess, 2, 1, "", DateTime.Now)
                        objclsAPIAcceess = Nothing
                        MessageBox.Show("Selected account is blocked for API access", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If cmbRole.Text = "Patient" Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.DeActivate, "API blocked/inactivated for patient", patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                        ElseIf cmbRole.Text = "Patient Representative" Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.DeActivate, "API blocked/inactivated for Patient Representative: " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ElseIf cmbRole.Text = "Others" Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.DeActivate, "API blocked/inactivated for " + sRoleName + ": " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                        Call btnViewReport_Click(Nothing, Nothing)
                    End If
                End If

            End If


        End If


    End Sub

    Private Sub btnunblock_Click(sender As System.Object, e As System.EventArgs) Handles btnunblock.Click
        If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
            Dim index As Integer = gvData.Selection.r1
            If index < 0 Then
                MessageBox.Show("Please select an account to un-block for API access.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Else
                Dim IsActivated As String = Convert.ToString(gvData.Cols("API Account Status")(index)).ToUpper().Trim()
                If IsActivated = "ACTIVATED" Then
                    'If IsActivated <> "ACTIVATED" AndAlso IsActivated <> "BLOCKED" Then
                    MessageBox.Show("Selected account is already activated for API access", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                ElseIf IsActivated = "NOT ACTIVATED" Then
                    MessageBox.Show("Selected account is Not activated ", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    Dim dilogResult As DialogResult = MessageBox.Show("Do you want to unblock selected account for API access? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If dilogResult = Windows.Forms.DialogResult.Yes Then

                        Dim sEmail As String = Convert.ToString(gvData.Cols("Email")(index)).Trim()
                        Dim patientID As Int64 = Convert.ToInt64(gvData.Cols("nPatientID")(index))


                        Dim sFirstName As String = Convert.ToString(gvData.Cols("First Name")(index)).Trim()
                        Dim sLastName As String = Convert.ToString(gvData.Cols("Last Name")(index)).Trim()
                        Dim sRoleName As String = Convert.ToString(gvData.Cols("Patient Code")(index)).Trim()

                        Dim arrAPIAccess As APIAccess() = New APIAccess(0) {}
                        Dim objAPIAccess As New APIAccess()
                        objAPIAccess.APIUserID = patientID
                        objAPIAccess.UserName = sEmail
                        objAPIAccess.Password = ""
                        arrAPIAccess(0) = objAPIAccess
                        Dim objclsAPIAcceess As New clsAPIAcceess()
                        Dim _result As Int64 = -1
                        _result = objclsAPIAcceess.APIAccessProceess(dbConnectionstring, arrAPIAccess, 3, 1, "", DateTime.Now)
                        objclsAPIAcceess = Nothing
                        MessageBox.Show("Selected account is un-blocked for API access", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If cmbRole.Text = "Patient" Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.DeActivate, "API unblocked/activated for patient" + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ElseIf cmbRole.Text = "Patient Representative" Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.DeActivate, "API unblocked/activated for Patient Representative: " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ElseIf cmbRole.Text = "Others" Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.DeActivate, "API unblocked/activated for  " + sRoleName + ": " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If

                        Call btnViewReport_Click(Nothing, Nothing)
                    End If
                End If

            End If


        End If
    End Sub

    Private Sub btnChangeCredential_Click(sender As System.Object, e As System.EventArgs) Handles btnChangeCredential.Click


        If gvData.DataSource IsNot Nothing AndAlso gvData.Rows.Count > 0 Then
            Dim index As Integer = gvData.Selection.r1
            If index < 0 Then
                MessageBox.Show("Please select an account to change credentials for API access.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Else
                Dim IsActivated As String = Convert.ToString(gvData.Cols("API Account Status")(index)).ToUpper().Trim()
                If IsActivated = "BLOCKED" Then
                    'If IsActivated <> "ACTIVATED" AndAlso IsActivated <> "BLOCKED" Then
                    MessageBox.Show("Selected account is blocked for API access.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                ElseIf IsActivated = "NOT ACTIVATED" Then
                    MessageBox.Show("Selected account is Not activated ", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    txtReserPassword.Clear()
                    txtResetUserName.Clear()
                    ShowHideOnResetButton(True)
                    Dim userName As String = Convert.ToString(gvData.Cols("User Name")(index))
                    Dim oClsEncryption As New gloSecurity.ClsEncryption()
                    Dim password As String = oClsEncryption.DecryptFromBase64String(Convert.ToString(gvData.Cols("Password")(index)), _encryptionKey)
                    oClsEncryption.Dispose()
                    oClsEncryption = Nothing

                    txtResetUserName.Text = userName
                    txtReserPassword.Text = password

                End If

            End If


        End If
    End Sub


    Private Function GetRandomNumber() As String

        Dim chars = ""
        For i As Integer = 65 To 90
            chars += Char.ConvertFromUtf32(i)
        Next
        For i As Integer = 97 To 122
            chars += Char.ConvertFromUtf32(i)
        Next
        For i As Integer = 48 To 57
            chars += Char.ConvertFromUtf32(i)
        Next

        Dim random = New Random()




        Dim result = New String(Enumerable.Repeat(chars, 7).[Select](Function(s) s(random.[Next](s.Length))).ToArray())

        chars = ""


        chars += Char.ConvertFromUtf32(33)
        chars += Char.ConvertFromUtf32(35)
        chars += Char.ConvertFromUtf32(36)
        chars += Char.ConvertFromUtf32(37)
        chars += Char.ConvertFromUtf32(38)
        chars += Char.ConvertFromUtf32(42)
        chars += Char.ConvertFromUtf32(64)


        Dim random1 = New Random()




        Dim result1 = New String(Enumerable.Repeat(chars, 1).[Select](Function(s) s(random1.[Next](s.Length))).ToArray())

        Dim t1 = New Random()
        Dim t2 = t1.[Next](0, 7)

        result = result.Insert(t2, result1)

        Return result

    End Function
    Dim _encryptionKey As String = "12345678"
    Private Sub btnSaveResetCredentials_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveResetCredentials.Click
        If (txtResetUserName.Text.Trim() = "") Then
            MessageBox.Show("Only spcace is not allowded for user name", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If (txtReserPassword.Text.Trim() = "") Then
            MessageBox.Show("Only spcace is not allowded for password", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If (txtResetUserName.Text.Length < 8) Then
            MessageBox.Show("Please choose a user name  with at least 8 characters", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If (txtReserPassword.Text.Length < 8) Then
            MessageBox.Show("Please choose a password with at least 8 characters", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
       

        Dim dilogResult As DialogResult = MessageBox.Show("Are you sure you want to change credentials for selected account? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If dilogResult = Windows.Forms.DialogResult.Yes Then
            Dim index As Integer = gvData.Selection.r1
            Dim patientID As Int64 = Convert.ToInt64(gvData.Cols("nPatientID")(index))
            Dim sEmail As String = Convert.ToString(gvData.Cols("Email")(index)).Trim()
            Dim sFirstName As String = Convert.ToString(gvData.Cols("First Name")(index)).Trim()
            Dim sLastName As String = Convert.ToString(gvData.Cols("Last Name")(index)).Trim()
            Dim sRoleName As String = Convert.ToString(gvData.Cols("Patient Code")(index)).Trim()




        

            Dim arrAPIAccess As APIAccess() = New APIAccess(0) {}
            Dim objAPIAccess As New APIAccess()
            objAPIAccess.APIUserID = patientID
            objAPIAccess.UserName = txtResetUserName.Text
            objAPIAccess.Password = txtReserPassword.Text
            arrAPIAccess(0) = objAPIAccess
            Dim objclsAPIAcceess As New clsAPIAcceess()
            Dim _result As Int64 = -1
            _result = objclsAPIAcceess.APIAccessProceess(dbConnectionstring, arrAPIAccess, 4, 1, "", DateTime.Now)
            objclsAPIAcceess = Nothing
            If _result = 41 Then
                MessageBox.Show("Credentials are updated successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ShowHideOnResetButton(False)
            ElseIf _result = 40 Then

                MessageBox.Show("user name is already exist for another account", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

            Call btnViewReport_Click(Nothing, Nothing)
            If cmbRole.Text = "Patient" Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Modify, "API credentials updated for patient: " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            ElseIf cmbRole.Text = "Patient Representative" Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.PatientRepresentative, gloAuditTrail.ActivityType.DeActivate, "API credentials updated for Patient Representative: " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf cmbRole.Text = "Others" Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.DeActivate, "API credentials updated for " + sRoleName + ": " + sFirstName + " " + sLastName, patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            Call btnViewReport_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnClosePanel_Click(sender As System.Object, e As System.EventArgs) Handles btnClosePanel.Click
        ShowHideOnResetButton(False)
    End Sub

    Public Sub ShowHideOnResetButton(ByVal isToShow As Boolean)
        pnlResetCredentials.Visible = isToShow

        If isToShow = True Then
            pnlResetCredentials.Left = ((Me.Width / 2) - (pnlResetCredentials.Width / 2))
            pnlResetCredentials.BringToFront()
            Btn_Next.Enabled = False
            BtnPrev.Enabled = False
            BtnLast.Enabled = False
            BtnFirst.Enabled = False

            btnBlock.Enabled = False
            btnunblock.Enabled = False
            btnChangeCredential.Enabled = False
            btnActivation.Enabled = False
            btnViewReport.Enabled = False
            btnReset.Enabled = False
            btnClearFilter.Enabled = False
            cmbPageSize.Enabled = False
            cmbRole.Enabled = False
            cmbPortalAccountStatus.Enabled = False
            cmbProvider.Enabled = False

            txtSearch.Enabled = False
            dtpfrom.Enabled = False
            dtpTo.Enabled = False
            btnClearSearch.Enabled = False
        Else
            pnlResetCredentials.BringToFront()
            Btn_Next.Enabled = True
            BtnPrev.Enabled = True
            BtnLast.Enabled = True
            BtnFirst.Enabled = True

            btnBlock.Enabled = True
            btnunblock.Enabled = True
            btnChangeCredential.Enabled = True
            btnActivation.Enabled = True
            btnViewReport.Enabled = True
            btnReset.Enabled = True
            btnClearFilter.Enabled = True
            cmbPageSize.Enabled = True
            cmbRole.Enabled = True
            cmbPortalAccountStatus.Enabled = True
            cmbProvider.Enabled = True

            txtSearch.Enabled = True
            dtpfrom.Enabled = True
            dtpTo.Enabled = True
            btnClearSearch.Enabled = True
            pnlResetCredentials.SendToBack()
        End If


    End Sub
    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID))
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog(Me)
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult
                nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
                oForm = Nothing
            Else
                nProviderAssociationID = 0
                sProviderTaxID = ""
            End If

            If oResult = Windows.Forms.DialogResult.OK Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False

        Finally
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="PatientID">Patient ID</param>
    ''' <param name="nRetriveID">0: retrive nMessageID from Gl_MessageQueue.
    ''' 1: retrive nPortalAccessID from PateintPortalAccess.</param>
    ''' <returns></returns>
    Private Function GetMessageQueueOrPoratlAccessID(PatientID As Int64, nRetriveID As Integer) As Long
        Dim oDB As New gloDatabaseLayer.DBLayer(dbConnectionstring)
        Dim dt As DataTable = Nothing
        Dim _sqlQuery As String = ""
        Dim nID As Int64 = 0
        Try
            oDB.Connect(False)
            If PatientID > 0 Then
                'Warning Removed at the time of Change made to solve memory Leak and word crash issue
                'Bug #93369: 00001074: setup Appointment
                'If nRetriveID = 0 Then
                '    _sqlQuery = " SELECT nMessageID as nMessageID FROM dbo.Gl_Messagequeue WHERE nPatientID =  " + PatientID.ToString() + " AND sMessageName='PATIENTINVITATION' AND sServiceName = 'PatientPortal' AND nStatus IN (1, 0) "
                'ElseIf nRetriveID = 1 Then
                '    _sqlQuery = " SELECT nPatientPortalAccessID as nPatientPortalAccessID  FROM dbo.PatientPortalAccess WHERE nPatientID =  " + PatientID.ToString() + " AND bIsBlocked=1 AND bIsQuickActivated=1"
                'End If
                _sqlQuery = " SELECT nAPIAccessID as nPatientPortalAccessID  FROM dbo.APIAccess WHERE nAPIAccessUserID =  " + PatientID.ToString() + " AND bAllowAccess=1"

                oDB.Retrive_Query(_sqlQuery, dt)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                nID = Convert.ToInt64(dt.Rows(0)("nPatientPortalAccessID"))
                'If nRetriveID = 0 Then
                '    nID = Convert.ToInt64(dt.Rows(0)("nMessageID"))
                'ElseIf nRetriveID = 1 Then
                '    nID = Convert.ToInt64(dt.Rows(0)("nPatientPortalAccessID"))
                'End If

            End If
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Return 0
        Catch ex As Exception
            MessageBox.Show("ERROR : " + ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return 0
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()

            End If
        End Try
        Return nID
    End Function

    Private Sub cmbRole_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbRole.SelectedIndexChanged
        If cmbRole.SelectedIndex = 0 Then
            cmbProvider.Visible = True
            label1.Visible = True
        Else
            cmbProvider.Visible = False
            label1.Visible = False
        End If
    End Sub

    Public Function getDefaultProviderId() As Int64
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _Provider As Int64 = 0
        Try
            conn = New SqlConnection(dbConnectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "select sSettingsValue from settings where sSettingsName = 'PatientDefaultProvider'"
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _Provider = CType(temp, Int64)
                If _Provider = 0 Then
                    strQuery = "select top 1 nProviderID from provider_mst"
                    cmd.CommandText = strQuery
                    Dim temp1 As Object = cmd.ExecuteScalar()
                    If Not IsNothing(temp1) Then
                        _Provider = CType(temp1, Int64)
                    End If
                End If
            End If
            Return _Provider
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            strQuery = Nothing
        End Try
    End Function

End Class
