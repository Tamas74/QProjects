Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloEMR.gloStream.DiseaseManagement
Imports System.Drawing
Imports gloEMRGeneralLibrary
Imports System.Data.SqlClient
Imports System.Net

Imports gloSettings

Imports System.Xml
Imports System.Xml.Linq


Public Class frmProviderEducation

    Private ProviderId As Long
    Private VisitId As Long
    Private EducationId As Long
    Private eCurrentSelectedButton As DockingTags
    Private dataTableAssociatedCodes As New DataTable

    Private strSelectedCode As String = ""
    Private strSelectedCodeSystem As String = ""
    Private strSelectedCodeDesc As String = ""


    Private strPatientCode As String = ""
    Private strPatientLanguage As String = ""
    Private strPatientGender As String = ""
    Private strPatientAgeUnit As String = ""
    Private strPatientAgeValue As String = ""

    Private strConceptID As String = String.Empty
    Private strConceptID_Old As String = String.Empty
    Private strDescriptionID As String = String.Empty
    Private strSnoMedId As String = String.Empty
    Private strDescription As String = String.Empty

    Dim PatientAge As New gloUserControlLibrary.AgeDetail

    Public DocumentTitle As String = ""
    Public DocumentCompleted As Boolean = False

    Dim BrowserLink As String = ""
    Public HomeUrl As String = ""


    Private Const Col_ProviderEducationId As Integer = 0
    Private Const Col_ProviderId As Integer = 1
    Private Const Col_Provider As Integer = 2
    Private Const Col_VisitId As Integer = 3
    Private Const Col_DocumentTitle As Integer = 4
    Private Const Col_DocumentURl As Integer = 5
    Private Const Col_Document As Integer = 6
    Private Const Col_CreatedDateTime As Integer = 7

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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal _ProviderId As Long, ByVal _VisitId As Long)

        ' This call is required by the designer.
        InitializeComponent()
        ProviderId = _ProviderId
        VisitId = _VisitId

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal _ProviderId As Long, ByVal _VisitId As Long, ByVal _EducationId As Long)

        ' This call is required by the designer.
        InitializeComponent()
        ProviderId = _ProviderId
        VisitId = _VisitId
        EducationId = _EducationId
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub frmProviderEducation_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        btn_Right.BackgroundImage = gloEMR.My.Resources.Rewind
        btn_Right.BackgroundImageLayout = ImageLayout.Center

        Try
            CodesTreeView.ImageList = imgTreeView
            btn_AddSnoMed.Enabled = False

            btnICD9.Tag = "UnSelected"
            btnICD10.Tag = "UnSelected"
            btnSnoMed.Tag = "UnSelected"
            btnLabs.Tag = "UnSelected"
            btnMedication.Tag = "UnSelected"

            'FillProviders()

            If EducationId <> 0 Then
                LoadEducation()
            End If

            If gblnIcd10MasterTransition = True Then 'gblnIcd10Transition
                btnICD10.PerformClick()
            Else
                btnICD9.PerformClick()
            End If

            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New Cls_TabIndexSettings(Me)
            tom.SetTabOrder(scheme)
            tom = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    'Private Sub cmbProvider_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
    '    'If cmbProvider.SelectedIndex > 0 Then
    '    '    ProviderId = Convert.ToInt64(cmbProvider.SelectedValue)
    '    'Else
    '    '    ProviderId = 0
    '    'End If
    '    ''If ProviderId > 0 Then
    '    'pnlProviderData.Visible = True
    '    'Designgrid()
    '    ''Else
    '    ''pnlProviderData.Visible = False
    '    ''End If
    'End Sub

    'Private Sub C1ProviderEducation_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
    '    Dim r As Integer = C1ProviderEducation.HitTest(e.X, e.Y).Row
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        BrowserLink = Convert.ToString(C1ProviderEducation.GetData(r, Col_DocumentURl))
    '        InfoButtonWebBrowser.Navigate(BrowserLink)
    '        Me.Cursor = Cursors.Default
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub btnICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.Click

        Try
            If btnICD9.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.ICD9Button)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
                CodesTreeView.Visible = True
                pnlEncounterSnomed.Visible = False
            End If
            eCurrentSelectedButton = DockingTags.ICD9Button

            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            CodesTreeView.ColonAsSeparator = False
            CodesTreeView.txtsearch.Text = String.Empty

            FillCodesTreeView(DockingTags.ICD9Button, GetDataTablesForFillingTreeView(DockingTags.ICD9Button))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD9)
            'pnlLeftRadioBtnTop.Enabled = True
            'rbtAll.Checked = True


            ts_SmallStrip_btn_Document.Text = btnICD9.Text


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnICD10_Click(sender As System.Object, e As System.EventArgs) Handles btnICD10.Click   ''added for ICD10 implementation

        Try
            If btnICD10.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.ICD10Button)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
                CodesTreeView.Visible = True
                pnlEncounterSnomed.Visible = False
            End If
            eCurrentSelectedButton = DockingTags.ICD10Button

            CodesTreeView.ColonAsSeparator = False
            CodesTreeView.txtsearch.Text = String.Empty

            FillCodesTreeView(DockingTags.ICD10Button, GetDataTablesForFillingTreeView(DockingTags.ICD10Button))

            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.ICD10)
            'pnlLeftRadioBtnTop.Enabled = True
            'rbtAll.Checked = True
            ts_SmallStrip_btn_Document.Text = btnICD10.Text
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnLabs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.Click
        Try
            If btnLabs.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.LabsButton)
                CodesTreeView.SearchBox = True
                btn_AddSnoMed.Enabled = False
                CodesTreeView.Visible = True
                pnlEncounterSnomed.Visible = False
            End If
            eCurrentSelectedButton = DockingTags.LabsButton

            Me.CodesTreeView.ColonAsSeparator = True
            FillCodesTreeView(DockingTags.LabsButton, GetDataTablesForFillingTreeView(DockingTags.LabsButton))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.LOINC)
            'pnlLeftRadioBtnTop.Enabled = True
            'rbtAll.Checked = True
            CodesTreeView.txtsearch.Text = String.Empty
            ts_SmallStrip_btn_Document.Text = btnLabs.Text
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
                CodesTreeView.Visible = True
                pnlEncounterSnomed.Visible = False
            End If
            eCurrentSelectedButton = DockingTags.MedicationButton
         
            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            With CodesTreeView
                .EducationMappingSearchType = 1 'For all codes         
                .ColonAsSeparator = False
            End With

            FillCodesTreeView(DockingTags.MedicationButton, GetDataTablesForFillingTreeView(DockingTags.MedicationButton))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.NDC)
            'pnlLeftRadioBtnTop.Enabled = True
            'rbtAll.Checked = True
            CodesTreeView.txtsearch.Text = String.Empty
            ts_SmallStrip_btn_Document.Text = btnMedication.Text
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btnSnoMed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSnoMed.Click
        Try
            If btnSnoMed.Tag = "UnSelected" Then
                PopulateAssociates(DockingTags.SnoMedButton)
                btn_AddSnoMed.Enabled = False
                CodesTreeView.Visible = False
                pnlEncounterSnomed.Visible = True
                gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                If gstrSMDBConnstr <> String.Empty Then
                    objclsgeneral.IsConnect(gstrSMDBConnstr, GetConnectionString())
                End If
                CodesTreeView.Nodes.Clear()
            End If

            eCurrentSelectedButton = DockingTags.SnoMedButton
      
            'Changed function arguments for enabling Radio Buttons on 4-Aug-2013
            Me.CodesTreeView.ColonAsSeparator = False
            FillCodesTreeView(DockingTags.SnoMedButton, GetDataTablesForFillingTreeView(DockingTags.SnoMedButton))
            ChangeIconIfAssociationIsFound(clsEducationAssociationDatabaseLayer.Codes.SNOMED)
            'pnlLeftRadioBtnTop.Enabled = False
            CodesTreeView.txtsearch.Text = String.Empty
            For Each ElementNode As gloUserControlLibrary.myTreeNode In CodesTreeView.Nodes
                ElementNode.IsSnomedCode = True
            Next
            ts_SmallStrip_btn_Document.Text = btnSnoMed.Text
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub btn_AddSnoMed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddSnoMed.Click

        Try
            AddSnomedNode()
            'txtsearchDrugs.Text = String.Empty
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub CodesTreeView_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles CodesTreeView.NodeMouseDoubleClick
        Dim selNode As gloUserControlLibrary.myTreeNode = CodesTreeView.SelectedNode
        Dim Code As String = ""
        Dim CodeSystem As String = ""
        Dim CodeDisplay As String = ""


        If eCurrentSelectedButton = DockingTags.ICD9Button Then ''or condition added for ICD10 implementation
            Code = selNode.Tag
            CodeSystem = "2.16.840.1.113883.6.103"
            strSelectedCodeSystem = "ICD9"
        ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then
            Code = selNode.Tag
            CodeSystem = "2.16.840.1.113883.6.90"
            strSelectedCodeSystem = "ICD10"
        ElseIf eCurrentSelectedButton = DockingTags.MedicationButton Then
            Code = selNode.NDCCode
            CodeSystem = "2.16.840.1.113883.6.69"
            strSelectedCodeSystem = "NDC"
        ElseIf eCurrentSelectedButton = DockingTags.LabsButton Then
            Code = selNode.Tag
            CodeSystem = "2.16.840.1.113883.6.1"
            strSelectedCodeSystem = "LOINC"
        ElseIf eCurrentSelectedButton = DockingTags.SnoMedButton Then
            Code = selNode.SnomedID
            CodeSystem = "2.16.840.1.113883.6.96"
            strSelectedCodeSystem = "Snomed"
        End If
        CodeDisplay = selNode.Text
        strSelectedCode = Code

        strSelectedCodeDesc = CodeDisplay
        OpenEducationMaterial(Code, CodeSystem, CodeDisplay)

    End Sub

    Private Sub trvLinks_NodeMouseClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvLinks.NodeMouseClick

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim BrowserLink As String = Convert.ToString(e.Node.Tag)
            InfoButtonWebBrowser.Navigate(BrowserLink)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally

        End Try
    End Sub




    Private Sub ChangeIconsAfterSearch() Handles CodesTreeView.SearchFired
        Try
            Dim NSearchType As Integer = 1
            'If rbtAll.Checked = True Then
            '    NSearchType = 1
            'End If
            'If rbtAssociated.Checked = True Then
            '    NSearchType = 2
            'End If
            'If rbtUnassociated.Checked = True Then
            '    NSearchType = 3
            'End If
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

    Private Sub AddSnomedNode()

        Dim frmSnomedBrowser As gloSnoMed.FrmSelectProblem = Nothing
        Me.Cursor = Cursors.WaitCursor
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

            
                Dim Code As Long = 0
                Dim CodeSystem As String = ""

                Dim BrowserLink As String = ""

                Dim strLang As String = ""
                'Dim AgeinYears As Decimal
                Dim sAgeUnit As String = ""
                Dim sAgeValue As String = ""
                Dim Gender As String = ""

                Code = strConceptID
                CodeSystem = "2.16.840.1.113883.6.96"


                strSelectedCode = Code
                strSelectedCodeSystem = "Snomed"
                strSelectedCodeDesc = strDescription



                OpenEducationMaterial(Code, CodeSystem, strDescription)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If frmSnomedBrowser IsNot Nothing Then
                frmSnomedBrowser.Dispose()
                frmSnomedBrowser = Nothing
            End If
            Me.Cursor = Cursors.Default
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

    Private Function GetDataTablesForFillingTreeView(ByVal DockingTags_Call As DockingTags, Optional ByVal nSearchType As Integer = 1, Optional ByVal SearchString As String = "") As DataTable

        Dim clsEducationDBLayer As New clsEducationAssociationDatabaseLayer()
        Dim returnedDataTable As New DataTable

        Try
            Select Case DockingTags_Call
                Case DockingTags.ICD9Button
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.ICD9, nSearchType, SearchString)
                Case DockingTags.ICD10Button  ''added for ICD10 implementation
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.ICD10, nSearchType, SearchString)
                Case DockingTags.LabsButton
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.LOINC, nSearchType)
                Case DockingTags.MedicationButton
                    returnedDataTable = clsEducationDBLayer.GetCodes(clsEducationAssociationDatabaseLayer.Codes.NDC, nSearchType)
                Case DockingTags.SnoMedButton
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

    Private Sub ButtonHover(sender As Object, e As System.EventArgs) Handles btnICD9.MouseHover, btnICD10.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongYellow
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub ButtonMouseLeave(sender As Object, e As System.EventArgs) Handles btnICD9.MouseLeave, btnICD10.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongButton
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

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
                Case DockingTags.LeftPanel
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
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub


