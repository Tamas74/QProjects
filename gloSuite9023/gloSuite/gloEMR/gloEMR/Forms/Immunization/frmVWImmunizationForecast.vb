Imports gloUserControlLibrary
Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloGeneral

Public Class frmVWImmunizationForecast

    Dim _PatientID As Long
    Dim _StatusFilter As Integer = 1
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip
    Private _isLoaded As Boolean = False
    
    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID

        globalSecurity.gstrClientMachineName = gstrClientMachineName
        globalSecurity.gnUserID = gnLoginID
        globalSecurity.gstrLoginName = gstrLoginName

    End Sub

    Private Sub frmVWImplantableDevices_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Set_PatientDetailStrip()

            OptAll.Checked = True
            ShowForecast()
            gloC1FlexStyle.Style(C1Forecast, True)
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            _isLoaded = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowForecast()
        Me.Cursor = Cursors.WaitCursor

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As New DataTable()

        Dim dvIM As DataView

        Try
            '0-Inactive 1- Active 2- All 3-Deleted
            If optActive.Checked Then
                _StatusFilter = 1
            Else
                If OptAll.Checked Then
                    _StatusFilter = 2
                Else
                    If optInactive.Checked Then
                        _StatusFilter = 0
                    Else
                        _StatusFilter = 3
                    End If

                End If
            End If
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@strSearch", txtSearch.Text.Trim, ParameterDirection.Input, SqlDbType.VarChar)
            oParam.Add("@status", _StatusFilter, ParameterDirection.Input, SqlDbType.Int)

            oDB.Retrive("gsp_ShowForecast", oParam, dtIM)

            dvIM = dtIM.DefaultView
            C1Forecast.DataSource = dvIM
            C1Forecast.ShowCellLabels = False
            C1Forecast.AllowSorting = True
            C1Forecast.AllowEditing = False
            C1Forecast.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
            Designgrid()

            oParam.Dispose()
            oParam = Nothing

            oDB.Disconnect()
            oDB = Nothing

        Catch ex As Exception
            If oParam IsNot Nothing Then
                oParam.Dispose()
                oParam = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub Designgrid()
        Try
            C1Forecast.Visible = True

            C1Forecast.Rows.Fixed = 1
            If C1Forecast.Rows.Count > 1 Then                
                C1Forecast.Cols(1).DataType = System.Type.GetType("Nullable(of System.DateTime)")                
                C1Forecast.Cols(7).DataType = System.Type.GetType("Nullable(of System.DateTime)")                
                C1Forecast.Cols(8).DataType = System.Type.GetType("Nullable(of System.DateTime)")                
            End If

            C1Forecast.Cols(0).Caption = "Forecast ID"
            C1Forecast.Cols(1).Caption = "Administration Date"
            C1Forecast.Cols(2).Caption = "Patient ID"
            C1Forecast.Cols(3).Caption = "External ID"
            C1Forecast.Cols(4).Caption = "Vaccine Administered"
            C1Forecast.Cols(5).Caption = "Vaccine Group"
            C1Forecast.Cols(6).Caption = "Scheduled Used"
            C1Forecast.Cols(7).Caption = "Vaccination Due Date"
            C1Forecast.Cols(8).Caption = "Earliest Date To Give"

            C1Forecast.Cols(0).Visible = False
            C1Forecast.Cols(1).Visible = True
            C1Forecast.Cols(2).Visible = False
            C1Forecast.Cols(3).Visible = False
            C1Forecast.Cols(4).Visible = True
            C1Forecast.Cols(5).Visible = True
            C1Forecast.Cols(6).Visible = True
            C1Forecast.Cols(7).Visible = True
            C1Forecast.Cols(8).Visible = True
            

            C1Forecast.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(7).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1Forecast.Cols(8).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            
            Dim nWidth As Integer = C1Forecast.Width

            C1Forecast.Cols(1).Width = CInt((0.13 * (nWidth)))
            C1Forecast.Cols(4).Width = CInt((0.25 * (nWidth)))
            C1Forecast.Cols(5).Width = CInt((0.25 * (nWidth)))
            C1Forecast.Cols(6).Width = CInt((0.11 * (nWidth)))
            C1Forecast.Cols(7).Width = CInt((0.13 * (nWidth)))
            C1Forecast.Cols(8).Width = CInt((0.13 * (nWidth)))
            
        Catch ex As Exception
            MessageBox.Show("Error while designing grid", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click
        Me.Close()
    End Sub
    Private Sub ClearContextMenuStrip(ByRef cmnuStrip As ContextMenuStrip)
        Try
            If (IsNothing(cmnuStrip) = False) Then
                Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cmnuStrip}

                If (IsNothing(CmpControls) = False) Then
                    If CmpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                    End If
                End If

                If (IsNothing(CmpControls) = False) Then
                    If CmpControls.Length > 0 Then
                        gloGlobal.cEventHelper.ClearContextMenuStrip(CmpControls)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    
    Private Sub Set_PatientDetailStrip()
        'Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.ImplantableDevices)
            .BringToFront()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        pnlSearch.BringToFront()
        pnlMain.BringToFront()
        pnlTop.SendToBack()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        ShowForecast()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.Clear()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.Query, gloAuditTrail.ActivityType.Query, "Implantable Device Query """ + txtSearch.Text + """", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
        End If

    End Sub

    Private Sub Status_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optActive.CheckedChanged, optInactive.CheckedChanged, OptAll.CheckedChanged
        If _isLoaded Then
            ShowForecast()
        End If
    End Sub

    Private Sub C1Forecast_OwnerDrawCell(sender As Object, e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles C1Forecast.OwnerDrawCell
        If e.Row > 0 And (e.Col = 12 Or e.Col = 13) Then
            Dim value As Date = C1Forecast.GetData(e.Row, e.Col)
            If IsDate(value) Then
                If value.Year <= 1900 Then
                    e.Text = String.Empty
                End If
            End If
        End If
    End Sub

    Private Sub OptDelete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles OptDelete.CheckedChanged
        If _isLoaded Then
            ShowForecast()
        End If
    End Sub
End Class
