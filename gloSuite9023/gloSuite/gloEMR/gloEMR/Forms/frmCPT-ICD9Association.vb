Option Compare Text
Imports System.Threading

Public Class frmCPTICD9Association
    Inherits System.Windows.Forms.Form
    '  Dim oclsCPTICD9 As clsCPTICD9Association
    'Dim oclsCPTAssociation As clsCPTAssociation

    Private Const strSortByCode As String = "CODE"
    Private Const strSortByDesc As String = "DESC"

    ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
    Public Shared ISCPTICD9AssocOpen As Boolean = False
    Public Shared CPTICD9SelNodeKey As Long = 0
    ''''''''''''''''''''''''''
    Public Shared CPTICD9SelCode As String = ""
    ''''''''''''''''''''''''''
    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True
    ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
    'code added for optimization in 6020()
    Private Delegate Sub fill_ControlDelegate()

    Dim dtOrderbyCode As DataTable
    Dim dtOrderbyDesc As DataTable
    Dim dtAssociates As New DataTable

    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbDescsearch As System.Windows.Forms.RadioButton
    Friend WithEvents rbCodesearch As System.Windows.Forms.RadioButton
    Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton

    Dim wpfICD10UserControl As gloUIControlLibrary.ICDSubCodeControl = Nothing
    '' chetan added 
    ' Dim objclsSmartDiagnosis As clsSmartDiagnosis
    ' Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    '' chetan added 
    'sarika 24th sept 07


    'Dim dtAssociatesICD9 As New DataTable
    'Dim dtAssociatesDrugs As New DataTable
    'Dim dtAssociatesPatient As New DataTable
    'Dim dtAssociatesTags As New DataTable
    '----------------

    '' Privious Assumtion Ref: ICD9Association
    '''' id=0 '' Drugs
    '''' id=1 '' CPT
    '''' id=2 '' Patient Education
    '''' id=4 '' Tags
    '''' id=else  '' ICD9
    '''''

    Enum Associates
        CPT = 1
        ICD9 = 2
        Drugs = 3
        PE = 4
        Tags = 5
        ''Added Rahul For new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014
        Referral = 6
        Order = 7
        LabOrder = 8
        Flow = 9
        ICD10 = 10
        MUPE = 11
        ''
    End Enum

    Enum OrderBy
        Code = 0
        Description = 1
    End Enum

    Dim ICD9Count As String
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlLeftSearch As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents pnltrvCPT As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents pnlRadiobtn As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnPatientEducation As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnDrugs As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnTags As System.Windows.Forms.Panel
    Friend WithEvents pnltrvAssociates As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnICD9 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents GloUC_trvAssociates As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GloUC_trvCPT As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftRadioBtnTop As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftRadioBtn As System.Windows.Forms.Panel
    Friend WithEvents rbtAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtUnassociated As System.Windows.Forms.RadioButton
    Friend WithEvents rbtAssociated As System.Windows.Forms.RadioButton
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnReferrals As System.Windows.Forms.Panel
    Friend WithEvents btnReferrals As System.Windows.Forms.Button
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnOrders As System.Windows.Forms.Panel
    Friend WithEvents btnOrders As System.Windows.Forms.Button
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnLabOrders As System.Windows.Forms.Panel
    Friend WithEvents btnLabOrders As System.Windows.Forms.Button
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents pnlFlowsheet As System.Windows.Forms.Panel
    Friend WithEvents btnFlowsheet As System.Windows.Forms.Button
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlbtnICD10 As System.Windows.Forms.Panel
    Friend WithEvents btnICD10 As System.Windows.Forms.Button
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Private WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnMUPatientEducation As System.Windows.Forms.Panel
    Friend WithEvents btnMUPatientEducation As System.Windows.Forms.Button
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents elementHostICD10 As System.Windows.Forms.Integration.ElementHost
    Dim CPTCount As String


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        '''''
        frm = Nothing
        '''''
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Dim tooltipnew As ToolTip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents btnPatientEducation As System.Windows.Forms.Button
    Friend WithEvents cntICD9Association As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents btnTags As System.Windows.Forms.Button
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents txtsearchCPT As System.Windows.Forms.TextBox
    Friend WithEvents btnICD9 As System.Windows.Forms.Button
    Friend WithEvents trvAssociates As System.Windows.Forms.TreeView
    Friend WithEvents trvCPTAssociation As System.Windows.Forms.TreeView
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents txtsearchAssociates As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCPTICD9Association))
        Me.tooltipnew = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLeftRadioBtnTop = New System.Windows.Forms.Panel()
        Me.pnlLeftRadioBtn = New System.Windows.Forms.Panel()
        Me.rbtAll = New System.Windows.Forms.RadioButton()
        Me.rbtUnassociated = New System.Windows.Forms.RadioButton()
        Me.rbtAssociated = New System.Windows.Forms.RadioButton()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.pnltrvCPT = New System.Windows.Forms.Panel()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlLeftSearch = New System.Windows.Forms.Panel()
        Me.txtsearchCPT = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GloUC_trvAssociates = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlFlowsheet = New System.Windows.Forms.Panel()
        Me.btnFlowsheet = New System.Windows.Forms.Button()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.pnlbtnLabOrders = New System.Windows.Forms.Panel()
        Me.btnLabOrders = New System.Windows.Forms.Button()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.pnlbtnOrders = New System.Windows.Forms.Panel()
        Me.btnOrders = New System.Windows.Forms.Button()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.pnlbtnReferrals = New System.Windows.Forms.Panel()
        Me.btnReferrals = New System.Windows.Forms.Button()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearchAssociates = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnltrvAssociates = New System.Windows.Forms.Panel()
        Me.trvAssociates = New System.Windows.Forms.TreeView()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.pnlbtnTags = New System.Windows.Forms.Panel()
        Me.btnTags = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.pnlbtnDrugs = New System.Windows.Forms.Panel()
        Me.btnDrugs = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnlRadiobtn = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbCodesearch = New System.Windows.Forms.RadioButton()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.rbDescsearch = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.pnlbtnPatientEducation = New System.Windows.Forms.Panel()
        Me.btnPatientEducation = New System.Windows.Forms.Button()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.pnlbtnMUPatientEducation = New System.Windows.Forms.Panel()
        Me.btnMUPatientEducation = New System.Windows.Forms.Button()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.pnlbtnICD10 = New System.Windows.Forms.Panel()
        Me.btnICD10 = New System.Windows.Forms.Button()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.pnlbtnICD9 = New System.Windows.Forms.Panel()
        Me.btnICD9 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.trvCPTAssociation = New System.Windows.Forms.TreeView()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblOK = New System.Windows.Forms.ToolStripButton()
        Me.tblFinish = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.cntICD9Association = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.cntTags = New System.Windows.Forms.ContextMenu()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.elementHostICD10 = New System.Windows.Forms.Integration.ElementHost()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftRadioBtnTop.SuspendLayout()
        Me.pnlLeftRadioBtn.SuspendLayout()
        Me.pnltrvCPT.SuspendLayout()
        Me.pnlLeftSearch.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlFlowsheet.SuspendLayout()
        Me.pnlbtnLabOrders.SuspendLayout()
        Me.pnlbtnOrders.SuspendLayout()
        Me.pnlbtnReferrals.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltrvAssociates.SuspendLayout()
        Me.pnlbtnTags.SuspendLayout()
        Me.pnlbtnDrugs.SuspendLayout()
        Me.pnlRadiobtn.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlbtnPatientEducation.SuspendLayout()
        Me.pnlbtnMUPatientEducation.SuspendLayout()
        Me.pnlbtnICD10.SuspendLayout()
        Me.pnlbtnICD9.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.GloUC_trvCPT)
        Me.pnlLeft.Controls.Add(Me.pnlLeftRadioBtnTop)
        Me.pnlLeft.Controls.Add(Me.pnltrvCPT)
        Me.pnlLeft.Controls.Add(Me.pnlLeftSearch)
        Me.pnlLeft.Controls.Add(Me.Panel1)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(261, 584)
        Me.pnlLeft.TabIndex = 1
        '
        'GloUC_trvCPT
        '
        Me.GloUC_trvCPT.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT.CheckBoxes = False
        Me.GloUC_trvCPT.CodeMember = Nothing
        Me.GloUC_trvCPT.ColonAsSeparator = False
        Me.GloUC_trvCPT.Comment = Nothing
        Me.GloUC_trvCPT.ConceptID = Nothing
        Me.GloUC_trvCPT.CPT = Nothing
        Me.GloUC_trvCPT.mpidmember = Nothing
        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvCPT.DrugFlag = CType(16, Short)
        Me.GloUC_trvCPT.DrugFormMember = Nothing
        Me.GloUC_trvCPT.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCPT.DurationMember = Nothing
        Me.GloUC_trvCPT.EducationMappingSearchType = 1
        Me.GloUC_trvCPT.FrequencyMember = Nothing
        Me.GloUC_trvCPT.HistoryType = Nothing
        Me.GloUC_trvCPT.ICD9 = Nothing
        Me.GloUC_trvCPT.ICDRevision = Nothing
        Me.GloUC_trvCPT.ImageIndex = 0
        Me.GloUC_trvCPT.ImageList = Me.imgTreeView
        Me.GloUC_trvCPT.ImageObject = Nothing
        Me.GloUC_trvCPT.Indicator = Nothing
        Me.GloUC_trvCPT.IsCPTSearch = False
        Me.GloUC_trvCPT.IsDiagnosisSearch = False
        Me.GloUC_trvCPT.IsDrug = False
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSearchForEducationMapping = False
        Me.GloUC_trvCPT.IsSystemCategory = Nothing
        Me.GloUC_trvCPT.Location = New System.Drawing.Point(0, 61)
        Me.GloUC_trvCPT.MaximumNodes = 1000
        Me.GloUC_trvCPT.Name = "GloUC_trvCPT"
        Me.GloUC_trvCPT.NDCCodeMember = Nothing
        Me.GloUC_trvCPT.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_trvCPT.ParentImageIndex = 0
        Me.GloUC_trvCPT.ParentMember = Nothing
        Me.GloUC_trvCPT.RouteMember = Nothing
        Me.GloUC_trvCPT.RowOrderMember = Nothing
        Me.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT.SearchBox = True
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 0
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(261, 523)
        Me.GloUC_trvCPT.SmartTreatmentId = Nothing
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 45
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "CPT_01.ico")
        Me.imgTreeView.Images.SetKeyName(2, "ICD 9_01.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Drugs.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Tag.ico")
        Me.imgTreeView.Images.SetKeyName(5, "Patient Education.ico")
        Me.imgTreeView.Images.SetKeyName(6, "ICD9 Association.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(8, "CPT-ICD9 Association.ico")
        Me.imgTreeView.Images.SetKeyName(9, "CPT Association.ico")
        Me.imgTreeView.Images.SetKeyName(10, "FLow sheet.ico")
        Me.imgTreeView.Images.SetKeyName(11, "Lab orders.ico")
        Me.imgTreeView.Images.SetKeyName(12, "Radiology Orders.ico")
        Me.imgTreeView.Images.SetKeyName(13, "Refferal.ico")
        '
        'pnlLeftRadioBtnTop
        '
        Me.pnlLeftRadioBtnTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftRadioBtnTop.Controls.Add(Me.pnlLeftRadioBtn)
        Me.pnlLeftRadioBtnTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftRadioBtnTop.Location = New System.Drawing.Point(0, 31)
        Me.pnlLeftRadioBtnTop.Name = "pnlLeftRadioBtnTop"
        Me.pnlLeftRadioBtnTop.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLeftRadioBtnTop.Size = New System.Drawing.Size(261, 30)
        Me.pnlLeftRadioBtnTop.TabIndex = 46
        '
        'pnlLeftRadioBtn
        '
        Me.pnlLeftRadioBtn.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeftRadioBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftRadioBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtAll)
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtUnassociated)
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtAssociated)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label58)
        Me.pnlLeftRadioBtn.Controls.Add(Me.lbl_pnlRight)
        Me.pnlLeftRadioBtn.Controls.Add(Me.lbl_pnlTop)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label59)
        Me.pnlLeftRadioBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftRadioBtn.Location = New System.Drawing.Point(3, 0)
        Me.pnlLeftRadioBtn.Name = "pnlLeftRadioBtn"
        Me.pnlLeftRadioBtn.Size = New System.Drawing.Size(258, 27)
        Me.pnlLeftRadioBtn.TabIndex = 0
        '
        'rbtAll
        '
        Me.rbtAll.AutoSize = True
        Me.rbtAll.BackColor = System.Drawing.Color.Transparent
        Me.rbtAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtAll.Location = New System.Drawing.Point(213, 5)
        Me.rbtAll.Name = "rbtAll"
        Me.rbtAll.Size = New System.Drawing.Size(37, 18)
        Me.rbtAll.TabIndex = 2
        Me.rbtAll.TabStop = True
        Me.rbtAll.Text = "All"
        Me.rbtAll.UseVisualStyleBackColor = False
        '
        'rbtUnassociated
        '
        Me.rbtUnassociated.AutoSize = True
        Me.rbtUnassociated.BackColor = System.Drawing.Color.Transparent
        Me.rbtUnassociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtUnassociated.Location = New System.Drawing.Point(102, 5)
        Me.rbtUnassociated.Name = "rbtUnassociated"
        Me.rbtUnassociated.Size = New System.Drawing.Size(96, 18)
        Me.rbtUnassociated.TabIndex = 1
        Me.rbtUnassociated.TabStop = True
        Me.rbtUnassociated.Text = "Unassociated"
        Me.rbtUnassociated.UseVisualStyleBackColor = False
        '
        'rbtAssociated
        '
        Me.rbtAssociated.AutoSize = True
        Me.rbtAssociated.BackColor = System.Drawing.Color.Transparent
        Me.rbtAssociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtAssociated.Location = New System.Drawing.Point(7, 5)
        Me.rbtAssociated.Name = "rbtAssociated"
        Me.rbtAssociated.Size = New System.Drawing.Size(83, 18)
        Me.rbtAssociated.TabIndex = 0
        Me.rbtAssociated.TabStop = True
        Me.rbtAssociated.Text = "Associated"
        Me.rbtAssociated.UseVisualStyleBackColor = False
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label58.Location = New System.Drawing.Point(0, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 25)
        Me.Label58.TabIndex = 20
        Me.Label58.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(257, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 25)
        Me.lbl_pnlRight.TabIndex = 19
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(258, 1)
        Me.lbl_pnlTop.TabIndex = 4
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label59.Location = New System.Drawing.Point(0, 26)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(258, 1)
        Me.Label59.TabIndex = 5
        Me.Label59.Text = "label1"
        '
        'pnltrvCPT
        '
        Me.pnltrvCPT.Controls.Add(Me.Label53)
        Me.pnltrvCPT.Controls.Add(Me.Label52)
        Me.pnltrvCPT.Controls.Add(Me.Label6)
        Me.pnltrvCPT.Controls.Add(Me.Label7)
        Me.pnltrvCPT.Controls.Add(Me.Label8)
        Me.pnltrvCPT.Controls.Add(Me.Label23)
        Me.pnltrvCPT.Location = New System.Drawing.Point(116, 166)
        Me.pnltrvCPT.Name = "pnltrvCPT"
        Me.pnltrvCPT.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvCPT.Size = New System.Drawing.Size(91, 150)
        Me.pnltrvCPT.TabIndex = 2
        Me.pnltrvCPT.Visible = False
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.White
        Me.Label53.Location = New System.Drawing.Point(7, 1)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(232, 4)
        Me.Label53.TabIndex = 43
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.White
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label52.Location = New System.Drawing.Point(4, 0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(3, 146)
        Me.Label52.TabIndex = 42
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 1)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 147)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(90, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 147)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(3, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(237, 1)
        Me.Label23.TabIndex = 9
        Me.Label23.Text = "label1"
        '
        'pnlLeftSearch
        '
        Me.pnlLeftSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLeftSearch.Controls.Add(Me.txtsearchCPT)
        Me.pnlLeftSearch.Controls.Add(Me.Label11)
        Me.pnlLeftSearch.Controls.Add(Me.Label13)
        Me.pnlLeftSearch.Controls.Add(Me.PictureBox2)
        Me.pnlLeftSearch.Controls.Add(Me.Label14)
        Me.pnlLeftSearch.Controls.Add(Me.Label15)
        Me.pnlLeftSearch.Controls.Add(Me.Label24)
        Me.pnlLeftSearch.Controls.Add(Me.Label25)
        Me.pnlLeftSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLeftSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlLeftSearch.Location = New System.Drawing.Point(62, 107)
        Me.pnlLeftSearch.Name = "pnlLeftSearch"
        Me.pnlLeftSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLeftSearch.Size = New System.Drawing.Size(71, 28)
        Me.pnlLeftSearch.TabIndex = 1
        Me.pnlLeftSearch.Visible = False
        '
        'txtsearchCPT
        '
        Me.txtsearchCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchCPT.ForeColor = System.Drawing.Color.Black
        Me.txtsearchCPT.Location = New System.Drawing.Point(32, 6)
        Me.txtsearchCPT.Multiline = True
        Me.txtsearchCPT.Name = "txtsearchCPT"
        Me.txtsearchCPT.Size = New System.Drawing.Size(206, 16)
        Me.txtsearchCPT.TabIndex = 0
        Me.txtsearchCPT.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(32, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 4)
        Me.Label11.TabIndex = 37
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(32, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 2)
        Me.Label13.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 23)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(4, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 1)
        Me.Label14.TabIndex = 42
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 24)
        Me.Label15.TabIndex = 41
        Me.Label15.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(70, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 24)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(68, 1)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Panel1.Size = New System.Drawing.Size(261, 31)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label55)
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.Label27)
        Me.Panel2.Controls.Add(Me.Label28)
        Me.Panel2.Controls.Add(Me.Label29)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(257, 25)
        Me.Panel2.TabIndex = 0
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.Transparent
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(1, 1)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(255, 23)
        Me.Label55.TabIndex = 13
        Me.Label55.Text = "CPT"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(1, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(255, 1)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 24)
        Me.Label27.TabIndex = 11
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(256, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 24)
        Me.Label28.TabIndex = 10
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(257, 1)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "label1"
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlRight.Controls.Add(Me.Panel5)
        Me.pnlRight.Controls.Add(Me.pnlFlowsheet)
        Me.pnlRight.Controls.Add(Me.pnlbtnLabOrders)
        Me.pnlRight.Controls.Add(Me.pnlbtnOrders)
        Me.pnlRight.Controls.Add(Me.pnlbtnReferrals)
        Me.pnlRight.Controls.Add(Me.pnlSearch)
        Me.pnlRight.Controls.Add(Me.pnltrvAssociates)
        Me.pnlRight.Controls.Add(Me.pnlbtnTags)
        Me.pnlRight.Controls.Add(Me.pnlbtnDrugs)
        Me.pnlRight.Controls.Add(Me.pnlRadiobtn)
        Me.pnlRight.Controls.Add(Me.pnlbtnPatientEducation)
        Me.pnlRight.Controls.Add(Me.pnlbtnMUPatientEducation)
        Me.pnlRight.Controls.Add(Me.pnlbtnICD10)
        Me.pnlRight.Controls.Add(Me.pnlbtnICD9)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(767, 54)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.pnlRight.Size = New System.Drawing.Size(261, 584)
        Me.pnlRight.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.elementHostICD10)
        Me.Panel5.Controls.Add(Me.GloUC_trvAssociates)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(261, 302)
        Me.Panel5.TabIndex = 42
        '
        'GloUC_trvAssociates
        '
        Me.GloUC_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAssociates.CheckBoxes = False
        Me.GloUC_trvAssociates.CodeMember = Nothing
        Me.GloUC_trvAssociates.ColonAsSeparator = False
        Me.GloUC_trvAssociates.Comment = Nothing
        Me.GloUC_trvAssociates.ConceptID = Nothing
        Me.GloUC_trvAssociates.CPT = Nothing
        Me.GloUC_trvAssociates.mpidmember = Nothing
        Me.GloUC_trvAssociates.DescriptionMember = Nothing
        Me.GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAssociates.DrugFlag = CType(16, Short)
        Me.GloUC_trvAssociates.DrugFormMember = Nothing
        Me.GloUC_trvAssociates.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAssociates.DurationMember = Nothing
        Me.GloUC_trvAssociates.EducationMappingSearchType = 1
        Me.GloUC_trvAssociates.FrequencyMember = Nothing
        Me.GloUC_trvAssociates.HistoryType = Nothing
        Me.GloUC_trvAssociates.ICD9 = Nothing
        Me.GloUC_trvAssociates.ICDRevision = Nothing
        Me.GloUC_trvAssociates.ImageIndex = 0
        Me.GloUC_trvAssociates.ImageList = Me.imgTreeView
        Me.GloUC_trvAssociates.ImageObject = Nothing
        Me.GloUC_trvAssociates.Indicator = Nothing
        Me.GloUC_trvAssociates.IsCPTSearch = False
        Me.GloUC_trvAssociates.IsDiagnosisSearch = False
        Me.GloUC_trvAssociates.IsDrug = False
        Me.GloUC_trvAssociates.IsNarcoticsMember = Nothing
        Me.GloUC_trvAssociates.IsSearchForEducationMapping = False
        Me.GloUC_trvAssociates.IsSystemCategory = Nothing
        Me.GloUC_trvAssociates.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvAssociates.MaximumNodes = 1000
        Me.GloUC_trvAssociates.Name = "GloUC_trvAssociates"
        Me.GloUC_trvAssociates.NDCCodeMember = Nothing
        Me.GloUC_trvAssociates.ParentImageIndex = 0
        Me.GloUC_trvAssociates.ParentMember = Nothing
        Me.GloUC_trvAssociates.RouteMember = Nothing
        Me.GloUC_trvAssociates.RowOrderMember = Nothing
        Me.GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvAssociates.SearchBox = True
        Me.GloUC_trvAssociates.SearchText = Nothing
        Me.GloUC_trvAssociates.SelectedImageIndex = 0
        Me.GloUC_trvAssociates.SelectedNode = Nothing
        Me.GloUC_trvAssociates.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAssociates.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvAssociates.SelectedParentImageIndex = 0
        Me.GloUC_trvAssociates.Size = New System.Drawing.Size(258, 299)
        Me.GloUC_trvAssociates.SmartTreatmentId = Nothing
        Me.GloUC_trvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAssociates.TabIndex = 44
        Me.GloUC_trvAssociates.Tag = Nothing
        Me.GloUC_trvAssociates.UnitMember = Nothing
        Me.GloUC_trvAssociates.ValueMember = Nothing
        '
        'pnlFlowsheet
        '
        Me.pnlFlowsheet.Controls.Add(Me.btnFlowsheet)
        Me.pnlFlowsheet.Controls.Add(Me.Label57)
        Me.pnlFlowsheet.Controls.Add(Me.Label60)
        Me.pnlFlowsheet.Controls.Add(Me.Label73)
        Me.pnlFlowsheet.Controls.Add(Me.Label74)
        Me.pnlFlowsheet.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFlowsheet.Location = New System.Drawing.Point(0, 332)
        Me.pnlFlowsheet.Name = "pnlFlowsheet"
        Me.pnlFlowsheet.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlFlowsheet.Size = New System.Drawing.Size(261, 28)
        Me.pnlFlowsheet.TabIndex = 51
        '
        'btnFlowsheet
        '
        Me.btnFlowsheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnFlowsheet.BackgroundImage = CType(resources.GetObject("btnFlowsheet.BackgroundImage"), System.Drawing.Image)
        Me.btnFlowsheet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFlowsheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFlowsheet.FlatAppearance.BorderSize = 0
        Me.btnFlowsheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFlowsheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFlowsheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnFlowsheet.Location = New System.Drawing.Point(1, 1)
        Me.btnFlowsheet.Name = "btnFlowsheet"
        Me.btnFlowsheet.Size = New System.Drawing.Size(256, 23)
        Me.btnFlowsheet.TabIndex = 0
        Me.btnFlowsheet.Tag = "UnSelected"
        Me.btnFlowsheet.Text = "&Flowsheet"
        Me.btnFlowsheet.UseVisualStyleBackColor = False
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(1, 24)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(256, 1)
        Me.Label57.TabIndex = 12
        Me.Label57.Text = "label2"
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(0, 1)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(1, 24)
        Me.Label60.TabIndex = 11
        Me.Label60.Text = "label4"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label73.Location = New System.Drawing.Point(257, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1, 24)
        Me.Label73.TabIndex = 10
        Me.Label73.Text = "label3"
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(0, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(258, 1)
        Me.Label74.TabIndex = 9
        Me.Label74.Text = "label1"
        '
        'pnlbtnLabOrders
        '
        Me.pnlbtnLabOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnLabOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnLabOrders.Controls.Add(Me.btnLabOrders)
        Me.pnlbtnLabOrders.Controls.Add(Me.Label68)
        Me.pnlbtnLabOrders.Controls.Add(Me.Label69)
        Me.pnlbtnLabOrders.Controls.Add(Me.Label70)
        Me.pnlbtnLabOrders.Controls.Add(Me.Label71)
        Me.pnlbtnLabOrders.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnLabOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnLabOrders.Location = New System.Drawing.Point(0, 360)
        Me.pnlbtnLabOrders.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnLabOrders.Name = "pnlbtnLabOrders"
        Me.pnlbtnLabOrders.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLabOrders.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnLabOrders.TabIndex = 50
        '
        'btnLabOrders
        '
        Me.btnLabOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLabOrders.BackgroundImage = CType(resources.GetObject("btnLabOrders.BackgroundImage"), System.Drawing.Image)
        Me.btnLabOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabOrders.FlatAppearance.BorderSize = 0
        Me.btnLabOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabOrders.Location = New System.Drawing.Point(1, 1)
        Me.btnLabOrders.Name = "btnLabOrders"
        Me.btnLabOrders.Size = New System.Drawing.Size(256, 23)
        Me.btnLabOrders.TabIndex = 0
        Me.btnLabOrders.Tag = "UnSelected"
        Me.btnLabOrders.Text = "&Orders && Results"
        Me.btnLabOrders.UseVisualStyleBackColor = False
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label68.Location = New System.Drawing.Point(1, 24)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(256, 1)
        Me.Label68.TabIndex = 8
        Me.Label68.Text = "label2"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(0, 1)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 24)
        Me.Label69.TabIndex = 7
        Me.Label69.Text = "label4"
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label70.Location = New System.Drawing.Point(257, 1)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(1, 24)
        Me.Label70.TabIndex = 6
        Me.Label70.Text = "label3"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(0, 0)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(258, 1)
        Me.Label71.TabIndex = 5
        Me.Label71.Text = "label1"
        '
        'pnlbtnOrders
        '
        Me.pnlbtnOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnOrders.Controls.Add(Me.btnOrders)
        Me.pnlbtnOrders.Controls.Add(Me.Label64)
        Me.pnlbtnOrders.Controls.Add(Me.Label65)
        Me.pnlbtnOrders.Controls.Add(Me.Label66)
        Me.pnlbtnOrders.Controls.Add(Me.Label67)
        Me.pnlbtnOrders.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnOrders.Location = New System.Drawing.Point(0, 388)
        Me.pnlbtnOrders.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnOrders.Name = "pnlbtnOrders"
        Me.pnlbtnOrders.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnOrders.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnOrders.TabIndex = 49
        '
        'btnOrders
        '
        Me.btnOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnOrders.BackgroundImage = CType(resources.GetObject("btnOrders.BackgroundImage"), System.Drawing.Image)
        Me.btnOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOrders.FlatAppearance.BorderSize = 0
        Me.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOrders.Location = New System.Drawing.Point(1, 1)
        Me.btnOrders.Name = "btnOrders"
        Me.btnOrders.Size = New System.Drawing.Size(256, 23)
        Me.btnOrders.TabIndex = 0
        Me.btnOrders.Tag = "UnSelected"
        Me.btnOrders.Text = "&Order Templates"
        Me.btnOrders.UseVisualStyleBackColor = False
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label64.Location = New System.Drawing.Point(1, 24)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(256, 1)
        Me.Label64.TabIndex = 8
        Me.Label64.Text = "label2"
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(0, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 24)
        Me.Label65.TabIndex = 7
        Me.Label65.Text = "label4"
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label66.Location = New System.Drawing.Point(257, 1)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(1, 24)
        Me.Label66.TabIndex = 6
        Me.Label66.Text = "label3"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(0, 0)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(258, 1)
        Me.Label67.TabIndex = 5
        Me.Label67.Text = "label1"
        '
        'pnlbtnReferrals
        '
        Me.pnlbtnReferrals.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnReferrals.Controls.Add(Me.btnReferrals)
        Me.pnlbtnReferrals.Controls.Add(Me.Label61)
        Me.pnlbtnReferrals.Controls.Add(Me.Label62)
        Me.pnlbtnReferrals.Controls.Add(Me.Label63)
        Me.pnlbtnReferrals.Controls.Add(Me.Label72)
        Me.pnlbtnReferrals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnReferrals.Location = New System.Drawing.Point(0, 416)
        Me.pnlbtnReferrals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnReferrals.Name = "pnlbtnReferrals"
        Me.pnlbtnReferrals.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnReferrals.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnReferrals.TabIndex = 48
        '
        'btnReferrals
        '
        Me.btnReferrals.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnReferrals.BackgroundImage = CType(resources.GetObject("btnReferrals.BackgroundImage"), System.Drawing.Image)
        Me.btnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReferrals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReferrals.FlatAppearance.BorderSize = 0
        Me.btnReferrals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReferrals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnReferrals.Location = New System.Drawing.Point(1, 1)
        Me.btnReferrals.Name = "btnReferrals"
        Me.btnReferrals.Size = New System.Drawing.Size(256, 23)
        Me.btnReferrals.TabIndex = 0
        Me.btnReferrals.Tag = "UnSelected"
        Me.btnReferrals.Text = "&Referral Letter"
        Me.btnReferrals.UseVisualStyleBackColor = False
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label61.Location = New System.Drawing.Point(1, 24)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(256, 1)
        Me.Label61.TabIndex = 8
        Me.Label61.Text = "label2"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(0, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 24)
        Me.Label62.TabIndex = 7
        Me.Label62.Text = "label4"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(257, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 24)
        Me.Label63.TabIndex = 6
        Me.Label63.Text = "label3"
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(0, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(258, 1)
        Me.Label72.TabIndex = 5
        Me.Label72.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtsearchAssociates)
        Me.pnlSearch.Controls.Add(Me.Label20)
        Me.pnlSearch.Controls.Add(Me.Label21)
        Me.pnlSearch.Controls.Add(Me.PictureBox1)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Controls.Add(Me.Label30)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(143, 28)
        Me.pnlSearch.TabIndex = 1
        Me.pnlSearch.Visible = False
        '
        'txtsearchAssociates
        '
        Me.txtsearchAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchAssociates.ForeColor = System.Drawing.Color.Black
        Me.txtsearchAssociates.Location = New System.Drawing.Point(30, 6)
        Me.txtsearchAssociates.Multiline = True
        Me.txtsearchAssociates.Name = "txtsearchAssociates"
        Me.txtsearchAssociates.Size = New System.Drawing.Size(206, 16)
        Me.txtsearchAssociates.TabIndex = 0
        Me.txtsearchAssociates.Visible = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(30, 2)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(109, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(30, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(109, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(2, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(137, 1)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 2)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 23)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(139, 2)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(1, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(139, 1)
        Me.Label30.TabIndex = 39
        Me.Label30.Text = "label1"
        '
        'pnltrvAssociates
        '
        Me.pnltrvAssociates.Controls.Add(Me.trvAssociates)
        Me.pnltrvAssociates.Controls.Add(Me.Label54)
        Me.pnltrvAssociates.Controls.Add(Me.Label51)
        Me.pnltrvAssociates.Controls.Add(Me.Label22)
        Me.pnltrvAssociates.Controls.Add(Me.Label34)
        Me.pnltrvAssociates.Controls.Add(Me.Label35)
        Me.pnltrvAssociates.Controls.Add(Me.Label36)
        Me.pnltrvAssociates.Location = New System.Drawing.Point(0, 0)
        Me.pnltrvAssociates.Name = "pnltrvAssociates"
        Me.pnltrvAssociates.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnltrvAssociates.Size = New System.Drawing.Size(115, 123)
        Me.pnltrvAssociates.TabIndex = 2
        Me.pnltrvAssociates.Visible = False
        '
        'trvAssociates
        '
        Me.trvAssociates.BackColor = System.Drawing.Color.White
        Me.trvAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAssociates.ForeColor = System.Drawing.Color.Black
        Me.trvAssociates.HideSelection = False
        Me.trvAssociates.ImageIndex = 7
        Me.trvAssociates.ImageList = Me.imgTreeView
        Me.trvAssociates.Indent = 21
        Me.trvAssociates.ItemHeight = 20
        Me.trvAssociates.Location = New System.Drawing.Point(5, 6)
        Me.trvAssociates.Name = "trvAssociates"
        Me.trvAssociates.SelectedImageIndex = 7
        Me.trvAssociates.ShowLines = False
        Me.trvAssociates.Size = New System.Drawing.Size(231, 402)
        Me.trvAssociates.TabIndex = 0
        Me.trvAssociates.Visible = False
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.White
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label54.Location = New System.Drawing.Point(5, 2)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(106, 4)
        Me.Label54.TabIndex = 43
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.White
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label51.Location = New System.Drawing.Point(2, 2)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(3, 117)
        Me.Label51.TabIndex = 42
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(2, 119)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(109, 1)
        Me.Label22.TabIndex = 12
        Me.Label22.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(1, 2)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 118)
        Me.Label34.TabIndex = 11
        Me.Label34.Text = "label4"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label35.Location = New System.Drawing.Point(111, 2)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 118)
        Me.Label35.TabIndex = 10
        Me.Label35.Text = "label3"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(1, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(111, 1)
        Me.Label36.TabIndex = 9
        Me.Label36.Text = "label1"
        '
        'pnlbtnTags
        '
        Me.pnlbtnTags.Controls.Add(Me.btnTags)
        Me.pnlbtnTags.Controls.Add(Me.Label41)
        Me.pnlbtnTags.Controls.Add(Me.Label42)
        Me.pnlbtnTags.Controls.Add(Me.Label43)
        Me.pnlbtnTags.Controls.Add(Me.Label44)
        Me.pnlbtnTags.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnTags.Location = New System.Drawing.Point(0, 444)
        Me.pnlbtnTags.Name = "pnlbtnTags"
        Me.pnlbtnTags.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlbtnTags.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnTags.TabIndex = 3
        '
        'btnTags
        '
        Me.btnTags.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnTags.BackgroundImage = CType(resources.GetObject("btnTags.BackgroundImage"), System.Drawing.Image)
        Me.btnTags.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTags.FlatAppearance.BorderSize = 0
        Me.btnTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTags.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTags.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTags.Location = New System.Drawing.Point(2, 2)
        Me.btnTags.Name = "btnTags"
        Me.btnTags.Size = New System.Drawing.Size(255, 22)
        Me.btnTags.TabIndex = 0
        Me.btnTags.Tag = "UnSelected"
        Me.btnTags.Text = "&Tags"
        Me.btnTags.UseVisualStyleBackColor = False
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(2, 24)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(255, 1)
        Me.Label41.TabIndex = 12
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(1, 2)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 23)
        Me.Label42.TabIndex = 11
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(257, 2)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 23)
        Me.Label43.TabIndex = 10
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(1, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(257, 1)
        Me.Label44.TabIndex = 9
        Me.Label44.Text = "label1"
        '
        'pnlbtnDrugs
        '
        Me.pnlbtnDrugs.Controls.Add(Me.btnDrugs)
        Me.pnlbtnDrugs.Controls.Add(Me.Label37)
        Me.pnlbtnDrugs.Controls.Add(Me.Label38)
        Me.pnlbtnDrugs.Controls.Add(Me.Label39)
        Me.pnlbtnDrugs.Controls.Add(Me.Label40)
        Me.pnlbtnDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnDrugs.Location = New System.Drawing.Point(0, 472)
        Me.pnlbtnDrugs.Name = "pnlbtnDrugs"
        Me.pnlbtnDrugs.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlbtnDrugs.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnDrugs.TabIndex = 4
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDrugs.BackgroundImage = CType(resources.GetObject("btnDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDrugs.FlatAppearance.BorderSize = 0
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugs.Location = New System.Drawing.Point(2, 2)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(255, 22)
        Me.btnDrugs.TabIndex = 0
        Me.btnDrugs.Tag = "UnSelected"
        Me.btnDrugs.Text = "&Drugs"
        Me.btnDrugs.UseVisualStyleBackColor = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(2, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(255, 1)
        Me.Label37.TabIndex = 12
        Me.Label37.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(1, 2)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 23)
        Me.Label38.TabIndex = 11
        Me.Label38.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(257, 2)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 23)
        Me.Label39.TabIndex = 10
        Me.Label39.Text = "label3"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(1, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(257, 1)
        Me.Label40.TabIndex = 9
        Me.Label40.Text = "label1"
        '
        'pnlRadiobtn
        '
        Me.pnlRadiobtn.Controls.Add(Me.Panel4)
        Me.pnlRadiobtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadiobtn.Name = "pnlRadiobtn"
        Me.pnlRadiobtn.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlRadiobtn.Size = New System.Drawing.Size(240, 30)
        Me.pnlRadiobtn.TabIndex = 0
        Me.pnlRadiobtn.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.rbCodesearch)
        Me.Panel4.Controls.Add(Me.Label56)
        Me.Panel4.Controls.Add(Me.rbDescsearch)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label31)
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.Label33)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(1, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(236, 26)
        Me.Panel4.TabIndex = 8
        Me.Panel4.Visible = False
        '
        'rbCodesearch
        '
        Me.rbCodesearch.BackColor = System.Drawing.Color.Transparent
        Me.rbCodesearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCodesearch.Location = New System.Drawing.Point(11, 1)
        Me.rbCodesearch.Name = "rbCodesearch"
        Me.rbCodesearch.Size = New System.Drawing.Size(102, 24)
        Me.rbCodesearch.TabIndex = 2
        Me.rbCodesearch.Text = "ICD9 Code"
        Me.rbCodesearch.UseVisualStyleBackColor = False
        Me.rbCodesearch.Visible = False
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.Transparent
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(1, 1)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(10, 24)
        Me.Label56.TabIndex = 14
        '
        'rbDescsearch
        '
        Me.rbDescsearch.BackColor = System.Drawing.Color.Transparent
        Me.rbDescsearch.Checked = True
        Me.rbDescsearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbDescsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDescsearch.Location = New System.Drawing.Point(139, 1)
        Me.rbDescsearch.Name = "rbDescsearch"
        Me.rbDescsearch.Size = New System.Drawing.Size(96, 24)
        Me.rbDescsearch.TabIndex = 3
        Me.rbDescsearch.TabStop = True
        Me.rbDescsearch.Text = "Description"
        Me.rbDescsearch.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(234, 1)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label2"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 25)
        Me.Label31.TabIndex = 11
        Me.Label31.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(235, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 25)
        Me.Label32.TabIndex = 10
        Me.Label32.Text = "label3"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(236, 1)
        Me.Label33.TabIndex = 9
        Me.Label33.Text = "label1"
        '
        'pnlbtnPatientEducation
        '
        Me.pnlbtnPatientEducation.Controls.Add(Me.btnPatientEducation)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label45)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label46)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label47)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label48)
        Me.pnlbtnPatientEducation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnPatientEducation.Location = New System.Drawing.Point(0, 500)
        Me.pnlbtnPatientEducation.Name = "pnlbtnPatientEducation"
        Me.pnlbtnPatientEducation.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlbtnPatientEducation.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnPatientEducation.TabIndex = 5
        '
        'btnPatientEducation
        '
        Me.btnPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnPatientEducation.BackgroundImage = CType(resources.GetObject("btnPatientEducation.BackgroundImage"), System.Drawing.Image)
        Me.btnPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPatientEducation.FlatAppearance.BorderSize = 0
        Me.btnPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPatientEducation.Location = New System.Drawing.Point(2, 2)
        Me.btnPatientEducation.Name = "btnPatientEducation"
        Me.btnPatientEducation.Size = New System.Drawing.Size(255, 22)
        Me.btnPatientEducation.TabIndex = 0
        Me.btnPatientEducation.Tag = "UnSelected"
        Me.btnPatientEducation.Text = "&Patient Education"
        Me.btnPatientEducation.UseVisualStyleBackColor = False
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(2, 24)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(255, 1)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(1, 2)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 23)
        Me.Label46.TabIndex = 11
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(257, 2)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 23)
        Me.Label47.TabIndex = 10
        Me.Label47.Text = "label3"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(1, 1)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(257, 1)
        Me.Label48.TabIndex = 9
        Me.Label48.Text = "label1"
        '
        'pnlbtnMUPatientEducation
        '
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.btnMUPatientEducation)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label79)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label80)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label81)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label82)
        Me.pnlbtnMUPatientEducation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnMUPatientEducation.Location = New System.Drawing.Point(0, 528)
        Me.pnlbtnMUPatientEducation.Name = "pnlbtnMUPatientEducation"
        Me.pnlbtnMUPatientEducation.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlbtnMUPatientEducation.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnMUPatientEducation.TabIndex = 53
        '
        'btnMUPatientEducation
        '
        Me.btnMUPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnMUPatientEducation.BackgroundImage = CType(resources.GetObject("btnMUPatientEducation.BackgroundImage"), System.Drawing.Image)
        Me.btnMUPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMUPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMUPatientEducation.FlatAppearance.BorderSize = 0
        Me.btnMUPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMUPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMUPatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMUPatientEducation.Location = New System.Drawing.Point(2, 2)
        Me.btnMUPatientEducation.Name = "btnMUPatientEducation"
        Me.btnMUPatientEducation.Size = New System.Drawing.Size(255, 22)
        Me.btnMUPatientEducation.TabIndex = 0
        Me.btnMUPatientEducation.Tag = "UnSelected"
        Me.btnMUPatientEducation.Text = "&MU Patient Education"
        Me.btnMUPatientEducation.UseVisualStyleBackColor = False
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label79.Location = New System.Drawing.Point(2, 24)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(255, 1)
        Me.Label79.TabIndex = 12
        Me.Label79.Text = "label2"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(1, 2)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 23)
        Me.Label80.TabIndex = 11
        Me.Label80.Text = "label4"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label81.Location = New System.Drawing.Point(257, 2)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 23)
        Me.Label81.TabIndex = 10
        Me.Label81.Text = "label3"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(1, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(257, 1)
        Me.Label82.TabIndex = 9
        Me.Label82.Text = "label1"
        '
        'pnlbtnICD10
        '
        Me.pnlbtnICD10.Controls.Add(Me.btnICD10)
        Me.pnlbtnICD10.Controls.Add(Me.Label75)
        Me.pnlbtnICD10.Controls.Add(Me.Label76)
        Me.pnlbtnICD10.Controls.Add(Me.Label77)
        Me.pnlbtnICD10.Controls.Add(Me.Label78)
        Me.pnlbtnICD10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnICD10.Location = New System.Drawing.Point(0, 556)
        Me.pnlbtnICD10.Name = "pnlbtnICD10"
        Me.pnlbtnICD10.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlbtnICD10.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnICD10.TabIndex = 52
        '
        'btnICD10
        '
        Me.btnICD10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.btnICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD10.FlatAppearance.BorderSize = 0
        Me.btnICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD10.Location = New System.Drawing.Point(2, 2)
        Me.btnICD10.Name = "btnICD10"
        Me.btnICD10.Size = New System.Drawing.Size(255, 22)
        Me.btnICD10.TabIndex = 0
        Me.btnICD10.Tag = "UnSelected"
        Me.btnICD10.Text = "&ICD10"
        Me.btnICD10.UseVisualStyleBackColor = False
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label75.Location = New System.Drawing.Point(2, 24)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(255, 1)
        Me.Label75.TabIndex = 12
        Me.Label75.Text = "label2"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(1, 2)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 23)
        Me.Label76.TabIndex = 11
        Me.Label76.Text = "label4"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label77.Location = New System.Drawing.Point(257, 2)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(1, 23)
        Me.Label77.TabIndex = 10
        Me.Label77.Text = "label3"
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(1, 1)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(257, 1)
        Me.Label78.TabIndex = 9
        Me.Label78.Text = "label1"
        '
        'pnlbtnICD9
        '
        Me.pnlbtnICD9.Controls.Add(Me.btnICD9)
        Me.pnlbtnICD9.Controls.Add(Me.Label1)
        Me.pnlbtnICD9.Controls.Add(Me.Label3)
        Me.pnlbtnICD9.Controls.Add(Me.Label4)
        Me.pnlbtnICD9.Controls.Add(Me.Label5)
        Me.pnlbtnICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnICD9.Location = New System.Drawing.Point(0, 2)
        Me.pnlbtnICD9.Name = "pnlbtnICD9"
        Me.pnlbtnICD9.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlbtnICD9.Size = New System.Drawing.Size(261, 28)
        Me.pnlbtnICD9.TabIndex = 6
        '
        'btnICD9
        '
        Me.btnICD9.BackColor = System.Drawing.Color.Transparent
        Me.btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD9.FlatAppearance.BorderSize = 0
        Me.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD9.Location = New System.Drawing.Point(2, 2)
        Me.btnICD9.Name = "btnICD9"
        Me.btnICD9.Size = New System.Drawing.Size(255, 22)
        Me.btnICD9.TabIndex = 0
        Me.btnICD9.Tag = "Selected"
        Me.btnICD9.Text = "&ICD9"
        Me.btnICD9.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(2, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(255, 1)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(257, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(257, 1)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "label1"
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMiddle.Controls.Add(Me.trvCPTAssociation)
        Me.pnlMiddle.Controls.Add(Me.Label50)
        Me.pnlMiddle.Controls.Add(Me.Label49)
        Me.pnlMiddle.Controls.Add(Me.Label19)
        Me.pnlMiddle.Controls.Add(Me.Label18)
        Me.pnlMiddle.Controls.Add(Me.Label17)
        Me.pnlMiddle.Controls.Add(Me.Label16)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(264, 54)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Padding = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.pnlMiddle.Size = New System.Drawing.Size(500, 584)
        Me.pnlMiddle.TabIndex = 2
        '
        'trvCPTAssociation
        '
        Me.trvCPTAssociation.BackColor = System.Drawing.Color.White
        Me.trvCPTAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPTAssociation.CheckBoxes = True
        Me.trvCPTAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCPTAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCPTAssociation.ForeColor = System.Drawing.Color.Black
        Me.trvCPTAssociation.HideSelection = False
        Me.trvCPTAssociation.ImageIndex = 7
        Me.trvCPTAssociation.ImageList = Me.imgTreeView
        Me.trvCPTAssociation.Indent = 21
        Me.trvCPTAssociation.ItemHeight = 20
        Me.trvCPTAssociation.Location = New System.Drawing.Point(4, 8)
        Me.trvCPTAssociation.Name = "trvCPTAssociation"
        Me.trvCPTAssociation.SelectedImageIndex = 7
        Me.trvCPTAssociation.ShowLines = False
        Me.trvCPTAssociation.Size = New System.Drawing.Size(494, 572)
        Me.trvCPTAssociation.TabIndex = 0
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.White
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label50.Location = New System.Drawing.Point(2, 8)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(2, 572)
        Me.Label50.TabIndex = 41
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.White
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Location = New System.Drawing.Point(2, 4)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(496, 4)
        Me.Label49.TabIndex = 40
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Location = New System.Drawing.Point(1, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 576)
        Me.Label19.TabIndex = 39
        Me.Label19.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(498, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 576)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(1, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(498, 1)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Location = New System.Drawing.Point(1, 580)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(498, 1)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "label1"
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblRefresh, Me.tblOK, Me.tblFinish, Me.ts_gloCommunityDownload, Me.tsbRefresh, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(1028, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblRefresh
        '
        Me.tblRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRefresh.Image = CType(resources.GetObject("tblRefresh.Image"), System.Drawing.Image)
        Me.tblRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblRefresh.Name = "tblRefresh"
        Me.tblRefresh.Size = New System.Drawing.Size(45, 50)
        Me.tblRefresh.Text = " &New "
        Me.tblRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblRefresh.Visible = False
        '
        'tblOK
        '
        Me.tblOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblOK.Image = CType(resources.GetObject("tblOK.Image"), System.Drawing.Image)
        Me.tblOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblOK.Name = "tblOK"
        Me.tblOK.Size = New System.Drawing.Size(66, 50)
        Me.tblOK.Text = "&Save&&Cls"
        Me.tblOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblOK.ToolTipText = "Save and Close"
        '
        'tblFinish
        '
        Me.tblFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblFinish.Image = CType(resources.GetObject("tblFinish.Image"), System.Drawing.Image)
        Me.tblFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblFinish.Name = "tblFinish"
        Me.tblFinish.Size = New System.Drawing.Size(53, 50)
        Me.tblFinish.Text = " &Finish "
        Me.tblFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblFinish.Visible = False
        '
        'ts_gloCommunityDownload
        '
        Me.ts_gloCommunityDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_gloCommunityDownload.Image = CType(resources.GetObject("ts_gloCommunityDownload.Image"), System.Drawing.Image)
        Me.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload"
        Me.ts_gloCommunityDownload.Size = New System.Drawing.Size(73, 50)
        Me.ts_gloCommunityDownload.Tag = "gloCommunityDownload"
        Me.ts_gloCommunityDownload.Text = "Download"
        Me.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_gloCommunityDownload.ToolTipText = "Download from gloCommunity"
        Me.ts_gloCommunityDownload.Visible = False
        '
        'tsbRefresh
        '
        Me.tsbRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbRefresh.Image = CType(resources.GetObject("tsbRefresh.Image"), System.Drawing.Image)
        Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRefresh.Name = "tsbRefresh"
        Me.tsbRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tsbRefresh.Text = "Re&fresh"
        Me.tsbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbRefresh.Visible = False
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(51, 50)
        Me.tblClose.Text = " &Close "
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cntICD9Association
        '
        Me.cntICD9Association.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteICD9Item})
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 0
        Me.mnuDeleteICD9Item.Text = "Remove  Associate"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblMedication)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1028, 54)
        Me.pnlToolStrip.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(261, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 584)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(764, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 584)
        Me.Splitter2.TabIndex = 7
        Me.Splitter2.TabStop = False
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.Text = "Remove Item"
        '
        'cntTags
        '
        Me.cntTags.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Remove Item"
        '
        'elementHostICD10
        '
        Me.elementHostICD10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHostICD10.Location = New System.Drawing.Point(0, 0)
        Me.elementHostICD10.Name = "elementHostICD10"
        Me.elementHostICD10.Size = New System.Drawing.Size(258, 299)
        Me.elementHostICD10.TabIndex = 45
        Me.elementHostICD10.Visible = False
        Me.elementHostICD10.Child = Nothing
        '
        'frmCPTICD9Association
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 638)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCPTICD9Association"
        Me.ShowInTaskbar = False
        Me.Text = "Smart Treatment"
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftRadioBtnTop.ResumeLayout(False)
        Me.pnlLeftRadioBtn.ResumeLayout(False)
        Me.pnlLeftRadioBtn.PerformLayout()
        Me.pnltrvCPT.ResumeLayout(False)
        Me.pnlLeftSearch.ResumeLayout(False)
        Me.pnlLeftSearch.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlFlowsheet.ResumeLayout(False)
        Me.pnlbtnLabOrders.ResumeLayout(False)
        Me.pnlbtnOrders.ResumeLayout(False)
        Me.pnlbtnReferrals.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltrvAssociates.ResumeLayout(False)
        Me.pnlbtnTags.ResumeLayout(False)
        Me.pnlbtnDrugs.ResumeLayout(False)
        Me.pnlRadiobtn.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlbtnPatientEducation.ResumeLayout(False)
        Me.pnlbtnMUPatientEducation.ResumeLayout(False)
        Me.pnlbtnICD10.ResumeLayout(False)
        Me.pnlbtnICD9.ResumeLayout(False)
        Me.pnlMiddle.ResumeLayout(False)
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmCPTICD9Association_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        ISCPTICD9AssocOpen = False
        CPTICD9SelNodeKey = 0
        CPTICD9SelCode = ""

        If Not IsNothing(dtAssociates) Then
            dtAssociates.Dispose()
            dtAssociates = Nothing
        End If
        If Not IsNothing(dtOrderbyCode) Then
            dtOrderbyCode.Dispose()
            dtOrderbyCode = Nothing
        End If
        If Not IsNothing(dtOrderbyDesc) Then
            dtOrderbyDesc.Dispose()
            dtOrderbyDesc = Nothing
        End If

        If Not IsNothing(tooltipnew) Then
            tooltipnew.Dispose()
            tooltipnew = Nothing
        End If

        If elementHostICD10 IsNot Nothing Then
            elementHostICD10.Child = Nothing
            elementHostICD10 = Nothing
        End If

        If wpfICD10UserControl IsNot Nothing Then
            RemoveHandler wpfICD10UserControl.SearchFired, AddressOf ICD10SearchFired
            RemoveHandler wpfICD10UserControl.CodeAddedToCurrent, AddressOf ICD10CodeAdded
            RemoveHandler wpfICD10UserControl.CodeSelectedForImport, AddressOf ICD10CodeAdded

            wpfICD10UserControl.Dispose()
            wpfICD10UserControl = Nothing
        End If

    End Sub

    ''chetan added to Fill the Associates of Tags in ContaxMenu
    Private Sub FillTagsAssociates()
        Try


            Dim dtTags As DataTable
            Dim objclsSmartDiagnosis As clsSmartDiagnosis = New clsSmartDiagnosis
            dtTags = objclsSmartDiagnosis.GetAllCategory("Tags")
            objclsSmartDiagnosis.Dispose()
            objclsSmartDiagnosis = Nothing

            cntTags.MenuItems.Clear()

            Dim oChildItem As MenuItem
            Dim i As Integer
            oChildItem = New MenuItem
            oChildItem.Text = "Remove  Associate"
            With cntTags.MenuItems
                .Add(oChildItem)
            End With
            AddHandler oChildItem.Click, AddressOf cntTags_Click
            oChildItem = Nothing
            If (IsNothing(dtTags) = False) Then

                For i = 0 To dtTags.Rows.Count - 1
                    oChildItem = New MenuItem
                    oChildItem.Text = dtTags.Rows(i)(1).ToString
                    With cntTags.MenuItems
                        .Add(oChildItem)
                    End With
                    AddHandler oChildItem.Click, AddressOf cntTags_Click
                    oChildItem = Nothing
                Next
                dtTags.Dispose()
                dtTags = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''''Chetan Added  Event Handler for cntTags.Click
    Public Sub cntTags_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        If oCurrentMenu.Text.Trim() = "Remove  Associate" Then
            If Not trvCPTAssociation.SelectedNode Is trvCPTAssociation.Nodes.Item(0) OrElse Not trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Then

                Dim mychildnode As myTreeNode
                '  Dim key As Int64
                mychildnode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                'objMedicationDBLayer.DeleteMedication(mychildnode.Index) 'delete from collection
                'key = mychildnode.Key
                mychildnode.Remove() 'delete from Medicationdetails treeview

                'Add the deleted node to Medication treeview

            End If
        Else

            Try
                If IsNothing(trvCPTAssociation.SelectedNode) = False Then
                    If IsNothing(oCurrentMenu) = False Then
                        Dim TagName As String
                        TagName = "[" & oCurrentMenu.Text() & "]"
                        '''' Insert Assocated Tag Name into the 'TemplateResult'   
                        CType(trvCPTAssociation.SelectedNode, myTreeNode).TemplateResult = TagName
                        '''' Concat the Associated Tag with the Node so that User Can understand 
                        '''' which Tag is Assocated with it 
                        trvCPTAssociation.SelectedNode.Text = CType(trvCPTAssociation.SelectedNode, myTreeNode).NodeName & "-" & TagName
                    End If
                End If
            Catch ex As Exception
                ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule., gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    'code added for optimization in 6020()
    Private Function fill_CPTControl()

        Dim i As Integer
        Try
            If (InvokeRequired) Then
                Me.Invoke(New fill_ControlDelegate(AddressOf fill_CPTControl))
            Else
                If gblnIcd10MasterTransition = True Then 'gblnIcd10Transition
                    'dt = oclsCPTICD9.FillControls(Associates.ICD10)
                    'dtAssociates = dt
                    'If dt.Rows.Count < 400 Then
                    '    ICD9Count = dt.Rows.Count - 1
                    'Else
                    '    ICD9Count = 400
                    'End If

                    'For i = 0 To ICD9Count   'dt.Rows.Count - 1 'ICD9Count
                    '    Dim mychildnode As myTreeNode
                    '    ''mychildnode.Display = "Icd9Code - Icd9Desc"
                    '    ''mychildnode.key =ICD9ID
                    '    ''mychildnode.Tag = Icd9Desc
                    '    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String))
                    '    trvAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
                    'Next

                    'trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
                    'trvAssociates.ExpandAll()
                    'Panel4.Visible = True
                    'txtsearchCPT.Text = ""
                    'txtsearchCPT.Focus()
                    'txtsearchCPT.Select()
                    ' '''Fill ICD9 using treeview control
                    'GloUC_trvAssociates.DataSource = dt
                    'GloUC_trvAssociates.ValueMember = dt.Columns("nICD9ID").ColumnName
                    'GloUC_trvAssociates.DescriptionMember = dt.Columns("sDescription").ColumnName
                    'GloUC_trvAssociates.CodeMember = dt.Columns("sICD9Code").ColumnName
                    'GloUC_trvAssociates.ICDRevision = dt.Columns("nICDRevision").ColumnName
                    'GloUC_trvAssociates.IsDiagnosisSearch = True
                    'GloUC_trvAssociates.FillTreeView()

                    Try
                        putallpaneldown()
                        PopulateAssociates(Associates.ICD10)

                        '03-Jun-14 Aniket: Resolving search issues
                        GloUC_trvAssociates.IsDiagnosisSearch = True
                        GloUC_trvAssociates.txtsearch.Text = ""
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try


                Else

                    Dim dt As DataTable
                    Dim oclsCPTICD9 As clsCPTICD9Association = New clsCPTICD9Association
                    dt = oclsCPTICD9.FillControls(Associates.ICD9)
                    If (IsNothing(dtAssociates) = False) Then
                        dtAssociates.Dispose()
                        dtAssociates = Nothing
                    End If
                    dtAssociates = dt
                    oclsCPTICD9.Dispose()
                    oclsCPTICD9 = Nothing


                    If dt.Rows.Count < 400 Then
                        ICD9Count = dt.Rows.Count - 1
                    Else
                        ICD9Count = 400
                    End If

                    For i = 0 To ICD9Count   'dt.Rows.Count - 1 'ICD9Count
                        Dim mychildnode As myTreeNode
                        ''mychildnode.Display = "Icd9Code - Icd9Desc"
                        ''mychildnode.key =ICD9ID
                        ''mychildnode.Tag = Icd9Desc
                        mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String))
                        trvAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
                    Next

                    trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
                    trvAssociates.ExpandAll()
                    Panel4.Visible = True
                    txtsearchCPT.Text = ""
                    txtsearchCPT.Focus()
                    txtsearchCPT.Select()

                    GloUC_trvAssociates.DataSource = dt
                    GloUC_trvAssociates.ValueMember = dt.Columns("nICD9ID").ColumnName
                    GloUC_trvAssociates.DescriptionMember = dt.Columns("sDescription").ColumnName
                    GloUC_trvAssociates.CodeMember = dt.Columns("sICD9Code").ColumnName
                    GloUC_trvAssociates.ICDRevision = dt.Columns("nICDRevision").ColumnName
                    GloUC_trvAssociates.IsDiagnosisSearch = True
                    GloUC_trvAssociates.FillTreeView()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
        Return Nothing
    End Function

    Private Sub frmCPTICD9Association_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SuspendLayout()
        Me.Cursor = Cursors.WaitCursor
        Try
            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If

            rbtAll.Checked = True
            'oclsCPTICD9 = New clsCPTICD9Association
            trvCPTAssociation.AllowDrop = True

            Dim rootnode As myTreeNode
            ' Dim dt As New DataTable
            UpdateLog("Get All CPTs from Database ")

            GloUC_trvCPT.txtsearch.Text = CPTICD9SelCode
            dtOrderbyDesc = FillCPT(dtOrderbyDesc, OrderBy.Description)

            UpdateLog("Fill 400 CPTs in TreeView control - START")
            rootnode = New myTreeNode("CPT Association", -1)
            rootnode.ImageIndex = 1
            rootnode.SelectedImageIndex = 1
            trvCPTAssociation.Nodes.Add(rootnode)


            If gblnIcd10MasterTransition = True Then 'gblnIcd10Transition
                'Populate By Default ICD9 data in Associates' TreeView
                rootnode = New myTreeNode("ICD10", -1)
                rootnode.ImageIndex = 2
                rootnode.SelectedImageIndex = 2
                trvAssociates.Nodes.Add(rootnode)
            Else
                'Populate By Default ICD9 data in Associates' TreeView
                rootnode = New myTreeNode("ICD9", -1)
                rootnode.ImageIndex = 2
                rootnode.SelectedImageIndex = 2
                trvAssociates.Nodes.Add(rootnode)
                GloUC_trvCPT.IsDiagnosisSearch = True ''added for Bug #92518 searching not working 
            End If

            Dim oTS As New ThreadStart(AddressOf Me.fill_CPTControl)
            Dim oThread As New Thread(oTS)
            oThread.Start()


            'Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
            If ISCPTICD9AssocOpen = True Then
                If CPTICD9SelNodeKey <> 0 Then
                    trvCPTAssociation.Nodes.Item(0).Nodes.Clear()
                    GloUC_trvCPT.FillTree(CPTICD9SelCode)
                    Dim newnode As gloUserControlLibrary.myTreeNode
                    For Each newnode In GloUC_trvCPT.Nodes
                        If newnode.ID = CPTICD9SelNodeKey Then
                            Dim tree_node As New gloEMR.myTreeNode
                            tree_node.Text = newnode.Text
                            tree_node.Tag = newnode.Tag
                            tree_node.Key = newnode.ID
                            AddNode(tree_node)
                            tree_node.Dispose()
                            tree_node = Nothing
                            Exit For
                        End If
                    Next
                End If
            End If

            'chetan added for filling context menu for tags 
            ' objclsSmartDiagnosis = New clsSmartDiagnosis
            FillTagsAssociates()

            tooltipnew.SetToolTip(btnICD10, "ICD10 List")



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        Me.ResumeLayout()
    End Sub

    Private Shared frm As frmCPTICD9Association
    Public Shared IsOpen As Boolean = False

    Private Sub ICD10CodeAdded()

        Dim dtBillableCodes As DataTable = Nothing
        Dim targetnode1 As myTreeNode = Nothing
        Dim SelectedICDCode As gloUIControlLibrary.Classes.ICD10.clsICD10 = Nothing
        Dim oclsCPTICD9 As clsCPTICD9Association = Nothing

        Try

            targetnode1 = CType(trvCPTAssociation.SelectedNode, myTreeNode)
            SelectedICDCode = wpfICD10UserControl.GetSelectedICDCode()

            If Not IsNothing(targetnode1) Then

                If Not IsNothing(SelectedICDCode) Then

                    If SelectedICDCode.CodeType = 0 Then

                        oclsCPTICD9 = New clsCPTICD9Association
                        dtBillableCodes = oclsCPTICD9.GetBillableICD10Codes(SelectedICDCode.ICD10Code)

                        For Each element As DataRow In dtBillableCodes.Rows

                            Dim oNodeToAdd As New myTreeNode
                            oNodeToAdd.Key = element("nICD10ID")
                            oNodeToAdd.Text = element("sICD10Code") + " " + element("sDescriptionLong")
                            oNodeToAdd.DrugName = element("sDescriptionLong")
                            oNodeToAdd.Dosage = ""
                            oNodeToAdd.DrugForm = ""
                            oNodeToAdd.Route = ""
                            oNodeToAdd.Frequency = ""
                            oNodeToAdd.NDCCode = ""
                            oNodeToAdd.IsNarcotics = 0
                            oNodeToAdd.Duration = ""
                            oNodeToAdd.mpid = 0
                            oNodeToAdd.DrugQtyQualifier = ""
                            oNodeToAdd.nICDRevision = 10
                            If Not IsNothing(oNodeToAdd) AndAlso Not IsNothing(targetnode1) Then
                                AddAssociates(oNodeToAdd, targetnode1)
                                'Shubhangi 20091208
                                'Check the setting Reset search text box after assiging category
                                If gblnResetSearchTextBox = True Then
                                    GloUC_trvAssociates.txtsearch.ResetText()
                                End If
                                oNodeToAdd.Dispose()
                                oNodeToAdd = Nothing
                            End If
                        Next

                    Else
                        Dim oNodeToAdd As New myTreeNode

                        oNodeToAdd.Key = SelectedICDCode.ICDCodeID
                        oNodeToAdd.Text = SelectedICDCode.ICD10Code + " " + SelectedICDCode.LongDescription
                        oNodeToAdd.DrugName = SelectedICDCode.LongDescription
                        oNodeToAdd.Dosage = ""
                        oNodeToAdd.DrugForm = ""
                        oNodeToAdd.Route = ""
                        oNodeToAdd.Frequency = ""
                        oNodeToAdd.NDCCode = ""
                        oNodeToAdd.IsNarcotics = 0
                        oNodeToAdd.Duration = ""
                        oNodeToAdd.mpid = 0
                        oNodeToAdd.DrugQtyQualifier = ""
                        oNodeToAdd.nICDRevision = 10

                        If Not IsNothing(oNodeToAdd) Then
                            AddAssociates(oNodeToAdd, targetnode1)
                            'Shubhangi 20091208
                            'Check the setting Reset search text box after assiging category
                            If gblnResetSearchTextBox = True Then
                                GloUC_trvAssociates.txtsearch.ResetText()
                            End If
                            oNodeToAdd.Dispose()
                            oNodeToAdd = Nothing
                        End If

                    End If

                Else
                    MessageBox.Show("Select ICD10 code or category to add.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Else

                MessageBox.Show("Select CPT code to associate.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(dtBillableCodes) Then
                dtBillableCodes.Dispose()
                dtBillableCodes = Nothing
            End If

            If Not IsNothing(oclsCPTICD9) Then
                oclsCPTICD9.Dispose()
                oclsCPTICD9 = Nothing
            End If


        End Try
    End Sub

    Private Sub ICD10SearchFired(ByVal SearchText As String)
        Dim dt As DataTable = Nothing
        Dim oclsCPTICD9 As clsCPTICD9Association = Nothing
        Try
            oclsCPTICD9 = New clsCPTICD9Association
            dt = oclsCPTICD9.FillControls(Associates.ICD10, SearchText)

            Me.wpfICD10UserControl.BindTreeNodes(dt)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If

            If oclsCPTICD9 IsNot Nothing Then
                oclsCPTICD9.Dispose()
                oclsCPTICD9 = Nothing
            End If

        End Try
    End Sub


    Public Shared Function GetInstance() As frmCPTICD9Association
        '_mu.WaitOne()
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmCPTICD9Association" Then
                    ''If CType(f, frmICD9Association) = PatientID Then
                    IsOpen = True
                    frm = f
                    ''End If

                End If
            Next
            If (IsOpen = False) Then
                ''frm = New frmICD9Association(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
                frm = New frmCPTICD9Association()
            End If
            'frm = New frmHistory(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
            ''Else
            ''For Each f As Form In Application.OpenForms
            ''    If f.Name = "frmHistory" Then
            ''        If CType(f, frmHistory).m_PatientID = PatientID Then
            ''            IsOpen = True
            ''            frm = f
            ''        End If

            ''    End If
            ''Next
            ''If (IsOpen = False) Then
            ''    frm = New frmHistory(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
            ''End If

            ''End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

    Private Function FillCPT(ByVal dt As DataTable, ByVal intOrderBy As OrderBy) As DataTable
        '''' dt = dtOrderbyCode Or dtOrderbyDesc 
        ''''' Flag  =0 then Pull ICD9 OrderBy ICD9Code
        ''''' Flag  =1 then Pull ICD9 OrderBy Description
        If IsNothing(dt) = True Then
            dt = New DataTable
        End If

        '   Dim i As Integer
        'Dim dt As DataTable

        If dt.Rows.Count = 0 Then
            Dim oclsCPTAssociation As clsCPTAssociation
            oclsCPTAssociation = New clsCPTAssociation
            ''Populate CPT data
            '' TO Pull CPTs Order By CPTCode Pass 0
            dt = oclsCPTAssociation.FillCPT(intOrderBy, GloUC_trvCPT.txtsearch.Text.Trim)
            oclsCPTAssociation.Dispose()
            oclsCPTAssociation = Nothing

            GloUC_trvCPT.DataSource = dt
            GloUC_trvCPT.ValueMember = dt.Columns("CPTID").ColumnName
            ''20090910
            GloUC_trvCPT.Tag = dt.Columns("CPTID").ColumnName
            GloUC_trvCPT.DescriptionMember = dt.Columns("sDescription").ColumnName
            GloUC_trvCPT.CodeMember = dt.Columns("CPTCode").ColumnName
            GloUC_trvCPT.SmartTreatmentId = dt.Columns("SmartTreatmentId").ColumnName
            GloUC_trvCPT.IsCPTSearch = True
            GloUC_trvCPT.FillTreeView()

        End If

        ''If IsNothing(dt) = False Then
        'trvCPT.Hide()
        'trvCPT.Nodes.Clear()
        'Dim rootnode As TreeNode
        'rootnode = New myTreeNode("CPT", -1)
        'rootnode.ImageIndex = 1
        'rootnode.SelectedImageIndex = 1
        'trvCPT.Nodes.Add(rootnode)

        'If IsNothing(dt) = False Then

        '    ' If dt.Rows.Count < 400 Then
        '    CPTCount = dt.Rows.Count - 1
        '    'Else
        '    CPTCount = 400
        '    '  End If

        '    For i = 0 To CPTCount 'dt.Rows.Count - 1
        '        Dim mychildnode As myTreeNode
        '        mychildnode = New myTreeNode(dt.Rows(i)(2), dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
        '        mychildnode.ImageIndex = 7
        '        mychildnode.SelectedImageIndex = 7
        '        trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
        '    Next
        '    trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
        'End If
        'trvCPT.ExpandAll()
        'trvCPT.Show()
        'trvCPT.Select()

        ''fill CPT treeview using user control

        Return dt

    End Function

    Private Sub PopulateAssociates(ByVal id As Associates, Optional ByVal strsearch As String = "")
        GloUC_trvAssociates.IsDiagnosisSearch = False  ''added for Bug #92518 searching not working 
        If Not IsNothing(trvAssociates.Nodes.Item(0)) Then
            trvAssociates.Nodes.Item(0).Nodes.Clear()
        End If
        trvAssociates.Nodes.Clear()
        'txtsearchAssociates.Text = ""
        Dim rootnode As myTreeNode = Nothing

        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        ''pnlbtnDrugs.Dock = DockStyle.Bottom
        ''btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        ''btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        ' ''btnDrugs.ForeColor = Color.Navy

        ''pnlbtnPatientEducation.Dock = DockStyle.Bottom
        ''btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        ''btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        ' ''btnPatientEducation.ForeColor = Color.Navy

        ''pnlbtnTags.Dock = DockStyle.Bottom
        ''btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        ''btnTags.BackgroundImageLayout = ImageLayout.Stretch
        ' ''btnTags.ForeColor = Color.Navy

        ''pnlbtnICD9.Dock = DockStyle.Bottom
        ''btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        ''btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        ' ''btnICD9.ForeColor = Color.Navy
        If id = Associates.ICD10 Then
            GloUC_trvAssociates.Visible = False

            If wpfICD10UserControl Is Nothing Then
                wpfICD10UserControl = New gloUIControlLibrary.ICDSubCodeControl()
                If elementHostICD10 IsNot Nothing Then
                    elementHostICD10.Child = wpfICD10UserControl
                End If
                AddHandler wpfICD10UserControl.SearchFired, AddressOf ICD10SearchFired
                AddHandler wpfICD10UserControl.CodeAddedToCurrent, AddressOf ICD10CodeAdded
                AddHandler wpfICD10UserControl.CodeSelectedForImport, AddressOf ICD10CodeAdded
            End If

            If Not wpfICD10UserControl.DisplayRedesignedForSmartTreatment Then
                wpfICD10UserControl.RedesignDisplay()
            End If
            wpfICD10UserControl.btnClearSearch_Click(Me, New System.Windows.RoutedEventArgs())
            elementHostICD10.Visible = True
            elementHostICD10.BringToFront()
        Else
            GloUC_trvAssociates.Visible = True
            elementHostICD10.Visible = False
            elementHostICD10.SendToBack()
            GloUC_trvAssociates.BringToFront()
        End If

        If id = Associates.Drugs Then

            ''pnlbtnDrugs.Dock = DockStyle.Top
            ''With btnDrugs
            ''    .ForeColor = Color.FromArgb(31, 73, 125)
            ''    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
            ''    .BackgroundImageLayout = ImageLayout.Stretch
            ''End With
            ''rootnode = New myTreeNode("Drugs", -1)
            ''rootnode.ImageIndex = 3
            ''rootnode.SelectedImageIndex = 3
            rootnode = New myTreeNode("Drugs", -1)
            rootnode.ImageIndex = 3
            rootnode.SelectedImageIndex = 3

            pnlbtnDrugs.Dock = DockStyle.Top
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "Selected"

        ElseIf id = Associates.ICD9 Then
            rootnode = New myTreeNode("ICD9", -1)
            rootnode.ImageIndex = 2
            rootnode.SelectedImageIndex = 2

            pnlbtnICD9.Dock = DockStyle.Top
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "Selected"
            GloUC_trvAssociates.IsDiagnosisSearch = True ''added for Bug #92518 searching not working 
        ElseIf id = Associates.ICD10 Then
            rootnode = New myTreeNode("ICD10", -1)
            rootnode.ImageIndex = 2
            rootnode.SelectedImageIndex = 2

            pnlbtnICD10.Dock = DockStyle.Top
            btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnICD10.BackgroundImageLayout = ImageLayout.Stretch
            btnICD10.Tag = "Selected"
        ElseIf id = Associates.PE Then

            ''pnlbtnPatientEducation.Dock = DockStyle.Top
            ''With btnPatientEducation
            ''    .ForeColor = Color.FromArgb(31, 73, 125)
            ''    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
            ''    .BackgroundImageLayout = ImageLayout.Stretch
            ''End With

            ''rootnode = New myTreeNode("Patient Education", -1)
            ''rootnode.ImageIndex = 5
            ''rootnode.SelectedImageIndex = 5

            rootnode = New myTreeNode("Patient Education", -1)
            rootnode.ImageIndex = 5
            rootnode.SelectedImageIndex = 5

            pnlbtnPatientEducation.Dock = DockStyle.Top
            btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            btnPatientEducation.Tag = "Selected"
            '' Fill Tags
        ElseIf id = Associates.MUPE Then

            rootnode = New myTreeNode("MU Patient Education", -1)
            rootnode.ImageIndex = 5
            rootnode.SelectedImageIndex = 5

            pnlbtnMUPatientEducation.Dock = DockStyle.Top
            btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            btnMUPatientEducation.Tag = "Selected"

        ElseIf id = Associates.Tags Then

            ''pnlbtnTags.Dock = DockStyle.Top
            ''With btnTags
            ''    .ForeColor = Color.FromArgb(31, 73, 125)
            ''    .BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
            ''    .BackgroundImageLayout = ImageLayout.Stretch
            ''End With
            ''rootnode = New myTreeNode("Tags", -1)
            ''rootnode.ImageIndex = 4
            ''rootnode.SelectedImageIndex = 4

            rootnode = New myTreeNode("Tags", -1)
            rootnode.ImageIndex = 4
            rootnode.SelectedImageIndex = 4

            pnlbtnTags.Dock = DockStyle.Top
            btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnTags.BackgroundImageLayout = ImageLayout.Stretch
            btnTags.Tag = "Selected"

            ''Added Rahul for new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014
            ''Fill Flowsheet
        ElseIf id = Associates.Flow Then

            rootnode = New myTreeNode("Flowsheet", -1)
            rootnode.ImageIndex = 10
            rootnode.SelectedImageIndex = 10

            pnlFlowsheet.Dock = DockStyle.Top
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
            btnFlowsheet.Tag = "Selected"

            ''Fill Lab Orders
        ElseIf id = Associates.LabOrder Then

            rootnode = New myTreeNode("Orders & Results", -1)
            rootnode.ImageIndex = 11
            rootnode.SelectedImageIndex = 11

            pnlbtnLabOrders.Dock = DockStyle.Top
            btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnLabOrders.Tag = "Selected"

            ''Fill Orders
        ElseIf id = Associates.Order Then

            rootnode = New myTreeNode("Order Templates", -1)
            rootnode.ImageIndex = 12
            rootnode.SelectedImageIndex = 12

            pnlbtnOrders.Dock = DockStyle.Top
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "Selected"

            ''Fill Referral Letter
        ElseIf id = Associates.Referral Then

            rootnode = New myTreeNode("Referral Letter", -1)
            rootnode.ImageIndex = 13
            rootnode.SelectedImageIndex = 13

            pnlbtnReferrals.Dock = DockStyle.Top
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
            btnReferrals.Tag = "Selected"
            ''
        End If
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        trvAssociates.Nodes.Add(rootnode)



        ''Sandip Darade 20091014
        If Not IsNothing(GloUC_trvAssociates.txtsearch.Text) Then
            strsearch = GloUC_trvAssociates.txtsearch.Text
        End If

        '03-Jun-14 Aniket: Resolving search issues
        'If gblnResetSearchTextBox = True Then
        '    strsearch = ""
        'End If
        Dim dt As DataTable
        Dim oclsCPTICD9 As clsCPTICD9Association = New clsCPTICD9Association
        dt = oclsCPTICD9.FillControls(id, strsearch)
        If (IsNothing(dtAssociates) = False) Then
            dtAssociates.Dispose()
            dtAssociates = Nothing
        End If
        dtAssociates = dt
        oclsCPTICD9.Dispose()
        oclsCPTICD9 = Nothing

        ' Dim i As Integer
        '  Dim myTempNode As myTreeNode
        'For i = 0 To dt.Rows.Count - 1
        ' Dim mychildnode As myTreeNode
        If id = Associates.ICD9 Then
            '' trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String)))
            ''Associates filled using treeview control

            GloUC_trvAssociates.Clear()
            GloUC_trvAssociates.DataSource = dt
            GloUC_trvAssociates.ValueMember = dt.Columns("nICD9ID").ColumnName
            GloUC_trvAssociates.DescriptionMember = dt.Columns("sDescription").ColumnName
            GloUC_trvAssociates.CodeMember = dt.Columns("sICD9Code").ColumnName
            GloUC_trvAssociates.ICDRevision = dt.Columns("nICDRevision").ColumnName
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
            GloUC_trvAssociates.FillTreeView()
        ElseIf id = Associates.ICD10 Then
            '' trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String)))
            ''Associates filled using treeview control

            wpfICD10UserControl.BindTreeNodes(dt)

            'GloUC_trvAssociates.Clear()
            'GloUC_trvAssociates.DataSource = dt
            'GloUC_trvAssociates.ValueMember = dt.Columns("nICD9ID").ColumnName
            'GloUC_trvAssociates.DescriptionMember = dt.Columns("sDescription").ColumnName
            'GloUC_trvAssociates.CodeMember = dt.Columns("sICD9Code").ColumnName
            'GloUC_trvAssociates.ICDRevision = dt.Columns("nICDRevision").ColumnName
            'GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            'GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
            'GloUC_trvAssociates.FillTreeView()
        ElseIf id = Associates.Drugs Then
            GloUC_trvAssociates.Clear()
            GloUC_trvAssociates.DataSource = dt
            ''Sandip Darade 20091014  if drugs to be filled in the treview 
            GloUC_trvAssociates.IsDrug = True

            GloUC_trvAssociates.DrugFlag = 16 ''For all drugs 
            GloUC_trvAssociates.ValueMember = dt.Columns("DrugsID").ColumnName
            GloUC_trvAssociates.DescriptionMember = dt.Columns("Dosage").ColumnName
            'DrugName SHOULD BE THE DESCRIPTIO FIELD
            ' GloUC_trvAssociates.DescriptionMember = dt.Columns("DrugName").ColumnName
            GloUC_trvAssociates.CodeMember = dt.Columns("DrugName").ColumnName
            GloUC_trvAssociates.DrugFormMember = dt.Columns("DrugForm").ColumnName
            GloUC_trvAssociates.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
            GloUC_trvAssociates.NDCCodeMember = Convert.ToString(dt.Columns("sNDCcode").ColumnName) '''''bug fixed 6850
            GloUC_trvAssociates.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
            GloUC_trvAssociates.FrequencyMember = dt.Columns("sFrequency").ColumnName
            GloUC_trvAssociates.DurationMember = dt.Columns("sDuration").ColumnName
            GloUC_trvAssociates.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
            GloUC_trvAssociates.mpidmember = dt.Columns("mpid").ColumnName
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
            GloUC_trvAssociates.ICDRevision = Nothing
            'Display Type Changed by Mayuri:20091008
            'To display drugs in form of DrugName and Drug Form
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
            GloUC_trvAssociates.FillTreeView()

            'GloUC_trvAssociates.DataSource = dt
            'GloUC_trvAssociates.ValueMember = dt.Columns("DrugsID").ColumnName
            'GloUC_trvAssociates.DescriptionMember = dt.Columns("DrugName").ColumnName
            'GloUC_trvAssociates.CodeMember = dt.Columns("DrugName").ColumnName
            'GloUC_trvAssociates.FillTreeView()
        Else
            '' trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(1), String)))
            ''IF Tags clicked column 0 = TemplateID AND column 1 = TemplateName
            ''IF Patient Education clicked  column 0 = nTemplateID AND column 1 = sTemplateName
            GloUC_trvAssociates.Clear()
            GloUC_trvAssociates.DataSource = dt
            GloUC_trvAssociates.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
            GloUC_trvAssociates.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)
            GloUC_trvAssociates.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
            GloUC_trvAssociates.ICDRevision = Nothing
            GloUC_trvAssociates.FillTreeView()
        End If

        'If Not IsNothing(dt) Then
        '    dt.Dispose()
        '    dt = Nothing
        'End If
        'rootnode.Nodes.Add(dt.Rows(i)(1))
        'Next
        'trvAssociates.ExpandAll()
        'trvAssociates.Select()
        'trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
    End Sub

    'Private Sub txtsearchCPT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchCPT.TextChanged

    '    '''''''''''''''''''####################''''''''''''''''''''''''''
    '    '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
    '    Dim strSearchDetails As String
    '    If Trim(txtsearchCPT.Text) <> "" Then
    '        strSearchDetails = Replace(txtsearchCPT.Text, "'", "''")
    '        strSearchDetails = Replace(strSearchDetails, "[", "") & ""
    '        strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
    '    Else
    '        strSearchDetails = ""
    '    End If
    '    '''''''''''''''''''####################''''''''''''''''''''''''''



    '    'Try
    '    '    Dim mychildnode As myTreeNode
    '    '    'child node collection
    '    '    For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
    '    '        'compare selected node text and entered text
    '    '        Dim str As String
    '    '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchCPT.Text))))
    '    '        If str = UCase(Trim(txtsearchCPT.Text)) Then
    '    '            trvCPT.SelectedNode = mychildnode
    '    '            txtsearchCPT.Focus()
    '    '            Exit Sub
    '    '        End If
    '    '    Next
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    'End Try

    '    'sarika 24th sept 07
    '    'first fill the treeview with only those nodes which match the search criteria 
    '    'i.e., strings starting with the search string and strings containing the search string as substring

    '    If txtsearchCPT.Tag <> Trim(strSearchDetails) Then
    '        ' If btnAllDrugs.Dock = DockStyle.Top Then
    '        'If rbSearch2.Checked = False Then
    '        AddCPT(Trim(strSearchDetails), dtOrderbyCode)
    '        'Else
    '        AddCPT(Trim(strSearchDetails), dtOrderbyDesc)
    '        'End If

    '        'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
    '        '    AddDrugs(Trim(txtsearchCPT.Text))
    '        'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
    '        '    AddDrugs(Trim(txtsearchCPT.Text))
    '        'End If
    '        'If Len(Trim(txtsearchDrug.Text)) = 1 Then
    '        txtsearchCPT.Tag = Trim(strSearchDetails)
    '        'End If
    '    End If

    '    Exit Sub

    '    '---------------------------------------------------------------------------------

    '    'Try
    '    '    If Trim(txtsearchCPT.Text) <> "" Then
    '    '        If trvCPT.Nodes.Item(0).GetNodeCount(False) > 0 Then
    '    '            Dim mychildnode As myTreeNode
    '    '            'child node collection
    '    '            For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
    '    '                'search against Description
    '    '                If rbSearch2.Checked = True Then
    '    '                    If UCase(Mid(mychildnode.Tag, 1, Len(Trim(txtsearchCPT.Text)))) = UCase(Trim(txtsearchCPT.Text)) Then
    '    '                        'select matching node
    '    '                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
    '    '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
    '    '                        '*************
    '    '                        trvCPT.SelectedNode = mychildnode
    '    '                        txtsearchCPT.Focus()
    '    '                        Exit Sub
    '    '                    End If
    '    '                Else
    '    '                    'search against CPT Code
    '    '                    Dim str As String
    '    '                    str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
    '    '                    str = Mid(str, 1, Len(Trim(txtsearchCPT.Text)))
    '    '                    If UCase(str) = UCase(Trim(txtsearchCPT.Text)) Then
    '    '                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
    '    '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
    '    '                        '*************
    '    '                        'select matching node
    '    '                        trvCPT.SelectedNode = mychildnode
    '    '                        txtsearchCPT.Focus()
    '    '                        Exit Sub
    '    '                    End If
    '    '                End If
    '    '            Next

    '    '            ' '' 
    '    '            ' '' 20070922 - Mahesh - InString Search 
    '    '            For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
    '    '                'search against Description
    '    '                If rbSearch2.Checked = True Then
    '    '                    If InStr(UCase(mychildnode.Tag.ToString.Trim), UCase(txtsearchCPT.Text.Trim), CompareMethod.Text) > 0 Then
    '    '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
    '    '                        'select matching node
    '    '                        trvCPT.SelectedNode = mychildnode
    '    '                        txtsearchCPT.Focus()
    '    '                        Exit Sub
    '    '                    End If
    '    '                Else
    '    '                    'search against CPT Code
    '    '                    Dim str As String
    '    '                    str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
    '    '                    '  str = Mid(str, 1, Len(Trim(txtsearchDrugs.Text)))
    '    '                    If InStr(UCase(str.Trim), UCase(txtsearchCPT.Text.Trim), CompareMethod.Text) > 0 Then
    '    '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
    '    '                        'select matching node
    '    '                        trvCPT.SelectedNode = mychildnode
    '    '                        txtsearchCPT.Focus()
    '    '                        Exit Sub
    '    '                    End If
    '    '                End If
    '    '            Next
    '    '            ' '' 
    '    '        End If
    '    '    End If

    '    'Catch ex As Exception
    '    '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    'End Try

    'End Sub

    ''sarika 24th september 07
    'Private Sub AddCPT(ByVal strsearch As String, ByVal dt As DataTable)
    '    Try


    '        Dim i As Integer
    '        Dim tdt As DataTable
    '        'For i = 0 To dt.Rows.Count - 1
    '        Dim dv As New DataView(dt)

    '        'If rbSearch2.Checked = True Then
    '        ''description 
    '        dv.RowFilter = "sDescription Like '%" & strsearch & "%'"
    '        'Else
    '        ''code
    '        dv.RowFilter = "CPTCode Like '%" & strsearch & "%'"
    '        'End If
    '        tdt = New DataTable
    '        tdt = dv.ToTable

    '        'add the nodes to treenode
    '        trvCPT.BeginUpdate()
    '        trvCPT.Visible = False
    '        If trvCPT.GetNodeCount(False) > 0 Then
    '            trvCPT.Nodes.Item(0).Remove()
    '            Dim rootnode As TreeNode
    '            rootnode = New myTreeNode("CPT", -1)
    '            rootnode.ImageIndex = 1
    '            rootnode.SelectedImageIndex = 1
    '            trvCPT.Nodes.Add(rootnode)
    '        End If

    '        'fill the treeview with the dv
    '        If Not tdt Is Nothing Then
    '            trvCPT.Visible = True

    '            If tdt.Rows.Count < 400 Then
    '                CPTCount = tdt.Rows.Count - 1
    '            Else
    '                CPTCount = 400
    '            End If

    '            For i = 0 To CPTCount 'tdt.Rows.Count - 1
    '                Dim mychildnode As myTreeNode
    '                mychildnode = New myTreeNode(tdt.Rows(i)(2), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String))
    '                mychildnode.ImageIndex = 7
    '                mychildnode.SelectedImageIndex = 7
    '                trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
    '            Next
    '            If tdt.Rows.Count > 0 Then
    '                trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
    '            End If
    '        Else
    '        End If
    '        trvCPT.ExpandAll()
    '    Catch ex As Exception
    '        'Dim objex As New gloUserControlLibrary.gloUserControlExceptions
    '        'objex.ErrorMessage = ""
    '        'Throw objex
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        trvCPT.EndUpdate()
    '    End Try

    'End Sub
    ''-------------------------

    Private Sub trvCPTAssociation_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvCPTAssociation.DragOver
        Try
            'If IsNothing(trvCPTAssociation.SelectedNode) = True Then
            '    Exit Sub
            'End If

            'Check that there is a TreeNode being dragged
            'commented on 30/8/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'As the mouse moves over nodes, provide feedback to the user
            'by highlighting the node that is the current drop target
            Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))

            'commented on 30/8/2005 Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)
            Dim targetNode As myTreeNode = selectedTreeview.GetNodeAt(pt)

            'See if the targetNode is currently selected, if so no need to validate again
            'If Not (selectedTreeview Is targetNode) Then
            '    'Select the node currently under the cursor
            '    selectedTreeview.SelectedNode = targetNode


            '    'Check that the selected node is not the dropNode and also that it
            '    'is not a child of the dropNode and therefore an invalid target
            '    Dim dropNode As TreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
            '    Do Until targetNode Is Nothing
            '        If targetNode Is dropNode Then
            '            e.Effect = DragDropEffects.None
            '            Exit Sub
            '        End If
            '        targetNode = targetNode.Parent
            '    Loop
            'End If

            'Currently selected node is a suitable target, allow the move
            e.Effect = DragDropEffects.Move
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RefreshCPT()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        Dim innerchildflag As Boolean = False
        Dim outerchildflag As Boolean = False
        Dim parentflag As Boolean = False

        For Each ptn As TreeNode In trvCPTAssociation.Nodes.Item(0).Nodes
            For Each otherptn As TreeNode In ptn.Nodes
                For Each ootherptn As TreeNode In otherptn.Nodes
                    If ootherptn.Checked = False Then
                        innerchildflag = True
                        Exit For
                    End If
                Next
                If innerchildflag = False And otherptn.Nodes.Count > 0 Then
                    otherptn.Checked = True

                Else

                    outerchildflag = True
                End If
                innerchildflag = False
            Next

            If outerchildflag = False And ptn.Nodes.Count > 0 Then
                ptn.Checked = True
            Else

                parentflag = True
            End If
            outerchildflag = False
        Next
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
    End Sub

    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        For Each ctn As TreeNode In tn.Nodes
            ctn.Checked = bCheck
            CheckAllChildren(ctn, bCheck)
        Next
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
    End Sub


    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        If tn Is Nothing Then
            Exit Sub
        End If
        If tn.Parent Is Nothing Then
            Exit Sub
        End If

        bChildTrigger = False
        bParentTrigger = False

        If bCheck Then
            Dim bNodeFound As Boolean = False
            For Each _Node As TreeNode In tn.Parent.Nodes
                If _Node.Checked = False Then
                    tn.Parent.Checked = False
                    bNodeFound = True
                    Exit For
                End If
            Next
            If bNodeFound = False Then
                tn.Parent.Checked = True
            End If
        Else
            tn.Parent.Checked = bCheck
        End If

        CheckMyParent(tn.Parent, bCheck)
        bParentTrigger = True
        bChildTrigger = True
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
    End Sub

    Private Sub RefreshCPT()
        '' Clear Asscoation 
        trvCPTAssociation.Nodes(0).Nodes.Clear()

        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        trvCPTAssociation.Nodes.Item(0).Checked = False
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        '' Fill CPTs
        Call FillCPT(dtOrderbyCode, OrderBy.Code)
        txtsearchCPT.Text = ""
        txtsearchCPT.Focus()
        'trvCPTAssociation.Nodes.Item(0).Nodes.Clear()
        'trvCPT.Nodes.Item(0).Nodes.Clear()
        'Dim dt As DataTable
        'dt = oclsCPTICD9.FillControls(3)
        'Dim i As Integer
        'For i = 0 To dt.Rows.Count - 1
        '    Dim mychildnode As myTreeNode
        '    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
        '    trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
        'Next
        'trvCPT.ExpandAll()
        'trvCPT.Select()

    End Sub

    Private Sub SaveAssociation()
        'Get node count of child nodes in trICD9Associates
        If trvCPTAssociation.Nodes(0).GetNodeCount(False) > 0 Then

            Dim i As Integer
            For i = 0 To trvCPTAssociation.Nodes(0).GetNodeCount(False) - 1
                Dim CPTNode As myTreeNode
                'get the ICD9Node associated sequentially
                CPTNode = trvCPTAssociation.Nodes(0).Nodes.Item(i)
                If CPTNode.GetNodeCount(True) > 0 Then
                    Dim k As Integer
                    Dim arrlist As New ArrayList
                    For k = 0 To 7
                        Dim AssociateNode As myTreeNode
                        AssociateNode = CPTNode.Nodes.Item(k)
                        Dim j As Integer
                        For j = 0 To AssociateNode.GetNodeCount(False) - 1
                            ''''''''''''''' Added by Ujwala - Smart Treatment Changes - checkbox - as on 20101012
                            Dim thisNode As myTreeNode = CType(AssociateNode.Nodes.Item(j), myTreeNode)
                            If AssociateNode.Text = "ICD9/10" Or AssociateNode.Text = "ICD10" Then
                                arrlist.Add(New myList(thisNode.Key, "i", AssociateNode.Nodes.Item(j).Checked, 0, "", Convert.ToInt16(thisNode.nICDRevision)))
                                'For De-Normalization
                            ElseIf AssociateNode.Text = "Drugs" Then
                                arrlist.Add(New myList(thisNode.Key, "d", thisNode.DrugName, thisNode.NDCCode, thisNode.mpid))
                                'For De-Normalization
                            ElseIf AssociateNode.Text = "Patient Education" Then
                                arrlist.Add(New myList(thisNode.Key, "p", AssociateNode.Nodes.Item(j).Checked, 0))

                                '''' Added By Mahesh
                            ElseIf AssociateNode.Text = "Tags" Then
                                arrlist.Add(New myList(thisNode.Key, "t", AssociateNode.Nodes.Item(j).Checked, 0, thisNode.Text))

                                ''Added Rahul on 20101014
                            ElseIf AssociateNode.Text = "Flowsheet" Then
                                arrlist.Add(New myList(thisNode.Key, "f", AssociateNode.Nodes.Item(j).Checked, 0))

                            ElseIf AssociateNode.Text = "Referral Letter" Then
                                arrlist.Add(New myList(thisNode.Key, "r", AssociateNode.Nodes.Item(j).Checked, 0))

                            ElseIf AssociateNode.Text = "Orders & Results" Then
                                arrlist.Add(New myList(thisNode.Key, "l", AssociateNode.Nodes.Item(j).Checked, 0))

                            ElseIf AssociateNode.Text = "Order Templates" Then
                                arrlist.Add(New myList(thisNode.Key, "o", AssociateNode.Nodes.Item(j).Checked, 0))
                                ''End
                            End If
                            ''''''''''''''' Added by Ujwala - Smart Treatment Changes - checkbox - as on 20101012
                        Next

                    Next
                    Dim oclsCPTICD9 As clsCPTICD9Association = New clsCPTICD9Association
                    oclsCPTICD9.AddData(CPTNode.Key, CPTNode.Tag, arrlist)
                    oclsCPTICD9.Dispose()
                    oclsCPTICD9 = Nothing
                    arrlist.Clear()
                    arrlist = Nothing
                End If
            Next
            ''RefreshCPT()
        End If
        'Shubhangi 20091202
        'Change the save button to save & close
        Me.Close()
    End Sub

    Private Sub trvAssociates_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvAssociates.DragDrop, trvCPTAssociation.DragDrop
        Try
            'Check that there is a TreeNode being dragged

            'commented on 30/08/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'Get the TreeNode being dragged
            'commented on 30/08/2005 Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

            'The target node should be selected from the DragOver event

            'commented on 30/08/2005 Dim targetNode As TreeNode = selectedTreeview.SelectedNode

            Dim targetNode1 As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

            'Remove the drop node from its current location

            'If there is no targetNode add dropNode to the bottom of the TreeView root
            'nodes, otherwise add it to the end of the dropNode child nodes
            'If targetNode Is Nothing Then
            '    dropNode.Remove()
            '    selectedTreeview.Nodes.Add(dropNode)
            '    AddControl()
            '    PopulateMedication(dropNode.Key)

            'targetnode is the node selected on which the dropnode is to be dropped.
            'If targetNode Is selectedTreeview.Nodes.Item(0) Then

            'check if dropnode is node selected from trvAssociates treeview
            If dropNode.TreeView Is trvAssociates Then
                If trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) > 0 Then
                    If Not trvCPTAssociation.SelectedNode Is trvCPTAssociation.Nodes.Item(0) Then
                        If Not IsNothing(targetNode1) AndAlso Not IsNothing(dropNode) Then
                            AddAssociates(dropNode, targetNode1)
                        End If
                    End If
                End If
            End If
            'commented from 14/09/2005
            'If dropNode.Parent Is trvAssociates.Nodes.Item(0) Then
            '    Dim targetnode As myTreeNode
            '    'check if targetnode is node at second level in trvCPTAssociation treeview
            '    If targetNode1.Parent Is trvCPTAssociation.Nodes.Item(0) Or (targetNode1.Key = -1) Then
            '        If targetNode1.Parent Is trvCPTAssociation.Nodes.Item(0) Then
            '            targetnode = targetNode1
            '        Else

            '            targetnode = targetNode1.Parent
            '        End If

            '        Dim str As String
            '        str = dropNode.Key
            '        Dim mytragetnode As myTreeNode
            '        Dim associatenode As myTreeNode

            '        associatenode = dropNode.Clone
            '        associatenode.Key = dropNode.Key
            '        associatenode.Text = dropNode.Text

            '        'if selected category is cpt, add node to cpt child node 
            '        'in trICD9Associates
            '        If btnCPT.Dock = DockStyle.Top Then
            '            For Each mytragetnode In targetnode.Nodes.Item(0).Nodes
            '                If mytragetnode.Key = str Then
            '                    Exit Sub
            '                End If
            '            Next
            '            targetnode.Nodes.Item(0).Nodes.Add(associatenode)
            '            'if selected category is Drugs, add node to Drugs child node 
            '            'in trICD9Associates
            '        ElseIf btnDrugs.Dock = DockStyle.Top Then
            '            For Each mytragetnode In targetnode.Nodes.Item(1).Nodes
            '                If mytragetnode.Key = str Then
            '                    Exit Sub
            '                End If
            '            Next
            '            targetnode.Nodes.Item(1).Nodes.Add(associatenode)

            '            'if selected category is PatientEducation, add node to PatientEducation child node 
            '            'in trICD9Associates
            '        ElseIf btnPatientEducation.Dock = DockStyle.Top Then
            '            For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
            '                If mytragetnode.Key = str Then
            '                    Exit Sub
            '                End If
            '            Next
            '            targetnode.Nodes.Item(2).Nodes.Add(associatenode)
            '        End If
            '        dropNode.EnsureVisible()
            '        selectedTreeview.ExpandAll()
            '        selectedTreeview.SelectedNode = dropNode

            '    End If
            'End If
            'commendted from 14/09/2005
            'Ensure the newley created node is visible to the user and select it
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub btnICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.Click
        Try
            If btnICD9.Tag = "UnSelected" Then
                putallpaneldown()
                GloUC_trvAssociates.txtsearch.Text = ""
                PopulateAssociates(Associates.ICD9)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTags_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTags.Click
        Try
            If btnTags.Tag = "UnSelected" Then
                'Populate Tempalate of Type-'Tags' data
                putallpaneldown()
                PopulateAssociates(Associates.Tags)
                GloUC_trvAssociates.txtsearch.Text = ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.Click
        Try
            If btnDrugs.Tag = "UnSelected" Then
                putallpaneldown()
                PopulateAssociates(Associates.Drugs)
                GloUC_trvAssociates.txtsearch.Text = ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPatientEducation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientEducation.Click
        Try
            If btnPatientEducation.Tag = "UnSelected" Then
                putallpaneldown()
                PopulateAssociates(Associates.PE)
                GloUC_trvAssociates.txtsearch.Text = ""
            End If
            'Populate Patient Education data         
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '***********************Ojeswini_18Dec2008***********************************************


    Private Sub trvAssociates_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvAssociates.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trvCPTAssociation_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCPTAssociation.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                Dim trvNode As TreeNode
                trvNode = trvCPTAssociation.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trvCPTAssociation.SelectedNode = trvNode
                End If

                If Not IsNothing(trvCPTAssociation.SelectedNode) Then
                    ''Sanjog- Added on 20101122 to remove error on rignt click on Root node 
                    If (trvCPTAssociation.SelectedNode.Level <> 0) Then
                        If trvCPTAssociation.SelectedNode.Parent.Text = "Tags" Then
                            'Try
                            '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                            '        trvCPTAssociation.ContextMenu.Dispose()
                            '        trvCPTAssociation.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvCPTAssociation.ContextMenu = cntTags
                            Exit Sub
                        End If
                    End If
                    ''Sanjog- Added on 20101122 to remove error on rignt click on Root node
                    If trvCPTAssociation.Nodes.Item(0).Text = trvCPTAssociation.SelectedNode.Text Or trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Or (CType(trvCPTAssociation.SelectedNode, myTreeNode).Key = -1) Then
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = cntICD9Association
                        'treeindex = trPrescriptionDetails.SelectedNode.Index
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If Not trvCPTAssociation.SelectedNode Is trvCPTAssociation.Nodes.Item(0) OrElse Not trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Then

                Dim mychildnode As myTreeNode
                '  Dim key As Int64
                mychildnode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                'objMedicationDBLayer.DeleteMedication(mychildnode.Index) 'delete from collection
                'key = mychildnode.Key
                mychildnode.Remove() 'delete from Medicationdetails treeview

                'Add the deleted node to Medication treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub AddNode(ByVal mynode As myTreeNode)

        'If mynode.Parent Is trvCPT.Nodes.Item(0) Then
        Dim str As String
        str = mynode.Key
        Dim mytragetnode As myTreeNode
        For Each mytragetnode In trvCPTAssociation.Nodes.Item(0).Nodes
            If mytragetnode.Key.ToString.Trim = str.Trim Then
                Exit Sub
            End If
        Next

        'Add ICD9/Drugs/PE/Tags to CPT node
        'trvCPT.SelectedNode.Remove()

        Dim associatenode As myTreeNode

        associatenode = mynode.Clone
        associatenode.Key = mynode.Key
        associatenode.Text = mynode.Text

        associatenode.NodeName = mynode.Text
        associatenode.ImageIndex = 1
        associatenode.SelectedImageIndex = 1


        ''For De-Normalization
        'Dim tempNode As New myTreeNode
        'tempNode.DrugName = mynode.DrugName
        'tempNode.Dosage = mynode.Dosage
        'tempNode.DrugForm = mynode.DrugForm
        'For De-Normalization

        associatenode.ImageIndex = 1
        associatenode.SelectedImageIndex = 1

        trvCPTAssociation.Nodes.Item(0).Nodes.Add(associatenode)
        Dim MyChild As New myTreeNode
        MyChild.Text = "ICD9/10"
        MyChild.Key = -1
        MyChild.ImageIndex = 2
        MyChild.SelectedImageIndex = 2
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Drugs"
        MyChild.Key = -1
        MyChild.ImageIndex = 3
        MyChild.SelectedImageIndex = 3
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Patient Education"
        MyChild.Key = -1
        MyChild.ImageIndex = 5
        MyChild.SelectedImageIndex = 5
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Tags"
        MyChild.Key = -1
        MyChild.ImageIndex = 4
        MyChild.SelectedImageIndex = 4
        associatenode.Nodes.Add(MyChild)

        ''Added Rahul For new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014
        MyChild = New myTreeNode
        MyChild.Text = "Flowsheet"
        MyChild.Key = -1
        MyChild.ImageIndex = 10
        MyChild.SelectedImageIndex = 10
        associatenode.Nodes.Add(MyChild)


        MyChild = New myTreeNode
        MyChild.Text = "Orders & Results"
        MyChild.Key = -1
        MyChild.ImageIndex = 11
        MyChild.SelectedImageIndex = 11
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Order Templates"
        MyChild.Key = -1
        MyChild.ImageIndex = 12
        MyChild.SelectedImageIndex = 12
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Referral Letter"
        MyChild.Key = -1
        MyChild.ImageIndex = 13
        MyChild.SelectedImageIndex = 13
        associatenode.Nodes.Add(MyChild)
        associatenode.Expand()
        ''End

        Dim dt As DataTable
        Dim oclsCPTICD9 As clsCPTICD9Association = New clsCPTICD9Association
        dt = oclsCPTICD9.FetchICD9forUpdate(associatenode.Key)
        oclsCPTICD9.Dispose()
        oclsCPTICD9 = Nothing

        If (IsNothing(dt) = False) Then


            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                'Addded by Pradeep on 23122010
                'For Default tag change
                If dt.Rows(i).Item(1).ToString().Trim <> "" Then  ''''''''''''''''''If condition added by Ujwala - To skip blank node addition - as on 11112010
                    'End of  Code Added by Pradeep
                    'add cpt items to cpt node in icd9
                    If dt.Rows(i).Item(2) = "i" Then                        'CPT Description    CPTID

                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                        ''associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        tempnode.Text = dt.Rows(i).Item(1) '''''Description
                        tempnode.Key = dt.Rows(i).Item(0) '''''CPT ID
                        tempnode.nICDRevision = dt.Rows(i).Item("ICDRevision") '''''ICD Recision
                        associatenode.Nodes.Item(0).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(0).Expand()
                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                        'add drug items to drug node in icd9
                    ElseIf dt.Rows(i).Item(2) = "d" Then                    'Drugname + Dosage                              DrugID              Drugname        
                        'associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(3), dt.Rows(i).Item(0), CType(dt.Rows(i).Item(1), String)))
                        'For De-Normalization '\\Suraj 20090128
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        'tempnode.Key = mynode.Key
                        tempnode.Key = dt.Rows(i).Item(0)

                        tempnode.DrugName = dt.Rows(i).Item(1)
                        tempnode.Dosage = dt.Rows(i).Item(3)
                        tempnode.DrugForm = dt.Rows(i).Item(4)

                        tempnode.Route = dt.Rows(i).Item(5)
                        tempnode.Frequency = dt.Rows(i).Item(6)
                        tempnode.NDCCode = dt.Rows(i).Item(7)
                        tempnode.IsNarcotics = dt.Rows(i).Item(8)
                        tempnode.Duration = dt.Rows(i).Item(9)
                        tempnode.mpid = dt.Rows(i).Item(10)
                        tempnode.DrugQtyQualifier = dt.Rows(i).Item(11)

                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                        'Code Added by Mayuri:20091009
                        'To display both DrugName and DrugForm
                        'To check whether drugform is blank or not
                        tempnode.Text = tempnode.DrugName
                    'End Code Added by Mayuri:20091009
                    associatenode.Nodes.Item(1).Nodes.Add(tempnode)
                    associatenode.Nodes.Item(1).Expand()

                    'add PE items to PE node in icd9
                    ElseIf dt.Rows(i).Item(2) = "p" Then

                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                        ''associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        tempnode.Text = dt.Rows(i).Item(1) '''''Description
                        tempnode.Key = dt.Rows(i).Item(0) '''''PE ID
                        associatenode.Nodes.Item(2).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(2).Expand()
                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "t" Then
                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                        ''associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        tempnode.Text = dt.Rows(i).Item(1) '''''Description
                        tempnode.Key = dt.Rows(i).Item(0) '''''Tags ID
                        Dim strnodename As String = dt.Rows(i).Item(1) ''Description
                        Dim ind As Integer = strnodename.LastIndexOf("-")
                        If ind > -1 Then
                            strnodename = strnodename.Substring(0, ind)
                        End If
                        tempnode.NodeName = strnodename


                        ' tempnode.NodeName = dt.Rows(i).Item(1) '''''Description
                        associatenode.Nodes.Item(3).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(3).Expand()
                        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                        ''Added Rahul for new Association on 20101014
                    ElseIf dt.Rows(i).Item(2) = "f" Then
                        Dim flownode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        flownode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(4).Nodes.Add(flownode)
                        associatenode.Nodes.Item(4).Expand()
                        ' associatenode.Nodes.Item(4).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "l" Then
                        Dim labnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        labnode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(5).Nodes.Add(labnode)
                        associatenode.Nodes.Item(5).Expand()
                        ' associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                    ElseIf dt.Rows(i).Item(2) = "o" Then
                        Dim ordnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        ordnode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(6).Nodes.Add(ordnode)
                        associatenode.Nodes.Item(6).Expand()
                        'associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "r" Then
                        Dim reffnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        reffnode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(7).Nodes.Add(reffnode)
                        associatenode.Nodes.Item(7).Expand()
                        ' associatenode.Nodes.Item(7).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ''End
                    End If
                End If

            Next
            dt.Dispose()
            dt = Nothing
        End If

        'trvCPTAssociation.ExpandAll()
        trvCPTAssociation.Select()

        'treeindex = -1
        'End If

        'Ensure the newly created node is visible to the user and select it
        associatenode.EnsureVisible()
        trvCPTAssociation.SelectedNode = associatenode

        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
        CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

        'treeindex = mynode.Index
        'End If
    End Sub

    'Private Sub txtsearchCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchCPT.KeyPress
    '    If (e.KeyChar = ChrW(13)) Then
    '        trvCPT.Select()
    '    Else
    '        trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
    '    End If
    '    ''--Added by Anil on 20071213
    '    mdlGeneral.ValidateText(txtsearchCPT.Text, e)
    '    ''----
    'End Sub

    '\\ added by suraj 20090128 - for allowed '-' char in searchbox 
    Public Function ValidateTextSearch(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub txtsearchAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchAssociates.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvAssociates.Select()
        Else
            trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
        End If
        '\\ 20090128 added by suraj - for drugs search with allowed char '-'
        If pnlbtnDrugs.Dock = DockStyle.Top Then
            ValidateTextSearch(txtsearchAssociates.Text, e)
        Else
            mdlGeneral.ValidateText(txtsearchAssociates.Text, e)
        End If
        ' ''--Added by Anil on 20071213
        'mdlGeneral.ValidateText(txtsearchAssociates.Text, e)
        ' ''----
    End Sub


    Private Sub btnICD9_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.MouseEnter
        'btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        'btnICD9.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnICD9_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.MouseHover

        btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch


        '  Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnICD9, "ICD9 List")
    End Sub

    Private Sub btnDrugs_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseEnter
        'btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        'btnDrugs.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnDrugs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseHover

        btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnDrugs.BackgroundImageLayout = ImageLayout.Stretch

        '  Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnDrugs, "Drugs List")
    End Sub

    Private Sub btnPatientEducation_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.MouseEnter
        'btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        'btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnPatientEducation_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.MouseHover

        btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch

        '  Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnPatientEducation, "Patient Education List")
    End Sub

    Private Sub btnTags_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTags.MouseEnter
        'btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        'btnTags.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnTags_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTags.MouseHover

        btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnTags.BackgroundImageLayout = ImageLayout.Stretch


        '   Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnTags, "Tags List")
    End Sub

    Private Sub trvAssociates_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvAssociates.DoubleClick
        'MsgBox(CType(trvCPTAssociation.SelectedNode, myTreeNode).Text)
        Try
            Dim mynode As myTreeNode
            Dim targetnode As myTreeNode
            targetnode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
            mynode = CType(trvAssociates.SelectedNode, myTreeNode)
            'check if selected node is rootnode
            If Not IsNothing(targetnode) AndAlso Not IsNothing(mynode) Then
                AddAssociates(mynode, targetnode)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvAssociates.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                Dim targetnode1 As myTreeNode
                targetnode1 = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                mynode = CType(trvAssociates.SelectedNode, myTreeNode)
                'check if selected node is rootnode
                If Not IsNothing(targetnode1) AndAlso Not IsNothing(mynode) Then
                    AddAssociates(mynode, targetnode1)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal targetnode1 As myTreeNode)
        If Not mynode Is trvAssociates.Nodes(0) AndAlso Not targetnode1 Is trvCPTAssociation.Nodes(0) Then     'not root node
            Dim targetnode As myTreeNode
            'check if targetnode is node at second level in trvCPTAssociation treeview
            If targetnode1.Parent Is trvCPTAssociation.Nodes.Item(0) Or (targetnode1.Key = -1) Or (CType(targetnode1.Parent, myTreeNode).Key = -1) Then
                If targetnode1.Parent Is trvCPTAssociation.Nodes(0) Then
                    '' if Target Node's Parent node is Root Node i.e CPT Node
                    targetnode = targetnode1
                ElseIf (CType(targetnode1.Parent, myTreeNode).Key = -1) Then
                    '' Made CPT Node as selected Node which is Grand-Parent of selected node
                    targetnode = targetnode1.Parent.Parent
                Else
                    '' Made CPT Node as selected Node
                    targetnode = targetnode1.Parent
                End If

                Dim str As String
                str = mynode.Key
                Dim mytragetnode As myTreeNode
                Dim associatenode As myTreeNode

                associatenode = mynode.Clone
                associatenode.Key = mynode.Key
                associatenode.Text = mynode.Text
                '' chetan added 13 nov 2010
                'Dim strnodename As String = mynode.Text
                'Dim ind As Integer = strnodename.LastIndexOf("-")
                'If ind > -1 Then
                '    strnodename = strnodename.Substring(0, ind)
                'End If
                associatenode.NodeName = mynode.Text
                '' chetan added  13 nov 2010
                '\\ Suraj 20090128
                'For De-Normalization
                'SHUBHANGI 

                associatenode.DrugName = mynode.DrugName

                associatenode.Dosage = mynode.Dosage
                associatenode.DrugForm = mynode.DrugForm
                associatenode.Route = mynode.Route
                associatenode.Frequency = mynode.Frequency
                associatenode.NDCCode = mynode.NDCCode
                associatenode.IsNarcotics = mynode.IsNarcotics
                associatenode.Duration = mynode.Duration
                associatenode.mpid = mynode.mpid
                associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
                associatenode.nICDRevision = mynode.nICDRevision


                'For De-Normalization

                If pnlbtnDrugs.Dock = DockStyle.Top Then
                    If Not IsNothing(mynode.Tag) Then
                        associatenode.Tag = mynode.Tag
                    End If
                End If

                'if selected category is ICD9, add node to ICD9's child node 
                'in trvCPTAssociates
                If pnlbtnICD9.Dock = DockStyle.Top Or pnlbtnICD10.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(0).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                        If mytragetnode.nICDRevision <> associatenode.nICDRevision Then

                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(0).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(0).Expand()


                    'if selected category is Drugs, add node to Drugs child node 
                    'in trICD9Associates
                ElseIf pnlbtnDrugs.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(1).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(1).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(1).Expand()
                    'if selected category is PatientEducation, add node to PatientEducation child node 
                    'in trICD9Associates
                ElseIf pnlbtnPatientEducation.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(2).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(2).Expand()
                    'if selected category is Tags, add node to Tags child node 
                    'in trICD9Associates
                ElseIf pnlbtnMUPatientEducation.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(2).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(2).Expand()
                    'if selected category is Tags, add node to Tags child node 
                    'in trICD9Associates
                ElseIf pnlbtnTags.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(3).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(3).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(3).Expand()

                    ''Added Rahul For new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014
                ElseIf pnlFlowsheet.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(4).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(4).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(4).Expand()

                ElseIf pnlbtnLabOrders.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(5).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(5).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(5).Expand()

                ElseIf pnlbtnOrders.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(6).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(6).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(6).Expand()

                ElseIf pnlbtnReferrals.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(7).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        'If CType(mytragetnode.Key, String) = str Or (targetnode.Nodes.Item(7).Nodes.Count = 1) Then  '     commented by chetan on 16 feb 2010 to allow more items to add in Referral_Letter
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(7).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(7).Expand()
                    ''End
                End If
                mynode.EnsureVisible()
                trvCPTAssociation.ExpandAll()
                trvCPTAssociation.SelectedNode = mynode
                ' trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub tblMedication_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        Try
            Select Case e.Button.Text
                Case "&New"
                    RefreshCPT()
                Case "&Save"
                    SaveAssociation()
                Case "&Finish"
                    SaveAssociation()
                    Me.Close()
                Case "&Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearchAssociates_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtsearchAssociates.PreviewKeyDown

    End Sub

    Private Sub txtsearchAssociates_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchAssociates.TextChanged
        Try
            '''''''''''''''''''####################''''''''''''''''''''''''''
            '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
            Dim strSearchDetails As String
            If Trim(txtsearchAssociates.Text) <> "" Then
                strSearchDetails = Replace(txtsearchAssociates.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If
            '''''''''''''''''''####################''''''''''''''''''''''''''

            If Panel4.Visible = True Then ''---------> comment remove by suraj 20090205 

                'sarika 24th sept 07
                'add the code of new instr search
                If txtsearchAssociates.Tag <> Trim(strSearchDetails) Then
                    If pnlbtnDrugs.Dock = DockStyle.Top Then
                        AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 2)
                        txtsearchAssociates.Tag = Trim(strSearchDetails)
                        Exit Sub
                    ElseIf pnlbtnTags.Dock = DockStyle.Top Then
                        AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 3)
                        txtsearchAssociates.Tag = Trim(strSearchDetails)
                        Exit Sub
                    ElseIf pnlbtnPatientEducation.Dock = DockStyle.Top Then
                        AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 4)
                        txtsearchAssociates.Tag = Trim(strSearchDetails)
                        Exit Sub
                    ElseIf pnlbtnICD9.Dock = DockStyle.Top Then
                        AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 1)
                        txtsearchAssociates.Tag = Trim(strSearchDetails)
                        Exit Sub
                    End If

                    'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
                    '    AddDrugs(Trim(txtsearchCPT.Text))
                    'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
                    '    AddDrugs(Trim(txtsearchCPT.Text))
                    'End If
                    'If Len(Trim(txtsearchDrug.Text)) = 1 Then

                    'End If
                End If
                '--------------------------------------------------------
                If Trim(txtsearchAssociates.Text) <> "" Then
                    If trvAssociates.Nodes.Item(0).GetNodeCount(False) > 0 Then
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In trvAssociates.Nodes.Item(0).Nodes
                            'search against Description
                            If rbDescsearch.Checked = True Then
                                If UCase(Mid(mychildnode.Tag, 1, Len(Trim(strSearchDetails)))) = UCase(Trim(strSearchDetails)) Then
                                    'select matching node
                                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                    trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                                    '*************
                                    trvAssociates.SelectedNode = mychildnode
                                    txtsearchAssociates.Focus()
                                    Exit Sub
                                End If
                            Else
                                'search against ICD9 Code
                                Dim str As String
                                str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
                                str = Mid(str, 1, Len(Trim(strSearchDetails)))
                                If UCase(str) = UCase(Trim(strSearchDetails)) Then
                                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                    trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                                    '*************
                                    'select matching node
                                    trvAssociates.SelectedNode = mychildnode
                                    txtsearchAssociates.Focus()
                                    Exit Sub
                                End If
                            End If
                        Next

                        '' 20070922 - Mahesh - InString Searching 
                        'child node collection
                        For Each mychildnode In trvAssociates.Nodes.Item(0).Nodes
                            'search against Description
                            Dim str As String = ""

                            If rbDescsearch.Checked = True Then
                                str = Mid(mychildnode.Tag, 1, Len(Trim(strSearchDetails)))
                                If InStr(UCase(str), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                                    'select matching node
                                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                    trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                                    '*************
                                    trvAssociates.SelectedNode = mychildnode
                                    txtsearchAssociates.Focus()
                                    Exit Sub
                                End If
                            Else
                                'search against CPT Code
                                str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))

                                If InStr(UCase(str), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                    trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                                    '*************
                                    'select matching node
                                    trvAssociates.SelectedNode = mychildnode
                                    txtsearchAssociates.Focus()
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                End If
            Else '--------->
                'If Trim(txtsearchAssociates.Text) <> "" Then
                If pnlbtnDrugs.Dock = DockStyle.Top Then
                    If Len(Trim(txtsearchAssociates.Text)) <= 1 Then
                        If txtsearchAssociates.Tag <> Trim(txtsearchAssociates.Text) Then

                            PopulateAssociates(Associates.Drugs, Trim(txtsearchAssociates.Text))
                            txtsearchAssociates.Tag = Trim(txtsearchAssociates.Text)
                        End If
                    End If
                End If
                Dim mychildnode As myTreeNode
                'child node collection
                For Each mychildnode In trvAssociates.Nodes.Item(0).Nodes
                    'compare selected node text and entered text
                    Dim str As String
                    str = Mid(UCase(Trim(mychildnode.Text)), 1, Len(UCase(Trim(txtsearchAssociates.Text))))
                    If str = UCase(Trim(txtsearchAssociates.Text)) Then
                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                        trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                        '*************
                        trvAssociates.SelectedNode = mychildnode
                        txtsearchAssociates.Focus()
                        Exit Sub
                    End If
                Next

                ' '' 20070922 - Mahesh -  Instring Search
                'child node collection
                For Each mychildnode In trvAssociates.Nodes.Item(0).Nodes
                    'compare selected node text and entered text
                    Dim str As String
                    str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchAssociates.Text))))
                    If str = UCase(Trim(txtsearchAssociates.Text)) Then
                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                        trvAssociates.SelectedNode = trvAssociates.SelectedNode.LastNode
                        '*************
                        trvAssociates.SelectedNode = mychildnode
                        txtsearchAssociates.Focus()
                        Exit Sub
                    End If
                Next
            End If '--------->
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddSearchAssociates(ByVal strSearch As String, ByVal dt As DataTable, ByVal type As Integer)
        Try

            'For i = 0 To dt.Rows.Count - 1
            If (IsNothing(dt)) Then
                Exit Sub
            End If

            Dim dv As New DataView(dt)
            Dim i As Integer
            Dim tdt As DataTable

            If type = 1 Then
                'icd9
                If rbCodesearch.Checked = True Then
                    ''code
                    dv.RowFilter = "ICD9code Like '%" & strSearch & "%'"
                Else
                    ''description 
                    dv.RowFilter = "sDescription Like '%" & strSearch & "%'"
                End If

            ElseIf type = 2 Then
                'drugs
                'nDrugsID, sDrugNAme , isnull(sDosage,' ') 
                'dv.RowFilter = "sDrugNAme Like '%" & strSearch & "%'"  '' COMMENTED BY SUDHIR 20090202
                dv.RowFilter = "DrugNAme Like '%" & strSearch & "%'"
            ElseIf type = 3 Then
                'tags
                'select nTemplateID AS TemplateID , sTemplateName AS TemplateName
                dv.RowFilter = "TemplateName Like '%" & strSearch & "%'"
            ElseIf type = 4 Then
                'Patient education
                dv.RowFilter = "sTemplateName Like '%" & strSearch & "%'"
            End If


            ' tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trvAssociates.BeginUpdate()
            trvAssociates.Nodes(0).Nodes.Clear()
            trvAssociates.Visible = False

            For i = 0 To tdt.Rows.Count - 1
                'Dim mychildnode As myTreeNode
                If type = 1 Then
                    'ICD9
                    trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1), tdt.Rows(i)(0), CType(tdt.Rows(i)(2), String)))

                ElseIf type = 2 Then
                    'Drugs
                    trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1) & "-" & tdt.Rows(i)(2), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String)))

                Else
                    '3 and 4
                    'tags and PE
                    trvAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String)))

                End If
                'rootnode.Nodes.Add(dt.Rows(i)(1))
            Next
            trvAssociates.Visible = True
            trvAssociates.ExpandAll()
            trvAssociates.Select()
            trvAssociates.SelectedNode = trvAssociates.Nodes.Item(0)
            txtsearchAssociates.Focus()
            dv.Dispose()
            dv = Nothing
            tdt.Dispose()
            tdt = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            'objex.ErrorMessage = ""
            'Throw objex
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvAssociates.EndUpdate()
        End Try
    End Sub

    Private Sub trAssociates_ContextMenuChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvAssociates.ContextMenuChanged
        txtsearchCPT.Text = ""
    End Sub

    Private Sub rbSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dtOrderbyCode = FillCPT(dtOrderbyCode, OrderBy.Code)

            txtsearchCPT.Text = ""
            txtsearchCPT.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub rbSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dtOrderbyDesc = FillCPT(dtOrderbyDesc, OrderBy.Description)

            txtsearchCPT.Text = ""
            txtsearchCPT.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub frmICD9Association_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        txtsearchAssociates.Text = ""
        txtsearchAssociates.Tag = ""
    End Sub



    Private Sub tblRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblRefresh.Click
        RefreshCPT()
    End Sub

    Private Sub tblOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblOK.Click
        SaveAssociation()
    End Sub

    Private Sub tblFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblFinish.Click
        SaveAssociation()
        Me.Close()
    End Sub

    Private Sub tblClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblClose.Click
        Me.Close()
    End Sub

    Private Sub btnDrugs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseLeave
        'If pnlbtnDrugs.Dock = DockStyle.Bottom Then
        '    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        '    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        'End If
        If btnDrugs.Tag = "Selected" Then
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnICD9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.MouseLeave
        'If pnlbtnICD9.Dock = DockStyle.Bottom Then
        '    btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        '    btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        'Else
        '    btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        '    btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        'End If
        If btnICD9.Tag = "Selected" Then
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnPatientEducation_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.MouseLeave
        'If pnlbtnPatientEducation.Dock = DockStyle.Bottom Then
        '    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        '    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        'End If
        If btnPatientEducation.Tag = "Selected" Then
            btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnTags_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTags.MouseLeave
        'If pnlbtnTags.Dock = DockStyle.Bottom Then
        '    btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        '    btnTags.BackgroundImageLayout = ImageLayout.Stretch
        'End If
        If btnTags.Tag = "Selected" Then
            btnTags.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnTags.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnTags.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnTags.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub rbCodesearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbCodesearch.Click
        Try
            PopulateAssociates(Associates.ICD9, strSortByCode)
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub rbDescsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDescsearch.Click
        Try
            PopulateAssociates(Associates.ICD9, strSortByDesc)
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    '''''Following code lines are addded by Anil 0n 27/09/07 at 10:30 a.m.
    '''''This code clears the search textboxes, gets the focus on the root of theTreeView  on click of Refresh button.
    Private Sub tsbRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRefresh.Click
        txtsearchAssociates.Clear()
        txtsearchCPT.Clear()
        trvAssociates.CollapseAll()
        trvAssociates.Focus()
        trvAssociates.ExpandAll()
        'trvCPT.CollapseAll()
        'trvCPT.Focus()
        'trvCPT.ExpandAll()
        trvCPTAssociation.CollapseAll()
        trvCPTAssociation.ExpandAll()
        'rbSearch1.Checked = False
        'rbSearch2.Checked = True
        rbCodesearch.Checked = False
        rbDescsearch.Checked = True

        '''''upto here the code is added
    End Sub
    '<<<<<<<<<<<<<Ojeswini_17Dec2008>>>>>>>>>>>>>>>>>>>>>
    Private Sub rbSearch2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If rbSearch2.Checked = True Then

        '    rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Bold)
        '    'rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Regular)
        'Else
        '    rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Regular)
        '    'rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Bold)

        'End If


    End Sub

    Private Sub rbSearch1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If rbSearch1.Checked = True Then

        '    rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Bold)
        '    'rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Regular)
        'Else
        '    rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Regular)
        '    'rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Bold)

        'End If
    End Sub

    Private Sub rbCodesearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCodesearch.CheckedChanged

        If rbCodesearch.Checked = True Then

            rbCodesearch.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            'rbCodesearch.Font = New Font("Tahoma", 9, FontStyle.Regular)
        Else
            rbCodesearch.Font = gloGlobal.clsgloFont.gFont ' New Font("Tahoma", 9, FontStyle.Regular)
            'rbCodesearch.Font = New Font("Tahoma", 9, FontStyle.Bold)

        End If
    End Sub

    Private Sub rbDescsearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDescsearch.CheckedChanged

        If rbDescsearch.Checked = True Then

            rbDescsearch.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            'rbDescsearch.Font = New Font("Tahoma", 9, FontStyle.Regular)
        Else
            rbDescsearch.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            'rbDescsearch.Font = New Font("Tahoma", 9, FontStyle.Bold)

        End If
    End Sub
    '<<<<<<<<<<<<<Ojeswini_17Dec2008>>>>>>>>>>>>>>>>>>>>>

    Private Sub trvCPTAssociation_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCPTAssociation.AfterSelect

    End Sub
    '' Mayuri- Event handled on 20090910 

    Private Sub GloUC_trvCPT_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvCPT.NodeAdded
        Try
            Dim dtAssociation As DataTable
            '' To Get Already Associated Template with Selected CPT
            Dim oclsCPTICD9 As clsCPTICD9Association = New clsCPTICD9Association
            dtAssociation = oclsCPTICD9.FetchICD9forUpdate(ChildNode.Tag)
            oclsCPTICD9.Dispose()
            oclsCPTICD9 = Nothing
            '' If Association found then change the Image of Treenode 
            If Not IsNothing(dtAssociation) Then
                If dtAssociation.Rows.Count > 0 Then
                    ChildNode.ImageIndex = 1
                    ChildNode.SelectedImageIndex = 1
                End If
                dtAssociation.Dispose()
                dtAssociation = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvCPT_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvCPT.NodeMouseDoubleClick
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            'Dim oNodeToAdd As New myTreeNode
            'oNodeToAdd.Key = oNode.ID
            'oNodeToAdd.Text = oNode.Text
            'If Not IsNothing(oNode) Then
            '    AddNode(oNodeToAdd)
            'End If
            ''

            If Not IsNothing(oNode) Then
                Dim mynode As New myTreeNode

                mynode.Key = oNode.ID
                mynode.Text = oNode.Text
                AddNode(mynode)
                mynode.Dispose()
                mynode = Nothing
            End If
            ''
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvCPT.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)

                If Not IsNothing(oNode) Then
                    Dim oNodeToAdd As New myTreeNode
                    oNodeToAdd.Key = oNode.ID
                    oNodeToAdd.Text = oNode.Text
                    AddNode(oNodeToAdd)
                    oNodeToAdd.Dispose()
                    oNodeToAdd = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GloUC_trvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvAssociates.NodeMouseDoubleClick
        'MsgBox(CType(trvCPTAssociation.SelectedNode, myTreeNode).Text)
        Try
            Dim targetnode1 As myTreeNode = CType(trvCPTAssociation.SelectedNode, myTreeNode)

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)


            'check if selected node is rootnode
            If Not IsNothing(targetnode1) AndAlso Not IsNothing(oNode) Then
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
                oNodeToAdd.nICDRevision = oNode.ICDRevision
                If Not IsNothing(oNodeToAdd) Then
                    AddAssociates(oNodeToAdd, targetnode1)
                    'Shubhangi 20091208
                    'Check the setting Reset search text box after assiging category
                    If gblnResetSearchTextBox = True Then
                        GloUC_trvAssociates.txtsearch.ResetText()
                    End If
                    oNodeToAdd.Dispose()
                    oNodeToAdd = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvAssociates.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                Dim targetnode1 As myTreeNode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode)

                If Not IsNothing(oNode) Then
                    Dim oNodeToAdd As New myTreeNode
                    oNodeToAdd.Key = oNode.ID
                    'SHUBHANGI 2010803 IF THE DRUG IS SELECTED 
                    If (btnDrugs.Tag = "Selected") Then

                        If (oNode.Description <> "") Then ' IF DOSAGE FIELD IS NOT EMPTY
                            oNodeToAdd.Text = oNode.Code & " - " & oNode.Description & " - " & oNode.DrugForm
                        Else
                            oNodeToAdd.Text = oNode.Code & " - " & oNode.DrugForm
                        End If
                        oNodeToAdd.DrugName = oNode.Code
                        oNodeToAdd.DrugForm = oNode.DrugForm
                        oNodeToAdd.Route = oNode.Route
                        oNodeToAdd.Frequency = oNode.Frequency
                        oNodeToAdd.NDCCode = oNode.NDCCode
                        oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                        oNodeToAdd.Duration = oNode.Duration
                        oNodeToAdd.mpid = oNode.mpid
                        oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier

                    ElseIf (btnICD9.Tag = "Selected") Then
                        If (oNode.Code <> "") Then
                            oNodeToAdd.Text = oNode.Code & " - " & oNode.Description
                        Else
                            oNodeToAdd.Text = oNode.Description
                        End If
                        oNodeToAdd.nICDRevision = oNode.ICDRevision
                    ElseIf (btnICD10.Tag = "Selected") Then
                        If (oNode.Code <> "") Then
                            oNodeToAdd.Text = oNode.Code & " - " & oNode.Description
                        Else
                            oNodeToAdd.Text = oNode.Description
                        End If
                        oNodeToAdd.nICDRevision = oNode.ICDRevision
                    Else
                        oNodeToAdd.Text = oNode.Description
                    End If
                    'check if selected node is rootnode
                    If Not IsNothing(targetnode1) AndAlso Not IsNothing(oNode) Then
                        If Not IsNothing(oNodeToAdd) Then
                            AddAssociates(oNodeToAdd, targetnode1)
                        End If
                    End If
                    If Not IsNothing(oNodeToAdd) Then
                        oNodeToAdd.Dispose()
                        oNodeToAdd = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Shubhangi 2001212
    'Display all associated CPT records
    Private Sub rbtAssociated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAssociated.Click
        Dim dtAssociation As DataTable
        'dtAssociation = oclsCPTICD9.FetchassociatedCPT()
        Dim oclsCPTAssociation As clsCPTAssociation
        oclsCPTAssociation = New clsCPTAssociation
        dtAssociation = oclsCPTAssociation.FillCPT(OrderBy.Description, GloUC_trvCPT.txtsearch.Text.Trim, 1)
        oclsCPTAssociation.Dispose()
        oclsCPTAssociation = Nothing

        Dim strAssociated As String = ""
        Dim dv As DataView
        dv = dtAssociation.DefaultView
        'strAssociated = "isICD9associated = 'true' or isDRUGassociated= 'true' or isTagGassociated='true' or isPatientEducationGassociated='true' or isReferralLetterGassociated = 'true' or isOrdersGassociated = 'true' or isLabOrderGassociated = 'true' or isFlowsheetGassociated='true' "
        strAssociated = "SmartTreatmentId = 'true'"
        If dtAssociation.Rows.Count > 0 Then
            dv.RowFilter = strAssociated
        End If
        Dim dt As DataTable
        dt = dv.ToTable
        oclsCPTAssociation = Nothing
        'Bind filtrated data to control
        GloUC_trvCPT.DataSource = dtAssociation
        GloUC_trvCPT.ValueMember = dtAssociation.Columns("CPTID").ColumnName
        GloUC_trvCPT.Tag = dtAssociation.Columns("CPTID").ColumnName
        GloUC_trvCPT.DescriptionMember = dtAssociation.Columns("sDescription").ColumnName
        GloUC_trvCPT.CodeMember = dtAssociation.Columns("CPTCode").ColumnName
        GloUC_trvCPT.SmartTreatmentId = dtAssociation.Columns("SmartTreatmentId").ColumnName
        GloUC_trvCPT.IsCPTSearch = True
        GloUC_trvCPT.FillTreeView()
        dt.Dispose()
        dt = Nothing
        dv.Dispose()
        dv = Nothing
    End Sub
    'Shubhangi 20091212
    'Display Unassociated records
    Private Sub rbtUnassociated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtUnassociated.Click
        Dim dtAssociation As DataTable
        'dtAssociation = oclsCPTICD9.FetchassociatedCPT()
        Dim oclsCPTAssociation As clsCPTAssociation
        oclsCPTAssociation = New clsCPTAssociation
        dtAssociation = oclsCPTAssociation.FillCPT(OrderBy.Description, GloUC_trvCPT.txtsearch.Text.Trim, 2)
        oclsCPTAssociation.Dispose()
        oclsCPTAssociation = Nothing
        Dim strAssociated As String = ""
        Dim dv As DataView
        dv = dtAssociation.DefaultView
        'If GloUC_trvCPT.txtsearch.Text.Trim() <> "" Then
        '    strAssociated = "isICD9associated = 'false' And isDRUGassociated= 'false' And isTagGassociated='false' And isPatientEducationGassociated='false' AND isReferralLetterGassociated = 'false' AND isOrdersGassociated = 'false' AND isLabOrderGassociated = 'false' AND isFlowsheetGassociated = 'false' And sCPTCode LIKE '" & GloUC_trvCPT.txtsearch.Text.Trim() & "%'"
        'Else
        '    strAssociated = "isICD9associated = 'false' And isDRUGassociated= 'false' And isTagGassociated='false' And isPatientEducationGassociated='false' AND isReferralLetterGassociated = 'false' AND isOrdersGassociated = 'false' AND isLabOrderGassociated = 'false' AND isFlowsheetGassociated = 'false'"
        'End If
        'If GloUC_trvCPT.txtsearch.Text.Trim() <> "" Then
        '    strAssociated = "SmartTreatmentId = 'false' And CPTCode LIKE '" & GloUC_trvCPT.txtsearch.Text.Trim() & "%'"
        'Else
        strAssociated = "SmartTreatmentId = 'false'  "
        'End If
        If dtAssociation.Rows.Count > 0 Then
            dv.RowFilter = strAssociated
        End If
        Dim dt As DataTable
        dt = dv.ToTable
        'Bind filtrated data to control
        'oclsCPTAssociation = Nothing
        GloUC_trvCPT.DataSource = dtAssociation
        GloUC_trvCPT.ValueMember = dtAssociation.Columns("CPTID").ColumnName

        GloUC_trvCPT.Tag = dtAssociation.Columns("CPTID").ColumnName
        GloUC_trvCPT.DescriptionMember = dtAssociation.Columns("sDescription").ColumnName
        GloUC_trvCPT.CodeMember = dtAssociation.Columns("CPTCode").ColumnName
        GloUC_trvCPT.SmartTreatmentId = dtAssociation.Columns("SmartTreatmentId").ColumnName
        GloUC_trvCPT.IsCPTSearch = True
        GloUC_trvCPT.FillTreeView()
        dt.Dispose()
        dt = Nothing
        dv.Dispose()
        dv = Nothing

    End Sub
    'Shubhangi 20091212
    'Display all - Associated & unassociated records.
    Private Sub rbtAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAll.Click
        'Dim dt As New DataTable
        'dt = oclsCPTICD9.FillControls(Associates.CPT, "DESC")
        Dim dtAssociation As DataTable
        'dtAssociation = oclsCPTICD9.FetchassociatedCPT()
        Dim oclsCPTAssociation As clsCPTAssociation
        oclsCPTAssociation = New clsCPTAssociation
        dtAssociation = oclsCPTAssociation.FillCPT(OrderBy.Description, GloUC_trvCPT.txtsearch.Text.Trim)
        Dim strAssociated As String = ""
        Dim dv As DataView
        dv = dtAssociation.DefaultView
        If GloUC_trvCPT.txtsearch.Text.Trim() <> "" Then
            strAssociated = "CPTCode LIKE '" & GloUC_trvCPT.txtsearch.Text.Trim() & "%'"
        Else
            strAssociated = ""
        End If
        If dtAssociation.Rows.Count > 0 Then
            dv.RowFilter = strAssociated
        End If
        Dim dt As DataTable
        dt = dv.ToTable
        'Bind filtrated data to control
        oclsCPTAssociation.Dispose()
        oclsCPTAssociation = Nothing
        GloUC_trvCPT.DataSource = dt
        GloUC_trvCPT.ValueMember = dt.Columns("CPTID").ColumnName
        ''20090910
        GloUC_trvCPT.Tag = dt.Columns("CPTID").ColumnName
        'GloUC_trvCPT.Tag = dt.Columns(0).ColumnName
        GloUC_trvCPT.DescriptionMember = dt.Columns("sDescription").ColumnName
        GloUC_trvCPT.CodeMember = dt.Columns("CPTCode").ColumnName
        GloUC_trvCPT.SmartTreatmentId = dt.Columns("SmartTreatmentId").ColumnName
        GloUC_trvCPT.IsCPTSearch = True
        GloUC_trvCPT.FillTreeView()
        dv.Dispose()
        dv = Nothing
        dtAssociation.Dispose()
        dtAssociation = Nothing
    End Sub

    Private Sub rbtAssociated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAssociated.CheckedChanged
        If rbtAssociated.Checked = True Then
            rbtAssociated.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtAssociated.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtUnassociated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtUnassociated.CheckedChanged
        If rbtUnassociated.Checked = True Then
            rbtUnassociated.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtUnassociated.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAll.CheckedChanged
        If rbtAll.Checked = True Then
            rbtAll.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbtAll.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub trvCPTAssociation_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCPTAssociation.AfterCheck
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
        If bChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked)
        End If
        If bParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked)
        End If
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101012
    End Sub
