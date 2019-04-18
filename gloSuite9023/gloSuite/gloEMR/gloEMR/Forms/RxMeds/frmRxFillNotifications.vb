Imports gloEMRGeneralLibrary

Public Class frmRxFillNotifications

    Private COL_COUNT As Integer = 6

    Private COL_PatientName As Integer = 0
    Private COL_Drug As Integer = 1
    Private COL_FillStatus As Integer = 2
    Private COL_ReceivedDate As Integer = 3
    Private COL_MessageID As Integer = 4
    Private COL_PrescriptionID As Integer = 5

    Private nPatientID As Long = 0

    Public Sub New()
        InitializeComponent()

        Me.dtpFrom.Value = Date.Now.AddDays(-7)
        Me.dtpToDate.Value = Date.Now
    End Sub

    Public Sub New(ByVal _PatientID As Long)
        InitializeComponent()

        Me.dtpFrom.Value = Date.Now.AddDays(-7)
        Me.dtpToDate.Value = Date.Now

        Me.nPatientID = _PatientID
    End Sub

    Private Sub frmViewedRxFill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub dtpFrom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFrom.TextChanged
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtptoDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetFlexgridColumns()
        With _Flex
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            .Cols(0).Width = 30
            .ExtendLastCol = True
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width / 10

            .Cols(COL_PatientName).Width = _Width * 1.7
            .Cols(COL_Drug).Width = _Width * 1.7
            .Cols(COL_FillStatus).Width = _Width * 1.7
            .Cols(COL_ReceivedDate).Width = _Width * 2.7
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_PrescriptionID).Width = 0

            'set column header
            .SetData(0, COL_PatientName, "Patient Name")
            .SetData(0, COL_Drug, "Medication Prescribed")
            .SetData(0, COL_FillStatus, "Fill Status")
            .SetData(0, COL_ReceivedDate, "Received Date")
            .SetData(0, COL_MessageID, "MessageID")
            .SetData(0, COL_PrescriptionID, "PrescriptionID")

            'set visiblity for column 
            .Cols(COL_PatientName).Visible = True
            .Cols(COL_Drug).Visible = True
            .Cols(COL_FillStatus).Visible = True
            .Cols(COL_ReceivedDate).Visible = True
            .Cols(COL_MessageID).Visible = False
            .Cols(COL_PrescriptionID).Visible = False

            'set column editing properties.
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_Drug).AllowEditing = False
            .Cols(COL_FillStatus).AllowEditing = False
            .Cols(COL_ReceivedDate).AllowEditing = False
            .Cols(COL_MessageID).AllowEditing = False
            .Cols(COL_PrescriptionID).AllowEditing = False

        End With
    End Sub

    Private Sub frmViewedRxFill_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        gloC1FlexStyle.Style(_Flex)

        Try
            Me.RefreshData()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    Private Sub RefreshData()
        Dim dtRequests As New DataTable()
        Try
            SetFlexgridColumns()
            Using p As New PrescriptionBusinessLayer()
                If Me.nPatientID <> 0 Then
                    dtRequests = p.GetAllRxChangeRequests(dtpFrom.Value, dtpToDate.Value, nPatientID)
                Else
                    dtRequests = p.GetAllRxChangeRequests(dtpFrom.Value, dtpToDate.Value)
                End If
            End Using

            BindData(dtRequests)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub BindData(ByVal oDatatable As DataTable)
        Try
            If oDatatable IsNot Nothing AndAlso oDatatable.Rows.Count > 0 Then
                RemoveHandler _Flex.SelChange, AddressOf _Flex_CellButtonClick
                _Flex.Rows.Count = 1
                For i As Integer = 0 To oDatatable.Rows.Count - 1
                    _Flex.Rows.Add()

                    _Flex.SetData(i + 1, COL_PatientName, oDatatable.Rows(i)("sPatientName"))
                    _Flex.SetData(i + 1, COL_Drug, oDatatable.Rows(i)("sDrugName"))

                    If (Convert.ToString(oDatatable.Rows(i)("sFillStatus")) = "Partial Fill") Then
                        _Flex.SetData(i + 1, COL_FillStatus, "Partial filled")
                        _Flex.SetCellImage(i + 1, COL_FillStatus, My.Resources.Rx_PartiallyFilled)
                    ElseIf (Convert.ToString(oDatatable.Rows(i)("sFillStatus")) = "Filled") Then
                        _Flex.SetData(i + 1, COL_FillStatus, "Filled")
                        _Flex.SetCellImage(i + 1, COL_FillStatus, My.Resources.Rx_Filled)
                    ElseIf (Convert.ToString(oDatatable.Rows(i)("sFillStatus")) = "Not Filled") Then
                        _Flex.SetData(i + 1, COL_FillStatus, "Not filled")
                        _Flex.SetCellImage(i + 1, COL_FillStatus, My.Resources.Rx_NotFilled)
                    End If

                    _Flex.SetData(i + 1, COL_ReceivedDate, oDatatable.Rows(i)("dtDateReceived"))
                    _Flex.SetData(i + 1, COL_MessageID, oDatatable.Rows(i)("nMessageID"))
                    _Flex.SetData(i + 1, COL_PrescriptionID, oDatatable.Rows(i)("sPrescriberOrderNumber"))

                Next
                AddHandler _Flex.SelChange, AddressOf _Flex_CellButtonClick                
                _Flex_CellButtonClick(Me, New EventArgs())
            Else
                _Flex.Rows.Count = 1
                Me.gloRxRequests.SetC1FlexGrid()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

    Private Sub _Flex_CellButtonClick(sender As System.Object, e As System.EventArgs) Handles _Flex.SelChange
        Dim nPrescriptionID As Int64 = 0

        Try
            If _Flex.RowSel > 0 Then

                Dim sMessageID As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_MessageID))
                Dim sPrescriptionID As String = Convert.ToString(_Flex.GetData(_Flex.RowSel, COL_PrescriptionID))

                If gloRxRequests IsNot Nothing Then

                    If Int64.TryParse(sPrescriptionID, nPrescriptionID) AndAlso nPrescriptionID <> 0 Then
                        gloRxRequests.PrescriberOrderNumber = nPrescriptionID
                        gloRxRequests.MessageID = Nothing
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.View, "RxFill notification viewed", nPatientID, nPrescriptionID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
                    Else
                        gloRxRequests.PrescriberOrderNumber = 0
                        gloRxRequests.MessageID = sMessageID
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.View, "RxFill notification viewed for messageID " + sMessageID, nPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
                    End If

                    gloRxRequests.RefreshMessages()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlbbtnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnRefresh.Click
        Try
            gloRxRequests.RefreshMessages()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class