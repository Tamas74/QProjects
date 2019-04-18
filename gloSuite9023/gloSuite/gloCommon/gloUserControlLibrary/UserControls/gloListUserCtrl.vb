Imports gloEMRGeneralLibrary

Public Class gloListUsrCtrl

    Public Property ListText() As String
        Get
            If Not IsNothing(trvList.SelectedNode) Then
                Return trvList.SelectedNode.Text
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            If Not IsNothing(trvList.SelectedNode) Then
                trvList.SelectedNode.Text = value
            End If
        End Set
    End Property

    Public Event trvAfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    Public Event trvDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event trvMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event txtchanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim ToolTip As ToolTip = Nothing
    Protected Sub InitialiseControls()
        Try
            'set the dispaly properties for TextBox
            With txtsearchDrug
                .Font = gloGlobal.clsgloFont.gFontVerdana_Regular 'New Font("Verdana", 9, FontStyle.Regular)
                .BorderStyle = Windows.Forms.BorderStyle.FixedSingle

            End With

            With trvList
                .Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New Font("Verdana", 9, FontStyle.Bold)
                .HideSelection = False
                .ShowLines = False
                .ShowPlusMinus = False
                .BackColor = System.Drawing.Color.FromArgb(170, 210, 230)
                .ForeColor = Color.Black

            End With
            ToolTip = New ToolTip()
            ToolTip.SetToolTip(Me.btnSearchClose, "Clear Search")

        Catch ex As Exception
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error Initializing Text box Control"
            Throw objex

        End Try

    End Sub



    Protected Sub FillTreeView()
        Try
            trvList.Nodes.Clear()
            Dim rootnode As myTreeNode

            With trvList


                rootnode = New myTreeNode("General", -1)
                rootnode.ImageIndex = 0
                rootnode.SelectedImageIndex = 0
                trvList.Nodes.Add(rootnode)

            End With

        Catch ex As Exception
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error Initializing the treeview control"
            Throw objex
        End Try

    End Sub

    Public Sub New()
        MyBase.New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitialiseControls()
        FillTreeView()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub trvList_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvList.AfterSelect
        Try
            RaiseEvent trvAfterSelect(sender, e)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub trvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvList.DoubleClick
        Try
            RemoveHandler trvList.DoubleClick, AddressOf trvList_DoubleClick
            RaiseEvent trvDoubleClick(sender, e)
            AddHandler trvList.DoubleClick, AddressOf trvList_DoubleClick

        Catch ex As Exception
            AddHandler trvList.DoubleClick, AddressOf trvList_DoubleClick
        End Try

    End Sub


    Private Sub trvList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvList.KeyPress
        Try
            Dim objsender As Object = Nothing
            Dim obje As System.EventArgs = Nothing
            Dim key As Integer = Asc(e.KeyChar)
            If key = 13 Then
                RemoveHandler trvList.KeyPress, AddressOf trvList_KeyPress
                trvList_DoubleClick(objsender, obje)
                AddHandler trvList.KeyPress, AddressOf trvList_KeyPress
            End If
            txtsearchDrug.Text = ""
            txtsearchDrug.Focus()
        Catch ex As Exception
            AddHandler trvList.KeyPress, AddressOf trvList_KeyPress
        End Try

    End Sub

    Protected Sub trvList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvList.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim trvNode As myTreeNode
                trvNode = CType(trvList.GetNodeAt(e.X, e.Y), myTreeNode)
                If IsNothing(trvNode) = False Then
                    trvList.SelectedNode = trvNode
                    'trvNode.BackColor = Color.CornflowerBlue
                    If Not IsNothing(trvList.SelectedNode) Then
                        If trvList.Nodes.Item(0) Is trvList.SelectedNode Then
                            'Try
                            '    If (IsNothing(trvList.ContextMenuStrip) = False) Then
                            '        trvList.ContextMenuStrip.Dispose()
                            '        trvList.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvList.ContextMenuStrip = Nothing
                        Else
                            'trvList.ContextMenu = cntDrugs
                            'Try
                            '    If (IsNothing(trvList.ContextMenuStrip) = False) Then
                            '        trvList.ContextMenuStrip.Dispose()
                            '        trvList.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvList.ContextMenuStrip = cntListmenuStrip
                            'treeindex = trDrugs.SelectedNode.Index
                        End If
                    End If
                End If
                'Try
                '    If (IsNothing(trvList.ContextMenuStrip) = False) Then
                '        trvList.ContextMenuStrip.Dispose()
                '        trvList.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trvList.ContextMenuStrip = cntListmenuStrip
            End If
            RaiseEvent trvMouseDown(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'If e.Button = Windows.Forms.MouseButtons.Right Then
        '    trvList.ContextMenuStrip = cntListmenuStrip
        'End If
        'RaiseEvent trvMouseDown(sender, e)
    End Sub

    Private Sub txtsearchDrug_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchDrug.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvList.Select()
        Else
            trvList.SelectedNode = trvList.Nodes.Item(0)
        End If

    End Sub



    Private Sub txtsearchDrug_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearchDrug.TextChanged
        Try
            RaiseEvent txtchanged(sender, e)
        Catch ex As Exception

        End Try

    End Sub
    'C
    'Added by Rahul Patel on 25-11-2010
    'For Adding Clear text button while Searching durg in drug list.Bug ID :5628

    Private Sub btnSearchClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchClose.Click
        txtsearchDrug.ResetText()
        txtsearchDrug.Focus()
        'Don't set here, instead moved to initializecontrols.
        'Dim ToolTip As New ToolTip()
        'ToolTip.SetToolTip(Me.btnSearchClose, "Clear Search")
    End Sub
End Class


