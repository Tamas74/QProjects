Imports gloEMR.gloStream.DiseaseManagement

Partial Public Class frmDM_RulesSetup

    Private Sub SaveCriteria()

        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement

        Dim strHeight As String = ""
        Dim strHeightMax As String = ""
        Dim strHeight_Ex As String = ""
        Dim strHeightMax_Ex As String = ""
        Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
        Dim oCriteria_Exclusion As New gloStream.DiseaseManagement.Supporting.Criteria
        Dim oOtherDetails As New gloStream.DiseaseManagement.Supporting.OtherDetails
        Dim oOtherDetails_Exclusion As New gloStream.DiseaseManagement.Supporting.OtherDetails

        Try

            If blsCopyRule Then
                m_CriteriaId = 0
            End If

            If oDM.IsExists(txtName.Text.Replace("'", "''").Trim(), blsModify, m_CriteriaId) = True Then
                MessageBox.Show("Please enter another name, name already exist.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                Exit Sub
            End If

            _IsValid = True

            Dim _ResultCriteriaID As Int64 = 0

            strHeight = txtHeightMin.Text + "'" + txtHeightMinInch.Text + "''"
            strHeightMax = txtHeightMax.Text + "'" + txtHeightMaxInch.Text + "''"

            strHeight_Ex = txtHeightMin_Ex.Text + "'" + txtHeightMinInch_Ex.Text + "''"
            strHeightMax_Ex = txtHeightMax_Ex.Text + "'" + txtHeightMaxInch_Ex.Text + "''"

            If Trim(txtName.Text) = "" Then
                MessageBox.Show("Please enter the name.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                txtName.Focus()
                Exit Sub
            End If

            If Trim(txtMessage.Text) = "" Then
                MessageBox.Show("Please enter the message.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                txtMessage.Focus()
                Exit Sub
            End If

            If chckRecurring.Checked = True And (cmbPeriod.SelectedIndex = -1 Or cmbDurationType.SelectedIndex = -1) Then
                MessageBox.Show("Select recurrence criteria {Duration and Type}", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                cmbPeriod.Focus()
                Exit Sub
            End If

            If Not (txtBPsettingMax.Text.Trim() = "" And txtBPsettingMin.Text.Trim() = "") Then
                If txtBPsettingMax.Text.Trim() = "" Or txtBPsettingMin.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPsettingMax.Text.Trim() = "" Then
                        txtBPsettingMax.Focus()
                    End If
                    If txtBPsettingMin.Text.Trim() = "" Then
                        txtBPsettingMin.Focus()
                    End If
                    Exit Sub
                End If
            End If
          
            If Not (txtBPsettingMaxTo.Text.Trim() = "" And txtBPsettingMinTo.Text.Trim() = "") Then
                If txtBPsettingMaxTo.Text.Trim() = "" Or txtBPsettingMinTo.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPsettingMaxTo.Text.Trim() = "" Then
                        txtBPsettingMaxTo.Focus()
                    End If
                    If txtBPsettingMinTo.Text.Trim() = "" Then
                        txtBPsettingMinTo.Focus()
                    End If
                    Exit Sub
                End If
            End If

            If Not (txtBPsettingMax_Ex.Text.Trim() = "" And txtBPsettingMin_Ex.Text.Trim() = "") Then
                If txtBPsettingMax_Ex.Text.Trim() = "" Or txtBPsettingMin_Ex.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPsettingMax_Ex.Text.Trim() = "" Then
                        txtBPsettingMax_Ex.Focus()
                    End If
                    If txtBPsettingMin_Ex.Text.Trim() = "" Then
                        txtBPsettingMin_Ex.Focus()
                    End If
                    Exit Sub
                End If
            End If
           
            If Not (txtBPsettingMax_Ex_To.Text.Trim() = "" And txtBPsettingMin_Ex_To.Text.Trim() = "") Then
                If txtBPsettingMax_Ex_To.Text.Trim() = "" Or txtBPsettingMin_Ex_To.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPsettingMax_Ex_To.Text.Trim() = "" Then
                        txtBPsettingMax_Ex_To.Focus()
                    End If
                    If txtBPsettingMin_Ex_To.Text.Trim() = "" Then
                        txtBPsettingMin_Ex_To.Focus()
                    End If
                    Exit Sub
                End If
            End If

            'BP Standing validation
            If Not (txtBPstandingMax.Text.Trim() = "" And txtBPstandingMin.Text.Trim() = "") Then
                If txtBPstandingMax.Text.Trim() = "" Or txtBPstandingMin.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPstandingMax.Text.Trim() = "" Then
                        txtBPstandingMax.Focus()
                    End If
                    If txtBPstandingMin.Text.Trim() = "" Then
                        txtBPstandingMin.Focus()
                    End If
                    Exit Sub
                End If
            End If

            If Not (txtBPstandingMaxTo.Text.Trim() = "" And txtBPstandingMinTo.Text.Trim() = "") Then
                If txtBPstandingMaxTo.Text.Trim() = "" Or txtBPstandingMinTo.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPstandingMaxTo.Text.Trim() = "" Then
                        txtBPstandingMaxTo.Focus()
                    End If
                    If txtBPstandingMinTo.Text.Trim() = "" Then
                        txtBPstandingMinTo.Focus()
                    End If
                    Exit Sub
                End If
            End If

            If Not (txtBPstandingMax_Ex.Text.Trim() = "" And txtBPstandingMin_Ex.Text.Trim() = "") Then
                If txtBPstandingMax_Ex.Text.Trim() = "" Or txtBPstandingMin_Ex.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPstandingMax_Ex.Text.Trim() = "" Then
                        txtBPstandingMax_Ex.Focus()
                    End If
                    If txtBPstandingMin_Ex.Text.Trim() = "" Then
                        txtBPstandingMin_Ex.Focus()
                    End If
                    Exit Sub
                End If
            End If

            If Not (txtBPstandingMax_Ex_To.Text.Trim() = "" And txtBPstandingMin_Ex_To.Text.Trim() = "") Then
                If txtBPstandingMax_Ex_To.Text.Trim() = "" Or txtBPstandingMin_Ex_To.Text.Trim() = "" Then
                    MessageBox.Show("Enter all details of BPSitting.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _IsValid = False
                    If txtBPstandingMax_Ex_To.Text.Trim() = "" Then
                        txtBPstandingMax_Ex_To.Focus()
                    End If
                    If txtBPstandingMin_Ex_To.Text.Trim() = "" Then
                        txtBPstandingMin_Ex_To.Focus()
                    End If
                    Exit Sub
                End If
            End If

            'Check for the enterd State value for manual data entered
            For i As Integer = 0 To cmbState.Items.Count - 1
                If cmbState.Text.ToUpper = cmbState.Items(i).ToString.ToUpper Then
                    cmbState.SelectedIndex = i
                    Exit For
                End If
            Next

            For i As Integer = 0 To cmbState_Ex.Items.Count - 1
                If cmbState_Ex.Text.ToUpper = cmbState_Ex.Items(i).ToString.ToUpper Then
                    cmbState_Ex.SelectedIndex = i
                    Exit For
                End If
            Next

            'Check for the enterd Gender for manual data entered
            For i As Integer = 0 To cmbGender.Items.Count - 1
                If cmbGender.Text.ToUpper = cmbGender.Items(i).ToString.ToUpper Then
                    cmbGender.SelectedIndex = i
                    Exit For
                End If
            Next

            For i As Integer = 0 To cmbGender_Ex.Items.Count - 1
                If cmbGender_Ex.Text.ToUpper = cmbGender_Ex.Items(i).ToString.ToUpper Then
                    cmbGender_Ex.SelectedIndex = i
                    Exit For
                End If
            Next

            'Check for the enterd race value for manual data entered
            For i As Integer = 0 To cmbRace.Items.Count - 1
                If cmbRace.Text.ToUpper = cmbRace.Items(i).ToString.ToUpper Then
                    cmbRace.SelectedIndex = i
                    Exit For
                End If
            Next

            'Exclucion

            'Check for the enterd marital status for manual data entered
            For i As Integer = 0 To cmbMaritalSt.Items.Count - 1
                If cmbMaritalSt.Text.ToUpper = cmbMaritalSt.Items(i).ToString.ToUpper Then
                    cmbMaritalSt.SelectedIndex = i
                    Exit For
                End If
            Next

            'Check for the enterd Employee status value for manual data entered
            For i As Integer = 0 To cmbEmpStatus.Items.Count - 1
                If cmbEmpStatus.Text.ToUpper = cmbEmpStatus.Items(i).ToString.ToUpper Then
                    cmbEmpStatus.SelectedIndex = i
                    Exit For
                End If
            Next

            For i As Integer = 0 To cmbEmpStatus_Ex.Items.Count - 1
                If cmbEmpStatus_Ex.Text.ToUpper = cmbEmpStatus_Ex.Items(i).ToString.ToUpper Then
                    cmbEmpStatus_Ex.SelectedIndex = i
                    Exit For
                End If
            Next

            'Check for the enterd Employee Age Minimum value for manual data entered
            For i As Integer = 0 To cmbAgeMin.Items.Count - 1
                If cmbAgeMin.Text.ToUpper = cmbAgeMin.Items(i).ToString.ToUpper Then
                    cmbAgeMin.SelectedIndex = i
                    Exit For
                End If
            Next

            For i As Integer = 0 To cmbAgeMin_Ex.Items.Count - 1
                If cmbAgeMin_Ex.Text.ToUpper = cmbAgeMin_Ex.Items(i).ToString.ToUpper Then
                    cmbAgeMin_Ex.SelectedIndex = i
                    Exit For
                End If
            Next

            'Check for the enterd Employee  Age Maximum value for manual data entered
            For i As Integer = 0 To cmbAgeMax.Items.Count - 1
                If cmbAgeMax.Text.ToUpper = cmbAgeMax.Items(i).ToString.ToUpper Then
                    cmbAgeMax.SelectedIndex = i
                    Exit For
                End If
            Next

            For i As Integer = 0 To cmbAgeMax_Ex.Items.Count - 1
                If cmbAgeMax_Ex.Text.ToUpper = cmbAgeMax_Ex.Items(i).ToString.ToUpper Then
                    cmbAgeMax_Ex.SelectedIndex = i
                    Exit For
                End If
            Next

            'Minimum maximum Age check
            'Condition changed by Mayuri:20091216-To validate for '0'-Bug ID-#4166

            If Val(cmbAgeMin.Text) >= 0 And Val(cmbAgeMax.Text) >= 0 Then
                If MinMaxValidator(Trim(cmbAgeMin.Text), Trim(cmbAgeMax.Text)) = False Then
                    MessageBox.Show("Please check the minimum and maximum values for age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbAgeMax.Focus()
                    _IsValid = False
                    Exit Sub
                End If
                If Val(Trim(cmbAgeMin.Text)) = Val(cmbAgeMax.Text) Then
                    If MinMaxValidator(Trim(cmbAgeMinMnth.Text), Trim(cmbAgeMaxMnth.Text)) = False Then
                        MessageBox.Show("Please check the minimum and maximum values for age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        cmbAgeMax.Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                End If
            End If

            If Val(cmbAgeMin_Ex.Text) >= 0 And Val(cmbAgeMax_Ex.Text) >= 0 Then
                If MinMaxValidator(Trim(cmbAgeMin_Ex.Text), Trim(cmbAgeMin_Ex.Text)) = False Then
                    MessageBox.Show("Please check the minimum and maximum values for age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cmbAgeMin_Ex.Focus()
                    _IsValid = False
                    Exit Sub
                End If

                If Val(cmbAgeMin_Ex.Text) = Val(cmbAgeMax_Ex.Text) Then
                    If MinMaxValidator(Trim(cmbAgeMinMnth_Ex.Text), Trim(cmbAgeMaxMnth_Ex.Text)) = False Then
                        MessageBox.Show("Please check the minimum and maximum values for age.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        cmbAgeMax.Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                End If
            End If

            If Val(cmbAgeMin.Text) < 0 Or Val(cmbAgeMin.Text) >= 125 Then
                MessageBox.Show("Please enter minimum age(0 - 124).  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMin.Focus()
                _IsValid = False
                Exit Sub
            End If

            If Val(cmbAgeMin_Ex.Text) < 0 Or Val(cmbAgeMin_Ex.Text) >= 125 Then
                MessageBox.Show("Please enter minimum age(0 - 124).  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMin_Ex.Focus()
                _IsValid = False
                Exit Sub
            End If

            If Val(cmbAgeMax.Text) < 0 Or Val(cmbAgeMax.Text) >= 125 Then
                MessageBox.Show("Please enter maximum age(0 - 124).  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMax.Focus()
                _IsValid = False
                Exit Sub
            End If

            If Val(cmbAgeMax_Ex.Text) < 0 Or Val(cmbAgeMax_Ex.Text) >= 125 Then
                MessageBox.Show("Please enter maximum age(0 - 124).  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAgeMax_Ex.Focus()
                _IsValid = False
                Exit Sub
            End If

            With oCriteria
                .Name = txtName.Text.Trim

                If Not (cmbAgeMin.Text = "" And cmbAgeMinMnth.Text = "") Then
                    .AgeMinimum = CType(cmbAgeMin.SelectedItem & "." & cmbAgeMinMnth.SelectedItem, Double)
                End If
                If Not (cmbAgeMax.Text = "" And cmbAgeMaxMnth.Text = "") Then
                    .AgeMaximum = CType(cmbAgeMax.SelectedItem & "." & cmbAgeMaxMnth.SelectedItem, Double)
                End If
                .City = txtCity.Text
                If Not cmbGender.SelectedItem Is Nothing Then
                    .Gender = cmbGender.SelectedItem
                End If
                If Not cmbState.SelectedItem Is Nothing Then
                    .State = cmbState.SelectedItem
                End If
                If Not cmbRace.SelectedItem Is Nothing Then
                    .Race = cmbRace.SelectedItem
                End If
                .Zip = txtZip.Text
                If Not cmbMaritalSt.SelectedItem Is Nothing Then
                    .MaritalStatus = cmbMaritalSt.SelectedItem
                End If
                If Not cmbEmpStatus.SelectedItem Is Nothing Then
                    .EmployeeStatus = cmbEmpStatus.SelectedItem
                End If

                .HeightMinimum = strHeight
                .HeightMaximum = strHeightMax
                .BPSittingMinimum = CDbl(Val(txtBPsettingMin.Text.Trim))
                .BPSittingMaximum = CDbl(Val(txtBPsettingMax.Text.Trim))

                .BPSittingToMinimum = CDbl(Val(txtBPsettingMinTo.Text.Trim))
                .BPSittingToMaximum = CDbl(Val(txtBPsettingMaxTo.Text.Trim))

                oCriteria.WeightMinimum = CDbl(Val(txtWeightMin.Text.Trim))
                .WeightMaximum = CDbl(Val(txtWeightMax.Text.Trim))

                .BPStandingMinimum = CDbl(Val(txtBPstandingMin.Text.Trim))
                .BPStandingMaximum = CDbl(Val(txtBPstandingMax.Text.Trim))

                .BPStandingToMinimum = CDbl(Val(txtBPstandingMinTo.Text.Trim))
                .BPStandingToMaximum = CDbl(Val(txtBPstandingMaxTo.Text.Trim))

                .TempratureMinumum = CDbl(Val(txtTemperatureMin.Text.Trim))
                .TempratureMaximum = CDbl(Val(txtTemperatureMax.Text.Trim))
                .PulseMinimum = CDbl(Val(txtPulseMin.Text.Trim))
                .PulseMaximum = CDbl(Val(txtPulseMax.Text.Trim))
                .BMIMinimum = CDbl(Val(txtBMImin.Text.Trim))
                .BMIMaximum = CDbl(Val(txtBMImax.Text.Trim))
                .PulseOXMinimum = CDbl(Val(txtPulseOXmin.Text.Trim))
                .PulseOXMaximum = CDbl(Val(txtPulseOXmax.Text.Trim))
                .DisplayMessage = txtMessage.Text.Trim
                .IsActive = chkIsActiveRule.Checked
                .bIsRecuringRule = chckRecurring.Checked
                .dtRecurrenceStartDate = dtStartDate.Value.Date
                .dtRecurrenceEndDate = dtEndDate.Value.Date
                .nDuratiotype = cmbDurationType.SelectedIndex
                .nDuratioPeriod = cmbPeriod.SelectedItem
                .sBibliographicCitatation = txtBibliographicCitation.Text
                .sInterventionDeveloper = txtInterventionDeveloper.Text
                .sFundingSource = txtFundingSource.Text
                .sRelease = txtRelease.Text
                .sRevisionDates = txtRevisionDates.Text

                'Added by Sameer On 19 Sept 2013 for Special Alert CheckBox in DM Rule Setup 
                .bIsSpecialAlert = chkSpecialAlert.Checked
            End With

            'Set oCritetria_Exclusion Object for Exception
            With oCriteria_Exclusion
                .Name = txtName.Text.Trim

                If Not (cmbAgeMin_Ex.Text = "" And cmbAgeMinMnth_Ex.Text = "") Then
                    .AgeMinimum = CType(cmbAgeMin_Ex.SelectedItem & "." & cmbAgeMinMnth_Ex.SelectedItem, Double)
                End If

                If Not (cmbAgeMax_Ex.Text = "" And cmbAgeMaxMnth_Ex.Text = "") Then
                    .AgeMaximum = CType(cmbAgeMax_Ex.SelectedItem & "." & cmbAgeMaxMnth_Ex.SelectedItem, Double)
                End If

                .City = txtCity_Ex.Text

                If Not cmbGender_Ex.SelectedItem Is Nothing Then
                    .Gender = cmbGender_Ex.SelectedItem
                End If

                If Not cmbState_Ex.SelectedItem Is Nothing Then
                    .State = cmbState_Ex.SelectedItem
                End If

                If Not cmbRace.SelectedItem Is Nothing Then '''''''''''Exclusion 
                    .Race = cmbRace.SelectedItem
                End If

                .Zip = txtZip_Ex.Text

                If Not cmbMaritalSt.SelectedItem Is Nothing Then '''''''''''Exclusion 
                    .MaritalStatus = cmbMaritalSt.SelectedItem
                End If

                If Not cmbEmpStatus_Ex.SelectedItem Is Nothing Then
                    .EmployeeStatus = cmbEmpStatus_Ex.SelectedItem
                End If

                .HeightMinimum = strHeight_Ex
                .HeightMaximum = strHeightMax_Ex
                .BPSittingMinimum = CDbl(Val(txtBPsettingMin_Ex.Text.Trim))
                .BPSittingMaximum = CDbl(Val(txtBPsettingMax_Ex.Text.Trim))

                .BPSittingToMinimum = CDbl(Val(txtBPsettingMin_Ex_To.Text.Trim))
                .BPSittingToMaximum = CDbl(Val(txtBPsettingMax_Ex_To.Text.Trim))

                .WeightMinimum = CDbl(Val(txtWeightMin_Ex.Text.Trim))
                .WeightMaximum = CDbl(Val(txtWeightMax_Ex.Text.Trim))
                .BPStandingMinimum = CDbl(Val(txtBPstandingMin_Ex.Text.Trim))
                .BPStandingMaximum = CDbl(Val(txtBPstandingMax_Ex.Text.Trim))

                .BPStandingToMinimum = CDbl(Val(txtBPstandingMin_Ex_To.Text.Trim))
                .BPStandingToMaximum = CDbl(Val(txtBPstandingMax_Ex_To.Text.Trim))

                .TempratureMinumum = CDbl(Val(txtTemperatureMin_Ex.Text.Trim))
                .TempratureMaximum = CDbl(Val(txtTemperatureMax_Ex.Text.Trim))
                .PulseMinimum = CDbl(Val(txtPulseMin_Ex.Text.Trim))
                .PulseMaximum = CDbl(Val(txtPulseMax_Ex.Text.Trim))
                .BMIMinimum = CDbl(Val(txtBMImin_Ex.Text.Trim))
                .BMIMaximum = CDbl(Val(txtBMImax_Ex.Text.Trim))
                .PulseOXMinimum = CDbl(Val(txtPulseOXmin_Ex.Text.Trim))
                .PulseOXMaximum = CDbl(Val(txtPulseOXmax_Ex.Text.Trim))
                .DisplayMessage = txtMessage.Text.Trim
                .IsActive = chkIsActiveRule.Checked
            End With

            If Not (String.IsNullOrEmpty(cmbChkBoxRace.Text)) Then
                For iCount As Integer = 0 To cmbChkBoxRace.Items.Count - 1
                    Dim _text As String = cmbChkBoxRace.Text.ToString()
                    If cmbChkBoxRace.CheckBoxItems(iCount).Text = cmbChkBoxRace.Text Then
                        cmbChkBoxRace.CheckBoxItems(iCount).CheckState = CheckState.Checked
                    End If
                Next
            End If

            For iCount As Integer = 0 To cmbChkBoxRace.Items.Count - 1
                If cmbChkBoxRace.CheckBoxItems(iCount).Checked = True Then
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = cmbChkBoxRace.Tag
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Race.GetHashCode()
                    oOtherDetail1.ItemName = cmbChkBoxRace.CheckBoxItems(iCount).Text
                    oOtherDetails.Add(oOtherDetail1)
                End If
            Next

            For iCount As Integer = 0 To cmbChkBoxRace_Ex.Items.Count - 1
                If cmbChkBoxRace_Ex.CheckBoxItems(iCount).Checked = True Then
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = cmbChkBoxRace_Ex.Tag
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Race.GetHashCode()
                    oOtherDetail1.ItemName = cmbChkBoxRace_Ex.CheckBoxItems(iCount).Text
                    oOtherDetails_Exclusion.Add(oOtherDetail1)
                End If
            Next
            

            For iCount As Integer = 0 To cmbChkBoxGender.Items.Count - 1
                If cmbChkBoxGender.CheckBoxItems(iCount).Checked = True Then
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = cmbChkBoxGender.Tag
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Gender.GetHashCode()
                    oOtherDetail1.ItemName = cmbChkBoxGender.CheckBoxItems(iCount).Text
                    oOtherDetails.Add(oOtherDetail1)
                End If
            Next

            For iCount As Integer = 0 To cmbGender_Ex.Items.Count - 1
                If cmbGender_Ex.CheckBoxItems(iCount).Checked = True Then
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = cmbGender_Ex.Tag
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Gender.GetHashCode()
                    oOtherDetail1.ItemName = cmbGender_Ex.CheckBoxItems(iCount).Text
                    oOtherDetails_Exclusion.Add(oOtherDetail1)
                End If
            Next

            For iCount As Integer = 0 To cmbChkBoxMaritalSt.Items.Count - 1
                If cmbChkBoxMaritalSt.CheckBoxItems(iCount).Checked = True Then
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = cmbChkBoxMaritalSt.Tag
                    oOtherDetail1.DetailType = Supporting.enumDetailType.MaritalStatus.GetHashCode()
                    oOtherDetail1.ItemName = cmbChkBoxMaritalSt.CheckBoxItems(iCount).Text
                    oOtherDetails.Add(oOtherDetail1)
                End If
            Next

            For iCount As Integer = 0 To cmbChkBoxMaritalSt_Ex.Items.Count - 1
                If cmbChkBoxMaritalSt_Ex.CheckBoxItems(iCount).Checked = True Then
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = cmbChkBoxMaritalSt_Ex.Tag
                    oOtherDetail1.DetailType = Supporting.enumDetailType.MaritalStatus.GetHashCode()
                    oOtherDetail1.ItemName = cmbChkBoxMaritalSt_Ex.CheckBoxItems(iCount).Text
                    oOtherDetails_Exclusion.Add(oOtherDetail1)
                End If
            Next

            'History
            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                For Each HistoryNode As myTreeNode In CategoryNode.Nodes
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = CategoryNode.Text
                    oOtherDetail1.DetailType = Supporting.enumDetailType.History.GetHashCode()
                    oOtherDetail1.ItemName = HistoryNode.Text
                    oOtherDetails.Add(oOtherDetail1)
                Next
            Next

            'History Exclusion
            For Each CategoryNode_Ex As TreeNode In trvSelectedHistory_Ex.Nodes
                For Each HistoryNode_Ex As myTreeNode In CategoryNode_Ex.Nodes
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = CategoryNode_Ex.Text
                    oOtherDetail1.DetailType = Supporting.enumDetailType.History.GetHashCode()
                    oOtherDetail1.ItemName = HistoryNode_Ex.Text
                    oOtherDetails_Exclusion.Add(oOtherDetail1)
                Next
            Next

            'Insurance 
            For i As Integer = 0 To trvSelectedInsurance.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim thisNode As myTreeNode = CType(trvSelectedInsurance.Nodes(i), myTreeNode)
                oOtherDetail1.CategoryName = Supporting.enumDetailType.Insurance.ToString()
                oOtherDetail1.ItemName = thisNode.DrugName
                oOtherDetail1.DetailType = Supporting.enumDetailType.Insurance.GetHashCode()
                oOtherDetail1.Result1 = thisNode.NDCCode
                oOtherDetail1.Result2 = 0
                oOtherDetails.Add(oOtherDetail1)
            Next

            'Insurance Exclusion
            For i As Integer = 0 To trvSelectedInsurance_Ex.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim thisNode As myTreeNode = CType(trvSelectedInsurance_Ex.Nodes(i), myTreeNode)
                oOtherDetail1.CategoryName = Supporting.enumDetailType.Insurance.ToString()
                oOtherDetail1.ItemName = thisNode.DrugName
                oOtherDetail1.DetailType = Supporting.enumDetailType.Insurance.GetHashCode()
                oOtherDetail1.Result1 = thisNode.NDCCode
                oOtherDetail1.Result2 = 0
                oOtherDetails_Exclusion.Add(oOtherDetail1)
            Next


            'Drug
            For i As Integer = 0 To trvSelectedDrugs.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim thisNode As myTreeNode = CType(trvSelectedDrugs.Nodes(i), myTreeNode)
                oOtherDetail1.CategoryName = thisNode.DrugName
                If thisNode.DrugForm.ToString.Trim <> "" Then
                    If thisNode.Dosage.Trim <> "" Then
                        oOtherDetail1.ItemName = thisNode.Dosage & " - " & thisNode.DrugForm
                    Else
                        oOtherDetail1.ItemName = thisNode.DrugForm
                    End If
                Else
                    oOtherDetail1.ItemName = thisNode.Dosage
                End If
                oOtherDetail1.DetailType = Supporting.enumDetailType.Medication.GetHashCode()
                oOtherDetail1.Result1 = thisNode.mpid ''thisNode.NDCCode

                If Convert.ToInt32(oOtherDetail1.Result1) = 0 Then
                    oOtherDetail1.Result2 = thisNode.NDCCode
                Else
                    oOtherDetail1.Result2 = ""
                End If

                oOtherDetails.Add(oOtherDetail1)
            Next

            'Drug Exclusion
            For i As Integer = 0 To trvSelectedDrugs_Ex.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                Dim thisNode As myTreeNode = CType(trvSelectedDrugs_Ex.Nodes(i), myTreeNode)
                oOtherDetail1.CategoryName = thisNode.DrugName
                If thisNode.DrugForm.ToString.Trim <> "" Then
                    If thisNode.Dosage.Trim <> "" Then
                        oOtherDetail1.ItemName = thisNode.Dosage & " - " & thisNode.DrugForm
                    Else
                        oOtherDetail1.ItemName = thisNode.DrugForm
                    End If
                Else
                    oOtherDetail1.ItemName = thisNode.Dosage
                End If
                oOtherDetail1.DetailType = Supporting.enumDetailType.Medication.GetHashCode()
                oOtherDetail1.Result1 = thisNode.mpid ''thisNode.NDCCode

                If Convert.ToInt32(oOtherDetail1.Result1) = 0 Then
                    oOtherDetail1.Result2 = thisNode.NDCCode
                Else
                    oOtherDetail1.Result2 = ""
                End If

                oOtherDetails_Exclusion.Add(oOtherDetail1)
            Next

            'CPT
            For i As Integer = 0 To trvselectedCPT.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.CPT.GetHashCode()
                oOtherDetail1.CategoryName = CType(trvselectedCPT.Nodes(i), myTreeNode).Tag
                oOtherDetail1.ItemName = CType(trvselectedCPT.Nodes(i), myTreeNode).DrugName
                oOtherDetails.Add(oOtherDetail1)
            Next

            'CPT Exclusion
            For i As Integer = 0 To trvselectedCPT_Ex.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.CPT.GetHashCode()
                oOtherDetail1.CategoryName = CType(trvselectedCPT_Ex.Nodes(i), myTreeNode).Tag
                oOtherDetail1.ItemName = CType(trvselectedCPT_Ex.Nodes(i), myTreeNode).DrugName
                oOtherDetails_Exclusion.Add(oOtherDetail1)
            Next

            'ICD9
            For i As Integer = 0 To trvselecteICDs.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.ICD9.GetHashCode()
                oOtherDetail1.CategoryName = CType(trvselecteICDs.Nodes(i), myTreeNode).Tag
                oOtherDetail1.ItemName = CType(trvselecteICDs.Nodes(i), myTreeNode).DrugName
                oOtherDetails.Add(oOtherDetail1)
            Next

            'ICD9 Exclusion
            For i As Integer = 0 To trvselectedICDs_Ex.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.ICD9.GetHashCode()
                oOtherDetail1.CategoryName = CType(trvselectedICDs_Ex.Nodes(i), myTreeNode).Tag
                oOtherDetail1.ItemName = CType(trvselectedICDs_Ex.Nodes(i), myTreeNode).DrugName
                oOtherDetails_Exclusion.Add(oOtherDetail1)
            Next

            'ICD10
            For i As Integer = 0 To trvselecteICD10s.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.ICD10.GetHashCode()
                oOtherDetail1.CategoryName = CType(trvselecteICD10s.Nodes(i), myTreeNode).Tag
                oOtherDetail1.ItemName = CType(trvselecteICD10s.Nodes(i), myTreeNode).DrugName
                oOtherDetails.Add(oOtherDetail1)
            Next

            'ICD10 Exclusion
            For i As Integer = 0 To trvselectedICD10s_Ex.Nodes.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.DetailType = Supporting.enumDetailType.ICD10.GetHashCode()
                oOtherDetail1.CategoryName = CType(trvselectedICD10s_Ex.Nodes(i), myTreeNode).Tag
                oOtherDetail1.ItemName = CType(trvselectedICD10s_Ex.Nodes(i), myTreeNode).DrugName
                oOtherDetails_Exclusion.Add(oOtherDetail1)
            Next

            'RadiologyLAB
            Dim _dtSelected As DataTable = Nothing
            _dtSelected = getSlectedLabAndResult(c1Labs)
            If _dtSelected IsNot Nothing AndAlso _dtSelected.Rows.Count > 0 Then
                For _rowIndex As Integer = 0 To _dtSelected.Rows.Count - 1
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.ItemName = _dtSelected.Rows(_rowIndex)("Test")
                    oOtherDetail1.OperatorName = _dtSelected.Rows(_rowIndex)("Group") '' OPERATOR NAME AS GROUP NAME ''
                    oOtherDetail1.CategoryName = _dtSelected.Rows(_rowIndex)("Category")
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Order.GetHashCode()
                    oOtherDetails.Add(oOtherDetail1)
                Next
            End If

            _dtSelected.Dispose()
            _dtSelected = Nothing
            _dtSelected = getSlectedLabAndResult(c1Labs_Ex)

            If _dtSelected IsNot Nothing AndAlso _dtSelected.Rows.Count > 0 Then
                For _rowIndex As Integer = 0 To _dtSelected.Rows.Count - 1
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.ItemName = _dtSelected.Rows(_rowIndex)("Test")
                    oOtherDetail1.OperatorName = _dtSelected.Rows(_rowIndex)("Group") '' OPERATOR NAME AS GROUP NAME ''
                    oOtherDetail1.CategoryName = _dtSelected.Rows(_rowIndex)("Category")
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Order.GetHashCode()
                    oOtherDetails_Exclusion.Add(oOtherDetail1)
                Next
            End If

            'Lab
            _dtSelected.Dispose()
            _dtSelected = Nothing
            _dtSelected = getSlectedLabAndResult(C1LabResult)

            If _dtSelected IsNot Nothing AndAlso _dtSelected.Rows.Count > 0 Then
                For _rowIndex As Integer = 0 To _dtSelected.Rows.Count - 1

                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    C1LabResult.Select()
                    oOtherDetail1.ItemName = _dtSelected.Rows(_rowIndex)("Result")
                    oOtherDetail1.CategoryName = _dtSelected.Rows(_rowIndex)("Test")
                    oOtherDetail1.Result1 = _dtSelected.Rows(_rowIndex)("Result Value1")
                    oOtherDetail1.Result2 = _dtSelected.Rows(_rowIndex)("Result Value2")
                    oOtherDetail1.OperatorName = _dtSelected.Rows(_rowIndex)("Operator")
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Lab.GetHashCode()
                    oOtherDetail1.LionicCode = _dtSelected.Rows(_rowIndex)("Loinc Code")
                    oOtherDetails.Add(oOtherDetail1)
                Next
            End If

            _dtSelected.Dispose()
            _dtSelected = Nothing
            _dtSelected = getSlectedLabAndResult(C1LabResult_Ex)

            If _dtSelected IsNot Nothing AndAlso _dtSelected.Rows.Count > 0 Then
                For _rowIndex As Integer = 0 To _dtSelected.Rows.Count - 1
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    C1LabResult.Select()
                    oOtherDetail1.ItemName = _dtSelected.Rows(_rowIndex)("Result")
                    oOtherDetail1.CategoryName = _dtSelected.Rows(_rowIndex)("Test")
                    oOtherDetail1.Result1 = _dtSelected.Rows(_rowIndex)("Result Value1")
                    oOtherDetail1.Result2 = _dtSelected.Rows(_rowIndex)("Result Value2")
                    oOtherDetail1.OperatorName = _dtSelected.Rows(_rowIndex)("Operator")
                    oOtherDetail1.LionicCode = _dtSelected.Rows(_rowIndex)("Loinc Code")
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Lab.GetHashCode()
                    oOtherDetails_Exclusion.Add(oOtherDetail1)
                Next
            End If

            _dtSelected.Dispose()
            _dtSelected = Nothing

            For _SnomedCodeIndex As Integer = 0 To lstVw_SnoMed.Items.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.ItemName = lstVw_SnoMed.Items(_SnomedCodeIndex).Text  ''SubItems(_SnomedCodeIndex).Tag
                oOtherDetail1.CategoryName = lstVw_SnoMed.Items(_SnomedCodeIndex).Tag
                oOtherDetail1.DetailType = Supporting.enumDetailType.SnomedCode.GetHashCode()
                oOtherDetails.Add(oOtherDetail1)
            Next

            For _SnomedCodeIndex_Ex As Integer = 0 To lstExVw_SnoMed.Items.Count - 1
                Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                oOtherDetail1.ItemName = lstExVw_SnoMed.Items(_SnomedCodeIndex_Ex).Text '' .SubItems(_SnomedCodeIndex_Ex).Tag
                oOtherDetail1.CategoryName = lstExVw_SnoMed.Items(_SnomedCodeIndex_Ex).Tag
                oOtherDetail1.DetailType = Supporting.enumDetailType.SnomedCode.GetHashCode()
                oOtherDetails_Exclusion.Add(oOtherDetail1)
            Next

            For i As Integer = 0 To trOrderInfo.GetNodeCount(False) - 1
                For j As Integer = 0 To trOrderInfo.Nodes(i).GetNodeCount(False) - 1
                    If trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Count > 0 Then
                        For k As Integer = 0 To trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Count - 1
                            Dim thisNode As myTreeNode = CType(trOrderInfo.Nodes(i).Nodes.Item(j).Nodes.Item(k), myTreeNode)
                            If trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Orders" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                'lst.Index = thisNode.Tag
                                lst.ID = thisNode.Key
                                oCriteria.LabOrders.Add(lst)

                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Order Templates" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                'lst.Index = thisNode.Tag
                                lst.ID = thisNode.Key
                                oCriteria.RadiologyOrders.Add(lst)

                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Referrals" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                'lst.Index = thisNode.Tag
                                lst.ID = thisNode.Key
                                oCriteria.Referrals.Add(lst)

                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Guidelines" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                'lst.Index = thisNode.Tag
                                lst.ID = thisNode.Key
                                lst.DMTemplateName = thisNode.Text


                                'Get the template (description from the template_gallery table
                                If Not IsNothing(thisNode.DMTemplate) Then
                                    lst.DMTemplate = thisNode.DMTemplate
                                Else
                                    lst.DMTemplate = Nothing
                                End If
                                oCriteria.Guidelines.Add(lst)

                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "Rx" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                'lst.Index = thisNode.Tag
                                lst.ID = thisNode.Key
                                lst.DrugName = thisNode.DrugName
                                lst.Dosage = thisNode.Dosage

                                lst.DrugForm = thisNode.DrugForm
                                lst.Route = thisNode.Route
                                lst.Duration = thisNode.Duration
                                lst.Frequency = thisNode.Frequency
                                lst.NDCCode = thisNode.NDCCode
                                lst.mpid = thisNode.mpid
                                lst.IsNarcotic = thisNode.IsNarcotics
                                lst.DrugQtyQualifier = thisNode.DrugQtyQualifier
                                oCriteria.RxDrugs.Add(lst)


                            ElseIf trOrderInfo.Nodes(i).Nodes.Item(j).Text = "IM" Then
                                Dim lst As New myList
                                lst.Value = thisNode.Text
                                lst.ID = thisNode.Key
                                lst.DrugForm = thisNode.DrugForm      'ConceptID
                                lst.Duration = thisNode.Duration      'DescriptionID
                                lst.Frequency = thisNode.Frequency    'TradEName                    
                                lst.DrugQtyQualifier = thisNode.DrugQtyQualifier
                                'lst.Index = thisNode.Tag              ' IM ID
                                lst.Route = thisNode.Route              ' Orignal Vaccine name
                                lst.NDCCode = thisNode.NDCCode  '' Manufacturer
                                oCriteria.IMlst.Add(lst)
                            End If
                        Next
                    End If
                Next
            Next


            For Each CategoryNode As TreeNode In trvselectedprob.Nodes
                For Each ProblemNode As TreeNode In CategoryNode.Nodes
                    Dim oOtherDetail1 As New gloStream.DiseaseManagement.Supporting.OtherDetail
                    oOtherDetail1.CategoryName = CategoryNode.Text
                    oOtherDetail1.DetailType = Supporting.enumDetailType.Problemlist.GetHashCode()
                    oOtherDetail1.ItemName = ProblemNode.Text
                    oOtherDetails.Add(oOtherDetail1)
                Next
            Next

            oCriteria.OtherDetails = oOtherDetails
            oCriteria_Exclusion.OtherDetails = oOtherDetails_Exclusion

            If m_CriteriaId > 0 Then
                oDM.AddRuleHistory(gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, "Rule ""Name : " & txtName.Text.Trim() & """ Modified", System.Environment.MachineName, m_CriteriaId)
            Else
                oDM.AddRuleHistory(gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, "New Rule ""Name : " & txtName.Text.Trim() & """ Created", System.Environment.MachineName, m_CriteriaId)
            End If

            _ResultCriteriaID = oDM.SaveCriteria(m_CriteriaId, oCriteria, LoadFirst, blsCopyRule)
            Dim oDM_Exclusion As New gloStream.DiseaseManagement.DiseaseManagement
            oDM_Exclusion.SaveException(_ResultCriteriaID, oCriteria_Exclusion, LoadFirst)
            oDM_Exclusion.Dispose()
            oDM_Exclusion = Nothing
            If m_CriteriaId = 0 And Not blsCopyRule Then
                oDM.AddRuleHistory(gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, "New Rule ""Name : " & txtName.Text.Trim() & """ Created", System.Environment.MachineName, _ResultCriteriaID)
            End If

            If m_CriteriaId = 0 And blsCopyRule Then
                oDM.AddRuleHistory(gloEMR.mdlGeneral.gnLoginID, gloEMR.mdlGeneral.gstrLoginName, "Copy Rule ""Name : " & txtName.Text.Trim() & """ Created", System.Environment.MachineName, _ResultCriteriaID)
            End If

            If _ResultCriteriaID > 0 Then
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If



            If oCriteria IsNot Nothing Then
                'gloStream.DiseaseManagement.Supporting.Criteria does not have idisposable interface implemented 
                oCriteria.Dispose()
                oCriteria = Nothing
            End If

            If oCriteria_Exclusion IsNot Nothing Then
                'gloStream.DiseaseManagement.Supporting.Criteria does not have idisposable interface implemented 
                oCriteria_Exclusion.Dispose()
                oCriteria_Exclusion = Nothing
            End If

            If oOtherDetails IsNot Nothing Then
                'gloStream.DiseaseManagement.Supporting.OtherDetails does not have idisposable interface implemented
                oOtherDetails.Dispose()
                oOtherDetails = Nothing
            End If

            If oOtherDetails_Exclusion IsNot Nothing Then
                'gloStream.DiseaseManagement.Supporting.OtherDetails does not have idisposable interface implemented
                oOtherDetails_Exclusion.Dispose()
                oOtherDetails_Exclusion = Nothing
            End If

        End Try
    End Sub

    Public Sub PopulateAssocaitedInfo(ByVal ID As Int32)

        Try
            pnlbtnLab.Dock = DockStyle.Bottom
            btnLab.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnLab.BackgroundImageLayout = ImageLayout.Stretch
            btnLab.Tag = "UnSelected"

            pnlbtnReferrals.Dock = DockStyle.Bottom
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
            btnReferrals.Tag = "UnSelected"

            pnlbtnRx.Dock = DockStyle.Bottom
            btnRx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnRx.BackgroundImageLayout = ImageLayout.Stretch
            btnRx.Tag = "UnSelected"

            pnlbtnRadiologyTest.Dock = DockStyle.Bottom
            btnRadiologyTest.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnRadiologyTest.BackgroundImageLayout = ImageLayout.Stretch
            btnRadiologyTest.Tag = "UnSelected"

            pnlbtnGuideline.Dock = DockStyle.Bottom
            btnGuideline.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
            btnGuideline.Tag = "UnSelected"

            '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
            pnlIM.Dock = DockStyle.Bottom
            btnIM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnIM.BackgroundImageLayout = ImageLayout.Stretch
            btnIM.Tag = "UnSelected"
            '''''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup



            If ID = 1 Then
                With btnLab
                    pnlbtnLab.Dock = DockStyle.Top
                    .Tag = "Selected"
                    '.ForeColor = Color.Black
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BringToFront()
                End With
                'trvTriggers.BringToFront()
                FillLabTest()
            ElseIf ID = 2 Then

                With btnReferrals
                    pnlbtnReferrals.Dock = DockStyle.Top
                    .Tag = "Selected"
                    '.ForeColor = Color.Black
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BringToFront()
                End With
                '  trvTriggers.BringToFront()
                FillReferrals()
            ElseIf ID = 3 Then
                With btnRx
                    pnlbtnRx.Dock = DockStyle.Top
                    .Tag = "Selected"
                    '.ForeColor = Color.Black
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BringToFront()
                End With
                ' trvTriggers.BringToFront()
                FillRx()
            ElseIf ID = 4 Then
                With btnRadiologyTest
                    pnlbtnRadiologyTest.Dock = DockStyle.Top
                    .Tag = "Selected"
                    '.ForeColor = Color.Black
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BringToFront()
                End With
                '  trvTriggers.BringToFront()
                FillRadiologyTest()
            ElseIf ID = 5 Then
                With btnGuideline
                    pnlbtnGuideline.Dock = DockStyle.Top
                    .Tag = "Selected"
                    '.ForeColor = Color.Black
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BringToFront()
                End With
                ' trvTriggers.BringToFront()
                fill_guideline()

            ElseIf ID = 6 Then
                With btnIM
                    pnlIM.Dock = DockStyle.Top
                    .Tag = "Selected"
                    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    .BackgroundImageLayout = ImageLayout.Stretch
                    .BringToFront()
                End With
                fill_IM()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fillProblemlistSnomadeoff(Optional ByVal strsearch As String = "")
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim dtProblemlist As DataTable = Nothing
        Try
            dtProblemlist = oDM.GetProblemList(strsearch)
            If Not dtProblemlist Is Nothing Then
                trvSnowmedOff.Nodes.Clear()
                For Each dr As DataRow In dtProblemlist.Rows
                    trvSnowmedOff.Nodes.Add(dr(0))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If
            If dtProblemlist IsNot Nothing Then
                dtProblemlist.Dispose()
                dtProblemlist = Nothing
            End If
        End Try
    End Sub

    Private Sub Search(ByVal searchtextbox As TextBox, ByVal searchtreeview As TreeView)
        Try
            'check for text to be search
            If Trim(searchtextbox.Text) <> "" Then
                If (IsNothing(searchtreeview) = False) Then
                    If searchtreeview.GetNodeCount(False) > 0 Then
                        Dim mychildnode As TreeNode
                        'child node collection
                        For i As Integer = 0 To searchtreeview.Nodes.Count - 1
                            ''For Each mychildnode In searchtreeview.Nodes.Item(i).Nodes ''Commented Sandip Darade 
                            For Each mychildnode In searchtreeview.Nodes  ''Sandip Darade 
                                Dim str As String
                                str = UCase(Trim(mychildnode.Text))
                                If Mid(str, 1, Len(Trim(searchtextbox.Text))) = UCase(Trim(searchtextbox.Text)) Then
                                    searchtreeview.SelectedNode = searchtreeview.Nodes(searchtreeview.Nodes.Count - 1)
                                    searchtreeview.SelectedNode = mychildnode
                                    searchtextbox.Focus()
                                    Exit Sub
                                End If
                            Next
                        Next
                    End If
                End If


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function GetHistoryCategory() As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim dt As DataTable = Nothing
        Dim Query As String = ""
        Try
            oDB.Connect(GetConnectionString)
            Query = "SELECT nCategoryID, sDescription FROM Category_MST WHERE (sCategoryType = 'History') ORDER BY sDescription"
            dt = oDB.ReadQueryDataTable(Query)
            If Not IsNothing(dt) Then
                Return dt
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub Fill_Histories(Optional ByVal HistoryCategory As String = "")
        Dim oHistory As gloStream.DiseaseManagement.Supporting.History = Nothing
        Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
        Dim dtHistoryCategory As DataTable = Nothing
        txtSearch.Text = ""
        Try
            If HistoryCategory = "" Then
                dtHistoryCategory = GetHistoryCategory()
                cmbHistoryCategory.DataSource = dtHistoryCategory
                cmbHistoryCategory.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
                cmbHistoryCategory.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
                Fill_Histories(dtHistoryCategory.Rows(0)("sDescription").ToString)
            Else
                trvHistoryRight.Nodes.Clear()
                Dim oNode As myTreeNode
                If HistoryCategory = "Allergies" Then
                    oHistory = oDm.Histories(HistoryCategory)
                    If Not oHistory Is Nothing Then
                        For i As Integer = 1 To oHistory.Items.Count
                            oNode = New myTreeNode
                            oNode.Text = oHistory.Items(i).Name
                            oNode.Tag = HistoryCategory
                            oNode.Key = oHistory.Items(i).ID
                            trvHistoryRight.Nodes.Add(oNode)
                            oNode = Nothing
                        Next
                        oHistory.Dispose()
                        oHistory = Nothing
                    End If
                ElseIf gblnCodedHistory = True Then
                    dt = oDm.GetHistoriesDataTable(HistoryCategory)

                    If Not dt Is Nothing Then

                        Dim Rowcount As Integer
                        If dt.Rows.Count > 50 Then
                            Rowcount = 50
                        Else
                            Rowcount = dt.Rows.Count - 1
                        End If

                        For i As Integer = 0 To Rowcount
                            oNode = New myTreeNode
                            oNode.Text = dt.Rows(i)("Column1")
                            oNode.Tag = HistoryCategory
                            oNode.Key = dt.Rows(i)("ICD9ID")
                            trvHistoryRight.Nodes.Add(oNode)
                            oNode = Nothing
                        Next
                    End If
                Else
                    oHistory = oDm.Histories(HistoryCategory)
                    If Not oHistory Is Nothing Then
                        For i As Integer = 1 To oHistory.Items.Count
                            oNode = New myTreeNode
                            oNode.Text = oHistory.Items(i).Name
                            oNode.Tag = HistoryCategory
                            oNode.Key = oHistory.Items(i).ID
                            trvHistoryRight.Nodes.Add(oNode)
                            oNode = Nothing
                        Next
                        oHistory.Dispose()
                        oHistory = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oHistory IsNot Nothing Then
                oHistory.Dispose()
                oHistory = Nothing
            End If
            If oDm IsNot Nothing Then
                oDm.Dispose()
                oDm = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_Histories_1_Ex(Optional ByVal HistoryCategory As String = "")
        'Dim oHistory As New gloStream.DiseaseManagement.Supporting.History
        'Dim oCategories As New gloStream.DiseaseManagement.Supporting.Categories
        Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
        Dim dtHistoryCategory As DataTable = Nothing
        txtSearch_Ex.Text = ""

        Try
            If HistoryCategory = "" Then
                dtHistoryCategory = GetHistoryCategory()
                cmbHistoryCategory_Ex.DataSource = dtHistoryCategory
                cmbHistoryCategory_Ex.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
                cmbHistoryCategory_Ex.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
                Fill_Histories_1_Ex(dtHistoryCategory.Rows(0)("sDescription").ToString)
            Else
                ' Dim oNode As myTreeNode
                dt = oDm.GetHistoriesDataTable(HistoryCategory)
                If Not dt Is Nothing Then
                    GloUC_TrvHistoryEx.DataSource = dt
                    GloUC_TrvHistoryEx.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
                    GloUC_TrvHistoryEx.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
                    GloUC_TrvHistoryEx.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)

                    If dt.Columns.Contains("AllergyClassID") Then
                        GloUC_TrvHistoryEx.AllergyClassID = Convert.ToString(dt.Columns("AllergyClassID").ColumnName)
                    End If

                    If HistoryCategory = "Allergies" Then
                        GloUC_TrvHistoryEx.Tag = Convert.ToString(dt.Columns("IsDrug").ColumnName)
                    Else
                        GloUC_TrvHistoryEx.Tag = ""
                    End If
                    GloUC_TrvHistoryEx.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                    GloUC_TrvHistoryEx.FillTreeView()
                    GloUC_TrvHistoryEx.FocusSearchBox()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   If oHistory IsNot Nothing Then
            'oHistory = Nothing
            'End If
            'If oCategories IsNot Nothing Then
            'oCategories = Nothing
            'End If
            If oDm IsNot Nothing Then
                oDm.Dispose()
                oDm = Nothing
            End If
            If dtHistoryCategory IsNot Nothing Then
                dtHistoryCategory = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_Histories_old(Optional ByVal HistoryCategory As String = "")
        Dim oHistory As gloStream.DiseaseManagement.Supporting.History
        Dim oCategories As New gloStream.DiseaseManagement.Supporting.Categories
        Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
        Dim dtHistoryCategory As DataTable

        If HistoryCategory = "" Then
            dtHistoryCategory = GetHistoryCategory()
            cmbHistoryCategory.DataSource = dtHistoryCategory
            cmbHistoryCategory.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
            cmbHistoryCategory.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
            Fill_Histories(dtHistoryCategory.Rows(0)("sDescription").ToString)
        Else
            trvHistoryRight.Nodes.Clear()
            Dim oNode As myTreeNode
            oHistory = oDm.Histories(HistoryCategory)
            If Not oHistory Is Nothing Then
                For i As Integer = 1 To oHistory.Items.Count
                    oNode = New myTreeNode
                    oNode.Text = oHistory.Items(i).Name
                    oNode.Tag = HistoryCategory
                    oNode.Key = oHistory.Items(i).ID
                    trvHistoryRight.Nodes.Add(oNode)
                    oNode = Nothing
                Next
            End If
            '''''This function is not in use
        End If
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Function GetFtInch(ByVal strHeight As String) As Array
        strHeight = Mid(strHeight, 1, Len(strHeight) - 1)
        Return Split(strHeight, "'", , CompareMethod.Text)
    End Function

    Private Function GetTemplate(ByVal TemplateID As Int64) As Object
        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim img As Object
        Try
            img = oDM.GetTemplate(TemplateID)
            Return img
        Catch ex As Exception
            Return Nothing
        Finally
            oDM.Dispose()
        End Try

    End Function

    Private Sub FillAllCriteria()
        Try
            If LoadFirst = False Then
                Fill_Labs_ByTable()
                Fill_RadiologyLabs_ByTable()
                Fill_OtherInfo()
                Fill_CriteriaDetails(m_CriteriaId)
            End If
        Catch ex As Exception
        Finally
            LoadFirst = True
        End Try
    End Sub

    Private Function RemoveSelectedItemFromList(ByRef lstView As ListView) As Boolean
        Dim _isItemRemoved As Boolean = False
        Try
            If Not IsNothing(lstView) AndAlso Not IsNothing(lstView.Items) AndAlso lstView.Items.Count > 0 AndAlso lstView.SelectedItems.Count > 0 Then
                For Each item As ListViewItem In lstView.SelectedItems
                    lstView.Items.Remove(item)
                    _isItemRemoved = True
                Next
            Else
                MessageBox.Show("Select item to be removed from the list or no items present to remove.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            _isItemRemoved = False
        Finally
        End Try
        Return _isItemRemoved
    End Function

    Private Function AddItemToList(ByRef lstView As ListView, ByVal lstViewItem As ListViewItem) As Boolean
        Dim _isItemAdded As Boolean = False
        Dim Ispresent As Boolean = False
        Try
            If Not IsNothing(lstViewItem) Then
                For Each myItems As ListViewItem In lstView.Items
                    If myItems.Tag = lstViewItem.Tag Then
                        Ispresent = True
                        Exit For
                    End If
                Next
                If Ispresent = False Then
                    lstView.Items.Add(lstViewItem)
                    _isItemAdded = True
                End If
            End If
        Catch ex As Exception
            _isItemAdded = False
        Finally
        End Try
        Return _isItemAdded
    End Function

    Private Sub ShowSnoMedSelector(ByVal tab As TabType)
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem(Me.Text, gstrSMDBConnstr, GetConnectionString())
        Dim _lstVwitem As ListViewItem = Nothing
        Dim str As String = ""

        Try
            ' frm.StartPosition = FormStartPosition.CenterScreen
            ' frm.ShowInTaskbar = False
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

            If frm._DialogResult Then
                If frm.strConceptID.Trim() <> "" Then
                    _lstVwitem = New ListViewItem()
                    _lstVwitem.Text = frm.strSelectedConceptID + "-" + frm.strSelectedDescription
                    _lstVwitem.Tag = frm.strSelectedConceptID
                    _lstVwitem.SubItems.Add(frm.strSelectedDescription)
                    If tab = TabType.Trigger Then
                        AddItemToList(lstVw_SnoMed, _lstVwitem)
                    ElseIf tab = TabType.Exception Then
                        AddItemToList(lstExVw_SnoMed, _lstVwitem)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _lstVwitem = Nothing
            frm.Dispose()
        End Try
    End Sub

#Region " Fill control methods "

    Private Sub FillCriteriaRefInfo()
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim _dtCriteriaRefInfo As DataTable = Nothing
        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            _dtCriteriaRefInfo = oDM.GetCriteriaRefInfo(m_CriteriaId)
            If _dtCriteriaRefInfo IsNot Nothing AndAlso _dtCriteriaRefInfo.Rows.Count > 0 Then
                txtBibliographicCitation.Text = _dtCriteriaRefInfo(0)("sBibliographicCitatation").ToString()
                txtInterventionDeveloper.Text = _dtCriteriaRefInfo(0)("sInterventionDeveloper").ToString()
                txtFundingSource.Text = _dtCriteriaRefInfo(0)("sFundingSource").ToString()
                txtRelease.Text = _dtCriteriaRefInfo(0)("sRelease").ToString()
                txtRevisionDates.Text = _dtCriteriaRefInfo(0)("sRevisionDates").ToString()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            C1LabResult.EndUpdate()
            C1LabResult_Ex.EndUpdate()
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If
            If Not IsNothing(_dtCriteriaRefInfo) Then
                _dtCriteriaRefInfo.Dispose()
                _dtCriteriaRefInfo = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_Labs_ByTable()
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim _dtLabsTest As DataTable = Nothing
        Dim _dtExLabsTest As DataTable = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            _dtLabsTest = oDM.GetLabModuleTestsTable(m_CriteriaId)
            _dtExLabsTest = oDM.GeExLabModuleTestsTable(m_CriteriaId)

            If Not IsNothing(_dtLabsTest) Then
                'C1LabResult.Clear()
                C1LabResult.DataSource = Nothing
                C1LabResult.BeginUpdate()
                C1LabResult.DataSource = _dtLabsTest.Copy()
                C1LabResult.EndUpdate()
                DesignLabsGridByTable(C1LabResult)
                SetLabs_ByTable()
            End If

            If Not IsNothing(_dtExLabsTest) Then
                'C1LabResult_Ex.Clear()
                C1LabResult_Ex.DataSource = Nothing
                C1LabResult_Ex.BeginUpdate()
                C1LabResult_Ex.DataSource = _dtExLabsTest.Copy()
                C1LabResult_Ex.EndUpdate()
                DesignLabsGridByTable(C1LabResult_Ex)
                SetEXLabs_ByTable()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            C1LabResult.EndUpdate()
            C1LabResult_Ex.EndUpdate()
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If
            If Not IsNothing(_dtLabsTest) Then
                _dtLabsTest.Dispose()
                _dtLabsTest = Nothing
            End If
            If Not IsNothing(_dtExLabsTest) Then
                _dtExLabsTest.Dispose()
                _dtExLabsTest = Nothing
            End If
        End Try
    End Sub

    Private Sub DesignLabsGridByTable(ByRef c1FlexGrid)
        If c1FlexGrid.Rows.Count > 1 Then
            Dim strOperator As String = "Greater Than" & "|" & "Less Than" & "|" & "Between" & "|" & "Equals"
            Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            Dim rgOperator As C1.Win.C1FlexGrid.CellRange = Nothing
            Try
                c1FlexGrid.Cols("Value").DataType = GetType(Boolean)
                c1FlexGrid.Cols("LabId").DataType = GetType(Int64)
                c1FlexGrid.Cols("Test").DataType = GetType(String)
                c1FlexGrid.Cols("ResultID").DataType = GetType(Int64)
                c1FlexGrid.Cols("Result").DataType = GetType(String)
                c1FlexGrid.Cols("Operator").DataType = GetType(String)
                c1FlexGrid.Cols("Result Value1").DataType = GetType(String)
                c1FlexGrid.Cols("Result Value2").DataType = GetType(String)
                c1FlexGrid.Cols("LabId").Visible = False
                c1FlexGrid.Cols("ResultID").Visible = False

                rgOperator = c1FlexGrid.GetCellRange(1, c1FlexGrid.Cols("Operator").Index, c1FlexGrid.Rows.Count - 1, c1FlexGrid.Cols("Operator").Index)
                '  cStyle = c1FlexGrid.Styles.Add("Operator")
                Try
                    If (c1FlexGrid.Styles.Contains("Operator")) Then
                        cStyle = c1FlexGrid.Styles("Operator")
                    Else
                        cStyle = c1FlexGrid.Styles.Add("Operator")

                    End If
                Catch ex As Exception
                    cStyle = c1FlexGrid.Styles.Add("Operator")

                End Try
                cStyle.ComboList = strOperator
                rgOperator.Style = cStyle

                c1FlexGrid.Cols(0).Width = 40
                c1FlexGrid.Cols("Test").Width = 290
                c1FlexGrid.Cols("Result").Width = 290
                c1FlexGrid.Cols("Operator").Width = 100
                c1FlexGrid.Cols("Result Value1").Width = 115
                c1FlexGrid.Cols("Result Value2").Width = 115

                c1FlexGrid.ExtendLastCol = False
                c1FlexGrid.Cols("Test").AllowEditing = False
                c1FlexGrid.Cols("Result").AllowEditing = False
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                strOperator = Nothing
                cStyle = Nothing
                rgOperator = Nothing
            End Try
        End If
    End Sub

    Private Sub Fill_RadiologyLabs_ByTable()
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim _dtRadiologyOrders As DataTable = Nothing
        Dim _dtExRadiologyOrders As DataTable = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            _dtRadiologyOrders = oDM.GetOrdersTable(m_CriteriaId)
            _dtExRadiologyOrders = oDM.GetExOrdersTable(m_CriteriaId)

            If Not IsNothing(_dtRadiologyOrders) Then
                'c1Labs.Clear()
                c1Labs.DataSource = Nothing
                c1Labs.BeginUpdate()
                c1Labs.DataSource = _dtRadiologyOrders.Copy()
                c1Labs.EndUpdate()
                DesignRadiologyGridByTable(c1Labs)
                SetOrders_ByTable()
            End If

            If Not IsNothing(_dtExRadiologyOrders) Then
                'c1Labs_Ex.Clear()
                c1Labs_Ex.DataSource = Nothing
                c1Labs_Ex.BeginUpdate()
                c1Labs_Ex.DataSource = _dtExRadiologyOrders.Copy()
                c1Labs_Ex.EndUpdate()
                DesignRadiologyGridByTable(c1Labs_Ex)
                SetExOrders_ByTable()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            c1Labs.EndUpdate()
            c1Labs_Ex.EndUpdate()

            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If Not IsNothing(_dtRadiologyOrders) Then
                _dtRadiologyOrders.Dispose()
                _dtRadiologyOrders = Nothing
            End If
            If Not IsNothing(_dtExRadiologyOrders) Then
                _dtExRadiologyOrders.Dispose()
                _dtExRadiologyOrders = Nothing
            End If
        End Try
    End Sub

    Private Sub DesignRadiologyGridByTable(ByRef c1FlexGrid)
        Try
            c1FlexGrid.Cols("Value").DataType = GetType(Boolean)
            c1FlexGrid.Cols("CategoryId").DataType = GetType(Int64)
            c1FlexGrid.Cols("Category").DataType = GetType(String)
            c1FlexGrid.Cols("GroupId").DataType = GetType(Int64)
            c1FlexGrid.Cols("Group").DataType = GetType(String)
            c1FlexGrid.Cols("TestId").DataType = GetType(Int64)
            c1FlexGrid.Cols("Test").DataType = GetType(String)
            c1FlexGrid.Cols("CategoryId").Visible = False
            c1FlexGrid.Cols("GroupId").Visible = False
            c1FlexGrid.Cols("TestId").Visible = False
            c1FlexGrid.Cols("Value").Width = 50
            c1FlexGrid.Cols("Category").Width = 250
            c1FlexGrid.Cols("Group").Width = 250
            c1FlexGrid.ExtendLastCol = True
            c1FlexGrid.Cols("Category").AllowEditing = False
            c1FlexGrid.Cols("Group").AllowEditing = False
            c1FlexGrid.Cols("Test").AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub Fill_OtherInfo()
        Dim associatenode As myTreeNode = Nothing
        Dim MyChild As myTreeNode = Nothing

        Try
            associatenode = New myTreeNode("Orders", -1)

            MyChild = New myTreeNode
            MyChild.Text = "Orders"
            MyChild.Key = -1
            MyChild.ImageIndex = 6
            MyChild.SelectedImageIndex = 6
            associatenode.Nodes.Add(MyChild)
            MyChild = Nothing

            MyChild = New myTreeNode
            MyChild.Text = "Order Templates"
            MyChild.Key = -1
            MyChild.ImageIndex = 7
            MyChild.SelectedImageIndex = 7
            associatenode.Nodes.Add(MyChild)
            MyChild = Nothing

            MyChild = New myTreeNode
            MyChild.Text = "Guidelines"
            MyChild.Key = -1
            MyChild.ImageIndex = 8
            MyChild.SelectedImageIndex = 8
            associatenode.Nodes.Add(MyChild)
            MyChild = Nothing

            MyChild = New myTreeNode
            MyChild.Text = "Rx"
            MyChild.Key = -1
            MyChild.ImageIndex = 9
            MyChild.SelectedImageIndex = 9
            associatenode.Nodes.Add(MyChild)
            MyChild = Nothing

            MyChild = New myTreeNode
            MyChild.Text = "Referrals"
            MyChild.Key = -1
            MyChild.ImageIndex = 10
            MyChild.SelectedImageIndex = 10
            associatenode.Nodes.Add(MyChild)
            MyChild = Nothing

            MyChild = New myTreeNode
            MyChild.Text = "IM"
            MyChild.Key = -1
            MyChild.ImageIndex = 12
            MyChild.SelectedImageIndex = 12
            associatenode.Nodes.Add(MyChild)
            MyChild = Nothing

            trOrderInfo.BeginUpdate()
            trOrderInfo.Nodes.Add(associatenode)
            trOrderInfo.EndUpdate()
            trOrderInfo.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            MyChild = Nothing
            associatenode = Nothing
        End Try
    End Sub

    Public Sub FillRadiologyTest()
        Dim dtOrders As DataTable = Nothing
        Dim oCls As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            oCls = New gloStream.DiseaseManagement.Common.Criteria
            dtOrders = oCls.OrdersTable

            If Not IsNothing(dtOrders) Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dtOrders.Copy()
                GloUC_trvAssociates.CodeMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                GloUC_trvAssociates.ValueMember = Convert.ToString(dtOrders.Columns(0).ColumnName)
                GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                GloUC_trvAssociates.FillTreeView()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Arrow
            oCls.Dispose()
            oCls = Nothing
            If Not IsNothing(dtOrders) Then
                dtOrders.Dispose()
                dtOrders = Nothing
            End If
        End Try
    End Sub

    Public Sub FillReferrals()
        Dim objTemplateGallery As clsTemplateGallery = Nothing
        Dim objCategory As myTreeNode = Nothing
        Dim dvTemplate As DataView = Nothing
        Dim dtCategory As DataTable = Nothing
        Dim ValueMember As Int64 = 0
        Dim DisplayMember As String = ""
        Dim dtTemplate As DataTable = Nothing

        Try
            objTemplateGallery = New clsTemplateGallery
            dtCategory = objTemplateGallery.GetAllCategory

            For i As Integer = 0 To dtCategory.Rows.Count - 1
                ValueMember = 0
                DisplayMember = ""

                ValueMember = dtCategory.Rows(i)(0)
                DisplayMember = dtCategory.Rows(i)(1)

                If DisplayMember = "Referral Letter" Then
                    objCategory = New myTreeNode(DisplayMember, ValueMember)
                    dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                    dtTemplate = dvTemplate.Table
                    If Not dtTemplate Is Nothing Then
                        GloUC_trvAssociates.Clear()
                        GloUC_trvAssociates.DataSource = dtTemplate.Copy()
                        GloUC_trvAssociates.ValueMember = dtTemplate.Columns(0).ColumnName
                        GloUC_trvAssociates.DescriptionMember = dtTemplate.Columns(1).ColumnName
                        GloUC_trvAssociates.CodeMember = dtTemplate.Columns(1).ColumnName
                        GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                        GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                        GloUC_trvAssociates.FillTreeView()
                    End If
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing

            objCategory = Nothing

            If Not IsNothing(dtTemplate) Then
                dtTemplate.Dispose()
            End If

            If Not IsNothing(dtCategory) Then
                dtCategory.Dispose()
            End If

            If Not IsNothing(dvTemplate) Then
                dvTemplate.Dispose()
            End If

        End Try
    End Sub

    Private Sub Fill_Age()

        Dim oCollection As Collection = Nothing
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim oSett As gloSettings.GeneralSettings = Nothing
        Dim objVal As Object = Nothing
        Dim _result As Boolean = False

        Try

            objVal = New Object()
            oSett = New gloSettings.GeneralSettings(GetConnectionString())
            oSett.GetSetting("PEDIATRICS", objVal)

            If Not IsNothing(objVal) AndAlso objVal.ToString() <> "" Then
                _result = CType(objVal, Boolean)
            End If

           
            'cmbAgeMinMnth.Enabled = _result
            'cmbAgeMaxMnth.Enabled = _result
            'cmbAgeMinMnth_Ex.Enabled = _result
            'cmbAgeMaxMnth_Ex.Enabled = _result

            oDM = New gloStream.DiseaseManagement.Common.Criteria
            '' oCollection = New Collection
            oCollection = oDM.Age

            cmbAgeMin.BeginUpdate()
            cmbAgeMax.BeginUpdate()
            cmbAgeMin_Ex.BeginUpdate()
            cmbAgeMax_Ex.BeginUpdate()

            If Not IsNothing(oCollection) AndAlso oCollection.Count > 0 Then
                cmbAgeMin.Items.Clear()
                cmbAgeMax.Items.Clear()
                cmbAgeMin_Ex.Items.Clear()
                cmbAgeMin_Ex.Items.Clear()
                cmbAgeMinMnth.Items.Clear()
                cmbAgeMaxMnth.Items.Clear()
                cmbAgeMinMnth_Ex.Items.Clear()
                cmbAgeMaxMnth_Ex.Items.Clear()

                For i As Int16 = 1 To oCollection.Count - 1
                    cmbAgeMin.Items.Add(oCollection(i))
                    cmbAgeMax.Items.Add(oCollection(i))
                    cmbAgeMin_Ex.Items.Add(oCollection(i))
                    cmbAgeMax_Ex.Items.Add(oCollection(i))
                Next
            End If

            cmbAgeMin.Items.RemoveAt(0)
            cmbAgeMax.Items.RemoveAt(0)
            cmbAgeMin_Ex.Items.RemoveAt(0)
            cmbAgeMax_Ex.Items.RemoveAt(0)

            cmbAgeMin.Items.Insert(0, "")
            cmbAgeMax.Items.Insert(0, "")
            cmbAgeMin_Ex.Items.Insert(0, "")
            cmbAgeMax_Ex.Items.Insert(0, "")

            'For i As Int16 = 0 To 11
            cmbAgeMinMnth.Items.Clear()
            cmbAgeMinMnth.Items.Add("")
            cmbAgeMinMnth.Items.Add("01")
            cmbAgeMinMnth.Items.Add("02")
            cmbAgeMinMnth.Items.Add("03")
            cmbAgeMinMnth.Items.Add("04")
            cmbAgeMinMnth.Items.Add("05")
            cmbAgeMinMnth.Items.Add("06")
            cmbAgeMinMnth.Items.Add("07")
            cmbAgeMinMnth.Items.Add("08")
            cmbAgeMinMnth.Items.Add("09")
            cmbAgeMinMnth.Items.Add("10")
            cmbAgeMinMnth.Items.Add("11")

            For itemIndex As Int16 = 0 To cmbAgeMinMnth.Items.Count - 1
                cmbAgeMinMnth_Ex.Items.Add(cmbAgeMinMnth.Items(itemIndex))
            Next

            cmbAgeMaxMnth.Items.Clear()
            cmbAgeMaxMnth.Items.Add("")
            cmbAgeMaxMnth.Items.Add("01")
            cmbAgeMaxMnth.Items.Add("02")
            cmbAgeMaxMnth.Items.Add("03")
            cmbAgeMaxMnth.Items.Add("04")
            cmbAgeMaxMnth.Items.Add("05")
            cmbAgeMaxMnth.Items.Add("06")
            cmbAgeMaxMnth.Items.Add("07")
            cmbAgeMaxMnth.Items.Add("08")
            cmbAgeMaxMnth.Items.Add("09")
            cmbAgeMaxMnth.Items.Add("10")
            cmbAgeMaxMnth.Items.Add("11")

            For itemIndex As Int16 = 0 To cmbAgeMaxMnth.Items.Count - 1
                cmbAgeMaxMnth_Ex.Items.Add(cmbAgeMaxMnth.Items(itemIndex))
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmbAgeMin.EndUpdate()
            cmbAgeMax.EndUpdate()
            cmbAgeMin_Ex.EndUpdate()
            cmbAgeMax_Ex.EndUpdate()
            oCollection.Clear()
            oCollection = Nothing
            oDM.Dispose()
            oDM = Nothing
            If Not IsNothing(oSett) Then
                oSett.Dispose()
            End If
            objVal = Nothing
        End Try
    End Sub

    Private Sub fill_Maritalst()
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim oCollection As Collection = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            oCollection = oDM.MaritalStatus
            cmbChkBoxMaritalSt.BeginUpdate()
            cmbChkBoxMaritalSt_Ex.BeginUpdate()
            cmbChkBoxMaritalSt.Items.Clear()
            cmbChkBoxMaritalSt_Ex.Items.Clear()

            If Not IsNothing(oCollection) AndAlso oCollection.Count > 0 Then
                For i As Int16 = 1 To oCollection.Count
                    cmbChkBoxMaritalSt.Items.Add(oCollection(i))
                    cmbChkBoxMaritalSt_Ex.Items.Add(oCollection(i))
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmbChkBoxMaritalSt.EndUpdate()
            cmbChkBoxMaritalSt_Ex.EndUpdate()

            If Not IsNothing(oDM) Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If Not IsNothing(oCollection) Then
                oCollection.Clear()
                oCollection = Nothing
            End If
        End Try
    End Sub

    Private Sub fill_gender()
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim oCollection As Collection = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            oCollection = oDM.Gender
            cmbChkBoxGender.BeginUpdate()
            cmbGender_Ex.BeginUpdate()
            cmbChkBoxGender.Items.Clear()
            cmbGender_Ex.Items.Clear()

            For i As Int16 = 1 To oCollection.Count
                cmbChkBoxGender.Items.Add(oCollection(i))
                cmbGender_Ex.Items.Add(oCollection(i))
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmbChkBoxGender.EndUpdate()
            cmbGender_Ex.EndUpdate()
            If Not IsNothing(oDM) Then
                oDM.Dispose()
                oDM = Nothing
            End If
            If Not IsNothing(oCollection) Then
                oCollection.Clear()
                oCollection = Nothing
            End If
        End Try
    End Sub

    Private Sub fill_race()
        Dim oCollectection As Collection = Nothing
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Try

            oDM = New gloStream.DiseaseManagement.Common.Criteria
            oCollectection = oDM.Race
            cmbChkBoxRace.BeginUpdate()
            cmbChkBoxRace_Ex.BeginUpdate()
            cmbChkBoxRace.Items.Clear()
            cmbChkBoxRace_Ex.Items.Clear()

            For i As Int16 = 1 To oCollectection.Count
                cmbChkBoxRace.Items.Add(oCollectection(i))
                cmbChkBoxRace_Ex.Items.Add(oCollectection(i))
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            cmbChkBoxRace.EndUpdate()
            cmbChkBoxRace_Ex.EndUpdate()

            If Not IsNothing(oDM) Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If Not IsNothing(oCollectection) Then
                oCollectection.Clear()
                oCollectection = Nothing
            End If
        End Try
    End Sub

    Private Sub fill_state()
        Dim oCollectection1 As Collection = Nothing
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            oCollectection1 = oDM.State
            cmbState.BeginUpdate()
            cmbState_Ex.BeginUpdate()
            cmbState.Items.Clear()
            cmbState_Ex.Items.Clear()

            If Not IsNothing(oCollectection1) AndAlso oCollectection1.Count > 0 Then
                cmbState.Items.Add("")
                cmbState_Ex.Items.Add("")
                For i As Integer = 1 To oCollectection1.Count
                    cmbState.Items.Add(oCollectection1(i))
                    cmbState_Ex.Items.Add(oCollectection1(i))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmbState.EndUpdate()
            cmbState_Ex.EndUpdate()
            oCollectection1.Clear()
            oCollectection1 = Nothing
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If
        End Try
    End Sub

    Private Sub fill_EmpState()

        Dim oCollectection As Collection = Nothing
        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            oCollectection = oDM.EmploymentStatus
            cmbEmpStatus.Items.Clear()
            cmbEmpStatus_Ex.Items.Clear()

            If Not IsNothing(oCollectection) AndAlso oCollectection.Count > 0 Then
                cmbEmpStatus.Items.Add("")
                cmbEmpStatus_Ex.Items.Add("")
                For i As Int16 = 1 To oCollectection.Count
                    cmbEmpStatus.Items.Add(oCollectection(i))
                    cmbEmpStatus_Ex.Items.Add(oCollectection(i))
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If
            oCollectection.Clear()
            oCollectection = Nothing
        End Try
    End Sub

#End Region ' Fill control methods '

#Region " Fill Orders to be given controls methods "

    'AddAssociated not yet refactored - Sagar
    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal strType As String, Optional ByVal addTemplate As Boolean = False)

        For Each myRootNode As myTreeNode In trOrderInfo.Nodes(0).Nodes

            If myRootNode.Text = strType Then
                ''Loop for all field nodes in each Root node
                For Each myTargetNode As myTreeNode In myRootNode.Nodes
                    ''Check whether the node already exists
                    '   If myRootNode.Text = "Guidelines" Then
                    If myRootNode.Text = "IM" Then
                    Else
                        If myTargetNode.Text = mynode.Text Then
                            ''If present do nothing
                            Exit Sub
                        End If
                    End If
                    If myRootNode.Text = "IM" Then
                        If myTargetNode.Key = mynode.Key And blnloadIm = False Then
                            Exit Sub
                        End If
                    End If
                Next

                ''Map all the node values to the associated node
                Dim Associatenode As New myTreeNode

                If myRootNode.Text <> "IM" Then
                    Associatenode = mynode.Clone
                    Associatenode.Key = mynode.Key
                    Associatenode.Text = mynode.Text
                    Associatenode.Tag = mynode.Tag
                End If

                If myRootNode.Text = "Guidelines" Then
                    Associatenode.DMTemplate = mynode.TemplateResult
                    Associatenode.DMTemplateName = mynode.DrugName
                End If

                If myRootNode.Text = "Rx" Then
                    Associatenode.DMTemplate = mynode.DrugName
                    Associatenode.DrugName = mynode.DrugName
                    Associatenode.Dosage = mynode.Dosage
                    Associatenode.Tag = mynode.Key
                    Associatenode.DrugForm = mynode.DrugForm
                    Associatenode.Duration = mynode.Duration
                    Associatenode.Frequency = mynode.Frequency
                    Associatenode.mpid = mynode.mpid
                    Associatenode.NDCCode = mynode.NDCCode
                    Associatenode.Route = mynode.Route
                    Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
                    Associatenode.IsNarcotics = mynode.IsNarcotics
                End If

                If myRootNode.Text = "IM" Then

                    mynode.Text = mynode.Text.Replace(" - " & mynode.Dosage, "")

                    Dim IMnode As New myTreeNode

                    If mynode.DrugQtyQualifier.ToString = "" Or mynode.DrugQtyQualifier.ToString = "0" Then
                        If mynode.Key = 0 And mynode.Tag <> "0" Then
                            mynode.Key = mynode.Tag
                        End If
                        IMnode = mynode.Clone
                        IMnode.Key = mynode.Key
                        IMnode.Text = mynode.Text
                        IMnode.Tag = mynode.Tag
                        IMnode.Route = mynode.Route ' SKU
                        IMnode.DrugForm = mynode.DrugForm    'VaccineCode
                        IMnode.Frequency = mynode.Frequency  'TradeName
                        IMnode.NDCCode = mynode.NDCCode 'Manufacture
                        IMnode.DMTemplateName = mynode.DrugName
                        IMnode.Duration = mynode.Duration ''Lot Number
                        IMnode.DrugName = mynode.DrugName ''Vaccine Name 
                        IMnode.DrugQtyQualifier = mynode.DrugQtyQualifier
                        IMnode.Dosage = mynode.Dosage
                        IMnode.ImageIndex = 0
                        IMnode.SelectedImageIndex = 0
                        myRootNode.Nodes.Add(IMnode)
                    Else
                        For Cnt As Int32 = 1 To mynode.DrugQtyQualifier

                            If mynode.Key <> 0 And mynode.Tag = "0" Then
                                mynode.Tag = mynode.Key
                            End If

                            IMnode = mynode.Clone
                            IMnode.Key = mynode.Key
                            IMnode.Route = mynode.Route 'Orginal Name of Vaccine
                            IMnode.Text = mynode.Text
                            IMnode.Tag = mynode.Tag
                            IMnode.DMTemplateName = mynode.DrugName
                            IMnode.DrugForm = mynode.DrugForm    'Vaccine code
                            IMnode.Duration = mynode.Duration    'Lot Number
                            IMnode.Frequency = mynode.Frequency  'tradEName                    
                            IMnode.NDCCode = mynode.NDCCode 'Manufacture

                            IMnode.DrugName = mynode.DrugName ''Vaccine Name 
                            IMnode.DrugQtyQualifier = mynode.DrugQtyQualifier
                            IMnode.Dosage = mynode.Dosage
                            IMnode.ImageIndex = 0
                            IMnode.SelectedImageIndex = 0
                            myRootNode.Nodes.Add(IMnode)
                        Next
                    End If
                End If

                If myRootNode.Text <> "IM" Then
                    Associatenode.ImageIndex = 0
                    Associatenode.SelectedImageIndex = 0
                    myRootNode.Nodes.Add(Associatenode)
                End If
            End If
        Next
        trOrderInfo.ExpandAll()
        blnloadIm = False
    End Sub

    Public Sub FillLabTest()

        Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        Dim dtLabsModule As DataTable = Nothing

        Try
            oDM = New gloStream.DiseaseManagement.Common.Criteria
            dtLabsModule = oDM.LabModuleTest
            GloUC_trvAssociates.Clear()

            If Not dtLabsModule Is Nothing Then
                GloUC_trvAssociates.DataSource = dtLabsModule.Copy()
                GloUC_trvAssociates.ValueMember = dtLabsModule.Columns(1).ColumnName
                GloUC_trvAssociates.DescriptionMember = dtLabsModule.Columns(0).ColumnName
                GloUC_trvAssociates.CodeMember = dtLabsModule.Columns(0).ColumnName
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                GloUC_trvAssociates.FillTreeView()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDM.Dispose()
            oDM = Nothing
            If Not IsNothing(dtLabsModule) Then
                dtLabsModule.Dispose()
            End If
        End Try
    End Sub

    Private Sub fill_IM()

        Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing
        Dim dt As DataTable = Nothing
        Dim Query As String = ""

        Try
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            Query = "SELECT im.im_item_id,dbo.IM_SeparateCodeAndDescription(im.im_sVaccine,'-','Description') AS  im_sVaccine,  isnull(im.im_item_Count,0) AS im_item_Count, " _
                    & "  dbo.IM_SeparateCodeAndDescription(im.im_sVaccine,'-','Code') AS im_vaccine_code,  CASE ISNULL(im_LotNumber,'') WHEN ''  THEN  convert(varchar,im_item_Count ) ELSE (convert(varchar,im_item_Count) + ' - ' + im_LotNumber)  END AS im_LotNumberwithCount,ISNULL(im_sTradeName,'') AS im_sTradeName ,ISNULL(im_sManufacturer,'') AS im_sManufacturer,ISNULL(im_LotNumber,'') AS im_LotNumber,ISNULL(im_sSKU,'') AS im_sSKU   FROM  im_mst im  WHERE im_sActive='Active' ORDER BY im_sVaccine  "

            dt = oDB.ReadQueryDataTable(Query)
            oDB.Disconnect()
            GloUC_trvAssociates.Clear()

            If Not dt Is Nothing Then
                GloUC_trvAssociates.DataSource = dt.Copy()
                GloUC_trvAssociates.ValueMember = dt.Columns("im_item_id").ColumnName
                GloUC_trvAssociates.DescriptionMember = dt.Columns("im_LotNumberwithCount").ColumnName
                GloUC_trvAssociates.CodeMember = dt.Columns("im_sVaccine").ColumnName
                GloUC_trvAssociates.DrugFormMember = dt.Columns("im_vaccine_code").ColumnName
                GloUC_trvAssociates.DrugQtyQualifierMember = dt.Columns("im_item_Count").ColumnName 'item_Count
                GloUC_trvAssociates.FrequencyMember = dt.Columns("im_sTradeName").ColumnName 'TradeName
                GloUC_trvAssociates.NDCCodeMember = dt.Columns("im_sManufacturer").ColumnName 'Manufacturer
                GloUC_trvAssociates.DurationMember = dt.Columns("im_LotNumber").ColumnName ''Lot Number
                GloUC_trvAssociates.RouteMember = dt.Columns("im_sSKU").ColumnName
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                GloUC_trvAssociates.FillTreeView()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dt.Dispose()
            dt = Nothing
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try

    End Sub

    Private Sub fill_guideline()
        Dim oDm As gloStream.DiseaseManagement.Common.Guidelines = Nothing
        Dim dt As DataTable = Nothing
        Try
            oDm = New gloStream.DiseaseManagement.Common.Guidelines
            dt = oDm.GuidelinesTables("")

            If Not dt Is Nothing Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dt.Copy()
                GloUC_trvAssociates.ValueMember = dt.Columns("nTemplateID").ColumnName
                GloUC_trvAssociates.DescriptionMember = dt.Columns("sTemplateName").ColumnName
                GloUC_trvAssociates.CodeMember = dt.Columns("sTemplateName").ColumnName
                GloUC_trvAssociates.ImageObject = dt.Columns("sDescription").ColumnName
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                GloUC_trvAssociates.FillTreeView()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDm = Nothing
            If Not IsNothing(dt) Then
                dt.Dispose()
            End If
        End Try
    End Sub

    Public Sub FillRx()

        Dim obj As ClsICD9AssociationDBLayer = Nothing
        Dim strsearch As String = ""

        Try
            If Not IsNothing(GloUC_trvAssociates.txtsearch.Text) Then
                strsearch = GloUC_trvAssociates.txtsearch.Text
            End If

            obj = New ClsICD9AssociationDBLayer
            dt = obj.FillControls(0, strsearch)

            If Not IsNothing(dt) Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dt
                GloUC_trvAssociates.IsDrug = True
                GloUC_trvAssociates.DrugFlag = 16 ''For all drugs 
                GloUC_trvAssociates.ValueMember = dt.Columns("DrugsID").ColumnName
                GloUC_trvAssociates.DescriptionMember = dt.Columns("Dosage").ColumnName
                GloUC_trvAssociates.CodeMember = dt.Columns("DrugName").ColumnName
                GloUC_trvAssociates.DrugFormMember = dt.Columns("DrugForm").ColumnName
                GloUC_trvAssociates.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
                GloUC_trvAssociates.NDCCodeMember = Convert.ToString(dt.Columns("sNDCCode").ColumnName)
                GloUC_trvAssociates.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
                GloUC_trvAssociates.FrequencyMember = dt.Columns("sFrequency").ColumnName
                GloUC_trvAssociates.DurationMember = dt.Columns("sDuration").ColumnName
                GloUC_trvAssociates.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
                GloUC_trvAssociates.mpidmember = dt.Columns("mpid").ColumnName
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                GloUC_trvAssociates.FillTreeView()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(obj) Then
                obj.Dispose()
            End If
        End Try
    End Sub

#End Region 'Fill Orders to be given controls methods '

    Public Sub Fill_CriteriaDetails(ByVal lCriteriaID As Long)

        Dim oCriteria As gloStream.DiseaseManagement.Supporting.Criteria = Nothing
        Dim oDM As gloStream.DiseaseManagement.DiseaseManagement = Nothing
        Dim _listviewItem As ListViewItem = Nothing
        Dim oCriteriaList As List(Of gloStream.DiseaseManagement.Supporting.Criteria) = Nothing

        Try
            If lCriteriaID <> 0 Then

                oDM = New gloStream.DiseaseManagement.DiseaseManagement
                oCriteriaList = oDM.DM_GetRuleDetails(lCriteriaID)

                If Not IsNothing(oCriteriaList) AndAlso oCriteriaList.Count > 0 Then
                    For objIndex As Integer = 0 To oCriteriaList.Count - 1
                        oCriteria = oCriteriaList(objIndex)
                        If Not IsNothing(oCriteria) Then

                            If oCriteria.IsTriggerObject = True Then
                                With oCriteria

                                    If blsCopyRule = False Then
                                        txtName.Text = .Name
                                    End If

                                    txtMessage.Text = .DisplayMessage
                                    chkIsActiveRule.Checked = .IsActive
                                    chkSpecialAlert.Checked = .bIsSpecialAlert
                                    chckRecurring.Checked = .bIsRecuringRule

                                    If .dtRecurrenceStartDate <> Date.MinValue Then
                                        dtStartDate.Value = Convert.ToDateTime(.dtRecurrenceStartDate).ToShortDateString()
                                    End If

                                    If .dtRecurrenceEndDate <> Date.MinValue Then
                                        dtEndDate.Value = Convert.ToDateTime(.dtRecurrenceEndDate).ToShortDateString()
                                    End If

                                    cmbDurationType.SelectedIndex = .nDuratiotype
                                    cmbPeriod.Text = .nDuratioPeriod

                                    'Set Triggers Demographics Information
                                    SetDemographicsInformation(TabType.Trigger, .City, .State, .Zip, .EmployeeStatus)

                                    'Set Triggers Vitals Information
                                    SetAgeMin(TabType.Trigger, .AgeMinimum, .AgeMinimum)
                                    SetAgeMax(TabType.Trigger, .AgeMaximum, .AgeMaximum)
                                    SetHeightMin(TabType.Trigger, .HeightMinimum)
                                    SetHeightMax(TabType.Trigger, .HeightMaximum)
                                    SetOtherVitals(TabType.Trigger, .PulseMinimum, .PulseMaximum, .PulseOXMinimum, .PulseOXMaximum, .BPSittingMinimum, .BPSittingMaximum,
                                                   .BPStandingMinimum, .BPStandingMaximum, .WeightMinimum, .WeightMaximum, .TempratureMinumum, .TempratureMaximum,
                                                   .BMIMinimum, .BMIMaximum, .BPSittingToMinimum, .BPSittingToMaximum,
                                                   .BPStandingToMinimum, .BPStandingToMaximum)

                                    'Set Triggers other Information
                                    SetOtherInformation(oCriteria.OtherDetails, TabType.Trigger)
                                    If oCriteria.bIsOldRule = True And blsCopyRule = False Then
                                        tlsDM_Save.Visible = False
                                        lblHealthPlanAlert.Visible = True
                                    End If

                                    If blsCopyRule Then
                                        chkIsActiveRule.Checked = False
                                    End If

                                    'Set QuickOrder/Order To Be Given Information
                                    trOrderInfo.BeginUpdate()
                                    SetQuickOrder(.LabOrders, QuickOrderType.LabOrders)
                                    SetQuickOrder(.RadiologyOrders, QuickOrderType.RadiologyOrders)
                                    SetQuickOrder(.Referrals, QuickOrderType.Referrals)
                                    SetQuickOrder(.RxDrugs, QuickOrderType.RxDrugs)
                                    SetQuickOrder(.Guidelines, QuickOrderType.Guidelines)
                                    blnloadIm = True
                                    SetQuickOrder(.IMlst, QuickOrderType.IM)
                                    blnloadIm = False
                                End With

                            ElseIf oCriteria.IsTriggerObject = False Then
                                With oCriteria
                                    'Set Exceptions Demographics Information
                                    SetDemographicsInformation(TabType.Exception, .City, .State, .Zip, .EmployeeStatus)

                                    'Set Exceptions Vitals Information
                                    SetAgeMin(TabType.Exception, .AgeMinimum, .AgeMinimum)
                                    SetAgeMax(TabType.Exception, .AgeMaximum, .AgeMaximum)
                                    SetHeightMin(TabType.Exception, .HeightMinimum)
                                    SetHeightMax(TabType.Exception, .HeightMaximum)
                                    SetOtherVitals(TabType.Exception, .PulseMinimum, .PulseMaximum, .PulseOXMinimum, .PulseOXMaximum, .BPSittingMinimum, .BPSittingMaximum,
                                                   .BPStandingMinimum, .BPStandingMaximum, .WeightMinimum, .WeightMaximum, .TempratureMinumum, .TempratureMaximum,
                                                   .BMIMinimum, .BMIMaximum, .BPSittingToMinimum, .BPSittingToMaximum,
                                                   .BPStandingToMinimum, .BPStandingToMaximum)

                                    'Set Exceptions other Information
                                    SetOtherInformation(oCriteria.OtherDetails, TabType.Exception)
                                End With
                            End If
                        End If
                    Next

                End If

                'gloStream.DiseaseManagement.Supporting.Criteria does not have idisposable interface implemented and ocriteria set to nothing in finally
                trOrderInfo.ExpandAll()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trOrderInfo.EndUpdate()

            oCriteria = Nothing
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If

            If Not IsNothing(oCriteriaList) Then
                oCriteriaList.Clear()
                oCriteriaList = Nothing
            End If
        End Try
    End Sub

#Region " Criteria/Rule set methods "

    Public Sub SetDemographicsInformation(ByVal tab As TabType, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal EmployementStatus As String)
        Try
            If TabType.Trigger = tab Then
                txtCity.Text = City
                cmbState.Text = State : SetCombiIndex(cmbState)
                txtZip.Text = Zip
                cmbEmpStatus.Text = EmployementStatus : SetCombiIndex(cmbEmpStatus)
            ElseIf TabType.Exception = tab Then
                txtCity_Ex.Text = City
                cmbState_Ex.Text = State : SetCombiIndex(cmbState_Ex)
                txtZip_Ex.Text = Zip
                cmbEmpStatus_Ex.Text = EmployementStatus : SetCombiIndex(cmbEmpStatus_Ex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SetAgeMin(ByVal tab As TabType, ByVal ageMinimum As String, ByVal ageMin As String)
        Dim _ageArrayStr() As String = Nothing
        Try
            If ageMinimum.ToString.Contains(".") Then
                If TabType.Trigger = tab Then
                    Dim _age() As String = ageMinimum.ToString.Split(".")
                    cmbAgeMin.Text = _age(0) '': SetCombiIndex(cmbAgeMin)
                    _ageArrayStr = ageMin.Split(".")
                    If (_ageArrayStr(1).ToString() = "1") Then
                        cmbAgeMinMnth.Text = "10"
                    Else
                        cmbAgeMinMnth.Text = _age(1) ''Format(CDbl((CDbl("." & _age(1))) * 12), "#00")
                    End If
                ElseIf TabType.Exception = tab Then
                    Dim _age() As String = ageMinimum.ToString.Split(".")
                    cmbAgeMin_Ex.Text = _age(0) '': SetCombiIndex(cmbAgeMin_Ex)
                    _ageArrayStr = ageMin.Split(".")
                    If (_ageArrayStr(1).ToString() = "1") Then
                        cmbAgeMinMnth_Ex.Text = "10"
                    Else
                        cmbAgeMinMnth_Ex.Text = _age(1) ''Format(CDbl((CDbl("." & _age(1))) * 12), "#00")
                    End If
                End If
            Else
                If Val(ageMinimum) > 0 Then
                    If TabType.Trigger = tab Then
                        cmbAgeMin.Text = ageMinimum '': SetCombiIndex(cmbAgeMin)
                        cmbAgeMinMnth.Text = "00" '': SetCombiIndex(cmbAgeMinMnth)
                    ElseIf TabType.Exception = tab Then
                        cmbAgeMin_Ex.Text = ageMinimum : SetCombiIndex(cmbAgeMin_Ex)
                        cmbAgeMinMnth_Ex.Text = "00" '': SetCombiIndex(cmbAgeMinMnth_Ex)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _ageArrayStr = Nothing
        End Try
    End Sub

    Public Sub SetAgeMax(ByVal tab As TabType, ByVal ageMaximum As String, ByVal ageMax As String)
        Dim _ageArrayStr() As String = Nothing
        Try
            If ageMaximum.ToString.Contains(".") Then
                If tab = TabType.Trigger Then
                    Dim _age() As String = ageMaximum.ToString.Split(".")
                    cmbAgeMax.Text = _age(0) ''SetCombiIndex(cmbAgeMax)
                    _ageArrayStr = ageMax.Split(".")
                    If (_ageArrayStr(1).ToString() = "1") Then
                        cmbAgeMaxMnth.Text = "10"
                    Else
                        cmbAgeMaxMnth.Text = _age(1) '' Format(CDbl((CDbl("." & _age(1))) * 12), "#00") : SetCombiIndex(cmbAgeMaxMnth)
                    End If
                ElseIf tab = TabType.Exception Then
                    Dim _age() As String = ageMaximum.ToString.Split(".")
                    cmbAgeMax_Ex.Text = _age(0) '' SetCombiIndex(cmbAgeMax_Ex)
                    _ageArrayStr = ageMax.Split(".")
                    If (_ageArrayStr(1).ToString() = "1") Then
                        cmbAgeMaxMnth_Ex.Text = "10"
                    Else
                        cmbAgeMaxMnth_Ex.Text = _age(1)
                    End If
                End If
            Else
                If Val(ageMaximum) > 0 Then
                    If tab = TabType.Trigger Then
                        cmbAgeMax.Text = ageMaximum '' SetCombiIndex(cmbAgeMax)
                        cmbAgeMaxMnth.Text = "00" '' SetCombiIndex(cmbAgeMaxMnth)
                    ElseIf tab = TabType.Exception Then
                        cmbAgeMax_Ex.Text = ageMaximum ''SetCombiIndex(cmbAgeMax_Ex)
                        cmbAgeMaxMnth_Ex.Text = "00" ''SetCombiIndex(cmbAgeMaxMnth_Ex)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _ageArrayStr = Nothing
        End Try
    End Sub

    Public Sub SetHeightMin(ByVal tab As TabType, ByVal heightMinimum As String)
        Dim arrHeight() As String
        Try
            If tab = TabType.Trigger Then
                If heightMinimum.Length > 0 Then
                    arrHeight = GetFtInch(heightMinimum)
                    txtHeightMin.Text = arrHeight(0)
                    txtHeightMinInch.Text = arrHeight(1)
                End If
            ElseIf tab = TabType.Exception Then
                If heightMinimum.Length > 0 Then
                    arrHeight = GetFtInch(heightMinimum)
                    txtHeightMin_Ex.Text = arrHeight(0)
                    txtHeightMinInch_Ex.Text = arrHeight(1)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            arrHeight = Nothing
        End Try
    End Sub

    Public Sub SetHeightMax(ByVal tab As TabType, ByVal heightMaximum As String)
        Dim arrHeight() As String
        Try
            If tab = TabType.Trigger Then
                If heightMaximum.Length > 0 Then
                    arrHeight = GetFtInch(heightMaximum)
                    txtHeightMax.Text = arrHeight(0)
                    txtHeightMaxInch.Text = arrHeight(1)
                End If
            ElseIf tab = TabType.Exception Then
                If heightMaximum.Length > 0 Then
                    arrHeight = GetFtInch(heightMaximum)
                    txtHeightMax_Ex.Text = arrHeight(0)
                    txtHeightMaxInch_Ex.Text = arrHeight(1)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            arrHeight = Nothing
        End Try
    End Sub

    Public Sub SetOtherVitals(ByVal tab As TabType, ByVal Pulse_Minimum As Double, ByVal Pulse_Maximum As Double, ByVal PulseOX_Minimum As Double,
        ByVal PulseOX_Maximum As Double, ByVal BPSitting_Minimum As Double, ByVal BPSitting_Maximum As Double, ByVal BPStanding_Minimum As Double,
        ByVal BPStanding_Maximum As Double, ByVal WeightMinimum As Double, ByVal WeightMaximum As Double, ByVal Temprature_Minimum As Double,
                ByVal Temprature_Maximum As Double, ByVal BMI_Minimum As Double, ByVal BMI_Maximum As Double, ByVal BPSitting_ToMinimum As Double, ByVal BPSitting_ToMaximum As Double, ByVal BPStanding_ToMinimum As Double,
        ByVal BPStanding_ToMaximum As Double)

        Try
            If tab = TabType.Trigger Then

                txtBPsettingMin.Text = ""
                If BPSitting_Minimum <> 0.0 Then
                    txtBPsettingMin.Text = BPSitting_Minimum
                End If

                txtBPsettingMax.Text = ""
                If BPSitting_Maximum <> 0.0 Then
                    txtBPsettingMax.Text = BPSitting_Maximum
                End If

                txtBPsettingMinTo.Text = ""
                If BPSitting_ToMinimum <> 0.0 Then
                    txtBPsettingMinTo.Text = BPSitting_ToMinimum
                End If

                txtBPsettingMaxTo.Text = ""
                If BPSitting_ToMaximum <> 0.0 Then
                    txtBPsettingMaxTo.Text = BPSitting_ToMaximum
                End If

                txtWeightMin.Text = ""
                If WeightMinimum <> 0.0 Then
                    txtWeightMin.Text = WeightMinimum
                End If

                txtWeightMax.Text = ""
                If WeightMaximum <> 0.0 Then
                    txtWeightMax.Text = WeightMaximum
                End If

                txtBPstandingMin.Text = ""
                If BPStanding_Minimum <> 0.0 Then
                    txtBPstandingMin.Text = BPStanding_Minimum
                End If

                txtBPstandingMax.Text = ""
                If BPStanding_Maximum <> 0.0 Then
                    txtBPstandingMax.Text = BPStanding_Maximum
                End If

                txtBPstandingMinTo.Text = ""
                If BPStanding_ToMinimum <> 0.0 Then
                    txtBPstandingMinTo.Text = BPStanding_ToMinimum
                End If

                txtBPstandingMaxTo.Text = ""
                If BPStanding_ToMaximum <> 0.0 Then
                    txtBPstandingMaxTo.Text = BPStanding_ToMaximum
                End If

                txtTemperatureMin.Text = ""
                If Temprature_Minimum <> 0.0 Then
                    txtTemperatureMin.Text = Temprature_Minimum
                End If

                txtTemperatureMax.Text = ""
                If Temprature_Maximum <> 0.0 Then
                    txtTemperatureMax.Text = Temprature_Maximum
                End If

                txtPulseMin.Text = ""
                If Pulse_Minimum <> 0.0 Then
                    txtPulseMin.Text = Pulse_Minimum
                End If

                txtPulseMax.Text = ""
                If Pulse_Maximum <> 0.0 Then
                    txtPulseMax.Text = Pulse_Maximum
                End If

                txtBMImin.Text = ""
                If BMI_Minimum <> 0.0 Then
                    txtBMImin.Text = BMI_Minimum
                End If

                txtBMImax.Text = ""
                If BMI_Maximum <> 0.0 Then
                    txtBMImax.Text = BMI_Maximum
                End If

                txtPulseOXmin.Text = ""
                If PulseOX_Minimum <> 0.0 Then
                    txtPulseOXmin.Text = PulseOX_Minimum
                End If

                txtPulseOXmax.Text = ""
                If PulseOX_Maximum <> 0.0 Then
                    txtPulseOXmax.Text = PulseOX_Maximum
                End If

            ElseIf tab = TabType.Exception Then

                txtBPsettingMin_Ex.Text = ""
                If BPSitting_Minimum <> 0.0 Then
                    txtBPsettingMin_Ex.Text = BPSitting_Minimum
                End If

                txtBPsettingMax_Ex.Text = ""
                If BPSitting_Maximum <> 0.0 Then
                    txtBPsettingMax_Ex.Text = BPSitting_Maximum
                End If

                If BPSitting_ToMinimum <> 0.0 Then
                    txtBPsettingMin_Ex_To.Text = BPSitting_ToMinimum
                End If

                txtBPsettingMax_Ex_To.Text = ""
                If BPSitting_ToMaximum <> 0.0 Then
                    txtBPsettingMax_Ex_To.Text = BPSitting_ToMaximum
                End If

                txtWeightMin_Ex.Text = ""
                If WeightMinimum <> 0.0 Then
                    txtWeightMin_Ex.Text = WeightMinimum
                End If

                txtWeightMax_Ex.Text = ""
                If WeightMaximum <> 0.0 Then
                    txtWeightMax_Ex.Text = WeightMaximum
                End If

                txtBPstandingMin_Ex.Text = ""
                If BPStanding_Minimum <> 0.0 Then
                    txtBPstandingMin_Ex.Text = BPStanding_Minimum
                End If

                txtBPstandingMax_Ex.Text = ""
                If BPStanding_Maximum <> 0.0 Then
                    txtBPstandingMax_Ex.Text = BPStanding_Maximum
                End If


                txtBPstandingMin_Ex_To.Text = ""
                If BPStanding_ToMinimum <> 0.0 Then
                    txtBPstandingMin_Ex_To.Text = BPStanding_ToMinimum
                End If

                txtBPstandingMax_Ex_To.Text = ""
                If BPStanding_ToMaximum <> 0.0 Then
                    txtBPstandingMax_Ex_To.Text = BPStanding_ToMaximum
                End If


                txtTemperatureMin_Ex.Text = ""
                If Temprature_Minimum <> 0.0 Then
                    txtTemperatureMin_Ex.Text = Temprature_Minimum
                End If

                txtTemperatureMax_Ex.Text = ""
                If Temprature_Maximum <> 0.0 Then
                    txtTemperatureMax_Ex.Text = Temprature_Maximum
                End If

                txtPulseMin_Ex.Text = ""
                If Pulse_Minimum <> 0.0 Then
                    txtPulseMin_Ex.Text = Pulse_Minimum
                End If

                txtPulseMax_Ex.Text = ""
                If Pulse_Maximum <> 0.0 Then
                    txtPulseMax_Ex.Text = Pulse_Maximum
                End If

                txtBMImin_Ex.Text = ""
                If BMI_Minimum <> 0.0 Then
                    txtBMImin_Ex.Text = BMI_Minimum
                End If

                txtBMImax_Ex.Text = ""
                If BMI_Maximum <> 0.0 Then
                    txtBMImax_Ex.Text = BMI_Maximum
                End If

                txtPulseOXmin_Ex.Text = ""
                If PulseOX_Minimum <> 0.0 Then
                    txtPulseOXmin_Ex.Text = PulseOX_Minimum
                End If

                txtPulseOXmax_Ex.Text = ""
                If PulseOX_Maximum <> 0.0 Then
                    txtPulseOXmax_Ex.Text = PulseOX_Maximum
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Public Sub SetInsuranceNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

        Dim myInsuranceNode As myTreeNode = Nothing
        Dim _listviewItem As ListViewItem = Nothing

        Try

            myInsuranceNode = New myTreeNode

            If item.ItemName <> "" Then
                myInsuranceNode.Text = item.ItemName
            Else
                myInsuranceNode.Text = item.ItemName
            End If

            myInsuranceNode.Tag = item.CategoryName
            myInsuranceNode.DrugName = item.ItemName
            myInsuranceNode.Dosage = item.ItemName
            myInsuranceNode.NDCCode = item.Result1


            _listviewItem = New ListViewItem
            _listviewItem.Text = myInsuranceNode.Text
            _listviewItem.Tag = myInsuranceNode

            If tab = TabType.Trigger Then
                trvSelectedInsurance.Nodes.Add(myInsuranceNode)
                lstVw_Insurance.Items.Add(_listviewItem)
            ElseIf tab = TabType.Exception Then
                trvSelectedInsurance_Ex.Nodes.Add(myInsuranceNode)
                lstExVw_Insurance.Items.Add(_listviewItem)
            End If

            _listviewItem = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myInsuranceNode = Nothing
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Public Sub SetDrugNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

        Dim myDrugNode As myTreeNode = Nothing
        Dim _listviewItem As ListViewItem = Nothing

        Try

            myDrugNode = New myTreeNode

            myDrugNode.Text = item.CategoryName

            myDrugNode.Tag = item.ItemName
            myDrugNode.DrugName = item.CategoryName
            myDrugNode.Dosage = item.ItemName
            myDrugNode.mpid = item.Result1
            myDrugNode.NDCCode = item.Result2

            _listviewItem = New ListViewItem
            _listviewItem.Text = myDrugNode.Text
            _listviewItem.Tag = myDrugNode

            If tab = TabType.Trigger Then
                trvSelectedDrugs.Nodes.Add(myDrugNode)
                lstVw_Drugs.Items.Add(_listviewItem)
            ElseIf tab = TabType.Exception Then
                trvSelectedDrugs_Ex.Nodes.Add(myDrugNode)
                lstExVw_Drugs.Items.Add(_listviewItem)
            End If

            _listviewItem = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myDrugNode = Nothing
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Public Sub SetHistoryNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

        Dim _listviewItem As ListViewItem = Nothing
        Dim CategoryFound As Boolean = False
        Dim HistoryFound As Boolean = False
        Dim oCategoryNode As TreeNode = Nothing
        Dim oHistoryNode As myTreeNode = Nothing

        Try

            If tab = TabType.Trigger Then

                For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
                    If CategoryNode.Text = item.CategoryName Then

                        oHistoryNode = New myTreeNode
                        oHistoryNode.Text = item.ItemName
                        oHistoryNode.Tag = item.CategoryName
                        CategoryNode.Nodes.Add(oHistoryNode)
                        CategoryNode.Expand()

                        _listviewItem = New ListViewItem
                        _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
                        _listviewItem.Tag = oHistoryNode
                        lstVw_History.Items.Add(_listviewItem)

                        _listviewItem = Nothing
                        oHistoryNode = Nothing
                        CategoryFound = True
                        Exit For
                    End If
                Next

                If Not CategoryFound Then
                    oCategoryNode = New TreeNode
                    oHistoryNode = New myTreeNode
                    oCategoryNode.Text = item.CategoryName
                    oCategoryNode.ImageIndex = 0
                    oCategoryNode.SelectedImageIndex = 0
                    oHistoryNode.Text = item.ItemName
                    oHistoryNode.Tag = item.CategoryName
                    oCategoryNode.Nodes.Add(oHistoryNode)
                    trvSelectedHistory.Nodes.Add(oCategoryNode)
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
                    _listviewItem.Tag = oHistoryNode
                    lstVw_History.Items.Add(_listviewItem)
                End If

                trvSelectedHistory.ExpandAll()

            ElseIf tab = TabType.Exception Then

                For Each CategoryNode As TreeNode In trvSelectedHistory_Ex.Nodes
                    If CategoryNode.Text = item.CategoryName Then

                        oHistoryNode = New myTreeNode
                        oHistoryNode.Text = item.ItemName
                        oHistoryNode.Tag = item.CategoryName
                        CategoryNode.Nodes.Add(oHistoryNode)
                        CategoryNode.Expand()
                        _listviewItem = New ListViewItem
                        _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
                        _listviewItem.Tag = oHistoryNode
                        lstExVw_History.Items.Add(_listviewItem)
                        _listviewItem = Nothing
                        oHistoryNode = Nothing
                        CategoryFound = True
                        Exit For
                    End If
                Next

                If Not CategoryFound Then
                    oCategoryNode = New TreeNode
                    oHistoryNode = New myTreeNode
                    oCategoryNode.Text = item.CategoryName
                    oCategoryNode.ImageIndex = 0
                    oCategoryNode.SelectedImageIndex = 0
                    oHistoryNode.Text = item.ItemName
                    oHistoryNode.Tag = item.CategoryName
                    oCategoryNode.Nodes.Add(oHistoryNode)
                    trvSelectedHistory_Ex.Nodes.Add(oCategoryNode)
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
                    _listviewItem.Tag = oHistoryNode
                    lstExVw_History.Items.Add(_listviewItem)
                End If
                trvSelectedHistory_Ex.ExpandAll()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oCategoryNode = Nothing
            oHistoryNode = Nothing
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetICDNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

        Dim myICDNode As myTreeNode = Nothing
        Dim _listviewItem As ListViewItem = Nothing

        Try
            myICDNode = New myTreeNode
            myICDNode.Text = item.CategoryName + " - " + item.ItemName
            myICDNode.Tag = item.CategoryName
            myICDNode.DrugName = item.ItemName

            _listviewItem = New ListViewItem
            _listviewItem.Text = myICDNode.Text
            _listviewItem.Tag = myICDNode

            If tab = TabType.Trigger Then
                trvselecteICDs.Nodes.Add(myICDNode)
                lstVw_ICD9.Items.Add(_listviewItem)
            ElseIf tab = TabType.Exception Then
                trvselectedICDs_Ex.Nodes.Add(myICDNode)
                lstExVw_ICD.Items.Add(_listviewItem)
            End If

            _listviewItem = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myICDNode = Nothing
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetICD10Node(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

        Dim myICDNode As myTreeNode = Nothing
        Dim _listviewItem As ListViewItem = Nothing

        Try
            myICDNode = New myTreeNode
            myICDNode.Text = item.CategoryName + " - " + item.ItemName
            myICDNode.Tag = item.CategoryName
            myICDNode.DrugName = item.ItemName

            _listviewItem = New ListViewItem
            _listviewItem.Text = myICDNode.Text
            _listviewItem.Tag = myICDNode

            If tab = TabType.Trigger Then
                trvselecteICD10s.Nodes.Add(myICDNode)
                lstVw_ICD10.Items.Add(_listviewItem)
            ElseIf tab = TabType.Exception Then
                trvselectedICD10s_Ex.Nodes.Add(myICDNode)
                lstExVw_ICD10.Items.Add(_listviewItem)
            End If

            _listviewItem = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myICDNode = Nothing
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetCPTNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Dim myCPTNode As myTreeNode = Nothing
        Dim _listviewItem As ListViewItem = Nothing

        Try
            myCPTNode = New myTreeNode
            myCPTNode.Text = item.CategoryName.Trim() + " - " + item.ItemName
            myCPTNode.Tag = item.CategoryName
            myCPTNode.DrugName = item.ItemName

            _listviewItem = New ListViewItem
            _listviewItem.Text = myCPTNode.Text
            _listviewItem.Tag = myCPTNode
            If tab = TabType.Trigger Then
                trvselectedCPT.Nodes.Add(myCPTNode)
                lstVw_CPT.Items.Add(_listviewItem)
            ElseIf tab = TabType.Exception Then
                trvselectedCPT_Ex.Nodes.Add(myCPTNode)
                lstExVw_CPT.Items.Add(_listviewItem)
            End If
            _listviewItem = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            myCPTNode = Nothing
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetOrder(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Dim _listviewItem As ListViewItem = Nothing
        Try
            If tab = TabType.Trigger Then
                For j As Integer = 1 To c1Labs.Rows.Count - 1
                    Dim _TestCell As String = c1Labs.GetData(j, COL_IDENTITY) & ""
                    If Mid(_TestCell, 1, 1) = "T" Then
                        If c1Labs.Rows(j).Node.Data = item.ItemName And
                            c1Labs.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.OperatorName And
                            c1Labs.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data = item.CategoryName Then

                            c1Labs.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

                            _listviewItem = New ListViewItem
                            _listviewItem.Text = c1Labs.Rows(j).Node.Data
                            lstVw_Orders.Items.Add(_listviewItem)
                            _listviewItem = Nothing

                        End If
                    End If
                Next
            ElseIf tab = TabType.Exception Then
                For j As Integer = 1 To c1Labs_Ex.Rows.Count - 1
                    Dim _TestCell As String = c1Labs_Ex.GetData(j, COL_IDENTITY) & ""
                    If Mid(_TestCell, 1, 1) = "T" Then
                        If c1Labs_Ex.Rows(j).Node.Data = item.ItemName And
                            c1Labs_Ex.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.OperatorName And
                            c1Labs_Ex.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data = item.CategoryName Then

                            c1Labs_Ex.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

                            _listviewItem = New ListViewItem
                            _listviewItem.Text = c1Labs_Ex.Rows(j).Node.Data
                            lstExVw_Orders.Items.Add(_listviewItem)
                            _listviewItem = Nothing
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetLabs(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Dim _listviewItem As ListViewItem = Nothing
        Try
            If tab = TabType.Trigger Then
                For j As Integer = 1 To C1LabResult.Rows.Count - 1
                    If C1LabResult.Rows(j).Node.Level = 1 Then
                        If C1LabResult.Rows(j).Node.Data = item.ItemName And C1LabResult.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.CategoryName Then
                            C1LabResult.SetCellCheck(j, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            C1LabResult.SetData(j, COL_Operator, item.OperatorName)
                            C1LabResult.SetData(j, COL_ResultValue1, item.Result1)
                            C1LabResult.SetData(j, COL_ResultValue2, item.Result2)
                            _listviewItem = New ListViewItem
                            _listviewItem.Text = C1LabResult.Rows(j).Node.Data
                            lstVw_Lab.Items.Add(_listviewItem)
                            _listviewItem = Nothing
                        End If
                    End If
                Next
            ElseIf tab = TabType.Exception Then
                For j As Integer = 1 To C1LabResult_Ex.Rows.Count - 1
                    If C1LabResult_Ex.Rows(j).Node.Level = 1 Then
                        If C1LabResult_Ex.Rows(j).Node.Data = item.ItemName And C1LabResult_Ex.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.CategoryName Then
                            C1LabResult_Ex.SetCellCheck(j, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            C1LabResult_Ex.SetData(j, COL_Operator, item.OperatorName)
                            C1LabResult_Ex.SetData(j, COL_ResultValue1, item.Result1)
                            C1LabResult_Ex.SetData(j, COL_ResultValue2, item.Result2)
                            _listviewItem = New ListViewItem
                            _listviewItem.Text = C1LabResult_Ex.Rows(j).Node.Data
                            lstExVw_Lab.Items.Add(_listviewItem)
                            _listviewItem = Nothing
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetLabs_ByTable()
        Dim _listviewItem As ListViewItem = Nothing
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing

        Try
            _dt = CType(C1LabResult.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
            End If
            If Not IsNothing(_dv) Then
                _dtFilter = _dv.ToTable().Copy()
            End If
            lstVw_Lab.Items.Clear()
            If Not IsNothing(_dtFilter) Then
                For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Result").ToString() + "   " + _dtFilter.Rows(_rowIndex)("Operator") + " " + _dtFilter.Rows(_rowIndex)("Result Value1") + " " + _dtFilter.Rows(_rowIndex)("Result Value2")
                    _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
                    lstVw_Lab.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
                _dtFilter = Nothing
            End If
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Sub

    Private Sub SetEXLabs_ByTable()
        Dim _listviewItem As ListViewItem = Nothing
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing

        Try
            _dt = CType(C1LabResult_Ex.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dt) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
            End If
            If Not IsNothing(_dv) Then
                _dtFilter = _dv.ToTable().Copy()
            End If
            lstExVw_Lab.Items.Clear()
            If Not IsNothing(_dtFilter) Then
                For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Result").ToString() + "   " + _dtFilter.Rows(_rowIndex)("Operator") + " " + _dtFilter.Rows(_rowIndex)("Result Value1") + " " + _dtFilter.Rows(_rowIndex)("Result Value2")
                    _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
                    lstExVw_Lab.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            '_dt.Dispose()
            '_dt = Nothing
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
                _dtFilter = Nothing
            End If
            ''dtFilter datatable can't disposed because it has been assigned to dataview _dv 
        End Try
    End Sub

    Private Sub SetOrders_ByTable()
        Dim _listviewItem As ListViewItem = Nothing
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing

        Try
            _dt = CType(c1Labs.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            _dtFilter.Dispose()
            _dtFilter = _dv.ToTable().Copy()

            lstVw_Orders.Items.Clear()

            For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
                _listviewItem = New ListViewItem
                _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Test").ToString()
                _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
                lstVw_Orders.Items.Add(_listviewItem)
                _listviewItem = Nothing
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            '_dt.Dispose()
            '_dt = Nothing
            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
                _dtFilter = Nothing
            End If
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Sub

    Private Sub SetExOrders_ByTable()
        Dim _listviewItem As ListViewItem = Nothing
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing
        Try
            _dt = CType(c1Labs_Ex.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()

            End If
            If Not IsNothing(_dv) Then
                _dtFilter = _dv.ToTable().Copy()
            End If

            lstExVw_Orders.Items.Clear()
            If Not IsNothing(_dtFilter) Then
                For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Test").ToString()
                    _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
                    lstExVw_Orders.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            '_dt.Dispose()
            '_dt = Nothing
            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
                _dtFilter = Nothing
            End If
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Sub

    Private Function getSlectedLabAndResult(ByRef c1FlexGrid) As DataTable
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing
        Try
            _dt = CType(c1FlexGrid.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            If Not IsNothing(_dv) Then
                _dtFilter = _dv.ToTable().Copy()
            End If
            Return _dtFilter
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            _dt = Nothing
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Function

    Private Sub SetRace(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Try
            If tab = TabType.Trigger Then
                For iCount As Integer = 1 To cmbChkBoxRace.CheckBoxItems.Count - 1
                    If cmbChkBoxRace.CheckBoxItems(iCount).Text = item.ItemName Then
                        cmbChkBoxRace.CheckBoxItems(iCount).Checked = True
                    End If
                Next
            ElseIf tab = TabType.Exception Then
                For iCount As Integer = 1 To cmbChkBoxRace_Ex.CheckBoxItems.Count - 1
                    If cmbChkBoxRace_Ex.CheckBoxItems(iCount).Text = item.ItemName Then
                        cmbChkBoxRace_Ex.CheckBoxItems(iCount).Checked = True
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            item = Nothing
        End Try
    End Sub

    Private Sub SetMaritalStatus(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Try
            If tab = TabType.Trigger Then
                For iCount As Integer = 1 To cmbChkBoxMaritalSt.CheckBoxItems.Count - 1
                    If cmbChkBoxMaritalSt.CheckBoxItems(iCount).Text = item.ItemName Then
                        cmbChkBoxMaritalSt.CheckBoxItems(iCount).Checked = True
                    End If
                Next
            ElseIf tab = TabType.Exception Then
                For iCount As Integer = 1 To cmbChkBoxMaritalSt_Ex.CheckBoxItems.Count - 1
                    If cmbChkBoxMaritalSt_Ex.CheckBoxItems(iCount).Text = item.ItemName Then
                        cmbChkBoxMaritalSt_Ex.CheckBoxItems(iCount).Checked = True
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            item = Nothing
        End Try
    End Sub

    Private Sub SetGender(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Try
            If tab = TabType.Trigger Then
                For iCount As Integer = 1 To cmbChkBoxGender.CheckBoxItems.Count - 1
                    If cmbChkBoxGender.CheckBoxItems(iCount).Text = item.ItemName Then
                        cmbChkBoxGender.CheckBoxItems(iCount).Checked = True
                    End If
                Next
            ElseIf tab = TabType.Exception Then
                For iCount As Integer = 1 To cmbGender_Ex.CheckBoxItems.Count - 1
                    If cmbGender_Ex.CheckBoxItems(iCount).Text = item.ItemName Then
                        cmbGender_Ex.CheckBoxItems(iCount).Checked = True
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            item = Nothing
        End Try
    End Sub

    Private Sub SetSnomedCode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
        Dim _lstVwitem As ListViewItem = Nothing
        Try
            _lstVwitem = New ListViewItem()
            _lstVwitem.Text = item.ItemName
            _lstVwitem.Tag = item.CategoryName
            _lstVwitem.SubItems.Add(item.ItemName)

            If tab = TabType.Trigger Then
                AddItemToList(lstVw_SnoMed, _lstVwitem)
            ElseIf tab = TabType.Exception Then
                AddItemToList(lstExVw_SnoMed, _lstVwitem)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _lstVwitem = Nothing
            item = Nothing
        End Try
    End Sub

    Private Sub SetQuickOrder(ByVal collections As Collection, ByVal quickOrderType As QuickOrderType)
        Dim _myTreeNode As myTreeNode = Nothing
        Dim objList As myList = Nothing
        Try
            If Not IsNothing(collections) AndAlso collections.Count > 0 Then
                For itemIndex As Integer = 1 To collections.Count
                    objList = CType(collections(itemIndex), myList)

                    Select Case (quickOrderType)
                        Case quickOrderType.LabOrders
                            _myTreeNode = New myTreeNode(objList.Value, objList.ID)
                            If Not IsNothing(_myTreeNode) Then
                                AddAssociates(_myTreeNode, "Orders")
                                _myTreeNode.Dispose()
                                _myTreeNode = Nothing
                            End If

                        Case quickOrderType.RadiologyOrders
                            _myTreeNode = New myTreeNode(objList.Value, objList.ID)
                            If Not IsNothing(_myTreeNode) Then
                                AddAssociates(_myTreeNode, "Order Templates")
                                _myTreeNode.Dispose()
                                _myTreeNode = Nothing
                            End If

                        Case quickOrderType.Referrals
                            _myTreeNode = New myTreeNode(objList.Value, objList.ID)
                            If Not IsNothing(_myTreeNode) Then
                                AddAssociates(_myTreeNode, "Referrals")
                                _myTreeNode.Dispose()
                                _myTreeNode = Nothing
                            End If

                        Case quickOrderType.RxDrugs
                            _myTreeNode = New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage, objList.DrugForm, objList.Route, objList.Frequency, objList.NDCCode, objList.IsNarcotic, objList.Duration, objList.mpid, objList.DrugQtyQualifier)
                            If Not IsNothing(_myTreeNode) Then
                                AddAssociates(_myTreeNode, "Rx")
                                _myTreeNode.Dispose()
                                _myTreeNode = Nothing
                            End If

                        Case quickOrderType.Guidelines
                            _myTreeNode = New myTreeNode()
                            _myTreeNode.Text = objList.DMTemplateName
                            _myTreeNode.Tag = objList.ID
                            If Not IsNothing(objList.DMTemplate) Then
                                _myTreeNode.TemplateResult = objList.DMTemplate
                            Else
                                _myTreeNode.TemplateResult = Nothing
                            End If

                            If Not IsNothing(_myTreeNode) Then
                                AddAssociates(_myTreeNode, "Guidelines")
                                _myTreeNode.Dispose()
                                _myTreeNode = Nothing
                            End If

                        Case quickOrderType.IM
                            blnloadIm = True
                            _myTreeNode = New myTreeNode()
                            _myTreeNode.Text = objList.DMTemplateName ''Vaccine Name
                            _myTreeNode.Tag = objList.ID                  'IM ID
                            _myTreeNode.Key = objList.ID                  'IM ID
                            _myTreeNode.DrugForm = objList.DrugForm ''Vaccine Code
                            _myTreeNode.Route = objList.Route ''SKU
                            _myTreeNode.DrugName = objList.Code
                            _myTreeNode.Dosage = objList.Description
                            _myTreeNode.Frequency = objList.Frequency ''TradeName
                            _myTreeNode.NDCCode = objList.NDCCode ''Manufaturer
                            _myTreeNode.IsNarcotics = objList.IsNarcotic
                            _myTreeNode.Duration = objList.Duration ''Lot Number

                            _myTreeNode.TemplateResult = Nothing
                            If Not IsNothing(_myTreeNode) Then
                                AddAssociates(_myTreeNode, "IM")
                                _myTreeNode.Dispose()
                                _myTreeNode = Nothing
                            End If
                    End Select
                    objList = Nothing
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            blnloadIm = False
            _myTreeNode = Nothing
            objList = Nothing
        End Try
    End Sub

    Private Sub SetOtherInformation(ByVal otherDetailList As Supporting.OtherDetails, ByVal tab As TabType)
        Try
            For i As Integer = 1 To otherDetailList.Count

                Select Case (otherDetailList.Item(i).DetailType)

                    Case Supporting.enumDetailType.Medication
                        SetDrugNode(tab, otherDetailList.Item(i))
                    Case Supporting.enumDetailType.Insurance
                        SetInsuranceNode(tab, otherDetailList.Item(i))
                    Case Supporting.enumDetailType.History
                        SetHistoryNode(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.ICD9
                        SetICDNode(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.ICD10
                        SetICD10Node(tab, otherDetailList.Item(i))

                    Case (Supporting.enumDetailType.CPT)
                        SetCPTNode(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.Order
                        '' SetOrder(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.Lab
                        ''  SetLabs(tab, otherDetailList.Item(i))
                        '' SetLabs_ByTable(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.Race
                        SetRace(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.MaritalStatus
                        SetMaritalStatus(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.Gender
                        SetGender(tab, otherDetailList.Item(i))

                    Case Supporting.enumDetailType.SnomedCode
                        SetSnomedCode(tab, otherDetailList.Item(i))

                End Select

            Next '......looping ....For i As Integer = 1 To otherDetailList.Count
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            otherDetailList.Clear()
            otherDetailList = Nothing
        End Try
    End Sub

    Private Sub Search_TextChanged(ByVal c1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strSearch As String)
        Dim sFilter As String = ""
        Dim _dv As DataView = Nothing
        Dim strSearchArray As String() = Nothing

        Try
            _dv = DirectCast(c1FlexGrid.DataSource, DataTable).DefaultView
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If strSearch.Trim() <> "" Then
                strSearchArray = strSearch.Split(","c)
            End If

            If strSearch.Trim() <> "" Then
                If strSearchArray.Length = 1 Then
                    'For Single value search 
                    strSearch = strSearchArray(0).Trim()
                    If strSearch.Length > 1 Then
                        Dim str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) & str
                    End If

                    _dv.RowFilter = _dv.Table.Columns("Test").ColumnName + " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Result").ColumnName & " Like '" & strSearch & "%' "
                Else
                    'For Comma separated  value search
                    For i As Integer = 0 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i).Trim()
                        If strSearch.Length > 1 Then
                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) & str
                        End If

                        If strSearch.Trim() <> "" Then
                            If sFilter = "" Then
                                '(i == 0)
                                sFilter = "(" + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Result").ColumnName & " Like '" & strSearch & "%' )"
                            Else
                                sFilter = sFilter & " AND (" + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Result").ColumnName & " Like '" & strSearch & "%')"
                            End If
                        End If
                    Next

                    _dv.RowFilter = sFilter
                End If
            Else
                _dv.RowFilter = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            strSearchArray = Nothing
        End Try
    End Sub

    Private Sub Search_Order_TextChanged(ByVal c1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strSearch As String)
        Dim sFilter As String = ""
        Dim _dv As DataView = Nothing
        Dim strSearchArray As String() = Nothing
        Try
            _dv = DirectCast(c1FlexGrid.DataSource, DataTable).DefaultView
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If strSearch.Trim() <> "" Then
                strSearchArray = strSearch.Split(","c)
            End If

            If strSearch.Trim() <> "" Then
                If strSearchArray.Length = 1 Then
                    'For Single value search 
                    strSearch = strSearchArray(0).Trim()
                    If strSearch.Length > 1 Then
                        Dim str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) & str
                    End If
                    _dv.RowFilter = _dv.Table.Columns("Category").ColumnName + " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' "
                Else
                    'For Comma separated  value search
                    For i As Integer = 0 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i).Trim()

                        If strSearch.Length > 1 Then
                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) & str
                        End If

                        If strSearch.Trim() <> "" Then
                            If sFilter = "" Then
                                '(i == 0)
                                sFilter = "(" + _dv.Table.Columns("Category").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' )"
                            Else
                                sFilter = sFilter & " AND (" + _dv.Table.Columns("Category").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%')"
                            End If
                        End If
                    Next
                    _dv.RowFilter = sFilter
                End If
            Else
                _dv.RowFilter = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            strSearchArray = Nothing
        End Try

    End Sub

#End Region ' Criteria/Rule set methods '

#Region "Fill Methods"


    Private Sub fill_Insurance()
        Dim obj As New ClsICD9AssociationDBLayer
        Try
            Dim strsearch As String = ""

            If Not IsNothing(GloUC_trvInsurance.txtsearch.Text) Then
                strsearch = GloUC_trvInsurance.txtsearch.Text
            End If

            dt = obj.FillControls(21, strsearch)

            If Not IsNothing(dt) Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    GloUC_trvInsurance.Clear()
                    GloUC_trvInsurance.DataSource = dt
                    GloUC_trvInsurance.ValueMember = Convert.ToString(dt.Columns("ContactID").ColumnName)
                    GloUC_trvInsurance.DescriptionMember = dt.Columns("sName").ColumnName
                    GloUC_trvInsurance.CodeMember = dt.Columns("sName").ColumnName
                    GloUC_trvInsurance.NDCCodeMember = Convert.ToString(dt.Columns("ContactID").ColumnName)
                    GloUC_trvInsurance.DDIDMember = dt.Columns("ContactID").ColumnName
                    GloUC_trvInsurance.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                    GloUC_trvInsurance.FillTreeView()
                ElseIf tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    GloUC_trvInsurance_Ex.Clear()
                    GloUC_trvInsurance_Ex.DataSource = dt
                    GloUC_trvInsurance_Ex.ValueMember = Convert.ToString(dt.Columns("ContactID").ColumnName)
                    GloUC_trvInsurance_Ex.DescriptionMember = dt.Columns("sName").ColumnName
                    GloUC_trvInsurance_Ex.CodeMember = dt.Columns("sName").ColumnName
                    GloUC_trvInsurance_Ex.NDCCodeMember = Convert.ToString(dt.Columns("ContactID").ColumnName)
                    GloUC_trvInsurance_Ex.DDIDMember = dt.Columns("ContactID").ColumnName
                    GloUC_trvInsurance_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                    GloUC_trvInsurance_Ex.FillTreeView()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If obj IsNot Nothing Then
                obj.Dispose()
                obj = Nothing
            End If
        End Try
    End Sub
    Private Sub fill_Drugs()
        Dim obj As New ClsICD9AssociationDBLayer
        Try
            Dim strsearch As String = ""

            If Not IsNothing(GloUC_trvDrugs.txtsearch.Text) Then
                strsearch = GloUC_trvDrugs.txtsearch.Text
            End If

            dt = obj.FillControls(0, strsearch)

            If Not IsNothing(dt) Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    GloUC_trvDrugs.Clear()
                    GloUC_trvDrugs.DataSource = dt
                    GloUC_trvDrugs.IsDrug = True
                    GloUC_trvDrugs.DrugFlag = 16 ''For all drugs 
                    GloUC_trvDrugs.ValueMember = dt.Columns("DrugsID").ColumnName
                    GloUC_trvDrugs.DescriptionMember = dt.Columns("Dosage").ColumnName
                    GloUC_trvDrugs.CodeMember = dt.Columns("DrugName").ColumnName
                    GloUC_trvDrugs.DrugFormMember = dt.Columns("DrugForm").ColumnName
                    GloUC_trvDrugs.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
                    GloUC_trvDrugs.NDCCodeMember = Convert.ToString(dt.Columns("sNDCCode").ColumnName) ''''bug fix for 6852
                    GloUC_trvDrugs.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
                    GloUC_trvDrugs.FrequencyMember = dt.Columns("sFrequency").ColumnName
                    GloUC_trvDrugs.DurationMember = dt.Columns("sDuration").ColumnName
                    GloUC_trvDrugs.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
                    GloUC_trvDrugs.mpidmember = dt.Columns("mpid").ColumnName
                    GloUC_trvDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                    'Display Type Changed by Mayuri:20091008 To display drugs in form of DrugName and Drug Form
                    GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                    GloUC_trvDrugs.FillTreeView()
                ElseIf tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    GloUC_trvDrugs_Ex.Clear()
                    GloUC_trvDrugs_Ex.DataSource = dt
                    GloUC_trvDrugs_Ex.IsDrug = True
                    GloUC_trvDrugs_Ex.DrugFlag = 16 ''For all drugs 
                    GloUC_trvDrugs_Ex.ValueMember = dt.Columns("DrugsID").ColumnName
                    GloUC_trvDrugs_Ex.DescriptionMember = dt.Columns("Dosage").ColumnName
                    GloUC_trvDrugs_Ex.CodeMember = dt.Columns("DrugName").ColumnName
                    GloUC_trvDrugs_Ex.DrugFormMember = dt.Columns("DrugForm").ColumnName
                    GloUC_trvDrugs_Ex.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
                    GloUC_trvDrugs_Ex.NDCCodeMember = Convert.ToString(dt.Columns("sNDCCode").ColumnName) ''''bug fix for 6852
                    GloUC_trvDrugs_Ex.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
                    GloUC_trvDrugs_Ex.FrequencyMember = dt.Columns("sFrequency").ColumnName
                    GloUC_trvDrugs_Ex.DurationMember = dt.Columns("sDuration").ColumnName
                    GloUC_trvDrugs_Ex.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
                    GloUC_trvDrugs_Ex.mpidmember = dt.Columns("mpid").ColumnName
                    GloUC_trvDrugs_Ex.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                    'Display Type Changed by Mayuri:20091008 To display drugs in form of DrugName and Drug Form
                    GloUC_trvDrugs_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                    GloUC_trvDrugs_Ex.FillTreeView()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If obj IsNot Nothing Then
                obj.Dispose()
                obj = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_Histories_1(Optional ByVal HistoryCategory As String = "")
        Dim oDm As New gloStream.DiseaseManagement.Common.Criteria
        Dim dtHistoryCategory As DataTable
        txtSearch.Text = ""
        Try
            If HistoryCategory = "" Then
                dtHistoryCategory = GetHistoryCategory()
                cmbHistoryCategory.DataSource = dtHistoryCategory
                cmbHistoryCategory.DisplayMember = dtHistoryCategory.Columns("sDescription").ColumnName
                cmbHistoryCategory.ValueMember = dtHistoryCategory.Columns("nCategoryID").ColumnName
                Fill_Histories_1(dtHistoryCategory.Rows(0)("sDescription").ToString)
            Else
                ' Dim oNode As myTreeNode
                dt = oDm.GetHistoriesDataTable(HistoryCategory)
                If Not dt Is Nothing Then
                    GloUC_trvHistory.DataSource = dt
                    GloUC_trvHistory.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
                    GloUC_trvHistory.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
                    GloUC_trvHistory.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)

                    If dt.Columns.Contains("AllergyClassID") Then
                        GloUC_trvHistory.AllergyClassID = Convert.ToString(dt.Columns("AllergyClassID").ColumnName)
                    End If

                    If HistoryCategory = "Allergies" Then
                        GloUC_trvHistory.Tag = Convert.ToString(dt.Columns("IsDrug").ColumnName)
                    Else
                        GloUC_trvHistory.Tag = ""
                    End If
                    GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                    GloUC_trvHistory.FillTreeView()
                    GloUC_trvHistory.FocusSearchBox()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDm IsNot Nothing Then
                oDm.Dispose()
                oDm = Nothing
            End If
        End Try
    End Sub

    Private Sub fill_CPTs(ByVal strSortBy As String)
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try
            dtCPT = objICD9AssociationDBLayer.FillControls(1, strSortBy)
            '   Dim oNode As TreeNode
            If IsNothing(dtCPT) = False Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    GloUC_trvCPT.DataSource = dtCPT
                    GloUC_trvCPT.CodeMember = Convert.ToString(dtCPT.Columns(3).ColumnName)
                    GloUC_trvCPT.ValueMember = Convert.ToString(dtCPT.Columns(0).ColumnName)
                    GloUC_trvCPT.DescriptionMember = Convert.ToString(dtCPT.Columns(1).ColumnName)
                    GloUC_trvCPT.FillTreeView()
                ElseIf tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    GloUC_trvCPT_Ex.DataSource = dtCPT
                    GloUC_trvCPT_Ex.CodeMember = Convert.ToString(dtCPT.Columns(3).ColumnName)
                    GloUC_trvCPT_Ex.ValueMember = Convert.ToString(dtCPT.Columns(0).ColumnName)
                    GloUC_trvCPT_Ex.DescriptionMember = Convert.ToString(dtCPT.Columns(1).ColumnName)
                    GloUC_trvCPT_Ex.FillTreeView()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objICD9AssociationDBLayer IsNot Nothing Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_ICD9s(ByVal strsearch As String)
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try
            dtICD9 = objICD9AssociationDBLayer.FillControls(3, strsearch)
            ' Dim oNode As TreeNode
            If IsNothing(dtICD9) = False Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    GloUC_trvICD9.DataSource = dtICD9
                    GloUC_trvICD9.CodeMember = "sICD9code"
                    GloUC_trvICD9.ValueMember = "nICD9ID"
                    GloUC_trvICD9.DescriptionMember = "sDescription"
                    GloUC_trvICD9.IsDiagnosisSearch = True
                    GloUC_trvICD9.FillTreeView()
                ElseIf tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    GloUC_trvICD9_Ex.DataSource = dtICD9
                    GloUC_trvICD9_Ex.CodeMember = "sICD9code"
                    GloUC_trvICD9_Ex.ValueMember = "nICD9ID"
                    GloUC_trvICD9_Ex.DescriptionMember = "sDescription"
                    GloUC_trvICD9_Ex.IsDiagnosisSearch = True
                    GloUC_trvICD9_Ex.FillTreeView()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objICD9AssociationDBLayer IsNot Nothing Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_ICD10s(ByVal strsearch As String)
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try
            dtICD9 = objICD9AssociationDBLayer.FillControls(15, strsearch)
            ' Dim oNode As TreeNode
            If IsNothing(dtICD9) = False Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    GloUC_trvICD10.DataSource = dtICD9
                    GloUC_trvICD10.CodeMember = "sICD9code"
                    GloUC_trvICD10.ValueMember = "nICD9ID"
                    GloUC_trvICD10.DescriptionMember = "sDescription"
                    GloUC_trvICD10.IsDiagnosisSearch = True
                    GloUC_trvICD10.FillTreeView()
                ElseIf tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    GloUC_trvICD10_Ex.DataSource = dtICD9
                    GloUC_trvICD10_Ex.CodeMember = "sICD9code"
                    GloUC_trvICD10_Ex.ValueMember = "nICD9ID"
                    GloUC_trvICD10_Ex.DescriptionMember = "sDescription"
                    GloUC_trvICD10_Ex.IsDiagnosisSearch = True
                    GloUC_trvICD10_Ex.FillTreeView()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objICD9AssociationDBLayer IsNot Nothing Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End Try
    End Sub
#End Region

    Private Sub GloUC_trvHistory_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvHistory.NodeAdded
        If Not String.IsNullOrWhiteSpace(ChildNode.AllergyClassID) Then  ''to identify allergies having codes                        
            ChildNode.ImageIndex = 11
            ChildNode.SelectedImageIndex = 11
        End If
    End Sub

    Private Sub GloUC_TrvHistoryEx_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_TrvHistoryEx.NodeAdded
        If Not String.IsNullOrWhiteSpace(ChildNode.AllergyClassID) Then  ''to identify allergies having codes                        
            ChildNode.ImageIndex = 11
            ChildNode.SelectedImageIndex = 11
        End If
    End Sub
End Class



