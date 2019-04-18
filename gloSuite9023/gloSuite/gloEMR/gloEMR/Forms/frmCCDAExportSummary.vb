Imports System.IO
Imports gloCCDLibrary
Imports gloCCDSchema

Public Class frmCCDAExportSummary

    Private oListControl As gloListControl.gloListControl
    Private ofrmList As frmViewListControl
    Private strServicesConnString As String
    Private dtCCDASettings As DataTable
    Private blnAllowUserConfiguration As Boolean

    Public Property AllowUserConfiguration As Boolean
        Set(value As Boolean)
            blnAllowUserConfiguration = value
        End Set
        Get
            Return blnAllowUserConfiguration
        End Get
    End Property

    Public Function GetServicesConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        Try
            If isSQLAuthentication = False Then
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
            Else
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
            End If

            Return strConnectionString
        Catch ex As Exception
            Return Nothing
        Finally
            strConnectionString = Nothing

        End Try
    End Function

    Private Sub frmCCDAExportSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strServicesConnString = GetServicesConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        dtCCDASettings = LoadSettings()


        If dtCCDASettings.Rows.Count > 0 Then
            txtExportLocation.Text = dtCCDASettings.Rows(0)("sSettingsValue")
        End If

        If blnAllowUserConfiguration = False Then
            txtExportLocation.Enabled = False
            btnBrowseLocation.Enabled = False
        Else
            txtExportLocation.Enabled = True
            btnBrowseLocation.Enabled = True
        End If

        lblStatus.Visible = False
        pnlStatusBar.Visible = False
        dtpFrom.CustomFormat = "MM/dd/yyyy"
        dtpToDate.CustomFormat = "MM/dd/yyyy"
        chkintime.Checked = False
        chkintime.Enabled = False
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Open, "Export Summary Opened", gloAuditTrail.ActivityOutCome.Success)

    End Sub

    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        Dim _listviewItem As ListViewItem = Nothing
        Try
            pgrbarStatus.Value = 1

            lblStatus.Text = ""

            If oListControl.SelectedItems.Count > 0 Then
                'Dim dcID As New DataColumn("ID")

                'Dim dcName As New DataColumn("Name")
                'dtPatients.Columns.Add(dcID)

                'dtPatients.Columns.Add(dcName)
                lstviewPatient.Items.Clear()
                For i As Integer = 0 To oListControl.SelectedItems.Count - 1
                    _listviewItem = New ListViewItem()
                    _listviewItem.SubItems.Add(oListControl.SelectedItems(i).Description)
                    _listviewItem.Text = oListControl.SelectedItems(i).Description
                    _listviewItem.Tag = oListControl.SelectedItems(i).ID
                    lstviewPatient.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If
            ofrmList.Close()

        Catch ex As Exception
        Finally

        End Try
    End Sub

    Private Function GenerateCDA(ByVal FilePath As String, ByVal _PatientId As Int64) As String

        Dim _FromDate As String
        Dim _ToDate As String
        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        Dim strFilePath As String = FilePath
        Dim msg As String = String.Empty

        Try

            _FromDate = Nothing
            _ToDate = Nothing

            If strFilePath <> "" Then
                If Directory.Exists(strFilePath) = True Then
                    Dim objCDAWriterParameters As gloCDAWriterParameters = SetWriterParametrs()
                    oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()

                    Dim _tempExamId As Int64 = 0
                    Dim _tempVisitId As Int64 = 0
                    Dim _tempOrderId As Int64 = 0

                    strFilePath = oCDADataExtraction.GenerateClinicalInformation(_PatientId, gnLoginID, objCDAWriterParameters, _tempVisitId, _FromDate, _ToDate, strFilePath, False, Nothing, _tempOrderId)
                    msg = oCDADataExtraction.strmsg
                    If msg <> "" Then
                        MessageBox.Show(msg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End If
                    objCDAWriterParameters.Dispose()
                    objCDAWriterParameters = Nothing
                    If strFilePath <> "" Then
                    Else
                        MessageBox.Show("CDA file not generated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If Not IsNothing(oCDADataExtraction) Then
                            oCDADataExtraction.Dispose()
                            oCDADataExtraction = Nothing
                        End If
                        GenerateCDA = Nothing
                        Exit Function
                    End If
                Else
                    'MessageBox.Show("Invalid CDA file path. Set a valid CDA path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & strFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Not IsNothing(oCDADataExtraction) Then
                        oCDADataExtraction.Dispose()
                        oCDADataExtraction = Nothing
                    End If
                    GenerateCDA = Nothing
                    Exit Function
                End If
            Else
                MessageBox.Show("The CCD/C-CDA file path is not set in gloEMR admin. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            If msg <> "" Then
                MessageBox.Show(msg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            GenerateCDA = Nothing
        Finally

            If Not IsNothing(oCDADataExtraction) Then
                oCDADataExtraction.Dispose()
                oCDADataExtraction = Nothing
            End If
        End Try
        Return strFilePath
    End Function

    Private Function SetWriterParametrs() As gloCDAWriterParameters

        Dim objCDAWriterParameters As gloCDAWriterParameters = New gloCDAWriterParameters()

        objCDAWriterParameters.CDAFileType = CDAFileTypeEnum.AmbulatorySummary
        objCDAWriterParameters.Demographics = True
        objCDAWriterParameters.SmokingStatus = True
        objCDAWriterParameters.Problems = True
        objCDAWriterParameters.Allergies = True
        objCDAWriterParameters.CareTeamMember = True
        objCDAWriterParameters.Procedures = True
        objCDAWriterParameters.CarePlan_GoalsAndInstructions = True
        objCDAWriterParameters.VitalSigns = True
        objCDAWriterParameters.LaboratoryResult = True
        objCDAWriterParameters.LaboratoryTest = True
        objCDAWriterParameters.Medications = True
        objCDAWriterParameters.EncounterDiagnoses = True
        objCDAWriterParameters.Immunizations = True
        objCDAWriterParameters.CognitiveStatus = True
        objCDAWriterParameters.ReasonForReferral = True
        objCDAWriterParameters.ReferringProvider = True
        objCDAWriterParameters.FunctionalStatus = True
        objCDAWriterParameters.CareProvider = True
        objCDAWriterParameters.CareOfficeContact = True
        objCDAWriterParameters.FamilyHistory = True
        objCDAWriterParameters.SocialHistory = True
        objCDAWriterParameters.Implant = True
        objCDAWriterParameters.Assessments = True
        objCDAWriterParameters.TreatmentPlan = True
        objCDAWriterParameters.Goals = True
        objCDAWriterParameters.HealthConcern = True
        objCDAWriterParameters.Visit_DateAndLocation = True
        Return objCDAWriterParameters

    End Function

    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
    End Sub

    Private Sub btnBrowsePatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePatient.Click
        ofrmList = New frmViewListControl
        oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Patient, True, Me.Width)

        AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
        AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
        ofrmList.Controls.Add(oListControl)
        oListControl.Dock = DockStyle.Fill
        oListControl.BringToFront()

        For i As Integer = 0 To lstviewPatient.Items.Count - 1
            oListControl.SelectedItems.Add(Convert.ToInt64(lstviewPatient.Items(i).Tag), lstviewPatient.Items(i).Text)
        Next
        oListControl.ShowHeaderPanel(False)
        oListControl.OpenControl()
        ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        ofrmList.Text = "Patient"
        ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))
        RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
        RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
        ofrmList.Controls.Remove(oListControl)
        oListControl.Dispose()
        oListControl = Nothing
        If IsNothing(ofrmList) = False Then
            ofrmList.Dispose()
            ofrmList = Nothing
        End If


    End Sub

    Private Function LoadSettings() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(strServicesConnString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtSettings As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters

            oDBParameters.Add("@DatabaseName", gstrDatabaseName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@ServiceName", "gloCCDA", ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@SettingName", "CCDLocation", ParameterDirection.Input, SqlDbType.Text)

            oDB.Retrive("gsp_LoadServiceSettings", oDBParameters, dtSettings)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try

        Return dtSettings

    End Function

    Private Sub ExportCCDA()

        Dim _PatientID As Int64

        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        Dim strSelectedPath As String = ""
        Dim _ErrorPatients As String = ""

        Try

            Me.Enabled = False

            If lstviewPatient.Items.Count > 0 Then
                lblStatus.Visible = True
                pnlStatusBar.Visible = True
                pgrbarStatus.Minimum = 0
                pgrbarStatus.Maximum = lstviewPatient.Items.Count
                pgrbarStatus.Step = 1
                pgrbarStatus.Visible = True

                'If gstrCCDFilePath <> "" Then
                'If Directory.Exists(gstrCCDFilePath) = True Then

                'FolderBrowserDialog1.SelectedPath = gstrCCDFilePath
                'If FolderBrowserDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                strSelectedPath = txtExportLocation.Text   'FolderBrowserDialog1.SelectedPath & "\"

                If strSelectedPath.EndsWith("\") = False Then
                    strSelectedPath += "\"
                End If

                For i As Integer = 0 To lstviewPatient.Items.Count - 1

                    _PatientID = lstviewPatient.Items(i).Tag

                    Application.DoEvents()
                    lblStatus.Text = "Exporting patient ..." & i + 1 & " Out of " & lstviewPatient.Items.Count
                    Dim strFilepath As String = ""

                    If strSelectedPath <> "" Then
                        If Directory.Exists(strSelectedPath) = True Then

                            Dim objCDAWriterParameters As gloCDAWriterParameters = SetWriterParametrs()
                            oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
                            oCDADataExtraction.IsExportSummary = True
                            If (chkDate.Checked = False) Then
                                strFilepath = oCDADataExtraction.GenerateClinicalInformation(_PatientID, gnLoginID, objCDAWriterParameters, 0, Nothing, Nothing, , False, Nothing, 0, strSelectedPath)
                            Else
                                Dim _FromDate As String = dtpFrom.Value
                                Dim _ToDate As String = dtpToDate.Value
                                If (chkintime.Checked = False) Then
                                    _FromDate = dtpFrom.Value.Date    '' date format change to datetime 
                                    _ToDate = dtpToDate.Value.Date
                                    _FromDate = _FromDate & " " & "12:00:00 AM"
                                    _ToDate = _ToDate & " " & "11:59:00 PM"
                                Else

                                End If
                                strFilepath = oCDADataExtraction.GenerateClinicalInformation(_PatientID, gnLoginID, objCDAWriterParameters, 0, _FromDate, _ToDate, , False, Nothing, 0, strSelectedPath)

                            End If
                            If oCDADataExtraction.strmsg <> "" Then
                                If _ErrorPatients = "" Then
                                    _ErrorPatients = Convert.ToString(lstviewPatient.Items(i).Text)
                                Else
                                    _ErrorPatients = _ErrorPatients & " , " & Convert.ToString(lstviewPatient.Items(i).Text)
                                End If

                            End If
                            oCDADataExtraction.Dispose()
                            oCDADataExtraction = Nothing
                            objCDAWriterParameters.Dispose()
                            objCDAWriterParameters = Nothing
                        End If

                    End If

                    pgrbarStatus.Value = i + 1
                Next
                If _ErrorPatients = "" Then
                    lblStatus.Text = "Done..."

                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, "Exported Summary. ", gloAuditTrail.ActivityOutCome.Success)
                Else
                    lblStatus.Text = "Failed for patients..." & _ErrorPatients
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, lblStatus.Text, gloAuditTrail.ActivityOutCome.Failure)
                End If

                'End If
                'FolderBrowserDialog1.Dispose()
                'FolderBrowserDialog1 = Nothing
                'Else
                '    'MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & gstrCCDFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'End If

                'Else
                '    MessageBox.Show("Set the CCD file generation path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'End If


            Else
                MessageBox.Show("Select at least one patient. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If lblStatus.Text = "Done..." Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Export, "Exported Summary", gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Export, ex, gloAuditTrail.ActivityOutCome.Failure)

        Finally

            '06-Oct-1027 Aniket: Resolving Bug #109641: gloEMR : Export Summary : Export summaries should show status bar green after export patient done. 
            pgrbarStatus.Value = pgrbarStatus.Maximum

            If Not IsNothing(oCDADataExtraction) Then
                oCDADataExtraction.Dispose()
                oCDADataExtraction = Nothing
            End If

            Me.Enabled = True

        End Try

    End Sub

    Private Sub tblExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblExport.Click

        If Trim(txtExportLocation.Text) = "" Then
            MessageBox.Show("Select the CCDA Export Location.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnBrowseLocation.Focus()
            Exit Sub
        End If

        If (Directory.Exists(Trim(txtExportLocation.Text))) = False Then
            MessageBox.Show("Export directory not found. Select a valid CCDA Export Location.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ExportCCDA()

    End Sub

    Private Sub tblClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblClose.Click
        Me.Close()

    End Sub

    Private Sub btnClearPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPatient.Click

        For i As Integer = 0 To lstviewPatient.Items.Count - 1
            If lstviewPatient.SelectedItems.Count > 0 Then
                If lstviewPatient.SelectedItems(i).Selected = True Then
                    lstviewPatient.Items.Remove(lstviewPatient.SelectedItems(i))
                    Exit For
                End If
            End If

        Next

    End Sub

    Private Sub btnClearAllPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAllPatient.Click
        lstviewPatient.Items.Clear()

    End Sub


    Private Sub chkDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDate.CheckedChanged
        If (chkDate.Checked = True) Then
            dtpFrom.Enabled = True
            dtpToDate.Enabled = True
            chkintime.Enabled = True
            ''"MM/dd/yyyy hh:mm:ss tt"
            'dtpFrom.CustomFormat = "MM/dd/yyyy"
            'dtpToDate.CustomFormat = "MM/dd/yyyy"
        Else
            dtpFrom.Enabled = False
            dtpToDate.Enabled = False
            chkintime.Enabled = False

        End If
        chkintime.Checked = False

    End Sub

    Private Sub chkintime_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkintime.CheckedChanged
        ''"MM/dd/yyyy hh:mm:ss tt"
        If (chkintime.Checked = True) Then
            dtpFrom.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
            dtpToDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Else
            dtpFrom.CustomFormat = "MM/dd/yyyy"
            dtpToDate.CustomFormat = "MM/dd/yyyy"
        End If
    End Sub

    Private Sub btnBrowseLocation_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseLocation.Click

        FolderBrowserDialog1.SelectedPath = txtExportLocation.Text

        If FolderBrowserDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
            txtExportLocation.Text = FolderBrowserDialog1.SelectedPath
        End If

    End Sub

End Class