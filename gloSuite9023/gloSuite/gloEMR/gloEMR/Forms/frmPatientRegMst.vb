Imports System.IO
Imports System.Text
Imports gloUserControlLibrary
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared



Public Class frmPatientRegMst

    Private WithEvents dgCustomGrid As CustomDataGrid
    Private ArrPatientRegistration As ArrayList
    Private ArrPatientInsurance As ArrayList
    Private ArrPatientOriginalData As New ArrayList
    Private ReferralCount As Integer

    Private _PatientDirectiveStatus As Boolean = False
    Dim mynode As TreeNode
    Private intId As Long
    Private PatientRegistration As New ClsPatientRegistrationDBLayer
    Private key As Int16
    Dim myCam As clsWebCam
    Dim blnPhotoModified As Boolean = False
    Public intStatus As Int16 '' 2= Referral , 4 = Physician
    Private AreaCode As Int64
    Dim dt As DataTable
    Dim InsuranceIDs As Collection
    Dim nCollectionCounter As Integer = 0

    ''''' next 4 array use for any modification on form and Close button click 
    Dim _arrayGetData As ArrayList
    Dim _arraySetData As ArrayList
    Dim _arrReferals As ArrayList
    Dim _arrInsurance As ArrayList
    Dim nInsuranceNodeCount As Integer = 0
    Dim strInsuranceText As String = ""
    Dim strSubPolicy As String = ""
    Dim strSubId As String = ""
    Dim strSubName As String = ""
    Dim strEmployer As String = ""
    Dim strGroup As String = ""
    Dim nPhone As String = ""
    Dim dtDob As String = ""
    Dim chkPrimaryOrNot As Boolean
    Dim bTextChangeFlag_insurance As Boolean = False
    '''''

    ' flag for the data modification by user
    Dim bTextChangeFlag As Boolean = False

    Dim pnl As New System.Windows.Forms.Panel
    Private WithEvents oC1flex As gloUC_CustomSearchInC1Flexgrid

    '' For Record Level Locking
    Private _blnRecordLock As Boolean = False
    ''sarika 17th aug 07
    'Private PatientID As Int64 = 0

    Private _blnPrint As Boolean = False

    ''''''Declare by Anil on20071119
    Private _blnAutoPatientCode As Boolean

    Public IsPharmacy As Boolean = False
    'code added by supriya 24/7/2008,providerid sentfrom rxrequest form
    Private RefillReqProviderID As String = ""

    'sarika 1st august 08
    Dim _blnScanDoc As Boolean = False

    Private Sub frmPatientRegMst_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            If _blnRecordLock = False Then
                UnLock_Transaction(TrnType.PatientRegistration, gnPatientID, 0, Now)
            End If
            '' <><><> Unlock the Record <><><>
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '' 
    Private Sub frmPatientRegMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim arrlist As ArrayList

        'sarika 1st august 08
        _blnScanDoc = False
        '-------------------------

        'sarika 17th aug 07
        'txtInsuranceNew.Enabled = False

        chkSameasPatient.Enabled = True
        chkPrimary.Enabled = True
        '----------

        Try
            TabPage4.Visible = False

            'Vinayak - 14 Feb 07 //
            gDMSCategory_PatientDirective = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_PatientDirective) = False Then
                gDMSCategory_PatientDirective = objSettings.DMSCategory_PatientDirective
            End If
            'objSettings = Nothing

            '/Vinayak - 14 Feb 07 //

            FillControls()
            If blnHPIEnabled = False Then
                cmbLocation.Enabled = False
            End If

            '''''''''''''''''''Code is added by Anil on 20071119
            If objSettings.AutoGeneratePatientCode = True Then
                Dim sqlquery As String = ""
                Dim nPatCode As Int64 = 0
                sqlquery = "Select isnull(MAX(Cast(sPatientCode AS Numeric )),0) + 1  from Patient  where  ISnumeric(sPatientCode)=1"
                Dim con As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqlcmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand(sqlquery, con)
                con.Open()
                nPatCode = sqlcmd.ExecuteScalar
                con.Close()
                txtPatientCode.Text = nPatCode.ToString
                txtPatientCode.Enabled = False
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''
            objSettings = Nothing
            mynode = New TreeNode("Insurance")
            mynode.ImageIndex = 0
            mynode.SelectedImageIndex = 0
            trInsurance.Nodes.Add(mynode)

            If intId <> 0 Then  'Update mode
                arrlist = PatientRegistration.FetchDataForUpdate(ID)  'Fetch Patient Record
                If Not IsNothing(arrlist) Then
                    GetData(arrlist)
                End If
                arrlist = PatientRegistration.FetchReferrals(ID) 'Fetch Referrals against Patient
                If Not IsNothing(arrlist) Then
                    setReferrals(arrlist)
                    _arrReferals = arrlist
                End If

                FillTreeView()
                'Show the history button if the form is opened in Update mode only
                btnPatientRecordChange.Visible = True

            ElseIf Not IsNothing(ArrPatientRegistration) Then
                If ArrPatientRegistration.Count > 0 Then
                    GetData(ArrPatientRegistration)
                    FillTreeViewforOMR()
                End If
            End If
            'code added by supriya 24/7/2008
            'set provider to one sent from RxRequest form
            If RefillReqProviderID <> "" Then
                cmbProvider.SelectedValue = RefillReqProviderID
            End If
            'code added by supriya 24/7/2008
            key = -1
            trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
            txtPatientCode.Select()
            'tbPatientRegistration.BackColor = System.Drawing.Color.FromArgb(195, 217, 249)


            If intId <> 0 Then
                '''' For Modify
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, "Patient Registration Information Viewed", gloAuditTrail.ActivityOutCome.Success)
            Else
                '' For Add New
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Open, "Patient Registration Opened", gloAuditTrail.ActivityOutCome.Success)
            End If

            '' code for the Icon view on forms tool bar
            'If intId <> 0 Then
            '    '''' For Modify
            '    'Me.Icon = "dbs.ico"
            '    'Me.Icon = CType(Global.gloEMR.My.Resources.Resources.cpt, System.Drawing.Icon)
            'Else
            '    '' For Add New
            '    'Me.Icon = "db.ico"
            '    'Me.Icon = CType(Global.gloEMR.My.Resources.Resources.icd9, System.Drawing.Icon)

            '    If txtfname.Text.Trim.Length = 0 Then
            '        btnOK.Enabled = False
            '    Else
            '        btnOK.Enabled = True
            '    End If
            'End If
            ''
            Call btnDisabpleEnable()
            btnCapture.Enabled = False

            'checking for the text modification Flag
            bTextChangeFlag = False
            Dim strFname As String = txtfname.Text
            Dim strLName As String = txtlname.Text
            Dim strPtCode As String = txtPatientCode.Text
            mskDOB1.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
            Dim strDob As String = mskDOB1.Text
            mskDOB1.TextMaskFormat = MaskFormat.IncludeLiterals


            ''code for get data of Patient Insurance
            Dim nInsCount As Integer = 0

            nInsuranceNodeCount = trInsurance.Nodes(0).Nodes.Count

            'For nInsCount = 0 To cmbInsuranceNew.Items.Count - 1
            '    _arrInsurance(nInsCount) = cmbInsuranceNew.Items.Item(nInsCount)
            'Next
            ''
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRecordLock = True Then
                btnOK.Enabled = False
            End If
        End Try
    End Sub

    'Private Sub FillTreeViewforOMR()
    '    Try
    '        '     If ID <> 0 Then
    '        Dim mychildnode As TreeNode
    '        Dim i As Integer
    '        Dim intkey As Long
    '        Dim strname As String
    '        For i = 0 To ArrPatientInsurance.Count - 1
    '            Dim PatientInsurance As New ClsPatientInsurance
    '            PatientInsurance = ArrPatientInsurance.Item(i)
    '            'Insert in to patientinsurance collection
    '            PatientRegistration.PopulateArraylist(PatientInsurance)
    '            intkey = PatientInsurance.InsuranceId
    '            strname = PatientInsurance.InsuranceName
    '            mychildnode = New TreeNode(strname)
    '            If PatientInsurance.Primaryflag Then
    '                mychildnode.ForeColor = Color.Blue
    '                mychildnode.ImageIndex = 1
    '                mychildnode.SelectedImageIndex = 1
    '            Else
    '                mychildnode.ImageIndex = 2
    '                mychildnode.SelectedImageIndex = 2
    '            End If
    '            mynode.Nodes.Add(mychildnode)
    '            PatientInsurance = Nothing
    '        Next
    '        key = 0
    '        mynode.ExpandAll()
    '        'mychildnode = trInsurance.Nodes.Item(0).Nodes.Item(0)
    '        'trInsurance.SelectedNode = mynode

    '        'End If
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub setReferrals(ByRef arrlist As ArrayList)
    '    Dim i As Integer
    '    If Not IsNothing(arrlist) Then
    '        For i = 0 To arrlist.Count - 1
    '            cmbReferrals.Items.Add(arrlist.Item(i))
    '            If i = 0 Then
    '                Dim objmylist As myList
    '                objmylist = (CType(arrlist.Item(0), myList))
    '                cmbReferrals.Text = objmylist.Description
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub GetData(ByRef Arrlist As ArrayList)
        Dim strtext As String

        ' fill data in temp list for the Verification purpose whether user modify Patient info. or not
        ' Added on 20070716 - Bipin
        _arrayGetData = Arrlist

        'Retrieve Data for Modifying Patient registration Details
        If IsDBNull(Arrlist.Item(0)) Then
            txtPatientCode.Text = ""
        Else
            txtPatientCode.Text = CType(Arrlist.Item(0), System.String)
        End If
        If IsDBNull(Arrlist.Item(1)) Then
            txtfname.Text = ""
        Else
            txtfname.Text = CType(Arrlist.Item(1), System.String)
        End If
        If IsDBNull(Arrlist.Item(2)) Then
            txtmName.Text = ""
        Else
            txtmName.Text = CType(Arrlist.Item(2), System.String)
        End If
        If IsDBNull(Arrlist.Item(3)) Then
            txtlname.Text = ""
        Else
            txtlname.Text = CType(Arrlist.Item(3), System.String)
        End If
        If IsDBNull(Arrlist.Item(4)) Then
            'mskssn.Text = ""
        Else
            Dim str As String
            str = CType(Arrlist.Item(4), String)
            If Len(str) < 9 And Val(str) <> 0 Then

                Dim j As Int16
                For j = 1 To 9 - Len(str)
                    str = "" & str
                Next
                'strtext = splittext(str, True)
            End If
            '''''MskSSn.CtlText = splittext(CType(Arrlist.Item(4), System.Int64), True)

            'If str <> "" Then
            '    strtext = splittext(str, True)
            'End If
            '

            'ssn change
            'If Val(str) <> 0 Then
            strtext = splittext(str, True)
            'End If
            '


            If strtext <> "" Then
                'MskSSn.CtlText = strtext
                MskSSn1.Text = strtext
                strtext = ""
            End If
            'MskSSn.Mask = "###/##/####"
        End If
        If IsDBNull(Arrlist.Item(5)) Then
            'dtpDOB.Checked = False
        Else
            'dtpDOB.Value = CType(Arrlist.Item(5), System.DateTime)
            'dtpDOB.Checked = True
            Try
                If IsDate(CType(Arrlist.Item(5), System.DateTime)) Then
                    mskDOB1.Text = Format(CType(Arrlist.Item(5), System.DateTime).Date, "MM/dd/yyyy")
                End If
                'mskDOB.CtlText = mskDOB.Text
            Catch ex As Exception

            End Try
            'Try
            '    If IsDate(CType(Arrlist.Item(5), System.DateTime)) Then
            '        mskDOB.CtlText = Format(CType(Arrlist.Item(5), System.DateTime).Date, "MM/dd/yyyy")
            '    End If
            '    'mskDOB.CtlText = mskDOB.Text
            'Catch ex As Exception

            'End Try
        End If

        If Arrlist.Item(6) = "Male" Then
            rbGender1.Checked = True
        ElseIf Arrlist.Item(6) = "Female" Then
            rbGender2.Checked = True
        ElseIf Arrlist.Item(6) = "Other" Then
            rbGender3.Checked = True
        End If
        If IsDBNull(Arrlist.Item(7)) Then
            cmbMaritalstatus.Text = ""
        Else
            cmbMaritalstatus.Text = CType(Arrlist.Item(7), System.String)
        End If
        If IsDBNull(Arrlist.Item(8)) Then
            txtAddress1.Text = ""
        Else
            txtAddress1.Text = CType(Arrlist.Item(8), System.String)
        End If
        If IsDBNull(Arrlist.Item(9)) Then
            txtAddress2.Text = ""
        Else
            txtAddress2.Text = CType(Arrlist.Item(9), System.String)
        End If
        If IsDBNull(Arrlist.Item(10)) Then
            txtCity.Text = ""
        Else
            txtCity.Text = CType(Arrlist.Item(10), System.String)
        End If
        If IsDBNull(Arrlist.Item(11)) Then
            cmbState.Text = ""
        Else
            cmbState.Text = CType(Arrlist.Item(11), System.String)
        End If
        If IsDBNull(Arrlist.Item(12)) Then
            txtZip.Text = ""
        Else
            txtZip.Text = CType(Arrlist.Item(12), System.String)
        End If
        If IsDBNull(Arrlist.Item(13)) Then
            txtCounty.Text = ""
        Else
            txtCounty.Text = CType(Arrlist.Item(13), System.String)
        End If
        If IsDBNull(Arrlist.Item(14)) Then
            'mskPhone.CtlText = ""
        Else
            'If Len(CType(Arrlist.Item(14), System.String)) = 10 Then
            strtext = splittext(CType(Arrlist.Item(14), System.String), False)
            ' modification on 20070522 - Bipin
            If strtext <> "" Then
                mskPhone1.Text = strtext
                strtext = ""
            End If
            'If strtext <> "" Then
            '    mskPhone.CtlText = strtext
            '    strtext = ""
            'End If
            'End If
        End If
        If IsDBNull(Arrlist.Item(15)) Then
            'mskMobile.Text = ""
        Else
            'If Len(CType(Arrlist.Item(15), System.String)) = 10 Then
            strtext = splittext(CType(Arrlist.Item(15), System.String), False)
            ' modification on 20070522 - Bipin
            If strtext <> "" Then
                mskMobile1.Text = strtext
                strtext = ""
            End If
            'If strtext <> "" Then
            '    mskMobile.CtlText = strtext
            '    strtext = ""
            'End If
            'End If
        End If
        If IsDBNull(Arrlist.Item(16)) Then
            txtemail.Text = ""
        Else
            txtemail.Text = CType(Arrlist.Item(16), System.String)
        End If
        If IsDBNull(Arrlist.Item(17)) Then
            txtfax.Text = ""
        Else
            txtfax.Text = CType(Arrlist.Item(17), System.String)
        End If
        If IsDBNull(Arrlist.Item(18)) Then
            txtOccupation.Text = ""
        Else
            txtOccupation.Text = CType(Arrlist.Item(18), System.String)

        End If
        If IsDBNull(Arrlist.Item(19)) Then
            cmbEmploymentStatus.Text = ""
        Else
            cmbEmploymentStatus.Text = CType(Arrlist.Item(19), System.String)
        End If
        If IsDBNull(Arrlist.Item(20)) Then
            txtWLocation.Text = ""
        Else
            txtWLocation.Text = CType(Arrlist.Item(20), System.String)
        End If
        If IsDBNull(Arrlist.Item(21)) Then
            txtwAddress1.Text = ""
        Else
            txtwAddress1.Text = CType(Arrlist.Item(21), System.String)
        End If
        If IsDBNull(Arrlist.Item(22)) Then
            txtwAddress2.Text = ""
        Else
            txtwAddress2.Text = CType(Arrlist.Item(22), System.String)
        End If
        If IsDBNull(Arrlist.Item(23)) Then
            txtwCity.Text = ""
        Else
            txtwCity.Text = CType(Arrlist.Item(23), System.String)
        End If
        If IsDBNull(Arrlist.Item(24)) Then
            cmbwState.Text = ""
        Else
            cmbwState.Text = CType(Arrlist.Item(24), System.String)
        End If
        If IsDBNull(Arrlist.Item(25)) Then
            txtwZip.Text = ""
        Else
            txtwZip.Text = CType(Arrlist.Item(25), System.String)
        End If
        If IsDBNull(Arrlist.Item(26)) Then
            'mskwphone.Text = ""
        Else
            'If Len(CType(Arrlist.Item(26), System.String)) = 10 Then
            strtext = splittext(CType(Arrlist.Item(26), System.String), False)
            'If strtext <> "" Then
            '    mskwPhone.CtlText = strtext
            '    strtext = ""
            'End If
            If strtext <> "" Then
                mskwPhone1.Text = strtext
                strtext = ""
            End If
            'End If
        End If
        If IsDBNull(Arrlist.Item(27)) Then
            txtwFax.Text = ""
        Else
            txtwFax.Text = CType(Arrlist.Item(27), System.String)
        End If
        If IsDBNull(Arrlist.Item(28)) Then
            txtInsuranceNotes.Text = ""
        Else
            txtInsuranceNotes.Text = CType(Arrlist.Item(28), System.String)
        End If

        'If IsDBNull(Arrlist.Item(28)) Then
        '    dtpRegistrationdate.Checked = False
        'Else
        '    dtpRegistrationdate.Value = CType(Arrlist.Item(28), System.DateTime)
        '    dtpRegistrationdate.Checked = True
        'End If

        If IsDBNull(Arrlist.Item(29)) Then
            txtpcp.Tag = 0
        Else
            txtpcp.Tag = CType(Arrlist.Item(29), Long)
        End If
        If IsDBNull(Arrlist.Item(30)) Then
            txtGuarantor.Text = ""
        Else
            txtGuarantor.Text = CType(Arrlist.Item(30), System.String)
        End If
        'If IsDBNull(Arrlist.Item(29)) Then
        '    txtInsuranceType.Text = ""
        'Else
        '    txtInsuranceType.Text = CType(Arrlist.Item(29), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(30)) Then
        '    txtpSubscribername.Text = ""
        'Else
        '    txtpSubscribername.Text = CType(Arrlist.Item(30), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(31)) Then
        '    txtpSubscriberID.Text = ""
        'Else
        '    txtpSubscriberID.Text = CType(Arrlist.Item(31), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(32)) Then
        '    txtpSubscriberPolicy.Text = ""
        'Else
        '    txtpSubscriberPolicy.Text = CType(Arrlist.Item(32), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(33)) Then
        '    txtsSubscriberID.Text = ""
        'Else
        '    txtsSubscriberID.Text = CType(Arrlist.Item(33), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(34)) Then
        '    txtsSubscriberName.Text = ""
        'Else
        '    txtsSubscriberName.Text = CType(Arrlist.Item(34), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(35)) Then
        '    txtsSubscriberPolicy.Text = ""
        'Else
        '    txtsSubscriberPolicy.Text = CType(Arrlist.Item(35), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(36)) Then
        '    txttSubscriberID.Text = ""
        'Else
        '    txttSubscriberID.Text = CType(Arrlist.Item(36), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(37)) Then
        '    txttSubscriberName.Text = ""
        'Else
        '    txttSubscriberName.Text = CType(Arrlist.Item(37), System.String)
        'End If
        'If IsDBNull(Arrlist.Item(38)) Then
        '    txttSubscriberPolicy.Text = ""
        'Else
        '    txttSubscriberPolicy.Text = CType(Arrlist.Item(38), System.String)
        'End If
        If IsDBNull(Arrlist.Item(31)) Then
            txtspousename.Text = ""
        Else
            txtspousename.Text = CType(Arrlist.Item(31), System.String)
        End If

        ' modification on 20070522 by Bipin
        If IsDBNull(Arrlist.Item(32)) Then
            'mskspousePhone.Text = ""
        Else
            'If Len(CType(Arrlist.Item(32), System.String)) = 10 Then
            strtext = splittext(CType(Arrlist.Item(32), System.String), False)
            If strtext <> "" Then
                mskspousePhone1.Text = strtext
                strtext = ""
            End If
            'End If
        End If

        'If IsDBNull(Arrlist.Item(32)) Then
        '    'mskspousePhone.Text = ""
        'Else
        '    'If Len(CType(Arrlist.Item(32), System.String)) = 10 Then
        '    strtext = splittext(CType(Arrlist.Item(32), System.String), False)
        '    If strtext <> "" Then
        '        mskspousePhone.CtlText = strtext
        '        strtext = ""
        '    End If
        '    'End If
        'End If
        If IsDBNull(Arrlist.Item(33)) Then
            cmbRace.Text = ""
        Else
            cmbRace.Text = CType(Arrlist.Item(33), System.String)
        End If
        If IsDBNull(Arrlist.Item(34)) Then
            cmbPatientStatus.Text = ""
        Else
            cmbPatientStatus.Text = CType(Arrlist.Item(34), System.String)
        End If
        If IsDBNull(Arrlist.Item(35)) Then
            cmbProvider.Text = ""
        Else
            'cmbProvider.SelectedIndex = cmbProvider.FindStringExact(CType(Arrlist.Item(37), System.String))

            'cmbProvider.Tag = CType(Arrlist.Item(35), System.Int64)
            cmbProvider.SelectedValue = CType(Arrlist.Item(35), Long)
        End If
        'If IsDBNull(Arrlist.Item(37)) Then
        '    cmbProvider.Text = ""
        'Else
        '    cmbProvider.Text = CType(Arrlist.Item(37), System.String)
        'End If


        'If IsDBNull(Arrlist.Item(48)) Then
        'cmbReferral.Text = ""
        'Else
        'cmbReferral.SelectedIndex = cmbReferral.FindStringExact(CType(Arrlist.Item(44), System.String))
        'cmbReferral.Text = CType(Arrlist.Item(48), System.String)
        'End If
        If IsDBNull(Arrlist.Item(36)) Then
            txtPharmacy.Tag = 0
        Else
            'cmbPharmacy.SelectedIndex = cmbPharmacy.FindStringExact(CType(Arrlist.Item(45), System.String))
            txtPharmacy.Tag = CType(Arrlist.Item(36), Long)
        End If

        If IsDBNull(Arrlist.Item(38)) Then
            txtPharmacy.Text = ""
        Else
            txtPharmacy.Text = CType(Arrlist.Item(38), System.String)
        End If
        'If IsDBNull(Arrlist.Item(50)) Then
        'cmbInsuranceID.Text = ""
        'Else
        'cmbInsuranceID.SelectedIndex = cmbInsuranceID.FindStringExact(CType(Arrlist.Item(46), System.String))
        'cmbInsuranceID.Text = CType(Arrlist.Item(50), System.String)
        'End If


        picFinalPatient.Visible = True
        picWebCamPatient.Visible = False
        If IsNothing(ArrPatientRegistration) Then
            'If ArrPatientRegistration.Count > 0 Then
            If IsDBNull(Arrlist.Item(39)) = True Then
                picFinalPatient.Image = Nothing
            Else
                Dim arrPicture() As Byte = CType(Arrlist.Item(39), Byte())
                Dim ms As New MemoryStream(arrPicture)
                picFinalPatient.Image = Image.FromStream(ms)
                ms.Close()
                picFinalPatient.SizeMode = PictureBoxSizeMode.CenterImage
            End If
            'End If
        End If
        If Not IsDBNull(Arrlist.Item(40)) Then
            Try
                If IsDate(CType(Arrlist.Item(40), System.DateTime)) Then
                    ' modification on 20070522 by Bipin
                    'mskRegistrationdate.CtlText = Format(CType(Arrlist.Item(40), System.DateTime).Date, "MM/dd/yyyy")
                    mskRegistrationdate1.Text = Format(CType(Arrlist.Item(40), System.DateTime).Date, "MM/dd/yyyy")
                End If
            Catch ex As Exception

            End Try
        End If
        If IsDBNull(Arrlist.Item(41)) Then
            txtpcp.Text = ""
        Else
            txtpcp.Text = CType(Arrlist.Item(41), System.String)
        End If
        If Not IsNothing(ArrPatientRegistration) Then
            If Not IsDBNull(Arrlist.Item(42)) Then
                'Dim arrreflist As New ArrayList
                'arrreflist.Add(New myList(CType(Arrlist.Item(42), System.Int64), CType(Arrlist.Item(43), System.String)))
                cmbReferrals.Items.Add(New myList(CType(Arrlist.Item(42), Long), CType(Arrlist.Item(43), System.String)))
            End If
        End If
        If IsNothing(ArrPatientRegistration) Then
            Try
                If Not IsNothing(Arrlist.Item(42)) Then
                    If Not IsDBNull(Arrlist.Item(42)) Then

                        If IsDate(CType(Arrlist.Item(42), System.DateTime)) Then
                            ' modification on 20070522 by Bipin
                            'mskInjurydate.CtlText = Format(CType(Arrlist.Item(42), System.DateTime).Date, "MM/dd/yyyy")
                            mskInjurydate1.Text = Format(CType(Arrlist.Item(42), System.DateTime).Date, "MM/dd/yyyy")
                        End If

                    End If
                End If
                If Not IsNothing(Arrlist.Item(43)) Then
                    If Not IsDBNull(Arrlist.Item(43)) Then

                        If IsDate(CType(Arrlist.Item(43), System.DateTime)) Then
                            ' modification on 20070522 by Bipin
                            'mskSurgeryDate.CtlText = Format(CType(Arrlist.Item(43), System.DateTime).Date, "MM/dd/yyyy")
                            mskSurgeryDate1.Text = Format(CType(Arrlist.Item(43), System.DateTime).Date, "MM/dd/yyyy")
                        End If

                    End If
                End If
                If IsDBNull(Arrlist.Item(44)) Then
                    cmbDominance.Text = ""
                Else
                    cmbDominance.Text = CType(Arrlist.Item(44), System.String)
                End If

                If IsDBNull(Arrlist.Item(45)) Then
                    cmbLocation.Text = ""
                Else
                    cmbLocation.Text = CType(Arrlist.Item(45), System.String)
                End If

                '''' Code updation by Ravikiran on 27/01/2007

                If IsDBNull(Arrlist.Item(46)) Then
                    txtMother_fName.Text = ""
                Else
                    txtMother_fName.Text = CType(Arrlist.Item(46), System.String)
                End If
                If IsDBNull(Arrlist.Item(47)) Then
                    txtMother_mName.Text = ""
                Else
                    txtMother_mName.Text = CType(Arrlist.Item(47), System.String)
                End If
                If IsDBNull(Arrlist.Item(48)) Then
                    txtMother_lName.Text = ""
                Else
                    txtMother_lName.Text = CType(Arrlist.Item(48), System.String)
                End If
                If IsDBNull(Arrlist.Item(49)) Then
                    txtMother_Address1.Text = ""
                Else
                    txtMother_Address1.Text = CType(Arrlist.Item(49), System.String)
                End If
                If IsDBNull(Arrlist.Item(50)) Then
                    txtMother_Address2.Text = ""
                Else
                    txtMother_Address2.Text = CType(Arrlist.Item(50), System.String)
                End If
                If IsDBNull(Arrlist.Item(51)) Then
                    txtMother_City.Text = ""
                Else
                    txtMother_City.Text = CType(Arrlist.Item(51), System.String)
                End If
                If IsDBNull(Arrlist.Item(52)) Then
                    cmbMother_State.Text = ""
                Else
                    cmbMother_State.Text = CType(Arrlist.Item(52), System.String)
                End If
                If IsDBNull(Arrlist.Item(53)) Then
                    txtMother_Zip.Text = ""
                Else
                    txtMother_Zip.Text = CType(Arrlist.Item(53), System.String)
                End If
                If IsDBNull(Arrlist.Item(54)) Then
                    txtMother_County.Text = ""
                Else
                    txtMother_County.Text = CType(Arrlist.Item(54), System.String)
                End If
                If IsDBNull(Arrlist.Item(55)) Then
                    'mskPhone.CtlText = ""
                Else
                    'If Len(CType(Arrlist.Item(14), System.String)) = 10 Then
                    ' modification on 20070522 by Bipin
                    strtext = splittext(CType(Arrlist.Item(55), System.String), False)
                    If strtext <> "" Then
                        mskMother_Phone1.Text = strtext
                        strtext = ""
                    End If
                    'If strtext <> "" Then
                    '    mskMother_Phone.CtlText = strtext
                    '    strtext = ""
                    'End If
                    'End If
                End If
                If IsDBNull(Arrlist.Item(56)) Then
                    'mskMobile.Text = ""
                Else
                    'If Len(CType(Arrlist.Item(15), System.String)) = 10 Then

                    strtext = splittext(CType(Arrlist.Item(56), System.String), False)
                    ' modification on 20070522 by Bipin
                    If strtext <> "" Then
                        mskMother_Mobile1.Text = strtext
                        strtext = ""
                    End If
                    'If strtext <> "" Then
                    '    mskMother_Mobile.CtlText = strtext
                    '    strtext = ""
                    'End If
                    'End If
                End If
                If IsDBNull(Arrlist.Item(57)) Then

                    txtMother_Fax.Text = ""
                Else
                    txtMother_Fax.Text = CType(Arrlist.Item(57), System.String)
                End If
                If IsDBNull(Arrlist.Item(58)) Then
                    txtMother_Email.Text = ""
                Else
                    txtMother_Email.Text = CType(Arrlist.Item(58), System.String)
                End If


                If IsDBNull(Arrlist.Item(59)) Then
                    txtFather_fName.Text = ""
                Else
                    txtFather_fName.Text = CType(Arrlist.Item(59), System.String)
                End If
                If IsDBNull(Arrlist.Item(60)) Then
                    txtFather_mName.Text = ""
                Else
                    txtFather_mName.Text = CType(Arrlist.Item(60), System.String)
                End If
                If IsDBNull(Arrlist.Item(61)) Then
                    txtFather_lName.Text = ""
                Else
                    txtFather_lName.Text = CType(Arrlist.Item(61), System.String)
                End If
                If IsDBNull(Arrlist.Item(62)) Then
                    txtFather_Address1.Text = ""
                Else
                    txtFather_Address1.Text = CType(Arrlist.Item(62), System.String)
                End If
                If IsDBNull(Arrlist.Item(63)) Then
                    txtFather_Address2.Text = ""
                Else
                    txtFather_Address2.Text = CType(Arrlist.Item(63), System.String)
                End If
                If IsDBNull(Arrlist.Item(64)) Then
                    txtFather_City.Text = ""
                Else
                    txtFather_City.Text = CType(Arrlist.Item(64), System.String)
                End If
                If IsDBNull(Arrlist.Item(65)) Then
                    cmbFather_State.Text = ""
                Else
                    cmbFather_State.Text = CType(Arrlist.Item(65), System.String)
                End If
                If IsDBNull(Arrlist.Item(66)) Then
                    txtFather_Zip.Text = ""
                Else
                    txtFather_Zip.Text = CType(Arrlist.Item(66), System.String)
                End If
                If IsDBNull(Arrlist.Item(67)) Then
                    txtFather_County.Text = ""
                Else
                    txtFather_County.Text = CType(Arrlist.Item(67), System.String)
                End If
                If IsDBNull(Arrlist.Item(68)) Then
                    'mskPhone.CtlText = ""
                Else
                    'If Len(CType(Arrlist.Item(14), System.String)) = 10 Then
                    strtext = splittext(CType(Arrlist.Item(68), System.String), False)
                    ' modification on 20070522 by Bipin
                    If strtext <> "" Then
                        mskFather_Phone1.Text = strtext
                        strtext = ""
                    End If
                    'If strtext <> "" Then
                    '    mskFather_Phone.CtlText = strtext
                    '    strtext = ""
                    'End If
                    'End If
                End If
                If IsDBNull(Arrlist.Item(69)) Then
                    'mskMobile.Text = ""
                Else
                    'If Len(CType(Arrlist.Item(15), System.String)) = 10 Then
                    strtext = splittext(CType(Arrlist.Item(69), System.String), False)
                    ' modification on 20070522 by Bipin
                    If strtext <> "" Then
                        mskFather_Mobile1.Text = strtext
                        strtext = ""
                    End If
                    'If strtext <> "" Then
                    '    mskFather_Mobile.CtlText = strtext
                    '    strtext = ""
                    'End If
                    'End If
                End If
                If IsDBNull(Arrlist.Item(70)) Then

                    txtFather_Fax.Text = ""
                Else
                    txtFather_Fax.Text = CType(Arrlist.Item(70), System.String)
                End If
                If IsDBNull(Arrlist.Item(71)) Then
                    txtFather_Email.Text = ""
                Else
                    txtFather_Email.Text = CType(Arrlist.Item(71), System.String)
                End If

                If IsDBNull(Arrlist.Item(72)) Then
                    txtGuardian_fName.Text = ""
                Else
                    txtGuardian_fName.Text = CType(Arrlist.Item(72), System.String)
                End If
                If IsDBNull(Arrlist.Item(73)) Then
                    txtGuardian_mName.Text = ""
                Else
                    txtGuardian_mName.Text = CType(Arrlist.Item(73), System.String)
                End If
                If IsDBNull(Arrlist.Item(74)) Then
                    txtGuardian_lName.Text = ""
                Else
                    txtGuardian_lName.Text = CType(Arrlist.Item(74), System.String)
                End If
                If IsDBNull(Arrlist.Item(75)) Then
                    txtGuardian_Address1.Text = ""
                Else
                    txtGuardian_Address1.Text = CType(Arrlist.Item(75), System.String)
                End If
                If IsDBNull(Arrlist.Item(76)) Then
                    txtGuardian_Address2.Text = ""
                Else
                    txtGuardian_Address2.Text = CType(Arrlist.Item(76), System.String)
                End If
                If IsDBNull(Arrlist.Item(77)) Then
                    txtGuardian_City.Text = ""
                Else
                    txtGuardian_City.Text = CType(Arrlist.Item(77), System.String)
                End If
                If IsDBNull(Arrlist.Item(78)) Then
                    cmbGuardian_State.Text = ""
                Else
                    cmbGuardian_State.Text = CType(Arrlist.Item(78), System.String)
                End If
                If IsDBNull(Arrlist.Item(79)) Then
                    txtGuardian_Zip.Text = ""
                Else
                    txtGuardian_Zip.Text = CType(Arrlist.Item(79), System.String)
                End If
                If IsDBNull(Arrlist.Item(80)) Then
                    txtGuardian_County.Text = ""
                Else
                    txtGuardian_County.Text = CType(Arrlist.Item(80), System.String)
                End If
                If IsDBNull(Arrlist.Item(81)) Then
                    'mskPhone.CtlText = ""
                Else
                    'If Len(CType(Arrlist.Item(14), System.String)) = 10 Then
                    strtext = splittext(CType(Arrlist.Item(81), System.String), False)
                    ' modification on 20070522 by Bipin
                    If strtext <> "" Then
                        mskGuardian_Phone1.Text = strtext
                        strtext = ""
                    End If
                    'If strtext <> "" Then
                    '    mskGuardian_Phone.CtlText = strtext
                    '    strtext = ""
                    'End If
                    'End If
                End If
                If IsDBNull(Arrlist.Item(82)) Then
                    'mskMobile.Text = ""
                Else
                    'If Len(CType(Arrlist.Item(15), System.String)) = 10 Then
                    strtext = splittext(CType(Arrlist.Item(82), System.String), False)

                    ' modification on 20070522 by Bipin
                    If strtext <> "" Then
                        mskGuardian_Mobile1.Text = strtext
                        strtext = ""
                    End If
                    'If strtext <> "" Then
                    '    mskGuardian_Mobile.CtlText = strtext
                    '    strtext = ""
                    'End If
                    'End If
                End If
                If IsDBNull(Arrlist.Item(83)) Then

                    txtGuardian_Fax.Text = ""
                Else
                    txtGuardian_Fax.Text = CType(Arrlist.Item(83), System.String)
                End If
                If IsDBNull(Arrlist.Item(84)) Then
                    txtGuardian_Email.Text = ""
                Else
                    txtGuardian_Email.Text = CType(Arrlist.Item(84), System.String)
                End If

                If Arrlist.Item(85) Is Nothing Then
                    chkDirective.Checked = False
                    _PatientDirectiveStatus = False
                    chkDirective.Enabled = True
                Else
                    If Val(Arrlist.Item(85)) = 0 Then
                        chkDirective.Checked = False
                        _PatientDirectiveStatus = False
                        chkDirective.Enabled = True
                    Else
                        chkDirective.Checked = True
                        _PatientDirectiveStatus = True
                        chkDirective.Enabled = False
                    End If
                End If

                'sarika ist august 08
                _blnScanDoc = chkDirective.Checked
                '--------------------


                If Arrlist.Item(86) Is Nothing Then
                    chkExemptFromRpt.Checked = False
                Else
                    If Val(Arrlist.Item(86)) = 0 Then
                        chkExemptFromRpt.Checked = False
                    Else
                        chkExemptFromRpt.Checked = True
                    End If
                End If



                'sarika Workers Comp 7th May 08
                If Arrlist.Item(87) Is Nothing Then
                    chkWorkersComp.Checked = False
                Else
                    If Val(Arrlist.Item(87)) = 0 Then
                        chkWorkersComp.Checked = False
                    Else
                        chkWorkersComp.Checked = True
                    End If
                End If


                If IsDBNull(Arrlist.Item(88)) Then
                    txtWorkersCompClaimNo.Text = ""
                Else
                    txtWorkersCompClaimNo.Text = CType(Arrlist.Item(88), System.String)
                End If


                '------------sarika Workers Comp 7th May 08


                'sarika Auto 7th May 08
                If Arrlist.Item(89) Is Nothing Then
                    chkAuto.Checked = False
                Else
                    If Val(Arrlist.Item(89)) = 0 Then
                        chkAuto.Checked = False
                    Else
                        chkAuto.Checked = True
                    End If
                End If


                If IsDBNull(Arrlist.Item(90)) Then
                    txtAutoClaimNo.Text = ""
                Else
                    txtAutoClaimNo.Text = CType(Arrlist.Item(90), System.String)
                End If


                '------------sarika Auto 7th May 08



                '''' updation Ends

                '' 20061225 -- Mahesh -- Patient Change History
                Call SetPatientOriginalData()
                ''
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btn_Next_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(3)
        txtPatientCode.Focus()
    End Sub

    Private Sub btn_Previous_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) 'TabPages(1)
        txtPatientCode.Focus()
    End Sub

    'Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Trim(txtInsuranceName.Text) = "" Then
    '            MsgBox("Insurance Name Required")
    '            Exit Sub
    '        End If
    '        If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
    '            MsgBox("Insurance Phone Details Incomplete")
    '            'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '            mskIPhone1.Focus()
    '            Exit Sub
    '        End If
    '        'If Len(Trim(mskIPhone.ClipText)) > 0 And Len(Trim(mskIPhone.ClipText)) < 10 Then
    '        '    MsgBox("Insurance Phone Details Incomplete")
    '        '    'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '    mskIPhone.Focus()
    '        '    Exit Sub
    '        'End If
    '        Dim PatientInsurance As New ClsPatientInsurance
    '        PatientInsurance.InsuranceId = CType(txtInsuranceName.Tag, Long)
    '        PatientInsurance.InsuranceName = txtInsuranceName.Text
    '        PatientInsurance.SubscriberId = txtSubscriberID.Text
    '        PatientInsurance.Subscribername = txtSubscriberName.Text
    '        PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
    '        PatientInsurance.Group = txtGroup.Text
    '        'If Len(Trim(mskIPhone.ClipText)) > 10 Then

    '        ' modification on 20070522 by Bipin
    '        PatientInsurance.Phone = mskIPhone1.Text
    '        'PatientInsurance.Phone = mskIPhone.ClipText
    '        'End If
    '        'If dtpIDOB.Checked Then
    '        '    PatientInsurance.Checked = True
    '        'End If

    '        ' modification on 20070522 by Bipin
    '        If Len(mskIDOB1.Text) = 10 Then
    '            'If IsDate(mskIDOB.CtlText) Then
    '            If IsDate(CType(mskIDOB1.Text, Date)) Then
    '                'PatientInsurance.DOB = mskIDOB.CtlText
    '                PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
    '                PatientInsurance.Checked = True
    '            Else
    '                MsgBox("Invalid Birth Date")
    '                mskIDOB1.Focus()
    '                Exit Sub
    '            End If
    '        Else
    '            PatientInsurance.Checked = False
    '        End If
    '        'If Len(mskIDOB.ClipText) = 8 Then
    '        '    'If IsDate(mskIDOB.CtlText) Then
    '        '    If IsDate(CType(mskIDOB.FormattedText, Date)) Then
    '        '        'PatientInsurance.DOB = mskIDOB.CtlText
    '        '        PatientInsurance.DOB = CType(mskIDOB.FormattedText, Date)
    '        '        PatientInsurance.Checked = True
    '        '    Else
    '        '        MsgBox("Invalid Birth Date")
    '        '        mskIDOB.Focus()
    '        '        Exit Sub
    '        '    End If
    '        'Else
    '        '    PatientInsurance.Checked = False
    '        'End If
    '        'PatientInsurance.DOB = dtpIDOB.Value
    '        PatientInsurance.Employer = txtEmployer.Text
    '        If chkPrimary.Checked Then
    '            PatientInsurance.Primaryflag = True
    '        Else
    '            PatientInsurance.Primaryflag = False
    '        End If
    '        'If ID = 0 Then
    '        If Not CheckNode(PatientInsurance.InsuranceName) Then
    '            MsgBox("Duplicate Insurance")
    '            RefreshInsurance()
    '            trInsurance.ExpandAll()
    '            trInsurance.Select()
    '            key = -1
    '            trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '            PatientInsurance = Nothing
    '            Exit Sub
    '        Else
    '            If key < 0 Then

    '                'Add Insurance 
    '                Dim mychildnode As TreeNode
    '                mychildnode = New TreeNode(PatientInsurance.InsuranceName)
    '                mynode.Nodes.Add(mychildnode)
    '                key = mychildnode.Index
    '                If PatientRegistration.SetInsuranceCol(PatientInsurance, key, -1) Then
    '                    If chkPrimary.Checked = True Then
    '                        mychildnode.ForeColor = Color.Blue
    '                        mychildnode.ImageIndex = 1
    '                        mychildnode.SelectedImageIndex = 1
    '                        Dim mynode1 As TreeNode
    '                        For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '                            If Not mynode1 Is mychildnode Then
    '                                mynode1.ForeColor = Color.Black
    '                                mynode1.ImageIndex = 2
    '                                mynode1.SelectedImageIndex = 2
    '                            End If
    '                        Next
    '                    Else
    '                        mychildnode.ImageIndex = 2
    '                        mychildnode.SelectedImageIndex = 2
    '                    End If
    '                    RefreshInsurance()
    '                    trInsurance.ExpandAll()
    '                    trInsurance.Select()
    '                    key = -1
    '                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '                End If

    '            Else
    '                'Modify Insurance
    '                mynode.Nodes(key).Text = PatientInsurance.InsuranceName
    '                If PatientRegistration.SetInsuranceCol(PatientInsurance, key) Then
    '                    If chkPrimary.Checked = True Then
    '                        trInsurance.SelectedNode.ForeColor = Color.Blue
    '                        trInsurance.SelectedNode.ImageIndex = 1
    '                        trInsurance.SelectedNode.SelectedImageIndex = 1
    '                        Dim mynode1 As TreeNode
    '                        For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '                            If Not mynode1 Is trInsurance.SelectedNode Then
    '                                mynode1.ForeColor = Color.Black
    '                                mynode1.ImageIndex = 2
    '                                mynode1.SelectedImageIndex = 2
    '                            End If
    '                        Next
    '                    Else
    '                        trInsurance.SelectedNode.ForeColor = Color.Black
    '                        trInsurance.SelectedNode.ImageIndex = 2
    '                        trInsurance.SelectedNode.SelectedImageIndex = 2
    '                    End If
    '                    RefreshInsurance()
    '                    trInsurance.ExpandAll()
    '                    trInsurance.Select()
    '                    key = -1
    '                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '                End If

    '            End If

    '        End If
    '        PatientInsurance = Nothing
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'trInsurance.CollapseAll()
    '    End Try
    'End Sub

    'Private Sub btnAdd_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnAdd, btnAdd.Text & "Insurance")
    'End Sub

    Private Sub btnCapture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCapture.Click
        'btnCapture1.Enabled = False
        'btnStart.Enabled = True
        'If myCam.iRunning Then
        '    picFinalPatient.Image = CType(myCam.copyFrame(picWebCamPatient, New RectangleF(0, 0, picWebCamPatient.Width, picWebCamPatient.Height)), Image)
        '    myCam.closeCam()
        '    picFinalPatient.Visible = True
        '    picWebCamPatient.Visible = False
        '    blnPhotoModified = True
        'End If
        'myCam = Nothing
        ''btnClear.Enabled = True
        btnCapture.Enabled = False
        btnStart.Enabled = True
        If myCam.iRunning Then
            picFinalPatient.Image = CType(myCam.copyFrame(picWebCamPatient, New RectangleF(0, 0, picWebCamPatient.Width, picWebCamPatient.Height)), Image)
            myCam.closeCam()
            picFinalPatient.Visible = True
            picWebCamPatient.Visible = False
            blnPhotoModified = True
        End If
        myCam = Nothing
        btnClear.Enabled = True
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        picFinalPatient.Visible = True
        picFinalPatient.Image = Nothing
        blnPhotoModified = True
    End Sub

    Private Sub btnClearInsurance_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Trim(txtInsuranceName.Text) <> "" And txtInsuranceName.Tag <> 0 Then
            If MessageBox.Show("Are you sure you want to Clear Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtInsuranceName.Text = ""
                txtInsuranceName.Tag = 0
            End If
        End If
        'If btnAdd.Text = "Update" Then
        '    If Trim(txtInsuranceName.Text) <> "" Then
        '        If MessageBox.Show("Are you sure you want to Delete the Selected Patient Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
        '            Try
        '                If key >= 0 Then
        '                    PatientRegistration.DeleteInsurance(key)
        '                    mynode.Nodes.RemoveAt(key)
        '                    RefreshInsurance()
        '                    key = -1
        '                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
        '                    trInsurance.Select()
        '                    'Else
        '                    '    txtInsuranceName.Text = ""
        '                End If

        '            Catch ex As SqlClient.SqlException
        '                MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Catch ex As Exception
        '                MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            End Try
        '        End If
        '    End If
        'End If
    End Sub

    'Private Sub btnClearInsurance_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnClearInsurance, "Delete Insurance")
    'End Sub

    Private Sub btnclearPCP_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Trim(txtpcp.Text) <> "" And txtpcp.Tag <> 0 Then
            If MessageBox.Show("Are you sure you want to Clear Primary Care Physician", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtpcp.Text = ""
                txtpcp.Tag = 0
            End If
        End If
    End Sub

    'Private Sub btnclearPCP_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnclearPCP, "Clear PCP")
    'End Sub

    Private Sub btnclearPharmacy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Trim(txtPharmacy.Text) <> "" And txtPharmacy.Tag <> 0 Then
            If MessageBox.Show("Are you sure you want to Clear Pharmacy", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtPharmacy.Text = ""
                txtPharmacy.Tag = 0
            End If
        End If
    End Sub

    'Private Sub btnclearPharmacy_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnclearPharmacy, "Clear Pharmacy")
    'End Sub

    Private Sub btnclearReferrals_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbReferrals.Items.Count > 0 Then
            If MessageBox.Show("Are you sure you want to Clear Referrals", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                'cmbReferrals.Items.Clear()
                cmbReferrals.Items.RemoveAt(cmbReferrals.SelectedIndex)
            End If
        End If
    End Sub

    'Private Sub btnclearReferrals_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnclearReferrals, "Clear Referrals")
    'End Sub

    'Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.Close()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If btnAdd.Text = "Update" Then
    '            If Trim(txtInsuranceName.Text) <> "" Then
    '                If MessageBox.Show("Are you sure you want to Delete the Selected Patient Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
    '                    Try
    '                        If key >= 0 Then
    '                            PatientRegistration.DeleteInsurance(key)
    '                            mynode.Nodes.RemoveAt(key)
    '                            RefreshInsurance()
    '                            key = -1
    '                            trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '                            trInsurance.Select()
    '                            'Else
    '                            '    txtInsuranceName.Text = ""
    '                        End If
    '                    Catch ex As SqlClient.SqlException
    '                        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Catch ex As Exception
    '                        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    End Try
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnInsurance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsurance.Click
        Try
            intStatus = 3
            'sarika 17th aug 07
            'txtInsuranceNew.Enabled = True
            chkSameasPatient.Enabled = True
            chkPrimary.Enabled = True
            If btnAdd.Text = "Add" Then
                clearInsCtrls()
            End If

            '---------
            Call loadC1flexgrid()
            ' modification in 20070525 - Bipin
            'AddControl()

            'If Not IsNothing(dgCustomGrid) Then
            '    dgCustomGrid.Top = tbPatientRegistration.Top + GroupBox9.Top + txtspousename.Top
            '    dgCustomGrid.Left = tbPatientRegistration.Left + trInsurance.Width + 10
            '    dgCustomGrid.Height = GroupBox9.Height + GroupBox14.Height
            '    dgCustomGrid.Visible = True
            '    dgCustomGrid.Width = GroupBox14.Width
            '    dgCustomGrid.BringToFront()
            '    BindGrid()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearInsCtrls()
        Try
            txtInsuranceName.Text = ""
            txtInsuranceNew.Text = ""
            txtSubscriberID.Text = ""
            txtSubscriberName.Text = ""
            txtSubscriberPolicy.Text = ""
            txtGroup.Text = ""
            txtEmployer.Text = ""
            mskIDOB1.Text = ""
            mskIPhone1.Text = ""

            'sarika 6th nov 07
            chkSameasPatient.Checked = False
            chkPrimary.Checked = False
            '--------------------

            btnAdd.Text = "Add"
            trInsurance.Select()


        Catch ex As Exception

        End Try
    End Sub

    'Private Sub btnnext1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) '.TabPages(1)
    '    txtMother_fName.Focus()
    'End Sub

    'Private Sub btnnext2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) 'TabPages(2)
    '    trInsurance.Select()
    'End Sub

    'Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnOK.Click
    '    Dim _lPatientDirectiveID As Int64 = 0

    '    Dim Arrlist As New ArrayList
    '    Try
    '        If Trim(txtPatientCode.Text) = "" Then
    '            MessageBox.Show("PatientCode Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
    '            txtPatientCode.Focus()
    '            Exit Sub
    '        ElseIf Trim(txtfname.Text) = "" Then
    '            MessageBox.Show("FirstName Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
    '            txtfname.Focus()
    '            Exit Sub
    '        ElseIf Trim(txtlname.Text) = "" Then
    '            MessageBox.Show("LastName Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
    '            txtlname.Focus()
    '            Exit Sub
    '            ' modification on 20070522 - Bipin
    '        ElseIf Len(mskDOB1.Text) <> 10 Then
    '            MessageBox.Show("Date of Birth Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
    '            mskDOB1.Focus()
    '            Exit Sub
    '            'ElseIf Len(mskDOB.ClipText) <> 8 Then
    '            '    MessageBox.Show("Date of Birth Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
    '            '    mskDOB.Focus()
    '            '    Exit Sub
    '        ElseIf Trim(Trim(cmbProvider.Text)) = "" Then
    '            MessageBox.Show("Provider Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
    '            cmbProvider.Focus()
    '            Exit Sub
    '            'ElseIf Not IsDate(mskDOB.CtlText) Then
    '            'ElseIf Not IsDate(Format(CType(mskDOB1.Text, System.DateTime).Date, "MM/dd/yyyy")) Then
    '        ElseIf Not IsDate(Format(mskDOB1.Text, "MM/dd/yyyy")) Then
    '            MessageBox.Show("Invalid Date of Birth ", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
    '            mskDOB1.Mask = ""
    '            'mskDOB1.CtlText = ""
    '            mskDOB1.Mask = "##/##/####"
    '            'mskDOB1.Format = "MM/dd/yyyy"
    '            mskDOB1.Focus()
    '            Exit Sub
    '            'ElseIf Not IsDate(mskDOB.FormattedText) Then
    '            '    MessageBox.Show("Invalid Date of Birth ", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
    '            '    mskDOB.Mask = ""
    '            '    mskDOB.CtlText = ""
    '            '    mskDOB.Mask = "##/##/####"
    '            '    mskDOB.Format = "MM/dd/yyyy"
    '            '    mskDOB.Focus()
    '            '    Exit Sub
    '            'changes done by Bipin on 20070522 CCHIT 2007
    '        ElseIf Len(Trim(MskSSn1.Text)) <> 9 And Val(MskSSn1.Text) <> 0 Then
    '            MessageBox.Show("Invalid SSN Number", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
    '            MskSSn1.Focus()
    '            Exit Sub

    '            'ElseIf Len(Trim(MskSSn.ClipText)) <> 9 And Val(MskSSn.ClipText) <> 0 Then
    '            '    MessageBox.Show("Invalid SSN Number", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
    '            '    MskSSn.Focus()
    '            '    Exit Sub

    '        ElseIf Len(Trim(mskPhone1.Text)) > 0 And Len(Trim(mskPhone1.Text)) < 10 Then
    '            MessageBox.Show("Phone Details Incomplete", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
    '            mskPhone1.Focus()
    '            Exit Sub
    '            'ElseIf Len(Trim(mskPhone.ClipText)) > 0 And Len(Trim(mskPhone.ClipText)) < 10 Then
    '            '    MessageBox.Show("Phone Details Incomplete", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
    '            '    mskPhone.Focus()
    '            '    Exit Sub

    '        ElseIf Len(Trim(mskwPhone1.Text)) > 0 And Len(Trim(mskwPhone1.Text)) < 10 Then
    '            MessageBox.Show("Work Phone Details Required", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) '.TabPages(1)
    '            mskwPhone1.Focus()
    '            Exit Sub

    '            'ElseIf Len(Trim(mskwPhone.ClipText)) > 0 And Len(Trim(mskwPhone.ClipText)) < 10 Then
    '            '    MessageBox.Show("Work Phone Details Required", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(1)
    '            '    mskwPhone.Focus()
    '            '    Exit Sub

    '        ElseIf Len(Trim(mskMobile1.Text)) > 0 And Len(Trim(mskMobile1.Text)) < 10 Then
    '            MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
    '            mskMobile1.Focus()
    '            Exit Sub
    '            'ElseIf Len(Trim(mskMobile.ClipText)) > 0 And Len(Trim(mskMobile.ClipText)) < 10 Then
    '            '    MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
    '            '    mskMobile.Focus()
    '            '    Exit Sub

    '        ElseIf Len(Trim(mskspousePhone1.Text)) > 0 And Len(Trim(mskspousePhone1.Text)) < 10 Then
    '            MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
    '            mskspousePhone1.Focus()
    '            Exit Sub
    '            'ElseIf Len(Trim(mskspousePhone.ClipText)) > 0 And Len(Trim(mskspousePhone.ClipText)) < 10 Then
    '            '    MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '            '    mskspousePhone.Focus()
    '            '    Exit Sub

    '            ' modification on 20070522 by Bipin
    '        ElseIf Len(mskIPhone1.Text) > 0 And Len(mskIPhone1.Text) < 10 Then
    '            MessageBox.Show("Insurance Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
    '            mskIPhone1.Focus()
    '            Exit Sub
    '            'ElseIf Len(mskIPhone.ClipText) > 0 And Len(mskIPhone.ClipText) < 10 Then
    '            '    MessageBox.Show("Insurance Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '            '    mskIPhone.Focus()
    '            '    Exit Sub
    '        Else
    '            ' modification on 20070522 by Bipin
    '            mskRegistrationdate1.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
    '            If Len(Trim(mskRegistrationdate1.Text)) > 4 And Len(Trim(mskRegistrationdate1.Text)) < 8 Then
    '                MessageBox.Show("Registration Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
    '                mskRegistrationdate1.Focus()
    '                Exit Sub
    '                'If Len(Trim(mskRegistrationdate.ClipText)) > 0 And Len(Trim(mskRegistrationdate.ClipText)) < 8 Then
    '                '    MessageBox.Show("Registration Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '                '    mskRegistrationdate.Focus()
    '                '    Exit Sub

    '                ' modification on 20070522 by Bipin
    '            ElseIf Len(Trim(mskRegistrationdate1.Text)) = 10 Then
    '                If Not IsDate(mskRegistrationdate1.Text) Then
    '                    MessageBox.Show("Invalid Registration Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
    '                    mskRegistrationdate1.Mask = ""
    '                    mskRegistrationdate1.Text = ""
    '                    mskRegistrationdate1.Mask = "##/##/####"
    '                    'mskRegistrationdate.Format = "MM/dd/yyyy"
    '                    mskRegistrationdate1.Focus()
    '                    Exit Sub
    '                End If

    '                'ElseIf Len(Trim(mskRegistrationdate.ClipText)) = 8 Then
    '                '    If Not IsDate(mskRegistrationdate.FormattedText) Then
    '                '        MessageBox.Show("Invalid Registration Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                '        tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '                '        mskRegistrationdate.Mask = ""
    '                '        mskRegistrationdate.CtlText = ""
    '                '        mskRegistrationdate.Mask = "##/##/####"
    '                '        mskRegistrationdate.Format = "MM/dd/yyyy"
    '                '        mskRegistrationdate.Focus()
    '                '        Exit Sub
    '                '    End If
    '            End If
    '            mskRegistrationdate1.TextMaskFormat = MaskFormat.IncludeLiterals
    '        End If
    '        ' modification on 20070522 by Bipin
    '        If Len(Trim(mskSurgeryDate1.Text)) > 4 And Len(Trim(mskSurgeryDate1.Text)) < 8 Then
    '            MessageBox.Show("Surgery Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
    '            mskSurgeryDate1.Focus()
    '            Exit Sub
    '            'If Len(Trim(mskSurgeryDate.ClipText)) > 0 And Len(Trim(mskSurgeryDate.ClipText)) < 8 Then
    '            '    MessageBox.Show("Surgery Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '            '    mskSurgeryDate.Focus()
    '            '    Exit Sub
    '        ElseIf Len(Trim(mskSurgeryDate1.Text)) = 10 Then
    '            If Not IsDate(mskSurgeryDate1.Text) Then
    '                MessageBox.Show("Invalid Surgery Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
    '                mskSurgeryDate1.Mask = ""
    '                mskSurgeryDate1.Text = ""
    '                mskSurgeryDate1.Mask = "##/##/####"
    '                'mskSurgeryDate.Format = "MM/dd/yyyy"
    '                mskSurgeryDate1.Focus()
    '                Exit Sub
    '            End If
    '        End If
    '        'ElseIf Len(Trim(mskSurgeryDate.ClipText)) = 8 Then
    '        '    If Not IsDate(mskSurgeryDate.FormattedText) Then
    '        '        MessageBox.Show("Invalid Surgery Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '        tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '        mskSurgeryDate.Mask = ""
    '        '        mskSurgeryDate.CtlText = ""
    '        '        mskSurgeryDate.Mask = "##/##/####"
    '        '        mskSurgeryDate.Format = "MM/dd/yyyy"
    '        '        mskSurgeryDate.Focus()
    '        '        Exit Sub
    '        '    End If
    '        'End If

    '        ' modification on 20070522 by Bipin
    '        If Len(Trim(mskInjurydate1.Text)) > 4 And Len(Trim(mskInjurydate1.Text)) < 8 Then
    '            MessageBox.Show("Injury Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
    '            mskInjurydate1.Focus()
    '            Exit Sub
    '        ElseIf Len(Trim(mskInjurydate1.Text)) = 10 Then
    '            If Not IsDate(mskInjurydate1.Text) Then
    '                MessageBox.Show("Invalid Injury Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
    '                mskInjurydate1.Mask = ""
    '                mskInjurydate1.Text = ""
    '                mskInjurydate1.Mask = "##/##/####"
    '                'mskInjurydate.Format = "MM/dd/yyyy"
    '                mskInjurydate1.Focus()
    '                Exit Sub
    '            End If

    '        End If

    '        'If Len(Trim(mskInjurydate.ClipText)) > 0 And Len(Trim(mskInjurydate.ClipText)) < 8 Then
    '        '    MessageBox.Show("Injury Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '    mskInjurydate.Focus()
    '        '    Exit Sub
    '        'ElseIf Len(Trim(mskInjurydate.ClipText)) = 8 Then
    '        '    If Not IsDate(mskInjurydate.FormattedText) Then
    '        '        MessageBox.Show("Invalid Injury Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '        tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '        mskInjurydate.Mask = ""
    '        '        mskInjurydate.CtlText = ""
    '        '        mskInjurydate.Mask = "##/##/####"
    '        '        mskInjurydate.Format = "MM/dd/yyyy"
    '        '        mskInjurydate.Focus()
    '        '        Exit Sub
    '        '    End If

    '        'End If
    '        SetData(Arrlist)
    '        'RefreshInsurance()
    '        'Dim ArrInsurance(chkLstInsurance.CheckedItems.Count - 1) As Int64
    '        Dim ArrReferral(cmbReferrals.Items.Count - 1) As Long
    '        Dim mlist As myList
    '        Dim i As Integer
    '        'If chkLstInsurance.CheckedItems.Count <> 0 Then
    '        '    For i = 0 To chkLstInsurance.CheckedItems.Count - 1
    '        '        mlist = chkLstInsurance.CheckedItems(i)
    '        '        ArrInsurance(i) = mlist.Index
    '        '    Next
    '        'End If
    '        If cmbReferrals.Items.Count <> 0 Then
    '            For i = 0 To cmbReferrals.Items.Count - 1
    '                mlist = cmbReferrals.Items.Item(i)
    '                ArrReferral(i) = mlist.Index
    '            Next
    '        End If
    '        If intId = 0 Then
    '            _lPatientDirectiveID = PatientRegistration.AddData(Arrlist, ArrReferral, optWebCam.Checked)
    '        Else
    '            _lPatientDirectiveID = intId
    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20070125 -Mahesh 
    '            If CheckPatientStatus(gnPatientID, , True) = False Then
    '                Exit Sub
    '            End If
    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20060918 -Mahesh 
    '            '''' Check if Current User has Admin Rights 
    '            'If gblnIsAdmin = False Then
    '            '    ''if Not then warn user
    '            '    Dim oclsPatReg As New ClsPatientRegistrationDBLayer
    '            '    With oclsPatReg
    '            '        Dim PatientStatus As String = ""
    '            '        PatientStatus = .PatientStatus(gnPatientID)
    '            '        oclsPatReg = Nothing
    '            '        '' If Patient Status Is "Legal Pending" or "Decesed" then 
    '            '        '' dont Allow any activity against this Patient
    '            '        If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Or PatientStatus = gtsrPatientStatus_LockCharts Then
    '            '            MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Adminstrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            '            Exit Sub
    '            '        End If
    '            '    End With
    '            'Else
    '            '    '' Allow User to save the Changes 
    '            'End If

    '            '''''<><><><><><><><><><><>''''
    '            '' Original UpadteData Procedure
    '            '' 20061225- Mahesh
    '            '' Commented By Mahesh for --" Patient Change History "
    '            'PatientRegistration.UpdateData(Arrlist, ArrReferral, blnPhotoModified)

    '            '' Patient Change History
    '            '' 20061225- Mahesh
    '            Dim IsChange As Boolean = False
    '            If IsDataChange() = True Then
    '                '' If PAtient Recodrd
    '                IsChange = True
    '            Else
    '                IsChange = False
    '            End If

    '            PatientRegistration.UpdateData(Arrlist, ArrReferral, ArrPatientOriginalData, blnPhotoModified, IsChange)
    '        End If

    '        '// Vinayak - 5 Feb 2007 //
    '        Dim _ScanDocFlag As Boolean = False
    '        If intId = 0 Then
    '            If _lPatientDirectiveID > 0 Then
    '                If chkDirective.Checked = True Then
    '                    _ScanDocFlag = True
    '                End If
    '            End If
    '        ElseIf intId > 0 Then
    '            If _lPatientDirectiveID > 0 Then
    '                If chkDirective.Checked = True Then
    '                    _ScanDocFlag = True
    '                End If
    '            End If
    '        End If

    '        If _ScanDocFlag = True Then
    '            '//Check for DMS Path & Patient Directive Category
    '            Dim oDMSPath As New gloStream.gloDMS.Supporting.Supporting
    '            'Check DMS System Path is Correct
    '            If oDMSPath.IsDMSSystem(DMSRootPath) = False Then
    '                _ScanDocFlag = False
    '                MessageBox.Show("Document Management System Path not set to scan patient directive document, please use Tools->Setting command to set DMS path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Else
    '                Dim oChk As New gloStream.gloDMS.DocumentCategory.DocumentCategory
    '                Dim oCat As New gloStream.gloDMS.DocumentCategory.Category
    '                oCat.Name = gDMSCategory_PatientDirective : oCat.IsDeleted = False
    '                If oChk.IsExists(oCat) = False Then
    '                    _ScanDocFlag = False
    '                    MessageBox.Show("DMS Category not set for advance directive to scan patient directive document. " & vbCrLf & "Please set it from DMS Category and then add documents from Scan Document functionality", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                End If
    '                oChk = Nothing
    '                oCat = Nothing
    '            End If
    '            oDMSPath = Nothing
    '            '//Check for DMS Path & Patient Directive Category
    '        End If

    '        If _ScanDocFlag = True Then
    '            If intId = 0 Then
    '                Set_ScanDocumentEvent(sender, e, gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _lPatientDirectiveID)
    '            Else
    '                If _PatientDirectiveStatus = True Then
    '                    If MessageBox.Show("Do you want to add more documents against patient directive?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
    '                        Set_ScanDocumentEvent(sender, e, gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _lPatientDirectiveID)
    '                    End If
    '                Else
    '                    Set_ScanDocumentEvent(sender, e, gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _lPatientDirectiveID)
    '                End If
    '            End If
    '        End If

    '        '// Vinayak - 5 Feb 2007 //

    '        gblnPatientAdded = True
    '        gstrPatientCode = txtPatientCode.Text
    '        Me.Close()
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub DemoHxPatientRecordChange()
        Dim frmHt As New frmPatientChangeHistory(ID)
        With frmHt
            '.MdiParent = CType(Me.ParentForm, MainMenu)
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog()
            .BringToFront()
        End With
    End Sub

    Private Sub btnpcp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        intStatus = 4
        'LoadGrid()
        Call loadC1flexgrid()
    End Sub

    Private Sub btnPharmacy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        intStatus = 1
        'LoadGrid()
        Call loadC1flexgrid()
    End Sub

    Public Sub loadC1flexgrid()
        Try
            'oC1flex = New myUserControl()
            'Dim c1f As New C1.Win.C1FlexGrid.C1FlexGrid
            If load_c1flexControlData() = False Then
                Exit Sub
            End If

            If intStatus <> 3 Then
                pnl.Top = tbPatientRegistration.Top + GroupBox12.Top + cmbProvider.Top + cmbProvider.Height
                pnl.Left = tbPatientRegistration.Left + GroupBox12.Left + 5
                pnl.Height = GroupBox9.Height + GroupBox14.Height - 100
                pnl.Width = GroupBox12.Width
                pnl.BackColor = Color.GhostWhite
                pnl.BorderStyle = BorderStyle.FixedSingle
                pnl.Visible = True
                visibleBtns(False)
                'tbPatientRegistration.TabPages(0).Controls.Add(pnl)
                'Me.TabControlPanel1.Controls.Add(pnl)
                tbPatientRegistration.Controls.Add(pnl)
                pnl.BringToFront()
            Else
                pnl.Top = tbPatientRegistration.Top + GroupBox9.Top + txtspousename.Top
                pnl.Left = tbPatientRegistration.Left + trInsurance.Width + 10
                pnl.Height = GroupBox9.Height + GroupBox14.Height - 100
                pnl.Width = GroupBox14.Width - 10
                pnl.BackColor = Color.GhostWhite
                pnl.BorderStyle = BorderStyle.FixedSingle
                pnl.Visible = True
                visibleBtns(False)
                'tbPatientRegistration.TabPages(3).Controls.Add(pnl)
                TabPage4.Controls.Add(pnl) ' .Controls.Add(pnl)
                pnl.BringToFront()
            End If
            'btnClosePanel.Top = pnl.Top + 10
            'btnClosePanel.Left = pnl.Left + 10

            Call visibleBtns(False)

            ' ''btnClosePanel.Visible = True
            ' ''tbPatientRegistration.TabPages(0).Controls.Add(btnClosePanel)
            ' ''btnClosePanel.BringToFront()

            'If load_c1flexControlData() = False Then
            '    Dim S As Object
            '    Dim er As EventArgs
            '    dgCustomGrid_CloseClick(S, er)
            'End If
            ' ''c1panel.Selectsearch(CustomDataGrid.enmcontrol.Search)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub setC1Style()
        pnl.Controls.Add(oC1flex)
        oC1flex.Dock = DockStyle.Fill
        ' oC1flex._flexObj.Cols(2).Selected = True
        oC1flex.BringToFront()
        oC1flex.Show()
    End Sub

    Public Function load_c1flexControlData() As Boolean
        Try
            Dim blnLoadGrid As Boolean = True
            ReferralCount = 0
            ' this function is modified on 20070523
            If intStatus = 1 Then
                dt = PatientRegistration.FillControls("M")
                If Not IsNothing(dt) Then
                    'If dt.Rows.Count > 0 Then
                    If IsPharmacy = True Then
                        oC1flex = New gloUC_CustomSearchInC1Flexgrid(dt, False, True)
                    Else
                        oC1flex = New gloUC_CustomSearchInC1Flexgrid(dt, False, False)
                    End If

                    Call setC1Style()
                    blnLoadGrid = True
                    'Else
                    '    Return False
                    'End If
                Else
                    blnLoadGrid = False
                End If
                'oC1flex.Dock = DockStyle.Fill
                'pnl.Controls.Add(oC1flex)
                'oC1flex.Dock = DockStyle.Fill
                'oC1flex.BringToFront()
                'oC1flex.Show()
                'dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
                'PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(1).ColumnName)
            ElseIf intStatus = 2 Then
                dt = PatientRegistration.FillControls("P")
                If Not IsNothing(dt) Then
                    'If dt.Rows.Count > 0 Then
                    oC1flex = New gloUC_CustomSearchInC1Flexgrid(dt, True)
                    Call setC1Style()
                    blnLoadGrid = True
                    'Else
                    '    Return False
                    'End If
            Else
                blnLoadGrid = False
            End If
            'oC1flex(dt)
            'dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
            'Sort by default on LastName
            'PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(3).ColumnName)
            ElseIf intStatus = 3 Then
                dt = PatientRegistration.FillControls("I")
                '' modification on 20070605 by bipin

                '-------
                'code commented by sarika 17th aug 07
                'oC1flex = New gloUC_CustomSearchInC1Flexgrid(dt, True)
                'code added by sarika 17th aug 07
                If Not IsNothing(dt) Then
                    'If dt.Rows.Count > 0 Then
                    oC1flex = New gloUC_CustomSearchInC1Flexgrid(dt, False)

                    '-----------
                    Call setC1Style()
                    blnLoadGrid = True
                    'Else
                    '    Return False
                    'End If
            Else
                blnLoadGrid = False
            End If
            ' oC1flex(dt)
            'dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
            'PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(1).ColumnName)
            ElseIf intStatus = 4 Then
                dt = PatientRegistration.FillControls("P")
                If Not IsNothing(dt) Then
                    'If dt.Rows.Count > 0 Then
                    oC1flex = New gloUC_CustomSearchInC1Flexgrid(dt, False)
                    Call setC1Style()
                    ReferralCount = dt.Rows.Count
                    blnLoadGrid = True
                    ''oC1flex(dt)
                    ''dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
                    ''Sort by default on LastName
                    ''PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(3).ColumnName)
                    'Else
                    '    Return False
                    'End If
            Else
                'ReferralCount = dt.Rows.Count
                'If ReferralCount > 0 Then
                '    Return True
                'Else
                blnLoadGrid = False
            End If
            End If

            Return blnLoadGrid
            'HideColumns()

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    'Private Sub btnprev1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
    '    txtMother_fName.Focus()
    'End Sub

    Private Sub btnPrev2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev2.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
        txtPatientCode.Focus()
    End Sub

    Private Sub btnReferrals_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        intStatus = 2
        'LoadGrid()
        Call loadC1flexgrid()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RefreshInsurance()
        key = -1
        trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    End Sub

    'Private Sub btnRefresh_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ToolTip1 = New System.Windows.Forms.ToolTip
    '    ToolTip1.SetToolTip(Me.btnAdd, "Refresh Insurance")
    'End Sub

    Private Sub btnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStart.Click
        If Trim(btnStart.Text) = "Browse Photo" Then
            Try
                Dim factorwidth As Double
                Dim factorheight As Double
                factorwidth = 1
                factorheight = 1
                With dlgOpenFile
                    .Title = "Select Clinic Logo"
                    .Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif"
                    .CheckFileExists = True
                    .Multiselect = False
                    .ShowHelp = False
                    .ShowReadOnly = False
                End With
                If dlgOpenFile.ShowDialog = DialogResult.OK Then
                    picFinalPatient.Image = Image.FromFile(dlgOpenFile.FileName)
                    If picFinalPatient.Image.Width > 0.9 * picFinalPatient.Width Then
                        factorwidth = (picFinalPatient.Width * 0.9) / picFinalPatient.Image.Width
                    End If
                    If picFinalPatient.Image.Height > 0.9 * picFinalPatient.Height Then
                        factorheight = (picFinalPatient.Height * 0.9) / picFinalPatient.Image.Height
                    End If
                    picFinalPatient.Image = New Bitmap(picFinalPatient.Image, New Size(picFinalPatient.Image.Size.Width * factorwidth, picFinalPatient.Image.Size.Height * factorheight))
                    picFinalPatient.SizeMode = PictureBoxSizeMode.CenterImage
                    blnPhotoModified = True
                End If
            Catch objErr As Exception
                'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            picFinalPatient.Visible = False
            picWebCamPatient.Visible = True
            myCam = New clsWebCam
            If myCam.initCam(picWebCamPatient.Handle.ToInt32, picWebCamPatient.Height, picWebCamPatient.Width) = True Then
                btnCapture.Visible = True
                btnCapture.Enabled = True
                btnClear.Enabled = False
                btnStart.Enabled = False
            End If
        End If
        'If Trim(btnStart.Text) = "Browse Photo" Then
        '    Try
        '        Dim factorwidth As Double
        '        Dim factorheight As Double
        '        factorwidth = 1
        '        factorheight = 1
        '        With dlgOpenFile
        '            .Title = "Select Clinic Logo"
        '            .Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif"
        '            .CheckFileExists = True
        '            .Multiselect = False
        '            .ShowHelp = False
        '            .ShowReadOnly = False
        '        End With
        '        If dlgOpenFile.ShowDialog = DialogResult.OK Then
        '            picFinalPatient.Image = Image.FromFile(dlgOpenFile.FileName)
        '            If picFinalPatient.Image.Width > 0.9 * picFinalPatient.Width Then
        '                factorwidth = (picFinalPatient.Width * 0.9) / picFinalPatient.Image.Width
        '            End If
        '            If picFinalPatient.Image.Height > 0.9 * picFinalPatient.Height Then
        '                factorheight = (picFinalPatient.Height * 0.9) / picFinalPatient.Image.Height
        '            End If
        '            picFinalPatient.Image = New Bitmap(picFinalPatient.Image, New Size(picFinalPatient.Image.Size.Width * factorwidth, picFinalPatient.Image.Size.Height * factorheight))
        '            picFinalPatient.SizeMode = PictureBoxSizeMode.CenterImage
        '            blnPhotoModified = True
        '        End If
        '    Catch objErr As Exception
        '        'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        'Else
        '    picFinalPatient.Visible = False
        '    picWebCamPatient.Visible = True
        '    myCam = New clsWebCam
        '    If myCam.initCam(picWebCamPatient.Handle.ToInt32) = True Then
        '        btnCapture.Visible = True
        '        btnCapture.Enabled = True
        '        btnStart.Enabled = False
        '    End If
        'End If
    End Sub

    Private Sub cbCopyAddress1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCopyAddress1.CheckedChanged
        Try
            If cbCopyAddress1.Checked = True Then
                txtFather_Address1.Text = Trim(txtAddress1.Text)
                txtFather_Address2.Text = Trim(txtAddress2.Text)
                txtFather_City.Text = Trim(txtCity.Text)
                cmbFather_State.Text = Trim(cmbState.Text)
                txtFather_Zip.Text = Trim(txtZip.Text)
                txtFather_County.Text = Trim(txtCounty.Text)

                txtFather_Fax.Text = Trim(txtfax.Text)
                txtFather_Email.Text = Trim(txtemail.Text)

                mskFather_Phone1.Text = mskPhone1.Text
                mskFather_Mobile1.Text = mskMobile1.Text

            Else

                txtFather_Address1.Text = ""
                txtFather_Address2.Text = ""
                txtFather_City.Text = ""
                cmbFather_State.Text = ""
                txtFather_Zip.Text = ""
                txtFather_County.Text = ""

                txtFather_Fax.Text = ""
                txtFather_Email.Text = ""

                mskFather_Phone1.Text = ""
                mskFather_Mobile1.Text = ""

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbCopyAddress2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCopyAddress2.CheckedChanged
        Try
            If cbCopyAddress2.Checked = True Then
                txtMother_Address1.Text = Trim(txtAddress1.Text)
                txtMother_Address2.Text = Trim(txtAddress2.Text)
                txtMother_City.Text = Trim(txtCity.Text)
                cmbMother_State.Text = Trim(cmbState.Text)
                txtMother_Zip.Text = Trim(txtZip.Text)
                txtMother_County.Text = Trim(txtCounty.Text)

                txtMother_Email.Text = Trim(txtemail.Text)
                txtMother_Fax.Text = Trim(txtfax.Text)

                mskMother_Phone1.Text = mskPhone1.Text
                mskMother_Mobile1.Text = mskMobile1.Text
            Else
                txtMother_Address1.Text = ""
                txtMother_Address2.Text = ""
                txtMother_City.Text = ""
                cmbMother_State.Text = ""
                txtMother_Zip.Text = ""
                txtMother_County.Text = ""

                txtMother_Email.Text = ""
                txtMother_Fax.Text = ""

                mskMother_Phone1.Text = ""
                mskMother_Mobile1.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbCopyAddress3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCopyAddress3.CheckedChanged
        Try
            If cbCopyAddress3.Checked = True Then
                txtGuardian_Address1.Text = Trim(txtAddress1.Text)
                txtGuardian_Address2.Text = Trim(txtAddress2.Text)
                txtGuardian_City.Text = Trim(txtCity.Text)
                cmbGuardian_State.Text = Trim(cmbState.Text)
                txtGuardian_Zip.Text = Trim(txtZip.Text)
                txtGuardian_County.Text = Trim(txtCounty.Text)

                txtGuardian_Fax.Text = Trim(txtfax.Text)
                txtGuardian_Email.Text = Trim(txtemail.Text)

                mskGuardian_Phone1.Text = mskPhone1.Text
                mskGuardian_Mobile1.Text = mskMobile1.Text

            Else

                txtGuardian_Address1.Text = ""
                txtGuardian_Address2.Text = ""
                txtGuardian_City.Text = ""
                cmbGuardian_State.Text = ""
                txtGuardian_Zip.Text = ""
                txtGuardian_County.Text = ""

                txtGuardian_Fax.Text = ""
                txtGuardian_Email.Text = ""

                mskGuardian_Phone1.Text = ""
                mskGuardian_Mobile1.Text = ""

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub chkGuarantor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGuarantor.CheckedChanged
    '    If chkGuarantor.Checked = True Then
    '        txtGuarantor.Text = txtfname.Text & " " & txtmName.Text & " " & txtlname.Text
    '    Else
    '        txtGuarantor.Text = ""
    '    End If
    'End Sub

    'Private Sub chkSameasPatient_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPatientStatus.CheckedChanged
    '    Try
    '        ' modification on 20070522 by Bipin
    '        If cmbPatientStatus.Checked = True Then
    '            txtSubscriberName.Text = Trim(txtfname.Text) & " " & Trim(txtmName.Text) & " " & Trim(txtlname.Text)
    '            txtEmployer.Text = Trim(txtWLocation.Text)
    '            'dtpIDOB.Value = dtpDOB.Value
    '            If Len(mskDOB1.Text) = 10 Then
    '                mskIDOB1.Text = mskDOB1.Text
    '            End If
    '        Else
    '            txtSubscriberName.Text = ""
    '            mskIDOB1.Mask = ""
    '            mskIDOB1.Text = ""
    '            mskIDOB1.Mask = "##/##/####"
    '            'mskIDOB.Format = "MM/dd/yyyy"
    '        End If

    '        'If chkSameasPatient.Checked = True Then
    '        '    txtSubscriberName.Text = Trim(txtfname.Text) & " " & Trim(txtmName.Text) & " " & Trim(txtlname.Text)
    '        '    txtEmployer.Text = Trim(txtWLocation.Text)
    '        '    'dtpIDOB.Value = dtpDOB.Value
    '        '    If Len(mskDOB.ClipText) = 8 Then
    '        '        mskIDOB.CtlText = mskDOB.CtlText
    '        '    End If
    '        'Else
    '        '    txtSubscriberName.Text = ""
    '        '    mskIDOB.Mask = ""
    '        '    mskIDOB.CtlText = ""
    '        '    mskIDOB.Mask = "##/##/####"
    '        '    mskIDOB.Format = "MM/dd/yyyy"
    '        'End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub dgCustomGrid_AddClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.AddClick
        Try
            Dim frm As frmContactMst
            If intStatus = 3 Then
                frm = New frmContactMst(False, "Insurance")
                frm.Text = "Add Contacts for Insurance"
            ElseIf intStatus = 1 Then
                frm = New frmContactMst(False, "Pharmacy")
                frm.Text = "Add Contacts for Pharmacy"
            ElseIf intStatus = 2 Then
                frm = New frmContactMst(True, "Physician")
                frm.Text = "Add Contacts for Physician"
            ElseIf intStatus = 4 Then
                frm = New frmContactMst(True, "Physician")
                frm.Text = "Add Contacts for Referrals"
            End If

            'Dim frm As ContactMaster
            'If intStatus = 3 Then
            '    frm = New ContactMaster(False, "Insurance")
            '    frm.Text = "Add Contacts for Insurance"
            'ElseIf intStatus = 1 Then
            '    frm = New ContactMaster(False, "Pharmacy")
            '    frm.Text = "Add Contacts for Pharmacy"
            'ElseIf intStatus = 2 Then
            '    frm = New ContactMaster(True, "Physician")
            '    frm.Text = "Add Contacts for Physician"
            'ElseIf intStatus = 4 Then
            '    frm = New ContactMaster(True, "Physician")
            '    frm.Text = "Add Contacts for Referrals"
            'End If

            frm.ShowDialog()

            ' ButtonX14_Click(sender, e)
            ' modify cod eon 20070613 to refresh the c1Grid
            Call loadC1flexgrid()

            ' default selection of new added row
            Dim searchdt = frm.strData

            'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            'c1Uc.SortDataview(searchdt)

            Dim searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
            oC1flex._UCflex.Select(searchrow, 1)

            'code commented on 20070524 Bipin
            'BindGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        Try
            'dgCustomGrid.Visible = False
            pnl.Visible = False
            Call visibleBtns(True)

            Select Case intStatus
                Case 1
                    btnPharmacyBrowse.Focus()
                Case 2
                    btnReferralsBrowse.Focus()
                Case 3
                    btnInsurance.Focus()
                Case 4
                    btnPharmacyBrowse.Focus()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgCustomGrid_Dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.Dblclick
        'SetGridValues()
        SetGridValues_c1UC(1)
    End Sub

    Private Sub dgCustomGrid_MouseUpClick(ByVal sender As Object, ByVal e As Object) Handles dgCustomGrid.MouseUpClick
        'Select entire row in Grid
        If dgCustomGrid.GetCurrentrowIndex >= 0 Then
            dgCustomGrid.GetSelect(dgCustomGrid.GetCurrentrowIndex)
        End If
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        'SetGridValues()
        SetGridValues_c1UC()
    End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            PatientRegistration.SetRowFilter(dgCustomGrid.SearchText)
            'Dim dt As DataTable
            ReferralCount = PatientRegistration.DsDataview.Count

            'If Not IsNothing(dt) Then
            '    ReferralCount = dt.Rows.Count
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
    '    Try
    '        If key >= 0 Then
    '            PatientRegistration.DeleteInsurance(key)
    '            If trInsurance.SelectedNode.GetNodeCount(False) = 0 Then
    '                trInsurance.SelectedNode.Remove()
    '                RefreshInsurance()
    '                key = -1
    '                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '            End If
    '        End If
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub mnuPrimary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPrimary.Click
    '    Try
    '        If key <> -1 Then
    '            PatientRegistration.SetasPrimary(key, True)
    '            chkPrimary.Checked = True
    '            trInsurance.SelectedNode.ForeColor = Color.Blue
    '            trInsurance.SelectedNode.ImageIndex = 1
    '            trInsurance.SelectedNode.SelectedImageIndex = 1
    '            Dim mynode As TreeNode
    '            For Each mynode In trInsurance.Nodes.Item(0).Nodes
    '                If Not mynode Is trInsurance.SelectedNode Then
    '                    mynode.ForeColor = Color.Black
    '                    mynode.ImageIndex = 2
    '                    mynode.SelectedImageIndex = 2
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub mskDOB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskDOB.ClipText)) < 2 Then
    '        mskDOB.SelStart = Len(Trim(mskDOB.ClipText))
    '    ElseIf Len(Trim(mskDOB.ClipText)) < 4 Then
    '        mskDOB.SelStart = Len(Trim(mskDOB.ClipText)) + 1
    '    ElseIf Len(Trim(mskDOB.ClipText)) < 8 Then
    '        mskDOB.SelStart = Len(Trim(mskDOB.ClipText)) + 2
    '    End If
    '    If Len(mskDOB.ClipText) > 0 Then
    '        mskDOB.CtlText = mskDOB.FormattedText
    '    End If
    'End Sub

    'Private Sub mskIDOB_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskIDOB.ClipText)) < 2 Then
    '        mskIDOB.SelStart = Len(Trim(mskIDOB.ClipText))
    '    ElseIf Len(Trim(mskIDOB.ClipText)) < 4 Then
    '        mskIDOB.SelStart = Len(Trim(mskIDOB.ClipText)) + 1
    '    ElseIf Len(Trim(mskIDOB.ClipText)) < 8 Then
    '        mskIDOB.SelStart = Len(Trim(mskIDOB.ClipText)) + 2
    '    End If
    '    If Len(mskIDOB.ClipText) > 0 Then
    '        mskIDOB.CtlText = mskIDOB.FormattedText
    '    End If
    'End Sub

    'Private Sub mskInjurydate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskInjurydate.ClipText)) < 2 Then
    '        mskInjurydate.SelStart = Len(Trim(mskInjurydate.ClipText))
    '    ElseIf Len(Trim(mskInjurydate.ClipText)) < 4 Then
    '        mskInjurydate.SelStart = Len(Trim(mskInjurydate.ClipText)) + 1
    '    ElseIf Len(Trim(mskInjurydate.ClipText)) < 8 Then
    '        mskInjurydate.SelStart = Len(Trim(mskInjurydate.ClipText)) + 2
    '    End If
    '    If Len(mskInjurydate.ClipText) > 0 Then
    '        mskInjurydate.CtlText = mskInjurydate.FormattedText
    '    End If
    'End Sub

    'Private Sub mskIPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskIPhone.ClipText)) < 3 Then
    '        mskIPhone.SelStart = Len(Trim(mskIPhone.ClipText)) + 1
    '    ElseIf Len(Trim(mskIPhone.ClipText)) < 6 Then
    '        mskIPhone.SelStart = Len(Trim(mskIPhone.ClipText)) + 3
    '    ElseIf Len(Trim(mskIPhone.ClipText)) < 10 Then
    '        mskIPhone.SelStart = Len(Trim(mskIPhone.ClipText)) + 4
    '    End If
    'End Sub

    'Private Sub mskPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskPhone.ClipText)) < 3 Then
    '        mskPhone.SelStart = Len(Trim(mskPhone.ClipText)) + 1
    '    ElseIf Len(Trim(mskPhone.ClipText)) < 6 Then
    '        mskPhone.SelStart = Len(Trim(mskPhone.ClipText)) + 3
    '    ElseIf Len(Trim(mskPhone.ClipText)) < 10 Then
    '        mskPhone.SelStart = Len(Trim(mskPhone.ClipText)) + 4
    '    End If
    'End Sub

    'Private Sub mskRegistrationdate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskRegistrationdate.ClipText)) < 2 Then
    '        mskRegistrationdate.SelStart = Len(Trim(mskRegistrationdate.ClipText))
    '    ElseIf Len(Trim(mskRegistrationdate.ClipText)) < 4 Then
    '        mskRegistrationdate.SelStart = Len(Trim(mskRegistrationdate.ClipText)) + 1
    '    ElseIf Len(Trim(mskRegistrationdate.ClipText)) < 8 Then
    '        mskRegistrationdate.SelStart = Len(Trim(mskRegistrationdate.ClipText)) + 2
    '    End If
    '    If Len(mskRegistrationdate.ClipText) > 0 Then
    '        mskRegistrationdate.CtlText = mskRegistrationdate.FormattedText
    '    End If
    'End Sub

    'Private Sub mskspousePhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskspousePhone.ClipText)) < 3 Then
    '        mskspousePhone.SelStart = Len(Trim(mskspousePhone.ClipText)) + 1
    '    ElseIf Len(Trim(mskspousePhone.ClipText)) < 6 Then
    '        mskspousePhone.SelStart = Len(Trim(mskspousePhone.ClipText)) + 3
    '    ElseIf Len(Trim(mskspousePhone.ClipText)) < 10 Then
    '        mskspousePhone.SelStart = Len(Trim(mskspousePhone.ClipText)) + 4
    '    End If
    'End Sub

    'Private Sub MskSSn_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(MskSSn.ClipText)) < 3 Then
    '        MskSSn.SelStart = Len(Trim(MskSSn.ClipText))
    '    ElseIf Len(Trim(MskSSn.ClipText)) < 5 Then
    '        MskSSn.SelStart = Len(Trim(MskSSn.ClipText)) + 1
    '    ElseIf Len(Trim(MskSSn.ClipText)) < 9 Then
    '        MskSSn.SelStart = Len(Trim(MskSSn.ClipText)) + 2
    '    End If
    'End Sub
    Private Sub MskSSn1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Len(Trim(MskSSn1.Text)) < 3 Then
            MskSSn1.SelectionStart = Len(Trim(MskSSn1.Text))
        ElseIf Len(Trim(MskSSn1.Text)) < 5 Then
            MskSSn1.SelectionStart = Len(Trim(MskSSn1.Text)) + 1
        ElseIf Len(Trim(MskSSn1.Text)) < 9 Then
            MskSSn1.SelectionStart = Len(Trim(MskSSn1.Text)) + 2
        End If
    End Sub

    'Private Sub mskSurgeryDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskSurgeryDate.ClipText)) < 2 Then
    '        mskSurgeryDate.SelStart = Len(Trim(mskSurgeryDate.ClipText))
    '    ElseIf Len(Trim(mskSurgeryDate.ClipText)) < 4 Then
    '        mskSurgeryDate.SelStart = Len(Trim(mskSurgeryDate.ClipText)) + 1
    '    ElseIf Len(Trim(mskSurgeryDate.ClipText)) < 8 Then
    '        mskSurgeryDate.SelStart = Len(Trim(mskSurgeryDate.ClipText)) + 2
    '    End If
    '    If Len(mskSurgeryDate.ClipText) > 0 Then
    '        mskSurgeryDate.CtlText = mskSurgeryDate.FormattedText
    '    End If
    'End Sub

    'Private Sub mskwPhone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Len(Trim(mskwPhone.ClipText)) < 3 Then
    '        mskwPhone.SelStart = Len(Trim(mskwPhone.ClipText)) + 1
    '    ElseIf Len(Trim(mskwPhone.ClipText)) < 6 Then
    '        mskwPhone.SelStart = Len(Trim(mskwPhone.ClipText)) + 3
    '    ElseIf Len(Trim(mskwPhone.ClipText)) < 10 Then
    '        mskwPhone.SelStart = Len(Trim(mskwPhone.ClipText)) + 4
    '    End If
    'End Sub

    Private Sub optBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optBrowse.Click
        btnStart.Enabled = True
        btnStart.Text = "Browse Photo"
        btnCapture.Visible = False
        picFinalPatient.Visible = True
        picWebCamPatient.Visible = False
        'btnStart.Enabled = True
        'btnStart.Text = "Browse Photo"
        'btnCapture.Visible = False
        'picFinalPatient.Visible = True
        'picWebCamPatient.Visible = False
    End Sub

    Private Sub optWebCam_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optWebCam.Click
        btnStart.Text = "Start"
        btnCapture.Visible = True
        btnCapture.Enabled = False
        picFinalPatient.Visible = False
        picWebCamPatient.Visible = True
        'btnStart.Text = "Start"
        'btnStart.Visible = True
        'btnCapture.Visible = True
        'btnCapture.Enabled = True
        'picFinalPatient.Visible = False
        'picWebCamPatient.Visible = True
    End Sub


    'Private Sub tbPatientRegistration_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPatientRegistration.SelectedTabChanged '  SelectedIndexChanged
    '    Try
    '        Select Case tbPatientRegistration.SelectedTab ' .SelectedIndex

    '            Case 0
    '                txtPatientCode.Select()
    '            Case 1
    '                cmbEmploymentStatus.Select()
    '            Case 2
    '                trInsurance.Select()
    '                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub trInsurance_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trInsurance.AfterSelect
        Try
            'Commented on 21/09/2005
            'Dim mychildnode As TreeNode
            'If Not IsNothing(e.Node) Then
            '    mychildnode = e.Node
            '    'check if node is parentnode
            '    If Not trInsurance.Nodes.Item(0) Is mychildnode Then
            '        key = mychildnode.Index
            '        If key <> -1 Then
            '            Dim PatientInsurance As New ClsPatientInsurance
            '            PatientRegistration.GetInsuranceCol(PatientInsurance, key)
            '            txtInsuranceName.Tag = PatientInsurance.InsuranceId
            '            txtInsuranceName.Text = PatientInsurance.InsuranceName
            '            txtSubscriberID.Text = PatientInsurance.SubscriberId
            '            txtSubscriberName.Text = PatientInsurance.Subscribername
            '            txtSubscriberPolicy.Text = PatientInsurance.SubscriberPolicy
            '            txtEmployer.Text = PatientInsurance.Employer
            '            If Len(PatientInsurance.Phone) = 10 Then
            '                mskIPhone.Text = splittext(PatientInsurance.Phone, False)
            '            End If

            '            txtGroup.Text = PatientInsurance.Group
            '            If PatientInsurance.Primaryflag = True Then
            '                chkPrimary.Checked = True
            '            Else
            '                chkPrimary.Checked = False
            '            End If
            '            'dtpIDOB.Checked = PatientInsurance.Checked
            '            'If PatientInsurance.Checked = True Then
            '            '    dtpIDOB.Value = PatientInsurance.DOB
            '            'End If
            '            If PatientInsurance.Checked = True Then
            '                mskIDOB.CtlText = Format(PatientInsurance.DOB.Date, "MM/dd/yyyy")
            '            End If
            '            btnAdd.Text = "Update"
            '        End If
            '    Else
            '        RefreshInsurance()
            '    End If
            'End If
            'Commented on 21/09/2005

            'Check if selectednode exists
            If Not IsNothing(trInsurance.SelectedNode) Then

                'check if node is parentnode
                If Not trInsurance.SelectedNode Is trInsurance.Nodes.Item(0) Then
                    key = trInsurance.SelectedNode.Index
                    If key <> -1 Then
                        'sarika 6th nov 07
                        clearInsCtrls()
                        '-----
                        'Populate Data from Insurance Collection against selected node
                        Dim PatientInsurance As New ClsPatientInsurance
                        PatientRegistration.GetInsuranceCol(PatientInsurance, key)
                        ' code modified on 20070612 for modification for multiple selection of the Insurance
                        'txtInsuranceName.Tag = PatientInsurance.InsuranceId
                        'txtInsuranceName.Text = PatientInsurance.InsuranceName
                        'cmbInsuranceNew.Tag = PatientInsurance.InsuranceId
                        'cmbInsuranceNew.Text = PatientInsurance.InsuranceName

                        txtInsuranceNew.Tag = PatientInsurance.InsuranceId
                        txtInsuranceNew.Text = PatientInsurance.InsuranceName


                        txtSubscriberID.Text = PatientInsurance.SubscriberId
                        txtSubscriberName.Text = PatientInsurance.Subscribername
                        txtSubscriberPolicy.Text = PatientInsurance.SubscriberPolicy
                        txtEmployer.Text = PatientInsurance.Employer

                        ' modification on 20070522 by Bipin
                        If Len(PatientInsurance.Phone) = 10 Then
                            mskIPhone1.Text = splittext(PatientInsurance.Phone, False)
                        Else
                            mskIPhone1.Text = "(___)-___-____"
                        End If
                        'If Len(PatientInsurance.Phone) = 10 Then
                        '    mskIPhone.CtlText = splittext(PatientInsurance.Phone, False)
                        'Else
                        '    mskIPhone.CtlText = "(___)-___-____"
                        'End If

                        txtGroup.Text = PatientInsurance.Group
                        If PatientInsurance.Primaryflag = True Then
                           
                            chkPrimary.Checked = True
                        Else
                            chkPrimary.Checked = False

                        End If
                        'dtpIDOB.Checked = PatientInsurance.Checked
                        'If PatientInsurance.Checked = True Then
                        '    dtpIDOB.Value = PatientInsurance.DOB
                        'End If

                        ' modification on 20070522 by Bipin
                        If PatientInsurance.Checked = True Then
                            mskIDOB1.Text = Format(PatientInsurance.DOB.Date, "MM/dd/yyyy")
                            'sarika 6th nov 07
                            'sarika 23rd july 08
                            'same as patient
                            '  chkSameasPatient.Checked = True
                            '-----------------
                        Else
                            mskIDOB1.Text = Format(PatientInsurance.DOB.Date, "MM/dd/yyyy")
                        End If
                        'If PatientInsurance.Checked = True Then
                        '    mskIDOB.CtlText = Format(PatientInsurance.DOB.Date, "MM/dd/yyyy")
                        'End If
                        btnAdd.Text = "Update"

                        ''''
                        'sarika 20th aug 07
                        ' strInsuranceText = cmbInsuranceNew.Text
                        strInsuranceText = txtInsuranceNew.Text
                        '''''
                        strSubPolicy = txtSubscriberPolicy.Text
                        strSubId = txtSubscriberID.Text
                        strSubName = txtSubscriberName.Text
                        strEmployer = txtEmployer.Text
                        nPhone = mskIPhone1.Text
                        strGroup = txtGroup.Text
                        chkPrimaryOrNot = chkPrimary.Checked
                        dtDob = mskIDOB1.Text
                        ''''

                    End If
                Else
                    RefreshInsurance()
                    key = -1

                    'sarika 20th aug 07
                    strInsuranceText = txtInsuranceNew.Text
                    '''''
                    strSubPolicy = txtSubscriberPolicy.Text
                    strSubId = txtSubscriberID.Text
                    strSubName = txtSubscriberName.Text
                    strEmployer = txtEmployer.Text
                    nPhone = mskIPhone1.Text
                    strGroup = txtGroup.Text
                    chkPrimaryOrNot = chkPrimary.Checked
                    dtDob = mskIDOB1.Text
                    ''''

                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)

                End If
            End If



            'sarika 20th aug 07
            bTextChangeFlag_insurance = False
            nInsuranceNodeCount = trInsurance.Nodes(0).Nodes.Count
            ''''''''''''
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ' code uncommented on 20070621 by bipin
    Private Sub trInsurance_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trInsurance.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                If Not trInsurance.SelectedNode Is trInsurance.Nodes.Item(0) Then
                    If trInsurance.SelectedNode.GetNodeCount(False) = 0 Then
                        'Bind to contextmenu only when selected Insurance Node is
                        'Not root node
                        trInsurance.ContextMenu = ContextMenu1
                    Else
                        trInsurance.ContextMenu = Nothing
                    End If
                Else
                    trInsurance.ContextMenu = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtfname_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfname.TextChanged
        If chkGuarantor.Checked = True Then
            txtGuarantor.Text = ""
            txtGuarantor.Text = txtfname.Text & " " & txtmName.Text & " " & txtlname.Text
        End If

        If txtfname.Text.Trim.Length > 0 Then
            bTextChangeFlag = True
        Else
            bTextChangeFlag = False
        End If
    End Sub

    Private Sub txtInsuranceName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInsuranceName.KeyPress, txtInsuranceNew.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtlname_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtlname.TextChanged
        If chkGuarantor.Checked = True Then
            txtGuarantor.Text = ""
            txtGuarantor.Text = txtfname.Text & " " & txtmName.Text & " " & txtlname.Text
        End If

        If txtlname.Text.Trim.Length > 0 Then
            bTextChangeFlag = True
        Else
            bTextChangeFlag = False
        End If
    End Sub

    Private Sub txtmName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmName.TextChanged
        If chkGuarantor.Checked = True Then
            txtGuarantor.Text = ""
            txtGuarantor.Text = txtfname.Text & " " & txtmName.Text & " " & txtlname.Text
        End If
    End Sub

    Private Sub txtPatientCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPatientCode.KeyPress
        Try
            If e.KeyChar = "'" Then
                e.KeyChar = ""
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPatientCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPatientCode.Validating, txtPatientCode.Validating
        If Trim(txtPatientCode.Text) <> "" Then
            Try
                If Not PatientRegistration.ValidateDescription(ID, Trim(txtPatientCode.Text)) Then
                    'MsgBox("Duplicate Patientcode")
                    MessageBox.Show("Duplicate Patientcode", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtPatientCode.Text = ""
                    txtPatientCode.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtpcp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpcp.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtPharmacy_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPharmacy.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtwZip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwZip.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMother_Zip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMother_Zip.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFather_Zip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFather_Zip.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtGuardian_Zip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGuardian_Zip.KeyPress
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtZip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress


        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtZip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtZip.Validating
        If Trim(txtZip.Text) <> "" Then
            Try
                Dim dt As DataTable
                dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtZip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtCity.Text = dt.Rows(0).Item(0)
                        AreaCode = dt.Rows(0).Item(2)
                        txtCounty.Text = dt.Rows(0).Item(3)
                        cmbState.Text = dt.Rows(0).Item(1)
                        If Len(Trim(mskPhone1.Text)) < 10 Then
                            mskPhone1.Text = "(" & AreaCode & ")-___-____"
                        ElseIf Len(Trim(mskPhone1.Text)) = 10 Then
                            mskPhone1.Text = "(" & AreaCode & ")-" & Mid(mskPhone1.Text, 4, 3) & "-" & Mid(mskPhone1.Text, 7, 4)
                        End If
                        'If Len(Trim(mskPhone.ClipText)) < 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-___-____"
                        'ElseIf Len(Trim(mskPhone.ClipText)) = 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-" & Mid(mskPhone.ClipText, 4, 3) & "-" & Mid(mskPhone.ClipText, 7, 4)
                        'End If
                    Else
                        txtCity.Text = ""
                        txtCounty.Text = ""
                        cmbState.Text = ""
                        mskPhone1.Mask = ""
                        mskPhone1.Text = ""
                        mskPhone1.Mask = "(###)-###-####"
                    End If

                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


    Public Property ID() As Long 'Store PatientID
        Get
            Return intId
        End Get
        Set(ByVal Value As Long)
            intId = Value
        End Set
    End Property

    Private Sub AddControl()
        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomDataGrid
        Me.Controls.Add(dgCustomGrid)
        'If intStatus = 1 Or intStatus = 2 Or intStatus = 4 Then
        '    dgCustomGrid.Parent = Me.TabPage1
        'Else
        '    dgCustomGrid.Parent = Me.TabPage3
        'End If
        dgCustomGrid.Visible = True
        'dgCustomGrid.Dock = DockStyle.Bottom
        dgCustomGrid.BringToFront()

    End Sub

    Private Sub BindGrid()
        Try
            Dim dt As DataTable
            ReferralCount = 0
            If intStatus = 1 Then
                dt = PatientRegistration.FillControls("M")
                dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
                PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(1).ColumnName)
            ElseIf intStatus = 2 Then

                dt = PatientRegistration.FillControls("P")

                'Dim dv As DataView
                'dv = PatientRegistration.DsDataview
                'dv.Table.Columns.Add("Select", GetType(System.Boolean))
                'dgCustomGrid.SetDatasource(dv)
                dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
                'Sort by default on LastName
                PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(3).ColumnName)
            ElseIf intStatus = 3 Then
                dt = PatientRegistration.FillControls("I")
                dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
                PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(1).ColumnName)
            ElseIf intStatus = 4 Then
                dt = PatientRegistration.FillControls("P")
                dgCustomGrid.SetDatasource(PatientRegistration.DsDataview)
                'Sort by default on LastName
                PatientRegistration.SortDataview(PatientRegistration.DsDataview.Table.Columns(3).ColumnName)
            End If
            If Not IsNothing(dt) Then
                ReferralCount = dt.Rows.Count
            End If
            HideColumns()

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Check for Duplicate Insurance
    Private Function CheckNode(ByVal strNodeText As String) As Boolean
        For Each childNode As TreeNode In mynode.Nodes
            If childNode.Index <> key Then
                If Trim(childNode.Text) = strNodeText Then
                    Return False
                    Exit Function
                End If
            End If
        Next
        Return True
    End Function

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Private Function DrawTab_old(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs, ByVal FocusedBackColor As Color, ByVal FocusedForeColor As Color, ByVal NonFocusedBackColor As Color, ByVal NonFocusedForeColor As Color)
    '    Dim f As Font
    '    Dim backBrush As Brush
    '    Dim foreBrush As Brush
    '    Dim sf As New StringFormat

    '    If e.Index = tbPatientRegistration.SelectedIndex Then
    '        f = New Font(e.Font, FontStyle.Regular)
    '        backBrush = New System.Drawing.SolidBrush(FocusedBackColor)
    '        foreBrush = New System.Drawing.SolidBrush(FocusedForeColor)




    '        Me.tbPatientRegistration.TabPages(e.Index).BackColor = FocusedBackColor
    '        Dim tabName As String = Me.tbPatientRegistration.TabPages(e.Index).Text

    '        Dim rect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height)
    '        sf.Alignment = StringAlignment.Center
    '        e.Graphics.FillRectangle(backBrush, rect)
    '        Dim r As RectangleF = New RectangleF(e.Bounds.X + 1, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4)
    '        e.Graphics.DrawString(tabName, f, foreBrush, r, sf)
    '    Else
    '        f = New Font(e.Font, FontStyle.Regular)
    '        backBrush = New System.Drawing.SolidBrush(FocusedBackColor)
    '        foreBrush = New System.Drawing.SolidBrush(NonFocusedForeColor)



    '        Me.tbPatientRegistration.TabPages(e.Index).BackColor = FocusedBackColor.FromArgb(184, 217, 255)
    '        Dim tabName As String = Me.tbPatientRegistration.TabPages(e.Index).Text

    '        Dim rect As New Rectangle(e.Bounds.X + 1, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height + 1)
    '        sf.Alignment = StringAlignment.Center
    '        e.Graphics.FillRectangle(backBrush, rect)
    '        Dim r As RectangleF = New RectangleF(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4)
    '        e.Graphics.DrawString(tabName, f, foreBrush, r, sf)
    '    End If
    '    sf.Dispose()

    '    f.Dispose()
    '    backBrush.Dispose()
    '    foreBrush.Dispose()


    'End Function

    Private Sub Fill_Location()
        Dim dt As DataTable
        cmbLocation.Items.Clear()
        cmbLocation.Items.Add("")

        dt = PatientRegistration.FillLocation()
        If Not IsNothing(dt) Then
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                cmbLocation.Items.Add(dt.Rows.Item(i)(1))
            Next
            'If dt.Rows.Count > 0 Then
            'End If
        End If
        cmbLocation.SelectedIndex = 0
    End Sub

    Private Sub FillControls()

        txtpcp.Tag = 0
        txtPharmacy.Tag = 0
        cmbEmploymentStatus.SelectedIndex = -1
        cmbEmploymentStatus.Items.Add("Retired")
        cmbEmploymentStatus.Items.Add("Employed")
        cmbEmploymentStatus.Items.Add("UnEmployed")
        cmbEmploymentStatus.Items.Add("Self-Employed")
        cmbEmploymentStatus.Items.Add("Student")

        cmbMaritalstatus.Items.Add("UnMarried")
        cmbMaritalstatus.Items.Add("Married")
        cmbMaritalstatus.Items.Add("Single")
        cmbMaritalstatus.Items.Add("Widowed")
        cmbMaritalstatus.Items.Add("Divorced")

        cmbPatientStatus.SelectedIndex = -1

        '''' Changes on 20070703 by Bipin
        'cmbPatientStatus.Items.Add("Normal")
        cmbPatientStatus.Items.Add("Active")
        ''''

        cmbPatientStatus.Items.Add("Deceased")
        cmbPatientStatus.Items.Add("Legal Pending")
        ''  20070125 
        cmbPatientStatus.Items.Add("Lock Charts")
        ''
        cmbPatientStatus.Items.Add("Default")
        '''' Changes on 20070703 by Bipin
        cmbPatientStatus.Items.Add("Erroneous")
        cmbPatientStatus.Items.Add("Non-Active")
        ''''

        Dim dt As DataTable
        'cmbProvider.Items.Add("")
        dt = PatientRegistration.FillControls("R") 'Provider

        'Add a blank row to datatable before setting source as Provider
        If Not IsNothing(dt) Then
            cmbProvider.DataSource = dt
            cmbProvider.DisplayMember = dt.Columns(1).ColumnName
            cmbProvider.ValueMember = dt.Columns(0).ColumnName
            If dt.Rows.Count = 1 Then
                cmbProvider.SelectedValue = -1
            End If
        End If
        'cmbProvider.Sorted = True
        'Dim i As Integer

        'For i = 0 To dt.Rows.Count - 1
        '    cmbProvider.Items.Add(New myList(dt.Rows.Item(i)(0), dt.Rows.Item(i)(1)))
        'Next

        'dt = PatientRegistration.FillControls("M") 'Pharamacy
        'cmbPharmacy.DataSource = dt
        'cmbPharmacy.DisplayMember = dt.Columns(1).ColumnName
        'cmbPharmacy.ValueMember = dt.Columns(0).ColumnName


        dt = PatientRegistration.FillControls("C") 'Category(Race)
        If Not IsNothing(dt) Then
            cmbRace.DataSource = dt
            cmbRace.DisplayMember = dt.Columns(1).ColumnName
            cmbRace.ValueMember = dt.Columns(0).ColumnName
            cmbRace.SelectedIndex = -1
        End If

        dt = PatientRegistration.FillControls("T")
        If Not IsNothing(dt) Then
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                cmbState.Items.Add(dt.Rows.Item(i)(0))
            Next

            ''''''''
            For i = 0 To dt.Rows.Count - 1
                'cmbState.Items.Add(dt.Rows.Item(i)(0))
                cmbState.Items.Add(dt.Rows.Item(i)(0))
            Next
            ''''''''''''
            'cmbState.DisplayMember = dt.Columns(1).ColumnName
            'cmbState.ValueMember = dt.Columns(0).ColumnName

            For i = 0 To dt.Rows.Count - 1
                cmbwState.Items.Add(dt.Rows.Item(i)(0))
            Next
            For i = 0 To dt.Rows.Count - 1
                cmbMother_State.Items.Add(dt.Rows.Item(i)(0))
                cmbFather_State.Items.Add(dt.Rows.Item(i)(0))
                cmbGuardian_State.Items.Add(dt.Rows.Item(i)(0))
            Next
        End If

        cmbDominance.Items.Add("Left Hand Dominant")
        cmbDominance.Items.Add("Right Hand Dominant")
        cmbDominance.Items.Add("Others")

        Fill_Location()

        'cmbwState.ValueMember = dt.Columns(0).ColumnName
    End Sub

    Private Sub FillTreeView()
        Try
            If ID <> 0 Then
                Dim count As Int16
                count = PatientRegistration.FetchPatientInsurance(ID)
                If count > 0 Then
                    Dim mychildnode As TreeNode
                    Dim i As Integer
                    Dim intkey As Long
                    Dim strname As String
                    For i = 0 To count - 1
                        Dim PatientInsurance As New ClsPatientInsurance
                        PatientRegistration.GetInsuranceCol(PatientInsurance, i)
                        intkey = PatientInsurance.InsuranceId
                        strname = PatientInsurance.InsuranceName
                        mychildnode = New TreeNode(strname)
                        If PatientInsurance.Primaryflag Then
                            mychildnode.ForeColor = Color.Blue
                            mychildnode.ImageIndex = 1
                            mychildnode.SelectedImageIndex = 1
                        Else
                            mychildnode.ImageIndex = 2
                            mychildnode.SelectedImageIndex = 2
                        End If
                        mynode.Nodes.Add(mychildnode)
                    Next
                    key = 0
                    mynode.ExpandAll()
                    'mychildnode = trInsurance.Nodes.Item(0).Nodes.Item(0)
                    'trInsurance.SelectedNode = mynode

                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillTreeViewforOMR()
        Try
            '     If ID <> 0 Then
            Dim mychildnode As TreeNode
            Dim i As Integer
            Dim intkey As Long
            Dim strname As String
            For i = 0 To ArrPatientInsurance.Count - 1
                Dim PatientInsurance As New ClsPatientInsurance
                PatientInsurance = ArrPatientInsurance.Item(i)
                'Insert in to patientinsurance collection
                PatientRegistration.PopulateArraylist(PatientInsurance)
                intkey = PatientInsurance.InsuranceId
                strname = PatientInsurance.InsuranceName
                mychildnode = New TreeNode(strname)
                If PatientInsurance.Primaryflag Then
                    mychildnode.ForeColor = Color.Blue
                    mychildnode.ImageIndex = 1
                    mychildnode.SelectedImageIndex = 1
                Else
                    mychildnode.ImageIndex = 2
                    mychildnode.SelectedImageIndex = 2
                End If
                mynode.Nodes.Add(mychildnode)
                PatientInsurance = Nothing
            Next
            key = 0
            mynode.ExpandAll()
            'mychildnode = trInsurance.Nodes.Item(0).Nodes.Item(0)
            'trInsurance.SelectedNode = mynode

            'End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function FindDuplicateReferrals(ByVal Id As Long) As Boolean
        Dim i As Integer
        For i = 0 To cmbReferrals.Items.Count - 1
            Dim objmylist As myList
            objmylist = (CType(cmbReferrals.Items.Item(i), myList))
            If Id = objmylist.Index Then
                Return False
                Exit Function
            End If
        Next
        Return True
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub HideColumns()
        Dim ts As New clsDataGridTableStyle(PatientRegistration.DsDataview.Table.TableName)
        'Dim ts As DataGridTableStyle = New DataGridTableStyle
        'ts.MappingName = PatientRegistration.DsDataview.Table.TableName
        ts.ReadOnly = True
        If intStatus = 2 Then
            ts.RowHeadersVisible = True
        Else
            ts.RowHeadersVisible = False
        End If
        'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
        'ts.HeaderBackColor = System.Drawing.Color.WhiteSmoke
        'ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        ' ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, TemplateGalleryNameCol, CategoryCol, ProviderCol, DescriptionCol})

        If intStatus = 1 Or intStatus = 3 Then
            Dim dgID As New DataGridTextBoxColumn

            With dgID
                .MappingName = PatientRegistration.DsDataview.Table.Columns(0).ColumnName
                .Alignment = HorizontalAlignment.Center
                .Width = 0
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol1 As New DataGridTextBoxColumn
            With dgCol1
                .MappingName = PatientRegistration.DsDataview.Table.Columns(1).ColumnName
                .HeaderText = "Name"
                .Width = dgCustomGrid.GridWith / 3
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol2 As New DataGridTextBoxColumn
            With dgCol2
                .MappingName = PatientRegistration.DsDataview.Table.Columns(2).ColumnName
                .HeaderText = "Street"
                .Width = dgCustomGrid.GridWith / 2.5
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol3 As New DataGridTextBoxColumn
            With dgCol3
                .MappingName = PatientRegistration.DsDataview.Table.Columns(3).ColumnName
                .HeaderText = "City"
                .Width = dgCustomGrid.GridWith / 8
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol4 As New DataGridTextBoxColumn
            With dgCol4
                .MappingName = PatientRegistration.DsDataview.Table.Columns(4).ColumnName
                .HeaderText = "State"
                .Width = dgCustomGrid.GridWith / 12
                .NullText = ""
                .ReadOnly = True
            End With
            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4})
        ElseIf intStatus = 2 Or intStatus = 4 Then
            Dim dgID As New DataGridTextBoxColumn

            With dgID
                .MappingName = PatientRegistration.DsDataview.Table.Columns(0).ColumnName
                .Alignment = HorizontalAlignment.Center
                .Width = 0
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol1 As New DataGridTextBoxColumn
            With dgCol1
                .MappingName = PatientRegistration.DsDataview.Table.Columns(1).ColumnName
                .HeaderText = "FirstName"
                .Width = dgCustomGrid.GridWith / 6
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol2 As New DataGridTextBoxColumn
            With dgCol2
                .MappingName = PatientRegistration.DsDataview.Table.Columns(2).ColumnName
                .HeaderText = "MiddleName"
                .Width = 0
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol3 As New DataGridTextBoxColumn
            With dgCol3
                .MappingName = PatientRegistration.DsDataview.Table.Columns(3).ColumnName
                .HeaderText = "LastName"
                .Width = dgCustomGrid.GridWith / 6
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol4 As New DataGridTextBoxColumn
            With dgCol4
                .MappingName = PatientRegistration.DsDataview.Table.Columns(4).ColumnName
                .HeaderText = "Street"
                .Width = dgCustomGrid.GridWith / 3
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol5 As New DataGridTextBoxColumn
            With dgCol5
                .MappingName = PatientRegistration.DsDataview.Table.Columns(5).ColumnName
                .HeaderText = "City"
                .Width = dgCustomGrid.GridWith / 6
                .NullText = ""
                .ReadOnly = True
            End With

            Dim dgCol6 As New DataGridTextBoxColumn
            With dgCol6
                .MappingName = PatientRegistration.DsDataview.Table.Columns(6).ColumnName
                .HeaderText = "State"
                .Width = dgCustomGrid.GridWith / 19
                .NullText = ""
                .ReadOnly = True
            End With
            'Dim dgCol5 As New DataGridBoolColumn
            'With dgCol5
            '    .HeaderText = "Select"
            '    .AllowNull = False
            '    .NullValue = False
            '    .NullText = False
            '    .TrueValue = True
            '    .FalseValue = False
            '    .ReadOnly = False
            '    .Alignment = HorizontalAlignment.Left
            '    .MappingName = "Select"
            '    .Width = 50
            'End With
            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4, dgCol5, dgCol6})
        End If
        'dgContactsList.TableStyles.Clear()
        dgCustomGrid.SetTableStyleCol(ts)
    End Sub

    Private Function IsDataChange() As Boolean
        Dim blnIsChange As Boolean
        With ArrPatientOriginalData
            If ArrPatientOriginalData.Count > 0 Then
                '.Add(Trim(txtPatientCode.Text))    '' 0  --NOT Used (Dont Update Change )
                If .Item(1).ToString = Trim(txtfname.Text) Then               '' 1 
                    .Item(1) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(2).ToString = Trim(txtmName.Text) Then               '' 2
                    .Item(2) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(3).ToString = Trim(txtlname.Text) Then               '' 3
                    .Item(3) = ""
                Else
                    blnIsChange = True
                End If

                'If .Item(4).ToString = CType(mskDOB.FormattedText, Date) Then '' 4
                '    .Item(4) = "" ' CType(mskDOB.FormattedText, Date)
                'Else
                '    blnIsChange = True
                'End If

                'If .Item(4).ToString = CType(mskDOB1.Text, Date) Then '' 4
                '    .Item(4) = "" ' CType(mskDOB.FormattedText, Date)
                'Else
                '    blnIsChange = True
                'End If

                If .Item(4).ToString = Trim(mskDOB1.Text) Then '' 4
                    .Item(4) = "" ' CType(mskDOB.FormattedText, Date)
                Else
                    blnIsChange = True
                End If

                If .Item(5).ToString = "Male" And rbGender1.Checked = True Then
                    .Item(5) = ""
                ElseIf .Item(5).ToString = "Female" And rbGender2.Checked = True Then
                    .Item(5) = ""
                ElseIf .Item(5).ToString = "Other" And rbGender3.Checked = True Then
                    .Item(5) = ""
                Else
                    blnIsChange = True
                End If

                ' If rbGender1.Checked = True Then
                '    .Add("Male")    '' 5
                ' ElseIf rbGender2.Checked = True Then
                '    .Add("Female")  '' 5
                ' ElseIf rbGender3.Checked = True Then
                '    .Add("Other")   '' 5
                ' End If
                ' If .Item(5).ToString = rbGender1.ToString Then
                '   Return blnIsChange = False
                ' End If

                '.Add(cmbMaritalstatus.Text)     '' 6 -- NOT Used (Dont Update Change)

                If .Item(7).ToString = Trim(txtAddress1.Text) Then '' 7
                    .Item(7) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(8).ToString = Trim(txtAddress2.Text) Then '' 8
                    .Item(8) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(9).ToString = Trim(txtCity.Text) Then   '' 9
                    .Item(9) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(10).ToString = Trim(cmbState.Text) Then  '' 10
                    .Item(10) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(11).ToString = (Trim(txtZip.Text)) Then    '' 11
                    .Item(11) = ""
                Else
                    blnIsChange = True
                End If

                If .Item(12).ToString = (Trim(txtCounty.Text)) Then '' 12
                    .Item(12) = ""
                Else
                    blnIsChange = True
                End If

                'modification on 20070522 by Bipin
                If .Item(13).ToString = (Trim(mskPhone1.Text)) Then '' 13
                    .Item(13) = ""
                Else
                    blnIsChange = True
                End If
                'If .Item(13).ToString = (Trim(mskPhone.ClipText)) Then '' 13
                '    .Item(13) = ""
                'Else
                '    blnIsChange = True
                'End If
            End If
        End With

        Return blnIsChange
    End Function

    Private Sub LoadGrid()
        Try
            'AddControl()
            'If Not IsNothing(dgCustomGrid) Then
            '    dgCustomGrid.Top = tbPatientRegistration.Top + GroupBox12.Top + cmbProvider.Top + cmbProvider.Height
            '    dgCustomGrid.Left = tbPatientRegistration.Left + GroupBox12.Left + 5
            '    dgCustomGrid.Height = GroupBox9.Height + GroupBox14.Height - 50
            '    dgCustomGrid.Visible = True
            '    dgCustomGrid.Width = GroupBox12.Width
            '    dgCustomGrid.BringToFront()
            '    dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            '    BindGrid()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub RefreshInsurance()
        'txtInsuranceName.Text = ""
        'cmbInsuranceNew.Text = ""
        txtInsuranceNew.Text = ""

        txtSubscriberID.Text = ""
        txtSubscriberName.Text = ""
        txtSubscriberPolicy.Text = ""
        txtEmployer.Text = ""

        ' modification on 20070522 by Bipin
        'mskIPhone.Mask = ""
        'mskIPhone.CtlText = ""
        'mskIPhone.Mask = "(###)-###-####"

        mskIPhone1.Mask = ""
        mskIPhone1.Text = ""
        mskIPhone1.Mask = "(###)-###-####"

        txtGroup.Text = ""
        chkPrimary.Checked = False
        chkSameasPatient.Checked = False
        '''''''''''''''''''''''''''''''''''''''''''''''' cmbPatientStatus.Checked = False

        ' modification on 20070522 by Bipin
        mskIDOB1.Mask = ""
        mskIDOB1.Text = ""
        mskIDOB1.Mask = "##/##/####"
        'mskIDOB1.Format = "MM/dd/yyyy"

        'mskIDOB.Mask = ""
        'mskIDOB.CtlText = ""
        'mskIDOB.Mask = "##/##/####"
        'mskIDOB.Format = "MM/dd/yyyy"
        btnAdd.Text = "Add"
        RemoveControl()
        'dtpIDOB.Checked = False
        'mskIDOB.CtlText = ""

        'trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    End Sub

    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            Me.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid = Nothing
        End If
    End Sub
#Region "Commented By Pramod for DMSV2"
    'Private Sub Set_ScanDocumentEvent(ByVal sender As Object, ByVal e As System.EventArgs, ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType, ByVal PatID As Int64)
    '    Dim oChk As New gloStream.gloDMS.DocumentCategory.DocumentCategory
    '    Dim oCat As New gloStream.gloDMS.DocumentCategory.Category
    '    oCat.Name = gDMSCategory_PatientDirective : oCat.IsDeleted = False
    '    If oChk.IsExists(oCat) = False Then
    '        MessageBox.Show("DMS Category not set for patient directive, please add document from Scan Document functionality")
    '        Exit Sub
    '    End If

    '    'Dim oImportDocumentEvent As New frmDMS_ScannedDocumentEvent ' ABBYY
    '    Dim oImportDocumentEvent As New frmDMS_PatientDirective
    '    Dim oDocument As New gloStream.gloDMS.Document.Document                             ' DMS Document
    '    Dim _PatientID As Long = 0                                                      ' Patient ID
    '    Dim _Category As String = ""                                                    ' Destination Category
    '    Dim _Month As String = ""                                                       ' Destination Month
    '    Dim _Year As String = ""
    '    Dim _DoucmentName As String = ""
    '    Dim _CurrentDocument As String = ""
    '    Dim _ImportDocument As String = ""
    '    Dim i As Integer = 0


    '    Try
    '        _PatientID = PatID 'CLng(txtPatientID.Text)
    '        _Month = MonthName(Month(Date.Now))
    '        _DoucmentName = ""
    '        If DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '            _Year = Trim(Year(Date.Now))
    '            _Category = gDMSCategory_PatientDirective
    '            _CurrentDocument = ""
    '        Else
    '            Exit Sub
    '        End If

    '        'Process
    '        If _PatientID > 0 Then
    '            'Check Month
    '            If Trim(_Month) = "" Then
    '                MessageBox.Show("Month not found to import document", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '                Exit Sub
    '            End If

    '            'Category
    '            If DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '                If Trim(_Category) = "" Then
    '                    MessageBox.Show("Category not selected to import document", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '                    Exit Sub
    '                End If
    '            End If

    '            'Set Values to Form Object
    '            With oImportDocumentEvent
    '                With .ProcessParameter
    '                    .PatientID = _PatientID
    '                    .Category = _Category
    '                    .Month = _Month
    '                    .Year = _Year
    '                    .DocumentName = _DoucmentName
    '                    .DocumentType = DocumentType
    '                End With

    '                'Show Form
    '                .Left = ((Me.Width - 492) / 2) + 100
    '                .Top = ((Me.Height - 284) / 2) + 150
    '                .ShowDialog()
    '                _ImportDocument = .sImportDocumentPath ' Document path which is imported

    '                'Audit Trial
    '                If File.Exists(_ImportDocument) = True Then
    '                    Dim objAudit As New clsAudit
    '                    Dim sMessage As String = ""
    '                    If DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '                        'sMessage = "Document Imported from file with the name " & _ImportDocument & " in " & _Category
    '                        sMessage = "Document Imported from file into Advance Directive."
    '                    ElseIf DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '                        '  sMessage = "Document Imported from file into General Bin with the name " & _ImportDocument
    '                        sMessage = "Document Imported from file into General Bin."
    '                    End If
    '                    objAudit.CreateLog(clsAudit.enmActivityType.PHIImport, sMessage, gstrLoginName, gstrClientMachineName, _PatientID)
    '                    objAudit = Nothing
    '                End If

    '                If File.Exists(_ImportDocument) = False Then
    '                    Dim oDB As New gloStream.gloDataBase.gloDataBase
    '                    oDB.Connect(GetConnectionString)
    '                    ''''"AND nPatientDirective <> 1",This is added in the query below to resolve the bug no-424 by Mahesh on 10302007
    '                    oDB.ExecuteNonSQLQuery("UPDATE Patient SET nPatientDirective = 0 WHERE nPatientID = " & PatID & " AND nPatientDirective <> 1")
    '                    oDB.Disconnect()
    '                End If

    '            End With ' With oPrintDeleteEvent.ProcessParameter

    '        End If ' If _PatientID > 0 Then

    '    Catch objError As Exception
    '        MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Exit Sub
    '    Finally
    '        oImportDocumentEvent = Nothing
    '        oDocument = Nothing
    '        _PatientID = Nothing
    '        _Category = Nothing
    '        _Month = Nothing
    '        _Year = Nothing
    '        _DoucmentName = Nothing
    '        _CurrentDocument = Nothing
    '        _ImportDocument = Nothing
    '    End Try
    'End Sub
