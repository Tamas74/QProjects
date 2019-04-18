Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloUserControlLibrary
Imports gloEMRGeneralLibrary.gloprintfax
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary
Imports System.IO
Imports gloSureScript

Imports schema = gloGlobal.Schemas.Surescript
Imports ss = gloGlobal.SS
Imports common = gloGlobal.Common.ServiceObjectBase


Public Class frmViewRxRequests

#Region "Variables and Properties"

    Dim selectedPharmacyNCPDP As String = Nothing
    Dim selectedPharmacyID As Int64 = Nothing
    Dim selectedProviderSPI As String = Nothing
    Dim selectedPatientID As Int64 = Nothing

    Dim SSChangeRequest As schema.RxChangeRequest = Nothing
    Dim SSMessageData As schema.MessageType = Nothing

    Dim _PatientID
    Dim _PrescriberId As String = ""

    Dim rxRequestMsgID As String = "" 'This is the MessageID used by Pharmacies to send a RefillRequest
    Dim intRefillpanelheight As Int32

    Private COL_Approve As Integer = 0
    Private COL_Deny As Integer = 1
    Private COL_Cancel As Integer = 2

    Private COL_RxReferenceNumber As Integer = 3
    Private COL_nMessageID As Integer = 4
    Private COL_RequestRepresentation As Integer = 5
    Private COL_PatientName As Integer = 6
    Private COL_PatientGender As Integer = 7
    Private COL_PatientDOB As Integer = 8
    Private COL_Medication As Integer = 9
    Private COL_Quantity As Integer = 10
    Private COL_PrescriptionDate As Integer = 11
    Private COL_DateReceived As Integer = 12
    Private COL_RefillQuantity As Integer = 13
    Private COL_PatientID As Integer = 14
    Private COL_RefillQualifier As Integer = 15
    Private COL_MessageID As Integer = 16
    Private COL_PharmacyID As Integer = 17
    Private COL_PatientLastName As Integer = 18
    Private COL_RefillReqNDCCode As Integer = 19
    Private COL_dtWrittenDate As Integer = 20
    Private COL_Notes As Integer = 21
    Private COL_Drugstreangth As Integer = 22
    Private COL_PatAddr1 As Integer = 23
    Private COL_PatAddr2 As Integer = 24
    Private COL_PatCity As Integer = 25
    Private COL_Patstate As Integer = 26
    Private COL_PatZip As Integer = 27
    Private COL_PatPhone As Integer = 28
    Private COL_PatFax As Integer = 29
    Private COL_RequestType As Integer = 30
    Private COL_RxTransactionId As Integer = 31

    Private COL_COUNT As Integer = 32
    Private filetodelete = Nothing
    'Private _rxFormType As RxFormType

    Dim surescriptsServiceURL As String = Nothing

    Dim helper As PrescriptionBusinessLayer = Nothing
    Dim WithEvents ss_helper As gloSureScript.gloSurescriptsHelper = Nothing

    Dim Dv_Search As DataView = Nothing
    Dim _blnSearch As Boolean = True 'for applying searching on grid
    Dim C1_DataTable As New DataTable
    Dim dvNext As DataView = Nothing
    Private _VisibleCount As Int16 = 0
    Dim strPatientLastName As String = ""
    Dim strPatientMiddleName As String = ""

    Dim dProviderID As Dictionary(Of String, Int64) = Nothing

    'Public Property FormType() As RxFormType
    '    Get
    '        Return _rxFormType
    '    End Get
    '    Set(ByVal value As RxFormType)
    '        _rxFormType = value
    '    End Set
    'End Property

    Public ReadOnly Property SelectedXMLRequest() As String
        Get
            If String.IsNullOrWhiteSpace(requestViewer.Tag) Then
                Return Nothing
            Else
                Return requestViewer.Tag
            End If
        End Get
    End Property


#End Region

