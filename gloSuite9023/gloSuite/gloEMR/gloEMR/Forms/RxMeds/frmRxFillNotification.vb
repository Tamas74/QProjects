Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloUserControlLibrary
Imports gloEMRGeneralLibrary.gloprintfax
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary
Imports System.IO
Imports gloSureScript

Imports schema = gloGlobal.Schemas.Surescript
Imports ss = gloGlobal.SS
Imports common = gloGlobal.Common.ServiceObjectBase

Public Class frmRxFillNotification

#Region "Variables and Properties"

    Private WithEvents PatientStripControl As gloUserControlLibrary.gloUC_PatientStrip
    Private filetodelete = Nothing


    Public ReadOnly Property dtRxFillReqs As DataTable
        Get
            If Me.gloRxFillNotification IsNot Nothing Then
                Return Me.gloRxFillNotification.dtRxFillReqs
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property SSRxFillRequest As schema.RxFill
        Get
            If Me.gloRxFillNotification IsNot Nothing Then
                Return Me.gloRxFillNotification.SSRxFillRequest
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property SSMessageData As schema.MessageType
        Get
            If Me.gloRxFillNotification IsNot Nothing Then
                Return Me.gloRxFillNotification.SSMessageData
            Else
                Return Nothing
            End If
        End Get
    End Property

    Dim PatientID As Int64 = 0
    Dim PrescriberOrderNumber As Int64 = 0
    Dim VisitDate As Date
    Dim MessageID As String = Nothing

#End Region

#Region "Constructor"

    Public Sub New(ByVal PatientId As Int64, ByVal PrescriptionID As Int64, ByVal dtVisitDate As Date)
        InitializeComponent()

        Me.PatientID = PatientId
        Me.PrescriberOrderNumber = PrescriptionID
        Me.VisitDate = dtVisitDate

        Me.InitializeRxRequestUserControl(PatientId, PrescriptionID)
        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.View, "RxFill notification viewed", PatientId, PrescriptionID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
    End Sub

    Public Sub New(ByVal MessageID As String, ByVal dtVisitDate As Date)
        InitializeComponent()

        Me.MessageID = MessageID
        Me.VisitDate = dtVisitDate

        Me.InitializeRxRequestUserControl(MessageID)
        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.View, "RxFill notification viewed for messageid " + MessageID, PatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
    End Sub

#End Region

#Region "Form Events"

    Private Sub frmRxFillNotifications_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Dispose()
        Catch

        End Try
    End Sub

    Private Sub frmRxFillNotifications_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If (Me.MessageID IsNot Nothing) Then
                Using helper As New PrescriptionBusinessLayer()
                    helper.UpdateRxMessageStatus(Me.MessageID, "Viewed", "RxFill")
                End Using
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmRxFillNotifications_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim sPrescriptionId As String = Convert.ToString(PrescriberOrderNumber)
        Try
            If PatientID = 0 Then
                PatientID = GetPatientIdByDemographics()
            End If

            LoadPatientStripControl()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

#Region "Function and Sub"
    Public Function GetPatientIdByDemographics() As Int64
        Dim _nSelectedPatientID As Int64 = 0
        Try
            If SSRxFillRequest IsNot Nothing Then
                If SSRxFillRequest.Patient IsNot Nothing Then
                    If SSRxFillRequest.Patient.Name IsNot Nothing Then
                        With SSRxFillRequest.Patient
                            Using helper As New PrescriptionBusinessLayer()
                                _nSelectedPatientID = helper.GetPatientIdByDemographics(.Name.FirstName, .Name.LastName, .Gender, .DateOfBirth.Item.Date)
                            End Using
                        End With
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _nSelectedPatientID

    End Function

    Private Sub InitializeRxRequestUserControl(ByVal PatientId As Int64, ByVal PrescriptionID As Int64)
        If gloRxFillNotification IsNot Nothing Then
            gloRxFillNotification.PatientID = PatientId
            gloRxFillNotification.PrescriberOrderNumber = PrescriptionID            
        End If
    End Sub

    Private Sub InitializeRxRequestUserControl(ByVal MessageID As String)
        If gloRxFillNotification IsNot Nothing Then
            gloRxFillNotification.MessageID = MessageID        
        End If
    End Sub

    Private Sub LoadPatientStripControl()
        Try
            If PatientID <> 0 Then
                If IsNothing(PatientStripControl) Then
                    PatientStripControl = New gloUserControlLibrary.gloUC_PatientStrip

                    With (PatientStripControl)
                        .Dock = DockStyle.Top
                        .Padding = New Padding(3, 0, 3, 0)
                        .ShowDetail(PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.RxFillNotifications)
                        .BringToFront()
                        '   .TransactionDate = VisitDate

                        '  .DTPEnabled = False

                    End With
                    Me.Controls.Add(PatientStripControl)
                    pnl_toolstrip.SendToBack()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


    Private Sub ts_btnClose_Click(sender As Object, e As System.EventArgs) Handles ts_btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.Close, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnRefresh_Click(sender As Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            '  DisplayRequests()
            Me.gloRxFillNotification.RefreshMessages()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.Refresh, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class