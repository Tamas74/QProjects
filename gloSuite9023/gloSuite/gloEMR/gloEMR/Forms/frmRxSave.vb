Imports gloPatientSavingMessage
Imports System.Windows.Forms.Integration

Public Class frmRxSave
    Dim host As ElementHost

    Dim objProcessLayer As clsProcessLayer
    Dim objwindow As gloPatientSavingMessage.UC_RxSaving = Nothing
    Dim _PatientID As Long
    Public Sub New(ByVal obj As gloPatientSavingMessage.UC_RxSaving)
        InitializeComponent()
        objwindow = obj
    End Sub

    Private Sub frmRxSave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(objwindow) Then
            AddHandler objwindow.tlbCloseClick, AddressOf Close_click
            host = New ElementHost()
            pnlMain.Controls.Add(host)
            host.Child = objwindow
            host.Dock = DockStyle.Fill
        End If
    End Sub
    Private Sub Close_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

End Class