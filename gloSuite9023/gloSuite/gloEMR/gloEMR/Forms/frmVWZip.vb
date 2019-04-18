Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient

Public Class frmVWZip

    Dim oPatientReg As ClsPatientRegistrationDBLayer
    Public Shared blnModify As Boolean
    Dim _blnSearch As Boolean = True
    Dim ID As Long
    'Dim zipCode As Int64
    Dim zipCode As String
    Dim State As String
    Dim City As String
    Dim County As String
    Dim AreaCode As String

    Private Sub frmVWZip_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(oPatientReg) Then   ''added  for   bugid 71311
            oPatientReg.Dispose()
            oPatientReg = Nothing
        End If
    End Sub
    'Dim Defaultdv As DataView



    Private Sub frmVWZip_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtSearch.Focus()
        Try

            gloC1FlexStyle.Style(C1Zip)
            Dim dvZip As DataView
            'C1Zip.DataSource = oPatientReg.FetchZip() 'To fill the grid 
            ' oPatientReg.FetchZip()
            oPatientReg=New ClsPatientRegistrationDBLayer
            dvZip = oPatientReg.FetchZip()
           
            'If Not IsNothing(oPatientReg.GetDataview) Then
            If Not IsNothing(dvZip) Then
                'oPatientReg.SortDataview(oPatientReg.GetDataview.Table.Columns(1).ColumnName)
                oPatientReg.SortDataview(dvZip.Table.Columns(1).ColumnName)

            End If
            ' oPatientReg.Dispose()
            ' oPatientReg = Nothing
            C1Zip.DataSource = dvZip
            C1Zip.AllowSorting = AllowSortingEnum.SingleColumn
            CustomGridStyle() 'To design grid and fill grid with all zip entry

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    ''nID,ISNULL(Zip,0)As zip, City,ST,county
    'To design grid column

    'This is to set Grid Style
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try

            C1Zip.ScrollBars = ScrollBars.None
            'Dim dv As DataView
            'dv = oPatientReg.GetDataview


            'C1Zip.DataSource = dv
            C1Zip.Cols(0).Width = 0 'Hide nID column 
            'Code Added by Mayuri:20091027
            'To Fix Bug:#4453
            C1Zip.Cols("zip").Caption = "Zip"
            C1Zip.Cols("City").Caption = "City"
            C1Zip.Cols("City").Width = 150

            C1Zip.Cols("ST").Caption = "State"
            C1Zip.Cols("ST").Width = 150
            C1Zip.Cols("County").Caption = "County"
            C1Zip.Cols("AreaCode").Caption = "Area Code"
            'End Code Added by Mayuri:20091027
            'txtSearch.Text = ""
            'txtSearch.Text = strsearchtxt
          
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        C1Zip.ScrollBars = ScrollBars.Both
    End Sub

    'Event for Toolstrip Buttons
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddZip() 'To add new zip entry
            Case "Modify"
                Call UpdateZip() 'To update existing zip entry
            Case "Delete"
                Call DeleteZip() 'To delete zip entry
            Case "Refresh"
                Call RefreshZip() 'To refresh list after performing add/update/delete operation
            Case "Close"
                Call FormClose() 'To close the master zip form
        End Select
    End Sub

    'To add new zip entry 
    Public Sub AddZip()
        Try
            'Create form object and defining it's property
            Dim objfrmZip As New frmMSTZip()
            objfrmZip.Text = "Add ZIP"
            objfrmZip.StartPosition = FormStartPosition.CenterScreen
            If objfrmZip.ShowDialog(IIf(IsNothing(objfrmZip.Parent), Me, objfrmZip.Parent)) = Windows.Forms.DialogResult.Yes Then

                'Shubhangi 20090827 Take nID of added Field
                Dim nID As Int64
                nID = objfrmZip.ID

                ' 'To refresh list after performing add/update/delete operation
                RefreshZip()

                '' To Select The Newly Added row in the ZIP Table
                Dim rowIndex As Int64
                rowIndex = C1Zip.FindRow(nID, 1, 0, False, True, False)
                C1Zip.Select(rowIndex, 0, True)
            End If
            objfrmZip.Dispose()
            objfrmZip = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'To update existing zip entry
    Public Sub UpdateZip()

        Dim objfrmZip As frmMSTZip
        '    Dim C1Index As Integer

        'To get the current selected row details
        Try
            If C1Zip.Rows.Count > 1 Then
                blnModify = True

                ID = C1Zip.Item(C1Zip.Row, 0).ToString
                zipCode = C1Zip.Item(C1Zip.Row, 1).ToString
                City = C1Zip.Item(C1Zip.Row, 2).ToString
                State = C1Zip.Item(C1Zip.Row, 3).ToString
                County = C1Zip.Item(C1Zip.Row, 4).ToString
                AreaCode = C1Zip.Item(C1Zip.Row, 5).ToString

                'Pass the current selected row details to constructor
                objfrmZip = New frmMSTZip(ID, zipCode, City, County, State, AreaCode)
                objfrmZip.Text = "Update City"
                objfrmZip.StartPosition = FormStartPosition.CenterScreen
                If objfrmZip.ShowDialog(IIf(IsNothing(objfrmZip.Parent), Me, objfrmZip.Parent)) = Windows.Forms.DialogResult.Yes Then

                    'Shubhangi 20090827 Take nID of added Field
                    Dim nID As Int64
                    nID = objfrmZip.ID
                    RefreshZip()

                    'To refresh list after performing add/update/delete operation
                    Dim rowIndex As Int64
                    rowIndex = C1Zip.FindRow(nID, 1, 0, False, True, False)
                    C1Zip.Select(rowIndex, 0, True)

                End If
                objfrmZip.Dispose()
                objfrmZip = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
       
    End Sub

    'To delete the selected row from the database
    Public Sub DeleteZip()
        Dim ID As Long
        Dim SelectedRow As Integer = 0

        Try
            If C1Zip.Rows.Count > 1 Then

                If MessageBox.Show("Are you sure you want to delete this ZIP code?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    'ID = C1Zip.Rows(C1Zip.Row, 2).ToString  'get ID for the zip entry
                    ID = C1Zip.Item(C1Zip.Row, 0).ToString()
                    SelectedRow = C1Zip.RowSel  'To select index of selected row 
                    If IsNothing(oPatientReg) Then
                        oPatientReg = New ClsPatientRegistrationDBLayer
                    End If
                    oPatientReg.DeleteZIP(ID)
                    '  oPatientReg.Dispose()   commented for   bugid 71311
                    '  oPatientReg = Nothing
                    C1Zip.Rows.Remove(SelectedRow)
                    '  CType(C1Zip.DataSource, DataView).Item(SelectedRow - 1).Delete()
                    'C1Zip.DataSource = oPatientReg.FetchZip() 'Fetch the remaining entry form the database
                    'If Not IsNothing(oPatientReg.GetDataview) Then
                    '    oPatientReg.SortDataview(oPatientReg.GetDataview.Table.Columns(1).ColumnName)
                    'End If

                    'Dim sortOrder As String = CType(C1Zip.DataSource, DataView).Sort
                    'Dim strSearchstring As String = txtSearch.Text.Trim
                    'Dim arrcolumnsort() As String = Split(sortOrder, "]")
                    'Dim strcolumnName As String = arrcolumnsort.GetValue(0)
                    'Dim strsortorder As String = ""
                    'If arrcolumnsort.Length > 1 Then
                    '    strsortorder = arrcolumnsort.GetValue(1)

                    '    RefreshZip()
                    '    Dim rowIndex As Integer
                    '    Dim rowIndex1 As Integer
                    '    rowIndex = C1Zip.MouseRow

                    '    'If SelectedRow <= 1 Then
                    '    '    Dim nID As Integer = rowIndex - 1
                    '    '    rowIndex1 = C1Zip.FindRow(nID, 1, 0, False, True, False)
                    '    '    C1Zip.Select(rowIndex1, 0, True)
                    '    'Else
                    '    '    C1Zip.Select(SelectedRow - 1, 0, True)
                    '    '    'End If

                    '    CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                    '    If SelectedRow <= 1 Then
                    '        If C1Zip.Rows.Count > 0 Then
                    '            C1Zip.Select(1, 0, True)
                    '        End If
                    '    Else
                    '        C1Zip.Select(SelectedRow - 1, 0, True)
                    '    End If

                    'End If
                    '    'To design the grid and associate data to it 

                    'Else
                    '    ''C1Zip.GetData(C1Zip.Row).Focus()
                End If
            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    'To refresh grid  data 
    Public Sub RefreshZip()
        Try
            If IsNothing(oPatientReg) Then
                oPatientReg = New ClsPatientRegistrationDBLayer
            End If
            C1Zip.DataSource = oPatientReg.FetchZip() 'Fetch the data fr om the database
            CustomGridStyle() 'Design grid
            txtSearch.Clear()
            ' C1Zip.Focus()
            'oPatientReg.Dispose()   commented for   bugid 71311
            'oPatientReg = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    'To close the master zip chanage application
    Public Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    'Sorting of records by ASC/DEC
    Private Sub C1Zip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If C1Zip.Row >= 0 Then
            If (e.KeyChar = ChrW(13)) Then
                UpdateZip()
            End If
        End If
    End Sub

    'To take selected column name in lable
    Private Sub C1Zip_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Zip.MouseDoubleClick
        'To get the selected row
        txtSearch.Focus()
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As HitTestInfo = C1Zip.HitTest(ptPoint)

            If htInfo.Type = HitTestTypeEnum.ColumnHeader Then

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = HitTestTypeEnum.Cell Then
                _blnSearch = True
                UpdateZip()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Zip_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If C1Zip.Row >= 0 Then
            ' C1Zip.Select(C1Zip.Row)
        End If
    End Sub

    'This is for Search functionality on that text change event of text box
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        Try


            'Dim dvZip As DataView
            'Dim oClsPatientReg As New ClsPatientRegistrationDBLayer     'new class obj

            'If (txtSearch.Text.Trim.Length = 1) Then                ''Checking the text serch
            '    dvZip = oClsPatientReg.GetZIPCodes(txtSearch.Text.Trim.Replace("'", "''"))
            'ElseIf (txtSearch.Text.Trim.Length > 1) Then
            '    dvZip = CType(C1Zip.DataSource, DataView)
            'Else
            '    dvZip = oClsPatientReg.FetchZip()                   ''if there is no value it will insert default value
            'End If




            ''Use wait cursor till fill C!Zip B'coz Large No of records are present.


            ''Code Added by Mayuri:20091027
            ''To Fix Bug:#4463



            'Dim strSearchArray() As String
            'Dim sFilter As String = ""

            'C1Zip.Cursor = Cursors.WaitCursor
            ''  dvZip = CType(C1Zip.DataSource, DataView)

            'Dim strZipSearch As String = ""
            'If txtSearch.Text.Trim <> "" Then
            '    strZipSearch = txtSearch.Text.Trim
            '    strZipSearch = strZipSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
            'End If
            'If strZipSearch.Length > 1 Then
            '    Dim str As String = strZipSearch.Substring(1).Replace("%", "")
            '    strZipSearch = strZipSearch.Substring(0, 1) + str
            'End If
            'If strZipSearch.Trim <> "" Then
            '    strSearchArray = strZipSearch.Split(",")
            'End If
            ''Shubhangi 20091105
            ''Use , separated & on multiple column search
            'If strZipSearch.Trim <> "" Then
            '    If strSearchArray.Length = 1 Then
            '        strZipSearch = strSearchArray(0).Trim
            '        ''dvZip.RowFilter 
            '        sFilter = dvZip.Table.Columns(1).ColumnName & " Like '" & strZipSearch & "%' OR " _
            '                                  & dvZip.Table.Columns(2).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                  & dvZip.Table.Columns(3).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                  & dvZip.Table.Columns(4).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                  & dvZip.Table.Columns(5).ColumnName & " Like '%" & strZipSearch & "%' "
            '    Else
            '        For i As Integer = 0 To strSearchArray.Length - 1
            '            strZipSearch = strSearchArray(i).Trim
            '            If strZipSearch.Trim <> "" Then

            '                If i = 0 Then
            '                    sFilter = "(" & dvZip.Table.Columns(1).ColumnName & " Like '" & strZipSearch & "%' OR " _
            '                                     & dvZip.Table.Columns(2).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                     & dvZip.Table.Columns(3).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                     & dvZip.Table.Columns(4).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                     & dvZip.Table.Columns(5).ColumnName & " Like '%" & strZipSearch & "%')"
            '                ElseIf sFilter <> "" Then
            '                    sFilter = sFilter + " AND "
            '                    'End If
            '                    sFilter = sFilter & "(" & dvZip.Table.Columns(1).ColumnName & " Like '" & strZipSearch & "%' OR " _
            '                                          & dvZip.Table.Columns(2).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                          & dvZip.Table.Columns(3).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                          & dvZip.Table.Columns(4).ColumnName & " Like '%" & strZipSearch & "%' OR " _
            '                                          & dvZip.Table.Columns(5).ColumnName & " Like '%" & strZipSearch & "%')"
            '                End If
            '            End If

            '        Next

            '    End If
            'Else

            '    ''dvZip = oPatientReg.GetDataview
            '    sFilter = ""

            '    ''dvZip.RowFilter = ""

            'End If

            'If sFilter = "" Then                                ''checking when there is no data into the filter
            '    C1Zip.DataSource = dvZip
            '    CustomGridStyle()

            '    Exit Sub
            'Else
            '    dvZip.RowFilter = sFilter
            '    C1Zip.DataSource = dvZip                        ''dhruv---------------------------end

            'End If




            ''****************
            ''   string[] strSearchArray=null;
            ''  string sFilter = "";
            ''  DataView _dv = new DataView();
            ''_dv = (DataView)c1ViewContacts.DataSource;
            ''  c1ViewContacts.DataSource = _dv;
            ''  this.Cursor = Cursors.WaitCursor;
            ''  Try
            ''  {
            ''      string strSearch = txt_search.Text.Trim();

            ''     strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*","%");
            ''      If (strSearch.Length > 1) Then
            ''     {
            ''         string str = strSearch.Substring(1).Replace("%", "");
            ''         strSearch = strSearch.Substring(0, 1) + str;
            ''     }
            ''     if (strSearch.Trim() != "")
            ''     {
            ''         strSearchArray = strSearch.Split(',');
            ''     }
            ''****************

            ''dvZip.RowFilter = dvZip.Table.Columns(1).ColumnName & " Like '" & strZipSearch & "%' OR " _
            ''            & dvZip.Table.Columns(2).ColumnName & " Like '" & strZipSearch & "%' OR " _
            ''            & dvZip.Table.Columns(3).ColumnName & " Like '" & strZipSearch & "%' OR " _
            ''            & dvZip.Table.Columns(4).ColumnName & " Like '" & strZipSearch & "%' OR " _
            ''            & dvZip.Table.Columns(5).ColumnName & " Like '" & strZipSearch & "%'"

            '' C1Zip.DataSource = dvZip
            'C1Zip.Cols(0).Width = 0



            'If RefreshGrid = True And txtSearch.Text <> "" Then
            'If txtSearch.Text <> "" Then
            '    txtSearch.Clear()
            '    ' ON TEXT CLEAR THIS FUNCTION WILL CALL ON TEXT CHANGE, NO NEED TO EXECUTE CODE AGAIN
            '    Return
            'End If

            Dim oConnection As SqlConnection = New SqlConnection
            Dim sqlCmd As SqlCommand = New SqlCommand
            Dim da As SqlDataAdapter

            oConnection.ConnectionString = GetConnectionString()
            oConnection.Open()
            sqlCmd.CommandType = CommandType.Text

            Dim _SearchString As String = ""
            Dim strSearchArray As String() = Nothing
            Dim _SelectQuery As String = ""
            Dim _WhereQuery As String = ""

            _SelectQuery = " SELECT TOP(100) nID,ISNULL(Zip,'') As zip, ISNULL(City,'') AS City, ISNULL(ST,'') AS ST, ISNULL(county,'') AS county,  ISNULL(AreaCode,'') AS AreaCode FROM CSZ_MST "

            _SearchString = txtSearch.Text.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]")

            If _SearchString.StartsWith("*") = True Then
                _SearchString = _SearchString.Replace("*", "%")
            End If

            _SearchString = _SearchString.Replace("*", "[*]")

            If _SearchString.Trim() <> "" Then
                strSearchArray = _SearchString.Split(",")
            End If


            If _SearchString.Trim() <> "" Then

                If strSearchArray.Length = 1 Then
                    _SearchString = strSearchArray(0).Trim()
                    _WhereQuery = (((((_WhereQuery & " WHERE City Like '") + _SearchString & "%' OR " & " ST Like '") + _SearchString & "%' OR " & " zip Like '") + _SearchString & "%' OR " & " AreaCode Like '") + _SearchString & "%' OR " & " county Like '") + _SearchString & "%'"
                Else

                    'For Comma separated value search
                    For i As Integer = 0 To strSearchArray.Length - 1
                        _SearchString = strSearchArray(i).Trim()

                        'IF CONDITION IS COMMENTED COZ TO RESOLVE EXCEPTION IF , & THEN PUT ANY KEYWORDS
                        ' If _SearchString.Trim() <> "" Then
                        If i = 0 Then
                            _WhereQuery = _WhereQuery & " WHERE "   'Here i=0 means we have to concatinate WHERE coz from here we are applying WHERE condition

                        Else
                            _WhereQuery = _WhereQuery & " AND "    'Here i>0 means there is alerady WHERE is present but we want to add more condion for that Where so use AND to Concatinate where conditions

                        End If

                        'Above we just concatinate Where & And & actual condition will concatinate here
                        _WhereQuery = (((((_WhereQuery & " (City Like '") + _SearchString & "%' OR " & " ST Like '") + _SearchString & "%' OR " & " zip Like '") + _SearchString & "%' OR " & " AreaCode Like '") + _SearchString & "%' OR " & " county Like '") + _SearchString & "%') "
                        ' End If
                    Next

                End If
            End If



            Dim dtZip As DataTable = New DataTable()
            sqlCmd.CommandText = (_SelectQuery & " ") + _WhereQuery & " ORDER BY Zip"
            sqlCmd.Connection = oConnection
            da = New SqlDataAdapter(sqlCmd)

            da.Fill(dtZip)
            C1Zip.DataSource = dtZip
            CustomGridStyle()

            If sqlCmd IsNot Nothing Then
                sqlCmd.Parameters.Clear()
                sqlCmd.Dispose()
                sqlCmd = Nothing
            End If
            oConnection.Dispose()
            oConnection = Nothing
            da.Dispose()
            da = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            C1Zip.Cursor = Cursors.Default
        End Try
    End Sub

    



    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    '    If _blnSearch = True Then
    '        Try
    '            Me.Cursor = Cursors.WaitCursor
    '            Dim dvPatient As DataView

    '            dvPatient = CType(C1Zip.DataSource(), DataView)



    '            If IsNothing(dvPatient) Then
    '                Me.Cursor = Cursors.Default
    '                Exit Sub
    '            End If

    '            C1Zip.DataSource = dvPatient
    '            Dim strPatientSearchDetails As String
    '            If Trim(txtSearch.Text) <> "" Then
    '                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
    '                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
    '                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
    '                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
    '            Else
    '                strPatientSearchDetails = ""
    '            End If

    '            Select Case Trim(lblSearch.Text)
    '                Case "ZIP Code"
    '                    ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                    dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                    ''Else
    '                    ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
    '                    ''End If
    '                Case "City"
    '                    ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                    dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                    ''Else
    '                    ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
    '                    ''End If
    '                Case ("State")
    '                    ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                Case ("County")
    '                    dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"

    '                Case ("Area Code")
    '                    ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                    dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                Case ""
    '                    ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                    dvPatient.RowFilter = dvPatient.Table.Columns(6).ColumnName & " Like '%" & strPatientSearchDetails & "%'"


    '            End Select
    '            Me.Cursor = Cursors.Default
    '        Catch objErr As Exception
    '            Me.Cursor = Cursors.Default
    '            MessageBox.Show(objErr.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End If

    'End Sub

    'After Enter button click, the cursor should come on first record of C1Zip
    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If C1Zip.Rows.Count > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                C1Zip.Select()
                ' RefreshZip()
            End If
            If txtSearch.Text = "" Then
                RefreshZip()
            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930 
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()

    End Sub
End Class