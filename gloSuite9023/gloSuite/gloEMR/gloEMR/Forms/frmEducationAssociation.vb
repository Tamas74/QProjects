Imports System.Data
Imports System.Collections

Public Class frmEducationAssociation

#Region "Class Attributes"
    Private strProviderReference As String = "Provider Reference"
    Private strPatientEducation As String = "Patient Education"

    Private sUCtrlMaterialType As String = Nothing

    'Dim frmSnomedBrowser As gloUserControlLibrary.frmSelectProblem

    Private Shared frmEducationAssociationForm As frmEducationAssociation

    Private strConceptID As String = String.Empty
    Private strConceptID_Old As String = String.Empty
    Private strDescriptionID As String = String.Empty
    Private strSnoMedId As String = String.Empty
    Private strDescription As String = String.Empty

    Private sCurrentSelectedNode As String = Nothing
    Private eCurrentSelectedButton As DockingTags
    Private eCurrentReferenceMaterial As EducationTag

    Private collectionLabs As Dictionary(Of String, DockingTags)
    Private collectionICD9 As Dictionary(Of String, DockingTags)
    Private collectionICD10 As Dictionary(Of String, DockingTags) ''added for ICd10 implementation
    Private collectionMedication As Dictionary(Of String, DockingTags)
    Private collectionSnomed As Dictionary(Of String, DockingTags)

    Private dataTableICD9 As DataTable
    Private dataTableMedication As DataTable
    Private dataTableLabResults As DataTable
    Private dataTableSnomed As DataTable

    Private dataTableAssociatedCodes As New DataTable
    Private dataTableDelete As DataTable




    Private Enum ButtonSelectionTags
        NotSelected = 0
        Selected = 1
    End Enum

    Private Enum EducationTag
        Patient = 1
        Provider = 2
    End Enum

    Private Enum DockingTags
        ProviderReference
        PatientEducation

        LeftPanel
        RightPanel

        SnoMedButton
        LabsButton
        ICD9Button
        MedicationButton
        ICD10Button ''added for ICD10 implementation
    End Enum
#End Region

#Region "Initialization"

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.




    End Sub

    Public Shared Function GetInstance() As frmEducationAssociation
        If frmEducationAssociationForm Is Nothing Then
            frmEducationAssociationForm = New frmEducationAssociation
        End If

        Return frmEducationAssociationForm
    End Function

#End Region

#Region "Form Events"

    Private Sub frmEducationAssociation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try            
            chkAdvancedProviderReference.Enabled = False
            treeViewCodesAssociation.CheckBoxes = False
            treeViewCodesAssociation.ImageList = imgTreeView
            CodesTreeView.ImageList = imgTreeView
            btn_AddSnoMed.Enabled = False

            For nCounter As Integer = 0 To 125 Step 5
                cmbAgeMin.Items.Add(nCounter.ToString)
                cmbAgeMax.Items.Add(nCounter.ToString)
            Next


            cmbAgeMin.SelectedItem = cmbAgeMax.Items(0)
            cmbAgeMin.Enabled = False

            cmbAgeMax.SelectedItem = cmbAgeMax.Items(0)
            cmbAgeMax.Enabled = False

            cmbGender.Enabled = False

            chkEnableDemographics.Checked = False


            btnICD9.Tag = "UnSelected"
            btnICD10.Tag = "UnSelected" ''added for ICD10 implementation
            btnSnoMed.Tag = "UnSelected"
            btnLabs.Tag = "UnSelected"
            btnMedication.Tag = "UnSelected"
            ''according to admin setting showing ICd10 on top  
            If gblnIcd10MasterTransition = True Then 'gblnIcd10Transition
                btnICD10.PerformClick()
            Else
                btnICD9.PerformClick()
            End If

            btnPatientEducation.PerformClick()


            If dataTableDelete Is Nothing Then
                dataTableDelete = New DataTable("Delete")

                With dataTableDelete.Columns
                    .Add(New DataColumn("sCode", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sCodeType", System.Type.GetType("System.String")))
                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try



    End Sub

    Private Sub frmEducationAssociation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If frmEducationAssociationForm IsNot Nothing Then
                frmEducationAssociationForm = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub frmEducationAssociation_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If collectionLabs IsNot Nothing Then
                collectionLabs.Clear()
                collectionLabs = Nothing
            End If

            If collectionICD9 IsNot Nothing Then
                collectionICD9.Clear()
                collectionICD9 = Nothing
            End If
            If collectionICD10 IsNot Nothing Then ''added for ICD10 implementation
                collectionICD10.Clear()
                collectionICD10 = Nothing
            End If
            If collectionMedication IsNot Nothing Then
                collectionMedication.Clear()
                collectionMedication = Nothing
            End If

            If collectionSnomed IsNot Nothing Then
                collectionSnomed.Clear()
                collectionSnomed = Nothing
            End If

            If dataTableICD9 IsNot Nothing Then
                dataTableICD9.Clear()
                dataTableICD9.Dispose()
                dataTableICD9 = Nothing
            End If

            If dataTableMedication IsNot Nothing Then
                dataTableMedication.Clear()
                dataTableMedication.Dispose()
                dataTableMedication = Nothing
            End If

            If dataTableLabResults IsNot Nothing Then
                dataTableLabResults.Clear()
                dataTableLabResults.Dispose()
                dataTableLabResults = Nothing
            End If

            If dataTableSnomed IsNot Nothing Then
                dataTableSnomed.Clear()
                dataTableSnomed.Dispose()
                dataTableSnomed = Nothing
            End If

            If dataTableDelete IsNot Nothing Then
                dataTableDelete.Clear()
                dataTableDelete.Dispose()
                dataTableDelete = Nothing
            End If

            If dataTableAssociatedCodes IsNot Nothing Then
                dataTableAssociatedCodes.Clear()
                dataTableAssociatedCodes.Dispose()
                dataTableAssociatedCodes = Nothing
            End If

            Me.eCurrentSelectedButton = Nothing
            Me.sCurrentSelectedNode = Nothing
            Me.sUCtrlMaterialType = Nothing

        End Try

    End Sub

#End Region

#Region "Button Click Events"

    Private Sub tblSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblSave.Click

        Dim bIsCollectionICD9Initialized As Boolean = False
        Dim bIsCollectionMedicationInitialized As Boolean = False
        Dim bIsCollectionLabsInitialized As Boolean = False
        Dim bIsCollectionSnomedInitialzed As Boolean = False
        Dim bIsCollectionICD10Initialized As Boolean = False ''added for ICD10 implementation
        Try

            BuildDatatables()

            For Each Node As myTreeNode In treeViewCodesAssociation.Nodes
                Dim sCode As String = (GetTargetNodeID(CType(Node.Tag, String)))
                If collectionICD9 IsNot Nothing Then
                    bIsCollectionICD9Initialized = True
                    If collectionICD9.Count > 0 Then
                        If collectionICD9.ContainsKey(sCode) Then
                            InsertIntoICDDataTable(Node, 9)
                        End If
                    End If
                End If

                If collectionICD10 IsNot Nothing Then  ''added for ICD10 implementation
                    bIsCollectionICD10Initialized = True
                    If collectionICD10.Count > 0 Then
                        If collectionICD10.ContainsKey(sCode) Then
                            InsertIntoICDDataTable(Node, 10)
                        End If
                    End If
                End If

                If collectionLabs IsNot Nothing Then
                    bIsCollectionLabsInitialized = True
                    If collectionLabs.Count > 0 Then
                        If collectionLabs.ContainsKey(sCode) Then
                            InsertIntoLabsDataTable(Node)
                        End If
                    End If
                End If

                If Me.collectionMedication IsNot Nothing Then
                    bIsCollectionMedicationInitialized = True
                    If collectionMedication.Count > 0 Then
                        If collectionMedication.ContainsKey(sCode) Then

                            InsertIntoMedicationDataTable(Node)
                        End If
                    End If
                End If

                If Me.collectionSnomed IsNot Nothing Then
                    bIsCollectionSnomedInitialzed = True
                    If collectionSnomed.Count > 0 Then
                        If collectionSnomed.ContainsKey(sCode) Then
                            InsertIntoSnomedDataTable(Node)
                        End If
                    End If
                End If
            Next

            If InsertIntoDatabase() Then
                Me.tblClose.PerformClick()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub tblClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblClose.Click

        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        
    End Sub

    Private Sub btn_AddSnoMed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddSnoMed.Click

        Try
            AddSnomedNode()
            txtsearchDrugs.Text = String.Empty
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnPatientEducation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.Click
        Try
            If btnPatientEducation.Tag = "UnSelected" Then
                eCurrentReferenceMaterial = EducationTag.Patient
                PopulateAssociates(DockingTags.PatientEducation)
            End If
            With chkAdvancedProviderReference
                .Checked = False
                .Enabled = False
            End With


            FillReferenceMaterialTreeView(clsEducationAssociationDatabaseLayer.Template.PatientEducationMaterial)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnProviderReference_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProviderReference.Click
        Try
            If btnProviderReference.Tag = "UnSelected" Then
                eCurrentReferenceMaterial = EducationTag.Provider
                PopulateAssociates(DockingTags.ProviderReference)
                chkAdvancedProviderReference.Enabled = True
            End If

            FillReferenceMaterialTreeView(clsEducationAssociationDatabaseLayer.Template.ProviderReferenceMaterial)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try


    End Sub

    Private Sub btnSnoMed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSnoMed.Click
        Try
            If btnSnoMed.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.SnoMedButton)
                btn_AddSnoMed.Enabled = True

                CodesTreeView.Nodes.Clear()
            End If
            eCurrentSelectedButton = DockingTags.SnoMedButton

            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            Me.CodesTreeView.ColonAsSeparator = False
            FillCodesTreeView(DockingTags.SnoMedButton, GetDataTablesForFillingTreeView(DockingTags.SnoMedButton))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.SNOMED)
            pnlLeftRadioBtnTop.Enabled = False
            CodesTreeView.txtsearch.Text = String.Empty
            'Changes 3-9-2013
            For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                ElementNode.IsSnomedCode = True
            Next            
            DemographicsLabResults(False)
            pnlCriteriaLab.Visible = False

            If eCurrentReferenceMaterial = EducationTag.Provider Then
                chkAdvancedProviderReference.Enabled = True
            Else
                chkAdvancedProviderReference.Enabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub AddSnomedNode()

        Dim frmSnomedBrowser As gloSnoMed.FrmSelectProblem = Nothing

        Try
            gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
            frmSnomedBrowser = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())
            Dim bNodeExists As Boolean = False
            With frmSnomedBrowser
                .blnIsProblem = True
                ' .StartPosition = FormStartPosition.CenterScreen
                '.ShowInTaskbar = False
            End With
            frmSnomedBrowser.ShowDialog(IIf(IsNothing(frmSnomedBrowser.Parent), Me, frmSnomedBrowser.Parent))

            If frmSnomedBrowser._DialogResult = True Then
                strConceptID = frmSnomedBrowser.strSelectedConceptID
                strSnoMedId = frmSnomedBrowser.strSelectedSnoMedID
                strDescriptionID = frmSnomedBrowser.strSelectedDescriptionID
                strDescription = frmSnomedBrowser.strSelectedDescription

                Dim Node As New gloUserControlLibrary.myTreeNode
                With Node
                    .Text = strDescription
                    .ConceptID = strConceptID
                    .SnomedID = strSnoMedId
                    .IsSnomedCode = True
                End With

                For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                    If ElementNode.IsSnomedCode And Node.IsSnomedCode Then
                        If ElementNode.Tag = Node.ConceptID Then
                            bNodeExists = True
                            Exit For
                        End If
                    End If
                Next

                If bNodeExists = False Then
                    AddNode(Node, DockingTags.SnoMedButton, "NULL")
                    Node.Tag = strConceptID
                    CodesTreeView.Nodes.Add(Node)
                Else
                    AddNode(Node, True)
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If frmSnomedBrowser IsNot Nothing Then
                frmSnomedBrowser.Dispose()
                frmSnomedBrowser = Nothing
            End If
        End Try


    End Sub

    Private Sub btnLabs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.Click
        Try            
            If btnLabs.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.LabsButton)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
            End If
            eCurrentSelectedButton = DockingTags.LabsButton

            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            Me.CodesTreeView.ColonAsSeparator = True
            FillCodesTreeView(DockingTags.LabsButton, GetDataTablesForFillingTreeView(DockingTags.LabsButton))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.LOINC)
            pnlLeftRadioBtnTop.Enabled = True
            rbtAll.Checked = True
            CodesTreeView.txtsearch.Text = String.Empty

            If eCurrentReferenceMaterial = EducationTag.Provider Then
                chkAdvancedProviderReference.Enabled = True
            Else
                chkAdvancedProviderReference.Enabled = False
            End If

            pnlCriteriaLab.Visible = True
            If chkEnableDemographics.Checked Then
                DemographicsLabResults(True)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnMedication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMedication.Click
        Try
            If btnMedication.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.MedicationButton)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
            End If
            eCurrentSelectedButton = DockingTags.MedicationButton

            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            With CodesTreeView
                .EducationMappingSearchType = 1 'For all codes         
                .ColonAsSeparator = False
            End With

            FillCodesTreeView(DockingTags.MedicationButton, GetDataTablesForFillingTreeView(DockingTags.MedicationButton))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.NDC)
            pnlLeftRadioBtnTop.Enabled = True
            rbtAll.Checked = True
            CodesTreeView.txtsearch.Text = String.Empty            
            DemographicsLabResults(False)

            pnlCriteriaLab.Visible = False

            If eCurrentReferenceMaterial = EducationTag.Provider Then
                chkAdvancedProviderReference.Enabled = True
            Else
                chkAdvancedProviderReference.Enabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.Click

        Try
            If btnICD9.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.ICD9Button)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
            End If
            eCurrentSelectedButton = DockingTags.ICD9Button

            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            CodesTreeView.ColonAsSeparator = False
            CodesTreeView.txtsearch.Text = String.Empty

            FillCodesTreeView(DockingTags.ICD9Button, GetDataTablesForFillingTreeView(DockingTags.ICD9Button))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD9)
            pnlLeftRadioBtnTop.Enabled = True
            rbtAll.Checked = True

            DemographicsLabResults(False)
            pnlCriteriaLab.Visible = False

            If eCurrentReferenceMaterial = EducationTag.Provider Then
                chkAdvancedProviderReference.Enabled = True
            Else
                chkAdvancedProviderReference.Enabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub


