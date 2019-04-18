Imports gloEMR.gloEMRWord
Imports gloWord
Public Class frmAmendmentsSetup

#Region " Private & Public Variable Declaration "

    Dim _PatientID As Int64 = 0
    Dim _AmendmentID As Int64 = 0
    Private WithEvents gloUC_PatientStrip As gloUserControlLibrary.gloUC_PatientStrip
    Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management
    Dim _DocumentID As Int64 = 0
    Dim _sPatientName As String = ""
    Dim _sPatientCode As String = ""
    Dim lblRequestDateDefaultLocation As System.Drawing.Point
    Dim dtRequestDateDefaultLocation As System.Drawing.Point

 

#End Region

#Region " Property Procedures "

    Public ReadOnly Property PatientID() As Int64
        Get
            Return _PatientID
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New(ByVal PatientId As Int64, ByVal AmendmentId As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me._PatientID = PatientId
        Me._AmendmentID = AmendmentId

    End Sub

#End Region

#Region " Form Events "

    Private Sub frmAmendmentsSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try

            LoadFormData()
            lblRequestDateDefaultLocation = lblRequestDate.Location
            dtRequestDateDefaultLocation = dtRequestDate.Location
            Dim scheme As gloBilling.Cls_TabIndexSettings.TabScheme = gloBilling.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New gloBilling.Cls_TabIndexSettings(Me)
            tom.SetTabOrder(scheme)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AmedmentsSetupScreen, gloAuditTrail.ActivityType.Open, "Amedment Setup Screen Open", _PatientID, _AmendmentID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AmedmentsSetupScreen, gloAuditTrail.ActivityType.Open, "Amedment Setup Screen Open", _PatientID, _AmendmentID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

        End Try

    End Sub

    Private Sub frmAmendmentsSetup_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Dispose all form level objects here
        Try
            If Not IsNothing(gloUC_PatientStrip) Then
                gloUC_PatientStrip.Dispose()
                gloUC_PatientStrip = Nothing
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AmedmentsSetupScreen, gloAuditTrail.ActivityType.Close, "Amedment Setup Screen Closed", _PatientID, _AmendmentID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            'Keep this catch blank
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.AmedmentsSetupScreen, gloAuditTrail.ActivityType.Close, "Amedment Setup Screen Closed", _PatientID, _AmendmentID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

#End Region

#Region " Patient Details Strip "

    Private Sub SetPatientStrip()

        If IsNothing(gloUC_PatientStrip) Then

            gloUC_PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip


            With gloUC_PatientStrip

                .Dock = DockStyle.Top
                .Padding = New Padding(3, 0, 3, 0)
                .ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None)
                .BringToFront()
                .DTPEnabled = False
                _sPatientName = .PatientName
                _sPatientCode = .PatientCode
            End With
            Me.Controls.Add(gloUC_PatientStrip)
            pnlMain.BringToFront()
        End If

    End Sub

#End Region

