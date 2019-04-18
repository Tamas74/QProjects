Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class frmVitalNorms

    Dim oVital As New clsPatientVitals
    Private _NormName As String = ""
    Private _FromAge As Integer = 0
    Private _ToAge As Integer = 0
    Private _Gender As String = "Male"
    Private _IsRecordModified As Boolean = False
    Private _IsRecordsLoading As Boolean = True
    Private _IsSaveClicked As Boolean = False
    Private _assignNormName As String = ""
    Private _selectedNodeIndex As Integer = 0
    Private oAllNorms As gloStream.Vitals.Supportings.VitalNorms ''USED TO SAVE FOR TEMPERORY PROCESS.
    'Shubhangi


    'Enum WeightIn
    '    Lbs
    '    Kg
    '    LbsOz
    'End Enum

    'Dim _WeightFlag As WeightIn
    ' Dim _TempInCelcius As Boolean = False
    'Dim _HeadCircumInch As Boolean = False
    'Dim _StatureInInch As Boolean = False
    'Shubhangi

    Private Sub frmVitalNorms_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Close, "Vital Norms closed", gloAuditTrail.ActivityOutCome.Success)
    End Sub

    Private Sub frmVitalNorms_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim result As Windows.Forms.DialogResult

        If _IsRecordModified = True And _IsSaveClicked = False Then

            result = MessageBox.Show("Do you want to save records?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If result = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            ElseIf result = Windows.Forms.DialogResult.Yes Then
                ''Added by Mayuri:20100329-Issue:#6526-Vitals:Validation message is showing more times in Master.
                tlsNorms.Focus()
                If Not Me.ActiveControl Is tlsNorms Then
                    e.Cancel = True
                    Exit Sub
                End If
                ''end code Added by Mayuri:20100329

                If IsValidEntries() = True Then
                    tsb_Save_Click(Nothing, Nothing)
                Else
                    e.Cancel = True
                End If
            End If

        End If

    End Sub

    Private Sub frmVitalNorms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try



            pnlMainVitalsNorms.Visible = False

            FillNormTree()

            oAllNorms = oVital.GetNorms()

            _NormName = trvNorms.Nodes(0).Text
            lblNormHeader.Text = Space(5) & trvNorms.Nodes(0).Text
            Dim strLimit() As String = Split(trvNorms.Nodes(0).Tag, "-")
            _FromAge = Convert.ToInt16(strLimit(0))
            _ToAge = Convert.ToInt16(strLimit(1))

            ResetPanel()
            FillNorms(_NormName)

            rbMale.Checked = True
            Dim objSettings As New clsSettings
            Dim IsVitalNormEbabled As Boolean = objSettings.IsSettingEnabled("VITAL_NORMS_ENABLED")
            objSettings.Dispose()
            objSettings = Nothing
            chkVitalNorms.Checked = IsVitalNormEbabled
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, "View Vital Norms", gloAuditTrail.ActivityOutCome.Success)
    End Sub

    Private Function ConvertFtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
        Return (Ft * 30.48 + Inch * 2.54) / 100
        ''   1 ft = 30.48 cm
        ''   1 inch = 2.54 cm
    End Function

    Private Sub FillNormTree(Optional ByVal FromAge As Int16 = 0, Optional ByVal NewAge As Int16 = 0)
        Dim dtNormNames As DataTable = Nothing
        Dim oNode As TreeNode = Nothing
        Try
            dtNormNames = oVital.GetNormNames()

            Dim cms_TrvNormsNode As New ContextMenuStrip
            Dim cmsmnu_Split As New ToolStripMenuItem("Split Range")
            cmsmnu_Split.Image = Global.gloEMR.My.Resources.Resources.Split_vitals_norms

            cms_TrvNormsNode.Items.Add(cmsmnu_Split)
            AddHandler cmsmnu_Split.Click, AddressOf SpiltNorm
            Dim mnuMerge As New ToolStripMenuItem("Merge with Next")
            mnuMerge.Image = Global.gloEMR.My.Resources.Resources.Merge_vital_norms
            cms_TrvNormsNode.Items.Add(mnuMerge)
            AddHandler mnuMerge.Click, AddressOf MergeNorm
            Dim SelectedNode As Int16 = 0
            If IsNothing(dtNormNames) = False Then
                For Each oTNode As TreeNode In trvNorms.Nodes
                    If (IsNothing(oTNode.ContextMenuStrip) = False) Then
                        oTNode.ContextMenuStrip.Dispose()
                        oTNode.ContextMenuStrip = Nothing
                    End If
                Next
                trvNorms.Nodes.Clear()
                For iRow As Integer = 0 To dtNormNames.Rows.Count - 1
                    oNode = New TreeNode
                    oNode.Text = dtNormNames.Rows(iRow)("sNorm").ToString
                    oNode.Tag = dtNormNames.Rows(iRow)("nFromAge").ToString & "-" & dtNormNames.Rows(iRow)("nToAge").ToString
                    _assignNormName = oNode.Tag
                    'Try
                    '    If (IsNothing(oNode.ContextMenuStrip) = False) Then
                    '        oNode.ContextMenuStrip.Dispose()
                    '        oNode.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    oNode.ContextMenuStrip = cms_TrvNormsNode
                    trvNorms.Nodes.Add(oNode)
                Next
                'Try
                '    If (IsNothing(oNode.ContextMenuStrip) = False) Then
                '        oNode.ContextMenuStrip.Dispose()
                '        oNode.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                oNode.ContextMenuStrip = Nothing
                Dim cms_TrvNormsNode_SplitOnly As New ContextMenuStrip
                Dim cmsmnu_Split_LastNode As New ToolStripMenuItem("Split Range")
                cmsmnu_Split_LastNode.Image = Global.gloEMR.My.Resources.Resources.Merge_vital_norms
                cms_TrvNormsNode_SplitOnly.Items.Add(cmsmnu_Split_LastNode)
                AddHandler cmsmnu_Split_LastNode.Click, AddressOf SpiltNorm
                oNode.ContextMenuStrip = cms_TrvNormsNode_SplitOnly
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillNorms(ByVal normName As String)
        'Dim oVitalNorms As New gloStream.Vitals.Supportings.VitalNorms
        _IsRecordsLoading = True
        Try
            pnlMainVitalsNorms.Visible = True
            ''oVitalNorms = oVital.GetNorms(normName)
            For i As Integer = 1 To oAllNorms.Count
                If oAllNorms(i).NormName = normName And oAllNorms(i).Gender = _Gender Then
                    Select Case oAllNorms(i).VitalType
                        Case gloStream.Vitals.Supportings.VitalTypes.Height
                            txtHeightMin.Text = oAllNorms(i).MinValue \ 12
                            txtHeightMinInch.Text = oAllNorms(i).MinValue Mod 12
                            txtHeightMax.Text = oAllNorms(i).MaxValue \ 12
                            txtHeightMaxInch.Text = oAllNorms(i).MaxValue Mod 12
                            'shubhangi
                            txtHeightMinInInch.Text = (Val(txtHeightMin.Text) * 12) + Val(txtHeightMinInch.Text)
                            txtHeightMinIncm.Text = Format(Val(txtHeightMinInInch.Text) * 2.54, "#0.00")
                            txtHeightMaxInInch.Text = (Val(txtHeightMax.Text) * 12) + Val(txtHeightMaxInch.Text)
                            txtHeightMaxIncm.Text = Format(Val(txtHeightMaxInInch.Text) * 2.54, "#0.00")
                            txtHeightMinInInch.Enabled = False
                            txtHeightMinIncm.Enabled = False
                            txtHeightMaxInInch.Enabled = False
                            txtHeightMaxIncm.Enabled = False
                            'txtHeightMin.Enabled = False
                            'txtHeightMinIncm.Enabled = False
                            'txtHeightMaxInInch.Enabled = False
                            'txtHeightMaxIncm.Enabled = False

                        Case gloStream.Vitals.Supportings.VitalTypes.Weight
                            txtWeightMin.Text = oAllNorms(i).MinValue
                            txtWeightMax.Text = oAllNorms(i).MaxValue
                            'Shubhangi 'For Min weight
                            txtWeightKg.Text = Format(Val(txtWeightMin.Text) * 0.45, "#0.000")
                            txtWeightlbs.Text = Decimal.Truncate(Val(txtWeightMin.Text))
                            Dim _decimalPlaces() As String = Split(txtWeightMin.Text, ".", , CompareMethod.Text)
                            If _decimalPlaces.Length > 1 Then
                                txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                                If Val(txtWtOz.Text) = 0 Then
                                    txtWtOz.Clear()
                                End If
                            Else
                                txtWtOz.Clear()
                            End If
                            'For max weight
                            txtMaxWeightKg.Text = Format(Val(txtWeightMax.Text) * 0.45, "#0.000")
                            txtMaxWeightlbs.Text = Decimal.Truncate(Val(txtWeightMax.Text))
                            Dim _decimalPlacesMax() As String = Split(txtWeightMax.Text, ".", , CompareMethod.Text)
                            If _decimalPlacesMax.Length > 1 Then
                                txtMaxWtOz.Text = CType("0." & _decimalPlacesMax(1), Decimal) * 16
                                If Val(txtMaxWtOz.Text) = 0 Then
                                    txtMaxWtOz.Clear()
                                End If
                            Else
                                txtMaxWtOz.Clear()
                            End If
                            txtWeightMin.Enabled = False
                            txtWeightKg.Enabled = False
                            txtWeightMax.Enabled = False
                            txtMaxWeightKg.Enabled = False
                        Case gloStream.Vitals.Supportings.VitalTypes.RespiratoryRate
                            txtRespRateMin.Text = oAllNorms(i).MinValue
                            txtRespRateMax.Text = oAllNorms(i).MaxValue
                        Case gloStream.Vitals.Supportings.VitalTypes.PulsePerMinute
                            txtPulseMin.Text = oAllNorms(i).MinValue
                            txtPulseMax.Text = oAllNorms(i).MaxValue
                        Case gloStream.Vitals.Supportings.VitalTypes.PulseOX
                            txtPulseOXmin.Text = oAllNorms(i).MinValue
                            txtPulseOXmax.Text = oAllNorms(i).MaxValue
                        Case gloStream.Vitals.Supportings.VitalTypes.BPSystolic
                            txtBPSystolicMin.Text = oAllNorms(i).MinValue
                            txtBPSystolicMax.Text = oAllNorms(i).MaxValue
                        Case gloStream.Vitals.Supportings.VitalTypes.BPDiastolic
                            txtBPDiastolicMin.Text = oAllNorms(i).MinValue
                            txtBPDiastolicMax.Text = oAllNorms(i).MaxValue
                        Case gloStream.Vitals.Supportings.VitalTypes.Temperature
                            txtTemperatureMin.Text = oAllNorms(i).MinValue
                            txtTemperatureMax.Text = oAllNorms(i).MaxValue
                            txtTemperatureMinIncel.Text = Format((5 / 9) * (Val(txtTemperatureMin.Text) - 32), "#0.00")
                            txtTemperatureMaxInCel.Text = Format((5 / 9) * (Val(txtTemperatureMax.Text) - 32), "#0.00")
                            txtTemperatureMinIncel.Enabled = False
                            txtTemperatureMaxInCel.Enabled = False
                        Case gloStream.Vitals.Supportings.VitalTypes.HeadCircumference
                            txtHeadCircumMin.Text = oAllNorms(i).MinValue
                            txtHeadCircumMax.Text = oAllNorms(i).MaxValue
                            txtHeadCircumMaxInInch.Text = Format(Val(txtHeadCircumMax.Text) * 0.3937008, "#0.00")
                            txtHeadCircumMinInInch.Text = Format(Val(txtHeadCircumMin.Text) * 0.3937008, "#0.00")
                            txtHeadCircumMax.Enabled = False
                            txtHeadCircumMin.Enabled = False
                        Case gloStream.Vitals.Supportings.VitalTypes.Stature
                            txtStatureMin.Text = oAllNorms(i).MinValue
                            txtStatureMax.Text = oAllNorms(i).MaxValue
                            txtStatureMinInInch.Text = Format(Val(txtStatureMin.Text) * 0.3937008, "#0.00")
                            txtStatureMaxInInch.Text = Format(Val(txtStatureMax.Text) * 0.3937008, "#0.00")
                            txtStatureMax.Enabled = False
                            txtStatureMin.Enabled = False
                        Case Else
                            'pnlMainVitalsNorms.Visible = False
                    End Select
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        _IsRecordsLoading = False
    End Sub

    Private Sub TempSaveNorms(ByVal normName As String)
        Dim oVitalNorm As gloStream.Vitals.Supportings.VitalNorm

        '' DELETE EXISTING NORMS TO SAVE NEW ONES.
        For iRow As Integer = oAllNorms.Count To 1 Step -1
            If oAllNorms(iRow).NormName = normName And oAllNorms(iRow).Gender = _Gender Then
                oAllNorms.Remove(iRow)
            End If
        Next


        '' HEIGHT ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If (txtHeightMin.Text <> "" and txtHeightMinInch.Text <> "") and (txtHeightMax.Text <> "" and txtHeightMaxInch.Text <> "") Then
        If (txtHeightMin.Text <> "" Or txtHeightMinInch.Text <> "") Or (txtHeightMax.Text <> "" Or txtHeightMaxInch.Text <> "") Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.Height.GetHashCode
            oVitalNorm.MinValue = Val(txtHeightMin.Text) * 12 + Val(txtHeightMinInch.Text)
            oVitalNorm.MaxValue = Val(txtHeightMax.Text) * 12 + Val(txtHeightMaxInch.Text)
            'oVitalNorm.MinInchValue = Val(txtHeightMinInInch.Text) * 12 + Val(txtHeightMinIncm.Text) 'Shubhangi
            'oVitalNorm.MaxInchValue = Val(txtHeightMaxInInch.Text) * 12 + Val(txtHeightMaxIncm)

            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' WEIGHT ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtWeightMin.Text <> "" And txtWeightMax.Text <> "" Then
        If txtWeightMin.Text <> "" Or txtWeightMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.Weight.GetHashCode
            oVitalNorm.MinValue = Val(txtWeightMin.Text)
            oVitalNorm.MaxValue = Val(txtWeightMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' RESPIRATORY RATE ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtRespRateMin.Text <> "" And txtRespRateMax.Text <> "" Then
        If txtRespRateMin.Text <> "" Or txtRespRateMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.RespiratoryRate.GetHashCode
            oVitalNorm.MinValue = Val(txtRespRateMin.Text)
            oVitalNorm.MaxValue = Val(txtRespRateMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' PULSE PER MINUTE ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtPulseMin.Text <> "" And txtPulseMax.Text <> "" Then
        If txtPulseMin.Text <> "" Or txtPulseMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.PulsePerMinute.GetHashCode
            oVitalNorm.MinValue = Val(txtPulseMin.Text)
            oVitalNorm.MaxValue = Val(txtPulseMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' PULSE OX ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtPulseOXmin.Text <> "" And txtPulseOXmax.Text <> "" Then
        If txtPulseOXmin.Text <> "" Or txtPulseOXmax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.PulseOX.GetHashCode
            oVitalNorm.MinValue = Val(txtPulseOXmin.Text)
            oVitalNorm.MaxValue = Val(txtPulseOXmax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' BP SYSTOLIC ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtBPSystolicMin.Text <> "" And txtBPSystolicMax.Text <> "" Then
        If txtBPSystolicMin.Text <> "" Or txtBPSystolicMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.BPSystolic.GetHashCode
            oVitalNorm.MinValue = Val(txtBPSystolicMin.Text)
            oVitalNorm.MaxValue = Val(txtBPSystolicMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' BP DIASTOLIC ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtBPDiastolicMin.Text <> "" And txtBPDiastolicMax.Text <> "" Then
        If txtBPDiastolicMin.Text <> "" Or txtBPDiastolicMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.BPDiastolic.GetHashCode
            oVitalNorm.MinValue = Val(txtBPDiastolicMin.Text)
            oVitalNorm.MaxValue = Val(txtBPDiastolicMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' TEMPERATURE ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtTemperatureMin.Text <> "" And txtTemperatureMax.Text <> "" Then
        If txtTemperatureMin.Text <> "" Or txtTemperatureMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.Temperature.GetHashCode
            oVitalNorm.MinValue = Val(txtTemperatureMin.Text)
            oVitalNorm.MaxValue = Val(txtTemperatureMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' HEAD CIRCUMFERENCE ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtHeadCircumMin.Text <> "" And txtHeadCircumMax.Text <> "" Then
        If txtHeadCircumMin.Text <> "" Or txtHeadCircumMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.HeadCircumference.GetHashCode
            oVitalNorm.MinValue = Val(txtHeadCircumMin.Text)
            oVitalNorm.MaxValue = Val(txtHeadCircumMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

        '' STATURE ''
        'if condition commented and modified by dipak 20091106 to fix bug no 4979 :-GLO2009-0003504 - Edit->Vital Norms
        'If txtStatureMin.Text <> "" And txtStatureMax.Text <> "" Then
        If txtStatureMin.Text <> "" Or txtStatureMax.Text <> "" Then
            oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
            oVitalNorm.NormName = _NormName
            oVitalNorm.VitalType = gloStream.Vitals.Supportings.VitalTypes.Stature.GetHashCode
            oVitalNorm.MinValue = Val(txtStatureMin.Text)
            oVitalNorm.MaxValue = Val(txtStatureMax.Text)
            oVitalNorm.FromAge = _FromAge
            oVitalNorm.ToAge = _ToAge
            oVitalNorm.Gender = _Gender
            oAllNorms.Add(oVitalNorm)
        End If

    End Sub

    Private Sub SaveNorms()
        If oVital.SaveVitalNorms(oAllNorms, chkVitalNorms.Checked) = True Then
            Me.Close()
        End If
    End Sub
    Private Sub SaveNorms(ByVal IsfromMergeSplit As Boolean)
        If oVital.SaveVitalNorms(oAllNorms, chkVitalNorms.Checked) = True Then
            _IsRecordModified = False
        End If
    End Sub
    Private Sub ResetPanel()
        _IsRecordsLoading = True
        For Each ctrl As Control In pnlVitals.Controls
            If TypeOf ctrl Is TextBox Then
                ctrl.Text = ""
            End If
        Next
        _IsRecordsLoading = False
    End Sub

    Private Function IsValidEntries() As Boolean
        '' SAVING VALIDATIONS ''
        '' HEIGHT ''
        If (Val(txtHeightMin.Text) * 12 + Val(txtHeightMinInch.Text)) >= (Val(txtHeightMax.Text) * 12 + Val(txtHeightMaxInch.Text)) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtHeightMin.Focus()
            Return False
        End If

        '' WEIGHT ''
        If Val(txtWeightMin.Text) >= Val(txtWeightMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtWeightMin.Focus()
            Return False
        End If

        '' RESPIRATORY RATE ''
        If Val(txtRespRateMin.Text) >= Val(txtRespRateMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRespRateMin.Focus()
            Return False
        End If

        '' PULSE ''
        If Val(txtPulseMin.Text) >= Val(txtPulseMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPulseMin.Focus()
            Return False
        End If

        '' PULSE OX ''
        If Val(txtPulseOXmin.Text) >= Val(txtPulseOXmax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPulseOXmin.Focus()
            Return False
        End If

        '' BP SYSTOLIC ''
        If Val(txtBPSystolicMin.Text) >= Val(txtBPSystolicMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBPSystolicMin.Focus()
            Return False
        End If

        '' BP DIASTOLIC ''
        If Val(txtBPDiastolicMin.Text) >= Val(txtBPDiastolicMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBPDiastolicMin.Focus()
            Return False
        End If

        '' TEMPERATURE ''
        If Val(txtTemperatureMin.Text) >= Val(txtTemperatureMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtTemperatureMin.Focus()
            Return False
        End If

        '' HEADCIRCUM ''
        If Val(txtHeadCircumMin.Text) >= Val(txtHeadCircumMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtHeadCircumMin.Focus()
            Return False
        End If

        '' STATURE ''
        If Val(txtStatureMin.Text) >= Val(txtStatureMax.Text) Then
            MessageBox.Show("Minimum value must be less than maximum value.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtStatureMin.Focus()
            Return False
        End If

        '' ALL VALID ''
        Return True
    End Function

    Private Sub tsb_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsb_Close.Click

        Me.Close()
    End Sub

    Private Sub tsb_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        ''Added by Mayuri:20100329-To fix issue:#6526-Vitals:Validation message is showing more times in Master.
        tlsNorms.Focus()
        If Not Me.ActiveControl Is tlsNorms Then
            Exit Sub
        End If

        _IsSaveClicked = True
        If IsValidEntries() = True Then
            TempSaveNorms(_NormName)
            SaveNorms()
        End If

    End Sub

    Private Sub trvNorms_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvNorms.AfterSelect
        If IsNothing(e.Node) = False Then

            If IsValidEntries() = False Then
                trvNorms.SelectedNode = Nothing
                Exit Sub
            End If

            TempSaveNorms(_NormName)

            _NormName = e.Node.Text
            lblNormHeader.Text = Space(5) & e.Node.Text
            Dim strLimit() As String = Split(e.Node.Tag, "-")
            _FromAge = Convert.ToInt16(strLimit(0))
            _ToAge = Convert.ToInt16(strLimit(1))
            ResetPanel()
            FillNorms(e.Node.Text)
        End If
    End Sub

    Private Sub txtHeightMinInInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMinInInch.KeyPress

    End Sub



    Private Sub txtAllBoxes_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHeightMin.TextChanged, txtWeightMin.TextChanged, txtWeightMax.TextChanged, txtTemperatureMin.TextChanged, txtTemperatureMinIncel.TextChanged, txtTemperatureMaxInCel.TextChanged, txtTemperatureMax.TextChanged, txtStatureMin.TextChanged, txtStatureMinInInch.TextChanged, txtStatureMaxInInch.TextChanged, txtStatureMax.TextChanged, txtRespRateMin.TextChanged, txtRespRateMax.TextChanged, txtPulseOXmin.TextChanged, txtPulseOXmax.TextChanged, txtPulseMin.TextChanged, txtPulseMax.TextChanged, txtHeightMinInch.TextChanged, txtHeightMaxInch.TextChanged, txtHeightMax.TextChanged, txtHeadCircumMin.TextChanged, txtHeadCircumMinInInch.TextChanged, txtHeadCircumMaxInInch.TextChanged, txtHeadCircumMax.TextChanged, txtBPSystolicMin.TextChanged, txtBPSystolicMax.TextChanged, txtBPDiastolicMin.TextChanged, txtBPDiastolicMax.TextChanged, txtHeightMinInInch.TextChanged, txtHeightMinIncm.TextChanged, txtHeightMaxInInch.TextChanged, txtHeightMaxIncm.TextChanged, txtWeightlbs.TextChanged, txtWtOz.TextChanged, txtWeightKg.TextChanged, txtMaxWeightlbs.TextChanged, txtMaxWtOz.TextChanged, txtMaxWeightKg.TextChanged
        If _IsRecordsLoading = False Then
            _IsRecordModified = True
            _IsRecordsLoading = True


            ''Sandip Darade 20100326 Bug ID 6525
            Dim str As String = CType(sender, TextBox).Text
            ''allow numerics only 
            If (CType(sender, TextBox).Name = "txtHeightMin" Or CType(sender, TextBox).Name = "txtHeightMax") Then
                str = GetFormattedString(str, False, False)
            Else    ''allow decimal  
                str = GetFormattedString(str, False, True)
            End If


            CType(sender, TextBox).Text = str
            ''end Remove Special character
            'Shubhangi 20090908 
            'To check which text box's text change event occur & according to that do conversion

            Select Case CType(sender, TextBox).Name

                'For Height Min in ft 
                Case "txtHeightMin"
                    'Calc_WtKg_BMI()

                    If txtHeightMin.Text = "" And txtHeightMinInch.Text = "" Then
                        txtHeightMinIncm.Clear()
                        txtHeightMinInInch.Clear()
                        txtHeightMinInInch.Enabled = True
                        txtHeightMinIncm.Enabled = True
                    Else
                        txtHeightMinInInch.Text = (Val(txtHeightMin.Text) * 12) + Val(txtHeightMinInch.Text)
                        txtHeightMinIncm.Text = Format(Val(txtHeightMinInInch.Text) * 2.54, "#0.00")
                        txtHeightMinInInch.Enabled = False
                        txtHeightMinIncm.Enabled = False
                    End If


                    'For Height Min in inches 
                Case "txtHeightMinInch"
                    ' Calc_WtKg_BMI()

                    If txtHeightMin.Text = "" And txtHeightMinInch.Text = "" Then
                        txtHeightMinIncm.Clear()
                        txtHeightMinInInch.Clear()
                        txtHeightMinInInch.Enabled = True
                        txtHeightMinIncm.Enabled = True
                    Else
                        txtHeightMinInInch.Text = (Val(txtHeightMin.Text) * 12) + Val(txtHeightMinInch.Text)
                        txtHeightMinIncm.Text = Format(Val(txtHeightMinInInch.Text) * 2.54, "#0.00")
                        'txtHeightMin.Text = Format(Val(txtHeightMinInInch.Text) Mod 12, "#0.00")
                        'txtHeightMinInch.Text = Split(CType(Val(txtHeightMinInInch.Text) / 12, String), ".", , CompareMethod.Text)(0)
                        txtHeightMinInInch.Enabled = False
                        txtHeightMinIncm.Enabled = False
                    End If


                    'For Height Min in inches 
                Case "txtHeightMinInInch"
                    ' Calc_WtKg_BMI()

                    If txtHeightMinInInch.Text <> "" Then
                        txtHeightMinIncm.Text = Format(Val(txtHeightMinInInch.Text) * 2.54, "#0.00")
                        txtHeightMinInch.Text = Format(Val(txtHeightMinInInch.Text) Mod 12, "#0.00")
                        txtHeightMin.Text = Split(CType(Val(txtHeightMinInInch.Text) / 12, String), ".", , CompareMethod.Text)(0)
                        txtHeightMin.Enabled = False
                        txtHeightMinInch.Enabled = False
                        txtHeightMinIncm.Enabled = False
                    Else
                        txtHeightMin.Clear()
                        txtHeightMinInch.Clear()
                        txtHeightMinIncm.Clear()
                        txtHeightMin.Enabled = True
                        txtHeightMinInch.Enabled = True
                        txtHeightMinIncm.Enabled = True
                    End If


                    'For Height Min in CM 
                Case "txtHeightMinIncm"
                    'Calc_WtKg_BMI()

                    If txtHeightMinIncm.Text <> "" Then
                        txtHeightMinInInch.Text = Format(Val(txtHeightMinIncm.Text) * 0.3937008, "#0.00")
                        txtHeightMinInch.Text = Format(Val(txtHeightMinInInch.Text) Mod 12, "#0.00")
                        txtHeightMin.Text = Split(CType(Val(txtHeightMinInInch.Text) / 12, String), ".", , CompareMethod.Text)(0)
                        txtHeightMin.Enabled = False
                        txtHeightMinInch.Enabled = False
                        txtHeightMinInInch.Enabled = False
                    Else
                        txtHeightMin.Clear()
                        txtHeightMinInch.Clear()
                        txtHeightMinInInch.Clear()
                        txtHeightMin.Enabled = True
                        txtHeightMinInch.Enabled = True
                        txtHeightMinInInch.Enabled = True
                    End If


                    'For Height Max in ft 
                Case "txtHeightMax"
                    ' Calc_WtKg_Max()

                    If txtHeightMax.Text = "" And txtHeightMaxInch.Text = "" Then
                        txtHeightMaxIncm.Clear()
                        txtHeightMaxInInch.Clear()
                        txtHeightMaxInInch.Enabled = False
                        txtHeightMaxIncm.Enabled = False
                    Else
                        txtHeightMaxInInch.Text = (Val(txtHeightMax.Text) * 12) + Val(txtHeightMaxInch.Text)
                        txtHeightMaxIncm.Text = Format(Val(txtHeightMaxInInch.Text) * 2.54, "#0.00")
                        txtHeightMaxInInch.Enabled = False
                        txtHeightMaxIncm.Enabled = False
                    End If


                    'For Height Max in Inch 
                Case "txtHeightMaxInch"
                    ' Calc_WtKg_Max()

                    If txtHeightMax.Text = "" And txtHeightMaxInch.Text = "" Then
                        txtHeightMaxIncm.Clear()
                        txtHeightMaxInInch.Clear()
                        txtHeightMaxInInch.Enabled = True
                        txtHeightMaxIncm.Enabled = True
                    Else
                        txtHeightMaxInInch.Text = (Val(txtHeightMax.Text) * 12) + Val(txtHeightMaxInch.Text)
                        txtHeightMaxIncm.Text = Format(Val(txtHeightMaxInInch.Text) * 2.54, "#0.00")
                        txtHeightMaxInInch.Enabled = False
                        txtHeightMaxIncm.Enabled = False
                    End If


                    'For Height Max in Inch
                Case "txtHeightMaxInInch"
                    ' Calc_WtKg_Max()

                    If txtHeightMaxInInch.Text <> "" Then
                        txtHeightMaxIncm.Text = Format(Val(txtHeightMaxInInch.Text) * 2.54, "#0.00")
                        txtHeightMaxInch.Text = Format(Val(txtHeightMaxInInch.Text) Mod 12, "#0.00")
                        txtHeightMax.Text = Split(CType(Val(txtHeightMaxInInch.Text) / 12, String), ".", , CompareMethod.Text)(0)
                        txtHeightMax.Enabled = False
                        txtHeightMaxInch.Enabled = False
                        txtHeightMaxIncm.Enabled = False
                    Else
                        txtHeightMax.Clear()
                        txtHeightMaxInch.Clear()
                        txtHeightMaxIncm.Clear()
                        txtHeightMax.Enabled = True
                        txtHeightMaxInch.Enabled = True
                        txtHeightMaxIncm.Enabled = True
                    End If


                    'For Height Max in CM
                Case "txtHeightMaxIncm"
                    ' Calc_WtKg_Max()

                    If txtHeightMaxIncm.Text <> "" Then
                        txtHeightMaxInInch.Text = Format(Val(txtHeightMaxIncm.Text) * 0.3937008, "#0.00")
                        txtHeightMaxInch.Text = Format(Val(txtHeightMaxInInch.Text) Mod 12, "#0.00")
                        txtHeightMax.Text = Split(CType(Val(txtHeightMaxInInch.Text) / 12, String), ".", , CompareMethod.Text)(0)
                        txtHeightMax.Enabled = False
                        txtHeightMaxInch.Enabled = False
                        txtHeightMaxInInch.Enabled = False
                    Else
                        txtHeightMax.Clear()
                        txtHeightMaxInch.Clear()
                        txtHeightMaxInInch.Clear()
                        txtHeightMax.Enabled = True
                        txtHeightMaxInch.Enabled = True
                        txtHeightMaxInInch.Enabled = True
                    End If


                    'For Weight Min in ft 
                Case "txtWeightlbs"
                    'If _WeightFlag = WeightIn.Lbs Then
                    If Val(txtWeightlbs.Text) > 0 Or Val(txtWtOz.Text) > 0 Then
                        txtWeightMin.Text = Format(Val(txtWeightlbs.Text) + Val(txtWtOz.Text) / 16, "#0.000")
                        txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45, "#0.000")
                        txtWeightMin.Enabled = False
                        txtWeightKg.Enabled = False
                    Else
                        txtWeightlbs.Clear()
                        txtWeightKg.Clear()
                        txtWeightMin.Clear()
                        txtWeightMin.Enabled = True
                        txtWeightKg.Enabled = True
                    End If
                    'Calc_WtKg_BMI()
                    'End If
                Case "txtWtOz"

                    'If _WeightFlag = WeightIn.Lbs Then
                    If Val(txtWeightlbs.Text) > 0 Or Val(txtWtOz.Text) > 0 Then
                        txtWeightMin.Text = Format(Val(txtWeightlbs.Text) + Val(txtWtOz.Text) / 16, "#0.000")
                        txtWeightKg.Text = Format(Val(txtWeightMin.Text) * 0.45, "#0.000")
                        txtWeightMin.Enabled = False
                        txtWeightKg.Enabled = False
                    Else
                        txtWeightlbs.Clear()
                        txtWeightKg.Clear()
                        txtWeightMin.Enabled = True
                        txtWeightKg.Enabled = True
                        'txtWeightChanged.Clear()
                    End If
                    'Calc_WtKg_BMI()
                    'End If

                Case "txtWeightMin"
                    Try
                        'If _WeightFlag = WeightIn.Lbs Then

                        If txtWeightMin.Text <> "" Then
                            txtWeightKg.Text = Format(Val(txtWeightMin.Text) * 0.45, "#0.000")
                            txtWeightlbs.Text = Decimal.Truncate(Val(txtWeightMin.Text))
                            Dim _decimalPlaces() As String = Split(txtWeightMin.Text, ".", , CompareMethod.Text)
                            If _decimalPlaces.Length > 1 Then
                                txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                                If Val(txtWtOz.Text) = 0 Then
                                    txtWtOz.Clear()
                                End If
                            Else
                                txtWtOz.Clear()
                            End If
                            txtWeightlbs.Enabled = False
                            txtWtOz.Enabled = False
                            txtWeightKg.Enabled = False
                        Else
                            txtWeightKg.Clear()
                            'txtWeightChanged.Clear()
                            txtWeightMin.Clear()
                            txtWtOz.Clear()
                            txtWeightlbs.Clear()
                            txtWeightlbs.Enabled = True
                            txtWtOz.Enabled = True
                            txtWeightKg.Enabled = True
                        End If
                        'Calc_WtKg_BMI()
                        'End If
                    Catch ex As Exception

                    End Try
                Case "txtWeightKg"
                    Try
                        'If _WeightFlag = WeightIn.Lbs Then
                        If txtWeightKg.Text <> "" Then
                            txtWeightMin.Text = Format(Val(txtWeightKg.Text) / 0.45, "#0.000")
                            txtWeightlbs.Text = Decimal.Truncate(Val(txtWeightMin.Text))
                            Dim _decimalPlaces() As String = Split(txtWeightMin.Text, ".", , CompareMethod.Text)
                            If _decimalPlaces.Length > 1 Then
                                txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                            Else
                                txtWtOz.Clear()
                            End If

                            txtWeightlbs.Enabled = False
                            txtWtOz.Enabled = False
                            txtWeightMin.Enabled = False
                        Else
                            txtWeightMin.Clear()
                            'txtWeightChanged.Clear()
                            txtWeightlbs.Clear()
                            txtWtOz.Clear()
                            txtWeightlbs.Enabled = True
                            txtWtOz.Enabled = True
                            txtWeightMin.Enabled = True
                        End If

                        'Calc_WtKg_BMI()
                        'End If
                    Catch ex As Exception

                    End Try

                Case "txtMaxWeightlbs"
                    'If _WeightFlag = WeightIn.Lbs Then
                    If Val(txtMaxWeightlbs.Text) > 0 Or Val(txtMaxWtOz.Text) > 0 Then
                        txtWeightMax.Text = Format(Val(txtMaxWeightlbs.Text) + Val(txtMaxWtOz.Text) / 16, "#0.000")
                        txtMaxWeightKg.Text = Format(Val(txtMaxWeightlbs.Text) * 0.45, "#0.000")
                        'If txtWeightlbs.Tag <> 0 Then
                        ' txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                        ''End If
                        txtWeightMax.Enabled = False
                        txtMaxWeightKg.Enabled = False
                    Else
                        txtMaxWeightlbs.Clear()
                        txtMaxWeightKg.Clear()
                        txtWeightMax.Clear()
                        txtWeightMax.Enabled = True
                        txtMaxWeightKg.Enabled = True
                    End If
                    'Calc_WtKg_BMI()
                    'End If

                Case "txtMaxWtOz"
                    'If _WeightFlag = WeightIn.Lbs Then
                    If Val(txtMaxWeightlbs.Text) > 0 Or Val(txtMaxWtOz.Text) > 0 Then
                        txtWeightMax.Text = Format(Val(txtMaxWeightlbs.Text) + Val(txtMaxWtOz.Text) / 16, "#0.000")
                        txtMaxWeightKg.Text = Format(Val(txtWeightMax.Text) * 0.45, "#0.000")
                    Else
                        txtMaxWeightlbs.Clear()
                        txtMaxWeightKg.Clear()
                        'txtWeightChanged.Clear()
                    End If
                    'Calc_WtKg_BMI()
                    'End If

                Case "txtWeightMax"

                    'Try
                    'If _WeightFlag = WeightIn.Lbs Then

                    If txtWeightMax.Text <> "" Then
                        txtMaxWeightKg.Text = Format(Val(txtWeightMax.Text) * 0.45, "#0.000")
                        txtMaxWeightlbs.Text = Decimal.Truncate(Val(txtWeightMax.Text))
                        Dim _decimalPlaces() As String = Split(txtWeightMax.Text, ".", , CompareMethod.Text)
                        If _decimalPlaces.Length > 1 Then
                            txtMaxWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                            If Val(txtMaxWtOz.Text) = 0 Then
                                txtMaxWtOz.Clear()
                            End If
                        Else
                            txtMaxWtOz.Clear()
                        End If
                        txtMaxWeightlbs.Enabled = False
                        txtMaxWtOz.Enabled = False
                        txtMaxWeightKg.Enabled = False

                    Else
                        txtMaxWeightKg.Clear()
                        'txtWeightChanged.Clear()
                        txtWeightMax.Clear()
                        txtMaxWtOz.Clear()
                        txtMaxWeightlbs.Clear()
                        txtMaxWeightlbs.Enabled = True
                        txtMaxWtOz.Enabled = True
                        txtMaxWeightKg.Enabled = True
                    End If
                    'End If
                    'Calc_WtKg_BMI()

                Case "txtMaxWeightKg"
                    'If _WeightFlag = WeightIn.Lbs Then
                    If txtMaxWeightKg.Text <> "" Then
                        txtWeightMax.Text = Format(Val(txtMaxWeightKg.Text) / 0.45, "#0.000")
                        txtMaxWeightlbs.Text = Decimal.Truncate(Val(txtWeightMax.Text))
                        Dim _decimalPlaces() As String = Split(txtWeightMax.Text, ".", , CompareMethod.Text)
                        If _decimalPlaces.Length > 1 Then
                            txtMaxWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                        Else
                            txtMaxWtOz.Clear()
                        End If

                        txtMaxWeightlbs.Enabled = False
                        txtMaxWtOz.Enabled = False
                        txtWeightMax.Enabled = False
                        'If txtWeightlbs.Tag <> 0 Then
                        '    txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                        'End If
                    Else
                        txtWeightMax.Clear()
                        'txtWeightChanged.Clear()
                        txtMaxWeightlbs.Clear()
                        txtMaxWtOz.Clear()
                        txtMaxWeightlbs.Enabled = True
                        txtMaxWtOz.Enabled = True
                        txtWeightMax.Enabled = True
                    End If
                    'Calc_WtKg_BMI()
                    'End If

                Case "txtTemperatureMin"
                    'If _TempInCelcius = False Then
                    ''To convert Fahrenheit to Celcius
                    If txtTemperatureMin.Text <> "" Then
                        txtTemperatureMinIncel.Text = Format((5 / 9) * (Val(txtTemperatureMin.Text) - 32), "#0.00")
                        txtTemperatureMinIncel.Enabled = False
                    Else
                        txtTemperatureMinIncel.Clear()
                        txtTemperatureMin.Enabled = True
                        txtTemperatureMinIncel.Enabled = True
                    End If
                    'End If

                Case "txtTemperatureMinIncel"
                    'If _TempInCelcius = True Then
                    ''To convert Celcius to Fahrenheit
                    If txtTemperatureMinIncel.Text <> "" Then
                        txtTemperatureMin.Text = Format((9 / 5) * Val(txtTemperatureMinIncel.Text) + 32, "#0.00")
                        txtTemperatureMin.Enabled = False
                    Else
                        txtTemperatureMin.Clear()
                        txtTemperatureMinIncel.Enabled = True
                        txtTemperatureMin.Enabled = True
                    End If

                    'If txtTemperature.TextLength = 6 Then
                    '    txtBPSittingMax.Focus()
                    'End If
                    'End If

                Case "txtTemperatureMax"
                    'If _TempInCelcius = False Then
                    ''To convert Fahrenheit to Celcius
                    If txtTemperatureMax.Text <> "" Then
                        txtTemperatureMaxInCel.Text = Format((5 / 9) * (Val(txtTemperatureMax.Text) - 32), "#0.00")
                        txtTemperatureMaxInCel.Enabled = False
                    Else
                        txtTemperatureMaxInCel.Clear()
                        txtTemperatureMax.Enabled = True
                        txtTemperatureMaxInCel.Enabled = True
                    End If
                    'End If

                Case "txtTemperatureMaxInCel"
                    'If _TempInCelcius = True Then
                    ''To convert Celcius to Fahrenheit
                    If txtTemperatureMaxInCel.Text <> "" Then
                        txtTemperatureMax.Text = Format((9 / 5) * Val(txtTemperatureMaxInCel.Text) + 32, "#0.00")
                        txtTemperatureMax.Enabled = False
                    Else
                        txtTemperatureMax.Clear()
                        txtTemperatureMaxInCel.Enabled = True
                        txtTemperatureMax.Enabled = True
                    End If
                    'End If

                Case "txtHeadCircumMinInInch"
                    'If _HeadCircumInch = True Then
                    If txtHeadCircumMinInInch.Text <> "" Then
                        txtHeadCircumMin.Text = Format(Val(txtHeadCircumMinInInch.Text) * 2.54, "#0.00")
                        txtHeadCircumMin.Enabled = False
                    Else
                        txtHeadCircumMin.Clear()
                        txtHeadCircumMinInInch.Enabled = True
                        txtHeadCircumMin.Enabled = True
                    End If
                    'End If

                Case "txtHeadCircumMin"
                    'If _HeadCircumInch = False Then
                    If txtHeadCircumMin.Text <> "" Then
                        txtHeadCircumMinInInch.Text = Format(Val(txtHeadCircumMin.Text) * 0.3937008, "#0.00")
                        txtHeadCircumMinInInch.Enabled = False
                    Else
                        txtHeadCircumMinInInch.Clear()
                        txtHeadCircumMin.Enabled = True
                        txtHeadCircumMinInInch.Enabled = True
                    End If
                    'End If

                Case "txtHeadCircumMaxInInch"
                    'If _HeadCircumInch = True Then
                    If txtHeadCircumMaxInInch.Text <> "" Then
                        txtHeadCircumMax.Text = Format(Val(txtHeadCircumMaxInInch.Text) * 2.54, "#0.00")
                        txtHeadCircumMax.Enabled = False
                    Else
                        txtHeadCircumMax.Clear()
                        txtHeadCircumMax.Enabled = True
                        txtHeadCircumMaxInInch.Enabled = True

                    End If
                    'End If
                Case "txtHeadCircumMax"
                    'If _HeadCircumInch = False Then
                    If txtHeadCircumMax.Text <> "" Then
                        txtHeadCircumMaxInInch.Text = Format(Val(txtHeadCircumMax.Text) * 0.3937008, "#0.00")
                        txtHeadCircumMaxInInch.Enabled = False
                    Else
                        txtHeadCircumMaxInInch.Clear()
                        txtHeadCircumMax.Enabled = True
                        txtHeadCircumMaxInInch.Enabled = True
                    End If
                    'End If


                Case "txtStatureMinInInch"
                    'If _StatureInInch = True Then
                    If txtStatureMinInInch.Text <> "" Then
                        txtStatureMin.Text = Format(Val(txtStatureMinInInch.Text) * 2.54, "#0.00")
                        txtStatureMin.Enabled = False
                    Else
                        txtStatureMin.Clear()
                        txtStatureMin.Enabled = True
                        txtStatureMinInInch.Enabled = True
                    End If
                    'End If

                Case "txtStatureMin"
                    'If _StatureInInch = False Then
                    If txtStatureMin.Text <> "" Then
                        txtStatureMinInInch.Text = Format(Val(txtStatureMin.Text) * 0.3937008, "#0.00")
                        txtStatureMinInInch.Enabled = False
                    Else
                        txtStatureMinInInch.Clear()
                        txtStatureMin.Enabled = True
                        txtStatureMinInInch.Enabled = True
                    End If
                    'End If

                Case "txtStatureMaxInInch"
                    'If _StatureInInch = True Then
                    If txtStatureMaxInInch.Text <> "" Then
                        txtStatureMax.Text = Format(Val(txtStatureMaxInInch.Text) * 2.54, "#0.00")
                        txtStatureMax.Enabled = False
                    Else
                        txtStatureMax.Clear()
                        txtStatureMax.Enabled = True
                        txtStatureMaxInInch.Enabled = True
                    End If
                    'End If

                Case "txtStatureMax"
                    'If _StatureInInch = False Then
                    If txtStatureMax.Text <> "" Then
                        txtStatureMaxInInch.Text = Format(Val(txtStatureMax.Text) * 0.3937008, "#0.00")
                        txtStatureMaxInInch.Enabled = False
                    Else
                        txtStatureMaxInInch.Clear()
                        txtStatureMaxInInch.Enabled = True
                        txtStatureMax.Enabled = True
                    End If
                    'End If

            End Select

            _IsRecordsLoading = False
        End If


    End Sub

    Private Sub Calc_WtKg_Max()
        Dim dHeight As Double
        If Val(txtWeightKg.Text) <> 0.0 Then
            txtWeightlbs.Text = Format(Val(txtWeightKg.Text) / 0.45, "#0.00")
            'dHeight = Val(mskHeight.Text) * Val(mskHeight.Text)
            dHeight = FtToMtr(Val(txtHeightMax.Text), Val(txtHeightMaxInch.Text))
            dHeight = dHeight * dHeight
            'If (Val(txtWeightKg.Text) > 0 And dHeight > 0) Then
            'txtBMI.Text = Format(Val(txtWeightKg.Text) / dHeight, "#0.00")
            'Else
            'txtBMI.Text = ""
            ' End If
            'Else
            'txtBMI.Text = ""

        End If

    End Sub
    'Private Sub txtWtOz_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWtOz.Validating
    '    Try
    '        If Val(txtWeightlbs.Text) <= 0 Then
    '            If Val(txtWtOz.Text) >= 16 Then
    '                txtWeightlbs.Text = Val(txtWtOz.Text) \ 16
    '                txtWtOz.Text = Format(Val(txtWtOz.Text) Mod 16, "#0.00")
    '                'Exit Sub
    '            End If
    '        Else
    '            If Val(txtWtOz.Text) >= 16 Then
    '                MessageBox.Show("Invalid value of Oz", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                txtWtOz.Focus()
    '                Exit Sub
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'Allow only numeric and decimal point keys

            If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
                'e.KeyChar.IsDigit(e.KeyChar)
                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    'Shubhangi
    Private Sub Calc_WtKg_BMI()
        Dim dHeight As Double
        'If Val(txtWeightKg.Text) <> 0.0 Then
        ' txtWeightMin.Text = Format(Val(txtWeightKg.Text) / 0.45, "#0.00")
        'dHeight = Val(mskHeight.Text) * Val(mskHeight.Text)
        dHeight = FtToMtr(Val(txtHeightMin.Text), Val(txtHeightMinInch.Text))
        dHeight = dHeight * dHeight
        'If (Val(txtWeightKg.Text) > 0 And dHeight > 0) Then
        'txtBMI.Text = Format(Val(txtWeightKg.Text) / dHeight, "#0.00")
        'Else
        'txtBMI.Text = ""
        ' End If
        'Else
        'txtBMI.Text = ""

        'End If

    End Sub

    Private Function FtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
        Return (Ft * 30.48 + Inch * 2.54) / 100
        ''   1 ft = 30.48 cm
        ''   1 inch = 2.54 cm
    End Function
    'End Shubhangi

    Private Sub rbMale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMale.CheckedChanged
        If rbMale.Checked Then
            rbMale.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbFemale.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

            If IsValidEntries() = False Then
                Exit Sub
            End If

            TempSaveNorms(_NormName)

            _Gender = "Male"

            ResetPanel()
            FillNorms(_NormName)
        End If
    End Sub

    Private Sub rbFemale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbFemale.CheckedChanged
        If rbFemale.Checked Then
            rbFemale.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rbMale.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

            If IsValidEntries() = False Then
                Exit Sub
            End If

            TempSaveNorms(_NormName)

            _Gender = "Female"

            ResetPanel()
            FillNorms(_NormName)
        End If
    End Sub
    Private Sub txtAllBoxes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeightMin.KeyPress, txtWeightMin.KeyPress, txtWeightMax.KeyPress, txtTemperatureMin.KeyPress, txtTemperatureMax.KeyPress, txtStatureMin.KeyPress, txtStatureMax.KeyPress, txtRespRateMin.KeyPress, txtRespRateMax.KeyPress, txtPulseOXmin.KeyPress, txtPulseOXmax.KeyPress, txtPulseMin.KeyPress, txtPulseMax.KeyPress, txtHeightMinInch.KeyPress, txtHeightMaxInch.KeyPress, txtHeightMax.KeyPress, txtHeadCircumMin.KeyPress, txtHeadCircumMax.KeyPress, txtBPSystolicMin.KeyPress, txtBPSystolicMax.KeyPress, txtBPDiastolicMin.KeyPress, txtBPDiastolicMax.KeyPress
        ValidateNumeric(CType(sender, TextBox).Text, e)

    End Sub

    'Shubhangi 20090909 
    'Handle key down event B'coz this fire before text change & we want to set some Boolean variables value for temp,head circumference & stature on all text boxes's key down event
    Private Sub txtAllBoxes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHeightMin.KeyDown, txtWeightMin.KeyDown, txtWeightMax.KeyDown, txtTemperatureMin.KeyDown, txtTemperatureMinIncel.KeyDown, txtTemperatureMaxInCel.KeyDown, txtTemperatureMax.KeyDown, txtStatureMin.KeyDown, txtStatureMinInInch.KeyDown, txtStatureMaxInInch.KeyDown, txtStatureMax.KeyDown, txtRespRateMin.KeyDown, txtRespRateMax.KeyDown, txtPulseOXmin.KeyDown, txtPulseOXmax.KeyDown, txtPulseMin.KeyDown, txtPulseMax.KeyDown, txtHeightMinInch.KeyDown, txtHeightMaxInch.KeyDown, txtHeightMax.KeyDown, txtHeadCircumMin.KeyDown, txtHeadCircumMinInInch.KeyDown, txtHeadCircumMaxInInch.KeyDown, txtHeadCircumMax.KeyDown, txtBPSystolicMin.KeyDown, txtBPSystolicMax.KeyDown, txtBPDiastolicMin.KeyDown, txtBPDiastolicMax.KeyDown, txtHeightMinInInch.KeyDown, txtHeightMinIncm.KeyDown, txtHeightMaxInInch.KeyDown, txtHeightMaxIncm.KeyDown, txtWeightlbs.KeyDown, txtWtOz.KeyDown, txtWeightKg.KeyDown, txtMaxWeightlbs.KeyDown, txtMaxWtOz.KeyDown, txtMaxWeightKg.KeyDown
        'Select Case CType(sender, TextBox).Name
        'Case "txtTemperatureMin"
        '    _TempInCelcius = False
        'Case "txtTemperatureMinIncel"
        '    _TempInCelcius = True
        'Case "txtTemperatureMax"
        '    _TempInCelcius = False
        'Case "txtTemperatureMaxInCel"
        '    _TempInCelcius = True
        '    Case "txtHeadCircumMinInInch"
        '        _HeadCircumInch = True
        '    Case "txtHeadCircumMin"
        '        _HeadCircumInch = False
        '    Case "txtHeadCircumMax"
        '        _HeadCircumInch = False
        '    Case "txtHeadCircumMaxInInch"
        '        _HeadCircumInch = True
        '    Case "txtStatureMinInInch"
        '        _StatureInInch = True
        '    Case "txtStatureMin"
        '        _StatureInInch = False
        '    Case "txtStatureMaxInInch"
        '        _StatureInInch = True
        '    Case "txtStatureMax"
        '        _StatureInInch = False
        'End Select
    End Sub

    Private Sub txtHeightMin_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHeightMin.KeyUp
        If Len(txtHeightMin.Text) > 0 Or Len(txtHeightMinInch.Text) > 0 Then
            txtHeightMinIncm.Enabled = False
            txtHeightMinInInch.Enabled = False
        Else
            txtHeightMinIncm.Enabled = True
            txtHeightMinInInch.Enabled = True
            txtHeightMin.Enabled = True
            txtHeightMinInch.Enabled = True
        End If
        ''Shubhangi
        'If txtHeightMinInch.Text > 12 Then
        '    txtHeightMin.Text = Format(Val(txtHeightMinInInch.Text) Mod 12, "#0.00")
        '    txtHeightMinInch.Text = Split(CType(Val(txtHeightMinInInch.Text) / 12, String), ".", , CompareMethod.Text)(0)
        'End If

    End Sub

    Private Sub txtHeightMin_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMin.Validating
        If txtHeightMin.TextLength = 1 Then
            txtHeightMinInch.Focus()
        End If

        'If (Val(txtft.Text) * 12 + Val(txtInch.Text)) > 84 Then
        '    MessageBox.Show("Height must be in range (0-84)Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    'txtInch.Focus()
        '    Exit Sub
        'End If

        If txtHeightMin.Text = "" Then
            txtHeightMin.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub txtHeightMinInch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHeightMinInch.KeyUp
        If Len(txtHeightMinInch.Text) > 0 Or Len(txtHeightMin.Text) > 0 Then
            txtHeightMaxIncm.Enabled = False
            txtHeightMinInInch.Enabled = False
        Else
            txtHeightMaxIncm.Enabled = True
            txtHeightMinInInch.Enabled = True
            txtHeightMin.Enabled = True
            txtHeightMinInch.Enabled = True
        End If
    End Sub

    Private Sub tlsNorms_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsNorms.ItemClicked

    End Sub
    ''Events changed by Mayuri:20100329-To fix issue:#6526-Vitals:Validation message is showing more times in Master.
    Private Sub txtWtOz_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWtOz.Validating
        Try


            If Val(txtWeightlbs.Text) <= 0 Then
                If Val(txtWtOz.Text) >= 16 Then 'And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtWtOz.Text)

                    _Ft = Math.Floor(_TotalInches / 16)
                    _Inches = Math.Round(_TotalInches Mod 16, 2)
                    txtWeightlbs.Text = _Ft
                    txtWtOz.Text = _Inches
                    'Exit Sub
                End If
            Else
                If Val(txtWtOz.Text) >= 16 Then
                    MessageBox.Show("Invalid value of Oz", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWtOz.Focus()

                    Exit Sub

                End If

            End If

            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtHeightMinInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMinInch.Validating
        Try


            If Val(txtHeightMin.Text) <= 0 Then
                If Val(txtHeightMinInch.Text) >= 12 Then 'And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtHeightMinInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtHeightMin.Text = _Ft
                    txtHeightMinInch.Text = _Inches
                    'Exit Sub
                End If
            Else
                If Val(txtHeightMinInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMinInch.Focus()

                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtHeightMaxInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtHeightMaxInch.Validating
        Try


            If Val(txtHeightMax.Text) <= 0 Then
                If Val(txtHeightMaxInch.Text) >= 12 Then 'And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtHeightMaxInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtHeightMax.Text = _Ft
                    txtHeightMaxInch.Text = _Inches
                    'Exit Sub
                End If
            Else
                If Val(txtHeightMaxInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtHeightMaxInch.Focus()

                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtMaxWtOz_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMaxWtOz.Validating
        Try


            If Val(txtMaxWeightlbs.Text) <= 0 Then
                If Val(txtMaxWtOz.Text) >= 16 Then 'And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtMaxWtOz.Text)

                    _Ft = Math.Floor(_TotalInches / 16)
                    _Inches = Math.Round(_TotalInches Mod 16, 2)
                    txtMaxWeightlbs.Text = _Ft
                    txtMaxWtOz.Text = _Inches
                    'Exit Sub
                End If
            Else
                If Val(txtMaxWtOz.Text) >= 16 Then
                    MessageBox.Show("Invalid value of Oz", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtMaxWtOz.Focus()

                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''end code added by Mayuri:20100329
    Private Sub SpiltNorm(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (SaveVitalNormsForSplit_Merge() = True) Then
            Else
                Exit Sub
            End If
            Dim strNorm As String = _assignNormName
            Dim strVitalRangeArr() As String
            strVitalRangeArr = strNorm.Split("-")
            If (strVitalRangeArr.Length = 2) Then
                Dim fromAge As Int16 = strVitalRangeArr(0)
                Dim toAge As Int16 = strVitalRangeArr(1)
                Dim ofrmAddVitalNorm As frmAddVitalNorm = New frmAddVitalNorm(fromAge, toAge)
                ofrmAddVitalNorm.StartPosition = FormStartPosition.CenterParent
                ofrmAddVitalNorm.ShowDialog(IIf(IsNothing(ofrmAddVitalNorm.Parent), Me, ofrmAddVitalNorm.Parent))
                'FillNormTree(fromAge, ofrmAddVitalNorm.newToAge)
                If (ofrmAddVitalNorm.DialogResult = Windows.Forms.DialogResult.OK) Then
                    ReloadFormWithLastSelectedNode(_selectedNodeIndex)
                End If
                ofrmAddVitalNorm.Dispose()
                ofrmAddVitalNorm = Nothing
            Else
                Exit Sub
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub MergeNorm(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (SaveVitalNormsForSplit_Merge() = True) Then
            Else
                Exit Sub
            End If
            Dim strNorm As String = _assignNormName
            If (MessageBox.Show("This Vital Norm will be merged with the next one and will no longer be available. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes) Then
                Dim strVitalRangeArr() As String
                strVitalRangeArr = strNorm.Split("-")
                If (strVitalRangeArr.Length = 2) Then
                    Dim toAge As Int16 = strVitalRangeArr(0)
                    Dim fromAge As Int16 = strVitalRangeArr(1)
                    MergeOBVitalsNorms(toAge, fromAge)
                Else
                    Exit Sub
                End If
                ReloadFormWithLastSelectedNode(_selectedNodeIndex)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub MergeOBVitalsNorms(ByVal oldForm As Int16, ByVal oldTo As Integer)
        Try
            Dim oDB As New DataBaseLayer
            Dim oParamater As DBParameter
            Try
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nFromAge "
                oParamater.Value = oldForm
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@nToAge"
                oParamater.Value = oldTo
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
                oDB.ExecuteNon_Query("gsp_MergeVitalNorms")
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                oDB.Dispose()
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvNorms_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvNorms.MouseDown

    End Sub

    Private Sub trvNorms_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvNorms.NodeMouseClick
        _assignNormName = e.Node.Tag
        _selectedNodeIndex = e.Node.Index
    End Sub
    Private Sub ReloadFormWithLastSelectedNode(ByVal nodeindex As Integer)
        Try
            pnlMainVitalsNorms.Visible = False
            FillNormTree()
            oAllNorms = oVital.GetNorms()
            _NormName = trvNorms.Nodes(nodeindex).Text
            lblNormHeader.Text = Space(5) & trvNorms.Nodes(nodeindex).Text
            Dim strLimit() As String = Split(trvNorms.Nodes(nodeindex).Tag, "-")
            _FromAge = Convert.ToInt16(strLimit(0))
            _ToAge = Convert.ToInt16(strLimit(1))

            ResetPanel()
            FillNorms(_NormName)
            trvNorms.SelectedNode = trvNorms.Nodes(nodeindex)
            ' rbMale.Checked = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Function SaveVitalNormsForSplit_Merge() As Boolean
        Dim result As Windows.Forms.DialogResult

        If _IsRecordModified = True And _IsSaveClicked = False Then

            result = MessageBox.Show("This operation requires the current record to be saved. Do you want to save it?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If result = Windows.Forms.DialogResult.Cancel Then
                Return False
            ElseIf result = Windows.Forms.DialogResult.Yes Then
                ''Added by Mayuri:20100329-Issue:#6526-Vitals:Validation message is showing more times in Master.
                tlsNorms.Focus()
                If Not Me.ActiveControl Is tlsNorms Then
                    Return False
                End If
                ''end code Added by Mayuri:20100329
                If IsValidEntries() = True Then
                    Try
                        tlsNorms.Focus()
                        If Not Me.ActiveControl Is tlsNorms Then
                            SaveVitalNormsForSplit_Merge = Nothing
                            Exit Function
                        End If

                        '_IsSaveClicked = True
                        If IsValidEntries() = True Then
                            TempSaveNorms(_NormName)
                            SaveNorms(True)
                        End If
                        '                        tsb_Save_Click(Nothing, Nothing)
                        Return True
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        Return False
                    End Try
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return True
        End If
        ' Return Nothing
    End Function

    Private Sub chkVitalNorms_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVitalNorms.CheckedChanged
        pnlMainVitalsNorms.Enabled = chkVitalNorms.Checked
        pnlLeft.Enabled = chkVitalNorms.Checked

    End Sub

    Private Sub tsb_RestoreDefaultSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_RestoreDefaultSetting.Click
        Try
            If (MessageBox.Show("Vital Norms will be replaced by default norms and current norms will no longer be available. Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes) Then
                RestoreDeafultSetting()
                ReloadFormWithLastSelectedNode(0)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Function RestoreDeafultSetting() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDB.Execute("gsp_RestoreDefaultVitalNorms", oDBParameters)
            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            Return False
        Catch ex As Exception
            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function
End Class