Public Class gloUC_CustomSearchInC1Flexgrid

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
    Public Event btnUC_Modify_click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    Private _RowIndex As Int32
    Private _RowsCount As Int32
    Private _dt As DataTable = Nothing
    Public _bSelectFlag As Boolean

    Dim i As Integer
    Private WithEvents _dv As DataView = Nothing
    ' Dim tempDt As New DataTable


    Dim sColumnDataType
    'Private dttemp As DataTable = Nothing
    Private dvNext As DataView = Nothing

    Private _blnAddEditFlag As Boolean = True


    Private _IsPharmacy As Boolean = False

    Private ToolTippic_OK As New System.Windows.Forms.ToolTip
    Private ToolTippic_ADDNew As New System.Windows.Forms.ToolTip
    Private ToolTippic_Modify As New System.Windows.Forms.ToolTip
    Private ToolTippnl_Close As New System.Windows.Forms.ToolTip
    Private ToolTipbtnClearSearchFlexGrid As New System.Windows.Forms.ToolTip

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



    Public Property AddEditFlag() As Boolean
        Get
            Return _blnAddEditFlag
        End Get
        Set(ByVal value As Boolean)
            _blnAddEditFlag = value
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

    Public Sub New(ByVal dt As DataTable, Optional ByVal bSelectFlag As Boolean = False, Optional ByVal IsPharmacy As Boolean = False)
        '_dt = New DataTable
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        
        _dt = dt
        If (IsNothing(_dt)) Then
            Exit Sub
        End If

        'tempDt = dt
        _bSelectFlag = bSelectFlag
        If _bSelectFlag Then
            Dim clmnSelect1 As New DataColumn
            With clmnSelect1
                .ColumnName = "Select"
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = CBool("False")
            End With
            _dt.Columns.Add(clmnSelect1)
        End If
        _dv = _dt.DefaultView
        _IsPharmacy = IsPharmacy
        ' If _dt.Rows.Count > 0 Then
        '.DataSource = _dt.DefaultView
        _Flex.DataSource = _dv
        'Else
        '    MessageBox.Show("No data available")
        ' End If
        SetGridStyle()
        setlblTxtStyles()

        setColumnWidth()
        '_Flex.Cols(0).AllowEditing = True
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub setlblTxtStyles()
        With txtSearchFlexGrid
            .BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            .ForeColor = Drawing.SystemColors.WindowText
            .Width = 200
            .Height = 21
        End With

        With lblColName
            .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            .Height = 82
            .Width = 15
            '.BackColor = Color.FromArgb(207, 227, 254)
        End With
        With lblSearchString
            .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            .Height = 82
            .Width = 15
            .BackColor = Color.FromArgb(207, 227, 254)
        End With

        If _bSelectFlag = True Then
            With _Flex
                .Cols("Select").Width = 50
                .Cols("Select").DataType = System.Type.GetType("System.Boolean")
                '.Cols(0).Name = "Select"
                .SetData(0, 0, "Select")
                .Cols("Select").AllowEditing = True
            End With
        Else
            With _Flex
                .Cols(0).Width = 0
                .Cols(0).Visible = False
                '.Cols(0).DataType = System.Type.GetType("System.Boolean")
                '.Cols(0).Name = "Select"
                '.SetData(0, 0, "Select")
                '.Cols(0).AllowEditing = True
            End With
        End If
    End Sub

    Public Function SetGridStyle() As DataTable
        With _Flex
            .Redraw = False
            .Cols.Fixed = 0
            .Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            '.BackColor = System.Drawing.Color.White

            .Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250) '' Color.LightBlue
            .Styles.Alternate.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)

            .Styles.Editor.BackColor = Color.Beige
            .Styles.Editor.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Editor.ForeColor = Color.Black

            .Styles.EmptyArea.BackColor = Color.White
            .Styles.EmptyArea.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.EmptyArea.ForeColor = Color.FromArgb(31, 73, 125)


            .Styles.Fixed.BackColor = Color.FromArgb(86, 126, 211)
            .Styles.Fixed.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Fixed.ForeColor = Color.White

            .Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Focus.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Focus.ForeColor = Color.Black

            .Styles.Frozen.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Frozen.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Frozen.ForeColor = Color.Black

            .Styles.Highlight.BackColor = Color.FromArgb(255, 197, 108)
            .Styles.Highlight.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Highlight.ForeColor = Color.Black

            .Styles.NewRow.BackColor = Color.FromArgb(240, 247, 255)
            .Styles.NewRow.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.NewRow.ForeColor = Color.FromArgb(31, 73, 125)

            .Styles.Normal.BackColor = Color.FromArgb(240, 247, 255)
            .Styles.Normal.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)


            .Styles.Search.BackColor = Color.FromArgb(255, 197, 108)
            .Styles.Search.Border.Color = Color.FromArgb(159, 181, 221)
            .Styles.Search.ForeColor = Color.Black

            'Dim clmnSelect As New DataColumn
            'With clmnSelect
            '    .ColumnName = "Select"
            '    .DataType = System.Type.GetType("System.Boolean")
            '    .DefaultValue = CBool("False")
            'End With
            '_dt.Columns.Add(clmnSelect)

            'Dim dt As New DataTable
            If (IsNothing(_dt)) Then
                Return _dt
            End If
            With _Flex
                .Visible = False

                ''.Cols.Count = 12

                '' No Need TO Check Row Count 
                'If _dt.Rows.Count > 0 Then
                .Cols.Count = _dt.Columns.Count
                'Else
                'MessageBox.Show("No data available")
                'End If

                If _bSelectFlag = True Then
                    .Cols("Select").Move(0)
                    .Cols(1).Visible = False
                Else
                    With _Flex
                        .Cols(0).Visible = False

                        If _dt.Columns.Count >= 8 Then  'check for col count
                            ''GLO2011-0014767 : Quantity not being written out on prescriptions
                            If _Flex.Cols(7).Name = "Refills" Then
                                .Cols(7).Visible = True
                            Else
                                .Cols(7).Visible = False
                            End If
                            ''end
                        End If


                        '''''this condition is check when we call the Drug control in Pending Refill request form 
                        If _Flex.Cols.Count = 5 Then
                            If _Flex.Cols(4).Name = "NDCCode" Then
                                _Flex.Cols(4).Visible = False ''''hide the no 4 i.e. the NDCCode Col 
                            End If
                        End If

                        '''''this condition is check when we call the Drug control from RefillReqUserControl in Rx-Meds Form
                        If _Flex.Cols.Count = 6 Then
                            If _Flex.Cols(5).Name = "NDCCode" Then
                                _Flex.Cols(5).Visible = False ''''hide the no 5 i.e. the NDCCode Col 
                            End If
                        End If


                    End With
                End If

                .Visible = True
                .Refresh()
                .Redraw = True

                Return _dt
            End With
        End With
    End Function

    Public Sub ResizeColumnWidth(ByVal TotalWidth As Single)
        If (IsNothing(_dt)) Then
            Exit Sub
        End If
        Dim nColCount As Integer
        Dim i As Integer
        
        nColCount = _dt.Columns.Count

        _Flex.Width = TotalWidth

        Dim _Width As Single = (_Flex.Width - 20) / nColCount
        'set column value

        For i = 0 To nColCount - 1
            With _Flex
                '.Cols(i).Width = _Width * 2
                .Cols(i).Width = _Width
                If Not .Cols(i).Name = "Select" Then
                    .Cols(i).AllowEditing = False
                Else
                    .Cols(i).Width = 50
                End If
            End With
        Next
    End Sub

    Private Sub setColumnWidth()
        If (IsNothing(_dt)) Then
            Exit Sub
        End If
        Dim nColCount As Integer
        Dim i As Integer

        nColCount = _dt.Columns.Count

        Dim _Width As Single = (_Flex.Width - 20) / nColCount
        'set column value

        For i = 0 To nColCount - 1
            With _Flex
                '' adjust Column Width
                'If .Cols(i).Caption = "MiddleName" Then
                '    .Cols(i).Width = _Width * 1.2
                'Else

                '.Cols(i).Width = _Width * 2
                .Cols(i).Width = _Width
                'End If


                If Not .Cols(i).Name = "Select" Then
                    .Cols(i).AllowEditing = False
                Else
                    .Cols(i).Width = 50
                End If

                'CCHIT 08
                If .Cols(i).Name = "sDuration_SplitChr" Then
                    .Cols(i).Width = 0
                End If
                'CCHIT 08
                'this is with respect to the RefillRequest User Control
                If .Cols(i).Name = "DrugName" Then
                    .Cols(i).Width = _Width * 3
                End If
                If .Cols(i).Name = "DrugNameHidden" Then
                    .Cols(i).Width = 0
                End If
                'this is with respect to the RefillRequest User Control

                ''GLO2011-0014767 : Quantity not being written out on prescriptions
                If .Cols(i).Name = "sAmount_SplitChr" Then
                    .Cols(i).Width = 0
                End If
                ''end

            End With
        Next
        ''
        'With _Flex
        '    If _Flex.Cols.Contains("LastName") = True Then
        '        .Select(0, _Flex.Cols("LastName").Index, True)
        '        .ColSel = _Flex.Cols("LastName").Index
        '    End If
        'End With
        ''
    End Sub


    Protected Sub _Flex_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.AfterEdit
        RaiseEvent _FlexAfterEdit(sender, e)
    End Sub

    Private Sub _Flex_CellButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellButtonClick
        RaiseEvent _FlexCellButtonClick(sender, e)
    End Sub

    Private Sub _Flex_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                
                '_Flex.ContextMenuStrip = cntListmenuStrip
                'Dim trvNode As myTreeNode
                'trvNode = CType(trPrescriptionDetails.GetNodeAt(e.X, e.Y), myTreeNode)
                ' If IsNothing(trvNode) = False Then
                With _Flex
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    'MessageBox.Show(r)
                    .Select(r, True)
                    'trPrescriptionDetails.SelectedNode = trvNode
                    If AddEditFlag = True Then
                        If _Flex.Row > 0 Then
                            '_Flex.ContextMenuStrip = cntListmenuStrip ''Addded new menu with icon    ''old menue: cntListmenuStrip1
                        Else
                            'Try
                            '    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                            '        _Flex.ContextMenuStrip.Dispose()
                            '        _Flex.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            _Flex.ContextMenuStrip = Nothing
                            Exit Sub
                        End If
                    Else
                        'Try
                        '    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                        '        _Flex.ContextMenuStrip.Dispose()
                        '        _Flex.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        _Flex.ContextMenuStrip = Nothing
                        Exit Sub
                    End If
                    RaiseEvent _FlexMouseDown(sender, e)
                End With
            Else
                Exit Sub
            End If
            '_Flex.ContextMenu = ContextMenuStrip1

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'Try
        '    If e.Button = Windows.Forms.MouseButtons.Right Then
        '        _Flex.ContextMenuStrip = cntListmenuStrip
        '        'Dim trvNode As myTreeNode
        '        'trvNode = CType(trPrescriptionDetails.GetNodeAt(e.X, e.Y), myTreeNode)
        '        ' If IsNothing(trvNode) = False Then
        '        With _Flex
        '            Dim r As Integer = .HitTest(e.X, e.Y).Row
        '            .Select(r, True)
        '            'trPrescriptionDetails.SelectedNode = trvNode
        '            If _Flex.Row <> 0 Then
        '                _Flex.ContextMenuStrip = cntListmenuStrip
        '            Else
        '                _Flex.ContextMenuStrip = Nothing
        '            End If
        '        End With

        '    End If
        '    RaiseEvent _FlexMouseDown(sender, e)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

    End Sub

    Private Sub _Flex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.Click
        Try
            i = _Flex.Col
            Dim temp As Integer = 0
            temp = _Flex.HitTest.Row

            'Shubhangi 20091013
            'Commented by shubhangi 20091013 B'Coz qwe want general search


            'If temp > 0 Then
            '    If i > 0 Then
            '        'Shubhangi 20091013
            '        'Commented by shubhangi 20091013 B'Coz qwe want general search

            '        'If _Flex.GetData(0, i) <> "Select" Then
            '        '    lblColName.Text = CType((_Flex.GetData(0, i)), String)
            '        '    sColumnDataType = _Flex.Cols(i).DataType.ToString
            '        'Else
            '        '    Dim selected_Items = _Flex.GetData(0, i)
            '        'End If

            '        If sColumnDataType = "System.DateTime" Then
            '            MessageBox.Show("No search available on Date column", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        Else
            '            'DateTimePicker1.Visible = False
            '            txtSearchFlexGrid.Visible = True
            '        End If
            '    Else
            '        Exit Sub
            '    End If
            'End If
            ' Label1.Text = lblColName.Text
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _Flex_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.DoubleClick
        ' modified on 20070615 after test cases - no double click on header allow
        Dim temp As Integer
        temp = _Flex.HitTest.Row
        If temp > 0 Then
            RaiseEvent _FlexDoubleClick(sender, e, _bSelectFlag)
        End If
    End Sub

    Private Sub _Flex_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseUp
        RaiseEvent _FlexMouseUp(sender, e)
    End Sub

    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "[#]") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "[$]") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "[%]") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "[^]") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "[&]") & ""

            '' Was Commented Before 2090602
            '' Uncommneted By Mahesh to Handle the Special Char in search By Replacing char with '[Char]'
            '' Ref: http://sqlserver2000.databases.aspfaq.com/how-do-i-search-for-special-characters-e-g-in-sql-server.html
            strSpecialChar = Replace(strSpecialChar, "~", "[~]") & ""
            strSpecialChar = Replace(strSpecialChar, "!", "[!]") & ""
            strSpecialChar = Replace(strSpecialChar, "*", "[*]") & ""
            strSpecialChar = Replace(strSpecialChar, ";", "[;]") & ""
            strSpecialChar = Replace(strSpecialChar, "/", "[/]") & ""
            strSpecialChar = Replace(strSpecialChar, "?", "[?]") & ""
            strSpecialChar = Replace(strSpecialChar, ">", "[>]") & ""
            strSpecialChar = Replace(strSpecialChar, "<", "[<]") & ""
            strSpecialChar = Replace(strSpecialChar, "\", "[\]") & ""
            strSpecialChar = Replace(strSpecialChar, "|", "[|]") & ""
            strSpecialChar = Replace(strSpecialChar, "{", "[{]") & ""
            strSpecialChar = Replace(strSpecialChar, "}", "[}]") & ""
            strSpecialChar = Replace(strSpecialChar, "-", "[-]") & ""
            strSpecialChar = Replace(strSpecialChar, "_", "[_]") & ""
            ''END Was Commented Before 2090602
            Return strSpecialChar

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try


    End Function

    ''------------This Function is added by Anil on 20071213
    ''---This is added to validate the special characters in the search text box
    Public Sub ValidateText(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(45)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
            'If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
            '    e.Handled = True
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtSearchFlexGrid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchFlexGrid.KeyPress
        Try
            'ReplaceSpecialCharacters(txtSearchFlexGrid.Text)
            ValidateText(txtSearchFlexGrid.Text, e)
            If (e.KeyChar = ChrW(13)) Then
                'Dim rg As C1.Win.C1FlexGrid.CellRange = _Flex.GetCellRange(1, 1)
                _Flex.Focus()
                If _Flex.Rows.Count > 1 Then
                    _Flex.Select(1, 1, 1, 1, True)
                Else

                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''---------Added up to here

    Private Sub txtSearchFlexGrid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchFlexGrid.TextChanged
        'RaiseEvent txtSearchFlexGridTextChanged(sender, e)
        Try
            Dim str As String = ""
            'Dim rowid As Integer
            Dim strSearchtext() As String
            str = txtSearchFlexGrid.Text
            ''''code added by Anil on 27/10/2007 
            ''''this is added because The application was giving error for these characters.
            str = Replace(str, "[", "") & ""
            str = Replace(str, "'", "") & ""
            '''' added for solving sales force case-0006647
            str = Replace(str, "*", "") & ""
            If (str <> "%") Then
                str = Replace(str, "%", "") & ""
            End If
            '''' End
            ''''

            ' modified on 20070615 for null value
            If str.Trim <> "" Then
                '''' Pramod Multi Column Search for Pharmacy Contact Start
                If _IsPharmacy = True Then
                    strSearchtext = Split(str, ",") '''' Split String on comma
                    Dim strstring As String = ""
                    If strSearchtext.Length = 1 Then '''' if search on only one column 
                        strstring = strSearchtext.GetValue(0)
                        'ISNULL(sStreet, '') AS 'Address Line1',ISNULL(sAddressLine2, '') AS'Address Line2',
                        If (IsNothing(_dv) = False) Then
                            _dv.RowFilter = _dv.ToTable.Columns("Name").ColumnName & " Like '%" & strstring & "%'  or " & _dv.ToTable.Columns("PhoneNo").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("FAX").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("AddressLine1").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("AddressLine2").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("City").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("State").ColumnName & " Like '%" & strstring & " ' or " & _dv.ToTable.Columns("ZipCode").ColumnName & " Like '%" & strstring & "%'"
                        Else
                            _dv = New DataView()
                        End If

                    Else '''' If search on multiple column 
                        For i = 1 To strSearchtext.Length - 1 '''' Loop if more than two column Search
                            strstring = strSearchtext.GetValue(i)
                            If strstring <> "" Then
                                Dim dttemp As DataTable = Nothing
                                If i = 1 Then
                                    If strSearchtext.GetValue(i) <> "" Then  '''' If second Column 
                                        If (IsNothing(_dv) = False) Then
                                            dttemp = _dv.ToTable
                                        Else
                                            dttemp = New DataTable()
                                        End If

                                        dvNext = dttemp.Copy().DefaultView
                                    End If
                                Else
                                    If strSearchtext.GetValue(i) <> "" Then '''' if Other than second column
                                        If (IsNothing(dvNext) = False) Then

                                            dttemp = dvNext.ToTable
                                        Else
                                            dttemp = New DataTable()
                                        End If

                                        dvNext = dttemp.Copy().DefaultView
                                    End If
                                End If
                                If (IsNothing(dttemp) = False) Then
                                    dttemp.Dispose()
                                    dttemp = Nothing
                                End If
                                If (IsNothing(dvNext) = False) Then
                                    dvNext.RowFilter = dvNext.ToTable.Columns("Name").ColumnName & " Like '%" & strstring & "%'  or " & dvNext.ToTable.Columns("PhoneNo").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("FAX").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("AddressLine1").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("AddressLine2").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("City").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("State").ColumnName & " Like '%" & strstring & " ' or " & dvNext.ToTable.Columns("ZipCode").ColumnName & " Like '%" & strstring & "%'"
                                End If
                            End If
                        Next
                    End If
                    With _Flex
                        If strstring <> "" Then
                            If strSearchtext.Length = 1 Then '''' If only one column search 
                                .DataSource = _dv
                            Else '''' If multiple column
                                If strSearchtext.GetValue(1) <> "" Then
                                    .DataSource = dvNext
                                End If
                            End If
                        End If
                        SetGridStyle()
                        'setColumnWidth()
                    End With
                    '''' Pramod Multi Column Search for Pharmacy Contact End
                Else
                    'If lblColName.Text.Trim <> "" Then

                    'SortDataview(lblColName.Text)
                    SetRowFilter(str)
                    With _Flex
                        'i = .Cols(lblColName.Text).Index
                        'rowid = .FindRow(str, 1, i, False, False, True)
                        '.Row = rowid
                        'SortDataview(str)
                        .DataSource = _dv
                        ' SetGridStyle()
                        setColumnWidth()
                    End With
                    'Else
                    '    'code  modified 20070601 by Bipin
                    '    i = 2
                    '    lblColName.Text = _Flex.GetData(0, i)
                    '    SortDataview(lblColName.Text)
                    '    SetRowFilter(str)
                    '    With _Flex
                    '        i = .Cols(lblColName.Text).Index
                    '        rowid = .FindRow(str, 1, i, False, False, True)
                    '        .Row = rowid
                    '        'SortDataview(str)
                    '        .DataSource = _dv
                    '        'SetGridStyle()
                    '        setColumnWidth()
                    '    End With
                    'End If
                End If
            Else
                If (IsNothing(_dv) = False) Then
                    _dv.Sort = ""
                    _dv.RowFilter = ""
                End If
                
                _Flex.DataSource = _dv
                SetGridStyle()
                setColumnWidth()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCloseRefill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRefill.Click
        'RaiseEvent btnCloseRefillClick(sender, e)
        RaiseEvent btnUC_Cancelclick(sender, e)
    End Sub

    Private Sub cmbCOlumnName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Label1.Text = cmbCOlumnName.Text
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
            Dim str As String = ""
            Dim colcnt As Int32 = 0
            'str = _dv.Sort
            'str = Splittext(str)
            'str = Mid(str, 2)
            'str = Mid(str, 1, Len(str) - 1)
            'Shubhangi 20091013
            'In  string & general search

            If Not IsNothing(_dv) Then
                If _dv.Table.Columns.Count > 0 Then

                    colcnt = _dv.Table.Columns.Count

                    For i = 1 To _dv.Table.Columns.Count - 1
                        If colcnt = 4 Or colcnt = 5 Then
                            '\\ it is used in frmRxRequest.vb
                            If i = 1 Then
                                If txtSearch = "%" Then     ''''if condition added for solving sales force case-0006647
                                    strexpr = _dv.Table.Columns(i).ColumnName() & " Like '%[" & txtSearch & "]%'"     ''''if condition added for solving sales force case-0006647
                                Else
                                    strexpr = _dv.Table.Columns(i).ColumnName() & " Like '%" & txtSearch & "%'"
                                End If
                            Else
                                If txtSearch = "%" Then     ''''if condition added for solving sales force case-0006647
                                    strexpr = strexpr + " OR " & _dv.Table.Columns(i).ColumnName() & " Like '%[" & txtSearch & "]%'"    ''''if condition added for solving sales force case-0006647
                                Else
                                    strexpr = strexpr + " OR " & _dv.Table.Columns(i).ColumnName() & " Like '%" & txtSearch & "%'"
                                End If
                            End If

                        Else
                            '\\ it is used in frmHL7MessageQueue.vb
                            If i = 1 Then
                                Continue For
                            ElseIf i = 2 Then
                                strexpr = _dv.Table.Columns(i).ColumnName() & " Like '%" & txtSearch & "%'"
                            Else
                                strexpr = strexpr + " OR " & _dv.Table.Columns(i).ColumnName() & " Like '%" & txtSearch & "%'"
                            End If

                        End If

                    Next

                    ''strexpr = _dv.Table.Columns(2).ColumnName() & " Like '%" & txtSearch & "%' OR " _
                    ''        & _dv.Table.Columns(3).ColumnName() & " Like '%" & txtSearch & "%' OR " _
                    ''        & _dv.Table.Columns(4).ColumnName() & " Like '%" & txtSearch & "%' OR " _
                    ''        & _dv.Table.Columns(5).ColumnName() & " Like '%" & txtSearch & "%' OR " _
                    ''        & _dv.Table.Columns(6).ColumnName() & " Like '%" & txtSearch & "%' OR " _
                    ''        & _dv.Table.Columns(7).ColumnName() & " Like '%" & txtSearch & "%' "

                End If
                _dv.RowFilter = strexpr
            End If



            'Shubhangi 20091013 Commented by shubhangi
            'If sColumnDataType = "System.String" Then
            '    strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            '    'Shubhangi 20091013 Commented by shubhangi
            '    'ElseIf sColumnDataType = "System.DateTime" Then
            '    '    Dim searchDt As DateTime
            '    '    searchDt = CType(txtSearch, System.DateTime)
            '    '    strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = '" & searchDt & "'"
            'ElseIf sColumnDataType = "System.Int32" Then
            '    Dim searchInt As Int32
            '    searchInt = CType(txtSearch, System.Int32)
            '    strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = " & searchInt & " "
            'End If

            'strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Function SetRowfilter() As DataView
        Try
            If _bSelectFlag = True Then
                Dim strexpr As String
                SortDataview("Select")
                If (IsNothing(_dv) = False) Then
                    _dv.Table.AcceptChanges()
                    strexpr = "" & _dv.Table.Columns("Select").ColumnName() & " =1"
                    _dv.RowFilter = strexpr
                End If
               
                Return _dv
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function



    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strexpr As String = ""
        'strexpr = "" & _dv.Table.Columns(lblColName.Text).ColumnName() & "  = '" & DateTimePicker1.Value & "'"

        Dim _dv_temp As New DataView
        _dv_temp.RowFilter = strexpr

        If _dv_temp.Count = 0 Then
            MessageBox.Show("No data Available for this search crieteria", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            _dv_temp.Dispose()
            _dv_temp = Nothing
            Exit Sub
        Else
            _dv_temp.Dispose()
            _dv_temp = Nothing
            If (IsNothing(_dv) = False) Then
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'InitializeComponent()
        '_Flex.Refresh()

    End Sub

    Private Sub btnUC_Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUC_Add.Click

        RaiseEvent btnUC_ADDclick(sender, e)

    End Sub

    Private Sub btnUC_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUC_Close.Click
        RaiseEvent btnUC_Cancelclick(sender, e)
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pic_ADDNew.Click

        RaiseEvent btnUC_ADDclick(sender, e)

    End Sub

    Private Sub btnUC_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUC_OK.Click
        'code commented by sarika 31 st oct 07
        'RaiseEvent btnUC_OKclick(sender, e)
        'code added by sarika 31 st oct 07
        If _Flex.Row > 0 Then
            RaiseEvent btnUC_OKclick(sender, e)
        End If

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pic_OK.Click
        'RaiseEvent btnUC_OKclick(sender, e)

        'code commented by sarika 31st oct 07
        'If _bSelectFlag = False Then
        '    RaiseEvent _FlexDoubleClick(sender, e, _bSelectFlag)
        'Else
        '    RaiseEvent btnUC_OKclick(sender, e)
        'End If
        '---

        'code added by sarika 31st oct 07
        If _Flex.Row > 0 Then
            If _bSelectFlag = False Then
                RaiseEvent _FlexDoubleClick(sender, e, _bSelectFlag)
            Else
                RaiseEvent btnUC_OKclick(sender, e)
            End If
        End If

    End Sub

    Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click, ModifySigInfoToolStripMenuItem.Click
        RaiseEvent btnUC_ADDclick(sender, e)
    End Sub

    Private Sub pnl_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnl_Close.Click
        RaiseEvent btnUC_Cancelclick(sender, e)
    End Sub

    



    Private Sub pic_Modify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pic_Modify.Click
        'code commented by sarika 31st oct 07
        'RaiseEvent btnUC_Modify_click(sender, e)

        'code added by sarika 31st oct 07
        If _Flex.Row > 0 Then
            RaiseEvent btnUC_Modify_click(sender, e)
        End If

    End Sub

    Private Sub gloUC_CustomSearchInC1Flexgrid_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

        ToolTippic_OK.Dispose()
        ToolTippic_OK = Nothing

        ToolTippic_ADDNew.Dispose()
        ToolTippic_ADDNew = Nothing

        ToolTippic_Modify.Dispose()
        ToolTippic_Modify = Nothing

        ToolTippnl_Close.Dispose()
        ToolTippnl_Close = Nothing

        ToolTipbtnClearSearchFlexGrid.Dispose()
        ToolTipbtnClearSearchFlexGrid = Nothing

    End Sub

  




    Private Sub gloUC_CustomSearchInC1Flexgrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        gloC1FlexStyle.Style(_Flex)

        'bydfault set the focus to the search text box when called from the frmRxRequest from
        txtSearchFlexGrid.Select()
        txtSearchFlexGrid.Focus()
        'bydfault set the focus to the search text box when called from the frmRxRequest from
        ''
        'sarika 18th oct 07
        'if the add edit facility is disabled then it should not show the add button
        If AddEditFlag = False Then
            Me.pic_ADDNew.Visible = False
            Me.pnl_NextAddNew.Visible = False
            Me.pic_Modify.Visible = False
            Me.pnl_NextModify.Visible = False
        End If
        '------------------

        With _Flex

            If _Flex.Cols.Contains("LastName") = True Then
                '.Select(0, _Flex.Cols("LastName").Index, True)
                '.ColSel = _Flex.Cols("LastName").Index
                lblColName.Text = "LastName"
                i = _Flex.Cols("LastName").Index
                sColumnDataType = _Flex.Cols(i).DataType.ToString

                SortDataview(lblColName.Text)

            ElseIf _Flex.Cols.Contains("UserName") = True Then
                lblColName.Text = "UserName"
                i = _Flex.Cols("UserName").Index
                sColumnDataType = _Flex.Cols(i).DataType.ToString

                SortDataview(lblColName.Text)

            ElseIf _Flex.Cols.Contains("Name") = True Then
                lblColName.Text = "Name"
                i = _Flex.Cols("Name").Index
                sColumnDataType = _Flex.Cols(i).DataType.ToString

                SortDataview(lblColName.Text)


                'code added by sarika 1 oct 07

                'In the above If we check  the columns like Last name and user name and when this user ctrl loads the default column would be set accordingly
                'But for the dataviews which do not having any of the above columns on every form where the dataview is mapped to this user control
                'sort the dataview according to the default column u want to set using the SortDataview method of this control on the indivisual forms
                'from where this user control is loaded . the column on which the dataview would be added 
                'if the sortdataview is not called on the respective form then here the default column would be
                'the 2nd column of the dataview if select flag is true since the 1st column would be id and
                'the 1st column of the dataview if select flag is false since here the oth column would be id 
            Else
                'check whether the SortDataView is called or not
                Dim str As String = ""
                If (IsNothing(_dv) = False) Then
                    str = _dv.Sort
                    'str = Splittext(str)
                    'str = Mid(str, 2)
                    'str = Mid(str, 1, Len(str) - 1)

                    str = Replace(str, "[", "")
                    str = Replace(str, "]", "")
                    str = Trim(str)

                    If str <> "" Then
                        'the sortdataview has been called on the calling form

                        lblColName.Text = str
                        i = _Flex.Cols(str).Index

                    Else
                        'the sortdataview has not been called on the calling form
                        'sort on the first column shown by default 
                        'check for the select flag
                        If _bSelectFlag = True Then
                            'chk for the column count
                            '1st column is id
                            'get the column count
                            If _dv.Table.Columns.Count >= 3 Then
                                lblColName.Text = _dv.Table.Columns(1).ColumnName
                                i = _Flex.Cols(lblColName.Text).Index

                            End If
                        Else
                            'chk for the column count
                            '0th column is id
                            'get the column count

                            If _dv.Table.Columns.Count >= 2 Then
                                lblColName.Text = _dv.Table.Columns(1).ColumnName
                                i = _Flex.Cols(lblColName.Text).Index

                            End If
                        End If

                        SortDataview(lblColName.Text)
                        sColumnDataType = _Flex.Cols(i).DataType.ToString

                    End If
                End If

                '----------------------------------
                End If
                If _IsPharmacy Then
                    Label2.Text = "Search :"
                    pnlColName.Visible = False
                End If
        End With


        ToolTippic_OK.SetToolTip(Me.pic_OK, "OK")
        ToolTippic_ADDNew.SetToolTip(Me.pic_ADDNew, "Add")
        ToolTippic_Modify.SetToolTip(Me.pic_Modify, "Modify")
        ToolTippnl_Close.SetToolTip(Me.pnl_Close, "Close")
        ToolTipbtnClearSearchFlexGrid.SetToolTip(Me.btnClearSearchFlexGrid, "Clear Search")

    End Sub

    Private Sub cntListmenuStrip_EndDrag(ByVal sender As Object, ByVal e As System.EventArgs) Handles cntListmenuStrip1.EndDrag, cntListmenuStrip.EndDrag

    End Sub

    'sarika 3rd oct 07
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click, RemoveDrugToolStripMenuItem.Click
        RaiseEvent btnUC_Modify_click(sender, e)
    End Sub
    '----------------------------
    Private Sub btnUC_Mod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUC_Mod.Click
        'code commented by sarika 31st oct 07
        'RaiseEvent btnUC_Modify_click(sender, e)

        'code commented by sarika 31st oct 07
        If _Flex.Row > 0 Then
            RaiseEvent btnUC_Modify_click(sender, e)
        End If

    End Sub

    Private Sub _Flex_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _Flex.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            If _Flex.Row > 0 Then
                If _bSelectFlag = False Then
                    RaiseEvent _FlexDoubleClick(sender, e, _bSelectFlag)
                Else
                    RaiseEvent btnUC_OKclick(sender, e)
                End If
            End If
        End If
    End Sub

   


    Private Sub btnClearSearchFlexGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearchFlexGrid.Click
        txtSearchFlexGrid.ResetText()
        txtSearchFlexGrid.Focus()
    End Sub

   
End Class
