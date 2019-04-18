Imports gloEMRGeneralLibrary
Public Class gloUC_LabHistory

    Enum enmLabHistoryCriteria
        None = 0
        AddPendingResult = 1
        Current = 2
        Yesterday = 3
        LastWeek = 4
        LastMonth = 5
        Older = 6
    End Enum

    Private Const NodePendingResult As String = "Pending Result - Order"
    Private Const NodeCurrent As String = "Current"
    Private Const NodeYesterday As String = "Yesterday"
    Private Const NodeLastWeek As String = "Last Week"
    Private Const NodeLastMonth As String = "Last Month"
    Private Const NodeOlder As String = "Older"

    Private Const MenuModify As String = "Modify"
    Private Const MenuDelete As String = "Delete"
    Private Const MenuPrint As String = "Print"
    Private Const MenuFax As String = "Fax"
    Private Const MenuPrintPreview As String = "Print Preview"

    Public Event gUC_OpenLabForModify(ByVal OrderID As Int64)
    Public Event gUC_DeleteOrder(ByVal OrderID As Int64)
    Public Event gUC_PrintOrder(ByVal OrderID As Int64)
    Public Event gUC_FaxOrder(ByVal OrderID As Int64)
    Public Event gUC_PreviewOrder(ByVal OrderID As Int64)
    Public Event gUC_FillOrder(ByVal CriteriaNumber As Int16)



    Protected Sub FillTrvHistory(ByVal AddPendingResult As Boolean)
        Dim oNode As TreeNode
        Try
            trvHistory.Nodes.Clear()

            'Pending
            If AddPendingResult = True Then
                oNode = New TreeNode
                With oNode
                    .Text = NodePendingResult
                    .Tag = "R" & enmLabHistoryCriteria.AddPendingResult
                    .SelectedImageIndex = 0
                    .ImageIndex = 0
                    .ForeColor = Color.Maroon
                End With

                Dim oChildNode As New TreeNode
                oChildNode.Text = "Testing Order"
                oChildNode.Tag = "39562148962"
                oNode.Nodes.Add(oChildNode)
                oChildNode = Nothing

                trvHistory.Nodes.Add(oNode)
                oNode = Nothing
            End If

            'Current
            oNode = New TreeNode
            With oNode
                .Text = NodeCurrent
                .Tag = "R" & enmLabHistoryCriteria.Current
                .SelectedImageIndex = 1
                .ImageIndex = 1
                .ForeColor = Color.Blue
            End With
            trvHistory.Nodes.Add(oNode)
            oNode = Nothing

            'Yeasterday
            oNode = New TreeNode
            With oNode
                .Text = NodeYesterday
                .Tag = "R" & enmLabHistoryCriteria.Yesterday
                .SelectedImageIndex = 2
                .ImageIndex = 2
                .ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
            End With
            trvHistory.Nodes.Add(oNode)
            oNode = Nothing


            'Last Week
            oNode = New TreeNode
            With oNode
                .Text = NodeLastWeek
                .Tag = "R" & enmLabHistoryCriteria.LastWeek
                .SelectedImageIndex = 3
                .ImageIndex = 3
                .ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
            End With
            trvHistory.Nodes.Add(oNode)
            oNode = Nothing


            'Last Month
            oNode = New TreeNode
            With oNode
                .Text = NodeLastMonth
                .Tag = "R" & enmLabHistoryCriteria.LastMonth
                .SelectedImageIndex = 4
                .ImageIndex = 4
                .ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
            End With
            trvHistory.Nodes.Add(oNode)
            oNode = Nothing

            'Older
            oNode = New TreeNode
            With oNode
                .Text = NodeOlder
                .Tag = "R" & enmLabHistoryCriteria.Older
                .SelectedImageIndex = 5
                .ImageIndex = 5
                .ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
            End With
            trvHistory.Nodes.Add(oNode)
            oNode = Nothing
            ''Sandip Darade 20090904
            ''expand the first node having  labs
            Show_LabsHistory()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#Region "Menu Click Events"
    Private Sub MenuEvent_Modify(ByVal sender As Object, ByVal e As EventArgs)
        If Not trvHistory.SelectedNode Is Nothing Then
            RaiseEvent gUC_OpenLabForModify(Convert.ToInt64(trvHistory.SelectedNode.Tag))
        End If
    End Sub

    Private Sub MenuEvent_Delete(ByVal sender As Object, ByVal e As EventArgs)
        If Not trvHistory.SelectedNode Is Nothing Then
            RaiseEvent gUC_DeleteOrder(Convert.ToInt64(trvHistory.SelectedNode.Tag))
            If IsNothing(trvHistory.SelectedNode.Parent) = False Then
                trvHistory.SelectedNode = trvHistory.SelectedNode.Parent
            End If
        End If
    End Sub

    Private Sub MenuEvent_Print(ByVal sender As Object, ByVal e As EventArgs)
        If Not trvHistory.SelectedNode Is Nothing Then
            RaiseEvent gUC_PrintOrder(Convert.ToInt64(trvHistory.SelectedNode.Tag))
        End If
    End Sub

    Private Sub MenuEvent_Fax(ByVal sender As Object, ByVal e As EventArgs)
        If Not trvHistory.SelectedNode Is Nothing Then
            RaiseEvent gUC_FaxOrder(Convert.ToInt64(trvHistory.SelectedNode.Tag))
        End If
    End Sub

    Private Sub MenuEvent_PrintPreview(ByVal sender As Object, ByVal e As EventArgs)
        If Not trvHistory.SelectedNode Is Nothing Then
            RaiseEvent gUC_PreviewOrder(Convert.ToInt64(trvHistory.SelectedNode.Tag))
        End If
    End Sub

