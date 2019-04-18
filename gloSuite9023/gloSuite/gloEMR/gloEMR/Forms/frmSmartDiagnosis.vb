Option Compare Text
Imports gloEMRGeneralLibrary
Imports gloTaskMail
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports gloUIControlLibrary
Imports System.Collections.ObjectModel
Imports System.ComponentModel


Public Class frmSmartDiagnosis

    Inherits System.Windows.Forms.Form

    Dim objReferralsDBLayer As New ClsReferralsDBLayer
    Public Shared nRefTempID As Int64 = 0
    Private m_PatientID As Int64
    Private ExamProviderId As Long
    Public blnExamFinished As Boolean
    Dim WithEvents frmExamChild As IExamChildEvents
    Public dtDOS As DateTime = DateTime.Now.Date
    Public Shared blnRefChangesMade As Boolean = False
    Dim nSmartDxID As Int64 = -1
    Dim objclsSmartDiagnosis As clsSmartDiagnosis
    Public mycaller As frmPatientExam
    ' Dim dtOrderbyCode As DataTable
    '   Dim dtOrderbyDesc As DataTable

    Private m_VisitID As Long
    Private m_ExamID As Long
    Private m_ProviderID As Long
    Private m_ExamFilePath As String
    Dim m_ExamDate As DateTime
    Friend WithEvents tblSmartDiagnosis As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Dim arrPE As New ArrayList
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents C1Dignosis As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents mnuSetasPrimary As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOpenICD9Assoc As System.Windows.Forms.MenuItem 'Added for opening ICD9 Association form
    Friend WithEvents GloUC_trvICD As gloUserControlLibrary.gloUC_TreeView
    Dim IsFormload As Boolean = False
    Dim _dt As DataTable
    '' SUDHIR 20090827 '' FOR CPT DRIVEN DIAGNOSIS ''
    Dim arrCPTDrivenDiagnosis As New ArrayList
    Dim objclsPatientEducation As New clsPatientEducation
    Friend WithEvents rbICD10 As System.Windows.Forms.RadioButton
    Friend WithEvents RbICD9 As System.Windows.Forms.RadioButton
    'Private ObjTasksDBLayer As New ClsTasksDBLayer
    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101012
    Dim _PatientID As Long
    Friend WithEvents pnl_btnAllDiagnosis As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnAllDiagnosis As System.Windows.Forms.Button
    Friend WithEvents pnl_btnPatDiagnosis As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnPatDiagnosis As System.Windows.Forms.Button
    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101012
    Dim _blnICDTransition As Boolean = False
    ''Const TVM_GETEDITCONTROL = "0x110F"

    Private oModifierControl As gloListControl.gloListControl
    Friend WithEvents pnlSmartDX As System.Windows.Forms.Panel
    ' Dim ofrmModifierControl = New frmViewListControl
    Friend WithEvents pnlModifiers As System.Windows.Forms.Panel
    ' Dim _IsNodeDoubleClick As Boolean = False
    Dim _IsSkipCheckEvent As Boolean = False

    Dim dtActiveCPTTable As DataTable
#Region " Windows Form Designer generated code "

    Public Property PatientID() As Int64
        Get
            Return m_PatientID
        End Get
        Set(ByVal value As Int64)
            m_PatientID = value
        End Set
    End Property
    Private _SmartDiagnosisWidth As String = "SmartDiagnosisScreenWidth"
    Public ReadOnly Property SmartDiagnosisWidth() As String
        Get
            Return _SmartDiagnosisWidth
        End Get
    End Property
    Public Sub New(ByVal VisitID As Long, ByVal ExamID As Long, ByVal ExamDate As DateTime, ByVal ProviderID As Long, ByVal PatientID As Long, Optional ByVal sExamFilePath As String = "")
        MyBase.New()
        m_VisitID = VisitID
        m_ExamID = ExamID
        m_ExamDate = ExamDate
        m_ProviderID = ProviderID
        m_ExamFilePath = sExamFilePath


        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101012
        _PatientID = PatientID
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101012
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
            Try
                If IsNothing(cntICD9Association) = False Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(cntICD9Association)
                    If (IsNothing(cntICD9Association.MenuItems) = False) Then
                        cntICD9Association.MenuItems.Clear()
                    End If
                    cntICD9Association.Dispose()
                    cntICD9Association = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If IsNothing(cntTags) = False Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(cntTags)
                    If (IsNothing(cntTags.MenuItems) = False) Then
                        cntTags.MenuItems.Clear()
                    End If
                    cntTags.Dispose()
                    cntTags = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents cntICD9Association As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents ImgAssociation As System.Windows.Forms.ImageList
    Friend WithEvents trICD9Association As System.Windows.Forms.TreeView
    Friend WithEvents imgSmartDia As System.Windows.Forms.ImageList
    Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem

    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    Friend WithEvents tblbtn_Save_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPatientQuestionnaire As System.Windows.Forms.Button
    Friend WithEvents btnAddFields As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents tblbtn_MICOFF_32 As System.Windows.Forms.ToolStripButton
    'Friend WithEvents wdNewExam As AxDSOFramer.AxFramerControl


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSmartDiagnosis))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GloUC_trvICD = New gloUserControlLibrary.gloUC_TreeView()
        Me.ImgAssociation = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_btnPatDiagnosis = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnPatDiagnosis = New System.Windows.Forms.Button()
        Me.pnl_btnAllDiagnosis = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnAllDiagnosis = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbICD10 = New System.Windows.Forms.RadioButton()
        Me.RbICD9 = New System.Windows.Forms.RadioButton()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.imgSmartDia = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.C1Dignosis = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.trICD9Association = New System.Windows.Forms.TreeView()
        Me.cntTags = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.tblSmartDiagnosis = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.cntICD9Association = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.mnuSetasPrimary = New System.Windows.Forms.MenuItem()
        Me.mnuOpenICD9Assoc = New System.Windows.Forms.MenuItem()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlModifiers = New System.Windows.Forms.Panel()
        Me.pnlSmartDX = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.pnl_btnPatDiagnosis.SuspendLayout()
        Me.pnl_btnAllDiagnosis.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.tblSmartDiagnosis.SuspendLayout()
        Me.pnlSmartDX.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GloUC_trvICD)
        Me.Panel1.Controls.Add(Me.pnl_btnPatDiagnosis)
        Me.Panel1.Controls.Add(Me.pnl_btnAllDiagnosis)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(691, 684)
        Me.Panel1.TabIndex = 1
        '
        'GloUC_trvICD
        '
        Me.GloUC_trvICD.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD.CheckBoxes = False
        Me.GloUC_trvICD.CodeMember = Nothing
        Me.GloUC_trvICD.ColonAsSeparator = False
        Me.GloUC_trvICD.Comment = Nothing
        Me.GloUC_trvICD.ConceptID = Nothing
        Me.GloUC_trvICD.CPT = Nothing
        Me.GloUC_trvICD.mpidmember = Nothing
        Me.GloUC_trvICD.DescriptionMember = Nothing
        Me.GloUC_trvICD.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvICD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvICD.DrugFlag = CType(16, Short)
        Me.GloUC_trvICD.DrugFormMember = Nothing
        Me.GloUC_trvICD.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD.DurationMember = Nothing
        Me.GloUC_trvICD.EducationMappingSearchType = 1
        Me.GloUC_trvICD.FrequencyMember = Nothing
        Me.GloUC_trvICD.HistoryType = Nothing
        Me.GloUC_trvICD.ICD9 = Nothing
        Me.GloUC_trvICD.ICDRevision = Nothing
        Me.GloUC_trvICD.ImageIndex = 2
        Me.GloUC_trvICD.ImageList = Me.ImgAssociation
        Me.GloUC_trvICD.ImageObject = Nothing
        Me.GloUC_trvICD.Indicator = Nothing
        Me.GloUC_trvICD.IsCPTSearch = False
        Me.GloUC_trvICD.IsDiagnosisSearch = False
        Me.GloUC_trvICD.IsDrug = False
        Me.GloUC_trvICD.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD.IsSearchForEducationMapping = False
        Me.GloUC_trvICD.IsSystemCategory = Nothing
        Me.GloUC_trvICD.Location = New System.Drawing.Point(0, 58)
        Me.GloUC_trvICD.MaximumNodes = 1000
        Me.GloUC_trvICD.Name = "GloUC_trvICD"
        Me.GloUC_trvICD.NDCCodeMember = Nothing
        Me.GloUC_trvICD.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_trvICD.ParentImageIndex = 0
        Me.GloUC_trvICD.ParentMember = Nothing
        Me.GloUC_trvICD.RouteMember = Nothing
        Me.GloUC_trvICD.RowOrderMember = Nothing
        Me.GloUC_trvICD.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD.SearchBox = True
        Me.GloUC_trvICD.SearchText = Nothing
        Me.GloUC_trvICD.SelectedImageIndex = 2
        Me.GloUC_trvICD.SelectedNode = Nothing
        Me.GloUC_trvICD.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvICD.SelectedParentImageIndex = 0
        Me.GloUC_trvICD.Size = New System.Drawing.Size(691, 598)
        Me.GloUC_trvICD.SmartTreatmentId = Nothing
        Me.GloUC_trvICD.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD.TabIndex = 0
        Me.GloUC_trvICD.Tag = Nothing
        Me.GloUC_trvICD.UnitMember = Nothing
        Me.GloUC_trvICD.ValueMember = Nothing
        '
        'ImgAssociation
        '
        Me.ImgAssociation.ImageStream = CType(resources.GetObject("ImgAssociation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgAssociation.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgAssociation.Images.SetKeyName(0, "")
        Me.ImgAssociation.Images.SetKeyName(1, "")
        Me.ImgAssociation.Images.SetKeyName(2, "Bullet06.ico")
        '
        'pnl_btnPatDiagnosis
        '
        Me.pnl_btnPatDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnPatDiagnosis.Controls.Add(Me.Label19)
        Me.pnl_btnPatDiagnosis.Controls.Add(Me.Label20)
        Me.pnl_btnPatDiagnosis.Controls.Add(Me.Label25)
        Me.pnl_btnPatDiagnosis.Controls.Add(Me.Label26)
        Me.pnl_btnPatDiagnosis.Controls.Add(Me.btnPatDiagnosis)
        Me.pnl_btnPatDiagnosis.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnPatDiagnosis.Location = New System.Drawing.Point(0, 656)
        Me.pnl_btnPatDiagnosis.Name = "pnl_btnPatDiagnosis"
        Me.pnl_btnPatDiagnosis.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_btnPatDiagnosis.Size = New System.Drawing.Size(691, 28)
        Me.pnl_btnPatDiagnosis.TabIndex = 19
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(4, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(686, 1)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 24)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(690, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 24)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(688, 1)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "label1"
        '
        'btnPatDiagnosis
        '
        Me.btnPatDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPatDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatDiagnosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPatDiagnosis.FlatAppearance.BorderSize = 0
        Me.btnPatDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatDiagnosis.Location = New System.Drawing.Point(3, 0)
        Me.btnPatDiagnosis.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnPatDiagnosis.Name = "btnPatDiagnosis"
        Me.btnPatDiagnosis.Size = New System.Drawing.Size(688, 25)
        Me.btnPatDiagnosis.TabIndex = 9
        Me.btnPatDiagnosis.Tag = "UnSelected"
        Me.btnPatDiagnosis.Text = "Patient Problem Diagnosis"
        Me.btnPatDiagnosis.UseVisualStyleBackColor = False
        '
        'pnl_btnAllDiagnosis
        '
        Me.pnl_btnAllDiagnosis.Controls.Add(Me.Label15)
        Me.pnl_btnAllDiagnosis.Controls.Add(Me.Label16)
        Me.pnl_btnAllDiagnosis.Controls.Add(Me.Label17)
        Me.pnl_btnAllDiagnosis.Controls.Add(Me.Label18)
        Me.pnl_btnAllDiagnosis.Controls.Add(Me.btnAllDiagnosis)
        Me.pnl_btnAllDiagnosis.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_btnAllDiagnosis.Location = New System.Drawing.Point(0, 30)
        Me.pnl_btnAllDiagnosis.Name = "pnl_btnAllDiagnosis"
        Me.pnl_btnAllDiagnosis.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_btnAllDiagnosis.Size = New System.Drawing.Size(691, 28)
        Me.pnl_btnAllDiagnosis.TabIndex = 13
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(4, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(686, 1)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 24)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(690, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 24)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(688, 1)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "label1"
        '
        'btnAllDiagnosis
        '
        Me.btnAllDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnAllDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllDiagnosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAllDiagnosis.FlatAppearance.BorderSize = 0
        Me.btnAllDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllDiagnosis.Location = New System.Drawing.Point(3, 0)
        Me.btnAllDiagnosis.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnAllDiagnosis.Name = "btnAllDiagnosis"
        Me.btnAllDiagnosis.Size = New System.Drawing.Size(688, 25)
        Me.btnAllDiagnosis.TabIndex = 10
        Me.btnAllDiagnosis.Tag = "Selected"
        Me.btnAllDiagnosis.Text = "All Diagnosis"
        Me.btnAllDiagnosis.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(691, 30)
        Me.Panel6.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.rbICD10)
        Me.Panel2.Controls.Add(Me.RbICD9)
        Me.Panel2.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Controls.Add(Me.lbl_RightBrd)
        Me.Panel2.Controls.Add(Me.lbl_TopBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(688, 24)
        Me.Panel2.TabIndex = 0
        '
        'rbICD10
        '
        Me.rbICD10.AutoSize = True
        Me.rbICD10.BackColor = System.Drawing.Color.Transparent
        Me.rbICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD10.Location = New System.Drawing.Point(135, 3)
        Me.rbICD10.Name = "rbICD10"
        Me.rbICD10.Size = New System.Drawing.Size(58, 18)
        Me.rbICD10.TabIndex = 11
        Me.rbICD10.TabStop = True
        Me.rbICD10.Text = "ICD10"
        Me.rbICD10.UseVisualStyleBackColor = False
        '
        'RbICD9
        '
        Me.RbICD9.AutoSize = True
        Me.RbICD9.BackColor = System.Drawing.Color.Transparent
        Me.RbICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbICD9.Location = New System.Drawing.Point(42, 3)
        Me.RbICD9.Name = "RbICD9"
        Me.RbICD9.Size = New System.Drawing.Size(51, 18)
        Me.RbICD9.TabIndex = 10
        Me.RbICD9.TabStop = True
        Me.RbICD9.Text = "ICD9"
        Me.RbICD9.UseVisualStyleBackColor = False
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(686, 1)
        Me.lbl_BottomBrd.TabIndex = 9
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
        Me.lbl_LeftBrd.TabIndex = 8
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(687, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 7
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(688, 1)
        Me.lbl_TopBrd.TabIndex = 6
        Me.lbl_TopBrd.Text = "label1"
        '
        'imgSmartDia
        '
        Me.imgSmartDia.ImageStream = CType(resources.GetObject("imgSmartDia.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSmartDia.TransparentColor = System.Drawing.Color.Transparent
        Me.imgSmartDia.Images.SetKeyName(0, "CPT.ico")
        Me.imgSmartDia.Images.SetKeyName(1, "Drugs.ico")
        Me.imgSmartDia.Images.SetKeyName(2, "Pat Education.ico")
        Me.imgSmartDia.Images.SetKeyName(3, "Tag.ico")
        Me.imgSmartDia.Images.SetKeyName(4, "Specialty.ico")
        Me.imgSmartDia.Images.SetKeyName(5, "ICD 09.ico")
        Me.imgSmartDia.Images.SetKeyName(6, "DX01.ico")
        Me.imgSmartDia.Images.SetKeyName(7, "Small Arrow.ico")
        Me.imgSmartDia.Images.SetKeyName(8, "Bullet06.ico")
        Me.imgSmartDia.Images.SetKeyName(9, "FLow sheet.ico")
        Me.imgSmartDia.Images.SetKeyName(10, "Lab orders.ico")
        Me.imgSmartDia.Images.SetKeyName(11, "Radiology.ico")
        Me.imgSmartDia.Images.SetKeyName(12, "Refferal.ico")
        Me.imgSmartDia.Images.SetKeyName(13, "Template Gallery.ico")
        Me.imgSmartDia.Images.SetKeyName(14, "CPTCommon.ico")
        Me.imgSmartDia.Images.SetKeyName(15, "ICD10GalleryGreen.ico")
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.C1Dignosis)
        Me.Panel7.Location = New System.Drawing.Point(250, 480)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(590, 239)
        Me.Panel7.TabIndex = 5
        Me.Panel7.Visible = False
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
        Me.C1Dignosis.Size = New System.Drawing.Size(590, 239)
        Me.C1Dignosis.StyleInfo = resources.GetString("C1Dignosis.StyleInfo")
        Me.C1Dignosis.TabIndex = 7
        Me.C1Dignosis.Visible = False
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(691, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 684)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.trICD9Association)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel5.Location = New System.Drawing.Point(695, 54)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(343, 684)
        Me.Panel5.TabIndex = 2
        '
        'trICD9Association
        '
        Me.trICD9Association.BackColor = System.Drawing.Color.White
        Me.trICD9Association.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trICD9Association.CheckBoxes = True
        Me.trICD9Association.ContextMenu = Me.cntTags
        Me.trICD9Association.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trICD9Association.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trICD9Association.ForeColor = System.Drawing.Color.Black
        Me.trICD9Association.HideSelection = False
        Me.trICD9Association.ImageIndex = 8
        Me.trICD9Association.ImageList = Me.imgSmartDia
        Me.trICD9Association.Indent = 21
        Me.trICD9Association.ItemHeight = 20
        Me.trICD9Association.LabelEdit = True
        Me.trICD9Association.Location = New System.Drawing.Point(1, 4)
        Me.trICD9Association.Name = "trICD9Association"
        Me.trICD9Association.SelectedImageIndex = 8
        Me.trICD9Association.ShowLines = False
        Me.trICD9Association.Size = New System.Drawing.Size(338, 676)
        Me.trICD9Association.TabIndex = 0
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
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 680)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(338, 1)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 677)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(339, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 677)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(340, 1)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.tblSmartDiagnosis)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1038, 54)
        Me.Panel4.TabIndex = 0
        '
        'tblSmartDiagnosis
        '
        Me.tblSmartDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.tblSmartDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblSmartDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblSmartDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSmartDiagnosis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblSmartDiagnosis.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblSmartDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSave, Me.tblClose})
        Me.tblSmartDiagnosis.Location = New System.Drawing.Point(0, 0)
        Me.tblSmartDiagnosis.Name = "tblSmartDiagnosis"
        Me.tblSmartDiagnosis.Size = New System.Drawing.Size(1038, 53)
        Me.tblSmartDiagnosis.TabIndex = 0
        Me.tblSmartDiagnosis.Text = "ToolStrip1"
        '
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.tblClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'cntICD9Association
        '
        Me.cntICD9Association.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteICD9Item, Me.mnuSetasPrimary, Me.mnuOpenICD9Assoc})
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 0
        Me.mnuDeleteICD9Item.Text = "Remove Item"
        '
        'mnuSetasPrimary
        '
        Me.mnuSetasPrimary.Index = 1
        Me.mnuSetasPrimary.Text = "Set as Primary"
        '
        'mnuOpenICD9Assoc
        '
        Me.mnuOpenICD9Assoc.Index = 2
        Me.mnuOpenICD9Assoc.Text = "Edit Item"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(695, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(343, 3)
        Me.Splitter2.TabIndex = 4
        Me.Splitter2.TabStop = False
        '
        'pnlModifiers
        '
        Me.pnlModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlModifiers.Location = New System.Drawing.Point(0, 0)
        Me.pnlModifiers.Name = "pnlModifiers"
        Me.pnlModifiers.Size = New System.Drawing.Size(1038, 738)
        Me.pnlModifiers.TabIndex = 13
        '
        'pnlSmartDX
        '
        Me.pnlSmartDX.Controls.Add(Me.Panel7)
        Me.pnlSmartDX.Controls.Add(Me.Splitter2)
        Me.pnlSmartDX.Controls.Add(Me.Panel5)
        Me.pnlSmartDX.Controls.Add(Me.Splitter1)
        Me.pnlSmartDX.Controls.Add(Me.Panel1)
        Me.pnlSmartDX.Controls.Add(Me.Panel4)
        Me.pnlSmartDX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSmartDX.Location = New System.Drawing.Point(0, 0)
        Me.pnlSmartDX.Name = "pnlSmartDX"
        Me.pnlSmartDX.Size = New System.Drawing.Size(1038, 738)
        Me.pnlSmartDX.TabIndex = 14
        '
        'frmSmartDiagnosis
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1038, 738)
        Me.Controls.Add(Me.pnlSmartDX)
        Me.Controls.Add(Me.pnlModifiers)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSmartDiagnosis"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Smart Diagnosis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.pnl_btnPatDiagnosis.ResumeLayout(False)
        Me.pnl_btnAllDiagnosis.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.tblSmartDiagnosis.ResumeLayout(False)
        Me.tblSmartDiagnosis.PerformLayout()
        Me.pnlSmartDX.ResumeLayout(False)
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
    ' Private col_IsCommon As Integer = 14
    Private Col_Count = 14
#End Region

#Region "Class Initializers"
    Private objDiagnosisDBLayer As ClsDiagnosisDBLayer
#End Region
    Dim IsFormLoading As Boolean = True
    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True
    ''Smart Dx Enhancements
    ''Private WithEvents dgCustomGrid As CustomTask
    Dim dt As DataTable
    Private Const col_Select As Integer = 0
    Private Const col_ModifierID As Integer = 1
    Private Const col_ModifierCode As Integer = 2
    Private Const col_ModifierDesc As Integer = 3
    Private Const col_ModifierCount As Integer = 4
    Dim sSmartDXName As String = ""
    ''
    Public Property blnICDTransition() As Boolean
        Get
            Return _blnICDTransition
        End Get
        Set(ByVal Value As Boolean)
            _blnICDTransition = Value
        End Set
    End Property

    Private Sub frmSmartDiagnosis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Added Code for Saving Width of the Screen
        Dim oclsDiagnosis As New ClsDiagnosisDBLayer
        Try
            Dim _Width As Long = Panel1.Width
            oclsDiagnosis.AddMyWidthSetting(_SmartDiagnosisWidth, _Width, gClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
            If Not objReferralsDBLayer Is Nothing Then
                objReferralsDBLayer.Dispose()
                objReferralsDBLayer = Nothing
            End If
            If Not objclsPatientEducation Is Nothing Then
                objclsPatientEducation.Dispose()
                objclsPatientEducation = Nothing
            End If
            If (IsNothing(objclsSmartDiagnosis) = False) Then
                objclsSmartDiagnosis.Dispose()
                objclsSmartDiagnosis = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oclsDiagnosis) Then
                oclsDiagnosis.Dispose()
                oclsDiagnosis = Nothing
            End If
        End Try
        
        
    End Sub

    Private Sub frmSmartDiagnosis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Added Code to getMyWidth as per USERID
            Dim dtMyWidth As DataTable
            Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
            objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            Dim MyWidth As Object = Nothing

            dtMyWidth = objDiagnosisDBLayer.GetMyWidthSetting(_SmartDiagnosisWidth)
            If (dtMyWidth.Rows.Count > 0) Then
                MyWidth = Convert.ToString(dtMyWidth.Rows(0)("sSettingsValue"))
                Panel1.Width = MyWidth
            End If

            If Not IsNothing(dtMyWidth) Then
                dtMyWidth.Dispose()
                dtMyWidth = Nothing
            End If

            Dim dbSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(GetConnectionString())
            Dim oSettingValue As Object = Nothing
            dbSettings.GetSetting("DEFAULTPROBLEMDXFORSMARTDX", oSettingValue)
            If Not IsNothing(oSettingValue) AndAlso Convert.ToString(oSettingValue).Trim() <> "" Then

                If Convert.ToInt32(oSettingValue) = 1 Then

                    pnl_btnPatDiagnosis.Dock = DockStyle.Top
                    btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnPatDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
                    btnPatDiagnosis.Tag = "Selected"

                    pnl_btnAllDiagnosis.Dock = DockStyle.Bottom
                    btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
                    btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
                    btnAllDiagnosis.Tag = "UnSelected"

                End If

            End If

            dtActiveCPTTable = New DataTable()
            Dim colCPTCode As New DataColumn("sCPTCode")
            Dim colFromDate As New DataColumn("dtFromDate")
            Dim colToDate As New DataColumn("dtToDate")

            dtActiveCPTTable.Columns.Add(colCPTCode)
            dtActiveCPTTable.Columns.Add(colFromDate)
            dtActiveCPTTable.Columns.Add(colToDate)

            oSettingValue = Nothing
            dbSettings.Dispose()
            dbSettings = Nothing

            pnlModifiers.Visible = False
            pnlModifiers.SendToBack()
            pnlSmartDX.Visible = True
            trICD9Association.DrawMode = TreeViewDrawMode.OwnerDrawAll


            Me.DoubleBuffered = True
            If (IsNothing(objclsSmartDiagnosis)) Then
                objclsSmartDiagnosis = New clsSmartDiagnosis
            End If

            _dt = Nothing
            _dt = objclsSmartDiagnosis.FetchSmartDiagnosis()


            trICD9Association.AllowDrop = True

            frmPatientExam.nRefTempID = 0
            Dim rootnode As myTreeNode
            rootnode = New myTreeNode("Common", -1)
            rootnode.ImageIndex = 14
            rootnode.SelectedImageIndex = 14
            trICD9Association.Nodes.Add(rootnode)
            rootnode = Nothing

            rootnode = New myTreeNode("Diagnosis", -1)
            rootnode.ImageIndex = 6
            rootnode.SelectedImageIndex = 6
            trICD9Association.Nodes.Add(rootnode)

            rootnode = Nothing 'Change made to solve memory Leak and word crash issue

            frmPatientExam.arrTagID = New ArrayList

            '''''''''' Fill Associates of Tags In Context Menu
            Call FillTagsAssociates()

            '''''''''' If Smart_Diagnosis already Exists
            IsFormload = True
            If gblnICD9Driven Then
                Load_Diagnosis()
            Else
                Load_CPTDrivenDiagnosis()
            End If
            IsFormload = False

            ''''
            Dim dtPE As DataTable
            dtPE = objclsSmartDiagnosis.FetchPatientEducation(m_VisitID)

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
                            lst = Nothing 'Change made to solve memory Leak and word crash issue
                        Next
                    Next
                    ''''''<><><><><><><><>
                    arrPE = objclsSmartDiagnosis.GetEducationID(arrPE)
                    ''''''<><><><><><><><>
                End If
            End If
            ''''
            If Not IsNothing(dtPE) Then
                dtPE.Dispose()
                dtPE = Nothing
            End If
            If gblnICD9Driven Then
                SetGridStyle()
                FillICDCPTMOD()
            End If


            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, "Smart Diagnosis Opened", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)

            'This function is use to fill nodes to which patient visited 
            FillNodes()
            CheckAllParentNodes()
            DisplayCommonCPT()
            UncheckNodesInICD()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            
            IsFormLoading = False
        End Try
    End Sub
