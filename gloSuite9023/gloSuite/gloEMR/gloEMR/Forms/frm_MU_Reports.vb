Imports System.Data.SqlClient
Imports System.IO

Public Class frm_MU_Reports
    Private COL_Check As Integer = 0
    Private COL_CoreMeasure As Integer = 1
    Private COL_Button As Integer = 2
    Private COL_Numeratot As Integer = 3
    Private COL_Denomenator As Integer = 4
    Private COL_Exclusion As Integer = 5
    Private COL_Percent As Integer = 6
    Dim j As Integer = 0
    Private _databaseConnectionString As String
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    ' Dim Con As New SqlConnection(GetConnectionString)

    ' Private oListControl As gloListControl.gloListControl
    Dim oListhistoryItem As gloListControl.gloListControl
    Private HistoryItemList As gloGeneralItem.gloItems
    Dim _HistoryItemList As String = ""
    Private NQF0028bHistoryItemList As gloGeneralItem.gloItems
    Dim _NQF0028bHistoryItemList As String = ""

    Dim _ClinicID As Int64 = 0
    Private _IsformLoading As Boolean = False

    Private Sub frm_MU_Reports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillProviders()
        SetGridStyle()
        FillGrid()
    End Sub
    Private Sub SetGridStyle()
        'If IsNothing(HistoryItemList) = False Then
        '    For i As Integer = 0 To HistoryItemList.Count - 1
        '        oListhistoryItem.SelectedItems.Add(HistoryItemList(i))
        '    Next
        'End If
        C1QualityMeasures.Cols.Count = 7
        C1QualityMeasures.Rows.Add()
        'C1QualityMeasures.ScrollBars = ScrollBars.None
        C1QualityMeasures.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        C1QualityMeasures.Cols(COL_Check).AllowEditing = True
        C1QualityMeasures.Cols(COL_Button).AllowEditing = True
        C1QualityMeasures.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        gloC1FlexStyle.Style(C1QualityMeasures)
        C1QualityMeasures.SetData(0, COL_Check, "Select")
        C1QualityMeasures.SetData(0, COL_CoreMeasure, "Core Measures")
        C1QualityMeasures.SetData(0, COL_Button, "...")
        C1QualityMeasures.SetData(0, COL_Numeratot, "Numerator")
        C1QualityMeasures.SetData(0, COL_Denomenator, "Denominator")
        C1QualityMeasures.SetData(0, COL_Exclusion, "Exclusions")
        C1QualityMeasures.SetData(0, COL_Percent, "Percent")
        'C1QualityMeasures.Cols(COL_Check).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

        C1QualityMeasures.Cols(COL_Denomenator).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Numeratot).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Exclusion).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Percent).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        C1QualityMeasures.Cols(COL_Button).AllowResizing = False
        ' C1QualityMeasures.Cols(COL_Check).ImageAlignFixed = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter

        ''Visible
        C1QualityMeasures.Cols(COL_Check).Visible = True
        C1QualityMeasures.Cols(COL_CoreMeasure).Visible = True
        C1QualityMeasures.Cols(COL_Button).Visible = True
        C1QualityMeasures.Cols(COL_Numeratot).Visible = True
        C1QualityMeasures.Cols(COL_Denomenator).Visible = True
        C1QualityMeasures.Cols(COL_Exclusion).Visible = True
        C1QualityMeasures.Cols(COL_Percent).Visible = True


        ''Width
        'C1QualityMeasures.Cols(COL_Check).Width = Width * 0.1
        'C1QualityMeasures.Cols(COL_CoreMeasure).Width = Width * 0.7
        'C1QualityMeasures.Cols(COL_Button).Width = Width / 38
        'C1QualityMeasures.Cols(COL_Numeratot).Width = Width * 0.2
        'C1QualityMeasures.Cols(COL_Denomenator).Width = Width * 0.25
        'C1QualityMeasures.Cols(COL_Exclusion).Width = Width * 0.15
        'C1QualityMeasures.Cols(COL_Percent).Width = Width * 0.15

        C1QualityMeasures.Cols(COL_Check).Width = Width * 0.05
        C1QualityMeasures.Cols(COL_CoreMeasure).Width = Width * 0.65
        C1QualityMeasures.Cols(COL_Button).Width = Width / 45
        C1QualityMeasures.Cols(COL_Numeratot).Width = Width * 0.1
        C1QualityMeasures.Cols(COL_Denomenator).Width = Width * 0.1
        C1QualityMeasures.Cols(COL_Exclusion).Width = Width * 0.1
        C1QualityMeasures.Cols(COL_Percent).Width = Width * 0.1


        ''Editing
        C1QualityMeasures.Cols(COL_CoreMeasure).AllowEditing = False
        C1QualityMeasures.Cols(COL_Numeratot).AllowEditing = False
        C1QualityMeasures.Cols(COL_Denomenator).AllowEditing = False
        C1QualityMeasures.Cols(COL_Exclusion).AllowEditing = False
        C1QualityMeasures.Cols(COL_Percent).AllowEditing = False

    End Sub
    Private Function GetHistoryItems() As String
        Try

            Dim i As Int16
            If Not IsNothing(HistoryItemList) Then


                For i = 0 To HistoryItemList.Count - 1
                    If _HistoryItemList = "" Then
                        ''_HistoryItemList = "'" & HistoryItemList.Item(i).Description & "'"
                        _HistoryItemList = HistoryItemList.Item(i).Description
                    Else
                        ' _HistoryItemList = _HistoryItemList & "," & "'" & HistoryItemList.Item(i).Description & "'"
                        _HistoryItemList = _HistoryItemList & "," & HistoryItemList.Item(i).Description
                    End If
                    ''


                    ''
                Next
            End If
            Return _HistoryItemList
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function GetNQF0028bHistoryItems() As String
        Try

            Dim i As Int16
            If Not IsNothing(NQF0028bHistoryItemList) Then


                For i = 0 To NQF0028bHistoryItemList.Count - 1
                    If _NQF0028bHistoryItemList = "" Then
                        ''_HistoryItemList = "'" & HistoryItemList.Item(i).Description & "'"
                        _NQF0028bHistoryItemList = NQF0028bHistoryItemList.Item(i).Description
                    Else
                        ' _HistoryItemList = _HistoryItemList & "," & "'" & HistoryItemList.Item(i).Description & "'"
                        _NQF0028bHistoryItemList = _NQF0028bHistoryItemList & "," & NQF0028bHistoryItemList.Item(i).Description
                    End If
                    ''


                    ''
                Next
            End If
            Return _NQF0028bHistoryItemList
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub FillGrid()
        Try
            _IsformLoading = True
            ' Dim dt As DataTable = New DataTable()
            '    Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            C1QualityMeasures.SetData(1, COL_CoreMeasure, "NQF 0421 - Adult Weight Screening and Follow-Up")
            C1QualityMeasures.Rows.Add(170)
            'C1QualityMeasures.Cols(COL_Check).StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            Dim i As Integer
            For i = 1 To 170
                C1QualityMeasures.Rows(i).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            Next


            C1QualityMeasures.Rows(1).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            'C1QualityMeasures.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetCellCheck(1, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)

            C1QualityMeasures.SetData(2, COL_CoreMeasure, Space(3) & "Population 1: Age  65 years or older ")
            C1QualityMeasures.SetData(3, COL_CoreMeasure, Space(5) & "Numerator 1:  BMI calculated and a follow-up plan if BMI outside parameters")
            'dt = CalculatePercent("MU_NQF0421")
            'C1QualityMeasures.SetData(3, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(3, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(3, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(3, COL_Percent, dt.Rows(0)(3).ToString() & "%")

            C1QualityMeasures.SetData(4, COL_CoreMeasure, Space(3) & "Population 2: Age 18 years to 64 years")
            C1QualityMeasures.SetData(5, COL_CoreMeasure, Space(5) & "Numerator 2:  BMI calculated and a follow-up plan if BMI outside parameters")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0421_POP2")
            'C1QualityMeasures.SetData(5, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(5, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(5, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(5, COL_Percent, dt.Rows(0)(3).ToString() & "%")
           
           

            C1QualityMeasures.SetCellCheck(6, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(6).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(6, COL_CoreMeasure, "NQF 0013 - Hypertension: Blood Pressure Measurement")
            C1QualityMeasures.SetData(7, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years or older with a diagnosis of hypertension")
            C1QualityMeasures.SetData(8, COL_CoreMeasure, Space(5) & "Numerator:  Patients with BP (Systolic and Diastolic) recorded.")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0013")
            'C1QualityMeasures.SetData(8, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(8, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(8, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(8, COL_Percent, dt.Rows(0)(3).ToString() & "%")

            C1QualityMeasures.SetCellCheck(9, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(9).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(9, COL_CoreMeasure, "NQF 0028a: Preventive Care and Screening Measure Pair: a.Tobacco Use Assessment")
            C1QualityMeasures.SetData(10, COL_CoreMeasure, Space(3) & "Population 1: Age 18 years or older")
            C1QualityMeasures.SetData(11, COL_CoreMeasure, Space(5) & "Numerator: Patients who were queried about tobacco use")

            
            C1QualityMeasures.SetCellCheck(12, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(12).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(12, COL_CoreMeasure, "NQF 0028b: Preventive Care and Screening Measure Pair: b.Tobacco Cessation Intervention")
            C1QualityMeasures.SetData(13, COL_CoreMeasure, Space(3) & "Population 1: Tobacco users age 18 years or older")
            C1QualityMeasures.SetData(14, COL_CoreMeasure, Space(5) & "Numerator: Patients who received cessation intervention.")

            C1QualityMeasures.SetCellCheck(15, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(15).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(15, COL_CoreMeasure, "NQF 0041: Preventive Care and Screening: Influenza Immunization for Patients ≥ 50 Years Old")
            C1QualityMeasures.SetData(16, COL_CoreMeasure, Space(3) & "Population 1: Age 50 years or older")
            C1QualityMeasures.SetData(17, COL_CoreMeasure, Space(5) & "Numerator:  Patients who received influenza vaccination.")

            C1QualityMeasures.SetCellCheck(18, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(18).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(18, COL_CoreMeasure, "NQF 0024: Weight Assessment and Counseling for Children and Adolescents")
            C1QualityMeasures.SetData(19, COL_CoreMeasure, Space(3) & "Population 1: Age 2 years to 16 years with an encounter and not pregnant")
            C1QualityMeasures.SetData(20, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.SetData(21, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.SetData(22, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")

            C1QualityMeasures.SetCellCheck(23, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(23).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(23, COL_CoreMeasure, "NQF 0024: Weight Assessment and Counseling for Children and Adolescents")

            C1QualityMeasures.SetData(24, COL_CoreMeasure, Space(3) & "Population 2: Age 2 years to 10 years with an encounter and not pregnant")
            C1QualityMeasures.SetData(25, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.SetData(26, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.SetData(27, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")

            C1QualityMeasures.SetData(28, COL_CoreMeasure, Space(3) & "Population 2: Age 2 years to 10 years with an encounter and not pregnant")
            C1QualityMeasures.SetData(29, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.SetData(30, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.SetData(31, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")

            ''''''''

            C1QualityMeasures.SetCellCheck(32, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(32).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(32, COL_CoreMeasure, "NQF 0038: Childhood immunization Status")
            C1QualityMeasures.SetData(33, COL_CoreMeasure, Space(3) & "Population 1: Patients who reach age 2 within reporting period.")
            C1QualityMeasures.SetData(34, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients who received 4 DTaP Vaccines.")
            C1QualityMeasures.SetData(35, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received 3 IPV Vaccines.")
            C1QualityMeasures.SetData(36, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received 1 MMR Vaccine.")
            C1QualityMeasures.SetData(37, COL_CoreMeasure, Space(5) & "Numerator 4:  Patients who received 2 HiB Vaccines.")
            C1QualityMeasures.SetData(38, COL_CoreMeasure, Space(5) & "Numerator 5:  Patients who received 3 Hep B Vaccines.")
            C1QualityMeasures.SetData(39, COL_CoreMeasure, Space(5) & "Numerator 6:  Patients who received 1 VZV Vaccine.")
            C1QualityMeasures.SetData(40, COL_CoreMeasure, Space(5) & "Numerator 7:  Patients who received 4 PCV Vaccines.")
            C1QualityMeasures.SetData(41, COL_CoreMeasure, Space(5) & "Numerator 8:  Patients who received 2 Hep A Vaccines.")
            C1QualityMeasures.SetData(42, COL_CoreMeasure, Space(5) & "Numerator 9:  Patients who received 2 RV Vaccines.")
            C1QualityMeasures.SetData(43, COL_CoreMeasure, Space(5) & "Numerator 10:  Patients who received 2 flu Vaccines.")
            C1QualityMeasures.SetData(44, COL_CoreMeasure, Space(5) & "Numerator 11:  Patients who received Vaccine set 1.")
            C1QualityMeasures.SetData(45, COL_CoreMeasure, Space(5) & "Numerator 12:  Patients who received Vaccine set 2.")

            C1QualityMeasures.SetCellCheck(46, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(46).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(46, COL_CoreMeasure, "NQF 0001: Asthma Assessment")
            C1QualityMeasures.SetData(47, COL_CoreMeasure, Space(3) & "Population 1: Age 5 years to 50 years with diagnosis of asthma")
            C1QualityMeasures.SetData(48, COL_CoreMeasure, Space(5) & "Numerator:  Daytime and nocturnal asthma symptoms assessed")

            C1QualityMeasures.SetCellCheck(49, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(49).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(49, COL_CoreMeasure, "NQF 0002: Appropriate Testing for Children with Pharyngitis")
            C1QualityMeasures.SetData(50, COL_CoreMeasure, Space(3) & "Population 1: Age 2 years to 18 years diagnosed with Pharyngitis and dispensed an antibiotic")
            C1QualityMeasures.SetData(51, COL_CoreMeasure, Space(5) & "Numerator:  Patients who received a group A streptococcus (strep) test for the episode.")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0002")
            'C1QualityMeasures.SetData(51, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(51, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(51, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(51, COL_Percent, dt.Rows(0)(3).ToString() & "%")

            C1QualityMeasures.SetCellCheck(52, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(52).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(52, COL_CoreMeasure, "NQF 0004: Initiation and Engagement of Alcohol and Other Drug Dependence Treatment: (a) Initiation, (b) Engagement")
            C1QualityMeasures.SetData(53, COL_CoreMeasure, Space(3) & "Population 1: Age 12 years to 16 years with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(54, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(55, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with AOD diagnosis within 30 days")
            C1QualityMeasures.SetData(56, COL_CoreMeasure, Space(3) & "Population 2: Age 17 years or older with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(57, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(58, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with an AOD Diagnosis within 30 days")

            C1QualityMeasures.SetData(59, COL_CoreMeasure, Space(3) & "Population 2: Age 17 years or older with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(60, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(61, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with an AOD Diagnosis within 30 days")
            C1QualityMeasures.SetData(59, COL_CoreMeasure, Space(3) & "Population 3: Age 12 years or older with a new episode of AOD dependence.")
            C1QualityMeasures.SetData(60, COL_CoreMeasure, Space(5) & "Numerator 1:  Initiate treatment within 14 days of the diagnosis")
            C1QualityMeasures.SetData(61, COL_CoreMeasure, Space(5) & "Numerator 2:  Two additional services with an AOD Diagnosis within 30 days")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF004_Pop1Num1")
            'C1QualityMeasures.SetData(54, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(54, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(54, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(54, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF004_Pop1Num2")
            'C1QualityMeasures.SetData(55, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(55, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(55, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(55, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF004_Pop2Num1")
            'C1QualityMeasures.SetData(57, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(57, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(57, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(57, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF004_Pop2Num2")
            'C1QualityMeasures.SetData(58, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(58, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(58, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(58, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF004_Pop3Num1")
            'C1QualityMeasures.SetData(60, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(60, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(60, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(60, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF004_Pop3Num2")
            'C1QualityMeasures.SetData(61, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(61, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(61, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(61, COL_Percent, dt.Rows(0)(3).ToString() & "%")

            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0012")
            C1QualityMeasures.SetCellCheck(65, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(65).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(65, COL_CoreMeasure, "NQF 0012: Prenatal Care:  Screening for Human Immunodeficiency Virus (HIV)")
            C1QualityMeasures.SetData(66, COL_CoreMeasure, Space(3) & "Population 1: Delivered Live Birth and had Prenatal Visit")
            C1QualityMeasures.SetData(67, COL_CoreMeasure, Space(5) & "Numerator: HIV Screening Performed")

            'C1QualityMeasures.SetData(67, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(67, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(67, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(67, COL_Percent, dt.Rows(0)(3).ToString() & "%")
            C1QualityMeasures.SetCellCheck(68, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(68).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(68, COL_CoreMeasure, "NQF 0014: Prenatal Care:  Anti-D Immune Globulin")
            C1QualityMeasures.SetData(69, COL_CoreMeasure, Space(3) & "Population 1:   D (Rh) negative, unsensitized patients who delivered live birth")
            C1QualityMeasures.SetData(70, COL_CoreMeasure, Space(5) & "Numerator: Received anti‐D immune globulin at 26‐30 weeks gestation")

            C1QualityMeasures.SetCellCheck(71, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(71).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(71, COL_CoreMeasure, "NQF 0018:Controlling High Blood Pressure")
            C1QualityMeasures.SetData(72, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years to 85 years with diagnosis of Hypertension")
            C1QualityMeasures.SetData(73, COL_CoreMeasure, Space(5) & "Numerator:  Blood Pressure controlled (< 140/90)")

            C1QualityMeasures.SetCellCheck(74, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(74).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(74, COL_CoreMeasure, "NQF 0027:Smoking and Tobacco Use Cessation, Medical assistance")
            C1QualityMeasures.SetData(75, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years or older")
            C1QualityMeasures.SetData(76, COL_CoreMeasure, Space(5) & "Numerator 1:  Tobacco user")
            C1QualityMeasures.SetData(77, COL_CoreMeasure, Space(5) & "Numerator 2:  Tobacco cessation counseling")

            C1QualityMeasures.SetCellCheck(78, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(78).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(78, COL_CoreMeasure, "NQF 0031:Breast Cancer Screening")
            C1QualityMeasures.SetData(79, COL_CoreMeasure, Space(3) & "Population 1:  Women 40–69 years of age")
            C1QualityMeasures.SetData(80, COL_CoreMeasure, Space(5) & "Numerator:  Breast cancer screen performed")


            '''''''''
            C1QualityMeasures.SetCellCheck(81, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(81).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(81, COL_CoreMeasure, "NQF 0033:Chlamydia Screening for Women")
            C1QualityMeasures.SetData(82, COL_CoreMeasure, Space(3) & "Population 1:  Women 15‐24 years of age identified as sexually active")
            C1QualityMeasures.SetData(83, COL_CoreMeasure, Space(5) & "Numerator:  Chlamydia screening performed")
            C1QualityMeasures.SetData(84, COL_CoreMeasure, Space(3) & "Population 2:  Women 15‐20 years of age identified as sexually active")
            C1QualityMeasures.SetData(85, COL_CoreMeasure, Space(5) & "Numerator:  Chlamydia screening performed")
            C1QualityMeasures.SetData(86, COL_CoreMeasure, Space(3) & "Population 3:  Women 21‐24 years of age identified as sexually active")
            C1QualityMeasures.SetData(87, COL_CoreMeasure, Space(5) & "Numerator:  Chlamydia screening performed")

            C1QualityMeasures.SetCellCheck(88, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(88).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(88, COL_CoreMeasure, "NQF 0034:Colorectal Cancer Screening")
            C1QualityMeasures.SetData(89, COL_CoreMeasure, Space(3) & "Population 1:  Age 50-75 years")
            C1QualityMeasures.SetData(90, COL_CoreMeasure, Space(5) & "Numerator:  Colorectal cancer screening performed")

            C1QualityMeasures.SetCellCheck(91, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(91).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(91, COL_CoreMeasure, "NQF 0036:Use of Appropriate Medications for Asthma")
            C1QualityMeasures.SetData(92, COL_CoreMeasure, Space(3) & "Population 1:  Age 5-11 years with persistent asthma")
            C1QualityMeasures.SetData(93, COL_CoreMeasure, Space(5) & "Numerator:  Asthma medication prescribed")
            C1QualityMeasures.SetData(94, COL_CoreMeasure, Space(3) & "Population 2:  Age 12-50 years with persistent asthma")
            C1QualityMeasures.SetData(95, COL_CoreMeasure, Space(5) & "Numerator:  Asthma medication prescribed")
            C1QualityMeasures.SetData(96, COL_CoreMeasure, Space(3) & "Population 3:  Age 5-50 years with persistent asthma")
            C1QualityMeasures.SetData(97, COL_CoreMeasure, Space(5) & "Numerator:  Asthma medication prescribed")

            C1QualityMeasures.SetCellCheck(98, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(98).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(98, COL_CoreMeasure, "NQF 0043:Pneumonia Vaccination Status for Older Adults")
            C1QualityMeasures.SetData(99, COL_CoreMeasure, Space(3) & "Population 1:  Age 65 years or older")
            C1QualityMeasures.SetData(100, COL_CoreMeasure, Space(5) & "Numerator:  Pneumococcal vaccination administered")

            C1QualityMeasures.SetCellCheck(101, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(101).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(101, COL_CoreMeasure, "NQF 0047:Asthma Pharmacologic Therapy")
            C1QualityMeasures.SetData(102, COL_CoreMeasure, Space(3) & "Population 1:  Age 5-40 years with mild, moderate, or persistent asthma")
            C1QualityMeasures.SetData(103, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed control medication or an acceptable alternative")

            C1QualityMeasures.SetCellCheck(104, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(104).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(104, COL_CoreMeasure, "NQF 0052:Low Back Pain: Use of Imaging Studies")
            C1QualityMeasures.SetData(105, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-50 years with low back pain diagnosis")
            C1QualityMeasures.SetData(106, COL_CoreMeasure, Space(5) & "Numerator:  Did not have an imaging study within 28 days ")

            C1QualityMeasures.SetCellCheck(107, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(107).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(107, COL_CoreMeasure, "NQF 0055:Diabetes: Eye Exam")
            C1QualityMeasures.SetData(108, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(109, COL_CoreMeasure, Space(5) & "Numerator:  Had a retinal or dilated eye exam")

            C1QualityMeasures.SetCellCheck(110, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(110).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(110, COL_CoreMeasure, "NQF 0056:Diabetes: Foot Exam")
            C1QualityMeasures.SetData(111, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(112, COL_CoreMeasure, Space(5) & "Numerator:  Had a foot exam")

            C1QualityMeasures.SetCellCheck(113, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(113).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(113, COL_CoreMeasure, "NQF 0059:Diabetes:  HbA1c Poor Control")
            C1QualityMeasures.SetData(114, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(115, COL_CoreMeasure, Space(5) & "Numerator:  Had HbA1c >9.0%.")

            C1QualityMeasures.SetCellCheck(116, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(116).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(116, COL_CoreMeasure, "NQF 0061:Diabetes: Blood Pressure Management")
            C1QualityMeasures.SetData(117, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(118, COL_CoreMeasure, Space(5) & "Numerator:  Had Blood Pressure <140/90 mmHg.")

            C1QualityMeasures.SetCellCheck(119, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(119).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(119, COL_CoreMeasure, "NQF 0062:Diabetes: Urine Screening")
            C1QualityMeasures.SetData(120, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(121, COL_CoreMeasure, Space(5) & "Numerator:  Had a nephropathy screening test or evidence of nephropathy")

            C1QualityMeasures.SetCellCheck(119, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(119).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(119, COL_CoreMeasure, "NQF 0064:Diabetes: LDL Management & Control")
            C1QualityMeasures.SetData(120, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(121, COL_CoreMeasure, Space(5) & "Numerator 1:  Had LDL‐C lab test")
            C1QualityMeasures.SetData(122, COL_CoreMeasure, Space(5) & "Numerator 2:  Had LDL‐C <100mg/dL")

            C1QualityMeasures.SetCellCheck(123, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(123).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(123, COL_CoreMeasure, "NQF 0067:Coronary Artery Disease (CAD): Oral Antiplatelet Therapy Prescribed for Patients with CAD")
            C1QualityMeasures.SetData(124, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of CAD")
            C1QualityMeasures.SetData(125, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed oral antiplatelet therapy")

            C1QualityMeasures.SetCellCheck(126, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(126).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(126, COL_CoreMeasure, "NQF 0070:Coronary Artery Disease (CAD): Beta-Blocker Therapy for CAD Patients with Prior Myocardial Infarction (MI)")
            C1QualityMeasures.SetData(127, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with diagnosis of CAD and prior MI")
            C1QualityMeasures.SetData(128, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed beta‐blocker therapy")

            C1QualityMeasures.SetCellCheck(129, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(129).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(129, COL_CoreMeasure, "NQF 0073:Ischemic Vascular Disease (IVD): Blood Pressure Management")
            C1QualityMeasures.SetData(130, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with AMI, CABG, or PTCA in prev year or IVD in current year")
            C1QualityMeasures.SetData(131, COL_CoreMeasure, Space(5) & "Numerator:  Blood Pressure controlled (< 140/90)")

            C1QualityMeasures.SetCellCheck(132, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(132).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(132, COL_CoreMeasure, "NQF 0074:Coronary Artery Disease (CAD): Drug Therapy for Lowering LDL-Cholesterol")
            C1QualityMeasures.SetData(133, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with diagnosis of CAD")
            C1QualityMeasures.SetData(134, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed a lipid‐lowering therapy")

            C1QualityMeasures.SetCellCheck(132, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(132).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(132, COL_CoreMeasure, "NQF 0075:Ischemic Vascular Disease (IVD): Complete Lipid Panel and LDL Control")
            C1QualityMeasures.SetData(133, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with AMI, CABG, or PTCA in prev year or IVD in current year")
            C1QualityMeasures.SetData(134, COL_CoreMeasure, Space(5) & "Numerator 1:  Had LDL‐C lab test")
            C1QualityMeasures.SetData(135, COL_CoreMeasure, Space(5) & "Numerator 2:  Had LDL‐C <100mg/dL")

            C1QualityMeasures.SetCellCheck(136, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(136).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(136, COL_CoreMeasure, "NQF 0081:Heart Failure (HF) : Angiotensin-Converting Enzyme (ACE) Inhibitor or Angiotensin Receptor Blocker (ARB) Therapy for Left Ventricular Systolic Dysfunction ")
            C1QualityMeasures.SetData(137, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of heart failure and LVSD (LVEF < 40%)")
            C1QualityMeasures.SetData(138, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed ACE inhibitor or ARB therapy")

            C1QualityMeasures.SetCellCheck(139, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(139).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(139, COL_CoreMeasure, "NQF 0083:Heart Failure (HF): Beta-Blocker Therapy for Left Ventricular Systolic Dysfunction (LVSD)")
            C1QualityMeasures.SetData(140, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of heart failure and LVSD (LVEF < 40%)")
            C1QualityMeasures.SetData(141, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed beta‐blocker therapy")

            C1QualityMeasures.SetCellCheck(142, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(142).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(142, COL_CoreMeasure, "NQF 0084:Heart Failure (HF) : Warfarin Therapy Patients with Atrial Fibrillation")
            C1QualityMeasures.SetData(143, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of heart failure and paroxysmal or chronic atrial fibrillation")
            C1QualityMeasures.SetData(144, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed warfarin therapy")

            C1QualityMeasures.SetCellCheck(145, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(145).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(145, COL_CoreMeasure, "NQF 0086:Primary Open Angle Glaucoma (POAG): Optic Nerve Evaluation")
            C1QualityMeasures.SetData(146, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of POAG")
            C1QualityMeasures.SetData(147, COL_CoreMeasure, Space(5) & "Numerator:  Had an optic nerve head evaluation")

            C1QualityMeasures.SetCellCheck(148, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(148).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(148, COL_CoreMeasure, "NQF 0088:Diabetic Retinopathy: Documentation of Presence or Absence of Macular Edema and Level of Severity of Retinopathy")
            C1QualityMeasures.SetData(149, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of diabetic retinopathy")
            C1QualityMeasures.SetData(150, COL_CoreMeasure, Space(5) & "Numerator:  Doc of macular edema and level of severity of retinopathy")

            C1QualityMeasures.SetCellCheck(151, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(151).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(151, COL_CoreMeasure, "NQF 0089:Diabetic Retinopathy: Communication with the Physician Managing Ongoing Diabetes Care")
            C1QualityMeasures.SetData(152, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with a diagnosis of diabetic retinopathy and a macular or fundus exam")
            C1QualityMeasures.SetData(153, COL_CoreMeasure, Space(5) & "Numerator:  Documented communication to the PCP")

            C1QualityMeasures.SetCellCheck(154, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(154).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(154, COL_CoreMeasure, "NQF 0105:Anti‐depressant medication management: (a) Effective Acute Phase Treatment, (b)Effective Continuation Phase Treatment")
            C1QualityMeasures.SetData(155, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older, diagnosed with a new episode of major depression, and treated with antidepressant medication")
            C1QualityMeasures.SetData(156, COL_CoreMeasure, Space(5) & "Numerator 1:  Medication dispensed 84 days or more after diagnosis")
            C1QualityMeasures.SetData(157, COL_CoreMeasure, Space(5) & "Numerator 2:  Medication dispensed 180 days or more after diagnosis")

            C1QualityMeasures.SetCellCheck(158, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(158).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(158, COL_CoreMeasure, "NQF 0385:Oncology Colon Cancer:  Chemotherapy for Stage III Colon Cancer Patients")
            C1QualityMeasures.SetData(159, COL_CoreMeasure, Space(3) & "Population 1:  Age 18 years and older with Stage IIIA through IIIC colon cancer")
            C1QualityMeasures.SetData(160, COL_CoreMeasure, Space(5) & "Numerator:  Adjuvant chemotherapy")

            C1QualityMeasures.SetCellCheck(161, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(161).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(161, COL_CoreMeasure, "NQF 0387:Oncology Breast Cancer:  Hormonal Therapy for Stage IC-IIIC Estrogen Receptor/Progesterone Receptor (ER/PR) Positive Breast Cancer")
            C1QualityMeasures.SetData(162, COL_CoreMeasure, Space(3) & "Population 1:  Female, age 18 years and older with Stage IC through IIIC, ER or PR positive breast cancer")
            C1QualityMeasures.SetData(163, COL_CoreMeasure, Space(5) & "Numerator:  Prescribed tamoxifen or aromatase inhibitor")

            C1QualityMeasures.SetCellCheck(164, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(164).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(164, COL_CoreMeasure, "NQF 0389:Prostate Cancer:  Avoidance of Overuse of Bone Scan for Staging Low Risk Prostate Cancer Patients")
            C1QualityMeasures.SetData(165, COL_CoreMeasure, Space(3) & "Population 1:  Diagnosed and treated prostate cancer with low risk of recurrence")
            C1QualityMeasures.SetData(166, COL_CoreMeasure, Space(5) & "Numerator:  Bone scan not performed")

            C1QualityMeasures.SetCellCheck(167, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(167).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(167, COL_CoreMeasure, "NQF 0575:Diabetes: HbA1c Control (<8%)")
            C1QualityMeasures.SetData(168, COL_CoreMeasure, Space(3) & "Population 1:  Age 18-75 years with diabetes (type 1 or type 2)")
            C1QualityMeasures.SetData(169, COL_CoreMeasure, Space(5) & "Numerator:  Had HbA1c <8.0%")



            C1QualityMeasures.Rows.Insert(32)
            C1QualityMeasures.Rows(32).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetData(32, COL_CoreMeasure, Space(3) & "Population 3: Age 11 years to 16 years with an encounter and not pregnant")
            C1QualityMeasures.Rows.Insert(33)
            C1QualityMeasures.Rows(33).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetData(33, COL_CoreMeasure, Space(5) & "Numerator 1:  Patients with a BMI calculation.")
            C1QualityMeasures.Rows.Insert(34)
            C1QualityMeasures.Rows(34).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetData(34, COL_CoreMeasure, Space(5) & "Numerator 2:  Patients who received nutrition counseling.")
            C1QualityMeasures.Rows.Insert(35)
            C1QualityMeasures.Rows(35).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetData(35, COL_CoreMeasure, Space(5) & "Numerator 3:  Patients who received counseling for physical activity.")

        

            C1QualityMeasures.Rows.Insert(85)
            C1QualityMeasures.SetCellCheck(85, COL_Check, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            C1QualityMeasures.Rows(85).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
            C1QualityMeasures.SetData(85, COL_CoreMeasure, "NQF 0032:Cervical Cancer Screening")
            C1QualityMeasures.Rows.Insert(86)
            C1QualityMeasures.Rows(86).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetData(86, COL_CoreMeasure, Space(3) & "Population 1:  Women 21-64 years of age")
            C1QualityMeasures.Rows.Insert(87)
            C1QualityMeasures.Rows(87).StyleNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
            C1QualityMeasures.SetData(87, COL_CoreMeasure, Space(5) & "Numerator:  Pap test performed")

            C1QualityMeasures.Cols(COL_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            C1QualityMeasures.Rows.Remove(66)
            C1QualityMeasures.Rows.Remove(66)
            C1QualityMeasures.Rows.Remove(66)


            C1QualityMeasures.Rows.Remove(28)
            C1QualityMeasures.Rows.Remove(28)
            C1QualityMeasures.Rows.Remove(28)
            C1QualityMeasures.Rows.Remove(28)
            C1QualityMeasures.Rows.Remove(23)

            ''Added by Mayuri:20100901-To add button in cell
            Dim rg As C1.Win.C1FlexGrid.CellRange
            ''NQF0028a
            rg = C1QualityMeasures.GetCellRange(11, COL_Button, 11, COL_Button)

            rg.StyleNew.ComboList = "..."
            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            Dim rg1 As C1.Win.C1FlexGrid.CellRange
            ''NQF0028a
            rg1 = C1QualityMeasures.GetCellRange(14, COL_Button, 14, COL_Button)
            rg1.StyleNew.ComboList = "..."
            C1QualityMeasures.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            GenerateReport()
            _IsformLoading = False

        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub tlbbtn_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_SelectAll.Click
        ''Dim j As Integer = 0
        For j = 0 To C1QualityMeasures.Rows.Count - 1
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            checkval = C1QualityMeasures.GetCellCheck(j, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                C1QualityMeasures.SetCellCheck(j, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
            End If
        Next
    End Sub

    Private Sub tlbbtn_DeelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_DeelectAll.Click

        For j = 0 To C1QualityMeasures.Rows.Count - 1
            Dim checkval As C1.Win.C1FlexGrid.CheckEnum
            checkval = C1QualityMeasures.GetCellCheck(j, 0)
            If checkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                C1QualityMeasures.SetCellCheck(j, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            End If
        Next
    End Sub

    Private Sub tlbbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        Me.Close()
    End Sub
    Private Function CalculatePercent(ByVal SPName As String, Optional ByVal HistoryItemList As String = "") As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Try



            'Dim Percent As Single
            'Dim cmd As New SqlCommand("" + SPName + "", Con)
            'cmd.CommandType = CommandType.StoredProcedure
            'Dim objParam As SqlParameter

            'Dim da As New SqlDataAdapter
            'Dim dt As New DataTable
            'da.SelectCommand = cmd
            'da.Fill(dt)
            'If dt.Rows.Count > 0 Then
            '    dt.Columns.Add()
            '    If dt.Rows(0)(0).ToString() = "0" Then
            '        dt.Rows(0)(3) = "N/A"
            '    Else
            '        Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
            '        dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

            '    End If

            'End If
            'Return dt

            Dim oParameter As gloDatabaseLayer.DBParameter

            oDB.Connect(False)
            Dim dt As DataTable = Nothing



            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            'Dim cmd As New SqlCommand("" + SPName + "", Con)
            'cmd.CommandType = CommandType.StoredProcedure
            'Dim objParam As SqlParameter

            'Dim da As New SqlDataAdapter
            'Dim dt As New DataTable
            'da.SelectCommand = cmd
            'da.Fill(dt)
            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                If dt.Rows(0)(0).ToString() = "0" Then
                    dt.Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If

            End If
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            oParameters.Clear()
            oParameters.Dispose()
            oParameters = Nothing
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    Private Sub FillProviders()
        Try
            Dim dtProvider As DataTable
            Dim objPatient As New clsPatient
            'Dim dr As DataRow
            '  Dim i As Int16
            dtProvider = objPatient.GetProviders(_ClinicID)
            If Not IsNothing(dtProvider) Then
                If dtProvider.Rows.Count > 0 Then
                    Dim dr As DataRow
                    dr = dtProvider.NewRow
                    dr("nProviderID") = 0
                    dr("ProviderName") = "All"
                    dtProvider.Rows.InsertAt(dr, 0)
                    dtProvider.AcceptChanges()
                    cmbProviders.DataSource = dtProvider.Copy()


                    cmbProviders.ValueMember = dtProvider.Columns("nProviderID").ColumnName
                    cmbProviders.DisplayMember = dtProvider.Columns("ProviderName").ColumnName
                    cmbProviders.Refresh()
                    cmbProviders.SelectedIndex = 0
                End If
                dtProvider.Dispose()
                dtProvider = Nothing

            End If
            objPatient = Nothing

        Catch ex As Exception

        End Try


    End Sub
    Private Function RetriveDataWithHistoryItems(ByVal SPName As String, Optional ByVal HistoryItemList As String = "")
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try
            Dim oParameter As gloDatabaseLayer.DBParameter
            oDB.Connect(False)
            Dim dt As DataTable = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@HistoryItemList"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = _HistoryItemList
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicStartDate.Value
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = dtpicEndDate.Value
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = cmbProviders.SelectedValue.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, dt)
            ''
            oDB.Disconnect()
            Dim Percent As Single
            'Dim cmd As New SqlCommand("" + SPName + "", Con)
            'cmd.CommandType = CommandType.StoredProcedure
            'Dim objParam As SqlParameter

            'Dim da As New SqlDataAdapter
            'Dim dt As New DataTable
            'da.SelectCommand = cmd
            'da.Fill(dt)
            If dt.Rows.Count > 0 Then
                dt.Columns.Add()
                If dt.Rows(0)(0).ToString() = "0" Then
                    dt.Rows(0)(3) = "N/A"
                Else
                    Percent = Single.Parse(dt.Rows(0)(1).ToString()) / Single.Parse(dt.Rows(0)(0).ToString()) * 100
                    dt.Rows(0)(3) = FormatNumber(Percent, 2, TriState.True)

                End If

            End If
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            oParameters.Clear()
            oParameters.Dispose()
            oParameters = Nothing
            oDB.Dispose()
            oDB = Nothing

        End Try
    End Function
    ''Added by Mayuri:20100901-To retrieve tobacco related history items
#Region "Cell Button Click Event"
    Dim ofrmList As frmViewListControl = Nothing
    Private Sub C1QualityMeasures_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1QualityMeasures.CellButtonClick

        If C1QualityMeasures.ColSel = COL_Button Then
            If C1QualityMeasures.Row = 14 Then
                Try
                    If IsNothing(ofrmList) = False Then
                        ofrmList.Dispose()
                        ofrmList = Nothing
                    End If
                    ofrmList = New frmViewListControl
                    ofrmList.Text = "History Items"
                    If IsNothing(oListhistoryItem) = False Then
                       
                        oListhistoryItem.Dispose()
                        oListhistoryItem = Nothing
                    End If
                    oListhistoryItem = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.HistoryItem, True, ofrmList.Width)
                    oListhistoryItem.ControlHeader = "History Items"


                    AddHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                    AddHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                    ''To get previously selected items
                    If IsNothing(NQF0028bHistoryItemList) = False Then
                        For i As Integer = 0 To NQF0028bHistoryItemList.Count - 1
                            oListhistoryItem.SelectedItems.Add(NQF0028bHistoryItemList(i))
                        Next
                    End If


                    ofrmList.Controls.Add(oListhistoryItem)
                    oListhistoryItem.Dock = DockStyle.Fill
                    oListhistoryItem.BringToFront()
                    oListhistoryItem.OpenControl()
                    oListhistoryItem.ShowHeaderPanel(False)
                    ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))
                    If IsNothing(oListhistoryItem) = False Then
                        ofrmList.Controls.Remove(oListhistoryItem)
                        RemoveHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                        RemoveHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                        oListhistoryItem.Dispose()
                        oListhistoryItem = Nothing
                    End If

                    If IsNothing(ofrmList) = False Then
                        ofrmList.Dispose()
                        ofrmList = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            ElseIf C1QualityMeasures.Row = 11 Then
                Try
                    If IsNothing(ofrmList) = False Then
                        ofrmList.Dispose()
                        ofrmList = Nothing
                    End If
                    ofrmList = New frmViewListControl
                    ofrmList.Text = "History Items"
                    If IsNothing(oListhistoryItem) = False Then

                        oListhistoryItem.Dispose()
                        oListhistoryItem = Nothing
                    End If

                    oListhistoryItem = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.HistoryItem, True, ofrmList.Width)
                    oListhistoryItem.ControlHeader = "History Items"


                    AddHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                    AddHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                    ''To get previously selected items
                    If IsNothing(HistoryItemList) = False Then
                        For i As Integer = 0 To HistoryItemList.Count - 1
                            oListhistoryItem.SelectedItems.Add(HistoryItemList(i))
                        Next
                    End If


                    ofrmList.Controls.Add(oListhistoryItem)
                    oListhistoryItem.Dock = DockStyle.Fill
                    oListhistoryItem.BringToFront()
                    oListhistoryItem.OpenControl()
                    oListhistoryItem.ShowHeaderPanel(False)
                    ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))

                    If IsNothing(oListhistoryItem) = False Then
                        ofrmList.Controls.Remove(oListhistoryItem)
                        RemoveHandler oListhistoryItem.ItemSelectedClick, AddressOf oListhistoryItem_ItemSelectedClick
                        RemoveHandler oListhistoryItem.ItemClosedClick, AddressOf oListhistoryItem_ItemClosedClick
                        oListhistoryItem.Dispose()
                        oListhistoryItem = Nothing
                    End If

                    If IsNothing(ofrmList) = False Then
                        ofrmList.Dispose()
                        ofrmList = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
            'Dim frmHistory As New frmHistory(gnVisitID, Date.Now.ToShortDateString(), False)
            '' Dim frmHistory As New frmHistory()
            ''frmHistory.MdiParent = Me
            'frmHistory.Show()

        End If
    End Sub

#End Region
    Private Sub oListhistoryItem_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Dim dt As New DataTable
            If C1QualityMeasures.Row = 11 Then

                If (IsNothing(HistoryItemList) = False) Then
                    HistoryItemList.Dispose()
                    HistoryItemList = Nothing
                End If


                HistoryItemList = New gloGeneralItem.gloItems()
                Dim HistoryItem As gloGeneralItem.gloItem
                If oListhistoryItem.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListhistoryItem.SelectedItems.Count - 1

                        HistoryItem = New gloGeneralItem.gloItem()
                        HistoryItem.ID = oListhistoryItem.SelectedItems(i).ID
                        HistoryItem.Description = oListhistoryItem.SelectedItems(i).Description
                        HistoryItemList.Add(HistoryItem)
                        HistoryItem.Dispose()
                        ' HistoryItem = Nothing

                    Next
                End If
                '_HistoryItemList = ""
                '_HistoryItemList = GetHistoryItems()
                'dt = RetriveDataWithHistoryItems("MU_NQF0028a", _HistoryItemList)
                'C1QualityMeasures.SetData(11, COL_Denomenator, dt.Rows(0)(0).ToString())
                'C1QualityMeasures.SetData(11, COL_Numeratot, dt.Rows(0)(1).ToString())
                'C1QualityMeasures.SetData(11, COL_Exclusion, dt.Rows(0)(2).ToString())
                'If (dt.Rows(0)(3).ToString() = "N/A") Then
                '    C1QualityMeasures.SetData(11, COL_Percent, dt.Rows(0)(3).ToString())
                'Else
                '    C1QualityMeasures.SetData(11, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                'End If
            ElseIf C1QualityMeasures.Row = 14 Then
                If (IsNothing(NQF0028bHistoryItemList) = False) Then
                    NQF0028bHistoryItemList.Dispose()
                    NQF0028bHistoryItemList = Nothing
                End If
                NQF0028bHistoryItemList = New gloGeneralItem.gloItems()
                Dim NQF0028bHistoryItem As gloGeneralItem.gloItem
                If oListhistoryItem.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListhistoryItem.SelectedItems.Count - 1

                        NQF0028bHistoryItem = New gloGeneralItem.gloItem()
                        NQF0028bHistoryItem.ID = oListhistoryItem.SelectedItems(i).ID
                        NQF0028bHistoryItem.Description = oListhistoryItem.SelectedItems(i).Description
                        NQF0028bHistoryItemList.Add(NQF0028bHistoryItem)
                        NQF0028bHistoryItem.Dispose()
                        ' HistoryItem = Nothing

                    Next
                End If
                '_NQF0028bHistoryItemList = ""
                '_NQF0028bHistoryItemList = GetNQF0028bHistoryItems()
                'dt = RetriveDataWithHistoryItems("MU_NQF0028b", _NQF0028bHistoryItemList)
                'C1QualityMeasures.SetData(14, COL_Denomenator, dt.Rows(0)(0).ToString())
                'C1QualityMeasures.SetData(14, COL_Numeratot, dt.Rows(0)(1).ToString())
                'C1QualityMeasures.SetData(14, COL_Exclusion, dt.Rows(0)(2).ToString())
                'If (dt.Rows(0)(3).ToString() = "N/A") Then
                '    C1QualityMeasures.SetData(14, COL_Percent, dt.Rows(0)(3).ToString())
                'Else
                '    C1QualityMeasures.SetData(14, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                'End If
            End If

            If IsNothing(ofrmList) = False Then
                ofrmList.Close()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Added by Mayuri:20100901-To retrieve tobacco related history items and close form 
    Private Sub oListhistoryItem_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)

        If IsNothing(ofrmList) = False Then
            ofrmList.Close()
        End If

    End Sub
    

    
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID").ToString() <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
                cmbProviders.SelectedValue = _ClinicID
            End If
        End If
    End Sub

    Private Sub C1QualityMeasures_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1QualityMeasures.EnterCell
        C1QualityMeasures.Row = C1QualityMeasures.Row
    End Sub

    Private Sub tlbbtn_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            GenerateReport()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GenerateReport()
        Try
            Dim dt As DataTable = Nothing
            dt = CalculatePercent("MU_NQF0421")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(3, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(3, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(3, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(3, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If
           


            dt = CalculatePercent("MU_NQF0421_POP2")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(5, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(5, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(5, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(5, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If

            dt = CalculatePercent("MU_NQF0013")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(8, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(8, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(8, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(8, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If
            'dt = Nothing
            'dt = CalculatePercent("MU_NQF0002")
            'C1QualityMeasures.SetData(50, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(50, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(50, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(50, COL_Percent, dt.Rows(0)(3).ToString() & "%")


            dt = CalculatePercent("MU_NQF004_Pop1Num1")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(53, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(53, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(53, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(53, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If
            'C1QualityMeasures.SetData(55, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(55, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(55, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(55, COL_Percent, dt.Rows(0)(3).ToString() & "%")

            dt = CalculatePercent("MU_NQF004_Pop1Num2")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(54, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(54, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(54, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(54, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If
            'C1QualityMeasures.SetData(56, COL_Denomenator, dt.Rows(0)(0).ToString())
            'C1QualityMeasures.SetData(56, COL_Numeratot, dt.Rows(0)(1).ToString())
            'C1QualityMeasures.SetData(56, COL_Exclusion, dt.Rows(0)(2).ToString())
            'C1QualityMeasures.SetData(56, COL_Percent, dt.Rows(0)(3).ToString() & "%")

            dt = CalculatePercent("MU_NQF004_Pop2Num1")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(56, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(56, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(56, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(56, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If

            dt = CalculatePercent("MU_NQF004_Pop2Num2")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(57, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(57, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(57, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(57, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If

            dt = CalculatePercent("MU_NQF004_Pop3Num1")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(59, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(59, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(59, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(59, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If

            dt = CalculatePercent("MU_NQF004_Pop3Num2")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(60, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(60, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(60, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(60, COL_Percent, dt.Rows(0)(3).ToString() & "%")

                dt.Dispose()
                dt = Nothing
            End If

            dt = CalculatePercent("MU_NQF0012")
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(63, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(63, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(63, COL_Exclusion, dt.Rows(0)(2).ToString())
                C1QualityMeasures.SetData(63, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                dt.Dispose()
                dt = Nothing
            End If
            _HistoryItemList = ""
            _HistoryItemList = GetHistoryItems()
            dt = RetriveDataWithHistoryItems("MU_NQF0028a", _HistoryItemList)
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(11, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(11, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(11, COL_Exclusion, dt.Rows(0)(2).ToString())
                If (dt.Rows(0)(3).ToString() = "N/A") Then
                    C1QualityMeasures.SetData(11, COL_Percent, dt.Rows(0)(3).ToString())
                Else
                    C1QualityMeasures.SetData(11, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                End If
                dt.Dispose()
                dt = Nothing
            End If
            _NQF0028bHistoryItemList = ""
            _NQF0028bHistoryItemList = GetNQF0028bHistoryItems()

            dt = RetriveDataWithHistoryItems("MU_NQF0028b", _NQF0028bHistoryItemList)
            If (IsNothing(dt) = False) Then
                C1QualityMeasures.SetData(14, COL_Denomenator, dt.Rows(0)(0).ToString())
                C1QualityMeasures.SetData(14, COL_Numeratot, dt.Rows(0)(1).ToString())
                C1QualityMeasures.SetData(14, COL_Exclusion, dt.Rows(0)(2).ToString())
                If (dt.Rows(0)(3).ToString() = "N/A") Then
                    C1QualityMeasures.SetData(14, COL_Percent, dt.Rows(0)(3).ToString())
                Else
                    C1QualityMeasures.SetData(14, COL_Percent, dt.Rows(0)(3).ToString() & "%")
                End If
                dt.Dispose()
                dt = Nothing
            End If
            'If (_IsformLoading = False) Then
            '    MessageBox.Show("Report generated successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If

        Catch ex As Exception

        End Try
    End Sub

  
    Private Sub tlsbtnShowReportList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnShowReportList.Click
        Try
            GenerateReport()
        Catch ex As Exception

        End Try
    End Sub
End Class