#End Region

#Region "Docking Functions"

    Private Sub PopulateAssociates(ByVal ButtonTag As DockingTags, Optional ByVal strsearch As String = "")

        Try
            Select Case ButtonTag
                Case DockingTags.PatientEducation
                    DockDownAllButtons(DockingTags.RightPanel)
                    DockUpPanel(ButtonTag)

                Case DockingTags.ProviderReference
                    DockDownAllButtons(DockingTags.RightPanel)
                    DockUpPanel(DockingTags.ProviderReference)

                Case DockingTags.SnoMedButton
                    DockDownAllButtons(DockingTags.LeftPanel)
                    DockUpPanel(DockingTags.SnoMedButton)

                Case DockingTags.LabsButton
                    DockDownAllButtons(DockingTags.LeftPanel)
                    DockUpPanel(DockingTags.LabsButton)

                Case DockingTags.ICD9Button
                    DockDownAllButtons(DockingTags.LeftPanel)
                    DockUpPanel(DockingTags.ICD9Button)

                Case DockingTags.ICD10Button  ''added for ICD10 implementation
                    DockDownAllButtons(DockingTags.LeftPanel)
                    DockUpPanel(DockingTags.ICD10Button)

                Case DockingTags.MedicationButton
                    DockDownAllButtons(DockingTags.LeftPanel)
                    DockUpPanel(DockingTags.MedicationButton)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub DockDownAllButtons(ByVal DockingPanel As DockingTags)
        Try
            Select Case DockingPanel
                Case DockingTags.RightPanel
                    pnlbtnProviderReference.Dock = DockStyle.Bottom
                    pnlbtnPatientEducation.Dock = DockStyle.Bottom

                    With btnPatientEducation
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "UnSelected"
                    End With

                    With btnProviderReference
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "UnSelected"
                    End With
                Case DockingTags.LeftPanel 'DockingPanel.LeftPanel
                    DockAllLeftPanelObjects()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub DockAllLeftPanelObjects()
        Try
            pnlICD9.Dock = DockStyle.Bottom
            pnlICD10.Dock = DockStyle.Bottom ''added for ICD10 implementation
            pnlLabs.Dock = DockStyle.Bottom
            pnlMedication.Dock = DockStyle.Bottom
            pnlSnomed.Dock = DockStyle.Bottom

            With btnICD9
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                .BackgroundImageLayout = ImageLayout.Stretch
                .Tag = "UnSelected"
            End With

            With btnICD10 ''added for ICD10 implementation
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                .BackgroundImageLayout = ImageLayout.Stretch
                .Tag = "UnSelected"
            End With

            With btnLabs
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                .BackgroundImageLayout = ImageLayout.Stretch
                .Tag = "UnSelected"
            End With

            With btnMedication
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                .BackgroundImageLayout = ImageLayout.Stretch
                .Tag = "UnSelected"
            End With

            With btnSnoMed
                .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                .BackgroundImageLayout = ImageLayout.Stretch
                .Tag = "UnSelected"
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub DockUpPanel(ByVal PanelName As DockingTags)

        Try
            Select Case PanelName
                Case DockingTags.MedicationButton
                    pnlMedication.Dock = DockStyle.Top
                    With btnMedication
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "Selected"
                        .BringToFront()
                    End With
                Case DockingTags.ICD9Button
                    pnlICD9.Dock = DockStyle.Top
                    With btnICD9
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "Selected"
                        .BringToFront()
                    End With

                Case DockingTags.ICD10Button ''added for ICD10 implementation
                    pnlICD10.Dock = DockStyle.Top
                    With btnICD10
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "Selected"
                        .BringToFront()
                    End With

                Case DockingTags.LabsButton
                    pnlLabs.Dock = DockStyle.Top
                    With btnLabs
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "Selected"
                        .BringToFront()
                    End With
                Case DockingTags.SnoMedButton
                    pnlSnomed.Dock = DockStyle.Top
                    With btnSnoMed
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "Selected"
                        .BringToFront()
                    End With
                Case DockingTags.PatientEducation
                    pnlbtnPatientEducation.Dock = DockStyle.Top
                    With btnPatientEducation
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                        .BackgroundImageLayout = ImageLayout.Stretch
                        .Tag = "Selected"
                        .BringToFront()
                    End With
                    pnlRight.Refresh()
                Case DockingTags.ProviderReference
                    pnlbtnProviderReference.Dock = DockStyle.Top
                    btnProviderReference.ForeColor = Color.FromArgb(31, 73, 125)
                    btnProviderReference.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnProviderReference.BackgroundImageLayout = ImageLayout.Stretch
                    btnProviderReference.Tag = "Selected"
                    btnProviderReference.BringToFront()
                    pnlRight.Refresh()

            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub


#End Region

#Region "Private Functions and Procedures"

    Private Sub DemographicsLabResults(ByVal EnableCriteria As Boolean)
        Try
            If Not EnableCriteria Then
                txtValueOne.Enabled = False
                txtValueTwo.Enabled = False
                cmbOperator.Enabled = False
            Else
                If cmbOperator.SelectedIndex > 0 Then
                    txtValueOne.Enabled = True
                    txtValueTwo.Enabled = True
                End If                
                cmbOperator.Enabled = True
                If (cmbOperator.Items.Count > 0) Then
                    cmbOperator.SelectedIndex = 0
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Function GetResourceType(ByVal Node As myTreeNode) As Integer
        Dim nResourceType As Integer = 0

        Try
            If Node.IsPatientEducationMaterial Then
                nResourceType = 1
            ElseIf Node.IsProviderReferenceMaterial Then
                nResourceType = 2
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

        Return nResourceType

    End Function

    Private Sub ChangeIconsAfterSearch() Handles CodesTreeView.SearchFired
        Try
            Dim NSearchType As Integer = 1
            If rbtAll.Checked = True Then
                NSearchType = 1
            End If
            If rbtAssociated.Checked = True Then
                NSearchType = 2
            End If
            If rbtUnassociated.Checked = True Then
                NSearchType = 3
            End If
            Select Case eCurrentSelectedButton
                Case DockingTags.ICD9Button
                    FillCodesTreeView(DockingTags.ICD9Button, GetDataTablesForFillingTreeView(DockingTags.ICD9Button, NSearchType, CodesTreeView.txtsearch.Text.Trim()))

                    ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD9)

                Case DockingTags.ICD10Button ''added for ICD10 implementation
                    FillCodesTreeView(DockingTags.ICD10Button, GetDataTablesForFillingTreeView(DockingTags.ICD10Button, NSearchType, CodesTreeView.txtsearch.Text.Trim()))

                    ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD10)

                Case DockingTags.LabsButton
                    ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.LOINC)
                Case DockingTags.MedicationButton
                    ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.NDC)
                Case DockingTags.SnoMedButton
                    ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.SNOMED)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub ChangeIconIfAssociationIsFound(ByVal CodeType As clsEducationAssociationDatabaseLayer.Codes)

        Dim oEducationDatabaseLayer As New clsEducationAssociationDatabaseLayer()
        Dim _drRowFilter As DataRow() = Nothing

        Try

            dataTableAssociatedCodes = oEducationDatabaseLayer.GetAssociatedCodes(CodeType)

            If CodeType <> clsEducationAssociationDatabaseLayer.Codes.SNOMED Then

                If dataTableAssociatedCodes IsNot Nothing Then

                    If dataTableAssociatedCodes.Rows.Count > 0 Then

                        Dim sCode As String = Nothing
                        Dim sCodeDes As String = Nothing
                        Dim nCodeImageIndex As Integer = 0
                        For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes

                            If CodeType = clsEducationAssociationDatabaseLayer.Codes.ICD9 Then
                                sCode = ElementNode.Code
                                sCodeDes = "sICD9Code"
                                nCodeImageIndex = 6
                            ElseIf CodeType = clsEducationAssociationDatabaseLayer.Codes.ICD10 Then ''added for ICD10 implementation
                                sCode = ElementNode.Code
                                sCodeDes = "sICD9Code"
                                nCodeImageIndex = 15
                            ElseIf CodeType = clsEducationAssociationDatabaseLayer.Codes.LOINC Then
                                sCode = ElementNode.Tag
                                sCodeDes = "LOINCCode"
                                nCodeImageIndex = 9
                            ElseIf CodeType = clsEducationAssociationDatabaseLayer.Codes.NDC Then
                                sCode = ElementNode.mpid
                                sCodeDes = "mpid"
                                nCodeImageIndex = 3
                            ElseIf CodeType = clsEducationAssociationDatabaseLayer.Codes.SNOMED Then
                                sCode = ElementNode.Tag
                                sCodeDes = "sConceptID"
                                nCodeImageIndex = 14
                            End If


                            _drRowFilter = dataTableAssociatedCodes.[Select](sCodeDes + "='" + sCode.Replace("'", "''") + "'")

                            If _drRowFilter.Length > 0 Then
                                ElementNode.ImageIndex = nCodeImageIndex
                                ElementNode.SelectedImageIndex = nCodeImageIndex
                            End If

                        Next

                    End If
                End If
            Else

                For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                    ElementNode.ImageIndex = 14
                    ElementNode.SelectedImageIndex = 14
                Next

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If oEducationDatabaseLayer IsNot Nothing Then
                oEducationDatabaseLayer.Dispose()
                oEducationDatabaseLayer = Nothing
            End If
            If _drRowFilter IsNot Nothing Then
                _drRowFilter = Nothing
            End If

        End Try

    End Sub

    Private Sub CheckForDelete(ByVal NodeIndex As Integer)

        Try
            If treeViewCodesAssociation.Nodes(NodeIndex).Nodes(0).Nodes.Count = 0 Then

                If treeViewCodesAssociation.Nodes(NodeIndex).Nodes(1).Nodes.Count = 0 Then
                    Dim nodeCurrent As myTreeNode = CType(treeViewCodesAssociation.Nodes(NodeIndex), myTreeNode)
                    Dim sCode As String = String.Empty
                    Dim sCodeType As String = String.Empty

                    If nodeCurrent.IsICD9 Then
                        sCode = GetTargetNodeID(nodeCurrent.Tag.ToString)
                        sCodeType = clsEducationAssociationDatabaseLayer.Codes.ICD9.GetHashCode
                    ElseIf nodeCurrent.IsSnomedCode Then
                        sCode = GetTargetNodeID(nodeCurrent.Tag.ToString)
                        sCodeType = clsEducationAssociationDatabaseLayer.Codes.SNOMED.GetHashCode
                    ElseIf nodeCurrent.IsLabs Then
                        sCode = GetTargetNodeID(nodeCurrent.Tag.ToString)
                        sCodeType = clsEducationAssociationDatabaseLayer.Codes.LOINC.GetHashCode
                    ElseIf nodeCurrent.IsMedication Then
                        sCodeType = clsEducationAssociationDatabaseLayer.Codes.NDC.GetHashCode
                        sCode = nodeCurrent.Tag
                    End If

                    Dim deleteDataRow As DataRow = dataTableDelete.NewRow()

                    deleteDataRow("sCode") = sCode
                    deleteDataRow("sCodeType") = sCodeType

                    dataTableDelete.Rows.Add(deleteDataRow)
                    deleteDataRow = Nothing

                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Function GetTargetNodeType(ByVal Node As gloEMR.myTreeNode) As String
        Return Node.Tag.ToString.Substring(Node.Tag.ToString.IndexOf("|") + 1)
    End Function

    Private Function GetTargetNodeID(ByVal ElementNode As gloEMR.myTreeNode) As String
        Return ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))
    End Function

    Private Function GetTargetNodeID(ByVal Value As String) As String
        Return Value.Remove(Value.IndexOf("|"))
    End Function

    Private Function GetDemographicsFromNode(ByVal Node As myTreeNode, Optional ByVal IsLabs As Boolean = False) As String

        Dim sDemographics As String = ""
        
        Try
            If Node.MaximumAge > 0 Then
                sDemographics = " [Age between " + Node.MinimumAge.ToString + " and " + Node.MaximumAge.ToString + " ]"
            End If

            If Node.Gender <> "" Then
                sDemographics = sDemographics + "[Gender " + Node.Gender + "]"
            End If

            If Node.IsProviderAdvanceMaterial = 1 Then
                sDemographics = sDemographics + " [Advanced Provider Reference]"
            End If

            If IsLabs Then
                If Node.LabResultOperator <> -1 Then
                    Dim sOperator As String = Nothing

                    Select Case Node.LabResultOperator
                        Case 1 'Between
                            sOperator = " [Lab Results Between "
                        Case 2
                            sOperator = " [Lab Results Equals "
                        Case 3
                            sOperator = " [Lab Results Greater Than "
                        Case 4
                            sOperator = " [Lab Results Less Than "
                    End Select

                    Select Case Node.LabResultOperator                        
                        Case 1
                            If Node.LabResultValueTwo <> 0.0 Then                                
                                sDemographics = sDemographics + sOperator + Node.LabResultValueOne.ToString + " & " + Node.LabResultValueTwo.ToString + "]"
                            End If

                        Case 2, 3, 4
                            If Node.LabResultValueTwo <> 0.0 Then
                                sDemographics = sDemographics + sOperator + Node.LabResultValueTwo.ToString + "]"
                            End If
                    End Select
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

        Return sDemographics

    End Function

