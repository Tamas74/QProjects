Imports gloEMRGeneralLibrary.gloEMRPrescription

Public Class frmDosageCalculator

    Dim _nRxPatientid As Long

    Dim _ErrorMessage As String = "gloEMR"

    Dim _sPatWeight As String = ""
    Dim _strPatWeightUnit As String = ""
    Dim _strPrescribedDose As String = ""
    Dim _CalculatedDosage As String = ""

    Dim _strDrugDosageForm As String = ""

    Dim strConcatUnit As String = "" 'this is only use to claculate the the unit if it is mg/kg type


    ''for CCHIT11
    Dim strDosageFrequencyText As String = "" 'this is only use to show dosage frequency text value selected from custom prescription
    Dim nDosageFrequencyValue As Int32 'this is only use to show dosage frequency value selected from custom prescription
    Public Property Frequency() As String
        Get
            Return _CalculatedDosage
        End Get
        Set(ByVal Value As String)
            _CalculatedDosage = Value
        End Set
    End Property

    Private _btnClick As Int16

    Public Event SaveDosage(ByVal strDosageVal As String)



    Private Sub txtWeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeight.KeyPress
        Try

            Dim chkNumeric As String = txtWeight.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid Numeric or Decimal value", _ErrorMessage, MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmDosageCalculator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim _RXBusinessLayer As RxBusinesslayer = New RxBusinesslayer(_nRxPatientid)

        Dim dtDosageFreq As DataTable = Nothing

        Try
            dtDosageFreq = _RXBusinessLayer.getDosageFreqValue(txtDosageFrequency.Text)

            If Not IsNothing(dtDosageFreq) Then
                If dtDosageFreq.Rows.Count > 0 Then
                    nDosageFrequencyValue = dtDosageFreq.Rows(0)("AbbrValue")
                End If
            End If

            'for the dosage information like 125 MG/5ML. then DoseageAvailable = 125 and Volume = 5.
            Dim strDose() As String = Nothing
            Dim strNumDig As String = ""
            If _strPrescribedDose <> "" Then
                strDose = _strPrescribedDose.Split("/")

                If strDose.Length > 0 Then
                    Dim strDoseAvailable As String = strDose(0)

                    strNumDig = strDoseAvailable.Replace(" ", "")
                    Dim strbldDoseAvlb As New System.Text.StringBuilder
                    Dim strbldDoseAvlbUnit As New System.Text.StringBuilder
                    For i As Int16 = 0 To strNumDig.Length - 1
                        If IsNumeric(strNumDig(i)) Or strNumDig(i) = "." Then
                            strbldDoseAvlb.Append(strNumDig(i))
                        Else
                            strbldDoseAvlbUnit.Append(strNumDig(i))
                        End If
                    Next
                    txtDoseavailable.Text = strbldDoseAvlb.ToString
                    cmbDosageavailableUnit.SelectedItem = strbldDoseAvlbUnit.ToString

                    strNumDig = ""

                    Dim strDoseVolume As String = ""
                    If strDose.Length > 1 Then
                        strDoseVolume = strDose(1)
                    Else
                        strDoseVolume = strDose(0)
                    End If

                    strNumDig = strDoseVolume.Replace(" ", "")
                    Dim strbldDoseVolume As New System.Text.StringBuilder
                    Dim strbldDoseVolumeUnit As New System.Text.StringBuilder
                    For i As Int16 = 0 To strNumDig.Length - 1
                        If IsNumeric(strNumDig(i)) Or strNumDig(i) = "." Then
                            strbldDoseVolume.Append(strNumDig(i))
                        Else
                            strbldDoseVolumeUnit.Append(strNumDig(i))
                        End If
                    Next
                    txtDosageVolume.Text = strbldDoseVolume.ToString
                    cmbVolumeUnit.SelectedItem = strbldDoseVolumeUnit.ToString
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(_RXBusinessLayer) Then
                _RXBusinessLayer.Dispose()
                _RXBusinessLayer = Nothing
            End If
            If Not IsNothing(dtDosageFreq) Then
                dtDosageFreq.Dispose()
                dtDosageFreq = Nothing
            End If
        End Try
    End Sub

    Public Sub New(ByVal _weight As String, ByVal _strWtUnit As String, ByVal _strDosage As String, ByVal _strDosageForm As String, ByVal DosageFrequencyText As String, ByVal DosageFrequencyValue As Int32, ByVal RxPatientid As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _nRxPatientid = RxPatientid
        ' Add any initialization after the InitializeComponent() call.

        _strPatWeightUnit = _strWtUnit
        _strPrescribedDose = _strDosage
        _strDrugDosageForm = _strDosageForm

        ''for CCHIT11
        txtDosageFrequency.Text = DosageFrequencyText
        strDosageFrequencyText = DosageFrequencyText
        If DosageFrequencyValue <> 0 Then
            nDosageFrequencyValue = DosageFrequencyValue
        Else
            nDosageFrequencyValue = 1
        End If

        txtDrugForm.Text = _strDrugDosageForm

        cmbWeightunit.Items.Add("lbs")
        cmbWeightunit.Items.Add("kg")
        cmbWeightunit.SelectedIndex = 1


        cmbDosageavailableUnit.SelectedIndex = 0
        cmbDosageOrderedUnit.SelectedIndex = 0


        ''''''''this code was written for requirement given on 10 feb 2009 tuesday. requirement is as follows
        '''''when the given drug item has drug for as syrup/solution/liquid then the weight
        'text box is enable and the dosage is calculated with foll formula
        'Dosage = ((_DosageOrdered * _weight) / _DosageAvailable) * _DosageVolume
        'if the doseform is of type capsule/table or etc then the weight,dosevolumne
        'text box and weightunit,volumeunit combo box disabled and the dosage is
        'calculated with foll formula        Dosage = _DosageOrdered / _DosageAvailable


        If _strDrugDosageForm <> "" Then


            If _strDrugDosageForm.Contains("Capsule") Then
                _strDrugDosageForm = "Capsule"
            ElseIf _strDrugDosageForm.Contains("Tablet") Then
                _strDrugDosageForm = "Tablet"
            ElseIf _strDrugDosageForm.Contains("Syrup") Or _strDrugDosageForm.Contains("Solution") Or _strDrugDosageForm.Contains("liquid") Then
                _strDrugDosageForm = "Liquid"
            End If



            If _strDrugDosageForm = "Tablet" Or _strDrugDosageForm = "Capsule" Then
                txtWeight.Enabled = False
                txtDosageVolume.Enabled = False
                cmbWeightunit.Enabled = False
                cmbVolumeUnit.Enabled = False

            Else
                If _strPatWeightUnit <> "kg" Then
                    _sPatWeight = _weight / 2.2

                    'check weight = 0 that means there is no weight entered in the Vitals table, therefore bydefault keep the weight unit value = 'lb'
                    If _sPatWeight <> 0 Then
                        txtWeight.Text = _sPatWeight
                        cmbWeightunit.SelectedItem = _strPatWeightUnit
                    Else
                        cmbWeightunit.SelectedText = "lbs"
                    End If
                Else
                    txtWeight.Text = _weight
                    cmbWeightunit.SelectedItem = _strPatWeightUnit
                End If
            End If

        End If






    End Sub


    ''convert the calculated dosage to quarters.
    '1 - 1.25 = 1.25
    '1.26 - 1.50= 1.50
    '1.51 - 1.75 = 1.75
    '1.75 - 2 = 2
    Private Function ConvertToQuarter(ByVal _CalulatedDosage As Double) As Double
        Try
            Dim x As Double = _CalulatedDosage
            Dim y, z, w As Integer


            y = x * 100

            'Calculate mod
            w = y Mod 100


            If w < 50 Then
                z = y / 100
            ElseIf w = 50 Then
                z = (y / 100)
            Else
                z = (y / 100)
                z -= 1
            End If

            'compare w's value
            If w < 25 Then
                w = 25
            ElseIf w > 25 And w < 50 Then
                w = 50
            ElseIf w > 50 And w < 75 Then
                w = 75
            ElseIf w > 75 And w < 100 Then
                w = 100
            End If

            'Calculate results
            x = z * 100 + w
            x = x / 100

            'Display result
            Return x
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return 0.0
        End Try
    End Function




    Private Sub tlbbtnCalculateDosage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnCalculateDosage.Click
        Dim _Weight As Int16
        Dim _DosageOrdered As Int32
        Dim _DosageAvailable As Int32
        Dim _DosageVolume As Int32

        Try

            If _strDrugDosageForm <> "" Then
                If _strDrugDosageForm = "Tablet" Or _strDrugDosageForm = "Capsule" Then

                    If txtDosageOrdered.Text <> "" Then
                        _DosageOrdered = txtDosageOrdered.Text
                    Else
                        MessageBox.Show("Please enter the Dosage order for the patient", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    If Not IsNothing(cmbDosageOrderedUnit.SelectedItem) Then
                        'dont know what code to add
                    Else
                        MessageBox.Show("Please select the appropriate unit for Dosage to be ordered for the patient", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If


                    If txtDoseavailable.Text <> "" Then
                        _DosageAvailable = txtDoseavailable.Text
                    Else
                        MessageBox.Show("Please enter the available dosage quantity", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If


                    Dim _CalulatedDosage As Double = _DosageOrdered / _DosageAvailable

                    'calculate the dosage for Table/Capsule in Quarters
                    _CalulatedDosage = ConvertToQuarter(_CalulatedDosage)

                    _CalulatedDosage = _CalulatedDosage / nDosageFrequencyValue
                    ' _CalulatedDosage = _CalulatedDosage
                    txtCalulatedDosage.Text = _CalulatedDosage & " " & cmbVolumeUnit.SelectedItem & " " & _strDrugDosageForm

                Else 'for liquid type drugs
                    '--------validation for weight textbox and combobox
                    If txtWeight.Text <> "" Then
                        If cmbWeightunit.SelectedItem = "lbs" Then
                            'convert the weight from lbs to kg by divide the value by 2.2
                            Dim _wt As Int16 = CType(Val(txtWeight.Text), Int16)
                            _Weight = _wt / 2.2

                        Else 'the value selected is in kg
                            _Weight = CType(Val(txtWeight.Text), Int16)
                        End If
                    Else
                        MessageBox.Show("Please enter the weight of the patient", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    '--------validation for weight textbox and combobox

                    '_Weight = Format(_Weight, "#0.00") 'for having the precision upto 2 decimal places



                    '--------validation for Dosage ordered textbox and related comboboxes
                    If txtDosageOrdered.Text <> "" Then
                        _DosageOrdered = txtDosageOrdered.Text
                    Else
                        MessageBox.Show("Please enter the Dosage order for the patient", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    If Not IsNothing(cmbDosageOrderedUnit.SelectedItem) Then
                        'dont know what code to add
                    Else
                        MessageBox.Show("Please select the appropriate unit for Dosage to be ordered for the patient", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    '--------validation for Dosage ordered textbox and related comboboxes



                    If txtDoseavailable.Text <> "" Then
                        _DosageAvailable = txtDoseavailable.Text
                    Else
                        MessageBox.Show("Please enter the available dosage quantity", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If


                    If txtDosageVolume.Text <> "" Then
                        _DosageVolume = txtDosageVolume.Text
                    Else
                        MessageBox.Show("Please enter the dosage volume", _ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If


                    Dim _CalulatedDosage As Int32 = ((_DosageOrdered * _Weight) / _DosageAvailable) * _DosageVolume
                    _CalulatedDosage = _CalulatedDosage 'Format(_CalulatedDosage, "#0.00")

                    _CalulatedDosage = _CalulatedDosage / nDosageFrequencyValue
                    txtCalulatedDosage.Text = _CalulatedDosage & " " & cmbVolumeUnit.SelectedItem ' " ml"
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tlbbtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnOk.Click

        '' Dim strCalculatedDosage As String = ""

        'save the calculated dosage value to the custom prescription Frequency text box control
        Try
            If txtCalulatedDosage.Text.Trim <> "" Or txtTimes.Text <> "" Or cmbDysWksMnths.Text <> "" Then
                _CalculatedDosage = txtCalulatedDosage.Text.Trim & " " & txtTimes.Text & " " & cmbDysWksMnths.Text
            Else
                _CalculatedDosage = ""
            End If

            RaiseEvent SaveDosage(_CalculatedDosage.Trim)
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub



    Private Sub txtWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeight.TextChanged
        Try
            CalculateTotalDoseOrdered()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDosageOrdered_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDosageOrdered.TextChanged
        Try
            CalculateTotalDoseOrdered()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbDosageOrderedUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDosageOrderedUnit.SelectedIndexChanged
        Try
            CalculateTotalDoseOrdered()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalculateTotalDoseOrdered()
        Try
            If txtWeight.Text.Trim <> "" And txtDosageOrdered.Text.Trim <> "" Then


                SplitDoseOrdUnit()

                txtTotalDoseOrdered.Text = txtWeight.Text * txtDosageOrdered.Text & " " & strConcatUnit

            Else
                txtTotalDoseOrdered.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub



    'split the unit part if it in the format mg/kg
    Private Sub SplitDoseOrdUnit()
        Try
            strConcatUnit = ""
            Dim strUnit As String() = Split(cmbDosageOrderedUnit.Text, "/")
            If strUnit.Length > 0 Then
                strConcatUnit = strUnit(0) & "/day"
            Else
                strConcatUnit = cmbDosageOrderedUnit.Text & "/day"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtDosageOrdered_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDosageOrdered.KeyPress
        Try

            Dim chkNumeric As String = txtDosageOrdered.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid Numeric or Decimal value", _ErrorMessage, MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtDoseavailable_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseavailable.KeyPress
        Try

            Dim chkNumeric As String = txtDoseavailable.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid Numeric or Decimal value", _ErrorMessage, MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtDosageVolume_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDosageVolume.KeyPress
        Try

            Dim chkNumeric As String = txtDosageVolume.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid Numeric or Decimal value", _ErrorMessage, MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
End Class
