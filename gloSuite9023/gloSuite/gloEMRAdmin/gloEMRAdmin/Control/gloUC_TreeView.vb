Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary

Public Class gloUC_TreeView
    '' to Test Undo Chanekin
#Region " Enumaration "
    Enum enumDisplayType
        Code = 1
        Descripation = 2
        Code_Description = 3
        Code_Description_Unit = 4
        Code_Description_DrugForm = 5
    End Enum

    Enum enumSortType
        ByCode = 1
        ByDescription = 2
        ByUnit = 3
        ByDrugForm = 4
    End Enum

    Enum enumSearchType
        Simple = 1
        Instring = 2
    End Enum
#End Region

#Region " Shared Variables"
    'Use Shared Variable B'coz we want to acess this variable in gloEMR project
    Public Shared blnResetSearch As Boolean = False
    Private IsDrugSearch As Boolean = False
#End Region

#Region " Private Variables "
    Private _DTMaster As DataTable
    Private _ValueMember As String
    Private _CodeMember As String
    Private _DescriptionMember As String
    Private _ICDRevision As String
    Private _SmartTreatmentId As String
    Private _sIndicator As String 'GLO2010-0005444' Indicator

    ''GLO2011-0010684
    ''Variable used to hold the ROS comments 
    Private _sComment As String

    Private _ParentMember As String
    Private _UnitMember As String
    Private _DrugFormMember As String
    Private _RouteMember As String
    Private _FrequencyMember As String
    Private _DurationMember As String
    Private _NDCCodeMember As String
    Private _DrugQtyQualifierMember As String
    Private _DDIDMember As String
    Private _IsNarcoticsMember As String
    Private _DisplayType As enumDisplayType = enumDisplayType.Descripation
    Private _SortType As enumSortType = enumSortType.ByDescription
    Private _SearchType As enumSearchType = enumSearchType.Instring
    Private _MaxNode As Int32 = 1000
    Private _SearchBox As Boolean = True
    Private _ParentImageIndex As Integer
    Private _SelectedParentImageIndex As Integer
    Private _ImageObject As String
    Private _ConceptID As String
    ''Adde by Mayuri:20120831-History Onset PRD-7010
    Private _CPT As String
    Private _HistoryType As String
    Private _ICD9 As String
    Private _Tag As Object

    Private Const _MessageBoxCaption = "gloEMR"
    Private Const col_DisplayText = "DISPLAY_TEXT"
    Private dvSort As DataView
    Private dtSort As DataTable
    Private oColSelectedNodes As New Collection
    Private arrSelectedNodeIDs As New ArrayList
    Private isTreeLoading As Boolean = False

    ''Sandip Darade  20091014
    Private _IsDrug As Boolean = False
    Private _DrugFlag As Int16 = 16
    Private _IsSystemCategory As String

    'Ashish Tamhane 6-Sep-2013: Added bIsSearchForEducationMapping
    'for enabling search for Drugs_MST INNER JOINED to Education_Medication
    Private bIsSearchForEducationMapping As Boolean = False
    Private nEducationMappingSearchType As Integer = 1 'Default for all search   
    Private bIsDiagnosisSearch As Boolean = False
    Private bISCPTSearch As Boolean = False

#End Region