#Region "Encounter Snomed Selector"
    Private objclsgeneral As gloSnoMed.ClsGeneral = New gloSnoMed.ClsGeneral()
    Private _isFormLoading As Boolean = False
    Private _SearchBy As String = "ConceptID"
    Public strProblem As String = ""
    Public strSelectedConceptID As String = ""
    Public strSelectedDescriptionID As String = ""
    Public strSelectedDescription As String = ""
    Public strConceptDesc As String = ""
    Public strCodeSystem As String = "SNOMED"
    Dim _CurrentTime As System.DateTime
    Private Sub FillDefination()
        Dim oNode As TreeNode = Nothing
        Dim dsSnomed As New DataSet()
        'trvSubtype.Nodes.Clear()
        'trvSnoMed.Nodes.Clear()

        oNode = trvFindings.SelectedNode
        If (oNode IsNot Nothing) Then
            If (oNode.Tag IsNot Nothing) Then
                strConceptID = oNode.Tag.ToString()
                Dim ICD9Description As String = ""

                If String.IsNullOrEmpty(oNode.Name) Then
                    oNode.Name = oNode.Text
                End If
                ICD9Description = oNode.Name.Trim()
                oNode = Nothing
                dsSnomed = objclsgeneral.Fill_SnomedDetails(strConceptID, "False", ICD9Description, True)

                If dsSnomed IsNot Nothing Then
                    dsSnomed.Dispose()
                    dsSnomed = Nothing

                End If
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Cursor = Cursors.WaitCursor

        Try

            If Not String.IsNullOrEmpty(Me.Text.Trim()) Then
                If DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100 Then
                    Timer1.[Stop]()


                    mnuFindings_Click(Nothing, Nothing)
                    'If pnlmiddle.Visible Then
                    '    FillDefination()

                    'End If
                    FillDefination()
                End If
            Else
                Timer1.[Stop]()

                mnuFindings_Click(Nothing, Nothing)
                'If pnlmiddle.Visible Then
                '    FillDefination()

                'End If
                FillDefination()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub trvFindings_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvFindings.AfterSelect
        If Timer1.Enabled = False Then
            Timer1.[Stop]()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub trvFindings_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles trvFindings.BeforeSelect
        _CurrentTime = DateTime.Now
        Timer1.[Stop]()
        Timer1.Interval = 700
        Timer1.Enabled = True
    End Sub

    Private Sub trvFindings_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles trvFindings.BeforeExpand
        Try
            Me.Cursor = Cursors.WaitCursor

            If (e.Node Is Nothing) = False Then

                ' TreeNode eNode = new TreeNode();
                Dim eNode As TreeNode = e.Node
                Dim dsTreeview As DataSet = Nothing
                ' new DataSet();
                If (eNode IsNot Nothing) Then
                    If (e.Node.Parent Is Nothing) = False Then
                        If e.Node.Nodes(0).Tag.ToString() = "TempNode999*" Then
                            trvFindings.Nodes.Remove(e.Node.Nodes(0))
                            If chkCOREProblem.Checked = False Then

                                dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), False)
                                objclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode)

                            End If
                        End If
                    ElseIf e.Node.Nodes(0).Tag.ToString() = "TempNode9999*" Then

                        dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Tag.ToString(), True)
                        objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview, chkCOREProblem.Checked)
                        If eNode.Tag IsNot Nothing Then
                            strProblem = eNode.Name
                            strSelectedConceptID = eNode.Tag.ToString()

                            If (dsTreeview Is Nothing) = False Then
                                If dsTreeview.Tables("Parent").Rows.Count > 0 Then
                                    'strDescriptionID = dsTreeview.Tables("Parent").Rows(0)("DESCRIPTIONID").ToString()
                                    'StrSnoMedID = dsTreeview.Tables("Parent").Rows(0)("SNOMEDID").ToString()
                                End If
                                'Not USED ( variable not assigned)
                                'If dsTreeview.Tables("IsDefinition").Rows.Count > 0 Then
                                '    strSelectedDescription = dsTreeview.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME").ToString()
                                'End If
                            End If

                        End If

                    ElseIf e.Node.Nodes(0).Tag.ToString() = "ICDTempNode9999*" Then




                    End If
                End If


                If dsTreeview IsNot Nothing Then
                    dsTreeview.Dispose()
                    dsTreeview = Nothing
                End If

                If (eNode IsNot Nothing) Then
                    eNode = Nothing
                End If
            End If
        Catch
            ' (Exception ex)
        Finally
            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub trvFindings_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFindings.NodeMouseDoubleClick
        Try
            Dim Code As String = 0
            Dim CodeSystem As String = ""
            Code = Convert.ToString(e.Node.Tag)
            CodeSystem = "2.16.840.1.113883.6.96"

            strSelectedCode = Code
            strSelectedCodeSystem = "Snomed"
            strSelectedCodeDesc = e.Node.Text
            OpenEducationMaterial(Code, CodeSystem, e.Node.Text)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtSMSearch_SearchFired() Handles txtSMSearch.SearchFired
        Dim _Term As String = Nothing

        Try

            If txtSMSearch.Text.Length > 1 Then
                trvFindings.BeginUpdate()
                Me.Cursor = Cursors.WaitCursor
                _Term = ""
                If chkCOREProblem.Checked Then


                    objclsgeneral.SearchCORESnomed(txtSMSearch.Text.Trim(), trvFindings, _SearchBy)
                Else


                    objclsgeneral.SearchSnomed(txtSMSearch.Text.Trim(), False, trvFindings, _SearchBy)
                End If

                _Term = strConceptDesc

                If chkCOREProblem.Checked Then

                    If trvFindings.Nodes.Count > 0 Then

                        If trvFindings.Nodes(0).Nodes.Count > 0 Then
                            If trvFindings.Nodes.Count = 1 Then
                                Dim oNode__1 As TreeNode = trvFindings.Nodes(0)
                                trvFindings.SelectedNode = oNode__1
                                trvFindings.Nodes(0).ExpandAll()
                                For Each onode__2 As TreeNode In trvFindings.Nodes(0).Nodes
                                    If (onode__2 Is Nothing) = False Then

                                        If Convert.ToString(onode__2.Tag) = strConceptID Then
                                            If Convert.ToString(onode__2.Tag) = strConceptID And onode__2.Text.Trim() = _Term.Trim() Then
                                                trvFindings.SelectedNode = onode__2
                                                ' TODO: might not be correct. Was : Exit For
                                                Exit For
                                            End If
                                        End If

                                    End If
                                Next

                            End If

                        End If
                    End If
                Else

                    For Each onode__2 As TreeNode In trvFindings.Nodes
                        If (onode__2 Is Nothing) = False Then
                            If Convert.ToString(onode__2.Tag) = strConceptID Then
                                If Convert.ToString(onode__2.Tag) = strConceptID And onode__2.Name.Trim() = _Term.Trim() Then
                                    trvFindings.SelectedNode = onode__2
                                    ' TODO: might not be correct. Was : Exit For
                                    Exit For
                                End If
                            End If

                        End If
                    Next
                End If
                ' }
                Me.Cursor = Cursors.[Default]


                trvFindings.EndUpdate()
            Else
                trvFindings.Nodes.Clear()

            End If
        Catch

        Finally

            Me.Cursor = Cursors.[Default]
        End Try
    End Sub

