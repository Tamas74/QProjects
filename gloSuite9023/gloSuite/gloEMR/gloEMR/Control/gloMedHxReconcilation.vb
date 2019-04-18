Imports gloMeds.Core
Imports System.Data.SqlClient

Public Class gloMedHxReconcilation

#Region "Variable Declaration"
    Private _dataSource As List(Of MedHx.MedHxItemNew)
    Private dtSelectedList As DataTable = Nothing
    Private _nPatientID As Int64 = 0
#End Region

#Region "Property"

#End Region
#Region "Column Constants"

    '------------Medication Coloumns----------
    Private Const COL_Med_Select As Integer = 0
    Private Const COL_Med_Dup As Integer = 1
    Private Const COL_Med_Source As Integer = 2
    Private Const Col_Med_Drug As Integer = 3
    Private Const Col_Med_sNDCCode As Integer = 4
    Private Const Col_Med_Amount As Integer = 5
    Private Const Col_Med_DrugForm As Integer = 6
    Private Const Col_Med_Frequency As Integer = 7

    Private Const Col_Med_DaysSupply As Integer = 8
    Private Const Col_Med_MedicationDate As Integer = 9
    Private Const Col_Med_StartDate As Integer = 10
    Private Const Col_Med_Refills As Integer = 11
    Private Const Col_Med_Status As Integer = 12
    Private Const Col_Med_AllowSub As Integer = 13


    Private Const Col_Med_Rx_sName As Integer = 14
    Private Const Col_Med_Rx_sNCPDPID As Integer = 15
    Private Const Col_Med_Rx_NPI As Integer = 16
    Private Const Col_Med_Rx_sAddressline1 As Integer = 17
    Private Const Col_Med_Rx_sAddressline2 As Integer = 18
    Private Const Col_Med_Rx_sCity As Integer = 19
    Private Const Col_Med_Rx_sState As Integer = 20
    Private Const Col_Med_Rx_sZip As Integer = 21
    Private Const Col_Med_Rx_sPhone As Integer = 22
    Private Const Col_Med_Rx_sFax As Integer = 23
    Private Const Col_Med_Rx_sEmail As Integer = 24
    Private Const PrescriberNPI As Integer = 25
    Private Const Col_Med_Direction As Integer = 26

    Private Const Col_Med_EndDate As Integer = 27

    '------------------------------------ --------------

#End Region

#Region "C1 Styles"

    Dim cStyle_Blue As C1.Win.C1FlexGrid.CellStyle
    Dim cStyle_Pink As C1.Win.C1FlexGrid.CellStyle
    Dim cStyle_StrikoutFont As C1.Win.C1FlexGrid.CellStyle
    Dim cStyle_NormalFont As C1.Win.C1FlexGrid.CellStyle

#End Region



#Region "Constuctor and Form Load"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal DataSource As List(Of MedHx.MedHxItemNew), ByVal PatientID As Int64)

        InitializeComponent()
        _dataSource = DataSource
        _nPatientID = PatientID
    End Sub


    Private Sub frmReconcileList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataTableFromMedHXItem("All")
        FillConsolidatedList()
        RemoveExactDuplicates()
        HighLightSimilarDuplicates()
        ' GetActiveMedication()
        'HighLightExactDuplicates()
    End Sub
