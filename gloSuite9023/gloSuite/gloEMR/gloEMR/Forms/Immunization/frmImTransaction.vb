Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient
Imports gloBilling


Public Class frmImTransaction

#Region "Variable declaration"

    Inherits frmBaseForm

    Dim ofrmDiagnosisList As frmViewListControl = Nothing
    Dim ofrmList As frmViewListControl = Nothing
    Dim objfrmHistory As frmHistory
    Dim _PatientID As Long
    Dim _TransactionID As Long
    Dim _DocumentID As Long
    Dim _GridTransId As Long
    Dim _AssociatedDocumentID As Long = -1
    Dim _isLoadGridCvxControl As Boolean = False
    Dim isSaved As Boolean = False
    Dim _lst As gloEMR.myList
    Dim combo As New ComboBox
    Dim tooltip As New ToolTip
    Dim hashtblItemName As New Hashtable
    Dim isVISGridFilled As Boolean = False
    'Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management

    Private oCVXControl As gloUserControlLibrary.gloUC_GridList
    Private oTradeNameControl As gloUserControlLibrary.gloUC_GridList
    Private oMVXControl As gloUserControlLibrary.gloUC_GridList
    Private oDiagnosisListControl As gloListControl.gloListControl
    Private oListControl As gloListControl.gloListControl
    Private ICDType As Integer = 9  ''added for ICd10 implementation
    Public Event GridListLoaded()
    Public Event GridListClosed()
    Public Event GridListLoaded1()
    Public Event GridListClosed1()
    Public Event GridListLoaded2()
    Public Event GridListClosed2()
    Public Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer

    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private blnCVXSelector As Boolean
    Private blnMVXSelector As Boolean
    Private blnTradeNameSelector As Boolean

    Private oLateralityListControl As gloListControl.gloListControl
    Private ofrmLateralityList As frmViewListControl
    Private strRefusalCode As String = ""
    Private strRefusalDescription As String = ""
    '------------------------------------------------'
    Private oCQMListControl As gloListControl.gloListControl
    Private ofrmCQMList As frmViewListControl
    Private strCQMCode As String = ""
    Private strCQMDescription As String = ""

    'Public isHistory As Int16 = 0

#End Region

#Region "Constructor"

    Public Sub New(ByVal TransactionID As Long, ByVal PatientID As Long)
        MyBase.New()
        InitializeComponent()
        _PatientID = PatientID
        _TransactionID = TransactionID
        cmbLocation.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbLocation.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox
    End Sub

    Public Sub New(ByVal TransactionID As Long, ByVal PatientID As Long, ByVal lst As gloEMR.myList)
        'Added by Mayuri:20120202-DM rule in Helath plan
        MyBase.New()
        InitializeComponent()
        _PatientID = PatientID
        _TransactionID = TransactionID
        _lst = lst
    End Sub

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

#End Region

    Enum RefreshType
        None = 0
        UnitOfMeasureCodes = 1
        PublicityCodes = 2
    End Enum

