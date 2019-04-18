Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient


Public Class frmImTransaction
    Inherits frmBaseForm

    Dim ofrmDiagnosisList = New frmViewListControl
    Dim ofrmList = New frmViewListControl

    Dim _PatientID As Long
    Dim _TransactionID As Long

    Private oDiagnosisListControl As gloListControl.gloListControl
    Private oListControl As gloListControl.gloListControl

    Dim hashtblItemName As New Hashtable

    Dim objfrmHistory As frmHistory

    Dim _isLoadGridCvxControl As Boolean = False
    Private oCVXControl As gloUserControlLibrary.gloUC_GridList
    Private oTradeNameControl As gloUserControlLibrary.gloUC_GridList

    Public Event GridListLoaded()
    Public Event GridListClosed()

    Private oMVXControl As gloUserControlLibrary.gloUC_GridList

    Public Event GridListLoaded1()
    Public Event GridListClosed1()

    Public Event GridListLoaded2()
    Public Event GridListClosed2()
    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Dim _DocumentID As Long
    Dim _AssociatedDocumentID As Long = -1
    Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management
    Dim isSaved As Boolean = False
    Dim _lst As gloEMR.myList

    Public Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer



    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click
        'Developer:Dipak Patil
        'Date:20120207
        'Bug ID/PRD Name/Sales force Case:Immunization PRD
        'Reason: Audit log entry commented as it become absolute due to one entry presented at closed event of form.
        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Immunization Transaction closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Me.Close()
    End Sub

    Public Sub New(ByVal TransactionID As Long, ByVal PatientID As Long)
        MyBase.New()
        InitializeComponent()
        _PatientID = PatientID
        _TransactionID = TransactionID

        cmbLocation.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbLocation.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

    End Sub
    ''Added by Mayuri:20120202-DM rule in Helath plan
    Public Sub New(ByVal TransactionID As Long, ByVal PatientID As Long, ByVal lst As gloEMR.myList)
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

    ''' <summary>
    ''' Selecting master record of particular SKU number
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSKUDetails() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dtsku As New DataTable()
        Dim strSQL As String

        Try

            If Trim(cmbSKU.Text) = "" Then
                strSQL = " SELECT im_sSKU, isnull(im_sVaccine,'') as Vaccine, isnull(im_sManufacturer,'') as im_sManufacturer, isnull(im_sTradeName, '') as im_sTradeName, isnull(im_sNDCCode, 0) as im_sNDCCode, isnull(im_cpt_code,'') as im_cpt_code, isnull(im_Diagnosis_Code,0) as im_Diagnosis_Code " & _
                         " FROM IM_MST where 1 =2"
            Else
                strSQL = " SELECT im_sSKU, isnull(im_sVaccine,'') as Vaccine, isnull(im_sManufacturer,'') as im_sManufacturer, isnull(im_sTradeName, '') as im_sTradeName, isnull(im_sNDCCode, 0) as im_sNDCCode, isnull(im_cpt_code,'') as im_cpt_code, isnull(im_Diagnosis_Code,0) as im_Diagnosis_Code" & _
                         " FROM IM_MST" & _
                         " where im_sActive = 'Active' and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"
            End If


            dtsku = GetList(strSQL)

            Return dtsku

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            If Not IsNothing(dtsku) Then
                dtsku.Dispose()
                dtsku = Nothing
            End If
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
        Dim dtRoute As New DataTable
        Try
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dtRoute)

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            Return dtRoute
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If Not IsNothing(dtRoute) Then
                dtRoute.Dispose()
                dtRoute = Nothing
            End If
        End Try
    End Function


    ''' <summary>
    ''' Fill all combo box list (Administer, Route, Site, ICD, CPT List)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillControl()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dsIM As New DataSet

        Try
            Me.Cursor = Cursors.WaitCursor

            'default Administered should be selected
            optAdministered.Checked = True


            'selecting records of User, provider, sku, route, site refusal reason, funding list from below sp
            oDB.Connect(False)
            oDB.Retrive("IM_TransactionFillControl", dsIM)
            oDB.Disconnect()
            oDB = Nothing


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


            'Get SKU List
            FillSKUList()


            dsIM.Dispose()
            dsIM = Nothing

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(dsIM) Then
                dsIM.Dispose()
                dsIM = Nothing
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmImTransaction_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Close, "Immunization Transaction Closed", gloAuditTrail.ActivityOutCome.Success)
    End Sub

    Private Sub frmImTransaction_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmImTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Developer:Dipak Patil
        'Date:20120207
        'Bug ID/PRD Name/Sales force Case:Immunization PRD
        'Reason: To Implement Audit trail CreateAuditLog line added 
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "Immunization Transaction Opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        FillControl()
        ShowRequiredLable()

        If _TransactionID > 0 Then
            ShowSelectedImmunization()
        Else
            ''Added by Mayuri:20120202-DM rule in Helath plan
            If IsNothing(_lst) = False Then
                _isLoadGridCvxControl = True
                txtCvx.Text = _lst.DMTemplateName
                txtMvx.Text = _lst.NDCCode
                txt_TradeName.Text = _lst.Frequency
                cmbLotNumber.DataSource = Nothing
                cmbLotNumber.Items.Add(_lst.Duration)
                cmbLotNumber.SelectedText = _lst.Duration
                cmbSKU.DataSource = Nothing
                cmbSKU.Items.Add(_lst.Route)
                cmbSKU.SelectedText = _lst.Route

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
        End If
        cmbSKU.Select()
        _isLoaded = True
    End Sub

    Private Sub test()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dtIM As New DataTable()

        oDB.Connect(False)
        oParam = New gloDatabaseLayer.DBParameters
        oParam.Add("@TransactioID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt)
        oDB.Retrive("IM_ShowSelectedImmunization", oParam, dtIM)

        oParam.Dispose()
        oParam = Nothing

        oDB.Disconnect()
        oDB = Nothing

    End Sub

    Private Sub ShowSelectedImmunization()

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dtIM As New DataTable()


        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@TransactioID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("IM_ShowSelectedImmunization", oParam, dtIM)

            If dtIM.Rows.Count > 0 Then

                dttransaction_date.Value = dtIM.Rows(0)("TransactionDate")

                If Not IsDBNull(dtIM.Rows(0)("Administred")) Then
                    If dtIM.Rows(0)("Administred") = "0" Then
                        optAdministered.Checked = True
                    ElseIf dtIM.Rows(0)("Administred") = "1" Then
                        optReported.Checked = True
                    ElseIf dtIM.Rows(0)("Administred") = "2" Then
                        optRefused.Checked = True
                    End If
                End If

                cmbAdministred.SelectedValue = dtIM.Rows(0)("nAdministerID")

                If Not IsDBNull(dtIM.Rows(0)("nLocationID")) Then
                    cmbLocation.SelectedValue = dtIM.Rows(0)("nLocationID")
                End If


                cmbProvider.SelectedValue = dtIM.Rows(0)("nProviderID")

                If Not IsDBNull(dtIM.Rows(0)("sku")) Then
                    cmbSKU.Text = dtIM.Rows(0)("sku")
                End If

                RemoveHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
                RemoveHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
                RemoveHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged


                If Not IsDBNull(dtIM.Rows(0)("Vaccine")) Then
                    txtCvx.Text = dtIM.Rows(0)("Vaccine")
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


                If Not IsDBNull(dtIM.Rows(0)("LotNumber")) Then
                    cmbLotNumber.Text = dtIM.Rows(0)("LotNumber")
                End If


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

                If Not IsDBNull(dtIM.Rows(0)("im_reasonfor_nonadmin")) Then
                    cmbRefusalreason.Text = dtIM.Rows(0)("im_reasonfor_nonadmin")
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
                    If dtIM.Rows(0)("NDCCode") <> 0 Then
                        txtNDCcode.Text = dtIM.Rows(0)("NDCCode")
                    End If
                End If


                cmbIcd.Items.Clear()
                Dim arrICD() As String
                If Not IsDBNull(dtIM.Rows(0)("DiagnosisCode")) Then
                    arrICD = dtIM.Rows(0)("DiagnosisCode").split(",")
                    For i As Integer = 0 To arrICD.GetUpperBound(0)
                        cmbIcd.Items.Add(arrICD(i))
                    Next
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


            End If

            oParam.Dispose()
            oParam = Nothing

            oDB.Disconnect()
            oDB = Nothing

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Showing Master data in several field using SKU
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbSKU_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSKU.SelectedIndexChanged

        Dim dtsku As New DataTable
        _isLoadGridCvxControl = True
        Try

            RemoveHandler txt_TradeName.TextChanged, AddressOf txt_TradeName_TextChanged
            RemoveHandler txtCvx.TextChanged, AddressOf txtCvx_TextChanged
            RemoveHandler txtMvx.TextChanged, AddressOf txtMvx_TextChanged

            dtsku = GetSKUDetails()

            If dtsku.Rows.Count > 0 Then
                txtCvx.Text = dtsku.Rows(0)("Vaccine")
                txt_TradeName.Text = dtsku.Rows(0)("im_sTradeName")
                txtMvx.Text = dtsku.Rows(0)("im_sManufacturer")

                If dtsku.Rows(0)("im_sNDCCode") <> 0 Then
                    txtNDCcode.Text = dtsku.Rows(0)("im_sNDCCode")
                End If

                If dtsku.Rows(0)("im_Diagnosis_Code") <> "" Then
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


            ElseIf cmbSKU.Text.Length > 0 Then
                txtCvx.Text = ""
                txt_TradeName.Text = ""
                txtMvx.Text = ""
                cmbIcd.DataSource = Nothing
                cmbCpt.DataSource = Nothing
                cmbIcd.Items.Clear()
                cmbCpt.Items.Clear()
                txtNDCcode.Text = ""
            End If

            dtexpDate.Checked = False

            'Fill Lot No.
            dtsku = Nothing
            dtsku = New DataTable

            Dim strSQL As String

            strSQL = " SELECT Distinct im_LotNumber " & _
                     " FROM IM_MST" & _
                     " where im_sActive = 'Active' and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"

            strSQL = strSQL + " union select distinct im_trn_Lotnumber as im_LotNumber " & _
                              " from im_trn_dtl " & _
                              " where im_trn_Lotnumber not in " & _
                              " (select distinct im_LotNumber from IM_MST) " & _
                              " and sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'" & _
                              " order by im_LotNumber"



            'RemoveHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged

            If Trim(cmbSKU.Text) <> "" Then
                dtsku = GetList(strSQL)
                If dtsku.Rows.Count > 0 Then
                    cmbLotNumber.DataSource = dtsku
                    cmbLotNumber.ValueMember = "im_LotNumber"
                    cmbLotNumber.DisplayMember = "im_LotNumber"
                    If dtsku.Rows.Count = 1 Then
                        cmbLotNumber.SelectedIndex = 0
                    Else
                        cmbLotNumber.SelectedIndex = -1
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

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _isLoadGridCvxControl = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    ' ''' <summary>
    ' ''' Fill Lot No.
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    'Private Sub cmbVaccine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged

    '    Dim dtsku As New DataTable
    '    Dim strSQL As String

    '    RemoveHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged

    '    strSQL = " SELECT Distinct im_LotNumber " & _
    '             " FROM IM_MST" & _
    '             " where im_sActive = 'Active' and im_sVaccine = '" & cmbVaccine.Text & "'"

    '    If Trim(cmbSKU.Text) <> "" Then
    '        strSQL = strSQL + " and im_sSKU = '" & cmbSKU.Text & "'"

    '        strSQL = strSQL + " union select distinct im_trn_Lotnumber as im_LotNumber " & _
    '      " from im_trn_dtl " & _
    '      " where im_trn_Lotnumber not in " & _
    '      " (select distinct im_LotNumber from IM_MST) " & _
    '      " and sSKU = '" & cmbSKU.Text & "'" & _
    '      " order by im_LotNumber"

    '    End If




    '    dtsku = GetList(strSQL)
    '    '  If dtsku.Rows.Count > 0 Then
    '    cmbLotNumber.DataSource = dtsku
    '    cmbLotNumber.ValueMember = "im_LotNumber"
    '    cmbLotNumber.DisplayMember = "im_LotNumber"
    '    cmbLotNumber.SelectedIndex = -1

    '    AddHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged
    '    ' End If

    'End Sub

    ''' <summary>
    ''' Set Expiry date depends on SKU or Vaccine
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbLotNumber_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLotNumber.SelectedIndexChanged
        SetExpiryDate()
        SetVISDocumentDetails()

    End Sub


    Private Sub SetExpiryDate()
        Dim dtdata As New DataTable
        Dim strSQL As String
        Try
            Me.Cursor = Cursors.WaitCursor

            If Trim(cmbLotNumber.Text) = "" Then
                dtexpDate.Value = Today.Date
                dtexpDate.Checked = False
                Exit Sub
            End If

            strSQL = " SELECT top 1 im_dtExpiration from IM_MST where im_sActive = 'Active' "

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



            strSQL = " SELECT top 1 im_sFundingSource as Funding from IM_MST where im_sActive = 'Active' "

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
            If dtdata.Rows.Count > 0 Then
                If Not IsDBNull(dtdata.Rows(0)("Funding")) Then
                    cmbFunding.Text = dtdata.Rows(0)("Funding")
                End If
            End If


            dtdata.Dispose()
            dtdata = Nothing
            strSQL = Nothing

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnBrowsDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsDiagnosis.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ofrmDiagnosisList = New frmViewListControl
            Dim arrCPTTextSplit As String()
            oDiagnosisListControl = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.Diagnosis, True, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
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
            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog()

            If IsNothing(ofrmDiagnosisList) = False Then
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dtICD9Code As DataTable
            Dim ToList As gloGeneralItem.gloItems
            dtICD9Code = New DataTable
            Dim dcID As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Code")
            dtICD9Code.Columns.Add(dcID)
            dtICD9Code.Columns.Add(dcDescription)
            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem
            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                If oDiagnosisListControl.SelectedItems.Count > 5 Then
                    MessageBox.Show("Select only 5 Diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oDiagnosisListControl.CloseOnDoubleClick = False
                Else
                    For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtICD9Code.NewRow()
                        drTemp("ID") = oDiagnosisListControl.SelectedItems(i).ID
                        drTemp("Code") = oDiagnosisListControl.SelectedItems(i).Code
                        dtICD9Code.Rows.Add(drTemp)
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = oDiagnosisListControl.SelectedItems(i).ID
                        ToItem.Description = oDiagnosisListControl.SelectedItems(i).Code
                        ToList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbIcd.DataSource = dtICD9Code
                    cmbIcd.ValueMember = dtICD9Code.Columns("ID").ColumnName
                    cmbIcd.DisplayMember = dtICD9Code.Columns("Code").ColumnName
                    ofrmDiagnosisList.Close()
                End If
            Else
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

    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmDiagnosisList.Close()
        If IsNothing(ofrmDiagnosisList) = False Then
            ofrmDiagnosisList = Nothing
        End If
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
            ofrmList = New frmViewListControl
            Dim arrCPTTextSplit As String()
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
            ofrmList.ShowDialog()

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


    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            'code added by dipak 20090910 for add all selected code in dataTable and Bind that datable to cmbCPT
            Dim dtCPTCode As DataTable
            Dim ToList As gloGeneralItem.gloItems
            dtCPTCode = New DataTable
            Dim dcID As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Code")
            dtCPTCode.Columns.Add(dcID)
            dtCPTCode.Columns.Add(dcDescription)
            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem
            If oListControl.SelectedItems.Count > 0 Then
                If oListControl.SelectedItems.Count > 5 Then
                    MessageBox.Show("Select only 5 CPT", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oListControl.CloseOnDoubleClick = False
                Else
                    For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtCPTCode.NewRow()
                        drTemp("ID") = oListControl.SelectedItems(i).ID
                        drTemp("Code") = oListControl.SelectedItems(i).Code
                        dtCPTCode.Rows.Add(drTemp)
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = oListControl.SelectedItems(i).ID
                        ToItem.Description = oListControl.SelectedItems(i).Code
                        ToList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbCpt.DataSource = dtCPTCode
                    cmbCpt.ValueMember = dtCPTCode.Columns("ID").ColumnName
                    cmbCpt.DisplayMember = dtCPTCode.Columns("Code").ColumnName
                    cmbCpt.SelectedIndex = 0
                    ofrmList.Close()
                End If
            Else
                ''Added Rahul for Fixed BugID 6726 on 20101129
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

    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ofrmList.Close()
            If IsNothing(ofrmList) = False Then
                ofrmList = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Save.Click
        Dim clsIMTran As New clsgloIMTransaction
        Dim OIM As New gloStream.Immunization.ItemSetup
        Try
            Me.Cursor = Cursors.WaitCursor
            oCvxControl_InternalGridLostFocus(Nothing, Nothing)
            oMvxControl_InternalGridLostFocus(Nothing, Nothing)
            oTradeNameControl_InternalGridLostFocus(Nothing, Nothing)

            If cmbLocation.SelectedIndex = -1 Then
                TabImmunization.SelectTab(TabPageAdministratin)
                MessageBox.Show("Select Location.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbLocation.Select()
                Exit Sub
            End If

            If cmbProvider.Text.Trim = "" Then
                TabImmunization.SelectTab(TabPageAdministratin)
                MessageBox.Show("Select Provider.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbProvider.Select()
                Exit Sub
            End If

            'txtCvx
            If txtCvx.Text.Trim = "" Then
                TabImmunization.SelectTab(TabPageAdministratin)
                MessageBox.Show("Select Vaccine.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCvx.Select()
                Exit Sub
            End If


            If txt_TradeName.Text.Trim = "" Then
                TabImmunization.SelectTab(TabPageAdministratin)
                MessageBox.Show("Select Trade Name.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txt_TradeName.Select()
                Exit Sub
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
            If optRefused.Checked = False Then
                If cmbLotNumber.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Lot Number.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbLotNumber.Select()
                    Exit Sub
                End If
                If txt_dosage_given.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Enter Dosage Given.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_dosage_given.Select()
                    Exit Sub
                End If

            Else

                If cmbRefusalreason.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Select Refusal Reason.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbRefusalreason.Select()
                    Exit Sub
                End If
                If txt_refused_by.Text.Trim = "" Then
                    TabImmunization.SelectTab(TabPageAdministratin)
                    MessageBox.Show("Enter Refused By.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_refused_by.Select()
                    Exit Sub
                End If

            End If
            ''Added on 20120307-To give warnings on save
            If CheckTradeNameisValidagainstSKU() = False Then
                _IsValidationFailed = True
                cmbSKU.Select()
                Exit Sub
            End If
            Dim dtVaccine As DataSet
            Dim _CVX As String = ""
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
                                Exit Sub
                            End If
                        End If


                    End If
                End If
            End If
            ''
           


            With clsIMTran

                .PatientID = _PatientID
                .transaction_id = _TransactionID
                .transaction_date = dttransaction_date.Value

                If optAdministered.Checked Then
                    .admin_repo_refused = 0
                ElseIf optReported.Checked Then
                    .admin_repo_refused = 1
                ElseIf optRefused.Checked Then
                    .admin_repo_refused = 2
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
                .lot_number = cmbLotNumber.Text

                If dtexpDate.Checked = True Then
                    .expiration_date = dtexpDate.Value
                End If

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
                .units = txt_units.Text
                .route = cmbRoute.Text
                .Site = cmbSite.Text

                If chk_vis_given.Checked Then
                    .bvis_given = 1
                Else
                    .bvis_given = 0
                End If

                .vis = txt_vis.Text
                .visDocumentID = _DocumentID
                .VisAssociatedDocumentID = _AssociatedDocumentID
                If dtpublication_date.Checked = True Then
                    .publication_date = dtpublication_date.Value
                End If

                .refusal_reason = cmbRefusalreason.Text
                .refused_by = txt_refused_by.Text
                .refusal_comments = txt_refusal_comments.Text
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
                Dim StrAudittrailString As String = ""
                If (_TransactionID <= 0) Then
                    StrAudittrailString = "Immunization Record Added."
                Else
                    StrAudittrailString = "Immunization Record Modified."
                End If
                Dim TranctionID As Int64 = -1
                TranctionID = .AddIMTransaction()

                If (TranctionID > 0) Then
                    _TransactionID = TranctionID
                End If

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, StrAudittrailString, _PatientID, _TransactionID, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                isSaved = True
                If IsNothing(sender) = False Then
                    _isSaveClicked = True
                    _isClose = True
                    Me.Close()
                End If
            End With

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Error.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally
            If IsNothing(OIM) = False Then
                OIM = Nothing
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub
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
        End Try
    End Function

    Private Sub txt_dosage_given_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_dosage_given.KeyPress
        AllowDecimal(txt_dosage_given.Text, e)
    End Sub

    Private Sub AllowNumericValue(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txt_amount_given_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_amount_given.KeyPress
        AllowDecimal(txt_amount_given.Text, e)
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(46)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

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
    End Sub

    Private Sub ShowRequiredLable()
        If optRefused.Checked = True Then
            lblDosesGiven.Visible = False
            lblLotNumber.Visible = False

            lblRefusalreason.Visible = True
            lblRefusedBy.Visible = True
            chkPatientHasReaction.Enabled = False
            lblNote.Visible = True
            txt_dosage_given.Text = "0"
            RefusalEnableDisable(optRefused.Checked)
            grpReaction.Enabled = False

        Else
            lblDosesGiven.Visible = True
            lblLotNumber.Visible = True
            lblRefusalreason.Visible = False
            lblRefusedBy.Visible = False
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
            txt_units.Enabled = False
            cmbSite.Enabled = False
        Else
            cmbRefusalreason.Enabled = False
            txt_refused_by.Enabled = False
            txt_refusal_comments.Enabled = False

            txt_dosage_given.Enabled = True
            cmbRoute.Enabled = True
            txt_amount_given.Enabled = True
            txt_units.Enabled = True
            cmbSite.Enabled = True
        End If
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

        Dim dt As New DataTable
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
        End If

        _sVaccineName = txtCvx.Text.Trim()


        If _sVaccineName = "" Then
            MessageBox.Show("Select Vaccine to add Reaction.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TabImmunization.SelectTab(TabPageAdministratin)
            txtCvx.Select()
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
            objfrmHistory.ShowDialog()
        End If
        '  End If

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

    Private Sub chk_ResultedInPermDisability_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_ResultedInPermDisability.CheckedChanged
        If chk_ResultedInPermDisability.Checked = True Then
            chk_NoneOfTheAbove.Checked = False
        End If
    End Sub

    Private Sub txtNDCcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        AllowNumericValue(txtNDCcode.Text, e)
    End Sub

    Private Sub btnTradeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeName.Click
        Try
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
            oTradeNameControl.FillControl("")
            _isLoadGridCvxControl = False
            txt_TradeName.Select()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub oCvxControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oCVXControl.C1GridList.Select()
    End Sub

    Private Sub oCvxControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'RemoveHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
        'txtCvx.Text = txtCvx.Text & e.KeyChar
        'txtCvx.Select()
        'AddHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
    End Sub

    Private Sub oMVXControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oMVXControl.C1GridList.Select()
    End Sub

    Private Sub oTradeNameControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oTradeNameControl.C1GridList.Select()
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
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GetCPTCodeFromCVX()
        ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
        cmbCPT.Items.Clear()
        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim dt As DataTable
        If txtCvx.Text.Trim() <> "" Then
            dt = oClsIM.GetCPTFromCVXCode(txtCvx.Text.Trim())
            If IsNothing(dt) = False Then

                If dt.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        cmbCPT.Items.Add(dt.Rows(i)("cptcode").ToString().Trim)
                    Next


                End If
                If cmbCPT.Items.Count > 0 Then
                    cmbCPT.SelectedIndex = 0
                End If

            End If
        End If
    End Sub
    Private Sub oCvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

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

    Private Sub oTradeNameControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

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
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CloseProcedureControl(ByVal _controlName As String)
        Try
            If _controlName = "Cvx" Then
                For i As Integer = 0 To pnlCvxControl.Controls.Count - 1
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
            ElseIf _controlName = "Mvx" Then
                For i As Integer = 0 To pnlMvxControl.Controls.Count - 1
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
            ElseIf _controlName = "TradeName" Then
                For i As Integer = 0 To pnlTradeNameControl.Controls.Count - 1
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

    Private Sub oMvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

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

   
    Private Sub GetCVXMVX()
        ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim ds As DataSet
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
    End Sub
    Private Sub btnCvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCvx.Click
        Try
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
            oCVXControl.FillControl("")
            _isLoadGridCvxControl = False
            txtCvx.Select()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvx.Click
        Try
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
            oMVXControl.FillControl("")
            _isLoadGridCvxControl = False
            txtMvx.Select()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMvx_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnMvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnMvx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnMvx.BackgroundImageLayout = ImageLayout.Stretch
        ToolTip1.SetToolTip(btnMvx, "Select Manufacturer")
    End Sub

    Private Sub btnCvx_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnCvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnCvx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnCvx.BackgroundImageLayout = ImageLayout.Stretch
        ToolTip1.SetToolTip(btnCvx, "Select Vaccine")
    End Sub

    Private Sub btnTradeName_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnTradeName_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
        ToolTip1.SetToolTip(btnTradeName, "Select Trade Name")
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
                End If
            End If
            FillVaccineLot()


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


    Private Sub FillVaccineLot()
        Dim dtsku As New DataTable
        Dim strSQL As String

        RemoveHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged

        strSQL = " SELECT Distinct im_LotNumber " & _
                 " FROM IM_MST" & _
                 " where im_sActive = 'Active' and im_sVaccine = '" & Replace(txtCvx.Text, "'", "''") & "'"

        If Trim(cmbSKU.Text) <> "" Then
            strSQL = strSQL + " and im_sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'"

            strSQL = strSQL + " union select distinct im_trn_Lotnumber as im_LotNumber " & _
          " from im_trn_dtl " & _
          " where im_trn_Lotnumber not in " & _
          " (select distinct im_LotNumber from IM_MST) " & _
          " and sSKU = '" & Replace(cmbSKU.Text, "'", "''") & "'" & _
          " order by im_LotNumber"
        End If

        dtsku = GetList(strSQL)
        cmbLotNumber.DataSource = dtsku
        cmbLotNumber.ValueMember = "im_LotNumber"
        cmbLotNumber.DisplayMember = "im_LotNumber"
        cmbLotNumber.SelectedIndex = -1
        AddHandler cmbLotNumber.SelectedIndexChanged, AddressOf cmbLotNumber_SelectedIndexChanged
    End Sub

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
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    Private Sub btnSearchsku_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnSearchsku, "Select SKU")
    End Sub

    Private Sub btnBrowsDiagnosis_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnBrowsDiagnosis, "Select Diagnosis")
    End Sub

    Private Sub btnBrowsCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnBrowsCPT, "Select CPT")
    End Sub

    Private Sub btnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScan.Click
        Try
            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_VIS) = False Then
                gDMSCategory_VIS = objSettings.DMSCategory_VIS
            End If
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

    Private Function Set_ScanDocumentEvent(ByVal VISCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64, ByRef SelectedDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try

            _result = oScanDocument.ShowEDocument_Immunization(_PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization, _DocumentID, ScanContainerID, ScanDocumentID, SelectedDocumentID, False, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oScanDocument) = False Then
                oScanDocument.Dispose()
                oScanDocument = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Sub ViewScanDoucment()
        Try
            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _SelectedDocumentId As Int64 = 0
            Dim _result As Boolean = False
            If Not IsNothing(oViewDocument) Then
                oViewDocument = Nothing
            End If

            If (_DocumentID > 0) Then
                If IsNothing(oViewDocument) Then
                    oViewDocument = New gloEDocumentV3.gloEDocV3Management()
                End If

                _result = Get_ViewDocumentEvent(_ScanContainerID, _ScanDocumentID, _SelectedDocumentId)
                _DocumentID = _ScanDocumentID

                txt_vis.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If Not IsNothing(oViewDocument) Then
                    oViewDocument = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(oViewDocument) Then
                oViewDocument.Dispose()
            End If
        Finally
            If Not IsNothing(oViewDocument) Then
                oViewDocument.Dispose()
            End If
        End Try
    End Sub

    Private Function Get_ViewDocumentEvent(ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64, ByRef selectedDocumentid As Int64) As Boolean
        If IsNothing(oViewDocument) Then
            oViewDocument = New gloEDocumentV3.gloEDocV3Management()
        End If
        Dim _result As Boolean = False
        Try
            '_PatientID
            _result = oViewDocument.ShowEDocument_Immunization(_PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization, _DocumentID, ScanContainerID, ScanDocumentID, selectedDocumentid, False, True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oViewDocument) = False Then
                oViewDocument.Dispose()
            End If
        End Try
        Return _result
    End Function

    Private Sub tblbtn_PrintVis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_PrintVis.Click
        Try
            If isSaved = False Then
                'If (MessageBox.Show("The Immunization record needs to be saved before open VIS Document. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                Try

                    tblbtn_Save_Click(Nothing, Nothing)
                    If (isSaved = False) Then
                        Exit Sub
                    End If
                Catch ex As Exception
                Finally
                    isSaved = False
                End Try
            End If

            'Developer:Dipak Patil
            'Date:20120207
            'Bug ID/PRD Name/Sales force Case:Immunization PRD
            'Reason: CreateAuditLog Entry For Immunization
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "VIS document viewed.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            'End If
            If (_DocumentID > 0) Then
                ViewScanDoucment()
                chk_vis_given.Checked = True
            Else
                'txt_TradeName,cmbLotNumber,txtCvx
                Dim DocumentIDForCopy As Int64
                Dim dtIM As New DataTable

                dtIM = GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())
                If dtIM.Rows.Count > 0 Then
                    If Not IsDBNull(dtIM.Rows(0)("DocumentID")) Then
                        DocumentIDForCopy = dtIM.Rows(0)("DocumentID")
                    End If
                End If

                'Dim DocumentIDForCopy As Int64 = GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())
                Dim IsAssociatedDocumentIDPresentForPatient As Boolean = False
                Dim AlreadyExistDocumentID As Int64 = -1
                AlreadyExistDocumentID = GetAlreadyExistDocumentID(_PatientID, DocumentIDForCopy)

                If (AlreadyExistDocumentID > 0) Then
                    IsAssociatedDocumentIDPresentForPatient = True
                    _DocumentID = AlreadyExistDocumentID
                End If

                If (DocumentIDForCopy > 0) And (IsAssociatedDocumentIDPresentForPatient = False) Then
                    CopyVisDocument(_PatientID, DocumentIDForCopy)
                Else

                End If
                If (_DocumentID > 0) Then
                    ViewScanDoucment()
                    chk_vis_given.Checked = True
                Else
                    'MessageBox.Show("This vaccine does not have any VIS document associated document it", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnScan_Click(Nothing, Nothing)
                    chk_vis_given.Checked = True
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub CopyVisDocument(ByVal nPatinetID As Int64, ByVal MasterDocumentID As Int64)
        Dim cmd As SqlCommand
        Dim sqlParam As SqlParameter
        Dim ExamParam As SqlParameter
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
            If Not IsNothing(sqlParam) Then 'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(ExamParam) Then
                ExamParam = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Sub

    Public Function GetAssociatedVisDocumentID(ByVal nPatinetID As Int64, ByVal Vaccine_Name As String, ByVal LotNumber As String, ByVal sTradeName As String) As DataTable

        Dim dtIM As New DataTable()

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
            oDB = Nothing

            Return dtIM

        Catch ex As Exception
            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            If Not IsNothing(dtIM) Then
                dtIM.Dispose()
                dtIM = Nothing
            End If
        Finally
            If Not IsNothing(dtIM) Then
                dtIM.Dispose()
                dtIM = Nothing
            End If
        End Try



        'Dim cmd As SqlCommand
        'Dim sqlParam As SqlParameter
        'Dim DocIDParameter As SqlParameter
        'Dim Con As SqlConnection = New SqlConnection(GetConnectionString())
        'Try
        '    cmd = New SqlCommand("gsp_GetAssociatedVisDocumentID", Con)
        '    cmd.CommandType = CommandType.StoredProcedure
        '    Con.Open()

        '    DocIDParameter = cmd.Parameters.Add("@DocID", SqlDbType.BigInt)
        '    DocIDParameter.Direction = ParameterDirection.InputOutput
        '    DocIDParameter.Value = 0

        '    'Visit ID
        '    sqlParam = cmd.Parameters.Add("@Vaccine_Name", SqlDbType.Text)
        '    sqlParam.Direction = ParameterDirection.Input
        '    sqlParam.Value = Vaccine_Name

        '    sqlParam = cmd.Parameters.Add("@LotNumber", SqlDbType.Text)
        '    sqlParam.Direction = ParameterDirection.Input
        '    sqlParam.Value = LotNumber

        '    sqlParam = cmd.Parameters.Add("@sTradeName", SqlDbType.Text)
        '    sqlParam.Direction = ParameterDirection.Input
        '    sqlParam.Value = sTradeName

        '    'patient Id
        '    sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
        '    sqlParam.Direction = ParameterDirection.Input
        '    sqlParam.Value = nPatinetID

        '    cmd.ExecuteNonQuery()
        '    Dim AssociatedVisDocumentID = DocIDParameter.Value
        '    Return AssociatedVisDocumentID

        'Catch ex As SqlException
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'Finally
        '    Con.Close()
        '    If Not IsNothing(sqlParam) Then 'Obj Disposed by Mitesh
        '        sqlParam = Nothing
        '    End If
        '    If Not IsNothing(DocIDParameter) Then
        '        DocIDParameter = Nothing
        '    End If
        '    If Not IsNothing(cmd) Then
        '        cmd.Dispose()
        '        cmd = Nothing
        '    End If
        'End Try
    End Function

    Private Sub optAdministered_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAdministered.CheckedChanged
        If optAdministered.Checked = True Then
            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If
        End If
    End Sub

    Private Sub optReported_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReported.CheckedChanged
        If optReported.Checked = True Then
            If chkPatientHasReaction.Checked = True Then
                grpReaction.Enabled = True
            Else
                grpReaction.Enabled = False
            End If
        End If
    End Sub

    Private Sub cmbLotNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLotNumber.TextChanged
        Try
            SetExpiryDate()
            SetVISDocumentDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetVISDocumentDetails()
        Dim dtIM As New DataTable
        dtIM = GetAssociatedVisDocumentID(_PatientID, txtCvx.Text.Trim(), cmbLotNumber.Text, txt_TradeName.Text.Trim())
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
            Exit Sub
        End If
        dtIM.Dispose()
        dtIM = Nothing
    End Sub

    Public Function GetAlreadyExistDocumentID(ByVal nPatinetID As Int64, ByVal MasterDocumentID As Int64) As Int64
        Dim cmd As SqlCommand
        Dim sqlParam As SqlParameter
        Dim DocIDParameter As SqlParameter
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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Private Sub txtNDCcode_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNDCcode.KeyPress
        AllowNumericValue(txtNDCcode.Text, e)
    End Sub

    Private Sub txt_vis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_vis.TextChanged
        'isSaved = False
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

    Public Function AddCategory(ByVal CategoryName As String, ByVal Caption As String)
        Dim frm As New CategoryMaster(CategoryName)
        Try
            frm.Text = Caption
            frm.ShowDialog()
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            frm = Nothing
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
    End Function

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

            ofrmSKUSearch.ShowDialog()
            If (ofrmSKUSearch.DialogResult = Windows.Forms.DialogResult.OK) Then
                If ofrmSKUSearch.nLocationID <> "" Then
                    cmbLocation.SelectedValue = ofrmSKUSearch.nLocationID
                End If

                cmbSKU.SelectedIndex = -1
                cmbSKU.SelectedIndex = cmbSKU.FindStringExact(ofrmSKUSearch.SKU)



                _isLoadGridCvxControl = True
                txtCvx.Text = ofrmSKUSearch.Vaccine.ToString()
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

    Private Sub txtHospitalizationDays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHospitalizationDays.KeyPress
        AllowNumericValue(txtHospitalizationDays.Text, e)
    End Sub

    Private Function GetIMLocation() As Decimal

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dt As New DataTable
        Dim strSQL As String
        Dim Locationid As Decimal


        Try
            strSQL = " Select dbo.GetIMLocationID ( " & _PatientID & " ) "
            dt = GetList(strSQL)

            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dt)

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            If dt.Rows.Count > 0 Then
                Locationid = dt.Rows(0)(0)
            End If

            Return Locationid

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function


    Private Function GetSKUList() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dt As New DataTable
        Dim strSQL As String
        Dim Locationid As Long

        Try

            Locationid = cmbLocation.SelectedValue

            strSQL = " select distinct im_sSKU from IM_MST where im_sActive = 'Active' and im_nLocationID = " & Locationid.ToString & " order by im_sSKU "

            dt = GetList(strSQL)

            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dt)

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            Return dt

        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function


    Private Sub FillSKUList()
        'Get SKU List
        RemoveHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged
        Dim str As String
        str = cmbSKU.Text
        Dim dtsku As New DataTable
        dtsku = GetSKUList()
        cmbSKU.DataSource = dtsku
        cmbSKU.ValueMember = "im_sSKU"
        cmbSKU.DisplayMember = "im_sSKU"
        cmbSKU.Text = str
        AddHandler cmbSKU.SelectedIndexChanged, AddressOf cmbSKU_SelectedIndexChanged
    End Sub

    Dim combo As New ComboBox
    Dim tooltip As New ToolTip


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


    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer
        Dim g As Graphics = Me.CreateGraphics()
        Dim s As SizeF = g.MeasureString(_text, combo.Font)
        Dim width As Integer = Convert.ToInt32(s.Width)
        Return width
    End Function


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

End Class
