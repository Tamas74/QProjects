Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms


Public Class frm_MU_Dashboard
    Private COL_Check As Integer = 0
    Private COL_Measure As Integer = 1
    Private COL_Goal As Integer = 2
    Private COL_Comment As Integer = 3
    'Sanjog added on 2011 may 10  to show first Denomenator n then numerator column
    Private COL_calNumerator As Integer = 4
    Private COL_calDenominator As Integer = 5
    'Sanjog added on 2011 may 10  to show first Denomenator n then numerator column
    Private COL_calPercent As Integer = 6
    Private COL_calFlag1 As Integer = 7
    Private COL_calFlag2 As Integer = 8
    Private COL_Copybtn As Integer = 9
    'Sanjog added on 2011 may 10  to show first Denomenator n then numerator column
    Private COL_rptNumerator As Integer = 10
    Private COL_rptDenominator As Integer = 11
    'Sanjog added on 2011 may 10  to show first Denomenator n then numerator column
    Private COL_rptPercent As Integer = 12
    Private COL_rptFlag1 As Integer = 13
    Private COL_rptFlag2 As Integer = 14
    ''Sanjog
    Private ROW_CPOEMedication As Integer = 1
    Private ROW_DICheck As Integer = 2
    Private ROW_ProblemList As Integer = 3
    Private ROW_EPrescription As Integer = 4
    Private ROW_ActiveMEdicationList As Integer = 5
    Private ROW_ActiveAllergyList As Integer = 6
    Private ROW_Demographic As Integer = 7
    Private ROW_Vitals As Integer = 8
    Private ROW_SmokingStatus As Integer = 9
    Private ROW_CQM As Integer = 10
    Private ROW_DecisionSupportRule As Integer = 11
    Private ROW_EleCopyPatientHealth As Integer = 12
    Private ROW_ClinicalSummaryforPatient As Integer = 13
    Private ROW_ElectronicExchange As Integer = 14
    Private ROW_ProtectEleHealthInfo As Integer = 15
    Private ROW_MEnuSet As Integer = 16
    Private ROW_DrugFormularyCheck As Integer = 17
    Private ROW_ClinicallabTest As Integer = 18
    Private ROW_Generatelistofpatient As Integer = 19
    Private ROW_Reminderstopatient As Integer = 20
    Private ROW_ElectronicAccessHealthInfo As Integer = 21
    Private ROW_PatientSpecificEducatiion As Integer = 22
    Private ROW_MedicalReconcilation As Integer = 23
    Private ROW_SummaryCareTransition As Integer = 24
    Private ROW_ImmunizationRegistry As Integer = 25
    Private ROW_SyndromicServilance As Integer = 26
    ''Sanjog

    Private ProviderID As Int64
    Private StartDate As String
    Private Enddate As String
    Private Percent As String
    Private nReportId As Int64 = 0
    Private nId As Int64
    Private Exception As Int64 = 0
    ''Sanjog- Added on 20101203 to optimize the code
    Private blnIsLoading As Boolean = False

    Dim btn As New Button

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
    Public mycaller As frm_MU_ViewDashboard


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
            C1QualityMeasures.Cols(COL_calNumerator).AllowEditing = False
            C1QualityMeasures.Cols(COL_calDenominator).AllowEditing = False
            C1QualityMeasures.Cols(COL_calPercent).AllowEditing = False
            C1QualityMeasures.Cols(COL_calFlag1).AllowEditing = False
            C1QualityMeasures.Cols(COL_calFlag2).AllowEditing = False
            C1QualityMeasures.Cols(COL_Copybtn).AllowEditing = True
            C1QualityMeasures.Cols(COL_rptNumerator).AllowEditing = True
            C1QualityMeasures.Cols(COL_rptDenominator).AllowEditing = True
            C1QualityMeasures.Cols(COL_rptPercent).AllowEditing = False
            C1QualityMeasures.Cols(COL_rptFlag1).AllowEditing = False
            C1QualityMeasures.Cols(COL_rptFlag2).AllowEditing = False

            ' C1QualityMeasures.Cols(COL_rptNumerator).DataType = GetType(System.Int64)
            C1QualityMeasures.Cols(COL_rptDenominator).AllowEditing = True
            'C1QualityMeasures.Cols(COL_rptDenominator).DataType = GetType(System.Int64)
            C1QualityMeasures.Cols(COL_rptDenominator).AllowEditing = True

            Dim cs As C1.Win.C1FlexGrid.CellStyle '= C1QualityMeasures.Styles.Add("cs_editableChekcbox")
            'cs.DataType = new TypeOf(System.Boolean)    
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
            'C1QualityMeasures.Cols(COL_calFlag1).AllowMerging = True
            'C1QualityMeasures.Cols(COL_calFlag2).AllowMerging = True


            ''Allow Editing
            'C1QualityMeasures.Cols(COL_Check).AllowResizing = False
            'C1QualityMeasures.Cols(COL_Measure).AllowResizing = False
            'C1QualityMeasures.Cols(COL_Goal).AllowResizing = False
            'C1QualityMeasures.Cols(COL_Comment).AllowResizing = False
            'C1QualityMeasures.Cols(COL_calNumerator).AllowResizing = False
            'C1QualityMeasures.Cols(COL_calDenominator).AllowResizing = False
            'C1QualityMeasures.Cols(COL_calPercent).AllowResizing = False
            'C1QualityMeasures.Cols(COL_calFlag1).AllowResizing = False
            'C1QualityMeasures.Cols(COL_calFlag2).AllowResizing = False
            'C1QualityMeasures.Cols(COL_Copybtn).AllowResizing = False
            'C1QualityMeasures.Cols(COL_rptNumerator).AllowResizing = False
            'C1QualityMeasures.Cols(COL_rptDenominator).AllowResizing = False
            'C1QualityMeasures.Cols(COL_rptPercent).AllowResizing = False
            'C1QualityMeasures.Cols(COL_rptFlag1).AllowResizing = False
            'C1QualityMea sures.Cols(COL_rptFlag2).AllowResizing = False

            C1QualityMeasures.Rows.Add(26)
            FillMeasures()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillData()
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dt As New DataTable

            If GetAdminsetting("CPOE MU STAGE 1 2013") = "0" Then
                dt = GetdataWithParam("MU_CPOE_MedicationOrders_New", ProviderID.ToString(), StartDate, Enddate)
            Else
                dt = GetdataWithParam("MU_CPOE_MedicationOrders_MU2013", ProviderID.ToString(), StartDate, Enddate)
            End If

            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_CPOEMedication, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_CPOEMedication, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 30 Then
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
                'C1QualityMeasures.SetData(ROW_CPOEMedication, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_CPOEMedication, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_CPOEMedication, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_CPOEMedication, COL_rptFlag2, Nothing)

            End If

            dt = Getdata("MU_DIChecks")
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0).ToString() = "1" Then
                    C1QualityMeasures.SetData(ROW_DICheck, COL_calNumerator, "Enabled")
                Else
                    C1QualityMeasures.SetData(ROW_DICheck, COL_calNumerator, "Disabled")
                End If
            End If




            dt = GetdataWithParam("MU_ProblemUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ProblemList, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ProblemList, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent < 80 Then
                        C1QualityMeasures.SetCellImage(ROW_ProblemList, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ProblemList, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ProblemList, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ProblemList, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ProblemList, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_ProblemList, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_ProblemList, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_ProblemList, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ProblemList, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ProblemList, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_ePrescribedReport", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_EPrescription, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_EPrescription, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 40 Then
                        C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_EPrescription, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_EPrescription, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_EPrescription, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_EPrescription, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_EPrescription, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_EPrescription, COL_rptFlag2, Nothing)
            End If

            dt = GetdataWithParam("MU_MedicationUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent < 80 Then
                        C1QualityMeasures.SetCellImage(ROW_ActiveMEdicationList, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ActiveMEdicationList, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ActiveMEdicationList, COL_calFlag1, Nothing)
                    C1QualityMeasures.SetCellImage(ROW_ActiveMEdicationList, COL_calFlag2, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ActiveMEdicationList, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ActiveMEdicationList, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_AllergyUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_calDenominator, dt.Rows(0)(1).ToString())

                If Percent <> "N/A" Then
                    If Percent < 80 Then
                        C1QualityMeasures.SetCellImage(ROW_ActiveAllergyList, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ActiveAllergyList, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ActiveAllergyList, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ActiveAllergyList, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ActiveAllergyList, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_DemographicUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_Demographic, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_Demographic, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
                        C1QualityMeasures.SetCellImage(ROW_Demographic, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_Demographic, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_Demographic, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_Demographic, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_Demographic, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_Demographic, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_Demographic, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_Demographic, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_Demographic, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_Demographic, COL_rptFlag2, Nothing)
            End If


            If GetAdminsetting("VITAL MU STAGE 1 2013") = "0" Then
                dt = GetdataWithParam("MU_PatientVitalReport", ProviderID.ToString(), StartDate, Enddate)
            Else
                dt = GetdataWithParam("MU_PatientVitalReport_MU2013", ProviderID.ToString(), StartDate, Enddate)
            End If


            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_Vitals, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_Vitals, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
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
                'C1QualityMeasures.SetData(ROW_Vitals, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_Vitals, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_Vitals, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_Vitals, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_Vitals, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_SmokingStatus", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_SmokingStatus, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SmokingStatus, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
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
                'C1QualityMeasures.SetData(ROW_SmokingStatus, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_SmokingStatus, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_SmokingStatus, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_SmokingStatus, COL_rptFlag2, Nothing)
            End If

            dt = Getdata("MU_DMRulesSetup")
            If dt.Rows.Count > 0 Then
                C1QualityMeasures.SetData(ROW_DecisionSupportRule, COL_calNumerator, "DM Rules: " & dt.Rows(0)(0).ToString())
            End If


            'START SHUBHANGI
            dt = GetdataWithParam("MU_ElecCopyofPatientInfo", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
                        C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_calFlag2, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_EleCopyPatientHealth, COL_rptFlag2, Nothing)
            End If
            'END SHUBHANGI

            'START SHUBHANGI
            dt = GetdataWithParam("MU_ClinicalSummaryUsage", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
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
                'C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ClinicalSummaryforPatient, COL_rptFlag2, Nothing)
            End If
            'END SHUBHANGI

            dt = Getdata("MU_FormularyChecks")
            If dt.Rows.Count > 0 Then
                If IsNothing(dt) = False Then
                    If dt.Rows(0)(0).ToString() = "1" Then
                        C1QualityMeasures.SetData(ROW_DrugFormularyCheck, COL_calNumerator, "Enabled")
                    Else
                        C1QualityMeasures.SetData(ROW_DrugFormularyCheck, COL_calNumerator, "Disabled")
                    End If
                End If
            End If

            dt = GetdataWithParam("MU_ClinicalTestResults", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                If IsNothing(dt) = False Then
                    Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                    C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_calNumerator, dt.Rows(0)(0).ToString())
                    C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_calDenominator, dt.Rows(0)(1).ToString())
                    If Percent <> "N/A" Then
                        If Percent < 40 Then
                            C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_calFlag1, ImgFlag.Images(2))
                        Else
                            C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_calFlag1, Nothing)
                        End If
                        C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                    Else
                        C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_calPercent, Percent)
                        C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_calFlag1, Nothing)
                    End If
                    If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                        C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_calFlag2, ImgFlag.Images(0))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_calFlag2, Nothing)
                    End If
                End If
                'C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ClinicallabTest, COL_rptFlag2, Nothing)
            End If

            dt = GeneratePatientListCreated(cmb_Provider.SelectedValue)

            Dim dtDate As New DateTime
            If (dt.Rows.Count > 0) Then
                dtDate = dt.Rows(0)(0).ToString()
                C1QualityMeasures.SetData(ROW_Generatelistofpatient, COL_calNumerator, dtDate.ToShortDateString())
            Else
                C1QualityMeasures.SetData(ROW_Generatelistofpatient, COL_calNumerator, "None")
            End If


            dt = GetdataWithParam("MU_PatientReminderUsage", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 20 Then
                        C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_calFlag1, Nothing)
                End If
                If Convert.ToInt64(dt.Rows(0)(2).ToString()) = 1 Then
                    C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_calFlag2, ImgFlag.Images(0))
                Else
                    C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_calFlag2, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_Reminderstopatient, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_ElecAccesstoPatientInfo", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 10 Then
                        C1QualityMeasures.SetCellImage(ROW_ElectronicAccessHealthInfo, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_ElectronicAccessHealthInfo, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_ElectronicAccessHealthInfo, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ElectronicAccessHealthInfo, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_ElectronicAccessHealthInfo, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_PatientSpecificEducation", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 10 Then
                        C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducatiion, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducatiion, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducatiion, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducatiion, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_PatientSpecificEducatiion, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_Medication_Reconciliation", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
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
                'C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_MedicalReconcilation, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_SummaryCareTransitions", ProviderID.ToString(), StartDate, Enddate)
            If dt.Rows.Count > 0 Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_calDenominator, dt.Rows(0)(1).ToString())
                If Percent <> "N/A" Then
                    If Percent < 50 Then
                        C1QualityMeasures.SetCellImage(ROW_SummaryCareTransition, COL_calFlag1, ImgFlag.Images(2))
                    Else
                        C1QualityMeasures.SetCellImage(ROW_SummaryCareTransition, COL_calFlag1, Nothing)
                    End If
                    C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
                Else
                    C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_calPercent, Percent)
                    C1QualityMeasures.SetCellImage(ROW_SummaryCareTransition, COL_calFlag1, Nothing)
                End If
                'C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_rptDenominator, Nothing)
                'C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_rptNumerator, Nothing)
                'C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_rptPercent, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_SummaryCareTransition, COL_rptFlag1, Nothing)
                'C1QualityMeasures.SetCellImage(ROW_SummaryCareTransition, COL_rptFlag2, Nothing)
            End If


            dt = GetdataWithParam("MU_SubmitImmunization", ProviderID.ToString(), StartDate, Enddate)

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


            dt = GetdataWithParam("MU_Submitsurveillancedata", ProviderID.ToString(), StartDate, Enddate)

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

    Private Sub HideRowsAsPerSettings()

        If GetAdminsetting("ELECTRONIC PRESCRIPTION MU 2013") = "1" Then
            C1QualityMeasures.Rows(ROW_EPrescription).Visible = False
        End If

        If GetAdminsetting("REPORT CLINICAL QM MU 2013") = "1" Then
            C1QualityMeasures.Rows(ROW_CQM).Visible = False
        End If

        If GetAdminsetting("ELECTRONIC COPY OF PAT. INFO. MU 2013") = "1" Then
            C1QualityMeasures.Rows(ROW_EleCopyPatientHealth).Visible = False
        End If


        If GetAdminsetting("ELECTRONICALLY EXCH cLINICAL INFO MU 2013") = "1" Then
            C1QualityMeasures.Rows(ROW_ElectronicExchange).Visible = False
        End If

    End Sub

    Private Sub FillMeasures()
        Try

            C1QualityMeasures.Rows(ROW_CPOEMedication).AllowEditing = True
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Measure, "Use CPOE for medication orders")
            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Goal, "30%")
            C1QualityMeasures.SetCellStyle(ROW_CPOEMedication, COL_Check, C1QualityMeasures.Styles("cs_editableChekcbox"))



            C1QualityMeasures.SetData(ROW_CPOEMedication, COL_Comment, "Use CPOE for medication orders directly entered by any licensed healthcare professional who can enter orders into the medical record per state, local and professional guidelines")
            ''

            C1QualityMeasures.Rows(ROW_DICheck).AllowEditing = True
            C1QualityMeasures.SetData(ROW_DICheck, COL_Measure, "Implement Drug Interaction Checks")
            'C1QualityMeasures.Rows(i).AllowEditing = False
            C1QualityMeasures.SetData(ROW_DICheck, COL_Goal, "")
            C1QualityMeasures.SetData(ROW_DICheck, COL_Comment, "Implement drug-drug and drug-allergy interaction checks")

            C1QualityMeasures.SetData(ROW_ProblemList, COL_Measure, "Maintain Problem List")
            C1QualityMeasures.SetData(ROW_ProblemList, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_ProblemList, COL_Comment, "Maintain an up-to-date problem list of current and active diagnoses")


            C1QualityMeasures.SetData(ROW_EPrescription, COL_Measure, "Electronic prescriptions")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Goal, "40%")
            C1QualityMeasures.SetData(ROW_EPrescription, COL_Comment, "Generate and transmit permissible prescriptions electronically (eRx)")

            C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_Measure, "Maintain active medication list")
            C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_ActiveMEdicationList, COL_Comment, "Maintain active medication list")

            C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_Measure, "Maintain active med. allergy list")
            C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_Goal, "80%")
            C1QualityMeasures.SetData(ROW_ActiveAllergyList, COL_Comment, "Maintain active medication allergy list")


            C1QualityMeasures.SetData(ROW_Demographic, COL_Measure, "Record demographics")
            C1QualityMeasures.SetData(ROW_Demographic, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_Demographic, COL_Comment, "Record demographics " & vbNewLine & "o preferred language" & vbNewLine & "o gender" & vbNewLine & "o race" & vbNewLine & "o ethnicity" & vbNewLine & "o date of birth")



            C1QualityMeasures.SetData(ROW_Vitals, COL_Measure, "Record and chart vital signs")
            C1QualityMeasures.SetData(ROW_Vitals, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_Vitals, COL_Comment, "Record and chart changes in vital signs:" & vbCrLf & "o Height" & vbCrLf & "o weight" & vbNewLine & "o blood pressure" & vbNewLine & "o Calculate And display : BMI" & vbNewLine & "  o Plot and display growth charts for children 2-20 years, including BMI.")


            C1QualityMeasures.SetData(ROW_SmokingStatus, COL_Measure, "Record smoking status")
            C1QualityMeasures.SetData(ROW_SmokingStatus, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_SmokingStatus, COL_Comment, "Record smoking status for patients 13 years old or older")


            C1QualityMeasures.SetData(ROW_CQM, COL_Measure, "Report clinical quality measures")
            C1QualityMeasures.Rows(ROW_CQM).AllowEditing = True
            C1QualityMeasures.SetData(ROW_CQM, COL_Goal, "")
            C1QualityMeasures.SetData(ROW_CQM, COL_calNumerator, "")
            C1QualityMeasures.SetData(ROW_CQM, COL_calDenominator, "")
            C1QualityMeasures.SetData(ROW_CQM, COL_calPercent, "")
            C1QualityMeasures.SetData(ROW_CQM, COL_Comment, "Report ambulatory clinical quality measures to CMS or the States")

            C1QualityMeasures.SetData(ROW_DecisionSupportRule, COL_Measure, "Clinical decision support rule")
            C1QualityMeasures.Rows(ROW_DecisionSupportRule).AllowEditing = True
            C1QualityMeasures.SetData(ROW_DecisionSupportRule, COL_Comment, "Implement one clinical decision support rule relevant to specialty or high clinical priority along with the ability to track compliance that rule")


            C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_Measure, "Electronic copy of pat. health info.")
            C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_EleCopyPatientHealth, COL_Comment, "Provide patients with an electronic copy of their health information (including diagnostic test results, problem list, medication lists, medication allergies), upon request")

            C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_Measure, "Clinical summaries for patients")
            C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_ClinicalSummaryforPatient, COL_Comment, "Provide clinical summaries for patients for each office visit")


            C1QualityMeasures.SetData(ROW_ElectronicExchange, COL_Measure, "Electronically exchange clinical info.")
            C1QualityMeasures.Rows(ROW_ElectronicExchange).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ElectronicExchange, COL_Comment, "Capability to exchange key clinical information (for example, problem list, medication list, medication allergies, diagnostic test results), among providers of care and patient authorized entities electronically")

            C1QualityMeasures.SetData(ROW_ProtectEleHealthInfo, COL_Measure, "Protect electronic health information")
            C1QualityMeasures.Rows(ROW_ProtectEleHealthInfo).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ProtectEleHealthInfo, COL_Goal, "")
            C1QualityMeasures.SetData(ROW_ProtectEleHealthInfo, COL_calNumerator, "")
            C1QualityMeasures.SetData(ROW_ProtectEleHealthInfo, COL_calDenominator, "")
            C1QualityMeasures.SetData(ROW_ProtectEleHealthInfo, COL_calPercent, "")
            C1QualityMeasures.SetData(ROW_ProtectEleHealthInfo, COL_Comment, "Protect electronic health information created or maintained by the certified EHR technology through the implementation of appropriate technical capabilities")


            Dim j As Integer
            For j = ROW_CPOEMedication To ROW_ProtectEleHealthInfo
                C1QualityMeasures.Rows(j).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(j, COL_Comment, ImgFlag.Images(1))
                C1QualityMeasures.SetCellCheck(j, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
            Next

            ''''''''''''''''''''''''''''''''''''''''''''
            ''''Menu Set Measure
            '''''''''''''''''''''''''''''''''''''''''''''
            'C1QualityMeasures.Rows.Add()

            C1QualityMeasures.Rows(ROW_MEnuSet).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
            C1QualityMeasures.Rows(ROW_MEnuSet).StyleNew.ForeColor = Color.White
            C1QualityMeasures.Rows(ROW_MEnuSet).StyleNew.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            C1QualityMeasures.Rows(ROW_MEnuSet).AllowMerging = True
            C1QualityMeasures(ROW_MEnuSet, COL_calFlag1) = "Flag"
            C1QualityMeasures(ROW_MEnuSet, COL_calFlag2) = "Flag"

            C1QualityMeasures.SetData(ROW_MEnuSet, COL_Measure, "Menu Set Measures")
            C1QualityMeasures.SetData(ROW_MEnuSet, COL_Goal, "Goal%")
            C1QualityMeasures.SetCellStyle(ROW_MEnuSet, COL_Check, New C1.Win.C1FlexGrid.TextAlignEnum)
            C1QualityMeasures.SetData(ROW_MEnuSet, COL_calNumerator, "Numerator")
            C1QualityMeasures.SetData(ROW_MEnuSet, COL_calDenominator, "Denominator")
            C1QualityMeasures.SetData(ROW_MEnuSet, COL_calPercent, "Percent")


            C1QualityMeasures.SetData(ROW_DrugFormularyCheck, COL_Measure, "Implement drug formulary checks")
            C1QualityMeasures.Rows(ROW_DrugFormularyCheck).AllowEditing = True
            C1QualityMeasures.SetData(ROW_DrugFormularyCheck, COL_Comment, "Implement drug formulary checks")


            C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_Measure, "Incorporate clinical lab-test results")
            C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_Goal, "40%")
            C1QualityMeasures.SetData(ROW_ClinicallabTest, COL_Comment, "Incorporate clinical lab-test results into EHR as structured data")


            C1QualityMeasures.SetData(ROW_Generatelistofpatient, COL_Measure, "Generate lists of patients")
            C1QualityMeasures.Rows(ROW_Generatelistofpatient).AllowEditing = True
            C1QualityMeasures.SetData(ROW_Generatelistofpatient, COL_Comment, "Generate lists of patients by specific conditions to use for quality improvement, reduction of disparities, research or outreach")


            C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_Measure, "Send reminders to patients")
            C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_Goal, "20%")
            C1QualityMeasures.SetData(ROW_Reminderstopatient, COL_Comment, "Send reminders to patients per patient preference for preventive/ follow up care")

            C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_Measure, "Electronic access to health info.")
            C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_ElectronicAccessHealthInfo, COL_Comment, "Provide patients with timely electronic access to their health information (including lab results, problem list, medication lists, medication allergies) within four business days of the information being available to the EP")


            C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_Measure, "Patient-specific education resources")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_Goal, "10%")
            C1QualityMeasures.SetData(ROW_PatientSpecificEducatiion, COL_Comment, "Use certified EHR technology to identify patient-specific education resources and provide those resources to the patient if appropriate")


            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Measure, "Medication reconciliation")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_MedicalReconcilation, COL_Comment, "The EP, eligible hospital or CAH who receives a patient from another setting of care or provider of care or believes an encounter is relevant should perform medication reconciliation")


            C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_Measure, "Summary provided at care transitions")
            C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_Goal, "50%")
            C1QualityMeasures.SetData(ROW_SummaryCareTransition, COL_Comment, "The EP, eligible hospital or CAH who transitions their patient to another setting of care or provider of care or refers their patient to another provider of care should provide summary of care record for each transition of care or referral")


            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Measure, "Submit data to imm. registries or IIS")
            C1QualityMeasures.Rows(ROW_ImmunizationRegistry).AllowEditing = True
            C1QualityMeasures.SetData(ROW_ImmunizationRegistry, COL_Comment, "Capability to submit electronic data to immunization registries or Immunization Information Systems and actual submission in accordance with applicable law and practice")


            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Measure, "Submit syndromic surveillance data")
            C1QualityMeasures.Rows(ROW_SyndromicServilance).AllowEditing = True
            C1QualityMeasures.SetData(ROW_SyndromicServilance, COL_Comment, "Capability to submit electronic syndromic surveillance data to public health agencies and actual submission in accordance with applicable law and practice")

            'Dim btn As New Button
            'btn = New Button
            'Dim rct As New Rectangle
            'rct = C1QualityMeasures.GetCellRect(1, COL_Copybtn, True)
            'btn.Text = "-->>"
            'AddHandler btn.Click, AddressOf btn_Click
            'C1QualityMeasures.Controls.Add(btn)
            'btn.Bounds = rct

            Dim rg As C1.Win.C1FlexGrid.CellRange
            rg = C1QualityMeasures.GetCellRange(ROW_CPOEMedication, COL_Copybtn, ROW_CPOEMedication, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_ProblemList, COL_Copybtn, ROW_SmokingStatus, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_EleCopyPatientHealth, COL_Copybtn, ROW_ClinicalSummaryforPatient, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_ClinicallabTest, COL_Copybtn, ROW_ClinicallabTest, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(ROW_Reminderstopatient, COL_Copybtn, ROW_SummaryCareTransition, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            For j = ROW_DrugFormularyCheck To ROW_SyndromicServilance
                C1QualityMeasures.Rows(j).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(j, COL_Comment, ImgFlag.Images(1))
                C1QualityMeasures.SetCellCheck(j, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub frm_MU_Dashboard_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsNothing(mycaller) = False Then
            If mycaller.CanSelect = True Then
                mycaller.RefreshMeasure(nId)

            End If
        End If
    End Sub
    'Private Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    'End Sub

    Private Sub frm_MU_Dashboard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            blnIsLoading = True

            SetGridStyle()

            FillYear()

            If nReportId = 0 Then

                FillProvider()
                dtpicStartDate.Text = New DateTime(cmb_RptYear.Text, 1, 1)
                dtpicEndDate.Text = New DateTime(cmb_RptYear.Text, 12, 31)

                GetValues()
                FillData()
                HideRowsAsPerSettings()
            Else

                FillFromDT(nReportId)
            End If

            CheckStatus()

            'Aniket: 12-Mar-13 Fixed Issue 47723 from 7021
            ShowNPIandTacID(cmb_Provider.SelectedValue)

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

            oDB.Retrive("MU_Select_MainMeasure_MST", oParameters, dt)
            oParameters.Clear()

            If dt.Rows.Count > 0 Then

                txtReportName.Text = dt.Rows(0)(0)

                FillProvider(Convert.ToInt64(dt.Rows(0)(1)))
                cmb_Provider.SelectedValue = Convert.ToInt64(dt.Rows(0)(1))

                ShowNPIandTacID(Convert.ToInt64(dt.Rows(0)(1)))
                cmb_RptYear.Text = dt.Rows(0)(2).ToString()
                If Convert.ToString(dt.Rows(0)(3)) = True Then
                    chk_FirstYear.Checked = True
                    dtpicStartDate.Enabled = True
                Else
                    dtpicStartDate.Enabled = False
                    chk_FirstYear.Checked = False
                End If
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

                oDB.Retrive("MU_Select_MainMeasure_DTL", oParameters, dt)
                oParameters.Clear()
                Dim dtrwno As Integer = 0
                If dt.Rows.Count > 0 Then
                    For i = 1 To C1QualityMeasures.Rows.Count - 1
                        If i = ROW_MEnuSet Then
                            Continue For
                        End If
                        'If i > 16 Then
                        If dt.Rows(dtrwno)(0) = True Then
                            C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                        Else
                            C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                        End If
                        ' End If
                        C1QualityMeasures.SetData(i, COL_Measure, dt.Rows(dtrwno)(1))
                        If i = ROW_DICheck Or i = ROW_DrugFormularyCheck Then
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
                        ElseIf i = ROW_DecisionSupportRule Then
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
                        ElseIf i = ROW_Generatelistofpatient Or i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then
                            If dt.Rows(dtrwno)(3) = 0 Then
                                C1QualityMeasures.SetData(i, COL_calNumerator, "None")
                            Else
                                C1QualityMeasures.SetData(i, COL_calNumerator, gloDateMaster.gloDate.DateAsDate(dt.Rows(dtrwno)(3)))
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
        If chk_FirstYear.Checked = True Then
            StartDate = dtpicStartDate.Value.ToShortDateString()
            Enddate = dtpicEndDate.Value.ToShortDateString
        Else
            If cmb_RptYear.Text <> "" Then
                StartDate = New DateTime(cmb_RptYear.Text, 1, 1).ToShortDateString()
                Enddate = New DateTime(cmb_RptYear.Text, 12, 31).ToShortDateString()
            Else
                StartDate = New DateTime(Date.Now.Year, 1, 1).ToShortDateString()
                Enddate = New DateTime(Date.Now.Year, 12, 31).ToShortDateString()
            End If
        End If
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
                If e.Col <> 0 And e.Col <> COL_Copybtn Then
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
                        '  C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptDenominator, DBNull.Value)
                        If Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)) <> "" AndAlso Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)) <> "" Then

                            Dim _Deno As Int64 = 0
                            Dim _Num As Int64 = 0
                            _Deno = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)
                            _Num = C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)
                            If (_Deno >= 0) AndAlso _Num >= 0 Then

                                Dim perc As Single
                                If _Deno = 0 Then
                                    C1QualityMeasures.SetData(C1QualityMeasures.Row, COL_rptPercent, "N/A")
                                    C1QualityMeasures.SetCellImage(C1QualityMeasures.Row, COL_rptFlag1, Nothing)

                                Else
                                    perc = (_Num / _Deno) * 100
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
                            If Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptDenominator)) <> "" And Convert.ToString(C1QualityMeasures.GetData(C1QualityMeasures.Row, COL_rptNumerator)) <> "" Then
                                If _Deno >= 0 AndAlso _Num >= 0 Then
                                    If e.Row = ROW_Vitals Or e.Row = ROW_SmokingStatus Or e.Row = ROW_EleCopyPatientHealth Or e.Row = ROW_ClinicalSummaryforPatient Or e.Row = ROW_ClinicallabTest Or e.Row = ROW_Reminderstopatient Or e.Row = ROW_MedicalReconcilation Or e.Row = ROW_SummaryCareTransition Then
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
                For k = ROW_CPOEMedication To ROW_ProtectEleHealthInfo
                    If k = ROW_DICheck Or k = ROW_CQM Or k = ROW_DecisionSupportRule Or k = ROW_ElectronicExchange Or k = ROW_ProtectEleHealthInfo Then
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
            ElseIf rowno = ROW_MEnuSet Then

                For k = ROW_DrugFormularyCheck To ROW_SyndromicServilance
                    If k = ROW_DrugFormularyCheck Or k = ROW_Generatelistofpatient Or k = ROW_ImmunizationRegistry Or k = ROW_SyndromicServilance Then
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
        cmb_RptYear.Items.Clear()
        Dim i As Integer
        For i = 1 To 11
            cmb_RptYear.Items.Add("20" & i + 10)
        Next
        cmb_RptYear.SelectedIndex = 0
    End Sub

    Private Sub chk_FirstYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_FirstYear.CheckedChanged
        Try
            If blnIsLoading = False Then
                blnIsLoading = True
                If chk_FirstYear.Checked = True Then
                    dtpicStartDate.Enabled = True
                    ''Commented on 2011 April to show firstdate of selected year
                    'Dim dt As DateTime
                    'Dim sdt As DateTime
                    'Dim edt As DateTime
                    'dt=New DateTime(cmb_RptYear.Text, DateTime.Now.Month, DateTime.Now.Day)
                    'sdt = New DateTime(cmb_RptYear.Text, 10, 2)
                    'If Date.Compare(sdt, dt) < 0 Then
                    '    'MessageBox.Show("Select Proper Date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    'blnIsLoading = True
                    '    dtpicStartDate.Value = New DateTime(cmb_RptYear.Text, 1, 1)
                    '    dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(90)
                    '    'blnIsLoading = False
                    '    'Return
                    'Else
                    '    ''Sanjog-Added on 20101110 to show the first and last date of year for start date and end date 
                    '    dtpicStartDate.Text = New DateTime(cmb_RptYear.Text, DateTime.Now.Month, DateTime.Now.Day)
                    '    ''Sanjog-Added on 20101110 to show the first and last date of year for start date and end date 
                    '    dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(90)
                    '    'dtpicEndDate.Value = New DateTime(cmb_RptYear.Text, DateTime.Now.Month, DateTime.Now.Day)
                    '    'dtpicStartDate.Text = Convert.ToDateTime(dtpicStartDate.Value).AddDays(-90)
                    'End If
                    dtpicStartDate.Value = New DateTime(cmb_RptYear.Text, 1, 1)
                    dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(89)
                    ''Added on 2011 April to show firstdate of selected year
                Else
                    dtpicStartDate.Enabled = False
                    ''Sanjog-Added on 20101110 to show the first and last date of year for start date and end date 
                    dtpicStartDate.Text = New DateTime(cmb_RptYear.Text, 1, 1)
                    dtpicEndDate.Text = New DateTime(cmb_RptYear.Text, 12, 31)
                    ''Sanjog-Added on 20101110 to show the first and last date of year for start date and end date 
                End If
                CheckStatus()
                GetValues()
                'SetGridStyle()
                blnIsLoading = False
                FillData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CheckStatus()
        Try
            Dim i As Int64
            Dim j As Int64
            If chk_FirstYear.Checked = True Then
                If Date.Compare(dtpicStartDate.Value.ToShortDateString(), dtpicEndDate.Value.ToShortDateString()) <= 0 Then
                    i = Date.Compare(dtpicStartDate.Value.ToShortDateString(), Date.Now.ToShortDateString())
                    j = Date.Compare(dtpicEndDate.Value.ToShortDateString(), Date.Now.ToShortDateString())
                    If i >= 0 And j <= 0 Then
                        Label5.Text = "Reporting Period In Progress"
                    ElseIf i < 0 And j < 0 Then
                        Label5.Text = "Reporting Period Complete"
                    ElseIf i < 0 And j <= 0 Then
                        Label5.Text = "Reporting Period In Progress"
                    ElseIf i > 0 And j > 0 Then
                        Label5.Text = "Reporting Period Not Started"
                    ElseIf i = 0 And j > 0 Then
                        Label5.Text = "Reporting Period In Progress"
                    ElseIf i <= 0 And j >= 0 Then
                        Label5.Text = "Reporting Period In Progress"
                    Else
                        Label5.Text = "Please Select Proper date"
                    End If
                Else
                    MessageBox.Show("Please Select Proper date")
                End If
            Else
                ''
                i = Year(Date.Now.ToShortDateString())
                j = Convert.ToInt64(cmb_RptYear.SelectedItem)
                If i = j Then
                    Label5.Text = "Reporting Period In Progress"
                ElseIf i < j Then
                    Label5.Text = "Reporting Period Not Started"
                Else
                    Label5.Text = "Reporting Period Complete"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''commented by sanjog 
    'Private Function Retrivedata(ByVal SPName As String, ByVal nProviderID As Int64) As DataTable
    '    Try
    '        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
    '        Dim oParameter As gloDatabaseLayer.DBParameter
    '        Dim oParameters As New gloDatabaseLayer.DBParameters
    '        oDB.Connect(False)
    '        Dim dt As New DataTable

    '        oParameter = New gloDatabaseLayer.DBParameter()
    '        oParameter.ParameterName = "@Provider"
    '        oParameter.ParameterDirection = ParameterDirection.Input
    '        oParameter.DataType = SqlDbType.BigInt
    '        oParameter.Value = nProviderID
    '        oParameters.Add(oParameter)
    '        oParameter = Nothing

    '        oDB.Retrive("" + SPName + "", oParameters, dt)

    '        oDB.Disconnect()
    '        Return dt
    '    Catch ex As Exception
    '    End Try
    'End Function
    Private Function GeneratePatientListCreated(ByVal ProviderID As Int64) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable
            Dim conn As SqlConnection = New SqlConnection(_databaseConnectionString)
            Try
                'conn = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")
                conn.Open()
                cmd = New SqlCommand("SELECT CONVERT(VARCHAR,sSettingsValue,101) FROM settings WHERE sSettingsName = 'LastPatientListCreated |" & ProviderID & "' ", conn)
                Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
                ad.Fill(dt)
            Finally
                If Not IsNothing(conn) Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                End If
            End Try
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
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
    'Private Function GeneratelistsofPatients()
    '    Dim cmd As New SqlCommand("" + SPName + "", Con)
    '    cmd.CommandType = CommandType.Text

    '    Dim objParam As SqlParameter
    '    Dim da As New SqlDataAdapter
    '    Dim dt As New DataTable
    '    da.SelectCommand = cmd
    '    da.Fill(dt)
    '    Return dt
    'End Function
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

    Private Sub dtpicStartDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpicStartDate.ValueChanged
        Try
            If blnIsLoading = False Then
                If chk_FirstYear.Checked = True Then
                    Dim dt As DateTime
                    Dim sdt As DateTime
                    Dim edt As DateTime
                    dt = dtpicStartDate.Value
                    sdt = New DateTime(cmb_RptYear.Text, 10, 3)
                    edt = New DateTime(cmb_RptYear.Text, 1, 1)
                    If Date.Compare(sdt, dt) < 0 Or Date.Compare(edt, dt) > 0 Then
                        MessageBox.Show("Select Proper Date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'blnIsLoading = True
                        dtpicStartDate.Value = New DateTime(cmb_RptYear.Text, 1, 1)
                        'blnIsLoading = False
                        Return
                    End If
                End If
                dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(89)
                CheckStatus()
                GetValues()
                'SetGridStyle()
                FillData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtpicEndDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpicEndDate.ValueChanged
        If blnIsLoading = False Then
            CheckStatus()
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
                clsPrntRpt.PrintReport("rptMU", "ReportID,User", nReportId & "," & _LoginName, gblnDefaultPrinter, "")

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, "Report Printed", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, "Report Not Printed", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Failure)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SaveReport() As Boolean
        Try
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
            If chk_FirstYear.Checked = True Then
                blnFirstYr = 1
            Else
                blnFirstYr = 0
            End If

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
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Value = cmb_RptYear.SelectedItem
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
            oDB.Execute("MU_InUp_MainMeasures_MST", oParameters, oParameters(0).Value)
            'nReportId = oDB.ExecuteScalar("MU_InUp_MainMeasures_MST")
            nReportId = oParameters(0).Value
            'oParameters = Nothing
            oParameters.Clear()
            '''''''''''''''''''''''''''''''''Insert Or Update In Details Table''''''''''''''''''''''''
            For i = ROW_CPOEMedication To ROW_SyndromicServilance

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

                If i < ROW_MEnuSet Then
                    MeasureType = 1
                ElseIf i > ROW_MEnuSet And i <= ROW_SyndromicServilance Then
                    MeasureType = 2
                End If


                If i = ROW_MEnuSet Then
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

                If i = ROW_CQM Or i = ROW_ElectronicExchange Or i = ROW_ProtectEleHealthInfo Then
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


                    oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
                    oParameters.Clear()

                    Continue For
                ElseIf i = ROW_Generatelistofpatient Or i = ROW_ImmunizationRegistry Or i = ROW_SyndromicServilance Then
                    If Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)) = "None" Then
                        blnFirstYr = 0
                    Else
                        Dim dateNumeric As String = (Convert.ToString(C1QualityMeasures.GetData(i, COL_calNumerator)))
                        blnFirstYr = gloDateMaster.gloDate.DateAsNumber(dateNumeric)
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

                    oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
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

                    If i = ROW_DICheck Or i = ROW_DrugFormularyCheck Then
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

                        oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
                        oParameters.Clear()
                        Continue For

                    ElseIf i = ROW_DecisionSupportRule Then
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


                        oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
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
                        If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(i, COL_Goal).ToString().Trim("%")) Then
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

                    oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
                    oParameters.Clear()
                End If
            Next
            If nReportId > 0 Then
                nId = nReportId
            End If
            If blnFlag Then
                '  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "Report Updated", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Modify, "2011+ dashboard modified with the name '" & txtReportName.Text & "' for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            Else
                '   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "Report Added", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Add, "New MU 2011+ dashboard created with the name '" & txtReportName.Text & "' for the Provider '" & cmb_Provider.Text & "' with the measurement period From '" & Format(dtpicStartDate.Value, "MM/dd/yyyy") & "' to '" & Format(dtpicEndDate.Value, "MM/dd/yyyy") & "'.", 0, 0, cmb_Provider.SelectedValue, gloAuditTrail.ActivityOutCome.Success)
            End If
            Return True
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'MessageBox.Show("Records Save Successfully...!", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
        'str = "Insert Into MU_MainMeasures_MST(nID,sReportName,nProviderID,sProviderName,nReportingYear,blnIsFirstYear,dtStartDate,dtEndDate,sReportingPeriodStatus,sMachineName,sUserName) values(" & 1 & ",'',3,'3',7,'','','','','','')"
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
                ''Sanjog- Added on 20101202 to show the date of the slected year 
                If chk_FirstYear.Checked = True Then
                    Dim day As Int32
                    Dim month As Int32
                    day = dtpicStartDate.Value.Day
                    month = dtpicStartDate.Value.Month
                    dtpicStartDate.Value = New DateTime(cmb_RptYear.Text, month, day)
                    day = dtpicEndDate.Value.Day
                    month = dtpicEndDate.Value.Month
                    dtpicEndDate.Value = Convert.ToDateTime(dtpicStartDate.Value).AddDays(89)
                Else
                    dtpicStartDate.Value = New DateTime(cmb_RptYear.Text, 1, 1)
                    dtpicEndDate.Value = New DateTime(cmb_RptYear.Text, 12, 31)
                    StartDate = New DateTime(cmb_RptYear.Text, 1, 1)
                    Enddate = New DateTime(cmb_RptYear.Text, 12, 31)
                End If
                ''Sanjog- Added on 20101202 to show the date of the slected year
                GetValues()
                'SetGridStyle()
                blnIsLoading = False
                FillData()
                CheckStatus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub cmb_Provider_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Provider.SelectedIndexChanged
    '    Try
    '        If blnIsLoading = False Then
    '            GetValues()
    '            'SetGridStyle()
    '            FillData()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

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
                    If e.Row = ROW_DICheck Or e.Row = ROW_CQM Or e.Row = ROW_DecisionSupportRule Or e.Row = ROW_ElectronicExchange Or e.Row = ROW_ProtectEleHealthInfo Or e.Row = ROW_MEnuSet Or e.Row = ROW_DrugFormularyCheck Or e.Row = ROW_Generatelistofpatient Or e.Row = ROW_ImmunizationRegistry Or e.Row = ROW_SyndromicServilance Then
                        e.Cancel = True
                    End If
                End If

                If e.Row = ROW_MEnuSet Then
                    If e.Col = COL_rptDenominator Or e.Col = COL_rptNumerator Then
                        e.Cancel = True
                    End If
                End If

                If e.Row <= ROW_MEnuSet And e.Col = COL_Check Then
                    e.Cancel = True
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1QualityMeasures.KeyPressEdit

        Try
            'If Convert.ToString(C1QualityMeasures.GetData(e.Row, e.Col)) = "" Then
            'Else
            If Char.IsNumber(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
                e.Handled = True
                'MessageBox.Show("Please enter valid number", "Boat House", MessageBoxButtons.OK)
            End If
            'End If
        Catch
        End Try
    End Sub
    Private Sub ShowNPIandTacID(ByVal pid As Long)
        Dim cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable
            Dim conn As SqlConnection = New SqlConnection(_databaseConnectionString)
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
    'Sanjog -Commented on 2011 May 10 to remove the certification no from MU Dashboard
    'Private Sub ShowCertificationNumber()
    '    Try
    '        Dim dt As New DataTable
    '        Dim conn As SqlConnection = New SqlConnection(_databaseConnectionString)
    '        Try
    '            conn.Open()
    '            Dim cmd1 As SqlCommand = New SqlCommand("select Isnull(sSettingsValue,'') from settings where sSettingsName='gloEMR Certification'", conn)
    '            Dim ad1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
    '            ad1.Fill(dt)
    '            If dt.Rows.Count > 0 Then
    '                lbl_CertificationValue.Text = dt.Rows(0)(0).ToString()
    '            Else
    '                lbl_CertificationValue.Text = "CC-1112-501340-1"
    '            End If
    '            dt = Nothing
    '        Finally
    '            If Not IsNothing(conn) Then
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                End If
    '            End If
    '        End Try
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Sanjog -Commented on 2011 May 10 to remove the certification no from MU Dashboard

    Private Sub C1QualityMeasures_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.DoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1QualityMeasures.HitTest(ptPoint)
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                If (C1QualityMeasures.Row = ROW_DICheck Or C1QualityMeasures.Row = ROW_CQM Or C1QualityMeasures.Row = ROW_DecisionSupportRule Or C1QualityMeasures.Row = ROW_ElectronicExchange Or C1QualityMeasures.Row = ROW_ProtectEleHealthInfo Or C1QualityMeasures.Row = ROW_ElectronicExchange Or C1QualityMeasures.Row = ROW_Generatelistofpatient Or C1QualityMeasures.Row = ROW_ImmunizationRegistry Or C1QualityMeasures.Row = ROW_SyndromicServilance Or C1QualityMeasures.Row = ROW_DrugFormularyCheck) Then
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
                ofrm.SetReportingParameters(MeasureString, cmb_Provider.Text, lbl_NPIValue.Text, cmb_RptYear.Text, txtReportName.Text, lbl_TaxIDValue.Text, chk_FirstYear.Checked, dtpicStartDate.Value, dtpicEndDate.Value, Label5.Text)
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

                    If GetAdminsetting("CPOE MU STAGE 1 2013") = "0" Then
                        Return "MU_CPOE_MedicationOrders_New"
                    Else
                        Return "MU_CPOE_MedicationOrders_MU2013"
                    End If

                Case ROW_DICheck
                    Return "MU_DIChecks"
                Case ROW_ProblemList
                    Return "MU_ProblemUsageReport"
                Case ROW_EPrescription
                    Return "MU_ePrescribedReport"
                Case ROW_ActiveMEdicationList
                    Return "MU_MedicationUsageReport"
                Case ROW_ActiveAllergyList
                    Return "MU_AllergyUsageReport"
                Case ROW_Demographic
                    Return "MU_DemographicUsageReport"
                Case ROW_Vitals

                    If GetAdminsetting("VITAL MU STAGE 1 2013") = "0" Then
                        Return "MU_PatientVitalReport"
                    Else
                        Return "MU_PatientVitalReport_MU2013"
                    End If

                Case ROW_SmokingStatus
                    Return "MU_SmokingStatus"
                Case ROW_DecisionSupportRule
                    Return "MU_DMRulesSetup"
                Case ROW_EleCopyPatientHealth
                    Return "MU_ElecCopyofPatientInfo"
                Case ROW_ClinicalSummaryforPatient
                    Return "MU_ClinicalSummaryUsage"
                Case ROW_DrugFormularyCheck
                    Return "MU_FormularyChecks"
                Case ROW_ClinicallabTest
                    Return "MU_ClinicalTestResults"
                Case ROW_Reminderstopatient
                    Return "MU_PatientReminderUsage"
                Case ROW_ElectronicAccessHealthInfo
                    Return "MU_ElecAccesstoPatientInfo"
                Case ROW_PatientSpecificEducatiion
                    Return "MU_PatientSpecificEducation"
                Case ROW_MedicalReconcilation
                    Return "MU_Medication_Reconciliation"
                Case ROW_SummaryCareTransition
                    Return "MU_SummaryCareTransitions"
                Case ROW_ImmunizationRegistry
                    Return "MU_SubmitImmunization"
                Case ROW_SyndromicServilance
                    Return "MU_Submitsurveillancedata"
                Case Else
                    Return ""
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUReport, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        End Try
    End Function

    Private Sub C1QualityMeasures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1QualityMeasures.Click

    End Sub

    Private Sub C1QualityMeasures_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1QualityMeasures.DoubleClick

    End Sub

    Private Sub cmb_Provider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Provider.SelectedIndexChanged

    End Sub

    Private Sub C1QualityMeasures_SetupEditor(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.SetupEditor
        If e.Col = COL_rptNumerator OrElse e.Col = COL_rptDenominator Then
            If Not IsNothing(C1QualityMeasures.Editor) Then
                Dim txt As TextBox = DirectCast(C1QualityMeasures.Editor, TextBox)
                txt.MaxLength = Convert.ToString(Int64.MaxValue).Length - 1
            End If

        End If
    End Sub
End Class
