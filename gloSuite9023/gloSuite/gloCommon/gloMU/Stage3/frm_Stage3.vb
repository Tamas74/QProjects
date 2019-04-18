Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms


Public Class frm_Stage3

    Private COL_Check As Integer = 1
    Private COL_Objective As Integer = 0
    Private COL_Measure As Integer = 2
    Private COL_Goal As Integer = 3
    Private COL_Comment As Integer = 4

    Private COL_calNumerator As Integer = 5
    Private COL_calDenominator As Integer = 6

    Private COL_calPercent As Integer = 7
    Private COL_calFlag1 As Integer = 8
    Private COL_calFlag2 As Integer = 9
    Private COL_Copybtn As Integer = 10

    Private COL_rptNumerator As Integer = 11
    Private COL_rptDenominator As Integer = 12

    Private COL_rptPercent As Integer = 13
    Private COL_rptFlag1 As Integer = 14
    Private COL_rptFlag2 As Integer = 15

    Private ROW_CPOEMedicationLabRad As Integer = 1
    Private ROW_CPOEMedication As Integer = 2
    Private ROW_Laboratory As Integer = 3
    Private ROW_Radiology As Integer = 4

    Private ROW_ProtectElectronicHealthInformation As Integer = 5
   

    Private ROW_EPrescription As Integer = 6

  


    Private ROW_PatientElectronicAccess_Measure1 As Integer = 7
    Private ROW_PatientSpecificEducation As Integer = 8

    Private ROW_PatientElectronicAccess_Measure2 As Integer = 9
    Private ROW_SecureElectronicMessaging As Integer = 10
    Private ROW_PatientGeneratedHealthData As Integer = 11



    Private ROW_SummaryOfCare As Integer = 12
    Private ROW_PatientRequeustOrAccept As Integer = 13
    Private ROW_MedicalReconcilation As Integer = 14

 
    Private ROW_ImmunizationRegistry As Integer = 15
    Private ROW_SyndromicServilance As Integer = 16
    Private ROW_SpecializedRegistryReporting As Integer = 17



    Private ProviderID As Int64
    Private StartDate As String
    Private Enddate As String
    Private Percent As String
    Private nReportId As Int64 = 0

    Private nId As Int64
    Private Exception As Int64 = 0

    Private blnIsLoading As Boolean = False

    Private sCellData As String = String.Empty

    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _MessageBoxCaption As String = String.Empty
    Private _databaseConnectionString As String = String.Empty
    Private _LoginName As String = String.Empty
    Private gstrSQLServerName As String = String.Empty
    Private gstrDatabaseName As String = String.Empty
    Private gblnSQLAuthentication As String = String.Empty
    Private gstrSQLUserEMR As String = String.Empty
    Private gstrSQLPasswordEMR As String = String.Empty
    Private gblnDefaultPrinter As Boolean = False

    Dim Con As SqlConnection = New SqlConnection(_databaseConnectionString)
    Public mycaller As frm_ViewStage3
    Private dtReportingPeriod As DataTable
    Private dtProviders As DataTable = Nothing


    Public ReadOnly Property ID() As Int64
        Get
            Return nId
        End Get
    End Property

    Private Sub SetGridStyle()
        Try
            If C1QualityMeasures.Rows.Count > 1 Then
                C1QualityMeasures.Rows.RemoveRange(1, C1QualityMeasures.Rows.Count - 1)
            End If

            C1QualityMeasures.Cols.Count = 16
            C1QualityMeasures.Rows.Count = 18

            C1QualityMeasures.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1QualityMeasures.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            gloC1FlexStyle.Style(C1QualityMeasures)
            C1QualityMeasures.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free

            Dim rg As C1.Win.C1FlexGrid.CellRange
            rg = C1QualityMeasures.GetCellRange(0, COL_calFlag1, 0, COL_calFlag2)

            C1QualityMeasures(0, COL_Check) = "Select"
            C1QualityMeasures(0, COL_Objective) = "Objective"
            C1QualityMeasures(0, COL_calFlag1) = "Flag"
            C1QualityMeasures(0, COL_calFlag2) = "Flag"
            C1QualityMeasures.SetData(0, COL_Measure, "Core Measures")
            C1QualityMeasures.SetData(0, COL_Goal, "Goal%")
            C1QualityMeasures.SetData(0, COL_calNumerator, "Numerator")
            C1QualityMeasures.SetData(0, COL_calDenominator, "Denominator")
            C1QualityMeasures.SetData(0, COL_calPercent, "Percent")

            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            C1QualityMeasures.SetData(0, COL_rptNumerator, "Numerator")
            C1QualityMeasures.SetData(0, COL_rptDenominator, "Denominator")
            C1QualityMeasures.SetData(0, COL_rptPercent, "Percent")
            C1QualityMeasures.SetData(0, COL_rptFlag1, "Flag")
            C1QualityMeasures.SetData(0, COL_rptFlag2, "Flag")
            ''    C1QualityMeasures.SetData(0, COL_PerformancePoints, "Points(%)")
            C1QualityMeasures.Cols(COL_Objective).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftCenter
            C1QualityMeasures.Cols(COL_Check).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Measure).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1QualityMeasures.Cols(COL_Goal).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_Comment).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftCenter
            C1QualityMeasures.Cols(COL_calNumerator).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_calDenominator).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_calFlag1).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_calFlag2).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_calPercent).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_Copybtn).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_rptNumerator).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_rptDenominator).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_rptPercent).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_rptFlag1).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_rptFlag2).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            '   C1QualityMeasures.Cols(COL_PerformancePoints).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter

            ' ''Width
            C1QualityMeasures.Cols(COL_Objective).Width = Width * 0.27
            C1QualityMeasures.Cols(COL_Check).Width = Width * 0.04
            C1QualityMeasures.Cols(COL_Measure).Width = Width * 0.27
            C1QualityMeasures.Cols(COL_Goal).Width = Width * 0.055
            C1QualityMeasures.Cols(COL_Comment).Width = 18 'Width * 0.0132

            C1QualityMeasures.Cols(COL_Comment).AllowResizing = False

            C1QualityMeasures.Cols(COL_calNumerator).Width = Width * 0.067
            C1QualityMeasures.Cols(COL_calDenominator).Width = Width * 0.074
            C1QualityMeasures.Cols(COL_calPercent).Width = Width * 0.06
            C1QualityMeasures.Cols(COL_calFlag1).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_calFlag2).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_Copybtn).Width = 0
            C1QualityMeasures.Cols(COL_rptNumerator).Width = 0
            C1QualityMeasures.Cols(COL_rptDenominator).Width = 0
            C1QualityMeasures.Cols(COL_rptPercent).Width = 0
            C1QualityMeasures.Cols(COL_rptFlag1).Width = 0
            C1QualityMeasures.Cols(COL_rptFlag2).Width = 0
            ' C1QualityMeasures.Cols(COL_PerformancePoints).Width = Width * 0.06
            C1QualityMeasures.ExtendLastCol = False



            ' ''Editing
            C1QualityMeasures.Cols(COL_Objective).AllowEditing = False
            C1QualityMeasures.Cols(COL_Measure).AllowEditing = False
            C1QualityMeasures.Cols(COL_Goal).AllowEditing = False
            C1QualityMeasures.Cols(COL_Comment).AllowEditing = False
            C1QualityMeasures.Cols(COL_calDenominator).AllowEditing = False
            C1QualityMeasures.Cols(COL_calPercent).AllowEditing = False
            C1QualityMeasures.Cols(COL_calFlag1).AllowEditing = False
            C1QualityMeasures.Cols(COL_calFlag2).AllowEditing = False

            C1QualityMeasures.Cols(COL_rptPercent).AllowEditing = False
            C1QualityMeasures.Cols(COL_rptFlag1).AllowEditing = False
            C1QualityMeasures.Cols(COL_rptFlag2).AllowEditing = False
            '  C1QualityMeasures.Cols(COL_PerformancePoints).AllowEditing = False


            Dim cs As C1.Win.C1FlexGrid.CellStyle

            Try
                If (C1QualityMeasures.Styles.Contains("cs_editableChekcbox")) Then
                    cs = C1QualityMeasures.Styles("cs_editableChekcbox")
                Else
                    cs = C1QualityMeasures.Styles.Add("cs_editableChekcbox")

                End If
            Catch ex As Exception
                cs = C1QualityMeasures.Styles.Add("cs_editableChekcbox")

            End Try

            C1QualityMeasures.Rows(0).AllowMerging = True


            For i As Integer = 1 To C1QualityMeasures.Cols.Count - 1
                C1QualityMeasures.Cols(i).AllowEditing = False
            Next

            C1QualityMeasures.Cols(COL_Check).AllowEditing = True
            C1QualityMeasures.Cols(COL_Copybtn).AllowEditing = True
            C1QualityMeasures.Cols(COL_rptDenominator).AllowEditing = True
            C1QualityMeasures.Cols(COL_rptNumerator).AllowEditing = True
            C1QualityMeasures.Cols(COL_calNumerator).AllowEditing = False


            'Aniket: Increase Rows here
            'C1QualityMeasures.Rows.Add(16) ''17

            FillMeasures()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetGoal()

        C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Goal, "60%")
        C1QualityMeasures.SetData(ROW_Laboratory, COL_Goal, "60%")
        C1QualityMeasures.SetData(ROW_Radiology, COL_Goal, "60%")

        C1QualityMeasures.SetData(ROW_EPrescription, COL_Goal, "60%")
        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Goal, "80%")
        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "10%")
        C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Goal, "35%")




        C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Goal, "25%")
        C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Goal, "5%")
        C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Goal, "50%")
        C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Goal, "40%")
        C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Goal, "80%")

    End Sub

    Private Sub FillData()

        Me.Cursor = Cursors.WaitCursor
        Dim _cntPerformance As Decimal = 0
        Dim _cntBaseScoremeasures As Integer = 0


        Try
            Dim dt As New DataTable
            Dim dtprov As DataTable = CType(cmb_Provider.DataSource, DataTable)

            SetProviders()

            'If (dtProviders.Columns.Contains("ProviderName")) Then
            '    dtProviders.Columns.Remove("ProviderName")
            'End If

            'If (dtProviders.Columns.Contains("Select")) Then
            '    dtProviders.Columns.Remove("Select")
            'End If

            'dtProviders.Columns(0).ColumnName = "ProviderID"

            If StartDate Is Nothing Then
                StartDate = dtpicStartDate.Value.ToShortDateString()
                ProviderID = cmb_Provider.SelectedValue.ToString()
            End If
            If Enddate Is Nothing Then
                Enddate = dtpicEndDate.Value.ToShortDateString()
            End If

            dt = GetdataWithParam("MU_CPOE_Medication_Stage2", dtProviders, StartDate, Enddate)

            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_CPOEMedication, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_CPOEMedication, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 60 Then
                        C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_CPOEMedication, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_CPOEMedication, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag1, Nothing)
                End If

                C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag2, ImgFlag.Images(0))


            End If

            dt = GetdataWithParam("MU_CPOE_Laboratory_Stage2", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                C1QualityMeasures.SetData(ROW_Laboratory, COL_calNumerator, dt.Rows(0)(0).ToString())

                C1QualityMeasures.SetData(ROW_Laboratory, COL_calDenominator, dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetCellImage(ROW_Laboratory, COL_calFlag2, ImgFlag.Images(0))
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 60 Then
                        C1QualityMeasures.SetCellImage(ROW_Laboratory, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_Laboratory, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_Laboratory, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_Laboratory, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_Laboratory, COL_calFlag1, Nothing)
                End If
            End If




            dt = GetdataWithParam("MU_CPOE_Radiology_Stage2", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_Radiology, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_Radiology, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 60 Then
                        C1QualityMeasures.SetCellImage(ROW_Radiology, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_Radiology, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_Radiology, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_Radiology, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_Radiology, COL_calFlag1, Nothing)
                End If


                C1QualityMeasures.SetCellImage(ROW_Radiology, COL_calFlag2, ImgFlag.Images(0))



            End If
            'eRx
            dt = GetdataWithParam("MU_ePrescribing_Stage3", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_EPrescription, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_EPrescription, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 60 Then
                        C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_EPrescription, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_EPrescription, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_calFlag1, Nothing)
                End If
            End If

            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If
            'Clinical Decision Support

            'dt = Getdata("MU_DIChecks_Stage2") commented mipsaci
            'If dt.Rows.Count > 0 Then commented mipsaci
            '    If dt.Rows(0)(0).ToString() = "1" Then
            '        C1QualityMeasures.SetData(ROW_ClinicalDecisionDrugInteractionCheck, COL_calNumerator, "Enabled")
            '    Else
            '        C1QualityMeasures.SetData(ROW_ClinicalDecisionDrugInteractionCheck, COL_calNumerator, "Disabled")
            '    End If
            'End If

            'Implement Drug Interaction Checks
            'dt = Getdata("MU_DMRulesSetup_Stage2")
            'If dt.Rows.Count > 0 Then
            '    C1QualityMeasures.SetData(ROW_ClinicalDecisionSupport, COL_calNumerator, "DM Rules: " & dt.Rows(0)(0).ToString())
            'End If


            'Timely Online Access
            dt = GetdataWithParam("MU_PatientElectronicAccess_Measure1_Stage3", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then

                    If Percent <= 80 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, Nothing)
                    End If

                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    '    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    '  _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calPercent, Percent)
                    ' C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_PerformancePoints, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            'View, Download or transmit health information
            dt = GetdataWithParam("MU_PatientElectronicAccess_Measure2_Stage3", dtProviders, StartDate, Enddate, False)

            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                'C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "5%")
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 10 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, Nothing)
                    End If

                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    '  C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calPercent, Percent)
                    ' C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_PerformancePoints, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            'Patient Specific Education 
            dt = GetdataWithParam("MU_PatientSpecificEducation_Stage3", dtProviders, StartDate, Enddate, False)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 35 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    ' C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calPercent, Percent)
                    '   C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_PerformancePoints, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            'Medication Reconciliation
            dt = GetdataWithParam("MU_ClinicalInformation_Reconciliation_Stage3", dtProviders, StartDate, Enddate)  ''sp change
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 80 Then
                        C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    '  C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calPercent, Percent)
                    '    C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_PerformancePoints, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag2, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            'Summary of care
            dt = GetdataWithParam("MU_SummaryofCare_Measure2_Stage3", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 50 Then
                        C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    ' C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calPercent, Percent)
                    '   C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_PerformancePoints, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag2, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            ''request/accept patient care record
            'Summary of care
            dt = GetdataWithParam("MU_RequestAcceptPatientCareRecord_Stage3", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 40 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    '  C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calPercent, Percent)
                    '   C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_PerformancePoints, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag2, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If


            ''ROW_PatientRequeustOrAccept

            ''Patient Generated Health Data


            dt = GetdataWithParam("MU_PatientGeneratedHealthData_Stage3", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 5 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    ' C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calPercent, Percent)
                    '  C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_PerformancePoints, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag2, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If








            'Use Secure Electronic Messaging
            dt = GetdataWithParam("MU_SecureElectronicMessaging_Stage3", dtProviders, StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                'C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Goal, "25%")
                C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 25 Then
                        'C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, "Disabled") 'FormatNumber(Percent, 2, TriState.True) & "%"
                        C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        ' C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, "Enabled") 'FormatNumber(Percent, 2, TriState.True) & "%"
                        C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, Nothing)
                    End If

                    C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    '  C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                    _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                Else
                    C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calPercent, Percent)
                    ' C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_PerformancePoints, Nothing)
                End If

            End If
            If dt.Rows(0)(1) > 0 Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            'Immunization 
            dt = GetdataWithParam("MU_SubmitImmunization_Stage3", dtProviders, StartDate, Enddate)

            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0)(0).ToString() <> "0" And dt.Rows(0)(0).ToString() <> "") Then
                    C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calNumerator, dt.Rows(0)(0).ToString())
                Else
                    C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calNumerator, "None")
                End If
            End If
            If Convert.ToInt64(dt.Rows(0)(1).ToString()) = 1 Then
                C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, ImgFlag.Images(0))
            Else
                C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, Nothing)
            End If
            If Convert.ToString(dt.Rows(0)(0)) > "0" Then
                _cntBaseScoremeasures = _cntBaseScoremeasures + 1
            End If

            'Syndromic Surveillance
            dt = GetdataWithParam("MU_SubmitsurveillanceData_Stage3", dtProviders, StartDate, Enddate)

            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0)(0).ToString() <> "0" And dt.Rows(0)(0).ToString() <> "") Then
                    C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_calNumerator, dt.Rows(0)(0).ToString())
                Else
                    C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_calNumerator, "None")
                End If
            End If
            If Convert.ToInt64(dt.Rows(0)(1).ToString()) = 1 Then
                C1QualityMeasures.SetCellImage(ROW_SyndromicServilance, COL_calFlag2, ImgFlag.Images(0))
            Else
                C1QualityMeasures.SetCellImage(ROW_SyndromicServilance, COL_calFlag2, Nothing)
            End If

            'Specialized Registry Reporting
            dt = Getdata("MU_SpecializedRegistryReporting_Stage3")

            If (Not IsNothing(dt) And dt.Rows.Count > 0) Then
                If dt.Rows(0)(0).ToString() = "1" Then
                    C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_calNumerator, "Enabled")
                Else
                    C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_calNumerator, "Disabled")
                End If
            Else
                C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_calNumerator, "Disabled")
            End If
            C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_Goal, "")
            SetGoal()


        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally



        End Try

        Me.Cursor = Cursors.Default

    End Sub

    Private Function GetAdminsetting(ByVal settingname As String) As String
        Dim value As New Object()
        Dim ogloSettings As New gloSettings.GeneralSettings(_databaseConnectionString)
        Try
            ogloSettings.GetSetting(settingname, 0, appSettings("ClinicID"), value)
            ogloSettings.Dispose()
            ogloSettings = Nothing
            Return value
        Catch ex As Exception
            Return Nothing
        Finally
            value = Nothing
        End Try
    End Function

    Private Sub FillMeasures()

        Try
            C1QualityMeasures.SetData(ROW_CPOEMedicationLabRad, COL_Measure, "CPOE for Medication,Laboratory and Radiology Orders")
            '' C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Objective, "CPOE for Medication,Laboratory and Radiology Orders")

            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Measure, "Medication Orders")
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Goal, "60%")
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Comment, "More than 60 precent of medication orders created by the EP during the EHR reporting period are recorded using computerized provider order entry")
            C1QualityMeasures.Rows(ROW_CPOEMedication).AllowEditing = True

            C1QualityMeasures.SetData(ROW_Laboratory, COL_Measure, "Laboratory Orders")
            C1QualityMeasures.SetData(ROW_Laboratory, COL_Goal, "60%")
            C1QualityMeasures.Rows(ROW_Laboratory).AllowEditing = True
            C1QualityMeasures.SetData(ROW_Laboratory, COL_Comment, "More than 60 precent of laboratory orders created by the EP during the EHR reporting period are recorded using computerized provider order entry")

            C1QualityMeasures.SetData(ROW_Radiology, COL_Measure, "Radiology Orders")
            C1QualityMeasures.SetData(ROW_Radiology, COL_Goal, "60%")
            C1QualityMeasures.Rows(ROW_Radiology).AllowEditing = True
            C1QualityMeasures.SetData(ROW_Radiology, COL_Comment, "More than 60 precent of diagnostic imaging orders created by the EP during the EHR reporting period are recorded using computerized provider order entry")




            C1QualityMeasures.SetData(ROW_EPrescription, COL_Measure, "e-Prescribing (eRx)")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Objective, "Electronic Prescribing")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Goal, "60%")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Comment, "More than 60 precent of all premissible prescriptions written by the EP are queried for a drug fromulary and transmitted using CEHRT")


            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Objective, "Patient Electronic Access")


            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Measure, "Patient Access")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Comment, "The patient is provided timely access to view online, download , and transmit his or her health information nad the provider ensures the patient's health information is available for the patient to access using any application of their choice that is configured to meet the technical specifiations of the Application Programming Interface (API) in the provider's CEHRT.")


            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Measure, "Patient-Specific Education")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Goal, "35%")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Comment, "The EP must use clinically relevant information from CEHRT to identify patient-specific educatioonal resources and provide electronic access to those materials to more that 35 precent of unique patients seen by the EP during the EHR reporting period")

            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Objective, "Protect Electronic Health Information")
            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Measure, "Security Risk Analysis")

            C1QualityMeasures.Rows(ROW_ProtectElectronicHealthInformation).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Comment, "Protect electronic health information created or maintained by the certified EHR technology (CEHRT) through the implementation of appropriate technical capabilities.")

            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Objective, "Coordination of Care Through Patient Engagement")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Measure, "View, Download, or Transmit (VDT)")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Comment, "More than 5 percent of all unique patients seen by the EP during the EHR reporting period (or their authorized representatives) view, download, or transmit to a third party their health information.")



            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Measure, "Secure Messaging")
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Goal, "25%")
            C1QualityMeasures.Rows(ROW_SecureElectronicMessaging).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Comment, "For an EHR reporting period in 2017, more than 5 precent of all unique patients seen by the EP during the EHR reporting period, a secure message was sent using the electronic messaging function of CEHRT to the patient, or in response to a secure message sent by the patient or their authorized representative ")


            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Measure, "Patient Generated Health Data")
            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Goal, "1 Patient")
            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Comment, "Patient Generated Health Data or Data from a nonclinical setting is incorporated into the CEHRT for more than 5 precent of all unique patientts seen by the EP during the EHR reporting period.")

            'C1QualityMeasures.Rows(ROW_MedicalReconcilation).AllowEditing = False

            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Objective, "Health Information Exchange")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Measure, "Patient Care Record Exchange")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Goal, "5%")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Comment, "For more than 50 precent of transitions of care and referrals, the EP that transitions or refers their patient to another setting of care or provider of care")



            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Measure, "Request/Accept Patient Care Record")
            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Goal, "40%")
            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Comment, "For more that 40 precent of transitions or referrals received and patient encounters in which the provider has never before encountered the patient, the EP incorporates into the patient's EHR an elcetronic summary of care document.")

            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Measure, "Clinical information Reconcilation")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Comment, "For more than 80 precent of transitions or referrals received and patient encounters in which the provider has never before encountered the patient, the EP preforms a clinical information reconciliation.")

            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Objective, "Public Health and Clinical Data Registry Reporting")


            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Measure, "Immunization Registries Reporting")
            C1QualityMeasures.Rows(ROW_ImmunizationRegistry).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Comment, "Capability to submit electronic data to immunization registries or immunization information systems except where prohibited, and in accordance with applicable law and practice.")


            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Measure, "Syndromic Surveillance Reporting")
            C1QualityMeasures.Rows(ROW_SyndromicServilance).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Comment, "Capability to submit electronic syndromic surveillance data to public health agencies except where prohibited, and in accordance with applicable law and practice.")

            C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_Measure, "Specialized Registry Reporting")
            C1QualityMeasures.Rows(ROW_SpecializedRegistryReporting).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_Comment, "The EP is in active engagement to submit data to a specialized registry.")

            'Set the checkboxes
            For intCoreMeasures As Integer = ROW_CPOEMedication To ROW_SpecializedRegistryReporting   'ROW_ProtectElectronicHealthInformation

                'If intCoreMeasures <> ROW_PatientElectronicAccess_Measure2 And intCoreMeasures <> ROW_PatientElectronicAccess_Measure1 And intCoreMeasures <> ROW_CPOEMedication And intCoreMeasures <> ROW_ClinicalDecisionSupport And intCoreMeasures <> ROW_ClinicalDecisionDrugInteractionCheck Then ' And intCoreMeasures <> ROW_ImmunizationRegistry And intCoreMeasures <> ROW_SyndromicServilance And intCoreMeasures <> ROW_SpecializedRegistryReporting
                '    C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                'End If
                'If intCoreMeasures <> ROW_PatientElectronicAccess_Measure2 And intCoreMeasures <> ROW_PatientElectronicAccess_Measure1 And intCoreMeasures <> ROW_CPOEMedication And intCoreMeasures <> ROW_ClinicalDecisionDrugInteractionCheck Then ' And intCoreMeasures <> ROW_ImmunizationRegistry And intCoreMeasures <> ROW_SyndromicServilance And intCoreMeasures <> ROW_SpecializedRegistryReporting
                '    C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                'End If
                If intCoreMeasures = ROW_SpecializedRegistryReporting Or intCoreMeasures = ROW_SyndromicServilance Then
                    C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If
                C1QualityMeasures.Rows(intCoreMeasures).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(intCoreMeasures, COL_Comment, ImgFlag.Images(1))

            Next

            Dim rg As C1.Win.C1FlexGrid.CellRange
            'rg = C1QualityMeasures.GetCellRange(ROW_CPOEMedication, COL_Copybtn, ROW_EPrescription, COL_Copybtn)
            'rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_PatientElectronicAccess_Measure1, COL_Copybtn, ROW_PatientElectronicAccess_Measure2, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_PatientSpecificEducation, COL_Copybtn, ROW_MedicalReconcilation, COL_Copybtn)
            rg.StyleNew.ComboList = "..."


            rg = C1QualityMeasures.GetCellRange(ROW_SummaryOfCare, COL_Copybtn, ROW_SummaryOfCare, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            SetGoal()
            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always


        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frm_MU_Dashboard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        blnIsLoading = True

        RemoveHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

        If IsNothing(dtProviders) = False Then
            dtProviders.Dispose()
            dtProviders = Nothing
        End If

        If IsNothing(mycaller) = False Then
            If mycaller.CanSelect = True Then
                mycaller.RefreshMeasure(nId)

            End If
        End If

        cmb_RptYear.DataSource = Nothing
        cmb_RptYear.Items.Clear()
        If IsNothing(dtReportingPeriod) = False Then
            dtReportingPeriod.Dispose()
            dtReportingPeriod = Nothing
        End If


        If IsNothing(Con) = False Then
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            Con.Dispose()
            Con = Nothing

        End If



    End Sub

    Private Sub SetDatePickersAccess()

        If cmb_RptYear.SelectedValue = 90 Then

            dtpicStartDate.Enabled = True
            dtpicEndDate.Enabled = False

        ElseIf cmb_RptYear.SelectedValue = 0 Then

            dtpicStartDate.Enabled = True
            dtpicEndDate.Enabled = True

        Else

            dtpicStartDate.Enabled = False
            dtpicEndDate.Enabled = False

        End If

    End Sub

    Private Sub SetReportingDates()
        Try

            dtpicStartDate.MaxDate = New DateTime(2099, 1, 1)
            '  RemoveHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged
            '  RemoveHandler dtpicStartDate.ValueChanged, AddressOf dtpicStartDate_ValueChanged
            'First Year [any 90 day period]
            dtpicEndDate.Value = DateTime.Now ''added for bugid 100240

            If cmb_RptYear.SelectedValue = 90 Then

                dtpicStartDate.Value = New DateTime(Date.Now.Year, 1, 1)
                dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(89)

                dtpicStartDate.Enabled = True
                dtpicEndDate.Enabled = False

                '2013 Calendar Year
            ElseIf cmb_RptYear.SelectedValue = 2013 Then

                dtpicStartDate.Value = New DateTime(2013, 1, 1)
                dtpicEndDate.Value = New DateTime(2013, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

                '2014 Q1
            ElseIf cmb_RptYear.SelectedValue = 20141 Then

                dtpicStartDate.Value = New DateTime(2014, 1, 1)
                dtpicEndDate.Value = New DateTime(2014, 3, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

                '2014 Q2
            ElseIf cmb_RptYear.SelectedValue = 20142 Then

                dtpicStartDate.Value = New DateTime(2014, 4, 1)
                dtpicEndDate.Value = New DateTime(2014, 6, 30)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

                '2014 Q3
            ElseIf cmb_RptYear.SelectedValue = 20143 Then

                dtpicStartDate.Value = New DateTime(2014, 7, 1)
                dtpicEndDate.Value = New DateTime(2014, 9, 30)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

                '2014 Q4
            ElseIf cmb_RptYear.SelectedValue = 20144 Then

                dtpicStartDate.Value = New DateTime(2014, 10, 1)
                dtpicEndDate.Value = New DateTime(2014, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

                '2015 Calendar Year
            ElseIf cmb_RptYear.SelectedValue = 2015 Then

                dtpicStartDate.Value = New DateTime(2015, 1, 1)
                dtpicEndDate.Value = New DateTime(2015, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

                '2016 Calendar Year
            ElseIf cmb_RptYear.SelectedValue = 2016 Then

                dtpicStartDate.Value = New DateTime(2016, 1, 1)

                dtpicEndDate.Value = New DateTime(2016, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False
                '2016 Calendar Year
            ElseIf cmb_RptYear.SelectedValue = 2017 Then

                dtpicStartDate.Value = New DateTime(2017, 1, 1)

                dtpicEndDate.Value = New DateTime(2017, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

            ElseIf cmb_RptYear.SelectedValue = 2018 Then

                dtpicStartDate.Value = New DateTime(2018, 1, 1)

                dtpicEndDate.Value = New DateTime(2018, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False
                'Manual Set  
            ElseIf cmb_RptYear.SelectedValue = 0 Then
                dtpicStartDate.Text = Date.Today
                dtpicEndDate.Text = Date.Today

                dtpicStartDate.Enabled = True
                dtpicEndDate.Enabled = True

            ElseIf cmb_RptYear.SelectedValue = 1 Then
                dtpicStartDate.Value = New DateTime(DateTime.Now.Year, 1, 1)
                dtpicEndDate.Value = New DateTime(DateTime.Now.Year, 3, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False

            ElseIf cmb_RptYear.SelectedValue = 2 Then
                dtpicStartDate.Value = New DateTime(DateTime.Now.Year, 4, 1)
                dtpicEndDate.Value = New DateTime(DateTime.Now.Year, 6, 30)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False


            ElseIf cmb_RptYear.SelectedValue = 3 Then
                dtpicStartDate.Value = New DateTime(DateTime.Now.Year, 7, 1)
                dtpicEndDate.Value = New DateTime(DateTime.Now.Year, 9, 30)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False
            ElseIf cmb_RptYear.SelectedValue = 4 Then
                dtpicStartDate.Value = New DateTime(DateTime.Now.Year, 10, 1)
                dtpicEndDate.Value = New DateTime(DateTime.Now.Year, 12, 31)

                dtpicStartDate.Enabled = False
                dtpicEndDate.Enabled = False
            End If
        Catch ex As Exception
        Finally
            '    AddHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged
            '  AddHandler dtpicStartDate.ValueChanged, AddressOf dtpicStartDate_ValueChanged
        End Try
    End Sub

    Private Sub frm_MU_Dashboard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            blnIsLoading = True

            SetGridStyle()

            FillYear()

            If nReportId = 0 Then

                FillProvider()
                SetReportingDates()
                GetValues()
                FillData()

            Else

                FillFromDT(nReportId)

            End If

            CheckStatus()

            'Aniket: 12-Mar-13 Fixed Issue 47723 from 7021
            ShowNPIandTacID(cmb_Provider.SelectedValue)

            AddHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

            blnIsLoading = False

        Catch ex As Exception
            blnIsLoading = False
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillFromDT(ByVal rptid As Int64)
        Dim flgImmu As Boolean = False
        Dim flgSyndr As Boolean = False
        Dim flgSpec As Boolean = False

        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dt As DataTable = Nothing
            Dim i As Integer
            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ReportID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Value = rptid
            oParameters.Add(oParameter)
            oParameter = Nothing


            ''''''''''''''''''''''''Master Record'''''''''''''''''''''''''''''''''

            oDB.Retrive("MU_Select_MainMeasure_MST_Stage3", oParameters, dt)
            oParameters.Clear()

            If dt.Rows.Count > 0 Then

                txtReportName.Text = dt.Rows(0)(0)
                FillProvider(Convert.ToInt64(dt.Rows(0)(1)))
                cmb_Provider.SelectedValue = Convert.ToInt64(dt.Rows(0)(1))
                ShowNPIandTacID(Convert.ToInt64(dt.Rows(0)(1)))
                cmb_RptYear.Text = dt.Rows(0)(2).ToString()


                SetDatePickersAccess()

                dtpicStartDate.Text = dt.Rows(0)(4).ToString()
                dtpicEndDate.Text = dt.Rows(0)(5).ToString()
                Label5.Text = dt.Rows(0)(6).ToString()
                'If (Convert.ToString(dt.Rows(0)("sBaseScore")).Trim() = "") Then
                '    lblBaseScoreData.Text = "0"
                'Else
                '    lblBaseScoreData.Text = dt.Rows(0)("sBaseScore").ToString()
                'End If
                'lblPerformanceScoreData.Text = dt.Rows(0)("sPerformanceScore").ToString()

                dt.Clear()


                ''''''''''''''''''''Details Record'''''''''''''''''''''''''''''
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ReportID"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = rptid
                oParameters.Add(oParameter)
                oParameter = Nothing

                oDB.Retrive("MU_Select_MainMeasure_DTL_Stage3", oParameters, dt)
                oParameters.Clear()

                Dim dtrwno As Integer = 0
                'Dim dr As DataRow() = dt.Select("sMeasure in('Medication Orders','Radiology Orders','Laboratory Orders')")
                'For rowlen As Integer = 0 To dr.Length - 1
                '    dt.Rows.Remove(dr(rowlen))
                'Next


                If dt.Rows.Count > 0 Then
                    For i = 1 To C1QualityMeasures.Rows.Count - 1
                        'If i = ROW_ClinicalDecisionSupportRule Or i = ROW_CPOEMainMeasure Or i = ROW_PatientElectronicAccess Or i = ROW_PublicHealthMeasures Then
                        '    Continue For
                        'End If
                        ''Mayuri-commeneted
                        'If i = ROW_PatientElectronicAccess Or i = ROW_PublicHealthMeasures Then
                        '    Continue For
                        'End If
                        'If (dtrwno = 13) Then
                        '    Exit For
                        'End If

                        C1QualityMeasures.SetData(i, COL_Measure, dt.Rows(dtrwno)(1))
                        ' If i = ROW_ClinicalDecisionDrugInteractionCheck Then
                        '''' 'If i = ROW_ClinicalDecisionDrugInteractionCheck Or i = ROW_DrugFormularyCheck Then
                        'If Convert.ToString(dt.Rows(dtrwno)(3)) = "1" Then
                        'C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
                        'Else
                        'C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                        'End If
                        'If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                        'C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                        '  Else
                        'C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                        'End If
                        'If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                        'C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                        'Else
                        'C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                        'End If
                        'dtrwno = dtrwno + 1
                        'Continue For

                        ' Else
                        'If i = ROW_ClinicalDecisionSupport Then
                        '    C1QualityMeasures.SetData(i, COL_calNumerator, "DM Rules: " & dt.Rows(dtrwno)(3))
                        '    If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                        '        C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                        '    Else
                        '        C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                        '    End If
                        '    If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                        '        C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                        '    Else
                        '        C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                        '    End If
                        '    dtrwno = dtrwno + 1
                        '    Continue For

                        If i = ROW_SpecializedRegistryReporting Then

                            If (Convert.ToString(dt.Rows(dtrwno)(0)) = "True") Then
                                C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                flgSpec = True
                            ElseIf (Convert.ToString(dt.Rows(dtrwno)(0)) = "False") Then
                                C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                flgSpec = False
                            End If

                            If (Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(dtrwno)(3)))) Then
                                If (Convert.ToString(dt.Rows(dtrwno)(3)) = "1") Then
                                    C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
                                Else
                                    C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                                End If

                            Else
                                C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                            End If

                            If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                            End If

                            If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                            End If

                        ElseIf i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then

                            If dt.Rows(dtrwno)(3) = 0 Then
                                C1QualityMeasures.SetData(i, COL_calNumerator, "None")
                            Else
                                C1QualityMeasures.SetData(i, COL_calNumerator, gloDateMaster.gloDate.DateAsDateString(dt.Rows(dtrwno)(3)))
                            End If

                            If (Convert.ToString(dt.Rows(dtrwno)(0)) = "True") Then
                                ' 
                                If (i = ROW_ImmunizationRegistry) Then
                                    flgImmu = True
                                Else
                                    flgSyndr = True
                                    C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                End If
                            ElseIf (Convert.ToString(dt.Rows(dtrwno)(0)) = "False") Then
                                ' 
                                If (i = ROW_ImmunizationRegistry) Then
                                    flgImmu = False
                                Else
                                    flgSyndr = False
                                    C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                End If

                            End If


                            If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                            End If

                            If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                            End If

                        ElseIf i = ROW_ProtectElectronicHealthInformation Then
                            C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(dtrwno)("nGoal"))
                        ElseIf (i = ROW_SecureElectronicMessaging Or i = ROW_PatientElectronicAccess_Measure2) Then
                            'C1QualityMeasures.SetData(i, COL_Goal, dt.Rows(dtrwno)(2))
                            C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(dtrwno)(3))
                            C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(dtrwno)(4))

                            If Convert.ToString(dt.Rows(dtrwno)(5)) = "N/A" Or Convert.ToString(dt.Rows(dtrwno)(5)).Trim() = "" Then
                                C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5))
                                '  C1QualityMeasures.SetData(i, COL_PerformancePoints, dt.Rows(dtrwno)(5))
                            Else
                                C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5) & "%")
                                '  C1QualityMeasures.SetData(i, COL_PerformancePoints, (dt.Rows(dtrwno)(5)) / 10 & "%")
                            End If

                            If Convert.ToString(dt.Rows(dtrwno)(6)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_calFlag1, Nothing)
                            End If

                            If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                            End If

                            'C1QualityMeasures.SetData(i, COL_rptNumerator, dt.Rows(dtrwno)(7))
                            'C1QualityMeasures.SetData(i, COL_rptDenominator, dt.Rows(dtrwno)(8))
                            'If Convert.ToString(dt.Rows(dtrwno)(9)) = "N/A" Or Convert.ToString(dt.Rows(dtrwno)(9)).Trim() = "" Then
                            '    C1QualityMeasures.SetData(i, COL_rptPercent, dt.Rows(dtrwno)(9))
                            'Else
                            '    C1QualityMeasures.SetData(i, COL_rptPercent, dt.Rows(dtrwno)(9) & "%")
                            'End If

                            'If (Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(dtrwno)(9)))) Then
                            '    If (Convert.ToString(dt.Rows(dtrwno)(9)).Trim("%") <> "N/A") Then
                            '        If (Convert.ToDouble(Convert.ToString(dt.Rows(dtrwno)(9)).Trim("%")) < 1) Then
                            '            C1QualityMeasures.SetCellImage(i, COL_rptFlag1, ImgFlag.Images(2))
                            '        Else
                            '            C1QualityMeasures.SetCellImage(i, COL_rptFlag1, Nothing)
                            '        End If
                            '    Else
                            '        C1QualityMeasures.SetCellImage(i, COL_rptFlag1, Nothing)
                            '    End If

                            'End If

                            'If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                            '    C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                            'Else
                            '    C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                            'End If

                        Else
                            ' If i = ROW_CPOERadiology Or i = ROW_CPOELabs Then
                            'If (Convert.ToString(dt.Rows(dtrwno)(0)) = "False") Then
                            ' C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                            ' Else
                            ' C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            ' End If
                            'End If
                            If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                            End If
                            If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                            Else
                                C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                            End If
                            C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(dtrwno)(3))
                            C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(dtrwno)(4))
                            If Convert.ToString(dt.Rows(dtrwno)(5)) = "N/A" Or Convert.ToString(dt.Rows(dtrwno)(5)).Trim() = "" Then
                                C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5))
                                ' C1QualityMeasures.SetData(i, COL_PerformancePoints, dt.Rows(dtrwno)(5))
                            Else
                                C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5) & "%")
                                ' C1QualityMeasures.SetData(i, COL_PerformancePoints, (dt.Rows(dtrwno)(5)) / 10 & "%")
                            End If
                            If Convert.ToString(dt.Rows(dtrwno)(6)) = "1" Then
                                C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                            End If
                            C1QualityMeasures.SetData(i, COL_rptNumerator, dt.Rows(dtrwno)(7))
                            C1QualityMeasures.SetData(i, COL_rptDenominator, dt.Rows(dtrwno)(8))
                            If Convert.ToString(dt.Rows(dtrwno)(9)) = "N/A" Or Convert.ToString(dt.Rows(dtrwno)(9)).Trim() = "" Then
                                C1QualityMeasures.SetData(i, COL_rptPercent, dt.Rows(dtrwno)(9))
                            Else
                                C1QualityMeasures.SetData(i, COL_rptPercent, dt.Rows(dtrwno)(9) & "%")
                                If Convert.ToInt64(FormatNumber(dt.Rows(dtrwno)(9), 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(i, COL_Goal).ToString().Trim("%")) Then
                                    C1QualityMeasures.SetCellImage(i, COL_rptFlag1, ImgFlag.Images(2))
                                End If
                            End If

                            If dt.Rows(dtrwno)("bMeasureRequired") = False Then
                                C1QualityMeasures.Rows(i).Visible = False
                            End If

                        End If
                        If i = ROW_PatientElectronicAccess_Measure1 Or i = ROW_PatientSpecificEducation Or i = ROW_PatientElectronicAccess_Measure2 Or i = ROW_SecureElectronicMessaging Or i = ROW_PatientGeneratedHealthData Or i = ROW_SummaryOfCare Or i = ROW_PatientRequeustOrAccept Or i = ROW_MedicalReconcilation Or i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Or i = ROW_SpecializedRegistryReporting Then
                            'If Convert.ToString(dt.Rows(dtrwno)(5)) = "N/A" Or Convert.ToString(dt.Rows(dtrwno)(5)).Trim() = "" Then
                            '    ' C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5))
                            '    C1QualityMeasures.SetData(i, COL_PerformancePoints, Nothing)
                            'Else
                            '   ' C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5) & "%")
                            '    C1QualityMeasures.SetData(i, COL_PerformancePoints, (dt.Rows(dtrwno)(5)) / 10 & "%")
                            'End If
                            'If Convert.ToString(dt.Rows(dtrwno)("sPerformancePoint")) <> "" Then
                            '    C1QualityMeasures.SetData(i, COL_PerformancePoints, dt.Rows(dtrwno)("sPerformancePoint") & "%")
                            'End If

                        End If
                        dtrwno = dtrwno + 1
                    Next
                    ''Mayuri -commeneted
                    'If (flgImmu = True And flgSyndr = True And flgSpec = True) Then
                    '    C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    'Else
                    '    C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    'End If

                End If
            End If
            C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag2, ImgFlag.Images(0))
            C1QualityMeasures.SetCellImage(ROW_Laboratory, COL_calFlag2, ImgFlag.Images(0))
            C1QualityMeasures.SetCellImage(ROW_Radiology, COL_calFlag2, ImgFlag.Images(0))
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'FillData()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetValues()

        ProviderID = cmb_Provider.SelectedValue.ToString()

        StartDate = dtpicStartDate.Value.ToShortDateString()
        Enddate = dtpicEndDate.Value.ToShortDateString


    End Sub

    Private Function CalculatePercent(ByVal numerator As String, ByVal denominator As String) As String
        Try
            Dim strPer As String
            Dim Percent As Single
            If denominator = "0" Then
                strPer = "N/A"
            Else
                Percent = Single.Parse(numerator) / Single.Parse(denominator) * 100
                strPer = Convert.ToString(Percent)
            End If
            Return strPer
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub C1QualityMeasures_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.AfterEdit

        Try

            If blnIsLoading = False Then
                'Bug #96761: MU_2015:gloEMR:Application shows exception on Save n close button.
                If e.Col = COL_calNumerator And (e.Row = ROW_ImmunizationRegistry Or e.Row = ROW_SyndromicServilance Or e.Row = ROW_SpecializedRegistryReporting) Then

                    If (Convert.ToString(C1QualityMeasures.GetData(e.Row, e.Col)) <> sCellData) Then
                        C1QualityMeasures.SetData(e.Row, e.Col, sCellData)
                    End If
                    'End If
                End If

                'If (e.Row = ROW_PublicHealthMeasures And e.Col = COL_Check) Then
                '    If (C1.Win.C1FlexGrid.CheckEnum.Unchecked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        C1QualityMeasures.SetCellCheck(ROW_ImmunizationRegistry, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                '        C1QualityMeasures.SetCellCheck(ROW_SyndromicServilance, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                '        C1QualityMeasures.SetCellCheck(ROW_SpecializedRegistryReporting, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                '    ElseIf (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        C1QualityMeasures.SetCellCheck(ROW_ImmunizationRegistry, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                '        C1QualityMeasures.SetCellCheck(ROW_SyndromicServilance, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                '        C1QualityMeasures.SetCellCheck(ROW_SpecializedRegistryReporting, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                '    End If
                'ElseIf (e.Row = ROW_ImmunizationRegistry And e.Col = COL_Check) Then
                '    If (C1.Win.C1FlexGrid.CheckEnum.Unchecked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        If ((C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_PublicHealthMeasures, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SyndromicServilance, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SpecializedRegistryReporting, e.Col))) Then
                '            C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                '        End If
                '    ElseIf (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        If ((C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SyndromicServilance, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SpecializedRegistryReporting, e.Col))) Then
                '            C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                '        End If
                '    End If
                'ElseIf (e.Row = ROW_SyndromicServilance And e.Col = COL_Check) Then
                '    If (C1.Win.C1FlexGrid.CheckEnum.Unchecked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        If ((C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_PublicHealthMeasures, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_ImmunizationRegistry, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SpecializedRegistryReporting, e.Col))) Then
                '            C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                '        End If
                '    ElseIf (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        If ((C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_ImmunizationRegistry, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SpecializedRegistryReporting, e.Col))) Then
                '            C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                '        End If
                '    End If
                'ElseIf (e.Row = ROW_SpecializedRegistryReporting And e.Col = COL_Check) Then
                '    If (C1.Win.C1FlexGrid.CheckEnum.Unchecked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        If ((C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_PublicHealthMeasures, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_ImmunizationRegistry, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SyndromicServilance, e.Col))) Then
                '            C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                '        End If
                '    ElseIf (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(e.Row, e.Col)) Then
                '        If ((C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_SyndromicServilance, e.Col)) And (C1.Win.C1FlexGrid.CheckEnum.Checked = C1QualityMeasures.GetCellCheck(ROW_ImmunizationRegistry, e.Col))) Then
                '            C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                '        End If
                '    End If
                'End If

                'C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)

                'If (e.Col <> 0) And (e.Col <> 4 And e.Row <> ROW_ProtectElectronicHealthInformation) Then
                '    If C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col).ToString() = "" Then
                '        C1QualityMeasures.SetData(e.Row, COL_rptDenominator, Nothing)
                '        C1QualityMeasures.SetData(e.Row, COL_rptNumerator, Nothing)
                '        C1QualityMeasures.SetData(e.Row, COL_rptPercent, Nothing)
                '        C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag1, Nothing)
                '        C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, Nothing)
                '    Else

                '        If (C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col) < 0) Then
                '            C1QualityMeasures.SetData(C1QualityMeasures.Row, C1QualityMeasures.Col, Math.Abs(C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col)))
                '        End If
                '        If (C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col) = 0) Then
                '            'C1QualityMeasures.SetData(C1QualityMeasures.Row, C1QualityMeasures.Col, 1)
                '        End If
                '        If Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)) <> "" AndAlso Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)) <> "" Then
                '            Dim _Deno As Int64 = 0
                '            Dim _Num As Int64 = 0
                '            _Deno = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)
                '            _Num = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)
                '            If _Deno >= 0 AndAlso _Num >= 0 Then
                '                Dim perc As Single
                '                If _Deno = 0 Then
                '                    C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, "N/A")
                '                    C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)

                '                Else
                '                    perc = _Num / _Deno * 100
                '                    perc = FormatNumber(perc, 2, TriState.True)
                '                    C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, perc & "%")

                '                    'If Convert.ToInt64(FormatNumber(perc, 0, TriState.UseDefault, TriState.UseDefault, TriState.False)) < Convert.ToInt64(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Goal).ToString().Trim("%")) Then
                '                    If (e.Row = ROW_PatientElectronicAccess_Measure2 Or e.Row = ROW_SecureElectronicMessaging) Then
                '                        If _Num < 1 Then
                '                            C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, ImgFlag.Images(2))
                '                        Else
                '                            C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)
                '                        End If
                '                    Else
                '                        If Convert.ToSingle(perc) < Convert.ToSingle(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Goal).ToString().Trim("%")) Then
                '                            C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, ImgFlag.Images(2))
                '                        Else
                '                            C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)
                '                        End If
                '                    End If

                '                End If

                '            Else
                '                C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)
                '                C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, Nothing)
                '            End If
                '            If _Deno = 0 AndAlso _Num = 0 Then
                '                C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, "N/A")
                '            End If
                '            If Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)) <> "" AndAlso Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)) <> "" Then
                '                If _Deno >= 0 AndAlso _Num >= 0 Then
                '                    If e.Row = ROW_MedicalReconcilation Or e.Row = ROW_SummaryOfCare Then
                '                        If _Deno = 0 Then
                '                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, ImgFlag.Images(0))
                '                        Else
                '                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, Nothing)
                '                        End If
                '                    ElseIf e.Row = ROW_CPOEMedication Then
                '                        If _Deno < 100 Then
                '                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, ImgFlag.Images(0))
                '                        Else
                '                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, Nothing)
                '                        End If
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.CellButtonClick
        'Try
        '    Dim rowno As Integer
        '    Dim k As Integer
        '    Dim Perc As Single
        '    Dim img As Image
        '    rowno = C1QualityMeasures.RowSel()

        '    If rowno = 0 Then
        '        For k = ROW_CPOEMedication To ROW_ProtectElectronicHealthInformation
        '            'If k = ROW_ClinicalDecisionDrugInteractionCheck Or k = ROW_ClinicalDecisionSupport Or k = ROW_ProtectElectronicHealthInformation Then
        '            '    Continue For
        '            'End If
        '            If k = ROW_ClinicalDecisionDrugInteractionCheck Or k = ROW_ProtectElectronicHealthInformation Then
        '                Continue For
        '            End If
        '            C1QualityMeasures.SetData(k, COL_rptNumerator, C1QualityMeasures.GetData(k, COL_calNumerator).ToString())
        '            C1QualityMeasures.SetData(k, COL_rptDenominator, C1QualityMeasures.GetData(k, COL_calDenominator).ToString())
        '            C1QualityMeasures.SetData(k, COL_rptPercent, C1QualityMeasures.GetData(k, COL_calPercent).ToString())
        '            If C1QualityMeasures.GetData(k, COL_calPercent).ToString() <> "N/A" Then
        '                Perc = Convert.ToSingle(C1QualityMeasures.GetData(k, COL_calPercent).ToString().Trim("%"))
        '                If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(k, COL_Goal).ToString().Trim("%")) Then
        '                    C1QualityMeasures.SetCellImage(k, COL_rptFlag1, ImgFlag.Images(2))
        '                End If
        '            End If
        '            img = C1QualityMeasures.GetCellImage(k, COL_calFlag2)
        '            If Not IsNothing(img) Then
        '                C1QualityMeasures.SetCellImage(k, COL_rptFlag2, ImgFlag.Images(0))
        '            Else
        '                C1QualityMeasures.SetCellImage(k, COL_rptFlag2, Nothing)
        '            End If
        '        Next
        '    Else

        '        C1QualityMeasures.SetData(rowno, COL_rptNumerator, C1QualityMeasures.GetData(rowno, COL_calNumerator).ToString())
        '        C1QualityMeasures.SetData(rowno, COL_rptDenominator, C1QualityMeasures.GetData(rowno, COL_calDenominator).ToString())
        '        C1QualityMeasures.SetData(rowno, COL_rptPercent, C1QualityMeasures.GetData(rowno, COL_calPercent).ToString())
        '        If C1QualityMeasures.GetData(rowno, COL_calPercent).ToString() <> "N/A" Then
        '            If (rowno = ROW_PatientElectronicAccess_Measure2) Then
        '                If (C1QualityMeasures.GetData(rowno, COL_calNumerator).ToString() >= 1) Then
        '                    C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, Nothing)
        '                Else
        '                    C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, ImgFlag.Images(2))
        '                End If

        '            Else
        '                Perc = Convert.ToSingle(C1QualityMeasures.GetData(rowno, COL_calPercent).ToString().Trim("%"))
        '                If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(rowno, COL_Goal).ToString().Trim("%")) Then
        '                    C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, ImgFlag.Images(2))
        '                End If
        '            End If

        '        Else
        '            C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, Nothing)
        '        End If
        '        img = C1QualityMeasures.GetCellImage(rowno, COL_calFlag2)
        '        If Not IsNothing(img) Then
        '            C1QualityMeasures.SetCellImage(rowno, COL_rptFlag2, ImgFlag.Images(0))
        '        Else
        '            C1QualityMeasures.SetCellImage(rowno, COL_rptFlag2, Nothing)
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub C1QualityMeasures_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.MouseMove
        If C1QualityMeasures.MouseCol = COL_Comment Then
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        Else

        End If
    End Sub

    Private Sub FillProvider(Optional ByVal ProviderID As Int64 = 0)

        Try

            cmb_Provider.Items.Clear()

            Dim clsPat As New cls_MU_Measures_Stage3
            Dim dt As New DataTable

            dt = clsPat.GetProviders(Convert.ToInt64(appSettings("ClinicID")), ProviderID)
            clsPat = Nothing

            cmb_Provider.ValueMember = "nProviderID"
            cmb_Provider.DisplayMember = "ProviderName"
            cmb_Provider.DataSource = dt

            dt = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillYear()

        Dim bytRowCount As Byte = 0

        If IsNothing(dtReportingPeriod) = True Then
            dtReportingPeriod = New DataTable

            dtReportingPeriod.Columns.Add("ReportingPeriodID", GetType(Int64))
            dtReportingPeriod.Columns.Add("ReportingPeriod", GetType(String))

            dtReportingPeriod.Rows.Add()
            bytRowCount = 0
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 90
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "First Year [any 90 day period]"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 2013
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2013 Calendar Year"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 20141
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2014 Q1"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 20142
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2014 Q2"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 20143
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2014 Q3"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 20144
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2014 Q4"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 2015
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2015 Calendar Year"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 2016
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2016 Calendar Year"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 2017
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2017 Calendar Year"


            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 2018
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "2018 Calendar Year"


            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 1
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "Q1"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 2
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "Q2"



            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 3
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "Q3"

            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 4
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "Q4"




            dtReportingPeriod.Rows.Add()
            bytRowCount += 1
            dtReportingPeriod(bytRowCount)("ReportingPeriodID") = 0
            dtReportingPeriod(bytRowCount)("ReportingPeriod") = "Manual Set"

            cmb_RptYear.DisplayMember = "ReportingPeriod"
            cmb_RptYear.ValueMember = "ReportingPeriodID"
            cmb_RptYear.DataSource = dtReportingPeriod
            cmb_RptYear.SelectedIndex = bytRowCount

        End If

    End Sub

    Private Sub chk_FirstYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            If blnIsLoading = False Then
                blnIsLoading = True

                SetReportingDates()

                CheckStatus()
                ' GetValues()

                blnIsLoading = False
                ' FillData()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub CheckStatus()

        Try


            If Now.Date >= dtpicStartDate.Value And Now.Date <= dtpicEndDate.Value Then
                Label5.Text = "Reporting Period in Progress"

            ElseIf Now.Date > dtpicStartDate.Value And Now.Date > dtpicEndDate.Value Then
                Label5.Text = "Reporting Period Complete"

            ElseIf Now.Date < dtpicStartDate.Value And Now.Date < dtpicEndDate.Value Then
                Label5.Text = "Reporting Period Not Started"

            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function GeneratePatientListCreated(ByVal ProviderID As Int64) As DataTable

        Dim cmdPatientList As SqlCommand
        Dim daPatientList As SqlDataAdapter
        Dim strSQL As String
        Dim connPatientList As SqlConnection = New SqlConnection(_databaseConnectionString)
        Dim dtPatientList As New DataTable
        strSQL = "SELECT CONVERT(VARCHAR,Max(dtReportGeneratedDate),101) FROM MU_PatientListReport WHERE nProviderID = " & ProviderID & " AND dtReportGeneratedDate Between '" & StartDate & "' AND '" & Enddate & "'"
        cmdPatientList = New SqlCommand(strSQL, connPatientList)
        daPatientList = New SqlDataAdapter(cmdPatientList)
        Try
            connPatientList.Open()
            daPatientList.Fill(dtPatientList)
            connPatientList.Close()
            If IsDBNull(dtPatientList.Rows(0)(0)) = True Then
                dtPatientList.Rows.Clear()
            End If
            Return dtPatientList
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(connPatientList) Then
                connPatientList.Dispose()
                connPatientList = Nothing
            End If
            If cmdPatientList IsNot Nothing Then
                cmdPatientList.Parameters.Clear()
                cmdPatientList.Dispose()
                cmdPatientList = Nothing
            End If
            daPatientList.Dispose()
            daPatientList = Nothing
        End Try

    End Function

    Private Function Getdata(ByVal SPName As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("" + SPName + "", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Private Function GetdataWithParam(ByVal SPName As String, ByVal ProviderID As DataTable, ByVal startdate As String, ByVal enddate As String, Optional ByVal IsReportingYear As Boolean = True) As DataTable
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim dt As New DataTable

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = ProviderID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@FromDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = startdate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ToDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = enddate
            oParameters.Add(oParameter)
            oParameter = Nothing

            If IsReportingYear = False Then

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ReportingYear"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = IsReportingYear
                oParameters.Add(oParameter)
                oParameter = Nothing

            End If

            oDB.Retrive("" + SPName + "", oParameters, dt)


            oDB.Disconnect()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Sub New(ByVal reportID As Int64)
        nReportId = reportID
        InitializeComponent()
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then


                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                Con = New SqlConnection(_databaseConnectionString)
            End If
        End If
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

    End Sub

    Public Sub New()
        nReportId = 0
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then


                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                Con = New SqlConnection(_databaseConnectionString)
            End If
        End If

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
    End Sub

    Private Sub dtpicStartDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpicStartDate.ValueChanged
        '   dtpicStartDate.MaxDate = dtpicEndDate.Value ''added for bugid 100240
        Try

            If blnIsLoading = False Then

                RemoveHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

                If cmb_RptYear.SelectedValue = 90 Then

                    If Year(dtpicStartDate.Value) <> (Year(Convert.ToDateTime(dtpicStartDate.Value).AddDays(89))) Then
                        MessageBox.Show("The 90 days reporting period must be in a single year.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(89)

                End If

                CheckStatus()
                Application.DoEvents()
                'GetValues()
                'FillData()

                AddHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dtpicEndDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles dtpicEndDate.ValueChanged
        '   dtpicStartDate.MaxDate = dtpicEndDate.Value ''added for bugid 100240
        If blnIsLoading = False Then
            CheckStatus()
            Application.DoEvents()
            'GetValues()
            'FillData()
        End If

    End Sub

    Private Sub tlb_MUDashboard_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlb_MUDashboard.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveReport()
                Case "Print"
                    PrintReport()
                Case "Close"
                    Me.Close()
                Case "Save&Close"
                    If SaveReport() Then
                        Me.Close()
                    End If
                Case "RefreshReportList"
                    GetValues()
                    'SetGridStyle()
                    If nReportId = 0 Then
                        FillData()
                    Else
                        FillData()
                        'FillFromDT(nReportId)
                    End If
                    CheckStatus()
                Case "CQM"

                    Dim objfrmMeasures As New gloMU.frm_MIPSQuality_Reports(cmb_Provider.Text, dtpicStartDate.Value.ToString(), dtpicEndDate.Value.ToString())
                    With objfrmMeasures
                        '.Text = "Meaningful Use"
                        .MdiParent = Me.ParentForm
                        .WindowState = FormWindowState.Maximized
                        .Show()
                        .BringToFront()
                        .ShowInTaskbar = False
                    End With
                    ''added for bugid 109434 
                    'Dim ofrm As New gloMU.frm_MIPSQuality_Reports(cmb_Provider.Text, dtpicStartDate.Value.ToString(), dtpicEndDate.Value.ToString())
                    'ofrm.StartPosition = FormStartPosition.CenterParent
                    'ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    'If Not IsNothing(ofrm) Then
                    '    ofrm.Dispose()
                    '    ofrm = Nothing
                    'End If

                Case "Settings"
                    Dim objfrmExcludedTemplate As New gloMU.frmExcludedTemplate()
                    With objfrmExcludedTemplate
                        '.Text = "Meaningful Use"
                        '.MdiParent = Me.MdiParent
                        .ShowInTaskbar = False
                        .ShowDialog(IIf(IsNothing(objfrmExcludedTemplate.Parent), Me, objfrmExcludedTemplate.Parent))
                        '.WindowState = FormWindowState.Normal
                        .BringToFront()
                        '.ShowInTaskbar = False
                        'If objfrmExcludedTemplate.DialogResult = Windows.Forms.DialogResult.OK Then
                        '    GetValues()
                        '    FillData()
                        'End If
                        .Dispose()
                    End With

            End Select
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintReport()
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport

        Try
            If txtReportName.Text.Trim() = "" Then
                MessageBox.Show("Enter Report Name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If nReportId = 0 Then
                GetValues()
                FillData()
            End If
            If SaveReport() Then
                clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                'clsPrntRpt = New gloSSRSApplication.clsPrintReport("glosvr02", "certification", True, "sa", "saglosvr02")
                clsPrntRpt.PrintReport("rptMUStage3", "ReportID,User", nReportId & "," & _LoginName, gblnDefaultPrinter, "")

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, "Report Printed", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, "Report Not Printed", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Failure)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SaveReport() As Boolean

        'Try
        If txtReportName.Text.Trim() = "" Then
            MessageBox.Show("Enter Report Name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        'If cmb_Provider.Items.Count = 0 Then
        '    MessageBox.Show("Select Reporting Provider.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return False
        'End If

        GetValues()
        Dim img As Image
        Dim cal_Expn As Int16 = 0
        Dim rpt_Expn As Int16 = 0
        'Dim str As String
        Dim blnFlag As Boolean = False
        ' Dim rowno As Int64
        Dim i As Integer
        Dim Perc As Single
        Dim blnFirstYr As Int64 = False
        Dim isSelect As Int32 = False
        Dim MeasureType As Int16 = 1

        'If chk_FirstYear.Checked = True Then
        '    blnFirstYr = 1
        'Else
        '    blnFirstYr = 0
        'End If

        If nReportId = 0 Then
            blnFlag = False
        Else
            blnFlag = True
        End If


        '''''''''''''''''''''''''Insert Or Update In Master Table''''''''''''''''''''''''''

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)
        Dim dt As New DataTable

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@nID"
        oParameter.ParameterDirection = ParameterDirection.InputOutput
        oParameter.DataType = SqlDbType.BigInt
        oParameter.Value = nReportId
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sReportName"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = txtReportName.Text.ToString().Trim()
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@nProviderID"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.BigInt
        oParameter.Value = cmb_Provider.SelectedValue
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sProviderName"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = cmb_Provider.Text
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@nReportingYear"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = cmb_RptYear.Text
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@blnIsFirstYear"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.Bit
        oParameter.Value = blnFirstYr
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@dtStartDate"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = StartDate
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@dtEndDate"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Enddate
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sReportingPeriodStatus"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Label5.Text
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sMachineName"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Environment.MachineName       ''Environment.MachineName 
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sUserName"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = _LoginName
        oParameters.Add(oParameter)
        oParameter = Nothing


        'oParameter = New gloDatabaseLayer.DBParameter()
        'oParameter.ParameterName = "@sBaseScore"
        'oParameter.ParameterDirection = ParameterDirection.Input
        'oParameter.DataType = SqlDbType.VarChar
        'oParameter.Value = lblBaseScoreData.Text
        'oParameters.Add(oParameter)
        'oParameter = Nothing

        'oParameter = New gloDatabaseLayer.DBParameter()
        'oParameter.ParameterName = "@sPerformanceScore"
        'oParameter.ParameterDirection = ParameterDirection.Input
        'oParameter.DataType = SqlDbType.VarChar
        'oParameter.Value = Convert.ToString(lblPerformanceScoreData.Text).Trim("%")
        'oParameters.Add(oParameter)
        'oParameter = Nothing
        '''''''''''''''''''''''''
        oDB.Execute("mu_inup_mainmeasures_mst_stage3", oParameters, oParameters(0).Value)
        'nReportId = oDB.ExecuteScalar("MU_InUp_MainMeasures_MST")
        nReportId = oParameters(0).Value
        'oParameters = Nothing
        oParameters.Clear()
        '''''''''''''''''''''''''''''''''Insert Or Update In Details Table''''''''''''''''''''''''
        '   For i = ROW_ProtectElectronicHealthInformation To ROW_SpecializedRegistryReporting
        For i = ROW_CPOEMedicationLabRad To ROW_SpecializedRegistryReporting

            img = C1QualityMeasures.GetCellImage(i, COL_calFlag2)
            If Not IsNothing(img) Then
                cal_Expn = 1
            Else
                cal_Expn = 0
            End If

            'img = C1QualityMeasures.GetCellImage(i, COL_rptFlag2)
            'If Not IsNothing(img) Then
            '    rpt_Expn = 1
            'Else
            '    rpt_Expn = 0
            'End If


            'If i = ROW_PatientElectronicAccess Or i = ROW_PublicHealthMeasures Then 'Or i = ROW_SyndromicServilance
            '    ''Here We have to Put code for Report which dnt have denominator and numerator values
            '    Continue For
            'End If
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            checkval = C1QualityMeasures.GetCellCheck(i, COL_Check)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Or checkval = C1.Win.C1FlexGrid.CheckEnum.None Then
                isSelect = 1
            Else
                isSelect = 0
            End If

            If i = ROW_ProtectElectronicHealthInformation Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nID"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = nReportId
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@blnIsSelected"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = isSelect
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sMeasure"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = C1QualityMeasures.GetData(i, COL_Measure)
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nLineNo"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = i
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nMeasureType"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = MeasureType
                oParameters.Add(oParameter)
                oParameter = Nothing


                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@bMeasureRequired"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = CheckMeasureRequiredSetting(blnFlag, C1QualityMeasures.GetData(i, COL_Measure))
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nGoal"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator))
                oParameters.Add(oParameter)
                oParameter = Nothing


                'oParameter = New gloDatabaseLayer.DBParameter()
                'oParameter.ParameterName = "@sPerformancePoint"
                'oParameter.ParameterDirection = ParameterDirection.Input
                'oParameter.DataType = SqlDbType.VarChar
                'oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim("%")
                'oParameters.Add(oParameter)
                'oParameter = Nothing


                oDB.Execute("MU_InUp_MainMeasures_DTL_Stage3", oParameters)
                oParameters.Clear()

                Continue For





            ElseIf i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Or i = ROW_SpecializedRegistryReporting Then ''i = ROW_Laboratory Or

                If i = ROW_SpecializedRegistryReporting Then
                    If (Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)).Trim() = "1") Then
                        blnFirstYr = 1
                    Else
                        blnFirstYr = 0
                    End If
                Else
                    If Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)) = "None" Or Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)).Trim() = "" Then
                        blnFirstYr = 0
                    Else
                        Dim dateNumeric As String = (Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)))
                        blnFirstYr = gloDateMaster.gloDate.DateAsNumber(dateNumeric)
                    End If
                End If

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nID"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = nReportId
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@blnIsSelected"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = isSelect
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sMeasure"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = C1QualityMeasures.GetData(i, COL_Measure)
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nLineNo"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = i
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nMeasureType"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = MeasureType
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Numerator"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = blnFirstYr
                oParameters.Add(oParameter)
                oParameter = Nothing

                ''Sanjog- Added on 20101112 to add the Exception Criteria
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Exception"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = cal_Expn
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nRept_Exception"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = rpt_Expn
                oParameters.Add(oParameter)
                oParameter = Nothing
                ''Sanjog- Added on 20101112 to add the Exception Criteria

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@bMeasureRequired"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = CheckMeasureRequiredSetting(blnFlag, C1QualityMeasures.GetData(i, COL_Measure))
                oParameters.Add(oParameter)
                oParameter = Nothing

                'oParameter = New gloDatabaseLayer.DBParameter()
                'oParameter.ParameterName = "@sPerformancePoint"
                'oParameter.ParameterDirection = ParameterDirection.Input
                'oParameter.DataType = SqlDbType.VarChar
                'oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim("%")
                'oParameters.Add(oParameter)
                'oParameter = Nothing


                oDB.Execute("MU_InUp_MainMeasures_DTL_Stage3", oParameters)
                oParameters.Clear()
            Else
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nID"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = nReportId
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@blnIsSelected"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = isSelect
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sMeasure"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = C1QualityMeasures.GetData(i, COL_Measure)
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nGoal"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_Goal)).Trim("%")
                oParameters.Add(oParameter)
                oParameter = Nothing


                'oParameter = New gloDatabaseLayer.DBParameter()
                'oParameter.ParameterName = "@sPerformancePoint"
                'oParameter.ParameterDirection = ParameterDirection.Input
                'oParameter.DataType = SqlDbType.VarChar
                'oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim("%")
                'oParameters.Add(oParameter)
                'oParameter = Nothing

                'If i = ROW_ClinicalDecisionDrugInteractionCheck Or i = ROW_DrugFormularyCheck Then
                'If i = ROW_ClinicalDecisionDrugInteractionCheck Then  commented if condition MIPSACI 
                '    If C1QualityMeasures.GetData(i, COL_calNumerator).ToString() = "Enabled" Then
                '        blnFirstYr = 1
                '    Else
                '        blnFirstYr = 0
                '    End If
                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@nCalc_Numerator"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.BigInt
                '    oParameter.Value = blnFirstYr
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@nLineNo"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.Int
                '    oParameter.Value = i
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@nMeasureType"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.Int
                '    oParameter.Value = MeasureType
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@bMeasureRequired"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.Bit
                '    oParameter.Value = CheckMeasureRequiredSetting(blnFlag, C1QualityMeasures.GetData(i, COL_Measure))
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    oDB.Execute("MIPS_InUp_MainMeasures_DTL_ACI", oParameters)
                '    oParameters.Clear()
                '    Continue For

                'ElseIf i = ROW_ClinicalDecisionSupport Then
                '    Dim str1 As Array
                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@nCalc_Numerator"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.BigInt
                '    str1 = C1QualityMeasures.GetData(i, COL_calNumerator).ToString().Split(":")
                '    oParameter.Value = str1.GetValue(1).ToString().Trim()
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@nLineNo"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.Int
                '    oParameter.Value = i
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@nMeasureType"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.Int
                '    oParameter.Value = MeasureType
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing

                '    ''Sanjog- Added on 20101112 to add the Exception Criteria

                '    ''Sanjog- Added on 20101112 to add the Exception Criteria

                '    oParameter = New gloDatabaseLayer.DBParameter()
                '    oParameter.ParameterName = "@bMeasureRequired"
                '    oParameter.ParameterDirection = ParameterDirection.Input
                '    oParameter.DataType = SqlDbType.Bit
                '    oParameter.Value = CheckMeasureRequiredSetting(blnFlag, C1QualityMeasures.GetData(i, COL_Measure))
                '    oParameters.Add(oParameter)
                '    oParameter = Nothing


                '    oDB.Execute("MIPS_InUp_MainMeasures_DTL_ACI", oParameters)
                '    oParameters.Clear()
                '    Continue For

                ' Else
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Numerator"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt

                'If i = ROW_SecureElectronicMessaging Then
                '    If C1QualityMeasures.GetData(i, COL_calNumerator).ToString() = "Enabled" Then
                '        oParameter.Value = 1
                '    Else
                '        oParameter.Value = 0
                '    End If
                'Else
                oParameter.Value = C1QualityMeasures.GetData(i, COL_calNumerator)
                ' End If

                oParameters.Add(oParameter)
                oParameter = Nothing


                ' End If

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Denominator"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                'If i = ROW_SecureElectronicMessaging Then
                '    oParameter.Value = 0
                'Else
                oParameter.Value = C1QualityMeasures.GetData(i, COL_calDenominator)
                'End If
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sCalc_Percent"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_calPercent)).Trim("%")
                oParameters.Add(oParameter)
                oParameter = Nothing

                If Convert.ToString(C1QualityMeasures.GetData(i, COL_calPercent)) <> "N/A" And Convert.ToString(C1QualityMeasures.GetData(i, COL_calPercent)) <> "" Then

                    Perc = Convert.ToSingle(C1QualityMeasures.GetData(i, COL_calPercent).ToString().Trim("%"))

                    '18-Sep-14 Aniket: Resolving issue where red flag was not shown when % was 4.5 if goal was 5
                    'As per mail from Phill dated 11-Sep-2014: FW: MU Dashboard Calculation Error
                    'If (i = ROW_PatientElectronicAccess_Measure2 Or i = ROW_SecureElectronicMessaging Or i = ROW_Laboratory) Then

                    '    If (C1QualityMeasures.GetData(i, COL_calNumerator) < 1) Then
                    '        blnFirstYr = 1
                    '    Else
                    '        blnFirstYr = 0
                    '    End If
                    'Else
                    ''commented for Bug #100237
                    If Convert.ToDecimal(FormatNumber(Perc, 2, TriState.True)) <= Convert.ToInt64(C1QualityMeasures.GetData(i, COL_Goal).ToString().Trim("%")) Then
                        blnFirstYr = 1
                        '''''If Flag Is 1 Means Goal % is Not achieve
                    Else
                        blnFirstYr = 0
                        '''''If Flag Is 0 Means Goal % is achieve
                    End If

                    'End If


                Else
                    blnFirstYr = 2
                    '''''If Flag Is 2 Means Goal % is N/A Not Applicable
                End If

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Status"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = blnFirstYr
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nRept_Numerator"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = C1QualityMeasures.GetData(i, COL_rptNumerator)
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nRept_Denominator"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = C1QualityMeasures.GetData(i, COL_rptDenominator)
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sRept_Percent"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_rptPercent)).Trim("%")
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nLineNo"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = i
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nMeasureType"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = MeasureType
                oParameters.Add(oParameter)
                oParameter = Nothing

                ''Sanjog- Added on 20101112 to add the Exception Criteria
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Exception"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = cal_Expn
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nRept_Exception"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Int
                oParameter.Value = rpt_Expn
                oParameters.Add(oParameter)
                oParameter = Nothing
                ''Sanjog- Added on 20101112 to add the Exception Criteria

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@bMeasureRequired"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = CheckMeasureRequiredSetting(blnFlag, C1QualityMeasures.GetData(i, COL_Measure))
                oParameters.Add(oParameter)
                oParameter = Nothing

                'oParameter = New gloDatabaseLayer.DBParameter()
                'oParameter.ParameterName = "@sPerformancePoint"
                'oParameter.ParameterDirection = ParameterDirection.Input
                'oParameter.DataType = SqlDbType.VarChar
                'oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints))
                'oParameters.Add(oParameter)
                'oParameter = Nothing

                oDB.Execute("MU_InUp_MainMeasures_DTL_Stage3", oParameters)
                oParameters.Clear()
            End If
        Next
        If nReportId > 0 Then
            nId = nReportId
        End If
        If blnFlag Then
            ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "Report Updated", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "Stage 2 Modified 2015+ dashboard modified with the name '" & txtReportName.Text & "' for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
        Else
            ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "Report Added", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "New MU Stage 2 Modified 2015+ dashboard created with the name '" & txtReportName.Text & "' for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
        End If
        Return True

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Function

    Private Function CheckMeasureRequiredSetting(ByVal bModifyMeasure As Boolean, ByVal MeasureName As String) As Boolean

        If bModifyMeasure = False Then
            If MeasureName = "Electronic prescriptions" Or MeasureName = "Report clinical quality measures" Or MeasureName = "Electronic copy of pat. health info." Or MeasureName = "Electronically exchange clinical info." Then

                If MeasureName = "Electronic prescriptions" Then
                    If GetAdminsetting("ELECTRONIC PRESCRIPTION MU 2013") = "0" Then
                        Return True
                    Else
                        Return False
                    End If
                End If

                If MeasureName = "Report clinical quality measures" Then
                    If GetAdminsetting("REPORT CLINICAL QM MU 2013") = "0" Then
                        Return True
                    Else
                        Return False
                    End If
                End If

                If MeasureName = "Electronic copy of pat. health info." Then
                    If GetAdminsetting("ELECTRONIC COPY OF PAT. INFO. MU 2013") = "0" Then
                        Return True
                    Else
                        Return False
                    End If
                End If

                If MeasureName = "Electronically exchange clinical info." Then
                    If GetAdminsetting("ELECTRONICALLY EXCH cLINICAL INFO MU 2013") = "0" Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return True
                End If

            Else
                Return True
            End If
        Else
            Return True
        End If

    End Function

    Private Sub cmb_RptYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_RptYear.SelectedIndexChanged

        Try
            If blnIsLoading = False Then
                blnIsLoading = True



                SetReportingDates()

                ' GetValues()

                blnIsLoading = False
                ' FillData()
                CheckStatus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmb_Provider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Provider.SelectionChangeCommitted
        Try
            If blnIsLoading = False Then
                ShowNPIandTacID(cmb_Provider.SelectedValue)
                'GetValues()
                'SetGridStyle()
                ' FillData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.BeforeEdit
        Try
            If blnIsLoading = False Then
                'Bug #96761: MU_2015:gloEMR:Application shows exception on Save n close button.

                'If e.Col = COL_rptDenominator Or e.Col = COL_rptNumerator Or e.Col = COL_Copybtn Then

                '    If e.Row = ROW_ClinicalDecisionDrugInteractionCheck Or e.Row = ROW_ProtectElectronicHealthInformation Or e.Row = ROW_ImmunizationRegistry Or e.Row = ROW_SyndromicServilance Then
                '        e.Cancel = True
                '    End If

                'End If

                If e.Col = COL_Check And (e.Row <> ROW_ImmunizationRegistry AndAlso e.Row <> ROW_SyndromicServilance AndAlso e.Row <> ROW_SpecializedRegistryReporting) Then
                    e.Cancel = True
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1QualityMeasures.KeyPressEdit

        Try

            If e.Col = COL_calNumerator And e.Row = ROW_ProtectElectronicHealthInformation Then
                e.Handled = False
            ElseIf e.Col >= COL_rptNumerator Then
                If Char.IsNumber(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
                    e.Handled = True
                End If
            Else

                e.Handled = True
            End If

        Catch

        End Try

    End Sub

    Private Sub ShowNPIandTacID(ByVal pid As Long)
        Try
            Dim dt As New DataTable
            Dim conn As SqlConnection = New SqlConnection(_databaseConnectionString)
            Dim cmd As SqlCommand = Nothing
            Try
                conn.Open()
                cmd = New SqlCommand("select ISNULL(sNPI,'') as NPI,ISNULL(sCompanyTaxID,'') as TaxID  from Provider_MST where nProviderID=" & pid & "", conn)
                Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
                ad.Fill(dt)
                If dt.Rows.Count > 0 Then
                    lbl_NPIValue.Text = dt.Rows(0)(0).ToString()
                    lbl_TaxIDValue.Text = dt.Rows(0)(1).ToString()
                End If
            Finally
                dt.Dispose()
                dt = Nothing

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

            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1QualityMeasures_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.DoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1QualityMeasures.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                'If (C1QualityMeasures.Row = ROW_ClinicalDecisionDrugInteractionCheck Or C1QualityMeasures.Row = ROW_CQM Or C1QualityMeasures.Row = ROW_ClinicalDecisionSupport Or C1QualityMeasures.Row = ROW_ElectronicExchange Or C1QualityMeasures.Row = ROW_ProtectElectronicHealthInformation Or C1QualityMeasures.Row = ROW_ElectronicExchange Or C1QualityMeasures.Row = ROW_Generatelistofpatient Or C1QualityMeasures.Row = ROW_ImmunizationRegistry Or C1QualityMeasures.Row = ROW_SyndromicServilance Or C1QualityMeasures.Row = ROW_DrugFormularyCheck) Then
                If (C1QualityMeasures.Row = ROW_ProtectElectronicHealthInformation Or C1QualityMeasures.Row = ROW_ImmunizationRegistry Or C1QualityMeasures.Row = ROW_SyndromicServilance Or C1QualityMeasures.Row = ROW_SpecializedRegistryReporting) Then

                    sCellData = String.Empty
                    If C1QualityMeasures.Col = COL_calNumerator Then
                        sCellData = Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col))
                    End If

                    Exit Sub
                End If
                Dim ofrm As frm_MU_Detail_Report
                ofrm = frm_MU_Detail_Report.GetInstance()
                ofrm.StoreProcedureName = GetStoreProcedureName(C1QualityMeasures.Row)
                If (ofrm.StoreProcedureName.Trim() = "") Then
                    Exit Sub
                End If
                Dim MeasureString As String = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Measure).ToString()
                GetValues()

                SetProviders()

                With ofrm

                    If ofrm.StoreProcedureName = "MU_PatientElectronicAccess_Measure2_Stage3" OrElse ofrm.StoreProcedureName = "MU_PatientSpecificEducation_Stage3" Then
                        .IsReportingYear = False
                    End If

                    .ProviderID = dtProviders
                    .StartDate = StartDate
                    .EndDate = Enddate
                    .MdiParent = Me.MdiParent
                    .Show()
                    .SetReportingParameters(MeasureString, cmb_Provider.Text, lbl_NPIValue.Text, cmb_RptYear.Text, txtReportName.Text, lbl_TaxIDValue.Text, False, dtpicStartDate.Value, dtpicEndDate.Value, Label5.Text)
                    .RefreshContent()
                    .BringToFront()
                End With


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub SetProviders()

        If IsNothing(dtProviders) = False Then
            dtProviders.Dispose()
            dtProviders = Nothing
        End If

        dtProviders = New DataTable

        dtProviders.Columns.Add("ProviderID", GetType(Int64))
        dtProviders.Rows.Add(dtProviders.NewRow())

        dtProviders.Rows(0)("ProviderID") = cmb_Provider.SelectedValue

    End Sub

    Public Function GetStoreProcedureName(ByVal ColumnNumber As Integer) As String

        Try
            Select Case ColumnNumber

                Case ROW_CPOEMedication
                    Return "MU_CPOE_Medication_Stage2"

                Case ROW_Laboratory
                    Return "MU_CPOE_Laboratory_Stage2"

                Case ROW_Radiology
                    Return "MU_CPOE_Radiology_Stage2"

                Case ROW_EPrescription
                    Return "MU_ePrescribing_Stage3"

                Case ROW_PatientElectronicAccess_Measure1
                    Return "MU_PatientElectronicAccess_Measure1_Stage3"

                Case ROW_PatientElectronicAccess_Measure2
                    Return "MU_PatientElectronicAccess_Measure2_Stage3"

                Case ROW_PatientSpecificEducation
                    Return "MU_PatientSpecificEducation_Stage3"

                Case ROW_MedicalReconcilation
                    Return "MU_ClinicalInformation_Reconciliation_Stage3"

                Case ROW_SummaryOfCare
                    Return "MU_SummaryofCare_Measure2_Stage3"

                Case ROW_ImmunizationRegistry
                    Return "MU_SubmitImmunization_Stage3"

                Case ROW_SecureElectronicMessaging
                    Return "MU_SecureElectronicMessaging_Stage3"

                Case ROW_SyndromicServilance
                    Return "MU_SubmitsurveillanceData_Stage3"

                Case ROW_PatientGeneratedHealthData
                    Return "MU_PatientGeneratedHealthData_Stage3"

                Case ROW_PatientRequeustOrAccept
                    Return "MU_RequestAcceptPatientCareRecord_Stage3"

                Case ROW_SpecializedRegistryReporting
                    Return "MU_SpecializedRegistryReporting_Stage3"

                Case Else
                    Return ""

            End Select


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        End Try
    End Function


    Private Sub C1QualityMeasures_SetupEditor(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.SetupEditor
        If e.Col = COL_rptNumerator OrElse e.Col = COL_rptDenominator Then
            If Not IsNothing(C1QualityMeasures.Editor) Then
                Dim txt As TextBox = DirectCast(C1QualityMeasures.Editor, TextBox)
                txt.MaxLength = Convert.ToString(Int64.MaxValue).Length - 1
            End If

        End If
    End Sub

    Private Sub lblBaseScore_Click(sender As System.Object, e As System.EventArgs)

    End Sub
End Class