#Region " Public Properties "
    'Added ColonAsSeparator property for making : (colon)
    'as a separator. Defaults to False.
    Public Property ColonAsSeparator() As Boolean = False

    Public Property EducationMappingSearchType() As Integer
        Get
            Return Me.nEducationMappingSearchType
        End Get
        Set(ByVal value As Integer)
            Me.nEducationMappingSearchType = value
        End Set
    End Property

    Public Property IsSearchForEducationMapping() As Boolean
        Get
            Return Me.bIsSearchForEducationMapping
        End Get
        Set(ByVal value As Boolean)
            Me.bIsSearchForEducationMapping = value
        End Set
    End Property
    Public Property IsDiagnosisSearch() As Boolean
        Get
            Return Me.bIsDiagnosisSearch
        End Get
        Set(ByVal value As Boolean)
            Me.bIsDiagnosisSearch = value
        End Set
    End Property
    Public Property IsCPTSearch() As Boolean
        Get
            Return Me.bISCPTSearch
        End Get
        Set(ByVal value As Boolean)
            Me.bISCPTSearch = value
        End Set
    End Property
    ''' <summary>
    ''' Sets the Data Source to the control.
    ''' </summary>
    Public WriteOnly Property DataSource() As DataTable
        Set(ByVal value As DataTable)
            _DTMaster = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the value member to the control.
    ''' </summary>    
    Public Property ValueMember() As String
        Get
            Return _ValueMember
        End Get
        Set(ByVal value As String)
            _ValueMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the code member to the control.
    ''' </summary>

    Public Property Indicator() As String  ''GLO2010-0005444' sIndicator
        Get
            Return _sIndicator
        End Get
        Set(ByVal value As String)
            _sIndicator = value
        End Set
    End Property

    ''GLO2011-0010684
    '' Property used to hold the ROS Comments 
    Public Property Comment() As String
        Get
            Return _sComment
        End Get
        Set(ByVal value As String)
            _sComment = value
        End Set
    End Property

    Public Property CodeMember() As String
        Get
            Return _CodeMember
        End Get
        Set(ByVal value As String)
            _CodeMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the description member to the control.
    ''' </summary>
    Public Property DescriptionMember() As String
        Get
            Return _DescriptionMember
        End Get
        Set(ByVal value As String)
            _DescriptionMember = value
        End Set
    End Property
    Public Property ICDRevision() As String
        Get
            Return _ICDRevision
        End Get
        Set(ByVal value As String)
            _ICDRevision = value
        End Set
    End Property

    Public Property SmartTreatmentId() As String
        Get
            Return _SmartTreatmentId
        End Get
        Set(ByVal value As String)
            _SmartTreatmentId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the Image member to the control.
    ''' </summary>
    Public Property ImageObject() As String
        Get
            Return _ImageObject
        End Get
        Set(ByVal value As String)
            _ImageObject = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the parent member to the control which will inserted as a parent nodes.
    ''' </summary>
    Public Property ParentMember() As String
        Get
            Return _ParentMember
        End Get
        Set(ByVal value As String)
            _ParentMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the unit member to the control.
    ''' </summary>
    Public Property UnitMember() As String
        Get
            Return _UnitMember
        End Get
        Set(ByVal value As String)
            _UnitMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the drug form member to the control.
    ''' </summary>
    Public Property DrugFormMember() As String
        Get
            Return _DrugFormMember
        End Get
        Set(ByVal value As String)
            _DrugFormMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the route member to the control.
    ''' </summary>
    Public Property RouteMember() As String
        Get
            Return _RouteMember
        End Get
        Set(ByVal value As String)
            _RouteMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the frequency member to the control.
    ''' </summary>
    Public Property FrequencyMember() As String
        Get
            Return _FrequencyMember
        End Get
        Set(ByVal value As String)
            _FrequencyMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the duration member to the control.
    ''' </summary>
    Public Property DurationMember() As String
        Get
            Return _DurationMember
        End Get
        Set(ByVal value As String)
            _DurationMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the NDC code member to the control.
    ''' </summary>
    Public Property NDCCodeMember() As String
        Get
            Return _NDCCodeMember
        End Get
        Set(ByVal value As String)
            _NDCCodeMember = value
        End Set
    End Property

    Public Property ConceptID() As String
        Get
            Return _ConceptID
        End Get
        Set(ByVal value As String)
            _ConceptID = value
        End Set
    End Property
    Public Property ICD9() As String
        Get
            Return _ICD9
        End Get
        Set(ByVal value As String)
            _ICD9 = value
        End Set
    End Property
    Public Property CPT() As String
        Get
            Return _CPT
        End Get
        Set(ByVal value As String)
            _CPT = value
        End Set
    End Property
    Public Property HistoryType() As String
        Get
            Return _HistoryType
        End Get
        Set(ByVal value As String)
            _HistoryType = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets the drug quantity qualifier member to the control.
    ''' </summary>
    Public Property DrugQtyQualifierMember() As String
        Get
            Return _DrugQtyQualifierMember
        End Get
        Set(ByVal value As String)
            _DrugQtyQualifierMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the DDID member to the control.
    ''' </summary>
    Public Property DDIDMember() As String
        Get
            Return _DDIDMember
        End Get
        Set(ByVal value As String)
            _DDIDMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the IsNarcotics member to the control.
    ''' </summary>
    Public Property IsNarcoticsMember() As String
        Get
            Return _IsNarcoticsMember
        End Get
        Set(ByVal value As String)
            _IsNarcoticsMember = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the display type of the control.
    ''' </summary>
    Public Property DisplayType() As enumDisplayType
        Get
            Return _DisplayType
        End Get
        Set(ByVal value As enumDisplayType)
            _DisplayType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sort type of the control.
    ''' </summary>
    Public Property Sort() As enumSortType
        Get
            Return _SortType
        End Get
        Set(ByVal value As enumSortType)
            _SortType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the search type of the control.
    ''' </summary>
    Public Property Search() As enumSearchType
        Get
            Return _SearchType
        End Get
        Set(ByVal value As enumSearchType)
            _SearchType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indication number of nodes to be displayed on gloUC_TreeView control.
    ''' </summary>
    Public Property MaximumNodes() As Int32
        Get
            Return _MaxNode
        End Get
        Set(ByVal value As Int32)
            _MaxNode = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the gloUC_TreeView control.
    ''' </summary>
    Public Property CheckBoxes() As Boolean
        Get
            Return trvMain.CheckBoxes
        End Get
        Set(ByVal value As Boolean)
            trvMain.CheckBoxes = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the tree node that is currently selected in the gloUC_TreeView control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedNode() As TreeNode
        Get
            Return trvMain.SelectedNode
        End Get
        Set(ByVal value As TreeNode)
            trvMain.SelectedNode = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the collection of checked nodes in the gloUC_TreeView control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedNodes() As Collection
        Get
            Return oColSelectedNodes
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indication whether seach box is displayed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SearchBox() As Boolean
        Get
            Return _SearchBox
        End Get
        Set(ByVal value As Boolean)
            pnlSearch.Visible = value
            _SearchBox = value
        End Set
    End Property

    '''' <summary>
    '''' Property to Get or Set the value in Search box of control
    '''' </summary>
    '''' <remarks></remarks>
    Private _SearchText As String
    Public Property SearchText() As String
        Get
            Return _SearchText
        End Get
        Set(ByVal value As String)
            _SearchText = value
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets the System.Windows.Forms.ImageList that contains the System.Drawing.Image objects used by the tree nodes.
    ''' </summary>
    ''' <returns>
    ''' The System.Windows.Forms.ImageList that contains the System.Drawing.Image objects used by the tree nodes. The default value is null.
    ''' </returns>
    Public Property ImageList() As ImageList
        Get
            Return trvMain.ImageList

        End Get
        Set(ByVal value As ImageList)
            trvMain.ImageList = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the image-list index value of the default image that is displayed by the tree nodes.
    ''' </summary>
    ''' <returns>
    ''' A zero-based index that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList. The default is zero.
    ''' </returns>
    Public Property ImageIndex() As Integer
        Get
            Return trvMain.ImageIndex
        End Get
        Set(ByVal value As Integer)
            trvMain.ImageIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the image list index value of the image that is displayed when a tree node is selected.
    ''' </summary>
    ''' <returns>
    ''' A zero-based index value that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList.
    ''' </returns>
    Public Property SelectedImageIndex() As Integer
        Get
            Return trvMain.SelectedImageIndex
        End Get
        Set(ByVal value As Integer)
            trvMain.SelectedImageIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the image-list index value of the default image that is displayed by the parent tree nodes.
    ''' </summary>
    ''' <returns>
    ''' A zero-based index that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList. The default is zero.
    ''' </returns>
    Public Property ParentImageIndex() As Integer
        Get
            Return _ParentImageIndex
        End Get
        Set(ByVal value As Integer)
            _ParentImageIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the image list index value of the image that is displayed when a parent tree node is selected.
    ''' </summary>
    ''' <returns>
    ''' A zero-based index value that represents the position of an System.Drawing.Image in an System.Windows.Forms.ImageList.
    ''' </returns>
    Public Property SelectedParentImageIndex() As Integer
        Get
            Return _SelectedParentImageIndex
        End Get
        Set(ByVal value As Integer)
            _SelectedParentImageIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the collection of tree nodes that are assigned to the gloUC_TreeView control.
    ''' </summary>
    Public ReadOnly Property Nodes() As TreeNodeCollection
        Get
            Return trvMain.Nodes
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the arraylist of node IDs which will be selected nodes on the gloUC_TreeView control.
    ''' </summary>
    Public Property SelectedNodeIDs() As ArrayList
        Get
            Return arrSelectedNodeIDs
        End Get
        Set(ByVal value As ArrayList)
            If IsNothing(value) Then
                arrSelectedNodeIDs.Clear()
            Else
                arrSelectedNodeIDs = value
            End If
        End Set
    End Property

    Public Property Tag() As String
        Get
            Return _Tag
        End Get
        Set(ByVal value As String)
            _Tag = value
        End Set
    End Property
    ''Sandip Darade  20091014
    Public Property IsDrug() As Boolean
        Get
            Return _IsDrug
        End Get
        Set(ByVal value As Boolean)
            _IsDrug = value
        End Set
    End Property

    Public Property DrugFlag() As Int16
        Get
            Return _DrugFlag
        End Get
        Set(ByVal value As Int16)
            _DrugFlag = value
        End Set
    End Property

    'Sanjog
    Public Property IsSystemCategory() As String
        Get
            Return _IsSystemCategory
        End Get
        Set(ByVal value As String)
            _IsSystemCategory = value
        End Set
    End Property
    'Sanjog
#End Region

