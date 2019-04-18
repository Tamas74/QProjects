Imports System.Windows.Forms
Imports gloSettings

Public Class frmViewQuickStatus
#Region "Variable Declaration"
    Dim _Flexgrid As C1.Win.C1FlexGrid.C1FlexGrid
    Dim bIsUnknown As Boolean = False
    '  Dim dtMedlist As DataTable = Nothing
    ' Private dtList As DataTable = Nothing
    Private Dv As DataView = Nothing
    Public Property DtMedlist As DataTable
#End Region

#Region "Constants--"
    Private COL_COUNT As Integer = 56
    'new column added for Rx feild changes
    Private COL_MEDICATIONID As Integer = 0
    Private COL_VISITID As Integer = 1
    Private COL_PATIENTID As Integer = 2
    Private COL_MEDICATION_NAME As Integer = 3

    'Infobutton
    Private COl_INFOBUTTON As Integer = 4

    Private COL_STARTDATE As Integer = 5
    Private COL_ENDDATE As Integer = 6
    Private COL_STATUS As Integer = 7
    'code added by sagar for getting the user name who changed the medication item in C1FlexGrid on 30 apr 2007
    Private COL_LOGINNAME As Integer = 8
    Private COL_RENEWED As Integer = 9
    Private COL_FREQUENCY As Integer = 10
    Private COL_DURATION As Integer = 11
    Private COL_AMOUNT As Integer = 12
    Private COL_MEDICATIONDATE As Integer = 13
    Private COL_DOSAGE As Integer = 14
    Private COL_REASON As Integer = 15
    Private COL_DDID As Integer = 16
    Private COL_DRUGFORM As Integer = 17
    'Private COL_USERID As Integer = 16
    Private COL_ROUTE As Integer = 18
    'For De-Normalization
    Private COL_Narcotic As Integer = 19
    Private COL_NDCCode As Integer = 20
    Private COL_DRUGQTYQUALIFIER As Integer = 21
    Private COL_PBMSOURCENAME As Integer = 22
    'For De-Normalization

    Private COL_Rx_sRefills As Integer = 23
    Private COL_Rx_sNotes As Integer = 24
    Private COL_Rx_sMethod As Integer = 25
    Private COL_Rx_bMaySubstitute As Integer = 26
    Private COL_Rx_nDrugID As Integer = 27
    Private COL_Rx_blnflag As Integer = 28
    Private COL_Rx_sLotNo As Integer = 29
    Private COL_Rx_dtExpirationdate As Integer = 30
    Private COL_Rx_nProviderId As Integer = 31
    Private COL_Rx_sChiefComplaints As Integer = 32
    Private COL_Rx_sStatus As Integer = 33
    Private COL_Rx_sRxReferenceNumber As Integer = 34
    Private COL_Rx_sRefillQualifier As Integer = 35
    Private COL_Rx_nPharmacyId As Integer = 36
    Private COL_Rx_sNCPDPID As Integer = 37
    Private COL_Rx_nContactID As Integer = 38
    Private COL_Rx_sName As Integer = 39
    Private COL_Rx_sAddressline1 As Integer = 40
    Private COL_Rx_sAddressline2 As Integer = 41
    Private COL_Rx_sCity As Integer = 42
    Private COL_Rx_sState As Integer = 43
    Private COL_Rx_sZip As Integer = 44
    Private COL_Rx_sEmail As Integer = 45
    Private COL_Rx_sFax As Integer = 46
    Private COL_Rx_sPhone As Integer = 47
    Private COL_Rx_sServiceLevel As Integer = 48
    Private COL_Rx_sPrescriberNotes As Integer = 49
    Private COL_Rx_eRxStatus As Integer = 50
    Private COL_Rx_eRxStatusMessage As Integer = 51
    Private COL_RxMedDMSID As Integer = 52 ''''For CCHIT Medication Reconcilation store DMS ID 
    Private COL_RowNumber As Integer = 53 ''''coloumn will contain the item number with respect to medication collection
    Private COL_MEDICATION As Integer = 54
    Private COl_State As Integer = 55
#End Region

#Region "Form Constructor"
    Public Sub New(ByVal Flex As C1.Win.C1FlexGrid.C1FlexGrid)

        InitializeComponent()
        _Flexgrid = Flex

    End Sub
#End Region