#End Region

    Private Sub gloUC_LabHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillTrvHistory(False)
    End Sub

    Private Sub trvHistory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvHistory.AfterSelect
        If Not trvHistory.SelectedNode Is Nothing Then
            If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
            End If
        End If
    End Sub

    Public Function FillOrder(ByVal nCriteria As Int16, ByVal oList As gloEMRActors.LabActor.LabOrders) As Boolean
        If trvHistory.GetNodeCount(False) > 0 Then
            Dim oNode As TreeNode = Nothing
            Dim oOrderNode As TreeNode = Nothing

            For i As Int16 = 0 To trvHistory.GetNodeCount(False) - 1
                If Val(Mid(trvHistory.Nodes(i).Tag, 2)) = nCriteria Then
                    oNode = trvHistory.Nodes(i)
                End If
            Next


            If Not oNode Is Nothing Then
                oNode.Nodes.Clear()
                If Not oList Is Nothing Then
                    For i As Int16 = 0 To oList.Count - 1
                        oOrderNode = New TreeNode
                        With oOrderNode
                            .Text = oList.Item(i).OrderNoPrefix & "-" & oList.Item(i).OrderNoID & "-" & oList.Item(i).TransactionDate
                            .Tag = oList.Item(i).OrderID
                            .ImageIndex = 6
                            .SelectedImageIndex = 6
                        End With
                        oNode.Nodes.Add(oOrderNode)
                        oOrderNode = Nothing
                    Next
                End If

                oNode.ExpandAll()
            End If

        End If
        Return Nothing
    End Function

    Private Sub trvHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvHistory.DoubleClick
        If Not trvHistory.SelectedNode Is Nothing Then
            If Mid(trvHistory.SelectedNode.Tag, 1, 1) <> "R" Then
                RaiseEvent gUC_OpenLabForModify(Convert.ToInt64(trvHistory.SelectedNode.Tag))
            End If
        End If
    End Sub

    Private Sub trvHistory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvHistory.MouseDown

        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Try
                    If (IsNothing(trvHistory.ContextMenuStrip) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(trvHistory.ContextMenuStrip)
                        If (IsNothing(trvHistory.ContextMenuStrip.Items) = False) Then
                            trvHistory.ContextMenuStrip.Items.Clear()
                        End If
                        trvHistory.ContextMenuStrip.Dispose()
                        trvHistory.ContextMenuStrip = Nothing
                    End If
                Catch

                End Try
               
               
                Dim trvnode As TreeNode
                trvnode = trvHistory.GetNodeAt(e.X, e.Y)
                If IsNothing(trvnode) = False Then
                    trvHistory.SelectedNode = trvnode
                    trvnode = trvHistory.SelectedNode
                    If Not IsNothing(trvnode) Then
                        If Mid(trvnode.Tag, 1, 1) = "R" AndAlso Val(Mid(trvnode.Tag, 2)) <= 6 Then ' Root Node
                            'Do Nothing
                        Else
                            If Val(trvnode.Tag) > 0 Then
                                Dim oContextMenu As New ContextMenuStrip
                                oContextMenu.Items.Add(MenuModify, ImgModify.Image, AddressOf MenuEvent_Modify)
                                oContextMenu.Items.Add(MenuDelete, ImgDelete.Image, AddressOf MenuEvent_Delete)
                                oContextMenu.Items.Add("-")
                                oContextMenu.Items.Add(MenuPrint, ImgPrint.Image, AddressOf MenuEvent_Print)
                                oContextMenu.Items.Add(MenuFax, ImgFax.Image, AddressOf MenuEvent_Fax)
                                oContextMenu.Items.Add("-")
                                oContextMenu.Items.Add(MenuPrintPreview, ImgPreview.Image, AddressOf MenuEvent_PrintPreview)
                                Try
                                    If (IsNothing(trvHistory.ContextMenuStrip) = False) Then
                                        gloGlobal.cEventHelper.RemoveAllEventHandlers(trvHistory.ContextMenuStrip)
                                        If (IsNothing(trvHistory.ContextMenuStrip.Items) = False) Then
                                            trvHistory.ContextMenuStrip.Items.Clear()
                                        End If
                                        trvHistory.ContextMenuStrip.Dispose()
                                        trvHistory.ContextMenuStrip = Nothing
                                    End If
                                Catch

                                End Try

                                trvHistory.ContextMenuStrip = oContextMenu
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' oContextMenu = Nothing
        End Try
    End Sub

    'sarika 28th sept 07
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        Try
            If Trim(txtSearch.Text) <> "" Then
                For i As Integer = 0 To trvHistory.Nodes.Count - 1
                    If trvHistory.Nodes.Item(i).GetNodeCount(False) > 0 Then
                        Dim mychildnode As TreeNode
                        'child node collection
                        For Each mychildnode In trvHistory.Nodes.Item(i).Nodes
                            'search against Description

                            If UCase(Mid(mychildnode.Text, 1, Len(Trim(txtSearch.Text)))) = UCase(Trim(txtSearch.Text)) Then
                                'select matching node
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvHistory.SelectedNode = trvHistory.SelectedNode.LastNode
                                '*************
                                trvHistory.SelectedNode = mychildnode
                                trvHistory.Select()
                                'txtSearch.Focus()
                                Exit Sub
                            End If

                        Next
                    End If
                Next
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Lab orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    '-----------------------------------------------------

    ''Sandip Darade 20090902
    ''expand the first node having  labs
    Private Sub Show_LabsHistory()

        For Each n As TreeNode In trvHistory.Nodes
            Select Case n.Tag

                Case "R" & enmLabHistoryCriteria.AddPendingResult
                    trvHistory.SelectedNode = n
                    If Not trvHistory.SelectedNode Is Nothing Then
                        If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                            RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
                        End If
                    End If
                   
                Case "R" & enmLabHistoryCriteria.Current
                    trvHistory.SelectedNode = n
                    If Not trvHistory.SelectedNode Is Nothing Then
                        If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                            RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
                        End If
                    End If
                    
                Case "R" & enmLabHistoryCriteria.Yesterday
                    trvHistory.SelectedNode = n
                    If Not trvHistory.SelectedNode Is Nothing Then
                        If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                            RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
                        End If
                    End If
                  
                Case "R" & enmLabHistoryCriteria.LastWeek
                    trvHistory.SelectedNode = n
                    If Not trvHistory.SelectedNode Is Nothing Then
                        If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                            RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
                        End If
                    End If
                   
                Case "R" & enmLabHistoryCriteria.LastMonth
                    trvHistory.SelectedNode = n
                    If Not trvHistory.SelectedNode Is Nothing Then
                        If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                            RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
                        End If
                    End If
                   
                Case "R" & enmLabHistoryCriteria.LastMonth
                    trvHistory.SelectedNode = n
                    If Not trvHistory.SelectedNode Is Nothing Then
                        If Mid(trvHistory.SelectedNode.Tag, 1, 1) = "R" Then
                            RaiseEvent gUC_FillOrder(Val(Mid(trvHistory.SelectedNode.Tag, 2)))
                        End If
                    End If


            End Select

        Next


    End Sub
End Class
