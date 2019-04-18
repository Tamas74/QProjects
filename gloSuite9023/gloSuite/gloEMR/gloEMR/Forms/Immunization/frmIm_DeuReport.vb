Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports gloSSRSApplication.SSRS

Public Class frmIm_DueReport

    Private COL_PATIENTCODE As Integer = 2 '1 '10
    Private Col_USERNAME As Integer = 3 '2
    Private COL_DOB As Integer = 4 '3 '4
    Private COL_ITEM_NAME As Integer = 5 '4 '5
    Private COL_COUNTER As Integer = 6 '5 '9
    Private COL_DUEDATE As Integer = 7 '6 '3
    Private COL_NOTES As Integer = 8 '7

    Private COL_ITEMNAME As Integer = 11 '10
    Private COL_ITEMCOUNTERID As Integer = 9 '8
    Private COL_GIVENDATE As Integer = 10 '9
    Private COL_TRNDATE As Integer = 0
    Private COL_STATUS As Integer = 1 '11

    'COL_ITEMNAME,COL_ITEMCOUNTERID,Col_USERNAME,COL_DUEDATE,COL_DOB,COL_ITEM_NAME,COL_GIVENDATE,COL_NOTES,COL_TRNDATE,COL_COUNTER,COL_PATIENTCODE
    Private COL_COUNT As Integer = 12
    Dim _PatientID As Long
    Dim _RptName As String = ""
    Public _PatDOB As String = ""
    Public _age As String = ""

    Public Shared IsOpen As Boolean = False
    Dim PnlContainer As Object
    Dim dsReportDataset As DataSet
    Dim _LocationID As Int64

    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean

    Private Shared frm As frmIm_DueReport

    Public Shared Function GetInstance(ByVal PatientID As Long, ByVal ReportName As String) As frmIm_DueReport
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmIm_DueReport" Then
                    'If CType(f, frmRpt_PatientICD9CPT) = PatientID Then
                    IsOpen = True
                    frm = f
                    'End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmIm_DueReport(PatientID, ReportName)
            End If

        Finally

        End Try
        Return frm
    End Function

