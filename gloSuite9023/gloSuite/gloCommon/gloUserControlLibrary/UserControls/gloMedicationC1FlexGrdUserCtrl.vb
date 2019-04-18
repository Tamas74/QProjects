Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports gloEMRGeneralLibrary.Glogeneral
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary



Public Class gloMedicationC1FlexGrdUserCtrl

    Dim strCmbMedStatusLastvalue As String = "" ''''''bug 4904 ---wil be used to save the last val of cmbo when we change the state 

    'Code for integrated RxMed Form
    Private _IsValidDrug As Boolean
    Private _RxBuisnessLayer As RxBusinesslayer
    Public formLock As Boolean = False
    ' Public Dv As DataView
    'Code for integrated RxMed Form
   




    ''''''To get the Patient Count in the DataView after Searching 

    Private _VisibleCount As Int16 = 0
    Public Event InfoButtonClicked(ByVal NDCCode As String, ByVal NDCCodeDesc As String)

    Public Event MedicationDoubleClicked(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event StripItemClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event btnMedRecClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event btnRecHistoryClick(ByVal sender As Object, ByVal e As System.EventArgs)
    ''''for C1 Flex Grid by sagar''''''''''''''''''''''''''''''''''
    Private COL_COUNT As Integer = 57
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
    Private COL_UPDATEDBy As Integer = 9 ''-- New Column Added & Rename Orignal Updated column to Reviewed By.
    Private COL_RENEWED As Integer = 10
    Private COL_FREQUENCY As Integer = 11
    Private COL_DURATION As Integer = 12
    Private COL_AMOUNT As Integer = 13
    Private COL_MEDICATIONDATE As Integer = 14
    Private COL_DOSAGE As Integer = 15
    Private COL_REASON As Integer = 16
    Private COL_mpid As Integer = 17
    Private COL_DRUGFORM As Integer = 18
    'Private COL_USERID As Integer = 16
    Private COL_ROUTE As Integer = 19
    'For De-Normalization
    Private COL_Narcotic As Integer = 20
    Private COL_NDCCode As Integer = 21
    Private COL_DRUGQTYQUALIFIER As Integer = 22
    Private COL_PBMSOURCENAME As Integer = 23
    'For De-Normalization

    Private COL_Rx_sRefills As Integer = 24
    Private COL_Rx_sNotes As Integer = 25
    Private COL_Rx_sMethod As Integer = 26
    Private COL_Rx_bMaySubstitute As Integer = 27
    Private COL_Rx_nDrugID As Integer = 28
    Private COL_Rx_blnflag As Integer = 29
    Private COL_Rx_sLotNo As Integer = 30
    Private COL_Rx_dtExpirationdate As Integer = 31
    Private COL_Rx_nProviderId As Integer = 32
    Private COL_Rx_sChiefComplaints As Integer = 33
    Private COL_Rx_sStatus As Integer = 34
    Private COL_Rx_sRxReferenceNumber As Integer = 35
    Private COL_Rx_sRefillQualifier As Integer = 36
    Private COL_Rx_nPharmacyId As Integer = 37
    Private COL_Rx_sNCPDPID As Integer = 38
    Private COL_Rx_nContactID As Integer = 39
    Private COL_Rx_sName As Integer = 40
    Private COL_Rx_sAddressline1 As Integer = 41
    Private COL_Rx_sAddressline2 As Integer = 42
    Private COL_Rx_sCity As Integer = 43
    Private COL_Rx_sState As Integer = 44
    Private COL_Rx_sZip As Integer = 45
    Private COL_Rx_sEmail As Integer = 46
    Private COL_Rx_sFax As Integer = 47
    Private COL_Rx_sPhone As Integer = 48
    Private COL_Rx_sServiceLevel As Integer = 49
    Private COL_Rx_sPrescriberNotes As Integer = 50
    Private COL_Rx_eRxStatus As Integer = 51
    Private COL_Rx_eRxStatusMessage As Integer = 52
    Private COL_RxMedDMSID As Integer = 53 ''''For CCHIT Medication Reconcilation store DMS ID 
    Private COL_RowNumber As Integer = 54 ''''coloumn will contain the item number with respect to medication collection
    Private COL_MEDICATION As Integer = 55
    Private COl_State As Integer = 56
    Private _MedBuisnessLayer As MedicationBusinessLayer
    'event is raised when we want to remove the custom control when the status of the combo box is changed
    Public Event cmbChangeRemoveControl()
    'event is raised when we want to make the Print/Fax button Visible/InVisible when we change the status of the combo box
    Public Event PrnFaxToggle(ByVal flag As Boolean)
    Public Event btnScanViewDoc(ByVal MedicationCol As Medications, ByVal btnScanViewDocumentText As String)

    Public Event MedicationItemDeleted()
    Public Threshold As Double
    Private _PatientID As Long


    Public isInfobuttonMenuAdded As Boolean = False
    Public Event InfoButtonDocumentClicked(ByVal templateCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourcetype As String)
    Public Event SaveMedication(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SaveRxMedStateCheck(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event btnSignedAgreementClick(ByVal sender As Object, ByVal e As System.EventArgs)
    'Code for integrated RxMed Form
    Public Property ValidDrug() As Boolean
        Get
            Return _IsValidDrug
        End Get
        Set(ByVal value As Boolean)
            _IsValidDrug = value
        End Set
    End Property
    Public Property RxBuisnessLayerObject() As RxBusinesslayer
        Get
            Return _RxBuisnessLayer
        End Get
        Set(ByVal value As RxBusinesslayer)
            _RxBuisnessLayer = value
        End Set
    End Property

    Private _RefillDisable As Boolean
    Public Property RefillDisable() As Boolean
        Get
            Return _RefillDisable
        End Get
        Set(ByVal value As Boolean)
            _RefillDisable = value
        End Set
    End Property



    Public _EducationMaterialEnabled As Boolean
    Public Property EducationMaterialEnabled() As Boolean
        Get
            Return _EducationMaterialEnabled
        End Get
        Set(ByVal value As Boolean)
            _EducationMaterialEnabled = value
        End Set
    End Property

    Public _AdvancedReferenceEnabled As Boolean
    Public Property AdvancedReferenceEnabled() As Boolean
        Get
            Return _AdvancedReferenceEnabled
        End Get
        Set(ByVal value As Boolean)
            _AdvancedReferenceEnabled = value
        End Set
    End Property

    Public _VisitIDaftersave As Long
    Public Property VisitIDaftersave() As Long
        Get
            Return _VisitIDaftersave
        End Get
        Set(ByVal value As Long)
            _VisitIDaftersave = value
        End Set
    End Property
    Public Property SenderID() As String
    Public Property FormularyID() As String
    Public Property CopayID() As String



    'Code for integrated RxMed Form


    'Code for integrated RxMed Form
    'code commented by dipak as not in use
    'Public Sub New(ByRef objRxBusinessLayer As RxBusinesslayer)
    '    MyBase.new()
    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    _RxBuisnessLayer = objRxBusinessLayer
    '    Dv = New DataView
    '    AddMenu()
    '    'SetFlexgridColumns()

    'End Sub
    'Code for integrated RxMed Form

    Public Sub New(ByVal PatientID As Long, ByRef objMedBusinessLayer As MedicationBusinessLayer, ByVal _formLock As Boolean, Optional ByRef objRxBusinessLayer As RxBusinesslayer = Nothing)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientID = PatientID
        _MedBuisnessLayer = objMedBusinessLayer

        'to Refill medication item to the Prescription collection
        _RxBuisnessLayer = objRxBusinessLayer
        formLock = _formLock
        '  Dv = New DataView
        'to Refill medication item to the Prescription collection
        If formLock = False Then
            AddCombo()
            'AddMenu()
        End If

        SetFlexgridColumns()
        _Flex.Rows.Count = 1
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub AddCombo()
        'cmbMedStatus.Dock = DockStyle.Fill
        cmbMedStatus.DropDownStyle = ComboBoxStyle.DropDownList
        'pnlCombo.Controls.Add(cmbMedStatus)

        cmbMedStatus.Items.Add("All")
        cmbMedStatus.Items.Add("Active")
        cmbMedStatus.Items.Add("Inactive")
        cmbMedStatus.Items.Add("Erroneous")
        cmbMedStatus.Items.Add("Cancelled")
        cmbMedStatus.Items.Add("Completed")
        cmbMedStatus.Items.Add("Discontinued")
        cmbMedStatus.Items.Add("Unknown")

        cmbMedStatus.Items.Add("Aborted")
        cmbMedStatus.Items.Add("Held")
        cmbMedStatus.Items.Add("Normal")

        cmbMedStatus.Items.Add("New")
        cmbMedStatus.Items.Add("Nullified")
        cmbMedStatus.Items.Add("Obsolete")
        cmbMedStatus.Items.Add("Suspended")

        'pnlCombo.Dock = DockStyle.Top
        cmbMedStatus.Visible = True
        pnlCombo.Visible = True
        cmbMedStatus.Text = "Active"
    End Sub

    Public Property MedBuisnessLayerObject() As MedicationBusinessLayer
        Get
            Return _MedBuisnessLayer
        End Get
        Set(ByVal value As MedicationBusinessLayer)
            _MedBuisnessLayer = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub AddMenu()
        Dim tlstripitem As ToolStripMenuItem
        tlstripitem = New ToolStripMenuItem
        Dim Separator1 As ToolStripSeparator
        Dim Separator2 As ToolStripSeparator

        '''''hide this menu. as per discussion with Pravin patil on 26 Aug 2009. fixed in gloEMR2.8 was asked to carry forward ot gloEMR5.0
        'tlstripitem.Text = "Delete All Medications"
        'tlstripitem.Tag = 1
        'cntListmenuStrip.Items.Add(tlstripitem)
        'AddHandler tlstripitem.Click, AddressOf StripItem_Click
        'tlstripitem = Nothing



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not cntListmenuStrip.Items.ContainsKey("MarkAsActive") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Mark as Active"
            tlstripitem.Name = "MarkAsActive"
            tlstripitem.Tag = 5
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(7)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If

        If Not cntListmenuStrip.Items.ContainsKey("MarkAsCompleted") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Mark as Completed"
            tlstripitem.Name = "MarkAsCompleted"
            tlstripitem.Tag = 8
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(7)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing

        End If

        If Not cntListmenuStrip.Items.ContainsKey("MarkAsDiscontinued") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Mark as Discontinued"
            tlstripitem.Name = "MarkAsDiscontinued"
            tlstripitem.Tag = 9
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(7)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If

        If Not cntListmenuStrip.Items.ContainsKey("MarkAsInactive") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Mark as Inactive"
            tlstripitem.Name = "MarkAsInactive"
            tlstripitem.Tag = 6
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(7)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If

        If Not cntListmenuStrip.Items.ContainsKey("MarkAsUnknown") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Mark as Unknown"
            tlstripitem.Name = "MarkAsUnknown"
            tlstripitem.Tag = 10
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(7)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not cntListmenuStrip.Items.ContainsKey("Separator2") Then
            Separator2 = New ToolStripSeparator()
            Separator2.Name = "Separator2"
            Separator2.Size = New System.Drawing.Size(97, 6)
            cntListmenuStrip.Items.Add(Separator2)
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not cntListmenuStrip.Items.ContainsKey("RenewRefill") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Renew/Refill"
            tlstripitem.Name = "RenewRefill"
            tlstripitem.Tag = 3
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(1)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not cntListmenuStrip.Items.ContainsKey("Separator1") Then
            Separator1 = New ToolStripSeparator()
            Separator1.Name = "Separator1"
            Separator1.Size = New System.Drawing.Size(97, 6)
            cntListmenuStrip.Items.Add(Separator1)
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not cntListmenuStrip.Items.ContainsKey("ModifyMedicationItem") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Modify Medication Item"
            tlstripitem.Name = "ModifyMedicationItem"
            tlstripitem.Tag = 4
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(8)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If

        If Not cntListmenuStrip.Items.ContainsKey("DeleteMedicationItem") Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Delete Medication Item"
            tlstripitem.Name = "DeleteMedicationItem"
            tlstripitem.Tag = 2
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(0)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If


    End Sub

    Public Sub RemoveMenu()

        If cntListmenuStrip.Items.ContainsKey("MarkAsActive") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("MarkAsActive")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("MarkAsActive")
        End If

        If cntListmenuStrip.Items.ContainsKey("MarkAsCompleted") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("MarkAsCompleted")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("MarkAsCompleted")
        End If

        If cntListmenuStrip.Items.ContainsKey("MarkAsDiscontinued") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("MarkAsDiscontinued")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("MarkAsDiscontinued")
        End If

        If cntListmenuStrip.Items.ContainsKey("MarkAsInactive") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("MarkAsInactive")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("MarkAsInactive")
        End If

        If cntListmenuStrip.Items.ContainsKey("MarkAsUnknown") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("MarkAsUnknown")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("MarkAsUnknown")
        End If

        If cntListmenuStrip.Items.ContainsKey("Separator2") Then
            'Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("Separator2")
            'toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("Separator2")
        End If

        If cntListmenuStrip.Items.ContainsKey("RenewRefill") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("RenewRefill")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("RenewRefill")
        End If

        If cntListmenuStrip.Items.ContainsKey("Separator1") Then
            'Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("Separator1")
            'toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("Separator1")
        End If

        If cntListmenuStrip.Items.ContainsKey("ModifyMedicationItem") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("ModifyMedicationItem")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("ModifyMedicationItem")
        End If

        If cntListmenuStrip.Items.ContainsKey("DeleteMedicationItem") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("DeleteMedicationItem")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("DeleteMedicationItem")
        End If

    End Sub

    Private Sub StripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim Prescriptiondate As DateTime
            Dim _ServerDatetime As DateTime
            Select Case CType(sender, ToolStripMenuItem).Tag

                Case 1
                    ''Not in use : Previously used for Delete all medications
                    Try
                        Dim loopcount As Int16
                        For loopcount = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                            If Not IsDBNull(_MedBuisnessLayer.MedicationCol.Item(loopcount)._PrescriptionId) AndAlso Not IsNothing(_MedBuisnessLayer.MedicationCol.Item(loopcount)._PrescriptionId) AndAlso (_MedBuisnessLayer.MedicationCol.Item(loopcount)._PrescriptionId) <> 0 Then
                                Dim dt As System.Data.DataTable = _MedBuisnessLayer.getServerTime(_MedBuisnessLayer.MedicationCol.Item(loopcount)._PrescriptionId)
                                If (IsNothing(dt) = False) Then


                                    If dt.Rows.Count > 0 Then
                                        If Not IsDBNull(dt.Rows(0)(1)) Then
                                            Prescriptiondate = CType(dt.Rows(0)(1), DateTime)
                                        End If
                                        If Not IsDBNull(dt.Rows(0)(0)) Then
                                            _ServerDatetime = CType(dt.Rows(0)(0), DateTime)
                                        End If
                                        If Not IsNothing(Prescriptiondate) AndAlso Not IsNothing(_ServerDatetime) Then
                                            If DateDiff(DateInterval.Minute, Prescriptiondate, _ServerDatetime) <= Threshold Then
                                                dt.Dispose()
                                                dt = Nothing
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                            End If
                        Next
                        If MessageBox.Show("Are you sure you want to delete medication?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True ''used for drug delete flag on rx, mx grid 

                            If _MedBuisnessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Add Then
                                _MedBuisnessLayer.MedicationCol.Clear()
                                _Flex.Rows.Count = 1
                            Else

                                '_MedBuisnessLayer.DeleteMedication()
                                '_MedBuisnessLayer.MedicationCol.Clear()
                                _Flex.Rows.Count = 1
                                cmbMedStatus.Text = "Active"
                            End If
                        End If

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DeleteMedication, gloAuditTrail.ActivityType.Delete, "Medication item was deleted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                Case 2

                    Dim i As Integer 'declared to get the value of current row
                    Try
                        i = _Flex.Row
                        If MessageBox.Show("Are you sure you want to delete medication item?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            If i > 0 Then
                                'If treeindex >= 0 Then
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True ''used for drug delete flag on rx, mx grid 
                                'If Not trPrescriptionDetails.SelectedNode Is trPrescriptionDetails.Nodes.Item(0) Then '''''by sagar start here
                                'Check if a valid row is selected
                                'If valid row is selected delete that row as well as item from Medication collection
                                Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                                For CollectionCnt As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                                    If FlexRowNo = _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).ItemNumber Then
                                        If Not IsNothing(_Flex.Rows.Item(0)) Then
                                            Dim key As Long
                                            Dim PatId As Long = 0
                                            Dim ProvId As Long = 0


                                            If Not IsDBNull(_MedBuisnessLayer.MedicationCol.Item(CollectionCnt)._PrescriptionId) AndAlso Not IsNothing(_MedBuisnessLayer.MedicationCol.Item(CollectionCnt)._PrescriptionId) AndAlso (_MedBuisnessLayer.MedicationCol.Item(CollectionCnt)._PrescriptionId) <> 0 Then ''_Flex.Row - 1
                                                Dim dt As System.Data.DataTable = _MedBuisnessLayer.getServerTime(_MedBuisnessLayer.MedicationCol.Item(CollectionCnt)._PrescriptionId) ''_Flex.Row - 1
                                                If (IsNothing(dt) = False) Then
                                                    If dt.Rows.Count > 0 Then
                                                        If Not IsDBNull(dt.Rows(0)(1)) Then
                                                            Prescriptiondate = CType(dt.Rows(0)(1), DateTime)
                                                        End If
                                                        If Not IsDBNull(dt.Rows(0)(0)) Then
                                                            _ServerDatetime = CType(dt.Rows(0)(0), DateTime)
                                                        End If
                                                        If Not IsNothing(Prescriptiondate) AndAlso Not IsNothing(_ServerDatetime) Then
                                                            If DateDiff(DateInterval.Minute, Prescriptiondate, _ServerDatetime) <= Threshold Then
                                                                MessageBox.Show("The medication cannot be deleted as it has not crossed the threshold limit.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                                dt.Dispose()
                                                                dt = Nothing
                                                                Exit Sub
                                                            End If
                                                        End If
                                                    End If
                                                    dt.Dispose()
                                                    dt = Nothing
                                                End If
                                            End If
                                            If _MedBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
                                                _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).State = "D"
                                                '_MedBuisnessLayer.MedicationCol.RemoveAt(CollectionCnt) ''''_Flex.Row - 1    delete from collection
                                                key = _Flex.GetData(_Flex.Row, COL_mpid)
                                                PatId = _Flex.GetData(_Flex.Row, COL_PATIENTID)
                                                If Not IsDBNull(_Flex.GetData(_Flex.Row, COL_Rx_nProviderId)) Then
                                                    ProvId = _Flex.GetData(_Flex.Row, COL_Rx_nProviderId)
                                                End If
                                                _Flex.Rows.Item(_Flex.Row).Visible = False
                                                Exit For ''''once item from collection and row from grid is deleted then exit for
                                            Else
                                                _MedBuisnessLayer.MedicationCol.RemoveAt(CollectionCnt) ''''_Flex.Row - 1    delete from collection
                                                _Flex.Rows.Remove(i)
                                                If _MedBuisnessLayer.MedicationCol.Count > CollectionCnt Then '' resolved Bug 73210
                                                    Dim flex_index As Int16 = i
                                                    For index As Int16 = CollectionCnt To _MedBuisnessLayer.MedicationCol.Count - 1
                                                        _MedBuisnessLayer.MedicationCol.Item(index).ItemNumber = index
                                                        _Flex.SetData(flex_index, COL_RowNumber, index)
                                                        flex_index = flex_index + 1
                                                    Next
                                                End If
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DeleteMedication, gloAuditTrail.ActivityType.Delete, "Medication item was deleted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                                                RaiseEvent MedicationItemDeleted()
                                                Exit For ''''once item from collection and row from grid is deleted then exit for
                                            End If
                                        End If
                                    End If
                                Next


                            End If

                        End If
                        RaiseEvent StripItemClick(sender, e)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                Case 3
                    Dim sNDCCode As String = ""
                    Dim nCheckDrugID As Int64 = 0
                    Dim mpid As Int32 = 0
                    Try
                        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True Then
                            MessageBox.Show("Please save previous transaction before refill request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        sNDCCode = Convert.ToString(_Flex.Item(_Flex.Row, COL_NDCCode))

                        If Not IsDBNull(_Flex.Item(_Flex.Row, COL_NDCCode)) Then
                            If Not _Flex.Item(_Flex.Row, COL_mpid) Is Nothing Then
                                If Not _Flex.Item(_Flex.Row, COL_mpid).ToString() = "" Then
                                    mpid = Convert.ToInt32(_Flex.Item(_Flex.Row, COL_mpid))
                                    sNDCCode = _RxBuisnessLayer.RetrieveRepresentative_NDC(sNDCCode, mpid)
                                    'Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                                    '    If sNDCCode = "" Then
                                    '        If ogloGSHelper.IsDrugExist(Convert.ToString(_Flex.Item(_Flex.Row, COL_NDCCode))) = False Then
                                    '            If Not _Flex.Item(_Flex.Row, COL_NDCCode).ToString().Contains("GLO") Then
                                    '                MessageBox.Show("Selected Drug no more available in drug master." & vbCrLf & "Please contact System Administrator.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '                Exit Sub
                                    '            End If
                                    '        End If
                                    '    End If
                                    'End Using
                                End If
                            End If
                            If sNDCCode = "" Then
                                sNDCCode = CType(_Flex.GetData(_Flex.Row, COL_NDCCode), String)
                                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                                    mpid = oDIBHelper.GetMarketedProductId(sNDCCode, mpid)
                                End Using
                            End If

                            Call FetchRefillDrugDetail(sNDCCode, mpid, _RxBuisnessLayer.CurrentVisitID, _RxBuisnessLayer.ProviderID, _RxBuisnessLayer.PharmacyId)

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationRefill, gloAuditTrail.ActivityType.Refill, "Medication item Refilled", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)

                        End If

                        RaiseEvent StripItemClick(sender, e)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                Case 4
                    '' Modify medications
                    RaiseEvent MedicationDoubleClicked(_Flex, e)
                Case 5
                    '' Mark as active
                    ChangeStatus("Active")
                Case 6
                    '' Mark as Inactive
                    ChangeStatus("Inactive")
                Case 8
                    '' Mark as Completed
                    ChangeStatus("Completed")
                Case 9
                    '' Mark as Discontinued
                    ChangeStatus("Discontinued")
                Case 10
                    '' Mark as Unknown
                    ChangeStatus("Unknown")
            End Select

        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error deleting medication"
            Throw objex

        End Try




    End Sub


    Public Function FetchRefillDrugDetail(ByVal ndc As String, ByVal mpid As Int32, ByVal visitID As Int64, ByVal providerID As Int64, ByVal pharmacyID As Int64) As Boolean
        Dim objRxDBLayer As New RxBusinesslayer(_PatientID)
        Try
            ''''''this function is called in case the Rx form is opened from Disease Management

            Dim objprescription As Prescription = objRxDBLayer.FetchRefillDrugDetailsRevised(pharmacyID, mpid, ndc)

            objprescription.VisitID = visitID
            objprescription.ProviderID = providerID

            'objRxDBLayer.FillDrugDetailsRevised(objprescription)

            If Not objprescription.Medication = "" Then


                objprescription.PrescriptionID = 0
                objprescription.IseRxed = 0
                If _RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                    objprescription.Prescriptiondate = _RxBuisnessLayer.PrescriptionDate    'Set date as Old Prescription Date
                End If

                If String.IsNullOrEmpty(objprescription.Dosage) Then
                    objprescription.Medication = CType(_Flex.Item(_Flex.Row, COL_MEDICATION), System.String)
                End If


                objprescription.PatientID = _PatientID 'globalPatient.gnPatientID
                objprescription.Dosage = CType(_Flex.Item(_Flex.Row, COL_DOSAGE), System.String)
                objprescription.Route = CType(_Flex.Item(_Flex.Row, COL_ROUTE), System.String)
                objprescription.Frequency = CType(_Flex.Item(_Flex.Row, COL_FREQUENCY), System.String)

                'Changes done against Surescript Compliance case #00441992  
                'whenever a drug is refill from medication to prescription since we do not have may substitute User interface in Medication custom control we will by default send refill qualifier as "R"
                objprescription.RefillQualifier = "R" ''GLO2011-0012936/ GLO2011-0015685 DCG - RX Err
                'Dim strRefills As String
                If Not IsDBNull(_Flex.Item(_Flex.Row, COL_Rx_sRefills)) Then
                    If CType(_Flex.Item(_Flex.Row, COL_Rx_sRefills), System.String) <> "" AndAlso objprescription.IsNarcotics <> 2 Then
                        objprescription.Refills = CType(_Flex.Item(_Flex.Row, COL_Rx_sRefills), System.String)
                    Else
                        objprescription.Refills = 0
                    End If
                Else
                    objprescription.Refills = 0
                End If

                objprescription.Duration = CType(_Flex.Item(_Flex.Row, COL_DURATION), System.String)
                objprescription.Startdate = Now.Date

                Dim amt As String = CType(_Flex.Item(_Flex.Row, COL_AMOUNT), System.String)

                If Not String.IsNullOrWhiteSpace(amt) Then
                    If Not IsNumeric(amt) Then
                        Dim qty As String() = amt.Split(" ")
                        If (qty.Length >= 1) Then
                            amt = qty(0)
                        End If
                    End If
                    objprescription.Amount = amt & " " & objprescription.PotencyUnit
                End If

                If Not IsNothing(objprescription.Amount) Then
                    If objprescription.Amount <> "" Then
                        If objprescription.Amount.Contains("Unspecified") Then
                            objprescription.Amount = objprescription.Amount.Trim.Replace("Unspecified", "") + objprescription.DosageForm
                        End If
                    End If
                End If

                objprescription.UserName = globalSecurity.gstrLoginName

                'If objprescription.Duration <> "" Then
                '    objprescription.Enddate = CalulateEndDate(objprescription.Startdate, objprescription.Duration)
                '    If objprescription.Enddate <> "12:00:00 AM" Then
                '        objprescription.CheckEndDate = True
                '    End If
                'End If

                objprescription.Notes = CType(_Flex.Item(_Flex.Row, COL_Rx_sNotes), System.String)
                objprescription.PrescriberNotes = CType(_Flex.Item(_Flex.Row, COL_Rx_sPrescriberNotes), System.String)

                If gloGeneral.clsgeneral.gblnDisableAllowSubstitution.HasValue Then
                    objprescription.Maysubstitute = gloGeneral.clsgeneral.gblnDisableAllowSubstitution.Value
                End If

                objprescription.Renewed = "Renewed" & " " & Now & " " & globalSecurity.gstrLoginName
                objprescription.UpdatedByUserName = globalSecurity.gstrLoginName

                If Not IsDBNull(_Flex.Item(_Flex.Row, COL_Rx_sMethod)) Then
                    If _Flex.Item(_Flex.Row, COL_Rx_sMethod) <> "" Then
                        If _Flex.Item(_Flex.Row, COL_Rx_sMethod) <> "eRx" Then
                            objprescription.Method = _Flex.Item(_Flex.Row, COL_Rx_sMethod)
                        Else
                            If objprescription.IsNarcotics > 0 Then
                                objprescription.Method = "Print"
                            Else
                                If objprescription.PhNCPDPID <> "" Then
                                    objprescription.Method = "eRx"
                                Else
                                    objprescription.Method = "Print"
                                End If
                            End If
                        End If
                    Else
                        If objprescription.IsNarcotics > 0 Then
                            objprescription.Method = "Print"
                        Else
                            If objprescription.PhNCPDPID <> "" Then
                                objprescription.Method = "eRx"
                            Else
                                objprescription.Method = "Print"
                            End If
                        End If
                    End If
                End If

                objprescription.OldDosage = CType(_Flex.Item(_Flex.Row, COL_DOSAGE), System.String)
                objprescription.DosageForm = CType(_Flex.Item(_Flex.Row, COL_DRUGFORM), System.String)
                objprescription.State = "A"



                _Flex.SetData(_Flex.Row, COl_State, "M")
                Dim nGridRowNo As Integer = _Flex.GetData(_Flex.RowSel, COL_RowNumber)
                Dim keyValString As String = DateTime.Now.Day.ToString() & DateTime.Now.Month.ToString() & DateTime.Now.Year.ToString() & DateTime.Now.Minute.ToString() & DateTime.Now.Millisecond.ToString()
                _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).CheckFlag = New tmpCheckFlag()
                _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).CheckFlag.flag = gloTmpFlag.RefillMedication
                objprescription.CheckFlag = New tmpCheckFlag()
                objprescription.CheckFlag.flag = gloTmpFlag.RefillMedication
                Dim keyValue As New KeyValuePair(Of Object, Object)(keyValString, _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).Status)
                _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).CheckFlag.keyVal = keyValue
                objprescription.CheckFlag.keyVal = keyValue
                _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).Status = "Discontinued"

                If _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).State = "A" Then
                    _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).State = "A"
                Else
                    _MedBuisnessLayer.MedicationCol.Item(nGridRowNo).State = "M"
                End If






                If Not _Flex.Item(_Flex.Row, COL_Rx_nDrugID) Is Nothing Then
                    If Not _Flex.Item(_Flex.Row, COL_Rx_nDrugID).ToString() = "" Then
                        objprescription.DrugID = Convert.ToInt64(_Flex.Item(_Flex.Row, COL_Rx_nDrugID))
                    Else
                        ''''''check if Drugid is = 0, if yes remove the drugID using NDCcode, if still the NDCcode is blank then remove the Drug id using SIG parameters and save the DrugID
                        Dim nDrugID As Int64 = 0
                        If Not IsDBNull(_Flex.Item(_Flex.Row, COL_Rx_nDrugID)) Then
                            If Convert.ToInt64(_Flex.Item(_Flex.Row, COL_Rx_nDrugID)) = 0 Then
                                If objprescription.NDCCode <> "" Then
                                    nDrugID = _RxBuisnessLayer.GetDrugIDByNDC(objprescription.NDCCode)
                                Else
                                    If objprescription.mpid <> 0 Then
                                        '''''get drugID using mpid parameter
                                        nDrugID = _RxBuisnessLayer.GetDrugIdBympid(objprescription.mpid)
                                    Else
                                        Dim sDrugName As String = objprescription.Medication
                                        Dim sDosage As String = objprescription.Dosage
                                        Dim sRoute As String = objprescription.Route
                                        Dim sFrequency As String = objprescription.Frequency
                                        Dim sDuration As String = objprescription.Duration
                                        nDrugID = _RxBuisnessLayer.GetDrugIDFromSig(sDrugName, sDosage, sRoute, sFrequency, sDuration)
                                    End If
                                End If
                            End If
                            objprescription.DrugID = nDrugID
                        Else
                            objprescription.DrugID = nDrugID
                        End If

                    End If
                Else
                    ''''''check if Drugid is = 0, if yes remove the drugID using NDCcode, if still the NDCcode is blank then remove the Drug id using SIG parameters and save the DrugID
                    Dim nDrugID As Int64 = 0
                    If Convert.ToInt64(_Flex.Item(_Flex.Row, COL_Rx_nDrugID)) = 0 Then
                        If objprescription.NDCCode <> "" Then
                            nDrugID = _RxBuisnessLayer.GetDrugIDByNDC(objprescription.NDCCode)
                        Else
                            If objprescription.mpid <> 0 Then
                                '''''get drugID using mpid parameter
                                nDrugID = _RxBuisnessLayer.GetDrugIdBympid(objprescription.mpid)
                            Else
                                Dim sDrugName As String = objprescription.Medication
                                Dim sDosage As String = objprescription.Dosage
                                Dim sRoute As String = objprescription.Route
                                Dim sFrequency As String = objprescription.Frequency
                                Dim sDuration As String = objprescription.Duration
                                nDrugID = _RxBuisnessLayer.GetDrugIDFromSig(sDrugName, sDosage, sRoute, sFrequency, sDuration)
                            End If
                        End If
                    End If
                    objprescription.DrugID = nDrugID
                End If


                If objprescription.DosageForm = "" Then
                    objprescription.DosageForm = _RxBuisnessLayer.GetDosageformCode(objprescription.NDCCode)
                End If
                objprescription.CPOEOrder = _MedBuisnessLayer.GetProviderIDForUser(globalSecurity.gnUserID)


                _RxBuisnessLayer.PrescriptionCol.Add(objprescription)
                _IsValidDrug = True
            Else
                MessageBox.Show("Selected drug NDC (" & ndc & ") is no longer available in drug master. Please search All Drugs for alternate therapy.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Dim objex As New PrescriptionException
            objex.ErrMessage = "Error fetching Drug Details."
            Throw objex
            Return False
        Finally
            objRxDBLayer.Dispose()
            objRxDBLayer = Nothing
        End Try
    End Function


    Public Sub ChangeStatus(ByVal newStatus As String)
        Dim i As Integer

        Try

            i = _Flex.Row

            If i > 0 Then
                Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)

                For CollectionCnt As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                    If FlexRowNo = _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).ItemNumber Then
                        If Not IsNothing(_Flex.Rows.Item(0)) Then
                            _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).Status = newStatus
                            SetFlexGridData(_MedBuisnessLayer.MedicationCol.Item(CollectionCnt).ItemNumber)
                            _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).UpdatedByUserName = _MedBuisnessLayer.GetCurrentUserName
                            _Flex.SetData(_Flex.Row, COL_UPDATEDBy, _MedBuisnessLayer.GetCurrentUserName)
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, "Changing Medication Status on right click : " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'Private Sub ChangeQuickStatus(ByVal dtNewStatus As DataTable)
    '    Try
    '        If Not IsNothing(dtNewStatus) Then
    '            For rCount As Integer = 0 To dtNewStatus.Rows.Count - 1
    '                For CollectionCnt As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
    '                    If dtNewStatus.Rows(rCount)(0).ToString() = _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).MedicationID Then
    '                        _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).Status = dtNewStatus.Rows(rCount)(1).ToString()
    '                        ' SetFlexGridData(_MedBuisnessLayer.MedicationCol.Item(CollectionCnt).ItemNumber)
    '                        '_Flex.SetData(rCount + 1, COL_STATUS, dtNewStatus.Rows(rCount)(1).ToString())
    '                        _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).State = "M"
    '                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
    '                        Exit For
    '                    End If
    '                Next
    '                For GridCnt As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
    '                    If dtNewStatus.Rows(rCount)(0).ToString() = _Flex.GetData(rCount + 1, COL_MEDICATIONID) Then
    '                        _Flex.SetData(rCount + 1, COL_STATUS, dtNewStatus.Rows(rCount)(1).ToString())
    '                        Exit For
    '                    End If
    '                Next
    '            Next
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, "Changing Medication Status on right click : " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    Private Sub btnQuickStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuickStatus.Click
        ' Dim bFlgIsSaved As Boolean = True
        Try
            cmbMedStatus.SelectedIndex = 1
            prevselecteditem = cmbMedStatus.SelectedItem
            SetMedicationStatus()

            For i As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                If _MedBuisnessLayer.MedicationCol.Item(i).State <> "U" Then
                    ' bFlgIsSaved = False
                    Exit Sub
                End If
            Next

            SetQuickStatus(GetUniqDrugsCollection())

            'If _Flex.Rows.Count > 1 Then
            '    Using ofrmViewQuickStatus As New frmViewQuickStatus(_Flex)
            '        If ofrmViewQuickStatus.ShowDialog() = DialogResult.OK Then
            '            ChangeQuickStatus(ofrmViewQuickStatus.DtMedlist)
            '            ofrmViewQuickStatus.DtMedlist = Nothing
            '            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
            '        End If
            '    End Using
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Quick Status Button :" & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub SetQuickStatus(ByVal Dv As DataView)
        Try
            If IsNothing(Dv) Then
                Exit Sub
            End If

            With _Flex

                Dim _Duration As Integer = 0
                Dim nDuration As String() = Nothing
                Dim tmpMedicationID As String = String.Empty
                Dim tmpStatus As String = String.Empty
                For i As Integer = 1 To .Rows.Count - 1
                    tmpMedicationID = String.Empty
                    tmpStatus = String.Empty

                    Dv.RowFilter = "MedicationID ='" & .GetData(i, COL_MEDICATIONID).ToString().Trim() & "'"
                    If Dv.Count > 0 Then

                        If .GetData(i, COL_DURATION).ToString().Trim() = "" Then
                            _Duration = 0
                        Else
                            nDuration = Nothing
                            nDuration = .GetData(i, COL_DURATION).ToString().Trim.Split(" ")
                            If nDuration.Length > 0 Then
                                If nDuration.Length = 2 Then
                                    If IsNumeric(nDuration(0)) Then
                                        If nDuration(1) = "Days" Then
                                            _Duration = nDuration(0)
                                        ElseIf nDuration(1) = "Weeks" Then
                                            _Duration = Convert.ToInt32(nDuration(0)) * 7
                                        ElseIf nDuration(1) = "Months" Then
                                            _Duration = DateDiff(DateInterval.Day, CType(.GetData(i, COL_STARTDATE), Date), CType(.GetData(i, COL_STARTDATE), Date).AddMonths(nDuration(0)))
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
                        If IsNothing(.GetData(i, COL_ENDDATE)) = False AndAlso Convert.ToString(.GetData(i, COL_ENDDATE)) <> "" Then

                            If _Duration = 0 Then

                                If DateTime.Now.Date <= CType(.GetData(i, COL_ENDDATE), Date) Then
                                    .SetData(i, COL_STATUS, "Active")
                                ElseIf CType(.GetData(i, COL_ENDDATE), Date).AddMonths(2) > DateTime.Now.Date Then
                                    .SetData(i, COL_STATUS, "Unknown")
                                Else
                                    .SetData(i, COL_STATUS, "Completed")
                                End If

                            Else
                                If DateTime.Now.Date <= CType(.GetData(i, COL_STARTDATE), Date).AddDays(_Duration) Then
                                    .SetData(i, COL_STATUS, "Active")
                                ElseIf CType(.GetData(i, COL_STARTDATE), Date).AddDays(_Duration).AddMonths(2) > DateTime.Now.Date Then
                                    .SetData(i, COL_STATUS, "Unknown")
                                Else
                                    .SetData(i, COL_STATUS, "Completed")
                                End If

                            End If

                        Else
                            .SetData(i, COL_STATUS, "Active")
                        End If
                        tmpMedicationID = .GetData(i, COL_MEDICATIONID).ToString().Trim()
                        tmpStatus = .GetData(i, COL_STATUS).ToString().Trim()

                        'Update Medbusiness layer collection
                        For CollectionCnt As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                            If tmpMedicationID = _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).MedicationID.ToString().Trim Then
                                _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).Status = tmpStatus
                                _MedBuisnessLayer.MedicationCol.Item(CollectionCnt).State = "M"
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True
                                Exit For
                            End If
                        Next

                    End If
                Next
            End With



            ' DtMedlist = dtTempList.Copy
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Quick Status Button :" & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Function GetUniqDrugsCollection() As DataView
        Dim dtList As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Dim dvDrugs As DataView = Nothing
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

            For rNo As Integer = 1 To _Flex.Rows.Count - 1
                With _Flex
                    dr = dtList.NewRow()
                    dr("MedicationID") = .GetData(rNo, COL_MEDICATIONID).ToString
                    dr("DrugName") = .GetData(rNo, COL_MEDICATION_NAME).ToString
                    dr("NDCCode") = .GetData(rNo, COL_NDCCode).ToString
                    dr("Startdate") = .GetData(rNo, COL_STARTDATE).ToString
                    dtList.Rows.Add(dr)
                End With
            Next

            Dim view As DataView = dtList.DefaultView
            view.Sort = "DrugName,Startdate Desc"
            dt = view.ToTable()

            dvDrugs = DeleteDuplicateFromDataTable(dt, "DrugName")

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
        Return dvDrugs
    End Function
    Protected Function DeleteDuplicateFromDataTable(ByVal dtDuplicate As DataTable,
                                      ByVal columnName As String) As DataView
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

    'retrieve the default pharmacy id against that patient
    Friend Function GetDefaultPatPhDetails(ByVal nPatientId As Long) As DataTable
        Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
        Dim PhDetails As DataTable = Nothing
        Try
            'Dim strquery As String = "select isnull(nPharmacyID,0) from Patient where npatientid = " & nPatientId & " "
            Dim strquery As String
            strquery = " select isnull(nContactId,0) as PharmacyId,ISNULL(sName,'') AS PharmacyName,ISNULL( sAddressLine1,'') AS AddressLine1 ," _
                        & " ISNULL(sAddressLine2,'') AS AddressLine2 ,ISNULL(sCity,'')AS City,ISNULL(sState,'')AS State,ISNULL(nContactId,0) AS ContactId," _
                        & " ISNULL(sZIP,'')AS Zip,ISNULL(sPhone,'')AS Phone,ISNULL(sFax,'')AS Fax,ISNULL(sNCPDPID,'')AS NCPDPID ,ISNULL(sEmail,'')AS Email," _
                        & " ISNULL(sServiceLevel,'')AS ServiceLevel" _
                        & " from Patient_DTL where nContactFlag =1 and nPatientID = " & nPatientId & " AND ISNULL(sPharmacyStatus,'') = 1"
            PhDetails = ogloEMRDatabase.GetDataTable_Query(strquery)

            Return PhDetails

        Catch ex As Exception
            Return PhDetails
        Finally
            'If Not IsNothing(PhDetails) Then
            '    PhDetails.Dispose()
            '    PhDetails = Nothing
            'End If
            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If
        End Try
    End Function

    Private Sub btnScanViewDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanViewDocument.Click
        Try
            '''''add the document id to the visitid present in the grid
            Dim nVisitId As Int64
            If _Flex.Rows.Count > 1 Then
                nVisitId = _Flex.GetData(_Flex.RowSel, COL_VISITID)
                RaiseEvent btnScanViewDoc(_MedBuisnessLayer.MedicationCol, btnScanViewDocument.Text)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub ClearRows()
        _Flex.Rows.Count = 1
    End Sub

    'Added code to get IsNarcotic Value for problem #801 against Problem Incident #75560: 00040363
    'Private Function GetNarcoticsFromDrugMst(ByVal mpid As Int32, ByVal ndccode As String) As Int64

    '    Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
    '    Dim oDrugType As gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest = Nothing
    '    Try
    '        Dim _strSQl As String = String.Empty
    '        Dim nIsNarcotics As Int64 = -1
    '        If mpid <> 0 Then
    '            _strSQl = "select isnull(nIsNarcotics,0) from Drugs_Mst where mpid = " & mpid
    '            nIsNarcotics = ogloEMRDatabase.GetDataValue(_strSQl, False)

    '        Else
    '            ''_strSQl = "select isnull(nIsNarcotics,0) from Drugs_Mst where sNDCcode = '" & ndccode & "'"
    '            oDrugType = New gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest
    '            nIsNarcotics = oDrugType.GetDrugInfo(ndccode)
    '        End If

    '        If (IsDBNull(nIsNarcotics)) Then
    '            Return 0
    '        Else
    '            Return nIsNarcotics
    '        End If



    '    Catch ex As Exception
    '        Dim objex As New PrescriptionException
    '        objex.ErrMessage = ""
    '        Throw objex
    '        Return 0
    '    Finally
    '        If Not IsNothing(ogloEMRDatabase) Then
    '            ogloEMRDatabase.Dispose()
    '            ogloEMRDatabase = Nothing
    '        End If
    '        If Not IsNothing(oDrugType) Then
    '            oDrugType.Dispose()
    '            oDrugType = Nothing
    '        End If
    '    End Try
    'End Function


    'Function added by Ashish for getting RxType for Formulary 3.0 changes as on 6th March 2015
    Private Function GetRxTypeOfDrug(ByVal mpid As Int32, ByVal NDCCode As String) As String
        Dim _gloEMRDatabase As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim sRxType As String = String.Empty
        Dim dt As DataTable = Nothing
        Try
            Dim _strsql As String = ""
            _strsql = "select top 1 isnull(RxType,'') as RxType from drugs_Mst where mpid = '" & mpid & "' and sNDCCode = '" & NDCCode & "'"

            dt = _gloEMRDatabase.GetDataTable_Query(_strsql)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)(0)) Then
                    sRxType = (CType(dt.Rows(0)(0), String))
                End If
            End If
            Return sRxType
        Catch ex As Exception
            Return ""
        Finally
            If Not IsNothing(_gloEMRDatabase) Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If

            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function


    Private Sub SetFlexgridColumns()
        'Dim cs As C1.Win.C1FlexGrid.CellStyle = Nothing

        'Try
        '    If (_Flex.Styles.Contains("MedHxRowStyle")) Then
        '        cs = _Flex.Styles("MedHxRowStyle")
        '    Else
        '        cs = _Flex.Styles.Add("MedHxRowStyle")
        '        cs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

        '    End If
        'Catch ex As Exception
        '    cs = _Flex.Styles.Add("MedHxRowStyle")
        '    cs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        'End Try



        ' cs.ForeColor = Color.Green
        With _Flex
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn ''as discussed commented to resolve the incident #00002884 
            .Redraw = False
            'C1FlexPrescription.SetCellCheck(1, 1, CheckEnum.Unchecked)
            .Cols(0).Width = 30

            '.Cols.Count = 2
            '.ExtendLastCol = True
            '.Tree.Column = 1

            'set properties of c1 grid
            '.Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width / 10
            'set column value

            .Cols(COL_MEDICATIONID).Width = 0 '_Width * 1
            .Cols(COL_VISITID).Width = 0 '_Width * 1
            .Cols(COL_PATIENTID).Width = 0 '_Width * 1
            .Cols(COL_MEDICATION_NAME).Width = _Width * 5

            'Infobutton
            .Cols(COl_INFOBUTTON).Width = _Width * 0.3

            .Cols(COL_MEDICATION).Width = 0 ''also called drug name
            .Cols(COL_DOSAGE).Width = 0 '_Width * 1.3 '_Width * 1
            .Cols(COL_ROUTE).Width = 0 ''_Width * 0.7
            .Cols(COL_DRUGFORM).Width = 0 ' _Width * 1.2
            .Cols(COL_FREQUENCY).Width = _Width * 1.8
            .Cols(COL_DURATION).Width = _Width * 1.1 '_Width * 1
            .Cols(COL_AMOUNT).Width = _Width * 1.1 ''also called Dispense
            .Cols(COL_STARTDATE).Width = _Width * 1.25
            .Cols(COL_STARTDATE).DataType = GetType(System.DateTime)
            .Cols(COL_STARTDATE).Format = "MM/dd/yyyy"

            .Cols(COL_ENDDATE).Width = _Width * 1.2

            .Cols(COL_ENDDATE).DataType = GetType(System.DateTime)
            .Cols(COL_ENDDATE).Format = "MM/dd/yyyy"

            .Cols(COL_LOGINNAME).Width = _Width * 1.3
            .Cols(COL_UPDATEDBy).Width = _Width * 1.3 '' New Column

            .Cols(COL_MEDICATIONDATE).Width = 0
            .Cols(COL_STATUS).Width = _Width * 1.0 '0 'changed in 6030 for Rx Feild changes
            .Cols(COL_REASON).Width = 0
            .Cols(COL_mpid).Width = 0 '_Width * 1
            '.Cols(COL_USERID).Width = 0
            .Cols(COL_RENEWED).Width = _Width * 2 '_Width * 1
            'For De-Normalization
            .Cols(COL_Narcotic).Width = 0
            .Cols(COL_NDCCode).Width = 0 '_Width * 2

            .Cols(COL_DRUGQTYQUALIFIER).Width = 0

            .Cols(COL_PBMSOURCENAME).Width = 0
            .Cols(COL_Rx_sRefills).Width = _Width * 1.0 '0 'changed in 6030 for Rx Feild changes
            .Cols(COL_Rx_sNotes).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sMethod).Width = _Width * 1.4 '0 'changed in 6030 for Rx Feild changes
            .Cols(COL_Rx_bMaySubstitute).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_nDrugID).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_blnflag).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_sLotNo).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_dtExpirationdate).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_nProviderId).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_sChiefComplaints).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sStatus).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sRxReferenceNumber).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sRefillQualifier).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_nPharmacyId).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sNCPDPID).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_nContactID).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sName).Width = _Width * 1.5
            .Cols(COL_Rx_sAddressline1).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sAddressline2).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sCity).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sState).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sZip).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sEmail).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sFax).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sPhone).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sServiceLevel).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sPrescriberNotes).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_eRxStatus).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_eRxStatusMessage).Width = 0 '_Width * 1.0
            .Cols(COL_RxMedDMSID).Width = 0 '_Width * 1.0
            .Cols(COL_RowNumber).Width = 0 ' _Width * 1
            .Cols(COl_State).Width = 0

            'set column header
            .SetData(0, COL_MEDICATIONID, "Medication ID")
            .SetData(0, COL_VISITID, "Visit ID")
            .SetData(0, COL_PATIENTID, "Patient ID")
            .SetData(0, COL_MEDICATION, "Drug Name") ''this is called as Medication but in image it is DrugName
            'Infobutton
            .SetData(0, COl_INFOBUTTON, "")
            .SetData(0, COL_MEDICATION_NAME, "Drug") 'changed in 6030 for Rx Feild changes
            .SetData(0, COL_FREQUENCY, "Patient Directions")
            .SetData(0, COL_AMOUNT, "Quantity")
            .SetData(0, COL_DOSAGE, "Dosage")
            .SetData(0, COL_ROUTE, "Route")
            .SetData(0, COL_DRUGFORM, "Drug Form")
            '.SetData(0, COL_FREQUENCY, "Frequency")
            .SetData(0, COL_FREQUENCY, "Patient Directions") 'changed in 6030 for Rx Feild changes
            .SetData(0, COL_DURATION, "Duration")
            .SetData(0, COL_STARTDATE, "Start Date")
            .SetData(0, COL_ENDDATE, "End Date")
            '.SetData(0, COL_AMOUNT, "Dispense") ''this is called as Amount but in image it is Dispense 
            .SetData(0, COL_AMOUNT, "Quantity") 'changed in 6030 for Rx Feild changes
            .SetData(0, COL_MEDICATIONDATE, "Medication Date")
            .SetData(0, COL_STATUS, "Status")
            .SetData(0, COL_REASON, "Reason")
            .SetData(0, COL_mpid, "Dispensible Drug ID")
            '.SetData(0, COL_LOGINNAME, "User")
            .SetData(0, COL_LOGINNAME, "Reviewed By")
            .SetData(0, COL_UPDATEDBy, "Updated By") '' New column added
            '.SetData(0, COL_USERID, "User ID")
            .SetData(0, COL_RENEWED, "Renewed/Changed")
            'For De-Normalization
            .SetData(0, COL_Narcotic, "Narcotic")
            .SetData(0, COL_NDCCode, "NDC Code")
            .SetData(0, COL_DRUGQTYQUALIFIER, "Drug Unit")
            .SetData(0, COL_PBMSOURCENAME, "PBM Name")
            'For De-Normalization
            '.SetData(0, COL_Rx_sRefills, "Rx_sRefills")
            .SetData(0, COL_Rx_sRefills, "Refills") 'changed in 6030 for Rx Feild changes
            .SetData(0, COL_Rx_sNotes, "Rx_sNotes")
            '.SetData(0, COL_Rx_sMethod, "Rx_sMethod")
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
            '.SetData(0, COL_Rx_sName, "Pharmacy Name") 'Pharmacy Name
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
            'set visiblity for column 
            .Cols(COL_MEDICATIONID).Visible = False
            .Cols(COL_VISITID).Visible = False '_Width * 1
            .Cols(COL_PATIENTID).Visible = False ')_Width * 1
            .Cols(COL_MEDICATION).Visible = False
            .Cols(COL_MEDICATION_NAME).Visible = True ''also called drug name
            'Infobutton
            If EducationMaterialEnabled Then
                .Cols(COl_INFOBUTTON).Visible = True
            Else
                .Cols(COl_INFOBUTTON).Visible = False
            End If

            .Cols(COL_DOSAGE).Visible = True
            .Cols(COL_ROUTE).Visible = True
            .Cols(COL_DRUGFORM).Visible = True
            .Cols(COL_FREQUENCY).Visible = True
            .Cols(COL_DURATION).Visible = True
            .Cols(COL_AMOUNT).Visible = True ''also called Dispense
            .Cols(COL_STARTDATE).Visible = True
            .Cols(COL_ENDDATE).Visible = True
            .Cols(COL_LOGINNAME).Visible = True
            .Cols(COL_RENEWED).Visible = True
            .Cols(COL_MEDICATIONDATE).Visible = False
            .Cols(COL_STATUS).Visible = True 'False
            .Cols(COL_REASON).Visible = False
            .Cols(COL_mpid).Visible = False
            '.Cols(COL_USERID).Visible = False
            'For De-Normalization
            .Cols(COL_Narcotic).Visible = False
            .Cols(COL_NDCCode).Visible = False 'True '
            .Cols(COL_DRUGQTYQUALIFIER).Visible = False
            .Cols(COL_PBMSOURCENAME).Visible = True
            'For De-Normalization
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
            .Cols(COL_Rx_sName).Visible = True 'Pharmacy Name
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

            ' set column editing properties.
            .Cols(COL_MEDICATIONID).AllowEditing = False
            .Cols(COL_VISITID).AllowEditing = False '_Width * 1
            .Cols(COL_PATIENTID).AllowEditing = False '_Width * 1
            .Cols(COL_MEDICATION).AllowEditing = False ''also called drug name
            .Cols(COL_MEDICATION_NAME).AllowEditing = False
            'infobutton
            .Cols(COl_INFOBUTTON).AllowResizing = False
            .Cols(COl_INFOBUTTON).AllowEditing = False

            .Cols(COL_DOSAGE).AllowEditing = False
            .Cols(COL_ROUTE).AllowEditing = False
            .Cols(COL_FREQUENCY).AllowEditing = False
            .Cols(COL_DURATION).AllowEditing = False
            .Cols(COL_AMOUNT).AllowEditing = False ''also called Dispense
            .Cols(COL_STARTDATE).AllowEditing = False
            .Cols(COL_ENDDATE).AllowEditing = False
            .Cols(COL_LOGINNAME).AllowEditing = False
            .Cols(COL_MEDICATIONDATE).AllowEditing = False
            .Cols(COL_STATUS).AllowEditing = False
            .Cols(COL_REASON).AllowEditing = False
            .Cols(COL_mpid).AllowEditing = False
            '.Cols(COL_USERID).AllowEditing = False
            .Cols(COL_RENEWED).AllowEditing = False
            'For De-Normalization
            .Cols(COL_Narcotic).AllowEditing = False
            .Cols(COL_NDCCode).AllowEditing = False
            .Cols(COL_DRUGFORM).AllowEditing = False
            .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False
            .Cols(COL_PBMSOURCENAME).AllowEditing = False
            'For De-Normalization

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
            .Cols(COL_Rx_sName).AllowEditing = False 'Pharmacy Name
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

            .ForeColor = Color.Black
            .Redraw = True
        End With
    End Sub

    ''' <summary>
    ''' use whenever u want to see all colums in grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFlexgridColumns_OpenAllCols()
        With _Flex
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn ''as discussed commented to resolve the incident #00002884 
            'C1FlexPrescription.SetCellCheck(1, 1, CheckEnum.Unchecked)
            .Cols(0).Width = 30

            '.Cols.Count = 2
            '.ExtendLastCol = True
            '.Tree.Column = 1

            'set properties of c1 grid
            '.Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width / 10
            'set column value

            .Cols(COL_MEDICATIONID).Width = _Width * 1
            .Cols(COL_VISITID).Width = _Width * 1
            .Cols(COL_PATIENTID).Width = _Width * 1
            .Cols(COL_MEDICATION).Width = _Width * 2 ''also called drug name
            .Cols(COL_DOSAGE).Width = _Width * 1.3 '_Width * 1
            .Cols(COL_ROUTE).Width = _Width * 0.7
            .Cols(COL_DRUGFORM).Width = _Width * 1.2
            .Cols(COL_FREQUENCY).Width = _Width * 1
            .Cols(COL_DURATION).Width = _Width * 1.1 '_Width * 1
            .Cols(COL_AMOUNT).Width = _Width * 1.1 ''also called Dispense
            .Cols(COL_STARTDATE).Width = _Width * 1.25
            .Cols(COL_ENDDATE).Width = _Width * 1.2
            .Cols(COL_LOGINNAME).Width = _Width * 1.0
            .Cols(COL_MEDICATIONDATE).Width = _Width * 1.0
            .Cols(COL_STATUS).Width = _Width * 1.0
            .Cols(COL_REASON).Width = _Width * 1.0
            .Cols(COL_mpid).Width = _Width * 1
            '.Cols(COL_USERID).Width = 0
            .Cols(COL_RENEWED).Width = _Width * 2 '_Width * 1
            'For De-Normalization
            .Cols(COL_Narcotic).Width = _Width * 1.0
            .Cols(COL_NDCCode).Width = _Width * 1.0

            .Cols(COL_DRUGQTYQUALIFIER).Width = _Width * 1.0

            'Dont show the formulary data columns
            .Cols(COL_PBMSOURCENAME).Width = _Width * 1.0

            'For De-Normalization
            .Cols(COL_Rx_sRefills).Width = _Width * 1.0
            .Cols(COL_Rx_sNotes).Width = _Width * 1.0
            .Cols(COL_Rx_sMethod).Width = _Width * 1.0
            .Cols(COL_Rx_bMaySubstitute).Width = 0
            .Cols(COL_Rx_nDrugID).Width = _Width * 1.0
            .Cols(COL_Rx_blnflag).Width = _Width * 1.0
            .Cols(COL_Rx_sLotNo).Width = _Width * 1.0
            .Cols(COL_Rx_dtExpirationdate).Width = _Width * 1.0
            .Cols(COL_Rx_nProviderId).Width = _Width * 1.0
            .Cols(COL_Rx_sChiefComplaints).Width = _Width * 1.0
            .Cols(COL_Rx_sStatus).Width = _Width * 1.0
            .Cols(COL_Rx_sRxReferenceNumber).Width = _Width * 1.0
            .Cols(COL_Rx_sRefillQualifier).Width = _Width * 1.0
            .Cols(COL_Rx_nPharmacyId).Width = _Width * 1.0
            .Cols(COL_Rx_sNCPDPID).Width = _Width * 1.0
            .Cols(COL_Rx_nContactID).Width = _Width * 1.0
            .Cols(COL_Rx_sName).Width = _Width * 1.5
            .Cols(COL_Rx_sAddressline1).Width = _Width * 1.0
            .Cols(COL_Rx_sAddressline2).Width = _Width * 1.0
            .Cols(COL_Rx_sCity).Width = _Width * 1.0
            .Cols(COL_Rx_sState).Width = _Width * 1.0
            .Cols(COL_Rx_sZip).Width = _Width * 1.0
            .Cols(COL_Rx_sEmail).Width = _Width * 1.0
            .Cols(COL_Rx_sFax).Width = _Width * 1.0
            .Cols(COL_Rx_sPhone).Width = _Width * 1.0
            .Cols(COL_Rx_sServiceLevel).Width = _Width * 1.0
            .Cols(COL_Rx_sPrescriberNotes).Width = _Width * 1.0
            .Cols(COL_Rx_eRxStatus).Width = _Width * 1.0
            .Cols(COL_Rx_eRxStatusMessage).Width = _Width * 1.0
            .Cols(COL_RxMedDMSID).Width = _Width * 1.0
            .Cols(COL_RowNumber).Width = _Width * 1.0
            .Cols(COl_State).Width = _Width * 1.0

            'set column header
            .SetData(0, COL_MEDICATIONID, "Medication ID")
            .SetData(0, COL_VISITID, "Visit ID")
            .SetData(0, COL_PATIENTID, "Patient ID")
            .SetData(0, COL_MEDICATION, "Drug Name") ''this is called as Medication but in image it is DrugName
            .SetData(0, COL_DOSAGE, "Dosage")
            .SetData(0, COL_ROUTE, "Route")
            .SetData(0, COL_DRUGFORM, "Drug Form")
            .SetData(0, COL_FREQUENCY, "Frequency")
            .SetData(0, COL_DURATION, "Duration")
            .SetData(0, COL_STARTDATE, "Start Date")
            .SetData(0, COL_ENDDATE, "End Date")
            .SetData(0, COL_AMOUNT, "Dispense") ''this is called as Amount but in image it is Dispense 
            .SetData(0, COL_MEDICATIONDATE, "Medication Date")
            .SetData(0, COL_STATUS, "Status")
            .SetData(0, COL_REASON, "Reason")
            .SetData(0, COL_mpid, "Dispensible Drug ID")
            .SetData(0, COL_LOGINNAME, "User")
            '.SetData(0, COL_USERID, "User ID")
            .SetData(0, COL_RENEWED, "Renewed/Changed")
            'For De-Normalization
            .SetData(0, COL_Narcotic, "Narcotic")
            .SetData(0, COL_NDCCode, "NDC Code")
            .SetData(0, COL_DRUGQTYQUALIFIER, "Drug Unit")
            .SetData(0, COL_PBMSOURCENAME, "PBM Name")
            'For De-Normalization
            .SetData(0, COL_Rx_sRefills, "Rx_sRefills")
            .SetData(0, COL_Rx_sNotes, "Rx_sNotes")
            .SetData(0, COL_Rx_sMethod, "Rx_sMethod")
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
            .SetData(0, COL_Rx_sName, "Pharmacy Name") 'Pharmacy Name
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
            'set visiblity for column 
            .Cols(COL_MEDICATIONID).Visible = True
            .Cols(COL_VISITID).Visible = True '_Width * 1
            .Cols(COL_PATIENTID).Visible = True '_Width * 1
            .Cols(COL_MEDICATION).Visible = True ''also called drug name
            .Cols(COL_DOSAGE).Visible = True
            .Cols(COL_ROUTE).Visible = True
            .Cols(COL_DRUGFORM).Visible = True
            .Cols(COL_FREQUENCY).Visible = True
            .Cols(COL_DURATION).Visible = True
            .Cols(COL_AMOUNT).Visible = True ''also called Dispense
            .Cols(COL_STARTDATE).Visible = True
            .Cols(COL_ENDDATE).Visible = True
            .Cols(COL_LOGINNAME).Visible = True
            .Cols(COL_RENEWED).Visible = True
            .Cols(COL_MEDICATIONDATE).Visible = True
            .Cols(COL_STATUS).Visible = True
            .Cols(COL_REASON).Visible = True
            .Cols(COL_mpid).Visible = True
            '.Cols(COL_USERID).Visible = False
            'For De-Normalization
            .Cols(COL_Narcotic).Visible = True
            .Cols(COL_NDCCode).Visible = True
            .Cols(COL_DRUGQTYQUALIFIER).Visible = True
            .Cols(COL_PBMSOURCENAME).Visible = True
            'For De-Normalization
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
            .Cols(COL_Rx_sName).Visible = True 'Pharmacy Name
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
            .Cols(COl_State).Visible = True

            ' set column editing properties.
            .Cols(COL_MEDICATIONID).AllowEditing = False
            .Cols(COL_VISITID).AllowEditing = False '_Width * 1
            .Cols(COL_PATIENTID).AllowEditing = False '_Width * 1
            .Cols(COL_MEDICATION).AllowEditing = False ''also called drug name
            .Cols(COL_DOSAGE).AllowEditing = False
            .Cols(COL_ROUTE).AllowEditing = False
            .Cols(COL_FREQUENCY).AllowEditing = False
            .Cols(COL_DURATION).AllowEditing = False
            .Cols(COL_AMOUNT).AllowEditing = False ''also called Dispense
            .Cols(COL_STARTDATE).AllowEditing = False
            .Cols(COL_ENDDATE).AllowEditing = False
            .Cols(COL_LOGINNAME).AllowEditing = False
            .Cols(COL_MEDICATIONDATE).AllowEditing = False
            .Cols(COL_STATUS).AllowEditing = False
            .Cols(COL_REASON).AllowEditing = False
            .Cols(COL_mpid).AllowEditing = False
            '.Cols(COL_USERID).AllowEditing = False
            .Cols(COL_RENEWED).AllowEditing = False
            'For De-Normalization
            .Cols(COL_Narcotic).AllowEditing = False
            .Cols(COL_NDCCode).AllowEditing = False
            .Cols(COL_DRUGFORM).AllowEditing = False
            .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False
            .Cols(COL_PBMSOURCENAME).AllowEditing = False
            'For De-Normalization

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
            .Cols(COL_Rx_sName).AllowEditing = False 'Pharmacy Name
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


            'Infobutton
            .SetData(0, COl_INFOBUTTON, "")
            .Cols(COl_INFOBUTTON).AllowEditing = False
            .Cols(COl_INFOBUTTON).Visible = True


            .ForeColor = Color.Black
        End With
    End Sub

    Private Sub RedesignFlexgridColumns()
        With _Flex
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn ''as discussed commented to resolve the incident #00002884 
            .Redraw = False
            'C1FlexPrescription.SetCellCheck(1, 1, CheckEnum.Unchecked)
            .Cols(0).Width = 30

            '.Cols.Count = 2
            '.ExtendLastCol = True
            '.Tree.Column = 1

            'set properties of c1 grid
            '.Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width / 10
            'set column value

            .Cols(COL_MEDICATIONID).Width = 0 '_Width * 1
            .Cols(COL_VISITID).Width = 0 '_Width * 1
            .Cols(COL_PATIENTID).Width = 0 '_Width * 1
            .Cols(COL_MEDICATION).Width = 0 '_Width * 2 ''also called drug name
            .Cols(COL_MEDICATION_NAME).Width = _Width * 5
            'Infobutton
            .Cols(COl_INFOBUTTON).Width = _Width * 0.3

            .Cols(COL_DOSAGE).Width = 0 '_Width * 1.3 '_Width * 1
            .Cols(COL_ROUTE).Width = 0 '_Width * 0.7
            .Cols(COL_DRUGFORM).Width = 0 ' _Width * 1.2
            .Cols(COL_FREQUENCY).Width = _Width * 1.8
            .Cols(COL_DURATION).Width = _Width * 1.1 '_Width * 1
            .Cols(COL_AMOUNT).Width = _Width * 1.1 ''also called Dispense
            .Cols(COL_STARTDATE).Width = _Width * 1.25
            .Cols(COL_STARTDATE).DataType = GetType(System.DateTime)
            .Cols(COL_STARTDATE).Format = "MM/dd/yyyy"

            .Cols(COL_ENDDATE).DataType = GetType(System.DateTime)
            .Cols(COL_ENDDATE).Format = "MM/dd/yyyy"

            .Cols(COL_ENDDATE).Width = _Width * 1.2
            .Cols(COL_LOGINNAME).Width = _Width * 1.0
            .Cols(COL_MEDICATIONDATE).Width = 0
            .Cols(COL_STATUS).Width = _Width * 1
            .Cols(COL_REASON).Width = 0
            .Cols(COL_mpid).Width = 0 '_Width * 1
            '.Cols(COL_USERID).Width = 0
            .Cols(COL_RENEWED).Width = _Width * 2 '_Width * 1
            'For De-Normalization
            .Cols(COL_Narcotic).Width = 0
            .Cols(COL_NDCCode).Width = 0

            .Cols(COL_DRUGQTYQUALIFIER).Width = 0
            .Cols(COL_PBMSOURCENAME).Width = _Width * 1.0
            'For De-Normalization
            .Cols(COL_Rx_sRefills).Width = _Width * 1.0
            .Cols(COL_Rx_sNotes).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sMethod).Width = _Width * 1.4
            .Cols(COL_Rx_bMaySubstitute).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_nDrugID).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_blnflag).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_sLotNo).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_dtExpirationdate).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_nProviderId).Width = 0 ' _Width * 1.0
            .Cols(COL_Rx_sChiefComplaints).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sStatus).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sRxReferenceNumber).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sRefillQualifier).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_nPharmacyId).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sNCPDPID).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_nContactID).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sName).Width = _Width * 1.5
            .Cols(COL_Rx_sAddressline1).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sAddressline2).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sCity).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sState).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sZip).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sEmail).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sFax).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sPhone).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sServiceLevel).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_sPrescriberNotes).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_eRxStatus).Width = 0 '_Width * 1.0
            .Cols(COL_Rx_eRxStatusMessage).Width = 0 '_Width * 1.0
            .Cols(COL_RxMedDMSID).Width = 0 '_Width * 1.0
            .Cols(COL_RowNumber).Width = 0 '_Width * 1
            .Cols(COl_State).Width = 0 '18
            'set column header
            .SetData(0, COL_MEDICATIONID, "Medication ID")
            .SetData(0, COL_VISITID, "Visit ID")
            .SetData(0, COL_PATIENTID, "Patient ID")
            .SetData(0, COL_MEDICATION, "Drug Name") ''this is called as Medication but in image it is DrugName
            .SetData(0, COL_MEDICATION_NAME, "Drug")
            'Infobutton
            .SetData(0, COl_INFOBUTTON, "")

            .SetData(0, COL_DOSAGE, "Dosage")
            .SetData(0, COL_ROUTE, "Route")
            .SetData(0, COL_DRUGFORM, "Drug Form")
            .SetData(0, COL_FREQUENCY, "Patient Directions")
            .SetData(0, COL_DURATION, "Duration")
            .SetData(0, COL_STARTDATE, "Start Date")
            .SetData(0, COL_ENDDATE, "End Date")
            .SetData(0, COL_AMOUNT, "Quantity") ''this is called as Amount but in image it is Dispense 
            .SetData(0, COL_MEDICATIONDATE, "Medication Date")
            .SetData(0, COL_STATUS, "Status")
            .SetData(0, COL_REASON, "Reason")
            .SetData(0, COL_mpid, "Dispensible Drug ID")
            .SetData(0, COL_LOGINNAME, "Reviewed By")
            .SetData(0, COL_UPDATEDBy, "Updated By") '' New column added
            '.SetData(0, COL_USERID, "User ID")
            .SetData(0, COL_RENEWED, "Renewed/Changed")
            'For De-Normalization
            .SetData(0, COL_Narcotic, "Narcotic")
            .SetData(0, COL_NDCCode, "NDC Code")
            .SetData(0, COL_DRUGQTYQUALIFIER, "Drug Unit")
            .SetData(0, COL_PBMSOURCENAME, "PBM Name")
            'For De-Normalization
            '.SetData(0, COL_Rx_sRefills, "Rx_sRefills")
            .SetData(0, COL_Rx_sRefills, "Refills")
            .SetData(0, COL_Rx_sNotes, "Rx_sNotes")
            '.SetData(0, COL_Rx_sMethod, "Rx_sMethod")
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
            .SetData(0, COL_Rx_sName, "Pharmacy") 'Pharmacy Name
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

            'set visiblity for column 
            .Cols(COL_MEDICATIONID).Visible = False
            .Cols(COL_VISITID).Visible = False '_Width * 1
            .Cols(COL_PATIENTID).Visible = False '_Width * 1
            .Cols(COL_MEDICATION).Visible = False ''also called drug name
            .Cols(COL_MEDICATION_NAME).Visible = True

            'Infobutton
            If EducationMaterialEnabled Then
                .Cols(COl_INFOBUTTON).Visible = True
            Else
                .Cols(COl_INFOBUTTON).Visible = False
            End If

            .Cols(COL_DOSAGE).Visible = False
            .Cols(COL_ROUTE).Visible = False
            .Cols(COL_DRUGFORM).Visible = False
            .Cols(COL_FREQUENCY).Visible = True
            .Cols(COL_DURATION).Visible = True
            .Cols(COL_AMOUNT).Visible = True ''also called Dispense
            .Cols(COL_STARTDATE).Visible = True
            .Cols(COL_ENDDATE).Visible = True
            .Cols(COL_LOGINNAME).Visible = True
            .Cols(COL_UPDATEDBy).Visible = True '' New Column
            .Cols(COL_RENEWED).Visible = True
            .Cols(COL_MEDICATIONDATE).Visible = False
            .Cols(COL_STATUS).Visible = True
            .Cols(COL_REASON).Visible = False
            .Cols(COL_mpid).Visible = False
            '.Cols(COL_USERID).Visible = False
            'For De-Normalization
            .Cols(COL_Narcotic).Visible = False
            .Cols(COL_NDCCode).Visible = False
            .Cols(COL_DRUGQTYQUALIFIER).Visible = False
            .Cols(COL_PBMSOURCENAME).Visible = True
            'For De-Normalization
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
            .Cols(COL_Rx_sName).Visible = True 'Pharmacy Name
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
            .Cols(COl_State).Visible = True
            ' set column editing properties.
            .Cols(COL_MEDICATIONID).AllowEditing = False
            .Cols(COL_VISITID).AllowEditing = False '_Width * 1
            .Cols(COL_PATIENTID).AllowEditing = False '_Width * 1
            .Cols(COL_MEDICATION).AllowEditing = False ''also called drug name
            .Cols(COL_MEDICATION_NAME).AllowEditing = False
            'Infobutton
            .Cols(COl_INFOBUTTON).AllowEditing = False
            .Cols(COL_DOSAGE).AllowEditing = False
            .Cols(COL_ROUTE).AllowEditing = False
            .Cols(COL_FREQUENCY).AllowEditing = False
            .Cols(COL_DURATION).AllowEditing = False
            .Cols(COL_AMOUNT).AllowEditing = False ''also called Dispense
            .Cols(COL_STARTDATE).AllowEditing = False
            .Cols(COL_ENDDATE).AllowEditing = False
            .Cols(COL_LOGINNAME).AllowEditing = False
            .Cols(COL_UPDATEDBy).AllowEditing = False '' New Column
            .Cols(COL_MEDICATIONDATE).AllowEditing = False
            .Cols(COL_STATUS).AllowEditing = False
            .Cols(COL_REASON).AllowEditing = False
            .Cols(COL_mpid).AllowEditing = False
            '.Cols(COL_USERID).AllowEditing = False
            .Cols(COL_RENEWED).AllowEditing = False
            'For De-Normalization
            .Cols(COL_Narcotic).AllowEditing = False
            .Cols(COL_NDCCode).AllowEditing = False
            .Cols(COL_DRUGFORM).AllowEditing = False
            .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False
            .Cols(COL_PBMSOURCENAME).AllowEditing = False
            'For De-Normalization

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
            .Cols(COL_Rx_sName).AllowEditing = False 'Pharmacy Name
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
            .ForeColor = Color.Black
            .Redraw = True

        End With
    End Sub

    'this will retrun the numner of rows present in the flex grid
    Public Function GetMedicationFlexRowCount() As Integer
        Dim MxFlexRwCount As Integer = 1
        Try
            If _Flex.Rows.Count > 1 Then
                MxFlexRwCount = _Flex.Rows.Count
                Return MxFlexRwCount
            Else
                Return MxFlexRwCount
            End If

        Catch ex As Exception
            Return MxFlexRwCount
        End Try
    End Function

    ''' <summary>
    ''' 'for CCHIT11 this wil return the doument id of present in the grid then the btn text is "View"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentId() As Long
        Try
            If _Flex.Rows.Count > 1 Then
                Dim RxMedDMSID As Long = 0
                For i As Integer = 1 To _Flex.Rows.Count
                    RxMedDMSID = _Flex.GetData(i, COL_RxMedDMSID)
                    If RxMedDMSID <> 0 Then
                        Return RxMedDMSID
                    End If
                Next
            End If
            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    'Add Row to Flexgrid and Set data in flexgrid for selected row after making changes to custom Medication control.
    Public Sub AddandSetFlexgridData(ByVal _Medication As Medication)
        Try
            If txtSearch.Text <> "" Then '''''bug 7079
                txtSearch.Text = ""
            End If


            Dim myFlexRow As C1.Win.C1FlexGrid.Row = _Flex.Rows.Add()
            Dim rowcount As Int16 = myFlexRow.Index
            _Flex.Select(myFlexRow.Index, 0)





            With _Flex

                .SetData(_Flex.RowSel, COL_MEDICATIONID, _Medication.MedicationID)
                _Flex.Select(myFlexRow.Index, 0)
                'MsgBox(_Flex.Row.ToString())
                .SetData(_Flex.RowSel, COL_VISITID, _Medication.VisitID)
                _Flex.Select(myFlexRow.Index, 0)
                'MsgBox(_Flex.Row.ToString())
                '.SetData(_Flex.RowSel, 3, _Prescription.PatientID)
                .SetData(_Flex.RowSel, COL_PATIENTID, _PatientID)
                _Flex.Select(myFlexRow.Index, 0)
                'globalPatient.gnPatientID)
                'MsgBox(_Flex.Row.ToString())
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_MEDICATION, _Medication.Medication) 'changed in 6030 for Rx Feild Change
                _Flex.Select(myFlexRow.Index, 0)
                'MsgBox(_Flex.Row.ToString())
                '_Flex.Select(myFlexRow.Index, 0)
                If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                    .SetData(_Flex.RowSel, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route)
                Else
                    .SetData(_Flex.RowSel, COL_MEDICATION_NAME, _Medication.Medication)
                End If

                _Flex.Select(myFlexRow.Index, 0)
                'MsgBox(_Flex.Row.ToString())
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_DOSAGE, _Medication.Dosage)
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_ROUTE, _Medication.Route)
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_FREQUENCY, _Medication.Frequency)
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_DURATION, _Medication.Duration)
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_AMOUNT, _Medication.Amount) 'dispense
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_MEDICATIONDATE, _Medication.Medicationdate)
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_STARTDATE, CType(_Medication.Startdate, Date)) '''''bug 7079
                _Flex.Select(myFlexRow.Index, 0)
                '_Flex.Select(myFlexRow.Index, 0)
                Dim _strEndDate As String = ""
                Dim odt As DateTime = Now
                Try
                    If (Date.TryParse(_Medication.Enddate.ToString(), odt) = False) Then
                        _strEndDate = ""
                    Else
                        _strEndDate = _Medication.Enddate
                    End If
                    If odt = Date.MinValue Then
                        _strEndDate = ""
                    End If
                Catch ex As Exception
                    _strEndDate = ""
                End Try

                If _strEndDate = "" Then
                    .SetData(_Flex.RowSel, COL_ENDDATE, Nothing)
                Else
                    .SetData(_Flex.RowSel, COL_ENDDATE, CType(_strEndDate, Date))
                End If

                '_Flex.Select(myFlexRow.Index, 0) 'CType(_Medication.Enddate, String)'''''bug 7079
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_STATUS, If(_Medication.Status <> "", _Medication.Status, "Active"))
                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_REASON, _Medication.Reason)
                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_mpid, _Medication.mpid)
                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_LOGINNAME, _Medication.UserName)

                .SetData(_Flex.RowSel, COL_UPDATEDBy, _Medication.UpdatedByUserName) '' New Column

                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                '.SetData(_Flex.RowSel, COL_USERID, _Medication.UserID)
                .SetData(_Flex.RowSel, COL_RENEWED, _Medication.Renewed)
                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                'For De-Normalization
                .SetData(_Flex.RowSel, COL_Narcotic, _Medication.IsNarcotics)
                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_NDCCode, _Medication.NDCCode)
                '_Flex.Select(myFlexRow.Index, 0)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_DRUGFORM, _Medication.DosageForm)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_DRUGQTYQUALIFIER, _Medication.StrengthUnit)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_PBMSOURCENAME, _Medication.PBMSourceName)
                _Flex.Select(myFlexRow.Index, 0)
                'For De-Normalization
                .SetData(_Flex.RowSel, COL_Rx_bMaySubstitute, _Medication.Rx_MaySubstitute)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_Rx_nDrugID, _Medication.Rx_DrugID)
                _Flex.Select(myFlexRow.Index, 0)

                .SetData(_Flex.RowSel, COL_Rx_sName, _Medication.Rx_PhName)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_Rx_sNotes, _Medication.Rx_Notes)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_Rx_sPrescriberNotes, _Medication.Rx_PrescriberNotes)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_Rx_sMethod, _Medication.Rx_Method)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_Rx_sRefills, _Medication.Rx_Refills)
                _Flex.Select(myFlexRow.Index, 0)
                '.Row = myFlexRow.Index

                ''''bug fix for 7882
                'Dim _ItemNumber As Integer
                'If _MedBuisnessLayer.MedicationCol.Count = 1 Then
                '    _ItemNumber = myFlexRow.Index
                '    _ItemNumber = _ItemNumber - 1

                'Else
                '    _ItemNumber = _MedBuisnessLayer.MedicationCol.Item(_MedBuisnessLayer.MedicationCol.Count - 2).ItemNumber
                '    _ItemNumber = _ItemNumber + 1

                'End If
                .SetData(_Flex.RowSel, COl_State, _Medication.State)
                _Flex.Select(myFlexRow.Index, 0)
                .SetData(_Flex.RowSel, COL_RowNumber, _Medication.ItemNumber)
                _Flex.Select(myFlexRow.Index, 0)
                '_Medication.ItemNumber = _ItemNumber ''the item number will keep track for collection item number that will correspond to row number in the grid
                ''''bug fix for 7882

                'Infobutton
                .SetCellImage(_Flex.RowSel, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)

                If _Medication.PBMSourceName <> "" Then
                    Dim cs As C1.Win.C1FlexGrid.CellStyle = Nothing

                    Try
                        If (_Flex.Styles.Contains("MedHxRowStyle")) Then
                            cs = _Flex.Styles("MedHxRowStyle")
                        Else
                            cs = _Flex.Styles.Add("MedHxRowStyle")
                            cs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

                        End If
                    Catch ex As Exception
                        cs = _Flex.Styles.Add("MedHxRowStyle")
                        cs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                    End Try

                    .Rows(_Flex.RowSel).Style = cs
                End If
                ' MsgBox(_Flex.Row.ToString())
                _Flex.Select(myFlexRow.Index, 0)

            End With
            _Flex.Row = _Flex.RowSel
            RowIndex = _Flex.Row
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try

    End Sub


    'Set data in flexgrid for selected row after making changes to custom Medication control.
    Public Sub SetFlexGridData(Optional ByVal SelectedMedColItem As Integer = 0, Optional ByVal dtPharmacyDetails As DataTable = Nothing)
        Dim _Medication As Medication
        _Medication = _MedBuisnessLayer.MedicationCol.Item(SelectedMedColItem)

        With _Flex

            .SetData(.Row, COL_MEDICATIONID, _Medication.MedicationID)
            .SetData(.Row, COL_VISITID, _Medication.VisitID)
            '.SetData(.Row, 3, _Prescription.PatientID)
            .SetData(.Row, COL_PATIENTID, _PatientID) 'globalPatient.gnPatientID)
            If _MedBuisnessLayer.FilterType = "All" Then
                If _Medication.Status = "Discontinued" Then
                    .SetData(.Row, COL_MEDICATION, _Medication.Medication & "(" & _Medication.Status & ")") ''drug name
                    If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                        .SetData(.Row, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route & "(" & _Medication.Status & ")")
                    Else
                        .SetData(.Row, COL_MEDICATION_NAME, _Medication.Medication & "(" & _Medication.Status & ")")
                    End If

                Else
                    .SetData(.Row, COL_MEDICATION, _Medication.Medication) ''drug name changed in 6030 for Rx Feild change
                    If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                        .SetData(.Row, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route)
                    Else
                        .SetData(.Row, COL_MEDICATION_NAME, _Medication.Medication)
                    End If
                End If
            Else
                .SetData(.Row, COL_MEDICATION, _Medication.Medication) ''drug name changed in 6030 for Rx Feild change
                If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                    .SetData(.Row, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route)
                Else
                    .SetData(.Row, COL_MEDICATION_NAME, _Medication.Medication)
                End If

            End If
            .SetData(.Row, COL_DOSAGE, _Medication.Dosage)
            .SetData(.Row, COL_ROUTE, _Medication.Route)
            .SetData(.Row, COL_FREQUENCY, _Medication.Frequency)
            .SetData(.Row, COL_DURATION, _Medication.Duration)
            .SetData(.Row, COL_AMOUNT, _Medication.Amount) ''dispense
            .SetData(.Row, COL_MEDICATIONDATE, _Medication.Medicationdate)
            .SetData(.Row, COL_STARTDATE, FormatDateTime(_Medication.Startdate, DateFormat.ShortDate))
            If _Medication.CheckEndDate = True Then
                .SetData(.Row, COL_ENDDATE, FormatDateTime(_Medication.Enddate, DateFormat.ShortDate))
            End If

            .SetData(.Row, COL_STATUS, _Medication.Status)
            .SetData(.Row, COL_REASON, _Medication.Reason)
            .SetData(.Row, COL_mpid, _Medication.mpid)
            .SetData(.Row, COL_LOGINNAME, _Medication.UserName)
            .SetData(.Row, COL_UPDATEDBy, _Medication.UpdatedByUserName) '' New Column
            '.SetData(.Row, COL_USERID, _Medication.UserID)
            .SetData(.Row, COL_RENEWED, _Medication.Renewed)
            'For De-Normalization
            .SetData(.Row, COL_Narcotic, _Medication.IsNarcotics)
            .SetData(.Row, COL_NDCCode, _Medication.NDCCode)
            .SetData(.Row, COL_DRUGFORM, _Medication.DosageForm)
            .SetData(.Row, COL_DRUGQTYQUALIFIER, _Medication.StrengthUnit)
            'For De-Normalization
            .SetData(.Row, COL_Rx_sRefills, _Medication.Rx_Refills)
            .SetData(.Row, COL_Rx_sNotes, _Medication.Rx_Notes)
            .SetData(.Row, COL_Rx_sMethod, _Medication.Rx_Method)
            .SetData(.Row, COL_Rx_sPrescriberNotes, _Medication.Rx_PrescriberNotes)

            '------------Added on 20090901
            'For Pharmacy
            If _Medication.Rx_PharmacyId <> 0 Then
                .SetData(.Row, COL_Rx_nPharmacyId, _Medication.Rx_PharmacyId)
                .SetData(.Row, COL_Rx_sNCPDPID, _Medication.Rx_NCPDPID)
                .SetData(.Row, COL_Rx_nContactID, _Medication.Rx_ContactID)
                .SetData(.Row, COL_Rx_sName, _Medication.Rx_PhName) 'Pharmacy name
                .SetData(.Row, COL_Rx_sAddressline1, _Medication.Rx_Addressline1)
                .SetData(.Row, COL_Rx_sAddressline2, _Medication.Rx_Addressline2)
                .SetData(.Row, COL_Rx_sCity, _Medication.Rx_City)
                .SetData(.Row, COL_Rx_sState, _Medication.Rx_State)
                .SetData(.Row, COL_Rx_sZip, _Medication.Rx_Zip)
                .SetData(.Row, COL_Rx_sEmail, _Medication.Rx_Email)
                .SetData(.Row, COL_Rx_sFax, _Medication.Rx_Fax)
                .SetData(.Row, COL_Rx_sPhone, _Medication.Rx_Phone)
                .SetData(.Row, COL_Rx_sServiceLevel, _Medication.Rx_ServiceLevel)
            Else
                If Not IsNothing(dtPharmacyDetails) Then
                    If dtPharmacyDetails.Rows.Count > 0 Then
                        .SetData(.Row, COL_Rx_sNCPDPID, dtPharmacyDetails.Rows(0)("sNCPDPID"))
                        .SetData(.Row, COL_Rx_nContactID, dtPharmacyDetails.Rows(0)("nContactID"))
                        .SetData(.Row, COL_Rx_sName, dtPharmacyDetails.Rows(0)("sName"))
                        .SetData(.Row, COL_Rx_sAddressline1, dtPharmacyDetails.Rows(0)("sAddressLine1"))
                        .SetData(.Row, COL_Rx_sAddressline2, dtPharmacyDetails.Rows(0)("sAddressLine2"))
                        .SetData(.Row, COL_Rx_sCity, dtPharmacyDetails.Rows(0)("sCity"))
                        .SetData(.Row, COL_Rx_sState, dtPharmacyDetails.Rows(0)("sState"))
                        .SetData(.Row, COL_Rx_sZip, dtPharmacyDetails.Rows(0)("sZIP"))
                        .SetData(.Row, COL_Rx_sEmail, dtPharmacyDetails.Rows(0)("sEmail"))
                        .SetData(.Row, COL_Rx_sFax, dtPharmacyDetails.Rows(0)("sFax"))
                        .SetData(.Row, COL_Rx_sPhone, dtPharmacyDetails.Rows(0)("sPhone"))
                        .SetData(.Row, COL_Rx_sServiceLevel, dtPharmacyDetails.Rows(0)("sServiceLevel"))

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sNCPDPID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sNCPDPID")) Then
                            _Medication.Rx_NCPDPID = dtPharmacyDetails.Rows(0)("sNCPDPID")
                        Else
                            _Medication.Rx_NCPDPID = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("nContactID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("nContactID")) Then
                            _Medication.Rx_ContactID = dtPharmacyDetails.Rows(0)("nContactID")
                        Else
                            _Medication.Rx_ContactID = 0
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sName")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sName")) Then
                            _Medication.Rx_PhName = dtPharmacyDetails.Rows(0)("sName")
                        Else
                            _Medication.Rx_PhName = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine1")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine1")) Then
                            _Medication.Rx_Addressline1 = dtPharmacyDetails.Rows(0)("sAddressLine1")
                        Else
                            _Medication.Rx_Addressline1 = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine2")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine2")) Then
                            _Medication.Rx_Addressline2 = dtPharmacyDetails.Rows(0)("sAddressLine2")
                        Else
                            _Medication.Rx_Addressline2 = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sCity")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sCity")) Then
                            _Medication.Rx_City = dtPharmacyDetails.Rows(0)("sCity")
                        Else
                            _Medication.Rx_City = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sState")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sState")) Then
                            _Medication.Rx_State = dtPharmacyDetails.Rows(0)("sState")
                        Else
                            _Medication.Rx_State = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sZIP")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sZIP")) Then
                            _Medication.Rx_Zip = dtPharmacyDetails.Rows(0)("sZIP")
                        Else
                            _Medication.Rx_Zip = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sEmail")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sEmail")) Then
                            _Medication.Rx_Email = dtPharmacyDetails.Rows(0)("sEmail")
                        Else
                            _Medication.Rx_Email = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sFax")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _Medication.Rx_Fax = dtPharmacyDetails.Rows(0)("sFax")
                        Else
                            _Medication.Rx_Fax = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sPhone")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _Medication.Rx_Phone = dtPharmacyDetails.Rows(0)("sPhone")
                        Else
                            _Medication.Rx_Phone = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sServiceLevel")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sServiceLevel")) Then
                            _Medication.Rx_ServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                        Else
                            _Medication.Rx_ServiceLevel = ""
                        End If

                    End If
                Else
                    If _Medication.Rx_NCPDPID <> "" Then
                        .SetData(.Row, COL_Rx_sNCPDPID, _Medication.Rx_NCPDPID)
                    Else
                        .SetData(.Row, COL_Rx_sNCPDPID, "")
                    End If
                    If _Medication.Rx_ContactID <> 0 Then
                        .SetData(.Row, COL_Rx_nContactID, _Medication.Rx_ContactID)
                    Else
                        .SetData(.Row, COL_Rx_nContactID, 0)
                    End If
                    If _Medication.Rx_PhName <> "" Then ''''bug fix for 6645
                        .SetData(.Row, COL_Rx_sName, _Medication.Rx_PhName)
                    Else
                        .SetData(.Row, COL_Rx_sName, "")
                    End If
                    If _Medication.Rx_Addressline1 <> "" Then
                        .SetData(.Row, COL_Rx_sAddressline1, _Medication.Rx_Addressline1)
                    Else
                        .SetData(.Row, COL_Rx_sAddressline1, "")
                    End If
                    If _Medication.Rx_Addressline2 <> "" Then
                        .SetData(.Row, COL_Rx_sAddressline2, _Medication.Rx_Addressline2)
                    Else
                        .SetData(.Row, COL_Rx_sAddressline2, "")
                    End If
                    If _Medication.Rx_City <> "" Then
                        .SetData(.Row, COL_Rx_sCity, _Medication.Rx_City)
                    Else
                        .SetData(.Row, COL_Rx_sCity, "")
                    End If
                    If _Medication.Rx_State <> "" Then
                        .SetData(.Row, COL_Rx_sState, _Medication.Rx_State)
                    Else
                        .SetData(.Row, COL_Rx_sState, "")
                    End If
                    If _Medication.Rx_Zip <> "" Then
                        .SetData(.Row, COL_Rx_sZip, _Medication.Rx_Zip)
                    Else
                        .SetData(.Row, COL_Rx_sZip, "")
                    End If
                    If _Medication.Rx_Email <> "" Then
                        .SetData(.Row, COL_Rx_sEmail, _Medication.Rx_Email)
                    Else
                        .SetData(.Row, COL_Rx_sEmail, "")
                    End If
                    If _Medication.Rx_Fax <> "" Then
                        .SetData(.Row, COL_Rx_sFax, _Medication.Rx_Fax)
                    Else
                        .SetData(.Row, COL_Rx_sFax, "")
                    End If
                    If _Medication.Rx_Phone <> "" Then
                        .SetData(.Row, COL_Rx_sPhone, _Medication.Rx_Phone)
                    Else
                        .SetData(.Row, COL_Rx_sPhone, "")
                    End If
                    If _Medication.Rx_ServiceLevel <> "" Then
                        .SetData(.Row, COL_Rx_sServiceLevel, _Medication.Rx_ServiceLevel)
                    Else
                        .SetData(.Row, COL_Rx_sServiceLevel, "")
                    End If

                End If
                'For Pharmacy
            End If
            If _Medication.State = "U" Then
                _Medication.State = "M"
            End If
            .SetData(.Row, COl_State, _Medication.State)
            '--------------

        End With
    End Sub

    ''for CCHIT add DMSID to Medication items for which we view DMS document
    Public Sub SetFlexGridData(ByVal RxMedDMSId As Long)
        Try


            With _Flex
                If _Flex.Rows.Count > 1 Then
                    Dim rwCnt As Int16
                    For rwCnt = 1 To _Flex.Rows.Count - 1
                        _Flex.SetData(rwCnt, COL_RxMedDMSID, RxMedDMSId)
                    Next

                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    'added optional _FlagReload in 6040 for reloading the grid after save button click and set state of the row to "Unchanged"(U) 
    Public Sub BindFlexgrid(Optional ByVal _FlagReload As Boolean = False)
        Dim i As Int16
        Dim _Medication As Medication

        '''''Added code to resolve bug 9795
        If Not IsNothing(_Flex.DataSource) Then

            _Flex.DataSource = Nothing
            SetFlexgridColumns()
        End If
        '''''Added code to resolve bug 9795
        _Flex.Redraw = False
        _Flex.Rows.Count = 1

        For i = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
            _Medication = _MedBuisnessLayer.MedicationCol.Item(i)
            With _Flex
                .Rows.Add()

                .SetData(i + 1, COL_MEDICATIONID, _Medication.MedicationID)
                .SetData(i + 1, COL_VISITID, _Medication.VisitID)
                .SetData(i + 1, COL_PATIENTID, _PatientID) 'globalPatient.gnPatientID)
                If _MedBuisnessLayer.FilterType = "All" Then
                    If _Medication.Status = "Discontinued" Then
                        .SetData(i + 1, COL_MEDICATION, _Medication.Medication & "(" & _Medication.Status & ")")
                        If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                            .SetData(i + 1, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route & "(" & _Medication.Status & ")")
                        Else
                            .SetData(i + 1, COL_MEDICATION_NAME, _Medication.Medication & "(" & _Medication.Status & ")")
                        End If
                    Else
                        .SetData(i + 1, COL_MEDICATION, _Medication.Medication)
                        If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                            .SetData(i + 1, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route)
                        Else
                            .SetData(i + 1, COL_MEDICATION_NAME, _Medication.Medication)
                        End If
                    End If
                Else
                    .SetData(i + 1, COL_MEDICATION, _Medication.Medication)
                    If _Medication.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Medication.Route) Then
                        .SetData(i + 1, COL_MEDICATION_NAME, _Medication.Medication & " " & _Medication.Route)
                    Else
                        .SetData(i + 1, COL_MEDICATION_NAME, _Medication.Medication)
                    End If
                End If
                .SetData(i + 1, COL_DOSAGE, _Medication.Dosage)
                .SetData(i + 1, COL_ROUTE, _Medication.Route)
                .SetData(i + 1, COL_FREQUENCY, _Medication.Frequency)
                .SetData(i + 1, COL_DURATION, _Medication.Duration)
                .SetData(i + 1, COL_AMOUNT, _Medication.Amount)
                .SetData(i + 1, COL_MEDICATIONDATE, _Medication.Medicationdate)

                .SetData(i + 1, COL_STARTDATE, _Medication.Startdate)

                Dim _strEndDate As String = ""
                Dim odt As DateTime = Now
                Try
                    If (Date.TryParse(_Medication.Enddate.ToString(), odt) = False) Then
                        _strEndDate = ""
                    Else
                        _strEndDate = _Medication.Enddate
                    End If
                    If odt = Date.MinValue Then
                        _strEndDate = ""
                    End If
                Catch ex As Exception
                    _strEndDate = ""
                End Try

                If _strEndDate = "" Then
                    .SetData(i + 1, COL_ENDDATE, Nothing)
                Else
                    .SetData(i + 1, COL_ENDDATE, CType(_Medication.Enddate, Date))
                End If

                ValidateAndSetDate(i + 1) '''''Added code to resolve bug 9795

                .SetData(i + 1, COL_STATUS, If(_Medication.Status <> "", _Medication.Status, "Active"))
                .SetData(i + 1, COL_REASON, _Medication.Reason)
                .SetData(i + 1, COL_mpid, _Medication.mpid)
                .SetData(i + 1, COL_LOGINNAME, _Medication.UserName)
                .SetData(i + 1, COL_UPDATEDBy, _Medication.UpdatedByUserName) '' New Column
                '.SetData(i + 1, COL_USERID, _Medication.UserID)
                .SetData(i + 1, COL_RENEWED, _Medication.Renewed)
                'For De-Normalization
                .SetData(i + 1, COL_Narcotic, _Medication.IsNarcotics)
                .SetData(i + 1, COL_NDCCode, _Medication.NDCCode)
                .SetData(i + 1, COL_DRUGFORM, _Medication.DosageForm)
                .SetData(i + 1, COL_DRUGQTYQUALIFIER, _Medication.StrengthUnit)
                .SetData(i + 1, COL_PBMSOURCENAME, _Medication.PBMSourceName)
                'For De-Normalization

                .SetData(i + 1, COL_Rx_sRefills, _Medication.Rx_Refills)
                .SetData(i + 1, COL_Rx_sNotes, _Medication.Rx_Notes)
                .SetData(i + 1, COL_Rx_sMethod, _Medication.Rx_Method)
                .SetData(i + 1, COL_Rx_bMaySubstitute, _Medication.Rx_MaySubstitute)
                .SetData(i + 1, COL_Rx_nProviderId, _Medication.Rx_ProviderId)
                .SetData(i + 1, COL_Rx_nPharmacyId, _Medication.Rx_PharmacyId)
                .SetData(i + 1, COL_Rx_sNCPDPID, _Medication.Rx_NCPDPID)
                .SetData(i + 1, COL_Rx_nContactID, _Medication.Rx_ContactID)
                .SetData(i + 1, COL_Rx_sName, _Medication.Rx_PhName)
                .SetData(i + 1, COL_Rx_sPrescriberNotes, _Medication.Rx_PrescriberNotes)
                .SetData(i + 1, COL_Rx_eRxStatus, _Medication.Rx_eRxStatus)
                .SetData(i + 1, COL_RxMedDMSID, _Medication.RxMedDMSID)

                .SetData(i + 1, COL_RowNumber, _Medication.ItemNumber)
                '_Medication.ItemNumber = i ''the item number will keep track for collection item number that will correspond to row number in the grid
                If _FlagReload = True Then
                    .SetData(i + 1, COl_State, "U")
                Else
                    .SetData(i + 1, COl_State, _Medication.State)
                End If

                'Infobutton
                .SetCellImage(i + 1, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)
                If _Medication.PBMSourceName <> "" Then
                    Dim cs As C1.Win.C1FlexGrid.CellStyle = Nothing

                    Try
                        If (_Flex.Styles.Contains("MedHxRowStyle")) Then
                            cs = _Flex.Styles("MedHxRowStyle")
                        Else
                            cs = _Flex.Styles.Add("MedHxRowStyle")
                            cs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

                        End If
                    Catch ex As Exception
                        cs = _Flex.Styles.Add("MedHxRowStyle")
                        cs.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                    End Try

                    .Rows(i + 1).Style = cs
                    '.SetCellStyle(i + 1, COL_MEDICATION_NAME, cs)
                    '.SetCellStyle(i + 1, COL_MEDICATION, cs)
                End If
            End With
        Next
        _Flex.Redraw = True
        For i = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
            If _MedBuisnessLayer.MedicationCol.Item(i).RxMedDMSID <> 0 Then
                SetTextViewDocument()
            End If
        Next

    End Sub

    Public Sub AddMedhistoryData(ByVal oRxHubInterface As gloRxHub.ClsPatient)
        Dim flexRwCnt As Int16 = 0
        Try
            If _Flex.Rows.Count > 0 Then ''''if there are more rows other than header row
                flexRwCnt = _Flex.Rows.Count ''''this will contain the items that are already present in the flex grid along with the header row
                Dim _sVisitid As String = "0"
                If _MedBuisnessLayer.MedicationCol.Count > 0 Then
                    _sVisitid = _MedBuisnessLayer.MedicationCol.Item(0).VisitID.ToString
                Else
                    _sVisitid = _Flex.GetData(1, COL_VISITID)
                End If


                If oRxHubInterface.Medication.Count > 0 Then

                    For cnt As Integer = 0 To oRxHubInterface.Medication.Count - 1
                        _Flex.Rows.Add()
                        _Flex.SetData(flexRwCnt, COL_MEDICATIONID, 0)
                        _Flex.SetData(flexRwCnt, COL_VISITID, _sVisitid)
                        _Flex.SetData(flexRwCnt, COL_PATIENTID, _PatientID)
                        _Flex.SetData(flexRwCnt, COL_MEDICATION, oRxHubInterface.Medication.Item(cnt).MedicationDrug.DrugName)
                        _Flex.SetData(flexRwCnt, COL_DOSAGE, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Dosage) ''''to be removed
                        _Flex.SetData(flexRwCnt, COL_ROUTE, "")
                        _Flex.SetData(flexRwCnt, COL_FREQUENCY, "")
                        _Flex.SetData(flexRwCnt, COL_DURATION, oRxHubInterface.Medication.Item(cnt).MedicationDrug.DaysSupply)
                        _Flex.SetData(flexRwCnt, COL_AMOUNT, oRxHubInterface.Medication.Item(cnt).MedicationDrug.DrugQuantityValue)
                        _Flex.SetData(flexRwCnt, COL_MEDICATIONDATE, DateTime.Now)
                        _Flex.SetData(flexRwCnt, COL_STARTDATE, oRxHubInterface.Medication.Item(cnt).MedicationDrug.LastFillDate)
                        Dim sDaysSupply As Double = CType(oRxHubInterface.Medication.Item(cnt).MedicationDrug.DaysSupply, Double)
                        Dim dtLastFillDate As DateTime = oRxHubInterface.Medication.Item(cnt).MedicationDrug.LastFillDate
                        If sDaysSupply <= 31 Then
                            _Flex.SetData(flexRwCnt, COL_ENDDATE, dtLastFillDate.AddDays(sDaysSupply))
                        Else
                            sDaysSupply = sDaysSupply - 31
                            dtLastFillDate = dtLastFillDate.AddMonths(1)
                            _Flex.SetData(flexRwCnt, COL_ENDDATE, dtLastFillDate.AddDays(sDaysSupply))
                        End If
                        _Flex.SetData(flexRwCnt, COL_STATUS, "")
                        _Flex.SetData(flexRwCnt, COL_REASON, "")
                        _Flex.SetData(flexRwCnt, COL_mpid, 0)
                        _Flex.SetData(flexRwCnt, COL_LOGINNAME, gloRxHub.ClsgloRxHubGeneral.gstrRxHubLoginUser)

                        _Flex.SetData(flexRwCnt, COL_UPDATEDBy, gloRxHub.ClsgloRxHubGeneral.gstrRxHubLoginUser) '' New Column

                        _Flex.SetData(flexRwCnt, COL_RENEWED, "")
                        _Flex.SetData(flexRwCnt, COL_Narcotic, 0)
                        _Flex.SetData(flexRwCnt, COL_NDCCode, oRxHubInterface.Medication.Item(cnt).MedicationDrug.NDCCode)
                        _Flex.SetData(flexRwCnt, COL_DRUGFORM, oRxHubInterface.Medication.Item(cnt).MedicationDrug.DosageForm) ''''to be removed
                        _Flex.SetData(flexRwCnt, COL_DRUGQTYQUALIFIER, oRxHubInterface.Medication.Item(cnt).MedicationDrug.StrengthUnits) ''''to be removed


                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sRefills) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sRefills, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sRefills)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sRefills, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sNotes) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sNotes, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sNotes)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sNotes, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sMethod) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sMethod, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sMethod)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sMethod, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_bMaySubstitute) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_bMaySubstitute, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_bMaySubstitute)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_bMaySubstitute, False)
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nDrugID) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_nDrugID, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nDrugID)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_nDrugID, 0)
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_blnflag) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_blnflag, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_blnflag)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_blnflag, False)
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sLotNo) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sLotNo, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sLotNo)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sLotNo, "")
                        End If

                        'If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_dtExpirationdate) Then
                        '    _Flex.SetData(flexRwCnt, COL_Rx_dtExpirationdate, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_dtExpirationdate)
                        'Else
                        '    _Flex.SetData(flexRwCnt, COL_Rx_dtExpirationdate, "")
                        'End If
                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nProviderId) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_nProviderId, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nProviderId)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_nProviderId, 0)
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sChiefComplaints) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sChiefComplaints, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sChiefComplaints)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sChiefComplaints, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sStatus) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sStatus, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sStatus)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sStatus, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sRxReferenceNumber) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sRxReferenceNumber, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sRxReferenceNumber)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sRxReferenceNumber, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sRefillQualifier) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sRefillQualifier, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sRefillQualifier)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sRefillQualifier, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nPharmacyId) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_nPharmacyId, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nPharmacyId)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_nPharmacyId, 0)
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sNCPDPID) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sNCPDPID, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sNCPDPID)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sNCPDPID, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nContactID) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_nContactID, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_nContactID)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_nContactID, 0)
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sName) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sName, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sName)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sName, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sAddressline1) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sAddressline1, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sAddressline1)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sAddressline1, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sAddressline2) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sAddressline2, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sAddressline2)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sAddressline2, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sCity) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sCity, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sCity)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sCity, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sState) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sState, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sState)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sState, "")
                        End If


                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sZip) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sZip, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sZip)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sZip, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sEmail) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sEmail, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sEmail)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sEmail, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sFax) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sFax, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sFax)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sFax, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sPhone) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sPhone, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sPhone)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sPhone, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sServiceLevel) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sServiceLevel, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sServiceLevel)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sServiceLevel, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sPrescriberNotes) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_sPrescriberNotes, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_sPrescriberNotes)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_sPrescriberNotes, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_eRxStatus) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_eRxStatus, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_eRxStatus)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_eRxStatus, "")
                        End If

                        If Not IsNothing(oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_eRxStatusMessage) Then
                            _Flex.SetData(flexRwCnt, COL_Rx_eRxStatusMessage, oRxHubInterface.Medication.Item(cnt).MedicationDrug.Rx_eRxStatusMessage)
                        Else
                            _Flex.SetData(flexRwCnt, COL_Rx_eRxStatusMessage, "")
                        End If

                        _Flex.SetData(flexRwCnt, COL_RxMedDMSID, 0)

                        _Flex.SetData(flexRwCnt, COL_PBMSOURCENAME, oRxHubInterface.Medication.Item(cnt).MedicationDrug.PBMSourceName)

                        flexRwCnt = flexRwCnt + 1
                    Next
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SetTextViewDocument()
        Try
            btnScanViewDocument.Text = "View"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gloMedicationC1FlexGrdUserCtrl__FlexClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me._FlexClick
        'Try
        '    RowIndex = _Flex.Row
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub gloMedicationC1FlexGrdUserCtrl__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me._FlexDoubleClick
        'Try
        '    RowIndex = _Flex.Row
        'Catch ex As Exception

        'End Try
    End Sub


    Private Sub gloMedicationC1FlexGrdUserCtrl__FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me._FlexMouseDown
        Try
            Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
            If formLock = False Then
                If r <= 0 Then
                    RowIndex = r
                Else
                    RowIndex = _Flex.Row
                End If
            Else
                If Not IsNothing(cntListmenuStrip) Then
                    Me.cntListmenuStrip.Items(0).Visible = False
                    Me.cntListmenuStrip.Items(1).Visible = False
                End If
                Exit Sub
            End If
            If _RefillDisable = True Then ''do not allow to add/delete any transaction as per medication carry forward case scenarios
                Me.cntListmenuStrip.Items(0).Visible = False
                Me.cntListmenuStrip.Items(1).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Dim prevselecteditem As String = ""
    Private Sub cmbMedStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMedStatus.Click
        prevselecteditem = cmbMedStatus.SelectedItem
    End Sub

    Private Sub cmbMedStatus_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMedStatus.SelectionChangeCommitted

        SetMedicationStatus()

    End Sub

    Public Sub SetMedicationStatus()
        Dim arrlist As New ArrayList
        Dim tempvisitid As Int64
        Dim tempdate As DateTime
        Try
            Try
                If formLock = False Then
                    Dim dlgmsg As DialogResult = Nothing
                    Dim CurrentSelectedItem As String = cmbMedStatus.Text
                    For i As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
                        If _MedBuisnessLayer.MedicationCol.Item(i).State <> "U" Then
                            dlgmsg = MessageBox.Show("Do you want to save the changes?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                            If dlgmsg = Windows.Forms.DialogResult.Yes Then
                                RaiseEvent SaveRxMedStateCheck(New Object(), New EventArgs())
                                cmbMedStatus.Text = CurrentSelectedItem
                                Exit For
                            Else
                                RemoveHandler cmbMedStatus.SelectionChangeCommitted, AddressOf cmbMedStatus_SelectionChangeCommitted
                                cmbMedStatus.Text = prevselecteditem
                                AddHandler cmbMedStatus.SelectionChangeCommitted, AddressOf cmbMedStatus_SelectionChangeCommitted
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            If cmbMedStatus.Text = "Active" Then
                btnScanViewDocument.Enabled = True
                btnScanViewDocument.ForeColor = Color.FromArgb(31, 73, 125)
                btnQuickStatus.Visible = True
            Else
                btnScanViewDocument.Enabled = False
                btnScanViewDocument.ForeColor = Color.DarkGray
                btnQuickStatus.Visible = False
            End If

            txtSearch.Text = ""
            RaiseEvent cmbChangeRemoveControl()

            If _MedBuisnessLayer.TransactionMode = MedicationBusinessLayer._TransactionMode.Edit Then
                If _MedBuisnessLayer.CurrentVisitID <> 0 Then
                    _MedBuisnessLayer.MedicationCol.Clear()

                    If IsNothing(_Flex.DataSource) Then
                        _Flex.Rows.Count = 1
                    End If

                    _MedBuisnessLayer.FilterType = Trim(cmbMedStatus.Text)
                    strCmbMedStatusLastvalue = cmbMedStatus.Text

                    If _MedBuisnessLayer.FilterType = "Active" Then
                        RaiseEvent PrnFaxToggle(False)
                    Else
                        RaiseEvent PrnFaxToggle(True)
                    End If

                    _MedBuisnessLayer.FetchMedicationforUpdate(_MedBuisnessLayer.CurrentVisitDate, 0, _MedBuisnessLayer.FilterType)

                    BindFlexgrid()
                    RowIndex = 1
                End If
            Else
                Dim _MedicationBusinessLayer As New MedicationBusinessLayer(_PatientID)
                Dim _Medications As New Medications
                _MedBuisnessLayer.MedicationCol.Clear()
                _MedBuisnessLayer.FilterType = Trim(cmbMedStatus.Text)

                If _MedBuisnessLayer.FilterType = "Active" Then
                    RaiseEvent PrnFaxToggle(False)
                Else
                    RaiseEvent PrnFaxToggle(True)
                End If

                If _MedBuisnessLayer.CurrentVisitID <> 0 Then
                    _Medications = _MedicationBusinessLayer.PopulateMedicationHistory(_MedBuisnessLayer.CurrentVisitID, 2, Now, _MedBuisnessLayer.FilterType)
                    For i As Integer = 0 To _Medications.Count - 1
                        _MedBuisnessLayer.MedicationCol.Add(_Medications.Item(i))
                    Next
                    BindFlexgrid()
                    RowIndex = 1

                    _Medications.Dispose()
                    _MedicationBusinessLayer.Dispose()
                Else
                    arrlist = _MedicationBusinessLayer.GetVisitForHistory(0, 1, Now, _MedBuisnessLayer.FilterType)
                    If arrlist.Count > 0 Then
                        tempvisitid = CType(arrlist.Item(0), Int64)
                        tempdate = CType(arrlist.Item(1), DateTime)
                        _Medications = _MedicationBusinessLayer.PopulateMedicationHistory(tempvisitid, 2, tempdate, _MedBuisnessLayer.FilterType)
                        For i As Integer = 0 To _Medications.Count - 1
                            _MedBuisnessLayer.MedicationCol.Add(_Medications.Item(i))
                        Next
                        BindFlexgrid()
                        RowIndex = 1

                        _Medications.Dispose()
                        _MedicationBusinessLayer.Dispose()
                    Else
                        BindFlexgrid()
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = ex.Message
            Throw objex
        End Try
    End Sub

    Private Sub gloMedicationC1FlexGrdUserCtrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(_Flex)

        Try
            strCmbMedStatusLastvalue = cmbMedStatus.Text
            If EducationMaterialEnabled Then
                _Flex.Cols(COl_INFOBUTTON).Visible = True
            Else
                _Flex.Cols(COl_INFOBUTTON).Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = ex.Message
            Throw objex
        End Try

    End Sub



    'C

    ''get the NDCCODe against the selected Row no, so that after sorting the grid we can fetch the correct item from the Medication col and show in the Custom Medication control. 
    Public Function GetNDCCodeFromGrid(ByVal GridRowNo As Integer) As String
        Dim sNDCMxGrid As String = ""
        Dim sMedicationName As String = ""
        Dim sMedDosage As String = ""
        Dim sMedRoute As String = ""


        Dim sMedFreq As String = ""
        Dim sMedDuration As String = ""
        Dim sSelectedMed As String = ""
        Try
            If _Flex.Rows.Count > 0 Then
                For i As Integer = 0 To _Flex.Rows.Count
                    sNDCMxGrid = _Flex.GetData(GridRowNo, COL_NDCCode)
                    sMedicationName = _Flex.GetData(GridRowNo, COL_MEDICATION)
                    sMedDosage = _Flex.GetData(GridRowNo, COL_DOSAGE)
                    sMedRoute = _Flex.GetData(GridRowNo, COL_ROUTE)
                    sMedFreq = _Flex.GetData(GridRowNo, COL_FREQUENCY)
                    sMedDuration = _Flex.GetData(GridRowNo, COL_DURATION)

                    sSelectedMed = sSelectedMed & sNDCMxGrid & "~" & sMedicationName & "~" & sMedDosage & "~" & sMedRoute & "~" & sMedFreq & "~" & sMedDuration

                    Return sSelectedMed 'sNDCMxGrid
                Next
            End If
            Return sSelectedMed 'sNDCMxGrid
        Catch ex As Exception
            Return sNDCMxGrid
        End Try
    End Function

    Public Function GetRowNoFromGrid(ByVal GridRowNo As Integer) As Integer
        Dim RowNoColVal As Integer
        Try
            If _Flex.Rows.Count > 0 Then
                For i As Integer = 0 To _Flex.Rows.Count
                    RowNoColVal = _Flex.GetData(GridRowNo, COL_RowNumber) ''''the COL_RowNumber contains the drug items added to the grid, that will help us to compare with collection item number

                    Return RowNoColVal
                Next
            End If
            Return RowNoColVal
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub gloMedicationC1FlexGrdUserCtrl_OnAfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles Me.OnAfterSort
        Try
            Dim eRxSortOrder As enuRxSortOrder
            Select Case e.Col
                Case COL_MEDICATION
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.DrugNameAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.DrugNameDesc
                    End If
                Case COL_DOSAGE
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.DosageAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.DosageDesc
                    End If
                Case COL_ROUTE
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.RouteAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.RouteDesc
                    End If
                Case COL_FREQUENCY
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.FrequencyAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.FrequencyDesc
                    End If
                Case COL_DURATION
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.DurationAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.DurationDesc
                    End If
                Case COL_AMOUNT
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.DispenseAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.DispenseDesc
                    End If
                Case COL_STARTDATE
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.StartDateAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.StartDateDesc
                    End If
                Case COL_ENDDATE
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.EndDateAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.EndDateDesc
                    End If
                Case COL_LOGINNAME
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.UserNameAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.UserNameDesc
                    End If
                Case COL_UPDATEDBy '' New Column
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.UserNameAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.UserNameDesc
                    End If
                Case COL_RENEWED
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.RenewedAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.RenewedDesc
                    End If
            End Select
            '_MedBuisnessLayer.MedicationCol.Sort(New CMySortMed(eRxSortOrder))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub


    ''' <summary>
    '''  '''''Added function to resolve bug 9795
    ''' this function was written becaz when we change the status of the med combo box, invalid start/end dates with time used to get assigned to the start/end date cell in flex grid
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub ValidateAndSetDate(ByVal iRow As Integer)

        Dim odt As DateTime = Now

        ''Start date column
        Try

            If (Date.TryParse(_Flex.GetData(iRow, COL_STARTDATE).ToString(), odt) = False) Then
                _Flex.SetData(iRow, COL_STARTDATE, "")
            ElseIf odt = Date.MinValue Then
                _Flex.SetData(iRow, COL_STARTDATE, "")
            Else
                Dim sDtStartdate As String = ""
                sDtStartdate = CType(_Flex.GetData(iRow, COL_STARTDATE), Date)
                _Flex.SetData(iRow, COL_STARTDATE, sDtStartdate)
            End If

        Catch ex As Exception
            _Flex.SetData(iRow, COL_STARTDATE, "")
        End Try

        ''End date column
        Try
            If (_Flex.GetData(iRow, COL_ENDDATE) <> Nothing) Then
                If (Date.TryParse(_Flex.GetData(iRow, COL_ENDDATE).ToString(), odt) = False) Then
                    _Flex.SetData(iRow, COL_ENDDATE, Nothing)
                ElseIf odt = Date.MinValue Then
                    _Flex.SetData(iRow, COL_ENDDATE, Nothing)
                Else
                    'Dim sDtEnddate As String = ""
                    'sDtEnddate = CType(_Flex.GetData(iRow, COL_ENDDATE), Date)
                    _Flex.SetData(iRow, COL_ENDDATE, CType(_Flex.GetData(iRow, COL_ENDDATE), Date))
                End If
            Else
                _Flex.SetData(iRow, COL_ENDDATE, Nothing)
            End If
        Catch ex As Exception
            _Flex.SetData(iRow, COL_ENDDATE, Nothing)
        End Try
    End Sub





    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            ''commented for bug #8434 in 6040, logic shifter to key press event
            'Dim oCol As DataColumn

            ''oCol.Caption = ""
            ''oCol.ColumnName = ""
            ''tempData.Columns.Add(oCol)
            'If IsNothing(Dv_Search) Or txtSearch.Text.Trim <> "" Then
            '    C1Med_DataTable = New DataTable
            '    If _Flex.Cols.Count > 0 Then
            '        oCol = New DataColumn
            '        For i As Integer = 0 To _Flex.Cols.Count - 1
            '            oCol.Caption = _Flex.GetData(0, i)
            '            oCol.ColumnName = _Flex.GetData(0, i)
            '            C1Med_DataTable.Columns.Add(_Flex.GetData(0, i))
            '            'tempData.ImportRow(C1Formulary.Rows(i)(i))
            '        Next

            '    End If

            '    Dim oRow As DataRow
            '    If _Flex.Rows.Count > 1 Then

            '        For iRow As Integer = 1 To _Flex.Rows.Count - 1

            '            oRow = C1Med_DataTable.NewRow

            '            For iCol As Integer = 0 To _Flex.Cols.Count - 1

            '                oRow(iCol) = _Flex.GetData(iRow, iCol)
            '                'Changes done in 6030 only column change for Rx feild change in 6030
            '                If (iCol = 4) Then
            '                    Dim sDtStartdate As String = ""
            '                    sDtStartdate = CType(_Flex.GetData(iRow, iCol), Date)
            '                    oRow(4) = sDtStartdate
            '                End If

            '                If (iCol = 5) Then ''''bug fix 6610
            '                    Dim odt As DateTime = Now
            '                    Try

            '                        If (Date.TryParse(_Flex.GetData(iRow, 5).ToString(), odt) = False) Then
            '                            oRow(5) = ""
            '                        ElseIf odt = Date.MinValue Then
            '                            oRow(5) = ""
            '                        Else
            '                            Dim sDtEnddate As String = ""
            '                            sDtEnddate = CType(_Flex.GetData(iRow, 5), Date)
            '                            oRow(5) = sDtEnddate
            '                        End If

            '                    Catch ex As Exception
            '                        oRow(5) = ""
            '                    End Try
            '                End If
            '            Next

            '            C1Med_DataTable.Rows.Add(oRow)

            '        Next

            '        Dv_Search = C1Med_DataTable.DefaultView


            '    End If
            'Else

            '    _Flex.DataSource = C1Med_DataTable


            'End If


            InstringSearch()
            RedesignFlexgridColumns()




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Dim dvNext As DataView = Nothing
    Dim Dv_Search As DataView = Nothing
    Private Sub InstringSearch()
        Try

            'If _Flex.Rows.Count > 1 Then
            If Dv_Search Is Nothing Then
                Me.Cursor = Cursors.[Default]
                Exit Sub
            End If



            'Dim COL_MEDICATION As Byte = 3
            'Dim COL_DOSAGE As Byte = 4
            'Dim COL_ROUTE As Byte = 5
            'Dim COL_DRUGFORM As Byte = 6
            'Dim COL_FREQUENCY As Byte = 7
            'Dim COL_DURATION As Byte = 8
            'Dim COL_AMOUNT As Byte = 11
            'Dim COL_STARTDATE As Byte = 4
            'Dim COL_ENDDATE As Byte = 5
            'Dim COL_LOGINNAME As Byte = 16
            'Dim COL_MEDICATIONDATE As Byte = 12
            'Dim COL_STATUS As Byte = 13
            'Dim COL_REASON As Byte = 14
            'Dim COL_RENEWED As Byte = 17
            'Dim COL_PBMSOURCENAME As Byte = 21
            'Dim COL_Rx_sName As Byte = 38

            'code changed in 6030 for Rx Feild change
            Dim COL_MEDICATION_NAME As Byte = 3
            'Dim COL_DOSAGE As Byte = 4
            'Dim COL_ROUTE As Byte = 5
            'Dim COL_DRUGFORM As Byte = 6
            Dim COL_FREQUENCY As Byte = 9
            Dim COL_DURATION As Byte = 10
            Dim COL_AMOUNT As Byte = 11
            Dim COL_STARTDATE As Byte = 4
            Dim COL_ENDDATE As Byte = 5
            Dim COL_UPDATEDBy As Byte = 6
            Dim COL_LOGINNAME As Byte = 7
            Dim COL_MEDICATIONDATE As Byte = 12
            Dim COL_STATUS As Byte = 6
            Dim COL_REASON As Byte = 14
            Dim COL_RENEWED As Byte = 8
            Dim COL_PBMSOURCENAME As Byte = 21
            Dim COL_Rx_sName As Byte = 38



            Dim str As String = ""

            Dim strSearchArray As String()
            str = txtSearch.Text
            'str = str.Replace("[", "") + ""; 
            'str = str.Replace("'", "") + ""; 
            str = str.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")

            If str.Trim() <> "" Then
                strSearchArray = str.Split(","c)
                Dim strSearch As String = ""

                'used for instring search
                If strSearchArray.Length = 1 Then
                    strSearch = strSearchArray(0)
                    'code changed in 6030 for Rx Feild change
                    'Dv_Search.RowFilter = "[" & Dv_Search.Table.Columns(COL_MEDICATION).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_DOSAGE).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_ROUTE).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_DRUGFORM).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_FREQUENCY).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_DURATION).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_STARTDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_ENDDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_LOGINNAME).ColumnName & " Like '%" & strSearch & "%' OR " & Dv_Search.Table.Columns(COL_RENEWED).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_PBMSOURCENAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_Rx_sName).ColumnName & "]" & " Like '%" & strSearch & "%'"
                    Dv_Search.RowFilter = "[" & Dv_Search.Table.Columns(COL_MEDICATION_NAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_FREQUENCY).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_DURATION).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_STARTDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_ENDDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_LOGINNAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_RENEWED).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_PBMSOURCENAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & Dv_Search.Table.Columns(COL_Rx_sName).ColumnName & "]" & " Like '%" & strSearch & "%'  OR " & "[" & Dv_Search.Table.Columns(COL_UPDATEDBy).ColumnName & "]" & " Like '%" & strSearch & "%'"
                Else
                    Dim dtTemp As DataTable = Nothing
                    For i As Byte = 1 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i)
                        If strSearch.Trim() <> "" Then
                            'If i = 1 Then
                            '    dtTemp = Dv_Search.ToTable()
                            '    dvNext = dtTemp.DefaultView
                            'Else
                            '    dtTemp = dvNext.ToTable()
                            '    dvNext = dtTemp.DefaultView
                            'End If
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
                            'code changed in 6030 for Rx Feild change
                            ''dvNext.RowFilter = dvNext.Table.Columns(COL_MEDICATION).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DOSAGE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_ROUTE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DRUGFORM).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_FREQUENCY).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DURATION).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_STARTDATE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_ENDDATE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_LOGINNAME).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RENEWED).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_PBMSOURCENAME).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_Rx_sName).ColumnName & " Like '%" & strSearch & "%'"
                            'dvNext.RowFilter = "[" & dvNext.Table.Columns(COL_MEDICATION).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DOSAGE).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_ROUTE).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_DRUGFORM).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_FREQUENCY).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DURATION).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_STARTDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_ENDDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_LOGINNAME).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RENEWED).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_PBMSOURCENAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_Rx_sName).ColumnName & "]" & " Like '%" & strSearch & "%'"
                            dvNext.RowFilter = "[" & dvNext.Table.Columns(COL_MEDICATION_NAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_FREQUENCY).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_DURATION).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_STARTDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_ENDDATE).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_LOGINNAME).ColumnName & " Like '%" & strSearch & "%' OR " & dvNext.Table.Columns(COL_RENEWED).ColumnName & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_PBMSOURCENAME).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_Rx_sName).ColumnName & "]" & " Like '%" & strSearch & "%' OR " & "[" & dvNext.Table.Columns(COL_UPDATEDBy).ColumnName & "]" & " Like '%" & strSearch & "%'"
                        End If
                    Next
                    If Not IsNothing(dtTemp) Then ''disposed as per glo Code optimizer tool in 8000 version
                        dtTemp.Dispose()
                        dtTemp = Nothing
                    End If
                End If

                If strSearch <> "" AndAlso strSearch.Trim() <> "" Then
                    If strSearchArray.Length = 1 Then
                        _Flex.DataSource = Dv_Search
                        If (IsNothing(Dv_Search) = False) Then
                            _VisibleCount = Dv_Search.Count
                        Else
                            _VisibleCount = 0
                        End If

                    Else
                        _Flex.DataSource = dvNext
                        If (IsNothing(dvNext) = False) Then
                            _VisibleCount = dvNext.Count
                        Else
                            _VisibleCount = 0
                        End If

                    End If
                End If
            Else
                Dv_Search.RowFilter = ""
                _Flex.DataSource = Dv_Search
                If (IsNothing(Dv_Search) = False) Then
                    _VisibleCount = Dv_Search.Count
                Else
                    _VisibleCount = 0
                End If
            End If

            '_Flex.Selection.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            _Flex.Refresh()

            For iRow As Integer = 1 To _Flex.Rows.Count - 1
                _Flex.SetCellImage(iRow, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)
            Next
            _Flex.Refresh()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Search, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Function isADeletedMedication(ByVal myItemNumber As Integer) As Boolean
        For iCount As Integer = 0 To _MedBuisnessLayer.MedicationCol.Count - 1
            If (_MedBuisnessLayer.MedicationCol.Item(iCount).ItemNumber = myItemNumber) Then
                If (_MedBuisnessLayer.MedicationCol.Item(iCount).State = "D") Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next
        Return False
    End Function

    '''' Bug #58796: MU2 Audit Trail
    Dim isMedSearched As Boolean = False
    Dim nAudit As Integer = 0
    '''' Bug #58796: MU2 Audit Trail
    Private Sub txtSearch_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.LostFocus
        If nAudit = 0 And isMedSearched = True Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.Query, gloAuditTrail.ActivityType.Search, "Medication Query " & Chr(34) & txtSearch.Text & Chr(34), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            nAudit = nAudit + 1
        End If

    End Sub



    Dim _my_C1Med_DataTable As DataTable ''fix bug #8434 in 6040
    ''fix bug #8434 in 6040 -- search logic shifted from text search to key press
    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        isMedSearched = True '' Bug #58796: MU2 Audit Trail
        Dim oCol As DataColumn

        'oCol.Caption = ""
        'oCol.ColumnName = ""
        'tempData.Columns.Add(oCol)
        'If _my_C1Med_DataTable.Rows.Count <> _Flex.Cols.Count Then
        If txtSearch.Text.Trim = "" Then
            If (IsNothing(_my_C1Med_DataTable) = False) Then
                _my_C1Med_DataTable.Dispose()
                _my_C1Med_DataTable = Nothing
            End If
            _my_C1Med_DataTable = New DataTable
            If _Flex.Cols.Count > 0 Then

                For i As Integer = 0 To _Flex.Cols.Count - 1
                    If IsNothing(_Flex.GetData(0, i)) Then
                        oCol = New DataColumn
                        oCol.Caption = _Flex.GetData(0, i)
                        oCol.ColumnName = _Flex.GetData(0, i)
                        '  oCol.DataType = System.Type.GetType("System.Byte[]")
                        _my_C1Med_DataTable.Columns.Add(oCol)
                        oCol = Nothing
                    Else
                        If _Flex.GetData(0, i) = "" Then
                            oCol = New DataColumn
                            oCol.Caption = _Flex.GetData(0, i)
                            oCol.ColumnName = _Flex.GetData(0, i)
                            '     oCol.DataType = System.Type.GetType("System.Byte[]")


                            _my_C1Med_DataTable.Columns.Add(oCol)
                            oCol = Nothing
                        Else
                            _my_C1Med_DataTable.Columns.Add(_Flex.GetData(0, i))
                        End If
                        'tempData.ImportRow(C1Formulary.Rows(i)(i))
                    End If

                Next

            End If

            Dim oRow As DataRow
            If _Flex.Rows.Count > 1 Then

                For iRow As Integer = 1 To _Flex.Rows.Count - 1
                    If isADeletedMedication(_Flex.GetData(iRow, COL_RowNumber)) = False Then
                        oRow = _my_C1Med_DataTable.NewRow

                        For iCol As Integer = 0 To _Flex.Cols.Count - 1

                            oRow(iCol) = _Flex.GetData(iRow, iCol)
                            If Not IsNothing(_Flex.GetData(iRow, iCol)) Then
                                If (_Flex.GetData(iRow, iCol).GetType.Name = "Date") Then
                                    Dim odt As DateTime = Now
                                    Try

                                        If (Date.TryParse(_Flex.GetData(iRow, iCol).ToString(), odt) = False) Then
                                            oRow(iCol) = ""
                                        ElseIf odt = Date.MinValue Then
                                            oRow(iCol) = ""
                                        Else
                                            Dim sDtEnddate As String = ""
                                            sDtEnddate = CType(_Flex.GetData(iRow, iCol), Date)
                                            oRow(iCol) = sDtEnddate
                                        End If

                                    Catch ex As Exception
                                        oRow(iCol) = ""
                                    End Try

                                    ' If oRow(iCol).DataType = System.Type.GetType("System.Byte[]") Then
                                    'If Not IsNothing(_Flex.GetCellImage(iRow, iCol)) Then
                                    '    oRow(iCol) = _Flex.GetCellImage(iRow, iCol)
                                    'End If
                                    ' End If
                                End If
                            Else
                                ' If oRow(iCol).DataType = System.Type.GetType("System.Byte[]") Then
                                'If Not IsNothing(_Flex.GetCellImage(iRow, iCol)) Then
                                '    oRow(iCol) = _Flex.GetCellImage(iRow, iCol)
                                'End If
                                'End If
                            End If

                            'Changes done in 6030 only column change for Rx feild change in 6030
                            'If (iCol = 4) Then
                            '    Dim sDtStartdate As String = ""
                            '    _Flex.gett()
                            '    sDtStartdate = CType(_Flex.GetData(iRow, iCol), Date)
                            '    oRow(4) = sDtStartdate
                            'End If

                            'If (iCol = 5) Then ''''bug fix 6610
                            '    Dim odt As DateTime = Now
                            '    Try

                            '        If (Date.TryParse(_Flex.GetData(iRow, 5).ToString(), odt) = False) Then
                            '            oRow(5) = ""
                            '        ElseIf odt = Date.MinValue Then
                            '            oRow(5) = ""
                            '        Else
                            '            Dim sDtEnddate As String = ""
                            '            sDtEnddate = CType(_Flex.GetData(iRow, 5), Date)
                            '            oRow(5) = sDtEnddate
                            '        End If

                            '    Catch ex As Exception
                            '        oRow(5) = ""
                            '    End Try
                            'End If
                        Next

                        _my_C1Med_DataTable.Rows.Add(oRow)
                    End If
                Next

                Dv_Search = _my_C1Med_DataTable.DefaultView


            End If
            'Else

            '    _Flex.DataSource = C1Med_DataTable

        End If
        'End If
        If e.KeyChar = CType(ChrW(Keys.Back), Char) Then
            ' C1Med_DataTable = _my_C1Med_DataTable.Copy()
            Dv_Search = _my_C1Med_DataTable.Copy().DefaultView
        End If
    End Sub

    Private Sub BtnMedRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMedRec.Click
        RaiseEvent btnMedRecClick(Nothing, Nothing)
    End Sub

    Private Sub _Flex_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.Click
        If _Flex.Col = COl_INFOBUTTON Then

            'Dim sNDCCode As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_NDCCode))
            'Dim nVisitid As Long = 0
            'nVisitid = Convert.ToInt64(_Flex.GetData(_Flex.Row, COL_VISITID))
            'If nVisitid = 0 Then
            '    Dim dr As DialogResult = MessageBox.Show("You need to save newly added drug to get education material." & vbNewLine & "Do you want to save?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            '    If dr.ToString = "Yes" Then
            '        RaiseEvent SaveMedication(sender, e)
            '        nVisitid = VisitIDaftersave
            '    Else
            '        nVisitid = 0
            '    End If

            'End If

            'If nVisitid > 0 Then
            '    Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            '    Dim dtPatientInfo As New DataTable()
            '    Dim strSql As String = "select sGender,[dbo].[fn_CalculateAgeMU] (Patient.dtDOB,dbo.gloGetDate()) as age,sLang as Lang from Patient where nPatientID  =" & _PatientID
            '    dtPatientInfo = ogloEMRDatabase.GetDataTable_Query(strSql)
            '    Dim pGender As String = ""
            '    Dim pAge As String = ""
            '    Dim pLanguage As String = ""
            '    If dtPatientInfo IsNot Nothing Then
            '        pGender = Convert.ToString(dtPatientInfo.Rows(0)("sGender"))
            '        pAge = Convert.ToString(Convert.ToInt32(dtPatientInfo.Rows(0)("age")))
            '        pLanguage = Convert.ToString(dtPatientInfo.Rows(0)("Lang"))
            '    End If

            '    If sNDCCode.Length > 0 Then
            '        clsinfobutton_Medication.Openinfosource(sNDCCode, "2.16.840.1.113883.6.69", pLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication.GetHashCode(), nVisitid)
            '    Else
            '        MessageBox.Show("NDC code is not available for selected Drug", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    End If
            'End If







        End If
    End Sub


    Private Sub _Flex_MouseDown_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim c As Integer = _Flex.HitTest(e.X, e.Y).Column
            Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row

            If r > 0 Then
                If c = COl_INFOBUTTON Then
                    Try
                        If Not IsNothing(cntListmenuStrip.Items) Then
                            If cntListmenuStrip.Items.Count > 2 Then
                                For i As Integer = cntListmenuStrip.Items.Count - 1 To 2 Step -1
                                    If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                        cntListmenuStrip.Items.RemoveAt(i)
                                    ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                        cntListmenuStrip.Items.RemoveAt(i)
                                    ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                        cntListmenuStrip.Items.RemoveAt(i)
                                    End If
                                Next
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    If EducationMaterialEnabled Then
                        AddInfobuttonMenu(sender, e)
                    End If



                    If Not IsNothing(cntListmenuStrip.Items) Then
                        If cntListmenuStrip.Items.Count > 3 Then  'CS#377194 integration done from 9000
                            For i As Integer = 0 To cntListmenuStrip.Items.Count - 1
                                If cntListmenuStrip.Items(i).GetType().Name = "ToolStripSeparator" Then
                                    cntListmenuStrip.Items(i).Visible = False
                                Else
                                    ''TODO : add newly added menu items here
                                    If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Delete Medication Item" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Renew/Refill" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Modify Medication Item" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Active" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Inactive" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Completed" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Discontinued" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Unknown" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

            Dim c As Integer = _Flex.HitTest(e.X, e.Y).Column
            Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
            If r > 0 Then 'CS#377194 integration done from 9000
                If c <> COl_INFOBUTTON Then

                    If cntListmenuStrip.Items.Count > 2 Then
                        If _Flex.GetData(r, 7) <> "Cancelled" Then
                            AddMenu()
                            For i As Integer = 0 To cntListmenuStrip.Items.Count - 1
                                If cntListmenuStrip.Items(i).GetType().Name = "ToolStripSeparator" Then
                                    cntListmenuStrip.Items(i).Visible = True
                                Else
                                    ''TODO : add newly added menu items here
                                    If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                        cntListmenuStrip.Items(i).Visible = False

                                    ElseIf cntListmenuStrip.Items(i).Text = "Renew/Refill" Then
                                        cntListmenuStrip.Items(i).Visible = True

                                    ElseIf cntListmenuStrip.Items(i).Text = "Delete Medication Item" Then
                                        cntListmenuStrip.Items(i).Visible = True

                                    ElseIf cntListmenuStrip.Items(i).Text = "Modify Medication Item" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Active" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Inactive" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Completed" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Discontinued" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Unknown" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    End If
                                End If


                            Next
                        Else
                            RemoveMenu()
                        End If
                    End If

                Else
                    If Not IsNothing(cntListmenuStrip.Items) Then
                        If cntListmenuStrip.Items.Count > 3 Then
                            For i As Integer = 0 To cntListmenuStrip.Items.Count - 1
                                ''TODO : add newly added menu items here
                                If cntListmenuStrip.Items(i).GetType().Name = "ToolStripSeparator" Then
                                    cntListmenuStrip.Items(i).Visible = False
                                Else
                                    If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                        cntListmenuStrip.Items(i).Visible = True
                                    ElseIf cntListmenuStrip.Items(i).Text = "Delete Medication Item" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Renew/Refill" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Modify Medication Item" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Active" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Inactive" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Completed" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Discontinued" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    ElseIf cntListmenuStrip.Items(i).Text = "Mark as Unknown" Then
                                        cntListmenuStrip.Items(i).Visible = False
                                    End If
                                End If

                            Next
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub AddInfobuttonMenu(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)


        Dim tlstripitem As ToolStripMenuItem
        tlstripitem = New ToolStripMenuItem

        Dim tlstripitem_Patient As ToolStripMenuItem
        tlstripitem_Patient = New ToolStripMenuItem

        Dim tlstripitem_Provider As ToolStripMenuItem
        tlstripitem_Provider = New ToolStripMenuItem

        Dim tlstripitem_Infobutton As ToolStripMenuItem
        tlstripitem_Infobutton = New ToolStripMenuItem

        Dim tlstripsubitem As ToolStripMenuItem
        tlstripsubitem = New ToolStripMenuItem


        Dim dtEdu As DataTable = Nothing

        Dim pGender As String = ""
        Dim pAge As String = ""
        Dim pLanguage As String = ""
        Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
        Try

            ' Dim sNDCCode As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_NDCCode))
            Dim mpid As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_mpid))
            Dim strSql As String = "select sGender,[dbo].[fn_CalculateAgeMU] (Patient.dtDOB,dbo.gloGetDate()) as age,sLang as Lang from Patient where nPatientID  =" & _PatientID
            Dim dtPatientInfo As DataTable = ogloEMRDatabase.GetDataTable_Query(strSql)
            If dtPatientInfo IsNot Nothing Then
                If (dtPatientInfo.Rows.Count >= 1) Then
                    pGender = Convert.ToString(dtPatientInfo.Rows(0)("sGender"))
                    pAge = Convert.ToString(Convert.ToInt32(dtPatientInfo.Rows(0)("age")))
                    pLanguage = Convert.ToString(dtPatientInfo.Rows(0)("Lang"))
                End If
                dtPatientInfo.Dispose()
                dtPatientInfo = Nothing
            End If
            Dim clsinfobutton_Medication As New gloEMRGeneralLibrary.clsInfobutton()
            dtEdu = clsinfobutton_Medication.GetEducationMaterial(mpid, "2.16.840.1.113883.6.69", pAge, pGender)
            clsinfobutton_Medication = Nothing
            If Not IsNothing(dtEdu) Then
                If dtEdu.Rows.Count > 0 Then
                    tlstripitem_Patient = New ToolStripMenuItem
                    tlstripitem_Patient.Text = "Patient Reference Material"
                    tlstripitem_Patient.Image = ImageFlex.Images(4)
                    tlstripitem_Patient.ForeColor = Color.FromArgb(31, 73, 125)

                    tlstripitem_Provider = New ToolStripMenuItem
                    tlstripitem_Provider.Text = "Provider Reference Material"
                    tlstripitem_Provider.Image = ImageFlex.Images(5)
                    tlstripitem_Provider.ForeColor = Color.FromArgb(31, 73, 125)

                    tlstripitem_Infobutton = New ToolStripMenuItem
                    tlstripitem_Infobutton.Text = "Infobutton"
                    tlstripitem_Infobutton.Image = ImageFlex.Images(6)
                    tlstripitem_Infobutton.ForeColor = Color.FromArgb(31, 73, 125)
                    AddHandler tlstripitem_Infobutton.Click, AddressOf OpenInfobutton

                    For i As Integer = 0 To dtEdu.Rows.Count - 1
                        If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 1 Then
                            tlstripsubitem = New ToolStripMenuItem
                            tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                            tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
                            tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Patient Reference Material"
                            tlstripitem_Patient.DropDownItems.Add(tlstripsubitem)
                            AddHandler tlstripsubitem.Click, AddressOf OpenEducation
                            tlstripsubitem = Nothing
                        ElseIf Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 2 Then
                            If Convert.ToBoolean(dtEdu.Rows(i)("bIsAdvancedProviderReference")) Then
                                If AdvancedReferenceEnabled = True Then
                                    tlstripsubitem = New ToolStripMenuItem
                                    tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                    tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
                                    tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                    tlstripitem_Provider.DropDownItems.Add(tlstripsubitem)
                                    AddHandler tlstripsubitem.Click, AddressOf OpenEducation
                                    tlstripsubitem = Nothing
                                End If
                            Else
                                tlstripsubitem = New ToolStripMenuItem
                                tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
                                tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
                                tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
                                tlstripitem_Provider.DropDownItems.Add(tlstripsubitem)
                                AddHandler tlstripsubitem.Click, AddressOf OpenEducation
                                tlstripsubitem = Nothing
                            End If
                        End If
                    Next


                    cntListmenuStrip.Items.Add(tlstripitem_Infobutton)
                    If tlstripitem_Patient.DropDownItems.Count > 0 Then
                        cntListmenuStrip.Items.Add(tlstripitem_Patient)
                    End If
                    If tlstripitem_Provider.DropDownItems.Count > 0 Then
                        cntListmenuStrip.Items.Add(tlstripitem_Provider)
                    End If

                    cntListmenuStrip.Visible = True
                    cntListmenuStrip.Show(CType(sender, Control), e.Location)

                    ''TODO : add newly added menu items here
                    cntListmenuStrip.Items("Delete Medication Item").Visible = False

                    cntListmenuStrip.Items("Renew/Refill").Visible = False

                    cntListmenuStrip.Items("Modify Medication Item").Visible = False
                    cntListmenuStrip.Items("Mark as Active").Visible = False
                    cntListmenuStrip.Items("Mark as Inactive").Visible = False
                    cntListmenuStrip.Items("Mark as Completed").Visible = False
                    cntListmenuStrip.Items("Mark as Discontinued").Visible = False
                    cntListmenuStrip.Items("Mark as Unknown").Visible = False

                    tlstripitem_Patient = Nothing
                    tlstripitem_Provider = Nothing
                Else
                    'Open Online Document
                    OpenInfobutton(sender, New System.EventArgs())
                End If
            Else
                'Open Online Document
                OpenInfobutton(sender, New System.EventArgs())
            End If



            'If Not IsNothing(dtEdu) Then
            '    If dtEdu.Rows.Count > 0 Then
            '        tlstripitem = New ToolStripMenuItem
            '        tlstripitem.Text = "Patient Reference Material"
            '        tlstripitem.Image = ImageFlex.Images(3)
            '        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            '        For i As Integer = 0 To dtEdu.Rows.Count - 1
            '            If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 1 Then
            '                tlstripsubitem = New ToolStripMenuItem
            '                tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
            '                tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
            '                tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Patient Reference Material"
            '                tlstripitem.DropDownItems.Add(tlstripsubitem)
            '                AddHandler tlstripsubitem.Click, AddressOf OpenEducation
            '                tlstripsubitem = Nothing
            '            End If
            '        Next
            '        cntListmenuStrip.Items.Add(tlstripitem)
            '        'AddHandler tlstripitem.Click, AddressOf StripItem_Click
            '        tlstripitem = Nothing

            '        tlstripitem = New ToolStripMenuItem
            '        tlstripitem.Text = "Provider Reference Material"
            '        tlstripitem.Image = ImageFlex.Images(3)
            '        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            '        For i As Integer = 0 To dtEdu.Rows.Count - 1
            '            If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 2 Then
            '                If Convert.ToBoolean(dtEdu.Rows(i)("bIsAdvancedProviderReference")) Then
            '                    If AdvancedReferenceEnabled = True Then
            '                        tlstripsubitem = New ToolStripMenuItem
            '                        tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
            '                        tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
            '                        tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
            '                        tlstripitem.DropDownItems.Add(tlstripsubitem)
            '                        AddHandler tlstripsubitem.Click, AddressOf OpenEducation
            '                        tlstripsubitem = Nothing
            '                    End If
            '                Else
            '                    tlstripsubitem = New ToolStripMenuItem
            '                    tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
            '                    tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
            '                    tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
            '                    tlstripitem.DropDownItems.Add(tlstripsubitem)
            '                    AddHandler tlstripsubitem.Click, AddressOf OpenEducation
            '                    tlstripsubitem = Nothing
            '                End If

            '            End If
            '        Next
            '        cntListmenuStrip.Items.Add(tlstripitem)
            '        'AddHandler tlstripitem.Click, AddressOf StripItem_Click
            '        tlstripitem = Nothing

            '    End If
            'End If

            isInfobuttonMenuAdded = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("AddInfobuttonMenu ::" & ex.ToString(), False)
        Finally

            If Not IsNothing(tlstripitem) Then
                tlstripitem.Dispose()
                tlstripitem = Nothing
            End If

            If Not IsNothing(tlstripsubitem) Then
                tlstripsubitem.Dispose()
                tlstripsubitem = Nothing
            End If

            If Not IsNothing(dtEdu) Then
                dtEdu.Dispose()
                dtEdu = Nothing
            End If


            pGender = Nothing
            pAge = Nothing
            pLanguage = Nothing

            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If

        End Try
    End Sub

    Private Sub OpenInfobutton(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim sNDCCode As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_NDCCode))
            Dim sNDCCodeDesc As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_MEDICATION_NAME))
            Dim nVisitid As Long = 0
            nVisitid = Convert.ToInt64(_Flex.GetData(_Flex.Row, COL_VISITID))
            If nVisitid = 0 Then
                Dim dr As DialogResult = MessageBox.Show("You need to save newly added drug to get education material." & vbNewLine & "Do you want to save?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If dr.ToString = "Yes" Then
                    RaiseEvent SaveMedication(sender, e)
                    nVisitid = VisitIDaftersave
                Else
                    nVisitid = 0
                End If

            End If

            If nVisitid > 0 Then
                RaiseEvent InfoButtonClicked(sNDCCode, sNDCCodeDesc)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("OpenInfobutton ::" & ex.ToString(), False)
        End Try
    End Sub
    Private Sub OpenEducation(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim oCurrentMenu As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
            Dim tag() As String = oCurrentMenu.Tag.ToString().Split("-")
            Dim templateName As String = oCurrentMenu.Text
            Dim nTempId As Int64 = CType(tag(0), Int64)
            Dim OpenFor As String = tag(1).ToString()
            OpenFor = OpenFor + "-" + templateName
            RaiseEvent InfoButtonDocumentClicked(nTempId, OpenFor, templateName, tag(1))
        Catch ex As Exception

        End Try


    End Sub
    Public Sub DeleteCorrespondingMedication(ByVal itemNumber As Integer)
        Try
            For i As Integer = 1 To _Flex.Rows.Count - 1
                Dim FlexRowNo As Integer = _Flex.GetData(i, COL_RowNumber)
                If FlexRowNo = itemNumber Then
                    _Flex.Rows.Item(i).Visible = False
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanViewDocument.MouseHover, BtnMedRec.MouseHover, btnQuickStatus.MouseHover
        If sender IsNot Nothing Then
            Dim btn As Button = sender
            If btn IsNot Nothing Then
                btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongYellow
                btn.BackgroundImageLayout = ImageLayout.Stretch
            End If
        End If
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanViewDocument.MouseLeave, BtnMedRec.MouseLeave, btnQuickStatus.MouseLeave
        If sender IsNot Nothing Then
            Dim btn As Button = sender
            If btn IsNot Nothing Then
                btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongButton
                btn.BackgroundImageLayout = ImageLayout.Stretch
            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    'cntListmenuStrip
    Private Sub cntListmenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If Convert.ToString(cmbMedStatus.SelectedItem) = "Cancelled" Then
            RemoveMenu()
        Else
            AddMenu()
        End If
    End Sub

    Private Sub btnrechist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrechist.Click
        RaiseEvent btnRecHistoryClick(Nothing, Nothing)
    End Sub

    Private Sub brnSignedAgreement_Click(sender As System.Object, e As System.EventArgs) Handles brnSignedAgreement.Click
        RaiseEvent btnSignedAgreementClick(Nothing, Nothing)
    End Sub
    Public Sub ChangeAgreementIcon(ByVal _IsagreementSigned As Boolean)
        If _IsagreementSigned Then
            brnSignedAgreement.Image = My.Resources.AgreementGreen.ToBitmap()
        Else
            brnSignedAgreement.Image = My.Resources.AgreementRed.ToBitmap
        End If

    End Sub
End Class
