Imports System.Windows.Forms.Integration
'Imports gloIntuitSecureMsg


Public Class frmIntuitSecureMessage

    Public PatientID As Int64
    Private ConnectionString As String
    Private ClientMachineID As String
    Private ClientMachineName As String
    Private _IsSendMsgFrm As Boolean
    Public _CommDetailID As Int64


    Public Sub New(ByVal nPatientID As Int64, ByVal sConnectionString As String, ByVal sClientMachineID As String, ByVal sClientMachineName As String, ByVal IsSendMsgFrm As Boolean, Optional ByVal CommDetailID As Int64 = 0)
        MyBase.New()
        InitializeComponent()
        PatientID = nPatientID
        ConnectionString = sConnectionString
        ClientMachineID = sClientMachineID
        ClientMachineName = sClientMachineName
        _IsSendMsgFrm = IsSendMsgFrm

        _CommDetailID = CommDetailID
    End Sub

    Dim ucSendMsg As gloUserControlLibrary.UC_SendIntuitSecureMsg
    Dim ucReadMsg As gloUserControlLibrary.UC_ReadSecureMsg
    Dim elReadMsg As New ElementHost


    Private Sub frmIntuitSecureMessage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _IsSendMsgFrm = True Then

            Dim elSendMsg As New ElementHost
            ucSendMsg = New gloUserControlLibrary.UC_SendIntuitSecureMsg(PatientID, ConnectionString, ClientMachineID, ClientMachineName)
            RemoveHandler ucSendMsg.SetFormHeight, AddressOf SetFormHeight
            RemoveHandler ucSendMsg.GetFormHeight, AddressOf GetFormHeight
            AddHandler ucSendMsg.SetFormHeight, AddressOf SetFormHeight
            AddHandler ucSendMsg.GetFormHeight, AddressOf GetFormHeight
            elSendMsg.Child = ucSendMsg
            elSendMsg.Dock = DockStyle.Fill
            Me.Controls.Add(elSendMsg)

            AddHandler ucSendMsg.CloseIntuitForm, AddressOf ClosefrmIntuitSecureMessage
        Else


            ucReadMsg = New gloUserControlLibrary.UC_ReadSecureMsg(PatientID, _CommDetailID, GetConnectionString(), gstrgloEMRStartupPath, gstrgloTempFolder)
            RemoveHandler ucReadMsg.SetFormHeight, AddressOf SetFormHeight
            RemoveHandler ucReadMsg.GetFormHeight, AddressOf GetFormHeight
            AddHandler ucReadMsg.SetFormHeight, AddressOf SetFormHeight
            AddHandler ucReadMsg.GetFormHeight, AddressOf GetFormHeight
            '' AddHandler ucReadMsg.OpenAttachFile, AddressOf OpenAttachFile
            elReadMsg.Child = ucReadMsg
            elReadMsg.Dock = DockStyle.Fill
            Me.Controls.Add(elReadMsg)
            RemoveHandler ucReadMsg.CloseIntuitForm, AddressOf ClosefrmIntuitSecureMessage
            AddHandler ucReadMsg.CloseIntuitForm, AddressOf ClosefrmIntuitSecureMessage

            AddHandler ucReadMsg.OpenSendSecureMsgForm, AddressOf OpenSendSecureMsgForm
        End If


      
    End Sub
    Private Sub SetFormHeight()
        'If uc.ht = 31 Then
        '    Me.Height = 600 + 73 + uc.ht
        'Else
        '    Me.Height = 600 + 73 + uc.ht
        'End If
        If _IsSendMsgFrm = True Then
            If IsNothing(ucSendMsg) = False Then
                If ucSendMsg.ht = 31 Then
                    Me.Height = 627 + 73 + ucSendMsg.ht
                Else
                    Me.Height = 627 + 73 + ucSendMsg.ht
                End If
            End If
        Else
            If IsNothing(ucReadMsg) = False Then
                If ucReadMsg.ht = 31 Then
                    Me.Height = 590 + 73 + ucReadMsg.ht
                Else
                    Me.Height = 590 + 73 + ucReadMsg.ht
                End If
            End If
        End If
    End Sub

    Private Sub GetFormHeight()
        '  Me.Height = 600 + 73 + uc.ht
        If _IsSendMsgFrm = True Then

            Me.Height = 627 + 73 + ucSendMsg.ht
        Else
            Me.Height = 590 + 73 + ucReadMsg.ht
        End If
    End Sub

    Private Sub ClosefrmIntuitSecureMessage()
        Me.Close()
    End Sub
    Private Sub OpenSendSecureMsgForm()
        ElementHost1.Visible = True
        If IsNothing(elReadMsg) = False Then
            Me.Controls.Remove(elReadMsg)
        End If
        Dim elSendMsg As New ElementHost
        ucSendMsg = New gloUserControlLibrary.UC_SendIntuitSecureMsg(PatientID, ConnectionString, ClientMachineID, ClientMachineName)
        RemoveHandler ucSendMsg.SetFormHeight, AddressOf SetFormHeight
        RemoveHandler ucSendMsg.GetFormHeight, AddressOf GetFormHeight
        AddHandler ucSendMsg.SetFormHeight, AddressOf SetFormHeight
        AddHandler ucSendMsg.GetFormHeight, AddressOf GetFormHeight
        elSendMsg.Child = ucSendMsg
        elSendMsg.Dock = DockStyle.Fill
        elSendMsg.BringToFront()
        _IsSendMsgFrm = True
        Me.Controls.Add(elSendMsg)

        AddHandler ucSendMsg.CloseIntuitForm, AddressOf ClosefrmIntuitSecureMessage
        ElementHost1.Visible = False
    End Sub

End Class