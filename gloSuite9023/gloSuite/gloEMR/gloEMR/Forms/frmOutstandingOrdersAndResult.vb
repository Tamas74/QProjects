Imports gloEMR.gloEMRWord
Imports C1.Win.C1FlexGrid
Imports gloAuditTrail

Public Class frmOutstandingOrdersAndResult

    Dim dtProviderList As New DataTable()
    Dim dtOrderStatusList As New DataTable()

    Dim UnsentOrdersrowIndex As Integer
    Dim UnResultedOrdersrowIndex As Integer
    Dim UnAcknowledgedOrdersrowIndex As Integer
    Dim AllOrderOrdersrowIndex As Integer
    Dim UnfinishedOrderTemplatesrowIndex As Integer

    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _nLoginProviderId As Long = 0
    Dim _PatientID As Long

    ''variable needed for passing parameter from form to report.
    Dim sTabName As String = ""
    Dim nProviderId As Long = 0
    Dim sDateCategory As String = ""
    Dim dtFromdate As Date
    Dim dtTodate As Date
    Dim nOrderStatusNumber As Integer = 0
    Dim IsResultedOrderDate As Boolean = False

#Region "Form Load and Constructor"

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        InitializeComponent()
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If
        _PatientID = PatientID
    End Sub

    Private Sub frmOutstandingOrdersAndResult_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FillproviderList()
            FillOrderStatusList()
            If (cmbUnsentOrdersDate.Items.Count > 0) Then
                cmbUnsentOrdersDate.SelectedIndex = 0
            End If
            'cmbUnsentOrdersDate.SelectedIndex = 0
            If (cmbUnresultedDate.Items.Count > 1) Then
                cmbUnresultedDate.SelectedIndex = 1
            Else
                If (cmbUnresultedDate.Items.Count > 0) Then
                    cmbUnresultedDate.SelectedIndex = 0
                End If
            End If
            'cmbUnresultedDate.SelectedIndex = 1
            If (cmbUnacknowledgedDate.Items.Count > 1) Then
                cmbUnacknowledgedDate.SelectedIndex = 1
            Else
                If (cmbUnacknowledgedDate.Items.Count > 0) Then
                    cmbUnacknowledgedDate.SelectedIndex = 0
                End If
            End If
            'cmbUnacknowledgedDate.SelectedIndex = 1
            rdoResultedOrderDate.Checked = True
            If (cmbAllOrderDate.Items.Count > 0) Then
                cmbAllOrderDate.SelectedIndex = 0
            End If
            ' cmbAllOrderDate.SelectedIndex = 0
            If (cmbUnfinishedOrderDate.Items.Count > 0) Then
                cmbUnfinishedOrderDate.SelectedIndex = 0
            End If
            'cmbUnfinishedOrderDate.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Unsent Orders"

    Private Function RetrieveUnsentOrdersData() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Flag", "UnsentOrders", ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@ProviderID", cmbProviders.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@DateCategory", cmbUnsentOrdersDate.Text, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@Fromdate", dtUnsentOrdersFrom.Value, ParameterDirection.Input, SqlDbType.Date)
            oParam.Add("@Todate", dtUnsentOrdersTo.Value, ParameterDirection.Input, SqlDbType.Date)
            oDB.Retrive("gsp_GetOutstandingOrders", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub FillGridUnsentOrders()
        Try
            Dim dt As DataTable = RetrieveUnsentOrdersData()

            GridUnsentOrders.DataSource = dt.Copy().DefaultView
            FilterUnsentRecord()

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterUnsentRecord()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dv As DataView = CType(GridUnsentOrders.DataSource, DataView)

            If IsNothing(dv) Then
                Exit Sub
            Else
                GridUnsentOrders.DataSource = dv
            End If

            Dim dt As DataTable = dv.Table

            If dt.Rows.Count > 0 Then
                Dim strPatientSearchDetails As String
                If Trim(txtSearchUnsentOrders.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearchUnsentOrders.Text, "'", "''")
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                dt.DefaultView.RowFilter = " Provider Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientFirstName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientMiddleName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientLastName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderDateFilter Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderStatus Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Test Like '%" & strPatientSearchDetails & "%'"
                strPatientSearchDetails = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridUnsentOrders_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnsentOrders.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = GridUnsentOrders.HitTest(e.X, e.Y)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                Dim PatientID As Long = 0
                Dim nVisitId As Long = 0
                Dim OrderNoPrefix As String = ""
                PatientID = Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "PatientID"))
                nVisitId = Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "nVisitId"))
                OrderNoPrefix = GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "OrderNoPrefix")
                OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                OrderNoPrefix = Nothing
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbUnsentOrdersDate_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbUnsentOrdersDate.SelectedValueChanged
        Try
            If cmbUnsentOrdersDate.Text = "All Time" Then
                dtUnsentOrdersFrom.Visible = False
                dtUnsentOrdersTo.Visible = False
                lblUnsentOrdersFrom.Visible = False
                lblUnsentOrdersTo.Visible = False
            Else
                dtUnsentOrdersFrom.Visible = True
                dtUnsentOrdersTo.Visible = True
                lblUnsentOrdersFrom.Visible = True
                lblUnsentOrdersTo.Visible = True
                SetFromAndToDate(cmbUnsentOrdersDate.Text, "Unsent Orders")
            End If
            FillGridUnsentOrders()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtUnsentOrdersFrom_ValueChanged(sender As Object, e As System.EventArgs) Handles dtUnsentOrdersFrom.ValueChanged
        Try
            FillGridUnsentOrders()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtUnsentOrdersTo_ValueChanged(sender As Object, e As System.EventArgs) Handles dtUnsentOrdersTo.ValueChanged
        Try
            FillGridUnsentOrders()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearchUnsentOrders_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearchUnsentOrders.TextChanged
        FilterUnsentRecord()
    End Sub

    Private Sub btnClearUnsentOrdersSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearUnsentOrdersSearch.Click
        txtSearchUnsentOrders.Text = ""
    End Sub

    Private Sub GridUnsentOrders_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnsentOrders.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub GridUnsentOrders_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnsentOrders.MouseClick
        Try
            If GridUnsentOrders.Rows.Count > 1 Then
                Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnsentOrders.DataSource), CurrencyManager)
                Dim dr As DataRowView = TryCast(cm.Current, DataRowView)
                UnsentOrdersrowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)
                cm = Nothing
                dr = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridUnsentOrders_AfterSort(sender As System.Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles GridUnsentOrders.AfterSort
        Try
            If GridUnsentOrders.Rows.Count > 1 Then
                If UnsentOrdersrowIndex > -1 Then
                    For Each rw As C1.Win.C1FlexGrid.Row In GridUnsentOrders.Rows
                        Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnsentOrders.DataSource), CurrencyManager)
                        Dim dr As DataRowView = TryCast(rw.DataSource, DataRowView)
                        If dr IsNot Nothing Then
                            Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)

                            If currIndex = UnsentOrdersrowIndex Then
                                Dim cr As CellRange = GridUnsentOrders.GetCellRange(rw.Index, 1)
                                ' to scroll the selected row in the visible area
                                GridUnsentOrders.[Select](cr, True)
                                cr = GridUnsentOrders.GetCellRange(rw.Index, 0, rw.Index, GridUnsentOrders.Cols.Count - 1)
                                GridUnsentOrders.[Select](cr, False)
                                Exit For
                            End If
                        End If
                        cm = Nothing
                        dr = Nothing
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Unresulted Orders"

    Private Function RetrieveUnResultedOrderData() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Flag", "Unresulted", ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@ProviderID", cmbProviders.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@DateCategory", cmbUnresultedDate.Text, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@Fromdate", dtUnresultedFrom.Value, ParameterDirection.Input, SqlDbType.Date)
            oParam.Add("@Todate", dtUnresultedTo.Value, ParameterDirection.Input, SqlDbType.Date)
            oDB.Retrive("gsp_GetOutstandingOrders", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub FillGridUnResultedOrder()
        Try

            Dim dt As DataTable = RetrieveUnResultedOrderData()

            GridUnResultedOrders.DataSource = dt.Copy().DefaultView
            FilterUnresultedOrder()

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterUnresultedOrder()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dv As DataView = CType(GridUnResultedOrders.DataSource, DataView)

            If IsNothing(dv) Then
                Exit Sub
            Else
                GridUnResultedOrders.DataSource = dv
            End If

            Dim dt As DataTable = dv.Table

            If dt.Rows.Count > 0 Then
                Dim strPatientSearchDetails As String
                If Trim(txtSearchUnresulted.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearchUnresulted.Text, "'", "''")
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                dt.DefaultView.RowFilter = " Provider Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientFirstName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientMiddleName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientLastName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderDateFilter Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Test Like '%" & strPatientSearchDetails & "%'"
                strPatientSearchDetails = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridUnResultedOrders_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnResultedOrders.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = GridUnResultedOrders.HitTest(e.X, e.Y)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                Dim PatientID As Long = 0
                Dim nVisitId As Long = 0
                Dim OrderNoPrefix As String = ""
                PatientID = Convert.ToInt64(GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "PatientID"))
                nVisitId = Convert.ToInt64(GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "nVisitId"))
                OrderNoPrefix = GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "OrderNoPrefix")
                OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                OrderNoPrefix = Nothing
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbUnresultedDate_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbUnresultedDate.SelectedValueChanged
        Try
            If cmbUnresultedDate.Text = "120 + Days" Then
                dtUnresultedFrom.Visible = False
                dtUnresultedTo.Visible = False
                lblUnresultedFrom.Visible = False
                lblUnresultedTo.Visible = False
            Else
                dtUnresultedFrom.Visible = True
                dtUnresultedTo.Visible = True
                lblUnresultedFrom.Visible = True
                lblUnresultedTo.Visible = True
                SetFromAndToDate(cmbUnresultedDate.Text, "Unresulted")
            End If
            FillGridUnResultedOrder()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtUnresultedFrom_ValueChanged(sender As Object, e As System.EventArgs) Handles dtUnresultedFrom.ValueChanged
        FillGridUnResultedOrder()
    End Sub

    Private Sub dtUnresultedTo_ValueChanged(sender As Object, e As System.EventArgs) Handles dtUnresultedTo.ValueChanged
        FillGridUnResultedOrder()
    End Sub

    Private Sub txtSearchUnresulted_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearchUnresulted.TextChanged
        FilterUnresultedOrder()
    End Sub

    Private Sub btnClearUnresultedSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearUnresultedSearch.Click
        txtSearchUnresulted.Text = ""
    End Sub

    Private Sub GridUnResultedOrders_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnResultedOrders.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub GridUnResultedOrders_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnResultedOrders.MouseClick
        Try
            If GridUnResultedOrders.Rows.Count > 1 Then
                Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnResultedOrders.DataSource), CurrencyManager)
                Dim dr As DataRowView = TryCast(cm.Current, DataRowView)
                UnResultedOrdersrowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)
                cm = Nothing
                dr = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridUnResultedOrders_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles GridUnResultedOrders.AfterSort
        Try
            If GridUnResultedOrders.Rows.Count > 1 Then
                If UnResultedOrdersrowIndex > -1 Then
                    For Each rw As C1.Win.C1FlexGrid.Row In GridUnResultedOrders.Rows
                        Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnResultedOrders.DataSource), CurrencyManager)
                        Dim dr As DataRowView = TryCast(rw.DataSource, DataRowView)
                        If dr IsNot Nothing Then
                            Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)

                            If currIndex = UnResultedOrdersrowIndex Then
                                Dim cr As CellRange = GridUnResultedOrders.GetCellRange(rw.Index, 1)
                                ' to scroll the selected row in the visible area
                                GridUnResultedOrders.[Select](cr, True)
                                cr = GridUnResultedOrders.GetCellRange(rw.Index, 0, rw.Index, GridUnResultedOrders.Cols.Count - 1)
                                GridUnResultedOrders.[Select](cr, False)
                                Exit For
                            End If
                        End If
                        cm = Nothing
                        dr = Nothing
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Unacknowledged Orders"

    Private Function RetrieveUnAcknowledgedOrderData() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Flag", "Unacknowledged", ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@ProviderID", cmbProviders.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@DateCategory", cmbUnacknowledgedDate.Text, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@Fromdate", dtUnacknowledgedFrom.Value, ParameterDirection.Input, SqlDbType.Date)
            oParam.Add("@Todate", dtUnacknowledgedTo.Value, ParameterDirection.Input, SqlDbType.Date)

            Dim blnvalue As Boolean
            If rdoResultedOrderDate.Checked = True Then
                blnvalue = True
            Else
                blnvalue = False
            End If

            oParam.Add("@IsResultedOrderDate", blnvalue, ParameterDirection.Input, SqlDbType.Bit)
            oDB.Retrive("gsp_GetOutstandingOrders", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub FillGridUnacknowledgedOrder()
        Try
            Dim dt As DataTable = RetrieveUnAcknowledgedOrderData()

            GridUnAcknowledgedOrders.DataSource = dt.Copy().DefaultView
            FilterUnacknowledgedOrder()

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterUnacknowledgedOrder()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dv As DataView = CType(GridUnAcknowledgedOrders.DataSource, DataView)

            If IsNothing(dv) Then
                Exit Sub
            Else
                GridUnAcknowledgedOrders.DataSource = dv
            End If

            Dim dt As DataTable = dv.Table


            If dt.Rows.Count > 0 Then
                Dim strPatientSearchDetails As String
                If Trim(txtSearchUnacknowledged.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearchUnacknowledged.Text, "'", "''")
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                Dim strfilter As String =
 " Provider Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientFirstName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientMiddleName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientLastName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderDateFilter Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Convert(Age,'System.String') Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Test Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " ResultDate Like '%" & strPatientSearchDetails & "%'"
                dt.DefaultView.RowFilter = strfilter
                strPatientSearchDetails = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridUnAcknowledgedOrders_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnAcknowledgedOrders.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = GridUnAcknowledgedOrders.HitTest(e.X, e.Y)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                Dim PatientID As Long = 0
                Dim nVisitId As Long = 0
                Dim OrderNoPrefix As String = ""
                PatientID = Convert.ToInt64(GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "PatientID"))
                nVisitId = Convert.ToInt64(GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "nVisitId"))
                OrderNoPrefix = GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "OrderNoPrefix")
                OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                OrderNoPrefix = Nothing
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbUnacknowledgedDate_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbUnacknowledgedDate.SelectedValueChanged
        Try
            If cmbUnacknowledgedDate.Text = "30 + Days" Then
                dtUnacknowledgedFrom.Visible = False
                dtUnacknowledgedTo.Visible = False
                lblUnacknowledgedFrom.Visible = False
                lblUnacknowledgedTo.Visible = False
            Else
                dtUnacknowledgedFrom.Visible = True
                dtUnacknowledgedTo.Visible = True
                lblUnacknowledgedFrom.Visible = True
                lblUnacknowledgedTo.Visible = True
                SetFromAndToDate(cmbUnacknowledgedDate.Text, "Unacknowledged")
            End If
            FillGridUnacknowledgedOrder()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtUnacknowledgedFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtUnacknowledgedFrom.ValueChanged
        FillGridUnacknowledgedOrder()
    End Sub

    Private Sub dtUnacknowledgedTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtUnacknowledgedTo.ValueChanged
        FillGridUnacknowledgedOrder()
    End Sub

    Private Sub txtSearchUnacknowledged_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearchUnacknowledged.TextChanged
        FilterUnacknowledgedOrder()
    End Sub

    Private Sub btnClearUnacknowledgedSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearUnacknowledgedSearch.Click
        txtSearchUnacknowledged.Text = ""
    End Sub

    Private Sub rdoResultedOrderDate_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoResultedOrderDate.CheckedChanged
        FillGridUnacknowledgedOrder()
    End Sub

    Private Sub rdoUnacknowledgedTests_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoUnacknowledgedTests.CheckedChanged
        FillGridUnacknowledgedOrder()
    End Sub

    Private Sub GridUnAcknowledgedOrders_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnAcknowledgedOrders.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub GridUnAcknowledgedOrders_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnAcknowledgedOrders.MouseClick
        Try
            If GridUnAcknowledgedOrders.Rows.Count > 1 Then
                Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnAcknowledgedOrders.DataSource), CurrencyManager)
                Dim dr As DataRowView = TryCast(cm.Current, DataRowView)
                UnAcknowledgedOrdersrowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)
                cm = Nothing
                dr = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridUnAcknowledgedOrders_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles GridUnAcknowledgedOrders.AfterSort
        Try
            If GridUnAcknowledgedOrders.Rows.Count > 1 Then
                If UnAcknowledgedOrdersrowIndex > -1 Then
                    For Each rw As C1.Win.C1FlexGrid.Row In GridUnAcknowledgedOrders.Rows
                        Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnAcknowledgedOrders.DataSource), CurrencyManager)
                        Dim dr As DataRowView = TryCast(rw.DataSource, DataRowView)
                        If dr IsNot Nothing Then
                            Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)

                            If currIndex = UnAcknowledgedOrdersrowIndex Then
                                Dim cr As CellRange = GridUnAcknowledgedOrders.GetCellRange(rw.Index, 1)
                                ' to scroll the selected row in the visible area
                                GridUnAcknowledgedOrders.[Select](cr, True)
                                cr = GridUnAcknowledgedOrders.GetCellRange(rw.Index, 0, rw.Index, GridUnAcknowledgedOrders.Cols.Count - 1)
                                GridUnAcknowledgedOrders.[Select](cr, False)
                                Exit For
                            End If
                        End If
                        cm = Nothing
                        dr = Nothing
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "All Orders"

    Private Function RetrieveAllOrdersData() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Flag", "AllOrders", ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@ProviderID", cmbProviders.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@DateCategory", cmbAllOrderDate.Text, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@Fromdate", dtAllOrdersFrom.Value, ParameterDirection.Input, SqlDbType.Date)
            oParam.Add("@Todate", dtAllOrdersTo.Value, ParameterDirection.Input, SqlDbType.Date)
            oParam.Add("@OrderStatusNumber", cmbAllOrdersStatus.SelectedValue, ParameterDirection.Input, SqlDbType.Int)
            oDB.Retrive("gsp_GetOutstandingOrders", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub FillGridAllOrders()
        Try

            Dim dt As DataTable = RetrieveAllOrdersData()

            GridAllOrdersOrders.DataSource = dt.Copy().DefaultView
            RetrieveAllOrdersData()

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterAllOrders()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dv As DataView = CType(GridAllOrdersOrders.DataSource, DataView)

            If IsNothing(dv) Then
                Exit Sub
            Else
                GridAllOrdersOrders.DataSource = dv
            End If

            Dim dt As DataTable = dv.Table

            If dt.Rows.Count > 0 Then
                Dim strPatientSearchDetails As String
                If Trim(txtSearchAllOrders.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearchAllOrders.Text, "'", "''")
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                dt.DefaultView.RowFilter = " Provider Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientFirstName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientMiddleName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientLastName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderDateFilter Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderStatus Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Test Like '%" & strPatientSearchDetails & "%'"
                strPatientSearchDetails = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridAllOrdersOrders_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridAllOrdersOrders.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = GridAllOrdersOrders.HitTest(e.X, e.Y)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                Dim PatientID As Long = 0
                Dim nVisitId As Long = 0
                Dim OrderNoPrefix As String = ""
                PatientID = Convert.ToInt64(GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "PatientID"))
                nVisitId = Convert.ToInt64(GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "nVisitId"))
                OrderNoPrefix = GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "OrderNoPrefix")
                OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                OrderNoPrefix = Nothing
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbAllOrderDate_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbAllOrderDate.SelectedValueChanged
        SetFromAndToDate(cmbAllOrderDate.Text, "All Orders")
        FillGridUnacknowledgedOrder()
    End Sub

    Private Sub dtAllOrdersFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtAllOrdersFrom.ValueChanged
        FillGridAllOrders()
    End Sub

    Private Sub dtAllOrdersTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtAllOrdersTo.ValueChanged
        FillGridAllOrders()
    End Sub

    Private Sub txtSearchAllOrders_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearchAllOrders.TextChanged
        FilterAllOrders()
    End Sub

    Private Sub btnClearAllOrdersSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearAllOrdersSearch.Click
        txtSearchAllOrders.Text = ""
    End Sub

    Private Sub FillOrderStatusList()
        Try
            Me.Cursor = Cursors.WaitCursor
            RemoveHandler cmbAllOrdersStatus.SelectedValueChanged, AddressOf cmbAllOrdersStatus_SelectedValueChanged
            dtOrderStatusList = RetrieveData("OrderStatus")
            cmbAllOrdersStatus.DataSource = dtOrderStatusList
            cmbAllOrdersStatus.ValueMember = "OrderStatusNumber"
            cmbAllOrdersStatus.DisplayMember = "OrderStatus"
            If dtOrderStatusList.Rows.Count > 0 Then
                cmbAllOrdersStatus.SelectedIndex = 0
            End If
            AddHandler cmbAllOrdersStatus.SelectedValueChanged, AddressOf cmbAllOrdersStatus_SelectedValueChanged
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbAllOrdersStatus_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbAllOrdersStatus.SelectedValueChanged
        FillGridAllOrders()
    End Sub

    Private Sub GridAllOrdersOrders_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridAllOrdersOrders.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub GridAllOrdersOrders_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridAllOrdersOrders.MouseClick
        Try
            If GridAllOrdersOrders.Rows.Count > 1 Then
                Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridAllOrdersOrders.DataSource), CurrencyManager)
                Dim dr As DataRowView = TryCast(cm.Current, DataRowView)
                AllOrderOrdersrowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)
                cm = Nothing
                dr = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridAllOrdersOrders_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles GridAllOrdersOrders.AfterSort
        Try
            If GridAllOrdersOrders.Rows.Count > 1 Then
                If AllOrderOrdersrowIndex > -1 Then
                    For Each rw As C1.Win.C1FlexGrid.Row In GridAllOrdersOrders.Rows
                        Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridAllOrdersOrders.DataSource), CurrencyManager)
                        Dim dr As DataRowView = TryCast(rw.DataSource, DataRowView)
                        If dr IsNot Nothing Then
                            Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)

                            If currIndex = AllOrderOrdersrowIndex Then
                                Dim cr As CellRange = GridAllOrdersOrders.GetCellRange(rw.Index, 1)
                                ' to scroll the selected row in the visible area
                                GridAllOrdersOrders.[Select](cr, True)
                                cr = GridAllOrdersOrders.GetCellRange(rw.Index, 0, rw.Index, GridAllOrdersOrders.Cols.Count - 1)
                                GridAllOrdersOrders.[Select](cr, False)
                                Exit For
                            End If
                        End If
                        cm = Nothing
                        dr = Nothing
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Unfinished Order Templates"

    Private Function RetrieveUnfinishedOrderTemplatesData() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)

            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Flag", "UnfinishedOrderTemplates", ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@ProviderID", cmbProviders.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@DateCategory", cmbUnfinishedOrderDate.Text, ParameterDirection.Input, SqlDbType.Text)
            oParam.Add("@Fromdate", dtUnfinishedOrderFrom.Value, ParameterDirection.Input, SqlDbType.Date)
            oParam.Add("@Todate", dtUnfinishedOrderTo.Value, ParameterDirection.Input, SqlDbType.Date)
            oDB.Retrive("gsp_GetOutstandingOrders", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub FillUnfinishedOrderTemplates()
        Try
            Dim dt As DataTable = RetrieveUnfinishedOrderTemplatesData()
            GridUnfinishedOrderTemplates.DataSource = dt.Copy().DefaultView
            FilterUnfinishedOrdersTemplates()

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterUnfinishedOrdersTemplates()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dv As DataView = CType(GridUnfinishedOrderTemplates.DataSource, DataView)

            If IsNothing(dv) Then
                Exit Sub
            Else
                GridUnfinishedOrderTemplates.DataSource = dv
            End If

            Dim dt As DataTable = dv.Table


            If dt.Rows.Count > 0 Then
                Dim strPatientSearchDetails As String
                If Trim(txtSearchUnfinishedOrders.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearchUnfinishedOrders.Text, "'", "''")
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                dt.DefaultView.RowFilter = " Provider Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientFirstName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientMiddleName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " PatientLastName Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderDateFilter Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Category Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " OrderStatus Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Test Like '%" & strPatientSearchDetails & "%'"
                strPatientSearchDetails = Nothing
            End If

            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridUnfinishedOrderTemplates_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnfinishedOrderTemplates.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = GridUnfinishedOrderTemplates.HitTest(e.X, e.Y)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                OpenRadiology()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmbUnfinishedOrderDate_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbUnfinishedOrderDate.SelectedValueChanged
        SetFromAndToDate(cmbUnfinishedOrderDate.Text, "Unfinished Order Templates")
        FillUnfinishedOrderTemplates()
    End Sub

    Private Sub dtUnfinishedOrderFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtUnfinishedOrderFrom.ValueChanged
        FillUnfinishedOrderTemplates()
    End Sub

    Private Sub dtUnfinishedOrderTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtUnfinishedOrderTo.ValueChanged
        FillUnfinishedOrderTemplates()
    End Sub

    Private Sub txtSearchUnfinishedOrders_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearchUnfinishedOrders.TextChanged
        FilterUnfinishedOrdersTemplates()
    End Sub

    Private Sub btnClearUnfinishedOrdersSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearUnfinishedOrdersSearch.Click
        txtSearchUnfinishedOrders.Text = ""
    End Sub

    Private Sub cmbUnfinishedOrdersStatus_SelectedValueChanged(sender As System.Object, e As System.EventArgs)
        FillUnfinishedOrderTemplates()
    End Sub

    Private Sub OpenRadiology()
        If Convert.ToInt64(GridUnfinishedOrderTemplates.GetData(GridUnfinishedOrderTemplates.RowSel, "PatientID")) >= 0 Then
            _PatientID = Convert.ToInt64(GridUnfinishedOrderTemplates.GetData(GridUnfinishedOrderTemplates.RowSel, "PatientID"))
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            Else

                Dim nVisitId As Long = 0
                Dim VisitDate As String = ""

                nVisitId = Convert.ToInt64(GridUnfinishedOrderTemplates.GetData(GridUnfinishedOrderTemplates.RowSel, "nVisitId"))
                VisitDate = GridUnfinishedOrderTemplates.GetData(GridUnfinishedOrderTemplates.RowSel, "OrderDate")


                Dim frm As frm_LM_Orders
                frm = frm_LM_Orders.GetInstance(nVisitId, VisitDate, _PatientID, 1, False)

                If IsNothing(frm) = True Then
                    Exit Sub
                End If

                With frm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                    .MdiParent = CType(Me.MdiParent, MainMenu)
                    .WindowState = FormWindowState.Maximized
                    .Show()
                End With
            End If
        End If



    End Sub

    Private Sub GridUnfinishedOrderTemplates_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnfinishedOrderTemplates.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub GridUnfinishedOrderTemplates_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridUnfinishedOrderTemplates.MouseClick
        Try
            If GridUnfinishedOrderTemplates.Rows.Count > 1 Then
                Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnfinishedOrderTemplates.DataSource), CurrencyManager)
                Dim dr As DataRowView = TryCast(cm.Current, DataRowView)
                UnfinishedOrderTemplatesrowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)
                cm = Nothing
                dr = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridUnfinishedOrderTemplates_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles GridUnfinishedOrderTemplates.AfterSort
        Try
            If GridAllOrdersOrders.Rows.Count > 1 Then
                If UnfinishedOrderTemplatesrowIndex > -1 Then
                    For Each rw As C1.Win.C1FlexGrid.Row In GridUnfinishedOrderTemplates.Rows
                        Dim cm As CurrencyManager = DirectCast(BindingContext(Me.GridUnfinishedOrderTemplates.DataSource), CurrencyManager)
                        Dim dr As DataRowView = TryCast(rw.DataSource, DataRowView)
                        If dr IsNot Nothing Then
                            Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)

                            If currIndex = UnfinishedOrderTemplatesrowIndex Then
                                Dim cr As CellRange = GridUnfinishedOrderTemplates.GetCellRange(rw.Index, 1)
                                ' to scroll the selected row in the visible area
                                GridUnfinishedOrderTemplates.[Select](cr, True)
                                cr = GridUnfinishedOrderTemplates.GetCellRange(rw.Index, 0, rw.Index, GridUnfinishedOrderTemplates.Cols.Count - 1)
                                GridUnfinishedOrderTemplates.[Select](cr, False)
                                Exit For
                            End If
                        End If
                        cm = Nothing
                        dr = Nothing
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Sub Procedure"

    Private Sub OpenLabs(PatientID As Long, nVisitId As Long, OrderNoPrefix As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            If PatientID >= 0 Then
                '_PatientID = Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "PatientID"))
                If MainMenu.IsAccess(False, PatientID, , True) = False Then
                    Exit Sub
                End If
                If MainMenu.IsAccess(False, PatientID) = False Then
                    Exit Sub
                Else
                    'Dim nVisitId As Long = 0
                    'nVisitId = Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "nVisitId"))
                    Dim frm_viewgloLab As New gloEmdeonInterface.Forms.frmViewgloLab(PatientID)

                    If (frm_viewgloLab.CheckInstance() = True) Then
                        MessageBox.Show("A Lab Order screen is already open. Please close that to continue…", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        frm_viewgloLab.Dispose()
                        frm_viewgloLab = Nothing
                        Exit Sub
                    End If

                    With frm_viewgloLab
                        AddHandler frm_viewgloLab.EventCDA, AddressOf mdlGeneral.OpenCDA
                        AddHandler frm_viewgloLab.EntOpenMessage, AddressOf CType(Me.ParentForm, MainMenu).OpenMessage
                        AddHandler frm_viewgloLab.EvntOpenPatientLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenPatientLetter
                        AddHandler frm_viewgloLab.EvntOpenReferralLetter, AddressOf CType(Me.ParentForm, MainMenu).OpenReferralLetters
                        AddHandler frm_viewgloLab.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart
                        AddHandler frm_viewgloLab.EvntOpenPlanOfTreatment, AddressOf CType(Me.ParentForm, MainMenu).OpenPlanofTreatment
                        'AddHandler ofrmViewgloLab.EvntOpenClinicalChart, AddressOf OpenClinicalChart
                        AddHandler frm_viewgloLab.EvntGenerateCCDHandler, AddressOf CType(Me.ParentForm, MainMenu).openCCD
                        AddHandler frm_viewgloLab.EvntGenerateCDAHandler, AddressOf CType(Me.ParentForm, MainMenu).openCDA
                        AddHandler frm_viewgloLab.FormClosed, AddressOf CType(Me.ParentForm, MainMenu).ofrmViewgloLab_FormClosed 'ShowTasks
                        AddHandler frm_viewgloLab.Activated, AddressOf CType(Me.ParentForm, MainMenu).frmViewgloLab_Activated
                        AddHandler frm_viewgloLab.EntOpenEducation, AddressOf CType(Me.ParentForm, MainMenu).OpenEducation
                        frm_viewgloLab.objgloLabPatientExam = New clsPatientExams
                        frm_viewgloLab.objgloLabPatientMessages = New clsMessage
                        frm_viewgloLab.objgloLabPatientLetters = New clsPatientLetters
                        frm_viewgloLab.objgloLabNurseNotes = New clsNurseNotes
                        frm_viewgloLab.objgloLabHistory = New clsPatientHistory
                        frm_viewgloLab.objgloLabLabs = New clsLabs
                        frm_viewgloLab.objgloLabDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                        frm_viewgloLab.objgloLabRxmed = New clsPatientDetails
                        frm_viewgloLab.objgloLabOrders = New clsPatientDetails
                        frm_viewgloLab.objgloLabProblemList = New clsPatientProblemList
                        frm_viewgloLab.objgloLabCriteria = New DocCriteria
                        frm_viewgloLab.objgloLabWord = New clsWordDocument
                        .SelectOrderTab = True
                        .LabOrderParameter.OrderNumberPrefix = OrderNoPrefix
                        .LabOrderParameter.VisitID = nVisitId
                        .LabOrderParameter.PatientID = PatientID
                        ''added for split control functionality order & result screen
                        Dim objclsSplit_Laborder As New gloEMRGeneralLibrary.clsSplitScreen()
                        objclsSplit_Laborder.blnShowSmokingStatusCol = gblnShowSmokingColumn
                        frm_viewgloLab.VisitID = nVisitId
                        frm_viewgloLab.clsSplit_Laborder = objclsSplit_Laborder
                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                        CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                        .MdiParent = CType(Me.MdiParent, MainMenu)
                        .ShowInTaskbar = False
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Functions"

    Public Function GetDateRange(Category As String) As DateTime()
        Dim DateRange(1) As DateTime
        Try

            Select Case Category
                Case "Today"
                    DateRange(0) = System.DateTime.Now.Date
                    DateRange(1) = System.DateTime.Now.Date
                    Return DateRange

                Case "Yesterday"
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-1)
                    DateRange(1) = System.DateTime.Now.Date.AddDays(-1)
                    Return DateRange

                Case "Last 7 Days"
                    Dim WeekLdate As DateTime
                    WeekLdate = System.DateTime.Now.Date.AddDays(-(Weekday(Now, FirstDayOfWeek.Sunday)))
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-7)
                    DateRange(1) = System.DateTime.Now.Date
                    Return DateRange

                Case "Last Week"
                    Dim WeekLdate As DateTime
                    WeekLdate = System.DateTime.Now.Date.AddDays(-(Weekday(Now, FirstDayOfWeek.Sunday)))
                    DateRange(0) = WeekLdate.AddDays(-6)
                    DateRange(1) = WeekLdate
                    Return DateRange

                Case "This Month"
                    Dim nLastMonth As Integer
                    Dim nYear As Integer
                    nLastMonth = (System.DateTime.Now.Month)
                    nYear = System.DateTime.Now.Year
                    If nLastMonth = 0 Then
                        nLastMonth = 12
                        nYear = System.DateTime.Now.Year
                    End If
                    DateRange(0) = DateAndTime.DateSerial(nYear, nLastMonth, 1)
                    DateRange(1) = DateAndTime.DateSerial(nYear, nLastMonth, Date.DaysInMonth(nYear, nLastMonth)) 'To Date
                    Return DateRange

                Case "Last Month"
                    Dim nLastMonth As Integer
                    Dim nYear As Integer
                    nLastMonth = (System.DateTime.Now.Month - 1)
                    nYear = System.DateTime.Now.Year
                    If nLastMonth = 0 Then
                        nLastMonth = 12
                        nYear = System.DateTime.Now.Year - 1
                    End If
                    DateRange(0) = DateAndTime.DateSerial(nYear, nLastMonth, 1)
                    DateRange(1) = DateAndTime.DateSerial(nYear, nLastMonth, Date.DaysInMonth(nYear, nLastMonth)) 'To Date
                    Return DateRange

                Case "This Year"
                    DateRange(0) = DateAndTime.DateSerial(System.DateTime.Now.Year, 1, 1)
                    DateRange(1) = DateAndTime.DateSerial(System.DateTime.Now.Year, 12, Date.DaysInMonth(System.DateTime.Now.Year, 12)) 'To Date
                    Return DateRange

                Case "0 - 15 Days"
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-15)
                    DateRange(1) = System.DateTime.Now.Date
                    Return DateRange

                Case "16 - 60 Days"
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-76)
                    DateRange(1) = System.DateTime.Now.Date.AddDays(-16)
                    Return DateRange

                Case "61 - 120 Days"
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-197)
                    DateRange(1) = System.DateTime.Now.Date.AddDays(-77)
                    Return DateRange

                Case "120+ Days"
                    DateRange(0) = System.DateTime.Now.Date
                    DateRange(1) = System.DateTime.Now.Date
                    Return DateRange

                Case "0 - 7 Days"
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-7)
                    DateRange(1) = System.DateTime.Now.Date
                    Return DateRange

                Case "8 - 30 Days"
                    DateRange(0) = System.DateTime.Now.Date.AddDays(-38)
                    DateRange(1) = System.DateTime.Now.Date.AddDays(-8)
                    Return DateRange

                Case "30+ Days"
                    DateRange(0) = System.DateTime.Now.Date
                    DateRange(1) = System.DateTime.Now.Date
                    Return DateRange

                Case Else
                    Return Nothing
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Function RetrieveData(flag As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dt As DataTable = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@Flag", flag, ParameterDirection.Input, SqlDbType.Text)
            oDB.Retrive("gsp_GetOutstandingOrders", oParam, dt)
            oDB.Disconnect()
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return Nothing
        Finally
            Me.Cursor = Cursors.Default
            If Not IsNothing(oParam) Then
                oParam.Dispose() : oParam = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Private Sub PrintReport()
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport = Nothing
        Dim _MessageBoxCaption As String = String.Empty
        Dim _databaseConnectionString As String = String.Empty
        Dim _LoginName As String = String.Empty
        Dim gstrSQLServerName As String = String.Empty
        Dim gstrDatabaseName As String = String.Empty
        Dim gstrSQLUserEMR As String = String.Empty
        Dim gstrSQLPasswordEMR As String = String.Empty
        Dim ParameterValue As String = Nothing
        Dim ParameterName As String = Nothing

        Dim gblnDefaultPrinter As Boolean = False
        Dim gblnSQLAuthentication As Boolean = False

        Try


            If Not String.IsNullOrEmpty(appSettings("DataBaseConnectionString")) Then
                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If

            If Not String.IsNullOrEmpty(appSettings("UserName")) Then
                _LoginName = Convert.ToString(appSettings("UserName"))
            End If

            If Not String.IsNullOrEmpty(appSettings("SQLServerName")) Then
                gstrSQLServerName = Convert.ToString(appSettings("SQLServerName"))
            End If

            If Not String.IsNullOrEmpty(appSettings("DatabaseName")) Then
                gstrDatabaseName = Convert.ToString(appSettings("DatabaseName"))
            End If

            If Not String.IsNullOrEmpty(appSettings("SQLLoginName")) Then
                gstrSQLUserEMR = Convert.ToString(appSettings("SQLLoginName"))
            End If

            If Not String.IsNullOrEmpty(appSettings("SQLPassword")) Then
                gstrSQLPasswordEMR = Convert.ToString(appSettings("SQLPassword"))
            End If

            If Not String.IsNullOrEmpty(appSettings("DefaultPrinter")) Then
                gblnDefaultPrinter = Not Convert.ToBoolean(appSettings("DefaultPrinter"))
            End If

            If Not String.IsNullOrEmpty(appSettings("WindowAuthentication")) Then
                gblnSQLAuthentication = Not Convert.ToBoolean(appSettings("WindowAuthentication"))
            End If

            SetFormData()


            ParameterName = "user,Flag,ProviderID,DateCategory,Fromdate,Todate,OrderStatusNumber,IsResultedOrderDate"
            ParameterValue = _LoginName & "," & sTabName & "," & nProviderId & "," & sDateCategory & "," + dtFromdate & "," & dtTodate & "," & nOrderStatusNumber & "," & IsResultedOrderDate


            clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
            clsPrntRpt.PrintReport("rpt_OutstandingOrder", ParameterName, ParameterValue, gblnDefaultPrinter, "")
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            If clsPrntRpt IsNot Nothing Then
                clsPrntRpt.Dispose()
                clsPrntRpt = Nothing
            End If

            _MessageBoxCaption = Nothing
            _databaseConnectionString = Nothing
            _LoginName = Nothing
            gstrSQLServerName = Nothing
            gstrDatabaseName = Nothing
            gstrSQLUserEMR = Nothing
            gstrSQLPasswordEMR = Nothing
            ParameterValue = Nothing
            ParameterName = Nothing
        End Try
    End Sub

    Private Sub SetFormData()
        Try
            Select Case TabOutstandingOrders.SelectedTab.Text

                Case " Unsent Orders "
                    sTabName = "UnsentOrders"
                    sDateCategory = cmbUnsentOrdersDate.Text
                    dtFromdate = dtUnsentOrdersFrom.Value
                    dtTodate = dtUnsentOrdersTo.Value
                Case " Unresulted "
                    sTabName = "Unresulted"
                    sDateCategory = cmbUnresultedDate.Text
                    dtFromdate = dtUnresultedFrom.Value
                    dtTodate = dtUnresultedTo.Value
                Case " Unacknowledged "
                    sTabName = "Unacknowledged"
                    sDateCategory = cmbUnacknowledgedDate.Text
                    dtFromdate = dtUnacknowledgedFrom.Value
                    dtTodate = dtUnacknowledgedTo.Value
                Case " All Orders "
                    sTabName = "AllOrders"
                    sDateCategory = cmbAllOrderDate.Text
                    dtFromdate = dtAllOrdersFrom.Value
                    dtTodate = dtAllOrdersTo.Value
                Case " Unfinished Order Templates "
                    sTabName = "UnfinishedOrderTemplates"
                    sDateCategory = cmbUnfinishedOrderDate.Text
                    dtFromdate = dtUnfinishedOrderFrom.Value
                    dtTodate = dtUnfinishedOrderTo.Value
            End Select

            nProviderId = cmbProviders.SelectedValue



            nOrderStatusNumber = cmbAllOrdersStatus.SelectedValue

            If rdoResultedOrderDate.Checked = True Then
                IsResultedOrderDate = True
            Else
                IsResultedOrderDate = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Dim frmSSRS As gloEMRReports.frmSSRSViewer
    Private Sub ShowSSRSReport(ByVal ReportName As String, ByVal ReportTitle As String, ByVal blnIsgloStreamReport As Boolean)
        Try

            If Not IsNothing(frmSSRS) Then  ''slr free frmssrs before allocating

                frmSSRS.Dispose()
                frmSSRS = Nothing
            End If
            frmSSRS = New gloEMRReports.frmSSRSViewer(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)

            SetFormData()

            frmSSRS.sOOTabName = sTabName
            frmSSRS.nOOProviderId = nProviderId
            frmSSRS.sOODateCategory = sDateCategory
            frmSSRS.dtOOFromdate = dtFromdate
            frmSSRS.dtOOTodate = dtTodate
            frmSSRS.nOOOrderStatusNumber = nOrderStatusNumber
            frmSSRS.IsOOResultedOrderDate = IsResultedOrderDate

            frmSSRS.reportName = ReportName
            frmSSRS.reportTitle = ReportTitle
            frmSSRS.IsgloStreamReport = blnIsgloStreamReport

            frmSSRS.Conn = GetConnectionString()

            frmSSRS.ShowDialog()
            frmSSRS.Dispose()
            frmSSRS = Nothing


        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Fill Combobox"

    Private Sub FillproviderList()
        Try
            RemoveHandler cmbProviders.SelectedIndexChanged, AddressOf cmbProviders_SelectedIndexChanged
            Me.Cursor = Cursors.WaitCursor
            dtProviderList = RetrieveData("Provider")
            cmbProviders.DataSource = dtProviderList
            cmbProviders.DisplayMember = "ProviderName"
            cmbProviders.ValueMember = "nProviderID"
            If dtProviderList.Rows.Count > 0 Then
                If (_nLoginProviderId > 0) Then
                    cmbProviders.SelectedValue = _nLoginProviderId
                Else
                    cmbProviders.SelectedIndex = 0
                End If
            End If
            AddHandler cmbProviders.SelectedIndexChanged, AddressOf cmbProviders_SelectedIndexChanged
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbProviders_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbProviders.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            FillGridUnsentOrders()
            FillGridUnResultedOrder()
            FillGridUnacknowledgedOrder()
            FillGridAllOrders()
            FillUnfinishedOrderTemplates()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SetFromAndToDate(SelectedText As String, SelectedTabName As String)
        Try
            Dim _DateRange() As DateTime

            _DateRange = GetDateRange(SelectedText)

            If IsNothing(_DateRange) = False Then
                If _DateRange.Length > 0 Then

                    Select Case SelectedTabName

                        Case "Unsent Orders"
                            dtUnsentOrdersFrom.Value = _DateRange(0)
                            dtUnsentOrdersTo.Value = _DateRange(1)

                        Case "Unresulted"
                            dtUnresultedFrom.Value = _DateRange(0)
                            dtUnresultedTo.Value = _DateRange(1)

                        Case "Unacknowledged"
                            dtUnacknowledgedFrom.Value = _DateRange(0)
                            dtUnacknowledgedTo.Value = _DateRange(1)

                        Case "All Orders"
                            dtAllOrdersFrom.Value = _DateRange(0)
                            dtAllOrdersTo.Value = _DateRange(1)

                        Case "Unfinished Order Templates"
                            dtUnfinishedOrderFrom.Value = _DateRange(0)
                            dtUnfinishedOrderTo.Value = _DateRange(1)
                    End Select
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Form Close"

    Private Sub frmOutstandingOrdersAndResult_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            RemoveHandler cmbProviders.SelectedIndexChanged, AddressOf cmbProviders_SelectedIndexChanged
            If IsNothing(dtProviderList) = False Then : dtProviderList.Dispose() : dtProviderList = Nothing : End If

            RemoveHandler cmbAllOrdersStatus.SelectedValueChanged, AddressOf cmbAllOrdersStatus_SelectedValueChanged
            If IsNothing(dtOrderStatusList) = False Then : dtOrderStatusList.Dispose() : dtOrderStatusList = Nothing : End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Toolstrip button click"

    Private Sub ts_btnOpen_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnOpen.Click
        Try

            Dim PatientID As Long = 0
            Dim nVisitId As Long = 0
            Dim OrderNoPrefix As String = ""

            Select Case TabOutstandingOrders.SelectedTab.Text
                Case " Unsent Orders "
                    If GridUnsentOrders.RowSel > 0 Then
                        If Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "PatientID")) >= 0 Then
                            PatientID = Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "PatientID"))
                            nVisitId = Convert.ToInt64(GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "nVisitId"))
                            OrderNoPrefix = GridUnsentOrders.GetData(GridUnsentOrders.RowSel, "OrderNoPrefix")
                            OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                        End If
                    End If

                Case " Unresulted "
                    If GridUnResultedOrders.RowSel > 0 Then
                        If Convert.ToInt64(GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "PatientID")) >= 0 Then
                            PatientID = Convert.ToInt64(GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "PatientID"))
                            nVisitId = Convert.ToInt64(GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "nVisitId"))
                            OrderNoPrefix = GridUnResultedOrders.GetData(GridUnResultedOrders.RowSel, "OrderNoPrefix")
                            OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                        End If
                    End If

                Case " Unacknowledged "
                    If GridUnAcknowledgedOrders.RowSel > 0 Then
                        If Convert.ToInt64(GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "PatientID")) >= 0 Then
                            PatientID = Convert.ToInt64(GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "PatientID"))
                            nVisitId = Convert.ToInt64(GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "nVisitId"))
                            OrderNoPrefix = GridUnAcknowledgedOrders.GetData(GridUnAcknowledgedOrders.RowSel, "OrderNoPrefix")
                            OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                        End If
                    End If

                Case " All Orders "
                    If GridAllOrdersOrders.RowSel > 0 Then
                        If Convert.ToInt64(GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "PatientID")) >= 0 Then
                            PatientID = Convert.ToInt64(GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "PatientID"))
                            nVisitId = Convert.ToInt64(GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "nVisitId"))
                            OrderNoPrefix = GridAllOrdersOrders.GetData(GridAllOrdersOrders.RowSel, "OrderNoPrefix")
                            OpenLabs(PatientID, nVisitId, OrderNoPrefix)
                        End If
                    End If

                Case " Unfinished Order Templates "
                    If GridUnfinishedOrderTemplates.RowSel > 0 Then
                        If Convert.ToInt64(GridUnfinishedOrderTemplates.GetData(GridUnfinishedOrderTemplates.RowSel, "PatientID")) >= 0 Then
                            OpenRadiology()
                        End If
                    End If

            End Select

            OrderNoPrefix = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            FillGridUnsentOrders()
            FillGridUnResultedOrder()
            FillGridUnacknowledgedOrder()
            FillGridAllOrders()
            FillUnfinishedOrderTemplates()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As Object, e As System.EventArgs) Handles ts_btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnPrint.Click
        Try
            PrintReport()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Orders, ActivityCategory.PrintDocument, ActivityType.Print, "Printed outstanding order report", _PatientID, 0, 0, ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Orders, ActivityCategory.PrintDocument, ActivityType.Print, "Printed outstanding order report", _PatientID, 0, 0, ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ts_btnPrintPreview_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnPrintPreview.Click
        Try
            ShowSSRSReport("rpt_OutstandingOrder", "Outstanding Orders", True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Orders, ActivityCategory.Reports, ActivityType.Print, "Printed outstanding order report", _PatientID, 0, 0, ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Orders, ActivityCategory.Reports, ActivityType.Print, "Printed outstanding order report", _PatientID, 0, 0, ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region "Tab Selected"

    Private Sub TabOutstandingOrders_Selected(sender As Object, e As System.Windows.Forms.TabControlEventArgs) Handles TabOutstandingOrders.Selected

        Select Case TabOutstandingOrders.SelectedTab.Text

            Case " Unsent Orders "
                SetFromAndToDate(cmbUnsentOrdersDate.Text, "Unsent Orders")

            Case " Unresulted "
                SetFromAndToDate(cmbUnresultedDate.Text, "Unresulted")

            Case " Unacknowledged "
                SetFromAndToDate(cmbUnacknowledgedDate.Text, "Unacknowledged")

            Case " All Orders "
                SetFromAndToDate(cmbAllOrderDate.Text, "All Orders")

            Case " Unfinished Order Templates "
                SetFromAndToDate(cmbUnfinishedOrderDate.Text, "Unfinished Order Templates")

        End Select

    End Sub

#End Region



End Class