#Region "Added Rahul for new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014"

    Private Sub putallpaneldown()
        Try
            pnlRadiobtn.Visible = False

            pnlbtnICD9.Dock = DockStyle.Bottom
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
            btnICD9.Tag = "UnSelected"

            pnlbtnMUPatientEducation.Dock = DockStyle.Bottom
            btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            btnMUPatientEducation.Tag = "UnSelected"

            pnlbtnPatientEducation.Dock = DockStyle.Bottom
            btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            btnPatientEducation.Tag = "UnSelected"

            pnlbtnTags.Dock = DockStyle.Bottom
            btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnTags.BackgroundImageLayout = ImageLayout.Stretch
            btnTags.Tag = "UnSelected"


            pnlbtnDrugs.Dock = DockStyle.Bottom
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            btnDrugs.Tag = "UnSelected"

            pnlbtnReferrals.Dock = DockStyle.Bottom
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
            btnReferrals.Tag = "UnSelected"

            pnlbtnOrders.Dock = DockStyle.Bottom
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnOrders.Tag = "UnSelected"

            pnlbtnLabOrders.Dock = DockStyle.Bottom
            btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
            btnLabOrders.Tag = "UnSelected"

            pnlFlowsheet.Dock = DockStyle.Bottom
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
            btnFlowsheet.Tag = "UnSelected"

            pnlbtnICD10.Dock = DockStyle.Bottom
            btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnICD10.BackgroundImageLayout = ImageLayout.Stretch
            btnICD10.Tag = "UnSelected"

            pnlSearch.BringToFront()
            pnltrvAssociates.BringToFront()

            rbCodesearch.Checked = False
            rbDescsearch.Checked = True
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Tag = ""


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReferrals_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.Click
        If btnReferrals.Tag = "UnSelected" Then
            putallpaneldown()
            PopulateAssociates(Associates.Referral)
            GloUC_trvAssociates.txtsearch.Text = ""
        End If
    End Sub

    Private Sub btnOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.Click
        If btnOrders.Tag = "UnSelected" Then
            putallpaneldown()
            PopulateAssociates(Associates.Order)
            GloUC_trvAssociates.txtsearch.Text = ""
        End If
    End Sub

    Private Sub btnLabOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabOrders.Click
        If btnLabOrders.Tag = "UnSelected" Then
            putallpaneldown()
            PopulateAssociates(Associates.LabOrder)
            GloUC_trvAssociates.txtsearch.Text = ""
        End If       
    End Sub

    Private Sub btnFlowsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlowsheet.Click
        If btnFlowsheet.Tag = "UnSelected" Then
            putallpaneldown()
            PopulateAssociates(Associates.Flow)
            GloUC_trvAssociates.txtsearch.Text = ""
        End If
        
    End Sub

    Private Sub btnReferrals_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReferrals.MouseHover
        btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        '  Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnReferrals, "Referral Letter List")
    End Sub

    Private Sub btnReferrals_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReferrals.MouseLeave
        If btnReferrals.Tag = "Selected" Then
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnOrders_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrders.MouseHover
        ' Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnOrders, "Orders List")

        btnOrders.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnOrders.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnOrders_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrders.MouseLeave
        If btnOrders.Tag = "Selected" Then
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnLabOrders_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabOrders.MouseHover
        '   Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnLabOrders, "Orders & Results List")

        btnLabOrders.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLabOrders_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabOrders.MouseLeave
        If btnLabOrders.Tag = "Selected" Then
            btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnFlowsheet_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlowsheet.MouseHover
        '  Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnFlowsheet, "Flow Sheet List")

        btnFlowsheet.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnFlowsheet_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlowsheet.MouseLeave
        If btnFlowsheet.Tag = "Selected" Then
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub trvCPTAssociation_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvCPTAssociation.NodeMouseClick
        Select Case e.Node.Text
            Case "Drugs"
                If btnDrugs.Tag = "Selected" Then
                    Exit Sub
                End If            
            Case "Patient Education"
                If btnPatientEducation.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "Tags"
                If btnTags.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "Referral Letter"
                If btnReferrals.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "Flowsheet"
                If btnFlowsheet.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "Order Templates"
                If btnOrders.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "Orders & Results"
                If btnLabOrders.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "ICD10"
                If btnICD10.Tag = "Selected" Then
                    Exit Sub
                End If
            Case "ICD9/10"
                If btnICD9.Tag = "Selected" Then
                    Exit Sub
                End If
        End Select

        If ((e.Node.Text = "Drugs") Or (e.Node.Text = "ICD9/10") Or (e.Node.Text = "Patient Education") Or (e.Node.Text = "Tags") Or (e.Node.Text = "Referral Letter") Or (e.Node.Text = "Flowsheet") Or (e.Node.Text = "Order Templates") Or (e.Node.Text = "Orders & Results")) Then
            putallpaneldown()
        End If


        Select Case e.Node.Text

            Case "Drugs"

                PopulateAssociates(Associates.Drugs)

            Case "ICD9/10"

                If gblnIcd10MasterTransition = True Then
                    PopulateAssociates(Associates.ICD10)
                Else
                    PopulateAssociates(Associates.ICD9)
                End If



            Case "Patient Education"

                PopulateAssociates(Associates.PE)

            Case "Tags"

                PopulateAssociates(Associates.Tags)

            Case "Referral Letter"

                PopulateAssociates(Associates.Referral)

            Case "Flowsheet"

                PopulateAssociates(Associates.Flow)

            Case "Order Templates"

                PopulateAssociates(Associates.Order)

            Case "Orders & Results"

                PopulateAssociates(Associates.LabOrder)

            Case "ICD10"

                PopulateAssociates(Associates.ICD10)

        End Select
    End Sub