#Region "CORE Problem"
    Private Sub chkCOREProblem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCOREProblem.CheckedChanged

        If _isFormLoading = False Then
            ClearData()

            txtSMSearch_SearchFired()
        End If

    End Sub
#End Region

    Private Sub ClearData()
        trvFindings.Nodes.Clear()
        txtSMSearch.Clear()
    End Sub

    Private Sub mnuFindings_Click(sender As Object, e As EventArgs)
        Dim oNode As TreeNode = Nothing
        Try

            strSelectedDescriptionID = ""
            oNode = trvFindings.SelectedNode
            If (oNode IsNot Nothing) Then
                If (oNode.Tag IsNot Nothing) Then
                    strConceptID = oNode.Tag.ToString()
                    Dim ICD9Description As String = ""

                    If String.IsNullOrEmpty(oNode.Name) Then
                        oNode.Name = oNode.Text
                    End If
                    ICD9Description = oNode.Name.Trim()
                    If chkCOREProblem.Checked Then
                        Dim dsICD As DataSet = Nothing
                        If oNode.Parent IsNot Nothing Then
                            Dim ICDCode As [String] = objclsgeneral.Fill_ICD9(oNode.Parent.Text)
                        Else

                            dsICD = objclsgeneral.GetCOREICDData(strConceptID, "")
                        End If

                    Else
                        Dim dsSnomed As DataSet = objclsgeneral.Fill_SnomedDetails(strConceptID, "False", ICD9Description, False)
                    End If

                    strSelectedConceptID = strConceptID




                    strProblem = oNode.Name
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If oNode IsNot Nothing Then
                oNode = Nothing
            End If
        End Try

    End Sub


    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        ClearData()
    End Sub
#End Region

