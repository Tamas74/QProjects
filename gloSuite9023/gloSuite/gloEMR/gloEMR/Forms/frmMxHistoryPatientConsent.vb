Imports gloRxHub


Public Class frmMxHistoryPatientConsent
    Public oPatient As gloRxHub.ClsPatient

    Public RxHUBstartDate As DateTime
    Public RxHUBendDate As DateTime


    Private Sub ts_btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnOK.Click
        '' ''Commented on 20100101 - previous logic
        ' ''Parental Guardian
        ''If rbtnPG_Yes.Checked = True Then
        ''    RxHUBPatientConsent = "Y"
        ''End If
        ''If rbtnPG_No.Checked = True Then
        ''    RxHUBPatientConsent = "X"
        ''End If
        ' ''Parental Guardian

        ' ''Return Drug
        ''If rbtnReturnDrg_Yes.Checked = True Then
        ''    RxHUBPatientConsent = "Y"
        ''End If
        ''If rbtnReturnDrg_No.Checked = True Then
        ''    RxHUBPatientConsent = "N"
        ''End If
        ' ''Return Drug

        ' ''Physician only
        ''If rbtnPhOnly_Yes.Checked = True Then
        ''    RxHUBPatientConsent = "P"
        ''End If
        ''If rbtnPhOnly_No.Checked = True Then
        ''    RxHUBPatientConsent = "Z"
        ''End If
        ' ''Physician only

        ''\\New logic - 20100110
        If rbtnConsentgiven.Checked = True Then
            RxHUBPatientConsent = "Y"
        End If

        If rbtnNoConsent.Checked = True Then
            RxHUBPatientConsent = "N"
        End If

        If rbtnPrescriber.Checked = True Then
            RxHUBPatientConsent = "P"
        End If

        If rbtnFromAnyPrescriber.Checked = True Then
            RxHUBPatientConsent = "X"
        End If
     
        If rbtnForPrescriber.Checked = True Then
            RxHUBPatientConsent = "Z"
        End If
      
        'RxHUBstartDate = Convert.ToDateTime(dtFrom.Text.Trim)
        'RxHUBendDate = Convert.ToDateTime(dtTo.Text.Trim)

        'gSelectedPBMName = cmbPBM.SelectedText.ToString
        'gSelectedPBM_MemberID = cmbPBM.SelectedValue.ToString

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub


    Private Sub frmMxHistoryPatientConsent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'rbtnPG_Yes.Enabled = True
        'rbtnReturnDrg_No.Enabled = True
        'rbtnPhOnly_Yes.Enabled = True
        'rbtnPG_No.Enabled = True
        'rbtnPhOnly_No.Enabled = True
        'Dim dtPBM As DataTable = New DataTable()
        'With dtPBM.Columns
        '    .Add("PBMName")
        '    .Add("PBMMemberID")
        'End With
        rbtnConsentgiven.Checked = True

        If gstrRxHubDisclaimer.Trim() <> "" Then
            txtRxHubDisclaimer.Text = gstrRxHubDisclaimer

        End If

        '\\Add items to combobox ----------- 
        'Dim oEligibilityCheck As New clsEligibilityCheckDBLayer()
        'Dim dt271Info As DataTable = oEligibilityCheck.Get271Information(gnPatientID)
        'If Not IsNothing(dt271Info) Then
        '    If dt271Info.Rows.Count > 0 Then
        '        cmbPBM.Items.Clear()
        '        For rwCnt As Integer = 0 To dt271Info.Rows.Count - 1

        '            '' Dim _271FormularyId As String = dt271Info.Rows(rwCnt)("sFormularyListID")
        '            '' Dim _271CoverageId As String = dt271Info.Rows(rwCnt)("sCoverageID")

        '            Dim _HealthPlanName As String
        '            If dt271Info.Rows(rwCnt)("sHealthPlanName") <> "" Then
        '                _HealthPlanName = dt271Info.Rows(rwCnt)("sHealthPlanName")
        '            Else
        '                _HealthPlanName = ""
        '            End If
        '            Dim _PBMName As String = dt271Info.Rows(rwCnt)("sPBM_PayerName")
        '            Dim _PBMNameHealthPlnName As String
        '            If _HealthPlanName <> "" Then
        '                _PBMNameHealthPlnName = _PBMName & "-" & _HealthPlanName
        '            Else
        '                _PBMNameHealthPlnName = _PBMName
        '            End If

        '            cmbPBM.DataSource = dt271Info
        '            'Imp:An eligible coverage is a coverage with at least a mail order or retail pharmacy benefit.
        '            If dt271Info.Rows(rwCnt)("sPhEligiblityorBenefitInfo") = "Active Coverage" Or dt271Info.Rows(rwCnt)("sMailOrdEligiblityorBenefitInfo") = "Active Coverage" Then
        '                dtPBM.Rows.Add()
        '                dtPBM.Rows(dtPBM.Rows.Count - 1)("PBMName") = _PBMNameHealthPlnName
        '                dtPBM.Rows(dtPBM.Rows.Count - 1)("PBMMemberID") = dt271Info.Rows(rwCnt)("sPBM_PayerMemberID")


        '                'cmbPBM.Tag = dt271Info.Rows(rwCnt)("sPBM_PayerMemberID")
        '                'cmbPBM.Items.Add(_PBMNameHealthPlnName)

        '            End If

        '        Next

        '    Else
        '        cmbPBM.Items.Clear()
        '    End If

        'Else
        '    cmbPBM.Items.Clear()
        'End If
        'If cmbPBM.Items.Count > 0 Then
        '    cmbPBM.SelectedIndex = 0
        'End If
        'cmbPBM.DataSource = dtPBM
        'cmbPBM.DisplayMember = "PBMName"
        'cmbPBM.ValueMember = "PBMMemberID"



        'cmbPBM.Enabled = True '\\make change*** False
        'gPBMAllselectFlag = True
        '// --------------


    End Sub


 
    Private Sub grpBxConsentFlags_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grpBxConsentFlags.Click
        'If rbtnLevel1.Checked = True Then
        '    rbtnNO.Enabled = False
        '    rbtnParentalorGurdian.Enabled = False
        '    rbtnPhysicianOnly.Enabled = False
        '    If rbtnYes.Checked = True Then
        '        oPatient.RxH271Master.Consent = "Y"
        '    Else
        '        oPatient.RxH271Master.Consent = "X"
        '    End If

        'ElseIf rbtnLevel2.Checked = True Then
        '    pnlLevel1.Enabled = False
        '    pnlLevel3.Enabled = False

        '    If rbtnNO.Checked = True Then
        '        oPatient.RxH271Master.Consent = "N"
        '    End If
        'Else
        '    pnlLevel1.Enabled = False
        '    pnlLevel2.Enabled = False
        '    If rbtnPhysicianOnly.Checked = True Then
        '        oPatient.RxH271Master.Consent = "P"
        '    Else
        '        oPatient.RxH271Master.Consent = "Z"
        '    End If
        'End If
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbtnLevel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnLevel1.Click
        pnlLevel1.Enabled = True

        pnlLevel2.Enabled = False
        pnlLevel3.Enabled = False
    End Sub

    Private Sub rbtnLevel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnLevel2.Click
        pnlLevel1.Enabled = False
        pnlLevel2.Enabled = True
        pnlLevel3.Enabled = False
    End Sub

    Private Sub rbtnLevel3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnLevel3.Click
        pnlLevel1.Enabled = False
        pnlLevel2.Enabled = False
        pnlLevel3.Enabled = True
    End Sub

    Private Sub rbtnLevel2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub rbtnParentaorGuardian_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub rbtnPhysicianOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbPBM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPBM.SelectedIndexChanged
        Try
            ''''gSelectedPBMName = cmbPBM.SelectedItem.ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbtn_SelectedPBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtn_SelectedPBM.Click
        'rdbtn_SelectedPBM.Checked = True  '\\make change*** and if () make change rdiobtn for selected visible=true , enable=true
        'cmbPBM.Enabled = True

        'rdbtn_AllPBM.Checked = False
        'gPBMAllselectFlag = False
    End Sub

    Private Sub rdbtn_AllPBM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtn_AllPBM.Click
        rdbtn_AllPBM.Checked = True
        gPBMAllselectFlag = True

        rdbtn_SelectedPBM.Checked = False
        cmbPBM.Enabled = True '\\False    '\\make change***
    End Sub

    Public Sub New(ByVal PBMName As String, ByVal PBMMemberID As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        gSelectedPBMName = PBMName
        gSelectedPBM_MemberID = PBMMemberID
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class