#End Region

#Region "Tree View Clicks and Double Clicks"

    Private Sub UCtrlReferenceMaterial_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles UCtrlReferenceMaterial.NodeMouseDoubleClick

        Dim TargetNode As myTreeNode = CType(treeViewCodesAssociation.SelectedNode, myTreeNode)
        Dim ReferenceMaterialNode As gloUserControlLibrary.myTreeNode = CType(UCtrlReferenceMaterial.SelectedNode, gloUserControlLibrary.myTreeNode)
        Dim sTargetNodeType As String = Nothing
        Dim sReferenceMaterialNodeType As String = sUCtrlMaterialType 'UCtrlReferenceMaterial.Tag.ToString


        Try
            'Could be ProviderReference or PatientEducation

            If pnlCriteriaLab.Visible Then
                If cmbOperator.SelectedIndex = 1 Then

                    Dim dValueOne As Decimal = 0
                    Dim dValueTwo As Decimal = 0

                    If txtValueOne.Text = String.Empty Then
                        MsgBox("Value One cannot be empty", MsgBoxStyle.Information)
                        Exit Sub
                    Else
                        dValueOne = Convert.ToDecimal(txtValueOne.Text)
                    End If

                    If txtValueTwo.Text = String.Empty Then
                        MsgBox("Value Two cannot be empty", MsgBoxStyle.Information)
                        Exit Sub
                    Else
                        dValueTwo = Convert.ToDecimal(txtValueTwo.Text)
                    End If

                    If dValueOne > dValueTwo Then
                        MsgBox("Value One cannot be more then Value Two", MsgBoxStyle.Information)
                        Exit Sub
                    ElseIf dValueOne = dValueTwo Then
                        MsgBox("Value One and Value Two cannot be the same", MsgBoxStyle.Information)
                        Exit Sub
                    End If

                    'If txtValueOne.Text = txtValueTwo.Text Then
                    '    If txtValueOne.Text = String.Empty Then
                    '        MsgBox("Please enter proper values in Value One and Value Two", MsgBoxStyle.Information)
                    '    Else
                    '        MsgBox("Value One and Value Two cannot be the same", MsgBoxStyle.Information)
                    '    End If
                    '    Exit Sub
                    'End If

                End If
            End If

            If TargetNode Is Nothing AndAlso treeViewCodesAssociation.Nodes.Count = 1 Then
                TargetNode = CType(treeViewCodesAssociation.Nodes(0), myTreeNode)
            End If

            If TargetNode IsNot Nothing Then
                sTargetNodeType = GetTargetNodeType(TargetNode)
                'Could be ParentNode, PatientEducation or ProviderReference

                If sReferenceMaterialNodeType = "ProviderReference" Then

                    Dim NodeProviderReferenceMaterial As New myTreeNode()
                    With NodeProviderReferenceMaterial
                        .Tag = ReferenceMaterialNode.ID.ToString + "|ProviderReferenceMaterial"
                        
                        'Will need to change this for enable/disable Demographics
                        If chkAdvancedProviderReference.Checked Then
                            .IsProviderAdvanceMaterial = 1
                        End If
                        If chkEnableDemographics.Checked Then
                            If IsParentNodeLab(TargetNode) Then
                                'If eCurrentSelectedButton = DockingTags.LabsButton Then
                                If IsDemographicsCorrect(True) = False Then
                                    MsgBox("Please enter the criteria.", MsgBoxStyle.Information)
                                    Exit Sub
                                End If
                            Else
                                If IsDemographicsCorrect(False) = False Then
                                    MsgBox("Please enter the criteria.", MsgBoxStyle.Information)
                                    Exit Sub
                                End If
                            End If

                            If IsParentNodeLab(TargetNode) Then
                                AddLabResultsValue(NodeProviderReferenceMaterial)
                            End If


                            .MinimumAge = cmbAgeMin.Text
                            .MaximumAge = cmbAgeMax.Text
                            .Gender = cmbGender.Text
                        Else
                            .MinimumAge = 0
                            .MaximumAge = 0
                            .Gender = String.Empty
                        End If

                            .IsProviderReferenceMaterial = True
                        If eCurrentSelectedButton = DockingTags.LabsButton Or IsParentNodeLab(TargetNode) Then
                            .Text = ReferenceMaterialNode.Text + GetDemographicsFromNode(NodeProviderReferenceMaterial, True) ' GetDemographicsFromComboBoxes()
                        Else
                            .Text = ReferenceMaterialNode.Text + GetDemographicsFromNode(NodeProviderReferenceMaterial) ' GetDemographicsFromComboBoxes()
                        End If

                        .TemplateName = ReferenceMaterialNode.Text
                        'Try
                        '    If (IsNothing(.ContextMenuStrip) = False) Then
                        '        .ContextMenuStrip.Dispose()
                        '        .ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenuStrip = contextMenus
                    End With

                    Select Case sTargetNodeType

                        Case "ParentNode"
                            If TargetNode.Nodes(1).Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Nodes(1).Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Nodes(1).Nodes.Add(NodeProviderReferenceMaterial)
                                End If
                            Else
                                TargetNode.Nodes(1).Nodes.Add(NodeProviderReferenceMaterial)
                            End If

                        Case "PatientEducation"
                            If TargetNode.Parent.Nodes(1).Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Parent.Nodes(1).Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Parent.Nodes(1).Nodes.Add(NodeProviderReferenceMaterial)
                                End If
                            Else
                                TargetNode.Parent.Nodes(1).Nodes.Add(NodeProviderReferenceMaterial)
                            End If


                        Case "ProviderReference"
                            If TargetNode.Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Nodes.Add(NodeProviderReferenceMaterial)
                                End If
                            Else
                                TargetNode.Nodes.Add(NodeProviderReferenceMaterial)
                            End If


                        Case "PatientEducationMaterial"
                            If TargetNode.Parent.Parent.Nodes(1).Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Parent.Parent.Nodes(1).Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Parent.Parent.Nodes(1).Nodes.Add(NodeProviderReferenceMaterial)
                                End If
                            Else
                                TargetNode.Parent.Parent.Nodes(1).Nodes.Add(NodeProviderReferenceMaterial)
                            End If


                        Case "ProviderReferenceMaterial"
                            If TargetNode.Parent.Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Parent.Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Parent.Nodes.Add(NodeProviderReferenceMaterial)
                                End If
                            Else
                                TargetNode.Parent.Nodes.Add(NodeProviderReferenceMaterial)
                            End If


                    End Select

                    If NodeProviderReferenceMaterial IsNot Nothing Then
                        NodeProviderReferenceMaterial.Dispose()
                        NodeProviderReferenceMaterial = Nothing
                    End If


                ElseIf sReferenceMaterialNodeType = "PatientEducation" Then

                    Dim NodePatientEducationMaterial As New myTreeNode()
                    With NodePatientEducationMaterial
                        .Tag = ReferenceMaterialNode.ID.ToString + "|PatientEducationMaterial"

                If chkEnableDemographics.Checked Then
                    If eCurrentSelectedButton = DockingTags.LabsButton Then
                        If IsDemographicsCorrect(True) = False Then
                            MsgBox("Please enter the criteria.", MsgBoxStyle.Information)
                            Exit Sub
                        End If
                    Else
                        If IsDemographicsCorrect(False) = False Then
                            MsgBox("Please enter the criteria.", MsgBoxStyle.Information)
                            Exit Sub
                        End If
                    End If

                    If eCurrentSelectedButton = DockingTags.LabsButton Then
                        AddLabResultsValue(NodePatientEducationMaterial)

                    End If

                    .MinimumAge = cmbAgeMin.Text
                    .MaximumAge = cmbAgeMax.Text
                    .Gender = cmbGender.Text
                Else
                    .MinimumAge = 0
                    .MaximumAge = 0
                    .Gender = String.Empty
                End If
                        If eCurrentSelectedButton = DockingTags.LabsButton Or IsParentNodeLab(TargetNode) Then
                            .Text = ReferenceMaterialNode.Text + GetDemographicsFromNode(NodePatientEducationMaterial, True) ' GetDemographicsFromComboBoxes()
                        Else
                            .Text = ReferenceMaterialNode.Text + GetDemographicsFromNode(NodePatientEducationMaterial) ' GetDemographicsFromComboBoxes()
                        End If
                '.Text = ReferenceMaterialNode.Text + GetDemographicsFromNode(NodePatientEducationMaterial)
                        .TemplateName = ReferenceMaterialNode.Text
                        'Try
                        '    If (IsNothing(.ContextMenuStrip) = False) Then
                        '        .ContextMenuStrip.Dispose()
                        '        .ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenuStrip = contextMenus
                .IsPatientEducationMaterial = True
                    End With

                    Select Case sTargetNodeType

                        Case "ParentNode"
                            If TargetNode.Nodes(0).Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Nodes(0).Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Nodes(0).Nodes.Add(NodePatientEducationMaterial)
                                End If
                            Else
                                TargetNode.Nodes(0).Nodes.Add(NodePatientEducationMaterial)
                            End If

                        Case "ProviderReference"
                            If TargetNode.Parent.Nodes(0).Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Parent.Nodes(0).Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Parent.Nodes(0).Nodes.Add(NodePatientEducationMaterial)
                                End If
                            Else
                                TargetNode.Parent.Nodes(0).Nodes.Add(NodePatientEducationMaterial)
                            End If

                        Case "PatientEducation"
                            If TargetNode.Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Nodes.Add(NodePatientEducationMaterial)
                                End If
                            Else
                                TargetNode.Nodes.Add(NodePatientEducationMaterial)
                            End If

                        Case "PatientEducationMaterial"
                            If TargetNode.Parent.Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Parent.Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Parent.Nodes.Add(NodePatientEducationMaterial)
                                End If
                            Else
                                TargetNode.Parent.Nodes.Add(NodePatientEducationMaterial)
                            End If


                        Case "ProviderReferenceMaterial"
                            If TargetNode.Parent.Parent.Nodes(0).Nodes.Count > 0 Then
                                Dim nNodeID_RefMaterialNode As String = ReferenceMaterialNode.ID.ToString()
                                Dim boolNodeAlreadyExists = False

                                For Each ElementNode As myTreeNode In TargetNode.Parent.Parent.Nodes(0).Nodes

                                    Dim nNodeID_TargetNode As String = ElementNode.Tag.ToString.Remove(ElementNode.Tag.ToString.IndexOf("|"))

                                    If nNodeID_TargetNode.ToLower = nNodeID_RefMaterialNode.ToLower Then
                                        boolNodeAlreadyExists = True
                                    End If
                                Next

                                If boolNodeAlreadyExists = False Then
                                    TargetNode.Parent.Parent.Nodes(0).Nodes.Add(NodePatientEducationMaterial)
                                End If
                            Else
                                TargetNode.Parent.Parent.Nodes(0).Nodes.Add(NodePatientEducationMaterial)
                            End If

                    End Select

                    If NodePatientEducationMaterial IsNot Nothing Then
                        NodePatientEducationMaterial.Dispose()
                        NodePatientEducationMaterial = Nothing
                    End If

                End If

                UpdateDeleteDataTable(GetTargetNodeType(TargetNode), TargetNode)

            End If

            treeViewCodesAssociation.ExpandAll()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If TargetNode IsNot Nothing Then
                '   TargetNode.Dispose()
                TargetNode = Nothing
            End If

            If ReferenceMaterialNode IsNot Nothing Then
                ReferenceMaterialNode = Nothing
            End If

            sTargetNodeType = ""
            sReferenceMaterialNodeType = ""

        End Try

    End Sub

    Private Function IsDemographicsCorrect(ByVal IsLabs As Boolean) As Boolean
        Dim reValue As Boolean = False
        Try

            If IsLabs Then
                If Me.cmbAgeMax.Text = 0 And Me.cmbGender.Text = String.Empty And (txtValueOne.Text = String.Empty Or txtValueOne.Text = "") And (txtValueTwo.Text = String.Empty Or txtValueTwo.Text = "") Then
                    'If all values are empty or "" then its false
                    reValue = False
                Else
                    reValue = True
                    If Me.cmbAgeMax.Text = 0 And Me.cmbGender.Text = String.Empty And (txtValueOne.Text = String.Empty Or txtValueOne.Text = "") And (txtValueTwo.Text = String.Empty Or txtValueTwo.Text = "") Then
                        If cmbOperator.SelectedIndex = 0 Then
                            'Operator is 'Between' and ValueOne OR ValueTwo is empty
                            'then also its false. Or operator is important here.
                            If (txtValueOne.Text = String.Empty Or txtValueOne.Text = "") Or (txtValueTwo.Text = String.Empty Or txtValueTwo.Text = "") Then
                                reValue = False
                            End If
                        ElseIf cmbOperator.SelectedIndex > 0 Then
                            'For all other values ValueTwo should not be empty
                            If (txtValueTwo.Text = String.Empty Or txtValueTwo.Text = "") Then
                                reValue = False
                            End If
                        End If
                    End If
                End If

            Else
                If Me.cmbAgeMax.Text = 0 And Me.cmbGender.Text = String.Empty Then
                    reValue = False
                Else
                    reValue = True
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

        Return reValue

    End Function

    Private Sub UpdateDeleteDataTable(ByVal NodeType As String, ByVal nodeCurrent As myTreeNode)

        Dim sCode As String = String.Empty
        Dim sCodeType As String = String.Empty
        Dim oParentNode As myTreeNode = Nothing
        Try
            Select Case NodeType
                Case "ParentNode"
                    oParentNode = nodeCurrent
                Case "PatientEducationMaterial", "ProviderReferenceMaterial"
                    oParentNode = nodeCurrent.Parent.Parent
                Case "PatientEducation", "ProviderReference"
                    oParentNode = nodeCurrent.Parent
            End Select

            If oParentNode.IsICD9 Then
                sCode = GetTargetNodeID(oParentNode.Tag.ToString)
                sCodeType = clsEducationAssociationDatabaseLayer.Codes.ICD9.GetHashCode
            ElseIf oParentNode.IsSnomedCode Then
                sCode = GetTargetNodeID(oParentNode.Tag.ToString)
                sCodeType = clsEducationAssociationDatabaseLayer.Codes.SNOMED.GetHashCode
            ElseIf oParentNode.IsLabs Then
                sCode = GetTargetNodeID(oParentNode.Tag.ToString)
                sCodeType = clsEducationAssociationDatabaseLayer.Codes.LOINC.GetHashCode
            ElseIf oParentNode.IsMedication Then
                sCode = oParentNode.NDCCode
                sCodeType = clsEducationAssociationDatabaseLayer.Codes.NDC.GetHashCode

            End If

            If dataTableDelete IsNot Nothing Then
                If dataTableDelete.Rows.Count > 0 Then
                    Dim deleteRow As DataRow() = Nothing
                    deleteRow = dataTableDelete.Select("sCode='" + sCode.Replace("'", "''") + "' and sCodeType=" & sCodeType)
                    If deleteRow.Length > 0 Then
                        dataTableDelete.Rows.Remove(deleteRow(0))
                        dataTableDelete.AcceptChanges()
                    End If
                    deleteRow = Nothing
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            sCode = ""
            sCodeType = ""
            If oParentNode IsNot Nothing Then
                '   oParentNode.Dispose()
                oParentNode = Nothing
            End If
        End Try

    End Sub

    Private Sub treeViewCodesAssociation_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeViewCodesAssociation.AfterSelect

        Dim nodeSelected As myTreeNode = CType(treeViewCodesAssociation.SelectedNode, myTreeNode)

        Try

            If nodeSelected IsNot Nothing Then
                If nodeSelected.Name = "Patient Education" Or Me.GetTargetNodeType(nodeSelected) = "PatientEducationMaterial" Then
                    btnPatientEducation.PerformClick()
                ElseIf nodeSelected.Name = "Provider Reference" Or Me.GetTargetNodeType(nodeSelected) = "ProviderReferenceMaterial" Then
                    btnProviderReference.PerformClick()
                End If
                sCurrentSelectedNode = nodeSelected.Tag

                PanelLabResultTests(nodeSelected)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If nodeSelected IsNot Nothing Then
                nodeSelected.Dispose()
                nodeSelected = Nothing
            End If
        End Try

    End Sub

    Private Sub PanelLabResultTests(ByVal nodeSelected As myTreeNode)
        Dim nodeParent As myTreeNode = Nothing
        Select Case GetTargetNodeType(nodeSelected)
            Case "ProviderReference", "PatientEducation"
                nodeParent = CType(nodeSelected.Parent, myTreeNode)
            Case "ProviderReferenceMaterial", "PatientEducationMaterial"
                nodeParent = CType(nodeSelected.Parent.Parent, myTreeNode)
            Case "ParentNode"
                nodeParent = nodeSelected
        End Select

        If nodeParent.IsLabs Then
            pnlCriteriaLab.Visible = True

            If nodeSelected.IsProviderAdvanceMaterial Then
                chkAdvancedProviderReference.Checked = True
            Else
                chkAdvancedProviderReference.Checked = False
            End If
            If chkEnableDemographics.Checked Then
                cmbOperator.Enabled = True
                If (cmbOperator.Items.Count > 0) Then
                    cmbOperator.SelectedIndex = 0
                End If


                txtValueOne.Text = String.Empty
                txtValueTwo.Text = String.Empty
            Else
                cmbOperator.Enabled = False
            End If
        Else
            pnlCriteriaLab.Visible = False
        End If
        nodeParent = Nothing
    End Sub

    Private Function IsParentNodeLab(ByVal nodeSelected As myTreeNode) As Boolean
        Dim nodeParent As myTreeNode = Nothing
        Dim bReturned As Boolean = False
        Select Case GetTargetNodeType(nodeSelected)
            Case "ProviderReference", "PatientEducation"
                nodeParent = CType(nodeSelected.Parent, myTreeNode)
            Case "ProviderReferenceMaterial", "PatientEducationMaterial"
                nodeParent = CType(nodeSelected.Parent.Parent, myTreeNode)
            Case "ParentNode"
                nodeParent = nodeSelected
        End Select

        If nodeParent.IsLabs Then
            bReturned = True
        End If
        Return bReturned
    End Function

    Private Function GetParentNode(ByVal nodeSelected As myTreeNode)
        Dim nodeParent As myTreeNode = Nothing
        Select Case GetTargetNodeType(nodeSelected)
            Case "ProviderReference", "PatientEducation"
                nodeParent = CType(nodeSelected.Parent, myTreeNode)
            Case "ProviderReferenceMaterial", "PatientEducationMaterial"
                nodeParent = CType(nodeSelected.Parent.Parent, myTreeNode)
            Case "ParentNode"
                nodeParent = nodeSelected
        End Select
        Return nodeParent
    End Function

    Private Sub GetContextMenu(ByVal Sender As Object, ByVal e As MouseEventArgs) Handles treeViewCodesAssociation.MouseUp
        Try
            If e.Button = MouseButtons.Right Then
                treeViewCodesAssociation.SelectedNode = treeViewCodesAssociation.GetNodeAt(e.X, e.Y)
                contextMenus.Hide()
                'Try
                '    If (IsNothing(treeViewCodesAssociation.ContextMenuStrip) = False) Then
                '        treeViewCodesAssociation.ContextMenuStrip.Dispose()
                '        treeViewCodesAssociation.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                treeViewCodesAssociation.ContextMenuStrip = Nothing
                If treeViewCodesAssociation.SelectedNode IsNot Nothing Then
                    Select Case GetTargetNodeType(treeViewCodesAssociation.SelectedNode)
                        Case "PatientEducationMaterial", "ProviderReferenceMaterial"
                            contextMenus.Show(treeViewCodesAssociation, e.Location)
                            Dim nodeSelected As myTreeNode = CType(treeViewCodesAssociation.SelectedNode, myTreeNode)
                            If nodeSelected.MaximumAge = 0 And nodeSelected.Gender = String.Empty And nodeSelected.LabResultOperator = 0 And nodeSelected.LabResultValueOne = 0 And nodeSelected.LabResultValueTwo = 0 And nodeSelected.IsProviderAdvanceMaterial = 0 Then
                                contextMenus.Items(1).Enabled = False
                            Else
                                contextMenus.Items(1).Enabled = True
                            End If
                        Case "ParentNode"
                            'Try
                            '    If (IsNothing(treeViewCodesAssociation.ContextMenuStrip) = False) Then
                            '        treeViewCodesAssociation.ContextMenuStrip.Dispose()
                            '        treeViewCodesAssociation.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            treeViewCodesAssociation.ContextMenuStrip = contextRemoveParent

                            Dim nodeSelected As myTreeNode = CType(treeViewCodesAssociation.SelectedNode, myTreeNode)

                            If nodeSelected.IsLabs Then
                                toolStripParent.Text = "Remove Lab Test/Result"
                            ElseIf nodeSelected.IsICD9 Then
                                If (nodeSelected.nICDRevision = 9) Then ''added condition to display ICD 9 or 10 
                                    toolStripParent.Text = "Remove Problem(ICD9)"
                                End If
                                If (nodeSelected.nICDRevision = 10) Then
                                    toolStripParent.Text = "Remove Problem(ICD10)"
                                End If
                            ElseIf nodeSelected.IsMedication Then
                                toolStripParent.Text = "Remove Medication"
                            ElseIf nodeSelected.IsSnomedCode Then
                                toolStripParent.Text = "Remove Problem(SnoMed)"
                            End If
                            contextRemoveParent.Show(treeViewCodesAssociation, e.Location)
                        Case Else
                            contextRemoveParent.Hide()
                    End Select

                End If


            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub AddNode(ByVal oNode As gloUserControlLibrary.myTreeNode, ByVal IsSnomed As Boolean, Optional ByVal SnomedCode As String = "")

        Dim sNodeName As String = oNode.ID.ToString
        Dim bNodeExists As Boolean = False

        Try

            For Each Node As myTreeNode In treeViewCodesAssociation.Nodes
                Select Case eCurrentSelectedButton
                    Case DockingTags.ICD9Button
                        If collectionICD9 IsNot Nothing Then
                            If collectionICD9.ContainsKey(oNode.Code) Then
                                bNodeExists = True
                            End If
                        End If
                    Case DockingTags.ICD10Button   ''added for ICD10 implementation
                        If collectionICD10 IsNot Nothing Then
                            If collectionICD10.ContainsKey(oNode.Code) Then
                                bNodeExists = True
                            End If
                        End If
                    Case DockingTags.LabsButton
                        If collectionLabs IsNot Nothing Then
                            If collectionLabs.ContainsKey(oNode.Tag) Then
                                bNodeExists = True
                            End If
                        End If
                    Case DockingTags.MedicationButton
                        If collectionMedication IsNot Nothing Then
                            If collectionMedication.ContainsKey(oNode.ID) Then
                                bNodeExists = True
                            End If
                        End If
                    Case DockingTags.SnoMedButton
                        If collectionSnomed IsNot Nothing Then
                            If collectionSnomed.ContainsKey(oNode.ConceptID) Then
                                bNodeExists = True
                            End If
                        End If
                End Select
            Next

            If bNodeExists = False Then

                Dim nodeParentCode As New myTreeNode

                With nodeParentCode
                    .ImageIndex = 0
                    .SelectedImageIndex = 0
                    .Key = oNode.ID
                    .Text = oNode.Text
                    'Try
                    '    If (IsNothing(.ContextMenuStrip) = False) Then
                    '        .ContextMenuStrip.Dispose()
                    '        .ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    .ContextMenuStrip = contextMenus

                    If oNode.IsSnomedCode Then
                        .IsSnomedCode = True
                    End If


                    If eCurrentSelectedButton = DockingTags.ICD9Button Then
                        .ImageIndex = 6
                        .SelectedImageIndex = 6
                        .Tag = oNode.Code.ToString + "|ParentNode"
                        .IsICD9 = True
                        .nICDRevision = 9 ''added condition to display ICD 9 or 10 
                        If collectionICD9 Is Nothing Then
                            collectionICD9 = New Dictionary(Of String, DockingTags)
                        End If

                        collectionICD9.Add(oNode.Code.ToString, DockingTags.ICD9Button)

                    ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then  ''added for ICD10 implementation
                        .ImageIndex = 15
                        .SelectedImageIndex = 15
                        .Tag = oNode.Code.ToString + "|ParentNode"
                        .IsICD9 = True
                        .nICDRevision = 10  ''added condition to display ICD 9 or 10 
                        If collectionICD10 Is Nothing Then
                            collectionICD10 = New Dictionary(Of String, DockingTags)
                        End If

                        collectionICD10.Add(oNode.Code.ToString, DockingTags.ICD10Button)


                    ElseIf eCurrentSelectedButton = DockingTags.LabsButton Then
                        .ImageIndex = 9
                        .SelectedImageIndex = 9
                        .Tag = oNode.Tag.ToString + "|ParentNode"
                        .IsLabs = True
                        If collectionLabs Is Nothing Then
                            collectionLabs = New Dictionary(Of String, DockingTags)
                        End If

                        collectionLabs.Add(oNode.Tag.ToString, DockingTags.LabsButton)

                    ElseIf eCurrentSelectedButton = DockingTags.MedicationButton Then
                        .ImageIndex = 3
                        .SelectedImageIndex = 3
                        .Tag = oNode.ID.ToString + "|ParentNode"
                        .IsMedication = True
                        .DrugForm = oNode.DrugForm
                        .Route = oNode.Route
                        .Frequency = oNode.Frequency
                        .NDCCode = oNode.NDCCode
                        .IsNarcotics = oNode.IsNarcotics
                        .Duration = oNode.Duration
                        .DrugQtyQualifier = oNode.DrugQtyQualifier
                        .Dosage = oNode.Description
                        .mpid = oNode.mpid

                        If collectionMedication Is Nothing Then
                            collectionMedication = New Dictionary(Of String, DockingTags)
                        End If

                        collectionMedication.Add(oNode.ID.ToString, DockingTags.MedicationButton)

                    ElseIf eCurrentSelectedButton = DockingTags.SnoMedButton Then
                        .ImageIndex = 14
                        .SelectedImageIndex = 14
                        .Tag = oNode.ConceptID.ToString + "|ParentNode"

                        If SnomedCode = "NULL" Or SnomedCode = String.Empty Then
                            .SnomedID = oNode.SnomedID
                        Else
                            .SnomedID = oNode.Code
                        End If


                        If collectionSnomed Is Nothing Then
                            collectionSnomed = New Dictionary(Of String, DockingTags)
                        End If

                        collectionSnomed.Add(oNode.ConceptID.ToString, DockingTags.SnoMedButton)

                    End If
                End With


                treeViewCodesAssociation.Nodes.Add(nodeParentCode)

                Dim MyChild As New myTreeNode
                With MyChild
                    .Text = "Patient Education"
                    .Name = "PatientEducation"
                    .Tag = oNode.ID.ToString + "|PatientEducation"
                    .SelectedImageIndex = 5
                    .ImageIndex = 5

                End With


                nodeParentCode.Nodes.Add(MyChild)

                If eCurrentSelectedButton = DockingTags.ICD9Button Then
                    FillReferenceMaterialNodes(oNode.Code, eCurrentSelectedButton, 1, nodeParentCode)
                ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then  ''added for ICD10 implementation
                    FillReferenceMaterialNodes(oNode.Code, eCurrentSelectedButton, 1, nodeParentCode)

                ElseIf eCurrentSelectedButton = DockingTags.MedicationButton Then
                    FillReferenceMaterialNodes(oNode.mpid, eCurrentSelectedButton, 1, nodeParentCode)
                ElseIf eCurrentSelectedButton = DockingTags.LabsButton Then
                    FillReferenceMaterialNodes(oNode.Tag, eCurrentSelectedButton, 1, nodeParentCode)
                ElseIf eCurrentSelectedButton = DockingTags.SnoMedButton Then
                    If SnomedCode <> "NULL" Then
                        FillReferenceMaterialNodes(oNode.ConceptID.ToString, eCurrentSelectedButton, 1, nodeParentCode)
                    End If

                End If

                MyChild = New myTreeNode
                With MyChild
                    .Text = "Provider Reference"
                    .Name = "ProviderReference"
                    .Tag = oNode.ID.ToString + "|ProviderReference"
                    .SelectedImageIndex = 13
                    .ImageIndex = 13

                End With


                nodeParentCode.Nodes.Add(MyChild)

                If MyChild IsNot Nothing Then
                    MyChild.Dispose()
                    MyChild = Nothing
                End If

                If eCurrentSelectedButton = DockingTags.ICD9Button Then
                    FillReferenceMaterialNodes(oNode.Code, eCurrentSelectedButton, 2, nodeParentCode)
                ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then ''added for ICD10 implementation
                    FillReferenceMaterialNodes(oNode.Code, eCurrentSelectedButton, 2, nodeParentCode)

                ElseIf eCurrentSelectedButton = DockingTags.MedicationButton Then
                    FillReferenceMaterialNodes(oNode.mpid, eCurrentSelectedButton, 2, nodeParentCode)
                ElseIf eCurrentSelectedButton = DockingTags.LabsButton Then
                    FillReferenceMaterialNodes(oNode.Tag, eCurrentSelectedButton, 2, nodeParentCode)
                ElseIf eCurrentSelectedButton = DockingTags.SnoMedButton Then
                    If SnomedCode <> "NULL" Then
                        FillReferenceMaterialNodes(oNode.ConceptID.ToString, eCurrentSelectedButton, 2, nodeParentCode)
                    End If
                End If

                nodeParentCode.Expand()
                If nodeParentCode IsNot Nothing Then
                    nodeParentCode.Dispose()
                    nodeParentCode = Nothing
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            sNodeName = ""
            bNodeExists = False
        End Try

    End Sub

    Private Sub FillReferenceMaterialNodes(ByVal sCode As String, ByVal CurrentCodeSystem As DockingTags, ByVal nCurrentRefMaterialType As Integer, ByVal ParentNodeToAdd As myTreeNode)
        Try
            Dim sCodeDesc As String = Nothing
            Dim sMaterialType As String = Nothing
            Dim nRefMaterialType As Integer = 0

            If nCurrentRefMaterialType = 1 Then
                sMaterialType = "|PatientEducationMaterial"
                nRefMaterialType = 0
            ElseIf nCurrentRefMaterialType = 2 Then
                sMaterialType = "|ProviderReferenceMaterial"
                nRefMaterialType = 1
            End If

            If CurrentCodeSystem = DockingTags.ICD9Button Then
                sCodeDesc = "sICD9Code"
            ElseIf CurrentCodeSystem = DockingTags.ICD10Button Then ''added for ICD10 implementation
                sCodeDesc = "sICD9Code"
            ElseIf CurrentCodeSystem = DockingTags.MedicationButton Then
                sCodeDesc = "mpid"
            ElseIf CurrentCodeSystem = DockingTags.LabsButton Then
                sCodeDesc = "LOINCCode"
            ElseIf CurrentCodeSystem = DockingTags.SnoMedButton Then
                sCodeDesc = "sConceptID"
            End If


            Dim _drSettings As DataRow() = Nothing
            _drSettings = dataTableAssociatedCodes.Select(sCodeDesc + "='" + sCode.Replace("'", "''") + "' and nResourceType=" & nCurrentRefMaterialType)

            If _drSettings.Length > 0 Then

                For Each DataRow As DataRow In _drSettings

                    Dim NodePatientEducationMaterial As New myTreeNode()
                    With NodePatientEducationMaterial
                        .Tag = DataRow("nTemplateID").ToString + sMaterialType
                        .MinimumAge = DataRow("nAgeMin").ToString
                        .MaximumAge = DataRow("nAgeMax").ToString
                        .Gender = DataRow("sGender").ToString
                        .IsProviderAdvanceMaterial = DataRow("bIsAdvancedProviderReference").ToString

                        If CurrentCodeSystem = DockingTags.LabsButton Then
                            .LabResultValueOne = DataRow("nValueOne").ToString.TrimEnd("0")
                            .LabResultValueTwo = DataRow("nValueTwo").ToString.TrimEnd("0")
                            .LabResultOperator = DataRow("nOperator").ToString
                            .Text = DataRow("sTemplateName").ToString + GetDemographicsFromNode(NodePatientEducationMaterial, True)
                        Else
                            .Text = DataRow("sTemplateName").ToString + GetDemographicsFromNode(NodePatientEducationMaterial)
                        End If

                        .TemplateName = DataRow("sTemplateName").ToString
                        'Try
                        '    If (IsNothing(.ContextMenuStrip) = False) Then
                        '        .ContextMenuStrip.Dispose()
                        '        .ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenuStrip = contextMenus

                        If nCurrentRefMaterialType = 1 Then
                            .IsPatientEducationMaterial = True
                            .IsProviderReferenceMaterial = False
                        Else
                            .IsProviderReferenceMaterial = True
                            .IsPatientEducationMaterial = False
                        End If

                    End With
                    ParentNodeToAdd.Nodes(nRefMaterialType).Nodes.Add(NodePatientEducationMaterial)
                Next
                ParentNodeToAdd.Nodes(nRefMaterialType).Expand()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub FillCodesTreeView(ByVal Sender As DockingTags, ByVal BindingDataTable As DataTable)
        Try
            CodesTreeView.Clear()

            Dim sColumnName As String = Nothing
            Dim sDescription As String = Nothing
            Dim sCode As String = Nothing

            If Sender = DockingTags.ICD9Button Or Sender = DockingTags.ICD10Button Then  ''added for ICD10 implementation
                sColumnName = "nICD9ID"
                sDescription = "sDescription"
                sCode = "ICD9Code"
            ElseIf Sender = DockingTags.MedicationButton Then
                sColumnName = "DrugsID"
                sDescription = "DrugName"
                sCode = "sNDCCode"
            ElseIf Sender = DockingTags.LabsButton Then
                sColumnName = "LOINCID"
                sDescription = "LOINCLongName"
                sCode = "LOINCCode"
            ElseIf Sender = DockingTags.SnoMedButton Then
                sColumnName = "nSnomedMappingID"
                sDescription = "sSnomedDescription"
            End If

            With CodesTreeView
                .DataSource = BindingDataTable
                .ValueMember = BindingDataTable.Columns(sColumnName).ColumnName
                If Sender = DockingTags.LabsButton Then
                    .Tag = BindingDataTable.Columns(1).ColumnName
                Else
                    .Tag = BindingDataTable.Columns(0).ColumnName
                End If

                .DescriptionMember = BindingDataTable.Columns(sDescription).ColumnName
                .DrugFormMember = Nothing
                .NDCCodeMember = Nothing
                .mpidmember = Nothing
                If Sender = DockingTags.MedicationButton Then
                    .IsDrug = True
                    .IsSearchForEducationMapping = True
                    'For enabling search on Medication codes
                Else
                    .IsDrug = False
                    .IsSearchForEducationMapping = False
                End If

                If Sender = DockingTags.ICD9Button Or Sender = DockingTags.ICD10Button Then ''or condition added for ICD10 implementation
                    .IsDiagnosisSearch = True
                    .CodeMember = BindingDataTable.Columns("ICD9Code").ColumnName
                    .DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description

                ElseIf Sender = DockingTags.MedicationButton Then
                    .IsDiagnosisSearch = False
                    .IsDrug = True
                    .DrugFlag = 16 ''For all drugs 
                    .ValueMember = BindingDataTable.Columns("DrugsID").ColumnName
                    .DescriptionMember = BindingDataTable.Columns("Dosage").ColumnName
                    .CodeMember = BindingDataTable.Columns("DrugName").ColumnName
                    .mpidmember = BindingDataTable.Columns("mpid").ColumnName
                    .NDCCodeMember = BindingDataTable.Columns("sNDCCode").ColumnName
                    .DrugFormMember = BindingDataTable.Columns("DrugForm").ColumnName
                    .DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                ElseIf Sender = DockingTags.LabsButton Then
                    .IsDiagnosisSearch = False
                    .CodeMember = BindingDataTable.Columns("LOINCCode").ColumnName
                    .DrugFormMember = Nothing
                    .DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                ElseIf Sender = DockingTags.SnoMedButton Then
                    .IsDiagnosisSearch = False
                    .CodeMember = BindingDataTable.Columns("sSnomedID").ColumnName
                    .DrugFormMember = Nothing
                    .DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                Else
                    .IsDiagnosisSearch = False
                    'Other then ICD9Code we don't want to display the codes for the descriptions
                    .CodeMember = ""
                    .DrugFormMember = Nothing
                    .DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                End If




                .FillTreeView()


            End With

            BindingDataTable.Clear()
            BindingDataTable.Dispose()
            BindingDataTable = Nothing

            CodesTreeView.BringToFront()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillReferenceMaterialTreeView(ByVal Sender As clsEducationAssociationDatabaseLayer.Template)

        Dim clsEducationDBLayer As New clsEducationAssociationDatabaseLayer()
        Dim TemplatesDataTable As New DataTable
        Dim sValueMember As String = "nTemplateID"
        Dim sDescriptionMember As String = "sTemplateName"

        Try

            If Sender = clsEducationAssociationDatabaseLayer.Template.PatientEducationMaterial Then
                TemplatesDataTable = clsEducationDBLayer.GetTemplates(clsEducationAssociationDatabaseLayer.Template.PatientEducationMaterial)
            ElseIf Sender = clsEducationAssociationDatabaseLayer.Template.ProviderReferenceMaterial Then
                TemplatesDataTable = clsEducationDBLayer.GetTemplates(clsEducationAssociationDatabaseLayer.Template.ProviderReferenceMaterial)
            End If


            With UCtrlReferenceMaterial
                .Clear()
                .DataSource = TemplatesDataTable
                .ValueMember = TemplatesDataTable.Columns(sValueMember).ColumnName
                .CodeMember = TemplatesDataTable.Columns(sValueMember).ColumnName
                .Tag = TemplatesDataTable.Columns("nTemplateID").ColumnName
                .DescriptionMember = TemplatesDataTable.Columns(sDescriptionMember).ColumnName
                .DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                .Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                .FillTreeView()

                If Sender = clsEducationAssociationDatabaseLayer.Template.ProviderReferenceMaterial Then
                    sUCtrlMaterialType = "ProviderReference"
                ElseIf Sender = clsEducationAssociationDatabaseLayer.Template.PatientEducationMaterial Then
                    sUCtrlMaterialType = "PatientEducation"
                End If

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If clsEducationDBLayer IsNot Nothing Then
                clsEducationDBLayer.Dispose()
                clsEducationDBLayer = Nothing

            End If
            If TemplatesDataTable IsNot Nothing Then
                TemplatesDataTable.Clear()
                TemplatesDataTable.Dispose()
                TemplatesDataTable = Nothing
                sValueMember = ""
                sDescriptionMember = ""
            End If
        End Try

    End Sub

    Private Sub AddLabResultsValue(ByVal Node As myTreeNode)

        Select Case cmbOperator.SelectedIndex
            Case -1, 0
                With Node
                    .LabResultValueOne = 0
                    .LabResultValueTwo = 0
                    .LabResultOperator = 0
                End With

            Case Else
                If txtValueOne.Text = String.Empty Then
                    Node.LabResultValueOne = 0
                Else
                    Node.LabResultValueOne = Convert.ToDecimal(txtValueOne.Text)
                End If

                If txtValueTwo.Text = String.Empty Then
                    With Node
                        .LabResultValueTwo = 0
                        .LabResultOperator = 0
                    End With
                Else
                    With Node
                        .LabResultValueTwo = Convert.ToDecimal(txtValueTwo.Text)
                        .LabResultOperator = cmbOperator.SelectedIndex
                    End With

                End If

        End Select

        'If cmbOperator.SelectedIndex > 1 Then
        '    With Node
        '        .LabResultValueOne = 0
        '        If txtValueTwo.Text = String.Empty Then
        '            .LabResultValueOne = 0
        '        Else
        '            .LabResultValueTwo = Convert.ToDecimal(txtValueTwo.Text)
        '        End If
        ''    End With
        'End If

        'Node.LabResultOperator = cmbOperator.SelectedIndex

    End Sub

    Private Sub AddReferenceMaterialToNode(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles CodesTreeView.NodeMouseDoubleClick

        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

            If Not IsNothing(oNode) Then
                If eCurrentSelectedButton = DockingTags.SnoMedButton Then
                    oNode.IsSnomedCode = True
                    oNode.ConceptID = e.Node.Tag.ToString
                    AddNode(oNode, True)
                Else
                    AddNode(oNode, False)
                End If


            End If
            If (cmbAgeMin.Items.Count > 0) Then
                cmbAgeMin.SelectedIndex = 0
            End If
            If (cmbAgeMax.Items.Count > 0) Then
                cmbAgeMax.SelectedIndex = 0
            End If
            If (cmbGender.Items.Count > 0) Then
                cmbGender.SelectedIndex = 0
            End If

        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Combo Boxes"

    Private Sub cmbOperatorItemChanged(sender As Object, e As System.EventArgs) Handles cmbOperator.SelectedIndexChanged
        Try
            Select Case cmbOperator.SelectedIndex
                Case 0
                    With txtValueOne
                        .Enabled = False
                        .Text = String.Empty
                    End With

                    With txtValueTwo
                        .Enabled = False
                        .Text = String.Empty
                    End With
                Case 1
                    txtValueOne.Enabled = True
                    txtValueTwo.Enabled = True
                Case Else
                    With txtValueOne
                        .Enabled = False
                        .Text = String.Empty
                    End With

                    txtValueTwo.Enabled = True
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub cmbOperator_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbOperator.SelectedValueChanged
        '0 Nothing
        '1 Between
        '2 Equals
        '3 Greater than
        '4 Less than
        Try
            Dim nSelectedIndex As Integer = cmbOperator.SelectedIndex

            If nSelectedIndex = 0 Then
                txtValueOne.Enabled = True
                txtValueTwo.Enabled = True
            Else
                txtValueOne.Enabled = False
                txtValueTwo.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub cmbAgeMin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If cmbAgeMax.SelectedIndex <> Nothing Then
                If cmbAgeMax.SelectedIndex < cmbAgeMin.SelectedIndex Then
                    cmbAgeMax.SelectedIndex = cmbAgeMin.SelectedIndex
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub cmbAgeMax_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If cmbAgeMin.SelectedIndex <> Nothing Then
                If cmbAgeMin.SelectedIndex > cmbAgeMax.SelectedIndex Then
                    cmbAgeMin.SelectedIndex = cmbAgeMax.SelectedIndex
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

#End Region

#Region "Radio Buttons"

    Private Sub rbtAssociated_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtAssociated.CheckedChanged
        Try

            If rbtAssociated.Checked Then  ''code change  added against bug 63743
                If Cursor = Cursors.Default Then
                    Cursor = Cursors.WaitCursor

                    Select Case eCurrentSelectedButton
                        Case DockingTags.ICD9Button
                            FillCodesTreeView(DockingTags.ICD9Button, GetDataTablesForFillingTreeView(DockingTags.ICD9Button, 2, CodesTreeView.txtsearch.Text.Trim()))
                            For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                                ElementNode.ImageIndex = 6
                                ElementNode.SelectedImageIndex = 6
                            Next
                        Case DockingTags.ICD10Button ''added for ICD10 implementation
                            FillCodesTreeView(DockingTags.ICD10Button, GetDataTablesForFillingTreeView(DockingTags.ICD10Button, 2, CodesTreeView.txtsearch.Text.Trim()))
                            For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                                ElementNode.ImageIndex = 15
                                ElementNode.SelectedImageIndex = 15
                            Next
                        Case DockingTags.LabsButton
                            FillCodesTreeView(DockingTags.LabsButton, GetDataTablesForFillingTreeView(DockingTags.LabsButton, 2))
                            For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                                ElementNode.ImageIndex = 9
                                ElementNode.SelectedImageIndex = 9
                            Next
                        Case DockingTags.MedicationButton
                            CodesTreeView.EducationMappingSearchType = 2
                            FillCodesTreeView(DockingTags.MedicationButton, GetDataTablesForFillingTreeView(DockingTags.MedicationButton, 2))
                            For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                                ElementNode.ImageIndex = 3
                                ElementNode.SelectedImageIndex = 3
                            Next

                    End Select
                    Cursor = Cursors.Default
                    If (rbtAssociated.Checked = False) Then
                        RemoveHandler rbtAssociated.CheckedChanged, AddressOf rbtAssociated_CheckedChanged
                        rbtAssociated.Checked = True
                        AddHandler rbtAssociated.CheckedChanged, AddressOf rbtAssociated_CheckedChanged

                    End If
                Else
                    rbtAssociated.Checked = False
                End If
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            If (rbtAssociated.Checked = False) Then
                RemoveHandler rbtAssociated.CheckedChanged, AddressOf rbtAssociated_CheckedChanged
                rbtAssociated.Checked = True
                AddHandler rbtAssociated.CheckedChanged, AddressOf rbtAssociated_CheckedChanged

            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)




        End Try

    End Sub



    Private Sub rbtUnassociated_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtUnassociated.CheckedChanged
        Try

            If rbtUnassociated.Checked Then  ''code change  added against bug 63743
                If Cursor = Cursors.Default Then
                    Cursor = Cursors.WaitCursor

                    Select Case eCurrentSelectedButton
                        Case DockingTags.ICD9Button
                            FillCodesTreeView(DockingTags.ICD9Button, GetDataTablesForFillingTreeView(DockingTags.ICD9Button, 3, CodesTreeView.txtsearch.Text.Trim()))
                        Case DockingTags.ICD10Button  ''added for ICD10 implementation
                            FillCodesTreeView(DockingTags.ICD10Button, GetDataTablesForFillingTreeView(DockingTags.ICD10Button, 3, CodesTreeView.txtsearch.Text.Trim()))

                        Case DockingTags.LabsButton
                            FillCodesTreeView(DockingTags.LabsButton, GetDataTablesForFillingTreeView(DockingTags.LabsButton, 3))
                        Case DockingTags.MedicationButton
                            CodesTreeView.EducationMappingSearchType = 3
                            FillCodesTreeView(DockingTags.MedicationButton, GetDataTablesForFillingTreeView(DockingTags.MedicationButton, 3))
                    End Select
                    Cursor = Cursors.Default
                    If (rbtUnassociated.Checked = False) Then
                        RemoveHandler rbtUnassociated.CheckedChanged, AddressOf rbtUnassociated_CheckedChanged
                        rbtUnassociated.Checked = True
                        AddHandler rbtUnassociated.CheckedChanged, AddressOf rbtUnassociated_CheckedChanged

                    End If
                Else
                    rbtUnassociated.Checked = False
                End If
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            If (rbtUnassociated.Checked = False) Then
                RemoveHandler rbtUnassociated.CheckedChanged, AddressOf rbtUnassociated_CheckedChanged
                rbtUnassociated.Checked = True
                AddHandler rbtUnassociated.CheckedChanged, AddressOf rbtUnassociated_CheckedChanged

            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)


        End Try

    End Sub





    Private Sub rbtAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtAll.CheckedChanged
        Try
            If rbtAll.Checked Then ''code change  added against bug 63743
                If Cursor = Cursors.Default Then
                    Cursor = Cursors.WaitCursor

                    Select Case eCurrentSelectedButton
                        Case DockingTags.ICD9Button
                            FillCodesTreeView(DockingTags.ICD9Button, GetDataTablesForFillingTreeView(DockingTags.ICD9Button, 1, CodesTreeView.txtsearch.Text.Trim()))
                            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD9)
                        Case DockingTags.ICD10Button  ''added for ICD10 implementation
                            FillCodesTreeView(DockingTags.ICD10Button, GetDataTablesForFillingTreeView(DockingTags.ICD10Button, 1, CodesTreeView.txtsearch.Text.Trim()))
                            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD10)
                        Case DockingTags.LabsButton
                            FillCodesTreeView(DockingTags.LabsButton, GetDataTablesForFillingTreeView(DockingTags.LabsButton, 1))
                            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.LOINC)
                        Case DockingTags.MedicationButton
                            CodesTreeView.EducationMappingSearchType = 1
                            FillCodesTreeView(DockingTags.MedicationButton, GetDataTablesForFillingTreeView(DockingTags.MedicationButton, 1))
                            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.NDC)
                    End Select
                    Cursor = Cursors.Default
                    If (rbtAll.Checked = False) Then
                        RemoveHandler rbtAll.CheckedChanged, AddressOf rbtAll_CheckedChanged
                        rbtAll.Checked = True
                        AddHandler rbtAll.CheckedChanged, AddressOf rbtAll_CheckedChanged

                    End If
                Else
                    rbtAll.Checked = False
                End If

            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            If (rbtAll.Checked = False) Then
                RemoveHandler rbtAll.CheckedChanged, AddressOf rbtAll_CheckedChanged
                rbtAll.Checked = True
                AddHandler rbtAll.CheckedChanged, AddressOf rbtAll_CheckedChanged

            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)



        End Try
    End Sub


