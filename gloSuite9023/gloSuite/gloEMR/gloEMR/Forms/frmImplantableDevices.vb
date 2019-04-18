Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient
Imports gloAuditTrail
Imports System.Text.RegularExpressions
Public Class frmImplantableDevices

#Region "Variable declaration"

    Inherits frmBaseForm

    Dim _PatientID As Long
    Dim _TransactionID As Long
    Dim _isLoadGridCvxControl As Boolean = False
    Dim isSaved As Boolean = False    
    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private IsFormLocked As Boolean = False
    Private LockID As Int64 = 0
    Dim nVisitID As Int64
    Private sStatusReason As String = ""
    'Dim sDeviceDescription As String
    'Dim sGMDNPTName As String

#End Region

#Region "Constructor"

    Public Sub New(ByVal TransactionID As Long, ByVal PatientID As Long, Optional ByVal isRecordLock As Boolean = False)
        MyBase.New()
        InitializeComponent()
        _PatientID = PatientID
        _TransactionID = TransactionID
        IsFormLocked = isRecordLock        
    End Sub
   
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

#End Region

   

#Region "Form Event"

    Private Sub frmImTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim scheme As gloBilling.Cls_TabIndexSettings.TabScheme = gloBilling.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New gloBilling.Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing
        
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "Implantable Device Transaction Opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

            FillControl()
            ShowRequiredLable()
        If IsFormLocked Then
            tblbtn_Save.Enabled = False
        End If

        If _TransactionID > 0 Then
            
            ShowSelectedImmunization()
        Else
            dttransaction_date.Checked = False
            dttransaction_date.Value = Now.Date
            dtTransactionTime.Checked = False
            txt_UDI.Text = ""
            txt_BrandName.Text = ""
            txt_CompanyName.Text = ""
            txt_DI.Text = ""
            txt_ExpiryDate.Text = ""
            txt_HCTP.Text = ""
            txt_IssuingAgency.Text = ""
            txt_LotBatch.Text = ""
            txt_ManufacturingDate.Text = ""
            txt_MRIstatus.Text = ""
            txt_NRL.Text = ""
            txt_SerialNumber.Text = ""
            txt_VersionOrModel.Text = ""
            optActive.Checked = True
            optInactive.Checked = False
        End If

        _isLoaded = True
    End Sub

    Private Sub frmImTransaction_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If IsFormLocked = False Then
            UnLock_Transaction(TrnType.ImplantDevices, _PatientID, _TransactionID, dttransaction_date.Value)
        End If

        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Close, "Implantable Device Transaction Closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

    End Sub

#End Region