#Region "Form Event"

    Private Sub frmImTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Developer:Dipak Patil
        'Date:20120207
        'Bug ID/PRD Name/Sales force Case:Immunization PRD
        'Reason: To Implement Audit trail CreateAuditLog line added 
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "Immunization Transaction Opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        BtnUncertainCVX.Enabled = False

        lblDosesOnHand.Text = ""

        FillControl()
        ShowRequiredLable()

        If _TransactionID > 0 Then
            ShowSelectedImmunization()
        Else
            ''Added by Mayuri:20120202-DM rule in Helath plan
            If IsNothing(_lst) = False Then
                _isLoadGridCvxControl = True

                Fill_VaccineInfomrationfromRecommendation()


                _isLoadGridCvxControl = False
            End If

            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If

            dtPatientDied.Enabled = chkPatientDied.Checked
            If chk_RequiredHospitalization.Checked = True Then
                txtHospitalizationDays.Enabled = True
            Else
                txtHospitalizationDays.Enabled = False
            End If

            txt_dosage_given.Text = 1
            dtTransactionTime.Checked = False
            Dim scheme As gloBilling.Cls_TabIndexSettings.TabScheme = gloBilling.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New gloBilling.Cls_TabIndexSettings(Me)
            tom.SetTabOrder(scheme)
        End If
        cmbSKU.Select()
        _isLoaded = True

        fillGrid()

        If _isHistory = True Then
            tblbtn_Save.Enabled = False
            MessageBox.Show("This is downloaded History. You can not modify it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub frmImTransaction_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Close, "Immunization Transaction Closed", gloAuditTrail.ActivityOutCome.Success)
    End Sub

#End Region

#Region "Button Click"

    Private Sub btnSearchsku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchsku.Click
        Dim ofrmSKUSearch As New frmIMSkuSearch
        Try
            RemoveHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
            RemoveHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
            RemoveHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged

            If IsNothing(cmbLocation.SelectedValue) Then
                ofrmSKUSearch.nLocationID = 0
            Else
                ofrmSKUSearch.nLocationID = cmbLocation.SelectedValue
            End If

            ofrmSKUSearch.ShowDialog(IIf(IsNothing(ofrmSKUSearch.Parent), Me, ofrmSKUSearch.Parent))
            If (ofrmSKUSearch.DialogResult = Windows.Forms.DialogResult.OK) Then
                If ofrmSKUSearch.nLocationID <> "" Then
                    cmbLocation.SelectedValue = ofrmSKUSearch.nLocationID
                End If

                RemoveHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged
                '  Dim dtsku As New DataTable
                cmbSKU.SelectedIndex = -1
                cmbSKU.SelectedIndex = cmbSKU.FindStringExact(ofrmSKUSearch.SKU)
                AddHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged

                _isLoadGridCvxControl = True
                txtCvx.Text = ofrmSKUSearch.Vaccine.ToString()

                If ofrmSKUSearch.nCategoryID <> "" Then
                    cmbCategory.SelectedValue = ofrmSKUSearch.nCategoryID
                End If

                txt_TradeName.Text = ofrmSKUSearch.Trade.ToString()
                txtMvx.Text = ofrmSKUSearch.Manufacturer
                _isLoadGridCvxControl = False
                cmbIcd.Items.Clear()
                cmbLotNumber.SelectedIndex = -1
                If cmbLotNumber.FindStringExact(ofrmSKUSearch.LotNo) = -1 Then
                    cmbLotNumber.Text = ofrmSKUSearch.LotNo
                Else
                    cmbLotNumber.SelectedIndex = cmbLotNumber.FindStringExact(ofrmSKUSearch.LotNo)
                End If

                Dim arrICD() As String
                If ofrmSKUSearch.Diagnosis.ToString() <> "" Then
                    arrICD = ofrmSKUSearch.Diagnosis.Split(",")
                    For i As Integer = 0 To arrICD.GetUpperBound(0)
                        cmbIcd.Items.Add(arrICD(i))
                    Next
                End If
                If cmbIcd.Items.Count > 0 Then
                    cmbIcd.SelectedIndex = 0
                End If

                cmbCpt.Items.Clear()
                Dim arrCPT() As String
                If ofrmSKUSearch.CPT.ToString() <> "" Then
                    arrCPT = ofrmSKUSearch.CPT.ToString.Split(",")
                    For i As Integer = 0 To arrCPT.GetUpperBound(0)
                        cmbCpt.Items.Add(arrCPT(i))
                    Next
                End If
                If cmbCpt.Items.Count > 0 Then
                    cmbCpt.SelectedIndex = 0
                End If

                txtNDCcode.Text = ofrmSKUSearch.NDC.ToString()

                If IsDate(ofrmSKUSearch.ExpDate) = True Then
                    dtexpDate.Checked = True
                    dtexpDate.Value = CType(ofrmSKUSearch.ExpDate, Date)
                End If

                If ofrmSKUSearch.nICDRevision.ToString() <> "" Then
                    ICDType = CType(ofrmSKUSearch.nICDRevision, Integer)
                End If

                fillGrid()

            End If
            AddHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
            AddHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
            AddHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged
            cmbSKU.Select()
            ofrmSKUSearch.Dispose()
            ofrmSKUSearch = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnCvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCvx.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnCVXSelector = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
            oCVXControl.FillControl("")
            _isLoadGridCvxControl = False
            txtCvx.Select()
            blnCVXSelector = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvx.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnMVXSelector = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
            oMVXControl.FillControl("")
            _isLoadGridCvxControl = False
            txtMvx.Select()
            blnMVXSelector = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTradeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeName.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnTradeNameSelector = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
            oTradeNameControl.FillControl("")
            _isLoadGridCvxControl = False
            txt_TradeName.Select()
            blnTradeNameSelector = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnAddManufacturerCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddManufacturerCategory.Click
        AddCategory("Manufacturer", "Add Manufacturer")
    End Sub

    Private Sub BtnAddVaccineCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddVaccineCategory.Click
        AddCategory("Vaccine", "Add Vaccine")
    End Sub

    Private Sub BtnAddTradeNameCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddTradeNameCategory.Click
        AddCategory("TradeName", "Add Trade Name")
    End Sub

    Private Sub btnBrowseReaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseReaction.Click
        Dim _sVaccineName As String = ""
        Dim _ConceptID As String = ""
        Dim _DescriptionID As String = ""
        Dim _SnomedID As String = ""
        Dim _ICD9 As String = ""
        Dim _SnomedDescription As String = ""
        Dim _SnomedDefination As String = ""
        Dim _RxNormID As String = ""
        Dim _NDCCode As String = ""
        Dim oImmunization As New gloStream.Immunization.ItemSetup

        For Each di As DictionaryEntry In hashtblItemName
            If di.Key.ToString = txtCvx.Text Then
                _sVaccineName = di.Value.ToString()
                Exit For
            End If
        Next

        Dim dt As DataTable
        dt = oImmunization.GetSnoMedids(_sVaccineName)
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                _ConceptID = dt.Rows(0)("im_sConceptID")
                _DescriptionID = dt.Rows(0)("im_sDescriptionID")
                _SnomedID = dt.Rows(0)("im_sSnoMedID")
                _ICD9 = dt.Rows(0)("sICD9")
                _RxNormID = dt.Rows(0)("sTranID1")
                _SnomedDefination = dt.Rows(0)("im_sSnomedDefination")
                _NDCCode = dt.Rows(0)("sTranID2")
            End If
            dt.Dispose()
            dt = Nothing
        End If

        _sVaccineName = txtCvx.Text.Trim()

        If _sVaccineName = "" Then
            MessageBox.Show("Select Vaccine to add Reaction.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TabImmunization.SelectTab(TabPageAdministratin)
            txtCvx.Select()
            oImmunization = Nothing
            Exit Sub
        End If

        If _ConceptID.ToString.Trim() = "ConceptID" Then
            _ConceptID = ""
        End If
        If _DescriptionID.ToString.Trim() = "DescriptionID" Then
            _DescriptionID = ""
        End If
        If _SnomedID.ToString.Trim() = "SnoMedID" Then
            _SnomedID = ""
        End If
        objfrmHistory = New frmHistory(GenerateVisitID(Now.Date, _PatientID), DateTime.Now, _sVaccineName, _ConceptID, _SnomedID, _DescriptionID, _ICD9, _SnomedDefination, _RxNormID, _NDCCode, True, _PatientID)
        objfrmHistory.MdiParent = Me.ParentForm
        objfrmHistory.mycallerImm = Me
        objfrmHistory.blnShowMessageBox = False
        objfrmHistory.blnShowAddModeMessageBox = False
        objfrmHistory.PopulatePatientHistory_Final()
        If objfrmHistory.blncancel = False Then

        Else
            ''Fixed Bug id 41911 - Added 'Me' parameter for show form for windows 8 on 20121213
            objfrmHistory.ShowDialog(IIf(IsNothing(objfrmHistory.Parent), Me, objfrmHistory.Parent))

            ''End Bug id 41911
        End If
        '  End If
        objfrmHistory.Dispose()
        objfrmHistory = Nothing

        oImmunization = Nothing
    End Sub

    Private Sub btnBrowsDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsDiagnosis.Click

        Try
            Me.Cursor = Cursors.WaitCursor
            If (IsNothing(ofrmDiagnosisList) = False) Then
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
            End If
            ofrmDiagnosisList = New frmViewListControl
            '  Dim arrCPTTextSplit As String()

            oDiagnosisListControl = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.Diagnosis, True, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            oDiagnosisListControl.gblnIcd10Transition = gblnIcd10Transition  ''If true then ICD10 gets selected 

            AddHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
            AddHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick

            ofrmDiagnosisList.Controls.Add(oDiagnosisListControl)
            oDiagnosisListControl.Dock = DockStyle.Fill
            oDiagnosisListControl.BringToFront()

            'code make all CPT Code Selected in ListControl  Which are present in cmbCPT
            For i As Integer = 0 To cmbIcd.Items.Count - 1
                cmbIcd.SelectedIndex = i
                oDiagnosisListControl.SelectedItems.Add(0, cmbIcd.Text, "")
            Next

            oDiagnosisListControl.ShowHeaderPanel(False)
            oDiagnosisListControl.IsICD9_10 = ICDType  ''added for ICd10 implementation
            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog(IIf(IsNothing(ofrmDiagnosisList.Parent), Me, ofrmDiagnosisList.Parent))
            ICDType = oDiagnosisListControl.IsICD9_10   ''added for ICd10 implementation
            If IsNothing(oDiagnosisListControl) = False Then
                RemoveHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
                RemoveHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
                If IsNothing(ofrmDiagnosisList) = False Then

                    ofrmDiagnosisList.Controls.Remove(oDiagnosisListControl)
                End If

                oDiagnosisListControl.Dispose()
                oDiagnosisListControl = Nothing
            End If
            If IsNothing(ofrmDiagnosisList) = False Then
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearDiagnosis.Click
        If cmbIcd.SelectedIndex >= 0 Then
            If IsNothing(cmbIcd.DataSource) Then
                cmbIcd.Items.RemoveAt(cmbIcd.SelectedIndex)
            Else
                Dim dtCPTCode As DataTable
                dtCPTCode = cmbIcd.DataSource
                dtCPTCode.Rows(cmbIcd.SelectedIndex).Delete()
                cmbIcd.Refresh()
            End If
        End If
    End Sub

    Private Sub btnClearCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCPT.Click
        If cmbCpt.SelectedIndex >= 0 Then
            If IsNothing(cmbCpt.DataSource) Then
                cmbCpt.Items.RemoveAt(cmbCpt.SelectedIndex)
            Else
                Dim dtCPTCode As DataTable
                dtCPTCode = cmbCpt.DataSource
                dtCPTCode.Rows(cmbCpt.SelectedIndex).Delete()
                cmbCpt.Refresh()
            End If
        End If
    End Sub

    Private Sub btnBrowsCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsCPT.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If (IsNothing(ofrmList) = False) Then
                ofrmList.Dispose()
                ofrmList = Nothing
            End If
            ofrmList = New frmViewListControl
            '   Dim arrCPTTextSplit As String()
            oListControl = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.CPT, True, Me.Width)
            oListControl.ControlHeader = "CPT"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
            'code make all CPT Code Selected in ListControl  Which are present in cmbCPT
            For i As Integer = 0 To cmbCpt.Items.Count - 1
                cmbCpt.SelectedIndex = i
                oListControl.SelectedItems.Add(0, cmbCpt.Text, "")
            Next
            oListControl.ShowHeaderPanel(False)
            oListControl.OpenControl()
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.Text = "CPT"
            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))
            If IsNothing(oListControl) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                If IsNothing(ofrmList) = False Then

                    ofrmList.Controls.Remove(oListControl)
                End If

                oListControl.Dispose()
                oListControl = Nothing
            End If
            If IsNothing(ofrmList) = False Then
                ofrmList.Dispose()
                ofrmList = Nothing
                'End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScan.Click
        Try
            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_VIS) = False Then
                gDMSCategory_VIS = objSettings.DMSCategory_VIS
            End If
            objSettings.Dispose()
            objSettings = Nothing
            ScanViewDoucment()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        If (_DocumentID > 0) Then
            ViewScanDoucment()
        End If
    End Sub

    Private Sub tblbtn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Save.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            oCvxControl_InternalGridLostFocus(Nothing, Nothing)
            oMvxControl_InternalGridLostFocus(Nothing, Nothing)
            oTradeNameControl_InternalGridLostFocus(Nothing, Nothing)

            If lblLocation.Visible = True Then
                If cmbLocation.SelectedIndex = -1 Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Location.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbLocation.Select()
                    Exit Sub
                End If
            End If

            If lblProviderName.Visible = True Then
                If cmbProvider.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Provider.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbProvider.Select()
                    Exit Sub
                End If
            End If




            If optRefused.Checked = True And txtcqm.Text <> "" Then
            Else

                If txtCvx.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Vaccine.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtCvx.Select()
                    Exit Sub
                End If

            End If



            If optRefused.Checked = True And txtcqm.Text <> "" Then

            Else
                If lblTradeName.Visible = True Then
                    If txt_TradeName.Text.Trim = "" Then
                        TabImmunization.SelectTab(TabPageAdministratin)
                        MessageBox.Show("Select Trade Name.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txt_TradeName.Select()
                        Exit Sub
                    End If
                End If
            End If




            If lblLotNumber.Visible = True Then
                If cmbLotNumber.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Lot Number.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbLotNumber.Select()
                    Exit Sub
                End If
            End If

            If lblDosesGiven.Visible = True Then
                If txt_dosage_given.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Enter Dosage Given.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_dosage_given.Select()
                    Exit Sub
                End If
            End If

            If optAdministered.Checked = True Then
                If GetRequirefunding() = "1" Then
                    If cmbFunding.Text.Trim = "" Then
                        TabImmunization.SelectTab(TabPageAdministratin)
                        MessageBox.Show("Select Funding.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        cmbFunding.Select()
                        Exit Sub
                    End If
                End If
            End If

            If optRefused.Checked = False Then
                'Added by Amit to check Track Vaccine Inventory setting is ON or OFF
                Dim value As New Object()
                Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
                ogloSettings.GetSetting("TrackVaccineInventory", 0, gnClinicID, value)
                ogloSettings.Dispose()
                ogloSettings = Nothing
                If value = "1" Then
                    'checking individual Vaccines Inventory Track setting ON/OFF, if ON then showing
                    'Adjust Inventory screen
                    If GetVaccinInventoryTrack() > 0 Then

                        Dim ddosesOnHand As Decimal
                        ddosesOnHand = GetVaccinAvailableDosesOnHand()

                        If ddosesOnHand < CType(txt_dosage_given.Text, Decimal) Then
                            TabImmunization.SelectTab(TabPageAdministratin)
                            If MessageBox.Show("Vaccine doses given (" + txt_dosage_given.Text + ") exceed vaccine doses in inventory (" + ddosesOnHand.ToString + ").  Would you like to continue? Y/N.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                                txt_dosage_given.Select()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If lblRefusalreason.Visible = True Then
                'If cmbRefusalreason.Text.Trim = "" Then
                '    TabImmunization.SelectTab(TabPageAdministratin)
                '    MessageBox.Show("Select Refusal Reason.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    cmbRefusalreason.Select()
                '    Exit Sub
                'End If

                If txtRefusalReason.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Refusal Reason.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtRefusalReason.Select()
                    Exit Sub
                End If
            End If



            If lblRefusedBy.Visible = True Then
                If txt_refused_by.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Enter Refused By.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_refused_by.Select()
                    Exit Sub
                End If
            End If

            If txt_amount_given.Text <> "" Then
                If Not IsNumeric(txt_amount_given.Text) Then
                    txt_amount_given.Text = ""
                End If
            End If

            If txt_dosage_given.Text <> "" Then
                If Not IsNumeric(txt_dosage_given.Text) Then
                    txt_dosage_given.Text = ""
                End If
            End If

            If txtNDCcode.Text <> "" Then
                If Not IsNumeric(txtNDCcode.Text) Then
                    txtNDCcode.Text = ""
                End If
            End If

            ''Added on 20120307-To give warnings on save
            If CheckTradeNameisValidagainstSKU() = False Then
                _IsValidationFailed = True
                cmbSKU.Select()
                Exit Sub
            End If

            If txt_TradeName.Text.Trim <> "" Then
                Dim dtVaccine As DataSet
                Dim _CVX As String = ""
                Dim OIM As New gloStream.Immunization.ItemSetup
                If OIM.IsCustomTradeNameOrCVX(txt_TradeName.Text.Trim, txtCvx.Text.Trim, 0) = False Then
                    If OIM.IsCustomTradeNameOrCVX(txt_TradeName.Text.Trim, txtCvx.Text.Trim, 1) = False Then
                        dtVaccine = OIM.CheckVaccineisValidaginstTradeName(txt_TradeName.Text.Trim, txtCvx.Text.Trim)
                        If IsNothing(dtVaccine) = False Then
                            If dtVaccine.Tables("CVX").Rows.Count > 0 Then
                                _CVX = dtVaccine.Tables("CVX").Rows(0)("Vaccine")
                            End If
                            If dtVaccine.Tables("Exists").Rows.Count > 0 Then
                                If dtVaccine.Tables("Exists").Rows(0)("IsExists") = "0" Then
                                    MessageBox.Show("Based on the CDC Vaccine Database,  the Trade Name " & "'" & txt_TradeName.Text.Trim & "'" & " does not match the Vaccine Given " & "'" & txtCvx.Text.Trim & "'" & "." & Chr(13) & Chr(13) & "Please select one of the following CVX codes or use a custom CVX code: " & Space(70) & Chr(13) & _CVX, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    _IsValidationFailed = True
                                    txtCvx.Select()
                                    OIM = Nothing
                                    dtVaccine.Dispose()
                                    dtVaccine = Nothing
                                    Exit Sub
                                End If
                            End If
                            dtVaccine.Dispose()
                            dtVaccine = Nothing
                        End If
                    End If
                End If
                OIM = Nothing
            End If

            'Dim obj As Object = cmbUnitOfMeasure.SelectedValue
            'Dim obj1 As Object = cmbUnitOfMeasure.Text

            ''Warning for Unit of Measure entered/selected is not from standard set given
            If IsNothing(cmbUnitOfMeasure.SelectedValue) = True AndAlso cmbUnitOfMeasure.Text.Trim() <> "" Then
                Dim _dlgResult As DialogResult = Windows.Forms.DialogResult.None
                _dlgResult = MessageBox.Show("Unit of measure for Amount Given selected is not a recognized unit or from the standard set provided. " + Environment.NewLine + "Continue save?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)

                If _dlgResult = Windows.Forms.DialogResult.Cancel Then
                    cmbUnitOfMeasure.Select()
                    cmbUnitOfMeasure.Focus()
                    Exit Sub

                End If
            End If

            Dim sTransactiondatetime As String = ""
            Dim clsIMTran As New clsgloIMTransaction
            With clsIMTran

                .PatientID = _PatientID
                .transaction_id = _TransactionID
                If dtTransactionTime.Checked = True Then
                    sTransactiondatetime = dttransaction_date.Value.Date
                    sTransactiondatetime = sTransactiondatetime + " " + dtTransactionTime.Value.TimeOfDay.ToString()
                    .transaction_date = Convert.ToDateTime(sTransactiondatetime)
                Else
                    .transaction_date = dttransaction_date.Value.Date

                End If
                .ICDType = ICDType  ''added for ICd10 implementation

                If optAdministered.Checked Then
                    .admin_repo_refused = 0
                ElseIf optReported.Checked Then
                    .admin_repo_refused = 1
                ElseIf optRefused.Checked Then
                    .admin_repo_refused = 2
                ElseIf optNotAdministered.Checked Then
                    .admin_repo_refused = 3
                ElseIf optPartiallyAdministered.Checked Then
                    .admin_repo_refused = 4
                End If

                .administered_id = cmbAdministred.SelectedValue
                .nLocationID = cmbLocation.SelectedValue
                .ProviderID = cmbProvider.SelectedValue

                If cmbSKU.Text.Trim <> "" Then
                    .sku = cmbSKU.Text
                End If

                .vaccine = txtCvx.Text
                .tradeName = txt_TradeName.Text
                .manufacturer = txtMvx.Text

                .nCategoryID = cmbCategory.SelectedValue
                .lot_number = cmbLotNumber.Text

                If dtexpDate.Checked = True Then
                    .expiration_date = dtexpDate.Value
                End If

                If optRefused.Checked = False Then
                    If txt_dosage_given.Text = "" Then
                        .dosage_given = 0
                    Else
                        .dosage_given = txt_dosage_given.Text
                    End If

                    If txt_amount_given.Text = "" Then
                        .amount_given = 0
                    Else
                        .amount_given = txt_amount_given.Text
                    End If

                    '.units = txt_units.Text
                    If Not IsNothing(cmbUnitOfMeasure.SelectedValue) Then
                        If Not IsDBNull(cmbUnitOfMeasure.SelectedValue) Then
                            .units = cmbUnitOfMeasure.SelectedValue.ToString().Trim()
                        Else
                            .units = ""
                        End If
                    Else
                        .units = cmbUnitOfMeasure.Text.Trim()
                    End If

                    .route = cmbRoute.Text
                    .Site = cmbSite.Text
                Else
                    .dosage_given = 0
                    .amount_given = 0
                    .units = ""
                End If

                'If chk_vis_given.Checked Then
                '    .bvis_given = 1
                'Else
                '    .bvis_given = 0
                'End If

                If getIsVISDocAssociated() Then
                    .bvis_given = 1
                Else
                    .bvis_given = 0
                End If

                .Status = cmbStatus.Text.Trim()
                .vis = txt_vis.Text
                .visDocumentID = _DocumentID
                .VisAssociatedDocumentID = _AssociatedDocumentID
                If dtpublication_date.Checked = True Then
                    .publication_date = dtpublication_date.Value
                End If

                If optRefused.Checked Then
                    .refusal_reason = strRefusalDescription
                    .refusal_reasonCode = strRefusalCode
                    .refused_by = txt_refused_by.Text
                    .refusal_comments = txt_refusal_comments.Text

                    .CQM_Code = strCQMCode
                    .CQM_Desc = strCQMDescription
                    .Cqmlbl_by = txtcqm.Text
                End If

                .reminder = chkSetReminder.Checked

                If dtDueDate.Checked = True Then
                    .due_date = dtDueDate.Value
                End If

                For i As Integer = 0 To cmbIcd.Items.Count - 1
                    cmbIcd.SelectedIndex = i
                    If .Diagnosis_code = "" Then
                        .Diagnosis_code = cmbIcd.Text
                    Else
                        .Diagnosis_code = .Diagnosis_code & "," & cmbIcd.Text
                    End If
                Next

                For i As Integer = 0 To cmbCpt.Items.Count - 1
                    cmbCpt.SelectedIndex = i
                    If .cpt_code = "" Then
                        .cpt_code = cmbCpt.Text
                    Else
                        .cpt_code = .cpt_code & "," & cmbCpt.Text
                    End If
                Next

                If txtNDCcode.Text <> "" Then
                    .ndc_code = txtNDCcode.Text
                End If
                .funding = cmbFunding.Text
                .immunizationfunding = cmbImmunizationFunding.Text
                .notes = txt_notes.Text

                .bPatientHasAReaction = chkPatientHasReaction.Checked
                .OnsetDate = dtOnsetDate.Value

                .sAdverseEvent = txtAdverseEvent.Text
                .bPatientDied = chkPatientDied.Checked

                If chkPatientDied.Checked = True Then
                    .PatientDieddate = dtPatientDied.Value
                End If

                .bLifeThreateningIllness = chk_LifeThreateningIllness.Checked
                .bRequiredEmergencyRoom = chk_RequiredEmergencyRoom.Checked
                .bRequiredHospitalization = chk_RequiredHospitalization.Checked
                .HospitalizationDays = Val(txtHospitalizationDays.Text)
                .bResultedInProlongation = chk_ResultedInProlongation.Checked
                .bResultedInPermDisability = chk_ResultedInPermDisability.Checked
                .bNoneOfTheAbove = chk_NoneOfTheAbove.Checked

                If rdo_PatientRecoveredYes.Checked = True Then
                    .sPatientRecovered = "Y"
                ElseIf rdo_PatientRecoveredNo.Checked = True Then
                    .sPatientRecovered = "N"
                ElseIf rdo_PatientRecoveredUnknown.Checked = True Then
                    .sPatientRecovered = "U"
                End If

                If lstReaction.Items.Count > 0 Then
                    For i As Integer = 0 To lstReaction.Items.Count - 1
                        If i = 0 Then
                            .Reaction = lstReaction.Items(i).ToString()
                        Else
                            .Reaction = .Reaction & vbNewLine & lstReaction.Items(i).ToString()
                        End If
                    Next
                End If

                If Not IsNothing(lblSnomedIdValue.Tag) Then

                    Dim _snomedInfo As ArrayList = Nothing
                    '.SnoMedConceptID = Convert.ToString(lblSnomedIdValue.Tag)

                    Try
                        'If unable to cast (will never be a situation) will just fetch the ID from tag
                        _snomedInfo = CType(lblSnomedIdValue.Tag, ArrayList)

                        If Not IsNothing(_snomedInfo) AndAlso _snomedInfo.Count > 0 Then

                            .SnoMedConceptID = Convert.ToString(_snomedInfo(0))
                            .SnoMedConceptDescription = Convert.ToString(_snomedInfo(1))

                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Unable to read Snomed code selected on Immunization", False)
                    Finally
                        _snomedInfo = Nothing
                    End Try
                Else

                    .SnoMedConceptID = ""
                    .SnoMedConceptDescription = ""

                End If

                If Not IsNothing(cmbPublicityCode.SelectedValue) AndAlso Convert.ToString(cmbPublicityCode.SelectedValue).Trim() <> "" Then
                    .PublicityCode = cmbPublicityCode.SelectedValue
                    .PublicityCodeDescription = cmbPublicityCode.Text
                End If
                .publicity_effective_date = publicityeffetiveDTP.Value

                If Not IsNothing(lblOrdProvider.Text) AndAlso Convert.ToString(lblOrdProvider.Tag).Trim() <> "" Then
                    .OrderingProviderID = lblOrdProvider.Tag
                    .OrderingProviderType = OrdProviderType
                Else
                    .OrderingProviderID = 0
                    .OrderingProviderType = OrderingProviderType.None
                End If

                Dim StrAudittrailString As String = ""
                Dim sActivityType As String = ""

                If (_TransactionID <= 0) Then
                    StrAudittrailString = "Immunization Record Added."
                    sActivityType = "Insert"
                    .ImmunizationStatus = "A"
                Else
                    StrAudittrailString = "Immunization Record Modified."
                    sActivityType = "After Update"
                    .ImmunizationStatus = "U"
                End If

                'start of Added by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX
                Try
                    If Not lUncertainCVX Is Nothing Then
                        .DtUncertainFormulationCVX = getUncettainFormulationCVX_Table()
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Unable to read uncertain formulation CVX selected on Immunization", False)
                End Try
                'end of Added by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX

                Try
                    If c1VIS.Rows.Count > 1 Then
                        .DtImmunizationVIS = getImmunizationVIS_Table()
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Unable to read uncertain formulation CVX selected on Immunization", False)
                End Try

                Dim TranctionID As Int64 = -1
                TranctionID = .AddIMTransaction()

                Dim nAuditTrailID As Int64 = 0
                If _TransactionID <= 0 Then
                    nAuditTrailID = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, StrAudittrailString, _PatientID, TranctionID, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    nAuditTrailID = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, StrAudittrailString, _PatientID, _TransactionID, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                If (TranctionID > 0) Then
                    _TransactionID = TranctionID
                End If
                .AddIMHistory(nAuditTrailID, sActivityType)

                isSaved = True
                If IsNothing(_lst) = False Then
                    _lst.IsFinished = True
                End If

                If IsNothing(sender) = False Then
                    _isSaveClicked = True
                    _isClose = True
                    Me.Close()
                End If

            End With
            clsIMTran = Nothing

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Error.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tblbtn_PrintVis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_PrintVis.Click
        Try

            'If isSaved = False Then
            '    'If (MessageBox.Show("The Immunization record needs to be saved before open VIS Document. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            '    Try
            '        tblbtn_Save_Click(Nothing, Nothing)
            '        If (isSaved = False) Then
            '            Exit Sub
            '        End If
            '    Catch ex As Exception
            '    Finally
            '        isSaved = False
            '    End Try
            'End If

            'Developer:Dipak Patil
            'Date:20120207
            'Bug ID/PRD Name/Sales force Case:Immunization PRD
            'Reason: CreateAuditLog Entry For Immunization
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "VIS document viewed.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            'End If


            Dim DocumentIDForCopy As Int64 = 0

            If c1VIS.Rows.Count > 1 Then
                Dim selectedRow As Integer = c1VIS.RowSel
                If Not IsDBNull(c1VIS.GetData(selectedRow, 1)) Then
                    DocumentIDForCopy = c1VIS.GetData(selectedRow, 1)
                Else
                    DocumentIDForCopy = 0
                End If
            End If


            Dim IsAssociatedDocumentIDPresentForPatient As Boolean = False
            Dim AlreadyExistDocumentID As Int64 = -1
            AlreadyExistDocumentID = GetAlreadyExistDocumentID(_PatientID, DocumentIDForCopy)

            If (AlreadyExistDocumentID > 0) Then
                IsAssociatedDocumentIDPresentForPatient = True
                _DocumentID = AlreadyExistDocumentID
            End If

            If (DocumentIDForCopy > 0) And (IsAssociatedDocumentIDPresentForPatient = False) Then
                CopyVisDocument(_PatientID, DocumentIDForCopy)
            End If

            If (_DocumentID > 0) Then
                ViewScanDoucment()
                chk_vis_given.Checked = True
                If _DocumentID > 0 Then
                    c1VIS.SetData(c1VIS.RowSel, 1, _DocumentID)
                    c1VIS.SetData(c1VIS.RowSel, 2, True)
                Else
                    c1VIS.SetData(c1VIS.RowSel, 1, _DocumentID)
                    c1VIS.SetData(c1VIS.RowSel, 2, False)
                End If
            Else
                btnScan_Click(Nothing, Nothing)
                chk_vis_given.Checked = True

                If _DocumentID > 0 Then
                    c1VIS.SetData(c1VIS.RowSel, 1, _DocumentID)
                    c1VIS.SetData(c1VIS.RowSel, 2, True)
                Else
                    c1VIS.SetData(c1VIS.RowSel, 2, False)
                End If
            End If

            If c1VIS.Rows.Count > 1 Then
                Dim isMasterEntryExists As Boolean = False
                For i As Integer = 1 To c1VIS.Rows.Count - 1
                    If c1VIS.Rows(i)(5) = "" Then
                        _DocumentID = c1VIS.Rows(i)(1)
                        isMasterEntryExists = True
                        txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                        If txt_vis.Text = "" Then
                            _DocumentID = 0
                        End If
                    End If
                Next

                If isMasterEntryExists = False Then
                    txt_vis.Text = ""
                    _DocumentID = 0
                End If

            End If

            Me.Focus()
            isDocumentImported()
            TabImmunization.SelectedIndex = 2

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click

        Me.Close()
    End Sub

#End Region

#Region "SelectedIndexChanged Event"

    Private Sub cmbSKU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSKU.SelectedIndexChanged

        Dim dtsku As DataTable
        _isLoadGridCvxControl = True
        Try

            RemoveHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
            RemoveHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
            RemoveHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged

            dtsku = GetSKUDetails()
            If (IsNothing(dtsku) = False) Then
                If dtsku.Rows.Count > 0 Then
                    txtCvx.Text = dtsku.Rows(0)("Vaccine")
                    txt_TradeName.Text = dtsku.Rows(0)("im_sTradeName")
                    txtMvx.Text = dtsku.Rows(0)("im_sManufacturer")
                    ICDType = dtsku.Rows(0)("nICDRevision") ''added for ICd10 implementation
                    If dtsku.Rows(0)("im_sNDCCode") <> 0 Then
                        txtNDCcode.Text = dtsku.Rows(0)("im_sNDCCode")
                    End If

                    If dtsku.Rows(0)("im_Diagnosis_Code") <> "" Then
                        'cmbIcd.Items.Clear()
                        cmbIcd.DataSource = Nothing
                        cmbIcd.Items.Clear()
                        Dim arrICD() As String
                        arrICD = dtsku.Rows(0)("im_Diagnosis_Code").split(",")
                        For i As Integer = 0 To arrICD.GetUpperBound(0)
                            cmbIcd.Items.Add(arrICD(i))
                        Next
                        If cmbIcd.Items.Count > 0 Then
                            cmbIcd.SelectedIndex = 0
                        End If
                    End If

                    If dtsku.Rows(0)("im_cpt_code") <> "" Then
                        'cmbCpt.Items.Clear()
                        cmbCpt.DataSource = Nothing
                        cmbCpt.Items.Clear()
                        Dim arrCPT() As String
                        arrCPT = dtsku.Rows(0)("im_cpt_code").split(",")
                        For i As Integer = 0 To arrCPT.GetUpperBound(0)
                            cmbCpt.Items.Add(arrCPT(i))
                        Next
                        If cmbCpt.Items.Count > 0 Then
                            cmbCpt.SelectedIndex = 0
                        End If
                    End If
                    'fillGrid()
                ElseIf cmbSKU.Text.Length > 0 Then
                    txtCvx.Text = ""
                    txt_TradeName.Text = ""
                    txtMvx.Text = ""

                    cmbIcd.DataSource = Nothing
                    cmbIcd.Items.Clear()

                    cmbCpt.DataSource = Nothing
                    cmbCpt.Items.Clear()

                    txtNDCcode.Text = ""
                End If
                dtsku.Dispose()
                dtsku = Nothing
            End If

            dtexpDate.Checked = False

            'Fill Lot No.

            'dtsku = Nothing
            '            dtsku = New DataTable

            Dim strSQL As String

            strSQL = " SELECT Distinct im_LotNumber " & _
                     " FROM IM_MST" & _
                     " where im_sActive = 'Active' and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "' and im_nLocationID = " & cmbLocation.SelectedValue

            strSQL = strSQL + " union select distinct im_trn_Lotnumber as im_LotNumber " & _
                              " from im_trn_dtl " & _
                              " where im_trn_Lotnumber not in " & _
                              " (select distinct im_LotNumber from IM_MST) " & _
                              " and sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'" & _
                              " and nLocationID = " & cmbLocation.SelectedValue & _
                              " order by im_LotNumber"


            If Trim(cmbSKU.Text) <> "" Then
                dtsku = GetList(strSQL)
                If (IsNothing(dtsku) = False) Then
                    If dtsku.Rows.Count > 0 Then

                        RemoveHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged
                        RemoveHandler cmbLotNumber.TextChanged, AddressOf cmbLotNumber_TextChanged

                        cmbLotNumber.DataSource = dtsku
                        cmbLotNumber.ValueMember = "im_LotNumber"
                        cmbLotNumber.DisplayMember = "im_LotNumber"

                        AddHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged
                        AddHandler cmbLotNumber.TextChanged, AddressOf cmbLotNumber_TextChanged

                        If dtsku.Rows.Count = 1 Then
                            cmbLotNumber.SelectedIndex = -1
                            cmbLotNumber.SelectedIndex = 0
                        Else
                            lblDosesOnHand.Text = ""
                            If cmbCategory.Items.Count > 0 Then
                                cmbCategory.SelectedValue = 0
                            End If

                            cmbLotNumber.SelectedIndex = -1
                        End If
                    End If
                End If

            End If

            'SetVISDocumentDetails()

            'GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())

            'If (txt_vis.Text.Trim() = "") Then
            'Dim AssociatedVisDocumentID As Int64 = GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())
            'txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(AssociatedVisDocumentID)
            'End If

            AddHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
            AddHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
            AddHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged

            GetVaccinAvailableDoses()

            Get_AllUncertainFomulationCVX()

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _isLoadGridCvxControl = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbLotNumber_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLotNumber.SelectedIndexChanged
        SetExpiryDate()
        SetVISDocumentDetails()
        GetVaccinAvailableDoses()
        fillGrid()
    End Sub

    Private Sub cmbLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        FillSKUList()
        combo = DirectCast(sender, ComboBox)
        If cmbLocation.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation")), cmbLocation) >= cmbLocation.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation"))
                If ToolTip1.GetToolTip(cmbLocation) <> txt Then
                    ToolTip1.SetToolTip(cmbLocation, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbLocation, "")
            End If
        End If
    End Sub

#End Region

#Region "CheckedChanged Event"

    Private Sub chkPatientHasReaction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPatientHasReaction.CheckedChanged
        If chkPatientHasReaction.Checked = True Then
            grpReaction.Enabled = True
            If rdo_PatientRecoveredNo.Checked = False And rdo_PatientRecoveredYes.Checked = False And rdo_PatientRecoveredUnknown.Checked = False Then
                rdo_PatientRecoveredUnknown.Checked = True
            End If
            dtOnsetDate.Select()
        Else
            grpReaction.Enabled = False
        End If
    End Sub

    Private Sub chkPatientDied_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPatientDied.CheckedChanged
        dtPatientDied.Enabled = chkPatientDied.Checked

        If chkPatientDied.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If

    End Sub

    Private Sub chk_NoneOfTheAbove_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_NoneOfTheAbove.CheckedChanged
        If chk_NoneOfTheAbove.Checked = True Then
            chkPatientDied.Checked = False
            dtPatientDied.Enabled = False
            chk_LifeThreateningIllness.Checked = False
            chk_RequiredEmergencyRoom.Checked = False
            txtHospitalizationDays.Text = ""
            chk_RequiredHospitalization.Checked = False
            chk_ResultedInProlongation.Checked = False
            chk_ResultedInPermDisability.Checked = False
        End If
    End Sub

    Private Sub optRefused_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRefused.CheckedChanged
        ShowRequiredLable()
        If optRefused.Checked Then
            lblDosesOnHand.Visible = False
            GetLocationProviderList()
        End If
    End Sub

    Private Sub chk_LifeThreateningIllness_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_LifeThreateningIllness.CheckedChanged
        If chk_LifeThreateningIllness.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If
    End Sub

    Private Sub chk_RequiredEmergencyRoom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_RequiredEmergencyRoom.CheckedChanged
        If chk_RequiredEmergencyRoom.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If
    End Sub

    Private Sub chk_RequiredHospitalization_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_RequiredHospitalization.CheckedChanged
        If chk_RequiredHospitalization.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If

        If chk_RequiredHospitalization.Checked = True Then
            txtHospitalizationDays.Enabled = True
        Else
            txtHospitalizationDays.Enabled = False
        End If
    End Sub

    Private Sub chk_ResultedInProlongation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ResultedInProlongation.CheckedChanged
        If chk_ResultedInProlongation.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If
    End Sub

    Private Sub optAdministered_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAdministered.CheckedChanged
        If optAdministered.Checked = True Then
            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If
            If _TransactionID > 0 Then
                If Trim(txt_TradeName.Text) = "" Then
                    txt_TradeName.Enabled = True
                    btnTradeName.Enabled = True
                    BtnAddTradeNameCategory.Enabled = True
                End If
            End If
            lblDosesOnHand.Visible = True
            ShowRequiredLable()
            GetLocationProviderList()
        End If
    End Sub

    Private Sub optReported_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReported.CheckedChanged
        If optReported.Checked = True Then
            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If
            If _TransactionID > 0 Then
                txt_TradeName.Enabled = False
                btnTradeName.Enabled = False
                BtnAddTradeNameCategory.Enabled = False
            End If
            lblDosesOnHand.Visible = True
            ShowRequiredLable()
            GetLocationProviderList(True)
        End If

    End Sub

    Private Sub chk_ResultedInPermDisability_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ResultedInPermDisability.CheckedChanged
        If chk_ResultedInPermDisability.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If
    End Sub

#End Region

#Region "ItemSelectedClick Event"

    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                If oDiagnosisListControl.SelectedItems.Count > 5 Then
                    MessageBox.Show("Select only 5 Diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oDiagnosisListControl.CloseOnDoubleClick = False
                Else
                    'SLR: 2/18/2015: what is the purpose of filling toList here?
                    Dim dtICD9Code As DataTable
                    ' Dim ToList As gloGeneralItem.gloItems
                    dtICD9Code = New DataTable
                    Dim dcID As New DataColumn("ID")
                    Dim dcDescription As New DataColumn("Code")
                    dtICD9Code.Columns.Add(dcID)
                    dtICD9Code.Columns.Add(dcDescription)
                    '  ToList = New gloGeneralItem.gloItems()
                    '   Dim ToItem As gloGeneralItem.gloItem
                    For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtICD9Code.NewRow()
                        drTemp("ID") = oDiagnosisListControl.SelectedItems(i).ID
                        drTemp("Code") = oDiagnosisListControl.SelectedItems(i).Code
                        dtICD9Code.Rows.Add(drTemp)
                        '  ToItem = New gloGeneralItem.gloItem()
                        ' ToItem.ID = oDiagnosisListControl.SelectedItems(i).ID
                        ' ToItem.Description = oDiagnosisListControl.SelectedItems(i).Code
                        ' ToList.Add(ToItem)
                        ' ToItem = Nothing
                    Next
                    cmbIcd.DataSource = dtICD9Code
                    cmbIcd.ValueMember = dtICD9Code.Columns("ID").ColumnName
                    cmbIcd.DisplayMember = dtICD9Code.Columns("Code").ColumnName
                    ofrmDiagnosisList.Close()
                End If
            Else
                ' cmbIcd.Items.Clear()
                cmbIcd.DataSource = Nothing
                cmbIcd.Items.Clear()
                ofrmDiagnosisList.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            'code added by dipak 20090910 for add all selected code in dataTable and Bind that datable to cmbCPT

            If oListControl.SelectedItems.Count > 0 Then
                If oListControl.SelectedItems.Count > 5 Then
                    MessageBox.Show("Select only 5 CPT", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oListControl.CloseOnDoubleClick = False
                Else
                    'SLR: 2/18/2015: what is the purpose of filling tolist here?
                    Dim dtCPTCode As DataTable
                    Dim ToList As gloGeneralItem.gloItems
                    dtCPTCode = New DataTable
                    Dim dcID As New DataColumn("ID")
                    Dim dcDescription As New DataColumn("Code")
                    dtCPTCode.Columns.Add(dcID)
                    dtCPTCode.Columns.Add(dcDescription)
                    ToList = New gloGeneralItem.gloItems()
                    Dim ToItem As gloGeneralItem.gloItem
                    For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtCPTCode.NewRow()
                        drTemp("ID") = oListControl.SelectedItems(i).ID
                        drTemp("Code") = oListControl.SelectedItems(i).Code
                        dtCPTCode.Rows.Add(drTemp)
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = oListControl.SelectedItems(i).ID
                        ToItem.Description = oListControl.SelectedItems(i).Code
                        ToList.Add(ToItem)
                        ToItem.Dispose()
                        ToItem = Nothing
                    Next
                    cmbCpt.DataSource = dtCPTCode
                    cmbCpt.ValueMember = dtCPTCode.Columns("ID").ColumnName
                    cmbCpt.DisplayMember = dtCPTCode.Columns("Code").ColumnName
                    If (cmbCpt.Items.Count > 0) Then
                        cmbCpt.SelectedIndex = 0
                    End If

                    ofrmList.Close()
                    ToList.Dispose()
                    ToList = Nothing
                End If
            Else
                ''Added Rahul for Fixed BugID 6726 on 20101129
                ' cmbCpt.Items.Clear()
                cmbCpt.DataSource = Nothing
                cmbCpt.Items.Clear()
                ofrmList.Close()
                ''End
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "ItemSelected Event"

    Private Sub oTradeNameControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem
            If oTradeNameControl.SelectedItems IsNot Nothing Then
                If oTradeNameControl.SelectedItems.Count > 0 Then
                    oProcedure = oTradeNameControl.SelectedItems(0)
                    Select Case oTradeNameControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.TradeName
                            txt_TradeName.Text = oProcedure.Description
                    End Select
                    CloseProcedureControl("TradeName")
                Else
                End If
            Else
            End If
            txt_TradeName.Select()
            GetCVXMVX()
            GetCPTCodeFromCVX()
            Get_AllUncertainFomulationCVX() 'Added by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oMvxControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oMVXControl.SelectedItems IsNot Nothing Then
                If oMVXControl.SelectedItems.Count > 0 Then
                    oProcedure = oMVXControl.SelectedItems(0)
                    Select Case oMVXControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.Mvx
                            txtMvx.Text = oProcedure.Description
                    End Select
                    CloseProcedureControl("Mvx")
                Else
                End If
            Else
            End If
            txtMvx.Select()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oCvxControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oCVXControl.SelectedItems IsNot Nothing Then
                If oCVXControl.SelectedItems.Count > 0 Then
                    oProcedure = oCVXControl.SelectedItems(0)
                    Select Case oCVXControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.Cvx
                            txtCvx.Text = oProcedure.Description
                    End Select
                    CloseProcedureControl("Cvx")
                Else
                End If
            End If
            txtCvx.Focus()
            GetCPTCodeFromCVX()
            Get_AllUncertainFomulationCVX() 'Added by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "ItemClosedClick Event"

    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmDiagnosisList.Close()
        'If IsNothing(ofrmDiagnosisList) = False Then 'SLR: Don't make nothing so that it get disposed when dialog closes..

        '    ofrmDiagnosisList = Nothing
        'End If
    End Sub

    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ofrmList.Close()
            'If IsNothing(ofrmList) = False Then 'SLR: Don't make nothing so that it get disposed when dialog closes..
            '    ofrmList = Nothing
            'End If
        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "InternalgridLostFocus Event"

    Private Sub oCvxControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlCvxControl.Visible Then
            If oCVXControl IsNot Nothing Then
                If oCVXControl.IsRecordExist(txtCvx.Text.Trim) = False Then
                    txtCvx.Text = ""
                    CloseProcedureControl("Cvx")
                Else
                    CloseProcedureControl("Cvx")
                End If
            End If
        End If
        ' _isLoadGridCvxControl = False
    End Sub

    Private Sub oTradeNameControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlTradeNameControl.Visible Then
            If oTradeNameControl IsNot Nothing Then

                If oTradeNameControl.IsRecordExist(txt_TradeName.Text.Trim) = False Then
                    txt_TradeName.Text = ""
                    CloseProcedureControl("TradeName")
                Else
                    CloseProcedureControl("TradeName")
                End If
            End If
        End If
    End Sub

    Private Sub oMvxControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlMvxControl.Visible Then
            If oMVXControl IsNot Nothing Then

                If oMVXControl.IsRecordExist(txtMvx.Text.Trim) = False Then
                    txtMvx.Text = ""
                    CloseProcedureControl("Mvx")
                Else
                    CloseProcedureControl("Mvx")
                End If
            End If
        End If
    End Sub

#End Region

#Region "InternalGridKeyDown Event"

    Private Sub oCvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub oTradeNameControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub oMvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

#End Region

#Region "MouseEnter"

    Private Sub cmbLocation_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.MouseEnter
        combo = DirectCast(sender, ComboBox)
        If cmbLocation.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation")), cmbLocation) >= cmbLocation.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation"))
                If ToolTip1.GetToolTip(cmbLocation) <> txt Then
                    ToolTip1.SetToolTip(cmbLocation, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbLocation, "")

            End If
        End If
    End Sub

#End Region

#Region "KeyPress Event"

    Private Sub txtNDCcode_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNDCcode.KeyPress
        AllowNumericValue(txtNDCcode.Text, e)
    End Sub

    Private Sub txtHospitalizationDays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHospitalizationDays.KeyPress
        AllowNumericValue(txtHospitalizationDays.Text, e)
    End Sub

    Private Sub txtNDCcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        AllowNumericValue(txtNDCcode.Text, e)
    End Sub

    Private Sub oCvxControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'RemoveHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
        'txtCvx.Text = txtCvx.Text & e.KeyChar
        'txtCvx.Select()
        'AddHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
    End Sub

    Private Sub txt_dosage_given_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_dosage_given.KeyPress
        AllowDecimal(txt_dosage_given.Text, e)
    End Sub

    Private Sub txt_amount_given_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_amount_given.KeyPress
        AllowDecimal(txt_amount_given.Text, e)
    End Sub


#End Region

#Region "MouseMove Event"

    Private Sub oMVXControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oMVXControl.C1GridList.Select()
    End Sub

    Private Sub oTradeNameControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oTradeNameControl.C1GridList.Select()
    End Sub

    Private Sub oCvxControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oCVXControl.C1GridList.Select()
    End Sub

#End Region

#Region "MouseLeave Event"

    Private Sub btnMvx_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnMvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnCvx_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnCvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnTradeName_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

#End Region

#Region "TextChanged Event"

    Private Sub cmbLotNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLotNumber.TextChanged
        Try
            SetExpiryDate()
            SetVISDocumentDetails()
            GetVaccinAvailableDoses()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_vis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_vis.TextChanged
        'isSaved = False
    End Sub

    Private Sub txtMvx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMvx.TextChanged
        Try
            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txtMvx.Text
                If (_strSearchString.Trim() <> "") Then

                    If IsNothing(oMVXControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
                    Else
                        If oMVXControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
                        End If
                    End If

                    If Not IsNothing(oMVXControl) Then
                        oMVXControl.FillControl(_strSearchString)
                    End If
                End If

                If IsNothing(oMVXControl) = False Then
                    If oMVXControl.C1GridList.Focused Then
                        SetCursorPos(txtMvx.Left + Me.Left + 300, txtMvx.Top + Me.Top + 110)
                    End If
                    RemoveHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
                    txtMvx.Focus()
                    txtMvx.SelectionStart = Len(txtMvx.Text)
                    AddHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
                End If

                'Added by Amit show available doses on hand
                GetVaccinAvailableDoses()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_TradeName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_TradeName.TextChanged
        Try
            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txt_TradeName.Text.Trim
                If (_strSearchString.Trim() <> "") Then

                    If IsNothing(oTradeNameControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
                    Else
                        If oTradeNameControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
                        End If
                    End If

                    If Not IsNothing(oTradeNameControl) Then
                        oTradeNameControl.FillControl(_strSearchString)
                    End If
                Else
                    _isLoadGridCvxControl = True
                    txtCvx.Text = ""
                    cmbCpt.Items.Clear()
                    txtMvx.Text = ""
                    _isLoadGridCvxControl = False
                End If

                If IsNothing(oTradeNameControl) = False Then
                    If oTradeNameControl.C1GridList.Focused Then
                        SetCursorPos(txt_TradeName.Left + Me.Left + 300, txt_TradeName.Top + Me.Top + 110)
                    End If
                    RemoveHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
                    txt_TradeName.Focus()
                    txt_TradeName.SelectionStart = Len(txt_TradeName.Text)
                    AddHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
                End If
            End If

            'Added by Amit show available doses on hand
            GetVaccinAvailableDoses()

            SetVISDocumentDetails()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCvx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCvx.TextChanged
        Try
            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txtCvx.Text
                If (_strSearchString.Trim() <> "") Then

                    If IsNothing(oCVXControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
                    Else
                        If oCVXControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
                        End If
                    End If

                    If Not IsNothing(oCVXControl) Then
                        oCVXControl.FillControl(_strSearchString)
                    End If
                Else
                    cmbCpt.Items.Clear()
                    lUncertainCVX = Nothing
                    BtnUncertainCVX.Enabled = False
                End If
            End If

            FillVaccineLot()
            'Added by Amit show available doses on hand
            GetVaccinAvailableDoses()
            SetVISDocumentDetails()

            'GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())


            'Try
            '    Dim AssociatedVisDocumentID As Int64 = GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())
            '    txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(AssociatedVisDocumentID)
            'Catch ex As Exception

            'End Try

            If IsNothing(oCVXControl) = False Then
                If oCVXControl.C1GridList.Focused Then
                    SetCursorPos(txtCvx.Left + Me.Left + 300, txtCvx.Top + Me.Top + 110)
                End If
                RemoveHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
                txtCvx.Focus()
                txtCvx.SelectionStart = Len(txtCvx.Text)
                AddHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "MouseHover Event"

    Private Sub btnSearchsku_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnSearchsku, "Select SKU")
    End Sub

    Private Sub btnBrowsDiagnosis_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnBrowsDiagnosis, "Select Diagnosis")
    End Sub

    Private Sub btnBrowsCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnBrowsCPT, "Select CPT")
    End Sub

    Private Sub btnTradeName_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
        ToolTip1.SetToolTip(btnTradeName, "Select Trade Name")
    End Sub

    Private Sub btnMvx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnMvx.BackgroundImageLayout = ImageLayout.Stretch
        ToolTip1.SetToolTip(btnMvx, "Select Manufacturer")
    End Sub

    Private Sub btnCvx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnCvx.BackgroundImageLayout = ImageLayout.Stretch
        ToolTip1.SetToolTip(btnCvx, "Select Vaccine")
    End Sub



#End Region

#Region "KeyUp Event"

    Private Sub txtMvx_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMvx.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlMvxControl.Visible Then
                    If oMVXControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oMVXControl.GetCurrentSelectedItem()
                        'If _IsItemSelected Then

                        'End If
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlMvxControl.Visible Then
                    If oMVXControl IsNot Nothing Then
                        oMVXControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlMvxControl.Visible Then
                    If oMVXControl IsNot Nothing Then
                        oMvxControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txt_TradeName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_TradeName.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlTradeNameControl.Visible Then
                    If oTradeNameControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oTradeNameControl.GetCurrentSelectedItem()
                        'If _IsItemSelected Then

                        'End If
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlTradeNameControl.Visible Then
                    If oTradeNameControl IsNot Nothing Then
                        oTradeNameControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlTradeNameControl.Visible Then
                    If oTradeNameControl IsNot Nothing Then
                        oTradeNameControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txtCvx_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCvx.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlCvxControl.Visible Then
                    If oCVXControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oCVXControl.GetCurrentSelectedItem()
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlCvxControl.Visible Then
                    If oCVXControl IsNot Nothing Then
                        oCVXControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlCvxControl.Visible Then
                    If oCVXControl IsNot Nothing Then
                        oCvxControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


#End Region

#Region "LostFocus Event"

    Private Sub txtMvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMvx.LostFocus
        If pnlMvxControl.Visible Then
            If oMVXControl IsNot Nothing Then
                If oMVXControl.Focus() = False Then
                    If oMVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Mvx")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnTradeName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTradeName.LostFocus
        If pnlTradeNameControl.Visible Then
            _isLoadGridCvxControl = True
            If oTradeNameControl IsNot Nothing Then
                If oTradeNameControl.Focus() = False Then
                    If oTradeNameControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("TradeName")
                    End If
                End If
            End If
            _isLoadGridCvxControl = False
        End If
    End Sub

    Private Sub btnCvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCvx.LostFocus
        If pnlCvxControl.Visible Then
            _isLoadGridCvxControl = True
            If oCVXControl IsNot Nothing Then
                If oCVXControl.Focus() = False Then
                    If oCVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Cvx")
                    End If
                End If
            End If
            _isLoadGridCvxControl = False
        End If
    End Sub

    Private Sub btnMvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMvx.LostFocus
        If pnlMvxControl.Visible Then
            If oMVXControl IsNot Nothing Then
                If oMVXControl.Focus() = False Then
                    If oMVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Mvx")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtCvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCvx.LostFocus
        If pnlCvxControl.Visible Then
            If oCVXControl IsNot Nothing Then
                If oCVXControl.Focus() = False Then
                    If oCVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Cvx")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txt_TradeName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_TradeName.LostFocus
        If pnlTradeNameControl.Visible Then
            If oTradeNameControl IsNot Nothing Then
                If oTradeNameControl.Focus() = False Then
                    If oTradeNameControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("TradeName")
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "SubProcedure"

    Private Sub FillControl()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dsIM As DataSet = Nothing

        Try
            Me.Cursor = Cursors.WaitCursor

            'default Administered should be selected
            optAdministered.Checked = True

            'selecting records of User, provider, sku, route, site refusal reason, funding list from below sp
            oDB.Connect(False)
            oDB.Retrive("IM_TransactionFillControl", dsIM)
            oDB.Disconnect()

            If (IsNothing(dsIM) = False) Then

                'Get Administer List
                cmbAdministred.DataSource = dsIM.Tables(0)
                cmbAdministred.ValueMember = "nUserID"
                cmbAdministred.DisplayMember = "sLoginName"
                cmbAdministred.SelectedValue = gnLoginID

                'Get Provider List
                cmbProvider.DataSource = dsIM.Tables(1)
                cmbProvider.ValueMember = "nProviderID"
                cmbProvider.DisplayMember = "Provider"
                cmbProvider.SelectedValue = gnPatientProviderID

                'Get Route List
                cmbRoute.DataSource = dsIM.Tables(2)
                cmbRoute.ValueMember = "nCategoryID"
                cmbRoute.DisplayMember = "sDescription"
                cmbRoute.SelectedIndex = -1

                ''Get Site List
                cmbSite.DataSource = dsIM.Tables(3)
                cmbSite.ValueMember = "nCategoryID"
                cmbSite.DisplayMember = "sDescription"
                cmbSite.SelectedIndex = -1

                'Get Refusal Reason List
                cmbRefusalreason.DataSource = dsIM.Tables(4)
                cmbRefusalreason.ValueMember = "RefusalReason"
                cmbRefusalreason.DisplayMember = "RefusalReason"
                cmbRefusalreason.SelectedIndex = -1

                'Get Funding List
                cmbFunding.DataSource = dsIM.Tables(5)
                cmbFunding.ValueMember = "Funding"
                cmbFunding.DisplayMember = "Funding"
                cmbFunding.SelectedIndex = -1

                'Get Location List
                RemoveHandler cmbLocation.SelectedIndexChanged, AddressOf cmbLocation_SelectedIndexChanged
                cmbLocation.DataSource = dsIM.Tables(6)
                cmbLocation.ValueMember = "nLocationID"
                cmbLocation.DisplayMember = "sLocation"
                cmbLocation.SelectedValue = GetIMLocation()
                AddHandler cmbLocation.SelectedIndexChanged, AddressOf cmbLocation_SelectedIndexChanged

                'Get Funding List
                RemoveHandler cmbCategory.SelectedValueChanged, AddressOf cmbCategory_SelectedValueChanged
                cmbCategory.DataSource = dsIM.Tables(7)
                cmbCategory.ValueMember = "nCategoryID"
                cmbCategory.DisplayMember = "sDescription"
                cmbCategory.SelectedIndex = -1
                AddHandler cmbCategory.SelectedValueChanged, AddressOf cmbCategory_SelectedValueChanged

                'Get Unit Of Measure Codes and fill in combobox
                cmbUnitOfMeasure.DataSource = dsIM.Tables(8)
                cmbUnitOfMeasure.ValueMember = "Code"
                cmbUnitOfMeasure.DisplayMember = "Unit"
                cmbUnitOfMeasure.SelectedIndex = -1


                'Get Publicity Code and fill in combobox
                cmbPublicityCode.DataSource = dsIM.Tables(9)
                cmbPublicityCode.ValueMember = "Code"
                cmbPublicityCode.DisplayMember = "Unit"
                cmbPublicityCode.SelectedIndex = -1

                'Get Immunization Funding List
                cmbImmunizationFunding.DataSource = dsIM.Tables(10)
                cmbImmunizationFunding.ValueMember = "ImmunizationFunding"
                cmbImmunizationFunding.DisplayMember = "ImmunizationFunding"
                cmbImmunizationFunding.SelectedIndex = -1

            End If
            'Get SKU List
            FillSKUList()

            Dim dtstatus As DataTable = Nothing
            oDB.Connect(False)
            oDB.Retrive("gsp_GetActStatus", dtstatus)

            cmbStatus.DataSource = dtstatus
            cmbStatus.ValueMember = "ActStatus"
            cmbStatus.DisplayMember = "EMRDisplayName"
            cmbStatus.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default

            'If Not IsNothing(dsIM) Then 'SLR: Don't dispsoe, since they are the datasources,,
            '    dsIM.Dispose()
            '    dsIM = Nothing
            'End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSKUList()
        Dim str As String
        Dim dtsku As DataTable

        'Get SKU List
        Try
            RemoveHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged

            str = cmbSKU.Text
            dtsku = GetSKUList()
            cmbSKU.DataSource = dtsku
            cmbSKU.ValueMember = "im_sSKU"
            cmbSKU.DisplayMember = "im_sSKU"
            cmbSKU.Text = str
            AddHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            str = Nothing
        End Try

    End Sub

    Private Sub FillVaccineLot()
        Dim dtsku As DataTable = Nothing
        Dim strSQL As String

        Try
            RemoveHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged

            strSQL = " SELECT Distinct im_LotNumber " & _
                     " FROM IM_MST" & _
                     " where im_sActive = 'Active' and im_sVaccine = '" & Replace(txtCvx.Text, "'", "''") & "' and im_nLocationID = " & cmbLocation.SelectedValue

            If Trim(cmbSKU.Text) <> "" Then
                strSQL = strSQL + " and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"

                strSQL = strSQL + " union select distinct im_trn_Lotnumber as im_LotNumber " & _
              " from im_trn_dtl " & _
              " where im_trn_Lotnumber not in " & _
              " (select distinct im_LotNumber from IM_MST) " & _
              " and sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'" & _
              " and nLocationID = " & cmbLocation.SelectedValue & _
              " order by im_LotNumber"
            End If

            dtsku = GetList(strSQL)
            cmbLotNumber.DataSource = dtsku
            cmbLotNumber.ValueMember = "im_LotNumber"
            cmbLotNumber.DisplayMember = "im_LotNumber"
            cmbLotNumber.SelectedIndex = -1
            AddHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally

            'If Not IsNothing(dtsku) Then
            '    dtsku.Dispose()
            '    dtsku = Nothing
            'End If

            strSQL = Nothing

        End Try

    End Sub
    Private Sub Fill_VaccineInfomrationfromRecommendation()
        Try
            Dim dtIM As DataTable = Nothing
            dtIM = getVaccineInformationfromRecommendation(_lst.ID)

            If dtIM IsNot Nothing AndAlso dtIM.Rows.Count > 0 Then

                RemoveHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
                RemoveHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
                RemoveHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged

                If Convert.ToInt64(dtIM.Rows(0)("LocationID")) > 0 Then
                    cmbLocation.SelectedValue = dtIM.Rows(0)("LocationID")
                End If

                RemoveHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged
                '  Dim dtsku As New DataTable
                cmbSKU.SelectedIndex = -1
                cmbSKU.SelectedIndex = cmbSKU.FindStringExact(dtIM.Rows(0)("SKU"))

                AddHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged


                txtCvx.Text = dtIM.Rows(0)("VaccineName")

                If Convert.ToInt64(dtIM.Rows(0)("CategoryID")) > 0 Then
                    cmbCategory.SelectedValue = dtIM.Rows(0)("CategoryID")
                End If

                txt_TradeName.Text = dtIM.Rows(0)("sTradeName")
                txtMvx.Text = dtIM.Rows(0)("Manufacture")

                cmbIcd.Items.Clear()
                cmbLotNumber.SelectedIndex = -1
                If cmbLotNumber.FindStringExact(dtIM.Rows(0)("LotNumber")) = -1 Then
                    cmbLotNumber.Text = dtIM.Rows(0)("LotNumber")
                Else
                    cmbLotNumber.SelectedIndex = cmbLotNumber.FindStringExact(dtIM.Rows(0)("LotNumber"))
                End If

                Dim arrICD() As String
                If dtIM.Rows(0)("DiagnosisCode") <> "" Then
                    arrICD = dtIM.Rows(0)("DiagnosisCode").Split(",")
                    For i As Integer = 0 To arrICD.GetUpperBound(0)
                        cmbIcd.Items.Add(arrICD(i))
                    Next
                End If
                If cmbIcd.Items.Count > 0 Then
                    cmbIcd.SelectedIndex = 0
                End If

                cmbCpt.Items.Clear()
                Dim arrCPT() As String
                If dtIM.Rows(0)("CPTCode") <> "" Then
                    arrCPT = dtIM.Rows(0)("CPTCode").ToString.Split(",")
                    For i As Integer = 0 To arrCPT.GetUpperBound(0)
                        cmbCpt.Items.Add(arrCPT(i))
                    Next
                End If
                If cmbCpt.Items.Count > 0 Then
                    cmbCpt.SelectedIndex = 0
                End If

                txtNDCcode.Text = dtIM.Rows(0)("NDCCode")

                If IsDate(dtIM.Rows(0)("ExpirationDate")) = True Then
                    dtexpDate.Checked = True
                    dtexpDate.Value = CType(dtIM.Rows(0)("ExpirationDate"), Date)
                End If

                If dtIM.Rows(0)("ICDRevision") <> 0 Then
                    ICDType = CType(dtIM.Rows(0)("ICDRevision"), Integer)
                End If


                AddHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
                AddHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
                AddHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged
                cmbSKU.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetExpiryDate()
        Dim dtdata As DataTable = Nothing
        Dim strSQL As String
        Try
            Me.Cursor = Cursors.WaitCursor

            If Trim(cmbLotNumber.Text) = "" Then
                dtexpDate.Value = Today.Date
                dtexpDate.Checked = False
                Exit Sub
            End If

            strSQL = " SELECT top 1 im_dtExpiration from IM_MST where im_sActive = 'Active' and im_nLocationID = " & cmbLocation.SelectedValue

            If Trim(txtCvx.Text) <> "" Then
                strSQL = strSQL + " and im_sVaccine = '" & Replace(txtCvx.Text, "'", "''") & "'"
            End If

            If Trim(cmbSKU.Text) <> "" Then
                strSQL = strSQL + " and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"
            End If

            If Trim(cmbLotNumber.Text) <> "" Then
                strSQL = strSQL + " and im_LotNumber = '" & Replace(cmbLotNumber.Text, "'", "''") & "'"
            End If

            dtdata = GetList(strSQL)
            If (IsNothing(dtdata) = False) Then

                If dtdata.Rows.Count > 0 Then
                    If cmbLotNumber.Text <> "" Then
                        If Not IsDBNull(dtdata.Rows(0)("im_dtExpiration")) Then
                            dtexpDate.Checked = True
                            dtexpDate.Value = dtdata.Rows(0)("im_dtExpiration")
                        Else
                            dtexpDate.Value = Today.Date
                            dtexpDate.Checked = False
                        End If

                    End If
                Else
                    dtexpDate.Value = Today.Date
                    dtexpDate.Checked = False
                End If
                dtdata.Dispose()
                dtdata = Nothing
            Else
                dtexpDate.Value = Today.Date
                dtexpDate.Checked = False
            End If
            'Set Category
            strSQL = " SELECT top 1 im_nCategoryID as CategoryID from IM_MST where im_sActive = 'Active' and im_nLocationID = " & cmbLocation.SelectedValue

            If Trim(txtCvx.Text) <> "" Then
                strSQL = strSQL + " and im_sVaccine = '" & Replace(txtCvx.Text, "'", "''") & "'"
            End If

            If Trim(cmbSKU.Text) <> "" Then
                strSQL = strSQL + " and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"
            End If

            If Trim(cmbLotNumber.Text) <> "" Then
                strSQL = strSQL + " and im_LotNumber = '" & Replace(cmbLotNumber.Text, "'", "''") & "'"
            End If

            dtdata = GetList(strSQL)
            If (IsNothing(dtdata) = False) Then


                If dtdata.Rows.Count > 0 Then
                    If Not IsDBNull(dtdata.Rows(0)("CategoryID")) Then
                        cmbCategory.SelectedValue = dtdata.Rows(0)("CategoryID")
                    End If
                End If
                dtdata.Dispose()
                dtdata = Nothing
            End If
            'Set Category end

            strSQL = " SELECT top 1 im_sFundingSource as Funding from IM_MST where im_sActive = 'Active' and im_nLocationID = " & cmbLocation.SelectedValue

            If Trim(txtCvx.Text) <> "" Then
                strSQL = strSQL + " and im_sVaccine = '" & Replace(txtCvx.Text, "'", "''") & "'"
            End If

            If Trim(cmbSKU.Text) <> "" Then
                strSQL = strSQL + " and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"
            End If

            If Trim(cmbLotNumber.Text) <> "" Then
                strSQL = strSQL + " and im_LotNumber = '" & Replace(cmbLotNumber.Text, "'", "''") & "'"
            End If

            dtdata = GetList(strSQL)
            If (IsNothing(dtdata) = False) Then
                If dtdata.Rows.Count > 0 Then
                    If Not IsDBNull(dtdata.Rows(0)("Funding")) Then
                        cmbFunding.Text = dtdata.Rows(0)("Funding")
                    End If
                End If
                dtdata.Dispose()
                dtdata = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default

            If Not IsNothing(dtdata) Then
                dtdata.Dispose()
                dtdata = Nothing
            End If

            strSQL = Nothing

        End Try
    End Sub
    Private Function getVaccineInformationfromRecommendation(ByVal nImmunizationMasterID As Int64) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nImmunizationMasterID", nImmunizationMasterID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_getIMinfofromRecommendation", oParam, dtIM)
            oDB.Disconnect()
            Return dtIM
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If oParam IsNot Nothing Then
                oParam.Dispose()
                oParam = Nothing
            End If
        End Try
    End Function

    Private Sub GetLocationProviderList(Optional ByVal _isReported As Boolean = False)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dsIM As DataSet = Nothing

        Dim locationID As Long = 0
        Dim ProviderID As Long = 0

        Try
            Me.Cursor = Cursors.WaitCursor

            locationID = cmbLocation.SelectedValue
            ProviderID = cmbProvider.SelectedValue

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@blnReported", _isReported, ParameterDirection.Input, SqlDbType.Bit)

            oDB.Retrive("IM_GetLocationProviderList", oParam, dsIM)
            oDB.Disconnect()

            If Not IsNothing(dsIM) Then

                'Get Location List
                RemoveHandler cmbLocation.SelectedIndexChanged, AddressOf cmbLocation_SelectedIndexChanged
                cmbLocation.DataSource = dsIM.Tables(0)
                cmbLocation.ValueMember = "nLocationID"
                cmbLocation.DisplayMember = "sLocation"

                If locationID = 0 Then
                    cmbLocation.SelectedValue = GetIMLocation()
                Else
                    cmbLocation.SelectedValue = locationID
                End If

                AddHandler cmbLocation.SelectedIndexChanged, AddressOf cmbLocation_SelectedIndexChanged

                'Get Provider List
                cmbProvider.DataSource = dsIM.Tables(1)
                cmbProvider.ValueMember = "nProviderID"
                cmbProvider.DisplayMember = "Provider"

                If ProviderID = 0 Then
                    cmbProvider.SelectedValue = gnPatientProviderID
                Else
                    cmbProvider.SelectedValue = ProviderID
                End If

                If String.IsNullOrEmpty(Convert.ToString(cmbProvider.SelectedValue)) Then
                    Dim sSQL As String = "SELECT nProviderID, sFirstName + ' ' + sMiddleName + ' ' + sLastName AS Provider FROM Provider_mst WHERE nProviderID = "

                    If ProviderID = 0 Then
                        sSQL = sSQL & gnPatientProviderID
                    Else
                        sSQL = sSQL & ProviderID
                    End If

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

                            If ProviderID = 0 Then
                                cmbProvider.SelectedValue = gnPatientProviderID
                            Else
                                cmbProvider.SelectedValue = ProviderID
                            End If

                            drRow = Nothing
                        End If

                        sSQL = Nothing
                        dtProviderDetails.Dispose()
                        dtProviderDetails = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not IsNothing(dsIM) Then 'SLR: Don't dispose, since they are datasources..
            '    dsIM.Dispose()
            '    dsIM = Nothing
            'End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(oParam) Then
                oParam.Dispose()
                oParam = Nothing
            End If

            locationID = Nothing
            ProviderID = Nothing

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SetVISDocumentDetails()
        Dim dtIM As DataTable
        dtIM = GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())
        If (IsNothing(dtIM) = False) Then
            If dtIM.Rows.Count > 0 Then

                If Not IsDBNull(dtIM.Rows(0)("DocumentID")) Then
                    txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(dtIM.Rows(0)("DocumentID"))
                End If

                If Not IsDBNull(dtIM.Rows(0)("PublicationDate")) Then
                    dtpublication_date.Checked = True
                    dtpublication_date.Value = dtIM.Rows(0)("PublicationDate")
                Else
                    dtpublication_date.Value = Today.Date
                    dtpublication_date.Checked = False
                End If
            Else
                txt_vis.Text = ""
                dtpublication_date.Value = Today.Date
                dtpublication_date.Checked = False
                _DocumentID = 0
                Exit Sub
            End If
            dtIM.Dispose()
            dtIM = Nothing
        End If
    End Sub

    Public Sub CopyVisDocument(ByVal nPatinetID As Int64, ByVal MasterDocumentID As Int64)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim ExamParam As SqlParameter = Nothing
        Dim Con As SqlConnection = New SqlConnection(GetDMSConnectionString())
        Try
            cmd = New SqlCommand("gsp_CopyVisDocument", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Con.Open()

            ExamParam = cmd.Parameters.Add("@NewDocID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.InputOutput
            ExamParam.Value = 0

            'Visit ID
            sqlParam = cmd.Parameters.Add("@DocumentID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = MasterDocumentID

            'patient Id
            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatinetID

            cmd.ExecuteNonQuery()
            _DocumentID = ExamParam.Value
            _AssociatedDocumentID = MasterDocumentID
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(sqlParam) Then 'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(ExamParam) Then
                ExamParam = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    Public Sub ViewScanDoucment()
        Try
            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _SelectedDocumentId As Int64 = 0
            Dim _result As Boolean = False
            'If Not IsNothing(oViewDocument) Then
            '    oViewDocument = Nothing
            'End If

            If (_DocumentID > 0) Then
                'If IsNothing(oViewDocument) Then
                '    oViewDocument = New gloEDocumentV3.gloEDocV3Management()
                'End If

                _result = Get_ViewDocumentEvent(_ScanContainerID, _ScanDocumentID, _SelectedDocumentId)
                _DocumentID = _ScanDocumentID

                txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If txt_vis.Text.Trim() = "" Then
                    _DocumentID = 0
                End If
                'If Not IsNothing(oViewDocument) Then
                '    oViewDocument = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'If Not IsNothing(oViewDocument) Then
            '    oViewDocument.Dispose()
            'End If
        Finally
            'If Not IsNothing(oViewDocument) Then
            '    oViewDocument.Dispose()
            'End If
        End Try
    End Sub

    Public Sub ScanViewDoucment()
        Try
            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _SelectedDocumentID As Int64 = 0
            Dim _result As Boolean = False

            Dim sDMSScanCategory As String

            Dim _ScanDocFlag As Boolean = True
            sDMSScanCategory = gDMSCategory_VIS

            If _ScanDocFlag = True Then
                If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, sDMSScanCategory, gClinicID) = False Then
                    MessageBox.Show("DMS Category for VIS has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _ScanDocFlag = False
                End If
            End If

            If _ScanDocFlag = True Then
                Dim arrDocumentInfo As New ArrayList
                Dim strDocumentInfo As String = ""
                _result = Set_ScanDocumentEvent(sDMSScanCategory, _ScanContainerID, _ScanDocumentID, _SelectedDocumentID)
                _DocumentID = _ScanDocumentID

                txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If (txt_vis.Text.Trim() = "") Then
                    _DocumentID = 0
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
        End Try
    End Sub

    Private Sub GetCVXMVX()
        ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim ds As DataSet = Nothing
        Try
            If txt_TradeName.Text.Trim() <> "" Then
                ds = oClsIM.GetCVXMvxFromTradeName(txt_TradeName.Text.Trim())
                If IsNothing(ds) = False Then
                    If IsNothing(ds) = False Then
                        If ds.Tables("Vaccine").Rows.Count > 0 Then
                            _isLoadGridCvxControl = True
                            txtCvx.Text = Convert.ToString(ds.Tables("Vaccine").Rows(0)("Vaccine"))

                            _isLoadGridCvxControl = False
                        End If
                        If ds.Tables("Manufacturer").Rows.Count > 0 Then
                            _isLoadGridCvxControl = True

                            txtMvx.Text = Convert.ToString(ds.Tables("Manufacturer").Rows(0)("Manufacturer"))
                            _isLoadGridCvxControl = False
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If Not IsNothing(oClsIM) Then
                oClsIM = Nothing
            End If

            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub

    Private Sub GetCPTCodeFromCVX()
        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim dt As DataTable = Nothing

        Try
            ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
            If Not IsNothing(cmbCpt.DataSource) Then
                ' cmbCpt.Items.Clear()
                cmbCpt.DataSource = Nothing
            End If
            cmbCpt.Items.Clear()

            If txtCvx.Text.Trim() <> "" Then
                dt = oClsIM.GetCPTFromCVXCode(txtCvx.Text.Trim())
                If IsNothing(dt) = False Then

                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            cmbCpt.Items.Add(dt.Rows(i)("cptcode").ToString().Trim)
                        Next
                    End If
                    If cmbCpt.Items.Count > 0 Then
                        cmbCpt.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If Not IsNothing(oClsIM) Then
                oClsIM = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Private Sub CloseProcedureControl(ByVal _controlName As String)
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            If _controlName = "Cvx" And blnCVXSelector = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlCvxControl.Controls.Count - 1 To 0 Step -1
                    pnlCvxControl.Controls.RemoveAt(i)
                Next
                If oCVXControl IsNot Nothing Then

                    RemoveHandler oCVXControl.ItemSelected, AddressOf oCvxControl_ItemSelected
                    RemoveHandler oCVXControl.InternalGridKeyDown, AddressOf oCvxControl_InternalGridKeyDown
                    RemoveHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
                    RemoveHandler oCVXControl.C1GridList.MouseMove, AddressOf oCvxControl_MouseMove

                    oCVXControl.Dispose()
                    oCVXControl = Nothing
                End If
                pnlCvxControl.Visible = False
                RaiseEvent GridListClosed()
                pnlCvxControl.SendToBack()
                '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            ElseIf _controlName = "Mvx" And blnMVXSelector = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlMvxControl.Controls.Count - 1 To 0 Step -1
                    pnlMvxControl.Controls.RemoveAt(i)
                Next
                If oMVXControl IsNot Nothing Then

                    RemoveHandler oMVXControl.ItemSelected, AddressOf oMvxControl_ItemSelected
                    RemoveHandler oMVXControl.InternalGridKeyDown, AddressOf oMvxControl_InternalGridKeyDown
                    RemoveHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
                    RemoveHandler oMVXControl.C1GridList.MouseMove, AddressOf oMVXControl_MouseMove

                    oMVXControl.Dispose()
                    oMVXControl = Nothing
                End If
                pnlMvxControl.Visible = False
                RaiseEvent GridListClosed1()
                pnlMvxControl.SendToBack()
                '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            ElseIf _controlName = "TradeName" And blnTradeNameSelector = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlTradeNameControl.Controls.Count - 1 To 0 Step -1
                    pnlTradeNameControl.Controls.RemoveAt(i)
                Next
                If oTradeNameControl IsNot Nothing Then

                    RemoveHandler oTradeNameControl.ItemSelected, AddressOf oTradeNameControl_ItemSelected
                    RemoveHandler oTradeNameControl.InternalGridKeyDown, AddressOf oTradeNameControl_InternalGridKeyDown
                    RemoveHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
                    RemoveHandler oTradeNameControl.C1GridList.MouseMove, AddressOf oTradeNameControl_MouseMove

                    oTradeNameControl.Dispose()
                    oTradeNameControl = Nothing
                End If
                pnlTradeNameControl.Visible = False
                RaiseEvent GridListClosed2()
                pnlTradeNameControl.SendToBack()
            End If


        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub OpenProcedureControl(ByVal ControlType As gloUserControlLibrary.gloGridListControlType, ByVal ControlHeader As String, ByVal SearchText As String)

        Try
            If ControlType = gloUserControlLibrary.gloGridListControlType.Cvx Then
                If oCVXControl IsNot Nothing Then
                    CloseProcedureControl("Cvx")
                End If
                oCVXControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.Cvx, True, 100)
                oCVXControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oCVXControl.ItemSelected, AddressOf oCvxControl_ItemSelected
                AddHandler oCVXControl.InternalGridKeyDown, AddressOf oCvxControl_InternalGridKeyDown
                AddHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus

                AddHandler oCVXControl.C1GridList.MouseMove, AddressOf oCvxControl_MouseMove

                AddHandler oCVXControl.C1GridList.KeyPress, AddressOf oCvxControl_KeyPress

                oCVXControl.ImmunizationName = txtCvx.Text.Trim()
                oCVXControl.ControlHeader = ControlHeader

                oCVXControl.ShowHeader = False

                pnlCvxControl.Controls.Add(oCVXControl)
                oCVXControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oCVXControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oCVXControl.Show()
                pnlCvxControl.Visible = True
                RaiseEvent GridListLoaded()
                pnlCvxControl.BringToFront()
            ElseIf ControlType = gloUserControlLibrary.gloGridListControlType.Mvx Then
                If oMVXControl IsNot Nothing Then
                    CloseProcedureControl("Mvx")
                End If
                oMVXControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.Mvx, True, 100)
                oMVXControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oMVXControl.ItemSelected, AddressOf oMvxControl_ItemSelected
                AddHandler oMVXControl.InternalGridKeyDown, AddressOf oMvxControl_InternalGridKeyDown
                AddHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus

                AddHandler oMVXControl.C1GridList.MouseMove, AddressOf oMVXControl_MouseMove

                oMVXControl.ImmunizationName = txtMvx.Text.Trim()
                oMVXControl.ControlHeader = ControlHeader

                oMVXControl.ShowHeader = False

                pnlMvxControl.Controls.Add(oMVXControl)
                oMVXControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oMVXControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oMVXControl.Show()
                pnlMvxControl.Visible = True
                RaiseEvent GridListLoaded1()
                pnlMvxControl.BringToFront()
            ElseIf ControlType = gloUserControlLibrary.gloGridListControlType.TradeName Then
                If oTradeNameControl IsNot Nothing Then
                    CloseProcedureControl("TradeName")
                End If
                oTradeNameControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.TradeName, True, 100)
                oTradeNameControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oTradeNameControl.ItemSelected, AddressOf oTradeNameControl_ItemSelected
                AddHandler oTradeNameControl.InternalGridKeyDown, AddressOf oTradeNameControl_InternalGridKeyDown
                AddHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus

                AddHandler oTradeNameControl.C1GridList.MouseMove, AddressOf oTradeNameControl_MouseMove

                oTradeNameControl.ImmunizationName = txt_TradeName.Text.Trim()
                oTradeNameControl.ControlHeader = ControlHeader

                oTradeNameControl.ShowHeader = False

                pnlTradeNameControl.Controls.Add(oTradeNameControl)
                oTradeNameControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oTradeNameControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oTradeNameControl.Show()
                pnlTradeNameControl.Visible = True
                RaiseEvent GridListLoaded2()
                pnlTradeNameControl.BringToFront()
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Public Sub SetReaction()
        lstReaction.Items.Clear()
        Dim _sReaction As String = objfrmHistory.Reaction
        If _sReaction <> "" Then
            Dim Im_Reaction As String() = _sReaction.Split(vbNewLine)
            If Not IsNothing(Im_Reaction) Then
                If Im_Reaction.Count > 0 Then
                    For Each i As String In Im_Reaction
                        If i.Trim() <> "" Then
                            lstReaction.Items.Add(i.Trim())
                        End If
                    Next
                    If lstReaction.Items.Count > 0 Then
                        ' cmbreacdt.Checked = True
                    Else
                        ' cmbreacdt.Checked = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ShowRequiredLable()


        If optRefused.Checked = True Then

            'Refused - Mandatory Fields
            '1. Trade Name
            '2. Location
            '3. Provider Name
            '4. Refused By
            '5. Refusal Reason

            lblTradeName.Visible = True
            lblLocation.Visible = True
            lblProviderName.Visible = True
            lblDosesGiven.Visible = False

            lblLotNumber.Visible = False

            lblRefusalreason.Visible = True
            lblRefusedBy.Visible = True


            lblcqm.Visible = True

            chkPatientHasReaction.Enabled = False
            lblNote.Visible = True

            txt_dosage_given.Text = "0"
            RefusalEnableDisable(optRefused.Checked)
            grpReaction.Enabled = False

            lblFunding.Visible = False

        ElseIf optReported.Checked = True Or optPartiallyAdministered.Checked = True Or optNotAdministered.Checked = True Then

            'Reported - Mandatory Fields
            '1. Vaccine
            '2. Lot Number


            lblTradeName.Visible = False
            lblLocation.Visible = False
            lblProviderName.Visible = False
            lblDosesGiven.Visible = False

            lblLotNumber.Visible = False

            lblRefusalreason.Visible = False
            lblRefusedBy.Visible = False

            lblcqm.Visible = True

            chkPatientHasReaction.Enabled = True
            lblNote.Visible = False

            grpReaction.Enabled = True
            RefusalEnableDisable(False)

            lblFunding.Visible = False

        Else
            'If Funding field mandatory
            If GetRequirefunding() = "1" Then
                lblFunding.Visible = True
            Else
                lblFunding.Visible = False
            End If

            'Administered - Mandatory Fields
            '1. Trade Name
            '2. Location
            '3. Provider Name
            '4. Doseg Given 


            lblTradeName.Visible = True
            lblLocation.Visible = True
            lblProviderName.Visible = True
            lblDosesGiven.Visible = True

            lblLotNumber.Visible = True

            lblRefusalreason.Visible = False
            lblRefusedBy.Visible = False

            lblcqm.Visible = True

            chkPatientHasReaction.Enabled = True
            lblNote.Visible = False

            If txt_dosage_given.Text = "0" Then
                txt_dosage_given.Text = 1
            End If

            RefusalEnableDisable(optRefused.Checked)
        End If
    End Sub

    Private Sub RefusalEnableDisable(ByVal blnFlag As Boolean)
        If blnFlag = True Then
            cmbRefusalreason.Enabled = True
            txt_refused_by.Enabled = True
            txt_refusal_comments.Enabled = True

            txt_dosage_given.Enabled = False
            cmbRoute.Enabled = False
            txt_amount_given.Enabled = False
            'txt_units.Enabled = False
            cmbUnitOfMeasure.Enabled = False
            cmbSite.Enabled = False
            txtRefusalReason.Enabled = True
            btnBrwRefusalReason.Enabled = True
            btnClearRefusalReason.Enabled = True

            lblcqm.Enabled = True
            txtcqm.Enabled = True
            btnbrwcqm.Enabled = True
            btnclrcqm.Enabled = True


        Else
            cmbRefusalreason.Enabled = False
            txt_refused_by.Enabled = False
            txt_refusal_comments.Enabled = False

            txt_dosage_given.Enabled = True
            cmbRoute.Enabled = True
            txt_amount_given.Enabled = True
            cmbUnitOfMeasure.Enabled = True
            txt_units.Enabled = True
            cmbSite.Enabled = True
            txtRefusalReason.Enabled = False
            btnBrwRefusalReason.Enabled = False
            btnClearRefusalReason.Enabled = False

            lblcqm.Enabled = True
            txtcqm.Enabled = False
            btnbrwcqm.Enabled = False
            btnclrcqm.Enabled = False
        End If
    End Sub

    Private Sub ShowSelectedImmunization()

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing
        Dim sTransactionDateTime As String()
        Try
            Me.Cursor = Cursors.WaitCursor

            cmbSKU.Enabled = False
            btnSearchsku.Enabled = False

            txtCvx.Enabled = False
            btnCvx.Enabled = False
            BtnAddVaccineCategory.Enabled = False

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@TransactioID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("IM_ShowSelectedImmunization", oParam, dtIM)
            oDB.Disconnect()
            If (IsNothing(dtIM) = False) Then


                If dtIM.Rows.Count > 0 Then

                    If dtIM.Rows(0)("TransactionDate") IsNot Nothing Then
                        sTransactionDateTime = dtIM.Rows(0)("TransactionDate").ToString().Split(New Char() {" "c})
                        If sTransactionDateTime(1).ToString() = "12:00:00" Then
                            dttransaction_date.Value = dtIM.Rows(0)("TransactionDate")
                            dtTransactionTime.Checked = False
                            'dtTransactionTime.Enabled = False
                        Else
                            'dtTransactionTime.Enabled = True
                            dtTransactionTime.Checked = True
                            dttransaction_date.Value = dtIM.Rows(0)("TransactionDate")
                            dtTransactionTime.Value = dtIM.Rows(0)("TransactionDate")
                        End If
                    End If
                    If dtIM.Rows(0)("dtPublicityCodeLastUpdated") IsNot Nothing AndAlso Not dtIM.Rows(0).IsNull("dtPublicityCodeLastUpdated") Then
                        publicityeffetiveDTP.Value = dtIM.Rows(0)("dtPublicityCodeLastUpdated")
                    End If



                    If Not IsDBNull(dtIM.Rows(0)("Administred")) Then
                        If dtIM.Rows(0)("Administred") = "0" Then
                            optAdministered.Checked = True
                        ElseIf dtIM.Rows(0)("Administred") = "1" Then
                            optReported.Checked = True
                        ElseIf dtIM.Rows(0)("Administred") = "2" Then
                            optRefused.Checked = True
                        ElseIf dtIM.Rows(0)("Administred") = "3" Then
                            optNotAdministered.Checked = True
                        ElseIf dtIM.Rows(0)("Administred") = "4" Then
                            optPartiallyAdministered.Checked = True
                        End If
                    End If

                    cmbAdministred.SelectedValue = dtIM.Rows(0)("nAdministerID")

                    If String.IsNullOrEmpty(Convert.ToString(cmbAdministred.SelectedValue)) Then
                        Dim sSQL As String = "SELECT nUserID, sLoginName FROM User_MST WHERE nUserID = " & dtIM.Rows(0)("nAdministerID")

                        oDB.Connect(False)

                        Dim dtLoginDetails As DataTable = Nothing

                        oDB.Retrive_Query(sSQL, dtLoginDetails)

                        oDB.Disconnect()
                        If (IsNothing(dtLoginDetails) = False) Then


                            If dtLoginDetails.Rows.Count > 0 Then
                                Dim dtUsers As DataTable = cmbAdministred.DataSource
                                Dim drRow As DataRow = dtUsers.NewRow()

                                drRow("nUserID") = dtLoginDetails.Rows(0)("nUserID")
                                drRow("sLoginName") = dtLoginDetails.Rows(0)("sLoginName")

                                dtUsers.Rows.Add(drRow)

                                cmbAdministred.DataSource = dtUsers
                                cmbAdministred.ValueMember = "nUserID"
                                cmbAdministred.DisplayMember = "sLoginName"
                                cmbAdministred.SelectedValue = dtIM.Rows(0)("nAdministerID")

                                drRow = Nothing
                            End If
                            dtLoginDetails.Dispose()
                            dtLoginDetails = Nothing
                        End If
                        sSQL = Nothing
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("nLocationID")) Then
                        cmbLocation.SelectedValue = dtIM.Rows(0)("nLocationID")
                    End If
                    ICDType = dtIM.Rows(0)("nICDRevision") ''added for ICd10 implementation

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

                    If Not IsDBNull(dtIM.Rows(0)("sku")) Then
                        RemoveHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged
                        cmbSKU.Text = dtIM.Rows(0)("sku")
                        AddHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged
                    End If

                    RemoveHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
                    RemoveHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
                    RemoveHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged


                    If Not IsDBNull(dtIM.Rows(0)("Vaccine")) Then
                        txtCvx.Text = dtIM.Rows(0)("Vaccine")
                    End If

                    If optAdministered.Checked = True Then
                        If Not IsDBNull(dtIM.Rows(0)("TradeName")) Then
                            If Len(Trim(dtIM.Rows(0)("TradeName"))) > 0 Then
                                txt_TradeName.Enabled = False
                                btnTradeName.Enabled = False
                                BtnAddTradeNameCategory.Enabled = False
                            End If
                        End If
                    Else
                        txt_TradeName.Enabled = False
                        btnTradeName.Enabled = False
                        BtnAddTradeNameCategory.Enabled = False
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("TradeName")) Then
                        txt_TradeName.Text = dtIM.Rows(0)("TradeName")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("Manufacturer")) Then
                        txtMvx.Text = dtIM.Rows(0)("Manufacturer")
                    End If

                    AddHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
                    AddHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
                    AddHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged


                    RemoveHandler cmbCategory.SelectedValueChanged, AddressOf cmbCategory_SelectedValueChanged
                    If Not IsDBNull(dtIM.Rows(0)("im_nCategoryID")) Then
                        cmbCategory.SelectedValue = dtIM.Rows(0)("im_nCategoryID")
                    End If
                    AddHandler cmbCategory.SelectedValueChanged, AddressOf cmbCategory_SelectedValueChanged


                    RemoveHandler cmbLotNumber.TextChanged, AddressOf cmbLotNumber_TextChanged
                    If Not IsDBNull(dtIM.Rows(0)("LotNumber")) Then
                        cmbLotNumber.Text = dtIM.Rows(0)("LotNumber")
                    End If
                    AddHandler cmbLotNumber.TextChanged, AddressOf cmbLotNumber_TextChanged


                    If Not IsDBNull(dtIM.Rows(0)("ExpiryDate")) Then
                        dtexpDate.Checked = True
                        dtexpDate.Value = dtIM.Rows(0)("ExpiryDate")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("nDosageGiven")) Then
                        txt_dosage_given.Text = dtIM.Rows(0)("nDosageGiven")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("nAmountGiven")) Then
                        txt_amount_given.Text = dtIM.Rows(0)("nAmountGiven")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("Unit")) Then

                        txt_units.Text = dtIM.Rows(0)("Unit")

                        cmbUnitOfMeasure.SelectedValue = Convert.ToString(dtIM.Rows(0)("Unit")).Trim()
                        If IsNothing(cmbUnitOfMeasure.SelectedValue) Then
                            cmbUnitOfMeasure.Text = Convert.ToString(dtIM.Rows(0)("Unit")).Trim()
                        End If


                    End If

                    If Not IsDBNull(dtIM.Rows(0)("Route")) Then
                        cmbRoute.Text = dtIM.Rows(0)("Route")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("Site")) Then
                        cmbSite.Text = dtIM.Rows(0)("Site")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("bVISGiven")) Then
                        chk_vis_given.Checked = dtIM.Rows(0)("bVISGiven")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("sVIS")) Then
                        txt_vis.Text = dtIM.Rows(0)("sVIS")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("nDocumentID")) Then
                        _DocumentID = dtIM.Rows(0)("nDocumentID")
                    End If
                    If Not IsDBNull(dtIM.Rows(0)("nMasterDocumentID")) Then
                        _AssociatedDocumentID = dtIM.Rows(0)("nMasterDocumentID")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("PublicationDate")) Then
                        dtpublication_date.Checked = True
                        dtpublication_date.Value = dtIM.Rows(0)("PublicationDate")
                    Else
                        dtpublication_date.Value = Today.Date
                        dtpublication_date.Checked = False
                    End If

                    If (dtIM.Rows(0)("im_reasonfor_nonadmin") <> String.Empty) Then
                        txtRefusalReason.Text = dtIM.Rows(0)("sReasonConceptID") + " - " + dtIM.Rows(0)("im_reasonfor_nonadmin")
                        strRefusalCode = Convert.ToString(dtIM.Rows(0)("sReasonConceptID"))
                        strRefusalDescription = Convert.ToString(dtIM.Rows(0)("im_reasonfor_nonadmin"))
                        '' cmbRefusalreason.Text = dtIM.Rows(0)("im_reasonfor_nonadmin")
                    End If

                    ''cqm
                    If (dtIM.Rows(0)("sValuesetOID") <> String.Empty) Then
                        txtcqm.Text = dtIM.Rows(0)("sValueSetName")
                        strCQMCode = Convert.ToString(dtIM.Rows(0)("sValuesetOID"))
                        strCQMDescription = Convert.ToString(dtIM.Rows(0)("sValueSetName"))

                    End If



                    If Not IsDBNull(dtIM.Rows(0)("sRefusalComments")) Then
                        txt_refusal_comments.Text = dtIM.Rows(0)("sRefusalComments")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("sRefusedBy")) Then
                        txt_refused_by.Text = dtIM.Rows(0)("sRefusedBy")
                    End If







                    If Not IsDBNull(dtIM.Rows(0)("im_reminder")) Then
                        chkSetReminder.Checked = dtIM.Rows(0)("im_reminder")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("DueDate")) Then
                        dtDueDate.Checked = True
                        dtDueDate.Value = dtIM.Rows(0)("DueDate")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("NDCCode")) Then
                        ''--Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS  
                        If dtIM.Rows(0)("NDCCode") <> "0" Then
                            txtNDCcode.Text = dtIM.Rows(0)("NDCCode")
                        End If
                    End If


                    cmbIcd.Items.Clear()
                    Dim arrICD() As String
                    If Not IsDBNull(dtIM.Rows(0)("DiagnosisCode")) Then
                        If dtIM.Rows(0)("DiagnosisCode") <> "" Then
                            arrICD = dtIM.Rows(0)("DiagnosisCode").split(",")
                            For i As Integer = 0 To arrICD.GetUpperBound(0)
                                cmbIcd.Items.Add(arrICD(i))
                            Next
                        End If
                    End If
                    If cmbIcd.Items.Count > 0 Then
                        cmbIcd.SelectedIndex = 0
                    End If


                    cmbCpt.Items.Clear()
                    Dim arrCPT() As String
                    If Not IsDBNull(dtIM.Rows(0)("cptCode")) Then
                        arrCPT = dtIM.Rows(0)("cptCode").split(",")
                        For i As Integer = 0 To arrCPT.GetUpperBound(0)
                            cmbCpt.Items.Add(arrCPT(i))
                        Next
                    End If
                    If cmbCpt.Items.Count > 0 Then
                        cmbCpt.SelectedIndex = 0
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("Funding")) Then
                        cmbFunding.Text = dtIM.Rows(0)("Funding")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("immunizationfunding")) Then
                        cmbImmunizationFunding.Text = dtIM.Rows(0)("immunizationfunding")
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("Comments")) Then
                        txt_notes.Text = dtIM.Rows(0)("Comments")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("bPatientHasAReaction")) Then
                        chkPatientHasReaction.Checked = dtIM.Rows(0)("bPatientHasAReaction")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("OnsetDate")) Then
                        dtOnsetDate.Value = dtIM.Rows(0)("OnsetDate")
                    End If


                    If Not IsDBNull(dtIM.Rows(0)("Administred")) Then
                        If dtIM.Rows(0)("Administred") = "2" Then
                            grpReaction.Enabled = False
                        Else
                            If chkPatientHasReaction.Checked = True Then
                                grpReaction.Enabled = True
                            Else
                                grpReaction.Enabled = False
                            End If
                        End If
                    End If


                    txtAdverseEvent.Text = dtIM.Rows(0)("sAdverseEvent")
                    chkPatientDied.Checked = dtIM.Rows(0)("bPatientDied")

                    If Not IsDBNull(dtIM.Rows(0)("PatientDieddate")) Then
                        dtPatientDied.Value = dtIM.Rows(0)("PatientDieddate")
                    End If

                    If dtIM.Rows(0)("bPatientDied") = True Then
                        dtPatientDied.Enabled = True
                    Else
                        dtPatientDied.Enabled = False
                    End If


                    chk_LifeThreateningIllness.Checked = dtIM.Rows(0)("bLifeThreateningIllness")
                    chk_RequiredEmergencyRoom.Checked = dtIM.Rows(0)("bRequiredEmergencyRoom")
                    chk_RequiredHospitalization.Checked = dtIM.Rows(0)("bRequiredHospitalization")

                    If chk_RequiredHospitalization.Checked = True Then
                        txtHospitalizationDays.Enabled = True
                    Else
                        txtHospitalizationDays.Enabled = False
                    End If

                    txtHospitalizationDays.Text = dtIM.Rows(0)("HospitalizationDays")

                    chk_ResultedInProlongation.Checked = dtIM.Rows(0)("bResultedInProlongation")
                    chk_ResultedInPermDisability.Checked = dtIM.Rows(0)("bResultedInPermDisability")
                    chk_NoneOfTheAbove.Checked = dtIM.Rows(0)("bNoneOfTheAbove")


                    If dtIM.Rows(0)("sPatientRecovered") = "Y" Then
                        rdo_PatientRecoveredYes.Checked = True
                    ElseIf dtIM.Rows(0)("sPatientRecovered") = "N" Then
                        rdo_PatientRecoveredNo.Checked = True
                    ElseIf dtIM.Rows(0)("sPatientRecovered") = "U" Then
                        rdo_PatientRecoveredUnknown.Checked = True
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("sReaction")) Then
                        Dim _SplReaction() As String

                        _SplReaction = Split(dtIM.Rows(0)("sReaction"), vbNewLine)

                        For i As Integer = 0 To _SplReaction.Count - 1
                            lstReaction.Items.Add(_SplReaction.GetValue(i).ToString().Trim())
                        Next
                    End If
                    If Not IsDBNull(dtIM.Rows(0)("immunizationStatus")) Then
                        cmbStatus.Text = dtIM.Rows(0)("immunizationStatus")
                    End If

                    'Start - Sagar Ghodke 26 July 2013: Implementation of ConceptID for Immunization
                    Dim _snomedInfo As ArrayList = Nothing
                    Dim _conceptId As String = ""
                    Dim _conceptDesc As String = ""

                    If Not IsDBNull(dtIM.Rows(0)("SnomedConceptID")) AndAlso Convert.ToString(dtIM.Rows(0)("SnomedConceptID")).Trim() <> "" Then
                        _conceptId = Convert.ToString(dtIM.Rows(0)("SnomedConceptID")).Trim()
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("SnomedConceptDesc")) AndAlso Convert.ToString(dtIM.Rows(0)("SnomedConceptDesc")).Trim() <> "" Then
                        _conceptDesc = Convert.ToString(dtIM.Rows(0)("SnomedConceptDesc")).Trim()
                    End If

                    If _conceptId.Trim() <> "" And _conceptDesc.Trim() <> "" Then
                        lblSnomedIdValue.Text = _conceptId + "-" + _conceptDesc
                    ElseIf _conceptId.Trim() <> "" And _conceptDesc.Trim() = "" Then
                        lblSnomedIdValue.Text = _conceptId
                    Else
                        lblSnomedIdValue.Text = ""
                    End If


                    _snomedInfo = New ArrayList()
                    _snomedInfo.Add(_conceptId)
                    _snomedInfo.Add(_conceptDesc)
                    lblSnomedIdValue.Tag = _snomedInfo


                    'Finish - Sagar Ghodke 26 July 2013: Implementation of ConceptID for Immunization




                    'Start - Sagar Ghodke 31 July 2013: Implementation of Publicity Code for Immunization
                    If Not IsDBNull(dtIM.Rows(0)("sPublicityCode")) AndAlso Convert.ToString(dtIM.Rows(0)("sPublicityCode")).Trim() <> "" Then
                        cmbPublicityCode.SelectedValue = Convert.ToString(dtIM.Rows(0)("sPublicityCode")).Trim()
                    End If
                    'sPublicityCodeDesc As sPublicityCodeDesc,
                    'dtPublicityCodeLastUpdated AS dtPublicityCodeLastUpdated
                    'Finish - Sagar Ghodke 31 July 2013: Implementation of Publicity Code for Immunization

                    If Not IsDBNull(dtIM.Rows(0)("nOrderingProviderType")) AndAlso Convert.ToString(dtIM.Rows(0)("nOrderingProviderType")).Trim() <> "" Then
                        OrdProviderType = DirectCast(dtIM.Rows(0)("nOrderingProviderType"), OrderingProviderType)
                    End If

                    If Not IsDBNull(dtIM.Rows(0)("nOderingProviderID")) AndAlso Convert.ToString(dtIM.Rows(0)("nOderingProviderID")).Trim() <> "" Then
                        lblOrdProvider.Tag = Convert.ToString(dtIM.Rows(0)("nOderingProviderID")).Trim()
                        lblOrdProvider.Text = Convert.ToString(dtIM.Rows(0)("sOrderingProvider")).Trim()
                    End If

                    'Is it downloaded History through HL7
                    If Not IsDBNull(dtIM.Rows(0)("IsHistory")) Then
                        _isHistory = dtIM.Rows(0)("IsHistory")
                    End If
                End If
            End If
            GetVaccinAvailableDoses()

            Get_AllUncertainFomulationCVX() 'Added by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub ShowTooltipOnWriteOffComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right, e.Bounds.Bottom + 25)
                    End If
                Else
                    tooltip.Hide(combo)
                End If
            Else
                tooltip.Hide(combo)
            End If
            e.DrawFocusRectangle()
        End If
    End Sub

