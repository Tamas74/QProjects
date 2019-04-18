Imports gloEMR.gloStream.DiseaseManagement
Imports System.Windows.Forms
Partial Public Class frmCQM_RulesSetup

    Private Sub SaveCriteria()

     
    End Sub

    Public Sub PopulateAssocaitedInfo(ByVal ID As Int32)

        Try
           

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub fillProblemlistSnomadeoff(Optional ByVal strsearch As String = "")
       
    End Sub

    Private Sub Search(ByVal searchtextbox As TextBox, ByVal searchtreeview As TreeView)
        Try
            'check for text to be search
            If Trim(searchtextbox.Text) <> "" Then
                If (IsNothing(searchtreeview) = False) Then
                    If searchtreeview.GetNodeCount(False) > 0 Then
                        Dim mychildnode As TreeNode
                        'child node collection
                        For i As Integer = 0 To searchtreeview.Nodes.Count - 1
                            ''For Each mychildnode In searchtreeview.Nodes.Item(i).Nodes ''Commented Sandip Darade 
                            For Each mychildnode In searchtreeview.Nodes  ''Sandip Darade 
                                Dim str As String
                                str = UCase(Trim(mychildnode.Text))
                                If Mid(str, 1, Len(Trim(searchtextbox.Text))) = UCase(Trim(searchtextbox.Text)) Then
                                    searchtreeview.SelectedNode = searchtreeview.Nodes(searchtreeview.Nodes.Count - 1)
                                    searchtreeview.SelectedNode = mychildnode
                                    searchtextbox.Focus()
                                    Exit Sub
                                End If
                            Next
                        Next
                    End If
                End If


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function GetHistoryCategory() As DataTable
        Return Nothing
    End Function

    Private Sub Fill_Histories(Optional ByVal HistoryCategory As String = "")
      
    End Sub

    Private Sub Fill_Histories_1_Ex(Optional ByVal HistoryCategory As String = "")
       
    End Sub

    Private Sub Fill_Histories_old(Optional ByVal HistoryCategory As String = "")
      
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Function GetFtInch(ByVal strHeight As String) As Array
        strHeight = Mid(strHeight, 1, Len(strHeight) - 1)
        Return Split(strHeight, "'", , CompareMethod.Text)
    End Function

    Private Function GetTemplate(ByVal TemplateID As Int64) As Object
       
        Return Nothing
    End Function

    Private Sub FillAllCriteria()
        Try
            If LoadFirst = False Then
                '  Fill_Labs_ByTable()
                '  Fill_RadiologyLabs_ByTable()
                '   Fill_OtherInfo()
                ' Fill_CriteriaDetails(m_CriteriaId)
            End If
        Catch ex As Exception
        Finally
            LoadFirst = True
        End Try
    End Sub

    Private Function RemoveSelectedItemFromList(ByRef lstView As System.Windows.Forms.ListView) As Boolean
        Dim _isItemRemoved As Boolean = False
        Try
            If Not IsNothing(lstView) AndAlso Not IsNothing(lstView.Items) AndAlso lstView.Items.Count > 0 AndAlso lstView.SelectedItems.Count > 0 Then
                For Each item As ListViewItem In lstView.SelectedItems
                    lstView.Items.Remove(item)
                    _isItemRemoved = True
                Next
            Else
                MessageBox.Show("Select item to be removed from the list or no items present to remove.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            _isItemRemoved = False
        Finally
        End Try
        Return _isItemRemoved
    End Function

    Private Function AddItemToList(ByRef lstView As ListView, ByVal lstViewItem As ListViewItem) As Boolean
        Dim _isItemAdded As Boolean = False
        Dim Ispresent As Boolean = False
        Try
            If Not IsNothing(lstViewItem) Then
                For Each myItems As ListViewItem In lstView.Items
                    If myItems.Tag = lstViewItem.Tag Then
                        Ispresent = True
                        Exit For
                    End If
                Next
                If Ispresent = False Then
                    lstView.Items.Add(lstViewItem)
                    _isItemAdded = True
                End If
            End If
        Catch ex As Exception
            _isItemAdded = False
        Finally
        End Try
        Return _isItemAdded
    End Function

    Private Sub ShowSnoMedSelector(ByVal tab As TabType)
       
    End Sub

#Region " Fill control methods "

    Private Sub FillCriteriaRefInfo()
     
    End Sub

    Private Sub Fill_Labs_ByTable()
       
    End Sub

    Private Sub DesignLabsGridByTable(ByRef c1FlexGrid)
        If c1FlexGrid.Rows.Count > 1 Then
            Dim strOperator As String = "Greater Than" & "|" & "Less Than" & "|" & "Between" & "|" & "Equals"
            Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            Dim rgOperator As C1.Win.C1FlexGrid.CellRange = Nothing
            Try
                c1FlexGrid.Cols("Value").DataType = GetType(Boolean)
                c1FlexGrid.Cols("LabId").DataType = GetType(Int64)
                c1FlexGrid.Cols("Test").DataType = GetType(String)
                c1FlexGrid.Cols("ResultID").DataType = GetType(Int64)
                c1FlexGrid.Cols("Result").DataType = GetType(String)
                c1FlexGrid.Cols("Operator").DataType = GetType(String)
                c1FlexGrid.Cols("Result Value1").DataType = GetType(String)
                c1FlexGrid.Cols("Result Value2").DataType = GetType(String)
                c1FlexGrid.Cols("LabId").Visible = False
                c1FlexGrid.Cols("ResultID").Visible = False

                rgOperator = c1FlexGrid.GetCellRange(1, c1FlexGrid.Cols("Operator").Index, c1FlexGrid.Rows.Count - 1, c1FlexGrid.Cols("Operator").Index)
                '  cStyle = c1FlexGrid.Styles.Add("Operator")
                Try
                    If (c1FlexGrid.Styles.Contains("Operator")) Then
                        cStyle = c1FlexGrid.Styles("Operator")
                    Else
                        cStyle = c1FlexGrid.Styles.Add("Operator")

                    End If
                Catch ex As Exception
                    cStyle = c1FlexGrid.Styles.Add("Operator")

                End Try
                cStyle.ComboList = strOperator
                rgOperator.Style = cStyle

                c1FlexGrid.Cols(0).Width = 40
                c1FlexGrid.Cols("Test").Width = 290
                c1FlexGrid.Cols("Result").Width = 290
                c1FlexGrid.Cols("Operator").Width = 100
                c1FlexGrid.Cols("Result Value1").Width = 115
                c1FlexGrid.Cols("Result Value2").Width = 115

                c1FlexGrid.ExtendLastCol = False
                c1FlexGrid.Cols("Test").AllowEditing = False
                c1FlexGrid.Cols("Result").AllowEditing = False
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                strOperator = Nothing
                cStyle = Nothing
                rgOperator = Nothing
            End Try
        End If
    End Sub

    Private Sub Fill_RadiologyLabs_ByTable()
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        'Dim _dtRadiologyOrders As DataTable = Nothing
        'Dim _dtExRadiologyOrders As DataTable = Nothing

        'Try
        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    _dtRadiologyOrders = oDM.GetOrdersTable(m_CriteriaId)
        '    _dtExRadiologyOrders = oDM.GetExOrdersTable(m_CriteriaId)

        '    If Not IsNothing(_dtRadiologyOrders) Then
        '        'c1Labs.Clear()
        '        c1Labs.DataSource = Nothing
        '        c1Labs.BeginUpdate()
        '        c1Labs.DataSource = _dtRadiologyOrders.Copy()
        '        c1Labs.EndUpdate()
        '        DesignRadiologyGridByTable(c1Labs)
        '        SetOrders_ByTable()
        '    End If

        '    If Not IsNothing(_dtExRadiologyOrders) Then
        '        'c1Labs_Ex.Clear()
        '        c1Labs_Ex.DataSource = Nothing
        '        c1Labs_Ex.BeginUpdate()
        '        c1Labs_Ex.DataSource = _dtExRadiologyOrders.Copy()
        '        c1Labs_Ex.EndUpdate()
        '        DesignRadiologyGridByTable(c1Labs_Ex)
        '        SetExOrders_ByTable()
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    c1Labs.EndUpdate()
        '    c1Labs_Ex.EndUpdate()

        '    If oDM IsNot Nothing Then
        '        oDM.Dispose()
        '        oDM = Nothing
        '    End If

        '    If Not IsNothing(_dtRadiologyOrders) Then
        '        _dtRadiologyOrders.Dispose()
        '        _dtRadiologyOrders = Nothing
        '    End If
        '    If Not IsNothing(_dtExRadiologyOrders) Then
        '        _dtExRadiologyOrders.Dispose()
        '        _dtExRadiologyOrders = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub DesignRadiologyGridByTable(ByRef c1FlexGrid)
        Try
            c1FlexGrid.Cols("Value").DataType = GetType(Boolean)
            c1FlexGrid.Cols("CategoryId").DataType = GetType(Int64)
            c1FlexGrid.Cols("Category").DataType = GetType(String)
            c1FlexGrid.Cols("GroupId").DataType = GetType(Int64)
            c1FlexGrid.Cols("Group").DataType = GetType(String)
            c1FlexGrid.Cols("TestId").DataType = GetType(Int64)
            c1FlexGrid.Cols("Test").DataType = GetType(String)
            c1FlexGrid.Cols("CategoryId").Visible = False
            c1FlexGrid.Cols("GroupId").Visible = False
            c1FlexGrid.Cols("TestId").Visible = False
            c1FlexGrid.Cols("Value").Width = 50
            c1FlexGrid.Cols("Category").Width = 250
            c1FlexGrid.Cols("Group").Width = 250
            c1FlexGrid.ExtendLastCol = True
            c1FlexGrid.Cols("Category").AllowEditing = False
            c1FlexGrid.Cols("Group").AllowEditing = False
            c1FlexGrid.Cols("Test").AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub Fill_OtherInfo()
        'Dim associatenode As myTreeNode = Nothing
        'Dim MyChild As myTreeNode = Nothing

        'Try
        '    associatenode = New myTreeNode("Orders", -1)

        '    MyChild = New myTreeNode
        '    MyChild.Text = "Orders"
        '    MyChild.Key = -1
        '    MyChild.ImageIndex = 6
        '    MyChild.SelectedImageIndex = 6
        '    associatenode.Nodes.Add(MyChild)
        '    MyChild = Nothing

        '    MyChild = New myTreeNode
        '    MyChild.Text = "Order Templates"
        '    MyChild.Key = -1
        '    MyChild.ImageIndex = 7
        '    MyChild.SelectedImageIndex = 7
        '    associatenode.Nodes.Add(MyChild)
        '    MyChild = Nothing

        '    MyChild = New myTreeNode
        '    MyChild.Text = "Guidelines"
        '    MyChild.Key = -1
        '    MyChild.ImageIndex = 8
        '    MyChild.SelectedImageIndex = 8
        '    associatenode.Nodes.Add(MyChild)
        '    MyChild = Nothing

        '    MyChild = New myTreeNode
        '    MyChild.Text = "Rx"
        '    MyChild.Key = -1
        '    MyChild.ImageIndex = 9
        '    MyChild.SelectedImageIndex = 9
        '    associatenode.Nodes.Add(MyChild)
        '    MyChild = Nothing

        '    MyChild = New myTreeNode
        '    MyChild.Text = "Referrals"
        '    MyChild.Key = -1
        '    MyChild.ImageIndex = 10
        '    MyChild.SelectedImageIndex = 10
        '    associatenode.Nodes.Add(MyChild)
        '    MyChild = Nothing

        '    MyChild = New myTreeNode
        '    MyChild.Text = "IM"
        '    MyChild.Key = -1
        '    MyChild.ImageIndex = 12
        '    MyChild.SelectedImageIndex = 12
        '    associatenode.Nodes.Add(MyChild)
        '    MyChild = Nothing

        '    'trOrderInfo.BeginUpdate()
        '    'trOrderInfo.Nodes.Add(associatenode)
        '    'trOrderInfo.EndUpdate()
        '    'trOrderInfo.ExpandAll()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    MyChild = Nothing
        '    associatenode = Nothing
        'End Try
    End Sub

    Public Sub FillRadiologyTest()
        'Dim dtOrders As DataTable = Nothing
        'Dim oCls As gloStream.DiseaseManagement.Common.Criteria = Nothing
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    oCls = New gloStream.DiseaseManagement.Common.Criteria
        '    dtOrders = oCls.OrdersTable

        '    If Not IsNothing(dtOrders) Then
        '        GloUC_trvAssociates.Clear()
        '        GloUC_trvAssociates.DataSource = dtOrders.Copy()
        '        GloUC_trvAssociates.CodeMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
        '        GloUC_trvAssociates.ValueMember = Convert.ToString(dtOrders.Columns(0).ColumnName)
        '        GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
        '        GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        '        GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        '        GloUC_trvAssociates.FillTreeView()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Arrow
        '    oCls.Dispose()
        '    oCls = Nothing
        '    If Not IsNothing(dtOrders) Then
        '        dtOrders.Dispose()
        '        dtOrders = Nothing
        '    End If
        'End Try
    End Sub

    Public Sub FillReferrals()
        'Dim objTemplateGallery As clsTemplateGallery = Nothing
        'Dim objCategory As myTreeNode = Nothing
        'Dim dvTemplate As DataView = Nothing
        'Dim dtCategory As DataTable = Nothing
        'Dim ValueMember As Int64 = 0
        'Dim DisplayMember As String = ""
        'Dim dtTemplate As DataTable = Nothing

        'Try
        '    objTemplateGallery = New clsTemplateGallery
        '    dtCategory = objTemplateGallery.GetAllCategory

        '    For i As Integer = 0 To dtCategory.Rows.Count - 1
        '        ValueMember = 0
        '        DisplayMember = ""

        '        ValueMember = dtCategory.Rows(i)(0)
        '        DisplayMember = dtCategory.Rows(i)(1)

        '        If DisplayMember = "Referral Letter" Then
        '            objCategory = New myTreeNode(DisplayMember, ValueMember)
        '            dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
        '            dtTemplate = dvTemplate.Table
        '            If Not dtTemplate Is Nothing Then
        '                GloUC_trvAssociates.Clear()
        '                GloUC_trvAssociates.DataSource = dtTemplate.Copy()
        '                GloUC_trvAssociates.ValueMember = dtTemplate.Columns(0).ColumnName
        '                GloUC_trvAssociates.DescriptionMember = dtTemplate.Columns(1).ColumnName
        '                GloUC_trvAssociates.CodeMember = dtTemplate.Columns(1).ColumnName
        '                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        '                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        '                GloUC_trvAssociates.FillTreeView()
        '            End If
        '        End If
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    objTemplateGallery.Dispose()
        '    objTemplateGallery = Nothing

        '    objCategory = Nothing

        '    If Not IsNothing(dtTemplate) Then
        '        dtTemplate.Dispose()
        '    End If

        '    If Not IsNothing(dtCategory) Then
        '        dtCategory.Dispose()
        '    End If

        '    If Not IsNothing(dvTemplate) Then
        '        dvTemplate.Dispose()
        '    End If

        'End Try
    End Sub

    Private Sub Fill_Age()

        'Dim oCollection As Collection = Nothing
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        'Dim oSett As gloSettings.GeneralSettings = Nothing
        'Dim objVal As Object = Nothing
        'Dim _result As Boolean = False

        'Try

        '    objVal = New Object()
        '    oSett = New gloSettings.GeneralSettings(GetConnectionString())
        '    oSett.GetSetting("PEDIATRICS", objVal)

        '    If Not IsNothing(objVal) AndAlso objVal.ToString() <> "" Then
        '        _result = CType(objVal, Boolean)
        '    End If


        '    'cmbAgeMinMnth.Enabled = _result
        '    'cmbAgeMaxMnth.Enabled = _result
        '    'cmbAgeMinMnth_Ex.Enabled = _result
        '    'cmbAgeMaxMnth_Ex.Enabled = _result

        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    '' oCollection = New Collection
        '    oCollection = oDM.Age

        '    cmbAgeMin.BeginUpdate()
        '    cmbAgeMax.BeginUpdate()
        '    cmbAgeMin_Ex.BeginUpdate()
        '    cmbAgeMax_Ex.BeginUpdate()

        '    If Not IsNothing(oCollection) AndAlso oCollection.Count > 0 Then
        '        cmbAgeMin.Items.Clear()
        '        cmbAgeMax.Items.Clear()
        '        cmbAgeMin_Ex.Items.Clear()
        '        cmbAgeMin_Ex.Items.Clear()
        '        cmbAgeMinMnth.Items.Clear()
        '        cmbAgeMaxMnth.Items.Clear()
        '        cmbAgeMinMnth_Ex.Items.Clear()
        '        cmbAgeMaxMnth_Ex.Items.Clear()

        '        For i As Int16 = 1 To oCollection.Count - 1
        '            cmbAgeMin.Items.Add(oCollection(i))
        '            cmbAgeMax.Items.Add(oCollection(i))
        '            cmbAgeMin_Ex.Items.Add(oCollection(i))
        '            cmbAgeMax_Ex.Items.Add(oCollection(i))
        '        Next
        '    End If

        '    cmbAgeMin.Items.RemoveAt(0)
        '    cmbAgeMax.Items.RemoveAt(0)
        '    cmbAgeMin_Ex.Items.RemoveAt(0)
        '    cmbAgeMax_Ex.Items.RemoveAt(0)

        '    cmbAgeMin.Items.Insert(0, "")
        '    cmbAgeMax.Items.Insert(0, "")
        '    cmbAgeMin_Ex.Items.Insert(0, "")
        '    cmbAgeMax_Ex.Items.Insert(0, "")

        '    'For i As Int16 = 0 To 11
        '    cmbAgeMinMnth.Items.Clear()
        '    cmbAgeMinMnth.Items.Add("")
        '    cmbAgeMinMnth.Items.Add("01")
        '    cmbAgeMinMnth.Items.Add("02")
        '    cmbAgeMinMnth.Items.Add("03")
        '    cmbAgeMinMnth.Items.Add("04")
        '    cmbAgeMinMnth.Items.Add("05")
        '    cmbAgeMinMnth.Items.Add("06")
        '    cmbAgeMinMnth.Items.Add("07")
        '    cmbAgeMinMnth.Items.Add("08")
        '    cmbAgeMinMnth.Items.Add("09")
        '    cmbAgeMinMnth.Items.Add("10")
        '    cmbAgeMinMnth.Items.Add("11")

        '    For itemIndex As Int16 = 0 To cmbAgeMinMnth.Items.Count - 1
        '        cmbAgeMinMnth_Ex.Items.Add(cmbAgeMinMnth.Items(itemIndex))
        '    Next

        '    cmbAgeMaxMnth.Items.Clear()
        '    cmbAgeMaxMnth.Items.Add("")
        '    cmbAgeMaxMnth.Items.Add("01")
        '    cmbAgeMaxMnth.Items.Add("02")
        '    cmbAgeMaxMnth.Items.Add("03")
        '    cmbAgeMaxMnth.Items.Add("04")
        '    cmbAgeMaxMnth.Items.Add("05")
        '    cmbAgeMaxMnth.Items.Add("06")
        '    cmbAgeMaxMnth.Items.Add("07")
        '    cmbAgeMaxMnth.Items.Add("08")
        '    cmbAgeMaxMnth.Items.Add("09")
        '    cmbAgeMaxMnth.Items.Add("10")
        '    cmbAgeMaxMnth.Items.Add("11")

        '    For itemIndex As Int16 = 0 To cmbAgeMaxMnth.Items.Count - 1
        '        cmbAgeMaxMnth_Ex.Items.Add(cmbAgeMaxMnth.Items(itemIndex))
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    cmbAgeMin.EndUpdate()
        '    cmbAgeMax.EndUpdate()
        '    cmbAgeMin_Ex.EndUpdate()
        '    cmbAgeMax_Ex.EndUpdate()
        '    oCollection.Clear()
        '    oCollection = Nothing
        '    oDM.Dispose()
        '    oDM = Nothing
        '    If Not IsNothing(oSett) Then
        '        oSett.Dispose()
        '    End If
        '    objVal = Nothing
        'End Try
    End Sub

    Private Sub fill_Maritalst()
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        'Dim oCollection As Collection = Nothing

        'Try
        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    oCollection = oDM.MaritalStatus
        '    cmbChkBoxMaritalSt.BeginUpdate()
        '    cmbChkBoxMaritalSt_Ex.BeginUpdate()
        '    cmbChkBoxMaritalSt.Items.Clear()
        '    cmbChkBoxMaritalSt_Ex.Items.Clear()

        '    If Not IsNothing(oCollection) AndAlso oCollection.Count > 0 Then
        '        For i As Int16 = 1 To oCollection.Count
        '            cmbChkBoxMaritalSt.Items.Add(oCollection(i))
        '            cmbChkBoxMaritalSt_Ex.Items.Add(oCollection(i))
        '        Next
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    cmbChkBoxMaritalSt.EndUpdate()
        '    cmbChkBoxMaritalSt_Ex.EndUpdate()

        '    If Not IsNothing(oDM) Then
        '        oDM.Dispose()
        '        oDM = Nothing
        '    End If

        '    If Not IsNothing(oCollection) Then
        '        oCollection.Clear()
        '        oCollection = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub fill_gender()
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        'Dim oCollection As Collection = Nothing

        'Try
        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    oCollection = oDM.Gender
        '    cmbChkBoxGender.BeginUpdate()
        '    cmbGender_Ex.BeginUpdate()
        '    cmbChkBoxGender.Items.Clear()
        '    cmbGender_Ex.Items.Clear()

        '    For i As Int16 = 1 To oCollection.Count
        '        cmbChkBoxGender.Items.Add(oCollection(i))
        '        cmbGender_Ex.Items.Add(oCollection(i))
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    cmbChkBoxGender.EndUpdate()
        '    cmbGender_Ex.EndUpdate()
        '    If Not IsNothing(oDM) Then
        '        oDM.Dispose()
        '        oDM = Nothing
        '    End If
        '    If Not IsNothing(oCollection) Then
        '        oCollection.Clear()
        '        oCollection = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub fill_race()
        'Dim oCollectection As Collection = Nothing
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing
        'Try

        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    oCollectection = oDM.Race
        '    cmbChkBoxRace.BeginUpdate()
        '    cmbChkBoxRace_Ex.BeginUpdate()
        '    cmbChkBoxRace.Items.Clear()
        '    cmbChkBoxRace_Ex.Items.Clear()

        '    For i As Int16 = 1 To oCollectection.Count
        '        cmbChkBoxRace.Items.Add(oCollectection(i))
        '        cmbChkBoxRace_Ex.Items.Add(oCollectection(i))
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally

        '    cmbChkBoxRace.EndUpdate()
        '    cmbChkBoxRace_Ex.EndUpdate()

        '    If Not IsNothing(oDM) Then
        '        oDM.Dispose()
        '        oDM = Nothing
        '    End If

        '    If Not IsNothing(oCollectection) Then
        '        oCollectection.Clear()
        '        oCollectection = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub fill_state()
        'Dim oCollectection1 As Collection = Nothing
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing

        'Try
        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    oCollectection1 = oDM.State
        '    cmbState.BeginUpdate()
        '    cmbState_Ex.BeginUpdate()
        '    cmbState.Items.Clear()
        '    cmbState_Ex.Items.Clear()

        '    If Not IsNothing(oCollectection1) AndAlso oCollectection1.Count > 0 Then
        '        cmbState.Items.Add("")
        '        cmbState_Ex.Items.Add("")
        '        For i As Integer = 1 To oCollectection1.Count
        '            cmbState.Items.Add(oCollectection1(i))
        '            cmbState_Ex.Items.Add(oCollectection1(i))
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    cmbState.EndUpdate()
        '    cmbState_Ex.EndUpdate()
        '    oCollectection1.Clear()
        '    oCollectection1 = Nothing
        '    If oDM IsNot Nothing Then
        '        oDM.Dispose()
        '        oDM = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub fill_EmpState()

        'Dim oCollectection As Collection = Nothing
        'Dim oDM As gloStream.DiseaseManagement.Common.Criteria = Nothing

        'Try
        '    oDM = New gloStream.DiseaseManagement.Common.Criteria
        '    oCollectection = oDM.EmploymentStatus
        '    cmbEmpStatus.Items.Clear()
        '    cmbEmpStatus_Ex.Items.Clear()

        '    If Not IsNothing(oCollectection) AndAlso oCollectection.Count > 0 Then
        '        cmbEmpStatus.Items.Add("")
        '        cmbEmpStatus_Ex.Items.Add("")
        '        For i As Int16 = 1 To oCollectection.Count
        '            cmbEmpStatus.Items.Add(oCollectection(i))
        '            cmbEmpStatus_Ex.Items.Add(oCollectection(i))
        '        Next
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    If oDM IsNot Nothing Then
        '        oDM.Dispose()
        '        oDM = Nothing
        '    End If
        '    oCollectection.Clear()
        '    oCollectection = Nothing
        'End Try
    End Sub

#End Region ' Fill control methods '

#Region " Fill Orders to be given controls methods "

    'AddAssociated not yet refactored - Sagar
    'Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal strType As String, Optional ByVal addTemplate As Boolean = False)


    ' End Sub

    Public Sub FillLabTest()


    End Sub

    Private Sub fill_IM()


    End Sub

    Private Sub fill_guideline()

    End Sub

    Public Sub FillRx()


    End Sub

#End Region 'Fill Orders to be given controls methods '

    Public Sub Fill_CriteriaDetails(ByVal lCriteriaID As Long)


    End Sub

#Region " Criteria/Rule set methods "

    Public Sub SetDemographicsInformation(ByVal tab As TabType, ByVal City As String, ByVal State As String, ByVal Zip As String, ByVal EmployementStatus As String)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SetAgeMin(ByVal tab As TabType, ByVal ageMinimum As String, ByVal ageMin As String)
        Dim _ageArrayStr() As String = Nothing
        Try
            If ageMinimum.ToString.Contains(".") Then
                If TabType.Trigger = tab Then
                    Dim _age() As String = ageMinimum.ToString.Split(".")
                    cmbAgeMin.Text = _age(0) '': SetCombiIndex(cmbAgeMin)
                    _ageArrayStr = ageMin.Split(".")
                    If (_ageArrayStr(1).ToString() = "1") Then
                        cmbAgeMinMnth.Text = "10"
                    Else
                        cmbAgeMinMnth.Text = _age(1) ''Format(CDbl((CDbl("." & _age(1))) * 12), "#00")
                    End If
                ElseIf TabType.Exception = tab Then
                    'Dim _age() As String = ageMinimum.ToString.Split(".")
                    'cmbAgeMin_Ex.Text = _age(0) '': SetCombiIndex(cmbAgeMin_Ex)
                    '_ageArrayStr = ageMin.Split(".")
                    'If (_ageArrayStr(1).ToString() = "1") Then
                    '    cmbAgeMinMnth_Ex.Text = "10"
                    'Else
                    '    cmbAgeMinMnth_Ex.Text = _age(1) ''Format(CDbl((CDbl("." & _age(1))) * 12), "#00")
                    'End If
                End If
            Else
                If Val(ageMinimum) > 0 Then
                    If TabType.Trigger = tab Then
                        cmbAgeMin.Text = ageMinimum '': SetCombiIndex(cmbAgeMin)
                        cmbAgeMinMnth.Text = "00" '': SetCombiIndex(cmbAgeMinMnth)
                    ElseIf TabType.Exception = tab Then
                        '   cmbAgeMin_Ex.Text = ageMinimum : SetCombiIndex(cmbAgeMin_Ex)
                        '  cmbAgeMinMnth_Ex.Text = "00" '': SetCombiIndex(cmbAgeMinMnth_Ex)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _ageArrayStr = Nothing
        End Try
    End Sub

    Public Sub SetAgeMax(ByVal tab As TabType, ByVal ageMaximum As String, ByVal ageMax As String)
        Dim _ageArrayStr() As String = Nothing
        Try
            If ageMaximum.ToString.Contains(".") Then
                If tab = TabType.Trigger Then
                    Dim _age() As String = ageMaximum.ToString.Split(".")
                    cmbAgeMax.Text = _age(0) ''SetCombiIndex(cmbAgeMax)
                    _ageArrayStr = ageMax.Split(".")
                    If (_ageArrayStr(1).ToString() = "1") Then
                        cmbAgeMaxMnth.Text = "10"
                    Else
                        cmbAgeMaxMnth.Text = _age(1) '' Format(CDbl((CDbl("." & _age(1))) * 12), "#00") : SetCombiIndex(cmbAgeMaxMnth)
                    End If
                ElseIf tab = TabType.Exception Then
                    Dim _age() As String = ageMaximum.ToString.Split(".")
                    'cmbAgeMax_Ex.Text = _age(0) '' SetCombiIndex(cmbAgeMax_Ex)
                    '_ageArrayStr = ageMax.Split(".")
                    'If (_ageArrayStr(1).ToString() = "1") Then
                    '    cmbAgeMaxMnth_Ex.Text = "10"
                    'Else
                    '    cmbAgeMaxMnth_Ex.Text = _age(1)
                    'End If
                End If
            Else
                If Val(ageMaximum) > 0 Then
                    If tab = TabType.Trigger Then
                        cmbAgeMax.Text = ageMaximum '' SetCombiIndex(cmbAgeMax)
                        cmbAgeMaxMnth.Text = "00" '' SetCombiIndex(cmbAgeMaxMnth)
                    ElseIf tab = TabType.Exception Then
                        'cmbAgeMax_Ex.Text = ageMaximum ''SetCombiIndex(cmbAgeMax_Ex)
                        'cmbAgeMaxMnth_Ex.Text = "00" ''SetCombiIndex(cmbAgeMaxMnth_Ex)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _ageArrayStr = Nothing
        End Try
    End Sub

    Public Sub SetHeightMin(ByVal tab As TabType, ByVal heightMinimum As String)
        'Dim arrHeight() As String
        'Try
        '    If tab = TabType.Trigger Then
        '        If heightMinimum.Length > 0 Then
        '            arrHeight = GetFtInch(heightMinimum)
        '            txtHeightMin.Text = arrHeight(0)
        '            txtHeightMinInch.Text = arrHeight(1)
        '        End If
        '    ElseIf tab = TabType.Exception Then
        '        If heightMinimum.Length > 0 Then
        '            arrHeight = GetFtInch(heightMinimum)
        '            '  txtHeightMin_Ex.Text = arrHeight(0)
        '            '  txtHeightMinInch_Ex.Text = arrHeight(1)
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    arrHeight = Nothing
        'End Try
    End Sub

    Public Sub SetHeightMax(ByVal tab As TabType, ByVal heightMaximum As String)
        'Dim arrHeight() As String
        'Try
        '    If tab = TabType.Trigger Then
        '        If heightMaximum.Length > 0 Then
        '            arrHeight = GetFtInch(heightMaximum)
        '            txtHeightMax.Text = arrHeight(0)
        '            txtHeightMaxInch.Text = arrHeight(1)
        '        End If
        '    ElseIf tab = TabType.Exception Then
        '        If heightMaximum.Length > 0 Then
        '            arrHeight = GetFtInch(heightMaximum)
        '            '   txtHeightMax_Ex.Text = arrHeight(0)
        '            '  txtHeightMaxInch_Ex.Text = arrHeight(1)
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    arrHeight = Nothing
        'End Try
    End Sub

    Public Sub SetOtherVitals(ByVal tab As TabType, ByVal Pulse_Minimum As Double, ByVal Pulse_Maximum As Double, ByVal PulseOX_Minimum As Double,
        ByVal PulseOX_Maximum As Double, ByVal BPSitting_Minimum As Double, ByVal BPSitting_Maximum As Double, ByVal BPStanding_Minimum As Double,
        ByVal BPStanding_Maximum As Double, ByVal WeightMinimum As Double, ByVal WeightMaximum As Double, ByVal Temprature_Minimum As Double,
                ByVal Temprature_Maximum As Double, ByVal BMI_Minimum As Double, ByVal BMI_Maximum As Double, ByVal BPSitting_ToMinimum As Double, ByVal BPSitting_ToMaximum As Double, ByVal BPStanding_ToMinimum As Double,
        ByVal BPStanding_ToMaximum As Double)

        Try
            If tab = TabType.Trigger Then

                txtBPsettingMin.Text = ""
                If BPSitting_Minimum <> 0.0 Then
                    txtBPsettingMin.Text = BPSitting_Minimum
                End If

                txtBPsettingMax.Text = ""
                If BPSitting_Maximum <> 0.0 Then
                    txtBPsettingMax.Text = BPSitting_Maximum
                End If

                txtBPsettingMinTo.Text = ""
                If BPSitting_ToMinimum <> 0.0 Then
                    txtBPsettingMinTo.Text = BPSitting_ToMinimum
                End If

                txtBPsettingMaxTo.Text = ""
                If BPSitting_ToMaximum <> 0.0 Then
                    txtBPsettingMaxTo.Text = BPSitting_ToMaximum
                End If

                'txtWeightMin.Text = ""
                'If WeightMinimum <> 0.0 Then
                '    txtWeightMin.Text = WeightMinimum
                'End If

                'txtWeightMax.Text = ""
                'If WeightMaximum <> 0.0 Then
                '    txtWeightMax.Text = WeightMaximum
                'End If

                txtBPstandingMin.Text = ""
                If BPStanding_Minimum <> 0.0 Then
                    txtBPstandingMin.Text = BPStanding_Minimum
                End If

                txtBPstandingMax.Text = ""
                If BPStanding_Maximum <> 0.0 Then
                    txtBPstandingMax.Text = BPStanding_Maximum
                End If

                txtBPstandingMinTo.Text = ""
                If BPStanding_ToMinimum <> 0.0 Then
                    txtBPstandingMinTo.Text = BPStanding_ToMinimum
                End If

                txtBPstandingMaxTo.Text = ""
                If BPStanding_ToMaximum <> 0.0 Then
                    txtBPstandingMaxTo.Text = BPStanding_ToMaximum
                End If

                'txtTemperatureMin.Text = ""
                'If Temprature_Minimum <> 0.0 Then
                '    txtTemperatureMin.Text = Temprature_Minimum
                'End If

                'txtTemperatureMax.Text = ""
                'If Temprature_Maximum <> 0.0 Then
                '    txtTemperatureMax.Text = Temprature_Maximum
                'End If

                'txtPulseMin.Text = ""
                'If Pulse_Minimum <> 0.0 Then
                '    txtPulseMin.Text = Pulse_Minimum
                'End If

                'txtPulseMax.Text = ""
                'If Pulse_Maximum <> 0.0 Then
                '    txtPulseMax.Text = Pulse_Maximum
                'End If

                txtBMImin.Text = ""
                If BMI_Minimum <> 0.0 Then
                    txtBMImin.Text = BMI_Minimum
                End If

                txtBMImax.Text = ""
                If BMI_Maximum <> 0.0 Then
                    txtBMImax.Text = BMI_Maximum
                End If

                'txtPulseOXmin.Text = ""
                'If PulseOX_Minimum <> 0.0 Then
                '    txtPulseOXmin.Text = PulseOX_Minimum
                'End If

                'txtPulseOXmax.Text = ""
                'If PulseOX_Maximum <> 0.0 Then
                '    txtPulseOXmax.Text = PulseOX_Maximum
                'End If

            ElseIf tab = TabType.Exception Then

                'txtBPsettingMin_Ex.Text = ""
                'If BPSitting_Minimum <> 0.0 Then
                '    txtBPsettingMin_Ex.Text = BPSitting_Minimum
                'End If

                'txtBPsettingMax_Ex.Text = ""
                'If BPSitting_Maximum <> 0.0 Then
                '    txtBPsettingMax_Ex.Text = BPSitting_Maximum
                'End If

                'If BPSitting_ToMinimum <> 0.0 Then
                '    txtBPsettingMin_Ex_To.Text = BPSitting_ToMinimum
                'End If

                'txtBPsettingMax_Ex_To.Text = ""
                'If BPSitting_ToMaximum <> 0.0 Then
                '    txtBPsettingMax_Ex_To.Text = BPSitting_ToMaximum
                'End If

                'txtWeightMin_Ex.Text = ""
                'If WeightMinimum <> 0.0 Then
                '    txtWeightMin_Ex.Text = WeightMinimum
                'End If

                'txtWeightMax_Ex.Text = ""
                'If WeightMaximum <> 0.0 Then
                '    txtWeightMax_Ex.Text = WeightMaximum
                'End If

                'txtBPstandingMin_Ex.Text = ""
                'If BPStanding_Minimum <> 0.0 Then
                '    txtBPstandingMin_Ex.Text = BPStanding_Minimum
                'End If

                'txtBPstandingMax_Ex.Text = ""
                'If BPStanding_Maximum <> 0.0 Then
                '    txtBPstandingMax_Ex.Text = BPStanding_Maximum
                'End If


                'txtBPstandingMin_Ex_To.Text = ""
                'If BPStanding_ToMinimum <> 0.0 Then
                '    txtBPstandingMin_Ex_To.Text = BPStanding_ToMinimum
                'End If

                'txtBPstandingMax_Ex_To.Text = ""
                'If BPStanding_ToMaximum <> 0.0 Then
                '    txtBPstandingMax_Ex_To.Text = BPStanding_ToMaximum
                'End If


                'txtTemperatureMin_Ex.Text = ""
                'If Temprature_Minimum <> 0.0 Then
                '    txtTemperatureMin_Ex.Text = Temprature_Minimum
                'End If

                'txtTemperatureMax_Ex.Text = ""
                'If Temprature_Maximum <> 0.0 Then
                '    txtTemperatureMax_Ex.Text = Temprature_Maximum
                'End If

                'txtPulseMin_Ex.Text = ""
                'If Pulse_Minimum <> 0.0 Then
                '    txtPulseMin_Ex.Text = Pulse_Minimum
                'End If

                'txtPulseMax_Ex.Text = ""
                'If Pulse_Maximum <> 0.0 Then
                '    txtPulseMax_Ex.Text = Pulse_Maximum
                'End If

                'txtBMImin_Ex.Text = ""
                'If BMI_Minimum <> 0.0 Then
                '    txtBMImin_Ex.Text = BMI_Minimum
                'End If

                'txtBMImax_Ex.Text = ""
                'If BMI_Maximum <> 0.0 Then
                '    txtBMImax_Ex.Text = BMI_Maximum
                'End If

                'txtPulseOXmin_Ex.Text = ""
                'If PulseOX_Minimum <> 0.0 Then
                '    txtPulseOXmin_Ex.Text = PulseOX_Minimum
                'End If

                'txtPulseOXmax_Ex.Text = ""
                'If PulseOX_Maximum <> 0.0 Then
                '    txtPulseOXmax_Ex.Text = PulseOX_Maximum
                'End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    'Public Sub SetInsuranceNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

    '    Dim myInsuranceNode As myTreeNode = Nothing
    '    Dim _listviewItem As ListViewItem = Nothing

    '    Try

    '        myInsuranceNode = New myTreeNode

    '        If item.ItemName <> "" Then
    '            myInsuranceNode.Text = item.ItemName
    '        Else
    '            myInsuranceNode.Text = item.ItemName
    '        End If

    '        myInsuranceNode.Tag = item.CategoryName
    '        myInsuranceNode.DrugName = item.ItemName
    '        myInsuranceNode.Dosage = item.ItemName
    '        myInsuranceNode.NDCCode = item.Result1


    '        _listviewItem = New ListViewItem
    '        _listviewItem.Text = myInsuranceNode.Text
    '        _listviewItem.Tag = myInsuranceNode

    '        If tab = TabType.Trigger Then
    '            trvSelectedInsurance.Nodes.Add(myInsuranceNode)
    '            lstVw_Insurance.Items.Add(_listviewItem)
    '        ElseIf tab = TabType.Exception Then
    '            trvSelectedInsurance_Ex.Nodes.Add(myInsuranceNode)
    '            lstExVw_Insurance.Items.Add(_listviewItem)
    '        End If

    '        _listviewItem = Nothing

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        myInsuranceNode = Nothing
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Public Sub SetDrugNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

    '    Dim myDrugNode As myTreeNode = Nothing
    '    Dim _listviewItem As ListViewItem = Nothing

    '    Try

    '        myDrugNode = New myTreeNode

    '        myDrugNode.Text = item.CategoryName

    '        myDrugNode.Tag = item.ItemName
    '        myDrugNode.DrugName = item.CategoryName
    '        myDrugNode.Dosage = item.ItemName
    '        myDrugNode.NDCCode = item.Result1
    '        myDrugNode.DDID = item.Result2

    '        _listviewItem = New ListViewItem
    '        _listviewItem.Text = myDrugNode.Text
    '        _listviewItem.Tag = myDrugNode

    '        If tab = TabType.Trigger Then
    '            trvSelectedDrugs.Nodes.Add(myDrugNode)
    '            lstVw_Drugs.Items.Add(_listviewItem)
    '        ElseIf tab = TabType.Exception Then
    '            trvSelectedDrugs_Ex.Nodes.Add(myDrugNode)
    '            lstExVw_Drugs.Items.Add(_listviewItem)
    '        End If

    '        _listviewItem = Nothing

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        myDrugNode = Nothing
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Public Sub SetHistoryNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

    '    Dim _listviewItem As ListViewItem = Nothing
    '    Dim CategoryFound As Boolean = False
    '    Dim HistoryFound As Boolean = False
    '    Dim oCategoryNode As TreeNode = Nothing
    '    Dim oHistoryNode As myTreeNode = Nothing

    '    Try

    '        If tab = TabType.Trigger Then

    '            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
    '                If CategoryNode.Text = item.CategoryName Then

    '                    oHistoryNode = New myTreeNode
    '                    oHistoryNode.Text = item.ItemName
    '                    oHistoryNode.Tag = item.CategoryName
    '                    CategoryNode.Nodes.Add(oHistoryNode)
    '                    CategoryNode.Expand()

    '                    _listviewItem = New ListViewItem
    '                    _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
    '                    _listviewItem.Tag = oHistoryNode
    '                    lstVw_History.Items.Add(_listviewItem)

    '                    _listviewItem = Nothing
    '                    oHistoryNode = Nothing
    '                    CategoryFound = True
    '                    Exit For
    '                End If
    '            Next

    '            If Not CategoryFound Then
    '                oCategoryNode = New TreeNode
    '                oHistoryNode = New myTreeNode
    '                oCategoryNode.Text = item.CategoryName
    '                oCategoryNode.ImageIndex = 0
    '                oCategoryNode.SelectedImageIndex = 0
    '                oHistoryNode.Text = item.ItemName
    '                oHistoryNode.Tag = item.CategoryName
    '                oCategoryNode.Nodes.Add(oHistoryNode)
    '                trvSelectedHistory.Nodes.Add(oCategoryNode)
    '                _listviewItem = New ListViewItem
    '                _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
    '                _listviewItem.Tag = oHistoryNode
    '                lstVw_History.Items.Add(_listviewItem)
    '            End If

    '            trvSelectedHistory.ExpandAll()

    '        ElseIf tab = TabType.Exception Then

    '            For Each CategoryNode As TreeNode In trvSelectedHistory_Ex.Nodes
    '                If CategoryNode.Text = item.CategoryName Then

    '                    oHistoryNode = New myTreeNode
    '                    oHistoryNode.Text = item.ItemName
    '                    oHistoryNode.Tag = item.CategoryName
    '                    CategoryNode.Nodes.Add(oHistoryNode)
    '                    CategoryNode.Expand()
    '                    _listviewItem = New ListViewItem
    '                    _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
    '                    _listviewItem.Tag = oHistoryNode
    '                    lstExVw_History.Items.Add(_listviewItem)
    '                    _listviewItem = Nothing
    '                    oHistoryNode = Nothing
    '                    CategoryFound = True
    '                    Exit For
    '                End If
    '            Next

    '            If Not CategoryFound Then
    '                oCategoryNode = New TreeNode
    '                oHistoryNode = New myTreeNode
    '                oCategoryNode.Text = item.CategoryName
    '                oCategoryNode.ImageIndex = 0
    '                oCategoryNode.SelectedImageIndex = 0
    '                oHistoryNode.Text = item.ItemName
    '                oHistoryNode.Tag = item.CategoryName
    '                oCategoryNode.Nodes.Add(oHistoryNode)
    '                trvSelectedHistory_Ex.Nodes.Add(oCategoryNode)
    '                _listviewItem = New ListViewItem
    '                _listviewItem.Text = oHistoryNode.Tag + " - " + oHistoryNode.Text
    '                _listviewItem.Tag = oHistoryNode
    '                lstExVw_History.Items.Add(_listviewItem)
    '            End If
    '            trvSelectedHistory_Ex.ExpandAll()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        oCategoryNode = Nothing
    '        oHistoryNode = Nothing
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetICDNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

    '    Dim myICDNode As myTreeNode = Nothing
    '    Dim _listviewItem As ListViewItem = Nothing

    '    Try
    '        myICDNode = New myTreeNode
    '        myICDNode.Text = item.CategoryName + " - " + item.ItemName
    '        myICDNode.Tag = item.CategoryName
    '        myICDNode.DrugName = item.ItemName

    '        _listviewItem = New ListViewItem
    '        _listviewItem.Text = myICDNode.Text
    '        _listviewItem.Tag = myICDNode

    '        If tab = TabType.Trigger Then
    '            trvselecteICDs.Nodes.Add(myICDNode)
    '            lstVw_ICD9.Items.Add(_listviewItem)
    '        ElseIf tab = TabType.Exception Then
    '            trvselectedICDs_Ex.Nodes.Add(myICDNode)
    '            lstExVw_ICD.Items.Add(_listviewItem)
    '        End If

    '        _listviewItem = Nothing

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        myICDNode = Nothing
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetICD10Node(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)

    '    Dim myICDNode As myTreeNode = Nothing
    '    Dim _listviewItem As ListViewItem = Nothing

    '    Try
    '        myICDNode = New myTreeNode
    '        myICDNode.Text = item.CategoryName + " - " + item.ItemName
    '        myICDNode.Tag = item.CategoryName
    '        myICDNode.DrugName = item.ItemName

    '        _listviewItem = New ListViewItem
    '        _listviewItem.Text = myICDNode.Text
    '        _listviewItem.Tag = myICDNode

    '        If tab = TabType.Trigger Then
    '            trvselecteICD10s.Nodes.Add(myICDNode)
    '            lstVw_ICD10.Items.Add(_listviewItem)
    '        ElseIf tab = TabType.Exception Then
    '            trvselectedICD10s_Ex.Nodes.Add(myICDNode)
    '            lstExVw_ICD10.Items.Add(_listviewItem)
    '        End If

    '        _listviewItem = Nothing

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        myICDNode = Nothing
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetCPTNode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Dim myCPTNode As myTreeNode = Nothing
    '    Dim _listviewItem As ListViewItem = Nothing

    '    Try
    '        myCPTNode = New myTreeNode
    '        myCPTNode.Text = item.CategoryName.Trim() + " - " + item.ItemName
    '        myCPTNode.Tag = item.CategoryName
    '        myCPTNode.DrugName = item.ItemName

    '        _listviewItem = New ListViewItem
    '        _listviewItem.Text = myCPTNode.Text
    '        _listviewItem.Tag = myCPTNode
    '        If tab = TabType.Trigger Then
    '            trvselectedCPT.Nodes.Add(myCPTNode)
    '            lstVw_CPT.Items.Add(_listviewItem)
    '        ElseIf tab = TabType.Exception Then
    '            trvselectedCPT_Ex.Nodes.Add(myCPTNode)
    '            lstExVw_CPT.Items.Add(_listviewItem)
    '        End If
    '        _listviewItem = Nothing

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        myCPTNode = Nothing
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetOrder(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Dim _listviewItem As ListViewItem = Nothing
    '    Try
    '        If tab = TabType.Trigger Then
    '            For j As Integer = 1 To c1Labs.Rows.Count - 1
    '                Dim _TestCell As String = c1Labs.GetData(j, COL_IDENTITY) & ""
    '                If Mid(_TestCell, 1, 1) = "T" Then
    '                    If c1Labs.Rows(j).Node.Data = item.ItemName And
    '                        c1Labs.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.OperatorName And
    '                        c1Labs.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data = item.CategoryName Then

    '                        c1Labs.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

    '                        _listviewItem = New ListViewItem
    '                        _listviewItem.Text = c1Labs.Rows(j).Node.Data
    '                        lstVw_Orders.Items.Add(_listviewItem)
    '                        _listviewItem = Nothing

    '                    End If
    '                End If
    '            Next
    '        ElseIf tab = TabType.Exception Then
    '            For j As Integer = 1 To c1Labs_Ex.Rows.Count - 1
    '                Dim _TestCell As String = c1Labs_Ex.GetData(j, COL_IDENTITY) & ""
    '                If Mid(_TestCell, 1, 1) = "T" Then
    '                    If c1Labs_Ex.Rows(j).Node.Data = item.ItemName And
    '                        c1Labs_Ex.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.OperatorName And
    '                        c1Labs_Ex.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Root).Data = item.CategoryName Then

    '                        c1Labs_Ex.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

    '                        _listviewItem = New ListViewItem
    '                        _listviewItem.Text = c1Labs_Ex.Rows(j).Node.Data
    '                        lstExVw_Orders.Items.Add(_listviewItem)
    '                        _listviewItem = Nothing
    '                    End If
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetLabs(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Dim _listviewItem As ListViewItem = Nothing
    '    Try
    '        If tab = TabType.Trigger Then
    '            For j As Integer = 1 To C1LabResult.Rows.Count - 1
    '                If C1LabResult.Rows(j).Node.Level = 1 Then
    '                    If C1LabResult.Rows(j).Node.Data = item.ItemName And C1LabResult.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.CategoryName Then
    '                        C1LabResult.SetCellCheck(j, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Checked)
    '                        C1LabResult.SetData(j, COL_Operator, item.OperatorName)
    '                        C1LabResult.SetData(j, COL_ResultValue1, item.Result1)
    '                        C1LabResult.SetData(j, COL_ResultValue2, item.Result2)
    '                        _listviewItem = New ListViewItem
    '                        _listviewItem.Text = C1LabResult.Rows(j).Node.Data
    '                        lstVw_Lab.Items.Add(_listviewItem)
    '                        _listviewItem = Nothing
    '                    End If
    '                End If
    '            Next
    '        ElseIf tab = TabType.Exception Then
    '            For j As Integer = 1 To C1LabResult_Ex.Rows.Count - 1
    '                If C1LabResult_Ex.Rows(j).Node.Level = 1 Then
    '                    If C1LabResult_Ex.Rows(j).Node.Data = item.ItemName And C1LabResult_Ex.Rows(j).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data = item.CategoryName Then
    '                        C1LabResult_Ex.SetCellCheck(j, COL_TestName, C1.Win.C1FlexGrid.CheckEnum.Checked)
    '                        C1LabResult_Ex.SetData(j, COL_Operator, item.OperatorName)
    '                        C1LabResult_Ex.SetData(j, COL_ResultValue1, item.Result1)
    '                        C1LabResult_Ex.SetData(j, COL_ResultValue2, item.Result2)
    '                        _listviewItem = New ListViewItem
    '                        _listviewItem.Text = C1LabResult_Ex.Rows(j).Node.Data
    '                        lstExVw_Lab.Items.Add(_listviewItem)
    '                        _listviewItem = Nothing
    '                    End If
    '                End If
    '            Next
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        _listviewItem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    Private Sub SetLabs_ByTable()
        Dim _listviewItem As ListViewItem = Nothing
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing

        Try
            _dt = CType(C1LabResult.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
            End If
            If Not IsNothing(_dv) Then
                _dtFilter = _dv.ToTable().Copy()
            End If
            lstVw_Lab.Items.Clear()
            If Not IsNothing(_dtFilter) Then
                For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Result").ToString() + "   " + _dtFilter.Rows(_rowIndex)("Operator") + " " + _dtFilter.Rows(_rowIndex)("Result Value1") + " " + _dtFilter.Rows(_rowIndex)("Result Value2")
                    _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
                    lstVw_Lab.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
                _dtFilter = Nothing
            End If
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Sub

    Private Sub SetEXLabs_ByTable()
        'Dim _listviewItem As ListViewItem = Nothing
        'Dim _dv As DataView = Nothing
        'Dim _dt As DataTable = Nothing
        'Dim _dtFilter As DataTable = Nothing

        'Try
        '    _dt = CType(C1LabResult_Ex.DataSource, DataTable)
        '    _dtFilter = _dt.Copy()

        '    If Not IsNothing(_dt) AndAlso _dtFilter.Rows.Count > 0 Then
        '        _dv = _dtFilter.DefaultView
        '        _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
        '    End If

        '    If Not IsNothing(_dtFilter) Then
        '        _dtFilter.Dispose()
        '    End If
        '    If Not IsNothing(_dv) Then
        '        _dtFilter = _dv.ToTable().Copy()
        '    End If
        '    lstExVw_Lab.Items.Clear()
        '    If Not IsNothing(_dtFilter) Then
        '        For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
        '            _listviewItem = New ListViewItem
        '            _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Result").ToString() + "   " + _dtFilter.Rows(_rowIndex)("Operator") + " " + _dtFilter.Rows(_rowIndex)("Result Value1") + " " + _dtFilter.Rows(_rowIndex)("Result Value2")
        '            _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
        '            lstExVw_Lab.Items.Add(_listviewItem)
        '            _listviewItem = Nothing
        '        Next
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    _listviewItem = Nothing
        '    '_dt.Dispose()
        '    '_dt = Nothing
        '    If Not IsNothing(_dv) Then
        '        _dv.Dispose()
        '        _dv = Nothing
        '    End If
        '    If Not IsNothing(_dtFilter) Then
        '        _dtFilter.Dispose()
        '        _dtFilter = Nothing
        '    End If
        '    ''dtFilter datatable can't disposed because it has been assigned to dataview _dv 
        'End Try
    End Sub

    Private Sub SetOrders_ByTable()
        Dim _listviewItem As ListViewItem = Nothing
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing

        Try
            _dt = CType(c1Labs.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            _dtFilter.Dispose()
            _dtFilter = _dv.ToTable().Copy()

            '  lstVw_Orders.Items.Clear()

            'For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
            '    _listviewItem = New ListViewItem
            '    _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Test").ToString()
            '    _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
            '    lstVw_Orders.Items.Add(_listviewItem)
            '    _listviewItem = Nothing
            'Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _listviewItem = Nothing
            '_dt.Dispose()
            '_dt = Nothing
            If Not IsNothing(_dtFilter) Then
                _dtFilter.Dispose()
                _dtFilter = Nothing
            End If
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Sub

    Private Sub SetExOrders_ByTable()
        'Dim _listviewItem As ListViewItem = Nothing
        'Dim _dv As DataView = Nothing
        'Dim _dt As DataTable = Nothing
        'Dim _dtFilter As DataTable = Nothing
        'Try
        '    _dt = CType(c1Labs_Ex.DataSource, DataTable)
        '    _dtFilter = _dt.Copy()

        '    If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
        '        _dv = _dtFilter.DefaultView
        '        _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
        '    End If

        '    If Not IsNothing(_dtFilter) Then
        '        _dtFilter.Dispose()

        '    End If
        '    If Not IsNothing(_dv) Then
        '        _dtFilter = _dv.ToTable().Copy()
        '    End If

        '    lstExVw_Orders.Items.Clear()
        '    If Not IsNothing(_dtFilter) Then
        '        For _rowIndex As Integer = 0 To _dtFilter.Rows.Count - 1
        '            _listviewItem = New ListViewItem
        '            _listviewItem.Text = _dtFilter.Rows(_rowIndex)("Test").ToString()
        '            _listviewItem.Tag = _dtFilter.Rows(_rowIndex)
        '            lstExVw_Orders.Items.Add(_listviewItem)
        '            _listviewItem = Nothing
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    _listviewItem = Nothing
        '    '_dt.Dispose()
        '    '_dt = Nothing
        '    If Not IsNothing(_dtFilter) Then
        '        _dtFilter.Dispose()
        '        _dtFilter = Nothing
        '    End If
        '    If Not IsNothing(_dv) Then
        '        _dv.Dispose()
        '        _dv = Nothing
        '    End If
        'End Try
    End Sub

    Private Function getSlectedLabAndResult(ByRef c1FlexGrid) As DataTable
        Dim _dv As DataView = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dtFilter As DataTable = Nothing
        Try
            _dt = CType(c1FlexGrid.DataSource, DataTable)
            _dtFilter = _dt.Copy()

            If Not IsNothing(_dtFilter) AndAlso _dtFilter.Rows.Count > 0 Then
                _dv = _dtFilter.DefaultView
                _dv.RowFilter = _dv.Table.Columns("Value").ColumnName + "=1"
            End If

            If Not IsNothing(_dv) Then
                _dtFilter = _dv.ToTable().Copy()
            End If
            Return _dtFilter
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            _dt = Nothing
            If Not IsNothing(_dv) Then
                _dv.Dispose()
                _dv = Nothing
            End If
        End Try
    End Function

    'Private Sub SetRace(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Try
    '        If tab = TabType.Trigger Then
    '            For iCount As Integer = 1 To cmbChkBoxRace.CheckBoxItems.Count - 1
    '                If cmbChkBoxRace.CheckBoxItems(iCount).Text = item.ItemName Then
    '                    cmbChkBoxRace.CheckBoxItems(iCount).Checked = True
    '                End If
    '            Next
    '        ElseIf tab = TabType.Exception Then
    '            For iCount As Integer = 1 To cmbChkBoxRace_Ex.CheckBoxItems.Count - 1
    '                If cmbChkBoxRace_Ex.CheckBoxItems(iCount).Text = item.ItemName Then
    '                    cmbChkBoxRace_Ex.CheckBoxItems(iCount).Checked = True
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetMaritalStatus(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Try
    '        If tab = TabType.Trigger Then
    '            For iCount As Integer = 1 To cmbChkBoxMaritalSt.CheckBoxItems.Count - 1
    '                If cmbChkBoxMaritalSt.CheckBoxItems(iCount).Text = item.ItemName Then
    '                    cmbChkBoxMaritalSt.CheckBoxItems(iCount).Checked = True
    '                End If
    '            Next
    '        ElseIf tab = TabType.Exception Then
    '            For iCount As Integer = 1 To cmbChkBoxMaritalSt_Ex.CheckBoxItems.Count - 1
    '                If cmbChkBoxMaritalSt_Ex.CheckBoxItems(iCount).Text = item.ItemName Then
    '                    cmbChkBoxMaritalSt_Ex.CheckBoxItems(iCount).Checked = True
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetGender(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Try
    '        If tab = TabType.Trigger Then
    '            For iCount As Integer = 1 To cmbChkBoxGender.CheckBoxItems.Count - 1
    '                If cmbChkBoxGender.CheckBoxItems(iCount).Text = item.ItemName Then
    '                    cmbChkBoxGender.CheckBoxItems(iCount).Checked = True
    '                End If
    '            Next
    '        ElseIf tab = TabType.Exception Then
    '            For iCount As Integer = 1 To cmbGender_Ex.CheckBoxItems.Count - 1
    '                If cmbGender_Ex.CheckBoxItems(iCount).Text = item.ItemName Then
    '                    cmbGender_Ex.CheckBoxItems(iCount).Checked = True
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        item = Nothing
    '    End Try
    'End Sub

    'Private Sub SetSnomedCode(ByVal tab As TabType, ByVal item As Supporting.OtherDetail)
    '    Dim _lstVwitem As ListViewItem = Nothing
    '    Try
    '        _lstVwitem = New ListViewItem()
    '        _lstVwitem.Text = item.ItemName
    '        _lstVwitem.Tag = item.CategoryName
    '        _lstVwitem.SubItems.Add(item.ItemName)

    '        If tab = TabType.Trigger Then
    '            AddItemToList(lstVw_SnoMed, _lstVwitem)
    '        ElseIf tab = TabType.Exception Then
    '            AddItemToList(lstExVw_SnoMed, _lstVwitem)
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        _lstVwitem = Nothing
    '        item = Nothing
    '    End Try
    'End Sub

    Private Sub SetQuickOrder(ByVal collections As Collection, ByVal quickOrderType As QuickOrderType)
        'Dim _myTreeNode As myTreeNode = Nothing
        ''  Dim objList As myList = Nothing
        'Try
        '    If Not IsNothing(collections) AndAlso collections.Count > 0 Then
        '        For itemIndex As Integer = 1 To collections.Count
        '            objList = CType(collections(itemIndex), myList)

        '            Select Case (quickOrderType)
        '                Case quickOrderType.LabOrders
        '                    _myTreeNode = New myTreeNode(objList.Value, objList.ID)
        '                    If Not IsNothing(_myTreeNode) Then
        '                        AddAssociates(_myTreeNode, "Orders")
        '                        _myTreeNode.Dispose()
        '                        _myTreeNode = Nothing
        '                    End If

        '                Case quickOrderType.RadiologyOrders
        '                    _myTreeNode = New myTreeNode(objList.Value, objList.ID)
        '                    If Not IsNothing(_myTreeNode) Then
        '                        AddAssociates(_myTreeNode, "Order Templates")
        '                        _myTreeNode.Dispose()
        '                        _myTreeNode = Nothing
        '                    End If

        '                Case quickOrderType.Referrals
        '                    _myTreeNode = New myTreeNode(objList.Value, objList.ID)
        '                    If Not IsNothing(_myTreeNode) Then
        '                        AddAssociates(_myTreeNode, "Referrals")
        '                        _myTreeNode.Dispose()
        '                        _myTreeNode = Nothing
        '                    End If

        '                Case quickOrderType.RxDrugs
        '                    _myTreeNode = New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage, objList.DrugForm, objList.Route, objList.Frequency, objList.NDCCode, objList.IsNarcotic, objList.Duration, objList.mpid, objList.DrugQtyQualifier)
        '                    If Not IsNothing(_myTreeNode) Then
        '                        AddAssociates(_myTreeNode, "Rx")
        '                        _myTreeNode.Dispose()
        '                        _myTreeNode = Nothing
        '                    End If

        '                Case quickOrderType.Guidelines
        '                    _myTreeNode = New myTreeNode()
        '                    _myTreeNode.Text = objList.DMTemplateName
        '                    _myTreeNode.Tag = objList.ID
        '                    If Not IsNothing(objList.DMTemplate) Then
        '                        _myTreeNode.TemplateResult = objList.DMTemplate
        '                    Else
        '                        _myTreeNode.TemplateResult = Nothing
        '                    End If

        '                    If Not IsNothing(_myTreeNode) Then
        '                        AddAssociates(_myTreeNode, "Guidelines")
        '                        _myTreeNode.Dispose()
        '                        _myTreeNode = Nothing
        '                    End If

        '                Case quickOrderType.IM
        '                    blnloadIm = True
        '                    _myTreeNode = New myTreeNode()
        '                    _myTreeNode.Text = objList.DMTemplateName ''Vaccine Name
        '                    _myTreeNode.Tag = objList.ID                  'IM ID
        '                    _myTreeNode.Key = objList.ID                  'IM ID
        '                    _myTreeNode.DrugForm = objList.DrugForm ''Vaccine Code
        '                    _myTreeNode.Route = objList.Route ''SKU
        '                    _myTreeNode.DrugName = objList.Code
        '                    _myTreeNode.Dosage = objList.Description
        '                    _myTreeNode.Frequency = objList.Frequency ''TradeName
        '                    _myTreeNode.NDCCode = objList.NDCCode ''Manufaturer
        '                    _myTreeNode.IsNarcotics = objList.IsNarcotic
        '                    _myTreeNode.Duration = objList.Duration ''Lot Number

        '                    _myTreeNode.TemplateResult = Nothing
        '                    If Not IsNothing(_myTreeNode) Then
        '                        AddAssociates(_myTreeNode, "IM")
        '                        _myTreeNode.Dispose()
        '                        _myTreeNode = Nothing
        '                    End If
        '            End Select
        '            objList = Nothing
        '        Next
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    blnloadIm = False
        '    _myTreeNode = Nothing
        '    objList = Nothing
        'End Try
    End Sub

    'Private Sub SetOtherInformation(ByVal otherDetailList As Supporting.OtherDetails, ByVal tab As TabType)
    '    Try
    '        For i As Integer = 1 To otherDetailList.Count

    '            Select Case (otherDetailList.Item(i).DetailType)

    '                Case Supporting.enumDetailType.Medication
    '                    SetDrugNode(tab, otherDetailList.Item(i))
    '                Case Supporting.enumDetailType.Insurance
    '                    SetInsuranceNode(tab, otherDetailList.Item(i))
    '                Case Supporting.enumDetailType.History
    '                    SetHistoryNode(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.ICD9
    '                    SetICDNode(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.ICD10
    '                    SetICD10Node(tab, otherDetailList.Item(i))

    '                Case (Supporting.enumDetailType.CPT)
    '                    SetCPTNode(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.Order
    '                    '' SetOrder(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.Lab
    '                    ''  SetLabs(tab, otherDetailList.Item(i))
    '                    '' SetLabs_ByTable(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.Race
    '                    SetRace(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.MaritalStatus
    '                    SetMaritalStatus(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.Gender
    '                    SetGender(tab, otherDetailList.Item(i))

    '                Case Supporting.enumDetailType.SnomedCode
    '                    SetSnomedCode(tab, otherDetailList.Item(i))

    '            End Select

    '        Next '......looping ....For i As Integer = 1 To otherDetailList.Count
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        otherDetailList.Clear()
    '        otherDetailList = Nothing
    '    End Try
    'End Sub

    Private Sub Search_TextChanged(ByVal c1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strSearch As String)
        Dim sFilter As String = ""
        Dim _dv As DataView = Nothing
        Dim strSearchArray As String() = Nothing

        Try
            _dv = DirectCast(c1FlexGrid.DataSource, DataTable).DefaultView
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If strSearch.Trim() <> "" Then
                strSearchArray = strSearch.Split(","c)
            End If

            If strSearch.Trim() <> "" Then
                If strSearchArray.Length = 1 Then
                    'For Single value search 
                    strSearch = strSearchArray(0).Trim()
                    If strSearch.Length > 1 Then
                        Dim str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) & str
                    End If

                    _dv.RowFilter = _dv.Table.Columns("Test").ColumnName + " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Result").ColumnName & " Like '" & strSearch & "%' "
                Else
                    'For Comma separated  value search
                    For i As Integer = 0 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i).Trim()
                        If strSearch.Length > 1 Then
                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) & str
                        End If

                        If strSearch.Trim() <> "" Then
                            If sFilter = "" Then
                                '(i == 0)
                                sFilter = "(" + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Result").ColumnName & " Like '" & strSearch & "%' )"
                            Else
                                sFilter = sFilter & " AND (" + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Result").ColumnName & " Like '" & strSearch & "%')"
                            End If
                        End If
                    Next

                    _dv.RowFilter = sFilter
                End If
            Else
                _dv.RowFilter = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            strSearchArray = Nothing
        End Try
    End Sub

    Private Sub Search_Order_TextChanged(ByVal c1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal strSearch As String)
        Dim sFilter As String = ""
        Dim _dv As DataView = Nothing
        Dim strSearchArray As String() = Nothing
        Try
            _dv = DirectCast(c1FlexGrid.DataSource, DataTable).DefaultView
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%")

            If strSearch.Trim() <> "" Then
                strSearchArray = strSearch.Split(","c)
            End If

            If strSearch.Trim() <> "" Then
                If strSearchArray.Length = 1 Then
                    'For Single value search 
                    strSearch = strSearchArray(0).Trim()
                    If strSearch.Length > 1 Then
                        Dim str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) & str
                    End If
                    _dv.RowFilter = _dv.Table.Columns("Category").ColumnName + " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' "
                Else
                    'For Comma separated  value search
                    For i As Integer = 0 To strSearchArray.Length - 1
                        strSearch = strSearchArray(i).Trim()

                        If strSearch.Length > 1 Then
                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) & str
                        End If

                        If strSearch.Trim() <> "" Then
                            If sFilter = "" Then
                                '(i == 0)
                                sFilter = "(" + _dv.Table.Columns("Category").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%' )"
                            Else
                                sFilter = sFilter & " AND (" + _dv.Table.Columns("Category").ColumnName & " Like '" & strSearch & "%' OR " + _dv.Table.Columns("Test").ColumnName & " Like '" & strSearch & "%')"
                            End If
                        End If
                    Next
                    _dv.RowFilter = sFilter
                End If
            Else
                _dv.RowFilter = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            strSearchArray = Nothing
        End Try

    End Sub

#End Region ' Criteria/Rule set methods '

#Region "Fill Methods"


    Private Sub fill_Insurance()

    End Sub
    Private Sub fill_Drugs()

    End Sub

    Private Sub Fill_Histories_1(Optional ByVal HistoryCategory As String = "")

    End Sub

    Private Sub fill_CPTs(ByVal strSortBy As String)

    End Sub

    Private Sub Fill_ICD9s(ByVal strsearch As String)

    End Sub

    Private Sub Fill_ICD10s(ByVal strsearch As String)

    End Sub
#End Region

    'Private Sub GloUC_trvHistory_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvHistory.NodeAdded
    'If IsNothing(ChildNode.Tag) = False Then
    '    If Convert.ToString(ChildNode.Tag) <> "" Then
    '        If ChildNode.Tag = 1 Then
    '            ChildNode.ImageIndex = 11
    '            ChildNode.SelectedImageIndex = 11
    '        End If
    '    End If
    'End If
    'End Sub


End Class