#Region "Button Click"

    Private Sub tblbtn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Save.Click


        Try
            Me.Cursor = Cursors.WaitCursor

            If lblProviderName.Visible = True Then
                If cmbProvider.Text.Trim = "" Then                    
                    MessageBox.Show("Select Provider.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbProvider.Select()
                    Exit Sub
                End If
            End If

            If txt_UDI.Text.Trim = "" Then                
                MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txt_UDI.Select()
                Exit Sub
            End If

            If lblDeviceID.Visible = True Then
                If txt_DI.Text.Trim = "" Then                    
                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_DI.Select()
                    Exit Sub
                End If
            End If

            If lblIssuingAgency.Visible = True Then
                If txt_IssuingAgency.Text.Trim = "" Then

                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_IssuingAgency.Select()
                    Exit Sub
                End If
            End If

            If lblBrandName.Visible = True Then
                If txt_BrandName.Text.Trim = "" Then
                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_BrandName.Select()
                    Exit Sub
                End If
            End If

            If lblManufacturer.Visible = True Then
                If txt_CompanyName.Text.Trim = "" Then
                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_CompanyName.Select()
                    Exit Sub
                End If
            End If

            If lblVersionOrModel.Visible = True Then
                If txt_VersionOrModel.Text.Trim = "" Then
                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_VersionOrModel.Select()
                    Exit Sub
                End If
            End If

            If lblNRL.Visible = True Then
                If txt_NRL.Text.Trim = "" Then
                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_NRL.Select()
                    Exit Sub
                End If
            End If

            If lblMRI.Visible = True Then
                If txt_MRIstatus.Text.Trim = "" Then
                    MessageBox.Show("Enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_MRIstatus.Select()
                    Exit Sub
                End If
            End If

            Dim LongDate As Regex = New Regex("^(0[1-9]|1[0-2])([/+-])(0[1-9]|1[0-9]|2[0-9]|3[0,1])([/+-])(19|20)[0-9]{2}$")

            If txt_ExpiryDate.Text.Trim <> "" Then
                If Not LongDate.Match(txt_ExpiryDate.Text.Trim).Success Then
                    MessageBox.Show("Enter valid date or enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_ExpiryDate.Select()
                    Exit Sub                
                End If
            End If
            If txt_ManufacturingDate.Text.Trim <> "" Then
                If Not LongDate.Match(txt_ManufacturingDate.Text.Trim).Success Then
                    MessageBox.Show("Enter valid date or enter UDI and parse using Parse button.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_ManufacturingDate.Select()
                    Exit Sub                
                End If
            End If

            Dim strNotes = ""
            If optInactive.Checked AndAlso sStatusReason = "" Then
                Dim frm As New frmAddNotes()
                frm.Text = "Notes"
                frm.ShowInTaskbar = False
                frm.StartPosition = FormStartPosition.CenterParent
                frm._LabelCaption = "Reason for Inactive"
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                strNotes = frm._Notes
                frm.Dispose()
                frm = Nothing

                'If MessageBox.Show("Do you want to delete the selected Implantable Device record?   ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If strNotes = "" Then
                    Exit Sub
                End If
            Else
                strNotes = sStatusReason
            End If
            Dim sTransactiondatetime As String = ""
            Dim clsIMTran As New clsgloImplantableDevicesTransaction
            With clsIMTran
                .PatientID = _PatientID
                .transaction_id = _TransactionID
                If dttransaction_date.Checked = True Then
                    If dtTransactionTime.Checked = True Then
                        sTransactiondatetime = dttransaction_date.Value.Date
                        sTransactiondatetime = sTransactiondatetime + " " + dtTransactionTime.Value.TimeOfDay.ToString()
                        .transaction_date = Convert.ToDateTime(sTransactiondatetime)
                    Else
                        .transaction_date = dttransaction_date.Value.Date

                    End If
                Else
                    .transaction_date = Nothing
                End If
                .ProviderID = cmbProvider.SelectedValue
                .UDI = txt_UDI.Text
                .DeviceID = txt_DI.Text
                .IssuingAgency = txt_IssuingAgency.Text
                .VersionOrModel = txt_VersionOrModel.Text
                .BrandName = txt_BrandName.Text
                .LabeledContainingNRL = txt_NRL.Text
                .manufacturer = txt_CompanyName.Text
                .MRIStatus = txt_MRIstatus.Text
                .LotBatchNumber = txt_LotBatch.Text
                .SerialNumber = txt_SerialNumber.Text
                .expiration_date = txt_ExpiryDate.Text
                .Manufacturing_date = txt_ManufacturingDate.Text
                .HCTP_Code = txt_HCTP.Text

                If optActive.Checked Then
                    .DeviceActive = True
                Else
                    .DeviceActive = False
                End If
                ._strStatusNotes = strNotes
                .VisitID = nVisitID
                Dim splconceptid As String() = lblconcptid.Text.Trim().Split("-")
                If (splconceptid.Length > 1) Then
                    .ConceptID = splconceptid(0).Trim
                    .SMDescription = splconceptid(1).Trim
                Else
                    .ConceptID = lblconcptid.Text.Trim
                    .SMDescription = ""
                End If
                .DeviceDescription = txtDeviceDescription.Text
                .GmdnPTName = txtGmdnPTName.Text
                .DtProcedure = New DataTable
                Dim sampleColumn As DataColumn = New DataColumn("CPTCode", GetType(String))
                Dim sampleColumn1 As DataColumn = New DataColumn("sDescription", GetType(String))
                .DtProcedure.Columns.Add(sampleColumn)
                .DtProcedure.Columns.Add(sampleColumn1)
                If lstProcedures.Items.Count > 0 Then

                    For i As Integer = 0 To lstProcedures.Items.Count - 1

                        Dim sprocedures As String() = lstProcedures.Items(i).ToString.Split(":")
                        .DtProcedure.Rows.Add()
                        .DtProcedure.Rows(i)(0) = sprocedures(0).Trim
                        .DtProcedure.Rows(i)(1) = sprocedures(1).Trim
                    Next
                    .DtProcedure.AcceptChanges()
                End If
                Dim StrAudittrailString As String = ""

                If (_TransactionID <= 0) Then
                    StrAudittrailString = "Implantable Device Record Added."
                Else
                    StrAudittrailString = "Implantable Device Record Modified."
                End If

                Dim TranctionID As Int64 = -1
                TranctionID = .AddIMTransaction()

                If _TransactionID <= 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, StrAudittrailString, _PatientID, TranctionID, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, StrAudittrailString, _PatientID, _TransactionID, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                End If
                If (TranctionID > 0) Then
                    _TransactionID = TranctionID
                End If

                isSaved = True

                If IsNothing(sender) = False Then
                    _isSaveClicked = True
                    _isClose = True
                    Me.Close()
                End If

            End With
            clsIMTran = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Implantable Device Record Error.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click
        Me.Close()
    End Sub

#End Region


#Region "MouseHover Event"

    Private Sub btnParseUDI_Click(sender As Object, e As System.EventArgs) Handles btnParseUDI.Click
        'https://accessgudid.nlm.nih.gov/api/v1/parse_udi.json?udi=(01)00011954013448(11)141231(17)150707(10)A213B1(21)1234
        '=/A9999XYZ100T0944=,000025=A99971312345600=>014032=}013032&,1000000000000XYZ123

        If txt_UDI.Text.Trim <> "" Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim APIRESPONSE As APIResponse
                Dim parseudi As New Parse_UDI
                parseudi.UDI = Uri.EscapeDataString(txt_UDI.Text.Trim)
                APIRESPONSE = parseudi.UDI_Parser()                
                If Not APIRESPONSE Is Nothing Then
                    parseudi.UDI = APIRESPONSE.di
                    Dim slookup As String() = parseudi.UDI_Lookup().Split("|")
                    If slookup.Length > 0 Then
                        txtDeviceDescription.Text = slookup(0)
                        txtGmdnPTName.Text = slookup(1)
                    Else
                        txtDeviceDescription.Text = ""
                        txtGmdnPTName.Text = ""
                    End If
                    If Not (APIRESPONSE.di) Is Nothing Then
                        txt_DI.Text = APIRESPONSE.di
                    Else
                        txt_DI.Text = ""
                    End If
                    If Not (APIRESPONSE.issuing_agency) Is Nothing Then
                        txt_IssuingAgency.Text = APIRESPONSE.issuing_agency
                    Else
                        txt_IssuingAgency.Text = ""
                    End If
                    If Not (APIRESPONSE.lot_number) Is Nothing Then
                        txt_LotBatch.Text = APIRESPONSE.lot_number
                    Else
                        txt_LotBatch.Text = ""
                    End If
                    If Not (APIRESPONSE.serial_number) Is Nothing Then
                        txt_SerialNumber.Text = APIRESPONSE.serial_number
                    Else
                        txt_SerialNumber.Text = ""
                    End If
                    If Not (APIRESPONSE.expiration_date) Is Nothing Then
                        'txt_ExpiryDate.Text = APIRESPONSE.expiration_date
                        txt_ExpiryDate.Text = Convert.ToDateTime(APIRESPONSE.expiration_date).ToString("MM/dd/yyyy")
                    Else
                        txt_ExpiryDate.Text = ""
                    End If
                    If Not (APIRESPONSE.manufacturing_date) Is Nothing Then
                        txt_ManufacturingDate.Text = Convert.ToDateTime(APIRESPONSE.manufacturing_date).ToString("MM/dd/yyyy")
                    Else
                        txt_ManufacturingDate.Text = ""
                    End If
                    If Not (APIRESPONSE.donation_id) Is Nothing Then
                        txt_HCTP.Text = APIRESPONSE.donation_id
                    Else
                        txt_HCTP.Text = ""
                    End If

                End If
                If txt_DI.Text.Trim <> "" Then
                    Dim dt As DataTable
                    Dim clsIMTran As New clsgloImplantableDevicesTransaction
                    Try
                        With clsIMTran
                            dt = .ShowDeviceInformation(txt_DI.Text.Trim)
                        End With
                        If dt.Rows.Count > 0 Then
                            txt_BrandName.Text = dt.Rows(0)("Brand_Name")
                            txt_CompanyName.Text = dt.Rows(0)("COMPANY_NAME")
                            txt_VersionOrModel.Text = dt.Rows(0)("VERSION_MODEL_NUMBER")
                            txt_MRIstatus.Text = dt.Rows(0)("MRI_SAFETY_STATUS")
                            If Not IsDBNull(dt.Rows(0)("LABELED_CONTAINS_NRL")) Then
                                If Not IsDBNull(dt.Rows(0)("LABELED_CONTAINS_NRL")) Then
                                    If dt.Rows(0)("LABELED_CONTAINS_NRL") Then
                                        txt_NRL.Text = "Yes"
                                    Else
                                        txt_NRL.Text = "No"
                                    End If
                                Else
                                    txt_NRL.Text = "No"
                                End If
                            End If
                        End If
                        If Not dt Is Nothing Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                    Catch ex As Exception
                    Finally
                        clsIMTran = Nothing
                    End Try
                End If
            Catch ex As Exception

            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub btnParseUDI_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParseUDI.MouseHover
        ToolTip1.SetToolTip(btnParseUDI, "Parse UDI")
    End Sub

#End Region


#Region "SubProcedure"
    Private Sub ShowRequiredLable()

        lblUDI.Visible = True
        lblDeviceID.Visible = True
        lblProviderName.Visible = True
        lblIssuingAgency.Visible = True
        lblBrandName.Visible = True
        lblManufacturer.Visible = True
        lblMRI.Visible = True
        lblVersionOrModel.Visible = True
        lblNRL.Visible = True


    End Sub
    Private Sub FillControl()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dsIM As DataSet = Nothing

        Try
            Me.Cursor = Cursors.WaitCursor

            'selecting records of User, provider, sku, route, site refusal reason, funding list from below sp
            oDB.Connect(False)
            oDB.Retrive("gsp_ImplantableDeviceFillControl", dsIM)
            oDB.Disconnect()

            If (IsNothing(dsIM) = False) Then

                'Get Provider List
                cmbProvider.DataSource = dsIM.Tables(0)
                cmbProvider.ValueMember = "nProviderID"
                cmbProvider.DisplayMember = "Provider"
                cmbProvider.SelectedValue = gnPatientProviderID

            End If

        Catch ex As Exception
            MessageBox.Show("Error on Patient implantable device." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Private Sub ShowSelectedImmunization()

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing
        Dim sTransactionDateTime As String()
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@TransactioID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_ShowSelectedPatientImplantableDevice", oParam, dtIM)
            oDB.Disconnect()
            If (IsNothing(dtIM) = False) Then

                If dtIM.Rows.Count > 0 Then

                    If dtIM.Rows(0)("TransactionDate") IsNot Nothing Then
                        If IsDBNull(dtIM.Rows(0)("TransactionDate")) Then
                            dttransaction_date.Checked = False
                            dtTransactionTime.Checked = False
                        Else
                            sTransactionDateTime = dtIM.Rows(0)("TransactionDate").ToString().Split(New Char() {" "c})
                            If sTransactionDateTime(1).ToString() = "12:00:00" Then
                                dttransaction_date.Value = dtIM.Rows(0)("TransactionDate")
                                dtTransactionTime.Checked = False
                                dttransaction_date.Checked = True
                            Else
                                dtTransactionTime.Checked = True
                                dttransaction_date.Checked = True
                                dttransaction_date.Value = dtIM.Rows(0)("TransactionDate")
                                dtTransactionTime.Value = dtIM.Rows(0)("TransactionDate")
                            End If
                        End If
                    Else
                        dttransaction_date.Checked = False
                        dtTransactionTime.Checked = False
                    End If

                        If Not IsDBNull(dtIM.Rows(0)("DeviceStatus")) Then
                            If dtIM.Rows(0)("DeviceStatus") = "0" Then
                                optInactive.Checked = True
                            ElseIf dtIM.Rows(0)("DeviceStatus") = "1" Then
                                optActive.Checked = True
                            End If
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("DeviceID")) Then
                            txt_DI.Text = dtIM.Rows(0)("DeviceID")
                        End If

                        cmbProvider.SelectedValue = dtIM.Rows(0)("nProviderID")

                        If String.IsNullOrEmpty(Convert.ToString(cmbProvider.SelectedValue)) Then
                            Dim sSQL As String = "SELECT nProviderID, sFirstName + ' ' + sMiddleName + ' ' + sLastName AS Provider FROM Provider_mst WHERE nProviderID = " & dtIM.Rows(0)("nProviderID")

                            oDB.Connect(False)

                            Dim dtProviderDetails As DataTable = Nothing

                            oDB.Retrive_Query(sSQL, dtProviderDetails)

                            oDB.Disconnect()
                            If (IsNothing(dtProviderDetails) = False) Then


                                If dtProviderDetails.Rows.Count > 0 Then
                                    Dim dtProviders As DataTable = cmbProvider.DataSource
                                    Dim drRow As DataRow = dtProviders.NewRow()

                                    drRow("nProviderID") = dtProviderDetails.Rows(0)("nProviderID")
                                    drRow("Provider") = dtProviderDetails.Rows(0)("Provider")

                                    dtProviders.Rows.Add(drRow)

                                    cmbProvider.DataSource = dtProviders
                                    cmbProvider.ValueMember = "nProviderID"
                                    cmbProvider.DisplayMember = "Provider"
                                    cmbProvider.SelectedValue = dtIM.Rows(0)("nProviderID")

                                    drRow = Nothing
                                End If
                                dtProviderDetails.Dispose()
                                dtProviderDetails = Nothing
                            End If
                            sSQL = Nothing

                        End If
                        Dim dtProc As DataTable = Nothing
                        Dim clsIM As New clsgloImplantableDevicesTransaction
                        dtProc = clsIM.get_Procedures(_TransactionID, _PatientID)
                        For i As Integer = 0 To dtProc.Rows.Count - 1
                            lstProcedures.Items.Add(dtProc.Rows(i)(0))
                        Next

                        If Not IsDBNull(dtIM.Rows(0)("IssuingAgency")) Then
                            txt_IssuingAgency.Text = dtIM.Rows(0)("IssuingAgency")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("BrandName")) Then
                            txt_BrandName.Text = dtIM.Rows(0)("BrandName")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("CompanyName")) Then
                            txt_CompanyName.Text = dtIM.Rows(0)("CompanyName")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("Version_Model")) Then
                            txt_VersionOrModel.Text = dtIM.Rows(0)("Version_Model")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("MRIStatus")) Then
                            txt_MRIstatus.Text = dtIM.Rows(0)("MRIStatus")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("IsLabelContainedNRL")) Then
                            txt_NRL.Text = dtIM.Rows(0)("IsLabelContainedNRL")
                        Else
                            txt_NRL.Text = "No"
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("ExpiryDate")) AndAlso dtIM.Rows(0)("ExpiryDate") <> "" Then

                            txt_ExpiryDate.Text = Convert.ToDateTime(dtIM.Rows(0)("ExpiryDate")).ToString("MM/dd/yyyy")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("ManufacturingDate")) AndAlso dtIM.Rows(0)("ManufacturingDate") <> "" Then
                            txt_ManufacturingDate.Text = Convert.ToDateTime(dtIM.Rows(0)("ManufacturingDate")).ToString("MM/dd/yyyy")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("SerialNumber")) Then
                            txt_SerialNumber.Text = dtIM.Rows(0)("SerialNumber")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("Lot_Batch")) Then
                            txt_LotBatch.Text = dtIM.Rows(0)("Lot_Batch")
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("HCTPCode")) Then
                            txt_HCTP.Text = Convert.ToString(dtIM.Rows(0)("HCTPCode"))
                        End If

                        If Not IsDBNull(dtIM.Rows(0)("UDI")) Then
                            txt_UDI.Text = dtIM.Rows(0)("UDI")
                        End If
                        If Not IsDBNull(dtIM.Rows(0)("SNOMED")) Then
                            lblconcptid.Text = dtIM.Rows(0)("SNOMED")
                        End If
                        If Not IsDBNull(dtIM.Rows(0)("DeviceDescription")) Then
                            txtDeviceDescription.Text = dtIM.Rows(0)("DeviceDescription")
                        End If
                        If Not IsDBNull(dtIM.Rows(0)("GmdnPTName")) Then
                            txtGmdnPTName.Text = dtIM.Rows(0)("GmdnPTName")
                        End If
                        If Not IsDBNull(dtIM.Rows(0)("sReasonforDelete")) Then
                            sStatusReason = dtIM.Rows(0)("sReasonforDelete")
                        Else
                            sStatusReason = ""
                        End If
                    End If
                End If

        Catch ex As Exception
            MessageBox.Show("Error on Patient Implantable device." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default

            If Not IsNothing(oParam) Then
                oParam.Dispose()
                oParam = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(dtIM) Then
                dtIM.Dispose()
                dtIM = Nothing
            End If

        End Try

    End Sub
    
    Private Sub AllowNumericValue(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

#End Region

#Region "Function"
    Private Function Save_Device() Handles Me.SaveFunction
        Try
            tblbtn_Save_Click(Nothing, Nothing)
            If (isSaved = False) Then
                _IsValidationFailed = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return Nothing
    End Function

#End Region

    Private Sub txt_UDI_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txt_UDI.KeyPress
        If e.KeyChar = ChrW(13) Then
            btnParseUDI.Enabled = True
            btnParseUDI.Focus()
        End If
    End Sub

    Private Sub btnConceptID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConceptID.Click
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem("Implantable Devices", gstrSMDBConnstr, GetConnectionString())
        Dim str As String = ""
        Try
            frm.strConceptDesc = ""
            frm.strDescriptionID = ""
            Dim splconceptid As String() = lblconcptid.Text.Trim().Split("-")
            If (splconceptid.Length > 1) Then
                frm.strConceptID = splconceptid(0)
                frm.txtSMSearch.Text = splconceptid(0)
            Else
                frm.strConceptID = lblconcptid.Text.Trim
                frm.txtSMSearch.Text = lblconcptid.Text.Trim
            End If
            If lblconcptid.Text.Trim = "" Then
                frm.txtSMSearch.Text = txtGmdnPTName.Text
            End If

            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))


            If frm._DialogResult Then

                If IsNothing(frm.strConceptID) = False Then


                    If Convert.ToString(frm.strConceptID).Trim = "0" Then
                        lblconcptid.Text = ""
                    Else
                        lblconcptid.Text = Convert.ToString(frm.strConceptID) '.ToString()
                    End If
                Else
                    lblconcptid.Text = frm.strConceptID
                End If
                If lblconcptid.Text.Trim() <> "" Then    ''changes done for 8020 snomed prd
                    If frm.strSelectedDescription.Trim() <> "" Then
                        lblconcptid.Text = lblconcptid.Text + "-" + frm.strSelectedDescription
                    End If
                End If
            End If


        Catch ex As Exception

            frm.Dispose()
        End Try
    End Sub

    Private Sub btnClrPlayingDevice_Click(sender As System.Object, e As System.EventArgs) Handles btnClrPlayingDevice.Click
        If lblconcptid.Text <> "" Then
            lblconcptid.Text = ""
        End If
    End Sub
    Private oCPTListControl As gloListControl.gloListControl
    Dim ofrmCPTList As frmViewListControl
    Private Sub btnBrowseProc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowseProc.Click
        Try

            ofrmCPTList = New frmViewListControl

            oCPTListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CPTProcedures, True, Me.Width)
            oCPTListControl.ControlHeader = "Procedures"
            AddHandler oCPTListControl.ItemSelectedClick, AddressOf oCPTListControl_ItemSelectedClick
            AddHandler oCPTListControl.ItemClosedClick, AddressOf oCPTListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oCPTListControl)
            oCPTListControl.Dock = DockStyle.Fill
            oCPTListControl.BringToFront()
            oCPTListControl.PatientID = _PatientID            
            oCPTListControl.ShowHeaderPanel(False)
            For i As Integer = 0 To lstProcedures.Items.Count - 1
                Dim splconceptid As String() = lstProcedures.Items(i).ToString().Trim().Split(":")
                oCPTListControl.SelectedItems.Add(splconceptid(0).Trim, splconceptid(1).Trim)
            Next


            oCPTListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Procedures"
            ofrmCPTList.ShowDialog(IIf(IsNothing(ofrmCPTList.Parent), Me, ofrmCPTList.Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oCPTListControl.ItemSelectedClick, AddressOf oCPTListControl_ItemSelectedClick
                RemoveHandler oCPTListControl.ItemClosedClick, AddressOf oCPTListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oCPTListControl)
                oCPTListControl.Dispose()
                oCPTListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing                
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oCPTListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Strdata As String = ""
        Try
            If lstProcedures.Items.Count > 0 Then
                lstProcedures.Items.Clear()
            End If
            If oCPTListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCPTListControl.SelectedItems.Count - 1
                    Strdata = oCPTListControl.SelectedItems(i).Code & " : " & oCPTListControl.SelectedItems(i).Description
                    lstProcedures.Items.Add(Strdata)
                Next
            End If
            ofrmCPTList.Close()

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub oCPTListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If
    End Sub

    Private Sub btnClearProc_Click(sender As System.Object, e As System.EventArgs) Handles btnClearProc.Click
        If lstProcedures.Items.Count > 0 Then
            lstProcedures.Items.Clear()
        End If
    End Sub
    

    Private Sub dttransaction_date_ValueChanged(sender As Object, e As System.EventArgs) Handles dttransaction_date.ValueChanged
        nVisitID = GenerateVisitID(Format(dttransaction_date.Value, "MM/dd/yyyy"), _PatientID)
    End Sub

   
End Class
