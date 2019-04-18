Imports System.Data.SqlClient
Public Class frmCV_Criteria

    Dim _nCriteriaId As Int64 = 0
    Dim _sCriterianame As String = ""
    Private Const Col_MsgID As Integer = 0
    Private Const Col_CriteriaName As Integer = 1
    Private Const Col_Displaymsg As Integer = 2

    Private Sub frmCV_Criteria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Code Start added by kanchan on 20120102 for gloCommunity integration
        If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
            ts_gloCommunityDownload.Visible = gblngloCommunity
        End If
        'Code end added by kanchan on 20120102 for gloCommunity integration
        gloC1FlexStyle.Style(C1CV_Criteria)

        Designgrid()
    End Sub

    Private Sub Designgrid()


        Try
            Dim dt As DataTable

            txtSearch.Focus()
            dt = GetCV_Criteria()

            'C1CV_Criteria.DataSource = Nothing
            'C1CV_Criteria.Clear()
            C1CV_Criteria.DataSource = Nothing
            If dt IsNot Nothing Then
                'If dt.Rows.Count > 0 Then
                Dim _dv As DataView = dt.Copy().DefaultView
                C1CV_Criteria.Visible = True

                C1CV_Criteria.DataSource = _dv
                C1CV_Criteria.Rows.Fixed = 1


                'C1CV_Criteria.DataSource = _dv
                'C1CV_Criteria.Rows.Fixed = 1
                'C1CV_Criteria.Rows.Count = 1
                'C1CV_Criteria.Cols.Count = 3

                'C1CV_Criteria.SetData(0, Col_MsgID, "ID")
                'C1CV_Criteria.SetData(0, Col_CriteriaName, "CriteriaName")
                'C1CV_Criteria.SetData(0, Col_Displaymsg, "Message")

                C1CV_Criteria.Cols(0).Caption = "ID"
                C1CV_Criteria.Cols(1).Caption = "Criteria Name"
                C1CV_Criteria.Cols(2).Caption = "Message"


                C1CV_Criteria.Cols(0).Visible = False
                C1CV_Criteria.Cols(1).Visible = True
                C1CV_Criteria.Cols(2).Visible = True

                C1CV_Criteria.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1CV_Criteria.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1CV_Criteria.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                Dim nWidth As Integer = Pnl_grid.Width

                C1CV_Criteria.Cols(Col_MsgID).Width = 0
                C1CV_Criteria.Cols(Col_CriteriaName).Width = CInt((0.49 * (nWidth)))
                C1CV_Criteria.Cols(Col_Displaymsg).Width = CInt((0.5 * (nWidth)))

                'For i As Integer = 0 To dt.Rows.Count - 1
                '    Dim RowIndex As Int32 = C1CV_Criteria.Rows.Count
                '    C1CV_Criteria.Rows.Add()
                '    C1CV_Criteria.SetData(RowIndex, Col_MsgID, dt.Rows(i)("ID"))
                '    C1CV_Criteria.SetData(RowIndex, Col_CriteriaName, dt.Rows(i)("CriteriaName"))
                '    C1CV_Criteria.SetData(RowIndex, Col_Displaymsg, dt.Rows(i)("DisplayMessage"))

                'Next
                C1CV_Criteria.ShowCellLabels = True
                C1CV_Criteria.AllowEditing = False

                dt.Dispose()
                dt = Nothing
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetCV_Criteria() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim sQLQuery As String = ""
        Try
            sQLQuery = " SELECT ISNULL(cv_mst_Id,0) AS ID, ISNULL(cv_mst_CriteriaName,'') AS CriteriaName,ISNULL(cv_mst_DisplayMessage,'') AS DisplayMessage  FROM  CV_Criteria_MST"

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

                        Dim oCVsetup As New frmCV_Setup
                        oCVsetup.ShowDialog(IIf(IsNothing(oCVsetup.Parent), Me, oCVsetup.Parent))
                        Designgrid()
                        oCVsetup.Dispose()

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "CardioVascular Criteria not saved", gloAuditTrail.ActivityOutCome.Failure)
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Could not Save record in Cardiovascular Setup", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case "Modify"
                    Try
                        'Added by Mayuri:20091007
                        Me.Cursor = Cursors.WaitCursor
                        'End Code Added by Mayuri:20091007
                        If (C1CV_Criteria.Rows.Count > 1) Then
                            'Me.Cursor = Cursors.WaitCursor
                            _nCriteriaId = Convert.ToInt64(C1CV_Criteria.GetData(C1CV_Criteria.RowSel, Col_MsgID))
                            _sCriterianame = Convert.ToString(C1CV_Criteria.GetData(C1CV_Criteria.RowSel, Col_CriteriaName))
                            Dim oCVsetup As New frmCV_Setup(True, _nCriteriaId, _sCriterianame)
                            oCVsetup.ShowDialog(IIf(IsNothing(oCVsetup.Parent), Me, oCVsetup.Parent))
                            Designgrid()
                            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "CardioVascular Record Modified", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''Added Rahul P on 20101011
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "CardioVascular Record Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''
                            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Modified Record in Cardiovascular Setup", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                            oCVsetup.Dispose()
                        End If
                    Catch ex As Exception
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "CardioVascular Record not Modified", gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "CardioVascular Record not Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Could not modify record in Cardiovascular Setup", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Case "Delete"
                    If (C1CV_Criteria.Rows.Count > 1) Then
                        If MessageBox.Show("Are you sure you want to delete this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            _nCriteriaId = Convert.ToInt64(C1CV_Criteria.GetData(C1CV_Criteria.RowSel, Col_MsgID))
                            _sCriterianame = Convert.ToInt64(C1CV_Criteria.GetData(C1CV_Criteria.RowSel, Col_MsgID))
                            DeleteCV_Criteria(_nCriteriaId)
                            Designgrid()
                        End If
                    End If
                Case "Refresh"
                    Me.Cursor = Cursors.WaitCursor
                    Designgrid()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, "CardioVascular Criteria List Refreshed", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, "CardioVascular Criteria List Refreshed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Refereshed Record in Cardiovascular Criteria", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                Case "Close"
                    Me.Close()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "View CardioVascular Closed", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "View CardioVascular Closed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed the Cardiovascular Criteria form", gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try



    End Sub

    Private Sub DeleteCV_Criteria(ByVal Id As Int64)

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "DELETE  FROM  CV_Criteria_MST WHERE  cv_mst_Id = " & Id & ""
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                objCmd.CommandText = "DELETE  FROM  CV_Criteria_DTL WHERE  cv_mst_Id = " & Id & ""
                If (objCmd.ExecuteNonQuery() > 0) Then
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Cardio Vascular Criteria Deleted", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Cardio Vascular Criteria Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "CV criteria deleted.  ", gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                End If
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "CardioVascular Criteria not deleted", gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "CV criteria not deleted.  ", gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Failure, gstrMessageBoxCaption)
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

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As DataView = DirectCast(C1CV_Criteria.DataSource, DataView)
        ' _dv = CType(C1CV_Criteria.DataSource, DataView)
        'C1CV_Criteria.DataSource = _dv
        If (IsNothing(_dv) = False) Then


            Try
                Dim strSearch As String = txtSearch.Text.Trim()

                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")


                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns("CriteriaName").ColumnName & " Like '%") + strSearch & "%'"
                Else
                    'commented by Shubhangi 20091005 B'coz we want in string search(still we not enter % or *  'coz that was previous requirement)
                    ' _dv.RowFilter = (_dv.Table.Columns("CriteriaName").ColumnName & " Like '") + strSearch & "%'"
                    _dv.RowFilter = (_dv.Table.Columns("CriteriaName").ColumnName & " Like '%") + strSearch & "%' OR " _
                            & (_dv.Table.Columns("DisplayMessage").ColumnName & " Like '%") + strSearch & "%'"

                End If
                C1CV_Criteria.DataSource = _dv
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Searched Cardio vascular criteria having substring  " & txtSearch.Text.Trim, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Searched Cardio vascular criteria having substring  " & txtSearch.Text.Trim, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Cardio vascular criteria having substring  " & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)

            Catch Ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Could not search Cardio vascular criteria having substring  " & txtSearch.Text.Trim, gloAuditTrail.ActivityOutCome.Failure)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Cardio vascular criteria having substring  " & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                MessageBox.Show(Ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub Pnl_grid_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pnl_grid.Resize
        Dim nWidth As Integer = Pnl_grid.Width

        C1CV_Criteria.Cols(Col_MsgID).Width = 0
        C1CV_Criteria.Cols(Col_CriteriaName).Width = CInt((0.49 * (nWidth)))
        C1CV_Criteria.Cols(Col_Displaymsg).Width = CInt((0.5 * (nWidth)))

    End Sub

    Private Sub C1CV_Criteria_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1CV_Criteria.DoubleClick
       
    End Sub

    Private Sub C1CV_Criteria_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Criteria.MouseDoubleClick
        Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1CV_Criteria.HitTest(e.X, e.Y)
        If (hti.Column = 1 AndAlso hti.Row = -1 OrElse hti.Row = 0) Then
            C1CV_Criteria.Row = -1
        End If
        If (C1CV_Criteria.Row = -1) Then
            Return
        End If
        Try
            If (C1CV_Criteria.Rows.Count > 1) Then

                _nCriteriaId = Convert.ToInt64(C1CV_Criteria.GetData(C1CV_Criteria.RowSel, Col_MsgID))
                _sCriterianame = Convert.ToString(C1CV_Criteria.GetData(C1CV_Criteria.RowSel, Col_CriteriaName))
                Dim oCVsetup As New frmCV_Setup(True, _nCriteriaId, _sCriterianame)
                oCVsetup.ShowDialog(IIf(IsNothing(oCVsetup.Parent), Me, oCVsetup.Parent))
                Designgrid()
                oCVsetup.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1CV_Criteria_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Criteria.MouseMove
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
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(sender As System.Object, e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmCVSetupDown As New gloCommunity.Forms.gloCommunityViewData("CVSetup", "Download")
            'code added by seema on 20120221 to prevent opening of multiple windows
            If gloCommunity.Classes.clsGeneral.getInstance(FrmCVSetupDown.Name, FrmCVSetupDown.Text) = False Then
                Try

                    With FrmCVSetupDown
                        .MdiParent = Application.OpenForms("MainMenu")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                Catch objErr As Exception
                    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ''Added for fixed Bug # : 35658 on 20120904
    Private Function CheckUser() As Boolean
        Dim oCommunity As gloCommunity.Classes.clsgloCommunityUsers = Nothing
        Dim _blnUserCheck As Boolean = False
        Try
            oCommunity = New gloCommunity.Classes.clsgloCommunityUsers()
            _blnUserCheck = oCommunity.CheckAuthentication()
        Catch ex As Exception
        Finally
            If Not IsNothing(oCommunity) Then
                oCommunity = Nothing
            End If
        End Try
        Return _blnUserCheck
    End Function
    ''End
End Class