#Region "Menu Events"

    Private Sub ts_Previous_Click(sender As System.Object, e As System.EventArgs) Handles ts_Previous.Click
        InfoButtonWebBrowser.GoBack()
    End Sub

    Private Sub ts_Next_Click(sender As System.Object, e As System.EventArgs) Handles ts_Next.Click
        InfoButtonWebBrowser.GoForward()
    End Sub

    Private Sub InfoButtonWebBrowser_Navigated(sender As System.Object, e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles InfoButtonWebBrowser.Navigated
        BrowserLink = InfoButtonWebBrowser.Url.ToString
    End Sub

    Private Sub InfoButtonWebBrowser_NewWindow(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles InfoButtonWebBrowser.NewWindow
        e.Cancel = True
        Me.Cursor = Cursors.WaitCursor
        BrowserLink = InfoButtonWebBrowser.StatusText
        InfoButtonWebBrowser.Navigate(BrowserLink)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ts_Refresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_Refresh.Click
        Me.Cursor = Cursors.WaitCursor
        InfoButtonWebBrowser.Navigate(BrowserLink)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ts_Home_Click(sender As System.Object, e As System.EventArgs) Handles ts_Home.Click
        Me.Cursor = Cursors.WaitCursor
        InfoButtonWebBrowser.Navigate(HomeUrl)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ts_Save_Click(sender As System.Object, e As System.EventArgs) Handles ts_Save.Click

        'Dim temFile As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
        'saveWebContentToFile(temFile)
        'Dim _speNotes As Object = CType(gloWord.LoadAndCloseWord.ConvertFiletoBinary(temFile), Object)

        If SaveProviderEducation(GetVisitID(System.DateTime.Now), strSelectedCode, strSelectedCodeSystem, strSelectedCodeDesc, DocumentTitle, BrowserLink, ProviderId) Then
            MessageBox.Show("Provider Education document saved successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If

    End Sub

    Private Sub ts_Close_Click(sender As System.Object, e As System.EventArgs) Handles ts_Close.Click
        Me.Close()
    End Sub

    Public Function SaveProviderEducation(ByVal nVisitID As Long, ByVal SelectedCode As String, ByVal SelectedCodeSystem As String, ByVal SelectedCodeDesc As String, ByVal sTitle As String, ByVal DocumentURL As String, Optional ProviderID As Long = 0) As Boolean

        Dim ProviderEducationID As Long = 0
        Dim bInsertResult As Boolean = False
        Dim sqlQueryName As String = String.Empty
        Dim sqlQueryResult As String = ""
        Dim dbLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim dbParams As gloDatabaseLayer.DBParameters = Nothing
        Dim innerEx As Exception = Nothing

        Try
            sqlQueryName = "gsp_InUpProviderEducation"
            dbLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())
            dbLayer.Connect(False)
            dbParams = New gloDatabaseLayer.DBParameters()
            dbParams.Add("@ProviderEducationID", ProviderEducationID, ParameterDirection.InputOutput, SqlDbType.BigInt)
            dbParams.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            dbParams.Add("@VisitID", nVisitID, ParameterDirection.Input, SqlDbType.BigInt)

            dbParams.Add("@Code", SelectedCode, ParameterDirection.Input, SqlDbType.VarChar)
            dbParams.Add("@CodeSystem", SelectedCodeSystem, ParameterDirection.Input, SqlDbType.VarChar)
            dbParams.Add("@CodeDescription", SelectedCodeDesc, ParameterDirection.Input, SqlDbType.VarChar)

            dbParams.Add("@DocumentTitle", sTitle, ParameterDirection.Input, SqlDbType.VarChar)
            dbParams.Add("@DocumentURL", DocumentURL, ParameterDirection.Input, SqlDbType.VarChar)
            'dbParams.Add("@Document", If((oResult Is Nothing), DirectCast(DBNull.Value, Object), oResult), ParameterDirection.Input, SqlDbType.VarBinary)
            dbLayer.Execute(sqlQueryName, dbParams)

            dbLayer.Disconnect()
            If (IsNothing(dbParams) = False) Then
                ProviderEducationID = Convert.ToInt64(dbParams.Item("@ProviderEducationID").Value)
            End If

            If Convert.ToString(sqlQueryResult).Trim().Length = 0 Then
                bInsertResult = True
            Else
                bInsertResult = False
                innerEx = New Exception("Error while inserting patient photo.::" + Convert.ToString(sqlQueryResult))
                innerEx.Source = [String].Format("Class: {0}, Method: {1}, Stored Procedure: {2}.", "PatientPhoto", "Insert", sqlQueryName)
                gloAuditTrail.gloAuditTrail.ExceptionLog(innerEx, False)
            End If

        Catch ex As SqlException
            Return False
        Catch ex As Exception
            Return False
        Finally

        End Try
        Return bInsertResult


    End Function

    Private Sub saveWebContentToFile(fileName As String)
        Dim tmpobjWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim tmpwd As Microsoft.Office.Interop.Word.Document
        tmpwd = tmpobjWord.WebToDocx(InfoButtonWebBrowser, fileName)
        tmpobjWord.CloseWordApplication(tmpwd)
        tmpobjWord = Nothing
    End Sub

#End Region


    Private Sub navigation_complete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        DocumentCompleted = True

        If Not IsNothing(InfoButtonWebBrowser.Document) Then
            DocumentTitle = Convert.ToString(InfoButtonWebBrowser.Document.Title)
        End If

        If IsNothing(DocumentTitle) OrElse DocumentTitle = "" Then
            DocumentTitle = Convert.ToString(InfoButtonWebBrowser.DocumentTitle)
        End If
        If DocumentTitle = "Health Information for You: MedlinePlus Connect" Or DocumentTitle = "Información de salud para usted: MedlinePlus Connect" Then
            Try
                'Code To fetch H2 tag from html Source Code
                For Each element As HtmlElement In InfoButtonWebBrowser.Document.All
                    Dim HeaderElement() As String
                    If element.TagName().ToString.ToUpper() = "H2" Then
                        HeaderElement = element.InnerText.ToString().Split("[")
                        If HeaderElement.Length > 0 Then
                            DocumentTitle = HeaderElement(0)
                        End If
                    End If
                    HeaderElement = Nothing
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        'If justToSaveLink Then
        '    saveWebContentToFile(PathToSaveLink)
        '    justToSaveLink = False
        '    Me.Close()
        'End If

    End Sub


    Private Function GetURL(ByVal sGender As String, ByVal sAge As String, ByVal sAgeUnit As String, ByVal Code As String, ByVal CodeSystem As String, ByVal strLang As String)
        Dim sURL As String = ""
        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='INFOBUTTON_URL'"
        Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As New SqlCommand(strSql, con)
        Dim dbURL As String = ""

        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dtSetting As New DataTable
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                dbURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            sURL = dbURL + "patientPerson.administrativeGenderCode.c=" + sGender + "&" +
                                "age.v.v=" + sAge + "&age.v.u=" + sAgeUnit + "&" + "mainSearchCriteria.v.c=" + Code + "&mainSearchCriteria.v.cs=" + CodeSystem + "&" +
                                 "performer.languageCode.c=en&informationRecipient.languageCode.c=" + strLang + "&knowledgeResponseType=text/XML"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Openinfosource :: " & ex.ToString(), False)
        Finally
            If Not IsNothing(con) Then  ''connection state close
                If (con.State = ConnectionState.Open) Then
                    con.Close()
                End If
                con.Dispose()
            End If
        End Try

        Return sURL
    End Function

    Public Function Geturl(ByVal strUrl As String, ByVal _PatientLanguage As String) As String

        Dim oclient As New WebClient
        Dim urlstring As String = oclient.DownloadString(strUrl)
        oclient.Dispose()
        oclient = Nothing
        Dim FinalUrl As String = strUrl
        Dim informationStringen As String = ""
        Dim informationStringSP As String = ""
        Dim NotHavingStringen As String = ""
        Dim NotHavingStringsp As String = ""
        Dim FootterString As String = ""
        Dim tochkTitle As String = ""
        Dim informationString As String = ""
        Dim NotHavingString As String = ""

        'below Added for Bug #93626: 00001080: Problem List info button
        ' and new settings value inserted in database setting table
        Dim infoLogoStringEN As String = ""
        Dim infoSingleResultStringEN As String = ""
        Dim infoMultiResultStringEN As String = ""
        Dim infoLogoStringSP As String = ""
        Dim infoSingleResultStringSP As String = ""
        Dim infoMultiResultStringSP As String = ""
        Try
            Dim dtSettings As DataTable = GetInfobuttonSettings()
            If Not IsNothing(dtSettings) Then
                If dtSettings.Rows.Count > 0 Then
                    For i As Integer = 0 To dtSettings.Rows.Count - 1 Step 1
                        Select Case Convert.ToString(dtSettings.Rows(i)("sSettingsName"))
                            Case "Infobutton_Information_StringEN"
                                informationStringen = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_Information_StringSP"
                                informationStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_NotHaving_StringEN"
                                NotHavingStringen = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_NotHaving_StringSP"
                                NotHavingStringsp = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_FooterString"
                                FootterString = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "Infobutton_tochkTitle"
                                tochkTitle = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))

                                'below Added for Bug #93626: 00001080: Problem List info button
                            Case "INFOBUTTON_LOGOTEXT_EN"
                                infoLogoStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_LOGOTEXT_SP"
                                infoLogoStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultSingle_EN"
                                infoSingleResultStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultSingle_SP"
                                infoSingleResultStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultMulti_EN"
                                infoMultiResultStringEN = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                            Case "INFOBUTTON_ResultMulti_SP"
                                infoMultiResultStringSP = Convert.ToString(dtSettings.Rows(i)("sSettingsValue"))
                        End Select
                    Next
                End If
                dtSettings.Dispose()
                dtSettings = Nothing
            End If

            ' Added by hemant for Bug #93626: 00001080: Problem List info button
            Dim startSearchString As String = ""
            Dim endSearchString As String = ""
            If _PatientLanguage = "sp" Then
                informationString = informationStringSP
                NotHavingString = NotHavingStringsp
                startSearchString = infoLogoStringSP
                endSearchString = infoMultiResultStringSP
            Else
                informationString = informationStringen
                NotHavingString = NotHavingStringen
                startSearchString = infoLogoStringEN
                endSearchString = infoMultiResultStringEN
            End If

            'if setting does not exist then continue to execute as per old implementation
            If Not (String.IsNullOrEmpty(startSearchString) Or String.IsNullOrEmpty(endSearchString)) Then
                Dim startSearchIndex As Integer = urlstring.IndexOf(startSearchString)
                If startSearchIndex <> -1 Then
                    Dim startSearchIndexFinal As Integer = startSearchIndex + startSearchString.Length
                    ' if single results found singular and plural for end serach string
                    Dim endSearchStringIndex As Integer = urlstring.IndexOf(endSearchString)
                    If endSearchStringIndex = -1 Then
                        If _PatientLanguage = "en" Then
                            endSearchString = infoSingleResultStringEN
                        ElseIf _PatientLanguage = "sp" Then
                            endSearchString = infoSingleResultStringSP
                        End If
                        endSearchStringIndex = urlstring.IndexOf(endSearchString)
                    End If
                    If endSearchStringIndex <> -1 And endSearchStringIndex > startSearchIndexFinal Then
                        Dim strBetween As String = urlstring.Substring(startSearchIndexFinal, endSearchStringIndex - startSearchIndexFinal + endSearchString.Length)
                        Dim strFinalBetween As String = strBetween.Substring(strBetween.LastIndexOf(">") + 1)

                        If Not String.IsNullOrEmpty(strFinalBetween) Then
                            Dim strArr() As String
                            strArr = strFinalBetween.Trim().Split()
                            Dim cnt As Integer
                            If strArr.Length >= 3 Then
                                If Integer.TryParse(strArr(0), cnt) Then
                                    If cnt = 0 Or cnt > 1 Then
                                        Return FinalUrl
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            '   Added by hemant for Bug #93626: 00001080: Problem List info button Ends here 
            Dim myInformationValue As Integer = urlstring.IndexOf(informationString)
            Dim mySubstring As String = urlstring
            If (myInformationValue = -1) Then
                If (urlstring.IndexOf(NotHavingString) > -1) Then
                    Return FinalUrl
                End If
            End If
            If (myInformationValue > -1) Then
                mySubstring = urlstring.Substring(myInformationValue + Len(informationString))
            End If
            '  Dim myArticleValue As Integer = mySubstring.IndexOf(toFind)
            Dim myTitleValue As Integer = mySubstring.IndexOf(tochkTitle)
            '  Dim myFooterValue As Integer = mySubstring.IndexOf(FootterString)

            If myTitleValue > 0 Then
                Dim resultstr As String = mySubstring.Substring(myTitleValue)

                Dim myhtm As Integer = resultstr.IndexOf(".htm")
                Dim myLess As Integer = resultstr.IndexOf("<")
                Do
                    If myLess < myhtm Then
                        mySubstring = resultstr.Substring(Len(tochkTitle))
                        myTitleValue = mySubstring.IndexOf(tochkTitle)
                        resultstr = mySubstring.Substring(myTitleValue)
                        myhtm = resultstr.IndexOf(".htm")
                        myLess = resultstr.IndexOf("<")
                    Else
                        Exit Do
                    End If
                Loop

                myhtm = resultstr.IndexOf(".htm""")
                Dim totalLength As Integer = 0
                If myhtm = -1 Or myhtm > 200 Then
                    myhtm = resultstr.IndexOf(".html")
                    totalLength = 5
                Else

                    totalLength = 4
                End If
                If myhtm <> -1 Then
                    FinalUrl = resultstr.Substring(0, myhtm + totalLength)
                End If
            Else
                Return FinalUrl
            End If
          
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally

        End Try

        Return FinalUrl
    End Function

    Private Function GetInfobuttonSettings() As DataTable
        Dim Con As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dtBibliographicinfo As New DataTable()
        Try
            cmd = New SqlCommand("SELECT nSettingsID,sSettingsName,sSettingsValue  FROM Settings WHERE sSettingsName like '%Infobutton%'", Con)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtBibliographicinfo)
            Return dtBibliographicinfo
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function


    Private Sub OpenEducationMaterial(ByVal Code As String, ByVal CodeSystem As String, ByVal CodeDisplay As String)

        Dim Audience As String = "PROV"
        Dim TaskContext As String = ""
        Dim dtEducation As DataTable

        Dim IncludeGender As Boolean
        Dim IncludeAge As Boolean
        Dim AgeUnit As String = ""
        Dim AgeValue As String = "0"
        Dim Language As String = ""
        Dim CodeDescription As String = ""
        Dim ProviderId As Long = 0
        Dim PatientId As Long = 0
        Dim VisitId As Long=0
        Dim AgeGroup As String = ""
        Dim RxNormResponse As gloGlobal.DIB.RxnormFlagInfo = Nothing
        Dim Source As Integer



        Dim Gender As String = cmbGender.Text
        Dim ParameterString As String = ""
        Dim strNDCCode As String = ""

        Me.Cursor = Cursors.WaitCursor

        InfoButtonWebBrowser.Navigate("about:blank")

        PatientAge.Years = Convert.ToInt32(txtAgeYears.Text)
        PatientAge.Months = Convert.ToInt32(txtAgeMonths.Text)
        PatientAge.Days = Convert.ToInt32(txtAgeDays.Text)

        Get_PatientDetails(PatientId)

        If Language = "" Then
            Language = strPatientLanguage
        End If

        If PatientAge.Years <> 0 Then
            AgeUnit = "a"
            AgeValue = PatientAge.Years
        ElseIf PatientAge.Months <> 0 Then
            AgeUnit = "mo"
            AgeValue = PatientAge.Months
        ElseIf PatientAge.Days <> 0 Then
            AgeUnit = "d"
            AgeValue = PatientAge.Days
        End If

        'If IncludeAge = True Then
        If AgeValue = "" Or AgeUnit = "" Then
            AgeValue = strPatientAgeValue
            AgeUnit = strPatientAgeUnit
        End If
        If AgeValue <> "" Then
            AgeGroup = GetAgeGroup(Convert.ToInt32(AgeValue), AgeUnit)
        End If


        'If Audience = "Provider" Then
        '    Audience = "PROV"
        'Else
        '    Audience = "PAT"
        'End If
        Audience = "PROV"
        If Gender.ToUpper() = "Male".ToUpper() Then
            Gender = "M"
        ElseIf Gender.ToUpper() = "Female".ToUpper() Then
            Gender = "F"
        ElseIf Gender.ToUpper() = "Other".ToUpper() Then
            Gender = "UN"
        ElseIf Gender.ToUpper() = "Unknown".ToUpper() Then
            Gender = "UNK"
        End If


        If Language = "English" Then
            Language = "en"
        ElseIf Language = "Spanish" Or Language = "Spanish; Castilian" Then
            Language = "sp"
        Else
            Language = "en"
        End If

        If CodeSystem = "2.16.840.1.113883.6.96" Or
           CodeSystem = "2.16.840.1.113883.6.90" Or
           CodeSystem = "2.16.840.1.113883.6.103" Then       'SnomedCT/ICD10/ICD9
            TaskContext = "PROBLISTREV"
            Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
        ElseIf CodeSystem = "2.16.840.1.113883.6.69" Or
               CodeSystem = "2.16.840.1.113883.6.88" Then   'NDC/RxNorm
            TaskContext = "MLREV"
            Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
        ElseIf CodeSystem = "2.16.840.1.113883.6.1" Then    'Loinc
            TaskContext = "LABRREV"
            Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
        End If

        ParameterString = "taskContext.c.c=" + TaskContext +
                                      "&mainSearchCriteria.v.c=" + Code +
                                      "&mainSearchCriteria.v.cs=" + CodeSystem + ""

        If CodeDescription <> "" Then
            ParameterString = ParameterString + "&mainSearchCriteria.v.dn=" + CodeDescription + ""
        End If

        If Gender <> "" Then
            ParameterString = ParameterString + "&patientPerson.administrativeGenderCode.c=" + Gender + "&patientPerson.administrativeGenderCode.cs=2.16.840.1.113883.5.1"
        End If

        If AgeValue <> "" And AgeUnit <> "" Then
            ParameterString = ParameterString + "&age.v.v=" + AgeValue +
                                                "&age.v.u=" + AgeUnit + ""
            If AgeGroup <> "" Then
                ParameterString = ParameterString + "&ageGroup.v.c=" + AgeGroup + "&ageGroup.v.cs=2.16.840.1.113883.6.177"
            End If
        End If

        ParameterString = ParameterString + "&informationRecipient.languageCode.c=" + Language + ""
        ParameterString = ParameterString + "&informationRecipient=" + Audience + ""
        ParameterString = ParameterString + "&performer=PROV"
        ParameterString = ParameterString + "&knowledgeResponseType=text/xml"

        dtEducation = OpenInfobuttonLinks(ParameterString)

        'If No Response for NDC Code then Check for RXNORM Code
        If IsNothing(dtEducation) AndAlso CodeSystem = "2.16.840.1.113883.6.69" Then
            Try
                CodeSystem = "2.16.840.1.113883.6.88"
              
                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    RxNormResponse = oGSHelper.GetRxNormCode(Code)
                    If RxNormResponse IsNot Nothing Then
                        Code = RxNormResponse.Code
                        RxNormResponse = Nothing
                    End If
                End Using

                strSelectedCode = Code
                strSelectedCodeSystem = "RxNorm"

                ParameterString = "taskContext.c.c=" + TaskContext +
                                  "&mainSearchCriteria.v.c=" + Code +
                                  "&mainSearchCriteria.v.cs=" + CodeSystem +
                                  "&mainSearchCriteria.v.dn=" + CodeDescription + ""
                If IncludeGender Then
                    ParameterString = ParameterString + "&patientPerson.administrativeGenderCode.c=" + Gender + "&patientPerson.administrativeGenderCode.cs=2.16.840.1.113883.5.1"
                End If
                If IncludeAge Then
                    ParameterString = ParameterString + "&age.v.v=" + AgeValue +
                                                        "&age.v.u=" + AgeUnit +
                                                        "&ageGroup.v.c=" + AgeGroup + "&ageGroup.v.cs=2.16.840.1.113883.6.177"
                End If
                ParameterString = ParameterString + "&informationRecipient.languageCode.c=" + Language + ""
                ParameterString = ParameterString + "&informationRecipient=" + Audience + ""
                ParameterString = ParameterString + "&performer=PROV"
                ParameterString = ParameterString + "&knowledgeResponseType=text/xml"
                dtEducation = OpenInfobuttonLinks(ParameterString)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            End Try

        End If
        ''



        If IsNothing(dtEducation) Then
            dtEducation = New DataTable()
            Dim colSource As New DataColumn("Source")
            Dim colTitle As New DataColumn("Title")
            Dim colLink As New DataColumn("Link")
            dtEducation.Columns.Add(colSource)
            dtEducation.Columns.Add(colTitle)
            dtEducation.Columns.Add(colLink)

            Dim MedlineURL As String = GetURL(Gender, AgeValue, AgeUnit, Code, CodeSystem, Language)
            Dim NewMedlineURL As String = GetURL(MedlineURL, Language)
            Dim dRow As DataRow = dtEducation.NewRow()
            dRow("Source") = "Medline Plus"
            dRow("Title") = CodeDescription
            dRow("Link") = NewMedlineURL
            dtEducation.Rows.Add(dRow)
            MedlineURL = String.Empty
            NewMedlineURL = String.Empty
            dRow = Nothing
            colSource = Nothing
            colTitle = Nothing
            colLink = Nothing
        ElseIf dtEducation.Rows.Count <= 0 Then
            Dim MedlineURL As String = GetURL(Gender, AgeValue, AgeUnit, Code, CodeSystem, Language)
            Dim NewMedlineURL As String = GetURL(MedlineURL, Language)
            Dim dRow As DataRow = dtEducation.NewRow()
            dRow("Source") = "Medline Plus"
            dRow("Title") = CodeDescription
            dRow("Link") = NewMedlineURL
            dtEducation.Rows.Add(dRow)
            MedlineURL = String.Empty
            NewMedlineURL = String.Empty
            dRow = Nothing
        End If

        If Not IsNothing(dtEducation) Then
            If dtEducation.Rows.Count > 0 Then
                pnlInfobuttonresurces.Visible = True
                trvLinks.Nodes.Clear()
                trvLinks.ImageKey = Nothing
                trvLinks.SelectedImageIndex = -1
                Dim node As TreeNode
                Dim subNode As TreeNode
                For Each row As DataRow In dtEducation.Rows
                    'search in the treeview if any country is already present
                    node = Searchnode(row.Item(0).ToString(), trvLinks)
                    If node IsNot Nothing Then
                        subNode = New TreeNode(row.Item(1).ToString())
                        subNode.Tag = Convert.ToString(row.Item(2))
                        subNode.ForeColor = Color.Blue
                        node.Nodes.Add(subNode)
                    Else
                        node = New TreeNode(row.Item(0).ToString())
                        subNode = New TreeNode(row.Item(1).ToString())
                        subNode.Tag = Convert.ToString(row.Item(2))
                        subNode.ForeColor = Color.Blue
                        node.Nodes.Add(subNode)
                        trvLinks.Nodes.Add(node)
                    End If
                Next
                trvLinks.ExpandAll()
                If dtEducation.Rows.Count = 1 Then
                    BrowserLink = Convert.ToString(dtEducation.Rows(0)("Link"))
                    InfoButtonWebBrowser.Navigate(BrowserLink)
                End If
            Else
                pnlInfobuttonresurces.Visible = False
            End If
        Else
            pnlInfobuttonresurces.Visible = False
        End If


        Try
            AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Public Function OpenInfobuttonLinks(ByVal ParameterString As String) As DataTable
        Dim arrLinks As New ArrayList()
        Dim serviceURL As String = ""
        Dim blnUseDefaultPrinter As Boolean
        Dim sURL As String = ""
        Dim Newurl As String = ""
        Dim strPath As String = ""


        Dim strSql As String = "select sSettingsValue from Settings where sSettingsName='OPENINFOBUTTON_URL'"
        Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim cmd As New SqlCommand(strSql, con)
        Dim da As New SqlDataAdapter(cmd)
        Dim dtSetting As New DataTable
        Dim xDoc As New XmlDocument()


        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
                If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                    blnUseDefaultPrinter = True
                Else
                    blnUseDefaultPrinter = False
                End If
            Else
                blnUseDefaultPrinter = True
            End If
            gloRegistrySetting.CloseRegistryKey()

            con.Open()
            da.Fill(dtSetting)
            da.Dispose()
            da = Nothing

            If Not IsNothing(dtSetting) Then
                serviceURL = Convert.ToString(dtSetting.Rows(0)("sSettingsValue"))
                dtSetting.Dispose()
                dtSetting = Nothing
            End If

            Newurl = serviceURL + ParameterString

            'CoreUrl = Newurl

            Dim oclient As New WebClient
            xDoc.LoadXml(oclient.DownloadString(Newurl))
            Dim dtEducation As New DataTable
            Dim colSource As New DataColumn("Source")
            Dim colTitle As New DataColumn("Title")
            Dim colLink As New DataColumn("Link")

            dtEducation.Columns.Add(colSource)
            dtEducation.Columns.Add(colTitle)
            dtEducation.Columns.Add(colLink)


            Dim xnl As XmlNodeList = xDoc.GetElementsByTagName("entry")
            Dim xnlFeed As XmlNodeList = xDoc.GetElementsByTagName("feed")
            Dim strSource As String = ""
            Dim strTitle As String = ""
            Dim strSubTitle As String = ""
            Dim strLink As String = ""

            For Each node As XmlNode In xnlFeed
                For Each childNode As XmlNode In node.ChildNodes
                    If childNode.Name = "title" Then
                        strSource = childNode.InnerText
                    ElseIf childNode.Name = "entry" Then
                        xnl = childNode.ChildNodes
                        For Each ccNode As XmlNode In xnl
                            If ccNode.Name = "link" Then
                                strLink = ccNode.Attributes("href").Value

                                Dim dRow As DataRow = dtEducation.NewRow()
                                dRow("Source") = strSource
                                dRow("Title") = strTitle
                                'dRow("SubTitle") = strSubTitle
                                dRow("Link") = strLink
                                dtEducation.Rows.Add(dRow)

                            ElseIf ccNode.Name = "title" Then
                                strTitle = Convert.ToString(ccNode.InnerText)
                            ElseIf ccNode.Name = "subtitle" Then
                                strSubTitle = Convert.ToString(ccNode.InnerText)
                            End If

                        Next
                    End If
                Next
            Next

            oclient.Dispose()
            oclient = Nothing
            Return dtEducation
        Catch ex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message + " Inner Execption : " + Convert.ToString(ex.InnerException), False)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message, False)
            Return Nothing
        Finally


            sURL = Nothing
            strSql = Nothing
            Newurl = Nothing
            strPath = Nothing

            If Not IsNothing(xDoc) Then
                xDoc = Nothing
            End If

            If Not IsNothing(con) Then
                con.Close()
                con.Dispose()
                con = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            If Not IsNothing(dtSetting) Then
                dtSetting.Dispose()
                dtSetting = Nothing
            End If
        End Try

    End Function

    Private Function Searchnode(ByVal nodetext As String, ByVal trv As TreeView) As TreeNode
        For Each node As TreeNode In trv.Nodes
            If node.Text = nodetext Then
                Return node
            End If
        Next
        Return Nothing
    End Function

    Public Function GetAgeGroup(ByVal ageValue As Integer, ByVal ageUnit As String) As String
        Dim AgeGroup As String = ""

        Select Case ageUnit
            Case "a"
                Select Case ageValue
                    Case 1
                        AgeGroup = "D007223" '"infant" '(1-23 months)
                    Case 2 To 5
                        AgeGroup = "D002675" ' '"child,preschool" '(2-5 Years)
                    Case 6 To 12
                        AgeGroup = "D002648" '"child" '(6-12 Years)
                    Case 13 To 18
                        AgeGroup = "D000293" '"adolescent" '(13-18 Years)
                    Case 19 To 24
                        AgeGroup = "D055815" '"young adult" '(19-24 Years)
                    Case 19 To 44
                        AgeGroup = "D000328" '"adult" '(19-44 Years)
                    Case 45 To 64
                        AgeGroup = "D008875" '"middle aged" '(45-64 Years)
                    Case 56 To 79
                        AgeGroup = "D000368" '"aged" '(56-79 Years)
                    Case Else
                        AgeGroup = "D000369" '"aged,80 and older" '(>=80 Years)
                End Select
            Case "mo"
                AgeGroup = "D007223" '"infant" '(1-23 months)
            Case "d"
                AgeGroup = "D007231" ' "infant,newborn" '(birth to 1 month)
        End Select

        Return AgeGroup

    End Function



    Public Function IsSnomedMandatory() As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim Con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim _IsSnomedMandatory As Boolean = False
        Try
            Dim strQry As String = "SELECT ISNULL(sSettingsValue,0) as IsSnomedCTMandatory FROM dbo.Settings WHERE sSettingsName ='REQUIRESNOMEDCT'"

            cmd = New SqlCommand(strQry, Con)
            Con.Open()
            _IsSnomedMandatory = cmd.ExecuteScalar()
            Con.Close()
            Return _IsSnomedMandatory
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If

        End Try
    End Function

    Private Sub Get_PatientDetails(ByVal PatientID As Long)
        Dim dtPatient As DataTable = Nothing
        Try
            dtPatient = GetPatientInfo(PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientLanguage = Convert.ToString(dtPatient.Rows(0)("sLang"))
                    GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
        End Try
    End Sub

    Public Sub GetAge(ByVal BirthDate As DateTime)

        Dim _BDate As DateTime = BirthDate
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = Now.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 And BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If Now < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 

        ' Check if the last month was a full month. 
        If Now < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (Now - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If Now.Day = 29 And Now.Month = 2 Then
                days += 1
            ElseIf Now.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 And Now.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        If years <> 0 Then
            strPatientAgeUnit = "a"
            strPatientAgeValue = Convert.ToString(years)
        ElseIf months <> 0 Then
            strPatientAgeUnit = "mo"
            strPatientAgeValue = Convert.ToString(months)
        ElseIf days <> 0 Then
            strPatientAgeUnit = "d"
            strPatientAgeValue = Convert.ToString(days)
        End If
    End Sub


    'Private Sub Designgrid()
    '    Try
    '        Dim dt As DataTable


    '        Dim oProviderEducation As New clsProviderEducation(GetConnectionString())
    '        If ProviderId = 0 Then
    '            dt = oProviderEducation.GetProviderEducation(0, 0, 0)
    '        Else
    '            dt = oProviderEducation.GetProviderEducation(ProviderId, 1)
    '        End If


    '        C1ProviderEducation.DataSource = Nothing
    '        If dt IsNot Nothing Then

    '            Dim _dv As DataView = dt.Copy().DefaultView
    '            C1ProviderEducation.Visible = True

    '            C1ProviderEducation.DataSource = _dv
    '            C1ProviderEducation.Rows.Fixed = 1


    '            C1ProviderEducation.Cols(Col_ProviderEducationId).Caption = "ProviderEducationID"
    '            C1ProviderEducation.Cols(Col_ProviderId).Caption = "ProviderId"
    '            C1ProviderEducation.Cols(Col_Provider).Caption = "Provider"
    '            C1ProviderEducation.Cols(Col_VisitId).Caption = "VisitiD"
    '            C1ProviderEducation.Cols(Col_DocumentTitle).Caption = "Document Title"
    '            C1ProviderEducation.Cols(Col_DocumentURl).Caption = "Document URL"
    '            C1ProviderEducation.Cols(Col_Document).Caption = "Document"
    '            C1ProviderEducation.Cols(Col_CreatedDateTime).Caption = "Created On"

    '            C1ProviderEducation.Cols(Col_ProviderEducationId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '            C1ProviderEducation.Cols(Col_ProviderId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '            C1ProviderEducation.Cols(Col_Provider).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '            C1ProviderEducation.Cols(Col_VisitId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '            C1ProviderEducation.Cols(Col_DocumentTitle).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '            C1ProviderEducation.Cols(Col_DocumentURl).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '            C1ProviderEducation.Cols(Col_CreatedDateTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

    '            C1ProviderEducation.Cols(Col_ProviderEducationId).Visible = False
    '            C1ProviderEducation.Cols(Col_ProviderId).Visible = False
    '            C1ProviderEducation.Cols(Col_Provider).Visible = True
    '            C1ProviderEducation.Cols(Col_VisitId).Visible = False
    '            C1ProviderEducation.Cols(Col_DocumentTitle).Visible = True
    '            C1ProviderEducation.Cols(Col_DocumentURl).Visible = True
    '            C1ProviderEducation.Cols(Col_Document).Visible = False
    '            C1ProviderEducation.Cols(Col_CreatedDateTime).Visible = True

    '            Dim nWidth As Integer = Me.Width - 10

    '            C1ProviderEducation.Cols(Col_ProviderEducationId).Width = 0
    '            C1ProviderEducation.Cols(Col_ProviderId).Width = 0
    '            C1ProviderEducation.Cols(Col_Provider).Width = CInt((0.13 * (nWidth)))
    '            C1ProviderEducation.Cols(Col_VisitId).Width = 0
    '            C1ProviderEducation.Cols(Col_DocumentTitle).Width = CInt((0.2 * (nWidth)))
    '            C1ProviderEducation.Cols(Col_DocumentURl).Width = CInt((0.5 * (nWidth)))
    '            C1ProviderEducation.Cols(Col_Document).Width = 0
    '            C1ProviderEducation.Cols(Col_CreatedDateTime).Width = CInt((0.12 * (nWidth)))


    '            C1ProviderEducation.Cols(Col_CreatedDateTime).DataType = GetType(DateTime)
    '            C1ProviderEducation.Cols(Col_CreatedDateTime).Format = "MM/dd/yyyy"

    '            C1ProviderEducation.AllowEditing = False
    '            dt.Dispose()
    '            dt = Nothing

    '            If EducationId <> 0 Then

    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub LoadEducation()
        Dim dt As DataTable
        Dim oProviderEducation As New clsProviderEducation(GetConnectionString())
        Me.Cursor = Cursors.WaitCursor
        dt = oProviderEducation.GetProviderEducation(ProviderId, 2, EducationId)
        pnlInfobuttonresurces.Visible = False
        BrowserLink = Convert.ToString(dt.Rows(0)("documentUrl"))
        HomeUrl = BrowserLink
        InfoButtonWebBrowser.Navigate(BrowserLink)
        Me.Cursor = Cursors.Default
    End Sub

    'Private Sub FillProviders()
    '    Try
    '        Dim dt As DataTable
    '        dt = gloGlobal.gloPMMasters.GetProviders()

    '        If dt IsNot Nothing Then
    '            Dim dr As DataRow = dt.NewRow()
    '            dr("nProviderID") = 0
    '            dr("sProviderName") = "All"
    '            dt.Rows.InsertAt(dr, 0)
    '            dt.AcceptChanges()

    '            RemoveHandler cmbProvider.SelectedIndexChanged, AddressOf cmbProvider_SelectedIndexChanged
    '            cmbProvider.DataSource = dt
    '            cmbProvider.ValueMember = dt.Columns("nProviderID").ColumnName
    '            cmbProvider.DisplayMember = dt.Columns("sProviderName").ColumnName
    '            cmbProvider.Refresh()
    '            cmbProvider.SelectedIndex = -1

    '            If ProviderId = 0 Then
    '                ProviderId = gloGlobal.gloPMGlobal.LoginProviderID
    '            End If
    '            'If ProviderId > 0 Then
    '            pnlProviderData.Visible = True
    '            Designgrid()
    '            'End If
    '            cmbProvider.SelectedValue = ProviderId
    '            AddHandler cmbProvider.SelectedIndexChanged, AddressOf cmbProvider_SelectedIndexChanged
    '        End If
    '        dt = Nothing
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '    End Try

    'End Sub

    Private Sub btn_Right_Click(sender As System.Object, e As System.EventArgs) Handles btn_Right.Click
        'btn_Right.BackgroundImage = gloEMR.My.Resources.Rewind
        'btn_Right.BackgroundImageLayout = ImageLayout.Center
        'pnlSmallStrip.Visible = True
        'pnlLeft.Visible = False

        'If pnlSmallStrip.Visible = True Then
        '    pnlLeft.Visible = False
        'Else
        '    pnlSmallStrip.Visible = True 
        '    pnlLeft.Visible = True
        'End If


        If pnlLeft.Visible = True Then
            btn_Right.BackgroundImage = gloEMR.My.Resources.ForwardHover
            btn_Right.BackgroundImageLayout = ImageLayout.Center
            pnlLeft.Visible = False
            pnlSmallStrip.Visible = True
        Else
            btn_Right.BackgroundImage = gloEMR.My.Resources.Rewind
            btn_Right.BackgroundImageLayout = ImageLayout.Center
            pnlLeft.Visible = True
            pnlSmallStrip.Visible = True
        End If

    End Sub

    'Private Sub btn_Right_MouseHover(sender As System.Object, e As System.EventArgs) Handles btn_Right.MouseHover
    '    If pnlLeft.Visible = True Then
    '        btn_Right.BackgroundImage = gloEMR.My.Resources.Rewind
    '        btn_Right.BackgroundImageLayout = ImageLayout.Center
    '    Else
    '        btn_Right.BackgroundImage = gloEMR.My.Resources.ForwardHover
    '        btn_Right.BackgroundImageLayout = ImageLayout.Center
    '    End If
    'End Sub

    'Private Sub btn_Right_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btn_Right.MouseLeave
    '    If pnlLeft.Visible = True Then
    '        btn_Right.BackgroundImage = gloEMR.My.Resources.Rewind
    '        btn_Right.BackgroundImageLayout = ImageLayout.Center
    '    Else
    '        btn_Right.BackgroundImage = gloEMR.My.Resources.ForwardHover
    '        btn_Right.BackgroundImageLayout = ImageLayout.Center
    '    End If
    'End Sub
End Class