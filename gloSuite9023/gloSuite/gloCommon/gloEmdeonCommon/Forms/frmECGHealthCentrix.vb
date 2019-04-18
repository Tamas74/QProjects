Imports AxQuintonECG
Imports ECGMgmtCom
Imports QuintonECG
Imports System.Windows
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class frmECGHealthCentrix

    Dim _nPatientId As Int64 = 0
    Dim _sConnectionString As String = String.Empty
    Dim _sOrderId As String = String.Empty
    Dim _TestId As String = String.Empty
    Dim _VisitId As Int64 = 0
    Dim gloUC_PatientStrip1 As New gloUserControlLibrary.gloUC_PatientStrip()
    Dim _sCurrentProcess As String = String.Empty
    Dim _nProcessCount As Integer = 0

    Public Sub New(ByVal nPatientId As Long, ByVal sConnectionString As String, ByVal nVisitId As Long)

        _nPatientId = nPatientId
        _VisitId = nVisitId
        _sConnectionString = sConnectionString
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal nPatientId As Long, ByVal sConnectionString As String, ByVal sOrderid As String, ByVal sTestId As String, ByVal nVisitId As Int64)
        _nPatientId = nPatientId
        _VisitId = nVisitId
        _sConnectionString = sConnectionString
        _sOrderId = sOrderid
        _TestId = sTestId

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmECGHealthCentrix_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            LoadPatientStrip()
        
            Dim ofrmLogin As New frmHealthCentrixLoad(EI, frmHealthCentrixLoad.ProcessTypes.Login)
            ofrmLogin.ShowInTaskbar = False
            ofrmLogin.BringToFront()
            ofrmLogin.ShowDialog(IIf(IsNothing(ofrmLogin.Parent), Me, ofrmLogin.Parent))

            If ofrmLogin.ErrorString.Length > 0 Then
                System.Windows.Forms.MessageBox.Show(ofrmLogin.ErrorString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                ofrmLogin.Dispose()
                Return
            End If
            EI.ViewTest(_TestId, ComEditMode.Edit)
            ofrmLogin.Dispose()
        Catch ex As COMException
            System.Windows.Forms.MessageBox.Show(mdlEcgProcessLayer.GetErrorString(ex.ErrorCode), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub LoadPatientStrip()
        Try
            '  Add Patient Details Control 

            gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
            gloUC_PatientStrip1.BringToFront()
            Panel1.BringToFront()
            gloUC_PatientStrip1.DTPEnabled = False
            If True Then
                gloUC_PatientStrip1.Dock = DockStyle.Top
                gloUC_PatientStrip1.DTPValue = Now
                gloUC_PatientStrip1.ShowDetail(_nPatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None, 0, _VisitId, 0, False, _
                 False, False, "", False)
            End If
            pnlControl.Controls.Add(gloUC_PatientStrip1)
        Catch ex As Exception
             gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub frmECGHealthCentrix_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            EI.Logout()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
       
    End Sub

    Private Sub EI_ViewTestComplete(ByVal sender As System.Object, ByVal e As AxQuintonECG.__ECGIntegrationCtrl_ViewTestCompleteEvent) Handles EI.ViewTestComplete
        Dim ofrmLogin As New frmHealthCentrixLoad(EI, _nPatientId, frmHealthCentrixLoad.ProcessTypes.GetSelectedOrderInfo, _sOrderId, _TestId.ToString(), 0, 0)
        ofrmLogin.ShowInTaskbar = False
        ofrmLogin.BringToFront()
        ofrmLogin.ShowDialog(IIf(IsNothing(ofrmLogin.Parent), Me, ofrmLogin.Parent))

        If ofrmLogin.ErrorString.Length > 0 Then
            System.Windows.Forms.MessageBox.Show(ofrmLogin.ErrorString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            ofrmLogin.Dispose()
            Return
        End If
        ofrmLogin.Dispose()

        Me.Close()
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click


        If System.Windows.Forms.MessageBox.Show("Are you sure you want to close?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Forms.DialogResult.Yes Then
            Me.Close()
        Else
            Return
        End If


    End Sub
End Class