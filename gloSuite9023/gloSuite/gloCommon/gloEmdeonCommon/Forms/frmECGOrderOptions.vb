Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class frmECGOrderOptions

    Dim _nPatientId As Int64 = 0
    Dim _nVisitId As Int64 = 0
    Dim _sConnectionString As String = String.Empty
    Dim _dtVisitDate As Date
    Dim _nLoginId As Int64 = 0
    Dim _nMachineId As Int64 = 0
    Public Event LoadLocalECGOrder(ByVal nPatientId As Int64, ByVal nVisitId As Int64, ByVal dtVisitDate As Date)

    Dim _TestType As TestType = TestType.NoTestTypeSelected
    Public Enum TestType
        LocalTest
        DeviceTest
        NoTestTypeSelected
    End Enum

    Public Property TestToConduct() As TestType
        Get
            Return _TestType
        End Get
        Set(ByVal value As TestType)
            _TestType = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtVisitDate As Date, ByVal sConnectionString As String, ByVal nLoginId As Int64, ByVal nMachineId As Int64)
        _nPatientId = nPatientID
        _nVisitId = nVisitID
        _dtVisitDate = dtVisitDate
        _nLoginId = nLoginId
        _sConnectionString = sConnectionString
        _nMachineId = nMachineId
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_RecordEcg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RecordEcg.Click
        'start of code commented by manoj jadhav on 20111102 for new device order from gloEMR
        'Me.Hide()
        'RaiseEvent LoadLocalECGOrder(_nPatientId, _nVisitId, _dtVisitDate)
        'end of code commented by manoj jadhav on 20111102 for new device order from gloEMR
        TestToConduct = TestType.LocalTest 'code added by manoj jadhav on 20111102 for new local order from gloEMR
        Me.Close()
    End Sub

    Private Sub btn_deviceOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_deviceOrder.Click
        'start of code commented by manoj jadhav on 20111102 for new device order from gloEMR
        ' ''Check wheter the previous order is still pending, if pending cancel the existing order and place new order.
        'Me.Hide()
        'NewDeviceOrder()
        'end of code commented by manoj jadhav on 20111102 for new device order from gloEMR
        TestToConduct = TestType.DeviceTest 'code added by manoj jadhav on 20111102 for new device order from gloEMR
        Me.Close()
    End Sub
    'start of code commented by manoj jadhav on 20111102 for new device order from gloEMR
    'Public Function CheckPendingDeviceOrders(ByVal nPatientId As Long) As Boolean
    '    Dim objDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
    '    Dim sQuery As String = String.Empty
    '    Dim objResult As Object = Nothing
    '    Dim blnResult As Boolean = False
    '    Try
    '        sQuery = " select count(*) from CV_ElectroCardioGrams WHERE (sOrderId<>'' OR sOrderId<>'') AND sExternalCode='Pending' AND nPatientID=" & nPatientId
    '        objDbLayer.Connect(False)
    '        objResult = objDbLayer.ExecuteScalar_Query(sQuery)
    '        objDbLayer.Disconnect()

    '        If Not IsNothing(objResult) And objResult.ToString() <> "" Then
    '            If Convert.ToInt16(objResult) > 0 Then
    '                blnResult = True
    '            End If
    '        End If

    '    Catch ex As Exception
    '        blnResult = True
    '    Finally
    '        If Not IsNothing(objDbLayer) Then
    '            objDbLayer.Dispose()
    '        End If
    '        objResult = Nothing
    '    End Try
    '    Return blnResult
    'End Function
    'Private Sub NewDeviceOrder()
    '    'Process flow.
    '    '1. Check for pending orders
    '    '2. if the pending order are available then get the satus of the pending order.
    '    '3. again get the pending orders if the orders are till pending ask the user for cancel the order and place new order
    '    '4. if user selects cancel order then cancel existing order and place new order.

    '    Try
    '        mdlEcgProcessLayer.sConnectionString = _sConnectionString
    '        If ValidateUserSettings(_nLoginId) = False Then
    '            MessageBox.Show("Please configure ECG Device interface settings", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Return
    '        End If


    '        '1. Check for pending orders
    '        If CheckPendingDeviceOrders(_nPatientId) Then
    '            'if found then get the staus of order.

    '            Dim objECGForm As New frmHealthCentrixLoad(_nPatientId, frmHealthCentrixLoad.ProcessTypes.GetOrderStatus, "", _nVisitId, _nMachineId)
    '            objECGForm.BringToFront()
    '            objECGForm.ShowInTaskbar = False
    '            objECGForm.ShowDialog(Me)
    '            If objECGForm.ErrorString.Length > 0 And objECGForm.ErrorString <> "Test result not available, Please try again later." Then
    '                MessageBox.Show(objECGForm.ErrorString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                objECGForm.Dispose()
    '                Return
    '            End If

    '            objECGForm = Nothing

    '            'again get the status of order
    '            If CheckPendingDeviceOrders(_nPatientId) Then
    '                ''Ask user wether the existing order should be canceled and place new order.
    '                Dim dgResult As DialogResult
    '                dgResult = MessageBox.Show("Do you want to cancel the pending device order and place new device order?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

    '                If dgResult = Windows.Forms.DialogResult.Yes Then

    '                    'if yes then cancel and create new device order.

    '                    objECGForm = New frmHealthCentrixLoad(_nPatientId, frmHealthCentrixLoad.ProcessTypes.CancelAndPlaceNewOrder, "", _nVisitId, _nMachineId)
    '                    objECGForm.BringToFront()
    '                    objECGForm.ShowInTaskbar = False
    '                    objECGForm.ShowDialog(Me)
    '                    If objECGForm.ErrorString.Length > 0 Then
    '                        MessageBox.Show(objECGForm.ErrorString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                        Return
    '                    Else
    '                        '"New ECG order placed successfully"
    '                        MessageBox.Show("New ECG order placed successfully", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    End If
    '                    objECGForm.Dispose()

    '                Else
    '                    'If no then return
    '                    Exit Sub
    '                End If

    '            Else
    '                'if no create new device order.

    '                objECGForm = New frmHealthCentrixLoad(_nPatientId, frmHealthCentrixLoad.ProcessTypes.PlaceOrder, "", _nVisitId, _nMachineId)
    '                objECGForm.BringToFront()
    '                objECGForm.ShowInTaskbar = False
    '                objECGForm.ShowDialog(Me)
    '                If objECGForm.ErrorString.Length > 0 Then
    '                    MessageBox.Show(objECGForm.ErrorString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Return
    '                Else
    '                    '"New ECG order placed successfully"
    '                    MessageBox.Show("New ECG order placed successfully", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                End If
    '                objECGForm.Dispose()
    '            End If
    '        Else
    '            'if no create new device order.

    '            Dim objECGForm As New frmHealthCentrixLoad(_nPatientId, frmHealthCentrixLoad.ProcessTypes.PlaceOrder, "", _nVisitId, _nMachineId)
    '            objECGForm.BringToFront()
    '            objECGForm.ShowInTaskbar = False
    '            objECGForm.ShowDialog(Me)
    '            If objECGForm.ErrorString.Length > 0 Then
    '                MessageBox.Show(objECGForm.ErrorString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Return
    '            Else
    '                '"New ECG order placed successfully"
    '                MessageBox.Show("New ECG order placed successfully", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If
    '            objECGForm.Dispose()
    '        End If


    '    Catch ex As COMException
    '        If ex.ErrorCode = -2147221164 Then
    '            MessageBox.Show("Please install appropriate ECG device prerequisites.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
    '    Finally

    '    End Try
    'End Sub
    'end of code commented by manoj jadhav on 20111102 for new device order from gloEMR

    Private Sub pnlCloseBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlCloseBtn.Click
        Me.Close()
    End Sub
End Class