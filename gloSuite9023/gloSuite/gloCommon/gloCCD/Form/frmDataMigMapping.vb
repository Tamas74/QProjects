Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Public Class frmDataMigMapping
    Dim ht As Hashtable
    'Dim strConnectionString As String = "SERVER=" & gServerName & ";DATABASE=" & gDataBase & ";Integrated Security=SSPI"
    Dim myHl7DragNode As MyTreeNode
    Dim mygloEMRDragNode As MyTreeNode
    Dim SendingApplication As String
    Dim SendingApplicationField As String
    Private blnIsOutBoundMapping As Boolean
    Dim blnNoInboundClients As Boolean = True
    Dim blnNoOutboundClients As Boolean = True
    Private blnIsLoaded As Boolean = False
    Dim gstrMessageBoxCaption As String = "gloEMR"
    Private _LoginUser As String = ""
    Private _LoginID As Int64 = 0
    Private _ClinicID As Int64 = 1


    Private Sub frmHL7Data_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtVersion.Text = "1.0"
        Dim dt As DataTable = FillReferencePath()
        BrowseToolStripButton_Click(sender, e)

    End Sub

    'Code Start- Added by kanchan on 20100324 for altapoint
    Private Sub PopulateHL7Data()
        Dim oFile As FileInfo = Nothing
        Dim dom As New XmlDocument()
        Dim tNode As MyTreeNode = Nothing
        Dim mynode As MyTreeNode

        Try
            oFile = New FileInfo(gloLibCCDGeneral.CCDFilePath)
            If oFile IsNot Nothing Then

                ' SECTION 1. Create a DOM Document and load the XML data into it.
                dom.Load(oFile.FullName)

                ' SECTION 2. Initialize the TreeView control.
                trHL7Fields.Nodes.Clear()

                'Dim oXMLSettings As New Xml.XmlReaderSettings()
                'Dim xReader As XmlReader
                'xReader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath, oXMLSettings)



                'While (xReader.Read())

                '    Select xReader.NodeType

                '        Case XmlNodeType.Element
                '            'Console.Write("<" + xReader.Name)
                '            mynode = New MyTreeNode(xReader.Name)
                '            trHL7Fields.ImageIndex = 4
                '            trHL7Fields.SelectedImageIndex = 4
                '            trHL7Fields.Nodes.Add(mynode)
                '            mynode = Nothing
                '            xReader.
                '        Case XmlNodeType.Text
                '            'Console.WriteLine (xReader.Value);

                '        Case XmlNodeType.EndElement
                '            'Console.Write("</" + xReader.Name)


                '    End Select

                'End While




                mynode = New MyTreeNode(dom.DocumentElement.Name)
                trHL7Fields.ImageIndex = 4
                trHL7Fields.SelectedImageIndex = 4


                'If Not IsNothing(mynode.Attributes) AndAlso mynode.Attributes.Count > 0 Then

                '    For index As Integer = 0 To mynode.Attributes.Count - 1
                '        trHL7Fields.Nodes.Add(New MyTreeNode(mynode.Attributes.ItemOf(index).InnerText.ToString()))

                '    Next

                'End If

                trHL7Fields.Nodes.Add(mynode)
                tNode = trHL7Fields.Nodes(0)
                tNode.ImageIndex = 4
                tNode.SelectedImageIndex = 4
                ' SECTION 3. Populate the TreeView with the DOM nodes.
                AddNode(dom.DocumentElement, tNode)
                trHL7Fields.ExpandAll()


            End If

        Catch ex As Exception
            'ClsMigrationGeneral.UpdateLog(ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            mynode = Nothing
            If Not IsNothing(oFile) Then
                oFile = Nothing
            End If
            If Not IsNothing(dom) Then
                dom = Nothing
            End If
            If Not IsNothing(tNode) Then
                tNode = Nothing
            End If
            If Not IsNothing(mynode) Then
                mynode = Nothing
            End If
        End Try
    End Sub
    'Code End- Added by kanchan on 20100324 for altapoint

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub trHL7Fields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trHL7Fields.DoubleClick
        Dim _documentprefix As String = ""
        Dim mySelectedNode As MyTreeNode
        Dim myTNODE As New MyTreeNode
        Dim _xPath As String
        Dim oFile As FileInfo
        Dim doc As New XmlDocument()

        Dim cnn As New SqlConnection()
        Dim _strSQL As String = ""
        Dim AllowNode As Int32
        Dim cmd As SqlCommand = Nothing

        Dim RefSelectedNode As MyTreeNode
        Dim nsmgr As XmlNamespaceManager
        Try
            If IsNothing(trHL7Fields.SelectedNode.Parent) Then              'check if parent do nothing 
                Exit Sub
            Else
                mySelectedNode = CType(trHL7Fields.SelectedNode, MyTreeNode)

                'Find count of particular node of XML file
                oFile = New FileInfo(gloLibCCDGeneral.CCDFilePath)
                doc.Load(oFile.FullName)
                Dim Selectedcount As New Int64
                nsmgr = New XmlNamespaceManager(doc.NameTable)
                If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                    _documentprefix = "CR"
                    nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                    _xPath = GetPrefixedXPath(mySelectedNode.FullPath, _documentprefix)
                Else
                    _documentprefix = doc.DocumentElement.Prefix
                    nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                    _xPath = GetPrefixedXPath(mySelectedNode.FullPath, "")
                End If

                Selectedcount = doc.SelectNodes(_xPath, nsmgr).Count
                If (Selectedcount > 1) Then

                    cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
                    'Dim cnn As New SqlConnection(_Connectionstring)
                    cnn.Open()
                    'Dim sqlAdpt As New SqlDataAdapter
                    'Dim dt As New DataTable

                    cmd = New SqlCommand
                    cmd.Connection = cnn
                    cmd.CommandType = CommandType.Text

                    _strSQL = "SELECT COUNT(sNodePath) FROM dbo.CCR_MultipleComplexNode WHERE sNodePath='" & mySelectedNode.FullPath & "'"
                    cmd.CommandText = _strSQL
                    AllowNode = cmd.ExecuteScalar()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    cnn.Close()
                    cnn.Dispose()
                    cnn = Nothing
                    If (AllowNode > 0) Then

                        Dim frm As New frmRefTreeNode(_LoginID, _ClinicID, mySelectedNode)
                        ''   frm.WindowState = FormWindowState.Maximized
                        frm.StartPosition = FormStartPosition.CenterScreen
                        frm.BringToFront()
                        frm.ShowInTaskbar = False
                        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        RefSelectedNode = CType(frm._TempTreeNode, MyTreeNode)
                        Dim temp As String

                        temp = mySelectedNode.Parent.FullPath '+ "\" + RefSelectedNode.FullPath
                        myTNODE.RefFieldPath = temp
                        If Not IsNothing(RefSelectedNode.Parent) Then
                            myTNODE.RefFieldNode = RefSelectedNode.Parent.FullPath.ToString()
                        Else
                            myTNODE.RefFieldNode = RefSelectedNode.FullPath.ToString()
                        End If

                        If Not IsNothing(RefSelectedNode.ToolTipText) Then
                            myTNODE.RefFieldValue = RefSelectedNode.ToolTipText
                        Else
                            myTNODE.RefFieldValue = ""
                        End If
                        temp = Nothing
                        frm.Dispose()
                        frm = Nothing
                    End If
                    '' ''''''''
                End If
                myTNODE.FieldName = mySelectedNode.FullPath 'Node Full Path
                myTNODE.Text = mySelectedNode.Text
                myTNODE.TableName = mySelectedNode.TableName 'Node Type
                myTNODE.DispalyName = mySelectedNode.FullPath

                myTNODE.ImageIndex = 4
                myTNODE.SelectedImageIndex = 4

                'Dim mySelectedNode.
                trMappedFields.Nodes.Add(myTNODE)
                trMappedFields.SelectedNode = myTNODE
                trMappedFields.ExpandAll()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            _documentprefix = Nothing
            mySelectedNode = Nothing
            _xPath = Nothing
            oFile = Nothing
            doc = Nothing
            _strSQL = Nothing
            nsmgr = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Add the Prefix on xmlpath
    ''' </summary>
    ''' <param name="XPath"></param>
    ''' <param name="Prefix"></param>
    ''' <returns>Prefix path</returns>
    ''' <remarks></remarks>
    Private Function GetPrefixedXPath(ByVal XPath As String, ByVal Prefix As String) As String
        Dim _prefixXPath As String = ""

        Try
            XPath = XPath.Replace("\", "/")

            If Prefix.Trim() <> "" Then
                _prefixXPath = XPath.Replace("/", "/" & Prefix.Trim() & ":")
                _prefixXPath = Prefix & ":" & _prefixXPath
            Else
                _prefixXPath = XPath
            End If
        Catch ex As Exception
            _prefixXPath = ""
        End Try

        Return _prefixXPath
    End Function

    Private Function FillReferencePath() As DataTable

        Dim _table As New DataTable
        Dim sqladp As SqlDataAdapter
        Dim cnn As New SqlConnection()
        ' Dim cmd As New SqlCommand()
        Dim _strSQL As String = Nothing

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            _strSQL = "SELECT isnull(sNodePath,'') FROM dbo.CCR_MultipleComplexNode "
            sqladp = New SqlDataAdapter(_strSQL, cnn)
            sqladp.Fill(_table)
            sqladp.Dispose()
            sqladp = Nothing

        Catch ex As Exception
            _table = Nothing
        Finally

            If Not IsNothing(cnn) Then
                cnn.Dispose()
            End If
            _strSQL = Nothing
        End Try

        Return _table

    End Function
    'this functionality same for drag & drop events for gloEMR  field
    Private Sub trgloEMRFields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trgloEMRFields.DoubleClick
        'Dim _SepChar As String
        Dim mySelectedNode As MyTreeNode            '3rd treeview-trgloEMRFields selected item
        Dim myTNODE As New MyTreeNode
        Dim Parentnode As MyTreeNode
        Dim myMappedSTRSelectedNode As MyTreeNode
        Try

            If IsNothing(trgloEMRFields.SelectedNode.Parent) Then     'check if parent do nothing 
                Exit Sub                                               'else add
            Else
                If (trMappedFields.GetNodeCount(False) > 0) Then 'check if trmapp treeview has node or blank

                    mySelectedNode = CType(trgloEMRFields.SelectedNode, MyTreeNode)

                    myTNODE.FieldName = mySelectedNode.FieldName
                    myTNODE.Text = mySelectedNode.Text
                    myTNODE.Datatype = mySelectedNode.Datatype
                    myTNODE.DispalyName = mySelectedNode.DispalyName
                    myTNODE.Separator = mySelectedNode.Parent.FullPath    ' Parent Node

                    ' Dim mySelectedNodehl7 As New MyTreeNode


                    myMappedSTRSelectedNode = CType(trMappedFields.SelectedNode, MyTreeNode)

                    If Not IsNothing(myMappedSTRSelectedNode.Text) Then
                        Parentnode = myMappedSTRSelectedNode.Parent
                        If Not IsNothing(Parentnode) Then                        '
                            Parentnode = myMappedSTRSelectedNode.Parent
                            If Parentnode.GetNodeCount(False) = 1 Then
                                'msg separator
                                Dim result As Int32
                                result = MessageBox.Show("You are binding multiple gloEMR Field to single Field, So you want to add any separator character ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                            ElseIf Parentnode.GetNodeCount(False) > 1 Then
                                myTNODE.ImageIndex = 5
                                myTNODE.SelectedImageIndex = 5
                                Parentnode.Nodes.Add(myTNODE)
                                trMappedFields.ExpandAll()

                            End If
                        Else
                            ''previous logic -> if selected node of trMapped field is parent node then add (child)multiple gloemr fields or single field
                            ' ''MsgBox("Parent node....HL7Field", MsgBoxStyle.Information)
                            ''trMappedFields.SelectedNode.Nodes.Clear()
                            ''mynode.ImageIndex = 5 '3
                            ''mynode.SelectedImageIndex = 5 '3
                            ''trMappedFields.SelectedNode.Nodes.Add(mynode)
                            ''trMappedFields.ExpandAll()

                            'if selected node of trMapped field is parent node then add multiple fields or single field
                            'New logic "this functionality must add on trgloEMR_drag & drop event"

                            If (myMappedSTRSelectedNode.GetNodeCount(True) = 0) Then '\if 'ckeck if selected node has any child node or not, if no add gloemrfields node else add separator message
                                'myTNODE.Separator = ""
                                myTNODE.ImageIndex = 5
                                myTNODE.SelectedImageIndex = 5
                                myMappedSTRSelectedNode.Nodes.Add(myTNODE)
                                trMappedFields.ExpandAll()
                            Else
                                'now selected node having at least 1 child so add separator
                                If (myMappedSTRSelectedNode.GetNodeCount(True) = 1) Then
                                    Dim result As Int32
                                    result = MessageBox.Show("You are binding multiple gloEMR Field to single Field, So you want to add any separator character ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

                                ElseIf (myMappedSTRSelectedNode.GetNodeCount(True)) > 1 Then 'if more than 1 child don't ask for separator char, direct add child

                                    'myTNODE.Separator = ""
                                    myTNODE.ImageIndex = 5
                                    myTNODE.SelectedImageIndex = 5
                                    myMappedSTRSelectedNode.Nodes.Add(myTNODE)
                                    trMappedFields.ExpandAll()

                                End If
                            End If '\endif

                        End If
                    Else
                        Exit Try
                        'MsgBox("Please Select Mapping [Middle] TreeView node under which You want to add gloEMRField....", MsgBoxStyle.Information)
                    End If
                Else
                    Exit Try
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            mySelectedNode = Nothing       '3rd treeview-trgloEMRFields selected item
            myTNODE = Nothing
            Parentnode = Nothing
            myMappedSTRSelectedNode = Nothing
        End Try
    End Sub
    'Close Button
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripButton2.Click
        Me.Close()
        'Dim frmObject As New frmStartup()
        'frmObject.Show()
    End Sub
    'SAVE Button
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton1.Click
        Dim arry As New ArrayList
        Dim ogloCCR_Interface As New gloCCR_Interface()
        ogloCCR_Interface.CCRVersion = txtVersion.Text.Trim()
        ogloCCR_Interface.UserName = _LoginUser
        ogloCCR_Interface.UserID = _LoginID
        ogloCCR_Interface.ClinicID = _ClinicID
        Try
            If trMappedFields.Nodes.Count = 0 Then
                MessageBox.Show("Please bind CSV Fields with gloEMRFields", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not IsNothing(ogloCCR_Interface) Then
                    ogloCCR_Interface.Dispose()
                    ogloCCR_Interface = Nothing
                End If
                Exit Sub
            End If

            If trMappedFields.GetNodeCount(False) = False Then   '-check child node present or treeview is blank or not
                If (trHL7Fields.GetNodeCount(True) = 1) Then
                    If Not IsNothing(ogloCCR_Interface) Then
                        ogloCCR_Interface.Dispose()
                        ogloCCR_Interface = Nothing
                    End If
                    Exit Sub
                End If
                MessageBox.Show("Please bind CSV Fields with gloEMRFields", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                If Not IsNothing(ogloCCR_Interface) Then
                    ogloCCR_Interface.Dispose()
                    ogloCCR_Interface = Nothing
                End If
                Exit Sub
            End If

            For i As Int32 = 0 To trMappedFields.GetNodeCount(False) - 1    '-check child node present or not
                If (trMappedFields.Nodes.Item(i).GetNodeCount(False) <= 0) Then
                    MessageBox.Show("Please bind CSV Fields with gloEMRFields", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    If Not IsNothing(ogloCCR_Interface) Then
                        ogloCCR_Interface.Dispose()
                        ogloCCR_Interface = Nothing
                    End If
                    Exit Sub
                End If
            Next


            Dim flag As Boolean = True      'PID
            Dim temp As String
            For i As Int32 = 0 To trMappedFields.GetNodeCount(False) - 1

                'Dim myMappedSTRSelectedNode As New MyTreeNode

                'myMappedSTRSelectedNode = CType(trMappedFields.Nodes.Item(i), MyTreeNode)

                Dim objclsUsrMapp As New ClsUserMappingField
                Dim thisNode As MyTreeNode = CType(trMappedFields.Nodes.Item(i), MyTreeNode)
                Dim thisNodeItem0 As MyTreeNode = CType(trMappedFields.Nodes.Item(i).Nodes.Item(0), MyTreeNode)
                objclsUsrMapp.EventName = thisNode.Text
                objclsUsrMapp.OtherFieldName = thisNode.FieldName
                objclsUsrMapp.gloEMRFieldName = thisNodeItem0.FieldName
                objclsUsrMapp.gloEMRDisplayName = thisNodeItem0.Text
                objclsUsrMapp.DataType = thisNodeItem0.Datatype
                objclsUsrMapp.gloEMRModuleName = thisNodeItem0.Separator
                objclsUsrMapp.NodeType = thisNode.TableName
                temp = thisNode.RefFieldPath
                If Not IsNothing(temp) Then
                    objclsUsrMapp.ReferenceNodePath = thisNode.RefFieldPath
                Else
                    objclsUsrMapp.ReferenceNodePath = ""
                End If
                temp = thisNode.RefFieldValue
                If Not IsNothing(temp) Then
                    objclsUsrMapp.ReferenceNodeValue = thisNode.RefFieldValue
                Else
                    objclsUsrMapp.ReferenceNodeValue = ""
                End If
                temp = thisNode.RefFieldNode
                If Not IsNothing(temp) Then
                    objclsUsrMapp.ReferenceNode = thisNode.RefFieldNode
                Else
                    objclsUsrMapp.ReferenceNode = ""
                End If


                arry.Add(objclsUsrMapp)
                objclsUsrMapp = Nothing
                thisNode = Nothing
                thisNodeItem0 = Nothing

            Next

            If (ogloCCR_Interface.InsertUserMappingField(arry)) Then
                'MsgBox("UserMapping has been mapped for Inbound Message successfully ", MsgBoxStyle.Information)
                MessageBox.Show("User mapping has been mapped for migration successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                'MsgBox("Data Can't Added.....Error.", MsgBoxStyle.Information)
                MessageBox.Show("Data Can't Added.....Error", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            arry.Clear()
            arry = Nothing

            'First end if



        Catch ex As Exception
            MessageBox.Show(ex.ToString, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ogloCCR_Interface) Then
                ogloCCR_Interface.Dispose()
                ogloCCR_Interface = Nothing
            End If
        End Try

    End Sub


    Private Sub trMappedFields_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trMappedFields.MouseDown
        Dim Parentnode As MyTreeNode
        Try
            'Dim Parentnode As TreeNode
            'Dim myMappedSTRSelectedNode As New MyTreeNode
            Parentnode = CType(trMappedFields.GetNodeAt(e.X, e.Y), MyTreeNode)
            'Parentnode = trMappedFields.GetNodeAt(e.X, e.Y)
            '
            If Not IsNothing(Parentnode) Then
                trMappedFields.SelectedNode = CType(Parentnode, TreeNode)
                'trMappedFields.SelectedNode = Parentnode
            End If

            If Not IsNothing(Parentnode) Then
                If e.Button = Windows.Forms.MouseButtons.Right Then

                    If Not IsNothing(Parentnode.Parent) Then
                        'child selected
                        mnuDelHL7Field.Visible = False
                        mnuDelgloEMRField.Visible = True
                    Else
                        'Parent selected
                        mnuDelgloEMRField.Visible = False
                        mnuDelHL7Field.Visible = True
                    End If
                Else
                    mnuDelgloEMRField.Visible = False
                    mnuDelHL7Field.Visible = False
                End If
            Else
                mnuDelgloEMRField.Visible = False
                mnuDelHL7Field.Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Parentnode = Nothing
        End Try
    End Sub
    'Remove Child fields 
    Private Sub mnuDelgloEMRField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelgloEMRField.Click

        'Dim Parentnode As TreeNode
        'Parentnode = trMappedFields.SelectedNode.Parent   'get parent
        Dim Parentnode As MyTreeNode
        Dim myMappedSTRSelectedNode As MyTreeNode
        Try
            Parentnode = CType(trMappedFields.SelectedNode.Parent, MyTreeNode)

            'if remove from multilple child group of 1st child then copy that separator char to second child
            myMappedSTRSelectedNode = CType(trMappedFields.SelectedNode, MyTreeNode)

            If Trim(myMappedSTRSelectedNode.Separator) <> "" Then
                If Parentnode.GetNodeCount(False) > 1 Then
                    If Not IsNothing(Parentnode.FirstNode) Then
                        CType(Parentnode.FirstNode, MyTreeNode).Separator = myMappedSTRSelectedNode.Separator
                    End If
                Else
                    CType(Parentnode.FirstNode, MyTreeNode).Separator = ""
                End If
            End If

            trMappedFields.SelectedNode.Remove()
            ' trMappedFields.SelectedNode.Nodes.Clear()
            trMappedFields.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Parentnode = Nothing
            myMappedSTRSelectedNode = Nothing
        End Try
    End Sub
    'Remove Parent fields
    Private Sub mnuDelHL7Field_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelHL7Field.Click

        trMappedFields.SelectedNode.Remove()

        trMappedFields.ExpandAll()


    End Sub


    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim oDatabaselayer As New ClsDatabaseLayer
        'oDatabaselayer.ConnectionString = ClsMigrationGeneral.Connectionstring
        'Try
        '    Dim sdisplayName As String = ""
        '    Dim EventName As String = ""
        '    Dim dt As New DataTable
        '    If trMappedFields.Nodes.Count = 0 Then
        '        Exit Sub
        '    End If
        '    EventName = cmbHL7Event.SelectedItem(0)

        '    dt = oDatabaselayer.DefaultMapping(EventName)
        '    trMappedFields.Nodes.Clear()

        '    For i As Int32 = 0 To dt.Rows.Count - 1

        '        Dim ParentNodeFieldName As String = (dt.Rows(i)("sOtherFieldName")).ToString

        '        '' by Abhijeet on 20100526
        '        ''Dim Prnode As New MyTreeNode(ParentNodeFieldName)
        '        Dim Prnode As New MyTreeNode(ParentNodeFieldName, ParentNodeFieldName, "")
        '        '' End of changes by Abhijeet on 20100526

        '        trMappedFields.Nodes.Add(Prnode)

        '        Dim ChildFieldName As String = (dt.Rows(i)("gloEMRFieldName")).ToString
        '        Dim FieldDisplayname As String = (dt.Rows(i)("sgloEMRDisplayName")).ToString
        '        Dim datatype As String = (dt.Rows(i)("sDataType")).ToString

        '        Dim Chnode As New MyTreeNode(ChildFieldName, FieldDisplayname, datatype, "")
        '        Chnode.ImageIndex = 5
        '        Chnode.SelectedImageIndex = 5
        '        Prnode.Nodes.Add(Chnode)
        '        trMappedFields.ExpandAll()

        '    Next

        '    If (trMappedFields.GetNodeCount(True)) > 0 Then                   'Select default 1st node 
        '        trMappedFields.SelectedNode = trMappedFields.Nodes.Item(0)
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    If Not IsNothing(oDatabaselayer) Then
        '        oDatabaselayer.Dispose()
        '        oDatabaselayer = Nothing
        '    End If
        'End Try


    End Sub

    'Private Sub trgloEMRFields_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trgloEMRFields.ItemDrag
    '    DoDragDrop(e.Item, DragDropEffects.Copy)
    'End Sub

    Private Sub tbxSearchHL7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbxSearchHL7.KeyDown

    End Sub

    Private Sub tbxSearchHL7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbxSearchHL7.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trHL7Fields.Select()
        Else
            trHL7Fields.SelectedNode = trHL7Fields.Nodes.Item(0)
        End If
    End Sub

    'SEARCH HL7 fields
    Private Sub tbxSearchHL7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxSearchHL7.TextChanged
        ' Dim mynode As TreeNode
        Dim mynode As MyTreeNode
        Try

            If Trim(tbxSearchHL7.Text) <> "" Then

                For Each mynode In trHL7Fields.Nodes.Item(0).Nodes
                    Dim str As String
                    str = UCase(Trim(mynode.Text))
                    If UCase(Mid(str, 1, Len(UCase(Trim(tbxSearchHL7.Text))))) = UCase(Trim(tbxSearchHL7.Text)) Then
                        If Not IsNothing(trHL7Fields.SelectedNode) Then
                            If Not IsNothing(trHL7Fields.SelectedNode.LastNode) Then
                                trHL7Fields.SelectedNode = trHL7Fields.SelectedNode.LastNode
                            End If
                        End If

                        trHL7Fields.SelectedNode = mynode
                        tbxSearchHL7.Focus()
                        Exit Sub
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            mynode = Nothing
        End Try

    End Sub

    'Private Sub AddSearchNode(Optional ByVal strsearch As String = "")

    '    Dim oDatabaselayer As New ClsDatabaseLayer
    '    oDatabaselayer.ConnectionString = strConnectionString

    '    Try
    '        trHL7Fields.BeginUpdate()
    '        trHL7Fields.Visible = False
    '        If trHL7Fields.GetNodeCount(False) > 0 Then
    '            trHL7Fields.Nodes.Item(0).Remove()
    '            Dim objmytreenode As New TreeNode
    '            objmytreenode.Text = cmbHL7Segment.SelectedItem.ToString
    '            'objmytreenode.Key = -1
    '            'objmytreenode.ImageIndex = 5
    '            'objmytreenode.SelectedImageIndex = 5
    '            trHL7Fields.Nodes.Add(objmytreenode)
    '        End If
    '        trHL7Fields.Visible = True

    '        Dim segName As String = 0
    '        Dim dt As New DataTable

    '        segName = CType(cmbHL7Segment.SelectedItem, ClsHL7Tables).SegmentName

    '        ' dt = oDatabaselayer.FilltrHL7Fields(segName, strsearch)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub tbxSearchgloEMR_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbxSearchgloEMR.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trgloEMRFields.Select()
        Else
            'trgloEMRFields.SelectedNode = trgloEMRFields.Nodes.Item(0)
            trgloEMRFields.SelectedNode = CType(trgloEMRFields.Nodes.Item(0), MyTreeNode)
        End If
    End Sub

    'SEARCH gloEMR fields
    Private Sub tbxSearchgloEMR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxSearchgloEMR.TextChanged
        Try
            If Trim(tbxSearchgloEMR.Text) <> "" Then
                For i As Int32 = 0 To trgloEMRFields.GetNodeCount(False) - 1
                    'Dim mynode As TreeNode
                    Dim mynode As MyTreeNode

                    For Each mynode In trgloEMRFields.Nodes.Item(i).Nodes
                        Dim str As String
                        str = mynode.Text
                        If UCase(Mid(str, 1, Len(UCase(Trim(tbxSearchgloEMR.Text))))) = UCase(Trim(tbxSearchgloEMR.Text)) Then
                            If Not IsNothing(trgloEMRFields.SelectedNode) Then
                                If Not IsNothing(trgloEMRFields.SelectedNode.LastNode) Then
                                    trgloEMRFields.SelectedNode = trgloEMRFields.SelectedNode.LastNode

                                End If
                            End If
                            trgloEMRFields.SelectedNode = mynode
                            tbxSearchgloEMR.Focus()
                            Exit Sub

                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'not Use
    'Private Sub trHL7Fields_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trHL7Fields.MouseMove
    ' ''// Get the node at the current mouse pointer location.
    'Dim theNode As TreeNode = Me.trHL7Fields.GetNodeAt(e.X, e.Y)
    ' ''// Set a ToolTip only if the mouse pointer is actually paused on a node.
    'If Not IsNothing(theNode) Then
    '    If theNode.Tag <> "" Then
    '        If (theNode.Tag.ToString() <> Me.tltpHL7Fields.GetToolTip(Me.trHL7Fields)) Then
    '            Me.tltpHL7Fields.SetToolTip(Me.trHL7Fields, theNode.ToolTipText.ToString())
    '            ''        theNode.ToolTipText = theNode.ToolTipText.ToString()
    '        End If
    '    Else'
    '        tltpHL7Fields.SetToolTip(trHL7Fields, "")
    '    End If
    'Else
    '    tltpHL7Fields.SetToolTip(trHL7Fields, "")
    'End If
    'tltpHL7Fields.Hide(Me)
    ' End Sub


    ''SHOW TOOLTIP Value

    Private Sub trHL7Fields_NodeMouseHover(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseHoverEventArgs) Handles trHL7Fields.NodeMouseHover
       
        If IsNothing(e.Node.Parent) Then
            Exit Sub
        End If
        Dim theNode As MyTreeNode = CType(e.Node, MyTreeNode)
        Try
            If Not IsNothing(theNode) Then
                If theNode.DispalyName <> "" Then
                    If (theNode.DispalyName.ToString() <> Me.tltpHL7Fields.GetToolTip(Me.trHL7Fields)) Then
                        Me.tltpHL7Fields.SetToolTip(Me.trHL7Fields, theNode.ToolTipText.ToString())
                    End If
                Else
                    tltpHL7Fields.SetToolTip(trHL7Fields, "")
                End If

            Else
                tltpHL7Fields.SetToolTip(trHL7Fields, "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            theNode = Nothing
        End Try
    End Sub
    ''By Suraj
    Private Sub trMappedFields_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trMappedFields.DragOver
        e.Effect = DragDropEffects.Copy
    End Sub

    'logic for dragdrop hl7 fields to mapped tree view
    Private Sub trHL7Fields_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trHL7Fields.DragDrop, trMappedFields.DragDrop
        Dim dropNode As MyTreeNode
        Dim targetNode As MyTreeNode
        Dim childnode As MyTreeNode
        Dim newparent As MyTreeNode
        Try
            ' If e.Data.GetDataPresent("System.Windows.Forms.MyTreeNode", True) = False Then Exit Sub
            'If e.Data.GetDataPresent("System.Windows.Forms.gloHL7Mapping.MyTreeNode", True) = False Then Exit Sub
            ' Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), MyTreeNode)
            If Not IsNothing(myHl7DragNode) Then
                'MessageBox.Show("hl7 drop" & myHl7DragNode.Text)
                dropNode = myHl7DragNode

                myHl7DragNode = Nothing

                'myHl7DragNode = Nothing
                'Dim targetNode As TreeNode = trMappedFields.SelectedNode
                targetNode = CType(trMappedFields.SelectedNode, MyTreeNode)

                If dropNode.TreeView Is trHL7Fields Then
                    'Dim childnode As TreeNode
                    'childnode = CType(dropNode.Clone, TreeNode)
                    childnode = New MyTreeNode(dropNode.FieldName, dropNode.DispalyName, dropNode.TableName)

                    Dim flag As Boolean = False
                    For i As Int32 = 0 To trMappedFields.GetNodeCount(False) - 1
                        'If (childnode.Text = trMappedFields.Nodes.Item(i).Text) Then
                        If childnode.Text = CType(trMappedFields.Nodes.Item(i), MyTreeNode).Text Then
                            flag = True
                            Exit For
                        Else
                            flag = False
                        End If
                    Next
                    If flag = False Then
                        'Dim newparent As TreeNode
                        newparent = childnode
                        trMappedFields.Nodes.Add(newparent)
                        newparent.ImageIndex = 4 '1                             'add image icon
                        newparent.SelectedImageIndex = 4 '1
                        trMappedFields.SelectedNode = newparent
                        trMappedFields.ExpandAll()
                    Else
                        ' MessageBox.Show("Please Select another HL7Fields , this Field already Exist into Mapped Fields...", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dropNode = Nothing
            targetNode = Nothing
            childnode = Nothing
            newparent = Nothing
        End Try

    End Sub

    '' **** Drag Drop Events HL7
    Private Sub trHL7Fields_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trHL7Fields.MouseDown

        If IsNothing(trHL7Fields.GetNodeAt(e.X, e.Y)) Then
            Exit Sub
        End If
        If IsNothing(trHL7Fields.GetNodeAt(e.X, e.Y).Parent) Then
            Exit Sub
        End If
        If (Not IsNothing(trHL7Fields.GetNodeAt(e.X, e.Y))) Then

            ' myHl7DragNode = New MyTreeNode
            myHl7DragNode = trHL7Fields.GetNodeAt(e.X, e.Y)

        End If

    End Sub

    ''Drag Drop Events EMR

    'logic for dragdrop gloEMR fields to mapped tree view
    'this functionality same for gloemr field double click

    Private Sub trgloEMRFields_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trgloEMRFields.DragDrop, trMappedFields.DragDrop
        'Dim _SepChar As String
        Dim dropNode As MyTreeNode
        Dim targetNode As MyTreeNode
        Dim childnode As MyTreeNode
        Dim Parentnode As MyTreeNode
        Try

            If Not IsNothing(mygloEMRDragNode) Then  'chk for trgloEMR treeview node is empty

                dropNode = mygloEMRDragNode

                mygloEMRDragNode = Nothing

                If (trMappedFields.GetNodeCount(False) > 0) Then   'chk for trmapped treeview is empty

                    targetNode = CType(trMappedFields.SelectedNode, MyTreeNode)

                    If dropNode.TreeView Is trgloEMRFields Then
                        'Dim childnode As TreeNode

                        If (dropNode.Nodes.Count > 0) Then     'check - if parent, do nothing. (ckeck for in gloEMR treeview)
                            Exit Sub
                        Else                                    'now you can add it to trMapped treeview
                            'childnode = CType(dropNode.Clone, TreeNode)
                            childnode = New MyTreeNode(dropNode.FieldName, dropNode.DispalyName, dropNode.Datatype, dropNode.Separator)

                            childnode.ImageIndex = 3
                            childnode.SelectedImageIndex = 3
                            'Dim Parentnode As TreeNode
                            If Not IsNothing(targetNode.Text) Then
                                Parentnode = targetNode.Parent
                                If Not IsNothing(Parentnode) Then        'check node for parent or child node
                                    'MsgBox("Child node....gloEMRField", MsgBoxStyle.Information)
                                    Parentnode = targetNode.Parent   'get parent
                                    ' ''targetNode.Remove()              'Delete treeviewnode

                                    ' ''Parentnode.Nodes.Clear()         'clear treenode
                                    ' ''childnode.ImageIndex = 5                             'add image icon
                                    ' ''childnode.SelectedImageIndex = 5
                                    ' ''Parentnode.Nodes.Add(childnode)     'add 3rd trview node  
                                    ' ''trMappedFields.ExpandAll()
                                    '-----new added by suraj200800904
                                    If Parentnode.GetNodeCount(False) = 1 Then
                                        'msg separator
                                        Dim result As Int32
                                        result = MessageBox.Show("You are binding multiple gloEMR Field to single Field, So you want to add any separator character ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

                                    ElseIf Parentnode.GetNodeCount(False) > 1 Then
                                        childnode.ImageIndex = 5
                                        childnode.SelectedImageIndex = 5
                                        Parentnode.Nodes.Add(childnode)
                                        trMappedFields.ExpandAll()

                                    End If
                                    '-----
                                Else
                                    'MsgBox("Parent node....HL7Field", MsgBoxStyle.Information)
                                    ''new logic for separator
                                    If (targetNode.GetNodeCount(True) = 0) Then '\if 'ckeck if selected node has any child node or not, if no - add gloemrfields node else add separator message
                                        childnode.Separator = ""
                                        childnode.ImageIndex = 5
                                        childnode.SelectedImageIndex = 5
                                        targetNode.Nodes.Add(childnode)
                                        trMappedFields.ExpandAll()
                                    Else
                                        'now selected node having at least 1 child so add separator
                                        'separator logic= check for selected node have only 1 child, then Msg else add node without asking Msg 
                                        If (targetNode.GetNodeCount(True) = 1) Then
                                            Dim result As Int32
                                            result = MessageBox.Show("You are binding multiple gloEMR Field to single Field, So you want to add any separator character ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                                        End If

                                    End If '\endif

                                End If
                            Else
                                'MsgBox("Please Select Mapping [Middle] TreeView node under which You want to add gloEMRField....", MsgBoxStyle.Information)
                            End If
                        End If
                    End If

                Else 'chk for trmapped treeview is empty
                    Exit Try
                End If

            Else 'chk for trgloEMR treeview node is empty
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dropNode = Nothing
            targetNode = Nothing
            childnode = Nothing
            Parentnode = Nothing
        End Try

    End Sub

    '' **** Drag Drop Events EMR    

    Private Sub trgloEMRFields_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trgloEMRFields.MouseDown

        'Get the node at mouse down
        If (Not IsNothing(trgloEMRFields.GetNodeAt(e.X, e.Y))) Then

            ' mygloEMRDragNode = new MyTreeNode
            mygloEMRDragNode = trgloEMRFields.GetNodeAt(e.X, e.Y)

        End If

    End Sub

    ''  **** By Suraj

    Private Sub trgloEMRFields_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trgloEMRFields.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Copy)
    End Sub

    Private Sub trHL7Fields_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trHL7Fields.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Copy)
    End Sub

    Public Sub New(ByVal _UserName As String, ByVal _UserID As Int64, ByVal ClinicID As Int64)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _LoginUser = _UserName
        _LoginID = _UserID
        _ClinicID = ClinicID
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub PopulateHL7DataInbound()
        Dim ogloCCR_Interface As New gloCCR_Interface()
        mnuDelHL7Field.Visible = True
        mnuDelgloEMRField.Visible = True

        trMappedFields.AllowDrop = True

        'assign selected clients value which is from addClient Form
        'cmbClients.SelectedIndex = cmbClients.FindStringExact(sSelectedClient)
        Dim flgCount As Int32 = 0

        Dim ParentNodeFieldName As String = Nothing
        Dim Prnode As MyTreeNode = Nothing
        Dim ChildFieldName As String = Nothing
        Dim FieldDisplayname As String = Nothing
        Dim ModuleName As String = Nothing
        Dim Chnode As MyTreeNode = Nothing
        Dim strParentNode As String = Nothing
        Try

            PopulateHL7Data()
            '''''''''''''''''''''''''''''''''''''''''

            ''------mapping display of gloEMRField & HL7Field in trMappFields treeview--------s
            Dim sdisplayName As String = 0
            Dim sCCRVer As String = 0
            Dim dt As DataTable

            'Dim displayEvent = cmbHL7Event.Text
            sCCRVer = txtVersion.Text.Trim
            'eventName = cmbHL7Event.SelectedValue
            dt = ogloCCR_Interface.DefaultMappingRestore(sCCRVer, "")

            ''Populate trMappedField treeview---------
            trMappedFields.Nodes.Clear()
            If (IsNothing(dt) = False) Then


                For i As Int32 = 0 To dt.Rows.Count - 1

                    ParentNodeFieldName = (dt.Rows(i)("sXMLAttributeName")).ToString

                    Prnode = New MyTreeNode(ParentNodeFieldName, dt.Rows(i)("sXMLAttributeName"), "")
                    Prnode.FieldName = (dt.Rows(i)("sXMLElementPath")).ToString 'XML Full path
                    Prnode.Separator = (dt.Rows(i)("sModuleName")).ToString 'gloEMR module name
                    Prnode.TableName = (dt.Rows(i)("sNodeType")).ToString 'Node type
                    Prnode.RefFieldNode = (dt.Rows(i)("sSingleMultipleEntity")).ToString
                    Prnode.RefFieldPath = (dt.Rows(i)("sReferencePath")).ToString
                    Prnode.RefFieldValue = (dt.Rows(i)("sReferenceValue")).ToString
                    'Prnode.ImageIndex = 5
                    'Prnode.SelectedImageIndex = 5
                    trMappedFields.Nodes.Add(Prnode)

                    ChildFieldName = (dt.Rows(i)("gloEMRFieldName")).ToString
                    FieldDisplayname = (dt.Rows(i)("gloEMRDisplayName")).ToString
                    ModuleName = (dt.Rows(i)("sModuleName")).ToString 'gloEMR module name
                    'Dim datatype As String = (dt.Rows(i)("sDataType")).ToString

                    'Dim Chnode As New MyTreeNode(ChildFieldName, FieldDisplayname, datatype, "")
                    Chnode = New MyTreeNode(ChildFieldName, FieldDisplayname, "")
                    Chnode.Separator = ModuleName
                    Chnode.ImageIndex = 5
                    Chnode.SelectedImageIndex = 5
                    Prnode.Nodes.Add(Chnode)
                    trMappedFields.ExpandAll()

                    ParentNodeFieldName = Nothing
                    Prnode = Nothing
                    ChildFieldName = Nothing
                    FieldDisplayname = Nothing
                    ModuleName = Nothing
                    Chnode = Nothing

                Next
                dt.Dispose()
                dt = Nothing
            End If
            '''''''''''''''''''''''''''''''''''''''''

            '--------get Categoryfnction to show in trgloEMRFields treeview-----------------------
            Dim dtable As DataTable


            dtable = ogloCCR_Interface.GetCategary()

            'trMappedFields.Nodes.Clear()-----------
            trgloEMRFields.Nodes.Clear()
            If (IsNothing(dtable) = False) Then


                For i As Int32 = 0 To dtable.Rows.Count - 1
                    strParentNode = dtable.Rows(i)("sCategoryName")
                    'Dim Prnode As New TreeNode
                    Prnode = New MyTreeNode(strParentNode)
                    'Prnode.Text = strParentNode

                    trgloEMRFields.Nodes.Add(Prnode)
                    Dim dtfields As DataTable
                    dtfields = ogloCCR_Interface.GetCategoryData(dtable.Rows(i)(0))
                    If (IsNothing(dtfields) = False) Then


                        For j As Int32 = 0 To dtfields.Rows.Count - 1
                            'Dim Chnode As New MyTreeNode
                            Chnode = New MyTreeNode(dtfields.Rows(j)(0), dtfields.Rows(j)(1), dtfields.Rows(j)(2), "")
                            ' Chnode.Tag = dtfields.Rows(j)(0) & "," & dtfields.Rows(j)(2)  'add dataType fields
                            ' Chnode.Text = dtfields.Rows(j)(1)
                            Chnode.ImageIndex = 3
                            Chnode.SelectedImageIndex = 3
                            Prnode.Nodes.Add(Chnode)
                            Chnode = Nothing
                        Next
                        dtfields.Dispose()
                        dtfields = Nothing
                    End If
                    Prnode = Nothing
                    strParentNode = Nothing
                Next
                dtable.Dispose()
                dtable = Nothing
            End If
            trgloEMRFields.ExpandAll()

            '-----------
            If (trMappedFields.GetNodeCount(True)) > 0 Then                   'Select default 1st node 
                trMappedFields.SelectedNode = trMappedFields.Nodes.Item(0)
                trMappedFields.SelectedImageIndex = 4
            End If

            If (trgloEMRFields.GetNodeCount(True)) > 0 Then                   'Select default 1st node
                trgloEMRFields.SelectedNode = trgloEMRFields.Nodes.Item(0)
            End If

            If (trHL7Fields.GetNodeCount(True)) > 0 Then                   'Select default 1st node
                trHL7Fields.SelectedNode = trHL7Fields.Nodes.Item(0)
            End If
            'End If

        Catch ex As Exception
            'ClsMigrationGeneral.UpdateLog(ex.ToString)
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ogloCCR_Interface) Then
                ogloCCR_Interface.Dispose()
                ogloCCR_Interface = Nothing
            End If

            ParentNodeFieldName = Nothing
            Prnode = Nothing
            ChildFieldName = Nothing
            FieldDisplayname = Nothing
            ModuleName = Nothing
            Chnode = Nothing
            strParentNode = Nothing

        End Try
    End Sub

    Private Sub BrowseToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseToolStripButton.Click
        Dim bresult As DialogResult = Nothing
        Dim ogloCCDInterface As gloCCDInterface = Nothing
        Dim sFileType As String = Nothing
        Try
            dlgOpenFile.Title = "Select Clinical Document"
            dlgOpenFile.Filter = "XML Files(*.xml)|*.xml"
            dlgOpenFile.CheckFileExists = True
            dlgOpenFile.Multiselect = False
            dlgOpenFile.ShowHelp = False
            dlgOpenFile.ShowReadOnly = False
            bresult = dlgOpenFile.ShowDialog(System.Windows.Forms.Form.ActiveForm)
            If bresult = Windows.Forms.DialogResult.OK Then
                gloLibCCDGeneral.CCDFilePath = dlgOpenFile.FileName

                ogloCCDInterface = New gloCCDInterface()
                sFileType = ogloCCDInterface.GetClinicalFileType()
                If sFileType = "CCR" Then
                    'Just for testing purpose Code Added
                    txtVersion.Text = "1.0"
                Else
                    txtVersion.Text = "2.5"
                End If

                Application.DoEvents()
                pnlPrintMessage.BringToFront()
                pnlPrintMessage.Visible = True
                'lblPleaseWait.Visible = True
                'lblPleaseWait.BringToFront()
                Application.DoEvents()
                Me.Cursor = Cursors.WaitCursor
                PopulateHL7DataInbound()
                Me.Cursor = Cursors.Default
                pnlPrintMessage.Visible = False
            Else
                MessageBox.Show("Select a file for Mapping", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ogloCCDInterface) Then
                ogloCCDInterface.Dispose()
                ogloCCDInterface = Nothing
            End If
            bresult = Nothing
            sFileType = Nothing
        End Try
    End Sub

    Private Sub AddNode(ByVal inXmlNode As XmlNode, ByVal inTreeNode As MyTreeNode)

        Dim xNode As XmlNode
        Dim tNode As MyTreeNode
        Dim nodeList As XmlNodeList
        Dim currentTreeNode As MyTreeNode
        Dim i As Integer

        ' Loop through the XML nodes until the leaf is reached.
        ' Add the nodes to the TreeView during the looping process.
        If inXmlNode.HasChildNodes Then

            nodeList = inXmlNode.ChildNodes
            For i = 0 To nodeList.Count - 1

                xNode = inXmlNode.ChildNodes(i)


                currentTreeNode = New MyTreeNode(xNode.Name)
                currentTreeNode.TableName = XmlNodeType.Element.ToString()

                Dim _attributeNode As MyTreeNode = Nothing

                If Not IsNothing(xNode.Attributes) AndAlso xNode.Attributes.Count > 0 Then
                    For index As Integer = 0 To xNode.Attributes.Count - 1
                        _attributeNode = New MyTreeNode(xNode.Attributes.ItemOf(index).Name)
                        _attributeNode.ImageIndex = 11
                        _attributeNode.SelectedImageIndex = 11
                        _attributeNode.TableName = XmlNodeType.Attribute.ToString()
                        currentTreeNode.Nodes.Add(_attributeNode)
                        _attributeNode = Nothing
                    Next
                End If

                'inTreeNode.Nodes.Add(New MyTreeNode(xNode.Name))
                inTreeNode.Nodes.Add(currentTreeNode)
                'tNode = inTreeNode.Nodes(i)
                tNode = inTreeNode.LastNode
                If i = 0 Then
                    tNode.ImageIndex = 3
                    tNode.SelectedImageIndex = 3
                Else
                    tNode.ImageIndex = 4
                    tNode.SelectedImageIndex = 4
                End If
                AddNode(xNode, tNode)
            Next
        Else
            ' Here you need to pull the data from the XmlNode based on the
            ' type of node, whether attribute values are required, and so forth.
            inTreeNode.Text = (inXmlNode.OuterXml).Trim()

            If inTreeNode.Text.Contains("xmlns") = False Then
                inTreeNode.Parent.ToolTipText = inTreeNode.Text
                inTreeNode.Parent.BackColor = Color.Yellow
                inTreeNode.Remove()
            Else
                'Dim _count As Integer = inTreeNode.Text.IndexOf("xmlns")
                inTreeNode.Text = inTreeNode.DispalyName 'inTreeNode.Text.Remove(_count).Replace("<", "").Replace(">", "")
            End If
        End If
        xNode = Nothing
        tNode = Nothing
        nodeList = Nothing
        currentTreeNode = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class