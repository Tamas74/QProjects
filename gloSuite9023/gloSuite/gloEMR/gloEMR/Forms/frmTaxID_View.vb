Imports System.Data.SqlClient
Public Class frmTaxID_View

    Dim nTaxMasterID As Int64 = 0
    Dim sTaxID As String = ""
    Private Const Col_ID As Integer = 0
    Private Const Col_TIN As Integer = 1
    Private Const Col_TITAL As Integer = 2
    Private Const Col_IsActive As Integer = 3
    Private Const Col_USERNAME As Integer = 4
    Private Const Col_CREATEDDATE As Integer = 5
    Private Const Col_MODIFIEDDATE As Integer = 6

    Public Enum ActiveStatus
        InActive = 0
        Active = 1
    End Enum



    Private Sub frmTaxID_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        gloC1FlexStyle.Style(C1TaxID)
        Designgrid()
    End Sub

    Private Sub Designgrid()


        Try
            Dim dt As DataTable

            txtSearch.Focus()
            dt = GetTaxIDs()

            C1TaxID.DataSource = Nothing
            If dt IsNot Nothing Then

                Dim _dv As DataView = dt.Copy().DefaultView
                C1TaxID.Visible = True

                C1TaxID.DataSource = _dv
                C1TaxID.Rows.Fixed = 1


                C1TaxID.Cols(Col_ID).Caption = "ID"
                C1TaxID.Cols(Col_TIN).Caption = "Tax Identifier Number"
                C1TaxID.Cols(Col_TITAL).Caption = "Title"
                C1TaxID.Cols(Col_IsActive).Caption = "Status"
                C1TaxID.Cols(Col_USERNAME).Caption = "User Name"
                C1TaxID.Cols(Col_CREATEDDATE).Caption = "Created Date"
                C1TaxID.Cols(Col_MODIFIEDDATE).Caption = "Modified Date"

                C1TaxID.Cols(Col_ID).Visible = False
                C1TaxID.Cols(Col_TIN).Visible = True
                C1TaxID.Cols(Col_TITAL).Visible = True
                C1TaxID.Cols(Col_IsActive).Visible = True
                C1TaxID.Cols(Col_USERNAME).Visible = True
                C1TaxID.Cols(Col_CREATEDDATE).Visible = True
                C1TaxID.Cols(Col_MODIFIEDDATE).Visible = True
                ''C1TaxID.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_ID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_TIN).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_TITAL).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_IsActive).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_USERNAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_CREATEDDATE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1TaxID.Cols(Col_MODIFIEDDATE).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                Dim nWidth As Integer = Pnl_grid.Width

                C1TaxID.Cols(Col_ID).Width = 0
                C1TaxID.Cols(Col_TIN).Width = CInt((0.2 * (nWidth)))
                C1TaxID.Cols(Col_TITAL).Width = CInt((0.4 * (nWidth)))
                C1TaxID.Cols(Col_USERNAME).Width = CInt((0.09 * (nWidth)))
                C1TaxID.Cols(Col_CREATEDDATE).Width = CInt((0.1 * (nWidth)))
                C1TaxID.Cols(Col_MODIFIEDDATE).Width = CInt((0.1 * (nWidth)))
                C1TaxID.Cols(Col_MODIFIEDDATE).DataType = GetType(DateTime)
                C1TaxID.Cols(Col_MODIFIEDDATE).Format = "MM/dd/yyyy"
                C1TaxID.Cols(Col_CREATEDDATE).DataType = GetType(DateTime)
                C1TaxID.Cols(Col_CREATEDDATE).Format = "MM/dd/yyyy"
                C1TaxID.Cols(Col_IsActive).DataType = GetType(String)

                ''C1TaxID.ShowCellLabels = True
                C1TaxID.AllowEditing = False

                dt.Dispose()
                dt = Nothing
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetTaxIDs() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim sQLQuery As String = ""
        Try
            sQLQuery = " SELECT TINMaster.nTINMasterID AS nTinMasterID,ISNULL(TINMaster.sTIN,'') AS  sTIN, ISNULL(TINMaster.sTINTitle,'') AS Tital, CASE WHEN ISNULL(TINMaster.bIsActive,0)=0 THEN 'InActive' ELSE 'Active' END AS Status,ISNULL(ls.sLoginName,'') AS UserName,CreatedDate AS CreatedDate ,dtModifiedDate AS ModifiedDate  " &
                       " FROM [dbo].[TINMaster] WITH(NOLOCK)  LEFT OUTER JOIN dbo.LoginSession ls WITH(NOLOCK) ON ls.LoginSessionID = dbo.TINMaster.LoginSessionID WHERE ISNULL(dbo.TINMaster.bIsDeleted,0)=0 Order By ISNULL(TINMaster.bIsActive,0) desc , CreatedDate desc"


            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon
            Dim da As SqlDataAdapter
            Dim dt As DataTable
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            Return dt
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try

            Select Case e.ClickedItem.Tag
                Case "Add"
                    Try

                        Me.Cursor = Cursors.WaitCursor


                        Dim oTaxIDSetup As New frmTINMaster(GetConnectionString(), 0, gintLoginSessionID)
                        oTaxIDSetup.ShowDialog(Me)
                        'Dim oCVsetup As New frmCV_Setup
                        'oCVsetup.ShowDialog(IIf(IsNothing(oCVsetup.Parent), Me, oCVsetup.Parent))
                        Designgrid()
                        'oCVsetup.Dispose()

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Tax ID not saved", gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case "Modify"
                    ModifyTaxID()
                Case "Delete"
                    If (C1TaxID.Rows.Count > 1 AndAlso C1TaxID.RowSel > 0) Then
                        nTaxMasterID = Convert.ToInt64(C1TaxID.GetData(C1TaxID.RowSel, Col_ID))
                        sTaxID = Convert.ToString(C1TaxID.GetData(C1TaxID.RowSel, Col_TIN))
                        If (Not IsTinInTransaction(nTaxMasterID)) Then
                            If MessageBox.Show("Are you sure you want to delete this record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                DeleteTaxID(nTaxMasterID, sTaxID)
                                Designgrid()
                            End If
                        Else
                            MessageBox.Show("Selected record is associated with provider(s) transaction, you can not delete this record. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    End If
                Case "Refresh"
                    Me.Cursor = Cursors.WaitCursor
                    Designgrid()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, "Tax ID List Refreshed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                Case "Close"
                    Me.Close()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Tax ID List Closed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try



    End Sub

    Private Sub DeleteTaxID(ByVal Id As Int64, ByVal sTaxID As String)

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "delete dbo.TINMaster WHERE dbo.TINMaster.nTINMasterID= " & Id & ""
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                objCmd.CommandText = "delete dbo.TINProviderAssociation WHERE dbo.TINProviderAssociation.nTINMasterID= " & Id & ""
                If (objCmd.ExecuteNonQuery() > 0) Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.ProviderMultipleTaxIDView, gloAuditTrail.ActivityType.Delete, "Tax ID='" & sTaxID & "' Deleted", 0, nTaxMasterID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.ProviderMultipleTaxIDView, gloAuditTrail.ActivityType.Delete, "Tax ID='" & sTaxID & "' Deleted", 0, nTaxMasterID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception

        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Sub
    Private Function IsTinInTransaction(ByVal nTinMasterID) As Boolean
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim oResult As Object

        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "SELECT t.nTINMasterID FROM dbo.TINMaster t WITH (NOLOCK) " &
                                 "INNER JOIN dbo.TINProviderAssociation ta WITH (NOLOCK) ON ta.nTINMasterID = t.nTINMasterID " &
                                 "INNER JOIN dbo.TINTransaction t2 WITH (NOLOCK) ON t2.nAssociationID = ta.nAssociationID AND t2.nProviderID = ta.nProviderID " &
                                 "WHERE t.nTINMasterID = " & nTinMasterID & ""

            objCmd.Connection = objCon
            oResult = objCmd.ExecuteScalar()
            If oResult IsNot Nothing AndAlso Convert.ToInt64(oResult) Then
                Return True
            Else
                Return False
            End If


        Catch gex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), True)
            Return False
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As DataView = DirectCast(C1TaxID.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            Try
                Dim strSearch As String = txtSearch.Text.Trim()

                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")


                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns(Col_TIN).ColumnName & " Like '%") + strSearch & "%'"
                Else
                    _dv.RowFilter = (_dv.Table.Columns(Col_TIN).ColumnName & " Like '%") + strSearch & "%' OR " _
                            & (_dv.Table.Columns(Col_TITAL).ColumnName & " Like '%") + strSearch & "%'"

                End If
                C1TaxID.DataSource = _dv
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Searched Tax ID criteria having substring  " & txtSearch.Text.Trim, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Catch Ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Could not search Tax ID criteria having substring  " & txtSearch.Text.Trim, gloAuditTrail.ActivityOutCome.Failure)

                MessageBox.Show(Ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub Pnl_grid_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pnl_grid.Resize
        Dim nWidth As Integer = Pnl_grid.Width

        C1TaxID.Cols(Col_ID).Width = 0
        C1TaxID.Cols(Col_TIN).Width = CInt((0.2 * (nWidth)))
        C1TaxID.Cols(Col_TITAL).Width = CInt((0.4 * (nWidth)))
        C1TaxID.Cols(Col_USERNAME).Width = CInt((0.09 * (nWidth)))
        C1TaxID.Cols(Col_CREATEDDATE).Width = CInt((0.1 * (nWidth)))
        C1TaxID.Cols(Col_MODIFIEDDATE).Width = CInt((0.1 * (nWidth)))

    End Sub

    Private Sub C1CV_Criteria_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1TaxID.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub C1TaxID_RowColChange(sender As System.Object, e As System.EventArgs) Handles C1TaxID.RowColChange
        Dim _sStatus As String
        If C1TaxID.RowSel > 0 Then

            If IsNothing(C1TaxID.GetData(C1TaxID.RowSel, Col_IsActive)) = False Then
                _sStatus = C1TaxID.GetData(C1TaxID.RowSel, Col_IsActive).ToString()
                If _sStatus.ToUpper() = "ACTIVE" Then
                    tsbtn_Act_Deact_Rule.Text = "De-&activate"
                    tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
                Else
                    tsbtn_Act_Deact_Rule.Text = "&Activate"
                    tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
                End If
            End If
        End If
    End Sub

    Private Sub tsbtn_Act_Deact_Rule_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_Act_Deact_Rule.Click
        PerformActivateDeActivate()
    End Sub
    Private Sub PerformActivateDeActivate()

        Dim _SelectedTaxId As Int64 = 0
        Dim _SelectedTIN As String = ""
        Dim _nSelectedRowIndex As Integer = -1
        Dim _sActivationDeActivationNote As String = ""

        Try
            If Not IsNothing(C1TaxID) AndAlso C1TaxID.Rows.Count > 0 AndAlso C1TaxID.RowSel > 0 Then
                With C1TaxID
                    _SelectedTaxId = .GetData(.RowSel, Col_ID)
                    _SelectedTIN = .GetData(.RowSel, Col_TIN)
                    If _SelectedTaxId > 0 Then

                        If tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then
                            If Not MessageBox.Show("Are you sure you want to de-activate the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If



                        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                            If Not MessageBox.Show("Are you sure you want to activate the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If


                        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                            If UpdateActiveStatus(_SelectedTaxId, _SelectedTIN, ActiveStatus.Active.GetHashCode()) > 0 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.ProviderMultipleTaxIDView, gloAuditTrail.ActivityType.Modify, "'" & _SelectedTIN & "' successfully activated", 0, _SelectedTaxId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If

                        Else
                            If UpdateActiveStatus(_SelectedTaxId, _SelectedTIN, ActiveStatus.InActive.GetHashCode()) > 0 Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.ProviderMultipleTaxIDView, gloAuditTrail.ActivityType.Modify, "'" & _SelectedTIN & "' successfully De-activated", 0, _SelectedTaxId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If

                        End If
                        Designgrid()
                        ''_nSelectedRowIndex = .RowSel
                        _nSelectedRowIndex = .FindRow(_SelectedTaxId.ToString(), 0, 0, False, False, False)

                        If (_nSelectedRowIndex <> -1 And _nSelectedRowIndex > 0) Then
                            .RowSel = _nSelectedRowIndex
                            .Select(_nSelectedRowIndex, 0)
                        End If
                    End If

                End With

                'If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then

                '    tsbtn_Act_Deact_Rule.Text = "De-&activate"
                '    tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                '    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate

                'ElseIf tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then

                '    tsbtn_Act_Deact_Rule.Text = "&Activate"
                '    tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                '    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate

                'End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Function UpdateActiveStatus(ByVal Id As Int64, ByVal sTaxID As String, ByVal ActiveStatus As Integer) As Integer

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim nResult As Integer = 0
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "UPDATE dbo.TINMaster SET  dbo.TINMaster.bIsActive = " & ActiveStatus & ",  dbo.TINMaster.LoginSessionID = dbo.Get_AuditSessionID(),  dbo.TINMaster.CreatedDate = dbo.gloGetDate() WHERE dbo.TINMaster.nTINMasterID= " & Id & ""
            objCmd.Connection = objCon
            nResult = objCmd.ExecuteNonQuery()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.ProviderMultipleTaxIDView, gloAuditTrail.ActivityType.Modify, "'" & sTaxID & "' successfully activated", 0, Id, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            nResult = 0
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return nResult
    End Function

    Private Sub C1TaxID_DoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1TaxID.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1TaxID.HitTest(ptPoint)
        With C1TaxID
            If .Rows.Count > 1 And htInfo.Row > 0 Then
                ModifyTaxID()
            End If
        End With
       
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ModifyTaxID()
        Try

            Me.Cursor = Cursors.WaitCursor
            If (C1TaxID.Rows.Count > 1) Then
                nTaxMasterID = Convert.ToInt64(C1TaxID.GetData(C1TaxID.RowSel, Col_ID))
                sTaxID = Convert.ToString(C1TaxID.GetData(C1TaxID.RowSel, Col_TIN))
                Dim oTaxIDSetup As New frmTINMaster(GetConnectionString(), nTaxMasterID, gintLoginSessionID, True, IsTinInTransaction(nTaxMasterID))
                oTaxIDSetup.ShowDialog(Me)
                Designgrid()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Tax ID ='" & sTaxID & "' Record Modified", 0, nTaxMasterID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProviderMultipleTaxID, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Tax ID ='" & sTaxID & "' Record not Modified", 0, nTaxMasterID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class