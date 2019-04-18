Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class frmAddDictionary
   

    ''Column used in Flexgrid
    Private Col_Select As Integer = 0
    Private Col_ID As Integer = 1
    Private Col_sFieldName As Integer = 2
    Private col_Hidden_TableName As Integer = 3
    Private Col_sTableName As Integer = 4
    Private Col_sCaption As Integer = 5
    Private Col_sTableCaption As Integer = 6
    Private Col_TableNamecaption As Integer = 7
    Private Col_Count As Integer = 8

    ' Dim cmnu As ContextMenu
    Dim flag As Boolean
    'variable used for arraylist of data dictionary fields added
    Dim ArrlistDataDictionary As New ArrayList

    Private Sub frmAddDictionary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Commented by Rahul Patel on 03-01-2010
        'Due UnUsed code
        ''Set the style of flexgrid  
        'Call setGridStyle()
        ''Bind data to flexgrid
        'Call Binddata()
        'end of code Commented  by Rahul Patel on 03-01-2010

        Fill_DataDictionary()
    End Sub

    'Commented by Rahul Patel on 03-01-2010
    'Due UnUsed code
    'Public Sub setGridStyle()

    '    gloC1FlexStyle.Style(C1Dictonary)

    '    With C1Dictonary
    '        .Rows.Fixed = 0
    '        .Cols.Count = Col_Count
    '        Dim _TotalWidth As Single = .Width - 5
    '        .Cols.Fixed = 0
    '        .Rows.Count = 1
    '        '.AllowEditing = False

    '        ''select column
    '        .Cols(Col_Select).Width = 0 ' _TotalWidth * 0.1
    '        '.SetData(0, Col_Select, "Select")
    '        .Cols(Col_Select).AllowEditing = True
    '        .Cols(Col_Select).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        ''ID
    '        .Cols(Col_ID).Width = 0
    '        '.SetData(0, Col_ID, "ID")
    '        .Cols(Col_ID).AllowEditing = False
    '        .Cols(Col_ID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        ''Field Name
    '        .Cols(Col_sFieldName).Width = 0
    '        '.SetData(0, Col_sFieldName, "Field Name")
    '        .Cols(Col_sFieldName).AllowEditing = False
    '        .Cols(Col_sFieldName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        ''Table Name
    '        .Cols(col_Hidden_TableName).Width = 0
    '        '.SetData(0, col_Hidden_TableName, "Hidden Table Name")
    '        .Cols(col_Hidden_TableName).AllowEditing = False
    '        .Cols(col_Hidden_TableName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        ''Table Name
    '        .Cols(Col_sTableName).Width = 0  '_TotalWidth * 0.43
    '        '.SetData(0, Col_sTableName, "Table Name")
    '        .Cols(Col_sTableName).AllowEditing = False
    '        .Cols(Col_sTableName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


    '        ''Caption
    '        .Cols(Col_sCaption).Width = 0   ' _TotalWidth * 0.44
    '        '.SetData(0, Col_sCaption, "Caption")
    '        .Cols(Col_sCaption).AllowEditing = False
    '        .Cols(Col_sCaption).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        ''Table Caption
    '        .Cols(Col_sTableCaption).Width = 0
    '        '.SetData(0, Col_sTableCaption, "Table Caption")
    '        .Cols(Col_sTableCaption).AllowEditing = False
    '        .Cols(Col_sCaption).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

    '        ''Treeview column  
    '        .Cols(Col_TableNamecaption).Width = _TotalWidth * 0.96
    '        '.SetData(0, Col_TableNamecaption, "Table Name")
    '        .Cols(Col_TableNamecaption).AllowEditing = True
    '        .Cols(Col_TableNamecaption).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


    '    End With
    'End Sub

    'Public Sub Binddata()
    '    Try
    '        Dim strSelectQry As String = "SELECT * FROM DataDictionary_MST order by sTableCaption asc"
    '        dt = New DataTable
    '        oDB = New DataBaseLayer
    '        '' Bind data table with data 
    '        dt = oDB.GetDataTable_Query(strSelectQry)
    '        Dim i As Integer = 0
    '        Dim j As Integer = 0
    '        Dim _Row As Integer = 0

    '        With C1Dictonary
    '            'set the property of treeColumn
    '            .Tree.Column = Col_TableNamecaption
    '            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '            '.Tree.LineStyle = Drawing2D.DashStyle.Solid
    '            .Tree.LineStyle = Drawing2D.DashStyle.Custom
    '            .Tree.Indent = 30


    '            For i = 0 To dt.Rows.Count - 1
    '                'If the category is not present 
    '                If UCase(.GetData(.Rows.Count - 1, col_Hidden_TableName)) <> UCase(dt.Rows(i)("sTableCaption")) Then
    '                    If .Rows.Count <> 1 Then
    '                        .Rows.Add()
    '                    End If

    '                    _Row = .Rows.Count - 1
    '                    'set the property and data to newly added row
    '                    With .Rows(_Row)
    '                        .AllowEditing = False
    '                        .ImageAndText = True
    '                        .Height = 24
    '                        .IsNode = True
    '                        .Node.Expanded = False
    '                        .Node.Level = 0
    '                        .Node.Data = dt.Rows(i)("sTableCaption")
    '                        .Node.Image = Global.gloEMR.My.Resources.Resources.Table
    '                    End With
    '                    'set the data to other flexgrid column which is used for reference 
    '                    .SetData(_Row, col_Hidden_TableName, dt.Rows(i)("sTableCaption"))
    '                    .SetData(_Row, Col_ID, dt.Rows(i)("nDictionaryID"))
    '                    .SetData(_Row, Col_sFieldName, dt.Rows(i)("sFieldName"))
    '                    .SetData(_Row, Col_sCaption, dt.Rows(i)("sCaption"))
    '                    .SetData(_Row, Col_sTableCaption, dt.Rows(i)("STableCaption"))



    '                    ''select the caption for selected category
    '                    Dim strSelectQry_CAP As String = "SELECT * FROM DataDictionary_MST WHERE sTableCaption = '" & dt.Rows(i)("sTableCaption") & "' "
    '                    Dim dt_CAP As New DataTable
    '                    oDB = New DataBaseLayer
    '                    '' Bind data table with data 
    '                    dt_CAP = oDB.GetDataTable_Query(strSelectQry_CAP)
    '                    For k As Integer = 0 To dt_CAP.Rows.Count - 1
    '                        .Rows.Add()
    '                        _Row = .Rows.Count - 1
    '                        With .Rows(_Row)
    '                            .AllowEditing = True
    '                            .ImageAndText = True
    '                            .Height = 24
    '                            .IsNode = True
    '                            .Node.Level = 1
    '                            .Node.Data = dt_CAP.Rows(k)("sCaption")
    '                        End With
    '                        Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, Col_Select, _Row, Col_Select)
    '                        rgActive.StyleNew.DataType = GetType(Boolean)
    '                        rgActive.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
    '                        rgActive.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter

    '                        .SetCellCheck(_Row, Col_TableNamecaption, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
    '                        .SetData(_Row, col_Hidden_TableName, dt_CAP.Rows(k)("sTableCaption"))
    '                        .SetData(_Row, Col_ID, dt_CAP.Rows(k)("nDictionaryID"))
    '                        .SetData(_Row, Col_sFieldName, dt_CAP.Rows(k)("sFieldName"))
    '                        .SetData(_Row, Col_sCaption, dt_CAP.Rows(k)("sCaption"))
    '                        .SetData(_Row, Col_sTableCaption, dt_CAP.Rows(k)("STableCaption"))
    '                    Next
    '                End If
    '            Next
    '        End With

    '        For i = 0 To C1Dictonary.Rows.Count - 1
    '            Dim nd As C1.Win.C1FlexGrid.Node = C1Dictonary.Rows(i).Node
    '            If nd.Level = 0 Then
    '                nd.Collapsed = True
    '                flag = False
    '            End If
    '        Next


    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Add Data Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    'Private Sub Expand_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer
    '    Try
    '        For i = 0 To C1Dictonary.Rows.Count - 1
    '            Dim nd As C1.Win.C1FlexGrid.Node = C1Dictonary.Rows(i).Node
    '            If nd.Level = 0 Then
    '                nd.Expanded = True
    '                flag = True
    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub contextmenu()
    '    Try
    '        If Not IsNothing(cmnu) Then
    '            cmnu.Dispose()
    '        End If
    '        cmnu = New ContextMenu()
    '        If flag = False Then
    '            Dim menuExpand As New MenuItem
    '            menuExpand.Text = "Expand All"
    '            cmnu.MenuItems.Add(menuExpand)
    '            AddHandler menuExpand.Click, AddressOf Expand_click
    '        ElseIf flag = True Then
    '            Dim menuCollapse As New MenuItem
    '            menuCollapse.Text = "Collapse All"
    '            cmnu.MenuItems.Add(menuCollapse)
    '            AddHandler menuCollapse.Click, AddressOf Collapse_click
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub Collapse_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer
    '    Try
    '        For i = 0 To C1Dictonary.Rows.Count - 1
    '            Dim nd As C1.Win.C1FlexGrid.Node = C1Dictonary.Rows(i).Node
    '            If nd.Level = 0 Then
    '                nd.Collapsed = True
    '                flag = False
    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub AddDataFields()
    '    Dim i As Integer = 0
    '    Dim ArrlistDataDictionary As New ArrayList

    '    For i = 1 To C1Dictonary.Rows.Count - 1
    '        '' If item is check then add it to arraylist
    '        If C1Dictonary.GetCellCheck(i, Col_TableNamecaption) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    '            Dim listData As New myList
    '            ''ID  
    '            listData.ID = C1Dictonary.GetData(i, Col_ID)
    '            ''Field Name
    '            listData.Code = C1Dictonary.GetData(i, Col_sFieldName)
    '            ''Table Name
    '            listData.ParameterName = C1Dictonary.GetData(i, col_Hidden_TableName)
    '            ''Caption
    '            listData.Description = C1Dictonary.GetData(i, Col_sCaption)
    '            ''Table Caption
    '            listData.Reaction = C1Dictonary.GetData(i, Col_sTableCaption)

    '            ArrlistDataDictionary.Add(listData)
    '            listData = Nothing
    '        End If
    '    Next
    '    frmPatientExam.Arrlist = ArrlistDataDictionary
    '    'code comented by dipak 20091001 No need to use frmPatientExam.blnIncludeCaption  as IncludeCaption property procedure is used
    '    'If chckCaption.Checked Then
    '    '    frmPatientExam.blnIncludeCaption = True
    '    'Else
    '    '    frmPatientExam.blnIncludeCaption = False
    '    'End If
    '    'no need to use frmPatientExam.blnChangesMad as form return boolean variable to indicate changes made ao not
    '    'blnChangesMade=true is commented because DialogResult is used for track changes made or not
    '    'frmPatientExam.blnChangesMade = True
    '    'ArrlistDataDictionary is commeted because it required on other form.
    '    'ArrlistDataDictionary = Nothing
    '    'end comment by diipak 200901001
    '    Me.Close()
    'End Sub

    'Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
    '    Try
    '        If (e.KeyChar = ChrW(13)) Then
    '            With C1Dictonary
    '                If .RowSel >= 0 Then
    '                    .Select()
    '                    'CurrentRowIndex = 0
    '                End If
    '            End With

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
    '    Try

    '        Dim strSearch As String
    '        With txtsearch
    '            If Trim(.Text) <> "" Then
    '                strSearch = Replace(.Text, "'", "''")
    '            Else
    '                strSearch = ""
    '            End If
    '        End With

    '        With C1Dictonary
    '            .Row = .FindRow(strSearch, 1, Col_TableNamecaption, False, False, True)
    '        End With

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'Call Binddata()
    'end of code Commented  by Rahul Patel on 03-01-2010

    Private Sub tlsDictionary_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDictionary.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Ok"
                ' AddDataFields()
                AddDataFields_new()
            Case "Cancel"
                'blnChangesMade=false is commented because DialogResult is used for track changes made or not
                'frmPatientExam.blnChangesMade = False
                'No-for changes not made
                Me.DialogResult = Windows.Forms.DialogResult.No
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    'Private Sub C1Dictonary_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        contextmenu()
    '        C1Dictonary.ContextMenu = cmnu
    '    End If
    'End Sub

    'Private Sub C1Dictonary_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    'Public Shared Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point)
    '    Try
    '        Dim myFont As Font = oGrid.Font
    '        Dim stringsize As SizeF
    '        Dim colsize As Integer = 0
    '        Dim sText As String = ""
    '        Dim nRow As Integer
    '        Dim nCol As Integer

    '        If oGrid.MouseCol > -1 And oGrid.MouseRow > -1 Then
    '            oC1ToolTip.Font = myFont
    '            oC1ToolTip.MaximumWidth = 400

    '            nRow = oGrid.MouseRow
    '            nCol = oGrid.MouseCol

    '            If nRow > 0 Then 'And nCol > 0 Then
    '                If Not oGrid.GetData(nRow, nCol) Is Nothing Then
    '                    sText = oGrid.GetData(nRow, nCol)
    '                End If
    '                colsize = oGrid.Cols(nCol).WidthDisplay
    '            End If
    '            Dim oGrp As Graphics = oGrid.CreateGraphics()
    '            stringsize = oGrp.MeasureString(sText, myFont)

    '            If stringsize.Width > colsize Then
    '                oC1ToolTip.SetToolTip(oGrid, sText)
    '            Else
    '                oC1ToolTip.SetToolTip(oGrid, "")
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub C1Dictonary_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    'End Sub

    ''Sandip Darade 20090601
    ''Fill the datadictionary fields to treeview control
    Private Sub Fill_DataDictionary()
        Dim strSelectQry As String = "SELECT nDictionaryID, sFieldName, sTableName, sCaption, sTableCaption FROM DataDictionary_MST order by sTableCaption asc"
        Dim oDB As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable
        'dt = New DataTable
            oDB = New DataBaseLayer
            dt = oDB.GetDataTable_Query(strSelectQry)
        oDB.Dispose()
        oDB = Nothing

        If Not IsNothing(dt) Then
            GloUC_trvDataDictionary.ImageIndex = 2
            GloUC_trvDataDictionary.SelectedImageIndex = 2
            GloUC_trvDataDictionary.ParentImageIndex = 1
            GloUC_trvDataDictionary.SelectedParentImageIndex = 1
            GloUC_trvDataDictionary.DataSource = dt
            GloUC_trvDataDictionary.ParentMember = "sTableCaption"
            GloUC_trvDataDictionary.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvDataDictionary.CodeMember = Convert.ToString(dt.Columns("sCaption").ColumnName)
            GloUC_trvDataDictionary.ValueMember = Convert.ToString(dt.Columns("nDictionaryID").ColumnName)
            GloUC_trvDataDictionary.DescriptionMember = Convert.ToString(dt.Columns("sFieldName").ColumnName)
            GloUC_trvDataDictionary.Name = Convert.ToString(dt.Columns("sTableName").ColumnName)
            GloUC_trvDataDictionary.FillTreeView()
            GloUC_trvDataDictionary.CollapseAll()
        End If


    End Sub
    ''Sandip Darade 20090601
    ''Add the selected data fields the exam
    Private Sub AddDataFields_new()
        Dim i As Integer = 0
        'ArrlistDataDictionary is move to private declaration of form.
        'Dim ArrlistDataDictionary As New ArrayList


        If GloUC_trvDataDictionary.SelectedNodes.Count = 0 Then
            MessageBox.Show("Select Data Dictionary fields to add in the document.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        For Each n As gloUserControlLibrary.myTreeNode In GloUC_trvDataDictionary.SelectedNodes
            Dim listData As New myList
            ''ID  
            listData.ID = n.ID
            ''Field Name
            listData.Code = n.Description
            ''Table Name
            listData.ParameterName = n.Parent.Text
            ''Caption
            listData.Description = n.Code
            ''Table Caption
            listData.Reaction = n.Parent.Text

            ArrlistDataDictionary.Add(listData)
            listData = Nothing
        Next
        frmPatientExam.Arrlist = ArrlistDataDictionary
        'code commented by dipak 20091001 as ArrlistDataDictionary used on other form after dialog closed and use of frmPatientExam.blnChangesMade is replaced with indows.Forms.DialogResult.Yes
        ' No need to use frmPatientExam.blnIncludeCaption  as IncludeCaption property procedure is used
        'If chckCaption.Checked Then
        '    frmPatientExam.blnIncludeCaption = True
        'Else
        '    frmPatientExam.blnIncludeCaption = False
        'End If
        'blnChangesMade=true is commented because DialogResult is used for track changes made or not
        'frmPatientExam.blnChangesMade = True
        'line is commented because  ArrlistDataDictionary required on other form
        'ArrlistDataDictionary = Nothing
        'end comment by dipak 20091001
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    '' SUDHIR 20090630 '' 
    Private Sub GloUC_trvDataDictionary_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvDataDictionary.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Try

                    Dim CmpControls() As System.Windows.Forms.ContextMenu = {GloUC_trvDataDictionary.ContextMenu}

                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                        End If
                    End If

                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.DisposeContextMenu(CmpControls)
                        End If
                    End If

                    Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {GloUC_trvDataDictionary.ContextMenuStrip}

                    If (IsNothing(CmppControls) = False) Then
                        If CmppControls.Length > 0 Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                        End If
                    End If

                    If (IsNothing(CmppControls) = False) Then
                        If CmppControls.Length > 0 Then
                            gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                        End If
                    End If

                Catch ex As Exception

                End Try
                GloUC_trvDataDictionary.ContextMenu = Nothing
                Dim oContext As New ContextMenu
                Dim oContextItem As New MenuItem
                oContextItem.Text = "Add Table Template"
                oContext.MenuItems.Add(oContextItem)
                GloUC_trvDataDictionary.ContextMenu = oContext
                AddHandler oContextItem.Click, AddressOf OnTableTemplate_Click
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnTableTemplate_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ofrm As New frmMSTTableTemplate
            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
            Fill_DataDictionary()
            ofrm.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '' END SUDHIR '' 
    ''' <summary>
    ''' Property procedure to get IncludeCaption check box is checked or not
    ''' </summary>
    ''' <value></value>
    ''' <returns>true-Include Caption and false-Do not Include Caption</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IncludeCaption()
        Get
            If chckCaption.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    ''' <summary>
    ''' Returns array list of liquid link fields for add in document
    ''' </summary>
    ''' <value></value>
    ''' <returns>ArrlistDataDictionary of type Array List</returns>
    ''' <remarks>Added by dipak 20091001</remarks>
    Public ReadOnly Property GetArrlistDataDictionary()
        Get
            Return ArrlistDataDictionary
        End Get
    End Property
End Class