#End Region

    Private Sub frmIm_DueReport_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

        End Try
        If (IsNothing(dsReportDataset) = False) Then
            dsReportDataset.Dispose()
            dsReportDataset = Nothing
        End If
    End Sub

    Private Sub frmIm_DueReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            tblbtn_Print_32.Visible = True
            If _RptName = "RptPatientImmunizationSummary" Then 'Vaccine record
                Me.Text = "Vaccine Record Report"
                PnlVaccineReport.Visible = True
                tblbtn_Print_32.Visible = True
                tblbtn_Print_32.Enabled = False
                tblbtnShowReport.Visible = True
                dtpVacFrom.Text = _PatDOB
                dtpVacTo.Value = Now
                dtpVacFrom.Enabled = False
                dtpVacTo.Enabled = False
                rdb_VaccineGroup.Checked = True
                FillLocation()
            ElseIf _RptName = "RptVaccineInventory" Then
                Me.Text = "Vaccine Inventory Report"
                pnlVacInventory.Visible = True
                tblbtn_Print_32.Visible = True
                tblbtn_Print_32.Enabled = False
                tblbtnShowReport.Visible = True
                dtReceiveFrom.Text = DateAdd(DateInterval.Year, -5, Now)
                dtReceiveTo.Value = Now
                dtReceiveFrom.Enabled = False
                dtReceiveTo.Enabled = False
                rdbtnVaccine.Checked = True
                chkActive.Checked = False
                chkDateRage.Checked = False
                If (IsNothing(dsReportDataset) = False) Then
                    dsReportDataset.Dispose()
                    dsReportDataset = Nothing
                End If
                dsReportDataset = getReportData()
                Fill_Vaccine()
                Fill_FundingSource()
                FillLocation_Inventory()
                FillVaccineCategory()

            ElseIf _RptName = "RptImmunizationDue" Then 'Immunization Due 
                LoadReport()
            ElseIf _RptName = "rptPatientImmSummaryByTrade" Then 'Summary
                Me.Text = "Immunization Summary Report"
                LoadReport()
            ElseIf _RptName = "RptPatientImmunizationDue" Then 'Print Due
                LoadReport()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub LoadReport()
        Try

            Dim strReportProtocol As String = ""
            Dim strReportServer As String = ""
            Dim strReportFolder As String = ""
            Dim strVirtualDir As String = ""

            Cursor.Current = Cursors.WaitCursor
            Dim oSetting As New gloSettings.GeneralSettings(GetConnectionString)
            Dim oValue As New Object()

            oSetting.GetSetting("ReportProtocol", oValue)
            If oValue IsNot Nothing Then
                strReportProtocol = oValue.ToString()
                oValue = Nothing
            End If

            oSetting.GetSetting("ReportServer", oValue)
            If oValue IsNot Nothing Then
                strReportServer = oValue.ToString()
                oValue = Nothing
            End If

            oSetting.GetSetting("ReportFolder", oValue)
            If oValue IsNot Nothing Then
                strReportFolder = oValue.ToString()
                oValue = Nothing
            End If

            oSetting.GetSetting("ReportVirtualDirectory", oValue)
            If oValue IsNot Nothing Then
                strVirtualDir = oValue.ToString()
                oValue = Nothing
            End If

            If strReportServer = "" OrElse strReportFolder = "" OrElse strVirtualDir = "" Then
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            ReportViewer1.Visible = True
            Try
                Dim reportParam As String
                Dim SSRSReportURL As System.Uri
                reportParam = "Conn=" & GetConnectionString()
                SSRSReportURL = New Uri(strReportProtocol + "://" & strReportServer & "/" & strVirtualDir)
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote
                ReportViewer1.ServerReport.ReportServerUrl = SSRSReportURL
            Catch ex As Exception
                Cursor.Current = Cursors.[Default]
                MessageBox.Show("SSRS Reporting Service is not available.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Return
            End Try

            Dim _reportName As String = _RptName

            _LocationID = cmbLocation.SelectedValue
            Dim paramList As New List(Of Microsoft.Reporting.WinForms.ReportParameter)()

            gloSSRSApplication.gloSSRS.Create_Datasource("dsEMR", "gloEMR", GetConnectionString, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR, True)
            If _RptName = "rptPatientImmSummaryByTrade" Then 'Summary
                ReportViewer1.ServerReport.ReportPath = "/" & strReportFolder & "/" & _reportName
                ReportViewer1.ShowParameterPrompts = False
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("PatientID", _PatientID, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("User", gstrLoginName, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FromDate", Date.Now, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("ToDate", Date.Now, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("LocationID", _LocationID, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Age", "(" & _age & ")", False))
                ReportViewer1.ServerReport.SetParameters(paramList)

            ElseIf _RptName = "RptPatientImmunizationDue" Then 'Print Due
                ReportViewer1.ServerReport.ReportPath = "/" & strReportFolder & "/" & _reportName
                ReportViewer1.ShowParameterPrompts = False
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("User", gstrLoginName, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("PatientID", _PatientID, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Age", "(" & _age & ")", False))
                ReportViewer1.ServerReport.SetParameters(paramList)

            ElseIf _RptName = "RptPatientImmunizationSummary" Then 'Vaccine record
                If rdb_VaccineGroup.Checked = True Then
                    _reportName = "RptPatientImmunizationSummary"
                ElseIf rdb_AdminDate.Checked = True Then
                    _reportName = "rptPatientImmSummaryByTrade"
                End If

                ReportViewer1.ServerReport.ReportPath = "/" & strReportFolder & "/" & _reportName
                ReportViewer1.ShowParameterPrompts = False
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("PatientID", _PatientID, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("User", gstrLoginName, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FromDate", dtpVacFrom.Value, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("ToDate", dtpVacTo.Value, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("LocationID", _LocationID, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Age", "(" & _age & ")", False))

                ReportViewer1.ServerReport.SetParameters(paramList)

            ElseIf _RptName = "RptImmunizationDue" Then 'Immunization Due 
                ReportViewer1.ServerReport.ReportPath = "/" & strReportFolder & "/" & _reportName

            ElseIf _RptName = "RptVaccineInventory" Then
                Dim _LocationID As Int64
                _LocationID = cmb_Location.SelectedValue
                ReportViewer1.ServerReport.ReportPath = "/" & strReportFolder & "/" & _reportName
                ReportViewer1.ShowParameterPrompts = False
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("IsFlag", 0, False))


                Dim fromdt As Date
                Dim todt As Date

                If chkDateRage.Checked Then
                    fromdt = dtReceiveFrom.Value.Date
                    todt = dtReceiveTo.Value.Date
                Else
                    fromdt = Convert.ToDateTime("01/01/1900")
                    todt = Today.Date
                End If

                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FromDate", fromdt, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("ToDate", todt, False))


                Dim Sortby As String = ""

                If rdbtnVaccine.Checked Then
                    Sortby = "Vaccine"
                ElseIf rdbtnTrade.Checked Then
                    Sortby = "TradeName"
                ElseIf rdbtnVaccineGroup.Checked Then
                    Sortby = "VaccineGroup"
                ElseIf rdbtnDateReceived.Checked Then
                    Sortby = "DateReceived"
                ElseIf rdbtnFundingSource.Checked Then
                    Sortby = "FundingSource"
                End If

                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Sortby", Sortby, False))

                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("ShowInactive", chkActive.Checked, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("LocationID", _LocationID, False))


                Dim i As Integer
                Dim Funding As String
                Funding = " "

                For i = 0 To chkFundingSourceList.CheckedItems.Count - 1
                    chkFundingSourceList.SelectedItem = chkFundingSourceList.CheckedItems(i)   '
                    If i = 0 Then
                        Funding = chkFundingSourceList.Text
                    Else
                        Funding = Funding & "|" & chkFundingSourceList.Text
                    End If
                Next

                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FundingSource", Funding, False))

                i = 0
                Dim VaccineList As String
                VaccineList = " "

                For i = 0 To ChkVaccineList.CheckedItems.Count - 1
                    ChkVaccineList.SelectedItem = ChkVaccineList.CheckedItems(i)   '
                    If i = 0 Then
                        VaccineList = ChkVaccineList.Text
                    Else
                        VaccineList = VaccineList & "|" & ChkVaccineList.Text
                    End If
                Next

                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Vaccine", VaccineList, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("CategoryID", cmb_Category.SelectedValue.ToString, False))

                ReportViewer1.ServerReport.SetParameters(paramList)

            End If
            Me.ReportViewer1.RefreshReport()
            Cursor.Current = Cursors.[Default]
        Catch ex As Exception
            If ex.Message = "Unable to connect to the remote server" Or ex.Message = "The request failed with HTTP status 404: ." Or ex.Message = "The underlying connection was closed: An unexpected error occurred on a send." Then
                MessageBox.Show("Unable to connect to the report server. Please check report settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Cursor.Current = Cursors.[Default]
        End Try
    End Sub

    Private Sub PrintSSRSReport(ReportName As String, ParameterName As String, ParameterValue As String)
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        Dim _MessageBoxCaption As String = String.Empty
        Dim _databaseConnectionString As String = String.Empty
        Dim _LoginName As String = String.Empty
        Dim gstrSQLServerName As String = String.Empty
        Dim gstrDatabaseName As String = String.Empty
        Dim gblnSQLAuthentication As String = String.Empty
        Dim gstrSQLUserEMR As String = String.Empty
        Dim gstrSQLPasswordEMR As String = String.Empty
        Dim gblnDefaultPrinter As Boolean = False
        Dim Con As SqlConnection = Nothing ' New SqlConnection(_databaseConnectionString)
        Dim PDFFileName As String = ""

        Try

            If appSettings("DataBaseConnectionString") IsNot Nothing Then
                If appSettings("DataBaseConnectionString") <> "" Then
                    _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                End If
            End If

            Con = New SqlConnection(_databaseConnectionString)
            If appSettings("UserName") IsNot Nothing Then
                If appSettings("UserName") <> "" Then
                    _LoginName = Convert.ToString(appSettings("UserName"))
                End If
            End If

            If appSettings("SQLServerName") IsNot Nothing Then
                If appSettings("SQLServerName") <> "" Then
                    gstrSQLServerName = Convert.ToString(appSettings("SQLServerName"))
                End If
            End If

            If appSettings("DatabaseName") IsNot Nothing Then
                If appSettings("DatabaseName") <> "" Then
                    gstrDatabaseName = Convert.ToString(appSettings("DatabaseName"))
                End If
            End If

            If appSettings("SQLLoginName") IsNot Nothing Then
                If appSettings("SQLLoginName") <> "" Then
                    gstrSQLUserEMR = Convert.ToString(appSettings("SQLLoginName"))
                End If
            End If

            If appSettings("SQLPassword") IsNot Nothing Then
                If appSettings("SQLPassword") <> "" Then
                    gstrSQLPasswordEMR = Convert.ToString(appSettings("SQLPassword"))
                End If
            End If

            If appSettings("DefaultPrinter") IsNot Nothing Then
                If appSettings("DefaultPrinter") <> "" Then
                    gblnDefaultPrinter = Not Convert.ToBoolean(appSettings("DefaultPrinter"))
                End If
            End If

            If appSettings("WindowAuthentication") IsNot Nothing Then
                If appSettings("WindowAuthentication") <> "" Then
                    gblnSQLAuthentication = Not Convert.ToBoolean(appSettings("WindowAuthentication"))
                End If
            End If

            If ReportName = "RptPatientImmunizationSummary" Then
                If rdb_AdminDate.Checked = True Then
                    ReportName = "rptPatientImmSummaryByTrade"
                End If
            End If

            clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
            If Not (gloGlobal.gloTSPrint.isCopyPrint And gloGlobal.gloTSPrint.UseEMFForSSRS) Then
                PDFFileName = ConvertSSRStoPDF(ReportName)
            End If
            clsPrntRpt.PrintReport(ReportName, ParameterName, ParameterValue, gblnDefaultPrinter, "", PDFFileName, ReportViewer1.ServerReport)
            clsPrntRpt = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ConvertSSRStoPDF(ByVal RptName As String) As String
        Try
            Dim warnings As Microsoft.Reporting.WinForms.Warning() = Nothing
            Dim streamids() As String = Nothing
            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim extension As String = Nothing
            Dim bytes() As Byte = Nothing
            Dim Format As String = Nothing
            Format = "PDF"

            bytes = Me.ReportViewer1.ServerReport.Render(Format, Nothing, mimeType, encoding, extension, streamids, warnings)
            Dim oSettings As gloSettings.DatabaseSetting.DataBaseSetting = New gloSettings.DatabaseSetting.DataBaseSetting
            Dim _FileName As String = ""
            _FileName = (gloSettings.FolderSettings.AppTempFolderPath _
                        + (Guid.NewGuid.ToString + ".PDF"))
            Dim fs As FileStream = New FileStream(_FileName, FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
            fs.Dispose()
            fs = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, (RptName + " Usage Report Printed.."), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            Return _FileName

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try

    End Function
    Private Sub tblbtn_Print_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Print_32.Click

        Dim parameterName As String = ""
        Dim ParameterValue As String = ""
        Dim _DefaultPrinter As Boolean = False

        If Not IsNothing(appSettings("DefaultPrinter")) Then
            If appSettings("DefaultPrinter") <> "" Then
                _DefaultPrinter = Convert.ToBoolean(appSettings("DefaultPrinter"))
            End If
        End If

        If _RptName = "rptPatientImmSummaryByTrade" Then
            parameterName = "PatientID,User,FromDate,ToDate,LocationID,Age"
            ParameterValue = _PatientID.ToString & "," & gstrLoginName.ToString & "," & Date.Now.ToString & "," & Date.Now.ToString & "," & _LocationID & "," & _age
        ElseIf _RptName = "RptPatientImmunizationSummary" Then
            parameterName = "PatientID,User,FromDate,ToDate,LocationID,Age"
            ParameterValue = _PatientID.ToString & "," & gstrLoginName.ToString & "," & dtpVacFrom.Value.ToString & "," & dtpVacTo.Value.ToString & "," & _LocationID & "," & _age
        ElseIf _RptName = "RptPatientImmunizationDue" Then
            parameterName = "User,PatientID,Age"
            ParameterValue = gstrLoginName & "," & _PatientID.ToString & "," & _age
        ElseIf _RptName = "RptVaccineInventory" Then

            _LocationID = cmb_Location.SelectedValue

            Dim fromdt As Date
            Dim todt As Date

            If chkDateRage.Checked Then
                fromdt = dtReceiveFrom.Value.Date
                todt = dtReceiveTo.Value.Date
            Else
                fromdt = Convert.ToDateTime("01/01/1900")
                todt = Today.Date
            End If

            Dim Sortby As String = ""

            If rdbtnVaccine.Checked Then
                Sortby = "Vaccine"
            ElseIf rdbtnTrade.Checked Then
                Sortby = "TradeName"
            ElseIf rdbtnVaccineGroup.Checked Then
                Sortby = "VaccineGroup"
            ElseIf rdbtnDateReceived.Checked Then
                Sortby = "DateReceived"
            ElseIf rdbtnFundingSource.Checked Then
                Sortby = "FundingSource"
            End If

            Dim i As Integer
            Dim Funding As String
            Funding = " "

            For i = 0 To chkFundingSourceList.CheckedItems.Count - 1
                chkFundingSourceList.SelectedItem = chkFundingSourceList.CheckedItems(i)   '
                If i = 0 Then
                    Funding = chkFundingSourceList.Text
                Else
                    Funding = Funding & "|" & chkFundingSourceList.Text
                End If
            Next

            i = 0
            Dim VaccineList As String
            VaccineList = " "

            For i = 0 To ChkVaccineList.CheckedItems.Count - 1
                ChkVaccineList.SelectedItem = ChkVaccineList.CheckedItems(i)   '
                If i = 0 Then
                    VaccineList = Replace(ChkVaccineList.Text, ",", "@")
                Else
                    VaccineList = VaccineList & "|" & Replace(ChkVaccineList.Text, ",", "@")
                End If
            Next

            parameterName = "FromDate,ToDate,Sortby,ShowInactive,LocationID,FundingSource,Vaccine"
            ParameterValue = fromdt.ToString & "," & todt.ToString & "," & Sortby.ToString & "," & chkActive.Checked.ToString & "," & _LocationID.ToString & "," & Funding.ToString & "," & VaccineList.ToString

        End If

        PrintSSRSReport(_RptName, parameterName, ParameterValue)

    End Sub

    Private Sub tblbtn_Close_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close_32.Click
        Me.Close()
    End Sub

    Private Sub tblbtnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtnShowReport.Click
        tblbtn_Print_32.Enabled = True
        LoadReport()
    End Sub

    Private Sub chkDateRange_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkDateRange.CheckStateChanged
        If chkDateRange.CheckState = CheckState.Checked Then
            dtpVacFrom.Enabled = True
            dtpVacTo.Enabled = True
        Else
            dtpVacFrom.Enabled = False
            dtpVacTo.Enabled = False
        End If
    End Sub

    Private Function getReportData() As DataSet
        Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
        Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
        Dim Cmd As New System.Data.SqlClient.SqlCommand
        Dim ds As New DataSet
        Try

            Cmd = New System.Data.SqlClient.SqlCommand("Rpt_VaccineInventoryReport", Con)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@IsFlag", SqlDbType.Int)
            Cmd.Parameters("@IsFlag").Value = 1
            sqladpt.SelectCommand = Cmd
            sqladpt.Fill(ds)
            Con.Close()
            ds.Tables(0).TableName = "FundingSource"
            ds.Tables(1).TableName = "Vaccine"
            Return ds
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
            'If Not IsNothing(ds) Then : Slr: Returning hence don't dfispose
            '    ds.Dispose()
            '    ds = Nothing
            'End If
        End Try
    End Function

    Private Function GetLocation() As DataTable
        Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
        Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
        Dim Cmd As New System.Data.SqlClient.SqlCommand
        Dim dt As New DataTable
        Try

            Cmd = New System.Data.SqlClient.SqlCommand("GetIM_Location", Con)
            Cmd.CommandType = CommandType.StoredProcedure

            sqladpt.SelectCommand = Cmd
            sqladpt.Fill(dt)
            Con.Close()

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
            'If Not IsNothing(dt) Then SLR: Don't dispsoe, since it is returned
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function

    Private Sub Fill_Vaccine()
        Try
            With ChkVaccineList
                Dim odt As DataTable = Nothing
                If (IsNothing(dsReportDataset) = False) Then
                    odt = dsReportDataset.Tables("Vaccine")
                    Dim dtNewRow As DataRow
                    dtNewRow = odt.NewRow()
                    dtNewRow.Item("Vaccine") = "Select All"
                    odt.Rows.InsertAt(dtNewRow, 0)
                End If

                .DataSource = odt
                .DisplayMember = odt.Columns("Vaccine").ColumnName.ToString
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillLocation()
        Try
            Dim dtVaccineInformation As DataTable
            dtVaccineInformation = GetLocation()
            cmbLocation.DataSource = dtVaccineInformation
            cmbLocation.ValueMember = "nLocationID"
            cmbLocation.DisplayMember = "sLocation"
            cmbLocation.SelectedValue = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillLocation_Inventory()
        Try
            Dim dtVaccineInformation As DataTable
            dtVaccineInformation = GetLocation()
            cmb_Location.DataSource = dtVaccineInformation
            cmb_Location.ValueMember = "nLocationID"
            cmb_Location.DisplayMember = "sLocation"
            cmb_Location.SelectedValue = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetIMLocation() As Decimal
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim Locationid As Decimal = 0
        Try
            strSQL = " Select dbo.GetIMLocationID ( " & _PatientID & " ) "
            dt = GetList(strSQL)
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dt)

            oDB.Disconnect()

            If (IsNothing(dt) = False) Then
                If dt.Rows.Count > 0 Then
                    Locationid = dt.Rows(0)(0)
                End If
            End If


            Return Locationid

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Function GetList(ByVal strSQL As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dtRoute As DataTable = Nothing
        Try
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dtRoute)

            oDB.Disconnect()

            Return dtRoute
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub Fill_FundingSource()
        Try
            With chkFundingSourceList
                Dim odt As DataTable = Nothing
                If (IsNothing(dsReportDataset) = False) Then
                    odt = dsReportDataset.Tables("FundingSource")
                    Dim dtNewRow As DataRow
                    dtNewRow = odt.NewRow()
                    dtNewRow.Item("FundingSource") = "Select All"
                    odt.Rows.InsertAt(dtNewRow, 0)
                End If

                .DataSource = odt
                .DisplayMember = odt.Columns("FundingSource").ColumnName.ToString
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkFundingSourceList_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chkFundingSourceList.ItemCheck
        Try
            If chkFundingSourceList.Text = "Select All" Then
                Dim chkstate As CheckState
                chkstate = e.NewValue
                Dim i As Integer
                RemoveHandler chkFundingSourceList.ItemCheck, AddressOf chkFundingSourceList_ItemCheck
                For i = 0 To chkFundingSourceList.Items.Count - 1
                    chkFundingSourceList.SetItemCheckState(i, chkstate)
                Next
                AddHandler chkFundingSourceList.ItemCheck, AddressOf chkFundingSourceList_ItemCheck
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddHandler chkFundingSourceList.ItemCheck, AddressOf chkFundingSourceList_ItemCheck
        End Try
    End Sub

    Private Sub ChkVaccineList_ItemCheck(sender As Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles ChkVaccineList.ItemCheck
        Try
            If ChkVaccineList.Text = "Select All" Then
                Dim chkstate As CheckState
                chkstate = e.NewValue
                Dim i As Integer
                RemoveHandler ChkVaccineList.ItemCheck, AddressOf ChkVaccineList_ItemCheck
                For i = 0 To ChkVaccineList.Items.Count - 1
                    ChkVaccineList.SetItemCheckState(i, chkstate)
                Next
                AddHandler ChkVaccineList.ItemCheck, AddressOf ChkVaccineList_ItemCheck
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddHandler ChkVaccineList.ItemCheck, AddressOf ChkVaccineList_ItemCheck
        End Try
    End Sub

    Private Sub chkDateRage_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkDateRage.CheckStateChanged
        If chkDateRage.CheckState = CheckState.Checked Then
            dtReceiveFrom.Enabled = True
            dtReceiveTo.Enabled = True
        Else
            dtReceiveFrom.Enabled = False
            dtReceiveTo.Enabled = False
        End If
    End Sub

    Private Function GetVaccineCategory() As DataTable
        Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
        Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
        Dim Cmd As New System.Data.SqlClient.SqlCommand
        Dim dt As New DataTable
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("GetIM_VaccineCategory", Con)
            Cmd.CommandType = CommandType.StoredProcedure

            sqladpt.SelectCommand = Cmd
            sqladpt.Fill(dt)
            Con.Close()

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function

    Private Sub FillVaccineCategory()
        Try
            Dim dt As DataTable
            dt = GetVaccineCategory()
            cmb_Category.DataSource = dt
            cmb_Category.ValueMember = "nCategoryID"
            cmb_Category.DisplayMember = "sDescription"
            cmb_Category.SelectedValue = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class