#End Region

    Private Function Set_ScanDocumentEvent(ByVal PatientID As Int64, ByVal LabCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64) As Boolean
        'Dim oScanDocument As New gloEDocument.gloEDocumentManagement()
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try
            '_result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocument.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
            _result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oScanDocument.Dispose()
        End Try
        Return _result
    End Function
    Private Sub SetData(ByRef Arrlist As ArrayList)
        'Add Patient data for Insert
        Arrlist.Add(intId)
        Arrlist.Add(Trim(txtPatientCode.Text))
        Arrlist.Add(Trim(txtfname.Text))
        Arrlist.Add(Trim(txtmName.Text))
        Arrlist.Add(Trim(txtlname.Text))

        ' modification on 20070522 - Bipin

        Arrlist.Add(Trim(MskSSn1.Text))
        'Arrlist.Add(Trim(MskSSn.ClipText))
        'Arrlist.Add(mskDOB.CtlText)

        ' modification on 20070522 - Bipin
        'Arrlist.Add(CType(mskDOB.FormattedText, Date))  
      
        Arrlist.Add(CType(mskDOB1.Text, Date))



        'Format(mskDOB1.Text, "MM/dd/yyyy")
        'Arrlist.Add(Trim(mskDOB1.Text))

        If rbGender1.Checked = True Then
            Arrlist.Add("Male")
        ElseIf rbGender2.Checked = True Then
            Arrlist.Add("Female")
        ElseIf rbGender3.Checked = True Then
            Arrlist.Add("Other")
        End If

        Arrlist.Add(cmbMaritalstatus.Text)
        Arrlist.Add(Trim(txtAddress1.Text))
        Arrlist.Add(Trim(txtAddress2.Text))
        Arrlist.Add(Trim(txtCity.Text))
        Arrlist.Add(Trim(cmbState.Text))
        Arrlist.Add(Trim(txtZip.Text))

        ' modification on 20070522 - Bipin
        'Arrlist.Add(Trim(mskPhone.ClipText))
        'Arrlist.Add(Trim(mskMobile.ClipText))
        Arrlist.Add(Trim(mskPhone1.Text))
        Arrlist.Add(Trim(mskMobile1.Text))

        Arrlist.Add(Trim(txtemail.Text))
        Arrlist.Add(Trim(txtfax.Text))
        Arrlist.Add(Trim(txtOccupation.Text))
        Arrlist.Add(Trim(cmbEmploymentStatus.Text))
        Arrlist.Add(Trim(txtWLocation.Text))
        Arrlist.Add(Trim(txtwAddress1.Text))
        Arrlist.Add(Trim(txtwAddress2.Text))
        Arrlist.Add(Trim(txtwCity.Text))
        Arrlist.Add(Trim(cmbwState.Text))
        Arrlist.Add(Trim(txtwZip.Text))

        ' modification on 20070522 - Bipin
        'Arrlist.Add(Trim(mskwPhone.ClipText))
        Arrlist.Add(Trim(mskwPhone1.Text))
        Arrlist.Add(Trim(txtwFax.Text))
        Arrlist.Add(Trim(txtInsuranceNotes.Text))
        'Arrlist.Add(dtpRegistrationdate.Value)

        Arrlist.Add(Trim(txtpcp.Tag))
        Arrlist.Add(Trim(txtGuarantor.Text))
        Arrlist.Add(Trim(txtspousename.Text))

        'Arrlist.Add(Trim(mskspousePhone.ClipText))
        Arrlist.Add(Trim(mskspousePhone1.Text))

        Arrlist.Add(cmbRace.Text)
        Arrlist.Add(cmbPatientStatus.Text)

        'If Len(mskDOB.ClipText) > 0 Then
        '    Arrlist.Add(True)
        'Else
        '    Arrlist.Add(False)
        'End If
        If Len(mskDOB1.Text) > 0 Then
            Arrlist.Add(True)
        Else
            Arrlist.Add(False)
        End If
        'code commented as provider id is now compulsory
        'If cmbProvider.Text <> "" Then
        '    Dim objmylist As myList
        '    objmylist = cmbProvider.SelectedItem
        '    Arrlist.Add(objmylist.Index)
        'Else
        '    Arrlist.Add(0)
        'End If
        'code commented as provider id is now compulsory

        Arrlist.Add(cmbProvider.SelectedValue)
        Arrlist.Add(txtPharmacy.Tag)
        Arrlist.Add(Trim(txtCounty.Text))
        'Arrlist.Add(cmbReferral.SelectedValue)
        'Arrlist.Add(cmbPharmacy.SelectedValue)
        'Arrlist.Add(cmbInsuranceID.SelectedValue)

        ' modification on 20070522 by Bipin
        If Len(Trim(mskRegistrationdate1.Text)) < 5 Then
            'code commented by sarika 31st oct 07
            'Arrlist.Add(Now)
            'code added by sarika 31st oct 07

            Arrlist.Add(Format(Now.Date, "MM/dd/yyyy"))

            Arrlist.Add(False)
            'If Len(mskRegistrationdate.ClipText) = 0 Then
            '    Arrlist.Add(Now)
            '    Arrlist.Add(False)
        Else
            'Arrlist.Add(mskRegistrationdate.CtlText)

            ' modification on 20070522 by Bipin
            Arrlist.Add(Format(CType(mskRegistrationdate1.Text, Date), "MM/dd/yyyy"))
            Arrlist.Add(True)
            'Arrlist.Add(CType(mskRegistrationdate1.Text, Date))
            'Arrlist.Add(True)
        End If

        ' modification on 20070522 by Bipin ' default test is "/_ _/" 4 elements default present 
        If Len(Trim(mskInjurydate1.Text)) < 5 Then
            'sarika 31st oct 07
            'Arrlist.Add(Now)
            Arrlist.Add(Format(Now.Date, "MM/dd/yyyy"))
            Arrlist.Add(False)
        Else
            'Arrlist.Add(mskRegistrationdate.CtlText)
            Arrlist.Add(Format(CType(mskInjurydate1.Text, Date), "MM/dd/yyyy"))
            Arrlist.Add(True)
        End If
        'If Len(mskInjurydate.ClipText) = 0 Then
        '    Arrlist.Add(Now)
        '    Arrlist.Add(False)
        'Else
        '    'Arrlist.Add(mskRegistrationdate.CtlText)
        '    Arrlist.Add(CType(mskInjurydate.FormattedText, Date))
        '    Arrlist.Add(True)
        'End If

        ' modification on 20070522 by Bipin ' default test is "/_ _/" 4 elements default present 
        If Len(Trim(mskSurgeryDate1.Text)) < 5 Then
            'sarika 31st oct 07
            'Arrlist.Add(Now)
            Arrlist.Add(Format(Now.Date, "MM/dd/yyyy"))
            Arrlist.Add(False)
        Else
            'Arrlist.Add(mskRegistrationdate.CtlText)
            Arrlist.Add(Format(CType(mskSurgeryDate1.Text, Date), "MM/dd/yyyy"))
            Arrlist.Add(True)
        End If

        'If Len(mskSurgeryDate.ClipText) = 0 Then
        '    Arrlist.Add(Now)
        '    Arrlist.Add(False)
        'Else
        '    'Arrlist.Add(mskRegistrationdate.CtlText)
        '    Arrlist.Add(CType(mskSurgeryDate.FormattedText, Date))
        '    Arrlist.Add(True)
        'End If
        Arrlist.Add(cmbDominance.Text)
        '''' Location
        Arrlist.Add(cmbLocation.Text)
        ''''
        Arrlist.Add(picFinalPatient.Image)

        '''' Code updation done by Ravikiran on 27/01/2007

        ''''Adding Mother Details

        ''''index starts at 48 - 86
        Arrlist.Add(Trim(txtMother_fName.Text))
        Arrlist.Add(Trim(txtMother_mName.Text))
        Arrlist.Add(Trim(txtMother_lName.Text))
        Arrlist.Add(Trim(txtMother_Address1.Text))
        Arrlist.Add(Trim(txtMother_Address2.Text))
        Arrlist.Add(Trim(txtMother_City.Text))
        Arrlist.Add(Trim(cmbMother_State.Text))
        Arrlist.Add(Trim(txtMother_Zip.Text))
        Arrlist.Add(Trim(txtMother_County.Text))

        ' modification on 20070522 by Bipin
        Arrlist.Add(Trim(mskMother_Phone1.Text))
        Arrlist.Add(Trim(mskMother_Mobile1.Text))
        'Arrlist.Add(Trim(mskMother_Phone.ClipText))
        'Arrlist.Add(Trim(mskMother_Mobile.ClipText))

        Arrlist.Add(Trim(txtMother_Fax.Text))
        Arrlist.Add(Trim(txtMother_Email.Text))

        ''''adding Father Details

        Arrlist.Add(Trim(txtFather_fName.Text))
        Arrlist.Add(Trim(txtFather_mName.Text))
        Arrlist.Add(Trim(txtFather_lName.Text))
        Arrlist.Add(Trim(txtFather_Address1.Text))
        Arrlist.Add(Trim(txtFather_Address2.Text))
        Arrlist.Add(Trim(txtFather_City.Text))
        Arrlist.Add(Trim(cmbFather_State.Text))
        Arrlist.Add(Trim(txtFather_Zip.Text))
        Arrlist.Add(Trim(txtFather_County.Text))

        ' modification on 20070522 by Bipin
        Arrlist.Add(Trim(mskFather_Phone1.Text))
        'Arrlist.Add(Trim(mskFather_Phone.ClipText))

        'Arrlist.Add(Trim(mskFather_Mobile.ClipText))
        Arrlist.Add(Trim(mskFather_Mobile1.Text))

        Arrlist.Add(Trim(txtFather_Fax.Text))
        Arrlist.Add(Trim(txtFather_Email.Text))

        ''''Adding Guardian Details

        Arrlist.Add(Trim(txtGuardian_fName.Text))
        Arrlist.Add(Trim(txtGuardian_mName.Text))
        Arrlist.Add(Trim(txtGuardian_lName.Text))
        Arrlist.Add(Trim(txtGuardian_Address1.Text))
        Arrlist.Add(Trim(txtGuardian_Address2.Text))
        Arrlist.Add(Trim(txtGuardian_City.Text))
        Arrlist.Add(Trim(cmbGuardian_State.Text))
        Arrlist.Add(Trim(txtGuardian_Zip.Text))
        Arrlist.Add(Trim(txtGuardian_County.Text))

        ' modification on 20070522 by Bipin
        Arrlist.Add(Trim(mskGuardian_Phone1.Text))
        Arrlist.Add(Trim(mskGuardian_Mobile1.Text))
        'Arrlist.Add(Trim(mskGuardian_Phone.ClipText))
        'Arrlist.Add(Trim(mskGuardian_Mobile.ClipText))

        Arrlist.Add(Trim(txtGuardian_Fax.Text))
        Arrlist.Add(Trim(txtGuardian_Email.Text))

        ''''
        'Arrlist.Add(Trim(mskIPhone1.Text))

        If chkDirective.Checked = False Then
            Arrlist.Add(0)
        Else
            Arrlist.Add(1)
        End If

        If chkExemptFromRpt.Checked = False Then
            Arrlist.Add(0)
        Else
            Arrlist.Add(1)
        End If


        'sarika Workers Comp 7th May 08
        If chkWorkersComp.Checked = False Then
            Arrlist.Add(0)
        Else
            Arrlist.Add(1)
        End If


        If txtWorkersCompClaimNo.Text = "" Then
            Arrlist.Add("")
        Else
            Arrlist.Add(txtWorkersCompClaimNo.Text.Trim())
        End If
        '----------sarika Workers Comp 7th May 08


        'sarika Auto 7th Msy 08
        If chkAuto.Checked = False Then
            Arrlist.Add(0)
        Else
            Arrlist.Add(1)
        End If


        If txtAutoClaimNo.Text = "" Then
            Arrlist.Add("")
        Else
            Arrlist.Add(txtAutoClaimNo.Text.Trim())
        End If
        '----------sarika Auto 7th May 08




        ' add data to temp list for verification purpose whether user modify patient information or not
        ' Added on 20070716 - Bipin
        _arraySetData = Arrlist


    End Sub


    'Private Sub SetGridValues(Optional ByVal dblstatus As Int16 = 0)
    '    Try
    '        If dgCustomGrid.GetCurrentrowIndex >= 0 Then
    '            If intStatus = 1 Then
    '                txtPharmacy.Text = CType(dgCustomGrid.CurrentName, System.String)
    '                txtPharmacy.Tag = CType(dgCustomGrid.CurrentID, Long)
    '                btnPharmacy.Focus()
    '            ElseIf intStatus = 2 Then
    '                'cmbReferrals.Items.Clear()
    '                'Dim i As Integer
    '                'For i = 0 To dgCustomGrid.GetVisiblerowcount - 2
    '                '    If Not IsDBNull(dgCustomGrid.GetItem(i, 5)) Then
    '                '        If CType(dgCustomGrid.GetItem(i, 5), System.Boolean) = True Then
    '                '            'If cmbReferrals.FindStringExact(CType(dgCustomGrid.CurrentName, System.String)) < 0 Then
    '                '            cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 0), System.Int64), CType(dgCustomGrid.GetItem(i, 1), System.String)))
    '                '            'End If
    '                '        End If
    '                '    End If
    '                'Next
    '                If dblstatus = 0 Then
    '                    Dim i As Integer
    '                    Dim count As Integer

    '                    For i = 0 To ReferralCount - 1
    '                        If dgCustomGrid.GetIsSelected(i) Then

    '                            'If cmbReferrals.FindStringExact(CType(dgCustomGrid.GetItem(i, 1), System.String)) < 0 Then

    '                            If FindDuplicateReferrals(CType(dgCustomGrid.GetItem(i, 0), Long)) Then
    '                                Dim strname As String
    '                                strname = CType(dgCustomGrid.GetItem(i, 1), System.String) & " " & CType(dgCustomGrid.GetItem(i, 2), System.String) & " " & CType(dgCustomGrid.GetItem(i, 3), System.String)
    '                                cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 0), Long), strname))
    '                                cmbReferrals.Text = strname
    '                            End If
    '                        End If

    '                    Next
    '                ElseIf dblstatus = 1 Then
    '                    'If cmbReferrals.FindStringExact(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 1), System.String)) < 0 Then
    '                    If FindDuplicateReferrals(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 0), Long)) Then
    '                        Dim strname As String
    '                        strname = CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 1), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 3), System.String)
    '                        cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 0), Long), strname))
    '                        cmbReferrals.Text = strname
    '                    End If
    '                End If
    '                btnReferrals.Focus()
    '            ElseIf intStatus = 3 Then
    '                txtInsuranceName.Text = CType(dgCustomGrid.CurrentName, System.String)
    '                txtInsuranceName.Tag = CType(dgCustomGrid.CurrentID, Long)
    '                btnInsurance.Focus()
    '            ElseIf intStatus = 4 Then
    '                Dim strname As String
    '                strname = CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 1), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 3), System.String)

    '                txtpcp.Text = strname
    '                txtpcp.Tag = CType(dgCustomGrid.CurrentID, Long)
    '                btnpcp.Focus()
    '            End If
    '        End If
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        dgCustomGrid.Visible = False
    '    End Try
    'End Sub


    '' To Set/Keep the Original Data (Data Before Modify) of Patient

    Private Sub SetGridValues_c1UC(Optional ByVal dblstatus As Int16 = 0)
        Try
            If dt.Rows.Count > 0 Then
                If intStatus = 1 Then
                    Dim tempName As System.String
                    ' modified on  20070615 bipin
                    If oC1flex._UCflex.Row > 0 Then
                        tempName = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String)
                        txtPharmacy.Text = tempName
                        txtPharmacy.Tag = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64)
                        'txtPharmacy.Text = CType(dgCustomGrid.CurrentName, System.String)
                        'txtPharmacy.Tag = CType(dgCustomGrid.CurrentID, Long)
                        btnPharmacyBrowse.Focus()
                    End If
                ElseIf intStatus = 2 Then
                    ' code commented becoz previous logic was for multi select datagrid
                    ' current logic is depend on c1flexgrid's 'select' multiple comlumns 

                    'cmbReferrals.Items.Clear()
                    'Dim i As Integer
                    'For i = 0 To dgCustomGrid.GetVisiblerowcount - 2
                    '    If Not IsDBNull(dgCustomGrid.GetItem(i, 5)) Then
                    '        If CType(dgCustomGrid.GetItem(i, 5), System.Boolean) = True Then
                    '            'If cmbReferrals.FindStringExact(CType(dgCustomGrid.CurrentName, System.String)) < 0 Then
                    '            cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 0), System.Int64), CType(dgCustomGrid.GetItem(i, 1), System.String)))
                    '            'End If
                    '        End If
                    '    End If
                    'Next
                    'MessageBox.Show("Under construction...")

                    Dim i As Integer
                    Dim count As Integer

                    'For i = 0 To dv.count - 1 '  ReferralCount - 1
                    '    'If FindDuplicateReferrals(CType(dgCustomGrid.GetItem(i, 0), Long)) Then
                    '    Dim strname As String
                    '    'strname = CType(dgCustomGrid.GetItem(i, 1), System.String) & " " & CType(dgCustomGrid.GetItem(i, 2), System.String) & " " & CType(dgCustomGrid.GetItem(i, 3), System.String)
                    '    strname = d
                    '    cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 0), Long), strname))
                    '    cmbReferrals.Text = strname
                    '    'End If
                    'Next

                    'If dblstatus = 0 Then
                    '    'Dim i As Integer
                    '    'Dim count As Integer

                    '    For i = 0 To ReferralCount - 1
                    '        If dgCustomGrid.GetIsSelected(i) Then

                    '            'If cmbReferrals.FindStringExact(CType(dgCustomGrid.GetItem(i, 1), System.String)) < 0 Then

                    '            If FindDuplicateReferrals(CType(dgCustomGrid.GetItem(i, 0), Long)) Then
                    '                Dim strname As String
                    '                strname = CType(dgCustomGrid.GetItem(i, 1), System.String) & " " & CType(dgCustomGrid.GetItem(i, 2), System.String) & " " & CType(dgCustomGrid.GetItem(i, 3), System.String)
                    '                cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 0), Long), strname))
                    '                cmbReferrals.Text = strname
                    '            End If
                    '        End If

                    '    Next
                    'ElseIf dblstatus = 1 Then
                    '    'If cmbReferrals.FindStringExact(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 1), System.String)) < 0 Then
                    '    'If FindDuplicateReferrals(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 0), Long)) Then
                    '    '    Dim strname As String
                    '    '    strname = CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 1), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 3), System.String)
                    '    '    cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 0), Long), strname))
                    '    '    cmbReferrals.Text = strname
                    '    'End If
                    '    Dim strname As String
                    '    strname = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String) & " " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 2), System.String) & " " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 3), System.String)
                    '    cmbReferrals.Items.Add(New myList(CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64), strname))
                    '    cmbReferrals.Text = strname
                    'End If
                    btnReferralsBrowse.Focus()
                ElseIf intStatus = 3 Then

                    Dim tempName As System.String
                    'If oC1flex._UCflex.Row > 0 Then
                    '    tempName = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String)
                    '    cmbInsuranceNew.Text = tempName
                    '    cmbInsuranceNew.Tag = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64)
                    '    btnInsurance.Focus()
                    'End If
                    'txtInsuranceName.Text = CType(dgCustomGrid.CurrentName, System.String)
                    'txtInsuranceName.Tag = CType(dgCustomGrid.CurrentID, Long)
                    'btnInsurance.Focus()


                    'sarika 17th aug 07
                    If oC1flex._UCflex.Row > 0 Then
                        tempName = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String)
                        'cmbInsuranceNew.Text = tempName
                        'cmbInsuranceNew.Tag = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64)
                        'btnInsurance.Focus()

                        txtInsuranceNew.Text = tempName
                        'CType(dgCustomGrid.CurrentName, System.String)
                        txtInsuranceNew.Tag = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 0), System.Int64)
                        Dim insId As Int64 = Convert.ToInt64(txtInsuranceNew.Tag)
                        'sarika 3rd oct 07
                        Dim insPhone As String = ""
                        insPhone = GetInsPhone(insId)

                        If insPhone <> "" And Len(insPhone) > 9 Then
                            mskIPhone1.Text = insPhone
                        End If
                        '----------------------------------------------------------

                        'CType(dgCustomGrid.CurrentID, Long)
                        btnInsurance.Focus()
                    End If
                ElseIf intStatus = 4 Then
                    'Dim strname As String
                    'strname = CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 1), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2), System.String) & " " & CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 3), System.String)

                    'txtpcp.Text = strname
                    'txtpcp.Tag = CType(dgCustomGrid.CurrentID, Long)
                    'btnpcp.Focus()

                    'CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String)
                    If oC1flex._UCflex.Row > 0 Then
                        Dim strname As String
                        '''Added by Anil on 20071126
                        If Not CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 4), System.String) = "" Then
                            strname = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String) & " " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 2), System.String) & " " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 3), System.String) & ", " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 4), System.String)
                            '''
                        Else
                            strname = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 1), System.String) & " " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 2), System.String) & " " & CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, 3), System.String)
                        End If
                        txtpcp.Text = strname
                        txtpcp.Tag = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64)
                        btnPCPBrowse.Focus()
                    End If
                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'dgCustomGrid.Visible = False
            pnl.Visible = False
            Call visibleBtns(True)
        End Try
    End Sub

    Private Function GetInsPhone(ByVal InsuranceID As Int64) As String
        Dim InsPhone As String = ""
        Dim objClsPatientRegDBLayer As New ClsPatientRegistrationDBLayer
        Try
            InsPhone = objClsPatientRegDBLayer.GetInsurancePhone(InsuranceID)

            Return InsPhone
        Catch ex As Exception
            MessageBox.Show("Error retreiving Insurance Phone information." & ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Private Sub SetPatientOriginalData()
        ArrPatientOriginalData.Clear()
        With ArrPatientOriginalData
            .Add(Trim(txtPatientCode.Text))    '' 0  --NOT Used (Dont Update Change )
            .Add(Trim(txtfname.Text))    '' 1 
            .Add(Trim(txtmName.Text))    '' 2
            .Add(Trim(txtlname.Text))    '' 3

            ' modification on 20070522 by Bipin 
            '.Add(CType(mskDOB.FormattedText, Date))    '' 4
            .Add(Trim(mskDOB1.Text))

            If rbGender1.Checked = True Then
                .Add("Male")    '' 5
            ElseIf rbGender2.Checked = True Then
                .Add("Female")  '' 5
            ElseIf rbGender3.Checked = True Then
                .Add("Other")   '' 5
            End If
            .Add(cmbMaritalstatus.Text)     '' 6 -- NOT Used (Dont Update Change)
            .Add(Trim(txtAddress1.Text))    '' 7
            .Add(Trim(txtAddress2.Text))    '' 8
            .Add(Trim(txtCity.Text))        '' 9
            .Add(Trim(cmbState.Text))       '' 10
            .Add(Trim(txtZip.Text))         '' 11
            .Add(Trim(txtCounty.Text))      '' 12
            .Add(Trim(mskPhone1.Text))   '' 13
        End With
    End Sub


    Private Sub setReferrals(ByRef arrlist As ArrayList)
        Dim i As Integer
        If Not IsNothing(arrlist) Then
            For i = 0 To arrlist.Count - 1
                cmbReferrals.Items.Add(arrlist.Item(i))
                If i = 0 Then
                    Dim objmylist As myList
                    objmylist = (CType(arrlist.Item(0), myList))
                    cmbReferrals.Text = objmylist.Description
                End If
            Next
        End If
    End Sub


    Private Function splittext(ByVal str As String, ByVal splitflag As Boolean) As String
        Dim str1 As String
        If Trim(str) <> "" Then
            If splitflag Then
                Return Mid(str, 1, 3) & "/" & Mid(str, 4, 2) & "/" & Mid(str, 6, 4)
            Else
                'MsgBox("(" & Mid(str, 1, 3) & ")" & "-" & Mid(str, 4, 3) & "-" & Mid(str, 7, 4))
                str = Replace(str, "-", "")
                str = Replace(str, "(", "")
                str = Replace(str, ")", "")
                str = Replace(str, "/", "")
                str = Replace(str, "\", "")
                str = Replace(str, " ", "")
                str = Replace(str, "x", "")
                str = Replace(str, "X", "")
                If Trim(str) <> "" Then
                    If IsNumeric(str) Then
                        If Len(str) = 10 Then
                            Return "(" & Mid(str, 1, 3) & ")" & "-" & Mid(str, 4, 3) & "-" & Mid(str, 7, 4)
                        Else
                            Return ""
                        End If
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            End If
        End If
    End Function

    'modified on 20070524 - Bipin 
    Private Sub btnClosePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'pnl.Visible = False
        'btnClosePanel.Visible = False
    End Sub

    Private Sub oC1flex__FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal selectflag As Boolean) Handles oC1flex._FlexDoubleClick
        'SetGridValues(1)
        SetGridValues_c1UC(1)
        oC1flex_btnUC_OKclick(sender, e)
    End Sub

    Private Sub oC1flex_btnUC_ADDclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_ADDclick
        'code commented by sarika 3rd oct 07
        'dgCustomGrid_AddClick(sender, e)
        'code added by sarika 3rd oct 07
        Try
            Dim frm As frmContactMst
            Dim colno As Integer

            If intStatus = 3 Then
                frm = New frmContactMst(False, "Insurance")
                frm.Text = "Add Contacts for Insurance"
                colno = 1
                frm.FillControls()
            ElseIf intStatus = 1 Then
                frm = New frmContactMst(False, "Pharmacy")
                frm.Text = "Add Contacts for Pharmacy"
                colno = 1
                frm.FillControls()
            ElseIf intStatus = 2 Then
                frm = New frmContactMst(True, "Physician")
                frm.Text = "Add Contacts for Physician"
                colno = 2
                frm.FillControls()
            ElseIf intStatus = 4 Then
                frm = New frmContactMst(True, "Physician")
                frm.Text = "Add Contacts for Referrals"
                colno = 1
                frm.FillControls()
            End If

            'Dim frm As ContactMaster
            'If intStatus = 3 Then
            '    frm = New ContactMaster(False, "Insurance")
            '    frm.Text = "Add Contacts for Insurance"
            'ElseIf intStatus = 1 Then
            '    frm = New ContactMaster(False, "Pharmacy")
            '    frm.Text = "Add Contacts for Pharmacy"
            'ElseIf intStatus = 2 Then
            '    frm = New ContactMaster(True, "Physician")
            '    frm.Text = "Add Contacts for Physician"
            'ElseIf intStatus = 4 Then
            '    frm = New ContactMaster(True, "Physician")
            '    frm.Text = "Add Contacts for Referrals"
            'End If

            frm.ShowDialog()

            ' ButtonX14_Click(sender, e)
            ' modify cod eon 20070613 to refresh the c1Grid
            Call loadC1flexgrid()

            ' default selection of new added row
            Dim searchdt = frm.ContactID

            If searchdt = 0 Then
                oC1flex._UCflex.Select(1, 1)
                Exit Sub
            End If


            'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            'c1Uc.SortDataview(searchdt)
            Dim searchrow As Integer = 0
            If oC1flex._bSelectFlag = False Then
                searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 0, False, True, False)

            Else
                searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, False, True, False)
            End If

            oC1flex._UCflex.Select(searchrow, 1)

            'code commented on 20070524 Bipin
            'BindGrid()
            colno = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '-----------------------------------------------------
    End Sub

    Private Sub oC1flex_btnUC_Cancelclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_Cancelclick
        dgCustomGrid_CloseClick(sender, e)
    End Sub

    'code added by sarika 05/10/2007
    Private Sub oC1flex_btnUC_Modify_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_Modify_click
        Dim ID As Int64

        Try

            If oC1flex._UCflex.Row > 0 Then
                ID = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64)
                'txtPharmacy.Text = CType(dgCustomGrid.CurrentName, System.String)
                'txtPharmacy.Tag = CType(dgCustomGrid.CurrentID, Long)
                btnPharmacyBrowse.Focus()
                'Else
                '    oC1flex_btnUC_ADDclick(sender, e)
                '    Exit Sub
            End If

            Dim frm As frmContactMst

            If intStatus = 1 Then
                frm = New frmContactMst(False, ID, "Pharmacy")
                frm.Text = "Add Contacts for Pharmacy"
                frm.FillControls()
            ElseIf intStatus = 2 Then
                frm = New frmContactMst(True, ID, "Physician")
                frm.Text = "Add Contacts for Physician"
                frm.FillControls()
            ElseIf intStatus = 3 Then
                frm = New frmContactMst(False, ID, "Insurance")
                frm.Text = "Add Contacts for Insurance"
                frm.FillControls()
            ElseIf intStatus = 4 Then
                frm = New frmContactMst(True, ID, "Physician")
                frm.Text = "Add Contacts for Referrals"
                frm.FillControls()
            End If

            'Dim frm As ContactMaster
            'If intStatus = 3 Then
            '    frm = New ContactMaster(False, "Insurance")
            '    frm.Text = "Add Contacts for Insurance"
            'ElseIf intStatus = 1 Then
            '    frm = New ContactMaster(False, "Pharmacy")
            '    frm.Text = "Add Contacts for Pharmacy"
            'ElseIf intStatus = 2 Then
            '    frm = New ContactMaster(True, "Physician")
            '    frm.Text = "Add Contacts for Physician"
            'ElseIf intStatus = 4 Then
            '    frm = New ContactMaster(True, "Physician")
            '    frm.Text = "Add Contacts for Referrals"
            'End If

            frm.ShowDialog()

            ' ButtonX14_Click(sender, e)
            ' modify cod eon 20070613 to refresh the c1Grid
            Call loadC1flexgrid()

            ' default selection of new added row
            'Dim searchdt = frm.strData

            'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            'c1Uc.SortDataview(searchdt)
            'Dim searchrow As String = ""
            'If intStatus = 1 Then
            '    'Pharmacy 
            '    searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
            'ElseIf intStatus = 2 Then
            '    'Physician
            '    searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 2, True)
            'ElseIf intStatus = 3 Then
            '    'Insurance
            '    searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
            'ElseIf intStatus = 4 Then
            '    'Physician ---referrals
            '    searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
            'End If

            Dim searchdt = ID

            'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            'c1Uc.SortDataview(searchdt)
            Dim searchrow As Integer = 0
            If oC1flex._bSelectFlag = False Then
                searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 0, False, True, False)

            Else
                searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, False, True, False)
            End If

            oC1flex._UCflex.Select(searchrow, 1)


            'code commented on 20070524 Bipin
            'BindGrid()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '-----------------------------------------------------

    Private Sub oC1flex_btnUC_OKclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles oC1flex.btnUC_OKclick
        Try
            pnl.Visible = False
            Call visibleBtns(True)

            If IsNothing(dt) = False Then
                Dim dv As DataView = oC1flex.SetRowfilter

                '                If IsNothing(dv) = False Then
                Dim j As Integer
                Dim strname As String
                InsuranceIDs = New Collection

                ''''''''' code modified on 20070615
                'If dv.Count = 0 Then
                '    MessageBox.Show("Please select atleast one Physician ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Try
                'End If
                '''''''''''''

                ' code modified on 20070605 by bipin
                If IsNothing(dv) = False Then
                    'If dv.Count > 0 Then
                    For j = 0 To dv.Count - 1
                        'cmbReferrals.Items.Add(New myList(CType(dv.Item(j)(0), String), strname))
                        If intStatus = 2 Then
                            '' Add Referrals 
                            '''---Added by Anil on 20071126
                            If Not CType(dv.Item(j)("Degree"), System.String) = "" Then
                                strname = CType(dv.Item(j)("FirstName"), System.String) & " " & CType(dv.Item(j)("MiddleName"), System.String) & " " & CType(dv.Item(j)("LastName"), System.String) & ", " & CType(dv.Item(j)("Degree"), System.String)
                            Else
                                strname = CType(dv.Item(j)("FirstName"), System.String) & " " & CType(dv.Item(j)("MiddleName"), System.String) & " " & CType(dv.Item(j)("LastName"), System.String)
                            End If
                            '''---

                            cmbReferrals.Items.Add(New myList(dv.Item(j)("nContactId"), strname))
                            cmbReferrals.Text = strname
                        ElseIf intStatus = 3 Then
                            'code commented by sarika 17th aug 07

                            'strname = CType(dv.Item(j)(1), System.String) '& " " & CType(dv.Item(j)(2), System.String) & " " & CType(dv.Item(j)(3), System.String)
                            'cmbInsuranceNew.Items.Add(New myList(dv.Item(j)(0), strname))
                            'cmbInsuranceNew.Text = strname
                            'InsuranceIDs.Add(dv.Item(j)(0))

                            'sarika 17th aug 07

                        End If
                    Next
                    'Else
                    '    MessageBox.Show("Please select atleast one Physician ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Call loadC1flexgrid()
                    '    Exit Sub
                End If

                'If IsNothing(dv) = False Then
                '    For j = 0 To dv.Count - 1
                '        'cmbReferrals.Items.Add(New myList(CType(dv.Item(j)(0), String), strname))
                '        strname = CType(dv.Item(j)(1), System.String) & " " & CType(dv.Item(j)(2), System.String) & " " & CType(dv.Item(j)(3), System.String)
                '        cmbReferrals.Items.Add(New myList(dv.Item(j)(0), strname))
                '        cmbReferrals.Text = strname
                '    Next
                'End If
            End If
            'Dim strname As String
            'strname = CType(dgCustomGrid.GetItem(i, 1), System.String) & " " & CType(dgCustomGrid.GetItem(i, 2), System.String) & " " & CType(dgCustomGrid.GetItem(i, 3), System.String)
            'cmbReferrals.Items.Add(New myList(CType(dgCustomGrid.GetItem(i, 0), Long), strname))
            'cmbReferrals.Text = strname

            'SetGridValues_c1UC()
            'btnClosePanel.Visible = False
            'dgCustomGrid_OKClick(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub DrawTab(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs, ByVal FocusedBackColor As Color, ByVal FocusedForeColor As Color, ByVal NonFocusedBackColor As Color, ByVal NonFocusedForeColor As Color)
        ''tbPatientRegistration.DrawMode = TabDrawMode.Normal
        'Dim f As Font
        'Dim backBrush As Brush
        'Dim foreBrush As Brush
        'Dim sf As New StringFormat()

        'If e.Index = tbPatientRegistration.SelectedIndex Then
        '    f = New Font(e.Font, FontStyle.Regular)
        '    backBrush = New System.Drawing.SolidBrush(Color.LightSkyBlue)
        '    foreBrush = New System.Drawing.SolidBrush(Color.Black)

        '    tbPatientRegistration.TabPages(e.Index).BackColor = Color.GhostWhite
        '    Dim tabName As String = tbPatientRegistration.TabPages(e.Index).Text

        '    Dim rect As New Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height)
        '    sf.Alignment = StringAlignment.Center
        '    e.Graphics.FillRectangle(backBrush, rect)
        '    Dim r As RectangleF = New RectangleF(e.Bounds.X + 1, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4)
        '    e.Graphics.DrawString(tabName, f, foreBrush, r, sf)
        'Else
        '    f = New Font(e.Font, FontStyle.Regular)
        '    backBrush = New System.Drawing.SolidBrush(Color.DarkBlue)
        '    foreBrush = New System.Drawing.SolidBrush(Color.Black)

        '    tbPatientRegistration.TabPages(e.Index).BackColor = Color.GhostWhite
        '    Dim tabName As String = tbPatientRegistration.TabPages(e.Index).Text

        '    Dim rect As New Rectangle(e.Bounds.X + 1, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height + 1)
        '    sf.Alignment = StringAlignment.Center
        '    e.Graphics.FillRectangle(backBrush, rect)
        '    Dim r As RectangleF = New RectangleF(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4)
        '    e.Graphics.DrawString(tabName, f, foreBrush, r, sf)
        'End If

        'sf.Dispose()

        'f.Dispose()
        'backBrush.Dispose()
        'foreBrush.Dispose()
    End Sub

    'this constructor is called from the RxRequest form when there is no patient that was registered in the clinic
    Public Sub New(ByVal PatFirstName As String, ByVal PatLastName As String, ByVal PatientDOB As String, ByVal PatientGender As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        txtfname.Text = PatFirstName
        txtlname.Text = PatLastName
        mskDOB1.Text = Format(CType(PatientDOB, Date), "MM/dd/yyyy")

        If PatientGender = "Male" Then
            rbGender1.Checked = True
        ElseIf PatientGender = "Female" Then
            rbGender2.Checked = True
        Else ' means the gender is passed as other
            rbGender3.Checked = True
        End If

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'this constructor is called from the RxRequest form when there is no patient that was registered in the clinic
    'Dim ofrmPatientRegMst As New frmPatientRegMst(strPatFirstName, strPatLastName, strPatientDOB, strPatientGender, strPataddr, strPhone, strCity, strState, strZip, strPharmacyname, strPharmacyid, strProvidername)
    Public Sub New(ByVal PatFirstName As String, ByVal PatLastName As String, ByVal PatientDOB As String, ByVal PatientGender As String, ByVal PatAddr As String, ByVal PatPhone As String, ByVal PatCity As String, ByVal PatState As String, ByVal PatZip As String, ByVal PharmacyName As String, ByVal Pharmacyid As String, ByVal strProviderId As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        txtfname.Text = PatFirstName
        txtlname.Text = PatLastName
        mskDOB1.Text = Format(CType(PatientDOB, Date), "MM/dd/yyyy")

        If PatientGender = "Male" Then
            rbGender1.Checked = True
        ElseIf PatientGender = "Female" Then
            rbGender2.Checked = True
        Else ' means the gender is passed as other
            rbGender3.Checked = True
        End If

        txtAddress1.Text = PatAddr
        mskIPhone1.Text = PatPhone
        txtCity.Text = PatCity
        cmbState.SelectedText = PatState
        txtZip.Text = PatZip
        txtPharmacy.Text = PharmacyName
        txtPharmacy.Tag = Pharmacyid
        RefillReqProviderID = strProviderId



        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Public Sub New(ByVal blnRecordLock As Boolean)
        _blnRecordLock = blnRecordLock
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnNext0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext0.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) '.TabPages(1)
        txtMother_fName.Focus()
    End Sub

    Private Sub btnNext1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext1.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) 'TabPages(2)
        trInsurance.Select()
    End Sub

    Private Sub btnPrev1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev1.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
        txtMother_fName.Focus()
    End Sub

    Private Sub btnNext2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext2.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(3)
        txtPatientCode.Focus()
    End Sub

    Private Sub btnPrevious2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious2.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) 'TabPages(1)
        txtPatientCode.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub btnBrowsePhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
    '    If Trim(btnStart.Text) = "Browse Photo" Then
    '        Try
    '            Dim factorwidth As Double
    '            Dim factorheight As Double
    '            factorwidth = 1
    '            factorheight = 1
    '            With dlgOpenFile
    '                .Title = "Select Clinic Logo"
    '                .Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif"
    '                .CheckFileExists = True
    '                .Multiselect = False
    '                .ShowHelp = False
    '                .ShowReadOnly = False
    '            End With
    '            If dlgOpenFile.ShowDialog = DialogResult.OK Then
    '                picFinalPatient.Image = Image.FromFile(dlgOpenFile.FileName)
    '                If picFinalPatient.Image.Width > 0.9 * picFinalPatient.Width Then
    '                    factorwidth = (picFinalPatient.Width * 0.9) / picFinalPatient.Image.Width
    '                End If
    '                If picFinalPatient.Image.Height > 0.9 * picFinalPatient.Height Then
    '                    factorheight = (picFinalPatient.Height * 0.9) / picFinalPatient.Image.Height
    '                End If
    '                picFinalPatient.Image = New Bitmap(picFinalPatient.Image, New Size(picFinalPatient.Image.Size.Width * factorwidth, picFinalPatient.Image.Size.Height * factorheight))
    '                picFinalPatient.SizeMode = PictureBoxSizeMode.CenterImage
    '                blnPhotoModified = True
    '            End If
    '        Catch objErr As Exception
    '            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    Else
    '        picFinalPatient.Visible = False
    '        picWebCamPatient.Visible = True
    '        myCam = New clsWebCam
    '        If myCam.initCam(picWebCamPatient.Handle.ToInt32) = True Then
    '            btnCapture.Visible = True
    '            btnCapture.Enabled = True
    '            btnStart.Enabled = False
    '        End If
    '    End If
    'End Sub

    Private Sub btnClearPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        picFinalPatient.Visible = True
        picFinalPatient.Image = Nothing
        blnPhotoModified = True
    End Sub

    Private Sub btnPrev3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev3.Click
        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) 'TabPages(1)
        txtPatientCode.Focus()
    End Sub

    'Private Sub btnCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCapture.Click, btnCapture1.Click
    '    Try
    '        btnCapture1.Enabled = False
    '        btnStart.Enabled = True

    '        If IsNothing(myCam) = False Then
    '            If myCam.iRunning Then
    '                picFinalPatient.Image = CType(myCam.copyFrame(picWebCamPatient, New RectangleF(0, 0, picWebCamPatient.Width, picWebCamPatient.Height)), Image)
    '                myCam.closeCam()
    '                picFinalPatient.Visible = True
    '                picWebCamPatient.Visible = False
    '                blnPhotoModified = True
    '            Else
    '                MessageBox.Show("Error Initializing Web Cam", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End If
    '        Else
    '            MessageBox.Show("Please check the Web Camera ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '        myCam = Nothing
    '        btnCapture.Enabled = True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '    Try

    '        If _blnRecordLock = True Then
    '            '' if the Patient Record is locked then dont ask for save 
    '            Me.Close()
    '            Exit Sub
    '        End If


    '        Dim Result As DialogResult
    '        Dim nArrayCounter As Integer = 0
    '        Dim _arrReferal() As String

    '        _arraySetData = New ArrayList
    '        '_arrReferal = 

    '        'SetData(_arraySetData)

    '        If intId <> 0 Then  '' For Modify
    '            ' Function for Checking Data of array with controls existing data

    '            If IsDataChangeModifyPatient() = True Then
    '                '' If There are Changes in Insurance then User have to Save the Changes in Insurance First
    '                Exit Sub
    '            End If
    '            'For nArrayCounter = 0 To _arrayGetData.Count - 1
    '            '    If _arrayGetData(nArrayCounter).ToString = _arraySetData(nArrayCounter + 1).ToString Then
    '            '        IsDataChange()
    '            '    Else
    '            '        MessageBox.Show("Data Change... " & nArrayCounter)
    '            '        Exit Sub
    '            '    End If
    '            'Next
    '        Else '' For Add New

    '        End If

    '        If bTextChangeFlag = True Or bTextChangeFlag_insurance = True Then
    '            Result = MessageBox.Show("Do you want to save your changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    '            If Result = Windows.Forms.DialogResult.Yes Then
    '                bTextChangeFlag = False
    '                btnOK_Click(sender, e)
    '            ElseIf Result = Windows.Forms.DialogResult.Cancel Then
    '                bTextChangeFlag = False
    '                Exit Sub
    '            Else
    '                bTextChangeFlag = False
    '                Me.Close()
    '            End If
    '        Else
    '            If bTextChangeFlag_insurance = True Then
    '                Exit Sub
    '            Else
    '                Me.Close()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    '---- sarika 6th nov 07
    Private Sub ClosePatientRegistration()
        Dim sender As Object
        Dim e As EventArgs
        Try

            If _blnRecordLock = True Then
                '' if the Patient Record is locked then dont ask for save 
                Me.Close()
                Exit Sub
            End If


            Dim Result As DialogResult
            Dim nArrayCounter As Integer = 0
            Dim _arrReferal() As String

            _arraySetData = New ArrayList
            '_arrReferal = 

            'SetData(_arraySetData)


            '      If intId <> 0 Then  '' For Modify

            ' Function for Checking Data of array with controls existing data
            If btnAdd.Text = "Add" Then


                If btnAdd.Enabled = True And (txtInsuranceNew.Text <> "") Then
                    '   Dim result As New Windows.Forms.DialogResult

                    '  result = MessageBox.Show("Please click on Add/Update Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If MessageBox.Show("Do you want to Add Insurance information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then

                        btnAdd_Click(sender, e)
                        bTextChangeFlag_insurance = True
                        '   MessageBox.Show("Please click on Add Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '  btnAdd.Focus()
                        'Exit Sub
                    Else
                        clearInsCtrls()
                        'btnAdd.Enabled = False
                        RefreshInsurance()
                        btnAdd.Enabled = False
                        bTextChangeFlag_insurance = False
                        'Exit Sub
                        ' bTextChangeFlag_insurance = IsInsuranceDataChange()
                    End If
                    'Else
                    '  bTextChangeFlag_insurance = IsInsuranceDataChange()
                End If  'btnAdd.Enabled = True 
            Else
                'if btnadd.text = "Update"

                ' code modified on 20070612 for modification for multiple selection of the Insurance
                'txtInsuranceName.Tag = PatientInsurance.InsuranceId
                'txtInsuranceName.Text = PatientInsurance.InsuranceName
                'cmbInsuranceNew.Tag = PatientInsurance.InsuranceId
                'cmbInsuranceNew.Text = PatientInsurance.InsuranceName
                Dim blnUpdateIns As Boolean = False
                blnUpdateIns = chkInsUpdated()
                'chk if info updated or not
                'if info updated then
                If blnUpdateIns = True Then
                    If MessageBox.Show("Do you want to Update Insurance information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then

                        btnAdd_Click(sender, e)
                        bTextChangeFlag_insurance = True
                        'btnAdd.Focus()
                        'Exit Sub
                    Else

                        'btnAdd.Enabled = False
                        RefreshInsurance()
                        btnAdd.Enabled = True
                        'bTextChangeFlag_insurance = False
                        ' Exit Sub
                        ' bTextChangeFlag_insurance = IsInsuranceDataChange()
                    End If   'if msg no
                End If   'if blnupdateinsurance
            End If  'btnadd.text

            'if Insurance Information is Changed the No Need to Check other Information Changes Or Not
            If bTextChangeFlag_insurance = False Then

                If IsDataChangeModifyPatient() = True Then
                    '' If There are Changes in Insurance then User have to Save the Changes in Insurance First
                    'Exit Sub
                    bTextChangeFlag = True
                Else
                    'chk for insurance changes
                    'save the insurances in the insurance collection
                    bTextChangeFlag_insurance = PatientRegistration.chkInsChanged()
                End If
            End If
            'For nArrayCounter = 0 To _arrayGetData.Count - 1
            '    If _arrayGetData(nArrayCounter).ToString = _arraySetData(nArrayCounter + 1).ToString Then
            '        IsDataChange()
            '    Else
            '        MessageBox.Show("Data Change... " & nArrayCounter)
            '        Exit Sub
            '    End If
            'Next

            If bTextChangeFlag = True Or bTextChangeFlag_insurance = True Or blnPhotoModified = True Then
                Result = MessageBox.Show("Do you want to save your changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                If Result = Windows.Forms.DialogResult.Yes Then
                    bTextChangeFlag = False
                    SaveandClosePatientRecord()
                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    bTextChangeFlag = False
                    Exit Sub
                Else
                    bTextChangeFlag = False
                    Me.Close()
                End If
            Else
                If bTextChangeFlag_insurance = True Then
                    Exit Sub
                Else
                    Me.Close()
                End If
            End If

            'Else '' For Add New
            ''Result = MessageBox.Show("Do you want to save your changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            ''If Result = Windows.Forms.DialogResult.Yes Then

            ''    btnOK_Click(sender, e)
            ''ElseIf Result = Windows.Forms.DialogResult.Cancel Then

            ''    Exit Sub
            ''Else

            ''    Me.Close()
            ''End If

            'If btnAdd.Text = "Add" Then


            '    If btnAdd.Enabled = True And (txtInsuranceNew.Text <> "") Then
            '        '   Dim result As New Windows.Forms.DialogResult

            '        '  result = MessageBox.Show("Please click on Add/Update Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        If MessageBox.Show("Do you want to Add Insurance information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            '            btnAdd_Click(sender, e)
            '            bTextChangeFlag_insurance = True
            '            '   MessageBox.Show("Please click on Add Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            '  btnAdd.Focus()
            '            'Exit Sub
            '        Else
            '            clearInsCtrls()
            '            'btnAdd.Enabled = False
            '            RefreshInsurance()
            '            btnAdd.Enabled = False
            '            bTextChangeFlag_insurance = False
            '            'Exit Sub
            '            ' bTextChangeFlag_insurance = IsInsuranceDataChange()
            '        End If
            '        'Else
            '        '  bTextChangeFlag_insurance = IsInsuranceDataChange()
            '    End If  'btnAdd.Enabled = True 
            'Else
            '    'if btnadd.text = "Update"

            '    ' code modified on 20070612 for modification for multiple selection of the Insurance
            '    'txtInsuranceName.Tag = PatientInsurance.InsuranceId
            '    'txtInsuranceName.Text = PatientInsurance.InsuranceName
            '    'cmbInsuranceNew.Tag = PatientInsurance.InsuranceId
            '    'cmbInsuranceNew.Text = PatientInsurance.InsuranceName
            '    Dim blnUpdateIns As Boolean = False
            '    blnUpdateIns = chkInsUpdated()
            '    'chk if info updated or not
            '    'if info updated then
            '    If blnUpdateIns = True Then
            '        If MessageBox.Show("Do you want to Update Insurance information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            '            btnAdd_Click(sender, e)
            '            bTextChangeFlag_insurance = True
            '            'btnAdd.Focus()
            '            'Exit Sub
            '        Else

            '            'btnAdd.Enabled = False
            '            RefreshInsurance()
            '            btnAdd.Enabled = True
            '            bTextChangeFlag_insurance = False
            '            ' Exit Sub
            '            ' bTextChangeFlag_insurance = IsInsuranceDataChange()
            '        End If   'if msg no
            '    End If   'if blnupdateinsurance
            'End If  'btnadd.text

            '' ''if Insurance Information is Changed the No Need to Check other Information Changes Or Not
            'If bTextChangeFlag_insurance = False Then

            '    If IsDataChangeModifyPatient() = True Then
            '        '' If There are Changes in Insurance then User have to Save the Changes in Insurance First
            '        'Exit Sub
            '        bTextChangeFlag = True
            '    Else
            '        'chk for insurance changes
            '        'save the insurances in the insurance collection
            '        bTextChangeFlag_insurance = PatientRegistration.chkInsChanged()
            '    End If
            'End If
            ''For nArrayCounter = 0 To _arrayGetData.Count - 1
            ''    If _arrayGetData(nArrayCounter).ToString = _arraySetData(nArrayCounter + 1).ToString Then
            ''        IsDataChange()
            ''    Else
            ''        MessageBox.Show("Data Change... " & nArrayCounter)
            ''        Exit Sub
            ''    End If
            ''Next

            'If bTextChangeFlag = True Or bTextChangeFlag_insurance = True Or blnPhotoModified = True Then
            '    Result = MessageBox.Show("Do you want to save your changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            '    If Result = Windows.Forms.DialogResult.Yes Then
            '        bTextChangeFlag = False
            '        btnOK_Click(sender, e)
            '    ElseIf Result = Windows.Forms.DialogResult.Cancel Then
            '        bTextChangeFlag = False
            '        Exit Sub
            '    Else
            '        bTextChangeFlag = False
            '        Me.Close()
            '    End If
            'Else
            '    If bTextChangeFlag_insurance = True Then
            '        Exit Sub
            '    Else
            '        Me.Close()
            '    End If
            'End If

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '    Try

    '        If _blnRecordLock = True Then
    '            '' if the Patient Record is locked then dont ask for save 
    '            Me.Close()
    '            Exit Sub
    '        End If


    '        Dim Result As DialogResult
    '        Dim nArrayCounter As Integer = 0
    '        Dim _arrReferal() As String

    '        _arraySetData = New ArrayList
    '        '_arrReferal = 

    '        'SetData(_arraySetData)

    '        If intId <> 0 Then  '' For Modify

    '            ' Function for Checking Data of array with controls existing data
    '            If btnAdd.Text = "Add" Then


    '                If btnAdd.Enabled = True And (txtInsuranceNew.Text <> "") Then
    '                    '   Dim result As New Windows.Forms.DialogResult

    '                    '  result = MessageBox.Show("Please click on Add/Update Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    If MessageBox.Show("Do you want to Add Insurance information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
    '                        btnAdd_Click(sender, e)
    '                        bTextChangeFlag_insurance = True
    '                        '   MessageBox.Show("Please click on Add Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        '  btnAdd.Focus()
    '                        'Exit Sub
    '                    Else
    '                        clearInsCtrls()
    '                        'btnAdd.Enabled = False
    '                        RefreshInsurance()
    '                        btnAdd.Enabled = False
    '                        bTextChangeFlag_insurance = False
    '                        'Exit Sub
    '                        ' bTextChangeFlag_insurance = IsInsuranceDataChange()
    '                    End If
    '                    'Else
    '                    '  bTextChangeFlag_insurance = IsInsuranceDataChange()
    '                End If  'btnAdd.Enabled = True 
    '            Else
    '                'if btnadd.text = "Update"

    '                ' code modified on 20070612 for modification for multiple selection of the Insurance
    '                'txtInsuranceName.Tag = PatientInsurance.InsuranceId
    '                'txtInsuranceName.Text = PatientInsurance.InsuranceName
    '                'cmbInsuranceNew.Tag = PatientInsurance.InsuranceId
    '                'cmbInsuranceNew.Text = PatientInsurance.InsuranceName
    '                Dim blnUpdateIns As Boolean = False
    '                blnUpdateIns = chkInsUpdated()
    '                'chk if info updated or not
    '                'if info updated then
    '                If blnUpdateIns = True Then
    '                    If MessageBox.Show("Do you want to Update Insurance information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
    '                        btnAdd_Click(sender, e)
    '                        bTextChangeFlag_insurance = True
    '                        'btnAdd.Focus()
    '                        'Exit Sub
    '                    Else

    '                        'btnAdd.Enabled = False
    '                        RefreshInsurance()
    '                        btnAdd.Enabled = True
    '                        bTextChangeFlag_insurance = False
    '                        ' Exit Sub
    '                        ' bTextChangeFlag_insurance = IsInsuranceDataChange()
    '                    End If   'if msg no
    '                End If   'if blnupdateinsurance
    '            End If  'btnadd.text

    '            'if Insurance Information is Changed the No Need to Check other Information Changes Or Not
    '            If bTextChangeFlag_insurance = False Then

    '                If IsDataChangeModifyPatient() = True Then
    '                    '' If There are Changes in Insurance then User have to Save the Changes in Insurance First
    '                    'Exit Sub
    '                    bTextChangeFlag = True
    '                Else
    '                    'chk for insurance changes
    '                    'save the insurances in the insurance collection
    '                    bTextChangeFlag_insurance = PatientRegistration.chkInsChanged()
    '                End If
    '            End If
    '            'For nArrayCounter = 0 To _arrayGetData.Count - 1
    '            '    If _arrayGetData(nArrayCounter).ToString = _arraySetData(nArrayCounter + 1).ToString Then
    '            '        IsDataChange()
    '            '    Else
    '            '        MessageBox.Show("Data Change... " & nArrayCounter)
    '            '        Exit Sub
    '            '    End If
    '            'Next

    '            If bTextChangeFlag = True Or bTextChangeFlag_insurance = True Or blnPhotoModified = True Then
    '                Result = MessageBox.Show("Do you want to save your changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    '                If Result = Windows.Forms.DialogResult.Yes Then
    '                    bTextChangeFlag = False
    '                    btnOK_Click(sender, e)
    '                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
    '                    bTextChangeFlag = False
    '                    Exit Sub
    '                Else
    '                    bTextChangeFlag = False
    '                    Me.Close()
    '                End If
    '            Else
    '                If bTextChangeFlag_insurance = True Then
    '                    Exit Sub
    '                Else
    '                    Me.Close()
    '                End If
    '            End If

    '        Else '' For Add New
    '            Result = MessageBox.Show("Do you want to save your changes to this record? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    '            If Result = Windows.Forms.DialogResult.Yes Then

    '                btnOK_Click(sender, e)
    '            ElseIf Result = Windows.Forms.DialogResult.Cancel Then

    '                Exit Sub
    '            Else

    '                Me.Close()
    '            End If

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Public Function chkInsUpdated() As Boolean
        Dim blnInsChanged As Boolean = False

        Try
            Dim blnUpdateIns As Boolean = False
            Dim PatientInsurance As New ClsPatientInsurance
            PatientRegistration.GetInsuranceCol(PatientInsurance, trInsurance.SelectedNode.Index)

            If PatientInsurance.InsuranceName <> txtInsuranceNew.Text Then
                blnUpdateIns = True
            ElseIf PatientInsurance.Phone <> mskIPhone1.Text Then
                blnUpdateIns = True
            ElseIf PatientInsurance.Primaryflag <> chkPrimary.Checked Then
                blnUpdateIns = True
            ElseIf PatientInsurance.SubscriberId <> txtSubscriberID.Text Then
                blnUpdateIns = True
            ElseIf PatientInsurance.Subscribername <> txtSubscriberName.Text Then
                blnUpdateIns = True
            ElseIf PatientInsurance.SubscriberPolicy <> txtSubscriberPolicy.Text Then
                blnUpdateIns = True
            ElseIf PatientInsurance.Group <> txtGroup.Text Then
                blnUpdateIns = True
            ElseIf PatientInsurance.Employer <> txtEmployer.Text Then
                blnUpdateIns = True
            ElseIf chkDateChanged(PatientInsurance.DOB, mskIDOB1) = True Then
                blnUpdateIns = True
                'ElseIf PatientInsurance.DOB.Date <> Convert.ToDateTime(mskIDOB1.Text).Date Then

            ElseIf PatientInsurance.Checked <> chkSameasPatient.Checked Then
                blnUpdateIns = True
            End If

            Return blnUpdateIns
        Catch ex As Exception

        End Try
    End Function

   

    '-------------------------------

    Private Sub SaveandClosePatientRecord()
        Dim sender As Object
        Dim e As EventArgs

        Dim _lPatientDirectiveID As Int64 = 0

        Dim Arrlist As New ArrayList
        Try


            If Trim(txtPatientCode.Text) = "" Then
                MessageBox.Show("Enter Patient Code", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
                txtPatientCode.Focus()
                _blnPrint = False
                Exit Sub
            ElseIf Trim(txtfname.Text) = "" Then
                MessageBox.Show("Enter First Name", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
                txtfname.Focus()
                _blnPrint = False
                Exit Sub
            ElseIf Trim(txtlname.Text) = "" Then
                MessageBox.Show("Enter Last Name", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
                txtlname.Focus()
                _blnPrint = False
                Exit Sub
                ' modification on 20070522 - Bipin
            ElseIf Len(mskDOB1.Text) <> 10 Then
                MessageBox.Show("Enter Date of Birth", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
                mskDOB1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(mskDOB.ClipText) <> 8 Then
                '    MessageBox.Show("Date of Birth Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskDOB.Focus()
                '    Exit Sub

            ElseIf Trim(Trim(cmbProvider.Text)) = "" Then
                MessageBox.Show("Provider Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) 'TabPages(0)
                cmbProvider.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Not IsDate(mskDOB.CtlText) Then
                'ElseIf Not IsDate(Format(CType(mskDOB1.Text, System.DateTime).Date, "MM/dd/yyyy")) Then
            ElseIf Not IsDate(mskDOB1.Text) Then
                MessageBox.Show("Invalid Date of Birth ", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
                mskDOB1.Mask = ""
                'mskDOB1.CtlText = ""
                mskDOB1.Mask = "##/##/####"
                'mskDOB1.Format = "MM/dd/yyyy"
                mskDOB1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Not IsDate(mskDOB.FormattedText) Then
                '    MessageBox.Show("Invalid Date of Birth ", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskDOB.Mask = ""
                '    mskDOB.CtlText = ""
                '    mskDOB.Mask = "##/##/####"
                '    mskDOB.Format = "MM/dd/yyyy"
                '    mskDOB.Focus()
                '    Exit Sub
                'changes done by Bipin on 20070522 CCHIT 2007
            ElseIf Len(Trim(MskSSn1.Text)) <> 9 And Val(MskSSn1.Text) <> 0 Then
                MessageBox.Show("Invalid SSN Number", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
                MskSSn1.Focus()
                _blnPrint = False
                Exit Sub

                'ElseIf Len(Trim(MskSSn.ClipText)) <> 9 And Val(MskSSn.ClipText) <> 0 Then
                '    MessageBox.Show("Invalid SSN Number", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    MskSSn.Focus()
                '    Exit Sub

            ElseIf Len(Trim(mskPhone1.Text)) > 0 And Len(Trim(mskPhone1.Text)) < 10 Then
                MessageBox.Show("Phone Details Incomplete", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
                mskPhone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskPhone.ClipText)) > 0 And Len(Trim(mskPhone.ClipText)) < 10 Then
                '    MessageBox.Show("Phone Details Incomplete", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskPhone.Focus()
                '    Exit Sub

                'sarika 27th aug 07
                'ElseIf Len(Trim(mskwPhone1.Text)) > 0 And Len(Trim(mskwPhone1.Text)) < 10 Then
                '    ' If MessageBox.Show("Do you want to enter the Work Phone Details?", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) '.TabPages(1)
                '    'Else
                '    mskwPhone1.Text = ""
                '    ' End If
                '    mskwPhone1.Focus()

                '    ' _blnPrint = False
                '    ' Exit Sub
                '    '-----------
                '    'ElseIf Len(Trim(mskwPhone.ClipText)) > 0 And Len(Trim(mskwPhone.ClipText)) < 10 Then
                '    '    MessageBox.Show("Work Phone Details Required", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(1)
                '    '    mskwPhone.Focus()
                '    '    Exit Sub

            ElseIf Len(Trim(mskMobile1.Text)) > 0 And Len(Trim(mskMobile1.Text)) < 10 Then
                MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
                mskMobile1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskMobile.ClipText)) > 0 And Len(Trim(mskMobile.ClipText)) < 10 Then
                '    MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskMobile.Focus()
                '    Exit Sub


                '----------------Guardian Mobile validation
                '---------Mother
            ElseIf Len(Trim(mskMother_Mobile1.Text)) > 0 And Len(Trim(mskMother_Mobile1.Text)) < 10 Then
                MessageBox.Show("Mother Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(0)
                mskMother_Mobile1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskMobile.ClipText)) > 0 And Len(Trim(mskMobile.ClipText)) < 10 Then
                '    MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskMobile.Focus()
                '    Exit Sub
                '-------------------Father
            ElseIf Len(Trim(mskFather_Mobile1.Text)) > 0 And Len(Trim(mskFather_Mobile1.Text)) < 10 Then
                MessageBox.Show("Father Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(0)
                mskFather_Mobile1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskMobile.ClipText)) > 0 And Len(Trim(mskMobile.ClipText)) < 10 Then
                '    MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskMobile.Focus()
                '    Exit Sub
                '---------------------Guardian 
            ElseIf Len(Trim(mskGuardian_Mobile1.Text)) > 0 And Len(Trim(mskGuardian_Mobile1.Text)) < 10 Then
                MessageBox.Show("Guardian Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(0)
                mskGuardian_Mobile1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskMobile.ClipText)) > 0 And Len(Trim(mskMobile.ClipText)) < 10 Then
                '    MessageBox.Show("Mobile Details Required", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(0)
                '    mskMobile.Focus()
                '    Exit Sub

                '----------------------------------OCCUPATION PHONE VALIDATIONs
            ElseIf Len(Trim(mskwPhone1.Text)) > 0 And Len(Trim(mskwPhone1.Text)) < 10 Then
                MessageBox.Show("Occupation Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(1) '.TabPages(2)
                mskwPhone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskspousePhone.ClipText)) > 0 And Len(Trim(mskspousePhone.ClipText)) < 10 Then
                '    MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskspousePhone.Focus()
                '    Exit Sub

                ' modification on 20070522 by Bipin

                '---------------------------------GUARDIAN INFORMATION PHONE VALIDATIONs
                '---------------MOTHER
            ElseIf Len(Trim(mskMother_Phone1.Text)) > 0 And Len(Trim(mskMother_Phone1.Text)) < 10 Then
                MessageBox.Show("Mother Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
                mskMother_Phone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskspousePhone.ClipText)) > 0 And Len(Trim(mskspousePhone.ClipText)) < 10 Then
                '    MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskspousePhone.Focus()
                '    Exit Sub

                ' modification on 20070522 by Bipin
                '-------------------FATHER
            ElseIf Len(Trim(mskFather_Phone1.Text)) > 0 And Len(Trim(mskFather_Phone1.Text)) < 10 Then
                MessageBox.Show("Father Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
                mskFather_Phone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskspousePhone.ClipText)) > 0 And Len(Trim(mskspousePhone.ClipText)) < 10 Then
                '    MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskspousePhone.Focus()
                '    Exit Sub

                ' modification on 20070522 by Bipin
                '----------------GUARDIAN
            ElseIf Len(Trim(mskGuardian_Phone1.Text)) > 0 And Len(Trim(mskGuardian_Phone1.Text)) < 10 Then
                MessageBox.Show("Guardian Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(2) '.TabPages(2)
                mskGuardian_Phone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskspousePhone.ClipText)) > 0 And Len(Trim(mskspousePhone.ClipText)) < 10 Then
                '    MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskspousePhone.Focus()
                '    Exit Sub

                ' modification on 20070522 by Bipin
                '----------------------------------

            ElseIf Len(Trim(mskspousePhone1.Text)) > 0 And Len(Trim(mskspousePhone1.Text)) < 10 Then
                MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(2)
                mskspousePhone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(Trim(mskspousePhone.ClipText)) > 0 And Len(Trim(mskspousePhone.ClipText)) < 10 Then
                '    MessageBox.Show("Spouse Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskspousePhone.Focus()
                '    Exit Sub

                ' modification on 20070522 by Bipin
            ElseIf Len(mskIPhone1.Text) > 0 And Len(mskIPhone1.Text) < 10 Then
                MessageBox.Show("Insurance Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(2)
                mskIPhone1.Focus()
                _blnPrint = False
                Exit Sub
                'ElseIf Len(mskIPhone.ClipText) > 0 And Len(mskIPhone.ClipText) < 10 Then
                '    MessageBox.Show("Insurance Phone Details Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskIPhone.Focus()
                '    Exit Sub
            Else
                ' modification on 20070522 by Bipin

                If Len(Trim(mskRegistrationdate1.Text)) > 4 And Len(Trim(mskRegistrationdate1.Text)) < 8 Then
                    MessageBox.Show("Registration Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(2)
                    mskRegistrationdate1.Mask = "##/##/####"
                    mskRegistrationdate1.Focus()
                    _blnPrint = False
                    Exit Sub
                    'If Len(Trim(mskRegistrationdate.ClipText)) > 0 And Len(Trim(mskRegistrationdate.ClipText)) < 8 Then
                    '    MessageBox.Show("Registration Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                    '    mskRegistrationdate.Focus()
                    '    Exit Sub

                    ' modification on 20070522 by Bipin
                ElseIf Len(Trim(mskRegistrationdate1.Text)) = 10 Then
                    If Not IsDate(mskRegistrationdate1.Text) Then
                        MessageBox.Show("Invalid Registration Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(2)
                        mskRegistrationdate1.Mask = ""
                        mskRegistrationdate1.Text = ""
                        mskRegistrationdate1.Mask = "##/##/####"

                        mskRegistrationdate1.Focus()
                        _blnPrint = False
                        Exit Sub
                    End If

                    'ElseIf Len(Trim(mskRegistrationdate.ClipText)) = 8 Then
                    '    If Not IsDate(mskRegistrationdate.FormattedText) Then
                    '        MessageBox.Show("Invalid Registration Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                    '        mskRegistrationdate.Mask = ""
                    '        mskRegistrationdate.CtlText = ""
                    '        mskRegistrationdate.Mask = "##/##/####"
                    '        mskRegistrationdate.Format = "MM/dd/yyyy"
                    '        mskRegistrationdate.Focus()
                    '        Exit Sub
                    '    End If
                End If
                mskRegistrationdate1.TextMaskFormat = MaskFormat.IncludeLiterals
            End If


            '---------validation for DOB greater that todays date
            If mskDOB1.Text.Trim.Length > 4 And mskDOB1.Text.Trim.Length < 11 Then
                If IsDate(mskDOB1.Text) Then
                    Dim tempDate As DateTime
                    tempDate = Format(CType(mskDOB1.Text, Date), "MM/dd/yyyy")
                    'If tempDate.Year < 1900 Then
                    If tempDate.Year < 1850 Then
                        MessageBox.Show("Date of birth should not less than year 1850 ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'mskDOB1.Clear()
                        mskDOB1.Focus()
                        Exit Sub
                    End If

                    If tempDate > System.DateTime.Now.Date Then
                        MessageBox.Show("Date of birth should not greater than todays date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'mskDOB1.Clear()
                        mskDOB1.Focus()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Invalid Birth date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'mskDOB1.Clear()
                    mskDOB1.Focus()
                    'Exit Sub
                End If
                'Else
                '    MessageBox.Show("Enter Date of Birth", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    mskDOB1.Focus()
                '    Exit Sub
            End If
            '---------validation for DOB greater that todays date

            '********************Insurance tab date validation
            '-------Registration date
            mskRegistrationdate1.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            If mskRegistrationdate1.Text <> "" Then
                If mskRegistrationdate1.MaskCompleted = False Then

                    MessageBox.Show("Enter Registration Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(0)
                    mskRegistrationdate1.Focus()
                    _blnPrint = False
                    Exit Sub

                End If
            End If
            mskRegistrationdate1.TextMaskFormat = MaskFormat.IncludeLiterals

            '---------Surgery date
            mskSurgeryDate1.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            If mskSurgeryDate1.Text <> "" Then

                If mskSurgeryDate1.MaskCompleted = False Then

                    MessageBox.Show("Enter Surgery Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(0)
                    mskSurgeryDate1.Focus()
                    _blnPrint = False
                    Exit Sub

                End If
            End If
            mskSurgeryDate1.TextMaskFormat = MaskFormat.IncludeLiterals
            '----------Injury date
            mskInjurydate1.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            If mskInjurydate1.Text <> "" Then
                If mskInjurydate1.MaskCompleted = False Then

                    MessageBox.Show("Enter Injury Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(0)
                    mskInjurydate1.Focus()
                    _blnPrint = False
                    Exit Sub

                End If
            End If
            mskInjurydate1.TextMaskFormat = MaskFormat.IncludeLiterals
            '*************************************************



            ' modification on 20070522 by Bipin
            If Len(Trim(mskSurgeryDate1.Text)) > 4 And Len(Trim(mskSurgeryDate1.Text)) < 8 Then
                MessageBox.Show("Surgery Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
                mskSurgeryDate1.Focus()
                _blnPrint = False
                Exit Sub
                'If Len(Trim(mskSurgeryDate.ClipText)) > 0 And Len(Trim(mskSurgeryDate.ClipText)) < 8 Then
                '    MessageBox.Show("Surgery Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                '    mskSurgeryDate.Focus()
                '    Exit Sub
            ElseIf Len(Trim(mskSurgeryDate1.Text)) = 10 Then
                If Not IsDate(mskSurgeryDate1.Text) Then
                    MessageBox.Show("Invalid Surgery Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
                    mskSurgeryDate1.Mask = ""
                    mskSurgeryDate1.Text = ""
                    mskSurgeryDate1.Mask = "##/##/####"
                    mskSurgeryDate1.Focus()
                    _blnPrint = False
                    Exit Sub
                End If
            End If
            'ElseIf Len(Trim(mskSurgeryDate.ClipText)) = 8 Then
            '    If Not IsDate(mskSurgeryDate.FormattedText) Then
            '        MessageBox.Show("Invalid Surgery Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
            '        mskSurgeryDate.Mask = ""
            '        mskSurgeryDate.CtlText = ""
            '        mskSurgeryDate.Mask = "##/##/####"
            '        mskSurgeryDate.Format = "MM/dd/yyyy"
            '        mskSurgeryDate.Focus()
            '        Exit Sub
            '    End If
            'End If

            ' modification on 20070522 by Bipin
            If Len(Trim(mskInjurydate1.Text)) > 4 And Len(Trim(mskInjurydate1.Text)) < 8 Then
                MessageBox.Show("Injury Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
                mskInjurydate1.Focus()
                _blnPrint = False
                Exit Sub
            ElseIf Len(Trim(mskInjurydate1.Text)) = 10 Then
                If Not IsDate(mskInjurydate1.Text) Then
                    MessageBox.Show("Invalid Injury Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(3)
                    mskInjurydate1.Mask = ""
                    mskInjurydate1.Text = ""
                    mskInjurydate1.Mask = "##/##/####"
                    'mskInjurydate.Format = "MM/dd/yyyy"
                    mskInjurydate1.Focus()
                    _blnPrint = False
                    Exit Sub
                End If

            End If

            'If Len(Trim(mskInjurydate.ClipText)) > 0 And Len(Trim(mskInjurydate.ClipText)) < 8 Then
            '    MessageBox.Show("Injury Date Incomplete", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
            '    mskInjurydate.Focus()
            '    Exit Sub
            'ElseIf Len(Trim(mskInjurydate.ClipText)) = 8 Then
            '    If Not IsDate(mskInjurydate.FormattedText) Then
            '        MessageBox.Show("Invalid Injury Date", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
            '        mskInjurydate.Mask = ""
            '        mskInjurydate.CtlText = ""
            '        mskInjurydate.Mask = "##/##/####"
            '        mskInjurydate.Format = "MM/dd/yyyy"
            '        mskInjurydate.Focus()
            '        Exit Sub
            '    End If

            'End If



            'sarika Workers Comp 7th May 08

            If chkWorkersComp.Checked = True Then
                If txtWorkersCompClaimNo.Text = "" Then
                    MessageBox.Show("Please enter the Worker's Compensation Claim Number.", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWorkersCompClaimNo.Focus()
                    Exit Sub
                End If
                'If mskInjurydate1.Text = "" Then
                If Len(Trim(mskInjurydate1.Text)) <= 4 Then
                    MessageBox.Show("Please enter the injury date.", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    mskInjurydate1.Focus()
                    Exit Sub
                End If
            End If
            'sarika Workers Comp 7th May 08



            'sarika Auto 7th May 08

            If chkAuto.Checked = True Then
                If txtAutoClaimNo.Text = "" Then
                    MessageBox.Show("Please enter the Auto Claim Number.", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtAutoClaimNo.Focus()
                    Exit Sub
                End If
                'If mskInjurydate1.Text = "" Then
                If Len(Trim(mskInjurydate1.Text)) <= 4 Then
                    MessageBox.Show("Please enter the injury date.", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    mskInjurydate1.Focus()
                    Exit Sub
                End If
            End If
            'sarika Auto 7th May 08



            'If chkDirective.Checked = True Then
            '    Dim oDMSPathChk As New gloStream.gloDMS.Supporting.Supporting
            '    If oDMSPathChk.IsDMSSystem(DMSRootPath) = True Then
            '        Dim oChk As New gloStream.gloDMS.DocumentCategory.DocumentCategory
            '        Dim oCat As New gloStream.gloDMS.DocumentCategory.Category
            '        oCat.Name = gDMSCategory_PatientDirective : oCat.IsDeleted = False
            '        If oChk.IsExists(oCat) = False Then
            '            MessageBox.Show("Advance Directive, DMS Category not set. Please set the category.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End If
            '    Else
            '        MessageBox.Show("Document Management System Path not set to scan patient directive document, please use Tools->Setting command to set DMS path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    End If
            '    oDMSPathChk = Nothing
            'End If

            'sarika 23rd july 08

            '' ''// Vinayak - 5 Feb 2007 //
            ' ''Dim _ScanDocFlag As Boolean = False
            ' ''If intId = 0 Then
            ' ''    If _lPatientDirectiveID > 0 Then
            ' ''        If chkDirective.Checked = True Then
            ' ''            _ScanDocFlag = True
            ' ''        End If
            ' ''    End If
            ' ''ElseIf intId > 0 Then
            ' ''    If _lPatientDirectiveID > 0 Then
            ' ''        If chkDirective.Checked = True Then
            ' ''            _ScanDocFlag = True
            ' ''        End If
            ' ''    End If
            ' ''End If

            ' ''Dim _ScanContainerID As Int64 = 0
            ' ''Dim _ScanDocumentID As Int64 = 0
            ' ''Dim _result As Boolean = False

            ' ''If _ScanDocFlag = True Then
            ' ''    If gloEDocument.eDocManager.eDocValidator.IsCategoryExists(0, gDMSCategory_PatientDirective, gClinicID) = False Then
            ' ''        MessageBox.Show("DMS Category for lab order has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' ''        _ScanDocFlag = False
            ' ''    End If
            ' ''End If

            ' ''If _ScanDocFlag = True Then
            ' ''    If intId = 0 Then
            ' ''        _result = Set_ScanDocumentEvent(_lPatientDirectiveID, gDMSCategory_PatientDirective, _ScanContainerID, _ScanDocumentID)
            ' ''    Else
            ' ''        If _PatientDirectiveStatus = True Then
            ' ''            If MessageBox.Show("Do you want to add more documents against patient directive?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ' ''                _result = Set_ScanDocumentEvent(_lPatientDirectiveID, gDMSCategory_PatientDirective, _ScanContainerID, _ScanDocumentID)
            ' ''            End If
            ' ''        Else
            ' ''            _result = Set_ScanDocumentEvent(_lPatientDirectiveID, gDMSCategory_PatientDirective, _ScanContainerID, _ScanDocumentID)
            ' ''        End If
            ' ''    End If
            ' ''End If


            ' ''chkDirective.Checked = _result
            '-----------------

            SetData(Arrlist)
            'RefreshInsurance()
            'Dim ArrInsurance(chkLstInsurance.CheckedItems.Count - 1) As Int64
            Dim ArrReferral(cmbReferrals.Items.Count - 1) As Long
            Dim mlist As myList
            Dim i As Integer
            'If chkLstInsurance.CheckedItems.Count <> 0 Then
            '    For i = 0 To chkLstInsurance.CheckedItems.Count - 1
            '        mlist = chkLstInsurance.CheckedItems(i)
            '        ArrInsurance(i) = mlist.Index
            '    Next
            'End If
            If cmbReferrals.Items.Count <> 0 Then
                For i = 0 To cmbReferrals.Items.Count - 1
                    mlist = cmbReferrals.Items.Item(i)
                    ArrReferral(i) = mlist.Index
                Next
            End If
            If intId = 0 Then
                _lPatientDirectiveID = PatientRegistration.AddData(Arrlist, ArrReferral, optWebCam.Checked)
                If gblnAddModPatient Then
                    SendHL7PatientDetails(_lPatientDirectiveID, False)
                End If
            Else
                _lPatientDirectiveID = intId
                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                If CheckPatientStatus(intId, , True) = False Then
                    If gblnAddModPatient Then
                        SendHL7PatientDetails(_lPatientDirectiveID, True)
                    End If
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20060918 -Mahesh 
                '''' Check if Current User has Admin Rights 
                'If gblnIsAdmin = False Then
                '    ''if Not then warn user
                '    Dim oclsPatReg As New ClsPatientRegistrationDBLayer
                '    With oclsPatReg
                '        Dim PatientStatus As String = ""
                '        PatientStatus = .PatientStatus(gnPatientID)
                '        oclsPatReg = Nothing
                '        '' If Patient Status Is "Legal Pending" or "Decesed" then 
                '        '' dont Allow any activity against this Patient
                '        If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Or PatientStatus = gtsrPatientStatus_LockCharts Then
                '            MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Adminstrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '            Exit Sub
                '        End If
                '    End With
                'Else
                '    '' Allow User to save the Changes 
                'End If

                '''''<><><><><><><><><><><>''''
                '' Original UpadteData Procedure
                '' 20061225- Mahesh
                '' Commented By Mahesh for --" Patient Change History "
                'PatientRegistration.UpdateData(Arrlist, ArrReferral, blnPhotoModified)

                '' Patient Change History
                '' 20061225- Mahesh
                Dim IsChange As Boolean = False
                If IsDataChange() = True Then
                    '' If PAtient Recodrd
                    IsChange = True
                Else
                    IsChange = False
                End If



                PatientRegistration.UpdateData(Arrlist, ArrReferral, ArrPatientOriginalData, blnPhotoModified, IsChange)
                If gblnAddModPatient Then
                    SendHL7PatientDetails(_lPatientDirectiveID, True)
                End If
            End If

            'sarika 23rd july 08
            '// Vinayak - 5 Feb 2007 //

            Dim _ScanDocFlag As Boolean = False

            If intId = 0 Then
                If _lPatientDirectiveID > 0 Then
                    If chkDirective.Checked = True Then
                        _ScanDocFlag = True
                    End If
                End If
            ElseIf intId > 0 Then
                If _lPatientDirectiveID > 0 Then
                    If chkDirective.Checked = True Then
                        _ScanDocFlag = True
                    End If
                End If
            End If

            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _result As Boolean = False

            If _ScanDocFlag = True Then
                If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, gDMSCategory_PatientDirective, gClinicID) = False Then
                    MessageBox.Show("DMS Category for Advance Directive has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _ScanDocFlag = False
                End If
            End If

            If _ScanDocFlag = True Then
                If intId = 0 Then
                    _result = Set_ScanDocumentEvent(_lPatientDirectiveID, gDMSCategory_PatientDirective, _ScanContainerID, _ScanDocumentID)
                Else
                    If _PatientDirectiveStatus = True Then
                        If MessageBox.Show("Do you want to add more documents against patient directive?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            _result = Set_ScanDocumentEvent(_lPatientDirectiveID, gDMSCategory_PatientDirective, _ScanContainerID, _ScanDocumentID)
                        End If
                    Else
                        _result = Set_ScanDocumentEvent(_lPatientDirectiveID, gDMSCategory_PatientDirective, _ScanContainerID, _ScanDocumentID)
                    End If
                End If
                'sarika 23rd july 08
                'for Patient Reg Advance Directive
            Else

                _result = False
                '-----------
            End If


            'sarika 23rd july 08
            'for Patient Reg Advance Directive
            If (intId = 0) Then
                If (_result = False) Then

                    chkDirective.Checked = _result

                    'update advanced directive flag to false

                    PatientRegistration.UpdateAdvDirective(_lPatientDirectiveID)

                End If
            Else
                If _blnScanDoc = True Then
                    chkDirective.Checked = _blnScanDoc
                Else
                    If (_result = False) Then

                        chkDirective.Checked = _result

                        'update advanced directive flag to false

                        PatientRegistration.UpdateAdvDirective(_lPatientDirectiveID)

                    End If
                End If
            End If
            
            '--------------------


            '// Vinayak - 5 Feb 2007 //
            '-------------------------------------

            gblnPatientAdded = True
            gstrPatientCode = txtPatientCode.Text

            intId = _lPatientDirectiveID

            '' All Changes Have Been Saved So Reset This bTextChangeFlag  Flag
            bTextChangeFlag = False
            '' 

            If _blnPrint = False Then
                '' IF Click on OK Then Only Close The Form
                '' else  IF This Procedure Called From Print Then Dont Close It
                Me.Close()
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPharmacyBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPharmacyBrowse.Click
        intStatus = 1
        'LoadGrid()
        IsPharmacy = True
        Call loadC1flexgrid()

    End Sub

    Private Sub btnPharmacyClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPharmacyClose.Click
        If Trim(txtPharmacy.Text) <> "" And txtPharmacy.Tag <> 0 Then
            If MessageBox.Show("Are you sure you want to Clear Pharmacy", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtPharmacy.Text = ""
                txtPharmacy.Tag = 0
            End If
        End If
    End Sub

    Private Sub btnReferralsClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferralsClose.Click
        If cmbReferrals.Items.Count > 0 Then
            If MessageBox.Show("Are you sure you want to Clear Referrals", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                'cmbReferrals.Items.Clear()
                'sarika 3rd oct 07
                If cmbReferrals.SelectedIndex < 0 Then
                    MessageBox.Show("Please select the Referral Name, you want to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                '--------------------------
                cmbReferrals.Items.RemoveAt(cmbReferrals.SelectedIndex)
                If cmbReferrals.Items.Count = 0 Then
                    'cmbReferrals.Items.Clear()
                    cmbReferrals.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub btnReferralsBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferralsBrowse.Click
        'intStatus = 4
        ''LoadGrid()chkSameasPatient
        'Call loadC1flexgrid()
        intStatus = 2
        'LoadGrid()
        IsPharmacy = False
        Call loadC1flexgrid()
    End Sub

    Private Sub btnPCPClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPCPClose.Click
        If Trim(txtpcp.Text) <> "" And txtpcp.Tag <> 0 Then
            If MessageBox.Show("Are you sure you want to Clear Primary Care Physician", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtpcp.Text = ""
                txtpcp.Tag = 0
            End If
        End If
    End Sub

    Private Sub btnPCPBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPCPBrowse.Click
        intStatus = 4
        'LoadGrid()
        IsPharmacy = False
        Call loadC1flexgrid()
    End Sub

    'code commented by sarika 6th nov 07
    'Private Sub btnCloseInsuranceName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseInsuranceName.Click
    '    ' code added on 20070613 for multiple selection of Insurance 
    '    'code commented by sarika 17th june 07
    '    'If cmbInsuranceNew.Items.Count > 0 Then

    '    '    If MessageBox.Show("Are you sure you want to Clear Insurance.", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
    '    '        'cmbReferrals.Items.Clear()
    '    '        'also remove it from the insurance tree

    '    '        cmbInsuranceNew.Items.RemoveAt(cmbInsuranceNew.SelectedIndex)

    '    '        If cmbInsuranceNew.Items.Count = 0 Then
    '    '            'cmbReferrals.Items.Clear()
    '    '            cmbInsuranceNew.Text = ""
    '    '        End If
    '    '    End If
    '    'End If
    '    '-------------

    '    'sarika 17th aug 07
    '    txtInsuranceNew.Text = ""

    '    '--------------------------

    '    'If Trim(txtInsuranceName.Text) <> "" And txtInsuranceName.Tag <> 0 Then
    '    '    If MessageBox.Show("Are you sure you want to Clear Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
    '    '        txtInsuranceName.Text = ""
    '    '        txtInsuranceName.Tag = 0
    '    '    End If
    '    'End If

    '    'If Trim(cmbInsuranceNew.Text) <> "" Then 'And cmbInsuranceNew.Tag <> 0 Then
    '    '    If MessageBox.Show("Are you sure you want to Clear Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
    '    '        cmbInsuranceNew.Text = ""
    '    '        cmbInsuranceNew.Tag = 0
    '    '    End If
    '    'End If

    '    'If btnAdd.Text = "Update" Then
    '    '    If Trim(txtInsuranceName.Text) <> "" Then
    '    '        If MessageBox.Show("Are you sure you want to Delete the Selected Patient Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
    '    '            Try
    '    '                If key >= 0 Then
    '    '                    PatientRegistration.DeleteInsurance(key)
    '    '                    mynode.Nodes.RemoveAt(key)
    '    '                    RefreshInsurance()
    '    '                    key = -1
    '    '                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '    '                    trInsurance.Select()
    '    '                    'Else
    '    '                    '    txtInsuranceName.Text = ""
    '    '                End If

    '    '            Catch ex As SqlClient.SqlException
    '    '                MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '            Catch ex As Exception
    '    '                MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '            End Try
    '    '        End If
    '    '    End If
    '    'End If
    'End Sub

    'Private Sub btnInsurance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsurance.Click
    '    Try
    '        intStatus = 3
    '        Call loadC1flexgrid()
    '        ' modification in 20070525 - Bipin
    '        'AddControl()

    '        'If Not IsNothing(dgCustomGrid) Then
    '        '    dgCustomGrid.Top = tbPatientRegistration.Top + GroupBox9.Top + txtspousename.Top
    '        '    dgCustomGrid.Left = tbPatientRegistration.Left + trInsurance.Width + 10
    '        '    dgCustomGrid.Height = GroupBox9.Height + GroupBox14.Height
    '        '    dgCustomGrid.Visible = True
    '        '    dgCustomGrid.Width = GroupBox14.Width
    '        '    dgCustomGrid.BringToFront()
    '        '    BindGrid()
    '        'End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    '0--------

    'code added by sarika 6th nov 07

    Private Sub btnCloseInsuranceName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseInsuranceName.Click

        txtInsuranceNew.Text = ""

        'sarika 2nd nov 07
        clearInsCtrls()
        '----
        '--------------------------
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            '<><><><><><><>sarika 17th aug 07<><><><><><><><>

            If btnAdd.Text = "Add" Then
                'insurance name valuidation
                'it should not be empty
                If Trim(txtInsuranceNew.Text) = "" Then
                    MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtInsuranceNew.Focus()
                    Exit Sub
                End If

                If Not CheckNode(txtInsuranceNew.Text) Then
                    'MsgBox("Duplicate Insurance")
                    MessageBox.Show("Duplicate Insurance", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshInsurance()
                    trInsurance.ExpandAll()
                    trInsurance.Select()
                    key = -1
                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
                    'PatientInsurance = Nothing
                    Exit Sub
                End If

                'phone validations
                If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
                    'MsgBox("Insurance Phone Details Incomplete")
                    MessageBox.Show("Insurance Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                    mskIPhone1.Focus()
                    Exit Sub
                End If

                'DOB validations
                mskIDOB1.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
                If mskIDOB1.Text <> "" Then
                    mskIDOB1.TextMaskFormat = MaskFormat.IncludeLiterals
                    If Len(mskIDOB1.Text) <> 10 Then
                        MessageBox.Show("Enter Date of Birth", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(0)
                        mskIDOB1.Focus()
                        Exit Sub
                    End If
                End If
                mskIDOB1.TextMaskFormat = MaskFormat.IncludeLiterals
                    


                Dim PatientInsurance As New ClsPatientInsurance

                If Len(mskIDOB1.Text) = 10 Then
                    If Not IsDate(mskIDOB1.Text) Then
                        MessageBox.Show("Invalid Date of Birth ", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(0)
                        mskIDOB1.Mask = ""
                        'mskDOB1.CtlText = ""
                        mskIDOB1.Mask = "##/##/####"
                        'mskDOB1.Format = "MM/dd/yyyy"
                        mskIDOB1.Focus()
                        Exit Sub
                    Else
                        PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
                        PatientInsurance.Checked = chkSameasPatient.Checked ''True
                    End If
                Else
                    PatientInsurance.Checked = False
                End If



                'commented the code becaz it was giving the Conversion from String “”to date is not valid error message .
                'If Len(mskIDOB1.Text) = 10 Then
                '    If IsDate(CType(mskIDOB1.Text, Date)) Then
                '        '            PatientInsurance.DOB = mskIDOB1.CtlText
                '        PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
                '        PatientInsurance.Checked = True
                '    Else
                '        MsgBox("Invalid Birth Date")

                '        mskIDOB1.Focus()
                '        Exit Sub
                '    End If
                'Else
                '    PatientInsurance.Checked = False
                'End If

                'add the insurance in the PatientInsurance collection
                PatientInsurance.InsuranceId = txtInsuranceNew.Tag
                PatientInsurance.InsuranceName = txtInsuranceNew.Text
                ' PatientInsurance.PatientId = intId
                PatientInsurance.SubscriberId = txtSubscriberID.Text
                PatientInsurance.Subscribername = txtSubscriberName.Text
                PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
                PatientInsurance.Employer = txtEmployer.Text
                PatientInsurance.Group = txtGroup.Text
                PatientInsurance.Phone = mskIPhone1.Text

                If chkPrimary.Checked = True Then
                    PatientInsurance.Primaryflag = True
                Else
                    PatientInsurance.Primaryflag = False
                End If

                Dim mychildnode As TreeNode
                mychildnode = New TreeNode(PatientInsurance.InsuranceName)
                mynode.Nodes.Add(mychildnode)
                key = mychildnode.Index
                If PatientRegistration.SetInsuranceCol(PatientInsurance, key, -1) Then
                    If chkPrimary.Checked = True Then
                        mychildnode.ForeColor = Color.Blue
                        mychildnode.ImageIndex = 1
                        mychildnode.SelectedImageIndex = 1
                        Dim mynode1 As TreeNode
                        For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
                            If Not mynode1 Is mychildnode Then
                                mynode1.ForeColor = Color.Black
                                mynode1.ImageIndex = 2
                                mynode1.SelectedImageIndex = 2
                            End If
                        Next
                    Else
                        mychildnode.ImageIndex = 2
                        mychildnode.SelectedImageIndex = 2
                    End If
                    RefreshInsurance()
                    trInsurance.ExpandAll()
                    trInsurance.Select()
                    key = -1
                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
                End If

            Else 'btn text is "Update"

                If Trim(txtInsuranceNew.Text) = "" Then
                    MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtInsuranceNew.Focus()
                    Exit Sub
                End If

                ''///////if btnadd.text = "Update"
                'phone validations
                If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
                    'MsgBox("Insurance Phone Details Incomplete")
                    MessageBox.Show("Insurance Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
                    mskIPhone1.Focus()
                    Exit Sub
                End If

                'DOB validations
                mskIDOB1.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
                If mskIDOB1.Text <> "" Then
                    mskIDOB1.TextMaskFormat = MaskFormat.IncludeLiterals
                    If Len(mskIDOB1.Text) <> 10 Then
                        MessageBox.Show("Enter Date of Birth", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) 'TabPages(0)
                        mskIDOB1.Focus()
                        Exit Sub
                    End If
                End If
                mskIDOB1.TextMaskFormat = MaskFormat.IncludeLiterals

                Dim PatientInsurance As New ClsPatientInsurance

                If Len(mskIDOB1.Text) = 10 Then
                    If Not IsDate(mskIDOB1.Text) Then
                        MessageBox.Show("Invalid Date of Birth ", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(3) '.TabPages(0)
                        mskIDOB1.Mask = ""
                        'mskDOB1.CtlText = ""
                        mskIDOB1.Mask = "##/##/####"
                        'mskDOB1.Format = "MM/dd/yyyy"
                        mskIDOB1.Focus()
                        Exit Sub
                    Else
                        PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
                        PatientInsurance.Checked = chkSameasPatient.Checked ''True
                    End If
                Else
                    PatientInsurance.Checked = False
                End If

                'commented the code becaz it was giving the Conversion from String “”to date is not valid error message .
                'If Len(mskIDOB1.Text) = 10 Then
                '    If IsDate(CType(mskIDOB1.Text, Date)) Then
                '        '            PatientInsurance.DOB = mskIDOB1.CtlText
                '        PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
                '        PatientInsurance.Checked = True
                '    Else
                '        MsgBox("Invalid Birth Date")

                '        mskIDOB1.Focus()
                '        Exit Sub
                '    End If
                'Else
                '    PatientInsurance.Checked = False
                'End If

                'add the insurance in the PatientInsurance collection
                PatientInsurance.InsuranceId = txtInsuranceNew.Tag
                PatientInsurance.InsuranceName = txtInsuranceNew.Text
                ' PatientInsurance.PatientId = intId
                PatientInsurance.SubscriberId = txtSubscriberID.Text
                PatientInsurance.Subscribername = txtSubscriberName.Text
                PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
                PatientInsurance.Employer = txtEmployer.Text
                PatientInsurance.Group = txtGroup.Text
                PatientInsurance.Phone = mskIPhone1.Text

                If chkPrimary.Checked = True Then
                    PatientInsurance.Primaryflag = True
                Else
                    PatientInsurance.Primaryflag = False
                End If

                mynode.Nodes(key).Text = PatientInsurance.InsuranceName
                If PatientRegistration.SetInsuranceCol(PatientInsurance, key) Then
                    If chkPrimary.Checked = True Then
                        trInsurance.SelectedNode.ForeColor = Color.Blue
                        trInsurance.SelectedNode.ImageIndex = 1
                        trInsurance.SelectedNode.SelectedImageIndex = 1
                        Dim mynode1 As TreeNode
                        For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
                            If Not mynode1 Is trInsurance.SelectedNode Then
                                mynode1.ForeColor = Color.Black
                                mynode1.ImageIndex = 2
                                mynode1.SelectedImageIndex = 2
                            End If
                        Next
                    Else
                        trInsurance.SelectedNode.ForeColor = Color.Black
                        trInsurance.SelectedNode.ImageIndex = 2
                        trInsurance.SelectedNode.SelectedImageIndex = 2
                    End If
                    RefreshInsurance()
                    trInsurance.ExpandAll()
                    trInsurance.Select()
                    key = -1
                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
                End If
            End If ''if btnAdd.text = "add"

            btnAdd.Enabled = False
            '  bTextChangeFlag = True
            '  bTextChangeFlag_insurance = False

            '----------------------------------------------
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'trInsurance.CollapseAll()
        End Try
    End Sub


    'Private Sub btnInsurance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsurance.Click
    '    Try
    '        intStatus = 3
    '        Call loadC1flexgrid()
    '        ' modification in 20070525 - Bipin
    '        'AddControl()

    '        'If Not IsNothing(dgCustomGrid) Then
    '        '    dgCustomGrid.Top = tbPatientRegistration.Top + GroupBox9.Top + txtspousename.Top
    '        '    dgCustomGrid.Left = tbPatientRegistration.Left + trInsurance.Width + 10
    '        '    dgCustomGrid.Height = GroupBox9.Height + GroupBox14.Height
    '        '    dgCustomGrid.Visible = True
    '        '    dgCustomGrid.Width = GroupBox14.Width
    '        '    dgCustomGrid.BringToFront()
    '        '    BindGrid()
    '        'End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    '-----

    'Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
    '    Try


    '        '            /*
    '        '            //////Code commented by sarika 17th aug 07
    '        '            If btnAdd.Text = "Add" Then
    '        '                'If Trim(txtInsuranceName.Text) = "" Then
    '        '                '    'MsgBox("Insurance Name Required")
    '        '                '    MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                '    Exit Sub
    '        '                'End If
    '        '                Dim j = 0

    '        '                If Trim(cmbInsuranceNew.Text) = "" Then
    '        '                    MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                    cmbInsuranceNew.Focus()
    '        '                    Exit Sub
    '        '                End If

    '        '                If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
    '        '                    'MsgBox("Insurance Phone Details Incomplete")
    '        '                    MessageBox.Show("Insurance Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                    'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '                    mskIPhone1.Focus()
    '        '                    Exit Sub
    '        '                End If

    '        '                'If Len(Trim(mskIPhone.ClipText)) > 0 And Len(Trim(mskIPhone.ClipText)) < 10 Then
    '        '                '    MsgBox("Insurance Phone Details Incomplete")
    '        '                '    'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '                '    mskIPhone.Focus()
    '        '                '    Exit Sub
    '        '                'End If

    '        '                'for multiple Insurance selection


    '        '                For j = 0 To cmbInsuranceNew.Items.Count - 1
    '        '                    cmbInsuranceNew.SelectedItem = cmbInsuranceNew.Items.Item(j)

    '        '                    Dim PatientInsurance As New ClsPatientInsurance
    '        '                    'PatientInsurance.InsuranceId = CType(txtInsuranceName.Tag, Long)

    '        '                    ' code modified on 20070605 by bipin
    '        '                    'PatientInsurance.InsuranceName = txtInsuranceName.Text
    '        '                    PatientInsurance.InsuranceName = cmbInsuranceNew.Items.Item(j).ToString

    '        '                    ''''' To get insurance ID 
    '        '                    'PatientInsurance.InsuranceId = InsuranceIDs(j + 1)
    '        '                    ' ''''
    '        '                    'PatientInsurance.InsuranceId = CType(cmbInsuranceNew.Tag, Long)

    '        '                    PatientInsurance.SubscriberId = txtSubscriberID.Text
    '        '                    PatientInsurance.Subscribername = txtSubscriberName.Text
    '        '                    PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
    '        '                    PatientInsurance.Group = txtGroup.Text
    '        '                    'If Len(Trim(mskIPhone.ClipText)) > 10 Then

    '        '                    ' modification on 20070522 by Bipin
    '        '                    PatientInsurance.Phone = mskIPhone1.Text
    '        '                    'PatientInsurance.Phone = mskIPhone.ClipText
    '        '                    'End If
    '        '                    'If dtpIDOB.Checked Then
    '        '                    '    PatientInsurance.Checked = True
    '        '                    'End If

    '        '                    ' modification on 20070522 by Bipin
    '        '                    If Len(mskIDOB1.Text) = 10 Then
    '        '                        'If IsDate(mskIDOB.CtlText) Then
    '        '                        If IsDate(CType(mskIDOB1.Text, Date)) Then
    '        '                            'PatientInsurance.DOB = mskIDOB.CtlText
    '        '                            PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
    '        '                            PatientInsurance.Checked = True
    '        '                        Else
    '        '                            'MsgBox("Invalid Birth Date")

    '        '                            mskIDOB1.Focus()
    '        '                            Exit Sub
    '        '                        End If
    '        '                    Else
    '        '                        PatientInsurance.Checked = False
    '        '                    End If
    '        '                    'If Len(mskIDOB.ClipText) = 8 Then
    '        '                    '    'If IsDate(mskIDOB.CtlText) Then
    '        '                    '    If IsDate(CType(mskIDOB.FormattedText, Date)) Then
    '        '                    '        'PatientInsurance.DOB = mskIDOB.CtlText
    '        '                    '        PatientInsurance.DOB = CType(mskIDOB.FormattedText, Date)
    '        '                    '        PatientInsurance.Checked = True
    '        '                    '    Else
    '        '                    '        MsgBox("Invalid Birth Date")
    '        '                    '        mskIDOB.Focus()
    '        '                    '        Exit Sub
    '        '                    '    End If
    '        '                    'Else
    '        '                    '    PatientInsurance.Checked = False
    '        '                    'End If
    '        '                    'PatientInsurance.DOB = dtpIDOB.Value
    '        '                    PatientInsurance.Employer = txtEmployer.Text
    '        '                    If chkPrimary.Checked Then
    '        '                        PatientInsurance.Primaryflag = True
    '        '                    Else
    '        '                        PatientInsurance.Primaryflag = False
    '        '                    End If
    '        '                    'If ID = 0 Then
    '        '                    If Not CheckNode(PatientInsurance.InsuranceName) Then
    '        '                        'MsgBox("Duplicate Insurance")
    '        '                        'MessageBox.Show("Duplicate Insurance", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                        'RefreshInsurance()
    '        '                        'trInsurance.ExpandAll()
    '        '                        'trInsurance.Select()
    '        '                        'key = -1
    '        '                        'trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        '                        'PatientInsurance = Nothing
    '        '                        'Exit Sub
    '        '                    Else
    '        '                        If key < 0 Then

    '        '                            'Add Insurance 
    '        '                            Dim mychildnode As TreeNode
    '        '                            mychildnode = New TreeNode(PatientInsurance.InsuranceName)
    '        '                            mynode.Nodes.Add(mychildnode)
    '        '                            key = mychildnode.Index
    '        '                            If PatientRegistration.SetInsuranceCol(PatientInsurance, key, -1) Then
    '        '                                If chkPrimary.Checked = True Then
    '        '                                    mychildnode.ForeColor = Color.Blue
    '        '                                    mychildnode.ImageIndex = 1
    '        '                                    mychildnode.SelectedImageIndex = 1
    '        '                                    Dim mynode1 As TreeNode
    '        '                                    For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '        '                                        If Not mynode1 Is mychildnode Then
    '        '                                            mynode1.ForeColor = Color.Black
    '        '                                            mynode1.ImageIndex = 2
    '        '                                            mynode1.SelectedImageIndex = 2
    '        '                                        End If
    '        '                                    Next
    '        '                                Else
    '        '                                    mychildnode.ImageIndex = 2
    '        '                                    mychildnode.SelectedImageIndex = 2
    '        '                                End If
    '        '                                RefreshInsurance()
    '        '                                trInsurance.ExpandAll()
    '        '                                trInsurance.Select()
    '        '                                key = -1
    '        '                                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        '                                '''' To get insurance ID 
    '        '                                ' nCollectionCounter is the counter where only newly added Insurances ID fill into the collection, so this is the separate counter
    '        '                                PatientInsurance.InsuranceId = InsuranceIDs(nCollectionCounter + 1)
    '        '                                nCollectionCounter = nCollectionCounter + 1
    '        '                                ''''
    '        '                            End If

    '        '                        Else
    '        '                            'Modify Insurance
    '        '                            mynode.Nodes(key).Text = PatientInsurance.InsuranceName
    '        '                            If PatientRegistration.SetInsuranceCol(PatientInsurance, key) Then
    '        '                                If chkPrimary.Checked = True Then
    '        '                                    trInsurance.SelectedNode.ForeColor = Color.Blue
    '        '                                    trInsurance.SelectedNode.ImageIndex = 1
    '        '                                    trInsurance.SelectedNode.SelectedImageIndex = 1
    '        '                                    Dim mynode1 As TreeNode
    '        '                                    For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '        '                                        If Not mynode1 Is trInsurance.SelectedNode Then
    '        '                                            mynode1.ForeColor = Color.Black
    '        '                                            mynode1.ImageIndex = 2
    '        '                                            mynode1.SelectedImageIndex = 2
    '        '                                        End If
    '        '                                    Next
    '        '                                Else
    '        '                                    trInsurance.SelectedNode.ForeColor = Color.Black
    '        '                                    trInsurance.SelectedNode.ImageIndex = 2
    '        '                                    trInsurance.SelectedNode.SelectedImageIndex = 2
    '        '                                End If
    '        '                                RefreshInsurance()
    '        '                                trInsurance.ExpandAll()
    '        '                                trInsurance.Select()
    '        '                                key = -1
    '        '                                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        '                                '''' To get insurance ID 
    '        '                                PatientInsurance.InsuranceId = InsuranceIDs(nCollectionCounter + 1)
    '        '                                nCollectionCounter = nCollectionCounter + 1
    '        '                                ''''
    '        '                            End If
    '        '                        End If
    '        '                    End If

    '        '                    PatientInsurance = Nothing
    '        '                Next
    '        '                nCollectionCounter = 0
    '        '            Else
    '        '                'btnAdd.text = "Update
    '        '                Dim j = 0
    '        '                If Trim(cmbInsuranceNew.Text) = "" Then
    '        '                    MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                    cmbInsuranceNew.Focus()
    '        '                    Exit Sub
    '        '                End If

    '        '                If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
    '        '                    'MsgBox("Insurance Phone Details Incomplete")
    '        '                    MessageBox.Show("Insurance Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                    'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '        '                    mskIPhone1.Focus()
    '        '                    Exit Sub
    '        '                End If

    '        '                'cmbInsuranceNew.SelectedItem = cmbInsuranceNew.Items.Item(j)

    '        '                Dim PatientInsurance As New ClsPatientInsurance
    '        '                'PatientInsurance.InsuranceId = CType(txtInsuranceName.Tag, Long)

    '        '                ' code modified on 20070605 by bipin
    '        '                'PatientInsurance.InsuranceName = txtInsuranceName.Text
    '        '                PatientInsurance.InsuranceName = cmbInsuranceNew.Text

    '        '                '''' To get insurance ID 
    '        '                PatientInsurance.InsuranceId = cmbInsuranceNew.Tag
    '        '                ''''
    '        '                'PatientInsurance.InsuranceId = CType(cmbInsuranceNew.Tag, Long)

    '        '                PatientInsurance.SubscriberId = txtSubscriberID.Text
    '        '                PatientInsurance.Subscribername = txtSubscriberName.Text
    '        '                PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
    '        '                PatientInsurance.Group = txtGroup.Text
    '        '                'If Len(Trim(mskIPhone.ClipText)) > 10 Then

    '        '                ' modification on 20070522 by Bipin
    '        '                PatientInsurance.Phone = mskIPhone1.Text
    '        '                'PatientInsurance.Phone = mskIPhone.ClipText
    '        '                'End If
    '        '                'If dtpIDOB.Checked Then
    '        '                '    PatientInsurance.Checked = True
    '        '                'End If

    '        '                ' modification on 20070522 by Bipin
    '        '                If Len(mskIDOB1.Text) = 10 Then
    '        '                    'If IsDate(mskIDOB.CtlText) Then
    '        '                    If IsDate(CType(mskIDOB1.Text, Date)) Then
    '        '                        'PatientInsurance.DOB = mskIDOB.CtlText
    '        '                        PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
    '        '                        PatientInsurance.Checked = True
    '        '                    Else
    '        '                        'MsgBox("Invalid Birth Date")

    '        '                        mskIDOB1.Focus()
    '        '                        Exit Sub
    '        '                    End If
    '        '                Else
    '        '                    PatientInsurance.Checked = False
    '        '                End If

    '        '                PatientInsurance.Employer = txtEmployer.Text
    '        '                If chkPrimary.Checked Then
    '        '                    PatientInsurance.Primaryflag = True
    '        '                Else
    '        '                    PatientInsurance.Primaryflag = False
    '        '                End If
    '        '                'If ID = 0 Then
    '        '                If Not CheckNode(PatientInsurance.InsuranceName) Then
    '        '                    'MsgBox("Duplicate Insurance")
    '        '                    MessageBox.Show("Duplicate Insurance", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        '                    RefreshInsurance()
    '        '                    trInsurance.ExpandAll()
    '        '                    trInsurance.Select()
    '        '                    key = -1
    '        '                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        '                    PatientInsurance = Nothing
    '        '                    Exit Sub
    '        '                Else
    '        '                    If key < 0 Then

    '        '                        'Add Insurance 
    '        '                        Dim mychildnode As TreeNode
    '        '                        mychildnode = New TreeNode(PatientInsurance.InsuranceName)
    '        '                        mynode.Nodes.Add(mychildnode)
    '        '                        key = mychildnode.Index
    '        '                        If PatientRegistration.SetInsuranceCol(PatientInsurance, key, -1) Then
    '        '                            If chkPrimary.Checked = True Then
    '        '                                mychildnode.ForeColor = Color.Blue
    '        '                                mychildnode.ImageIndex = 1
    '        '                                mychildnode.SelectedImageIndex = 1
    '        '                                Dim mynode1 As TreeNode
    '        '                                For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '        '                                    If Not mynode1 Is mychildnode Then
    '        '                                        mynode1.ForeColor = Color.Black
    '        '                                        mynode1.ImageIndex = 2
    '        '                                        mynode1.SelectedImageIndex = 2
    '        '                                    End If
    '        '                                Next
    '        '                            Else
    '        '                                mychildnode.ImageIndex = 2
    '        '                                mychildnode.SelectedImageIndex = 2
    '        '                            End If
    '        '                            RefreshInsurance()
    '        '                            trInsurance.ExpandAll()
    '        '                            trInsurance.Select()
    '        '                            key = -1
    '        '                            trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        '                        End If

    '        '                    Else
    '        '                        'Modify Insurance
    '        '                        mynode.Nodes(key).Text = PatientInsurance.InsuranceName
    '        '                        If PatientRegistration.SetInsuranceCol(PatientInsurance, key) Then
    '        '                            If chkPrimary.Checked = True Then
    '        '                                trInsurance.SelectedNode.ForeColor = Color.Blue
    '        '                                trInsurance.SelectedNode.ImageIndex = 1
    '        '                                trInsurance.SelectedNode.SelectedImageIndex = 1
    '        '                                Dim mynode1 As TreeNode
    '        '                                For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '        '                                    If Not mynode1 Is trInsurance.SelectedNode Then
    '        '                                        mynode1.ForeColor = Color.Black
    '        '                                        mynode1.ImageIndex = 2
    '        '                                        mynode1.SelectedImageIndex = 2
    '        '                                    End If
    '        '                                Next
    '        '                            Else
    '        '                                trInsurance.SelectedNode.ForeColor = Color.Black
    '        '                                trInsurance.SelectedNode.ImageIndex = 2
    '        '                                trInsurance.SelectedNode.SelectedImageIndex = 2
    '        '                            End If
    '        '                            RefreshInsurance()
    '        '                            trInsurance.ExpandAll()
    '        '                            trInsurance.Select()
    '        '                            key = -1
    '        '                            trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '        '                        End If
    '        '                    End If
    '        '                End If

    '        '                PatientInsurance = Nothing
    '        '            End If



    '        '            /////////////////////////////////////////////////////////////////////////////////////////////
    '        '*/


    '        '<><><><><><><>sarika 17th aug 07<><><><><><><><>



    '        If btnAdd.Text = "Add" Then
    '            'insurance name valuidation
    '            'it should not be empty
    '            If Trim(txtInsuranceNew.Text) = "" Then
    '                MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                txtInsuranceNew.Focus()
    '                Exit Sub
    '            End If

    '            If Not CheckNode(txtInsuranceNew.Text) Then
    '                'MsgBox("Duplicate Insurance")
    '                MessageBox.Show("Duplicate Insurance", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                RefreshInsurance()
    '                trInsurance.ExpandAll()
    '                trInsurance.Select()
    '                key = -1
    '                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '                'PatientInsurance = Nothing
    '                Exit Sub
    '            End If

    '            'phone validations
    '            If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
    '                'MsgBox("Insurance Phone Details Incomplete")
    '                MessageBox.Show("Insurance Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '                mskIPhone1.Focus()
    '                Exit Sub
    '            End If

    '            'DOB validations
    '            Dim PatientInsurance As New ClsPatientInsurance

    '            If Len(mskIDOB1.Text) = 10 Then
    '                If IsDate(CType(mskIDOB1.Text, Date)) Then
    '                    '            PatientInsurance.DOB = mskIDOB1.CtlText
    '                    PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
    '                    PatientInsurance.Checked = True
    '                Else
    '                    MsgBox("Invalid Birth Date")

    '                    mskIDOB1.Focus()
    '                    Exit Sub
    '                End If
    '            Else
    '                PatientInsurance.Checked = False
    '            End If

    '            'add the insurance in the PatientInsurance collection
    '            PatientInsurance.InsuranceId = txtInsuranceNew.Tag
    '            PatientInsurance.InsuranceName = txtInsuranceNew.Text
    '            ' PatientInsurance.PatientId = intId
    '            PatientInsurance.SubscriberId = txtSubscriberID.Text
    '            PatientInsurance.Subscribername = txtSubscriberName.Text
    '            PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
    '            PatientInsurance.Employer = txtEmployer.Text
    '            PatientInsurance.Group = txtGroup.Text
    '            PatientInsurance.Phone = mskIPhone1.Text

    '            If chkPrimary.Checked = True Then
    '                PatientInsurance.Primaryflag = True
    '            Else
    '                PatientInsurance.Primaryflag = False
    '            End If

    '            Dim mychildnode As TreeNode
    '            mychildnode = New TreeNode(PatientInsurance.InsuranceName)
    '            mynode.Nodes.Add(mychildnode)
    '            key = mychildnode.Index
    '            If PatientRegistration.SetInsuranceCol(PatientInsurance, key, -1) Then
    '                If chkPrimary.Checked = True Then
    '                    mychildnode.ForeColor = Color.Blue
    '                    mychildnode.ImageIndex = 1
    '                    mychildnode.SelectedImageIndex = 1
    '                    Dim mynode1 As TreeNode
    '                    For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '                        If Not mynode1 Is mychildnode Then
    '                            mynode1.ForeColor = Color.Black
    '                            mynode1.ImageIndex = 2
    '                            mynode1.SelectedImageIndex = 2
    '                        End If
    '                    Next
    '                Else
    '                    mychildnode.ImageIndex = 2
    '                    mychildnode.SelectedImageIndex = 2
    '                End If
    '                RefreshInsurance()
    '                trInsurance.ExpandAll()
    '                trInsurance.Select()
    '                key = -1
    '                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '            End If



    '        Else 'if btnadd.text = update

    '            If Trim(txtInsuranceNew.Text) = "" Then
    '                MessageBox.Show("Insurance Name Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                txtInsuranceNew.Focus()
    '                Exit Sub
    '            End If

    '            ''///////if btnadd.text = "Update"
    '            'phone validations
    '            If Len(Trim(mskIPhone1.Text)) > 0 And Len(Trim(mskIPhone1.Text)) < 10 Then
    '                'MsgBox("Insurance Phone Details Incomplete")
    '                MessageBox.Show("Insurance Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                'tbPatientRegistration.SelectedTab = tbPatientRegistration.TabPages(2)
    '                mskIPhone1.Focus()
    '                Exit Sub
    '            End If

    '            'DOB validations
    '            Dim PatientInsurance As New ClsPatientInsurance

    '            If Len(mskIDOB1.Text) = 10 Then
    '                If IsDate(CType(mskIDOB1.Text, Date)) Then
    '                    '            PatientInsurance.DOB = mskIDOB1.CtlText
    '                    PatientInsurance.DOB = CType(mskIDOB1.Text, Date)
    '                    PatientInsurance.Checked = True
    '                Else
    '                    MsgBox("Invalid Birth Date")

    '                    mskIDOB1.Focus()
    '                    Exit Sub
    '                End If
    '            Else
    '                PatientInsurance.Checked = False
    '            End If

    '            'add the insurance in the PatientInsurance collection
    '            PatientInsurance.InsuranceId = txtInsuranceNew.Tag
    '            PatientInsurance.InsuranceName = txtInsuranceNew.Text
    '            ' PatientInsurance.PatientId = intId
    '            PatientInsurance.SubscriberId = txtSubscriberID.Text
    '            PatientInsurance.Subscribername = txtSubscriberName.Text
    '            PatientInsurance.SubscriberPolicy = txtSubscriberPolicy.Text
    '            PatientInsurance.Employer = txtEmployer.Text
    '            PatientInsurance.Group = txtGroup.Text
    '            PatientInsurance.Phone = mskIPhone1.Text

    '            If chkPrimary.Checked = True Then
    '                PatientInsurance.Primaryflag = True
    '            Else
    '                PatientInsurance.Primaryflag = False
    '            End If

    '            mynode.Nodes(key).Text = PatientInsurance.InsuranceName
    '            If PatientRegistration.SetInsuranceCol(PatientInsurance, key) Then
    '                If chkPrimary.Checked = True Then
    '                    trInsurance.SelectedNode.ForeColor = Color.Blue
    '                    trInsurance.SelectedNode.ImageIndex = 1
    '                    trInsurance.SelectedNode.SelectedImageIndex = 1
    '                    Dim mynode1 As TreeNode
    '                    For Each mynode1 In trInsurance.Nodes.Item(0).Nodes
    '                        If Not mynode1 Is trInsurance.SelectedNode Then
    '                            mynode1.ForeColor = Color.Black
    '                            mynode1.ImageIndex = 2
    '                            mynode1.SelectedImageIndex = 2
    '                        End If
    '                    Next
    '                Else
    '                    trInsurance.SelectedNode.ForeColor = Color.Black
    '                    trInsurance.SelectedNode.ImageIndex = 2
    '                    trInsurance.SelectedNode.SelectedImageIndex = 2
    '                End If
    '                RefreshInsurance()
    '                trInsurance.ExpandAll()
    '                trInsurance.Select()
    '                key = -1
    '                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    '            End If
    '        End If ''if btnAdd.text = "add"


    '        bTextChangeFlag = True
    '        bTextChangeFlag_insurance = False

    '        '----------------------------------------------
    '    Catch ex As SqlClient.SqlException
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'trInsurance.CollapseAll()
    '    End Try
    'End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If btnAdd.Text = "Update" Then
                'If Trim(txtInsuranceName.Text) <> "" Then
                If Trim(txtInsuranceNew.Text) <> "" Then
                    If MessageBox.Show("Are you sure you want to Delete the Selected Patient Insurance", "Patient Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        Try
                            If key >= 0 Then
                                PatientRegistration.DeleteInsurance(key)
                                mynode.Nodes.RemoveAt(key)
                                RefreshInsurance()
                                key = -1
                                trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
                                trInsurance.Select()

                                'sarika 20th aug 07
                                nInsuranceNodeCount = trInsurance.Nodes(0).Nodes.Count
                                '''''''
                                'Else
                                '    txtInsuranceName.Text = ""
                            End If
                        Catch ex As SqlClient.SqlException
                            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefreshI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshI.Click
        RefreshInsurance()
        key = -1
        trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
    End Sub

    Private Sub btnScan_old_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkSameasPatient_CheckedChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSameasPatient.CheckedChanged
        Try
            ' modification on 20070522 by Bipin
            'If cmbPatientStatus.CanSelect = True Then '  .Checked = True Then
            If chkSameasPatient.Checked = True Then
                txtSubscriberName.Text = Trim(txtfname.Text) & " " & Trim(txtmName.Text) & " " & Trim(txtlname.Text)
                txtEmployer.Text = Trim(txtWLocation.Text)
                'dtpIDOB.Value = dtpDOB.Value
                If Len(mskDOB1.Text) = 10 Then
                    mskIDOB1.Text = mskDOB1.Text
                End If
            Else
                txtSubscriberName.Text = ""
                mskIDOB1.Mask = ""
                mskIDOB1.Text = ""
                mskIDOB1.Mask = "##/##/####"
                txtEmployer.Text = ""
                'mskIDOB.Format = "MM/dd/yyyy"
            End If

            'If chkSameasPatient.Checked = True Then
            '    txtSubscriberName.Text = Trim(txtfname.Text) & " " & Trim(txtmName.Text) & " " & Trim(txtlname.Text)
            '    txtEmployer.Text = Trim(txtWLocation.Text)
            '    'dtpIDOB.Value = dtpDOB.Value
            '    If Len(mskDOB.ClipText) = 8 Then
            '        mskIDOB.CtlText = mskDOB.CtlText
            '    End If
            'Else
            '    txtSubscriberName.Text = ""
            '    mskIDOB.Mask = ""
            '    mskIDOB.CtlText = ""
            '    mskIDOB.Mask = "##/##/####"
            '    mskIDOB.Format = "MM/dd/yyyy"
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub chkGuarantor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGuarantor.CheckedChanged
        If chkGuarantor.Checked = True Then
            txtGuarantor.Text = txtfname.Text & " " & txtmName.Text & " " & txtlname.Text
        Else
            txtGuarantor.Text = ""
        End If
    End Sub

    Private Sub txtMother_Zip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMother_Zip.Validating
        If Trim(txtMother_Zip.Text) <> "" Then
            Try
                Dim dt As DataTable
                dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtMother_Zip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtMother_City.Text = dt.Rows(0).Item(0)
                        AreaCode = dt.Rows(0).Item(2)
                        txtMother_County.Text = dt.Rows(0).Item(3)
                        cmbMother_State.Text = dt.Rows(0).Item(1)
                        If Len(Trim(mskMother_Phone1.Text)) < 10 Then
                            mskMother_Phone1.Text = "(" & AreaCode & ")-___-____"
                        ElseIf Len(Trim(mskMother_Phone1.Text)) = 10 Then
                            mskMother_Phone1.Text = "(" & AreaCode & ")-" & Mid(mskMother_Phone1.Text, 4, 3) & "-" & Mid(mskMother_Phone1.Text, 7, 4)
                        End If
                        'If Len(Trim(mskPhone.ClipText)) < 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-___-____"
                        'ElseIf Len(Trim(mskPhone.ClipText)) = 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-" & Mid(mskPhone.ClipText, 4, 3) & "-" & Mid(mskPhone.ClipText, 7, 4)
                        'End If
                    Else
                        txtMother_City.Text = ""
                        txtMother_County.Text = ""
                        cmbMother_State.Text = ""
                        mskMother_Phone1.Mask = ""
                        mskMother_Phone1.Text = ""
                        mskMother_Phone1.Mask = "(###)-###-####"
                    End If

                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtFather_Zip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFather_Zip.Validating
        If Trim(txtFather_Zip.Text) <> "" Then
            Try
                Dim dt As DataTable
                dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtFather_Zip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtFather_City.Text = dt.Rows(0).Item(0)
                        AreaCode = dt.Rows(0).Item(2)
                        txtFather_County.Text = dt.Rows(0).Item(3)
                        cmbFather_State.Text = dt.Rows(0).Item(1)
                        If Len(Trim(mskFather_Phone1.Text)) < 10 Then
                            mskFather_Phone1.Text = "(" & AreaCode & ")-___-____"
                        ElseIf Len(Trim(mskMother_Phone1.Text)) = 10 Then
                            mskFather_Phone1.Text = "(" & AreaCode & ")-" & Mid(mskFather_Phone1.Text, 4, 3) & "-" & Mid(mskFather_Phone1.Text, 7, 4)
                        End If
                        'If Len(Trim(mskPhone.ClipText)) < 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-___-____"
                        'ElseIf Len(Trim(mskPhone.ClipText)) = 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-" & Mid(mskPhone.ClipText, 4, 3) & "-" & Mid(mskPhone.ClipText, 7, 4)
                        'End If
                    Else
                        txtFather_City.Text = ""
                        txtFather_County.Text = ""
                        cmbFather_State.Text = ""
                        mskFather_Phone1.Mask = ""
                        mskFather_Phone1.Text = ""
                        mskFather_Phone1.Mask = "(###)-###-####"
                    End If

                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtGuardian_Zip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtGuardian_Zip.Validating
        If Trim(txtGuardian_Zip.Text) <> "" Then
            Try
                Dim dt As DataTable
                dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtGuardian_Zip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtGuardian_City.Text = dt.Rows(0).Item(0)
                        AreaCode = dt.Rows(0).Item(2)
                        txtGuardian_County.Text = dt.Rows(0).Item(3)
                        cmbGuardian_State.Text = dt.Rows(0).Item(1)
                        If Len(Trim(mskGuardian_Phone1.Text)) < 10 Then
                            mskGuardian_Phone1.Text = "(" & AreaCode & ")-___-____"
                        ElseIf Len(Trim(mskMother_Phone1.Text)) = 10 Then
                            mskGuardian_Phone1.Text = "(" & AreaCode & ")-" & Mid(mskGuardian_Phone1.Text, 4, 3) & "-" & Mid(mskGuardian_Phone1.Text, 7, 4)
                        End If
                        'If Len(Trim(mskPhone.ClipText)) < 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-___-____"
                        'ElseIf Len(Trim(mskPhone.ClipText)) = 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-" & Mid(mskPhone.ClipText, 4, 3) & "-" & Mid(mskPhone.ClipText, 7, 4)
                        'End If
                    Else
                        txtGuardian_City.Text = ""
                        txtGuardian_County.Text = ""
                        cmbGuardian_State.Text = ""
                        mskGuardian_Phone1.Mask = ""
                        mskGuardian_Phone1.Text = ""
                        mskGuardian_Phone1.Mask = "(###)-###-####"
                    End If

                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


    'Private Sub ButtonX9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim frm1 As New frmContactMst(True, "")
    '        frm1.ShowDialog()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Public Function visibleBtns(ByVal flg As Boolean)
        If flg = False Then
            btnOK.Visible = False
            btnClose.Visible = False
            btnPrint.Visible = False
        Else
            btnOK.Visible = True
            btnClose.Visible = True
            btnPrint.Visible = True
        End If
    End Function

    Private Sub txtwZip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtwZip.Validating
        If Trim(txtwZip.Text) <> "" Then
            Try
                Dim dt As DataTable
                dt = PatientRegistration.FetchAddressInfo(Int64.Parse(Trim(txtwZip.Text)))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        txtwCity.Text = dt.Rows(0).Item(0)
                        AreaCode = dt.Rows(0).Item(2)
                        'txtFather_County.Text = dt.Rows(0).Item(3)
                        cmbwState.Text = dt.Rows(0).Item(1)
                        If Len(Trim(mskwPhone1.Text)) < 10 Then
                            mskwPhone1.Text = "(" & AreaCode & ")-___-____"
                        ElseIf Len(Trim(mskwPhone1.Text)) = 10 Then
                            mskwPhone1.Text = "(" & AreaCode & ")-" & Mid(mskwPhone1.Text, 4, 3) & "-" & Mid(mskwPhone1.Text, 7, 4)
                        End If
                        'If Len(Trim(mskPhone.ClipText)) < 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-___-____"
                        'ElseIf Len(Trim(mskPhone.ClipText)) = 10 Then
                        '    mskPhone.CtlText = "(" & AreaCode & ")-" & Mid(mskPhone.ClipText, 4, 3) & "-" & Mid(mskPhone.ClipText, 7, 4)
                        'End If
                    Else
                        txtwCity.Text = ""
                        'txtwCounty.Text = ""
                        cmbwState.Text = ""
                        mskwPhone1.Mask = ""
                        mskwPhone1.Text = ""
                        mskwPhone1.Mask = "(###)-###-####"
                    End If

                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtPharmacy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPharmacy.TextChanged
        If txtPharmacy.Text.Trim.Length = 0 Then
            btnPharmacyClose.Enabled = False
        Else
            btnPharmacyClose.Enabled = True
        End If
    End Sub

    Private Sub cmbReferrals_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbReferrals.SelectedIndexChanged
        If cmbReferrals.Text.Trim.Length = 0 Then
            btnReferralsClose.Enabled = False
        Else
            btnReferralsClose.Enabled = True
        End If
    End Sub

    Private Sub txtpcp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpcp.TextChanged
        If txtpcp.Text.Trim.Length = 0 Then
            btnPCPClose.Enabled = False
        Else
            btnPCPClose.Enabled = True
        End If
    End Sub

    'Private Sub cmbInsuranceNew_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbInsuranceNew.SelectedIndexChanged
    '    If cmbInsuranceNew.Text.Trim.Length = 0 Then
    '        btnCloseInsuranceName.Enabled = False
    '    Else
    '        btnCloseInsuranceName.Enabled = True
    '    End If
    'End Sub

    Private Sub btnDisabpleEnable()
        '' pharmacy
        If txtPharmacy.Text.Trim.Length = 0 Then
            btnPharmacyClose.Enabled = False
        Else
            btnPharmacyClose.Enabled = True
        End If

        '' referals
        If cmbReferrals.Text.Trim.Length = 0 Then
            btnReferralsClose.Enabled = False
        Else
            btnReferralsClose.Enabled = True
        End If

        'Physicians
        If txtpcp.Text.Trim.Length = 0 Then
            btnPCPClose.Enabled = False
        Else
            btnPCPClose.Enabled = True
        End If

        'insurance
        If txtInsuranceNew.Text.Trim.Length = 0 Then
            btnCloseInsuranceName.Enabled = False
        Else
            btnCloseInsuranceName.Enabled = True
        End If
    End Sub

    Private Sub btnPharmacyBrowse_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPharmacyBrowse.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnPharmacyBrowse, "Browse Pharmacy")
    End Sub

    Private Sub btnPharmacyClose_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPharmacyClose.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnPharmacyClose, "Clear Pharmacy")
    End Sub

    Private Sub btnReferralsBrowse_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReferralsBrowse.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnReferralsBrowse, "Browse Referrals")
    End Sub

    Private Sub btnReferralsClose_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReferralsClose.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnReferralsClose, "Clear Referrals")
    End Sub

    Private Sub btnPCPBrowse_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCPBrowse.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnPCPBrowse, "Browse Physician")
    End Sub

    Private Sub btnPCPClose_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCPClose.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnPCPClose, "Clear Physician")
    End Sub

    Private Sub btnInsurance_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsurance.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnInsurance, "Browse Insurance")
    End Sub

    Private Sub ButtonX18_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseInsuranceName.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnCloseInsuranceName, "Clear Insurance")
    End Sub

    Private Sub mskRegistrationdate1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mskRegistrationdate1.Validating
        mskRegistrationdate1.TextMaskFormat = MaskFormat.IncludeLiterals
        If mskRegistrationdate1.Text.Trim.Length > 4 Then
            If IsDate(mskRegistrationdate1.Text) Then

                Dim tempDate As DateTime
                tempDate = Format(CType(mskRegistrationdate1.Text, Date), "MM/dd/yyyy")

                'If tempDate.Year < 1900 Then
                If tempDate.Year < 1850 Then
                    MessageBox.Show("Date of Registration should not less than year 1850 ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskRegistrationdate1.Focus()
                    mskRegistrationdate1.Clear()
                    Exit Sub
                End If

                If tempDate > System.DateTime.Now.Date Then
                    MessageBox.Show("Date of Registration should not greater than todays date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskRegistrationdate1.Focus()
                    mskRegistrationdate1.Clear()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Invalid Registration date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskRegistrationdate1.Focus()
                mskRegistrationdate1.Clear()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub mskDOB1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mskDOB1.Validating
        If mskDOB1.Text.Trim.Length > 4 And mskDOB1.Text.Trim.Length < 11 Then
            If IsDate(mskDOB1.Text) Then
                Dim tempDate As DateTime
                tempDate = Format(CType(mskDOB1.Text, Date), "MM/dd/yyyy")
                'If tempDate.Year < 1900 Then
                If tempDate.Year < 1850 Then
                    MessageBox.Show("Date of birth should not less than year 1850 ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskDOB1.Clear()
                    mskDOB1.Focus()
                    Exit Sub
                End If

                If tempDate > System.DateTime.Now.Date Then
                    MessageBox.Show("Date of birth should not greater than todays date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskDOB1.Clear()
                    mskDOB1.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Invalid Birth date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskDOB1.Clear()
                mskDOB1.Focus()
                'Exit Sub
            End If
            'Else
            '    MessageBox.Show("Enter Date of Birth", "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    mskDOB1.Focus()
            '    Exit Sub
        End If
    End Sub

    Private Sub mskInjurydate1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mskInjurydate1.Validating
        mskInjurydate1.TextMaskFormat = MaskFormat.IncludeLiterals
        If mskInjurydate1.Text.Trim.Length > 4 Then
            If IsDate(mskInjurydate1.Text) Then

                Dim tempDate As DateTime
                tempDate = Format(CType(mskInjurydate1.Text, Date), "MM/dd/yyyy")

                'If tempDate.Year < 1900 Then
                If tempDate.Year < 1850 Then
                    MessageBox.Show("Date of Injury should not less than year 1850 ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskInjurydate1.Focus()
                    Exit Sub
                End If

                If tempDate > System.DateTime.Now.Date Then
                    MessageBox.Show("Date of Injury should not greater than todays date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskInjurydate1.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Invalid Injury date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskInjurydate1.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub mskSurgeryDate1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mskSurgeryDate1.Validating
        mskSurgeryDate1.TextMaskFormat = MaskFormat.IncludeLiterals
        If mskSurgeryDate1.Text.Trim.Length > 4 Then
            If IsDate(mskSurgeryDate1.Text) Then

                Dim tempDate As DateTime
                tempDate = Format(CType(mskSurgeryDate1.Text, Date), "MM/dd/yyyy")

                'If tempDate.Year < 1900 Then
                If tempDate.Year < 1850 Then
                    MessageBox.Show("Date of Surgery should not less than year 1850 ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskSurgeryDate1.Focus()
                    Exit Sub
                End If

                If tempDate > System.DateTime.Now.Date Then
                    MessageBox.Show("Date of Surgery should not greater than todays date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskSurgeryDate1.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Invalid Surgery date ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                mskSurgeryDate1.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub MskSSn1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MskSSn1.Validating
        'sarika ssn change 7th may 08
        'If Len(Trim(MskSSn1.Text)) <> 9 And Val(MskSSn1.Text) <> 0 Then
        If Len(Trim(MskSSn1.Text)) < 9 And (Val(MskSSn1.Text) <> 0 Or Len(Trim(MskSSn1.Text)) > 0) Then
            '---ssn change
            MessageBox.Show("Invalid SSN Number", "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Information)
            tbPatientRegistration.SelectedTab = tbPatientRegistration.Tabs.Item(0) '.TabPages(0)
            MskSSn1.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub optBrowse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBrowse.CheckedChanged
        'If optBrowse.Checked = True Then
        '    btnStart.Visible = True
        '    btnCapture.Visible = False
        'Else
        '    btnStart.Visible = False
        '    btnCapture.Visible = True
        'End If
        btnCapture.Visible = False
    End Sub

    Private Sub optWebCam_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWebCam.CheckedChanged
        'If optWebCam.Checked = True Then
        '    btnStart.Visible = False
        '    btnCapture.Visible = True
        'Else
        '    btnStart.Visible = True
        '    btnCapture.Visible = False
        'End If
        btnCapture.Visible = True
    End Sub

    Private Sub mnuPrimary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrimary.Click
        Try
            If key <> -1 Then
                PatientRegistration.SetasPrimary(key, True)
                chkPrimary.Checked = True
                trInsurance.SelectedNode.ForeColor = Color.Blue
                trInsurance.SelectedNode.ImageIndex = 1
                trInsurance.SelectedNode.SelectedImageIndex = 1
                Dim mynode As TreeNode
                For Each mynode In trInsurance.Nodes.Item(0).Nodes
                    If Not mynode Is trInsurance.SelectedNode Then
                        mynode.ForeColor = Color.Black
                        mynode.ImageIndex = 2
                        mynode.SelectedImageIndex = 2
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        Try
            If key >= 0 Then
                PatientRegistration.DeleteInsurance(key)
                If trInsurance.SelectedNode.GetNodeCount(False) = 0 Then
                    trInsurance.SelectedNode.Remove()
                    RefreshInsurance()
                    key = -1
                    trInsurance.SelectedNode = trInsurance.Nodes.Item(0)
                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtfname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtfname.Validating
        'If txtfname.Text.Trim.Length = 0 Then
        '    btnOK.Enabled = False
        'Else
        '    btnOK.Enabled = True
        'End If
    End Sub

    Private Sub txtPatientCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPatientCode.TextChanged
        If txtPatientCode.Text.Trim.Length > 0 Then
            bTextChangeFlag = True
        Else
            bTextChangeFlag = False
        End If
    End Sub

    Private Sub mskDOB1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskDOB1.TextChanged
        'mskDOB1.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
        If mskDOB1.Text.Trim.Length > 4 Then
            bTextChangeFlag = True
        Else
            bTextChangeFlag = False
        End If
        'mskDOB1.TextMaskFormat = MaskFormat.IncludeLiterals
    End Sub

    'code commented by sarika 6th nov 07
    ' Function for Checking Data of array with controls existing data
    'Public Function IsDataChangeModifyPatient() As Boolean
    '    'GetData()
    '    ' function is checking the dataBase value with existing controls values where set the flag 
    '    'if differences found
    '    ' Every 
    '    If Not _arrayGetData Is Nothing Then
    '        If _arrayGetData.Count > 0 Then

    '            If Not txtPatientCode.Text.Trim = _arrayGetData.Item(0).trim.ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtfname.Text.Trim = _arrayGetData.Item(1).trim.ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtmName.Text.Trim = _arrayGetData.Item(2).trim.ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtlname.Text.Trim = _arrayGetData.Item(3).trim.ToString Then
    '                bTextChangeFlag = True

    '                'ElseIf Not dtpDOB.Checked = _arrayGetData.Item(5) Then
    '            ElseIf Not mskDOB1.Text = _arrayGetData.Item(5) Then
    '                bTextChangeFlag = True

    '                'ElseIf Not "Male" = _arrayGetData.Item(6).ToString.Trim Or Not "Female" = _arrayGetData.Item(6).ToString.Trim Or Not "Other" = _arrayGetData.Item(6).ToString.Trim Then

    '            ElseIf rbGender1.Checked = True And "Male" <> _arrayGetData.Item(6).ToString.Trim Then
    '                'Arrlist.Add("Male")
    '                bTextChangeFlag = True
    '            ElseIf rbGender2.Checked = True And "Female" <> _arrayGetData.Item(6).ToString.Trim Then
    '                'Arrlist.Add("Female")
    '                bTextChangeFlag = True
    '            ElseIf rbGender3.Checked = True And "Other" <> _arrayGetData.Item(6).ToString.Trim Then
    '                'Arrlist.Add("Other")
    '                bTextChangeFlag = True

    '            ElseIf Not cmbMaritalstatus.Text.Trim = _arrayGetData.Item(7).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtAddress1.Text.Trim = _arrayGetData.Item(8).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtAddress2.Text.Trim = _arrayGetData.Item(9).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtCity.Text.Trim = _arrayGetData.Item(10).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbState.Text.Trim = _arrayGetData.Item(11).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtZip.Text.Trim = _arrayGetData.Item(12).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtCounty.Text.Trim = _arrayGetData.Item(13).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskPhone1.Text = _arrayGetData.Item(14).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskMobile1.Text = _arrayGetData.Item(15).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtemail.Text.Trim = _arrayGetData.Item(16).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtfax.Text.Trim = _arrayGetData.Item(17).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtOccupation.Text.Trim = _arrayGetData.Item(18).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbEmploymentStatus.Text.Trim = _arrayGetData.Item(19).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtWLocation.Text.Trim = _arrayGetData.Item(20).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtwAddress1.Text.Trim = _arrayGetData.Item(21).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtwAddress2.Text.Trim = _arrayGetData.Item(22).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtwCity.Text.Trim = _arrayGetData.Item(23).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbwState.Text.Trim = _arrayGetData.Item(24).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtwZip.Text.Trim = _arrayGetData.Item(25).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskwPhone1.Text.Trim = _arrayGetData.Item(26).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtwFax.Text.Trim = _arrayGetData.Item(27).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtInsuranceNotes.Text = _arrayGetData.Item(28).ToString.Trim Then
    '                bTextChangeFlag = True

    '                'ElseIf Not txtpcp.Text.Trim = _arrayGetData.Item(29).trim.ToString Then
    '                '    bTextChangeFlag = True

    '            ElseIf Not txtGuarantor.Text.Trim = _arrayGetData.Item(30).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtspousename.Text.Trim = _arrayGetData.Item(31).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskspousePhone1.Text = _arrayGetData.Item(32).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbRace.Text = _arrayGetData.Item(33).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbPatientStatus.Text = _arrayGetData.Item(34).ToString Then
    '                bTextChangeFlag = True

    '                'ElseIf Not cmbProvider.Text = _arrayGetData.Item(37).trim.ToString Then
    '            ElseIf Not cmbProvider.SelectedValue.ToString = _arrayGetData.Item(35).ToString Then
    '                bTextChangeFlag = True
    '                '    Arrlist.Item(37))  having provider name  
    '                '    cmbProvider.Text   
    '                'Else
    '                '    cmbProvider.Text   CType(Arrlist.Item(37), System.String)
    '                'End  

    '                'ElseIf Not txtPharmacy.Text.Trim = _arrayGetData.Item(36).ToString.Trim Then
    '                '    bTextChangeFlag = True

    '            ElseIf Not txtPharmacy.Text.Trim = _arrayGetData.Item(38).ToString.Trim Then
    '                bTextChangeFlag = True

    '                '    picFinalPatient.Image(Nothing) = _arrayGetData.Item(39)
    '            ElseIf chkDateChanged(_arrayGetData.Item(40), mskRegistrationdate1) = True Then  '''_arrayGetData.Item(40).ToString.Length > 0 Then

    '                'sarika 1st nov 07
    '                'code added 
    '                bTextChangeFlag = True

    '                'code commented
    '                'If IsDBNull(_arrayGetData.Item(40)) = False Then
    '                '    If _arrayGetData.Item(40).ToString.Trim <> "" Then
    '                '        'code modified by sarika 31st oct 07
    '                '        If Not mskRegistrationdate1.Text = Format(CType(_arrayGetData.Item(40), Date), "MM/dd/yyyy") Then
    '                '            bTextChangeFlag = True
    '                '        End If
    '                '    End If
    '                'End If
    '                '------
    '            ElseIf Not txtpcp.Text.Trim = _arrayGetData.Item(41).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf chkDateChanged(_arrayGetData.Item(42), mskInjurydate1) = True Then  '' _arrayGetData.Item(42).ToString.Length > 0 Then
    '                'sarika 1st nov 07
    '                'code added 
    '                bTextChangeFlag = True

    '                'code commented
    '                'If IsDBNull(_arrayGetData.Item(42)) = False Then
    '                '    If _arrayGetData.Item(42).ToString.Trim <> "" Then
    '                '        'code modified by sarika 31st oct 07
    '                '        If Not mskInjurydate1.Text = Format(CType(_arrayGetData.Item(42), Date), "MM/dd/yyyy") Then
    '                '            bTextChangeFlag = True
    '                '        End If
    '                '    End If
    '                'Else
    '                '    '' If DB Null and if the Date is Entered then Set Flag True
    '                '    If mskInjurydate1.MaskCompleted = True Then
    '                '        bTextChangeFlag = True
    '                '    End If
    '                'End If

    '            ElseIf chkDateChanged(_arrayGetData.Item(43), mskSurgeryDate1) = True Then   '' _arrayGetData.Item(43).ToString.Length > 0 Then
    '                'sarika 1st nov 07
    '                'code added 

    '                bTextChangeFlag = True

    '                'code commented

    '                'If IsDBNull(_arrayGetData.Item(43)) = False Then
    '                '    If _arrayGetData.Item(43).ToString.Trim <> "" Then
    '                '        'code modified by sarika 31st oct 07
    '                '        If Not mskSurgeryDate1.Text = Format(CType(_arrayGetData.Item(43), Date), "MM/dd/yyyy") Then
    '                '            bTextChangeFlag = True
    '                '        End If
    '                '    End If
    '                'End If

    '            ElseIf Not cmbDominance.Text.Trim = _arrayGetData.Item(44).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbLocation.Text.Trim = _arrayGetData.Item(45).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_fName.Text.Trim = _arrayGetData.Item(46).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_mName.Text.Trim = _arrayGetData.Item(47).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_lName.Text.Trim = _arrayGetData.Item(48).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_Address1.Text.Trim = _arrayGetData.Item(49).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_Address2.Text.Trim = _arrayGetData.Item(50).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_City.Text.Trim = _arrayGetData.Item(51).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbMother_State.Text = _arrayGetData.Item(52).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_Zip.Text.Trim = _arrayGetData.Item(53).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_County.Text = _arrayGetData.Item(54).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskMother_Phone1.Text = _arrayGetData.Item(55).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskMother_Mobile1.Text = _arrayGetData.Item(56).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_Fax.Text.Trim = _arrayGetData.Item(57).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtMother_Email.Text.Trim = _arrayGetData.Item(58).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_fName.Text.Trim = _arrayGetData.Item(59).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_mName.Text.Trim = _arrayGetData.Item(60).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_lName.Text.Trim = _arrayGetData.Item(61).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_Address1.Text.Trim = _arrayGetData.Item(62).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_Address2.Text.Trim = _arrayGetData.Item(63).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_City.Text.Trim = _arrayGetData.Item(64).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbFather_State.Text = _arrayGetData.Item(65).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_Zip.Text.Trim = _arrayGetData.Item(66).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_County.Text = _arrayGetData.Item(67).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskFather_Phone1.Text.Trim = _arrayGetData.Item(68).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskFather_Mobile1.Text = _arrayGetData.Item(69).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_Fax.Text.Trim = _arrayGetData.Item(70).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtFather_Email.Text.Trim = _arrayGetData.Item(71).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_fName.Text = _arrayGetData.Item(72).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_mName.Text.Trim = _arrayGetData.Item(73).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_lName.Text.Trim = _arrayGetData.Item(74).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_Address1.Text.Trim = _arrayGetData.Item(75).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_Address2.Text.Trim = _arrayGetData.Item(76).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_City.Text.Trim = _arrayGetData.Item(77).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not cmbGuardian_State.Text.Trim = _arrayGetData.Item(78).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_Zip.Text.Trim = _arrayGetData.Item(79).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_County.Text.Trim = _arrayGetData.Item(80).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskGuardian_Phone1.Text = _arrayGetData.Item(81).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not mskGuardian_Mobile1.Text = _arrayGetData.Item(82).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_Fax.Text.Trim = _arrayGetData.Item(83).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not txtGuardian_Email.Text.Trim = _arrayGetData.Item(84).ToString.Trim Then
    '                bTextChangeFlag = True

    '            ElseIf Not chkDirective.Checked = _arrayGetData.Item(85).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not chkExemptFromRpt.Checked = _arrayGetData.Item(86).ToString Then
    '                bTextChangeFlag = True

    '            ElseIf Not _arrayGetData.Item(4).ToString = 0 Then
    '                If Not MskSSn1.Text.Trim = _arrayGetData.Item(4).ToString Then
    '                    bTextChangeFlag = True
    '                End If
    '            Else
    '                bTextChangeFlag = False
    '            End If

    '            ' for Referals modification check
    '            Dim nReffCounter As Integer = 0
    '            If Not cmbReferrals.Items.Count = _arrReferals.Count Then
    '                bTextChangeFlag = True
    '                Exit Function
    '            Else
    '                For nReffCounter = 0 To cmbReferrals.Items.Count - 1
    '                    If Not cmbReferrals.Items(nReffCounter).ToString = _arrReferals.Item(nReffCounter).ToString Then
    '                        bTextChangeFlag = True
    '                        Exit Function
    '                    End If
    '                Next
    '            End If
    '            ''''

    '            ' for Insurance Modification check
    '            ' check for the total number of Insurance and currently added Insurance list
    '            If Not nInsuranceNodeCount = trInsurance.Nodes(0).Nodes.Count Then
    '                bTextChangeFlag_insurance = True
    '                '' Commented on 20071101
    '                'MessageBox.Show("Please click on Add/Update Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                ''
    '                Return bTextChangeFlag_insurance
    '                ' Exit Function
    '            End If

    '            ' check for newly added Insurance in Insurance combobox
    '            'If cmbInsuranceNew.Items.Count > 1 Then
    '            '    bTextChangeFlag_insurance = True
    '            '    ChangeInsuranceDisplayMsg()
    '            'End If

    '            ' Check individual elements of the Insurance tree node
    '            If Not strInsuranceText = txtInsuranceNew.Text Then

    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not strSubPolicy = txtSubscriberPolicy.Text Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not strSubId = txtSubscriberID.Text Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not strSubName = txtSubscriberName.Text Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not strEmployer = txtEmployer.Text Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not nPhone = mskIPhone1.Text Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not strGroup = txtGroup.Text Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf Not chkPrimaryOrNot = chkPrimary.Checked Then
    '                bTextChangeFlag_insurance = True
    '                ChangeInsuranceDisplayMsg()

    '            ElseIf mskIDOB1.Text.Trim.Length > 4 Then
    '                If Not dtDob = mskIDOB1.Text Then
    '                    bTextChangeFlag_insurance = True
    '                    ChangeInsuranceDisplayMsg()
    '                End If
    '            End If
    '            ''''
    '            End If
    '    End If


    '    Return bTextChangeFlag_insurance
    'End Function
    '---

    'code added by sarika 6th nov  07
    Public Function IsDataChangeModifyPatient() As Boolean
        'GetData()
        ' function is checking the dataBase value with existing controls values where set the flag 
        'if differences found
        ' Every 
        If Not _arrayGetData Is Nothing Then
            If _arrayGetData.Count > 0 Then

                If Not txtPatientCode.Text.Trim = _arrayGetData.Item(0).trim.ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtfname.Text.Trim = _arrayGetData.Item(1).trim.ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtmName.Text.Trim = _arrayGetData.Item(2).trim.ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtlname.Text.Trim = _arrayGetData.Item(3).trim.ToString Then
                    bTextChangeFlag = True

                    'ElseIf Not dtpDOB.Checked = _arrayGetData.Item(5) Then

                    'sarika Date Of Birth 7th May 08
                    'ElseIf mskDOB1.Text.Trim = "/  /" Then
                    '    bTextChangeFlag = True


                ElseIf Not mskDOB1.Text.Trim = Convert.ToDateTime((_arrayGetData.Item(5)).Date).ToString("MM/dd/yyyy") Then
                    'ElseIf Not mskDOB1.Text.Trim = _arrayGetData.Item(5).ToString("MM/dd/yyyy") Then
                    bTextChangeFlag = True


                    'ElseIf Not mskDOB1.Text = _arrayGetData.Item(5) Then
                    '    bTextChangeFlag = True

                    'ElseIf Not "Male" = _arrayGetData.Item(6).ToString.Trim Or Not "Female" = _arrayGetData.Item(6).ToString.Trim Or Not "Other" = _arrayGetData.Item(6).ToString.Trim Then
                    '---------Date Of Birth


                ElseIf rbGender1.Checked = True And "Male" <> _arrayGetData.Item(6).ToString.Trim Then
                    'Arrlist.Add("Male")
                    bTextChangeFlag = True
                ElseIf rbGender2.Checked = True And "Female" <> _arrayGetData.Item(6).ToString.Trim Then
                    'Arrlist.Add("Female")
                    bTextChangeFlag = True
                ElseIf rbGender3.Checked = True And "Other" <> _arrayGetData.Item(6).ToString.Trim Then
                    'Arrlist.Add("Other")
                    bTextChangeFlag = True

                ElseIf Not cmbMaritalstatus.Text.Trim = _arrayGetData.Item(7).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtAddress1.Text.Trim = _arrayGetData.Item(8).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtAddress2.Text.Trim = _arrayGetData.Item(9).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtCity.Text.Trim = _arrayGetData.Item(10).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not cmbState.Text.Trim = _arrayGetData.Item(11).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtZip.Text.Trim = _arrayGetData.Item(12).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtCounty.Text.Trim = _arrayGetData.Item(13).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not mskPhone1.Text = _arrayGetData.Item(14).ToString Then
                    bTextChangeFlag = True

                ElseIf Not mskMobile1.Text = _arrayGetData.Item(15).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtemail.Text.Trim = _arrayGetData.Item(16).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtfax.Text.Trim = _arrayGetData.Item(17).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtOccupation.Text.Trim = _arrayGetData.Item(18).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not cmbEmploymentStatus.Text.Trim = _arrayGetData.Item(19).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtWLocation.Text.Trim = _arrayGetData.Item(20).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtwAddress1.Text.Trim = _arrayGetData.Item(21).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtwAddress2.Text.Trim = _arrayGetData.Item(22).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtwCity.Text.Trim = _arrayGetData.Item(23).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not cmbwState.Text.Trim = _arrayGetData.Item(24).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtwZip.Text.Trim = _arrayGetData.Item(25).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not mskwPhone1.Text.Trim = _arrayGetData.Item(26).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtwFax.Text.Trim = _arrayGetData.Item(27).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtInsuranceNotes.Text = _arrayGetData.Item(28).ToString.Trim Then
                    bTextChangeFlag = True

                    'ElseIf Not txtpcp.Text.Trim = _arrayGetData.Item(29).trim.ToString Then
                    '    bTextChangeFlag = True

                ElseIf Not txtGuarantor.Text.Trim = _arrayGetData.Item(30).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtspousename.Text.Trim = _arrayGetData.Item(31).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not mskspousePhone1.Text = _arrayGetData.Item(32).ToString Then
                    bTextChangeFlag = True

                ElseIf Not cmbRace.Text = _arrayGetData.Item(33).ToString Then
                    bTextChangeFlag = True

                ElseIf Not cmbPatientStatus.Text = _arrayGetData.Item(34).ToString Then
                    bTextChangeFlag = True

                    'ElseIf Not cmbProvider.Text = _arrayGetData.Item(37).trim.ToString Then
                ElseIf Not cmbProvider.SelectedValue.ToString = _arrayGetData.Item(35).ToString Then
                    bTextChangeFlag = True


                ElseIf Not txtPharmacy.Text.Trim = _arrayGetData.Item(38).ToString.Trim Then
                    bTextChangeFlag = True


                ElseIf chkDateChanged(_arrayGetData.Item(40), mskRegistrationdate1) = True Then  '''_arrayGetData.Item(40).ToString.Length > 0 Then

                    'sarika 1st nov 07
                    'code added 
                    bTextChangeFlag = True


                ElseIf Not txtpcp.Text.Trim = _arrayGetData.Item(41).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf chkDateChanged(_arrayGetData.Item(42), mskInjurydate1) = True Then  '' _arrayGetData.Item(42).ToString.Length > 0 Then
                    'sarika 1st nov 07
                    'code added 
                    bTextChangeFlag = True



                ElseIf chkDateChanged(_arrayGetData.Item(43), mskSurgeryDate1) = True Then   '' _arrayGetData.Item(43).ToString.Length > 0 Then
                    'sarika 1st nov 07
                    'code added 

                    bTextChangeFlag = True

                    'code commented

                ElseIf Not cmbDominance.Text.Trim = _arrayGetData.Item(44).ToString Then
                    bTextChangeFlag = True

                ElseIf Not cmbLocation.Text.Trim = _arrayGetData.Item(45).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_fName.Text.Trim = _arrayGetData.Item(46).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_mName.Text.Trim = _arrayGetData.Item(47).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_lName.Text.Trim = _arrayGetData.Item(48).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_Address1.Text.Trim = _arrayGetData.Item(49).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_Address2.Text.Trim = _arrayGetData.Item(50).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_City.Text.Trim = _arrayGetData.Item(51).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not cmbMother_State.Text = _arrayGetData.Item(52).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_Zip.Text.Trim = _arrayGetData.Item(53).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_County.Text = _arrayGetData.Item(54).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not mskMother_Phone1.Text = _arrayGetData.Item(55).ToString Then
                    bTextChangeFlag = True

                ElseIf Not mskMother_Mobile1.Text = _arrayGetData.Item(56).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_Fax.Text.Trim = _arrayGetData.Item(57).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtMother_Email.Text.Trim = _arrayGetData.Item(58).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_fName.Text.Trim = _arrayGetData.Item(59).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_mName.Text.Trim = _arrayGetData.Item(60).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_lName.Text.Trim = _arrayGetData.Item(61).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_Address1.Text.Trim = _arrayGetData.Item(62).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_Address2.Text.Trim = _arrayGetData.Item(63).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_City.Text.Trim = _arrayGetData.Item(64).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not cmbFather_State.Text = _arrayGetData.Item(65).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_Zip.Text.Trim = _arrayGetData.Item(66).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_County.Text = _arrayGetData.Item(67).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not mskFather_Phone1.Text.Trim = _arrayGetData.Item(68).ToString Then
                    bTextChangeFlag = True

                ElseIf Not mskFather_Mobile1.Text = _arrayGetData.Item(69).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_Fax.Text.Trim = _arrayGetData.Item(70).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtFather_Email.Text.Trim = _arrayGetData.Item(71).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_fName.Text = _arrayGetData.Item(72).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_mName.Text.Trim = _arrayGetData.Item(73).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_lName.Text.Trim = _arrayGetData.Item(74).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_Address1.Text.Trim = _arrayGetData.Item(75).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_Address2.Text.Trim = _arrayGetData.Item(76).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_City.Text.Trim = _arrayGetData.Item(77).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not cmbGuardian_State.Text.Trim = _arrayGetData.Item(78).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_Zip.Text.Trim = _arrayGetData.Item(79).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_County.Text.Trim = _arrayGetData.Item(80).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not mskGuardian_Phone1.Text = _arrayGetData.Item(81).ToString Then
                    bTextChangeFlag = True

                ElseIf Not mskGuardian_Mobile1.Text = _arrayGetData.Item(82).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_Fax.Text.Trim = _arrayGetData.Item(83).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not txtGuardian_Email.Text.Trim = _arrayGetData.Item(84).ToString.Trim Then
                    bTextChangeFlag = True

                ElseIf Not chkDirective.Checked = _arrayGetData.Item(85).ToString Then
                    bTextChangeFlag = True

                ElseIf Not chkExemptFromRpt.Checked = _arrayGetData.Item(86).ToString Then
                    bTextChangeFlag = True

                    'sarika Workers Comp 7th May 08

                ElseIf Not chkWorkersComp.Checked = _arrayGetData.Item(87).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtWorkersCompClaimNo.Text.Trim = _arrayGetData.Item(88).ToString Then
                    bTextChangeFlag = True

                    '-------------sarika Workers Comp 7th May 08


                    'sarika Auto 7th May 08

                ElseIf Not chkAuto.Checked = _arrayGetData.Item(89).ToString Then
                    bTextChangeFlag = True

                ElseIf Not txtAutoClaimNo.Text.Trim = _arrayGetData.Item(90).ToString Then
                    bTextChangeFlag = True

                    '-------------sarika Auto 7th May 08




                    'ElseIf Not _arrayGetData.Item(4).ToString = "" Then
                    '    If Not MskSSn1.Text.Trim = _arrayGetData.Item(4).ToString Then
                ElseIf Not MskSSn1.Text.Trim = _arrayGetData.Item(4).ToString Then
                    bTextChangeFlag = True
                End If

                'Else
                '    bTextChangeFlag = False
                'End If

                ' for Referals modification check
                Dim nReffCounter As Integer = 0
                If Not cmbReferrals.Items.Count = _arrReferals.Count Then
                    bTextChangeFlag = True
                    Exit Function
                Else
                    For nReffCounter = 0 To cmbReferrals.Items.Count - 1
                        If Not cmbReferrals.Items(nReffCounter).ToString = _arrReferals.Item(nReffCounter).ToString Then
                            bTextChangeFlag = True
                            Exit Function
                        End If
                    Next
                End If
                ''''
            End If
        End If

        Return bTextChangeFlag
    End Function

    Private Function IsInsuranceDataChange() As Boolean
        Try
            ' for Insurance Modification check
            ' check for the total number of Insurance adn currently added Insurance list

            'btnadd.enabled = false
            'If Not nInsuranceNodeCount = trInsurance.Nodes(0).Nodes.Count Then
            '    bTextChangeFlag_insurance = True
            '    'MessageBox.Show("Please click on Add/Update Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return bTextChangeFlag_insurance
            '    ' Exit Function
            'End If

            ' check for newly added Insurance in Insurance combobox
            'If cmbInsuranceNew.Items.Count > 1 Then
            '    bTextChangeFlag_insurance = True
            '    ChangeInsuranceDisplayMsg()
            'End If

            ' Check individual elements of the Insurance tree node
            ' ''If Not strInsuranceText = txtInsuranceNew.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not strSubPolicy = txtSubscriberPolicy.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not strSubId = txtSubscriberID.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not strSubName = txtSubscriberName.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not strEmployer = txtEmployer.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not nPhone = mskIPhone1.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not strGroup = txtGroup.Text Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf Not chkPrimaryOrNot = chkPrimary.Checked Then
            ' ''    bTextChangeFlag_insurance = True
            ' ''    ChangeInsuranceDisplayMsg()

            ' ''ElseIf mskIDOB1.Text.Trim.Length > 4 Then
            ' ''    If Not dtDob = mskIDOB1.Text Then
            ' ''        bTextChangeFlag_insurance = True
            ' ''        ChangeInsuranceDisplayMsg()
            ' ''    End If
            ' ''End If
            ''''
            'compare the two insurance collections

            bTextChangeFlag_insurance = PatientRegistration.chkInsChanged()

            Return bTextChangeFlag_insurance


        Catch ex As Exception

        End Try
    End Function

    Private Function chkDateChanged(ByVal dt As Object, ByVal mskbox As MaskedTextBox) As Boolean
        Dim _blnflag As Boolean

        If IsDBNull(dt) = False Then
            If dt.ToString.Trim <> "" Then
                'code modified by sarika 31st oct 07
                If Not mskbox.Text = Format(CType(dt, Date), "MMddyyyy") Then
                    _blnflag = True
                End If
            End If
        Else
            '' If DB Null and if the Date is Entered then Set Flag True
            If mskbox.MaskCompleted = True Then
                _blnflag = True
            End If
        End If

        Return _blnflag
    End Function
    '---------------------------------------------------------


    ' common message for insurance changes Only
    Private Sub ChangeInsuranceDisplayMsg()
        MessageBox.Show("Please click on Add/Update Button of Insurance Name to save this changes ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'TabItem4.IsSelected = True
        btnAdd.Select()
        Exit Sub
    End Sub

    'Public Function IsDataChange_old()
    '    'GetData()

    '    txtPatientCode.Text = Arrlist.Item(0)
    '    txtfname.Text = Arrlist.Item(1)
    '    txtmName.Text = Arrlist.Item(2)
    '    txtlname.Text = Arrlist.Item(3)
    '    mskssn.Text = Arrlist.Item(4)
    '    dtpDOB.Checked = Arrlist.Item(5)
    '    rbGender1 = Arrlist.Item(6)
    '    cmbMaritalstatus.Text = Arrlist.Item(7)
    '    txtAddress1.TextArrlist.Item(8)
    '    txtAddress2.Text = Arrlist.Item(9)
    '    txtCity.Text = Arrlist.Item(10)
    '    cmbState.Text = Arrlist.Item(11)
    '    txtZip.Text = Arrlist.Item(12)
    '    txtCounty.Text = Arrlist.Item(13)
    '    mskPhone.CtlText = Arrlist.Item(14)
    '    mskMobile.Text = Arrlist.Item(15)
    '    txtemail.Text = Arrlist.Item(16)
    '    txtfax.Text = Arrlist.Item(17)
    '    txtOccupation.Text = Arrlist.Item(18)
    '    cmbEmploymentStatus.Text(Arrlist.Item(19))
    '    txtWLocation.Text = Arrlist.Item(20)
    '    txtwAddress1.Text = Arrlist.Item(21)
    '    txtwAddress2.Text = Arrlist.Item(22)
    '    txtwCity.Text = Arrlist.Item(23)
    '    cmbwState.Text = Arrlist.Item(24)
    '    txtwZip.Text = Arrlist.Item(25)
    '    mskwphone.Text = Arrlist.Item(26)
    '    txtwFax.Text = Arrlist.Item(27)
    '    txtchiefcomplaints.Text = Arrlist.Item(28)
    '    txtpcp.Tag(0) = Arrlist.Item(29)
    '    txtGuarantor.Text = Arrlist.Item(30)
    '    txtspousename.Text = Arrlist.Item(31)
    '    mskspousePhone.Text = Arrlist.Item(32)
    '    cmbRace.Text = Arrlist.Item(33)
    '    cmbPatientStatus.Text = Arrlist.Item(34)
    '    cmbProvider.Text(Arrlist.Item(35))
    '    '    Arrlist.Item(37))   
    '    '    cmbProvider.Text   
    '    'Else
    '    '    cmbProvider.Text   CType(Arrlist.Item(37), System.String)
    '    'End  
    '    txtPharmacy.Tag(0) = Arrlist.Item(36)
    '    txtPharmacy.Text = Arrlist.Item(38)
    '    picFinalPatient.Image(Nothing) = Arrlist.Item(39)
    '    mskRegistrationdate1.Text = Arrlist.Item(40)
    '    txtpcp.Text = Arrlist.Item(41)
    '    mskInjurydate1.Text = Arrlist.Item(42)
    '    mskSurgeryDate1.Text = Arrlist.Item(43)
    '    cmbDominance.Text = Arrlist.Item(44)
    '    cmbLocation.Text = Arrlist.Item(45)
    '    txtMother_fName.Text = Arrlist.Item(46)
    '    txtMother_mName.Text = Arrlist.Item(47)
    '    txtMother_lName.Text = Arrlist.Item(48)
    '    txtMother_Address1.Text = Arrlist.Item(49)
    '    txtMother_Address2.Text = Arrlist.Item(50)
    '    txtMother_City.Text = Arrlist.Item(51)
    '    cmbMother_State.Text = Arrlist.Item(52)
    '    txtMother_Zip.Text = Arrlist.Item(53)
    '    txtMother_County.Text = Arrlist.Item(54)
    '    mskPhone.CtlText = Arrlist.Item(55)
    '    mskMobile.Text = Arrlist.Item(56)
    '    txtMother_Fax.Text = Arrlist.Item(57)
    '    txtMother_Email.Text = Arrlist.Item(58)
    '    txtFather_fName.Text = Arrlist.Item(59)
    '    txtFather_mName.Text = Arrlist.Item(60)
    '    txtFather_lName.Text = Arrlist.Item(61)
    '    txtFather_Address1.Text = Arrlist.Item(62)
    '    txtFather_Address2.Text = Arrlist.Item(63)
    '    txtFather_City.Text = Arrlist.Item(64)
    '    cmbFather_State.Text = Arrlist.Item(65)
    '    txtFather_Zip.Text = Arrlist.Item(66)
    '    txtFather_County.Text = Arrlist.Item(67)
    '    mskPhone.CtlText = Arrlist.Item(68)
    '    mskMobile.Text = Arrlist.Item(69)
    '    txtFather_Fax.Text = Arrlist.Item(70)
    '    txtFather_Email.Text = Arrlist.Item(71)
    '    txtGuardian_fName.Text = Arrlist.Item(72)
    '    txtGuardian_mName.Text = Arrlist.Item(73)
    '    txtGuardian_lName.Text = Arrlist.Item(74)
    '    txtGuardian_Address1.Text = Arrlist.Item(75)
    '    txtGuardian_Address2.Text = Arrlist.Item(76)
    '    txtGuardian_City.Text = Arrlist.Item(77)
    '    cmbGuardian_State.Text = Arrlist.Item(78)
    '    txtGuardian_Zip.Text = Arrlist.Item(79)
    '    txtGuardian_County.Text = Arrlist.Item(80)
    '    mskPhone.CtlText = Arrlist.Item(81)
    '    mskMobile.Text = Arrlist.Item(82)
    '    txtGuardian_Fax.Text = Arrlist.Item(83)
    '    txtGuardian_Email.Text = Arrlist.Item(84)
    '    chkDirective.Checked(False) = Arrlist.Item(85)
    '    chkExemptFromRpt.Checked(False) = Arrlist.Item(86)


    '    '---------------------------------------------------------
    '    'setdata()
    '    Arrlist.Add(intId)0
    '    Arrlist.Add(1)1
    '    Arrlist.Add(Trim(txtfname.Text))2
    '    Arrlist.Add(Trim(txtmName.Text))3
    '    Arrlist.Add(Trim(txtlname.Text))4  


    '    Arrlist.Add(Trim(MskSSn1.Text))

    '    Arrlist.Add(CType(mskDOB1.Text, Date))

    '    If rbGender1.Checked = True Then
    '        Arrlist.Add("Male")
    '    ElseIf rbGender2.Checked = True Then
    '        Arrlist.Add("Female")
    '    ElseIf rbGender3.Checked = True Then
    '        Arrlist.Add("Other")
    '    End If

    '    Arrlist.Add(cmbMaritalstatus.Text)
    '    Arrlist.Add(Trim(txtAddress1.Text))
    '    Arrlist.Add(Trim(txtAddress2.Text))
    '    Arrlist.Add(Trim(txtCity.Text))
    '    Arrlist.Add(Trim(cmbState.Text))
    '    Arrlist.Add(Trim(txtZip.Text))

    '    Arrlist.Add(Trim(mskPhone1.Text))
    '    Arrlist.Add(Trim(mskMobile1.Text))

    '    Arrlist.Add(Trim(txtemail.Text))
    '    Arrlist.Add(Trim(txtfax.Text))
    '    Arrlist.Add(Trim(txtOccupation.Text))
    '    Arrlist.Add(Trim(cmbEmploymentStatus.Text))
    '    Arrlist.Add(Trim(txtWLocation.Text))
    '    Arrlist.Add(Trim(txtwAddress1.Text))
    '    Arrlist.Add(Trim(txtwAddress2.Text))
    '    Arrlist.Add(Trim(txtwCity.Text))
    '    Arrlist.Add(Trim(cmbwState.Text))
    '    Arrlist.Add(Trim(txtwZip.Text))


    '    Arrlist.Add(Trim(mskwPhone1.Text))
    '    Arrlist.Add(Trim(txtwFax.Text))
    '    Arrlist.Add(Trim(txtchiefcomplaints.Text))

    '    Arrlist.Add(Trim(txtpcp.Tag))
    '    Arrlist.Add(Trim(txtGuarantor.Text))
    '    Arrlist.Add(Trim(txtspousename.Text))

    '    Arrlist.Add(Trim(mskspousePhone1.Text))

    '    Arrlist.Add(cmbRace.Text)
    '    Arrlist.Add(cmbPatientStatus.Text)

    '    If Len(mskDOB1.Text) > 0 Then
    '        Arrlist.Add(True)
    '    Else
    '        Arrlist.Add(False)
    '    End If


    '    Arrlist.Add(cmbProvider.SelectedValue)
    '    Arrlist.Add(txtPharmacy.Tag)
    '    Arrlist.Add(Trim(txtCounty.Text))

    '    If Len(Trim(mskRegistrationdate1.Text)) < 5 Then
    '        Arrlist.Add(Now)
    '        Arrlist.Add(False)
    '    Else
    '        Arrlist.Add(CType(mskRegistrationdate1.Text, Date))
    '        Arrlist.Add(True)
    '    End If

    '    If Len(Trim(mskInjurydate1.Text)) < 5 Then
    '        Arrlist.Add(Now)
    '        Arrlist.Add(False)
    '    Else
    '        Arrlist.Add(CType(mskInjurydate1.Text, Date))
    '        Arrlist.Add(True)
    '    End If

    '    If Len(Trim(mskSurgeryDate1.Text)) < 5 Then
    '        Arrlist.Add(Now)
    '        Arrlist.Add(False)
    '    Else
    '        Arrlist.Add(CType(mskSurgeryDate1.Text, Date))
    '        Arrlist.Add(True)
    '    End If


    '    Arrlist.Add(cmbDominance.Text)

    '    Arrlist.Add(cmbLocation.Text)

    '    Arrlist.Add(picFinalPatient.Image)

    '    Arrlist.Add(Trim(txtMother_fName.Text))
    '    Arrlist.Add(Trim(txtMother_mName.Text))
    '    Arrlist.Add(Trim(txtMother_lName.Text))
    '    Arrlist.Add(Trim(txtMother_Address1.Text))
    '    Arrlist.Add(Trim(txtMother_Address2.Text))
    '    Arrlist.Add(Trim(txtMother_City.Text))
    '    Arrlist.Add(Trim(cmbMother_State.Text))
    '    Arrlist.Add(Trim(txtMother_Zip.Text))
    '    Arrlist.Add(Trim(txtMother_County.Text))


    '    Arrlist.Add(Trim(mskMother_Phone1.Text))
    '    Arrlist.Add(Trim(mskMother_Mobile1.Text))


    '    Arrlist.Add(Trim(txtMother_Fax.Text))
    '    Arrlist.Add(Trim(txtMother_Email.Text))

    '    Arrlist.Add(Trim(txtFather_fName.Text))
    '    Arrlist.Add(Trim(txtFather_mName.Text))
    '    Arrlist.Add(Trim(txtFather_lName.Text))
    '    Arrlist.Add(Trim(txtFather_Address1.Text))
    '    Arrlist.Add(Trim(txtFather_Address2.Text))
    '    Arrlist.Add(Trim(txtFather_City.Text))
    '    Arrlist.Add(Trim(cmbFather_State.Text))
    '    Arrlist.Add(Trim(txtFather_Zip.Text))
    '    Arrlist.Add(Trim(txtFather_County.Text))


    '    Arrlist.Add(Trim(mskFather_Phone1.Text))

    '    Arrlist.Add(Trim(mskFather_Mobile1.Text))

    '    Arrlist.Add(Trim(txtFather_Fax.Text))
    '    Arrlist.Add(Trim(txtFather_Email.Text))

    '    Arrlist.Add(Trim(txtGuardian_fName.Text))
    '    Arrlist.Add(Trim(txtGuardian_mName.Text))
    '    Arrlist.Add(Trim(txtGuardian_lName.Text))
    '    Arrlist.Add(Trim(txtGuardian_Address1.Text))
    '    Arrlist.Add(Trim(txtGuardian_Address2.Text))
    '    Arrlist.Add(Trim(txtGuardian_City.Text))
    '    Arrlist.Add(Trim(cmbGuardian_State.Text))
    '    Arrlist.Add(Trim(txtGuardian_Zip.Text))
    '    Arrlist.Add(Trim(txtGuardian_County.Text))


    '    Arrlist.Add(Trim(mskGuardian_Phone1.Text))
    '    Arrlist.Add(Trim(mskGuardian_Mobile1.Text))

    '    Arrlist.Add(Trim(txtGuardian_Fax.Text))
    '    Arrlist.Add(Trim(txtGuardian_Email.Text))

    '    If chkDirective.Checked = False Then
    '        Arrlist.Add(0)
    '    Else
    '        Arrlist.Add(1)
    '    End If

    '    If chkExemptFromRpt.Checked = False Then
    '        Arrlist.Add(0)
    '    Else
    '        Arrlist.Add(1)
    '    End If

    'End Function

    Private Sub PrintPatientRegistration()
        Dim sender As Object
        Dim e As EventArgs
        ''b4 printing save the new patient or modifications made to the old patient if any
        'If intId <> 0 Then
        '    'modify mode
        '    'save the changes
        '    Call btnOK_Click(sender, e)
        '    'print the reg dets and close
        '    Callprint(gnPatientID.ToString)
        'Else
        '    'add mode
        '    'insert the patient
        '    'call save

        '    'get the patientid
        '    Call btnOK_Click(sender, e)

        '    'get the new patientid
        '    Callprint(PatientID.ToString)
        'End If


        ' '''
        ''Me.Close()


        ''''''''''
        _blnPrint = True
        'save the changes
        Call ts_btnSaveandClose_Click(sender, e)
        'print the reg dets and close
        If _blnPrint = True Then
            Callprint(intId.ToString)
        End If

        _blnPrint = False
        '''''''''
    End Sub
    Private Sub Callprint(ByVal PatientID As String)
        'Only if there are items in flexgrid then check for duplication.
        Dim returnvisitid As Int64
        Try


            Dim oRpt As ReportDocument
            oRpt = New ReportDocument
            'code commented temporarily by sarika 13th june 07

            'oRpt.Load(System.Windows.Forms.Application.StartupPath & "\Reports\RptPatientCurrentMedication.rpt")
            'added the startuppath code as asked by madam on MSN on 19 june 2007
            oRpt.Load(gstrgloEMRStartupPath & "\Reports\rptPatientRegDeta.rpt")


            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim TableCounter

            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"

                'sarika 13th june 2007
                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next

            oRpt.SetParameterValue("PatientID", PatientID)



            'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName

            oRpt.PrintToPrinter(1, False, 0, 0)



            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Report is printed.", gstrLoginName, gstrClientMachineName, 0, True)
            'objAudit = Nothing
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Report is printed.", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtInsuranceNew_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsuranceNew.TextChanged
        If txtInsuranceNew.Text.Trim.Length = 0 Then
            btnCloseInsuranceName.Enabled = False
        Else
            btnCloseInsuranceName.Enabled = True
            btnAdd.Enabled = True
        End If
    End Sub

    'Private Sub mskPhone1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskPhone1.Enter
    '    mskPhone1.Select(5, 0)
    'End Sub

    Private Sub mskPhone1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskPhone1.GotFocus
        mskPhone1.Select(0, 0)

    End Sub

    Private Sub mskwPhone1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskwPhone1.GotFocus
        mskwPhone1.Select(0, 0)
    End Sub

    Private Sub SendHL7PatientDetails(ByVal PatientId As Int64, ByVal blnIsUpdate As Boolean)
        Dim oglohl7 As New gloHl7Interface.HL7Library.gloHL7
        Try
            oglohl7.DatabaseName = gstrDatabaseName
            oglohl7.ServerName = gstrSQLServerName
            If oglohl7.Connect(System.Windows.Forms.Application.StartupPath, gloHl7Interface.HL7Library.enumHL7ConnectionType.Create, "2.3") Then
                If blnIsUpdate Then
                    oglohl7.CreateA08Message(PatientId)
                Else
                    oglohl7.CreateA04Message(PatientId)
                End If
            End If
        Catch ex As gloHl7Interface.HL7DatabaseLibrary.HL7DatabaseException
            MsgBox(ex.ErrMessage)
        Catch ex As gloHl7Interface.HL7Library.HL7Exception
            MsgBox(ex.ErrMessage)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'sarika Workers Comp 7th May 08
    Private Sub chkWorkersComp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWorkersComp.CheckedChanged
        Try
            If chkWorkersComp.Checked = True Then
                txtWorkersCompClaimNo.Enabled = True
            Else
                txtWorkersCompClaimNo.Enabled = False
                txtWorkersCompClaimNo.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '-------------- 'sarika Workers Comp 7th May 08---------



    'sarika Auto 7th May 08
    Private Sub chkAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAuto.CheckedChanged
        Try
            If chkAuto.Checked = True Then
                txtAutoClaimNo.Enabled = True
            Else
                txtAutoClaimNo.Enabled = False
                txtAutoClaimNo.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Patient Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub tls__ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_PatientRegistration.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "SaveCls"
                    SaveandClosePatientRecord()
                Case "Demo Hx"
                    DemoHxPatientRecordChange()
                Case "Print"
                    PrintPatientRegistration()
                Case "Close"
                    ClosePatientRegistration()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub ts_btnSaveandClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnSaveandClose.Click
        '  btnAdd_Click()
    End Sub

   
    
    'http://bytes.com/forum/thread761563.html
    'Private Sub txtemail_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtemail.Validating
    '    Dim rEMail As New System.Text.RegularExpressions.Regex("^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")
    '    If txtemail.Text.Length > 0 Then
    '        If Not rEMail.IsMatch(txtemail.Text) Then
    '            MessageBox.Show("E-Mail expected", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
    '            txtemail.SelectAll()
    '            e.Cancel = True
    '        End If
    '    End If

    'End Sub

  
End Class
