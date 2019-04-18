Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloSureScript.PrescriptionMedicationEventArgs
Imports gloRxHub
Imports System.Linq
Imports gloGlobal.FS3


Public Class gloRxC1FlexGrdPrescriptionUserCtrl
    Public Event RowDoubleClicked(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
    Public Event DrugFormularyQueried(ByVal SelectedDrug As String, ByVal mpid As Int64, ByVal rxType As String, ByVal IsFormularyQueried As Boolean, ByVal SelectedRow As Integer, ByVal QueriedFrom As FormularyQueriedFrom)
    Public Event PDRProgramsRequested(ByVal SelectedDrug As Integer?)

    Public Event DrugFormularyRequested(ByVal SelectedRow As Integer)
    Public Event CancelRxRequested(ByVal SelectedRow As Integer, ByVal Flag As String)
    Public Event DrugPARequested(ByVal SelectedRow As Integer)
    'Public Event PDRInfoButtonClicked(ByVal SelectedRow As Integer)

    Public Event InfoButtonClicked(ByVal NDCCode As String, ByVal NDCCodeDesc As String)

    Private _PatientID As Long
    Private _nLoginProviderid As Long
    Public Sub New(ByRef objRxBusinessLayer As RxBusinesslayer, ByVal PatientID As Long, ByVal nLoginProviderid As Long, ByVal _formLock As Boolean)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _RxBuisnessLayer = objRxBusinessLayer
        formLock = _formLock
        ' Add any initialization after the InitializeComponent() call.
        If formLock = False Then
            AddMenu()
        End If
        SetFlexgridColumns()
        _Flex.Rows.Count = 1
        _PatientID = PatientID
        _nLoginProviderid = nLoginProviderid
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private _SelectedMedication As String
    Public Property SelectedMedication() As String
        Get
            Return _SelectedMedication
        End Get
        Set(ByVal value As String)
            _SelectedMedication = value
        End Set
    End Property


    Public Event StripItemClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event DeleteCorrespondingMedication(ByVal sender As Object, ByVal e As gloSureScript.PrescriptionMedicationEventArgs)
    Public Event PrescriptionItemDeleted()
    Public formLock As Boolean = False
    Private _RxBuisnessLayer As RxBusinesslayer
    '    Private COL_COUNT As Integer = 24


    Public isInfobuttonMenuAdded As Boolean = False
    Public Event InfoButtonDocumentClicked(ByVal templateCode As String, ByVal openFor As String, ByVal TemplateName As String, ByVal sResourceType As String)
    Public Event SavePrescription(ByVal sender As Object, ByVal e As System.EventArgs)

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

#Region "Design Columns"


    'code added by supriya 19/7/2008
    Private COL_COUNT As Integer = 58

    Private COL_SELECT As Integer = 0
    Private COL_PRESCRIPTIONID As Integer = 1
    Private COL_VISITID As Integer = 2
    Private COL_PATIENTID As Integer = 3
    Private COL_MEDICATION As Integer = 4

    Private COl_INFOBUTTON As Integer = 5

    Private COL_PDR As Integer = 6

    Private COL_DOSAGE As Integer = 7
    Private COL_ROUTE As Integer = 8
    Private COL_DRUGFORM As Integer = 9
    Private COL_RxDrugType As Integer = 10
    ''EPA
    Private COL_EPAStatus As Integer = 11
    Private COL_FormularyStatus As Integer = 12
    Private COL_CoverageIndicator As Integer = 13
    Private COL_CopayIndicator As Integer = 14

    Private COL_FREQUENCY As Integer = 15
    Private COL_DURATION As Integer = 16
    Private COL_AMOUNT As Integer = 17
    Private COL_REFILLS As Integer = 18
    Private COL_METHOD As Integer = 19
    Private COL_eRxStatus As Integer = 20
    Private COL_USERNAME As Integer = 21
    Private COL_NOTES As Integer = 22
    Private Col_REASON As Integer = 23
    Private COL_STARTDATE As Integer = 24
    Private COL_PRESCRIPTIONDATE As Integer = 25
    Private COL_DRUGID As Integer = 26
    Private COL_INFLAG As Integer = 27
    Private COL_LOTNUMBER As Integer = 28
    Private COL_EXPDATE As Integer = 29
    Private COL_USERID As Integer = 30
    Private COL_ENDDATE As Integer = 31

    'code added by supriya 19/7/2008
    Private COL_Narcotic As Integer = 32
    Private COL_ProviderID As Integer = 33
    Private COL_PharmacyID As Integer = 34

    'For De-Normalization
    Private COL_NDCCode As Integer = 35
    Private COL_mpid As Integer = 36

    Private COL_DRUGQTYQUALIFIER As Integer = 37
    'For De-Normalization

    'Pharmacy Info
    Private COL_NCPDPID As Integer = 38
    Private COL_PharmacyContactID As Integer = 39

    Private COL_MAYSUBSTITUTE As Integer = 40
    Private COL_PharmacyAddressline1 As Integer = 41
    Private COL_PharmacyAddressline2 As Integer = 42
    Private COL_PharmacyCity As Integer = 43
    Private COL_PharmacysState As Integer = 44
    Private COL_PharmacyZip As Integer = 45
    Private COL_PharmacyEmail As Integer = 46
    Private COL_PharmacyFax As Integer = 47
    Private COL_PharmacyPhone As Integer = 48
    Private COL_PharmacyServiceLevel As Integer = 49
    'Pharmacy Info

    ''Temperory Formulary Information
    Private COL_FormularyAlternatives_Grid As Integer = 50
    Private COL_FormularyInformation_RchTxt As Integer = 51

    Private COL_PharmacyName As Integer = 52
    Private COL_eRxStatusMessage As Integer = 53
    Private COL_RowNumber As Integer = 54 ''''coloumn will contain the item number with respect to Precription collection
    Private COL_FormularyQueried As Integer = 55
    Private COL_FS As Integer = 56

    Private COL_IseRxed As Integer = 57

#End Region

    Public Sub ShowFormulary(ByVal row As Int32)
        _Flex.Select(row, 0)
        PrescriptionRowChangedRevised(row, True, FormularyQueriedFrom.None)
    End Sub


    ''TODO : check usage & remove 
    Public Sub SelectRow(ByVal i As Int32)
        _Flex.Select(i, 0)
    End Sub


    Public Property RxBuisnessLayerObject() As RxBusinesslayer
        Get
            Return _RxBuisnessLayer
        End Get
        Set(ByVal value As RxBusinesslayer)
            _RxBuisnessLayer = value
        End Set
    End Property

    Public Sub AddMenu()
        Dim tlstripitem As ToolStripMenuItem
        tlstripitem = New ToolStripMenuItem

        tlstripitem.Text = "Delete All Prescriptions"
        tlstripitem.Tag = 1
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = ImageFlex.Images(6)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf StripItem_Click
        'tlstripitem.Dispose()
        tlstripitem = Nothing

        tlstripitem = New ToolStripMenuItem
        tlstripitem.Text = "Delete Prescription Item"
        tlstripitem.Tag = 2
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = ImageFlex.Images(5)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf StripItem_Click
        tlstripitem = Nothing

        tlstripitem = New ToolStripMenuItem
        tlstripitem.Text = "Show eRx status"
        tlstripitem.Tag = 3
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = ImageFlex.Images(7)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf StripItem_Click
        tlstripitem = Nothing

        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True Then
            tlstripitem = New ToolStripMenuItem
            tlstripitem.Text = "Show Alternatives"
            tlstripitem.Tag = 4
            tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            tlstripitem.Image = ImageFlex.Images(12)
            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If
    End Sub
    'Context menu to  delete prescription/prescription item
    Private Sub StripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case CType(sender, ToolStripMenuItem).Tag
                Case 1

                    If MessageBox.Show("Are you sure you want to delete prescription?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        Dim PatId As Long = 0
                        Dim ProvId As Long = 0
                        If _RxBuisnessLayer.PrescriptionCol.Count > 0 Then
                            PatId = _RxBuisnessLayer.PrescriptionCol.Item(0).PatientID
                            ProvId = _RxBuisnessLayer.PrescriptionCol.Item(0).ProviderID
                        End If
                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True ''used for drug delete flag on rx, mx grid

                        If _RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Add Then
                            _RxBuisnessLayer.PrescriptionCol.Clear()
                            _Flex.Rows.Count = 1

                        Else 'it is opened in Edit mode
                            For i As Integer = _Flex.Rows.Count - 1 To 1 Step -1
                                If _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID <> 0 Then 'this means that the prescription id is present therefore remove those items from the collection and just hide the respective row/rows
                                    _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Status = "D"
                                    _RxBuisnessLayer.PrescriptionCol.Item(i - 1).State = "D"
                                    _Flex.Rows.Item(i).Visible = False
                                    Dim arg As New gloSureScript.PrescriptionMedicationEventArgs(_RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID)
                                    RaiseEvent DeleteCorrespondingMedication(Me, arg)

                                Else ' there is no PrescriptionID that means that item is newly added therefeore remove that item from the Rx collection and delete the respective row/rows
                                    _RxBuisnessLayer.PrescriptionCol.RemoveAt(i - 1) 'delete from collection
                                    _Flex.Rows.Remove(i)
                                    '_Flex.Row = _Flex.Row - 1
                                End If
                            Next
                            '_RxBuisnessLayer.DeletePrescription()
                            '_RxBuisnessLayer.PrescriptionCol.Clear()
                            '_Flex.Rows.Count = 1
                        End If
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DeletePrescription, gloAuditTrail.ActivityType.Delete, "All Prescription item were deleted", PatId, 0, ProvId, gloAuditTrail.ActivityOutCome.Success)

                        'Code added by Ashish on 18th March to remove
                        'Drug details control on deleting Drug
                        RaiseEvent StripItemClick(sender, e)
                    End If


                Case 2
                    Dim i As Integer 'declared to get the value of current row
                    i = _Flex.Row
                    If MessageBox.Show("Are you sure you want to delete prescription item?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        If i > 0 Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button

                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRxDeleteFlag = True ''used for drug delete flag on rx, mx grid 

                            'If treeindex >= 0 Then

                            'If Not trPrescriptionDetails.SelectedNode Is trPrescriptionDetails.Nodes.Item(0) Then '''''by sagar start here
                            'Check if a valid row is selected
                            'If valid row is selected delete that row as well as item from PRescription collection
                            If Not IsNothing(_Flex.Rows.Item(0)) Then
                                Dim key As Long
                                Dim PatId As Long = 0
                                Dim ProvId As Long = 0
                                If _RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                                    'just change the status of that Rx item to 'D'(means deleted) in the prescription collection and make that flex grids row width =0,
                                    'therefore user will feel that the item is deleted but it is actually not deleted from the grid and the tiem is still present in the prescription collection
                                    If _RxBuisnessLayer.PrescriptionCol.Count > 0 Then
                                        Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                                        For CollectionCnt As Integer = 0 To _RxBuisnessLayer.PrescriptionCol.Count - 1
                                            If FlexRowNo = _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).ItemNumber Then
                                                If _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).PrescriptionID <> 0 Then
                                                    _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).Status = "D"
                                                    _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).State = "D"
                                                    _Flex.Rows.Item(_Flex.Row).Visible = False

                                                    'WritEventPasstheParameter as PrescriptionID
                                                    Dim arg As New gloSureScript.PrescriptionMedicationEventArgs(_RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).PrescriptionID)
                                                    RaiseEvent DeleteCorrespondingMedication(Me, arg)
                                                    ' MakeCorrespondingMedicationCollection(_RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).PrescriptionID)
                                                    Exit For ''''once item from collection and row from grid is deleted then exit for
                                                Else 'new item added so delete it directly

                                                    _RxBuisnessLayer.PrescriptionCol.RemoveAt(CollectionCnt) 'delete from collection
                                                    key = _Flex.GetData(_Flex.Row, COL_DRUGID)
                                                    _Flex.Rows.Remove(i)
                                                    If _Flex.Row > 0 Then
                                                        _Flex.Row = _Flex.Row - 1
                                                    End If                                                       'RaiseEvent to RemoveControl(i.e customprescription)
                                                    RaiseEvent PrescriptionItemDeleted()
                                                    Exit For ''''once item from collection and row from grid is deleted then exit for
                                                End If

                                            End If

                                        Next
                                    End If


                                Else 'this means the transaction mode is ADD therefore directly can follow the previous code
                                    If _RxBuisnessLayer.PrescriptionCol.Count > 0 Then
                                        ''''''''bug fix 6250
                                        Dim Rx_Id As Long = _Flex.GetData(_Flex.Row, COL_PRESCRIPTIONID)
                                        If Rx_Id <> 0 Then
                                            'Delete  this prescription item from Prescription table
                                            '_RxBuisnessLayer.DeletePrescription(Rx_Id)
                                        End If
                                        ''''''''bug fix 6250
                                        Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                                        For CollectionCnt As Integer = 0 To _RxBuisnessLayer.PrescriptionCol.Count - 1
                                            If FlexRowNo = _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).ItemNumber Then
                                                _RxBuisnessLayer.PrescriptionCol.RemoveAt(CollectionCnt) 'FlexRowNodelete from collection that item whose value matches with row no colums in Rxgrid
                                                key = _Flex.GetData(_Flex.Row, COL_DRUGID)
                                                PatId = _Flex.GetData(_Flex.Row, COL_PATIENTID)
                                                key = _Flex.GetData(_Flex.Row, COL_ProviderID)
                                                _Flex.Rows.Remove(i)
                                                Exit For ''''once item from collection and row from grid is deleted then exit for
                                            End If
                                        Next
                                        '_RxBuisnessLayer.PrescriptionCol.RemoveAt(_Flex.Row - 1) 'delete from collection
                                        'key = _Flex.GetData(_Flex.Row, COL_DRUGID)
                                        'PatId = _Flex.GetData(_Flex.Row, COL_PATIENTID)
                                        'key = _Flex.GetData(_Flex.Row, COL_ProviderID)
                                        '_Flex.Rows.Remove(i)

                                    Else
                                        _Flex.Rows.Count = 1
                                    End If

                                    RaiseEvent PrescriptionItemDeleted()
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DeletePrescription, gloAuditTrail.ActivityType.Delete, "Prescription item was deleted", PatId, 0, ProvId, gloAuditTrail.ActivityOutCome.Success)
                                End If '_RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit 


                            End If
                        End If
                        'Code added by Ashish on 18th March to remove
                        'Drug details control on deleting Drug
                        RaiseEvent StripItemClick(sender, e)
                    End If
                Case 3 '''''''show the eRx message status as mesaage box popup
                    '''''''fetch the eRxStatus and eRxStatusMessage values from the selected grid row. 
                    Dim eRxStatus As String = ""
                    Dim eRxStatusMessage As String = ""

                    If Not IsNothing(_Flex.GetData(_Flex.Row, COL_eRxStatus)) Then
                        eRxStatus = _Flex.GetData(_Flex.Row, COL_eRxStatus)
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
                    Else ''''''bug fixed 2780
                        MessageBox.Show("No status message available.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case 4
                    Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                    For CollectionCnt As Integer = 0 To _RxBuisnessLayer.PrescriptionCol.Count - 1
                        If FlexRowNo = _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).ItemNumber Then
                            SelectedMedication = _RxBuisnessLayer.PrescriptionCol.Item(CollectionCnt).Medication
                        End If
                    Next
                    RaiseEvent DrugFormularyRequested(_Flex.Row)
                    ''Add code on show Alternatives
                Case 5
                    Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                    RaiseEvent DrugPARequested(FlexRowNo)
                Case 6
                    Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                    RaiseEvent CancelRxRequested(FlexRowNo, "C")
                Case 7
                    Dim FlexRowNo As Integer = _Flex.GetData(_Flex.Row, COL_RowNumber)
                    RaiseEvent CancelRxRequested(FlexRowNo, "D")
                    ''Add code for CANCELRX
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error Deleting Prescription"
            Throw objex
        End Try

    End Sub

    Private Sub SetFlexgridColumns()
        With _Flex
            '.Redraw = False
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn ''as discussed commented to resolve the incident #00002884 
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
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

            'Dim _Width As Single = (.Width - 20) / 11
            Dim _Width As Single = .Width / 10
            'set column value
            .Cols(COL_SELECT).Width = 18  '_Width * 2
            .Cols(COL_PRESCRIPTIONID).Width = 0 '_Width * 1
            .Cols(COL_VISITID).Width = 0 '_Width * 1
            .Cols(COL_PATIENTID).Width = 0 '_Width * 1
            .Cols(COL_MEDICATION).Width = _Width * 4 ''also called drug name
            .Cols(COl_INFOBUTTON).Width = _Width * 0.3
            .Cols(COL_PDR).Width = 0

            .Cols(COL_IseRxed).Width = 0

            .Cols(COL_DOSAGE).Width = 0 '_Width * 1' width changed in 6030 for Rx feild changes

            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsEpaServiceEnabledProvider Then
                .Cols(COL_EPAStatus).Width = _Width * 1
            Else
                .Cols(COL_EPAStatus).Width = 0
            End If

            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = True Then
                'show the formulary data columns
                .Cols(COL_RxDrugType).Width = _Width * 1 '_Width * 1
                .Cols(COL_FormularyStatus).Width = _Width * 1.6 '_Width * 1
                .Cols(COL_CoverageIndicator).Width = 0 '_Width * 0.8 '_Width * 1
                .Cols(COL_CopayIndicator).Width = _Width * 1 '_Width * 1
            Else
                'Dont show the formulary data columns
                .Cols(COL_RxDrugType).Width = 0
                .Cols(COL_FormularyStatus).Width = 0
                .Cols(COL_CoverageIndicator).Width = 0
                .Cols(COL_CopayIndicator).Width = 0
            End If


            .Cols(COL_ROUTE).Width = 0 '_Width * 0.6  width changed in 6030 for Rx feild changes
            .Cols(COL_FREQUENCY).Width = _Width * 1.5 '2
            .Cols(COL_DURATION).Width = _Width * 0.9 '_Width * 1
            .Cols(COL_AMOUNT).Width = _Width * 0.8 ''also called Dispense
            .Cols(COL_REFILLS).Width = _Width * 0.6
            .Cols(COL_STARTDATE).Width = _Width * 1.0
            .Cols(COL_ENDDATE).Width = _Width * 1.0
            .Cols(COL_NOTES).Width = 0
            .Cols(Col_REASON).Width = 0
            .Cols(COL_METHOD).Width = _Width * 1.1 '0 width changed in 6030 for Rx feild changes
            .Cols(COL_MAYSUBSTITUTE).Width = _Width * 1.5 'width changed in 6030 for Rx feild changes
            .Cols(COL_PRESCRIPTIONDATE).Width = 0 '_Width * 1
            .Cols(COL_DRUGID).Width = 0
            .Cols(COL_INFLAG).Width = 0 '18 '_Width * 1
            .Cols(COL_LOTNUMBER).Width = 0
            .Cols(COL_EXPDATE).Width = 0
            .Cols(COL_USERID).Width = 0
            .Cols(COL_USERNAME).Width = _Width * 1.0

            'code added by supriya by 19/7/2008
            .Cols(COL_Narcotic).Width = 0
            .Cols(COL_ProviderID).Width = 0
            .Cols(COL_PharmacyID).Width = 0

            'For De-Normalization
            .Cols(COL_NDCCode).Width = 0 '_Width * 1 '
            .Cols(COL_mpid).Width = 0
            .Cols(COL_DRUGFORM).Width = 0 '_Width * 1 width changed in 6030 for Rx feild changes
            .Cols(COL_DRUGQTYQUALIFIER).Width = 0
            'For De-Normalization

            'For Pharmacy
            .Cols(COL_NCPDPID).Width = 0
            .Cols(COL_PharmacyContactID).Width = 0
            .Cols(COL_PharmacyName).Width = _Width * 1.4
            .Cols(COL_PharmacyAddressline1).Width = 0
            .Cols(COL_PharmacyAddressline2).Width = 0
            .Cols(COL_PharmacyCity).Width = 0
            .Cols(COL_PharmacysState).Width = 0
            .Cols(COL_PharmacyZip).Width = 0
            .Cols(COL_PharmacyEmail).Width = 0
            .Cols(COL_PharmacyFax).Width = 0
            .Cols(COL_PharmacyPhone).Width = 0
            .Cols(COL_PharmacyServiceLevel).Width = 0
            'For Pharmacy

            'Temperory Formulary Information - Grid
            .Cols(COL_FormularyAlternatives_Grid).Width = 0 ' 5
            'Temperory Formulary Information - Richtextbox
            .Cols(COL_FormularyInformation_RchTxt).Width = 0 ' 5

            .Cols(COL_eRxStatus).Width = _Width * 1.5
            .Cols(COL_eRxStatusMessage).Width = 0
            .Cols(COL_RowNumber).Width = 0 '_Width * 1

            .Cols(COL_STARTDATE).DataType = GetType(System.DateTime)
            .Cols(COL_STARTDATE).Format = "MM/dd/yyyy"

            .Cols(COL_ENDDATE).DataType = GetType(System.DateTime)
            .Cols(COL_ENDDATE).Format = "MM/dd/yyyy"

            .Cols(COL_SELECT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PRESCRIPTIONID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_IseRxed).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_VISITID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PATIENTID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_MEDICATION).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COl_INFOBUTTON).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PDR).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterTop
            .Cols(COL_DOSAGE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(COL_RxDrugType).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_EPAStatus).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_FormularyStatus).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_CoverageIndicator).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_CoverageIndicator).ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_CopayIndicator).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_CopayIndicator).ImageAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(COL_ROUTE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_FREQUENCY).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_DURATION).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_AMOUNT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_REFILLS).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_STARTDATE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_ENDDATE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_NOTES).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(Col_REASON).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_METHOD).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_MAYSUBSTITUTE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PRESCRIPTIONDATE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_DRUGID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_INFLAG).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_LOTNUMBER).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_EXPDATE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_USERID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_USERNAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            'code added by supriya 19/7/2008
            .Cols(COL_Narcotic).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_ProviderID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            'For De-Normalization
            .Cols(COL_NDCCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_mpid).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_DRUGFORM).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_DRUGQTYQUALIFIER).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            'For De-Normalization

            'Pharmacy Information
            .Cols(COL_NCPDPID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyContactID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyAddressline1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyAddressline2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyCity).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacysState).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyZip).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyEmail).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyFax).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyPhone).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PharmacyServiceLevel).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            'Pharmacy Information

            'Temperory Formulary Information - Grid
            .Cols(COL_FormularyAlternatives_Grid).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            'Temperory Formulary Information - Richtextbox
            .Cols(COL_FormularyInformation_RchTxt).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(COL_eRxStatus).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_eRxStatusMessage).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_RowNumber).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_FormularyQueried).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            'set column header
            .SetData(0, COL_SELECT, "")
            .SetData(0, COL_PRESCRIPTIONID, "Prescription ID")
            .SetData(0, COL_IseRxed, "IseRxed")
            .SetData(0, COL_VISITID, "Visit ID")
            .SetData(0, COL_PATIENTID, "Patient ID")
            'renamed in 6030 for Rx feild chnges
            '.SetData(0, COL_MEDICATION, "Drug Name") ''this is called as Medication but in image it is DrugName
            '.SetData(0, COL_FREQUENCY, "Frequency")
            '.SetData(0, COL_AMOUNT, "Dispense") ''this is called as Amount but in image it is Dispense
            '.SetData(0, COL_MAYSUBSTITUTE, "May Substitute")
            .SetData(0, COL_MEDICATION, "Drug")
            .SetData(0, COl_INFOBUTTON, "")
            .SetData(0, COL_PDR, "")
            .SetData(0, COL_FREQUENCY, "Patient Directions")
            .SetData(0, COL_AMOUNT, "Quantity")
            .SetData(0, COL_MAYSUBSTITUTE, "Allow Substitution")

            .SetData(0, COL_DOSAGE, "Dosage")
            .SetData(0, COL_RxDrugType, "Rx Type")
            .SetData(0, COL_EPAStatus, "PA Status")
            .SetData(0, COL_FormularyStatus, "Formulary Status")
            .SetData(0, COL_CoverageIndicator, "Coverage")
            .SetData(0, COL_CopayIndicator, "Copay")
            .SetData(0, COL_ROUTE, "Route")
            .SetData(0, COL_DURATION, "Duration")
            .SetData(0, COL_REFILLS, "Refills")
            .SetData(0, COL_STARTDATE, "Start Date")
            .SetData(0, COL_ENDDATE, "End Date")
            .SetData(0, COL_NOTES, "Notes")
            .SetData(0, Col_REASON, "Reason")
            '.SetData(0, COL_METHOD, "Method")
            .SetData(0, COL_METHOD, "Issue Method")
            .SetData(0, COL_PRESCRIPTIONDATE, "Prescription Date")
            .SetData(0, COL_DRUGID, "Drug ID")
            .SetData(0, COL_INFLAG, "lnflag")
            .SetData(0, COL_LOTNUMBER, "Lot No")
            .SetData(0, COL_EXPDATE, "Expiration date")
            .SetData(0, COL_USERID, "User ID")
            'renamed in 6030 for Rx feild chnges
            '.SetData(0, COL_USERNAME, "User Name")
            .SetData(0, COL_USERNAME, "User")


            'code added by supriya 19/7/2008
            .SetData(0, COL_Narcotic, "Narcotic")
            .SetData(0, COL_PharmacyID, "PharmacyID")
            .SetData(0, COL_ProviderID, "ProviderID")

            'For De-Normalization
            .SetData(0, COL_NDCCode, "NDC Code")
            .SetData(0, COL_mpid, "MPID")
            .SetData(0, COL_DRUGFORM, "Drug Form")
            .SetData(0, COL_DRUGQTYQUALIFIER, "Drug Unit")
            'For De-Normalization

            'Pharmacy Information
            .SetData(0, COL_NCPDPID, "NCPDPID")
            .SetData(0, COL_PharmacyContactID, "Ph ContactID")
            'renamed in 6030 for Rx feild chnges
            '.SetData(0, COL_PharmacyName, "Pharmacy Name")
            .SetData(0, COL_PharmacyName, "Pharmacy")
            .SetData(0, COL_PharmacyAddressline1, "Ph Address1")
            .SetData(0, COL_PharmacyAddressline2, "Ph Address2")
            .SetData(0, COL_PharmacyCity, "Pharmacy City")
            .SetData(0, COL_PharmacysState, "Pharmacy State")
            .SetData(0, COL_PharmacyZip, "Pharmacy Zip")
            .SetData(0, COL_PharmacyEmail, "Pharmacy Email")
            .SetData(0, COL_PharmacyFax, "Pharmacy Fax")
            .SetData(0, COL_PharmacyPhone, "Pharmacy Phone")
            .SetData(0, COL_PharmacyServiceLevel, "Ph Service level")
            'Pharmacy Information

            'Temperory Formulary Information - Grid
            .SetData(0, COL_FormularyAlternatives_Grid, "Temp Formulary Grid")
            'Temperory Formulary Information - rich text box
            .SetData(0, COL_FormularyInformation_RchTxt, "Temp Formulary Richtextbox")

            'renamed in 6030 for Rx feild chnges
            '.SetData(0, COL_eRxStatus, "eRx Status")
            '.SetData(0, COL_eRxStatusMessage, "eRx Status Message")
            .SetData(0, COL_eRxStatus, "Status")
            .SetData(0, COL_eRxStatusMessage, "eRx Message")
            .SetData(0, COL_RowNumber, "RowNumber")
            .SetData(0, COL_FormularyQueried, "Formulary Check")
            .SetData(0, COL_FS, "FS")

            'set visiblity for column 
            .Cols(COL_SELECT).Visible = True
            .Cols(COL_PRESCRIPTIONID).Visible = False
            .Cols(COL_IseRxed).Visible = True
            .Cols(COL_VISITID).Visible = False
            .Cols(COL_PATIENTID).Visible = False
            .Cols(COL_MEDICATION).Visible = True ''drug name

            If EducationMaterialEnabled Then
                .Cols(COl_INFOBUTTON).Visible = True
            Else
                .Cols(COl_INFOBUTTON).Visible = False
            End If

            .Cols(COL_DOSAGE).Visible = False 'True changes done in 6030 for Rx feild Changes

            .Cols(COL_RxDrugType).Visible = True
            .Cols(COL_EPAStatus).Visible = True
            .Cols(COL_FormularyStatus).Visible = True
            .Cols(COL_CoverageIndicator).Visible = True
            .Cols(COL_CopayIndicator).Visible = True



            .Cols(COL_ROUTE).Visible = False 'True
            .Cols(COL_FREQUENCY).Visible = True
            .Cols(COL_DURATION).Visible = True
            .Cols(COL_AMOUNT).Visible = True ''the dispense coloum in image
            .Cols(COL_REFILLS).Visible = True
            .Cols(COL_STARTDATE).Visible = True
            .Cols(COL_ENDDATE).Visible = True
            .Cols(COL_NOTES).Visible = False
            .Cols(Col_REASON).Visible = False
            .Cols(COL_METHOD).Visible = True 'False changes done in 6030 for Rx feild Changes
            .Cols(COL_MAYSUBSTITUTE).Visible = True 'False changes done in 6030 for Rx feild Changes
            .Cols(COL_PRESCRIPTIONDATE).Visible = False
            .Cols(COL_DRUGID).Visible = False
            .Cols(COL_INFLAG).Visible = False
            .Cols(COL_LOTNUMBER).Visible = False
            .Cols(COL_EXPDATE).Visible = False
            .Cols(COL_USERID).Visible = False
            .Cols(COL_USERNAME).Visible = True

            'code added by supriya 19/7/2008

            .Cols(COL_Narcotic).Visible = False
            .Cols(COL_ProviderID).Visible = False
            .Cols(COL_PharmacyID).Visible = False

            'For De-Normalization
            .Cols(COL_NDCCode).Visible = True
            .Cols(COL_mpid).Visible = False
            .Cols(COL_DRUGFORM).Visible = True
            .Cols(COL_DRUGQTYQUALIFIER).Visible = False
            'For De-Normalization

            'Pharmacy Information
            .Cols(COL_NCPDPID).Visible = False
            .Cols(COL_PharmacyContactID).Visible = False
            .Cols(COL_PharmacyName).Visible = True '--False
            .Cols(COL_PharmacyAddressline1).Visible = False
            .Cols(COL_PharmacyAddressline2).Visible = False
            .Cols(COL_PharmacyCity).Visible = False
            .Cols(COL_PharmacysState).Visible = False
            .Cols(COL_PharmacyZip).Visible = False
            .Cols(COL_PharmacyEmail).Visible = False
            .Cols(COL_PharmacyFax).Visible = False
            .Cols(COL_PharmacyPhone).Visible = False
            .Cols(COL_PharmacyServiceLevel).Visible = False
            'Pharmacy Information

            'Temperory Formulary Information - Grid
            .Cols(COL_FormularyAlternatives_Grid).Visible = True
            'Temperory Formulary Information - Richtextbox
            .Cols(COL_FormularyInformation_RchTxt).Visible = True

            .Cols(COL_eRxStatus).Visible = True
            .Cols(COL_eRxStatusMessage).Visible = True
            .Cols(COL_RowNumber).Visible = True
            .Cols(COL_FormularyQueried).Visible = False
            .Cols(COL_FS).Visible = False
            .Cols(COL_IseRxed).Visible = False

            ' set column editing properties.

            '.Cols(COL_SELECT).AllowEditing = True
            .Cols(COL_PRESCRIPTIONID).AllowEditing = False
            .Cols(COL_IseRxed).AllowEditing = False
            .Cols(COL_VISITID).AllowEditing = False
            .Cols(COL_PATIENTID).AllowEditing = False
            .Cols(COL_MEDICATION).AllowEditing = False ''drug name
            .Cols(COl_INFOBUTTON).AllowEditing = False ''drug name
            .Cols(COL_PDR).AllowEditing = False
            .Cols(COL_DOSAGE).AllowEditing = False

            .Cols(COL_NDCCode).AllowEditing = False
            .Cols(COL_mpid).AllowEditing = False
            .Cols(COL_DRUGFORM).AllowEditing = False
            .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False

            .Cols(COL_ROUTE).AllowEditing = False
            .Cols(COL_FREQUENCY).AllowEditing = False
            .Cols(COL_DURATION).AllowEditing = False
            .Cols(COL_AMOUNT).AllowEditing = False ''the dispense coloum in image
            .Cols(COL_REFILLS).AllowEditing = False
            .Cols(COL_STARTDATE).AllowEditing = False
            .Cols(COL_ENDDATE).AllowEditing = False
            .Cols(COL_NOTES).AllowEditing = False
            .Cols(Col_REASON).AllowEditing = False
            .Cols(COL_METHOD).AllowEditing = False
            .Cols(COL_MAYSUBSTITUTE).AllowEditing = False
            .Cols(COL_PRESCRIPTIONDATE).AllowEditing = False
            .Cols(COL_DRUGID).AllowEditing = False
            .Cols(COL_INFLAG).AllowEditing = False
            .Cols(COL_LOTNUMBER).AllowEditing = False
            .Cols(COL_EXPDATE).AllowEditing = False
            .Cols(COL_USERID).AllowEditing = False
            .Cols(COL_USERNAME).AllowEditing = False

            'code added by supriya 19/7/2008

            .Cols(COL_Narcotic).AllowEditing = False
            .Cols(COL_ProviderID).AllowEditing = False
            .Cols(COL_PharmacyID).AllowEditing = False

            'For De-Normalization
            .Cols(COL_NDCCode).AllowEditing = False
            .Cols(COL_mpid).AllowEditing = False
            .Cols(COL_DRUGFORM).AllowEditing = False
            .Cols(COL_DRUGQTYQUALIFIER).AllowEditing = False
            'For De-Normalization

            'Temperory Formulary Information - Grid
            .Cols(COL_FormularyAlternatives_Grid).AllowEditing = False
            'Temperory Formulary Information - richtextbox
            .Cols(COL_FormularyInformation_RchTxt).AllowEditing = False

            .Cols(COL_RxDrugType).AllowEditing = False
            .Cols(COL_EPAStatus).AllowEditing = False
            .Cols(COL_FormularyStatus).AllowEditing = False
            .Cols(COL_CopayIndicator).AllowEditing = False
            .Cols(COL_PharmacyName).AllowEditing = False

            .Cols(COL_eRxStatus).AllowEditing = False
            .Cols(COL_eRxStatusMessage).AllowEditing = False
            .Cols(COL_RowNumber).AllowEditing = False
            .Cols(COL_FormularyQueried).AllowEditing = False
            .Redraw = True
        End With
    End Sub



    ' 'Add Row to Flexgrid and Set data in flexgrid for selected row after making changes to custom prescription control.
    Public Sub AddNewPrescription(ByVal _Prescription As Prescription, ByVal EligStatus As RxBusinesslayer.EligibilityStatus)

        Dim rowcount As Int16 = _RxBuisnessLayer.PrescriptionCol.Count
        '_Flex.Rows.Count = 1

        If _Prescription.mpid = 0 Or _Prescription.mpid = Nothing Then
            Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                _Prescription.mpid = oDIBHelper.GetMarketedProductId(_Prescription.NDCCode)
            End Using
        End If
        If (Not _Prescription.RxType.Any()) Then
            _Prescription.RxType = _RxBuisnessLayer.GetRxType(_Prescription.mpid)
        End If


        With _Flex

            RemoveHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
            .Rows.Add()
            AddHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
            If (rowcount > -1) AndAlso (rowcount < _Flex.Rows.Count) Then
                .SetCellCheck(rowcount, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                .SetData(rowcount, COL_PRESCRIPTIONID, _Prescription.PrescriptionID)
                .SetData(rowcount, COL_IseRxed, _Prescription.IseRxed)
                .SetData(rowcount, COL_VISITID, _Prescription.VisitID)
                .SetData(rowcount, COL_PATIENTID, _PatientID) ''globalPatient.gnPatientID) 
                If _Prescription.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Prescription.Route) Then
                    .SetData(rowcount, COL_MEDICATION, _Prescription.Medication & " " & _Prescription.Route)
                Else
                    .SetData(rowcount, COL_MEDICATION, _Prescription.Medication)
                End If
                .SetData(rowcount, COL_EPAStatus, _Prescription.PriorAuthorizationStatus)
                .SetData(rowcount, COL_DOSAGE, _Prescription.Dosage)
                .SetData(rowcount, COL_RxDrugType, _Prescription.RxType)
                .SetData(rowcount, COL_ROUTE, _Prescription.Route)
                .SetData(rowcount, COL_FREQUENCY, _Prescription.Frequency)
                .SetData(rowcount, COL_DURATION, _Prescription.Duration)
                .SetData(rowcount, COL_AMOUNT, _Prescription.Amount) 'dispense
                .SetData(rowcount, COL_REFILLS, _Prescription.Refills)
                .SetData(rowcount, COL_STARTDATE, CType(_Prescription.Startdate, Date))

                Dim _strEndDate As String = ""
                Dim odt As DateTime = Now
                Try
                    If (Date.TryParse(_Prescription.Enddate.ToString(), odt) = False) Then
                        _strEndDate = ""
                    Else
                        _strEndDate = _Prescription.Enddate
                    End If
                    If odt = Date.MinValue Then
                        _strEndDate = ""
                    End If
                Catch ex As Exception
                    _strEndDate = ""
                End Try

                If _strEndDate = "" Then
                    .SetData(rowcount, COL_ENDDATE, Nothing)
                Else
                    .SetData(rowcount, COL_ENDDATE, CType(_Prescription.Enddate, Date))
                End If


                .SetData(rowcount, COL_NOTES, _Prescription.Notes)
                .SetData(rowcount, Col_REASON, _Prescription.ReasontoOverride)

                'added new logic for issue method in 6052 (narcotic=print,normal drug & eRx Pharmacy =eRx)
                If _Prescription.Method = "" Then
                    If _Prescription.PhNCPDPID <> "" Then
                        _Prescription.Method = "eRx"
                    Else
                        _Prescription.Method = "Print"
                    End If
                Else
                    If _Prescription.Method = "eRx" Then
                        If _Prescription.PhNCPDPID <> "" Then
                            _Prescription.Method = "eRx"
                        Else
                            _Prescription.Method = "Print"
                        End If
                    End If
                End If

                .SetData(rowcount, COL_METHOD, _Prescription.Method)
                .SetData(rowcount, COL_MAYSUBSTITUTE, If(_Prescription.Maysubstitute, "Yes", "No"))
                .SetData(rowcount, COL_PRESCRIPTIONDATE, _Prescription.Prescriptiondate)
                .SetData(rowcount, COL_DRUGID, _Prescription.DrugID)
                .SetData(rowcount, COL_INFLAG, _Prescription.State) 'lnflag
                .SetData(rowcount, COL_LOTNUMBER, _Prescription.LotNo)
                .SetData(rowcount, COL_EXPDATE, _Prescription.ExpirationDate)
                .SetData(rowcount, COL_USERID, _Prescription.UserId)
                .SetData(rowcount, COL_USERNAME, _Prescription.UserName)

                'code added by supriya 19/7/2008
                .SetData(rowcount, COL_Narcotic, _Prescription.IsNarcotics)
                .SetData(rowcount, COL_ProviderID, _Prescription.ProviderID)
                .SetData(rowcount, COL_PharmacyID, _Prescription.PharmacyID)

                'For De-Normalization
                .SetData(rowcount, COL_NDCCode, _Prescription.NDCCode)
                '.SetData(rowcount, COL_Narcotic, _Prescription.IsNarcotics) 'saves the narcotic type value
                .SetData(rowcount, COL_mpid, _Prescription.mpid)
                .SetData(rowcount, COL_DRUGFORM, _Prescription.DosageForm)
                .SetData(rowcount, COL_DRUGQTYQUALIFIER, _Prescription.StrengthUnit)
                'For De-Normalization

                'For Pharmacy
                .SetData(rowcount, COL_NCPDPID, _Prescription.PhNCPDPID)
                .SetData(rowcount, COL_PharmacyContactID, _Prescription.PhContactID)
                .SetData(rowcount, COL_PharmacyName, _Prescription.PharmacyName)
                .SetData(rowcount, COL_PharmacyAddressline1, _Prescription.PhAddressline1)
                .SetData(rowcount, COL_PharmacyAddressline2, _Prescription.PhAddressline2)
                .SetData(rowcount, COL_PharmacyCity, _Prescription.PhCity)
                .SetData(rowcount, COL_PharmacysState, _Prescription.PhState)
                .SetData(rowcount, COL_PharmacyZip, _Prescription.PhZip)
                .SetData(rowcount, COL_PharmacyEmail, _Prescription.PhEmail)
                .SetData(rowcount, COL_PharmacyFax, _Prescription.PhFax)
                .SetData(rowcount, COL_PharmacyPhone, _Prescription.PhPhone)
                .SetData(rowcount, COL_PharmacyServiceLevel, _Prescription.PhServiceLevel)
                'For Pharmacy

                ''''bug fix for 7882
                Dim _ItemNumber As Integer
                If _RxBuisnessLayer.PrescriptionCol.Count = 1 Then
                    _ItemNumber = rowcount
                    _ItemNumber = _ItemNumber - 1

                Else
                    _ItemNumber = _RxBuisnessLayer.PrescriptionCol.Item(_RxBuisnessLayer.PrescriptionCol.Count - 2).ItemNumber
                    _ItemNumber = _ItemNumber + 1

                End If

                'Infobutton
                .SetCellImage(rowcount, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)

                .SetData(rowcount, COL_RowNumber, _ItemNumber)
                .SetData(rowcount, COL_FormularyQueried, _Prescription.IsFormularyQueried)

                '' TODO : Need to check if this is needed
                ''.SetData(rowcount, COL_FS, "")

                _Prescription.ItemNumber = _ItemNumber ''the item number will keep track for collection item number that will correspond to row number in the grid
                ''''bug fix for 7882

                .Redraw = True
                RemoveHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
                .Row = rowcount
                AddHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
            End If
        End With

        RowIndex = rowcount

        If EligStatus <> RxBusinesslayer.EligibilityStatus.NotChecked Then
            ShowFormulary(_Flex.RowSel)
        Else
            Me.PDRProgramsRequested_Event(_Flex.RowSel)
        End If


    End Sub

    Public Function GetMaxItemNo() As Integer

        Try
            If _Flex.Rows.Count > 1 Then
                Dim maxNo As Integer = _Flex.Rows(1)(COL_RowNumber)
                For i As Integer = 1 To _Flex.Rows.Count

                    If maxNo > _Flex.Rows(i)(COL_RowNumber) Then

                    End If
                Next

            End If
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    Public Sub UpdateEPAStatus()
        Try
            Dim row As Integer
            Dim _Prescription As Prescription
            If IsNothing(_RxBuisnessLayer.PrescriptionCol) OrElse _RxBuisnessLayer.PrescriptionCol.Count <= 0 Then
                Exit Sub
            End If
            For row = 0 To _RxBuisnessLayer.PrescriptionCol.Count - 1
                _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(row)
                If _Prescription.State <> "D" AndAlso _Prescription.ItemNumber = row Then
                    If _Flex.Rows.Count > (row + 1) Then
                        _Flex.SetData(row + 1, COL_EPAStatus, _Prescription.PriorAuthorizationStatus)
                    End If
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, "@UpdateEPAStatusInfo " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub UpdateEPAStatusRow(ByVal row As Integer)
        Try
            Dim _Prescription As Prescription
            If _RxBuisnessLayer.PrescriptionCol.ElementAtOrDefault(row) IsNot Nothing Then
                _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(row)
                If _Prescription.State <> "D" AndAlso _Prescription.ItemNumber = row Then
                    If _Flex.Rows.Count > (row + 1) Then
                        _Flex.SetData(row + 1, COL_EPAStatus, _Prescription.PriorAuthorizationStatus)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, "@UpdateEPAStatusInfo " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Public Sub UpdatePDRStatus()
    '    Try
    '        Dim row As Integer
    '        Dim _Prescription As Prescription
    '        If IsNothing(_RxBuisnessLayer.PrescriptionCol) OrElse _RxBuisnessLayer.PrescriptionCol.Count <= 0 Then
    '            Exit Sub
    '        End If

    '        For row = 0 To _RxBuisnessLayer.PrescriptionCol.Count - 1
    '            _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(row)
    '            If _Prescription.State <> "D" AndAlso _Prescription.ItemNumber = row Then
    '                If _Flex.Rows.Count > (row + 1) Then
    '                    _Flex.SetCellImage(row + 1, COL_PDR, If(_Prescription.PCTransactionID <> 0, Global.gloUserControlLibrary.My.Resources.Resources.PDR, Global.gloUserControlLibrary.My.Resources.Resources.PDRGrey))
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, "@UpdateEPAStatusInfo " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    'Public Sub SetPDRLoadingStatus(ByVal row As Integer)
    '    Try
    '        Dim _Prescription As Prescription
    '        If _RxBuisnessLayer.PrescriptionCol.ElementAtOrDefault(row) IsNot Nothing Then
    '            _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(row)
    '            If _Prescription.State <> "D" AndAlso _Prescription.ItemNumber = row Then
    '                If _Flex.Rows.Count > (row + 1) Then
    '                    _Flex.SetCellImage(row + 1, COL_PDR, Global.gloUserControlLibrary.My.Resources.Resources.PDRWaiting)
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, "@UpdateEPAStatusInfo " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    'Public Sub UpdatePDRStatusRow(ByVal row As Integer)
    '    Try
    '        Dim _Prescription As Prescription
    '        If _RxBuisnessLayer.PrescriptionCol.ElementAtOrDefault(row) IsNot Nothing Then
    '            _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(row)
    '            If _Prescription.State <> "D" AndAlso _Prescription.ItemNumber = row Then
    '                If _Flex.Rows.Count > (row + 1) Then
    '                    _Flex.SetCellImage(row + 1, COL_PDR, If(_Prescription.PCTransactionID <> 0, Global.gloUserControlLibrary.My.Resources.Resources.PDR, Global.gloUserControlLibrary.My.Resources.Resources.PDRGrey))
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, "@UpdateEPAStatusInfo " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub


    Public Sub UpdateDrugFormularyInfo(ByVal Row As Integer, ByVal Status As FormularyStatus, Optional ByVal Copay As FormularyCopay = Nothing)
        If Row >= 1 Then
            If Status IsNot Nothing Then
                _Flex.SetData(Row, COL_FS, Status.fs)
                Using fs As New StatusConverter(Status.fs)
                    _Flex.SetData(Row, COL_FormularyStatus, fs.DisplayText)
                    _Flex.SetCellImage(Row, COL_FormularyStatus, fs.DisplayIcon)

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.FSTrigger, "Formulary queried ", _PatientID, 0, _nLoginProviderid, gloAuditTrail.ActivityOutCome.Success)

                End Using
            End If

            If Copay IsNot Nothing Then
                Using cop As New CopayConverter(Copay.cop)
                    _Flex.SetData(Row, COL_CopayIndicator, cop.DisplayText)
                End Using
            Else
                _Flex.SetData(Row, COL_CopayIndicator, "")
            End If
        End If
    End Sub


    Public Sub UpdateFlexgridData(ByVal _Prescriptions As Prescriptions)
        'Dim RowNumber As Int16 = _RxBuisnessLayer.PrescriptionCol.Count
        '_Flex.Rows.Count = 1
        Dim RowNumber As Integer = 0
        For i As Integer = 0 To _Prescriptions.Count - 1
            RowNumber = i + 1
            With _Flex
                '.Redraw = False
                .SetCellCheck(RowNumber, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                .SetData(RowNumber, COL_PRESCRIPTIONID, _Prescriptions.Item(i).PrescriptionID)
                .SetData(RowNumber, COL_IseRxed, _Prescriptions.Item(i).IseRxed)
                .SetData(RowNumber, COL_VISITID, _Prescriptions.Item(i).VisitID)
                '.SetData(RowNumber, 3, _Prescriptions.item(i).PatientID)
                .SetData(RowNumber, COL_PATIENTID, _PatientID) ''globalPatient.gnPatientID) 'patientid has been temporarily hardcoded
                If _Prescriptions.Item(i).NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Prescriptions.Item(i).Route) Then
                    .SetData(RowNumber, COL_MEDICATION, _Prescriptions.Item(i).Medication & " " & _Prescriptions.Item(i).Route)
                Else
                    .SetData(RowNumber, COL_MEDICATION, _Prescriptions.Item(i).Medication)
                End If
                .SetData(RowNumber, COL_EPAStatus, _Prescriptions.Item(i).PriorAuthorizationStatus)
                .SetData(RowNumber, COL_DOSAGE, _Prescriptions.Item(i).Dosage)
                .SetData(RowNumber, COL_RxDrugType, _Prescriptions.Item(i).RxType)

                If _Prescriptions.Item(i).FormularyStatus <> "Unknown" Then
                    If _Prescriptions.Item(i).FormularyStatus <> "U" Then

                        Dim FrmlyStat As String = _Prescriptions.Item(i).FormularyStatus

                        Dim FrmlyStat_int As Integer = If(_Prescriptions.Item(i).FormularyStatus = "", 0, _Prescriptions.Item(i).FormularyStatus)

                        If FrmlyStat = "Not Reimbursable" Or FrmlyStat = "0" Then
                            .SetData(RowNumber, COL_FormularyStatus, "Not Reimbursable")
                            .SetCellImage(RowNumber, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.Not_Reimbursable)
                        ElseIf FrmlyStat = "Off Formulary" Or FrmlyStat = "1" Then
                            .SetData(RowNumber, COL_FormularyStatus, "Off Formulary")
                            .SetCellImage(RowNumber, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.Off_Formulary)
                        ElseIf FrmlyStat.Contains("On Formulary") Or FrmlyStat_int = 2 Then
                            .SetData(RowNumber, COL_FormularyStatus, "On Formulary")
                            .SetCellImage(RowNumber, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.On_Formulary)
                        ElseIf FrmlyStat.Contains("On Formulary") Or FrmlyStat_int > 2 Then
                            FrmlyStat_int = FrmlyStat_int - 2
                            If FrmlyStat_int >= 1 Then
                                .SetData(RowNumber, COL_FormularyStatus, "On Formulary PL-" & FrmlyStat_int)
                                .SetCellImage(RowNumber, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.On_Formulary)
                            End If
                        End If
                    Else
                        .SetData(RowNumber, COL_FormularyStatus, "Unknown")
                        .SetCellImage(RowNumber, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.U)
                    End If
                Else
                    .SetData(RowNumber, COL_FormularyStatus, "Unknown")
                    .SetCellImage(RowNumber, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.U)

                End If
                '.SetData(RowNumber, COL_CoverageIndicator, _Prescriptions.item(i).CoverageIndicator)
                If _Prescriptions.Item(i).CoverageIndicator = "True" Then
                    .SetCellImage(RowNumber, COL_CoverageIndicator, Global.gloUserControlLibrary.My.Resources.Coverage_True01)
                Else 'False
                    .SetCellImage(RowNumber, COL_CoverageIndicator, Global.gloUserControlLibrary.My.Resources.Coverage_False01)
                End If
                '.SetData(RowNumber, COL_CopayIndicator, _Prescriptions.item(i).CopayIndicator)
                'If _Prescriptions.item(i).CopayIndicator = "True" Then
                '    .SetCellImage(RowNumber, COL_CopayIndicator, Global.gloUserControlLibrary.My.Resources.Copay_dollar)
                'Else 'False
                '    .SetCellImage(RowNumber, COL_CopayIndicator, Global.gloUserControlLibrary.My.Resources.Copay_dollarClose)
                'End If

                .SetData(RowNumber, COL_ROUTE, _Prescriptions.Item(i).Route)
                .SetData(RowNumber, COL_FREQUENCY, _Prescriptions.Item(i).Frequency)
                .SetData(RowNumber, COL_DURATION, _Prescriptions.Item(i).Duration)
                .SetData(RowNumber, COL_AMOUNT, _Prescriptions.Item(i).Amount) 'dispense
                .SetData(RowNumber, COL_REFILLS, _Prescriptions.Item(i).Refills)
                .SetData(RowNumber, COL_STARTDATE, _Prescriptions.Item(i).Startdate)

                If _Prescriptions.Item(i).Enddate = Nothing OrElse _Prescriptions.Item(i).Enddate = "12:00:00 AM" Then
                    .SetData(RowNumber, COL_ENDDATE, Nothing)
                Else
                    .SetData(RowNumber, COL_ENDDATE, _Prescriptions.Item(i).Enddate)
                End If

                .SetData(RowNumber, COL_NOTES, _Prescriptions.Item(i).Notes)
                .SetData(RowNumber, Col_REASON, _Prescriptions.Item(i).ReasontoOverride)
                .SetData(RowNumber, COL_METHOD, _Prescriptions.Item(i).Method)
                .SetData(RowNumber, COL_MAYSUBSTITUTE, _Prescriptions.Item(i).Maysubstitute)
                .SetData(RowNumber, COL_PRESCRIPTIONDATE, _Prescriptions.Item(i).Prescriptiondate)
                .SetData(RowNumber, COL_DRUGID, _Prescriptions.Item(i).DrugID)
                .SetData(RowNumber, COL_INFLAG, 0) 'lnflag
                .SetData(RowNumber, COL_LOTNUMBER, _Prescriptions.Item(i).LotNo)
                .SetData(RowNumber, COL_EXPDATE, _Prescriptions.Item(i).ExpirationDate)
                .SetData(RowNumber, COL_USERID, _Prescriptions.Item(i).UserId)
                .SetData(RowNumber, COL_USERNAME, _Prescriptions.Item(i).UserName)

                'code added by supriya 19/7/2008
                .SetData(RowNumber, COL_Narcotic, _Prescriptions.Item(i).IsNarcotics)
                .SetData(RowNumber, COL_ProviderID, _Prescriptions.Item(i).ProviderID)
                .SetData(RowNumber, COL_PharmacyID, _Prescriptions.Item(i).PharmacyID)

                'For De-Normalization
                .SetData(RowNumber, COL_NDCCode, _Prescriptions.Item(i).NDCCode)
                '.SetData(RowNumber, COL_Narcotic, _Prescriptions.item(i).IsNarcotics) 'saves the narcotic type value
                .SetData(RowNumber, COL_mpid, _Prescriptions.Item(i).mpid)
                .SetData(RowNumber, COL_DRUGFORM, _Prescriptions.Item(i).DosageForm)
                .SetData(RowNumber, COL_DRUGQTYQUALIFIER, _Prescriptions.Item(i).StrengthUnit)
                'For De-Normalization

                'For Pharmacy
                .SetData(RowNumber, COL_NCPDPID, _Prescriptions.Item(i).PhNCPDPID)
                .SetData(RowNumber, COL_PharmacyContactID, _Prescriptions.Item(i).PhContactID)
                .SetData(RowNumber, COL_PharmacyName, _Prescriptions.Item(i).PharmacyName)
                .SetData(RowNumber, COL_PharmacyAddressline1, _Prescriptions.Item(i).PhAddressline1)
                .SetData(RowNumber, COL_PharmacyAddressline2, _Prescriptions.Item(i).PhAddressline2)
                .SetData(RowNumber, COL_PharmacyCity, _Prescriptions.Item(i).PhCity)
                .SetData(RowNumber, COL_PharmacysState, _Prescriptions.Item(i).PhState)
                .SetData(RowNumber, COL_PharmacyZip, _Prescriptions.Item(i).PhZip)
                .SetData(RowNumber, COL_PharmacyEmail, _Prescriptions.Item(i).PhEmail)
                .SetData(RowNumber, COL_PharmacyFax, _Prescriptions.Item(i).PhFax)
                .SetData(RowNumber, COL_PharmacyPhone, _Prescriptions.Item(i).PhPhone)
                .SetData(RowNumber, COL_PharmacyServiceLevel, _Prescriptions.Item(i).PhServiceLevel)
                'For Pharmacy

                'temperory formulary information grid
                If _Prescriptions.Item(i).FormularyInformation_Grid <> "" Then
                    .SetData(RowNumber, COL_CopayIndicator, _Prescriptions.Item(i).FormularyInformation_Grid)
                Else
                    .SetData(RowNumber, COL_CopayIndicator, "No Details")
                End If
                .SetData(RowNumber, COL_FormularyQueried, _Prescriptions.Item(i).IsFormularyQueried)

                '' TODO : Need to check the following for Fs 3.0
                ''.SetData(RowNumber, COL_FS, "")

                'temperory formulary information richtextbox
                .SetData(RowNumber, COL_FormularyInformation_RchTxt, _Prescriptions.Item(i).FormularyInformation_RchTxtBx)
                'Infobutton
                .SetCellImage(RowNumber, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)
                .Row = RowNumber
                .Redraw = True
            End With
        Next

    End Sub

    'Set data in flexgrid for selected row after making changes to custom prescription control.
    Public Sub SetFlexGridData(Optional ByVal SelectedRxColItem As Integer = 0, Optional ByVal dtPharmacyDetails As DataTable = Nothing, Optional ByVal _RefReqRxCol As Prescriptions = Nothing)
        Dim _Prescription As Prescription
        If Not IsNothing(_RxBuisnessLayer.PrescriptionCol) Then
            If _RxBuisnessLayer.PrescriptionCol.Count = 0 Then
                _RxBuisnessLayer.PrescriptionCol = _RefReqRxCol
            End If
        End If
        _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(SelectedRxColItem)
        With _Flex
            '.Redraw = False
            .SetData(.Row, COL_PRESCRIPTIONID, _Prescription.PrescriptionID)
            .SetData(.Row, COL_IseRxed, _Prescription.IseRxed)
            .SetData(.Row, COL_VISITID, _Prescription.VisitID)
            '.SetData(.Row, 3, _Prescription.PatientID)
            .SetData(.Row, COL_PATIENTID, _PatientID) 'globalPatient.gnPatientID)
            'concatenated the drug name,dosage,route and drug form in 6030 for Rx feild changes
            If _Prescription.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Prescription.Route) Then
                .SetData(.Row, COL_MEDICATION, _Prescription.Medication & " " & _Prescription.Route)
            Else
                .SetData(.Row, COL_MEDICATION, _Prescription.Medication)
            End If
            .SetData(.Row, COL_EPAStatus, _Prescription.PriorAuthorizationStatus)
            .SetData(.Row, COL_DOSAGE, _Prescription.Dosage)
            .SetData(.Row, COL_ROUTE, _Prescription.Route)
            .SetData(.Row, COL_FREQUENCY, _Prescription.Frequency)
            .SetData(.Row, COL_DURATION, _Prescription.Duration)
            .SetData(.Row, COL_AMOUNT, _Prescription.Amount) ''dispense
            .SetData(.Row, COL_REFILLS, _Prescription.Refills)
            .SetData(.Row, COL_STARTDATE, _Prescription.Startdate)
            If _Prescription.Enddate = Nothing OrElse _Prescription.Enddate = "12:00:00 AM" Then
                .SetData(.Row, COL_ENDDATE, Nothing)
            Else
                .SetData(.Row, COL_ENDDATE, _Prescription.Enddate)
            End If

            .SetData(.Row, COL_NOTES, _Prescription.Notes)
            .SetData(.Row, Col_REASON, _Prescription.ReasontoOverride)
            .SetData(.Row, COL_METHOD, _Prescription.Method)
            .SetData(.Row, COL_MAYSUBSTITUTE, If(_Prescription.Maysubstitute, "Yes", "No"))
            .SetData(.Row, COL_PRESCRIPTIONDATE, _Prescription.Prescriptiondate)
            .SetData(.Row, COL_DRUGID, _Prescription.DrugID)
            .SetData(.Row, COL_INFLAG, _Prescription.State) ''lnflag
            .SetData(.Row, COL_LOTNUMBER, _Prescription.LotNo)
            .SetData(.Row, COL_EXPDATE, _Prescription.ExpirationDate)
            .SetData(.Row, COL_USERID, _Prescription.UserId)
            .SetData(.Row, COL_USERNAME, _Prescription.UserName)
            '            _RxBuisnessLayer.SetInsuranceCol(_Prescription, RowIndex - 1)

            'code added by supriya 19/7/2008
            .SetData(.Row, COL_Narcotic, _Prescription.IsNarcotics)
            .SetData(.Row, COL_ProviderID, _Prescription.ProviderID)


            'For De-Normalization
            .SetData(.Row, COL_NDCCode, _Prescription.NDCCode)
            .SetData(.Row, COL_Narcotic, _Prescription.IsNarcotics) 'save the narcotic type value
            .SetData(.Row, COL_mpid, _Prescription.mpid)
            .SetData(.Row, COL_DRUGFORM, _Prescription.DosageForm)
            .SetData(.Row, COL_DRUGQTYQUALIFIER, _Prescription.StrengthUnit)
            'For De-Normalization

            'set the eRxstatus and eRxStatus Message to the flex grid
            .SetData(.Row, COL_eRxStatus, _Prescription.eRxStatus)
            .SetData(.Row, COL_eRxStatusMessage, _Prescription.eRxStatusMessage)

            'Infobutton
            .SetCellImage(.Row, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)

            'For Pharmacy
            If _Prescription.PharmacyID <> 0 Then
                .SetData(.Row, COL_PharmacyID, _Prescription.PharmacyID)
                .SetData(.Row, COL_NCPDPID, _Prescription.PhNCPDPID)
                .SetData(.Row, COL_PharmacyContactID, _Prescription.PhContactID)
                .SetData(.Row, COL_PharmacyName, _Prescription.PharmacyName)
                .SetData(.Row, COL_PharmacyAddressline1, _Prescription.PhAddressline1)
                .SetData(.Row, COL_PharmacyAddressline2, _Prescription.PhAddressline2)
                .SetData(.Row, COL_PharmacyCity, _Prescription.PhCity)
                .SetData(.Row, COL_PharmacysState, _Prescription.PhState)
                .SetData(.Row, COL_PharmacyZip, _Prescription.PhZip)
                .SetData(.Row, COL_PharmacyEmail, _Prescription.PhEmail)
                .SetData(.Row, COL_PharmacyFax, _Prescription.PhFax)
                .SetData(.Row, COL_PharmacyPhone, _Prescription.PhPhone)
                .SetData(.Row, COL_PharmacyServiceLevel, _Prescription.PhServiceLevel)
            Else
                If Not IsNothing(dtPharmacyDetails) Then
                    If dtPharmacyDetails.Rows.Count > 0 Then
                        .SetData(.Row, COL_NCPDPID, dtPharmacyDetails.Rows(0)("sNCPDPID"))
                        .SetData(.Row, COL_PharmacyContactID, dtPharmacyDetails.Rows(0)("nContactID"))
                        .SetData(.Row, COL_PharmacyName, dtPharmacyDetails.Rows(0)("sName"))
                        .SetData(.Row, COL_PharmacyAddressline1, dtPharmacyDetails.Rows(0)("sAddressLine1"))
                        .SetData(.Row, COL_PharmacyAddressline2, dtPharmacyDetails.Rows(0)("sAddressLine2"))
                        .SetData(.Row, COL_PharmacyCity, dtPharmacyDetails.Rows(0)("sCity"))
                        .SetData(.Row, COL_PharmacysState, dtPharmacyDetails.Rows(0)("sState"))
                        .SetData(.Row, COL_PharmacyZip, dtPharmacyDetails.Rows(0)("sZIP"))
                        .SetData(.Row, COL_PharmacyEmail, dtPharmacyDetails.Rows(0)("sEmail"))
                        .SetData(.Row, COL_PharmacyFax, dtPharmacyDetails.Rows(0)("sFax"))
                        .SetData(.Row, COL_PharmacyPhone, dtPharmacyDetails.Rows(0)("sPhone"))
                        .SetData(.Row, COL_PharmacyServiceLevel, dtPharmacyDetails.Rows(0)("sServiceLevel"))

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sNCPDPID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sNCPDPID")) Then
                            _Prescription.PhNCPDPID = dtPharmacyDetails.Rows(0)("sNCPDPID")
                        Else
                            _Prescription.PhNCPDPID = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("nContactID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("nContactID")) Then
                            _Prescription.PhContactID = dtPharmacyDetails.Rows(0)("nContactID")
                        Else
                            _Prescription.PhContactID = 0
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sName")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sName")) Then
                            _Prescription.PharmacyName = dtPharmacyDetails.Rows(0)("sName")
                        Else
                            _Prescription.PharmacyName = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine1")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine1")) Then
                            _Prescription.PhAddressline1 = dtPharmacyDetails.Rows(0)("sAddressLine1")
                        Else
                            _Prescription.PhAddressline1 = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine2")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine2")) Then
                            _Prescription.PhAddressline2 = dtPharmacyDetails.Rows(0)("sAddressLine2")
                        Else
                            _Prescription.PhAddressline2 = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sCity")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sCity")) Then
                            _Prescription.PhCity = dtPharmacyDetails.Rows(0)("sCity")
                        Else
                            _Prescription.PhCity = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sState")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sState")) Then
                            _Prescription.PhState = dtPharmacyDetails.Rows(0)("sState")
                        Else
                            _Prescription.PhState = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sZIP")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sZIP")) Then
                            _Prescription.PhZip = dtPharmacyDetails.Rows(0)("sZIP")
                        Else
                            _Prescription.PhZip = ""
                        End If

                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sEmail")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sEmail")) Then
                            _Prescription.PhEmail = dtPharmacyDetails.Rows(0)("sEmail")
                        Else
                            _Prescription.PhEmail = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sFax")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _Prescription.PhFax = dtPharmacyDetails.Rows(0)("sFax")
                        Else
                            _Prescription.PhFax = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sPhone")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                            _Prescription.PhPhone = dtPharmacyDetails.Rows(0)("sPhone")
                        Else
                            _Prescription.PhPhone = ""
                        End If
                        If Not IsNothing(dtPharmacyDetails.Rows(0)("sServiceLevel")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sServiceLevel")) Then
                            _Prescription.PhServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                        Else
                            _Prescription.PhServiceLevel = ""
                        End If

                    End If
                Else

                    If _Prescription.PhNCPDPID <> "" Then
                        .SetData(.Row, COL_NCPDPID, _Prescription.PhNCPDPID)
                    Else
                        .SetData(.Row, COL_NCPDPID, "")
                    End If

                    If _Prescription.PhContactID <> 0 Then
                        .SetData(.Row, COL_PharmacyContactID, _Prescription.PhContactID)
                    Else
                        .SetData(.Row, COL_PharmacyContactID, 0)
                    End If

                    If _Prescription.PharmacyName <> "" Then
                        .SetData(.Row, COL_PharmacyName, _Prescription.PharmacyName)
                    Else
                        .SetData(.Row, COL_PharmacyName, "")
                    End If

                    If _Prescription.PhAddressline1 <> "" Then
                        .SetData(.Row, COL_PharmacyAddressline1, _Prescription.PhAddressline1)
                    Else
                        .SetData(.Row, COL_PharmacyAddressline1, "")
                    End If

                    If _Prescription.PhAddressline2 <> "" Then
                        .SetData(.Row, COL_PharmacyAddressline2, _Prescription.PhAddressline2)
                    Else
                        .SetData(.Row, COL_PharmacyAddressline2, "")
                    End If

                    If _Prescription.PhCity <> "" Then
                        .SetData(.Row, COL_PharmacyCity, _Prescription.PhCity)
                    Else
                        .SetData(.Row, COL_PharmacyCity, "")
                    End If

                    If _Prescription.PhState <> "" Then
                        .SetData(.Row, COL_PharmacysState, _Prescription.PhState)
                    Else
                        .SetData(.Row, COL_PharmacysState, "")
                    End If

                    If _Prescription.PhZip <> "" Then
                        .SetData(.Row, COL_PharmacyZip, _Prescription.PhZip)
                    Else
                        .SetData(.Row, COL_PharmacyZip, "")
                    End If

                    If _Prescription.PhEmail <> "" Then
                        .SetData(.Row, COL_PharmacyEmail, _Prescription.PhEmail)
                    Else
                        .SetData(.Row, COL_PharmacyEmail, "")
                    End If

                    If _Prescription.PhFax <> "" Then
                        .SetData(.Row, COL_PharmacyFax, _Prescription.PhFax)
                    Else
                        .SetData(.Row, COL_PharmacyFax, "")
                    End If

                    If _Prescription.PhPhone <> "" Then
                        .SetData(.Row, COL_PharmacyPhone, _Prescription.PhPhone)
                    Else
                        .SetData(.Row, COL_PharmacyPhone, "")
                    End If

                    If _Prescription.PhServiceLevel <> "" Then
                        .SetData(.Row, COL_PharmacyServiceLevel, _Prescription.PhServiceLevel)
                    Else
                        .SetData(.Row, COL_PharmacyServiceLevel, _Prescription.PhServiceLevel)
                    End If

                    .Redraw = True

                End If

                'For Pharmacy
            End If


        End With
    End Sub

    'Set data in flexgrid for selected row after making changes to custom prescription control.
    Public Sub SetFlexGridData_eRxStatus(ByVal eRxStatus As String, ByVal eRxStatusMessage As String, ByVal eRx_DrgNDCCode As String, ByVal eRx_RowNumber As Integer, ByVal IseRxed As Integer)

        Dim eRxDrgNDCCode As String = ""
        Dim eRxRowNumber As Integer
        Try

            With _Flex
                If _Flex.Rows.Count > 1 Then
                    For i As Integer = 1 To _Flex.Rows.Count - 1
                        eRxDrgNDCCode = _Flex.GetData(i, COL_NDCCode)
                        eRxRowNumber = _Flex.GetData(i, COL_RowNumber)
                        ''condition modified in 6030 to solve bug 13911 (updating message against all drugs having same NDC) 
                        If eRxDrgNDCCode = eRx_DrgNDCCode And eRxRowNumber = eRx_RowNumber Then
                            'set the eRxstatus and eRxStatus Message to the flex grid
                            If eRxStatus <> "" Then
                                .SetData(i, COL_eRxStatus, eRxStatus)
                                .SetData(i, COL_eRxStatusMessage, eRxStatusMessage) 'this is optional column so even if we add no data it doesnt matters
                            Else
                                .SetData(i, COL_eRxStatus, "")
                                .SetData(i, COL_eRxStatusMessage, eRxStatusMessage) 'this is optional column so even if we add no data it doesnt matters
                            End If

                            .SetData(i, COL_IseRxed, IseRxed)
                        End If
                    Next
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub gloRxC1FlexGrdPrescriptionUserCtrl__FlexClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me._FlexClick
        Try
            RowIndex = _Flex.Row

            ''Infobutton
            'If _Flex.Col = COl_INFOBUTTON Then

            '    Dim sNDCCode As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_NDCCode))
            '    Dim nVisitid As Long = 0
            '    nVisitid = Convert.ToInt64(_Flex.GetData(_Flex.Row, COL_VISITID))
            '    If nVisitid = 0 Then
            '        Dim dr As DialogResult = MessageBox.Show("You need to save newly added drug to get education material." & vbNewLine & "Do you want to save?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            '        If dr.ToString = "Yes" Then
            '            RaiseEvent SavePrescription(sender, e)
            '            nVisitid = VisitIDaftersave
            '        Else
            '            nVisitid = 0
            '        End If

            '    End If

            '    If nVisitid > 0 Then
            '        Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            '        Dim dtPatientInfo As New DataTable()
            '        Dim strSql As String = "select sGender,[dbo].[fn_CalculateAgeMU] (Patient.dtDOB,dbo.gloGetDate()) as age,sLang as Lang from Patient where nPatientID  =" & _PatientID
            '        dtPatientInfo = ogloEMRDatabase.GetDataTable_Query(strSql)
            '        Dim pGender As String = ""
            '        Dim pAge As String = ""
            '        Dim pLanguage As String = ""
            '        If dtPatientInfo IsNot Nothing Then
            '            pGender = Convert.ToString(dtPatientInfo.Rows(0)("sGender"))
            '            pAge = Convert.ToString(Convert.ToInt32(dtPatientInfo.Rows(0)("age")))
            '            pLanguage = Convert.ToString(dtPatientInfo.Rows(0)("Lang"))
            '        End If

            '        If sNDCCode.Length > 0 Then
            '            clsinfobutton_Prescription.Openinfosource(sNDCCode, "2.16.840.1.113883.6.69", pLanguage, _PatientID, gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication.GetHashCode(), nVisitid)
            '        Else
            '            MessageBox.Show("NDC code is not available for selected Drug", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Public Sub BindFlexgrid(ByVal _RefReqRxCol As Prescriptions)
        Dim i As Int16
        Dim _Prescription As Prescription
        _Flex.Rows.Count = 1
        For i = 0 To _RefReqRxCol.Count - 1
            _Prescription = _RefReqRxCol.Item(i)
            With _Flex
                '.Redraw = False
                RemoveHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
                .Rows.Add()
                AddHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
                .SetCellCheck(i + 1, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                .SetData(i + 1, COL_PRESCRIPTIONID, _Prescription.PrescriptionID)
                .SetData(i + 1, COL_IseRxed, _Prescription.IseRxed)
                .SetData(i + 1, COL_VISITID, _Prescription.VisitID)
                .SetData(i + 1, COL_PATIENTID, _PatientID) '  globalPatient.gnPatientID)
                If _Prescription.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Prescription.Route) Then
                    .SetData(i + 1, COL_MEDICATION, _Prescription.Medication & " " & _Prescription.Route)
                Else
                    .SetData(i + 1, COL_MEDICATION, _Prescription.Medication)
                End If
                .SetData(i + 1, COL_EPAStatus, _Prescription.PriorAuthorizationStatus)
                .SetData(i + 1, COL_DOSAGE, _Prescription.Dosage)
                .SetData(i + 1, COL_ROUTE, _Prescription.Route)
                .SetData(i + 1, COL_FREQUENCY, _Prescription.Frequency)
                .SetData(i + 1, COL_DURATION, _Prescription.Duration)
                .SetData(i + 1, COL_AMOUNT, _Prescription.Amount)
                .SetData(i + 1, COL_REFILLS, _Prescription.Refills)
                .SetData(i + 1, COL_STARTDATE, _Prescription.Startdate)
                .SetData(i + 1, COL_ENDDATE, _Prescription.Enddate)
                .SetData(i + 1, COL_NOTES, _Prescription.Notes)
                .SetData(i + 1, Col_REASON, _Prescription.ReasontoOverride)
                .SetData(i + 1, COL_METHOD, _Prescription.Method)
                .SetData(i + 1, COL_MAYSUBSTITUTE, _Prescription.Maysubstitute)
                .SetData(i + 1, COL_PRESCRIPTIONDATE, _Prescription.Prescriptiondate)
                .SetData(i + 1, COL_DRUGID, _Prescription.DrugID)
                .SetData(i + 1, COL_INFLAG, 0) ''lnflag
                .SetData(i + 1, COL_LOTNUMBER, _Prescription.LotNo)
                .SetData(i + 1, COL_EXPDATE, _Prescription.ExpirationDate)
                .SetData(i + 1, COL_USERID, _Prescription.UserId)
                .SetData(i + 1, COL_USERNAME, _Prescription.UserName)
                'code added by supriya 19/7/2008
                .SetData(i + 1, COL_Narcotic, _Prescription.IsNarcotics) ' to save the narcotic type value
                .SetData(i + 1, COL_ProviderID, _Prescription.ProviderID)
                .SetData(i + 1, COL_PharmacyID, _Prescription.PharmacyID)

                'For De-Normalization
                .SetData(i + 1, COL_NDCCode, _Prescription.NDCCode)
                .SetData(i + 1, COL_mpid, _Prescription.mpid)
                .SetData(i + 1, COL_DRUGFORM, _Prescription.DosageForm)
                .SetData(i + 1, COL_DRUGQTYQUALIFIER, _Prescription.StrengthUnit)
                'For De-Normalization

                'For Pharmacy
                .SetData(i + 1, COL_NCPDPID, _Prescription.PhNCPDPID)
                .SetData(i + 1, COL_PharmacyContactID, _Prescription.PhContactID)
                .SetData(i + 1, COL_PharmacyName, _Prescription.PharmacyName)
                .SetData(i + 1, COL_PharmacyAddressline1, _Prescription.PhAddressline1)
                .SetData(i + 1, COL_PharmacyAddressline2, _Prescription.PhAddressline2)
                .SetData(i + 1, COL_PharmacyCity, _Prescription.PhCity)
                .SetData(i + 1, COL_PharmacysState, _Prescription.PhState)
                .SetData(i + 1, COL_PharmacyZip, _Prescription.PhZip)
                .SetData(i + 1, COL_PharmacyEmail, _Prescription.PhEmail)
                .SetData(i + 1, COL_PharmacyFax, _Prescription.PhFax)
                .SetData(i + 1, COL_PharmacyPhone, _Prescription.PhPhone)
                .SetData(i + 1, COL_PharmacyServiceLevel, _Prescription.PhServiceLevel)
                'For Pharmacy

                'Infobutton
                .SetCellImage(.Row, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)

                'if the formulary setting is off ie. the blnLoadFormularyDrugs = False then dont show the foll cols
                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = False Then
                    .Cols(COL_FormularyStatus).Width = 0
                    .Cols(COL_RxDrugType).Width = 0
                    .Cols(COL_CopayIndicator).Width = 0
                    '.Cols(COL_FS).Width = 0
                Else
                    If _Prescription.FormularyStatus <> "" Then
                        If _Prescription.FormularyStatus <> "Unknown" Then
                            If _Prescription.FormularyStatus <> "U" Then
                                Dim FrmlyStat As String = _Prescription.FormularyStatus
                                Dim FrmlyStat_int As Integer = _Prescription.FormularyStatus

                                If FrmlyStat = "Not Reimbursable" Or FrmlyStat = "0" Then
                                    .SetData(i + 1, COL_FormularyStatus, "Not Reimbursable")
                                    .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.Not_Reimbursable)
                                ElseIf FrmlyStat = "Off Formulary" Or FrmlyStat = "1" Then
                                    .SetData(i + 1, COL_FormularyStatus, "Off Formulary")
                                    .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.Off_Formulary)
                                ElseIf FrmlyStat.Contains("On Formulary") Or FrmlyStat_int = 2 Then
                                    .SetData(i + 1, COL_FormularyStatus, "On Formulary")
                                    .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.On_Formulary)
                                ElseIf FrmlyStat.Contains("On Formulary") Or FrmlyStat_int > 2 Then
                                    FrmlyStat_int = FrmlyStat_int - 2
                                    If FrmlyStat_int >= 1 Then
                                        .SetData(i + 1, COL_FormularyStatus, "On Formulary PL-" & FrmlyStat_int)
                                        .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.On_Formulary)
                                    End If
                                End If
                            Else
                                .SetData(i + 1, COL_FormularyStatus, "Unknown")
                                .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.U)

                            End If
                        Else
                            .SetData(i + 1, COL_FormularyStatus, "Unknown")
                            .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.U)

                        End If
                    Else
                        .SetData(i + 1, COL_FormularyStatus, "Unknown")
                        .SetCellImage(i + 1, COL_FormularyStatus, Global.gloUserControlLibrary.My.Resources.U)
                    End If
                    .SetData(i + 1, COL_RxDrugType, _Prescription.RxType)
                    .SetData(i + 1, COL_CopayIndicator, _Prescription.CopayIndicator)
                    .SetData(i + 1, COL_FormularyQueried, _Prescription.IsFormularyQueried)

                    '' TODO : Need to check the following for Fs 3.0
                    ''.SetData(RowNumber, COL_FS, "")

                    .Redraw = True
                End If
            End With
        Next

    End Sub

    Public Sub BindFlexgrid(Optional ByVal _isGridRowCheck As Boolean = False)
        Dim i As Int16
        Dim _Prescription As Prescription
        _Flex.Rows.Count = 1
        '_Flex.Redraw = False
        For i = 0 To _RxBuisnessLayer.PrescriptionCol.Count - 1
            _Prescription = _RxBuisnessLayer.PrescriptionCol.Item(i)
            With _Flex
                '.Redraw = FalseCOL_MEDICATION
                RemoveHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
                .Rows.Add()
                AddHandler _Flex.AfterSelChange, AddressOf _Flex_AfterSelChange
                If _isGridRowCheck = True Then
                    If _Prescription.Flag = True Then
                        .SetCellCheck(i + 1, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        .SetCellCheck(i + 1, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                Else
                    .SetCellCheck(i + 1, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                End If
                .SetData(i + 1, COL_PRESCRIPTIONID, _Prescription.PrescriptionID)
                .SetData(i + 1, COL_IseRxed, _Prescription.IseRxed)
                .SetData(i + 1, COL_VISITID, _Prescription.VisitID)
                .SetData(i + 1, COL_PATIENTID, _PatientID) '' globalPatient.gnPatientID)
                If _Prescription.NDCCode.Contains("GLO") AndAlso Not String.IsNullOrEmpty(_Prescription.Route) Then
                    .SetData(i + 1, COL_MEDICATION, _Prescription.Medication & " " & _Prescription.Route)
                Else
                    .SetData(i + 1, COL_MEDICATION, _Prescription.Medication)
                End If
                .SetData(i + 1, COL_EPAStatus, _Prescription.PriorAuthorizationStatus)
                .SetData(i + 1, COL_DOSAGE, _Prescription.Dosage)
                .SetData(i + 1, COL_ROUTE, _Prescription.Route)
                .SetData(i + 1, COL_FREQUENCY, _Prescription.Frequency)
                .SetData(i + 1, COL_DURATION, _Prescription.Duration)
                .SetData(i + 1, COL_AMOUNT, _Prescription.Amount)
                .SetData(i + 1, COL_REFILLS, _Prescription.Refills)
                .SetData(i + 1, COL_STARTDATE, _Prescription.Startdate)

                Dim _strEndDate As String = ""
                Dim odt As DateTime = Now
                Try
                    If (Date.TryParse(_Prescription.Enddate.ToString(), odt) = False) Then
                        _strEndDate = ""
                    Else
                        _strEndDate = _Prescription.Enddate
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
                    .SetData(i + 1, COL_ENDDATE, CType(_Prescription.Enddate, Date))
                End If

                .SetData(i + 1, COL_NOTES, _Prescription.Notes)
                .SetData(i + 1, Col_REASON, _Prescription.ReasontoOverride)
                .SetData(i + 1, COL_METHOD, If(_Prescription.Method = "", "eRx", _Prescription.Method))
                If _Prescription.Method = "" Then
                    .SetData(i + 1, COL_METHOD, "eRx")
                    _Prescription.Method = "eRx"
                Else
                    .SetData(i + 1, COL_METHOD, _Prescription.Method)
                End If
                .SetData(i + 1, COL_MAYSUBSTITUTE, If(_Prescription.Maysubstitute, "Yes", "No")) 'bug no 13923
                .SetData(i + 1, COL_PRESCRIPTIONDATE, _Prescription.Prescriptiondate)
                .SetData(i + 1, COL_DRUGID, _Prescription.DrugID)
                .SetData(i + 1, COL_INFLAG, _Prescription.State) ''lnflag
                .SetData(i + 1, COL_LOTNUMBER, _Prescription.LotNo)
                .SetData(i + 1, COL_EXPDATE, _Prescription.ExpirationDate)
                .SetData(i + 1, COL_USERID, _Prescription.UserId)
                .SetData(i + 1, COL_USERNAME, _Prescription.UserName)
                'code added by supriya 19/7/2008
                .SetData(i + 1, COL_Narcotic, _Prescription.IsNarcotics) ' to save the narcotic type value
                .SetData(i + 1, COL_ProviderID, _Prescription.ProviderID)
                .SetData(i + 1, COL_PharmacyID, _Prescription.PharmacyID)

                'Code added by Ashish on 6th March 2015 for adding RxType to flex grid.
                .SetData(i + 1, COL_RxDrugType, _Prescription.RxType)
                'For De-Normalization
                .SetData(i + 1, COL_NDCCode, _Prescription.NDCCode)
                .SetData(i + 1, COL_mpid, _Prescription.mpid)
                .SetData(i + 1, COL_DRUGFORM, _Prescription.DosageForm)
                .SetData(i + 1, COL_DRUGQTYQUALIFIER, _Prescription.StrengthUnit)
                'For De-Normalization

                'For Pharmacy
                .SetData(i + 1, COL_NCPDPID, _Prescription.PhNCPDPID)
                .SetData(i + 1, COL_PharmacyContactID, _Prescription.PhContactID)
                .SetData(i + 1, COL_PharmacyName, _Prescription.PharmacyName)
                .SetData(i + 1, COL_PharmacyAddressline1, _Prescription.PhAddressline1)
                .SetData(i + 1, COL_PharmacyAddressline2, _Prescription.PhAddressline2)
                .SetData(i + 1, COL_PharmacyCity, _Prescription.PhCity)
                .SetData(i + 1, COL_PharmacysState, _Prescription.PhState)
                .SetData(i + 1, COL_PharmacyZip, _Prescription.PhZip)
                .SetData(i + 1, COL_PharmacyEmail, _Prescription.PhEmail)
                .SetData(i + 1, COL_PharmacyFax, _Prescription.PhFax)
                .SetData(i + 1, COL_PharmacyPhone, _Prescription.PhPhone)
                .SetData(i + 1, COL_PharmacyServiceLevel, _Prescription.PhServiceLevel)
                'For Pharmacy

                'if the formulary setting is off ie. the blnLoadFormularyDrugs = False then dont show the foll cols
                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = False Then
                    .Cols(COL_FormularyStatus).Width = 0
                    .Cols(COL_RxDrugType).Width = 0
                    .Cols(COL_CopayIndicator).Width = 0
                    '.Cols(COL_FS).Width = 0
                End If
                .SetData(i + 1, COL_eRxStatus, _Prescription.eRxStatus)

                .SetData(i + 1, COL_eRxStatusMessage, _Prescription.eRxStatusMessage)
                'Infobutton
                .SetCellImage(i + 1, COl_INFOBUTTON, Global.gloUserControlLibrary.My.Resources.Resources.infobutton)
                .SetData(i + 1, COL_RowNumber, i)
                .SetData(i + 1, COL_FormularyQueried, _Prescription.IsFormularyQueried)

                '' TODO : Need to check the following for Fs 3.0
                ''.SetData(RowNumber, COL_FS, "")

                _Prescription.ItemNumber = i ''the item number will keep track for collection item number that will correspond to row number in the grid

                '.Redraw = True
            End With
        Next

        'If _Flex.Rows.Count > 1 Then
        '    _Flex.Select(1, True)
        'End If

        '_Flex.Redraw = True
    End Sub

    Public Function InsertCheckState(Optional ByVal blnstatus As Boolean = False) As Boolean
        Try
            Dim _tmpCheckstate As tmpCheckState
            Dim i As Int16
            _RxBuisnessLayer.TmpCheckStatesCol.Clear()
            With _Flex
                For i = 1 To .Rows.Count - 1
                    'Do you want to fill all checkstates or just the ones which are checked
                    If blnstatus Then
                        _tmpCheckstate = New tmpCheckState
                        If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                            If _Flex.Rows.Item(i).Visible = True Then
                                _tmpCheckstate.CheckState = True
                            Else
                                _tmpCheckstate.CheckState = False
                            End If

                        Else
                            _tmpCheckstate.CheckState = False
                        End If

                        _tmpCheckstate.NDCCode = CType(.GetData(i, COL_NDCCode), String)   ''remove drug ID dependancy - chech against NDC Code

                        _tmpCheckstate.DrugID = _RxBuisnessLayer.RetrieveDrugID_FROM_NDC(_tmpCheckstate.NDCCode)

                        ' added in 6030 to shift logic of printing from NDC to PrescriptionID(bug 11452)
                        _tmpCheckstate.PrescriptionID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID
                        '''''_tmpCheckstate.DrugID = CType(.GetData(i, COL_DRUGID), Long)
                        _tmpCheckstate.DrugPharmacyID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PharmacyID
                        _tmpCheckstate.IssueMethod = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Method
                        _tmpCheckstate.CPOEOrder = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).CPOEOrder
                        _tmpCheckstate.MessageType = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Status
                        _tmpCheckstate.ItemNumber = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).ItemNumber
                        'If _RxBuisnessLayer.PrescriptionCol.Item(i - 1).IsNarcotics > 1 Then  ''Get Latest EPCS Status For Prescription
                        '    If _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID <> 0 Then
                        '        _tmpCheckstate.EPCSeRxStatus = GetLatestEPCSStatuslabel(_RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID, _RxBuisnessLayer.PrescriptionCol.Item(i - 1).IsNarcotics) '_RxBuisnessLayer.GetEPCSStatus(_RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID)
                        '    End If
                        'Else
                        _tmpCheckstate.EPCSeRxStatus = ""
                        'End If
                        _RxBuisnessLayer.TmpCheckStatesCol.Add(_tmpCheckstate)
                        _tmpCheckstate.Dispose()
                        _tmpCheckstate = Nothing

                    Else
                        If _Flex.Rows.Item(i).Visible = True Then
                            If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                _tmpCheckstate = New tmpCheckState

                                _tmpCheckstate.CheckState = True

                                _tmpCheckstate.NDCCode = CType(.GetData(i, COL_NDCCode), String) ''remove drug ID dependancy - chech against NDC Code

                                _tmpCheckstate.DrugID = _RxBuisnessLayer.RetrieveDrugID_FROM_NDC(_tmpCheckstate.NDCCode)


                                ' added in 6030 to shift logic of printing from NDC to PrescriptionID(bug 11452)
                                _tmpCheckstate.PrescriptionID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID
                                ''_tmpCheckstate.DrugID = CType(.GetData(i, COL_DRUGID), Long)
                                _tmpCheckstate.DrugPharmacyID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PharmacyID
                                _tmpCheckstate.IssueMethod = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Method
                                _tmpCheckstate.CPOEOrder = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).CPOEOrder
                                _tmpCheckstate.MessageType = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Status
                                _tmpCheckstate.ItemNumber = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).ItemNumber
                                'If _RxBuisnessLayer.PrescriptionCol.Item(i - 1).IsNarcotics > 1 Then  ''Get Latest EPCS Status For Prescription
                                '    If _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID <> 0 Then
                                '        _tmpCheckstate.EPCSeRxStatus = GetLatestEPCSStatuslabel(_RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID, _RxBuisnessLayer.PrescriptionCol.Item(i - 1).IsNarcotics) '_RxBuisnessLayer.GetEPCSStatus(_RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID)
                                '    End If
                                'Else
                                '    _tmpCheckstate.EPCSeRxStatus = ""
                                'End If
                                _RxBuisnessLayer.TmpCheckStatesCol.Add(_tmpCheckstate)
                                _tmpCheckstate.Dispose()
                                _tmpCheckstate = Nothing
                            End If
                        End If

                    End If

                Next
            End With
            '''''comented to add the logic for C1FlexGrid by sagar''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'For i = 0 To trPrescriptionDetails.Nodes.Item(0).GetNodeCount(False) - 1 '''''by sagar start here
            '    If CType(trPrescriptionDetails.Nodes.Item(0).Nodes.Item(i), myTreeNode).Checked = True Then
            '        objmylist = New myList

            '        objmylist.Type = CType(trPrescriptionDetails.Nodes.Item(0).Nodes.Item(i), myTreeNode).Checked
            '        objmylist.Index = objPrescriptionDBLayer.GetDrugID(i)
            '        'objmylist.Index = CType(trPrescriptionDetails.Nodes.Item(0).Nodes.Item(i), myTreeNode).Key
            '        arrlist.Add(objmylist)
            '    End If
            'Next ''''' by sagar end here
            Return True
        Catch ex As gloUserControlLibrary.gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = ""
            Throw objex
        End Try
    End Function
    'Private Function GetLatestEPCSStatuslabel(ByVal PrescriptionID As Int64, ByVal _IsNarcotics As Short) As String
    '    Dim _status As String = ""
    '    Try
    '        _status = _RxBuisnessLayer.GetEPCSStatus(PrescriptionID)
    '        If _status = "" Then
    '            _status = _RxBuisnessLayer.GetLatestStatusForEPCS(PrescriptionID, _IsNarcotics)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    '    Return _status
    'End Function
    'added for 10.6 workflow changes
    Public Function InsertRefillCheckState(Optional ByVal blnstatus As Boolean = False) As Boolean
        Try
            Dim _tmpCheckstate As tmpCheckState
            Dim i As Int16
            _RxBuisnessLayer.TmpCheckStatesCol.Clear()
            With _Flex
                For i = 1 To .Rows.Count - 1
                    'Do you want to fill all checkstates or just the ones which are checked
                    If blnstatus Then
                        _tmpCheckstate = New tmpCheckState
                        If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                            If _Flex.Rows.Item(i).Visible = True Then
                                _tmpCheckstate.CheckState = True
                            Else
                                _tmpCheckstate.CheckState = False
                            End If

                        Else
                            _tmpCheckstate.CheckState = False
                        End If
                        If _RxBuisnessLayer.PrescriptionCol.Item(i - 1).MessageType = "RefillRequest" OrElse _RxBuisnessLayer.PrescriptionCol.Item(i - 1).MessageType = "RxChangeRequest" Then
                            _tmpCheckstate.NDCCode = CType(.GetData(i, COL_NDCCode), String)   ''remove drug ID dependancy - chech against NDC Code

                            _tmpCheckstate.DrugID = _RxBuisnessLayer.RetrieveDrugID_FROM_NDC(_tmpCheckstate.NDCCode)

                            ' added in 6030 to shift logic of printing from NDC to PrescriptionID(bug 11452)
                            _tmpCheckstate.PrescriptionID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID
                            '''''_tmpCheckstate.DrugID = CType(.GetData(i, COL_DRUGID), Long)
                            _tmpCheckstate.DrugPharmacyID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PharmacyID
                            _tmpCheckstate.IssueMethod = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Method
                            _tmpCheckstate.CPOEOrder = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).CPOEOrder
                            _tmpCheckstate.MessageType = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Status
                            _tmpCheckstate.ItemNumber = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).ItemNumber
                            _RxBuisnessLayer.TmpCheckStatesCol.Add(_tmpCheckstate)
                            _tmpCheckstate.Dispose()
                            _tmpCheckstate = Nothing
                        End If
                    Else
                        If _Flex.Rows.Item(i).Visible = True Then
                            If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                _tmpCheckstate = New tmpCheckState

                                _tmpCheckstate.CheckState = True

                                _tmpCheckstate.NDCCode = CType(.GetData(i, COL_NDCCode), String) ''remove drug ID dependancy - chech against NDC Code

                                _tmpCheckstate.DrugID = _RxBuisnessLayer.RetrieveDrugID_FROM_NDC(_tmpCheckstate.NDCCode)


                                ' added in 6030 to shift logic of printing from NDC to PrescriptionID(bug 11452)
                                _tmpCheckstate.PrescriptionID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PrescriptionID
                                ''_tmpCheckstate.DrugID = CType(.GetData(i, COL_DRUGID), Long)
                                _tmpCheckstate.DrugPharmacyID = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).PharmacyID
                                _tmpCheckstate.IssueMethod = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Method
                                _tmpCheckstate.CPOEOrder = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).CPOEOrder
                                _tmpCheckstate.MessageType = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).Status
                                _tmpCheckstate.ItemNumber = _RxBuisnessLayer.PrescriptionCol.Item(i - 1).ItemNumber
                                _RxBuisnessLayer.TmpCheckStatesCol.Add(_tmpCheckstate)
                                _tmpCheckstate.Dispose()
                                _tmpCheckstate = Nothing
                            End If
                        End If

                    End If

                Next
            End With
            '''''comented to add the logic for C1FlexGrid by sagar''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'For i = 0 To trPrescriptionDetails.Nodes.Item(0).GetNodeCount(False) - 1 '''''by sagar start here
            '    If CType(trPrescriptionDetails.Nodes.Item(0).Nodes.Item(i), myTreeNode).Checked = True Then
            '        objmylist = New myList

            '        objmylist.Type = CType(trPrescriptionDetails.Nodes.Item(0).Nodes.Item(i), myTreeNode).Checked
            '        objmylist.Index = objPrescriptionDBLayer.GetDrugID(i)
            '        'objmylist.Index = CType(trPrescriptionDetails.Nodes.Item(0).Nodes.Item(i), myTreeNode).Key
            '        arrlist.Add(objmylist)
            '    End If
            'Next ''''' by sagar end here
            Return True
        Catch ex As gloUserControlLibrary.gloUserControlExceptions
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = ""
            Throw objex
        End Try
    End Function

    '********************************************************************************
    Public Function getCheckedState(ByVal i As Int16) As Boolean
        Try
            If i >= 0 Then
                If _Flex.GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = ""
            getCheckedState = False
            Throw objex
        End Try

    End Function
    '********************************************************************************

    Public Sub ClearRows()
        _Flex.Rows.Count = 1
    End Sub

    Private Sub _Flex_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDoubleClick
        RaiseEvent RowDoubleClicked(sender, e)
    End Sub

    Private Sub gloRxC1FlexGrdPrescriptionUserCtrl__FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me._FlexMouseDown
        Try
            Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row

            If r < 0 Then
                Exit Sub
            Else
                If formLock = False Then
                    If _RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Add Then
                        cntListmenuStrip.Items(0).Visible = False
                    ElseIf _RxBuisnessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                        cntListmenuStrip.Items(0).Visible = True
                    End If
                Else
                    If Not IsNothing(cntListmenuStrip) Then
                        Me.cntListmenuStrip.Items(0).Visible = False
                        Me.cntListmenuStrip.Items(1).Visible = False
                        Me.cntListmenuStrip.Items(2).Visible = False
                        Me.cntListmenuStrip.Items(3).Visible = False
                    End If
                    Exit Sub
                End If
            End If



            If e.Button = Windows.Forms.MouseButtons.Left Then
                Dim c As Integer = _Flex.HitTest(e.X, e.Y).Column

                If r > 0 Then
                    If c = COl_INFOBUTTON Then
                        Try
                            If cntListmenuStrip.Items.Count > 2 Then
                                'SLR: Logic Problem to be changed on 4/2/2014
                                For i As Integer = 2 To cntListmenuStrip.Items.Count - 1
                                    If i < cntListmenuStrip.Items.Count Then
                                        If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                            cntListmenuStrip.Items.RemoveAt(i)
                                            i = i - 1
                                        ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                            cntListmenuStrip.Items.RemoveAt(i)
                                            i = i - 1
                                        ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                            cntListmenuStrip.Items.RemoveAt(i)
                                            i = i - 1
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                        If EducationMaterialEnabled Then
                            AddInfobuttonMenu(sender, e)
                        End If

                        If cntListmenuStrip.Items.Count > 3 Then
                            For i As Integer = 0 To cntListmenuStrip.Items.Count - 1
                                If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                    cntListmenuStrip.Items(i).Visible = True
                                ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                    cntListmenuStrip.Items(i).Visible = True
                                ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                    cntListmenuStrip.Items(i).Visible = True
                                ElseIf cntListmenuStrip.Items(i).Text = "Delete All Prescriptions" Then
                                    cntListmenuStrip.Items(i).Visible = False
                                ElseIf cntListmenuStrip.Items(i).Text = "Delete Prescription Item" Then
                                    cntListmenuStrip.Items(i).Visible = False
                                ElseIf cntListmenuStrip.Items(i).Text = "Show eRx status" Then
                                    cntListmenuStrip.Items(i).Visible = False
                                ElseIf cntListmenuStrip.Items(i).Text = "Show Alternatives" Then
                                    cntListmenuStrip.Items(i).Visible = False
                                ElseIf cntListmenuStrip.Items(i).Text = "Show ePA Process" Then
                                    cntListmenuStrip.Items(i).Visible = True
                                    If (_RxBuisnessLayer.PrescriptionCol.Count > 0) AndAlso (_Flex.Row < _RxBuisnessLayer.PrescriptionCol.Count) AndAlso (_Flex.Row > 0) Then
                                        Dim _PARefObj = _RxBuisnessLayer.PrescriptionCol.Item(_Flex.Row - 1).PAReferenceID
                                        If (IsNumeric(_PARefObj)) Then
                                            Dim _PAReferenceIDd As Int64 = Convert.ToInt64(_PARefObj)
                                            Using ePAInsertUpdate As New EPABusinesslayer()
                                                If _PAReferenceIDd > 0 AndAlso ePAInsertUpdate.epa_IsManualPriorAuthorization(_PatientID, _PAReferenceIDd) Then
                                                    cntListmenuStrip.Items(i).Visible = False
                                                End If
                                            End Using
                                        End If

                                    End If
                                End If
                            Next
                        End If
                        'ElseIf c = COL_PDR Then
                        '    RaiseEvent PDRInfoButtonClicked(_Flex.Row)
                        '    Exit Sub
                    End If
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

                Dim c As Integer = _Flex.HitTest(e.X, e.Y).Column

                If r > 0 Then
                    If cntListmenuStrip.Items.Count > 2 Then
                        For i As Integer = 0 To cntListmenuStrip.Items.Count - 1
                            If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
                                cntListmenuStrip.Items(i).Visible = False
                            ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
                                cntListmenuStrip.Items(i).Visible = False
                            ElseIf cntListmenuStrip.Items(i).Text = "Infobutton" Then
                                cntListmenuStrip.Items(i).Visible = False
                            ElseIf cntListmenuStrip.Items(i).Text = "Delete All Prescriptions" Then
                                cntListmenuStrip.Items(i).Visible = True
                            ElseIf cntListmenuStrip.Items(i).Text = "Delete Prescription Item" Then
                                cntListmenuStrip.Items(i).Visible = True
                            ElseIf cntListmenuStrip.Items(i).Text = "Show eRx status" Then
                                cntListmenuStrip.Items(i).Visible = True
                            ElseIf cntListmenuStrip.Items(i).Text = "Show Alternatives" Then
                                cntListmenuStrip.Items(i).Visible = True
                            ElseIf cntListmenuStrip.Items(i).Text = "Show ePA Process" Then
                                cntListmenuStrip.Items(i).Visible = True
                                If _RxBuisnessLayer.PrescriptionCol.Count > 0 Then
                                    Dim _PAReferenceIDd As Int64 = Convert.ToInt64(_RxBuisnessLayer.PrescriptionCol.Item(_Flex.Row - 1).PAReferenceID)
                                    Using ePAInsertUpdate As New EPABusinesslayer()
                                        If _PAReferenceIDd > 0 AndAlso ePAInsertUpdate.epa_IsManualPriorAuthorization(_PatientID, _PAReferenceIDd) Then
                                            cntListmenuStrip.Items(i).Visible = False
                                        End If
                                    End Using
                                End If
                            End If
                        Next
                    End If
                End If
            End If



            'If e.Button = Windows.Forms.MouseButtons.Right Then
            '    Try
            '        If cntListmenuStrip.Items.Count > 3 Then
            '            For i As Integer = 2 To cntListmenuStrip.Items.Count - 1
            '                If i < cntListmenuStrip.Items.Count Then
            '                    If cntListmenuStrip.Items(i).Text = "Patient Reference Material" Then
            '                        cntListmenuStrip.Items.RemoveAt(i)
            '                        i = i - 1
            '                    ElseIf cntListmenuStrip.Items(i).Text = "Provider Reference Material" Then
            '                        cntListmenuStrip.Items.RemoveAt(i)
            '                        i = i - 1
            '                    End If
            '                End If

            '            Next
            '        End If
            '    Catch ex As Exception

            '    End Try

            '    If EducationMaterialEnabled Then
            '        AddInfobuttonMenu()
            '    End If


            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub AddInfobuttonMenu(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

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
        Dim dtPatientInfo As DataTable = Nothing
        Dim pGender As String = ""
        Dim pAge As String = ""
        Dim pLanguage As String = ""
        Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
        Try



            ' Dim sNDCCode As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_NDCCode))
            Dim mpid As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_mpid))
            Dim strSql As String = "select sGender,[dbo].[fn_CalculateAgeMU] (Patient.dtDOB,dbo.gloGetDate()) as age,sLang as Lang from Patient where nPatientID  =" & _PatientID
            dtPatientInfo = ogloEMRDatabase.GetDataTable_Query(strSql)
            If dtPatientInfo IsNot Nothing Then
                If (dtPatientInfo.Rows.Count >= 1) Then
                    pGender = Convert.ToString(dtPatientInfo.Rows(0)("sGender"))
                    pAge = Convert.ToString(Convert.ToInt32(dtPatientInfo.Rows(0)("age")))
                    pLanguage = Convert.ToString(dtPatientInfo.Rows(0)("Lang"))
                End If
                dtPatientInfo.Dispose()
                dtPatientInfo = Nothing
            End If
            Dim clsinfobutton_Prescription As New gloEMRGeneralLibrary.clsInfobutton()
            dtEdu = clsinfobutton_Prescription.GetEducationMaterial(mpid, "2.16.840.1.113883.6.69", pAge, pGender)
            clsinfobutton_Prescription = Nothing

            If Not IsNothing(dtEdu) Then
                If dtEdu.Rows.Count > 0 Then
                    tlstripitem_Patient = New ToolStripMenuItem
                    tlstripitem_Patient.Text = "Patient Reference Material"
                    tlstripitem_Patient.Image = ImageFlex.Images(9)
                    tlstripitem_Patient.ForeColor = Color.FromArgb(31, 73, 125)

                    tlstripitem_Provider = New ToolStripMenuItem
                    tlstripitem_Provider.Text = "Provider Reference Material"
                    tlstripitem_Provider.Image = ImageFlex.Images(10)
                    tlstripitem_Provider.ForeColor = Color.FromArgb(31, 73, 125)


                    tlstripitem_Infobutton = New ToolStripMenuItem
                    tlstripitem_Infobutton.Text = "Infobutton"
                    tlstripitem_Infobutton.Image = ImageFlex.Images(11)
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
                    cntListmenuStrip.Items("Delete All Prescriptions").Visible = False
                    cntListmenuStrip.Items("Delete Prescription Item").Visible = False
                    cntListmenuStrip.Items("Show eRx status").Visible = False
                    cntListmenuStrip.Items("Show Alternatives").Visible = False

                    tlstripitem_Patient = Nothing
                    tlstripitem_Provider = Nothing
                Else
                    'Open Infobutton Document
                    OpenInfobutton(sender, New System.EventArgs())
                End If
            Else
                'Open Infobutton Document
                OpenInfobutton(sender, New System.EventArgs())
            End If


            'If Not IsNothing(dtEdu) Then
            '    If dtEdu.Rows.Count > 0 Then
            '        tlstripitem = New ToolStripMenuItem
            '        tlstripitem.Text = "Patient Reference Material"
            '        tlstripitem.Image = ImageFlex.Images(8)
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
            '        tlstripitem.Image = ImageFlex.Images(8)
            '        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
            '        For i As Integer = 0 To dtEdu.Rows.Count - 1
            '            If Convert.ToInt32(dtEdu.Rows(i)("nResourceType")) = 2 Then
            '                tlstripsubitem = New ToolStripMenuItem
            '                tlstripsubitem.Text = Convert.ToString(dtEdu.Rows(i)("sTemplateName"))
            '                tlstripsubitem.ForeColor = Color.FromArgb(31, 73, 125)
            '                tlstripsubitem.Tag = Convert.ToString(dtEdu.Rows(i)("nTemplateID")) + "-Provider Reference Material"
            '                tlstripitem.DropDownItems.Add(tlstripsubitem)
            '                AddHandler tlstripsubitem.Click, AddressOf OpenEducation
            '                tlstripsubitem = Nothing
            '            End If
            '        Next
            '        cntListmenuStrip.Items.Add(tlstripitem)
            '        'AddHandler tlstripitem.Click, AddressOf StripItem_Click
            '        tlstripitem = Nothing

            '    End If
            'End If

            isInfobuttonMenuAdded = True

        Catch ex As Exception
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

            If Not IsNothing(dtPatientInfo) Then
                dtPatientInfo.Dispose()
                dtPatientInfo = Nothing
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
        'Infobutton
        Try
            If _Flex.Col = COl_INFOBUTTON Then

                Dim sNDCCode As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_NDCCode))
                Dim sNDCCodeDesc As String = Convert.ToString(_Flex.GetData(_Flex.Row, COL_MEDICATION))
                Dim nVisitid As Long = 0
                nVisitid = Convert.ToInt64(_Flex.GetData(_Flex.Row, COL_VISITID))
                If nVisitid = 0 Then
                    Dim dr As DialogResult = MessageBox.Show("You need to save newly added drug to get education material." & vbNewLine & "Do you want to save?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If dr.ToString = "Yes" Then
                        RaiseEvent SavePrescription(sender, e)
                        nVisitid = VisitIDaftersave
                    Else
                        nVisitid = 0
                    End If

                End If
                Cursor.Current = Cursors.WaitCursor
                If nVisitid > 0 Then
                    RaiseEvent InfoButtonClicked(sNDCCode, sNDCCodeDesc)
                End If
            End If
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Cursor.Current = Cursors.Default
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


    'C

    ''get the NDCCODe against the selected Row no, so that after sorting the grid we can fetch the correct item from the Medication col and show in the Custom Medication control. 
    Public Function GetNDCCodeFromGrid(ByVal GridRowNo As Integer) As String
        Dim sNDCRxGrid As String = ""
        Dim sMedicationName As String = ""
        Try
            If _Flex.Rows.Count > 0 Then
                For i As Integer = 0 To _Flex.Rows.Count
                    sNDCRxGrid = _Flex.GetData(GridRowNo, COL_NDCCode)
                    sMedicationName = _Flex.GetData(GridRowNo, COL_MEDICATION)
                    Return sNDCRxGrid
                Next
            End If
            Return sNDCRxGrid
        Catch ex As Exception
            Return sNDCRxGrid
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

    Private Sub gloRxC1FlexGrdPrescriptionUserCtrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(_Flex)

        If EducationMaterialEnabled Then
            _Flex.Cols(COl_INFOBUTTON).Visible = True
        Else
            _Flex.Cols(COl_INFOBUTTON).Visible = False
        End If
    End Sub

    Private Sub gloRxC1FlexGrdPrescriptionUserCtrl_OnAfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles Me.OnAfterSort
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
                Case COL_REFILLS
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.RefillsAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.RefillsDesc
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
                Case COL_USERNAME
                    If e.Order = C1.Win.C1FlexGrid.SortFlags.Ascending Then
                        eRxSortOrder = enuRxSortOrder.UserNameAsc
                    ElseIf e.Order = C1.Win.C1FlexGrid.SortFlags.Descending Then
                        eRxSortOrder = enuRxSortOrder.UserNameDesc
                    End If

            End Select
            '_RxBuisnessLayer.PrescriptionCol.Sort(New CMySort(eRxSortOrder))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub _Flex_AfterSelChange(sender As System.Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles _Flex.AfterSelChange
        'If _Flex.GetData(_Flex.RowSel, COL_IseRxed) > 0 Then
        '    RaiseEvent IseRxed(1)
        'Else
        '    RaiseEvent IseRxed(0)
        'End If
    End Sub

    Private Sub PDRProgramsRequested_Event(ByVal row As Integer)
        Try
            If _RxBuisnessLayer.PrescriptionCol.Count > 0 Then
                If _RxBuisnessLayer.PrescriptionCol.Item(row - 1).Status = "D" And _RxBuisnessLayer.PrescriptionCol.Item(row - 1).State = "D" Then
                    Exit Sub
                End If
            End If

            Dim pcid As Int64 = Convert.ToInt64(_RxBuisnessLayer.PrescriptionCol.Item(row - 1).PCTransactionID)

            If (pcid <= 0) Then
                RaiseEvent PDRProgramsRequested(row - 1)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.General, "PDRProgramsRequested_Event : " + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub PrescriptionRowChangedRevised(ByVal row As Integer, Optional ByVal IsInfoRequested As Boolean = False, Optional QueriedFrom As FormularyQueriedFrom = FormularyQueriedFrom.None)
        Try
            If _Flex.RowSel >= 1 Then
                Me.PDRProgramsRequested_Event(row)

                If clsgeneral.gblnIsFormularyServiceEnabled Then
                    Dim mpid As Int64 = Convert.ToInt64(_Flex.GetData(row, COL_mpid))
                    Dim IsFormularyQueried As Boolean = Convert.ToBoolean(_Flex.GetData(row, COL_FormularyQueried))

                    If Not IsFormularyQueried OrElse IsInfoRequested Then
                        _Flex.SetData(row, COL_FormularyQueried, True)

                        Dim drugName As String = Convert.ToString(_Flex.GetData(row, COL_MEDICATION))
                        Dim rxType As String = Convert.ToString(_Flex.GetData(row, COL_RxDrugType))

                        RaiseEvent DrugFormularyQueried(drugName, mpid, rxType, IsFormularyQueried, row, QueriedFrom)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.General, "FS3_QueriedEvent : " + If(ex.InnerException IsNot Nothing, ex.InnerException, ex).ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#Region "EPA"

    Public Sub AddEPAContextMenu()
        If Not cntListmenuStrip.Items.ContainsKey("ShowEPAProcess") Then
            Dim tlstripitem As New ToolStripMenuItem("Show ePA Process")

            With tlstripitem
                .Name = "ShowEPAProcess"
                .Tag = 5
                .ForeColor = Color.FromArgb(31, 73, 125)
                .Image = ImageFlex.Images(13)
            End With

            cntListmenuStrip.Items.Add(tlstripitem)
            AddHandler tlstripitem.Click, AddressOf StripItem_Click
            tlstripitem = Nothing
        End If
    End Sub

    Public Sub RemoveEPAContextMenu()
        If cntListmenuStrip.Items.ContainsKey("ShowEPAProcess") Then
            Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("ShowEPAProcess")
            RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
            toolStripItem = Nothing

            cntListmenuStrip.Items.RemoveByKey("ShowEPAProcess")
        End If
    End Sub



#End Region


    Private bLoadCancelRxContextMenus As Boolean = True
    Public Property LoadCancelRxContextMenus() As Boolean
        Get
            Return bLoadCancelRxContextMenus
        End Get
        Set(ByVal value As Boolean)
            bLoadCancelRxContextMenus = value
        End Set
    End Property


#Region "CancelRx"
    Public Sub RemoveCancelRxContextMenu()
        Try
            LoadCancelRxContextMenus = False
            If cntListmenuStrip.Items.ContainsKey("tslCancelPrescription") Then
                Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("tslCancelPrescription")

                If toolStripItem.HasDropDownItems Then
                    If toolStripItem.DropDownItems.ContainsKey("mnuCancelPrescription") Then
                        Dim tsCancel As ToolStripMenuItem = toolStripItem.DropDownItems("mnuCancelPrescription")
                        RemoveHandler tsCancel.Click, AddressOf StripItem_Click
                        tsCancel = Nothing

                        toolStripItem.DropDownItems.RemoveByKey("mnuCancelPrescription")
                    End If

                    If toolStripItem.DropDownItems.ContainsKey("mnuDiscontinuePrescription") Then
                        Dim tsDiscontinue As ToolStripMenuItem = toolStripItem.DropDownItems("mnuDiscontinuePrescription")
                        RemoveHandler tsDiscontinue.Click, AddressOf StripItem_Click
                        tsDiscontinue = Nothing

                        toolStripItem.DropDownItems.RemoveByKey("mnuDiscontinuePrescription")
                    End If
                End If
                toolStripItem = Nothing
                cntListmenuStrip.Items.RemoveByKey("tslCancelPrescription")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region

#Region "PDR"

    'Public Sub LoadPDRInfoButton()
    '    If clsgeneral.gblnIsPDREnabled Then
    '        _Flex.Cols(COL_PDR).Visible = True
    '    Else
    '        _Flex.Cols(COL_PDR).Visible = False
    '    End If


    'End Sub

#End Region

    Private Sub cntListmenuStrip_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles cntListmenuStrip.Opening
        If (_Flex.RowSel > 0) AndAlso Me.LoadCancelRxContextMenus Then

            If (_RxBuisnessLayer.PrescriptionCol(_Flex.RowSel - 1).MessageType = "RxChangeRequest" OrElse _RxBuisnessLayer.PrescriptionCol(_Flex.RowSel - 1).MessageType = "RefillRequest") Then
                Exit Sub
            End If


            If Convert.ToInt64(_Flex.GetData(_Flex.RowSel, COL_PRESCRIPTIONID)) = 0 Then
                If cntListmenuStrip.Items.ContainsKey("CancelPrescription") Then
                    Dim toolStripItem As ToolStripMenuItem = cntListmenuStrip.Items("CancelPrescription")
                    RemoveHandler toolStripItem.Click, AddressOf StripItem_Click
                    toolStripItem = Nothing

                    cntListmenuStrip.Items.RemoveByKey("CancelPrescription")
                End If
            Else
                If Not cntListmenuStrip.Items.ContainsKey("CancelPrescription") Then
                    Dim tlstripitem As ToolStripMenuItem
                    tlstripitem = New ToolStripMenuItem

                    tlstripitem.Text = "Cancel Prescription"
                    tlstripitem.Name = "CancelPrescription"
                    tlstripitem.Tag = 6
                    tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
                    tlstripitem.Image = ImageFlex.Images(14)
                    cntListmenuStrip.Items.Add(tlstripitem)

                    Dim item1 As New System.Windows.Forms.ToolStripMenuItem()
                    item1.Text = "Cancel Prescription"
                    item1.Tag = 6
                    item1.ForeColor = Color.FromArgb(31, 73, 125)
                    item1.Image = ImageFlex.Images(14)

                    AddHandler item1.Click, AddressOf StripItem_Click

                    tlstripitem.DropDownItems.Add(item1)

                    Dim item2 As New System.Windows.Forms.ToolStripMenuItem()
                    item2.Text = "Discontinue Prescription"
                    item2.Tag = 7
                    item2.ForeColor = Color.FromArgb(31, 73, 125)
                    item2.Image = ImageFlex.Images(14)

                    AddHandler item2.Click, AddressOf StripItem_Click

                    tlstripitem.DropDownItems.Add(item2)

                    tlstripitem = Nothing

                End If
            End If
        End If

    End Sub
End Class