#Region " Control Events "
    Public Event NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    Public Shadows Event KeyPress As EventHandler(Of KeyPressEventArgs)
    Public Event AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    Public Event MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event NodeAdded(ByVal ChildNode As myTreeNode)
    Public Event SearchFired()


    Private Sub trvMain_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvMain.AfterCheck
        If isTreeLoading = False Then
            Dim iChildCheckedNodeCount As Int32 = 0
            If e.Node.Checked Then
                '' NODE CHECKED ''
                If _ParentMember <> "" And e.Node.Level = 0 Then
                    '' PARENT NODE CHECKED ''
                    For Each oNode As TreeNode In e.Node.Nodes
                        oNode.Checked = True
                    Next
                Else
                    '' CHILD NODE CHECKED ''
                    Dim _ID As Int64 = CType(e.Node, myTreeNode).ID
                    If arrSelectedNodeIDs.Contains(_ID) = False Then
                        oColSelectedNodes.Add(e.Node)
                        arrSelectedNodeIDs.Add(_ID)
                    End If
                End If

            Else
                '' NODE UNCHECKED ''
                If _ParentMember <> "" And e.Node.Level = 0 Then
                    '' PARENT NODE UNCHECKED ''
                    For Each oNode As TreeNode In e.Node.Nodes
                        oNode.Checked = False
                    Next
                Else
                    '' CHILD NODE UNCHECKED ''
                    For i As Integer = 1 To oColSelectedNodes.Count
                        Dim _ID As Int64 = CType(e.Node, myTreeNode).ID
                        If CType(oColSelectedNodes(i), myTreeNode).ID = _ID Then
                            oColSelectedNodes.Remove(i)
                            arrSelectedNodeIDs.Remove(_ID)
                            Exit Sub
                        End If
                    Next
                End If
            End If

            'Dim iChildNodeChekedCount As Int32 = 0

            'Dim oParent As TreeNode = e.Node.Parent

            'For Each oNode As TreeNode In oParent.Nodes
            '    If oNode.Checked Then
            '        iChildNodeChekedCount += 1
            '    End If
            'Next

            'If oParent.Nodes.Count = iChildNodeChekedCount Then
            '    oParent.Checked = True
            'Else
            '    oParent.Checked = False
            'End If
        End If
    End Sub

    Private Sub trvMain_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvMain.AfterSelect
        Try
            trvMain.SelectedNode = e.Node
            RaiseEvent AfterSelect(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvMain.KeyPress
        If e.KeyChar = ControlChars.Back Then
            txtsearch.Focus()
        ElseIf e.KeyChar = Chr(13) Then
            RaiseEvent KeyPress(sender, e)
            ' txtsearch.ResetText()
            txtsearch.Focus()
        End If
    End Sub

    Private Sub trvMain_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvMain.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            trvMain.SelectedNode = e.Node
        End If
    End Sub

    Private Sub trvMain_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvMain.NodeMouseDoubleClick
        Try
            trvMain.SelectedNode = e.Node
            RaiseEvent NodeMouseDoubleClick(sender, e)
            ''Shubhangi
            'txtsearch.ResetText()
            txtsearch.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        'If e.KeyChar = "'" Or e.KeyChar = "[" Or e.KeyChar = "^" Or e.KeyChar = "*" Or e.KeyChar = "%" Then
        If e.KeyChar = "[" Or e.KeyChar = "^" Or e.KeyChar = "*" Or e.KeyChar = "%" Then
            e.Handled = True
        ElseIf e.KeyChar = Chr(13) Then


            ''Added by Mayuri: to set focus on sleected node after entering code
            txtsearch.Tag = "True"
            txtsearch_SearchFired()
            If trvMain.Nodes.Count > 0 Then
                trvMain.Focus()
                If IsNothing(trvMain.SelectedNode) Then
                    trvMain.SelectedNode = trvMain.Nodes(0)
                End If
            End If


        End If
    End Sub

    ''Bug : 00000866. Back key was not working properly in KeyPress so added in KeyUp
    Private Sub txtsearch_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtsearch.KeyUp
        If e.KeyValue = Keys.Back Then
            txtsearch.Tag = "True"
            txtsearch_SearchFired()
        End If
    End Sub

    Private Sub txtsearch_SearchFired() Handles txtsearch.SearchFired

        Try

            ''Sandip darade  2001014
            ''Pull thadrug from database 
            If Me.bIsDiagnosisSearch Or Me.bISCPTSearch Then
                RaiseEvent SearchFired()
            Else
                IsDrugSearch = False
                If (IsDrug = True) Then
                    If Not IsNothing(_DTMaster) Then
                        _DTMaster = Nothing
                    End If
                    'If Me.bIsSearchForEducationMapping Then
                    '    _DTMaster = FillEducationMappingDrugs(Me.nEducationMappingSearchType)
                    'Else
                    '    _DTMaster = FillDrugs(txtsearch.Text)
                    'End If

                    IsDrugSearch = True
                    FillTreeView()
                    RaiseEvent SearchFired()
                    Exit Sub
                End If
                If IsNothing(_DTMaster) = False Then
                    If _DTMaster.Rows.Count > 0 Then
                        FillTree(txtsearch.Text.Trim())
                    End If
                End If
                RaiseEvent SearchFired()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged

    End Sub

    Private Sub trvMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvMain.MouseDown
        Try
            trvMain.SelectedNode = trvMain.GetNodeAt(e.X, e.Y)
            RaiseEvent MouseDown(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Public Methods "
    ''' <summary>
    ''' Fills the gloUC_TreeView according to bound data table and its properties.
    ''' </summary>
    Public Sub FillTreeView()
        'If gblngloTreeViewLoading Then
        '    Exit Sub
        'End If

        'gblngloTreeViewLoading = True
        Cursor = Cursors.WaitCursor
        Try
            If ValidateProperties() = False Then
                Exit Sub
            End If

            oColSelectedNodes.Clear()
            arrSelectedNodeIDs.Clear()

            '' SORTING DATAVIEW
            dvSort = _DTMaster.DefaultView
            Select Case _SortType
                Case enumSortType.ByCode
                    dvSort.Sort = _CodeMember
                Case enumSortType.ByDescription
                    dvSort.Sort = _DescriptionMember
                Case enumSortType.ByUnit
                    dvSort.Sort = _UnitMember
                    '' added on 20091008
                Case enumSortType.ByDrugForm
                    dvSort.Sort = _DrugFormMember
            End Select
            _DTMaster = dvSort.ToTable()
            '' END SORT ''

            Application.DoEvents() '' DO NOT MOVE THIS LINE OF CODE, IT MAY LEAD TO EXCEPTION WHILE MULTITHREAD ''

            '' CREATE NEW COLUMN OF DISPLAY_TEXT

            'Dim oColumn As New DataColumn(col_DisplayText)
            '_DTMaster.Columns.Add(oColumn)
            If _DTMaster.Columns.Contains(col_DisplayText) = False Then
                Dim oColumn As New DataColumn(col_DisplayText)
                _DTMaster.Columns.Add(oColumn)
            End If

            Dim _loopCount As Int64 = _DTMaster.Rows.Count - 1
            If _DTMaster.Rows.Count > _MaxNode Then
                _loopCount = _MaxNode - 1
            Else
                _loopCount = _DTMaster.Rows.Count - 1
            End If

            If (IsDrug = True) Then
                _loopCount = _DTMaster.Rows.Count - 1
            End If
            '' FILL NEW COLUMN RECORDS ''

            _DTMaster.BeginLoadData()
            Select Case _DisplayType
                Case enumDisplayType.Code
                    For index As Int64 = 0 To _loopCount
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember)
                    Next

                Case enumDisplayType.Descripation
                    For index As Int64 = 0 To _loopCount
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                    Next

                Case enumDisplayType.Code_Description
                    For index As Int64 = 0 To _loopCount
                        ' _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember)" - " & _DTMaster.Rows(index)(_DescriptionMember)

                        ''Sandip Darade 20090623
                        ''check if description is blank
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember).ToString.Trim
                        If (_DTMaster.Rows(index)(_DescriptionMember).ToString.Trim <> "") Then
                            If _DTMaster.Rows(index)(col_DisplayText).ToString.Trim <> "" Then
                                If IsNothing(_DTMaster.Columns("IsDrug")) = False Then
                                    If _DTMaster.Rows(index)("IsDrug") = 1 Then

                                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim & " - " & _DTMaster.Rows(index)(col_DisplayText).ToString.Trim
                                    End If
                                Else
                                    'Ashish Tamhane 9-Sep-2013
                                    'Added functionality to make : (colon) as separator
                                    If Me.ColonAsSeparator Then
                                        _DTMaster.Rows(index)(col_DisplayText) &= " : " & _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                                    Else
                                        _DTMaster.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                                    End If

                                End If

                                ' _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember) & " - " & _DTMaster.Rows(index)(col_DisplayText).ToString.Trim
                                '
                            Else
                                _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                            End If

                        End If

                    Next

                Case enumDisplayType.Code_Description_Unit
                    For index As Int64 = 0 To _loopCount
                        '_DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember) & " - " & _DTMaster.Rows(index)(_DescriptionMember) & " - " & dtSort.Rows(index)(_UnitMember)
                        ''Sandip Darade 20090623
                        ''check if description or unit or both are  blank
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember).ToString.Trim

                        'Additional conditions added to resolve the Problem 00000092
                        '---
                        If (_DTMaster.Rows(index)(_DescriptionMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                            _DTMaster.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                        ElseIf (_DTMaster.Rows(index)(_DescriptionMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                            _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                        End If

                        If (_DTMaster.Rows(index)(_UnitMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                            _DTMaster.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_UnitMember).ToString.Trim
                        ElseIf (_DTMaster.Rows(index)(_UnitMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                            _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_UnitMember).ToString.Trim
                        End If
                        '----
                    Next

                    '' Added on 20091008 
                    '' For Drugs we have to show Drug from alonfg with its name & Sig info
                Case enumDisplayType.Code_Description_DrugForm
                    For index As Int64 = 0 To _loopCount
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember).ToString.Trim
                        'Additional conditions added to resolve the Problem 00000092
                        '---
                        If (_DTMaster.Rows(index)(_DescriptionMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                            _DTMaster.Rows(index)(col_DisplayText) &= " " & _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                        ElseIf (_DTMaster.Rows(index)(_DescriptionMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                            _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                        End If

                        If (_DTMaster.Rows(index)(_DrugFormMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                            _DTMaster.Rows(index)(col_DisplayText) &= " " & _DTMaster.Rows(index)(_DrugFormMember).ToString.Trim
                        ElseIf (_DTMaster.Rows(index)(_DrugFormMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                            _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DrugFormMember).ToString.Trim
                        End If
                        '---
                    Next
                Case Else
                    For index As Int64 = 0 To _loopCount
                        dtSort.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember).ToString.Trim
                    Next

            End Select
            _DTMaster.EndLoadData()
            Application.DoEvents()

            dvSort = _DTMaster.DefaultView

            'If blnResetSearch = False Then
            '    txtsearch.Text = _SearchText
            'End If
            'Check variable of reset text 
            ''Added following condition -Do not clear search after entering search text
            If Me.bIsDiagnosisSearch Or Me.bISCPTSearch Then
            Else
                If (IsDrugSearch = False) Then
                    If blnResetSearch = True Then
                        txtsearch.Clear()
                    End If
                End If
            End If



            FillTree(txtsearch.Text.Trim)
            If (IsDrug <> True) Then
                If _DTMaster.Rows.Count > _MaxNode Then
                    Application.DoEvents()
                    AttachDisplayTextToDataTable(_loopCount - 1)
                    Application.DoEvents()
                End If
            End If
            txtsearch.Focus()
            txtsearch.DeselectAll()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'gblngloTreeViewLoading = False
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' Forces the control to invalidate its client area and immediately redraw itself and any child controls.
    ''' </summary>
    Public Overrides Sub Refresh()
        txtsearch.Clear()
        oColSelectedNodes.Clear()
        arrSelectedNodeIDs.Clear()
        FillTree(txtsearch.Text.Trim)
    End Sub

    ''' <summary>
    ''' Check all visible nodes of gloUC_TreeView control.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CheckAllNodes()
        Try
            If trvMain.CheckBoxes Then
                oColSelectedNodes.Clear()
                arrSelectedNodeIDs.Clear()

                '' CHECK ALL VISIBLE NODES ''
                For Each _Node As TreeNode In trvMain.Nodes
                    _Node.Checked = True
                Next

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Uncheck all the nodes of gloUC_TreeView control.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UncheckAllNodes()
        If trvMain.CheckBoxes Then
            isTreeLoading = True
            Try
                For Each oNode As TreeNode In trvMain.Nodes
                    oNode.Checked = False
                Next

                '' IF LEVEL ONE TREE ''
                If _ParentMember <> "" Then
                    For Each oParentNode As TreeNode In trvMain.Nodes
                        For Each oChildNode As TreeNode In oParentNode.Nodes
                            oChildNode.Checked = False
                        Next
                    Next
                End If

                oColSelectedNodes.Clear()
                arrSelectedNodeIDs.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            isTreeLoading = False
        End If
    End Sub

    ''' <summary>
    ''' Sets input focus to the tree view of gloUC_TreeView control.
    ''' </summary>
    Public Sub FocusTreeView()
        trvMain.Focus()
    End Sub

    ''' <summary>
    ''' Sets input focus to the search box of gloUC_TreeView control.
    ''' </summary>
    Public Sub FocusSearchBox()
        txtsearch.Focus()
    End Sub

    ''' <summary>
    ''' Clear all the values and members of gloUC_TreeView control and removes all nodes from tree.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        _DTMaster = Nothing
        _ValueMember = Nothing
        _DescriptionMember = Nothing
        _CodeMember = Nothing
        _DrugFormMember = Nothing
        _RouteMember = Nothing
        _NDCCodeMember = Nothing
        _IsNarcoticsMember = Nothing
        _FrequencyMember = Nothing
        _DurationMember = Nothing
        _DrugQtyQualifierMember = Nothing
        _DDIDMember = Nothing
        _DisplayType = Nothing
        _UnitMember = Nothing
        _ParentMember = Nothing
        _Tag = Nothing
        _IsDrug = False
        _DrugFlag = 0
        _ImageObject = Nothing

        'txtsearch.Clear()
        If Me.bIsDiagnosisSearch Or Me.bISCPTSearch Then
        Else
            If blnResetSearch = False Then

            Else
                txtsearch.Clear()

            End If
        End If


        trvMain.Nodes.Clear()
        oColSelectedNodes.Clear()
        arrSelectedNodeIDs.Clear()
    End Sub
    '' solving sales force case- 0009512
    Public Sub SetFocus()
        txtsearch.Focus()
        txtsearch.Select()
    End Sub
    '' end

    ''' <summary>
    ''' Collapses all the tree nodes.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CollapseAll()
        trvMain.CollapseAll()
        If trvMain.Nodes.Count > 0 Then
            trvMain.SelectedNode = trvMain.Nodes(0)
        End If
    End Sub

    ''' <summary>
    ''' Expands all the tree nodes.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExpandAll()
        trvMain.BeginUpdate()
        Try
            trvMain.ExpandAll()
            If trvMain.Nodes.Count > 0 Then
                trvMain.SelectedNode = trvMain.Nodes(0)
            End If
        Catch

        Finally
            trvMain.EndUpdate()
        End Try

    End Sub

#End Region

#Region " Private Methods "
    Private Function ValidateProperties() As Boolean

        Dim _ErrorString As String = ""

        '' VALIDATION FOR DATATABLE
        If IsNothing(_DTMaster) Then
            _ErrorString = "DataSource cannot be empty." & vbLf
        Else
            'Commented by Mayuri:20091005
            'To display treeview as blank if no Record associated with particular specilaity in frmICD9CPTGallery
            'If _DTMaster.Rows.Count = 0 Then
            '    Return False
            'End If
        End If

        '' VALIDATION FOR VALUE MEMBER '' 
        If _ValueMember = "" Then
            _ErrorString = _ErrorString & "ValueMember Property is not set" & vbLf
        Else
            If _DTMaster.Columns.Contains(_ValueMember) Then
                If _DTMaster.Columns(_ValueMember).DataType Is Type.GetType("System.String") Then
                    _ErrorString = _ErrorString & "Column '" & _ValueMember & "' does not contain numeric values at ValueMember property." & vbLf
                End If
            Else
                _ErrorString = _ErrorString & "Column '" & _ValueMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATION FOR DDID MEMBER ''
        If _DDIDMember <> "" Then
            If _DTMaster.Columns.Contains(_DDIDMember) Then
                If _DTMaster.Columns(_DDIDMember).DataType Is Type.GetType("System.String") Then
                    _ErrorString = _ErrorString & "Column '" & _DDIDMember & "' does not contain numeric values at DDIDMember property." & vbLf
                End If
            Else
                _ErrorString = _ErrorString & "Column '" & _DDIDMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATION FOR ISNARCOTICS MEMBER ''
        If _IsNarcoticsMember <> "" Then
            If _DTMaster.Columns.Contains(_IsNarcoticsMember) Then
                If _DTMaster.Columns(_IsNarcoticsMember).DataType Is Type.GetType("System.String") Then
                    _ErrorString = _ErrorString & "Column '" & _IsNarcoticsMember & "' does not contain numeric values at IsNarcoticsMember property." & vbLf
                End If
            Else
                _ErrorString = _ErrorString & "Column '" & _IsNarcoticsMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR CODE MEMBER ''
        If _CodeMember <> "" Then
            If _DTMaster.Columns.Contains(_CodeMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _CodeMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR DESCRIPTION MEMBER ''
        If _DescriptionMember <> "" Then
            If _DTMaster.Columns.Contains(_DescriptionMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _DescriptionMember & "' does not belong to table" & vbLf
            End If
        End If

        ' ''GLO2010-0005444' VALIDATE FOR INDICATOR MEMBER ''
        If _sIndicator <> "" Then
            If _DTMaster.Columns.Contains(_sIndicator) = False Then
                _ErrorString = _ErrorString & "Column '" & _sIndicator & "' does not belong to table" & vbLf
            End If
        End If

        '' GLO2011-0010684
        '' Check for the Comments Field is available in the DataTable (_DTMaster)
        If _sComment <> "" Then
            If _DTMaster.Columns.Contains(_sComment) = False Then
                _ErrorString = _ErrorString & "Column '" & _sComment & "' does not belong to table" & vbLf
            End If
        End If


        '' VALIDATE FOR PARENT MEMBER ''
        If _ParentMember <> "" Then
            If _DTMaster.Columns.Contains(_ParentMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _ParentMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR UNIT MEMBER ''
        If _UnitMember <> "" Then
            If _DTMaster.Columns.Contains(_UnitMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _UnitMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR DRUG FORM MEMBER ''
        If _DrugFormMember <> "" Then
            If _DTMaster.Columns.Contains(_DrugFormMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _DrugFormMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR ROUTE MEMBER ''
        If _RouteMember <> "" Then
            If _DTMaster.Columns.Contains(_RouteMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _RouteMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR FRQUENCY MEMBER ''
        If _FrequencyMember <> "" Then
            If _DTMaster.Columns.Contains(_FrequencyMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _FrequencyMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR DURATION MEMBER ''
        If _DurationMember <> "" Then
            If _DTMaster.Columns.Contains(_DurationMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _DurationMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR NDC CODE MEMBER ''
        If _NDCCodeMember <> "" Then
            If _DTMaster.Columns.Contains(_NDCCodeMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _NDCCodeMember & "' does not belong to table" & vbLf
            End If
        End If

        '' VALIDATE FOR DRUG QUANTITY QUALIFIER MEMBER ''
        If _DrugQtyQualifierMember <> "" Then
            If _DTMaster.Columns.Contains(_DrugQtyQualifierMember) = False Then
                _ErrorString = _ErrorString & "Column '" & _DrugQtyQualifierMember & "' does not belong to table" & vbLf
            End If
        End If


        ''VALIDATION FOR DISPLAY MEMBER REQUIRMENT
        Select Case _DisplayType
            Case enumDisplayType.Code
                If _CodeMember = "" Then
                    _ErrorString = _ErrorString & "CodeMember Property is not set" & vbLf
                End If
            Case enumDisplayType.Descripation
                If _DescriptionMember = "" Then
                    _ErrorString = _ErrorString & "DescriptionMember Property is not set" & vbLf
                End If
            Case enumDisplayType.Code_Description
                If _CodeMember = "" Then
                    _ErrorString = _ErrorString & "CodeMember Property is not set" & vbLf
                ElseIf _DescriptionMember = "" Then
                    _ErrorString = _ErrorString & "DescriptionMember Property is not set" & vbLf
                End If
            Case enumDisplayType.Code_Description_Unit
                If _CodeMember = "" Then
                    _ErrorString = _ErrorString & "CodeMember Property is not set" & vbLf
                ElseIf _DescriptionMember = "" Then
                    _ErrorString = _ErrorString & "DescriptionMember Property is not set" & vbLf
                ElseIf _UnitMember = "" Then
                    _ErrorString = _ErrorString & "UnitMember Property is not set" & vbLf
                End If
        End Select

        '' VALIDATION FOR SORT TYPE
        Select Case _SortType
            Case enumSortType.ByCode
                If _CodeMember = "" Then
                    _ErrorString = _ErrorString & "CodeMember Property is not set" & vbLf
                End If
            Case enumSortType.ByDescription
                If _DescriptionMember = "" Then
                    _ErrorString = _ErrorString & "DescriptionMember Property is not set" & vbLf
                End If
            Case enumSortType.ByUnit
                If _UnitMember = "" Then
                    _ErrorString = _ErrorString & "UnitMember Property is not set" & vbLf
                End If
        End Select

        '' VALIDATION FAILED ''
        If _ErrorString.Trim <> "" Then
            Dim oEX As New Exception(_ErrorString)
            Throw oEX
            Return False
        End If

        '' SUCCESSFULL VALIDATIONS ''
        Return True

    End Function

    Public Sub FillTree(Optional ByVal searchText As String = "")
        Try

            If searchText <> "" Then
                ''Sandip Darade 20090607
                ''Replace special operators to avoid errors
                searchText = searchText.Replace("'", "''")
                searchText = searchText.Replace("[", "") & ""
                searchText = ReplaceSpecialCharacters(searchText)
                If (dvSort.Table.Columns.Contains(col_DisplayText)) Then
                    If _SearchType = enumSearchType.Simple Then
                        dvSort.RowFilter = col_DisplayText & " LIKE '" & searchText.Trim() & "%'"
                    ElseIf _SearchType = enumSearchType.Instring Then
                        dvSort.RowFilter = col_DisplayText & " LIKE '%" & searchText.Trim() & "%'"
                    End If
                Else
                    dvSort.RowFilter = Nothing
                End If

            Else
                dvSort.RowFilter = Nothing
            End If

            '' GET SORTED / FILTERED DATAVIEW
            dtSort = dvSort.ToTable

            '' FILLING TREE VIEW 
            trvMain.BeginUpdate()
            'code line added by dipak 20091005 Scrollable =false before update

            'trvMain.Scrollable = False '' Line commented by Sandip Darade  20091015 as it was taking time 

            trvMain.Nodes.Clear()

            If _ParentMember = "" Then
                FillLevelZeroTree()
            Else
                FillLevelOneTree()
            End If

            CheckSelectedNodes()

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        Finally
            'code added by dipak 20091005 Scrollable =true after update
            trvMain.Scrollable = True
            trvMain.EndUpdate()
        End Try

    End Sub

    Private Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
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
            MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub FillLevelZeroTree()
        Dim oNode As myTreeNode
        For index As Integer = 0 To dtSort.Rows.Count - 1

            '' MAXIMUM NODES VALIDATION ''
            If index >= _MaxNode Then
                Exit For
            End If

            oNode = New myTreeNode

            '' BIND ID '' 
            If _ValueMember <> "" Then
                oNode.ID = CType(dtSort.Rows(index)(_ValueMember), Int64)
            End If


            '' BIND Tag '' 
            If _Tag <> "" Then
                oNode.Tag = CType(dtSort.Rows(index)(_Tag), Object)
            End If
            If _ICDRevision <> "" Then
                oNode.ICDRevision = CType(dtSort.Rows(index)(_ICDRevision), Object)
            End If
            '' BIND Tag '' 
            If _ConceptID <> "" Then
                oNode.ConceptID = CType(dtSort.Rows(index)(_ConceptID), Object)
            End If

            If _ICD9 <> "" Then
                oNode.ICD9 = CType(dtSort.Rows(index)(_ICD9), Object)
            End If
            If _CPT <> "" Then
                oNode.CPT = CType(dtSort.Rows(index)(_CPT), Object)
            End If
            If _HistoryType <> "" Then
                oNode.HistoryType = CType(dtSort.Rows(index)(_HistoryType), Object)
            End If
            '' BIND CODE '' 
            If _CodeMember <> "" Then
                oNode.Code = dtSort.Rows(index)(_CodeMember)
            End If

            '' BIND DESCRIPTION '' 
            If _DescriptionMember <> "" Then
                oNode.Description = dtSort.Rows(index)(_DescriptionMember)
            End If

            'GLO2010-0005444' BIND Indicator '' 
            If _sIndicator <> "" Then
                oNode.Indicator = dtSort.Rows(index)(_sIndicator)
            End If

            '' GLO2011-0010684
            '' Bind the Comments field to the Comment node 
            If _sComment <> "" Then
                oNode.Comments = dtSort.Rows(index)(_sComment)
            End If

            '' BIND UNIT ''
            If _UnitMember <> "" Then
                oNode.Unit = dtSort.Rows(index)(_UnitMember)
            End If

            '' BIND DRUG FORM ''
            If _DrugFormMember <> "" Then
                oNode.DrugForm = dtSort.Rows(index)(_DrugFormMember)
            End If

            '' BIND ROUTE ''
            If _RouteMember <> "" Then
                oNode.Route = dtSort.Rows(index)(_RouteMember)
            End If

            '' BIND FREQUENCY ''
            If _FrequencyMember <> "" Then
                oNode.Frequency = dtSort.Rows(index)(_FrequencyMember)
            End If

            '' BIND DURATION ''
            If _DurationMember <> "" Then
                oNode.Duration = dtSort.Rows(index)(_DurationMember)
            End If

            '' BIND NDC CODE ''
            If _NDCCodeMember <> "" Then
                oNode.NDCCode = dtSort.Rows(index)(_NDCCodeMember)
            End If

            '' BIND DRUG QUANTITY QUALIFIER ''
            If _DrugQtyQualifierMember <> "" Then
                oNode.DrugQtyQualifier = dtSort.Rows(index)(_DrugQtyQualifierMember)
            End If

            '' BIND DDID ''
            If _DDIDMember <> "" Then
                oNode.DDID = CType(dtSort.Rows(index)(_DDIDMember), Int64)
            End If

            '' BIND IS NARCOTICS ''
            If _IsNarcoticsMember <> "" Then
                oNode.IsNarcotics = CType(dtSort.Rows(index)(_IsNarcoticsMember), Int16)
            End If

            '' BIND IMAGE OBJECT TO NODE ''
            If _ImageObject <> "" Then
                oNode.TemplateResult = dtSort.Rows(index)(_ImageObject)
            End If

            If _IsSystemCategory <> "" Then
                oNode.IsSystemCategory = dtSort.Rows(index)(_IsSystemCategory)
            End If

            '' BIND DISPLAY TEXT ''
            If (dtSort.Columns.Contains(col_DisplayText)) Then
                oNode.Text = dtSort.Rows(index)(col_DisplayText)
            Else
                oNode.Text = ""
            End If





            If _SmartTreatmentId <> "" Then
                If Convert.ToBoolean(dtSort.Rows(index)("SmartTreatmentId")) Then
                    oNode.ImageIndex = 1
                    oNode.SelectedImageIndex = 1
                End If
                trvMain.Nodes.Add(oNode)
            Else
                trvMain.Nodes.Add(oNode)  ''added to resolve the smart order ICON issue
                RaiseEvent NodeAdded(oNode)
            End If


            '' ADD NODE ''


            '' To give the extra functionality of identifying the node
            '' Raise the event after node is added to the treeview
            ' RaiseEvent NodeAdded(oNode)
            ''
            oNode = Nothing
        Next
    End Sub

    Private Sub FillLevelOneTree()
        Try

            Dim oParentNode As myTreeNode
            Dim oChildNode As myTreeNode
            Dim arrParentNodes As New ArrayList
            Dim dvTemp As DataView
            Dim dtParent As DataTable
            Dim dtChild As DataTable
            Dim maxNodeCount As Integer = 0

            dvTemp = dtSort.DefaultView '' ALREADY SORTED/FILTERED TABLE
            dtParent = dvSort.ToTable(True, _ParentMember) '' WILL CONTAIN ONLY PARENT NODE (ROWS) ''  '' WILL GIVE DISTINCT RECORDS ''

            If IsNothing(dtParent) = False Then
                For iParent As Integer = 0 To dtParent.Rows.Count - 1
                    oParentNode = New myTreeNode
                    oParentNode.Text = dtParent.Rows(iParent)(_ParentMember)
                    oParentNode.ImageIndex = _ParentImageIndex
                    oParentNode.SelectedImageIndex = _SelectedParentImageIndex

                    '' VALIDATION FOR DUPLICATE PARENT NODES DUE TO CASE SENSITIVE DISTINCT ''
                    If arrParentNodes.Contains(oParentNode.Text.ToLower) = True Then
                        Continue For
                    End If

                    '' FILTER WITH CURRENT PARENT ''
                    dvTemp.RowFilter = Nothing
                    'Apply Replace to Handle Single quote for sql query to apply row filter
                    dvTemp.RowFilter = _ParentMember & " = '" & dtParent.Rows(iParent)(_ParentMember).ToString.Replace("'", "''") & "'"
                    dtChild = dvTemp.ToTable

                    For index As Integer = 0 To dtChild.Rows.Count - 1

                        '' MAXIMUM NODES VALIDATION ''
                        maxNodeCount += 1
                        If maxNodeCount >= _MaxNode Then
                            Exit For
                        End If

                        oChildNode = New myTreeNode

                        '' BIND ID '' 
                        If _ValueMember <> "" Then
                            oChildNode.ID = CType(dtChild.Rows(index)(_ValueMember), Int64)
                        End If

                        '' BIND Tag '' 
                        If _Tag <> "" Then
                            oChildNode.Tag = CType(dtChild.Rows(index)(_Tag), Object)
                        End If
                        If _ICDRevision <> "" Then
                            oChildNode.ICDRevision = CType(dtChild.Rows(index)(_ICDRevision), Object)
                        End If

                        '' BIND Tag '' 
                        If _ConceptID <> "" Then
                            oChildNode.ConceptID = CType(dtChild.Rows(index)(_ConceptID), Object)
                        End If
                        If _ICD9 <> "" Then
                            oChildNode.ICD9 = CType(dtSort.Rows(index)(_ICD9), Object)
                        End If
                        If _CPT <> "" Then
                            oChildNode.CPT = CType(dtSort.Rows(index)(_CPT), Object)
                        End If
                        If _HistoryType <> "" Then
                            oChildNode.HistoryType = CType(dtSort.Rows(index)(_HistoryType), Object)
                        End If
                        '' BIND CODE '' 
                        If _CodeMember <> "" Then
                            oChildNode.Code = dtChild.Rows(index)(_CodeMember)
                        End If

                        '' BIND DESCRIPTION '' 
                        If _DescriptionMember <> "" Then
                            oChildNode.Description = dtChild.Rows(index)(_DescriptionMember)
                        End If

                        ' ''GLO2010-0005444' BIND INDICATOR '' 
                        If _sIndicator <> "" Then
                            oChildNode.Indicator = dtChild.Rows(index)(_sIndicator)
                        End If

                        '' GLO2011-0010684
                        '' Bind the Comments field to the Comment node 
                        If _sComment <> "" Then
                            oChildNode.Comments = dtChild.Rows(index)(_sComment)
                        End If

                        '' BIND UNIT ''
                        If _UnitMember <> "" Then
                            oChildNode.Unit = dtChild.Rows(index)(_UnitMember)
                        End If

                        '' BIND DRUG FORM ''
                        If _DrugFormMember <> "" Then
                            oChildNode.DrugForm = dtChild.Rows(index)(_DrugFormMember)
                        End If

                        '' BIND ROUTE ''
                        If _RouteMember <> "" Then
                            oChildNode.Route = dtChild.Rows(index)(_RouteMember)
                        End If

                        '' BIND FREQUENCY ''
                        If _FrequencyMember <> "" Then
                            oChildNode.Frequency = dtChild.Rows(index)(_FrequencyMember)
                        End If

                        '' BIND DURATION ''
                        If _DurationMember <> "" Then
                            oChildNode.Duration = dtChild.Rows(index)(_DurationMember)
                        End If

                        '' BIND NDC CODE ''
                        If _NDCCodeMember <> "" Then
                            oChildNode.NDCCode = dtChild.Rows(index)(_NDCCodeMember)
                        End If

                        '' BIND DRUG QUANTITY QUALIFIER ''
                        If _DrugQtyQualifierMember <> "" Then
                            oChildNode.DrugQtyQualifier = dtChild.Rows(index)(_DrugQtyQualifierMember)
                        End If

                        '' BIND DDID ''
                        If _DDIDMember <> "" Then
                            oChildNode.DDID = CType(dtChild.Rows(index)(_DDIDMember), Int64)
                        End If

                        '' BIND IS NARCOTICS ''
                        If _IsNarcoticsMember <> "" Then
                            oChildNode.IsNarcotics = CType(dtChild.Rows(index)(_IsNarcoticsMember), Int16)
                        End If

                        '' BIND IMAGE OBJECT TO NODE ''
                        If _ImageObject <> "" Then
                            oChildNode.TemplateResult = dtSort.Rows(index)(_ImageObject)
                        End If

                        If _IsSystemCategory <> "" Then
                            oChildNode.IsSystemCategory = dtSort.Rows(index)(_IsSystemCategory)
                        End If

                        '' BIND DISPLAY TEXT ''
                        If (dtChild.Columns.Contains(col_DisplayText)) Then
                            oChildNode.Text = dtChild.Rows(index)(col_DisplayText)
                        Else
                            oChildNode.Text = ""
                        End If



                        '' ADD NODE TO PARENT ''
                        oParentNode.Nodes.Add(oChildNode)

                        '' 
                        '' To give the extra functionality of identifying the node
                        '' Raise the event after node is added to the treeview
                        RaiseEvent NodeAdded(oChildNode)
                        oChildNode = Nothing
                    Next

                    '' ADD PARENT NODE TO MAIN TREEVIEW''
                    trvMain.Nodes.Add(oParentNode)
                    arrParentNodes.Add(oParentNode.Text.ToLower)

                    If maxNodeCount >= _MaxNode Then
                        Exit For
                    End If
                    oParentNode = Nothing
                Next
            End If

            trvMain.ExpandAll()
            ''Sandip Darade 20090601
            ''select the first node of the tree
            If trvMain.Nodes.Count > 0 Then
                trvMain.SelectedNode = trvMain.Nodes(0)
                trvMain.SelectedNode.EnsureVisible()
            End If

            trvMain.Sort()
            arrParentNodes.Clear()
            arrParentNodes = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub CheckSelectedNodes()
        '' IF CHECKBOXES = TRUE '' THEN CHECK SELECTED NODES IN TREE ''
        If trvMain.CheckBoxes And arrSelectedNodeIDs.Count > 0 Then
            isTreeLoading = True

            If _ParentMember <> "" Then
                '' LEVEL 1 TREE ''
                For Each oParent As TreeNode In trvMain.Nodes
                    For iTree As Integer = 0 To oParent.Nodes.Count - 1
                        If arrSelectedNodeIDs.Contains(CType(oParent.Nodes(iTree), myTreeNode).ID) Then
                            oParent.Nodes(iTree).Checked = True
                        End If
                    Next
                Next
            Else
                '' LEVEL 0 TREE ''
                For iTree As Integer = 0 To trvMain.Nodes.Count - 1
                    If arrSelectedNodeIDs.Contains(CType(trvMain.Nodes(iTree), myTreeNode).ID) Then
                        trvMain.Nodes(iTree).Checked = True
                    End If
                Next
            End If

            isTreeLoading = False
        End If
    End Sub

    Private Sub AttachDisplayTextToDataTable(ByVal loopCount As Int64)
        '' WHILE APPLICATION DOEVENT, DTMASTER LOSE DISPLAY COLUMN, THEN ADD IT IF NOT PRESENT ''
        If _DTMaster.Columns.Contains(col_DisplayText) = False Then
            Dim oColumn As New DataColumn(col_DisplayText)
            _DTMaster.Columns.Add(oColumn)
        End If

        '' FILL NEW COLUMN RECORDS ''
        _DTMaster.BeginLoadData()
        Select Case _DisplayType
            Case enumDisplayType.Code
                For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                    _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember)
                Next

            Case enumDisplayType.Descripation
                For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                    _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                Next

            Case enumDisplayType.Code_Description
                For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                    '_DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember) & " - " & _DTMaster.Rows(index)(_DescriptionMember)
                    ''Sandip Darade 20090623
                    ''check if description is blank
                    _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember)

                    'Additional conditions added to resolve the Problem 00000092
                    '---
                    If (_DTMaster.Rows(index)(_DescriptionMember) <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                        _DTMaster.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_DescriptionMember)
                    ElseIf (_DTMaster.Rows(index)(_DescriptionMember) <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                    End If
                    '---
                Next

            Case enumDisplayType.Code_Description_Unit
                For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                    ' _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember) & " - " & _DTMaster.Rows(index)(_DescriptionMember) & " - " & dtSort.Rows(index)(_UnitMember)
                    ''Sandip Darade 20090623
                    ''check if description or unit or both are  blank
                    _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember)
                    'Additional conditions added to resolve the Problem 00000092
                    '---
                    If (_DTMaster.Rows(index)(_DescriptionMember) <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                        _DTMaster.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_DescriptionMember)
                    ElseIf (_DTMaster.Rows(index)(_DescriptionMember) <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                    End If

                    If (_DTMaster.Rows(index)(_UnitMember) <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                        _DTMaster.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_UnitMember)
                    ElseIf (_DTMaster.Rows(index)(_UnitMember) <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                        _DTMaster.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_UnitMember)
                    End If
                    '---
                Next

            Case Else
                'For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                '    dtSort.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                'Next
                ''Sandip Darade 20091012
                ''above code commented  replacing  it with the code below

                'Additional conditions added to resolve the Problem 00000092
                '---
                For index As Int64 = loopCount To _DTMaster.Rows.Count - 1
                    dtSort.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_CodeMember).ToString.Trim

                    If (dtSort.Rows(index)(_DescriptionMember) <> "" And dtSort.Rows(index)(col_DisplayText) <> "") Then
                        dtSort.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_DescriptionMember)
                    ElseIf (_DTMaster.Rows(index)(_DescriptionMember) <> "" And dtSort.Rows(index)(col_DisplayText) = "") Then
                        dtSort.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DescriptionMember)
                    End If

                    If (dtSort.Rows(index)(_DrugFormMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) <> "") Then
                        dtSort.Rows(index)(col_DisplayText) &= " - " & _DTMaster.Rows(index)(_DrugFormMember).ToString.Trim
                    ElseIf (dtSort.Rows(index)(_DrugFormMember).ToString.Trim <> "" And _DTMaster.Rows(index)(col_DisplayText) = "") Then
                        dtSort.Rows(index)(col_DisplayText) = _DTMaster.Rows(index)(_DrugFormMember).ToString.Trim
                    End If
                Next
                '---
        End Select
        _DTMaster.EndLoadData()
        dvSort = _DTMaster.DefaultView
    End Sub

    'SHUBHANGI 20090930
    'Use Clear button to clear search text box
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtsearch.ResetText()
        txtsearch.Focus()

        If Me.bIsDiagnosisSearch Or Me.bISCPTSearch Then
            RaiseEvent SearchFired()
        Else
            FillTree("")
        End If
        ' 
    End Sub

    ''Sandip darade  2001014
    ''Pull thadrug from database 
    'Public Function FillDrugs(Optional ByVal strsearch As String = "") As DataTable

    '    '' 'gsp_FillDrugs_Mst' pulls top 40 records replace it with 'gsp_FillAllDrugs_Mst' pulling all records
    '    Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '    Dim dtdrugs As DataTable
    '    strsearch = strsearch.Replace("'", "''")
    '    Try
    '        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
    '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
    '        oParamater.DataType = SqlDbType.Char
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@drugletter"
    '        oParamater.Value = strsearch
    '        oDB.DBParametersCol.Add(oParamater)

    '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
    '        oParamater.DataType = SqlDbType.Int
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@flag"
    '        oParamater.Value = DrugFlag
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing
    '        dtdrugs = oDB.GetDataTable("gsp_FillAllDrugs_Mst")

    '        Return dtdrugs
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(oDB) Then
    '            oDB.Dispose()
    '            oDB = Nothing
    '        End If
    '    End Try
    'End Function

    'Private Function FillEducationMappingDrugs(ByVal nSearchType As Integer) As DataTable
    '    Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '    Dim dtdrugs As DataTable
    '    Try
    '        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
    '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter

    '        With oParamater
    '            .DataType = SqlDbType.Int
    '            .Direction = ParameterDirection.Input
    '            .Name = "@FetchType"
    '            .Value = nSearchType
    '        End With
    '        oDB.DBParametersCol.Add(oParamater)

    '        oParamater = Nothing
    '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter

    '        With oParamater
    '            .DataType = SqlDbType.VarChar
    '            .Direction = ParameterDirection.Input
    '            .Name = "@SearchText"
    '            .Value = Me.txtsearch.Text.ToString
    '        End With

    '        oDB.DBParametersCol.Add(oParamater)
    '        dtdrugs = oDB.GetDataTable("Education_Drugs_Search")

    '        Return dtdrugs
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(oDB) Then
    '            oDB.Dispose()
    '            oDB = Nothing
    '        End If
    '    End Try
    'End Function

#End Region

End Class

