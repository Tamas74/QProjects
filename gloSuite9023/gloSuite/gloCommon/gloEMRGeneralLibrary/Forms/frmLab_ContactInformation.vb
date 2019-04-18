Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRLab
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmLab_ContactInformation

    Public nEditID As Int64
    Public sEditContactName As String
    Public sEditFirstName As String
    Public sEditMiddleName As String
    Public sEditLastName As String
    Public blnIsModify As Boolean = False
    Public blnContactType As LabActor.enumContactType
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _Country As String = ""
    'sarika 15th oct 07
    Public m_contactID As Long

    Public Property ContactID() As Long
        Get
            Return m_contactID
        End Get
        Set(ByVal value As Long)
            m_contactID = value
        End Set
    End Property
    '------
    'Dim oAddresscontrol As gloAddress.gloAddressControl = Nothing
    Private Sub SaveContactInformation()

        ' Dim oLabContactInfo As New LabActor.LabContactInformation
        Dim oContactInfo As New gloEMRLabContactInfo


        Try




            'Blank


            If blnContactType = LabActor.enumContactType.PreferredLab Then
                If txtContactName.Text = "" Then
                    MessageBox.Show("Please enter Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtContactName.Select()
                    Exit Sub
                End If
            Else
                'if referred by/sampled by
                If txtFirstName.Text = "" And txtLastName.Text = "" Then
                    MessageBox.Show("Please enter First Name and Last Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtFirstName.Select()
                    Exit Sub
                End If
                If txtFirstName.Text = "" Then
                    MessageBox.Show("Please enter First Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtFirstName.Select()
                    Exit Sub
                End If
                If txtLastName.Text = "" Then
                    MessageBox.Show("Please enter Last Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtLastName.Select()
                    Exit Sub
                End If
            End If

            With oContactInfo.LabContactInfo
                .ContactName = txtContactName.Text.Trim
                .FirstName = txtFirstName.Text.Trim
                .MiddleName = txtMiddleName.Text.Trim
                .LastName = txtLastName.Text.Trim
                .Type = blnContactType
                .Address1 = txtAddress1.Text.Trim()
                .Address2 = txtAddress2.Text.Trim()
                .City = txtCity.Text.Trim()
                .Country = cmbCountry.Text.Trim()
                .Zip = txtZip.Text.Trim()
                .State = cmbState.Text.Trim()
                .County = txtCounty.Text.Trim()
                .Phone = mskGIPhone.Text.Trim()

            End With


            If blnIsModify = True Then
                'open in add mode

                'check for Duplicate entries for add

                If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if Preferred lab
                    If oContactInfo.IsExists(Trim(txtContactName.Text), "", "", "", blnContactType) Then
                        MessageBox.Show("Duplicate Lab Name. Please enter another Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtContactName.Text = ""
                        txtContactName.Select()
                        Exit Sub
                    End If
                Else
                    'If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if referredby  / sampled by

                    If oContactInfo.IsExists("", Trim(txtFirstName.Text), Trim(txtMiddleName.Text), Trim(txtLastName.Text), blnContactType) Then
                        MessageBox.Show("Duplicate Contact Name. Please enter another Contact Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtFirstName.Text = ""
                        txtLastName.Text = ""
                        txtMiddleName.Text = ""
                        txtFirstName.Select()
                        Exit Sub
                    End If

                    'ElseIf blnContactType = LabActor.enumContactType.PreferredLab Then
                    '    'if

                End If

                'sarika 16th oct 07
                'oContactInfo.Add(blnContactType)
                m_contactID = oContactInfo.Add(blnContactType)
                '-----------
                '    oContactInfo.Modify(nEditID, blnContactType)

            Else
                'modify Lab contact information

                'check for Duplicate entries for modify

                If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if Preferred lab
                    If sEditContactName <> "" Then
                        If UCase(txtContactName.Text) <> UCase(sEditContactName) Then
                            If oContactInfo.IsExists(Trim(txtContactName.Text), "", "", "", blnContactType) Then
                                MessageBox.Show("Duplicate Lab Name. Please enter another Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtContactName.Text = ""
                                txtContactName.Select()
                                Exit Sub
                            End If
                        End If
                    Else
                        'sarika 16th oct 07
                        'i.e. the form is loaded from the usre control in Lab Orders form 
                        If oContactInfo.IsExists(nEditID, Trim(txtContactName.Text), "", "", "", blnContactType) Then
                            MessageBox.Show("Duplicate Lab Name. Please enter another Lab Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtContactName.Text = ""
                            txtContactName.Select()
                            Exit Sub
                        End If
                    End If
                Else
                    'If blnContactType = LabActor.enumContactType.PreferredLab Then
                    'if referredby  / sampled by

                    If UCase(txtFirstName.Text) <> UCase(sEditFirstName) Or UCase(txtMiddleName.Text) <> UCase(sEditMiddleName) Or UCase(txtLastName.Text) <> UCase(sEditLastName) Then
                        If oContactInfo.IsExists("", Trim(txtFirstName.Text), Trim(txtMiddleName.Text), Trim(txtLastName.Text), blnContactType) Then
                            MessageBox.Show("Duplicate Contact Name. Please enter another Contact Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtFirstName.Text = ""
                            txtLastName.Text = ""
                            txtMiddleName.Text = ""
                            txtFirstName.Select()
                            Exit Sub
                        End If
                    End If

                    'ElseIf blnContactType = LabActor.enumContactType.PreferredLab Then
                    '    'if

                End If

                oContactInfo.Modify(nEditID, blnContactType)
            End If



            If blnIsModify = True Then

                txtContactName.Text = ""
                txtFirstName.Text = ""
                txtMiddleName.Text = ""
                txtLastName.Text = ""

                If blnContactType = LabActor.enumContactType.PreferredLab Then
                    txtContactName.Select()
                ElseIf blnContactType = LabActor.enumContactType.ReferredBy Or blnContactType = LabActor.enumContactType.SampledBy Then
                    txtFirstName.Select()
                End If
            End If



            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            ' oLabContactInfo = Nothing
            If (IsNothing(oContactInfo) = False) Then
                oContactInfo.Dispose()
                oContactInfo = Nothing
            End If


        End Try
    End Sub

    Private Sub frmLab_ContactInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim _databaseconnectionstring As String = ""
            'oAddresscontrol = New gloAddress.gloAddressControl(gloEMRDatabase.DataBaseLayer.ConnectionString)
            'oAddresscontrol.Dock = DockStyle.Fill
            'oAddresscontrol.Name = "LabAddressControl"
            'pnlAddresssControl.Controls.Add(oAddresscontrol)

            fillCountry()



            If appSettings("Country") IsNot Nothing Then
                If appSettings("Country") <> "" Then

                    _Country = Convert.ToString(appSettings("Country"))
                Else
                    _Country = "US"
                End If
            Else
                _Country = "US"
            End If

            cmbCountry.SelectedValue = _Country
            If _Country = "Canada" Then
                lblState.Text = "Province :"
                Dim pt As New Point(168, 57)
                lblState.Location = pt

                txtCounty.Visible = False
                txtCounty.Text = String.Empty
                lblCounty.Visible = False

                txtZip.MaxLength = 6
            End If

            fillStates()




            If blnContactType = LabActor.enumContactType.PreferredLab Then

                pnlContactInfo.Visible = True
                pnlRefSampledBy.Visible = False
                Me.Text = "Preferred Lab Master"
            Else
                pnlContactInfo.Visible = False
                pnlRefSampledBy.Visible = True
                If blnContactType = LabActor.enumContactType.ReferredBy Then
                    Me.Text = "Referred By Master"
                ElseIf blnContactType = LabActor.enumContactType.SampledBy Then
                    Me.Text = "Sampled By Master"
                End If
            End If


            If nEditID > 0 Then
                Dim oContactInfo As LabActor.LabContactInformation
                Dim oContactInfoDtl As New gloEMRLabContactInfo
                oContactInfo = oContactInfoDtl.GetContactInformation(nEditID, blnContactType)
                If Not oContactInfo Is Nothing Then
                    txtContactName.Text = oContactInfo.ContactName
                    txtFirstName.Text = oContactInfo.FirstName
                    txtMiddleName.Text = oContactInfo.MiddleName
                    txtLastName.Text = oContactInfo.LastName

                    txtAddress1.Text = oContactInfo.Address1
                    txtAddress2.Text = oContactInfo.Address2
                    txtZip.Text = oContactInfo.Zip
                    txtCity.Text = oContactInfo.City
                    cmbCountry.Text = oContactInfo.Country
                    cmbState.Text = oContactInfo.State
                    txtCounty.Text = oContactInfo.County
                    mskGIPhone.Text = oContactInfo.Phone
                    oContactInfo.Dispose()
                    oContactInfo = Nothing
                End If
                ' oContactInfo = Nothing
                oContactInfoDtl.Dispose()
                oContactInfoDtl = Nothing
            End If

            If blnContactType = LabActor.enumContactType.PreferredLab Then
                txtContactName.Select()
            ElseIf blnContactType = LabActor.enumContactType.ReferredBy Or blnContactType = LabActor.enumContactType.SampledBy Then
                txtFirstName.Select()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub tlsp_ContactInformation_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ContactInformation.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveContactInformation()
            Case "Close"
                Me.Close()

        End Select
    End Sub

    Private Sub fillStates()

        Try
            Dim dtStates As DataTable = Nothing

            dtStates = gloGlobal.gloPMMasters.GetStates()

            If dtStates IsNot Nothing Then
                Dim dr As DataRow = dtStates.NewRow()
                dr("ST") = ""
                dtStates.Rows.InsertAt(dr, 0)
                dtStates.AcceptChanges()

                cmbState.DataSource = dtStates
                cmbState.DisplayMember = "ST"
                cmbState.SelectedIndex = -1
            End If
      
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing

        Finally
        End Try

    End Sub

    Private _isCountryComboLoading As Boolean = False

    Private Sub fillCountry()

        Try
            _isCountryComboLoading = True
            Dim dtCountry As DataTable = Nothing
            dtCountry = gloGlobal.gloPMMasters.GetCounrty()

            If dtCountry IsNot Nothing Then
                Dim dr As DataRow = dtCountry.NewRow()
                dr("sCode") = ""
                dr("sSubCode") = ""
                dr("sName") = ""
                dr("sStateLabel") = "State"
                dtCountry.Rows.InsertAt(dr, 0)
                dtCountry.AcceptChanges()

                cmbCountry.DataSource = dtCountry
                cmbCountry.DisplayMember = "sName"
                cmbCountry.ValueMember = "sCode"

                cmbCountry.BeginUpdate()
                cmbCountry.SelectedIndex = -1
                cmbCountry.EndUpdate()
            End If
     
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        Finally
            _isCountryComboLoading = False
        End Try

    End Sub

    Private Sub cmbCountry_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbCountry.SelectedIndexChanged
      
        Dim _drCountrySelectedRow As DataRowView = Nothing
        Dim _StateLabel As String = ""
        'Dim _bIsSystemRecord As Boolean = False

        Try
            If _isCountryComboLoading = False Then


                lblState.Text = ""
                'txtZip.Text = "";
                'cmbState.SelectedIndex = -1;

                If cmbCountry.SelectedItem IsNot Nothing Then
                    _drCountrySelectedRow = DirectCast(cmbCountry.SelectedItem, DataRowView)
                    _StateLabel = ""
                    '_bIsSystemRecord = False

                    _StateLabel = Convert.ToString(_drCountrySelectedRow("sStateLabel"))
                    If _StateLabel.Trim() <> "" Then
                        _StateLabel = _StateLabel.Trim() + " :"
                    Else
                        _StateLabel = "State :"
                    End If
                    lblState.Text = _StateLabel

                
                End If
                'if (IsLoadEvent == false)
                '{
                '    this.txtZip.TextChanged -= new System.EventHandler(this.txtZip_TextChanged);
                '    txtZip.Text = "";
                '    this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
                '}
                ''Point pt = new Point(168, 57);
                ''lblState.Location = pt;

                If _drCountrySelectedRow IsNot Nothing Then
                    If Convert.ToString(_drCountrySelectedRow("sCode")).ToUpper() = "US" Then
                        txtCounty.Visible = True
                        lblCounty.Visible = True
                        txtZip.MaxLength = 5
                        cmbState.DropDownStyle = ComboBoxStyle.DropDownList
                        _Country = "US"
                        '7022Items: Home Billing
                        'check for country only for US to disply area code.
                        SetAreaCode()
                    ElseIf Convert.ToString(_drCountrySelectedRow("sCode")).ToUpper() = "CA" Then
                        txtCounty.Visible = False
                        lblCounty.Visible = False
                        txtZip.MaxLength = 7
                        cmbState.DropDownStyle = ComboBoxStyle.DropDownList
                        _Country = "CA"
                        '7022Items: Home Billing
                        'check for country only for US to disply area code.
                        SetAreaCode()
                    Else
                        txtCounty.Visible = False
                        txtCounty.Text = String.Empty
                        lblCounty.Visible = False
                        txtZip.MaxLength = 10
                        cmbState.DropDownStyle = ComboBoxStyle.DropDown
                        cmbState.MaxLength = 2
                        _Country = ""
                        '7022Items: Home Billing
                        'check for country only for US to disply area code.
                        SetAreaCode()
                    End If
                Else
                    txtCounty.Visible = False
                    txtCounty.Text = String.Empty
                    lblCounty.Visible = False
                    txtZip.MaxLength = 10
                    cmbState.DropDownStyle = ComboBoxStyle.DropDown
                    cmbState.MaxLength = 2
                    _Country = ""
                    '7022Items: Home Billing
                    'check for country only for US to disply area code.
                    SetAreaCode()
                End If

              
                End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            ex = Nothing
        Finally
            _drCountrySelectedRow = Nothing
            'IsLoadEvent = false;
            _StateLabel = Nothing
        End Try
    End Sub

    Public Sub SetAreaCode()
        'If UseAreaCodeForPatient Then
        '7022Items: Home Billing
        'check for country only for US to disply area code.
        If _Country = "US" Then
            Me.txtAreaCode.Visible = True
            Me.txtArea.Visible = True
            Me.txtZip.Size = New Size(43, 22)
        Else
            Me.txtAreaCode.Visible = False
            Me.txtArea.Visible = False
            Me.txtZip.Size = New Size(88, 22)
        End If
        'End If
    End Sub
End Class