Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms


Public Class frm_MU_Dashboard_Stage2

    Private COL_Check As Integer = 0
    Private COL_Measure As Integer = 1
    Private COL_Goal As Integer = 2
    Private COL_Comment As Integer = 3

    Private COL_calNumerator As Integer = 4
    Private COL_calDenominator As Integer = 5

    Private COL_calPercent As Integer = 6
    Private COL_calFlag1 As Integer = 7
    Private COL_calFlag2 As Integer = 8
    Private COL_Copybtn As Integer = 9

    Private COL_rptNumerator As Integer = 10
    Private COL_rptDenominator As Integer = 11

    Private COL_rptPercent As Integer = 12
    Private COL_rptFlag1 As Integer = 13
    Private COL_rptFlag2 As Integer = 14


    Private ROW_CPOEMainMeasure As Integer = 1
    Private ROW_CPOEMedication As Integer = 2
    Private ROW_CPOERadiology As Integer = 3
    Private ROW_CPOELabs As Integer = 4

    Private ROW_EPrescription As Integer = 5 '4
    Private ROW_Demographic As Integer = 6 '7
    Private ROW_Vitals As Integer = 7 '8
    Private ROW_SmokingStatus As Integer = 8 '9

    Private ROW_ClinicalDecisionSupportRule As Integer = 9 '14
    Private ROW_ClinicalDecisionSupport As Integer = 10 '14
    Private ROW_ClinicalDecisionDrugInteractionCheck As Integer = 11 '4'2

    Private ROW_PatientElectronicAccess As Integer = 12
    Private ROW_PatientElectronicAccess_Measure1 As Integer = 13
    Private ROW_PatientElectronicAccess_Measure2 As Integer = 14

    Private ROW_ClinicalSummaryforPatient As Integer = 15
    Private ROW_ProtectElectronicHealthInformation As Integer = 16
    Private ROW_ClinicalLabTestResults As Integer = 17
    Private ROW_GenerateListOfPatient As Integer = 18
    Private ROW_PreventiveCare As Integer = 19
    Private ROW_PatientSpecificEducation As Integer = 20
    Private ROW_MedicalReconcilation As Integer = 21
    Private ROW_SummaryOfCare As Integer = 22
    Private ROW_SummaryOfCare_Measure1 As Integer = 23 '3
    Private ROW_SummaryOfCare_Measure2 As Integer = 24 '5
    Private ROW_SummaryOfCare_Measure3 As Integer = 25
    Private ROW_ImmunizationRegistry As Integer = 26 '30
    Private ROW_SecureElectronicMessaging As Integer = 27 '30

    Private ROW_MenuSet As Integer = 28 ' 24


    'Menu Set Objectives
    Private ROW_SyndromicServilance As Integer = 29
    Private ROW_ElectronicNotes As Integer = 30
    Private ROW_ImagingResults As Integer = 31
    Private ROW_FamilyHealthHistory As Integer = 32

   


    Private ProviderID As Int64
    Private StartDate As String
    Private Enddate As String
    Private Percent As String
    Private nReportId As Int64 = 0
    Private nId As Int64
    Private Exception As Int64 = 0

    Private blnIsLoading As Boolean = False

    'Dim btn As New Button

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
    Public mycaller As frm_MU_ViewDashboard_Stage2
    Private dtReportingPeriod As DataTable

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
            'C1QualityMeasures.Rows.Add()
            C1QualityMeasures.Cols.Count = 15
            'C1QualityMeasures.Cols.Fixed = 11
            'C1QualityMeasures.Rows.Add()
            'C1QualityMeasures.ScrollBars = ScrollBars.None
            C1QualityMeasures.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1QualityMeasures.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            gloC1FlexStyle.Style(C1QualityMeasures)
            C1QualityMeasures.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free

            Dim rg As C1.Win.C1FlexGrid.CellRange
            rg = C1QualityMeasures.GetCellRange(0, COL_calFlag1, 0, COL_calFlag2)
            'C1QualityMeasur()

            C1QualityMeasures(0, COL_Check) = "Select"
            C1QualityMeasures(0, COL_calFlag1) = "Flag"
            C1QualityMeasures(0, COL_calFlag2) = "Flag"
            C1QualityMeasures.SetData(0, COL_Measure, "Core Measures")
            C1QualityMeasures.SetData(0, COL_Goal, "Goal%")
            C1QualityMeasures.SetData(0, COL_calNumerator, "Numerator")
            C1QualityMeasures.SetData(0, COL_calDenominator, "Denominator")
            C1QualityMeasures.SetData(0, COL_calPercent, "Percent")

            'rg = C1QualityMeasures.GetCellRange(0, COL_Copybtn, 0, COL_Copybtn)
            'rg.StyleNew.ComboList = "..."
            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            'C1QualityMeasures.SetCellImage(0,COL_Copybtn
            'C1QualityMeasures(0, COL_calFlag2) = "Flag"

            'C1QualityMeasures.SetData(0, COL_calFlag1, "Flag")
            C1QualityMeasures.SetData(0, COL_rptNumerator, "Numerator")
            C1QualityMeasures.SetData(0, COL_rptDenominator, "Denominator")
            C1QualityMeasures.SetData(0, COL_rptPercent, "Percent")
            C1QualityMeasures.SetData(0, COL_rptFlag1, "Flag")
            C1QualityMeasures.SetData(0, COL_rptFlag2, "Flag")


            C1QualityMeasures.Cols(COL_Check).ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
            C1QualityMeasures.Cols(COL_Measure).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1QualityMeasures.Cols(COL_Goal).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            C1QualityMeasures.Cols(COL_Comment).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
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

            ' ''Width
            C1QualityMeasures.Cols(COL_Check).Width = Width * 0.04
            C1QualityMeasures.Cols(COL_Measure).Width = Width * 0.27
            C1QualityMeasures.Cols(COL_Goal).Width = Width * 0.05
            C1QualityMeasures.Cols(COL_Comment).Width = 18

            C1QualityMeasures.Cols(COL_Comment).AllowResizing = False

            C1QualityMeasures.Cols(COL_calNumerator).Width = Width * 0.067
            C1QualityMeasures.Cols(COL_calDenominator).Width = Width * 0.074
            C1QualityMeasures.Cols(COL_calPercent).Width = Width * 0.06
            C1QualityMeasures.Cols(COL_calFlag1).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_calFlag2).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_Copybtn).Width = Width * 0.05
            C1QualityMeasures.Cols(COL_rptNumerator).Width = Width * 0.067
            C1QualityMeasures.Cols(COL_rptDenominator).Width = Width * 0.074
            C1QualityMeasures.Cols(COL_rptPercent).Width = Width * 0.06
            C1QualityMeasures.Cols(COL_rptFlag1).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_rptFlag2).Width = Width * 0.02

            C1QualityMeasures.ExtendLastCol = False



            ' ''Editing
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




            Dim cs As C1.Win.C1FlexGrid.CellStyle '= C1QualityMeasures.Styles.Add("cs_editableChekcbox")

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
            C1QualityMeasures.Rows.Add(32)

            FillMeasures()



        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillData()

        Me.Cursor = Cursors.WaitCursor

        Try
            Dim dt As New DataTable

            'CPOE
            'Measure 1
            dt = GetdataWithParam("MU_CPOE_Medication_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)

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
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_calFlag2, Nothing)
                End If

            End If


            'Radiology
            dt = GetdataWithParam("MU_CPOE_Radiology_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)

            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_CPOERadiology, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_CPOERadiology, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 30 Then
                        C1QualityMeasures.SetCellImage(ROW_CPOERadiology, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_CPOERadiology, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_CPOERadiology, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_CPOERadiology, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_CPOERadiology, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_CPOERadiology, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_CPOERadiology, COL_calFlag2, Nothing)
                End If
            End If


            'Laboratory
            dt = GetdataWithParam("MU_CPOE_Laboratory_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)

            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_CPOELabs, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_CPOELabs, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 30 Then
                        C1QualityMeasures.SetCellImage(ROW_CPOELabs, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_CPOELabs, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_CPOELabs, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_CPOELabs, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_CPOELabs, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_CPOELabs, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_CPOELabs, COL_calFlag2, Nothing)
                End If
            End If


            'Measure 2
            dt = GetdataWithParam("MU_ePrescribedReport_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)
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

            'Measure 3
            dt = GetdataWithParam("MU_Demographics_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_Demographic, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_Demographic, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 80 Then
                        C1QualityMeasures.SetCellImage(ROW_Demographic, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_Demographic, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_Demographic, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_Demographic, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_Demographic, COL_calFlag1, Nothing)
                End If

            End If


            'Measure 4
            dt = GetdataWithParam("MU_Vital_Stage2", ProviderID.ToString(), StartDate, Enddate)


            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_Vitals, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_Vitals, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 80 Then
                        C1QualityMeasures.SetCellImage(ROW_Vitals, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_Vitals, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_Vitals, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_Vitals, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_Vitals, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_Vitals, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_Vitals, COL_calFlag2, Nothing)
                End If

            End If


            'Measure 5
            dt = GetdataWithParam("MU_SmokingStatus_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_SmokingStatus, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SmokingStatus, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 80 Then
                        C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_SmokingStatus, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_SmokingStatus, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_calFlag2, Nothing)
                End If

            End If


            'Measure 6

            dt = Getdata("MU_DIChecks_Stage2")
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0).ToString() = "1" Then
                    C1QualityMeasures.SetData(ROW_ClinicalDecisionDrugInteractionCheck, COL_calNumerator, "Enabled")
                Else
                    C1QualityMeasures.SetData(ROW_ClinicalDecisionDrugInteractionCheck, COL_calNumerator, "Disabled")
                End If
            End If


            dt = Getdata("MU_DMRulesSetup_Stage2")
            If dt.Rows.Count > 0 Then
                C1QualityMeasures.SetData(ROW_ClinicalDecisionSupport, COL_calNumerator, "DM Rules: " & dt.Rows(0)(0).ToString())
            End If


            'Measure 7
            dt = GetdataWithParam("MU_PatientElectronicAccess_Measure1_Stage1", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 50 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure1, COL_calFlag1, Nothing)
                End If

            End If


            'Measure 7
            dt = GetdataWithParam("MU_PatientElectronicAccess_Measure2_Stage1", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 5 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_PatientElectronicAccess_Measure2, COL_calFlag1, Nothing)
                End If

            End If

            'Measure 8
            dt = GetdataWithParam("MU_ClinicalSummaryUsage_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 50 Then
                        C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_calFlag2, Nothing)
                End If

            End If


            'Measure 9 Not Created

            'Measure 10
            dt = GetdataWithParam("MU_ClinicalLabTestResults_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                If IsNothing(dt) = False Then
                    Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_calNumerator, dt.Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_calDenominator, dt.Rows(0)(1).ToString())
                    If Percent <> "N/A" Then
                        If Percent <= 55 Then
                            C1QualityMeasures.SetCellImage(ROW_ClinicalLabTestResults, COL_calFlag1, ImgFlag.Images(2))
                        Else
                            C1QualityMeasures.SetCellImage(ROW_ClinicalLabTestResults, COL_calFlag1, Nothing)
                        End If
                        C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    Else
                        C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_calPercent, Percent)
                        C1QualityMeasures.SetCellImage(ROW_ClinicalLabTestResults, COL_calFlag1, Nothing)
                    End If
                    If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                        C1QualityMeasures.SetCellImage(ROW_ClinicalLabTestResults, COL_calFlag2, ImgFlag.Images(0))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ClinicalLabTestResults, COL_calFlag2, Nothing)
                    End If
                End If

            End If


            'Measure 11
            dt = GeneratePatientListCreated(cmb_Provider.SelectedValue)

            Dim dtDate As New DateTime
            If (dt.Rows.Count > 0) Then
                dtDate = dt.Rows(0)(0).ToString()
                C1QualityMeasures.SetData(ROW_GenerateListOfPatient, COL_calNumerator, dtDate.ToShortDateString())
            Else
                C1QualityMeasures.SetData(ROW_GenerateListOfPatient, COL_calNumerator, "None")
            End If




            'Measure 12
            dt = GetdataWithParam("MU_PatientReminderUsage_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PreventiveCare, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PreventiveCare, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 10 Then
                        C1QualityMeasures.SetCellImage(ROW_PreventiveCare, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PreventiveCare, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PreventiveCare, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_PreventiveCare, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_PreventiveCare, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_PreventiveCare, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_PreventiveCare, COL_calFlag2, Nothing)
                End If

            End If

            'Measure 13
            dt = GetdataWithParam("MU_PatientSpecificEducation_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 10 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducation, COL_calFlag1, Nothing)
                End If

            End If


            'Measure 14
            dt = GetdataWithParam("MU_Medication_Reconciliation_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 50 Then
                        C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_calFlag2, Nothing)
                End If

            End If


            'Measure 15
            dt = GetdataWithParam("MU_SummaryofCare_Measure3_Stage2", ProviderID.ToString(), StartDate, Enddate)


            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0)("Numerator") > 0) Then
                    'C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure3, COL_calNumerator, dt.Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure3, COL_calNumerator, "Yes")
                Else
                    C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure3, COL_calNumerator, "No")
                End If
            End If

            If Convert.ToInt64(dt.Rows(0)("Exception").ToString()) = 1 Then
                C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure3, COL_calFlag2, ImgFlag.Images(0))
            Else
                C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure3, COL_calFlag2, Nothing)
            End If

            dt = GetdataWithParam("MU_SummaryofCare_Measure2_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 10 Then
                        C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure2, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure2, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure2, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure2, COL_calFlag2, Nothing)
                End If

            End If



            dt = GetdataWithParam("MU_SummaryofCare_Measure1_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 50 Then
                        C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure1, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure1, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_SummaryOfCare_Measure1, COL_calFlag1, Nothing)
                End If
            End If


            'Measure 16
            dt = GetdataWithParam("MU_SubmitImmunization_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)

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


            'Measure 17
            dt = GetdataWithParam("MU_SecureElectronicMessaging_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent <= 5 Then
                        C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_SecureElectronicMessaging, COL_calFlag1, Nothing)
                End If

            End If



            dt = GetdataWithParam("MU_ElectronicNotes_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 30 Then
                        C1QualityMeasures.SetCellImage(ROW_ElectronicNotes, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ElectronicNotes, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ElectronicNotes, COL_calFlag1, Nothing)
                End If
            End If

            'Aniket: To set the Imaging Results SP here
            dt = GetdataWithParam("MU_ImagingResults_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ImagingResults, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ImagingResults, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 10 Then
                        C1QualityMeasures.SetCellImage(ROW_ImagingResults, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ImagingResults, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ImagingResults, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ImagingResults, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ImagingResults, COL_calFlag1, Nothing)
                End If
            End If


            dt = GetdataWithParam("MU_FamilyHistory_Stage2", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent <= 20 Then
                        C1QualityMeasures.SetCellImage(ROW_FamilyHealthHistory, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_FamilyHealthHistory, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_FamilyHealthHistory, COL_calFlag1, Nothing)
                End If
            End If




            dt = GetdataWithParam("MU_SubmitsurveillanceData_Stage2_Archive", ProviderID.ToString(), StartDate, Enddate)

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

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            'Measure 1
            C1QualityMeasures.Rows(ROW_CPOEMainMeasure).AllowEditing = True
            C1QualityMeasures.SetData(ROW_CPOEMainMeasure, COL_Measure, "CPOE for Medication, Laboratory and Radiology Orders")
            C1QualityMeasures.SetData(ROW_CPOEMainMeasure, COL_Comment, "Use computerized provider order entry (CPOE) for medication, laboratory and radiology orders directly entered by any licensed healthcare professional who can enter orders into the medical record per state, local and professional guidelines.")



            C1QualityMeasures.Rows(ROW_CPOEMedication).AllowEditing = True
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Measure, "Medication Orders")
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Goal, "60%")
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Comment, "More than 60 percent of medication created by the EP during the EHR reporting period are recorded using CPOE.")

            C1QualityMeasures.Rows(ROW_CPOERadiology).AllowEditing = True
            C1QualityMeasures.SetData(ROW_CPOERadiology, COL_Measure, "Radiology Orders")
            C1QualityMeasures.SetData(ROW_CPOERadiology, COL_Goal, "30%")
            C1QualityMeasures.SetData(ROW_CPOERadiology, COL_Comment, "More than 30 percent of radiology orders created by the EP during the EHR reporting period are recorded using CPOE.")

            C1QualityMeasures.Rows(ROW_CPOELabs).AllowEditing = True
            C1QualityMeasures.SetData(ROW_CPOELabs, COL_Measure, "Laboratory Orders")
            C1QualityMeasures.SetData(ROW_CPOELabs, COL_Goal, "30%")
            C1QualityMeasures.SetData(ROW_CPOELabs, COL_Comment, "More than 30 percent of laboratory orders created by the EP during the EHR reporting period are recorded using CPOE.")

            'Measure 2
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Measure, "e-Prescribing (eRx)")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Comment, "Generate and transmit permissible prescriptions electronically (eRx).")


            'Measure 3
            C1QualityMeasures.SetData(ROW_Demographic, COL_Measure, "Record Demographics")
            C1QualityMeasures.SetData(ROW_Demographic, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_Demographic, COL_Comment, "Record the following demographics: preferred language, sex, race, ethnicity, date of birth.")


            'Measure 4
            C1QualityMeasures.SetData(ROW_Vitals, COL_Measure, "Record Vital Signs")
            C1QualityMeasures.SetData(ROW_Vitals, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_Vitals, COL_Comment, "Record and chart changes in the following vital signs: height/length and weight (no age limit); blood pressure (ages 3 and over); calculate and display body mass index (BMI); and plot and display growth charts for patients 0-20 years, including BMI.")


            'Measure 5
            C1QualityMeasures.SetData(ROW_SmokingStatus, COL_Measure, "Record Smoking Status")
            C1QualityMeasures.SetData(ROW_SmokingStatus, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_SmokingStatus, COL_Comment, "Record smoking status for patients 13 years old or older")


            'Measure 6
            C1QualityMeasures.SetData(ROW_ClinicalDecisionSupportRule, COL_Measure, "Clinical Decision Support Rule")
            C1QualityMeasures.Rows(ROW_ClinicalDecisionSupportRule).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ClinicalDecisionSupportRule, COL_Comment, "Use clinical decision support to improve performance on high-priority health conditions.")


            C1QualityMeasures.SetData(ROW_ClinicalDecisionSupport, COL_Measure, "Clinical Decision Support")
            C1QualityMeasures.Rows(ROW_ClinicalDecisionSupport).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ClinicalDecisionSupport, COL_Comment, "The EP has enabled and implemented the functionality for drug-drug and drug-allergy interaction checks for the entire EHR reporting period.")


            C1QualityMeasures.Rows(ROW_ClinicalDecisionDrugInteractionCheck).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ClinicalDecisionDrugInteractionCheck, COL_Measure, "Implement Drug Interaction Checks")
            C1QualityMeasures.SetData(ROW_ClinicalDecisionDrugInteractionCheck, COL_Comment, "Implement Drug Interaction Checks")


            'Measure 7 To Check
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess, COL_Measure, "Patient Electronic Access")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess, COL_Comment, "Provide patients the ability to view online, download and transmit their health information within four business days of the information being available to the EP.")

            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Measure, "Timely online access to health information")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure1, COL_Comment, "More than 50 percent of all unique patients seen by the EP during the EHR reporting period are provided timely (available to the patient within 4 business days after the information is available to the EP) online access to their health information.")

            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Measure, "View, download, or transmit health information")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Goal, "5%")
            C1QualityMeasures.SetData(ROW_PatientElectronicAccess_Measure2, COL_Comment, "More than 5 percent of all unique patients seen by the EP during the EHR reporting period (or their authorized representatives) view, download, or transmit to a third party their health information.")


            'Measure 8
            C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_Measure, "Clinical Summaries")
            C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_Comment, "Provide clinical summaries for patients for each office visit")

            'Measure 9 To Check
            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Measure, "Protect Electronic Health Information")
            C1QualityMeasures.Rows(ROW_ProtectElectronicHealthInformation).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ProtectElectronicHealthInformation, COL_Comment, "Protect electronic health information created or maintained by the certified EHR technology (CEHRT) through the implementation of appropriate technical capabilities.")


            'Measure 10
            C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_Measure, "Clinical Lab-Test Results")
            C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_Goal, "55%")
            C1QualityMeasures.SetData(ROW_ClinicalLabTestResults, COL_Comment, "Incorporate clinical lab-test results into Certified EHR Technology (CEHRT) as structured data")


            'Measure 11
            C1QualityMeasures.SetData(ROW_GenerateListOfPatient, COL_Measure, "Patient Lists")
            C1QualityMeasures.Rows(ROW_GenerateListOfPatient).AllowEditing = True
            C1QualityMeasures.SetData(ROW_GenerateListOfPatient, COL_Comment, "Generate lists of patients by specific conditions to use for quality improvement, reduction of disparities, research or outreach")


            'Measure 12
            C1QualityMeasures.SetData(ROW_PreventiveCare, COL_Measure, "Preventive Care")
            C1QualityMeasures.SetData(ROW_PreventiveCare, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_PreventiveCare, COL_Comment, "Use clinically relevant information to identify patients who should receive reminders for preventive/follow-up care and send these patients the reminders, per patient preference.")


            'Measure 13
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Measure, "Patient-Specific Education Resources")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducation, COL_Comment, "Use clinically relevant information from Certified EHR Technology to identify patient-specific education resources and provide those resources to the patient.")


            'Measure 14
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Measure, "Medication Reconciliation")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Comment, "The EP, eligible hospital or CAH who receives a patient from another setting of care or provider of care or believes an encounter is relevant should perform medication reconciliation")


            'Measure 15
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Measure, "Summary of Care")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_SummaryOfCare, COL_Comment, "The EP who transitions their patient to another setting of care or provider of care or refers their patient to another provider of care should provide summary care record for each transition of care or referral.")


            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_Measure, "Measure 1")
            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure1, COL_Comment, "The EP who transitions or refers their patient to another setting of care or provider of care provides a summary of care record for more than 50 percent of transitions of care and referrals.")

            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_Measure, "Measure 2")
            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure2, COL_Comment, "The EP who transitions or refers their patient to another setting of care or provider of care provides a summary of care record for more than 10 percent of such transitions and referrals either (a) electronically transmitted using CEHRT to a recipient or (b) where the recipient receives the summary of care record via exchange facilitated by an organization that is a NwHIN Exchange participant or in a manner that is consistent with the governance mechanism ONC establishes for the NwHIN.")

            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure3, COL_Measure, "Measure 3")
            'C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure3, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_SummaryOfCare_Measure3, COL_Comment, "An EP must satisfy one of the following criteria:" & vbCrLf & vbCrLf & "Conducts one or more successful electronic exchanges of a summary of care document, as part of which is counted in 'measure 2' (for EPs the measure at §495.6(j)(14)(ii)(B) with a recipient who has EHR technology that was developed designed by a different EHR technology developer than the sender's EHR technology certified to 45 CFR 170.314(b)(2)." & vbCrLf & vbCrLf & "Conducts one or more successful tests with the CMS designated test EHR during the EHR reporting period.")


            'Measure 16
            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Measure, "Immunization Registries Data Submission")
            C1QualityMeasures.Rows(ROW_ImmunizationRegistry).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Comment, "Capability to submit electronic data to immunization registries or immunization information systems except where prohibited, and in accordance with applicable law and practice.")


            'Measure 17
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Measure, "Use Secure Electronic Messaging")
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Goal, "5%")
            C1QualityMeasures.Rows(ROW_SecureElectronicMessaging).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SecureElectronicMessaging, COL_Comment, "Use secure electronic messaging to communicate with patients on relevant health information.")






            'Set the checkboxes
            For intCoreMeasures As Integer = ROW_CPOEMainMeasure To ROW_SecureElectronicMessaging   'ROW_ProtectElectronicHealthInformation

                If intCoreMeasures <> ROW_PatientElectronicAccess_Measure2 And intCoreMeasures <> ROW_PatientElectronicAccess_Measure1 And intCoreMeasures <> ROW_CPOEMedication And intCoreMeasures <> ROW_CPOERadiology And intCoreMeasures <> ROW_CPOELabs And intCoreMeasures <> ROW_ClinicalDecisionSupport And intCoreMeasures <> ROW_ClinicalDecisionDrugInteractionCheck And intCoreMeasures <> ROW_SummaryOfCare_Measure1 And intCoreMeasures <> ROW_SummaryOfCare_Measure2 And intCoreMeasures <> ROW_SummaryOfCare_Measure3 Then
                    C1QualityMeasures.SetCellCheck(intCoreMeasures, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If

                C1QualityMeasures.Rows(intCoreMeasures).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(intCoreMeasures, COL_Comment, ImgFlag.Images(1))

            Next

            ''''''''''''''''''''''''''''''''''''''''''''
            ''''Menu Set Measure
            '''''''''''''''''''''''''''''''''''''''''''''
            'C1QualityMeasures.Rows.Add()

            C1QualityMeasures.Rows(ROW_MenuSet).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
            C1QualityMeasures.Rows(ROW_MenuSet).StyleNew.ForeColor = Color.White
            C1QualityMeasures.Rows(ROW_MenuSet).StyleNew.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            C1QualityMeasures.Rows(ROW_MenuSet).AllowMerging = True
            C1QualityMeasures(ROW_MenuSet, COL_calFlag1) = "Flag"
            C1QualityMeasures(ROW_MenuSet, COL_calFlag2) = "Flag"

            C1QualityMeasures.SetData(ROW_MenuSet, COL_Measure, "Menu Set Measures")
            C1QualityMeasures.SetData(ROW_MenuSet, COL_Goal, "Goal%")
            C1QualityMeasures.SetCellStyle(ROW_MenuSet, COL_Check, New C1.Win.C1FlexGrid.TextAlignEnum)
            C1QualityMeasures.SetData(ROW_MenuSet, COL_calNumerator, "Numerator")
            C1QualityMeasures.SetData(ROW_MenuSet, COL_calDenominator, "Denominator")
            C1QualityMeasures.SetData(ROW_MenuSet, COL_calPercent, "Percent")




            'Measure 1
            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Measure, "Syndromic Surveillance Data Submission")
            C1QualityMeasures.Rows(ROW_SyndromicServilance).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Comment, "Capability to submit electronic syndromic surveillance data to public health agencies except where prohibited, and in accordance with applicable law and practice.")

            'Measure 2
            C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_Measure, "Electronic Notes")
            C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_Goal, "30%")
            C1QualityMeasures.SetData(ROW_ElectronicNotes, COL_Comment, "Enter at least one electronic progress note created, edited and signed by an EP for more than 30 percent of unique patients with at least one office visit during the EHR Measure reporting period. The text of the electronic note must be text searchable and may contain drawings and other content")

            'Measure 3
            C1QualityMeasures.SetData(ROW_ImagingResults, COL_Measure, "Imaging Results")
            C1QualityMeasures.SetData(ROW_ImagingResults, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_ImagingResults, COL_Comment, "Imaging results consisting of the image itself and any explanation or other accompanying information are accessible through CEHRT.")

            'Measure 4
            C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_Measure, "Family Health History")
            C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_Goal, "20%")
            C1QualityMeasures.SetData(ROW_FamilyHealthHistory, COL_Comment, "More than 20 percent of all unique patients seen by the EP during the EHR reporting period have a structured data entry for one or more first-degree relatives.")



            Dim rg As C1.Win.C1FlexGrid.CellRange
            rg = C1QualityMeasures.GetCellRange(ROW_CPOEMedication, COL_Copybtn, ROW_SmokingStatus, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_PatientElectronicAccess_Measure1, COL_Copybtn, ROW_ClinicalSummaryforPatient, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_ClinicalLabTestResults, COL_Copybtn, ROW_ClinicalLabTestResults, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_PreventiveCare, COL_Copybtn, ROW_MedicalReconcilation, COL_Copybtn)
            rg.StyleNew.ComboList = "..."


            rg = C1QualityMeasures.GetCellRange(ROW_SummaryOfCare_Measure1, COL_Copybtn, ROW_SummaryOfCare_Measure2, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_ElectronicNotes, COL_Copybtn, ROW_FamilyHealthHistory, COL_Copybtn)
            rg.StyleNew.ComboList = "..."





            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            For j = ROW_SyndromicServilance To ROW_FamilyHealthHistory
                C1QualityMeasures.Rows(j).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(j, COL_Comment, ImgFlag.Images(1))
                C1QualityMeasures.SetCellCheck(j, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next

            'C1QualityMeasures.Rows(ROW_ProtectElectronicHealthInformation).AllowEditing = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frm_MU_Dashboard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        blnIsLoading = True

        RemoveHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

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

        End If

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

            oDB.Retrive("MU_Select_MainMeasure_MST_Stage2", oParameters, dt)
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

                dt.Clear()


                ''''''''''''''''''''Details Record'''''''''''''''''''''''''''''
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ReportID"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = rptid
                oParameters.Add(oParameter)
                oParameter = Nothing

                oDB.Retrive("MU_Select_MainMeasure_DTL_Stage2", oParameters, dt)
                oParameters.Clear()

                Dim dtrwno As Integer = 0

                If dt.Rows.Count > 0 Then
                    For i = 1 To C1QualityMeasures.Rows.Count - 1
                        If i = ROW_MenuSet Or i = ROW_ClinicalDecisionSupportRule Or i = ROW_SummaryOfCare Or i = ROW_CPOEMainMeasure Or i = ROW_PatientElectronicAccess Then
                            Continue For
                        End If

                        If i >= ROW_SyndromicServilance Then

                            If dt.Rows(dtrwno)(0) = True Then
                                C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            Else
                                C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                            End If

                        End If

                        C1QualityMeasures.SetData(i, COL_Measure, dt.Rows(dtrwno)(1))
                        If i = ROW_ClinicalDecisionDrugInteractionCheck Then
                            'If i = ROW_ClinicalDecisionDrugInteractionCheck Or i = ROW_DrugFormularyCheck Then
                            If Convert.ToString(dt.Rows(dtrwno)(3)) = "1" Then
                                C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
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
                            dtrwno = dtrwno + 1
                            Continue For

                        ElseIf i = ROW_ClinicalDecisionSupport Then
                            C1QualityMeasures.SetData(i, COL_calNumerator, "DM Rules: " & dt.Rows(dtrwno)(3))
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
                            dtrwno = dtrwno + 1
                            Continue For

                        ElseIf i = ROW_GenerateListOfPatient Or i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Or i = ROW_SummaryOfCare_Measure3 Then

                            If i = ROW_SummaryOfCare_Measure3 Then
                                If dt.Rows(dtrwno)(3) = 1 Then
                                    C1QualityMeasures.SetData(i, COL_calNumerator, "Yes")
                                Else
                                    C1QualityMeasures.SetData(i, COL_calNumerator, "No")
                                End If

                            Else
                                If dt.Rows(dtrwno)(3) = 0 Then
                                    C1QualityMeasures.SetData(i, COL_calNumerator, "None")
                                Else
                                    C1QualityMeasures.SetData(i, COL_calNumerator, gloDateMaster.gloDate.DateAsDate(dt.Rows(dtrwno)(3)))
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
                            Else
                                C1QualityMeasures.SetData(i, COL_calPercent, dt.Rows(dtrwno)(5) & "%")
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
                        dtrwno = dtrwno + 1
                    Next

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetValues()

        ProviderID = cmb_Provider.SelectedValue.ToString()

        StartDate = dtpicStartDate.Value.ToShortDateString()
        Enddate = dtpicEndDate.Value.ToShortDateString

        'If chk_FirstYear.Checked = True Then
        '    StartDate = dtpicStartDate.Value.ToShortDateString()
        '    Enddate = dtpicEndDate.Value.ToShortDateString
        'Else
        '    If cmb_RptYear.Text <> "" Then
        '        StartDate = New DateTime(cmb_RptYear.Text, 1, 1).ToShortDateString()
        '        Enddate = New DateTime(cmb_RptYear.Text, 12, 31).ToShortDateString()
        '    Else
        '        StartDate = New DateTime(Date.Now.Year, 1, 1).ToShortDateString()
        '        Enddate = New DateTime(Date.Now.Year, 12, 31).ToShortDateString()
        '    End If
        'End If

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

                If (e.Col <> 0 And e.Col <> COL_Copybtn) And (e.Col <> 4 And e.Row <> ROW_ProtectElectronicHealthInformation) Then
                    If C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col).ToString() = "" Then
                        C1QualityMeasures.SetData(e.Row, COL_rptDenominator, Nothing)
                        C1QualityMeasures.SetData(e.Row, COL_rptNumerator, Nothing)
                        C1QualityMeasures.SetData(e.Row, COL_rptPercent, Nothing)
                        C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag1, Nothing)
                        C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, Nothing)
                    Else

                        If (C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col) < 0) Then
                            C1QualityMeasures.SetData(C1QualityMeasures.Row, C1QualityMeasures.Col, Math.Abs(C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col)))
                        End If
                        If (C1QualityMeasures.GetData(C1QualityMeasures.Row, C1QualityMeasures.Col) = 0) Then
                            'C1QualityMeasures.SetData(C1QualityMeasures.Row, C1QualityMeasures.Col, 1)
                        End If
                        If Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)) <> "" AndAlso Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)) <> "" Then
                            Dim _Deno As Int64 = 0
                            Dim _Num As Int64 = 0
                            _Deno = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)
                            _Num = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)
                            If _Deno >= 0 AndAlso _Num >= 0 Then
                                Dim perc As Single
                                If _Deno = 0 Then
                                    C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, "N/A")
                                    C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)

                                Else
                                    perc = _Num / _Deno * 100
                                    perc = FormatNumber(perc, 2, TriState.True)
                                    C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, perc & "%")

                                    'If Convert.ToInt64(FormatNumber(perc, 0, TriState.UseDefault, TriState.UseDefault, TriState.False)) < Convert.ToInt64(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Goal).ToString().Trim("%")) Then
                                    If Convert.ToSingle(perc) < Convert.ToSingle(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_Goal).ToString().Trim("%")) Then
                                        C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, ImgFlag.Images(2))
                                    Else
                                        C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)
                                    End If
                                End If

                            Else
                                C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)
                                C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, Nothing)
                            End If
                            If _Deno = 0 AndAlso _Num = 0 Then
                                C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, "N/A")
                            End If
                            If Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)) <> "" AndAlso Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)) <> "" Then
                                If _Deno >= 0 AndAlso _Num >= 0 Then
                                    'If e.Row = ROW_Vitals Or e.Row = ROW_SmokingStatus Or e.Row = ROW_EleCopyPatientHealth Or e.Row = ROW_ClinicalSummaryforPatient Or e.Row = ROW_ClinicalLabTestResults Or e.Row = ROW_PreventiveCare Or e.Row = ROW_MedicalReconcilation Or e.Row = ROW_SummaryOfCare Or e.Row = ROW_SummaryCareTransition Then
                                    If e.Row = ROW_Vitals Or e.Row = ROW_SmokingStatus Or e.Row = ROW_ClinicalSummaryforPatient Or e.Row = ROW_ClinicalLabTestResults Or e.Row = ROW_PreventiveCare Or e.Row = ROW_MedicalReconcilation Or e.Row = ROW_SummaryOfCare Then
                                        If _Deno = 0 Then
                                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, ImgFlag.Images(0))
                                        Else
                                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, Nothing)
                                        End If
                                    ElseIf e.Row = ROW_CPOEMedication Then
                                        If _Deno < 100 Then
                                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, ImgFlag.Images(0))
                                        Else
                                            C1QualityMeasures.SetCellImage(e.Row, COL_rptFlag2, Nothing)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.CellButtonClick
        Try
            Dim rowno As Integer
            Dim k As Integer
            Dim Perc As Single
            Dim img As Image
            rowno = C1QualityMeasures.RowSel()

            If rowno = 0 Then
                For k = ROW_CPOEMedication To ROW_ProtectElectronicHealthInformation
                    'If k = ROW_ClinicalDecisionDrugInteractionCheck Or k = ROW_CQM Or k = ROW_ClinicalDecisionSupport Or k = ROW_ElectronicExchange Or k = ROW_ProtectElectronicHealthInformation Then
                    If k = ROW_ClinicalDecisionDrugInteractionCheck Or k = ROW_ClinicalDecisionSupport Or k = ROW_ProtectElectronicHealthInformation Then
                        Continue For
                    End If
                    C1QualityMeasures.SetData(k, COL_rptNumerator, C1QualityMeasures.GetData(k, COL_calNumerator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptDenominator, C1QualityMeasures.GetData(k, COL_calDenominator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptPercent, C1QualityMeasures.GetData(k, COL_calPercent).ToString())
                    If C1QualityMeasures.GetData(k, COL_calPercent).ToString() <> "N/A" Then
                        Perc = Convert.ToSingle(C1QualityMeasures.GetData(k, COL_calPercent).ToString().Trim("%"))
                        If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(k, COL_Goal).ToString().Trim("%")) Then
                            C1QualityMeasures.SetCellImage(k, COL_rptFlag1, ImgFlag.Images(2))
                        End If
                    End If
                    img = C1QualityMeasures.GetCellImage(k, COL_calFlag2)
                    If Not IsNothing(img) Then
                        C1QualityMeasures.SetCellImage(k, COL_rptFlag2, ImgFlag.Images(0))
                    Else
                        C1QualityMeasures.SetCellImage(k, COL_rptFlag2, Nothing)
                    End If
                Next
            ElseIf rowno = ROW_MenuSet Then

                'For k = ROW_DrugFormularyCheck To ROW_SyndromicServilance
                For k = ROW_SyndromicServilance To ROW_FamilyHealthHistory
                    If k = ROW_GenerateListOfPatient Or k = ROW_ImmunizationRegistry Or k = ROW_SyndromicServilance Then
                        'If k = ROW_DrugFormularyCheck Or k = ROW_GenerateListOfPatient Or k = ROW_ImmunizationRegistry Or k = ROW_SyndromicServilance Then
                        Continue For
                    End If
                    C1QualityMeasures.SetData(k, COL_rptNumerator, C1QualityMeasures.GetData(k, COL_calNumerator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptDenominator, C1QualityMeasures.GetData(k, COL_calDenominator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptPercent, C1QualityMeasures.GetData(k, COL_calPercent).ToString())
                    If C1QualityMeasures.GetData(k, COL_calPercent).ToString() <> "N/A" Then
                        Perc = Convert.ToSingle(C1QualityMeasures.GetData(k, COL_calPercent).ToString().Trim("%"))
                        If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(k, COL_Goal).ToString().Trim("%")) Then
                            C1QualityMeasures.SetCellImage(k, COL_rptFlag1, ImgFlag.Images(2))
                        End If
                    End If
                    img = C1QualityMeasures.GetCellImage(k, COL_calFlag2)
                    If Not IsNothing(img) Then
                        C1QualityMeasures.SetCellImage(k, COL_rptFlag2, ImgFlag.Images(0))
                    Else
                        C1QualityMeasures.SetCellImage(k, COL_rptFlag2, Nothing)
                    End If
                Next
            Else
                C1QualityMeasures.SetData(rowno, COL_rptNumerator, C1QualityMeasures.GetData(rowno, COL_calNumerator).ToString())
                C1QualityMeasures.SetData(rowno, COL_rptDenominator, C1QualityMeasures.GetData(rowno, COL_calDenominator).ToString())
                C1QualityMeasures.SetData(rowno, COL_rptPercent, C1QualityMeasures.GetData(rowno, COL_calPercent).ToString())
                If C1QualityMeasures.GetData(rowno, COL_calPercent).ToString() <> "N/A" Then
                    Perc = Convert.ToSingle(C1QualityMeasures.GetData(rowno, COL_calPercent).ToString().Trim("%"))
                    If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(rowno, COL_Goal).ToString().Trim("%")) Then
                        C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, ImgFlag.Images(2))
                    End If
                Else
                    C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, Nothing)
                End If
                img = C1QualityMeasures.GetCellImage(rowno, COL_calFlag2)
                If Not IsNothing(img) Then
                    C1QualityMeasures.SetCellImage(rowno, COL_rptFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(rowno, COL_rptFlag2, Nothing)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

            Dim clsPat As New cls_MU_Measures
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

        'cmb_RptYear.Items.Clear()

        'Dim i As Integer

        'For i = 1 To 11
        '    cmb_RptYear.Items.Add("20" & i + 10)
        'Next
        'cmb_RptYear.SelectedIndex = 0

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
                'If chk_FirstYear.Checked = True Then
                '    dtpicStartDate.Enabled = True

                '    dtpicStartDate.Value = New DateTime(Date.Now.Year, 1, 1)
                '    dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(89)

                'Else
                '    dtpicStartDate.Enabled = False

                '    dtpicStartDate.Text = New DateTime(cmb_RptYear.Text, 1, 1)
                '    dtpicEndDate.Text = New DateTime(cmb_RptYear.Text, 12, 31)

                'End If

                CheckStatus()
                GetValues()

                blnIsLoading = False
                FillData()
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

    Private Function GetdataWithParam(ByVal SPName As String, ByVal providerid As String, ByVal startdate As String, ByVal enddate As String) As DataTable
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim dt As New DataTable

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Provider"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Value = providerid
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
        'If appSettings("DataBaseConnectionString") IsNot Nothing Then
        '    If appSettings("DataBaseConnectionString") <> "" Then


        '        _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
        '    End If
        'End If
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
                GetValues()
                FillData()

                AddHandler dtpicEndDate.ValueChanged, AddressOf dtpicEndDate_ValueChanged

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dtpicEndDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles dtpicEndDate.ValueChanged

        If blnIsLoading = False Then
            CheckStatus()
            Application.DoEvents()
            GetValues()
            FillData()
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

                    Dim objfrmMeasures As New gloMU.frm_MU_Reports(cmb_Provider.Text, dtpicStartDate.Value.ToString(), dtpicEndDate.Value.ToString())
                    With objfrmMeasures
                        '.Text = "Meaningful Use"
                        .MdiParent = Me.MdiParent
                        .Show()
                        .WindowState = FormWindowState.Maximized
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
                        If objfrmExcludedTemplate.DialogResult = Windows.Forms.DialogResult.OK Then
                            GetValues()
                            FillData()
                        End If
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

            If SaveReport() Then
                clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
                'clsPrntRpt = New gloSSRSApplication.clsPrintReport("glosvr02", "certification", True, "sa", "saglosvr02")
                clsPrntRpt.PrintReport("rptMU_stage2", "ReportID,User", nReportId & "," & _LoginName, gblnDefaultPrinter, "")

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
            MessageBox.Show("Enter Report Name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

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
        Dim MeasureType As Int16

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
        '''''''''''''''''''''''''
        oDB.Execute("MU_InUp_MainMeasures_MST_Stage2", oParameters, oParameters(0).Value)
        'nReportId = oDB.ExecuteScalar("MU_InUp_MainMeasures_MST")
        nReportId = oParameters(0).Value
        'oParameters = Nothing
        oParameters.Clear()
        '''''''''''''''''''''''''''''''''Insert Or Update In Details Table''''''''''''''''''''''''
        For i = ROW_CPOEMedication To ROW_FamilyHealthHistory

            img = C1QualityMeasures.GetCellImage(i, COL_calFlag2)
            If Not IsNothing(img) Then
                cal_Expn = 1
            Else
                cal_Expn = 0
            End If

            img = C1QualityMeasures.GetCellImage(i, COL_rptFlag2)
            If Not IsNothing(img) Then
                rpt_Expn = 1
            Else
                rpt_Expn = 0
            End If

            If i < ROW_MenuSet Then
                MeasureType = 1
            ElseIf i > ROW_MenuSet And i <= ROW_SyndromicServilance Then
                MeasureType = 2
            End If


            If i = ROW_MenuSet Or i = ROW_ClinicalDecisionSupportRule Or i = ROW_SummaryOfCare Or i = ROW_PatientElectronicAccess Then 'Or i = ROW_SyndromicServilance
                ''Here We have to Put code for Report which dnt have denominator and numerator values
                Continue For
            End If
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            checkval = C1QualityMeasures.GetCellCheck(i, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                isSelect = 1
            Else
                isSelect = 0
            End If

            'If i = ROW_CQM Or i = ROW_ElectronicExchange Or i = ROW_ProtectElectronicHealthInformation Then
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

                oDB.Execute("MU_InUp_MainMeasures_DTL_Stage2", oParameters)
                oParameters.Clear()

                Continue For

            ElseIf i = ROW_GenerateListOfPatient Or i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Or i = ROW_SummaryOfCare_Measure3 Then

                If i = ROW_SummaryOfCare_Measure3 Then

                    If C1QualityMeasures.GetData(i, COL_calNumerator) = "Yes" Then
                        blnFirstYr = 1
                    Else
                        blnFirstYr = 0
                    End If

                Else
                    If Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)) = "None" Then
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

                oDB.Execute("MU_InUp_MainMeasures_DTL_Stage2", oParameters)
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

                'If i = ROW_ClinicalDecisionDrugInteractionCheck Or i = ROW_DrugFormularyCheck Then
                If i = ROW_ClinicalDecisionDrugInteractionCheck Then
                    If C1QualityMeasures.GetData(i, COL_calNumerator).ToString() = "Enabled" Then
                        blnFirstYr = 1
                    Else
                        blnFirstYr = 0
                    End If
                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@nCalc_Numerator"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.BigInt
                    oParameter.Value = blnFirstYr
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

                    oDB.Execute("MU_InUp_MainMeasures_DTL_Stage2", oParameters)
                    oParameters.Clear()
                    Continue For

                ElseIf i = ROW_ClinicalDecisionSupport Then
                    Dim str1 As Array
                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@nCalc_Numerator"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.BigInt
                    str1 = C1QualityMeasures.GetData(i, COL_calNumerator).ToString().Split(":")
                    oParameter.Value = str1.GetValue(1).ToString().Trim()
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

                    ''Sanjog- Added on 20101112 to add the Exception Criteria

                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@bMeasureRequired"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.Bit
                    oParameter.Value = CheckMeasureRequiredSetting(blnFlag, C1QualityMeasures.GetData(i, COL_Measure))
                    oParameters.Add(oParameter)
                    oParameter = Nothing


                    oDB.Execute("MU_InUp_MainMeasures_DTL_Stage2", oParameters)
                    oParameters.Clear()
                    Continue For

                Else
                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@nCalc_Numerator"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.BigInt
                    oParameter.Value = C1QualityMeasures.GetData(i, COL_calNumerator)
                    oParameters.Add(oParameter)
                    oParameter = Nothing


                End If

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@nCalc_Denominator"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = C1QualityMeasures.GetData(i, COL_calDenominator)
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@sCalc_Percent"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Value = Convert.ToString(C1QualityMeasures.GetData(i, COL_calPercent)).Trim("%")
                oParameters.Add(oParameter)
                oParameter = Nothing

                If Convert.ToString(C1QualityMeasures.GetData(i, COL_calPercent)) <> "N/A" Then
                    Perc = Convert.ToSingle(C1QualityMeasures.GetData(i, COL_calPercent).ToString().Trim("%"))

                    '18-Sep-14 Aniket: Resolving issue where red flag was not shown when % was 4.5 if goal was 5
                    'As per mail from Phill dated 11-Sep-2014: FW: MU Dashboard Calculation Error
                    If Convert.ToDecimal(FormatNumber(Perc, 2, TriState.True)) <= Convert.ToInt64(C1QualityMeasures.GetData(i, COL_Goal).ToString().Trim("%")) Then
                        blnFirstYr = 1
                        '''''If Flag Is 1 Means Goal % is Not achieve
                    Else
                        blnFirstYr = 0
                        '''''If Flag Is 0 Means Goal % is achieve
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

                oDB.Execute("MU_InUp_MainMeasures_DTL_Stage2", oParameters)
                oParameters.Clear()
            End If
        Next
        If nReportId > 0 Then
            nId = nReportId
        End If
        If blnFlag Then
            '   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "Report Updated", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "Stage 2 2014+ dashboard modified with the name '" & txtReportName.Text & "' for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
        Else
            ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "Report Added", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "New MU Stage 2 2014+ dashboard created with the name '" & txtReportName.Text & "' for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
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

                'Electronic prescriptions
                'ROW_EPrescription = 4

                'Report clinical quality measures
                'ROW_CQM = 10

                'Electronic copy of pat. health info.
                'ROW_EleCopyPatientHealth = 12

                'Electronically exchange clinical info.    
                'ROW_ElectronicExchange = 14

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
                FillData()
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
                GetValues()
                'SetGridStyle()
                FillData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.BeforeEdit
        Try
            If blnIsLoading = False Then

                If e.Col = COL_rptDenominator Or e.Col = COL_rptNumerator Or e.Col = COL_Copybtn Then

                    If e.Row = ROW_ClinicalDecisionDrugInteractionCheck Or e.Row = ROW_ClinicalDecisionSupport Or e.Row = ROW_ProtectElectronicHealthInformation Or e.Row = ROW_MenuSet Or e.Row = ROW_GenerateListOfPatient Or e.Row = ROW_ImmunizationRegistry Or e.Row = ROW_SyndromicServilance Then
                        e.Cancel = True
                    End If

                End If

                If e.Row = ROW_MenuSet Then
                    If e.Col = COL_rptDenominator Or e.Col = COL_rptNumerator Then
                        e.Cancel = True
                    End If
                End If

                If e.Row <= ROW_MenuSet And e.Col = COL_Check Then
                    e.Cancel = True
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1QualityMeasures.KeyPressEdit

        Try

            If e.Col = 4 And e.Row = ROW_ProtectElectronicHealthInformation Then
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
                If (C1QualityMeasures.Row = ROW_ClinicalDecisionDrugInteractionCheck Or C1QualityMeasures.Row = ROW_ClinicalDecisionSupport Or C1QualityMeasures.Row = ROW_ProtectElectronicHealthInformation Or C1QualityMeasures.Row = ROW_GenerateListOfPatient Or C1QualityMeasures.Row = ROW_ImmunizationRegistry Or C1QualityMeasures.Row = ROW_SyndromicServilance Or C1QualityMeasures.Row = ROW_SummaryOfCare_Measure3) Then
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
                ofrm.SingleProvider = ProviderID
                ofrm.StartDate = StartDate
                ofrm.EndDate = Enddate
                ofrm.MdiParent = Me.MdiParent
                ofrm.Show()
                ofrm.SetReportingParameters(MeasureString, cmb_Provider.Text, lbl_NPIValue.Text, cmb_RptYear.Text, txtReportName.Text, lbl_TaxIDValue.Text, False, dtpicStartDate.Value, dtpicEndDate.Value, Label5.Text)
                ofrm.RefreshContent()
                ofrm.BringToFront()

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Public Function GetStoreProcedureName(ByVal ColumnNumber As Integer) As String

        Try
            Select Case ColumnNumber

                Case ROW_CPOEMedication
                    Return "MU_CPOE_Medication_Stage2_Archive"

                Case ROW_CPOERadiology
                    Return "MU_CPOE_Radiology_Stage2_Archive"

                Case ROW_CPOELabs
                    Return "MU_CPOE_Laboratory_Stage2_Archive"

                Case ROW_EPrescription
                    Return "MU_ePrescribedReport_Stage2_Archive"

                Case ROW_Demographic
                    Return "MU_Demographics_Stage2"

                Case ROW_Vitals
                    Return "MU_Vital_Stage2"

                Case ROW_SmokingStatus
                    Return "MU_SmokingStatus_Stage2"


                Case ROW_ClinicalDecisionDrugInteractionCheck
                    Return "MU_DIChecks_Stage2"


                Case ROW_PatientElectronicAccess_Measure1
                    Return "MU_PatientElectronicAccess_Measure1_Stage1"


                Case ROW_PatientElectronicAccess_Measure2
                    Return "MU_PatientElectronicAccess_Measure2_Stage1"

                Case ROW_ClinicalSummaryforPatient
                    Return "MU_ClinicalSummaryUsage_Stage2"

                Case ROW_ClinicalLabTestResults
                    Return "MU_ClinicalLabTestResults_Stage2"

                Case ROW_PreventiveCare
                    Return "MU_PatientReminderUsage_Stage2"

                Case ROW_PatientSpecificEducation
                    Return "MU_PatientSpecificEducation_Stage2_Archive"

                Case ROW_MedicalReconcilation
                    Return "MU_Medication_Reconciliation_Stage2_Archive"

                Case ROW_SummaryOfCare_Measure1
                    Return "MU_SummaryofCare_Measure1_Stage2_Archive"

                Case ROW_SummaryOfCare_Measure2
                    Return "MU_SummaryofCare_Measure2_Stage2_Archive"

                Case ROW_ElectronicNotes
                    Return "MU_ElectronicNotes_Stage2"



                Case ROW_SummaryOfCare_Measure3
                    Return "MU_SummaryofCare_Measure3_Stage2"


                Case ROW_ClinicalDecisionSupport
                    Return "MU_DMRulesSetup_Stage2"

                Case ROW_ImmunizationRegistry
                    Return "MU_SubmitImmunization_Stage2_Archive"

                Case ROW_SecureElectronicMessaging
                    Return "MU_SecureElectronicMessaging_Stage2_Archive"

                Case ROW_SyndromicServilance
                    Return "MU_SubmitsurveillanceData_Stage2_Archive"

                Case ROW_FamilyHealthHistory
                    Return "MU_FamilyHistory_Stage2"

                Case ROW_ImagingResults
                    Return "MU_ImagingResults_Stage2"

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
End Class
