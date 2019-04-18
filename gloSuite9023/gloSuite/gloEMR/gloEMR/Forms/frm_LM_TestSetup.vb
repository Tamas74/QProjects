Imports gloEMR.gloStream.LabModule
Imports System.Reflection

'// Test View
Public Class frm_LM_TestSetup

    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    ''Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    ''Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    'Friend WithEvents lblCommandTop As System.Windows.Forms.Label
    ' Friend WithEvents lblCommandBottom As System.Windows.Forms.Label
    Friend WithEvents pnlCategory As System.Windows.Forms.Panel
    Friend WithEvents lblDividerCategory As System.Windows.Forms.Label
    Friend WithEvents lblCategoryHeader As System.Windows.Forms.Label
    Friend WithEvents lblDivierCategory As System.Windows.Forms.Label
    Friend WithEvents pnlList As System.Windows.Forms.Panel
    Friend WithEvents trvCategories As System.Windows.Forms.TreeView
    Friend WithEvents c1List As C1.Win.C1FlexGrid.C1FlexGrid
  
#End Region


    Private Const COL_NAME = 0
    Private Const COL_ID = 1
    Private Const COL_TESTGROUPFLAG = 2
    Private Const COL_LEVELNO = 3
    Private Const COL_GROUPNO = 4
    ''Added Rahul on 20101020
    Private Const COL_LOINCCode = 5
    ''
    Private Const COL_COUNT = 6

    Private Sub TestDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Fill_Categories()

        'Moved to Shown Event
        'If Not trvCategories.GetNodeCount(False) = 0 Then
        '    trvCategories.SelectedNode = trvCategories.Nodes(0)
        '    Dim _Category As String = trvCategories.SelectedNode.Text.Trim.Replace("'", "''")
        '    FillTestGroups(_Category)
        'Else
        '    FillTestGroups("")
        'End If
    End Sub
  
    Private Sub Fill_Categories()
        With trvCategories
            .Nodes.Clear()
            Dim oCategories As gloStream.LabModule.Category.Supporting.Categories
            Dim oMaintainCategories As New gloStream.LabModule.Category.MaintainCategory
            Dim oSupporting As New gloStream.LabModule.Category.Supporting.Supporting
            Dim _Type As String = oSupporting.CategoryType_enum_AsString(gloStream.LabModule.Category.Supporting.Supporting.enumCategoryType.Order)
            oCategories = oMaintainCategories.Categories(_Type)
            oSupporting = Nothing
            If Not oCategories Is Nothing Then
                For i As Int16 = 1 To oCategories.Count
                    Dim oNode As New TreeNode
                    With oNode
                        .Text = oCategories(i).Description
                        .Tag = oCategories(i).ID
                        .ImageIndex = 0
                        .SelectedImageIndex = 0
                    End With
                    If Not oNode Is Nothing Then
                        .Nodes.Add(oNode)
                    End If
                    oNode = Nothing
                Next
                oCategories.Dispose()
            End If

            oCategories = Nothing
            oMaintainCategories = Nothing
        End With
    End Sub

    Private Sub FillTestGroups(ByVal oCategory As String)
        With c1List
            gloC1FlexStyle.Style(c1List)
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 22
            .Cols(COL_NAME).Width = ((.Width / 4) * 4) - 20
            .Cols(COL_ID).Width = (.Width / 4) * 1
            .Cols(COL_TESTGROUPFLAG).Width = (.Width / 4) * 1
            .Cols(COL_LEVELNO).Width = (.Width / 4) * 1
            .Cols(COL_GROUPNO).Width = (.Width / 4) * 1
            ''Added Rahul on 20101020
            .Cols(COL_LOINCCode).Width = (.Width / 4) * 1
            ''
            .SetData(0, COL_NAME, "Tests")

            .Tree.Column = COL_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Dot
            .Tree.Indent = 15
            .Cols(COL_NAME).AllowEditing = False
            .Cols(COL_NAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter ''added for bugs no:4449
            .Cols(COL_ID).AllowEditing = False
            .Cols(COL_TESTGROUPFLAG).AllowEditing = False
            .Cols(COL_LEVELNO).AllowEditing = False
            .Cols(COL_GROUPNO).AllowEditing = False
            ''Added Rahul on 20101020
            .Cols(COL_LOINCCode).AllowEditing = False
            ''

            .Cols(COL_NAME).Visible = True
            .Cols(COL_ID).Visible = False
            .Cols(COL_TESTGROUPFLAG).Visible = False
            .Cols(COL_LEVELNO).Visible = False
            .Cols(COL_GROUPNO).Visible = False
            ''Added Rahul on 20100918
            .Cols(COL_LOINCCode).Visible = False
            ''

            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlClient.SqlDataReader
            ' Dim _strSQL As String

            '_strSQL = "SELECT LM_Test.lm_test_ID, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag, " _
            '& " LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description," _
            '& " LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo ,isnull(LM_Test.lm_test_sLonicID,'')as lm_test_sLonicID" _
            '& " FROM LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
            '& " WHERE (LM_Category.lm_category_Description = '" & oCategory & "' AND LM_Test.lm_test_Name IS NOT NULL) " _
            '& " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_ID, LM_Test.lm_test_TestGroupFlag"

            ''changes made for incident CAS-01734-P3T3M6 query replace by procedure and order by testname added
            oDB.Connect(GetConnectionString)
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@Category", oCategory, ParameterDirection.Input, SqlDbType.VarChar)
            oDataReader = oDB.ReadRecords("gsp_LM_getTestGroups")
            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        If oDataReader.Item("lm_test_GroupNo") = 0 Then
                            .Rows.Add()
                            With .Rows(.Rows.Count - 1)
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                '//.Style = FillControl.Styles("CS_Category")
                                .Node.Level = oDataReader.Item("lm_test_LevelNo")
                                .Node.Data = oDataReader.Item("lm_test_Name")
                                .Node.Key = oDataReader.Item("lm_test_ID")
                            End With
                            .SetData(.Rows.Count - 1, COL_ID, oDataReader.Item("lm_test_ID"))
                            .SetData(.Rows.Count - 1, COL_TESTGROUPFLAG, oDataReader.Item("lm_test_TestGroupFlag"))
                            .SetData(.Rows.Count - 1, COL_LEVELNO, oDataReader.Item("lm_test_LevelNo"))
                            .SetData(.Rows.Count - 1, COL_GROUPNO, oDataReader.Item("lm_test_GroupNo"))
                            ''Added Rahul on 20101020
                            .SetData(.Rows.Count - 1, COL_LOINCCode, oDataReader.Item("lm_test_sLonicID"))
                            ''
                        Else
                            Dim oFindNode As C1.Win.C1FlexGrid.Node
                            oFindNode = GetC1Node(oDataReader.Item("lm_test_GroupNo"))

                            If Not oFindNode Is Nothing Then
                                oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, oDataReader.Item("lm_test_Name"))

                                oFindNode.Data.ToString()
                                '//.Style = FillControl.Styles("CS_Category")
                                Dim _tmpRow As Integer = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If Not _tmpRow = -1 Then
                                    .Rows(_tmpRow).ImageAndText = True
                                    .Rows(_tmpRow).Height = 24
                                    .SetData(_tmpRow, COL_ID, oDataReader.Item("lm_test_ID"))
                                    .SetData(_tmpRow, COL_TESTGROUPFLAG, oDataReader.Item("lm_test_TestGroupFlag"))
                                    .SetData(_tmpRow, COL_LEVELNO, oDataReader.Item("lm_test_LevelNo"))
                                    .SetData(_tmpRow, COL_GROUPNO, oDataReader.Item("lm_test_GroupNo"))
                                    _tmpRow = -1
                                End If
                            End If
                        End If

                    End While
                End If
            End If
            oDB.Disconnect()
            oDB.DBParameters.Clear()
            oDB = Nothing
        End With

    End Sub

    Private Function GetC1Node(ByVal GroupNo As Long) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = c1List.FindRow(GroupNo, 0, COL_ID, False, True, True)
        If _FindRow > 0 Then
            _Node = c1List.Rows(_FindRow).Node
        End If
        Return _Node
    End Function

    Private Sub trvCategories_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategories.AfterSelect
        If Not trvCategories.SelectedNode Is Nothing Then
            Dim _Category As String = trvCategories.SelectedNode.Text.Trim.Replace("'", "''")
            FillTestGroups(_Category)
        End If
    End Sub

    Private Sub tls_strip_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_strip.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Category"
                Category()
                ''commented by Sandip Darade 20090429
                ''to remove Specimen 
                'Case "Specimen"
                '    Specimen()
            Case "NewGroup"
                NewGroup()
            Case "NewTest"
                NewTest()
            Case "Modify"
                Modify()
            Case "Delete"
                Delete()
            Case "Refresh"
                Refresh()
            Case "Close"
                Close_Form()
        End Select
    End Sub

    Public Sub Category()
        Dim _PreviousCategory As String = ""
        Dim _SelectedIndex As Integer = 0

        If Not trvCategories.SelectedNode Is Nothing Then
            _PreviousCategory = trvCategories.SelectedNode.Text.Trim.Replace("'", "''")
        End If

        'Dim oCategoryDialog As New gloStream.LabModule.Category.Dialog.CategoryDialog
        'oCategoryDialog.ShowInTaskbar = False ''Sandip Darade 20090309 
        'oCategoryDialog.ShowDialog(Me)
        'oCategoryDialog.Dispose()
        Dim ofrm_LM_CategorySetup As New frm_LM_CategorySetup
        ofrm_LM_CategorySetup.ShowInTaskbar = False
        ofrm_LM_CategorySetup.ShowDialog(IIf(IsNothing(ofrm_LM_CategorySetup.Parent), Me, ofrm_LM_CategorySetup.Parent))
        ofrm_LM_CategorySetup.Dispose()

        Fill_Categories()

        For i As Int16 = 0 To trvCategories.GetNodeCount(False) - 1
            If trvCategories.Nodes(i).Text = _PreviousCategory Then
                _SelectedIndex = i
                Exit For
            End If
        Next

        If Not trvCategories.GetNodeCount(False) = 0 Then
            trvCategories.SelectedNode = trvCategories.Nodes(_SelectedIndex)
        End If
    End Sub

    Public Sub Specimen()
        'Dim oSpecimenDialog As New gloStream.LabModule.Specimen.Dialog.SpecimenDialog
        'oSpecimenDialog.ShowInTaskbar = False ''Sandip Darade 20090309 
        'oSpecimenDialog.ShowDialog(Me)
        'oSpecimenDialog.Dispose()

        Dim ofrm_LM_SpecimenSetup As New frm_LM_SpecimenSetup
        ofrm_LM_SpecimenSetup.ShowInTaskbar = False
        ofrm_LM_SpecimenSetup.ShowDialog(IIf(IsNothing(ofrm_LM_SpecimenSetup.Parent), Me, ofrm_LM_SpecimenSetup.Parent))
        ofrm_LM_SpecimenSetup.Dispose()

    End Sub

    Public Sub NewGroup()
        If Not trvCategories.SelectedNode Is Nothing Then
            'Dim oTestGroup As New Test.Dialog.TestSetupDialog

            'oTestGroup._EditTestGroupID = 0
            'oTestGroup._EditTestGroupName = ""

            'Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
            'oTestGroup._TestGroupFlag = oSupporting.GetTestGroupFlagName("G")
            'oSupporting = Nothing

            'oTestGroup._Category = trvCategories.SelectedNode.Text
            'oTestGroup._SaveFlag = True

            'oTestGroup.ShowInTaskbar = False ''Sandip Darade 20090309 
            'oTestGroup.ShowDialog(Me)
            'oTestGroup = Nothing

            ''
            Dim ofrmTestGroup As New frm_LM_TestSetuptDialog
            ofrmTestGroup._EditTestGroupID = 0
            ofrmTestGroup._EditTestGroupName = ""

            Dim oSupporting As New Test.Supporting.Supporting
            ofrmTestGroup._TestGroupFlag = oSupporting.GetTestGroupFlagName("G")
            oSupporting = Nothing

            ofrmTestGroup._Category = trvCategories.SelectedNode.Text
            ofrmTestGroup._SaveFlag = True
            ofrmTestGroup.Text = "Group"
            ofrmTestGroup.ShowInTaskbar = False ''Sandip Darade 20090309
            '' ofrmTestGroup.Icon = My.Resources.New_Groups1 ''Provided Icon 
            ofrmTestGroup.ShowDialog(IIf(IsNothing(ofrmTestGroup.Parent), Me, ofrmTestGroup.Parent))
            ofrmTestGroup.Dispose()
            ofrmTestGroup = Nothing

            FillTestGroups(trvCategories.SelectedNode.Text.Trim.Replace("'", "''"))
        Else
            MessageBox.Show("Please select category to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End If
    End Sub

    Public Sub NewTest()
        If Not trvCategories.SelectedNode Is Nothing Then
            'Dim oTestGroup As New Test.Dialog.TestSetupDialog

            'oTestGroup._EditTestGroupID = 0
            'oTestGroup._EditTestGroupName = ""

            'Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
            'oTestGroup._TestGroupFlag = oSupporting.GetTestGroupFlagName("T")
            'oSupporting = Nothing

            'oTestGroup._Category = trvCategories.SelectedNode.Text.Trim
            'oTestGroup._SaveFlag = True

            'oTestGroup.ShowInTaskbar = False ''Sandip Darade 20090309 
            'oTestGroup.ShowDialog(Me)
            'oTestGroup = Nothing
            '
            Dim ofrmTestGroup As New frm_LM_TestSetuptDialog
            ofrmTestGroup._EditTestGroupID = 0
            ofrmTestGroup._EditTestGroupName = ""

            Dim oSupporting As New Test.Supporting.Supporting
            ofrmTestGroup._TestGroupFlag = oSupporting.GetTestGroupFlagName("T")
            oSupporting = Nothing

            ofrmTestGroup._Category = trvCategories.SelectedNode.Text
            ofrmTestGroup._SaveFlag = True

            ofrmTestGroup.ShowInTaskbar = False ''Sandip Darade 20090309 
            ofrmTestGroup.ShowDialog(IIf(IsNothing(ofrmTestGroup.Parent), Me, ofrmTestGroup.Parent))
            ofrmTestGroup.Dispose()
            ofrmTestGroup = Nothing


            FillTestGroups(trvCategories.SelectedNode.Text.Trim.Replace("'", "''"))
        Else
            MessageBox.Show("Please select category to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End If
    End Sub

    Public Sub Modify()
        If Not c1List.Row <= 0 Then
            If Not trvCategories.SelectedNode Is Nothing Then
                Dim oTestGroup As New frm_LM_TestSetuptDialog

                oTestGroup._EditTestGroupID = CLng(Val(c1List.GetData(c1List.Row, COL_ID) & ""))
                oTestGroup._EditTestGroupName = c1List.GetData(c1List.Row, COL_NAME).Trim & ""

                Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
                oTestGroup._TestGroupFlag = oSupporting.GetTestGroupFlagName(c1List.GetData(c1List.Row, COL_TESTGROUPFLAG) & "")
                oSupporting = Nothing

                oTestGroup._Category = trvCategories.SelectedNode.Text.Trim
                oTestGroup._SaveFlag = False

                oTestGroup.ShowInTaskbar = False ''Sandip Darade 20090309 

                oTestGroup.Text = oTestGroup._TestGroupFlag
                oTestGroup.ShowDialog(IIf(IsNothing(oTestGroup.Parent), Me, oTestGroup.Parent))
                oTestGroup.Dispose()
                oTestGroup = Nothing

                FillTestGroups(trvCategories.SelectedNode.Text.Trim.Replace("'", "''"))
            Else
                MessageBox.Show("Please select category to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
        Else
            MessageBox.Show("Please select test or group to modify", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End If

    End Sub

    Public Sub Delete()
        If Not c1List.Row <= 0 Then
            If Not trvCategories.SelectedNode Is Nothing Then
                Dim _DeleteName As String, _Category As String
                Dim _TestId As Int16 = 0
                _DeleteName = c1List.GetData(c1List.Row, COL_NAME).Trim.Replace("'", "''") & ""
                _Category = trvCategories.SelectedNode.Text.Trim.Replace("'", "''")
                _TestId = c1List.GetData(c1List.Row, COL_ID)
                If Not (_DeleteName.Trim = "" AndAlso _Category.Trim = "") Then
                    If MessageBox.Show("Are you sure you want to delete this test or group?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        Dim oMaintainTest As New gloStream.LabModule.Test.MaintainTest
                        If oMaintainTest.IsDelete(_DeleteName, _Category, _TestId) = True Then
                            If oMaintainTest.Delete(_DeleteName, _Category, _TestId) = False Then
                                If oMaintainTest.ErrorMessage <> "" Then
                                    MessageBox.Show(oMaintainTest.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
                                End If
                            Else
                                ''FillTestGroups(trvCategories.SelectedNode.Text.Trim.Replace("'", "''"))
                            End If
                        Else
                            If oMaintainTest.ErrorMessage <> "" Then
                                MessageBox.Show(oMaintainTest.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
                            Else

                            End If
                        End If
                        oMaintainTest = Nothing
                    End If
                Else
                    MessageBox.Show("Test or Group not found, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show("Please select category to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
        Else
            'MessageBox.Show("Please select test or group to modify", gstrMessageBoxCaption, MessageBoxButtons.OK)
            MessageBox.Show("There is no Test or Group associated with this category to delete.", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End If
        FillTestGroups(trvCategories.SelectedNode.Text.Trim.Replace("'", "''"))
    End Sub

    Public Sub Refresh()

        trvCategories.Focus()

        If Not trvCategories.SelectedNode Is Nothing Then
            Dim _Category As String = trvCategories.SelectedNode.Text.Trim.Replace("'", "''")
            FillTestGroups(_Category)
        End If
    End Sub

    Public Sub Close_Form()
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    
    Private Sub c1List_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1List.MouseDoubleClick
        ''20090909
        ''Mayuri
        Dim ptPoint As Point = New Point(e.X, e.Y)
        'To find Cell in c1List which user want to modify
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1List.HitTest(ptPoint)
        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
            Modify()
        End If
        ''end code
    End Sub


    Private Sub frm_LM_TestSetup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        If Not trvCategories.GetNodeCount(False) = 0 Then
            trvCategories.SelectedNode = trvCategories.Nodes(0)
            Dim _Category As String = trvCategories.SelectedNode.Text.Trim.Replace("'", "''")
            FillTestGroups(_Category)
        Else
            FillTestGroups("")
        End If
    End Sub
End Class