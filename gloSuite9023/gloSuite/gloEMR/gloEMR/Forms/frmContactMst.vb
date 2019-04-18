Public Class frmContactMst
    Inherits System.Windows.Forms.Form

    Private tbpage As TabPage
    Private Type As Boolean
    Private ID As Long
    Private ContactDBLayer As New ClsContactDBLayer(Type)

    Private AreaCode As Int64
    Private strContacts As String
    Public strData As String
    Private _IsePharmacy As Boolean = False
    'sarika 16th oct 07
    Private m_ContactID As Long

    Public Property ContactID() As Long
        Get
            Return m_ContactID
        End Get
        Set(ByVal value As Long)
            m_ContactID = value
        End Set
    End Property
    '------------------------------------

    Public Sub New(ByVal ContactType As Boolean, ByVal strContactType As String)
        MyBase.New()
        Type = ContactType
        strContacts = strContactType

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal ContactType As Boolean, ByVal ContactsId As Long, ByVal strcontacttype As String, Optional ByVal IsePharmacy As Boolean = False)
        MyBase.New()
        Type = ContactType
        ID = ContactsId
        strContacts = strcontacttype
        _IsePharmacy = IsePharmacy
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Private Sub btnNext1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext1.Click
        TabControl1.SelectedTab = TabControl1.Tabs.Item(1)

    End Sub

    Private Sub btnPrev1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev1.Click
        TabControl1.SelectedTab = TabControl1.Tabs.Item(0)
    End Sub

    Private Sub OKContactMST()
        Dim Arrlist As ArrayList
        If Type = False Then
            If Trim(txtname.Text) = "" Then
                'errName.SetError(txtname, "Name Required")
                MessageBox.Show("Enter Name", "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtname.Focus()
                Exit Sub
            Else
                'errName.SetError(txtname, "")
            End If
            If Trim(txtcontact.Text) = "" AndAlso strContacts <> "Others" AndAlso strContacts <> "Hospital" AndAlso strContacts <> "Insurance" AndAlso strContacts <> "Pharmacy" Then
                'errcontact.SetError(txtcontact, "Contact Required")
                MessageBox.Show("Enter Contact", "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtcontact.Focus()
                Exit Sub
            Else
                'errcontact.SetError(txtcontact, "")
            End If

        Else
            If Trim(txtfName.Text) = "" Then
                'errfname.SetError(txtfName, "First Name Required")
                MessageBox.Show("Enter First Name", "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtfName.Focus()
                Exit Sub
            Else
                'errfname.SetError(txtfName, "")
            End If
            If Trim(txtlName.Text) = "" Then
                'errlastname.SetError(txtlName, "Last Name Required")
                MessageBox.Show("Enter Last Name", "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtlName.Focus()
                Exit Sub
            Else
                'errlastname.SetError(txtlName, "")
            End If
        End If

        'code modified on 20070615
        If mskPhone.Text.Length > 1 AndAlso mskPhone.Text.Length < 10 Then
            MessageBox.Show("Invalid Phone Number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mskPhone.Focus()
            Exit Sub
        End If
        ''''

        'code modified on 5th oct 07 -- sarika
        If mskMobile.Text.Length > 1 AndAlso mskMobile.Text.Length < 10 Then
            MessageBox.Show("Invalid Mobile Number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mskMobile.Focus()
            Exit Sub
        End If
        ''''-------------------------------


        If ID = 0 Then  'add new contact

            Try
                Dim ArrInsurance(chkLstInsurance.CheckedItems.Count - 1) As Int64
                Dim ArrHospital(chkLstHospital.CheckedItems.Count - 1) As Int64
                Arrlist = SetData()
                'If strContacts = "Physician" Then
                Dim i As Integer
                If chkLstInsurance.CheckedItems.Count <> 0 Then

                    For i = 0 To chkLstInsurance.CheckedItems.Count - 1
                        'chkLstInsurance.SelectedItem = chkLstInsurance.CheckedItems(i)
                        'ArrInsurance(i) = CType(chkLstInsurance.SelectedValue, Int64)
                        Dim myobjList As myList
                        myobjList = chkLstInsurance.CheckedItems(i)
                        ArrInsurance(i) = myobjList.Index()
                        'arr(i) = CType(objchk.SelectedValue, Int64)
                    Next
                End If
                If chkLstHospital.CheckedItems.Count <> 0 Then

                    For i = 0 To chkLstHospital.CheckedItems.Count - 1
                        Dim myobjList As myList
                        myobjList = chkLstHospital.CheckedItems(i)
                        ArrHospital(i) = myobjList.Index
                        'chkLstHospital.SelectedItem = chkLstHospital.CheckedItems(i)
                        'ArrHospital(i) = CType(chkLstHospital.SelectedValue, Int64)
                        'arr(i) = CType(objchk.SelectedValue, Int64)
                    Next
                End If

                'End If


                'sarika 16th oct 07
                'ContactDBLayer.AddData(Arrlist, ArrInsurance, ArrHospital)

                m_ContactID = ContactDBLayer.AddData(Arrlist, ArrInsurance, ArrHospital)
                '--------------

            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else 'modify existing contact
            Try
                Arrlist = SetData()
                Dim ArrInsurance(chkLstInsurance.CheckedItems.Count - 1) As Int64
                Dim ArrHospital(chkLstHospital.CheckedItems.Count - 1) As Int64
                'If strContacts = "Physician" Then
                Dim i As Integer
                If chkLstInsurance.CheckedItems.Count <> 0 Then

                    For i = 0 To chkLstInsurance.CheckedItems.Count - 1
                        Dim myobjList As myList
                        myobjList = chkLstInsurance.CheckedItems(i)
                        ArrInsurance(i) = myobjList.Index()
                        'chkLstInsurance.SelectedItem = chkLstInsurance.CheckedItems(i)
                        'ArrInsurance(i) = CType(chkLstInsurance.SelectedValue, Int64)
                        'arr(i) = CType(objchk.SelectedValue, Int64)
                    Next
                End If
                If chkLstHospital.CheckedItems.Count <> 0 Then

                    For i = 0 To chkLstHospital.CheckedItems.Count - 1
                        Dim myobjList As myList
                        myobjList = chkLstHospital.CheckedItems(i)
                        ArrHospital(i) = myobjList.Index
                        'chkLstHospital.SelectedItem = chkLstHospital.CheckedItems(i)
                        'ArrHospital(i) = CType(chkLstHospital.SelectedValue, Int64)
                        'arr(i) = CType(objchk.SelectedValue, Int64)
                    Next
                End If

                'End If
                ContactDBLayer.UpdateData(Arrlist, ArrInsurance, ArrHospital)

                'If Type = False Then
                '    ContactDBLayer.UpdateSpeciality(ID, cmbSpeciality.SelectedValue, strContacts)
                'End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        ''''' code to default select current updated data
        If Type = False Then
            strData = txtname.Text
        Else
            'strData = txtfName.Text '+ Space(1) + txtmName.Text + Space(1) + txtlName.Text
            strData = txtfName.Text '+ "|" + txtmName.Text + "|" + txtlName.Text
        End If
        '''''

        ContactDBLayer = Nothing
        Me.Close()
    End Sub


    Private Sub frmContactMst_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If _IsePharmacy = True Then
                txtname.ReadOnly = True
                txtcontact.ReadOnly = True
                mskPhone.ReadOnly = True
                txtFax.ReadOnly = True
                txtPager.ReadOnly = True
                mskMobile.ReadOnly = True
                txtEmail.ReadOnly = True
                txtURL.ReadOnly = True
                txtAddressLine1.ReadOnly = True
                txtAddressLine2.ReadOnly = True
                txtCity.ReadOnly = True
                cmbState.Enabled = False
                txtZip.ReadOnly = True
                txtNotes.ReadOnly = True

                


                ts_btnOk.Visible = False
            Else
                txtname.ReadOnly = False
                txtcontact.ReadOnly = False
                mskPhone.ReadOnly = False
                txtFax.ReadOnly = False
                txtPager.ReadOnly = False
                mskMobile.ReadOnly = False
                txtEmail.ReadOnly = False
                txtURL.ReadOnly = False
                txtAddressLine1.ReadOnly = False
                txtAddressLine2.ReadOnly = False
                txtCity.ReadOnly = False
                cmbState.Enabled = True
                txtZip.ReadOnly = False
                txtNotes.ReadOnly = False

                ts_btnOk.Visible = True
            End If
            Dim arrlist As ArrayList
            'ContactDBLayer = New ClsContactDBLayer(Type)
            ContactDBLayer.ContactType = Type
            GroupBox1.BringToFront()
            'Me.Text = Me.Text & " for - " & strContacts
            'tbpage = TabControl1.TabPages(1)
            'tbpage = CType(TabControl1.Tabs.Item(1) , TabPage)
            'tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1)

            If Type = False Then
                GroupBox2.Visible = True
                'TabControl1.TabPages.RemoveAt(1)
                TabControl1.Tabs.Item(1).Visible = True
                btnNext1.Visible = True

                'TabControl1.Container.Remove(1)
                GroupBox3.Visible = False
                txtname.Focus()
                'btnNext.Visible = False
                'GroupBox8.Visible = False
            Else
                btnNext1.Visible = True
                txtfName.Focus()
                GroupBox2.Visible = False
                GroupBox3.Visible = True
                'btnNext.Visible = True
                If strContacts = "Physician" Then

                Else
                    'TabControl1.TabPages.RemoveAt(1)
                    ' code modify on 20070718
                    TabControl1.Tabs.Item(1).Visible = False
                    btnNext1.Visible = False
                End If
            End If

            If ID <> 0 Then
                arrlist = ContactDBLayer.FetchDataForUpdate(ID, strContacts)
                If arrlist.Count > 0 Then
                    GetData(arrlist)
                    'TextBox1.Text = CType(arrlist.Item(0), System.String)
                    'ComboBox1.Text = CType(arrlist.Item(1), System.String)

                End If
                'If strContacts = "Physician" Then
                Dim dt As DataTable
                dt = ContactDBLayer.FetchContactsDetail(ID, "I")
                Dim i As Integer
                Dim j As Integer
                If (IsNothing(dt) = False) Then


                  
                    For i = 0 To dt.Rows.Count - 1
                        For j = 0 To chkLstInsurance.Items.Count - 1
                            Dim myobjlist As myList
                            myobjlist = chkLstInsurance.Items(j)
                            'chkLstInsurance.SelectedItem = chkLstInsurance.Items(j)
                            'If dt.Rows.Item(i)(0) = chkLstInsurance.SelectedValue Then
                            If dt.Rows.Item(i)(0) = myobjlist.Index Then
                                chkLstInsurance.SetItemChecked(j, True)
                                Exit For
                            End If

                        Next
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If

                dt = ContactDBLayer.FetchContactsDetail(ID, "H")
                If (IsNothing(dt) = False) Then
                    For i = 0 To dt.Rows.Count - 1
                        For j = 0 To chkLstHospital.Items.Count - 1
                            Dim myobjlist As myList
                            myobjlist = chkLstHospital.Items(j)

                            'chkLstHospital.SelectedItem = chkLstHospital.Items(j)
                            'If dt.Rows.Item(i)(0) = chkLstHospital.SelectedValue Then
                            If dt.Rows.Item(i)(0) = myobjlist.Index Then
                                chkLstHospital.SetItemChecked(j, True)
                                Exit For
                            End If

                        Next
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If
            End If


            ' fill state data in combobox
            Dim dtState As DataTable
            Dim PatientRegistration As ClsPatientRegistrationDBLayer
            PatientRegistration = New ClsPatientRegistrationDBLayer
            dtState = PatientRegistration.FillControls("T")
            PatientRegistration.Dispose()
            PatientRegistration = Nothing
            If Not IsNothing(dtState) Then
                Dim i As Integer
                For i = 0 To dtState.Rows.Count - 1
                    cmbState.Items.Add(dtState.Rows.Item(i)(0))
                Next
                dtState.Dispose()
                dtState = Nothing
            End If

            'Call btnEnableDisable()
            If Type = False Then
                ' arrlist = New ArrayList
                arrlist = ContactDBLayer.selectSpeciality(ID, strContacts)
                cmbSpeciality.SelectedValue = arrlist.Item(0)
                'cmbSpeciality.Text = arrlist.Item(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub btnEnableDisable()
        If Type = False Then
            'GroupBox2.Visible = True
            If txtname.Text.Trim.Length = 0 Then
                ts_btnOk.Enabled = False
            Else
                ts_btnOk.Enabled = True
            End If
        Else
            If txtfName.Text.Trim.Length = 0 Then
                ts_btnOk.Enabled = False
            Else
                ts_btnOk.Enabled = True
            End If
        End If
    End Sub

    Friend Sub FillControls()
        Try

            Dim dts As DataTable
            Dim i As Integer

            dts = ContactDBLayer.FillControls("I")

            'chkLstInsurance.DataSource = dts
            If (IsNothing(dts) = False) Then


                For i = 0 To dts.Rows.Count - 1
                    chkLstInsurance.Items.Add(New myList(dts.Rows.Item(i)(0), dts.Rows.Item(i)(1)))
                Next
                dts.Dispose()
                dts = Nothing
            End If

            dts = ContactDBLayer.FillControls("H")

            'chkLstHospital.DataSource = dts
            'chkLstHospital.DisplayMember = dts.Columns(1).ColumnName
            'chkLstHospital.ValueMember = dts.Columns(0).ColumnName
            If (IsNothing(dts) = False) Then
                For i = 0 To dts.Rows.Count - 1
                    chkLstHospital.Items.Add(New myList(dts.Rows.Item(i)(0), dts.Rows.Item(i)(1)))
                Next
                dts.Dispose()
                dts = Nothing
            End If
            dts = ContactDBLayer.FillControls("S")
            cmbSpeciality.DataSource = dts
            If (IsNothing(dts) = False) Then
                cmbSpeciality.DisplayMember = dts.Columns(1).ColumnName
                cmbSpeciality.ValueMember = dts.Columns(0).ColumnName
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        cmbSpeciality.SelectedIndex = -1

    End Sub

    Private Function SetData() As ArrayList
        '@nContactId  numeric(18,0) output,      
        '@sContactType varchar(15),      
        '@sStreet varchar(50),      
        '@sCity  varchar(50),      
        '@sState  varchar(10),      
        '@sZip  varchar(50),      
        '@sPhone  varchar(50),      
        '@sFax  varchar(50),      
        '@sMobile varchar(50),      
        '@sPager  varchar(50),      
        '@sEmail  varchar(50),      
        '@sURL  varchar(50),      
        '@sNotes  varchar(50),      
        '@sName  varchar(50),      
        '@sFirstName varchar(50)=null, --null for hosp,insur,pharm       
        '@sMiddleName varchar(50)=null, --null for hosp,insur,pharm       
        '@sLastName varchar(50)=null, --null for hosp,insur,pharm       
        '@sGender varchar(50)=null, --null for hosp,insur,pharm       
        '@nSpecialtyId numeric(18,0)=null, --null for hosp,insur,pharm 

        '------------ New Field sDegree Added 20071122   
        '@sDegree varchar(50)=null, --null for hosp,insur,pharm       
        '------------  
        '--@nInsuranceID  numeric(18,0)=null, --for hosp,insur,pharm       
        '--@sHospitalAffiliation varchar(50)=null, --for hosp,insur,pharm       
          

        '@sContact		varchar(50)=null 	--null for physician and others
        Dim ArrList As New ArrayList
        ArrList.Add(ID)
        ArrList.Add(strContacts)
        ArrList.Add(Trim(txtAddressLine1.Text))
        ArrList.Add(Trim(txtCity.Text))
        ArrList.Add(Trim(cmbState.Text))
        ArrList.Add(Trim(txtZip.Text))
        ArrList.Add(Trim(mskPhone.Text))
        ArrList.Add(Trim(txtFax.Text))
        ArrList.Add(Trim(mskMobile.Text))
        ArrList.Add(Trim(txtPager.Text))
        ArrList.Add(Trim(txtEmail.Text))
        ArrList.Add(Trim(txtURL.Text))
        ArrList.Add(Trim(txtNotes.Text))
        ArrList.Add(Trim(txtname.Text))
        If Type = True Then
            ArrList.Add(Trim(txtfName.Text))
            ArrList.Add(Trim(txtmName.Text))
            ArrList.Add(Trim(txtlName.Text))

            If rbGender1.Checked = True Then
                ArrList.Add(rbGender1.Text)
            ElseIf rbGender2.Checked = True Then
                ArrList.Add(rbGender2.Text)
            End If
        End If
        'If strContacts = "Physician" Then
        ArrList.Add(cmbSpeciality.SelectedValue)


        ' End If
        If Type = False Then
            ArrList.Add(Trim(txtcontact.Text))
        End If

        ''''''''''added by Anil on 20071122 for new "sDegree" field
        ArrList.Add(Trim(txtDegree.Text))
        ArrList.Add(Trim(txtAddressLine2.Text))
        '''''''''''''''

        Return ArrList
    End Function

    Private Sub GetData(ByVal Arrlist As ArrayList)
        txtAddressLine1.Text = CType(Arrlist.Item(0), System.String)
        txtCity.Text = CType(Arrlist.Item(1), System.String)
        cmbState.Text = CType(Arrlist.Item(2), System.String)
        txtZip.Text = CType(Arrlist.Item(3), System.String)
        mskPhone.Text = CType(Arrlist.Item(4), System.String)
        txtFax.Text = CType(Arrlist.Item(5), System.String)
        mskMobile.Text = CType(Arrlist.Item(6), System.String)
        txtPager.Text = CType(Arrlist.Item(7), System.String)
        txtEmail.Text = CType(Arrlist.Item(8), System.String)
        txtURL.Text = CType(Arrlist.Item(9), System.String)
        txtNotes.Text = CType(Arrlist.Item(10), System.String)
        If strContacts = "Physician" Then
            If IsDBNull(Arrlist.Item(21)) Then
                txtAddressLine2.Text = ""
            Else
                txtAddressLine2.Text = CType(Arrlist.Item(21), System.String)
            End If

        Else
            If IsDBNull(Arrlist.Item(13)) Then
                txtAddressLine2.Text = ""
            Else
                txtAddressLine2.Text = CType(Arrlist.Item(13), System.String)
            End If

        End If

        If Type = True Then
            txtfName.Text = CType(Arrlist.Item(11), System.String)
            txtmName.Text = CType(Arrlist.Item(12), System.String)
            txtlName.Text = CType(Arrlist.Item(13), System.String)
            If CType(Arrlist.Item(14), System.String) = rbGender1.Text Then
                rbGender1.Checked = True
            ElseIf CType(Arrlist.Item(14), System.String) = rbGender2.Text Then
                rbGender2.Checked = True
            End If
            'cmbSpeciality.SelectedValue = CType(Arrlist.Item(15), System.Int32)
            'cmbInsurance.SelectedValue = CType(Arrlist.Item(16), System.Int32)
            If strContacts = "Physician" Then
                cmbSpeciality.SelectedValue = CType(Arrlist.Item(15), System.Int64)
                cmbSpeciality.Text = CType(Arrlist.Item(17), System.String)

                ''''added by Anil on 20071122 for new "sDegree" field
                txtDegree.Text = CType(Arrlist.Item(20), System.String)
                '''''

                'cmbInsurance.Text = CType(Arrlist.Item(18), System.String)
                'txtHospitalAffiliation.Text = CType(Arrlist.Item(19), System.String)
            End If
        Else
            txtname.Text = CType(Arrlist.Item(11), System.String)
            txtcontact.Text = CType(Arrlist.Item(12), System.String)

        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    'Private Sub btnOK_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
    '    Dim Arrlist As ArrayList
    '    If ID = 0 Then
    '        If Type = False Then
    '            If txtname.Text = "" Or txtcontact.Text = "" Then
    '                MsgBox("Please Fill in the Data Completely")
    '                Exit Sub
    '            End If
    '        Else
    '            If txtfName.Text = "" Or txtlName.Text = "" Then
    '                MsgBox("Please Fill in the Data Completely")
    '                Exit Sub
    '            End If
    '        End If
    '        Try

    '            Arrlist = SetData()
    '            ContactDBLayer.AddData(Arrlist)

    '        Catch ex As SqlClient.SqlException
    '            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    Else
    '        Try
    '            Arrlist = SetData()
    '            ContactDBLayer.UpdateData(Arrlist)

    '        Catch ex As SqlClient.SqlException
    '            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        End Try
    '    End If
    '    ContactDBLayer = Nothing
    '    Me.Close()
    'End Sub

    Private Sub btnClose_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBox8.BringToFront()
        GroupBox1.SendToBack()
    End Sub

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GroupBox1.BringToFront()

        GroupBox8.SendToBack()
    End Sub

    Private Sub txtfName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtfName.Validating
        'If Trim(txtfName.Text) = "" Then
        '    errfname.SetError(txtfName, "First Name Required")
        '    txtfName.Focus()
        'Else
        '    errfname.SetError(txtfName, "")
        'End If
    End Sub

    Private Sub txtlName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtlName.Validating
        'If Trim(txtlName.Text) = "" Then
        '    errlastname.SetError(txtlName, "Last Name Required")
        '    txtlName.Focus()
        'Else
        '    errlastname.SetError(txtlName, "")


        'End If
    End Sub

    Private Sub txtname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        'If Trim(txtname.Text) = "" Then
        '    errName.SetError(txtname, "Name Required")
        '    txtname.Focus()
        'Else
        '    errName.SetError(txtname, "")
        'End If
    End Sub

    Private Sub txtcontact_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtcontact.Validating
        'If Trim(txtcontact.Text) = "" Then
        '    errcontact.SetError(txtcontact, "Contact Required")
        '    txtcontact.Focus()
        'Else
        '    errcontact.SetError(txtcontact, "")
        'End If
    End Sub

    'Private Sub btnOK_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnOK, "Save Contacts")
    'End Sub

    'Private Sub btnClose_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnClose, "Close Contacts")

    'End Sub

    Private Sub txtZip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtZip.Validating
        If Trim(txtZip.Text) <> "" Then
            Try
                Dim dt As DataTable
                Dim PatientRegistration As ClsPatientRegistrationDBLayer
                PatientRegistration = New ClsPatientRegistrationDBLayer
                dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtZip.Text)))
                PatientRegistration.Dispose()
                PatientRegistration = Nothing
                'dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtZip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtCity.Text = dt.Rows(0).Item(0)
                        AreaCode = dt.Rows(0).Item(2)
                        'txtCounty.Text = dt.Rows(0).Item(3)
                        cmbState.Text = dt.Rows(0).Item(1)
                        If Len(Trim(mskPhone.Text)) < 10 Then
                            mskPhone.Text = "(" & AreaCode & ")-___-____"
                        ElseIf Len(Trim(mskPhone.Text)) = 10 Then
                            mskPhone.Text = "(" & AreaCode & ")-" & Mid(mskPhone.Text, 4, 3) & "-" & Mid(mskPhone.Text, 7, 4)
                        End If
                        'If Len(Trim(mskPhone.ClipText)) < 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-___-____"
                        'ElseIf Len(Trim(mskPhone.ClipText)) = 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-" & Mid(mskPhone.ClipText, 4, 3) & "-" & Mid(mskPhone.ClipText, 7, 4)
                        'End If
                    Else
                        txtCity.Text = ""
                        'txtCounty.Text = ""
                        cmbState.Text = ""
                        mskPhone.Mask = ""
                        mskPhone.Text = ""
                        mskPhone.Mask = "(###)-###-####"
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtfName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfName.TextChanged
        'Call btnEnableDisable()
    End Sub

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged
        'Call btnEnableDisable()
    End Sub

    Private Sub txtmName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmName.TextChanged
        'If txtmName.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtlName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlName.TextChanged
        'If txtlName.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtStreet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddressLine1.TextChanged
        'If txtStreet.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtCity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity.TextChanged
        'If txtCity.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtZip_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtZip.TextChanged
        'If txtZip.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtFax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFax.TextChanged
        'If txtFax.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtPager_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPager.TextChanged
        'If txtPager.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        'If txtEmail.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtURL.TextChanged
        'If txtURL.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.TextChanged
        'If txtNotes.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub txtcontact_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontact.TextChanged
        'If txtcontact.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub cmbState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        'If cmbState.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub cmbSpeciality_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSpeciality.SelectedIndexChanged
        'If cmbSpeciality.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub
  
    Private Sub mskMobile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskMobile.TextChanged
        'If mskMobile.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    Private Sub mskPhone_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskPhone.TextChanged
        'If mskPhone.Text.Trim.Length = 0 Then
        '    btnOK_new.Enabled = False
        'Else
        '    btnOK_new.Enabled = True
        'End If
    End Sub

    
    Private Sub GroupBox5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox5.Enter

    End Sub

  
    Private Sub tls_ContactMaster_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ContactMaster.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OKContactMST()

                Case "Cancel"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class
