Imports System.IO
Imports C1.Win.C1FlexGrid

Public Class frmUB04PrinterSettings

    Dim _OutputFilePath As String
    Dim isSaveAndClose As Boolean = False
    Dim SelectedPrinter As String = "Default"
    Dim bIsInvalidData As Boolean = False
    Dim bIsCapitalized As Boolean = False

#Region " C1 Constants "
    Private Const COL_CAPTION = 0
    Private Const COL_BOXNAME = 1
    Private Const COL_X = 2
    Private Const COL_Y = 3
    Private Const COL_PANEL = 4
    Private Const COL_CharSize = 5
    Dim _IsSave As Boolean = True

#End Region

#Region " Form Load and close "

    Private Sub frmUB04UB04PrinterSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Try
            Me.Text = SelectedPrinter
            If (SelectedPrinter.IndexOf("'") > -1) Then
                SelectedPrinter = SelectedPrinter.Replace("'", "''")
            End If
            FillSettingGrid()
            ResetLabels()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub frmUB04PrinterSettings_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isSaveAndClose = False Then

            If CloseForm() = True Then
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region " Private Methods "

    Private Sub FillSettingGrid()
        Try

            Dim oUB04 As New clsUB04Setting()
            Dim dtSettings As DataTable
            dtSettings = oUB04.GetUB04PrinterSettings(SelectedPrinter)
            If dtSettings IsNot Nothing Then
                If dtSettings.Rows.Count > 0 Then
                    C1Settings.DataSource = dtSettings
                    DesignGrid()
                Else '' IF NO SETTINGS FOUND THEN LOAD DEFAULT SETTINGS ''
                    dtSettings = oUB04.GetDefaultUB04PrinterSettings()
                    If dtSettings IsNot Nothing Then
                        C1Settings.DataSource = dtSettings
                        DesignGrid()
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub ResetLabels()
        '  lblX.Text = 0
        lblX1.Text = 0
        lblX2.Text = 0
        lblX3.Text = 0
        lblX4.Text = 0
        lblX5.Text = 0
        lblX6.Text = 0
        lblX7.Text = 0
        lblX8.Text = 0
        lblX9.Text = 0
        lblX10.Text = 0
        lblX11.Text = 0
        lblX12.Text = 0
        lblx13.Text = 0
        lblx14.Text = 0

        ' lblY.Text = 0
        lblY1.Text = 0
        lblY2.Text = 0
        lblY3.Text = 0
        lblY4.Text = 0
        lblY5.Text = 0
        lblY6.Text = 0
        lblY7.Text = 0
        lblY8.Text = 0
        lblY9.Text = 0
        lblY10.Text = 0
        lblY11.Text = 0
        lblY12.Text = 0
        lbly13.Text = 0
        lbly14.Text = 0

    End Sub

    Private Sub ResetSettings()
        Try
            Dim oUB04 As New clsUB04Setting()
            Dim dtSettings As DataTable
            dtSettings = Nothing
            dtSettings = oUB04.GetDefaultUB04PrinterSpecificSettings(SelectedPrinter)
            If dtSettings IsNot Nothing AndAlso dtSettings.Rows.Count > 0 Then
                If dtSettings.Rows.Count > 0 Then
                    C1Settings.DataSource = dtSettings
                    DesignGrid()
                End If
            Else
                dtSettings = Nothing
                dtSettings = oUB04.GetDefaultUB04PrinterSettings()
                If dtSettings IsNot Nothing Then
                    If dtSettings.Rows.Count > 0 Then
                        C1Settings.DataSource = dtSettings
                        DesignGrid()
                    End If
                End If
            End If

            ResetLabels()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub DesignGrid()
        gloC1FlexStyle.Style(C1Settings)
        C1Settings.AllowEditing = True
        C1Settings.Cols(COL_CAPTION).AllowEditing = False
        C1Settings.Cols(COL_X).AllowEditing = True
        C1Settings.Cols(COL_Y).AllowEditing = True
        C1Settings.Cols(COL_CharSize).AllowEditing = True


        C1Settings.Cols(COL_CAPTION).Width = C1Settings.Width * 0.55
        C1Settings.Cols(COL_X).Width = C1Settings.Width * 0.1
        C1Settings.Cols(COL_Y).Width = C1Settings.Width * 0.1
        C1Settings.Cols(COL_CharSize).Width = C1Settings.Width * 0.2


        C1Settings.Cols(COL_CAPTION).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Settings.Cols(COL_X).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Settings.Cols(COL_Y).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1Settings.Cols(COL_CharSize).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        C1Settings.Cols(COL_BOXNAME).Visible = False
        C1Settings.Cols(COL_PANEL).Visible = False

    End Sub

    Private Function CloseForm() As Boolean

        Dim res As DialogResult = MessageBox.Show("Do you want to save changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        If res = DialogResult.Yes Then
            SaveUB04Settings()
            Return True
        End If
        If res = DialogResult.No Then


            Return True
        End If
        If res = DialogResult.Cancel Then
            Return False
        End If
    End Function


    Private Sub SaveUB04Settings()
        Try
            If C1Settings.Rows.Count > 1 Then
                Me.Cursor = Cursors.WaitCursor
                Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)

                Dim _Query As String = ""

                If SelectedPrinter = "" Then
                    _Query = "DELETE FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType =1"
                Else
                    _Query = "DELETE FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType =1 and sPrinterName ='" + SelectedPrinter + "'"
                End If
                oDB.Connect(False)
                oDB.Execute_Query(_Query)
                For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                    If SelectedPrinter = "" Then
                        _Query = " INSERT INTO BL_UB04PrintSettings(sBoxCaption, sBoxName, nXCoordinate, nYCoordinate, sPrinterName, nClinicID, nFormType, nPanel,nCharSize)" _
                           & "  VALUES('" + C1Settings.GetData(iRow, COL_CAPTION).ToString().Replace("'", "''") + "', '" + C1Settings.GetData(iRow, COL_BOXNAME).ToString() + "', " _
                           & " " + C1Settings.GetData(iRow, COL_X).ToString() + ", " + C1Settings.GetData(iRow, COL_Y).ToString() + ", " _
                           & " '', 1,1, " + C1Settings.GetData(iRow, COL_PANEL).ToString() + ", '" + C1Settings.GetData(iRow, COL_CharSize).ToString() + "')"
                    Else
                        _Query = " INSERT INTO BL_UB04PrintSettings(sBoxCaption, sBoxName, nXCoordinate, nYCoordinate, sPrinterName, nClinicID, nFormType, nPanel,nCharSize)" _
                             & "  VALUES('" + C1Settings.GetData(iRow, COL_CAPTION).ToString().Replace("'", "''") + "', '" + C1Settings.GetData(iRow, COL_BOXNAME).ToString() + "', " _
                             & " " + C1Settings.GetData(iRow, COL_X).ToString() + ", " + C1Settings.GetData(iRow, COL_Y).ToString() + ", " _
                             & " '" + SelectedPrinter + "', 1,1, " + C1Settings.GetData(iRow, COL_PANEL).ToString() + ", " + C1Settings.GetData(iRow, COL_CharSize).ToString() + ")"
                    End If
                    oDB.Execute_Query(_Query)
                Next
                _IsSave = True
                oDB.Disconnect()
                oDB.Dispose()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)

        Finally
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub SaveAsDefaultsUB04Settings()
        Try

            If C1Settings.Rows.Count > 1 Then
                Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)

                Dim _Query As String = ""

                If SelectedPrinter = "" Then
                    _Query = "DELETE FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType =0"
                Else
                    _Query = "DELETE FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType =0 and sPrinterName ='" + SelectedPrinter + "'"
                End If

                oDB.Connect(False)
                oDB.Execute_Query(_Query)

                For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                    If SelectedPrinter = "" Then
                        _Query = " INSERT INTO BL_UB04PrintSettings(sBoxCaption, sBoxName, nXCoordinate, nYCoordinate, sPrinterName, nClinicID, nFormType, nPanel,nCharSize)" _
                              & "  VALUES('" + C1Settings.GetData(iRow, COL_CAPTION).ToString().Replace("'", "''") + "', '" + C1Settings.GetData(iRow, COL_BOXNAME).ToString() + "', " _
                              & " " + C1Settings.GetData(iRow, COL_X).ToString() + ", " + C1Settings.GetData(iRow, COL_Y).ToString() + ", " _
                              & " '', 1, 0, " + C1Settings.GetData(iRow, COL_PANEL).ToString() + ", " + C1Settings.GetData(iRow, COL_CharSize).ToString() + ")"
                    Else
                        _Query = " INSERT INTO BL_UB04PrintSettings(sBoxCaption, sBoxName, nXCoordinate, nYCoordinate, sPrinterName, nClinicID, nFormType, nPanel,nCharSize)" _
                             & "  VALUES('" + C1Settings.GetData(iRow, COL_CAPTION).ToString().Replace("'", "''") + "', '" + C1Settings.GetData(iRow, COL_BOXNAME).ToString() + "', " _
                             & " " + C1Settings.GetData(iRow, COL_X).ToString() + ", " + C1Settings.GetData(iRow, COL_Y).ToString() + ", " _
                             & " '" + SelectedPrinter + "', 1,0, " + C1Settings.GetData(iRow, COL_PANEL).ToString() + ", " + C1Settings.GetData(iRow, COL_CharSize).ToString() + ")"
                    End If


                    oDB.Execute_Query(_Query)
                Next

                oDB.Disconnect()
                oDB.Dispose()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Function SetUB04PaperForm() As gloUB04PaperForm
        Dim oUB04 As New gloUB04PaperForm(SelectedPrinter)

        Try
            '        oHCFA1500.CF_Top_InsuranceHeader.Value = "HEADER"
            Dim clsPrintUB04PaperForm As New gloPrintUB04PaperForm()
            Dim _result As String = clsPrintUB04PaperForm.GetFontSetupSettingformCmsAndUB("Capatalize UB04 data")
            bIsCapitalized = Convert.ToBoolean(If(_result = "", False, Convert.ToBoolean(_result)))

            If IsNothing(clsPrintUB04PaperForm) Then
                clsPrintUB04PaperForm.Dispose()
                clsPrintUB04PaperForm = Nothing
            End If

            ''Provider's Details
            oUB04.UB04_1_ProviderName.Value = formatedData(oUB04.UB04_1_ProviderName, "Any Hospital")
            oUB04.UB04_1_ProviderAddress.Value = formatedData(oUB04.UB04_1_ProviderAddress, "123 Any street")
            oUB04.UB04_1a_ProviderCity.Value = formatedData(oUB04.UB04_1a_ProviderCity, "Philadelphia   PA  19103")
            oUB04.UB04_1b_ProviderState.Value = formatedData(oUB04.UB04_1b_ProviderState, "")
            oUB04.UB04_1c_ProviderZipCode.Value = formatedData(oUB04.UB04_1c_ProviderZipCode, "")
            oUB04.UB04_1a_ProviderPhone.Value = formatedData(oUB04.UB04_1a_ProviderPhone, "002123456")
            oUB04.UB04_1b_ProviderFaxNumber.Value = formatedData(oUB04.UB04_1b_ProviderFaxNumber, "")
            oUB04.UB04_1c_ProviderCountryCode.Value = formatedData(oUB04.UB04_1c_ProviderCountryCode, "")
            ' Payer() 's Details
            oUB04.UB04_2_PayToName.Value = formatedData(oUB04.UB04_2_PayToName, "Any Hospital")
            oUB04.UB04_2_PayToAddress.Value = formatedData(oUB04.UB04_2_PayToAddress, "123 Any street")
            oUB04.UB04_2a_Pay_toCity.Value = formatedData(oUB04.UB04_2a_Pay_toCity, "Philadelphia  PA  19103")
            oUB04.UB04_2b_Pay_toState.Value = formatedData(oUB04.UB04_2b_Pay_toState, "")
            oUB04.UB04_2a_Pay_toZip.Value = formatedData(oUB04.UB04_2a_Pay_toZip, "")
            oUB04.UB04_2b_ReservedFL02.Value = formatedData(oUB04.UB04_2b_ReservedFL02, "002123456")

            ' Patient Control Number
            oUB04.UB04_3a_PatientControlNumber.Value = formatedData(oUB04.UB04_3a_PatientControlNumber, "1234564810")
            oUB04.UB04_3b_MedicalHealthRecordNumber.Value = formatedData(oUB04.UB04_3b_MedicalHealthRecordNumber, "98765")
            'Type Of Bill
            oUB04.UB04_4_TypeofBill.Value = formatedData(oUB04.UB04_4_TypeofBill, "831")
            oUB04.UB04_4_TypeofBillFrequencyCode.Value = formatedData(oUB04.UB04_4_TypeofBillFrequencyCode, "")
            'Fedaral Tax Number
            oUB04.UB04_5_FederalTaxNumber_Upperline.Value = formatedData(oUB04.UB04_5_FederalTaxNumber_Upperline, "")
            oUB04.UB04_5_FederalTaxNumber_Lowerline.Value = formatedData(oUB04.UB04_5_FederalTaxNumber_Lowerline, "022-222-2002")
            'Statement Covers Period(From - Through)
            oUB04.UB04_6a_StatementCoversPeriod_From.Value = formatedData(oUB04.UB04_6a_StatementCoversPeriod_From, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_6b_StatementCoversPeriod_Through.Value = formatedData(oUB04.UB04_6b_StatementCoversPeriod_Through, System.DateTime.Now.ToString("MMddyy"))
            ' ''Reserved
            oUB04.UB04_7a_ReservedFL07A.Value = formatedData(oUB04.UB04_7a_ReservedFL07A, "")
            oUB04.UB04_7b_ReservedFL07B.Value = formatedData(oUB04.UB04_7b_ReservedFL07B, "")
            'Patient Identifier ,Patient Social Security Number
            oUB04.UB04_8_PatientIdentifier.Value = formatedData(oUB04.UB04_8_PatientIdentifier, "")
            oUB04.UB04_8a_PatientSocialSecurityNumber.Value = formatedData(oUB04.UB04_8a_PatientSocialSecurityNumber, "4567895652")
            oUB04.UB04_8b_PatientName.Value = formatedData(oUB04.UB04_8b_PatientName, "FirstName MiddleName LastName")
            ' ''Patient Address
            oUB04.UB04_9a_PatientStreetAddress.Value = formatedData(oUB04.UB04_9a_PatientStreetAddress, "Patient street Address")
            oUB04.UB04_9b_PatientCity.Value = formatedData(oUB04.UB04_9b_PatientCity, "Patient City")
            oUB04.UB04_9c_PatientState.Value = formatedData(oUB04.UB04_9c_PatientState, "ST")
            oUB04.UB04_9d_PatientZip.Value = formatedData(oUB04.UB04_9d_PatientZip, "190103")
            oUB04.UB04_9e_PatientCountryCode.Value = formatedData(oUB04.UB04_9e_PatientCountryCode, "")
            'Birth Date
            oUB04.UB04_10_PatientBirthDate.Value = formatedData(oUB04.UB04_10_PatientBirthDate, "09011984")
            'Gender Admission Date,Admission(Hour),Admission(Type),Admission(Source),Discharge(Hour),Discharge(Status)

            oUB04.UB04_11_PatientGender.Value = formatedData(oUB04.UB04_11_PatientGender, "M")
            oUB04.UB04_11_PatientMaritalStatus.Value = formatedData(oUB04.UB04_11_PatientMaritalStatus, "")
            oUB04.UB04_11_PatientRace.Value = formatedData(oUB04.UB04_11_PatientRace, "")
            oUB04.UB04_12_Admission_Visit_StartofCareDate.Value = formatedData(oUB04.UB04_12_Admission_Visit_StartofCareDate, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_13_Admission_Visit_Hour.Value = formatedData(oUB04.UB04_13_Admission_Visit_Hour, "01")
            oUB04.UB04_14_AdmissionType.Value = formatedData(oUB04.UB04_14_AdmissionType, "3")
            oUB04.UB04_15_ReferralSource.Value = formatedData(oUB04.UB04_15_ReferralSource, "2")
            oUB04.UB04_16_DischargeHour.Value = formatedData(oUB04.UB04_16_DischargeHour, "")
            oUB04.UB04_17_DischargeStatus.Value = formatedData(oUB04.UB04_17_DischargeStatus, "01")
            'Condition Code
            oUB04.UB04_18_ConditionCodes.Value = formatedData(oUB04.UB04_18_ConditionCodes, "C1")
            oUB04.UB04_19_ConditionCodes.Value = formatedData(oUB04.UB04_19_ConditionCodes, "C2")
            oUB04.UB04_20_ConditionCodes.Value = formatedData(oUB04.UB04_20_ConditionCodes, "C3")
            oUB04.UB04_21_ConditionCodes.Value = formatedData(oUB04.UB04_21_ConditionCodes, "C4")
            oUB04.UB04_22_ConditionCodes.Value = formatedData(oUB04.UB04_22_ConditionCodes, "C5")
            oUB04.UB04_23_ConditionCodes.Value = formatedData(oUB04.UB04_23_ConditionCodes, "C6")
            oUB04.UB04_24_ConditionCodes.Value = formatedData(oUB04.UB04_24_ConditionCodes, "C7")
            oUB04.UB04_25_ConditionCodes.Value = formatedData(oUB04.UB04_25_ConditionCodes, "C8")
            oUB04.UB04_26_ConditionCodes.Value = formatedData(oUB04.UB04_26_ConditionCodes, "C9")
            oUB04.UB04_27_ConditionCodes.Value = formatedData(oUB04.UB04_27_ConditionCodes, "C10")
            oUB04.UB04_28_ConditionCodes.Value = formatedData(oUB04.UB04_28_ConditionCodes, "C11")
            'Accident State
            oUB04.UB04_29_AccidentState.Value = formatedData(oUB04.UB04_29_AccidentState, "AS")
            'Reserved
            oUB04.UB04_30_ReservedFL30A.Value = formatedData(oUB04.UB04_30_ReservedFL30A, "")
            oUB04.UB04_30_ReservedFL30B.Value = formatedData(oUB04.UB04_30_ReservedFL30B, "")
            'Occurrence Code Details
            oUB04.UB04_31a_OccurrenceCode.Value = formatedData(oUB04.UB04_31a_OccurrenceCode, "O1")
            oUB04.UB04_31a_OccurrenceDate.Value = formatedData(oUB04.UB04_31a_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_31b_OccurrenceDate.Value = formatedData(oUB04.UB04_31b_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_31b_OccurrenceCode.Value = formatedData(oUB04.UB04_31b_OccurrenceCode, "O1")



            oUB04.UB04_32a_OccurrenceCode.Value = formatedData(oUB04.UB04_32a_OccurrenceCode, "O2")
            oUB04.UB04_32a_OccurrenceDate.Value = formatedData(oUB04.UB04_32a_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_32b_OccurrenceCode.Value = formatedData(oUB04.UB04_32b_OccurrenceCode, "O3")
            oUB04.UB04_32b_OccurrenceDate.Value = formatedData(oUB04.UB04_32b_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))

            oUB04.UB04_33a_OccurrenceCode.Value = formatedData(oUB04.UB04_33a_OccurrenceCode, "O4")
            oUB04.UB04_33a_OccurrenceDate.Value = formatedData(oUB04.UB04_33a_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_33b_OccurrenceCode.Value = formatedData(oUB04.UB04_33b_OccurrenceCode, "O1")
            oUB04.UB04_33b_OccurrenceDate.Value = formatedData(oUB04.UB04_33b_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))

            oUB04.UB04_34a_OccurrenceCode.Value = formatedData(oUB04.UB04_34a_OccurrenceCode, "O5")
            oUB04.UB04_34a_OccurrenceDate.Value = formatedData(oUB04.UB04_34a_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_34b_OccurrenceCode.Value = formatedData(oUB04.UB04_34b_OccurrenceCode, "O6")
            oUB04.UB04_34b_OccurrenceDate.Value = formatedData(oUB04.UB04_34b_OccurrenceDate, System.DateTime.Now.ToString("MMddyy"))

            oUB04.UB04_35a_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_35a_OccurrenceSpanCode, "S1")
            oUB04.UB04_35a_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_35a_OccurrenceSpanDateFrom, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_35a_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_35a_OccurrenceSpanDateThrough, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_35b_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_35b_OccurrenceSpanCode, "S1")
            oUB04.UB04_35b_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_35b_OccurrenceSpanDateFrom, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_35b_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_35b_OccurrenceSpanDateThrough, System.DateTime.Now.ToString("MMddyy"))

            oUB04.UB04_36a_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_36a_OccurrenceSpanCode, "S1")
            oUB04.UB04_36a_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_36a_OccurrenceSpanDateFrom, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_36a_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_36a_OccurrenceSpanDateThrough, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_36b_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_36b_OccurrenceSpanCode, "S1")
            oUB04.UB04_36b_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_36b_OccurrenceSpanDateFrom, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_36b_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_36b_OccurrenceSpanDateThrough, System.DateTime.Now.ToString("MMddyy"))
            'Future Use
            oUB04.UB04_37_ReservedFL37.Value = formatedData(oUB04.UB04_37_ReservedFL37, "")
            'Responsible Party Name/Address
            oUB04.UB04_38_ResponsiblePartyNameAddress.Value = formatedData(oUB04.UB04_38_ResponsiblePartyNameAddress, "Responsible Party" + Environment.NewLine + "Any Street" + Environment.NewLine + "City    PA   190103")
            'Value Code,Value Code Amount

            oUB04.UB04_39a_ValueCode.Value = formatedData(oUB04.UB04_39a_ValueCode, "1")
            oUB04.UB04_39a_ValueCodeAmount = New FormFieldString(oUB04.UB04_39a_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_39a_ValueCodeAmount.Location.Y))
            oUB04.UB04_39a_ValueCodeAmount.Value = formatedData(oUB04.UB04_39a_ValueCodeAmount, "100")
            oUB04.UB04_39a_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39a_ValueCodeAmount_Cents, "00")

            oUB04.UB04_39b_ValueCode.Value = formatedData(oUB04.UB04_39b_ValueCode, "1")
            oUB04.UB04_39b_ValueCodeAmount = New FormFieldString(oUB04.UB04_39b_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_39b_ValueCodeAmount.Location.Y))
            oUB04.UB04_39b_ValueCodeAmount.Value = formatedData(oUB04.UB04_39b_ValueCodeAmount, "100")
            oUB04.UB04_39b_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39b_ValueCodeAmount_Cents, "00")

            oUB04.UB04_39c_ValueCode.Value = formatedData(oUB04.UB04_39c_ValueCode, "1")
            oUB04.UB04_39c_ValueCodeAmount = New FormFieldString(oUB04.UB04_39c_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_39c_ValueCodeAmount.Location.Y))
            oUB04.UB04_39c_ValueCodeAmount.Value = formatedData(oUB04.UB04_39c_ValueCodeAmount, "100")
            oUB04.UB04_39c_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39c_ValueCodeAmount_Cents, "00")

            oUB04.UB04_39d_ValueCode.Value = formatedData(oUB04.UB04_39d_ValueCode, "1")
            oUB04.UB04_39d_ValueCodeAmount = New FormFieldString(oUB04.UB04_39d_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_39d_ValueCodeAmount.Location.Y))
            oUB04.UB04_39d_ValueCodeAmount.Value = formatedData(oUB04.UB04_39d_ValueCodeAmount, "100")
            oUB04.UB04_39d_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39d_ValueCodeAmount_Cents, "00")

            oUB04.UB04_40a_ValueCode.Value = formatedData(oUB04.UB04_40a_ValueCode, "1")
            oUB04.UB04_40a_ValueCodeAmount = New FormFieldString(oUB04.UB04_40a_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_40a_ValueCodeAmount.Location.Y))
            oUB04.UB04_40a_ValueCodeAmount.Value = formatedData(oUB04.UB04_40a_ValueCodeAmount, "100")
            oUB04.UB04_40a_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40a_ValueCodeAmount_Cents, "00")

            oUB04.UB04_40b_ValueCode.Value = formatedData(oUB04.UB04_40b_ValueCode, "1")
            oUB04.UB04_40b_ValueCodeAmount = New FormFieldString(oUB04.UB04_40b_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_40b_ValueCodeAmount.Location.Y))
            oUB04.UB04_40b_ValueCodeAmount.Value = formatedData(oUB04.UB04_40b_ValueCodeAmount, "100")
            oUB04.UB04_40b_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40b_ValueCodeAmount_Cents, "00")

            oUB04.UB04_40c_ValueCode.Value = formatedData(oUB04.UB04_40c_ValueCode, "1")
            oUB04.UB04_40c_ValueCodeAmount = New FormFieldString(oUB04.UB04_40c_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_40c_ValueCodeAmount.Location.Y))
            oUB04.UB04_40c_ValueCodeAmount.Value = formatedData(oUB04.UB04_40c_ValueCodeAmount, "100")
            oUB04.UB04_40c_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40c_ValueCodeAmount_Cents, "00")

            oUB04.UB04_40d_ValueCode.Value = formatedData(oUB04.UB04_40d_ValueCode, "1")
            oUB04.UB04_40d_ValueCodeAmount = New FormFieldString(oUB04.UB04_40d_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_40d_ValueCodeAmount.Location.Y))
            oUB04.UB04_40d_ValueCodeAmount.Value = formatedData(oUB04.UB04_40d_ValueCodeAmount, "100")
            oUB04.UB04_40d_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40d_ValueCodeAmount_Cents, "00")

            oUB04.UB04_41a_ValueCode.Value = formatedData(oUB04.UB04_41a_ValueCode, "1")
            oUB04.UB04_41a_ValueCodeAmount = New FormFieldString(oUB04.UB04_41a_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_41a_ValueCodeAmount.Location.Y))
            oUB04.UB04_41a_ValueCodeAmount.Value = formatedData(oUB04.UB04_41a_ValueCodeAmount, "100")
            oUB04.UB04_41a_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41a_ValueCodeAmount_Cents, "00")

            oUB04.UB04_41b_ValueCode.Value = formatedData(oUB04.UB04_41b_ValueCode, "1")
            oUB04.UB04_41b_ValueCodeAmount = New FormFieldString(oUB04.UB04_41b_ValueCodeAmount.Location.X - ("100".ToString().Length * 16) + 20, (oUB04.UB04_41b_ValueCodeAmount.Location.Y))
            oUB04.UB04_41b_ValueCodeAmount.Value = formatedData(oUB04.UB04_41b_ValueCodeAmount, "100")
            oUB04.UB04_41b_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41b_ValueCodeAmount, "00")

            oUB04.UB04_41c_ValueCode.Value = formatedData(oUB04.UB04_41c_ValueCode, "1")
            oUB04.UB04_41c_ValueCodeAmount = New FormFieldString(oUB04.UB04_41c_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_41c_ValueCodeAmount.Location.Y))
            oUB04.UB04_41c_ValueCodeAmount.Value = formatedData(oUB04.UB04_41c_ValueCodeAmount, "100")
            oUB04.UB04_41c_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41c_ValueCodeAmount, "00")

            oUB04.UB04_41d_ValueCode.Value = formatedData(oUB04.UB04_41d_ValueCode, "1")
            oUB04.UB04_41d_ValueCodeAmount = New FormFieldString(oUB04.UB04_41d_ValueCodeAmount.Location.X - ("100".Length * 16) + 20, (oUB04.UB04_41d_ValueCodeAmount.Location.Y))
            oUB04.UB04_41d_ValueCodeAmount.Value = formatedData(oUB04.UB04_41d_ValueCodeAmount, "100")
            oUB04.UB04_41d_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41d_ValueCodeAmount_Cents, "00")



            'Revenue(Code)
            'For index As Integer = 0 To 1
            '    Dim oServiceLine As New ServiceLine()
            '    oServiceLine.UB04_42_RevenueCode.Value = "0310"
            '    oServiceLine.UB04_42_RevenueCode.Location = oUB04.UB04_42_RevenueCode.Location
            '    oServiceLine.UB04_43_RevenueCodeDescription.Value = "ANESTHESIA EYE LENS SURGERY"
            '    oServiceLine.UB04_43_RevenueCodeDescription.Location = oUB04.UB04_43_RevenueCodeDescription.Location
            '    oServiceLine.UB04_44_RateCodes.Value = "99231"
            '    oServiceLine.UB04_44_RateCodes.Location = oUB04.UB04_44_RateCodes.Location
            '    oServiceLine.UB04_45_ServiceDate_visit.Value = "0901110"
            '    oServiceLine.UB04_45_ServiceDate_visit.Location = oUB04.UB04_45_ServiceDate_visit.Location
            '    oServiceLine.UB04_46_ServiceUnits.Value = "1"
            '    oServiceLine.UB04_46_ServiceUnits.Location = oUB04.UB04_46_ServiceUnits.Location
            '    oServiceLine.UB04_47a_TotalCharges_Dollars.Value = "100"
            '    oServiceLine.UB04_47a_TotalCharges_Dollars.Location = oUB04.UB04_47a_TotalCharges_Dollars.Location
            '    oServiceLine.UB04_47b_TotalCharges_Cents.Value = "00"
            '    oServiceLine.UB04_47b_TotalCharges_Cents.Location = oUB04.UB04_47b_TotalCharges_Cents.Location
            '    oServiceLine.UB04_48a_Non_coveredCharges_Dollars.Value = ""
            '    oServiceLine.UB04_48a_Non_coveredCharges_Dollars.Location = oUB04.UB04_48a_Non_coveredCharges_Dollars.Location
            '    oServiceLine.UB04_48b_Non_coveredCharges_Cents.Value = ""
            '    oServiceLine.UB04_48b_Non_coveredCharges_Cents.Location = oUB04.UB04_48b_Non_coveredCharges_Cents.Location
            '    oServiceLine.UB04_49_ReservedFL49.Value = ""
            '    oServiceLine.UB04_49_ReservedFL49.Location = oUB04.UB04_49_ReservedFL49.Location
            '    oUB04.UB04_ServiceLines.Add(oServiceLine)
            'Next
            Dim total As Int16 = 0
            For index As Integer = 0 To 5
                Dim oServiceLine As New ServiceLine()

                oServiceLine.UB04_42_RevenueCode = New UB04FormFieldString(oUB04.UB04_42_RevenueCode.Location.X, (oUB04.UB04_42_RevenueCode.Location.Y + (index * 50)))
                oServiceLine.UB04_42_RevenueCode.Value = formatedData(oUB04.UB04_42_RevenueCode, "031" + (index + 1).ToString())
                oServiceLine.UB04_43_RevenueCodeDescription = New UB04FormFieldString(oUB04.UB04_43_RevenueCodeDescription.Location.X, (oUB04.UB04_43_RevenueCodeDescription.Location.Y + (index * 50)))
                oServiceLine.UB04_43_RevenueCodeDescription.Value = formatedData(oUB04.UB04_43_RevenueCodeDescription, "Anesthesia Eye Lens Surgery" + (index + 1).ToString())
                oServiceLine.UB04_44_RateCodes = New UB04FormFieldString(oUB04.UB04_44_RateCodes.Location.X, (oUB04.UB04_44_RateCodes.Location.Y + (index * 50)))
                oServiceLine.UB04_44_RateCodes.Value = formatedData(oUB04.UB04_44_RateCodes, "9923" + (index + 1).ToString())
                oServiceLine.UB04_45_ServiceDate_visit = New UB04FormFieldString(oUB04.UB04_45_ServiceDate_visit.Location.X, (oUB04.UB04_45_ServiceDate_visit.Location.Y + (index * 50)))
                oServiceLine.UB04_45_ServiceDate_visit.Value = formatedData(oUB04.UB04_45_ServiceDate_visit, System.DateTime.Now.ToString("MMddyy"))
                oServiceLine.UB04_46_ServiceUnits = New UB04FormFieldString(oUB04.UB04_46_ServiceUnits.Location.X, (oUB04.UB04_46_ServiceUnits.Location.Y + (index * 50)))
                oServiceLine.UB04_46_ServiceUnits.Value = formatedData(oUB04.UB04_46_ServiceUnits, "1")
                oServiceLine.UB04_47a_TotalCharges_Dollars = New UB04FormFieldString(oUB04.UB04_47a_TotalCharges_Dollars.Location.X - 30, (oUB04.UB04_47a_TotalCharges_Dollars.Location.Y + (index * 50)))
                oServiceLine.UB04_47a_TotalCharges_Dollars.Value = formatedData(oUB04.UB04_47a_TotalCharges_Dollars, "100")
                total += Convert.ToInt16(oServiceLine.UB04_47a_TotalCharges_Dollars.Value)
                oServiceLine.UB04_47b_TotalCharges_Cents = New UB04FormFieldString(oUB04.UB04_47b_TotalCharges_Cents.Location.X, (oUB04.UB04_47b_TotalCharges_Cents.Location.Y + (index * 50)))
                oServiceLine.UB04_47b_TotalCharges_Cents.Value = formatedData(oUB04.UB04_47b_TotalCharges_Cents, "00")
                oServiceLine.UB04_48a_Non_coveredCharges_Dollars = New UB04FormFieldString(oUB04.UB04_48a_Non_coveredCharges_Dollars.Location.X, (oUB04.UB04_48a_Non_coveredCharges_Dollars.Location.Y + (index * 50)))
                oServiceLine.UB04_48a_Non_coveredCharges_Dollars.Value = formatedData(oUB04.UB04_48a_Non_coveredCharges_Dollars, "")
                oServiceLine.UB04_48b_Non_coveredCharges_Cents = New UB04FormFieldString(oUB04.UB04_48b_Non_coveredCharges_Cents.Location.X, (oUB04.UB04_48b_Non_coveredCharges_Cents.Location.Y + (index * 50)))
                oServiceLine.UB04_48b_Non_coveredCharges_Cents.Value = formatedData(oUB04.UB04_48b_Non_coveredCharges_Cents, "")
                oServiceLine.UB04_49_ReservedFL49 = New UB04FormFieldString(oUB04.UB04_49_ReservedFL49.Location.X, (oUB04.UB04_49_ReservedFL49.Location.Y + (index * 50)))
                oServiceLine.UB04_49_ReservedFL49.Value = formatedData(oUB04.UB04_49_ReservedFL49, "")
                oUB04.UB04_ServiceLines.Add(oServiceLine)
            Next
            oUB04.UB04_42L23_RevenueCode.Value = formatedData(oUB04.UB04_42L23_RevenueCode, "001")
            oUB04.UB04_47L23_SummaryTotalCharges_Dollars = New UB04FormFieldString(oUB04.UB04_47L23_SummaryTotalCharges_Dollars.Location.X - 50, oUB04.UB04_47L23_SummaryTotalCharges_Dollars.Location.Y)
            oUB04.UB04_47L23_SummaryTotalCharges_Dollars.Value = formatedData(oUB04.UB04_47L23_SummaryTotalCharges_Dollars, total.ToString())
            oUB04.UB04_47L23_SummaryTotalCharges_Cents.Value = formatedData(oUB04.UB04_47L23_SummaryTotalCharges_Cents, "00")
            oUB04.UB04_48L23_SummaryNon_coveredCharges_Dollars.Value = formatedData(oUB04.UB04_48L23_SummaryNon_coveredCharges_Dollars, "")
            oUB04.UB04_48L23_SummaryNon_coveredCharges_Cents.Value = formatedData(oUB04.UB04_48L23_SummaryNon_coveredCharges_Cents, "")
            oUB04.UB04_49L23_Reserved49L23.Value = formatedData(oUB04.UB04_49L23_Reserved49L23, "")
            oUB04.UB04_43L23_CurrentPage.Value = formatedData(oUB04.UB04_43L23_CurrentPage, "1")
            oUB04.UB04_44L23_TotalPages.Value = formatedData(oUB04.UB04_44L23_TotalPages, "1")
            oUB04.UB04_44L23CreationDate.Value = formatedData(oUB04.UB04_44L23CreationDate, "")
            'Primary,Secondary,Tertiary Payer Name
            oUB04.UB04_50_PayerName_Primary.Value = formatedData(oUB04.UB04_50_PayerName_Primary, "PayerName_Primary")
            oUB04.UB04_50_PayerName_Secondary.Value = formatedData(oUB04.UB04_50_PayerName_Secondary, "PayerName_Secondary")
            oUB04.UB04_50_PayerName_Tertiary.Value = formatedData(oUB04.UB04_50_PayerName_Tertiary, "PayerName_Tertiary")
            'Helth Plan ID A,B,C,Release of information.,Assignment of benefits.

            oUB04.UB04_51_HealthPlanIDA.Value = formatedData(oUB04.UB04_51_HealthPlanIDA, "")
            oUB04.UB04_51_HealthPlanIDB.Value = formatedData(oUB04.UB04_51_HealthPlanIDB, "")
            oUB04.UB04_51_HealthPlanIDC.Value = formatedData(oUB04.UB04_51_HealthPlanIDC, "")
            oUB04.UB04_52_InformationRelease_Primary.Value = formatedData(oUB04.UB04_52_InformationRelease_Primary, "Y")
            oUB04.UB04_52_InformationRelease_Secondary.Value = formatedData(oUB04.UB04_52_InformationRelease_Secondary, "Y")
            oUB04.UB04_52_InformationRelease_Tertiary.Value = formatedData(oUB04.UB04_52_InformationRelease_Tertiary, "N")
            oUB04.UB04_53_BenefitsAssignment_Primary.Value = formatedData(oUB04.UB04_53_BenefitsAssignment_Primary, "Y")
            oUB04.UB04_53_BenefitsAssignment_Secondary.Value = formatedData(oUB04.UB04_53_BenefitsAssignment_Secondary, "Y")
            oUB04.UB04_53_BenefitsAssignment_Tertiary.Value = formatedData(oUB04.UB04_53_BenefitsAssignment_Tertiary, "N")
            'Prior Payment
            oUB04.UB04_54_PriorPaymentsDollars_Primary.Value = formatedData(oUB04.UB04_54_PriorPaymentsDollars_Primary, "90")
            oUB04.UB04_54_PriorPaymentsCents_Primary.Value = formatedData(oUB04.UB04_54_PriorPaymentsCents_Primary, "00")
            oUB04.UB04_54_PriorPaymentsDollars_Secondary.Value = formatedData(oUB04.UB04_54_PriorPaymentsDollars_Secondary, "80")
            oUB04.UB04_54_PriorPaymentsCents_Secondary.Value = formatedData(oUB04.UB04_54_PriorPaymentsCents_Secondary, "00")
            oUB04.UB04_54_PriorPaymentsDollars_Tertiary.Value = formatedData(oUB04.UB04_54_PriorPaymentsDollars_Tertiary, "70")
            oUB04.UB04_54_PriorPaymentsCents_Tertiary.Value = formatedData(oUB04.UB04_54_PriorPaymentsCents_Tertiary, "00")
            'Estimated amount due.
            oUB04.UB04_55a_EstimatedAmountDueDollars_Primary.Value = formatedData(oUB04.UB04_55a_EstimatedAmountDueDollars_Primary, "60")
            oUB04.UB04_55a_EstimatedAmountDueCents_Primary.Value = formatedData(oUB04.UB04_55a_EstimatedAmountDueCents_Primary, "00")
            oUB04.UB04_55b_EstimatedAmountDueDollars_Secondary.Value = formatedData(oUB04.UB04_55b_EstimatedAmountDueDollars_Secondary, "50")
            oUB04.UB04_55b_EstimatedAmountDueCents_Secondary.Value = formatedData(oUB04.UB04_55b_EstimatedAmountDueCents_Secondary, "00")
            oUB04.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value = formatedData(oUB04.UB04_55c_EstimatedAmountDueDollars_Tertiary, "40")
            oUB04.UB04_55c_EstimatedAmountDueCents_Tertiary.Value = formatedData(oUB04.UB04_55c_EstimatedAmountDueCents_Tertiary, "00")
            'NPI Number
            oUB04.UB04_56_NationalProviderIdentifier_NPI_.Value = formatedData(oUB04.UB04_56_NationalProviderIdentifier_NPI_, "0123456789")
            oUB04.UB04_57_OtherProvider_Primary.Value = formatedData(oUB04.UB04_57_OtherProvider_Primary, "")
            oUB04.UB04_57_OtherProvider_Secondary.Value = formatedData(oUB04.UB04_57_OtherProvider_Secondary, "")
            oUB04.UB04_57_OtherProvider_Tertiary.Value = formatedData(oUB04.UB04_57_OtherProvider_Tertiary, "")
            'Othere Provider ID,Insured Name Primary,Secondary ,Tertiary,Patient Relationship,Insured Unique ID ,Group Name,Group Number

            oUB04.UB04_58_InsuredName_Primary.Value = formatedData(oUB04.UB04_58_InsuredName_Primary, "InsuredName_Primary ")
            oUB04.UB04_58_InsuredName_Secondary.Value = formatedData(oUB04.UB04_58_InsuredName_Secondary, "InsuredName_Secondary")
            oUB04.UB04_58_InsuredName_Tertiary.Value = formatedData(oUB04.UB04_58_InsuredName_Tertiary, "InsuredName_Tertiary")
            oUB04.UB04_59_PatientRelationshipToInsured_Primary.Value = formatedData(oUB04.UB04_59_PatientRelationshipToInsured_Primary, "V1")
            oUB04.UB04_59_PatientRelationshipToInsured_Secondary.Value = formatedData(oUB04.UB04_59_PatientRelationshipToInsured_Secondary, "V2")
            oUB04.UB04_59_PatientRelationshipToInsured_Tertiary.Value = formatedData(oUB04.UB04_59_PatientRelationshipToInsured_Tertiary, "V3")
            oUB04.UB04_60_InsuredUniqueID_Primary.Value = formatedData(oUB04.UB04_60_InsuredUniqueID_Primary, "Insured Uniq ID")
            oUB04.UB04_60_InsuredUniqueID_Secondary.Value = formatedData(oUB04.UB04_60_InsuredUniqueID_Secondary, "Insured Uniq ID")
            oUB04.UB04_60_InsuredUniqueID_Tertiary.Value = formatedData(oUB04.UB04_60_InsuredUniqueID_Tertiary, "Insured Uniq ID")
            oUB04.UB04_61_InsuredGroupName_Primary.Value = formatedData(oUB04.UB04_61_InsuredGroupName_Primary, "")
            oUB04.UB04_61_InsuredGroupName_Secondary.Value = formatedData(oUB04.UB04_61_InsuredGroupName_Secondary, "")
            oUB04.UB04_61_InsuredGroupName_Tertiary.Value = formatedData(oUB04.UB04_61_InsuredGroupName_Tertiary, "")
            oUB04.UB04_62_InsuredGroupNumber_Primary.Value = formatedData(oUB04.UB04_62_InsuredGroupNumber_Primary, "456789123")
            oUB04.UB04_62_InsuredGroupNumber_Secondary.Value = formatedData(oUB04.UB04_62_InsuredGroupNumber_Secondary, "455555563")
            oUB04.UB04_62_InsuredGroupNumber_Tertiary.Value = formatedData(oUB04.UB04_62_InsuredGroupNumber_Tertiary, "456235789")
            'Treatment Authorization Code Primary,Secondary,Tertiary,Document Control Number,Employer Name

            oUB04.UB04_63_TreatmentAuthorizationCode_Primary.Value = formatedData(oUB04.UB04_63_TreatmentAuthorizationCode_Primary, "Authorization Code1")
            oUB04.UB04_63_TreatmentAuthorizationCode_Secondary.Value = formatedData(oUB04.UB04_63_TreatmentAuthorizationCode_Secondary, "Authorization Code2")
            oUB04.UB04_63_TreatmentAuthorizationCode_Tertiary.Value = formatedData(oUB04.UB04_63_TreatmentAuthorizationCode_Tertiary, "Authorization Code3")
            oUB04.UB04_64_DocumentControlNumber_A.Value = formatedData(oUB04.UB04_64_DocumentControlNumber_A, "Document Control Number A")
            oUB04.UB04_64_DocumentControlNumber_B.Value = formatedData(oUB04.UB04_64_DocumentControlNumber_B, "Document Control Number B")
            oUB04.UB04_64_DocumentControlNumber_C.Value = formatedData(oUB04.UB04_64_DocumentControlNumber_C, "Document Control Number C")
            oUB04.UB04_65_EmployerName_Primary.Value = formatedData(oUB04.UB04_65_EmployerName_Primary, "Employer Name Primary")
            oUB04.UB04_65_EmployerName_Secondary.Value = formatedData(oUB04.UB04_65_EmployerName_Secondary, "Employer Name Secondary")
            oUB04.UB04_65_EmployerName_Tertiary.Value = formatedData(oUB04.UB04_65_EmployerName_Tertiary, "Employer Name Tertiary")
            'ICD Version Indicator,Principal Diagnosis Code,Reserved,Admitting diagnosis.,Patient Visit Reason,PPS(Code),External Cause of Injury Code,Procedure(Code)

            oUB04.UB04_66_ICDVersionIndicator.Value = formatedData(oUB04.UB04_66_ICDVersionIndicator, "")
            oUB04.UB04_67_PrincipalDiagnosisCode.Value = formatedData(oUB04.UB04_67_PrincipalDiagnosisCode, "DX")
            oUB04.UB04_67a_OtherDiagnosis_A.Value = formatedData(oUB04.UB04_67a_OtherDiagnosis_A, "DX1")
            oUB04.UB04_67b_OtherDiagnosis_B.Value = formatedData(oUB04.UB04_67b_OtherDiagnosis_B, "DX2")
            oUB04.UB04_67c_OtherDiagnosis_C.Value = formatedData(oUB04.UB04_67c_OtherDiagnosis_C, "DX3")
            oUB04.UB04_67d_OtherDiagnosis_D.Value = formatedData(oUB04.UB04_67d_OtherDiagnosis_D, "DX4")
            oUB04.UB04_67e_OtherDiagnosis_E.Value = formatedData(oUB04.UB04_67e_OtherDiagnosis_E, "")
            oUB04.UB04_67f_OtherDiagnosis_F.Value = formatedData(oUB04.UB04_67f_OtherDiagnosis_F, "")
            oUB04.UB04_67g_OtherDiagnosis_G.Value = formatedData(oUB04.UB04_67g_OtherDiagnosis_G, "")
            oUB04.UB04_67h_OtherDiagnosis_H.Value = formatedData(oUB04.UB04_67h_OtherDiagnosis_H, "")
            oUB04.UB04_67i_OtherDiagnosis_I.Value = formatedData(oUB04.UB04_67i_OtherDiagnosis_I, "")
            oUB04.UB04_67j_OtherDiagnosis_J.Value = formatedData(oUB04.UB04_67j_OtherDiagnosis_J, "")
            oUB04.UB04_67k_OtherDiagnosis_K.Value = formatedData(oUB04.UB04_67k_OtherDiagnosis_K, "")
            oUB04.UB04_67l_OtherDiagnosis_L.Value = formatedData(oUB04.UB04_67l_OtherDiagnosis_L, "")
            oUB04.UB04_67m_OtherDiagnosis_M.Value = formatedData(oUB04.UB04_67m_OtherDiagnosis_M, "")
            oUB04.UB04_67n_OtherDiagnosis_N.Value = formatedData(oUB04.UB04_67n_OtherDiagnosis_N, "")
            oUB04.UB04_67o_OtherDiagnosis_O.Value = formatedData(oUB04.UB04_67o_OtherDiagnosis_O, "")
            oUB04.UB04_67p_OtherDiagnosis_P.Value = formatedData(oUB04.UB04_67p_OtherDiagnosis_P, "")
            oUB04.UB04_67q_OtherDiagnosis_Q.Value = formatedData(oUB04.UB04_67q_OtherDiagnosis_Q, "")
            oUB04.UB04_68_Reserved_68A.Value = formatedData(oUB04.UB04_68_Reserved_68A, "")
            oUB04.UB04_68_Reserved_68B.Value = formatedData(oUB04.UB04_68_Reserved_68B, "")
            oUB04.UB04_69_AdmittingDiagnosisCode.Value = formatedData(oUB04.UB04_69_AdmittingDiagnosisCode, "DX")
            oUB04.UB04_70a_PatientVisitReason_A.Value = formatedData(oUB04.UB04_70a_PatientVisitReason_A, "")
            oUB04.UB04_70b_PatientVisitReason_B.Value = formatedData(oUB04.UB04_70b_PatientVisitReason_B, "")
            oUB04.UB04_70c_PatientVisitReason_C.Value = formatedData(oUB04.UB04_70c_PatientVisitReason_C, "")
            oUB04.UB04_71_PPSCode.Value = formatedData(oUB04.UB04_71_PPSCode, "")
            oUB04.UB04_72a_ExternalCauseofInjuryCode_A.Value = formatedData(oUB04.UB04_72a_ExternalCauseofInjuryCode_A, "")
            oUB04.UB04_72b_ExternalCauseofInjuryCode_B.Value = formatedData(oUB04.UB04_72b_ExternalCauseofInjuryCode_B, "")
            oUB04.UB04_72c_ExternalCauseofInjuryCode_C.Value = formatedData(oUB04.UB04_72c_ExternalCauseofInjuryCode_C, "")
            oUB04.UB04_73_ReservedFL73.Value = formatedData(oUB04.UB04_73_ReservedFL73, "")
            oUB04.UB04_74_ProcedureCode_Principal.Value = formatedData(oUB04.UB04_74_ProcedureCode_Principal, "ICD-9")
            oUB04.UB04_74_ProcedureDate_Principal.Value = formatedData(oUB04.UB04_74_ProcedureDate_Principal, System.DateTime.Now.ToString("MMddyy"))
            oUB04.UB04_74a_ProcedureCode_OtherA.Value = formatedData(oUB04.UB04_74a_ProcedureCode_OtherA, "")
            oUB04.UB04_74a_ProcedureDate_OtherA.Value = formatedData(oUB04.UB04_74a_ProcedureDate_OtherA, "")
            oUB04.UB04_74b_ProcedureCode_OtherB.Value = formatedData(oUB04.UB04_74b_ProcedureCode_OtherB, "")
            oUB04.UB04_74b_ProcedureDate_OtherB.Value = formatedData(oUB04.UB04_74b_ProcedureDate_OtherB, "")
            oUB04.UB04_74c_ProcedureCode_OtherC.Value = formatedData(oUB04.UB04_74c_ProcedureCode_OtherC, "")
            oUB04.UB04_74c_ProcedureDate_OtherC.Value = formatedData(oUB04.UB04_74c_ProcedureDate_OtherC, "")
            oUB04.UB04_74d_ProcedureCode_OtherD.Value = formatedData(oUB04.UB04_74d_ProcedureCode_OtherD, "")
            oUB04.UB04_74d_ProcedureDate_OtherD.Value = formatedData(oUB04.UB04_74d_ProcedureDate_OtherD, "")
            oUB04.UB04_74e_ProcedureCode_OtherE.Value = formatedData(oUB04.UB04_74e_ProcedureCode_OtherE, "")
            oUB04.UB04_74e_ProcedureDate_OtherE.Value = formatedData(oUB04.UB04_74e_ProcedureDate_OtherE, "")
            oUB04.UB04_75a_ReservedFL75A.Value = formatedData(oUB04.UB04_75a_ReservedFL75A, "")
            oUB04.UB04_75b_ReservedFL75B.Value = formatedData(oUB04.UB04_75b_ReservedFL75B, "")
            oUB04.UB04_75c_ReservedFL75C.Value = formatedData(oUB04.UB04_75c_ReservedFL75C, "")
            oUB04.UB04_75d_ReservedFL75D.Value = formatedData(oUB04.UB04_75d_ReservedFL75D, "")
            ''Attending provider and identifiers,Operating provider and identifiers,Other provider name and identifiers.,other provider name and identifiers.

            oUB04.UB04_76_AttendingNPI.Value = formatedData(oUB04.UB04_76_AttendingNPI, "0123456789")
            oUB04.UB04_76_AttendingQUAL.Value = formatedData(oUB04.UB04_76_AttendingQUAL, "00")
            oUB04.UB04_76_AttendingID.Value = formatedData(oUB04.UB04_76_AttendingID, "123456789")
            oUB04.UB04_76a_AttendingLast.Value = formatedData(oUB04.UB04_76a_AttendingLast, "LAST NAME")
            oUB04.UB04_76b_AttendingFirst.Value = formatedData(oUB04.UB04_76b_AttendingFirst, "FIRST NAME")
            oUB04.UB04_77_OperatingNPI.Value = formatedData(oUB04.UB04_77_OperatingNPI, "")
            oUB04.UB04_77_OperatingQUAL.Value = formatedData(oUB04.UB04_77_OperatingQUAL, "")
            oUB04.UB04_77_OperatingID.Value = formatedData(oUB04.UB04_77_OperatingID, "")
            oUB04.UB04_77a_OperatingLast.Value = formatedData(oUB04.UB04_77a_OperatingLast, "")
            oUB04.UB04_77b_OperatingFirst.Value = formatedData(oUB04.UB04_77b_OperatingFirst, "")
            oUB04.UB04_77a_OperatingLast.Value = formatedData(oUB04.UB04_77a_OperatingLast, "")

            oUB04.UB04_78_OtherFirst.Value = formatedData(oUB04.UB04_78_OtherFirst, "FIRST NAME")
            oUB04.UB04_78_OtherLast.Value = formatedData(oUB04.UB04_78_OtherLast, "LAST NAME")
            oUB04.UB04_78_OtherNPI.Value = formatedData(oUB04.UB04_78_OtherNPI, "0123456789")
            oUB04.UB04_78_OtherProvider_QUAL.Value = formatedData(oUB04.UB04_78_OtherProvider_QUAL, "82")
            oUB04.UB04_78_OtherQUAL.Value = formatedData(oUB04.UB04_78_OtherQUAL, "G2")
            oUB04.UB04_78_OtherID.Value = formatedData(oUB04.UB04_78_OtherID, "123456789")


            oUB04.UB04_79_OtherNPI.Value = formatedData(oUB04.UB04_79_OtherNPI, "0123456789")
            oUB04.UB04_79_OtherProvider_QUAL.Value = formatedData(oUB04.UB04_79_OtherProvider_QUAL, "DN")
            oUB04.UB04_79_OtherID.Value = formatedData(oUB04.UB04_79_OtherID, "123456789")
            oUB04.UB04_79_OtherLast.Value = formatedData(oUB04.UB04_79_OtherLast, "LAST NAME")
            oUB04.UB04_79_OtherFirst.Value = formatedData(oUB04.UB04_79_OtherFirst, "FIRST NAME")
            oUB04.UB04_79_OtherQUAL.Value = formatedData(oUB04.UB04_79_OtherQUAL, "G2")

            oUB04.PayerCodeA_Primary.Value = formatedData(oUB04.PayerCodeA_Primary, "")
            oUB04.PayerCodeB_Secondary.Value = formatedData(oUB04.PayerCodeB_Secondary, "")
            oUB04.PayerCodeC_Tertiary.Value = formatedData(oUB04.PayerCodeC_Tertiary, "")
            ''Remark
            oUB04.UB04_80a_Remarks_1.Value = formatedData(oUB04.UB04_80a_Remarks_1, "Remark1")
            oUB04.UB04_80b_Remarks_2.Value = formatedData(oUB04.UB04_80b_Remarks_2, "Remark2")
            oUB04.UB04_80c_Remarks_3.Value = formatedData(oUB04.UB04_80c_Remarks_3, "Remark3")
            oUB04.UB04_80d_Remarks_4.Value = formatedData(oUB04.UB04_80d_Remarks_4, "")
            oUB04.UB04_81a_Code_Code_QUAL_A.Value = formatedData(oUB04.UB04_81a_Code_Code_QUAL_A, "")
            oUB04.UB04_81a_Code_Code_CODE_A.Value = formatedData(oUB04.UB04_81a_Code_Code_CODE_A, "")
            oUB04.UB04_81a_Code_Code_VALUE_A.Value = formatedData(oUB04.UB04_81a_Code_Code_VALUE_A, "")
            oUB04.UB04_81b_Code_Code_QUAL_B.Value = formatedData(oUB04.UB04_81b_Code_Code_QUAL_B, "")
            oUB04.UB04_81b_Code_Code_CODE_B.Value = formatedData(oUB04.UB04_81b_Code_Code_CODE_B, "")
            oUB04.UB04_81b_Code_Code_VALUE_B.Value = formatedData(oUB04.UB04_81b_Code_Code_VALUE_B, "")
            oUB04.UB04_81c_Code_Code_QUAL_C.Value = formatedData(oUB04.UB04_81c_Code_Code_QUAL_C, "")
            oUB04.UB04_81c_Code_Code_CODE_C.Value = formatedData(oUB04.UB04_81c_Code_Code_CODE_C, "")
            oUB04.UB04_81c_Code_Code_VALUE_C.Value = formatedData(oUB04.UB04_81c_Code_Code_VALUE_C, "")
            oUB04.UB04_81d_Code_Code_QUAL_D.Value = formatedData(oUB04.UB04_81d_Code_Code_QUAL_D, "")
            oUB04.UB04_81d_Code_Code_CODE_D.Value = formatedData(oUB04.UB04_81d_Code_Code_CODE_D, "")
            oUB04.UB04_81d_Code_Code_VALUE_D.Value = formatedData(oUB04.UB04_81d_Code_Code_VALUE_D, "")


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            oUB04 = Nothing
        Finally
        End Try
        Return oUB04
    End Function

    Private Sub printdoc_HCFA1500_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printdoc_HCFA1500.PrintPage

        Dim _width As Integer = e.PageSettings.Bounds.Width - 30
        Dim _height As Integer = e.PageSettings.Bounds.Height
        Dim _X As Integer = e.PageSettings.Bounds.X
        Dim _Y As Integer = e.PageSettings.Bounds.Y
        Dim _ub04Image As System.Drawing.Image = System.Drawing.Image.FromFile(_OutputFilePath)
        e.Graphics.DrawImage(_ub04Image, _X, _Y, _width, _height)
        If _ub04Image IsNot Nothing Then
            _ub04Image.Dispose()
            _ub04Image = Nothing
        End If


    End Sub

    Private Sub ExportSettings()
        Try
            Dim oDialog As New SaveFileDialog()
            oDialog.DefaultExt = ".csv"
            oDialog.Filter = "CSV File|*.csv"
            If oDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim _FileName As String = oDialog.FileName

                Dim oFile As New FileStream(_FileName, FileMode.Create)
                Dim oWriter As New StreamWriter(oFile)
                Dim _Temp As String
                Dim _Range As C1.Win.C1FlexGrid.CellRange

                For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                    _Range = C1Settings.GetCellRange(iRow, COL_CAPTION, iRow, COL_CharSize)
                    _Temp = _Range.Clip.Trim.Replace("	", ",")
                    oWriter.WriteLine(_Temp)
                Next

                oWriter.Close()
                oFile.Close()
            End If
        Catch ex As IOException
            MessageBox.Show("File cannot be access", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ImportSettings()
        Try

            Dim oDialog As New OpenFileDialog()
            oDialog.Filter = "CSV File|*.csv"
            If oDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim _FilePath As String = oDialog.FileName

                If File.Exists(_FilePath) Then
                    Dim oFile As New FileStream(_FilePath, FileMode.Open)
                    Dim oReader As New StreamReader(oFile)
                    Dim _Temp As String
                    Dim _SplitString() As String

                    '' CREATE DATA TABLE ''
                    Dim dtSettings As New DataTable
                    Dim oRow As DataRow
                    dtSettings.Columns.Add("Field Name", Type.GetType("System.String"))
                    dtSettings.Columns.Add("Box Name", Type.GetType("System.String"))
                    dtSettings.Columns.Add("X", Type.GetType("System.Int64"))
                    dtSettings.Columns.Add("Y", Type.GetType("System.Int64"))
                    dtSettings.Columns.Add("nPanel")
                    dtSettings.Columns.Add("CharSize")
                    While oReader.EndOfStream = False
                        _Temp = oReader.ReadLine()
                        If _Temp.Split(",").Length = 5 Then
                            _Temp = _Temp + ",30" ''Added 30 for CharSize column of DB.
                        End If
                        _SplitString = _Temp.Split(",")
                        If _SplitString.Length = 6 Then
                            oRow = dtSettings.NewRow
                            oRow("Field Name") = _SplitString(0)
                            oRow("Box Name") = _SplitString(1)
                            oRow("X") = _SplitString(2)
                            oRow("Y") = _SplitString(3)
                            oRow("nPanel") = _SplitString(4)
                            oRow("CharSize") = _SplitString(5)
                            dtSettings.Rows.Add(oRow)
                        Else
                            MessageBox.Show("Invalid file format.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            oReader.Close()
                            oFile.Close()
                            Exit Sub
                        End If
                    End While

                    oReader.Close()
                    oFile.Close()

                    C1Settings.DataSource = dtSettings
                    DesignGrid()
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function IsCoordinateValidate() As Boolean
        For iRow As Integer = 1 To C1Settings.Rows.Count - 1
            If (Convert.ToString(C1Settings.GetData(iRow, COL_X)).Trim <> "" And Convert.ToString(C1Settings.GetData(iRow, COL_Y)).Trim <> "") Then
                If (C1Settings.GetData(iRow, COL_X) > Int16.MaxValue Or C1Settings.GetData(iRow, COL_Y) > Int16.MaxValue Or C1Settings.GetData(iRow, COL_PANEL) > Int16.MaxValue) Then
                    MessageBox.Show("Please enter valid data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If (C1Settings.GetData(iRow, COL_X) > Int16.MaxValue) Then

                        C1Settings.Select(iRow, COL_X)
                        Return False
                    Else
                        C1Settings.Select(iRow, COL_Y)
                        Return False
                    End If
                End If

            Else
                MessageBox.Show("Co-ordinate Cannot be blank", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If (Convert.ToString(C1Settings.GetData(iRow, COL_X)).Trim = "") Then
                    C1Settings.Select(iRow, COL_X)
                    Return False
                Else
                    C1Settings.Select(iRow, COL_Y)
                    Return (False)

                End If
            End If

            If (Convert.ToString(C1Settings.GetData(iRow, COL_CharSize)).Trim <> "") Then
                If (C1Settings.GetData(iRow, COL_CharSize) > Int16.MaxValue) Then
                    MessageBox.Show("Please enter valid data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If (C1Settings.GetData(iRow, COL_CharSize) > Int16.MaxValue) Then

                        C1Settings.Select(iRow, COL_CharSize)
                        Return False
                    End If
                End If

            Else
                MessageBox.Show("Char Size Cannot be blank", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If (Convert.ToString(C1Settings.GetData(iRow, COL_CharSize)).Trim = "") Then
                    C1Settings.Select(iRow, COL_CharSize)
                    Return False
                End If
            End If
        Next
        Return True
    End Function


#End Region

#Region " Toolstrip Buttons "
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RowChange()
        If bIsInvalidData Then
            Exit Sub
        End If
        If (IsCoordinateValidate()) Then
            isSaveAndClose = True
            SaveUB04Settings()
            Me.Close()

        End If


    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If MessageBox.Show("Are you sure you want to reset co-ordinates?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            ResetSettings()
        End If
    End Sub

    Private Sub tlBtnTestPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlBtnTestPrint.Click
        Try
            RowChange()
            If bIsInvalidData Then
                Exit Sub
            End If
            If (IsCoordinateValidate()) Then
                SaveUB04Settings()
                'Dim _InputFilePath As String = Application.StartupPath & "\UB041500_BLANK.TIF"
                'Dim oHCFA1500PaperForm As New gloHCFA1500PaperForm()
                Dim oUB04PaperForm As New gloUB04PaperForm(SelectedPrinter)
                Dim oPrintForm As New gloPrintUB04PaperForm()
                'If System.IO.File.Exists(_InputFilePath) Then

                oUB04PaperForm = SetUB04PaperForm()
                If oUB04PaperForm IsNot Nothing Then
                    _OutputFilePath = oPrintForm.PrintUB04Form(oUB04PaperForm)
                End If
                If System.IO.File.Exists(_OutputFilePath) Then

                    Dim isPrinted As Boolean = False
                    If gloGlobal.gloTSPrint.isCopyPrint Then
                        Dim sPrinterName As String = "Default"
                        If printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName <> "" Then
                            sPrinterName = printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName
                        End If
                        Dim oConversion As New clsPrintDocumentConversion(mdlGeneral.GetConnectionString, sPrinterName, "UB")
                        Dim convertedFile As [String] = printdoc_UB04_Conversion(oConversion, True)

                        If Not (gloGlobal.gloTSPrint.UseEMFForClaims) Then
                            Dim tempPDFFilePath As [String] = gloSettings.FolderSettings.AppTempFolderPath + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf"
                            convertedFile = clsClinicalChartPrinting.ConvertTiffToPDF(convertedFile, tempPDFFilePath, True, True, False)
                        End If

                        isPrinted = gloClinicalQueueFunctions.CopyPrintDoc(convertedFile, "UB04", "PrintData")
                        If isPrinted Then
                            _OutputFilePath = convertedFile
                        End If
                    End If
                    If Not isPrinted Then
                        Dim oprintcontroller As New System.Drawing.Printing.StandardPrintController()
                        Dim opapers As System.Drawing.Printing.PrinterSettings.PaperSizeCollection
                        opapers = printdoc_HCFA1500.PrinterSettings.PaperSizes


                        For i As Integer = 0 To opapers.Count - 1

                            If opapers(i).PaperName.ToString().ToUpper() = "LETTER" Then
                                printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PaperSize = opapers(i)
                                Exit For
                            End If

                        Next

                        printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.Landscape = False
                        printdoc_HCFA1500.PrintController = oprintcontroller
                        printdoc_HCFA1500.Print()
                    End If

                End If

                'Else

                'MessageBox.Show("Source UB041500 file not present", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                'End If

                If oUB04PaperForm Is Nothing Then
                    oUB04PaperForm.Dispose()
                End If

                If oPrintForm Is Nothing Then
                    oPrintForm.Dispose()
                End If
            End If
            ''Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub tlbImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbImport.Click
        Try
            ImportSettings()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub tlbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbExport.Click
        Try
            RowChange()
            If bIsInvalidData Then
                Exit Sub
            End If
            If (IsCoordinateValidate()) Then
                ExportSettings()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub tlbSetAsDefaults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbSetAsDefaults.Click
        Try
            RowChange()
            If bIsInvalidData Then
                Exit Sub
            End If
            If (IsCoordinateValidate()) Then
                SaveAsDefaultsUB04Settings()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub


#End Region

#Region " Button Events "
    Private Sub btnRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnRight1.Click, btnRight2.Click, btnRight3.Click, btnRight4.Click, btnRight5.Click, btnRight6.Click, _
         btnRight7.Click, btnRight8.Click, btnRight9.Click, btnRight10.Click, btnRight11.Click, btnRight12.Click, btnRight13.Click, btnRight14.Click

        Try
            Cursor = Cursors.WaitCursor
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_X)) + 2
                    C1Settings.SetData(iRow, COL_X, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblX1.Text = Val(lblX1.Text) + 1
                Case 2
                    lblX2.Text = Val(lblX2.Text) + 1
                Case 3
                    lblX3.Text = Val(lblX3.Text) + 1
                Case 4
                    lblX4.Text = Val(lblX4.Text) + 1
                Case 5
                    lblX5.Text = Val(lblX5.Text) + 1
                Case 6
                    lblX6.Text = Val(lblX6.Text) + 1
                Case 7
                    lblX7.Text = Val(lblX7.Text) + 1
                Case 8
                    lblX8.Text = Val(lblX8.Text) + 1
                Case 9
                    lblX9.Text = Val(lblX9.Text) + 1
                Case 10
                    lblX10.Text = Val(lblX10.Text) + 1
                Case 11
                    lblX11.Text = Val(lblX11.Text) + 1
                Case 12
                    lblX12.Text = Val(lblX12.Text) + 1
                Case 13
                    lblx13.Text = Val(lblx13.Text) + 1
                Case 14
                    lblx14.Text = Val(lblx14.Text) + 1
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnLeft1.Click, btnLeft2.Click, btnLeft3.Click, btnLeft4.Click, btnLeft5.Click, btnLeft6.Click, _
        btnLeft7.Click, btnLeft8.Click, btnLeft9.Click, btnLeft10.Click, btnLeft11.Click, btnLeft12.Click, btnLeft13.Click, btnLeft14.Click

        Try
            Cursor = Cursors.WaitCursor
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_X)) - 2
                    C1Settings.SetData(iRow, COL_X, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblX1.Text = Val(lblX1.Text) - 1
                Case 2
                    lblX2.Text = Val(lblX2.Text) - 1
                Case 3
                    lblX3.Text = Val(lblX3.Text) - 1
                Case 4
                    lblX4.Text = Val(lblX4.Text) - 1
                Case 5
                    lblX5.Text = Val(lblX5.Text) - 1
                Case 6
                    lblX6.Text = Val(lblX6.Text) - 1
                Case 7
                    lblX7.Text = Val(lblX7.Text) - 1
                Case 8
                    lblX8.Text = Val(lblX8.Text) - 1
                Case 9
                    lblX9.Text = Val(lblX9.Text) - 1
                Case 10
                    lblX10.Text = Val(lblX10.Text) - 1
                Case 11
                    lblX11.Text = Val(lblX11.Text) - 1
                Case 12
                    lblX12.Text = Val(lblX12.Text) - 1
                Case 13
                    lblx13.Text = Val(lblx13.Text) - 1
                Case 14

                    lblx14.Text = Val(lblx14.Text) - 1
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnUp1.Click, btnUp2.Click, btnUp3.Click, btnUp4.Click, btnUp5.Click, btnUp6.Click, _
         btnUp7.Click, btnUp8.Click, btnUp9.Click, btnUp10.Click, btnUp11.Click, btnUp12.Click, btnUp13.Click, btnUp14.Click
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_Y)) - 2
                    C1Settings.SetData(iRow, COL_Y, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblY1.Text = Val(lblY1.Text) + 1
                Case 2
                    lblY2.Text = Val(lblY2.Text) + 1
                Case 3
                    lblY3.Text = Val(lblY3.Text) + 1
                Case 4
                    lblY4.Text = Val(lblY4.Text) + 1
                Case 5
                    lblY5.Text = Val(lblY5.Text) + 1
                Case 6
                    lblY6.Text = Val(lblY6.Text) + 1
                Case 7
                    lblY7.Text = Val(lblY7.Text) + 1
                Case 8
                    lblY8.Text = Val(lblY8.Text) + 1
                Case 9
                    lblY9.Text = Val(lblY9.Text) + 1
                Case 10
                    lblY10.Text = Val(lblY10.Text) + 1
                Case 11
                    lblY11.Text = Val(lblY11.Text) + 1
                Case 12
                    lblY12.Text = Val(lblY12.Text) + 1
                Case 13
                    lbly13.Text = Val(lbly13.Text) + 1
                Case 14
                    lbly14.Text = Val(lbly14.Text) + 1


            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnDown1.Click, btnDown2.Click, btnDown3.Click, btnDown4.Click, btnDown5.Click, btnDown6.Click, _
         btnDown7.Click, btnDown8.Click, btnDown9.Click, btnDown10.Click, btnDown11.Click, btnDown12.Click, btnDown13.Click, btnDown14.Click
        Try
            Cursor = Cursors.WaitCursor
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0
            Dim _Panel As Integer = CType(sender, Button).Tag

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                If Val(C1Settings.GetData(iRow, COL_PANEL)) = _Panel Then
                    _Temp = Val(C1Settings.GetData(iRow, COL_Y)) + 2
                    C1Settings.SetData(iRow, COL_Y, _Temp)
                End If
            Next

            Select Case _Panel
                Case 1
                    lblY1.Text = Val(lblY1.Text) - 1
                Case 2
                    lblY2.Text = Val(lblY2.Text) - 1
                Case 3
                    lblY3.Text = Val(lblY3.Text) - 1
                Case 4
                    lblY4.Text = Val(lblY4.Text) - 1
                Case 5
                    lblY5.Text = Val(lblY5.Text) - 1
                Case 6
                    lblY6.Text = Val(lblY6.Text) - 1
                Case 7
                    lblY7.Text = Val(lblY7.Text) - 1
                Case 8
                    lblY8.Text = Val(lblY8.Text) - 1
                Case 9
                    lblY9.Text = Val(lblY9.Text) - 1
                Case 10
                    lblY10.Text = Val(lblY10.Text) - 1
                Case 11
                    lblY11.Text = Val(lblY11.Text) - 1
                Case 12
                    lblY12.Text = Val(lblY12.Text) - 1
                Case 13
                    lbly13.Text = Val(lbly13.Text) - 1
                Case 14
                    lbly14.Text = Val(lbly14.Text) - 1

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeDown_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_Y)) + 2
                C1Settings.SetData(iRow, COL_Y, _Temp)
            Next

            'lblY.Text = Val(lblY.Text) - 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_Y)) - 2
                C1Settings.SetData(iRow, COL_Y, _Temp)
            Next

            'lblY.Text = Val(lblY.Text) + 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeRight_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_X)) + 2
                C1Settings.SetData(iRow, COL_X, _Temp)
            Next

            'lblX.Text = Val(lblX.Text) + 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub btnWholeLeft_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Try
            If C1Settings.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim _Temp As Integer = 0

            For iRow As Integer = 1 To C1Settings.Rows.Count - 1
                _Temp = Val(C1Settings.GetData(iRow, COL_X)) - 2
                C1Settings.SetData(iRow, COL_X, _Temp)
            Next

            'lblX.Text = Val(lblX.Text) - 1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Cursor = Cursors.Default
    End Sub
#End Region

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    'Private Sub C1Settings_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Settings.AfterEdit
    '    Dim x As String
    '    If (Convert.ToString(C1Settings.GetData(e.Row, e.Col)) = "") Then
    '        MessageBox.Show("Co-ordinate Cannot be blank", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        If (Convert.ToString(C1Settings.GetData(e.Row, COL_X)).Trim = "") Then
    '            'C1Settings.Select(e.Row, COL_X)
    '            e.Cancel = True
    '        Else
    '            'C1Settings.Select(e.Row, COL_Y)
    '            e.Cancel = True
    '        End If
    '    Else
    '        If (C1Settings.GetData(e.Row, COL_X) > Int16.MaxValue Or C1Settings.GetData(e.Row, COL_Y) > Int16.MaxValue Or C1Settings.GetData(e.Row, COL_PANEL) > Int16.MaxValue) Then
    '            MessageBox.Show("Invalid Data", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            If (C1Settings.GetData(e.Row, COL_X) > Int16.MaxValue) Then
    '                'C1Settings.Select(e.Row, COL_X)
    '                e.Cancel = True
    '            Else
    '                'C1Settings.Select(e.Row, COL_Y)
    '                e.Cancel = True
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub C1Settings_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1Settings.MouseMove
        gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltipDx, CType(sender, C1FlexGrid), e.Location)
    End Sub

   
    Private Sub printdoc_HCFA1500_EndPrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles printdoc_HCFA1500.EndPrint
        If File.Exists(_OutputFilePath) = True Then
            File.Delete(_OutputFilePath)
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal _SelectedPrinter As String)
        InitializeComponent()
        SelectedPrinter = _SelectedPrinter
    End Sub

    Dim toCreateEMF As Boolean = gloGlobal.gloTSPrint.UseEMFForClaims
    Dim _Bounds As Rectangle
    Private _DpiX As Single = 300.0F
    Private _DpiY As Single = 300.0F
    Private Function printdoc_UB04_Conversion(oConversion As clsPrintDocumentConversion, Optional isForPrint As [Boolean] = False) As String
        Dim PaperWidth As Single = oConversion.PaperWidth
        Dim PaperHeight As Single = oConversion.PaperHeight
        _DpiX = oConversion.DpiX
        _DpiY = oConversion.DpiY
        If Not isForPrint Then
            toCreateEMF = False
        End If
        _Bounds = New Rectangle(oConversion.BoundX, oConversion.BoundY, oConversion.BoundWidth, oConversion.BoundHeight)
        '_MarginBounds = New Rectangle(oConversion.MarginBoundsX, oConversion.MarginBoundsY, oConversion.MarginBoundsWidth, oConversion.MarginBoundsHeight)


        Dim bAnyError As Boolean = False
        Dim bmpWidht As Integer = 0
        Dim bmpHeight As Integer = 0

        bmpWidht = Convert.ToInt32(PaperWidth * _DpiX)
        bmpHeight = Convert.ToInt32(PaperHeight * _DpiY)

        Try
            Using NewBitmap As System.Drawing.Bitmap = New Bitmap(bmpWidht, bmpHeight)
                Dim emfBytes As Byte() = Nothing
                Try
                    NewBitmap.SetResolution(_DpiX, _DpiY)
                    If toCreateEMF Then
                        emfBytes = gloGlobal.CreateEMF.GetEMFBytes(PaperWidth, PaperHeight, bmpWidht, bmpHeight, AddressOf CreateEMFForUB04Form)
                    Else
                        Using eGraphics As System.Drawing.Graphics = Graphics.FromImage(NewBitmap)
                            FitImageToScaleAndKeepAtCenter(eGraphics)
                        End Using
                    End If
                Catch generatedExceptionName As Exception
                    If toCreateEMF Then
                        toCreateEMF = False
                        Try
                            Using eGraphics As System.Drawing.Graphics = Graphics.FromImage(NewBitmap)
                                FitImageToScaleAndKeepAtCenter(eGraphics)
                            End Using
                        Catch ex As Exception
                            bAnyError = True
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during scaling tiff file at printdoc_UB04_Conversion method", False)
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                        End Try
                    End If
                End Try
                If bAnyError Then
                    Return _OutputFilePath
                Else

                    Dim _printFilePath As String = ""
                    Dim sPath As String = ""
                    Try
                        sPath = gloSettings.FolderSettings.AppTempFolderPath + "Paper_Claim_Files"

                        If System.IO.Directory.Exists(sPath) = False Then
                            System.IO.Directory.CreateDirectory(sPath)
                        End If
                        _printFilePath = (sPath & Convert.ToString("\")) + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f_BLANK" + (If(toCreateEMF, ".emf", ".tif"))

                        If File.Exists(_printFilePath) = True Then
                            File.Delete(_printFilePath)
                        End If

                        If toCreateEMF Then
                            File.WriteAllBytes(_printFilePath, emfBytes)
                        Else
                            NewBitmap.Save(_printFilePath, System.Drawing.Imaging.ImageFormat.Tiff)
                        End If

                        Return _printFilePath
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during new bitmap save at printdoc_UB04_Conversion method", False)
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)

                        Return _OutputFilePath
                    End Try
                End If
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
        Return ""
    End Function


    Private Function CreateEMFForUB04Form(thisGraphics As Graphics, bmpWidth As Single, bmpHeight As Single) As Integer
        Try
            thisGraphics.Clear(Color.White)
            FitImageToScaleAndKeepAtCenter(thisGraphics)

            Return 0
        Catch
            Return 1
        End Try

    End Function

    Private Sub FitImageToScaleAndKeepAtCenter(eGraphics As System.Drawing.Graphics)
        Dim _width As Integer = _Bounds.Width - Convert.ToInt32(0.3F * _DpiX)
        Dim _height As Integer = _Bounds.Height
        Dim _X As Integer = _Bounds.X + Convert.ToInt32(0.05F * _DpiX)
        Dim _Y As Integer = _Bounds.Y + Convert.ToInt32(0.05F * _DpiY)

        Using thisImage As System.Drawing.Image = System.Drawing.Image.FromFile(_OutputFilePath)
            eGraphics.DrawImage(thisImage, _X, _Y, _width, _height)
        End Using
        Return
    End Sub
    Private Sub RowChange()
        Dim rowIndex As Integer = C1Settings.RowSel
        If C1Settings.Rows.Count - 1 = C1Settings.RowSel Then
            C1Settings.Select(C1Settings.RowSel - 1, C1Settings.ColSel)
        Else
            C1Settings.Select(C1Settings.RowSel + 1, C1Settings.ColSel)
        End If
        C1Settings.Select(rowIndex, C1Settings.ColSel)
    End Sub
    Private Sub C1Settings_RowValidating(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Settings.RowValidating
        If Convert.ToString(C1Settings.GetData(e.Row, e.Col)) = "" Then
            MessageBox.Show("Please enter valid Data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            e.Cancel = True
            bIsInvalidData = True
            C1Settings.StartEditing(e.Row, e.Col)
        Else
            bIsInvalidData = False
        End If

    End Sub

    Private Sub C1Settings_ValidateEdit(sender As System.Object, e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1Settings.ValidateEdit
        If Me.Visible = True Then
            If Convert.ToString(C1Settings.Editor.Text) = "" Then
                MessageBox.Show("Please enter valid Data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                e.Cancel = True
                bIsInvalidData = True
                C1Settings.StartEditing(e.Row, e.Col)
            Else
                bIsInvalidData = False
            End If
        End If
    End Sub
    

    Private Function formatedData(BoxField As UB04FormFieldString, sdata As String) As String
        Try
            If sdata.Trim().Length > Convert.ToInt32(BoxField.CharSize) Then
                sdata = Convert.ToString(sdata).Substring(0, Convert.ToInt32(BoxField.CharSize))
            End If
            If bIsCapitalized Then
                Return sdata.ToUpper()
            Else
                Return sdata
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return ""
        End Try


    End Function

    Private Function formatedData(BoxField As FormFieldString, sdata As String) As String
        Try
            If sdata.Trim().Length > Convert.ToInt32(BoxField.CharSize) Then
                sdata = Convert.ToString(sdata).Substring(0, Convert.ToInt32(BoxField.CharSize))
            End If
            If bIsCapitalized Then
                Return sdata.ToUpper()
            Else
                Return sdata
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return ""
        End Try


    End Function
End Class

