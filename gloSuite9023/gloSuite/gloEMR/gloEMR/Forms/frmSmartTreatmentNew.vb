
Option Compare Text
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMRGeneralLibrary
Imports gloTaskMail


Public Class frmSmartTreatmentNew

    Inherits System.Windows.Forms.Form
    Dim objclsSmartTreatment As clsSmartTreatment
    Dim oclsSmartTreatment As clsSmartTreatment
    Dim dtOrderbyCode As DataTable
    Dim dtOrderbyDesc As DataTable
    '' 
    'Friend WithEvents wdReferrals As AxDSOFramer.AxFramerControl
    '' 
    Dim objReferralsDBLayer As New ClsReferralsDBLayer
    Public Shared nRefTempID As Int64 = 0
    Private m_ExamFilePath As String
    Private ExamProviderId As Long
    Public blnExamFinished As Boolean
    Dim WithEvents frmExamChild As IExamChildEvents
    Public dtDos As DateTime
    

    Private m_VisitID As Long
    Private m_ExamID As Long
    Dim arrLabs As New ArrayList
    Dim arrRadiology As New ArrayList
    Dim arrTemplate As New ArrayList
    Dim arrFlow As New ArrayList
    Dim IsFormLoad As Boolean = False
    Dim m_ExamDate As DateTime
    Public mycaller As frmPatientExam
    Friend WithEvents tblSmartTreatment As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Dim arrPE As New ArrayList
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents C1Dignosis As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GloUC_trvCPT As gloUserControlLibrary.gloUC_TreeView
    Private m_ProviderID As Long

    ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013
    Private _PatientID As Long
    ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013

    Private arrCPTDrivenTreatment As New ArrayList
    Dim objclsPatientEducation As New clsPatientEducation
    Dim objclsgloEmrPrescription As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)

    Dim _dt As DataTable



