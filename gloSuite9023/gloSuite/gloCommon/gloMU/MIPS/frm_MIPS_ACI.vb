Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports gloUserControlLibrary
Imports gloCCDLibrary
Imports System.IO

Public Class frm_MIPS_ACI
    Dim dtCount As DataTable
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
    Private COL_Base As Integer = 16
    Private COL_PerformancePoints As Integer = 17
    Private COL_PerformanceScore As Integer = 18
    Private ROW_ProtectElectronicHealthInformation As Integer = 1
    '  Private ROW_CPOEMainMeasure As Integer = 2
    'Private ROW_CPOEMedication As Integer = 2
    'Private ROW_CPOERadiology As Integer = 3
    'Private ROW_CPOELabs As Integer = 4

    Private ROW_EPrescription As Integer = 2 '4

    'Private ROW_ClinicalDecisionSupportRule As Integer = 6 '14
    'Private ROW_ClinicalDecisionSupport As Integer = 7 '14
    'Private ROW_ClinicalDecisionDrugInteractionCheck As Integer = 3 '4'2 commented mipsaci

    'Private ROW_PatientElectronicAccess As Integer = 7
    Private ROW_PatientElectronicAccess_Measure1 As Integer = 3
    Private ROW_PatientSpecificEducation As Integer = 4
    'Private ROW_PatientCordinationofCare As Integer = 10
    Private ROW_PatientElectronicAccess_Measure2 As Integer = 5
    Private ROW_SecureElectronicMessaging As Integer = 6 '30
    Private ROW_PatientGeneratedHealthData As Integer = 7 '30


    'Private ROW_HealthInformationExchange As Integer = 15
    Private ROW_SummaryOfCare As Integer = 8
    Private ROW_PatientRequeustOrAccept As Integer = 9
    Private ROW_MedicalReconcilation As Integer = 10

    'Private ROW_SecureElectronicMessaging As Integer = 14 '30

    'Private ROW_PublicHealthMeasures As Integer = 16 '30
    ' Private ROW_PublicHealthMeasures As Integer = 19 '
    Private ROW_ImmunizationRegistry As Integer = 11 '30
    Private ROW_SyndromicServilance As Integer = 12
    Private ROW_SpecializedRegistryReporting As Integer = 13



    Private ProviderID As Int64
    Private StartDate As String
    Private Enddate As String
    Private Percent As String
    Private Performance As Decimal
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
    Public mycaller As frm_ViewMIPS_ACI
    Private dtReportingPeriod As DataTable
    Private dtProviders As DataTable = Nothing
    Dim Immuregreporting As String = String.Empty
    Dim SurviReporting As String = String.Empty

    Dim sQRDAServiceURL As String = ""
    Dim bEnableQPPSubmission As Boolean = False
    Dim sAuthToken As String = ""

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
            C1QualityMeasures.Cols.Count = 19

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
            'C1QualityMeasures.SetData(0, COL_PerformancePoints, "Points(%)") --Changed to performance
            C1QualityMeasures.SetData(0, COL_Base, "Base")
            C1QualityMeasures.SetData(0, COL_PerformancePoints, "Performance")
            C1QualityMeasures.SetData(0, COL_PerformanceScore, "Max")

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
            C1QualityMeasures.Cols(COL_PerformancePoints).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_PerformanceScore).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_Base).StyleNew.ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            ' ''Width
            C1QualityMeasures.Cols(COL_Objective).Width = Width * 0.24
            C1QualityMeasures.Cols(COL_Check).Width = Width * 0.04
            C1QualityMeasures.Cols(COL_Measure).Width = Width * 0.18
            C1QualityMeasures.Cols(COL_Goal).Width = 0
            C1QualityMeasures.Cols(COL_Comment).Width = 18 'Width * 0.0132

            C1QualityMeasures.Cols(COL_Comment).AllowResizing = False

            C1QualityMeasures.Cols(COL_calNumerator).Width = Width * 0.119
            C1QualityMeasures.Cols(COL_calDenominator).Width = Width * 0.07
            C1QualityMeasures.Cols(COL_calPercent).Width = Width * 0.06
            C1QualityMeasures.Cols(COL_calFlag1).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_calFlag2).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_Copybtn).Width = 0
            C1QualityMeasures.Cols(COL_rptNumerator).Width = 0
            C1QualityMeasures.Cols(COL_rptDenominator).Width = 0
            C1QualityMeasures.Cols(COL_rptPercent).Width = 0
            C1QualityMeasures.Cols(COL_rptFlag1).Width = 0
            C1QualityMeasures.Cols(COL_rptFlag2).Width = 0
            C1QualityMeasures.Cols(COL_PerformancePoints).Width = Width * 0.07
            C1QualityMeasures.Cols(COL_Base).Width = 50
            C1QualityMeasures.Cols(COL_PerformanceScore).Width = 70
            C1QualityMeasures.ExtendLastCol = False



            ' ''Editing
            C1QualityMeasures.Cols(COL_calNumerator).AllowEditing = False ''added forbugid 107004
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
            C1QualityMeasures.Cols(COL_PerformancePoints).AllowEditing = False


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
            C1QualityMeasures.Cols(COL_calNumerator).AllowEditing = True


            'Aniket: Increase Rows here
            C1QualityMeasures.Rows.Add(12) ''17

            FillMeasures()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetProviders()

        Dim dtprov As DataTable = CType(cmb_Provider.DataSource, DataTable)

        If IsNothing(dtProviders) = False Then
            dtProviders.Dispose()
            dtProviders = Nothing
        End If

        dtProviders = dtprov.Copy()

        If (dtProviders.Columns.Contains("ProviderName")) Then
            dtProviders.Columns.Remove("ProviderName")
        End If

        If (dtProviders.Columns.Contains("Select")) Then
            dtProviders.Columns.Remove("Select")
        End If

        dtProviders.Columns(0).ColumnName = "ProviderID"

    End Sub

    Private Sub FillData()

        Me.Cursor = Cursors.WaitCursor
        Dim _cntPerformance As Decimal = 0
        Dim _cntBaseScoremeasures As Integer = 0
        Dim _Performance As Decimal = 0
        Dim dt As DataTable = Nothing
        dtCount = New DataTable
        Try

            If Not IsNothing(cmb_Provider.DataSource) Then


                SetProviders()


                ''added for bugid 121196  It showing Exception on view ACI dashboard
                'If (Not IsNothing(dtProviders)) Then
                '    If (dtProviders.Columns.Contains("sEmployerid")) Then
                '        dtProviders.Columns.Remove("sEmployerid")
                '    End If
                'End If

              

                'eRx
                dt = GetdataWithParam("MU_ePrescribing_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_EPrescription, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_EPrescription, COL_calDenominator, dt.Rows(0)(1).ToString())
                        If Percent <> "N/A" Then
                            If Percent <= 50 Then
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

                    If dt.Rows(0)(0) > 0 Then
                        _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    End If
                End If

                If Convert.ToString(C1QualityMeasures.GetData(ROW_ProtectElectronicHealthInformation, COL_calNumerator)) = "Yes" Then
                    Dim _drow As DataRow = Nothing
                    _drow = dtCount.NewRow
                    _drow(0) = "1"
                    _drow(1) = "0"
                    _drow("MeasureName") = "Security Risk Analysis"
                    _drow("MeasureID") = "PI_PPHI_1"
                    dtCount.Rows.Add(_drow)
                    _drow = Nothing
                Else
                    Dim _drow As DataRow = Nothing
                    _drow = dtCount.NewRow
                    _drow(0) = "0"
                    _drow(1) = "0"
                    _drow("MeasureName") = "Security Risk Analysis"
                    _drow("MeasureID") = "PI_PPHI_1"
                    dtCount.Rows.Add(_drow)
                    _drow = Nothing
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
                dt = GetdataWithParam("MU_PatientElectronicAccess_Measure1_ACI", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calDenominator, dt.Rows(0)(1).ToString())
                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If Percent <= 50 Then
                                C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, Nothing)
                            End If
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            'C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_PerformancePoints, Performance)
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                        Else
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_PerformancePoints, Performance)
                            C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, Nothing)
                        End If

                    End If
                    If dt.Rows(0)(0) > 0 Then
                        _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    End If
                End If
                'View, Download or transmit health information
                dt = GetdataWithParam("MU_PatientElectronicAccess_Measure2_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "1 Patient")
                        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calDenominator, dt.Rows(0)(1).ToString())

                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If dt.Rows(0)(0).ToString() < 1 Then
                                C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, Nothing)
                            End If

                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_PerformancePoints, Performance)
                            'C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                        Else
                            C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, Nothing)
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_PerformancePoints, Performance)
                        End If

                    End If
                    'If dt.Rows(0)(0) > 0 Then
                    '    _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    'End If
                End If

                'Patient Specific Education 
                dt = GetdataWithParam("MU_PatientSpecificEducation_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calDenominator, dt.Rows(0)(1).ToString())
                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If Percent <= 10 Then
                                C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, Nothing)
                            End If
                            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_PerformancePoints, Performance)
                            'C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            _cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                        Else
                            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_PerformancePoints, Performance)
                            C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, Nothing)
                        End If

                    End If
                    'If dt.Rows(0)(1) > 0 Then
                    '    _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    'End If
                End If
                'Medication Reconciliation
                dt = GetdataWithParam("MU_ClinicalInformation_Reconciliation_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())  ''sp change
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calDenominator, dt.Rows(0)(1).ToString())
                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If Percent <= 50 Then
                                C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, Nothing)
                            End If
                            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            'C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_PerformancePoints, Performance)
                        Else
                            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_PerformancePoints, Performance)
                            C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, Nothing)
                        End If
                        If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                            C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag2, ImgFlag.Images(0))
                        Else
                            C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag2, Nothing)
                        End If

                    End If
                    'If dt.Rows(0)(1) > 0 Then
                    '    _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    'End If
                End If
                'Summary of care
                dt = GetdataWithParam("MU_SummaryofCare_Measure2_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calDenominator, dt.Rows(0)(1).ToString())
                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If Percent <= 10 Then
                                C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag1, Nothing)
                            End If
                            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            'C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_PerformancePoints, Performance)
                        Else
                            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_PerformancePoints, Performance)
                            C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag1, Nothing)
                            C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_calFlag2, Nothing)
                        End If

                    End If
                    If dt.Rows(0)(0) > 0 Then
                        _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    End If
                End If

                ''request/accept patient care record
                'Summary of care
                dt = GetdataWithParam("MU_RequestAcceptPatientCareRecord_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calDenominator, dt.Rows(0)(1).ToString())

                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If Percent <= 10 Then
                                C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag1, Nothing)
                            End If
                            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            'C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_PerformancePoints, Performance)
                        Else
                            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_PerformancePoints, Performance)
                            C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag1, Nothing)
                            C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_calFlag2, Nothing)
                        End If

                    End If
                    If dt.Rows(0)(0) > 0 Then
                        _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    End If
                End If

                ''ROW_PatientRequeustOrAccept

                ''Patient Generated Health Data


                dt = GetdataWithParam("MU_PatientGeneratedHealthData_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                        C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calDenominator, dt.Rows(0)(1).ToString())
                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If Percent <= 10 Then
                                C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag1, Nothing)
                            End If
                            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            'C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_PerformancePoints, Performance)
                        Else
                            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_PerformancePoints, Performance)
                            C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag1, Nothing)
                            C1QualityMeasures.SetCellImage(ROW_PatientGeneratedHealthData, COL_calFlag2, Nothing)
                        End If

                    End If
                    'If dt.Rows(0)(1) > 0 Then
                    '    _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    'End If
                End If







                'Use Secure Electronic Messaging
                dt = GetdataWithParam("MU_SecureElectronicMessaging_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                        C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Goal, "1 Patient")
                        C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, dt.Rows(0)(0).ToString())
                        C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calDenominator, dt.Rows(0)(1).ToString())

                        If Percent <> "N/A" Then
                            Performance = CalculatePerformance(Percent, 10)
                            _Performance = _Performance + Performance
                            If dt.Rows(0)(0).ToString() < 1 Then
                                'C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, "Disabled") 'FormatNumber(Percent, 2, TriState.True) & "%"
                                C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, ImgFlag.Images(2))
                            Else
                                ' C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, "Enabled") 'FormatNumber(Percent, 2, TriState.True) & "%"
                                C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, Nothing)
                            End If

                            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                            'C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_PerformancePoints, (FormatNumber(Percent, 2, TriState.True)) / 10 & "%")
                            '_cntPerformance = _cntPerformance + (FormatNumber(Percent, 2, TriState.True)) / 10
                            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_PerformancePoints, Performance)
                        Else
                            C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, Nothing)
                            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calPercent, Percent)
                            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_PerformancePoints, Performance)
                        End If

                    End If
                    'If dt.Rows(0)(1) > 0 Then
                    '    _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    'End If
                End If

                'Immunization 
                dt = GetdataWithParam("MU_SubmitImmunization_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())


                ' dtCount.Merge(dt)
                'If Not IsNothing(dt) Then
                '    If (dt.Rows.Count > 0) Then
                '        If (dt.Rows(0)(0).ToString() <> "0" And dt.Rows(0)(0).ToString() <> "") Then
                '            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calNumerator, dt.Rows(0)(0).ToString())
                '        Else
                '            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calNumerator, "None")
                '        End If
                '    End If
                '    If Convert.ToInt64(dt.Rows(0)(1).ToString()) = 1 Then
                '        C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, ImgFlag.Images(0))
                '    Else
                '        C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, Nothing)
                '    End If
                '    'If Convert.ToString(dt.Rows(0)(0)) > "0" Then
                '    '    _cntBaseScoremeasures = _cntBaseScoremeasures + 1
                '    'End If
                'Else
                '    C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, Nothing)
                'End If
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Performance = 0
                        If (dt.Rows(0)(0).ToString() <> "0" And dt.Rows(0)(0).ToString() <> "") Then
                            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calDenominator, dt.Rows(0)(0).ToString())

                            Dim _drow As DataRow = Nothing
                            _drow = dtCount.NewRow
                            _drow(0) = "0"
                            _drow(1) = "1"
                            _drow("MeasureName") = "Immunization Registry Reporting"
                            _drow("MeasureID") = "PI_PHCDRR_1"
                            dtCount.Rows.Add(_drow)
                            _drow = Nothing
                        Else
                            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calNumerator, "")
                            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calDenominator, "")
                            Dim _drow As DataRow = Nothing
                            _drow = dtCount.NewRow
                            _drow(0) = "0"
                            _drow(1) = "0"
                            _drow("MeasureName") = "Immunization Registry Reporting"
                            _drow("MeasureID") = "PI_PHCDRR_1"
                            dtCount.Rows.Add(_drow)
                            _drow = Nothing
                        End If
                        If Convert.ToInt64(dt.Rows(0)(1).ToString()) = 1 Then
                            C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, ImgFlag.Images(0))
                        Else
                            C1QualityMeasures.SetCellImage(ROW_ImmunizationRegistry, COL_calFlag2, Nothing)
                        End If
                        Try

                            Dim strnumvalue As String = Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator))
                            Dim strdenomvalue As String = Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calDenominator))
                            If (strnumvalue.Contains("Active") AndAlso strdenomvalue.Trim().Length > 2) Then
                                C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_PerformancePoints, "10")
                                Performance = 10

                            Else
                                C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_PerformancePoints, "0")
                                Performance = 0
                            End If
                            _Performance = _Performance + Performance

                        Catch ex As Exception

                        End Try

                    End If

                End If



                'Syndromic Surveillance
                dt = GetdataWithParam("MU_SubmitsurveillanceData_Stage3", dtProviders, StartDate, Enddate, cmb_taxid.Text.ToString())
                '   dtCount.Merge(dt)
                If Not IsNothing(dt) Then
                    If (dt.Rows.Count > 0) Then
                        If (dt.Rows(0)(0).ToString() <> "0" And dt.Rows(0)(0).ToString() <> "") Then
                            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_calNumerator, dt.Rows(0)(0).ToString())
                            Dim _drow As DataRow = Nothing
                            _drow = dtCount.NewRow
                            _drow(0) = "0"
                            _drow(1) = "1"
                            _drow("MeasureName") = "Syndromic Surveillance Reporting"
                            _drow("MeasureID") = "PI_PHCDRR_2"
                            dtCount.Rows.Add(_drow)
                            _drow = Nothing
                        Else
                            Dim _drow As DataRow = Nothing
                            _drow = dtCount.NewRow
                            _drow(0) = "0"
                            _drow(1) = "1"
                            _drow("MeasureName") = "Syndromic Surveillance Reporting"
                            _drow("MeasureID") = "PI_PHCDRR_2"
                            dtCount.Rows.Add(_drow)
                            _drow = Nothing
                            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_calNumerator, "")
                            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_calDenominator, "")
                        End If
                    End If
                    If Convert.ToInt64(dt.Rows(0)(1).ToString()) = 1 Then
                        C1QualityMeasures.SetCellImage(ROW_SyndromicServilance, COL_calFlag2, ImgFlag.Images(0))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SyndromicServilance, COL_calFlag2, Nothing)
                    End If
                End If
                'Specialized Registry Reporting
                '   dt = Getdata("MU_SpecializedRegistryReporting_Stage3")

                'If (Not IsNothing(dt) And dt.Rows.Count > 0) Then
                '    If dt.Rows(0)(0).ToString() = "1" Then
                '        'C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_calNumerator, "Enabled")
                '    Else
                '        'C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_calNumerator, "Disabled")
                '    End If
                'Else
                '    'C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_calNumerator, "Disabled")
                'End If
                'C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_Goal, "")
                lblPerformanceScoreData.Text = _cntPerformance


                'If _cntBaseScoremeasures = 10 Then
                '    lblBaseScoreData.Text = "50"
                'Else
                '    lblBaseScoreData.Text = "0"
                'End If

                If Convert.ToString(C1QualityMeasures.GetData(ROW_ProtectElectronicHealthInformation, COL_calNumerator)) = "Yes" AndAlso _cntBaseScoremeasures = 4 Then
                    '_cntBaseScoremeasures = _cntBaseScoremeasures + 1
                    lblBaseScoreData.Text = "50"
                Else
                    lblBaseScoreData.Text = "0"
                End If
                SetBaseScoreFlag()
                'If _cntBaseScoremeasures = 4 Then
                '    lblBaseScoreData.Text = "50"
                'Else
                '    lblBaseScoreData.Text = "0"
                'End If

                lblPerformanceScoreData.Text = (FormatNumber(_Performance, 2, TriState.True))
                If chkMIPSAPM.Checked Then
                    SetScoreValues(_Performance, True)
                Else
                    SetScoreValues(_Performance)
                End If

                'SetScoreValues(_Performance)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try

        Me.Cursor = Cursors.Default

    End Sub
    Private Sub SetScoreValues(ByVal _Performance As Decimal, Optional ByVal IsMIPSAPM As Boolean = False)
        Dim Total As Decimal = 0
        Dim Acitocompare As Decimal = 0
        Dim ACIScore As Decimal = 0
        Dim basescorevalue = Convert.ToInt32(lblBaseScoreData.Text)

        Total = _Performance + basescorevalue
        lbltotalValue.Text = Convert.ToString(FormatNumber(Total, 2, TriState.True))
        If IsMIPSAPM = True Then
            Acitocompare = (Total * 30) / 100
            If Acitocompare > 30 Then
                ACIScore = 30
            Else
                ACIScore = Acitocompare
            End If
        Else
            Acitocompare = (Total * 25) / 100
            If Acitocompare > 25 Then
                ACIScore = 25
            Else
                ACIScore = Acitocompare
            End If
        End If
        lblACICatValue.Text = FormatNumber(ACIScore, 2, TriState.True)

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
            Dim numcombo As New ComboBox()
            numcombo.DropDownStyle = ComboBoxStyle.DropDownList
            numcombo.Items.Add("Yes")
            numcombo.Items.Add("No")
            AddHandler numcombo.SelectedIndexChanged, AddressOf numcomboselectedChange

            C1QualityMeasures.SetData(ROW_EPrescription, COL_Measure, "e-Prescribing (eRx)")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Objective, "Electronic Prescribing")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Comment, "Generate and transmit permissible prescriptions electronically (eRx).")
            C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_Base, ImgFlag.Images(3))


            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Objective, "Patient Electronic Access")
            'C1QualityMeasures.SetData(ROW_PatientElectronicAccess, COL_Comment, "Provide patients the ability to view online, download and transmit their health information within four business days of the information being available to the EP.")

            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Measure, "Provide Patient Access")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Comment, "More than 50 percent of all unique patients seen by the EP during the EHR reporting period are provided timely (available to the patient within 4 business days after the information is available to the EP) online access to their health information.")
            C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_Base, ImgFlag.Images(3))
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_PerformanceScore, "10")


            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Measure, "Patient-Specific Education")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Comment, "Use clinically relevant information from Certified EHR Technology to identify patient-specific education resources and provide those resources to the patient.")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_PerformanceScore, "10")
            'C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Measure, "View, download, or transmit health information")
            'C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "1 Patient")
            'C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Comment, "More than 5 percent of all unique patients seen by the EP during the EHR reporting period (or their authorized representatives) view, download, or transmit to a third party their health information.")

            'Security Risk Analysis

            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Objective, "Protect Electronic Health Information")
            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Measure, "Security Risk Analysis")
            C1QualityMeasures.SetCellImage(ROW_ProtectElectronicHealthInformation, COL_Base, ImgFlag.Images(3))
            C1QualityMeasures.Rows(ROW_ProtectElectronicHealthInformation).Editor = numcombo
            C1QualityMeasures.Rows(ROW_ProtectElectronicHealthInformation).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Comment, "Protect electronic health information created or maintained by the certified EHR technology (CEHRT) through the implementation of appropriate technical capabilities.")

            'C1QualityMeasures.Rows(ROW_PatientSpecificEducation).AllowEditing = False
            'C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Measure, "Patient-Specific Education Resources")
            'C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Goal, "10%")
            'C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Comment, "Use clinically relevant information from Certified EHR Technology to identify patient-specific education resources and provide those resources to the patient.")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Objective, "Coordination of Care Through Patient Engagement")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Measure, "View, Download, or Transmit (VDT)")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "1 Patient")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Comment, "More than 5 percent of all unique patients seen by the EP during the EHR reporting period (or their authorized representatives) view, download, or transmit to a third party their health information.")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_PerformanceScore, "10")

            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Measure, "Patient Generated Health Data")
            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Goal, "1 Patient")
            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_Comment, "Patient Generated Health Data.")
            C1QualityMeasures.SetData(ROW_PatientGeneratedHealthData, COL_PerformanceScore, "10")


            'C1QualityMeasures.Rows(ROW_MedicalReconcilation).AllowEditing = False
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Measure, "Clinical information Reconcilation")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Comment, "The EP, eligible hospital or CAH who receives a patient from another setting of care or provider of care or believes an encounter is relevant should perform medication reconciliation")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_PerformanceScore, "10")

            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Objective, "Health Information Exchange")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Measure, "Send a Summary of Care")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Comment, "The EP who transitions or refers their patient to another setting of care or provider of care provides a summary of care record for more than 10 percent of such transitions and referrals either (a) electronically transmitted using CEHRT to a recipient or (b) where the recipient receives the summary of care record via exchange facilitated by an organization that is a NwHIN Exchange participant or in a manner that is consistent with the governance mechanism ONC establishes for the NwHIN.")
            C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_Base, ImgFlag.Images(3))
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_PerformanceScore, "10")

            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Measure, "Request/Accept Summary of Care")
            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_Comment, "Request/Accept Summary of Care")
            C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_Base, ImgFlag.Images(3))
            C1QualityMeasures.SetData(ROW_PatientRequeustOrAccept, COL_PerformanceScore, "10")

            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Measure, "Secure Messaging")
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Goal, "1 Patient")
            C1QualityMeasures.Rows(ROW_SecureElectronicMessaging).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Comment, "Use secure electronic messaging to communicate with patients on relevant health information.")
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_PerformanceScore, "10")

            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Objective, "Public Health and Clinical Data Registry Reporting")
            'C1QualityMeasures.SetData(ROW_PublicHealthMeasures, COL_Measure, "Public Health measures")
            'C1QualityMeasures.Rows(ROW_PublicHealthMeasures).AllowEditing = True
            'C1QualityMeasures.SetData(ROW_PublicHealthMeasures, COL_Comment, "The EP is in active engagement with a public health agency to submit electronic public health data from CEHRT except where prohibited and in accordance with applicable law and practice.")

            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Measure, "Immunization Registries Reporting")
            C1QualityMeasures.Rows(ROW_ImmunizationRegistry).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Comment, "Capability to submit electronic data to immunization registries or immunization information systems except where prohibited, and in accordance with applicable law and practice.")
            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_PerformanceScore, "0 or 10")

            'Set Active Engagement
            Dim immucombo As New ComboBox()
            immucombo.DropDownStyle = ComboBoxStyle.DropDownList
            immucombo.Items.Add("")
            immucombo.Items.Add("Active Engagement")
            Dim csty As C1.Win.C1FlexGrid.CellStyle = Nothing
            csty = C1QualityMeasures.Styles.Add("cs_styleimmucombo")

            csty.Editor = immucombo
            RemoveHandler immucombo.SelectedIndexChanged, AddressOf immcomboindexchanged
            AddHandler immucombo.SelectedIndexChanged, AddressOf immcomboindexchanged
            C1QualityMeasures.SetCellStyle(ROW_ImmunizationRegistry, COL_calNumerator, csty)
            Dim syncombo As New ComboBox()
            syncombo.DropDownStyle = ComboBoxStyle.DropDownList
            syncombo.Items.Add("")
            syncombo.Items.Add("Active Engagement")
            Dim cstysyn As C1.Win.C1FlexGrid.CellStyle = Nothing
            cstysyn = C1QualityMeasures.Styles.Add("cs_stylesyncombo")
            cstysyn.Editor = syncombo
            RemoveHandler syncombo.SelectedIndexChanged, AddressOf syncomboindexchanged
            AddHandler syncombo.SelectedIndexChanged, AddressOf syncomboindexchanged





            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Measure, "Syndromic Surveillance Reporting")
            C1QualityMeasures.Rows(ROW_SyndromicServilance).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Comment, "Capability to submit electronic syndromic surveillance data to public health agencies except where prohibited, and in accordance with applicable law and practice.")
            C1QualityMeasures.SetCellStyle(ROW_SyndromicServilance, COL_calNumerator, cstysyn)

            'C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_Measure, "Specialized Registry Reporting")
            'C1QualityMeasures.Rows(ROW_SpecializedRegistryReporting).AllowEditing = True
            'C1QualityMeasures.SetData(ROW_SpecializedRegistryReporting, COL_Comment, "The EP is in active engagement to submit data to a specialized registry.")

            'Set the checkboxes
            For intCoreMeasures As Integer = ROW_ProtectElectronicHealthInformation To ROW_SyndromicServilance    'ROW_ProtectElectronicHealthInformation

                'If intCoreMeasures <> ROW_PatientElectronicAccess_Measure2 And intCoreMeasures <> ROW_PatientElectronicAccess_Measure1 And intCoreMeasures <> ROW_CPOEMedication And intCoreMeasures <> ROW_ClinicalDecisionSupport And intCoreMeasures <> ROW_ClinicalDecisionDrugInteractionCheck Then ' And intCoreMeasures <> ROW_ImmunizationRegistry And intCoreMeasures <> ROW_SyndromicServilance And intCoreMeasures <> ROW_SpecializedRegistryReporting
                '    C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                'End If
                'If intCoreMeasures <> ROW_PatientElectronicAccess_Measure2 And intCoreMeasures <> ROW_PatientElectronicAccess_Measure1 And intCoreMeasures <> ROW_CPOEMedication And intCoreMeasures <> ROW_ClinicalDecisionDrugInteractionCheck Then ' And intCoreMeasures <> ROW_ImmunizationRegistry And intCoreMeasures <> ROW_SyndromicServilance And intCoreMeasures <> ROW_SpecializedRegistryReporting
                '    C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                'End If
                If intCoreMeasures = ROW_SyndromicServilance Then
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


            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always


        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frm_MU_Dashboard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        blnIsLoading = True

        RemoveHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

        If Not IsNothing(dtProviders) Then
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

            '2017 Calendar Year
        ElseIf cmb_RptYear.SelectedValue = 2017 Then

            dtpicStartDate.Value = New DateTime(2017, 1, 1)
            dtpicEndDate.Value = New DateTime(2017, 12, 31)

            dtpicStartDate.Enabled = False
            dtpicEndDate.Enabled = False

            '2018 Calendar Year
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

    End Sub

    Private Sub frm_MU_Dashboard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            blnIsLoading = True
            pnlcustomTask.Visible = False
            SetGridStyle()
            FillYear()
            If nReportId = 0 Then

                ' FillProvider()
                '  ShowTaxID(cmb_Provider.DataSource)
                SetReportingDates()
                GetValues()
                '  FillData()

            Else

                FillFromDT(nReportId)
                ShowTaxID(cmb_Provider.DataSource)
            End If

            CheckStatus()

            'Aniket: 12-Mar-13 Fixed Issue 47723 from 7021
            '   ShowNPIandTacID(cmb_Provider.SelectedValue)
            cmb_taxid.Text = "All"
            AddHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged
            blnIsLoading = False
            Immuregreporting = Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator))
            SurviReporting = Convert.ToString(C1QualityMeasures.GetData(ROW_SyndromicServilance, COL_calNumerator))

            Dim oSettings As New gloSettings.GeneralSettings(_databaseConnectionString)
            oSettings.GetSetting("EnableQPPSubmission", bEnableQPPSubmission)

            If bEnableQPPSubmission Then
                '    oSettings.GetSetting("QRDAIIISUBMISSIONPATH", sQRDAServiceURL)
                '    oSettings.GetSetting("QPPAuthToken", sAuthToken)

                '    Using oCDADataExtraction As New gloCCDLibrary.gloCDADataExtraction()
                '        ClinicCode = oCDADataExtraction.GetPracticeInfo().Rows(0)("sExternalCode")
                '    End Using

                '    '    tlsSubmitCMS.Visible = bEnableQPPSubmission
                '    tlsViewSubReport.Visible = bEnableQPPSubmission
            End If

            oSettings.Dispose()
            oSettings = Nothing

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
            Dim ds As DataSet = Nothing
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

            oDB.Retrive("MIPS_Select_MainMeasure_MST_ACI", oParameters, ds)
            oParameters.Clear()
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then

                txtReportName.Text = dt.Rows(0)(0)

                cmb_Provider.ValueMember = "nProviderID"
                cmb_Provider.DisplayMember = "ProviderName"

                If (ds.Tables.Count > 1) Then
                    cmb_Provider.DataSource = ds.Tables(1)
                End If

                '   dTIN.Clear()

                'For Each element As DataRow In ds.Tables(1).Rows
                '    dTIN.Add(Convert.ToInt64(element("nProviderID")), element("sEmployerID"))
                'Next

                'cmb_Provider.SelectedValue = Convert.ToInt64(dt.Rows(0)(1))

                If cmb_Provider.Items.Count > 0 Then
                    cmb_Provider.SelectedIndex = 0
                End If


                ShowNPIandTaxID(Convert.ToInt64(dt.Rows(0)(1)))
                cmb_RptYear.Text = dt.Rows(0)(2).ToString()


                SetDatePickersAccess()

                dtpicStartDate.Text = dt.Rows(0)(4).ToString()
                dtpicEndDate.Text = dt.Rows(0)(5).ToString()
                Label5.Text = dt.Rows(0)(6).ToString()
                If (Convert.ToString(dt.Rows(0)("sBaseScore")).Trim() = "") Then
                    lblBaseScoreData.Text = "0"
                Else
                    lblBaseScoreData.Text = dt.Rows(0)("sBaseScore").ToString()
                End If
                lblPerformanceScoreData.Text = dt.Rows(0)("sPerformanceScore").ToString()
                lbltotalValue.Text = dt.Rows(0)("sTotal").ToString()
                lblACICatValue.Text = dt.Rows(0)("sAciCatValue").ToString()
                If Not IsDBNull(dt.Rows(0)("bISMIPSAMPChecked")) Then
                    chkMIPSAPM.Checked = Convert.ToBoolean(dt.Rows(0)("bISMIPSAMPChecked"))
                End If


                dt.Clear()


                ''''''''''''''''''''Details Record'''''''''''''''''''''''''''''
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ReportID"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = rptid
                oParameters.Add(oParameter)
                oParameter = Nothing

                oDB.Retrive("MIPS_Select_MainMeasure_DTL_ACI", oParameters, dt)
                oParameters.Clear()

                Dim dtrwno As Integer = 0
                Dim dr As DataRow() = dt.Select("sMeasure in('Medication Orders','Radiology Orders','Laboratory Orders','Specialized Registry Reporting')")

                For rowlen As Integer = 0 To dr.Length - 1
                    dt.Rows.Remove(dr(rowlen))
                Next
                dtCount = dt

                If dt.Rows.Count > 0 Then
                    For i = 1 To C1QualityMeasures.Rows.Count - 1
                        'If i = ROW_ClinicalDecisionSupportRule Or i = ROW_CPOEMainMeasure Or i = ROW_PatientElectronicAccess Or i = ROW_PublicHealthMeasures Then
                        '    Continue For
                        'End If
                        ''Mayuri-commeneted
                        'If i = ROW_PatientElectronicAccess Or i = ROW_PublicHealthMeasures Then
                        '    Continue For
                        'End If

                        C1QualityMeasures.SetData(i, COL_Measure, dt.Rows(dtrwno)(1))


                        If i = ROW_SpecializedRegistryReporting Then

                            'If (Convert.ToString(dt.Rows(dtrwno)(0)) = "True") Then
                            '    C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            '    flgSpec = True
                            'ElseIf (Convert.ToString(dt.Rows(dtrwno)(0)) = "False") Then
                            '    C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                            '    flgSpec = False
                            'End If

                            'If (Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(dtrwno)(3)))) Then
                            '    If (Convert.ToString(dt.Rows(dtrwno)(3)) = "1") Then
                            '        C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
                            '    Else
                            '        C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                            '    End If

                            'Else
                            '    C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                            'End If

                            'If Convert.ToString(dt.Rows(dtrwno)(10)) = "1" Then
                            '    C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
                            'Else
                            '    C1QualityMeasures.SetCellImage(i, COL_calFlag2, Nothing)
                            'End If

                            'If Convert.ToString(dt.Rows(dtrwno)(11)) = "1" Then
                            '    C1QualityMeasures.SetCellImage(i, COL_rptFlag2, ImgFlag.Images(0))
                            'Else
                            '    C1QualityMeasures.SetCellImage(i, COL_rptFlag2, Nothing)
                            'End If

                        ElseIf i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then

                            If Convert.ToString(dt.Rows(dtrwno)(3)) = "0" Or Convert.ToString(dt.Rows(dtrwno)(3)).Trim() = "" Then
                                C1QualityMeasures.SetData(i, COL_calNumerator, "")
                            Else
                                'C1QualityMeasures.SetData(i, COL_calNumerator, gloDateMaster.gloDate.DateAsDateString(dt.Rows(dtrwno)(3)))

                                C1QualityMeasures.SetData(i, COL_calNumerator, "Active Engagement")
                            End If

                            'If dt.Rows(dtrwno)(3) = 0 Then
                            '    C1QualityMeasures.SetData(i, COL_calDenominator, "None")
                            'Else
                            '    C1QualityMeasures.SetData(i, COL_calDenominator, gloDateMaster.gloDate.DateAsDateString(dt.Rows(dtrwno)(3)))
                            'End If

                            If Convert.ToString(dt.Rows(dtrwno)(4)) = "0" Or Convert.ToString(dt.Rows(dtrwno)(4)).Trim() = "" Then
                                C1QualityMeasures.SetData(i, COL_calDenominator, "")
                            Else
                                C1QualityMeasures.SetData(i, COL_calDenominator, gloDateMaster.gloDate.DateAsDateString(dt.Rows(dtrwno)(4)))
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
                            C1QualityMeasures.SetData(i, COL_Goal, dt.Rows(dtrwno)(2))
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



                        Else

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
                        If i = ROW_PatientElectronicAccess_Measure1 Or i = ROW_PatientSpecificEducation Or i = ROW_PatientElectronicAccess_Measure2 Or i = ROW_SecureElectronicMessaging Or i = ROW_PatientGeneratedHealthData Or i = ROW_SummaryOfCare Or i = ROW_PatientRequeustOrAccept Or i = ROW_MedicalReconcilation Or i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then
                            'If Convert.ToString(dt.Rows(dtrwno)(5)) = "N/A" Or Convert.ToString(dt.Rows(dtrwno)(5)).Trim() = "" Then
                            '    ' C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5))
                            '    C1QualityMeasures.SetData(i, COL_PerformancePoints, Nothing)
                            'Else
                            '   ' C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5) & "%")
                            '    C1QualityMeasures.SetData(i, COL_PerformancePoints, (dt.Rows(dtrwno)(5)) / 10 & "%")
                            'End If
                            If Convert.ToString(dt.Rows(dtrwno)("sPerformancePoint")) <> "" Then
                                C1QualityMeasures.SetData(i, COL_PerformancePoints, dt.Rows(dtrwno)("sPerformancePoint"))
                            End If

                        End If
                        dtrwno = dtrwno + 1
                    Next
                    ''Mayuri -commeneted
                    'If (flgImmu = True And flgSyndr = True And flgSpec = True) Then
                    '    C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    'Else
                    '    C1QualityMeasures.SetCellCheck(ROW_PublicHealthMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    'End If
                    SetBaseScoreFlag()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetValues()
        If (Not IsNothing(cmb_Provider.SelectedValue)) Then

            ProviderID = cmb_Provider.SelectedValue.ToString()
        Else
            ProviderID = 0
        End If
        StartDate = dtpicStartDate.Value.ToShortDateString()
        Enddate = dtpicEndDate.Value.ToShortDateString


    End Sub

    Private Function CalculatePercent(ByVal numerator As String, ByVal denominator As String) As String
        Try
            If (numerator.Trim().ToString() = "") Then
                numerator = "0"
            End If
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
                '  If e.Col = COL_calNumerator And (e.Row = ROW_ImmunizationRegistry Or e.Row = ROW_SyndromicServilance Or e.Row = ROW_SpecializedRegistryReporting) Then
                If e.Col = COL_calNumerator AndAlso e.Row = ROW_SpecializedRegistryReporting Then
                    If (Convert.ToString(C1QualityMeasures.GetData(e.Row, e.Col)) <> sCellData) Then
                        C1QualityMeasures.SetData(e.Row, e.Col, sCellData)
                    End If
                    'End If
                End If





            End If
            'If blnIsLoading = False Then
            '    'Bug #96761: MU_2015:gloEMR:Application shows exception on Save n close button.
            '    If e.Col = COL_calNumerator And (e.Row = ROW_ImmunizationRegistry Or e.Row = ROW_SyndromicServilance Or e.Row = ROW_SpecializedRegistryReporting) Then

            '        If (Convert.ToString(C1QualityMeasures.GetData(e.Row, e.Col)) <> sCellData) Then
            '            C1QualityMeasures.SetData(e.Row, e.Col, sCellData)
            '        End If
            '        'End If
            '    End If

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
            'End If
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

    Private Function FillProvider(Optional ByVal ProviderID As Int64 = 0) As DataTable

        Try

            ' cmb_Provider.Items.Clear()

            Dim clsPat As New cls_MU_Measures
            Dim dt As New DataTable

            dt = clsPat.GetProviders(Convert.ToInt64(appSettings("ClinicID")), ProviderID)
            clsPat = Nothing

            cmb_Provider.ValueMember = "nProviderID"
            cmb_Provider.DisplayMember = "ProviderName"
            cmb_Provider.DataSource = dt

            Return dt

        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Function FillCustomGridProvider(Optional ByVal ProviderID As Int64 = 0) As DataTable

        Try

            ' cmb_Provider.Items.Clear()

            Dim clsPat As New cls_MU_Measures
            Dim dt As New DataTable

            dt = clsPat.GetProviders(Convert.ToInt64(appSettings("ClinicID")), ProviderID)
            clsPat = Nothing



            Return dt

        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

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
                'GetValues()


                'FillData()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            blnIsLoading = False
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

    Private Function GetdataWithParam(ByVal SPName As String, ByVal dtProviderID As DataTable, ByVal startdate As String, ByVal enddate As String, ByVal TIN As String) As DataTable
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
            oParameter.Value = dtProviderID
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
            If (TIN.ToUpper() <> "ALL") Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@TIN"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = TIN
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
        'dtpicStartDate.MaxDate = dtpicEndDate.Value ''added for bugid 100240
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
        'dtpicStartDate.MaxDate = dtpicEndDate.Value ''added for bugid 100240
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
                        ''.Text = "Meaningful Use"

                        .MdiParent = Me.ParentForm
                        .WindowState = FormWindowState.Maximized
                        .Show()
                        .BringToFront()
                        .ShowInTaskbar = False

                    End With

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
                Case "QRDAIII"
                    ExportQRDAIII()              
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
                clsPrntRpt.PrintReport("rptMIPSACI", "ReportID,User", nReportId & "," & _LoginName, gblnDefaultPrinter, "")

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

        If cmb_Provider.Items.Count = 0 Then
            MessageBox.Show("Select Reporting Provider.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Dim dtprovid As DataTable = Nothing
        If (Not IsNothing(cmb_Provider.DataSource)) Then
            dtprovid = CType(cmb_Provider.DataSource, DataTable).Copy()
            If (dtprovid.Columns.Contains("ProviderName")) Then
                dtprovid.Columns.Remove("ProviderName")
                dtprovid.Columns("nProviderID").ColumnName = "ProviderID"
            End If
        End If

        ''added for bugid 121196  It showing Exception on view ACI dashboard
        'If (Not IsNothing(dtprovid)) Then
        '    If (dtprovid.Columns.Contains("sEmployerid")) Then
        '        dtprovid.Columns.Remove("sEmployerid")
        '    End If
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

        'oParameter = New gloDatabaseLayer.DBParameter()
        'oParameter.ParameterName = "@nProviderID"
        'oParameter.ParameterDirection = ParameterDirection.Input
        'oParameter.DataType = SqlDbType.BigInt
        'oParameter.Value = cmb_Provider.SelectedValue
        'oParameters.Add(oParameter)
        'oParameter = Nothing

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


        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sBaseScore"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = lblBaseScoreData.Text
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sPerformanceScore"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Convert.ToString(lblPerformanceScoreData.Text).Trim("%")
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sTotal"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Convert.ToString(lbltotalValue.Text).Trim("%")
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sAciCatValue"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Convert.ToString(lblACICatValue.Text)
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@bIsMIPSAMPChecked"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.Bit
        oParameter.Value = chkMIPSAPM.Checked
        oParameters.Add(oParameter)
        oParameter = Nothing

        Dim strtin As String = ""
        Dim dttaxid As DataTable = CType(cmb_taxid.DataSource, DataTable)
        If (Not IsNothing(dttaxid)) Then
            For Each dr As DataRow In dttaxid.Rows
                strtin = strtin & dr(0).ToString() & ","

            Next
        End If
        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sTIN"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Convert.ToString(strtin)
        oParameters.Add(oParameter)
        oParameter = Nothing

        oParameter = New gloDatabaseLayer.DBParameter()
        oParameter.ParameterName = "@sNPI"
        oParameter.ParameterDirection = ParameterDirection.Input
        oParameter.DataType = SqlDbType.VarChar
        oParameter.Value = Convert.ToString(lbl_NPIValue.Text).Trim("%")
        oParameters.Add(oParameter)
        oParameter = Nothing



        If Not IsNothing(dtprovid) Then
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVPProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtprovid
            oParameters.Add(oParameter)
            oParameter = Nothing
        End If

        If (cmb_taxid.Text = "ALL") Then
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TaxID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = -1
            oParameters.Add(oParameter)
            oParameter = Nothing
        Else
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TaxID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmb_taxid.SelectedValue
            oParameters.Add(oParameter)
            oParameter = Nothing
        End If
        '''''''''''''''''''''''''
        oDB.Execute("MIPS_InUp_MainMeasures_MST_ACI", oParameters, oParameters(0).Value)
        'nReportId = oDB.ExecuteScalar("MU_InUp_MainMeasures_MST")
        nReportId = oParameters(0).Value
        'oParameters = Nothing
        oParameters.Clear()
        '''''''''''''''''''''''''''''''''Insert Or Update In Details Table''''''''''''''''''''''''
        For i = ROW_ProtectElectronicHealthInformation To ROW_SyndromicServilance

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


                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sPerformancePoint"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim()
                oParameters.Add(oParameter)
                oParameter = Nothing


                oDB.Execute("MIPS_InUp_MainMeasures_DTL_ACI", oParameters)
                oParameters.Clear()

                Continue For

            ElseIf i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then

                If i = ROW_SpecializedRegistryReporting Then


                    'If (Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)) = "Enabled") Then
                    '    blnFirstYr = 1
                    'Else
                    '    blnFirstYr = 0
                    'End If
                Else

                    'If Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)) = "None" Or Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)).Trim() = "" Then
                    '    blnFirstYr = 0
                    'Else
                    '    Dim dateNumeric As String = (Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)))
                    '    blnFirstYr = gloDateMaster.gloDate.DateAsNumber(dateNumeric)
                    'End If

                    Dim dateNumeric As String = (Convert.ToString(C1QualityMeasures.GetData(i, COL_calDenominator)))
                    If (dateNumeric.Trim() <> "") AndAlso (dateNumeric.Trim() <> "0") Then
                        blnFirstYr = gloDateMaster.gloDate.DateAsNumber(dateNumeric)
                    Else
                        blnFirstYr = 0
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


                ''
                If i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then

                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@nCalc_Numerator"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.BigInt
                    Dim numvalue As String = Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)).Trim()
                    If (numvalue <> "") AndAlso numvalue <> "0" Then
                        oParameter.Value = 1
                    Else
                        oParameter.Value = 0
                    End If
                    ''oParameter.Value = blnFirstYr
                    oParameters.Add(oParameter)
                    oParameter = Nothing

                    Dim percvalue As String = ""
                    If (i = ROW_ImmunizationRegistry) Then
                        Dim numvalue2 As String = Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)).Trim()
                        Dim denovalue As String = Convert.ToString(C1QualityMeasures.GetData(i, COL_calDenominator)).Trim()

                        If ((numvalue2.Trim() <> "" AndAlso numvalue2 <> "0") AndAlso (denovalue.Trim() <> "" AndAlso denovalue <> "0")) Then
                            percvalue = "10".Trim("%")
                        Else

                            percvalue = "0"
                        End If
                    Else
                        percvalue = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim("%")
                    End If
                    ''
                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@sPerformancePoint"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.VarChar
                    oParameter.Value = percvalue
                    oParameters.Add(oParameter)
                    oParameter = Nothing

                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@nCalc_Denominator"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.BigInt
                    oParameter.Value = blnFirstYr
                    oParameters.Add(oParameter)
                    oParameter = Nothing
                Else
                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@nCalc_Numerator"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.BigInt
                    oParameter.Value = blnFirstYr
                    oParameters.Add(oParameter)
                    oParameter = Nothing

                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@sPerformancePoint"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.VarChar
                    oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim("%")
                    oParameters.Add(oParameter)
                    oParameter = Nothing
                End If

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
                'oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim()
                'oParameters.Add(oParameter)
                'oParameter = Nothing


                oDB.Execute("MIPS_InUp_MainMeasures_DTL_ACI", oParameters)
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


                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sPerformancePoint"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_PerformancePoints)).Trim()
                oParameters.Add(oParameter)
                oParameter = Nothing

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
                If (Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)).Trim() = "") Then

                    oParameter.Value = "0"
                Else
                    oParameter.Value = C1QualityMeasures.GetData(i, COL_calNumerator)
                End If

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
                    If (i = ROW_PatientElectronicAccess_Measure2 Or i = ROW_SecureElectronicMessaging) Then

                        If (C1QualityMeasures.GetData(i, COL_calNumerator) < 1) Then
                            blnFirstYr = 1
                        Else
                            blnFirstYr = 0
                        End If
                    Else
                        ''commented for Bug #100237
                        'If Convert.ToDecimal(FormatNumber(Perc, 2, TriState.True)) <= Convert.ToInt64(C1QualityMeasures.GetData(i, COL_Goal).ToString().Trim("%")) Then
                        '    blnFirstYr = 1
                        '    '''''If Flag Is 1 Means Goal % is Not achieve
                        'Else
                        '    blnFirstYr = 0
                        '    '''''If Flag Is 0 Means Goal % is achieve
                        'End If

                    End If


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


                oDB.Execute("MIPS_InUp_MainMeasures_DTL_ACI", oParameters)
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
        If (Immuregreporting.Trim() <> Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator)).Trim()) Then
            If (Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator)).Trim().Length > 0) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, " Added '" & Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator)) & "' to Immunization   Report " & txtReportName.Text & " for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, " Removed  '" & Immuregreporting & "' From Immunization Report " & txtReportName.Text & " for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            End If

        End If
        If (SurviReporting.Trim() <> Convert.ToString(C1QualityMeasures.GetData(ROW_SyndromicServilance, COL_calNumerator)).Trim()) Then
            ''  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "Change numerator for Syndromic Survillance from  " & SurviReporting & "to " & Convert.ToString(C1QualityMeasures.GetData(ROW_SyndromicServilance, COL_calNumerator)) & " for Report " & txtReportName.Text & " for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            If (Convert.ToString(C1QualityMeasures.GetData(ROW_SyndromicServilance, COL_calNumerator)).Trim().Length > 0) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, " Added '" & Convert.ToString(C1QualityMeasures.GetData(ROW_SyndromicServilance, COL_calNumerator)) & "' to Syndromic Survillance   Report " & txtReportName.Text & " for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, " Removed  '" & SurviReporting & "' From Syndromic Surveillance Report " & txtReportName.Text & " for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            End If

        End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Function
    Private WithEvents dgCustomGrid As CustomTask
    'pnlcustomTask.Height = 225
    '    pnlcustomTask.Width = 330
    '    LoadUserGrid()
    '    SetCheckValues(cmbProblems)
    '    pnlcustomTask.BringToFront()
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

                GetValues()

                blnIsLoading = False
                'FillData()
                'CheckStatus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmb_Provider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Provider.SelectionChangeCommitted
        Try
            If blnIsLoading = False Then
                'ShowNPIandTaxID(cmb_Provider.SelectedValue)
                'GetValues()

                'FillData()
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

                If e.Col = COL_Check And (e.Row <> ROW_ImmunizationRegistry AndAlso e.Row <> ROW_SyndromicServilance) Then
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

    Private Sub ShowNPIandTaxID(ByVal pid As Long)

    End Sub

    Private Sub ShowTaxID(ByVal dtprov As DataTable)



        cmb_taxid.DataSource = Nothing
        cmb_taxid.Items.Clear()

        If Not IsNothing(dtprov) Then

            Dim ds As New DataSet
            Dim dtproviderid As DataTable = dtprov.Copy()
            dtproviderid.Columns.Remove("ProviderName")

            'If dtproviderid.Columns.Contains("sEmployerID") Then
            '    dtproviderid.Columns.Remove("sEmployerID")
            'End If

            dtproviderid.Columns(0).ColumnName = "ProviderID"
            Dim conn As SqlConnection = New SqlConnection(_databaseConnectionString)
            Dim cmd As SqlCommand = Nothing

            Try
                conn.Open()
                cmd = New SqlCommand("gsp_TaxIDListBYProviderID", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Tbl_ProviderID", dtproviderid)

                Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
                ad.Fill(ds)

                If ds.Tables(0).Rows.Count > 0 Then
                    cmb_taxid.DataSource = ds.Tables(0)
                    cmb_taxid.DisplayMember = "TaxID"
                    cmb_taxid.ValueMember = "TaxID"
                    cmb_taxid.Text = "All"
                End If

                'lbl_NPIValue.Text = ""

                If (ds.Tables.Count > 1) Then
                    If (ds.Tables(1).Rows.Count > 0) Then
                        lbl_NPIValue.Text = ""
                        For Each dr As DataRow In ds.Tables(1).Rows
                            lbl_NPIValue.Text &= dr(0).ToString() & " ,"
                        Next
                        If (lbl_NPIValue.Text.Trim().Length > 0) Then
                            lbl_NPIValue.Text = lbl_NPIValue.Text.Substring(0, lbl_NPIValue.Text.Length - 1)
                        End If
                    Else
                        lbl_NPIValue.Text = ""
                    End If
                    C1SuperTooltip1.SetToolTip(lbl_NPIValue, lbl_NPIValue.Text)
                    If (lbl_NPIValue.Text.Trim().Length > 25) Then
                        lbl_NPIValue.Text = lbl_NPIValue.Text.Substring(0, 25) & "..........."
                    End If

                End If

            Finally

                If Not IsNothing(dtproviderid) Then
                    dtproviderid.Dispose()
                End If

                dtproviderid = Nothing

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

        End If

    End Sub

    Private Sub C1QualityMeasures_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.DoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1QualityMeasures.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                'If (C1QualityMeasures.Row = ROW_ClinicalDecisionDrugInteractionCheck Or C1QualityMeasures.Row = ROW_CQM Or C1QualityMeasures.Row = ROW_ClinicalDecisionSupport Or C1QualityMeasures.Row = ROW_ElectronicExchange Or C1QualityMeasures.Row = ROW_ProtectElectronicHealthInformation Or C1QualityMeasures.Row = ROW_ElectronicExchange Or C1QualityMeasures.Row = ROW_Generatelistofpatient Or C1QualityMeasures.Row = ROW_ImmunizationRegistry Or C1QualityMeasures.Row = ROW_SyndromicServilance Or C1QualityMeasures.Row = ROW_DrugFormularyCheck) Then
                If (C1QualityMeasures.Row = ROW_ProtectElectronicHealthInformation Or C1QualityMeasures.Row = ROW_ImmunizationRegistry Or C1QualityMeasures.Row = ROW_SyndromicServilance) Then

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

                If IsNothing(dtProviders) = True Then
                    SetProviders()
                End If

                With ofrm
                    .TIN = cmb_taxid.Text.ToString()
                    .ProviderID = dtProviders
                    .StartDate = StartDate
                    .EndDate = Enddate
                    .MdiParent = Me.MdiParent
                    .Show()
                    Dim strtaxid As String = ""
                    If (cmb_taxid.Text = "ALL") Then
                        Dim dttxid As DataTable = CType(cmb_taxid.DataSource, DataTable)
                        If (Not IsNothing(dttxid)) Then
                            For Each dr As DataRow In dttxid.Rows
                                If (dr(0).ToString().Trim() <> "ALL") Then
                                    strtaxid = strtaxid & dr(0).ToString() & ","
                                End If
                            Next
                        End If
                        If (strtaxid.Length > 0) Then
                            strtaxid = strtaxid.Substring(0, strtaxid.Length - 1)
                        End If
                    Else
                        strtaxid = cmb_taxid.Text
                    End If
                    .SetReportingParameters(MeasureString, cmb_Provider.Text, lbl_NPIValue.Text, cmb_RptYear.Text, txtReportName.Text, strtaxid, False, dtpicStartDate.Value, dtpicEndDate.Value, Label5.Text)
                    .RefreshContent()
                    .BringToFront()
                End With



            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Public Function GetStoreProcedureName(ByVal ColumnNumber As Integer) As String

        Try
            Select Case ColumnNumber


                Case ROW_EPrescription
                    Return "MU_ePrescribing_Stage3"

                Case ROW_PatientElectronicAccess_Measure1
                    Return "MU_PatientElectronicAccess_Measure1_ACI"

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
                    Return "MU_SubmitImmunization_Stage3"

                Case ROW_PatientGeneratedHealthData
                    Return "MU_PatientGeneratedHealthData_Stage3"

                Case ROW_PatientRequeustOrAccept
                    Return "MU_RequestAcceptPatientCareRecord_Stage3"

                Case Else
                    Return ""

            End Select


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        End Try


    End Function


    Private Sub C1QualityMeasures_SetupEditor(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.SetupEditor
        If e.Col = COL_rptNumerator OrElse e.Col = COL_rptDenominator Then
            If Not IsNothing(C1QualityMeasures.Editor) Then
                Dim txt As TextBox = DirectCast(C1QualityMeasures.Editor, TextBox)
                txt.MaxLength = Convert.ToString(Int64.MaxValue).Length - 1
            End If

        End If
    End Sub

    Private Sub lblBaseScore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnbrprov_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrprov.Click
        pnlcustomTask.Height = 225
        pnlcustomTask.Width = 330
        LoadUserGrid()

        SetCheckValues(CType(cmb_Provider.DataSource, DataTable))
        pnlcustomTask.BringToFront()
    End Sub
    Private Sub LoadUserGrid()
        Try
            AddControl()
            If (dgCustomGrid IsNot Nothing) Then
                dgCustomGrid.Visible = True
                dgCustomGrid.SetSelectAllVisible = False
                dgCustomGrid.Width = pnlcustomTask.Width
                dgCustomGrid.Height = pnlcustomTask.Height
                dgCustomGrid.C1Grid.AllowEditing = False
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Visible = True
                dgCustomGrid.Selectsearch(gloUserControlLibrary.CustomTask.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub AddControl()
        If (dgCustomGrid IsNot Nothing) Then
            RemoveControl()
        End If

        dgCustomGrid = New gloUserControlLibrary.CustomTask()
        dgCustomGrid.Dock = DockStyle.Fill
        dgCustomGrid.Visible = True
        AddHandler dgCustomGrid.OKClick, AddressOf dgCustomGrid_OKClick
        AddHandler dgCustomGrid.CloseClick, AddressOf dgCustomGrid_CloseClick
        AddHandler dgCustomGrid.SearchChanged, AddressOf dgCustomGrid_SearchChanged
        AddHandler dgCustomGrid.MouseMoveClick, AddressOf dgCustomGrid_MouseMove
        pnlcustomTask.Controls.Add(dgCustomGrid)

        Dim y As Integer = 0
        Dim x As Integer = 0


        x = cmb_Provider.Location.X
        y = cmb_Provider.Location.Y
        dgCustomGrid.SetSearchTextWidth = 250

        pnlcustomTask.Location = New Point(x, y)
        pnlcustomTask.Visible = True
        pnlcustomTask.BringToFront()
        dgCustomGrid.BringToFront()
    End Sub
    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView = DirectCast(dgCustomGrid.GridDatasource, DataView)
            If dvPatient Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Return
            End If

            Dim strPatientSearchDetails As String = ""
            If dgCustomGrid.SearchText.ToString().Trim() <> "" Then
                strPatientSearchDetails = dgCustomGrid.SearchText.ToString().Replace("'", "''")
                strPatientSearchDetails = strPatientSearchDetails.Replace("[", "") + ""
                strPatientSearchDetails = ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If


            dvPatient.RowFilter = (Convert.ToString("[" + dvPatient.Table.Columns(1).ColumnName + "]" + " Like '") & strPatientSearchDetails) + "%'  "


            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.[Default]


            'dgCustomGrid.C1Grid.Cols(1).Width = 0
            'Else
            If dgCustomGrid.C1Task.Cols.Count > 2 Then
                dgCustomGrid.C1Task.Cols(1).Visible = False
            End If
            ' End If
            dgCustomGrid.Selectsearch(gloUserControlLibrary.CustomTask.enmcontrol.Search)
        Catch objErr As Exception
            Me.Cursor = Cursors.[Default]
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.[Select], objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString(), _MessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try
            strSpecialChar = strSpecialChar.Replace("#", "[#]") + ""
            strSpecialChar = strSpecialChar.Replace("$", "[$]") + ""
            strSpecialChar = strSpecialChar.Replace("%", "[%]") + ""
            strSpecialChar = strSpecialChar.Replace("^", "[^]") + ""
            strSpecialChar = strSpecialChar.Replace("&", "[&]") + ""
            strSpecialChar = strSpecialChar.Replace("~", "[~]") + ""
            strSpecialChar = strSpecialChar.Replace("!", "[!]") + ""
            strSpecialChar = strSpecialChar.Replace("*", "[*]") + ""
            strSpecialChar = strSpecialChar.Replace(";", "[;]") + ""
            strSpecialChar = strSpecialChar.Replace("/", "[/]") + ""
            strSpecialChar = strSpecialChar.Replace("?", "[?]") + ""
            strSpecialChar = strSpecialChar.Replace(">", "[>]") + ""
            strSpecialChar = strSpecialChar.Replace("<", "[<]") + ""
            strSpecialChar = strSpecialChar.Replace("\", "[\]") + ""
            strSpecialChar = strSpecialChar.Replace("|", "[|]") + ""
            strSpecialChar = strSpecialChar.Replace("{", "[{]") + ""
            strSpecialChar = strSpecialChar.Replace("}", "[}]") + ""
            strSpecialChar = strSpecialChar.Replace("-", "[-]") + ""
            strSpecialChar = strSpecialChar.Replace("_", "[_]") + ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
        Return strSpecialChar
    End Function

    Private Sub BindUserGrid()

        Try

            Dim dt As DataTable = FillCustomGridProvider()
            Dim col As New DataColumn()

            CustomDrugsGridStyle()

            col.ColumnName = "Select"
            col.DataType = System.Type.[GetType]("System.Boolean")
            col.DefaultValue = False

            If Not (dt.Columns.Contains(col.ColumnName)) Then
                dt.Columns.Add(col)
            End If

            Dim dvData As New DataView(dt)

            If (dt IsNot Nothing) Then
                dgCustomGrid.datasource(dvData)
            End If

            'RESET THE GRID
            Dim _TotalWidth As Single = dgCustomGrid.Gridwidth - 5
            dgCustomGrid.C1Grid.Cols.Move(dgCustomGrid.C1Grid.Cols.Count - 1, 0)
            dgCustomGrid.C1Grid.AllowEditing = True

            If dgCustomGrid.C1Grid.Cols.Count > 0 Then
                dgCustomGrid.C1Grid.Cols(0).AllowSorting = False
            End If

            dgCustomGrid.C1Grid.Cols("ProviderName").AllowEditing = False

            dgCustomGrid.C1Grid.Cols(1).Width = 0
            dgCustomGrid.C1Grid.Cols(1).Visible = False

            'dgCustomGrid.C1Grid.Cols("sEmployerID").Width = 0
            'dgCustomGrid.C1Grid.Cols("sEmployerID").Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As EventArgs)
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub

    Dim dtprov As DataTable = Nothing


    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim dtProvider As DataTable = Nothing

        Try
            dgCustomGrid.SearchText = ""

            If dgCustomGrid.C1Task.Rows.Count > 1 Then
                If (Not dtprov Is Nothing) Then
                    dtprov = CType(cmb_Provider.DataSource, DataTable)
                Else
                    dtprov = New DataTable
                    dtprov.Columns.Clear()
                    dtprov.Columns.Add("nProviderId")
                    dtprov.Columns.Add("ProviderName")
                End If

                dtprov.Rows.Clear()

                '  dTIN.Clear()

                Dim rowcnt As Integer = dgCustomGrid.GetTotalRows
                For j = 1 To rowcnt - 1
                    If (Convert.ToBoolean(dgCustomGrid.GetItem(j, 0))) Then
                        Dim dr As DataRow = dtprov.NewRow()
                        dr("nProviderId") = dgCustomGrid.GetItem(j, 1).ToString()
                        dr("ProviderName") = dgCustomGrid.GetItem(j, 2).ToString()
                        '   dTIN.Add(Convert.ToInt64(dgCustomGrid.GetItem(j, 1)), dgCustomGrid.GetItem(j, 3).ToString())
                        dtprov.Rows.Add(dr)
                    End If
                Next
                If (Not dtprov Is Nothing) Then
                    cmb_Provider.DataSource = dtprov
                    cmb_Provider.ValueMember = "nProviderId"
                    cmb_Provider.DisplayMember = "ProviderName"
                End If

                If cmb_Provider.SelectedIndex = -1 AndAlso cmb_Provider.Items.Count > 0 Then
                    cmb_Provider.SelectedIndex = 0
                End If

            End If


            pnlcustomTask.Visible = False
            dgCustomGrid.Visible = False
            ShowTaxID(cmb_Provider.DataSource)
            'GetValues()
            'FillData()

        Catch ex As Exception
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally

        End Try

    End Sub

    Private Sub dgCustomGrid_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub
    Public Sub CustomDrugsGridStyle()
        Dim _TotalWidth As Single = dgCustomGrid.C1Grid.Width - 5
        dgCustomGrid.C1Grid.Cols.Fixed = 0
        dgCustomGrid.C1Grid.Rows.Fixed = 1
        dgCustomGrid.C1Grid.Cols.Count = 4
        dgCustomGrid.C1Grid.AllowEditing = True
        dgCustomGrid.C1Grid.SetData(0, COL_Check, "Select")
        dgCustomGrid.C1Grid.SetData(0, 1, "Name")
        dgCustomGrid.C1Grid.SetData(0, 2, "NDCCode")
        dgCustomGrid.C1Grid.Cols(3).Width = 0
        dgCustomGrid.Height = 400
        dgCustomGrid.Width = 500
    End Sub

    Private Sub RemoveControl()
        If (dgCustomGrid IsNot Nothing) Then
            If pnlcustomTask.Controls.Contains(dgCustomGrid) Then
                pnlcustomTask.Controls.Remove(dgCustomGrid)
            End If
            dgCustomGrid.Visible = False
            Try
                RemoveHandler dgCustomGrid.OKClick, AddressOf dgCustomGrid_OKClick
                RemoveHandler dgCustomGrid.CloseClick, AddressOf dgCustomGrid_CloseClick
                RemoveHandler dgCustomGrid.SearchChanged, AddressOf dgCustomGrid_SearchChanged
                RemoveHandler dgCustomGrid.MouseMoveClick, AddressOf dgCustomGrid_MouseMove
                dgCustomGrid.Dispose()
                dgCustomGrid = Nothing
            Catch
            End Try
            dgCustomGrid = Nothing
        End If
    End Sub
    Private Sub SetCheckValues(ByVal dtdata As DataTable)
        Dim cnt As Integer = 0
        Dim lstitem As Integer = 0

        If IsNothing(dtdata) OrElse dtdata.Rows.Count = 0 Then

            For cnt = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next


        Else
            ' For lstitem = 0 To lst.Items.Count - 1
            For cnt = 1 To dgCustomGrid.C1Task.Rows.Count - 1
                Try


                    Dim drdata As DataRow() = dtdata.Select("nProviderid='" & dgCustomGrid.GetItem(cnt, 1).ToString().Trim() & "'")
                    If (drdata.Length > 0) Then
                        dgCustomGrid.C1Task.SetCellCheck(cnt, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
            ' Next
        End If
    End Sub

    Private Sub btnClearProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearProvider.Click
        Try


            If (Not IsNothing(cmb_Provider.DataSource)) Then
                Dim dt As DataTable = cmb_Provider.DataSource
                Dim dr As DataRow() = dt.Select("nProviderID='" & cmb_Provider.SelectedValue & "'")
                If (dr.Length > 0) Then
                    '  If dTIN.ContainsKey(Convert.ToInt64(cmb_Provider.SelectedValue)) Then
                    'dTIN.Remove(Convert.ToInt64(cmb_Provider.SelectedValue))
                    '  End If

                    dt.Rows.Remove(dr(0))
                    cmb_Provider.DataSource = dt
                End If
            End If

            If (Not IsNothing(cmb_Provider.DataSource)) Then
                ShowTaxID(cmb_Provider.DataSource)
                'GetValues()
                'FillData()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub cmb_taxid_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_taxid.SelectionChangeCommitted
        Try
            If blnIsLoading = False Then

                ' GetValues()
                'SetGridStyle()
                ' FillData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub immcomboindexchanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim intimmu As Single = 0
            If (Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator)) <> DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString()) Then
                C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_calNumerator, DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString())
                Dim strnumvalue As String = Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calNumerator))
                Dim strdenomvalue As String = Convert.ToString(C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_calDenominator))
                If (strnumvalue.Contains("Active") AndAlso strdenomvalue.Trim().Length > 2) Then
                    C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_PerformancePoints, "10")
                    intimmu = 10
                Else
                    If (C1QualityMeasures.GetData(ROW_ImmunizationRegistry, COL_PerformancePoints) = "10") Then
                        intimmu = -10
                    Else
                        intimmu = 0
                    End If
                    C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_PerformancePoints, "0")
                End If

                Dim perc As Single = FormatNumber(lblPerformanceScoreData.Text, 2, TriState.True)

                perc += Convert.ToSingle(intimmu)
                lblPerformanceScoreData.Text = FormatNumber(perc, 2, TriState.True)
                If chkMIPSAPM.Checked = True Then
                    SetScoreValues(perc, True)
                Else
                    SetScoreValues(perc)
                End If


            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub syncomboindexchanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_calNumerator, DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString())

        Catch ex As Exception

        End Try

    End Sub
    Private Sub numcomboselectedChange(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString() = "Yes" Then
                C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_calNumerator, DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString())
                If C1QualityMeasures.GetData(ROW_EPrescription, COL_calNumerator) > 0 AndAlso C1QualityMeasures.GetData(ROW_PatientElectronicAccess_Measure1, COL_calNumerator) > 0 AndAlso C1QualityMeasures.GetData(ROW_SummaryOfCare, COL_calNumerator) > 0 AndAlso C1QualityMeasures.GetData(ROW_PatientRequeustOrAccept, COL_calNumerator) > 0 Then
                    lblBaseScoreData.Text = "50"
                Else
                    lblBaseScoreData.Text = "0"
                End If

                SetBaseScoreFlag()


                Performance = Convert.ToDecimal(lblPerformanceScoreData.Text)
                If chkMIPSAPM.Checked Then
                    SetScoreValues(Performance, True)
                Else
                    SetScoreValues(Performance)
                End If



            ElseIf DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString() = "No" Or DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString() = "" Then
                C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_calNumerator, DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem.ToString())

                lblBaseScoreData.Text = "0"

                SetBaseScoreFlag()
                Performance = Convert.ToDecimal(lblPerformanceScoreData.Text)
                If chkMIPSAPM.Checked Then
                    SetScoreValues(Performance, True)
                Else
                    SetScoreValues(Performance)
                End If



            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub

    Private Sub chkMIPSAPM_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMIPSAPM.CheckedChanged
        Dim Performance As Decimal = 0
        Performance = Convert.ToDecimal(FormatNumber(lblPerformanceScoreData.Text.Trim, 2, TriState.True))

        If chkMIPSAPM.Checked = True Then
            'PreviousACIScore = lblACICatValue.Text

            SetScoreValues(Performance, True)
        Else
            SetScoreValues(Performance, False)
            'lblACICatValue.Text = PreviousACIScore
        End If
    End Sub
    Private Function CalculatePerformance(ByVal percent As Decimal, ByVal PerfromanceScoreWeight As Int16) As Decimal
        Dim performance As Decimal
        performance = (percent * PerfromanceScoreWeight) / 100
        '  Return Math.Round(performance, 0, MidpointRounding.AwayFromZero)
        Return Math.Ceiling(performance)
    End Function
    Private Sub SetBaseScoreFlag()
        If C1QualityMeasures.GetData(ROW_EPrescription, COL_calNumerator) > 0 Then
            C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_Base, ImgFlag.Images(3))
        Else
            C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_Base, ImgFlag.Images(6))
        End If
        If C1QualityMeasures.GetData(ROW_PatientElectronicAccess_Measure1, COL_calNumerator) > 0 Then
            C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_Base, ImgFlag.Images(3))
        Else
            C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_Base, ImgFlag.Images(6))

        End If
        If Convert.ToString(C1QualityMeasures.GetData(ROW_ProtectElectronicHealthInformation, COL_calNumerator)) = "Yes" Then
            C1QualityMeasures.SetCellImage(ROW_ProtectElectronicHealthInformation, COL_Base, ImgFlag.Images(3))
        Else
            C1QualityMeasures.SetCellImage(ROW_ProtectElectronicHealthInformation, COL_Base, ImgFlag.Images(6))
        End If
        If C1QualityMeasures.GetData(ROW_SummaryOfCare, COL_calNumerator) > 0 Then
            C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_Base, ImgFlag.Images(3))
        Else
            C1QualityMeasures.SetCellImage(ROW_SummaryOfCare, COL_Base, ImgFlag.Images(6))
        End If

        If C1QualityMeasures.GetData(ROW_PatientRequeustOrAccept, COL_calNumerator) > 0 Then
            C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_Base, ImgFlag.Images(3))
        Else
            C1QualityMeasures.SetCellImage(ROW_PatientRequeustOrAccept, COL_Base, ImgFlag.Images(6))
        End If

    End Sub

    Private sClinicCode As String
    Public Property ClinicCode() As String
        Get
            Return sClinicCode
        End Get
        Set(ByVal value As String)
            sClinicCode = value
        End Set
    End Property

    Private Function ValidateReport() As Boolean
        Dim bReturned As Boolean = True

        If txtReportName.Text.Trim() = "" Then
            MessageBox.Show("Please enter a report name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            bReturned = False
        End If

        If bReturned Then
            If cmb_Provider.Items.Count = 0 Then
                MessageBox.Show("Please Calculate all measures before Exporting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                bReturned = False
            End If

            If bReturned = True Then
                If (IsNothing(dtCount) = False AndAlso dtCount.Rows.Count > 0) Then
                    bReturned = True
                Else
                    MessageBox.Show("Please Calculate all measures before Exporting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    bReturned = False
                End If
            End If            
        End If

        Return bReturned
    End Function

    Private Function GenerateQRDAIII(ByVal FilePath As String) As Boolean
        Dim bReturned As Boolean = False

        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        Dim mStrXMLfile As String = ""
        Dim objgloCDAWriter As gloCDAWriter = Nothing
        Dim oPatient As gloCCDLibrary.Patient = Nothing
        Dim dsClinicnProvider As DataSet = Nothing
        Dim mPatient As gloCCDLibrary.Patient = Nothing
        Dim dtProviders As DataTable

        Try

            oPatient = New gloCCDLibrary.Patient
            mPatient = New gloCCDLibrary.Patient
            mPatient = oPatient

            oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
            objgloCDAWriter = New gloCDAWriter
            objgloCDAWriter.nQRDAFileType = gloCCDSchema.CDAFileTypeEnum.QRDA3
            objgloCDAWriter.MeasurementStartDate = dtpicStartDate.Value.ToString()
            objgloCDAWriter.MeasurementEndDate = dtpicEndDate.Value.ToString()
            objgloCDAWriter.dtMeasures = dtCount
            objgloCDAWriter.IsACI = True

            dtProviders = CType(cmb_Provider.DataSource, DataTable).Copy

            If IsNothing(dtProviders.Columns("ProviderName")) = False Then
                dtProviders.Columns.Remove("ProviderName")
            End If

            If IsNothing(dtProviders.Columns("nProviderID")) = False Then
                dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
            End If

            dsClinicnProvider = oCDADataExtraction.GetQRDAIIIInformation(dtProviders)

            mPatient.Clinic = oCDADataExtraction.GetClinicInfo(dsClinicnProvider.Tables("Clinic"))
            mPatient.PatientProviders = oCDADataExtraction.GetPatientProviderInfo(dsClinicnProvider.Tables("Provider"))
            objgloCDAWriter.mPatient = mPatient
            If Not IsNothing(dtProviders) Then
                If dtProviders.Rows.Count > 1 Then
                    objgloCDAWriter.IsGroupReporting = True
                Else
                    objgloCDAWriter.IsGroupReporting = False
                End If
            Else
                objgloCDAWriter.IsGroupReporting = False
            End If


            mStrXMLfile = objgloCDAWriter.GenerateQRDAIII(FilePath, "")
            bReturned = True            
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportReport, "QRDA Category III file exported successfully", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(oCDADataExtraction) Then
                oCDADataExtraction.Dispose()
                oCDADataExtraction = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(oPatient) Then
                oPatient.Dispose()
                oPatient = Nothing
            End If
            If Not IsNothing(dsClinicnProvider) Then
                dsClinicnProvider.Dispose()
                dsClinicnProvider = Nothing
            End If
            If Not IsNothing(mPatient) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
        End Try

        Return bReturned
    End Function

    Private Sub ExportQRDAIII()
        Dim strFilePath As String = ""

        Try
            If ValidateReport() Then
                SaveFileDialog1.FileName = Nothing
                SaveFileDialog1.Filter = "XML Files (*.xml)|*.xml|XSL Files (*.xsl)|*.xsl|All files(*.*)|*.*"
                SaveFileDialog1.OverwritePrompt = True

                If SaveFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    strFilePath = SaveFileDialog1.FileName

                    If Me.GenerateQRDAIII(strFilePath) Then
                        MessageBox.Show("QRDA Category III file exported successfully. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
        End Try
    End Sub
End Class
