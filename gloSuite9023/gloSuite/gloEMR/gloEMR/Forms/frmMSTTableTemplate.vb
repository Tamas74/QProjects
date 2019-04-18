Public Class frmMSTTableTemplate

#Region " Private Variables "
    Private oDataDictionary As New clsDataDictionary
    Private _DictionaryID As Int64 = 0
#End Region

    Private Sub frmMSTTableTemplate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FillTableTemplates()
        FillDataDictionaryItems()

        pnlTableEditor.Visible = False

    End Sub

#Region " Private Methods "
    Private Sub FillTableTemplates()
        Dim dtTables As DataTable
        dtTables = oDataDictionary.GetAllTableTemplates
        If dtTables IsNot Nothing Then
            gloTree_AvailableTables.Clear()
            If dtTables.Rows.Count > 0 Then
                gloTree_AvailableTables.DataSource = dtTables
                gloTree_AvailableTables.ValueMember = "nDictionaryID"
                gloTree_AvailableTables.ParentMember = "sTableCaption"
                gloTree_AvailableTables.DescriptionMember = "sCaption"
                gloTree_AvailableTables.CodeMember = "sFieldName"
                gloTree_AvailableTables.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                gloTree_AvailableTables.FillTreeView()
            End If
        End If
        _DictionaryID = 0
    End Sub

    Private Sub FillDataDictionaryItems()
        Dim dtDictionary As DataTable
        dtDictionary = oDataDictionary.GetDataDictionary(clsDataDictionary.enumDictionaryType.Vitals)
        If dtDictionary IsNot Nothing Then
            If dtDictionary.Rows.Count > 0 Then
                gloTree_DataDictionary.DataSource = dtDictionary
                gloTree_DataDictionary.ValueMember = "nDictionaryID"
                gloTree_DataDictionary.DescriptionMember = "sCaption"
                gloTree_DataDictionary.CodeMember = "sFieldName"
                gloTree_DataDictionary.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                gloTree_DataDictionary.FillTreeView()
            End If
        End If
    End Sub

    Private Sub SaveTableTemplate()
        Try
            Dim sFieldName As String = ""
            Dim sSplit() As String
            'Added by Mayuri:20091027
            'To fix issue:#4528
            If Trim(txtTemplateName.Text) = "" Then
                MessageBox.Show("Enter Table Template name.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            'End code Added by Mayuri:20091027
            '' SELECTED NODE VALIDATION ''
            If trvTable.Nodes.Count = 0 Then
                MessageBox.Show("No Data Dictionary items selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '' READ SELECTED DICTIONARY ITEMS FROM TREE ''
            For iNode As Integer = 0 To trvTable.Nodes.Count - 1
                sSplit = Split(trvTable.Nodes(iNode).Tag, ".")

                If iNode = trvTable.Nodes.Count - 1 Then
                    sFieldName = "Vitals." & sFieldName & sSplit(1)
                    'If rbDay.Checked Then
                    '    sFieldName = sFieldName & "|Today"
                    'Else
                    '    sFieldName = sFieldName & "|Hx"
                    'End If
                    Select Case cmbTemplateCategory.Text
                        Case "Taken today"
                            sFieldName = sFieldName & "|Today"
                            Exit Select
                        Case "All Vitals"
                            sFieldName = sFieldName & "|Hx"
                            Exit Select
                        Case "Latest Vital"
                            sFieldName = sFieldName & "|01"
                        Case "Last 2 Vitals"
                            sFieldName = sFieldName & "|02"
                        Case "Last 3 Vitals"
                            sFieldName = sFieldName & "|03"
                        Case "Last 4 Vitals"
                            sFieldName = sFieldName & "|04"
                        Case "Last 5 Vitals"
                            sFieldName = sFieldName & "|05"
                        Case "Last 10 Vitals"
                            sFieldName = sFieldName & "|10"
                        Case "Taken in last 3 days"
                            sFieldName = sFieldName & "|LastThreeDays"
                        Case "Taken in last week"
                            sFieldName = sFieldName & "|LastWeek"
                        Case "Taken in last month"
                            sFieldName = sFieldName & "|LastMonth"
                        Case "Taken in last 3 months"
                            sFieldName = sFieldName & "|LastThreeMonths"
                        Case "Taken in last 6 months"
                            sFieldName = sFieldName & "|LastSixMonths"
                        Case "Taken in last year"
                            sFieldName = sFieldName & "|LastYear"
                        Case Else
                            sFieldName = sFieldName & "|Today"
                    End Select
                    'If cmbTemplateCategory.SelectedText = "Taken today" Then
                    '    sFieldName = sFieldName & "|Today"
                    'ElseIf cmbTemplateCategory.SelectedText = "" Then
                    '    sFieldName = sFieldName & "|Hx"
                    'End If
                Else
                    sFieldName = sFieldName & sSplit(1) & ","
                End If
            Next

            If _DictionaryID > 0 Then
                '' MODIFY EXISTING ITEM TO DATABASE ''
                If oDataDictionary.IsFieldCaptionPresent(txtTemplateName.Text.Trim, "Vitals", _DictionaryID) = False Then
                    oDataDictionary.UpdateDataDictionary(_DictionaryID, sFieldName, "TableTemplate", txtTemplateName.Text.Trim, "Vitals")
                Else
                    MessageBox.Show("Table name already present.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtTemplateName.SelectAll()
                    Exit Sub
                End If
            Else
                '' ADD DATADICTIONARY ITEM TO DATABASE ''
                If oDataDictionary.IsFieldCaptionPresent(txtTemplateName.Text.Trim, "Vitals") = False Then
                    oDataDictionary.AddDataDictionary(sFieldName, "TableTemplate", txtTemplateName.Text.Trim, "Vitals")
                Else
                    MessageBox.Show("Table name already present.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtTemplateName.SelectAll()
                    Exit Sub
                End If
            End If

            '' REFRESH TABLE TEMPLATE LIST ''
            FillTableTemplates()
            pnlTableEditor.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteTableTemplate()
        Try
            If gloTree_AvailableTables.SelectedNode IsNot Nothing Then
                '' IF PARENT NODE SELECTED THEN EXIT ''
                If gloTree_AvailableTables.SelectedNode.Level = 0 Then
                    Exit Sub
                End If

                '' DELETE SELECTED TABLE TEMPLATE ''
                If MessageBox.Show("Are you sure you want to Delete selected Table Template?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sField As String
                    Dim sCaption As String

                    sField = CType(gloTree_AvailableTables.SelectedNode, gloUserControlLibrary.myTreeNode).Code
                    sCaption = CType(gloTree_AvailableTables.SelectedNode, gloUserControlLibrary.myTreeNode).Description

                    oDataDictionary.DeleteDataDictionary(sField, sCaption)

                    '' REFRESH TABLE TEMPLATE LIST AFTER DELETING ''
                    FillTableTemplates()

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewTableTemplate()
        _DictionaryID = 0
        txtTemplateName.Focus()
        txtTemplateName.Text = ""
        trvTable.Nodes.Clear()
        'rbDay.Checked = True
        cmbTemplateCategory.Text = "Taken today"
        pnlTableEditor.Visible = True
    End Sub

    Private Sub ModifyTableTemplate()
        Try
            If gloTree_AvailableTables.SelectedNode IsNot Nothing Then
                '' IF PARENT NODE SELECTED THEN EXIT ''
                If gloTree_AvailableTables.SelectedNode.Level = 0 Then
                    Exit Sub
                End If

                '' REMOVE UNWANTED STRINGS FROM FIELD ''
                _DictionaryID = CType(gloTree_AvailableTables.SelectedNode, gloUserControlLibrary.myTreeNode).ID

                Dim sFieldName As String = CType(gloTree_AvailableTables.SelectedNode, gloUserControlLibrary.myTreeNode).Code
                sFieldName = sFieldName.Substring(sFieldName.IndexOf(".") + 1, sFieldName.Length - sFieldName.IndexOf(".") - 1)
                sFieldName = sFieldName.Substring(0, sFieldName.IndexOf("|"))

                '' NOW WE GOT ONLY COLUMN NAMES OF TABLE '' LETS SPLIT IT ''
                Dim sColumns() As String = sFieldName.Split(",")

                trvTable.Nodes.Clear()

                '' FIND EACH ITEM IN DATADICTIONARY TREE AND PUSH IT IN SELECTED COLUMNS ''
                For iItem As Integer = 0 To sColumns.Length - 1
                    For Each iNode As gloUserControlLibrary.myTreeNode In gloTree_DataDictionary.Nodes
                        If "Vitals." & sColumns(iItem) = iNode.Code Then
                            gloTree_DataDictionary.SelectedNode = iNode
                            btnAdd_Click(Nothing, Nothing)
                            Exit For
                        End If
                    Next
                Next

                txtTemplateName.Text = gloTree_AvailableTables.SelectedNode.Text

                Dim sSplit() As String = Split(CType(gloTree_AvailableTables.SelectedNode, gloUserControlLibrary.myTreeNode).Code, "|")
                'If sSplit(1) = "Today" Then
                'rbDay.Checked = True
                Select Case sSplit(1).ToString()
                    Case "Today"
                        cmbTemplateCategory.Text = "Taken today"
                        Exit Select
                    Case "Hx"
                        cmbTemplateCategory.Text = "All Vitals"
                        Exit Select
                    Case "01"
                        cmbTemplateCategory.Text = "Latest Vital"
                        Exit Select
                    Case "02"
                        cmbTemplateCategory.Text = "Last 2 Vitals"
                        Exit Select
                    Case "03"
                        cmbTemplateCategory.Text = "Last 3 Vitals"
                        Exit Select
                    Case "04"
                        cmbTemplateCategory.Text = "Last 4 Vitals"
                        Exit Select
                    Case "05"
                        cmbTemplateCategory.Text = "Last 5 Vitals"
                        Exit Select
                    Case "10"
                        cmbTemplateCategory.Text = "Last 10 Vitals"
                        Exit Select
                    Case "LastThreeDays"
                        cmbTemplateCategory.Text = "Taken in last 3 days"
                        Exit Select
                    Case "LastWeek"
                        cmbTemplateCategory.Text = "Taken in last week"
                        Exit Select
                    Case "LastMonth"
                        cmbTemplateCategory.Text = "Taken in last month"
                        Exit Select
                    Case "LastThreeMonths"
                        cmbTemplateCategory.Text = "Taken in last 3 months"
                        Exit Select
                    Case "LastSixMonths"
                        cmbTemplateCategory.Text = "Taken in last 6 months"
                        Exit Select
                    Case "LastYear"
                        cmbTemplateCategory.Text = "Taken in last year"
                        Exit Select
                    Case Else
                        cmbTemplateCategory.Text = "Taken today"
                End Select
                'Else
                'rbHx.Checked = True
                'End If

                pnlTableEditor.Visible = True
                gloTree_DataDictionary.SelectedNode = Nothing
            End If
            txtTemplateName.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " ToolStrip Button Events "
    Private Sub tsb_TableTemplate_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsb_TableTemplate.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "New"
                NewTableTemplate()
                txtTemplateName.Focus()
            Case "Modify"
                ModifyTableTemplate()
            Case "Delete"
                DeleteTableTemplate()
            Case "Refresh"
                FillTableTemplates()
        End Select
    End Sub

    Private Sub tsb_AddTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_AddTable.Click
        SaveTableTemplate()
    End Sub

    Private Sub tsb_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Close.Click
        pnlTableEditor.Visible = False
    End Sub
#End Region

    Private Sub gloTree_DataDictionary_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles gloTree_DataDictionary.NodeMouseDoubleClick
        btnAdd_Click(Nothing, Nothing)
    End Sub

    Private Sub gloTree_AvailableTables_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles gloTree_AvailableTables.NodeMouseDoubleClick
        ModifyTableTemplate()
        txtTemplateName.Focus()
    End Sub

    Private Sub trvTable_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvTable.NodeMouseDoubleClick
        btnRemove_Click(Nothing, Nothing)
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer
            If IsNothing(trvTable.SelectedNode) = False Then
                oNode = trvTable.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    If trvTable.SelectedNode.Index <> 0 Then
                        prevIndex = trvTable.SelectedNode.PrevNode.Index
                        trvTable.Nodes.Remove(trvTable.SelectedNode)
                        trvTable.Nodes.Insert(prevIndex, oNode)
                        trvTable.SelectedNode = oNode
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer
            If IsNothing(trvTable.SelectedNode) = False Then
                oNode = trvTable.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    If trvTable.SelectedNode.Index <> trvTable.Nodes.Count - 1 Then
                        nextIndex = trvTable.SelectedNode.NextNode.Index
                        trvTable.Nodes.Remove(trvTable.SelectedNode)
                        trvTable.Nodes.Insert(nextIndex, oNode)
                        trvTable.SelectedNode = oNode
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If gloTree_DataDictionary.SelectedNode Is Nothing Then
            Exit Sub
        End If

        Try
            Dim oSelectedNode As gloUserControlLibrary.myTreeNode = gloTree_DataDictionary.SelectedNode

            '' CHECK FOR COLUMN LIMITS ''
            If trvTable.Nodes.Count >= 10 Then
                MessageBox.Show("Table Template limit is upto 10 items.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '' CHECK IF SELECTED NODE IS PRESENT OR NOT ''
            For Each iNode As TreeNode In trvTable.Nodes
                If iNode.Text = oSelectedNode.Text Then
                    Exit Sub
                End If
            Next

            '' INSERT SELECTED NODE IN TABLE TREE ''
            If oSelectedNode IsNot Nothing Then
                Dim oNode As New TreeNode
                oNode.Text = oSelectedNode.Text
                oNode.Tag = oSelectedNode.Code

                If trvTable.SelectedNode Is Nothing Then
                    trvTable.Nodes.Add(oNode) '' INSERT AT END ''
                Else
                    trvTable.Nodes.Insert(trvTable.SelectedNode.Index, oNode) '' INSERT AT SELECTION ''
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If trvTable.SelectedNode IsNot Nothing Then
            trvTable.Nodes.Remove(trvTable.SelectedNode)
        End If
    End Sub

#Region " Mouse Hoover & Leave Events "
    ''<<<<<<<<<<Ojeswini>>>>>>>For Give Mouse Hover and Leave image.

    Private Sub btnAdd_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseHover
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.YellowRight24
    End Sub

    Private Sub btnAdd_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseLeave
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.Right24
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.YellowDown24
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Down24
    End Sub


    Private Sub btnRemove_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseHover
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.YellowLeft24
    End Sub

    Private Sub btnRemove_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseLeave
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.Left24
    End Sub

    Private Sub btnUp_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.YellowUp24
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Up24
    End Sub
#End Region

    Private Sub rbDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDay.CheckedChanged
        If rbDay.Checked Then
            rbDay.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbDay.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbHx_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbHx.CheckedChanged
        If rbHx.Checked Then
            rbHx.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbHx.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub cmbTemplateCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTemplateCategory.SelectedIndexChanged
        ''cmbTemplateCategory.Text
    End Sub
End Class