Public Class gloSearchC1FlexgridUserCtrl


    Public Event gloC1FlexgridUserCtrlMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event _FlexAfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexCellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event _FlexClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event _FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _bSelectFlag As Boolean)
    Public Event _FlexMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event txtSearchFlexGridTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnCloseRefillClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event dgCustomGrid_AddClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnUC_ADDclick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnUC_OKclick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnUC_Cancelclick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private _RowIndex As Int32
    Private _RowsCount As Int32
    Public _dt As DataTable = Nothing
    Public _bSelectFlag As Boolean
    'Public _flex As C1.Win.C1FlexGrid.C1FlexGrid
    Dim i As Integer
    Private WithEvents _dv As DataView = Nothing

    'Dim _dv As New DataView
    Dim sColumnDataType

    Public Property RowsCol() As Int32
        Get
            Return _Flex.Rows.Count
        End Get
        Set(ByVal value As Int32)
            _Flex.Rows.Count = value
        End Set
    End Property
    Public Property RowIndex() As Int32
        Get
            Return _RowIndex
        End Get
        Set(ByVal value As Int32)
            _RowIndex = value
        End Set
    End Property

    Private _c1Flexgrid As C1.Win.C1FlexGrid.C1FlexGrid
    Public ReadOnly Property _flexObj() As C1.Win.C1FlexGrid.C1FlexGrid
        Get
            Return _c1Flexgrid
        End Get
    End Property

    Public ReadOnly Property _UCflex() As C1.Win.C1FlexGrid.C1FlexGrid
        Get
            Return _Flex
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New(ByVal dt As DataTable, Optional ByVal bSelectFlag As Boolean = False)
        '_dt = New DataTable
        '_bSelectFlag = bSelectFlag
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _dt = dt
        _bSelectFlag = bSelectFlag
        SetGridStyle()
        setlblTxtStyles()

        '_Flex.Cols(0).AllowEditing = True
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub setlblTxtStyles()
        With txtSearchFlexGrid
            .BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            .ForeColor = Drawing.SystemColors.WindowText
            .Width = 200
            .Height = 21
        End With

        With lblSearchOn
            .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New Font("Arial", 9, FontStyle.Regular)
            .Height = 82
            .Width = 15
            .BackColor = Color.FromArgb(207, 227, 254)
        End With
        With lblColName
            .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New Font("Arial", 9, FontStyle.Regular)
            .Height = 82
            .Width = 15
            '.BackColor = Color.FromArgb(207, 227, 254)
        End With
        With lblSearchString
            .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New Font("Arial", 9, FontStyle.Regular)
            .Height = 82
            .Width = 15
            .BackColor = Color.FromArgb(207, 227, 254)
        End With

        If _bSelectFlag = True Then
            With _Flex
                .Cols(0).Width = 50
                .Cols(0).DataType = System.Type.GetType("System.Boolean")
                .Cols(0).Name = "Select"
                .SetData(0, 0, "Select")
                .Cols(0).AllowEditing = True
            End With
        Else
            With _Flex
                .Cols(0).Width = 0
                '.Cols(0).DataType = System.Type.GetType("System.Boolean")
                '.Cols(0).Name = "Select"
                '.SetData(0, 0, "Select")
                '.Cols(0).AllowEditing = True
            End With
        End If
    End Sub

    public function SetGridStyle() As DataTable    
        ' Dim _dv As New DataView
        If (IsNothing(_dt)) Then
            Return _dt
        End If
        _dv = _dt.DefaultView

        With _Flex

            'Dim clmnSelect As New DataColumn
            'With clmnSelect
            '    .ColumnName = "Select"
            '    .DataType = System.Type.GetType("System.Boolean")
            '    .DefaultValue = CBool("False")
            'End With
            '_dt.Columns.Add(clmnSelect)

            .Cols.Fixed = 0
            .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New Font("Arial", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .BackColor = System.Drawing.Color.White

            .Styles.Fixed.BackColor = Color.FromArgb(51, 125, 207)
            .Styles.Fixed.ForeColor = Color.White
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFontArial_Big_Bold 'New Font("Arial", 10, FontStyle.Bold)

            .Styles.Alternate.BackColor = Color.FromArgb(234, 242, 252) '' Color.LightBlue
            .Styles.Alternate.ForeColor = Color.Black
            .Styles.Alternate.Border.Color = Drawing.SystemColors.Control

            .Styles.Normal.BackColor = Color.GhostWhite
            .Styles.Normal.ForeColor = Color.Black
            .Styles.Normal.Border.Color = Drawing.SystemColors.Control

            .Styles.Highlight.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Highlight.ForeColor = Color.Black

            .Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Focus.ForeColor = Color.Black

            'Dim clmnSelect As New DataColumn
            'With clmnSelect
            '    .ColumnName = "Select"
            '    .DataType = System.Type.GetType("System.Boolean")
            '    .DefaultValue = CBool("False")
            'End With
            '_dt.Columns.Add(clmnSelect)

            'Dim dt As New DataTable
            With _Flex
                .Visible = False

                '.Cols.Count = 12
                If _dt.Rows.Count > 0 Then
                    .Cols.Count = _dt.Columns.Count
                Else
                    MessageBox.Show("No data available")
                End If

                If _dt.Rows.Count > 0 Then
                    '.DataSource = _dt.DefaultView
                    .DataSource = _dv
                Else
                    MessageBox.Show("No data available")
                End If

                If _bSelectFlag = True Then
                    ' add column of check box
                    Dim clmnSelect1 As New DataColumn
                    With clmnSelect1
                        .ColumnName = "Select"
                        .DataType = System.Type.GetType("System.Boolean")
                        .DefaultValue = CBool("False")
                    End With
                    _dt.Columns.Add(clmnSelect1)
                    .Cols("Select").AllowEditing = True
                    .Cols("Select").Width = 0
                Else
                    With _Flex
                        .Cols(0).Width = 0
                        '.Cols(0).DataType = System.Type.GetType("System.Boolean")
                        '.Cols(0).Name = "Select"
                        '.SetData(0, 0, "Select")
                        '.Cols(0).AllowEditing = True
                    End With
                End If

                Dim colEdit
                For colEdit = 1 To _dt.Columns.Count - 1
                    If _dt.Columns(colEdit).ColumnName = "Select" Then
                        .Cols(colEdit).AllowEditing = True
                    Else
                        .Cols(colEdit).AllowEditing = False
                    End If
                Next

                .Visible = True
                .Refresh()

                ' fill combo
                Dim colCount As Integer
                For colCount = 0 To _dt.Columns.Count - 1
                    With cmbCOlumnName
                        .Items.Add(_dt.Columns(colCount).ColumnName)
                    End With
                Next

                Return _dt
            End With
        End With
    End Function

    ' function for data filter and view
    'ObjcustomGrid.SetDatasource(objMedicationDBLayer.DsDataview())
    'objMedicationDBLayer.SortDataview(objMedicationDBLayer.DsDataview.Table.Columns(0).ColumnName)
    'HideColumns(objMedicationDBLayer.DsDataview)
    'ReferralCount = objMedicationDBLayer.DsDataview.Count - 1

    Protected Sub _Flex_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.AfterEdit
        RaiseEvent _FlexAfterEdit(sender, e)
    End Sub

    Private Sub _Flex_CellButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellButtonClick
        RaiseEvent _FlexCellButtonClick(sender, e)
    End Sub

    Private Sub _Flex_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
            'If e.Button = Windows.Forms.MouseButtons.Right Then
            '    _Flex.ContextMenuStrip = cntListmenuStrip
            '    'Dim trvNode As myTreeNode
            '    'trvNode = CType(trPrescriptionDetails.GetNodeAt(e.X, e.Y), myTreeNode)
            '    ' If IsNothing(trvNode) = False Then
            '    With _Flex
            '        Dim r As Integer = .HitTest(e.X, e.Y).Row
            '        .Select(r, True)
            '        'trPrescriptionDetails.SelectedNode = trvNode
            '        If _Flex.Row <> 0 Then
            '            _Flex.ContextMenuStrip = cntListmenuStrip
            '        Else
            '            _Flex.ContextMenuStrip = Nothing
            '        End If
            '    End With

            'End If
            'RaiseEvent _FlexMouseDown(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub _Flex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.Click
        Try
            i = _Flex.Col
            If _Flex.GetData(0, i) <> "Select" Then
                lblColName.Text = CType((_Flex.GetData(0, i)), String)
                sColumnDataType = _Flex.Cols(i).DataType.ToString
            Else
                Dim selected_Items = _Flex.GetData(0, i)
            End If

            If sColumnDataType = "System.DateTime" Then
                MessageBox.Show("No search available on Date column", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                DateTimePicker1.Visible = False
                txtSearchFlexGrid.Visible = True
            End If

            ' Label1.Text = lblColName.Text
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _Flex_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.DoubleClick
        RaiseEvent _FlexDoubleClick(sender, e, _bSelectFlag)
    End Sub

    Private Sub _Flex_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseUp
        RaiseEvent _FlexMouseUp(sender, e)
    End Sub

    Private Sub txtSearchFlexGrid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchFlexGrid.TextChanged
        'RaiseEvent txtSearchFlexGridTextChanged(sender, e)
        Try
            Dim str As String = ""
            Dim rowid As Integer

            str = txtSearchFlexGrid.Text

            SortDataview(lblColName.Text)
            SetRowFilter(str)

            With _Flex
                rowid = .FindRow(str, 1, i, False, False, True)
                .Row = rowid
                'SortDataview(str)
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCloseRefill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRefill.Click
        RaiseEvent btnCloseRefillClick(sender, e)
    End Sub

    Private Sub cmbCOlumnName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCOlumnName.SelectedIndexChanged
        Label1.Text = cmbCOlumnName.Text
    End Sub

    Public Sub SortDataview(ByVal strsort As String)
        'Sort the Dataview on the first column by default
        If (IsNothing(_dv) = False) Then
            _dv.Sort = "[" & strsort & "]"
        End If

    End Sub

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Try
            Dim strexpr As String = ""
            Dim str As String
            If (IsNothing(_dv) = False) Then


                str = _dv.Sort
                'str = Splittext(str)
                str = Mid(str, 2)
                str = Mid(str, 1, Len(str) - 1)

                If sColumnDataType = "System.String" Then
                    strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
                ElseIf sColumnDataType = "System.DateTime" Then
                    Dim searchDt As DateTime
                    searchDt = CType(txtSearch, System.DateTime)
                    strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = '" & searchDt & "'"
                ElseIf sColumnDataType = "System.Int32" Then
                    Dim searchInt As Int32
                    searchInt = CType(txtSearch, System.Int32)
                    strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = " & searchInt & " "
                End If

                'strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"

                _dv.RowFilter = strexpr
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Function SetRowfilter() As DataView
        Try
            If _bSelectFlag = True Then
                If (IsNothing(_dv) = False) Then
                    Dim strexpr As String
                    strexpr = "" & _dv.Table.Columns("Select").ColumnName() & " = 1"

                    _dv.RowFilter = strexpr
                End If
              
                Return _dv
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    'Dim sColumnDataType = _Flex.Cols(Str).DataType.ToString
    '        If sColumnDataType = "System.String" Then
    '            strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
    '        ElseIf sColumnDataType = "System.DateTime" Then
    '            strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = '" & txtSearch & "%'"
    '        ElseIf sColumnDataType = "System.DateTime" Then
    '' strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
    '        End If

    'Private Function Splittext(ByVal strsplittext As String) As String
    '    If Trim(strsplittext) <> "" Then
    '        Dim arrstring() As String
    '        arrstring = Split(strsplittext, "-")
    '        Return arrstring(0)
    '    Else
    '        'Return ""
    '    End If
    'End Function
    
    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Dim strexpr
        If (IsNothing(_dv) = False) Then


            strexpr = "" & _dv.Table.Columns(lblColName.Text).ColumnName() & "  = '" & DateTimePicker1.Value & "'"

            Dim _dv_temp As New DataView
            _dv_temp.RowFilter = strexpr

            If _dv_temp.Count = 0 Then
                MessageBox.Show("No data Available for this search crieteria")
                Exit Sub
            Else
                _dv.RowFilter = strexpr
            End If
        End If
    End Sub

    Public Sub dateChecker(ByVal txtSearch As DateTime)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(_dv) = False) Then
            str = _dv.Sort
            'str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)

            'ElseIf sColumnDataType = "System.DateTime" Then       
            strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = '" & txtSearch & "'"

            _dv.RowFilter = strexpr
        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'InitializeComponent()
        '_Flex.Refresh()

    End Sub

    Private Sub btnUC_Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUC_Add.Click
        RaiseEvent btnUC_ADDclick(sender, e)
    End Sub
    
    Private Sub btnUC_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUC_Close.Click
        RaiseEvent btnUC_Cancelclick(sender, e)
    End Sub

    Private Sub btnUC_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUC_OK.Click
        RaiseEvent btnUC_OKclick(sender, e)
    End Sub
End Class
