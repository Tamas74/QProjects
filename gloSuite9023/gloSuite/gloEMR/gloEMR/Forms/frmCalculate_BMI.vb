Public Class frmCalculate_BMI
    ' Public Shared blnModify As Boolean
  
    Enum WeightIn
        Lbs
        Kg
        LbsOz
    End Enum
    ''Note :Tag values are given to each radio button at design time''
#Region "Enum Declarations"
    ''Added by Mayuri:20110105-Added ODI calculation Screen on vitals
    ''Enum to store Section name into table
    Enum SectionName
        Section1 = 1
        Section2 = 2
        Section3 = 3
        Section4 = 4
        Section5 = 5
        Section6 = 6
        Section7 = 7
        Section8 = 8
        Section9 = 9
        Section10 = 10
    End Enum
    ' ''Enum to store Selected option value into table
    'Enum Section
    '    OptionA = 0
    '    OptionB = 1
    '    OptionC = 2
    '    OptionD = 3
    '    OptionE = 4
    '    OptionF = 5
    'End Enum
    

    Enum HtFlag
        FtInch = 0
        Inch = 1
        Centimeter = 2
    End Enum
#End Region
#Region "Private Variables"
    Dim _WaightFlag As WeightIn
    '  Dim frmvitals As New frmPatientVitals()
    Dim _HeightFlag As HtFlag

    Dim _PatientID As Long
    Dim _VitalID As Long
    Dim _VisitID As Long
    Dim _dtVitalDate As DateTime
    Dim _isformLoad As Boolean
    Dim _IsSavenClose As Boolean = False
    Dim _IsCancel As Boolean = False
    Dim _SectionValue As String
    Dim _SectionName As String
    Dim _strtxtODIFromVitals As String
#End Region
#Region "Public Variables"
    Public _ArrList As New ArrayList
    Public Shared strtxtODI As String = ""
