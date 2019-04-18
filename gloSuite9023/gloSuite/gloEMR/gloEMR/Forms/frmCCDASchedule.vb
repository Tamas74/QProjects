Imports System
Imports System.Data
Imports gloCommon

Public Class frmCCDASchedule

    Private strServicesConnString As String
    Private dtCCDASettings As DataTable

    Private Sub cmbScheduleType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

        Try
            SetScheduleType()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub


    Private Sub SetScheduleType()

        Try

            If cmbScheduleType.Text = "One Time" Then
                grpOneTime.Enabled = True
                grpRecurring.Enabled = False
                grprdb.Enabled = False
            ElseIf cmbScheduleType.Text = "Recurring" Then
                grpOneTime.Enabled = False
                grpRecurring.Enabled = True
                grprdb.Enabled = False
            ElseIf cmbScheduleType.Text = "Recurring Date" Then
                grprdb.Enabled = True
                grpOneTime.Enabled = False
                grpRecurring.Enabled = False
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        

    End Sub

    Private Sub SetSettings()

        Dim dvSettings As New DataView(dtCCDASettings)


        Try


            dvSettings.RowFilter = "sSettingsName = 'Advanced CCD'"

            If dvSettings.Count = 0 Then
                Me.Visible = False
                MessageBox.Show("There is no CCDA Service configured for this database. CCDA schedule can only be set if the CCDA Service is configured.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
            End If


            dvSettings.RowFilter = "sSettingsName = 'CCDAScheduleType'"
            If dvSettings.Count > 0 Then
                cmbScheduleType.Text = dvSettings(0)("sSettingsValue")
            Else
                'Upgrade Scenario
                cmbScheduleType.SelectedIndex = 1 ' Recurring
                rbServiceConfigured.Checked = True
            End If

            SetScheduleType()

            dvSettings.RowFilter = "sSettingsName = 'CCDLocation'"
            If dvSettings.Count > 0 Then
                txtExportLocation.Text = dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDAOneTimeStartDate'"
            If dvSettings.Count > 0 Then
                dtOneTimeStartDate.Value = dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDAOneTimeEndDate'"
            If dvSettings.Count > 0 Then
                dtOneTimeEndDate.Value = dvSettings(0)("sSettingsValue")
            End If


            dvSettings.RowFilter = "sSettingsName = 'CCDAOneTimeGenerateOnDate'"
            If dvSettings.Count > 0 Then
                dtOneTimeGenerateOnDate.Value = dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDAOneTimeGenerateOnTime'"
            If dvSettings.Count > 0 Then
                dtOneTimeGenerateOnTime.Value = System.DateTime.Now.ToString("MM/dd/yyyy") + " " + dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDAnumRecursDays'"
            If dvSettings.Count > 0 Then
                numRecursDays.Value = dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDARecursGenerateOnDate'"
            If dvSettings.Count > 0 Then
                dtRecursGenerateOnDate.Value = dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDARecursGenerateOnTime'"
            If dvSettings.Count > 0 Then
                dtRecursGenerateOnTime.Value = System.DateTime.Now.ToString("MM/dd/yyyy") + " " + dvSettings(0)("sSettingsValue")
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDASystemConfigured'"
            If dvSettings.Count > 0 Then
                rbSystemConfigured.Checked = dvSettings(0)("sSettingsValue")
                rbServiceConfigured.Checked = Not rbSystemConfigured.Checked
            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDARecursStartDate'"
            If dvSettings.Count > 0 Then
                dtRecursStartDate.Value = dvSettings(0)("sSettingsValue")

            End If



            dvSettings.RowFilter = "sSettingsName = 'CCDARecurringDayBasedEncountersFrom'"
            If dvSettings.Count > 0 Then
                dtp_rdb_encfr.Value = dvSettings(0)("sSettingsValue")

            End If



            dvSettings.RowFilter = "sSettingsName = 'CCDARecurringDayBasedEncountersTo'"
            If dvSettings.Count > 0 Then
                dtp_rdb_encto.Value = dvSettings(0)("sSettingsValue")

            End If

            dvSettings.RowFilter = "sSettingsName = 'CCDARecurringDayBasedEncountersExportDayoftheMonth'"
            If dvSettings.Count > 0 Then
                num_rdb_dom.Value = dvSettings(0)("sSettingsValue")

            End If
            dvSettings.RowFilter = "sSettingsName = 'CCDARecurringDayBasedExportTime'"
            If dvSettings.Count > 0 Then
                dt_rdb_tm.Value = System.DateTime.Now.ToString("MM/dd/yyyy") + " " + dvSettings(0)("sSettingsValue")

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub SaveSettings()

        Dim dtSave As New DataTable

        Try

            If System.IO.Directory.Exists(txtExportLocation.Text) = False Then
                MessageBox.Show("The Export Location folder is not accessible. Select another location.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            dtSave.Columns.Add("SettingsName", GetType(String))
            dtSave.Columns.Add("SettingsValue", GetType(String))

            'dtSave.Rows.Add(dtSave.NewRow)
            'dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAScheduleEnabled"
            'dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = chkEnabled.Enabled

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAScheduleType"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = cmbScheduleType.Text

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDLocation"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = txtExportLocation.Text

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAOneTimeStartDate"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtOneTimeStartDate.Value '.Date

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAOneTimeEndDate"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtOneTimeEndDate.Value '.Date

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAOneTimeGenerateOnDate"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtOneTimeGenerateOnDate.Value.Date

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAOneTimeGenerateOnTime"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtOneTimeGenerateOnTime.Value.ToString("hh:mm tt")

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDAnumRecursDays"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = numRecursDays.Value

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecursGenerateOnDate"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtRecursGenerateOnDate.Value.Date

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecursGenerateOnTime"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtRecursGenerateOnTime.Value.ToString("hh:mm tt")

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDASystemConfigured"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = rbSystemConfigured.Checked

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecursStartDate"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtRecursStartDate.Value.Date



            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecurringDayBasedEncountersFrom"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtp_rdb_encfr.Value

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecurringDayBasedEncountersTo"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dtp_rdb_encto.Value

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecurringDayBasedEncountersExportDayoftheMonth"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = num_rdb_dom.Value

            dtSave.Rows.Add(dtSave.NewRow)
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsName") = "CCDARecurringDayBasedExportTime"
            dtSave.Rows(dtSave.Rows.Count - 1)("SettingsValue") = dt_rdb_tm.Value.ToString("hh:mm tt")

            SaveSettings(dtSave)
            Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If IsNothing(dtSave) = False Then
                dtSave.Dispose()
                dtSave = Nothing
            End If

        End Try

    End Sub

    Private Sub tblSaveCls_Click(sender As System.Object, e As System.EventArgs) Handles tblSaveCls.Click

        Try
            SaveSettings()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Close, "CCDA Schedule Saved", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    Public Function LoadSettings() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(strServicesConnString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtSettings As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters

            oDBParameters.Add("@DatabaseName", gstrDatabaseName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@ServiceName", "gloCCDA", ParameterDirection.Input, SqlDbType.Text)


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

    Private Sub SaveSettings(SettingsData As DataTable)

        Dim oDB As New gloDatabaseLayer.DBLayer(strServicesConnString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDBParameters.Clear()

            oDBParameters.Add("@TVP_ServiceSettings", SettingsData, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@DatabaseName", gstrDatabaseName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@ServiceName", "gloCCDA", ParameterDirection.Input, SqlDbType.Text)

            oDB.Execute("gsp_InUpServiceSettings", oDBParameters)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If (IsNothing(oDBParameters) = False) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
            If (IsNothing(oDB) = False) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub


    Private Sub tblClose_Click(sender As System.Object, e As System.EventArgs) Handles tblClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub frmCCDASchedule_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

        strServicesConnString = GetCCDAServiceConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)

        Try

            dtCCDASettings = LoadSettings()
            SetSettings()

            AddHandler cmbScheduleType.SelectedIndexChanged, AddressOf cmbScheduleType_SelectedIndexChanged
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Open, "CCDA Schedule Opened", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        

    End Sub

    Public Function GetCCDAServiceConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String

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

    Private Sub btnBrowse_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowse.Click

        Try
            fbExportLocation.ShowDialog(Me)

            If fbExportLocation.SelectedPath <> "" Then
                txtExportLocation.Text = fbExportLocation.SelectedPath
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    Private Sub rbSystemServiceConfigured_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSystemConfigured.CheckedChanged, rbServiceConfigured.CheckedChanged

        Try
            If rbSystemConfigured.Checked = True Then
                rbSystemConfigured.Font = New Font("Tahoma", 9, FontStyle.Bold)
                grpMain.Enabled = True
            Else
                rbSystemConfigured.Font = New Font("Tahoma", 9, FontStyle.Regular)
                grpMain.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on CCD Schedule. " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try


    End Sub

    
    
End Class