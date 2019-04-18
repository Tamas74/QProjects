Imports gloUserControlLibrary
Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloGeneral

Public Class frmVWImplantableDevices

    Dim _PatientID As Long
    Dim _StatusFilter As Integer = 1
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip
    Private _isLoaded As Boolean = False
    Public Shared blnCancelDelete As Boolean = False

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
            Set_PatientDetailStrip()

            optActive.Checked = True
            ShowImplantableDevices()
            gloC1FlexStyle.Style(C1PatientImplantableDevices, True)
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

    Private Sub ShowImplantableDevices()
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

            oDB.Retrive("gsp_ShowImplantableDevices", oParam, dtIM)

            dvIM = dtIM.DefaultView
            C1PatientImplantableDevices.DataSource = dvIM
            C1PatientImplantableDevices.ShowCellLabels = False
            C1PatientImplantableDevices.AllowSorting = True
            C1PatientImplantableDevices.AllowEditing = False
            C1PatientImplantableDevices.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
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
            C1PatientImplantableDevices.Visible = True
            C1PatientImplantableDevices.ShowCellLabels = True
            C1PatientImplantableDevices.Rows.Fixed = 1
            If C1PatientImplantableDevices.Rows.Count > 1 Then
                C1PatientImplantableDevices.Cols(1).DataType = GetType(System.String)                              
            End If

            C1PatientImplantableDevices.Cols(0).Caption = "ID"
            C1PatientImplantableDevices.Cols(1).Caption = "Implant Date"
            C1PatientImplantableDevices.Cols(2).Caption = "Patient Name"
            C1PatientImplantableDevices.Cols(3).Caption = "Device ID"
            C1PatientImplantableDevices.Cols(4).Caption = "Issuing Agency"
            C1PatientImplantableDevices.Cols(5).Caption = "Brand Name"
            C1PatientImplantableDevices.Cols(6).Caption = "Company Name"
            C1PatientImplantableDevices.Cols(7).Caption = "Version Or Model"
            C1PatientImplantableDevices.Cols(8).Caption = "MRI Safety Status"
            C1PatientImplantableDevices.Cols(9).Caption = "Labeled Contains NRL"
            C1PatientImplantableDevices.Cols(10).Caption = "Serial Number"
            C1PatientImplantableDevices.Cols(11).Caption = "Lot / Batch Number"
            C1PatientImplantableDevices.Cols(12).Caption = "Manufacturing Date"
            C1PatientImplantableDevices.Cols(13).Caption = "Expiration Date"
            C1PatientImplantableDevices.Cols(14).Caption = "Device HCT/P Code"
            C1PatientImplantableDevices.Cols(15).Caption = "Status"
            C1PatientImplantableDevices.Cols(16).Caption = "UDI"
            C1PatientImplantableDevices.Cols(17).Caption = "Snomed"
            C1PatientImplantableDevices.Cols(18).Caption = "Device Description"
            C1PatientImplantableDevices.Cols(19).Caption = "GMDN PT Name"

            C1PatientImplantableDevices.Cols(0).Visible = False
            C1PatientImplantableDevices.Cols(2).Visible = False
            

            C1PatientImplantableDevices.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(7).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(8).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(9).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(10).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(11).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(12).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(13).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(14).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(15).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(16).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(17).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(18).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1PatientImplantableDevices.Cols(19).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            Dim nWidth As Integer = C1PatientImplantableDevices.Width

            C1PatientImplantableDevices.Cols(1).Width = CInt((0.08 * (nWidth)))
            C1PatientImplantableDevices.Cols(4).Width = CInt((0.09 * (nWidth)))
            C1PatientImplantableDevices.Cols(5).Width = CInt((0.1 * (nWidth)))
            C1PatientImplantableDevices.Cols(6).Width = CInt((0.2 * (nWidth)))
            C1PatientImplantableDevices.Cols(7).Width = CInt((0.1 * (nWidth)))

            C1PatientImplantableDevices.Cols(8).Width = CInt((0.1 * (nWidth)))
            C1PatientImplantableDevices.Cols(9).Width = CInt((0.1 * (nWidth)))
            C1PatientImplantableDevices.Cols(10).Width = CInt((0.12 * (nWidth)))
            C1PatientImplantableDevices.Cols(11).Width = CInt((0.12 * (nWidth)))
            C1PatientImplantableDevices.Cols(12).Width = CInt((0.1 * (nWidth)))

            C1PatientImplantableDevices.Cols(13).Width = CInt((0.08 * (nWidth)))
            C1PatientImplantableDevices.Cols(14).Width = CInt((0.1 * (nWidth)))
            C1PatientImplantableDevices.Cols(15).Width = CInt((0.05 * (nWidth)))
            C1PatientImplantableDevices.Cols(16).Width = CInt((0.1 * (nWidth)))

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

    Private Sub C1PatientImplantableDevices_AfterRowColChange(sender As Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1PatientImplantableDevices.AfterRowColChange
        
        Try
            If C1PatientImplantableDevices.Rows.Count > 1 Then
                If C1PatientImplantableDevices.Row > 0 AndAlso C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 15) <> "Deleted" Then
                    tblbtn_Modify.Enabled = True
                    tblbtn_Delete.Enabled = True
                Else
                    tblbtn_Modify.Enabled = False
                    tblbtn_Delete.Enabled = False
                End If
            End If
            
        Catch ex As Exception

        End Try

    End Sub
    Private Sub C1PatientImplantableDevices_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientImplantableDevices.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1PatientImplantableDevices.HitTest(ptPoint)

        Try
            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                ModifyData()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Add.Click
        Try
            Dim frm As New frmImplantableDevices(0, _PatientID)
            frm.Text = "Add Implantable Device"
            frm.ShowInTaskbar = False
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            ShowImplantableDevices()
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            SetNKImplantsVisibility()
        End Try
    End Sub
    Private Sub ModifyData()
        If C1PatientImplantableDevices.Rows.Count > 1 Then
            Dim TransactionID As Int64
            TransactionID = Convert.ToInt64(C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0))
            If TransactionID > 0 AndAlso C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 15) <> "Deleted" Then
                Dim blnRecordLock As Boolean = False

                Dim dt As DataTable  ''slr new not needed
                If gblnRecordLocking = True Then
                    dt = clsgeneral.Scan_n_Lock_FormLevel(_PatientID, 0, TransactionID, "Patient Implantable Devices")
                    If dt.Rows(0)("IsOpen") = 1 Then
                        If MessageBox.Show("This record is being modified by " & dt.Rows(0)("UserName").ToString & " on " & dt.Rows(0)("MachineName").ToString & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                            ''Record open only for view.
                            blnRecordLock = True
                        Else
                            If Not IsNothing(dt) Then
                                dt.Dispose()
                                dt = Nothing
                            End If
                            Exit Sub
                        End If
                    End If

                    If Not IsNothing(dt) Then
                        dt.Dispose()
                        dt = Nothing
                    End If
                End If
                Dim frm As New frmImplantableDevices(TransactionID, _PatientID, blnRecordLock)
                frm.Text = "Modify Implantable Device"
                frm.ShowInTaskbar = False
                frm.StartPosition = FormStartPosition.CenterParent
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                ShowImplantableDevices()

                'To refresh list after performing add/update/delete operation
                Dim rowIndex As Int64
                rowIndex = C1PatientImplantableDevices.FindRow(TransactionID, 1, 0, False, True, False)
                C1PatientImplantableDevices.Select(rowIndex, 0, True)


                frm.Dispose()
                frm = Nothing
            End If
        End If
    End Sub
    Private Sub tblbtn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Modify.Click
        Try
            ModifyData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Delete.Click
        Try
            If C1PatientImplantableDevices.Rows.Count > 1 Then
                If Convert.ToInt64(C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0)) > 0 Then
                    Dim frm As New frmAddNotes()
                    frm.Text = "Notes"
                    frm.ShowInTaskbar = False
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm._LabelCaption = "Reason for delete"
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    Dim strNotes = frm._Notes
                    frm.Dispose()
                    frm = Nothing

                    'If MessageBox.Show("Do you want to delete the selected Implantable Device record?   ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If strNotes <> "" Then
                        Dim clsIMTran As New clsgloImplantableDevicesTransaction

                        clsIMTran.TranctionID = Convert.ToInt64(C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0))
                        clsIMTran.PatientID = _PatientID

                        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
                        Dim oParam As gloDatabaseLayer.DBParameters
                        oDB.Connect(False)
                        oParam = New gloDatabaseLayer.DBParameters
                        oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParam.Add("@transaction_id", C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0), ParameterDirection.Input, SqlDbType.BigInt)
                        oParam.Add("@DeleteReason", strNotes, ParameterDirection.Input, SqlDbType.Text)
                        oParam.Add("@Status", "Delete", ParameterDirection.Input, SqlDbType.Text)
                        oDB.Execute("gsp_ChangeImplantDeviceStatus", oParam)

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Delete, "Patient implanatable device Record Deleted.", _PatientID, clsIMTran.TranctionID, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

                        oParam.Dispose()
                        oParam = Nothing
                        oDB.Dispose()
                        oDB = Nothing

                        ShowImplantableDevices()

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            SetNKImplantsVisibility()
        End Try
    End Sub

    Private Sub tblbtn_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Refresh.Click
        ShowImplantableDevices()
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
        ShowImplantableDevices()
        txtSearch.Focus()
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
            ShowImplantableDevices()
        End If
    End Sub

    Private Sub C1PatientImplantableDevices_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1PatientImplantableDevices.MouseDown
        Try


        ClearContextMenuStrip(cmnuDeviceStatus)
        If e.Button = MouseButtons.Right Then
            Dim nRow As Integer
                nRow = C1PatientImplantableDevices.HitTest(e.X, e.Y).Row
                If nRow > 0 AndAlso C1PatientImplantableDevices.GetData(nRow, 15) <> "Deleted" Then
                    If nRow <> C1PatientImplantableDevices.RowSel Then
                        C1PatientImplantableDevices.Select(nRow, 1)
                    End If
                    Dim oMenuItem As ToolStripMenuItem
                    oMenuItem = New ToolStripMenuItem
                    Dim oChildMenuItem As ToolStripMenuItem = Nothing
                    oMenuItem.Text = "Device Status"
                    oMenuItem.Tag = "DeviceStatus"
                    oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
                    'oMenuItem.Image = Imgts_PatientDetails.Images(28)
                    cmnuDeviceStatus.Items.Add(oMenuItem)
                    If C1PatientImplantableDevices.GetData(nRow, 15) = "Active" Then
                        oChildMenuItem = New ToolStripMenuItem()
                        oChildMenuItem.Text = "Inactive"
                        oChildMenuItem.Tag = "Inactive"

                        oMenuItem.DropDownItems.Add(oChildMenuItem)
                        AddHandler oChildMenuItem.Click, AddressOf UpdateStatusInactive
                        oChildMenuItem = Nothing
                    Else
                        oChildMenuItem = New ToolStripMenuItem()
                        oChildMenuItem.Text = "Active"
                        oChildMenuItem.Tag = "Active"

                        oMenuItem.DropDownItems.Add(oChildMenuItem)
                        AddHandler oChildMenuItem.Click, AddressOf UpdateStatusActive
                        oChildMenuItem = Nothing
                    End If
                    C1PatientImplantableDevices.ContextMenuStrip = cmnuDeviceStatus
                    AssignContextMenuStrip(C1PatientImplantableDevices.ContextMenuStrip, cmnuDeviceStatus)
                End If
            End If
        Catch ex As Exception

        Finally

        End Try
    End Sub
    Private Sub AssignContextMenuStrip(ByRef cmnuStrip As ContextMenuStrip, ByVal cSrcStrip As ContextMenuStrip)
        Try
            If (IsNothing(cmnuStrip) = False) Then
                cmnuStrip = Nothing
            End If

            cmnuStrip = cSrcStrip

        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateStatusInactive(sender As Object, e As EventArgs)
        Dim frm As New frmAddNotes()
        frm.Text = "Notes"
        frm.ShowInTaskbar = False
        frm.StartPosition = FormStartPosition.CenterParent
        frm._LabelCaption = "Reason for Inactive"
        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
        Dim strNotes = frm._Notes
        frm.Dispose()
        frm = Nothing

        'If MessageBox.Show("Do you want to delete the selected Implantable Device record?   ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        If strNotes <> "" Then

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@transaction_id", C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0), ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@DeleteReason", strNotes, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@Status", "InActive", ParameterDirection.Input, SqlDbType.Text)
            oDB.Execute("gsp_ChangeImplantDeviceStatus", oParam)

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, "Patient implanatable device status changed to Inactive.", _PatientID, C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0), gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

            oParam.Dispose()
            oParam = Nothing
            oDB.Dispose()
            oDB = Nothing

            ShowImplantableDevices()
        End If
    End Sub
    Private Sub UpdateStatusActive(sender As Object, e As EventArgs)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        oDB.Connect(False)
        oParam = New gloDatabaseLayer.DBParameters
        oParam.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
        oParam.Add("@transaction_id", C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0), ParameterDirection.Input, SqlDbType.BigInt)
        oParam.Add("@DeleteReason", "", ParameterDirection.Input, SqlDbType.Text)
        oParam.Add("@Status", "Active", ParameterDirection.Input, SqlDbType.Text)
        oDB.Execute("gsp_ChangeImplantDeviceStatus", oParam)

        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, "Patient implanatable device status changed to Active.", _PatientID, C1PatientImplantableDevices.GetData(C1PatientImplantableDevices.Row, 0), gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)

        oParam.Dispose()
        oParam = Nothing
        oDB.Dispose()
        oDB = Nothing

        ShowImplantableDevices()
    End Sub

    Private Sub C1PatientImplantableDevices_OwnerDrawCell(sender As Object, e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles C1PatientImplantableDevices.OwnerDrawCell
        If e.Row > 0 And (e.Col = 12 Or e.Col = 13) Then
            Dim value As Date = C1PatientImplantableDevices.GetData(e.Row, e.Col)
            If IsDate(value) Then
                If value.Year <= 1900 Then
                    e.Text = String.Empty
                End If
            End If
        End If
    End Sub

    Private Sub OptDelete_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles OptDelete.CheckedChanged
        If _isLoaded Then
            ShowImplantableDevices()
        End If
    End Sub
    Private Sub SetNKImplantsVisibility()
        Try
            If (C1PatientImplantableDevices.Rows.Count <= 1) Then
                tblbtn_NKImplants.Visible = True
            Else
                tblbtn_NKImplants.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tblbtn_NKImplants_Click(sender As Object, e As System.EventArgs) Handles tblbtn_NKImplants.Click
        Dim PatspecCDAspc As gloCCDLibrary.frmPatspecCDAspc
        PatspecCDAspc = New gloCCDLibrary.frmPatspecCDAspc()
        PatspecCDAspc.OpenfrmImplants = True
        PatspecCDAspc.patientID = _PatientID
        PatspecCDAspc.ShowDialog(Me)
        PatspecCDAspc.Dispose()
        PatspecCDAspc = Nothing
    End Sub

    Private Sub frmVWImplantableDevices_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        SetNKImplantsVisibility()
    End Sub
End Class
