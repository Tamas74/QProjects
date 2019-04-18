Imports System.Data.SqlClient
Imports System.IO
Public Class frm_MU_Dashboard
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

    Private ProviderID As Int64
    Private StartDate As String
    Private Enddate As String
    Private Percent As String
    Private nReportId As Int64



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
            C1QualityMeasures.Rows(0).AllowMerging = True
            C1QualityMeasures.Cols(COL_calFlag1).AllowMerging = True
            C1QualityMeasures.Cols(COL_calFlag2).AllowMerging = True
            C1QualityMeasures(0, COL_Check) = "Select"
            C1QualityMeasures(0, COL_calFlag1) = "Flag"
            C1QualityMeasures(0, COL_calFlag2) = "Flag"
            C1QualityMeasures.SetData(0, COL_Measure, "Measure")
            C1QualityMeasures.SetData(0, COL_Goal, "Goal%")
            C1QualityMeasures.SetData(0, COL_calNumerator, "Numerator")
            C1QualityMeasures.SetData(0, COL_calDenominator, "Denominator")
            C1QualityMeasures.SetData(0, COL_calPercent, "Percent")
            'C1QualityMeasures.SetCellImage(0,COL_Copybtn,
            'C1QualityMeasures(0, COL_calFlag2) = "Flag"

            'C1QualityMeasures.SetData(0, COL_calFlag1, "Flag")
            C1QualityMeasures.SetData(0, COL_rptNumerator, "Numerator")
            C1QualityMeasures.SetData(0, COL_rptDenominator, "Denominator")
            C1QualityMeasures.SetData(0, COL_rptPercent, "Percent")
            C1QualityMeasures.SetData(0, COL_rptFlag1, "Flag")
            C1QualityMeasures.SetData(0, COL_rptFlag2, "Flag")


            C1QualityMeasures.Cols(COL_Check).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
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
            C1QualityMeasures.Cols(COL_Measure).Width = Width * 0.25
            C1QualityMeasures.Cols(COL_Goal).Width = Width * 0.05
            C1QualityMeasures.Cols(COL_Comment).Width = Width * 0.015
            C1QualityMeasures.Cols(COL_calNumerator).Width = Width * 0.09
            C1QualityMeasures.Cols(COL_calDenominator).Width = Width * 0.1
            C1QualityMeasures.Cols(COL_calPercent).Width = Width * 0.06
            C1QualityMeasures.Cols(COL_calFlag1).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_calFlag2).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_Copybtn).Width = Width * 0.09
            C1QualityMeasures.Cols(COL_rptNumerator).Width = Width * 0.09
            C1QualityMeasures.Cols(COL_rptDenominator).Width = Width * 0.1
            C1QualityMeasures.Cols(COL_rptPercent).Width = Width * 0.06
            C1QualityMeasures.Cols(COL_rptFlag1).Width = Width * 0.02
            C1QualityMeasures.Cols(COL_rptFlag2).Width = Width * 0.02



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
            C1QualityMeasures.Cols(COL_rptPercent).AllowEditing = True
            C1QualityMeasures.Cols(COL_rptFlag1).AllowEditing = False
            C1QualityMeasures.Cols(COL_rptFlag2).AllowEditing = False

            C1QualityMeasures.Rows.Add(26)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillData()
        Try
            Dim dt As DataTable = Nothing

            Dim i As Integer = 1
            C1QualityMeasures.SetData(i, COL_Measure, "Use CPOE for medication orders")
            C1QualityMeasures.SetData(i, COL_Goal, "30%")

            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Copybtn, "")
            'C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
            'C1QualityMeasures.SetCellImage(i, COL_calFlag2, ImgFlag.Images(0))
            C1QualityMeasures.SetData(i, COL_Comment, "Use CPOE for medication orders directly entered by any licensed healthcare professional who can enter orders into the medical record per state, local and professional guidelines")



            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Implement Drug Interaction Checks")
            C1QualityMeasures.SetData(i, COL_Goal, "")
            C1QualityMeasures.SetData(i, COL_Comment, "Implement drug-drug and drug-allergy interaction checks")
            dt = Getdata("MU_DIChecks")
            If (IsNothing(dt) = False) Then
                If dt.Rows(0)(0).ToString() = "1" Then
                    C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
                Else
                    C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                End If
                dt.Dispose()
                dt = Nothing
            End If
            
            ' C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Maintain Problem List")
            C1QualityMeasures.SetData(i, COL_Goal, "80%")
            C1QualityMeasures.SetData(i, COL_Comment, "Maintain an up-to-date problem list of current and active diagnoses")

            dt = GetdataWithParam("MU_ProblemUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If (IsNothing(dt) = False) Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(0)(1).ToString())
                dt.Dispose()
                dt = Nothing

            End If
            If Percent <> "N/A" Then
                If Convert.ToUInt64(FormatNumber(Percent, 0, TriState.True)) < 80 Then
                    C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                End If
                C1QualityMeasures.SetData(i, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
            Else
                C1QualityMeasures.SetData(i, COL_calPercent, Percent)
            End If

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Electronic prescriptions")
            C1QualityMeasures.SetData(i, COL_Goal, "40%")
            C1QualityMeasures.SetData(i, COL_Comment, "Generate and transmit permissible prescriptions electronically (eRx)")


            dt = GetdataWithParam("MU_ePrescribedReport", ProviderID.ToString(), StartDate, Enddate)
            If (IsNothing(dt) = False) Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(0)(1).ToString())
                dt.Dispose()
                dt = Nothing

            End If

            If Percent <> "N/A" Then
                If Convert.ToUInt64(FormatNumber(Percent, 0, TriState.True)) < 40 Then
                    C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                End If
                C1QualityMeasures.SetData(i, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
            Else
                C1QualityMeasures.SetData(i, COL_calPercent, Percent)
            End If


            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Maintain active medication list")
            C1QualityMeasures.SetData(i, COL_Goal, "80%")
            C1QualityMeasures.SetData(i, COL_Comment, "Maintain active medication list")

            C1QualityMeasures.SetData(i, COL_Comment, "Maintain active medication allergy list")
            dt = GetdataWithParam("MU_MedicationUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If (IsNothing(dt) = False) Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())

                C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(0)(1).ToString())
                dt.Dispose()
                dt = Nothing

            End If

            If Percent <> "N/A" Then
                If Convert.ToUInt64(FormatNumber(Percent, 0, TriState.True)) < 80 Then
                    C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                End If
                C1QualityMeasures.SetData(i, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
            Else
                C1QualityMeasures.SetData(i, COL_calPercent, Percent)
                C1QualityMeasures.SetData(i, COL_calFlag1, "")
                C1QualityMeasures.SetData(i, COL_calFlag2, "")
            End If

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Maintain active med. allergy list")
            C1QualityMeasures.SetData(i, COL_Goal, "80%")

            C1QualityMeasures.SetData(i, COL_Comment, "Maintain active medication allergy list")
            dt = GetdataWithParam("MU_AllergyUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If (IsNothing(dt) = False) Then

                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(0)(1).ToString())
                dt.Dispose()
                dt = Nothing
            End If

            If Percent <> "N/A" Then
                If Convert.ToUInt64(FormatNumber(Percent, 0, TriState.True)) < 80 Then
                    C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                End If
                C1QualityMeasures.SetData(i, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
            Else
                C1QualityMeasures.SetData(i, COL_calPercent, Percent)
            End If

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Record demographics")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")


            dt = GetdataWithParam("MU_DemographicUsageReport", ProviderID.ToString(), StartDate, Enddate)
            If (IsNothing(dt) = False) Then
                Percent = CalculatePercent(dt.Rows(0)(0).ToString(), dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(i, COL_calNumerator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(i, COL_calDenominator, dt.Rows(0)(1).ToString())
                dt.Dispose()
                dt = Nothing
            End If

            If Percent <> "N/A" Then
                If Convert.ToUInt64(FormatNumber(Percent, 0, TriState.True)) < 50 Then
                    C1QualityMeasures.SetCellImage(i, COL_calFlag1, ImgFlag.Images(2))
                End If
                C1QualityMeasures.SetData(i, COL_calPercent, FormatNumber(Percent, 2, TriState.True) & "%")
            Else
                C1QualityMeasures.SetData(i, COL_calPercent, Percent)
            End If
            C1QualityMeasures.SetData(i, COL_Comment, "Record demographics " & vbNewLine & "o preferred language" & vbNewLine & "o gender" & vbNewLine & "o race" & vbNewLine & "o ethnicity" & vbNewLine & "o date of birth")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Record and chart vital signs")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Record and chart changes in vital signs:" & vbCrLf & "o Height" & vbCrLf & "o weight" & vbNewLine & "o blood pressure" & vbNewLine & "o Calculate And display : BMI" & vbNewLine & "  o Plot and display growth charts for children 2-20 years, including BMI.")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Record smoking status")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Record smoking status for patients 13 years old or older")

            ' C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Report clinical quality measures")
            C1QualityMeasures.SetData(i, COL_Goal, "")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Report ambulatory clinical quality measures to CMS or the States")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Clinical decision support rule")
            C1QualityMeasures.SetData(i, COL_Comment, "Implement one clinical decision support rule relevant to specialty or high clinical priority along with the ability to track compliance that rule")
            dt = Getdata("MU_DMRulesSetup")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(i, COL_calNumerator, "DM Rules Set Up: " & dt.Rows(0)(0).ToString())
                dt.Dispose()
                dt = Nothing
            End If
         
            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Electronic copy of pat. health info.")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Provide patients with an electronic copy of their health information (including diagnostic test results, problem list, medication lists, medication allergies), upon request")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Clinical summaries for patients")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Provide clinical summaries for patients for each office visit")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Electronically exchange clinical info.")
            C1QualityMeasures.SetData(i, COL_Comment, "Capability to exchange key clinical information (for example, problem list, medication list, medication allergies, diagnostic test results), among providers of care and patient authorized entities electronically")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Protect electronic health information")
            C1QualityMeasures.SetData(i, COL_Goal, "")
            C1QualityMeasures.SetData(i, COL_calNumerator, "")
            C1QualityMeasures.SetData(i, COL_calDenominator, "")
            C1QualityMeasures.SetData(i, COL_calPercent, "")
            C1QualityMeasures.SetData(i, COL_Comment, "Protect electronic health information created or maintained by the certified EHR technology through the implementation of appropriate technical capabilities")


            Dim j As Integer
            For j = 1 To 15
                C1QualityMeasures.Rows(j).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(j, COL_Comment, ImgFlag.Images(1))
                C1QualityMeasures.SetCellCheck(j, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next

            ''''''''''''''''''''''''''''''''''''''''''''
            ''''Menu Set Measure
            '''''''''''''''''''''''''''''''''''''''''''''
            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.Rows(i).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
            C1QualityMeasures.Rows(i).StyleNew.ForeColor = Color.White
            C1QualityMeasures.Rows(i).StyleNew.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            C1QualityMeasures.Rows(16).AllowMerging = True
            'C1QualityMeasures.Cols(COL_calFlag1).AllowMerging = "Flag"
            'C1QualityMeasures.Cols(COL_calFlag2).AllowMerging = "Flag"
            C1QualityMeasures(i, COL_calFlag1) = "Flag"
            C1QualityMeasures(i, COL_calFlag2) = "Flag"

            C1QualityMeasures.SetData(i, COL_Measure, "Measure")
            C1QualityMeasures.SetData(i, COL_Goal, "Goal%")
            C1QualityMeasures.SetCellStyle(i, COL_Check, New C1.Win.C1FlexGrid.TextAlignEnum)
            C1QualityMeasures.SetData(i, COL_calNumerator, "Numerator")
            C1QualityMeasures.SetData(i, COL_calDenominator, "Denominator")
            C1QualityMeasures.SetData(i, COL_calPercent, "Percent")


            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Implement drug formulary checks")
            C1QualityMeasures.SetData(i, COL_Comment, "Implement drug formulary checks")
            dt = Getdata("MU_FormularyChecks")
            If (IsNothing(dt) = False) Then

                If dt.Rows(0)(0).ToString() = "1" Then
                    C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
                Else
                    C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                End If
                dt.Dispose()
                dt = Nothing
            Else
                C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")

            End If

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Incorporate clinical lab-test results")
            C1QualityMeasures.SetData(i, COL_Goal, "40%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Incorporate clinical lab-test results into EHR as structured data")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Generate lists of patients")
            C1QualityMeasures.SetData(i, COL_Comment, "Generate lists of patients by specific conditions to use for quality improvement, reduction of disparities, research or outreach")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Send reminders to patients")
            C1QualityMeasures.SetData(i, COL_Goal, "20%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Send reminders to patients per patient preference for preventive/ follow up care")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Electronic access to health info.")
            C1QualityMeasures.SetData(i, COL_Goal, "10%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Provide patients with timely electronic access to their health information (including lab results, problem list, medication lists, medication allergies) within four business days of the information being available to the EP")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Patient-specific education resources")
            C1QualityMeasures.SetData(i, COL_Goal, "10%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "Use certified EHR technology to identify patient-specific education resources and provide those resources to the patient if appropriate")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Medication reconciliation")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "The EP, eligible hospital or CAH who receives a patient from another setting of care or provider of care or believes an encounter is relevant should perform medication reconciliation")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Summary provided at care transitions")
            C1QualityMeasures.SetData(i, COL_Goal, "50%")
            C1QualityMeasures.SetData(i, COL_calNumerator, "0")
            C1QualityMeasures.SetData(i, COL_calDenominator, "0")
            C1QualityMeasures.SetData(i, COL_calPercent, "N/A")
            C1QualityMeasures.SetData(i, COL_Comment, "The EP, eligible hospital or CAH who transitions their patient to another setting of care or provider of care or refers their patient to another provider of care should provide summary of care record for each transition of care or referral")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Submit data to imm. registries or IIS")
            C1QualityMeasures.SetData(i, COL_Comment, "Capability to submit electronic data to immunization registries or Immunization Information Systems and actual submission in accordance with applicable law and practice")

            'C1QualityMeasures.Rows.Add()
            i = i + 1
            C1QualityMeasures.SetData(i, COL_Measure, "Submit syndromic surveillance data")
            C1QualityMeasures.SetData(i, COL_Comment, "Capability to submit electronic syndromic surveillance data to public health agencies and actual submission in accordance with applicable law and practice")


            Dim rg As C1.Win.C1FlexGrid.CellRange
            rg = C1QualityMeasures.GetCellRange(1, COL_Copybtn, 1, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(3, COL_Copybtn, 9, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(12, COL_Copybtn, 13, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(16, COL_Copybtn, 16, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(18, COL_Copybtn, 18, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            rg = C1QualityMeasures.GetCellRange(20, COL_Copybtn, 24, COL_Copybtn)
            rg.StyleNew.ComboList = "..."

            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            For j = 17 To 26
                C1QualityMeasures.Rows(j).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
                C1QualityMeasures.SetCellImage(j, COL_Comment, ImgFlag.Images(1))
                C1QualityMeasures.SetCellCheck(j, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub frm_MU_Dashboard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetGridStyle()
            FillProvider()
            FillYear()
            GetValues()
            FillData()
            If nReportId = 0 Then
                FillData()
            Else
                FillData()
                FillFromDT(nReportId)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GetValues()
        ProviderID = cmb_Provider.SelectedValue.ToString()
        If chk_FirstYear.Checked = True Then
            StartDate = dtpicStartDate.Value.ToString()
            Enddate = dtpicEndDate.Value.ToString()
        Else
            StartDate = New DateTime(cmb_RptYear.Text, 1, 1)
            Enddate = New DateTime(cmb_RptYear.Text, 12, 31)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Sub C1QualityMeasures_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.CellButtonClick
        Try
            Dim rowno As Integer
            Dim k As Integer
            Dim Perc As Single
            rowno = C1QualityMeasures.RowSel()
            If rowno = 0 Then
                For k = 1 To 15
                    If k = 2 Or k = 10 Or k = 11 Or k = 14 Or k = 15 Then
                        Continue For
                    End If
                    C1QualityMeasures.SetData(k, COL_rptNumerator, C1QualityMeasures.GetData(k, COL_calNumerator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptDenominator, C1QualityMeasures.GetData(k, COL_calDenominator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptPercent, C1QualityMeasures.GetData(k, COL_calPercent).ToString())
                    If C1QualityMeasures.GetData(k, COL_calPercent).ToString() <> "N/A" Then
                        Perc = Convert.ToSingle(C1QualityMeasures.GetData(k, COL_calPercent).ToString().Trim("%"))
                        If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(k, COL_Goal).ToString().Trim("%")) Then
                            C1QualityMeasures.SetCellImage(k, COL_rptFlag1, Image.FromFile("\\glosvr01\Integration\Design\gloEMR 5.0\16x16 Icon\FlagRed.png"))
                        End If
                    End If
                Next
            ElseIf rowno = 16 Then

                For k = 17 To C1QualityMeasures.Rows.Count - 1
                    If k = 17 Or k = 19 Or k = 25 Or k = 26 Then
                        Continue For
                    End If
                    C1QualityMeasures.SetData(k, COL_rptNumerator, C1QualityMeasures.GetData(k, COL_calNumerator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptDenominator, C1QualityMeasures.GetData(k, COL_calDenominator).ToString())
                    C1QualityMeasures.SetData(k, COL_rptPercent, C1QualityMeasures.GetData(k, COL_calPercent).ToString())
                    If C1QualityMeasures.GetData(k, COL_calPercent).ToString() <> "N/A" Then
                        Perc = Convert.ToSingle(C1QualityMeasures.GetData(k, COL_calPercent).ToString().Trim("%"))
                        If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(k, COL_Goal).ToString().Trim("%")) Then
                            C1QualityMeasures.SetCellImage(k, COL_rptFlag1, Image.FromFile("\\glosvr01\Integration\Design\gloEMR 5.0\16x16 Icon\FlagRed.png"))
                        End If
                    End If
                Next
            Else
                C1QualityMeasures.SetData(rowno, COL_rptNumerator, C1QualityMeasures.GetData(rowno, COL_calNumerator).ToString())
                C1QualityMeasures.SetData(rowno, COL_rptDenominator, C1QualityMeasures.GetData(rowno, COL_calDenominator).ToString())
                C1QualityMeasures.SetData(rowno, COL_rptPercent, C1QualityMeasures.GetData(rowno, COL_calPercent).ToString())
                If C1QualityMeasures.GetData(rowno, COL_calPercent).ToString() <> "N/A" Then
                    Perc = Convert.ToSingle(C1QualityMeasures.GetData(rowno, COL_calPercent).ToString().Trim("%"))
                    If Convert.ToInt64(FormatNumber(Perc, 0, TriState.True)) < Convert.ToInt64(C1QualityMeasures.GetData(rowno, COL_Goal).ToString().Trim("%")) Then
                        C1QualityMeasures.SetCellImage(rowno, COL_rptFlag1, Image.FromFile("\\glosvr01\Integration\Design\gloEMR 5.0\16x16 Icon\FlagRed.png"))
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1QualityMeasures_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1QualityMeasures.MouseMove
        If C1QualityMeasures.MouseCol = 3 Then
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        Else

        End If

    End Sub
    Private Sub FillProvider()
        cmb_Provider.Items.Clear()
        Dim clsPat As New clsPatient
        Dim dt As DataTable = Nothing
        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        dt = clsPat.GetProviders(Convert.ToInt64(appSettings("ClinicID")))
        cmb_Provider.ValueMember = "nProviderID"
        cmb_Provider.DisplayMember = "ProviderName"
        cmb_Provider.DataSource = dt
        clsPat = Nothing
        'cmb_Provider.Items.Insert(0, "Select")
    End Sub
    Private Sub FillYear()
        cmb_RptYear.Items.Clear()
        Dim i As Integer
        For i = 0 To 11
            cmb_RptYear.Items.Add("20" & i + 10)
        Next
        cmb_RptYear.SelectedIndex = 0
    End Sub

    Private Sub chk_FirstYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_FirstYear.CheckedChanged
        If chk_FirstYear.Checked = True Then
            'pnlMeasurementPeriod.Visible = True
            dtpicEndDate.Enabled = True
            dtpicStartDate.Enabled = True
        Else
            'pnlMeasurementPeriod.Visible = False
            dtpicEndDate.Enabled = False
            dtpicStartDate.Enabled = False
        End If
        CheckStatus()
    End Sub
    Private Sub CheckStatus()
        Try
            Dim i As Int64
            Dim j As Int64
            If chk_FirstYear.Checked = True Then
                If Date.Compare(dtpicStartDate.Value.ToShortDateString(), dtpicEndDate.Value.ToShortDateString()) <= 0 Then
                    i = Date.Compare(dtpicStartDate.Value.ToShortDateString(), Date.Now.ToShortDateString())
                    j = Date.Compare(dtpicEndDate.Value.ToShortDateString(), Date.Now.ToShortDateString())
                    If i >= 0 AndAlso j <= 0 Then
                        Label5.Text = "Reporting Period In Progress"
                    ElseIf i < 0 AndAlso j < 0 Then
                        Label5.Text = "Reporting Period Complete"
                    ElseIf i < 0 AndAlso j <= 0 Then
                        Label5.Text = "Reporting Period In Progress"
                    ElseIf i > 0 AndAlso j > 0 Then
                        Label5.Text = "Reporting Period Not Started"
                    ElseIf i >= 0 AndAlso j >= 0 Then
                        Label5.Text = "Reporting Period Not Started"
                    ElseIf i <= 0 AndAlso j >= 0 Then
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function Getdata(ByVal SPName As String) As DataTable
        Try
            Dim Con As New SqlConnection(GetConnectionString)
            Dim cmd As New SqlCommand("" + SPName + "", Con)
            cmd.CommandType = CommandType.StoredProcedure
            ' Dim objParam As SqlParameter
            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
            Con.Dispose()
            Con = Nothing
            da.Dispose()
            da = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Function GetdataWithParam(ByVal SPName As String, ByVal providerid As String, ByVal startdate As String, ByVal enddate As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try
            'Dim cmd As New SqlCommand("" + SPName + "", Con)
            'cmd.CommandType = CommandType.StoredProcedure
            'Dim objParam As New SqlParameter
            'cmd.Parameters.Add("@Provider", SqlDbType.BigInt, 18, providerid)
            'cmd.Parameters.AddWithValue("@FromDate", startdate)
            'cmd.Parameters.AddWithValue("@ToDate", enddate)
            'Dim da As New SqlDataAdapter
            'Dim dt As New DataTable
            'da.SelectCommand = cmd
            'da.Fill(dt)
            'Return dt



            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim dt As DataTable = Nothing

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

            oParameters.Dispose()
            oParameters = Nothing

            oDB.Disconnect()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    Public Sub New(ByVal reportID As Int64)
        nReportId = reportID
        InitializeComponent()
    End Sub
    Public Sub New()
        nReportId = 0
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub dtpicStartDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpicStartDate.ValueChanged
        CheckStatus()
    End Sub

    Private Sub dtpicEndDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpicEndDate.ValueChanged
        CheckStatus()
    End Sub

    Private Sub tlb_MUDashboard_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlb_MUDashboard.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveReport()
                Case "Print"
                    GetValues()
                    SetGridStyle()
                    FillData()
                Case "Close"
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Case "Save&Close"
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SaveReport()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Try
            GetValues()
            ' Dim str As String
            'Dim rowno As Int64
            Dim i As Integer
            Dim Perc As Single
            Dim blnFirstYr As Int32 = False
            Dim isSelect As Int32 = False
            If chk_FirstYear.Checked = True Then
                blnFirstYr = 1
            Else
                blnFirstYr = 0
            End If
            '''''''''''''''''''''''''Insert Or Update In Master Table''''''''''''''''''''''''''


            Dim oParameter As gloDatabaseLayer.DBParameter

            oDB.Connect(False)
            Dim dt As DataTable = Nothing

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
            oParameter.Value = "Report One"
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
            oParameter.Value = gstrClientMachineName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@sUserName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gstrLoginName
            oParameters.Add(oParameter)
            oParameter = Nothing
            '''''''''''''''''''''''''
            oDB.Execute("MU_InUp_MainMeasures_MST", oParameters, oParameters(0).Value)
            'nReportId = oDB.ExecuteScalar("MU_InUp_MainMeasures_MST")
            nReportId = oParameters(0).Value
            'oParameters = Nothing
            oParameters.Clear()
            '''''''''''''''''''''''''''''''''Insert Or Update In Details Table''''''''''''''''''''''''
            For i = 1 To C1QualityMeasures.Rows.Count - 1
                If i = 16 Then
                    Continue For
                End If

                Dim checkval As C1.Win.C1FlexGrid.CheckEnum
                checkval = C1QualityMeasures.GetCellCheck(i, 0)
                If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    isSelect = 1
                Else
                    isSelect = 0
                End If

                If i = 10 Or i = 14 Or i = 15 Or i = 19 Or i = 25 Or i = 26 Then
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

                    oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
                    oParameters.Clear()

                    Continue For
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

                    If i = 2 Or i = 17 Then
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

                        oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
                        oParameters.Clear()
                        Continue For

                    ElseIf i = 11 Then
                        Dim str1 As Array
                        oParameter = New gloDatabaseLayer.DBParameter()
                        oParameter.ParameterName = "@nCalc_Numerator"
                        oParameter.ParameterDirection = ParameterDirection.Input
                        oParameter.DataType = SqlDbType.BigInt
                        str1 = C1QualityMeasures.GetData(i, COL_calNumerator).ToString().Split(":")
                        If (str1.Length > 1) Then
                            oParameter.Value = str1.GetValue(1).ToString().Trim()
                        End If

                        oParameters.Add(oParameter)
                        oParameter = Nothing

                        oParameter = New gloDatabaseLayer.DBParameter()
                        oParameter.ParameterName = "@nLineNo"
                        oParameter.ParameterDirection = ParameterDirection.Input
                        oParameter.DataType = SqlDbType.Int
                        oParameter.Value = i
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

                    oDB.Execute("MU_InUp_MainMeasures_DTL", oParameters)
                    oParameters.Clear()
                End If
            Next
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            MessageBox.Show("Records Save Successfully...!", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oParameters.Clear()
            oParameters.Dispose()
            oParameters = Nothing
            oDB.Dispose()
            oDB = Nothing

        End Try
        'str = "Insert Into MU_MainMeasures_MST(nID,sReportName,nProviderID,sProviderName,nReportingYear,blnIsFirstYear,dtStartDate,dtEndDate,sReportingPeriodStatus,sMachineName,sUserName) values(" & 1 & ",'',3,'3',7,'','','','','','')"
    End Sub
    Private Sub FillFromDT(ByVal rptid As Int64)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Try
            Dim dt As DataTable = Nothing
            Dim i As Integer

            Dim oParameter As gloDatabaseLayer.DBParameter

            oDB.Connect(False)

            'Dim cmd As New SqlCommand("MU_Select_MainMeasure_MST", Con)
            'cmd.CommandType = CommandType.StoredProcedure
            'Dim objParam As SqlParameter
            'objParam.Value = rptid
            'cmd.Parameters.Add(objParam)
            'Dim da As New SqlDataAdapter
            'da.SelectCommand = cmd
            'da.Fill(dt)

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
                cmb_Provider.SelectedValue = Convert.ToInt64(dt.Rows(0)(1))
                cmb_RptYear.Text = dt.Rows(0)(2).ToString()
                If Convert.ToString(dt.Rows(0)(3)) = True Then
                    chk_FirstYear.Checked = True

                Else
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
                        'Dim checkval As C1.Win.C1FlexGrid.CheckEnum
                        If i = 16 Then
                            Continue For
                        End If
                        If dt.Rows(dtrwno)(0) = True Then
                            C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Checked)
                        Else
                            C1QualityMeasures.SetCellCheck(i, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                        End If
                        C1QualityMeasures.SetData(i, COL_Measure, dt.Rows(dtrwno)(1))
                        If i = 2 Or i = 17 Then
                            If Convert.ToString(dt.Rows(dtrwno)(3)) = "1" Then
                                C1QualityMeasures.SetData(i, COL_calNumerator, "Enabled")
                            Else
                                C1QualityMeasures.SetData(i, COL_calNumerator, "Disabled")
                            End If
                            dtrwno = dtrwno + 1
                            Continue For
                        ElseIf i = 11 Then
                            C1QualityMeasures.SetData(i, COL_calNumerator, "DM Rules Set Up: " & dt.Rows(dtrwno)(3))
                            dtrwno = dtrwno + 1
                            Continue For
                        Else

                            'C1QualityMeasures.SetData(i, COL_Goal, dt.Rows(dtrwno)(2) & "%")
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
                            C1QualityMeasures.SetData(i, COL_rptPercent, dt.Rows(dtrwno)(9))
                        End If
                        dtrwno = dtrwno + 1
                    Next

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oParameters.Clear()
            oParameters.Dispose()
            oParameters = Nothing
            oDB.Dispose()
            oDB = Nothing

        End Try

        
    End Sub
End Class