#End Region
    Public Property IsSavenClose() As Boolean
        Get
            Return _IsSavenClose
        End Get
        Set(ByVal value As Boolean)
            _IsSavenClose = value
        End Set
    End Property
    Public Property IsCancel() As Boolean
        Get
            Return _IsCancel
        End Get
        Set(ByVal value As Boolean)
            _IsCancel = value
        End Set
    End Property
    '  Dim _Validate As Boolean = True

    Private Sub txtft_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtft.KeyPress
        'Allow only numeric and decimal point keys
        'AllowDecimal(txtft.Text, e)
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtInch.Focus()
                    _HeightFlag = HtFlag.FtInch
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub txtft_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtft.KeyUp
        If Len(txtft.Text) > 0 Or Len(txtInch.Text) > 0 Then
            txtHtInCm.Enabled = False
            txtHtInInches.Enabled = False
        Else
            txtHtInCm.Enabled = True
            txtHtInInches.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
        End If
    End Sub

    Private Sub txtft_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtft.TextChanged
        Calc_WtKg_BMI()
        If _HeightFlag = HtFlag.FtInch Then
            If txtft.Text = "" AndAlso txtInch.Text = "" Then
                txtHtInCm.Clear()
                txtHtInInches.Clear()
            Else
                txtHtInInches.Text = (Val(txtft.Text) * 12) + Val(txtInch.Text)
                txtHtInCm.Text = Format(Val(txtHtInInches.Text) * 2.54, "#0.00")
            End If
        End If
    End Sub
    Private Sub txtft_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtft.Validating
        If txtft.TextLength = 1 Then
            txtInch.Focus()
        End If

        'If (Val(txtft.Text) * 12 + Val(txtInch.Text)) > 84 Then
        '    MessageBox.Show("Height must be in range (0-84)Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    'txtInch.Focus()
        '    Exit Sub
        'End If

        If txtft.Text = "" Then
            txtft.Text = ""
            Exit Sub
        End If
    End Sub
    Private Sub AllRadioButtonChanges_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbSection1_A.CheckedChanged, RbSection1_B.CheckedChanged, RbSection1_C.CheckedChanged, RbSection1_D.CheckedChanged, RbSection1_E.CheckedChanged, RbSection1_F.CheckedChanged, RbSection2_A.CheckedChanged, RbSection2_B.CheckedChanged, RbSection2_C.CheckedChanged, RbSection2_D.CheckedChanged, RbSection2_E.CheckedChanged, RbSection2_F.CheckedChanged, RbSection3_A.CheckedChanged, RbSection3_B.CheckedChanged, RbSection3_C.CheckedChanged, RbSection3_D.CheckedChanged, RbSection3_E.CheckedChanged, RbSection3_F.CheckedChanged, RbSection4_A.CheckedChanged, RbSection4_B.CheckedChanged, RbSection4_C.CheckedChanged, RbSection4_D.CheckedChanged, RbSection4_E.CheckedChanged, RbSection4_F.CheckedChanged, RbSection5_A.CheckedChanged, RbSection5_B.CheckedChanged, RbSection5_C.CheckedChanged, RbSection5_D.CheckedChanged, RbSection5_E.CheckedChanged, RbSection5_F.CheckedChanged, RbSection6_A.CheckedChanged, RbSection6_B.CheckedChanged, RbSection6_C.CheckedChanged, RbSection6_D.CheckedChanged, RbSection6_E.CheckedChanged, RbSection6_F.CheckedChanged, RbSection7_A.CheckedChanged, RbSection7_B.CheckedChanged, RbSection7_C.CheckedChanged, RbSection7_D.CheckedChanged, RbSection7_E.CheckedChanged, RbSection7_F.CheckedChanged, RbSection8_A.CheckedChanged, RbSection8_B.CheckedChanged, RbSection8_C.CheckedChanged, RbSection8_D.CheckedChanged, RbSection8_E.CheckedChanged, RbSection8_F.CheckedChanged, RbSection9_A.CheckedChanged, RbSection9_B.CheckedChanged, RbSection9_C.CheckedChanged, RbSection9_D.CheckedChanged, RbSection9_E.CheckedChanged, RbSection9_F.CheckedChanged, RbSection10_A.CheckedChanged, RbSection10_B.CheckedChanged, RbSection10_C.CheckedChanged, RbSection10_D.CheckedChanged, RbSection10_E.CheckedChanged, RbSection10_F.CheckedChanged
        Try

     

            Dim _value As Int16
            If (IsNothing(sender) = False) Then
                Dim radiobutton As RadioButton
                RadioButton = CType(sender, RadioButton)

                If radiobutton.Checked Then
                    radiobutton.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                Else
                    radiobutton.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
                End If
            End If
            _value = CalculateODIPercent()
            _value = _value * 2
            txtODIPercent.Text = _value
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function CalculateODIPercent() As String
        Try
            Dim _value As Int16
            Dim _value1 As Int16
            Dim _value2 As Int16
            Dim _value3 As Int16
            Dim _value4 As Int16
            Dim _value5 As Int16
            Dim _value6 As Int16
            Dim _value7 As Int16
            Dim _value8 As Int16
            Dim _value9 As Int16
            Dim _value10 As Int16

            Dim oRadioButton As Control.ControlCollection
            Dim oPanel As Control.ControlCollection
            oPanel = pnlQuestionaire.Controls
            Dim opnl As Panel
            For Each opanel1 As Control In oPanel
                If TypeOf opanel1 Is Panel Then
                    opnl = CType(opanel1, Panel)
                    If opnl.Name = "pnlSection1" Then
                        oRadioButton = pnlSection1.Controls
                        _value1 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection2" Then
                        oRadioButton = pnlSection2.Controls
                        _value2 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection3" Then
                        oRadioButton = pnlSection3.Controls
                        _value3 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection4" Then
                        oRadioButton = pnlSection4.Controls
                        _value4 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection5" Then
                        oRadioButton = pnlSection5.Controls
                        _value5 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection6" Then
                        oRadioButton = pnlSection6.Controls
                        _value6 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection7" Then
                        oRadioButton = pnlSection7.Controls
                        _value7 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection8" Then
                        oRadioButton = pnlSection8.Controls
                        _value8 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection9" Then
                        oRadioButton = pnlSection9.Controls
                        _value9 = findRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection10" Then
                        oRadioButton = pnlSection10.Controls
                        _value10 = findRadioButtons(oRadioButton)
                    End If
                End If
            Next
            _value = _value1 + _value2 + _value3 + _value4 + _value5 + _value6 + _value7 + _value8 + _value9 + _value10
            Return _value
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Function findRadioButtons(ByVal oRadioButton As Control.ControlCollection)
        Try
            Dim _value As Int16
            Dim oRad As RadioButton
            For Each oRadioButton1 As Control In oRadioButton
                If TypeOf oRadioButton1 Is RadioButton Then
                    oRad = CType(oRadioButton1, RadioButton)
                    If oRad.Checked = True Then
                        _value = _value + oRad.Tag
                        Exit For
                    End If
                End If


                ''Exit For
            Next
            Return _value
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End Try

    End Function
    Private Sub txtInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInch.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtInch.Text, e)
        _HeightFlag = HtFlag.FtInch


        'If txtInch.TextLength = 5 Then
        '    txtWeightKg.Focus()
        'End If
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'Allow only numeric and decimal point keys
            If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then

                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub txtInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInch.Validating
        Try
            If Val(txtft.Text) <= 0 Then
                If Val(txtInch.Text) >= 12 Then 'And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtft.Text = _Ft
                    txtInch.Text = _Inches
                    'Exit Sub
                End If

            Else
                If Val(txtInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtInch.Focus()
                    Exit Sub
                End If
            End If

            ''SUDHIR COMMENT 20081203
            'If (Val(txtft.Text) * 12 + Val(txtInch.Text)) > 84 And _Validate = True Then
            '    MessageBox.Show("Height must be in range (0-84)Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtInch.Focus()
            '    Exit Sub
            'End If

            If txtInch.Text = "" Then
                txtInch.Text = ""
                Exit Sub
            End If

            Calc_WtKg_BMI()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtInch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInch.TextChanged
        Try
            If txtInch.TextLength = 5 Then
                txtWeightlbs.Focus()
            End If
            Calc_WtKg_BMI()
            If _HeightFlag = HtFlag.FtInch Then
                If txtft.Text = "" AndAlso txtInch.Text = "" Then
                    txtHtInCm.Clear()
                    txtHtInInches.Clear()
                Else
                    txtHtInInches.Text = (Val(txtft.Text) * 12) + Val(txtInch.Text)
                    txtHtInCm.Text = Format(Val(txtHtInInches.Text) * 2.54, "#0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtInch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInch.KeyUp
        If Len(txtInch.Text) > 0 Or Len(txtft.Text) > 0 Then
            txtHtInCm.Enabled = False
            txtHtInInches.Enabled = False
        Else
            txtHtInCm.Enabled = True
            txtHtInInches.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
        End If
    End Sub

    Private Sub txtHtInInches_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHtInInches.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtHtInInches.Text, e)
        _HeightFlag = HtFlag.Inch
    End Sub

    Private Sub txtHtInInches_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHtInInches.KeyUp
        If Len(txtHtInInches.Text) > 0 Then
            txtHtInCm.Enabled = False
            txtft.Enabled = False
            txtInch.Enabled = False
        Else
            txtHtInCm.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
            txtHtInInches.Enabled = True
        End If
    End Sub
    Private Sub txtHtInInches_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHtInInches.TextChanged
        If _HeightFlag = HtFlag.Inch Then
            If txtHtInInches.Text <> "" Then
                txtHtInCm.Text = Format(Val(txtHtInInches.Text) * 2.54, "#0.00")
                ''Start :: Changed according to MU
                '' txtInch.Text = Format(Val(txtHtInInches.Text) Mod 12, "#0.00")
                txtInch.Text = Format(Convert.ToInt64(Val(txtHtInInches.Text) Mod 12), "#0.00")
                ''End :: Changed according to MU
                txtft.Text = Split(CType(Val(txtHtInInches.Text) / 12, String), ".", , CompareMethod.Text)(0)
            Else
                txtft.Clear()
                txtInch.Clear()
                txtHtInCm.Clear()
            End If
        End If
    End Sub
    Private Sub txtWtLbs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWtLbs.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWtLbs.Text, e)
        _WaightFlag = WeightIn.LbsOz
    End Sub

    Private Sub txtWtLbs_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWtLbs.KeyUp
        If Len(txtWtLbs.Text) > 0 Or Len(txtWtOz.Text) > 0 Then
            txtWeightlbs.Enabled = False
            txtWeightKg.Enabled = False
        Else
            txtWeightlbs.Enabled = True
            txtWeightKg.Enabled = True
            txtWtLbs.Enabled = True
            txtWtOz.Enabled = True
        End If
    End Sub
    Private Sub txtWtLbs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWtLbs.TextChanged
        If _WaightFlag = WeightIn.LbsOz Then
            If Val(txtWtLbs.Text) > 0 Or Val(txtWtOz.Text) > 0 Then
                txtWeightlbs.Text = Format(Val(txtWtLbs.Text) + Val(txtWtOz.Text) / 16, "#0.000")
                ''Start :: Calulation according to MU
                'txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45359237, "#0.000")
                txtWeightKg.Text = Format(Val(txtWeightlbs.Text) / 2.2033, "#0.00")
                ''End :: Calulation according to MU

                ' If txtWeightlbs.Tag <> 0 Then
                '     txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                'End If
            Else
                txtWeightlbs.Clear()
                txtWeightKg.Clear()
                'txtWeightChanged.Clear()
            End If
            Calc_WtKg_BMI()
        End If
    End Sub
    ''' <summary>
    ''' It is use to Calculate the BMI
    ''' </summary>


    Private Sub Calc_WtKg_BMI()


        Dim dHeight As Double
        If Val(txtWeightKg.Text) <> 0.0 Then
            ' txtWeightlbs.Text = Format(Val(txtWeightKg.Text) / 0.45, "#0.00")
            'dHeight = Val(mskHeight.Text) * Val(mskHeight.Text)
            dHeight = FtToMtr(Val(txtft.Text), Val(txtInch.Text))
            dHeight = dHeight * dHeight
            '            dHeight = Math.Round(dHeight, 2)
            If (Val(txtWeightKg.Text) > 0 AndAlso dHeight > 0) Then
                'txtBMI.Text = Format((Val(txtWeightKg.Text)) / (dHeight * dHeight), "#0.00")
                txtBMI.Text = Format(Val(txtWeightKg.Text) / dHeight, "#0.0")
            Else
                txtBMI.Text = ""
            End If
        Else
            txtBMI.Text = ""

        End If

    End Sub

    Private Function FtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
        Return (Ft * 30.48 + Inch * 2.54) / 100
        ''   1 ft = 30.48 cm
        ''   1 inch = 2.54 cm
    End Function
    Private Sub txtWtOz_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWtOz.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWtOz.Text, e)
        _WaightFlag = WeightIn.LbsOz
    End Sub

    Private Sub txtWtOz_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWtOz.KeyUp
        If Len(txtWtLbs.Text) > 0 Or Len(txtWtOz.Text) > 0 Then
            txtWeightlbs.Enabled = False
            txtWeightKg.Enabled = False
        Else
            txtWeightlbs.Enabled = True
            txtWeightKg.Enabled = True
            txtWtLbs.Enabled = True
            txtWtOz.Enabled = True
        End If
    End Sub

    Private Sub txtWtOz_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWtOz.TextChanged
        If _WaightFlag = WeightIn.LbsOz Then
            If Val(txtWtLbs.Text) > 0 Or Val(txtWtOz.Text) > 0 Then
                txtWeightlbs.Text = Format(Val(txtWtLbs.Text) + Val(txtWtOz.Text) / 16, "#0.000")
                ''Start :: Changeed According to MU
                ''txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45359237, "#0.000")
                txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.453, "#0.000")
                ''End :: Changeed According to MU
            Else
                txtWeightlbs.Clear()
                txtWeightKg.Clear()

            End If
            Calc_WtKg_BMI()
        End If
    End Sub


    Private Sub txtWtOz_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWtOz.Validating
        Try
            If Val(txtWtLbs.Text) <= 0 Then
                If Val(txtWtOz.Text) >= 16 Then
                    txtWtLbs.Text = Val(txtWtOz.Text) \ 16
                    txtWtOz.Text = Format(Val(txtWtOz.Text) Mod 16, "#0.00")
                    'Exit Sub
                End If
            Else
                If Val(txtWtOz.Text) >= 16 Then
                    MessageBox.Show("Invalid value of Oz", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWtOz.Focus()
                    Exit Sub
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtWeightlbs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightlbs.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWeightlbs.Text, e)
        _WaightFlag = WeightIn.Lbs
    End Sub


    Private Sub txtWeightlbs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeightlbs.TextChanged
        Try
            If _WaightFlag = WeightIn.Lbs Then

                If txtWeightlbs.Text <> "" Then
                    ''Start :: Change according to MU
                    ''txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45359237, "#0.000")
                    txtWeightKg.Text = Format(Val(txtWeightlbs.Text) / 2.2033, "#0.00")
                    ''End :: Change according to MU
                    txtWtLbs.Text = Decimal.Truncate((txtWeightlbs.Text))
                    Dim _decimalPlaces() As String = Split(txtWeightlbs.Text, ".", , CompareMethod.Text)
                    If _decimalPlaces.Length > 1 Then
                        txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                        If Val(txtWtOz.Text) = 0 Then
                            txtWtOz.Clear()
                        End If
                    Else
                        txtWtOz.Clear()
                    End If


                Else
                    txtWeightKg.Clear()

                    txtWtLbs.Clear()
                    txtWtOz.Clear()
                End If


                Calc_WtKg_BMI()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtWeightlbs_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWeightlbs.Validating

    End Sub
    Private Sub txtWeightlbs_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeightlbs.KeyUp
        Try
            If Len(txtWeightlbs.Text) > 0 Then
                txtWeightKg.Enabled = False
                txtWtLbs.Enabled = False
                txtWtOz.Enabled = False
            Else
                txtWeightlbs.Enabled = True
                txtWeightKg.Enabled = True
                txtWtLbs.Enabled = True
                txtWtOz.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtHtInCm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHtInCm.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtHtInCm.Text, e)
        _HeightFlag = HtFlag.Centimeter
    End Sub


    Private Sub txtHtInCm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHtInCm.KeyUp
        If Len(txtHtInCm.Text) > 0 Then
            txtHtInInches.Enabled = False
            txtft.Enabled = False
            txtInch.Enabled = False
        Else
            txtHtInInches.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
            txtHtInCm.Enabled = True
        End If
    End Sub

    Private Sub txtHtInCm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHtInCm.TextChanged
        If _HeightFlag = HtFlag.Centimeter Then
            If txtHtInCm.Text <> "" Then
                txtHtInInches.Text = Format(Val(txtHtInCm.Text) * 0.3937008, "#0.00")
                ''Start :: Changed according to MU
                ''txtInch.Text = Format(Val(txtHtInInches.Text) Mod 12, "#0.00")
                txtInch.Text = Format(Convert.ToInt64(Val(txtHtInInches.Text) Mod 12), "#0.00")
                ''End :: Changed according to MU
                txtft.Text = Split(CType(Val(txtHtInInches.Text) / 12, String), ".", , CompareMethod.Text)(0)

            Else
                txtft.Clear()
                txtInch.Clear()
                txtHtInInches.Clear()
            End If
        End If
    End Sub

    Private Sub txtWeightKg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightKg.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWeightKg.Text, e)
        _WaightFlag = WeightIn.Kg
    End Sub
    Private Sub txtWeightKg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeightKg.TextChanged
        Try
            If _WaightFlag = WeightIn.Kg Then
                If txtWeightKg.Text <> "" Then
                    ''Start :: Calulation according to MU
                    'txtWeightlbs.Text = Format(Val(txtWeightKg.Text) / 0.45359237, "#0.000")
                    txtWeightlbs.Text = Format(Val(txtWeightKg.Text) / 0.453, "#0.000")
                    ''End :: Calulation according to MU

                    txtWtLbs.Text = Decimal.Truncate(Val(txtWeightlbs.Text))
                    Dim _decimalPlaces() As String = Split(txtWeightlbs.Text, ".", , CompareMethod.Text)
                    If _decimalPlaces.Length > 1 Then
                        txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                    Else
                        txtWtOz.Clear()
                    End If

                    If txtWeightlbs.Tag <> 0 Then
                        'txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                    End If
                Else
                    txtWeightlbs.Clear()
                    ' txtWeightChanged.Clear()
                    txtWtLbs.Clear()
                    txtWtOz.Clear()
                End If

                txtWeightlbs.Text = (Val(txtWeightKg.Text) / 0.45359237)



                Calc_WtKg_BMI()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtWeightKg_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeightKg.KeyUp
        Try
            If Len(txtWeightKg.Text) > 0 Then
                txtWeightlbs.Enabled = False
                txtWtLbs.Enabled = False
                txtWtOz.Enabled = False
            Else
                txtWeightKg.Enabled = True
                txtWeightlbs.Enabled = True
                txtWtLbs.Enabled = True
                txtWtOz.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadVitalsODI(ByVal _ArrList As ArrayList)
        Try
            Dim i As Int16

            For i = 0 To _ArrList.Count - 1
                Dim lst As myList
                'lst = New myList
                lst = CType(_ArrList(i), myList)
                Dim oRadioButton As Control.ControlCollection = Nothing
                If lst.Code = 1 Then
                    oRadioButton = pnlSection1.Controls
                ElseIf lst.Code = 2 Then
                    oRadioButton = pnlSection2.Controls
                ElseIf lst.Code = 3 Then
                    oRadioButton = pnlSection3.Controls
                ElseIf lst.Code = 4 Then
                    oRadioButton = pnlSection4.Controls
                ElseIf lst.Code = 5 Then
                    oRadioButton = pnlSection5.Controls
                ElseIf lst.Code = 6 Then
                    oRadioButton = pnlSection6.Controls
                ElseIf lst.Code = 7 Then
                    oRadioButton = pnlSection7.Controls
                ElseIf lst.Code = 8 Then
                    oRadioButton = pnlSection8.Controls
                ElseIf lst.Code = 9 Then
                    oRadioButton = pnlSection9.Controls
                ElseIf lst.Code = 10 Then
                    oRadioButton = pnlSection10.Controls
                End If
                Dim oRad As RadioButton
                For Each oRadioButton1 As Control In oRadioButton
                    If TypeOf oRadioButton1 Is RadioButton Then
                        If lst.Description = oRadioButton1.Tag Then

                            oRad = CType(oRadioButton1, RadioButton)
                            oRad.Checked = True
                            Exit For
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmCalculate_BMI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'It assigns  Values From frmPatientVitals to these Form 
        Try
            ''Added by Mayuri:20110105-To Retrieve values from Vitals FORM
            Try
                If Me.Text = "Calculate ODI" Then
                    'Me.Text = "Calculate ODI"
                    gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
                End If
            Catch ex As Exception

            End Try
            If _isformLoad = True Then

                LoadVitalsODIFromdt()

            Else

            If IsNothing(frmPatientVitals.Arrlist) = False Then
                If frmPatientVitals.Arrlist.Count > 0 Then
                        'If _strtxtODIFromVitals = strtxtODI Then
                        LoadVitalsODI(frmPatientVitals.Arrlist)
                        'End If
                    Else
                        LoadVitalsODIFromdt()
                    End If


            End If
            End If
            If _strtxtODIFromVitals <> txtODIPercent.Text.Trim() Then
                FindButtons()
                txtODIPercent.Text = ""
            End If
            
            _isformLoad = False
            ' End If

            Me.TopMost = False
            txtft.Text = frmPatientVitals.strtxtft
        Catch ex As Exception
            txtft.Text = "0"
        End Try

        Try
            txtInch.Text = frmPatientVitals.strtxtinch
        Catch ex As Exception
            txtInch.Text = "0"
        End Try

        Try

            txtWtOz.Text = frmPatientVitals.strtxtwtoz
        Catch ex As Exception
            txtWtOz.Text = "0"
        End Try

        Try

            txtWtLbs.Text = frmPatientVitals.strtxtwtlbs
        Catch ex As Exception
            txtWtLbs.Text = "0"
        End Try


        Try

            txtWeightlbs.Text = frmPatientVitals.strtxtwghtlbs
        Catch ex As Exception
            txtWeightlbs.Text = "0"
        End Try

        Try

            txtWeightKg.Text = frmPatientVitals.strtxtwghtkg
        Catch ex As Exception
            txtWeightKg.Text = "0"
        End Try

        Try

            txtBMI.Text = frmPatientVitals.strtxtBMI
        Catch ex As Exception
            txtBMI.Text = "0"

        End Try
        Try

        Catch ex As Exception

        End Try




    End Sub
    ''Added on 20101231
    Public Sub LoadVitalsODIFromdt()

        Dim i As Int16
        If _VitalID > 0 Then
            Dim objpatientvitals As New clsPatientVitals
            Dim dt As DataTable
            dt = objpatientvitals.GetVitalsODI(_VitalID)
            objpatientvitals.Dispose()
            objpatientvitals = Nothing
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        '   Dim _control As Control
                        'Dim oRadioButton As _control.ControlCollection
                        Dim oRadioButton As Control.ControlCollection = Nothing
                        If dt.Rows(i)("sSectionName") = 1 Then
                            oRadioButton = pnlSection1.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 2 Then
                            oRadioButton = pnlSection2.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 3 Then
                            oRadioButton = pnlSection3.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 4 Then
                            oRadioButton = pnlSection4.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 5 Then
                            oRadioButton = pnlSection5.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 6 Then
                            oRadioButton = pnlSection6.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 7 Then
                            oRadioButton = pnlSection7.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 8 Then
                            oRadioButton = pnlSection8.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 9 Then
                            oRadioButton = pnlSection9.Controls
                        ElseIf dt.Rows(i)("sSectionName") = 10 Then
                            oRadioButton = pnlSection10.Controls

                        End If
                        Dim oRad As RadioButton
                        For Each oRadioButton1 As Control In oRadioButton
                            If TypeOf oRadioButton1 Is RadioButton Then
                                If dt.Rows(i)("sSectionValue") = oRadioButton1.Tag Then

                                    oRad = CType(oRadioButton1, RadioButton)
                                    oRad.Checked = True

                                    Exit For
                                End If
                            End If
                        Next
                    Next
                End If
                dt.Dispose()
                dt = Nothing
            End If
        End If

        'Return True
    End Sub


    ''End
    ''' <summary>
    ''' It is use to clear all values if any exception comes while clearing then it is set to 0
    ''' </summary>
    
    Private Sub Reset()
        Try
            txtft.Text = ""
        Catch ex As Exception
            txtft.Text = "0"
        End Try

        Try
            txtInch.Text = ""
        Catch ex As Exception
            txtInch.Text = "0"
        End Try

        Try

            txtWtOz.Text = ""
        Catch ex As Exception
            txtWtOz.Text = "0"
        End Try

        Try
            txtWtLbs.Text = ""

            txtWeightlbs.Text = ""

            txtWeightKg.Text = ""
        Catch ex As Exception
            txtWtLbs.Text = "0"
            txtWeightlbs.Text = "0"
            txtWeightKg.Text = "0"

        End Try
    End Sub

    Private Sub tblStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                'If _IsSavenClose = True Then
                'Else
                _IsCancel = True
                'End If


                Me.Close()
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Case "Reset"
                Reset()
            Case "Save"
                _IsSavenClose = True
                _ArrList = SaveVitalsODIInArray()
               
                frmPatientVitals.Arrlist = _ArrList
                strtxtODI = frmPatientVitals.ODIPercentCalculation()
                'txtODIPercent.Text = ODIPercentCalculation()
            Case "SavenClose"
                _IsSavenClose = True
                _ArrList = SaveVitalsODIInArray()
                frmPatientVitals.Arrlist = _ArrList
                strtxtODI = frmPatientVitals.ODIPercentCalculation()
                Me.Close()
        End Select
    End Sub
    Private Function ODIPercentCalculation() As String
        Try
            Dim j As Int16
            Dim k As Int16
            Dim i As Int16
            Dim Str As String
            For i = 0 To _ArrList.Count - 1
                Dim lst As myList = _ArrList(i)
                With lst
                    j = j + Convert.ToInt16(lst.Description)
                End With
            Next
            k = j * 2
            Str = Convert.ToString(k)
            Return Str
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End Try
    End Function
    ''Added on 20101104
    Public Function SaveVitalsODIInArray() As ArrayList
        Try
            _ArrList.Clear()
            Dim oRadioButton As Control.ControlCollection
            Dim oPanel As Control.ControlCollection
            oPanel = pnlQuestionaire.Controls
            Dim opnl As Panel
            For Each opanel1 As Control In oPanel
                If TypeOf opanel1 Is Panel Then
                    opnl = CType(opanel1, Panel)
                    If opnl.Name = "pnlSection1" Then
                        _SectionName = SectionName.Section1
                        oRadioButton = pnlSection1.Controls
                        GetRadioButtons(oRadioButton)
                    ElseIf opnl.Name = "pnlSection2" Then
                        _SectionName = SectionName.Section2
                        oRadioButton = pnlSection2.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection3" Then
                        _SectionName = SectionName.Section3
                        oRadioButton = pnlSection3.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection4" Then
                        _SectionName = SectionName.Section4
                        oRadioButton = pnlSection4.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection5" Then
                        _SectionName = SectionName.Section5
                        oRadioButton = pnlSection5.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection6" Then
                        _SectionName = SectionName.Section6
                        oRadioButton = pnlSection6.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection7" Then
                        _SectionName = SectionName.Section7
                        oRadioButton = pnlSection7.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection8" Then
                        _SectionName = SectionName.Section8
                        oRadioButton = pnlSection8.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection9" Then
                        _SectionName = SectionName.Section9
                        oRadioButton = pnlSection9.Controls
                        GetRadioButtons(oRadioButton)

                    ElseIf opnl.Name = "pnlSection10" Then
                        _SectionName = SectionName.Section10
                        oRadioButton = pnlSection10.Controls
                        GetRadioButtons(oRadioButton)

                    End If
                End If
            Next
            Return _ArrList
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Finally

        End Try
        ''End
    End Function


    Public Sub GetRadioButtons(ByVal oRadioButton As Control.ControlCollection)
        Try

            Dim oRad As RadioButton
            For Each oRadioButton1 As Control In oRadioButton
                If TypeOf oRadioButton1 Is RadioButton Then
                    oRad = CType(oRadioButton1, RadioButton)
                    If oRad.Checked = True Then
                        _SectionValue = oRad.Tag
                        Dim lst As New myList
                        With lst
                            .Code = _SectionName
                            .Description = _SectionValue
                            ''.HistoryItem = txtODIPercent.Text
                        End With
                        If IsNothing(lst.Code) = False OrElse IsNothing(lst.Description) = False Then
                            _ArrList.Add(lst)
                        End If
                        Exit For
                    End If
                End If


                ''Exit For
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub FindButtons()
        Dim oRadioButton As Control.ControlCollection
        Dim oPanel As Control.ControlCollection
        oPanel = pnlQuestionaire.Controls
        Dim opnl As Panel
        For Each opanel1 As Control In oPanel
            If TypeOf opanel1 Is Panel Then
                opnl = CType(opanel1, Panel)
                If opnl.Name = "pnlSection1" Then
                    oRadioButton = pnlSection1.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection2" Then
                    oRadioButton = pnlSection2.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection3" Then
                    oRadioButton = pnlSection3.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection4" Then
                    oRadioButton = pnlSection4.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection5" Then
                    oRadioButton = pnlSection5.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection6" Then
                    oRadioButton = pnlSection6.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection7" Then
                    oRadioButton = pnlSection7.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection8" Then
                    oRadioButton = pnlSection8.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection9" Then
                    oRadioButton = pnlSection9.Controls
                    GetButtons(oRadioButton)
                ElseIf opnl.Name = "pnlSection10" Then
                    oRadioButton = pnlSection10.Controls
                    GetButtons(oRadioButton)


                End If
            End If
        Next
    End Sub
    Public Sub GetButtons(ByVal oRadioButton As Control.ControlCollection)
        Try

            Dim oRad As RadioButton
            For Each oRadioButton1 As Control In oRadioButton
                If TypeOf oRadioButton1 Is RadioButton Then
                    oRad = CType(oRadioButton1, RadioButton)
                    If oRad.Checked = True Then
                        oRad.Checked = False
                    End If
                End If


                ''Exit For
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub txtBMI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMI.KeyPress
        AllowDecimal(txtBMI.Text, e)
    End Sub

    

    Public Sub New(ByVal nVitalID As Long, ByVal nVisitID As Long, ByVal PatientID As Long, ByVal isformLoad As Boolean, ByVal strtxtODIFromVitals As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientID = PatientID
        _VisitID = nVisitID
        _VitalID = nVitalID
        _isformLoad = isformLoad
        _strtxtODIFromVitals = strtxtODIFromVitals
       
        ''_dtVitalDate = dtVitalDate
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub SetHeightOnResolution()
        Dim Srn As Screen = Screen.PrimaryScreen
        Dim tempHeight, tempWidth As Integer
        '    Dim FixHeight, FixWidth As Integer
        tempHeight = Srn.Bounds.Height
        tempWidth = Srn.Bounds.Width

        If tempHeight >= 1024 Then
            If Me.Height > 970 Then
                Me.Height = 970
            End If
            Me.StartPosition = FormStartPosition.CenterScreen
        Else
            If Me.Height > 720 Then
                Me.Height = 720
            End If
            Me.StartPosition = FormStartPosition.CenterScreen
        End If

    End Sub
    
    Private Sub frmCalculate_BMI_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        SetHeightOnResolution()
    End Sub
End Class