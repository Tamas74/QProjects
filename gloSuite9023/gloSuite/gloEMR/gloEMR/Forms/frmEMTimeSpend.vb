Public Class frmEMTimeSpend
    Private _strStartTime As String
    Private _strEndTime As String

    Private _Result As String

    Private _IsTimeSpent As Boolean
    Private _IsOtherlabs As Boolean
    Private _IsOtherOrders As Boolean
    Private _IsOtherDiagnosticTest As Boolean
    Private _FieldValue As String

    Public Property strStartTime()
        Get
            Return _strStartTime
        End Get
        Set(ByVal value)
            _strStartTime = value
        End Set
    End Property

    Public Property strEndTime()
        Get
            Return _strEndTime
        End Get
        Set(ByVal value)
            _strEndTime = value
        End Set
    End Property

    Public Property Result()
        Get
            Return _Result
        End Get
        Set(ByVal value)
            _Result = value
        End Set
    End Property

    Public Property IsTimeSpent() As Boolean
        Get
            Return _IsTimeSpent
        End Get
        Set(ByVal value As Boolean)
            _IsTimeSpent = value
        End Set
    End Property

    Public Property IsOtherlabs() As Boolean
        Get
            Return _IsOtherlabs
        End Get
        Set(ByVal value As Boolean)
            _IsOtherlabs = value
        End Set
    End Property

    Public Property IsOtherOrders() As Boolean
        Get
            Return _IsOtherOrders
        End Get
        Set(ByVal value As Boolean)
            _IsOtherOrders = value
        End Set
    End Property

    Public Property IsOtherDiagnosticTest() As Boolean
        Get
            Return _IsOtherDiagnosticTest
        End Get
        Set(ByVal value As Boolean)
            _IsOtherDiagnosticTest = value
        End Set
    End Property


    Private Sub frmEMTimeSpend_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '30-Jan-15 Aniket: Resolving Bug #77678: gloEMR - Liquid Links - Application is showig incorrect value in Other Lab count screen.
            If IsTimeSpent = True Then
                pnlTimeSpend.Visible = True
                Dim str() As String = Split(_FieldValue, " ")

                If IsNumeric(str.GetValue(0)) = True Then
                    txttimeSpend.Text = str.GetValue(0)
                Else
                    txttimeSpend.Text = 0
                End If


                lblstarttimetext.Text = Convert.ToDateTime(_strStartTime).ToShortTimeString()
                lblendtimetext.Text = Convert.ToDateTime(_strEndTime).ToShortTimeString()
            ElseIf IsOtherlabs = True Then
                pnlLabs.Visible = True
                If IsNumeric(_FieldValue) = True Then
                    txtLabs.Text = _FieldValue
                Else
                    txtLabs.Text = 0

                End If

            ElseIf IsOtherOrders = True Then
                pnlOrders.Visible = True

                If IsNumeric(_FieldValue) = True Then
                    txtOrders.Text = _FieldValue
                Else
                    txtOrders.Text = 0
                End If

            ElseIf IsOtherDiagnosticTest = True Then
                pnlOtherDiagnostictest.Visible = True

                If IsNumeric(_FieldValue) = True Then
                    txtOtherDiagonsticTest.Text = _FieldValue
                Else
                    txtOtherDiagonsticTest.Text = 0
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tstripDiagnosis_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstripDiagnosis.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    If IsTimeSpent = True Then
                        _Result = txttimeSpend.Text.Trim
                    ElseIf IsOtherlabs = True Then
                        _Result = txtLabs.Text.Trim
                    ElseIf IsOtherOrders = True Then
                        _Result = txtOrders.Text.Trim
                    ElseIf IsOtherDiagnosticTest = True Then
                        _Result = txtOtherDiagonsticTest.Text.Trim
                    End If
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Case "Cancel"
                    If IsTimeSpent = True Then
                        _Result = txttimeSpend.Text.Trim
                    ElseIf IsOtherlabs = True Then
                        _Result = txtLabs.Text.Trim
                    ElseIf IsOtherOrders = True Then
                        _Result = txtOrders.Text.Trim
                    ElseIf IsOtherDiagnosticTest = True Then
                        _Result = txtOtherDiagonsticTest.Text.Trim
                    End If
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub New(ByVal FieldValue As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _FieldValue = FieldValue
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub chkdefaultvalue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkdefaultvalue.CheckedChanged
        Try
            If chkdefaultvalue.Checked = True Then

                Dim ospan As New TimeSpan
                ospan = Convert.ToDateTime(_strEndTime).Subtract(Convert.ToDateTime(_strStartTime))
                Dim strtimeSpend As String = Convert.ToString(Math.Round(ospan.TotalMinutes, 0)) 'Convert.ToString(ospan.Hours) & ":" & Convert.ToString(ospan.Minutes)
                strtimeSpend = strtimeSpend
                txttimeSpend.Enabled = False
                txttimeSpend.Text = strtimeSpend
            Else
                txttimeSpend.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtOrders_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrders.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

   
    Private Sub txtLabs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabs.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txttimeSpend_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttimeSpend.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtOtherDiagonsticTest_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherDiagonsticTest.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
End Class