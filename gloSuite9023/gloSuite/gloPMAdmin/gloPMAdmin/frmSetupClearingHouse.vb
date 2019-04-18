Imports gloCommon

Partial Public Class frmSetupClearingHouse
    Inherits Form
#Region "Variable Declaration"

    Private _messageBoxCaption As String = "gloPM"
    Private _databaseconnectionstring As String = ""
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Private _ClinicID As Int64 = 1
    Private _nClearingHouseID As Int64 = 0
    Private Const _encryptionKey As String = "12345678"
    Private _IsNew As Boolean = False
    Private _numRows As Integer = 0
    Private isSaveAndClose As Boolean = False
    Private _IsMultipleClearingHouse As Boolean = False
    Private _IsDefaultsettingforBatchEligibility As Boolean = False

    Public Property nClearingHouseID() As Int64
        Get
            Return _nClearingHouseID
        End Get
        Set(ByVal value As Int64)
            _nClearingHouseID = value
        End Set
    End Property




#End Region

#Region "Contructor"

    Public Sub New(ByVal DatabaseConnectionString As String, ByVal numRows As Integer)
        InitializeComponent()

        _IsNew = True
        _numRows = numRows
        _databaseconnectionstring = DatabaseConnectionString
        _nClearingHouseID = 0

        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _ClinicID = 0
            End If
        Else
            _ClinicID = 0
        End If
    End Sub

    Public Sub New(ByVal DatabaseConnectionString As String, ByVal ClearingHouseID As Int64, ByVal numRows As Integer)
        InitializeComponent()

        _databaseconnectionstring = DatabaseConnectionString
        _nClearingHouseID = ClearingHouseID

        _IsNew = False
        cmbClearingHouse.Enabled = False

        _numRows = numRows

        ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
        ''To Show Tooltip
        cmbRInterchangeIDQualifier.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbRInterchangeIDQualifier.DrawItem, AddressOf ShowTooltipOnComboBox

        cmbSInterchangeIDQualifier.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbSInterchangeIDQualifier.DrawItem, AddressOf ShowTooltipOnComboBox

        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _ClinicID = 0
            End If
        Else
            _ClinicID = 0

        End If
    End Sub

#End Region

