Public Class gloHistoryUserCtrl

    'variables
    Enum enmPreviousPrescriptions
        Current
        Yesterday
        LastWeek
        LastMonth
        Older
    End Enum

    Public Event trvAfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    Public Event trvDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event trvMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    Public Event _TextBoxSearchTextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event _CmboPrescSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    'Property variables
    Public Property History() As String
        Get
            If Not IsNothing(trvHistory.SelectedNode) Then
                Return trvHistory.SelectedNode.Text
            Else
                Return ""
            End If

        End Get
        Set(ByVal value As String)
            If Not IsNothing(trvHistory.SelectedNode) Then
                trvHistory.SelectedNode.Text = value
            End If

        End Set
    End Property


    Private Sub gloHistoryUserCtrl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'set the display properties of the user control

    End Sub

    Protected Sub InitializeUserCtrl()
        Try
            'set the dispaly properties for treeview 

            trvHistory.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
            trvHistory.BorderStyle = Windows.Forms.BorderStyle.None
            trvHistory.HideSelection = False
            trvHistory.ShowPlusMinus = False
            trvHistory.ShowLines = False
            trvHistory.ShowRootLines = True
            trvHistory.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
        Catch ex As Exception

            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error Initializing Control"
            Throw objex
        End Try
    End Sub

    Protected Sub FillTrvHistory()
        Try
            'fill the treeview
            trvHistory.Nodes.Clear()

            'With trvHistory
            '    .Nodes.Add("root", "General")
            '    .Nodes(0).Nodes.Add(enmPreviousPrescriptions.Current.ToString)
            '    .Nodes(0).Nodes.Add(enmPreviousPrescriptions.Yesterday.ToString)
            '    .Nodes(0).Nodes.Add(enmPreviousPrescriptions.LastWeek.ToString)
            '    .Nodes(0).Nodes.Add(enmPreviousPrescriptions.LastMonth.ToString)
            '    .Nodes(0).Nodes.Add(enmPreviousPrescriptions.Older.ToString)
            'End With

            '----------------------------------------------------------------
            Dim rootnode As myTreeNode

            rootnode = New myTreeNode("General", -1)

            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            'rootnode.ForeColor = Color.Blue

            trvHistory.Nodes.Add(rootnode)
            trvHistory.ExpandAll()

            Dim mychild As myTreeNode

            mychild = New myTreeNode("Current", enmPreviousPrescriptions.Current, enmPreviousPrescriptions.Current.ToString)
            mychild.ImageIndex = 2
            mychild.SelectedImageIndex = 2
            mychild.ForeColor = Color.Blue

            rootnode.Nodes.Add(mychild)
            'trvHistory.Nodes.Add(mychild)

            mychild = New myTreeNode("Yesterday", enmPreviousPrescriptions.Yesterday, enmPreviousPrescriptions.Yesterday.ToString)
            mychild.ImageIndex = 4
            mychild.SelectedImageIndex = 4
            mychild.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)

            rootnode.Nodes.Add(mychild)
            'trvHistory.Nodes.Add(mychild)

            mychild = New myTreeNode("Last Week", enmPreviousPrescriptions.LastWeek, enmPreviousPrescriptions.LastWeek.ToString)
            mychild.ImageIndex = 6
            mychild.SelectedImageIndex = 6
            mychild.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)

            rootnode.Nodes.Add(mychild)
            'trvHistory.Nodes.Add(mychild)
            '
            mychild = New myTreeNode("Last Month", enmPreviousPrescriptions.LastMonth, enmPreviousPrescriptions.LastMonth.ToString)
            mychild.ImageIndex = 8
            mychild.SelectedImageIndex = 8
            mychild.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)

            rootnode.Nodes.Add(mychild)
            'trvHistory.Nodes.Add(mychild)

            mychild = New myTreeNode("Older", enmPreviousPrescriptions.Older, enmPreviousPrescriptions.Older.ToString)
            mychild.ImageIndex = 10
            mychild.SelectedImageIndex = 10
            mychild.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)

            rootnode.Nodes.Add(mychild)
            'trvHistory.Nodes.Add(mychild)
        Catch ex As Exception

            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error while filling the History treeview."
            Throw objex
        End Try
    End Sub

    Protected Sub trvHistory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvHistory.AfterSelect
        Try
            RaiseEvent trvAfterSelect(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub trvHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvHistory.DoubleClick
        Try
            RaiseEvent trvDoubleClick(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub trvHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvHistory.KeyPress
        Try
            Dim objsender As Object = Nothing
            Dim obje As System.EventArgs = Nothing
            trvHistory_DoubleClick(objsender, obje)
        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub trvHistory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvHistory.MouseDown
        Try
            'If e.Button = Windows.Forms.MouseButtons.Right Then

            '    Dim trvnode As myTreeNode
            '    trvnode = CType(trvHistory.GetNodeAt(e.X, e.Y), myTreeNode)
            '    If IsNothing(trvnode) = False Then
            '        trvHistory.SelectedNode = trvnode
            '        Dim mynode As myTreeNode
            '        'mynode = CType(sender, TreeView).SelectedNode
            '        mynode = trvHistory.SelectedNode
            '        If Not IsNothing(mynode) Then

            '            'check if selected node is rootnode
            '            If trvHistory.Nodes.Item(0).Text = mynode.Text Then
            '                'intHistory = -1
            '                trvHistory.ContextMenuStrip = Nothing
            '            ElseIf mynode.Parent Is CType(trvHistory.Nodes.Item(0), myTreeNode) Then
            '                'If Not IsNothing(CType(trPrescriptionHistory.SelectedNode, myTreeNode).Tag) Then
            '                trvHistory.ContextMenuStrip = cntListmenuStrip
            '                cntListmenuStrip.Items(0).Visible = False 'this is for Edit Prescription menu
            '                cntListmenuStrip.Items(1).Visible = False 'this is for Delete Prescription menu
            '                cntListmenuStrip.Items(2).Visible = False 'this is for Print Prescription menu
            '                cntListmenuStrip.Items(3).Visible = False 'this is for Fax Prescription menu
            '                'intHistory = 2

            '            ElseIf Not IsNothing(mynode.Tag) Then
            '                Dim selnode As String
            '                selnode = mynode.Parent.Text

            '                trvHistory.ContextMenuStrip = cntListmenuStrip
            '                'mynode.Parent.Nodes.Clear()
            '                If selnode = "Current" Or selnode = "Yesterday" Then
            '                    If DateDiff(DateInterval.Hour, trvHistory.SelectedNode.Tag, Now) > 7 Then
            '                        'trPrescriptionHistory.SelectedNode = trPrescriptionHistory.Nodes.Item(0)
            '                        'intHistory = 3
            '                        cntListmenuStrip.Items(0).Visible = False 'this is for Edit Prescription menu
            '                        'mnuDeletePrescriptionHistory.Visible = True
            '                        cntListmenuStrip.Items(1).Visible = False 'this is for Delete Prescription History menu
            '                        cntListmenuStrip.Items(2).Visible = True 'this is for Print Prescription menu
            '                        cntListmenuStrip.Items(3).Visible = True 'this is for Fax Prescription menu
            '                    Else
            '                        'intHistory = 4
            '                        cntListmenuStrip.Items(0).Visible = True 'this is for Edit Prescription menu
            '                        cntListmenuStrip.Items(1).Visible = True 'this is for Delete Prescription History menu
            '                        If CType(trvHistory.SelectedNode.Tag, System.DateTime).Date <= Now.Date Then
            '                            'mnuRefill.Visible = False
            '                        Else
            '                            'mnuRefill.Visible = False
            '                        End If
            '                        cntListmenuStrip.Items(2).Visible = True 'this is for Print Prescription menu
            '                        cntListmenuStrip.Items(3).Visible = True 'this is for Fax Prescription menu
            '                    End If
            '                Else
            '                    'intHistory = 3
            '                    'trPrescriptionHistory.SelectedNode = trPrescriptionHistory.Nodes.Item(0)
            '                    cntListmenuStrip.Items(0).Visible = False 'this is for Edit Prescription menu
            '                    'mnuDeletePrescriptionHistory.Visible = True
            '                    cntListmenuStrip.Items(1).Visible = False 'this is for Delete Prescription History menu
            '                    'mnuRefill.Visible = False
            '                    cntListmenuStrip.Items(2).Visible = True 'this is for Print Prescription menu
            '                    cntListmenuStrip.Items(3).Visible = True 'this is for Fax Prescription menu
            '                End If


            '            Else
            '                trvHistory.ContextMenu = Nothing
            '                'mnuDeletePrescriptionHistory.Visible = False
            '                'mnuEditPrescription.Visible = False
            '                'mnuRefill.Visible = False
            '                'mnuPrint.Visible = False
            '                'mnuFax.Visible = False
            '            End If
            '        End If
            '    End If
            'End If
            RaiseEvent trvMouseDown(sender, e)
        Catch ex As Exception

            MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeUserCtrl()
        FillTrvHistory()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub _CmboPresc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent _CmboPrescSelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _TextBoxSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent _TextBoxSearchTextChanged(sender, e)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub RefreshHistoryData()
        Dim objnode As myTreeNode
        For i As Int16 = 0 To trvHistory.Nodes.Item(0).GetNodeCount(False) - 1
            objnode = trvHistory.Nodes.Item(0).Nodes.Item(i)
            'objnode.Collapse(False)''''commented this code, check bug 4341
            'Dim obje As New System.Windows.Forms.TreeViewEventArgs(objnode, TreeViewAction.ByMouse)
            'Call trvHistory_AfterSelect(objnode, obje)
            objnode = Nothing
        Next
    End Sub
    'C
End Class