#End Region


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    ''Sanjog- Added on 2011 Jan 17 to show the context menu on Property key press 
    Private Sub trvCPTAssociation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvCPTAssociation.KeyDown
        Try
            If e.KeyCode = Keys.Apps Then
                If Not IsNothing(trvCPTAssociation.SelectedNode) Then
                    If (trvCPTAssociation.SelectedNode.Level <> 0) Then
                        If trvCPTAssociation.SelectedNode.Parent.Text = "Tags" Then
                            'Try
                            '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                            '        trvCPTAssociation.ContextMenu.Dispose()
                            '        trvCPTAssociation.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvCPTAssociation.ContextMenu = cntTags
                            Exit Sub
                        End If
                    End If
                    If trvCPTAssociation.Nodes.Item(0).Text = trvCPTAssociation.SelectedNode.Text Or trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Or (CType(trvCPTAssociation.SelectedNode, myTreeNode).Key = -1) Then
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = cntICD9Association
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Sanjog- Added on 2011 Jan 17 to show the context menu on Property key press
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("CPT", "Download")
            'code added by seema on 20120221 to prevent opening of multiple windows
            If gloCommunity.Classes.clsGeneral.getInstance(FrmgloCommunityViewData.Name, FrmgloCommunityViewData.Text) = False Then
                Try

                    With FrmgloCommunityViewData
                        .MdiParent = Application.OpenForms("MainMenu")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                Catch objErr As Exception
                    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ''Added for fixed Bug # : 35658 on 20120904
    Private Function CheckUser() As Boolean
        Dim oCommunity As gloCommunity.Classes.clsgloCommunityUsers = Nothing
        Dim _blnUserCheck As Boolean = False
        Try
            oCommunity = New gloCommunity.Classes.clsgloCommunityUsers()
            _blnUserCheck = oCommunity.CheckAuthentication()
        Catch ex As Exception
        Finally
            If Not IsNothing(oCommunity) Then

                oCommunity = Nothing
            End If
        End Try
        Return _blnUserCheck
    End Function
    ''End

    Private Sub btnICD10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD10.Click
        Try
            If btnICD10.Tag = "UnSelected" Then
                putallpaneldown()
                GloUC_trvAssociates.txtsearch.Text = ""
                PopulateAssociates(Associates.ICD10)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_SearchFired() Handles GloUC_trvAssociates.SearchFired

        If pnlbtnICD9.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.ICD9)
        ElseIf pnlbtnICD10.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.ICD10)
        ElseIf pnlbtnDrugs.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.Drugs)
        ElseIf pnlbtnPatientEducation.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.PE)
        ElseIf pnlbtnTags.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.Tags)
        ElseIf pnlFlowsheet.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.Flow)
        ElseIf pnlbtnLabOrders.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.LabOrder)
        ElseIf pnlbtnOrders.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.Order)
        ElseIf pnlbtnReferrals.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.Referral)
        ElseIf pnlbtnMUPatientEducation.Dock = DockStyle.Top Then
            PopulateAssociates(Associates.MUPE)
        End If
    End Sub


    Private Sub btnICD10_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD10.MouseHover


        btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnICD10.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnICD10_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD10.MouseLeave
        If btnICD10.Tag = "Selected" Then
            btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD10.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD10.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub
    ''Added event to add top 100 logic
    Private Sub GloUC_trvCPT_SearchFired() Handles GloUC_trvCPT.SearchFired
        If rbtAssociated.Checked Then
            rbtAssociated_Click(Nothing, Nothing)
        ElseIf rbtUnassociated.Checked Then
            rbtUnassociated_Click(Nothing, Nothing)
        ElseIf rbtAll.Checked Then
            Dim dt As DataTable = Nothing
            Call FillCPT(dt, OrderBy.Description)
        End If

    End Sub

    Private Sub btnMUPatientEducation_Click(sender As System.Object, e As System.EventArgs) Handles btnMUPatientEducation.Click
        putallpaneldown()
        PopulateAssociates(Associates.MUPE)
        GloUC_trvAssociates.txtsearch.Text = ""
    End Sub

    Private Sub btnMUPatientEducation_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnMUPatientEducation.MouseLeave
        If btnMUPatientEducation.Tag = "Selected" Then
            btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnMUPatientEducation_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnMUPatientEducation.MouseHover
        btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch

        '  Dim tooltipnew As New ToolTip
        tooltipnew.SetToolTip(btnPatientEducation, "MU Patient Education List")
    End Sub
End Class
