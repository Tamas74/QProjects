'Developer:Dipak Patil
'Date:20120207
'Bug ID/PRD Name/Salesforce Case:Immunisation PRD
'Reason: New Implementation
Public Class frmIMSkuSearch
    Public SKU As String = ""
    Public Vaccine As String = ""
    Public Trade As String = ""
    Public LotNo As String = ""
    Public Manufacturer As String = ""
    Public NDC As String = ""
    Public CPT As String = ""
    Public Diagnosis As String = ""
    Public Funding As String = ""
    Public Comment As String = ""
    Public ExpDate As String = ""
    Public nLocationID As String
    Public nCategoryID As String
    Public nICDRevision As String = ""

    Private Sub frmIMSkuSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            cmbLocation.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler cmbLocation.DrawItem, AddressOf ShowTooltipOnWriteOffComboBox

            gloC1FlexStyle.Style(C1IMView)
            RefreshCategory()
            getLocationList()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub RefreshCategory()
        Try
            RefreshGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Developer:Dipak Patil
    'Date:20120207
    'Bug ID/PRD Name/Sales force Case:Immunization PRD
    'Reason: New Implementation for fill Grid
    Private Sub RefreshGrid()
        Dim dtIMDetails As DataTable
        Dim oIM As New gloStream.Immunization.ItemSetup
        dtIMDetails = oIM.GetSKUList()
        C1IMView.DataSource = dtIMDetails.DefaultView
        'C1DiagnosisOneCCDResponse.DataSource = dtCCDResponse
        For Each col As C1.Win.C1FlexGrid.Column In C1IMView.Cols
            col.AllowEditing = False
            col.Visible = False
        Next
        C1IMView.Cols(0).Visible = False
        C1IMView.Cols(1).Visible = False


        C1IMView.Cols("On Hand").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
        C1IMView.Cols("Exp. Date").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

        C1IMView.Cols("SKU").Width = 90
        C1IMView.Cols("Trade Name").Width = 100
        C1IMView.Cols("Vaccine").Width = 150
        C1IMView.Cols("Category").Width = 100
        C1IMView.Cols("On Hand").Width = 160
        C1IMView.Cols("Funding").Width = 60
        C1IMView.Cols("Mfr.").Width = 40
        C1IMView.Cols("Exp. Date").Width = 70
        C1IMView.Cols("Lot#").Width = 45
        C1IMView.Cols("Location").Width = 70
        C1IMView.Cols("nICDRevision").Width = 70

        C1IMView.Cols("SKU").Visible = True
        C1IMView.Cols("Vaccine").Visible = True
        C1IMView.Cols("Category").Visible = True
        C1IMView.Cols("Trade Name").Visible = True
        C1IMView.Cols("Mfr.").Visible = True
        C1IMView.Cols("Lot#").Visible = True
        C1IMView.Cols("NDC Code").Visible = True
        C1IMView.Cols("Funding").Visible = True
        C1IMView.Cols("Diagnosis").Visible = True
        C1IMView.Cols("CPT").Visible = True
        C1IMView.Cols("Exp. Date").Visible = True
        C1IMView.Cols("On Hand").Visible = True
        C1IMView.Cols("On Hand").Caption = "Total Doses in Inventory"
        C1IMView.Cols("Location").Visible = True
        C1IMView.Cols("nICDRevision").Visible = False
    End Sub

    'Developer:Dipak Patil
    'Date:20120207
    'Bug ID/PRD Name/Sales force Case:Immunization PRD
    'Reason: New Implementation for fill Grid
    Private Sub C1IMView_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1IMView.DoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1IMView.HitTest(ptPoint)
        'Dim strFileName As String
        'Dim HitRow As Integer
        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
            ts_btnOk_Click(Nothing, Nothing)
            'SKU = C1IMView.GetData(C1IMView.Row, 3).ToString()
            'DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    'Developer:Dipak Patil
    'Date:20120207
    'Bug ID/PRD Name/Sales force Case:Immunization PRD
    'Reason: New Implementation  Event for close button
    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    'Developer:Dipak Patil
    'Date:20120207
    'Bug ID/PRD Name/Sales force Case:Immunization PRD
    'Reason: Event will fire when Select button will clicked
    Private Sub ts_btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnOk.Click
        If (C1IMView.Row > 0) Then
            SKU = C1IMView.GetData(C1IMView.Row, "SKU").ToString()
            Vaccine = C1IMView.GetData(C1IMView.Row, "Vaccine").ToString()
            Trade = C1IMView.GetData(C1IMView.Row, "Trade Name").ToString()
            LotNo = C1IMView.GetData(C1IMView.Row, "Lot#").ToString()
            Manufacturer = C1IMView.GetData(C1IMView.Row, "Manufacturer").ToString()
            NDC = C1IMView.GetData(C1IMView.Row, "NDC Code").ToString()
            CPT = C1IMView.GetData(C1IMView.Row, "CPT").ToString()
            Diagnosis = C1IMView.GetData(C1IMView.Row, "Diagnosis").ToString()
            Funding = C1IMView.GetData(C1IMView.Row, "Funding").ToString()
            'Comment = C1IMView.GetData(C1IMView.Row, "Comments").ToString()
            ExpDate = C1IMView.GetData(C1IMView.Row, "Exp. Date").ToString()
            nLocationID = C1IMView.GetData(C1IMView.Row, "nLocationID").ToString()
            nCategoryID = C1IMView.GetData(C1IMView.Row, "nCategoryID").ToString()

            nICDRevision = C1IMView.GetData(C1IMView.Row, "nICDRevision").ToString()

            DialogResult = Windows.Forms.DialogResult.OK

        End If
    End Sub

    'Developer:Dipak Patil
    'Date:20120207
    'Bug ID/PRD Name/Sales force Case:Immunization PRD
    'Reason: Function will filter records as per search criteria.
    Private Sub FilterRecord()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dv As DataView
            Dim dt As DataTable
            dv = CType(C1IMView.DataSource, DataView)
            If IsNothing(dv) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            C1IMView.DataSource = dv
            dt = dv.Table
            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            If cmbLocation.Text <> "All" Then
                dt.DefaultView.RowFilter = " (SKU Like '%" & strPatientSearchDetails & "%' OR " & _
                             " Vaccine Like '%" & strPatientSearchDetails & "%' OR " & _
                             " Category Like '%" & strPatientSearchDetails & "%' OR " & _
                             " [Mfr.] Like '%" & strPatientSearchDetails & "%' OR " & _
                             " [Lot#] Like '%" & strPatientSearchDetails & "%' OR " & _
                             " [Trade Name] Like '%" & strPatientSearchDetails & "%' ) and " & _
                             " Location = '" & Replace(cmbLocation.Text, "'", "''") & "' "

            Else
                dt.DefaultView.RowFilter = " SKU Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Vaccine Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " Category Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " [Mfr.] Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " [Lot#] Like '%" & strPatientSearchDetails & "%' OR " & _
                                              " [Trade Name] Like '%" & strPatientSearchDetails & "%'  OR " & _
                                              " Location Like '%" & strPatientSearchDetails & "%'"
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub lblSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSearch.TextChanged

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        FilterRecord()
    End Sub

    Private Sub C1IMView_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1IMView.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.Clear()
    End Sub

    Private Sub getLocationList()
        Dim dtLocation As New DataTable
        Dim strquery As String
        strquery = " select 0 nLocationID, 'All' as sLocation union select nLocationID, sLocation from AB_Location where bIsBlocked = 0 order by sLocation"
        Dim cls As New clsgloIMTransaction()
        dtLocation = cls.GetList(strquery)
        If dtLocation.Rows.Count > 0 Then
            cmbLocation.DataSource = dtLocation
            cmbLocation.ValueMember = "nLocationID"
            cmbLocation.DisplayMember = "sLocation"
            cmbLocation.SelectedValue = nLocationID
        End If
    End Sub



    Dim combo As New ComboBox
    Dim tooltip As New ToolTip
    Dim tooltip1 As New ToolTip


    Private Sub cmbLocation_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        FilterRecord()

        combo = DirectCast(sender, ComboBox)
        If cmbLocation.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation")), cmbLocation) >= cmbLocation.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation"))
                If ToolTip1.GetToolTip(cmbLocation) <> txt Then
                    ToolTip1.SetToolTip(cmbLocation, txt)
                End If
            Else
                Me.ToolTip1.SetToolTip(cmbLocation, "")

            End If
        End If

    End Sub


    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer
        ''Code Review Changes: Dispose Graphics object
        Dim width As Integer = 0
        Dim g As Graphics = Me.CreateGraphics()
        If g IsNot Nothing Then
            Dim s As SizeF = g.MeasureString(_text, combo.Font)
            width = Convert.ToInt32(s.Width)
            'Dispose graphics object
            g.Dispose()
        End If

        Return width
    End Function


    Private Sub cmbLocation_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLocation.MouseEnter
        combo = DirectCast(sender, ComboBox)
        If cmbLocation.SelectedItem IsNot Nothing Then
            If getWidthofListItems(Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation")), cmbLocation) >= cmbLocation.DropDownWidth - 20 Then
                Dim txt As String = Convert.ToString(DirectCast(cmbLocation.Items(cmbLocation.SelectedIndex), DataRowView)("sLocation"))
                If tooltip1.GetToolTip(cmbLocation) <> txt Then
                    tooltip1.SetToolTip(cmbLocation, txt)
                End If
            Else
                Me.tooltip1.SetToolTip(cmbLocation, "")

            End If
        End If
    End Sub

    Private Sub ShowTooltipOnWriteOffComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then
                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth Then
                        Me.tooltip.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right, e.Bounds.Bottom + 25)
                    End If
                Else
                    tooltip.Hide(combo)
                End If
            Else
                tooltip.Hide(combo)
            End If
            e.DrawFocusRectangle()
        End If
    End Sub

End Class
