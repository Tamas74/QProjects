Public Class gloUCGeneralSearch
    Private _RowIndex As Int32
    Private _RowsCount As Int32
    Public _dt As DataTable
    Public _bSelectFlag As Boolean
    'Public _flex As C1.Win.C1FlexGrid.C1FlexGrid
    Dim i As Integer
    Private WithEvents _dv As DataView
    ' Dim tempDt As New DataTable

    'Dim _dv As New DataView
    Dim sColumnDataType

    Private dvNext As DataView

    'sarika 13th oct 07
    'private varibles for add and edit functionality
    Private _blnAddEditFlag As Boolean = True
    '---------------------------------------------------
    Private _IsPharmacy As Boolean = False

    Public Event AfterTextSearch(ByVal dv As DataView, ByVal sScarchString As String)

    Public Property SearchString() As String
        Get
            Return txtSearch.Text
        End Get
        Set(ByVal value As String)
            txtSearch.Text = value
        End Set
    End Property
    Public Sub IntialiseDatatable(ByVal dttemp As DataTable)
        Try
            ' _dt = New DataTable
            _dt = dttemp
            '  tempDt = dttemp
            _dv = _dt.DefaultView
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            ValidateText(txtSearch.Text, e)
            If (e.KeyChar = ChrW(13)) Then
                'Dim rg As C1.Win.C1FlexGrid.CellRange = _Flex.GetCellRange(1, 1)


                '_Flex.Select(1, 1, 1, 1, True)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '05-May-15 Aniket: Resolving Bug #82983: EMR: Synopsis(PT history)- Search functionlity doe not work properly
    Public Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        Try
            Dim str As String = ""
            'Dim rowid As Integer
            Dim strSearchtext() As String
            str = txtSearch.Text
            str = Replace(str, "'", "''") & ""
            str = Replace(str, "[", "") & ""
            str = ReplaceSpecialCharacters(str)
            ''''
            dvNext = Nothing
            ' modified on 20070615 for null value
            If str.Trim <> "" Then
                '''' Pramod Multi Column Search for Pharmacy Contact Start
                Dim strRowFilter As System.Text.StringBuilder
                strSearchtext = Split(str, ",") '''' Split String on comma
                Dim strstring As String = ""
                If strSearchtext.Length = 1 Then '''' if search on only one column 
                    strstring = strSearchtext.GetValue(0)

                    strRowFilter = New System.Text.StringBuilder
                    ''Code Added by Mayuri:20090905
                    ''To fix Exception if dataview is empty ,BugId:3663
                    ''if dataview is empty
                    If IsNothing(_dv) Then
                        RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
                        Exit Sub
                    End If
                    If _dv.Table Is Nothing Then
                        RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
                        Exit Sub
                        ''if dataview contains only header
                    ElseIf _dv.Table.Rows.Count = 0 Then
                        RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
                        Exit Sub

                    End If
                    ''end Code
                    For icnt As Int32 = 0 To _dv.ToTable.Columns.Count - 1
                        Dim oColType As System.Type
                        oColType = _dv.ToTable.Columns.Item(icnt).DataType.UnderlyingSystemType
                        If oColType.FullName = "System.Date" Then
                            Continue For
                        ElseIf oColType.FullName = "System.Decimal" Then
                            Continue For
                        ElseIf oColType.FullName = "System.DateTime" Then
                            Continue For
                        End If
                        If oColType.FullName = "System.String" Then
                            If strRowFilter.Length > 0 Then
                                strRowFilter.Append(" or ")
                            End If
                            strRowFilter.Append("[" & _dv.ToTable.Columns(icnt).ColumnName & "]")
                            'strRowFilter.Append(_dv.ToTable.Columns(icnt).ColumnName)
                            strRowFilter.Append(" Like '%")
                            strRowFilter.Append(strstring)
                            strRowFilter.Append("%'")
                        End If
                    Next
                    ' _dv.RowFilter = _dv.ToTable.Columns("Name").ColumnName & " Like '%" & strstring & "%'  or " & _dv.ToTable.Columns("PhoneNo").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("FAX").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("AddressLine1").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("AddressLine2").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("City").ColumnName & " Like '%" & strstring & "%' or " & _dv.ToTable.Columns("State").ColumnName & " Like '%" & strstring & " ' or " & _dv.ToTable.Columns("ZipCode").ColumnName & " Like '%" & strstring & "%'"
                    _dv.RowFilter = strRowFilter.ToString
                Else '''' If search on multiple column 
                    Dim dttemp As DataTable = Nothing
                    For i = 0 To strSearchtext.Length - 1 '''' Loop if more than two column Search
                        strstring = Trim(strSearchtext.GetValue(i))
                        If strstring <> "" Then
                            If ((i = 0) OrElse (IsNothing(dvNext))) Then
                                If strSearchtext.GetValue(i) <> "" Then  '''' If second Column 
                                    _dv.RowFilter = ""
                                    dttemp = _dv.ToTable
                                    dvNext = dttemp.DefaultView
                                End If
                            Else
                                If strSearchtext.GetValue(i) <> "" Then '''' if Other than second column
                                    dttemp = dvNext.ToTable
                                    dvNext = dttemp.DefaultView
                                End If
                            End If
                            'dvNext.RowFilter = dvNext.ToTable.Columns("Name").ColumnName & " Like '%" & strstring & "%'  or " & dvNext.ToTable.Columns("PhoneNo").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("FAX").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("AddressLine1").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("AddressLine2").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("City").ColumnName & " Like '%" & strstring & "%' or " & dvNext.ToTable.Columns("State").ColumnName & " Like '%" & strstring & " ' or " & dvNext.ToTable.Columns("ZipCode").ColumnName & " Like '%" & strstring & "%'"
                            strRowFilter = New System.Text.StringBuilder
                            For icnt As Int32 = 0 To dvNext.ToTable.Columns.Count - 1
                                Dim oColType As System.Type
                                oColType = dvNext.ToTable.Columns.Item(icnt).DataType.UnderlyingSystemType
                                If oColType.FullName = "System.Date" Then
                                    Continue For
                                ElseIf oColType.FullName = "System.Decimal" Then
                                    Continue For
                                ElseIf oColType.FullName = "System.DateTime" Then
                                    Continue For
                                End If
                                If oColType.FullName = "System.String" Then
                                    If strRowFilter.Length > 0 Then
                                        strRowFilter.Append(" or ")
                                    End If
                                    strRowFilter.Append("[" & dvNext.ToTable.Columns(icnt).ColumnName & "]")
                                    'strRowFilter.Append(dvNext.ToTable.Columns(icnt).ColumnName)
                                    'Code commented & added by kanchan on 20101030 for bug 5341
                                    'strRowFilter.Append("Like '%")
                                    strRowFilter.Append(" Like '%")
                                    strRowFilter.Append(strstring)
                                    strRowFilter.Append("%'")
                                End If

                            Next
                            dvNext.RowFilter = strRowFilter.ToString

                        End If
                    Next
                    If (IsNothing(dttemp) = False) Then
                        dttemp.Dispose()
                        dttemp = Nothing
                    End If
                    If Not IsNothing(dvNext) Then ''changes made for bugid 86630
                        RaiseEvent AfterTextSearch(dvNext, txtSearch.Text)
                    Else
                        RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
                    End If

                    Exit Sub
                End If
                'With _Flex
                If strstring <> "" Then
                    If strSearchtext.Length = 1 Then '''' If only one column search 
                        ' .DataSource = _dv ' do not set datasource
                        'Raiseevent
                        RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
                    Else '''' If multiple column
                        If strSearchtext.GetValue(1) <> "" Then
                            '.DataSource = dvNext ' do not set datasource
                            RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
                        End If
                    End If
                End If
                'SetGridStyle()

                'End With
                '''' Pramod Multi Column Search for Pharmacy Contact End

            Else

                _dv.Sort = ""
                _dv.RowFilter = ""

                ' do not set datasource
                '_Flex.DataSource = _dv
                'SetGridStyle()
                'setColumnWidth()
                ' do not set datasource
                RaiseEvent AfterTextSearch(_dv, txtSearch.Text)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub ValidateText(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) OrElse ((e.KeyChar >= ChrW(35) AndAlso e.KeyChar <= ChrW(38)) OrElse (e.KeyChar = ChrW(64)) OrElse (e.KeyChar = ChrW(33)) OrElse (e.KeyChar = ChrW(42)) OrElse (e.KeyChar = ChrW(43)) OrElse (e.KeyChar = ChrW(45)) OrElse (e.KeyChar = ChrW(60)) OrElse (e.KeyChar = ChrW(59)) OrElse (e.KeyChar = ChrW(61)) OrElse (e.KeyChar = ChrW(94)) OrElse (e.KeyChar = ChrW(96)) OrElse (e.KeyChar = ChrW(124)) OrElse (e.KeyChar = ChrW(125)) OrElse (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Public Sub SortDataview(ByVal strsort As String)
    '    'Sort the Dataview on the first column by default
    '    _dv.Sort = "[" & strsort & "]"
    'End Sub

    'Public Sub SetRowFilter(ByVal txtSearch As String)
    '    Try
    '        Dim strexpr As String = ""
    '        Dim str As String = ""
    '        str = _dv.Sort
    '        'str = Splittext(str)
    '        str = Mid(str, 2)
    '        str = Mid(str, 1, Len(str) - 1)

    '        If sColumnDataType = "System.String" Then
    '            strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
    '        ElseIf sColumnDataType = "System.DateTime" Then
    '            Dim searchDt As DateTime
    '            searchDt = CType(txtSearch, System.DateTime)
    '            strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = '" & searchDt & "'"
    '        ElseIf sColumnDataType = "System.Int32" Then
    '            Dim searchInt As Int32
    '            searchInt = CType(txtSearch, System.Int32)
    '            strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  = " & searchInt & " "
    '        End If

    '        'strexpr = "" & _dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"

    '        _dv.RowFilter = strexpr
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try

            'Shubhangi 20091008
            'Use to clear search text box
            txtSearch.ResetText()
            txtSearch.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
