Imports System.Resources
Imports System.Reflection
Imports System.Drawing.Imaging


Public Class frmDASCalculator

    Private _VitalID As Int64 = 0
    Private _PatientId As Int64 = 0
    Private _DASID As Int64 = 0
    Public DlogResult As Boolean = False
    Private IsChange As Boolean = False
    Public IsmakeasCurrent As Boolean = False
    Private IsLabValue = False


    Public oclsDAS As clsDAS = Nothing

    Public Sub New(ByVal Vital As Int64, ByVal PatientID As Int64)
        _VitalID = Vital
        _PatientId = PatientID
        InitializeComponent()
    End Sub

    Private Sub frmDASCalculator_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsChange = True Then
            Dim dresult As DialogResult
            dresult = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If dresult = Windows.Forms.DialogResult.No Then
                e.Cancel = False
            End If
            If dresult = Windows.Forms.DialogResult.Yes Then
                SaveDAS()
                IsChange = False
            End If
            If dresult = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub frmDASCalculator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not IsNothing(oclsDAS) Then
                FillDAS(oclsDAS)
            Else
                FillDAS(_VitalID)
            End If
            If IsmakeasCurrent Then
                If Not IsNothing(oclsDAS) Then
                    oclsDAS.nVitalID = 0
                    oclsDAS.nDASID = 0
                End If

                _DASID = 0
                _VitalID = 0
            End If
            IsChange = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub TenderCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_T_R_Shoulder.CheckedChanged, chk_T_R_RingDown.CheckedChanged, chk_T_R_LittleDown.CheckedChanged, chk_T_R_Wrist.CheckedChanged, chk_T_R_ThunbUp.CheckedChanged, chk_T_R_ThunbDown.CheckedChanged, chk_T_R_RingUp.CheckedChanged, chk_T_R_MiddleUp.CheckedChanged, chk_T_R_MiddleDown.CheckedChanged, chk_T_R_LittleUp.CheckedChanged, chk_T_R_Knee.CheckedChanged, chk_T_R_IndexUp.CheckedChanged, chk_T_R_IndexDown.CheckedChanged, chk_T_R_Elbow.CheckedChanged, chk_T_L_Wrist.CheckedChanged, chk_T_L_ThunbUp.CheckedChanged, chk_T_L_ThunbDown.CheckedChanged, chk_T_L_Shoulder.CheckedChanged, chk_T_L_RingUp.CheckedChanged, chk_T_L_RingDown.CheckedChanged, chk_T_L_MiddleUp.CheckedChanged, chk_T_L_MiddleDown.CheckedChanged, chk_T_L_LittleUp.CheckedChanged, chk_T_L_LittleDown.CheckedChanged, chk_T_L_Knee.CheckedChanged, chk_T_L_IndexUp.CheckedChanged, chk_T_L_IndexDown.CheckedChanged, chk_T_L_Elbow.CheckedChanged
        Try

            Dim objchk As CheckBox
            objchk = DirectCast(sender, CheckBox)

            If objchk.Checked Then
                If txtTenderCount.Text = "" Then
                    txtTenderCount.Text = 1
                Else
                    txtTenderCount.Text = Convert.ToInt64(txtTenderCount.Text) + 1
                End If
                objchk.BackgroundImage = gloEMR.My.Resources.DASBg1
                objchk.ImageAlign = ContentAlignment.MiddleCenter
                objchk.BackgroundImageLayout = ImageLayout.Stretch
            Else
                If txtTenderCount.Text = "" Then
                    txtTenderCount.Text = 0
                Else
                    txtTenderCount.Text = Convert.ToInt64(txtTenderCount.Text) - 1
                End If
                objchk.BackgroundImage = gloEMR.My.Resources.DASBg2
                objchk.ImageAlign = ContentAlignment.MiddleCenter
                objchk.BackgroundImageLayout = ImageLayout.Stretch
            End If
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SwollenCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_S_R_Shoulder.CheckedChanged, chk_S_R_LittleDown.CheckedChanged, chk_S_R_RingDown.CheckedChanged, chk_S_R_MiddleDown.CheckedChanged, chk_S_R_IndexDown.CheckedChanged, chk_S_L_MiddleUp.CheckedChanged, chk_S_L_IndexUp.CheckedChanged, chk_S_R_Wrist.CheckedChanged, chk_S_R_ThunbUp.CheckedChanged, chk_S_R_ThunbDown.CheckedChanged, chk_S_R_RingUp.CheckedChanged, chk_S_R_MiddleUp.CheckedChanged, chk_S_R_LittleUp.CheckedChanged, chk_S_R_IndexUp.CheckedChanged, chk_S_R_Elbow.CheckedChanged, chk_S_L_Wrist.CheckedChanged, chk_S_L_ThunbUp.CheckedChanged, chk_S_L_ThunbDown.CheckedChanged, chk_S_L_Shoulder.CheckedChanged, chk_S_L_MiddleDown.CheckedChanged, chk_S_L_IndexDown.CheckedChanged, chk_S_L_Elbow.CheckedChanged, chk_S_R_Knee.CheckedChanged, chk_S_L_RingUp.CheckedChanged, chk_S_L_RingDown1.CheckedChanged, chk_S_L_LittleUp.CheckedChanged, chk_S_L_LittleDown.CheckedChanged, chk_S_L_Knee.CheckedChanged
        Try

            Dim objchk As CheckBox
            objchk = DirectCast(sender, CheckBox)

            If objchk.Checked Then
                If txtSwollenCount.Text = "" Then
                    txtSwollenCount.Text = 1
                Else
                    txtSwollenCount.Text = Convert.ToInt64(txtSwollenCount.Text) + 1
                End If
                objchk.BackgroundImage = gloEMR.My.Resources.DASBg1_Swollwn
                objchk.ImageAlign = ContentAlignment.MiddleCenter
                objchk.BackgroundImageLayout = ImageLayout.Stretch
            Else
                If txtSwollenCount.Text = "" Then
                    txtSwollenCount.Text = 0
                Else
                    txtSwollenCount.Text = Convert.ToInt64(txtSwollenCount.Text) - 1
                End If
                objchk.BackgroundImage = gloEMR.My.Resources.DASBg2_Swollen
                objchk.ImageAlign = ContentAlignment.MiddleCenter
                objchk.BackgroundImageLayout = ImageLayout.Stretch
            End If
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Check_Or_UncheckTender(ByVal check As Boolean)
        Try
            Dim ocntl, opnlmain, opnlImage As Control
            For Each opnlmain In Me.Controls
                If opnlmain.Name = "pnlMain" Then
                    For Each opnlImage In opnlmain.Controls
                        If opnlImage.Name = "pnlImages" Then
                            For Each ocntl In opnlImage.Controls
                                If (TypeOf ocntl Is CheckBox) Then
                                    If ocntl.Name.Contains("chk_T_") Then
                                        DirectCast(ocntl, CheckBox).Checked = check
                                        If check Then
                                            DirectCast(ocntl, CheckBox).BackColor = Color.Red
                                        Else
                                            ocntl.BackColor = Color.FromArgb(207, 224, 248)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next

                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Sub Check_Or_UncheckSwollen(ByVal check As Boolean)
        Try
            Dim ocntl, opnlmain, opnlImage As Control
            For Each opnlmain In Me.Controls
                If opnlmain.Name = "pnlMain" Then
                    For Each opnlImage In opnlmain.Controls
                        If opnlImage.Name = "pnlImages" Then
                            For Each ocntl In opnlImage.Controls
                                If (TypeOf ocntl Is CheckBox) Then
                                    If ocntl.Name.Contains("chk_S_") Then
                                        DirectCast(ocntl, CheckBox).Checked = check
                                        If check Then
                                            DirectCast(ocntl, CheckBox).BackColor = Color.Blue
                                        Else
                                            ocntl.BackColor = Color.FromArgb(207, 224, 248)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub rbtnCRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnCRP.CheckedChanged
        Try

            If rbtnCRP.Checked Then
                txtCRP.Text = "0"
                txtCRP.Enabled = True
                txtESR.Text = ""
                txtESR.Enabled = False
            Else
                txtCRP.Text = ""
                txtCRP.Enabled = False
                txtESR.Text = "1"
                txtESR.Enabled = True
            End If
            FormulaText()
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub rbtnESR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnESR.CheckedChanged
        Try
            If rbtnESR.Checked Then
                txtESR.Text = "1"
                txtESR.Enabled = True
                txtCRP.Text = ""
                txtCRP.Enabled = False
            Else
                txtESR.Text = ""
                txtESR.Enabled = False
                txtCRP.Text = "0"
                txtCRP.Enabled = True
            End If
            FormulaText()
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkPainScale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPainScale.CheckedChanged
        Try
            If chkPainScale.Checked Then
                pnlPainScale.Visible = True
                txtPainScore.Visible = True
            Else
                pnlPainScale.Visible = False
                txtPainScore.Visible = False
                txtPainScore.Text = "0"
                trbPainScale.Value = 0
            End If
            FormulaText()
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub rbtnUseDiagram_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnUseDiagram.CheckedChanged
        Try
            If rbtnUseDiagram.Checked Then
                pnlHide.Visible = False
                txtTenderCount.Enabled = False
                txtSwollenCount.Enabled = False

                lblSwollenName.Visible = True
                lblTenderName.Visible = True
            Else
                pnlHide.Visible = True
                txtTenderCount.Enabled = True
                txtSwollenCount.Enabled = True

                lblSwollenName.Visible = False
                lblTenderName.Visible = False
            End If
            Check_Or_UncheckTender(False)
            Check_Or_UncheckSwollen(False)
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub rbtnJointCount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnJointCount.CheckedChanged
        Try
            If rbtnJointCount.Checked Then
                pnlHide.Visible = True
                pnlHide.BringToFront()
                txtTenderCount.Enabled = True
                txtSwollenCount.Enabled = True
                lblSwollenName.Visible = True
                lblTenderName.Visible = True
            Else
                pnlHide.Visible = False

                txtTenderCount.Enabled = False
                txtSwollenCount.Enabled = False
                lblSwollenName.Visible = False
                lblTenderName.Visible = False
            End If
            txtTenderCount.Text = "0"
            txtSwollenCount.Text = "0"
            Check_Or_UncheckTender(False)
            Check_Or_UncheckSwollen(False)
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormulaText()

        Try
            If rbtnESR.Checked Then
                'With ESR
                If chkPainScale.Checked Then
                    'With pain scale
                    lblCalculationName.Text = "using ESR with pain score"
                    lblCalculationFormula.Text = "0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.70*ln(ESR) + 0.014*PS"
                Else
                    'without pain scale
                    lblCalculationName.Text = "using ESR without pain score"
                    lblCalculationFormula.Text = "(0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.70*ln(ESR))*1.08 + 0.16"
                End If
            Else
                'With CRP
                If chkPainScale.Checked Then
                    'With pain scale
                    lblCalculationName.Text = "using CRP with pain score"
                    lblCalculationFormula.Text = "0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.36*ln(CRP+1) + 0.014*PS +0.96"
                Else
                    'without pain scale
                    lblCalculationName.Text = "using CRP without pain score"
                    lblCalculationFormula.Text = "(0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.36*ln(CRP+1) )*1.10 +1.15"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tblStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip.ItemClicked
        Try
            tblStrip.Select()
            Select Case e.ClickedItem.Tag
                Case "Close"
                    If IsChange = True Then
                        Dim dresult As DialogResult
                        dresult = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                        If dresult = Windows.Forms.DialogResult.No Then
                            IsChange = False
                            Me.Close()
                        End If
                        If dresult = Windows.Forms.DialogResult.Yes Then
                            SaveDAS()
                            IsChange = False
                            Me.Close()
                        End If
                        If dresult = Windows.Forms.DialogResult.Cancel Then
                            Return
                        End If
                    Else
                        Me.Close()
                    End If

                Case "Ok" 'Save + Close
                    SaveDAS()
                    IsChange = False
                    Me.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTenderCount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTenderCount.KeyPress
        Try
            Dim allowedChars As String = "0123456789"

            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If

            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtSwollenCount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSwollenCount.KeyPress
        Try
            Dim allowedChars As String = "0123456789"

            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtESR_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtESR.KeyPress
        Try

            If Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
            End If

            If e.KeyChar = "." And txtESR.Text.IndexOf(".") = -1 Then
                e.Handled = False 'allow single decimal point
            End If

            If e.KeyChar = Chr(13) Then txtESR.Focus() 'Enter key moves to specified control, or:
            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCRP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCRP.KeyPress
        Try

            If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

            If e.KeyChar = "." And txtCRP.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

            If e.KeyChar = Chr(13) Then txtCRP.Focus() 'Enter key moves to specified control, or:
            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtESR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtESR.TextChanged
        Try
            If txtESR.Enabled Then

                If Not Convert.ToString(txtESR.Text) = "" And Not Convert.ToString(txtESR.Text) = "." Then
                    If Convert.ToSingle(txtESR.Text) > 150 Or Convert.ToSingle(txtESR.Text) < 1 Then
                        If IsLabValue Then
                            MessageBox.Show("Lab result value for ESR is not in range between 1-150", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Value should be between 1-150", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                        txtESR.Text = 1
                    End If
                Else
                    If Convert.ToString(txtESR.Text) = "." Then
                        If IsLabValue Then
                            MessageBox.Show("Lab result value for ESR is not in range between 1-150", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Value should be between 1-150", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    End If
                    txtESR.Text = 1
                End If
            End If

            Dim dot As Integer, ch As String
            dot = txtESR.Text.IndexOf(".")

            If dot > -1 Then 'allow only 2 decimal places

                ch = txtESR.Text.Substring(dot + 1)
                If ch.Length > 2 Then
                    txtESR.Text = txtESR.Text.Remove(txtESR.Text.Length - 1, 1).Trim()
                End If
            End If


            txtESR.Select(txtESR.Text.Length, 0)
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtCRP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCRP.TextChanged
        Try
            If txtCRP.Enabled Then

                If Not Convert.ToString(txtCRP.Text) = "" And Not Convert.ToString(txtCRP.Text) = "." Then
                    If Convert.ToSingle(txtCRP.Text) > 150 Then
                        If IsLabValue Then
                            MessageBox.Show("Lab result value for CRP is not in range between 0-150", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Value should be between 0-150", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                        txtCRP.Text = 0
                    End If
                Else
                    If Convert.ToString(txtCRP.Text) = "." Then
                        txtCRP.Text = "0."
                    Else
                        txtCRP.Text = 0
                    End If

                End If
            End If


            Dim dot As Integer, ch As String
            dot = txtCRP.Text.IndexOf(".")

            If dot > -1 Then 'allow only 2 decimal places

                ch = txtCRP.Text.Substring(dot + 1)
                If ch.Length > 2 Then
                    txtCRP.Text = txtCRP.Text.Remove(txtCRP.Text.Length - 1, 1).Trim()
                End If
            End If

            txtCRP.Select(txtCRP.Text.Length, 0)
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CalculateDAS()
        Try

            Dim total As Single

            Dim cntTender As Integer
            Dim cntSwollen As Integer

            Dim cntESR As Single
            Dim cntCRP As Single

            Dim cntPainScore As Integer
            cntPainScore = Convert.ToInt16(trbPainScale.Value)

            If txtTenderCount.Text.ToString().Trim() = "" Then
                cntTender = 0
            Else
                cntTender = Convert.ToInt16(txtTenderCount.Text.ToString().Trim())
            End If

            If txtSwollenCount.Text.ToString().Trim() = "" Then
                cntSwollen = 0
            Else
                cntSwollen = Convert.ToInt16(txtSwollenCount.Text.ToString().Trim())
            End If

            If txtESR.Text.ToString().Trim() = "" Then
                cntESR = 0
            Else
                cntESR = Convert.ToSingle(txtESR.Text.ToString().Trim())
            End If

            If txtCRP.Text.ToString().Trim() = "" Then
                cntCRP = 0
            Else
                cntCRP = Convert.ToSingle(txtCRP.Text.ToString().Trim())
            End If

            If rbtnESR.Checked Then
                If chkPainScale.Checked Then
                    '0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.70*ln(ESR) + 0.014*PS
                    total = Convert.ToSingle((0.56 * Math.Sqrt(cntTender)) + (0.28 * Math.Sqrt(cntSwollen)) + (0.7 * Math.Log(cntESR)) + (0.014 * cntPainScore))
                Else
                    '(0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.70*ln(ESR))*1.08 + 0.16
                    total = Convert.ToSingle(((0.56 * Math.Sqrt(cntTender)) + (0.28 * Math.Sqrt(cntSwollen)) + (0.7 * Math.Log(cntESR))) * 1.08 + 0.16)
                End If
            Else
                If chkPainScale.Checked Then
                    '0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.36*ln(CRP+1) + 0.014*PS +0.96
                    total = Convert.ToSingle((0.56 * Math.Sqrt(cntTender)) + (0.28 * Math.Sqrt(cntSwollen)) + (0.36 * Math.Log(cntCRP + 1)) + (0.014 * cntPainScore) + 0.96)
                Else
                    '(0.56*sqrt(pjc28) + 0.28*sqrt(sjc28) + 0.36*ln(CRP+1) )*1.10 +1.15
                    total = Convert.ToSingle(((0.56 * Math.Sqrt(cntTender)) + (0.28 * Math.Sqrt(cntSwollen)) + (0.36 * Math.Log(cntCRP + 1))) * 1.1 + 1.15)
                End If

            End If
            total = FormatNumber(total, 2, TriState.True)
            txtCalculatedDAS.Text = total

            IsChange = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Try
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trbPainScale_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trbPainScale.ValueChanged
        txtPainScore.Text = trbPainScale.Value
        CalculateDAS()
    End Sub

    Private Sub txtTenderCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTenderCount.TextChanged
        Try
            If Not txtTenderCount.Text.ToString() = "" Then
                If Convert.ToInt64(txtTenderCount.Text) > 28 Then
                    MessageBox.Show("Value should be less than or equal to 28", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtTenderCount.Text = "28"
                    txtTenderCount.Focus()
                End If
            Else
                txtTenderCount.Text = "0"
            End If
            txtTenderCount.Select(txtTenderCount.Text.Length, 0)
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtSwollenCount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSwollenCount.TextChanged
        Try
            If Not txtSwollenCount.Text.ToString().Trim() = "" Then
                If Convert.ToInt64(txtSwollenCount.Text) > 28 Then
                    MessageBox.Show("Value should be less than or equal to 28", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtSwollenCount.Text = "28"
                    txtSwollenCount.Focus()
                End If
            Else
                txtSwollenCount.Text = "0"
            End If
            txtSwollenCount.Select(txtSwollenCount.Text.Length, 0)
            CalculateDAS()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveDAS()
        Try
            If Not IsNothing(oclsDAS) Then
                If Not IsNothing(oclsDAS.DASImage) Then
                    oclsDAS.DASImage.Dispose()
                    oclsDAS.DASImage = Nothing
                End If
            End If
            oclsDAS = New clsDAS()

            oclsDAS.nDASID = _DASID
            oclsDAS.nVitalID = _VitalID
            If rbtnUseDiagram.Checked Then
                oclsDAS.IsDiagram = 1
            Else
                oclsDAS.IsDiagram = 0
            End If

            oclsDAS.TenderJointCount = Convert.ToInt16(txtTenderCount.Text)
            oclsDAS.SwollenJointCount = Convert.ToInt16(txtSwollenCount.Text)

            'ESR Or CRP
            If rbtnESR.Checked Then
                oclsDAS.IsESR = 1
                oclsDAS.IsCRP = 0

                oclsDAS.ESRCount = Convert.ToSingle(txtESR.Text)
                oclsDAS.CRPCount = 0
            Else
                oclsDAS.IsCRP = 1
                oclsDAS.IsESR = 0

                oclsDAS.ESRCount = 0
                oclsDAS.CRPCount = Convert.ToSingle(txtCRP.Text)
            End If

            'Pain Score
            If chkPainScale.Checked Then
                oclsDAS.IsPainScale = 1
                oclsDAS.PainScore = Convert.ToInt16(trbPainScale.Value)
            Else
                oclsDAS.IsPainScale = 0
                oclsDAS.PainScore = 0
            End If

            oclsDAS.FormulaName = lblCalculationName.Text
            oclsDAS.CalculatedFormula = lblCalculationFormula.Text

            Dim strSelectedNode As String = ""
            Dim opnlmain, ocntl, opnlImage As Control
            Dim ochk As CheckBox
            For Each opnlmain In Me.Controls
                If opnlmain.Name = "pnlMain" Then
                    For Each opnlImage In opnlmain.Controls
                        If opnlImage.Name = "pnlImages" Then
                            For Each ocntl In opnlImage.Controls
                                If (TypeOf ocntl Is CheckBox) Then
                                    'To Get Swollen Join checked item
                                    ochk = DirectCast(ocntl, CheckBox)
                                    If ochk.Checked Then
                                        If ocntl.Name.Contains("chk_S_") Then
                                            strSelectedNode = strSelectedNode & "," & ocntl.Name.ToString()
                                        End If
                                        'To Get Tender Join checked item
                                        If ocntl.Name.Contains("chk_T_") Then
                                            strSelectedNode = strSelectedNode & "," & ocntl.Name.ToString()
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            oclsDAS.DASImage = clsDASCapture()
            strSelectedNode = strSelectedNode.Trim(",")
            oclsDAS.SelectedJointNodes = strSelectedNode
            oclsDAS.DASValue = Convert.ToSingle(txtCalculatedDAS.Text)
            DlogResult = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Function clsDASCapture() As Image
        Dim myBitmap As Image = Nothing
        Dim brsRd As Brush = New SolidBrush(Color.Red)
        Dim brsBl As Brush = New SolidBrush(Color.Blue)
        Dim penRd As Pen = New Pen(Color.Red)
        Dim penBl As Pen = New Pen(Color.Blue)

        Try

            If rbtnJointCount.Checked = True And rbtnUseDiagram.Checked = False Then
                myBitmap = CType(pb_HideImage.Image.Clone(), Image)
            ElseIf rbtnJointCount.Checked = False And rbtnUseDiagram.Checked = True Then
                myBitmap = CType(PictureBox1.Image.Clone(), Image)
                Dim grBMp As Graphics = Graphics.FromImage(myBitmap)
                grBMp.CompositingMode = Drawing2D.CompositingMode.SourceCopy
                penRd.Width = 2
                penBl.Width = 2

                If chk_T_R_Shoulder.Checked Then
                    grBMp.FillEllipse(brsRd, 290, 320, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 290, 320, 45, 45)
                End If
                If chk_T_R_Elbow.Checked Then
                    grBMp.FillEllipse(brsRd, 265, 610, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 265, 610, 45, 45)
                End If
                If chk_T_R_Wrist.Checked Then
                    grBMp.FillEllipse(brsRd, 198, 795, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 198, 795, 45, 45)
                End If

                If chk_T_R_ThunbDown.Checked Then
                    grBMp.FillEllipse(brsRd, 50, 970, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 50, 970, 29, 29)
                End If
                If chk_T_R_ThunbUp.Checked Then
                    grBMp.FillEllipse(brsRd, 25, 1005, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 25, 1005, 29, 29)
                End If

                If chk_T_R_IndexDown.Checked Then
                    grBMp.FillEllipse(brsRd, 75, 1025, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 75, 1025, 29, 29)
                End If
                If chk_T_R_IndexUp.Checked Then
                    grBMp.FillEllipse(brsRd, 45, 1095, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 45, 1095, 29, 29)
                End If

                If chk_T_R_MiddleDown.Checked Then
                    grBMp.FillEllipse(brsRd, 112, 1050, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 112, 1050, 29, 29)
                End If
                If chk_T_R_MiddleUp.Checked Then
                    grBMp.FillEllipse(brsRd, 89, 1115, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 89, 1115, 29, 29)
                End If

                If chk_T_R_RingDown.Checked Then
                    grBMp.FillEllipse(brsRd, 145, 1075, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 145, 1075, 29, 29)
                End If
                If chk_T_R_RingUp.Checked Then
                    grBMp.FillEllipse(brsRd, 123, 1135, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 123, 1135, 29, 29)
                End If

                If chk_T_R_LittleDown.Checked Then
                    grBMp.FillEllipse(brsRd, 180, 1085, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 180, 1085, 29, 29)
                End If
                If chk_T_R_LittleUp.Checked Then
                    grBMp.FillEllipse(brsRd, 162, 1150, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 162, 1150, 29, 29)
                End If

                If chk_T_R_Knee.Checked Then
                    grBMp.FillEllipse(brsRd, 365, 1250, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 365, 1250, 45, 45)
                End If


                If chk_T_L_Shoulder.Checked Then
                    grBMp.FillEllipse(brsRd, 630, 320, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 630, 320, 45, 45)
                End If
                If chk_T_L_Elbow.Checked Then
                    grBMp.FillEllipse(brsRd, 655, 610, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 655, 610, 45, 45)
                End If
                If chk_T_L_Wrist.Checked Then
                    grBMp.FillEllipse(brsRd, 720, 795, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 720, 795, 45, 45)
                End If

                If chk_T_L_ThunbDown.Checked Then
                    grBMp.FillEllipse(brsRd, 890, 970, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 890, 970, 29, 29)
                End If
                If chk_T_L_ThunbUp.Checked Then
                    grBMp.FillEllipse(brsRd, 910, 1005, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 910, 1005, 29, 29)
                End If

                If chk_T_L_IndexDown.Checked Then
                    grBMp.FillEllipse(brsRd, 860, 1025, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 860, 1025, 29, 29)
                End If
                If chk_T_L_IndexUp.Checked Then
                    grBMp.FillEllipse(brsRd, 885, 1095, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 885, 1095, 29, 29)
                End If

                If chk_T_L_MiddleDown.Checked Then
                    grBMp.FillEllipse(brsRd, 820, 1050, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 820, 1050, 29, 29)
                End If
                If chk_T_L_MiddleUp.Checked Then
                    grBMp.FillEllipse(brsRd, 845, 1115, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 845, 1115, 29, 29)
                End If

                If chk_T_L_RingDown.Checked Then
                    grBMp.FillEllipse(brsRd, 790, 1075, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 790, 1075, 29, 29)
                End If
                If chk_T_L_RingUp.Checked Then
                    grBMp.FillEllipse(brsRd, 810, 1135, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 810, 1135, 29, 29)
                End If

                If chk_T_L_LittleDown.Checked Then
                    grBMp.FillEllipse(brsRd, 752, 1085, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 752, 1085, 29, 29)
                End If
                If chk_T_L_LittleUp.Checked Then
                    grBMp.FillEllipse(brsRd, 770, 1150, 29, 29)
                Else
                    grBMp.DrawEllipse(penRd, 770, 1150, 29, 29)
                End If

                If chk_T_L_Knee.Checked Then
                    grBMp.FillEllipse(brsRd, 550, 1250, 45, 45)
                Else
                    grBMp.DrawEllipse(penRd, 550, 1250, 45, 45)
                End If



                If chk_S_R_Shoulder.Checked Then
                    grBMp.FillEllipse(brsBl, 1340, 320, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1340, 320, 45, 45)
                End If
                If chk_S_R_Elbow.Checked Then
                    grBMp.FillEllipse(brsBl, 1310, 610, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1310, 610, 45, 45)
                End If
                If chk_S_R_Wrist.Checked Then
                    grBMp.FillEllipse(brsBl, 1250, 795, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1250, 795, 45, 45)
                End If

                If chk_S_R_ThunbDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1100, 970, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1100, 970, 29, 29)
                End If
                If chk_S_R_ThunbUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1075, 1005, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1075, 1005, 29, 29)
                End If

                If chk_S_R_IndexDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1125, 1025, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1125, 1025, 29, 29)
                End If
                If chk_S_R_IndexUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1100, 1095, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1100, 1095, 29, 29)
                End If

                If chk_S_R_MiddleDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1165, 1050, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1165, 1050, 29, 29)
                End If

                If chk_S_R_MiddleUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1140, 1115, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1140, 1115, 29, 29)
                End If

                If chk_S_R_RingDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1195, 1075, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1195, 1075, 29, 29)
                End If
                If chk_S_R_RingUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1175, 1135, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1175, 1135, 29, 29)
                End If

                If chk_S_R_LittleDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1230, 1085, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1230, 1085, 29, 29)
                End If
                If chk_S_R_LittleUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1212, 1150, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1212, 1150, 29, 29)
                End If

                If chk_S_R_Knee.Checked Then
                    grBMp.FillEllipse(brsBl, 1420, 1250, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1420, 1250, 45, 45)
                End If


                If chk_S_L_Shoulder.Checked Then
                    grBMp.FillEllipse(brsBl, 1680, 320, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1680, 320, 45, 45)
                End If
                If chk_S_L_Elbow.Checked Then
                    grBMp.FillEllipse(brsBl, 1705, 610, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1705, 610, 45, 45)
                End If
                If chk_S_L_Wrist.Checked Then
                    grBMp.FillEllipse(brsBl, 1770, 795, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1770, 795, 45, 45)
                End If

                If chk_S_L_ThunbDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1940, 970, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1940, 970, 29, 29)
                End If
                If chk_S_L_ThunbUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1960, 1005, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1960, 1005, 29, 29)
                End If

                If chk_S_L_IndexDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1910, 1025, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1910, 1025, 29, 29)
                End If
                If chk_S_L_IndexUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1935, 1095, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1935, 1095, 29, 29)
                End If

                If chk_S_L_MiddleDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1870, 1050, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1870, 1050, 29, 29)
                End If
                If chk_S_L_MiddleUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1895, 1115, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1895, 1115, 29, 29)
                End If

                If chk_S_L_RingDown1.Checked Then
                    grBMp.FillEllipse(brsBl, 1840, 1075, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1840, 1075, 29, 29)
                End If
                If chk_S_L_RingUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1860, 1135, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1860, 1135, 29, 29)
                End If

                If chk_S_L_LittleDown.Checked Then
                    grBMp.FillEllipse(brsBl, 1802, 1085, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1802, 1085, 29, 29)
                End If
                If chk_S_L_LittleUp.Checked Then
                    grBMp.FillEllipse(brsBl, 1820, 1150, 29, 29)
                Else
                    grBMp.DrawEllipse(penBl, 1820, 1150, 29, 29)
                End If

                If chk_S_L_Knee.Checked Then
                    grBMp.FillEllipse(brsBl, 1600, 1250, 45, 45)
                Else
                    grBMp.DrawEllipse(penBl, 1600, 1250, 45, 45)
                End If
                'SLR : There is a path in startup directory how rights will be there? Is it not configurable? or only for debugging purpose and later will be removed?
                ' myBitmap.Save(Application.StartupPath + "\imgDasRD.bmp")
                grBMp.Dispose()
                grBMp = Nothing
            End If

            Return myBitmap
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return myBitmap
        Finally
            'If Not IsNothing(myBitmap) Then
            '    myBitmap.Dispose()
            '    myBitmap = Nothing
            'End If

            'If Not IsNothing(brsRd) Then
            '    brsRd.Dispose()
            '    brsRd = Nothing
            'End If
            If Not IsNothing(brsRd) Then
                brsRd.Dispose()
                brsRd = Nothing
            End If

            If Not IsNothing(brsBl) Then
                brsBl.Dispose()
                brsBl = Nothing
            End If

            If Not IsNothing(penRd) Then
                penRd.Dispose()
                penRd = Nothing
            End If

            If Not IsNothing(penBl) Then
                penBl.Dispose()
                penBl = Nothing
            End If



        End Try

    End Function


    Private Sub FillDAS(ByVal VitalID As Long)
        Try
            If Not IsNothing(oclsDAS) Then
                If Not IsNothing(oclsDAS.DASImage) Then
                    oclsDAS.DASImage.Dispose()
                    oclsDAS.DASImage = Nothing
                End If
            End If
            oclsDAS = New clsDAS

            oclsDAS.GetDAS(VitalID)

            '_VitalID = oclsDAS.nVitalID
            _DASID = oclsDAS.nDASID

            If _DASID > 0 Then

                If oclsDAS.IsDiagram = 1 Then
                    rbtnUseDiagram.Checked = True
                Else
                    rbtnJointCount.Checked = True
                End If

                If oclsDAS.IsESR = 1 Then
                    rbtnESR.Checked = True
                    txtESR.Text = oclsDAS.ESRCount
                    txtCRP.Text = ""
                Else
                    rbtnCRP.Checked = True
                    txtCRP.Text = oclsDAS.CRPCount
                    txtESR.Text = ""
                End If

                If oclsDAS.IsPainScale = 1 Then
                    chkPainScale.Checked = True
                    trbPainScale.Value = oclsDAS.PainScore
                    txtPainScore.Text = oclsDAS.PainScore

                Else
                    chkPainScale.Checked = False
                End If

                lblCalculationFormula.Text = oclsDAS.CalculatedFormula
                lblCalculationName.Text = oclsDAS.FormulaName

                Dim strNodes() As String = Nothing
                If Not oclsDAS.SelectedJointNodes = "" Then
                    strNodes = oclsDAS.SelectedJointNodes.Split(",")
                End If

                If Not IsNothing(strNodes) Then

                    If strNodes.Length > 0 Then

                        Dim opnlmain, ocntl, opnlImage As Control
                        Dim ochk As CheckBox
                        For Each opnlmain In Me.Controls
                            If opnlmain.Name = "pnlMain" Then
                                For Each opnlImage In opnlmain.Controls
                                    If opnlImage.Name = "pnlImages" Then

                                        For Each ocntl In opnlImage.Controls
                                            If (TypeOf ocntl Is CheckBox) Then
                                                'To Check Swollen Joint  item
                                                ochk = DirectCast(ocntl, CheckBox)
                                                If strNodes.Contains(ochk.Name) Then
                                                    ochk.Checked = True
                                                Else
                                                    ochk.Checked = False
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next

                    End If
                End If

                txtTenderCount.Text = oclsDAS.TenderJointCount
                txtSwollenCount.Text = oclsDAS.SwollenJointCount

            End If

            If oclsDAS.nDASID = 0 Then
                GetDASFromLab()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub FillDAS(ByVal oclsDAS As clsDAS)
        Try

            _DASID = oclsDAS.nDASID
            _VitalID = oclsDAS.nVitalID

            If oclsDAS.IsDiagram = 1 Then
                rbtnUseDiagram.Checked = True
            Else
                rbtnJointCount.Checked = True
            End If

            If oclsDAS.IsESR = 1 Then
                rbtnESR.Checked = True
                txtESR.Text = oclsDAS.ESRCount
                txtCRP.Text = ""
            Else
                rbtnCRP.Checked = True
                txtCRP.Text = oclsDAS.CRPCount
                txtESR.Text = ""
            End If

            If oclsDAS.IsPainScale = 1 Then
                chkPainScale.Checked = True
                trbPainScale.Value = oclsDAS.PainScore
                txtPainScore.Text = oclsDAS.PainScore

            Else
                chkPainScale.Checked = False
            End If

            lblCalculationFormula.Text = oclsDAS.CalculatedFormula
            lblCalculationName.Text = oclsDAS.FormulaName

            Dim strNodes() As String = Nothing
            If Not oclsDAS.SelectedJointNodes = "" Then
                strNodes = oclsDAS.SelectedJointNodes.Split(",")
            End If
            If Not IsNothing(strNodes) Then

                If strNodes.Length > 0 Then

                    Dim opnlmain, ocntl, opnlImage As Control
                    Dim ochk As CheckBox
                    For Each opnlmain In Me.Controls
                        If opnlmain.Name = "pnlMain" Then
                            For Each opnlImage In opnlmain.Controls
                                If opnlImage.Name = "pnlImages" Then

                                    For Each ocntl In opnlImage.Controls
                                        If (TypeOf ocntl Is CheckBox) Then
                                            'To Check Swollen Joint  item
                                            ochk = DirectCast(ocntl, CheckBox)
                                            If strNodes.Contains(ochk.Name) Then
                                                ochk.Checked = True
                                            Else
                                                ochk.Checked = False
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            End If

            txtTenderCount.Text = oclsDAS.TenderJointCount
            txtSwollenCount.Text = oclsDAS.SwollenJointCount

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtPainScore_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPainScore.KeyPress
        Try

            If Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
            Else
                e.Handled = False
            End If

            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPainScore_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPainScore.TextChanged
        If Convert.ToString(txtPainScore.Text) = "" Then
            txtPainScore.Text = 0
        End If

        If Convert.ToInt16(txtPainScore.Text) > 100 Then
            txtPainScore.Text = 100
        End If
        trbPainScale.Value = Convert.ToInt16(txtPainScore.Text)
        txtPainScore.Select(txtPainScore.Text.Length, 0)
    End Sub

    Private Sub GetDASFromLab()
        Try
            Dim dt As DataTable
            Dim ocls As New clsDAS

            dt = ocls.GetDASValueFromLab(_PatientId)
            If dt.Rows.Count > 0 Then
                If Not dt.Rows(0)("ESRValue").ToString = "" And Not dt.Rows(0)("ESRValue").ToString = "N/A" Then
                    rbtnESR.Checked = True
                    IsLabValue = True
                    If IsNumeric(dt.Rows(0)("ESRValue").ToString()) Then
                        txtESR.Text = dt.Rows(0)("ESRValue")
                    Else
                        MessageBox.Show("Lab result value for ESR is not in numeric format", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    IsLabValue = False
                Else
                    If Not dt.Rows(0)("CRPValue").ToString = "" And Not dt.Rows(0)("CRPValue").ToString = "N/A" Then
                        rbtnCRP.Checked = True
                        IsLabValue = True
                        If IsNumeric(dt.Rows(0)("CRPValue").ToString()) Then
                            txtCRP.Text = dt.Rows(0)("CRPValue")
                        Else
                            MessageBox.Show("Lab result value for CRP is not in numeric format", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        IsLabValue = False
                    End If
                End If
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If (IsNothing(ocls.DASImage) = False) Then
                ocls.DASImage.Dispose()
                ocls.DASImage = Nothing
            End If
            ocls = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetHeightOnResolution()
        Dim Srn As Screen = Screen.PrimaryScreen
        Dim tempHeight, tempWidth As Integer
        '  Dim FixHeight, FixWidth As Integer
        tempHeight = Srn.Bounds.Height
        tempWidth = Srn.Bounds.Width

        If tempHeight >= 1024 Then
            Me.Height = 970
            Me.StartPosition = FormStartPosition.CenterScreen
        Else
            Me.Height = 720
            Me.Width = Me.Width + 13
            Me.pnlMain.Height = pnlMain.Height - 250
            Me.StartPosition = FormStartPosition.CenterScreen
        End If

    End Sub

    Private Sub frmDASCalculator_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        SetHeightOnResolution()
    End Sub
End Class