#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal VisitID As Long, ByVal ExamID As Long, ByVal ExamDate As DateTime, ByVal PatientID As Long, Optional ByVal m_ExamProviderID As Long = 0)
        MyBase.New()
        m_VisitID = VisitID
        m_ExamID = ExamID
        m_ExamDate = ExamDate
        m_ProviderID = m_ExamProviderID

        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013
        _PatientID = PatientID
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim dtpContextMenu As ContextMenu() = {cntTags, cntCPTAssociation}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)
                gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
            Catch ex As Exception

            End Try


            If (IsNothing(objReferralsDBLayer) = False) Then
                objReferralsDBLayer.Dispose()
                objReferralsDBLayer = Nothing
            End If

            If (IsNothing(oclsSmartTreatment) = False) Then
                oclsSmartTreatment.Dispose()
                oclsSmartTreatment = Nothing
            End If
            If (IsNothing(oclsSmartTreatment) = False) Then
                oclsSmartTreatment.Dispose()
                oclsSmartTreatment = Nothing
            End If
            If (IsNothing(objclsgloEmrPrescription) = False) Then
                objclsgloEmrPrescription.Dispose()
                objclsgloEmrPrescription = Nothing
            End If
            If (IsNothing(objclsPatientEducation) = False) Then
                objclsPatientEducation.Dispose()
                objclsPatientEducation = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditICD9Item As System.Windows.Forms.MenuItem 'added by chetan on 17 feb 2010 to edit that parent 
    Friend WithEvents ImgAssociation As System.Windows.Forms.ImageList
    Friend WithEvents rbSearch2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSearch1 As System.Windows.Forms.RadioButton
    Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents trvCPT As System.Windows.Forms.TreeView
    Friend WithEvents trvCPTAssociation As System.Windows.Forms.TreeView
    Friend WithEvents pnlSearchCPT As System.Windows.Forms.Panel
    Friend WithEvents txtsearchCPT As System.Windows.Forms.TextBox
    Friend WithEvents pnlSmartTreatment As System.Windows.Forms.Panel
    Friend WithEvents cntCPTAssociation As System.Windows.Forms.ContextMenu
    Friend WithEvents imgSmartDia As System.Windows.Forms.ImageList
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSmartTreatmentNew))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgSmartDia = New System.Windows.Forms.ImageList(Me.components)
        Me.trvCPT = New System.Windows.Forms.TreeView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearchCPT = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlSearchCPT = New System.Windows.Forms.Panel()
        Me.rbSearch2 = New System.Windows.Forms.RadioButton()
        Me.rbSearch1 = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.C1Dignosis = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.trvCPTAssociation = New System.Windows.Forms.TreeView()
        Me.cntTags = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.tblSmartTreatment = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.ImgAssociation = New System.Windows.Forms.ImageList(Me.components)
        Me.cntCPTAssociation = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.mnuEditICD9Item = New System.Windows.Forms.MenuItem()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.pnlSearchCPT.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.tblSmartTreatment.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.pnlSearch)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 562)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GloUC_trvCPT)
        Me.Panel2.Controls.Add(Me.trvCPT)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(240, 562)
        Me.Panel2.TabIndex = 2
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
        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayContextMenuStrip = Nothing
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
        Me.GloUC_trvCPT.ImageIndex = 9
        Me.GloUC_trvCPT.ImageList = Me.imgSmartDia
        Me.GloUC_trvCPT.ImageObject = Nothing
        Me.GloUC_trvCPT.Indicator = Nothing
        Me.GloUC_trvCPT.IsCPTSearch = False
        Me.GloUC_trvCPT.IsDiagnosisSearch = False
        Me.GloUC_trvCPT.IsDrug = False
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSearchForEducationMapping = False
        Me.GloUC_trvCPT.IsSystemCategory = Nothing
        Me.GloUC_trvCPT.Location = New System.Drawing.Point(3, 3)
        Me.GloUC_trvCPT.MaximumNodes = 1000
        Me.GloUC_trvCPT.Name = "GloUC_trvCPT"
        Me.GloUC_trvCPT.NDCCodeMember = Nothing
        Me.GloUC_trvCPT.ParentImageIndex = 0
        Me.GloUC_trvCPT.ParentMember = Nothing
        Me.GloUC_trvCPT.RouteMember = Nothing
        Me.GloUC_trvCPT.RowOrderMember = Nothing
        Me.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT.SearchBox = True
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 9
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(237, 556)
        Me.GloUC_trvCPT.SmartTreatmentId = Nothing
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 47
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'imgSmartDia
        '
        Me.imgSmartDia.ImageStream = CType(resources.GetObject("imgSmartDia.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSmartDia.TransparentColor = System.Drawing.Color.Transparent
        Me.imgSmartDia.Images.SetKeyName(0, "ICD 09.ico")
        Me.imgSmartDia.Images.SetKeyName(1, "Drugs.ico")
        Me.imgSmartDia.Images.SetKeyName(2, "Pat Education.ico")
        Me.imgSmartDia.Images.SetKeyName(3, "Tag.ico")
        Me.imgSmartDia.Images.SetKeyName(4, "Specialty.ico")
        Me.imgSmartDia.Images.SetKeyName(5, "CPT.ico")
        Me.imgSmartDia.Images.SetKeyName(6, "Smart Treatment.ico")
        Me.imgSmartDia.Images.SetKeyName(7, "Small Arrow.ico")
        Me.imgSmartDia.Images.SetKeyName(8, "DX01.ico")
        Me.imgSmartDia.Images.SetKeyName(9, "Bullet06.ico")
        Me.imgSmartDia.Images.SetKeyName(10, "template.ico.jpg")
        Me.imgSmartDia.Images.SetKeyName(11, "FLow sheet.ico")
        Me.imgSmartDia.Images.SetKeyName(12, "Lab orders.ico")
        Me.imgSmartDia.Images.SetKeyName(13, "Radiology.ico")
        Me.imgSmartDia.Images.SetKeyName(14, "Refferal.ico")
        Me.imgSmartDia.Images.SetKeyName(15, "ICD10GalleryGreen.png")
        Me.imgSmartDia.Images.SetKeyName(16, "ICD1.ico")
        '
        'trvCPT
        '
        Me.trvCPT.BackColor = System.Drawing.Color.White
        Me.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCPT.ForeColor = System.Drawing.Color.Black
        Me.trvCPT.HideSelection = False
        Me.trvCPT.ImageIndex = 9
        Me.trvCPT.ImageList = Me.imgSmartDia
        Me.trvCPT.ItemHeight = 20
        Me.trvCPT.Location = New System.Drawing.Point(8, 5)
        Me.trvCPT.Name = "trvCPT"
        Me.trvCPT.SelectedImageIndex = 9
        Me.trvCPT.ShowLines = False
        Me.trvCPT.Size = New System.Drawing.Size(0, 0)
        Me.trvCPT.TabIndex = 1
        Me.trvCPT.Visible = False
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtsearchCPT)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 30)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(240, 26)
        Me.pnlSearch.TabIndex = 16
        Me.pnlSearch.Visible = False
        '
        'txtsearchCPT
        '
        Me.txtsearchCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchCPT.ForeColor = System.Drawing.Color.Black
        Me.txtsearchCPT.Location = New System.Drawing.Point(32, 5)
        Me.txtsearchCPT.Name = "txtsearchCPT"
        Me.txtsearchCPT.Size = New System.Drawing.Size(207, 15)
        Me.txtsearchCPT.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(207, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(207, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(239, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlSearchCPT)
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(240, 30)
        Me.Panel3.TabIndex = 17
        Me.Panel3.Visible = False
        '
        'pnlSearchCPT
        '
        Me.pnlSearchCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlSearchCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlSearchCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchCPT.Controls.Add(Me.rbSearch2)
        Me.pnlSearchCPT.Controls.Add(Me.rbSearch1)
        Me.pnlSearchCPT.Controls.Add(Me.Label9)
        Me.pnlSearchCPT.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlSearchCPT.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlSearchCPT.Controls.Add(Me.lbl_RightBrd)
        Me.pnlSearchCPT.Controls.Add(Me.lbl_TopBrd)
        Me.pnlSearchCPT.Controls.Add(Me.Panel4)
        Me.pnlSearchCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearchCPT.Location = New System.Drawing.Point(3, 3)
        Me.pnlSearchCPT.Name = "pnlSearchCPT"
        Me.pnlSearchCPT.Size = New System.Drawing.Size(237, 24)
        Me.pnlSearchCPT.TabIndex = 0
        '
        'rbSearch2
        '
        Me.rbSearch2.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch2.Checked = True
        Me.rbSearch2.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbSearch2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch2.Location = New System.Drawing.Point(134, 1)
        Me.rbSearch2.Name = "rbSearch2"
        Me.rbSearch2.Size = New System.Drawing.Size(102, 22)
        Me.rbSearch2.TabIndex = 0
        Me.rbSearch2.TabStop = True
        Me.rbSearch2.Text = "Description"
        Me.rbSearch2.UseVisualStyleBackColor = False
        '
        'rbSearch1
        '
        Me.rbSearch1.AutoSize = True
        Me.rbSearch1.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch1.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbSearch1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch1.Location = New System.Drawing.Point(17, 1)
        Me.rbSearch1.Name = "rbSearch1"
        Me.rbSearch1.Size = New System.Drawing.Size(79, 22)
        Me.rbSearch1.TabIndex = 4
        Me.rbSearch1.Text = "CPT Code"
        Me.rbSearch1.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(16, 22)
        Me.Label9.TabIndex = 9
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(236, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(237, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.C1Dignosis)
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(108, 81)
        Me.Panel4.TabIndex = 5
        Me.Panel4.Visible = False
        '
        'C1Dignosis
        '
        Me.C1Dignosis.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Dignosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Dignosis.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Dignosis.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Dignosis.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Dignosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Dignosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Dignosis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Dignosis.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Dignosis.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Dignosis.Location = New System.Drawing.Point(0, 0)
        Me.C1Dignosis.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Dignosis.Name = "C1Dignosis"
        Me.C1Dignosis.Rows.Count = 1
        Me.C1Dignosis.Rows.DefaultSize = 19
        Me.C1Dignosis.Rows.Fixed = 0
        Me.C1Dignosis.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Dignosis.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Dignosis.ShowCellLabels = True
        Me.C1Dignosis.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Dignosis.Size = New System.Drawing.Size(108, 81)
        Me.C1Dignosis.StyleInfo = resources.GetString("C1Dignosis.StyleInfo")
        Me.C1Dignosis.TabIndex = 8
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(240, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 562)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.trvCPTAssociation)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(244, 54)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(590, 562)
        Me.Panel5.TabIndex = 1
        '
        'trvCPTAssociation
        '
        Me.trvCPTAssociation.BackColor = System.Drawing.Color.White
        Me.trvCPTAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPTAssociation.CheckBoxes = True
        Me.trvCPTAssociation.ContextMenu = Me.cntTags
        Me.trvCPTAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCPTAssociation.ForeColor = System.Drawing.Color.Black
        Me.trvCPTAssociation.HideSelection = False
        Me.trvCPTAssociation.ImageIndex = 9
        Me.trvCPTAssociation.ImageList = Me.imgSmartDia
        Me.trvCPTAssociation.Indent = 21
        Me.trvCPTAssociation.ItemHeight = 20
        Me.trvCPTAssociation.Location = New System.Drawing.Point(5, 8)
        Me.trvCPTAssociation.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.trvCPTAssociation.Name = "trvCPTAssociation"
        Me.trvCPTAssociation.SelectedImageIndex = 9
        Me.trvCPTAssociation.ShowLines = False
        Me.trvCPTAssociation.Size = New System.Drawing.Size(581, 550)
        Me.trvCPTAssociation.TabIndex = 4
        '
        'cntTags
        '
        Me.cntTags.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Remove Item"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(1, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(4, 550)
        Me.Label12.TabIndex = 41
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(1, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(585, 4)
        Me.Label13.TabIndex = 40
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 558)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(585, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 555)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(586, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 555)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(587, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlToolBar
        '
        Me.pnlToolBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolBar.Controls.Add(Me.tblSmartTreatment)
        Me.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(834, 54)
        Me.pnlToolBar.TabIndex = 3
        Me.pnlToolBar.TabStop = True
        '
        'tblSmartTreatment
        '
        Me.tblSmartTreatment.BackColor = System.Drawing.Color.Transparent
        Me.tblSmartTreatment.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblSmartTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblSmartTreatment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSmartTreatment.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblSmartTreatment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblSmartTreatment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSave, Me.tblClose})
        Me.tblSmartTreatment.Location = New System.Drawing.Point(0, 0)
        Me.tblSmartTreatment.Name = "tblSmartTreatment"
        Me.tblSmartTreatment.Size = New System.Drawing.Size(834, 53)
        Me.tblSmartTreatment.TabIndex = 0
        Me.tblSmartTreatment.Text = "ToolStrip1"
        '
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ImgAssociation
        '
        Me.ImgAssociation.ImageStream = CType(resources.GetObject("ImgAssociation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgAssociation.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgAssociation.Images.SetKeyName(0, "")
        Me.ImgAssociation.Images.SetKeyName(1, "")
        '
        'cntCPTAssociation
        '
        Me.cntCPTAssociation.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteICD9Item, Me.mnuEditICD9Item})
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 0
        Me.mnuDeleteICD9Item.Text = "Remove Item"
        '
        'mnuEditICD9Item
        '
        Me.mnuEditICD9Item.Index = 1
        Me.mnuEditICD9Item.Text = "Edit Item"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(244, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(590, 4)
        Me.Splitter2.TabIndex = 4
        Me.Splitter2.TabStop = False
        Me.Splitter2.Visible = False
        '
        'frmSmartTreatmentNew
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(834, 616)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSmartTreatmentNew"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Smart Treatment"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.pnlSearchCPT.ResumeLayout(False)
        Me.pnlSearchCPT.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.tblSmartTreatment.ResumeLayout(False)
        Me.tblSmartTreatment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "FlexGrid Column variables"
    Private Col_ICD9Code_Description As Integer = 0
    Private Col_ICD9Code As Integer = 1
    Private Col_ICD9Desc As Integer = 2
    Private COl_CPTCode As Integer = 3
    Private Col_CPTDesc As Integer = 4
    Private Col_ModCode As Integer = 5
    Private Col_ModDesc As Integer = 6
    Private Col_Units As Integer = 7
    Private Col_ICD9Count As Integer = 8
    Private Col_CPTCount As Integer = 9
    Private Col_ModCount As Integer = 10
    Private Col_SnomedCode As Integer = 11
    Private Col_SnomedDesc As Integer = 12
    Private Col_ICDRevision As Integer = 13
    Private Col_Count = 14
#End Region

#Region "Class Initializers"
    Private objDiagnosisDBLayer As ClsDiagnosisDBLayer
    Dim objTreatmentDBLayer As ClsTreatmentDBLayer
#End Region

    Dim IsFormLoading As Boolean = False
    Dim IsTreeviewDoubleClick As Boolean = False

    Private Sub frmSmartTreatmentNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.DoubleBuffered = True
            IsFormLoading = True
            'function move below for bug : 6274 by kanchan on 20101130 
            SetGridStyle()
            oclsSmartTreatment = New clsSmartTreatment(_PatientID)
            trvCPTAssociation.AllowDrop = True

            'swaraj 29-03-2010 -- loading FetchSmartTreatment table'
            objclsSmartTreatment = New clsSmartTreatment(_PatientID)
            ' '' ''_dt = New DataTable
            _dt = Nothing
            _dt = objclsSmartTreatment.FetchSmartTreatment()
            'End swaraj'

            Dim rootnode As myTreeNode
            'Dim i As Integer
            Dim dt As DataTable
            ''rootnode = New myTreeNode("CPT", -1)

            ''rootnode.ImageIndex = 5
            ''rootnode.SelectedImageIndex = 5

            ''trvCPT.Nodes.Add(rootnode)
            frmPatientExam.nRefTempID = 0
            dt = oclsSmartTreatment.FillAssociatedCPT(1)
            GloUC_trvCPT.Clear()
            If IsNothing(dt) = False Then
                'With trvCPT
                '    .Nodes.Clear()
                '    For i As Int16 = 0 To dtCPT.Rows.Count - 1
                '        oNode = New TreeNode
                '        With oNode
                '            .Text = Convert.ToString(dtCPT.Rows(i)(2))
                '            .Tag = Convert.ToString(dtCPT.Rows(i)(3))
                '        End With
                '        .Nodes.Add(oNode)
                '        oNode = Nothing
                '    Next
                'End With
                GloUC_trvCPT.DataSource = dt
                GloUC_trvCPT.ValueMember = Convert.ToString(dt.Columns("nCPTID").ColumnName)
                GloUC_trvCPT.DescriptionMember = Convert.ToString(dt.Columns("Column1").ColumnName) ''Code - Description
                GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvCPT.FillTreeView()

            End If
            'Populate ICD9 Data
            'For i = 0 To dt.Rows.Count - 1
            '    Dim mychildnode As myTreeNode
            '    ''mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0) , CType(dt.Rows(i)(2), String))
            '    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
            '    mychildnode.ImageIndex = 9
            '    mychildnode.SelectedImageIndex = 9
            '    trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
            'Next
            'trvCPT.ExpandAll()
            'trvCPT.Select()

            rootnode = New myTreeNode("Treatment", -1)
            rootnode.ImageIndex = 6
            rootnode.SelectedImageIndex = 6
            trvCPTAssociation.Nodes.Add(rootnode)

            'Initialize shared array variable of patient exam
            frmPatientExam.arrTagID = New ArrayList

            '''''''''' Fill Associates of Tags In Context Menu
            IsFormLoad = True
            Call FillTagsAssociates()

            '''''''''' If Smart_Treatment already Exists
            If gblnICD9Driven Then
                Load_Treatment()
            Else
                Load_CPTDrivenDiagnosis()
            End If
            ''''''''''

            ''''''''''''''''''''''
            oclsSmartTreatment.dtDiagnosis = oclsSmartTreatment.ScanDiagnosis(m_ExamID, m_VisitID)

            Dim obj As New ClsTreatmentDBLayer
            oclsSmartTreatment.dtTreatment = obj.FetchCPTforUpdate(m_ExamID)
            obj = Nothing

            oclsSmartTreatment.dtDrugs = oclsSmartTreatment.FetchDrugsFromPrescription(m_VisitID, m_ExamDate)

            ''''
            Dim dtPE As DataTable
            dtPE = oclsSmartTreatment.FetchPatientEducation(m_VisitID)

            If IsNothing(dtPE) = False Then
                If dtPE.Rows.Count > 0 Then
                    Dim r As DataRow
                    For Each r In dtPE.Rows
                        Dim PE As String()
                        Dim j As Int16
                        PE = Split(CType(r(0), String), ",")
                        For j = 0 To PE.Length - 1
                            Dim lst As New myList
                            lst.Description = CType(PE.GetValue(j), String)
                            ''''arrPE.Add(CType(PE.GetValue(j), String)) ''& "-" & CType(r(1), String))
                            arrPE.Add(lst)
                        Next
                    Next
                    ''''''<><><><><><><><>
                    arrPE = oclsSmartTreatment.GetEducationID(arrPE)
                    ''''''<><><><><><><><>
                End If
            End If
            ''''
            ''''''''''''''''''''''

            trvCPTAssociation.SelectedNode = trvCPTAssociation.Nodes(0)
            'function commented & move above for bug : 6274 by kanchan on 20101130 
            'SetGridStyle()
            FillICDCPTMOD()
            '' To refresh the txtDrugs 
            Call RefreshSearch()
            txtsearchCPT.Focus()
            IsFormLoad = False
            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Other, "Smart Treatment Opened", gstrLoginName, gstrClientMachineName, _PatientID)
            ''  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.View, "Smart Treatment Opened", gloAuditTrail.ActivityOutCome.Success)
            ''Added vijay P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.View, "Smart Treatment Opened", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
            objTreatmentDBLayer = New ClsTreatmentDBLayer
            '  FillFlowSheet()
            '  FillOrders()
            ' FillReferrals()


            FillNodes() 'It checks the nodes FillFlowSheet,Lab Orders,Orders,Referrals to which patient visited
            CheckAllParentNodes()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            IsFormLoading = False



        End Try
    End Sub

    Public Sub SetGridStyle()
        Try
            gloC1FlexStyle.Style(C1Dignosis)
            With C1Dignosis
                Dim _TotalWidth As Single = .Width - 5
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = Col_Count

                'for ICD9
                .Cols(Col_ICD9Code_Description).Width = _TotalWidth * 0.83
                .SetData(0, Col_ICD9Code_Description, "ICD9")
                .Cols(Col_ICD9Code_Description).AllowEditing = False
                .Cols(Col_ICD9Code_Description).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Code).Width = _TotalWidth * 0.17
                .SetData(0, Col_ICD9Code, "ICD9CODE")
                .Cols(Col_ICD9Code).AllowEditing = True
                .Cols(Col_ICD9Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Desc).Width = _TotalWidth * 0.17
                .SetData(0, Col_ICD9Desc, "ICD9Description")
                .Cols(Col_ICD9Desc).AllowEditing = True
                .Cols(Col_ICD9Desc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COl_CPTCode).Width = _TotalWidth * 0.17
                .SetData(0, COl_CPTCode, "CPTCODE")
                .Cols(COl_CPTCode).AllowEditing = True
                .Cols(COl_CPTCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTDesc).Width = _TotalWidth * 0.17
                .SetData(0, Col_CPTDesc, "CPTDescription")
                .Cols(Col_CPTDesc).AllowEditing = True
                .Cols(Col_CPTDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCode).Width = _TotalWidth * 0
                .SetData(0, Col_ModCode, "MODCODE")
                .Cols(Col_ModCode).AllowEditing = True
                .Cols(Col_ModCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModDesc).Width = _TotalWidth * 0
                .SetData(0, Col_ModDesc, "MODDescription")
                .Cols(Col_ModDesc).AllowEditing = True
                .Cols(Col_ModDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


                .Cols(Col_Units).Width = _TotalWidth * 0.17
                .SetData(0, Col_Units, "Units")
                .Cols(Col_Units).DataType = GetType(System.Int64)
                .Cols(Col_Units).AllowEditing = True
                .Cols(Col_Units).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Count).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Count, "ICD9 Count")
                .Cols(Col_ICD9Count).DataType = GetType(System.Int64)
                .Cols(Col_ICD9Count).AllowEditing = True
                .Cols(Col_ICD9Count).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTCount).Width = _TotalWidth * 0
                .SetData(0, Col_CPTCount, "CPT Count")
                .Cols(Col_CPTCount).DataType = GetType(System.Int64)
                .Cols(Col_CPTCount).AllowEditing = True
                .Cols(Col_CPTCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCount).Width = _TotalWidth * 0
                .SetData(0, Col_ModCount, "Mod Count")
                .Cols(Col_ModCount).DataType = GetType(System.Int64)
                .Cols(Col_ModCount).AllowEditing = True
                .Cols(Col_ModCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_SnomedCode).Width = _TotalWidth * 0
                .SetData(0, Col_SnomedCode, "SnomedCode")
                .Cols(Col_SnomedCode).AllowEditing = True
                .Cols(Col_SnomedCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_SnomedDesc).Width = _TotalWidth * 0
                .SetData(0, Col_SnomedDesc, "SnomedDesc")
                .Cols(Col_SnomedDesc).AllowEditing = True
                .Cols(Col_SnomedDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICDRevision).Width = _TotalWidth * 0
                .SetData(0, Col_ICDRevision, "ICDRevision")
                .Cols(Col_ICDRevision).AllowEditing = True
                .Cols(Col_ICDRevision).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillICDCPTMOD()

        Try

            Dim _Row As Integer
            'set properties of treeview in flexgrid
            With C1Dignosis
                .Tree.Column = Col_ICD9Code_Description
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
            End With


            Dim dtICD9 As DataTable
            Dim dtCPT As DataTable
            Dim dtMOD As DataTable
            Dim dvICD9 As DataView
            Dim dvCPT As DataView

            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16


            Dim nextICD As Integer

            objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            ' flag = 0 - ICD9   flag = 1 - CPT flag = 2 -MOD
            dtICD9 = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamID, m_VisitID, "", "", "", 5)
            objDiagnosisDBLayer = Nothing
            If Not IsNothing(dtICD9) Then
                dvICD9 = New DataView(dtICD9)
                If Not IsNothing(dtICD9) Then
                    dtICD9 = Nothing
                End If
                dtICD9 = New DataTable()
                dtICD9 = New DataTable

                Dim strICD9(dtICD9.Columns.Count - 1) As String

                For i As Integer = 0 To dtICD9.Columns.Count - 1
                    strICD9.SetValue(dtICD9.Columns(i).ColumnName, i)
                Next
                dtICD9 = dvICD9.ToTable(True, strICD9)
                ''''Pramod 04232009 End

                With dtICD9
                    If IsNothing(dtICD9) = False Then
                        For nICD9 = 0 To .Rows.Count - 1
                            Dim count As Integer = nICD9 + 1
                            If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                C1Dignosis.Rows.Add()
                                _Row = C1Dignosis.Rows.Count - 1
                                'set the properties for newly added row
                                With C1Dignosis.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Selected = False
                                    .Node.Data = dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.icd9
                                End With
                                nextICD = _Row
                                With C1Dignosis
                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                    .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                    .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                    .SetData(_Row, Col_ICDRevision, Convert.ToString(dtICD9.Rows(nICD9)("nICDRevision")))
                                End With
                                Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")


                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                dtCPT = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamID, m_VisitID, strCurrentICD9, "", "", 1)
                                objDiagnosisDBLayer = Nothing
                                If Not IsNothing(dtCPT) Then
                                    dvCPT = New DataView(dtCPT)
                                    If Not IsNothing(dtCPT) Then
                                        dtCPT = Nothing
                                    End If
                                    dtCPT = New DataTable
                                    dtCPT = New DataTable
                                    Dim strCPT(dtCPT.Columns.Count - 1) As String
                                    For i As Integer = 0 To dtCPT.Columns.Count - 1
                                        strCPT.SetValue(dtCPT.Columns(i).ColumnName, i)
                                    Next
                                    dtCPT = dvCPT.ToTable(True, strCPT)
                                    With dtCPT
                                        If IsNothing(dtCPT) = False Then
                                            For nCPT = 0 To .Rows.Count - 1
                                                Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                                If strCurrentCPT.Trim <> "" Then
                                                    C1Dignosis.Rows.Add()
                                                    _Row = C1Dignosis.Rows.Count - 1
                                                    'set the properties for newly added row
                                                    With C1Dignosis.Rows(_Row)
                                                        .AllowEditing = True
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 1
                                                        .Node.Data = dtCPT.Rows(nCPT)("sCPTcode") & "-" & dtCPT.Rows(nCPT)("sCPTDescription")
                                                        .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                                    End With

                                                    With C1Dignosis
                                                        .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                        .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                        .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                                        .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                                        .SetData(_Row, Col_ICDRevision, Convert.ToString(dtICD9.Rows(nICD9)("nICDRevision")))
                                                        .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                        .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                        .SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))
                                                        .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                        .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                    End With


                                                End If

                                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                                dtMOD = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamID, m_VisitID, strCurrentICD9, strCurrentCPT, "", 2)
                                                objDiagnosisDBLayer = Nothing

                                                With dtMOD
                                                    If IsNothing(dtMOD) = False Then
                                                        For nMOD = 0 To .Rows.Count - 1

                                                            Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")

                                                            If strCurrentMod.Trim <> "" Then
                                                                C1Dignosis.Rows.Add()
                                                                _Row = C1Dignosis.Rows.Count - 1
                                                                'set the properties for newly added row
                                                                With C1Dignosis.Rows(_Row)
                                                                    .AllowEditing = False
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 2
                                                                    .Node.Data = dtMOD.Rows(nMOD)("sModCode") & "-" & dtMOD.Rows(nMOD)("sModDescription")
                                                                    .Node.Image = Global.gloEMR.My.Resources.Resources.Modifier
                                                                End With

                                                                With C1Dignosis
                                                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                    .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                    .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                    .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
                                                                    .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
                                                                    .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                                                    .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                                                    .SetData(_Row, Col_ICDRevision, Convert.ToString(dtICD9.Rows(nICD9)("nICDRevision")))
                                                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                                    .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                                    .SetData(_Row, Col_ModCount, nMOD + 1)
                                                                End With
                                                            End If
                                                        Next
                                                    End If
                                                End With '' With dtMOD
                                            Next '' For nCPT = 0 To .Rows.Count - 1
                                        End If
                                    End With '' With dtCPT
                                End If
                            End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                        Next ''For nICD9 = 0 To .Rows.Count - 1
                    End If  '' If IsNothing(dtICD9) = False Then
                End With '' With dtICD9
            End If


            dtICD9 = Nothing
            dtCPT = Nothing
            dtMOD = Nothing

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''' Fill Smart_Treatment already Exists
    Private Sub Load_Treatment()
        Dim dt As DataTable
        '''' >>>>>>>>> To Insert CPT <<<<<<<<<  '''''

        '''' get Treatment CPT(s) for selected Exam
        dt = oclsSmartTreatment.ScanTreatment(m_ExamID)

        oclsSmartTreatment.dtDiagnosis = dt

        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                '' if there Exists Treatment for this Exam Add it to Arraylist
                Dim arrCPT As New ArrayList
                Dim r As DataRow
                For Each r In dt.Rows
                    arrCPT.Add(CType(r(0), String))
                Next
                Dim _node() As String
                Dim node As gloUserControlLibrary.myTreeNode
                Dim i As Int16
                For i = 0 To arrCPT.Count - 1
                    For Each node In GloUC_trvCPT.Nodes
                        _node = node.Text.Trim.Split("-")
                        If _node.Length > 1 Then
                            If _node.GetValue(0) = CType(arrCPT(i), String) Then
                                ''code that works on tree view controls doublclick event   
                                ''AddNode
                                Dim mynode As New myTreeNode
                                mynode.Key = node.ID
                                mynode.Text = node.Text
                                If Not IsNothing(mynode) Then
                                    AddNode(mynode)
                                End If

                                ' trvCPTAssociation.SelectedNode.Checked = True

                                Call Load_Dignosis(trvCPTAssociation.SelectedNode)
                                Call Load_Drugs(trvCPTAssociation.SelectedNode)
                                Call Load_PatientEducation(trvCPTAssociation.SelectedNode)
                            End If
                        End If

                    Next
                Next
                ''''
            End If
        End If

    End Sub

    '' to Load Dignosis for the Aasociated CPT & Also in Current Exam
    Private Sub Load_Dignosis(ByVal CPTNode As myTreeNode)
        Dim obj As New ClsDiagnosisDBLayer
        Dim dtICD9 As DataTable

        dtICD9 = obj.FetchICD9forUpdate(m_ExamID, m_VisitID)

        oclsSmartTreatment.dtDiagnosis = dtICD9

        obj = Nothing

        If IsNothing(dtICD9) = False Then
            If dtICD9.Rows.Count > 0 Then
                '' if there Exists Diagnosis (ICD9)
                Dim arrICD9 As New ArrayList
                Dim r As DataRow
                For Each r In dtICD9.Rows
                    arrICD9.Add(CType(r(0), String) & "-" & CType(r(1), String))
                Next

                ' Dim ICD9Node As TreeNode
                'Dim Count As Int16
                'For Count = 0 To trvCPTAssociation.Nodes(0).GetNodeCount(False) - 1
                ' If ICD9Node.Text = trvCPTAssociation.Nodes(0).Nodes(Count).Text Then
                Dim Count1 As Int16
                For Count1 = 0 To CPTNode.GetNodeCount(False) - 1
                    If CPTNode.Nodes(Count1).Text = "ICD" And CType(CPTNode.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim ICD9Node As TreeNode
                        ' CPTNode = trvCPTAssociation.Nodes(0).Nodes(Count).Nodes(Count1)
                        For Each ICD9Node In CPTNode.Nodes(Count1).Nodes
                            Dim i As Int16
                            For i = 0 To arrICD9.Count - 1
                                If ICD9Node.Text = CType(arrICD9(i), String) Then
                                    '' If ICD9COde-Discription Mathches with TreeNode then 
                                    '' then add that ICD9 to associated treeview
                                    ICD9Node.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Next
                '     End If
                '  Next
                ''''
            End If
        End If

    End Sub

    Private Sub Load_Drugs(ByVal CPTNode As myTreeNode)
        Dim dtDrugs As DataTable
        '''' Retrive All Drugs Priscribed for the Visit of the Patient  from Priscription Table
        dtDrugs = oclsSmartTreatment.FetchDrugsFromPrescription(m_VisitID, m_ExamDate)

        oclsSmartTreatment.dtDrugs = dtDrugs

        If IsNothing(dtDrugs) = False Then
            If dtDrugs.Rows.Count > 0 Then
                '' if there Exists Treatment (CPT)
                Dim arrDrugs As New ArrayList
                Dim r As DataRow
                For Each r In dtDrugs.Rows
                    arrDrugs.Add(CType(r(0), String)) ''& "-" & CType(r(1), String))
                Next

                ' Dim ICD9Node As TreeNode
                'Dim Count As Int16
                'For Count = 0 To trvCPTAssociation.Nodes(0).GetNodeCount(False) - 1
                ' If ICD9Node.Text = trvCPTAssociation.Nodes(0).Nodes(Count).Text Then
                Dim Count1 As Int16
                For Count1 = 0 To CPTNode.GetNodeCount(False) - 1
                    If CPTNode.Nodes(Count1).Text = "Drugs" And CType(CPTNode.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim DrugsNode As TreeNode
                        ' CPTNode = trvCPTAssociation.Nodes(0).Nodes(Count).Nodes(Count1)
                        For Each DrugsNode In CPTNode.Nodes(Count1).Nodes
                            Dim i As Int16
                            For i = 0 To arrDrugs.Count - 1
                                If DrugsNode.Text = CType(arrDrugs(i), String) Then
                                    '' If ICD9COde-Discription Mathches with TreeNode then 
                                    '' then add that ICD9 to associated treeview
                                    DrugsNode.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Next
                '     End If
                '  Next
                ''''
            End If
        End If

    End Sub

    Private Sub Load_PatientEducation(ByVal CPTNode As myTreeNode)
        Dim dtPE As DataTable
        '' to Retrive Patient Education 
        dtPE = oclsSmartTreatment.FetchPatientEducation(m_VisitID)

        If IsNothing(dtPE) = False Then
            If dtPE.Rows.Count > 0 Then
                '' dtPE Contains PE Names Seaperated by "," 
                '' We have to Seaperate each name & keep it in ArrayList
                Dim r As DataRow
                For Each r In dtPE.Rows
                    '' to Seaperated PE Names
                    Dim PE As String()
                    Dim j As Int16
                    PE = Split(CType(r(0), String), ",")
                    For j = 0 To PE.Length - 1
                        Dim lst As New myList
                        lst.Description = CType(PE.GetValue(j), String)
                        ''''arrPE.Add(CType(PE.GetValue(j), String)) ''& "-" & CType(r(1), String))
                        arrPE.Add(lst) ''& "-" & CType(r(1), String))
                    Next
                Next

                ''''''<><><><><><><><>
                '' Get TemplateIDs of Template type PE & of name in arrPE
                arrPE = oclsSmartTreatment.GetEducationID(arrPE)
                ''''''<><><><><><><><>

                'Dim Count As Int16

                Dim Count1 As Int16
                For Count1 = 0 To CPTNode.GetNodeCount(False) - 1
                    If CPTNode.Nodes(Count1).Text = "Patient Education" And CType(CPTNode.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim PENode As TreeNode
                        For Each PENode In CPTNode.Nodes(Count1).Nodes
                            Dim i As Int16
                            For i = 0 To arrPE.Count - 1
                                If PENode.Text = Trim(CType(arrPE(i), myList).Description) Then
                                    '' If PE Name Mathches with TreeNode then 
                                    '' then Check that PE in associated treeview
                                    PENode.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Next
            End If
        End If

    End Sub

    '' to Fill the Associates of Tags in ContaxMenu
    Private Sub FillTagsAssociates()
        Try
            Dim dtTags As DataTable
            dtTags = oclsSmartTreatment.GetAllCategory("Tags")
            cntTags.MenuItems.Clear()

            Dim oChildItem As MenuItem
            Dim i As Integer
            For i = 0 To dtTags.Rows.Count - 1
                oChildItem = New MenuItem
                oChildItem.Text = dtTags.Rows(i)(1).ToString
                With cntTags.MenuItems
                    .Add(oChildItem)
                End With
                AddHandler oChildItem.Click, AddressOf cntTags_Click
                oChildItem = Nothing
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '''' Event Handler for cntTags.Click
    Public Sub cntTags_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearchCPT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchCPT.TextChanged
        Try


            If Trim(txtsearchCPT.Text) <> "" Then
                If trvCPT.Nodes.Item(0).GetNodeCount(False) > 0 Then
                    Dim mychildnode As myTreeNode
                    'child node collection
                    For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
                        'search against Description
                        Dim strcode As String

                        strcode = Splittext(mychildnode.Text)
                        If rbSearch2.Checked = True Then
                            '''' Search against CPT Description
                            Dim strvCPT As String
                            strvCPT = Mid(mychildnode.Text, Len(strcode) + 2, Len(Trim(mychildnode.Text)))
                            If UCase(Mid(strvCPT, 1, Len(Trim(txtsearchCPT.Text)))) = UCase(Trim(txtsearchCPT.Text)) Then
                                'select matching node
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                '*************
                                trvCPT.SelectedNode = mychildnode
                                txtsearchCPT.Focus()
                                Exit Sub
                            End If
                        Else
                            '''' Search against CPT Code
                            strcode = Mid(strcode, 1, Len(Trim(txtsearchCPT.Text)))
                            If UCase(strcode) = UCase(Trim(txtsearchCPT.Text)) Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trvCPT.SelectedNode = mychildnode
                                txtsearchCPT.Focus()
                                Exit Sub
                            End If
                        End If
                    Next

                    ' '' 20070922 MAhesh InString Search 
                    For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
                        'search against Description
                        If rbSearch2.Checked = True Then
                            If InStr(UCase(mychildnode.Tag), UCase(Trim(txtsearchCPT.Text)), CompareMethod.Text) > 0 Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trvCPT.SelectedNode = mychildnode
                                txtsearchCPT.Focus()
                                Exit Sub
                            End If
                        Else
                            'search against ICD9 Code
                            Dim str As String
                            str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
                            ' str = Mid(str, 1, Len(Trim(txtsearchDrugs.Text)))
                            If InStr(UCase(str), UCase(Trim(txtsearchCPT.Text)), CompareMethod.Text) > 0 Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trvCPT.SelectedNode = mychildnode
                                txtsearchCPT.Focus()
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, "-")
            Return arrstring(0)
        Else
            Return ""
        End If
    End Function

    Private Sub trvCPTAssociation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCPTAssociation.AfterCheck
        If bChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked)
        End If
        If bParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked)
        End If
        'If IsFormLoading = False Then
        If IsFormLoading = False And IsTreeviewDoubleClick = False Then
            Dim nMaxICD9Count As Integer = 0
            Dim ofNode As C1.Win.C1FlexGrid.Node
            Dim NewRow As Integer = 0
            Dim arrstrConctCPT() As String
            Dim arrstrConctICD9() As String
            If Not IsNothing(e.Node.Parent) Then
                If e.Node.Parent.Text = "ICD" Then
                    If e.Node.Checked = True Then

                        '' FOR ICD9 DRIVEN ONLY ''
                        If gblnICD9Driven = True Then
                            With C1Dignosis
                                For i As Integer = 0 To .Rows.Count - 1
                                    If .GetData(i, Col_ICD9Code_Description) = e.Node.Text Then
                                        ofNode = .Rows(i).Node
                                        If ofNode.Children > 0 Then
                                            For j As Integer = ofNode.Row.Index To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index

                                                If .GetData(j, Col_ICD9Code_Description) = e.Node.Parent.Parent.Text Then
                                                    Exit Sub
                                                End If
                                            Next
                                            NewRow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1
                                            .Rows.Insert(NewRow)

                                            With .Rows(NewRow)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                                '.Node.Data = trvCPT.SelectedNode.Text
                                                .Node.Data = e.Node.Text
                                            End With

                                            arrstrConctCPT = Split(e.Node.Parent.Parent.Text, "-", 2)
                                            arrstrConctICD9 = Split(e.Node.Text, "-", 2)


                                            .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                            .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                            .SetData(NewRow, Col_ICDRevision, CType(e.Node, myTreeNode).nICDRevision)

                                            .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                            .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                            .SetData(NewRow, Col_ICD9Count, .GetData(i, Col_ICD9Count))
                                            .SetData(NewRow, Col_CPTCount, .GetData(i, Col_CPTCount) + 1)
                                            Exit Sub
                                        End If
                                    End If
                                Next

                                If .Rows.Count - 1 > 0 Then
                                    nMaxICD9Count = .GetData(.Rows.Count - 1, Col_ICD9Count)
                                Else
                                    nMaxICD9Count = 0
                                End If
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                With .Rows(NewRow)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.icd9
                                    .Node.Data = e.Node.Text
                                End With

                                arrstrConctICD9 = Split(e.Node.Text, "-", 2)
                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                .SetData(NewRow, Col_ICDRevision, CType(e.Node, myTreeNode).nICDRevision)
                                .SetData(NewRow, Col_ICD9Count, nMaxICD9Count + 1)

                                nMaxICD9Count = .GetData(.Rows.Count - 1, Col_ICD9Count)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                With .Rows(NewRow)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 1
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                    .Node.Data = e.Node.Parent.Parent.Text
                                End With

                                arrstrConctCPT = Split(e.Node.Parent.Parent.Text, "-", 2)

                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                .SetData(NewRow, Col_ICDRevision, CType(e.Node, myTreeNode).nICDRevision)
                                .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                ''dhruv 20091221 to set the value as 1 
                                .SetData(NewRow, Col_Units, 1)

                                .SetData(NewRow, Col_ICD9Count, nMaxICD9Count)
                                .SetData(NewRow, Col_CPTCount, 1)
                                Exit Sub
                            End With
                        End If


                        '' FOR CPT DRIVEN DIAGNOSIS ''
                        If gblnICD9Driven = False Then
                            If e.Node.Parent.Text = "ICD" Then
                                If e.Node.Parent.Parent.Checked Then
                                    Dim _CPTCode, _CPTDescription, _CPTText, _ICD9Code, _ICD9Description, _ICD9Text As String
                                    _CPTText = e.Node.Parent.Parent.Text
                                    _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                                    _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)

                                    _ICD9Text = CType(e.Node, myTreeNode).Text
                                    _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-"))
                                    _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1)

                                    RemoveCPTICD9(_CPTCode, _CPTDescription, _ICD9Code, _ICD9Description)
                                End If
                            ElseIf e.Node.Parent.Text = "Treatment" Then
                                Dim _CPTCode, _CPTDescription, _CPTText, _ICD9Code, _ICD9Description, _ICD9Text As String
                                _CPTText = e.Node.Text
                                _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                                _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)

                                For Each oICD9Node As myTreeNode In e.Node.Nodes
                                    If oICD9Node.Text = "ICD" Then
                                        For Each oICD9 As myTreeNode In oICD9Node.Nodes
                                            If oICD9.Checked Then
                                                _ICD9Text = CType(oICD9, myTreeNode).Text
                                                _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-"))
                                                _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1)

                                                RemoveCPTICD9(_CPTCode, _CPTDescription, _ICD9Code, _ICD9Description)
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        End If


                    End If
                End If
            End If
        End If
    End Sub

    Private Sub trvCPTAssociation_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvCPTAssociation.DragOver
        Try
            If IsNothing(trvCPTAssociation.SelectedNode) = True Then
                Exit Sub
            End If

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvCPT_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvCPT.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trvCPT_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvCPT.DragDrop, trvCPTAssociation.DragDrop
        Try
            If IsNothing(trvCPTAssociation.SelectedNode) = True Then
                Exit Sub
            End If

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

            Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

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
            'If Not IsNothing(dropNode) Then
            '    AddNode(dropNode)
            'End If
            'commented from 14/09/2005
            If dropNode.Parent Is trvCPT.Nodes.Item(0) Then
                Dim str As String
                str = dropNode.Key
                Dim mytragetnode As myTreeNode
                For Each mytragetnode In trvCPTAssociation.Nodes.Item(0).Nodes
                    If mytragetnode.Key = str Then
                        Exit Sub
                    End If
                Next
                'dropNode.Remove()
                'If PopulateMedication(dropNode.Key) Then
                Dim associatenode As myTreeNode

                associatenode = dropNode.Clone
                associatenode.Key = dropNode.Key
                associatenode.Text = dropNode.Text
                associatenode.NodeName = dropNode.NodeName
                associatenode.ImageIndex = 5
                associatenode.SelectedImageIndex = 5

                associatenode.Checked = True

                trvCPTAssociation.Nodes.Item(0).Nodes.Add(associatenode)

                Dim mychild As myTreeNode

                mychild = New myTreeNode("CPT", -1)
                mychild.ImageIndex = 0
                mychild.SelectedImageIndex = 0
                associatenode.Nodes.Add(mychild)

                mychild = New myTreeNode("Drugs", -1)
                mychild.ImageIndex = 1
                mychild.SelectedImageIndex = 1
                associatenode.Nodes.Add(mychild)

                mychild = New myTreeNode("Patient Education", -1)
                mychild.ImageIndex = 2
                mychild.SelectedImageIndex = 2
                associatenode.Nodes.Add(mychild)

                mychild = New myTreeNode("Tags", -1)
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
                associatenode.Nodes.Add(mychild)


                mychild = New myTreeNode
                mychild.Text = "Flowsheet"
                mychild.Key = -1
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
                associatenode.Nodes.Add(mychild)


                mychild = New myTreeNode
                mychild.Text = "Orders and Results"
                mychild.Key = -1
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
                associatenode.Nodes.Add(mychild)

                mychild = New myTreeNode
                mychild.Text = "Order Templates"
                mychild.Key = -1
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
                associatenode.Nodes.Add(mychild)

                mychild = New myTreeNode
                mychild.Text = "Referral Letter"
                mychild.Key = -1
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
                associatenode.Nodes.Add(mychild)



                'associatenode.Nodes.Add(New myTreeNode("CPT", -1))
                'associatenode.Nodes.Add(New myTreeNode("Drugs", -1))
                'associatenode.Nodes.Add(New myTreeNode("Patient Education", -1))
                'associatenode.Nodes.Add(New myTreeNode("Tags", -1))


                Dim dt As DataTable
                dt = oclsSmartTreatment.FetchCPTforUpdate(dropNode.Key)
                Dim i As Integer
                Dim bl As Boolean = False
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(12).ToString() = "True" Then
                        bl = True
                    Else
                        bl = False
                    End If
                    If dt.Rows(i).Item(2) = "i" Then
                        ''                                   myTreeNode(    StrName             Key
                        Dim icnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        icnode.Checked = bl
                        associatenode.Nodes.Item(0).Nodes.Add(icnode)
                        '  associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Call Load_Dignosis(associatenode)
                    ElseIf dt.Rows(i).Item(2) = "d" Then
                        Dim drnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        drnode.Checked = bl
                        associatenode.Nodes.Item(1).Nodes.Add(drnode)
                        '  associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                    ElseIf dt.Rows(i).Item(2) = "p" Then
                        Dim penode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        penode.Checked = bl
                        associatenode.Nodes.Item(2).Nodes.Add(penode)
                        ' associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Call Load_PatientEducation(associatenode)
                    ElseIf dt.Rows(i).Item(2) = "t" Then
                        Dim tagnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        tagnode.Checked = bl
                        associatenode.Nodes.Item(3).Nodes.Add(tagnode)
                        ' associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))


                    ElseIf dt.Rows(i).Item(2) = "f" Then
                        Dim flownode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        flownode.Checked = bl
                        flownode.Tag = dt.Rows(i).Item(0)
                        associatenode.Nodes.Item(4).Nodes.Add(flownode)
                        ' associatenode.Nodes.Item(4).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "l" Then
                        Dim labnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        labnode.Checked = bl
                        labnode.Tag = dt.Rows(i).Item(0)
                        associatenode.Nodes.Item(5).Nodes.Add(labnode)
                        ' associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                    ElseIf dt.Rows(i).Item(2) = "o" Then
                        Dim ordnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        ordnode.Checked = bl
                        ordnode.Tag = dt.Rows(i).Item(0)
                        associatenode.Nodes.Item(6).Nodes.Add(ordnode)
                        'associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "r" Then
                        Dim reffnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        reffnode.Checked = bl
                        reffnode.Tag = dt.Rows(i).Item(0)
                        associatenode.Nodes.Item(7).Nodes.Add(reffnode)

                    End If
                Next






                'Else
                'RemoveControl() 'treeindex = targetNode.GetNodeCount(False) - 1
                'End If
                'treeindex = -1
                'End If

                'Ensure the newley created node is visible to the user and select it
                'dropNode.EnsureVisible()
                'dropNode.Expand()
                'trvCPTAssociation.ExpandAll()
                ''trvCPTAssociation.Select()
                'selectedTreeview.SelectedNode = dropNode

                trvCPTAssociation.ExpandAll()
                trvCPTAssociation.Select()

                'treeindex = -1
                'End If

                'Ensure the newly created node is visible to the user and select it
                associatenode.EnsureVisible()
                trvCPTAssociation.SelectedNode = associatenode

                '' To refresh the txtDrugs 
                Call RefreshSearch()

            End If
            'commented from 14/09/2005
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RefreshICD9()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshICD9()
        txtsearchCPT.Text = ""
        trvCPTAssociation.Nodes.Item(0).Nodes.Clear()
        trvCPT.Nodes.Item(0).Nodes.Clear()
        Dim dt As DataTable
        dt = oclsSmartTreatment.FillAssociatedCPT()
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            Dim mychildnode As myTreeNode
            mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
            mychildnode.ImageIndex = 9
            mychildnode.SelectedImageIndex = 9
            trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
        Next
        trvCPT.ExpandAll()
        trvCPT.Select()
        trvCPTAssociation.Nodes(0).Nodes.Clear()
        '' To refresh the txtDrugs 
        Call RefreshSearch()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SaveAssociation()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub SaveAssociation()
    '    'Get node count of child nodes in trvCPTAssociates
    '    'If trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) > 0 Then
    '    Dim arrCPTOnly As New ArrayList
    '    Dim arrDruglist As New ArrayList
    '    Dim arrExamICD9CPT As New ArrayList
    '    Dim arrlist As New ArrayList

    '    Dim CPTNode As New myTreeNode
    '    Dim i As Integer

    '    For i = 0 To trvCPTAssociation.Nodes(0).GetNodeCount(False) - 1
    '        Dim bIsOnlyTreatment As Boolean = False
    '        Dim bIsCPTChecked As Boolean = False
    '        'Get the CPTNode associated sequentially
    '        CPTNode = trvCPTAssociation.Nodes(0).Nodes(i)

    '        If CPTNode.Checked = True Then
    '            bIsOnlyTreatment = True
    '            bIsCPTChecked = True
    '        Else
    '            bIsCPTChecked = False
    '        End If

    '        If CPTNode.GetNodeCount(True) > 0 Then
    '            Dim k As Integer
    '            'Dim arrlist As New ArrayList
    '            For k = 0 To 3
    '                Dim AssociateNode As New myTreeNode
    '                AssociateNode = CPTNode.Nodes(k)
    '                Dim j As Integer
    '                For j = 0 To AssociateNode.GetNodeCount(False) - 1
    '                    If AssociateNode.Nodes(j).Checked = True Then
    '                        Dim lst As New myList
    '                        Dim lstExam As New myList
    '                        If AssociateNode.Text = "ICD9" Then
    '                            'arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 1))   '' For CPT
    '                            lst.ID = 1  '' For ICD9
    '                            lst.Description = CType(AssociateNode.Nodes.Item(j), myTreeNode).Name '' Code-Description
    '                            lst.Index = CType(AssociateNode.Nodes.Item(j), myTreeNode).Key '' ICD9ID
    '                            lst.HistoryCategory = CPTNode.NodeName  '' Associated with CPT

    '                            ''''''''''''''''''''
    '                            Dim strICD9 As String()
    '                            strICD9 = Split(lst.Description, "-", 2)
    '                            '' To Check ICD9 is Exist in Diagnosis
    '                            If oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtDiagnosis, strICD9(0), strICD9(1)) = -1 Then
    '                                '' IF not Exist then add ICD9 in the Table -dtDiagnosis
    '                                Dim r As DataRow
    '                                r = oclsSmartTreatment.dtDiagnosis.NewRow
    '                                r.Item(0) = strICD9(0)
    '                                r.Item(1) = strICD9(1)
    '                                oclsSmartTreatment.dtDiagnosis.Rows.Add(r)
    '                            End If
    '                            '''''''''''''''''              
    '                            '''''''''''''''''
    '                            Dim strCPT As String()
    '                            strCPT = Split(CPTNode.Text, "-", 2)
    '                            '' To Check CPT is Exists in Treatment
    '                            If oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strCPT(0), strCPT(1)) = -1 Then
    '                                '' If Not Exist then Add CPT in Table - dtTreatment 
    '                                Dim r As DataRow
    '                                r = oclsSmartTreatment.dtTreatment.NewRow
    '                                r.Item(0) = strCPT(0)
    '                                r.Item(1) = strCPT(1)
    '                                oclsSmartTreatment.dtTreatment.Rows.Add(r)
    '                            End If

    '                            lstExam.Code = strICD9.GetValue(0)
    '                            lstExam.Description = strICD9.GetValue(1)
    '                            lstExam.HistoryCategory = strCPT.GetValue(0)
    '                            lstExam.HistoryItem = strCPT.GetValue(1)
    '                            ''''''''''''''''''''                   

    '                        ElseIf AssociateNode.Text = "Drugs" Then
    '                            Dim DrudID As Long = CType(AssociateNode.Nodes.Item(j), myTreeNode).Key
    '                            'arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 2))  '' For Drugs
    '                            lst.ID = 2 '' For Drugs
    '                            lst.Description = CType(AssociateNode.Nodes.Item(j), myTreeNode).Name
    '                            lst.Index = CType(AssociateNode.Nodes.Item(j), myTreeNode).Key
    '                            lst.HistoryCategory = CPTNode.NodeName

    '                            '''''''''''''''''
    '                            Dim strCPT As String()
    '                            strCPT = Split(CPTNode.Text, "-", 2)
    '                            '' To Check CPT is Exists in Treatment
    '                            If oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strCPT(0), strCPT(1)) = -1 Then
    '                                '' If Not Exist then Add CPT in Table - dtTreatment 
    '                                Dim r As DataRow
    '                                r = oclsSmartTreatment.dtTreatment.NewRow
    '                                r.Item(0) = strCPT(0)
    '                                r.Item(1) = strCPT(1)
    '                                oclsSmartTreatment.dtTreatment.Rows.Add(r)
    '                            End If
    '                            ''''''''''''''''''''   

    '                            arrDruglist.Add(DrudID)
    '                            'arrDruglist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 2))

    '                        ElseIf AssociateNode.Text = "Patient Education" Then
    '                            'arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 3))   '' Patient Education
    '                            lst.ID = 3 '' For Patient Education
    '                            lst.Description = CType(AssociateNode.Nodes.Item(j), myTreeNode).Name
    '                            lst.Index = CType(AssociateNode.Nodes.Item(j), myTreeNode).Key
    '                            lst.HistoryCategory = CPTNode.NodeName

    '                            Dim l As Integer
    '                            Dim bIsExists As Boolean = False
    '                            For l = 0 To arrPE.Count - 1
    '                                If lst.Description = CType(arrPE(l), myList).Description Then
    '                                    bIsExists = True
    '                                End If
    '                            Next
    '                            If bIsExists = False Then
    '                                arrPE.Add(lst)
    '                            End If

    '                            ''''''''''''''''''''
    '                            Dim strvCPT As String()
    '                            strvCPT = Split(CPTNode.Text, "-", 2)
    '                            'Dim RowIndex As Integer
    '                            If oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strvCPT(0), strvCPT(1)) = -1 Then
    '                                Dim r As DataRow
    '                                r = oclsSmartTreatment.dtTreatment.NewRow
    '                                r.Item(0) = strvCPT(0)
    '                                r.Item(1) = strvCPT(1)
    '                                oclsSmartTreatment.dtTreatment.Rows.Add(r)
    '                            End If
    '                            '''''''''''''''''

    '                        ElseIf AssociateNode.Text = "Tags" Then
    '                            'arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 4))   '' For Tags
    '                            lst.ID = 4 '' For Tags
    '                            lst.Description = CType(AssociateNode.Nodes.Item(j), myTreeNode).Name  '' TemplateName
    '                            lst.Index = CType(AssociateNode.Nodes.Item(j), myTreeNode).Key   '' TemplateID
    '                            If IsNothing(CType(AssociateNode.Nodes.Item(j), myTreeNode).TemplateResult) = True Then
    '                                lst.HistoryItem = ""
    '                            Else
    '                                lst.HistoryItem = CType(AssociateNode.Nodes.Item(j), myTreeNode).TemplateResult.ToString '' Asscociataed Tag
    '                            End If
    '                            lst.HistoryCategory = CPTNode.NodeName  '''' 

    '                            '''''''''''''''''
    '                            Dim strCPT As String()
    '                            strCPT = Split(CPTNode.Text, "-", 2)
    '                            '' To Check CPT is Exists in Treatment
    '                            If oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strCPT(0), strCPT(1)) = -1 Then
    '                                '' If Not Exist then Add CPT in Table - dtTreatment 
    '                                Dim r As DataRow
    '                                r = oclsSmartTreatment.dtTreatment.NewRow
    '                                r.Item(0) = strCPT(0)
    '                                r.Item(1) = strCPT(1)
    '                                oclsSmartTreatment.dtTreatment.Rows.Add(r)
    '                            End If
    '                            ''''''''''''''''''''   

    '                            frmPatientExam.arrTagID.Add(lst) ''''CType(AssociateNode.Nodes.Item(j), myTreeNode).Key)
    '                        End If
    '                        arrlist.Add(lst)
    '                        arrExamICD9CPT.Add(lstExam)

    '                        bIsOnlyTreatment = False
    '                        bIsCPTChecked = True
    '                    Else
    '                        '''''if Not Checked then Remove From The DataTable
    '                        If AssociateNode.Text = "ICD9" Then
    '                            Dim strICD9 As String() '' String Array
    '                            strICD9 = Split(CType(AssociateNode.Nodes(j), myTreeNode).Name, "-", 2)

    '                            Dim RowIndex As Integer = -1
    '                            RowIndex = oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtDiagnosis, strICD9(0), strICD9(1))
    '                            If RowIndex <> -1 Then
    '                                ''''if ICD9 (Diagnosis) Exists in DataTable then Remove
    '                                oclsSmartTreatment.dtDiagnosis.Rows.RemoveAt(RowIndex)
    '                            End If
    '                        ElseIf AssociateNode.Text = "Drugs" Then

    '                        ElseIf AssociateNode.Text = "Patient Education" Then

    '                            Dim l As Integer
    '                            Dim bIsExists As Boolean = False
    '                            'For l = 0 To arrPE.Count - 1
    '                            For l = arrPE.Count - 1 To 0 Step -1
    '                                If CType(AssociateNode.Nodes(j), myTreeNode).Name = CType(arrPE(l), myList).Description Then
    '                                    arrPE.RemoveAt(l)
    '                                End If
    '                            Next
    '                        End If
    '                    End If
    '                Next
    '                Dim oclsDiagnosis As ClsDiagnosisDBLayer
    '                oclsDiagnosis = New ClsDiagnosisDBLayer
    '                'save data in ExamICDCPT Table
    '                oclsDiagnosis.SaveSmartDiagTreatmentAssociation(m_ExamID, _PatientID, m_VisitID, arrExamICD9CPT)
    '                oclsDiagnosis = Nothing
    '            Next

    '            'oclsSmartTreatment.AddData(m_ExamID, m_VisitID, arrlist, ICD9Node.NodeName, arrDruglist)
    '            ' Else
    '            ''if  for any ICD9 There is no Items Associated with it Then only That ICD9 is Saved as diagnosis
    '            ' oclsSmartTreatment.AddDiagnosis(m_ExamID, m_VisitID, ICD9Node.NodeName)
    '        End If


    '        If bIsOnlyTreatment = True Then
    '            '''''''''''''''''
    '            Dim strCPT As String()
    '            strCPT = Split(CPTNode.Text, "-", 2)
    '            '' To Check CPT is Exists in Treatment
    '            If oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strCPT(0), strCPT(1)) = -1 Then
    '                '' If Not Exist then Add CPT in Table - dtTreatment 
    '                Dim r As DataRow
    '                r = oclsSmartTreatment.dtTreatment.NewRow
    '                r.Item(0) = strCPT(0)
    '                r.Item(1) = strCPT(1)
    '                oclsSmartTreatment.dtTreatment.Rows.Add(r)
    '            End If
    '            ''''''''''''''''''''   
    '        End If

    '        If bIsCPTChecked = False Then
    '            ''''''''''<<<<<<<>>>>>>>>
    '            Dim strCPT As String()
    '            strCPT = Split(CPTNode.Text, "-", 2)

    '            Dim RowIndex As Integer = -1
    '            RowIndex = oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strCPT(0), strCPT(1))
    '            If RowIndex <> -1 Then
    '                oclsSmartTreatment.dtTreatment.Rows.RemoveAt(RowIndex)
    '            End If
    '            ''''''''''<<<<<<<>>>>>>>>
    '        End If
    '    Next

    '    'If arrlist.Count > 0 Then
    '    'oclsSmartTreatment.AddData(m_ExamID, m_VisitID, arrlist, ICD9Node.NodeName, arrDruglist)
    '    Dim arrDiagnosislist As New ArrayList
    '    For i = 0 To oclsSmartTreatment.dtDiagnosis.Rows.Count - 1
    '        arrDiagnosislist.Add(New mytable(oclsSmartTreatment.dtDiagnosis.Rows(i)(1).ToString, oclsSmartTreatment.dtDiagnosis.Rows(i)(0).ToString))
    '    Next

    '    oclsSmartTreatment.AddData(m_ExamID, m_VisitID, arrDruglist, arrPE)
    '    ' End If

    '    'If arrICD9Only.Count > 0 Then
    '    '    oclsSmartTreatment.AddDiagnosis(m_ExamID, m_VisitID, arrICD9Only)
    '    'End If

    '    RefreshICD9()
    '    frmPatientExam.Arrlist = arrDiagnosislist
    '    frmPatientExam.blnChangesMade = True
    '    'End If
    'End Sub

    Private Sub SaveAssociation()
        Dim oDrug As gloEMRActors.Drug
        Dim oDrugs As New gloEMRActors.Drugs

        Dim ReffCnt As Integer = 0
        Dim arrCPTOnly As New ArrayList 'if Only ICD9 is checked
        Dim arrDruglist As New ArrayList 'for druglist
        Dim arrExamICD9CPT As New ArrayList 'For cpt with Associated ICD9
        Dim arrlist As New ArrayList
        Dim arrexam As New ArrayList 'arraylist which has ICD9send to exam

        Dim CPTNode As myTreeNode
        Dim i As Integer
        Dim arrICD9CPT As New ArrayList
        Dim strLabTaskDescription As String = ""
        Dim strTreatment As String = ""

        Dim bIsICD9Checked As Boolean = False
        Dim _ISICD9CPT As Boolean = False
        Dim _IsRootNode As Boolean = False
        Dim lstonlyCPT As New myList
        Dim oclsSmartDiagnosis As New clsSmartDiagnosis
        arrexam.Clear()
        arrCPTOnly.Clear()
        arrPE.Clear()
        arrTemplate.Clear()
        frmPatientExam.nRefTempID = 0
        For i = 0 To trvCPTAssociation.Nodes(0).GetNodeCount(False) - 1
            Dim bIsOnlyDiagnosis As Boolean = False
            _IsRootNode = False
            'Get the ICD9Node associated sequentially
            CPTNode = trvCPTAssociation.Nodes(0).Nodes(i) 'First Node i.e CPT
            lstonlyCPT = New myList
            'Add only ICD9 to arraylist 

            If CPTNode.GetNodeCount(True) > 0 Then
                For k As Integer = 0 To 7
                    Dim AssociateNode As myTreeNode
                    AssociateNode = CPTNode.Nodes(k)
                    For j As Integer = 0 To AssociateNode.GetNodeCount(False) - 1
                        Dim thisNode As myTreeNode = CType(AssociateNode.Nodes.Item(j), myTreeNode)
                        If AssociateNode.Nodes(j).Checked = True Then
                            Dim lstExam As New myList
                            Dim lst As New myList
                            Dim Emdeonlst As New gloEmdeonCommon.myList '' Added by kanchan on 20100823
                            If AssociateNode.Text = "ICD" Then
                                ''''''''''''''''''''
                                _ISICD9CPT = True
                                'split the CPT 
                                Dim strICD9 As String()
                                strICD9 = Split(thisNode.Name, "-", 2)

                                'split the ICD9
                                Dim strCPT As String()
                                strCPT = Split(CPTNode.Text, "-", 2)

                                Dim strICD9Code As String = ""
                                Dim strICD9Desc As String = ""
                                Dim strCPTCode As String = ""
                                Dim strCPTDesc As String = ""

                                strICD9Code = strICD9.GetValue(0)
                                strICD9Desc = strICD9.GetValue(1)
                                strCPTCode = strCPT.GetValue(0)
                                strCPTDesc = strCPT.GetValue(1)


                                lstonlyCPT = New myList
                                lstonlyCPT.Code = strICD9Code
                                lstonlyCPT.Description = strICD9Desc
                                lstonlyCPT.HistoryCategory = strCPTCode
                                lstonlyCPT.HistoryItem = strCPTDesc
                                lstonlyCPT.nICDRevision = thisNode.nICDRevision

                                Dim oDiagnosis As New ClsDiagnosisDBLayer()
                                Dim dtTreat As DataTable = oDiagnosis.FetchICD9forUpdate(m_ExamID, m_VisitID)
                                If Not IsNothing(dtTreat) Then
                                    If dtTreat.Rows.Count > 0 Then
                                        For n As Integer = 0 To dtTreat.Rows.Count - 1 Step 1
                                            If Convert.ToString(dtTreat.Rows(n)("sICD9Code")) <> "" And Convert.ToString(dtTreat.Rows(n)("sICD9Description")) <> "" Then
                                                If Convert.ToString(dtTreat.Rows(n)("nICDRevision")) <> lstonlyCPT.nICDRevision Then
                                                    MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                    oDiagnosis = Nothing
                                                    dtTreat.Dispose()
                                                    dtTreat = Nothing
                                                    Exit Sub
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                                oDiagnosis = Nothing
                                If Not IsNothing(dtTreat) Then
                                    dtTreat.Dispose()
                                    dtTreat = Nothing
                                End If

                                If arrexam.Count > 0 Then
                                    For m As Integer = 0 To arrexam.Count
                                        If (CType(arrexam(0), mytable).nICDRevision <> lstonlyCPT.nICDRevision) Then
                                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        End If
                                    Next
                                End If


                                arrexam.Add(New mytable(strICD9Desc, strICD9Code, Convert.ToInt16(thisNode.nICDRevision)))

                            ElseIf AssociateNode.Text = "Drugs" Then

                                oDrug = New gloEMRActors.Drug

                                oDrug.DrugsID = thisNode.Key
                                oDrug.DrugsName = thisNode.DrugName
                                oDrug.Dosage = thisNode.Dosage
                                oDrug.DrugForm = thisNode.DrugForm
                                oDrug.Route = thisNode.Route
                                oDrug.Frequency = thisNode.Frequency
                                oDrug.NDCCode = thisNode.NDCCode
                                oDrug.IsNarcotics = thisNode.IsNarcotics
                                oDrug.Duration = thisNode.Duration
                                oDrug.mpid = thisNode.mpid
                                oDrug.DrugQtyQualifier = thisNode.DrugQtyQualifier


                                arrDruglist.Add(oDrug)

                                oDrug = Nothing

                            ElseIf AssociateNode.Text = "Patient Education" Then

                                Dim lstPE As New myList
                                lstPE.ID = 3 '' For Patient Education
                                lstPE.Description = thisNode.Name
                                lstPE.Index = thisNode.Key
                                lstPE.HistoryCategory = CPTNode.NodeName

                                Dim l As Integer
                                Dim bIsExists As Boolean = False
                                For l = 0 To arrPE.Count - 1
                                    If lstPE.Description = CType(arrPE(l), myList).Description Then
                                        bIsExists = True
                                    End If
                                Next
                                If bIsExists = False Then
                                    arrPE.Add(lstPE)
                                End If

                            ElseIf AssociateNode.Text = "Tags" Then

                                Dim lstTags As New myList
                                lstTags.ID = 4 '' For Tags
                                lstTags.Description = thisNode.Name  '' TemplateName
                                lstTags.Index = thisNode.Key   '' TemplateID
                                'Added by Pradeep on 23122010
                                'For default Tag Change.
                                If thisNode.Text <> "" Then
                                    Dim strTags As String = thisNode.Text.ToString()
                                    Dim ind As Integer = strTags.LastIndexOf("-")
                                    If ind > -1 Then
                                        lstTags.HistoryItem = strTags.Substring(ind + 1)
                                    Else
                                        lstTags.HistoryItem = ""
                                    End If
                                    'End of code added by Pradeep.
                                ElseIf IsNothing(thisNode.TemplateResult) = True Then
                                    lstTags.HistoryItem = ""
                                Else
                                    lstTags.HistoryItem = thisNode.TemplateResult.ToString '' Asscociataed Tag
                                End If
                                lstTags.HistoryCategory = CPTNode.NodeName  '''' 
                                frmPatientExam.arrTagID.Add(lstTags) ''''thisNode.Key)
                            ElseIf AssociateNode.Text = "Orders and Results" Then
                                'Developer: Sanjog(Dhamke)
                                'Date:14 Dec 2011
                                'Bug ID/PRD Name/Salesforce Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                                'Reason: To show task info
                                If Not strTreatment.Contains(CPTNode.Name) Then
                                    strTreatment += CPTNode.Name & ", "
                                    strLabTaskDescription += AssociateNode.Nodes(j).Text & ", "
                                Else
                                    strLabTaskDescription += AssociateNode.Nodes(j).Text & ", "
                                End If

                                Emdeonlst.Value = AssociateNode.Nodes(j).Text
                                Emdeonlst.ID = AssociateNode.Nodes(j).Tag
                                arrLabs.Add(Emdeonlst)
                                'Code End-Added by kanchan on 20100823 for changes in logic
                            ElseIf AssociateNode.Text = "Order Templates" Then
                                lst.Value = AssociateNode.Nodes(j).Text
                                lst.Index = AssociateNode.Nodes(j).Tag
                                arrRadiology.Add(lst) 'AssociateNode.Nodes(j).Text)

                            ElseIf AssociateNode.Text = "Referral Letter" Then
                                lst.Value = AssociateNode.Nodes(j).Text
                                lst.Index = AssociateNode.Nodes(j).Tag
                                arrTemplate.Add(lst)

                                ReffCnt = ReffCnt + 1
                                If ReffCnt >= 2 Then

                                    MessageBox.Show("Please select only one Referral ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                    frmPatientExam.nRefTempID = 0
                                    Exit Sub
                                End If

                                'arrTemplate.Add(lst)


                                'AssociateNode.Nodes(j).Text)
                            ElseIf AssociateNode.Text = "FlowSheet" Then
                                lst.Value = AssociateNode.Nodes(j).Text
                                lst.Index = AssociateNode.Nodes(j).Tag
                                arrFlow.Add(lst)

                                'AssociateNode.Nodes(j).Text)


                            End If
                        End If

                        'If AssociateNode.Text = "CPT" Then
                        arrCPTOnly.Add(lstonlyCPT)
                        _IsRootNode = False

                        'End If
                    Next 'For j As Integer = 0 To AssociateNode.GetNodeCount(False) - 1
                Next  'For k As Integer = 0 To 3
            End If
            'arrCPTOnly.Add(lstonlyICD9)
        Next 'For i = 0 To trICD9Association.Nodes(0).GetNodeCount(False) - 1

        If gblnICD9Driven Then
            saveTreatment()
        Else
            SaveCPTDrivenDiagnosis()
        End If



        ''Use datarow for performance
        If Not IsNothing(_dt) Then
            For Each drTask As DataRow In _dt.Rows
                'Code Start-Added by kanchan on 20100622 for generate task for drug
                If drTask(1) = "Drugs" Then ''drTask(1) for sFieldName
                    If arrDruglist.Count > 0 Then
                        'if Condition Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus
                                Dim ofrmPrescription As frmPrescription
                                ofrmPrescription = frmPrescription.GetInstance(arrDruglist, m_ProviderID, m_VisitID, _PatientID)
                                If IsNothing(ofrmPrescription) = True Then
                                    Exit Sub
                                End If
                                If frmPrescription.IsOpen = False Then
                                    'Incident #00013567 : Medication carry forward case
                                    'following changes done to resolve incident
                                    'If ofrmPrescription.LockForm(_PatientID) = False Then
                                    '    ofrmPrescription.Dispose()
                                    '    ofrmPrescription = Nothing

                                    'Else                                    
                                    With ofrmPrescription
                                        .WindowState = FormWindowState.Maximized
                                        .BringToFront()
                                        '.ShowReconcileMessage()
                                        .ShowDialog(IIf(IsNothing(ofrmPrescription.Parent), Me, ofrmPrescription.Parent))
                                        ofrmPrescription.Dispose()
                                        ofrmPrescription = Nothing
                                    End With
                                    'End If
                                Else
                                    MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                ''''Dim ofrmPrescription As New frmPrescription(arrDruglist, m_ProviderID, m_VisitID, _PatientID)
                            Else
                                Dim ofrmPrescription As frmPrescription
                                ofrmPrescription = frmPrescription.GetInstance(arrDruglist, m_ProviderID, m_VisitID, _PatientID, False)
                                If IsNothing(ofrmPrescription) = True Then
                                    Exit Sub
                                End If
                                With ofrmPrescription
                                    .WindowState = FormWindowState.Minimized
                                    .Opacity = 0
                                    '.ShowReconcileMessage()
                                    .Show()
                                    .Hide()
                                    .DeleteLockRecord()
                                    .Close()
                                End With
                            End If
                        Else
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                'Generate Task for Drug
                                If Not IsNothing(arrDruglist) Then
                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nDrugProviderID As Int64
                                    Dim sDrugProviderName As String

                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                    oPatientExam.Dispose()
                                    oPatientExam = Nothing
                                    dt = GetLabTaskProvider(nProviderId)

                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            nDrugProviderID = dt.Rows(0)("nProviderID")
                                            sDrugProviderName = dt.Rows(0)("ProviderName")
                                        End If
                                        dt.Dispose()
                                        dt = Nothing
                                    End If

                                    Dim strDrug As String = String.Empty
                                    Dim strDrugs As String = String.Empty

                                    'Added by kanchan on 20100624 for Append selected Drug in Notes
                                    Dim sDescription As String = " For Drug :" & vbCrLf
                                    Dim ncnt As Integer = 1

                                    For oDrugs1 As Integer = 0 To arrDruglist.Count - 1
                                        strDrug = DirectCast(DirectCast(arrDruglist.Item(oDrugs1), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).NDCCode
                                        If oDrugs1 = 0 Then
                                            strDrugs = strDrug
                                            'Added by kanchan on 20100624 for Append selected Drug in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDruglist.Item(oDrugs1), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                            ncnt = ncnt + 1

                                        Else
                                            strDrugs = strDrugs & "|" & strDrug
                                            'Added by kanchan on 20100624 for Append selected Drug in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDruglist.Item(oDrugs1), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                            ncnt = ncnt + 1

                                        End If
                                    Next

                                    ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, _PatientID, nDrugProviderID, sDescription, "Drugs available", TaskType.Drug, gstrLoginName)
                                    ''Added Rahul on 20101026
                                    Dim ofrm As New gloTaskMail.frmTask
                                    ofrm.DataBaseConnectionString = GetConnectionString()
                                    ofrm.TaskID = 0
                                    ofrm.PatientID = _PatientID
                                    ofrm.ProviderID = nDrugProviderID
                                    ofrm.rtxtDescription.Text = sDescription
                                    ofrm.txtSubject.Text = "Drugs available"
                                    ofrm._TaskType = TaskType.Drug
                                    ofrm._UserName = gstrLoginName
                                    ofrm.UserID = gnLoginID
                                    ofrm._sNotesExt = strDrugs

                                    If drTask(8) <> "" Then ''drTask(8) for sUserID
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Dispose()
                                    ofrm = Nothing

                                    ''End

                                    ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                    ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                    ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strDrugs & "' where ntaskid=" & _TaskId
                                    ' '' ''oDB.Connect(False)
                                    ' '' ''oDB.Execute_Query(strQry)

                                End If '
                            Else ''Save the Task.

                                Dim strDrug As String = String.Empty
                                Dim strDrugs As String = String.Empty

                                Dim sDescription As String = " For Drug :" & vbCrLf
                                Dim ncnt As Integer = 1

                                For oDrugs1 As Integer = 0 To arrDruglist.Count - 1
                                    strDrug = DirectCast(DirectCast(arrDruglist.Item(oDrugs1), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).NDCCode
                                    If oDrugs1 = 0 Then
                                        strDrugs = strDrug
                                        'Added by kanchan on 20100624 for Append selected Drug in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDruglist.Item(oDrugs1), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strDrugs = strDrugs & "|" & strDrug
                                        'Added by kanchan on 20100624 for Append selected Drug in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDruglist.Item(oDrugs1), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                        ncnt = ncnt + 1

                                    End If
                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(8) for sUserID & drTask(6) for sTaskusers
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If
                                oclsSmartDiagnosis.AddTasks("Drugs available", sDescription, Now.ToString(), Now.ToString(), TaskType.Drug, strDrugs, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If
                    End If
                    'Code End-Added by kanchan on 20100622 for generate task for drug

                    'objclsSmartDiagnosis.AddData(m_ExamID, m_VisitID, arrDruglist, arrPE)
                ElseIf drTask(1) = "Patient Education" Then ''drTask(1) for sFieldName
                    '' if there exits Templates for patient education in assocated ICD9 
                    If arrPE.Count > 0 Then
                        If drTask(2) = True Then ''drTask(2) for bFieldStatus
                            '    Dim objfrmpatienteducation As New frmPatientEducation(m_VisitID, _PatientID, m_ExamID, arrPE)
                            '    objfrmpatienteducation.ShowDialog(IIf(IsNothing(objfrmpatienteducation.Parent), Me, objfrmpatienteducation.Parent))
                            '    objfrmpatienteducation.Dispose()
                            '    objfrmpatienteducation = Nothing
                            'Else
                            '    '''' to Show Patient Education Form
                            '    Dim objfrmpatienteducation As New frmPatientEducation(m_VisitID, _PatientID, m_ExamID, arrPE)
                            '    objfrmpatienteducation.Opacity = 0
                            '    objfrmpatienteducation.Show()
                            '    objfrmpatienteducation.Hide()
                            '    objfrmpatienteducation.SaveExamEducation(False)
                            '    objfrmpatienteducation.Close()

                            Dim frm As frmPatientEducationPreview

                            For i = 0 To arrPE.Count - 1
                                frm = New frmPatientEducationPreview()
                                frm.VISID = m_VisitID
                                frm.PATID = _PatientID
                                frm.TMPID = CType(arrPE(i), myList).Index
                                frm.TempName = CType(arrPE(i), myList).Description
                                frm.EXAMID = m_ExamID
                                frm.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterTreatment
                                frm.ResourcCat = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
                                frm.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
                                frm.ISGRID = False
                                frm.FromOutSide = True
                                frm.ShowDialog(IIf(IsNothing(frmPatientEducationPreview.Parent), Me, frmPatientEducationPreview.Parent))
                                frm.Close()
                                frm.Dispose()
                                frm = Nothing
                            Next

                        Else
                            '''' to Show Patient Education Form
                            'Dim frmPatientPE As New frmPatientEducation(m_VisitID, _PatientID, m_ExamID, arrPE)

                            'With frmPatientPE
                            '    '  .myCaller = frmPatientExam
                            '    .blnOpenFromExam = False
                            '    .StartPosition = FormStartPosition.CenterParent
                            '    '.Visible = False
                            '    .Opacity = 0
                            '    .Show()
                            '    .Hide()
                            '    .SaveExamEducation(False)
                            '    .Close()
                            'End With
                            'frmPatientPE = Nothing 'Change made to solve memory Leak and word crash issue

                            If arrPE.Count > 0 Then

                                Dim frm As frmPatientEducationPreview
                                frm = New frmPatientEducationPreview()
                                frm.VISID = m_VisitID
                                frm.PATID = _PatientID
                                'frm.TMPID = CType(arrPE(i), myList).Index
                                'frm.TempName = CType(arrPE(i), myList).Description
                                frm.EXAMID = m_ExamID
                                frm.ISGRID = False
                                frm.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterTreatment
                                frm.ResourcCat = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
                                frm.ResourcTyp = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
                                frm.ArrList = arrPE
                                frm.FromOutSide = True
                                frm.Opacity = 0
                                frm.Show()
                                frm.Hide()
                                'frm.SaveExamEducationsWithArrayList(False)
                                frm.Close()
                                'frm.Dispose()
                                frm = Nothing

                            End If

                        End If

                    End If

                ElseIf drTask(1) = "Flowsheet" Then ''drTask(1) for sFieldName
                    If arrFlow.Count > 0 Then
                        'if Condition Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus
                                Dim objfrmpatientflowsheet As New frmPatientFlowSheet(_PatientID)
                                ''Bug : 00000828: Record locking
                                If objfrmpatientflowsheet.FormLevelLock() Then
                                    objfrmpatientflowsheet.WindowState = FormWindowState.Maximized
                                    frmPatientFlowSheet.Array_Flow_List = arrFlow

                                    objfrmpatientflowsheet.ShowDialog(IIf(IsNothing(objfrmpatientflowsheet.Parent), Me, objfrmpatientflowsheet.Parent))
                                End If
                                objfrmpatientflowsheet.Dispose()
                                objfrmpatientflowsheet = Nothing

                            Else
                                Dim objfrmpatientflowsheet As New frmPatientFlowSheet(_PatientID)
                                ''Bug : 00000828: Record locking
                                If objfrmpatientflowsheet.FormLevelLock() Then
                                    objfrmpatientflowsheet.WindowState = FormWindowState.Maximized
                                    frmPatientFlowSheet.Array_Flow_List = arrFlow

                                    objfrmpatientflowsheet.Opacity = 0
                                    objfrmpatientflowsheet.Show()
                                    objfrmpatientflowsheet.Hide()
                                    objfrmpatientflowsheet.SavePatientFlowSheet()
                                    objfrmpatientflowsheet.Close()
                                Else
                                    objfrmpatientflowsheet.Dispose()
                                    objfrmpatientflowsheet = Nothing
                                End If
                            End If
                        Else
                            'Code Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                            'Generate Task for Flowsheet
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form.drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrFlow) Then
                                    '        If arrLabs.Count > 0 Then
                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nFlowsheetProviderID As Int64
                                    Dim sFlowsheetProviderName As String
                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                    oPatientExam.Dispose()
                                    oPatientExam = Nothing
                                    dt = GetLabTaskProvider(nProviderId)

                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            nFlowsheetProviderID = dt.Rows(0)("nProviderID")
                                            sFlowsheetProviderName = dt.Rows(0)("ProviderName")
                                        End If
                                        dt.Dispose()
                                        dt = Nothing
                                    End If

                                    Dim strFlow As String = String.Empty
                                    Dim strFlows As String = String.Empty

                                    'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                    Dim sDescription As String = " For Flowsheet :" & vbCrLf
                                    Dim ncnt As Integer = 1

                                    For oFlowsheet As Integer = 0 To arrFlow.Count - 1
                                        strFlow = CType(arrFlow.Item(oFlowsheet), myList).ID & "~" & CType(arrFlow.Item(oFlowsheet), myList).Value
                                        If oFlowsheet = 0 Then
                                            strFlows = strFlow
                                            'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                            ncnt = ncnt + 1

                                        Else
                                            strFlows = strFlows & "|" & strFlow
                                            'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                            ncnt = ncnt + 1

                                        End If
                                    Next

                                    ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, _PatientID, nFlowsheetProviderID, sDescription, "Flowsheet available", TaskType.Flowsheet, gstrLoginName)
                                    ''Added Rahul on 20101026
                                    Dim ofrm As New gloTaskMail.frmTask
                                    ofrm.DataBaseConnectionString = GetConnectionString()
                                    ofrm.TaskID = 0
                                    ofrm.PatientID = _PatientID
                                    ofrm.ProviderID = nFlowsheetProviderID
                                    ofrm.rtxtDescription.Text = sDescription
                                    ofrm.txtSubject.Text = "Flowsheet available"
                                    ofrm._TaskType = TaskType.Flowsheet
                                    ofrm._UserName = gstrLoginName
                                    ofrm.UserID = gnLoginID
                                    ofrm._sNotesExt = strFlows

                                    If drTask(8) <> "" Then ''drTask(8) for sUserID
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Dispose()
                                    ofrm = Nothing

                                    ''End
                                    ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                    ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                    ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strFlows & "' where ntaskid=" & _TaskId
                                    ' '' ''oDB.Connect(False)
                                    ' '' ''oDB.Execute_Query(strQry)

                                End If
                            Else '' If False Then Save the Task.
                                Dim strFlow As String = String.Empty
                                Dim strFlows As String = String.Empty

                                'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                Dim sDescription As String = " For Flowsheet :" & vbCrLf
                                Dim ncnt As Integer = 1

                                For oFlowsheet As Integer = 0 To arrFlow.Count - 1
                                    strFlow = CType(arrFlow.Item(oFlowsheet), myList).ID & "~" & CType(arrFlow.Item(oFlowsheet), myList).Value
                                    If oFlowsheet = 0 Then
                                        strFlows = strFlow
                                        'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strFlows = strFlows & "|" & strFlow
                                        'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    End If
                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then
                                    _sUserID = drTask(8) ''drTask(8) for sUserID
                                    _sTaskusers = drTask(6) ''drTask(6) for sTaskusers
                                End If

                                oclsSmartDiagnosis.AddTasks("Flowsheet available", sDescription, Now.ToString(), Now.ToString(), TaskType.Flowsheet, strFlows, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If
                    End If


                ElseIf drTask(1) = "Orders and Results" Then ''drTask(1) for sFieldName
                    If arrLabs.Count > 0 Then
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus

                                Dim _TestList As String = String.Empty

                                Dim _MyTestList As gloEmdeonCommon.myList = Nothing
                                'Developer: Sanjog(Dhamke)
                                'Date:14 Dec 2011
                                'Bug ID/PRD Name/Salesforce Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                                'Reason: To show task info
                                _TestList = "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")
                                _TestList += vbNewLine & "Treatment:" & vbNewLine & strTreatment.Trim().Trim(",")

                                If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage <> "" Then

                                    Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage

                                        Case "TASK"
                                            gloLabSettings("TASK", _TestList, arrLabs)
                                        Case "LABORDER"
                                            gloLabSettings("LABORDER", "", arrLabs) '' added to show testnames on Emdeon screen ,v8022
                                        Case "RECORDRESULTS"
                                            gloLabSettings("RECORDRESULTS", "", arrLabs)
                                        Case "ASK"
                                            ' new modal dialog for instant choice for next action to be performed.
                                            Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                            frmAskform.ShowInTaskbar = False
                                            frmAskform.BringToFront()
                                            frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                            gloLabSettings(frmAskform.LabFlowConfirm, _TestList, arrLabs)
                                            frmAskform.Dispose()
                                            frmAskform = Nothing
                                        Case Else
                                            MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Select
                                    End Select
                                    _TestList = String.Empty
                                Else
                                    Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                    frmAskform.ShowInTaskbar = False
                                    frmAskform.BringToFront()
                                    frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                    gloLabSettings(frmAskform.LabFlowConfirm, _TestList)
                                    frmAskform.Dispose()
                                    frmAskform = Nothing
                                End If

                            Else
                                'Code Start-Added by kanchan on 20100823 for changes in logic
                                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(_PatientID)
                                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                frmNormalLab.ArrLabs = arrLabs '' Added by Abhijeet on 20100624
                                frmNormalLab.WindowState = FormWindowState.Minimized    '''''' added by Ujwala as on 11252010
                                frmNormalLab.ShowInTaskbar = False
                                frmNormalLab.BringToFront()
                                frmNormalLab.Show()
                                frmNormalLab.Hide()
                                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                frmNormalLab.Close()
                                frmNormalLab.Dispose()
                                'Code End-Added by kanchan on 20100823 for changes in logic

                            End If ''_dt.Rows(x)("bFieldStatus") = True
                        Else
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrLabs) Then
                                    If arrLabs.Count > 0 Then
                                        Dim dt As DataTable
                                        dt = Nothing
                                        Dim nLabProviderID As Int64
                                        Dim sLabProviderName As String
                                        Dim oPatientExam As New clsPatientExams
                                        Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                        oPatientExam.Dispose()
                                        oPatientExam = Nothing

                                        dt = GetLabTaskProvider(nProviderId)

                                        If Not IsNothing(dt) Then
                                            If dt.Rows.Count > 0 Then
                                                nLabProviderID = dt.Rows(0)("nProviderID")
                                                sLabProviderName = dt.Rows(0)("ProviderName")
                                            End If
                                            dt.Dispose()
                                            dt = Nothing
                                        End If

                                        Dim strlabs As String = String.Empty
                                        Dim strlab As String = String.Empty

                                        For olab As Integer = 0 To arrLabs.Count - 1
                                            strlab = CType(arrLabs.Item(olab), gloEmdeonCommon.myList).ID & "~" & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                            If olab = 0 Then
                                                strlabs = strlab
                                            Else
                                                strlabs = strlabs & "|" & strlab
                                            End If
                                        Next

                                        strLabTaskDescription = "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")
                                        strLabTaskDescription += vbNewLine & "Treatments:" & vbNewLine & strTreatment.Trim().Trim(",")

                                        ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, _PatientID, nLabProviderID, sDescription, "Lab available", TaskType.LabOrder, gstrLoginName)
                                        ''Added Rahul on 20101026
                                        Dim ofrm As New gloTaskMail.frmTask
                                        ofrm.DataBaseConnectionString = GetConnectionString()
                                        ofrm.TaskID = 0
                                        ofrm.PatientID = _PatientID
                                        ofrm.ProviderID = nLabProviderID
                                        ofrm.rtxtDescription.Text = strLabTaskDescription
                                        ofrm.txtSubject.Text = "Place Lab Order"
                                        ofrm._TaskType = TaskType.PlaceLabOrder
                                        ofrm._UserName = gstrLoginName
                                        ofrm.UserID = gnLoginID
                                        ofrm._sNotesExt = strlabs

                                        If drTask(8) <> "" Then ''drTask(8) for sUserID
                                            Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                            ofrm._taskuser_id = sUserID
                                            ofrm._SmartTask = True
                                        End If

                                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                        ofrm.Dispose()
                                        ofrm = Nothing
                                        ''End
                                        ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                        ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                        ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strlabs & "' where ntaskid=" & _TaskId
                                        ' '' ''oDB.Connect(False)
                                        ' '' ''oDB.Execute_Query(strQry)

                                    End If ''_dt.Rows(x)("bSendTask") = False
                                End If
                            Else '' If False Then Save the Task.
                                Dim strlabs As String = String.Empty
                                ''= SerializeArrayList(arrLabs)
                                Dim strlab As String = String.Empty

                                'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                Dim sDescription As String = " For Lab Test:" & vbCrLf
                                Dim ncnt As Integer = 1

                                For olab As Integer = 0 To arrLabs.Count - 1
                                    'Code commented & Added by kanchan on 20100823
                                    'strlab = CType(arrLabs.Item(olab), myList).ID & "~" & CType(arrLabs.Item(olab), myList).Value
                                    strlab = CType(arrLabs.Item(olab), gloEmdeonCommon.myList).ID & "~" & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                    If olab = 0 Then
                                        strlabs = strlab
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strlabs = strlabs & "|" & strlab
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    End If

                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then
                                    _sUserID = drTask(8) ''drTask(8) for sUserID
                                    _sTaskusers = drTask(6) ''drTask(6) for sTaskusers
                                End If

                                oclsSmartDiagnosis.AddTasks("Place Lab Order", sDescription, Now.ToString(), Now.ToString(), TaskType.PlaceLabOrder, strlabs, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If
                    End If



                ElseIf drTask(1) = "Order Templates" Then ''drTask(1) for sFieldName
                    If arrRadiology.Count > 0 Then
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus
                                'Dim frm As New frm_LM_Orders(m_VisitID, Now, _PatientID)
                                Dim frm As frm_LM_Orders
                                frm = frm_LM_Orders.GetInstance(m_VisitID, Now, _PatientID)
                                If IsNothing(frm) = True Then
                                    Exit Sub
                                End If
                                With frm
                                    ._ExamID = m_ExamID
                                    ._ArrRadi = arrRadiology
                                    ''._patientID = _PatientID
                                    ''._VisitID = m_VisitID
                                    ''._VisitDate = Now
                                    .WindowState = FormWindowState.Maximized
                                    .BringToFront()
                                    .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    .Dispose()
                                End With
                            Else
                                'Dim frm As New frm_LM_Orders(m_VisitID, Now, _PatientID)
                                Dim frm As frm_LM_Orders
                                frm = frm_LM_Orders.GetInstance(m_VisitID, Now, _PatientID)
                                If IsNothing(frm) = True Then
                                    Exit Sub
                                End If
                                With frm
                                    ._ExamID = m_ExamID
                                    ._ArrRadi = arrRadiology
                                    ''._patientID = _PatientID
                                    ''._VisitID = m_VisitID
                                    ''._VisitDate = Now
                                    .WindowState = FormWindowState.Minimized
                                    '.BringToFront()
                                    .Opacity = 0
                                    .Show()
                                    .Hide()
                                    .SaveOrders()
                                    .Close()
                                End With
                            End If ''_dt.Rows(x)("bFieldStatus") = True
                        Else
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrRadiology) Then
                                    If arrRadiology.Count > 0 Then
                                        Dim dt As DataTable
                                        dt = Nothing
                                        Dim nOrderProviderID As Int64
                                        Dim sOrderProviderName As String
                                        Dim oPatientExam As New clsPatientExams
                                        Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                        oPatientExam.Dispose()
                                        oPatientExam = Nothing

                                        dt = GetLabTaskProvider(nProviderId)

                                        If Not IsNothing(dt) Then
                                            If dt.Rows.Count > 0 Then
                                                nOrderProviderID = dt.Rows(0)("nProviderID")
                                                sOrderProviderName = dt.Rows(0)("ProviderName")
                                            End If
                                            dt.Dispose()
                                            dt = Nothing
                                        End If

                                        Dim strOrders As String = String.Empty
                                        ''= SerializeArrayList(arrLabs)
                                        Dim strOrder As String = String.Empty

                                        'Added by kanchan on 20100624 for Append selected Order in Notes
                                        Dim sDescription As String = " For Order:" & vbCrLf
                                        Dim ncnt As Integer = 1

                                        For oOrder As Integer = 0 To arrRadiology.Count - 1
                                            strOrder = CType(arrRadiology.Item(oOrder), myList).Index & "~" & CType(arrRadiology.Item(oOrder), myList).Value
                                            If oOrder = 0 Then
                                                strOrders = strOrder
                                                'Added by kanchan on 20100624 for Append selected Order in Notes
                                                sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                                ncnt = ncnt + 1

                                            Else
                                                strOrders = strOrders & "|" & strOrder
                                                'Added by kanchan on 20100624 for Append selected Order in Notes
                                                sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                                ncnt = ncnt + 1
                                            End If

                                        Next

                                        ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, _PatientID, nOrderProviderID, sDescription, "Order available", TaskType.OrderRadiology, gstrLoginName)
                                        ''Added Rahul on 20101026
                                        Dim ofrm As New gloTaskMail.frmTask
                                        ofrm.DataBaseConnectionString = GetConnectionString()
                                        ofrm.TaskID = 0
                                        ofrm.PatientID = _PatientID
                                        ofrm.ProviderID = nOrderProviderID
                                        ofrm.rtxtDescription.Text = sDescription
                                        ofrm.txtSubject.Text = "Order available"
                                        ofrm._TaskType = TaskType.OrderRadiology
                                        ofrm._UserName = gstrLoginName
                                        ofrm.UserID = gnLoginID
                                        ofrm._sNotesExt = strOrders

                                        If drTask(8) <> "" Then ''drTask(8) for sUserID
                                            Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                            ofrm._taskuser_id = sUserID
                                            ofrm._SmartTask = True
                                        End If

                                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                        ofrm.Dispose()
                                        ofrm = Nothing
                                        ''End
                                        ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                        ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                        ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strOrders & "' where ntaskid=" & _TaskId
                                        ' '' ''oDB.Connect(False)
                                        ' '' ''oDB.Execute_Query(strQry)

                                    End If  ''arrRadiology.Count > 0
                                End If   ''Not IsNothing(arrRadiology) '''
                            Else '' If False Then Save the Task.

                                Dim strOrders As String = String.Empty
                                Dim strOrder As String = String.Empty

                                'Added by kanchan on 20100624 for Append selected Order in Notes
                                Dim sDescription As String = " For Order:" & vbCrLf
                                Dim ncnt As Integer = 1

                                For oOrder As Integer = 0 To arrRadiology.Count - 1
                                    strOrder = CType(arrRadiology.Item(oOrder), myList).Index & "~" & CType(arrRadiology.Item(oOrder), myList).Value
                                    If oOrder = 0 Then
                                        strOrders = strOrder
                                        'Added by kanchan on 20100624 for Append selected Order in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strOrders = strOrders & "|" & strOrder
                                        'Added by kanchan on 20100624 for Append selected Order in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    End If

                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then
                                    _sUserID = drTask(8) ''drTask(8) for sUserID
                                    _sTaskusers = drTask(6) ''drTask(6) for sTaskusers
                                End If

                                oclsSmartDiagnosis.AddTasks("Order available", sDescription, Now.ToString(), Now.ToString(), TaskType.OrderRadiology, strOrders, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If ''_dt.Rows(x)("bSendTask") = False
                    End If  ''arrRadiology.Count > 0



                    '' To Show Prescription Form 
                ElseIf drTask(1) = "Referral Letter" Then ''drTask(1) for sFieldName
                    If arrTemplate.Count > 0 Then
                        If Not mycaller Is Nothing Then
                            Dim _TemplateName As String = ""
                            _TemplateName = CType(arrTemplate.Item(0), myList).Value
                            frmPatientExam.nRefTempID = Convert.ToInt64(CType(arrTemplate.Item(0), myList).Index)


                            'swaraj 02-04-2010 -- To open the patient Referral form when check box is checked in smart settings and to save the settings made in Patient Referral Form'
                            Dim dtVisitRef As New DataTable
                            Dim dtPatRef As New DataTable
                            ''check if Referrals exists against given m_VisitId
                            If Not objReferralsDBLayer.CheckReferral(m_VisitID, m_ExamID, _PatientID) Then

                                dtVisitRef = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitID, _PatientID, m_ExamID)

                                SaveReferrals(dtVisitRef, True, _TemplateName)
                            Else
                                'if Referral Details do not exist for that m_VisitId,
                                'Populate Referrals Details from Patient_Dtl Table
                                dtPatRef = objReferralsDBLayer.FillControls("R", _PatientID)
                                SaveReferrals(dtPatRef, False, _TemplateName)
                            End If

                            'Dim clsExam As New clsPatientExams
                            'Dim chkPatRefPricnt As Integer = 0
                            'chkPatRefPricnt = clsExam.Chk_Reff_PriCarePhycnt(_PatientID)
                            'If (chkPatRefPricnt <= 0) Then
                            '    If MessageBox.Show("Referral is  not Associated  for Patient. Do You Want to Associate It? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            '        Exit Sub
                            '    End If

                            'End If
                            frmSummaryofVisit.PatientTemplateID = Convert.ToInt64(CType(arrTemplate.Item(0), myList).Index)

                            Dim frm As frmSummaryofVisit = Nothing
                            Try
                                ' Dim frm As New frmSummaryofVisit(PatientID, mgnVisitID, examid, sFileName, ExamProviderId, blnExamFinished, True, "", nRefTempID)
                                frm = New frmSummaryofVisit(_PatientID, m_VisitID, m_ExamID, True, m_ExamFilePath, ExamProviderId, blnExamFinished, True)
                                ' nRefTempID = 0
                                If drTask(2) = True Then ''drTask(2) for bFieldStatus
                                    'frm.myCaller = Me
                                    frm.dtDos = m_ExamDate
                                    frmExamChild = frm
                                    frm.Text = "Patient Referrals"
                                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                                        frm.Close()
                                    End If
                                    If Not IsNothing(frm) Then
                                        frm.Dispose()
                                        frm = Nothing
                                    End If
                                Else
                                    'frm.myCaller = Me
                                    frm.dtDos = m_ExamDate
                                    frmExamChild = frm
                                    frm.Text = "Patient Referrals"

                                    frm.Opacity = 0
                                    frm.Show(Me)

                                    ''''''''''''''''''''''''
                                    '' ''Dim strFName As String
                                    ''strFName = ExamNewDocumentName
                                    ''Dim oCurDoc As New Wd.Document

                                    ''oCurDoc.SaveAs(strFName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                                    '' ''wdReferrals.Save(strFName, True, "", "")
                                    ''wdReferrals = New AxDSOFramer.AxFramerControl
                                    '' ''Me.Controls.Add(wdReferrals)

                                    'oCurDoc = New Wd.Document
                                    'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                                    'wdReferrals.Save(strFileName, True, "", "")
                                    ' ''frm.SaveTreeNode(0, strFName)
                                    ''''''''''''''''''''''''
                                    frm.Hide()

                                    frm.SaveReferrals()
                                    'frm.Close()

                                End If

                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                If Not IsNothing(frm) Then  'Obj disposed by Mitesh 
                                    frm.Close()
                                End If

                                If Not IsNothing(frm) Then
                                    frm.Dispose()
                                    frm = Nothing
                                End If
                            End Try

                            'end swaraj'

                        Else
                            frmPatientExam.nRefTempID = 0
                        End If
                    Else
                        frmPatientExam.nRefTempID = 0
                    End If

                End If

            Next
        End If
        ''End

        ''new code written

        frmPatientExam.Arrlist = arrexam
        frmPatientExam.blnChangesMade = True
        frmPatientExam.nRefTempID = 0
        '  Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)

    End Sub
    Private Sub SaveReferrals(ByVal objTable As DataTable, ByVal blnRef As Boolean, ByVal TemplateName As String)
        If Not objTable Is Nothing Then
            If objTable.Rows.Count > 0 Then
                Dim Arrlist As New ArrayList
                For j As Int32 = 0 To objTable.Rows.Count - 1
                    Dim strRefName As String = ""
                    Dim strFirstName As String = ""
                    Dim strMiddleName As String = "'"
                    Dim strLastName As String = "'"
                    Dim m_ContactId As Int64
                    Dim bIsPCP As Boolean
                    Dim ContactFlag As Int16
                    If blnRef Then
                        strRefName = objTable.Rows(j)(2).ToString

                        If Not IsDBNull(objTable.Rows(j)(2)) Then
                            m_ContactId = CType(objTable.Rows(j)(2), Int64)
                        End If
                        bIsPCP = CType(objTable.Rows(j)("bIsPCP"), Boolean)
                        ContactFlag = CType(objTable.Rows(j)("nContactFlag"), Int16)
                    Else
                        strRefName = objTable.Rows(j)(1).ToString
                        strFirstName = objTable.Rows(j)("sFirstName").ToString()
                        strMiddleName = objTable.Rows(j)("sMiddleName").ToString()
                        strLastName = objTable.Rows(j)("sLastName").ToString()
                        If Not IsDBNull(objTable.Rows(j)(0)) Then
                            m_ContactId = CType(objTable.Rows(j)(0), Int64)
                        End If
                        If CType(objTable.Rows(j)(2), String) = "P" Then
                            bIsPCP = True
                        Else
                            bIsPCP = False
                        End If
                        ContactFlag = CType(objTable.Rows(j)("nContactFlag"), Int16)
                    End If
                    Dim lst As New myList
                    'need to save templateid stored against every referral entry
                    lst.ID = frmPatientExam.nRefTempID 'TemplateID
                    lst.Index = m_ContactId 'ReferralID
                    lst.Description = strRefName '' ReferralName
                    lst.Type = bIsPCP
                    lst.ContactFlag = ContactFlag
                    lst.TemplateResult = Nothing '' Template(Object)
                    lst.ContactTemplateName = TemplateName
                    lst.ContactFirstName = strFirstName
                    lst.ContactMiddleName = strMiddleName
                    lst.ContactLastName = strLastName
                    '  lst.ID = _TemplateID
                    Arrlist.Add(lst)
                Next
                If blnRef Then
                    objReferralsDBLayer.AddData(Arrlist, m_VisitID, DateTime.Now, _PatientID, 2)
                Else
                    objReferralsDBLayer.AddData(Arrlist, m_VisitID, DateTime.Now, _PatientID, m_ExamID)

                End If

            End If
        End If


    End Sub

    Public Sub saveTreatment()
        Try
            With C1Dignosis
                .Col = Col_ICD9Code
                .Select()
                Dim i As Integer
                Dim lst As myList
                'Dim lstExam As myList

                Dim arrList As New ArrayList
                Dim oclsDiagnosis As ClsDiagnosisDBLayer
                oclsDiagnosis = New ClsDiagnosisDBLayer

                Dim strICD9Code As String = ""
                Dim strICD9Desc As String = ""
                Dim strCPTCode As String = ""
                Dim strCPTDesc As String = ""
                Dim strMODCode As String = ""
                Dim strMODDesc As String = ""
                Dim nICD9Count As Integer = 0
                Dim nCPTCount As Integer = 0
                Dim nModCount As Integer = 0
                Dim intUnits As System.Int64

                Dim strSnomedCode As String = ""
                Dim strSnomedDesc As String = ""
                Dim nICDRevision As Int16
                Dim blnOneSnoMed As Boolean
                For i = 1 To .Rows.Count - 1
                    lst = New myList
                    Dim _Node As C1.Win.C1FlexGrid.Node


                    _Node = .Rows(i).Node


                    If _Node.Level = 1 Then
                        intUnits = C1Dignosis.GetData(i, Col_Units)
                    End If
                    strICD9Code = .GetData(_Node.Row.Index, Col_ICD9Code)
                    strICD9Desc = .GetData(_Node.Row.Index, Col_ICD9Desc)
                    strSnomedCode = .GetData(_Node.Row.Index, Col_SnomedCode)
                    strSnomedDesc = .GetData(_Node.Row.Index, Col_SnomedDesc)
                    'if current row don't have any child means it is leaf and save to database  
                    If _Node.Children = 0 Then
                        _Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                        Dim rowno As Integer = _Node.Row.Index
                        strSnomedCode = .GetData(rowno, Col_SnomedCode)
                        strSnomedDesc = .GetData(rowno, Col_SnomedDesc)
                        strICD9Code = .GetData(rowno, Col_ICD9Code)
                        strICD9Desc = .GetData(rowno, Col_ICD9Desc)
                        nICD9Count = .GetData(rowno, Col_ICD9Count)

                        strCPTCode = .GetData(rowno, COl_CPTCode)
                        strCPTDesc = .GetData(rowno, Col_CPTDesc)
                        nCPTCount = .GetData(rowno, Col_CPTCount)

                        strMODCode = .GetData(rowno, Col_ModCode)
                        strMODDesc = .GetData(rowno, Col_ModDesc)
                        nModCount = .GetData(rowno, Col_ModCount)
                        nICDRevision = Convert.ToInt16(.GetData(rowno, Col_ICDRevision))

                        'Dim htSno As New Hashtable()
                        ''14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                        'htSno = oclsDiagnosis.GetDefaultSnomed(strICD9Code, strICD9Desc, Convert.ToInt16(.GetData(rowno, Col_ICDRevision)), blnOneSnoMed, Me)

                        'If Not IsNothing(htSno) Then

                        '    Dim key As ICollection = htSno.Keys
                        '    Dim k As String

                        '    For Each k In key

                        '        strSnomedCode = k
                        '        strSnomedDesc = htSno(k)
                        '    Next k

                        'End If

                        'htSno = Nothing
                        'list for ICD9,CPT and Modifier in ExamICD9CPT Table
                        lst.Code = strICD9Code
                        lst.Description = strICD9Desc
                        lst.HistoryCategory = strCPTCode
                        lst.HistoryItem = strCPTDesc
                        lst.Value = strMODCode
                        lst.ParameterName = strMODDesc
                        lst.TemplateResult = intUnits
                        lst.ICD9Count = nICD9Count
                        lst.CPTCount = nCPTCount
                        lst.ModCount = nModCount
                        lst.SnoDescription = strSnomedDesc
                        lst.SnowMadeID = strSnomedCode
                        lst.nICDRevision = nICDRevision
                        lst.IsSnomedOneToOne = blnOneSnoMed
                        arrList.Add(lst)

                    End If


                Next

                'save data in ExamICDCPT Table
                oclsDiagnosis.SaveDiagTreatmentAssociation(m_ExamID, _PatientID, m_VisitID, arrList, Me)
                If .Row > 0 Then
                    strICD9Code = .GetData(.Row, Col_ICD9Code)
                    strICD9Desc = .GetData(.Row, Col_ICD9Desc)
                End If
                oclsDiagnosis = Nothing
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Original Save Smart Diagnosis Procedure"
    '' By Mahesh on 20060316
    '''' WHY? : Overwrites Diagnosis & other data
    Private Sub SaveAssociation_Original()

        'Get node count of child nodes in trvCPTAssociates
        'If trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) > 0 Then
        Dim arrDruglist As New ArrayList
        Dim i As Integer
        For i = 0 To trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) - 1
            Dim ICD9Node As myTreeNode
            'get the ICD9Node associated sequentially
            ICD9Node = trvCPTAssociation.Nodes.Item(0).Nodes.Item(i)
            If ICD9Node.GetNodeCount(True) > 0 Then
                Dim k As Integer
                Dim arrlist As New ArrayList
                For k = 0 To 3
                    Dim AssociateNode As myTreeNode
                    AssociateNode = ICD9Node.Nodes.Item(k)
                    Dim j As Integer
                    For j = 0 To AssociateNode.GetNodeCount(False) - 1
                        Dim thisNode As myTreeNode = CType(AssociateNode.Nodes.Item(j), myTreeNode)
                        If AssociateNode.Nodes.Item(j).Checked = True Then
                            If AssociateNode.Text = "CPT" Then
                                arrlist.Add(New myList(thisNode.Key, thisNode.Name, 1)) '' For CPT

                            ElseIf AssociateNode.Text = "Drugs" Then
                                Dim DrudID As Long = thisNode.Key
                                arrlist.Add(New myList(thisNode.Key, thisNode.Name, 2))  '' For Drugs
                                arrDruglist.Add(DrudID)
                                'arrDruglist.Add(New myList(thisNode.Key, thisNode.Name, 2))

                            ElseIf AssociateNode.Text = "Patient Education" Then
                                arrlist.Add(New myList(thisNode.Key, thisNode.Name, 3))  '' Patient Education

                            ElseIf AssociateNode.Text = "Tags" Then
                                arrlist.Add(New myList(thisNode.Key, thisNode.Name, 4))  '' For Tags
                                frmPatientExam.arrTagID.Add(thisNode.Key)
                            End If
                        End If
                    Next

                Next

                oclsSmartTreatment.AddData(m_ExamID, m_VisitID, arrlist, ICD9Node.NodeName, arrDruglist, _PatientID, Me)
            Else
                '''''if  for any ICD9 There is no Items Associated with it Then only That ICD9 is Saved as diagnosis
                ' oclsSmartTreatment.AddDiagnosis(m_ExamID, m_VisitID, ICD9Node.NodeName)
            End If
        Next
        RefreshICD9()
        frmPatientExam.blnChangesMade = True
        'End If
    End Sub
#End Region

    Private Sub trvCPTAssociation_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCPTAssociation.MouseDown
        Try
            If e.Button = MouseButtons.Right Then

                Dim trvNode As TreeNode
                trvNode = trvCPTAssociation.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trvCPTAssociation.SelectedNode = trvNode
                End If

                If IsNothing(trvCPTAssociation.SelectedNode.Parent) = True Then
                    'Try
                    '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                    '        trvCPTAssociation.ContextMenu.Dispose()
                    '        trvCPTAssociation.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvCPTAssociation.ContextMenu = Nothing
                    Exit Sub
                End If

                If IsNothing(trvCPTAssociation.SelectedNode) = False Then

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

                    ''If trvCPTAssociation.Nodes.Item(0).Text = trvCPTAssociation.SelectedNode.Text Or trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Or (CType(trvCPTAssociation.SelectedNode, myTreeNode).Key = -1) Then
                    If trvCPTAssociation.Nodes(0).Text = trvCPTAssociation.SelectedNode.Parent.Text Then
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = cntCPTAssociation
                    Else
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = Nothing
                        'treeindex = trPrescriptionDetails.SelectedNode.Index
                    End If
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If IsNothing(trvCPTAssociation.SelectedNode.Parent) = False Then
                Dim i As Integer
                Dim j As Integer

                'If trvCPTAssociation.SelectedNode Is trvCPTAssociation.Nodes.Item(0) Then
                '    '''' ICD9Node 
                '    For i = trvCPTAssociation.SelectedNode.GetNodeCount(True) - 1 To 0 Step -1
                '        trvCPTAssociation.SelectedNode.Nodes(i).Remove()
                '    Next
                '    Exit Sub
                'End If

                'If CType(trvCPTAssociation.SelectedNode, myTreeNode).Key = -1 Then
                '    '''' "CPT","Drugs","PE","Tags"
                '    For i = trvCPTAssociation.SelectedNode.GetNodeCount(True) - 1 To 0 Step -1
                '        trvCPTAssociation.SelectedNode.Nodes(i).Remove()
                '    Next
                '    Exit Sub
                'End If

                ''''''''<<<<<<<>>>>>>>>
                Dim strvCPT As String()
                strvCPT = Split(trvCPTAssociation.SelectedNode.Text, "-", 2)

                Dim RowIndex As Integer = -1
                RowIndex = oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtDiagnosis, strvCPT(0), strvCPT(1))
                If RowIndex <> -1 Then
                    oclsSmartTreatment.dtDiagnosis.Rows.RemoveAt(RowIndex)
                End If

                For i = 0 To trvCPTAssociation.SelectedNode.GetNodeCount(False) - 1
                    Dim myNode As myTreeNode
                    If trvCPTAssociation.SelectedNode.Nodes(i).Text = "CPT" Then
                        For j = 0 To trvCPTAssociation.SelectedNode.Nodes(i).GetNodeCount(False) - 1
                            myNode = CType(trvCPTAssociation.SelectedNode.Nodes(i).Nodes(j), myTreeNode)
                            If myNode.Checked = True Then
                                Dim strCPT As String()
                                strCPT = Split(myNode.Text, "-", 2)
                                RowIndex = -1
                                RowIndex = oclsSmartTreatment.Check_Existence(oclsSmartTreatment.dtTreatment, strCPT(0), strCPT(1))
                                If RowIndex <> -1 Then
                                    oclsSmartTreatment.dtTreatment.Rows.RemoveAt(RowIndex)
                                End If
                            End If
                        Next

                    ElseIf trvCPTAssociation.SelectedNode.Nodes(i).Text = "Drugs" Then

                    ElseIf trvCPTAssociation.SelectedNode.Nodes(i).Text = "Patient Education" Then
                        For j = 0 To trvCPTAssociation.SelectedNode.Nodes(i).GetNodeCount(False) - 1
                            myNode = CType(trvCPTAssociation.SelectedNode.Nodes(i).Nodes(j), myTreeNode)
                            If myNode.Checked = True Then
                                Dim l As Integer
                                For l = arrPE.Count - 1 To 0 Step -1
                                    If myNode.Name = CType(arrPE(l), myList).Description Then
                                        arrPE.RemoveAt(l)
                                    End If
                                Next
                            End If
                        Next
                    ElseIf trvCPTAssociation.SelectedNode.Nodes(i).Text = "Tags" Then

                    End If
                Next
                ''''''''''<<<<<<<>>>>>>>>

                For Each oCPT As TreeNode In trvCPTAssociation.SelectedNode.Nodes
                    If oCPT.Text = "ICD" Then
                        For Each oICD9 As TreeNode In oCPT.Nodes
                            For i = C1Dignosis.Rows.Count - 1 To 0 Step -1
                                If C1Dignosis.GetData(i, Col_ICD9Code_Description) = trvCPTAssociation.SelectedNode.Text Then
                                    If C1Dignosis.Rows(i).Node.Level = 1 Then
                                        Dim oNode As C1.Win.C1FlexGrid.Node = C1Dignosis.Rows(i).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                                        If oNode.Data = oICD9.Text Then
                                            C1Dignosis.Rows(i).Node.RemoveNode()
                                            For j = i To C1Dignosis.Rows.Count - 1
                                                If C1Dignosis.GetData(j, Col_CPTCount) <> 0 Then
                                                    C1Dignosis.SetData(j, Col_CPTCount, (CType(C1Dignosis.GetData(j, Col_CPTCount), Integer) - 1))
                                                Else
                                                    Exit For
                                                End If
                                            Next
                                            If oNode.Children <= 0 Then
                                                oNode.RemoveNode()
                                                For j = i - 1 To C1Dignosis.Rows.Count - 1
                                                    If CType(C1Dignosis.GetData(j, Col_ICD9Count), Integer) > 0 Then
                                                        C1Dignosis.SetData(j, Col_ICD9Count, (CType(C1Dignosis.GetData(j, Col_ICD9Count), Integer) - 1))
                                                    End If
                                                Next
                                            End If
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                Next


                Dim mychildnode As myTreeNode
                'Dim key As Long
                mychildnode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                mychildnode.Remove() 'delete from treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvCPT_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvCPT.DoubleClick
        Try
            IsTreeviewDoubleClick = True
            If IsNothing(trvCPT.SelectedNode) = False Then
                Dim mynode As myTreeNode
                mynode = CType(trvCPT.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            IsTreeviewDoubleClick = False
        End Try
    End Sub

    Private Sub AddNode(ByVal mynode As myTreeNode)

        'If mynode.Parent Is trvCPT.Nodes.Item(0) Then
        Dim str As String
        str = mynode.Text
        Dim mytragetnode As myTreeNode
        For Each mytragetnode In trvCPTAssociation.Nodes.Item(0).Nodes
            If mytragetnode.Text = str Then
                Exit Sub
            End If
        Next

        ''''Add CPT/Drugs/PE to icd9 node

        'trvCPT.SelectedNode.Remove()
        Dim associatenode As myTreeNode

        associatenode = mynode.Clone
        associatenode.Key = mynode.Key
        associatenode.Text = mynode.Text
        associatenode.NodeName = mynode.Text

        associatenode.ImageIndex = 5
        associatenode.SelectedImageIndex = 5

        'associatenode.Checked = True

        trvCPTAssociation.Nodes.Item(0).Nodes.Add(associatenode)


        'swaraj 29-03-2010 -- loading the tree node data '
        Dim mychild As myTreeNode

        For x As Integer = 0 To _dt.Rows.Count - 1
            mychild = New myTreeNode
            'mychild = New myTreeNode(_dt.Rows(x)("sFieldName").ToString(), -1)
            If _dt.Rows(x)("sFieldName").ToString = "ICD9" Then
                mychild.Text = "ICD"
                mychild.ImageIndex = 16
                mychild.SelectedImageIndex = 16
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Drugs" Then
                mychild.Text = "Drugs"
                mychild.ImageIndex = 1
                mychild.SelectedImageIndex = 1
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Patient Education" Then
                mychild.Text = "Patient Education"
                mychild.ImageIndex = 2
                mychild.SelectedImageIndex = 2
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Tags" Then
                mychild.Text = "Tags"
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Flowsheet" Then
                mychild.Text = "Flowsheet"
                mychild.ImageIndex = 11
                mychild.SelectedImageIndex = 11
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Orders and Results" Then
                mychild.Text = "Orders and Results"
                mychild.ImageIndex = 12
                mychild.SelectedImageIndex = 12
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Order Templates" Then
                mychild.Text = "Order Templates"
                mychild.ImageIndex = 13
                mychild.SelectedImageIndex = 13
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Referral Letter" Then
                mychild.Text = "Referral Letter"
                mychild.ImageIndex = 14
                mychild.SelectedImageIndex = 14
            End If
            'mychild.ImageIndex = x
            'mychild.SelectedImageIndex = x
            associatenode.Nodes.Add(mychild)
        Next


        Dim dt As DataTable
        dt = oclsSmartTreatment.FetchCPTforUpdate(associatenode.Key)
        Dim i As Integer
        If IsNothing(dt) = False Then




            Dim bl As Boolean = False
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)(12).ToString() = "True" Then
                    bl = True
                Else
                    bl = False
                End If
                If dt.Rows(i).Item(2) = "i" Then
                    ''                                   myTreeNode(    StrName             Key
                    Dim icnode As New myTreeNode(Convert.ToString(dt.Rows(i).Item(1)), Convert.ToInt64(dt.Rows(i).Item(0)), Convert.ToInt16(dt.Rows(i).Item("ICDRevision")))
                    icnode.Checked = bl
                    If Convert.ToInt16(dt.Rows(i).Item("ICDRevision")) = 10 Then
                        icnode.ImageIndex = 15
                        icnode.SelectedImageIndex = 15
                    Else
                        icnode.ImageIndex = 0
                        icnode.SelectedImageIndex = 0
                    End If
                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "ICD" Then
                            oNode.Nodes.Add(icnode)
                            Exit For
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(0).Nodes.Add(icnode)
                    '  associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                    Call Load_Dignosis(associatenode)
                ElseIf dt.Rows(i).Item(2) = "d" Then
                    '  Dim drnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    Dim _strDrugNM As String = dt.Rows(i).Item(1)
                    Dim _ndrugId As Long = Convert.ToInt64(dt.Rows(i).Item(0))
                    Dim _ndcCode As String = Convert.ToString(dt.Rows(i).Item(7))
                    Dim _mpid As Int32 = Convert.ToInt32(dt.Rows(i).Item(10))

                    Dim drnode As New myTreeNode(_strDrugNM, _ndrugId, _ndcCode, _mpid)
                    'Dim drnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0), dt.Rows(i).Item(1), dt.Rows(i).Item(3), dt.Rows(i).Item(4), dt.Rows(i).Item(5), dt.Rows(i).Item(6), dt.Rows(i).Item(7), dt.Rows(i).Item(8), dt.Rows(i).Item(9), dt.Rows(i).Item(10), dt.Rows(i).Item(11))
                    drnode.Checked = bl

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Drugs" Then
                            oNode.Nodes.Add(drnode)
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(1).Nodes.Add(drnode)
                    '  associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                ElseIf dt.Rows(i).Item(2) = "p" Then
                    Dim penode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    penode.Checked = bl

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Patient Education" Then
                            oNode.Nodes.Add(penode)
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(2).Nodes.Add(penode)
                    ' associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                    Call Load_PatientEducation(associatenode)
                ElseIf dt.Rows(i).Item(2) = "t" Then

                    Dim indtempname As Integer = dt.Rows(i).Item(1).ToString().LastIndexOf("-[")
                    Dim tagnode As myTreeNode
                    If indtempname > -1 Then
                        Dim strtempname As String = ""
                        strtempname = dt.Rows(i).Item(1).ToString().Substring(0, indtempname)
                        tagnode = New myTreeNode(strtempname, dt.Rows(i).Item(0))
                        tagnode.Text = dt.Rows(i).Item(1)
                        tagnode.NodeName = strtempname
                    Else
                        tagnode = New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        tagnode.NodeName = dt.Rows(i).Item(1)
                    End If
                    '  Dim tagnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    tagnode.Checked = bl

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Tags" Then
                            oNode.Nodes.Add(tagnode)
                        End If
                    Next
                    'end swaraj'


                    'associatenode.Nodes.Item(3).Nodes.Add(tagnode)
                    ' associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))


                ElseIf dt.Rows(i).Item(2) = "f" Then
                    Dim flownode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    flownode.Checked = bl
                    flownode.Tag = dt.Rows(i).Item(0)

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Flowsheet" Then
                            oNode.Nodes.Add(flownode)
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(4).Nodes.Add(flownode)
                    ' associatenode.Nodes.Item(4).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                    'add Tags items to Tags node in icd9
                ElseIf dt.Rows(i).Item(2) = "l" Then
                    Dim labnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    labnode.Checked = bl
                    labnode.Tag = dt.Rows(i).Item(0)

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Orders and Results" Then
                            oNode.Nodes.Add(labnode)
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(5).Nodes.Add(labnode)
                    ' associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                ElseIf dt.Rows(i).Item(2) = "o" Then
                    Dim ordnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    ordnode.Checked = bl
                    ordnode.Tag = dt.Rows(i).Item(0)

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Order Templates" Then
                            oNode.Nodes.Add(ordnode)
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(6).Nodes.Add(ordnode)
                    'associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                    'add Tags items to Tags node in icd9
                ElseIf dt.Rows(i).Item(2) = "r" Then
                    Dim reffnode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                    reffnode.Checked = bl
                    reffnode.Tag = dt.Rows(i).Item(0)

                    'swaraj 29-03-2010 -- loading the tree node order sequentially '
                    For Each oNode As myTreeNode In associatenode.Nodes
                        If oNode.Text = "Referral Letter" Then
                            oNode.Nodes.Add(reffnode)
                        End If
                    Next
                    'end swaraj'

                    'associatenode.Nodes.Item(7).Nodes.Add(reffnode)

                End If
            Next



            'For i = 0 To dt.Rows.Count - 1

            '    'add cpt items to cpt node in icd9
            '    If dt.Rows(i).Item(2) = "i" Then
            '        associatenode.Nodes(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
            '        If gblnICD9Driven Then
            '            Load_Dignosis(associatenode)
            '        Else
            '            Load_CPTDrivenTreatment(associatenode)
            '        End If

            '        'add drug items to drug node in icd9
            '    ElseIf dt.Rows(i).Item(2) = "d" Then
            '        associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0), dt.Rows(i).Item(1), dt.Rows(i).Item(3), dt.Rows(i).Item(4), dt.Rows(i).Item(5), dt.Rows(i).Item(6), dt.Rows(i).Item(7), dt.Rows(i).Item(8), dt.Rows(i).Item(9), dt.Rows(i).Item(10), dt.Rows(i).Item(11)))


            '        'add PE items to PE node in icd9
            '    ElseIf dt.Rows(i).Item(2) = "p" Then
            '        associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
            '        Call Load_PatientEducation(associatenode)

            '        'add Tags items to Tags node in icd9
            '    ElseIf dt.Rows(i).Item(2) = "t" Then
            '        associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
            '    End If

            'Next
        End If
        trvCPTAssociation.ExpandAll()
        trvCPTAssociation.Select()

        'treeindex = -1
        'End If

        'Ensure the newly created node is visible to the user and select it
        associatenode.EnsureVisible()
        trvCPTAssociation.SelectedNode = associatenode
        'If IsFormLoading = False Then
        FillCPT_trv(associatenode)
        'End If
        '' To refresh the txtDrugs 
        Call RefreshSearch()
        'treeindex = mynode.Index

        ' If IsFormload = True Then


        ' End If


        CheckAllParentNodes()
        'End If
    End Sub





    'Public Sub FillReferrals()

    '    Dim reff As TreeNode


    '    For Each ptn As TreeNode In trvCPTAssociation.Nodes
    '        For Each childptn As TreeNode In ptn.Nodes
    '            For Each innerchildptn As TreeNode In childptn.Nodes
    '                If innerchildptn.Text = "Referral Letter" Then
    '                    reff = innerchildptn
    '                    checkreffnode(reff)
    '                    Exit For
    '                End If
    '            Next
    '        Next
    '    Next


    'End Sub
    'Public Sub FillFlowSheet()

    '    Dim flsht2 As TreeNode


    '    For Each ptn As TreeNode In trvCPTAssociation.Nodes
    '        For Each childptn As TreeNode In ptn.Nodes
    '            For Each innerchildptn As TreeNode In childptn.Nodes
    '                If innerchildptn.Text = "FlowSheet" Then
    '                    flsht2 = innerchildptn
    '                    checkflosheetnode(flsht2)
    '                    Exit For
    '                End If
    '            Next
    '        Next
    '    Next


    'End Sub

    'Public Sub FillOrders()
    '    Dim ordernode As TreeNode


    '    For Each ptn As TreeNode In trvCPTAssociation.Nodes
    '        For Each childptn As TreeNode In ptn.Nodes
    '            For Each innerchildptn As TreeNode In childptn.Nodes
    '                If innerchildptn.Text = "Order Templates" Then
    '                    ordernode = innerchildptn
    '                    checkordernode(ordernode)
    '                    Exit For
    '                End If
    '            Next
    '        Next
    '    Next



    'End Sub


    Public Sub FillNodes() ' it checks the nodes to which patient visited and saved 
        Dim flsht2 As TreeNode
        Dim lbordernode As TreeNode
        Dim ordernode As TreeNode
        Dim reff As TreeNode
        Dim PtEdu As TreeNode
        Dim drg As TreeNode

        For Each ptn As TreeNode In trvCPTAssociation.Nodes 'Check In Top Most Parent Nodes
            For Each childptn As TreeNode In ptn.Nodes   'Check In Top Most Parent's  Child Nodes
                For Each innerchildptn As TreeNode In childptn.Nodes ''Check In Top Most Parent Child's Child Nodes

                    If innerchildptn.Text = "FlowSheet" Then
                        flsht2 = innerchildptn
                        checkflosheetnode(flsht2)
                    ElseIf innerchildptn.Text = "Orders and Results" Then
                        lbordernode = innerchildptn
                        checklbordernode(lbordernode)
                        'Exit For
                    ElseIf innerchildptn.Text = "Order Templates" Then
                        ordernode = innerchildptn
                        checkordernode(ordernode)

                    ElseIf innerchildptn.Text = "Referral Letter" Then
                        reff = innerchildptn
                        checkreffnode(reff)
                    ElseIf innerchildptn.Text = "Patient Education" Then
                        PtEdu = innerchildptn
                        checkPatientEducationnode(PtEdu)
                    ElseIf innerchildptn.Text = "Drugs" Then
                        drg = innerchildptn
                        checkDrugsnode(drg)

                    End If
                Next
            Next
        Next



    End Sub
    Private Sub checkDrugsnode(ByVal drgnode As TreeNode)


        'Dim objPrescriptions As gloEMRGeneralLibrary.gloEMRActors.Prescriptions
        'objPrescriptions = objclsgloEmrPrescription.FetchPrescriptionforView(m_VisitID, Date.Now)
        'Dim intflag As Short = 0

        Dim dtDrug As New DataTable

        Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
            dtDrug = helper.GetPrescriptionsByPatient(_PatientID, Now.Date, m_VisitID)
        End Using
        '  dtDrug = objclsgloEmrPrescription.FetchPrescriptionforView(m_VisitID)
        'dtDrug = objclsgloEmrPrescription.FetchPrescriptionforView(m_VisitID, Date.Now)
        'dtDrug = objclsgloEmrPrescription.FetchPrescriptionforView(m_VisitID, Date.Now)
        'dtDrug = objclsgloEmrPrescription.FetchPrescriptionforUpdate(Date.Now, intflag)
        'dtDrug = objclsgloEmrPrescription.FetchPrescriptionforUpdate(m_VisitID)

        For Each drugnode As TreeNode In drgnode.Nodes
            drugnode.Checked = False

            Dim i As Int16
            For i = 0 To dtDrug.Rows.Count - 1
                If drugnode.Text.Trim() = dtDrug.Rows(i)(4).ToString().Trim() Then
                    '' If ICD9COde-Discription Mathches with TreeNode then 
                    '' then add that ICD9 to associated treeview
                    drugnode.Checked = True
                    Exit For
                End If
            Next

        Next
    End Sub

    ''new function
    Private Sub checkflosheetnode(ByVal flsht2 As TreeNode)
        Dim dtFlowsheet As DataTable

        dtFlowsheet = objTreatmentDBLayer.FetchFlowsheetFromFlowsheet(m_VisitID)

        If Not IsNothing(dtFlowsheet) Then

            ''If dtFlowsheet.Rows.Count > 0 Then
            For Each flshtnode As TreeNode In flsht2.Nodes
                flshtnode.Checked = False
                Dim i As Int16

                For i = 0 To dtFlowsheet.Rows.Count - 1
                    If flshtnode.Text = dtFlowsheet.Rows(i)(0).ToString() Then
                        '' If ICD9COde-Discription Mathches with TreeNode then 
                        '' then add that ICD9 to associated treeview
                        flshtnode.Checked = True
                        Exit For
                    End If
                Next
            Next
            ''End If
        End If
    End Sub
    'Private Sub checkPatientEducationnode(ByVal edu As TreeNode)
    '    Dim dtedu As DataTable
    '    dtedu = objclsPatientEducation.GetSelectedExamEducation(_PatientID, m_VisitID)
    '    Dim arrEducationName() As String
    '    For Each edunode As TreeNode In edu.Nodes
    '        edunode.Checked = False
    '        Dim i As Int16
    '        If Not IsNothing(dtedu) Then
    '            For i = 0 To dtedu.Rows.Count - 1
    '                arrEducationName = Split(dtedu.Rows(i)(1).ToString, ",")
    '                If arrEducationName.Length > 0 Then
    '                    For j As Integer = 0 To arrEducationName.Length - 1
    '                        If edunode.Text = arrEducationName.GetValue(j).ToString().Trim() Then
    '                            edunode.Checked = True
    '                            Exit For
    '                        End If
    '                    Next
    '                End If
    '            Next
    '        End If

    '    Next
    'End Sub


    Private Sub checkPatientEducationnode(ByVal edu As TreeNode)
        Dim arrPE As ArrayList
        arrPE = objclsPatientEducation.GetPatientEductionArray(_PatientID, m_ExamID, m_VisitID)
        If Not IsNothing(arrPE) Then
            For Each edunode As TreeNode In edu.Nodes
                edunode.Checked = False
                For i As Integer = 0 To arrPE.Count - 1
                    If edunode.Text = CType(arrPE.Item(i), myList).Description.ToString.Trim() Then
                        edunode.Checked = True
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub

    ''new function 
    Private Sub checkordernode(ByVal ordnode As TreeNode)
        Dim dtOrder As DataTable
        dtOrder = objTreatmentDBLayer.FetchOrder(m_VisitID)

        If Not IsNothing(dtOrder) Then
            ''If dtOrder.Rows.Count > 0 Then
            For Each radiologynode As TreeNode In ordnode.Nodes
                radiologynode.Checked = False
                Dim i As Int16
                For i = 0 To dtOrder.Rows.Count - 1
                    If radiologynode.Text = dtOrder.Rows(i)(0).ToString() Then
                        '' If ICD9COde-Discription Mathches with TreeNode then 
                        '' then add that ICD9 to associated treeview
                        radiologynode.Checked = True
                        Exit For
                    End If
                Next

            Next
            ''End If
        End If
    End Sub

    Private Sub checklbordernode(ByVal lbordnode As TreeNode)
        Dim dtlabord As DataTable
        dtlabord = objTreatmentDBLayer.FetchLaborderName(m_VisitID)


        For Each labordnode As TreeNode In lbordnode.Nodes
            labordnode.Checked = False
            Dim i As Int16
            For i = 0 To dtlabord.Rows.Count - 1
                If labordnode.Text = dtlabord.Rows(i)(0).ToString() Then
                    '' If ICD9COde-Discription Mathches with TreeNode then 
                    '' then add that ICD9 to associated treeview
                    labordnode.Checked = True
                    Exit For
                End If
            Next

        Next
    End Sub
    Private Sub checkreffnode(ByVal reff As TreeNode)
        Dim dtreff As DataTable
        dtreff = objTreatmentDBLayer.FetchReferralNameFromReferral(m_VisitID)
        If Not IsNothing(dtreff) Then
            ''If dtreff.Rows.Count > 0 Then
            For Each reffnode As TreeNode In reff.Nodes
                reffnode.Checked = False
                Dim i As Int16
                For i = 0 To dtreff.Rows.Count - 1
                    If reffnode.Text = dtreff.Rows(i)(0).ToString() Then
                        '' If ICD9COde-Discription Mathches with TreeNode then 
                        '' then add that ICD9 to associated treeview
                        reffnode.Checked = True
                        Exit For
                    End If
                Next

            Next
            ''End If
        End If
    End Sub


    Private Sub CheckAllParentNodes()
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

    End Sub


    Public Sub FillCPT_trv(ByVal oNode As myTreeNode)
        With C1Dignosis
            'Code Start-Added by kanchan on 20101130 for bug : 6274
            .Tree.Column = Col_ICD9Code_Description
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Solid
            .Tree.Indent = 15
            'Code End-Added by kanchan on 20101130 for bug : 6274
            Dim arrstrConctCPT() As String
            Dim arrstrConctICD9() As String
            Dim nMaxICD9Count As Integer
            Dim NewRow As Integer = 0
            Dim isFound As Boolean = False

            For Each oICD9 As myTreeNode In oNode.Nodes
                If oICD9.Text = "ICD" Then
                    For Each ochildICD9 As myTreeNode In oICD9.Nodes
                        isFound = False
                        If ochildICD9.Checked = True Then
                            For i As Integer = 0 To C1Dignosis.Rows.Count - 1
                                If .GetData(i, Col_ICD9Code_Description) = ochildICD9.Text Then
                                    Dim oFnode As C1.Win.C1FlexGrid.Node
                                    oFnode = .Rows(i).Node
                                    Dim LastIndex As Integer
                                    If oFnode.Children > 0 Then
                                        LastIndex = oFnode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1
                                    Else
                                        LastIndex = oFnode.Row.Index + 1
                                    End If

                                    .Rows.Insert(LastIndex)
                                    With .Rows(LastIndex)
                                        .AllowEditing = False
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                        ' .Node.Data = trvCPT.SelectedNode.Text ''Sandiip Darade
                                        .Node.Data = oNode.Text ''Sandiip Darade
                                    End With

                                    arrstrConctCPT = Split(oNode.Text, "-", 2)
                                    arrstrConctICD9 = Split(ochildICD9.Text, "-", 2)


                                    .SetData(LastIndex, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                    .SetData(LastIndex, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                    .SetData(LastIndex, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                    .SetData(LastIndex, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                    .SetData(LastIndex, Col_ICD9Count, .GetData(i, Col_ICD9Count))
                                    .SetData(LastIndex, Col_CPTCount, .GetData(i, Col_CPTCount) + 1)

                                    .SetData(LastIndex, Col_ICDRevision, ochildICD9.nICDRevision)
                                    isFound = True
                                    Exit For

                                End If
                            Next
                            If isFound = False Then

                                If .Rows.Count - 1 > 0 Then
                                    nMaxICD9Count = .GetData(.Rows.Count - 1, Col_ICD9Count)
                                Else
                                    nMaxICD9Count = 0
                                End If
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                With .Rows(NewRow)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.icd9
                                    .Node.Data = ochildICD9.Text

                                End With

                                arrstrConctICD9 = Split(ochildICD9.Text, "-", 2)
                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                .SetData(NewRow, Col_ICD9Count, nMaxICD9Count + 1)
                                .SetData(NewRow, Col_ICDRevision, ochildICD9.nICDRevision)
                                nMaxICD9Count = .GetData(.Rows.Count - 1, Col_ICD9Count)
                                .Rows.Add()
                                NewRow = .Rows.Count - 1
                                With .Rows(NewRow)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 1
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                    .Node.Data = oNode.Text
                                End With

                                arrstrConctCPT = Split(oNode.Text, "-", 2)
                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                .SetData(NewRow, Col_ICDRevision, ochildICD9.nICDRevision)
                                .SetData(NewRow, Col_ICD9Count, nMaxICD9Count)
                                .SetData(NewRow, Col_CPTCount, 1)
                            End If
                        End If
                    Next
                End If
            Next
        End With
    End Sub

    Private Sub txtsearchCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchCPT.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvCPT.Select()
        Else
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
        End If
    End Sub

    Private Sub txtsearchCPT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsearchCPT.Validating
        'Try
        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
        '        'compare selected node text and entered text
        '        Dim str As String
        '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchCPT.Text))))
        '        If str = UCase(Trim(txtsearchCPT.Text)) Then
        '            trvCPT.SelectedNode = mychildnode
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvCPT.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trvCPT.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'Private Sub tblSmartDiagnosis_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&Save"
    '                Call SaveAssociation()
    '                Me.Close()
    '            Case "&Close"
    '                Me.Close()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub frmSmartDiagnosis_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.LocationChanged

    End Sub

    Private Sub rbSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSearch1.Click
        Try
            txtsearchCPT.Text = ""

            If IsNothing(dtOrderbyCode) = True Then
                dtOrderbyCode = New DataTable
            End If

            If dtOrderbyCode.Rows.Count = 0 Then
                dtOrderbyCode = oclsSmartTreatment.FillAssociatedCPT(0)
            End If
            trvCPT.Hide()
            trvCPT.Nodes(0).Nodes.Clear()
            Dim i As Int16
            'Populate ICD9 Data
            For i = 0 To dtOrderbyCode.Rows.Count - 1
                Dim mychildnode As myTreeNode
                ''mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0) , CType(dt.Rows(i)(2), String))
                mychildnode = New myTreeNode(dtOrderbyCode.Rows(i)(1), dtOrderbyCode.Rows(i)(0))
                trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
            Next
            trvCPT.ExpandAll()
            trvCPT.Show()

            trvCPT.Select()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub rbSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSearch2.Click
        Try
            txtsearchCPT.Text = ""

            If IsNothing(dtOrderbyDesc) = True Then
                dtOrderbyDesc = New DataTable
            End If

            If dtOrderbyDesc.Rows.Count = 0 Then
                dtOrderbyDesc = oclsSmartTreatment.FillAssociatedCPT(1)
            End If
            Dim i As Int16

            trvCPT.Hide()
            trvCPT.Nodes(0).Nodes.Clear()

            'Populate ICD9 Data
            For i = 0 To dtOrderbyDesc.Rows.Count - 1
                Dim mychildnode As myTreeNode
                ''mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0) , CType(dt.Rows(i)(2), String))
                mychildnode = New myTreeNode(dtOrderbyDesc.Rows(i)(1), dtOrderbyDesc.Rows(i)(0))
                trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
            Next
            trvCPT.ExpandAll()
            trvCPT.Show()
            trvCPT.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub RefreshSearch()
        txtsearchCPT.Text = ""
        txtsearchCPT.Focus()
    End Sub

    Private Sub cntTags_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles cntTags.Popup

    End Sub

    Private Sub trvCPT_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCPT.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                Dim trvNode As TreeNode
                trvNode = trvCPT.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trvCPT.SelectedNode = trvNode
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvCPT_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCPT.AfterSelect

    End Sub

    Private Sub frmSmartTreatmentNew_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            ' gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Other, "Smart Treatment Closed", gstrLoginName, gstrClientMachineName, _PatientID)

            ''  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Close, "Smart Treatment Closed", gloAuditTrail.ActivityOutCome.Success)
            ''Added vijay P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Close, "Smart Treatment Closed", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblSmartTreatment_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblSmartTreatment.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    Call SaveAssociation()

                Case "Close"
                    frmPatientExam.nRefTempID = 0
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '*******************Ojeswini03072009*********************************
    'For Radio button Bold and Regular font.

    Private Sub rbSearch1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearch1.CheckedChanged
        If rbSearch1.Checked = True Then
            rbSearch1.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSearch1.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbSearch2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearch2.CheckedChanged
        If rbSearch2.Checked = True Then
            rbSearch2.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSearch2.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    '*******************Ojeswini03072009*********************************

    Private Sub trvCPTAssociation_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvCPTAssociation.BeforeCheck
        If IsFormLoading = False And IsTreeviewDoubleClick = False Then

            Dim ofNode As C1.Win.C1FlexGrid.Node
            If Not IsNothing(e.Node.Parent) Then
                If e.Node.Parent.Text = "ICD" Then
                    If e.Node.Checked = True Then
                        With C1Dignosis
                            For i As Integer = 0 To .Rows.Count - 1
                                If .GetData(i, Col_ICD9Code_Description) = e.Node.Text Then
                                    ofNode = Nothing
                                    ofNode = .Rows(i).Node
                                    If ofNode.Children > 1 Then
                                        If MessageBox.Show("This ICD9 has other assoicated CPT(s),do you want to delete?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                            ofNode.RemoveNode()
                                            RearrangeICD9COunt()
                                            Exit Sub
                                        Else
                                            e.Cancel = True
                                            Exit Sub
                                        End If
                                    Else
                                        ofNode.RemoveNode()
                                        RearrangeICD9COunt()
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End With
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub RearrangeICD9COunt()
        Dim oNode As C1.Win.C1FlexGrid.Node
        Dim cnt As Integer = 0
        With C1Dignosis
            For i As Integer = 1 To .Rows.Count - 1
                oNode = .Rows(i).Node
                If oNode.Level = 0 Then
                    cnt = cnt + 1
                End If
                .SetData(i, Col_ICD9Count, cnt)
            Next
        End With
    End Sub

    Private Sub trvCPTAssociation_BackColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvCPTAssociation.BackColorChanged

    End Sub

    Private Sub trvCPTAssociation_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvCPTAssociation.BeforeCollapse

    End Sub

    ''Sandip Darade 20090703
    ''tree view control added
#Region "Tree view control implementation"
    Private Sub GloUC_trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvCPT.KeyPress

    End Sub

    Private Sub GloUC_trvCPT_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvCPT.NodeMouseDoubleClick
        Try
            IsTreeviewDoubleClick = True
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)

            If IsNothing(oNode) = False Then
                Dim mynode As New myTreeNode
                mynode.Text = oNode.Text
                mynode.Key = oNode.ID
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            IsTreeviewDoubleClick = False
        End Try
    End Sub
#End Region

#Region " CPT Driven Load / Save "
    Private Sub Load_CPTDrivenDiagnosis()
        Try
            Dim oDiagnosis As New ClsDiagnosisDBLayer
            arrCPTDrivenTreatment = oDiagnosis.GetCPTDrivenDiagnosis(m_ExamID, m_VisitID, _PatientID)
            If arrCPTDrivenTreatment IsNot Nothing Then
                If arrCPTDrivenTreatment.Count > 0 Then

                    '' FETCH FOR ALREADY SAVED ICD9s, AND STORE IT IN ARRAYLIST ''
                    Dim arrAvailableCPTs As New ArrayList
                    'Dim _CPT As String
                    Dim oCPT As gloEMRGeneralLibrary.gloGeneral.myList
                    For i As Integer = 0 To arrCPTDrivenTreatment.Count - 1
                        oCPT = CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList)
                        If oCPT.HistoryCategory <> "" And arrAvailableCPTs.Contains(oCPT.HistoryCategory & "-" & oCPT.HistoryItem) = False Then
                            arrAvailableCPTs.Add(oCPT.HistoryCategory & "-" & oCPT.HistoryItem)
                        End If
                    Next

                    '' NOW SEARCH WHETHER STORED ICD9s ARE PRESENT IN TEMPLATES OR NOT ''
                    '' IF TEMPLATE FOUND ADD IT ''
                    For Each oCPTNode As TreeNode In GloUC_trvCPT.Nodes
                        If arrAvailableCPTs.Contains(oCPTNode.Text) Then

                            GloUC_trvCPT.SelectedNode = oCPTNode
                            GloUC_trvCPT_NodeMouseDoubleClick(Nothing, Nothing)
                            ' trvCPTAssociation.SelectedNode.Checked = True

                            Load_CPTDrivenTreatment(trvCPTAssociation.SelectedNode)
                            Load_Drugs(trvCPTAssociation.SelectedNode)
                            Load_PatientEducation(trvCPTAssociation.SelectedNode)

                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Load_CPTDrivenTreatment(ByVal oCPTNode As myTreeNode)
        Try
            For Each oNode As TreeNode In oCPTNode.Nodes
                If oNode.Text = "ICD" Then
                    Dim _CPTCode, _ICD9Code, _str As String
                    For Each oICD9Node As TreeNode In oNode.Nodes
                        _str = ""
                        _str = oICD9Node.Text
                        _ICD9Code = _str.Substring(0, _str.IndexOf("-"))

                        _str = ""
                        _str = oICD9Node.Parent.Parent.Text
                        _CPTCode = _str.Substring(0, _str.IndexOf("-"))

                        If isICD9CPT_Associated(_ICD9Code, _CPTCode) = True Then
                            oICD9Node.Checked = True
                        End If
                    Next
                End If
            Next
        Catch
        End Try
    End Sub

    Private Function isICD9CPT_Associated(ByVal sICD9Code As String, ByVal sCPTCode As String) As Boolean
        Try
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = 0 To arrCPTDrivenTreatment.Count - 1
                oList = CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList)

                If oList.Code.Trim() = sICD9Code.Trim() And oList.HistoryCategory.Trim() = sCPTCode.Trim() Then
                    Return True
                End If
            Next
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub SaveCPTDrivenDiagnosis()
        Try
            Dim oDiagnosis As New ClsDiagnosisDBLayer()
            If IsNothing(arrCPTDrivenTreatment) Then
                arrCPTDrivenTreatment = New ArrayList
            End If
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            Dim _CPTLineNumber As Integer = -1
            Dim _CPTText, _CPTCode, _CPTDescription, _ICD9Text, _ICD9Code, _ICD9Description, _ICDRevision, strSnomedCode, strSnomedDesc As String
            strSnomedCode = ""
            strSnomedDesc = ""
            For Each oCPTNode As myTreeNode In trvCPTAssociation.Nodes(0).Nodes

                ' If oCPTNode.Checked Then
                _CPTText = CType(oCPTNode, myTreeNode).Text
                _ICDRevision = CType(oCPTNode, myTreeNode).nICDRevision
                _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)

                _CPTLineNumber = -1
                _CPTLineNumber = GetCPTLineNumber(_CPTCode)

                If _CPTLineNumber = -1 Then '' IF CPT NOT PRESENT OR ICD9 OVERFLOW THEN CREATE NEW CPT LINE ''
                    _CPTLineNumber = GetNewCPTLineNumber()
                    '' ADD CPT ''
                    oList = New gloEMRGeneralLibrary.gloGeneral.myList
                    oList.Code = ""
                    oList.Description = ""
                    oList.HistoryCategory = _CPTCode
                    oList.HistoryItem = _CPTDescription
                    oList.Value = ""
                    oList.ParameterName = ""
                    oList.TemplateResult = "1"
                    oList.ICD9No = _CPTLineNumber
                    oList.nICDRevision = _ICDRevision

                    arrCPTDrivenTreatment.Add(oList)
                End If

                'else
                '_CPTCode = ""
                '_CPTDescription = ""
                ' End If
                Dim blnOneSnoMed As Boolean
                For Each oParent As myTreeNode In oCPTNode.Nodes

                    'If oParent.NodeName = "ICD9" Then
                    If oParent.Text = "ICD" Then
                        For Each oICD9Node As myTreeNode In oParent.Nodes

                            If oICD9Node.Checked Then
                                '' ADD CPT ''
                                _ICD9Text = oICD9Node.Text
                                _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-"))
                                _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1)
                                _ICDRevision = oICD9Node.nICDRevision


                                'Dim htSno As New Hashtable()
                                ''14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                                'htSno = oDiagnosis.GetDefaultSnomed(_ICD9Code, _ICD9Description, _ICDRevision, blnOneSnoMed, Me)

                                'If Not IsNothing(htSno) Then

                                '    Dim key As ICollection = htSno.Keys
                                '    Dim k As String

                                '    For Each k In key

                                '        strSnomedCode = k
                                '        strSnomedDesc = htSno(k)
                                '    Next k

                                'End If

                                'htSno = Nothing
                                '' FIND IF CURRENT NODE ALREADY PRESENT IN SAVED DIAGNOSIS OR NOT ''
                                '' IF CPT IS NEW THEN ADD IT IN ARRAYLIST ''
                                If isICD9CPT_Associated(_ICD9Code, _CPTCode) = False Then

                                    _CPTLineNumber = -1
                                    _CPTLineNumber = GetCPTLineNumber(_CPTCode)

                                    If _CPTLineNumber = -1 Then '' IF CPT NOT PRESENT OR ICD9 OVERFLOW THEN CREATE NEW CPT LINE ''
                                        _CPTLineNumber = GetNewCPTLineNumber()
                                        '' ADD CPT ''
                                        oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                        oList.Code = ""
                                        oList.Description = ""
                                        oList.HistoryCategory = _CPTCode
                                        oList.HistoryItem = _CPTDescription
                                        oList.Value = ""
                                        oList.ParameterName = ""
                                        oList.TemplateResult = "1"
                                        oList.ICD9No = _CPTLineNumber
                                        oList.nICDRevision = _ICDRevision
                                        arrCPTDrivenTreatment.Add(oList)

                                        '' ADD ICD9 ''
                                        If _ICD9Code <> "" Then
                                            oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                            oList.Code = _ICD9Code
                                            oList.Description = _ICD9Description
                                            oList.HistoryCategory = _CPTCode
                                            oList.HistoryItem = _CPTDescription
                                            oList.Value = ""
                                            oList.ParameterName = ""
                                            oList.TemplateResult = "1"
                                            oList.ICD9No = _CPTLineNumber
                                            oList.nICDRevision = _ICDRevision
                                            oList.SnomedID = strSnomedCode
                                            oList.SnomedDesc = strSnomedDesc
                                            oList.SnoMedOneToOneMapping = blnOneSnoMed
                                            arrCPTDrivenTreatment.Add(oList)
                                        End If


                                    Else '' IF CPT LINE FOUND ADD ICD9 TO IT ''
                                        '' ADD ICD9 ''
                                        oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                        oList.Code = _ICD9Code
                                        oList.Description = _ICD9Description
                                        oList.HistoryCategory = _CPTCode
                                        oList.HistoryItem = _CPTDescription
                                        oList.Value = ""
                                        oList.ParameterName = ""
                                        oList.TemplateResult = "1"
                                        oList.ICD9No = _CPTLineNumber
                                        oList.nICDRevision = _ICDRevision
                                        oList.SnomedID = strSnomedCode
                                        oList.SnomedDesc = strSnomedDesc
                                        oList.SnoMedOneToOneMapping = blnOneSnoMed
                                        arrCPTDrivenTreatment.Add(oList)
                                    End If

                                End If

                            End If

                        Next
                    End If

                Next

            Next

            If arrCPTDrivenTreatment IsNot Nothing Then
                If arrCPTDrivenTreatment.Count > 0 Then
                    oDiagnosis.SaveDiagTreatmentAssociation(m_ExamID, _PatientID, m_VisitID, arrCPTDrivenTreatment, Me, True)
                    oDiagnosis = Nothing
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetCPTLineNumber(ByVal sCPTCode As String) As Integer
        Try
            Dim _DxCount As Integer = -1
            Dim _CPTLineNumber As Integer = -1
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = arrCPTDrivenTreatment.Count - 1 To 0 Step -1
                oList = CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList.HistoryCategory = sCPTCode And oList.Value = "" Then
                    _DxCount = _DxCount + 1
                    _CPTLineNumber = oList.ICD9No
                End If
            Next

            '' IF ICD9 WILL OVERFLOW THEN DON'T SEND LINE NUMBER AND LET NEW CPT LINE CREATE ''
            If _DxCount >= 8 Then
                Return -1
            Else
                Return _CPTLineNumber
            End If
        Catch
            Return -1
        End Try
    End Function

    Private Function GetNewCPTLineNumber() As Integer
        Try
            Dim _NewNumber As Integer = -1
            For i As Integer = 0 To arrCPTDrivenTreatment.Count - 1
                If CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No > _NewNumber Then
                    _NewNumber = CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No
                End If
            Next
            If _NewNumber = -1 Then
                Return 1
            Else
                Return _NewNumber + 1
            End If
        Catch
            Return -1
        End Try
    End Function

    Private Sub RemoveCPTICD9(ByVal sCPTCode As String, ByVal sCPTDescription As String, ByVal sICD9Code As String, ByVal sICD9Description As String)
        Try

            Dim _ICD9Count As Integer = -1
            Dim _CPTLineNumbers As New ArrayList
            For i As Integer = 0 To arrCPTDrivenTreatment.Count - 1
                If CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory = sCPTCode Then
                    _ICD9Count = _ICD9Count + 1
                    If _CPTLineNumbers.Contains(CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No) = False Then
                        _CPTLineNumbers.Add(CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No)
                    End If
                End If
            Next

            '' IF NO OTHER ICD9 ASSOCIATED TO THIS CPT '' THEN  REMOVE WHOLE LINE ''
            If _ICD9Count = 1 Then
                For iLine As Integer = 0 To _CPTLineNumbers.Count - 1
                    For i As Integer = arrCPTDrivenTreatment.Count - 1 To 0 Step -1
                        If CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No = _CPTLineNumbers(iLine) Then
                            arrCPTDrivenTreatment.RemoveAt(i)
                        End If
                    Next
                Next

            Else
                '' IF ANY OTHER ICD9 ASSOCIATED WITH THIS CPT '' THEN REMOVE ICD9 FROM CPT LINE ''
                For i As Integer = 0 To arrCPTDrivenTreatment.Count - 1
                    For iLine As Integer = 0 To _CPTLineNumbers.Count - 1
                        If CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No = _CPTLineNumbers(iLine) And _
                        CType(arrCPTDrivenTreatment(i), gloEMRGeneralLibrary.gloGeneral.myList).Code = sICD9Code Then
                            arrCPTDrivenTreatment.RemoveAt(i)
                            Exit Sub
                        End If
                    Next
                Next
            End If

        Catch
        End Try
    End Sub
#End Region

    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True
    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        bParentTrigger = False
        For Each ctn As TreeNode In tn.Nodes
            bChildTrigger = False
            ctn.Checked = bCheck
            bChildTrigger = True

            CheckAllChildren(ctn, bCheck)
        Next
        bParentTrigger = True
    End Sub

    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
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
    End Sub
    '' function added by chetan on 18 feb 2010 for editing that node
    Private Sub mnuEditICD9Item_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditICD9Item.Click
        Try
            ''''''''''''
            Dim CPTICD9Code As String()
            CPTICD9Code = trvCPTAssociation.SelectedNode.Text.Split("-")
            ''''''''''''
            Dim frm As New frmCPTICD9Association
            With frm
                frmCPTICD9Association.ISCPTICD9AssocOpen = True
                frmCPTICD9Association.CPTICD9SelNodeKey = CType(trvCPTAssociation.SelectedNode, myTreeNode).Key
                frmCPTICD9Association.CPTICD9SelCode = CPTICD9Code(0).ToString.Trim()
                .WindowState = FormWindowState.Normal
                .Owner = Me
                '.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                .ShowDialog(frm.parent)

                Dim arrSelectedNode As New ArrayList
                For Each myReqNode As TreeNode In trvCPTAssociation.Nodes.Item(0).Nodes
                    If myReqNode.Nodes.Count > 0 Then
                        myReqNode.Nodes.Clear()
                    End If
                    arrSelectedNode.Add(CType(myReqNode, myTreeNode))
                Next

                If arrSelectedNode.Count > 0 Then
                    trvCPTAssociation.Nodes.Item(0).Nodes.Clear()
                    For i As Integer = 0 To arrSelectedNode.Count - 1
                        AddNode(CType(arrSelectedNode.Item(i), myTreeNode))
                    Next
                End If
                'Resolved bug no.77257::Smart Treatment- Applicaton should set focus on edited CPT after edit
                For Each mySelNode As TreeNode In trvCPTAssociation.Nodes.Item(0).Nodes
                    Dim CPTICD9Code1 As String()
                    CPTICD9Code1 = mySelNode.Text.Split("-")
                    If CPTICD9Code(0) = CPTICD9Code1(0) Then
                        mySelNode.EnsureVisible()
                        trvCPTAssociation.SelectedNode = mySelNode
                        Exit For
                    End If
                Next
                '


            End With
            frm.Dispose()
            frm = Nothing
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''Sanjog
    Private Sub gloLabSettings(ByVal _TaskType As String, Optional ByVal _TestNames As String = "", Optional ByVal _arrLabs As ArrayList = Nothing)

        Select Case _TaskType.ToString().ToUpper()
            Case "TASK"
                Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
                Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
                Dim objpatient As New gloPatient.Patient()
                Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
                objpatient = objgloPatient.GetPatient(_PatientID)

                Dim _LoginProviderId As Int64 = 0
                'Developer: Sanjog(Dhamke)
                'Date:14 Dec 2011
                'Bug ID/PRD Name/Sales force Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                'Reason: To show task info
                Dim strLabTest As String = ""
                Dim strLabTests As String = ""
                If Not IsNothing(_arrLabs) Then
                    For i As Integer = 0 To _arrLabs.Count - 1
                        strLabTest = CType(_arrLabs.Item(i), gloEmdeonCommon.myList).ID & "~" & CType(_arrLabs.Item(i), gloEmdeonCommon.myList).Value
                        If i = 0 Then
                            strLabTests = strLabTest
                        Else
                            strLabTests = strLabTests & "|" & strLabTest
                        End If
                    Next
                End If
                _LoginProviderId = GetProviderIDForUser(gnLoginID)
                objClsGeneral.TestList = _TestNames
                objClsGeneral.TestlistOnly = strLabTests
                Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(_PatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId, m_ExamID, gloTaskMail.TaskType.PlaceLabOrder)
                If nTaskID > 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", _PatientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

                _LoginProviderId = 0
            Case "LABORDER"

                'If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                '    Return
                'End If

                gloLabOrderScreen(_arrLabs)  '' added to show testnames on EmdeonScreen ,v8022

            Case "RECORDRESULTS"
                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(_PatientID)
                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.ArrLabs = _arrLabs '' Added by Abhijeet on 20100624
                frmNormalLab.WindowState = FormWindowState.Maximized
                frmNormalLab.ShowInTaskbar = False
                frmNormalLab.BringToFront()
                frmNormalLab.ShowDialog(IIf(IsNothing(frmNormalLab.Parent), Me, frmNormalLab.Parent))
                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.Dispose()
            Case Else
                MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Select
        End Select
    End Sub

    Private Sub gloLabOrderScreen(Optional ByVal _arrLabs As ArrayList = Nothing)  ''_arrLabs  added to show testnames on EmdeonScreen ,v8022


        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim objclsgeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim _LoginUserProviderID As Long = 0
        Dim _PatientProviderID As Long = 0


        Dim loopcnt As Int16 = 0
        Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
        Dim objpatient As New gloPatient.Patient()
        Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)

        objpatient = objgloPatient.GetPatient(_PatientID)
        ' _LoginUserProviderID = GetProviderIDForUser(_LoginUserID)
        _LoginUserProviderID = GetProviderIDForUser(gnLoginID)
        _PatientProviderID = objpatient.DemographicsDetail.PatientProviderID

        If Not gloEmdeonInterface.Classes.clsEmdeonGeneral.CheckConnectionParameters(GetConnectionString) Then
            MessageBox.Show("Lab Settings have not been configured in gloEMR Admin." + vbCrLf + "Please complete Lab Settings before ordering.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim LabConnectionAvailable As gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity
        LabConnectionAvailable = objclsgeneral.IsInternetConnectionAvailable()
        If LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.Success Then

            If Not compareProvider(_PatientProviderID, _LoginUserProviderID) Then
                Return
            End If

            Dim _billingStatus As Boolean = False

            Dim objGloPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
            _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient)

            If _billingStatus = True Then
                If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                    Dim frmLabDemo As New gloEmdeonInterface.Forms.frmLabDemonstration(_PatientID)
                    frmLabDemo.WindowState = FormWindowState.Maximized
                    frmLabDemo.BringToFront()
                    frmLabDemo.ShowDialog(IIf(IsNothing(frmLabDemo.Parent), Me, frmLabDemo.Parent))
                    frmLabDemo.Dispose()
                Else
                    Dim strQry As String = String.Empty
                    Dim boolPatientReg As [Boolean] = False
                    If ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()) Then
                        strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID where PatientExternalCodes.sExternalType = 'EMDEON' AND  Patient.sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                    End If
                    oDB.Connect(False)

                    For loopcnt = 1 To 3

                        Dim cnt As Int32 = 0
                        cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry))
                        If cnt < 1 Then
                            ' if cnt is greater than zero means patient registered

                            Application.DoEvents()

                            gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = _PatientID

                            boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, GetConnectionString)

                            If boolPatientReg Then
                                Exit For
                            End If
                        Else
                            boolPatientReg = True
                            Exit For
                        End If
                    Next

                    If boolPatientReg = True Then

                        Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(_PatientID)
                        objfrmEmdonInterface.LoginProviderID = gnLoginProviderID
                        Dim strLabTestName As String = ""  '' added to show testnames on EmdeonScreen ,v8022
                        If Not IsNothing(_arrLabs) Then
                            For i As Int32 = 0 To _arrLabs.Count - 1
                                If i = 0 Then
                                    strLabTestName = CType(_arrLabs.Item(i), gloEmdeonCommon.myList).Value
                                Else
                                    strLabTestName = strLabTestName & "~" & CType(_arrLabs.Item(i), gloEmdeonCommon.myList).Value
                                End If
                            Next
                        End If
                        objfrmEmdonInterface.TestsName = strLabTestName

                        objfrmEmdonInterface.WindowState = FormWindowState.Maximized
                        objfrmEmdonInterface.ShowDialog(IIf(IsNothing(objfrmEmdonInterface.Parent), Me, objfrmEmdonInterface.Parent))
                        objfrmEmdonInterface.Dispose()
                        objfrmEmdonInterface = Nothing
                    Else

                        If ConfirmNull(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString()) Then
                            MessageBox.Show(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Patient is not registered With Emdeon,please try again.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                End If
            End If
        Else

            If LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.NoInternet Then

                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(True)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                objFrmConnectionConfirm.Dispose()

            ElseIf LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.ServerNotresponding Then
                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(False)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                objFrmConnectionConfirm.Dispose()

            End If
        End If

    End Sub


    Public Function GetProviderIDForUser(ByVal UserID As Int64) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim ProID As Int64 = 0
        Try
            oDB.Connect(False)
            ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " & UserID & ""))
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ProID = 0
        Finally
            oDB.Dispose()
        End Try
        Return ProID
    End Function

    Protected Function ConfirmNull(ByVal strValue As String) As Boolean
        Dim blnCheck As Boolean = False
        Try
            If strValue IsNot Nothing AndAlso strValue.ToString().Trim().Length <> 0 AndAlso strValue.ToString() <> "" Then

                blnCheck = True
            End If
        Catch ex As Exception

        End Try
        Return blnCheck
    End Function

    Private Function compareProvider(ByVal _PatientProviderID As Int64, ByVal _LoginUserProviderID As Int64) As Boolean

        'Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
        'Dim strProviderName As String = String.Empty
        'Dim strLoginUserName As String = String.Empty
        'Dim strLabID As String = String.Empty

        Try

            '12-May-14 Aniket: Remove the validations as some are not needed and some are moved to the Emdeon Screen.
            Return True

            'If _PatientProviderID <> 0 Then
            '    strProviderName = objClsGeneral.GetProviderName(_PatientProviderID, gnClinicID)
            'End If
            'If _LoginUserProviderID <> 0 Then
            '    strLoginUserName = objClsGeneral.GetProviderName(_LoginUserProviderID, gnClinicID)
            'End If
            'If _LoginUserProviderID = 0 Then

            '    Dim drMesgResult As DialogResult = MessageBox.Show(("The user you are using is not set up as a provider. If you proceed, the lab order " & vbCr & vbLf & "provider will be defaulted to the current patient’s provider '") + strProviderName & "'." & vbCr & vbLf & vbCr & vbLf & "Would you like to proceed with creating a new order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            '    If drMesgResult = DialogResult.Yes Then
            '        strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
            '        If ConfirmNull(strLabID.ToString()) Then
            '            Return True
            '        Else
            '            If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '                Return True
            '            Else
            '                Return False
            '            End If
            '        End If
            '    Else
            '        Return False
            '    End If
            'End If

            'If _LoginUserProviderID <> _PatientProviderID Then
            '    Dim dgResult As DialogResult = MessageBox.Show((("This patient is currently assigned to the provider '" & strProviderName & "'.Would " & vbCr & vbLf & "you like to change the patient provider to '") + strLoginUserName & "' ? " & vbCr & vbLf & vbCr & vbLf & "If you select 'No', the lab order will be created for '") + strProviderName & "'.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            '    If dgResult = DialogResult.Yes Then
            '        If objClsGeneral.changePatientProvider(_LoginUserProviderID, _PatientID) Then
            '            strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
            '            If ConfirmNull(strLabID.ToString()) Then
            '                Return True
            '            Else
            '                If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '                    Return True
            '                Else
            '                    Return False
            '                End If
            '            End If
            '        Else
            '            Return False
            '        End If
            '    ElseIf dgResult = DialogResult.No Then
            '        strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
            '        If ConfirmNull(strLabID.ToString()) Then
            '            Return True
            '        Else
            '            If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '                Return True
            '            Else
            '                Return False
            '            End If
            '        End If
            '    ElseIf dgResult = DialogResult.Cancel Then
            '        Return False

            '    End If
            'End If

            'If _LoginUserProviderID = _PatientProviderID Then
            '    strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
            '    If ConfirmNull(strLabID.ToString()) Then
            '        Return True
            '    Else
            '        If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '            Return True
            '        Else
            '            Return False
            '        End If
            '    End If
            'Else
            '    Return False
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        End Try

    End Function

End Class