#Region "Constructors"

    Public Sub New(ByVal PatientID As Long, ByVal PrescriberId As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        pnlProcessRequest.Height = 419
        pnlProcessRequest.Visible = False

        If PrescriberId <> "" Then
            _PrescriberId = PrescriberId
        End If

        _PatientID = PatientID
        'FormType = FormType

        helper = New PrescriptionBusinessLayer()
        surescriptsServiceURL = gstrSurescriptServiceURL
        ss_helper = New gloSureScript.gloSurescriptsHelper(surescriptsServiceURL)
        dProviderID = New Dictionary(Of String, Int64)
        gloSureScript.gloSurescriptGeneral.sUserName = gstrSQLUserEMR
        gloSureScript.gloSurescriptGeneral.sPassword = gstrSQLPasswordEMR

    End Sub

#End Region

#Region "Form Events"

    Private Sub frmRxRequest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        gloC1FlexStyle.Style(C1RefillList)

        'Dim oRefillRequest As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing

        Dim sMessageID As String = String.Empty
        Dim sFileXML As String = String.Empty
        Dim dbLayer As New PrescriptionBusinessLayer()
        Dim dt As DataTable = Nothing

        Try
            globalSecurity.gstrDatabaseName = gstrDatabaseName
            globalSecurity.gstrSQLServerName = gstrSQLServerName
            globalSecurity.gstrSQLUserEMR = gstrSQLUserEMR
            globalSecurity.gstrSQLPasswordEMR = gstrSQLPasswordEMR
            globalSecurity.gblnSQLAuthentication = gblnSQLAuthentication

            'oRefillRequest = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest()

            clsgeneral.gblnIsStagingServer = gblnStagingServer
            clsgeneral.StartUpPath = System.Windows.Forms.Application.StartupPath
            cntListmenuStrip.Items.Clear()

            If gblnSQLAuthentication = True Then '''' this is used in gloSurescriptGeneral.GetconnectionString()
                gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
            End If

            dt = dbLayer.GetPrescriberList()
            trvPrescribers.Nodes.Clear()
            trvPrescribers.Nodes.Add("Prescribers")
            trvPrescribers.Nodes.Item(0).ImageIndex = 3
            trvPrescribers.Nodes.Item(0).SelectedImageIndex = 3

            lblSearch.Text = "Search :"

            SetClgrid()

            'Select Case Me.FormType
            '    Case RxFormType.RxChangeRequest
            '        tabControl.SelectedTab = tabControl.TabPages(tbChangeRx.Name)
            'End Select

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Dim mynode As TreeNode

                    For icnt As Int32 = 0 To dt.Rows.Count - 1
                        mynode = New TreeNode
                        mynode.Tag = dt.Rows(icnt)(0)
                        mynode.Text = dt.Rows(icnt)(1)
                        mynode.ImageIndex = 6
                        mynode.SelectedImageIndex = 6
                        trvPrescribers.Nodes.Item(0).Nodes.Add(mynode)

                        If dProviderID.ContainsKey(Convert.ToString(mynode.Tag)) = False Then
                            dProviderID.Add(Convert.ToString(mynode.Tag), Convert.ToInt64(dt.Rows(icnt)("nProviderID")))
                        End If

                    Next

                    If _PrescriberId <> "" AndAlso _PrescriberId <> "0" Then
                        For i As Int16 = 0 To trvPrescribers.Nodes(0).GetNodeCount(True) - 1
                            If _PrescriberId = trvPrescribers.Nodes(0).Nodes(i).Tag Then
                                trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(i)
                                Exit For
                            End If
                        Next
                    Else
                        trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)
                        _PrescriberId = trvPrescribers.SelectedNode.Tag
                        trvPrescribers.ExpandAll()
                    End If

                    dt = helper.GetRxChangeRequests(_PrescriberId)

                    SetClgrid()

                    DisplayChangeRequestList(dt)
                    DisplayRequestDetails()

                    If Not IsNothing(dbLayer) Then
                        dbLayer.Dispose()
                        dbLayer = Nothing
                    End If

                End If
            End If

            Dim list As DataTable = helper.GetDenialReasonCodes()

            cmbDenialReasonCode.DisplayMember = "value"
            cmbDenialReasonCode.ValueMember = "key"
            cmbDenialReasonCode.DataSource = list

            Me.dgChangeRequests.AutoGenerateColumns = False

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.View, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.View, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not IsNothing(oRefillRequest) Then
            '    oRefillRequest.Dispose()
            '    oRefillRequest = Nothing
            'End If
        End Try
    End Sub

    Private Sub frmRxRequest_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(Dv_Search) = False) Then
                Dv_Search.Dispose()
                Dv_Search = Nothing
            End If
            If (IsNothing(C1_DataTable) = False) Then
                C1_DataTable.Dispose()
                C1_DataTable = Nothing
            End If

            Me.Dispose()
        Catch

        End Try
    End Sub

#End Region

#Region "C1 Events"

    Private Sub C1RefillList_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1RefillList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub


    Private Sub C1RefillList_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1RefillList.CellButtonClick

        Try
            rxRequestMsgID = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_nMessageID))
            selectedPatientID = Convert.ToInt64(C1RefillList.GetData(C1RefillList.RowSel, COL_PatientID))

            SetRxChangeRequestObject()
            selectedPharmacyID = GetPharmacyIDByNCPDP()

            Select Case e.Col
                Case COL_Approve
                    If selectedPatientID = 0 Then
                        selectedPatientID = GetPatientIdByDemographics()
                    End If
                    If selectedPatientID <> 0 Then
                        If (SSChangeRequest.Request.ChangeRequestType IsNot Nothing) Then
                            If (SSChangeRequest.Request.ChangeRequestType <> "P") Then
                                DisplayApprovalView()
                            Else
                                ApproveDrug(e.Row)
                            End If
                        End If
                    End If


                Case COL_Deny
                    DisplayDenialView(e.Row)

                Case COL_Cancel
                    pnlProcessRequest.Visible = False

            End Select

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub C1RefillList_SelChange(sender As Object, e As System.EventArgs) Handles C1RefillList.SelChange
        pnlProcessRequest.Visible = False
        pnlwbBrowser.Visible = True
        DisplayRequestDetails()
    End Sub

    Private Sub C1RefillList_BeforeSelChange(sender As System.Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1RefillList.BeforeSelChange
        filetodelete = pnlwbBrowser.Tag
    End Sub

#End Region

#Region "Events"

    Private Sub trvPrescribers_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvPrescribers.AfterSelect
        Try
            rxRequestMsgID = ""
            txtSearch.Text = ""


            DisplayRequests()
            If dProviderID IsNot Nothing AndAlso dProviderID.ContainsKey(Convert.ToString(e.Node.Tag)) Then
                Me.Set_EPCSProviderEnabled(dProviderID(Convert.ToString(e.Node.Tag)))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            DisplayRequests()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Refresh, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Close, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlStrpMain_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlStrpMain.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Cancel"
                    pnlProcessRequest.Visible = False
                    pnlwbBrowser.Visible = True
                Case "OK"
                    If pnlDeny.Visible Then
                        DenySelectedRequest()
                    ElseIf pnlApprove.Visible Then

                        Dim nSelectedRow As Int32 = GetSelectedDrugIndex()
                        If nSelectedRow = -1 Then
                            If SSChangeRequest.Request.ChangeRequestType = "T" Then
                                If MessageBox.Show("You have decided to approve a drug that differs from the list of suggested drugs. Do you want to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                    ApproveDrug(nSelectedRow)
                                    pnlProcessRequest.Visible = False
                                    pnlwbBrowser.Visible = True
                                End If
                            Else
                                MessageBox.Show("Please tick a drug to approve.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            ApproveDrug(nSelectedRow)
                            pnlProcessRequest.Visible = False
                            pnlwbBrowser.Visible = True
                        End If
                    End If
            End Select

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    'Private Sub tabControl_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
    '    Try
    '        If TabControl.SelectedTab Is tbChangeRx Then
    '            Me.FormType = RxFormType.RxChangeRequest
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.View, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

#End Region

#Region "Enumerations"

    Private Enum RefillStatus
        eApprove
        eDeny
        eCancel
    End Enum

    'Public Enum RxFormType
    '    RxFill
    '    CancelRx
    '    RxChangeRequest
    'End Enum

#End Region

#Region "Display logic"



    Public Sub SetRxChangeRequestObject()

        Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

        If requestViewer.Tag IsNot Nothing Then
            Using reader As New StringReader(requestViewer.Tag)
                SSMessageData = New schema.MessageType()
                xmlSerializer = New Xml.Serialization.XmlSerializer(SSMessageData.GetType())
                SSMessageData = xmlSerializer.Deserialize(reader)
            End Using
        End If

        If SSMessageData IsNot Nothing Then
            If SSMessageData.Header IsNot Nothing Then
                selectedPharmacyNCPDP = SSMessageData.Header.From.Value
                selectedProviderSPI = SSMessageData.Header.To.Value
            End If
            If SSMessageData.Body IsNot Nothing AndAlso SSMessageData.Body.Item IsNot Nothing Then
                If TypeOf (SSMessageData.Body.Item) Is schema.RxChangeRequest Then
                    SSChangeRequest = DirectCast(SSMessageData.Body.Item, schema.RxChangeRequest)
                End If
            End If
        End If
        'SSMessageData = Nothing
        xmlSerializer = Nothing
    End Sub
    Public Function GetPatientIdByDemographics() As Int64
        Dim _nSelectedPatientID As Int64 = 0
        Try
            If SSChangeRequest IsNot Nothing Then
                If SSChangeRequest.Patient IsNot Nothing Then
                    If SSChangeRequest.Patient.Name IsNot Nothing Then
                        With SSChangeRequest.Patient
                            _nSelectedPatientID = helper.GetPatientIdByDemographics(.Name.FirstName, .Name.LastName, .Gender, .DateOfBirth.Item.Date)
                        End With

                        If _nSelectedPatientID = 0 Then
                            _nSelectedPatientID = AttemptPatientID()
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _nSelectedPatientID
    End Function

    Public Function GetPharmacyIDByNCPDP() As Int64
        Dim _nSelectedPharmacyID As Int64 = 0
        Try
            If SSChangeRequest IsNot Nothing Then
                If SSChangeRequest.Pharmacy IsNot Nothing Then
                    If SSChangeRequest.Pharmacy.StoreName IsNot Nothing Then
                        _nSelectedPharmacyID = helper.GetPharmacyIDByNCPDP(selectedPharmacyNCPDP)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _nSelectedPharmacyID
    End Function

    Private Sub DisplayApprovalView()
        Dim sRequestString As String = String.Empty

        Try
            pnlApprove.Visible = True
            pnlDeny.Visible = False

            If rxRequestMsgID <> "" Then

                lblDuration.Text = ""
                lblDrug.Text = ""
                lblQuantity.Text = ""
                lblDirections.Text = ""
                lblPharmacyNotes.Text = ""
                lblRefills.Text = ""
                lblSubstitution.Text = ""
                lblPatientName.Text = ""
                lblpnlHeader.Text = "Approve Request"

                If SSChangeRequest IsNot Nothing Then

                    If SSChangeRequest.Patient IsNot Nothing AndAlso SSChangeRequest.Patient.Name IsNot Nothing Then
                        lblPatientName.Text = SSChangeRequest.Patient.Name.FirstName + IIf(SSChangeRequest.Patient.Name.MiddleName IsNot Nothing, " " + SSChangeRequest.Patient.Name.MiddleName, "") + IIf(SSChangeRequest.Patient.Name.LastName IsNot Nothing, " " + SSChangeRequest.Patient.Name.LastName, "")
                    End If


                    If SSChangeRequest.MedicationPrescribed IsNot Nothing Then
                        lblDrug.Text = SSChangeRequest.MedicationPrescribed.DrugDescription
                        lblQuantity.Text = SSChangeRequest.MedicationPrescribed.Quantity.Value

                        If SSChangeRequest.MedicationPrescribed.DaysSupply IsNot Nothing Then
                            If SSChangeRequest.MedicationPrescribed.DaysSupply <> "0" Then
                                lblDuration.Text = SSChangeRequest.MedicationPrescribed.DaysSupply + " Days"
                            End If
                        End If

                        lblDirections.Text = SSChangeRequest.MedicationPrescribed.Directions
                        lblPharmacyNotes.Text = SSChangeRequest.MedicationPrescribed.Note
                        lblRefills.Text = SSChangeRequest.MedicationPrescribed.Refills.Value

                        If SSChangeRequest.MedicationPrescribed.Substitutions IsNot Nothing Then
                            lblSubstitution.Text = IIf(SSChangeRequest.MedicationPrescribed.Substitutions = "0", "Yes", "No")
                        End If

                    End If

                    dgChangeRequests.DataSource = Nothing
                    If SSChangeRequest.MedicationRequested IsNot Nothing AndAlso SSChangeRequest.MedicationRequested.Any() Then
                        dgChangeRequests.DataSource = SSChangeRequest.MedicationRequested
                    End If
                End If

                pnlProcessRequest.Visible = True
                pnlwbBrowser.Visible = False

            Else
                MessageBox.Show("Select a request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisplayDenialView(ByVal row As Integer)
        pnlDeny.Visible = True
        pnlApprove.Visible = False
        lblpnlHeader.Text = "Deny Request"
        If rxRequestMsgID <> "" Then
            lblMedicationItemName.Text = Convert.ToString(C1RefillList.GetData(row, COL_Medication))
            lblDenyPatientName.Text = Convert.ToString(C1RefillList.GetData(row, COL_PatientName))
            txtNotes.Text = ""

            lblDenialReasoncode.Visible = True
            cmbDenialReasonCode.Visible = True

            pnlProcessRequest.Visible = True
            pnlwbBrowser.Visible = False

        Else
            MessageBox.Show("Select a request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub DisplayRequestDetails()
        Dim dbLayer As New PrescriptionBusinessLayer()
        Dim sFileXML As String = String.Empty

        pnlProcessRequest.Visible = False
        pnlApprove.Visible = False
        Try
            If C1RefillList.RowSel >= 0 Then
                rxRequestMsgID = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_nMessageID))
            End If

            If (String.IsNullOrWhiteSpace(rxRequestMsgID)) Or (C1RefillList.RowSel < 0) Then
                requestViewer.Visible = False
                requestViewer.Navigate("about:blank")
                DeleteHTMLFile()
            Else
                requestViewer.Visible = True
                sFileXML = dbLayer.GetRxMessageXMLByID(rxRequestMsgID)
                XMLtoHTMLFileLoad(sFileXML)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dbLayer) Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try

    End Sub

    Private Function DisplayChangeRequestList(ByVal dt As DataTable)
        'Dim _sPatFirstName As String = ""
        'Dim _sPatLastName As String = ""
        'Dim _sPatDOB As String = ""
        'Dim _sPatGender As String = ""

        'Dim _PatID As String = 0

        Try
            RemoveHandler C1RefillList.SelChange, AddressOf C1RefillList_SelChange

            Dim csComboList As C1.Win.C1FlexGrid.CellStyle
            Try
                If (C1RefillList.Styles.Contains("CS_ComboList")) Then
                    csComboList = C1RefillList.Styles("CS_ComboList")
                Else
                    csComboList = C1RefillList.Styles.Add("CS_ComboList")
                    With csComboList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        '.ForeColor = Color.Black
                        '.BackColor = Color.GhostWhite
                        .DataType = GetType(String)
                        .ComboList = "..."
                    End With
                End If
            Catch ex As Exception
                csComboList = C1RefillList.Styles.Add("CS_ComboList")
                With csComboList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    '.ForeColor = Color.Black
                    '.BackColor = Color.GhostWhite
                    .DataType = GetType(String)
                    .ComboList = "..."
                End With
            End Try

            C1RefillList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            For i As Integer = 0 To dt.Rows.Count - 1
                With C1RefillList
                    .Rows.Add()

                    .SetCellStyle(i + 1, COL_Approve, .Styles("CS_ComboList"))
                    .SetCellStyle(i + 1, COL_Deny, .Styles("CS_ComboList"))
                    .SetCellStyle(i + 1, COL_Cancel, .Styles("CS_ComboList"))

                    .SetData(i + 1, COL_Approve, "")
                    .SetData(i + 1, COL_Deny, "")
                    .SetData(i + 1, COL_Cancel, "")

                    '_sPatFirstName = dt.Rows(i)("sPatientFirstName")
                    '_sPatLastName = dt.Rows(i)("sPatientLastName")
                    '_sPatGender = dt.Rows(i)("sPatientGender")
                    '_sPatDOB = dt.Rows(i)("sPatientDOB")

                    .SetData(i + 1, COL_nMessageID, Convert.ToString(dt.Rows(i)("nMessageID")))
                    .SetData(i + 1, COL_RequestRepresentation, Convert.ToString(dt.Rows(i)("sChangeRequestType")))
                    .SetData(i + 1, COL_RequestType, Convert.ToString(dt.Rows(i)("sRequestType")))
                    .SetData(i + 1, COL_RxTransactionId, Convert.ToString(dt.Rows(i)("nRxTransactionID")))

                    .SetData(i + 1, COL_PatientName, Convert.ToString(dt.Rows(i)("sPatientName")))
                    .SetData(i + 1, COL_PatientLastName, Convert.ToString(dt.Rows(i)("sPatientLastName")))

                    .SetData(i + 1, COL_PatientGender, dt.Rows(i)("sPatientGender"))
                    .SetData(i + 1, COL_PatientDOB, dt.Rows(i)("sPatientDOB"))
                    .SetData(i + 1, COL_Medication, dt.Rows(i)("sDrugName"))

                    .SetData(i + 1, COL_Quantity, dt.Rows(i)("sDrugQuantity"))
                    .SetData(i + 1, COL_PrescriptionDate, dt.Rows(i)("dtWrittenDate"))
                    .SetData(i + 1, COL_RefillQuantity, dt.Rows(i)("sRefillQuantity"))
                    .SetData(i + 1, COL_RefillQualifier, dt.Rows(i)("sRefillsQualifier"))
                    .SetData(i + 1, COL_DateReceived, dt.Rows(i)("dtDateReceived"))

                    'Select Case _sPatGender.ToUpper()
                    '    Case "M"
                    '        _sPatGender = "Male"
                    '    Case "MALE"
                    '        _sPatGender = "Male"
                    '    Case "F"
                    '        _sPatGender = "Female"
                    '    Case "FEMALE"
                    '        _sPatGender = "Female"
                    '    Case Else
                    '        _sPatGender = "Other"
                    'End Select

                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("PatientID"))
                    .SetData(i + 1, COL_MessageID, dt.Rows(i)("nMessageID"))
                End With
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Initialize, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dgRefillList.ContextMenuStrip = cntListmenuStrip
            AddHandler C1RefillList.SelChange, AddressOf C1RefillList_SelChange

        End Try

        Return Nothing
    End Function

    Public Function DisplayRequests()

        rxRequestMsgID = ""

        Dim dtRxChangeData As DataTable = Nothing
        Dim rxChangeDBLayer As New PrescriptionBusinessLayer()
        Dim mynode As TreeNode = Nothing

        pnlProcessRequest.Visible = False

        Try
            'CType(Me.MdiParent, MainMenu).FillMessage()

            dtRxChangeData = New DataTable
            mynode = CType(trvPrescribers.SelectedNode, TreeNode)

            If IsNothing(trvPrescribers.SelectedNode) = False Then
                If mynode.Text = "Prescribers" Then
                    C1RefillList.DataSource = Nothing
                Else
                    dtRxChangeData = rxChangeDBLayer.GetRxChangeRequests(Convert.ToString(trvPrescribers.SelectedNode.Tag))

                    If dtRxChangeData IsNot Nothing AndAlso dtRxChangeData.Rows.Count > 0 Then
                        SetClgrid()
                        DisplayChangeRequestList(dtRxChangeData)
                    Else
                        C1RefillList.DataSource = Nothing
                        C1RefillList.Refresh()
                        SetClgrid()
                    End If

                End If
            Else
                dgRefillList.DataSource = Nothing
            End If

            DisplayRequestDetails()

        Catch ex As PrescriptionException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If Not IsNothing(rxChangeDBLayer) Then
                rxChangeDBLayer.Dispose()
                rxChangeDBLayer = Nothing
            End If

            If dtRxChangeData IsNot Nothing Then
                dtRxChangeData.Dispose()
                dtRxChangeData = Nothing
            End If

        End Try
        Return Nothing
    End Function

#End Region

#Region "XML to HTML"

    Private Sub DeleteHTMLFile()
        Try
            Dim sFileToDelete As String = Convert.ToString(filetodelete)
            If Not String.IsNullOrEmpty(sFileToDelete) Then
                If File.Exists(sFileToDelete) Then
                    File.Delete(sFileToDelete)
                End If
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub XMLtoHTMLFileLoad(ByVal strContent As String)

        Dim _firstTransformation As String = ""
        Dim _secondTransforamtion As String = ""
        Dim UniqueFileName As String = gloGlobal.clsFileExtensions.GetUniqueDateString()
        Dim _strfileName1 As String = ""
        Dim oglointerface As New gloSureScript.gloSureScriptInterface

        Try

            If (Not String.IsNullOrEmpty(strContent)) Then
                _firstTransformation = oglointerface.Transform(strContent, System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
                _secondTransforamtion = oglointerface.Transform(_firstTransformation, System.Windows.Forms.Application.StartupPath & "\RxRequestSummary.xsl")
                _strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & UniqueFileName & ".html"

                requestViewer.Navigate("about:blank")

                If _strfileName1 <> "" Then

                    DeleteHTMLFile()

                    File.WriteAllText(_strfileName1, _secondTransforamtion)

                    requestViewer.Navigate(_strfileName1)
                    requestViewer.Tag = strContent
                    pnlwbBrowser.Tag = _strfileName1
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oglointerface) Then
                oglointerface.Dispose()
                oglointerface = Nothing
            End If
        End Try

    End Sub

#End Region

#Region "Deny Request"
    Private Function IsValidDenyRequest() As Boolean

        If String.IsNullOrWhiteSpace(rxRequestMsgID) Then
            MessageBox.Show("Select a request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If IsInternetConnectionAvailable() = False Then
            MessageBox.Show("Internet connection does not exist.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Return True
    End Function

    Private Sub DenySelectedRequest()


        If Not IsValidDenyRequest() Then
            Return
        End If

        Try
            ss_helper.DenyRxChangeRequest(rxRequestMsgID, requestViewer.Tag, cmbDenialReasonCode.SelectedValue, txtNotes.Text)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Load, "Error while denying RxChange Request", gloAuditTrail.ActivityOutCome.Success)

        End Try

    End Sub

    Public Sub ss_helper_RequestApproved(ByVal statusMessage As StatusMessage, ByVal ResponseMessage As Object) Handles ss_helper.RequestApproved
        Dim sSelectedPatientID As String = String.Empty
        Dim statusMessageText As String = String.Empty
        Dim drugName As String = String.Empty

        drugName = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_Medication))
        sSelectedPatientID = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_PatientID))

        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
        gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
        gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName

        Dim updateStatus As Boolean = True

        ' Problem #00000245 : Error Prompted on Pending Message Pop Box But After click on it no longer in screen.
        ' as per discussion in status meeting decided that no need to insert Status (Error) Messages in Database.
        If statusMessage.MessageName <> "Error" Then
            'Insert acknowledgement details in acknowledgement transaction
            If objSureScriptDBLayer.InsertAcknowledgements(statusMessage, True) Then
                'Insert data in message transaction
                If objSureScriptDBLayer.InsertintoMessageTransaction(CType(statusMessage, SureScriptMessage)) Then

                End If
            End If
        Else
            updateStatus = False
        End If

        If Not objSureScriptDBLayer Is Nothing Then
            objSureScriptDBLayer.Dispose()
            objSureScriptDBLayer = Nothing
        End If

        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Load, "Rx change request approved", gloAuditTrail.ActivityOutCome.Success)


        '' Display the status message
        statusMessageText = ss_helper.GetStatusMessage(statusMessage, gloSureScriptInterface.SentMessageType.eDenied, "RxChange", drugName)
        MessageBox.Show(statusMessageText, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '' Saving the denied transaction details in to DB
        helper.SaveDeniedTransaction(rxRequestMsgID, ResponseMessage, False, statusMessageText, cmbDenialReasonCode.SelectedText, txtNotes.Text, cmbDenialReasonCode.SelectedValue, sSelectedPatientID, "0", True, updateStatus)

        'helper.UpdateDeniedStatus(sMessageID)

        DisplayRequests()


    End Sub

    Public Sub ss_helper_RequestDenied(ByVal statusMessage As StatusMessage, ByVal ResponseMessage As Object) Handles ss_helper.RequestDenied
        Dim sSelectedPatientID As String = String.Empty
        Dim nSelectedPatientID As Int64 = 0
        Dim statusMessageText As String = String.Empty
        Dim drugName As String = String.Empty

        drugName = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_Medication))
        sSelectedPatientID = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_PatientID))
        Int64.TryParse(sSelectedPatientID, nSelectedPatientID)

        Dim objSureScriptDBLayer As New gloSureScriptDBLayer
        gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
        gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName

        Dim updateStatus As Boolean = True

        ' Problem #00000245 : Error Prompted on Pending Message Pop Box But After click on it no longer in screen.
        ' as per discussion in status meeting decided that no need to insert Status (Error) Messages in Database.
        If statusMessage.MessageName <> "Error" Then
            'Insert acknowledgement details in acknowledgement transaction
            If objSureScriptDBLayer.InsertAcknowledgements(statusMessage, True) Then
                'Insert data in message transaction
                If objSureScriptDBLayer.InsertintoMessageTransaction(CType(statusMessage, SureScriptMessage)) Then

                End If
            End If
        Else
            updateStatus = False
        End If

        If Not objSureScriptDBLayer Is Nothing Then
            objSureScriptDBLayer.Dispose()
            objSureScriptDBLayer = Nothing
        End If

        If statusMessage.MessageName <> "Error" Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Send, "Rx Change request denied", nSelectedPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Else
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Send, "Rx Change request was not denied", nSelectedPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

        '' Display the status message
        statusMessageText = ss_helper.GetStatusMessage(statusMessage, gloSureScriptInterface.SentMessageType.eDenied, "RxChange", drugName)
        MessageBox.Show(statusMessageText, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '' Saving the denied transaction details in to DB
        helper.SaveDeniedTransaction(rxRequestMsgID, ResponseMessage, False, statusMessageText, cmbDenialReasonCode.SelectedText, txtNotes.Text, cmbDenialReasonCode.SelectedValue, sSelectedPatientID, "0", True, updateStatus)

        'helper.UpdateDeniedStatus(sMessageID)

        DisplayRequests()


    End Sub
#End Region

#Region "Approve Request"

    Private Function ConvertToServiceMedication(ByVal SSMedicationRequested As schema.RxChangeDispensedMedicationType) As common.Medication
        Dim returned As common.Medication = Nothing

        Try
            returned = New common.Medication()
            With returned
                .days = SSMedicationRequested.DaysSupply
                .direction = SSMedicationRequested.Directions

                If SSMedicationRequested.DrugCoded IsNot Nothing Then
                    .drugCode = SSMedicationRequested.DrugCoded.ProductCode
                    .drugQual = SSMedicationRequested.DrugCoded.ProductCodeQualifier
                    .medication = SSMedicationRequested.DrugDescription
                    .ndc = SSMedicationRequested.DrugCoded.ProductCode
                End If

                If SSMedicationRequested.Quantity IsNot Nothing Then
                    .qty = SSMedicationRequested.Quantity.Value
                    .qtyUnit = SSMedicationRequested.Quantity.PotencyUnitCode
                End If

                If SSMedicationRequested.DrugCoded IsNot Nothing Then
                    .strength = New common.Strength()
                    .strength.unit = SSMedicationRequested.DrugCoded.StrengthCode
                    .strength.value = SSMedicationRequested.DrugCoded.Strength
                End If

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return returned
    End Function

    Private Function GetSelectedDrugIndex() As Int32
        Dim nReturned As Int32 = -1
        Try
            If dgChangeRequests.Rows IsNot Nothing Then
                For Each row As DataGridViewRow In dgChangeRequests.Rows
                    If row.Cells(dgChangeRequests.Columns.IndexOf(colChecked)).Value = True Then
                        nReturned = dgChangeRequests.Rows.IndexOf(row)
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return nReturned
    End Function

    Private Function GetSelectedSSMedication(ByVal row As Int32) As schema.RxChangeDispensedMedicationType
        Dim returned As schema.RxChangeDispensedMedicationType = Nothing
        Dim selectedRow As DataGridViewRow = Nothing

        Try
            selectedRow = dgChangeRequests.Rows(row)

            If selectedRow IsNot Nothing AndAlso selectedRow.DataBoundItem IsNot Nothing AndAlso TypeOf (selectedRow.DataBoundItem) Is schema.RxChangeDispensedMedicationType Then
                returned = DirectCast(selectedRow.DataBoundItem, schema.RxChangeDispensedMedicationType)
            End If
        Finally
            selectedRow = Nothing
        End Try

        Return returned
    End Function

    Private Function GetSelectedMedicationRequested(ByVal row As Int32) As common.Medication
        Dim returned As common.Medication = Nothing
        Dim medicationRequested As schema.RxChangeDispensedMedicationType = Nothing

        Try
            medicationRequested = Me.GetSelectedSSMedication(row)
            If medicationRequested IsNot Nothing Then
                returned = Me.ConvertToServiceMedication(medicationRequested)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Return returned
    End Function

    Private Function ResultOfMessage(ByVal DEASchedule As schema.DrugCodedTypeDEASchedule, ByVal RefillQualifer As String) As DialogResult
        Dim dialogResult As DialogResult = Windows.Forms.DialogResult.Yes
        Dim nDEASchedule As Int32 = Convert.ToInt32(DEASchedule)

        Try
            If nDEASchedule >= 2 Then
                If gbIsProviderEPCSEnable Then
                    If Me.ValidateEPCSData(SSChangeRequest) Then
                        If RefillQualifer IsNot Nothing AndAlso RefillQualifer.ToUpper() = "PRN" Then
                            dialogResult = MessageBox.Show("This RxChange Request is for controlled substance with PRN refill qualifier. RxChange Request for controlled substance cannot be approved with PRN refill qualifier. " & vbCrLf & vbCrLf & "Do you want to approve this RxChange Request without PRN?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        End If
                    Else
                        dialogResult = Windows.Forms.DialogResult.No
                    End If
                Else
                    Dim sBuilder As New System.Text.StringBuilder
                    sBuilder.Append("You have chosen to approve a RxChange Request for a controlled substance. A new printed prescription will be generated and provided to the pharmacy. " & vbCrLf & "")
                    sBuilder.Append("Would you like to proceed with printing the prescription?")

                    dialogResult = MessageBox.Show(sBuilder.ToString, "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    sBuilder.Clear()
                    sBuilder = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dialogResult
    End Function

    Private Sub ApproveDrug(ByVal row As Integer)
        Dim sRequestString As String = String.Empty
        Dim nPatientID As Int64 = 0

        Dim gloChangeRequest As ss.RxChangeRequest = Nothing
        Dim medicationRequested As schema.RxChangeDispensedMedicationType = Nothing
        Dim frmPrescription As frmPrescription = Nothing

        Dim nRxTransactionID As Int64 = 0
        Dim sRxTransactionId As String = ""

        Dim sTransactionReferenceNumber As String = ""
        Dim sRefillQualifier As String = ""

        Try
            sRequestString = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_RequestType))
            sRxTransactionId = Convert.ToString(C1RefillList.GetData(C1RefillList.RowSel, COL_RxTransactionId))
            Int64.TryParse(sRxTransactionId, nRxTransactionID)

            Dim pharmacy As schema.RxChangeRequestPharmacyType = SSChangeRequest.Pharmacy

            If SSMessageData IsNot Nothing AndAlso SSMessageData.Header IsNot Nothing Then
                If SSMessageData.Header.RxReferenceNumber IsNot Nothing Then
                    sTransactionReferenceNumber = SSMessageData.Header.RxReferenceNumber
                End If
            End If

            If SSChangeRequest.Request.ChangeRequestType IsNot Nothing Then
                If SSChangeRequest.Request.ChangeRequestType <> "P" Then
                    If row >= 0 Then
                        medicationRequested = Me.GetSelectedSSMedication(row)

                        If medicationRequested IsNot Nothing AndAlso medicationRequested.Refills IsNot Nothing Then
                            sRefillQualifier = medicationRequested.Refills.Qualifier
                        End If

                        If ResultOfMessage(medicationRequested.DrugCoded.DEASchedule, sRefillQualifier) = Windows.Forms.DialogResult.No Then
                            Exit Sub
                        End If
                    End If

                    gloChangeRequest = New ss.RxChangeRequest(rxRequestMsgID, nRxTransactionID, selectedPatientID, selectedPharmacyID, sTransactionReferenceNumber, sRequestString, SSChangeRequest.MedicationPrescribed, medicationRequested, Nothing, Convert.ToString(requestViewer.Tag))

                Else
                    Dim BenefitCord As schema.BenefitsCoordinationType = Nothing
                    If SSChangeRequest.BenefitsCoordination IsNot Nothing Then
                        BenefitCord = SSChangeRequest.BenefitsCoordination().FirstOrDefault()
                    End If

                    If SSChangeRequest.MedicationPrescribed IsNot Nothing AndAlso SSChangeRequest.MedicationPrescribed.Refills IsNot Nothing Then
                        sRefillQualifier = SSChangeRequest.MedicationPrescribed.Refills.Qualifier
                    End If

                    If SSChangeRequest.MedicationPrescribed.DrugCoded IsNot Nothing Then
                        If ResultOfMessage(SSChangeRequest.MedicationPrescribed.DrugCoded.DEASchedule, sRefillQualifier) = Windows.Forms.DialogResult.No Then
                            Exit Sub
                        End If
                    End If

                    gloChangeRequest = New ss.RxChangeRequest(rxRequestMsgID, nRxTransactionID, selectedPatientID, selectedPharmacyID, sTransactionReferenceNumber, sRequestString, SSChangeRequest.MedicationPrescribed, Nothing, BenefitCord, Convert.ToString(requestViewer.Tag))
                    BenefitCord = Nothing
                End If

                frmPrescription = frmPrescription.GetInstance(gloChangeRequest)
            End If


            If frmPrescription IsNot Nothing Then
                If frmPrescription.blncancel = True Then
                    frmPrescription.ShowInTaskbar = False
                    frmPrescription.MdiParent = Me.MdiParent
                    'new condition added to fix bug 14363
                    If frmPrescription.GetCurrentPatientID <> nPatientID Then
                        'frmPrescription.Setform = Me
                    Else
                        ''TODO: Verify & do the necessary changes if required for RxChange
                        'frmRx.IsDefaultPharmacyChanged = _setDefaultPharmacy
                        'frmRx.RefillRequest(nPatientId_RefillReq, 0, strQuantity, sRxReferenceNumber, dtdatereceived, sMessageID, _refreqNDCCode, oRefillRequest.ProviderID, oRefillRequest.PharmacyID)
                        frmPrescription.BringToFront()
                        frmPrescription.WindowState = FormWindowState.Maximized
                    End If

                    frmPrescription.Show()
                End If
            End If
            'End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            gloChangeRequest = Nothing
            'gloMedication = Nothing
        End Try
    End Sub

#End Region

#Region "Search logic"

    Private Sub txtSearch_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            'select the first row of the grid
            If (e.KeyChar = ChrW(13)) Then
                If C1RefillList.RowSel >= 0 Then
                    C1RefillList.Select(1, 1, 1, 1)
                    C1RefillList.RowSel = 0
                End If
            End If

            mdlGeneral.ValidateText(txtSearch.Text, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_SearchFired() Handles txtSearch.SearchFired
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim oCol As DataColumn

                If IsNothing(Dv_Search) And txtSearch.Text.Trim <> "" Then

                    If C1RefillList.Cols.Count > 0 Then
                        oCol = New DataColumn
                        For i As Integer = 4 To C1RefillList.Cols.Count - 1
                            oCol.Caption = C1RefillList.GetData(0, i)
                            oCol.ColumnName = C1RefillList.GetData(0, i)
                            If (C1_DataTable.Columns.Contains(Convert.ToString(C1RefillList.GetData(0, i))) = False) Then
                                C1_DataTable.Columns.Add(C1RefillList.GetData(0, i))
                            End If
                        Next
                    End If

                    Dim oRow As DataRow
                    If C1RefillList.Rows.Count > 1 Then

                        For iRow As Integer = 1 To C1RefillList.Rows.Count - 1

                            oRow = C1_DataTable.NewRow

                            For iCol As Integer = 4 To C1RefillList.Cols.Count - 1
                                oRow(iCol - 4) = C1RefillList.GetData(iRow, iCol)    'first 3 are button column so remove that 
                            Next
                            C1_DataTable.Rows.Add(oRow)

                        Next
                        Dv_Search = C1_DataTable.DefaultView
                    End If
                End If
                ' ------
                Dim dt As DataTable


                If IsNothing(Dv_Search) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                InstringSearch()

                C1RefillList.DataSource = Nothing
                SetClgrid()
                dt = Dv_Search.ToTable
                setdatatoC1(dt)

                DisplayRequestDetails()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub InstringSearch()
        Try
            If Dv_Search Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Exit Sub
            End If


            'Dim COL_RxReferenceNumber As Byte = 3
            'Dim COL_nMessageID As Byte = 4
            Dim COL_RequestType As Byte = 5
            Dim COL_PatientName As Byte = 6
            Dim COL_PatientGender As Byte = 7
            Dim COL_PatientDOB As Byte = 8
            Dim COL_Medication As Byte = 9
            Dim COL_Quantity As Byte = 10
            Dim COL_PrescriptionDate As Byte = 11
            Dim COL_DateReceived As Integer = 12
            Dim COL_RefillQuantity As Byte = 13
            Dim COL_PatientId As Byte = 14
            Dim COL_RefillQualifier As Byte = 15
            Dim COL_PatientLastName As Byte = 18
            Dim COL_RefillReqNDCCode As Byte = 19
            Dim COL_dtWrittenDate As Byte = 20

            Dim str As String = ""
            ' Dim rowid As Byte
            Dim strSearchArray As String()
            Dim strexpr As String = ""
            str = txtSearch.Text

            str = str.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")

            If str.Trim() <> "" Then
                strSearchArray = str.Split(","c)
                Dim strSearch As String = ""
                If strSearchArray.Length = 1 Then
                    strSearch = strSearchArray(0)
                    Dv_Search.RowFilter = "[Request]  Like '%" & strSearch & "%' OR [Name] Like '%" & strSearch & "%' OR [Gender] Like '%" & strSearch & "%' OR [DOB] Like '%" & strSearch & "%'" & " OR [Medication Prescribed] Like '%" & strSearch & "%' OR [Quantity] Like '%" & strSearch & "%' OR [Rx Date] Like '%" & strSearch & "%' OR [Date Rec.] Like '%" & strSearch & "%' OR [Refill Qty] Like '%" & strSearch & "%' OR [Ref. Qualifier]  Like '%" & strSearch & "%'"  'strSearch' is not declared. It may be inaccessible due to its protection level.	

                Else
                    Dim dtTemp As DataTable = Nothing
                    For i As Byte = 1 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i)
                        If strSearch.Trim() <> "" Then
                            If i = 1 Then
                                dtTemp = Dv_Search.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            Else
                                dtTemp = dvNext.ToTable()
                                dvNext = dtTemp.Copy().DefaultView
                                dtTemp.Dispose()
                                dtTemp = Nothing
                            End If


                            dvNext.RowFilter = dvNext.Table.Columns(COL_RequestType).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PatientName).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PatientGender).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PatientDOB).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_Medication).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_Quantity).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PrescriptionDate).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DateReceived).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RefillQuantity).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RefillQualifier).ColumnName & " Like '%" & strSearch & "%'" ' OR " & dvNext.Table.Columns(COL_RefillQualifier).ColumnName & " Like '%" & strSearch & "%'"
                        End If
                    Next
                    If Not IsNothing(dtTemp) Then ''disposed as per glo Code optimizer tool in 8000 version
                        dtTemp.Dispose()
                        dtTemp = Nothing
                    End If
                End If

                If strSearch <> "" AndAlso strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        C1RefillList.DataSource = Dv_Search
                        _VisibleCount = Dv_Search.Count
                    Else
                        C1RefillList.DataSource = dvNext
                        _VisibleCount = dvNext.Count
                    End If
                End If
            Else
                Dv_Search.RowFilter = ""
                C1RefillList.DataSource = Dv_Search
                _VisibleCount = Dv_Search.Count
            End If

            ' C1RefillList.Refresh()

            If txtSearch.Text.Trim() = "" Then
                C1RefillList.DataSource = C1_DataTable
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Search, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region

#Region "C1 Functions"

    Private Function SetClgrid()
        Try
            With C1RefillList
                .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular)
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
                .BackColor = System.Drawing.Color.White
                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
                .Col = 0
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Fixed = 0
                .Cols.Count = COL_COUNT
                .ExtendLastCol = True

                Dim _Width As Single = .Width / 10

                .Cols(COL_Approve).Width = _Width * 0.6
                .Cols(COL_Deny).Width = _Width * 0.5
                .Cols(COL_Cancel).Width = _Width * 0.5

                .Cols(COL_RxReferenceNumber).Width = 0
                .Cols(COL_nMessageID).Width = 0
                .Cols(COL_RequestRepresentation).Width = _Width * 1.5
                .Cols(COL_RequestType).Width = 0
                .Cols(COL_RxTransactionId).Width = 0

                .Cols(COL_PatientName).Width = _Width * 1.5
                .Cols(COL_PatientGender).Width = _Width * 0.5
                .Cols(COL_PatientDOB).Width = _Width * 1
                .Cols(COL_Medication).Width = _Width * 2
                .Cols(COL_Quantity).Width = _Width * 1.0
                .Cols(COL_PrescriptionDate).Width = _Width * 1.4
                .Cols(COL_DateReceived).Width = _Width * 1.2
                .Cols(COL_RefillQuantity).Width = _Width * 0.7
                .Cols(COL_PatientID).Width = 0
                .Cols(COL_RefillQualifier).Width = _Width * 1

                .Cols(COL_MessageID).Width = 0
                .Cols(COL_PharmacyID).Width = 0
                .Cols(COL_PatientLastName).Width = 0
                .Cols(COL_RefillReqNDCCode).Width = 0
                .Cols(COL_dtWrittenDate).Width = 0
                .Cols(COL_Notes).Width = 0
                .Cols(COL_Drugstreangth).Width = 0


                .SetData(0, COL_Approve, "Approve")
                .SetData(0, COL_Deny, "Deny")
                .SetData(0, COL_Cancel, "Cancel")

                .SetData(0, COL_RxReferenceNumber, "RxReferenceNumber")
                .SetData(0, COL_nMessageID, "nMessageID")
                .SetData(0, COL_RequestRepresentation, "Request")
                .SetData(0, COL_RequestType, "RequestType")
                .SetData(0, COL_RxTransactionId, "RxTransactionId")

                .SetData(0, COL_PatientName, "Name")
                .SetData(0, COL_PatientGender, "Gender")
                .SetData(0, COL_PatientDOB, "DOB")
                .SetData(0, COL_Medication, "Medication Prescribed")
                .SetData(0, COL_Quantity, "Quantity")
                .SetData(0, COL_PrescriptionDate, "Rx Date")
                .SetData(0, COL_DateReceived, "Date Rec.")
                .SetData(0, COL_RefillQuantity, "Refill Qty")
                .SetData(0, COL_PatientID, "PatientId")
                .SetData(0, COL_RefillQualifier, "Ref. Qualifier")
                .SetData(0, COL_MessageID, "MessageID")
                .SetData(0, COL_PharmacyID, "PharmacyID")
                .SetData(0, COL_PatientLastName, "Last Name")
                .SetData(0, COL_RefillReqNDCCode, "RefillReqNDCCode")
                .SetData(0, COL_dtWrittenDate, "WrittenDate")
                .SetData(0, COL_Notes, "DrugNotes")
                .SetData(0, COL_Drugstreangth, "Strength")

                .SetData(0, COL_PatAddr1, "PatientAddress1")
                .SetData(0, COL_PatAddr2, "PatientAddress2")
                .SetData(0, COL_PatCity, "PatientCity")
                .SetData(0, COL_Patstate, "PatientState")
                .SetData(0, COL_PatZip, "PatientZipcode")
                .SetData(0, COL_PatPhone, "PatPhone")
                .SetData(0, COL_PatFax, "PatientFax")

                .Cols(COL_Approve).Visible = True
                .Cols(COL_Deny).Visible = True

                If gblnAllowRefillCancel = False Then
                    .Cols(COL_Cancel).Visible = True
                Else
                    .Cols(COL_Cancel).Visible = False
                End If

                .Cols(COL_RxReferenceNumber).Visible = False
                .Cols(COL_nMessageID).Visible = False
                .Cols(COL_RequestRepresentation).Visible = True
                .Cols(COL_RequestType).Visible = False
                .Cols(COL_RxTransactionId).Visible = False
                .Cols(COL_PatientName).Visible = True
                .Cols(COL_PatientGender).Visible = True
                .Cols(COL_PatientDOB).Visible = True
                .Cols(COL_Medication).Visible = True
                .Cols(COL_Quantity).Visible = True
                .Cols(COL_PrescriptionDate).Visible = True
                .Cols(COL_DateReceived).Visible = True
                .Cols(COL_RefillQuantity).Visible = True
                .Cols(COL_PatientID).Visible = False
                .Cols(COL_RefillQualifier).Visible = True
                .Cols(COL_MessageID).Visible = False
                .Cols(COL_PharmacyID).Visible = False
                .Cols(COL_PatientLastName).Visible = False
                .Cols(COL_RefillReqNDCCode).Visible = False
                .Cols(COL_dtWrittenDate).Visible = False
                .Cols(COL_Notes).Visible = False
                .Cols(COL_Drugstreangth).Visible = False

                .Cols(COL_RxReferenceNumber).AllowEditing = False
                .Cols(COL_nMessageID).AllowEditing = False
                .Cols(COL_RequestRepresentation).AllowEditing = False
                .Cols(COL_RequestType).AllowEditing = False
                .Cols(COL_RxTransactionId).AllowEditing = False
                .Cols(COL_PatientName).AllowEditing = False
                .Cols(COL_PatientGender).AllowEditing = False
                .Cols(COL_PatientDOB).AllowEditing = False
                .Cols(COL_Medication).AllowEditing = False
                .Cols(COL_Quantity).AllowEditing = False
                .Cols(COL_PrescriptionDate).AllowEditing = False
                .Cols(COL_DateReceived).AllowEditing = False
                .Cols(COL_RefillQuantity).AllowEditing = False
                .Cols(COL_PatientID).AllowEditing = False
                .Cols(COL_RefillQualifier).AllowEditing = False
                .Cols(COL_MessageID).AllowEditing = False
                .Cols(COL_PharmacyID).AllowEditing = False
                .Cols(COL_PatientLastName).AllowEditing = False
                .Cols(COL_RefillReqNDCCode).AllowEditing = False

                .Cols(COL_PatAddr1).Visible = False
                .Cols(COL_PatAddr2).Visible = False
                .Cols(COL_PatCity).Visible = False
                .Cols(COL_Patstate).Visible = False
                .Cols(COL_PatZip).Visible = False
                .Cols(COL_PatPhone).Visible = False
                .Cols(COL_PatFax).Visible = False

                '.ForeColor = Color.Black
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Function SplitPatientName(ByVal PatientName As String) As Array
        Try
            Dim _result As String()
            _result = PatientName.Split(" ")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function SplitPatientName_WithPipe(ByVal PatientName As String) As Array
        Try
            Dim _result As String()
            _result = PatientName.Split("|")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Private Function setdatatoC1(ByVal dt As DataTable)
        Try
            Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= C1RefillList.Styles.Add("CS_ComboList")
            Try
                If (C1RefillList.Styles.Contains("CS_ComboList")) Then
                    csComboList = C1RefillList.Styles("CS_ComboList")
                Else
                    csComboList = C1RefillList.Styles.Add("CS_ComboList")
                    With csComboList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                        '.ForeColor = Color.Black
                        '.BackColor = Color.GhostWhite
                        .DataType = GetType(String)
                        .ComboList = "..."
                    End With
                End If
            Catch ex As Exception
                csComboList = C1RefillList.Styles.Add("CS_ComboList")
                With csComboList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    '.ForeColor = Color.Black
                    '.BackColor = Color.GhostWhite
                    .DataType = GetType(String)
                    .ComboList = "..."
                End With
            End Try

            C1RefillList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always

            For i As Integer = 0 To dt.Rows.Count - 1
                With C1RefillList
                    .Rows.Add()
                    .SetCellStyle(i + 1, COL_Approve, .Styles("CS_ComboList"))
                    .SetCellStyle(i + 1, COL_Deny, .Styles("CS_ComboList"))
                    .SetCellStyle(i + 1, COL_Cancel, .Styles("CS_ComboList"))

                    .SetData(i + 1, COL_Approve, "")
                    .SetData(i + 1, COL_Deny, "")
                    .SetData(i + 1, COL_Cancel, "")

                    '.SetData(i + 1, COL_RxReferenceNumber, dt.Rows(i)(0))
                    .SetData(i + 1, COL_nMessageID, dt.Rows(i)(0))
                    .SetData(i + 1, COL_MessageID, dt.Rows(i)(0))
                    .SetData(i + 1, COL_RequestRepresentation, dt.Rows(i)(1))
                    .SetData(i + 1, COL_RequestType, dt.Rows(i)("RequestType"))
                    .SetData(i + 1, COL_RxTransactionId, dt.Rows(i)("RxTransactionID"))

                    .SetData(i + 1, COL_PatientName, Convert.ToString(dt.Rows(i)(2)).Trim)
                    .SetData(i + 1, COL_PatientGender, dt.Rows(i)(3)) ''this is called as Medication but in image it is DrugName
                    .SetData(i + 1, COL_PatientDOB, dt.Rows(i)(4))
                    .SetData(i + 1, COL_Medication, dt.Rows(i)(5))
                    Try
                        If dt.Rows(i)("DosageDescription") <> "" Then
                            .SetData(i + 1, COL_Quantity, dt.Rows(i)("DosageDescription"))
                        Else
                            .SetData(i + 1, COL_Quantity, dt.Rows(i)("Quantity"))
                        End If
                    Catch ex As Exception
                        .SetData(i + 1, COL_Quantity, dt.Rows(i)("Quantity"))
                    End Try

                    .SetData(i + 1, COL_PrescriptionDate, dt.Rows(i)(7))
                    .SetData(i + 1, COL_DateReceived, dt.Rows(i)(8))
                    .SetData(i + 1, COL_RefillQuantity, dt.Rows(i)(9))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("PatientID"))
                    .SetData(i + 1, COL_RefillQualifier, dt.Rows(i)(11))
                    .SetData(i + 1, COL_MessageID, dt.Rows(i)(12))
                    .SetData(i + 1, COL_PharmacyID, dt.Rows(i)(13))
                    .SetData(i + 1, COL_PatientLastName, dt.Rows(i)(14))
                    .SetData(i + 1, COL_RefillReqNDCCode, dt.Rows(i)(15)) ''dt.Rows(i)("sProductCode")) ''Refill request NDCCode
                    ''New Col Added
                    .SetData(i + 1, COL_dtWrittenDate, dt.Rows(i)("WrittenDate"))
                    .SetData(i + 1, COL_Notes, dt.Rows(i)("DrugNotes"))
                    .SetData(i + 1, COL_Drugstreangth, dt.Rows(i)("Strength"))
                    .SetData(i + 1, COL_PatAddr1, dt.Rows(i)("PatientAddress1"))
                    .SetData(i + 1, COL_PatAddr2, dt.Rows(i)("PatientAddress2"))
                    .SetData(i + 1, COL_PatCity, dt.Rows(i)("PatientCity"))
                    .SetData(i + 1, COL_Patstate, dt.Rows(i)("PatientState"))
                    .SetData(i + 1, COL_PatZip, dt.Rows(i)("PatientZipcode"))
                    .SetData(i + 1, COL_PatPhone, dt.Rows(i)("PatPhone"))
                    .SetData(i + 1, COL_PatFax, dt.Rows(i)("PatientFax"))
                End With
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.Initialize, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

#End Region

#Region "DataGridView Events"

    Private Sub dgChangeRequests_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgChangeRequests.CellContentClick
        Dim nRowIndex As Int32 = e.RowIndex

        Try
            If e.ColumnIndex = 0 Then
                dgChangeRequests.CommitEdit(DataGridViewDataErrorContexts.Commit)

                For Each row As DataGridViewRow In dgChangeRequests.Rows
                    If dgChangeRequests.Rows.IndexOf(row) <> nRowIndex Then
                        row.Cells(0).Value = False
                    End If
                Next

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgChangeRequests_CellFormatting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgChangeRequests.CellFormatting
        Dim SSRequestedMed As schema.RxChangeDispensedMedicationType = Nothing

        Try
            If dgChangeRequests.Rows(e.RowIndex).DataBoundItem IsNot Nothing AndAlso TypeOf (dgChangeRequests.Rows(e.RowIndex).DataBoundItem) Is schema.RxChangeDispensedMedicationType Then
                SSRequestedMed = DirectCast(dgChangeRequests.Rows(e.RowIndex).DataBoundItem, schema.RxChangeDispensedMedicationType)

                Select Case e.ColumnIndex
                    Case dgChangeRequests.Columns.IndexOf(colQuantity)
                        If SSRequestedMed.Quantity IsNot Nothing AndAlso SSRequestedMed.Quantity.Value IsNot Nothing Then
                            e.Value = SSRequestedMed.Quantity.Value
                        End If
                    Case dgChangeRequests.Columns.IndexOf(colSubstitution)
                        If SSRequestedMed.Substitutions IsNot Nothing Then
                            e.Value = IIf(SSRequestedMed.Substitutions = "0", "Yes", "No")
                        End If
                    Case dgChangeRequests.Columns.IndexOf(colDirections)
                        If SSRequestedMed.Directions IsNot Nothing Then
                            dgChangeRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = SSRequestedMed.Directions
                        End If
                    Case dgChangeRequests.Columns.IndexOf(colNotes)
                        If SSRequestedMed.Note IsNot Nothing Then
                            dgChangeRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = SSRequestedMed.Note
                        End If
                    Case dgChangeRequests.Columns.IndexOf(colDrug)
                        If SSRequestedMed.DrugDescription IsNot Nothing Then
                            dgChangeRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).ToolTipText = SSRequestedMed.DrugDescription
                        End If
                    Case dgChangeRequests.Columns.IndexOf(colDuration)
                        If SSRequestedMed.DaysSupply IsNot Nothing Then
                            e.Value = SSRequestedMed.DaysSupply + " Days"
                        End If
                    Case dgChangeRequests.Columns.IndexOf(colRefills)
                        If SSRequestedMed.Refills IsNot Nothing Then
                            e.Value = SSRequestedMed.Refills.Value
                        End If
                End Select
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SSRequestedMed = Nothing
        End Try
    End Sub

#End Region

#Region "Unmatched Patient"

    Private Function AttemptPatientID() As Long
        Dim returnedID As String = 0
        Dim nProviderID As Int64 = 0

        Dim sFirstName As String = String.Empty
        Dim sLastName As String = String.Empty
        Dim dtDateOfBirth As DateTime = Date.Now
        Dim sGender As String = String.Empty

        Dim sAddressLine1 As String = String.Empty
        Dim sAddressLine2 As String = String.Empty
        Dim sPatientTelephone As String = String.Empty
        Dim sCity As String = String.Empty
        Dim sState As String = String.Empty
        Dim sZipCode As String = String.Empty

        Dim sPatientFax As String = String.Empty
        Dim sMiddleName As String = String.Empty

        Try
            If SSChangeRequest IsNot Nothing Then

                If SSChangeRequest.Patient IsNot Nothing Then

                    If SSChangeRequest.Patient.Name IsNot Nothing Then
                        dtDateOfBirth = SSChangeRequest.Patient.DateOfBirth.Item

                        With SSChangeRequest.Patient.Name
                            sFirstName = .FirstName
                            sMiddleName = .MiddleName
                            sLastName = .LastName
                        End With

                        sGender = SSChangeRequest.Patient.Gender

                        If SSChangeRequest.Patient.Address IsNot Nothing Then
                            With SSChangeRequest.Patient.Address
                                sAddressLine1 = .AddressLine1
                                sAddressLine2 = .AddressLine2
                                sCity = .City
                                sState = .State
                                sZipCode = .ZipCode
                            End With
                        End If
                    End If

                    If SSChangeRequest.Patient.CommunicationNumbers IsNot Nothing AndAlso SSChangeRequest.Patient.CommunicationNumbers.Any() Then
                        Dim number As schema.CommunicationType = SSChangeRequest.Patient.CommunicationNumbers.FirstOrDefault(Function(p) p.Qualifier = "TE")
                        If number IsNot Nothing Then
                            sPatientTelephone = number.Number
                            number = Nothing
                        End If

                        number = SSChangeRequest.Patient.CommunicationNumbers.FirstOrDefault(Function(p) p.Qualifier = "FX")

                        If number IsNot Nothing Then
                            sPatientFax = number.Number
                            number = Nothing
                        End If
                    End If
                End If


                If trvPrescribers.SelectedNode IsNot Nothing Then
                    If dProviderID.ContainsKey(Convert.ToString(trvPrescribers.SelectedNode.Tag)) Then
                        nProviderID = dProviderID(Convert.ToString(trvPrescribers.SelectedNode.Tag))
                    End If
                End If

                Using frmUnmatchedPatient As New gloPatient.frmMapUnMatchPatients(sFirstName, sMiddleName, sLastName, dtDateOfBirth, sGender)

                    frmUnmatchedPatient.ShowDialog(IIf(IsNothing(frmUnmatchedPatient.Parent), Me, frmUnmatchedPatient.Parent))

                    If frmUnmatchedPatient.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.MatchPatient Then
                        returnedID = frmUnmatchedPatient.SelectedPatientId
                    ElseIf frmUnmatchedPatient.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.NewPatient Then
                        Dim _ConnectStr As String = GetConnectionString()

                        Using patReg As New gloPatient.frmSetupQuickPatient(sFirstName, _
                                                                            sLastName, _
                                                                            dtDateOfBirth, _
                                                                            sGender, _
                                                                            sAddressLine1, _
                                                                            sAddressLine2, _
                                                                            sPatientTelephone, _
                                                                            sCity, _
                                                                            sState, _
                                                                            sZipCode, _
                                                                            selectedPharmacyNCPDP, _
                                                                            _ConnectStr, _
                                                                            nProviderID, _
                                                                            sPatientFax, _
                                                                            sMiddleName, _
                                                                            True)
                            patReg.ShowDialog(IIf(IsNothing(patReg.Parent), Me, patReg.Parent))
                            returnedID = patReg.ReturnPatientID
                        End Using
                    ElseIf frmUnmatchedPatient.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.DenyRequest Then
                        DisplayDenialView(C1RefillList.RowSel)
                    ElseIf frmUnmatchedPatient.CurrentAction = gloPatient.frmMapUnMatchPatients.FormAction.Cancel Then
                        returnedID = 0
                    End If
                End Using
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return returnedID
    End Function

#End Region

#Region "DEA Schedule"
    Private Sub Set_EPCSProviderEnabled(ByVal nProviderId As Long)
        Dim dtProvider As DataTable = Nothing
        Try
            Dim oRxBusinessLayer As New RxBusinesslayer(0)
            dtProvider = oRxBusinessLayer.GetPatientProviderDetails(nProviderId)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    Dim strServiceLevel As String = ""
                    strServiceLevel = Convert.ToString(dtProvider.Rows(0)("sServiceLevel"))

                    If gblnEpcsEnabled = True Then  ''Is EPCS Enabled for clinic
                        ''To check Provider is EPCS enabled
                        If strServiceLevel <> "" Then
                            If Mid(strServiceLevel, 5, 1) = 1 Then
                                gbIsProviderEPCSEnable = True
                            Else
                                gbIsProviderEPCSEnable = False
                            End If
                        Else
                            gbIsProviderEPCSEnable = False
                        End If
                    Else
                        gbIsProviderEPCSEnable = False
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If IsNothing(dtProvider) = False Then
                dtProvider.Dispose()
                dtProvider = Nothing
            End If
        End Try
    End Sub

    Private Function ValidateEPCSData(ByRef ChangeRequest As schema.RxChangeRequest) As Boolean
        Dim blnIsValid As Boolean = True
        Dim PrescriberMessageEPCSeRx As String = ""
        Dim PatientMessageEPCSeRx As String = ""
        Dim sMessage As String = ""
        Try
            If Not IsNothing(ChangeRequest) Then

                '''''''''''''''''Prescriber Validation------------------------------------------------------------------

                If ChangeRequest.Prescriber IsNot Nothing Then
                    If ChangeRequest.Prescriber.Name.LastName.Trim.Length = 0 Then
                        PrescriberMessageEPCSeRx = "LastName,"
                    End If

                    If ChangeRequest.Prescriber.Name.FirstName.Trim.Length = 0 Then
                        PrescriberMessageEPCSeRx = "FirstName,"
                    End If
                Else
                    PrescriberMessageEPCSeRx = "LastName,"
                    PrescriberMessageEPCSeRx = "FirstName,"
                End If

                If PrescriberMessageEPCSeRx.Length > 0 Then
                    PrescriberMessageEPCSeRx = "Provider’s " & PrescriberMessageEPCSeRx & ""
                End If

                '''''''''Patient Validation-------------------------------------------------------------------------------------------------                 

                If ChangeRequest.Patient IsNot Nothing AndAlso ChangeRequest.Patient.Address IsNot Nothing Then
                    If ChangeRequest.Patient.Address.AddressLine1.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Address Line1,"
                    End If

                    If ChangeRequest.Patient.Address.City.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "City,"
                    End If

                    If ChangeRequest.Patient.Address.State.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "State,"
                    End If

                    If ChangeRequest.Patient.Address.ZipCode.Trim.Length = 0 Then
                        PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Zip,"
                    End If
                Else
                    PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Address Line1,"
                    PatientMessageEPCSeRx = PatientMessageEPCSeRx & "City,"
                    PatientMessageEPCSeRx = PatientMessageEPCSeRx & "State,"
                    PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Zip,"
                End If

                If PatientMessageEPCSeRx.Length > 0 Then
                    PatientMessageEPCSeRx = "Patient's " & PatientMessageEPCSeRx
                End If
                sMessage = PrescriberMessageEPCSeRx & PatientMessageEPCSeRx
                If sMessage.Trim.Length > 0 Then
                    sMessage = sMessage.Substring(0, sMessage.Length - 1)
                    System.Windows.Forms.MessageBox.Show("This Prescription refill cannot be approved because the following data is missing.  " & vbCrLf & sMessage.ToString & "." & vbCrLf & "Please deny Refill Request.", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    blnIsValid = False
                End If

            End If

            Return blnIsValid
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        End Try
    End Function
#End Region

End Class
