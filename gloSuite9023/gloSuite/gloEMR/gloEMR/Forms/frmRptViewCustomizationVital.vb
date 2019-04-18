Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports gloSSRSApplication.SSRS
Public Class frmRptViewVitalCustomization
    Dim _PatientID As Long
    Dim _RptName As String = ""
    'The Below flags are used in order to unable and disable print and export functionality.
    Private blnPrintVitals As Boolean = False
    Public Shared IsOpen As Boolean = False
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
#Region " TO Check the Multiple instances Of Form "

    Private Shared frm As frmRptViewVitalCustomization
    Public Shared Function GetInstance(ByVal PatientID As Long, ByVal ReportName As String) As frmRptViewVitalCustomization
        Try
            IsOpen = False

            For Each f As Form In Application.OpenForms
                If f.Name = "frmRptViewVitalCustomization" Then
                    IsOpen = True
                    frm = f
                End If
            Next
            If (IsOpen = False) Then
                frm = New frmRptViewVitalCustomization(PatientID, ReportName)
            End If

        Finally

        End Try
        Return frm
    End Function

#End Region

    Private Sub frmRptViewCustomizationVital_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.ReportViewer1.RefreshReport()
        tblbtn_Print_32.Enabled = True
        tblbtn_Export_32.Enabled = True
        dtpVitalsFrom.Value = Today.AddYears(-1)
       
        blnPrintVitals = True
        LoadReport()
        blnPrintVitals = False

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


            Dim paramList As New List(Of Microsoft.Reporting.WinForms.ReportParameter)()
            Dim fromdt As Date
            Dim todt As Date

            gloSSRSApplication.gloSSRS.Create_Datasource("dsEMR", "gloEMR", GetConnectionString, gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR, True)
            If _RptName = "RptViewVitalsCustomization" Then
                ReportViewer1.ServerReport.ReportPath = "/" & strReportFolder & "/" & _reportName
                ReportViewer1.ShowParameterPrompts = False

                If chkDateRange.Checked Then
                    fromdt = dtpVitalsFrom.Value.Date
                    todt = dtpVitalsTo.Value.Date
                Else
                    fromdt = Today.Date.AddYears(-1)
                    todt = Today.Date
                End If
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("nPatientID", _PatientID, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("FromDate", fromdt, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("ToDate", todt, False))
                paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("User", gstrLoginName, False))
                If blnPrintVitals = True Then
                    paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Flag", "Print", False))
                Else
                    paramList.Add(New Microsoft.Reporting.WinForms.ReportParameter("Flag", "Export", False))
                End If


                ReportViewer1.ServerReport.SetParameters(paramList)

            End If
            Me.ReportViewer1.RefreshReport()
            tblbtnShowReport.Enabled = True
            tblbtnViewReport.Enabled = True

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
        Dim Con As SqlConnection = Nothing
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

            ReportName = "RptViewVitalsCustomization"


            clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)

            If Not (gloGlobal.gloTSPrint.isCopyPrint AndAlso gloGlobal.gloTSPrint.UseEMFForSSRS) Then
                PDFFileName = ConvertSSRStoPDF(ReportName)
            End If
            clsPrntRpt.PrintReport(ReportName, ParameterName, ParameterValue, gblnDefaultPrinter, "", PDFFileName, ReportViewer1.ServerReport)
            clsPrntRpt = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportSSRSReport(ReportName As String)

        Dim FileDialog As New SaveFileDialog()
        Try
            FileDialog.Filter = "Excel (*.xls)|*.xls"
            FileDialog.AddExtension = True
            FileDialog.FileName = "RptViewVitalsCustomization"
            If FileDialog.ShowDialog(Me) <> Windows.Forms.DialogResult.Cancel Then

                Dim warnings As Microsoft.Reporting.WinForms.Warning() = Nothing
                Dim streamids() As String = Nothing
                Dim mimeType As String = Nothing
                Dim encoding As String = Nothing
                Dim extension As String = Nothing
                Dim bytes() As Byte = Nothing
                Dim Format As String = Nothing
                Format = "Excel"

                bytes = Me.ReportViewer1.ServerReport.Render(Format, Nothing, mimeType, encoding, extension, streamids, warnings)

                Dim fs As FileStream = New FileStream(FileDialog.FileName, FileMode.Create)
                fs.Write(bytes, 0, bytes.Length)
                fs.Close()
                fs.Dispose()
                fs = Nothing

                MessageBox.Show("File Saved Successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, (_RptName + " Usage Report Printed.."), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            FileDialog.Dispose()
            FileDialog = Nothing
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
        blnPrintVitals = True
        Dim parameterName As String = ""
        Dim ParameterValue As String = ""
        Dim _DefaultPrinter As Boolean = False

        If Not IsNothing(appSettings("DefaultPrinter")) Then
            If appSettings("DefaultPrinter") <> "" Then
                _DefaultPrinter = Convert.ToBoolean(appSettings("DefaultPrinter"))
            End If
        End If

        Dim fromdt As Date
        Dim todt As Date
        If _RptName = "RptViewVitalsCustomization" Then
            parameterName = "nPatientID,FromDate,ToDate"
            If chkDateRange.Checked Then
                fromdt = dtpVitalsFrom.Value.Date
                todt = dtpVitalsTo.Value.Date
            Else
                fromdt = Today.Date.AddYears(-1)
                todt = Today.Date
            End If
            ParameterValue = _PatientID.ToString & "," & gstrLoginName.ToString & "," & fromdt.ToString & "," & todt.ToString
        End If

        PrintSSRSReport(_RptName, parameterName, ParameterValue)
        blnPrintVitals = False

    End Sub

    Private Sub tblbtn_Close_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close_32.Click
        Me.Close()
    End Sub

    Private Sub tblbtnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtnShowReport.Click
        If dtpVitalsTo.Value.Date < dtpVitalsFrom.Value.Date Then
            MessageBox.Show("To Date should be greater than or equal to From Date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpVitalsTo.Focus()
            Exit Sub
        End If
        tblbtn_Print_32.Enabled = True
        blnPrintVitals = True
        LoadReport()
        blnPrintVitals = False

    End Sub

    Private Sub chkDateRange_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkDateRange.CheckStateChanged
        If chkDateRange.CheckState = CheckState.Checked Then
            dtpVitalsFrom.Enabled = True
            dtpVitalsTo.Enabled = True
        Else
            dtpVitalsFrom.Enabled = False
            dtpVitalsTo.Enabled = False
        End If
    End Sub

    Private Sub tblbtn_Export_32_Click(sender As Object, e As System.EventArgs) Handles tblbtn_Export_32.Click
        ExportSSRSReport("RptViewVitalsCustomization")

    End Sub

    Private Sub tblbtnViewReport_Click(sender As Object, e As System.EventArgs) Handles tblbtnViewReport.Click
        If dtpVitalsTo.Value.Date < dtpVitalsFrom.Value.Date Then
            MessageBox.Show("To Date should be greater than or equal to From Date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtpVitalsTo.Focus()
            Exit Sub
        End If
        tblbtn_Export_32.Enabled = True
        tblbtn_Print_32.Enabled = False
        LoadReport()
    End Sub
End Class