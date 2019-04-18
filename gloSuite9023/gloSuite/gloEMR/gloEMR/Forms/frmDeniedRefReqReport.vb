Public Class frmDeniedRefReqReport

    Dim nPatientId As Int64 = 0 'passed from Rx-Meds

    'C1 Flex Grid Columns name
    Private COL_COUNT As Integer = 10
    Private COL_PatientName As Integer = 0
    Private COL_MessageId As Integer = 1
    Private COL_Notes As Integer = 2
    Private COL_DenialReasonCode As Integer = 3
    Private COL_DenailReason As Integer = 4
    Private COL_PatientId As Integer = 5
    Private COL_ProviderId As Integer = 6
    Private COL_eRxStatus As Integer = 7
    Private COL_eRxStatusMessage As Integer = 8
    Private COL_DenyDate As Integer = 9


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
            .Cols(COL_MessageId).Width = 0
            .Cols(COL_Notes).Width = 0
            .Cols(COL_DenialReasonCode).Width = _Width * 2.7
            .Cols(COL_DenailReason).Width = _Width * 2.2
            .Cols(COL_PatientId).Width = 0
            .Cols(COL_ProviderId).Width = 0
            .Cols(COL_eRxStatus).Width = _Width * 1.5
            .Cols(COL_eRxStatusMessage).Width = 0
            .Cols(COL_DenyDate).Width = _Width * 1.5

            'set column header
            .SetData(0, COL_PatientName, "Patient Name")
            .SetData(0, COL_MessageId, "MessageId")
            .SetData(0, COL_Notes, "Notes")
            .SetData(0, COL_DenialReasonCode, "Denial Reason")
            .SetData(0, COL_DenailReason, "Denial Note")
            .SetData(0, COL_PatientId, "PatientId")
            .SetData(0, COL_ProviderId, "ProviderId")
            .SetData(0, COL_eRxStatus, "Message Status")
            .SetData(0, COL_eRxStatusMessage, "StatusMessage")
            .SetData(0, COL_eRxStatusMessage, "StatusMessage")
            .SetData(0, COL_DenyDate, "Date")
          
            'set visiblity for column 
            .Cols(COL_PatientName).Visible = True
            .Cols(COL_MessageId).Visible = True
            .Cols(COL_Notes).Visible = True
            .Cols(COL_DenialReasonCode).Visible = True
            .Cols(COL_DenailReason).Visible = True
            .Cols(COL_PatientId).Visible = True
            .Cols(COL_ProviderId).Visible = True
            .Cols(COL_eRxStatus).Visible = True
            .Cols(COL_eRxStatusMessage).Visible = True
            .Cols(COL_DenyDate).Visible = True
           
            'set column editing properties.
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_MessageId).AllowEditing = False
            .Cols(COL_Notes).AllowEditing = False
            .Cols(COL_DenialReasonCode).AllowEditing = False
            .Cols(COL_DenailReason).AllowEditing = False
            .Cols(COL_PatientId).AllowEditing = False
            .Cols(COL_ProviderId).AllowEditing = False
            .Cols(COL_eRxStatus).AllowEditing = False
            .Cols(COL_eRxStatusMessage).AllowEditing = False
            .Cols(COL_DenyDate).AllowEditing = False

            .ForeColor = Color.Black
        End With
    End Sub

    ''' <summary>
    ''' call from dashboard
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    ''' <summary>
    ''' pass the patient id if the form is opened from Rx_Meds
    ''' </summary>
    ''' <param name="PatientId"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PatientId As Int64)
        InitializeComponent()
        nPatientId = PatientId
    End Sub

    Private Sub frmDeniedRefReqReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddMenu()

        gloC1FlexStyle.Style(_Flex)
        Dim oDatatable As New DataTable
        Try
            SetFlexgridColumns() 'set the properties of flex grid

            If nPatientId <> 0 Then 'if form is opened from Rx-Meds then we pass the patient id to see the report against the selected patient
                'oDatatable = GetDeniedReportSelectedPatient(nPatientId)
                'changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
                oDatatable = GetDeniedRefillRequestList(nPatientId, dtpFrom.Text, dtpToDate.Text)
                Try
                    gloPatient.gloPatient.GetWindowTitle(Me, nPatientId, GetConnectionString(), gstrMessageBoxCaption)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
            Else 'the form is directly opened from dashboard to view report for all patients
                'oDatatable = GetDeniedReportAllPatient()
                'changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
                oDatatable = GetDeniedRefillRequestList(nPatientId, dtpFrom.Text, dtpToDate.Text)
            End If

            'bind the values of respective datatable for the flex grid
            BindData(oDatatable)
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

    Public Sub AddMenu()
        Dim tlstripitem As ToolStripMenuItem
        tlstripitem = New ToolStripMenuItem

        tlstripitem.Text = "Show Details"
        tlstripitem.Tag = 1
        tlstripitem.Image = ImgLstFlex.Images(0)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf StripItem_Click
        tlstripitem = Nothing
    End Sub

    Private Sub StripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'fetch the eRxStatus and eRxStatusMessage values from the selected grid row. 
            Dim eRxStatus As String = ""
            Dim eRxStatusMessage As String = ""

            If Not IsNothing(_Flex.GetData(_Flex.Row, COL_eRxStatusMessage)) Then
                eRxStatus = _Flex.GetData(_Flex.Row, COL_eRxStatusMessage)
            Else
                eRxStatus = ""
            End If

            If Not IsNothing(_Flex.GetData(_Flex.Row, COL_eRxStatusMessage)) Then
                eRxStatusMessage = _Flex.GetData(_Flex.Row, COL_eRxStatusMessage)
            Else
                eRxStatusMessage = ""
            End If

            If eRxStatusMessage <> "" Then
                MessageBox.Show(eRxStatusMessage, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf eRxStatus <> "" Then '''''show what was saved in eRxStatus field
                MessageBox.Show(eRxStatus, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
            End If
        Catch ex As Exception

        End Try

    End Sub

    'Get the Denial Codes 
    Private Function GetDenialReasonCode(ByVal denialreason As String) As String
        Try
            Select Case denialreason
                Case "AA"
                    Return "Patient Unknown to the Prescriber"
                Case "AB"
                    Return "Patient never under Prescriber care"
                Case "AC"
                    Return "Patient no longer under Prescriber care"
                Case "AD"
                    Return "Patient has requested refill too soon"
                Case "AE"
                    Return "Medication never prescribed for the patient"
                Case "AF"
                    Return "Patient should contact Prescriber first"
                Case "AG"
                    Return "Refill not appropriate"
                Case "AH"
                    Return "Patient has picked up prescription"
                Case "AK"
                    Return "Patient has picked up partial fill of prescription"
                Case "AL"
                    Return "Patient has not picked up prescription, drug returned to stock"
                Case "AM"
                    Return "Patient needs appointment"
                Case "AN"
                    Return "Prescriber not associated with this practice or location."
                Case "AO"
                    Return "No attempt will be made to obtain Prior Authorization."
                Case "AP"
                    Return "Request already responded to by other means (e.g. phone or fax)"
            End Select
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' fetch denied report for all patients. called from Rx-Meds
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeniedReportAllPatient(Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim strSQL As String = ""
        Dim oDt As New DataTable
        Try
            If FromDate <> "" And ToDate <> "" Then
                strSQL = "SELECT     ResponseTransaction.*, Patient.sFirstName, Patient.sLastName, SureScriptMessageTransaction.sDateTimeStamp, SureScriptMessageTransaction.dtDateReceived " _
                        & " FROM         ResponseTransaction LEFT OUTER JOIN " _
                        & " Patient ON ResponseTransaction.nPatientID = Patient.nPatientID AND ResponseTransaction.sType = 'DN' INNER JOIN " _
                        & " SureScriptMessageTransaction ON ResponseTransaction.nMessageID = SureScriptMessageTransaction.nMessageID " _
                        & " where convert(datetime,convert(varchar,SureScriptMessageTransaction.dtDateReceived,101)) between '" & dtpFrom.Text & "' and '" & dtpToDate.Text & "'" _
                        & " and ISNULL(sMessageName, '') = ''"
            Else
                strSQL = "SELECT     ResponseTransaction.*, Patient.sFirstName, Patient.sLastName ,SureScriptMessageTransaction.sDateTimeStamp, SureScriptMessageTransaction.dtDateReceived  FROM  ResponseTransaction LEFT OUTER JOIN " _
                        & " Patient ON ResponseTransaction.nPatientID = Patient.nPatientID AND ResponseTransaction.sType = 'DN' INNER JOIN " _
                        & " SureScriptMessageTransaction ON ResponseTransaction.nMessageID = SureScriptMessageTransaction.nMessageID" _
                        & " and ISNULL(sMessageName, '') = ''"
            End If
            oDB.Connect(GetConnectionString)
            oDt = oDB.ReadQueryDataTable(strSQL)
            Return oDt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDB.Disconnect()
            oDB = Nothing
            oDt = Nothing
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB = Nothing
            oDt = Nothing
        End Try
    End Function


    Public Function GetDeniedRefillRequestList(Optional ByVal nPatientId As Long = 0, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As DataTable
        Me.Cursor = Cursors.WaitCursor
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim oDt As New DataTable()
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@FromDate", FromDate, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@ToDate", ToDate, ParameterDirection.Input, SqlDbType.Text)
            oDB.Retrive("GetDeniedRefillRequestList", oParam, oDt)
            Return oDt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            If oParam IsNot Nothing Then
                oParam.Dispose() : oParam = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect() : oDB = Nothing
            End If

            If oDt IsNot Nothing Then
                oDt.Dispose() : oDt = Nothing
            End If

            Me.Cursor = Cursors.Default
        End Try

    End Function


    ''' <summary>
    ''' fetch denied report for selected patient. called from dashboard
    ''' </summary>
    ''' <param name="PatientId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDeniedReportSelectedPatient(ByVal PatientId As Int64, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim strSQL As String = ""
        Dim oDt As New DataTable
        Try
            If FromDate <> "" And ToDate <> "" Then
                strSQL = "SELECT     ResponseTransaction.*,   Patient.sFirstName, Patient.sLastName, SureScriptMessageTransaction.sDateTimeStamp, SureScriptMessageTransaction.dtDateReceived " _
                        & " FROM         ResponseTransaction INNER JOIN " _
                        & " Patient ON ResponseTransaction.nPatientID = Patient.nPatientID AND ResponseTransaction.sType = 'DN' INNER JOIN " _
                        & " SureScriptMessageTransaction ON ResponseTransaction.nMessageID = SureScriptMessageTransaction.nMessageID " _
                        & " where convert(datetime,convert(varchar,SureScriptMessageTransaction.dtDateReceived,101)) between '" & FromDate & "' and '" & ToDate & "' and ResponseTransaction.nPatientID = " & PatientId _
                        & " and ISNULL(sMessageName, '') = ''"

            Else
                strSQL = "SELECT     ResponseTransaction.*, Patient.sFirstName, Patient.sLastName ,SureScriptMessageTransaction.sDateTimeStamp, SureScriptMessageTransaction.dtDateReceived  FROM  ResponseTransaction INNER JOIN " _
                        & " Patient ON ResponseTransaction.nPatientID = Patient.nPatientID AND ResponseTransaction.sType = 'DN' " _
                        & " INNER JOIN  SureScriptMessageTransaction ON ResponseTransaction.nMessageID = SureScriptMessageTransaction.nMessageID" _
                        & " where ResponseTransaction.nPatientID = " & PatientId _
                        & " and ISNULL(sMessageName, '') = ''"
            End If

            oDB.Connect(GetConnectionString)
            oDt = oDB.ReadQueryDataTable(strSQL)
            Return oDt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDB.Disconnect()
            oDB = Nothing
            oDt = Nothing
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB = Nothing
            oDt = Nothing
        End Try
    End Function

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub dtpFrom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFrom.TextChanged
        Dim oDatatable As New DataTable
        Try
            'SetFlexgridColumns() 'set the properties of flex grid

            If nPatientId <> 0 Then 'if form is opened from Rx-Meds then we pass the patient id to see the report against the selected patient
                'oDatatable = GetDeniedReportSelectedPatient(nPatientId, dtpFrom.Text, dtpToDate.Text)
                'changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
                oDatatable = GetDeniedRefillRequestList(nPatientId, dtpFrom.Text, dtpToDate.Text)

            Else 'the form is directly opened from dashboard to view report for all patients
                'oDatatable = GetDeniedReportAllPatient(dtpFrom.Text, dtpToDate.Text)
                'changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
                oDatatable = GetDeniedRefillRequestList(0, dtpFrom.Text, dtpToDate.Text)
            End If

            'bind the values of respective datatable for the flex grid
            BindData(oDatatable)
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

    Private Sub dtptoDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        Dim oDatatable As New DataTable
        Try
            If nPatientId <> 0 Then 'if form is opened from Rx-Meds then we pass the patient id to see the report against the selected patient
                'oDatatable = GetDeniedReportSelectedPatient(nPatientId, dtpFrom.Text, dtpToDate.Text)
                'changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
                oDatatable = GetDeniedRefillRequestList(nPatientId, dtpFrom.Text, dtpToDate.Text)

            Else 'the form is directly opened from dashboard to view report for all patients
                'oDatatable = GetDeniedReportAllPatient(dtpFrom.Text, dtpToDate.Text)
                'changes done to resolve Bug #84603: 00000931 : Patient Name not displayed in Denied refill request report
                oDatatable = GetDeniedRefillRequestList(0, dtpFrom.Text, dtpToDate.Text)
            End If

            'bind the values of respective datatable for the flex grid
            BindData(oDatatable)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

    Private Sub BindData(ByVal oDatatable As DataTable)
        Try
            If Not IsNothing(oDatatable) Then
                If oDatatable.Rows.Count > 0 Then
                    _Flex.Rows.Count = 1
                    For i As Integer = 0 To oDatatable.Rows.Count - 1
                        _Flex.Rows.Add() 'add new row

                        _Flex.SetData(i + 1, COL_PatientName, oDatatable.Rows(i)("PatientName"))
                        _Flex.SetData(i + 1, COL_MessageId, oDatatable.Rows(i)("nMessageID"))
                        _Flex.SetData(i + 1, COL_Notes, oDatatable.Rows(i)("sNotes"))

                        'show the denial reason instead of code
                        Dim _DenialReason As String = GetDenialReasonCode(oDatatable.Rows(i)("sDenialReasoncode"))
                        _Flex.SetData(i + 1, COL_DenialReasonCode, _DenialReason)

                        _Flex.SetData(i + 1, COL_DenailReason, oDatatable.Rows(i)("sNotes"))
                        _Flex.SetData(i + 1, COL_PatientId, oDatatable.Rows(i)("nPatientID"))
                        _Flex.SetData(i + 1, COL_ProviderId, oDatatable.Rows(i)("nProviderId"))
                        _Flex.SetData(i + 1, COL_eRxStatus, oDatatable.Rows(i)("eRxStatus"))
                        _Flex.SetData(i + 1, COL_eRxStatusMessage, oDatatable.Rows(i)("eRxStatusMessage"))
                        _Flex.SetData(i + 1, COL_DenyDate, oDatatable.Rows(i)("dtDateReceived"))
                    Next
                Else
                    _Flex.Rows.Count = 1
                End If
            Else
                _Flex.Rows.Count = 1
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

    Private Sub _Flex_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
            Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
            If r <= 0 Then
                Exit Sub
            Else
                _Flex.Row = r
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

End Class