#End Region

#Region "Text Boxes"

    Private Sub txtValueTwo_KeyUp(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtValueTwo.KeyPress, txtValueOne.KeyPress
        Dim objTextBox As TextBox = CType(sender, TextBox)

        Try
            If Char.IsNumber(e.KeyChar.ToString) = False And Char.ToLower(e.KeyChar) <> "." And Char.IsControl(e.KeyChar) = False Then
                e.Handled = True
            End If

            If Char.ToLower(e.KeyChar) = "." Then
                If objTextBox.Text.ToString.Contains(".") Then
                    e.Handled = True
                End If
            End If

            If Char.ToLower(e.KeyChar) = "-" Then
                If objTextBox.Text.ToString.Contains("-") Then
                    e.Handled = True
                Else
                    objTextBox.Select(0, 0)
                    e.Handled = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        objTextBox = Nothing
    End Sub

#End Region

#Region "Context Menus"

    Private Sub RemoveAssociationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveAssociationToolStripMenuItem.Click
        Try
            If treeViewCodesAssociation.SelectedNode IsNot Nothing Then
                Select Case GetTargetNodeType(treeViewCodesAssociation.SelectedNode)
                    Case "PatientEducationMaterial", "ProviderReferenceMaterial"
                        Dim nNodeIndex As Integer = Nothing
                        nNodeIndex = treeViewCodesAssociation.SelectedNode.Parent.Parent.Index
                        treeViewCodesAssociation.SelectedNode.Remove()
                        CheckForDelete(nNodeIndex)
                        If (cmbAgeMin.Items.Count > 0) Then
                            cmbAgeMin.SelectedIndex = 0
                        End If
                        If (cmbAgeMax.Items.Count > 0) Then
                            cmbAgeMax.SelectedIndex = 0
                        End If
                        If (cmbGender.Items.Count > 0) Then
                            cmbGender.SelectedIndex = 0
                        End If
                End Select

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub RemoveDemographicsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveDemographicsToolStripMenuItem.Click
        Try
            If treeViewCodesAssociation.SelectedNode IsNot Nothing Then
                Dim nodeSelected As myTreeNode = CType(treeViewCodesAssociation.SelectedNode, myTreeNode)

                nodeSelected.MaximumAge = 0
                nodeSelected.MinimumAge = 0
                nodeSelected.Gender = String.Empty

                nodeSelected.LabResultOperator = 0
                nodeSelected.LabResultValueOne = 0
                nodeSelected.LabResultValueTwo = 0
                nodeSelected.IsProviderAdvanceMaterial = 0

                nodeSelected.Text = nodeSelected.TemplateName

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub RemoveParentNode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles contextRemoveParent.Click
        Try
            If (Not IsNothing(treeViewCodesAssociation.SelectedNode)) Then
                If MouseButtons.Right Then
                    Dim ID As String = GetTargetNodeID(treeViewCodesAssociation.SelectedNode.Tag.ToString())

                    If collectionICD9 IsNot Nothing AndAlso collectionICD9.ContainsKey(ID) Then
                        collectionICD9.Remove(ID)
                    ElseIf collectionICD10 IsNot Nothing AndAlso collectionICD10.ContainsKey(ID) Then   ''added for ICD10 implementation
                        collectionICD10.Remove(ID)
                    ElseIf collectionSnomed IsNot Nothing AndAlso collectionSnomed.ContainsKey(ID) Then
                        collectionSnomed.Remove(ID)
                    ElseIf collectionMedication IsNot Nothing AndAlso collectionMedication.ContainsKey(ID) Then
                        collectionMedication.Remove(ID)
                    ElseIf collectionLabs IsNot Nothing AndAlso collectionLabs.ContainsKey(ID) Then
                        collectionLabs.Remove(ID)
                    End If

                    treeViewCodesAssociation.SelectedNode.Remove()
                    contextRemoveParent.Hide()

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

#End Region

#Region "Checkboxes"

    Private Sub chkEnableDemographics_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEnableDemographics.CheckedChanged
        Try
            If chkEnableDemographics.CheckState = CheckState.Checked Then
                cmbAgeMin.Enabled = True
                cmbAgeMax.Enabled = True
                cmbGender.Enabled = True

                Dim nodeParent As myTreeNode = Nothing
                If CType(treeViewCodesAssociation.SelectedNode, myTreeNode) IsNot Nothing Then
                    nodeParent = (GetParentNode(CType(treeViewCodesAssociation.SelectedNode, myTreeNode)))
                End If

                If eCurrentSelectedButton = DockingTags.LabsButton Then
                    DemographicsLabResults(True)
                    If eCurrentReferenceMaterial = EducationTag.Provider Then
                        chkAdvancedProviderReference.Enabled = True
                    End If
                End If

                If nodeParent IsNot Nothing Then
                    If nodeParent.IsLabs Then
                        DemographicsLabResults(True)
                        If eCurrentReferenceMaterial = EducationTag.Provider Then
                            chkAdvancedProviderReference.Enabled = True
                        End If
                    End If
                End If

                nodeParent = Nothing
            Else
                DemographicsLabResults(False)

                cmbAgeMin.Enabled = False
                cmbAgeMax.Enabled = False
                cmbGender.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

#End Region

#Region "Data Tables"

    Private Sub BuildDatatables()
        Try
            If dataTableICD9 Is Nothing Then
                dataTableICD9 = New DataTable("ICD9")

                With dataTableICD9.Columns

                    .Add(New DataColumn("nICD9MappingID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("sICD9Code", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sICD9CodeDescription", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nTemplateID", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("nResourceType", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("nAgeMin", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("nAgeMax", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("sGender", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("bIsAdvancedProviderReference", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("nICDRevision", System.Type.GetType("System.Int32"))) ''added for ICD10 implementation
                End With
            End If

            If dataTableMedication Is Nothing Then
                dataTableMedication = New DataTable("Medication")

                With dataTableMedication.Columns
                    .Add(New DataColumn("nMedMappingID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("nDrugsID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("sDrugName", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sDosage", System.Type.GetType("System.String")))

                    .Add(New DataColumn("sDrugForm", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sRoute", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sFrequency", System.Type.GetType("System.String")))

                    .Add(New DataColumn("sNDCCode", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nIsNarcotics", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("sDuration", System.Type.GetType("System.String")))

                    .Add(New DataColumn("mpid", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("sDrugQtyQualifier", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nTemplateID", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("nResourceType", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("nAgeMin", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("nAgeMax", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("sGender", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("bIsAdvancedProviderReference", System.Type.GetType("System.Int32")))
                End With
            End If

            If dataTableLabResults Is Nothing Then
                dataTableLabResults = New DataTable("LabResults")

                With dataTableLabResults.Columns
                    .Add(New DataColumn("nLabResultMappingID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("LOINCCode", System.Type.GetType("System.String")))
                    .Add(New DataColumn("LOINCLongName", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nTemplateID", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("nResourceType", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("nAgeMin", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("nAgeMax", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("sGender", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("nValueOne", System.Type.GetType("System.Decimal")))

                    .Add(New DataColumn("nValueTwo", System.Type.GetType("System.Decimal")))
                    .Add(New DataColumn("nOperator", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("bIsAdvancedProviderReference", System.Type.GetType("System.Int32")))
                End With
            End If

            If dataTableSnomed Is Nothing Then
                dataTableSnomed = New DataTable("Snomed")

                With dataTableSnomed.Columns
                    .Add(New DataColumn("nSnomedMappingID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("sConceptID", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sSnomedID", System.Type.GetType("System.String")))
                    .Add(New DataColumn("sSnomedDescription", System.Type.GetType("System.String")))

                    .Add(New DataColumn("nTemplateID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("nResourceType", System.Type.GetType("System.Int32")))
                    .Add(New DataColumn("nAgeMin", System.Type.GetType("System.Int64")))

                    .Add(New DataColumn("nAgeMax", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("sGender", System.Type.GetType("System.String")))
                    .Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
                    .Add(New DataColumn("bIsAdvancedProviderReference", System.Type.GetType("System.Int32")))
                End With
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub InsertIntoICDDataTable(ByVal Node As myTreeNode, ByVal IcdVersion As Integer)
        Try
            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes.Count > 0 Then
                'For entering Patient Education Material information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes
                    Dim ICD9DataRow As DataRow = dataTableICD9.NewRow()
                    ICD9DataRow("nICD9MappingID") = 0

                    ICD9DataRow("sICD9Code") = (GetTargetNodeID(CType(Node.Tag, String)))
                    ICD9DataRow("sICD9CodeDescription") = Node.Text.ToString
                    ICD9DataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    ICD9DataRow("nResourceType") = GetResourceType(ReferenceMaterialNode)
                    ICD9DataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    ICD9DataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    ICD9DataRow("sGender") = ReferenceMaterialNode.Gender
                    ICD9DataRow("nUserID") = gnLoginID

                    ICD9DataRow("bIsAdvancedProviderReference") = 0
                    ICD9DataRow("nICDRevision") = IcdVersion
                    dataTableICD9.Rows.Add(ICD9DataRow)
                    ICD9DataRow = Nothing
                Next
            End If

            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes.Count > 0 Then
                'For entering Provider Reference Information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes
                    Dim ICD9DataRow As DataRow = dataTableICD9.NewRow()
                    ICD9DataRow("nICD9MappingID") = 0

                    ICD9DataRow("sICD9Code") = (GetTargetNodeID(CType(Node.Tag, String)))
                    ICD9DataRow("sICD9CodeDescription") = Node.Text.ToString
                    ICD9DataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    ICD9DataRow("nResourceType") = GetResourceType(ReferenceMaterialNode)
                    ICD9DataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    ICD9DataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    ICD9DataRow("sGender") = ReferenceMaterialNode.Gender
                    ICD9DataRow("nUserID") = gnLoginID

                    ICD9DataRow("bIsAdvancedProviderReference") = ReferenceMaterialNode.IsProviderAdvanceMaterial
                    ICD9DataRow("nICDRevision") = IcdVersion
                    dataTableICD9.Rows.Add(ICD9DataRow)
                    ICD9DataRow = Nothing
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub InsertIntoMedicationDataTable(ByVal Node As myTreeNode)
        Try
            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes.Count > 0 Then
                'For entering Patient Education Material information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes
                    Dim MedicationDataRow As DataRow = dataTableMedication.NewRow()
                    MedicationDataRow("nMedMappingID") = 0

                    MedicationDataRow("nDrugsID") = (GetTargetNodeID(CType(Node.Tag, String)))
                    MedicationDataRow("sDrugName") = Node.Text.ToString


                    MedicationDataRow("sDosage") = Node.Dosage
                    MedicationDataRow("sDrugForm") = Node.DrugForm
                    MedicationDataRow("sRoute") = Node.Route

                    MedicationDataRow("sFrequency") = Node.Frequency
                    MedicationDataRow("sNDCCode") = Node.NDCCode
                    MedicationDataRow("nIsNarcotics") = Node.IsNarcotics

                    ''
                    MedicationDataRow("sDuration") = Node.Duration
                    MedicationDataRow("mpid") = Node.mpid
                    MedicationDataRow("sDrugQtyQualifier") = Node.DrugQtyQualifier

                    MedicationDataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    MedicationDataRow("nResourceType") = GetResourceType(ReferenceMaterialNode)
                    MedicationDataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    MedicationDataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    MedicationDataRow("sGender") = ReferenceMaterialNode.Gender
                    MedicationDataRow("nUserID") = gnLoginID
                    MedicationDataRow("bIsAdvancedProviderReference") = 0

                    dataTableMedication.Rows.Add(MedicationDataRow)
                    MedicationDataRow = Nothing
                Next
            End If

            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes.Count > 0 Then
                'For entering Provider Reference Information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes
                    Dim MedicationDataRow As DataRow = dataTableMedication.NewRow()
                    MedicationDataRow("nMedMappingID") = 0

                    MedicationDataRow("nDrugsID") = (GetTargetNodeID(CType(Node.Tag, String)))
                    MedicationDataRow("sDrugName") = Node.Text.ToString


                    MedicationDataRow("sDosage") = Node.Dosage
                    MedicationDataRow("sDrugForm") = Node.DrugForm
                    MedicationDataRow("sRoute") = Node.Route

                    MedicationDataRow("sFrequency") = Node.Frequency
                    MedicationDataRow("sNDCCode") = Node.NDCCode
                    MedicationDataRow("nIsNarcotics") = Node.IsNarcotics

                    ''
                    MedicationDataRow("sDuration") = Node.Duration
                    MedicationDataRow("mpid") = Node.mpid
                    MedicationDataRow("sDrugQtyQualifier") = Node.DrugQtyQualifier

                    MedicationDataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    MedicationDataRow("nResourceType") = GetResourceType(ReferenceMaterialNode)
                    MedicationDataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    MedicationDataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    MedicationDataRow("sGender") = ReferenceMaterialNode.Gender
                    MedicationDataRow("nUserID") = gnLoginID

                    MedicationDataRow("bIsAdvancedProviderReference") = ReferenceMaterialNode.IsProviderAdvanceMaterial
                    dataTableMedication.Rows.Add(MedicationDataRow)
                    MedicationDataRow = Nothing
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub InsertIntoLabsDataTable(ByVal Node As myTreeNode)
        Try
            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes.Count > 0 Then
                'For entering Patient Education Material information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes
                    Dim LabsDataRow As DataRow = dataTableLabResults.NewRow()
                    LabsDataRow("nLabResultMappingID") = 0

                    LabsDataRow("LOINCCode") = (GetTargetNodeID(CType(Node.Tag, String)))
                    LabsDataRow("LOINCLongName") = Node.Text.ToString

                    LabsDataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    LabsDataRow("nResourceType") = GetResourceType(ReferenceMaterialNode)
                    LabsDataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    LabsDataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    LabsDataRow("sGender") = ReferenceMaterialNode.Gender
                    LabsDataRow("nUserID") = gnLoginID


                    LabsDataRow("nValueOne") = ReferenceMaterialNode.LabResultValueOne
                    LabsDataRow("nValueTwo") = ReferenceMaterialNode.LabResultValueTwo
                    LabsDataRow("nOperator") = ReferenceMaterialNode.LabResultOperator

                    LabsDataRow("bIsAdvancedProviderReference") = 0

                    dataTableLabResults.Rows.Add(LabsDataRow)
                    LabsDataRow = Nothing
                Next
            End If

            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes.Count > 0 Then
                'For entering Provider Reference Information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes
                    Dim LabsDataRow As DataRow = dataTableLabResults.NewRow()
                    LabsDataRow("nLabResultMappingID") = 0

                    LabsDataRow("LOINCCode") = (GetTargetNodeID(CType(Node.Tag, String)))
                    LabsDataRow("LOINCLongName") = Node.Text.ToString

                    LabsDataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    LabsDataRow("nResourceType") = GetResourceType(ReferenceMaterialNode)
                    LabsDataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    LabsDataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    LabsDataRow("sGender") = ReferenceMaterialNode.Gender
                    LabsDataRow("nUserID") = gnLoginID

                    LabsDataRow("nValueOne") = ReferenceMaterialNode.LabResultValueOne
                    LabsDataRow("nValueTwo") = ReferenceMaterialNode.LabResultValueTwo
                    LabsDataRow("nOperator") = ReferenceMaterialNode.LabResultOperator

                    LabsDataRow("bIsAdvancedProviderReference") = ReferenceMaterialNode.IsProviderAdvanceMaterial

                    dataTableLabResults.Rows.Add(LabsDataRow)
                    LabsDataRow = Nothing
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub InsertIntoSnomedDataTable(ByVal Node As myTreeNode)
        Try
            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes.Count > 0 Then
                'For entering Patient Education Material information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(0).Nodes
                    Dim SnomedDataRow As DataRow = dataTableSnomed.NewRow()
                    SnomedDataRow("nSnomedMappingID") = 0

                    SnomedDataRow("sConceptID") = (GetTargetNodeID(CType(Node.Tag, String)))
                    SnomedDataRow("sSnomedID") = Node.SnomedID

                    SnomedDataRow("sSnomedDescription") = Node.Text.ToString

                    SnomedDataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    SnomedDataRow("nResourceType") = 1
                    SnomedDataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    SnomedDataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    SnomedDataRow("sGender") = ReferenceMaterialNode.Gender
                    SnomedDataRow("nUserID") = gnLoginID

                    SnomedDataRow("bIsAdvancedProviderReference") = 0
                    dataTableSnomed.Rows.Add(SnomedDataRow)
                    SnomedDataRow = Nothing
                Next
            End If

            If treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes.Count > 0 Then
                'For entering Provider Reference Information
                For Each ReferenceMaterialNode As myTreeNode In treeViewCodesAssociation.Nodes(Node.Index).Nodes(1).Nodes
                    Dim SnomedDataRow As DataRow = dataTableSnomed.NewRow()
                    SnomedDataRow("nSnomedMappingID") = 0

                    SnomedDataRow("sConceptID") = (GetTargetNodeID(CType(Node.Tag, String)))
                    SnomedDataRow("sSnomedID") = Node.SnomedID

                    SnomedDataRow("sSnomedDescription") = Node.Text.ToString

                    SnomedDataRow("nTemplateID") = (GetTargetNodeID(CType(ReferenceMaterialNode.Tag, String)))
                    SnomedDataRow("nResourceType") = 2
                    SnomedDataRow("nAgeMin") = ReferenceMaterialNode.MinimumAge

                    SnomedDataRow("nAgeMax") = ReferenceMaterialNode.MaximumAge
                    SnomedDataRow("sGender") = ReferenceMaterialNode.Gender
                    SnomedDataRow("nUserID") = gnLoginID

                    SnomedDataRow("bIsAdvancedProviderReference") = ReferenceMaterialNode.IsProviderAdvanceMaterial
                    dataTableSnomed.Rows.Add(SnomedDataRow)
                    SnomedDataRow = Nothing
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Function InsertIntoDatabase() As Boolean
        Dim sResult As String = String.Empty
        Dim bResult As Boolean = False
        Dim EducationDatabaseLayer As New clsEducationAssociationDatabaseLayer()

        Try
            sResult = EducationDatabaseLayer.InsertIntoDatabase(dataTableDelete, dataTableICD9, dataTableMedication, dataTableLabResults, dataTableSnomed)
            If sResult = "Association Not Saved" Then
                bResult = False
                MsgBox(sResult, MsgBoxStyle.Information)
            Else
                bResult = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            bResult = False
        Finally
            If EducationDatabaseLayer IsNot Nothing Then
                EducationDatabaseLayer.Dispose()
                EducationDatabaseLayer = Nothing
            End If
        End Try

        Return bResult
    End Function

    Private Function GetDataTablesForFillingTreeView(ByVal DockingTags As DockingTags, Optional ByVal nSearchType As Integer = 1, Optional ByVal SearchString As String = "") As DataTable

        Dim clsEducationDBLayer As New clsEducationAssociationDatabaseLayer()
        Dim returnedDataTable As New DataTable

        Try
            Select Case DockingTags
                Case frmEducationAssociation.DockingTags.ICD9Button
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.ICD9, nSearchType, SearchString)
                Case frmEducationAssociation.DockingTags.ICD10Button  ''added for ICD10 implementation
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.ICD10, nSearchType, SearchString)
                Case frmEducationAssociation.DockingTags.LabsButton
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.LOINC, nSearchType)
                Case frmEducationAssociation.DockingTags.MedicationButton
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.NDC, nSearchType)
                Case frmEducationAssociation.DockingTags.SnoMedButton
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.SNOMED)
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If clsEducationDBLayer IsNot Nothing Then
                clsEducationDBLayer.Dispose()
                clsEducationDBLayer = Nothing
            End If

        End Try

        Return returnedDataTable

    End Function


#End Region

#Region "Button Mouse Over events"

    Private Sub btnICD9_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.MouseHover
        btnICD9.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnICD9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.MouseLeave
        If eCurrentSelectedButton = DockingTags.ICD9Button Then
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnLabs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.MouseHover
        btnLabs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLabs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.MouseLeave
        If eCurrentSelectedButton = DockingTags.LabsButton Then
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnMedication_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMedication.MouseHover
        btnMedication.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnMedication.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnMedication_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMedication.MouseLeave
        If eCurrentSelectedButton = DockingTags.MedicationButton Then
            btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnMedication.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnMedication.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnMedication.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnSnoMed_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSnoMed.MouseHover
        btnSnoMed.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnSnoMed.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnSnoMed_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSnoMed.MouseLeave
        If eCurrentSelectedButton = DockingTags.SnoMedButton Then
            btnSnoMed.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnSnoMed.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnSnoMed.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnSnoMed.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btn_AddSnoMed_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AddSnoMed.MouseHover
        If btn_AddSnoMed.Enabled Then
            btn_AddSnoMed.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
            btn_AddSnoMed.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btn_AddSnoMed_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AddSnoMed.MouseLeave
        If btn_AddSnoMed.Enabled Then
            If eCurrentSelectedButton = DockingTags.SnoMedButton Then
                btn_AddSnoMed.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                btn_AddSnoMed.BackgroundImageLayout = ImageLayout.Stretch
            End If
        End If
    End Sub

    Private Sub btnPatientEducation_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.MouseHover
        btnPatientEducation.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnPatientEducation_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.MouseLeave
        If btnPatientEducation.Tag = "Selected" Then
            btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnProviderReference_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProviderReference.MouseHover
        btnProviderReference.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnProviderReference.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnProviderReference_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProviderReference.MouseLeave
        If btnProviderReference.Tag = "Selected" Then
            btnProviderReference.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnProviderReference.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnProviderReference.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnProviderReference.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub



    Private Sub btnICD10_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnICD10.MouseHover  ''added for ICD10 implementation
        btnICD10.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnICD10.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnICD10_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnICD10.MouseLeave  ''added for ICD10 implementation
        If eCurrentSelectedButton = DockingTags.ICD10Button Then
            btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD10.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD10.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub
#End Region

    Private Sub btnICD10_Click(sender As System.Object, e As System.EventArgs) Handles btnICD10.Click   ''added for ICD10 implementation

        Try
            If btnICD10.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.ICD10Button)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
            End If
            eCurrentSelectedButton = DockingTags.ICD10Button

            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            CodesTreeView.ColonAsSeparator = False
            CodesTreeView.txtsearch.Text = String.Empty

            FillCodesTreeView(DockingTags.ICD10Button, GetDataTablesForFillingTreeView(DockingTags.ICD10Button))

            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD10)
            pnlLeftRadioBtnTop.Enabled = True
            rbtAll.Checked = True

            DemographicsLabResults(False)
            pnlCriteriaLab.Visible = False

            If eCurrentReferenceMaterial = EducationTag.Provider Then
                chkAdvancedProviderReference.Enabled = True
            Else
                chkAdvancedProviderReference.Enabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub


  
End Class