Imports gloUserControlLibrary
Imports gloGlobal.EPA
Imports gloEMRGeneralLibrary.gloGeneral
Imports Audit = gloAuditTrail.gloAuditTrail

Public Class frmViewEPA

#Region "Properties and Variables"
    Public Property ConnectionString As String
    Public Property UserID As Int64

    Dim ServiceType As ServiceType = gloGlobal.EPA.ServiceType.Worklist

    Dim Accelerator As gloUserControlLibrary.gloAcceleratorEPA = Nothing
    Dim ePADatabaseLayer As EPABusinesslayer = Nothing
#End Region

#Region "Constructor and Loading"

    Public Sub New(ByVal ConnectionString As String, ByVal UserID As Int64)
        InitializeComponent()

        Try
            Me.ConnectionString = ConnectionString
            Me.UserID = UserID

            Me.ePADatabaseLayer = New EPABusinesslayer()
            Me.Accelerator = New gloAcceleratorEPA(Me.UserID, gstrEPAServiceURL, gstrEPAAPIURL)

            Me.Accelerator.Dock = DockStyle.Fill
            Me.pnlAccelerator.Controls.Add(Me.Accelerator)
        Catch ex As Exception
            Audit.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewEPA_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.BindProviders()
        Me.NavigateAccelerator()
    End Sub

#End Region

#Region "Buttons Click"

    Private Sub tlsbtn_WorkList_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtn_WorkList.Click
        Me.ServiceType = gloGlobal.EPA.ServiceType.Worklist
        Me.NavigateAccelerator()
    End Sub

    Private Sub tlsbtn_Processes_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtn_Processes.Click
        Me.ServiceType = gloGlobal.EPA.ServiceType.WorkProcess
        Me.NavigateAccelerator()
    End Sub

    Private Sub tlsbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtn_Close.Click
        Try
            If ePADatabaseLayer IsNot Nothing Then
                ePADatabaseLayer.Dispose()
                ePADatabaseLayer = Nothing
            End If

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

#Region "Bind Providers and Navigate Accelerator"

    Private Sub BindProviders()
        Dim dtProviders As DataTable = Nothing

        Try
            dtProviders = ePADatabaseLayer.GetProviderInRoles(Me.UserID)
            If dtProviders IsNot Nothing AndAlso dtProviders.Rows.Count > 0 Then
                cmbProviders.ValueMember = dtProviders.Columns("nProviderID").ColumnName
                cmbProviders.DisplayMember = dtProviders.Columns("sProviderName").ColumnName
                cmbProviders.DataSource = dtProviders
                If dtProviders.AsEnumerable().Any(Function(p) p("nProviderID") = gnLoginProviderID) Then
                    cmbProviders.SelectedValue = gnLoginProviderID
                Else
                    If dtProviders.AsEnumerable().Any() Then
                        cmbProviders.SelectedValue = Convert.ToInt64(dtProviders.AsEnumerable().OrderBy(Function(p) Convert.ToString(p("sProviderName"))).FirstOrDefault()("nProviderID"))
                    End If
                End If
            End If
        Catch ex As Exception
            Audit.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NavigateAccelerator()
        Try
            If cmbProviders.SelectedItem IsNot Nothing AndAlso TypeOf (cmbProviders.SelectedItem) Is DataRowView Then
                Dim nProviderID As Int64 = 0
                If Int64.TryParse(Convert.ToString(cmbProviders.SelectedValue), nProviderID) Then
                    Me.Accelerator.Navigate(nProviderID, ServiceType)
                End If
            End If
        Catch ex As Exception
            Audit.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

#Region "Providers Selection Changed"

    Private Sub cmbProviders_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs) Handles cmbProviders.SelectionChangeCommitted
        Me.NavigateAccelerator()
    End Sub

#End Region

End Class