#Region " Toolstrip button click event "

    Private Sub tsBtn_Save_Click(sender As System.Object, e As System.EventArgs) Handles tsBtn_Save.Click
        Dim oClsAmendments As New ClsAmendments(_PatientID, _AmendmentID)
        Try

            If msktxtRequestorPhone.IsValidated = True Then
                oClsAmendments.RequestorPhone = msktxtRequestorPhone.Text.Trim()
            Else
                Return
            End If
            If lblOthernamemandatory.Visible Then
                If txtOtherRequestor.Text.Trim() = "" Then
                    MessageBox.Show("Required other requester.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtOtherRequestor.Focus()
                    Exit Sub
                End If
            End If

            If txtAmendmentReason.Text.Trim() = "" Then
                MessageBox.Show("Enter amendment reason. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtAmendmentReason.Focus()
                Exit Sub
            End If

            If txtAmendmentDetails.Text.Trim() = "" Then
                MessageBox.Show("Enter amendment details. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtAmendmentDetails.Focus()
                Exit Sub
            End If


            If rbRequestorPatient.Checked Then
                oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Patient
                oClsAmendments.RequestorOtherSpecified = _sPatientName
            End If
            If rbRequestorOther.Checked Then
                oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Other
                oClsAmendments.RequestorOtherSpecified = txtOtherRequestor.Text
            End If
            If rbRequestorProvider.Checked Then
                oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Provider
                oClsAmendments.RequestorProviderID = CmbProvider.SelectedValue
                oClsAmendments.RequestorOtherSpecified = CmbProvider.Text
            End If
            oClsAmendments.RequestDate = dtRequestDate.Value

            oClsAmendments.AmendmentsReason = txtAmendmentReason.Text
            oClsAmendments.AmendmentsDetails = txtAmendmentDetails.Text

            If rbStatusAccepted.Checked Then
                oClsAmendments.AmendmentStatusId = ClsAmendments.AmendmentStatus.Accepted
            ElseIf rbStatusPending.Checked Then
                oClsAmendments.AmendmentStatusId = ClsAmendments.AmendmentStatus.Pending
            Else
                oClsAmendments.AmendmentStatusId = ClsAmendments.AmendmentStatus.Denied
            End If

            If dtpAcceptDeniedAmendmentDate.Enabled Then
                oClsAmendments.AcceptedOrDeniedDate = dtpAcceptDeniedAmendmentDate.Value
            End If

            If cmbAcceptedDeniedAmendmentUser.Enabled Then
                oClsAmendments.AcceptedOrDeniedUserID = cmbAcceptedDeniedAmendmentUser.SelectedValue
            End If

            If chkDeniedReasonOne.Enabled Then
                If chkDeniedReasonOne.Checked Then
                    oClsAmendments.AmendmentsDeniedReasonOne = ClsAmendments.DeniedReasons.DeniedReasonOne
                End If

            End If

            If chkDeniedReasonTwo.Enabled Then
                If chkDeniedReasonTwo.Checked Then
                    oClsAmendments.AmendmentsDeniedReasonTwo = ClsAmendments.DeniedReasons.DeniedReasonTwo
                End If

            End If

            If chkDeniedReasonThree.Enabled Then
                If chkDeniedReasonThree.Checked Then
                    oClsAmendments.AmendmentsDeniedReasonThree = ClsAmendments.DeniedReasons.DeniedReasonThree
                End If

            End If

            If chkDeniedReasonFour.Enabled Then
                If chkDeniedReasonFour.Checked Then
                    oClsAmendments.AmendmentsDeniedReasonFour = ClsAmendments.DeniedReasons.DeniedReasonfour
                End If

            End If

            If txtAcceptedDeniedNotes.Enabled Then
                oClsAmendments.AmendmentsDeniedNotes = txtAcceptedDeniedNotes.Text
            End If
            oClsAmendments.AmendmentsDocumentID = _DocumentID
            If oClsAmendments.SaveAmendmets() Then
                Me.Close()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            'dispose objects here..
            If oClsAmendments IsNot Nothing Then
                oClsAmendments = Nothing
            End If
        End Try
    End Sub

    Private Sub tsBtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tsBtn_Close.Click
        Me.Close()
    End Sub

    Private Sub btnScan_Click(sender As System.Object, e As System.EventArgs) Handles btnScan.Click
        Try
            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_Amedment) = False Then
                gDMSCategory_Amedment = objSettings.DMSCategory_Amedment
            End If
            objSettings = Nothing
            ScanViewDoucment()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnView_Click(sender As System.Object, e As System.EventArgs) Handles btnView.Click
        If (_DocumentID > 0) Then
            ViewScanDoucmentNew()
        End If
    End Sub

    Private Sub txtAmedmentScan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmedmentScan.TextChanged
        If txtAmedmentScan.Text.Trim() <> "" Then
            btnView.Visible = False
            btnView.Location = btnScan.Location
            btnScan.Visible = False
            txtAmedmentScan.BackColor = Color.FromArgb(251, 208, 95)
            txtAmedmentScan.ForeColor = Color.White

        Else
            btnView.Visible = False
            btnScan.Visible = False
        End If
    End Sub
#End Region

#Region " Radio button events "

    Private Sub rbRequestorType_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbRequestorPatient.CheckedChanged, rbRequestorOther.CheckedChanged, rbRequestorProvider.CheckedChanged

        Dim _tempRadioButtonVariable As RadioButton = Nothing
        Dim oClsAmendments As New ClsAmendments(_PatientID, _AmendmentID)
        oClsAmendments.GetPatientAmedments()
        Try

            _tempRadioButtonVariable = CType(sender, RadioButton)

            If Not IsNothing(_tempRadioButtonVariable) Then

                If _tempRadioButtonVariable.Tag = "PATIENT" Then
                    txtOtherRequestor.Text = ""
                    txtOtherRequestor.Enabled = False
                    lblOthernamemandatory.Visible = False
                    CmbProvider.Enabled = False
                    If _AmendmentID = 0 Then
                        msktxtRequestorPhone.Text = oClsAmendments.GetPatientPhonenumber(_PatientID)
                    Else
                        If oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Patient Then
                            msktxtRequestorPhone.Text = oClsAmendments.RequestorPhone
                        Else
                            msktxtRequestorPhone.Text = oClsAmendments.GetPatientPhonenumber(_PatientID)
                        End If

                    End If

                    lblProvider.Visible = False
                    CmbProvider.Visible = False
                    lblOtherRequestor.Visible = False
                    txtOtherRequestor.Visible = False
                    'lblRequestDate.Location = lblProvider.Location
                    'dtRequestDate.Location = CmbProvider.Location
                ElseIf _tempRadioButtonVariable.Tag = "OTHER" Then
                    txtOtherRequestor.Text = ""
                    txtOtherRequestor.Enabled = True
                    lblOthernamemandatory.Visible = True
                    CmbProvider.Enabled = False
                    lblProvider.Visible = False
                    CmbProvider.Visible = False
                    If _AmendmentID = 0 Then
                        msktxtRequestorPhone.Text = ""
                    Else
                        If oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Other Then
                            msktxtRequestorPhone.Text = oClsAmendments.RequestorPhone
                            txtOtherRequestor.Text = oClsAmendments.RequestorOtherSpecified
                        Else
                            msktxtRequestorPhone.Text = ""
                        End If

                    End If

                    lblOtherRequestor.Visible = True
                    txtOtherRequestor.Visible = True
                    'lblRequestDate.Location = lblRequestDateDefaultLocation
                    'dtRequestDate.Location = dtRequestDateDefaultLocation
                ElseIf _tempRadioButtonVariable.Tag = "PROVIDER" Then
                    txtOtherRequestor.Text = ""
                    txtOtherRequestor.Enabled = False
                    lblOthernamemandatory.Visible = False
                    CmbProvider.Enabled = True
                    If _AmendmentID Then
                        msktxtRequestorPhone.Text = oClsAmendments.GetProviderPhonenumber(CmbProvider.SelectedValue)
                    Else
                        If oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Provider Then
                            msktxtRequestorPhone.Text = oClsAmendments.RequestorPhone
                        Else
                            msktxtRequestorPhone.Text = oClsAmendments.GetProviderPhonenumber(CmbProvider.SelectedValue)
                        End If
                    End If

                    lblProvider.Visible = True
                    CmbProvider.Visible = True
                    lblOtherRequestor.Visible = False
                    txtOtherRequestor.Visible = False
                    'lblRequestDate.Location = lblRequestDateDefaultLocation
                    'dtRequestDate.Location = dtRequestDateDefaultLocation
                End If

            End If

        Catch ex As Exception
        Finally

            'If Not IsNothing(_tempRadioButtonVariable) Then
            '    _tempRadioButtonVariable.Dispose()
            '    _tempRadioButtonVariable = Nothing
            'End If

            If Not IsNothing(oClsAmendments) Then
                oClsAmendments = Nothing
            End If

        End Try
    End Sub

    Private Sub rbStatus_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbStatusAccepted.CheckedChanged, rbStatusPending.CheckedChanged, rbStatusDenied.CheckedChanged

        Dim _tempRadioButtonVariable As RadioButton = Nothing
        Dim _tempRadioButtonTag As String = ""

        Try

            _tempRadioButtonVariable = CType(sender, RadioButton)
            _tempRadioButtonTag = Convert.ToString(_tempRadioButtonVariable.Tag)

            Select Case _tempRadioButtonTag

                Case "ACCEPTED"

                    chkDeniedReasonOne.Checked = False
                    chkDeniedReasonOne.Enabled = False
                    chkDeniedReasonTwo.Checked = False
                    chkDeniedReasonTwo.Enabled = False
                    chkDeniedReasonThree.Checked = False
                    chkDeniedReasonThree.Enabled = False
                    chkDeniedReasonFour.Checked = False
                    chkDeniedReasonFour.Enabled = False
                    grpDeniedStatus.Enabled = False
                    dtpAcceptDeniedAmendmentDate.Enabled = True
                    cmbAcceptedDeniedAmendmentUser.Enabled = True
                    txtAcceptedDeniedNotes.Enabled = True


                Case "PENDING"

                    chkDeniedReasonOne.Checked = False
                    chkDeniedReasonOne.Enabled = False
                    chkDeniedReasonTwo.Checked = False
                    chkDeniedReasonTwo.Enabled = False
                    chkDeniedReasonThree.Checked = False
                    chkDeniedReasonThree.Enabled = False
                    chkDeniedReasonFour.Checked = False
                    chkDeniedReasonFour.Enabled = False
                    grpDeniedStatus.Enabled = False
                    dtpAcceptDeniedAmendmentDate.Enabled = False
                    cmbAcceptedDeniedAmendmentUser.Enabled = False
                    txtAcceptedDeniedNotes.Enabled = False
                    txtAcceptedDeniedNotes.Text = ""

                Case "DENIED"

                    chkDeniedReasonOne.Checked = False
                    chkDeniedReasonOne.Enabled = True
                    chkDeniedReasonTwo.Checked = False
                    chkDeniedReasonTwo.Enabled = True
                    chkDeniedReasonThree.Checked = False
                    chkDeniedReasonThree.Enabled = True
                    chkDeniedReasonFour.Checked = False
                    chkDeniedReasonFour.Enabled = True
                    grpDeniedStatus.Enabled = True
                    dtpAcceptDeniedAmendmentDate.Enabled = True
                    cmbAcceptedDeniedAmendmentUser.Enabled = True
                    txtAcceptedDeniedNotes.Enabled = True


            End Select

        Catch ex As Exception

        Finally
            'If Not IsNothing(_tempRadioButtonVariable) Then
            '    _tempRadioButtonVariable.Dispose()
            '    _tempRadioButtonVariable = Nothing
            'End If
        End Try

    End Sub

    Private Sub CmbProvider_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles CmbProvider.SelectedValueChanged
        Dim oClsAmendments As New ClsAmendments(_PatientID, 0)
        Try
            If CmbProvider.SelectedValue > 0 Then
                msktxtRequestorPhone.Text = oClsAmendments.GetProviderPhonenumber(CmbProvider.SelectedValue)
            End If
        Catch ex As Exception
        Finally
            If Not IsNothing(oClsAmendments) Then
                oClsAmendments = Nothing
            End If
        End Try

    End Sub
#End Region

#Region " Private & Public methods "

    Private Sub LoadFormData()
        Try
            btnView.Visible = False
            SetPatientStrip()
            FillProviderCombo()
            FillUserCombo()
            If Me._AmendmentID = 0 Then
                'new form open
                rbRequestorPatient.Checked = True
                rbStatusPending.Checked = True
            Else
                GetAmedments()
            End If
            If _AmendmentID > 0 Then
                Me.Text = "Modify Health Record Amendment for Patient - " & _sPatientName & " ( " & _sPatientCode & " )"
            Else
                Me.Text = "Add Health Record Amendment for Patient -  " & _sPatientName & " ( " & _sPatientCode & " )"
            End If
        Catch ex As Exception

        Finally

        End Try

    End Sub
    Private Sub GetAmedments()
        Dim oClsAmendments As New ClsAmendments(_PatientID, _AmendmentID)
        Try

            oClsAmendments.GetPatientAmedments()

            CmbProvider.SelectedValue = oClsAmendments.RequestorProviderID
            If oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Patient Then
                rbRequestorPatient.Checked = True
            ElseIf oClsAmendments.AmendmentsRequestorType = ClsAmendments.RequestorType.Other Then
                rbRequestorOther.Checked = True
                txtOtherRequestor.Text = oClsAmendments.RequestorOtherSpecified
            Else
                rbRequestorProvider.Checked = True
            End If
            dtRequestDate.Value = oClsAmendments.RequestDate
            msktxtRequestorPhone.Text = oClsAmendments.RequestorPhone
            txtAmendmentReason.Text = oClsAmendments.AmendmentsReason
            txtAmendmentDetails.Text = oClsAmendments.AmendmentsDetails

            If oClsAmendments.AmendmentStatusId = ClsAmendments.AmendmentStatus.Accepted Then
                rbStatusAccepted.Checked = True
            ElseIf oClsAmendments.AmendmentStatusId = ClsAmendments.AmendmentStatus.Denied Then
                rbStatusDenied.Checked = True
            Else
                rbStatusPending.Checked = True
            End If

            If oClsAmendments.AcceptedOrDeniedDate <> Date.MinValue Then
                dtpAcceptDeniedAmendmentDate.Value = oClsAmendments.AcceptedOrDeniedDate
            Else
                dtpAcceptDeniedAmendmentDate.Enabled = False
            End If

            If oClsAmendments.AmendmentsDeniedReasonOne = ClsAmendments.DeniedReasons.DeniedReasonOne Then
                chkDeniedReasonOne.Checked = True
            End If

            If oClsAmendments.AmendmentsDeniedReasonTwo = ClsAmendments.DeniedReasons.DeniedReasonTwo Then
                chkDeniedReasonTwo.Checked = True
            End If

            If oClsAmendments.AmendmentsDeniedReasonThree = ClsAmendments.DeniedReasons.DeniedReasonThree Then
                chkDeniedReasonThree.Checked = True
            End If

            If oClsAmendments.AmendmentsDeniedReasonFour = ClsAmendments.DeniedReasons.DeniedReasonfour Then
                chkDeniedReasonFour.Checked = True
            End If

            If Not rbStatusPending.Checked Then
                txtAcceptedDeniedNotes.Text = oClsAmendments.AmendmentsDeniedNotes
            End If
            If oClsAmendments.AcceptedOrDeniedUserID <> 0 Then
                cmbAcceptedDeniedAmendmentUser.SelectedValue = oClsAmendments.AcceptedOrDeniedUserID
            End If



            ''''Used same function for retrive document name which has been used for get document name for immunization

            txtAmedmentScan.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(oClsAmendments.AmendmentsDocumentID)
            _DocumentID = oClsAmendments.AmendmentsDocumentID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            'dispose objects here..
            If oClsAmendments IsNot Nothing Then
                oClsAmendments = Nothing
            End If
        End Try

    End Sub
    Public Sub FillProviderCombo()
        Dim oClsAmendments As New ClsAmendments(_PatientID, _AmendmentID)
        Dim dt As DataTable
        Try
            dt = oClsAmendments.GetActiveProvider()
            'Dim strProviderName As String = ""

            RemoveHandler CmbProvider.SelectedValueChanged, AddressOf CmbProvider_SelectedValueChanged
           
            CmbProvider.DataSource = Nothing
            CmbProvider.Items.Clear()

            CmbProvider.DataSource = dt
            CmbProvider.DisplayMember = dt.Columns("Name").ToString()
            CmbProvider.ValueMember = dt.Columns("nProviderID").ToString()

            Dim objProvider As New clsProvider
            Dim nPatientProvider As Int64 = 0
            ' Dim strPatientProviderName As String

            nPatientProvider = objProvider.GetPatientProvider(_PatientID)
            objProvider.Dispose()
            objProvider = Nothing
            If nPatientProvider > 0 Then
                CmbProvider.SelectedValue = nPatientProvider
            End If

            AddHandler CmbProvider.SelectedValueChanged, AddressOf CmbProvider_SelectedValueChanged

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            'dispose objects here..
            If oClsAmendments IsNot Nothing Then

                oClsAmendments = Nothing
            End If
        End Try
    End Sub

    Public Sub FillUserCombo()
        Dim oClsAmendments As New ClsAmendments(_PatientID, _AmendmentID)
        Dim dt As DataTable
        Try
            dt = oClsAmendments.GetActiveUser()
            'Dim strProviderName As String = ""

            cmbAcceptedDeniedAmendmentUser.DataSource = Nothing
            cmbAcceptedDeniedAmendmentUser.Items.Clear()
            cmbAcceptedDeniedAmendmentUser.DataSource = dt
            cmbAcceptedDeniedAmendmentUser.DisplayMember = dt.Columns("LoginName").ToString()
            cmbAcceptedDeniedAmendmentUser.ValueMember = dt.Columns("USERID").ToString()
            cmbAcceptedDeniedAmendmentUser.SelectedValue = gnLoginID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            'dispose objects here..
            If oClsAmendments IsNot Nothing Then
                oClsAmendments = Nothing
            End If
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
            Dim oClsAmendments As New ClsAmendments(_PatientID, _AmendmentID)
            sDMSScanCategory = gDMSCategory_Amedment

            If _ScanDocFlag = True Then
                If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, sDMSScanCategory, gClinicID) = False Then
                    MessageBox.Show("DMS Category for Amedments has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _ScanDocFlag = False
                End If
            End If

            If _ScanDocFlag = True Then
                Dim arrDocumentInfo As New ArrayList
                Dim strDocumentInfo As String = ""
                _result = Set_ScanDocumentEventNew(PatientID, sDMSScanCategory, _ScanContainerID, _ScanDocumentID)

                _DocumentID = _ScanDocumentID
                ''''Used same function for retrive document name which has been used for get document name for immunization
                txtAmedmentScan.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If txtAmedmentScan.Text.Trim() <> "" Then
                    btnView.Visible = False
                    btnScan.Visible = False
                Else
                    btnView.Visible = False
                    btnScan.Visible = False
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Amedments, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
        End Try
    End Sub

    Private Function Set_ScanDocumentEventNew(PatientID As Int64, LabCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try
            '_result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocument.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
            _result = oScanDocument.ShowEScanner(_PatientID, LabCategory, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), gnClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            oScanDocument.Dispose()
        End Try
        Return _result
    End Function

    Public Sub ViewScanDoucmentNew()
        Try
            ' pnlViewDocument.Visible = True

            If Not IsNothing(oViewDocument) Then
                oViewDocument = Nothing
            End If

            If (_DocumentID > 0) Then
                If IsNothing(oViewDocument) Then
                    oViewDocument = New gloEDocumentV3.gloEDocV3Management()
                End If
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
                ' added code to open view document from RxMeds for case GLO2011-0013188

                Dim isItDialog As Boolean = oViewDocument.ShowEDocument(PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Amedments, _DocumentID)

                If (isItDialog = True) Then
                    If Not IsNothing(oViewDocument) Then
                        'SLR: Dipose and then   

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
                End If

            End If

        Catch ex As Exception
            If Not IsNothing(oViewDocument) Then
                oViewDocument.Dispose()
            End If
        Finally
            'If Not IsNothing(oViewDocument) Then
            '    oViewDocument.Dispose()
            'End If


        End Try
    End Sub

#End Region


    
End Class