#Region "Form_Load"

    Private Sub frmViewQuickStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            DtMedlist = Nothing

            SetFlexgridColumns()
            AddandSetFlexgridData(_Flexgrid)
            SetQuickStatus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Quick Status Button :" & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        
    End Sub
    Private Sub SetQuickStatus()
        Try
            Dim dtTempList As New DataTable()
            Dim MedicationID As New DataColumn("MedicationID")
            MedicationID.DataType = GetType([String])
            Dim Drugstatus As New DataColumn("Drugstatus")
            Drugstatus.DataType = GetType([String])

            dtTempList.Columns.Add(MedicationID)
            dtTempList.Columns.Add(Drugstatus)
            Dim dr As DataRow = Nothing
            With c1MedicationList


                Dim _Duration As Integer = 0
                Dim nDuration As String() = Nothing
                For i As Integer = 1 To c1MedicationList.Rows.Count - 1
                    Dv.RowFilter = "MedicationID ='" & c1MedicationList.GetData(i, COL_MEDICATIONID).ToString().Trim() & "'"
                    If Dv.Count > 0 Then

                        If c1MedicationList.GetData(i, COL_DURATION).ToString().Trim() = "" Then
                            _Duration = 0
                        Else
                            nDuration = Nothing
                            nDuration = c1MedicationList.GetData(i, COL_DURATION).ToString().Trim.Split(" ")
                            If nDuration.Length > 0 Then
                                If nDuration.Length = 2 Then
                                    If IsNumeric(nDuration(0)) Then
                                        If nDuration(1) = "Days" Then
                                            _Duration = nDuration(0)
                                        ElseIf nDuration(1) = "Weeks" Then
                                            _Duration = Convert.ToInt32(nDuration(0)) * 7
                                        ElseIf nDuration(1) = "Months" Then
                                            _Duration = DateDiff(DateInterval.Day, CType(c1MedicationList.GetData(i, COL_STARTDATE), Date), CType(c1MedicationList.GetData(i, COL_STARTDATE), Date).AddMonths(nDuration(0)))
                                            ' _Duration = Convert.ToInt32(nDuration(0)) * 30
                                        End If
                                    End If
                                Else
                                    If IsNumeric(nDuration(0)) Then
                                        _Duration = nDuration(0)
                                    End If
                                End If
                            Else
                                _Duration = 0
                            End If
                        End If
                        If DateTime.Now.Date <= CType(c1MedicationList.GetData(i, COL_STARTDATE), Date).AddDays(_Duration) Then
                            .SetData(i, COL_STATUS, "Active")
                        ElseIf CType(c1MedicationList.GetData(i, COL_STARTDATE), Date).AddDays(_Duration).AddMonths(2) > DateTime.Now.Date Then
                            .SetData(i, COL_STATUS, "Unknown")
                        Else
                            .SetData(i, COL_STATUS, "Completed")
                        End If

                    End If
                    dr = dtTempList.NewRow()
                    dr("MedicationID") = c1MedicationList.GetData(i, COL_MEDICATIONID).ToString
                    dr("Drugstatus") = c1MedicationList.GetData(i, COL_STATUS).ToString
                    dtTempList.Rows.Add(dr)
                Next
            End With
            DtMedlist = dtTempList
            'dtTempList.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Quick Status Button :" & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region
  
    Private Sub SetFlexgridColumns()
        Try

        
        With c1MedicationList
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            .Redraw = False
           
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width / 10


                .Cols(COL_MEDICATIONID).Width = 0
                .Cols(COL_VISITID).Width = 0
                .Cols(COL_PATIENTID).Width = 0
                .Cols(COL_MEDICATION_NAME).Width = _Width * 5


                '.Cols(COl_INFOBUTTON).Width = _Width * 0.3

                .Cols(COL_MEDICATION).Width = 0
                .Cols(COL_DOSAGE).Width = 0
                .Cols(COL_ROUTE).Width = 0
                .Cols(COL_DRUGFORM).Width = 0
                .Cols(COL_FREQUENCY).Width = _Width * 1.8
                .Cols(COL_DURATION).Width = _Width * 1.1
                .Cols(COL_AMOUNT).Width = _Width * 1.1
                .Cols(COL_STARTDATE).Width = _Width * 1.25
                .Cols(COL_STARTDATE).DataType = GetType(System.DateTime)
                .Cols(COL_STARTDATE).Format = "MM/dd/yyyy"

                .Cols(COL_ENDDATE).Width = _Width * 1.2

                .Cols(COL_ENDDATE).DataType = GetType(System.DateTime)
                .Cols(COL_ENDDATE).Format = "MM/dd/yyyy"

                .Cols(COL_LOGINNAME).Width = _Width * 1.3
                .Cols(COL_MEDICATIONDATE).Width = 0
                .Cols(COL_STATUS).Width = _Width * 1.0
                .Cols(COL_REASON).Width = 0
                .Cols(COL_DDID).Width = 0

                .Cols(COL_RENEWED).Width = _Width * 2

                .Cols(COL_Narcotic).Width = 0
                .Cols(COL_NDCCode).Width = 0

                .Cols(COL_DRUGQTYQUALIFIER).Width = 0

                .Cols(COL_PBMSOURCENAME).Width = 0
                .Cols(COL_Rx_sRefills).Width = _Width * 1.0
                .Cols(COL_Rx_sNotes).Width = 0
                .Cols(COL_Rx_sMethod).Width = _Width * 1.4
                .Cols(COL_Rx_bMaySubstitute).Width = 0
                .Cols(COL_Rx_nDrugID).Width = 0
                .Cols(COL_Rx_blnflag).Width = 0
                .Cols(COL_Rx_sLotNo).Width = 0
                .Cols(COL_Rx_dtExpirationdate).Width = 0
                .Cols(COL_Rx_nProviderId).Width = 0
                .Cols(COL_Rx_sChiefComplaints).Width = 0
                .Cols(COL_Rx_sStatus).Width = 0
                .Cols(COL_Rx_sRxReferenceNumber).Width = 0
                .Cols(COL_Rx_sRefillQualifier).Width = 0
                .Cols(COL_Rx_nPharmacyId).Width = 0
                .Cols(COL_Rx_sNCPDPID).Width = 0
                .Cols(COL_Rx_nContactID).Width = 0
                .Cols(COL_Rx_sName).Width = _Width * 1.5
                .Cols(COL_Rx_sAddressline1).Width = 0
                .Cols(COL_Rx_sAddressline2).Width = 0
                .Cols(COL_Rx_sCity).Width = 0
                .Cols(COL_Rx_sState).Width = 0
                .Cols(COL_Rx_sZip).Width = 0
                .Cols(COL_Rx_sEmail).Width = 0
                .Cols(COL_Rx_sFax).Width = 0
                .Cols(COL_Rx_sPhone).Width = 0
                .Cols(COL_Rx_sServiceLevel).Width = 0
                .Cols(COL_Rx_sPrescriberNotes).Width = 0
                .Cols(COL_Rx_eRxStatus).Width = 0
                .Cols(COL_Rx_eRxStatusMessage).Width = 0
                .Cols(COL_RxMedDMSID).Width = 0
                .Cols(COL_RowNumber).Width = 0
            .Cols(COl_State).Width = 0


            .SetData(0, COL_MEDICATIONID, "Medication ID")
            .SetData(0, COL_VISITID, "Visit ID")
            .SetData(0, COL_PATIENTID, "Patient ID")
            .SetData(0, COL_MEDICATION, "Drug Name")

            .SetData(0, COl_INFOBUTTON, "")
            .SetData(0, COL_MEDICATION_NAME, "Drug")
            .SetData(0, COL_FREQUENCY, "Patient Directions")
            .SetData(0, COL_AMOUNT, "Quantity")
            .SetData(0, COL_DOSAGE, "Dosage")
            .SetData(0, COL_ROUTE, "Route")
            .SetData(0, COL_DRUGFORM, "Drug Form")

                .SetData(0, COL_FREQUENCY, "Patient Directions")
            .SetData(0, COL_DURATION, "Duration")
            .SetData(0, COL_STARTDATE, "Start Date")
            .SetData(0, COL_ENDDATE, "End Date")

                .SetData(0, COL_AMOUNT, "Quantity")
            .SetData(0, COL_MEDICATIONDATE, "Medication Date")
            .SetData(0, COL_STATUS, "Status")
            .SetData(0, COL_REASON, "Reason")
            .SetData(0, COL_DDID, "Dispensible Drug ID")

            .SetData(0, COL_LOGINNAME, "Updated By")

            .SetData(0, COL_RENEWED, "Renewed")

            .SetData(0, COL_Narcotic, "Narcotic")
            .SetData(0, COL_NDCCode, "NDC Code")
            .SetData(0, COL_DRUGQTYQUALIFIER, "Drug Unit")
            .SetData(0, COL_PBMSOURCENAME, "PBM Name")
            
                .SetData(0, COL_Rx_sRefills, "Refills")
            .SetData(0, COL_Rx_sNotes, "Rx_sNotes")

            .SetData(0, COL_Rx_sMethod, "Issue Method")
            .SetData(0, COL_Rx_bMaySubstitute, "Rx_bMaySubstitute")
            .SetData(0, COL_Rx_nDrugID, "Rx_nDrugID")
            .SetData(0, COL_Rx_blnflag, "Rx_blnflag")
            .SetData(0, COL_Rx_sLotNo, "Rx_sLotNo")
            .SetData(0, COL_Rx_dtExpirationdate, "Rx_dtExpirationdate")
            .SetData(0, COL_Rx_nProviderId, "Rx_nProviderId")
            .SetData(0, COL_Rx_sChiefComplaints, "Rx_sChiefComplaints")
            .SetData(0, COL_Rx_sStatus, "Rx_sStatus")
            .SetData(0, COL_Rx_sRxReferenceNumber, "Rx_sRxReferenceNumber")
            .SetData(0, COL_Rx_sRefillQualifier, "Rx_sRefillQualifier")
            .SetData(0, COL_Rx_nPharmacyId, "Rx_nPharmacyId")
            .SetData(0, COL_Rx_sNCPDPID, "Rx_sNCPDPID")
            .SetData(0, COL_Rx_nContactID, "Rx_nContactID")

            .SetData(0, COL_Rx_sName, "Pharmacy")
            .SetData(0, COL_Rx_sAddressline1, "Rx_sAddressline1")
            .SetData(0, COL_Rx_sAddressline2, "Rx_sAddressline2")
            .SetData(0, COL_Rx_sCity, "Rx_sCity")
            .SetData(0, COL_Rx_sState, "Rx_sState")
            .SetData(0, COL_Rx_sZip, "Rx_sZip")
            .SetData(0, COL_Rx_sEmail, "Rx_sEmail")
            .SetData(0, COL_Rx_sFax, "Rx_sFax")
            .SetData(0, COL_Rx_sPhone, "Rx_sPhone")
            .SetData(0, COL_Rx_sServiceLevel, "Rx_sServiceLevel")
            .SetData(0, COL_Rx_sPrescriberNotes, "Rx_sPrescriberNotes")
            .SetData(0, COL_Rx_eRxStatus, "Rx_eRxStatus")
            .SetData(0, COL_Rx_eRxStatusMessage, "Rx_eRxStatusMessage")
            .SetData(0, COL_RxMedDMSID, "DMSID")
            .SetData(0, COL_RowNumber, "RowNumber")
            .SetData(0, COl_State, "State")

            .Cols(COL_MEDICATIONID).Visible = False
                .Cols(COL_VISITID).Visible = False
                .Cols(COL_PATIENTID).Visible = False
            .Cols(COL_MEDICATION).Visible = False
                .Cols(COL_MEDICATION_NAME).Visible = True

            .Cols(COl_INFOBUTTON).Visible = False

            .Cols(COL_DOSAGE).Visible = True
            .Cols(COL_ROUTE).Visible = True
            .Cols(COL_DRUGFORM).Visible = True
            .Cols(COL_FREQUENCY).Visible = True
            .Cols(COL_DURATION).Visible = True
                .Cols(COL_AMOUNT).Visible = True
            .Cols(COL_STARTDATE).Visible = True
            .Cols(COL_ENDDATE).Visible = True
            .Cols(COL_LOGINNAME).Visible = True
            .Cols(COL_RENEWED).Visible = True
            .Cols(COL_MEDICATIONDATE).Visible = False
                .Cols(COL_STATUS).Visible = True
            .Cols(COL_REASON).Visible = False
            .Cols(COL_DDID).Visible = False
            
            .Cols(COL_Narcotic).Visible = False
                .Cols(COL_NDCCode).Visible = False
            .Cols(COL_DRUGQTYQUALIFIER).Visible = False
            .Cols(COL_PBMSOURCENAME).Visible = True

            .Cols(COL_Rx_sRefills).Visible = True
            .Cols(COL_Rx_sNotes).Visible = True
            .Cols(COL_Rx_sMethod).Visible = True
            .Cols(COL_Rx_bMaySubstitute).Visible = True
            .Cols(COL_Rx_nDrugID).Visible = True
            .Cols(COL_Rx_blnflag).Visible = True
            .Cols(COL_Rx_sLotNo).Visible = True
            .Cols(COL_Rx_dtExpirationdate).Visible = True
            .Cols(COL_Rx_nProviderId).Visible = True
            .Cols(COL_Rx_sChiefComplaints).Visible = True
            .Cols(COL_Rx_sStatus).Visible = True
            .Cols(COL_Rx_sRxReferenceNumber).Visible = True
            .Cols(COL_Rx_sRefillQualifier).Visible = True
            .Cols(COL_Rx_nPharmacyId).Visible = True
            .Cols(COL_Rx_sNCPDPID).Visible = True
            .Cols(COL_Rx_nContactID).Visible = True
                .Cols(COL_Rx_sName).Visible = True
            .Cols(COL_Rx_sAddressline1).Visible = True
            .Cols(COL_Rx_sAddressline2).Visible = True
            .Cols(COL_Rx_sCity).Visible = True
            .Cols(COL_Rx_sState).Visible = True
            .Cols(COL_Rx_sZip).Visible = True
            .Cols(COL_Rx_sEmail).Visible = True
            .Cols(COL_Rx_sFax).Visible = True
            .Cols(COL_Rx_sPhone).Visible = True
            .Cols(COL_Rx_sServiceLevel).Visible = True
            .Cols(COL_Rx_sPrescriberNotes).Visible = True
            .Cols(COL_Rx_eRxStatus).Visible = True
            .Cols(COL_Rx_eRxStatusMessage).Visible = True
            .Cols(COL_RxMedDMSID).Visible = True
            .Cols(COL_RowNumber).Visible = True
            .Cols(COl_State).Visible = False


            .Cols(COL_MEDICATIONID).AllowEditing = False
                .Cols(COL_VISITID).AllowEditing = False
                .Cols(COL_PATIENTID).AllowEditing = False
                .Cols(COL_MEDICATION).AllowEditing = False
            .Cols(COL_MEDICATION_NAME).AllowEditing = False
            'infobutton
            .Cols(COl_INFOBUTTON).AllowResizing = False
            .Cols(COl_INFOBUTTON).AllowEditing = False

            .Cols(COL_DOSAGE).AllowEditing = False
            .Cols(COL_ROUTE).AllowEditing = False
            .Cols(COL_FREQUENCY).AllowEditing = False
            .Cols(COL_DURATION).AllowEditing = False
                .Cols(COL_AMOUNT).AllowEditing = False
            .Cols(COL_STARTDATE).AllowEditing = False
            .Cols(COL_ENDDATE).AllowEditing = False
            .Cols(COL_LOGINNAME).AllowEditing = False
            .Cols(COL_MEDICATIONDATE).AllowEditing = False
            .Cols(COL_STATUS).AllowEditing = False
            .Cols(COL_REASON).AllowEditing = False
            .Cols(COL_DDID).AllowEditing = False

            .Cols(COL_RENEWED).AllowEditing = False

            .Cols(COL_Narcotic).AllowEditing = False
            .Cols(COL_NDCCode).AllowEditing = False
            .Cols(COL_DRUGFORM).AllowEditing = False
            .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False
            .Cols(COL_PBMSOURCENAME).AllowEditing = False
          
            .Cols(COL_Rx_sRefills).AllowEditing = False
            .Cols(COL_Rx_sNotes).AllowEditing = False
            .Cols(COL_Rx_sMethod).AllowEditing = False
            .Cols(COL_Rx_bMaySubstitute).AllowEditing = False
            .Cols(COL_Rx_nDrugID).AllowEditing = False
            .Cols(COL_Rx_blnflag).AllowEditing = False
            .Cols(COL_Rx_sLotNo).AllowEditing = False
            .Cols(COL_Rx_dtExpirationdate).AllowEditing = False
            .Cols(COL_Rx_nProviderId).AllowEditing = False
            .Cols(COL_Rx_sChiefComplaints).AllowEditing = False
            .Cols(COL_Rx_sStatus).AllowEditing = False
            .Cols(COL_Rx_sRxReferenceNumber).AllowEditing = False
            .Cols(COL_Rx_sRefillQualifier).AllowEditing = False
            .Cols(COL_Rx_nPharmacyId).AllowEditing = False
            .Cols(COL_Rx_sNCPDPID).AllowEditing = False
            .Cols(COL_Rx_nContactID).AllowEditing = False
                .Cols(COL_Rx_sName).AllowEditing = False
            .Cols(COL_Rx_sAddressline1).AllowEditing = False
            .Cols(COL_Rx_sAddressline2).AllowEditing = False
            .Cols(COL_Rx_sCity).AllowEditing = False
            .Cols(COL_Rx_sState).AllowEditing = False
            .Cols(COL_Rx_sZip).AllowEditing = False
            .Cols(COL_Rx_sEmail).AllowEditing = False
            .Cols(COL_Rx_sFax).AllowEditing = False
            .Cols(COL_Rx_sPhone).AllowEditing = False
            .Cols(COL_Rx_sServiceLevel).AllowEditing = False
            .Cols(COL_Rx_sPrescriberNotes).AllowEditing = False
            .Cols(COL_Rx_eRxStatus).AllowEditing = False
            .Cols(COL_Rx_eRxStatusMessage).AllowEditing = False
            .Cols(COL_RxMedDMSID).AllowEditing = False
            .Cols(COL_RowNumber).AllowEditing = False
            .Cols(COl_State).AllowEditing = False

                '.ForeColor = Color.Black
            .Redraw = True
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Quick Status Button :" & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub AddandSetFlexgridData(ByVal FlxGrid As C1.Win.C1FlexGrid.C1FlexGrid)
        Dim dtList As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try
        
            dtList = New DataTable
            Dim MedicationID As New DataColumn("MedicationID")
            MedicationID.DataType = GetType([String])
            Dim DrugName As New DataColumn("DrugName")
            DrugName.DataType = GetType([String])
            Dim NDCCode As New DataColumn("NDCCode")
            NDCCode.DataType = GetType([String])
            Dim STARTDATE As New DataColumn("Startdate")
            STARTDATE.DataType = GetType([DateTime])

            dtList.Columns.Add(MedicationID)
            dtList.Columns.Add(DrugName)
            dtList.Columns.Add(NDCCode)
            dtList.Columns.Add(STARTDATE)
            Dim dr As DataRow = Nothing

            For rNo As Integer = 1 To FlxGrid.Rows.Count - 1

                With c1MedicationList

                    dr = dtList.NewRow()
                    dr("MedicationID") = FlxGrid.GetData(rNo, COL_MEDICATIONID).ToString
                    dr("DrugName") = FlxGrid.GetData(rNo, COL_MEDICATION_NAME).ToString
                    dr("NDCCode") = FlxGrid.GetData(rNo, COL_NDCCode).ToString
                    dr("Startdate") = FlxGrid.GetData(rNo, COL_STARTDATE).ToString
                    dtList.Rows.Add(dr)

                    c1MedicationList.Rows.Add()
                    .SetData(rNo, COL_MEDICATIONID, FlxGrid.GetData(rNo, COL_MEDICATIONID))

                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_VISITID, FlxGrid.GetData(rNo, COL_VISITID))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_PATIENTID, FlxGrid.GetData(rNo, COL_PATIENTID))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_MEDICATION, FlxGrid.GetData(rNo, COL_MEDICATION)) 'changed in 6030 for Rx Feild Change
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_MEDICATION_NAME, FlxGrid.GetData(rNo, COL_MEDICATION_NAME))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_DOSAGE, FlxGrid.GetData(rNo, COL_DOSAGE))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_ROUTE, FlxGrid.GetData(rNo, COL_ROUTE))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_FREQUENCY, FlxGrid.GetData(rNo, COL_FREQUENCY))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_DURATION, FlxGrid.GetData(rNo, COL_DURATION))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_AMOUNT, FlxGrid.GetData(rNo, COL_AMOUNT)) 'dispense
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_MEDICATIONDATE, FlxGrid.GetData(rNo, COL_MEDICATIONDATE))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_STARTDATE, CType(FlxGrid.GetData(rNo, COL_STARTDATE), Date)) '''''bug 7079
                    c1MedicationList.Select(rNo, 0)

                    Dim _strEndDate As String = ""
                    Dim odt As DateTime = Now
                    Try
                        If (Date.TryParse(FlxGrid.GetData(rNo, COL_ENDDATE), odt) = False) Then
                            _strEndDate = ""
                        Else
                            _strEndDate = FlxGrid.GetData(rNo, COL_ENDDATE)
                        End If
                        If odt = Date.MinValue Then
                            _strEndDate = ""
                        End If
                    Catch ex As Exception
                        _strEndDate = ""
                    End Try

                    If _strEndDate = "" Then
                        .SetData(rNo, COL_ENDDATE, Nothing)
                    Else
                        .SetData(rNo, COL_ENDDATE, CType(_strEndDate, Date))
                    End If


                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_STATUS, FlxGrid.GetData(rNo, COL_STATUS))

                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_REASON, FlxGrid.GetData(rNo, COL_REASON))

                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_DDID, FlxGrid.GetData(rNo, COL_DDID))

                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_LOGINNAME, FlxGrid.GetData(rNo, COL_LOGINNAME))

                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_RENEWED, FlxGrid.GetData(rNo, COL_RENEWED))

                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_Narcotic, FlxGrid.GetData(rNo, COL_Narcotic))

                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_NDCCode, FlxGrid.GetData(rNo, COL_NDCCode))

                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_DRUGFORM, FlxGrid.GetData(rNo, COL_DRUGFORM))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_DRUGQTYQUALIFIER, FlxGrid.GetData(rNo, COL_DRUGQTYQUALIFIER))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_PBMSOURCENAME, FlxGrid.GetData(rNo, COL_PBMSOURCENAME))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_Rx_bMaySubstitute, FlxGrid.GetData(rNo, COL_Rx_bMaySubstitute))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_Rx_nDrugID, FlxGrid.GetData(rNo, COL_Rx_nDrugID))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COL_Rx_sName, FlxGrid.GetData(rNo, COL_Rx_sName))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_Rx_sNotes, FlxGrid.GetData(rNo, COL_Rx_sNotes))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_Rx_sPrescriberNotes, FlxGrid.GetData(rNo, COL_Rx_sPrescriberNotes))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_Rx_sMethod, FlxGrid.GetData(rNo, COL_Rx_sMethod))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_Rx_sRefills, FlxGrid.GetData(rNo, COL_Rx_sRefills))
                    c1MedicationList.Select(rNo, 0)

                    .SetData(rNo, COl_State, FlxGrid.GetData(rNo, COl_State))
                    c1MedicationList.Select(rNo, 0)
                    .SetData(rNo, COL_RowNumber, FlxGrid.GetData(rNo, COL_RowNumber))
                    c1MedicationList.Select(rNo, 0)


                    c1MedicationList.Select(rNo, 0)

                End With

            Next

            Dim view As DataView = dtList.DefaultView
            view.Sort = "DrugName,Startdate Desc"
            dt = view.ToTable()

            Dv = DeleteDuplicateFromDataTable(dt, "DrugName")
           
            If Not IsNothing(view) Then
                view.Dispose()
                view = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(dtList) Then
                dtList.Dispose()
                dtList = Nothing
            End If
        End Try

    End Sub

    Protected Function DeleteDuplicateFromDataTable(dtDuplicate As DataTable,
                                       columnName As String) As DataView
        Dim hashT As New Hashtable()
        Dim arrDuplicate As New ArrayList()
        For Each row As DataRow In dtDuplicate.Rows
            If hashT.Contains(row(columnName)) Then
                arrDuplicate.Add(row)
            Else
                hashT.Add(row(columnName), String.Empty)
            End If
        Next
        For Each row As DataRow In arrDuplicate
            dtDuplicate.Rows.Remove(row)
        Next

        Return dtDuplicate.DefaultView
    End Function
#Region "Button Clicks"

    Private Sub tlbbtn_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub tlbbtn_Accept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Accept.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
       
        Me.Close()
    End Sub
#End Region

#Region "Form_Closed"
    'Private Sub frmFinalizeReconcileList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    If IsNothing(Me) = False Then
    '        Me.Dispose()
    '    End If
    'End Sub
#End Region
End Class
