Public Class frmDrugProviderAssociation

    Dim oclsDrugProviderAssociation As New clsDrugProviderAssociation
    'Variable declaration by Mayuri:20091009
    'To check whether node added frm treeview drugs
    Private _isNodeAdded As Boolean = False
    'To check whether node removed from trvAddDrugstoProvider
    Private _isNodeRemoved As Boolean = False
    Private _isOnlySaveDontCloseform As Boolean = False

    ''New Modification for Drug Sig 

    Private Const COL_sProviderName As Integer = 0

    Private Const COL_nDrugID As Integer = 1
    Private Const COL_nProviderID As Integer = 2
    Private Const COL_sShowDrugName As Integer = 3 'this col shows with DrugName + dosage + drug form
    Private Const COL_sDrugName As Integer = 4
    Private Const COL_sDosage As Integer = 5
    Private Const COL_sRoute As Integer = 6
    Private Const COL_sFrequency As Integer = 7
    Private Const COL_sDrugForm As Integer = 8
    Private Const COL_sNDCCode As Integer = 9
    Private Const COL_nIsNarcotics As Integer = 10
    Private Const COL_sDuration As Integer = 11
    Private Const COL_mpid As Integer = 12
    Private Const COL_sDrugQtyQulifier As Integer = 13

    Private Const COL_nSigID As Integer = 14
    Private Const COL_nDrugType As Integer = 15
    Private Const COL_nRefill As Integer = 16
    Private Const COL_sDispAmt As Integer = 17

    '' Total Column Cont for Drugs Sig-Info Grid
    Private Const COL_DrugCount As Integer = 18

    Private bISNew_SIG As Boolean = False ''flag to check for adding new SIG or Modify Existing

    ''temp variables    
    Dim _nProviderID As Int64 = 0
    Dim _nDrugID As Int64 = 0
    Dim _sDrugName As String = ""
    Dim _sDosage As String = ""
    Dim _sRoute As String = ""
    Dim _sFrequency As String = ""
    Dim _sDrugForm As String = ""
    Dim _sNDCCode As String = ""
    Dim _nIsNarcotics As Int32 = 0
    Dim _sDuration As String = ""
    Dim _sDrugQtyQulifier As String = ""
    Dim _nDrugType As Int32 = 0
    Dim _sRefill As String = ""
    Dim _sDispAmt As String = ""
    Dim _nSigID As Int64 = 0
    Dim _PatientID As Long
    Dim mpid As Int32 = 0

    Dim _providerID As Long

    Public Sub New(ByVal PatientID As Long, Optional ByVal ProviderID As Long = 0)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
        _providerID = ProviderID
    End Sub
    Private Sub tblbtn_Close_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close_32.Click
        Me.Close()
    End Sub

    Private Sub btnAllDrugs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllDrugs.Click

        ''added by suraj on 20090127
        txtSearchDrugs.Text = ""
        txtSearchDrugs.Focus()

        'btnAllDrugs,btnNonClinical
        'set the dock properties of the buttons as per user clicks

        pnlbtnAllDrugs.Dock = DockStyle.Top
        btnAllDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnClinical.Dock = DockStyle.Bottom
        btnClinical.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch

        '20090820 for taking AllDrugs buttons in Top
        pnlbtnAllergies.Dock = DockStyle.Bottom
        btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnAllergies.BackgroundImageLayout = ImageLayout.Stretch


        pnlbtnC2.Dock = DockStyle.Bottom
        btnC2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnC2.BackgroundImageLayout = ImageLayout.Stretch


        '' lblSearchDrugs.Text = "Search All Drugs"

        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
        'Call fillData(4, txtSearchDrugs.Text.Trim)
        Dim dt As New DataTable
        Dim strsearch As String = ""
        If Not IsNothing(GloUC_trvAllDrugs.txtsearch.Text) Then
            strsearch = GloUC_trvAllDrugs.txtsearch.Text
        End If
        If gblnResetSearchTextBox = True Then
            strsearch = ""
        End If
        ''  dt = ''oclsDrugProviderAssociation.fillDataFromDb(4, "")
        dt = oclsDrugProviderAssociation.fillDataFromDb(4, strsearch)


        '' Fill Drugs
        GloUC_trvAllDrugs.Clear()
        GloUC_trvAllDrugs.ImageIndex = 2
        GloUC_trvAllDrugs.SelectedImageIndex = 2
        GloUC_trvAllDrugs.DataSource = dt
        ''Sandip Darade 20091014  if drugs to be filled in the treview 
        GloUC_trvAllDrugs.IsDrug = True
        GloUC_trvAllDrugs.DrugFlag = 11 '' flag to pull drugs 
        GloUC_trvAllDrugs.ValueMember = dt.Columns(0).ColumnName
        GloUC_trvAllDrugs.DescriptionMember = dt.Columns("Dosage").ColumnName
        GloUC_trvAllDrugs.CodeMember = dt.Columns("Column1").ColumnName
        GloUC_trvAllDrugs.DrugFormMember = dt.Columns("DrugForm").ColumnName
        GloUC_trvAllDrugs.RouteMember = dt.Columns("Route").ColumnName
        GloUC_trvAllDrugs.NDCCodeMember = dt.Columns("NDCCode").ColumnName
        GloUC_trvAllDrugs.IsNarcoticsMember = dt.Columns("IsNarcotic").ColumnName
        GloUC_trvAllDrugs.FrequencyMember = dt.Columns("Frequency").ColumnName
        GloUC_trvAllDrugs.DurationMember = dt.Columns("Duration").ColumnName
        GloUC_trvAllDrugs.DrugQtyQualifierMember = dt.Columns("DrugQtyQualifier").ColumnName

        GloUC_trvAllDrugs.mpidmember = dt.Columns("mpid").ColumnName

        If dt.Rows.Count > 50 Then
            GloUC_trvAllDrugs.MaximumNodes = 50
        End If
        GloUC_trvAllDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
        GloUC_trvAllDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple

        GloUC_trvAllDrugs.FillTreeView()


        GloUC_trvAllDrugs.FocusSearchBox()

    End Sub

    Private Sub btnAllDrugs_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllDrugs.MouseEnter
        ' button background changes for the effect of selection 
        'btnAllDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn
        'btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnAllDrugs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllDrugs.MouseHover
        'ToolTip1.SetToolTip(Me.btnAllDrugs, "View all Drugs")
        ' Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnAllDrugs, "View All Drugs")

        btnAllDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnAllDrugs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllDrugs.MouseLeave
        ' button background changes for the effect of selection 
        If pnlbtnAllDrugs.Dock = DockStyle.Bottom Then
            btnAllDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnAllDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnAllergies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllergies.Click
        ''added by suraj on 20090127
        txtSearchDrugs.ResetText()
        txtSearchDrugs.Focus()


        Panel3.BringToFront()
        Panel6.BringToFront()


        pnlbtnAllergies.Dock = DockStyle.Top
        btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnAllergies.BackgroundImageLayout = ImageLayout.Stretch


        pnlbtnC2.Dock = DockStyle.Bottom
        btnC2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnC2.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnAllDrugs.Dock = DockStyle.Bottom
        btnAllDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch


        pnlbtnClinical.Dock = DockStyle.Bottom
        btnClinical.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch

        ''lblSearch.Text = "Search Allergies"

        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
        Call fillData(1)
        ' enable for the cilinical drugs
        btnClinical.Enabled = True

        ' fill data in to Tree view for add new drugs
        'Call filldataForAddDrugs(1)


    End Sub

    Private Sub btnAllergies_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllergies.MouseEnter
        ' button background changes for the effect of selection 
        'btnAllergies.BackgroundImage = gloEMR.My.Resources.Resources.yellowbtn
        'btnAllergies.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnAllergies_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllergies.MouseHover
        'ToolTip1.SetToolTip(Me.btnAllDrugs, "View all Allergies")
        'Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnAllergies, "View All Allergies")

        btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnAllergies.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnAllergies_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllergies.MouseLeave
        If pnlbtnAllergies.Dock = DockStyle.Bottom Then
            btnAllergies.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnAllergies.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnAllergies.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnC2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnC2.Click
        ''added by suraj on 20090127
        txtSearchDrugs.Text = ""
        txtSearchDrugs.Focus()

        Panel3.BringToFront()
        Panel6.BringToFront()

        pnlbtnC2.Dock = DockStyle.Top
        btnC2.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
        btnC2.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnAllDrugs.Dock = DockStyle.Bottom
        btnAllDrugs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
        btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch


        pnlbtnClinical.Dock = DockStyle.Bottom
        btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnAllergies.Dock = DockStyle.Bottom
        btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnAllergies.BackgroundImageLayout = ImageLayout.Stretch



        '' lblSearch.Text = "Search C2"

        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
        Call fillData(2)
        btnClinical.Enabled = True
    End Sub

    Private Sub btnC2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnC2.MouseEnter
        btnC2.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnC2.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnC2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnC2.MouseHover
        'ToolTip1.SetToolTip(Me.btnAllDrugs, "View all C2/Schedule II Drugs")
        'Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnC2, "View Schedule II Drugs") ''bug fix 14359

        btnC2.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnC2.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnC2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnC2.MouseLeave
        If pnlbtnC2.Dock = DockStyle.Bottom Then
            btnC2.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnC2.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnC2.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
            btnC2.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnClinical_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClinical.Click
        'added by suraj on 20090127
        txtSearchDrugs.ResetText()
        txtSearchDrugs.Focus()

        Panel3.BringToFront()
        Panel6.BringToFront()
        ''Ojeswini
        pnlbtnClinical.Dock = DockStyle.Top
        btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnAllergies.Dock = DockStyle.Bottom
        btnAllergies.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnAllergies.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnC2.Dock = DockStyle.Bottom
        btnC2.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
        btnC2.BackgroundImageLayout = ImageLayout.Stretch


        pnlbtnAllDrugs.Dock = DockStyle.Bottom
        btnAllDrugs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
        btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

        '' lblSearch.Text = "Search Clinical"

        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
        Call fillData(3)
        'btnClinicalDrugs1.Visible = False
        btnClinical.Enabled = False
    End Sub

    Private Sub btnClinical_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClinical.MouseEnter
        'btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.yellowbtn
        'btnClinical.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnClinical_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClinical.MouseHover
        'ToolTip1.SetToolTip(Me.btnAllDrugs, "View all Clinical Drugs")
        'Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnClinical, "View Clinical Drugs")

        btnClinical.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnClinical_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClinical.MouseLeave
        ' button background changes for the effect of selection 
        If pnlbtnClinical.Dock = DockStyle.Bottom Then
            btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnClinical.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
            btnClinical.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnClinicalDrugsRight_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ''added by suraj on 20090127
        txtSearchDrugs.Text = ""
        txtSearchDrugs.Focus()

        'btnAllDrugs,btnNonClinical
        pnlbtnClinical.Dock = DockStyle.Top
        btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnAllDrugs.Dock = DockStyle.Bottom
        btnAllDrugs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
        btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

        ''lblSearchDrugs.Text = "Search Clinical Drugs"

        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
        '  Call fillData(5)
        Dim dt As New DataTable
        dt = oclsDrugProviderAssociation.fillDataFromDb(5, "")
        If Not IsNothing(dt) Then
            GloUC_trvAllDrugs.DataSource = dt
            GloUC_trvAllDrugs.ImageIndex = 2
            GloUC_trvAllDrugs.SelectedImageIndex = 2
            GloUC_trvAllDrugs.ValueMember = dt.Columns(0).ColumnName
            GloUC_trvAllDrugs.DescriptionMember = dt.Columns("Dosage").ColumnName
            GloUC_trvAllDrugs.CodeMember = dt.Columns(1).ColumnName
            GloUC_trvAllDrugs.DrugFormMember = dt.Columns("DrugForm").ColumnName
            GloUC_trvAllDrugs.RouteMember = dt.Columns("Route").ColumnName
            GloUC_trvAllDrugs.NDCCodeMember = dt.Columns("NDCCode").ColumnName
            GloUC_trvAllDrugs.IsNarcoticsMember = dt.Columns("IsNarcotic").ColumnName
            GloUC_trvAllDrugs.FrequencyMember = dt.Columns("Frequency").ColumnName
            GloUC_trvAllDrugs.DurationMember = dt.Columns("Duration").ColumnName
            GloUC_trvAllDrugs.DrugQtyQualifierMember = dt.Columns("DrugQtyQualifier").ColumnName
            GloUC_trvAllDrugs.mpidmember = dt.Columns("mpid").ColumnName
            GloUC_trvAllDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
            GloUC_trvAllDrugs.FillTreeView()
        End If


    End Sub

    Private Sub btnClinicalDrugsRight_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        'btnClinicalDrugsRight.BackgroundImage = gloEMR.My.Resources.Resources.yellowbtn
        'btnClinicalDrugsRight.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnClinicalDrugsRight_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        'ToolTip1.SetToolTip(Me.btnAllDrugs, "View all Non-Clinical Drugs")
        ' Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnClinical, "View Non-Clinical Drugs")

        btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnClinical.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnClinicalDrugsRight_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        If pnlbtnClinical.Dock = DockStyle.Bottom Then
            btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnClinical.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnClinical.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
            btnClinical.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnProvider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.Click
        ''added by suraj on 20090127
        txtSearch.Text = ""
        txtSearch.Focus()

        ''lblSearch.Text = "Search Providers"
        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
        Call fillData(0)
        'btnClinical.Enabled = True

    End Sub

    Private Sub btnProvider_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.MouseEnter
        'btnProvider.BackgroundImage = gloEMR.My.Resources.Resources.yellowbtn
        'btnProvider.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnProvider_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.MouseHover
        ' Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnProvider, "View Providers ")

        btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnProvider.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnProvider_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.MouseLeave
        If pnlbtnProvider.Dock = DockStyle.Bottom Then
            btnProvider.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnProvider.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnProvider.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
            btnProvider.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Function fillData(ByVal id As Int16, Optional ByVal strsearch As String = "") As Boolean
        ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs

        Try


            If id <= 3 Then
                trvAllDrugs.Nodes.Clear()
            End If


            'filling data from database that default view on click

            Dim dt As New DataTable
            Dim Drugflag As Int16 = 0
            ''Sandip Darade 20091014  if drugs to be filled in the treview 
            Select Case (id)
                Case 1
                    Drugflag = 14 ''Allergies 
                Case 2
                    Drugflag = 15   '' flag to fill C2/Schedule II drugs 
                Case 3
                    Drugflag = 12   '' flag to fill All Clinical Drugs
                Case 4
                    Drugflag = 11   '' flag to fill All drugs
                Case 5
                    Drugflag = 12   '' flag to fill All Clinical Drugs

            End Select

            If (id = 0) Then
                ''Sandip Darade 20091014  if drugs to be filled in the treview 
                If Not IsNothing(GloUC_trvDrugs.txtsearch.Text) Then
                    strsearch = GloUC_trvDrugs.txtsearch.Text
                End If
                If gblnResetSearchTextBox = True Then
                    strsearch = ""
                End If
                dt = oclsDrugProviderAssociation.fillDataFromDb(id, strsearch)
                '' Fill Providers
                GloUC_trvDrugs.ImageIndex = 2
                GloUC_trvDrugs.SelectedImageIndex = 2

                GloUC_trvDrugs.DataSource = dt
                GloUC_trvDrugs.ValueMember = dt.Columns(0).ColumnName
                GloUC_trvDrugs.DescriptionMember = dt.Columns(1).ColumnName
                GloUC_trvDrugs.CodeMember = dt.Columns(1).ColumnName
                GloUC_trvDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

                GloUC_trvDrugs.FillTreeView()
                If _providerID > 0 Then
                    Dim mytrenode As gloUserControlLibrary.myTreeNode = GloUC_trvDrugs.SelectNode(_providerID)
                    If mytrenode IsNot Nothing Then
                        LoadAssociation(mytrenode)
                    End If
                    mytrenode = Nothing
                End If

                GloUC_trvAllDrugs.FocusSearchBox()
            Else
                ''Sandip Darade 20091014  if drugs to be filled in the treview 

                If Not IsNothing(GloUC_trvAllDrugs.txtsearch.Text) Then
                    strsearch = GloUC_trvAllDrugs.txtsearch.Text
                End If
                If gblnResetSearchTextBox = True Then
                    strsearch = ""
                End If
                dt = oclsDrugProviderAssociation.fillDataFromDb(id, strsearch)

                '' Fill Drugs
                GloUC_trvAllDrugs.Clear()
                GloUC_trvAllDrugs.ImageIndex = 2
                GloUC_trvAllDrugs.SelectedImageIndex = 2
                GloUC_trvAllDrugs.DataSource = dt
                ''Sandip Darade 20091014  if drugs to be filled in the treview 
                GloUC_trvAllDrugs.IsDrug = True
                GloUC_trvAllDrugs.DrugFlag = Drugflag '' flag to pull drugs 
                GloUC_trvAllDrugs.ValueMember = dt.Columns(0).ColumnName
                GloUC_trvAllDrugs.DescriptionMember = dt.Columns("Dosage").ColumnName
                GloUC_trvAllDrugs.CodeMember = dt.Columns("Column1").ColumnName
                GloUC_trvAllDrugs.DrugFormMember = dt.Columns("DrugForm").ColumnName
                GloUC_trvAllDrugs.RouteMember = dt.Columns("Route").ColumnName
                GloUC_trvAllDrugs.NDCCodeMember = dt.Columns("NDCCode").ColumnName
                GloUC_trvAllDrugs.IsNarcoticsMember = dt.Columns("IsNarcotic").ColumnName
                GloUC_trvAllDrugs.FrequencyMember = dt.Columns("Frequency").ColumnName
                GloUC_trvAllDrugs.DurationMember = dt.Columns("Duration").ColumnName
                GloUC_trvAllDrugs.DrugQtyQualifierMember = dt.Columns("DrugQtyQualifier").ColumnName

                GloUC_trvAllDrugs.mpidmember = dt.Columns("mpid").ColumnName

                If dt.Rows.Count > 50 Then
                    GloUC_trvAllDrugs.MaximumNodes = 50
                End If
                GloUC_trvAllDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                GloUC_trvAllDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple

                GloUC_trvAllDrugs.FillTreeView()
                GloUC_trvAllDrugs.FocusSearchBox()
            End If


            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function
    '\\Added by suraj 20090127
    Private Sub txtSearchDrugs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchDrugs.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                trvAllDrugs.Select()
            Else
                trvAllDrugs.SelectedNode = trvAllDrugs.Nodes.Item(0)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtSearchDrugs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchDrugs.TextChanged
        ' text search in the filled tree view
        If pnlbtnAllDrugs.Dock = DockStyle.Top Then
            Call fillData(4, txtSearchDrugs.Text.Trim)
        Else
            Call fillData(5, txtSearchDrugs.Text.Trim)
        End If

        If txtSearchDrugs.Text.Trim.Length > 1 Then
            Dim mychildnode As myTreeNode
            'child node collection
            For Each mychildnode In trvAllDrugs.Nodes.Item(0).Nodes
                'search against Description
                If UCase(Mid(mychildnode.Text, 1, Len(Trim(txtSearchDrugs.Text)))) = UCase(Trim(txtSearchDrugs.Text)) Then
                    'select matching node
                    'select matching node   '\\Added by suraj 20090127
                    If Not IsNothing(trvAllDrugs.Nodes(0)) Then
                        trvAllDrugs.SelectedNode = trvAllDrugs.Nodes(0).LastNode
                    End If
                    trvAllDrugs.SelectedNode = mychildnode
                    txtSearchDrugs.Focus()
                    Exit Sub
                End If
            Next
        End If
    End Sub
    '\\Added by suraj 20090127
    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                trvDrugs.Select()
            Else
                trvDrugs.SelectedNode = trvDrugs.Nodes.Item(0)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        '\\Added by Suraj 20090127- for search in given treeview for Allegies,Provider,c2
        If Len(Trim(txtSearch.Text)) <= 1 Then
            If pnlbtnAllergies.Dock = DockStyle.Top Then
                Call fillData(1, txtSearch.Text.Trim)
            ElseIf pnlbtnC2.Dock = DockStyle.Top Then
                Call fillData(2, txtSearch.Text.Trim)
            ElseIf pnlbtnClinical.Dock = DockStyle.Top Then
                Call fillData(3, txtSearch.Text.Trim)
            ElseIf pnlbtnProvider.Dock = DockStyle.Top Then
                Call fillData(0, txtSearch.Text.Trim)
            End If
        End If

        Dim mychildnode As myTreeNode
        'child node collection
        For Each mychildnode In trvDrugs.Nodes.Item(0).Nodes
            'search against Description
            If UCase(Mid(mychildnode.Text, 1, Len(Trim(txtSearch.Text)))) = UCase(Trim(txtSearch.Text)) Then
                'select matching node
                'trvDrugs.SelectedNode = trvDrugs.SelectedNode.LastNode
                If Not IsNothing(trvDrugs.Nodes(0)) Then  '\\20090127
                    trvDrugs.SelectedNode = trvDrugs.Nodes(0).LastNode
                End If
                trvDrugs.SelectedNode = mychildnode
                txtSearch.Focus()
                Exit Sub
            End If
        Next

    End Sub

    Private Sub frmDrugProviderAssociation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(C1DrugstoProvider)

        Try

            ''''FillPotencyCode()
            txtDoseUnit.Visible = False
            txtDoseUnit.SendToBack()

            '''''''''Disable Potency Unit 8071
            cmbDoseUnit.Visible = False
            cmbDoseUnit.BringToFront()

            cmbDuration.Items.Add("Days")
            cmbDuration.Items.Add("Weeks")
            cmbDuration.Items.Add("Months")
            cmbDuration.Text = cmbDuration.Items(0)

            ' provider call is after All Drugs because default tree view should fill of Providers
            btnAllDrugs_Click(sender, e)
            btnProvider_Click(sender, e)

            'To give Select image to button_Ojeswini

            pnlbtnProvider.Dock = DockStyle.Top
            btnProvider.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongOrange
            btnProvider.BackgroundImageLayout = ImageLayout.Stretch
            If Not _providerID > 0 Then
                setDesignC1()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub trvAllDrugs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvAllDrugs.DoubleClick
        Dim bDuplicateDrug As Boolean
        Dim myRnode As New myTreeNode

        ''Sandip Darade 20090306
        '' for the application to not to respond when clicked on blank area/no node selected  
        If IsNothing(trvAllDrugs.SelectedNode) Then
            Exit Sub
        End If

        'If Not IsNothing(trvDrugs.SelectedNode) Then
        If trvAddDrugsToProvider.Nodes.Count >= 1 Then

            If trvAddDrugsToProvider.Nodes(0).Tag = 0 Then
                Dim nKaySelectd As Long = 0

                If Not (IsNothing(trvDrugs.SelectedNode) OrElse trvAddDrugsToProvider.Nodes(0).Text.Trim = "Provider") Then
                    nKaySelectd = CType(trvDrugs.SelectedNode, myTreeNode).Key

                    If nKaySelectd > 0 Then
                        'trvAddDrugsToProvider.Nodes.Clear()
                        If trvAddDrugsToProvider.Tag = 0 Then ' for Provider
                            'get Id and Text of selected Node ( Provider ) to add drug into provider Treeview
                            '''''''The IF statement is added by Anil on 20071106
                            If trvAllDrugs.SelectedNode.Text = "All Drugs" Or trvAllDrugs.SelectedNode.Text = "Clinical Drugs" Then
                                Exit Sub
                            Else
                                Dim thisNode As myTreeNode = CType(trvAllDrugs.SelectedNode, myTreeNode)
                                myRnode.Text = thisNode.Text
                                myRnode.Key = thisNode.Key
                                '\\ added by suraj 20090127
                                myRnode.DrugName = thisNode.DrugName
                                myRnode.Dosage = thisNode.Dosage
                                myRnode.DrugForm = thisNode.DrugForm
                                myRnode.Route = thisNode.Route
                                myRnode.Frequency = thisNode.Frequency
                                myRnode.Duration = thisNode.Duration

                                myRnode.mpid = thisNode.mpid
                                myRnode.NDCCode = thisNode.NDCCode
                                myRnode.IsNarcotics = thisNode.IsNarcotics
                                myRnode.DrugQtyQualifier = thisNode.DrugQtyQualifier
                                '
                                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                                If Not bDuplicateDrug = True Then
                                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                                    trvAddDrugsToProvider.ExpandAll()
                                Else
                                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
                            End If
                        End If
                    Else
                        MessageBox.Show("Please select the provider to add the drugs.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            ElseIf trvAddDrugsToProvider.Nodes(0).Tag = 1 Then ' Allergies
                Dim thisNode As myTreeNode = CType(trvAllDrugs.SelectedNode, myTreeNode)
                myRnode.Text = thisNode.Text
                myRnode.Key = thisNode.Key
                '\\suraj 20090127
                myRnode.DrugName = thisNode.DrugName
                myRnode.Dosage = thisNode.Dosage
                myRnode.DrugForm = thisNode.DrugForm
                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                If Not bDuplicateDrug = True Then
                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    trvAddDrugsToProvider.ExpandAll()
                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf trvAddDrugsToProvider.Nodes(0).Tag = 2 Then ' C2
                Dim thisNode As myTreeNode = CType(trvAllDrugs.SelectedNode, myTreeNode)
                myRnode.Text = thisNode.Text
                myRnode.Key = thisNode.Key
                '\\suraj 20090127
                myRnode.DrugName = thisNode.DrugName
                myRnode.Dosage = thisNode.Dosage
                myRnode.DrugForm = thisNode.DrugForm
                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                If Not bDuplicateDrug = True Then
                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    trvAddDrugsToProvider.ExpandAll()
                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf trvAddDrugsToProvider.Nodes(0).Tag = 3 Then ' Clinical
                Dim thisNode As myTreeNode = CType(trvAllDrugs.SelectedNode, myTreeNode)
                myRnode.Text = thisNode.Text
                myRnode.Key = thisNode.Key
                '\\suraj 20090127
                myRnode.DrugName = thisNode.DrugName
                myRnode.Dosage = thisNode.Dosage
                myRnode.DrugForm = thisNode.DrugForm
                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                If Not bDuplicateDrug = True Then
                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    trvAddDrugsToProvider.ExpandAll()
                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else

            End If
        Else
            MessageBox.Show("Selected Provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

    End Sub

    Private Sub tblbtn_Save_32_Click() Handles tblbtn_Save_32.Click
        Dim nDrugCounter As Integer = 0
        Dim nProviderId As Int64 = 0
        Dim DrugID As Int64 = 0
        '\\ 20090127
        Dim drugname As String = ""
        Dim dosage As String = ""
        Dim route As String = ""
        Dim frequency As String = ""
        Dim duration As String = ""
        Dim drugform As String = ""
        Dim ndccode As String = ""
        Dim isnarcotics As Integer = 0

        Dim DrugQtyQualifier As String = ""
        '
        Try
            _nProviderID = 0
            _nDrugID = 0
            _sDrugName = ""
            _sDosage = ""
            _sRoute = ""
            _sFrequency = ""
            _sDrugForm = ""
            _sNDCCode = ""
            _nIsNarcotics = 0
            _sDuration = ""
            _sDrugQtyQulifier = ""
            _sRefill = ""
            _nSigID = 0
            mpid = 0

            If C1DrugstoProvider.Rows.Count() > 1 Then

                If lbl_ProviderName.Text.ToString.Trim <> "" Then         ''check for provider Name

                    If Not IsDBNull(C1DrugstoProvider.Rows(1)(COL_nProviderID)) Then

                        _nProviderID = C1DrugstoProvider.Rows(1)(COL_nProviderID)
                        If _nProviderID > 0 Then
                            oclsDrugProviderAssociation.DeleteProvidersDrugs(_nProviderID)

                            For i As Int32 = 1 To C1DrugstoProvider.Rows.Count - 1

                                _nDrugID = C1DrugstoProvider.Rows(i)(COL_nDrugID)
                                _sDrugName = C1DrugstoProvider.Rows(i)(COL_sDrugName).ToString
                                _sDosage = C1DrugstoProvider.Rows(i)(COL_sDosage).ToString
                                _sRoute = C1DrugstoProvider.Rows(i)(COL_sRoute).ToString
                                _sFrequency = C1DrugstoProvider.Rows(i)(COL_sFrequency).ToString
                                _sDrugForm = C1DrugstoProvider.Rows(i)(COL_sDrugForm).ToString
                                _sNDCCode = C1DrugstoProvider.Rows(i)(COL_sNDCCode).ToString
                                _nIsNarcotics = C1DrugstoProvider.Rows(i)(COL_nIsNarcotics)
                                '\\ fetching Duration Value Days\weeks\months
                                _sDuration = C1DrugstoProvider.Rows(i)(COL_sDuration).ToString.Replace("|", " ")

                                mpid = C1DrugstoProvider.Rows(i)(COL_mpid)
                                _sDrugQtyQulifier = C1DrugstoProvider.Rows(i)(COL_sDrugQtyQulifier).ToString

                                If Not IsNothing(C1DrugstoProvider.Rows(i)(COL_sDrugQtyQulifier)) Then
                                    _sDrugQtyQulifier = C1DrugstoProvider.Rows(i)(COL_sDrugQtyQulifier).ToString
                                End If
                                If Not IsNothing(C1DrugstoProvider.Rows(i)(COL_nRefill)) Then
                                    _sRefill = C1DrugstoProvider.Rows(i)(COL_nRefill).ToString
                                End If

                                If Not IsNothing(C1DrugstoProvider.Rows(i)(COL_sDispAmt)) Then
                                    _sDispAmt = C1DrugstoProvider.Rows(i)(COL_sDispAmt).ToString
                                End If

                                _nSigID = C1DrugstoProvider.Rows(i)(COL_nSigID)
                                ''----------
                                oclsDrugProviderAssociation.AddProvidersDrugs(_nProviderID, _nDrugID, _sDrugName, _sDosage, _sRoute, _sFrequency, _sDuration, _sDrugForm, _sNDCCode, _nIsNarcotics, _sDrugQtyQulifier, _sRefill, _nSigID, _sDispAmt, mpid)
                            Next
                        End If

                    End If

                End If
            End If

            If _isOnlySaveDontCloseform = True Then
                _isOnlySaveDontCloseform = False
                Exit Sub
            Else
                _isOnlySaveDontCloseform = False
                ''save&Cls- so close the form
                Me.Close()
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally

        End Try

    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            'If Not IsNothing(trvDrugs.SelectedNode) Then
            If Not IsNothing(GloUC_trvDrugs.SelectedNode) Then
                Dim nproviderId As Long = 0
                Dim nKaySelectd As Long = 0
                If IsNothing(trvAddDrugsToProvider.SelectedNode) = False Then
                    nKaySelectd = CType(trvAddDrugsToProvider.SelectedNode, myTreeNode).Key
                Else
                    nproviderId = CType(trvAddDrugsToProvider.Nodes(0), myTreeNode).Key
                End If
                If nKaySelectd > 0 Then
                    'trvAddDrugsToProvider.Nodes.Clear()
                    Dim myRnode As New myTreeNode

                    'get Id and Text of selected Node ( Provider )
                    myRnode.Text = trvAddDrugsToProvider.SelectedNode.Text
                    myRnode.Key = CType(trvAddDrugsToProvider.SelectedNode, myTreeNode).Key

                    oclsDrugProviderAssociation.DeleteProvidersDrugs(nproviderId, nKaySelectd)
                    'trvDrugs_AfterSelect(sender, e)
                    trvAddDrugsToProvider.Nodes.Remove(trvAddDrugsToProvider.SelectedNode)

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvAddDrugsToProvider_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAddDrugsToProvider.MouseDown
        ' Context Menu
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ' _isNodeRemoved = True
            trvAddDrugsToProvider.SelectedNode = trvAddDrugsToProvider.GetNodeAt(e.X, e.Y)
            If IsNothing(trvAddDrugsToProvider.SelectedNode) = False Then
                If trvAddDrugsToProvider.SelectedNode Is trvAddDrugsToProvider.Nodes(0) Then
                    'Try
                    '    If (IsNothing(trvAddDrugsToProvider.ContextMenuStrip) = False) Then
                    '        trvAddDrugsToProvider.ContextMenuStrip.Dispose()
                    '        trvAddDrugsToProvider.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvAddDrugsToProvider.ContextMenuStrip = Nothing
                ElseIf IsNothing(trvAddDrugsToProvider.SelectedNode.Parent) = False Then
                    'Try
                    '    If (IsNothing(trvAddDrugsToProvider.ContextMenuStrip) = False) Then
                    '        trvAddDrugsToProvider.ContextMenuStrip.Dispose()
                    '        trvAddDrugsToProvider.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvAddDrugsToProvider.ContextMenuStrip = cntxDeleteDrugs
                Else
                    'Try
                    '    If (IsNothing(trvAddDrugsToProvider.ContextMenuStrip) = False) Then
                    '        trvAddDrugsToProvider.ContextMenuStrip.Dispose()
                    '        trvAddDrugsToProvider.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvAddDrugsToProvider.ContextMenuStrip = Nothing
                End If
            Else
                'Try
                '    If (IsNothing(trvAddDrugsToProvider.ContextMenuStrip) = False) Then
                '        trvAddDrugsToProvider.ContextMenuStrip.Dispose()
                '        trvAddDrugsToProvider.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trvAddDrugsToProvider.ContextMenuStrip = Nothing
            End If
        End If
    End Sub

    '<< NOT in USED >>
    ' check for selected drug is available or not
    'Private Function DuplicateDrugCheck(ByVal Type As Integer, ByVal DrudId As Long) As Boolean
    '\\ Changes done on 200901224 Suraj
    '\\ Check duplication on drugname,dosage,drugform previously check against drugid
    Private Function DuplicateDrugCheck(ByVal Type As Integer, ByVal _DrugName As String, ByVal _Dosage As String, ByVal _DrugForm As String) As Boolean

        Dim bDuplicateDrug As Boolean
        Dim i As Integer = 0

        If Type = 0 Then ' for provider
            For i = 0 To trvAddDrugsToProvider.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode)
                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next

            Return bDuplicateDrug

        ElseIf Type = 1 Then ' Allergies
            'search in nothe the tree views i.e. added tree view and existing tree view
            For i = 0 To trvAddDrugsToProvider.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode)

                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next

            For i = 0 To trvDrugs.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvDrugs.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvDrugs.Nodes(0).Nodes(i), myTreeNode)

                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next

            Return bDuplicateDrug

        ElseIf Type = 2 Then ' C2
            'search in nothe the tree views i.e. added tree view and existing tree view
            For i = 0 To trvAddDrugsToProvider.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode)

                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next

            For i = 0 To trvDrugs.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvDrugs.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvDrugs.Nodes(0).Nodes(i), myTreeNode)
                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next

            Return bDuplicateDrug

        ElseIf Type = 3 Then ' Clinical
            'search in nothe the tree views i.e. added tree view and existing tree view
            For i = 0 To trvAddDrugsToProvider.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvAddDrugsToProvider.Nodes(0).Nodes(i), myTreeNode)

                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next

            For i = 0 To trvDrugs.Nodes(0).Nodes.Count - 1
                'If DrudId = CType(trvDrugs.Nodes(0).Nodes(i), myTreeNode).Key Then
                Dim thisNode As myTreeNode = CType(trvDrugs.Nodes(0).Nodes(i), myTreeNode)
                If _DrugName = thisNode.DrugName AndAlso _Dosage = thisNode.Dosage AndAlso _DrugForm = thisNode.DrugForm Then
                    bDuplicateDrug = True
                    Exit For
                Else
                    bDuplicateDrug = False
                End If
            Next
            'return the flag for the data
            Return bDuplicateDrug
        Else
            MessageBox.Show("Please select the Type ")
            DuplicateDrugCheck = Nothing
            Exit Function
        End If

    End Function

    '<< New Logic - to check duplicate drug USEING C1Grid >>
    Private Function DuplicateDrugCheck_C1(ByVal _NDCCode As String, ByVal _DrugName As String, ByVal _Dosage As String, ByVal _Route As String, ByVal _Frequency As String, ByVal _Duration As String) As Boolean

        Dim bDuplicateDrug As Boolean
        Dim i As Integer = 0
        If C1DrugstoProvider.Rows.Count > 0 Then

            'If Type = 0 Then ' for provider
            For i = 1 To C1DrugstoProvider.Rows.Count - 1
                'Check first with NDCCode
                If _NDCCode.Trim.ToString <> "" Then
                    If _NDCCode = C1DrugstoProvider.Rows(i)(COL_sNDCCode).ToString Then
                        bDuplicateDrug = True
                        Return bDuplicateDrug
                    Else
                        bDuplicateDrug = False
                        Continue For
                    End If
                End If

                'NDC Code is blank , So Check now with Sig Parameter
                If _DrugName.ToUpper = C1DrugstoProvider.Rows(i)(COL_sDrugName).ToString.ToUpper AndAlso _Dosage.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sDosage).ToString AndAlso _Route.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sRoute).ToString AndAlso _Frequency.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sFrequency).ToString AndAlso _Duration.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sDuration).ToString Then
                    bDuplicateDrug = True
                    Return bDuplicateDrug
                Else
                    bDuplicateDrug = False
                End If
            Next
            Return bDuplicateDrug

        End If
        Return Nothing
    End Function

    '<<  to check duplicate drug from SIG control to C1Grid >>
    Private Function DuplicateDrugCheck_C1fromSigCtrl(ByVal _DrugName As String, ByVal _Dosage As String, ByVal _Route As String, ByVal _Frequency As String, ByVal _Duration As String, ByVal _sRefill As String, ByVal _sDispAmt As String) As Boolean
        Dim bDuplicateDrug As Boolean
        Dim i As Integer = 0
        Try
            If C1DrugstoProvider.Rows.Count > 0 Then
                'If Type = 0 Then ' for provider
                For i = 1 To C1DrugstoProvider.Rows.Count - 1
                    If _DrugName.ToUpper = C1DrugstoProvider.Rows(i)(COL_sDrugName).ToString.ToUpper And _Dosage.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sDosage).trim.ToString And _Route.Trim.ToString.ToUpper = C1DrugstoProvider.Rows(i)(COL_sRoute).ToString.ToUpper And _Frequency.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sFrequency).trim.ToString And _Duration.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sDuration).trim.ToString And _sRefill.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_nRefill).trim.ToString And _sDispAmt.Trim.ToString = C1DrugstoProvider.Rows(i)(COL_sDispAmt).trim.ToString Then
                        bDuplicateDrug = True
                        Return bDuplicateDrug
                    Else
                        bDuplicateDrug = False
                    End If
                Next
                Return bDuplicateDrug
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub trvDrugs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvDrugs.DoubleClick
        Dim dt As DataTable
        Try
            If pnlbtnProvider.Dock = DockStyle.Top Then
                If Not IsNothing(trvDrugs.SelectedNode) Then
                    Dim thisNode As myTreeNode = CType(trvDrugs.SelectedNode, myTreeNode)
                    Dim nKaySelectd As Long = 0
                    nKaySelectd = thisNode.Key

                    If nKaySelectd > 0 Then

                        trvAddDrugsToProvider.Nodes.Clear()
                        Dim myRnode As New myTreeNode

                        'get Id and Text of selected Node ( Provider )
                        myRnode.Text = "Provider : " & trvDrugs.SelectedNode.Text
                        myRnode.ImageIndex = 6
                        myRnode.SelectedImageIndex = 6
                        myRnode.Key = thisNode.Key

                        '\\added by suraj 20090127
                        myRnode.DrugName = thisNode.DrugName
                        myRnode.Dosage = thisNode.Dosage
                        myRnode.DrugForm = thisNode.DrugForm
                        myRnode.Route = thisNode.Route
                        myRnode.Frequency = thisNode.Frequency
                        myRnode.Duration = thisNode.Duration
                        myRnode.mpid = thisNode.mpid
                        myRnode.NDCCode = thisNode.NDCCode
                        myRnode.IsNarcotics = thisNode.IsNarcotics
                        myRnode.DrugQtyQualifier = thisNode.DrugQtyQualifier
                        '\\

                        ' add data into tree
                        trvAddDrugsToProvider.Nodes.Add(myRnode)

                        'returning data
                        dt = oclsDrugProviderAssociation.fillProvidersDrugs(myRnode.Key)

                        Dim mynode As myTreeNode
                        Dim i As Int16
                        For i = 0 To dt.Rows.Count - 1
                            mynode = New myTreeNode
                            'mynode.Text = dt.Rows.Item(i)(1) & dt.Rows.Item(i)(2)
                            'mynode.Key = dt.Rows.Item(i)(0)
                            '\\20090127
                            mynode.Text = dt.Rows(i)("DrugName")  '\\drugname & dosage & drugform
                            mynode.DrugName = dt.Rows(i)("DrugName")
                            mynode.DrugForm = dt.Rows(i)("DrugForm")
                            mynode.Dosage = dt.Rows(i)("Dosage")
                            mynode.Route = dt.Rows(i)("Route")
                            mynode.Frequency = dt.Rows(i)("Frequency")
                            mynode.Duration = dt.Rows(i)("Duration")
                            mynode.NDCCode = dt.Rows(i)("NDCCode")
                            mynode.IsNarcotics = dt.Rows(i)("IsNarcotic")
                            mynode.DrugQtyQualifier = dt.Rows(i)("DrugQtyQualifier")
                            mynode.mpid = dt.Rows(i)("mpid")
                            '\\
                            mynode.Key = dt.Rows.Item(i)(0) '\\drugid
                            mynode.ImageIndex = 2
                            mynode.SelectedImageIndex = 2
                            trvAddDrugsToProvider.Nodes(0).Nodes.Add(mynode)
                            mynode = Nothing
                        Next
                        trvAddDrugsToProvider.ExpandAll()
                        'MessageBox.Show("TExt" & myRnode.Text & " - KEy " & myRnode.Key) 


                    End If
                End If
            Else
                trvAddDrugsToProvider.Nodes.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function setDesignC1()
        Try

            '' Desing Grid
            C1DrugstoProvider.Cols.Fixed = 0
            C1DrugstoProvider.Rows.Fixed = 1
            C1DrugstoProvider.Rows.Count = 1
            C1DrugstoProvider.Cols.Count = COL_DrugCount
            C1DrugstoProvider.ExtendLastCol = True
            gloC1FlexStyle.Style(C1DrugstoProvider)

            C1DrugstoProvider.AllowEditing = False

            Dim _TotalWidth As Single = C1DrugstoProvider.Width - 5

            '' Set Headers 
            ''C1DrugstoProvider.SetData(0, COL_SELECT, "")

            C1DrugstoProvider.SetData(0, COL_sProviderName, "Provider Name")
            C1DrugstoProvider.SetData(0, COL_nDrugID, "DrugID")
            C1DrugstoProvider.SetData(0, COL_nProviderID, "ProviderID")
            C1DrugstoProvider.SetData(0, COL_sShowDrugName, "Drug Name")
            C1DrugstoProvider.SetData(0, COL_sDrugName, "Hidden Drug Name")
            C1DrugstoProvider.SetData(0, COL_sDosage, "Dosage")
            C1DrugstoProvider.SetData(0, COL_sRoute, "Route")
            C1DrugstoProvider.SetData(0, COL_sFrequency, "Patient Directions")
            C1DrugstoProvider.SetData(0, COL_sDrugForm, "DrugForm")
            C1DrugstoProvider.SetData(0, COL_sNDCCode, "NDCCode")
            C1DrugstoProvider.SetData(0, COL_nIsNarcotics, "IsNarcotics")
            C1DrugstoProvider.SetData(0, COL_sDuration, "Duration")
            C1DrugstoProvider.SetData(0, COL_sDrugQtyQulifier, "DrugQtyQulifier")
            C1DrugstoProvider.SetData(0, COL_nSigID, "SigID")
            C1DrugstoProvider.SetData(0, COL_nRefill, "No of Refill")
            C1DrugstoProvider.SetData(0, COL_sDispAmt, "Quantity")
            C1DrugstoProvider.SetData(0, COL_nDrugType, "DrugType")
            C1DrugstoProvider.SetData(0, COL_mpid, "mpid")

            ''Set width
            C1DrugstoProvider.Cols(COL_sProviderName).Width = 0
            C1DrugstoProvider.Cols(COL_nDrugID).Width = 0
            C1DrugstoProvider.Cols(COL_nProviderID).Width = 0
            C1DrugstoProvider.Cols(COL_sShowDrugName).Width = _TotalWidth * 0.5
            C1DrugstoProvider.Cols(COL_sDrugName).Width = 0
            C1DrugstoProvider.Cols(COL_sDosage).Width = 0 '_TotalWidth * 0.1
            C1DrugstoProvider.Cols(COL_sRoute).Width = _TotalWidth * 0.1
            C1DrugstoProvider.Cols(COL_sFrequency).Width = _TotalWidth * 0.2
            C1DrugstoProvider.Cols(COL_sDrugForm).Width = 0 '_TotalWidth * 0.1
            C1DrugstoProvider.Cols(COL_sNDCCode).Width = 0
            C1DrugstoProvider.Cols(COL_nIsNarcotics).Width = 0
            C1DrugstoProvider.Cols(COL_sDuration).Width = _TotalWidth * 0.1

            C1DrugstoProvider.Cols(COL_sDrugQtyQulifier).Width = 0 '_TotalWidth * 0.15
            C1DrugstoProvider.Cols(COL_nSigID).Width = 0
            C1DrugstoProvider.Cols(COL_nRefill).Width = _TotalWidth * 0.1
            C1DrugstoProvider.Cols(COL_sDispAmt).Width = _TotalWidth * 0.1
            C1DrugstoProvider.Cols(COL_nDrugType).Width = 0
            C1DrugstoProvider.Cols(COL_mpid).Width = 0

        Catch ex As Exception

        End Try
        Return Nothing
    End Function


    Private Sub tblbtn_Finish_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Finish_32.Click
        tblbtn_Save_32_Click()
        Me.Close()
    End Sub

    Private Sub GloUC_trvDrugs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvDrugs.KeyPress
        Dim dt As DataTable
        Try
            If e.KeyChar = ChrW(13) Then

                If pnlbtnProvider.Dock = DockStyle.Top Then
                    'If Not IsNothing(trvDrugs.SelectedNode) Then

                    Dim nKaySelectd As Long = 0
                    Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvDrugs.SelectedNode, gloUserControlLibrary.myTreeNode)
                    ' nKaySelectd = CType(trvDrugs.SelectedNode, myTreeNode).Key
                    nKaySelectd = oNode.ID
                    If nKaySelectd > 0 Then

                        trvAddDrugsToProvider.Nodes.Clear()
                        Dim myRnode As New myTreeNode
                        'get Id and Text of selected Node ( Provider )
                        myRnode.Text = "Provider : " & oNode.Text
                        myRnode.ImageIndex = 6
                        myRnode.SelectedImageIndex = 6
                        myRnode.Key = oNode.ID
                        '\\added by suraj 20090127
                        myRnode.DrugName = oNode.Code
                        myRnode.Dosage = oNode.Description
                        myRnode.DrugForm = oNode.DrugForm
                        myRnode.Route = oNode.Route
                        myRnode.Frequency = oNode.Frequency
                        myRnode.Duration = oNode.Duration
                        myRnode.mpid = oNode.mpid
                        myRnode.NDCCode = oNode.NDCCode
                        myRnode.IsNarcotics = oNode.IsNarcotics
                        myRnode.DrugQtyQualifier = oNode.DrugQtyQualifier
                        myRnode.mpid = oNode.mpid
                        '\\
                        ' add data into tree
                        trvAddDrugsToProvider.Nodes.Add(myRnode)
                        'returning data
                        dt = oclsDrugProviderAssociation.fillProvidersDrugs(myRnode.Key)
                        Dim mynode As myTreeNode
                        Dim i As Int16
                        For i = 0 To dt.Rows.Count - 1
                            mynode = New myTreeNode
                            'mynode.Text = dt.Rows.Item(i)(1) & dt.Rows.Item(i)(2)
                            'mynode.Key = dt.Rows.Item(i)(0)
                            '\\20090127
                            mynode.Text = dt.Rows(i)("DrugName")  '\\drugname & dosage 
                            ' mynode.Text = dt.Rows(i)("DrugName") & Space(1) & dt.Rows(i)("Dosage") & Space(1) & dt.Rows(i)("DrugForm") '\\drugname & dosage & drugform
                            mynode.DrugName = dt.Rows(i)("DrugName")
                            mynode.DrugForm = dt.Rows(i)("DrugForm")
                            mynode.Dosage = dt.Rows(i)("Dosage")
                            mynode.Route = dt.Rows(i)("Route")
                            mynode.Frequency = dt.Rows(i)("Frequency")
                            mynode.Duration = dt.Rows(i)("Duration")
                            mynode.NDCCode = dt.Rows(i)("NDCCode")
                            mynode.IsNarcotics = dt.Rows(i)("IsNarcotic")
                            mynode.DrugQtyQualifier = dt.Rows(i)("DrugQtyQualifier")

                            mynode.mpid = dt.Rows(i)("mpid")
                            '\\
                            mynode.Key = dt.Rows.Item(i)(0) '\\drugid
                            mynode.ImageIndex = 2
                            mynode.SelectedImageIndex = 2
                            trvAddDrugsToProvider.Nodes(0).Nodes.Add(mynode)
                            mynode = Nothing
                        Next
                        trvAddDrugsToProvider.ExpandAll()
                        'MessageBox.Show("TExt" & myRnode.Text & " - KEy " & myRnode.Key)       


                    End If
                    'End If
                Else
                    trvAddDrugsToProvider.Nodes.Clear()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    ''ADD Provider
    Private Sub GloUC_trvDrugs_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvDrugs.NodeMouseDoubleClick
        LoadAssociation(CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub


    Private Sub LoadAssociation(ByVal oNode As gloUserControlLibrary.myTreeNode)
        Dim dt As DataTable
        Try
            _isOnlySaveDontCloseform = False
            ''hide Sig Control
            pnlDrugSigCtrl.SendToBack()
            pnlDrugSigCtrl.Visible = False

            If pnlbtnProvider.Dock = DockStyle.Top Then
                'If Not IsNothing(trvDrugs.SelectedNode) Then
                Dim Result As DialogResult
                Dim nKaySelectd As Long = 0
                'Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
                'Code Added by Mayuri:20091009
                'to give user facility of save changes or not after adding or removing drugs before selecting another node
                If _isNodeAdded = True Or _isNodeRemoved = True Then
                    Result = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If Result = Windows.Forms.DialogResult.Yes Then
                        _isOnlySaveDontCloseform = True
                        tblbtn_Save_32_Click()

                        _isNodeAdded = False
                        _isNodeRemoved = False
                    ElseIf Result = Windows.Forms.DialogResult.No Then
                        'trvAddDrugsToProvider.Nodes.Clear()
                        C1DrugstoProvider.Clear()
                        setDesignC1()
                        _isNodeAdded = False
                        _isNodeRemoved = False
                    Else
                        _isNodeAdded = False
                        _isNodeRemoved = False
                    End If
                End If
                'End Code Added by Mayuri:20091009

                ' nKaySelectd = CType(trvDrugs.SelectedNode, myTreeNode).Key

                nKaySelectd = oNode.ID
                _nProviderID = oNode.ID
                ' If trvAddDrugsToProvider.Nodes.Count = 0 Then


                If nKaySelectd > 0 Then
                    lbl_ProviderName.Text = oNode.Text.ToString

                    ''''''''''''''''''''''NEW CHANGES replace treeview with C1 Flexgrid-------->
                    setDesignC1()
                    ''Addding provider name
                    Dim r As Integer = 0
                    Dim IsChildrug As Boolean = False

                    dt = oclsDrugProviderAssociation.fillProvidersDrugs(oNode.ID)
                    If Not IsNothing(dt) Then
                        If dt.Rows.Count > 0 Then
                            ''code added for optimization and flickering -pradeep 20110317
                            C1DrugstoProvider.Redraw = False

                            ' _C1DrugstoProvider.Rows.Count = dt.Rows.Count + 1
                            Dim dr As DataRow ', dr1 As DataRow
                            Dim rowIndex As Integer '%, colIndex%
                            rowIndex = 1
                            For Each dr In dt.Rows
                                If rowIndex > 1 Then
                                    For j As Int32 = 1 To C1DrugstoProvider.Rows.Count - 1
                                        If C1DrugstoProvider.Rows(j)(COL_nDrugID) = dr.Item("nDrugID") AndAlso C1DrugstoProvider.Rows(j)(COL_sDrugName).ToString.ToUpper = dr.Item("DrugName").ToString.ToUpper Then
                                            IsChildrug = True
                                            rowIndex = j
                                            Exit For
                                        Else
                                            IsChildrug = False
                                        End If
                                    Next
                                End If
                                If IsChildrug = False Then
                                    C1DrugstoProvider.Rows.Add()
                                    rowIndex = C1DrugstoProvider.Rows.Count - 1
                                    C1DrugstoProvider.Rows(rowIndex).IsNode = True
                                    C1DrugstoProvider.Rows(rowIndex).Node.Level = 0
                                    'C1DrugstoProvider.SetData(r, COL_sProviderName, oNode.Text.ToString)   ''ProviderName
                                    C1DrugstoProvider.SetData(rowIndex, COL_nProviderID, oNode.ID)                 ''ProviderID
                                    C1DrugstoProvider.SetData(rowIndex, COL_nDrugID, dr.Item("nDrugID"))                ''COL_nDrugID
                                    C1DrugstoProvider.SetData(rowIndex, COL_sShowDrugName, Convert.ToString(dr.Item("DrugName")))
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugName, dr.Item("DrugName").ToString)              ''COL_sDrugName 
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDosage, dr.Item("Dosage").ToString)              ''COL_sDosage   ''dosage
                                    C1DrugstoProvider.SetData(rowIndex, COL_sRoute, dr.Item("Route"))          ''Route
                                    C1DrugstoProvider.SetData(rowIndex, COL_sFrequency, dr.Item("Frequency"))
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugForm, dr.Item("DrugForm"))    ''drugform
                                    C1DrugstoProvider.SetData(rowIndex, COL_sNDCCode, dr.Item("NDCCode"))       ''NDCCODE
                                    C1DrugstoProvider.SetData(rowIndex, COL_nIsNarcotics, dr.Item("IsNarcotic"))       ''IsNarcotics
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDuration, dr.Item("Duration"))       ''Duration

                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugQtyQulifier, dr.Item("DrugQtyQualifier"))       ''DrugQtyQualifier
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugQtyQulifier, dr.Item("DrugQtyQualifier"))
                                    ''DrugQtyQualifier
                                    C1DrugstoProvider.SetData(rowIndex, COL_nSigID, dr.Item("SIGID"))
                                    ''COL_nDrugType
                                    C1DrugstoProvider.SetData(rowIndex, COL_nRefill, dr.Item("Refill"))
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDispAmt, dr.Item("Amount"))
                                    C1DrugstoProvider.SetData(rowIndex, COL_mpid, dr.Item("mpid"))
                                    rowIndex = rowIndex + 1
                                Else
                                    C1DrugstoProvider.Tree.Column = COL_sShowDrugName   ''assign COL property to add child 
                                    Dim oChildDrugNode As C1.Win.C1FlexGrid.Node
                                    oChildDrugNode = C1DrugstoProvider.Rows(rowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dr.Item("DrugName").ToString)
                                    Dim _LastIndex As Integer = oChildDrugNode.Row.Index
                                    ' rowIndex = rowIndex + 1
                                    rowIndex = _LastIndex
                                    C1DrugstoProvider.SetData(rowIndex, COL_nProviderID, oNode.ID)                 ''ProviderID
                                    C1DrugstoProvider.SetData(rowIndex, COL_nDrugID, dr.Item("nDrugID"))                ''COL_nDrugID
                                    C1DrugstoProvider.SetData(rowIndex, COL_sShowDrugName, Convert.ToString(dr.Item("DrugName")))
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugName, dr.Item("DrugName").ToString)              ''COL_sDrugName 
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDosage, dr.Item("Dosage").ToString)              ''COL_sDosage   ''dosage
                                    C1DrugstoProvider.SetData(rowIndex, COL_sRoute, dr.Item("Route"))          ''Route
                                    C1DrugstoProvider.SetData(rowIndex, COL_sFrequency, dr.Item("Frequency"))
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugForm, dr.Item("DrugForm"))    ''drugform
                                    C1DrugstoProvider.SetData(rowIndex, COL_sNDCCode, dr.Item("NDCCode"))       ''NDCCODE
                                    C1DrugstoProvider.SetData(rowIndex, COL_nIsNarcotics, dr.Item("IsNarcotic"))       ''IsNarcotics
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDuration, dr.Item("Duration"))       ''Duration

                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugQtyQulifier, dr.Item("DrugQtyQualifier"))       ''DrugQtyQualifier
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDrugQtyQulifier, dr.Item("DrugQtyQualifier"))
                                    ''DrugQtyQualifier
                                    C1DrugstoProvider.SetData(rowIndex, COL_nSigID, dr.Item("SIGID"))
                                    ''COL_nDrugType
                                    C1DrugstoProvider.SetData(rowIndex, COL_nRefill, dr.Item("Refill"))
                                    C1DrugstoProvider.SetData(rowIndex, COL_sDispAmt, dr.Item("Amount"))
                                    C1DrugstoProvider.SetData(rowIndex, COL_mpid, dr.Item("mpid"))
                                End If
                            Next
                            C1DrugstoProvider.Redraw = True

                        End If
                    End If
                End If
                ''end of code  for optimization and flickering -pradeep 20110317
                ''''''''''''''''''''''
            End If

            oNode = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAllDrugs_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvAllDrugs.NodeMouseDoubleClick
        Dim bDuplicateDrug As Boolean
        Dim myRnode As New myTreeNode
        Dim r As Integer = 0
        Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)


        '' ''New LOGIC with C1FlexGrid
        If lbl_ProviderName.Text.ToString.Trim <> "" Then

            If C1DrugstoProvider.Rows.Count > 0 Then      ''Check for Provider is present or not

                '' Problem 00000199 
                '' Description : Duplicate SigId Causes incorrect drug selection.
                '' Reason for change : as a preventative action change the unique id generation logic only for SigID.
                ' GetPrefixTransactionID(_PatientID) ''Generate unique SigID
                Dim sigid As Int64 = gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetUniqueID()

                ''New Logic:> Compare first with NDCCode, if NDCCode is blank -> compare with Sig parameter which are DrugName,Dosage,Route,frequency,Duration
                bDuplicateDrug = DuplicateDrugCheck_C1(oNode.NDCCode, oNode.Code, oNode.Description, oNode.Route, oNode.Frequency, oNode.Duration)
                If Not bDuplicateDrug = True Then
                    'trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    'trvAddDrugsToProvider.ExpandAll()

                    C1DrugstoProvider.Rows.Add()
                    r = C1DrugstoProvider.Rows.Count - 1
                    C1DrugstoProvider.Rows(r).IsNode = True
                    C1DrugstoProvider.Rows(r).Node.Level = 0

                    'C1DrugstoProvider.SetData(r, COL_sProviderName, providername)
                    C1DrugstoProvider.SetData(r, COL_nProviderID, _nProviderID)        ''providerid
                    C1DrugstoProvider.SetData(r, COL_nDrugID, oNode.ID)  '' nDrugID
                    C1DrugstoProvider.SetData(r, COL_sShowDrugName, oNode.Code)
                    C1DrugstoProvider.SetData(r, COL_sDrugName, oNode.Code) ''COL_sDrugName 
                    C1DrugstoProvider.SetData(r, COL_sDosage, oNode.Description)   ''dosage
                    C1DrugstoProvider.SetData(r, COL_sRoute, oNode.Route)          ''Route
                    C1DrugstoProvider.SetData(r, COL_sFrequency, oNode.Frequency)  ''frequency
                    C1DrugstoProvider.SetData(r, COL_sDrugForm, oNode.DrugForm)    ''drugform
                    C1DrugstoProvider.SetData(r, COL_sNDCCode, oNode.NDCCode)       ''NDCCODE
                    C1DrugstoProvider.SetData(r, COL_nIsNarcotics, oNode.IsNarcotics)       ''IsNarcotics
                    C1DrugstoProvider.SetData(r, COL_sDuration, oNode.Duration)       ''Duration

                    C1DrugstoProvider.SetData(r, COL_sDrugQtyQulifier, oNode.DrugQtyQualifier)       ''DrugQtyQualifier
                    C1DrugstoProvider.SetData(r, COL_nSigID, sigid)   ''COL_nSigID  
                    ''COL_nDrugType
                    C1DrugstoProvider.SetData(r, COL_nRefill, "")  ''Add blank refill number(varchar field)
                    C1DrugstoProvider.SetData(r, COL_sDispAmt, "")
                    C1DrugstoProvider.SetData(r, COL_mpid, oNode.mpid)
                    _isNodeAdded = True '' set flag true
                    'Shubhangi 20091208
                    'Check the setting Reset search text box after assiging category
                    If gblnResetSearchTextBox = True Then
                        GloUC_trvAllDrugs.txtsearch.ResetText()
                    End If

                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If
        Else
            MessageBox.Show("Please select a Provider ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    Private Sub GloUC_trvAllDrugs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvAllDrugs.KeyPress
        Dim bDuplicateDrug As Boolean
        Dim myRnode As New myTreeNode

        ''Sandip Darade 20090306
        '' for the application to not to respond when clicked on blank area/no node selected  
        'If IsNothing(trvAllDrugs.SelectedNode) Then
        '    Exit Sub
        'End If
        Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvAllDrugs.SelectedNode, gloUserControlLibrary.myTreeNode)

        'If Not IsNothing(trvDrugs.SelectedNode) Then
        If trvAddDrugsToProvider.Nodes.Count >= 1 Then

            If trvAddDrugsToProvider.Nodes(0).Tag = 0 Then
                Dim nKaySelectd As Long = 0

                '  If Not (IsNothing(trvDrugs.SelectedNode) Or trvAddDrugsToProvider.Nodes(0).Text.Trim = "Provider") Then
                If Not (IsNothing(GloUC_trvDrugs.SelectedNode) OrElse trvAddDrugsToProvider.Nodes(0).Text.Trim = "Provider") Then
                    ' nKaySelectd = CType(trvDrugs.SelectedNode, myTreeNode).Key
                    nKaySelectd = CType(GloUC_trvDrugs.SelectedNode, gloUserControlLibrary.myTreeNode).ID
                    If nKaySelectd > 0 Then
                        'trvAddDrugsToProvider.Nodes.Clear()
                        If trvAddDrugsToProvider.Tag = 0 Then ' for Provider
                            'get Id and Text of selected Node ( Provider ) to add drug into provider Treeview
                            '''''''The IF statement is added by Anil on 20071106
                            'If trvAllDrugs.SelectedNode.Text = "All Drugs" Or trvAllDrugs.SelectedNode.Text = "Clinical Drugs" Then
                            If GloUC_trvAllDrugs.SelectedNode.Text = "All Drugs" Or GloUC_trvAllDrugs.SelectedNode.Text = "Clinical Drugs" Then
                                Exit Sub
                            Else
                                myRnode.Text = oNode.Text
                                myRnode.Key = oNode.ID
                                myRnode.DrugName = oNode.Description
                                myRnode.Dosage = oNode.Code
                                myRnode.DrugForm = oNode.DrugForm
                                myRnode.Route = oNode.Route
                                myRnode.Frequency = oNode.Frequency
                                myRnode.Duration = oNode.Duration
                                myRnode.mpid = oNode.mpid
                                myRnode.NDCCode = oNode.NDCCode
                                myRnode.IsNarcotics = oNode.IsNarcotics
                                myRnode.DrugQtyQualifier = oNode.DrugQtyQualifier
                                myRnode.mpid = oNode.mpid
                                '
                                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                                If Not bDuplicateDrug = True Then
                                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                                    trvAddDrugsToProvider.ExpandAll()
                                Else
                                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs
                            End If
                        End If
                    Else
                        MessageBox.Show("Please select the provider to add the drugs.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            ElseIf trvAddDrugsToProvider.Nodes(0).Tag = 1 Then ' Allergies
                Dim thisNode As myTreeNode = CType(trvAllDrugs.SelectedNode, myTreeNode)
                myRnode.Text = thisNode.Text
                myRnode.Key = thisNode.Key
                '\\suraj 20090127
                myRnode.DrugName = thisNode.DrugName
                myRnode.Dosage = thisNode.Dosage
                myRnode.DrugForm = thisNode.DrugForm
                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                If Not bDuplicateDrug = True Then
                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    trvAddDrugsToProvider.ExpandAll()
                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf trvAddDrugsToProvider.Nodes(0).Tag = 2 Then ' C2
                Dim thisNode As myTreeNode = CType(trvAllDrugs.SelectedNode, myTreeNode)
                myRnode.Text = thisNode.Text
                myRnode.Key = thisNode.Key
                '\\suraj 20090127
                myRnode.DrugName = thisNode.DrugName
                myRnode.Dosage = thisNode.Dosage
                myRnode.DrugForm = thisNode.DrugForm
                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                If Not bDuplicateDrug = True Then
                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    trvAddDrugsToProvider.ExpandAll()
                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf trvAddDrugsToProvider.Nodes(0).Tag = 3 Then ' Clinical

                myRnode.Text = oNode.Text
                myRnode.Key = oNode.ID
                myRnode.DrugName = oNode.Description
                myRnode.Dosage = oNode.Code
                myRnode.DrugForm = oNode.DrugForm
                'bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.Key)
                bDuplicateDrug = DuplicateDrugCheck(trvAddDrugsToProvider.Nodes(0).Tag, myRnode.DrugName, myRnode.Dosage, myRnode.DrugForm)

                If Not bDuplicateDrug = True Then
                    trvAddDrugsToProvider.Nodes.Item(0).Nodes.Add(myRnode)
                    trvAddDrugsToProvider.ExpandAll()
                Else
                    MessageBox.Show("This drug has already been added in the provider drug list.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else

            End If
        Else
            MessageBox.Show("Selected provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    ''Add ContextMenu_SIG 
    Private Sub C1DrugstoProvider_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1DrugstoProvider.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'Try
                '    If (IsNothing(C1DrugstoProvider.ContextMenuStrip) = False) Then
                '        C1DrugstoProvider.ContextMenuStrip.Dispose()
                '        C1DrugstoProvider.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1DrugstoProvider.ContextMenuStrip = ContextMenuStrip_Sig

                Dim r As Integer = C1DrugstoProvider.HitTest(e.X, e.Y).Row
                C1DrugstoProvider.Select(r, True)
                If r > 0 Then
                    'Try
                    '    If (IsNothing(C1DrugstoProvider.ContextMenuStrip) = False) Then
                    '        C1DrugstoProvider.ContextMenuStrip.Dispose()
                    '        C1DrugstoProvider.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1DrugstoProvider.ContextMenuStrip = ContextMenuStrip_Sig
                    'RowIndex = r
                Else
                    'Try
                    '    If (IsNothing(C1DrugstoProvider.ContextMenuStrip) = False) Then
                    '        C1DrugstoProvider.ContextMenuStrip.Dispose()
                    '        C1DrugstoProvider.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1DrugstoProvider.ContextMenuStrip = Nothing
                End If
            Else
                ' RowIndex = C1DrugstoProvider.Row
            End If

            ' RaiseEvent _FlexMouseDown(sender, e)

        Catch ex As Exception

        End Try

    End Sub

    ''SIG ctrl save&Cls Button
    Private Sub ts_btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSave.Click
        Dim bDuplicateDrug As Boolean = False
        Dim sAmount As String = ""
        Dim sDoseunit As String = ""
        Try
            If Trim(txtRoute.Text) = "" Then
                MessageBox.Show("Route is mandatory", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtRoute.Focus()
                Exit Sub
            End If

            _sDuration = ""
            If txtDuration.Text.ToString.Trim.Length > 0 Then
                _sDuration = txtDuration.Text & " " & cmbDuration.Text.ToString()
            End If

            If bISNew_SIG = True Then
                _isNodeAdded = True '' set flag true

                ''Add new Sig against existing-drug's group or add new
                Dim sNdccode As String = lblNDCCodetxt.Text
                Dim r As Int32 = C1DrugstoProvider.RowSel()
                Dim parentrow As Int32 = 0

                If C1DrugstoProvider.Rows(r).IsNode = True Then
                    If C1DrugstoProvider.Rows(r).Node.Level = 0 Then
                        r = C1DrugstoProvider.RowSel()
                    ElseIf C1DrugstoProvider.Rows(r).Node.Level = 1 Then
                        parentrow = C1DrugstoProvider.Rows(r).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                        r = parentrow
                    End If
                Else
                    Exit Sub
                End If



                '\C1DrugstoProvider.SetData(oChildDrugNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, COL_sProviderName, "")
                '\C1DrugstoProvider.Rows(r).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dt.Rows(i)("DrugName").ToString)

                ''assign textbox value to temp variables
                ''_sProviderName : Do not change
                ''_nProviderID : Do not change
                ''_nDrugID : Do not change
                ''_sDrugName : Do not change
                '_sDosage : Do not change
                _sRoute = txtRoute.Text.ToString
                If _sRoute.ToString.Length = 0 Then
                    _sRoute = ""
                End If
                _sFrequency = txtFrequency.Text.ToString
                If _sFrequency.ToString.Length = 0 Then
                    _sFrequency = ""
                End If

                _sRefill = txtRefills.Text.ToString
                If _sRefill.ToString.Length = 0 Then
                    _sRefill = ""
                End If

                _sDispAmt = txtAmount.Text.ToString & " " & cmbDoseUnit.Text.ToString

                If _sDispAmt.ToString.Length = 0 Then
                    _sDispAmt = ""
                End If

                ''Validation: duplicate drug SIG info check
                bDuplicateDrug = DuplicateDrugCheck_C1fromSigCtrl(_sDrugName, _sDosage, _sRoute, _sFrequency, _sDuration, _sRefill, _sDispAmt)
                If bDuplicateDrug = True Then
                    MessageBox.Show("Duplicate SIG will not be allowed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                '' C1DrugstoProvider.Tree.Column = COL_sDrugName  ''assign COL property to add child 
                C1DrugstoProvider.Tree.Column = COL_sShowDrugName   ''assign COL property to add child 
                Dim oChildDrugNode As C1.Win.C1FlexGrid.Node
                oChildDrugNode = C1DrugstoProvider.Rows(r).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, lblDrugNametxt.Text) 'dt.Rows(i)("DrugName").ToString)
                Dim _LastIndex As Integer = oChildDrugNode.Row.Index
                r = _LastIndex

                '' Problem 00000199
                '' Description : Duplicate SigId Causes incorrect drug selection.
                '' Reason for change : as a preventative action change the unique id generation logic only for SigID.
                _nSigID = gloEMRGeneralLibrary.gloGeneral.clsgeneral.GetUniqueID()  'GetPrefixTransactionID(_PatientID) ''Generate unique SigID

                ''Assign temp variable value to C1DrugstoProvider (temp value from clicked row)
                'C1DrugstoProvider.SetData(r, COL_sProviderName, _sProviderName)  ''make provider col as blank
                C1DrugstoProvider.SetData(r, COL_nProviderID, _nProviderID)        ''providerid
                C1DrugstoProvider.SetData(r, COL_nDrugID, _nDrugID)  '' nDrugID
                C1DrugstoProvider.SetData(r, COL_sShowDrugName, _sDrugName)
                C1DrugstoProvider.SetData(r, COL_sDrugName, _sDrugName) ''COL_sDrugName 
                C1DrugstoProvider.SetData(r, COL_sDosage, _sDosage)   ''dosage
                C1DrugstoProvider.SetData(r, COL_sRoute, _sRoute)          ''Route
                C1DrugstoProvider.SetData(r, COL_sFrequency, _sFrequency)  ''frequency
                C1DrugstoProvider.SetData(r, COL_sDrugForm, _sDrugForm)    ''drugform
                C1DrugstoProvider.SetData(r, COL_sNDCCode, _sNDCCode)       ''NDCCODE
                C1DrugstoProvider.SetData(r, COL_nIsNarcotics, _nIsNarcotics) ''IsNarcotics
                C1DrugstoProvider.SetData(r, COL_sDuration, _sDuration)       ''Duration
                C1DrugstoProvider.SetData(r, COL_sDrugQtyQulifier, _sDrugQtyQulifier)       ''DrugQtyQualifier
                C1DrugstoProvider.SetData(r, COL_nSigID, _nSigID)       ''COL_nSigID
                C1DrugstoProvider.SetData(r, COL_mpid, mpid)
                'COL_nDrugType
                C1DrugstoProvider.SetData(r, COL_nRefill, _sRefill)

                'If _sDispAmt <> "" Then
                '    Dim retval As String() = SplitDuration(_sDispAmt) '\\split value with " "(blank space)
                '    If Not IsNothing(retval) Then
                '        If retval.Length > 1 Then
                '            sAmount = retval(0)
                '            sDoseunit = retval(1)
                '            _sDispAmt = sAmount & " " & sDoseunit
                '        Else
                '            _sDispAmt = sAmount & " " & sDoseunit
                '        End If
                '    Else
                '        C1DrugstoProvider.SetData(r, COL_sDispAmt, _sDispAmt)
                '    End If
                'Else
                C1DrugstoProvider.SetData(r, COL_sDispAmt, _sDispAmt)
                'End If

                C1DrugstoProvider.Refresh()



            Else
                ''Modify existing SIG
                _isNodeAdded = True '' set flag true
                Dim r As Int32 = C1DrugstoProvider.RowSel()

                _sRoute = txtRoute.Text.ToString
                If _sRoute.ToString.Length = 0 Then
                    _sRoute = ""
                End If

                _sFrequency = txtFrequency.Text.ToString
                If _sFrequency.ToString.Length = 0 Then
                    _sFrequency = ""
                End If

                _sRefill = txtRefills.Text.ToString
                If _sRefill.ToString.Length = 0 Then
                    _sRefill = ""
                End If


                _sDispAmt = txtAmount.Text.ToString & " " & cmbDoseUnit.Text.ToString


                If _sDispAmt.ToString.Length = 0 Then
                    _sDispAmt = ""
                End If
                ''_nSigID 

                ''Validation: duplicate drug SIG info check
                ''bug resolve Bug #80192
                ''''bDuplicateDrug = DuplicateDrugCheck_C1fromSigCtrl(_sDrugName, _sDosage, _sRoute, _sFrequency, _sDuration, _sRefill, _sDispAmt)
                If bDuplicateDrug = True Then
                    MessageBox.Show("Duplicate SIG will not be allowed.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If


                ''Assign temp variable value to C1DrugstoProvider (temp value from clicked row)
                'C1DrugstoProvider.SetData(r, COL_sProviderName, _sProviderName)
                'C1DrugstoProvider.SetData(r, COL_nProviderID, _nProviderID)        ''providerid
                'C1DrugstoProvider.SetData(r, COL_nDrugID, _nDrugID)  '' nDrugID
                'C1DrugstoProvider.SetData(r, COL_sDrugName, _sDrugName) ''COL_sDrugName 
                'C1DrugstoProvider.SetData(r, COL_sDosage, _sDosage)   ''dosage
                C1DrugstoProvider.SetData(r, COL_sRoute, _sRoute)          ''Route
                C1DrugstoProvider.SetData(r, COL_sFrequency, _sFrequency)  ''frequency
                'C1DrugstoProvider.SetData(r, COL_sDrugForm, _sDrugForm)    ''drugform
                'C1DrugstoProvider.SetData(r, COL_sNDCCode, _sNDCCode)       ''NDCCODE
                ''IsNarcotics
                C1DrugstoProvider.SetData(r, COL_sDuration, _sDuration)       ''Duration

                C1DrugstoProvider.SetData(r, COL_nRefill, _sRefill)
                C1DrugstoProvider.SetData(r, COL_sDispAmt, _sDispAmt)
                C1DrugstoProvider.Refresh()

            End If

            pnlDrugSigCtrl.SendToBack()
            pnlDrugSigCtrl.Visible = False


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    ''SIG ctrl CLOSE Button
    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        pnlDrugSigCtrl.SendToBack()
        pnlDrugSigCtrl.Visible = False
    End Sub

    ''Add New Sig Parameters
    Private Sub AddNewSigInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewSigInfoToolStripMenuItem.Click
        Try

            Dim r As Int32
            r = C1DrugstoProvider.RowSel()

            If r > 0 Then

                bISNew_SIG = True
                ' C1DrugstoProvider.Dock = DockStyle.Top
                lblNDCCodetxt.Text = C1DrugstoProvider.Rows(r)(COL_sNDCCode).ToString
                lblDrugNametxt.Text = C1DrugstoProvider.Rows(r)(COL_sDrugName).ToString
                txtRoute.Text = C1DrugstoProvider.Rows(r)(COL_sRoute).ToString
                If Trim(txtRoute.Text) <> "" Then
                    txtRoute.Enabled = False
                Else
                    txtRoute.Enabled = True
                End If
                txtFrequency.Text = ""
                txtDuration.Text = ""
                txtRefills.Text = ""
                txtAmount.Text = ""

                cmbDoseUnit.Text = ""


                ''assign value to temp variables

                If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_nProviderID)) Then
                    If C1DrugstoProvider.Rows(r)(COL_nProviderID).ToString.Length > 0 Then
                        _nProviderID = C1DrugstoProvider.Rows(r)(COL_nProviderID)
                    Else
                        _nProviderID = 0
                    End If
                Else
                    _nProviderID = 0
                End If
                _nDrugID = C1DrugstoProvider.Rows(r)(COL_nDrugID)
                _sDrugName = C1DrugstoProvider.Rows(r)(COL_sDrugName).ToString
                _sDosage = C1DrugstoProvider.Rows(r)(COL_sDosage).ToString
                _sRoute = C1DrugstoProvider.Rows(r)(COL_sRoute).ToString
                _sFrequency = C1DrugstoProvider.Rows(r)(COL_sFrequency).ToString
                _sDrugForm = C1DrugstoProvider.Rows(r)(COL_sDrugForm).ToString
                _sNDCCode = C1DrugstoProvider.Rows(r)(COL_sNDCCode).ToString
                _nIsNarcotics = C1DrugstoProvider.Rows(r)(COL_nIsNarcotics)
                '_sDuration = C1DrugstoProvider.Rows(r)(COL_sDuration).ToString

                '_nDrugType = C1DrugstoProvider.Rows(r)(COL_nDrugType)
                _nSigID = C1DrugstoProvider.Rows(r)(COL_nSigID)
                mpid = C1DrugstoProvider.Rows(r)(COL_mpid)

                If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_sDrugQtyQulifier)) Then
                    _sDrugQtyQulifier = C1DrugstoProvider.Rows(r)(COL_sDrugQtyQulifier).ToString
                End If
                If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_nRefill)) Then
                    _sRefill = C1DrugstoProvider.Rows(r)(COL_nRefill).ToString
                End If
                ''EpcsChange.....

                If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_nIsNarcotics)) Then
                    If _nIsNarcotics = 2 Then
                        txtRefills.Text = _sRefill
                        txtRefills.Enabled = False
                    Else
                        txtRefills.Enabled = True
                    End If
                End If

                ''EpcsChange.....
                If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_sDispAmt)) Then
                    _sDispAmt = C1DrugstoProvider.Rows(r)(COL_sDispAmt).ToString
                End If
                '''''''''Disable Potency Unit
                'If String.IsNullOrWhiteSpace(cmbDoseUnit.Text) Then
                '    Dim oDblayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(0)
                '    cmbDoseUnit.Text = oDblayer.GetPotencyUnit(mpid, _nDrugID)
                '    If Not IsNothing(oDblayer) Then
                '        oDblayer.Dispose()
                '        oDblayer = Nothing
                '    End If
                'End If
                'cmbDoseUnit.Enabled = False
                'cmbDoseUnit.DropDownStyle = ComboBoxStyle.Simple


                ''Adjust dock property for panels
                pnlDrugSigCtrl.BringToFront()
                pnlDrugSigCtrl.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function SplitDrug(ByVal _strDuration As String) As Array
        Try
            Dim _result As String()
            _result = _strDuration.Split(" ")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    ''Modify existing Sig parameter
    Private Sub ModifySigInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModifySigInfoToolStripMenuItem.Click
        Dim strDuration As String = ""
        Dim strCmbDuration As String = ""
        Dim strDispense As String = ""
        Dim strDoseUnit As String = ""
        Try

            Dim r As Int32
            r = C1DrugstoProvider.RowSel()
            If C1DrugstoProvider.Rows(r)(COL_sDrugName) <> "" Then ''check drug name should not be blank
                If r > 0 Then
                    bISNew_SIG = False

                    If Not IsNothing(_sDuration) Then
                        ''Dim retval As String() = SplitDuration(_sDuration.ToString) '\\split value with " "(blank space)
                        Dim retval As String() = SplitDrug(C1DrugstoProvider.Rows(r)(COL_sDuration).ToString)

                        If Not IsNothing(retval) Then
                            If retval.Length > 1 Then
                                strDuration = retval(0)
                                strCmbDuration = retval(retval.Length - 1)
                            Else
                                strDuration = retval(0) 'dv.Item(0)("sDuration").ToString
                            End If
                        Else
                            strDuration = retval(0) '"" 'dv.Item(0)("sDuration").ToString
                        End If
                    Else
                        strDuration = ""
                    End If

                    If strCmbDuration <> "" Then
                        If strCmbDuration.ToUpper = "DAYS" Then
                            cmbDuration.Text = cmbDuration.Items(0) '0th item is Days
                        ElseIf strCmbDuration.ToUpper = "WEEKS" Then
                            cmbDuration.Text = cmbDuration.Items(1) '1st item is Weeks
                        Else
                            cmbDuration.Text = cmbDuration.Items(2) '2nd item is Months
                        End If
                    End If

                    ''Assign value to textbox
                    lblNDCCodetxt.Text = C1DrugstoProvider.Rows(r)(COL_sNDCCode).ToString
                    lblDrugNametxt.Text = C1DrugstoProvider.Rows(r)(COL_sDrugName).ToString
                    txtRoute.Text = C1DrugstoProvider.Rows(r)(COL_sRoute).ToString
                    If Trim(txtRoute.Text) <> "" Then
                        txtRoute.Enabled = False
                    Else
                        txtRoute.Enabled = True
                    End If
                    txtFrequency.Text = C1DrugstoProvider.Rows(r)(COL_sFrequency).ToString
                    txtDuration.Text = strDuration
                    txtRefills.Text = C1DrugstoProvider.Rows(r)(COL_nRefill)

                    Dim DispenseAmt As String = C1DrugstoProvider.Rows(r)(COL_sDispAmt)
                    If Not IsNothing(DispenseAmt) AndAlso DispenseAmt <> "" Then
                        ''Dim retval As String() = SplitDuration(_sDuration.ToString) '\\split value with " "(blank space)
                        Dim retval As String() = SplitDrug(C1DrugstoProvider.Rows(r)(COL_sDispAmt).ToString)

                        If Not IsNothing(retval) Then
                            If retval.Length > 1 Then
                                strDispense = retval(0)
                                Dim strbld As New System.Text.StringBuilder
                                For i As Integer = 1 To retval.Length - 1
                                    strbld.Append(" ")
                                    strbld.Append(retval(i).ToString)
                                Next
                                strDoseUnit = strbld.ToString()
                            Else
                                strDispense = retval(0) 'dv.Item(0)("sDuration").ToString
                            End If
                        Else
                            strDispense = retval(0) '"" 'dv.Item(0)("sDuration").ToString
                        End If
                    Else
                        strDispense = ""
                    End If

                    txtAmount.Text = strDispense

                    cmbDoseUnit.Text = strDoseUnit

                    ''assign value to temp variables

                    If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_nProviderID)) Then
                        If C1DrugstoProvider.Rows(r)(COL_nProviderID).ToString.Length > 0 Then
                            _nProviderID = C1DrugstoProvider.Rows(r)(COL_nProviderID)
                        Else
                            _nProviderID = 0
                        End If
                    Else
                        _nProviderID = 0
                    End If
                    _nDrugID = C1DrugstoProvider.Rows(r)(COL_nDrugID)
                    _sDrugName = C1DrugstoProvider.Rows(r)(COL_sDrugName).ToString
                    _sDosage = C1DrugstoProvider.Rows(r)(COL_sDosage).ToString
                    '_sRoute = C1DrugstoProvider.Rows(r)(COL_sRoute).ToString
                    '_sFrequency = C1DrugstoProvider.Rows(r)(COL_sFrequency).ToString
                    _sDrugForm = C1DrugstoProvider.Rows(r)(COL_sDrugForm).ToString
                    _sNDCCode = C1DrugstoProvider.Rows(r)(COL_sNDCCode).ToString
                    _nIsNarcotics = C1DrugstoProvider.Rows(r)(COL_nIsNarcotics)
                    '_sDuration = C1DrugstoProvider.Rows(r)(COL_sDuration).ToString

                    _sDrugQtyQulifier = C1DrugstoProvider.Rows(r)(COL_sDrugQtyQulifier).ToString
                    '_nDrugType = C1DrugstoProvider.Rows(r)(COL_nDrugType)
                    '_nRefill = C1DrugstoProvider.Rows(r)(COL_nRefill).ToString
                    _nSigID = C1DrugstoProvider.Rows(r)(COL_nSigID)
                    mpid = C1DrugstoProvider.Rows(r)(COL_mpid)
                    ''EpcsChange.....

                    '''''''''Disable Potency Unit 
                    'If String.IsNullOrWhiteSpace(cmbDoseUnit.Text) Then
                    '    Dim oDblayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(0)
                    '    cmbDoseUnit.Text = oDblayer.GetPotencyUnit(mpid, _nDrugID)
                    '    If Not IsNothing(oDblayer) Then
                    '        oDblayer.Dispose()
                    '        oDblayer = Nothing
                    '    End If
                    'End If
                    'cmbDoseUnit.Enabled = False
                    'cmbDoseUnit.DropDownStyle = ComboBoxStyle.Simple


                    If Not IsNothing(C1DrugstoProvider.Rows(r)(COL_nIsNarcotics)) Then
                        If _nIsNarcotics = 2 Then
                            txtRefills.Text = C1DrugstoProvider.Rows(r)(COL_nRefill)
                            txtRefills.Enabled = False
                        Else
                            txtRefills.Enabled = True
                        End If
                    End If

                    ''EpcsChange.....

                    ''Adjust dock property for panels
                    pnlDrugSigCtrl.BringToFront()
                    pnlDrugSigCtrl.Visible = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    ''Remove selected Drug from C1grid
    Private Sub RemoveDrugToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveDrugToolStripMenuItem.Click
        Try
            Dim sMSGg As String = ""
            Dim Result As DialogResult
            Dim r As Int32
            r = C1DrugstoProvider.RowSel()

            If r > 0 Then
                If C1DrugstoProvider.Rows(r)(COL_sShowDrugName) <> "" Then
                    If C1DrugstoProvider.Rows(r).Node.Level = 0 Then
                        sMSGg = "Do you want to remove the Drug?"
                    Else
                        sMSGg = "Do you want to remove the SIG Information?"
                    End If
                    Result = MessageBox.Show(sMSGg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If Result = Windows.Forms.DialogResult.Yes Then
                        RemoveDrug(r)

                        ''hide Sig Control
                        pnlDrugSigCtrl.SendToBack()
                        pnlDrugSigCtrl.Visible = False
                    Else
                        Exit Sub
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''Remove Drug
    Private Function RemoveDrug(ByVal r As Int32)
        Dim tdrugID As Int64 = 0
        Dim tsigid As Int64 = 0

        Try
            If r > 0 Then
                If C1DrugstoProvider.Rows.Count > 1 Then

                    _isNodeRemoved = True
                    If C1DrugstoProvider.Rows(r).IsNode = True Then
                        If C1DrugstoProvider.Rows(r).Node.Level = 0 Then
                            If C1DrugstoProvider.Rows(r).Node.Children > 0 Then
                                Dim childcount As Int32 = C1DrugstoProvider.Rows(r).Node.Children

                                If childcount >= 1 Then
                                    For j As Int32 = 0 To childcount
                                        tdrugID = C1DrugstoProvider.Rows(r)(COL_nDrugID)
                                        '' tsigid = C1DrugstoProvider.Rows(r)(COL_nSigID)
                                        oclsDrugProviderAssociation.DeleteProvidersDrugs(_nProviderID, tdrugID, 0) ''remove from DrugProviderAssociation

                                        C1DrugstoProvider.Rows.Remove(r) '' remove selected drug row 

                                    Next
                                    C1DrugstoProvider.Refresh()

                                End If
                            Else

                                tdrugID = C1DrugstoProvider.Rows(r)(COL_nDrugID)
                                ''tsigid = C1DrugstoProvider.Rows(r)(COL_nSigID)
                                oclsDrugProviderAssociation.DeleteProvidersDrugs(_nProviderID, tdrugID, 0) ''remove from DrugProviderAssociation
                                C1DrugstoProvider.Rows.Remove(r) '' remove selected drug row 
                                C1DrugstoProvider.Refresh()

                            End If
                        ElseIf C1DrugstoProvider.Rows(r).Node.Level = 1 Then
                            tdrugID = C1DrugstoProvider.Rows(r)(COL_nDrugID)
                            tsigid = C1DrugstoProvider.Rows(r)(COL_nSigID)
                            oclsDrugProviderAssociation.DeleteProvidersDrugs(_nProviderID, tdrugID, tsigid) ''remove from DrugProviderAssociation

                            C1DrugstoProvider.Rows.Remove(r) '' remove selected drug row 
                            C1DrugstoProvider.Refresh()
                        End If

                    End If
                End If
            End If
        Catch ex As Exception

        End Try
        Return Nothing
    End Function

    ''validate & fixed issue: 5493 Duration field should be numeric
    Private Sub txtDuration_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuration.KeyPress

        Try

            Dim chkNumeric As String = txtDuration.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                        MessageBox.Show("Enter valid numeric value.", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid numeric value.", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1DrugstoProvider_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1DrugstoProvider.MouseMove

        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)

    End Sub

    Private Sub txtRefills_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRefills.KeyPress
        Try

            Dim chkNumeric As String = txtRefills.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric or decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True

                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try

            Dim chkNumeric As String = txtAmount.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub

            End If



            'If chkNumeric = "" Then

            '    Exit Sub
            'End If
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric or decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub




    Private Sub FillPotencyCode()
        Dim dtPotencyCode As New DataTable

        'Dim oDblayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(0)

        Try
            If cmbDoseUnit.Items.Count = 0 Then
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dtPotencyCode = helper.GetPotencyCode()
                End Using
                If dtPotencyCode IsNot Nothing Then
                    If dtPotencyCode.Rows.Count > 0 Then

                        Dim dr As DataRow = dtPotencyCode.NewRow()
                        dr("sPotencycode") = "0"
                        dr("sDescription") = ""
                        dtPotencyCode.Rows.InsertAt(dr, 0)
                        dtPotencyCode.AcceptChanges()

                        cmbDoseUnit.DataSource = dtPotencyCode
                        cmbDoseUnit.ValueMember = dtPotencyCode.Columns("sPotencycode").ColumnName
                        cmbDoseUnit.DisplayMember = dtPotencyCode.Columns("sDescription").ColumnName

                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(dtPotencyCode) Then
                dtPotencyCode.Dispose()
                dtPotencyCode = Nothing
            End If
            'If Not IsNothing(oDblayer) Then
            '    oDblayer.Dispose()
            '    oDblayer = Nothing
            'End If
        Finally
            'If Not IsNothing(oDblayer) Then
            '    oDblayer.Dispose()
            '    oDblayer = Nothing
            'End If
        End Try
    End Sub
End Class
