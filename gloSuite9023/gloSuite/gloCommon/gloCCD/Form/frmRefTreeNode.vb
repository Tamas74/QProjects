Imports gloCCDLibrary
Imports System.IO
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Xml

Public Class frmRefTreeNode
    Private mPatient As gloCCDLibrary.Patient

    Private _LoginID As Int64 = 0
    Private _ClinicID As Int64 = 1
    Private mEffectiveTime As String = ""
    Private sFileType As String = ""
    Private _TreeNode As MyTreeNode
    Public _TempTreeNode As MyTreeNode


    Public Sub New(ByVal _UserID As Int64, ByVal ClinicID As Int64, ByVal TreeNode As MyTreeNode)

        InitializeComponent()
        _LoginID = _UserID
        _ClinicID = ClinicID
        _TreeNode = TreeNode

    End Sub
    'Public Sub New(ByVal _UserID As Int64, ByVal ClinicID As Int64, ByVal TreeNode As MyTreeNode, ByVal NodeType As String)

    '    InitializeComponent()
    '    _LoginID = _UserID
    '    _ClinicID = ClinicID
    '    _TreeNode = TreeNode

    'End Sub


    Private Sub frmRefTreeNode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim node As TreeNode = CType(_TreeNode.Parent.Clone(), TreeNode)
        Try
            ' Dim temp As MyTreeNode = _TreeNode.Parent.Clone()

            If _TreeNode.TableName = "Attribute" Then
                For Each childnode As TreeNode In node.Nodes
                    If (_TreeNode.Text <> childnode.Text) Then
                        trReftree.Nodes.Add(childnode)
                    End If
                Next
            Else
                For Each childnode As TreeNode In node.Nodes
                    If (_TreeNode.Text <> childnode.Text) Then
                        trReftree.Nodes.Add(childnode)
                    End If
                Next

            End If




            'For x As Int32 = 0 To temp.Nodes.Count - 1
            '    trReftree.Nodes.Add(temp.Nodes(x))
            'Next


            'If Not IsNothing(trReftree) Then
            '    For i As Int32 = 0 To trReftree.Nodes.Count - 1
            '        If IsNothing(trReftree.Nodes(i).Parent) Then
            '            trReftree.Nodes(i).Checked = False
            '        End If
            '    Next
            'End If


        Catch ex As Exception
            'MessageBox.Show(ex.ToString,, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            node = Nothing
        End Try
    End Sub

    Private Sub UpdateCheckbox()



        'trReftree.BeginUpdate()
        'For Each node As TreeNode In trReftree.Nodes
        '    If HasCheckedChildNodes(node) Then
        '        If node.Checked = True Then
        '            For Each childNode As TreeNode In node.Nodes
        '                childNode.Checked = True
        '            Next
        '        End If
        '    End If
        'Next
        'trReftree.EndUpdate()












        'AddHandler trReftree.BeforeExpand, AddressOf CheckForCheckedChildren

        '' Expand all nodes of treeView1. Nodes without checked children are 
        '' prevented from expanding by the checkForCheckedChildren event handler.
        'trReftree.ExpandAll()

        '' Remove the checkForCheckedChildren event handler from the BeforeExpand 
        '' event so manual node expansion will work correctly.
        'RemoveHandler trReftree.BeforeExpand, AddressOf CheckForCheckedChildren

        '' Enable redrawing of treeView1.
        'trReftree.EndUpdate()
    End Sub

    Private Sub CheckForCheckedChildren(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs)
        If Not HasCheckedChildNodes(e.Node) Then
            e.Cancel = True
        End If
    End Sub 'CheckForCheckedChildren

    Private Function HasCheckedChildNodes(ByVal node As TreeNode) As Boolean
        If node.Nodes.Count = 0 Then
            Return False
        End If
        Dim childNode As TreeNode
        For Each childNode In node.Nodes
            If childNode.Checked Then
                Return True
            End If
            ' Recursively check the children of the current child node.
            If HasCheckedChildNodes(childNode) Then
                Return True
            End If
        Next childNode
        childNode = Nothing
        Return False
    End Function 'HasCheckedChildNodes

    'Private Sub populateTree(ByVal filename As [String])
    '    Dim fs As New FileStream(filename, FileMode.Open)

    '    Dim doc As New XmlDocument()
    '    doc.Load(fs)

    '    Dim firstNode As New TreeNode(filename)

    '    'call function below to add TreeNodes to 'firstNode' from XmlNodes in 'doc'
    '    doNodes(doc, firstNode.Nodes)

    '    'treeXML is the name of my TreeView
    '    treeXML.Nodes.Add(firstNode)

    '    'clean up
    '    doc = Nothing
    '    fs.Close()
    'End Sub

    'Private Sub doNodes(ByVal xn As XmlNode, ByVal tn As TreeNodeCollection)
    '    For Each child As XmlNode In xn.ChildNodes
    '        Dim newTN As TreeNode = Nothing

    '        'add a TreeNode to newTN, text depends on whether or not the current XmlNode has children
    '        If Not child.HasChildNodes AndAlso Not (child.Value Is Nothing) Then
    '            newTN = tn.Add(child.Value)
    '        Else
    '            newTN = tn.Add(child.Name)

    '            'call this function again to do the children of the current XmlNode
    '            doNodes(child, newTN.Nodes)
    '        End If
    '    Next
    'End Sub




    Private Sub trReftree_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trReftree.AfterCheck

        'For i As Int32 = 0 To trReftree.Nodes.Count - 1
        '    trReftree.Nodes(i).Checked = False

        'Next






        '   UpdateCheckbox()

    End Sub

    Private Sub trReftree_BeforeCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trReftree.BeforeCheck
        'For Each childnode As TreeNode In _TreeNode.Nodes
        '    If (_TreeNode.Text <> childnode.Text) Then

        '    Else
        '        childnode.Checked = False
        '    End If
        'Next
        ' For Each ChildNode As TreeNode In trReftree.Nodes

        'For i As Int32 = 0 To trReftree.Nodes.Count - 1
        '    trReftree.Nodes(i).Checked = False
        '    i = i + 1
        'Next
    End Sub

    Private Sub SaveToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton1.Click
        _TempTreeNode = Nothing
        Dim node As TreeNode = trReftree.SelectedNode

        '   If node.Checked = True Then
        _TempTreeNode = node
        ' End If

        If Not IsNothing(_TempTreeNode) Then
            'If _TempTreeNode.Nodes.Count > 0 Then
            '    Me.Close()
            'Else
            '    MessageBox.Show("Select Reference Node")
            'End If
            Me.Close()
        Else
            MessageBox.Show("Select Reference Node")
        End If
    End Sub

    Private Sub frmRefTreeNode_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub

    Private Sub frmRefTreeNode_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub trReftree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trReftree.AfterSelect
        If trReftree.SelectedNode.Nodes.Count > 0 Then
            Dim childNode As TreeNode
            For Each childNode In trReftree.SelectedNode.Nodes
                trReftree.SelectedNode = childNode
            Next
        End If
    End Sub
End Class