#Region " Form Load/Close Event"
    Dim _OrginalClearingHouseType As Integer = 0
    Private Sub frmSetupClearingHouse_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        txtNameofReceiver.Focus()

        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing

        RemoveHandler cmbClearingHouse.SelectedIndexChanged, AddressOf cmbClearingHouse_SelectedIndexChanged

        _IsMultipleClearingHouse = IsMultipleClearingHouse()
        If _IsMultipleClearingHouse = True Then
            chkIsDefault.Visible = True
            lblDefault.Visible = True
        Else
            chkIsDefault.Visible = False
            lblDefault.Visible = False
        End If


        FillControls()
        FillClearingHouse()
        ''7022Items:.STA extension for GatewayEDI statements
        ''Call method to fill combobox.
        FillStatementExtensions()

        ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
        FillSenderQualifier()
        FillReceiverQualifier()
        If _nClearingHouseID > 0 Then
            LoadClearingHouse()
        Else
            'setUpDefault()
            setUpDefaultFromDatabase()
        End If

        If _numRows = 0 Then
            chkIsDefault.Checked = True
        End If

        If chkIsDefault.Checked = True And _IsMultipleClearingHouse = True Then
            lblDefault.Visible = True
        Else
            lblDefault.Visible = False
        End If

        AddHandler cmbClearingHouse.SelectedIndexChanged, AddressOf cmbClearingHouse_SelectedIndexChanged

    End Sub


    Private Sub frmSetupClearingHouse_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        CloseForm()
    End Sub

    Private Sub CloseForm()
        Try
            RemoveHandler frmSetupClearingHouse.FormClosing, AddressOf frmSetupClearingHouse_FormClosing
            Dim _dlgRes As System.Windows.Forms.DialogResult
            If isSaveAndClose = False Then
                _dlgRes = MessageBox.Show("Do you want to save changes to this record? ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                If _dlgRes = Windows.Forms.DialogResult.Yes Then
                    If ValidateData() = True Then
                        If SaveData() = True Then
                            Me.Close()
                        End If
                    End If
                ElseIf _dlgRes = Windows.Forms.DialogResult.No Then
                    Me.Close()
                End If
            End If
        Catch ex As Exception

        End Try
        AddHandler cmbClearingHouse.SelectedIndexChanged, AddressOf cmbClearingHouse_SelectedIndexChanged
    End Sub
#End Region

#Region "Toolstrip Buttons"

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ts_btnClose.Click
        isSaveAndClose = False
        CloseForm()
        ''Me.Close()
    End Sub

    Private Sub ts_btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ts_btnSave.Click
        Try
            If ValidateData() = True Then
                If SaveData() = True Then
                    isSaveAndClose = True
                    Me.Close()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Save / Fill Methods"

    Private Sub LoadClearingHouse()
        Dim oClearingHouse As New ClearingHouse(_databaseconnectionstring)
        Dim oClsEncryption As New clsEncryption()
        Try
            oClearingHouse = oClearingHouse.GetClearingHouse(_nClearingHouseID)

            If oClearingHouse IsNot Nothing Then
                _nClearingHouseID = oClearingHouse.ClearingHouseID
                txtName.Text = oClearingHouse.ClearingHouseName
                txtNameofReceiver.Text = oClearingHouse.RecieverName
                txtReceiverID.Text = oClearingHouse.RecieverID
                txtSubmitterID.Text = oClearingHouse.SubmitterID
                chk1JQulifier.Checked = oClearingHouse.IsOneJQualifier
                txt1JQulifier.Text = oClearingHouse.OneJQualifier
                chkSenderCode.Checked = oClearingHouse.IsSenderCode
                txtSenderCode.Text = oClearingHouse.SenderCode
                chkVenderCode.Checked = oClearingHouse.IsVenderID
                txtVenderCode.Text = oClearingHouse.VenderID
                chkLoop1000BNM109.Checked = oClearingHouse.IsLoop1000B
                txtLoop1000BNM109.Text = oClearingHouse.Loop1000B
                chkIsDefault.Checked = oClearingHouse.IsDefault
                _IsDefaultsettingforBatchEligibility = oClearingHouse.IsDefault
                cmbClearingHouse.SelectedValue = oClearingHouse.ClearingHouseType
                _OrginalClearingHouseType = oClearingHouse.ClearingHouseType.GetHashCode()
                txtEligibilityUserName.Text = Convert.ToString(oClearingHouse.EligibilityUserName)
                txtEligibilityPassword.Text = oClsEncryption.DecryptFromBase64String(oClearingHouse.EligibilityPassword, _encryptionKey)
                txtEligibilityUrl.Text = oClearingHouse.EligibilityURL
                txtRealtimeClaimUserName.Text = Convert.ToString(oClearingHouse.RealTimeClaimUserName)
                txtRealtimeClaimPassword.Text = oClsEncryption.DecryptFromBase64String(oClearingHouse.RealTimeClaimPassword, _encryptionKey)
                txtRealtimeClaimUrl.Text = oClearingHouse.RealTimeClaimURL
                If oClearingHouse.ISEnableRealTimeClaimStatus = True Then
                    ChkEnableRealtimeClaimStatus.Checked = True
                Else
                    ChkEnableRealtimeClaimStatus.Checked = False
                End If
                ChkEnableRealtimeClaimStatus_CheckedChanged(New Object, New EventArgs)

                Select Case oClearingHouse.TypeOfData
                    Case TypeOfData.None
                        cmbTypeofData.SelectedIndex = -1
                        Exit Select
                    Case TypeOfData.TestData
                        cmbTypeofData.Text = "Test Data"
                        Exit Select
                    Case TypeOfData.ProductionData
                        cmbTypeofData.Text = "Production Data"
                        Exit Select
                    Case TypeOfData.Blank
                        cmbTypeofData.Text = ""
                        Exit Select
                    Case Else
                        Exit Select
                End Select
                cmbTypeofData.Refresh()

                chkISA.Checked = oClearingHouse.IsISA
                ''7022Items:.STA extension for GatewayEDI statements
                ''Assign value from database to combobox for selected clearing house.
                cmbStatementFileExtension.Text = oClearingHouse.StatementFileExtension

                ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                cmbRInterchangeIDQualifier.SelectedValue = oClearingHouse.ReceiverInterchangeIDQualifier
                cmbSInterchangeIDQualifier.SelectedValue = oClearingHouse.SenderInterchangeIDQualifier


                '#Region "Load Detail" 

                txt_ftpURL.Text = oClearingHouse.URL
                txt_Username.Text = oClearingHouse.UserName
                txt_Password.Text = oClsEncryption.DecryptFromBase64String(oClearingHouse.Password, _encryptionKey)
                txt_271EligibilityResponse.Text = oClearingHouse.In_271_ElgibilityResponse
                txt_276Eligibilityenquiry.Text = oClearingHouse.Out_276_ElgibilityEnquiry
                txt_277ClaimStatusResponse.Text = oClearingHouse.In_277_ClaimStatus
                txt_835RemittanceAdvice.Text = oClearingHouse.In_835_Remitance
                txt_837PclaimSubmission.Text = oClearingHouse.Out_837P_ClaimSubmition
                txt_997INAcknowledgement.Text = oClearingHouse.In_997_Acknowledge
                txt_997OUTAcknowledgement.Text = oClearingHouse.Out_997_Acknowledge
                txt_CSRReports.Text = oClearingHouse.Gen_CSRReports
                txt_Letters.Text = oClearingHouse.Gen_Letters
                txt_Reports.Text = oClearingHouse.Gen_Reports
                txt_Statements.Text = oClearingHouse.Gen_Statements
                ChkEnableBatchEligibilty.Checked = oClearingHouse.ISEbnableBatchEligibilty
                '#End Region 
                txt_WorkedTransactions.Text = oClearingHouse.Gen_WorkedTrans
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillControls()
        Try
            cmbTypeofData.Items.Clear()
            cmbTypeofData.Items.Add("")
            cmbTypeofData.Items.Add("Test Data")
            cmbTypeofData.Items.Add("Production Data")
            ''cmbTypeofData.Items.Add("Blank")

            cmbTypeofData.Refresh()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillClearingHouse()
        cmbClearingHouse.Items.Clear()

        Dim dtClearingHouse As New DataTable("ClearingHouse")
        dtClearingHouse.Columns.Add("nID")
        dtClearingHouse.Columns.Add("sDescription")
        dtClearingHouse.AcceptChanges()

        Dim dr As DataRow = dtClearingHouse.NewRow()
        dr("nID") = 0
        dr("sDescription") = ""
        dtClearingHouse.Rows.Add(dr)
        dtClearingHouse.AcceptChanges()

        dr = dtClearingHouse.NewRow()
        dr("nID") = 1
        dr("sDescription") = "GatewayEDI"
        dtClearingHouse.Rows.Add(dr)
        dtClearingHouse.AcceptChanges()

        dr = dtClearingHouse.NewRow()
        dr("nID") = 2
        dr("sDescription") = "RealMed"
        dtClearingHouse.Rows.Add(dr)
        dtClearingHouse.AcceptChanges()


        dr = dtClearingHouse.NewRow()
        dr("nID") = 3
        dr("sDescription") = "Other"
        dtClearingHouse.Rows.Add(dr)
        dtClearingHouse.AcceptChanges()

        cmbClearingHouse.DataSource = dtClearingHouse
        cmbClearingHouse.DisplayMember = "sDescription"
        cmbClearingHouse.ValueMember = "nID"
        cmbClearingHouse.Refresh()

        cmbClearingHouse.SelectedValue = 0
    End Sub


    Private Function SaveData() As Boolean
        Dim oClearingHouse As New ClearingHouse(_databaseconnectionstring)
        Dim oClsEncryption As New clsEncryption()
        Dim _result As Boolean = False
        Try
            oClearingHouse.ClearingHouseID = _nClearingHouseID
            oClearingHouse.ClearingHouseName = txtName.Text
            oClearingHouse.RecieverName = txtNameofReceiver.Text
            oClearingHouse.RecieverID = txtReceiverID.Text
            oClearingHouse.SubmitterID = txtSubmitterID.Text

            If chk1JQulifier.Checked = True Then
                oClearingHouse.IsOneJQualifier = True
                oClearingHouse.OneJQualifier = txt1JQulifier.Text
            Else
                oClearingHouse.IsOneJQualifier = False
                oClearingHouse.OneJQualifier = ""
            End If

            If chkSenderCode.Checked = True Then
                oClearingHouse.IsSenderCode = True
                oClearingHouse.SenderCode = txtSenderCode.Text
            Else
                oClearingHouse.IsSenderCode = False
                oClearingHouse.SenderCode = ""
            End If

            If chkVenderCode.Checked = True Then
                oClearingHouse.IsVenderID = True
                oClearingHouse.VenderID = txtVenderCode.Text
            Else
                oClearingHouse.IsVenderID = False
                oClearingHouse.VenderID = ""
            End If

            If chkLoop1000BNM109.Checked = True Then
                oClearingHouse.IsLoop1000B = True
                oClearingHouse.Loop1000B = txtLoop1000BNM109.Text
            Else
                oClearingHouse.IsLoop1000B = False
                oClearingHouse.Loop1000B = ""
            End If

            Select Case cmbTypeofData.Text.Trim()
                Case "Test Data"
                    oClearingHouse.TypeOfData = TypeOfData.TestData
                    Exit Select
                Case "Production Data"
                    oClearingHouse.TypeOfData = TypeOfData.ProductionData
                    Exit Select
                Case "Blank"
                    oClearingHouse.TypeOfData = TypeOfData.None
                    Exit Select
                Case Else
                    oClearingHouse.TypeOfData = TypeOfData.None
                    Exit Select
            End Select

            oClearingHouse.IsISA = chkISA.Checked
            oClearingHouse.ClinicID = _ClinicID

            '#Region "set detail fields" 

            oClearingHouse.ClearingHouseCode = ""
            oClearingHouse.FolderCategory = 0
            'none 
            oClearingHouse.Gen_CSRReports = txt_CSRReports.Text.Trim()
            oClearingHouse.Gen_Letters = txt_Letters.Text.Trim()
            oClearingHouse.Gen_Reports = txt_Reports.Text.Trim()
            oClearingHouse.Gen_Statements = txt_Statements.Text.Trim()
            oClearingHouse.Gen_WorkedTrans = txt_WorkedTransactions.Text.Trim()
            oClearingHouse.In_271_ElgibilityResponse = txt_271EligibilityResponse.Text.Trim()
            oClearingHouse.In_277_ClaimStatus = txt_277ClaimStatusResponse.Text.Trim()
            oClearingHouse.In_835_Remitance = txt_835RemittanceAdvice.Text.Trim()
            oClearingHouse.In_997_Acknowledge = txt_997INAcknowledgement.Text.Trim()
            oClearingHouse.Out_276_ElgibilityEnquiry = txt_276Eligibilityenquiry.Text.Trim()
            oClearingHouse.Out_837P_ClaimSubmition = txt_837PclaimSubmission.Text.Trim()
            oClearingHouse.Out_997_Acknowledge = txt_997OUTAcknowledgement.Text.Trim()
            oClearingHouse.Password = oClsEncryption.EncryptToBase64String(txt_Password.Text.Trim(), _encryptionKey)
            oClearingHouse.UserName = txt_Username.Text.Trim()
            oClearingHouse.URL = txt_ftpURL.Text.Trim()
            oClearingHouse.IsDefault = chkIsDefault.Checked
            oClearingHouse.ClearingHouseType = cmbClearingHouse.SelectedValue
            oClearingHouse.EligibilityUserName = txtEligibilityUserName.Text.Trim()
            oClearingHouse.EligibilityPassword = oClsEncryption.EncryptToBase64String(txtEligibilityPassword.Text.Trim(), _encryptionKey)
            oClearingHouse.EligibilityURL = txtEligibilityUrl.Text.Trim()
            If ChkEnableBatchEligibilty.Checked = True And Not IsEligVersion5010() Then
                MessageBox.Show("To use Batch Eligibility, the Admin setting for Eligibility ANSI  version needs to be set to 5010.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False

            Else
                oClearingHouse.ISEbnableBatchEligibilty = ChkEnableBatchEligibilty.Checked
            End If
            ''7022Items:.STA extension for GatewayEDI statements
            ''Pass file extension from combo-box for save.
            oClearingHouse.StatementFileExtension = cmbStatementFileExtension.Text

            ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            If cmbRInterchangeIDQualifier.Text = "" Then
                oClearingHouse.ReceiverInterchangeIDQualifier = ""
            Else
                oClearingHouse.ReceiverInterchangeIDQualifier = cmbRInterchangeIDQualifier.SelectedValue
            End If

            ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            If cmbSInterchangeIDQualifier.Text = "" Then
                oClearingHouse.SenderInterchangeIDQualifier = ""
            Else
                oClearingHouse.SenderInterchangeIDQualifier = cmbSInterchangeIDQualifier.SelectedValue
            End If

            '#End Region 

            ''RealTimeClaimStauts Settings
            If ChkEnableRealtimeClaimStatus.Checked Then
                If txtRealtimeClaimUrl.Text.Trim() = "" Or txtRealtimeClaimUrl.Text.Trim() Is Nothing Then
                    MessageBox.Show("Enter Url For ClaimStatus", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                ElseIf txtRealtimeClaimUserName.Text.Trim() = "" Or txtRealtimeClaimUserName.Text.Trim() Is Nothing Then
                    MessageBox.Show("Enter UserName For ClaimStatus", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                    End
                ElseIf txtRealtimeClaimPassword.Text.Trim() = "" Or txtRealtimeClaimPassword.Text.Trim() Is Nothing Then
                    MessageBox.Show("Enter Password For ClaimStatus", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            End If


            oClearingHouse.RealTimeClaimUserName = txtRealtimeClaimUserName.Text.Trim()
            oClearingHouse.RealTimeClaimPassword = oClsEncryption.EncryptToBase64String(txtRealtimeClaimPassword.Text.Trim(), _encryptionKey)
            oClearingHouse.RealTimeClaimURL = txtRealtimeClaimUrl.Text.Trim()
            If ChkEnableRealtimeClaimStatus.CheckState.Checked = CheckState.Unchecked Then
                oClearingHouse.ISEnableRealTimeClaimStatus = False
            Else
                oClearingHouse.ISEnableRealTimeClaimStatus = ChkEnableRealtimeClaimStatus.Checked
            End If


            Dim _nTempID As Int64 = oClearingHouse.Add(oClearingHouse)
            _nClearingHouseID = _nTempID

            If _nTempID > 0 Then
                If _IsDefaultsettingforBatchEligibility <> True Then
                    oClearingHouse.SetDefaultSettingforBatchEligibility(_nClearingHouseID)
                End If
                _result = True
            Else


                _result = False

            End If
        Catch ex As Exception

            _result = False
        End Try
        Return _result
    End Function

    Private Function ValidateData() As Boolean


        If cmbClearingHouse.SelectedIndex >= 0 Then

            Dim oClearingHouse As New ClearingHouse(_databaseconnectionstring)
            Try
                If cmbClearingHouse.Text.ToUpper() <> "OTHER" Then

                    If oClearingHouse.IsClearingHouseTypeExists(cmbClearingHouse.SelectedValue) = True And _OrginalClearingHouseType <> cmbClearingHouse.SelectedValue Then
                        MessageBox.Show("This clearinghouse house already exists.  Please modify the existing clearinghouse.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                End If
            Catch objErr As Exception
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

        If txtName.Text.Trim() = "" Then
            MessageBox.Show("Plaese enter the clearinghouse name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtName.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub setUpDefault()
        Dim _ClearingHouseType As Integer
        _ClearingHouseType = Convert.ToInt32(cmbClearingHouse.SelectedValue)

        Select Case _ClearingHouseType
            Case 1
                txtName.Text = "GatewayEDI"
                txtNameofReceiver.Text = "gloClinic"
                txtReceiverID.Text = "431420764000000"
                txtSubmitterID.Text = ""
                txtVenderCode.Text = "431420764"
                chkVenderCode.Checked = False

                txtSenderCode.Text = ""
                txt_271EligibilityResponse.Text = "eligibilityresponses"
                txt_277ClaimStatusResponse.Text = "277"
                txt_835RemittanceAdvice.Text = "remits"
                txt_997INAcknowledgement.Text = "997"
                txt_276Eligibilityenquiry.Text = "eligibilityrequest"
                txt_837PclaimSubmission.Text = "claims"
                txt_997OUTAcknowledgement.Text = "997"
                txt_CSRReports.Text = ""
                txt_Letters.Text = ""
                txt_Reports.Text = ""
                txt_Statements.Text = ""
                txt_WorkedTransactions.Text = ""

                txt_ftpURL.Text = "ftp.gatewayedi.com"
                txt_Username.Text = ""
                txt_Password.Text = ""

                txtEligibilityUserName.Text = ""
                txtEligibilityPassword.Text = ""
                Exit Select
            Case 2
                txtName.Text = "RealMed"
                txtNameofReceiver.Text = "gloClinic"
                txtReceiverID.Text = "REALMED"
                txtSubmitterID.Text = ""
                chkVenderCode.Checked = False
                txtVenderCode.Text = "35-2091331"

                txtSenderCode.Text = ""
                chkVenderCode.Checked = False
                txt_271EligibilityResponse.Text = "/ELIG/OUTPUT"
                txt_277ClaimStatusResponse.Text = "/STATUS/OUTPUT"
                txt_835RemittanceAdvice.Text = "/ERA/OUTPUT"
                txt_997INAcknowledgement.Text = "/CLAIMS/OUTPUT"
                txt_276Eligibilityenquiry.Text = "/STATUS/INPUT"
                txt_837PclaimSubmission.Text = "/CLAIMS/INPUT"
                txt_997OUTAcknowledgement.Text = ""
                txt_CSRReports.Text = ""
                txt_Letters.Text = ""
                txt_Reports.Text = ""
                txt_Statements.Text = "/PTST/INPUT"
                txt_WorkedTransactions.Text = ""

                txt_ftpURL.Text = "prodftp.realmed.com"
                txt_Username.Text = ""
                txt_Password.Text = ""

                txtEligibilityUserName.Text = ""
                txtEligibilityPassword.Text = ""
                Exit Select
            Case 3
                txtName.Text = ""
                txtNameofReceiver.Text = ""
                txtReceiverID.Text = ""
                txtSubmitterID.Text = ""
                txtVenderCode.Text = ""

                txtSenderCode.Text = ""
                txt_271EligibilityResponse.Text = ""
                txt_277ClaimStatusResponse.Text = ""
                txt_835RemittanceAdvice.Text = ""
                txt_997INAcknowledgement.Text = ""
                txt_276Eligibilityenquiry.Text = ""
                txt_837PclaimSubmission.Text = ""
                txt_997OUTAcknowledgement.Text = ""
                txt_CSRReports.Text = ""
                txt_Letters.Text = ""
                txt_Reports.Text = ""
                txt_Statements.Text = ""
                txt_WorkedTransactions.Text = ""

                txt_ftpURL.Text = ""
                txt_Username.Text = ""
                txt_Password.Text = ""

                txtEligibilityUserName.Text = ""
                txtEligibilityPassword.Text = ""

                chkSenderCode.Checked = False
                chkVenderCode.Checked = False
                Exit Select
            Case Else
                Exit Select
        End Select
    End Sub

    Private Sub setUpDefaultFromDatabase()

        Dim _ClearingHouseType As Integer
        _ClearingHouseType = Convert.ToInt32(cmbClearingHouse.SelectedValue)

        txtName.Text = ""
        txtNameofReceiver.Text = ""
        txtReceiverID.Text = ""
        txtSubmitterID.Text = ""
        txtVenderCode.Text = ""

        txtSenderCode.Text = ""
        txt_271EligibilityResponse.Text = ""
        txt_277ClaimStatusResponse.Text = ""
        txt_835RemittanceAdvice.Text = ""
        txt_997INAcknowledgement.Text = ""
        txt_276Eligibilityenquiry.Text = ""
        txt_837PclaimSubmission.Text = ""
        txt_997OUTAcknowledgement.Text = ""
        txt_CSRReports.Text = ""
        txt_Letters.Text = ""
        txt_Reports.Text = ""
        txt_Statements.Text = ""
        txt_WorkedTransactions.Text = ""

        txt_ftpURL.Text = ""
        txt_Username.Text = ""
        txt_Password.Text = ""

        txtEligibilityUserName.Text = ""
        txtEligibilityPassword.Text = ""

        chkSenderCode.Checked = False
        chkVenderCode.Checked = False

        If (_ClearingHouseType = 1 Or _ClearingHouseType = 2) Then

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            Dim dtClearingHouseSetting As New DataTable()
            Try

                Dim _sqlQuery As String = ""

                oDB.Connect(False)

                _sqlQuery = ("SELECT ISNULL(sDefaultField,'') as sDefaultField,ISNULL(sDefaultValue,'') as sDefaultValue FROM BL_ClearingHouse_Default WHERE nClearingHouseType = " & _ClearingHouseType & "")
                oDB.Retrive_Query(_sqlQuery, dtClearingHouseSetting)

            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDB.Dispose()
            End Try

            If (Not dtClearingHouseSetting Is Nothing) Then
                If (dtClearingHouseSetting.Rows.Count > 0) Then

                    txtName.Text = GetValue(dtClearingHouseSetting, "Name")
                    txtNameofReceiver.Text = GetValue(dtClearingHouseSetting, "NameofReceiver")
                    txtReceiverID.Text = GetValue(dtClearingHouseSetting, "ReceiverID")
                    txtSubmitterID.Text = GetValue(dtClearingHouseSetting, "SubmitterID")
                    If GetValue(dtClearingHouseSetting, "IsVenderCode") = 1 Then
                        chkVenderCode.Checked = True
                    End If
                    txtVenderCode.Text = GetValue(dtClearingHouseSetting, "VenderCode")
                    If GetValue(dtClearingHouseSetting, "IsSenderCode") = 1 Then
                        chkSenderCode.Checked = True
                    End If
                    txtSenderCode.Text = GetValue(dtClearingHouseSetting, "SenderCode")
                    txt_271EligibilityResponse.Text = GetValue(dtClearingHouseSetting, "271EligibilityResponse")
                    txt_277ClaimStatusResponse.Text = GetValue(dtClearingHouseSetting, "277ClaimStatusResponse")
                    txt_835RemittanceAdvice.Text = GetValue(dtClearingHouseSetting, "835RemittanceAdvice")
                    txt_997INAcknowledgement.Text = GetValue(dtClearingHouseSetting, "997INAcknowledgement")
                    txt_276Eligibilityenquiry.Text = GetValue(dtClearingHouseSetting, "276Eligibilityenquiry")
                    txt_837PclaimSubmission.Text = GetValue(dtClearingHouseSetting, "837PclaimSubmission")
                    txt_997OUTAcknowledgement.Text = GetValue(dtClearingHouseSetting, "997OUTAcknowledgement")
                    txt_CSRReports.Text = GetValue(dtClearingHouseSetting, "CSRReports")
                    txt_Letters.Text = GetValue(dtClearingHouseSetting, "Letters")
                    txt_Reports.Text = GetValue(dtClearingHouseSetting, "Reports")
                    txt_Statements.Text = GetValue(dtClearingHouseSetting, "Statements")
                    txt_WorkedTransactions.Text = GetValue(dtClearingHouseSetting, "WorkedTransactions")
                    txt_ftpURL.Text = GetValue(dtClearingHouseSetting, "ftpURL")
                    txtEligibilityUrl.Text = GetValue(dtClearingHouseSetting, "EligibilityUrl")

                    ''Bug #46004: gloPM Admin >> Clearinghouse >> Application does not set Statement file Extension default value for newly added clearing house.
                    ''Assign default value to newly added control.
                    Dim val As String = String.Empty

                    val = GetValue(dtClearingHouseSetting, "StatementFileExention")
                    If (String.IsNullOrEmpty(val) = False) Then
                        cmbStatementFileExtension.Text = val
                    End If

                    val = GetValue(dtClearingHouseSetting, "ReceiverIDQualifier")
                    If (String.IsNullOrEmpty(val) = False) Then
                        cmbRInterchangeIDQualifier.SelectedValue = val
                    End If

                    val = GetValue(dtClearingHouseSetting, "SenderIDQualifier")
                    If (String.IsNullOrEmpty(val) = False) Then
                        cmbSInterchangeIDQualifier.SelectedValue = val
                    End If

                End If
            End If
        End If
    End Sub

    Private Function GetValue(ByVal dtClearingHouseSetting As DataTable, ByVal FieldName As String) As String
        Dim _result As String = ""
        Dim _dr As DataRow()
        Try
            _dr = dtClearingHouseSetting.Select("sDefaultField='" & FieldName & "'")
            If (_dr.GetUpperBound(0) >= 0) Then
                _result = _dr(0)("sDefaultValue").ToString()
            End If
        Catch ex As Exception
            _result = ""
        End Try
        Return _result
    End Function

    Private Function IsDefaultAlreadyFilled() As Boolean
        Dim _result As Boolean
        _result = True
        If txtName.Text = "" And txtNameofReceiver.Text = "" And txtReceiverID.Text = "" And txt_271EligibilityResponse.Text = "" And txt_277ClaimStatusResponse.Text = "" And txt_835RemittanceAdvice.Text = "" And txt_997INAcknowledgement.Text = "" And txt_276Eligibilityenquiry.Text = "" And txt_837PclaimSubmission.Text = "" And txt_Statements.Text = "" And txt_ftpURL.Text = "" Then
            _result = False
        End If
        Return _result
    End Function

    Private Function IsMultipleClearingHouse() As Boolean
        Dim _Isenable As Boolean = False
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim _sqlQuery As String = ""
        Dim oMultipleClearingHouse As New Object()

        Try
            oDB = New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            oDB.Connect(False)
            _sqlQuery = " select sSettingsValue from Settings WITH (NOLOCK) where  sSettingsName='ISMULTIPLECLEARINGHOUSE' and nClinicID=" & _ClinicID & ""

            oMultipleClearingHouse = oDB.ExecuteScalar_Query(_sqlQuery)
            If oMultipleClearingHouse IsNot Nothing AndAlso Convert.ToString(oMultipleClearingHouse) <> "" Then
                If Convert.ToString(oMultipleClearingHouse).ToUpper() = "1" Then
                    _Isenable = True
                Else
                    _Isenable = False
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            _Isenable = False
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()

            End If
        End Try

        Return _Isenable

    End Function
    Private Function IsEligVersion5010() As Boolean
        Dim _Isenable As Boolean = False
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim _sqlQuery As String = ""
        Dim oEligVersionVersion As New Object()

        Try
            oDB = New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            oDB.Connect(False)
            _sqlQuery = "SELECT ANSI.sEligVersion FROM  BL_ANSIVersion ANSI WITH(NOLOCK) WHERE ANSI.nContactID = 0"

            oEligVersionVersion = oDB.ExecuteScalar_Query(_sqlQuery)
            If oEligVersionVersion IsNot Nothing AndAlso Convert.ToString(oEligVersionVersion) <> "" Then
                If Convert.ToString(oEligVersionVersion).ToUpper() = "ANSI 5010" Then
                    _Isenable = True
                Else
                    _Isenable = False
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            _Isenable = False
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()

            End If
        End Try

        Return _Isenable
    End Function
#End Region

#Region "Form Control Events"

    Private Sub chk1JQulifier_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chk1JQulifier.CheckStateChanged
        txt1JQulifier.Enabled = chk1JQulifier.Checked
    End Sub

    Private Sub chkSenderCode_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSenderCode.CheckStateChanged
        txtSenderCode.Enabled = chkSenderCode.Checked
    End Sub

    Private Sub chkVenderCode_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkVenderCode.CheckStateChanged
        txtVenderCode.Enabled = chkVenderCode.Checked
    End Sub

    Private Sub chkLoop1000BNM109_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLoop1000BNM109.CheckStateChanged
        txtLoop1000BNM109.Enabled = chkLoop1000BNM109.Checked
    End Sub

    Private Sub chkISA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkISA.CheckStateChanged

    End Sub

    Private Sub cmbClearingHouse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClearingHouse.SelectedIndexChanged

        If cmbClearingHouse.Text.ToUpper() = "OTHER" Then
            txtName.Enabled = True
        Else
            txtName.Enabled = False
        End If


    End Sub

    Private Sub cmbClearingHouse_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbClearingHouse.Validating

        If cmbClearingHouse.SelectedIndex >= 0 Then

            Dim oClearingHouse As New ClearingHouse(_databaseconnectionstring)
            Try
                If cmbClearingHouse.Text.Trim().ToUpper() <> "OTHER" And cmbClearingHouse.Text.Trim() <> "" Then

                    If oClearingHouse.IsClearingHouseTypeExists(cmbClearingHouse.SelectedValue) = True Then
                        MessageBox.Show("This clearinghouse house already exists.  Please modify the existing clearinghouse.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        e.Cancel = True
                    Else

                        If IsDefaultAlreadyFilled() = True Then
                            If MessageBox.Show("Default values will replace current values. Are you sure you want to load the default values?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                                'setUpDefault()
                                setUpDefaultFromDatabase()
                            End If
                        Else
                            'setUpDefault()
                            setUpDefaultFromDatabase()
                        End If

                    End If
                    ''Bug #46004: gloPM Admin >> Clearinghouse >> Application does not set Statement file Extension default value for newly added clearing house.
                Else
                    cmbStatementFileExtension.Text = ".txt"
                End If
            Catch objErr As Exception
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

    End Sub

    Private Sub chkIsDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsDefault.CheckedChanged

        If chkIsDefault.Checked = True Then
            chkIsDefault.Visible = False
            lblDefault.Visible = True
        End If

    End Sub

#End Region
    ''7022Items:.STA extension for GatewayEDI statements
    ''Fill value in combo-box for Statement file Extension.
    Private Sub FillStatementExtensions()
        cmbStatementFileExtension.Items.Clear()
        cmbStatementFileExtension.Items.Add(".sta")
        cmbStatementFileExtension.Items.Add(".txt")
        cmbStatementFileExtension.Refresh()
    End Sub

    ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
    Private Sub FillSenderQualifier()
        cmbSInterchangeIDQualifier.Items.Clear()

        Dim dtSenderQualifier As New DataTable("SenderQualifier")
        dtSenderQualifier.Columns.Add("sCode")
        dtSenderQualifier.Columns.Add("sDefination")
        dtSenderQualifier.AcceptChanges()

        Dim dr As DataRow = dtSenderQualifier.NewRow()
        dr("sCode") = "00"
        dr("sDefination") = ""
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "01"
        dr("sDefination") = "01 - Duns (Dun & Bradstreet)"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "14"
        dr("sDefination") = "14 - Duns Plus Suffix"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        ''Bug #46006: gloPM Admin >> clearing house >>Sender Qualifier >> Application shows wrong qualifier code
        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "20"
        dr("sDefination") = "20 - Health Industry Number (HIN)"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "27"
        dr("sDefination") = "27 - Carrier Identification Number as assigned by Health Care Financing Administration (HCFA)"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        ''Bug #45998: gloPM Admin >> Clearing house >> Sender Qualifier >> Application shows wrong description for qualifier codes.
        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "28"
        dr("sDefination") = "28 - Fiscal Intermediary Identification Number as assigned by Health Care Financing Administration (HCFA)"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        ''Bug #45998: gloPM Admin >> Clearing house >> Sender Qualifier >> Application shows wrong description for qualifier codes.
        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "29"
        dr("sDefination") = "29 - Medicare Provider and Supplier Identification Number as assigned by Health Care Financing Administration (HCFA)"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "30"
        dr("sDefination") = "30 - U.S. Federal Tax Identification Number"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "33"
        dr("sDefination") = "33 - National Association of Insurance Commissioners Company Code (NAIC)"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        dr = dtSenderQualifier.NewRow()
        dr("sCode") = "ZZ"
        dr("sDefination") = "ZZ - Mutually Defined"
        dtSenderQualifier.Rows.Add(dr)
        dtSenderQualifier.AcceptChanges()

        cmbSInterchangeIDQualifier.DataSource = dtSenderQualifier
        cmbSInterchangeIDQualifier.DisplayMember = "sDefination"
        cmbSInterchangeIDQualifier.ValueMember = "sCode"
        cmbSInterchangeIDQualifier.Refresh()

        'cmbSInterchangeIDQualifier.SelectedValue = "00"
    End Sub

    ''7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
    Private Sub FillReceiverQualifier()
        cmbRInterchangeIDQualifier.Items.Clear()

        Dim dtReceiverQualifier As New DataTable("ReceiverQualifier")
        dtReceiverQualifier.Columns.Add("sCode")
        dtReceiverQualifier.Columns.Add("sDefination")
        dtReceiverQualifier.AcceptChanges()

        Dim dr As DataRow = dtReceiverQualifier.NewRow()
        dr("sCode") = "00"
        dr("sDefination") = ""
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "01"
        dr("sDefination") = "01 - Duns (Dun & Bradstreet)"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "14"
        dr("sDefination") = "14 - Duns Plus Suffix"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "20"
        dr("sDefination") = "20 - Health Industry Number (HIN)"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "27"
        dr("sDefination") = "27 - Carrier Identification Number as assigned by Health Care Financing Administration (HCFA)"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        ''Bug #45998: gloPM Admin >> Clearing house >> Sender Qualifier >> Application shows wrong description for qualifier codes.
        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "28"
        dr("sDefination") = "28 - Fiscal Intermediary Identification Number as assigned by Health Care Financing Administration (HCFA)"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        ''Bug #45998: gloPM Admin >> Clearing house >> Sender Qualifier >> Application shows wrong description for qualifier codes.
        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "29"
        dr("sDefination") = "29 - Medicare Provider and Supplier Identification Number as assigned by Health Care Financing Administration (HCFA)"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "30"
        dr("sDefination") = "30 - U.S. Federal Tax Identification Number"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "33"
        dr("sDefination") = "33 - National Association of Insurance Commissioners Company Code (NAIC)"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        dr = dtReceiverQualifier.NewRow()
        dr("sCode") = "ZZ"
        dr("sDefination") = "ZZ - Mutually Defined"
        dtReceiverQualifier.Rows.Add(dr)
        dtReceiverQualifier.AcceptChanges()

        cmbRInterchangeIDQualifier.DataSource = dtReceiverQualifier
        cmbRInterchangeIDQualifier.DisplayMember = "sDefination"
        cmbRInterchangeIDQualifier.ValueMember = "sCode"
        cmbRInterchangeIDQualifier.Refresh()

        'cmbRInterchangeIDQualifier.SelectedValue = "00"
    End Sub

    Private Function getWidthofListItems(_text As String, combo As ComboBox) As Integer
        Dim width As Integer = 0
        Dim g As Graphics = Me.CreateGraphics()
        If g IsNot Nothing Then
            Dim s As SizeF = g.MeasureString(_text, combo.Font)
            width = Convert.ToInt32(s.Width)
            'Dispose graphics object
            g.Dispose()
        End If

        Return width
    End Function
    ''Bug #45999: gloPM Admin >> Clearing house >> Sender Qualifier >> Application does not shows tooltip for qualifier having large text.
    ''
    Private Sub cmbRInterchangeIDQualifier_MouseHover(sender As System.Object, e As System.EventArgs) Handles cmbRInterchangeIDQualifier.MouseHover
        Try
            Dim combo As New ComboBox()
            combo = DirectCast(sender, ComboBox)

            If cmbRInterchangeIDQualifier.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbRInterchangeIDQualifier.Items(cmbRInterchangeIDQualifier.SelectedIndex), DataRowView)("sDefination")), cmbRInterchangeIDQualifier) >= cmbRInterchangeIDQualifier.DropDownWidth - 40 Then
                    tooltip_Combobox.SetToolTip(cmbRInterchangeIDQualifier, Convert.ToString(DirectCast(cmbRInterchangeIDQualifier.Items(cmbRInterchangeIDQualifier.SelectedIndex), DataRowView)("sDefination")))
                Else
                    Me.tooltip_Combobox.Hide(cmbRInterchangeIDQualifier)
                End If
            Else
                Me.tooltip_Combobox.Hide(cmbRInterchangeIDQualifier)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        End Try
    End Sub

    Private Sub cmbSInterchangeIDQualifier_MouseHover(sender As System.Object, e As System.EventArgs) Handles cmbSInterchangeIDQualifier.MouseHover
        Try
            Dim combo As New ComboBox()
            combo = DirectCast(sender, ComboBox)

            If cmbSInterchangeIDQualifier.SelectedItem IsNot Nothing Then
                If getWidthofListItems(Convert.ToString(DirectCast(cmbSInterchangeIDQualifier.Items(cmbSInterchangeIDQualifier.SelectedIndex), DataRowView)("sDefination")), cmbSInterchangeIDQualifier) >= cmbSInterchangeIDQualifier.DropDownWidth - 40 Then
                    tooltip_Combobox.SetToolTip(cmbSInterchangeIDQualifier, Convert.ToString(DirectCast(cmbSInterchangeIDQualifier.Items(cmbSInterchangeIDQualifier.SelectedIndex), DataRowView)("sDefination")))
                Else
                    Me.tooltip_Combobox.Hide(cmbSInterchangeIDQualifier)
                End If
            Else
                Me.tooltip_Combobox.Hide(cmbSInterchangeIDQualifier)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        End Try
    End Sub
    Private Sub ShowTooltipOnComboBox(sender As Object, e As DrawItemEventArgs)
        Try
            Dim combo As New ComboBox()
            combo = DirectCast(sender, ComboBox)
            If combo.Items.Count > 0 AndAlso e.Index >= 0 Then
                e.DrawBackground()
                Using br As New SolidBrush(e.ForeColor)
                    e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
                End Using

                If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                    If combo.DroppedDown Then
                        Dim txt As String = combo.GetItemText(combo.Items(e.Index)).ToString()
                        If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth - 20 Then
                            If tooltip_Combobox.GetToolTip(combo) <> txt Then
                                Me.tooltip_Combobox.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 180, e.Bounds.Bottom)
                            End If
                        Else
                            Me.tooltip_Combobox.SetToolTip(combo, "")
                        End If
                    Else
                        Me.tooltip_Combobox.Hide(combo)
                    End If
                Else
                End If
                e.DrawFocusRectangle()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
        End Try
    End Sub

    Private Sub ChkEnableRealtimeClaimStatus_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkEnableRealtimeClaimStatus.CheckedChanged
        If ChkEnableRealtimeClaimStatus.CheckState = CheckState.Checked Then
            txtRealtimeClaimUrl.Enabled = True
            txtRealtimeClaimUserName.Enabled = True
            txtRealtimeClaimPassword.Enabled = True
        End If
        If ChkEnableRealtimeClaimStatus.CheckState = CheckState.Unchecked Then
            txtRealtimeClaimUrl.Enabled = False
            txtRealtimeClaimUserName.Enabled = False
            txtRealtimeClaimPassword.Enabled = False
        End If
    End Sub
End Class
