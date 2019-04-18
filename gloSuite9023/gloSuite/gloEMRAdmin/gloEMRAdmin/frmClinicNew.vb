Imports System.Text.RegularExpressions
Imports gloSettings
Imports C1.Win.C1FlexGrid

Public Class frmClinicNew
    Inherits System.Windows.Forms.Form


    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Dim tbPageOther As New TabPage
    Private Const COL_VALUE As Integer = 4
    Private Const COL_CLINICMODE As Integer = 2
    Public blnModify As Boolean
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Dim blnLogoChanged As Boolean = False
    ''Sandip Darade 20001113
    ''we dont show  ClinicNPI ,just  to maintain its value in common db1 
    Public ClinicNPI As String = ""
    Private bValidationFailed = False
    Public WithEvents oAddressContol As gloAddress.gloAddressControl
    Public WithEvents oAddressContolPL As gloAddress.gloAddressControl

    Private oListControl As gloListControl.gloListControl
    Private _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Taxonomy
    Dim dttimezn As DataTable = Nothing
    'Dim _IsClinicAdded As Boolean = False
    ''Commented By Dhruv 
    '#Region "Zip control implemented  "
    '    'region added by dipak 20090914
    '    'variable are used in Show ZipControl
    '    Public isFormLoading As Boolean = False
    '    Private oZipcontrol As gloPatient.gloZipcontrol
    '    Private isSearchControlOpen As Boolean = False
    '    Private _TempZipText As String
    '    Private _isZipItemSelected As Boolean = False
    '    Private _isTextBoxLoading As Boolean = False
    '#End Region
    ''End----------------Commented Dhruv 
    Private sAUSID As String = ""
    Public Property ValidationFailed() As Boolean
        Get
            Return bValidationFailed
        End Get
        Set(ByVal Value As Boolean)
            bValidationFailed = Value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ''dhruv adding the control
        oAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oAddressContol.Dock = DockStyle.Fill
        pnlAddresssControl.Controls.Add(oAddressContol)



        oAddressContolPL = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oAddressContolPL.Dock = DockStyle.Fill
        oAddressContolPL.txtAreaCode.Visible = True
        oAddressContolPL.txtArea.Visible = True
        oAddressContolPL.txtZip.Size = New Size(43, 22)
        pnlPLAddresssControl.Controls.Add(oAddressContolPL)
        ' Add any initialization after the InitializeComponent() call.
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try

            With dlgOpenFile
                .Title = "Select Clinic Logo"
                .Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif"
                .CheckFileExists = True
                .Multiselect = False
                .ShowHelp = False
                .ShowReadOnly = False
            End With
            If dlgOpenFile.ShowDialog = DialogResult.OK Then
                txtLogoPath.Text = dlgOpenFile.FileName
                picLogo.Image = Image.FromFile(dlgOpenFile.FileName)

                picLogo.SizeMode = PictureBoxSizeMode.StretchImage
                blnLogoChanged = True
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If tbcClinic.TabPages.Count = 1 Then
            '    tbcClinic.TabPages.Add(tbPageOther)
            'End If
            'tbcClinic.SelectedTab = tbPageOther
            TabControl1.SelectedTab = TabPage1
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmClinicNew_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (Not dttimezn Is Nothing) Then
            dttimezn.Dispose()
            dttimezn = Nothing
        End If
    End Sub

    Private Sub frmClinic_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1ClinicIDs)



        Dim objClinic As New clsClinic

        Try
            ' Contact details tab removed (ref# GLO2009-0003825)
            If TabControl1.TabPages.Contains(TabPage2) Then TabControl1.TabPages.Remove(TabPage2)


            'sarika AUS Username 20080922
            If objClinic.CheckColumnExists() = True Then
                txtAUSUsername.Text = objClinic.GetAUSUserName()
                sAUSID = txtAUSUsername.Text
            End If
            '----
            'Dim cntr, cnt As Integer
            'Dim dtClinicQualifiersIds As New DataTable
            'dtClinicQualifiersIds = objClinic.RetrieveClinicIDQualifiers(txtName.Tag)
            'If (dtClinicQualifiersIds.Rows.Count > 0) Then
            '    If (c1ClinicIDs.Rows.Count > 1) Then
            '        Dim dt As New DataTable()
            '        dt = TryCast(c1ClinicIDs.DataSource, DataTable).Copy()

            '        For cntr = 0 To dtClinicQualifiersIds.Rows.Count - 1

            '            For cnt = 0 To dt.Rows.Count - 1

            '                If (dtClinicQualifiersIds.Rows(cntr)("nQualifierID").ToString() = Convert.ToString(dt.Rows(cnt)("nID"))) Then
            '                    c1ClinicIDs.SetData(cnt + 1, 3, dtClinicQualifiersIds.Rows(cntr)("sValue"))
            '                End If
            '            Next

            '        Next

            '    End If
            'End If

            Dim objClsglosettings As New GeneralSettings(gstrConnectionString)
            Dim dtQualifierId As New DataTable
            dtQualifierId = objClsglosettings.getIDQualifiers(COL_CLINICMODE, txtName.Tag, True)
            c1ClinicIDs.DataSource = dtQualifierId
            Me.Cursor = Cursors.WaitCursor
            'tbPageOther = tbcClinic.TabPages(1)
            'tbcClinic.TabPages.Remove(tbPageOther)
            TabControl1.SelectedTab = TabPage1
            blnLogoChanged = False
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage
            dttimezn = gloGlobal.clsTimeZone.getAllTimeZome()
            Dim strtimezn As String = gloGlobal.clsTimeZone.getSelectedTimeZome()
            strtimezn = strtimezn

            cmbtimezn.DataSource = dttimezn
            cmbtimezn.DisplayMember = "TimeZoneName"
            cmbtimezn.ValueMember = "TimeZoneNameMinutes"
            If Not dttimezn Is Nothing Then
                Dim dr As DataRow() = dttimezn.Select("TimeZoneName='" & strtimezn.Replace("'", "''") & "'")
                If (dr.Length > 0) Then
                    Dim indtimezn As Integer = dttimezn.Rows.IndexOf(dr(0))
                    If (indtimezn >= 0) Then
                        cmbtimezn.SelectedIndex = indtimezn
                    End If
                End If
                dr = Nothing
                If cmbtimezn.SelectedIndex = -1 Then
                    cmbtimezn.SelectedIndex = 0
                End If
            End If
            Me.Cursor = Cursors.Default

            txtPhoneNo.AllowValidate = False
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub



    'Private Sub EnableDisableControls(ByVal blnStatus As Boolean)
    '    Try
    '        txtAddress.Enabled = blnStatus
    '        txtCity.Enabled = blnStatus
    '        txtContactPersonAddress.Enabled = blnStatus
    '        txtContactPersonEmail.Enabled = blnStatus
    '        txtContactPersonMobileNo.Enabled = blnStatus
    '        txtContactPersonName.Enabled = blnStatus
    '        txtContactPersonPhoneNo.Enabled = blnStatus
    '        txtContactPersonFAX.Enabled = blnStatus
    '        txtEmailAddress.Enabled = blnStatus
    '        txtFAX.Enabled = blnStatus
    '        txtLogoPath.Enabled = blnStatus
    '        txtMobileNo.Enabled = blnStatus
    '        txtName.Enabled = blnStatus
    '        txtPhoneNo.Enabled = blnStatus
    '        txtState.Enabled = blnStatus
    '        txtStreet.Enabled = blnStatus
    '        txtTAXID.Enabled = blnStatus
    '        txtURL.Enabled = blnStatus
    '        txtZIP.Enabled = blnStatus
    '        picLogo.Enabled = blnStatus
    '        btnBrowse.Enabled = blnStatus
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click
        Try
            If Trim(txtName.Text) = "" Then
                MessageBox.Show("Please enter the Clinic Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabControl1.SelectedIndex = 0
                '******26 th Oct 2007 
                txtName.Focus()
                Exit Sub
            End If

            'If Trim(txtLabel.Text) = "" Then
            '    MessageBox.Show("Please enter the Label ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '******By Kishor
            '    TabControl1.SelectedIndex = 0
            '    txtLabel.Focus()
            '    Exit Sub
            'End If

            'If Trim(txtAddress1.Text) = "" Then
            '    MessageBox.Show("Please enter the Clinic Address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '******By Sandip Deshmukh 26 th Oct 2007 
            '    '******to select the tabitem on the form if cause validation
            '    TabControl1.SelectedIndex = 0
            '    '******26 th Oct 2007 
            '    txtAddress1.Focus()
            '    Exit Sub
            'End If

            If Trim(oAddressContol.txtAddress1.Text) = "" Then
                MessageBox.Show("Please enter the Clinic Address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '******By Sandip Deshmukh 26 th Oct 2007 
                '******to select the tabitem on the form if cause validation
                TabControl1.SelectedIndex = 0
                '******26 th Oct 2007 
                txtAddress1.Focus()
                Exit Sub
            End If

            'If Not IsNumeric(txtPhoneNo.Text.Trim) Then
            '    MessageBox.Show("Please enter valid phone number. Phone number should contain only digits", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtPhoneNo.Text = ""
            '    txtPhoneNo.Focus()
            '    Exit Sub
            'Else
            '    If txtPhoneNo.Text.Length > 10 Then
            '        MessageBox.Show("Please enter valid phone number. Phone number should contain only 10 digits", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        txtPhoneNo.Text = ""
            '        txtPhoneNo.Focus()
            '        Exit Sub
            '    End If
            'End If
            ''Sandip Darade 20090720
            ''Phone no to be save d with mask format
            'Line comented by dipak 20090914 as control change to windows mask control to glomask control property no longer used
            ' txtPhoneNo.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            'code commented by dipak 2009014 as glo maskcontrol validation checked by isvalidated method
            'If Len(Trim(txtPhoneNo.Text.Replace(" ", ""))) > 0 And Len(Trim(txtPhoneNo.Text.Replace(" ", ""))) < 10 Then
            '    MessageBox.Show("Phone Details Incomplete", "Clinic Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '******By Sandip Deshmukh 26 th Oct 2007 
            '    '******to select the tabitem on the form if cause validation
            '    TabControl1.SelectedIndex = 0
            '    '******26 th Oct 2007 
            '    txtPhoneNo.Focus()
            '    Exit Sub
            'End If
            'end comment by dipak 20090914
            'Line comented by dipak 20090914 as control change to windows mask control to glomask control property no longer used
            ' txtPhoneNo.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
            'end coment by dipak 20090914
            ''
            ''Sandip Darade 20090720
            ''mobile no to be save d with mask format
            'Line comented by dipak 20090914 as control change to windows mask control to glomask control property no longer used
            '  txtMobileNo.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            'end coment by dipak

            'line commented by dipak 20090914 as glomask chack validation by IsValidated Method 
            'If Len(Trim(txtMobileNo.Text.Replace(" ", ""))) > 0 And Len(Trim(txtMobileNo.Text.Replace(" ", ""))) < 10 Then
            '    MessageBox.Show("Mobile Phone Details Incomplete", "Clinic Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '******By Sandip Deshmukh 26 th Oct 2007 
            '    '******to select the tabitem on the form if cause validation
            '    TabControl1.SelectedIndex = 0
            '    '******26 th Oct 2007 
            '    txtMobileNo.Focus()
            '    Exit Sub
            'End If
            'end dipak 20090914

            'Line comented by dipak 20090914 as control change to windows mask control to glomask control property no longer used
            '  txtMobileNo.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
            'end comment by dipak 20090914
            '******By Sandip Deshmukh 15 Oct 2007 10.57 a.m. Bug# 321
            '******For bug reported the control for Zip is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            'If Len(Trim(txtZip.Text)) > 0 And Len(Trim(txtZip.Text)) < 10 Then
            '    MessageBox.Show("Zip Code Details Incomplete", "Clinic Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtZip.Focus()
            '    Exit Sub
            'End If

            'If Not IsNumeric(txtZip.Text) Then
            '    MessageBox.Show("Invalid Zip Code. Zip should contain only digits", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtZip.Focus()
            '    Exit Sub
            'Else
            '    If txtZip.Text.Trim.Length > 10 Then
            '        MessageBox.Show(" Zip should contain less than 10 or 10 digits", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        txtZip.Focus()
            '        Exit Sub
            '    End If
            'End If
            '******15 Oct 2007 10.57 a.m. Bug# 321



            '******By Sandip Deshmukh 15 Oct 2007 2.15 p.m. Bug# 323
            '******For bug reported the control for Phone No. is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            'line commented by dipak 20090914 as glomask chack validation by IsValidated Method 
            'If Len(Trim(txtContactPersonPhoneNo.Text)) > 0 And Len(Trim(txtContactPersonPhoneNo.Text)) < 10 Then
            '    MessageBox.Show("Phone Details Incomplete", "Clinic Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '******By Sandip Deshmukh 26 th Oct 2007 
            '    '******to select the tabitem on the form if cause validation
            '    TabControl1.SelectedIndex = 1

            '    '******26 th Oct 2007 
            '    txtContactPersonPhoneNo.Focus()
            '    Exit Sub
            'End If
            'end comment dipak 20090914

            '******15 Oct 2007 2.15 p.m. Bug# 323


            '******By Sandip Deshmukh 15 Oct 2007 11.15 a.m. Bug# 323
            '******For bug reported the control for Mobile No. is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            'line commented by dipak 20090914 as glomask chack validation by IsValidated Method 
            'If Len(Trim(txtContactPersonMobileNo.Text)) > 0 And Len(Trim(txtContactPersonMobileNo.Text)) < 10 Then
            '    MessageBox.Show("Mobile Phone Details Incomplete", "Clinic Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '******By Sandip Deshmukh 26 th Oct 2007 
            '    '******to select the tabitem on the form if cause validation
            '    TabControl1.SelectedIndex = 1

            '    '******26 th Oct 2007 
            '    txtContactPersonMobileNo.Focus()
            '    Exit Sub
            'End If

            ''******15 Oct 2007 11.15 a.m. Bug# 323
            'end comment dipak 20090914

            'code added by dipak 20090914 to validate Mask controls
            'Shubhangi 20091201
            'for tab change use manual validation coz due to lost event property it is displaying the same message repetadlly

            txtPhoneNo.AllowValidate = True
            If Not (txtPhoneNo.IsValidated) Then
                txtPhoneNo.AllowValidate = False
                TabControl1.SelectedIndex = 0
                txtPhoneNo.Focus()
                Exit Sub
            End If
            txtMobileNo.AllowValidate = True
            If Not txtMobileNo.IsValidated Then
                txtMobileNo.AllowValidate = False
                TabControl1.SelectedIndex = 0
                txtMobileNo.Focus()
                Exit Sub
            End If
            'Shubhangi 20091202
            txtFax.AllowValidate = True
            If Not txtFax.IsValidated Then
                txtFax.AllowValidate = False
                TabControl1.SelectedIndex = 0
                txtFax.Focus()
                Exit Sub
            End If
            'shubhangi 20091202
            txtContactPersonFax.AllowValidate = True
            If Not txtContactPersonFax.IsValidated Then
                txtContactPersonFax.AllowValidate = False
                TabControl1.SelectedIndex = 1
                txtContactPersonFax.Focus()
                Exit Sub
            End If
            'shubhangi 20091202
            txtContactPersonMobileNo.AllowValidate = True
            If Not txtContactPersonMobileNo.IsValidated Then
                txtContactPersonMobileNo.AllowValidate = False
                TabControl1.SelectedIndex = 1
                txtContactPersonMobileNo.Focus()
                Exit Sub
            End If
            'Shubhangi 20091202
            txtContactPersonPhoneNo.AllowValidate = True
            If Not txtContactPersonPhoneNo.IsValidated Then
                txtContactPersonPhoneNo.AllowValidate = False
                TabControl1.SelectedIndex = 1
                txtContactPersonPhoneNo.Focus()
                Exit Sub
            End If

            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            If (txtEmailAddress.Text.Trim() <> "") Then
                ' If Regex.IsMatch(txtEmailAddress.Text.Trim(), "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") = False Then
                If (CheckEmailAddress(txtEmailAddress.Text.Trim()) = False) Then
                    MessageBox.Show("Please enter a valid email id.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtEmailAddress.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 08-09-2010
            ' For URL validation 
            If (txtURL.Text.Trim() <> "") Then
                'If Regex.IsMatch(txtURL.Text.Trim(), "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$") = False Then
                If (CheckURLAddress(txtURL.Text.Trim()) = False) Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtURL.Focus()
                    Exit Sub
                End If
            End If

            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            If (txtContactPersonEmail.Text.Trim() <> "") Then
                ' If Regex.IsMatch(txtContactPersonEmail.Text.Trim(), "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") = False Then
                If (CheckEmailAddress(txtContactPersonEmail.Text.Trim()) = False) Then
                    MessageBox.Show("Please enter a valid email id.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtContactPersonEmail.Focus()
                    Exit Sub
                End If
            End If



            'Clinic physical location address
            If (oAddressContolPL.txtAreaCode.TextLength > 0 And oAddressContolPL.txtAreaCode.TextLength < 4) Then
                If (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                    TabControl1.SelectedIndex = 2
                    oAddressContolPL.txtAreaCode.Select()
                    oAddressContolPL.txtAreaCode.Focus()
                    Exit Sub
                End If
            End If

            If mskPLPager.Text <> "" Then
                If (mskPLPager.IsValidated = False) Then
                    TabControl1.SelectedIndex = 2
                    mskPLPager.Focus()
                    Exit Sub
                End If
            End If

            If maskedPLPhno.Text <> "" Then
                If (maskedPLPhno.IsValidated = False) Then
                    TabControl1.SelectedIndex = 2
                    maskedPLPhno.Focus()
                    Exit Sub
                End If
            End If


            If mskPLFax.Text <> "" Then
                If (mskPLFax.IsValidated = False) Then
                    TabControl1.SelectedIndex = 2
                    mskPLFax.Focus()
                    Exit Sub
                End If
            End If

            If (txtPLEMail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtPLEMail.Text.Trim()) = False Then
                    TabControl1.SelectedIndex = 2
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPLEMail.Focus()
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default
            If (txtPLUrl.Text.Trim() <> "") Then
                If CheckURLAddress(txtPLUrl.Text.Trim()) = False Then
                    TabControl1.SelectedIndex = 2
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPLUrl.Focus()
                    Exit Sub
                End If
            End If
            'Clinic physical location address



            'Lines of code added by dipak 20090914 for save glomask control value with mask
            ''Code Commenetd by Mayuri:20100520-To save data without mask
            'txtPhoneNo.IncludeLiteralsAndPrompts = True
            'txtMobileNo.IncludeLiteralsAndPrompts = True
            'txtFax.IncludeLiteralsAndPrompts = True
            'txtContactPersonFax.IncludeLiteralsAndPrompts = True
            'txtContactPersonMobileNo.IncludeLiteralsAndPrompts = True
            'txtContactPersonPhoneNo.IncludeLiteralsAndPrompts = True
            'end addtion by dipak 20090914
            Dim objClinic As New clsClinic
            'Check Clinic already exists or not
            If blnModify = True Then
                If objClinic.CheckClinicExists(Trim(txtName.Text), txtName.Tag) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Clinic already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '******By Sandip Deshmukh 26 th Oct 2007 
                    '******to select the tabitem on the form if cause validation
                    TabControl1.SelectedIndex = 0
                    '******26 th Oct 2007 
                    objClinic = Nothing
                    txtName.Focus()
                    Exit Sub
                End If
            End If


            With objClinic
                .ClinicID = txtName.Tag
                .ClinicName = Trim(txtName.Text)
                .ContactName = Trim(txtClinicMaillingContact.Text)
                .Street = txtStreet.Text.Trim
                '.ClinicAddress1 = Replace(txtAddress1.Text.Trim, vbCrLf, " ")
                '.ClinicAddress2 = Replace(txtAddress2.Text.Trim, vbCrLf, " ") & ""  '' Sudhir 20090414 - If txtAddress2.text is Blank.. Replace produce NULL.
                '.City = txtCity.Text.Trim
                '.State = cmbState.Text.Trim
                '.ZIP = txtZip.Text.Trim
                .ClinicAddress1 = oAddressContol.txtAddress1.Text
                .ClinicAddress2() = oAddressContol.txtAddress2.Text
                .City = oAddressContol.txtCity.Text
                .State = oAddressContol.cmbState.Text
                .ZIP = oAddressContol.txtZip.Text
                .Country = oAddressContol.cmbCountry.Text
                .County = oAddressContol.txtCounty.Text

                .PhoneNo = txtPhoneNo.Text.Trim
                .MobileNo = txtMobileNo.Text.Trim
                .FAX = txtFax.Text.Trim
                .Email = txtEmailAddress.Text.Trim
                .URL = txtURL.Text.Trim
                .TAXID = txtTAXID.Text.Trim

                If txtTaxonomy.Text.Trim() <> "" Then
                    Dim _splitter As [String]()
                    _splitter = txtTaxonomy.Text.Split("-"c)
                    .TaxonomyCode = _splitter(0)
                Else
                    .TaxonomyCode = ""

                End If


                ''Added And Commanted By Debasish Das on 19th March 2010
                '' ''Sandip darade  20091113
                ''.ClinicNPI = ClinicNPI
                .ClinicNPI = txtClinicNPI.Text.Trim
                ''*******************************************************
                If blnLogoChanged = True Then
                    If IsNothing(picLogo.Image) = False Then
                        .ClinicLogo = picLogo.Image
                    End If
                    .bISSameImage = False
                End If

                'Code Added by Mayuri:20090926
                'If image doesn't present in PicLogo PictureBox then
                If IsNothing(picLogo.Image) = True Then
                    picLogo.Image = Nothing
                End If
                'end Code added by Mayuri:20090926

                .ContactPersonName = txtContactPersonName.Text.Trim
                .ContactPersonAddress1 = txtContactPersonAddress1.Text.Trim
                .ContactPersonAddress2 = txtContactPersonAddress2.Text.Trim
                .ContactPersonPhone = txtContactPersonPhoneNo.Text.Trim
                .ContactPersonMobile = txtContactPersonMobileNo.Text.Trim
                .ContactPersonEmail = txtContactPersonEmail.Text.Trim
                .ContactPersonFAX = txtContactPersonFax.Text.Trim

                ''sarika AUS User name 20080922
                '.AUSUserName = txtAUSUsername.Text.Trim
                ''----------

                'sarika siteID 20090708
                .SiteID = txtSiteID.Text.Trim()

                '--
                .ClinicLabel = txtLabel.Text
            End With



            'Clinic Physical Location Address
            With objClinic
                .ClinicPLContactName = txtPLContactName.Text.Trim()
                .ClinicPLAddressline1 = oAddressContolPL.txtAddress1.Text.Trim()
                .ClinicPLAddressline2 = oAddressContolPL.txtAddress2.Text.Trim()
                .ClinicPLCity = oAddressContolPL.txtCity.Text.Trim()
                .ClinicPLState = oAddressContolPL.cmbState.Text.Trim()
                .ClinicPLZIP = oAddressContolPL.txtZip.Text.Trim()
                .ClinicPLAreaCode = oAddressContolPL.txtAreaCode.Text.Trim()
                .ClinicPLCountry = oAddressContolPL.cmbCountry.Text.Trim()

                'If (oAddressContolPL.cmbCountry.Text.Trim().ToUpper() = "US") Then
                .ClinicPLCounty = oAddressContolPL.txtCounty.Text.Trim()
                'End If

                .ClinicPLPagerNo = mskPLPager.Text.Trim()
                .ClinicPLPhoneNo = maskedPLPhno.Text.Trim()
                .ClinicPLFAX = mskPLFax.Text.Trim()
                .ClinicPLEmail = txtPLEMail.Text.Trim()
                .ClinicPLURL = txtPLUrl.Text.Trim()
            End With

            c1ClinicIDs.FinishEditing()

            Dim dt As New DataTable()

            'Dim cntr As Integer
            dt = c1ClinicIDs.DataSource
            'dt.Columns.Add("bIsSystem")
            'dt.Columns.Add("userID")
            'If (dt.Rows.Count > 0) Then
            '    For cntr = 0 To dt.Rows.Count - 1
            '        'dt.Rows(cntr)("bIsSystem") = False
            '        dt.Rows(cntr)("userID") = Convert.ToInt64(appSettings("userID"))
            '    Next
            'End If

            ''NPI 
            'dt.Rows.Add()
            'dt.Rows(dt.Rows.Count - 1)("nQualifierID") = 1
            'dt.Rows(dt.Rows.Count - 1)("sCode") = ""
            'dt.Rows(dt.Rows.Count - 1)("sAdditionalDescription") = ""
            'dt.Rows(dt.Rows.Count - 1)("sValue") = txtClinicNPI.Text.Replace("'", "''")
            'dt.Rows(dt.Rows.Count - 1)("bIsSystem") = True
            'dt.Rows(dt.Rows.Count - 1)("userID") = Convert.ToInt64(appSettings("userID"))


            ''TAX ID
            'dt.Rows.Add()
            'dt.Rows(dt.Rows.Count - 1)("nQualifierID") = 5
            'dt.Rows(dt.Rows.Count - 1)("sCode") = ""
            'dt.Rows(dt.Rows.Count - 1)("sAdditionalDescription") = ""
            'dt.Rows(dt.Rows.Count - 1)("sValue") = txtTAXID.Text.Replace("'", "''")
            'dt.Rows(dt.Rows.Count - 1)("bIsSystem") = True
            'dt.Rows(dt.Rows.Count - 1)("userID") = Convert.ToInt64(appSettings("userID"))

            'dt.AcceptChanges()
            If blnModify = True Then

                If objClinic.UpdateClinicDetails(txtName.Tag, dt, Convert.ToInt64(appSettings("userID"))) = True Then
                    objClinic = Nothing
                    UpdateTimeZone()
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Unable to update Clinic Details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objClinic = Nothing
                End If

            Else
                If objClinic.InsertClinicDetails(dt, Convert.ToInt64(appSettings("userID"))) = True Then


                    UpdateErrorLog("Clinic Added sucessfully", mdlFunctions.enmErrorOccuredForm.Clinic, mdlFunctions.enmOperation.Add)
                    objClinic = Nothing
                    UpdateTimeZone()
                    Me.DialogResult = DialogResult.OK

                    Me.Close()
                    '_IsClinicAdded = True
                    'Dim frm As New frmgloEMRAdmin
                    ' frm.btnNew.Visible = False


                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Unable to insert Clinic Details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objClinic = Nothing
                End If

            End If
            'sarika AUS Username 20080922
            'check whether the column exists or not
            objClinic = New clsClinic
            If objClinic.CheckColumnExists() = True Then
                'update sExternalCode by AUSUsername

                objClinic.UpdateAUSUsername(txtAUSUsername.Text.Trim)

            End If
            '----
            objClinic = Nothing
            '-----------

            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has modified settings of " & txtName.Text.Trim & " Clinic.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '-------------

            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.DialogResult = DialogResult.None
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'sarika 27th feb
            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " Error modifying settings of " & txtName.Text.Trim & " Clinic.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
            'code added by dipak 20090914 to set default value as set in designer to avoid Disturb  other logic
            txtPhoneNo.IncludeLiteralsAndPrompts = False
            txtMobileNo.IncludeLiteralsAndPrompts = False
            txtFax.IncludeLiteralsAndPrompts = False
            txtContactPersonFax.IncludeLiteralsAndPrompts = False
            txtContactPersonMobileNo.IncludeLiteralsAndPrompts = False
            txtContactPersonPhoneNo.IncludeLiteralsAndPrompts = False
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TabControl1.SelectedTab = TabPage1

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub UpdateTimeZone()
        ' If (cmbtimezn.Text.Trim() <> "") Then
        gloGlobal.clsTimeZone.UpdateTimeZome(cmbtimezn.Text, cmbtimezn.SelectedValue.ToString())
        '   End If
    End Sub

    '******By Sandip Deshmukh 15 Oct 2007 10.57 a.m. Bug# 321
    '******For bug reported the control for Zip is 
    '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
    'Lines Commented by dipak 20090914 as Code Nolonger used because we need to allow user enter alphanumeric text in zip text box
    'Private Sub txtZip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress
    '    'MessageBox.Show(System.Convert.ToInt16(e.KeyChar))
    '    If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
    '        e.Handled = True
    '    End If
    'End Sub
    'end comment dipak 20090914
    '******15 Oct 2007 10.57 a.m. Bug# 321

    ''Commented By Dhruv 
    'Private Sub txtZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.LostFocus
    '    ' code Added By Dipak 20090914 for Show Zip Control on text Change of txtZip
    '    'code added for make Zip Control Invisible while los focus
    '    If oZipcontrol IsNot Nothing Then
    '        If _isZipItemSelected = False And oZipcontrol.C1GridList.Focused = False And oZipcontrol.Focused = False Then
    '            _isTextBoxLoading = True
    '            If txtCity.Text = "" AndAlso txtZip.Text = "" Then
    '                _TempZipText = txtZip.Text
    '            End If
    '            pnlInternalControl.Visible = False
    '            _isTextBoxLoading = False
    '        End If
    '    End If
    'End Sub
    ''End----------------Commented Dhruv 
    ''Commented By Dhruv 
    'Private Sub oZipcontrol_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
    '    'code added by dipak  20090914 for show ZipControl
    '    'code get a selected zip info  and store in variables
    '    Try
    '        If oZipcontrol.C1GridList.Row < 0 Then
    '            Exit Sub
    '        End If
    '        Dim _Zip As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString()
    '        Dim _City As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString()
    '        Dim _ID As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString()
    '        Dim _County As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString()
    '        Dim _State As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString()
    '        Dim _AreaCode As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString()

    '        _isTextBoxLoading = True
    '        'code assign a selected zip info to controls
    '        txtZip.Text = _Zip
    '        txtZip.Tag = _ID
    '        txtCity.Text = _City
    '        txtCity.Tag = _AreaCode
    '        cmbState.Text = _State

    '        _isTextBoxLoading = False
    '        _isZipItemSelected = True
    '        If pnlInternalControl.Visible = True Then
    '            pnlInternalControl.Visible = False
    '            txtCity.Focus()
    '        End If

    '        isSearchControlOpen = False
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
    '    End Try
    'End Sub
    ''End----------------Commented Dhruv 

    ''Commented By Dhruv 
    'Private Sub oZipcontrol_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
    '    'code added by dipak  20090914 for show ZipControl
    '    Try
    '        CloseInternalControl()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
    '    Finally
    '    End Try
    'End Sub
    ''End----------------Commented Dhruv 

    ''Commented By Dhruv 
    'Public Function OpenInternalControl(ByVal ControlType As gloPatient.gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
    '    'code added by dipak  20090914 for show ZipControl
    '    'code add control in display zip control here as parameter passed
    '    Dim _result As Boolean = False
    '    _isZipItemSelected = False
    '    Try

    '        If oZipcontrol IsNot Nothing Then
    '            CloseInternalControl()
    '        End If
    '        oZipcontrol = New gloPatient.gloZipcontrol(ControlType, False, 0, 0, 0, gloEMRAdmin.mdlGeneral.GetConnectionString())
    '        AddHandler oZipcontrol.ItemSelectedclick, AddressOf oZipcontrol_ItemSelected
    '        AddHandler oZipcontrol.InternalGridKeyDownclick, AddressOf oZipcontrol_InternalGridKeyDown
    '        oZipcontrol.ControlHeader = ControlHeader
    '        oZipcontrol.ShowHeader = False
    '        oZipcontrol.Dock = DockStyle.Fill
    '        pnlInternalControl.BringToFront()
    '        'pnlInternalControl.Visible = True
    '        pnlInternalControl.Controls.Add(oZipcontrol)
    '        If Not String.IsNullOrEmpty(SearchText) Then
    '            oZipcontrol.Search(SearchText, gloPatient.SearchColumn.Code)
    '        End If
    '        oZipcontrol.Show()
    '        _result = True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.[Error])
    '        _result = False
    '    Finally

    '    End Try

    '    isSearchControlOpen = True
    '    Return _result
    'End Function
    ''End----------------Commented Dhruv 

    ''Commented By Dhruv 
    'Private Function CloseInternalControl() As Boolean
    '    'code added by dipak  20090914 for show ZipControl
    '    ' to remove controls from panel
    '    If oZipcontrol IsNot Nothing Then

    '        _isTextBoxLoading = True
    '        For i As Integer = 0 To pnlInternalControl.Controls.Count - 1
    '            pnlInternalControl.Controls.RemoveAt(i)
    '        Next
    '        If oZipcontrol IsNot Nothing Then
    '            oZipcontrol.Dispose()
    '            oZipcontrol = Nothing
    '        End If
    '        _isTextBoxLoading = False
    '    End If
    '    Return _isTextBoxLoading
    '    'end dipak 20090914
    'End Function
    ''End----------------Commented Dhruv 



    ''Commented By Dhruv 
    'Private Sub txtZip_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.TextChanged
    '    'code added by dipak  20090914 for show ZipControl
    '    Try
    '        pnlInternalControl.BringToFront()
    '        'code added for show ZipControl except Form Loading And TextLoading(mean zipControl Apears only when user try enter zip using keyboard
    '        If isFormLoading = False And _isTextBoxLoading = False Then
    '            If pnlInternalControl.Visible = False Then
    '                pnlInternalControl.Visible = True
    '                OpenInternalControl(gloPatient.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
    '                oZipcontrol.FillControl(Convert.ToString(txtZip.Text.Trim()))
    '            Else
    '                oZipcontrol.FillControl(Convert.ToString(txtZip.Text.Trim()))
    '            End If
    '        End If
    '    Catch
    '    Finally
    '    End Try
    '    'end code by dipak 20090914
    'End Sub
    ''End----------------Commented Dhruv 


    ''Commented By Dhruv 
    'Private Sub txtZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZip.GotFocus
    '    ' code Added By Dipak 20090914 for Show Zip Control on text Change of txtZip
    '    'Zip Code Get Store Temperorly in _TempZipText variable
    '    Try
    '        _TempZipText = txtZip.Text.Trim()
    '    Catch
    '    End Try
    '    'End Code By Dipak
    'End Sub
    ''End----------------Commented Dhruv 


    ''Commented By Dhruv 
    'Private Sub txtZip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress
    '    ' code Added By Dipak 20090914 for Show Zip Control on text Change of txtZip
    '    Try

    '        If e.KeyChar = Convert.ToChar(13) Then
    '            '' HITS ENTER BUTTON ''
    '            If pnlInternalControl.Visible Then

    '                oZipcontrol_ItemSelected(Nothing, Nothing)
    '            End If
    '        ElseIf e.KeyChar = Convert.ToChar(27) Then
    '            '' HITS ESCAPE ''
    '            If txtZip.Text = "" AndAlso txtCity.Text = "" AndAlso cmbState.Text = "" Then
    '                _TempZipText = txtZip.Text
    '            End If
    '            txtCity.Focus()
    '        End If
    '        ''we are allowing only alphanumeric charactors for according referring the information from the link below  
    '        '' http://www.postalcodedownload.com/
    '        'The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
    '        ''an alphabetic character and "N" represents a numeric character. 
    '        If Not e.KeyChar = Convert.ToChar(8) Then
    '            If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-zA-Z]*$") = False Then
    '                e.Handled = True
    '            End If
    '        End If
    '    Catch

    '    End Try
    'End Sub
    ''End----------------Commented Dhruv 


    ''Commented By Dhruv 
    'Private Sub txtZip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtZip.KeyDown
    '    ' code Added By Dipak 20090914 for Show Zip Control on text Change of txtZip
    '    'Code added for detects '' HITS UP / DOWN '' and Set Focus to ZipControls Starting Row
    '    Try
    '        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
    '            '' HITS UP / DOWN ''
    '            If pnlInternalControl.Visible Then
    '                e.SuppressKeyPress = True
    '                e.Handled = True
    '                oZipcontrol.C1GridList.Focus()
    '                oZipcontrol.C1GridList.[Select](oZipcontrol.C1GridList.RowSel, 0)
    '            End If
    '        End If
    '    Catch
    '    End Try
    'End Sub
    ''End----------------Commented Dhruv 

    ''Commented By Dhruv 
    'Property procedure added by dipak 20090914 to access  private variable _isTextBoxLoading outside of module
    'Public Property isTextBoxLoading() As Boolean
    '    Get
    '        Return (_isTextBoxLoading)
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _isTextBoxLoading = value
    '    End Set
    'End Property
    ''End----------------Commented Dhruv 


    'end procedure addition by dipak 20090914
    'Code Added by Mayuri:20090926
    'To Clear Image
    Private Sub btnPicClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPicClear.Click
        picLogo.Image = Nothing
        txtLogoPath.Clear()
        blnLogoChanged = True
    End Sub
    'end Code by Mayuri:20090926

    Private Sub btnPicClear_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnPicClear.FlatStyle = FlatStyle.Flat
    End Sub
    Private Sub txtTAXID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTAXID.KeyPress
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtClinicNPI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClinicNPI.KeyPress
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub c1ClinicIDs_SetupEditor(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ClinicIDs.SetupEditor
        If e.Col = COL_VALUE Then
            CType(c1ClinicIDs.Editor, TextBox).MaxLength = 250
        End If

    End Sub

    Private Sub c1ClinicIDs_StartEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ClinicIDs.StartEdit
        c1ClinicIDs.Editor = CType(c1ClinicIDs.Editor, TextBox)
    End Sub

    Private Sub c1ClinicIDs_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1ClinicIDs.MouseMove
        gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub

    Private Sub TabControl1_Deselecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Deselecting
        e.Cancel = ValidationFailed
        ValidationFailed = False
    End Sub

    Private Sub mskPLPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskPLPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub


    Private Sub maskedPLPhno_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maskedPLPhno.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskPLFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskPLFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtPhoneNo_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhoneNo.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtMobileNo_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobileNo.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub btn_BrowseTaxonomy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_BrowseTaxonomy.Click
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Taxonomy, False, Me.Width)
            oListControl.ClinicID = txtName.Tag
            oListControl.ControlHeader = "Taxonomy"
            _CurrentControlType = gloListControl.gloListControlType.Providers
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception
        Finally
            pnl_tlsp_Top.Visible = False
            Panel2.Visible = False

        End Try
    End Sub

    Private Sub btn_ClearTaxonomy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearTaxonomy.Click
        txtTaxonomy.Clear()
    End Sub

    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        txtTaxonomy.Clear()

        If oListControl.SelectedItems.Count > 0 Then

            txtTaxonomy.Text = (oListControl.SelectedItems(0).Code.ToString() & "-") + oListControl.SelectedItems(0).Description.ToString()

        End If
        pnl_tlsp_Top.Visible = True
        Panel2.Visible = True
    End Sub
    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        pnl_tlsp_Top.Visible = True
        Panel2.Visible = True
    End Sub
End Class