#End Region

    Private Sub FillConsolidatedList()
        '

        'C1ConsolidatedList.Clear()
        C1ConsolidatedList.DataSource = Nothing
        C1ConsolidatedList.Rows.Count = 1
        Try
            If Not IsNothing(_dataSource) Then
                C1ConsolidatedList.DataSource = dtSelectedList   'dtSelectedList.DefaultView.ToTable(True)
            End If
            DesignConsolidatedList()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try

    End Sub
    Private Sub DesignConsolidatedList()
        Try

            'C1ConsolidatedList.AllowDragging = False
            C1ConsolidatedList.Cols.Count = 27
            C1ConsolidatedList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn


            C1ConsolidatedList.Cols(COL_Med_Select).AllowEditing = True
            C1ConsolidatedList.Cols(COL_Med_Select).Caption = "Select"
            C1ConsolidatedList.Cols(COL_Med_Select).DataType = GetType(Boolean)
            C1ConsolidatedList.Cols(COL_Med_Select).AllowSorting = True


            C1ConsolidatedList.Cols(COL_Med_Dup).Caption = "Dup"
            C1ConsolidatedList.Cols(COL_Med_Dup).AllowEditing = False
            C1ConsolidatedList.Cols(COL_Med_Dup).AllowSorting = True

            C1ConsolidatedList.Cols(COL_Med_Source).Caption = "Source"
            C1ConsolidatedList.Cols(COL_Med_Source).AllowEditing = False
            C1ConsolidatedList.Cols(COL_Med_Source).AllowSorting = True


            C1ConsolidatedList.Cols(Col_Med_Drug).Caption = "Drug"
            C1ConsolidatedList.Cols(Col_Med_Drug).AllowEditing = False
            C1ConsolidatedList.Cols(Col_Med_Drug).AllowSorting = True


            C1ConsolidatedList.Cols(Col_Med_sNDCCode).Caption = "NDCCode"
            C1ConsolidatedList.Cols(Col_Med_sNDCCode).AllowEditing = False


            C1ConsolidatedList.Cols(Col_Med_Amount).Caption = "Quantity"
            C1ConsolidatedList.Cols(Col_Med_Amount).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_DrugForm).Caption = "Drug Form"
            C1ConsolidatedList.Cols(Col_Med_DrugForm).AllowEditing = False


            C1ConsolidatedList.Cols(Col_Med_Frequency).Caption = "Frequency"
            C1ConsolidatedList.Cols(Col_Med_Frequency).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_DaysSupply).Caption = "DaysSupply"
            C1ConsolidatedList.Cols(Col_Med_DaysSupply).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_MedicationDate).Caption = "LastUpdated"
            C1ConsolidatedList.Cols(Col_Med_MedicationDate).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_StartDate).Caption = "Start Date"
            C1ConsolidatedList.Cols(Col_Med_StartDate).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_EndDate).Caption = "End Date"
            C1ConsolidatedList.Cols(Col_Med_EndDate).AllowEditing = False


            C1ConsolidatedList.Cols(Col_Med_Refills).Caption = "Refills"
            C1ConsolidatedList.Cols(Col_Med_Refills).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Status).Caption = "Status"
            C1ConsolidatedList.Cols(Col_Med_Status).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_AllowSub).Caption = "AllowSub"
            C1ConsolidatedList.Cols(Col_Med_AllowSub).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sName).Caption = "PharmacyName"
            C1ConsolidatedList.Cols(Col_Med_Rx_sName).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sNCPDPID).Caption = "PharmacyNCPDPID"
            C1ConsolidatedList.Cols(Col_Med_Rx_sNCPDPID).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_NPI).Caption = "NPI"
            C1ConsolidatedList.Cols(Col_Med_Rx_NPI).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sAddressline1).Caption = "PharmacyAddressline1"
            C1ConsolidatedList.Cols(Col_Med_Rx_sAddressline1).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sAddressline2).Caption = "PharmacyAddressline2"
            C1ConsolidatedList.Cols(Col_Med_Rx_sAddressline2).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sCity).Caption = "PharmacyCity"
            C1ConsolidatedList.Cols(Col_Med_Rx_sCity).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sState).Caption = "PharmacyState"
            C1ConsolidatedList.Cols(Col_Med_Rx_sState).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sZip).Caption = "PharmacyZip"
            C1ConsolidatedList.Cols(Col_Med_Rx_sZip).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sPhone).Caption = "PharmacyPhone"
            C1ConsolidatedList.Cols(Col_Med_Rx_sPhone).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sFax).Caption = "PharmacyFax"
            C1ConsolidatedList.Cols(Col_Med_Rx_sFax).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Rx_sEmail).Caption = "PharmacyEmail"
            C1ConsolidatedList.Cols(Col_Med_Rx_sEmail).AllowEditing = False

            C1ConsolidatedList.Cols(PrescriberNPI).Caption = "PrescriberNPI"
            C1ConsolidatedList.Cols(PrescriberNPI).AllowEditing = False

            C1ConsolidatedList.Cols(Col_Med_Direction).Caption = "Direction"
            C1ConsolidatedList.Cols(Col_Med_Direction).AllowEditing = False

            C1ConsolidatedList.Name = "Meds_ConsolidatedList"

            C1ConsolidatedList.Cols(COL_Med_Select).Width = 50
            C1ConsolidatedList.Cols(COL_Med_Dup).Width = 35
            C1ConsolidatedList.Cols(Col_Med_Drug).Width = 300
            C1ConsolidatedList.Cols(Col_Med_sNDCCode).Width = 150
            C1ConsolidatedList.Cols(Col_Med_Amount).Width = 100
            C1ConsolidatedList.Cols(Col_Med_Frequency).Width = 80
            C1ConsolidatedList.Cols(Col_Med_DaysSupply).Width = 80
            C1ConsolidatedList.Cols(Col_Med_MedicationDate).Width = 100
            C1ConsolidatedList.Cols(Col_Med_StartDate).Width = 100
            C1ConsolidatedList.Cols(Col_Med_EndDate).Width = 100

            C1ConsolidatedList.Cols(Col_Med_Refills).Width = 100
            C1ConsolidatedList.Cols(Col_Med_DrugForm).Width = 100
            C1ConsolidatedList.Cols(Col_Med_Status).Width = 100
            C1ConsolidatedList.Cols(Col_Med_AllowSub).Width = 0

            C1ConsolidatedList.Cols(Col_Med_Rx_sName).Width = 200
            C1ConsolidatedList.Cols(Col_Med_Rx_sNCPDPID).Width = 200
            C1ConsolidatedList.Cols(Col_Med_Rx_NPI).Width = 100
            C1ConsolidatedList.Cols(Col_Med_Rx_sAddressline1).Width = 0
            C1ConsolidatedList.Cols(Col_Med_Rx_sAddressline2).Width = 0
            C1ConsolidatedList.Cols(Col_Med_Rx_sCity).Width = 0
            C1ConsolidatedList.Cols(Col_Med_Rx_sState).Width = 0

            C1ConsolidatedList.Cols(Col_Med_Rx_sZip).Width = 0
            C1ConsolidatedList.Cols(Col_Med_Rx_sEmail).Width = 0
            C1ConsolidatedList.Cols(Col_Med_Rx_sFax).Width = 0
            C1ConsolidatedList.Cols(Col_Med_Rx_sPhone).Width = 0
            C1ConsolidatedList.Cols(PrescriberNPI).Width = 100
            C1ConsolidatedList.Cols(Col_Med_Direction).Width = 0


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            C1ConsolidatedList.Redraw = True
        End Try
    End Sub
    Private Sub RemoveExactDuplicates()
        C1ConsolidatedList.Redraw = False
        Try
            Dim i As Int32 = 1

            Dim _sItemDrugName As String = ""
            Dim _sItemDrugName1 As String = ""
            Dim _sItemNDCCode As String = ""
            Dim _sItemNDCCode1 As String = ""
            Dim _sItemStatus As String = ""
            Dim _sItemStatus1 As String = ""
            Dim _sItemSource As String = ""
            Dim _sItemSource1 As String = ""

            For i = 1 To C1ConsolidatedList.Rows.Count - 1
                If Not i >= C1ConsolidatedList.Rows.Count Then

                    _sItemDrugName = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_Drug)).Trim
                    _sItemNDCCode = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_sNDCCode)).Trim
                    _sItemStatus = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_Status)).Trim
                    _sItemSource = Convert.ToString(C1ConsolidatedList.GetData(i, COL_Med_Source)).Trim
                End If
                For j As Integer = i + 1 To C1ConsolidatedList.Rows.Count - 1
                    If Not j >= C1ConsolidatedList.Rows.Count Then

                        _sItemDrugName1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_Drug)).Trim
                        _sItemNDCCode1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_sNDCCode)).Trim
                        _sItemStatus1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_Status)).Trim
                        _sItemSource1 = Convert.ToString(C1ConsolidatedList.GetData(j, COL_Med_Source)).Trim

                        If (_sItemNDCCode.ToUpper = _sItemNDCCode1.ToUpper) AndAlso ((_sItemDrugName1.ToUpper.Contains(_sItemDrugName.ToUpper)) OrElse (_sItemDrugName.ToUpper.Contains(_sItemDrugName1.ToUpper))) AndAlso _sItemSource1 <> "Current" Then
                            C1ConsolidatedList.Rows(j).Visible = False
                            ' C1ConsolidatedList.Rows.Remove(j)
                            'C1ConsolidatedList.Refresh()
                            'j = j - 1
                        End If
                    End If


                Next

            Next
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            C1ConsolidatedList.Redraw = True

        End Try
    End Sub


    Private Function IsDuplicateExisits(ByVal _sItemSnomedCode As String, ByVal _sItemDiagnosis As String, ByVal _sItemNDCCode As String, ByVal _sItemDrugName As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = C1ConsolidatedList.DataSource ' dtSelectedList
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView


            If _sItemNDCCode <> "" AndAlso _sItemDrugName <> "" Then
                dv.RowFilter = "NDCCode = '" & _sItemNDCCode & "' OR Drug ='" & _sItemDrugName.Replace("'", "''") & "'"
            ElseIf _sItemNDCCode <> "" Then
                dv.RowFilter = "NDCCode = '" & _sItemNDCCode & "'"
            ElseIf _sItemDrugName <> "" Then
                dv.RowFilter = "Drug ='" & _sItemDrugName.Replace("'", "''") & "'"
            Else
                Return False
            End If

            If dv.Count >= 2 Then
                dv.RowFilter = "Source<>'Current'"
                If dv.Count >= 2 Then
                    Return True
                End If
                Return False
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(_dtTemp) Then
                _dtTemp.Dispose()
                _dtTemp = Nothing
            End If

        End Try

    End Function

    Private Sub HighLightSimilarDuplicates()
        C1ConsolidatedList.Redraw = False

        Try
            Dim i As Int32 = 1
            Dim _sItemSnomedCode As String = ""
            Dim _sItemComplaint As String = ""
            Dim _sItemRxNormCode As String = ""
            Dim _sItemAllergy As String = ""
            Dim _sItemDrugName As String = ""
            Dim _sItemFrequency As String = ""
            Dim _sItemDosage As String = ""
            Dim _sItemNDCCode As String = ""
            Dim _sItemSource As String = ""

            If Not IsNothing(C1ConsolidatedList.DataSource) Then

                Dim dtConsolidate As DataTable
                dtConsolidate = C1ConsolidatedList.DataSource

                For k As Integer = dtConsolidate.Rows.Count - 1 To 0 Step -1

                    If C1ConsolidatedList.Rows(k + 1).IsVisible Then
                    Else
                        dtConsolidate.Rows.RemoveAt(k)
                    End If

                Next
                C1ConsolidatedList.DataSource = dtConsolidate
                DesignConsolidatedList()
                For i = 1 To C1ConsolidatedList.Rows.Count - 1

                    _sItemDrugName = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_Drug))
                    _sItemFrequency = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_Frequency))
                    '_sItemDosage = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_sDosage))
                    _sItemNDCCode = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_sNDCCode))
                    ' _sItemSource = Convert.ToString(C1ConsolidatedList.Rows(i)(COL_Med_Source))

                    If IsDuplicateExisits("", "", _sItemNDCCode, _sItemDrugName) Then
                        If Convert.ToString(C1ConsolidatedList.Rows(i)(COL_Med_Source)) <> "Current" Then
                            C1ConsolidatedList.SetCellImage(i, COL_Med_Dup, ImgGrid.Images(1))
                            'C1ConsolidatedList.SetData(i, COL_Med_Dup, "Similar")
                        End If
                    Else
                        C1ConsolidatedList.SetData(i, COL_Med_Dup, Nothing)
                        C1ConsolidatedList.SetCellImage(i, COL_Med_Dup, Nothing)
                    End If

                Next

            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            C1ConsolidatedList.Redraw = True
        End Try
    End Sub


    Public Function DataTableFromMedHXItem(ByVal _listType As String) As DataTable
        Dim currentTable As DataTable = Nothing
        Dim number As Int32 = 0

        dtSelectedList = New DataTable()

        Dim SelectDrug As New DataColumn("Select")
        SelectDrug.DataType = GetType([Boolean])
        Dim Dup As New DataColumn("Dup")
        Dup.DataType = GetType([String])
        Dim SourceName As New DataColumn("Source")
        SourceName.DataType = GetType([String])
        Dim Drug As New DataColumn("Drug")
        Drug.DataType = GetType([String])
        Dim NDCCode As New DataColumn("NDCCode")
        NDCCode.DataType = GetType([String])
        Dim Quantity As New DataColumn("Quantity")
        Quantity.DataType = GetType([String])
        Dim DrugForm As New DataColumn("DrugForm")
        DrugForm.DataType = GetType([String])
        Dim Frequency As New DataColumn("Frequency")
        Frequency.DataType = GetType(String)
        Dim DaysSupply As New DataColumn("DaysSupply")
        DaysSupply.DataType = GetType([String])
        Dim LastUpdated As New DataColumn("LastUpdated")
        LastUpdated.DataType = GetType(DateTime)
        Dim StartDate As New DataColumn("StartDate")
        StartDate.DataType = GetType(DateTime)

        Dim EndDate As New DataColumn("EndDate")
        EndDate.DataType = GetType(DateTime)


        Dim Refills As New DataColumn("Refills")
        Refills.DataType = GetType([String])
        Dim Status As New DataColumn("Status")
        Status.DataType = GetType([String])
        Dim AllowSub As New DataColumn("AllowSub")
        AllowSub.DataType = GetType([String])
        Dim Rx_sName As New DataColumn("PharmacyName")
        Rx_sName.DataType = GetType([String])
        Dim Rx_sNCPDPID As New DataColumn("PharmacyNCPDPID")
        Rx_sNCPDPID.DataType = GetType([String])
        Dim NPI As New DataColumn("NPI")
        NPI.DataType = GetType([String])
        Dim Rx_sAddressline1 As New DataColumn("PharmacyAddressline1")
        Rx_sAddressline1.DataType = GetType([String])
        Dim Rx_sAddressline2 As New DataColumn("PharmacyAddressline2")
        Rx_sAddressline2.DataType = GetType([String])
        Dim Rx_sCity As New DataColumn("PharmacyCity")
        Rx_sCity.DataType = GetType([String])
        Dim Rx_sState As New DataColumn("PharmacyState")
        Rx_sState.DataType = GetType([String])
        Dim Rx_sZip As New DataColumn("PharmacyZip")
        Rx_sZip.DataType = GetType([String])
        Dim Rx_sPhone As New DataColumn("PharmacyPhone")
        Rx_sPhone.DataType = GetType([String])
        Dim Rx_sFax As New DataColumn("PharmacyFax")
        Rx_sFax.DataType = GetType([String])
        Dim Rx_sEmail As New DataColumn("PharmacyEmail")
        Rx_sEmail.DataType = GetType([String])
        Dim User As New DataColumn("PrescriberNPI")
        User.DataType = GetType([String])

        Dim Direction As New DataColumn("Direction")
        Direction.DataType = GetType([String])
        Try
            dtSelectedList.Columns.Add(SelectDrug)
            dtSelectedList.Columns.Add(Dup)
            dtSelectedList.Columns.Add(SourceName)
            dtSelectedList.Columns.Add(Drug)
            dtSelectedList.Columns.Add(NDCCode)
            dtSelectedList.Columns.Add(Quantity)
            dtSelectedList.Columns.Add(DrugForm)
            dtSelectedList.Columns.Add(Frequency)
            dtSelectedList.Columns.Add(DaysSupply)
            dtSelectedList.Columns.Add(LastUpdated)
            dtSelectedList.Columns.Add(StartDate)
            dtSelectedList.Columns.Add(EndDate)
            dtSelectedList.Columns.Add(Refills)

            dtSelectedList.Columns.Add(Status)
            dtSelectedList.Columns.Add(AllowSub)
            dtSelectedList.Columns.Add(Rx_sName)
            dtSelectedList.Columns.Add(Rx_sNCPDPID)
            dtSelectedList.Columns.Add(NPI)
            dtSelectedList.Columns.Add(Rx_sAddressline1)
            dtSelectedList.Columns.Add(Rx_sAddressline2)
            dtSelectedList.Columns.Add(Rx_sCity)
            dtSelectedList.Columns.Add(Rx_sState)
            dtSelectedList.Columns.Add(Rx_sZip)
            dtSelectedList.Columns.Add(Rx_sPhone)
            dtSelectedList.Columns.Add(Rx_sFax)
            dtSelectedList.Columns.Add(Rx_sEmail)
            dtSelectedList.Columns.Add(User)
            dtSelectedList.Columns.Add(Direction)

            Dim _dSource As List(Of MedHx.MedHxItemNew) = Nothing
            _dSource = _dataSource

            If _listType = "All" Then
                currentTable = GetActiveMedication()
                For Each item1 As DataRow In currentTable.Rows
                    Dim dr As DataRow = dtSelectedList.NewRow()
                    dr("Select") = True
                    dr("Dup") = ""
                    dr("Source") = "Current"
                    dr("Drug") = item1("sMedication")
                    dr("NDCCode") = item1("NDCCode")


                    dr("Quantity") = Convert.ToString(item1("sAmount"))


                    dr("Frequency") = "" 'item1("sFrequency")
                    dr("LastUpdated") = item1("dtMedicationDate")
                    dr("StartDate") = item1("dtStartDate")

                    dr("EndDate") = item1("dtEndDate")


                    dr("Refills") = Convert.ToString(item1("sRefills"))

                    dr("DrugForm") = item1("DrugForm")
                    dr("Status") = item1("sStatus")
                    dr("AllowSub") = "No"
                    dr("PharmacyName") = item1("Pharmacy")
                    dr("PharmacyNCPDPID") = ""
                    dr("NPI") = ""
                    dr("PharmacyAddressline1") = ""
                    dr("PharmacyAddressline2") = ""
                    dr("PharmacyCity") = ""
                    dr("PharmacyState") = ""
                    dr("PharmacyZip") = ""
                    dr("PharmacyPhone") = ""
                    dr("PharmacyFax") = ""
                    dr("PharmacyEmail") = ""
                    dr("PrescriberNPI") = item1("NPI")

                    dr("DaysSupply") = Convert.ToString(item1("sDuration"))
                    dr("Direction") = ""
                    dtSelectedList.Rows.Add(dr)
                Next
                For Each item As MedHx.MedHxItemNew In _dSource
                    Dim dr As DataRow = dtSelectedList.NewRow()

                    dr("Select") = item.[Select]
                    dr("Dup") = item.Dup
                    dr("Source") = item.PayerName
                    dr("Drug") = item.DrugName
                    dr("NDCCode") = item.NDCCode

                    dr("Quantity") = item.DrugQty
                    dr("Frequency") = ""
                    dr("LastUpdated") = item.MedicationDate
                    dr("StartDate") = item.StartDate




                    dr("Refills") = item.Refills

                    dr("DrugForm") = GetPotencyCodeValue(item.PotencyCode)

                    dr("Status") = item.Status

                    If item.AllowSubstition = False Then
                        dr("AllowSub") = "No"
                    Else
                        dr("AllowSub") = "Yes"
                    End If
                    dr("PharmacyName") = item.Pharmacy
                    dr("PharmacyNCPDPID") = item.NCPDPId
                    dr("NPI") = item.NPI
                    dr("PharmacyAddressline1") = item.PharmacyAddress1

                    dr("PharmacyAddressline2") = item.PharmacyAddress2
                    dr("PharmacyCity") = item.PharmacyCity
                    dr("PharmacyState") = item.PharmacyState
                    dr("PharmacyZip") = item.PharmacyZip

                    dr("PharmacyPhone") = item.PharmacyPhone
                    dr("PharmacyFax") = item.PharmacyFax
                    dr("PharmacyEmail") = item.PharmacyEmail
                    dr("PrescriberNPI") = item.PrescriberNPI
                    dr("DaysSupply") = item.DaySupply
                    dr("Direction") = item.Direction
                    dtSelectedList.Rows.Add(dr)
                Next

                Return dtSelectedList
            Else
                'Dim dv As DataView = Nothing
                'Dim dtTemp As DataTable = Nothing
                'Dim dt As DataTable = Nothing
                'dt = C1ConsolidatedList.DataSource
                'dtTemp = dt.Copy()
                'dv = dtTemp.DefaultView
                'dv.RowFilter = "[Select] = 'true'"
                'Return dv.ToTable
                Return C1ConsolidatedList.DataSource
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("gloMedHXRconcilation" & ex.ToString(), True)
            Return Nothing
        Finally
            If Not IsNothing(currentTable) Then
                currentTable.Dispose()
                currentTable = Nothing
            End If
            If Not IsNothing(SelectDrug) Then
                SelectDrug.Dispose()
                SelectDrug = Nothing
            End If
            If Not IsNothing(Dup) Then
                Dup.Dispose()
                Dup = Nothing
            End If
            If Not IsNothing(SourceName) Then
                SourceName.Dispose()
                SourceName = Nothing
            End If
            If Not IsNothing(Drug) Then
                Drug.Dispose()
                Drug = Nothing
            End If
            If Not IsNothing(NDCCode) Then
                NDCCode.Dispose()
                NDCCode = Nothing
            End If
            If Not IsNothing(Quantity) Then
                Quantity.Dispose()
                Quantity = Nothing
            End If
            If Not IsNothing(DrugForm) Then
                DrugForm.Dispose()
                DrugForm = Nothing
            End If
            If Not IsNothing(Frequency) Then
                Frequency.Dispose()
                Frequency = Nothing
            End If
            If Not IsNothing(DaysSupply) Then
                DaysSupply.Dispose()
                DaysSupply = Nothing
            End If
            If Not IsNothing(LastUpdated) Then
                LastUpdated.Dispose()
                LastUpdated = Nothing
            End If
            If Not IsNothing(StartDate) Then
                StartDate.Dispose()
                StartDate = Nothing
            End If

            If Not IsNothing(EndDate) Then
                EndDate.Dispose()
                EndDate = Nothing
            End If


            If Not IsNothing(AllowSub) Then
                AllowSub.Dispose()
                AllowSub = Nothing
            End If
            If Not IsNothing(Rx_sName) Then
                Rx_sName.Dispose()
                Rx_sName = Nothing
            End If

        End Try

    End Function

    Private Function GetPotencyCodeValue(potencyCode As String) As String
        Dim potencyValue As String = ""
        Dim _strSQl = "Select sDescription  from PotencyCodeMaster where sPotencycode='" & potencyCode.Trim & "'"
        Dim dtb As New DataTable
        Try
            Using cnn As New SqlConnection(GetConnectionString())
                cnn.Open()
                Using dad As New SqlDataAdapter(_strSQl, cnn)
                    dad.Fill(dtb)
                End Using
                cnn.Close()
            End Using
            If dtb IsNot Nothing AndAlso dtb.Rows.Count > 0 Then
                potencyValue = dtb.Rows(0)(0).ToString()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("gloMedHXRconcilation" & ex.ToString(), True)
        Finally
            If Not dtb Is Nothing Then
                dtb.Dispose()
                dtb = Nothing
            End If
        End Try
        Return potencyValue
    End Function


    Private Sub C1ConsolidatedList_AfterSort(sender As System.Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles C1ConsolidatedList.AfterSort
        HighLightSimilarDuplicates()
    End Sub

    Public Function FillActiveMedication(ByVal nPatientId As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_Active_GetMedication", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
            ''dt Contains Following Columns
            ' ''dtMedicationDate, sMedication ,sDosage ,sRoute, sFrequency, sDuration, sAmount, sStatus, dtStartDate, dtEndDate, UserID , UserName 
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Function

    Public Function GetActiveMedication() As DataTable
        Try
            Dim strfilter As String = String.Empty
            Dim dtPatientDetails As DataTable = Nothing
            dtPatientDetails = FillActiveMedication(_nPatientID)
            Dim dvfilter As DataView
            dvfilter = dtPatientDetails.DefaultView
            dvfilter.RowFilter = "sStatus='Active'"

            ''If cmbStatus.Text = "Active" Then
            ''    strfilter = "dtmedicationdate >=  '" & dtpToDate.Value.Date & " 12:00:00 am" & "' and dtmedicationdate <= '" & dtpToDate.Value.Date & " 11:59:59 pm" & "' "

            ''Else
            ''    strfilter = "dtmedicationdate >=  '" & dtpFromDate.Value.Date & " 12:00:00 am" & "' and dtmedicationdate <= '" & dtpToDate.Value.Date & " 11:59:59 pm" & "' "

            ''End If

            ''If cmbStatus.Text <> "All" And cmbStatus.Text <> "Active All" Then '' filter only status ''
            ''    strfilter = strfilter & " and sStatus = '" & cmbStatus.Text & "' "
            ''ElseIf cmbStatus.Text = "Active All" Then
            ''    If chkenbdate.Checked = False Then 'Developer:Pradeep/Date:03/05/2012/Bug:22468/Reason:Date filter was not implemented 
            ''        strfilter = "sStatus = " & "'Active'"
            ''    Else
            ''        strfilter = strfilter & " and  sStatus = " & "'Active'"
            ''    End If

            ''End If

            'strfilter = "dtmedicationdate >=  '" & DateTime.Now.Date & " 12:00:00 am" & "' and dtmedicationdate <= '" & DateTime.Now.Date & " 11:59:59 pm" & "' "

            'dvfilter.RowFilter = strfilter
            'dtfiltered = dvfilter.ToTable

            'If Not IsNothing(dvfilter) Then
            '    dvfilter.Dispose()
            '    dvfilter = Nothing
            'End If
            Return dvfilter.ToTable()

            'dtfiltered.DefaultView.ToTable()
            'c1ActiveMedication.DataSource = dtfiltered.DefaultView
            'c1ActiveMedication.Cols("dtmedicationdate").Caption = "Updated"
            'c1ActiveMedication.Cols("username").Caption = "Updated By"
            'c1ActiveMedication.Cols("smedication").Caption = "Drug"
            'c1ActiveMedication.Cols("Prescriber").Caption = "Prescriber"
            'c1ActiveMedication.Cols("dtstartdate").Caption = "Start Date"
            'c1ActiveMedication.Cols("dtenddate").Caption = "End Date"
            'c1ActiveMedication.Cols("sstatus").Caption = "Status"
            'c1ActiveMedication.Cols("sfrequency").Caption = "Patient Directions"
            'c1ActiveMedication.Cols("sduration").Caption = "Duration"
            'c1ActiveMedication.Cols("samount").Caption = "Quantity"
            'c1ActiveMedication.Cols("sRefills").Caption = "Refills"
            'c1ActiveMedication.Cols("sMethod").Caption = "Issue Method"
            'c1ActiveMedication.Cols("Pharmacy").Caption = "Pharmacy"

            'c1ActiveMedication.Cols("username").Visible = True

            'c1ActiveMedication.AllowEditing = False
            'c1ActiveMedication.AllowSorting = True

            ''Dim _width As Integer = pnlPatientDetail.Width

            ''If _width < Convert.ToInt32(c1ActiveMedicationize) And c1ActiveMedicationize <> Nothing Then
            ''    _width = Convert.ToInt32(c1ActiveMedicationize)
            ''End If

            ''c1ActiveMedication.Cols("dtmedicationdate").Width = _width * 0.08
            ''c1ActiveMedication.Cols("smedication").Width = _width * 0.2
            ''c1ActiveMedication.Cols("Prescriber").Width = _width * 0.1
            ''c1ActiveMedication.Cols("dtstartdate").Width = _width * 0.08
            ''c1ActiveMedication.Cols("dtenddate").Width = _width * 0.08
            ''c1ActiveMedication.Cols("sstatus").Width = _width * 0.07
            ''c1ActiveMedication.Cols("username").Width = _width * 0.09
            ''c1ActiveMedication.Cols("sfrequency").Width = _width * 0.12
            ''c1ActiveMedication.Cols("sduration").Width = _width * 0.07
            ''c1ActiveMedication.Cols("samount").Width = _width * 0.07
            ''c1ActiveMedication.Cols("sRefills").Width = _width * 0.05
            ''    c1ActiveMedication.Cols("sMethod").Width = _width * 0.095
            ''c1ActiveMedication.Cols("Pharmacy").Width = _width * 0.09

            'If Not IsNothing(dtfiltered) Then
            '    dtfiltered.Dispose()
            '    dtfiltered = Nothing
            'End If


            'c1ActiveMedication.Cols("samount").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GetActiveMedication" & ex.ToString(), True)
            Return Nothing
        End Try

    End Function

End Class