#End Region

#Region "Function"

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer
        ''Code Review Changes: Dispose Graphics object
        Dim width As Integer = 0
        Dim g As Graphics = Me.CreateGraphics()
        If g IsNot Nothing Then
            Dim s As SizeF = g.MeasureString(_text, combo.Font)
            width = Convert.ToInt32(s.Width)
            'Dispose graphics object
            g.Dispose()
            g = Nothing
        End If
        Return width
    End Function

    Private Function GetSKUList() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim Locationid As Long
        Try
            Locationid = cmbLocation.SelectedValue
            strSQL = " select distinct im_sSKU from IM_MST where im_sActive = 'Active' and im_nLocationID = " & Locationid.ToString & " order by im_sSKU "
            dt = GetList(strSQL)
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dt)
            oDB.Disconnect()
            Return dt
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then: should not be disposed since it is returned
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            strSQL = Nothing
            Locationid = Nothing
        End Try
    End Function

    Private Function GetIMLocation() As Decimal
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim Locationid As Decimal = 0
        Try
            strSQL = " Select dbo.GetIMLocationID ( " & _PatientID & " ) "
            dt = GetList(strSQL)
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dt)
            oDB.Disconnect()
            If (IsNothing(dt) = False) Then
                If dt.Rows.Count > 0 Then
                    Locationid = dt.Rows(0)(0)
                End If
            End If

            Return Locationid
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            strSQL = Nothing
            Locationid = Nothing
        End Try
    End Function

    Public Function AddCategory(ByVal CategoryName As String, ByVal Caption As String) As Boolean
        Dim frm As New CategoryMaster(CategoryName)
        Try
            frm.Text = Caption
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            If frm.CategoryFromDialogResult = Windows.Forms.DialogResult.OK Then
                Return True
            ElseIf frm.CategoryFromDialogResult = Windows.Forms.DialogResult.Cancel Or frm.CategoryFromDialogResult = Windows.Forms.DialogResult.None Then
                Return False
            End If
            Return False
        Catch ex As Exception


            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
    End Function

    Public Function GetAlreadyExistDocumentID(ByVal nPatinetID As Int64, ByVal MasterDocumentID As Int64) As Int64
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim DocIDParameter As SqlParameter = Nothing
        Dim Con As SqlConnection = New SqlConnection(GetConnectionString())
        Try
            cmd = New SqlCommand("gsp_GetAlreadyExistDocumentID", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Con.Open()

            DocIDParameter = cmd.Parameters.Add("@DocID", SqlDbType.BigInt)
            DocIDParameter.Direction = ParameterDirection.InputOutput
            DocIDParameter.Value = 0
            'patient Id
            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatinetID

            sqlParam = cmd.Parameters.Add("@MasterDocumentID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = MasterDocumentID

            cmd.ExecuteNonQuery()
            Dim AssociatedVisDocumentID = DocIDParameter.Value
            Return AssociatedVisDocumentID
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            If Not IsNothing(sqlParam) Then 'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(DocIDParameter) Then
                DocIDParameter = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function GetAssociatedVisDocumentID(ByVal nPatinetID As Int64, ByVal Vaccine_Name As String, ByVal LotNumber As String, ByVal sTradeName As String) As DataTable

        Dim dtIM As DataTable = Nothing

        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters

            oParam.Add("@Vaccine_Name", Vaccine_Name, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@LotNumber", LotNumber, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@sTradeName", sTradeName, ParameterDirection.Input, SqlDbType.Text)

            oDB.Retrive("gsp_GetAssociatedVisDocumentID", oParam, dtIM)
            oDB.Disconnect()

            oParam.Dispose()
            oParam = Nothing

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            Return dtIM

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dtIM) Then
            '    dtIM.Dispose()
            '    dtIM = Nothing
            'End If
        End Try

    End Function

    Private Function Get_ViewDocumentEvent(ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64, ByRef selectedDocumentid As Int64) As Boolean
        Dim oViewDocument As gloEDocumentV3.gloEDocV3Management = New gloEDocumentV3.gloEDocV3Management()

        Dim _result As Boolean = False
        Try
            '_PatientID
            oViewDocument.oPatientExam = New clsPatientExams

            oViewDocument.oPatientMessages = New clsMessage
            oViewDocument.oPatientLetters = New clsPatientLetters
            oViewDocument.oNurseNotes = New clsNurseNotes
            oViewDocument.oHistory = New clsPatientHistory
            oViewDocument.oLabs = New clsLabs
            oViewDocument.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
            oViewDocument.oRxmed = New clsPatientDetails
            oViewDocument.oOrders = New clsPatientDetails
            oViewDocument.oProblemList = New clsPatientProblemList

            oViewDocument.oCriteria = New DocCriteria
            oViewDocument.oWord = New clsWordDocument

            _result = oViewDocument.ShowEDocument_Immunization(_PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization, _DocumentID, ScanContainerID, ScanDocumentID, selectedDocumentid, False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oViewDocument) = False Then
                If (IsNothing(oViewDocument.oPatientExam) = False) Then
                    DirectCast(oViewDocument.oPatientExam, clsPatientExams).Dispose()
                    oViewDocument.oPatientExam = Nothing
                End If
                If (IsNothing(oViewDocument.oPatientMessages) = False) Then
                    DirectCast(oViewDocument.oPatientMessages, clsMessage).Dispose()
                    oViewDocument.oPatientMessages = Nothing
                End If
                If (IsNothing(oViewDocument.oPatientLetters) = False) Then
                    DirectCast(oViewDocument.oPatientLetters, clsPatientLetters).Dispose()
                    oViewDocument.oPatientLetters = Nothing
                End If
                If (IsNothing(oViewDocument.oNurseNotes) = False) Then
                    DirectCast(oViewDocument.oNurseNotes, clsNurseNotes).Dispose()
                    oViewDocument.oNurseNotes = Nothing
                End If
                If (IsNothing(oViewDocument.oHistory) = False) Then
                    DirectCast(oViewDocument.oHistory, clsPatientHistory).Dispose()
                    oViewDocument.oHistory = Nothing
                End If
                If (IsNothing(oViewDocument.oLabs) = False) Then
                    DirectCast(oViewDocument.oLabs, clsLabs).Dispose()
                    oViewDocument.oLabs = Nothing
                End If
                If (IsNothing(oViewDocument.oDMS) = False) Then
                    DirectCast(oViewDocument.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                    oViewDocument.oDMS = Nothing
                End If
                If (IsNothing(oViewDocument.oRxmed) = False) Then
                    DirectCast(oViewDocument.oRxmed, clsPatientDetails).Dispose()
                    oViewDocument.oRxmed = Nothing
                End If
                If (IsNothing(oViewDocument.oOrders) = False) Then
                    DirectCast(oViewDocument.oOrders, clsPatientDetails).Dispose()
                    oViewDocument.oOrders = Nothing
                End If
                If (IsNothing(oViewDocument.oProblemList) = False) Then
                    DirectCast(oViewDocument.oProblemList, clsPatientProblemList).Dispose()
                    oViewDocument.oProblemList = Nothing
                End If

                If (IsNothing(oViewDocument.oCriteria) = False) Then
                    DirectCast(oViewDocument.oCriteria, DocCriteria).Dispose()
                    oViewDocument.oCriteria = Nothing
                End If

                oViewDocument.Dispose()
            End If
            oViewDocument = Nothing

        End Try
        Return _result
    End Function

    Private Function Set_ScanDocumentEvent(ByVal VISCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64, ByRef SelectedDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try
            oScanDocument.oPatientExam = New clsPatientExams

            oScanDocument.oPatientMessages = New clsMessage
            oScanDocument.oPatientLetters = New clsPatientLetters
            oScanDocument.oNurseNotes = New clsNurseNotes
            oScanDocument.oHistory = New clsPatientHistory
            oScanDocument.oLabs = New clsLabs
            oScanDocument.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
            oScanDocument.oRxmed = New clsPatientDetails
            oScanDocument.oOrders = New clsPatientDetails
            oScanDocument.oProblemList = New clsPatientProblemList

            oScanDocument.oCriteria = New DocCriteria
            oScanDocument.oWord = New clsWordDocument
            _result = oScanDocument.ShowEDocument_Immunization(_PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization, _DocumentID, ScanContainerID, ScanDocumentID, SelectedDocumentID, False, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oScanDocument) = False Then
                If (IsNothing(oScanDocument.oPatientExam) = False) Then
                    DirectCast(oScanDocument.oPatientExam, clsPatientExams).Dispose()
                    oScanDocument.oPatientExam = Nothing
                End If
                If (IsNothing(oScanDocument.oPatientMessages) = False) Then
                    DirectCast(oScanDocument.oPatientMessages, clsMessage).Dispose()
                    oScanDocument.oPatientMessages = Nothing
                End If
                If (IsNothing(oScanDocument.oPatientLetters) = False) Then
                    DirectCast(oScanDocument.oPatientLetters, clsPatientLetters).Dispose()
                    oScanDocument.oPatientLetters = Nothing
                End If
                If (IsNothing(oScanDocument.oNurseNotes) = False) Then
                    DirectCast(oScanDocument.oNurseNotes, clsNurseNotes).Dispose()
                    oScanDocument.oNurseNotes = Nothing
                End If
                If (IsNothing(oScanDocument.oHistory) = False) Then
                    DirectCast(oScanDocument.oHistory, clsPatientHistory).Dispose()
                    oScanDocument.oHistory = Nothing
                End If
                If (IsNothing(oScanDocument.oLabs) = False) Then
                    DirectCast(oScanDocument.oLabs, clsLabs).Dispose()
                    oScanDocument.oLabs = Nothing
                End If
                If (IsNothing(oScanDocument.oDMS) = False) Then
                    DirectCast(oScanDocument.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                    oScanDocument.oDMS = Nothing
                End If
                If (IsNothing(oScanDocument.oRxmed) = False) Then
                    DirectCast(oScanDocument.oRxmed, clsPatientDetails).Dispose()
                    oScanDocument.oRxmed = Nothing
                End If
                If (IsNothing(oScanDocument.oOrders) = False) Then
                    DirectCast(oScanDocument.oOrders, clsPatientDetails).Dispose()
                    oScanDocument.oOrders = Nothing
                End If
                If (IsNothing(oScanDocument.oProblemList) = False) Then
                    DirectCast(oScanDocument.oProblemList, clsPatientProblemList).Dispose()
                    oScanDocument.oProblemList = Nothing
                End If

                If (IsNothing(oScanDocument.oCriteria) = False) Then
                    DirectCast(oScanDocument.oCriteria, DocCriteria).Dispose()
                    oScanDocument.oCriteria = Nothing
                End If

                oScanDocument.Dispose()
            End If
            oScanDocument = Nothing


        End Try
        Return _result
    End Function

    Private Function CheckTradeNameisValidagainstSKU() As Boolean
        Dim oDM As New gloStream.Immunization.ItemSetup
        Dim _result As Boolean = True
        Try
            Dim _NDC As String = ""
            _NDC = cmbSKU.Text.Trim
            If _NDC <> "" Then
                If _NDC.Contains("-") Then
                    _NDC = _NDC.Replace("-", "")
                End If

                If _NDC.Length >= 10 Then
                    If _NDC.Length = 12 Or _NDC.Length = 11 Then
                        If _NDC.StartsWith("3") Then
                            _NDC = _NDC.Substring(1, Len(_NDC.Trim) - 1)
                        End If
                    ElseIf _NDC.Length = 14 Or _NDC.Length = 13 Then
                        If _NDC.StartsWith("003") Then
                            _NDC = _NDC.Substring(3, Len(_NDC.Trim) - 3)
                        End If
                    End If
                    Dim ds As DataSet
                    ds = oDM.getNDCCode1(_NDC)
                    If IsNothing(ds) = False Then
                        If IsNothing(ds.Tables("NDCInfo")) = False Then


                            If ds.Tables("NDCInfo").Rows.Count > 0 Then

                                If txt_TradeName.Text <> ds.Tables("NDCInfo").Rows(0)("CdcProductName").ToString().Trim Then

                                    If MessageBox.Show("Based on the CDC Vaccine Database,  the Trade Name " & "'" & txt_TradeName.Text.Trim & "'" & " does not match the SKU Given " & "'" & cmbSKU.Text.Trim & "'" & Chr(13) & Chr(13) & "Please verify that the SKU from the packaging has been entered correctly and choose one of the following:" & Chr(13) & Chr(13) & "YES – Save this vaccine entry matching " & "'" & txt_TradeName.Text.Trim & "'" & " to the SKU " & "'" & cmbSKU.Text.Trim & "'" & Chr(13) & Chr(13) & "NO - Continue to edit this vaccine", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                        _result = True
                                        Return _result
                                    Else
                                        _result = False
                                        Return _result
                                    End If
                                End If
                            End If
                        End If
                        ds.Dispose()
                        ds = Nothing
                    End If
                    ''
                    _NDC = cmbSKU.Text.Trim
                    If _NDC <> "" Then
                        If _NDC.Contains("-") Then
                            _NDC = _NDC.Replace("-", "")
                        End If
                        If _NDC.Length = 11 Then
                            If _NDC.StartsWith("3") Then
                                ds = oDM.getNDCCode1(_NDC)
                                If IsNothing(ds) = False Then
                                    If IsNothing(ds.Tables("NDCInfo")) = False Then

                                        If ds.Tables("NDCInfo").Rows.Count > 0 Then

                                            If txt_TradeName.Text <> ds.Tables("NDCInfo").Rows(0)("CdcProductName").ToString().Trim Then

                                                If MessageBox.Show("Based on the CDC Vaccine Database,  the Trade Name " & "'" & txt_TradeName.Text.Trim & "'" & " does not match the SKU Given " & "'" & cmbSKU.Text.Trim & "'" & Chr(13) & Chr(13) & "Please verify that the SKU from the packaging has been entered correctly and choose one of the following:" & Chr(13) & Chr(13) & "YES – Save this vaccine entry matching " & "'" & txt_TradeName.Text.Trim & "'" & " to the SKU " & "'" & cmbSKU.Text.Trim & "'" & Chr(13) & Chr(13) & "NO - Continue to edit this vaccine", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                                    _result = True

                                                Else
                                                    _result = False
                                                End If
                                            End If
                                        End If
                                    End If
                                    ds.Dispose()
                                    ds = Nothing
                                End If
                            End If
                        End If
                    End If
                    ''
                End If
            End If
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDM = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Selecting master record of particular SKU number
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSKUDetails() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dtsku As DataTable = Nothing
        Dim strSQL As String

        Try
            ''nICDRevision added for ICD10 implementation
            If Trim(cmbSKU.Text) = "" Then
                strSQL = " SELECT im_sSKU, isnull(im_sVaccine,'') as Vaccine, isnull(im_sManufacturer,'') as im_sManufacturer, isnull(im_sTradeName, '') as im_sTradeName, isnull(im_sNDCCode, 0) as im_sNDCCode, isnull(im_cpt_code,'') as im_cpt_code, isnull(im_Diagnosis_Code,0) as im_Diagnosis_Code, im_nCategoryID,ISNULL(nICDRevision,9) as nICDRevision  " & _
                         " FROM IM_MST where 1 =2"
            Else
                strSQL = " SELECT im_sSKU, isnull(im_sVaccine,'') as Vaccine, isnull(im_sManufacturer,'') as im_sManufacturer, isnull(im_sTradeName, '') as im_sTradeName, isnull(im_sNDCCode, 0) as im_sNDCCode, isnull(im_cpt_code,'') as im_cpt_code, isnull(im_Diagnosis_Code,0) as im_Diagnosis_Code, im_nCategoryID ,ISNULL(nICDRevision,9) as nICDRevision " & _
                         " FROM IM_MST" & _
                         " where im_sActive = 'Active' and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "' and im_nLocationID = " & cmbLocation.SelectedValue.ToString
            End If

            dtsku = GetList(strSQL)
            Return dtsku

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            'If Not IsNothing(dtsku) Then
            '    dtsku.Dispose()
            '    dtsku = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            strSQL = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Select list for all combo box
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetList(ByVal strSQL As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dtRoute As DataTable = Nothing
        Try
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dtRoute)
            oDB.Disconnect()
            Return dtRoute
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            'If Not IsNothing(dtRoute) Then
            '    dtRoute.Dispose()
            '    dtRoute = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Function Save_Immunisation() Handles Me.SaveFunction
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
    Private Shared underLineFont As Font = Nothing
    Private Sub GetVaccinAvailableDoses()

        ' If Not cmbCategory.SelectedValue = Nothing Then
        'Added by Amit to check Track Vaccine Inventory setting is ON or OFF
        Dim value As New Object()
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        ogloSettings.GetSetting("TrackVaccineInventory", 0, gnClinicID, value)
        ogloSettings.Dispose()
        ogloSettings = Nothing

        If value = "1" Then
            'checking individual Vaccines Inventory Track setting ON/OFF, if ON then showing
            'Adjust Inventory screen
            If GetVaccinInventoryTrack() > 0 Then

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParam As gloDatabaseLayer.DBParameters
                Dim dt As DataTable = Nothing

                oDB.Connect(False)
                oParam = New gloDatabaseLayer.DBParameters
                oParam.Add("@im_nCategoryID", cmbCategory.SelectedValue, ParameterDirection.Input, SqlDbType.Decimal)
                oParam.Add("@im_sVaccine", txtCvx.Text, ParameterDirection.Input, SqlDbType.NVarChar)
                oParam.Add("@im_sTradeName", txt_TradeName.Text, ParameterDirection.Input, SqlDbType.NVarChar)
                oParam.Add("@im_LotNumber", cmbLotNumber.Text, ParameterDirection.Input, SqlDbType.NVarChar)
                oParam.Add("@im_nLocationID", cmbLocation.SelectedValue, ParameterDirection.Input, SqlDbType.Decimal)

                oDB.Retrive("IM_GetVaccinAvailableDoses", oParam, dt)
                oDB.Disconnect()

                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        If (IsNothing(underLineFont)) Then
                            underLineFont = New Font(lblDosesOnHand.Font, FontStyle.Underline)
                        End If
                        If dt.Rows(0)(0) <= 0 Then
                            lblDosesOnHand.ForeColor = Color.Red
                            lblDosesOnHand.Font = underLineFont
                        Else
                            lblDosesOnHand.ForeColor = Color.Blue
                            lblDosesOnHand.Font = underLineFont
                        End If

                        lblDosesOnHand.Text = Format(dt.Rows(0)(0), "########0.##").ToString + " doses on hand"
                    Else
                        lblDosesOnHand.Text = ""
                    End If
                    dt.Dispose()
                    dt = Nothing
                Else
                    lblDosesOnHand.Text = ""
                End If

                oParam.Dispose()
                oParam = Nothing

                oDB.Dispose()
                oDB = Nothing
            Else
                lblDosesOnHand.Text = ""
            End If
        End If
        ' End If


    End Sub

    Private Function GetVaccinAvailableDosesOnHand() As Decimal

        'Added by Amit to check Track Vaccine Inventory setting is ON or OFF
        Dim value As New Object()
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        ogloSettings.GetSetting("TrackVaccineInventory", 0, gnClinicID, value)
        ogloSettings.Dispose()
        ogloSettings = Nothing

        If value = "1" Then
            'checking individual Vaccines Inventory Track setting ON/OFF, if ON then showing
            'Adjust Inventory screen
            If GetVaccinInventoryTrack() > 0 Then

                Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                Dim oParam As gloDatabaseLayer.DBParameters
                Dim dt As DataTable = Nothing

                oDB.Connect(False)
                oParam = New gloDatabaseLayer.DBParameters
                oParam.Add("@im_nCategoryID", cmbCategory.SelectedValue, ParameterDirection.Input, SqlDbType.Decimal)
                oParam.Add("@im_sVaccine", txtCvx.Text, ParameterDirection.Input, SqlDbType.NVarChar)
                oParam.Add("@im_sTradeName", txt_TradeName.Text, ParameterDirection.Input, SqlDbType.NVarChar)
                oParam.Add("@im_LotNumber", cmbLotNumber.Text, ParameterDirection.Input, SqlDbType.NVarChar)
                oParam.Add("@im_nLocationID", cmbLocation.SelectedValue, ParameterDirection.Input, SqlDbType.Decimal)

                oDB.Retrive("IM_GetVaccinAvailableDoses", oParam, dt)
                oDB.Disconnect()

                oParam.Dispose()
                oParam = Nothing

                oDB.Dispose()
                oDB = Nothing

                If (IsNothing(dt) = False) Then
                    If dt.Rows.Count > 0 Then
                        Dim myresult As Decimal = dt.Rows(0)(0)
                        dt.Dispose()
                        dt = Nothing
                        Return myresult
                    Else
                        dt.Dispose()
                        dt = Nothing
                        Return 0
                    End If
                Else
                    Return 0
                End If

                Return Nothing
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

    Private Function GetVaccinInventoryTrack() As Long

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dt As DataTable = Nothing

        oDB.Connect(False)
        oParam = New gloDatabaseLayer.DBParameters
        oParam.Add("@im_nCategoryID", cmbCategory.SelectedValue, ParameterDirection.Input, SqlDbType.Decimal)
        oParam.Add("@im_sVaccine", txtCvx.Text, ParameterDirection.Input, SqlDbType.NVarChar)

        oParam.Add("@im_sTradeName", txt_TradeName.Text, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_LotNumber", cmbLotNumber.Text, ParameterDirection.Input, SqlDbType.NVarChar)
        oParam.Add("@im_nLocationID", cmbLocation.SelectedValue, ParameterDirection.Input, SqlDbType.Decimal)

        oDB.Retrive("IM_GetVaccinInventoryTrack", oParam, dt)
        oDB.Disconnect()

        oParam.Dispose()
        oParam = Nothing

        oDB.Dispose()
        oDB = Nothing
        If (IsNothing(dt) = False) Then
            If dt.Rows.Count > 0 Then
                Dim myResult As Long = dt.Rows(0)(0)
                dt.Dispose()
                dt = Nothing
                Return myResult
            Else
                dt.Dispose()
                dt = Nothing
                Return 0
            End If
        Else
            Return 0
        End If


    End Function

    Private Sub lblDosesOnHand_MouseLeave(sender As Object, e As System.EventArgs) Handles lblDosesOnHand.MouseLeave
        If lblDosesOnHand.ForeColor = Color.Blue Then
            Cursor.Current = Cursors.Default
        End If
    End Sub

    Private Sub lblDosesOnHand_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lblDosesOnHand.MouseMove
        'If lblDosesOnHand.ForeColor = Color.Blue Then
        Cursor.Current = Cursors.Hand
        'End If
    End Sub

    Private Sub lblDosesOnHand_Click(sender As System.Object, e As System.EventArgs) Handles lblDosesOnHand.Click
        Dim frm As New frmIM_Setup
        Dim VaccineID = GetVaccinInventoryTrack()
        If VaccineID > 0 Then
            frm._EditID = VaccineID
            frm._blnOPenedFromDoses = True
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            GetVaccinAvailableDoses()
        End If
        frm.Dispose()
        frm = Nothing
    End Sub

    Private Sub cmbCategory_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbCategory.SelectedValueChanged
        GetVaccinAvailableDoses()
    End Sub

    Private Function GetRequirefunding() As String
        'Added by Amit to check Track Vaccine Inventory setting is ON or OFF
        Dim value As New Object()
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        ogloSettings.GetSetting("REQUIREFUNDING", 0, gnClinicID, value)
        ogloSettings.Dispose()
        ogloSettings = Nothing
        Return value
    End Function

    Private Sub btnAddCategory_Click(sender As System.Object, e As System.EventArgs) Handles btnAddCategory.Click
        Try
            AddCategory("Immunization Inventory Category", "Add Immunization Inventory Category")
            GetRefreshedCategory()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetRefreshedCategory(Optional ByVal RefreshUnitCodes As RefreshType = RefreshType.None)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dsIM As DataSet = Nothing

        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)
            oDB.Retrive("IM_TransactionFillControl", dsIM)
            oDB.Disconnect()
            If (IsNothing(dsIM) = False) Then


                If RefreshUnitCodes = RefreshType.None Then

                    RemoveHandler cmbCategory.SelectedValueChanged, AddressOf cmbCategory_SelectedValueChanged
                    cmbCategory.DataSource = dsIM.Tables(7)
                    cmbCategory.ValueMember = "nCategoryID"
                    cmbCategory.DisplayMember = "sDescription"
                    cmbCategory.SelectedIndex = -1
                    AddHandler cmbCategory.SelectedValueChanged, AddressOf cmbCategory_SelectedValueChanged

                ElseIf RefreshUnitCodes = RefreshType.UnitOfMeasureCodes Then

                    Dim _prevSelectedUnitCode As String = ""
                    Dim _isPrevUnitCodeSelectedStandard As Boolean = False


                    If Not IsNothing(cmbUnitOfMeasure.SelectedValue) AndAlso cmbUnitOfMeasure.Text.Trim() <> "" Then
                        _prevSelectedUnitCode = cmbUnitOfMeasure.SelectedValue
                        _isPrevUnitCodeSelectedStandard = True
                    ElseIf cmbUnitOfMeasure.Text.Trim() <> "" AndAlso IsNothing(cmbUnitOfMeasure.SelectedValue) Then
                        _prevSelectedUnitCode = cmbUnitOfMeasure.Text
                        _isPrevUnitCodeSelectedStandard = False
                    End If

                    cmbUnitOfMeasure.DataSource = dsIM.Tables(8)
                    cmbUnitOfMeasure.ValueMember = "Code"
                    cmbUnitOfMeasure.DisplayMember = "Unit"
                    cmbUnitOfMeasure.SelectedIndex = -1

                    If _isPrevUnitCodeSelectedStandard = True Then
                        cmbUnitOfMeasure.SelectedValue = _prevSelectedUnitCode
                    Else
                        cmbUnitOfMeasure.Text = _prevSelectedUnitCode
                    End If

                ElseIf RefreshUnitCodes = RefreshType.PublicityCodes Then

                    Dim _prevSelectedUnitCode As String = ""

                    If Not IsNothing(cmbPublicityCode.SelectedValue) AndAlso cmbPublicityCode.Text.Trim() <> "" Then
                        _prevSelectedUnitCode = cmbPublicityCode.SelectedValue
                        'ElseIf cmbPublicityCode.Text.Trim() <> "" And IsNothing(cmbPublicityCode.SelectedValue) Then
                        '_prevSelectedUnitCode = cmbPublicityCode.Text
                    End If

                    cmbPublicityCode.DataSource = dsIM.Tables(9)
                    cmbPublicityCode.ValueMember = "Code"
                    cmbPublicityCode.DisplayMember = "Unit"
                    cmbPublicityCode.SelectedIndex = -1

                    cmbPublicityCode.SelectedValue = _prevSelectedUnitCode

                End If

            End If
        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default

            'If Not IsNothing(dsIM) Then
            '    dsIM.Dispose()
            '    dsIM = Nothing
            'End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Private Sub btnAddUnitsOfMeasureItem_Click(sender As System.Object, e As System.EventArgs) Handles btnAddUnitsOfMeasureItem.Click
        Try
            If AddCategory("Unit Of Measure Codes", "Add Unit Of Measure Code") = True Then
                GetRefreshedCategory(RefreshType.UnitOfMeasureCodes)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBrowseSnomed_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseSnomed.Click
        ShowSnoMedSelector()
    End Sub

    Private Sub btnClearSnomed_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSnomed.Click
        Try

            lblSnomedIdValue.Text = ""
            lblSnomedIdValue.Tag = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowSnoMedSelector()

        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem(Me.Text, gstrSMDBConnstr, GetConnectionString())
        Dim _snomedInfo As ArrayList = Nothing
        Try
            ' frm.StartPosition = FormStartPosition.CenterScreen
            ' frm.ShowInTaskbar = False
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            If frm._DialogResult Then

                If frm.strConceptID.Trim() <> "" Then

                    lblSnomedIdValue.Text = frm.strSelectedConceptID + "-" + frm.strSelectedDescription
                    'lblSnomedIdValue.Tag = frm.strSelectedConceptID
                    _snomedInfo = New ArrayList()
                    _snomedInfo.Add(frm.strSelectedConceptID)
                    _snomedInfo.Add(frm.strSelectedDescription)

                    lblSnomedIdValue.Tag = _snomedInfo

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _snomedInfo = Nothing
            frm.Dispose()
        End Try
    End Sub

    Private Sub btnAddPublicityCodeItem_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPublicityCodeItem.Click
        Try
            If AddCategory("Publicity Code", "Add Publicity Code") = True Then
                GetRefreshedCategory(RefreshType.PublicityCodes)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



#Region "Uncertain Formulation CVX" 'Added by manoj jadhav on 20130927 for MU2 Trnasfer to immunization registry : Sharing uncertain Formulation CVX

    Dim lUncertainCVX As List(Of FrmIM_UncertainCVX.UncertainCVX) = Nothing

    Private Sub BtnUncertainCVX_Click(sender As System.Object, e As System.EventArgs) Handles BtnUncertainCVX.Click
        Dim objFrmIM_UncertainCVX As FrmIM_UncertainCVX = Nothing
        Try
            If Len(txtCvx.Text.Trim) <= 0 Then
                MessageBox.Show("select vaccination first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Try
            End If
            If lUncertainCVX Is Nothing Then
                Exit Try
            End If
            Me.Cursor = Cursors.WaitCursor
            Update_UncertainFormulationCVX()
            objFrmIM_UncertainCVX = New FrmIM_UncertainCVX()
            objFrmIM_UncertainCVX._lUnCertainCVX = lUncertainCVX
            If _isHistory = True Then
                objFrmIM_UncertainCVX._isHistory = 1
            ElseIf _isHistory = False Then
                objFrmIM_UncertainCVX._isHistory = 0
            End If
            objFrmIM_UncertainCVX.ShowDialog(IIf(IsNothing(objFrmIM_UncertainCVX.Parent), Me, objFrmIM_UncertainCVX.Parent))
            If objFrmIM_UncertainCVX._UnCertainFormulationCVXSaved Then
                lUncertainCVX = objFrmIM_UncertainCVX._lUnCertainCVX
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            If Not objFrmIM_UncertainCVX Is Nothing Then
                objFrmIM_UncertainCVX.Dispose()
                objFrmIM_UncertainCVX = Nothing
            End If
        End Try
    End Sub

    Private Sub Get_AllUncertainFomulationCVX()
        Dim oClsIM As gloStream.Immunization.ItemSetup
        Dim dt As DataTable = Nothing
        Dim item As FrmIM_UncertainCVX.UncertainCVX = Nothing
        Try
            If lUncertainCVX Is Nothing Then
                lUncertainCVX = New List(Of FrmIM_UncertainCVX.UncertainCVX)
            End If
            oClsIM = New gloStream.Immunization.ItemSetup()
            dt = oClsIM.GetUncertainVaccincationFromCVXCode(txtCvx.Text.Trim(), _TransactionID, _isHistory)
            lUncertainCVX.Clear()
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                BtnUncertainCVX.Enabled = True
                For icnt As Integer = 0 To dt.Rows.Count - 1
                    item = New FrmIM_UncertainCVX.UncertainCVX()
                    item.bCheck = dt.Rows(icnt)("SEL")
                    item.UCVXCode = dt.Rows(icnt)("UCVXCode")
                    item.UCVXText = dt.Rows(icnt)("UCVXName")
                    If _TransactionID = 0 Then
                        item.UCVxPresentationDate = dttransaction_date.Value.Date
                        If dtpublication_date.Checked Then
                            item.UCVxPublicationDate = dtpublication_date.Value.Date
                        End If
                    Else
                        If IsDate(dt.Rows(icnt)("PresentedDate")) Then
                            item.UCVxPresentationDate = dt.Rows(icnt)("PresentedDate")
                        End If
                        If IsDate(dt.Rows(icnt)("PublicationDate")) Then
                            item.UCVxPublicationDate = dt.Rows(icnt)("PublicationDate")
                        End If
                    End If
                    If Not IsDBNull(dt.Rows(icnt)("ImmunizationScheduleUsed")) Then
                        item.UCVxImmunizationScheduleUsed = dt.Rows(icnt)("ImmunizationScheduleUsed")
                    End If
                    If Not IsDBNull(dt.Rows(icnt)("IsValidDose")) Then
                        item.UCVxIsDoseValid = dt.Rows(icnt)("IsValidDose")
                    End If
                    If Not IsDBNull(dt.Rows(icnt)("Reason")) Then
                        item.UCVxReason = dt.Rows(icnt)("Reason")
                    End If
                    lUncertainCVX.Add(item)
                Next
            Else
                BtnUncertainCVX.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            item = Nothing
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            oClsIM = Nothing
        End Try
    End Sub

    Private Function getUncettainFormulationCVX_Table() As DataTable
        Dim _DtResult As DataTable = Nothing
        Dim item As FrmIM_UncertainCVX.UncertainCVX = Nothing
        Try
            _DtResult = New DataTable()
            _DtResult.Columns.Add("UCVXCode", GetType(String))
            _DtResult.Columns.Add("UCVXDecs", GetType(String))
            _DtResult.Columns.Add("PresentedDate", GetType(Date))
            _DtResult.Columns.Add("PublicationDate", GetType(Date))
            If lUncertainCVX Is Nothing Then
                Exit Try
            End If
            Update_UncertainFormulationCVX()
            For i As Integer = 0 To lUncertainCVX.Count - 1
                item = lUncertainCVX(i)
                If item.bCheck Then
                    _DtResult.Rows.Add()
                    _DtResult.Rows(_DtResult.Rows.Count - 1)("UCVXCode") = item.UCVXCode
                    _DtResult.Rows(_DtResult.Rows.Count - 1)("UCVXDecs") = item.UCVXText
                    If IsDate(item.UCVxPresentationDate) Then
                        _DtResult.Rows(_DtResult.Rows.Count - 1)("PresentedDate") = item.UCVxPresentationDate
                    Else
                        _DtResult.Rows(_DtResult.Rows.Count - 1)("PresentedDate") = dttransaction_date.Value
                    End If
                    If IsDate(item.UCVxPublicationDate) Then
                        _DtResult.Rows(_DtResult.Rows.Count - 1)("PublicationDate") = item.UCVxPublicationDate
                    ElseIf dtpublication_date.Checked Then
                        _DtResult.Rows(_DtResult.Rows.Count - 1)("PublicationDate") = dtpublication_date.Value
                    End If
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return _DtResult
    End Function

    Private Function getImmunizationVIS_Table() As DataTable
        Dim dtImmVIS As DataTable = Nothing
        Try
            dtImmVIS = New DataTable()
            dtImmVIS.Columns.Add("DocumentId", GetType(Int64))
            dtImmVIS.Columns.Add("EncodedText", GetType(String))
            dtImmVIS.Columns.Add("PresentedDate", GetType(Date))

            For i As Integer = 1 To c1VIS.Rows.Count - 1
                If c1VIS.Rows(i)(1) <> 0 And c1VIS.Rows(i)(2) = 1 Then
                    dtImmVIS.Rows.Add(Convert.ToInt64(c1VIS.Rows(i)(1)), c1VIS.Rows(i)(5), c1VIS.Rows(i)(7))
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return dtImmVIS
    End Function

    Private Function getIsVISDocAssociated() As Boolean
        Dim IsImmVIS As Boolean = False
        Try
            For i As Integer = 1 To c1VIS.Rows.Count - 1
                If c1VIS.Rows(i)(1) <> 0 And c1VIS.Rows(i)(2) = 1 Then
                    IsImmVIS = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return IsImmVIS
    End Function

    Private Sub Update_UncertainFormulationCVX()
        Dim item As FrmIM_UncertainCVX.UncertainCVX = Nothing
        Try
            If lUncertainCVX Is Nothing Then
                Exit Try
            End If
            If lUncertainCVX.Count() = 0 Then
                Exit Try
            End If
            For i As Integer = 0 To lUncertainCVX.Count - 1
                item = lUncertainCVX(i)
                If item.bCheck Then
                    If item.UCVxPresentationDate Is Nothing OrElse Not IsDate(item.UCVxPresentationDate) Then
                        item.UCVxPresentationDate = dttransaction_date.Value.Date
                    End If
                    If (item.UCVxPublicationDate Is Nothing OrElse Not IsDate(item.UCVxPublicationDate)) AndAlso dtpublication_date.Checked Then
                        item.UCVxPublicationDate = dtpublication_date.Value.Date
                    End If
                End If
                lUncertainCVX(i) = item
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Ordering Provider Setup "

    'Dim dtOrdPv As DataTable = Nothing
    Dim OrdProviderType As OrderingProviderType = OrderingProviderType.None
    Dim oPhyListControl As New gloListControl.gloListControl

    Enum OrderingProviderType
        None = 0
        Provider = 1
        Physician = 2
    End Enum

    Private Sub btnRefProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefProvider.Click

        Try
            ClosePhyControl()
            oPhyListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Physicians, False, Me.Width)
            oPhyListControl.ControlHeader = "Physicians"
            oPhyListControl.ShowPhysiciansWithProvider = True
            oPhyListControl.IsFromReport = True
            AddHandler oPhyListControl.ItemSelectedClick, AddressOf PhysiciansSelectedClick
            AddHandler oPhyListControl.ItemClosedClick, AddressOf PhysiciansItemClosedClick
            oPhyListControl.Dock = DockStyle.Fill
            Me.Controls.Add(oPhyListControl)

            oPhyListControl.OpenControl()

            If oPhyListControl.IsDisposed = False Then
                oPhyListControl.Dock = DockStyle.Fill
                oPhyListControl.BringToFront()
            End If

        Catch ex As Exception
        Finally

        End Try
    End Sub
    Private Sub PhysiciansSelectedClick(ByVal sender As Object, ByVal e As EventArgs)

        lblOrdProvider.Text = oPhyListControl.SelectedItems(0).Description
        lblOrdProvider.Tag = oPhyListControl.SelectedItems(0).ID

        If Not IsNothing(oPhyListControl.SelectedItems(0).Code) Then
            If oPhyListControl.SelectedItems(0).Code = "Provider" Then
                OrdProviderType = OrderingProviderType.Provider
            ElseIf oPhyListControl.SelectedItems(0).Code = "Physicians" Then
                OrdProviderType = OrderingProviderType.Physician
            Else
                OrdProviderType = OrderingProviderType.None
            End If

        Else

            OrdProviderType = OrderingProviderType.None
        End If

    End Sub

    Private Sub PhysiciansItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ClosePhyControl()
    End Sub

    Private Sub btnClrRefProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClrRefProvider.Click
        lblOrdProvider.Text = ""
        lblOrdProvider.Tag = 0
        OrdProviderType = OrderingProviderType.None

    End Sub
    Private Sub ClosePhyControl()
        If Not IsNothing(oPhyListControl) Then
            Me.Controls.Remove(oPhyListControl)
            RemoveHandler oPhyListControl.ItemSelectedClick, AddressOf PhysiciansSelectedClick
            RemoveHandler oPhyListControl.ItemClosedClick, AddressOf PhysiciansItemClosedClick
            oPhyListControl.Dispose()
            oPhyListControl = Nothing
        End If
    End Sub

#End Region

#Region "Refusal reason code "
    Private Sub btnBrwRefusalReason_Click(sender As System.Object, e As System.EventArgs) Handles btnBrwRefusalReason.Click
        Try
            ofrmLateralityList = New frmViewListControl
            oLateralityListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.MUCQMRefusedCode, False, Me.Width)
            oLateralityListControl.ControlHeader = "Refusal Reason Code"
            'set the property true for refused code you want 
            oLateralityListControl.bShowNotTakenCodes = True
            oLateralityListControl.bShowAttributeCodes = True
            oLateralityListControl.bRefusedImmunizationCode = True
            oLateralityListControl.strSearchText = strRefusalCode
            AddHandler oLateralityListControl.ItemSelectedClick, AddressOf oLateralityListControl_ItemSelectedClick
            AddHandler oLateralityListControl.ItemClosedClick, AddressOf oLateralityListControl_ItemClosedClick
            ofrmLateralityList.Controls.Add(oLateralityListControl)
            oLateralityListControl.Dock = DockStyle.Fill
            oLateralityListControl.BringToFront()

            oLateralityListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmLateralityList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmLateralityList.Text = "Refusal Reason Code"
            ofrmLateralityList.ShowDialog(IIf(IsNothing(CType(ofrmLateralityList, Control).Parent), Me, CType(ofrmLateralityList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmLateralityList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oLateralityListControl.ItemSelectedClick, AddressOf oLateralityListControl_ItemSelectedClick
                RemoveHandler oLateralityListControl.ItemClosedClick, AddressOf oLateralityListControl_ItemClosedClick
                oLateralityListControl.Dispose()
                oLateralityListControl = Nothing
            End If

            If IsNothing(ofrmLateralityList) = False Then
                ofrmLateralityList.Dispose()
                ofrmLateralityList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClearRefusalReason_Click(sender As System.Object, e As System.EventArgs) Handles btnClearRefusalReason.Click
        txtRefusalReason.Clear()
        strRefusalCode = String.Empty
        strRefusalDescription = String.Empty

    End Sub

    Private Sub oLateralityListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmLateralityList.Close()
    End Sub

    Private Sub oLateralityListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oLateralityListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oLateralityListControl.SelectedItems.Count - 1
                    txtRefusalReason.Text = oLateralityListControl.SelectedItems(i).Code + " - " + oLateralityListControl.SelectedItems(i).Description
                    strRefusalCode = Convert.ToString(oLateralityListControl.SelectedItems(i).Code)
                    strRefusalDescription = Convert.ToString(oLateralityListControl.SelectedItems(i).Description)
                Next
                ofrmLateralityList.Close()
            Else
                txtRefusalReason.Clear()
                ofrmLateralityList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub oCQMListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmCQMList.Close()
    End Sub

    Private Sub oCQMListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oCQMListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCQMListControl.SelectedItems.Count - 1
                    txtcqm.Text = oCQMListControl.SelectedItems(i).Description
                    strCQMCode = Convert.ToString(oCQMListControl.SelectedItems(i).Code)
                    strCQMDescription = Convert.ToString(oCQMListControl.SelectedItems(i).Description)
                Next
                ofrmCQMList.Close()
            Else
                txtcqm.Clear()
                ofrmCQMList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

    Private Sub TabImmunization_Click(sender As System.Object, e As System.EventArgs) Handles TabImmunization.Click
        If TabImmunization.SelectedIndex = 2 Then
            If isVISGridFilled = True Then
                If c1VIS.Rows.Count > 1 Then
                    Dim Vaccine_Data As String()
                    Dim CVX_Code As String = "0"
                    If txtCvx.Text <> "" Then
                        Vaccine_Data = Split(txtCvx.Text, "-")
                        CVX_Code = Vaccine_Data(0)
                    End If
                    If CVX_Code <> c1VIS.GetData(1, 4) Then
                        fillGrid()
                    End If
                Else
                    fillGrid()
                End If
            Else
                fillGrid()
            End If
        End If

    End Sub

    Private Sub fillGrid()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtVIS As New DataTable()
        Dim Vaccine_Data As String()
        Dim CVX_Code As String = "0"

        Try
            If txtCvx.Text <> "" Then
                Vaccine_Data = Split(txtCvx.Text, "-")
                CVX_Code = Vaccine_Data(0)

                If CVX_Code <> "0" Then
                    oDB.Connect(False)
                    oParam = New gloDatabaseLayer.DBParameters
                    oParam.Add("@CVX_Code", CVX_Code, ParameterDirection.Input, SqlDbType.Text)
                    oParam.Add("@IM_TrnDTLMstID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt)
                    If cmbSKU.Text <> "" Then
                        oParam.Add("@IM_SKU", cmbSKU.Text, ParameterDirection.Input, SqlDbType.VarChar)
                    Else
                        oParam.Add("@IM_SKU", "0", ParameterDirection.Input, SqlDbType.VarChar)
                    End If

                    If _TransactionID = 0 Then
                        oParam.Add("@Action", "Add", ParameterDirection.Input, SqlDbType.VarChar)
                    Else
                        oParam.Add("@Action", "Modify", ParameterDirection.Input, SqlDbType.VarChar)
                    End If

                    oParam.Add("@im_LotNumber", cmbLotNumber.Text, ParameterDirection.Input, SqlDbType.Text)
                    oParam.Add("@im_nLocationID", cmbLocation.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt)


                    oDB.Retrive("IM_ShowImmunizationVIS", oParam, dtVIS)

                    If dtVIS.Rows.Count > 0 Then
                        c1VIS.DataSource = dtVIS
                        Designgrid()
                        'C1VIS.Row = 1
                        'If Not IsDBNull(c1VIS.GetData(1, 1)) Then
                        '    _DocumentID = c1VIS.GetData(1, 1)
                        'Else
                        '    _DocumentID = 0
                        'End If
                        isVISGridFilled = True
                    Else
                        c1VIS.DataSource = dtVIS
                        Designgrid()
                        isVISGridFilled = False
                    End If

                    isDocumentImported()

                    oParam.Dispose()
                    oParam = Nothing

                    oDB.Disconnect()
                    oDB = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error while fill grid", "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub isDocumentImported()
        Dim _isDocumentImported As Boolean = False

        If c1VIS.Rows.Count > 1 Then
            For i As Integer = 1 To c1VIS.Rows.Count - 1
                If c1VIS.GetData(i, 1) <> 0 Then
                    _isDocumentImported = True
                    Exit For
                End If
            Next
        End If

        If _isDocumentImported = True Then
            chk_vis_given.Checked = True
        Else
            chk_vis_given.Checked = False
        End If
    End Sub

    Private Sub Designgrid()
        Try
            c1VIS.Cols(0).Caption = "ID"
            c1VIS.Cols(1).Caption = "Document Id"
            c1VIS.Cols(2).Caption = ""
            c1VIS.Cols(3).Caption = "Document Name"
            c1VIS.Cols(4).Caption = "CVX Code"
            c1VIS.Cols(5).Caption = "Encoded text"
            c1VIS.Cols(6).Caption = "Edition Date"
            c1VIS.Cols(7).Caption = "Presented Date"

            c1VIS.Cols.Count = 8
            c1VIS.Rows.Fixed = 1

            c1VIS.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            c1VIS.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, FontStyle.Regular)
            c1VIS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            c1VIS.BackColor = Color.White
            c1VIS.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            c1VIS.ScrollBars = ScrollBars.Both
            SetGridStyle(c1VIS)
            ' set visibility of column
            c1VIS.Cols(0).Visible = False
            c1VIS.Cols(1).Visible = False
            c1VIS.Cols(2).Visible = True
            c1VIS.Cols(3).Visible = True
            c1VIS.Cols(4).Visible = True
            c1VIS.Cols(5).Visible = True
            c1VIS.Cols(6).Visible = True
            c1VIS.Cols(7).Visible = True
            ' set column type
            c1VIS.Cols(2).DataType = GetType(Boolean)
            c1VIS.Cols(7).DataType = GetType(Date)

            c1VIS.AllowEditing = True
            ' set column editing
            c1VIS.Cols(2).AllowEditing = True
            c1VIS.Cols(3).AllowEditing = False
            c1VIS.Cols(4).AllowEditing = False
            c1VIS.Cols(5).AllowEditing = False
            c1VIS.Cols(6).AllowEditing = False
            c1VIS.Cols(7).AllowEditing = True
            'set Heading
            c1VIS.SetData(0, 2, "Select")
            c1VIS.SetData(0, 3, "Document Name")
            c1VIS.SetData(0, 4, "CVX Code")
            c1VIS.SetData(0, 5, "Encoded text")
            c1VIS.SetData(0, 6, "Edition Date")
            c1VIS.SetData(0, 7, "Presented Date")
            ' set width

            Dim nWidth As Integer = c1VIS.Width

            c1VIS.Cols(2).Width = CInt((0.05 * (nWidth)))
            c1VIS.Cols(3).Width = CInt((0.3 * (nWidth)))
            c1VIS.Cols(4).Width = CInt((0.07 * (nWidth)))
            c1VIS.Cols(5).Width = CInt((0.3 * (nWidth)))
            c1VIS.Cols(6).Width = CInt((0.1 * (nWidth)))
            c1VIS.Cols(7).Width = CInt((0.1 * (nWidth)))

            c1VIS.ExtendLastCol = True
            'Align ment
            c1VIS.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            c1VIS.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1VIS.Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1VIS.Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1VIS.Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            c1VIS.Cols(7).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        Catch ex As Exception
            MessageBox.Show("Error while designing VIS grid", "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub SetGridStyle(oFlex As C1.Win.C1FlexGrid.C1FlexGrid)
        Try
            c1VIS.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            'oFlex.Rows.Count = 1
            oFlex.Rows.Fixed = 1

            oFlex.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            oFlex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            oFlex.BackColor = System.Drawing.Color.FromArgb(240, 247, 255)


            oFlex.Styles.Fixed.BackColor = Color.FromArgb(86, 126, 211)
            oFlex.Styles.Fixed.ForeColor = Color.White
            oFlex.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Bold)

            oFlex.Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250)
            ' Color.LightBlue;
            oFlex.Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)
            oFlex.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)

            oFlex.Styles.Normal.BackColor = Color.FromArgb(240, 247, 255)
            oFlex.Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)
            oFlex.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221)

            oFlex.Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102)
            oFlex.Styles.Highlight.ForeColor = Color.Black

            oFlex.Styles.Focus.BackColor = Color.FromArgb(254, 207, 102)
            oFlex.Styles.Focus.ForeColor = Color.Black

            Dim csHeader As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_Header")
            Try
                If (oFlex.Styles.Contains("CS_Header")) Then
                    csHeader = oFlex.Styles("CS_Header")
                Else
                    csHeader = oFlex.Styles.Add("CS_Header")
                    csHeader.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold)
                    csHeader.ForeColor = Color.Black
                    csHeader.BackColor = Color.FromArgb(192, 203, 233)
                    'csHeader.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    csHeader.DataType = Type.[GetType]("System.String")
                End If
            Catch ex As Exception
                csHeader = oFlex.Styles.Add("CS_Header")
                csHeader.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Bold)
                csHeader.ForeColor = Color.Black
                csHeader.BackColor = Color.FromArgb(192, 203, 233)
                'csHeader.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csHeader.DataType = Type.[GetType]("System.String")
            End Try




            Dim csRecord As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_Record")
            Try
                If (oFlex.Styles.Contains("CS_Record")) Then
                    csRecord = oFlex.Styles("CS_Record")
                Else
                    csRecord = oFlex.Styles.Add("CS_Record")
                    csRecord.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) ' IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular)
                    csRecord.ForeColor = Color.Black
                    csRecord.BackColor = Color.GhostWhite
                    csRecord.DataType = Type.[GetType]("System.String")
                End If
            Catch ex As Exception
                csRecord = oFlex.Styles.Add("CS_Record")
                csRecord.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csRecord.ForeColor = Color.Black
                csRecord.BackColor = Color.GhostWhite
                csRecord.DataType = Type.[GetType]("System.String")
            End Try

            'csRecord.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack



            Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_ComboList")
            Try
                If (oFlex.Styles.Contains("CS_ComboList")) Then
                    csComboList = oFlex.Styles("CS_ComboList")
                Else
                    csComboList = oFlex.Styles.Add("CS_ComboList")
                    csComboList.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    csComboList.ForeColor = Color.Black
                    csComboList.BackColor = Color.GhostWhite
                    'csComboList.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    csComboList.DataType = Type.[GetType]("System.String")
                    csComboList.ComboList = "..."
                End If
            Catch ex As Exception
                csComboList = oFlex.Styles.Add("CS_ComboList")
                csComboList.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csComboList.ForeColor = Color.Black
                csComboList.BackColor = Color.GhostWhite
                'csComboList.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csComboList.DataType = Type.[GetType]("System.String")
                csComboList.ComboList = "..."
            End Try



            Dim csCheckBox As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_CheckBox")
            Try
                If (oFlex.Styles.Contains("CS_CheckBox")) Then
                    csCheckBox = oFlex.Styles("CS_CheckBox")
                Else
                    csCheckBox = oFlex.Styles.Add("CS_CheckBox")
                    csCheckBox.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) ' IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    csCheckBox.ForeColor = Color.Black
                    csCheckBox.BackColor = Color.GhostWhite
                    'csCheckBox.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    csCheckBox.DataType = Type.[GetType]("System.Boolean")
                End If
            Catch ex As Exception
                csCheckBox = oFlex.Styles.Add("CS_CheckBox")
                csCheckBox.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csCheckBox.ForeColor = Color.Black
                csCheckBox.BackColor = Color.GhostWhite
                'csCheckBox.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                csCheckBox.DataType = Type.[GetType]("System.Boolean")
            End Try



            Dim csNotNormal As C1.Win.C1FlexGrid.CellStyle '= oFlex.Styles.Add("CS_NotNormal")
            Try
                If (oFlex.Styles.Contains("CS_NotNormal")) Then
                    csNotNormal = oFlex.Styles("CS_NotNormal")
                Else
                    csNotNormal = oFlex.Styles.Add("CS_NotNormal")
                    csNotNormal.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                    csNotNormal.ForeColor = Color.Red
                    csNotNormal.BackColor = Color.GhostWhite
                End If
            Catch ex As Exception
                csNotNormal = oFlex.Styles.Add("CS_NotNormal")
                csNotNormal.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Font, FontStyle.Bold) 'New Font(Font, FontStyle.Regular) 'New Font(Font, FontStyle.Regular)
                csNotNormal.ForeColor = Color.Red
                csNotNormal.BackColor = Color.GhostWhite
            End Try
        Catch ex As Exception
            MessageBox.Show("Error while setting style of VIS grid", "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub c1VIS_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1VIS.AfterEdit
        If e.Col = 7 Then
            If Not c1VIS.GetData(e.Row, e.Col) Is Nothing AndAlso c1VIS.GetData(e.Row, e.Col) <> "" Then
                Dim dttemp As String = Convert.ToDateTime(c1VIS.GetData(e.Row, e.Col)).ToString("MM/dd/yyyy")
                c1VIS.Rows(e.Row)(e.Col) = dttemp
            End If
        End If
    End Sub

    Private Sub c1VIS_Click(sender As System.Object, e As System.EventArgs) Handles c1VIS.Click
        If c1VIS.Rows.Count > 1 Then
            Dim selectedRow As Integer = c1VIS.RowSel
            If c1VIS.GetData(selectedRow, 2) = 1 Then
                If Not IsDBNull(c1VIS.GetData(selectedRow, 1)) Then
                    '_DocumentID = c1VIS.GetData(selectedRow, 1)
                    If c1VIS.GetData(selectedRow, 1) = 0 Then
                        c1VIS.SetData(c1VIS.RowSel, 2, False)
                    End If
                Else
                    _DocumentID = 0
                End If
            Else
                _DocumentID = 0
            End If

            _GridTransId = c1VIS.GetData(selectedRow, 0)
        End If
    End Sub

    Private Sub btnbrwcqm_Click(sender As System.Object, e As System.EventArgs) Handles btnbrwcqm.Click
        Try
            ofrmCQMList = New frmViewListControl
            oCQMListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CQMCategoriesValueSet, False, Me.Width)
            oCQMListControl.ControlHeader = "CQM Categories"

            oCQMListControl.bShowNotTakenCodes = True
            oCQMListControl.bShowAttributeCodes = True
            'oCQMListControl.bCQMCode = True
            'oCQMListControl.strSearchText = strCQMCode
            AddHandler oCQMListControl.ItemSelectedClick, AddressOf oCQMListControl_ItemSelectedClick
            AddHandler oCQMListControl.ItemClosedClick, AddressOf oCQMListControl_ItemClosedClick
            ofrmCQMList.Controls.Add(oCQMListControl)
            oCQMListControl.Dock = DockStyle.Fill
            oCQMListControl.BringToFront()

            oCQMListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmCQMList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCQMList.Text = "CQM Categories"
            ofrmCQMList.ShowDialog(IIf(IsNothing(CType(ofrmCQMList, Control).Parent), Me, CType(ofrmCQMList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmCQMList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oCQMListControl.ItemSelectedClick, AddressOf oCQMListControl_ItemSelectedClick
                RemoveHandler oCQMListControl.ItemClosedClick, AddressOf oCQMListControl_ItemClosedClick
                oCQMListControl.Dispose()
                oCQMListControl = Nothing
            End If

            If IsNothing(ofrmCQMList) = False Then
                ofrmCQMList.Dispose()
                ofrmCQMList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclrcqm_Click(sender As System.Object, e As System.EventArgs) Handles btnclrcqm.Click
        txtcqm.Clear()
        strCQMCode = String.Empty
        strCQMDescription = String.Empty
    End Sub

    Private Sub optPartiallyAdministered_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optPartiallyAdministered.CheckedChanged
        If optPartiallyAdministered.Checked = True Then
            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If
            If _TransactionID > 0 Then
                txt_TradeName.Enabled = False
                btnTradeName.Enabled = False
                BtnAddTradeNameCategory.Enabled = False
            End If
            lblDosesOnHand.Visible = True

            ShowRequiredLable()
            GetLocationProviderList(True)
        End If

    End Sub

    Private Sub optNotAdministered_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optNotAdministered.CheckedChanged
        If optNotAdministered.Checked = True Then
            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If
            If _TransactionID > 0 Then
                txt_TradeName.Enabled = False
                btnTradeName.Enabled = False
                BtnAddTradeNameCategory.Enabled = False
            End If
            lblDosesOnHand.Visible = True

            ShowRequiredLable()
            GetLocationProviderList(True)
        End If

    End Sub
End Class
