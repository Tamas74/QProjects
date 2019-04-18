Imports gloUserControlLibrary
Imports gloGlobal.EPA
Imports gloEMRGeneralLibrary.gloGeneral
Imports Audit = gloAuditTrail.gloAuditTrail

Public Class frmViewEPAProcess

#Region "Variables"
    Dim ConnectionString As String = String.Empty
    Dim UserID As Int64 = 0
    Dim ProviderID As Int64 = 0
    Dim PriorAuthReferenceID As String = String.Empty
    Dim Accelerator As gloUserControlLibrary.gloAcceleratorEPA = Nothing
#End Region

#Region "Constructor and Form Load"

    Public Sub New(ByVal UserID As Int64, ByVal ProviderID As Int64, ByVal PriorAuthReferenceID As String, ByVal ConnectionString As String)
        InitializeComponent()

        Try
            Me.UserID = UserID
            Me.ProviderID = ProviderID

            Me.PriorAuthReferenceID = PriorAuthReferenceID
            Me.ConnectionString = ConnectionString

            Me.Accelerator = New gloAcceleratorEPA(Me.UserID, gstrEPAServiceURL, gstrEPAAPIURL)
            Me.Accelerator.Dock = DockStyle.Fill
            Me.pnlAccelerator.Controls.Add(Me.Accelerator)

        Catch ex As Exception
            Audit.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewEPAProcess_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Accelerator.Navigate(Me.ProviderID, Me.PriorAuthReferenceID)
        Catch ex As Exception
            Audit.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Form Close"

    Private Sub tlsbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtn_Close.Click
        Try
            If Me.pnlAccelerator.Controls.Contains(Me.Accelerator) Then
                Me.pnlAccelerator.Controls.Remove(Me.Accelerator)
            End If

            If Me.Accelerator IsNot Nothing Then
                Me.Accelerator.Dispose()
                Me.Accelerator = Nothing
            End If

            Me.Close()
        Catch ex As Exception
            Audit.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region
    
End Class