#Region "ICD Driven Common CPT"

    Private Function ValidateDiagnosis() As Boolean
        Dim _iscommonchecked As Boolean = False
        For Each mynode As myTreeNode In trICD9Association.Nodes(0).Nodes
            If mynode.Checked = True Then
                _iscommonchecked = True
                Exit For
            End If
        Next
        Dim _ispresent As Boolean = False
        If _iscommonchecked = True Then
            If trICD9Association.Nodes(1).Nodes.Count <= 0 Then
                _ispresent = False
            Else
                _ispresent = True

            End If
            'For Each mynode As myTreeNode In trICD9Association.Nodes(1).Nodes
            '    If mynode.Checked Then
            '        _ispresent = True
            '        Exit For
            '    End If

            '    'For Each _cptnode As myTreeNode In mynode.Nodes
            '    '    If _cptnode.Text = "CPT" Then
            '    '        For Each _child As myTreeNode In _cptnode.Nodes
            '    '            If _child.Checked Then

            '    '                _ispresent = True
            '    '                Exit For

            '    '            End If
            '    '        Next
            '    '    End If
            '    'Next
            'Next
            If _ispresent = False Then
                MessageBox.Show("Only CPT(s) from Common CPT are selected. Please select at least one diagnosis in order to save. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If
        Return True
    End Function

    Private Function Validate() As Boolean
        Dim strICD As String = ""
        For Each mynode As myTreeNode In trICD9Association.Nodes(0).Nodes
            If mynode.Checked Then
                If strICD = "" Then
                    strICD = IsPresentInTree(mynode)
                Else
                    strICD = strICD & vbNewLine & IsPresentInTree(mynode)
                End If

            End If
        Next
        If strICD <> "" Then
            If MessageBox.Show("Following ICD/CPT’s have got modifiers which are conflicting with common CPT’s. Either remove the modifiers from the common CPT’s or from the ICD’s CPT: " & vbNewLine & vbNewLine & strICD & vbNewLine & vbNewLine & "Click on ‘Yes’ to ignore the ICD’s CPT’s modifiers and save the common CPT’s modifiers. " & vbNewLine & vbNewLine & "Click on ‘No’ if you are going to correct the entries manually. ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Return True
            Else
                Return False
            End If

        End If
        Return True
    End Function
    Private Function IsPresentInTree(ByVal mynode As myTreeNode) As String
        Dim strICD As String = ""
        For Each _node As myTreeNode In trICD9Association.Nodes(1).Nodes
            For Each _cptnode As myTreeNode In _node.Nodes
                If _cptnode.Text = "CPT" Then
                    For Each _child As myTreeNode In _cptnode.Nodes
                        If _child.Text = mynode.Text Then
                            If _child.Checked Then
                                'If (_child.Nodes(1).Nodes.Count > 0 And mynode.Nodes(1).Nodes.Count > 0) Or (_child.Nodes(0).Text <> "Units: 1" And mynode.Nodes(0).Text <> "Units: 1") Then
                                If (_child.Nodes(1).Nodes.Count > 0 And mynode.Nodes(1).Nodes.Count > 0) Then
                                    If strICD = "" Then
                                        strICD = _child.Parent.Parent.Text.Split("-").GetValue(0) & ": " & _child.Text.Split("-").GetValue(0)
                                    Else

                                        strICD = strICD & vbNewLine & _child.Parent.Parent.Text.Split("-").GetValue(0) & ": " & _child.Text.Split("-").GetValue(0)
                                    End If

                                End If

                            End If
                        End If
                    Next
                    Exit For
                End If
            Next
            If gblnSetCPTtoAllICD9 = False Then
                Exit For
            End If
        Next
        Return strICD
    End Function

    Private Sub DisplayCommonCPT()

        Dim arrAvailableICD9s As New ArrayList
        Try
            If gblnICD9Driven = False Then
                If arrCPTDrivenDiagnosis IsNot Nothing Then
                    If arrCPTDrivenDiagnosis.Count > 0 Then

                        Dim oICD9 As gloEMRGeneralLibrary.gloGeneral.myList

                        For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                            oICD9 = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)

                            If oICD9.Code <> "" And arrAvailableICD9s.Contains(oICD9.Code & " - " & oICD9.Description) = False Then

                                arrAvailableICD9s.Add(oICD9.Code)
                            End If
                        Next
                        oICD9 = Nothing 'Change made to solve memory Leak and word crash issue
                    End If
                End If
            End If ''''

            objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            Dim dtCommon As DataTable
            dtCommon = objDiagnosisDBLayer.FetchCommonCPT()
            If Not IsNothing(objDiagnosisDBLayer) Then
                objDiagnosisDBLayer.Dispose()
                objDiagnosisDBLayer = Nothing
            End If
            Dim _node As myTreeNode
            If Not IsNothing(dtCommon) Then
                If dtCommon.Rows.Count > 0 Then
                    For k As Integer = 0 To dtCommon.Rows.Count - 1
                        _node = New myTreeNode
                        _node.Text = dtCommon.Rows(k)(1)
                        _node.Key = dtCommon.Rows(k)(0)
                        trICD9Association.Nodes(0).Nodes.Add(_node)
                        If dtCommon.Rows(k)(3) = True Then
                            ' _node.Checked = True
                        End If
                        Dim unitsmynode As New myTreeNode("Units: ", dtCommon.Rows(k)(0))
                        unitsmynode.UnitsKey = "Units"
                        unitsmynode.ForeColor = Color.Blue
                        unitsmynode.Text = "Units: 1"

                        Dim modmynode As New myTreeNode("Modifiers", dtCommon.Rows(k)(0))
                        modmynode.ModifierKey = "M1"
                        modmynode.ForeColor = Color.Blue
                        _node.Nodes.Add(unitsmynode)
                        _node.Nodes.Add(modmynode)
                        modmynode.Nodes.Clear()
                        _node.Parent.Expand()
                        If gblnICD9Driven Then
                            If CheckCPT(_node) = True Then
                                _node.Checked = True
                                DisplayCommonModifiers(_node)

                            End If
                        Else
                            If arrAvailableICD9s.Count > 0 Then
                                If CheckCPTforCPTDriven(_node, arrAvailableICD9s) = True Then
                                    _node.Checked = True
                                    DisplayCommonModifiers(_node)

                                End If
                            Else
                                If arrCPTDrivenDiagnosis.Count > 0 Then
                                    _node.Checked = False
                                    Dim olist As gloEMRGeneralLibrary.gloGeneral.myList
                                    For m As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                                        olist = arrCPTDrivenDiagnosis(m)
                                        If olist.HistoryCategory.Trim = _node.Text.Split("-").GetValue(0).trim Then
                                            _node.Checked = True
                                            DisplayCommonModifiers(_node)
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If
                        _node = Nothing
                    Next
                End If
            End If
            If Not IsNothing(dtCommon) Then
                dtCommon.Dispose()
                dtCommon = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(arrAvailableICD9s) Then
                arrAvailableICD9s.Clear()
                arrAvailableICD9s = Nothing
            End If
        End Try
    End Sub
    Private Sub UncheckNodesInICD()
        For Each _common As myTreeNode In trICD9Association.Nodes(0).Nodes
            If _common.Checked Then
                For Each _ICD As myTreeNode In trICD9Association.Nodes(1).Nodes
                    For Each _CPT As myTreeNode In _ICD.Nodes
                        If _CPT.Text = "CPT" Then
                            For Each _cptchild As myTreeNode In _CPT.Nodes
                                If _cptchild.Text = _common.Text Then
                                    _cptchild.Checked = False
                                    Exit For
                                End If
                            Next
                            Exit For
                        End If
                    Next
                    ' Exit For
                Next
            End If

        Next
    End Sub
    Private Function CheckCPT(ByVal _node As myTreeNode)
        Dim _isnodecheck As Boolean = False
        Dim ofnode As C1.Win.C1FlexGrid.Node
        Try

            If trICD9Association.Nodes(1).Nodes.Count > 0 Then
                _node.Checked = False
            End If
            For i As Integer = 0 To C1Dignosis.Rows.Count - 1
                ofnode = C1Dignosis.Rows(i).Node
                If Not IsNothing(ofnode) Then


                    If ofnode.Level = 1 Then
                        If ofnode.Data = _node.Text Then
                            For Each _child As myTreeNode In trICD9Association.Nodes(1).Nodes
                                If _child.Text.Trim = ofnode.Parent.Data.trim Then
                                    For Each _cpt As myTreeNode In _child.Nodes
                                        If _cpt.Text = "CPT" Then
                                            If IsContainiCPT(_cpt, _node) Then
                                                Exit For
                                            Else

                                                _isnodecheck = True
                                                Exit For
                                            End If

                                        End If
                                    Next
                                    Exit For
                                End If

                            Next
                        End If
                    End If
                End If
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return _isnodecheck
    End Function
    Private Function IsContainiCPT(ByVal _node As myTreeNode, ByVal _commonnode As myTreeNode) As Boolean
        For Each _cpt As myTreeNode In _node.Nodes
            If _cpt.Text = _commonnode.Text Then

                Return True
            End If
        Next
        Return False
    End Function

    Private Function IscontainInCommonCPT(ByVal _node As myTreeNode, ByVal _commonnode As myTreeNode) As Boolean
        For Each _cpt As myTreeNode In _node.Nodes
            If _cpt.Text = _commonnode.Text Then
                If gblnICD9Driven = False Then
                    If _commonnode.Nodes.Count = 2 Then
                        If _commonnode.Nodes(1).Nodes.Count > 0 Then
                            _node.Nodes.Remove(_cpt)
                        End If
                    End If
                    ' _node.Nodes.Remove(_cpt)
                End If

                Return True
            End If
        Next
        Return False
    End Function
    Private Sub DisplayCommonModifiers(ByVal _node As myTreeNode)
        Dim obj As New ClsTreatmentDBLayer
        Try


            Dim _PrimaryICD As String = ""
            Dim _PrimaryICDID As Long
            Dim _ICDNode As myTreeNode = Nothing

            If trICD9Association.Nodes(1).Nodes.Count > 0 Then
                _ICDNode = trICD9Association.Nodes(1).Nodes(0)
                _PrimaryICD = _ICDNode.Text.Split("-").GetValue(0)
                _PrimaryICDID = _ICDNode.Key
            End If



            Dim _ICdCode As String = ""
            Dim _ICDDesc As String = ""
            Dim strICD9() As String
            Dim strCPT() As String
            Dim dtUnit As DataTable
            If Not IsNothing(_ICDNode) Then
                strICD9 = Split(_ICDNode.Text, "-", 2)
                _ICdCode = strICD9.GetValue(0)
                _ICDDesc = strICD9.GetValue(1)
            Else
                _ICdCode = ""
                _ICDDesc = ""
            End If

            strCPT = Split(_node.Text, "-", 2)
            dtUnit = obj.FetchModforUpdate(m_ExamID, _ICdCode, _ICDDesc.Trim, strCPT.GetValue(0), strCPT.GetValue(1), True)
            If Not IsNothing(dtUnit) Then
                If dtUnit.Rows.Count > 0 Then
                    If dtUnit.Rows(0)(5) <> 0 Then
                        _node.Nodes(0).Text = "Units: " & FormatNumber(dtUnit.Rows(0)(5))
                    Else
                        _node.Nodes(0).Text = "Units: 1"
                    End If
                End If

            End If
            If Not IsNothing(dtUnit) Then
                dtUnit.Dispose()
                dtUnit = Nothing
            End If
            '' To Get CPTs of the Selected ICD9
            Dim dtMod As DataTable
            If gblnICD9Driven Then
                dtMod = obj.FetchModforUpdate(m_ExamID, _ICdCode.Trim, _ICDDesc, Convert.ToString(strCPT.GetValue(0)).Trim, Convert.ToString(strCPT.GetValue(1)), False, True)
                If Not IsNothing(dtMod) Then
                    If dtMod.Rows.Count > 0 Then
                        For m As Integer = 0 To dtMod.Rows.Count - 1
                            If dtMod.Rows(m)(3) <> "" Then
                                Dim mymodnode As New myTreeNode

                                mymodnode.Text = dtMod.Rows(m)(2) & " - " & Convert.ToString(dtMod.Rows(m)(3)).Trim
                                mymodnode.Key = dtMod.Rows(m)(4)
                                mymodnode.ModifierCode = dtMod.Rows(m)(2)

                                mymodnode.ModifierKey = "999"
                                _node.Nodes(1).Nodes.Add(mymodnode)
                                mymodnode = Nothing
                            End If




                        Next
                    End If
                End If
                dtMod.Dispose()
                dtMod = Nothing
            Else
                dtMod = obj.FetchModforUpdate(m_ExamID, _ICdCode, _ICDDesc.Trim, strCPT.GetValue(0), strCPT.GetValue(1), False, False)
                If Not IsNothing(dtMod) Then
                    If dtMod.Rows.Count > 0 Then
                        For m As Integer = 0 To dtMod.Rows.Count - 1
                            If Convert.ToString(dtMod.Rows(m)(3)).Trim <> "" Then
                                Dim mymodnode As New myTreeNode

                                mymodnode.Text = dtMod.Rows(m)(2) & " - " & Convert.ToString(dtMod.Rows(m)(3)).Trim
                                mymodnode.Key = dtMod.Rows(m)(4)
                                mymodnode.ModifierCode = dtMod.Rows(m)(2)

                                mymodnode.ModifierKey = "999"
                                _node.Nodes(1).Nodes.Add(mymodnode)
                                mymodnode = Nothing
                            End If

                        Next
                    End If
                End If
                dtMod.Dispose()
                dtMod = Nothing


            End If
            _node.Nodes(1).Expand()

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            If Not IsNothing(obj) Then
                obj.Dispose()
                obj = Nothing
            End If
        End Try
    End Sub
    Private Function CheckCPTforCPTDriven(ByVal _node As myTreeNode, ByVal arrICD9 As ArrayList) As Boolean
        Dim _isnodecheck As Boolean = False

        Try

            If trICD9Association.Nodes(1).Nodes.Count > 0 Then
                _node.Checked = False
            End If

            If arrICD9 IsNot Nothing Then
                If arrICD9.Count >= 0 Then
                    For j As Integer = 0 To arrICD9.Count - 1

                        Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
                        For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                            oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                            If oList.Code = CType(arrICD9(j), String) Then
                                If oList.HistoryCategory.Trim = _node.Text.Split("-").GetValue(0).Trim Then
                                    For Each _child As myTreeNode In trICD9Association.Nodes(1).Nodes
                                        If _child.Text.Split("-").GetValue(0).Trim = oList.Code Then
                                            For Each _cpt As myTreeNode In _child.Nodes
                                                If _cpt.Text = "CPT" Then
                                                    If IsContainiCPT(_cpt, _node) Then
                                                        Exit For
                                                    Else
                                                        _isnodecheck = True
                                                        Exit For
                                                    End If

                                                End If
                                            Next
                                            Exit For
                                        End If

                                    Next
                                End If
                            End If
                        Next

                    Next
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return _isnodecheck
    End Function


    Private Sub RemoveselectedCommonCPT(ByVal _CPTNode As myTreeNode)
        Dim oRemoveNode As C1.Win.C1FlexGrid.Node

        For i As Integer = C1Dignosis.Rows.Count - 1 To 0 Step -1

            oRemoveNode = C1Dignosis.Rows(i).Node
            If Not IsNothing(oRemoveNode) Then
                If oRemoveNode.Level = 1 Then
                    If oRemoveNode.Data.ToString.Trim = _CPTNode.Text.Trim Then
                        oRemoveNode.RemoveNode()

                    End If
                End If
            End If
            '  oRemoveNode = Nothing
        Next




    End Sub
    Private Sub AddCommonCPTinGrid()
        Dim _isPresent As Boolean = False
        For Each _node As myTreeNode In trICD9Association.Nodes(0).Nodes
            If _node.Checked Then

                For Each _DxNode As myTreeNode In trICD9Association.Nodes(1).Nodes
                    For Each _CPT As myTreeNode In _DxNode.Nodes
                        If _CPT.Text = "CPT" Then

                            AddDatainGrid_new(_node, _DxNode)
                            Exit For

                        End If
                    Next
                    ''if set cpt to all ICD9 of then
                    If gblnSetCPTtoAllICD9 = False Then
                        Exit For
                    End If

                Next
            End If

        Next
    End Sub



    Private Sub AddDatainGrid_new(ByVal _node As myTreeNode, ByVal ICD As TreeNode)
        Dim ofNode As C1.Win.C1FlexGrid.Node

        Dim _strCPT As String = ""
        Dim strCPT() As String
        strCPT = _node.Text.Split("-")
        Dim index As Integer = 0
        Dim ispresent As Boolean = False
        Dim NewRow As Integer = 0
        For i As Integer = 0 To C1Dignosis.Rows.Count - 1
            ofNode = C1Dignosis.Rows(i).Node
            If Not IsNothing(ofNode) Then

                If ofNode.Level = 0 Then

                    If Convert.ToString(ofNode.Data).Trim = ICD.Text.Trim Then

                        'If _node.Nodes(1).Nodes.Count > 0 Or _node.Nodes(0).Text <> "Units: 1" Then

                        If IsContainInICDChildren(ofNode, _node) = True Then

                            If _node.Nodes.Count = 2 Then
                                If _node.Nodes(1).Nodes.Count > 0 Then
                                    If Not IsNothing(ofNode.LastChild) Then
                                        If Not IsNothing(ofNode.LastChild.LastChild) Then
                                            NewRow = ofNode.LastChild.LastChild.Row.Index + 1
                                        Else
                                            NewRow = ofNode.LastChild.Row.Index + 1
                                        End If
                                    Else
                                        NewRow = ofNode.Row.Index + 1
                                    End If
                                    appendCommonCPT(_node, ICD.Text, NewRow, ofNode)
                                    Exit For

                                End If
                            End If
                        Else
                            If Not IsNothing(ofNode.LastChild) Then
                                If Not IsNothing(ofNode.LastChild.LastChild) Then
                                    NewRow = ofNode.LastChild.LastChild.Row.Index + 1
                                Else
                                    NewRow = ofNode.LastChild.Row.Index + 1
                                End If
                            Else
                                NewRow = ofNode.Row.Index + 1
                            End If
                            appendCommonCPT(_node, ICD.Text, NewRow, ofNode)
                            Exit For

                        End If

                    End If

                End If
            End If

        Next

    End Sub
    Private Sub appendCommonCPT(ByVal _node As myTreeNode, ByVal ICD As String, ByVal _index As Integer, ByVal ofNode As C1.Win.C1FlexGrid.Node)
        Dim strCPT() As String = Nothing

        Dim _strCPT As String = ""

        Dim _ispresent As Boolean = False
        strCPT = _node.Text.Split("-")
        Dim _row As Integer = 0
        Dim ChildRow As Integer = 0
        If _node.Checked Then


            If _index > 0 Then

                _row = C1Dignosis.Rows.Insert(_index).Index
                Dim arrstrConctCPT() As String
                arrstrConctCPT = Split(_node.Text, "-", 2)
                With C1Dignosis

                    With .Rows(_row)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                        .Node.Data = _node.Text
                    End With
                    .SetData(_row, Col_ICD9Code, .GetData(_row - 1, Col_ICD9Code))
                    .SetData(_row, Col_ICD9Desc, .GetData(_row - 1, Col_ICD9Desc))
                    .SetData(_row, COl_CPTCode, arrstrConctCPT.GetValue(0))
                    .SetData(_row, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                    .SetData(_row, Col_ICD9Count, .GetData(_row - 1, Col_ICD9Count))
                    .SetData(_row, Col_CPTCount, .GetData(_row - 1, Col_CPTCount) + 1)
                    .SetData(_row, Col_ICDRevision, .GetData(_row - 1, Col_ICDRevision))

                    If _node.Nodes.Count > 0 Then
                        Dim unitsnode As New myTreeNode
                        unitsnode = _node.Nodes(0)
                        .SetData(_row, Col_Units, unitsnode.Text.Replace("Units: ", ""))
                    End If

                    If _node.Nodes.Count > 1 Then

                        '' ''Added by Mayuri :20150106-
                        Dim unitsnode As New myTreeNode
                        unitsnode = _node.Nodes(0)

                        Dim modnode As myTreeNode
                        Dim oldmodnode As myTreeNode
                        Dim arrstrMod() As String
                        If _node.Nodes(1).Nodes.Count > 0 Then
                            Dim oRow1 As C1.Win.C1FlexGrid.Row
                            If _row = 0 Then
                                .Rows.Add()
                                ChildRow = .Rows.Count - 1
                            Else
                                oRow1 = .Rows.Insert(_row + 1)
                                ChildRow = oRow1.Index
                            End If

                            For k As Integer = 0 To _node.Nodes(1).Nodes.Count - 1

                                If k > 0 Then

                                    ChildRow = ChildRow + 1
                                    C1Dignosis.Rows.Insert(ChildRow)
                                End If
                                '  NewRow = NewRow + 1
                                With .Rows(ChildRow)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 2
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.Modifier
                                    .Node.Data = _node.Nodes(1).Nodes(k).Text
                                End With



                                arrstrMod = Split(_node.Nodes(1).Nodes(k).Text, "-", 2)
                                oldmodnode = New myTreeNode
                                oldmodnode = _node.Nodes(1).Nodes(k)
                                modnode = New myTreeNode
                                modnode = _node.Nodes(1).Nodes(k)

                                modnode.Key = oldmodnode.Key
                                modnode.ModifierCode = oldmodnode.ModifierCode
                                modnode.ModifierKey = oldmodnode.ModifierKey

                                .SetData(ChildRow, Col_ICD9Code, .GetData(_index, Col_ICD9Code))
                                .SetData(ChildRow, Col_ICD9Desc, .GetData(_index, Col_ICD9Desc))
                                .SetData(ChildRow, Col_ICD9Count, .GetData(_index, Col_ICD9Count))
                                .SetData(ChildRow, Col_ICDRevision, .GetData(_index, Col_ICDRevision))

                                .SetData(ChildRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                .SetData(ChildRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))

                                .SetData(ChildRow, Col_CPTCount, 1)


                                .SetData(ChildRow, Col_ModCode, modnode.ModifierCode)
                                .SetData(ChildRow, Col_ModDesc, arrstrMod.GetValue(1))
                                .SetData(ChildRow, Col_ModCount, 1)

                                .SetData(ChildRow, Col_Units, unitsnode.Text.Replace("Units: ", ""))

                                modnode = Nothing
                                oldmodnode = Nothing

                            Next
                            unitsnode = Nothing
                        End If
                    End If
                    ''
                    ''
                End With
            End If
        End If
    End Sub


    Private Function IsContainInICDChildren(ByVal ofnode As C1.Win.C1FlexGrid.Node, ByVal CPT As myTreeNode) As Boolean
        Dim _ispresent As Boolean = False
        For i As Integer = 0 To ofnode.Nodes.Count - 1
            If Convert.ToString(ofnode.Nodes(i).Data).Trim = CPT.Text.Trim Then
                If CPT.Nodes.Count = 2 Then
                    If CPT.Nodes(1).Nodes.Count > 0 Then
                        ofnode.Nodes(i).RemoveNode()
                    End If
                End If

                _ispresent = True
                Exit For
            End If
        Next

        Return _ispresent
    End Function


#End Region
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

                .Cols(Col_ICD9Code).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Code, "ICD9CODE")
                .Cols(Col_ICD9Code).AllowEditing = True
                .Cols(Col_ICD9Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Desc).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Desc, "ICD9Description")
                .Cols(Col_ICD9Desc).AllowEditing = True
                .Cols(Col_ICD9Desc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COl_CPTCode).Width = _TotalWidth * 0
                .SetData(0, COl_CPTCode, "CPTCODE")
                .Cols(COl_CPTCode).AllowEditing = True
                .Cols(COl_CPTCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTDesc).Width = _TotalWidth * 0
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
                .Cols(Col_Units).DataType = GetType(System.Decimal)
                .Cols(Col_Units).AllowEditing = True
                .Cols(Col_Units).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Count).Width = _TotalWidth * 0.17
                .SetData(0, Col_ICD9Count, "ICD9 Count")
                .Cols(Col_ICD9Count).DataType = GetType(System.Int64)
                .Cols(Col_ICD9Count).AllowEditing = True
                .Cols(Col_ICD9Count).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTCount).Width = _TotalWidth * 0.17
                .SetData(0, Col_CPTCount, "CPT Count")
                .Cols(Col_CPTCount).DataType = GetType(System.Int64)
                .Cols(Col_CPTCount).AllowEditing = True
                .Cols(Col_CPTCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCount).Width = _TotalWidth * 0.17
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
                .Cols(Col_ICDRevision).AllowEditing = True
                .Cols(Col_ICDRevision).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                '.Cols(col_IsCommon).Width = _TotalWidth * 0
                '.Cols(col_IsCommon).AllowEditing = False
                '.Cols(col_IsCommon).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
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


            Dim dtICD9 As DataTable = Nothing
            Dim dtCPT As DataTable = Nothing
            Dim dtMOD As DataTable = Nothing
            Dim dvICD9 As DataView = Nothing
            Dim dvCPT As DataView = Nothing


            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16


            objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            ' flag = 0 - ICD9   flag = 1 - CPT flag = 2 -MOD
            dtICD9 = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamID, m_VisitID, "", "", "", 5)
            objDiagnosisDBLayer.Dispose()
            objDiagnosisDBLayer = Nothing
            If Not IsNothing(dtICD9) Then
                dvICD9 = New DataView(dtICD9)

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
                                objDiagnosisDBLayer.Dispose()
                                objDiagnosisDBLayer = Nothing
                                If Not IsNothing(dtCPT) Then
                                    dvCPT = New DataView(dtCPT)

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
                                                        .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                        .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                        .SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))
                                                        .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                        .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                        .SetData(_Row, Col_ICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                                    End With

                                                End If

                                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                                dtMOD = objDiagnosisDBLayer.FetchICD9CPTMod(m_ExamID, m_VisitID, strCurrentICD9, strCurrentCPT, "", 2)
                                                objDiagnosisDBLayer.Dispose()
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
                                                                    .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                                                    .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                                                    .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                    .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                    .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
                                                                    .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
                                                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                                    .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                                    .SetData(_Row, Col_ModCount, nMOD + 1)
                                                                    .SetData(_Row, Col_ICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                                                End With
                                                            End If
                                                        Next
                                                        If Not IsNothing(dtMOD) Then
                                                            dtMOD.Dispose()
                                                            dtMOD = Nothing
                                                        End If
                                                    End If
                                                End With '' With dtMOD
                                            Next '' For nCPT = 0 To .Rows.Count - 1
                                            If Not IsNothing(dtCPT) Then
                                                dtCPT.Dispose()
                                                dtCPT = Nothing
                                            End If
                                        End If
                                    End With '' With dtCPT
                                End If
                            End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                        Next ''For nICD9 = 0 To .Rows.Count - 1
                        If Not IsNothing(dtICD9) Then
                            dtICD9.Dispose()
                            dtICD9 = Nothing
                        End If
                    End If  '' If IsNothing(dtICD9) = False Then
                End With '' With dtICD9
            End If
            If Not IsNothing(dtICD9) Then
                dtICD9.Dispose()
                dtICD9 = Nothing
            End If
            If Not IsNothing(dtCPT) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If Not IsNothing(dtMOD) Then
                dtMOD.Dispose()
                dtMOD = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetSelectedSmartDxID(ByVal ICDid As Int64, Optional ByVal sICDCode As String = "") As Int64
        Dim oSmartDx As smartDx = Nothing
        Dim dtSmartDxIDs As DataTable = Nothing
        Dim nSmartDxId As Int64 = 0
        Try
            oSmartDx = New smartDx()
            dtSmartDxIDs = oSmartDx.getAssociatedSmartDxID(ICDid, m_ExamID, m_VisitID)
            If dtSmartDxIDs IsNot Nothing AndAlso dtSmartDxIDs.Rows.Count > 0 Then
                If dtSmartDxIDs.Rows.Count > 1 Then
                    Dim oSelectSmartDxId As New frmSelectSmartIDs(dtSmartDxIDs)
                    oSelectSmartDxId.WindowState = FormWindowState.Normal
                    oSelectSmartDxId.MaximizeBox = True
                    oSelectSmartDxId.ShowInTaskbar = False
                    oSelectSmartDxId.sICDs = sICDCode
                    oSelectSmartDxId.ShowDialog(Me)
                    nSmartDxId = oSelectSmartDxId.nSmartDxID
                    If oSelectSmartDxId IsNot Nothing Then
                        oSelectSmartDxId.Dispose()
                        oSelectSmartDxId = Nothing
                    End If
                Else
                    If (dtSmartDxIDs.Rows(0)(0) IsNot Nothing) Then
                        nSmartDxId = dtSmartDxIDs.Rows(0)(0)
                    End If


                End If
            Else
                Return nSmartDxId

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return 0
        End Try
        Return nSmartDxId
    End Function

    '''' If Smart_Diagnosis already Exists
    Private Sub Load_Diagnosis()
        Dim dt As DataTable
        Dim dvICD9 As DataView
        '''' >>>>>>>>> To Insert ICD9 <<<<<<<<<  '''''

        '''' get Dignosised ICD9(s) for selected Exam
        dt = objclsSmartDiagnosis.ScanDiagnosis(m_ExamID, m_VisitID)


        If IsNothing(dt) = False Then
            dvICD9 = New DataView(dt)
            Dim strICD9(dt.Columns.Count - 1) As String

            For i As Integer = 0 To dt.Columns.Count - 1
                strICD9.SetValue(dt.Columns(i).ColumnName, i)
            Next
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            '            dt = New DataTable
            dt = dvICD9.ToTable(True, strICD9)


            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("nICDRevision") = 10 Then
                    RbICD9.Checked = False
                    rbICD10.Checked = True
                ElseIf dt.Rows(0)("nICDRevision") = 9 Then
                    RbICD9.Checked = True
                End If
                '' if there Exists Diagnosis
                Dim arrICD9 As New ArrayList
                Dim r As DataRow
                For Each r In dt.Rows
                    'arrICD9.Add(CType(r(0), String) & "-" & CType(r(1), String))
                    arrICD9.Add(CType(r(0), String))
                Next

                '   Dim ICD9Node As TreeNode
                Dim node As gloUserControlLibrary.myTreeNode
                Dim i As Int16
                Dim _node() As String


                Dim oSmartDx As smartDx = Nothing
                ''Using treeview control 
                For i = 0 To arrICD9.Count - 1
                    For Each node In GloUC_trvICD.Nodes
                        ''
                        _node = node.Text.Trim.Split("-")
                        If _node.Length > 1 Then
                            If Convert.ToString(_node.GetValue(0)).Trim() = CType(arrICD9(i), String).Trim() Then


                                ' If node.Text = CType(arrICD9(i), String) Then
                                ''code that works on tree view controls doublclick event   
                                ''AddNode
                                oSmartDx = New smartDx()
                                nSmartDxID = GetSelectedSmartDxID(node.ID, node.Text)
                                If nSmartDxID > -1 Then
                                    sSmartDXName = oSmartDx.getAssociatedSmartDxName(nSmartDxID)
                                    Dim mynode As New myTreeNode
                                    mynode.Key = node.ID
                                    mynode.Text = node.Text
                                    mynode.Tag = node.ICDRevision
                                    mynode.SmartDxKey = nSmartDxID
                                    trICD9Association.BeginUpdate()
                                    If Not IsNothing(mynode) Then
                                        AddNode(mynode)
                                    End If
                                    ''Sandip Darade 20090919
                                    'trICD9Association.SelectedNode.Checked = True

                                    '   Call Load_Treatment_new1(trICD9Association.SelectedNode)
                                    Call Load_Drugs(trICD9Association.SelectedNode)
                                    Call Load_PatientEducation(trICD9Association.SelectedNode)
                                    trICD9Association.EndUpdate()
                                    '   End If
                                End If



                            End If
                        End If
                        ''

                    Next
                Next
                arrICD9.Clear()
                arrICD9 = Nothing
                If oSmartDx IsNot Nothing Then
                    oSmartDx.Dispose()
                    oSmartDx = Nothing
                End If
            Else
                If _blnICDTransition = True Then

                    rbICD10.Checked = True
                Else
                    RbICD9.Checked = True
                End If
            End If
        Else
            If _blnICDTransition = True Then
                rbICD10.Checked = True
            Else
                RbICD9.Checked = True
            End If
        End If

        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If
        ' CheckAllParentNodes()
    End Sub

    Private Sub Load_Treatment(ByVal ICD9Node As myTreeNode)
        '   If IsFormLoading Then


        Dim obj As New ClsTreatmentDBLayer
        Dim dtCPT As DataTable
        Dim strICD9 As String()
        Dim strICD9code As String = ""
        Dim strICD9Desc As String = ""

        strICD9 = Split(ICD9Node.Name, "-", 2)

        strICD9code = strICD9.GetValue(0)
        strICD9Desc = strICD9.GetValue(1)
        '' To Get CPTs of the Selected ICD9
        dtCPT = obj.FetchCPTforUpdate(m_ExamID, strICD9code, strICD9Desc)

        'objclsSmartDiagnosis.dtTreatment = dtCPT
        obj.Dispose()
        obj = Nothing

        If IsNothing(dtCPT) = False Then
            If dtCPT.Rows.Count > 0 Then
                '' if there Exists Treatment (CPT)
                Dim arrCPT As New ArrayList
                Dim r As DataRow
                For Each r In dtCPT.Rows
                    arrCPT.Add(CType(r(0), String) & "-" & CType(r(1), String))
                Next

                ' Dim ICD9Node As TreeNode
                '   Dim Count As Int16
                Dim Count1 As Int16
                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "CPT" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = 0 Then
                        Dim CPTNode As TreeNode
                        ' CPTNode = trICD9Association.Nodes(0).Nodes(Count).Nodes(Count1)
                        For Each CPTNode In ICD9Node.Nodes(Count1).Nodes
                            CPTNode.Checked = False
                            Dim i As Int16
                            For i = 0 To arrCPT.Count - 1
                                If CPTNode.Text = CType(arrCPT(i), String) Then
                                    '' If ICD9COde-Discription Mathches with TreeNode then 
                                    '' then add that ICD9 to associated treeview
                                    If IsFormLoading = False Then
                                        If IsPresentICDinGrid(CPTNode) Then

                                            CPTNode.Checked = True
                                            Exit For
                                        Else
                                            _IsSkipCheckEvent = True
                                            CPTNode.Checked = True
                                            _IsSkipCheckEvent = False
                                            Exit For
                                        End If
                                    Else
                                        CPTNode.Checked = True
                                        Exit For
                                    End If


                                End If
                            Next
                        Next
                    End If
                Next
                '     End If
                '  Next
                ''''
                arrCPT.Clear()
                arrCPT = Nothing
            End If
        End If
        If Not IsNothing(dtCPT) Then
            dtCPT.Dispose()
            dtCPT = Nothing
        End If
        '  End If
    End Sub

    Private Function IsPresentICDinGrid(ByVal cptNode As myTreeNode)
        Dim ofnode As C1.Win.C1FlexGrid.Node
        Dim _isPresent As Boolean = False
        For i As Integer = 0 To C1Dignosis.Rows.Count - 1
            ofnode = C1Dignosis.Rows(i).Node
            If Not IsNothing(ofnode) Then

                If ofnode.Level = 1 Then
                    If Not IsNothing(cptNode.Parent) Then
                        If cptNode.Parent.Text = "CPT" Then
                            If Not IsNothing(cptNode.Parent.Parent) Then

                                If Convert.ToString(ofnode.Parent.Data).Trim = cptNode.Parent.Parent.Text.Trim Then
                                    _isPresent = True
                                End If
                            End If
                        End If

                    End If
                End If
            End If
        Next
        Return _isPresent
    End Function

    Private Sub Load_Drugs(ByVal ICD9Node As myTreeNode)
        Dim dtDrugs As DataTable
        dtDrugs = objclsSmartDiagnosis.FetchDrugsFromPrescription(m_VisitID, m_ExamDate)

        '' 20070630

        Dim Count1 As Int16
        If IsNothing(dtDrugs) = False Then
            If dtDrugs.Rows.Count > 0 Then
                '' if there Exists Treatment (CPT)
                Dim arrDrugs As New ArrayList
                Dim r As DataRow
                For Each r In dtDrugs.Rows
                    arrDrugs.Add(CType(r(0), String)) ''& "-" & CType(r(1), String))
                Next

                ' Dim ICD9Node As TreeNode
                ' Dim Count As Int16

                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "Drugs" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim DrugsNode As TreeNode

                        For Each DrugsNode In ICD9Node.Nodes(Count1).Nodes
                            DrugsNode.Checked = False
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
                arrDrugs.Clear()
                arrDrugs = Nothing
            Else
                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "Drugs" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim DrugsNode As TreeNode
                        For Each DrugsNode In ICD9Node.Nodes(Count1).Nodes
                            DrugsNode.Checked = False
                        Next
                    End If
                Next
            End If
        End If
        If Not IsNothing(dtDrugs) Then
            dtDrugs.Dispose()
            dtDrugs = Nothing
        End If
    End Sub

    Private Sub Load_PatientEducation(ByVal ICD9Node As myTreeNode)
        Dim dtPE As DataTable
        dtPE = objclsSmartDiagnosis.FetchPatientEducation(m_VisitID)
        Dim Count1 As Int16
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
                        arrPE.Add(lst)
                        lst = Nothing 'Change made to solve memory Leak and word crash issue
                    Next
                Next

                ''''''<><><><><><><><>
                arrPE = objclsSmartDiagnosis.GetEducationID(arrPE)
                ''''''<><><><><><><><>

                ' Dim ICD9Node As TreeNode
                'Dim Count As Int16

                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "Patient Education" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim PENode As TreeNode

                        For Each PENode In ICD9Node.Nodes(Count1).Nodes
                            Dim i As Int16
                            PENode.Checked = False
                            For i = 0 To arrPE.Count - 1
                                If PENode.Text = Trim(CType(arrPE(i), myList).Description) Then
                                    '' If ICD9COde-Discription Mathches with TreeNode then 
                                    '' then add that ICD9 to associated treeview
                                    PENode.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Next
            Else
                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "Patient Education" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim PENode As TreeNode
                        For Each PENode In ICD9Node.Nodes(Count1).Nodes
                            PENode.Checked = False
                        Next
                    End If
                Next
            End If
        End If
        If Not IsNothing(dtPE) Then
            dtPE.Dispose()
            dtPE = Nothing
        End If
    End Sub

    '' to Fill the Associates of Tags in ContaxMenu
    Private Sub FillTagsAssociates()
        Try
            Dim dtTags As DataTable
            dtTags = objclsSmartDiagnosis.GetAllCategory("Tags")
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
            If Not IsNothing(dtTags) Then
                dtTags.Dispose()
                dtTags = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '''' Event Handler for cntTags.Click
    Public Sub cntTags_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        Try
            If IsNothing(trICD9Association.SelectedNode) = False Then
                If IsNothing(oCurrentMenu) = False Then
                    Dim TagName As String
                    TagName = "[" & oCurrentMenu.Text() & "]"
                    '''' Insert Assocated Tag Name into the 'TemplateResult'   
                    CType(trICD9Association.SelectedNode, myTreeNode).TemplateResult = TagName
                    '''' Concat the Associated Tag with the Node so that User Can understand 
                    '''' which Tag is Assocated with it 
                    trICD9Association.SelectedNode.Text = CType(trICD9Association.SelectedNode, myTreeNode).NodeName & "-" & TagName
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub trICD9Association_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trICD9Association.AfterCheck
        ''Sandip Darade 20090921
        ''to maintain the check ,uncheck status of tree nodes  

        If bChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked)
        End If
        If bParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked)
        End If
        If IsFormLoading = False Then
            If _IsSkipCheckEvent Then
                Exit Sub
            End If
            Dim nMaxICD9Count As Integer = 0
            Dim ofNode As C1.Win.C1FlexGrid.Node
            Dim NewRow As Integer = 0
            Dim ChildRow As Integer = 0
            Dim arrstrConctCPT() As String
            Dim arrstrConctICD9() As String
            ' Dim arrstrMod() As String
            If Not IsNothing(e.Node.Parent) Then
                If e.Node.Parent.Text = "CPT" Then
                    If e.Node.Checked = True Then

                        '' FOR ICD9 DRIVEN ONLY ''
                        If gblnICD9Driven = True Then
                            With C1Dignosis
                                For i As Integer = 0 To .Rows.Count - 1
                                    ofNode = .Rows(i).Node

                                    If Not IsNothing(ofNode) Then
                                        If Not IsNothing(ofNode.Parent) Then
                                            If Convert.ToString(ofNode.Parent.Data).Trim = e.Node.Parent.Parent.Text.Trim Then
                                                If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = Convert.ToString(e.Node.Text) Then
                                                    ofNode = .Rows(i).Node
                                                    If ofNode.Children > 0 Then
                                                        If ofNode.Nodes(0).Level = 2 Then
                                                            Exit Sub
                                                        End If
                                                        For j As Integer = ofNode.Row.Index To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                            If Convert.ToString(.GetData(j, Col_ICD9Code_Description)) = Convert.ToString(e.Node.Parent.Parent.Text) Then
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
                                                            .Node.Image = Global.gloEMR.My.Resources.Resources.icd9
                                                            .Node.Data = e.Node ''
                                                        End With

                                                        ''arrstrConctCPT = Split(e.Node.Parent.Parent.Text, "-", 2)
                                                        ''arrstrConctICD9 = 
                                                        arrstrConctCPT = Split(e.Node.Text, "-", 2)
                                                        arrstrConctICD9 = Split(e.Node.Parent.Parent.Text, "-", 2)



                                                        .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                                        .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                                        .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                                        .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                                        .SetData(NewRow, Col_ICD9Count, .GetData(i, Col_ICD9Count))
                                                        .SetData(NewRow, Col_CPTCount, .GetData(i, Col_CPTCount) + 1)
                                                        .SetData(NewRow, Col_ICDRevision, e.Node.Parent.Parent.Tag)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If



                                Next
                                ''Add checked CPT node informationn to C1Diagnosis   as a child to selected ICD 

                                If .Rows.Count - 1 > 0 Then
                                    nMaxICD9Count = .GetData(.Rows.Count - 1, Col_ICD9Count)
                                Else
                                    nMaxICD9Count = 0
                                End If '

                                For i As Integer = 0 To .Rows.Count - 1
                                    If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = e.Node.Parent.Parent.Text Then
                                        nMaxICD9Count = .GetData(i, Col_ICD9Count)
                                        Exit For
                                    End If
                                Next
                                '  Dim ChildCount As Integer = 0 'Code changes For CAS-09339-L9X5J8
                                For i As Integer = 0 To .Rows.Count - 1
                                    If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = Convert.ToString(e.Node.Parent.Parent.Text) Then
                                        ofNode = .Rows(i).Node
                                        If ofNode.Children > 0 Then
                                            '  ChildCount = ofNode.Children 'Code changes For CAS-09339-L9X5J8
                                            For j As Integer = ofNode.Row.Index To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                If Convert.ToString(.GetData(j, Col_ICD9Code_Description)) = Convert.ToString(e.Node.Text) Then
                                                    Exit Sub
                                                End If
                                            Next
                                        End If
                                    End If
                                Next

                                Dim ParentRow As Integer = 0
                                For i As Integer = 0 To .Rows.Count - 1
                                    '''''''''''''
                                    If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = Convert.ToString(e.Node.Parent.Parent.Text) Then
                                        ParentRow = i
                                        ''added  for case CAS-22491-V7N1J0 ,CAS-22010-W4X9M5 smart DX issue
                                        Dim nc As Integer = e.Node.Parent.Parent.GetNodeCount(False)
                                        Dim txtNode As C1.Win.C1FlexGrid.Node
                                        txtNode = .Rows(i).Node
                                        ParentRow += txtNode.Children

                                        Exit For
                                    End If
                                Next

                                Dim oRow As C1.Win.C1FlexGrid.Row
                                If ParentRow = 0 Then
                                    .Rows.Add()
                                    NewRow = .Rows.Count - 1
                                Else
                                    '  oRow = .Rows.Insert(ParentRow + ChildCount + 1) 'Code changes For CAS-09339-L9X5J8
                                    ' oRow = .Rows.Insert(.Rows.Count) ''Code changes For CAS-21068-N3S9F4
                                    oRow = .Rows.Insert(ParentRow + 1)
                                    NewRow = oRow.Index
                                End If


                                With .Rows(NewRow)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 1
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                    .Node.Data = e.Node.Text
                                End With

                                ''Set data for checked cpt  in C1Diagnosis

                                arrstrConctICD9 = Split(e.Node.Parent.Parent.Text, "-", 2)
                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                .SetData(NewRow, Col_ICD9Count, nMaxICD9Count + 1)
                                .SetData(NewRow, Col_ICDRevision, e.Node.Parent.Parent.Tag)
                                '
                                arrstrConctCPT = Split(e.Node.Text, "-", 2)

                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                .SetData(NewRow, Col_ICD9Count, nMaxICD9Count)
                                .SetData(NewRow, Col_CPTCount, 1)
                                If e.Node.Nodes.Count > 0 Then
                                    Dim unitsnode As New myTreeNode
                                    unitsnode = e.Node.Nodes(0)
                                    .SetData(NewRow, Col_Units, unitsnode.Text.Replace("Units: ", ""))
                                    unitsnode = Nothing
                                Else
                                    .SetData(NewRow, Col_Units, 1)
                                End If


                                If e.Node.Nodes.Count > 1 Then

                                    '' ''Added by Mayuri :20150106-
                                    Dim unitsnode As New myTreeNode
                                    unitsnode = e.Node.Nodes(0)

                                    Dim modnode As myTreeNode
                                    Dim oldmodnode As myTreeNode
                                    Dim arrstrMod() As String
                                    If e.Node.Nodes(1).Nodes.Count > 0 Then
                                        Dim oRow1 As C1.Win.C1FlexGrid.Row
                                        If NewRow = 0 Then
                                            .Rows.Add()
                                            ChildRow = .Rows.Count - 1
                                        Else
                                            oRow1 = .Rows.Insert(NewRow + 1)
                                            ChildRow = oRow1.Index
                                        End If

                                        For k As Integer = 0 To e.Node.Nodes(1).Nodes.Count - 1

                                            If k > 0 Then

                                                ChildRow = ChildRow + 1
                                                C1Dignosis.Rows.Insert(ChildRow)
                                            End If
                                            '  NewRow = NewRow + 1
                                            With .Rows(ChildRow)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Image = Global.gloEMR.My.Resources.Resources.Modifier
                                                .Node.Data = e.Node.Nodes(1).Nodes(k).Text
                                            End With



                                            arrstrMod = Split(e.Node.Nodes(1).Nodes(k).Text, "-", 2)
                                            oldmodnode = New myTreeNode
                                            oldmodnode = e.Node.Nodes(1).Nodes(k)
                                            modnode = New myTreeNode
                                            modnode = e.Node.Nodes(1).Nodes(k)

                                            modnode.Key = oldmodnode.Key
                                            modnode.ModifierCode = oldmodnode.ModifierCode
                                            modnode.ModifierKey = oldmodnode.ModifierKey

                                            .SetData(ChildRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                            .SetData(ChildRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                            .SetData(ChildRow, Col_ICD9Count, nMaxICD9Count)
                                            .SetData(ChildRow, Col_ICDRevision, e.Node.Parent.Parent.Tag)

                                            .SetData(ChildRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                            .SetData(ChildRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))

                                            .SetData(ChildRow, Col_CPTCount, 1)


                                            .SetData(ChildRow, Col_ModCode, modnode.ModifierCode)
                                            .SetData(ChildRow, Col_ModDesc, arrstrMod.GetValue(1))
                                            .SetData(ChildRow, Col_ModCount, 1)

                                            .SetData(ChildRow, Col_Units, unitsnode.Text.Replace("Units: ", ""))

                                            modnode = Nothing
                                            oldmodnode = Nothing

                                        Next
                                        unitsnode = Nothing
                                    End If
                                End If


                                ''
                                Exit Sub
                            End With
                        End If


                    Else


                    End If
                End If
            End If

            '''''''''
            '' FOR CPT DRIVEN ''
            If IsFormload = False And gblnICD9Driven = False Then
                If Not IsNothing(e.Node.Parent) Then ''
                    If e.Node.Parent.Text = "CPT" Then
                        Dim _CPTCode, _CPTDescription, _CPTText, _ICD9Code, _ICD9Description, _ICD9Text As String
                        _CPTText = e.Node.Text
                        _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                        _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)
                        If e.Node.Nodes.Count > 0 Then

                        End If
                        If e.Node.Parent.Parent.Checked Then
                            _ICD9Text = CType(e.Node.Parent.Parent, myTreeNode).Text
                            _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-"))
                            _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1)
                        Else
                            _ICD9Code = ""
                            _ICD9Description = ""
                        End If

                        RemoveCPTICD9(_CPTCode, _CPTDescription, _ICD9Code, _ICD9Description, True)

                    ElseIf e.Node.Parent.Text = "Diagnosis" Then

                        Dim _CPTCode, _CPTDescription, _CPTText, _ICD9Code, _ICD9Description, _ICD9Text, _unit As String
                        Dim _isCPTFound As Boolean = False

                        _ICD9Text = CType(e.Node, myTreeNode).Text
                        _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-"))
                        _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1)

                        For Each oCPTNode As myTreeNode In e.Node.Nodes
                            If oCPTNode.Text = "CPT" Then
                                For Each oCPT As myTreeNode In oCPTNode.Nodes
                                    If oCPT.Checked Then
                                        If oCPT.Nodes.Count > 0 Then
                                            _unit = oCPT.Nodes(0).Text.Replace("Units: ", "")
                                        End If
                                        'If oCPT.Nodes.Count > 1 Then
                                        '    For Each oModNode As myTreeNode In oCPT.Nodes(1).Nodes
                                        '        _modCode = oModNode.ModifierCode
                                        '        _modDesc = oModNode.Text
                                        '        Dim _olistmod As New gloEMRGeneralLibrary.gloGeneral.oListModifier()
                                        '        _olistmod.ModCode = _modCode
                                        '        _olistmod.ModDesc = _modDesc

                                        '    Next
                                        'End If
                                        _isCPTFound = True
                                        _CPTText = oCPT.Text
                                        _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                                        _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)

                                        RemoveCPTICD9(_CPTCode, _CPTDescription, _ICD9Code, _ICD9Description, False)
                                    End If
                                Next
                            End If
                        Next

                        If _isCPTFound = False Then
                            RemoveCPTICD9("", "", _ICD9Code, _ICD9Description, False)
                        End If
                    End If
                End If
            End If


            '''''''''
        End If
        'end Sandip Darade 20090921



    End Sub

    Private Sub trICD9Association_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trICD9Association.DragOver
        Try
            If IsNothing(trICD9Association.SelectedNode) = True Then
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

            e.Effect = DragDropEffects.Move
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '    Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SaveAssociation()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Dim clsSmartDx As New gloSmartDx.Core.SmartDx()
    Dim clsSmartDxDisplayList As New gloUIControlLibrary.Classes.SmartDX.clsSmartDX()
    Dim isTreatmentGiven As Boolean = False
    'Dim arrexam As ArrayList
    'Dim arrDruglist As ArrayList
    'Dim arrFlow As ArrayList
    'Dim arrLabs As ArrayList
    'Dim arrRadiology As ArrayList
    'Dim arrTemplate As ArrayList
    'Dim arrPE As ArrayList

    Private Sub FillSmartDxCommanCPTList(ByVal ICDId As Long)
        Dim CPTTreatmentNode As New myTreeNode
        Dim CPTModifierNode As New myTreeNode
        Dim CPTUnitNode As New myTreeNode
        Dim clsSmartCpt As gloSmartDx.Core.CPT = Nothing
        Dim clsSmartCptModifier As gloSmartDx.Core.CPTModifiers = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        Try
            If trICD9Association.Nodes(0).GetNodeCount(False) > 0 Then
                For i As Integer = 0 To trICD9Association.Nodes(0).GetNodeCount(False) - 1
                    CPTTreatmentNode = trICD9Association.Nodes(0).Nodes(i)
                    If CPTTreatmentNode.Checked Then
                        Dim strCpt() As String
                        strCpt = Split(CPTTreatmentNode.Name, "-", 2)
                        Dim strCptCode As String = ""
                        Dim strCptDesc As String = ""
                        strCptCode = strCpt.GetValue(0).ToString.Trim()
                        strCptDesc = strCpt.GetValue(1).ToString.Trim()
                        Dim myCpt As gloSmartDx.Core.CPT = (From column In clsSmartDx.smartCPTCodes
                                                            Where column.CPTCode = Convert.ToString(strCptCode) And
                                                                  column.ICDId = Convert.ToString(ICDId)).FirstOrDefault()
                        If IsNothing(myCpt) Then
                            clsSmartCpt = New gloSmartDx.Core.CPT()
                            clsSmartCpt.CPTId = CPTTreatmentNode.Key
                            clsSmartCpt.ICDId = ICDId
                            clsSmartCpt.CPTCode = strCptCode
                            clsSmartCpt.CPTDescription = strCptDesc
                            clsSmartCpt.CPTUnit = CPTTreatmentNode.FirstNode.Text.Split(": ")(1)
                            If CPTTreatmentNode.LastNode.GetNodeCount(False) > 0 Then
                                For j As Integer = 0 To CPTTreatmentNode.LastNode.GetNodeCount(False) - 1
                                    CPTModifierNode = CPTTreatmentNode.LastNode.Nodes(j)
                                    clsSmartCptModifier = New gloSmartDx.Core.CPTModifiers()
                                    clsSmartCptModifier.ICDId = ICDId
                                    clsSmartCptModifier.CPTId = CPTTreatmentNode.Key
                                    clsSmartCptModifier.CPTModifierCode = CPTModifierNode.Tag
                                    clsSmartCptModifier.CPTModifierDescription = CPTModifierNode.Text
                                    clsSmartCpt.CPTModifiers.Add(clsSmartCptModifier)
                                    clsSmartCptModifier.Dispose()
                                    clsSmartCptModifier = Nothing
                                Next
                            End If
                            If gblnICD9Driven Then
                                If Not IsNothing(clsSmartDx.smartICDCodes) Then
                                    clsSmartCpt.CPTLineNo = clsSmartDx.smartICDCodes.Count + 1
                                Else
                                    clsSmartCpt.CPTLineNo = 1
                                End If
                            Else
                                If Not IsNothing(clsSmartDx.smartCPTCodes) Then
                                    clsSmartCpt.CPTLineNo = clsSmartDx.smartCPTCodes.Count + 1
                                Else
                                    clsSmartCpt.CPTLineNo = 1
                                End If
                            End If

                            clsSmartDx.smartCPTCodes.Add(clsSmartCpt)
                            clsSmartCpt.Dispose()
                            clsSmartCpt = Nothing

                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                                Where column.DisplayName = Convert.ToString(CPTTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = CPTTreatmentNode.Name
                            clsSmartDxDisplay.Id = CPTTreatmentNode.Key
                            clsSmartDxDisplay.ICDId = ICDId
                            clsSmartDxDisplay.Type = "CPT"
                            clsSmartDxDisplay.SortId = 2
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If

                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(CPTTreatmentNode) Then
                CPTTreatmentNode.Dispose()
                CPTTreatmentNode = Nothing
            End If

            If Not IsNothing(CPTModifierNode) Then
                CPTModifierNode.Dispose()
                CPTModifierNode = Nothing
            End If

            If Not IsNothing(CPTUnitNode) Then
                CPTUnitNode.Dispose()
                CPTUnitNode = Nothing
            End If



        End Try
    End Sub

    Private Sub FillSmartDxCPTList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim CPTModifierNode As New myTreeNode
        Dim CPTUnitNode As New myTreeNode

        Dim clsSmartCpt As gloSmartDx.Core.CPT = Nothing
        Dim clsSmartCptModifier As gloSmartDx.Core.CPTModifiers = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing

        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim strCpt() As String
                        strCpt = Split(ICDTreatmentNode.Name, "-", 2)
                        Dim strCptCode As String = ""
                        Dim strCptDesc As String = ""
                        strCptCode = strCpt.GetValue(0).ToString.Trim()
                        strCptDesc = strCpt.GetValue(1).ToString.Trim()
                        Dim myCpt As gloSmartDx.Core.CPT = (From column In clsSmartDx.smartCPTCodes
                                                            Where column.CPTCode = Convert.ToString(strCptCode) And
                                                                  column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        '
                        If IsNothing(myCpt) Then
                            clsSmartCpt = New gloSmartDx.Core.CPT()
                            clsSmartCpt.CPTId = ICDTreatmentNode.Key
                            clsSmartCpt.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartCpt.CPTCode = strCptCode
                            clsSmartCpt.CPTDescription = strCptDesc
                            clsSmartCpt.CPTUnit = ICDTreatmentNode.FirstNode.Text.Split(": ")(1)

                            If ICDTreatmentNode.LastNode.GetNodeCount(False) > 0 Then
                                For j As Integer = 0 To ICDTreatmentNode.LastNode.GetNodeCount(False) - 1
                                    CPTModifierNode = ICDTreatmentNode.LastNode.Nodes(j)
                                    clsSmartCptModifier = New gloSmartDx.Core.CPTModifiers()
                                    clsSmartCptModifier.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                                    clsSmartCptModifier.CPTId = ICDTreatmentNode.Key
                                    clsSmartCptModifier.CPTModifierCode = CPTModifierNode.Tag
                                    clsSmartCptModifier.CPTModifierDescription = CPTModifierNode.Text
                                    clsSmartCpt.CPTModifiers.Add(clsSmartCptModifier)
                                    clsSmartCptModifier.Dispose()
                                    clsSmartCptModifier = Nothing
                                Next
                            End If
                            If gblnICD9Driven Then
                                If Not IsNothing(clsSmartDx.smartICDCodes) Then
                                    clsSmartCpt.CPTLineNo = clsSmartDx.smartICDCodes.Count + 1
                                Else
                                    clsSmartCpt.CPTLineNo = 1
                                End If
                            Else
                                If Not IsNothing(clsSmartDx.smartCPTCodes) Then
                                    clsSmartCpt.CPTLineNo = clsSmartDx.smartCPTCodes.Count + 1
                                Else
                                    clsSmartCpt.CPTLineNo = 1
                                End If
                            End If

                            clsSmartDx.smartCPTCodes.Add(clsSmartCpt)
                            clsSmartCpt.Dispose()
                            clsSmartCpt = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                            Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Name
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Key
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "CPT"
                            clsSmartDxDisplay.SortId = 2
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If

                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If


            If Not IsNothing(CPTModifierNode) Then
                CPTModifierNode.Dispose()
                CPTModifierNode = Nothing
            End If

            If Not IsNothing(CPTUnitNode) Then
                CPTUnitNode.Dispose()
                CPTUnitNode = Nothing
            End If

        End Try
    End Sub

    Private Sub FillSmartDxDrugList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartDrugs As gloSmartDx.Core.Drugs = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        'arrDruglist = New ArrayList()
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myDrug As gloSmartDx.Core.Drugs = (From column In clsSmartDx.smartDrugs
                                                               Where Convert.ToInt64(column.DrugsId) = Convert.ToInt64(ICDTreatmentNode.Key) And
                                                                     column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        If IsNothing(myDrug) Then
                            clsSmartDrugs = New gloSmartDx.Core.Drugs()
                            clsSmartDrugs.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDrugs.DrugsId = ICDTreatmentNode.Key
                            clsSmartDrugs.DrugsName = ICDTreatmentNode.DrugName
                            clsSmartDrugs.Dosage = ICDTreatmentNode.Dosage
                            clsSmartDrugs.DrugForm = ICDTreatmentNode.DrugForm
                            clsSmartDrugs.Route = ICDTreatmentNode.Route
                            clsSmartDrugs.Frequency = ICDTreatmentNode.Frequency
                            clsSmartDrugs.NDCCode = ICDTreatmentNode.NDCCode
                            clsSmartDrugs.IsNarcotics = ICDTreatmentNode.IsNarcotics
                            clsSmartDrugs.Duration = ICDTreatmentNode.Duration
                            clsSmartDrugs.DrugQtyQualifier = ICDTreatmentNode.DrugQtyQualifier
                            clsSmartDx.smartDrugs.Add(clsSmartDrugs)

                            'Dim oDrug As New gloEMRActors.Drug()
                            'oDrug.DrugsID = clsSmartDrugs.DrugsId
                            Dim _strDrugNM As String = ""

                            _strDrugNM = clsSmartDrugs.DrugsName
                           
                            'oDrug.DrugsName = clsSmartDrugs.DrugsName
                            'oDrug.Dosage = clsSmartDrugs.Dosage
                            'oDrug.DrugForm = clsSmartDrugs.DrugForm
                            'oDrug.Route = clsSmartDrugs.Route
                            'oDrug.Frequency = clsSmartDrugs.Frequency
                            'oDrug.NDCCode = clsSmartDrugs.NDCCode
                            'oDrug.IsNarcotics = clsSmartDrugs.IsNarcotics
                            'oDrug.Duration = clsSmartDrugs.Duration
                            'oDrug.nddid = clsSmartDrugs.nddid
                            'oDrug.DrugQtyQualifier = clsSmartDrugs.DrugQtyQualifier
                            'arrDruglist.Add(oDrug)
                            'oDrug = Nothing

                            clsSmartDrugs.Dispose()
                            clsSmartDrugs = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                          Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = _strDrugNM
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Key
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Drugs"
                            clsSmartDxDisplay.SortId = 3
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartDxPEList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartPatientEducation As gloSmartDx.Core.PatientEducation = Nothing
        'Dim clsSmartDxDiplay As gloSmartDx.Core.SmartDxDisplay = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myPE As gloSmartDx.Core.PatientEducation = (From column In clsSmartDx.smartPatientEducation
                                                                        Where Convert.ToInt64(column.EducationTemplateID) = Convert.ToInt64(ICDTreatmentNode.Key) And
                                                                             column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        If IsNothing(myPE) Then
                            clsSmartPatientEducation = New gloSmartDx.Core.PatientEducation()
                            clsSmartPatientEducation.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartPatientEducation.EducationTemplateID = ICDTreatmentNode.Key
                            clsSmartPatientEducation.EducationTemplateName = ICDTreatmentNode.Name
                            clsSmartPatientEducation.EducationIndex = ICDTreatmentNode.Key
                            clsSmartPatientEducation.EducationHistoryCategory = ICDTreatmentNode.NodeName
                            clsSmartDx.smartPatientEducation.Add(clsSmartPatientEducation)

                            'Dim lstmyPE As New myList
                            'lstmyPE.ID = clsSmartPatientEducation.EducationTemplateID
                            'lstmyPE.Description = clsSmartPatientEducation.EducationTemplateName
                            'lstmyPE.Index = clsSmartPatientEducation.EducationIndex
                            'lstmyPE.HistoryCategory = clsSmartPatientEducation.EducationHistoryCategory
                            'arrPE.Add(lstmyPE)
                            'lstmyPE = Nothing

                            clsSmartPatientEducation.Dispose()
                            clsSmartPatientEducation = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                           Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Name
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Key
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Patient Education"
                            clsSmartDxDisplay.SortId = 7
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            '    clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartDxOrdersList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartOrders As gloSmartDx.Core.Orders = Nothing
        'Dim clsSmartDxDiplay As gloSmartDx.Core.SmartDxDisplay = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        'arrLabs = New ArrayList()
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myOrder As gloSmartDx.Core.Orders = Nothing

                        myOrder = (From column In clsSmartDx.smartOrders
                                   Where Convert.ToInt64(column.TestId) = Convert.ToInt64(ICDTreatmentNode.Tag) And
                                         column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        If IsNothing(myOrder) Then
                            clsSmartOrders = New gloSmartDx.Core.Orders()
                            clsSmartOrders.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartOrders.ICDName = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Text)
                            clsSmartOrders.TestName = ICDTreatmentNode.Text
                            clsSmartOrders.TestId = ICDTreatmentNode.Tag
                            clsSmartDx.smartOrders.Add(clsSmartOrders)


                            'Dim lstmyOrders As New gloEmdeonCommon.myList
                            'lstmyOrders.Value = clsSmartOrders.TestName
                            'lstmyOrders.ID = clsSmartOrders.TestId
                            'arrLabs.Add(lstmyOrders)
                            'lstmyOrders = Nothing

                            clsSmartOrders.Dispose()
                            clsSmartOrders = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                          Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Text
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Tag
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Orders and Results"
                            clsSmartDxDisplay.SortId = 5
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartDxOrderTemplatesList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartOrderTemplates As gloSmartDx.Core.OrderTemplates = Nothing
        'Dim clsSmartDxDiplay As gloSmartDx.Core.SmartDxDisplay = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        'arrRadiology = New ArrayList()
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myOrderTemplate As gloSmartDx.Core.OrderTemplates = (From column In clsSmartDx.smartOrderTemplate
                                                                                 Where Convert.ToInt64(column.OrderTemplateID) = Convert.ToInt64(ICDTreatmentNode.Tag) And
                                                                                       column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        If IsNothing(myOrderTemplate) Then
                            clsSmartOrderTemplates = New gloSmartDx.Core.OrderTemplates()
                            clsSmartOrderTemplates.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartOrderTemplates.OrderTemplateName = ICDTreatmentNode.Text
                            clsSmartOrderTemplates.OrderTemplateID = ICDTreatmentNode.Tag
                            clsSmartDx.smartOrderTemplate.Add(clsSmartOrderTemplates)

                            'Dim lstmyOrdersTemplates As New myList
                            'lstmyOrdersTemplates.Value = clsSmartOrderTemplates.OrderTemplateName
                            'lstmyOrdersTemplates.Index = clsSmartOrderTemplates.OrderTemplateID
                            'arrRadiology.Add(lstmyOrdersTemplates)
                            'lstmyOrdersTemplates = Nothing


                            clsSmartOrderTemplates.Dispose()
                            clsSmartOrderTemplates = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                          Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Text
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Tag
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Order Templates"
                            clsSmartDxDisplay.SortId = 6
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartDxTagList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartTags As gloSmartDx.Core.Tags = Nothing
        'Dim clsSmartDxDiplay As gloSmartDx.Core.SmartDxDisplay = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myTag As gloSmartDx.Core.Tags = Nothing
                        If Not IsNothing(ICDTreatmentNode.Key) Then
                            myTag = (From column In clsSmartDx.smartTags
                                     Where Convert.ToInt64(column.TagTemplateID) = Convert.ToInt64(ICDTreatmentNode.Key) And
                                           column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        End If
                        If IsNothing(myTag) Then
                            clsSmartTags = New gloSmartDx.Core.Tags()
                            clsSmartTags.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartTags.TagTemplateID = ICDTreatmentNode.Key
                            clsSmartTags.TagTemplateName = ICDTreatmentNode.Text
                            clsSmartTags.TagTemplateResult = ICDTreatmentNode.TemplateResult
                            clsSmartDx.smartTags.Add(clsSmartTags)

                            'Dim lstTags As New myList
                            'lstTags.ID = 4
                            'lstTags.Description = ICDTreatmentNode.Text  '' TemplateName
                            'lstTags.Index = ICDTreatmentNode.Key  '' TemplateID
                            'If ICDTreatmentNode.Text <> "" Then
                            '    Dim strTags As String = ICDTreatmentNode.Text.ToString()
                            '    Dim ind As Integer = strTags.LastIndexOf("-")
                            '    If ind > -1 Then
                            '        lstTags.HistoryItem = strTags.Substring(ind + 1)
                            '    Else
                            '        lstTags.HistoryItem = ""
                            '    End If

                            'ElseIf IsNothing(ICDTreatmentNode.TemplateResult) = True Then
                            '    lstTags.HistoryItem = ""
                            'Else
                            '    lstTags.HistoryItem = ICDTreatmentNode.TemplateResult.ToString
                            'End If
                            'lstTags.HistoryCategory = ICDTreatmentNode.NodeName
                            'frmPatientExam.arrTagID.Add(lstTags)




                            clsSmartTags.Dispose()
                            clsSmartTags = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                         Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Text
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Key
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Tags"
                            clsSmartDxDisplay.SortId = 9
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartDxReferralList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartReferralLetters As gloSmartDx.Core.ReferralLetter = Nothing
        '  Dim clsSmartDxDiplay As gloSmartDx.Core.SmartDxDisplay = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        'arrTemplate = New ArrayList()
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myReferral As gloSmartDx.Core.ReferralLetter = (From column In clsSmartDx.smartReferralLetter
                                                                            Where Convert.ToInt64(column.ReferralLetterID) = Convert.ToInt64(ICDTreatmentNode.Tag) And
                                                                                  column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        If IsNothing(myReferral) Then
                            clsSmartReferralLetters = New gloSmartDx.Core.ReferralLetter()
                            clsSmartReferralLetters.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartReferralLetters.ReferralLetterName = ICDTreatmentNode.Text
                            clsSmartReferralLetters.ReferralLetterID = ICDTreatmentNode.Tag
                            clsSmartDx.smartReferralLetter.Add(clsSmartReferralLetters)

                            'Dim lstmyReferralLetter As New myList
                            'lstmyReferralLetter.Value = clsSmartReferralLetters.ReferralLetterName
                            'lstmyReferralLetter.Index = clsSmartReferralLetters.ReferralLetterID
                            'arrTemplate.Add(lstmyReferralLetter)
                            'lstmyReferralLetter = Nothing

                            clsSmartReferralLetters.Dispose()
                            clsSmartReferralLetters = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                       Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Text
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Tag
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Referral Letter"
                            clsSmartDxDisplay.SortId = 8
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Sub FillSmartDxFlowsheetList(ByVal TreatmentNode As myTreeNode)
        Dim ICDTreatmentNode As New myTreeNode
        Dim clsSmartFlowsheet As gloSmartDx.Core.Flowsheet = Nothing
        ' Dim clsSmartDxDiplay As gloSmartDx.Core.SmartDxDisplay = Nothing
        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing
        'arrFlow = New ArrayList()
        Try
            If TreatmentNode.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To TreatmentNode.GetNodeCount(False) - 1
                    ICDTreatmentNode = TreatmentNode.Nodes(i)
                    If ICDTreatmentNode.Checked Then
                        Dim myFlowsheet As gloSmartDx.Core.Flowsheet = (From column In clsSmartDx.smartFlowsheet
                                                                        Where Convert.ToInt64(column.FlowsheetID) = Convert.ToInt64(ICDTreatmentNode.Tag) And
                                                                              column.ICDId = Convert.ToString(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)).FirstOrDefault()
                        If IsNothing(myFlowsheet) Then
                            clsSmartFlowsheet = New gloSmartDx.Core.Flowsheet()
                            clsSmartFlowsheet.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartFlowsheet.FlowsheetName = ICDTreatmentNode.Text
                            clsSmartFlowsheet.FlowsheetID = ICDTreatmentNode.Tag
                            clsSmartDx.smartFlowsheet.Add(clsSmartFlowsheet)

                            'Dim lstmyFlowsheet As New myList
                            'lstmyFlowsheet.Value = clsSmartFlowsheet.FlowsheetName
                            'lstmyFlowsheet.Index = clsSmartFlowsheet.FlowsheetID
                            'arrFlow.Add(lstmyFlowsheet)

                            clsSmartFlowsheet.Dispose()
                            clsSmartFlowsheet = Nothing
                            'Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                            '                      Where column.DisplayName = Convert.ToString(ICDTreatmentNode.Name)).FirstOrDefault()
                            'If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICDTreatmentNode.Text
                            clsSmartDxDisplay.Id = ICDTreatmentNode.Tag
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(DirectCast(TreatmentNode.Parent, gloEMR.myTreeNode).Key)
                            clsSmartDxDisplay.Type = "Flowsheet"
                            clsSmartDxDisplay.SortId = 4
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                            'End If
                        End If
                        isTreatmentGiven = True
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(ICDTreatmentNode) Then
                ICDTreatmentNode.Dispose()
                ICDTreatmentNode = Nothing
            End If
        End Try
    End Sub

    Private Function IsEnableReviewScreen() As Boolean

        Dim objSettings As New clsSettings
        Dim _isEnableReviewScreen As Boolean = False

        Try

            objSettings.GetSettings()

            If IsNothing(objSettings.EnableSamrtDxReviewScreen) = False Then
                _isEnableReviewScreen = objSettings.EnableSamrtDxReviewScreen
            Else
                _isEnableReviewScreen = True
            End If

        Catch ex As Exception
            _isEnableReviewScreen = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try

        Return _isEnableReviewScreen

    End Function

    Private Function ShowSmartReview() As Boolean

        Dim clsSmartIcd As gloSmartDx.Core.ICD = Nothing
        Dim ICD9Node As New myTreeNode
        Dim ICDTreatmentTypeNode As New myTreeNode
        Dim clsDiagnosis As New ClsDiagnosisDBLayer
        'arrexam = New ArrayList 'arraylist which has ICD9send to exam
        Dim dlgResultReviewScren As Boolean = True

        Try

            If trICD9Association.Nodes(1).GetNodeCount(False) > 0 Then
                clsSmartDx.smartICDCodes = New List(Of gloSmartDx.Core.ICD)
                clsSmartDx.smartCPTCodes = New List(Of gloSmartDx.Core.CPT)
                clsSmartDx.smartCPTModifiers = New List(Of gloSmartDx.Core.CPTModifiers)
                clsSmartDx.smartDrugs = New List(Of gloSmartDx.Core.Drugs)
                clsSmartDx.smartPatientEducation = New List(Of gloSmartDx.Core.PatientEducation)
                clsSmartDx.smartOrders = New List(Of gloSmartDx.Core.Orders)
                clsSmartDx.smartOrderTemplate = New List(Of gloSmartDx.Core.OrderTemplates)
                clsSmartDx.smartTags = New List(Of gloSmartDx.Core.Tags)
                clsSmartDx.smartReferralLetter = New List(Of gloSmartDx.Core.ReferralLetter)
                clsSmartDx.smartFlowsheet = New List(Of gloSmartDx.Core.Flowsheet)
                clsSmartDxDisplayList.TreatmentList = New ObservableCollection(Of gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay)

                For i As Integer = 0 To trICD9Association.Nodes(1).GetNodeCount(False) - 1
                    ICD9Node = trICD9Association.Nodes(1).Nodes(i)
                    If ICD9Node.GetNodeCount(False) > 0 Then
                        isTreatmentGiven = False
                        For j As Integer = 0 To ICD9Node.GetNodeCount(False) - 1
                            ICDTreatmentTypeNode = ICD9Node.Nodes(j)
                            If ICDTreatmentTypeNode.GetNodeCount(False) > 0 Then
                                If ICDTreatmentTypeNode.Text = "CPT" Then
                                    FillSmartDxCPTList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Drugs" Then
                                    FillSmartDxDrugList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Patient Education" Then
                                    FillSmartDxPEList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Tags" Then
                                    FillSmartDxTagList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Flowsheet" Then
                                    FillSmartDxFlowsheetList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Orders and Results" Then
                                    FillSmartDxOrdersList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Order Templates" Then
                                    FillSmartDxOrderTemplatesList(ICDTreatmentTypeNode)
                                ElseIf ICDTreatmentTypeNode.Text = "Referral Letter" Then
                                    FillSmartDxReferralList(ICDTreatmentTypeNode)
                                End If
                            End If
                        Next
                        'If isTreatmentGiven = True Then
                        FillSmartDxCommanCPTList(ICD9Node.Key)
                        isTreatmentGiven = False
                        Dim strICD9() As String
                        strICD9 = Split(ICD9Node.Name, "-", 2)
                        Dim strICD9Code As String = ""
                        Dim strICD9Desc As String = ""
                        Dim strSnomedCode As String = ""
                        Dim strSnomedDesc As String = ""
                        strICD9Code = strICD9.GetValue(0).ToString.Trim()
                        strICD9Desc = strICD9.GetValue(1).ToString.Trim()

                        Dim htSno As New Hashtable()
                        Dim blnOneSnoMed As Boolean

                        htSno = clsDiagnosis.GetDefaultSnomed(strICD9Code, strICD9Desc, ICD9Node.Tag, blnOneSnoMed, Me)
                        If Not IsNothing(htSno) Then
                            Dim key As ICollection = htSno.Keys
                            Dim k As String
                            For Each k In key
                                strSnomedCode = k
                                strSnomedDesc = htSno(k)
                            Next k
                        End If
                        htSno = Nothing
                        Dim clsSmartDxDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = Nothing

                        clsSmartIcd = New gloSmartDx.Core.ICD()
                        clsSmartIcd.ICDCode = strICD9Code
                        clsSmartIcd.ICDDescription = strICD9Desc
                        clsSmartIcd.ICDId = ICD9Node.Key
                        clsSmartIcd.ICDRevision = ICD9Node.Tag
                        clsSmartIcd.ICDSnomedCode = strSnomedCode
                        clsSmartIcd.ICDSnomedDescription = strSnomedDesc
                        clsSmartIcd.IsSnomedOneToOne = blnOneSnoMed
                        clsSmartDx.smartICDCodes.Add(clsSmartIcd)
                        clsSmartIcd.Dispose()
                        clsSmartIcd = Nothing

                        Dim myCptDisplay As gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay = (From column In clsSmartDxDisplayList.TreatmentList
                                                       Where column.DisplayName = Convert.ToString(ICD9Node.Name)).FirstOrDefault()
                        If IsNothing(myCptDisplay) Then
                            clsSmartDxDisplay = New gloUIControlLibrary.Classes.SmartDX.clsSmartDXDisplay()
                            clsSmartDxDisplay.DisplayName = ICD9Node.Name
                            clsSmartDxDisplay.Id = Convert.ToInt64(ICD9Node.Key)
                            clsSmartDxDisplay.ICDId = Convert.ToInt64(ICD9Node.Key)
                            If ICD9Node.Tag = 10 Then
                                clsSmartDxDisplay.Type = "ICD-10"
                            ElseIf ICD9Node.Tag = 9 Then
                                clsSmartDxDisplay.Type = "ICD-9"
                            Else
                                clsSmartDxDisplay.Type = "ICD-9"
                            End If
                            clsSmartDxDisplay.SortId = 1
                            clsSmartDxDisplayList.TreatmentList.Add(clsSmartDxDisplay)
                            clsSmartDxDisplay.Dispose()
                            clsSmartDxDisplay = Nothing
                        End If

                        'arrexam.Add(New mytable(strICD9Desc, strICD9Code))

                        'End If
                    End If
                Next


                If Not IsNothing(clsSmartDxDisplayList.TreatmentList) Then
                    If clsSmartDxDisplayList.TreatmentList.Count > 0 Then
                        Dim objReview As New gloUIControlLibrary.WPFForms.frmReviewSmartDxSelection(clsSmartDxDisplayList)
                        Dim _interophelper As New System.Windows.Interop.WindowInteropHelper(objReview)
                        _interophelper.Owner = Me.Handle
                        objReview.ShowDialog()
                        dlgResultReviewScren = objReview.DialogResult
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(clsDiagnosis) Then
                clsDiagnosis.Dispose()
                clsDiagnosis = Nothing
            End If
            If Not IsNothing(clsSmartIcd) Then
                clsSmartIcd.Dispose()
                clsSmartIcd = Nothing
            End If
            If Not IsNothing(ICD9Node) Then
                ICD9Node.Dispose()
                ICD9Node = Nothing
            End If
            If Not IsNothing(ICDTreatmentTypeNode) Then
                ICDTreatmentTypeNode.Dispose()
                ICDTreatmentTypeNode = Nothing
            End If

        End Try

        Return dlgResultReviewScren
    End Function

    Private Function CreateExamIcd9Datatable() As DataTable
        Dim dtSmartDx As New DataTable()
        dtSmartDx.Columns.Add("nPatientID")
        dtSmartDx.Columns.Add("nExamID")
        dtSmartDx.Columns.Add("nVisitID")
        dtSmartDx.Columns.Add("sIcdCode")
        dtSmartDx.Columns.Add("sIcdDesc")
        dtSmartDx.Columns.Add("sCptCode")
        dtSmartDx.Columns.Add("sCptDesc")
        dtSmartDx.Columns.Add("sModCode")
        dtSmartDx.Columns.Add("sModDesc")
        dtSmartDx.Columns.Add("nUnit")
        dtSmartDx.Columns.Add("nLineNo")
        dtSmartDx.Columns.Add("sSnomedCode")
        dtSmartDx.Columns.Add("sSnomedDesc")
        dtSmartDx.Columns.Add("nIcdRevision")
        dtSmartDx.Columns.Add("bisOneToOneMap")

        Return dtSmartDx
    End Function

    Private Sub AddRowExamIcd9Datatable(ByRef dtSmartDx As DataTable, ByVal sIcdCode As String, ByVal sIcdDesc As String, ByVal sCptCode As String, ByVal sCptDesc As String, ByVal sModCode As String, ByVal sModDesc As String,
                                             ByVal nUnit As String, ByVal nLineNo As String, ByVal sSnomedCode As String, ByVal sSnomedDesc As String, ByVal nIcdRevision As Integer, ByVal bisOneToOneMap As Boolean)
        Dim drSamrtDx As DataRow = dtSmartDx.NewRow()
        drSamrtDx("nPatientID") = _PatientID
        drSamrtDx("nExamID") = m_ExamID
        drSamrtDx("nVisitID") = m_VisitID
        drSamrtDx("sIcdCode") = sIcdCode
        drSamrtDx("sIcdDesc") = sIcdDesc
        drSamrtDx("sCptCode") = sCptCode
        drSamrtDx("sCptDesc") = sCptDesc
        drSamrtDx("sModCode") = sModCode
        drSamrtDx("sModDesc") = sModDesc
        drSamrtDx("nUnit") = nUnit
        drSamrtDx("nLineNo") = nLineNo
        drSamrtDx("sSnomedCode") = sSnomedCode
        drSamrtDx("sSnomedDesc") = sSnomedDesc
        drSamrtDx("nIcdRevision") = nIcdRevision
        drSamrtDx("bisOneToOneMap") = bisOneToOneMap
        dtSmartDx.Rows.Add(drSamrtDx)
        drSamrtDx = Nothing
    End Sub

    Private Sub SaveExamICD9CPT()
        Dim nICDId As Long
        Dim nCPTId As Long
        Dim sIcdCode As String = ""
        Dim sIcdDesc As String = ""
        Dim sCptCode As String = ""
        Dim sCptDesc As String = ""
        Dim sModCode As String = ""
        Dim sModDesc As String = ""
        Dim nUnit As Double = 0
        Dim nLineNo As Int16 = 0
        Dim sSnomedCode As String = ""
        Dim sSnomedDesc As String = ""
        Dim nIcdRevision As Int16 = 0
        Dim bisOneToOneMap As Boolean
        Dim dtSmartDx As DataTable = Nothing
        Dim clsDiagnosis As New ClsDiagnosisDBLayer
        Try
            dtSmartDx = CreateExamIcd9Datatable()
            For Each IcdCode As gloSmartDx.Core.ICD In clsSmartDx.smartICDCodes
                nICDId = IcdCode.ICDId
                sIcdCode = IcdCode.ICDCode
                sIcdDesc = IcdCode.ICDDescription
                nIcdRevision = IcdCode.ICDRevision
                sSnomedCode = IcdCode.ICDSnomedCode
                sSnomedDesc = IcdCode.ICDSnomedDescription
                bisOneToOneMap = IcdCode.IsSnomedOneToOne
                If Not IsNothing(clsSmartDx.smartCPTCodes) Then
                    If clsSmartDx.smartCPTCodes.Count > 0 Then

                        Dim myCpt = (From column In clsSmartDx.smartCPTCodes Where column.ICDId = Convert.ToString(nICDId))
                        If Not IsNothing(myCpt) Then
                            For Each CptCode As gloSmartDx.Core.CPT In myCpt
                                nCPTId = CptCode.CPTId
                                sCptCode = CptCode.CPTCode
                                sCptDesc = CptCode.CPTDescription
                                nUnit = CptCode.CPTUnit
                                nLineNo = CptCode.CPTLineNo
                                If Not IsNothing(CptCode.CPTModifiers) Then
                                    Dim myModifier() As gloSmartDx.Core.CPTModifiers = (From column In CptCode.CPTModifiers
                                                                                        Where (column.ICDId = Convert.ToString(nICDId) And
                                                                                               column.CPTId = Convert.ToString(nCPTId)))
                                    For Each modifier As gloSmartDx.Core.CPTModifiers In myModifier
                                        sModCode = modifier.CPTModifierCode
                                        sModDesc = modifier.CPTModifierDescription
                                        If gblnICD9Driven Then
                                            AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, sCptCode, sCptDesc, sModCode, sModDesc, nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                                        Else
                                            AddRowExamIcd9Datatable(dtSmartDx, "", "", sCptCode, sCptDesc, "", "", nUnit, nLineNo, "", "", nIcdRevision, bisOneToOneMap)
                                            AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, sCptCode, sCptDesc, sModCode, sModDesc, nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                                        End If
                                    Next
                                Else
                                    If gblnICD9Driven Then
                                        AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, sCptCode, sCptDesc, "", "", nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                                    Else
                                        AddRowExamIcd9Datatable(dtSmartDx, "", "", sCptCode, sCptDesc, "", "", nUnit, nLineNo, "", "", nIcdRevision, bisOneToOneMap)
                                        AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, sCptCode, sCptDesc, "", "", nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                                    End If

                                End If
                            Next
                        Else
                            AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, "", "", "", "", nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                        End If
                    Else
                        AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, "", "", "", "", nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                    End If
                Else
                    AddRowExamIcd9Datatable(dtSmartDx, sIcdCode, sIcdDesc, "", "", "", "", nUnit, nLineNo, sSnomedCode, sSnomedDesc, nIcdRevision, bisOneToOneMap)
                End If
            Next


            If clsDiagnosis.SaveExamIcd9Cpt(dtSmartDx) Then
                DialogResult = Windows.Forms.DialogResult.Yes
            Else
                DialogResult = Windows.Forms.DialogResult.No
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            DialogResult = Windows.Forms.DialogResult.No
        Finally
            sIcdCode = Nothing
            sIcdDesc = Nothing
            sCptCode = Nothing
            sCptDesc = Nothing
            sModCode = Nothing
            sModDesc = Nothing
            sSnomedCode = Nothing
            sSnomedDesc = Nothing

            If Not IsNothing(dtSmartDx) Then
                dtSmartDx.Dispose()
                dtSmartDx = Nothing
            End If

            If Not IsNothing(clsDiagnosis) Then
                clsDiagnosis.Dispose()
                clsDiagnosis = Nothing
            End If
        End Try

    End Sub

    Private Function IsMultipleReferralLetter() As Boolean

        Dim referralLetterCount As Int16 = 0
        Dim isMultiple As Boolean = False

        For i As Integer = 0 To trICD9Association.Nodes(1).GetNodeCount(False) - 1

            If trICD9Association.Nodes(1).Nodes(i).GetNodeCount(False) > 0 Then

                For j As Integer = 0 To trICD9Association.Nodes(1).Nodes(i).GetNodeCount(False) - 1
                    If trICD9Association.Nodes(1).Nodes(i).Nodes(j).GetNodeCount(False) > 0 Then
                        If trICD9Association.Nodes(1).Nodes(i).Nodes(j).Text = "Referral Letter" Then
                            If trICD9Association.Nodes(1).Nodes(i).Nodes(j).GetNodeCount(False) > 0 Then
                                For k As Integer = 0 To trICD9Association.Nodes(1).Nodes(i).Nodes(j).GetNodeCount(False) - 1
                                    If trICD9Association.Nodes(1).Nodes(i).Nodes(j).Nodes(k).Checked = True Then

                                        referralLetterCount = referralLetterCount + 1
                                        If referralLetterCount > 1 Then
                                            isMultiple = True
                                            Exit For
                                        End If

                                    End If
                                Next

                            End If

                        End If
                    End If

                    If isMultiple Then
                        Exit For
                    End If

                Next
            End If
            If isMultiple Then
                Exit For
            End If
        Next

        Return isMultiple

    End Function


    Private Sub SaveAssociation()
        Dim oDrug As gloEMRActors.Drug
        Dim ReffCnt As Integer = 0
        'Dim arrICD9Only As New ArrayList 'if Only ICD9 is checked
        Dim arrDruglist As New ArrayList 'for durglist                
        Dim arrexam As New ArrayList 'arraylist which has ICD9send to exam
        Dim arrFlow As New ArrayList
        Dim ICD9Node As New myTreeNode
        Dim i As Integer

        Dim strLabTaskDescription As String = ""
        Dim strDiagnosis As String = ""

        Dim _IsRootNode As Boolean = False
        ' Dim lstonlyICD9 As New myList
        Dim arrLabs As New ArrayList
        Dim arrRadiology As New ArrayList
        Dim arrTemplate As New ArrayList
        Dim oclsSmartDiagnosis As New clsSmartDiagnosis
        arrexam.Clear()
        '  arrICD9Only.Clear()
        arrPE.Clear()
        arrLabs.Clear()
        frmPatientExam.nRefTempID = 0

        For i = 0 To trICD9Association.Nodes(1).GetNodeCount(False) - 1 ' Loop for treeview
            Dim bIsOnlyDiagnosis As Boolean = False
            _IsRootNode = False
            'Get the ICD9Node associated sequentially
            ICD9Node = trICD9Association.Nodes(1).Nodes(i) 'First Node i.e ICD9
            '  lstonlyICD9 = New myList
            'Add only ICD9 to arraylist 
            If ICD9Node.Checked = True Then
                Dim strICD9() As String
                strICD9 = Split(ICD9Node.Name, "-", 2)
                _IsRootNode = True
                Dim strICD9Code As String = ""
                Dim strICD9Desc As String = ""
                strICD9Code = strICD9.GetValue(0).ToString.Trim()
                strICD9Desc = strICD9.GetValue(1).ToString.Trim()

                ' IF only ICD9 is check
                'lstonlyICD9 = New myList
                'lstonlyICD9.Code = strICD9Code
                'lstonlyICD9.Description = strICD9Desc

                If arrexam.Count >= 1 Then 'check for the ICD9 is present in array list or not
                    Dim IsICD9Exists As Boolean = False
                    For m As Integer = 0 To arrexam.Count - 1
                        Dim myTbl As mytable
                        myTbl = CType(arrexam.Item(m), mytable)
                        If myTbl.Code = strICD9Code And myTbl.Description = strICD9Desc Then
                            IsICD9Exists = True
                        End If
                    Next
                    If IsICD9Exists = False Then
                        arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                    End If
                Else
                    arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                End If
            Else
            End If

            If ICD9Node.GetNodeCount(True) > 0 Then

                For k As Integer = 0 To ICD9Node.GetNodeCount(False) - 1
                    Dim AssociateNode As myTreeNode
                    AssociateNode = ICD9Node.Nodes(k)

                    For j As Integer = 0 To AssociateNode.GetNodeCount(False) - 1
                        Dim thisNode As myTreeNode = CType(AssociateNode.Nodes.Item(j), myTreeNode)
                        If AssociateNode.Nodes(j).Checked = True Then
                            Dim lst As New myList
                            Dim Emdeonlst As New gloEmdeonCommon.myList '' Added by kanchan on 20100823
                            If AssociateNode.Text = "CPT" Then
                                ''''''''''''''''''''
                                'split the CPT 

                                Dim strCPT As String()
                                strCPT = Split(thisNode.Name, "-", 2)

                                'split the ICD9
                                Dim strICD9 As String()
                                strICD9 = Split(ICD9Node.Text, "-", 2)

                                If ICD9Node.Checked = False Then
                                    '  Dim strICD9Drug() As String
                                    strICD9 = Split(ICD9Node.Name, "-", 2)
                                    _IsRootNode = True
                                    Dim strICD9CodeDrug As String = ""
                                    Dim strICD9DescDrug As String = ""
                                    strICD9CodeDrug = strICD9.GetValue(0).ToString.Trim()
                                    strICD9DescDrug = strICD9.GetValue(1).ToString.Trim()

                                    If arrexam.Count >= 1 Then 'check for the ICD9 is present in array list or not
                                        Dim IsICD9Exists As Boolean = False
                                        For m As Integer = 0 To arrexam.Count - 1
                                            Dim myTbl As mytable
                                            myTbl = CType(arrexam.Item(m), mytable)
                                            If myTbl.Code = strICD9CodeDrug And myTbl.Description = strICD9DescDrug Then
                                                IsICD9Exists = True
                                            End If
                                        Next
                                        If IsICD9Exists = False Then
                                            arrexam.Add(New mytable(strICD9DescDrug, strICD9CodeDrug))
                                        End If
                                    Else
                                        arrexam.Add(New mytable(strICD9DescDrug, strICD9CodeDrug))
                                    End If
                                End If


                            ElseIf AssociateNode.Text = "Drugs" Then
                                ''If ICD9 Node is not checked but the drugs is check the ICD for selected drug is saved
                                If ICD9Node.Checked = False Then
                                    Dim strICD9() As String
                                    strICD9 = Split(ICD9Node.Name, "-", 2)
                                    _IsRootNode = True
                                    Dim strICD9Code As String = ""
                                    Dim strICD9Desc As String = ""
                                    strICD9Code = strICD9.GetValue(0).ToString.Trim()
                                    strICD9Desc = strICD9.GetValue(1).ToString.Trim()

                                    If arrexam.Count >= 1 Then 'check for the ICD9 is present in array list or not
                                        Dim IsICD9Exists As Boolean = False
                                        For m As Integer = 0 To arrexam.Count - 1
                                            Dim myTbl As mytable
                                            myTbl = CType(arrexam.Item(m), mytable)
                                            If myTbl.Code = strICD9Code And myTbl.Description = strICD9Desc Then
                                                IsICD9Exists = True
                                            End If
                                        Next
                                        If IsICD9Exists = False Then
                                            arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                                        End If
                                    Else
                                        arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                                    End If

                                    Dim strICD9Drugs As String()
                                    Dim strICD9DrugsCode As String = ""
                                    Dim strICD9DrugsDesc As String = ""
                                    strICD9Drugs = Split(ICD9Node.Name, "-", 2)
                                    strICD9DrugsCode = strICD9Drugs.GetValue(0).ToString.Trim()
                                    strICD9DrugsDesc = strICD9Drugs.GetValue(1).ToString.Trim()

                                End If

                                oDrug = New gloEMRActors.Drug

                                'Dim lstDrug As New myList
                                'Dim DrudID As Long = thisNode.Key

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

                                ''If ICD9 Node is not checked but the Patient Education is check the ICD for selected PatientEducation is saved
                                If ICD9Node.Checked = False Then

                                    Dim strICD9() As String
                                    strICD9 = Split(ICD9Node.Name, "-", 2)
                                    _IsRootNode = True
                                    Dim strICD9Code As String = ""
                                    Dim strICD9Desc As String = ""
                                    strICD9Code = strICD9.GetValue(0).ToString.Trim()
                                    strICD9Desc = strICD9.GetValue(1).ToString.Trim()

                                    If arrexam.Count >= 1 Then 'check for the ICD9 is present in array list or not
                                        Dim IsICD9Exists As Boolean = False
                                        For m As Integer = 0 To arrexam.Count - 1
                                            Dim myTbl As mytable
                                            myTbl = CType(arrexam.Item(m), mytable)
                                            If myTbl.Code = strICD9Code And myTbl.Description = strICD9Desc Then
                                                IsICD9Exists = True
                                            End If
                                        Next
                                        If IsICD9Exists = False Then
                                            arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                                        End If
                                    Else
                                        arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                                    End If


                                    Dim strICD9Drugs As String()
                                    Dim strICD9DrugsCode As String = ""
                                    Dim strICD9DrugsDesc As String = ""
                                    strICD9Drugs = Split(ICD9Node.Name, "-", 2)
                                    strICD9DrugsCode = strICD9Drugs.GetValue(0).ToString.Trim()
                                    strICD9DrugsDesc = strICD9Drugs.GetValue(1).ToString.Trim()

                                End If

                                Dim lstPE As New myList
                                lstPE.ID = 3 '' For Patient Education
                                lstPE.Description = thisNode.Name
                                lstPE.Index = thisNode.Key
                                lstPE.HistoryCategory = ICD9Node.NodeName

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

                                ''If ICD9 Node is not checked but the Tags is check the ICD for selected Tags is saved
                                If ICD9Node.Checked = False Then

                                    Dim strICD9() As String
                                    strICD9 = Split(ICD9Node.Name, "-", 2)
                                    _IsRootNode = True
                                    Dim strICD9Code As String = ""
                                    Dim strICD9Desc As String = ""
                                    strICD9Code = strICD9.GetValue(0).ToString.Trim()
                                    strICD9Desc = strICD9.GetValue(1).ToString.Trim()

                                    If arrexam.Count >= 1 Then 'check for the ICD9 is present in array list or not
                                        Dim IsICD9Exists As Boolean = False
                                        For m As Integer = 0 To arrexam.Count - 1
                                            Dim myTbl As mytable
                                            myTbl = CType(arrexam.Item(m), mytable)
                                            If myTbl.Code = strICD9Code And myTbl.Description = strICD9Desc Then
                                                IsICD9Exists = True
                                            End If
                                        Next
                                        If IsICD9Exists = False Then
                                            arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                                        End If
                                    Else
                                        arrexam.Add(New mytable(strICD9Desc, strICD9Code))
                                    End If


                                    Dim strICD9Drugs As String()
                                    Dim strICD9DrugsCode As String = ""
                                    Dim strICD9DrugsDesc As String = ""
                                    strICD9Drugs = Split(ICD9Node.Name, "-", 2)
                                    strICD9DrugsCode = strICD9Drugs.GetValue(0).ToString.Trim()
                                    strICD9DrugsDesc = strICD9Drugs.GetValue(1).ToString.Trim()

                                End If
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
                                lstTags.HistoryCategory = ICD9Node.NodeName  '''' 
                                frmPatientExam.arrTagID.Add(lstTags) ''''thisNode.Key)
                            ElseIf AssociateNode.Text = "Orders and Results" Then


                                If Not strDiagnosis.Contains(ICD9Node.Name) Then
                                    strDiagnosis += ICD9Node.Name & ", "
                                    Emdeonlst.Value = AssociateNode.Nodes(j).Text
                                    strLabTaskDescription += AssociateNode.Nodes(j).Text & ", "
                                Else
                                    Emdeonlst.Value = AssociateNode.Nodes(j).Text
                                    strLabTaskDescription += AssociateNode.Nodes(j).Text & ", "
                                End If

                                Emdeonlst.ID = AssociateNode.Nodes(j).Tag
                                arrLabs.Add(Emdeonlst)
                                Emdeonlst = Nothing 'Change made to solve memory Leak and word crash issue
                                'Code End-Added by kanchan on 20100823 for changes in logic
                            ElseIf AssociateNode.Text = "Order Templates" Then
                                lst.Value = AssociateNode.Nodes(j).Text
                                lst.Index = AssociateNode.Nodes(j).Tag
                                arrRadiology.Add(lst) 'AssociateNode.Nodes(j).Text)
                            ElseIf AssociateNode.Text = "Referral Letter" Then
                                lst.Value = AssociateNode.Nodes(j).Text
                                lst.Index = AssociateNode.Nodes(j).Tag
                                arrTemplate.Add(lst) 'AssociateNode.Nodes(j).Text)



                                ReffCnt = ReffCnt + 1
                                If ReffCnt >= 2 Then

                                    MessageBox.Show("Please select only one Referral ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    arrTemplate.Clear()
                                    arrFlow.Clear()
                                    arrPE.Clear()
                                    arrLabs.Clear()
                                    arrRadiology.Clear()
                                    arrDruglist.Clear()

                                    arrexam.Clear()
                                    '  arrICD9Only.Clear()
                                    frmPatientExam.nRefTempID = 0
                                    Exit Sub
                                End If
                            ElseIf AssociateNode.Text = "FlowSheet" Then
                                lst.Value = AssociateNode.Nodes(j).Text
                                lst.Index = AssociateNode.Nodes(j).Tag
                                arrFlow.Add(lst) 'AssociateNode.Nodes(j).Text)
                            End If
                            'Change made to solve memory Leak and word crash issue
                            If Not lst Is Nothing Then
                                lst = Nothing
                            End If
                        End If


                        _IsRootNode = False

                    Next 'For j As Integer = 0 To AssociateNode.GetNodeCount(False) - 1
                    'Change made to solve memory Leak and word crash issue
                    If Not AssociateNode Is Nothing Then
                        AssociateNode = Nothing
                    End If
                Next  'For k As Integer = 0 To 3
            End If

        Next 'For i = 0 To trICD9Association.Nodes(0).GetNodeCount(False) - 1

        '' SUDHIR 20090806 '' 
        If gblnICD9Driven Then
            If saveDiagnosis() = False Then
                Exit Sub
            Else
                DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Else
            If SaveCPTDrivenDiagnosis() = False Then
                Exit Sub
            Else
                DialogResult = Windows.Forms.DialogResult.Yes
            End If
        End If
        trICD9Association.Visible = False
        ''Use datarow for performance.
        If Not IsNothing(_dt) Then
            ' '' ''For x As Integer = 0 To _dt.Rows.Count - 1
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
                                        .Close()
                                        .Dispose()
                                        ofrmPrescription = Nothing
                                    End With
                                    'End If
                                Else
                                    MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If

                            Else

                                Dim ofrmPrescription As frmPrescription
                                ofrmPrescription = frmPrescription.GetInstance(arrDruglist, m_ProviderID, m_VisitID, _PatientID, False)
                                If IsNothing(ofrmPrescription) = True Then
                                    Exit Sub
                                End If
                                ''''Dim ofrmPrescription As New frmPrescription(arrDruglist, m_ProviderID, m_VisitID, _PatientID)

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
                            'Generate Task for Drug
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk.
                                If Not IsNothing(arrDruglist) Then
                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nDrugProviderID As Int64
                                    Dim sDrugProviderName As String
                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                    'Change made to solve memory Leak and word crash issue
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
                                    Dim strDrug As String
                                    Dim strDrugs As String = ""
                                    strDrug = ""
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

                                    If drTask(8) <> "" Then ''drTask(8) for sUserID.
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    'Change made to solve memory Leak and word crash issue
                                    ofrm.Close()
                                    ofrm.Dispose()
                                    ofrm = Nothing

                                End If
                            Else
                                Dim strDrug As String
                                Dim strDrugs As String = ""
                                strDrug = ""
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
                                If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(8) for sUserID & drTask(6) for sTaskusers.
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If
                                oclsSmartDiagnosis.AddTasks("Drugs available", sDescription, Now.ToString(), Now.ToString(), TaskType.Drug, strDrugs, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If
                    End If
                    'Code End-Added by kanchan on 20100622 for generate task for drug

                ElseIf drTask(1) = "Patient Education" Then '' drTask(1) for sFieldName

                    '' if there exits Templates for patient education in associated ICD9 
                    If arrPE.Count > 0 Then
                        If drTask(2) = True Then ''drTask(2) for bFieldStatus

                            'Dim frmPatientPE As New frmPatientEducation(m_VisitID, _PatientID, m_ExamID, arrPE)

                            'With frmPatientPE
                            '    '  .myCaller = frmPatientExam
                            '    .blnOpenFromExam = False
                            '    .StartPosition = FormStartPosition.CenterParent
                            '    .ShowDialog(IIf(IsNothing(frmPatientPE.Parent), Me, frmPatientPE.Parent))
                            '    'Change made to solve memory Leak and word crash issue
                            '    .Close()
                            '    .Dispose()
                            'End With
                            'frmPatientPE = Nothing

                            Dim frm As frmPatientEducationPreview

                            For i = 0 To arrPE.Count - 1
                                frm = New frmPatientEducationPreview()
                                frm.TMPID = CType(arrPE(i), myList).Index
                                frm.TempName = CType(arrPE(i), myList).Description
                                frm.VISID = m_VisitID
                                frm.PATID = _PatientID
                                frm.EXAMID = m_ExamID
                                frm.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterDiagnosis
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
                                frm.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterDiagnosis
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
                                    'Change made to solve memory Leak and word crash issue
                                    objfrmpatientflowsheet.Close()
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
                                End If
                                objfrmpatientflowsheet.Dispose()
                                objfrmpatientflowsheet = Nothing 'Change made to solve memory Leak and word crash issue
                            End If
                        Else
                            'Code Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                            'Generate Task for Flowsheet
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk.
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

                                    Dim strFlow As String = ""
                                    Dim strFlows As String = ""
                                    strFlow = ""
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
                                    'Change made to solve memory Leak and word crash issue
                                    ofrm.Close()
                                    ofrm.Dispose()
                                    ofrm = Nothing


                                End If
                            Else '' If False Then Save the Task.
                                Dim strFlow As String = ""
                                Dim strFlows As String = ""
                                strFlow = ""
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
                                If drTask(8) <> "" And drTask(6) <> "" Then '' drTask(6) for sTaskusers.
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If

                                oclsSmartDiagnosis.AddTasks("Flowsheet available", sDescription, Now.ToString(), Now.ToString(), TaskType.Flowsheet, strFlows, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If
                    End If

                ElseIf drTask(1) = "Orders and Results" Then ''drTask(1) for sFieldName.
                    If arrLabs.Count > 0 Then
                        If drTask(5) = False Then ''drTask(5) for bSendTask.
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus.

                                'Developer: Sanjog(Dhamke)
                                'Date:10 Dec 2011
                                'Bug ID/PRD Name/Salesforce Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                                'Reason: To show task info

                                Dim _TestList As String = String.Empty

                                Dim _MyTestList As gloEmdeonCommon.myList = Nothing
                                _TestList += "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")

                                _TestList += vbNewLine & "Diagnosis:" & vbNewLine & strDiagnosis.Trim().Trim(",")
                                If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage <> "" Then

                                    Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage

                                        Case "TASK"
                                            gloLabSettings("TASK", _TestList, arrLabs)
                                        Case "LABORDER"
                                            gloLabSettings("LABORDER", "", arrLabs, strDiagnosis)  '' added to show testnames on EmdeonScreen ,v8022
                                        Case "RECORDRESULTS"
                                            gloLabSettings("RECORDRESULTS", "", arrLabs)
                                        Case "ASK"
                                            ' new modal dialog for instant choice for next action to be performed.
                                            Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                            frmAskform.ShowInTaskbar = False
                                            frmAskform.BringToFront()
                                            frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                            gloLabSettings(frmAskform.LabFlowConfirm, _TestList, arrLabs, strDiagnosis)
                                            'Change made to solve memory Leak and word crash issue
                                            frmAskform.Close()
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
                                    'Change made to solve memory Leak and word crash issue
                                    frmAskform.Close()
                                    frmAskform.Dispose()
                                    frmAskform = Nothing
                                End If

                            Else
                                'Code Start-Added by kanchan on 20100823 for changes in logic
                                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(_PatientID)
                                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                frmNormalLab.ArrLabs = arrLabs '' Added by Abhijeet on 20100624
                                frmNormalLab.WindowState = FormWindowState.Minimized  '''''' added by Ujwala as on 11252010
                                frmNormalLab.ShowInTaskbar = False
                                frmNormalLab.BringToFront()
                                frmNormalLab.Show()
                                frmNormalLab.Hide()
                                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                frmNormalLab.Close()
                                frmNormalLab.Dispose()
                                frmNormalLab = Nothing 'Change made to solve memory Leak and word crash issue
                                'Code End-Added by kanchan on 20100823 for changes in logic
                            End If
                        Else
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk.
                                If Not IsNothing(arrLabs) Then
                                    If arrLabs.Count > 0 Then
                                        Dim dt As DataTable
                                        dt = Nothing
                                        Dim nLabProviderID As Int64
                                        Dim sLabProviderName As String
                                        Dim oPatientExam As New clsPatientExams
                                        Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                        'Change made to solve memory Leak and word crash issue
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

                                        Dim strlabs As String = ""
                                        ''= SerializeArrayList(arrLabs)
                                        Dim strlab As String = ""
                                        strlab = ""
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        'Dim sDescription As String = " For Lab Test:" & vbCrLf
                                        Dim ncnt As Integer = 1
                                        strLabTaskDescription = "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")
                                        strLabTaskDescription += vbNewLine & "Diagnosis:" & vbNewLine & strDiagnosis.Trim().Trim(",")
                                        For olab As Integer = 0 To arrLabs.Count - 1
                                            'Code commented & Added by kanchan on 20100823
                                            'strlab = CType(arrLabs.Item(olab), myList).ID & "~" & CType(arrLabs.Item(olab), myList).Value
                                            strlab = CType(arrLabs.Item(olab), gloEmdeonCommon.myList).ID & "~" & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                            If olab = 0 Then
                                                strlabs = strlab
                                                'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                                'Resolve Bug #88776: 00000997: Smart Dx Orders Task missing DX when Preview is Off by Namrata'
                                                'sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                                ncnt = ncnt + 1

                                            Else
                                                strlabs = strlabs & "|" & strlab
                                                'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                                'sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                                ncnt = ncnt + 1

                                            End If

                                        Next

                                        'Commented & Added by kanchan on 20100618 for Smart diagnosis
                                        ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, _PatientID, nLabProviderID, sDescription, "Lab available", TaskType.LabOrder, gstrLoginName)
                                        Dim ofrm As New gloTaskMail.frmTask
                                        ofrm.DataBaseConnectionString = GetConnectionString()
                                        ofrm.TaskID = 0
                                        ofrm.PatientID = _PatientID
                                        ofrm.ProviderID = nLabProviderID
                                        'ofrm.rtxtDescription.Text = sDescription
                                        ofrm.rtxtDescription.Text = strLabTaskDescription
                                        ofrm.txtSubject.Text = "Place Lab Order "
                                        ofrm._TaskType = TaskType.PlaceLabOrder
                                        ofrm._UserName = gstrLoginName
                                        ofrm.UserID = gnLoginID
                                        ofrm._sNotesExt = strlabs

                                        If drTask(8) <> "" Then ''drTask(8) for sUserID.
                                            Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                            ofrm._taskuser_id = sUserID
                                            ofrm._SmartTask = True
                                        End If

                                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                        'Change made to solve memory Leak and word crash issue
                                        ofrm.Close()
                                        ofrm.Dispose()
                                        ofrm = Nothing

                                    End If
                                End If
                            Else '' If False Then Save the Task.
                                Dim strlabs As String = ""
                                ''= SerializeArrayList(arrLabs)
                                Dim strlab As String = ""
                                strlab = ""
                                'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                'Dim sDescription As String = " For Lab Test:" & vbCrLf
                                Dim ncnt As Integer = 1
                                'Resolve Bug #88776: 00000997: Smart Dx Orders Task missing DX when Preview is Off by Namrata'
                                strLabTaskDescription = "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")
                                strLabTaskDescription += vbNewLine & "Diagnosis:" & vbNewLine & strDiagnosis.Trim().Trim(",")
                                For olab As Integer = 0 To arrLabs.Count - 1
                                    'Code commented & Added by kanchan on 20100823
                                    'strlab = CType(arrLabs.Item(olab), myList).ID & "~" & CType(arrLabs.Item(olab), myList).Value
                                    strlab = CType(arrLabs.Item(olab), gloEmdeonCommon.myList).ID & "~" & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                    If olab = 0 Then
                                        strlabs = strlab
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        'sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strlabs = strlabs & "|" & strlab
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        'sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    End If

                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(6) for sTaskusers.
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If

                                oclsSmartDiagnosis.AddTasks("Place Lab Order ", strLabTaskDescription, Now.ToString(), Now.ToString(), TaskType.PlaceLabOrder, strlabs, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If  ''_dt.Rows(x)("bSendTask") = False
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
                                    ._VisitID = m_VisitID
                                    ._VisitDate = Now
                                    .WindowState = FormWindowState.Maximized
                                    .BringToFront()
                                    .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    'Change made to solve memory Leak and word crash issue
                                    .Close()
                                    .Dispose()
                                End With
                                frm = Nothing
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
                                    ._VisitID = m_VisitID
                                    ._VisitDate = Now
                                    .WindowState = FormWindowState.Minimized
                                    .Opacity = 0
                                    .Show()
                                    .Hide()
                                    .SaveOrders()
                                    .Close()

                                End With
                                frm = Nothing 'Change made to solve memory Leak and word crash issue
                            End If
                        Else
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrRadiology) Then
                                    If arrRadiology.Count > 0 Then
                                        Dim dt As DataTable
                                        dt = Nothing
                                        Dim nOrderProviderID As Int64
                                        Dim sOrderProviderName As String
                                        Dim oPatientExam As New clsPatientExams
                                        Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                        'Change made to solve memory Leak and word crash issue
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

                                        Dim strOrders As String = ""
                                        Dim strOrder As String = ""
                                        strOrder = ""
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
                                        'Change made to solve memory Leak and word crash issue
                                        ofrm.Close()
                                        ofrm.Dispose()
                                        ofrm = Nothing

                                    End If
                                End If
                            Else '' If False Then Save the Task.
                                Dim strOrders As String = ""
                                Dim strOrder As String = ""
                                strOrder = ""
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
                                If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(6) for sTaskusers
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If

                                oclsSmartDiagnosis.AddTasks("Order available", sDescription, Now.ToString(), Now.ToString(), TaskType.OrderRadiology, strOrders, _sUserID, _sTaskusers, _PatientID)
                            End If
                        End If  ''_dt.Rows(x)("bSendTask") = False
                    End If

                ElseIf drTask(1) = "Referral Letter" Then ''drTask(1) for sFieldName
                    If arrTemplate.Count > 0 Then
                        If Not mycaller Is Nothing Then
                            Dim _TemplateName As String = ""
                            frmPatientExam.nRefTempID = Convert.ToInt64(CType(arrTemplate.Item(0), myList).Index)
                            _TemplateName = CType(arrTemplate.Item(0), myList).Value
                            ' swaraj 01-01-2010 -- To Load Referral Templates Data'
                            Dim dtVisitRef As DataTable
                            Dim dtPatRef As DataTable

                            'check if Referrals exists against given m_VisitId
                            If Not objReferralsDBLayer.CheckReferral(m_VisitID, m_ExamID, _PatientID) Then
                                dtVisitRef = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitID, _PatientID, m_ExamID)
                                SaveReferrals(dtVisitRef, True, _TemplateName)
                                If Not IsNothing(dtVisitRef) Then
                                    dtVisitRef.Dispose()
                                    dtVisitRef = Nothing
                                End If
                            Else
                                'if Referral Details do not exist for that m_VisitId,
                                'Populate Referrals Details from Patient_Dtl Table
                                dtPatRef = objReferralsDBLayer.FillControls("R", _PatientID)
                                SaveReferrals(dtPatRef, False, _TemplateName)
                                If Not IsNothing(dtPatRef) Then
                                    dtPatRef.Dispose()
                                    dtPatRef = Nothing
                                End If
                            End If


                            frmSummaryofVisit.PatientTemplateID = Convert.ToInt64(CType(arrTemplate.Item(0), myList).Index)
                            Dim frm As frmSummaryofVisit = Nothing
                            Try
                                frm = New frmSummaryofVisit(_PatientID, m_VisitID, True, m_ExamID, m_ExamFilePath, ExamProviderId, blnExamFinished, True)

                                ''Sanjog- Added on 2011 Jan 20 to show the referrals forms
                                If drTask(2) = True Then ''drTask(5) for bFieldStatus
                                    frm.dtDos = m_ExamDate
                                    frmExamChild = frm
                                    frm.Text = "Patient Referrals"
                                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    'Change made to solve memory Leak and word crash issue
                                    If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                                        frm.Close()
                                    End If
                                    If Not IsNothing(frm) Then
                                        frm.Dispose()
                                        frm = Nothing
                                    End If
                                Else
                                    frm.dtDos = m_ExamDate
                                    frmExamChild = frm
                                    frm.Text = "Patient Referrals"

                                    frm.Opacity = 0
                                    frm.Show()
                                    frm.Visible = False
                                    frm.SaveReferrals()
                                    'frm.Close()
                                End If
                                frm = Nothing 'Change made to solve memory Leak and word crash issue
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

                        Else
                            frmPatientExam.nRefTempID = 0
                        End If
                    Else
                        frmPatientExam.nRefTempID = 0
                    End If
                End If

            Next
        End If

        'Change made to solve memory Leak and word crash issue
        If Not oclsSmartDiagnosis Is Nothing Then
            oclsSmartDiagnosis.Dispose()
            oclsSmartDiagnosis = Nothing
        End If
        If Not arrDruglist Is Nothing Then
            arrDruglist.Clear()
            arrDruglist = Nothing
        End If
        If Not arrFlow Is Nothing Then
            arrFlow.Clear()
            arrFlow = Nothing
        End If

        frmPatientExam.Arrlist = arrexam

        If Not arrexam Is Nothing Then
            arrexam.Clear()
            arrexam = Nothing
        End If
        frmPatientExam.blnChangesMade = True
        frmPatientExam.nRefTempID = 0
        '   Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)

    End Sub

    Private Sub SmartDxAssociationWithExam()
        Dim oSmartDx As New smartDx
        Dim ICD9Node As myTreeNode = Nothing

        Dim bIsDelete As Boolean = True
        Try
            If trICD9Association.Nodes IsNot Nothing AndAlso trICD9Association.Nodes(1).GetNodeCount(False) > 0 Then
                For i As Integer = 0 To trICD9Association.Nodes(1).GetNodeCount(False) - 1 ' Loop for treeview
                    'ICD9Node = Nothing
                    ICD9Node = trICD9Association.Nodes(1).Nodes(i) 'First Node i.e ICD9
                    oSmartDx.ExamAssociatedSmartDxInsetion(ICD9Node.SmartDxKey, m_ExamID, m_VisitID, ICD9Node.Key, Convert.ToInt16(ICD9Node.Tag), bIsDelete)
                    bIsDelete = False
                Next
            ElseIf gblnICD9Driven Then
                oSmartDx.ExamAssociatedDeletion(m_ExamID, m_VisitID)
            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            If oSmartDx IsNot Nothing Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If
        End Try
    End Sub

    Private Sub SaveAssociation_Reverted()

        Dim ReffCnt As Integer = 0
        Dim arrexam As New ArrayList 'arraylist which has ICD9send to exam
        Dim arrDruglist As New ArrayList 'for durglist                
        Dim arrFlow As New ArrayList
        Dim arrLabs As New ArrayList
        Dim arrRadiology As New ArrayList
        Dim arrTemplate As New ArrayList
        Dim ICD9Node As New myTreeNode
        Dim i As Integer
        Dim strLabTaskDescription As String = ""
        Dim strDiagnosis As String = ""
        Dim _IsRootNode As Boolean = False
        Dim oclsSmartDiagnosis As New clsSmartDiagnosis
        arrexam.Clear()
        arrPE.Clear()
        frmPatientExam.nRefTempID = 0
        For Each oICD9Node As myTreeNode In trICD9Association.Nodes(1).Nodes

            If RbICD9.Checked Then
                If oICD9Node.Tag = gloGlobal.gloICD.CodeRevision.ICD10 Then
                    MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If


            ElseIf rbICD10.Checked Then
                If oICD9Node.Tag = gloGlobal.gloICD.CodeRevision.ICD9 Then
                    MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            End If
        Next

        If Validate() = False Then
            Exit Sub
        End If

        If gblnICD9Driven = True Then
            If ValidateDiagnosis() = False Then
                Exit Sub
            End If
        End If

        If IsMultipleReferralLetter() = True Then
            MessageBox.Show("Please select only one Referral ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'SaveAssociation_Smart()

        Dim objSettings As New clsSettings
        objSettings.GetSettings()
        Dim _isEnableReviewScreen As Boolean = False

        If IsNothing(objSettings.EnableSamrtDxReviewScreen) = False Then
            _isEnableReviewScreen = objSettings.EnableSamrtDxReviewScreen
        Else
            _isEnableReviewScreen = True
        End If
        If _isEnableReviewScreen Then
            If Not IsNothing(clsSmartDxDisplayList.TreatmentList) Then
                If clsSmartDxDisplayList.TreatmentList.Count > 0 Then
                    Dim objReview As New gloUIControlLibrary.WPFForms.frmReviewSmartDxSelection(clsSmartDxDisplayList)
                    objReview.ShowDialog()
                    If objReview.DialogResult = True Then
                        SaveExamICD9CPT()
                    Else
                        Exit Sub
                    End If
                Else
                    If clsSmartDx.smartICDCodes.Count > 0 Then
                        SaveExamICD9CPT()
                    End If
                End If
            End If
        Else
            If clsSmartDx.smartICDCodes.Count > 0 Then
                SaveExamICD9CPT()
            End If
        End If

        Dim oSmartDx As New smartDx
        Dim bIsDelete As Boolean = True
        Try
            For i = 0 To trICD9Association.Nodes(1).GetNodeCount(False) - 1 ' Loop for treeview
                ICD9Node = Nothing
                ICD9Node = trICD9Association.Nodes(1).Nodes(i) 'First Node i.e ICD9
                oSmartDx.ExamAssociatedSmartDxInsetion(ICD9Node.SmartDxKey, m_ExamID, m_VisitID, ICD9Node.Key, Convert.ToInt16(ICD9Node.Tag), bIsDelete)
                bIsDelete = False
            Next
        Catch ex As Exception
            MessageBox.Show(ex, True)
        Finally
            If oSmartDx IsNot Nothing Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If
        End Try
        trICD9Association.Visible = False
        ''Use datarow for performance.
        If Not IsNothing(_dt) Then
            ' '' ''For x As Integer = 0 To _dt.Rows.Count - 1
            For Each drTask As DataRow In _dt.Rows
                'Code Start-Added by kanchan on 20100622 for generate task for drug
                If drTask(1) = "Drugs" Then ''drTask(1) for sFieldName
                    If Not IsNothing(arrDruglist) Then
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
                                            .Close()
                                            .Dispose()
                                            ofrmPrescription = Nothing
                                        End With
                                        'End If
                                    Else
                                        MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If

                                Else

                                    Dim ofrmPrescription As frmPrescription
                                    ofrmPrescription = frmPrescription.GetInstance(arrDruglist, m_ProviderID, m_VisitID, _PatientID, False)
                                    If IsNothing(ofrmPrescription) = True Then
                                        Exit Sub
                                    End If
                                    ''''Dim ofrmPrescription As New frmPrescription(arrDruglist, m_ProviderID, m_VisitID, _PatientID)

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
                                'Generate Task for Drug
                                If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk.
                                    If Not IsNothing(arrDruglist) Then
                                        Dim dt As DataTable
                                        dt = Nothing
                                        Dim nDrugProviderID As Int64
                                        Dim sDrugProviderName As String
                                        Dim oPatientExam As New clsPatientExams
                                        Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                        'Change made to solve memory Leak and word crash issue
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
                                        Dim strDrug As String
                                        Dim strDrugs As String = ""
                                        strDrug = ""
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

                                        If drTask(8) <> "" Then ''drTask(8) for sUserID.
                                            Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                            ofrm._taskuser_id = sUserID
                                            ofrm._SmartTask = True
                                        End If

                                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                        'Change made to solve memory Leak and word crash issue
                                        ofrm.Close()
                                        ofrm.Dispose()
                                        ofrm = Nothing

                                    End If
                                Else
                                    Dim strDrug As String
                                    Dim strDrugs As String = ""
                                    strDrug = ""
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
                                    If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(8) for sUserID & drTask(6) for sTaskusers.
                                        _sUserID = drTask(8)
                                        _sTaskusers = drTask(6)
                                    End If
                                    oclsSmartDiagnosis.AddTasks("Drugs available", sDescription, Now.ToString(), Now.ToString(), TaskType.Drug, strDrugs, _sUserID, _sTaskusers, _PatientID)
                                End If
                            End If
                        End If




                    End If
                    'Code End-Added by kanchan on 20100622 for generate task for drug

                ElseIf drTask(1) = "Patient Education" Then '' drTask(1) for sFieldName
                    If Not IsNothing(arrPE) Then
                        '' if there exits Templates for patient education in associated ICD9 
                        If arrPE.Count > 0 Then
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus

                                'Dim frmPatientPE As New frmPatientEducation(m_VisitID, _PatientID, m_ExamID, arrPE)

                                'With frmPatientPE
                                '    '  .myCaller = frmPatientExam
                                '    .blnOpenFromExam = False
                                '    .StartPosition = FormStartPosition.CenterParent
                                '    .ShowDialog(IIf(IsNothing(frmPatientPE.Parent), Me, frmPatientPE.Parent))
                                '    'Change made to solve memory Leak and word crash issue
                                '    .Close()
                                '    .Dispose()
                                'End With
                                'frmPatientPE = Nothing

                                Dim frm As frmPatientEducationPreview

                                For i = 0 To arrPE.Count - 1
                                    frm = New frmPatientEducationPreview()
                                    frm.TMPID = CType(arrPE(i), myList).Index
                                    frm.TempName = CType(arrPE(i), myList).Description
                                    frm.VISID = m_VisitID
                                    frm.PATID = _PatientID
                                    frm.EXAMID = m_ExamID
                                    frm.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterDiagnosis
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
                                    frm.Sourc = gloEMRGeneralLibrary.clsInfobutton.enumSource.EncounterDiagnosis
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


                    End If

                ElseIf drTask(1) = "Flowsheet" Then ''drTask(1) for sFieldName
                    If Not IsNothing(arrFlow) Then
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
                                        'Change made to solve memory Leak and word crash issue
                                        objfrmpatientflowsheet.Close()
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
                                    End If
                                    objfrmpatientflowsheet.Dispose()
                                    objfrmpatientflowsheet = Nothing 'Change made to solve memory Leak and word crash issue
                                End If
                            Else
                                'Code Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                                'Generate Task for Flowsheet
                                '' for assigning task ''
                                If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk.
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

                                        Dim strFlow As String = ""
                                        Dim strFlows As String = ""
                                        strFlow = ""
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
                                        'Change made to solve memory Leak and word crash issue
                                        ofrm.Close()
                                        ofrm.Dispose()
                                        ofrm = Nothing


                                    End If
                                Else '' If False Then Save the Task.
                                    Dim strFlow As String = ""
                                    Dim strFlows As String = ""
                                    strFlow = ""
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
                                    If drTask(8) <> "" And drTask(6) <> "" Then '' drTask(6) for sTaskusers.
                                        _sUserID = drTask(8)
                                        _sTaskusers = drTask(6)
                                    End If

                                    oclsSmartDiagnosis.AddTasks("Flowsheet available", sDescription, Now.ToString(), Now.ToString(), TaskType.Flowsheet, strFlows, _sUserID, _sTaskusers, _PatientID)
                                End If
                            End If
                        End If


                    End If
                ElseIf drTask(1) = "Orders and Results" Then ''drTask(1) for sFieldName.
                    If Not IsNothing(arrLabs) Then
                        If arrLabs.Count > 0 Then
                            If drTask(5) = False Then ''drTask(5) for bSendTask.
                                If drTask(2) = True Then ''drTask(2) for bFieldStatus.

                                    'Developer: Sanjog(Dhamke)
                                    'Date:10 Dec 2011
                                    'Bug ID/PRD Name/Salesforce Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                                    'Reason: To show task info
                                    If clsSmartDx.smartOrders.Count > 0 Then
                                        For Each objTemp As gloEMRGeneralLibrary.gloSmartDx.Core.Orders In clsSmartDx.smartOrders
                                            If Not strDiagnosis.Contains(objTemp.ICDName) Then
                                                strDiagnosis += ICD9Node.Name & ", "
                                                strLabTaskDescription += objTemp.TestName & ", "
                                            Else
                                                strLabTaskDescription += objTemp.TestName & ", "
                                            End If
                                        Next
                                    End If
                                    Dim _TestList As String = String.Empty

                                    Dim _MyTestList As gloEmdeonCommon.myList = Nothing
                                    _TestList += "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")

                                    _TestList += vbNewLine & "Diagnosis:" & vbNewLine & strDiagnosis.Trim().Trim(",")
                                    If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage <> "" Then
                                        Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage
                                            Case "TASK"
                                                gloLabSettings("TASK", _TestList, arrLabs)
                                            Case "LABORDER"
                                                gloLabSettings("LABORDER", "", arrLabs, strDiagnosis)  '' added to show testnames on EmdeonScreen ,v8022
                                            Case "RECORDRESULTS"
                                                gloLabSettings("RECORDRESULTS", "", arrLabs)
                                            Case "ASK"
                                                ' new modal dialog for instant choice for next action to be performed.
                                                Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                                frmAskform.ShowInTaskbar = False
                                                frmAskform.BringToFront()
                                                frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                                gloLabSettings(frmAskform.LabFlowConfirm, _TestList, arrLabs, strDiagnosis)
                                                'Change made to solve memory Leak and word crash issue
                                                frmAskform.Close()
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
                                        'Change made to solve memory Leak and word crash issue
                                        frmAskform.Close()
                                        frmAskform.Dispose()
                                        frmAskform = Nothing
                                    End If

                                Else
                                    'Code Start-Added by kanchan on 20100823 for changes in logic
                                    Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(_PatientID)
                                    AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                    frmNormalLab.ArrLabs = arrLabs '' Added by Abhijeet on 20100624
                                    frmNormalLab.WindowState = FormWindowState.Minimized  '''''' added by Ujwala as on 11252010
                                    frmNormalLab.ShowInTaskbar = False
                                    frmNormalLab.BringToFront()
                                    frmNormalLab.Show()
                                    frmNormalLab.Hide()
                                    RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                    frmNormalLab.Close()
                                    frmNormalLab.Dispose()
                                    frmNormalLab = Nothing 'Change made to solve memory Leak and word crash issue
                                    'Code End-Added by kanchan on 20100823 for changes in logic
                                End If
                            Else
                                If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk.
                                    If Not IsNothing(arrLabs) Then
                                        If arrLabs.Count > 0 Then
                                            Dim dt As DataTable
                                            dt = Nothing
                                            Dim nLabProviderID As Int64
                                            Dim sLabProviderName As String
                                            Dim oPatientExam As New clsPatientExams
                                            Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                            'Change made to solve memory Leak and word crash issue
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

                                            Dim strlabs As String = ""
                                            ''= SerializeArrayList(arrLabs)
                                            Dim strlab As String = ""
                                            strlab = ""
                                            'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                            Dim sDescription As String = " For Lab Test:" & vbCrLf
                                            Dim ncnt As Integer = 1
                                            strLabTaskDescription = "Lab Tests:" & vbNewLine & strLabTaskDescription.Trim().Trim(",")
                                            strLabTaskDescription += vbNewLine & "Diagnosis:" & vbNewLine & strDiagnosis.Trim().Trim(",")
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

                                            'Commented & Added by kanchan on 20100618 for Smart diagnosis
                                            ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, _PatientID, nLabProviderID, sDescription, "Lab available", TaskType.LabOrder, gstrLoginName)
                                            Dim ofrm As New gloTaskMail.frmTask
                                            ofrm.DataBaseConnectionString = GetConnectionString()
                                            ofrm.TaskID = 0
                                            ofrm.PatientID = _PatientID
                                            ofrm.ProviderID = nLabProviderID
                                            'ofrm.rtxtDescription.Text = sDescription
                                            ofrm.rtxtDescription.Text = strLabTaskDescription
                                            ofrm.txtSubject.Text = "Place Lab Order "
                                            ofrm._TaskType = TaskType.PlaceLabOrder
                                            ofrm._UserName = gstrLoginName
                                            ofrm.UserID = gnLoginID
                                            ofrm._sNotesExt = strlabs

                                            If drTask(8) <> "" Then ''drTask(8) for sUserID.
                                                Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                                ofrm._taskuser_id = sUserID
                                                ofrm._SmartTask = True
                                            End If

                                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                            'Change made to solve memory Leak and word crash issue
                                            ofrm.Close()
                                            ofrm.Dispose()
                                            ofrm = Nothing

                                        End If
                                    End If
                                Else '' If False Then Save the Task.
                                    Dim strlabs As String = ""
                                    ''= SerializeArrayList(arrLabs)
                                    Dim strlab As String = ""
                                    strlab = ""
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
                                    If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(6) for sTaskusers.
                                        _sUserID = drTask(8)
                                        _sTaskusers = drTask(6)
                                    End If

                                    oclsSmartDiagnosis.AddTasks("Place Lab Order ", sDescription, Now.ToString(), Now.ToString(), TaskType.PlaceLabOrder, strlabs, _sUserID, _sTaskusers, _PatientID)
                                End If
                            End If  ''_dt.Rows(x)("bSendTask") = False
                        End If


                    End If
                ElseIf drTask(1) = "Order Templates" Then ''drTask(1) for sFieldName
                    If Not IsNothing(arrRadiology) Then
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
                                        ._VisitID = m_VisitID
                                        ._VisitDate = Now
                                        .WindowState = FormWindowState.Maximized
                                        .BringToFront()
                                        .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                        'Change made to solve memory Leak and word crash issue
                                        .Close()
                                        .Dispose()
                                    End With
                                    frm = Nothing
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
                                        ._VisitID = m_VisitID
                                        ._VisitDate = Now
                                        .WindowState = FormWindowState.Minimized
                                        .Opacity = 0
                                        .Show()
                                        .Hide()
                                        .SaveOrders()
                                        .Close()

                                    End With
                                    frm = Nothing 'Change made to solve memory Leak and word crash issue
                                End If
                            Else
                                If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                    If Not IsNothing(arrRadiology) Then
                                        If arrRadiology.Count > 0 Then
                                            Dim dt As DataTable
                                            dt = Nothing
                                            Dim nOrderProviderID As Int64
                                            Dim sOrderProviderName As String
                                            Dim oPatientExam As New clsPatientExams
                                            Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(_PatientID)
                                            'Change made to solve memory Leak and word crash issue
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

                                            Dim strOrders As String = ""
                                            Dim strOrder As String = ""
                                            strOrder = ""
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
                                            'Change made to solve memory Leak and word crash issue
                                            ofrm.Close()
                                            ofrm.Dispose()
                                            ofrm = Nothing

                                        End If
                                    End If
                                Else '' If False Then Save the Task.
                                    Dim strOrders As String = ""
                                    Dim strOrder As String = ""
                                    strOrder = ""
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
                                    If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(6) for sTaskusers
                                        _sUserID = drTask(8)
                                        _sTaskusers = drTask(6)
                                    End If

                                    oclsSmartDiagnosis.AddTasks("Order available", sDescription, Now.ToString(), Now.ToString(), TaskType.OrderRadiology, strOrders, _sUserID, _sTaskusers, _PatientID)
                                End If
                            End If  ''_dt.Rows(x)("bSendTask") = False
                        End If

                    End If
                ElseIf drTask(1) = "Referral Letter" Then ''drTask(1) for sFieldName
                    If Not IsNothing(arrTemplate) Then
                        If arrTemplate.Count > 0 Then
                            If Not mycaller Is Nothing Then
                                Dim _TemplateName As String = ""
                                frmPatientExam.nRefTempID = Convert.ToInt64(CType(arrTemplate.Item(0), myList).Index)
                                _TemplateName = CType(arrTemplate.Item(0), myList).Value
                                ' swaraj 01-01-2010 -- To Load Referral Templates Data'
                                Dim dtVisitRef As DataTable
                                Dim dtPatRef As DataTable

                                'check if Referrals exists against given m_VisitId
                                If Not objReferralsDBLayer.CheckReferral(m_VisitID, m_ExamID, _PatientID) Then
                                    dtVisitRef = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitID, _PatientID, m_ExamID)
                                    SaveReferrals(dtVisitRef, True, _TemplateName)
                                    If Not IsNothing(dtVisitRef) Then
                                        dtVisitRef.Dispose()
                                        dtVisitRef = Nothing
                                    End If
                                Else
                                    'if Referral Details do not exist for that m_VisitId,
                                    'Populate Referrals Details from Patient_Dtl Table
                                    dtPatRef = objReferralsDBLayer.FillControls("R", _PatientID)
                                    SaveReferrals(dtPatRef, False, _TemplateName)
                                    If Not IsNothing(dtPatRef) Then
                                        dtPatRef.Dispose()
                                        dtPatRef = Nothing
                                    End If
                                End If

                                frmSummaryofVisit.PatientTemplateID = Convert.ToInt64(CType(arrTemplate.Item(0), myList).Index)
                                Dim frm As frmSummaryofVisit = Nothing
                                Try
                                    frm = New frmSummaryofVisit(_PatientID, m_VisitID, True, m_ExamID, m_ExamFilePath, ExamProviderId, blnExamFinished, True)
                                    ''Sanjog- Added on 2011 Jan 20 to show the referrals forms
                                    If drTask(2) = True Then ''drTask(5) for bFieldStatus
                                        frm.dtDos = m_ExamDate
                                        frmExamChild = frm
                                        frm.Text = "Patient Referrals"
                                        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                        'Change made to solve memory Leak and word crash issue
                                        If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                                            frm.Close()
                                        End If
                                        If Not IsNothing(frm) Then
                                            frm.Dispose()
                                            frm = Nothing
                                        End If
                                    Else
                                        frm.dtDos = m_ExamDate
                                        frmExamChild = frm
                                        frm.Text = "Patient Referrals"
                                        frm.Opacity = 0
                                        frm.Show()
                                        frm.Visible = False
                                        frm.SaveReferrals()
                                        'frm.Close()
                                    End If
                                    frm = Nothing 'Change made to solve memory Leak and word crash issue
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
                            Else
                                frmPatientExam.nRefTempID = 0
                            End If
                        Else
                            frmPatientExam.nRefTempID = 0
                        End If
                    End If
                End If

            Next
        End If

        'Change made to solve memory Leak and word crash issue
        If Not oclsSmartDiagnosis Is Nothing Then
            oclsSmartDiagnosis.Dispose()
            oclsSmartDiagnosis = Nothing
        End If
        'If Not arrDruglist Is Nothing Then
        '    arrDruglist = Nothing
        'End If
        'If Not arrFlow Is Nothing Then
        '    arrFlow = Nothing
        'End If

        frmPatientExam.Arrlist = arrexam

        If Not arrexam Is Nothing Then
            arrexam = Nothing
        End If
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
                    Dim strRefName As String
                    Dim strFirstName As String = ""
                    Dim strMiddleName As String = ""
                    Dim strLastName As String = ""
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
                    lst.ContactTemplateName = TemplateName
                    lst.ID = frmPatientExam.nRefTempID 'TemplateID
                    lst.Index = m_ContactId 'ReferralID
                    lst.Description = strRefName '' ReferralName
                    lst.ContactFirstName = strFirstName
                    lst.ContactMiddleName = strMiddleName
                    lst.ContactLastName = strLastName
                    lst.Type = bIsPCP
                    lst.ContactFlag = ContactFlag
                    lst.TemplateResult = Nothing '' Template(Object)
                    Arrlist.Add(lst)
                    lst = Nothing 'Change made to solve memory Leak and word crash issue
                Next
                If blnRef Then
                    objReferralsDBLayer.AddData(Arrlist, m_VisitID, DateTime.Now, _PatientID, 2)
                Else
                    objReferralsDBLayer.AddData(Arrlist, m_VisitID, DateTime.Now, _PatientID, m_ExamID)

                End If

            End If
        End If


    End Sub

    Public Sub GetReferrals()
        Dim frm As frmSummaryofVisit = Nothing
        Try
            ' Dim frm As New frmSummaryofVisit(PatientID, mgnVisitID, examid, sFileName, ExamProviderId, blnExamFinished, True, "", nRefTempID)
            frm = New frmSummaryofVisit(_PatientID, m_VisitID, True, m_ExamID, m_ExamFilePath, ExamProviderId, blnExamFinished, True)
            ' nRefTempID = 0
            'frm.myCaller = 
            frm.dtDos = m_ExamDate
            frmExamChild = frm
            frm.Text = "Patient Referrals"
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            'Change made to solve memory Leak and word crash issue
            If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                frm.Close()
            End If
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            frm = Nothing
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

    End Sub

    Public Function saveDiagnosis() As Boolean
        Try
            If Validate() = False Then
                Return False
            End If
            If ValidateDiagnosis() = False Then
                Return False
            End If

            AddCommonCPTinGrid()
            With C1Dignosis
                .Col = Col_ICD9Code
                Try
                    .Select()
                Catch
                    'blank catch was throwing random exception on select()
                End Try

                Dim i As Integer
                Dim lst As myList

                Dim arrList As New ArrayList

                Dim blnOneSnoMed As Boolean
                Dim _PrevisousICD As String = ""
                Dim strICD9Code As String = ""
                Dim strICD9Desc As String = ""
                Dim strCPTCode As String = ""
                Dim strCPTDesc As String = ""
                Dim strMODCode As String = ""
                Dim strMODDesc As String = ""
                Dim nICD9Count As Integer = 0
                Dim nCPTCount As Integer = 0
                Dim nModCount As Integer = 0
                Dim intUnits As System.Decimal
                Dim strSnomedCode As String = ""
                Dim strSnomedDesc As String = ""

                Dim timed_pt, untimed_pt As String

                For i = 1 To .Rows.Count - 1
                    lst = New myList
                    Dim _Node As C1.Win.C1FlexGrid.Node

                    'If RbICD9.Checked Then
                    '    If .GetData(i, Col_ICDRevision) = 10 Then
                    '        MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return False
                    '    End If


                    'ElseIf rbICD10.Checked Then
                    '    If .GetData(i, Col_ICDRevision) = 9 Then
                    '        MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return False
                    '    End If

                    'End If
                    _Node = .Rows(i).Node

                    timed_pt = Nothing
                    untimed_pt = Nothing

                    If _Node.Level = 1 Then
                        intUnits = C1Dignosis.GetData(i, Col_Units)
                        
                    End If
                    strICD9Code = .GetData(_Node.Row.Index, Col_ICD9Code)
                    strICD9Desc = .GetData(_Node.Row.Index, Col_ICD9Desc)
                    '  If _PrevisousICD <> strICD9Code Then
                    strSnomedCode = .GetData(_Node.Row.Index, Col_SnomedCode)
                    strSnomedDesc = .GetData(_Node.Row.Index, Col_SnomedDesc)
                    '  End If

                    'if current row don't have any child means it is leaf and save to database  
                    If _Node.Children = 0 Then
                        _Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                        Dim rowno As Integer = _Node.Row.Index

                        strICD9Code = .GetData(rowno, Col_ICD9Code)
                        strICD9Desc = .GetData(rowno, Col_ICD9Desc)
                        ' If _PrevisousICD <> strICD9Code Then
                        strSnomedCode = .GetData(rowno, Col_SnomedCode)
                        strSnomedDesc = .GetData(rowno, Col_SnomedDesc)
                        'End If

                        nICD9Count = .GetData(rowno, Col_ICD9Count)
                        strCPTCode = .GetData(rowno, COl_CPTCode)

                        Dim obj As New ClsTreatmentDBLayer
                        Try
                            Using dt_pt_billing As DataTable = obj.FetchPTBillingForCPT(m_ExamID, strCPTCode, strICD9Code)
                                If dt_pt_billing IsNot Nothing Then
                                    If dt_pt_billing.Rows.Count > 0 Then
                                        timed_pt = Convert.ToString(dt_pt_billing.Rows(0)("nTimedTherapy"))
                                        untimed_pt = Convert.ToString(dt_pt_billing.Rows(0)("nUnTimedTherapy"))
                                    End If
                                End If
                            End Using
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.None, ex, gloAuditTrail.ActivityOutCome.Failure)
                        Finally
                            If Not IsNothing(obj) Then
                                obj.Dispose()
                                obj = Nothing
                            End If
                        End Try

                        strCPTDesc = .GetData(rowno, Col_CPTDesc)
                        nCPTCount = .GetData(rowno, Col_CPTCount)

                        strMODCode = .GetData(rowno, Col_ModCode)
                        strMODDesc = .GetData(rowno, Col_ModDesc)
                        nModCount = .GetData(rowno, Col_ModCount)

                        'list for ICD9,CPT and Modifier in ExamICD9CPT Table
                        lst.Code = strICD9Code
                        lst.Description = strICD9Desc
                        lst.HistoryCategory = strCPTCode

                        lst.HistoryItem = strCPTDesc
                        lst.Value = strMODCode
                        lst.ParameterName = strMODDesc
                        lst.TemplateResult = intUnits

                        lst.TimedTherapy = timed_pt
                        lst.UnTimedTherapy = untimed_pt

                        lst.ICD9Count = nICD9Count
                        lst.CPTCount = nCPTCount
                        lst.ModCount = nModCount

                        lst.SnowMadeID = strSnomedCode
                        lst.SnoDescription = strSnomedDesc
                        lst.nICDRevision = Convert.ToInt16(.GetData(rowno, Col_ICDRevision))
                        lst.IsSnomedOneToOne = blnOneSnoMed
                        arrList.Add(lst)


                        Dim drRow As DataRow = dtActiveCPTTable.NewRow()
                        drRow("sCPTCode") = strCPTCode
                        drRow("dtFromDate") = gloDateMaster.gloDate.DateAsNumber(GetVisitdate(m_VisitID))
                        drRow("dtToDate") = 0
                        dtActiveCPTTable.Rows.Add(drRow)

                    End If

                    lst = Nothing 'Change made to solve memory Leak and word crash issue
                Next
                'Check Active CPT
                Dim CPTAlert As String = gloGlobal.gloPMGlobal.getCPTDeativatedCPT(dtActiveCPTTable)
                dtActiveCPTTable.Clear()
                If (CPTAlert <> "") Then
                    Dim dResult As DialogResult = MessageBox.Show(CPTAlert, "Diagnosis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    If dResult.ToString() = "Cancel" Then
                        Return False
                    End If
                End If
                Dim clsDiagnosis As New ClsDiagnosisDBLayer '

                'save data in ExamICDCPT Table
                clsDiagnosis.SaveDiagTreatmentAssociation(m_ExamID, _PatientID, m_VisitID, arrList, Me)
                If .Row > 0 Then
                    strICD9Code = .GetData(.Row, Col_ICD9Code)
                    strICD9Desc = .GetData(.Row, Col_ICD9Desc)
                End If
                clsDiagnosis.Dispose()
                clsDiagnosis = Nothing 'Change made to solve memory Leak and word crash issue
            End With
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        End Try
    End Function

    Private Sub RemoveSelectedModifier(ByVal ICDCode As String, ByVal CPTCode As String)
        Dim oRemoveNode As C1.Win.C1FlexGrid.Node

        For i As Integer = C1Dignosis.Rows.Count - 1 To 0 Step -1

            oRemoveNode = C1Dignosis.Rows(i).Node
            If Not IsNothing(oRemoveNode) Then
                If oRemoveNode.Level = 2 Then
                    ' If C1Dignosis.Rows(i)(Col_ModCode) = "" Then
                    If ICDCode = Convert.ToString(oRemoveNode.Parent.Parent.Data).Trim And CPTCode = Convert.ToString(oRemoveNode.Parent.Data).Trim Then
                        oRemoveNode.RemoveNode()
                    End If


                    'End If
                End If

            End If


        Next
    End Sub

    Private Sub trICD9Association_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trICD9Association.MouseDown
        Try
            If e.Button = MouseButtons.Right Then

                Dim trvNode As TreeNode
                trvNode = trICD9Association.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trICD9Association.SelectedNode = trvNode
                End If
                'if added by dipak to fix bug 4346 Patient Exam > SmtDx-> Rightclick > Shows Error message
                If Not (IsNothing(trICD9Association.SelectedNode)) Then
                    If IsNothing(trICD9Association.SelectedNode.Parent) = True Then
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = Nothing
                        Exit Sub
                    End If
                End If
                If IsNothing(trICD9Association.SelectedNode) = False Then

                    If trICD9Association.SelectedNode.Parent.Text = "Tags" Then
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = cntTags
                        Exit Sub
                    End If

                    ''If trICD9Association.Nodes.Item(0).Text = trICD9Association.SelectedNode.Text Or trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Or (CType(trICD9Association.SelectedNode, myTreeNode).Key = -1) Then
                    If trICD9Association.Nodes(1).Text = trICD9Association.SelectedNode.Parent.Text Then
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = cntICD9Association
                    Else
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = Nothing
                        'treeindex = trPrescriptionDetails.SelectedNode.Index
                    End If
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If IsNothing(trICD9Association.SelectedNode.Parent) = False Then
                Dim i As Integer
                Dim j As Integer

                Dim mychildnode As myTreeNode
                '   Dim key As Long
                mychildnode = CType(trICD9Association.SelectedNode, myTreeNode)

                For i = 0 To C1Dignosis.Rows.Count - 1
                    If Convert.ToString(C1Dignosis.GetData(i, Col_ICD9Code_Description)) = trICD9Association.SelectedNode.Text Then
                        For j = i To C1Dignosis.Rows.Count - 1
                            'If CType(C1Dignosis.GetData(j, Col_ICD9LineNo), Integer) > 0 Then
                            C1Dignosis.SetData(j, Col_ICD9Count, (CType(C1Dignosis.GetData(j, Col_ICD9Count), Integer) - 1))
                            'End If
                        Next
                        C1Dignosis.Rows(i).Node.RemoveNode()
                        Exit For
                    End If

                Next

                mychildnode.Remove() 'delete from treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trICD9_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub FillGrid(ByVal myNode As myTreeNode)
        For i As Integer = 0 To C1Dignosis.Rows.Count - 1
            If Convert.ToString(C1Dignosis.GetData(i, Col_ICD9Code_Description)) = myNode.Text Then
                Exit Sub
            End If
        Next
        Dim ICD9Count As Integer = 0
        Dim _Row As Integer
        _Row = C1Dignosis.Rows.Count - 1
        If _Row > 0 Then
            If Not IsNothing(C1Dignosis.GetData(C1Dignosis.Rows.Count - 1, Col_ICD9Count)) Then
                ICD9Count = C1Dignosis.GetData(C1Dignosis.Rows.Count - 1, Col_ICD9Count)
            Else
                ICD9Count = 0
            End If
        End If

        C1Dignosis.Rows.Add()

        _Row = C1Dignosis.Rows.Count - 1
        'set the properties for newly added row
        With C1Dignosis.Rows(_Row)
            .AllowEditing = False
            .ImageAndText = True
            .Height = 24
            .IsNode = True
            .Node.Level = 0
            .Node.Data = myNode.Text
            .Node.Image = Global.gloEMR.My.Resources.Resources.icd9
            '.Node.Key = strSelectedICD9
        End With
        Dim arrICD9() As String
        arrICD9 = Split(myNode.Text, "-", 2)
        C1Dignosis.SetData(_Row, Col_ICD9Code, arrICD9.GetValue(0))
        C1Dignosis.SetData(_Row, Col_ICDRevision, myNode.Tag)
        C1Dignosis.SetData(_Row, Col_ICD9Desc, arrICD9.GetValue(1))
        C1Dignosis.SetData(_Row, Col_ICD9Count, ICD9Count + 1)
        C1Dignosis.SetData(_Row, Col_Units, 1)
    End Sub

    Private Sub AddNode(ByVal mynode As myTreeNode)

        Dim obj As New ClsTreatmentDBLayer
        'If mynode.Parent Is trICD9.Nodes.Item(0) Then
        Dim str As String
        str = mynode.Text
        Dim mytragetnode As myTreeNode
        For Each mytragetnode In trICD9Association.Nodes.Item(1).Nodes
            If mytragetnode.Text = str Then
                Exit Sub
            End If
        Next

        Dim associatenode As myTreeNode



        associatenode = mynode.Clone
        associatenode.Key = mynode.Key
        associatenode.Text = mynode.Text
        associatenode.NodeName = mynode.Text
        associatenode.Tag = mynode.Tag
        If (mynode.Tag = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode()) Then
            associatenode.ImageIndex = 5
            associatenode.SelectedImageIndex = 5
        Else
            associatenode.ImageIndex = 15
            associatenode.SelectedImageIndex = 15
        End If

        associatenode.SmartDxKey = mynode.SmartDxKey

        trICD9Association.Nodes.Item(1).Nodes.Add(associatenode)

        'swaraj 29-03-2010 -- loading the tree node data '
        Dim mychild As myTreeNode
        'Dim GeneralNode As myTreeNode


        'GeneralNode = New myTreeNode("General", 0)
        'GeneralNode.ImageIndex = 6
        'GeneralNode.SelectedImageIndex = 6
        'associatenode.Nodes.Add(GeneralNode)
        'GeneralNode = Nothing 'Change made to solve memory Leak and word crash issue

        For x As Integer = 0 To _dt.Rows.Count - 1
            mychild = New myTreeNode
            'mychild = New myTreeNode(_dt.Rows(x)("sFieldName").ToString(), -1)
            If _dt.Rows(x)("sFieldName").ToString = "CPT" Then
                mychild.Text = "CPT"
                mychild.NodeName = "CPT"
                mychild.Key = 0
                mychild.ImageIndex = 0
                mychild.SelectedImageIndex = 0
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Drugs" Then
                mychild.Text = "Drugs"
                mychild.NodeName = "Drugs"
                mychild.Key = 1
                mychild.ImageIndex = 1
                mychild.SelectedImageIndex = 1
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Patient Education" Then
                mychild.Text = "Patient Education"
                mychild.NodeName = "Patient Education"
                mychild.Key = 2
                mychild.ImageIndex = 2
                mychild.SelectedImageIndex = 2
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Tags" Then
                mychild.Text = "Tags"
                mychild.NodeName = "Tags"
                mychild.Key = 3
                mychild.ImageIndex = 3
                mychild.SelectedImageIndex = 3
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Flowsheet" Then
                mychild.Text = "Flowsheet"
                mychild.NodeName = "Flowsheet"
                mychild.ImageIndex = 9
                mychild.SelectedImageIndex = 9
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Orders and Results" Then
                mychild.Text = "Orders and Results"
                mychild.NodeName = "Orders and Results"
                mychild.Key = 4
                mychild.ImageIndex = 10
                mychild.SelectedImageIndex = 10
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Order Templates" Then
                mychild.Text = "Order Templates"
                mychild.NodeName = "Order Templates"
                mychild.Key = 5
                mychild.ImageIndex = 11
                mychild.SelectedImageIndex = 11
            ElseIf _dt.Rows(x)("sFieldName").ToString = "Referral Letter" Then
                mychild.Text = "Referral Letter"
                mychild.NodeName = "Referral Letter"
                mychild.Key = 6
                mychild.ImageIndex = 12
                mychild.SelectedImageIndex = 12
            End If
            mychild.ExpandAll()
            associatenode.Nodes.Add(mychild)
            associatenode.ExpandAll()

            mychild = Nothing 'Change made to solve memory Leak and word crash issue
        Next
        'end swaraj'

        Dim dt As DataTable = Nothing
        If associatenode.SmartDxKey > 0 Then
            dt = objclsSmartDiagnosis.FetchICD9forUpdate(associatenode.SmartDxKey)
        End If

        Dim i As Integer

        Dim bl As Boolean = False



        If IsNothing(dt) = False Then

            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item(1).ToString().Trim <> "" Then  ''''''''''''''''''If condition added by Ujwala - To skip blank node addition - as on 11112010
                    If dt.Rows(i)(12).ToString() = "True" Then
                        bl = True
                    Else
                        bl = False
                    End If
                    'add cpt items to cpt node in icd9
                    If dt.Rows(i).Item(2) = "c" Then

                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        '  Dim unitsheadernode As New myTreeNode("Units", dt.Rows(i).Item(0))
                        Dim unitsmynode As New myTreeNode("Units: ", dt.Rows(i).Item(0))
                        unitsmynode.UnitsKey = "Units"
                        unitsmynode.ForeColor = Color.Blue

                        Dim modmynode As New myTreeNode("Modifiers", dt.Rows(i).Item(0))
                        modmynode.ModifierKey = "M1"
                        modmynode.ForeColor = Color.Blue

                        cptmynode.Checked = bl


                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "CPT" Then
                                oNode.Nodes.Add(cptmynode)
                                unitsmynode.Text = "Units: 1"

                                cptmynode.Nodes.Add(unitsmynode)
                                cptmynode.Nodes.Add(modmynode)
                                modmynode.Nodes.Clear()
                                cptmynode.Parent.Expand()
                                ''Added by Mayuri:#20150107
                                Dim strICD9 As String()
                                Dim strCPT As String()
                                Dim dtMod As DataTable
                                Dim dtUnit As DataTable
                                strICD9 = Split(associatenode.Name, "-", 2)
                                strCPT = Split(cptmynode.Name, "-", 2)
                                dtUnit = obj.FetchModforUpdate(m_ExamID, strICD9.GetValue(0), Convert.ToString(strICD9.GetValue(1)).Trim, strCPT.GetValue(0), strCPT.GetValue(1), True)
                                If Not IsNothing(dtUnit) Then
                                    If dtUnit.Rows.Count > 0 Then
                                        If dtUnit.Rows(0)(5) <> 0 Then
                                            unitsmynode.Text = "Units: " & FormatNumber(dtUnit.Rows(0)(5))
                                        Else
                                            unitsmynode.Text = "Units: 1"
                                        End If
                                    End If

                                End If
                                If Not IsNothing(dtUnit) Then
                                    dtUnit.Dispose()
                                    dtUnit = Nothing
                                End If
                                '' To Get CPTs of the Selected ICD9
                                If gblnICD9Driven Then
                                    dtMod = obj.FetchModforUpdate(m_ExamID, Convert.ToString(strICD9.GetValue(0)).Trim, Convert.ToString(strICD9.GetValue(1)), Convert.ToString(strCPT.GetValue(0)).Trim, Convert.ToString(strCPT.GetValue(1)), False, True)
                                    If Not IsNothing(dtMod) Then
                                        If dtMod.Rows.Count > 0 Then
                                            For k As Integer = 0 To dtMod.Rows.Count - 1
                                                If dtMod.Rows(k)(3) <> "" Then
                                                    Dim mymodnode As New myTreeNode

                                                    mymodnode.Text = dtMod.Rows(k)(2) & " - " & Convert.ToString(dtMod.Rows(k)(3)).Trim
                                                    mymodnode.Key = dtMod.Rows(k)(4)
                                                    mymodnode.ModifierCode = dtMod.Rows(k)(2)
                                                    ' mymodnode.Tag = dtMod.Rows(k)(4)
                                                    mymodnode.ModifierKey = "999"
                                                    modmynode.Nodes.Add(mymodnode)
                                                    mymodnode = Nothing
                                                End If




                                            Next
                                        End If
                                    End If
                                    If Not IsNothing(dtMod) Then
                                        dtMod.Dispose()
                                        dtMod = Nothing
                                    End If

                                Else
                                    dtMod = obj.FetchModforUpdate(m_ExamID, strICD9.GetValue(0), Convert.ToString(strICD9.GetValue(1)).Trim, strCPT.GetValue(0), strCPT.GetValue(1), False, False)
                                    If Not IsNothing(dtMod) Then
                                        If dtMod.Rows.Count > 0 Then
                                            For k As Integer = 0 To dtMod.Rows.Count - 1
                                                If Convert.ToString(dtMod.Rows(k)(3)).Trim <> "" Then
                                                    Dim mymodnode As New myTreeNode

                                                    mymodnode.Text = dtMod.Rows(k)(2) & " - " & Convert.ToString(dtMod.Rows(k)(3)).Trim
                                                    mymodnode.Key = dtMod.Rows(k)(4)
                                                    mymodnode.ModifierCode = dtMod.Rows(k)(2)
                                                    ' mymodnode.Tag = dtMod.Rows(k)(4)
                                                    mymodnode.ModifierKey = "999"
                                                    modmynode.Nodes.Add(mymodnode)
                                                    mymodnode = Nothing
                                                End If




                                            Next
                                        End If
                                    End If
                                    If Not IsNothing(dtMod) Then
                                        dtMod.Dispose()
                                        dtMod = Nothing
                                    End If
                                End If

                                ''
                                Exit For
                            End If
                        Next

                        cptmynode.Collapse()
                        modmynode.ExpandAll()
                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue
                        modmynode = Nothing
                        'end swaraj'

                        If gblnICD9Driven Then
                            Load_Treatment(associatenode)
                        Else
                            Load_CPTDrivenTreatment(associatenode)
                        End If

                        'add drug items to drug node in icd9
                    ElseIf dt.Rows(i).Item(2) = "d" Then
                        'associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim _strDrugNM As String = ""
                        Dim _ndrugId As Long = Convert.ToInt64(dt.Rows(i).Item(0))
                        Dim _ndcCode As String = Convert.ToString(dt.Rows(i).Item(7))
                        Dim _mpid As Int32 = Convert.ToInt32(dt.Rows(i).Item(10))
                        If dt.Rows(i).Item(1) <> "" Then
                            _strDrugNM = dt.Rows(i).Item(1)
                        End If

                        Dim cptmynode As New myTreeNode(_strDrugNM, _ndrugId, _ndcCode, _mpid)

                        cptmynode.Checked = bl
                        ' cptmynode.ExpandAll()
                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Drugs" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        'end swaraj'
                        cptmynode = Nothing
                        'add PE items to PE node in icd9
                    ElseIf dt.Rows(i).Item(2) = "p" Then

                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = bl
                        cptmynode.ExpandAll()
                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Patient Education" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue
                        'end swaraj'
                        ' associatenode.Nodes.Item(2).Nodes.Add(cptmynode)

                        Call Load_PatientEducation(associatenode)

                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "t" Then
                        'associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim indtempname As Integer = dt.Rows(i).Item(1).ToString().LastIndexOf("-[")
                        Dim cptmynode As myTreeNode
                        If indtempname > -1 Then
                            Dim strtempname As String = ""
                            strtempname = dt.Rows(i).Item(1).ToString().Substring(0, indtempname)
                            cptmynode = New myTreeNode(strtempname, dt.Rows(i).Item(0))
                            cptmynode.Text = dt.Rows(i).Item(1)
                            cptmynode.NodeName = strtempname
                        Else
                            cptmynode = New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                            cptmynode.NodeName = dt.Rows(i).Item(1)
                        End If

                        'Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = bl

                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Tags" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue

                        'end swaraj'

                    ElseIf dt.Rows(i).Item(2) = "f" Then

                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = bl
                        cptmynode.Tag = dt.Rows(i).Item(0)

                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Flowsheet" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue
                        'end swaraj'

                    ElseIf dt.Rows(i).Item(2) = "l" Then
                        '  associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = bl
                        cptmynode.Tag = dt.Rows(i).Item(0)

                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Orders and Results" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        'end swaraj'
                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue
                    ElseIf dt.Rows(i).Item(2) = "o" Then
                        ' associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = bl
                        cptmynode.Tag = dt.Rows(i).Item(0)

                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Order Templates" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        'end swaraj'
                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue

                    ElseIf dt.Rows(i).Item(2) = "r" Then
                        ' associatenode.Nodes.Item(7).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = bl
                        cptmynode.Tag = dt.Rows(i).Item(0)

                        'swaraj 29-03-2010 -- loading the tree node order sequentially '
                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Referral Letter" Then

                                oNode.Nodes.Add(cptmynode)
                                oNode.ExpandAll()
                                Exit For
                            End If
                        Next
                        'end swaraj'

                        cptmynode = Nothing 'Change made to solve memory Leak and word crash issue

                    End If

                End If ''''''''''''''''''If condition added by Ujwala - To skip blank node addition - as on 11112010
            Next
        End If
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If
        '  trICD9Association.ExpandAll()
        trICD9Association.Select()
        associatenode.Expand()
        'Ensure the newly created node is visible to the user and select it
        associatenode.EnsureVisible()
        trICD9Association.SelectedNode = associatenode


        If IsFormLoading = False Then
            If gblnICD9Driven Then
                FillGrid(mynode)
            End If


        End If

        Try
            ''changes made for case CAS-22491-V7N1J0 ,CAS-22010-W4X9M5 smart DX issue
            If (blndonotcallondiagadd = False) Then
                CheckAllParentNodes()
            Else

                ''added for case CAS-24364-L1C3M6
                Try


                    For Each _ICD As myTreeNode In trICD9Association.Nodes(1).Nodes
                        For Each _CPT As myTreeNode In _ICD.Nodes
                            If _CPT.Text = "CPT" Then
                                setgriddata(_CPT)

                            End If

                        Next
                    Next
                Catch ex As Exception

                End Try


            End If
        Catch ex As Exception
        Finally
            blndonotcallondiagadd = False
        End Try


        ' CollapseCPTNodes(associatenode)

    End Sub
    Private Sub setgriddata(ByVal ocpt As TreeNode)
        Dim nMaxICD9Count As Integer = 0
        Dim ofNode As C1.Win.C1FlexGrid.Node
        Dim NewRow As Integer = 0
        Dim ChildRow As Integer = 0
        Dim arrstrConctCPT() As String
        Dim arrstrConctICD9() As String
        '  Dim odiagnode As TreeNode = trICD9Association.Nodes(1).Nodes(0)




        For Each ocurrnode As TreeNode In ocpt.Nodes



            'If (Not ocurrnode.Parent Is Nothing) Then
            '    If (ocurrnode.Parent.Text = "CPT") Then
            If ocurrnode.Checked = True Then

                '' FOR ICD9 DRIVEN ONLY ''
                If gblnICD9Driven = True Then
                    With C1Dignosis
                        For i As Integer = 0 To .Rows.Count - 1
                            ofNode = .Rows(i).Node

                            If Not IsNothing(ofNode) Then
                                If Not IsNothing(ofNode.Parent) Then
                                    If Convert.ToString(ofNode.Parent.Data).Trim = ocurrnode.Parent.Parent.Text.Trim Then
                                        If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = Convert.ToString(ocurrnode.Text) Then
                                            ofNode = .Rows(i).Node
                                            If ofNode.Children > 0 Then
                                                If ofNode.Nodes(0).Level = 2 Then
                                                    Exit Sub
                                                End If
                                                For j As Integer = ofNode.Row.Index To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                                    If Convert.ToString(.GetData(j, Col_ICD9Code_Description)) = Convert.ToString(ocurrnode.Parent.Parent.Text) Then
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
                                                    .Node.Image = Global.gloEMR.My.Resources.Resources.icd9
                                                    .Node.Data = ocurrnode ''
                                                End With

                                                ''arrstrConctCPT = Split(ocurrnode.Parent.Parent.Text, "-", 2)
                                                ''arrstrConctICD9 = 
                                                arrstrConctCPT = Split(ocurrnode.Text, "-", 2)
                                                arrstrConctICD9 = Split(ocurrnode.Parent.Parent.Text, "-", 2)



                                                .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                                .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                                .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                                .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                                                .SetData(NewRow, Col_ICD9Count, .GetData(i, Col_ICD9Count))
                                                .SetData(NewRow, Col_CPTCount, .GetData(i, Col_CPTCount) + 1)
                                                .SetData(NewRow, Col_ICDRevision, ocurrnode.Parent.Parent.Tag)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                End If
                            End If



                        Next
                        ''Add checked CPT node informationn to C1Diagnosis   as a child to selected ICD 

                        If .Rows.Count - 1 > 0 Then
                            nMaxICD9Count = .GetData(.Rows.Count - 1, Col_ICD9Count)
                        Else
                            nMaxICD9Count = 0
                        End If '

                        For i As Integer = 0 To .Rows.Count - 1
                            If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = ocurrnode.Parent.Parent.Text Then
                                nMaxICD9Count = .GetData(i, Col_ICD9Count)
                                Exit For
                            End If
                        Next
                        '  Dim ChildCount As Integer = 0 'Code changes For CAS-09339-L9X5J8
                        For i As Integer = 0 To .Rows.Count - 1
                            If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = Convert.ToString(ocurrnode.Parent.Parent.Text) Then
                                ofNode = .Rows(i).Node
                                If ofNode.Children > 0 Then
                                    ' ChildCount = ofNode.Children 'Code changes For CAS-09339-L9X5J8
                                    For j As Integer = ofNode.Row.Index To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Convert.ToString(.GetData(j, Col_ICD9Code_Description)) = Convert.ToString(ocurrnode.Text) Then
                                            Exit Sub
                                        End If
                                    Next
                                End If
                            End If
                        Next

                        Dim ParentRow As Integer = 0
                        For i As Integer = 0 To .Rows.Count - 1
                            '''''''''''''
                            If Convert.ToString(.GetData(i, Col_ICD9Code_Description)) = Convert.ToString(ocurrnode.Parent.Parent.Text) Then
                                ParentRow = i

                                Dim nc As Integer = ocurrnode.Parent.Parent.GetNodeCount(False)
                                Dim txtNode As C1.Win.C1FlexGrid.Node
                                txtNode = .Rows(i).Node
                                ParentRow += txtNode.Children
                                Exit For
                            End If
                        Next

                        Dim oRow As C1.Win.C1FlexGrid.Row
                        If ParentRow = 0 Then
                            .Rows.Add()
                            NewRow = .Rows.Count - 1
                        Else
                            'oRow = .Rows.Insert(ParentRow + ChildCount + 1) 'Code changes For CAS-09339-L9X5J8
                            '  oRow = .Rows.Insert(.Rows.Count)
                            oRow = .Rows.Insert(ParentRow + 1)
                            NewRow = oRow.Index
                        End If


                        With .Rows(NewRow)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                            .Node.Data = ocurrnode.Text
                        End With

                        ''Set data for checked cpt  in C1Diagnosis

                        arrstrConctICD9 = Split(ocurrnode.Parent.Parent.Text, "-", 2)
                        .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                        .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                        .SetData(NewRow, Col_ICD9Count, nMaxICD9Count + 1)
                        .SetData(NewRow, Col_ICDRevision, ocurrnode.Parent.Parent.Tag)
                        '
                        arrstrConctCPT = Split(ocurrnode.Text, "-", 2)

                        .SetData(NewRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                        .SetData(NewRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                        .SetData(NewRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                        .SetData(NewRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))
                        .SetData(NewRow, Col_ICD9Count, nMaxICD9Count)
                        .SetData(NewRow, Col_CPTCount, 1)
                        If ocurrnode.Nodes.Count > 0 Then
                            Dim unitsnode As New myTreeNode
                            unitsnode = ocurrnode.Nodes(0)
                            .SetData(NewRow, Col_Units, unitsnode.Text.Replace("Units: ", ""))
                            unitsnode = Nothing
                        Else
                            .SetData(NewRow, Col_Units, 1)
                        End If


                        If ocurrnode.Nodes.Count > 1 Then


                            Dim unitsnode As New myTreeNode
                            unitsnode = ocurrnode.Nodes(0)

                            Dim modnode As myTreeNode
                            Dim oldmodnode As myTreeNode
                            Dim arrstrMod() As String
                            If ocurrnode.Nodes(1).Nodes.Count > 0 Then
                                Dim oRow1 As C1.Win.C1FlexGrid.Row
                                If NewRow = 0 Then
                                    .Rows.Add()
                                    ChildRow = .Rows.Count - 1
                                Else
                                    oRow1 = .Rows.Insert(NewRow + 1)
                                    ChildRow = oRow1.Index
                                End If

                                For k As Integer = 0 To ocurrnode.Nodes(1).Nodes.Count - 1

                                    If k > 0 Then

                                        ChildRow = ChildRow + 1
                                        C1Dignosis.Rows.Insert(ChildRow)
                                    End If
                                    '  NewRow = NewRow + 1
                                    With .Rows(ChildRow)
                                        .AllowEditing = False
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 2
                                        .Node.Image = Global.gloEMR.My.Resources.Resources.Modifier
                                        .Node.Data = ocurrnode.Nodes(1).Nodes(k).Text
                                    End With



                                    arrstrMod = Split(ocurrnode.Nodes(1).Nodes(k).Text, "-", 2)
                                    oldmodnode = New myTreeNode
                                    oldmodnode = ocurrnode.Nodes(1).Nodes(k)
                                    modnode = New myTreeNode
                                    modnode = ocurrnode.Nodes(1).Nodes(k)

                                    modnode.Key = oldmodnode.Key
                                    modnode.ModifierCode = oldmodnode.ModifierCode
                                    modnode.ModifierKey = oldmodnode.ModifierKey

                                    .SetData(ChildRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                    .SetData(ChildRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                    .SetData(ChildRow, Col_ICD9Count, nMaxICD9Count)
                                    .SetData(ChildRow, Col_ICDRevision, ocurrnode.Parent.Parent.Tag)

                                    .SetData(ChildRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                    .SetData(ChildRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))

                                    .SetData(ChildRow, Col_CPTCount, 1)


                                    .SetData(ChildRow, Col_ModCode, modnode.ModifierCode)
                                    .SetData(ChildRow, Col_ModDesc, arrstrMod.GetValue(1))
                                    .SetData(ChildRow, Col_ModCount, 1)

                                    .SetData(ChildRow, Col_Units, unitsnode.Text.Replace("Units: ", ""))

                                    modnode = Nothing
                                    oldmodnode = Nothing

                                Next
                                unitsnode = Nothing
                            End If
                        End If


                        ''
                        'Exit Sub
                    End With
                End If


            Else


            End If
            '    End If
            'End If

        Next

    End Sub



    Dim blndonotcallondiagadd As Boolean = False
    Private Sub CollapseCPTNodes(ByVal _associatenode As myTreeNode, Optional ByVal _IsPrimary As Boolean = False)
        For Each _mynode As myTreeNode In _associatenode.Nodes
            If _mynode.Text = "CPT" Then
                If _mynode.Nodes.Count > 0 Then
                    For Each _oCPTHeaderchild As myTreeNode In _mynode.Nodes
                        _oCPTHeaderchild.Collapse()
                        'If _IsPrimary = True Then
                        If _oCPTHeaderchild.Nodes.Count > 0 Then
                            For Each _oCPTChild As myTreeNode In _oCPTHeaderchild.Nodes
                                If _oCPTChild.Text.Contains("Units") Then
                                    _oCPTChild.UnitsKey = "Units"
                                    Exit For
                                End If
                            Next
                        End If
                        If _oCPTHeaderchild.Nodes.Count > 1 Then

                            For Each _oCPTChild As myTreeNode In _oCPTHeaderchild.Nodes
                                ' _oCPTChild.Expand()
                                If _IsPrimary Then
                                    If _oCPTChild.Text = "Modifiers" Then
                                        _oCPTChild.ExpandAll()
                                        _oCPTChild.ModifierKey = "M1"
                                        If _oCPTChild.Nodes.Count > 0 Then
                                            For Each oModNode As myTreeNode In _oCPTChild.Nodes
                                                oModNode.ModifierKey = "999"
                                            Next
                                        End If

                                    End If
                                    If _oCPTChild.Text.Contains("Units") Then
                                        _oCPTChild.UnitsKey = "Units"
                                    End If
                                End If


                            Next
                        End If
                        'Else

                        '    If _oCPTHeaderchild.Nodes.Count > 1 Then

                        '        For Each _oCPTChild As myTreeNode In _oCPTHeaderchild.Nodes
                        '            _oCPTChild.Expand()


                        '        Next
                        '    End If
                        'End If




                    Next
                    '_mynode.Collapse()
                End If

            End If
        Next
    End Sub

    Public Sub FillNodes() ' it checks the nodes to which patient visited and saved 
        Dim flsht2 As TreeNode
        Dim lbordernode As TreeNode
        Dim ordernode As TreeNode
        Dim reff As TreeNode
        Dim PtEdu As TreeNode
        Dim drg As TreeNode

        For Each ptn As TreeNode In trICD9Association.Nodes 'Check In Top Most Parent Nodes
            For Each childptn As TreeNode In ptn.Nodes   'Check In Top Most Parent's  Child Nodes
                For Each innerchildptn As TreeNode In childptn.Nodes ''Check In Top Most Parent Child's Child Nodes

                    If innerchildptn.Text = "FlowSheet" Then
                        flsht2 = innerchildptn
                        checkflosheetnode(flsht2)
                    ElseIf innerchildptn.Text = "Orders and Results" Then
                        lbordernode = innerchildptn
                        checklbordernode(lbordernode)

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

    'It checks the nodes inside the Flowsheet
    Private Sub checkflosheetnode(ByVal flsht As TreeNode)
        Dim dtFlowsheet As DataTable
        dtFlowsheet = objclsSmartDiagnosis.FetchFlowsheetFromFlowsheet(m_VisitID)
        If Not IsNothing(dtFlowsheet) Then
            For Each flshtnode As TreeNode In flsht.Nodes

                Dim i As Int16
                flshtnode.Checked = False

                For i = 0 To dtFlowsheet.Rows.Count - 1
                    If flshtnode.Text = dtFlowsheet.Rows(i)(0).ToString() Then
                        '' If ICD9COde-Discription Mathches with TreeNode then 
                        '' then add that ICD9 to associated treeview
                        flshtnode.Checked = True
                        Exit For
                    End If
                Next

            Next
        End If
        If Not IsNothing(dtFlowsheet) Then
            dtFlowsheet.Dispose()
            dtFlowsheet = Nothing
        End If
    End Sub

    'It checks the nodes inside the Order
    Private Sub checkordernode(ByVal ordnode As TreeNode)
        Dim dtOrder As DataTable
        dtOrder = objclsSmartDiagnosis.FetchOrder(m_VisitID)
        If Not IsNothing(dtOrder) Then
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
        End If
        If Not IsNothing(dtOrder) Then
            dtOrder.Dispose()
            dtOrder = Nothing
        End If
    End Sub
    Private Sub checkDrugsnode(ByVal drgnode As TreeNode)
        Dim dtDrug As DataTable
        Dim objclsgloEmrPrescription As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)

        Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
            dtDrug = helper.GetPrescriptionsByPatient(_PatientID, Now.Date, m_VisitID)
        End Using

        '   dtDrug = objclsgloEmrPrescription.FetchPrescriptionforView(m_VisitID)
        Dim i As Int16

        If Not IsNothing(dtDrug) Then
            For Each drugnode As TreeNode In drgnode.Nodes
                drugnode.Checked = False
                For i = 0 To dtDrug.Rows.Count - 1
                    If drugnode.Text.Trim() = dtDrug.Rows(i)(4).ToString().Trim() Then
                        '' If ICD9COde-Discription Mathches with TreeNode then 
                        '' then add that ICD9 to associated treeview
                        drugnode.Checked = True
                        Exit For
                    End If
                Next

            Next
        End If
        If (IsNothing(objclsgloEmrPrescription) = False) Then
            objclsgloEmrPrescription.Dispose()
            objclsgloEmrPrescription = Nothing
        End If
        If Not IsNothing(dtDrug) Then
            dtDrug.Dispose()
            dtDrug = Nothing
        End If
    End Sub
    'It checks the nodes inside the LabOrder
    Private Sub checklbordernode(ByVal lbordnode As TreeNode)
        Dim dtlabord As DataTable
        dtlabord = objclsSmartDiagnosis.FetchLaborderName(m_VisitID)

        If Not IsNothing(dtlabord) Then
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
        End If
        If Not IsNothing(dtlabord) Then
            dtlabord.Dispose()
            dtlabord = Nothing
        End If
    End Sub
    'It checks the nodes inside the Referral
    Private Sub checkreffnode(ByVal reff As TreeNode)
        Dim dtreff As DataTable
        dtreff = objclsSmartDiagnosis.FetchReferralNameFromReferral(m_VisitID)
        If Not IsNothing(dtreff) Then
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
        End If
        If Not IsNothing(dtreff) Then
            dtreff.Dispose()
            dtreff = Nothing
        End If
    End Sub

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
    Private Sub CheckAllParentNodes()
        Dim innerchildflag As Boolean = False
        Dim outerchildflag As Boolean = False
        Dim parentflag As Boolean = False

        For Each ptn As TreeNode In trICD9Association.Nodes.Item(1).Nodes
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

                If otherptn.Text = "CPT" Then
                    For Each _node As TreeNode In otherptn.Nodes
                        If _node.Checked Then
                            _node.Checked = True
                        End If
                    Next
                End If
            Next

            If outerchildflag = False And ptn.Nodes.Count > 0 Then
                ptn.Checked = True
            Else

                parentflag = True
            End If
            outerchildflag = False
        Next

    End Sub


    Private Function CheckCommonValidation() As Boolean

        Dim result As Boolean = True
        Try
            If C1Dignosis IsNot Nothing AndAlso C1Dignosis.Rows.Count > 1 Then
                For i As Integer = 1 To C1Dignosis.Rows.Count - 1
                    If RbICD9.Checked Then
                        If C1Dignosis.GetData(i, Col_ICDRevision) = 10 Then
                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            result = False
                            Exit For
                        End If
                    ElseIf rbICD10.Checked Then
                        If C1Dignosis.GetData(i, Col_ICDRevision) = 9 Then
                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            result = False
                            Exit For
                        End If

                    End If
                Next
            Else
                result = True
            End If

            If IsMultipleReferralLetter() = True And result = True Then
                MessageBox.Show("Please select only one Referral ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                result = False
            End If

            Return result

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function



    Private Sub tblSmartDiagnosis_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblSmartDiagnosis.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"

                    If (CheckCommonValidation()) Then
                        If IsEnableReviewScreen() Then
                            If ShowSmartReview() = False Then
                                Exit Sub
                            End If
                        End If
                        Call SaveAssociation()
                        Call SmartDxAssociationWithExam()
                    End If

                Case "Close"
                    frmPatientExam.nRefTempID = 0
                    '   Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub trICD9Association_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trICD9Association.BeforeCheck
        If IsFormLoading = False Then
            If _IsSkipCheckEvent Then
                Exit Sub
            End If
            Dim ofNode As C1.Win.C1FlexGrid.Node
            If Not IsNothing(e.Node.Parent) Then
                If e.Node.Parent.Text = "CPT" Then
                    If gblnICD9Driven Then


                        If e.Node.Checked = True Then
                            With C1Dignosis
                                For i As Integer = 0 To .Rows.Count - 1

                                    ofNode = Nothing
                                    ''
                                    ofNode = .Rows(i).Node
                                    If Not IsNothing(ofNode) Then
                                        If Not IsNothing(ofNode.Parent) Then
                                            If Convert.ToString(ofNode.Parent.Data).Trim = e.Node.Parent.Parent.Text.Trim Then
                                                ''
                                                If Convert.ToString(C1Dignosis.GetData(i, Col_ICD9Code_Description)) = e.Node.Text Then

                                                    ofNode.RemoveNode()
                                                    RearrangeICD9COunt()
                                                    Exit Sub
                                                End If

                                            End If
                                        End If
                                    End If




                                Next
                            End With
                        End If
                    End If
                ElseIf e.Node.Parent.Text = "Common" Then
                    If e.Node.Checked = True Then
                        RemoveselectedCommonCPT(e.Node)
                        Exit Sub
                    End If
                End If
            End If
            'If gblnICD9Driven = False Then
            '    Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            '    For i As Integer = arrCPTDrivenDiagnosis.Count - 1 To 0 Step -1
            '        oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
            '        If Not IsNothing(e.Node.Parent) Then
            '            If e.Node.Parent.Text = "CPT" Then
            '                If e.Node.Checked = True Then
            '                    If oList.HistoryCategory.Trim = Convert.ToString(e.Node.Text.Split("-").GetValue(0)).Trim Then
            '                        arrCPTDrivenDiagnosis.Remove(oList)
            '                    End If

            '                End If
            '            End If
            '        End If

            '    Next
            'End If
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
                .SetData(i, Col_CPTCount, cnt)
            Next
        End With
    End Sub

    Private Sub mnuSetasPrimary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetasPrimary.Click
        Try
            trICD9Association.BeginUpdate()
            Dim selNode As myTreeNode
            selNode = CType(trICD9Association.SelectedNode, myTreeNode)
            Dim NewNode As myTreeNode
            NewNode = selNode.Clone()
            NewNode.Key = selNode.Key
            NewNode.Text = selNode.Text
            NewNode.NodeName = selNode.Text
            NewNode.SmartDxKey = selNode.SmartDxKey

            If Not IsNothing(selNode) Then

                trICD9Association.Nodes(1).Nodes.Insert(0, NewNode)
                For i As Integer = 0 To C1Dignosis.Rows.Count - 1
                    If Convert.ToString(C1Dignosis.GetData(i, Col_ICD9Code_Description)) = selNode.Text Then
                        C1Dignosis.Rows(i).Node.Move(C1.Win.C1FlexGrid.NodeMoveEnum.First, C1Dignosis.Rows(0).Node)
                        Exit For
                    End If
                Next
            End If
            Dim ICD9Count As Integer = 0
            For i As Integer = 1 To C1Dignosis.Rows.Count - 1
                If IsNothing(C1Dignosis.GetData(i, Col_CPTCount)) Then
                    ICD9Count = ICD9Count + 1
                    C1Dignosis.SetData(i, Col_ICD9Count, ICD9Count)
                Else
                    C1Dignosis.SetData(i, Col_ICD9Count, ICD9Count)
                End If
            Next
            selNode.Remove()
            trICD9Association.SelectedNode = NewNode
            ' trICD9Association.ExpandAll()
            NewNode.ExpandAll()
            CollapseCPTNodes(NewNode, True)

            '  CollapseCPTNodes(NewNode, True)
            trICD9Association.EndUpdate()
        Catch ex As Exception

        End Try
    End Sub




    Private Sub GloUC_trvICD_NodeMouseDoubleClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvICD.NodeMouseDoubleClick
        Dim oSmartDx As smartDx = Nothing
        Try
            blndonotcallondiagadd = True
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode)

            Dim mytragetnode As myTreeNode
            For Each mytragetnode In trICD9Association.Nodes.Item(1).Nodes
                If mytragetnode.Text = oNode.Text Then
                    MessageBox.Show("Diagnosis code is already selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Next
            oSmartDx = New smartDx()
            nSmartDxID = GetSelectedSmartDxID(oNode.ID, oNode.Text)
            If nSmartDxID > -1 Then
                sSmartDXName = oSmartDx.getAssociatedSmartDxName(nSmartDxID)
                Dim mynode As New myTreeNode
                mynode.Key = oNode.ID
                mynode.Text = oNode.Text
                mynode.Tag = oNode.ICDRevision
                mynode.SmartDxKey = nSmartDxID
                '  mynode.Tag = GloUC_trvICD.Tag
                If Not IsNothing(mynode) Then
                    '  _IsNodeDoubleClick = True

                    AddNode(mynode)

                    '  _IsNodeDoubleClick = False
                End If
                mynode = Nothing 'Change made to solve memory Leak and word crash issue          
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oSmartDx IsNot Nothing Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If
        End Try
    End Sub

    Private Sub GloUC_trvICD_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvICD.KeyPress
        Dim oSmartDx As smartDx = Nothing
        If (e.KeyChar = ChrW(13)) AndAlso GloUC_trvICD.SelectedNode IsNot Nothing Then
            Try
                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode)
                Dim mytragetnode As myTreeNode
                For Each mytragetnode In trICD9Association.Nodes.Item(1).Nodes
                    If mytragetnode.Text = oNode.Text Then
                        MessageBox.Show("Diagnosis code is already selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Next
                Dim mynode As New myTreeNode
                oSmartDx = New smartDx()
                nSmartDxID = GetSelectedSmartDxID(oNode.ID, oNode.Text)
                If nSmartDxID > -1 Then
                    sSmartDXName = oSmartDx.getAssociatedSmartDxName(nSmartDxID)
                    If Not IsNothing(oNode) Then
                        mynode.Key = oNode.ID
                        mynode.Text = oNode.Text
                        mynode.Tag = oNode.ICDRevision
                        mynode.SmartDxKey = nSmartDxID
                        If Not IsNothing(mynode) Then
                            '  _IsNodeDoubleClick = True

                            AddNode(mynode)

                            ' _IsNodeDoubleClick = False
                        End If
                        mynode = Nothing 'Change made to solve memory Leak and word crash issue
                    End If
                End If




            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If oSmartDx IsNot Nothing Then
                    oSmartDx.Dispose()
                    oSmartDx = Nothing
                End If
            End Try
        End If
    End Sub

#Region " CPT Driven Load / Save "
    Private Sub Load_CPTDrivenDiagnosis()
        Try
            Dim oDiagnosis As New ClsDiagnosisDBLayer
            arrCPTDrivenDiagnosis = oDiagnosis.GetCPTDrivenDiagnosis(m_ExamID, m_VisitID, _PatientID)
            oDiagnosis.Dispose()
            oDiagnosis = Nothing 'Change made to solve memory Leak and word crash issue

            If arrCPTDrivenDiagnosis IsNot Nothing Then
                If arrCPTDrivenDiagnosis.Count > 0 Then

                    '' FETCH FOR ALREADY SAVED ICD9s, AND STORE IT IN ARRAYLIST ''
                    Dim arrAvailableICD9s As New ArrayList
                    '  Dim _ICD9 As String
                    Dim oICD9 As gloEMRGeneralLibrary.gloGeneral.myList
                    If arrCPTDrivenDiagnosis.Count >= 0 Then
                        If CType(arrCPTDrivenDiagnosis(0), gloEMRGeneralLibrary.gloGeneral.myList).nICDRevision = 10 Then
                            rbICD10.Checked = True
                        Else
                            RbICD9.Checked = True
                        End If
                    End If
                    For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                        oICD9 = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                        'If oICD9.Code <> "" And arrAvailableICD9s.Contains(oICD9.Code) = False Then

                        '    arrAvailableICD9s.Add(oICD9)
                        'End If

                        If oICD9.Code <> "" And arrAvailableICD9s.Contains(oICD9.Code & " - " & oICD9.Description) = False Then
                            'arrAvailableICD9s.Add(oICD9.Code & " - " & oICD9.Description)
                            arrAvailableICD9s.Add(oICD9.Code)
                        End If
                    Next
                    oICD9 = Nothing 'Change made to solve memory Leak and word crash issue

                    '' NOW SEARCH WHETHER STORED ICD9s ARE PRESENT IN TEMPLATES OR NOT ''
                    '' IF TEMPLATE FOUND ADD IT ''
                    Dim _oICD9Node() As String
                    If arrAvailableICD9s IsNot Nothing Then
                        If arrAvailableICD9s.Count >= 0 Then
                            For i As Integer = 0 To arrAvailableICD9s.Count - 1
                                For Each oICD9Node As TreeNode In GloUC_trvICD.Nodes
                                    _oICD9Node = oICD9Node.Text.Trim.Split("-")
                                    If _oICD9Node.GetValue(0) = CType(arrAvailableICD9s(i), String) Then
                                        GloUC_trvICD.SelectedNode = oICD9Node
                                        trICD9Association.BeginUpdate()
                                        GloUC_trvICD_NodeMouseDoubleClick_1(Nothing, Nothing)
                                        If trICD9Association.SelectedNode IsNot Nothing Then
                                            Load_CPTDrivenTreatment(trICD9Association.SelectedNode)
                                            Load_Drugs(trICD9Association.SelectedNode)
                                            Load_PatientEducation(trICD9Association.SelectedNode)
                                            trICD9Association.EndUpdate()
                                        End If
                                    End If

                                Next
                            Next
                        End If
                    End If

                    'Dim oICD9Snomed As gloEMRGeneralLibrary.glogeneral.myList
                    'For i As Integer = 0 To arrAvailableICD9s.Count - 1
                    '    oICD9Snomed = CType(arrAvailableICD9s(i), gloEMRGeneralLibrary.glogeneral.myList)
                    '    For Each oICD9Node As TreeNode In GloUC_trvICD.Nodes
                    '        _oICD9Node = oICD9Node.Text.Trim.Split("-")
                    '        If _oICD9Node.Length > 1 Then

                    '            If oICD9Snomed.Code = _oICD9Node.GetValue(0) Then


                    '                GloUC_trvICD.SelectedNode = oICD9Node
                    '                GloUC_trvICD.Tag = oICD9Snomed.SnomedID + " - " + oICD9Snomed.SnomedDesc
                    '                GloUC_trvICD_NodeMouseDoubleClick_1(Nothing, Nothing)

                    '                Load_CPTDrivenTreatment(trICD9Association.SelectedNode)
                    '                Load_Drugs(trICD9Association.SelectedNode)
                    '                Load_PatientEducation(trICD9Association.SelectedNode)

                    '            End If
                    '        End If

                    '    Next
                    'Next
                Else
                    If _blnICDTransition = True Then
                        rbICD10.Checked = True
                    Else
                        RbICD9.Checked = True
                    End If
                End If
            Else
                If _blnICDTransition = True Then
                    rbICD10.Checked = True
                Else
                    RbICD9.Checked = True
                End If
                arrCPTDrivenDiagnosis = New ArrayList
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Load_CPTDrivenTreatment(ByVal oICD9Node As myTreeNode)
        Try
            For Each oNode As TreeNode In oICD9Node.Nodes
                If oNode.Text = "CPT" Then
                    Dim _CPTCode, _ICD9Code, _str As String
                    For Each oCPTNode As TreeNode In oNode.Nodes
                        _str = ""
                        _str = oCPTNode.Text
                        _CPTCode = _str.Substring(0, _str.IndexOf("-"))

                        _str = ""
                        _str = oCPTNode.Parent.Parent.Text
                        _ICD9Code = _str.Substring(0, _str.IndexOf("-"))

                        If isICD9CPT_Associated(_ICD9Code, _CPTCode) = True Then
                            oCPTNode.Checked = True
                        Else
                            If IsFormLoading Then
                                oCPTNode.Checked = False
                            End If

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
            For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList.Code.Trim = sICD9Code.Trim And oList.HistoryCategory.Trim = sCPTCode.Trim Then
                    Return True
                End If
            Next
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function
    Private Function isICD9CPTMod_Associated(ByVal sICD9Code As String, ByVal sCPTCode As String, ByVal ModCode As String, ByVal ModLine As Integer) As Boolean
        Try
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList.Code.Trim = sICD9Code And oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Value.Trim = ModCode.Trim And oList.ICD9No = ModLine Then
                    Return True
                End If
                'If oList.Code.Trim = sICD9Code.Trim And oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Value.Trim = ModCode.Trim Then
                '    Return True
                'End If
            Next
            Return Nothing
        Catch
            Return Nothing
        End Try
    End Function
    Private Sub UpdateUnit(ByVal sICD9Code As String, ByVal sCPTCode As String, ByVal ModLine As Integer, ByVal _unit As String)
        Try
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList.HistoryCategory.Trim = sCPTCode.Trim And oList.ICD9No = ModLine Then
                    If oList.TemplateResult <> _unit Then
                        oList.TemplateResult = _unit
                        ' Exit For
                    End If

                End If
                'If oList.Code.Trim = sICD9Code.Trim And oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Value.Trim = ModCode.Trim Then
                '    Return True
                'End If
            Next

        Catch

        End Try
    End Sub
    Private Sub DeleteModifiers(ByVal sCPTCode As String, ByVal arrcpt As ArrayList, ByVal ModLine As Integer)
        Try


            If Not IsNothing(arrcpt) Then
                Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
                For i As Integer = arrcpt.Count - 1 To 0 Step -1
                    oList = CType(arrcpt(i), gloEMRGeneralLibrary.gloGeneral.myList)
                    If oList.Value <> "" Then
                        If oList.Code.Trim = "" And oList.HistoryCategory.Trim = sCPTCode.Trim And oList.ICD9No = ModLine Then
                            arrcpt.Remove(oList)
                        End If
                    End If

                Next
            End If

            Dim oList1 As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = arrCPTDrivenDiagnosis.Count - 1 To 0 Step -1
                oList1 = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList1.Value <> "" Then
                    If oList1.Code.Trim = "" And oList1.HistoryCategory.Trim = sCPTCode.Trim And oList1.ICD9No = ModLine Then
                        arrCPTDrivenDiagnosis.Remove(oList1)
                    End If
                End If

            Next
            '' IF ICD9 WILL OVERFLOW THEN DON'T SEND LINE NUMBER AND LET NEW CPT LINE CREATE ''

        Catch

        End Try
    End Sub
    Private Sub AddinDiagnosis(ByVal CommonCPT As myTreeNode)
        Dim newnode As myTreeNode
        For Each _node As myTreeNode In trICD9Association.Nodes(1).Nodes
            For Each mynode As myTreeNode In _node.Nodes
                If mynode.Text = "CPT" Then
                    If IscontainInCommonCPT(mynode, CommonCPT) Then
                        '  mynode.Nodes.Remove(CommonCPT)
                        ' Exit For
                    End If
                    'If CommonCPT.Nodes.Count = 2 Then
                    '    If CommonCPT.Nodes(1).Nodes.Count > 0 Then




                    newnode = New myTreeNode
                    newnode.Text = CommonCPT.Text
                    newnode.Key = CommonCPT.Key
                    mynode.Nodes.Add(newnode)
                    RemoveHandler trICD9Association.BeforeCheck, AddressOf trICD9Association_BeforeCheck
                    RemoveHandler trICD9Association.AfterCheck, AddressOf trICD9Association_AfterCheck
                    newnode.Checked = True

                    ''
                    Dim unitsmynode As New myTreeNode("Units: ", CommonCPT.Key)
                    unitsmynode.UnitsKey = "Units"
                    unitsmynode.ForeColor = Color.Blue
                    unitsmynode.Text = CommonCPT.Nodes(0).Text
                    newnode.Nodes.Add(unitsmynode)

                    Dim modmynode As New myTreeNode("Modifiers", CommonCPT.Key)
                    modmynode.ModifierKey = "M1"
                    modmynode.ForeColor = Color.Blue

                    newnode.Nodes.Add(modmynode)
                    modmynode.Nodes.Clear()

                    _node.Parent.Expand()

                    If CommonCPT.Nodes.Count > 1 Then
                        For Each _childnode As myTreeNode In CommonCPT.Nodes(1).Nodes
                            unitsmynode.Text = CommonCPT.Nodes(0).Text
                            Dim mymodnode As New myTreeNode

                            mymodnode.Text = _childnode.Text
                            mymodnode.ModifierKey = _childnode.ModifierKey
                            mymodnode.ModifierCode = _childnode.ModifierCode
                            mymodnode.Key = _childnode.Key
                            modmynode.Nodes.Add(mymodnode)
                            mymodnode = Nothing
                        Next
                    End If
                End If
                '    End If


                'End If
                '    End If
                Exit For
                'End If
            Next
            If gblnSetCPTtoAllICD9 = False Then
                Exit For
            End If
        Next
    End Sub

    Private Function SaveCPTDrivenDiagnosis() As Boolean
        Try


            If Validate() = False Then
                Return False
            End If



            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            Dim _CPTLineNumber As Integer = -1
            Dim _CPTText, _CPTCode, _CPTDescription, _ICD9Text, _ICD9Code, _ICD9Description, _ICDRevision, strSnomedCode, strSnomedDesc, _unit As String
            Dim timed_pt, untimed_pt As String

            '', _SnomedCode, _snomedDesc, _snomedText As String
            strSnomedCode = ""
            strSnomedDesc = ""
            _unit = "1"
            '' IF CURRENT ICD9 IS CHECKED BUT NO CPT HAS CHECKED THEN TRACK IT AND ADD ONLY ICD9 WITH BLANK CPT LINE ''
            Dim _CurrentICD9Checked As Boolean = False
            Dim _isCPTFound As Boolean = False
            Dim arrCPTRevision As New ArrayList
            Dim oDiagnosis As New ClsDiagnosisDBLayer()
            arrCPTRevision = oDiagnosis.GetCPTDrivenDiagnosis(m_ExamID, m_VisitID, _PatientID)
            If arrCPTRevision IsNot Nothing Then
                If arrCPTRevision.Count >= 0 Then
                    If RbICD9.Checked Then
                        If CType(arrCPTRevision(0), gloEMRGeneralLibrary.gloGeneral.myList).nICDRevision = 10 Then
                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            oDiagnosis.Dispose()
                            oDiagnosis = Nothing
                            Return False
                        End If
                    ElseIf rbICD10.Checked Then
                        If CType(arrCPTRevision(0), gloEMRGeneralLibrary.gloGeneral.myList).nICDRevision = 9 Then
                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            oDiagnosis.Dispose()
                            oDiagnosis = Nothing
                            Return False
                        End If
                    End If

                End If
            End If
            If trICD9Association.Nodes(1).Nodes.Count > 0 Then
                For Each _node As myTreeNode In trICD9Association.Nodes(0).Nodes
                    If _node.Checked Then
                        AddinDiagnosis(_node)
                    End If
                Next

            End If
            If Not IsNothing(arrCPTRevision) = False Then
                arrCPTRevision = Nothing

            End If

            If Not IsNothing(arrCPTDrivenDiagnosis) Then
                arrCPTDrivenDiagnosis.Clear()
                arrCPTDrivenDiagnosis = Nothing
                arrCPTDrivenDiagnosis = New ArrayList
            End If

            Dim blnOneSnoMed As Boolean
            ''common CPT
            If trICD9Association.Nodes(1).Nodes.Count <= 0 Then
                AddOnlyCommonCPTinCPTDriven()
                '
            End If
            ''
            For Each oICD9Node As myTreeNode In trICD9Association.Nodes(1).Nodes
                _CurrentICD9Checked = oICD9Node.Checked
                If RbICD9.Checked Then
                    If oICD9Node.Tag = gloGlobal.gloICD.CodeRevision.ICD10 Then
                        MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        oDiagnosis.Dispose()
                        oDiagnosis = Nothing
                        Return False

                    End If


                ElseIf rbICD10.Checked Then
                    If oICD9Node.Tag = gloGlobal.gloICD.CodeRevision.ICD9 Then
                        MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        oDiagnosis.Dispose()
                        oDiagnosis = Nothing
                        Return False
                    End If

                End If

                For Each oParent As myTreeNode In oICD9Node.Nodes
                    If oParent.Text.Trim() = "CPT" Then
                        _isCPTFound = False
                        For Each oCPTNode As myTreeNode In oParent.Nodes

                            If oCPTNode.Checked Then
                                _isCPTFound = True
                                '' ADD CPT ''
                                _CPTText = oCPTNode.Text
                                _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                                _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)


                                'If oICD9Node.Checked Then
                                _ICD9Text = CType(oICD9Node, myTreeNode).Text
                                _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-")).ToString.Trim()
                                _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1).ToString.Trim()
                                _ICDRevision = CType(oICD9Node, myTreeNode).Tag

                                timed_pt = Nothing
                                untimed_pt = Nothing

                                If oCPTNode.Nodes.Count > 0 Then
                                    _unit = oCPTNode.Nodes(0).Text.Replace("Units: ", "")

                                    Dim obj As New ClsTreatmentDBLayer
                                    Try
                                        Using dt_pt_billing As DataTable = obj.FetchPTBillingForCPT(m_ExamID, _CPTCode)
                                            If dt_pt_billing IsNot Nothing Then
                                                If dt_pt_billing.Rows.Count > 0 Then
                                                    timed_pt = Convert.ToString(dt_pt_billing.Rows(0)("nTimedTherapy"))
                                                    untimed_pt = Convert.ToString(dt_pt_billing.Rows(0)("nUnTimedTherapy"))
                                                End If
                                            End If
                                        End Using
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.None, ex, gloAuditTrail.ActivityOutCome.Failure)
                                    Finally
                                        If Not IsNothing(obj) Then
                                            obj.Dispose()
                                            obj = Nothing
                                        End If
                                    End Try

                                End If

                                Dim _TempICD As String = oCPTNode.Parent.Parent.Text.Substring(0, oCPTNode.Parent.Parent.Text.IndexOf("-")).ToString().Trim()

                                '  DeleteModifiers(_CPTCode.Trim, arrCPTRevision, GetICDCPTLineNumber(_TempICD, arrCPTRevision, _CPTCode))

                                '' FIND IF CURRENT NODE ALREADY PRESENT IN SAVED DIAGNOSIS OR NOT ''
                                '' IF CPT IS NEW THEN ADD IT IN ARRAYLIST ''
                                If isICD9CPT_Associated(_ICD9Code, _CPTCode) = False Then

                                    _CPTLineNumber = -1
                                    _CPTLineNumber = GetCPTLineNumber(_CPTCode, _TempICD)

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
                                        oList.TemplateResult = _unit
                                        oList.ICD9No = _CPTLineNumber
                                        oList.nICDRevision = _ICDRevision
                                        oList.SnomedID = strSnomedCode
                                        oList.SnomedDesc = strSnomedDesc
                                        oList.SnoMedOneToOneMapping = blnOneSnoMed
                                        oList.TimedTherapy = timed_pt
                                        oList.UnTimedTherapy = untimed_pt

                                        arrCPTDrivenDiagnosis.Add(oList)
                                        oList = Nothing

                                        If oCPTNode.Nodes.Count > 0 Then
                                            _unit = oCPTNode.Nodes(0).Text.Replace("Units: ", "")

                                        End If
                                        If oCPTNode.Nodes.Count > 1 Then

                                            For Each oModNode As myTreeNode In oCPTNode.Nodes(1).Nodes
                                                Dim armod() As String
                                                armod = oModNode.Text.Split("-")
                                                Dim ModLineNumber = GetModLineNumber(_CPTCode, Convert.ToString(armod.GetValue(0)).Trim)

                                                If isICD9CPTMod_Associated(_ICD9Code, _CPTCode, armod.GetValue(0), ModLineNumber) = False Then
                                                    oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                                    ''
                                                    oList.Code = ""

                                                    oList.Description = ""
                                                    oList.HistoryCategory = _CPTCode
                                                    oList.HistoryItem = _CPTDescription
                                                    oList.Value = ""
                                                    oList.ParameterName = ""
                                                    oList.TemplateResult = _unit
                                                    oList.ICD9No = _CPTLineNumber
                                                    oList.nICDRevision = _ICDRevision
                                                    oList.SnomedID = strSnomedCode
                                                    oList.SnomedDesc = strSnomedDesc
                                                    oList.SnoMedOneToOneMapping = blnOneSnoMed
                                                    ''
                                                    oList.Value = oModNode.ModifierCode
                                                    oList.ParameterName = armod.GetValue(1)
                                                    oList.TimedTherapy = timed_pt
                                                    oList.UnTimedTherapy = untimed_pt


                                                    arrCPTDrivenDiagnosis.Add(oList)
                                                    oList = Nothing
                                                End If

                                                armod = Nothing

                                            Next
                                        End If


                                        oList = Nothing 'Change made to solve memory Leak and word crash issue
                                        '' ADD ICD9 ''
                                        If _ICD9Code <> "" Then
                                            oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                            oList.Code = _ICD9Code
                                            oList.nICDRevision = _ICDRevision
                                            oList.Description = _ICD9Description
                                            oList.HistoryCategory = _CPTCode
                                            oList.HistoryItem = _CPTDescription

                                            oList.Value = ""
                                            oList.ParameterName = ""
                                            oList.TemplateResult = _unit
                                            oList.ICD9No = _CPTLineNumber
                                            oList.SnomedID = strSnomedCode
                                            oList.SnomedDesc = strSnomedDesc
                                            oList.SnoMedOneToOneMapping = blnOneSnoMed

                                            oList.Units = _unit
                                            oList.TimedTherapy = timed_pt
                                            oList.UnTimedTherapy = untimed_pt


                                            arrCPTDrivenDiagnosis.Add(oList)

                                            oList = Nothing


                                        End If


                                    Else '' IF CPT LINE FOUND ADD ICD9 TO IT ''
                                        '' ADD ICD9 ''
                                        oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                        oList.Code = _ICD9Code
                                        oList.nICDRevision = _ICDRevision
                                        oList.Description = _ICD9Description

                                        oList.HistoryCategory = _CPTCode
                                        oList.HistoryItem = _CPTDescription
                                        oList.Value = ""
                                        oList.ParameterName = ""
                                        oList.TemplateResult = _unit
                                        oList.ICD9No = _CPTLineNumber
                                        oList.SnomedID = strSnomedCode
                                        oList.SnomedDesc = strSnomedDesc
                                        oList.SnoMedOneToOneMapping = blnOneSnoMed
                                        oList.TimedTherapy = timed_pt
                                        oList.UnTimedTherapy = untimed_pt


                                        oList.Units = _unit
                                        Dim _index As Integer = 0
                                        _index = GetCPTRowIndex(_CPTCode)
                                        If _index <= 0 Then
                                            arrCPTDrivenDiagnosis.Add(oList)
                                        Else
                                            arrCPTDrivenDiagnosis.Insert(_index + 1, oList)
                                        End If

                                        oList = Nothing
                                        If oCPTNode.Nodes.Count > 0 Then
                                            _unit = oCPTNode.Nodes(0).Text.Replace("Units: ", "")

                                        End If
                                        If oCPTNode.Nodes.Count > 1 Then
                                            For Each oModNode As myTreeNode In oCPTNode.Nodes(1).Nodes
                                                Dim armod() As String
                                                armod = oModNode.Text.Split("-")
                                                Dim ModLineNumber = GetModLineNumber(_CPTCode, Convert.ToString(armod.GetValue(0)).Trim)

                                                If isICD9CPTMod_Associated(_ICD9Code, _CPTCode, armod.GetValue(0), ModLineNumber) = False Then
                                                    ''Added condition on 20150914- to add cpt ,icd and modifiers on one line if same cpt selected for multiple ICD's
                                                    If ModLineNumber = -1 Then


                                                        oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                                        ''
                                                        oList.Code = ""

                                                        oList.Description = ""
                                                        oList.HistoryCategory = _CPTCode
                                                        oList.HistoryItem = _CPTDescription
                                                        oList.Value = ""
                                                        oList.ParameterName = ""
                                                        oList.TemplateResult = _unit
                                                        oList.ICD9No = _CPTLineNumber
                                                        oList.nICDRevision = _ICDRevision
                                                        oList.SnomedID = strSnomedCode
                                                        oList.SnomedDesc = strSnomedDesc
                                                        oList.SnoMedOneToOneMapping = blnOneSnoMed
                                                        ''
                                                        oList.Value = oModNode.ModifierCode
                                                        oList.ParameterName = armod.GetValue(1)

                                                        oList.TimedTherapy = timed_pt
                                                        oList.UnTimedTherapy = untimed_pt

                                                        arrCPTDrivenDiagnosis.Add(oList)
                                                        oList = Nothing
                                                    End If
                                                End If

                                                armod = Nothing


                                            Next
                                        End If
                                        'arrCPTDrivenDiagnosis.Add(oList)
                                        oList = Nothing 'Change made to solve memory Leak and word crash issue
                                    End If
                                Else
                                    '''''''''
                                    If oCPTNode.Nodes.Count > 0 Then
                                        _unit = oCPTNode.Nodes(0).Text.Replace("Units: ", "")
                                        '  UpdateUnit(_ICD9Code, _CPTCode, GetICDCPTLineNumber(_TempICD, arrCPTRevision, _CPTCode), _unit)
                                    End If
                                    If oCPTNode.Nodes.Count > 1 Then
                                        For Each oModNode As myTreeNode In oCPTNode.Nodes(1).Nodes
                                            Dim armod() As String
                                            armod = oModNode.Text.Split("-")

                                            Dim ModLineNumber = GetModLineNumber(_CPTCode, Convert.ToString(armod.GetValue(0)).Trim)

                                            If isICD9CPTMod_Associated(_ICD9Code, _CPTCode, armod.GetValue(0), ModLineNumber) = False Then

                                                oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                                ''
                                                oList.Code = ""

                                                oList.Description = ""
                                                oList.HistoryCategory = _CPTCode
                                                oList.HistoryItem = _CPTDescription
                                                oList.Value = ""
                                                oList.ParameterName = ""
                                                oList.TemplateResult = _unit
                                                oList.ICD9No = GetCPTLineNumber(_CPTCode, _ICD9Code)
                                                oList.nICDRevision = _ICDRevision
                                                oList.SnomedID = strSnomedCode
                                                oList.SnomedDesc = strSnomedDesc
                                                oList.SnoMedOneToOneMapping = blnOneSnoMed
                                                ''
                                                oList.Value = oModNode.ModifierCode
                                                oList.ParameterName = armod.GetValue(1)

                                                oList.TimedTherapy = timed_pt
                                                oList.UnTimedTherapy = untimed_pt


                                                arrCPTDrivenDiagnosis.Add(oList)
                                                oList = Nothing
                                            End If

                                            armod = Nothing

                                        Next
                                    End If

                                End If

                            End If

                        Next

                    End If

                Next

                '' THIS IS ONLY IF ICD9 IS CHECKED AND NO CPT CHECKED UNDER IT. ''

                If _isCPTFound = False Then
                    _ICD9Text = CType(oICD9Node, myTreeNode).Text
                    _ICDRevision = CType(oICD9Node, myTreeNode).Tag
                    _ICD9Code = _ICD9Text.Substring(0, _ICD9Text.IndexOf("-")).Trim
                    _ICD9Description = _ICD9Text.Substring(_ICD9Text.IndexOf("-") + 1, _ICD9Text.Length - _ICD9Text.IndexOf("-") - 1).Trim

                    If isICD9CPT_Associated(_ICD9Code, "") = False Then

                        _CPTLineNumber = -1
                        _CPTLineNumber = GetCPTLineNumber("", _ICD9Code)

                        If _CPTLineNumber = -1 Then '' IF CPT NOT PRESENT OR ICD9 OVERFLOW THEN CREATE NEW CPT LINE ''
                            _CPTLineNumber = GetNewCPTLineNumber()
                            '' ADD CPT ''
                            oList = New gloEMRGeneralLibrary.gloGeneral.myList
                            oList.Code = ""
                            oList.Description = ""
                            oList.HistoryCategory = ""
                            oList.HistoryItem = ""
                            oList.Value = ""
                            oList.ParameterName = ""
                            oList.TemplateResult = _unit
                            oList.ICD9No = _CPTLineNumber
                            oList.nICDRevision = _ICDRevision
                            arrCPTDrivenDiagnosis.Add(oList)
                            oList = Nothing 'Change made to solve memory Leak and word crash issue
                            '' ADD ICD9 ''
                            oList = New gloEMRGeneralLibrary.gloGeneral.myList
                            oList.Code = _ICD9Code
                            oList.nICDRevision = _ICDRevision
                            oList.Description = _ICD9Description
                            oList.SnomedID = strSnomedCode
                            oList.SnomedDesc = strSnomedDesc
                            oList.SnoMedOneToOneMapping = blnOneSnoMed
                            oList.HistoryCategory = ""
                            oList.HistoryItem = ""
                            oList.Value = ""
                            oList.ParameterName = ""
                            oList.TemplateResult = _unit
                            oList.ICD9No = _CPTLineNumber

                            arrCPTDrivenDiagnosis.Add(oList)
                            oList = Nothing 'Change made to solve memory Leak and word crash issue
                        Else '' IF CPT LINE FOUND ADD ICD9 TO IT ''
                            '' ADD ICD9 ''
                            oList = New gloEMRGeneralLibrary.gloGeneral.myList
                            oList.Code = _ICD9Code
                            oList.nICDRevision = _ICDRevision
                            oList.Description = _ICD9Description
                            oList.SnomedID = strSnomedCode
                            oList.SnomedDesc = strSnomedDesc
                            oList.SnoMedOneToOneMapping = blnOneSnoMed

                            oList.HistoryCategory = ""
                            oList.HistoryItem = ""
                            oList.Value = ""
                            oList.ParameterName = ""
                            oList.TemplateResult = _unit
                            oList.ICD9No = _CPTLineNumber


                            arrCPTDrivenDiagnosis.Add(oList)
                            oList = Nothing 'Change made to solve memory Leak and word crash issue
                        End If

                    End If
                End If



                
            Next
          
            If arrCPTDrivenDiagnosis IsNot Nothing Then
                If arrCPTDrivenDiagnosis.Count > 0 Then

                    'Check Active CPT
                    For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                        If Convert.ToString(DirectCast(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).Code) = "" And Convert.ToString(DirectCast(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory) <> "" Then
                            Dim drRow As DataRow = dtActiveCPTTable.NewRow()
                            drRow("sCPTCode") = Convert.ToString(DirectCast(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory)
                            drRow("dtFromDate") = gloDateMaster.gloDate.DateAsNumber(GetVisitdate(m_VisitID))
                            drRow("dtToDate") = 0
                            dtActiveCPTTable.Rows.Add(drRow)
                        End If
                    Next
                    Dim CPTAlert As String = gloGlobal.gloPMGlobal.getCPTDeativatedCPT(dtActiveCPTTable)
                    dtActiveCPTTable.Clear()
                    If (CPTAlert <> "") Then
                        Dim dResult As DialogResult = MessageBox.Show(CPTAlert, "Diagnosis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                        If dResult.ToString() = "Cancel" Then
                            oDiagnosis.Dispose()
                            oDiagnosis = Nothing
                            Return False
                        End If
                    End If
                    '

                    oDiagnosis.SaveDiagTreatmentAssociation(m_ExamID, _PatientID, m_VisitID, arrCPTDrivenDiagnosis, Me, True)
                    oDiagnosis.Dispose()
                    oDiagnosis = Nothing

                End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Sub AddOnlyCommonCPTinCPTDriven()
        Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
        Dim _CPTLineNumber As Integer = -1
        Dim _CPTText, _CPTCode, _CPTDescription, strSnomedCode, strSnomedDesc, _unit, _ICDRevision As String
        Dim blnOneSnoMed As Boolean
        Dim _isCPTFound As Boolean = False
        '', _SnomedCode, _snomedDesc, _snomedText As String
        strSnomedCode = ""
        strSnomedDesc = ""
        _unit = "1"
        If gblnIcd10Transition Then
            _ICDRevision = 10
        Else
            _ICDRevision = 9
        End If

        For Each _node As myTreeNode In trICD9Association.Nodes(0).Nodes
            If _node.Checked Then
                _isCPTFound = True
                '' ADD CPT ''
                _CPTText = _node.Text
                _CPTCode = _CPTText.Substring(0, _CPTText.IndexOf("-"))
                _CPTDescription = _CPTText.Substring(_CPTText.IndexOf("-") + 1, _CPTText.Length - _CPTText.IndexOf("-") - 1)
                If _node.Nodes.Count > 0 Then
                    _unit = _node.Nodes(0).Text.Replace("Units: ", "")

                    _CPTLineNumber = -1
                    _CPTLineNumber = GetCPTLineNumber(_CPTCode, "")

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
                        oList.TemplateResult = _unit
                        oList.ICD9No = _CPTLineNumber
                        oList.nICDRevision = _ICDRevision
                        oList.SnomedID = strSnomedCode
                        oList.SnomedDesc = strSnomedDesc
                        oList.SnoMedOneToOneMapping = blnOneSnoMed

                        arrCPTDrivenDiagnosis.Add(oList)
                        oList = Nothing
                        'oList .ModCode =oCPTNode 
                        If _node.Nodes.Count > 0 Then
                            _unit = _node.Nodes(0).Text.Replace("Units: ", "")
                            ' UpdateUnit(_ICD9Code, _CPTCode, _CPTLineNumber, _unit)
                        End If
                        If _node.Nodes.Count > 1 Then

                            For Each oModNode As myTreeNode In _node.Nodes(1).Nodes
                                Dim armod() As String
                                armod = oModNode.Text.Split("-")
                                Dim ModLineNumber = GetModLineNumber(_CPTCode, Convert.ToString(armod.GetValue(0)).Trim)
                                ' DeleteModifiers(_CPTCode.Trim, arrCPTRevision, _CPTLineNumber)

                                oList = New gloEMRGeneralLibrary.gloGeneral.myList
                                ''
                                oList.Code = ""

                                oList.Description = ""
                                oList.HistoryCategory = _CPTCode
                                oList.HistoryItem = _CPTDescription
                                oList.Value = ""
                                oList.ParameterName = ""
                                oList.TemplateResult = _unit
                                oList.ICD9No = _CPTLineNumber
                                oList.nICDRevision = _ICDRevision
                                oList.SnomedID = strSnomedCode
                                oList.SnomedDesc = strSnomedDesc
                                oList.SnoMedOneToOneMapping = blnOneSnoMed
                                ''
                                oList.Value = oModNode.ModifierCode
                                oList.ParameterName = armod.GetValue(1)

                                arrCPTDrivenDiagnosis.Add(oList)
                                oList = Nothing


                                armod = Nothing

                            Next
                        End If
                        ''End Added by Mayuri: 20150108

                        oList = Nothing 'Change made to solve memory Leak and word crash issue

                    End If
                End If
            End If
        Next
    End Sub

    Private Function GetCPTLineNumber(ByVal sCPTCode As String, ByVal ICDCode As String) As Integer
        Try
            Dim _DxCount As Integer = -1
            Dim _CPTLineNumber As Integer = -1
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = arrCPTDrivenDiagnosis.Count - 1 To 0 Step -1
                oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                'If ICDCode <> "" Then
                '    If oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Value.Trim = "" And oList.Code.Trim = ICDCode.Trim Then
                '        _DxCount = _DxCount + 1
                '        _CPTLineNumber = oList.ICD9No
                '    End If
                'Else
                If oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Value.Trim = "" Then
                    _DxCount = _DxCount + 1
                    _CPTLineNumber = oList.ICD9No
                End If
                ' End If



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

    Private Function GetCPTRowIndex(ByVal sCPTCode As String) As Integer
        Dim _CPTIndex As Integer = 0
        Try

            Dim _CPTLineNumber As Integer = -1
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = arrCPTDrivenDiagnosis.Count - 1 To 0 Step -1
                oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)

                If oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Code.Trim <> "" Then
                    _CPTIndex = i
                    Return _CPTIndex
                End If


            Next

        Catch

        End Try
        Return _CPTIndex
    End Function
    Private Function GetICDCPTLineNumber(ByVal ICDCode As String, ByVal arrcpt As ArrayList, ByVal CPTCode As String) As Integer
        Try
            Dim _DxCount As Integer = -1
            Dim _CPTLineNumber As Integer = -1
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = arrcpt.Count - 1 To 0 Step -1
                oList = CType(arrcpt(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList.Code.Trim = ICDCode.Trim And oList.HistoryCategory.Trim = CPTCode.Trim Then
                    _DxCount = oList.ICD9No
                    Return _DxCount
                End If
            Next
            Return _DxCount
            '' IF ICD9 WILL OVERFLOW THEN DON'T SEND LINE NUMBER AND LET NEW CPT LINE CREATE ''
            'If _DxCount >= 8 Then
            '    Return -1
            'Else
            '    Return _CPTLineNumber
            'End If
        Catch
            Return -1
        End Try
    End Function
    Private Function GetModLineNumber(ByVal sCPTCode As String, ByVal ModCode As String) As Integer
        Try
            Dim _DxCount As Integer = -1
            Dim _CPTLineNumber As Integer = -1
            Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
            For i As Integer = arrCPTDrivenDiagnosis.Count - 1 To 0 Step -1
                oList = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList)
                If oList.HistoryCategory.Trim = sCPTCode.Trim And oList.Value.Trim = ModCode Then
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
            For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No > _NewNumber Then
                    _NewNumber = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No
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

    Private Sub RemoveCPTICD9(ByVal sCPTCode As String, ByVal sCPTDescription As String, ByVal sICD9Code As String, ByVal sICD9Description As String, ByVal bCPTClicked As Boolean)
        Try
            If bCPTClicked Then

                If sICD9Code = "" Then
                    '' REMOVE ONLY CPT LINE IF NO ICD9 ASSOCIATED TO IT ''
                    Dim _CPTIndex As Integer = -1
                    For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                        If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory = sCPTCode Then
                            If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).Code <> "" Then
                                '' IF ANY ICD9 ASSOCIATED TO IT DON'T REMOVE ''
                                Exit Sub
                            Else
                                _CPTIndex = i
                            End If
                        End If
                    Next

                    If _CPTIndex > -1 Then
                        arrCPTDrivenDiagnosis.RemoveAt(_CPTIndex)
                    End If
                Else
                    Dim _ICD9Count As Integer = -1
                    Dim _CPTLineNumber As Integer = -1
                    For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                        If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory = sCPTCode Then
                            _ICD9Count = _ICD9Count + 1
                            If _CPTLineNumber = -1 Then
                                _CPTLineNumber = CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No
                            End If
                        End If
                    Next

                    '' IF NO OTHER ICD9 ASSOCIATED TO THIS CPT '' THEN  REMOVE WHOLE LINE ''
                    If _ICD9Count = 1 Then
                        For i As Integer = arrCPTDrivenDiagnosis.Count - 1 To 0 Step -1
                            If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No = _CPTLineNumber Then
                                ' arrCPTDrivenDiagnosis.RemoveAt(i)
                            End If
                        Next
                    Else
                        '' IF ANY OTHER ICD9 ASSOCIATED WITH THIS CPT '' THEN REMOVE ICD9 FROM CPT LINE ''
                        For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                            If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).ICD9No = _CPTLineNumber And _
                            CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).Code = sICD9Code Then
                                arrCPTDrivenDiagnosis.RemoveAt(i)
                                Exit Sub
                            End If

                        Next
                    End If
                End If

            Else '' Diagnosis Clicked ''

                For i As Integer = 0 To arrCPTDrivenDiagnosis.Count - 1
                    If CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).HistoryCategory = sCPTCode And _
                    CType(arrCPTDrivenDiagnosis(i), gloEMRGeneralLibrary.gloGeneral.myList).Code = sICD9Code Then
                        arrCPTDrivenDiagnosis.RemoveAt(i)
                        Exit Sub
                    End If
                Next

            End If

        Catch
        End Try
    End Sub

#End Region



    '''Sandip Darade 20090919

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
        If tn.Text = "Modifiers" Then
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

    Private Sub mnuOpenICD9Assoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOpenICD9Assoc.Click
        Dim frm As New frmICD9Association
        '  Dim ICD9Node As System.Windows.Forms.TreeNode
        Dim oSmartDx As smartDx = Nothing
        Try

            oSmartDx = New smartDx()
            With frm
                frmICD9Association.ISICD9AssocOpen = True
                frmICD9Association.ICD9SelNodeKey = CType(trICD9Association.SelectedNode, myTreeNode).SmartDxKey
                Dim _arrICD9() As String = trICD9Association.SelectedNode.Text.ToString.Split("-")
                frmICD9Association.ICD9Code = _arrICD9(0)
                frmICD9Association.ICDRevision = trICD9Association.SelectedNode.Tag
                frmICD9Association.ISCopyICDSmarDx = False
                frmICD9Association.ICDSmarDxName = oSmartDx.getAssociatedSmartDxName(CType(trICD9Association.SelectedNode, myTreeNode).SmartDxKey)
                .isFromexam = True
                .WindowState = FormWindowState.Normal
                .Owner = Me
                '  .MdiParent = Me
                Dim pt As Point = Me.Location
                pt.X = pt.X + (Me.Width / 2)
                pt.Y = pt.Y - 200
                .Location = pt
                ' .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                If CType(trICD9Association.SelectedNode, myTreeNode).SmartDxKey <= 0 Then
                    frmICD9Association.ICDSmarDxName = _arrICD9(1)
                    .fillDianosisFromExam(_arrICD9(0), _arrICD9(1), CType(trICD9Association.SelectedNode, myTreeNode).Key, trICD9Association.SelectedNode.Tag)

                End If

                .ShowDialog(frm.Parent)
                ' '' ''.ISICD9AssocOpen = False
                ' '' ''.ICD9SelNodeKey = 0
                If CType(trICD9Association.SelectedNode, myTreeNode).SmartDxKey <= 0 Then
                    CType(trICD9Association.SelectedNode, myTreeNode).SmartDxKey = GetSelectedSmartDxID(CType(trICD9Association.SelectedNode, myTreeNode).Key)
                End If
                Dim arrSelectedNode As New ArrayList
                For Each myReqNode As TreeNode In trICD9Association.Nodes.Item(1).Nodes
                    If myReqNode.Nodes.Count > 0 Then
                        myReqNode.Nodes.Clear()
                    End If
                    arrSelectedNode.Add(CType(myReqNode, myTreeNode))
                Next

                If arrSelectedNode.Count > 0 Then
                    trICD9Association.Nodes.Item(1).Nodes.Clear()
                    For i As Integer = 0 To arrSelectedNode.Count - 1
                        AddNode(CType(arrSelectedNode.Item(i), myTreeNode))
                    Next
                End If

                'Change made to solve memory Leak and word crash issue
                .Close()
                .Dispose()
            End With
            frm = Nothing
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If frm IsNot Nothing Then
                frm.Dispose()
                frm = Nothing
            End If
            If oSmartDx IsNot Nothing Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If
        End Try
    End Sub

    Private Sub gloLabSettings(ByVal _TaskType As String, Optional ByVal _TestNames As String = "", Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _strdiag As String = "")

        Select Case _TaskType.ToString().ToUpper()
            Case "TASK"
                Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
                Dim objpatient As New gloPatient.Patient()
                Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
                'Developer: Sanjog(Dhamke)
                'Date:10 Dec 2011
                'Bug ID/PRD Name/Salesforce Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                'Reason: To show task info
                Dim strLabTest As String = ""
                Dim strLabTests As String = ""
                objpatient = objgloPatient.GetPatient(_PatientID)
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
                Dim _LoginProviderId As Int64 = 0

                _LoginProviderId = GetProviderIDForUser(gnLoginID)
                objClsGeneral.TestList = _TestNames
                objClsGeneral.TestlistOnly = strLabTests
                Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(_PatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId, m_ExamID, gloTaskMail.TaskType.PlaceLabOrder)
                If nTaskID > 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", _PatientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

                _LoginProviderId = 0
                'Change made to solve memory Leak and word crash issue
                objClsGeneral.Dispose()
                objClsGeneral = Nothing
                objpatient.Dispose()
                objpatient = Nothing
                objgloPatient.Dispose()
                objgloPatient = Nothing
            Case "LABORDER"

                gloLabOrderScreen(_arrLabs, _strdiag)  '' added to show testnames on EmdeonScreen ,v8022

            Case "RECORDRESULTS"
                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(_PatientID)
                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.ArrLabs = _arrLabs '' Added by Abhijeet on 20100624
                frmNormalLab.WindowState = FormWindowState.Maximized
                frmNormalLab.ShowInTaskbar = False
                frmNormalLab.BringToFront()
                frmNormalLab.ShowDialog(IIf(IsNothing(frmNormalLab.Parent), Me, frmNormalLab.Parent))
                'Change made to solve memory Leak and word crash issue
                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.Close()
                frmNormalLab.Dispose()
                frmNormalLab = Nothing
            Case Else
                MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Select
        End Select
    End Sub

    Private Sub gloLabOrderScreen(Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _strdiag As String = "")  '' _arrLabs added to show testnames on EmdeonScreen ,v8022

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim objclsgeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim _LoginUserProviderID As Long = 0
        Dim _PatientProviderID As Long = 0

        Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
        Dim objpatient As New gloPatient.Patient()
        Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)

        objpatient = objgloPatient.GetPatient(_PatientID)
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
                    'Change made to solve memory Leak and word crash issue
                    frmLabDemo.Close()
                    frmLabDemo.Dispose()
                    frmLabDemo = Nothing
                Else

                    Dim strQry As String = String.Empty
                    Dim boolPatientReg As [Boolean] = False
                    If ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()) Then
                        strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND   Patient.sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                    End If
                    oDB.Connect(False)

                    For loopcnt As Int16 = 1 To 3

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
                        objfrmEmdonInterface.strDiag = _strdiag
                        objfrmEmdonInterface.LoginProviderID = gnLoginProviderID
                        objfrmEmdonInterface.WindowState = FormWindowState.Maximized
                        objfrmEmdonInterface.ShowDialog(IIf(IsNothing(objfrmEmdonInterface.Parent), Me, objfrmEmdonInterface.Parent))
                        'Change made to solve memory Leak and word crash issue
                        objfrmEmdonInterface.Close()
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
                'Change made to solve memory Leak and word crash issue
                objFrmConnectionConfirm.Close()
                objFrmConnectionConfirm.Dispose()
                objFrmConnectionConfirm = Nothing

            ElseIf LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.ServerNotresponding Then
                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(False)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                'Change made to solve memory Leak and word crash issue
                objFrmConnectionConfirm.Close()
                objFrmConnectionConfirm.Dispose()
                objFrmConnectionConfirm = Nothing
            End If
        End If
        'Change made to solve memory Leak and word crash issue
        If Not oDB Is Nothing Then
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End If
        If Not objclsgeneral Is Nothing Then
            objclsgeneral.Dispose()
            objclsgeneral = Nothing
        End If
        If Not objClsgloLabPatientLayer Is Nothing Then
            objClsgloLabPatientLayer.Dispose()
            objClsgloLabPatientLayer = Nothing
        End If
        If Not objpatient Is Nothing Then
            objpatient.Dispose()
            objpatient = Nothing
        End If
        If Not objgloPatient Is Nothing Then
            objgloPatient.Dispose()
            objgloPatient = Nothing
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
            oDB = Nothing   'Change made to solve memory Leak and word crash issue
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

            '    'Dim drMesgResult As DialogResult = MessageBox.Show(("The user you are using is not set up as a provider. If you proceed, the lab order " & vbCr & vbLf & "provider will be defaulted to the current patient’s provider '") + strProviderName & "'." & vbCr & vbLf & vbCr & vbLf & "Would you like to proceed with creating a new order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            '    ' If drMesgResult = DialogResult.Yes Then
            '    'strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
            '    'If ConfirmNull(strLabID.ToString()) Then
            '    Return True
            '    'Else
            '    '    If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '    '        Return True
            '    '    Else
            '    '        Return False
            '    '    End If
            '    'End If
            '    'Else
            '    '    Return False
            '    'End If
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

            'Finally
            ''Change made to solve memory Leak and word crash issue
            'If Not objClsGeneral Is Nothing Then
            '    objClsGeneral.Dispose()
            '    objClsGeneral = Nothing
            'End If
        End Try

    End Function
    ''Added icd9 icd10 implementation
    Private Sub RbICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbICD9.CheckedChanged

        If RbICD9.Checked = True Then
            RbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            FillICD(9)
        Else
            RbICD9.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub
    Private Sub FillICD(ByVal ICDType As Integer)
        Dim dt As DataTable = Nothing
        If ICDType = 9 Then
            If btnPatDiagnosis.Tag = "Selected" Then
                dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD9, GloUC_trvICD.txtsearch.Text, 1, _PatientID, True)
            Else
                dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD9, GloUC_trvICD.txtsearch.Text, 1, _PatientID)
            End If

        ElseIf ICDType = 10 Then
            If btnPatDiagnosis.Tag = "Selected" Then
                dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD10, GloUC_trvICD.txtsearch.Text, 1, _PatientID, True)
            Else
                dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD10, GloUC_trvICD.txtsearch.Text, 1, _PatientID)
            End If

        End If

        If Not IsNothing(dt) Then

            GloUC_trvICD.DataSource = dt
            GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvICD.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
            GloUC_trvICD.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
            GloUC_trvICD.DescriptionMember = Convert.ToString(dt.Columns(2).ColumnName)
            GloUC_trvICD.ICDRevision = Convert.ToString(dt.Columns(3).ColumnName)
            GloUC_trvICD.IsDiagnosisSearch = True
            GloUC_trvICD.FillTreeView()
            GloUC_trvICD.FocusSearchBox()
        End If

    End Sub



    Private Sub rbICD10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD10.CheckedChanged

        If rbICD10.Checked = True Then
            rbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            FillICD(10)
        Else
            rbICD10.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnAllDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllDiagnosis.Click
        pnl_btnAllDiagnosis.Dock = DockStyle.Top
        btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnAllDiagnosis.Tag = "Selected"

        pnl_btnPatDiagnosis.Dock = DockStyle.Bottom
        btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnPatDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnPatDiagnosis.Tag = "UnSelected"

        Dim dt As DataTable = Nothing
        If rbICD10.Checked = True Then
            rbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbICD10.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
        If RbICD9.Checked Then
            dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD9, GloUC_trvICD.txtsearch.Text, 1, _PatientID)
        ElseIf rbICD10.Checked Then
            dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD10, GloUC_trvICD.txtsearch.Text, 1, _PatientID)
        End If

        If Not IsNothing(dt) Then

            GloUC_trvICD.DataSource = dt
            GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvICD.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
            GloUC_trvICD.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
            GloUC_trvICD.DescriptionMember = Convert.ToString(dt.Columns(2).ColumnName)
            GloUC_trvICD.ICDRevision = Convert.ToString(dt.Columns(3).ColumnName)
            GloUC_trvICD.IsDiagnosisSearch = True
            GloUC_trvICD.FillTreeView()
            GloUC_trvICD.FocusSearchBox()
        End If

    End Sub

    Private Sub btnAllDiagnosis_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllDiagnosis.MouseEnter
        btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnAllDiagnosis_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllDiagnosis.MouseLeave
        If pnl_btnAllDiagnosis.Dock = DockStyle.Top Then
            btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        End If


    End Sub

    Private Sub btnPatDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatDiagnosis.Click
        pnl_btnPatDiagnosis.Dock = DockStyle.Top
        btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnPatDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnPatDiagnosis.Tag = "Selected"

        pnl_btnAllDiagnosis.Dock = DockStyle.Bottom
        btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnAllDiagnosis.Tag = "UnSelected"

        Dim dt As DataTable = Nothing
        If rbICD10.Checked = True Then
            rbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbICD10.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If

        If RbICD9.Checked Then
            dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD9, GloUC_trvICD.txtsearch.Text, 1, _PatientID, True)
        ElseIf rbICD10.Checked Then
            dt = objclsSmartDiagnosis.FillICD9(gloGlobal.gloICD.CodeRevision.ICD10, GloUC_trvICD.txtsearch.Text, 1, _PatientID, True)
        End If

        If Not IsNothing(dt) Then

            GloUC_trvICD.DataSource = dt
            GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvICD.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
            GloUC_trvICD.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
            GloUC_trvICD.DescriptionMember = Convert.ToString(dt.Columns(2).ColumnName)
            GloUC_trvICD.ICDRevision = Convert.ToString(dt.Columns(3).ColumnName)
            GloUC_trvICD.IsDiagnosisSearch = True
            GloUC_trvICD.FillTreeView()
            GloUC_trvICD.FocusSearchBox()
        End If
    End Sub

    Private Sub trICD9Association_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles trICD9Association.AfterLabelEdit


        If Not IsNothing(e.Label) Then
            If e.Label.Length > 0 Then


                Dim regxp = New Regex("^[0-9]+(\.[0-9]+)?$")
                If regxp.IsMatch(e.Label) = False Then

                    e.CancelEdit = True
                    If e.Node.Text.Contains("Units") Then
                        e.Node.Text = e.Node.Text
                    Else
                        e.Node.Text = "Units: " & e.Node.Text

                    End If
                Else
                    Me.BeginInvoke(New MethodInvoker(Sub() afterafterdeleate(e.Node)))

                End If
            Else
                Me.BeginInvoke(New MethodInvoker(Sub() afterafterdeleate(e.Node)))


            End If
        Else

            Me.BeginInvoke(New MethodInvoker(Sub() afterafterdeleate(e.Node)))

        End If
    End Sub
    Private Sub afterafterdeleate(ByVal tnode As TreeNode)

        Try
            If tnode.Text <> "" Then
                If tnode.Text.Contains("Units:") Then
                    tnode.Text = tnode.Text
                Else
                    Dim _unit As String = FormatNumber(Convert.ToDecimal(tnode.Text))
                    If _unit = "0" Then
                        tnode.Text = "Units: " & "1"
                    ElseIf Convert.ToDouble(tnode.Text) > 999.9999 Then
                        tnode.Text = "Units: " & "1"
                    Else

                        tnode.Text = "Units: " & tnode.Text
                    End If
                End If
                
            ElseIf tnode.Text = "" Then
                tnode.Text = "Units: " & "1"
            ElseIf Convert.ToDouble(tnode.Text) > 999.9999 Then
                tnode.Text = "Units: " & "1"
            ElseIf tnode.Text.Contains("Units:") Then
                tnode.Text = tnode.Text
            Else
                tnode.Text = "Units: " & tnode.Text
            End If


        Catch ex As Exception
            tnode.Text = "Units: " & "1"
        End Try


        'End If
        If Not IsNothing(tnode.Parent) Then
            If Not IsNothing(tnode.Parent.Parent) Then
                If tnode.Parent.Parent.Text = "Common" Then


                    For i As Integer = 0 To C1Dignosis.Rows.Count - 1
                        Dim oNode As C1.Win.C1FlexGrid.Node
                        oNode = C1Dignosis.Rows(i).Node
                        If Not IsNothing(oNode) Then
                            If Not IsNothing(oNode.Parent) Then

                                If tnode.Parent.Text = C1Dignosis.Rows(i)(COl_CPTCode) & "-" & C1Dignosis.Rows(i)(Col_CPTDesc) Then
                                    C1Dignosis.SetData(i, Col_Units, tnode.Text.Replace("Units: ", "").Trim())
                                    If gblnSetCPTtoAllICD9 = False Then
                                        Exit For
                                    End If

                                End If

                            End If
                        End If

                    Next

                Else
                    If Not IsNothing(tnode.Parent.Parent.Parent) Then


                        For i As Integer = 0 To C1Dignosis.Rows.Count - 1
                            Dim oNode As C1.Win.C1FlexGrid.Node
                            oNode = C1Dignosis.Rows(i).Node
                            If Not IsNothing(oNode) Then
                                If Not IsNothing(oNode.Parent) Then
                                    If Convert.ToString(oNode.Parent.Data).Trim = tnode.Parent.Parent.Parent.Text.Trim Then
                                        If tnode.Parent.Text = C1Dignosis.Rows(i)(COl_CPTCode) & "-" & C1Dignosis.Rows(i)(Col_CPTDesc) Then
                                            C1Dignosis.SetData(i, Col_Units, tnode.Text.Replace("Units: ", "").Trim())
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If

                        Next
                    End If
                End If

            End If
        End If
    End Sub


    Private Sub trICD9Association_DrawNode(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs) Handles trICD9Association.DrawNode
        Dim _selectednode As myTreeNode
        If Not IsNothing(e.Node) Then


            _selectednode = e.Node
            If Convert.ToString(_selectednode.UnitsKey) = "Units" Then
                Dim backColor As Color, foreColor As Color
                If (e.State And TreeNodeStates.Selected) = TreeNodeStates.Selected Then
                    backColor = SystemColors.Highlight
                    foreColor = SystemColors.HighlightText
                    'ElseIf (e.State And TreeNodeStates.Hot) = TreeNodeStates.Hot Then
                    '    backColor = SystemColors.HotTrack
                    '    foreColor = SystemColors.HighlightText
                Else
                    backColor = e.Node.BackColor
                    foreColor = e.Node.ForeColor
                End If
                Using brush As New SolidBrush(backColor)
                    e.Graphics.FillRectangle(brush, e.Node.Bounds)
                End Using
                TextRenderer.DrawText(e.Graphics, e.Node.Text, Me.trICD9Association.Font, e.Node.Bounds, foreColor, backColor)

                If (e.State And TreeNodeStates.Focused) = TreeNodeStates.Focused Then
                    ControlPaint.DrawFocusRectangle(e.Graphics, e.Node.Bounds, foreColor, backColor)
                End If
                e.DrawDefault = False

            ElseIf Convert.ToString(_selectednode.ModifierKey) = "M1" Then


                Dim backColor As Color, foreColor As Color
                If (e.State And TreeNodeStates.Selected) = TreeNodeStates.Selected Then
                    backColor = SystemColors.Highlight
                    foreColor = SystemColors.HighlightText
                    'ElseIf (e.State And TreeNodeStates.Hot) = TreeNodeStates.Hot Then
                    '    backColor = SystemColors.HotTrack
                    '    foreColor = SystemColors.HighlightText
                Else
                    backColor = e.Node.BackColor
                    foreColor = e.Node.ForeColor
                End If
                Using brush As New SolidBrush(backColor)
                    e.Graphics.FillRectangle(brush, e.Node.Bounds)
                End Using
                TextRenderer.DrawText(e.Graphics, e.Node.Text, Me.trICD9Association.Font, e.Node.Bounds, foreColor, backColor)

                If (e.State And TreeNodeStates.Focused) = TreeNodeStates.Focused Then
                    ControlPaint.DrawFocusRectangle(e.Graphics, e.Node.Bounds, foreColor, backColor)
                End If
                e.DrawDefault = False

            ElseIf Convert.ToString(_selectednode.ModifierKey) = "999" Then

                'For Each _node As myTreeNode In _selectednode.Nodes
                '    If _node.Key = 999 Then
                Dim backColor As Color, foreColor As Color
                If (e.State And TreeNodeStates.Selected) = TreeNodeStates.Selected Then
                    backColor = SystemColors.Highlight
                    foreColor = SystemColors.HighlightText
                    'ElseIf (e.State And TreeNodeStates.Hot) = TreeNodeStates.Hot Then
                    '    backColor = SystemColors.HotTrack
                    '    foreColor = SystemColors.HighlightText
                Else
                    backColor = e.Node.BackColor
                    foreColor = e.Node.ForeColor
                End If
                Using brush As New SolidBrush(backColor)
                    e.Graphics.FillRectangle(brush, e.Node.Bounds)
                End Using
                TextRenderer.DrawText(e.Graphics, e.Node.Text, Me.trICD9Association.Font, e.Node.Bounds, foreColor, backColor)

                If (e.State And TreeNodeStates.Focused) = TreeNodeStates.Focused Then
                    ControlPaint.DrawFocusRectangle(e.Graphics, e.Node.Bounds, foreColor, backColor)
                End If
                e.DrawDefault = False



            Else
                e.DrawDefault = True
            End If


        End If

    End Sub

    Private Sub trICD9Association_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trICD9Association.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then


            trICD9Association.LabelEdit = False
            If Not IsNothing(e.Node) Then
                Dim _selectedNode As myTreeNode = e.Node
                If Convert.ToString(_selectedNode.ModifierKey) = "M1" Then
                    ' LoadUserGrid(e.X)
                    trICD9Association.SelectedNode = _selectedNode
                    LoadModfifierControl(e.X)
                ElseIf Convert.ToString(_selectedNode.UnitsKey) = "Units" Then
                    Dim strUnits() As String
                    Dim strUnit As String = ""
                    trICD9Association.SelectedNode = _selectedNode
                    If e.Node.Text.Contains("Units") Then
                        strUnits = e.Node.Text.Split(" :")
                        If strUnits.Length >= 1 Then
                            strUnit = strUnits(1)
                        End If
                    Else
                        strUnit = e.Node.Text
                    End If
                    trICD9Association.LabelEdit = True
                    e.Node.Text = strUnit
                    e.Node.BeginEdit()
                Else
                    trICD9Association.LabelEdit = False
                End If
            End If
        End If
    End Sub
    Private Sub LoadModfifierControl(ByVal X As Integer)
        Try


            pnlModifiers.Visible = True
            pnlModifiers.BringToFront()
            pnlSmartDX.Visible = False

            If IsNothing(oModifierControl) = False Then
                RemoveHandler oModifierControl.ItemSelectedClick, AddressOf oModifierControl_ItemSelectedClick
                RemoveHandler oModifierControl.ItemClosedClick, AddressOf oModifierControl_ItemClosedClick
                oModifierControl.Dispose()
                oModifierControl = Nothing
            End If


            oModifierControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Modifier, True, Me.Width)
            oModifierControl.ControlHeader = "Modifier"
            'gblnIcd10Transition ''added to Select  ICD10 if true 
            AddHandler oModifierControl.ItemSelectedClick, AddressOf oModifierControl_ItemSelectedClick
            AddHandler oModifierControl.ItemClosedClick, AddressOf oModifierControl_ItemClosedClick
            pnlModifiers.Controls.Add(oModifierControl)
            oModifierControl.Dock = DockStyle.Fill
            oModifierControl.BringToFront()
            For Each _node As myTreeNode In trICD9Association.SelectedNode.Nodes
                oModifierControl.SelectedItems.Add(_node.Key, _node.ModifierCode, "")

            Next
            '
            oModifierControl.ShowHeaderPanel(True)


            oModifierControl.OpenControl()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oModifierControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'SLR: 2/18/2015 What is the use of filling toList here?

            '   Dim ToList As gloGeneralItem.gloItems


            Dim _node As myTreeNode
            Dim _selectNode As myTreeNode = trICD9Association.SelectedNode
            If Not IsNothing(_selectNode) Then
                _selectNode.Nodes.Clear()
            End If
            '  ToList = New gloGeneralItem.gloItems()
            '  Dim ToItem As gloGeneralItem.gloItem
            If oModifierControl.SelectedItems.Count > 0 Then
                If Not IsNothing(_selectNode) Then

                    For i As Int16 = 0 To oModifierControl.SelectedItems.Count - 1

                        ''''

                        _node = New myTreeNode
                        _node.Text = oModifierControl.SelectedItems(i).Code & " - " & oModifierControl.SelectedItems(i).Description
                        ' _node.Tag = oModifierControl.SelectedItems(i).ID
                        _node.ModifierCode = oModifierControl.SelectedItems(i).Code
                        _node.ModifierKey = "999"
                        _node.Key = oModifierControl.SelectedItems(i).ID
                        _node.EnsureVisible()
                        If _selectNode.Nodes.Count > 0 Then
                            If _selectNode.Nodes.Contains(_node) Then
                                _node.Dispose()
                                _node = Nothing
                            Else
                                _selectNode.Nodes.Add(_node)
                                _node = Nothing
                            End If

                        Else
                            _selectNode.Nodes.Add(_node)
                            _node = Nothing
                        End If
                        ''''

                        '   ToItem = New gloGeneralItem.gloItem()
                        '  ToItem.ID = oModifierControl.SelectedItems(i).ID
                        '   ToItem.Description = oModifierControl.SelectedItems(i).Code
                        '  ToList.Add(ToItem)
                        '  ToItem = Nothing
                    Next
                    _selectNode.ExpandAll()
                End If

                pnlModifiers.Visible = False
                pnlModifiers.SendToBack()
                pnlSmartDX.Visible = True
                ' ofrmModifierControl.Close()
                ' End If

            Else


                pnlModifiers.Visible = False
                pnlModifiers.SendToBack()
                pnlSmartDX.Visible = True

            End If
            If Not IsNothing(_selectNode.Parent) Then
                If Not IsNothing(_selectNode.Parent.Parent) Then

                    If Not IsNothing(_selectNode.Parent.Parent.Parent) Then


                        FillModifierinGrid(_selectNode, oModifierControl.SelectedItems)
                    End If



                End If
            End If
            '  trICD9Association.SelectedNode = _NodeSelected
        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub FillModifierinGrid(ByVal _SelectNode As myTreeNode, ByVal _selectedItems As gloGeneralItem.gloItems)

        Dim _ICDCode As String = _SelectNode.Parent.Parent.Parent.Text.Trim
        Dim _CPTCode As String = _SelectNode.Parent.Text.Trim
        RemoveSelectedModifier(_ICDCode, _CPTCode)
        Dim arrstrConctICD9() As String
        Dim arrstrConctCPT() As String
        Dim nMaxICD9Count As Integer = 0
        Dim oICDNode As C1.Win.C1FlexGrid.Node
        For i As Integer = 0 To C1Dignosis.Rows.Count - 1
            oICDNode = C1Dignosis.Rows(i).Node
            If Not IsNothing(oICDNode) Then
                If Not IsNothing(oICDNode.Parent) Then


                    If Convert.ToString(oICDNode.Parent.Data).Trim = _ICDCode Then


                        If _SelectNode.Parent.Text = C1Dignosis.Rows(i)(COl_CPTCode) & "-" & C1Dignosis.Rows(i)(Col_CPTDesc) Then
                            If _selectedItems.Count > 0 Then
                                arrstrConctICD9 = _SelectNode.Parent.Parent.Parent.Text.Split("-")
                                arrstrConctCPT = _SelectNode.Parent.Text.Split("-")
                                'If C1Dignosis.Rows.Count - 1 > 0 Then
                                '    nMaxICD9Count = C1Dignosis.GetData(C1Dignosis.Rows.Count - 1, Col_ICD9Count)
                                'Else
                                '    nMaxICD9Count = 0
                                'End If '
                                nMaxICD9Count = C1Dignosis.GetData(i, Col_ICD9Count)
                                Dim oNode As C1.Win.C1FlexGrid.Node
                                Dim orow1 As C1.Win.C1FlexGrid.Row
                                Dim _newRow As Integer
                                oNode = C1Dignosis.Rows(i).Node
                                If oNode.Row.Index = 0 Then
                                    C1Dignosis.Rows.Add()
                                    _newRow = C1Dignosis.Rows.Count - 1
                                Else
                                    orow1 = C1Dignosis.Rows.Insert(oNode.Row.Index + 1)
                                    _newRow = orow1.Index
                                End If
                                For k As Integer = 0 To _selectedItems.Count - 1

                                    Dim arrstrMod() As String
                                    Dim oldmodnode As myTreeNode
                                    Dim modnode As myTreeNode
                                    Dim _row As Integer = 0
                                    If k > 0 Then

                                        _newRow = _newRow + 1
                                        C1Dignosis.Rows.Insert(_newRow)
                                    End If


                                    With C1Dignosis
                                        With .Rows(_newRow)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 2
                                            .Node.Image = Global.gloEMR.My.Resources.Resources.Modifier
                                            .Node.Data = _SelectNode.Nodes(k).Text
                                        End With
                                        arrstrMod = Split(_SelectNode.Nodes(k).Text, "-", 2)
                                        oldmodnode = New myTreeNode
                                        oldmodnode = _SelectNode.Nodes(k)
                                        modnode = New myTreeNode
                                        modnode = _SelectNode.Nodes(k)

                                        modnode.Key = oldmodnode.Key
                                        modnode.ModifierCode = oldmodnode.ModifierCode
                                        modnode.ModifierKey = oldmodnode.ModifierKey

                                        .SetData(_newRow, Col_ICD9Code, arrstrConctICD9.GetValue(0))
                                        .SetData(_newRow, Col_ICD9Desc, arrstrConctICD9.GetValue(1))
                                        .SetData(_newRow, Col_ICD9Count, nMaxICD9Count)
                                        .SetData(_newRow, Col_ICDRevision, _SelectNode.Parent.Parent.Parent.Tag)

                                        .SetData(_newRow, COl_CPTCode, arrstrConctCPT.GetValue(0))
                                        .SetData(_newRow, Col_CPTDesc, arrstrConctCPT.GetValue(1))

                                        .SetData(_newRow, Col_CPTCount, 1)


                                        .SetData(_newRow, Col_ModCode, modnode.ModifierCode)
                                        .SetData(_newRow, Col_ModDesc, arrstrMod.GetValue(1))
                                        .SetData(_newRow, Col_ModCount, 1)
                                        Dim unitnode As New myTreeNode
                                        unitnode = _SelectNode.Parent.Nodes(0)

                                        .SetData(_newRow, Col_Units, unitnode.Text.Replace("Units: ", ""))
                                        unitnode = Nothing

                                        modnode = Nothing
                                        oldmodnode = Nothing
                                    End With

                                Next
                                Exit For
                            End If


                        End If
                    End If
                End If
            End If
        Next


    End Sub



    Private Sub oModifierControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)

        pnlModifiers.Visible = False
        pnlModifiers.SendToBack()
        pnlSmartDX.Visible = True

    End Sub
    Public Function FormatNumber(ByVal Number As Decimal) As Decimal
        Dim _result As [Decimal] = Number
        Try
            Dim no As [String]() = _result.ToString().Split("."c)
            If no.GetUpperBound(0) > 0 Then
                If no(1).ToString().Length > 4 Then
                    no(1) = no(1).Substring(0, 4)
                End If
                _result = Convert.ToDecimal(no(0) + "." + no(1))
            End If
            _result = Convert.ToDecimal(_result.ToString("0.####"))
        Catch
            _result = Number
        End Try
        Return _result
    End Function


    Private Sub trICD9Association_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trICD9Association.KeyDown
        If e.KeyCode = Keys.Enter Then


            If Not IsNothing(trICD9Association.SelectedNode) Then


                Dim _selectedNode As myTreeNode = trICD9Association.SelectedNode

                trICD9Association.LabelEdit = False
                If Not IsNothing(_selectedNode) Then

                    If Convert.ToString(_selectedNode.ModifierKey) = "M1" Then
                        ' LoadUserGrid(e.X)
                        trICD9Association.SelectedNode = _selectedNode
                        LoadModfifierControl(0)
                        Me.Focus()
                        trICD9Association.SelectedNode = _selectedNode
                    ElseIf Convert.ToString(_selectedNode.UnitsKey) = "Units" Then
                        Dim strUnits() As String
                        Dim strUnit As String = ""
                        trICD9Association.SelectedNode = _selectedNode
                        If _selectedNode.Text.Contains("Units") Then
                            strUnits = _selectedNode.Text.Split(" :")
                            If strUnits.Length >= 1 Then
                                strUnit = strUnits(1)
                            End If
                        Else
                            strUnit = _selectedNode.Text
                        End If
                        trICD9Association.LabelEdit = True
                        _selectedNode.Text = strUnit
                        _selectedNode.BeginEdit()
                    Else
                        trICD9Association.LabelEdit = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnPatDiagnosis_MouseEnter(sender As Object, e As System.EventArgs) Handles btnPatDiagnosis.MouseEnter
        btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnPatDiagnosis.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

    Private Sub btnPatDiagnosis_MouseLeave(sender As Object, e As System.EventArgs) Handles btnPatDiagnosis.MouseLeave
        If pnl_btnPatDiagnosis.Dock = DockStyle.Top Then
            btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnPatDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnPatDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnPatDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub GloUC_trvICD_SearchFired() Handles GloUC_trvICD.SearchFired
        If RbICD9.Checked = True Then

            FillICD(9)
        Else
            FillICD(10)
        End If
    End Sub
End Class

