Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloemr.gloStream.DiseaseManagement
Imports gloUserControlLibrary


Public Class frmDM_PatientCriteria

#Region " Private Variables "
    Private mCriteriaID As Int64
    Private mPatientID As Int64
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
#End Region

    Dim oDM As New DiseaseManagement
    ' Dim dtPatientCriterias As DataTable
    Dim strParentToAssociate As String = "Labs"

#Region " Constructors "
    Public Sub New(ByVal CriteriaID As Int64, ByVal PatientID As Int64)
        InitializeComponent()
        mCriteriaID = CriteriaID
        mPatientID = PatientID
    End Sub
#End Region

    Private Sub frmDM_PatientCriteria_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmDM_PatientCriteria_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadPatientStrip()
        PopulateAssocaitedInfo(1)

        If mCriteriaID > 0 Then
            AddHealthPlanNode(mCriteriaID, False)
        Else
            FillTree() ''THIS IS EMPTY PARENT NODES TREE
        End If
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub LoadPatientStrip()
        ''Show the patient details based on id passed
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(mPatientID, gloUC_PatientStrip.enumFormName.HealthPlan)
        _PatientStrip.Dock = DockStyle.Top
        ''Add the patient Strip control to the panel
        'If pnlMain.Controls.Contains(_PatientStrip) = False Then
        Me.pnlMain.Controls.Add(_PatientStrip)
        'pnlToolStrip.SendToBack()
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)
        'End If
    End Sub

    Private Sub PopulateAssocaitedInfo(ByVal ID As Int32)
        pnlbtnLab.Dock = DockStyle.Bottom
        btnLab.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLab.BackgroundImageLayout = ImageLayout.Stretch
        btnLab.Tag = "UnSelected"

        pnlbtnReferrals.Dock = DockStyle.Bottom
        btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        btnReferrals.Tag = "UnSelected"

        pnlbtnRx.Dock = DockStyle.Bottom
        btnRx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRx.BackgroundImageLayout = ImageLayout.Stretch
        btnRx.Tag = "UnSelected"

        pnlbtnRadiologyTest.Dock = DockStyle.Bottom
        btnRadiologyTest.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiologyTest.BackgroundImageLayout = ImageLayout.Stretch
        btnRadiologyTest.Tag = "UnSelected"

        pnlbtnGuideline.Dock = DockStyle.Bottom
        btnGuideline.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
        btnGuideline.Tag = "UnSelected"
        If ID = 1 Then
            With btnLab
                pnlbtnLab.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillLabTest()
        ElseIf ID = 2 Then

            With btnReferrals
                pnlbtnReferrals.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillReferrals()
        ElseIf ID = 3 Then
            With btnRx
                pnlbtnRx.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillRx()
        ElseIf ID = 4 Then
            With btnRadiologyTest
                pnlbtnRadiologyTest.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillRadiologyTest()
        ElseIf ID = 5 Then
            With btnGuideline
                pnlbtnGuideline.Dock = DockStyle.Top
                .Tag = "Selected"
                '.ForeColor = Color.Black
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                .BackgroundImageLayout = ImageLayout.Stretch
                .BringToFront()
            End With
            trvTriggers.BringToFront()
            FillGuideline()
        End If

    End Sub

    Private Sub FillLabTest_old()
        trvTriggers.Nodes.Clear()
        Dim rootnode As myTreeNode = Nothing
        rootnode = New myTreeNode("Labs", -1)
        rootnode.ImageIndex = 6
        rootnode.SelectedImageIndex = 6
        trvTriggers.Nodes.Add(rootnode)
        Dim _C As Integer
        ''''Create object for the class
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oLabsModule As gloStream.DiseaseManagement.Supporting.LabModuleTests

        ''''assign Lab Test and Result to collection
        oLabsModule = oDM.LabModuleTests

        If Not oLabsModule Is Nothing Then
            If oLabsModule.Count > 0 Then

                'Fill Test
                For _C = 1 To oLabsModule.Count

                    rootnode = New myTreeNode(oLabsModule(_C).Name, oLabsModule(_C).TestID)
                    rootnode.ImageIndex = 11
                    rootnode.SelectedImageIndex = 11
                    trvTriggers.Nodes.Item(0).Nodes.Add(rootnode)

                    'For _G = 1 To oLabsModule.Item(_C).LabModuleTestResults.Count
                    '    'trAssociates.Nodes.Item(1).Nodes.Add(New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID))
                    '    Dim mychildnode As myTreeNode
                    '    mychildnode = New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID)
                    '    'trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
                    '    rootnode.Nodes.Add(mychildnode)

                    'Next
                Next
                trvTriggers.ExpandAll()
            End If
            oLabsModule.Dispose()
            oLabsModule = Nothing
        End If
        oDM.Dispose()
        oDM = Nothing

    End Sub
    Public Sub FillLabTest()
        ''''Create object for the class
        strParentToAssociate = "Labs"
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        ' Dim oLabsModule As gloStream.DiseaseManagement.Supporting.LabModuleTests

        Dim dtLabsModule As DataTable
        dtLabsModule = oDM.LabModuleTest
        GloUC_trvAssociates.Clear()
        If Not dtLabsModule Is Nothing Then
            GloUC_trvAssociates.DataSource = dtLabsModule
            GloUC_trvAssociates.ValueMember = dtLabsModule.Columns(1).ColumnName
            GloUC_trvAssociates.DescriptionMember = dtLabsModule.Columns(0).ColumnName
            GloUC_trvAssociates.CodeMember = dtLabsModule.Columns(0).ColumnName
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvAssociates.FillTreeView()
        End If
        oDM.Dispose()
        oDM = Nothing
        ' oLabsModule = Nothing
    End Sub

    Private Sub FillReferrals_old()
        Try
            trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing
            'rootnode = New myTreeNode("Templates", -1)
            'rootnode.ImageIndex = 0
            'rootnode.SelectedImageIndex = 0
            ' trvTriggers.Nodes.Add(rootnode)

            Dim newNode As New TreeNode
            Dim objMyTreeView As myTreeNode
            Dim objTemplateGallery As New clsTemplateGallery
            Dim objCategory As myTreeNode
            Dim objTemplate As myTreeNode
            Dim dvTemplate As DataView
            Dim dt_temp As DataTable = objTemplateGallery.GetAllCategory

            Dim j As Integer
            trvTriggers.Nodes.Clear()

            objMyTreeView = New myTreeNode("Referrals", 0)
            objMyTreeView.ImageIndex = 8  '' Category ICon
            objMyTreeView.SelectedImageIndex = 8 '' Category ICon
            trvTriggers.Nodes.Add(objMyTreeView)

            Dim ValueMember As Int64
            Dim DisplayMember As String

            For i As Integer = 0 To dt_temp.Rows.Count - 1
                'Dim ValueMember As Int64
                'Dim DisplayMember As String
                ValueMember = dt_temp.Rows(i)(0)
                DisplayMember = dt_temp.Rows(i)(1)
                If DisplayMember = "Referral Letter" Then
                    objCategory = New myTreeNode(DisplayMember, ValueMember)
                    'objCategory.ImageIndex = 7 '''' Template ICon
                    'objCategory.SelectedImageIndex = 7 '''' Template ICon
                    'objMyTreeView.Nodes.Add(objCategory)

                    '  dvTemplate = New DataView
                    dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                    For j = 0 To dvTemplate.Table.Rows.Count - 1
                        ''Dim ValueMember As Int64
                        ''Dim DisplayMember As String
                        ValueMember = dvTemplate.Table.Rows(j)(0)
                        DisplayMember = dvTemplate.Table.Rows(j)(1)
                        objTemplate = New myTreeNode(DisplayMember, ValueMember)
                        objTemplate.ImageIndex = 11 '''' Play ICon
                        objTemplate.SelectedImageIndex = 11 '''' Play ICon
                        'objMyTreeView.Nodes.Add(objTemplate)
                        'objCategory.Nodes.Add(objTemplate)
                        'objCategory.EnsureVisible()
                        'objCategory.ExpandAll()
                        objMyTreeView.Nodes.Add(objTemplate)

                    Next
                    objMyTreeView.ExpandAll()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FillReferrals()
        Try
            strParentToAssociate = "Referrals"
            Dim rootnode As myTreeNode = Nothing

            Dim newNode As New TreeNode
            'Dim objMyTreeView As myTreeNode
            Dim objTemplateGallery As New clsTemplateGallery
            Dim objCategory As myTreeNode
            'Dim objTemplate As myTreeNode
            Dim dvTemplate As DataView
            Dim dt_temp As DataTable = objTemplateGallery.GetAllCategory

            'Dim j As Integer
            Dim ValueMember As Int64
            Dim DisplayMember As String

            For i As Integer = 0 To dt_temp.Rows.Count - 1

                ValueMember = dt_temp.Rows(i)(0)
                DisplayMember = dt_temp.Rows(i)(1)
                If DisplayMember = "Referral Letter" Then
                    objCategory = New myTreeNode(DisplayMember, ValueMember)

                    '  dvTemplate = New DataView
                    dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                    Dim dt As DataTable
                    dt = dvTemplate.Table
                    If Not dt Is Nothing Then
                        GloUC_trvAssociates.Clear()
                        GloUC_trvAssociates.DataSource = dt
                        GloUC_trvAssociates.ValueMember = dt.Columns(0).ColumnName
                        GloUC_trvAssociates.DescriptionMember = dt.Columns(1).ColumnName
                        GloUC_trvAssociates.CodeMember = dt.Columns(1).ColumnName
                        GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                        GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                        GloUC_trvAssociates.FillTreeView()
                    End If

                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub FillRx_old(Optional ByVal SearchDrug As String = "")
        trvTriggers.Nodes.Clear()
        Dim rootnode As myTreeNode = Nothing
        rootnode = New myTreeNode("Rx", -1)
        rootnode.ImageIndex = 9
        rootnode.SelectedImageIndex = 9
        trvTriggers.Nodes.Add(rootnode)
        Dim oDrugs As gloStream.DiseaseManagement.Supporting.Drugs
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oNode As myTreeNode
        Try

            oDrugs = oDM.Drugs(SearchDrug)

            With rootnode
                ' .BeginUpdate()

                If Not oDrugs Is Nothing Then
                    For i As Int64 = 1 To oDrugs.Count
                        'oNode = New myTreeNode(oDrugs(i).Name, oDrugs(i).ID)
                        oNode = New myTreeNode
                        With oNode
                            .Text = oDrugs(i).Name
                            .Key = oDrugs(i).ID
                            .DMTemplateName = oDrugs(i).DrugName
                            .DrugName = oDrugs(i).DrugName
                            .Dosage = oDrugs(i).Dosage

                            'sarika DM Denormalization for Rx on 20090410
                            .DrugForm = oDrugs(i).DrugForm
                            .Route = oDrugs(i).Route
                            .Duration = oDrugs(i).Duration
                            .Frequency = oDrugs(i).Frequency

                            .NDCCode = oDrugs(i).NDCCode
                            .DrugQtyQualifier = oDrugs(i).DrugQtyQualifier
                            .IsNarcotics = oDrugs(i).IsNarcotics
                            '--

                        End With
                        oNode.ImageIndex = 11
                        oNode.SelectedImageIndex = 11
                        .Nodes.Add(oNode)
                        oNode = Nothing
                    Next
                    oDrugs.Dispose()
                    oDrugs = Nothing
                End If
                ' .EndUpdate()
                .ExpandAll()
            End With
            oDM.Dispose()
            oDM = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FillRx()
        strParentToAssociate = "Drugs"
        Dim dtdrugs As DataTable
        Dim objclsDrugs As New clsDrugs
        Try

            dtdrugs = objclsDrugs.GetAllDrugs(1).Table
            If Not IsNothing(dtdrugs) Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dtdrugs
                If dtdrugs.Rows.Count > 1000 Then
                    GloUC_trvAssociates.MaximumNodes = 1000
                End If
                GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtdrugs.Columns(3).ColumnName)
                GloUC_trvAssociates.ValueMember = Convert.ToString(dtdrugs.Columns(0).ColumnName)
                GloUC_trvAssociates.CodeMember = Convert.ToString(dtdrugs.Columns(1).ColumnName)
                'GloUC_trvAssociates.CodeMember = Convert.ToString(dtdrugs.Columns(3).ColumnName)
                'GloUC_trvAssociates.DrugFormMember = dtdrugs.Columns("DrugForm").ColumnName
                GloUC_trvAssociates.RouteMember = Convert.ToString(dtdrugs.Columns(4).ColumnName)
                GloUC_trvAssociates.NDCCodeMember = Convert.ToString(dtdrugs.Columns(10).ColumnName)
                'GloUC_trvAssociates.IsNarcoticsMember = dtdrugs.Columns("IsNarcotic").ColumnName
                'GloUC_trvAssociates.FrequencyMember = dtdrugs.Columns("Frequency").ColumnName
                'GloUC_trvAssociates.DurationMember = dtdrugs.Columns("Duration").ColumnName
                'GloUC_trvAssociates.DrugQtyQualifierMember = dtdrugs.Columns("DrugQtyQualifier").ColumnName

                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                GloUC_trvAssociates.FillTreeView()
            End If
            objclsDrugs.Dispose()
            objclsDrugs = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillRadiologyTest_old()
        Try
            trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing

            rootnode = New myTreeNode("Orders", -1)
            rootnode.ImageIndex = 7
            rootnode.SelectedImageIndex = 7
            trvTriggers.Nodes.Add(rootnode)
            Dim _C As Integer, _G As Integer, _T As Integer
            Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
            Dim oLabs As gloStream.DiseaseManagement.Supporting.Orders

            oLabs = oDM.Orders
            If Not oLabs Is Nothing Then
                If oLabs.Count > 0 Then

                    'Fill Category
                    For _C = 1 To oLabs.Count

                        'rootnode = New myTreeNode(oLabs(_C).Category, oLabs(_C).ID)
                        'trAssociates.Nodes.Item(0).Nodes.Add(rootnode)
                        For _G = 1 To oLabs.Item(_C).OrderGroups.Count
                            'Dim mychildnode As myTreeNode
                            'mychildnode = New myTreeNode(oLabs.Item(_C).LabGroups(_G).Name, oLabs.Item(_C).LabGroups(_G).ID)
                            'rootnode.Nodes.Add(mychildnode)
                            'Fill Tests Start
                            For _T = 1 To oLabs.Item(_C).OrderGroups(_G).Tests.Count
                                Dim mychildnode_ As myTreeNode
                                mychildnode_ = New myTreeNode(oLabs.Item(_C).OrderGroups(_G).Tests(_T).Description, oLabs.Item(_C).OrderGroups(_G).Tests(_T).ID)
                                ' rootnode.Nodes.Add(mychildnode_)
                                mychildnode_.ImageIndex = 11
                                mychildnode_.SelectedImageIndex = 11
                                trvTriggers.Nodes.Item(0).Nodes.Add(mychildnode_)
                            Next
                        Next
                        'Fill Tests Finish
                    Next ' For _G = 1 To oLabs.Item(_C).LabGroups.Count
                    'Fill Groups & Category
                    trvTriggers.ExpandAll()
                End If
                oLabs.Dispose()
                oLabs = Nothing
            End If
            oDM.Dispose()
            oDM = Nothing
            'oLabs = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FillRadiologyTest()
        Try
            strParentToAssociate = "Orders"
            Dim dtOrders As DataTable
            Dim oCls As New gloStream.DiseaseManagement.Common.Criteria
            Try
                dtOrders = oCls.OrdersTable
                If Not IsNothing(dtOrders) Then
                    GloUC_trvAssociates.Clear()
                    GloUC_trvAssociates.DataSource = dtOrders
                    GloUC_trvAssociates.CodeMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                    GloUC_trvAssociates.ValueMember = Convert.ToString(dtOrders.Columns(0).ColumnName)
                    GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                    GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                    GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                    GloUC_trvAssociates.FillTreeView()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            oCls.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillGuideline_old()
        Try
            trvTriggers.Nodes.Clear()
            Dim rootnode As myTreeNode = Nothing
            rootnode = New myTreeNode("Guidelines", -1)
            rootnode.ImageIndex = 8
            rootnode.SelectedImageIndex = 8
            trvTriggers.Nodes.Add(rootnode)
            'This code is commented by shilpa 
            Dim oGuideLines As gloStream.DiseaseManagement.Supporting.ItemDetails
            'Dim oCategories As New gloStream.DiseaseManagement.Common.GuidelinesType
            Dim oDm As New gloStream.DiseaseManagement.Common.Guidelines

            Dim oNode As myTreeNode
            '    oCategories = oDm.Categories

            'fill category of history

            '.Nodes.Clear()

            'If Not oCategories Is Nothing Then

            With trvTriggers
                'For j As Integer = 0 To .Nodes.Item(0).GetNodeCount(False) - 1

                'oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
                oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.PatientEducation)

                For i As Int16 = 1 To oGuideLines.Count
                    oNode = New myTreeNode
                    oNode.Text = oGuideLines(i).Description
                    oNode.Tag = oGuideLines(i).ID
                    oNode.Key = oGuideLines(i).ID
                    oNode.DMTemplate = oGuideLines(i).Template
                    oNode.ImageIndex = 11
                    oNode.SelectedImageIndex = 11
                    .Nodes.Item(0).Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oGuideLines.Dispose()
                oGuideLines = Nothing
                '   oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
                oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.WellnessGuidelines)

                For i As Int16 = 1 To oGuideLines.Count
                    oNode = New myTreeNode
                    oNode.Text = oGuideLines(i).Description
                    oNode.Tag = oGuideLines(i).ID
                    oNode.Key = oGuideLines(i).ID
                    oNode.DMTemplate = oGuideLines(i).Template
                    oNode.ImageIndex = 11
                    oNode.SelectedImageIndex = 11
                    .Nodes.Item(0).Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oGuideLines.Dispose()
                oGuideLines = Nothing
                'oGuideLines = New gloStream.DiseaseManagement.Supporting.ItemDetails
                oGuideLines = oDm.Guidelines(gloStream.DiseaseManagement.Common.GuidelinesType.PreventiveServices)

                For i As Int16 = 1 To oGuideLines.Count
                    oNode = New myTreeNode
                    oNode.Text = oGuideLines(i).Description
                    oNode.Tag = oGuideLines(i).ID
                    oNode.Key = oGuideLines(i).ID
                    oNode.DMTemplate = oGuideLines(i).Template
                    oNode.ImageIndex = 11
                    oNode.SelectedImageIndex = 11
                    .Nodes.Item(0).Nodes.Add(oNode)
                    oNode = Nothing
                Next
                oGuideLines.Dispose()
                oGuideLines = Nothing

                trvTriggers.ExpandAll()
                ' Next
            End With

            '            End If

            oDm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillGuideline()
        Try
            strParentToAssociate = "Guidelines"
            '   Dim oGuideLines As gloStream.DiseaseManagement.Supporting.ItemDetails
            Dim oCategories As New gloStream.DiseaseManagement.Common.GuidelinesType
            Dim oDm As New gloStream.DiseaseManagement.Common.Guidelines
            Dim dt As DataTable = oDm.GuidelinesTables("")
            ' Dim oNode As myTreeNode
            If Not oCategories Is Nothing Then

                If Not dt Is Nothing Then
                    GloUC_trvAssociates.Clear()
                    GloUC_trvAssociates.DataSource = dt
                    GloUC_trvAssociates.ValueMember = dt.Columns(0).ColumnName
                    GloUC_trvAssociates.DescriptionMember = dt.Columns(1).ColumnName
                    GloUC_trvAssociates.ImageObject = dt.Columns("sDescription").ColumnName
                    GloUC_trvAssociates.CodeMember = dt.Columns(1).ColumnName
                    GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                    GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                    GloUC_trvAssociates.FillTreeView()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillTree()
        Dim associatenode As New myTreeNode("Orders", -1)
        associatenode.ImageIndex = 12
        associatenode.SelectedImageIndex = 12
        trvHealthPlan.Nodes.Add(associatenode)

        Dim MyChild As New myTreeNode
        MyChild.Text = "Labs"
        MyChild.Tag = DiseaseManagement.TemplateCategoryID.Labs.GetHashCode
        MyChild.Key = -1
        MyChild.ImageIndex = 6
        MyChild.SelectedImageIndex = 6
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Orders"
        MyChild.Tag = DiseaseManagement.TemplateCategoryID.Radiology.GetHashCode
        MyChild.Key = -1
        MyChild.ImageIndex = 7
        MyChild.SelectedImageIndex = 7
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Guidelines"
        MyChild.Tag = DiseaseManagement.TemplateCategoryID.Guidelines.GetHashCode
        MyChild.Key = -1
        MyChild.ImageIndex = 8
        MyChild.SelectedImageIndex = 8
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Drugs"
        MyChild.Tag = DiseaseManagement.TemplateCategoryID.Rx.GetHashCode
        MyChild.Key = -1
        MyChild.ImageIndex = 9
        MyChild.SelectedImageIndex = 9
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Referrals"
        MyChild.Tag = DiseaseManagement.TemplateCategoryID.Referrals.GetHashCode
        MyChild.Key = -1
        MyChild.ImageIndex = 10
        MyChild.SelectedImageIndex = 10
        associatenode.Nodes.Add(MyChild)

        trvHealthPlan.ExpandAll()

    End Sub

    Private Sub AddTriggerAssociates(ByVal mynode As myTreeNode, ByVal strType As String)

        For Each myRootNode As myTreeNode In trvHealthPlan.Nodes(0).Nodes
            If myRootNode.Text = strType Then
                ''Loop for all field nodes in each Root node
                For Each myTargetNode As myTreeNode In myRootNode.Nodes
                    ''Check whether the node already exists
                    '  If myRootNode.Text = "Guidelines" Then
                    If myTargetNode.Text = mynode.Text Then
                        ''If present do nothing
                        Exit Sub
                    End If
                    'Else
                    'If myTargetNode.Key = mynode.Key Then
                    '    ''If present do nothing
                    '    Exit Sub
                    'End If
                    'End If

                Next
                ''Map all the node values to the associated node
                Dim Associatenode As myTreeNode
                Associatenode = mynode.Clone
                Associatenode.Key = mynode.Key
                Associatenode.Text = mynode.Text
                Associatenode.Tag = mynode.Tag
                'sarika DM Denormalization 20090402
                Associatenode.DMTemplateName = mynode.Text
                Associatenode.DMTemplate = mynode.DMTemplate
                '-------


                If myRootNode.Text = "Drugs" Then
                    Associatenode.DrugName = mynode.DrugName
                    Associatenode.DMTemplateName = mynode.DrugName
                    Associatenode.Dosage = mynode.Dosage

                    'sarika DM Denormalization 20090410
                    Associatenode.DrugForm = mynode.DrugForm
                    Associatenode.Duration = mynode.Duration
                    Associatenode.Frequency = mynode.Frequency

                    Associatenode.NDCCode = mynode.NDCCode
                    Associatenode.Route = mynode.Route
                    Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
                    Associatenode.IsNarcotics = mynode.IsNarcotics
                    '-----

                End If

                Associatenode.ImageIndex = 0
                Associatenode.SelectedImageIndex = 0
                myRootNode.Nodes.Add(Associatenode)
            End If
        Next
        trvHealthPlan.ExpandAll()
    End Sub
    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal targetNode As myTreeNode)

        ''Map all the node values to the associated node
        Dim Associatenode As myTreeNode
        Associatenode = mynode.Clone
        Associatenode.Key = mynode.Key
        Associatenode.Text = mynode.Text
        Associatenode.Tag = mynode.Key
        Associatenode.NodeName = mynode.Text
        If targetNode.Text = "Guidelines" Then
            Associatenode.DMTemplate = mynode.DMTemplate
            Associatenode.DMTemplate = mynode.DMTemplateName
        End If
        Associatenode.DMTemplateName = mynode.DMTemplateName
        If targetNode.Text = "Drugs" Then
            Associatenode.DMTemplate = mynode.DMTemplateName
            Associatenode.DrugName = mynode.DrugName
            Associatenode.Dosage = mynode.Dosage

            'sarika DM Denormalization 20090410
            Associatenode.DrugForm = mynode.DrugForm
            Associatenode.Duration = mynode.Duration
            Associatenode.Frequency = mynode.Frequency

            Associatenode.NDCCode = mynode.NDCCode
            Associatenode.Route = mynode.Route
            Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
            Associatenode.IsNarcotics = mynode.IsNarcotics
            '-----

        End If

        Associatenode.ImageIndex = 0
        Associatenode.SelectedImageIndex = 0
        Associatenode.TemplateResult = Nothing
        targetNode.Nodes.Add(Associatenode)
        trvHealthPlan.ExpandAll()
    End Sub
    Private Sub AddCriteriaNodes(ByVal _TransId As Int64, ByVal _TriggerId As Int64, ByVal _TriggerName As String, ByVal _Recurring As Boolean, ByVal rootNode As myTreeNode, ByVal strType As String, ByVal imgIndex As Int32)
        Try
            Dim blnExist As Boolean = False
            Dim parentNode As myTreeNode = Nothing
            Dim strDueDesc As String = ""
            ''Loop through Criteria node to check for the parent node of 'strtype' is available
            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = strType Then
                    parentNode = TargetNode
                    blnExist = True
                End If
            Next
            ''if not exists then add the node to the root node as parent node
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = strType
                parentNode.ImageIndex = imgIndex
                parentNode.SelectedImageIndex = imgIndex
                rootNode.Nodes.Add(parentNode)
            End If

            ''Map all the node values to the associated node
            Dim Associatenode As New myTreeNode
            Associatenode.Key = _TriggerId

            Associatenode.Tag = _TransId
            Associatenode.NodeName = _TriggerName
            ''Get the Trigger due details and map it to Triggerdetails object 
            Dim oDetails As DataTable = oDM.FindDueTriggerDetails(_TransId, _Recurring)
            If Not oDetails Is Nothing Then
                If oDetails.Rows.Count > 0 Then
                    Dim objTrigger As New TriggerDetails
                    objTrigger.TransId = _TransId
                    objTrigger.TriggerId = _TriggerId
                    'If Not IsDBNull(CType(oDetails.Rows(0)("DM_nCriteriaID"), String)) Then
                    '    objTrigger.CriteriaId = CType(oDetails.Rows(0)("DM_nCriteriaID"), String)
                    'End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_DueType")) Then
                        objTrigger.DueType = CType(oDetails.Rows(0)("DM_DueType"), String)
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_DueValue")) Then
                        objTrigger.DueValue = CType(oDetails.Rows(0)("DM_DueValue"), String)
                        If oDetails.Rows(0)("DM_DueValue") <> "" Then
                            strDueDesc = " " & "-" & " " & " Due on " & objTrigger.DueType & " " & objTrigger.DueValue
                        End If
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_bIsRecurring")) Then
                        objTrigger.Recurring = CType(oDetails.Rows(0)("DM_bIsRecurring"), Boolean)
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_sReason")) Then
                        objTrigger.Reason = CType(oDetails.Rows(0)("DM_sReason"), String)
                        If oDetails.Rows(0)("DM_sReason") <> "" Then
                            strDueDesc = strDueDesc & " " & "-" & " " & "Reason :" & objTrigger.Reason
                        End If
                    End If

                    If Not IsDBNull(oDetails.Rows(0)("DM_sNotes")) Then
                        objTrigger.Notes = CType(oDetails.Rows(0)("DM_sNotes"), String)
                        If oDetails.Rows(0)("DM_sNotes") <> "" Then
                            strDueDesc = strDueDesc & " " & "-" & " " & "Notes :" & objTrigger.Notes
                        End If
                    End If

                    If _Recurring Then
                        If Not IsDBNull(oDetails.Rows(0)("DM_dtStartDate")) Then
                            objTrigger.StartDate = CType(oDetails.Rows(0)("DM_dtStartDate"), DateTime)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_dtEndDate")) Then
                            objTrigger.EndDate = CType(oDetails.Rows(0)("DM_dtEndDate"), DateTime)
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_nDurationPeriod")) Then
                            objTrigger.DurationPeriod = CType(oDetails.Rows(0)("DM_nDurationPeriod"), Int32)
                            strDueDesc = strDueDesc & " " & "-" & " " & "Recur every " & objTrigger.DurationPeriod
                        End If
                        If Not IsDBNull(oDetails.Rows(0)("DM_nDurationType")) Then
                            objTrigger.DurationType = CType(oDetails.Rows(0)("DM_nDurationType"), String)
                            strDueDesc = strDueDesc & " " & objTrigger.DurationType
                        End If

                    End If

                    ''Save it as TemplateResult object for the treenode
                    Associatenode.TemplateResult = objTrigger
                    'Associatenode.DMTemplate = 
                Else
                    ''if due detail are not available set to null
                    Associatenode.TemplateResult = Nothing
                End If
            Else

                ''if due detail are not available set to null
                Associatenode.TemplateResult = Nothing
            End If

            Associatenode.Text = _TriggerName & strDueDesc
            Associatenode.ImageIndex = 0
            Associatenode.SelectedImageIndex = 0
            ''Add the respective node to the parent node
            parentNode.Nodes.Add(Associatenode)
            trvHealthPlan.ExpandAll()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub AddHealthPlanNode(ByVal _CriteriaId As Int64, ByVal blnManual As Boolean)
        Dim oCriteria As Criteria
        Dim rootNode As myTreeNode = Nothing
        Dim parentNode As myTreeNode = Nothing
        Dim blnExist As Boolean = False

        Dim objList As myList

        oCriteria = oDM.GetCriteria(_CriteriaId, mPatientID)
        With oCriteria

            txtName.Text = .Name
            txtMessage.Text = .DisplayMessage

            rootNode = New myTreeNode
            rootNode.Text = .Name & " - " & .DisplayMessage
            rootNode.Key = _CriteriaId
            rootNode.ImageIndex = 12
            rootNode.SelectedImageIndex = 12
            rootNode.IsFinished = blnManual
            trvHealthPlan.Nodes.Add(rootNode)

            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = "Labs" Then
                    blnExist = True
                End If
            Next
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = "Labs"
                parentNode.Tag = DiseaseManagement.TemplateCategoryID.Labs.GetHashCode
                parentNode.ImageIndex = 6
                parentNode.SelectedImageIndex = 6
                rootNode.Nodes.Add(parentNode)
            End If

            For i As Integer = 1 To .LabOrders.Count
                Try

                    'sarika DM Denormalization
                    'objList = New myList
                    objList = CType(.LabOrders.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing
                    '----


                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next


            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = "Orders" Then
                    blnExist = True
                End If
            Next
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = "Orders"
                parentNode.Tag = DiseaseManagement.TemplateCategoryID.Radiology.GetHashCode
                parentNode.ImageIndex = 7
                parentNode.SelectedImageIndex = 7
                rootNode.Nodes.Add(parentNode)
            End If
            For i As Integer = 1 To .RadiologyOrders.Count
                Try

                    'sarika DM Denormalization
                    'Dim RadiologyOrderName As String = oDM.GetRadiologyOrder(.RadiologyOrders.Item(i))
                    'Dim mynode As New myTreeNode(RadiologyOrderName, .RadiologyOrders.Item(i))


                    'objList = New myList
                    objList = CType(.RadiologyOrders.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing
                    '----



                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next


            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = "Referrals" Then
                    blnExist = True
                End If
            Next
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = "Referrals"
                parentNode.Tag = DiseaseManagement.TemplateCategoryID.Referrals.GetHashCode
                parentNode.ImageIndex = 10
                parentNode.SelectedImageIndex = 10
                rootNode.Nodes.Add(parentNode)
            End If

            For i As Integer = 1 To .Referrals.Count
                Try

                    'sarika DM Denormalization
                    'Dim ReferralsName As String = oDM.GetRefferalName(.Referrals.Item(i))
                    'Dim mynode As New myTreeNode(ReferralsName, .Referrals.Item(i))


                    'objList = New myList
                    objList = CType(.Referrals.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing

                    '----


                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = "Drugs" Then
                    blnExist = True
                End If
            Next
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = "Drugs"
                parentNode.Tag = DiseaseManagement.TemplateCategoryID.Rx.GetHashCode
                parentNode.ImageIndex = 9
                parentNode.SelectedImageIndex = 9
                rootNode.Nodes.Add(parentNode)
            End If

            For i As Integer = 1 To .RxDrugs.Count
                Try

                    'sarika DM Denormalization
                    '    Dim DrugName As String = oDM.GetDrugName(.RxDrugs.Item(i))
                    'Dim mynode As New myTreeNode(DrugName, .RxDrugs.Item(i))


                    'objList = New myList
                    objList = CType(.RxDrugs.Item(i), myList)
                    'Dim mynode As New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage)
                    'objList = Nothing

                    Dim mynode As New myTreeNode(objList.Value, objList.ID, objList.DrugName, objList.Dosage, objList.DrugForm, objList.Route, objList.Frequency, objList.NDCCode, objList.IsNarcotic, objList.Duration, objList.mpid, objList.DrugQtyQualifier)
                    objList = Nothing


                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next

            For Each TargetNode As myTreeNode In rootNode.Nodes
                If TargetNode.Text = "Guidelines" Then
                    blnExist = True
                End If
            Next
            If Not blnExist Then
                parentNode = New myTreeNode
                parentNode.Text = "Guidelines"
                parentNode.Tag = DiseaseManagement.TemplateCategoryID.Guidelines.GetHashCode
                parentNode.ImageIndex = 8
                parentNode.SelectedImageIndex = 8
                rootNode.Nodes.Add(parentNode)
            End If

            For i As Integer = 1 To .Guidelines.Count
                Try
                    'sarika DM Denormalization
                    '    Dim GuildLine As String = oDM.GetGuidLine(.Guidelines.Item(i))
                    'Dim mynode As New myTreeNode(GuildLine, .Guidelines.Item(i))


                    'objList = New myList
                    objList = CType(.Guidelines.Item(i), myList)
                    Dim mynode As New myTreeNode(objList.Value, objList.ID)
                    objList = Nothing

                    '----

                    'check if selected node is rootnode
                    If Not IsNothing(mynode) Then
                        AddAssociates(mynode, parentNode)
                        mynode.Dispose()
                        mynode = Nothing
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End With
        oCriteria.Dispose()
    End Sub

    Private Sub SavePatientCriteria()
       
        Dim ResultCriteriaID As Int64
        Dim IsPatientSpecific As Boolean = True
        Try
            If txtName.Text.Trim = "" Then
                MessageBox.Show("Enter Criteria Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtName.Focus()
                Exit Sub
            End If

            If txtMessage.Text.Trim = "" Then
                MessageBox.Show("Enter Message for criteria", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtMessage.Focus()
                Exit Sub
            End If
            Dim arrList As New ArrayList
            Dim oList As myList
            For Each CategoryNode As myTreeNode In trvHealthPlan.Nodes(0).Nodes
                For Each ItemNode As myTreeNode In CategoryNode.Nodes

                    oList = New myList
                    oList.ID = ItemNode.Key
                    oList.DMTemplateName = ItemNode.Text
                    oList.Index = CategoryNode.Tag
                    '    oList.Value = ItemNode.Text
                    If CategoryNode.Text = "Guidelines" Then
                        oList.DMTemplate = ItemNode.DMTemplate
                    End If
                    If CategoryNode.Text = "Drugs" Then
                        oList.DMTemplateName = ItemNode.DrugName
                        oList.Dosage = ItemNode.Dosage
                        '   oList.Value = ItemNode.Text
                        oList.DrugName = ItemNode.DrugName


                        'sarika DM Denormalization for Rx
                        oList.DrugForm = ItemNode.DrugForm
                        oList.Route = ItemNode.Route
                        oList.Duration = ItemNode.Duration
                        oList.Frequency = ItemNode.Frequency
                        oList.NDCCode = ItemNode.NDCCode
                        oList.mpid = ItemNode.mpid
                        oList.IsNarcotic = ItemNode.IsNarcotics
                        oList.DrugQtyQualifier = ItemNode.DrugQtyQualifier
                        '---




                    End If
                    arrList.Add(oList)
                    oList = Nothing
                Next
            Next

            If arrList.Count <= 0 Then
                MessageBox.Show("No Order Selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            '' SAVING PATIENT CRITERIA ''
            '' If Criteria To Modify is NOT Patient Specific(Common Criteria) then Create copy of Criteria and save new criteria against patient.
            If mCriteriaID > 0 Then
                If oDM.IsPatientSpecificCriteria(mCriteriaID) = True Then
                    IsPatientSpecific = True
                Else
                    IsPatientSpecific = False
                End If
            Else
                IsPatientSpecific = True
            End If

            ResultCriteriaID = oDM.AddPatientCriteria(arrList, mCriteriaID, mPatientID, txtName.Text.Trim, txtMessage.Text.Trim, IsPatientSpecific)
            '' ''

            If ResultCriteriaID > 0 Then
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsDM_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDM.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SavePatientCriteria()
            Case "Close"
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    Private Sub txtSearchOrder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchOrder.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                trvTriggers.Select()
            Else
                trvTriggers.SelectedNode = trvTriggers.Nodes.Item(0)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtSearchOrder_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchOrder.TextChanged
        Try

            Try
                ' search the data from node in Drug tree view
                ' Search(txtDrugSearch, trvDrgs)

                '''''''''Code is modified by Anil on 20071106
                '' Fill Drugs
                ' Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
                If txtSearchOrder.Text.Trim.Length <= 1 Then
                    ''''To get the drugs with first character in search textbox
                    'oDM.Drugs(txtSearchOrder.Text.Trim.ToLower)
                    '' Sandip  20090526 FillRx(txtSearchOrder.Text.Trim.ToLower)
                Else
                    ''''''''''code to select the drug with name greater than one character string
                    Dim mychildnode As TreeNode
                    'child node collection
                    For Each mychildnode In trvTriggers.Nodes.Item(0).Nodes
                        Dim str As String
                        str = UCase(mychildnode.Text)
                        If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
                            trvTriggers.SelectedNode = trvTriggers.Nodes.Item(0).Nodes(trvTriggers.Nodes.Item(0).Nodes.Count - 1)
                            trvTriggers.SelectedNode = mychildnode
                            txtSearchOrder.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                '''''''''''''''''''''''''''''''''
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Try
                If Trim(txtSearchOrder.Text) <> "" Then
                    If trvTriggers.Nodes(0).GetNodeCount(False) > 0 Then
                        Dim mychildnode As TreeNode
                        'child node collection

                        For Each mychildnode In trvTriggers.Nodes(0).Nodes
                            Dim str As String
                            str = UCase(Trim(mychildnode.Text))
                            If Mid(str, 1, Len(Trim(txtSearchOrder.Text))) = UCase(Trim(txtSearchOrder.Text)) Then
                                If Not IsNothing(trvTriggers.SelectedNode) Then
                                    If Not IsNothing(trvTriggers.SelectedNode.LastNode) Then
                                        trvTriggers.SelectedNode = trvTriggers.SelectedNode.LastNode
                                    End If
                                End If

                                trvTriggers.SelectedNode = mychildnode
                                'trvCategory.HideSelection = False
                                txtSearchOrder.Focus()
                                Exit Sub
                            Else
                                'trvCategory.HideSelection = True
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "DM Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLab.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(1)
    End Sub
    Private Sub btnRadiologyTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiologyTest.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(4)
    End Sub
    Private Sub btnReferrals_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReferrals.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(2)
    End Sub
    Private Sub btnRx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRx.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(3)
    End Sub
    Private Sub btnGuideline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuideline.Click
        txtSearchOrder.Text = ""
        PopulateAssocaitedInfo(5)
    End Sub
    Private Sub trvTriggers_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvTriggers.NodeMouseDoubleClick
        Try
            trvTriggers.SelectedNode = e.Node

            ''Get the selected node a nd make validation appropriately
            Dim myNode As myTreeNode
            myNode = CType(trvTriggers.SelectedNode, myTreeNode)
            If Not myNode Is Nothing Then
                If Not myNode Is trvTriggers.Nodes(0) Then
                    ''Add nodes to the reuired Orders node
                    AddTriggerAssociates(myNode, myNode.Parent.Text)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region " Button Design Events "
    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuideline.MouseHover, btnLab.MouseHover, btnRadiologyTest.MouseHover, btnReferrals.MouseHover, btnRx.MouseHover
        Try
            CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
            CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuideline.MouseLeave, btnLab.MouseLeave, btnRadiologyTest.MouseLeave, btnReferrals.MouseLeave, btnRx.MouseLeave
        Try
            If CType(sender, Button).Tag = "Selected" Then
                CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            Else
                CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region


    Private Sub trvHealthPlan_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvHealthPlan.NodeMouseClick

        Try
            Dim selectednode As TreeNode
            trvHealthPlan.SelectedNode = e.Node
            selectednode = e.Node
            ''If the mouse button clicked is of right one
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If Not IsNothing(trvHealthPlan.SelectedNode) Then
                    ''Validate the selected node for field node but should not be Parent or table node
                    If trvHealthPlan.Nodes.Item(0) Is trvHealthPlan.SelectedNode Then
                        'Try
                        '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
                        '        trvHealthPlan.ContextMenu.Dispose()
                        '        trvHealthPlan.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvHealthPlan.ContextMenu = Nothing
                    ElseIf trvHealthPlan.SelectedNode.Parent Is trvHealthPlan.Nodes.Item(0) Then
                        'Try
                        '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
                        '        trvHealthPlan.ContextMenu.Dispose()
                        '        trvHealthPlan.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvHealthPlan.ContextMenu = Nothing
                    Else
                        ''Clear the menu items and add the context menu
                        CntConditions.MenuItems.Clear()
                        'Try
                        '    If (IsNothing(trvHealthPlan.ContextMenu) = False) Then
                        '        trvHealthPlan.ContextMenu.Dispose()
                        '        trvHealthPlan.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvHealthPlan.ContextMenu = CntConditions

                        Dim oMenuItem As MenuItem
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Delete Item"
                            .Tag = "DeleteItem"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        CntConditions.MenuItems.Add(oMenuItem)
                        AddHandler oMenuItem.Click, AddressOf SetMenus


                        If trvHealthPlan.SelectedNode.Parent.Text = "Guidelines" Then
                            oMenuItem = New MenuItem
                            With oMenuItem
                                .Text = "Edit Template"
                                .Tag = "EditTemplate"
                                .Shortcut = Shortcut.CtrlShiftT
                                .ShowShortcut = False
                            End With
                            CntConditions.MenuItems.Add(oMenuItem)
                            ''set the handler for the menu item
                            AddHandler oMenuItem.Click, AddressOf SetMenus
                            oMenuItem = Nothing
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ''' <summary>
    ''' Implmenting the context menu for deleting the selected item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        'Dim TemplateID As Int64
        Try
            Dim mychildnode As myTreeNode
            If IsNothing(trvHealthPlan.SelectedNode) = True Then
                Exit Sub
            End If

            mychildnode = CType(trvHealthPlan.SelectedNode, myTreeNode)

            If oCurrentMenu.Tag = "DeleteItem" Then
                If Not IsNothing(mychildnode) Then
                    ''If child nodes are more than one delete only the selected item
                    If mychildnode.Parent.Nodes.Count > 0 Then
                        mychildnode.Remove()
                    End If

                End If
            ElseIf oCurrentMenu.Tag = "EditTemplate" Then
                'sarika DM Denormalization 20090402
                'TemplateID = Convert.ToInt64(mychildnode.Tag)
                'UpdateTemplate(TemplateID)

                UpdateTemplate(CType(mychildnode, myTreeNode))
                '---
            ElseIf oCurrentMenu.Tag = "EditTemplateTrigger" Then
                'sarika DM Denormalization 20090402
                'TemplateID = Convert.ToInt64(mychildnode.Tag)
                'UpdateTemplate(TemplateID)

                UpdateTemplate(CType(mychildnode, myTreeNode))
                '----
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "DM Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oCurrentMenu = Nothing
        End Try

    End Sub

    Private Sub UpdateTemplate(ByVal ID As Int64)

        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try
            ''''''''''''''''''
            objfrmTemplateGallery = New frmTemplateGallery(ID)
            With objfrmTemplateGallery
                .Text = "Modify Template"
                '.mycaller = Me
                .MdiParent = Me.ParentForm
                'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()

            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try
    End Sub


    'sarika DM Denormlization
    Private Sub UpdateTemplate(ByVal mySelectedNode As myTreeNode)

        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try


            '   blnModify = True



            ''''''''''''''''''
            objfrmTemplateGallery = New frmTemplateGallery(True)
            '  Me.DMSelectedNode = mySelectedNode
            With objfrmTemplateGallery
                .DMSelectedNode = mySelectedNode
                .Text = "Modify Template"
                '.mycaller = Me
                .MdiParent = Me.ParentForm
                '.Parent = Me
                'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                '.WindowState = FormWindowState.Maximized
                '.BringToFront()
                '.ShowDialog(Me)

                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()


                mySelectedNode = .DMSelectedNode
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try

    End Sub
    '---

    Private Sub GloUC_trvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvAssociates.NodeMouseDoubleClick
        Try

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

           
            If Not oNode Is Nothing Then
                ''Add nodes to the reuired Orders node
                Dim oNodeToAdd As New myTreeNode
                oNodeToAdd.Key = oNode.ID
                oNodeToAdd.Text = oNode.Text
                oNodeToAdd.DrugName = oNode.Code
                oNodeToAdd.Dosage = oNode.Description
                oNodeToAdd.DrugForm = oNode.DrugForm
                oNodeToAdd.Route = oNode.Route
                oNodeToAdd.Frequency = oNode.Frequency
                oNodeToAdd.NDCCode = oNode.NDCCode
                oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                oNodeToAdd.Duration = oNode.Duration
                oNodeToAdd.mpid = oNode.mpid
                oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier
                oNodeToAdd.DMTemplate = oNode.TemplateResult
                AddTriggerAssociates(oNodeToAdd, strParentToAssociate)
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvAssociates.KeyPress
        Try

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode)

           
            If Not oNode Is Nothing Then
                ''Add nodes to the reuired Orders node
                Dim oNodeToAdd As New myTreeNode
                oNodeToAdd.Key = oNode.ID
                oNodeToAdd.Text = oNode.Text
                oNodeToAdd.DrugName = oNode.Code
                oNodeToAdd.Dosage = oNode.Description
                oNodeToAdd.DrugForm = oNode.DrugForm
                oNodeToAdd.Route = oNode.Route
                oNodeToAdd.Frequency = oNode.Frequency
                oNodeToAdd.NDCCode = oNode.NDCCode
                oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                oNodeToAdd.Duration = oNode.Duration
                oNodeToAdd.mpid = oNode.mpid
                oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier
                AddTriggerAssociates(oNodeToAdd, strParentToAssociate)
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class