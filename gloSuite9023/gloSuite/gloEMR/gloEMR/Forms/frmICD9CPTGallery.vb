

Public Class frmICD9CPTGallery
    Dim oICD9CPT As DBICD9CPT
    Private oclsICD9 As New clsICD9
    Dim dtICD9Gallery As DataTable
    Dim dtCPTGallery As DataTable
    Dim dtCurrentICD9 As DataTable
    Dim dtCurrentCPT As DataTable
    Dim dtspeciality As DataTable
    Dim dtCategory As DataTable
    Dim DisplayMember As String = ""
    Dim ValuMember As String = ""

    Dim ICD9ID As Int64
    Dim CPTID As Int64

    Dim ICD9Code As String = ""
    Dim ICD9Description As String = ""
    Dim ICD9SpecialityID As Int64


    Dim strCodeDescr() As String
    Dim dvSpeciality As DataView
    Dim dvCategory As DataView
    Dim dtICD9 As DataTable
    Dim dtCPT As DataTable

    Dim strMyTargetNodeICD9() As String
    Dim strMyNodeICD9() As String
    Dim strMyTargetNodeCPT() As String
    Dim strMyNodeCPT() As String
    Dim IsICD9Present As Boolean = False
    Dim IsCPTPresent As Boolean = False


    Dim CPTCode As String = ""
    Dim CPTDescription As String = ""
    Dim CPTSpecialityID As Int64
    Dim CPTCategoryID As Int64
    Dim strCPTCodeDescr() As String
    Dim oclsCPT As New clsCPT

    Dim IsICD9Update As Boolean = False
    Dim IsCPTUpdate As Boolean = False


    Dim ICD9GalleryCount As Int64 = 0
    Dim CPTGalleryCount As Int64 = 0
    Dim strIndicator As String = ""
    'Variables Declare by Mayuri:20091008
    'To check whether selected node to remove is from ICD9Gallery or CurrentICD9
    Dim _isSelectedCPTGallery As Boolean = False
    'To check whether selected node to remove is from CPTGallery or CurrentCPT
    Private _isSelectedICD9Gallery As Boolean = False
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property
    Private Sub frmICD9CPTGallery_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub
    'End Variable Declaration by Mayuri:20091008 
    '14-01-2011'GLO2010-0005444'ICD9 code icons are not correct in icd9cpt gallery
    Private Sub glo_trvICD9Gallery_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles gloTrvICD9Gallery.NodeAdded
        Try
            If Not IsNothing(cmbICD9Gallery) Then
                If cmbICD9Gallery.Text = "All" Then
                    If Not IsNothing(ChildNode) Then
                        If (ChildNode.Indicator = "N") Then ''NEW
                            ChildNode.ImageIndex = 8
                            ChildNode.SelectedImageIndex = 8
                        ElseIf (ChildNode.Indicator = "R") Then 'REVISED'
                            ChildNode.ImageIndex = 9
                            ChildNode.SelectedImageIndex = 9
                        ElseIf (ChildNode.Indicator <> "R" And ChildNode.Indicator <> "N") Then ''NOT [NEW and REVISED]
                            ChildNode.ImageIndex = 4
                            ChildNode.SelectedImageIndex = 4
                        End If
                    Else
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Child node is null", False)
                    End If
                ElseIf cmbICD9Gallery.Text = "New" Then  ''NEW
                    If Not IsNothing(gloTrvICD9Gallery) Then
                        gloTrvICD9Gallery.ImageIndex = 8
                        gloTrvICD9Gallery.SelectedImageIndex = 8
                    Else
                        gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", False)
                    End If
                ElseIf cmbICD9Gallery.Text = "Revised" Then  'REVISED'
                    If Not IsNothing(gloTrvICD9Gallery) Then
                        gloTrvICD9Gallery.ImageIndex = 9
                        gloTrvICD9Gallery.SelectedImageIndex = 9
                    Else
                        gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", False)

                    End If
                ElseIf cmbICD9Gallery.Text = "No Change" Then 'NOT [NEW and REVISED]''IT Could be blank ""'
                    If Not IsNothing(gloTrvICD9Gallery) Then
                        gloTrvICD9Gallery.ImageIndex = 4
                        gloTrvICD9Gallery.SelectedImageIndex = 4
                    Else
                        gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", False)
                    End If
                    End If
                End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub



    Private Sub frmICD9CPTGallery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblCopyRight.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain
            lblCopyRight.Refresh()
            Me.Cursor = Cursors.WaitCursor
            If (cmbICD9Gallery.Items.Count > 0) Then
                cmbICD9Gallery.SelectedIndex = 0
            End If


            'gloAuditTrail.gloAuditTrail.UpdatePILog("Start Load")
            cmbICD9Gallery.Select()

            'cmbSpecialityICD9.SelectedIndex = 0
            'cmbSpecialityICD9.Text = ""
            'cmbSpecialityICD9.Select()

            oICD9CPT = New DBICD9CPT
            dtICD9Gallery = New DataTable
            strIndicator = Trim(cmbICD9Gallery.Text)
            dtICD9Gallery = oICD9CPT.GetICD9Gallery(1, strIndicator)
            If Not IsNothing(dtICD9Gallery) Then
                FillICD9GalleryTrv(dtICD9Gallery)
            End If
            'Code added by Mayuri:20091003 to fill current ICD9 treeview
            FillCurrentICD9(cmbSpecialityICD9.Text)

            ''Dhruv 20100104 to fill currrent ICD9CPT treeview
            FillCurrentICD9CPT(cmbSpecialityICD9CPT.Text)

            ''Dhruv Commented 20100104''
            'Code added by Mayuri:20091003 to fill current CPT treeview
            ''FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
            'End Code Added by Mayuri:20091003

            ''Dhruv 20100104 
            FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)


            Dim dtSpecialityCPT As DataTable
            oclsICD9 = New clsICD9
            dtspeciality = New DataTable
            dtspeciality = oclsICD9.GetAllSpeciality()
            dtSpecialityCPT = oclsICD9.GetAllSpeciality()
            'Code Added by Mayuri:20091005
            'To add Speciality "ALL" in Speciality combobox,inorder display All Data in Treeview Control
            Dim allRow As DataRow
            allRow = dtspeciality.NewRow()
            allRow("nSpecialtyId") = 0
            allRow("sDescription") = "All"
            dtspeciality.Rows.Add(allRow)


            ''Dhruv 20100104
            ''adding the new name as "all" under combobox
            Dim allRowCPT As DataRow
            allRowCPT = dtSpecialityCPT.NewRow()
            allRowCPT("nSpecialtyId") = 0
            allRowCPT("sDescription") = "All"
            dtSpecialityCPT.Rows.Add(allRowCPT)
            'End Code Added by Mayuri:20091005

            If ((Not IsNothing(dtspeciality)) AndAlso (Not IsNothing(dtSpecialityCPT))) Then
                'FillICD9Trv(dtspeciality)
                If ((IsNothing(dtspeciality) = False) AndAlso (IsNothing(dtSpecialityCPT) = False)) Then
                    If ((dtspeciality.Rows.Count > 0) AndAlso dtSpecialityCPT.Rows.Count > 0) Then


                        cmbSpecialityCPT.DataSource = dtspeciality
                        cmbSpecialityCPT.ValueMember = dtspeciality.Columns("nSpecialtyID").ColumnName
                        cmbSpecialityCPT.DisplayMember = dtspeciality.Columns("sDescription").ColumnName

                        cmbSpecialityICD9.Select()
                        cmbSpecialityICD9.DataSource = dtspeciality
                        cmbSpecialityICD9.ValueMember = dtspeciality.Columns("nSpecialtyID").ColumnName
                        cmbSpecialityICD9.DisplayMember = dtspeciality.Columns("sDescription").ColumnName

                        ''dhruv 20100104
                        ''binding the value to the combobox

                        cmbSpecialityICD9CPT.Select()
                        cmbSpecialityICD9CPT.DataSource = dtSpecialityCPT
                        cmbSpecialityICD9CPT.ValueMember = dtSpecialityCPT.Columns("nSpecialtyID").ColumnName
                        cmbSpecialityICD9CPT.DisplayMember = dtSpecialityCPT.Columns("sDescription").ColumnName

                        ''Code Added on 20091118:Mayuri
                        cmbSpecialityICD9.Text = "All"
                        ''End code Added on 20091118:Mayuri

                        ''Dhruv 20100104 
                        ''Selecting the text as "All"
                        cmbSpecialityICD9CPT.Text = "All"

                        'Code Commented by Mayuri:20091005
                        'cmbSpecialityCurrentCPT.DataSource = dtspeciality
                        'cmbSpecialityCurrentCPT.ValueMember = dtspeciality.Columns("nSpecialtyID").ColumnName
                        'cmbSpecialityCurrentCPT.DisplayMember = dtspeciality.Columns("sDescription").ColumnName
                        'End code commented by Mayuri:20091005

                    End If
                End If
            End If
            dtCategory = New DataTable
            dtCategory = oclsCPT.GetAllCategory()

            dtCPTGallery = New DataTable
            dtCPTGallery = oICD9CPT.GetCPTGallery(1)

            If Not IsNothing(dtCPTGallery) Then
                FillCPTGalleryTrv(dtCPTGallery)
            End If
            If Not IsNothing(dtCategory) Then
                FillCPTtrv(dtCategory)
            End If
            'gloAuditTrail.gloAuditTrail.UpdatePILog("END Load")
            'tlbSelectAll.Visible = True
            'tlbClearAll.Visible = False
            'tlbCurrentICD9.Visible = True
            'tlbCurrentCPT.Visible = False
            'tlbICD9Gallery.Visible = False
            'tlbCPTGallery.Visible = False
            'tlbImportIDC9.Visible = True
            'tlbImportCPT.Visible = False
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try



    End Sub

    '' COMMENTED BY SUDHIR 20090514 ''
    'Public Sub FillCPTtrv(ByVal dt As DataTable)
    '    Dim rootNode As New myTreeNode
    '    rootNode.Text = "CPT Category"
    '    rootNode.Key = -1
    '    rootNode.ImageIndex = 1
    '    rootNode.SelectedImageIndex = 1
    '    trvCPT.Nodes.Add(rootNode)
    '    Dim MyChild As myTreeNode
    '    Dim MyCategoryNode As myTreeNode
    '    dtCPT = New DataTable
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        MyChild = New myTreeNode
    '        MyChild.Text = CType(dt.Rows(i)("sDescription"), String)
    '        MyChild.Key = CType(dt.Rows(i)("nCategoryID"), Int64)
    '        MyChild.ImageIndex = 6
    '        MyChild.SelectedImageIndex = 6
    '        rootNode.Nodes.Add(MyChild)
    '        dvCategory = oclsCPT.GetAllCPT(CType(dt.Rows(i)("nCategoryID"), Int64))
    '        If i = 0 Then
    '            dtCPT = dvCategory.ToTable.Clone

    '            Dim clmnCategoryID As New DataColumn
    '            With clmnCategoryID
    '                .ColumnName = "CategoryID"
    '            End With
    '            dtCPT.Columns.Add(clmnCategoryID)
    '        End If
    '        'nCPTID,CPT_MST.sCPTCode, CPT_MST.sDescription, isnull(Specialty_MST.sDescription,'')
    '        If Not IsNothing(dvCategory.Table) Then
    '            For j As Integer = 0 To dvCategory.Table.Rows.Count - 1
    '                MyCategoryNode = New myTreeNode
    '                MyCategoryNode.Text = CType(dvCategory.Table.Rows(j)("sCPTCode"), String) & "-" & " " & CType(dvCategory.Table.Rows(j)("sDescription"), String)
    '                MyCategoryNode.Key = CType(dvCategory.Table.Rows(j)("nCPTID"), Int64)
    '                MyCategoryNode.Tag = CType(dvCategory.Table.Rows(j)("nSpecialtyID"), String)
    '                MyChild.Nodes.Add(MyCategoryNode)

    '                Dim r As DataRow
    '                r = dtCPT.NewRow()
    '                r.Item("nCPTID") = dvCategory.Table.Rows(j)("nCPTID")
    '                r.Item("sCPTCode") = dvCategory.Table.Rows(j)("sCPTCode")
    '                r.Item("sDescription") = dvCategory.Table.Rows(j)("sDescription")
    '                r.Item("nSpecialtyID") = dvCategory.Table.Rows(j)("nSpecialtyID")
    '                r.Item("CategoryID") = dt.Rows(i)("nCategoryID")
    '                dtCPT.Rows.Add(r)

    '            Next
    '        End If
    '    Next

    '    trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
    '    trvCPT.ExpandAll()
    'End Sub

    '' COMMENT BY SUDHIR 20090514 '' NEW GLOTREEVIEW CONTROL IMPLEMENTED '' 
    'Public Sub FillICD9GalleryTrv(ByVal dt As DataTable)
    '    ''''Fill ICD9Gallery Treeview
    '    Try
    '        If Not IsDBNull(dt) Then
    '            trvICD9Gallery.Nodes.Clear()
    '            Dim rootNode As New myTreeNode
    '            rootNode.Text = "ICD9 Gallery"
    '            rootNode.Key = -1
    '            rootNode.ImageIndex = 2
    '            rootNode.SelectedImageIndex = 2
    '            trvICD9Gallery.Nodes.Add(rootNode)
    '            trvICD9Gallery.BeginUpdate()
    '            Dim mytreeNode As myTreeNode
    '            If dt.Rows.Count < 400 Then
    '                ICD9GalleryCount = dt.Rows.Count - 1
    '            Else
    '                ICD9GalleryCount = 400
    '            End If

    '            For i As Integer = 0 To ICD9GalleryCount 'dt.Rows.Count - 1
    '                DisplayMember = dt.Rows(i)("sICD9Code") & " " & "-" & " " & dt.Rows(i)("sDescriptionMedium")
    '                ValuMember = dt.Rows(i)("nICD9ID")
    '                mytreeNode = New myTreeNode
    '                mytreeNode.Text = DisplayMember
    '                mytreeNode.Key = CType(ValuMember, Int64)
    '                If dt.Rows(i)("sIndicator") = "N" Then
    '                    mytreeNode.ImageIndex = 8
    '                    mytreeNode.SelectedImageIndex = 8
    '                ElseIf dt.Rows(i)("sIndicator") = "R" Then
    '                    mytreeNode.ImageIndex = 9
    '                    mytreeNode.SelectedImageIndex = 9
    '                ElseIf dt.Rows(i)("sIndicator") = "" Then
    '                    'mytreeNode.ImageIndex = 4
    '                    'mytreeNode.SelectedImageIndex = 4
    '                End If

    '                rootNode.Nodes.Add(mytreeNode)
    '                'trvICD9Gallery.Nodes(0).Nodes.Add(mytreeNode)
    '            Next
    '            trvICD9Gallery.ExpandAll()
    '            trvICD9Gallery.EndUpdate()
    '            trvICD9Gallery.SelectedNode = trvICD9Gallery.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        trvICD9Gallery.EndUpdate()
    '    End Try
    'End Sub

    Public Sub FillCPTtrv(ByVal dt As DataTable)

        cmbSpecialityCurrentCPT.DataSource = dt
        cmbSpecialityCurrentCPT.ValueMember = dt.Columns("nCategoryID").ColumnName
        cmbSpecialityCurrentCPT.DisplayMember = dt.Columns("sDescription").ColumnName
        'Commented by Mayuri:20091005
        'cmbICD9Current.DataSource = dt
        'cmbICD9Current.ValueMember = dt.Columns("nCategoryID").ColumnName
        'cmbICD9Current.DisplayMember = dt.Columns("sDescription").ColumnName
        'End code commented by Mayuri:20091005
    End Sub

    Public Sub FillICD9GalleryTrv(ByVal dt As DataTable)
        Try
            If Not IsNothing(dt) Then
                gloTrvICD9Gallery.DataSource = dt
                gloTrvICD9Gallery.ValueMember = dt.Columns("nICD9ID").ColumnName
                gloTrvICD9Gallery.CodeMember = dt.Columns("sICD9Code").ColumnName
                gloTrvICD9Gallery.DescriptionMember = dt.Columns("sDescriptionMedium").ColumnName
                gloTrvICD9Gallery.Indicator = dt.Columns("sIndicator").ColumnName
                gloTrvICD9Gallery.MaximumNodes = 200
                gloTrvICD9Gallery.FillTreeView()
                '14-01-2011'GLO2010-0005444'ICD9 code icons are not correct in icd9cpt gallery
                ''Not Code is writen in gloTrvICD9Gallery.NodeAdded Event
                'Select Case cmbICD9Gallery.Text
                '    Case "All"
                '        gloTrvICD9Gallery.ImageIndex = 0
                '        gloTrvICD9Gallery.SelectedImageIndex = 0
                '    Case "New"
                '        gloTrvICD9Gallery.ImageIndex = 8
                '        gloTrvICD9Gallery.SelectedImageIndex = 8
                '    Case "Revised"
                '        gloTrvICD9Gallery.ImageIndex = 9
                '        gloTrvICD9Gallery.SelectedImageIndex = 9
                '    Case "No Change"
                '        gloTrvICD9Gallery.ImageIndex = 4
                '        gloTrvICD9Gallery.SelectedImageIndex = 4
                'End Select
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Code Modified by Mayuri:20091005
    'Passed Speciality varible in order to fill treeview ICD9Current according to selected Speciality
    Private Sub FillCurrentICD9(ByVal Speciality As String)
        Try
            oclsICD9 = New clsICD9
            dtCurrentICD9 = oclsICD9.GetAllICD9(Speciality)
            If IsNothing(dtCurrentICD9) = False Then
                gloTrvCurrenICD9.DataSource = dtCurrentICD9
                gloTrvCurrenICD9.ValueMember = dtCurrentICD9.Columns("nICD9ID").ColumnName
                gloTrvCurrenICD9.CodeMember = dtCurrentICD9.Columns("sICD9Code").ColumnName
                gloTrvCurrenICD9.DescriptionMember = dtCurrentICD9.Columns("sDescription").ColumnName
                gloTrvCurrenICD9.CheckBoxes = True
                gloTrvCurrenICD9.FillTreeView()
                gloTrvCurrenICD9.ImageIndex = 0
                gloTrvCurrenICD9.SelectedImageIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Code Modified by Mayuri:20091005
    'Passed Speciality varible in order to fill treeview ICD9Current according to selected Speciality
    
    Private Sub FillCurrentICD9CPT(ByVal Speciality1 As String)
        Try
            oclsICD9 = New clsICD9
            dtCurrentICD9 = oclsICD9.GetAllICD9(Speciality1)
            If IsNothing(dtCurrentICD9) = False Then
                gloTrvCurrenICD9.DataSource = dtCurrentICD9
                gloTrvCurrenICD9.ValueMember = dtCurrentICD9.Columns("nICD9ID").ColumnName
                gloTrvCurrenICD9.CodeMember = dtCurrentICD9.Columns("sICD9Code").ColumnName
                gloTrvCurrenICD9.DescriptionMember = dtCurrentICD9.Columns("sDescription").ColumnName
                gloTrvCurrenICD9.CheckBoxes = True
                gloTrvCurrenICD9.FillTreeView()
                gloTrvCurrenICD9.ImageIndex = 0
                gloTrvCurrenICD9.SelectedImageIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'End Code Modified by Mayuri:20091005

    'Code Modified by Mayuri:20091005
    'Passed Speciality and Category varibles in order to fill treeview ICD9Current according to selected Category
    Private Sub FillCurrentCPT(ByVal Speciality As String, ByVal Category As String)
        Try
            oclsCPT = New clsCPT
            dtCurrentCPT = oclsCPT.GetAllCPT(Speciality, Category)

            If IsNothing(dtCurrentCPT) = False Then
                gloTrvCurrentCPT.DataSource = dtCurrentCPT
                gloTrvCurrentCPT.ValueMember = dtCurrentCPT.Columns("nCPTID").ColumnName
                gloTrvCurrentCPT.CodeMember = dtCurrentCPT.Columns("sCPTCode").ColumnName
                gloTrvCurrentCPT.DescriptionMember = dtCurrentCPT.Columns("sDescription").ColumnName
                'gloTrvCurrentCPT.DescriptionMember = dtCurrentCPT.Columns("sCategoryDesc").ColumnName
                gloTrvCurrentCPT.CheckBoxes = True
                gloTrvCurrentCPT.FillTreeView()
                gloTrvCurrentCPT.ImageIndex = 4
                gloTrvCurrentCPT.SelectedImageIndex = 4
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'End Code Modified by Mayuri:20091005

    'Code Commented by Mayuri:20091005
    'To fill treeview control according to selected speciality
    'Private Sub FillSpeciality()

    'Try
    '    oclsCPT = New clsCPT
    'dtCurrentCPT = oclsCPT.GetSpeciality
    '    If IsNothing(dtCurrentCPT) = False Then
    '        gloTrvCurrentCPT.DataSource = dtCurrentCPT
    '        gloTrvCurrentCPT.ValueMember = dtCurrentCPT.Columns("nCPTID").ColumnName
    '        gloTrvCurrentCPT.CodeMember = dtCurrentCPT.Columns("sCPTCode").ColumnName
    '        gloTrvCurrentCPT.DescriptionMember = dtCurrentCPT.Columns("sDescription").ColumnName
    '        'gloTrvCurrentCPT.DescriptionMember = dtCurrentCPT.Columns("sCategoryDesc").ColumnName
    '        gloTrvCurrentCPT.FillTreeView()
    '        gloTrvCurrentCPT.ImageIndex = 4
    '        gloTrvCurrentCPT.SelectedImageIndex = 4
    '    End If
    'Catch ex As Exception
    '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End Try


    'End Sub
    'End Code Commented by Mayuri:20091005

    'Public Sub FillCPTGalleryTrv(ByVal dt As DataTable)
    '    ''''Fill ICD9Gallery Treeview
    '    Try
    '        If Not IsDBNull(dt) Then
    '            trvCPTGallery.Nodes.Clear()
    '            Dim rootNode As New myTreeNode
    '            rootNode.Text = "CPT Gallery"
    '            rootNode.Key = -1
    '            rootNode.ImageIndex = 3
    '            rootNode.SelectedImageIndex = 3

    '            trvCPTGallery.Nodes.Add(rootNode)
    '            trvCPTGallery.BeginUpdate()
    '            Dim mytreeNode As myTreeNode

    '            If dt.Rows.Count < 400 Then
    '                CPTGalleryCount = dt.Rows.Count - 1
    '            Else
    '                CPTGalleryCount = 400
    '            End If

    '            For i As Integer = 0 To CPTGalleryCount 'dt.Rows.Count - 1
    '                DisplayMember = dt.Rows(i)("sCPTCode") & " " & "-" & " " & dt.Rows(i)("sDescription")
    '                ValuMember = dt.Rows(i)("nCPTID")
    '                mytreeNode = New myTreeNode
    '                mytreeNode.Text = DisplayMember
    '                mytreeNode.Key = CType(ValuMember, Int64)
    '                rootNode.Nodes.Add(mytreeNode)
    '                'trvICD9Gallery.Nodes(0).Nodes.Add(mytreeNode)
    '            Next
    '            trvCPTGallery.ExpandAll()
    '            trvCPTGallery.EndUpdate()
    '            trvCPTGallery.SelectedNode = trvCPTGallery.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        trvCPTGallery.EndUpdate()
    '    End Try
    'End Sub

    Public Sub FillCPTGalleryTrv(ByVal dt As DataTable)
        Try
            If Not IsNothing(dt) Then

                gloTrvCPTGallery.DataSource = dt
                gloTrvCPTGallery.ValueMember = dt.Columns("nCPTID").ColumnName
                gloTrvCPTGallery.CodeMember = dt.Columns("sCPTCode").ColumnName
                gloTrvCPTGallery.DescriptionMember = dt.Columns("sDescription").ColumnName
                gloTrvCPTGallery.MaximumNodes = 200
                gloTrvCPTGallery.ImageIndex = 4
                gloTrvCPTGallery.SelectedImageIndex = 4
                gloTrvCPTGallery.FillTreeView()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'Public Sub FillICD9Trv(ByVal dt As DataTable)
    '    Dim rootNode As New myTreeNode
    '    rootNode.Text = "ICD9 Speciality"
    '    rootNode.Key = -1
    '    rootNode.ImageIndex = 0
    '    rootNode.SelectedImageIndex = 0
    '    trvICD9.Nodes.Add(rootNode)
    '    Dim MyChild As myTreeNode
    '    Dim SpcialityNode As myTreeNode
    '    dtICD9 = New DataTable
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        MyChild = New myTreeNode
    '        dvSpeciality = New DataView
    '        MyChild.Text = CType(dt.Rows(i)("sDescription"), String)
    '        MyChild.Key = CType(dt.Rows(i)("nSpecialtyID"), Int64)
    '        MyChild.ImageIndex = 5
    '        MyChild.SelectedImageIndex = 5
    '        rootNode.Nodes.Add(MyChild)
    '        dvSpeciality = oclsICD9.GetAllICD(CType(dt.Rows(i)("nSpecialtyID"), Int64))
    '        If i = 0 Then
    '            dtICD9 = dvSpeciality.ToTable().Clone
    '            Dim clmnSpecialityID As New DataColumn
    '            With clmnSpecialityID
    '                .ColumnName = "SpecialityID"
    '            End With
    '            dtICD9.Columns.Add(clmnSpecialityID)
    '        End If
    '        'nICD9ID,sICD9Code, 'sDescription'=ICD9.sDescription
    '        If Not IsNothing(dvSpeciality) Then
    '            For j As Integer = 0 To dvSpeciality.Table.Rows.Count - 1
    '                SpcialityNode = New myTreeNode
    '                SpcialityNode.Text = CType(dvSpeciality.Table.Rows(j)("sICD9Code"), String) & "-" & " " & CType(dvSpeciality.Table.Rows(j)("sDescription"), String)
    '                SpcialityNode.Key = CType(dvSpeciality.Table.Rows(j)("nICD9ID"), Int64)
    '                MyChild.Nodes.Add(SpcialityNode)
    '                Dim r As DataRow
    '                r = dtICD9.NewRow()
    '                r.Item("nICD9ID") = dvSpeciality.Table.Rows(j)("nICD9ID")
    '                r.Item("sICD9Code") = dvSpeciality.Table.Rows(j)("sICD9Code")
    '                r.Item("sDescription") = dvSpeciality.Table.Rows(j)("sDescription")
    '                r.Item("SpecialityID") = dt.Rows(i)("nSpecialtyID")
    '                dtICD9.Rows.Add(r)
    '            Next
    '        End If

    '    Next
    '    trvICD9.ExpandAll()
    '    trvICD9.SelectedNode = trvICD9.Nodes.Item(0)
    '    'For i As Integer = 0 To dt.Rows.Count - 1
    '    '    IsSpecialityPresent = False
    '    '    For Each specialityNoade As myTreeNode In trvICD9.Nodes(0).Nodes
    '    '        If specialityNoade.Text = dt.Rows(i)("sDescription") Then
    '    '            IsSpecialityPresent = True
    '    '            Exit For
    '    '        End If
    '    '    Next
    '    '    If IsSpecialityPresent = False Then
    '    '        Dim myspecalityNode As New myTreeNode
    '    '        myspecalityNode.Text = dt.Rows(i)("sDescription")
    '    '        myspecalityNode.Key = dt.Rows(i)("nSpecialtyID")
    '    '        myspecalityNode.ImageIndex = 5
    '    '        myspecalityNode.SelectedImageIndex = 5

    '    '        trvICD9.Nodes(0).Nodes.Add(myspecalityNode)
    '    '    End If
    '    'Next


    'End Sub

    '' COMMENT BY SUDHIR 20090514 '' NOW DIRECTLY SAVE TO DATABASE ON CLICK .''
    'Private Sub btnInsetICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsetICD9.Click
    '    Try

    '        Dim selectedNode As New myTreeNode
    '        selectedNode = trvICD9.SelectedNode
    '        If IsNothing(selectedNode) Then
    '            Exit Sub
    '        End If
    '        Dim isICD9Check As Boolean = False
    '        If selectedNode.Key = -1 Then
    '            MessageBox.Show("Please select Speciality", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If
    '        For i As Integer = 0 To trvICD9Gallery.Nodes(0).Nodes.Count - 1
    '            If trvICD9Gallery.Nodes(0).Nodes.Item(i).Checked = True Then
    '                isICD9Check = True
    '                Dim myNode As New myTreeNode
    '                myNode = CType(trvICD9Gallery.Nodes(0).Nodes.Item(i), myTreeNode)
    '                If Not myNode Is Nothing Then
    '                    If Not myNode Is trvICD9.Nodes(0) Then
    '                        AddAssociatesICD9(myNode, trvICD9.SelectedNode.Text)
    '                    End If
    '                End If
    '                trvICD9Gallery.Nodes(0).Nodes.Item(i).Checked = False
    '            End If
    '        Next
    '        If isICD9Check = False Then
    '            MessageBox.Show("Please select ICD9 to insert", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            trvICD9.SelectedNode = selectedNode.FirstNode
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnInsetICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsetICD9.Click

        Try
            'Code Added on 20091026
            Me.Cursor = Cursors.WaitCursor
            'End Code Added on 20091026
            _isSelectedICD9Gallery = True
            Dim oCollection As Collection = gloTrvICD9Gallery.SelectedNodes
            Dim oclsICD9 As New clsICD9(GetConnectionString)
            ''Dhruv 20100105 
            ''To check the number's of modified and inserted ICD9  
            Dim _modifiedCounter As Integer = 0
            Dim _addCounter As Integer = 0
            If IsNothing(oCollection) = False Then
                If oCollection.Count > 0 Then
                    For i As Integer = 1 To oCollection.Count
                        Dim oNode As gloUserControlLibrary.myTreeNode
                        oNode = CType(oCollection(i), gloUserControlLibrary.myTreeNode)

                        'Dim oICD9 As New gloBilling.ICD9(GetConnectionString)
                        'If oICD9.IsExistsICD9(oNode.ID, oNode.Code, oNode.Description) = True Then
                        '    oICD9.ICD9ID = oNode.ID
                        'Else
                        '    oICD9.ICD9ID = 0
                        'End If

                        'oICD9.ICD9Code = oNode.Code
                        'oICD9.Description = oNode.Description
                        'oICD9.SpecialtyID = cmbSpecialityICD9.SelectedValue
                        'oICD9.Inactive = False
                        'oICD9.ClinicID = gnClinicID
                        'oICD9.Add() '' METHOD NOT IMPLEMENTED YET ''

                        If oclsICD9.CheckDuplicate(oNode.Code, oNode.Description) = True Then
                            ''oclsICD9.AddNewICD9(oNode.ID, oNode.Code, oNode.Description, CType(cmbSpecialityICD9.SelectedValue, Int64))
                            ''dhruv 20100104 
                            oclsICD9.AddNewICD9(oNode.ID, oNode.Code, oNode.Description, CType(cmbSpecialityICD9.SelectedValue, Int64))
                            _modifiedCounter = _modifiedCounter + 1

                        Else
                            oclsICD9.AddNewICD9(0, oNode.Code, oNode.Description, CType(cmbSpecialityICD9.SelectedValue, Int64))

                            'Code Added by Mayuri:20091003
                            'To delete ICD9 from ICD9Gallery,After Adding it into CurrentICD9

                            ''Commenetd by Mayuri:20100129-only add Icd9 into current ICD9 not remove from ICD9Gllery
                            'gloTrvICD9Gallery.Nodes.Remove(oNode)
                            'oclsICD9.DeletICD9(oNode.ID, _isSelectedICD9Gallery)
                            ''End code Commenetd by Mayuri:20100129

                            'oclsICD9.DeleteICD9(oNode.ID, oNode.Code)
                            'End code added by Mayuri:20091003

                            _addCounter = _addCounter + 1
                        End If


                    Next

                    Dim _msgStr As String = ""
                    If _addCounter > 0 Then
                        _msgStr = _addCounter & " ICD9(s) inserted" & vbLf
                    End If
                    If _modifiedCounter > 0 Then
                        _msgStr = _msgStr & _modifiedCounter & "ICD9(s) modified"
                    End If
                    If _msgStr <> "" Then
                        MessageBox.Show(_msgStr, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    gloTrvICD9Gallery.Refresh()
                    gloTrvCurrenICD9.Refresh()

                    'code Added by Mayuri:20091003
                    'To Refresh CurrentICD9 treeview
                    FillCurrentICD9(cmbSpecialityICD9.Text)
                    'to Refresh ICD9Gallery treeview


                    ''dhruv 20100104
                    ''FillCurrentICD9CPT(cmbSpecialityICD9CPT.Text)

                    dtICD9Gallery = oICD9CPT.GetICD9Gallery(1, strIndicator)
                    If Not IsNothing(dtICD9Gallery) Then
                        FillICD9GalleryTrv(dtICD9Gallery)
                    End If
                    'End Code Added by Mayuri:20091003

                    'FillICD9GalleryTrv(dtICD9Gallery)

                    'For i As Integer = 1 To oCollection.Count

                    '    gloTrvICD9Gallery.Nodes.Remove(gloTrvICD9Gallery.SelectedNodes())
                    'Next

                    ' gloTrvICD9Gallery.Nodes.Remove(gloTrvICD9Gallery.SelectedNodes())

                    oclsICD9.Dispose()
                    oclsICD9 = Nothing

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Code Added on 20091026
        Finally
            Me.Cursor = Cursors.Default
            'End Code Added on 20091026
        End Try
    End Sub


    'Private Sub AddAssociatesICD9(ByVal mynode As myTreeNode, ByVal strType As String)
    '    Try
    '        For Each myRootNode As myTreeNode In trvICD9.Nodes(0).Nodes
    '            ''Loop for all field nodes in each Root node
    '            For Each myTargetNode As myTreeNode In myRootNode.Nodes
    '                ''Check whether the node already exists
    '                'If myTargetNode.Key = mynode.Key Then
    '                '    ''If present do nothing
    '                '    Exit Sub
    '                'End If

    '                strMyTargetNodeICD9 = Split(myTargetNode.Text, "-")
    '                strMyNodeICD9 = Split(mynode.Text, "-")

    '                If Trim(strMyTargetNodeICD9.GetValue(0)) = Trim(strMyNodeICD9.GetValue(0)) And Trim(strMyTargetNodeICD9.GetValue(1)) = Trim(strMyNodeICD9.GetValue(1)) Then
    '                    IsICD9Present = True
    '                    Exit For
    '                End If
    '                'If myTargetNode.Text = mynode.Text Then
    '                '    Exit Sub
    '                'End If
    '            Next
    '            If IsICD9Present = True Then
    '                Exit For
    '            End If
    '        Next
    '        If IsICD9Present = False Then
    '            For Each myRootNode As myTreeNode In trvICD9.Nodes(0).Nodes
    '                If myRootNode.Text = strType Then
    '                    Dim Associatenode As New myTreeNode
    '                    'Associatenode = mynode.Clone
    '                    Associatenode.Text = mynode.Text
    '                    Associatenode.Key = mynode.Key
    '                    'Associatenode.ImageIndex = 0
    '                    'Associatenode.SelectedImageIndex = 0
    '                    myRootNode.Nodes.Add(Associatenode)
    '                End If
    '            Next
    '        End If
    '        trvICD9.ExpandAll()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        IsICD9Present = False
    '    End Try
    'End Sub
    'Private Sub AddAssociatesCPT(ByVal mynode As myTreeNode, ByVal strType As String)
    '    Try
    '        For Each myRootNode As myTreeNode In trvCPT.Nodes(0).Nodes
    '            ''Loop for all field nodes in each Root node
    '            For Each myTargetNode As myTreeNode In myRootNode.Nodes
    '                ''Check whether the node already exists
    '                'If myTargetNode.Key = mynode.Key Then
    '                '    ''If present do nothing
    '                '    Exit Sub
    '                'End If
    '                strMyTargetNodeCPT = Split(myTargetNode.Text, "-")
    '                strMyNodeCPT = Split(mynode.Text, "-")

    '                If Trim(strMyTargetNodeCPT.GetValue(0)) = Trim(strMyNodeCPT.GetValue(0)) And Trim(strMyTargetNodeCPT.GetValue(1)) = Trim(strMyNodeCPT.GetValue(1)) Then
    '                    IsCPTPresent = True
    '                    Exit For
    '                End If
    '            Next
    '            If IsCPTPresent = True Then
    '                Exit For
    '            End If
    '        Next
    '        If IsCPTPresent = False Then
    '            For Each myRootNode As myTreeNode In trvCPT.Nodes(0).Nodes
    '                If IsCPTPresent = False Then
    '                    If myRootNode.Text = strType Then
    '                        ''Map all the node values to the associated node
    '                        Dim Associatenode As New myTreeNode
    '                        'Associatenode = mynode.Clone
    '                        Associatenode.Text = mynode.Text
    '                        Associatenode.Key = mynode.Key
    '                        Associatenode.Tag = CType(cmbSpecialityCPT.SelectedValue, String)
    '                        'Associatenode.ImageIndex = 0
    '                        'Associatenode.SelectedImageIndex = 0
    '                        myRootNode.Nodes.Add(Associatenode)
    '                    End If
    '                End If
    '            Next
    '        End If
    '        trvCPT.ExpandAll()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        IsCPTPresent = False
    '    End Try
    'End Sub

    'Private Sub btnRemoveICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim isICD9Check As Boolean = False
    '        For i As Integer = 0 To trvICD9.Nodes(0).Nodes.Count - 1
    '            For j As Integer = trvICD9.Nodes(0).Nodes(i).Nodes.Count - 1 To 0 Step -1
    '                If trvICD9.Nodes(0).Nodes(i).Nodes.Item(j).Checked = True Then
    '                    isICD9Check = True
    '                    trvICD9.Nodes(0).Nodes(i).Nodes.Item(j).Remove()
    '                End If
    '            Next
    '        Next

    '        If isICD9Check = False Then
    '            MessageBox.Show("Please select ICD9 to Remove", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    'Private Sub txtsearchICD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try

    '        If (e.KeyChar = ChrW(13)) Then
    '            trvICD9Gallery.Select()
    '        Else
    '            trvICD9Gallery.SelectedNode = trvICD9Gallery.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    'Private Sub txtsearchICD9Gallery_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        Dim i As Integer
    '        Dim tdt As DataTable
    '        Dim dv As DataView

    '        dv = New DataView(dtICD9Gallery)
    '        Dim strsearch As String = txtsearchICD9Gallery.Text.Trim

    '        If rdICD9lftDesc.Checked = True Then
    '            dv.RowFilter = dv.Table.Columns("sDescriptionMedium").ColumnName & " Like '" & strsearch & "%'"
    '        Else
    '            dv.RowFilter = dv.Table.Columns("sICD9Code").ColumnName & " Like '" & strsearch & "%'"
    '        End If


    '        tdt = New DataTable
    '        tdt = dv.ToTable
    '        trvICD9Gallery.Nodes.Clear()
    '        'trvICDGallery.Nodes(0).Nodes.Clear()

    '        trvICD9Gallery.Hide()
    '        'trvICD9Gallery.BeginUpdate()
    '        'trvICD9Gallery.Nodes.Add(-1, "ICD9 Gallery")

    '        FillICD9GalleryTrv(tdt)
    '        'For i = 0 To tdt.Rows.Count - 1
    '        '    DisplayMember = tdt.Rows(i)("sICD9Code") & "-" & tdt.Rows(i)("sDescriptionMedium")
    '        '    ValuMember = tdt.Rows(i)("nICD9ID")
    '        '    Dim mytreeNode As New TreeNode
    '        '    mytreeNode.Text = DisplayMember
    '        '    mytreeNode.Tag = ValuMember
    '        '    trvICDGallery.Nodes(0).Nodes.Add(mytreeNode)
    '        'Next
    '        trvICD9Gallery.Nodes.Item(0).Expand()
    '        trvICD9Gallery.Show()
    '        trvICD9Gallery.SelectedNode = trvICD9Gallery.Nodes.Item(0)

    '        'trvCPT.Select()
    '        trvICD9Gallery.EndUpdate()

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub btnInsertCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsertCPT.Click
    '    Try
    '        If cmbSpecialityCPT.SelectedIndex = -1 Then
    '            MessageBox.Show("Please select Speciality", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            cmbSpecialityCPT.Focus()
    '            Exit Sub
    '        End If
    '        'If trvCPT.SelectedNode.Text = "" Or trvCPT.SelectedNode.Text = "CPT" Then
    '        '    Exit Sub
    '        'End If
    '        Dim isCPTCheck As Boolean = False
    '        Dim selectedNode As New myTreeNode
    '        selectedNode = trvCPT.SelectedNode
    '        If IsNothing(selectedNode) Then
    '            Exit Sub
    '        End If
    '        If selectedNode.Key = -1 Then
    '            MessageBox.Show("Please select Category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If


    '        For i As Integer = 0 To trvCPTGallery.Nodes(0).Nodes.Count - 1
    '            If trvCPTGallery.Nodes(0).Nodes.Item(i).Checked = True Then
    '                isCPTCheck = True
    '                Dim myNode As New myTreeNode
    '                myNode = CType(trvCPTGallery.Nodes(0).Nodes.Item(i), myTreeNode)
    '                If Not myNode Is Nothing Then
    '                    If Not myNode Is trvCPT.Nodes(0) Then
    '                        AddAssociatesCPT(myNode, trvCPT.SelectedNode.Text)
    '                    End If
    '                End If
    '                trvCPTGallery.Nodes(0).Nodes.Item(i).Checked = False
    '            End If
    '        Next
    '        If isCPTCheck = False Then
    '            MessageBox.Show("Please select CPT to insert", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            trvCPT.SelectedNode = selectedNode.FirstNode
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnInsertCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsertCPT.Click
        btnCPTAdd.Enabled = False
        Try
            'Code Added on 20091026
            Me.Cursor = Cursors.WaitCursor
            'End Code Added on 20091026
            Dim oCollection As Collection = gloTrvCPTGallery.SelectedNodes
            ''Dhruv 20100105 
            ''the counters to count the number of CPT that has been inserted and added. under the CPT
            Dim _modifiedCounter As Integer = 0
            Dim _addCounter As Integer = 0
            _isSelectedCPTGallery = True
            If IsNothing(oCollection) = False Then
                If oCollection.Count > 0 Then
                    For i As Integer = 1 To oCollection.Count
                        Dim oNode As gloUserControlLibrary.myTreeNode = CType(oCollection(i), gloUserControlLibrary.myTreeNode)

                        Dim oCPT As New gloBilling.CPT(GetConnectionString)

                        If oCPT.IsDuplicateCPT(oNode.ID, oNode.Code, oNode.Description) = True Then
                            oCPT.CPTID = oNode.ID
                            _modifiedCounter = _modifiedCounter + 1
                        Else
                            oCPT.CPTID = 0
                            _addCounter = _addCounter + 1
                        End If

                        oCPT.CPTCode = oNode.Code
                        oCPT.Description = oNode.Description
                        oCPT.ClinicID = gClinicID
                        oCPT.Inactive = False
                       
                        ''Dhruv 20100104
                        oCPT.nSpecialtyID = cmbSpecialityICD9CPT.SelectedValue
                        oCPT.SpecialityCode = cmbSpecialityICD9CPT.Text

                        oCPT.nCategoryID = cmbSpecialityCurrentCPT.SelectedValue
                        oCPT.Categorydesc = cmbSpecialityCurrentCPT.Text
                        oCPT.Add()
                        UpdateLog("InsertCPT " & i)

                        oCPT.Dispose()
                        oCPT = Nothing
                        'Code Added by Mayuri:20091003
                        'To delete CPT from CPTGallery,After Adding it into CurrentCPT
                        'gloTrvCPTGallery.Nodes.Remove(oNode)
                        'oICD9CPT.DeleteCPT(oNode.ID, _isSelectedCPTGallery)

                        'If oclsCPT.CheckDuplicate(oNode.Code, oNode.Description) = True Then
                        '    oclsCPT.AddNewCPT(oNode.ID, oNode.Code, oNode.Description, CType(cmbSpecialityCPT.SelectedValue, Int64), CType(cmbCategoryCPT.SelectedValue, Int64))
                        'Else
                        '    oclsCPT.AddNewCPT(0, oNode.Code, oNode.Description, CType(cmbSpecialityCPT.SelectedValue, Int64), CType(cmbCategoryCPT.SelectedValue, Int64))
                        'End If
                    Next

                    ''dhruv 20100105 
                    ''To check the string how may has been inserted and modified
                    Dim _MsgString As String = ""
                    If _addCounter > 0 Then
                        _MsgString = _addCounter & " CPT(s) inserted " & vbLf
                    End If
                    If _modifiedCounter > 0 Then
                        _MsgString = _MsgString & _modifiedCounter & " CPT(s) modified "
                    End If
                    If _MsgString <> "" Then
                        MessageBox.Show(_MsgString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    gloTrvCPTGallery.Refresh()
                    gloTrvCurrentCPT.Refresh()
                    'To Refresh CurrentCPT treeview
                    ''FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
                    'To Refrsh CPTGallery treeview
                    ''dhruv 20100104
                    FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)

                    dtCPTGallery = oICD9CPT.GetCPTGallery(1)
                    If Not IsNothing(dtCPTGallery) Then
                        FillCPTGalleryTrv(dtCPTGallery)
                    End If

                    'End Code Added by Mayuri:20091003
                    'FillCPTGalleryTrv(dtCPTGallery)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
            'Code Added on 20091026
        Finally
            Me.Cursor = Cursors.Default
            btnCPTAdd.Enabled = True
            'End Code Added on 20091026
        End Try
    End Sub

    'Private Sub btnRemoveCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim isCPTCheck As Boolean = False
    '        For i As Integer = 0 To trvCPT.Nodes(0).Nodes.Count - 1
    '            For j As Integer = trvCPT.Nodes(0).Nodes(i).Nodes.Count - 1 To 0 Step -1
    '                If trvCPT.Nodes(0).Nodes(i).Nodes.Item(j).Checked = True Then
    '                    isCPTCheck = True
    '                    trvCPT.Nodes(0).Nodes(i).Nodes.Item(j).Remove()
    '                End If
    '            Next
    '        Next

    '        If isCPTCheck = False Then
    '            MessageBox.Show("Please select CPT to Remove", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    'Private Sub txtsearchCPTGallery_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try

    '        If (e.KeyChar = ChrW(13)) Then
    '            trvCPTGallery.Select()
    '        Else
    '            trvCPTGallery.SelectedNode = trvCPTGallery.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    'Private Sub txtsearchCPTGallery_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try

    '        Dim i As Integer
    '        Dim tdt As DataTable
    '        Dim dv As New DataView(dtCPTGallery)
    '        Dim strsearch As String = txtsearchCPTGallery.Text.Trim
    '        If rdCPTlftCode.Checked = True Then
    '            dv.RowFilter = dv.Table.Columns("sCPTCode").ColumnName & " Like '" & strsearch & "%'"
    '        Else
    '            dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '" & strsearch & "%'"
    '        End If


    '        tdt = New DataTable
    '        tdt = dv.ToTable
    '        trvCPTGallery.Nodes.Clear()
    '        'trvICDGallery.Nodes(0).Nodes.Clear()

    '        trvCPTGallery.Hide()
    '        'trvICD9Gallery.BeginUpdate()
    '        'trvICD9Gallery.Nodes.Add(-1, "ICD9 Gallery")

    '        FillCPTGalleryTrv(tdt)
    '        'For i = 0 To tdt.Rows.Count - 1
    '        '    DisplayMember = tdt.Rows(i)("sICD9Code") & "-" & tdt.Rows(i)("sDescriptionMedium")
    '        '    ValuMember = tdt.Rows(i)("nICD9ID")
    '        '    Dim mytreeNode As New TreeNode
    '        '    mytreeNode.Text = DisplayMember
    '        '    mytreeNode.Tag = ValuMember
    '        '    trvICDGallery.Nodes(0).Nodes.Add(mytreeNode)
    '        'Next
    '        trvCPTGallery.Nodes.Item(0).Expand()
    '        trvCPTGallery.Show()
    '        trvCPTGallery.SelectedNode = trvCPTGallery.Nodes.Item(0)

    '        'trvCPT.Select()
    '        trvCPTGallery.EndUpdate()

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        trvCPTGallery.EndUpdate()
    '    End Try



    'End Sub

    'Private Sub trvCPTGallery_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    '    Try
    '        If trvCPTGallery.Nodes.Item(0).Checked = True And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvCPTGallery.Nodes(0).Nodes.Count - 1
    '                trvCPTGallery.Nodes(0).Nodes.Item(i).Checked = True
    '            Next
    '            'trvCPTGallery.SelectedNode = trvCPTGallery.Nodes(0).Nodes.Item(0)
    '        ElseIf trvCPTGallery.Nodes.Item(0).Checked = False And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvCPTGallery.Nodes(0).Nodes.Count - 1
    '                trvCPTGallery.Nodes(0).Nodes.Item(i).Checked = False
    '            Next
    '            'trvCPTGallery.SelectedNode = trvCPTGallery.Nodes.Item(0)
    '        End If


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Sub

    'Private Sub trvCPTGallery_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    'End Sub

    'Private Sub tbICD9CPTGallery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbICD9CPTGallery.Click
    '    'cmbSpecialityCPT.Text = "Select Speciality"
    '    'cmbSpecialityCPT.SelectedIndex = -1

    '    'txtsearchICD9Gallery.Text = ""
    '    'txtsearchCPTGallery.Text = ""
    '    'txtsearchCPT.Text = ""
    '    'txtSearchICD9.Text = ""
    'End Sub

    'Private Sub tbICD9CPTGallery_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbICD9CPTGallery.DrawItem


    'End Sub

    Private Sub tlICD9CptGallery_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlICD9CptGallery.ItemClicked

        Select Case e.ClickedItem.Tag
            Case "ImportICD9"
                btnInsetICD9_Click(Nothing, Nothing)
                'SaveICD9CPT()
            Case "ImportCPT"
                btnInsertCPT_Click(Nothing, Nothing)
            Case "Close"
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "SelectAll"
                If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
                    gloTrvICD9Gallery.CheckAllNodes()
                ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
                    gloTrvCPTGallery.CheckAllNodes()
                End If
                tlbSelectAll.Visible = False
                tlbClearAll.Visible = True
            Case "ClearAll"
                If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
                    gloTrvICD9Gallery.UncheckAllNodes()
                ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
                    gloTrvCPTGallery.UncheckAllNodes()
                End If
                tlbSelectAll.Visible = True
                tlbClearAll.Visible = False
            Case "CurrentICD9"
                pnlCurrentICD9.Visible = True
                pnlICD9Gallery.Visible = False
                FillCurrentICD9(cmbSpecialityICD9.Text)
                tlbCurrentICD9.Visible = False
                tlbICD9Gallery.Visible = True
                pnlICD9bottom.Visible = False
                pnlICD9Center.Visible = False
            Case "ICD9Gallery"
                pnlCurrentICD9.Visible = False
                pnlICD9Gallery.Visible = True
                tlbCurrentICD9.Visible = True
                tlbICD9Gallery.Visible = False
                pnlICD9bottom.Visible = True
                pnlICD9Center.Visible = True
            Case "CurrentCPT"
                pnlCurrentCPT.Visible = True
                pnlCPTGallery.Visible = False
                ''dhruv 20100104
                ''FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
                FillCurrentICD9CPT(cmbSpecialityICD9CPT.Text)
                FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)
                tlbCurrentCPT.Visible = False
                tlbCPTGallery.Visible = True
                ' pnlcenterCPT.Visible = False
            Case "CPTGallery"
                pnlCurrentCPT.Visible = False
                pnlCPTGallery.Visible = True
                tlbCurrentCPT.Visible = True
                tlbCPTGallery.Visible = False
                ' pnlcenterCPT.Visible = True
        End Select

    End Sub
    'Public Sub SaveICD9CPT()
    '    Try
    '        oICD9CPT = New DBICD9CPT
    '        If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
    '            oclsICD9 = New clsICD9
    '            Dim selectedNode As New myTreeNode
    '            Dim selectedNodeSpeca As New myTreeNode
    '            For i As Integer = 0 To trvICD9.Nodes(0).Nodes.Count - 1
    '                For j As Integer = 0 To trvICD9.Nodes(0).Nodes(i).Nodes.Count - 1
    '                    IsICD9Update = False
    '                    'If trvICD9.Nodes(0).Nodes(i).Nodes.Item(j).Checked = True Then
    '                    selectedNode = CType(trvICD9.Nodes(0).Nodes(i).Nodes.Item(j), myTreeNode)
    '                    strCodeDescr = Split(selectedNode.Text, "-", 2)
    '                    ICD9Code = strCodeDescr.GetValue(0)
    '                    ICD9Description = strCodeDescr.GetValue(1)
    '                    selectedNodeSpeca = CType(trvICD9.Nodes(0).Nodes.Item(i), myTreeNode)
    '                    ICD9SpecialityID = selectedNodeSpeca.Key
    '                    ICD9ID = selectedNode.Key
    '                    For k As Integer = 0 To dtICD9.Rows.Count - 1
    '                        If ICD9ID = CType(dtICD9.Rows(k)("nICD9ID"), Int64) Then
    '                            IsICD9Update = True
    '                            Exit For
    '                        End If
    '                    Next
    '                    If IsICD9Update = True Then
    '                        oclsICD9.AddNewICD9(ICD9ID, Trim(ICD9Code), Trim(ICD9Description), ICD9SpecialityID)
    '                    Else
    '                        oclsICD9.AddNewICD9(0, Trim(ICD9Code), Trim(ICD9Description), ICD9SpecialityID)
    '                    End If

    '                    'End If
    '                Next
    '            Next

    '        Else

    '            oclsCPT = New clsCPT
    '            Dim selectedNode As New myTreeNode
    '            Dim selectedNodeSpeca As New myTreeNode
    '            For i As Integer = 0 To trvCPT.Nodes(0).Nodes.Count - 1

    '                For j As Integer = 0 To trvCPT.Nodes(0).Nodes(i).Nodes.Count - 1
    '                    IsCPTUpdate = False
    '                    'If trvICD9.Nodes(0).Nodes(i).Nodes.Item(j).Checked = True Then
    '                    selectedNode = CType(trvCPT.Nodes(0).Nodes(i).Nodes.Item(j), myTreeNode)
    '                    strCPTCodeDescr = Split(selectedNode.Text, "-", 2)
    '                    CPTCode = strCPTCodeDescr.GetValue(0)
    '                    CPTDescription = strCPTCodeDescr.GetValue(1)
    '                    selectedNodeSpeca = CType(trvCPT.Nodes(0).Nodes.Item(i), myTreeNode)
    '                    CPTCategoryID = selectedNodeSpeca.Key
    '                    CPTSpecialityID = CType(selectedNode.Tag, Int64)  'cmbSpecialityCPT.SelectedValue
    '                    CPTID = CType(selectedNode.Key, Int64)
    '                    For k As Integer = 0 To dtCPT.Rows.Count - 1
    '                        If CPTID = CType(dtCPT.Rows(k)("nCPTID"), Int64) Then
    '                            IsCPTUpdate = True
    '                            Exit For
    '                        End If
    '                    Next
    '                    If IsCPTUpdate = False Then
    '                        oclsCPT.AddNewCPT(0, Trim(CPTCode), Trim(CPTDescription), CPTSpecialityID, CPTCategoryID)
    '                    Else
    '                        oclsCPT.AddNewCPT(CPTID, Trim(CPTCode), Trim(CPTDescription), CPTSpecialityID, CPTCategoryID)
    '                    End If

    '                    'End If
    '                Next
    '            Next
    '        End If
    '        Me.Close()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.AddTransactionLine, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Sub

    'Private Sub tbICD9CPTGallery_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbICD9CPTGallery.TabIndexChanged
    '    'cmbSpecialityCPT.SelectedIndex = -1
    '    cmbSpecialityCPT.Text = "Select Speciality"
    '    txtsearchICD9Gallery.Text = ""
    '    txtsearchCPTGallery.Text = ""
    'End Sub


    'Private Sub rdICD9lftCode_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If rdICD9lftCode.Checked = True Then
    '        dtICD9Gallery = New DataTable
    '        strIndicator = Trim(cmbIndicator.Text)
    '        dtICD9Gallery = oICD9CPT.GetICD9Gallery(0, strIndicator)
    '        FillICD9GalleryTrv(dtICD9Gallery)
    '    Else
    '        dtICD9Gallery = New DataTable
    '        strIndicator = Trim(cmbIndicator.Text)
    '        dtICD9Gallery = oICD9CPT.GetICD9Gallery(1, strIndicator)
    '        FillICD9GalleryTrv(dtICD9Gallery)
    '    End If


    'End Sub

    'Private Sub rdICD9lftDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If rdICD9lftDesc.Checked = True Then
    '        dtICD9Gallery = New DataTable
    '        strIndicator = Trim(cmbIndicator.Text)
    '        dtICD9Gallery = oICD9CPT.GetICD9Gallery(1, strIndicator)
    '        If Not IsNothing(dtICD9Gallery) Then
    '            FillICD9GalleryTrv(dtICD9Gallery)
    '        End If
    '    Else
    '        dtICD9Gallery = New DataTable
    '        strIndicator = Trim(cmbIndicator.Text)
    '        dtICD9Gallery = oICD9CPT.GetICD9Gallery(0, strIndicator)
    '        If Not IsNothing(dtICD9Gallery) Then
    '            FillICD9GalleryTrv(dtICD9Gallery)
    '        End If
    '    End If

    'End Sub


    'Private Sub rdCPTlftCode_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If rdCPTlftCode.Checked = True Then
    '        dtCPTGallery = New DataTable
    '        dtCPTGallery = oICD9CPT.GetCPTGallery(0)

    '        If Not IsNothing(dtCPTGallery) Then
    '            FillCPTGalleryTrv(dtCPTGallery)
    '        End If
    '    Else
    '        dtCPTGallery = New DataTable
    '        dtCPTGallery = oICD9CPT.GetCPTGallery(1)

    '        If Not IsNothing(dtCPTGallery) Then
    '            FillCPTGalleryTrv(dtCPTGallery)
    '        End If
    '    End If

    'End Sub


    'Private Sub rdCPTlftDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If rdCPTlftDesc.Checked = True Then
    '        dtCPTGallery = New DataTable
    '        dtCPTGallery = oICD9CPT.GetCPTGallery(1)

    '        If Not IsNothing(dtCPTGallery) Then
    '            FillCPTGalleryTrv(dtCPTGallery)
    '        End If
    '    Else
    '        dtCPTGallery = New DataTable
    '        dtCPTGallery = oICD9CPT.GetCPTGallery(0)

    '        If Not IsNothing(dtCPTGallery) Then
    '            FillCPTGalleryTrv(dtCPTGallery)
    '        End If
    '    End If
    'End Sub

    'Private Sub txtSearchICD9_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try

    '        If (e.KeyChar = ChrW(13)) Then
    '            trvICD9.Select()
    '        Else
    '            trvICD9.SelectedNode = trvICD9.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub


    'Private Sub txtSearchICD9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Trim(txtSearchICD9.Text) <> "" Then
    '            If trvICD9.Nodes(0).GetNodeCount(False) > 0 Then
    '                Dim mychildnode As TreeNode
    '                'child node collection
    '                For i As Integer = 0 To trvICD9.Nodes(0).Nodes.Count - 1
    '                    For Each mychildnode In trvICD9.Nodes(0).Nodes(i).Nodes
    '                        Dim str As String
    '                        str = UCase(Trim(mychildnode.Text))
    '                        If str.Contains(UCase(Trim(txtSearchICD9.Text))) = True Then ''  Mid(str, 1, Len(Trim(txtSearchICD9.Text))) = UCase(Trim(txtSearchICD9.Text)) Then
    '                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
    '                            If Not IsNothing(trvICD9.SelectedNode) Then
    '                                If Not IsNothing(trvICD9.SelectedNode.LastNode) Then
    '                                    trvICD9.SelectedNode = trvICD9.SelectedNode.LastNode
    '                                End If
    '                            End If
    '                            '*************
    '                            trvICD9.SelectedNode = mychildnode
    '                            txtSearchICD9.Focus()
    '                            Exit Sub
    '                        Else
    '                            trvICD9.SelectedNode = trvICD9.Nodes.Item(0)
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        Else
    '            trvICD9.SelectedNode = trvICD9.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub txtsearchCPT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try

    '        If (e.KeyChar = ChrW(13)) Then
    '            trvCPT.Select()
    '        Else
    '            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub



    'Private Sub txtsearchCPT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Trim(txtsearchCPT.Text) <> "" Then
    '            If trvCPT.Nodes(0).GetNodeCount(False) > 0 Then
    '                Dim mychildnode As TreeNode
    '                'child node collection
    '                For i As Integer = 0 To trvCPT.Nodes(0).Nodes.Count - 1
    '                    For Each mychildnode In trvCPT.Nodes(0).Nodes(i).Nodes
    '                        Dim str As String
    '                        str = UCase(Trim(mychildnode.Text))
    '                        If str.Contains(UCase(Trim(txtsearchCPT.Text))) = True Then ''  Mid(str, 1, Len(Trim(txtsearchCPT.Text))) = UCase(Trim(txtsearchCPT.Text)) Then
    '                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
    '                            If Not IsNothing(trvCPT.SelectedNode) Then
    '                                If Not IsNothing(trvCPT.SelectedNode.LastNode) Then
    '                                    trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
    '                                End If
    '                            End If
    '                            '*************
    '                            trvCPT.SelectedNode = mychildnode
    '                            txtsearchCPT.Focus()
    '                            Exit Sub
    '                        Else
    '                            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        Else
    '            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    'Private Sub trvICD9Gallery_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    '    Try
    '        If trvICD9Gallery.Nodes.Item(0).Checked = True And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvICD9Gallery.Nodes(0).Nodes.Count - 1
    '                trvICD9Gallery.Nodes(0).Nodes.Item(i).Checked = True
    '            Next
    '            'trvICD9Gallery.SelectedNode = trvICD9Gallery.Nodes(0).Nodes.Item(0)
    '        ElseIf trvICD9Gallery.Nodes.Item(0).Checked = False And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvICD9Gallery.Nodes(0).Nodes.Count - 1
    '                trvICD9Gallery.Nodes(0).Nodes.Item(i).Checked = False
    '            Next
    '            'trvICD9Gallery.SelectedNode = trvICD9Gallery.Nodes.Item(0)
    '        End If


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Sub



    'Private Sub trvICD9_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    '    Try
    '        If trvICD9.Nodes.Item(0).Checked = True And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvICD9.Nodes(0).Nodes.Count - 1
    '                trvICD9.Nodes(0).Nodes.Item(i).Checked = True
    '                For j As Integer = 0 To trvICD9.Nodes(0).Nodes(i).Nodes.Count - 1
    '                    trvICD9.Nodes(0).Nodes(i).Nodes.Item(j).Checked = True
    '                Next
    '            Next
    '        ElseIf trvICD9Gallery.Nodes.Item(0).Checked = False And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvICD9.Nodes(0).Nodes.Count - 1
    '                trvICD9.Nodes(0).Nodes.Item(i).Checked = False
    '                For j As Integer = 0 To trvICD9.Nodes(0).Nodes(i).Nodes.Count - 1
    '                    trvICD9.Nodes(0).Nodes(i).Nodes.Item(j).Checked = False
    '                Next
    '            Next
    '        End If
    '        If e.Node.Level = 1 Then
    '            If e.Node.Checked = True Then
    '                For k As Integer = 0 To e.Node.Nodes.Count - 1
    '                    e.Node.Nodes.Item(k).Checked = True
    '                Next
    '            Else
    '                For k As Integer = 0 To e.Node.Nodes.Count - 1
    '                    e.Node.Nodes.Item(k).Checked = False
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Sub

    'Private Sub trvCPT_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    '    Try
    '        If trvCPT.Nodes.Item(0).Checked = True And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvCPT.Nodes(0).Nodes.Count - 1
    '                trvCPT.Nodes(0).Nodes.Item(i).Checked = True
    '                For j As Integer = 0 To trvCPT.Nodes(0).Nodes(i).Nodes.Count - 1
    '                    trvCPT.Nodes(0).Nodes(i).Nodes.Item(j).Checked = True
    '                Next
    '            Next
    '        ElseIf trvCPTGallery.Nodes.Item(0).Checked = False And e.Node.Level = 0 Then
    '            For i As Integer = 0 To trvCPT.Nodes(0).Nodes.Count - 1
    '                trvCPT.Nodes(0).Nodes.Item(i).Checked = False
    '                For j As Integer = 0 To trvCPT.Nodes(0).Nodes(i).Nodes.Count - 1
    '                    trvCPT.Nodes(0).Nodes(i).Nodes.Item(j).Checked = False
    '                Next
    '            Next
    '        End If
    '        If e.Node.Level = 1 Then
    '            If e.Node.Checked = True Then
    '                For k As Integer = 0 To e.Node.Nodes.Count - 1
    '                    e.Node.Nodes.Item(k).Checked = True
    '                Next
    '            Else
    '                For k As Integer = 0 To e.Node.Nodes.Count - 1
    '                    e.Node.Nodes.Item(k).Checked = False
    '                Next
    '            End If
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Sub
    '****************Ojeswini_12Jan09****************************
    Private Sub cmbIndicator_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbICD9Gallery.SelectionChangeCommitted
        'oICD9CPT = New DBICD9CPT
        dtICD9Gallery = New DataTable
        strIndicator = Trim(cmbICD9Gallery.Text)
        'rdICD9lftDesc.Checked = True

        dtICD9Gallery = oICD9CPT.GetICD9Gallery(1, strIndicator)
        If Not IsNothing(dtICD9Gallery) Then
            FillICD9GalleryTrv(dtICD9Gallery)
        End If
        'txtsearchICD9Gallery.Clear()
        'Added by Mayuri:20091005
        'To fix BUG ID:#4148
        btnSelectAllICD9Gallery.Visible = True
        btnDeselectAllICD9Gallery.Visible = False
        'commenetd by Mayuri:20091005
        'tlbSelectAll.Visible = True
        'tlbClearAll.Visible = False
        'End Code Added and Commented by Mayuri:20091005
    End Sub

    'Private Sub rdICD9lftCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdICD9lftCode.Checked = True Then
    '        rdICD9lftCode.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdICD9lftCode.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rdICD9lftDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdICD9lftDesc.Checked = True Then
    '        rdICD9lftDesc.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdICD9lftDesc.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rdICD9rhtDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdICD9rhtDesc.Checked = True Then
    '        rdICD9rhtDesc.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdICD9rhtDesc.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rdICD9rhtCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdICD9rhtCode.Checked = True Then
    '        rdICD9rhtCode.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdICD9rhtCode.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rdCPTlftCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdCPTlftCode.Checked = True Then
    '        rdCPTlftCode.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdCPTlftCode.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    'Private Sub rdCPTlftDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If rdCPTlftDesc.Checked = True Then
    '        rdCPTlftDesc.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    Else
    '        rdCPTlftDesc.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    End If
    'End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsetICD9.MouseHover, btnInsertCPT.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsetICD9.MouseLeave, btnInsertCPT.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub tbICD9CPTGallery_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbICD9CPTGallery.SelectedIndexChanged
        'Commented by Mayuri:20091003
        'tlbSelectAll.Visible = True
        'tlbClearAll.Visible = False
        'End Code commented by Mayuri:20091003
        'If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
        '    tlbCurrentICD9.Visible = True
        '    tlbCurrentCPT.Visible = False
        '    tlbICD9Gallery.Visible = False
        '    tlbCPTGallery.Visible = False
        '    pnlICD9Gallery.Visible = True
        '    pnlCurrentICD9.Visible = False
        '    tlbImportIDC9.Visible = True
        '    tlbImportCPT.Visible = False
        '    pnlICD9bottom.Visible = True
        '    pnlICD9Center.Visible = True
        'Else
        '    tlbCurrentCPT.Visible = True
        '    tlbCurrentICD9.Visible = False
        '    tlbICD9Gallery.Visible = False
        '    tlbCPTGallery.Visible = False
        '    pnlCPTGallery.Visible = True
        '    pnlCurrentCPT.Visible = False
        '    tlbImportIDC9.Visible = False
        '    tlbImportCPT.Visible = True
        '    ' pnlcenterCPT.Visible = True
        'End If


        If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then

            FillCurrentICD9(cmbSpecialityICD9.Text)
        Else

            FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)
        End If
    End Sub

    Private Sub cmbSpecialityCurrentCPT_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSpecialityCurrentCPT.SelectionChangeCommitted
        ''dhruv 20100104
        ''FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
        FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)

    End Sub
    Private Sub btnICD9Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9Add.Click
        btnInsetICD9_Click(Nothing, Nothing)
    End Sub
    Private Sub btnSelectAllICD9Gallery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllICD9Gallery.Click
        If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
            gloTrvICD9Gallery.CheckAllNodes()
        End If
        btnSelectAllICD9Gallery.Visible = False
        btnDeselectAllICD9Gallery.Visible = True
    End Sub
    Private Sub btnDeselectAllICD9Gallery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAllICD9Gallery.Click
        If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
            gloTrvICD9Gallery.UncheckAllNodes()
            'ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            ' gloTrvCPTGallery.UncheckAllNodes()
        End If
        btnSelectAllICD9Gallery.Visible = True
        btnDeselectAllICD9Gallery.Visible = False
    End Sub
    Private Sub btnDeselectAllICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAllICD9.Click
        If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
            gloTrvCurrenICD9.UncheckAllNodes()
            'ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            'gloTrvCurrenICD9.UncheckAllNodes()
        End If
        btnSelectAllICD9.Visible = True
        btnDeselectAllICD9.Visible = False
    End Sub

    Private Sub btnSelectAllICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllICD9.Click
        If tbICD9CPTGallery.SelectedTab.Text = "ICD9" Then
            gloTrvCurrenICD9.CheckAllNodes()
            'ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            'gloTrvCurrenICD9.CheckAllNodes()
        End If
        btnSelectAllICD9.Visible = False
        btnDeselectAllICD9.Visible = True
    End Sub

    Private Sub btnSelectAllCPTGallery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllCPTGallery.Click
        If tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            gloTrvCPTGallery.CheckAllNodes()
        End If

        btnSelectAllCPTGallery.Visible = False
        btnDeselectAllCPTGallery.Visible = True


    End Sub

    Private Sub btnDeselectAllCPTGallery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAllCPTGallery.Click
        If tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            gloTrvCPTGallery.UncheckAllNodes()
            'ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            'gloTrvCurrenICD9.CheckAllNodes()
        End If
        btnSelectAllCPTGallery.Visible = True
        btnDeselectAllCPTGallery.Visible = False

    End Sub

 
    Private Sub cmbICD9Current_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Code Commented by Mayuri:20091005 No need of Catgeory combobox in ICD9Current
        'FillCurrentICD9()
        'End Code Commented by Mayuri:20091005
    End Sub


    'Private Sub cmbSpecialityICD9_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSpecialityICD9.SelectedIndexChanged
    '    'Code Added by Mayuri:20091005
    '    'To fill Treeview control ICD9 and CPTCurrent according to selected Speciality
    '    ''If tbICD9CPTGallery.SelectedTab Is tbPageICD9 Then
    '    'To fill Treeview control ICD9Current according to= selected Speciality

    '    FillCurrentICD9(cmbSpecialityICD9.Text)


    '    ''Dhruv 20100104 
    '    'Else
    '    '    'To fill Treeview control CPTCurrent according to selected Speciality
    '    '    FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
    '    ''End If
    '    'Commented by Mayuri:20091005
    '    'fill treeviews ICD9Current and CPTCurrent according to selected speciality  in FillCurrentICD9() and FillCurrentCPT() functions
    '    'FillSpeciality()
    '    'End code commented by Mayuri:20091005
    'End Sub
    'Code Added by Mayuri:20091008
    'To Remove selected CurrentICD9 from CurrentICD9 treeview control

    Private Sub btnICD9Remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9Remove.Click

        Try
            'Code Added on 20091026
            Me.Cursor = Cursors.WaitCursor
            'End Code Added on 20091026
            _isSelectedICD9Gallery = False
            Dim oCollection As Collection = gloTrvCurrenICD9.SelectedNodes()
            Dim _deletedCounter As Integer = 0
            If IsNothing(oCollection) = False Then
                If oCollection.Count > 0 Then
                    ''dhruv 20091128-----------------------------------------------------------------------------------------------------
                    ''it is used to provide the message before deleting
                    If (MessageBox.Show("Are you sure you want to remove selected ICD9?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        Dim oICD9 As New clsICD9(GetConnectionString)
                        For i As Integer = 1 To oCollection.Count
                            Dim oNode As gloUserControlLibrary.myTreeNode
                            oNode = CType(oCollection(i), gloUserControlLibrary.myTreeNode)
                            gloTrvCurrenICD9.Nodes.Remove(oNode)
                            'oclsICD9.DeleteICD9(oNode.ID, oNode.Code)
                            oICD9.DeletICD9(oNode.ID, _isSelectedICD9Gallery)
                            UpdateLog("DeletICD9 " & i)
                            'oclsICD9.DeleteCurrentICD9(oNode.ID)
                            _deletedCounter = _deletedCounter + 1
                        Next
                        oICD9.Dispose()

                        ''Dhruv 20100105 
                        ''when the CPT's are deleted it will shows how many of is deleted
                        MessageBox.Show(_deletedCounter & "  ICD9(s) removed", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    gloTrvCurrenICD9.Refresh()
                    FillCurrentICD9(cmbSpecialityICD9.Text)
                    ''If row is not selected then provide the message box..
                Else
                    MessageBox.Show("Select the ICD9 to remove", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Code Added on 20091026
        Finally
            Me.Cursor = Cursors.Default
            'End Code Added on 20091026
        End Try
    End Sub
    'End Code Added by Mayuri:20091008

    'Code Added by Mayuri:20091008
    'To Remove selected CurrentCPT from CurrentCPT treeview control
    Private Sub btnCPTRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPTRemove.Click
        Try
            'Code Added on 20091026
            Me.Cursor = Cursors.WaitCursor
            'End Code Added on 20091026
            _isSelectedCPTGallery = False
            Dim oCollection As Collection = gloTrvCurrentCPT.SelectedNodes()
            ''Dhruv 20100105 
            ''No of the CPT's that has been deleted
            Dim _deletedCounter As Integer = 0

            If IsNothing(oCollection) = False Then
                If oCollection.Count > 0 Then
                    ''dhruv 20091128-----------------------------------------------------------------------------------------------------
                    ''it is used to provide the message before deleting

                    If (MessageBox.Show("Are you sure you want to remove selected CPT?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                        Dim oCPT As New DBICD9CPT(GetConnectionString)
                        For i As Integer = 1 To oCollection.Count
                            Dim oNode As gloUserControlLibrary.myTreeNode
                            oNode = CType(oCollection(i), gloUserControlLibrary.myTreeNode)
                            gloTrvCurrentCPT.Nodes.Remove(oNode)
                            oCPT.DeleteCPT(oNode.ID, _isSelectedCPTGallery)
                            UpdateLog("DeleteCPT " & i)
                            'oclsICD9.DeleteCurrentCPT(oNode.ID, _isSelectedTreeview)
                            _deletedCounter = _deletedCounter + 1
                        Next
                        oCPT.Dispose()
                        ''Dhruv 20100105 
                        ''when the CPT's are deleted it will shows how many of is deleted
                        MessageBox.Show(_deletedCounter & "  CPT(s) removed", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    gloTrvCurrentCPT.Refresh()
                    'FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
                    ''dhruv 20100104
                    FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)

                    ''If row is not selected then provide the message box..
                Else
                    MessageBox.Show("Select the CPT to remove", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Code Added on 20091026
        Finally
            Me.Cursor = Cursors.Default
            'End Code Added on 20091026
        End Try
    End Sub
    'End Code Added by Mayuri:20091008
    'Code Added by Mayuri:20091008
    'To select All Checkboxes of CurrentCPT treeview control
    Private Sub btnSelectAllCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAllCPT.Click
        If tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            gloTrvCurrentCPT.CheckAllNodes()
            'ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            'gloTrvCurrenICD9.CheckAllNodes()
        End If
        btnSelectAllCPT.Visible = False
        btnDeselectAllCPT.Visible = True
    End Sub
    'Code Added by Mayuri:20091008
    'To Uncheck All Checkboxes of CurrentCPT treeview control
    Private Sub btnDeselectAllCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeselectAllCPT.Click
        If tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            gloTrvCurrentCPT.UncheckAllNodes()
            'ElseIf tbICD9CPTGallery.SelectedTab.Text = "CPT" Then
            'gloTrvCurrenICD9.CheckAllNodes()
        End If
        btnSelectAllCPT.Visible = True
        btnDeselectAllCPT.Visible = False
    End Sub
    'End Code added by Mayuri:20091008

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub




    '    Private Sub cmbSpecialityICD9CPT_SelectionChangeCommitted_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSpecialityICD9CPT.SelectedIndexChanged
    '        'Code Added by Mayuri:20091005
    '        'To fill Treeview control ICD9 and CPTCurrent according to selected Speciality
    '        ''If tbICD9CPTGallery.SelectedTab Is tbPageCPT Then
    '        'To fill Treeview control ICD9Current according to selected Speciality
    '        'FillCurrentICD9(cmbSpecialityICD9CPT.Text)

    '        FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)

    '        'Else
    '        '    'To fill Treeview control CPTCurrent according to selected Speciality
    '        '    FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)
    '        ''End If
    '        'Commented by Mayuri:20091005
    '        'fill treeviews ICD9Current and CPTCurrent according to selected speciality  in FillCurrentICD9() and FillCurrentCPT() functions
    '        'FillSpeciality()
    '        'End code commented by Mayuri:20091005
    '    End Sub


    Private Sub cmbSpecialityICD9_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSpecialityICD9.SelectionChangeCommitted
        'Code Added by Mayuri:20091005
        'To fill Treeview control ICD9 and CPTCurrent according to selected Speciality
        ''If tbICD9CPTGallery.SelectedTab Is tbPageICD9 Then
        'To fill Treeview control ICD9Current according to= selected Speciality

        FillCurrentICD9(cmbSpecialityICD9.Text)


        ''Dhruv 20100104 
        'Else
        ''To fill Treeview control CPTCurrent according to selected Speciality
        'FillCurrentCPT(cmbSpecialityICD9.Text, cmbSpecialityCurrentCPT.Text)
        ''End If
        'Commented by Mayuri:20091005
        'fill treeviews ICD9Current and CPTCurrent according to selected speciality  in FillCurrentICD9() and FillCurrentCPT() functions
        'FillSpeciality()
        'End code commented by Mayuri:20091005
    End Sub

    Private Sub cmbSpecialityICD9CPT_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSpecialityICD9CPT.SelectionChangeCommitted
        'Code Added by Mayuri:20091005
        'To fill Treeview control ICD9 and CPTCurrent according to selected Speciality
        ''If tbICD9CPTGallery.SelectedTab Is tbPageCPT Then
        'To fill Treeview control ICD9Current according to selected Speciality
        'FillCurrentICD9(cmbSpecialityICD9CPT.Text)

        FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)

        'Else
        '    'To fill Treeview control CPTCurrent according to selected Speciality
        '    FillCurrentCPT(cmbSpecialityICD9CPT.Text, cmbSpecialityCurrentCPT.Text)
        ''End If
        'Commented by Mayuri:20091005
        'fill treeviews ICD9Current and CPTCurrent according to selected speciality  in FillCurrentICD9() and FillCurrentCPT() functions
        'FillSpeciality()
        'End code commented by Mayuri:20091005
    End Sub


    Private Sub btnCPTAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPTAdd.Click

        btnInsertCPT_Click(Nothing, Nothing)

    End Sub

    Private Sub btnDeselectAllICD9Gallery_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAllICD9Gallery.MouseHover
        '  btnDeselectAllICD9Gallery.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub btnSelectAllICD9Gallery_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllICD9Gallery.MouseHover
        '  btnSelectAllICD9Gallery.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub btnICD9Add_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9Add.MouseHover
        '  btnICD9Add.FlatStyle = FlatStyle.Flat
    End Sub
End Class

'****************************************************************

