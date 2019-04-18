Partial Public Class frmSetupClearingHouse
    Inherits Form
#Region "Variable Declaration"

    Private _messageBoxCaption As String = "gloPM"
    Private _databaseconnectionstring As String = ""
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Private _ClinicID As Int64 = 1
    Private _nClearingHouseID As Int64 = 0
    Private Const _encryptionKey As String = "12345678"

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

    Public Sub New(ByVal DatabaseConnectionString As String)
        InitializeComponent()

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

    Public Sub New(ByVal DatabaseConnectionString As String, ByVal ClearingHouseID As Int64)
        InitializeComponent()

        _databaseconnectionstring = DatabaseConnectionString
        _nClearingHouseID = ClearingHouseID

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

#Region " Form Load Event"

    Private Sub frmSetupClearingHouse_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FillControls()
        If _nClearingHouseID > 0 Then
            LoadClearingHouse()
        End If
    End Sub

#End Region

#Region "Toolstrip Buttons"

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ts_btnSave.Click
        Try
            If ValidateData() = True Then
                If SaveData() = True Then
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
        Dim oClsEncryption As New ClsEncryption()
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
                        cmbTypeofData.Text = "Blank"
                        Exit Select
                    Case Else
                        Exit Select
                End Select
                cmbTypeofData.Refresh()

                chkISA.Checked = oClearingHouse.IsISA


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
            cmbTypeofData.Items.Add("Blank")

            cmbTypeofData.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveData() As Boolean
        Dim oClearingHouse As New ClearingHouse(_databaseconnectionstring)
        Dim oClsEncryption As New ClsEncryption()
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
                    oClearingHouse.TypeOfData = TypeOfData.Blank
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

            '#End Region 


            Dim _nTempID As Int64 = oClearingHouse.Add(oClearingHouse)
            _nClearingHouseID = _nTempID

            If _nTempID > 0 Then
               
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
        If txtName.Text.Trim() = "" Then
            MessageBox.Show("Plaese enter the clearinghouse name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtName.Focus()
            Return False
        End If
        Return True
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

#End Region



    
End Class
