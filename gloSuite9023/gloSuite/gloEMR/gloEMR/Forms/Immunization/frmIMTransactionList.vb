Imports gloUserControlLibrary
Imports gloEMR.gloEMRWord

Public Class frmIMTransactionList

    Dim _PatientID As Long
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip

    Public Shared blnCancelDelete As Boolean = False

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub

    Private Sub frmIMTransactionList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            tblbtn_V04IR.Visible = gblnEnableCQMCypressTesting '''' Show V04IR only for certification
            Set_PatientDetailStrip()
            ShowImmunizationList()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowImmunizationList()
        Me.Cursor = Cursors.WaitCursor

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As New DataTable()

        Dim dvIM As DataView


        Try

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("IM_ShowTransactionList", oParam, dtIM)
            C1PatientIM.ShowCellLabels = False
            C1PatientIM.AllowSorting = True
            C1PatientIM.AllowEditing = False



            dvIM = dtIM.DefaultView
            C1PatientIM.DataSource = dvIM
            C1PatientIM.Cols("TransactionDate").DataType = GetType(System.String)
            C1PatientIM.Cols("TransactionDate").Width = C1PatientIM.Width * 10 / 100
            'C1PatientIM.DataSource = dtIM

            oParam.Dispose()
            oParam = Nothing

            oDB.Disconnect()
            oDB = Nothing

        Catch ex As Exception
            If oParam IsNot Nothing Then
                oParam.Dispose()
                oParam = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If

            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click
        Me.Close()
    End Sub

    Private Sub C1PatientIM_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientIM.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1PatientIM.HitTest(ptPoint)

        Try
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                Dim TransactionID As Int64
                TransactionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1))
                Dim frm As New frmImTransaction(Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1)), _PatientID)
                frm.Text = "Modify Immunization"
                frm.ShowInTaskbar = False
                frm.StartPosition = FormStartPosition.CenterParent
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                ShowImmunizationList()
                FilterRecord()

                'To refresh list after performing add/update/delete operation
                Dim rowIndex As Int64
                rowIndex = C1PatientIM.FindRow(TransactionID, 1, 1, False, True, False)
                C1PatientIM.Select(rowIndex, 0, True)

                frm.Dispose()
                frm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Add.Click
        Try
            Dim frm As New frmImTransaction(0, _PatientID)
            frm.Text = "Add Immunization"
            frm.ShowInTaskbar = False
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            ShowImmunizationList()
            frm.Dispose()

            frm = Nothing
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetNKImmunizationVisibility()
        End Try
    End Sub

    Private Sub tblbtn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Modify.Click
        Try

            If C1PatientIM.Rows.Count > 1 Then
                If Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1)) > 0 Then

                    Dim TransactionID As Int64
                    TransactionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1))


                    Dim frm As New frmImTransaction(Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1)), _PatientID)
                    frm.Text = "Modify Immunization"
                    frm.ShowInTaskbar = False
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    ShowImmunizationList()


                    FilterRecord()

                    'To refresh list after performing add/update/delete operation
                    Dim rowIndex As Int64
                    rowIndex = C1PatientIM.FindRow(TransactionID, 1, 1, False, True, False)
                    C1PatientIM.Select(rowIndex, 0, True)


                    frm.Dispose()
                    frm = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Delete.Click
        Try
            If C1PatientIM.Rows.Count > 1 Then
                If Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1)) > 0 Then
                    If MessageBox.Show("Do you want to delete the selected Immunization record?   ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        'Added by Amit to check Track Vaccine Inventory setting is ON or OFF
                        Dim value As New Object()
                        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
                        ogloSettings.GetSetting("TrackVaccineInventory", 0, gnClinicID, value)

                        If value = "1" Then
                            'checking individual Vaccines Inventory Track setting ON/OFF, if ON then showing
                            'Adjust Inventory screen
                            If GetVaccinInventoryTrack() > 0 Then
                                Dim frm As New frmIM_AdjustInventory
                                'frm.SKU = IIf(IsDBNull(C1PatientIM.GetData(C1PatientIM.Row, 0)) = True, "", C1PatientIM.GetData(C1PatientIM.Row, 0))
                                frm.SKU = Convert.ToString(C1PatientIM.GetData(C1PatientIM.Row, 0))

                                frm.TradeName = Convert.ToString(C1PatientIM.GetData(C1PatientIM.Row, 6))
                                frm.Vaccine = C1PatientIM.GetData(C1PatientIM.Row, 7)
                                frm.CategoryID = C1PatientIM.GetData(C1PatientIM.Row, 18)
                                frm.Manufacturer = Convert.ToString(C1PatientIM.GetData(C1PatientIM.Row, 9))
                                frm.LotNumber = C1PatientIM.GetData(C1PatientIM.Row, 10)
                                frm.LocationID = C1PatientIM.GetData(C1PatientIM.Row, 17)
                                frm.DosageGiven = C1PatientIM.GetData(C1PatientIM.Row, 11)
                                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                frm.Dispose()
                                frm = Nothing
                            End If
                        End If

                        value = Nothing
                        ogloSettings.Dispose()
                        ogloSettings = Nothing

                        If blnCancelDelete = False Then

                            Dim clsIMTran As New clsgloIMTransaction
                            Dim nAuditTrailID As Int64 = 0
                            Dim _nHistoryId As Int64
                            clsIMTran.TranctionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1))
                            clsIMTran.PatientID = _PatientID
                            _nHistoryId = clsIMTran.AddIMHistory(nAuditTrailID, [Enum].GetName(GetType(gloAuditTrail.ActivityType), 3))


                            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                            Dim oParam As gloDatabaseLayer.DBParameters
                            oDB.Connect(False)

                            Dim biTransactionID As Int64 = 0
                            oParam = New gloDatabaseLayer.DBParameters
                            oParam.Add("@Patientid", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                            oParam.Add("@biMasterID", 0, ParameterDirection.Output, SqlDbType.BigInt)
                            oDB.Execute("GetIMMasterIDForPatient", oParam, biTransactionID)

                            oParam.Clear()
                            oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                            oParam.Add("@transaction_id", C1PatientIM.GetData(C1PatientIM.Row, 1), ParameterDirection.Input, SqlDbType.BigInt)
                            oDB.Execute("IM_DeleteTransactionLine", oParam)



                            nAuditTrailID = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Delete, "Immunization Record Deleted.", _PatientID, C1PatientIM.GetData(C1PatientIM.Row, 1), gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            clsIMTran.UpdateIMHistory(nAuditTrailID, _nHistoryId)


                            clsIMTran.AddMessageQueue(biTransactionID, _PatientID, clsIMTran.TranctionID, "D")

                            oParam.Dispose()
                            oParam = Nothing
                            oDB.Dispose()
                            oDB = Nothing

                            ShowImmunizationList()
                        End If

                        blnCancelDelete = False

                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetNKImmunizationVisibility()
        End Try
    End Sub

    Private Function GetVaccinInventoryTrack() As Long

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dt As New DataTable

        oDB.Connect(False)
        oParam = New gloDatabaseLayer.DBParameters
        oParam.Add("@im_nCategoryID", C1PatientIM.GetData(C1PatientIM.Row, 18), ParameterDirection.Input, SqlDbType.Decimal)
        oParam.Add("@im_sVaccine", C1PatientIM.GetData(C1PatientIM.Row, 7), ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_sTradeName", C1PatientIM.GetData(C1PatientIM.Row, 6), ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_LotNumber", C1PatientIM.GetData(C1PatientIM.Row, 10), ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_nLocationID", C1PatientIM.GetData(C1PatientIM.Row, 17), ParameterDirection.Input, SqlDbType.Decimal)

        oDB.Retrive("IM_GetVaccinInventoryTrack", oParam, dt)
        oDB.Disconnect()

        oParam.Dispose()
        oParam = Nothing

        oDB.Dispose()
        oDB = Nothing

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return 0
        End If

    End Function

    Private Sub tblbtn_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Refresh.Click
        ShowImmunizationList()
    End Sub

    Private Sub Set_PatientDetailStrip()
        'Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.Immunization)
            .BringToFront()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        pnlSearch.BringToFront()
        pnlMain.BringToFront()
        pnlTop.SendToBack()
    End Sub

    Private Sub tblbtn_Consent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Consent.Click
        Dim dtWord As DataTable = Nothing
        Dim objWord As New clsWordDocument
        Try
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            dtWord = New DataTable
            objWord = New clsWordDocument
            dtWord = objWord.FillTemplates(enumTemplateFlag.PatientConsent)
            objWord = Nothing
            If dtWord.Rows.Count = 0 Then
                ''''If not present then exit from sub
                MessageBox.Show("No template is associated for patient consent. Associate any template first", "Patient exam", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                Dim objfrmPatientConsent As New frmPatientConsent(_PatientID)

                With objfrmPatientConsent
                    .isExpandConsent = True
                    .MdiParent = Me.MdiParent
                    '.MinimizeBox = False
                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With
            End If
            dtWord = Nothing
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtWord) Then
                dtWord.Dispose()
                dtWord = Nothing
            End If
        End Try
    End Sub

    Private Sub tblbtn_PrintSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_PrintSummary.Click
        'Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        'Try
        '    If C1PatientIM.Rows.Count <= 1 Then
        '        MessageBox.Show("No Immunizations transaction available for this patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Exit Sub
        '    End If
        '    clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
        '    clsPrntRpt.PrintReport("rptPatientImmSummaryByTrade", "PatientID,User", _PatientID & "," & gstrLoginName, False, "")
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Immunization summary report printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        'Catch ex As Exception
        '    ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Error while printing immunization summary record.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        'Finally
        '    If Not IsNothing(clsPrntRpt) Then
        '        clsPrntRpt = Nothing
        '    End If
        'End Try
        Dim frm As frmIm_DueReport
        Try
            ''Added by Mayuri:20101110-to fix issue:#6080-multiple instance
            frm = New frmIm_DueReport(_PatientID, "rptPatientImmSummaryByTrade")
            frm.MdiParent = Me.ParentForm
            frm._age = gloUC_PatientStrip1.PatientAge.Age
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

        End Try
    End Sub

    Private Sub tblbtn_PrintDue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_PrintDue.Click
        'Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        'Try
        '    If checkDueImmunizationAvailable(_PatientID) = False Then
        '        MessageBox.Show("No immunizations due for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Exit Sub
        '    End If
        '    clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
        '    clsPrntRpt.PrintReport("RptPatientImmunizationDue", "PatientID,User", _PatientID & "," & gstrLoginName, False, "")
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization due report printed.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        'Catch ex As Exception
        '    'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization due record.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        'Finally
        '    If Not IsNothing(clsPrntRpt) Then
        '        clsPrntRpt = Nothing
        '    End If
        'End Try
        Dim frm As frmIm_DueReport
        Try
            ''Added by Mayuri:20101110-to fix issue:#6080-multiple instance
            frm = New frmIm_DueReport(_PatientID, "RptPatientImmunizationDue")
            frm.MdiParent = Me.ParentForm
            frm._age = gloUC_PatientStrip1.PatientAge.Age
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

        End Try
    End Sub

    Private Sub tblbtn_VaccineRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_VaccineRecord.Click
        'Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        'Try
        '    If C1PatientIM.Rows.Count <= 1 Then
        '        MessageBox.Show("No Immunization transaction available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Exit Sub
        '    End If

        '    clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
        ' clsPrntRpt.PrintReport("RptPatientImmunizationSummary", "PatientID,User", _PatientID & "," & gstrLoginName, False, "")
        'Catch ex As Exception
        '    'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    If Not IsNothing(clsPrntRpt) Then
        '        clsPrntRpt = Nothing
        '    End If
        'End Try
        Dim frm As frmIm_DueReport
        Try
            ''Added by Mayuri:20101110-to fix issue:#6080-multiple instance
            ' Dim _age As gloUserControlLibrary.AgeDetail
            frm = New frmIm_DueReport(_PatientID, "RptPatientImmunizationSummary")
            frm._PatDOB = gloUC_PatientStrip1.PatientDateOfBirth
            frm._age = gloUC_PatientStrip1.PatientAge.Age
            frm.MdiParent = Me.ParentForm
            frm.WindowState = FormWindowState.Maximized
            frm.Show()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

        End Try
    End Sub


    Private Sub tblbtn_GenCCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_GenCCD.Click
        Dim objfrm As frmCCDGenerateList = Nothing
        Try
            objfrm = New frmCCDGenerateList(_PatientID)
            objfrm.ChkImmunization.Checked = True

            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(objfrm) Then
                objfrm.Dispose()
                objfrm = Nothing
            End If
        End Try
    End Sub
    Private Function checkDueImmunizationAvailable(ByVal _patientID As Int64) As Boolean
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIMDUE As New DataTable()
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString())
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientID", _patientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("Rpt_PatientImmunizationDue", oParam, dtIMDUE)
            If dtIMDUE.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If dtIMDUE IsNot Nothing Then
                dtIMDUE.Dispose()
                dtIMDUE = Nothing
            End If
            If oParam IsNot Nothing Then
                oParam.Dispose()
                oParam = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        FilterRecord()
    End Sub

    Private Sub FilterRecord()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dv As DataView
            Dim dt As DataTable

            dv = CType(C1PatientIM.DataSource, DataView)

            If IsNothing(dv) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            C1PatientIM.DataSource = dv
            dt = dv.Table

            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If
            '" TransactionDate Like '%" & strPatientSearchDetails & "%' OR " & _
            dt.DefaultView.RowFilter = " Status Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Administered Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " TradeName Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Vaccine Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Category Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Manufacturer Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " LotNumber Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " DosageGiven Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " AmountGiven Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Unit Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Site Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Route Like '%" & strPatientSearchDetails & "%' OR " & _
                                          " Comments Like '%" & strPatientSearchDetails & "%' "

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub FillImmunization(ByVal sFilter As String)

        Me.Cursor = Cursors.WaitCursor

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As New DataTable()

        Try

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@Condition", sFilter, ParameterDirection.Input, SqlDbType.Text)
            oDB.Retrive("IM_ShowTransactionListSearch", oParam, dtIM)
            C1PatientIM.ShowCellLabels = False
            C1PatientIM.AllowSorting = True
            C1PatientIM.AllowEditing = False
            C1PatientIM.DataSource = dtIM
            oParam.Dispose()
            oParam = Nothing

            oDB.Disconnect()
            oDB = Nothing

        Catch ex As Exception
            If oParam IsNot Nothing Then
                oParam.Dispose()
                oParam = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If

            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.Clear()
    End Sub

    Private Sub tblbtn_ViewHistory_Click(se0nder As System.Object, e As System.EventArgs) Handles tblbtn_ViewHistory.Click
        Dim TransactionID As Int64 = 0
        If C1PatientIM.Row > 0 Then
            TransactionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1))
        End If

        Dim oForm As New frmIM_History(TransactionID, _PatientID)
        oForm.WindowState = FormWindowState.Normal
        oForm.StartPosition = FormStartPosition.CenterScreen
        oForm.ShowInTaskbar = False
        oForm.ShowDialog(IIf(IsNothing(oForm.Parent), Me, oForm.Parent))
        Me.Cursor = Cursors.Arrow
        oForm.Dispose()
        oForm = Nothing

    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Query, gloAuditTrail.ActivityType.Query, "Immunization Query """ + txtSearch.Text + """", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

    End Sub

    Private Sub tblbtn_HxForecast_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_HxForecastReq.Click
        If _PatientID <> 0 Then
            Dim oGeneralInterface As New clsGeneralInterface()
            oGeneralInterface.SendImmunization("Q11", 0, _PatientID)
        End If
    End Sub

    Private Sub tblbtn_ViewForecast_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_ViewForecast.Click
        Try
            If _PatientID <= 0 Then
                MessageBox.Show("Select the patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            Dim oForm As New frmVWImmunizationForecast(_PatientID)

            oForm.WindowState = FormWindowState.Normal
            oForm.StartPosition = FormStartPosition.CenterScreen
            oForm.ShowInTaskbar = False
            oForm.ShowDialog(IIf(IsNothing(oForm.Parent), Me, oForm.Parent))

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblReconcillation_Click(sender As System.Object, e As System.EventArgs) Handles tblReconcillation.Click
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dtPendingReconciliation As DataTable = Nothing
        Dim strSql As String = "SELECT * FROM Immunization_History_Reconcilation IMDTL JOIN Im_Trn_MST IMMST ON IMMST.im_trn_mst_id = IMDTL.im_trn_mst_id WHERE IMMST.im_trn_mst_nPatientId = " & _PatientID & " AND IMDTL.ReconcileStatus = 0"

        Try
            oDB.Connect(False)
            oDB.Retrive_Query(strSql, dtPendingReconciliation)
            oDB.Disconnect()

            If dtPendingReconciliation.Rows.Count > 0 Then
                ShowReconciliation()
            Else
                If _PatientID <> 0 Then
                    Dim oGeneralInterface As New clsGeneralInterface()
                    oGeneralInterface.SendImmunization("Q11", 0, _PatientID)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub ShowReconciliation()
        Dim ogloCCDReconcile As New gloCCDLibrary.gloCCDReconcilation
        Dim frmReconcilation As New frmHxForecastReconcileList
        Try

            frmReconcilation = New frmHxForecastReconcileList(_PatientID, "Immunization")
            frmReconcilation.LoginUser = gstrLoginName
            frmReconcilation.LoginID = gnLoginID


            frmReconcilation.ShowDialog(IIf(IsNothing(frmReconcilation.Parent), Me, frmReconcilation.Parent))

            ShowImmunizationList()

            Dim _isReadyLists As Boolean = False

            _isReadyLists = ogloCCDReconcile.IsReadyListsPresent(_PatientID, "Immunization")
            'If _isReadyLists = True Then
            '    tlbbtn_Reconcile.Enabled = True
            'Else
            '    tlbbtn_Reconcile.Enabled = False
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If IsNothing(frmReconcilation) = False Then
                frmReconcilation.Dispose()
                frmReconcilation = Nothing
            End If

            If IsNothing(ogloCCDReconcile) = False Then
                ogloCCDReconcile = Nothing
            End If
        End Try

    End Sub

    Private Sub tblbtn_V04IR_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_V04IR.Click
        Dim oGeneralInterface As New clsGeneralInterface()
        Try
            If C1PatientIM.Rows.Count > 1 Then
                If Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1)) > 0 Then
                    Dim TransactionID As Int64
                    TransactionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 1))
                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                    Dim oDBParameters As New gloDatabaseLayer.DBParameters()

                    oDB.Connect(False)
                    oDBParameters.Clear()
                    Dim biTransactionID As Int64 = 0
                    oDBParameters.Clear()
                    oDBParameters.Add("@Patientid", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDBParameters.Add("@biMasterID", 0, ParameterDirection.Output, SqlDbType.BigInt)
                    oDB.Execute("GetIMMasterIDForPatient", oDBParameters, biTransactionID)
                    If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolIMRegistryHL7Format Then
                        oGeneralInterface.SendImmunization("V04IR", biTransactionID, _PatientID, TransactionID, "A")
                        MessageBox.Show("Immunization Information is added in HL7 MessageQueue", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oGeneralInterface) Then
                oGeneralInterface.Dispose()
                oGeneralInterface = Nothing
            End If
        End Try
    End Sub
    Private Sub SetNKImmunizationVisibility()
        Try
            If (C1PatientIM.Rows.Count <= 1) Then
                tblNKImmunizations.Visible = True
            Else
                tblNKImmunizations.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub tblNKImmunizations_Click(sender As Object, e As System.EventArgs) Handles tblNKImmunizations.Click
        Dim PatspecCDAspc As gloCCDLibrary.frmPatspecCDAspc
        PatspecCDAspc = New gloCCDLibrary.frmPatspecCDAspc()
        PatspecCDAspc.OpenfrmImmunization = True
        PatspecCDAspc.patientID = _PatientID
        PatspecCDAspc.ShowDialog(Me)
        PatspecCDAspc.Dispose()
        PatspecCDAspc = Nothing

    End Sub

    Private Sub frmIMTransactionList_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        SetNKImmunizationVisibility()
    End Sub
End Class
