Imports gloUserControlLibrary
Imports gloEMR.gloEMRWord

Public Class frmIMTransactionList

    Dim _PatientID As Long
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip

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
        Dim oParam As gloDatabaseLayer.DBParameters
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
            C1PatientIM.Cols("TransactionDate").DataType = GetType(System.DateTime)

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
                TransactionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 0))
                Dim frm As New frmImTransaction(Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 0)), _PatientID)
                frm.Text = "Modify Immunization"
                frm.ShowInTaskbar = False
                frm.StartPosition = FormStartPosition.CenterParent
                frm.ShowDialog(Me)

                ShowImmunizationList()
                FilterRecord()

                'To refresh list after performing add/update/delete operation
                Dim rowIndex As Int64
                rowIndex = C1PatientIM.FindRow(TransactionID, 1, 0, False, True, False)
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
            frm.ShowDialog(Me)
            ShowImmunizationList()
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Modify.Click
        Try

            If C1PatientIM.Rows.Count > 1 Then
                If Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 0)) > 0 Then

                    Dim TransactionID As Int64
                    TransactionID = Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 0))

                    Dim frm As New frmImTransaction(Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 0)), _PatientID)
                    frm.Text = "Modify Immunization"
                    frm.ShowInTaskbar = False
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog(Me)
                    ShowImmunizationList()


                    FilterRecord()

                    'To refresh list after performing add/update/delete operation
                    Dim rowIndex As Int64
                    rowIndex = C1PatientIM.FindRow(TransactionID, 1, 0, False, True, False)
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
                If Convert.ToInt64(C1PatientIM.GetData(C1PatientIM.Row, 0)) > 0 Then
                    If MessageBox.Show("Are you sure, you want to delete the selected Immunization record?   ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                        Dim oParam As gloDatabaseLayer.DBParameters
                        oDB.Connect(False)
                        oParam = New gloDatabaseLayer.DBParameters
                        oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParam.Add("@transaction_id", C1PatientIM.GetData(C1PatientIM.Row, 0), ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.Execute("IM_DeleteTransactionLine", oParam)
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Delete, "Immunization Record Deleted.", _PatientID, C1PatientIM.GetData(C1PatientIM.Row, 0), gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        oParam.Dispose()
                        oParam = Nothing
                        oDB.Dispose()
                        oParam = Nothing
                        ShowImmunizationList()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtn_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Refresh.Click
        ShowImmunizationList()
    End Sub

    Private Sub Set_PatientDetailStrip()
        'Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip
        Me.Controls.Add(gloUC_PatientStrip1)
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.Immunization)
            .BringToFront()
        End With
        pnlSearch.BringToFront()
        pnlMain.BringToFront()
        pnlTop.SendToBack()
    End Sub

    Private Sub tblbtn_Consent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Consent.Click
        Dim dtWord As DataTable
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
            Dim _age As gloUserControlLibrary.AgeDetail
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
        Dim objfrm As frmCCDGenerateList
        Try
            objfrm = New frmCCDGenerateList(_PatientID)
            objfrm.ChkImmunization.Checked = True

            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
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
        Dim oDB As gloDatabaseLayer.DBLayer
        Dim oParam As gloDatabaseLayer.DBParameters
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
        Dim oParam As gloDatabaseLayer.DBParameters
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
End Class
