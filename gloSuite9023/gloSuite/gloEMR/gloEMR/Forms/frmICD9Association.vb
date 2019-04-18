
Option Compare Text
Imports System.Threading
Imports System.Text.RegularExpressions
Public Class frmICD9Association
    Inherits System.Windows.Forms.Form
    Dim objICD9AssociationDBLayer As ClsICD9AssociationDBLayer

    Private Const strSortByCode As String = "CODE"
    Private Const strSortByDesc As String = "DESC"
    Dim _IsCPT As Boolean = False

    Dim dtOrderbyCode As DataTable
    Dim dtOrderbyDesc As DataTable
    Dim dtCPT As New DataTable
    'sarika 26th sept 07
    Dim dtAssociates As New DataTable
    '-----------------------------------------------
    Dim ICD9Count As String
    Dim CPTCount As String

    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True

    Public Shared ISICD9AssocOpen As Boolean = False
    Public Shared ICD9SelNodeKey As Long = 0
    Public Shared ICDSmarDxName As String = ""
    Public Shared ISCopyICDSmarDx As Boolean = False
    Public Shared nProviderID As Int64 = 0
    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011

    Public Shared ICD9Code As String = ""
    Public Shared ICDRevision As Int16 = 9
    'code added for optimization in 6020()
    Private Delegate Sub fill_ControlDelegate()
    Private eCurrentSelectedButton As DockingTags
    Dim _isFormLoading As Boolean = False
    Private tooltipnew As ToolTip = Nothing

    Dim wpfICD10UserControl As gloUIControlLibrary.ICDSubCodeControl = Nothing
    Dim sSelectedICD10Code As String = String.Empty
    Dim sSelectedICD10Description As String = String.Empty
    Dim sSelectedICDRevision As Int16 = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode()
    Dim bGridFocused As Boolean = False
    Public isFromexam As Boolean = False





    Private Enum DockingTags
        ICD9Button
        ICD10Button
    End Enum

#Region " Windows Controls "
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlRightRadioBtn As System.Windows.Forms.Panel
    Friend WithEvents rbDescsearch As System.Windows.Forms.RadioButton
    Friend WithEvents rbCodesearch As System.Windows.Forms.RadioButton
    Friend WithEvents txtsearchAssociates As System.Windows.Forms.TextBox
    Friend WithEvents tblRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnltxtsearchDrugs As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnltxtsearchAssociates As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Friend WithEvents pnlLeftRadioBtnTop As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnltrICD9 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnPatientEducation As System.Windows.Forms.Panel
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnDrugs As System.Windows.Forms.Panel
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnTags As System.Windows.Forms.Panel
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnCPT As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnltrAssociates As System.Windows.Forms.Panel
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnlRightRadioBtnHeader As System.Windows.Forms.Panel
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents UCtrvICD9 As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents UCtrvAssociates As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents rbtAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtUnassociated As System.Windows.Forms.RadioButton
    Friend WithEvents rbtAssociated As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlFlowsheet As System.Windows.Forms.Panel
    Friend WithEvents btnFlowsheet As System.Windows.Forms.Button
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnLabOrders As System.Windows.Forms.Panel
    Friend WithEvents btnLabOrders As System.Windows.Forms.Button
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnOrders As System.Windows.Forms.Panel
    Friend WithEvents btnOrders As System.Windows.Forms.Button
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnReferrals As System.Windows.Forms.Panel
    Friend WithEvents btnReferrals As System.Windows.Forms.Button
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnTemplate As System.Windows.Forms.Panel
    Friend WithEvents btnTemplate As System.Windows.Forms.Button
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnICD10 As System.Windows.Forms.Button
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Private WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents btnICD9 As System.Windows.Forms.Button
    Friend WithEvents pnlbtnMUPatientEducation As System.Windows.Forms.Panel
    Friend WithEvents btnMUPatientEducation As System.Windows.Forms.Button
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents pnlTopProvider As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents chkProvider As System.Windows.Forms.CheckBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents C1SmartDX As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlGridDX As System.Windows.Forms.Panel
    Private WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label93 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label90 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label92 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents CmnuDiagnosis As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents oMnuRemoveDiagnosis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents elementHostICD10 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents tlsCodingRules As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label50 As System.Windows.Forms.Label
#End Region
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
            Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {CmnuDiagnosis}

            Dim CmpMControls() As System.Windows.Forms.ContextMenu = {cntICD9Association, cntTags}

            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try




            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                End If
            End If



            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                End If
            End If



            If (IsNothing(CmpMControls) = False) Then
                If CmpMControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpMControls)
                End If
            End If

            If (IsNothing(CmpMControls) = False) Then
                If CmpMControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenu(CmpMControls)
                End If
            End If

            frm = Nothing
        End If

        MyBase.Dispose(disposing)

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftRadioBtn As System.Windows.Forms.Panel
    Friend WithEvents txtsearchDrugs As System.Windows.Forms.TextBox
    Friend WithEvents pnltrICD9Association As System.Windows.Forms.Panel
    Friend WithEvents trICD9 As System.Windows.Forms.TreeView
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents btnPatientEducation As System.Windows.Forms.Button
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents cntICD9Association As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents pnltblMedication As System.Windows.Forms.Panel
    Friend WithEvents trICD9Association As System.Windows.Forms.TreeView
    Friend WithEvents btnTags As System.Windows.Forms.Button
    Friend WithEvents trAssociates As System.Windows.Forms.TreeView
    Friend WithEvents rbICD9Desc As System.Windows.Forms.RadioButton
    Friend WithEvents rbICD9Code As System.Windows.Forms.RadioButton
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmICD9Association))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.elementHostICD10 = New System.Windows.Forms.Integration.ElementHost()
        Me.UCtrvICD9 = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLeftRadioBtnTop = New System.Windows.Forms.Panel()
        Me.pnlLeftRadioBtn = New System.Windows.Forms.Panel()
        Me.rbtUnassociated = New System.Windows.Forms.RadioButton()
        Me.rbtAssociated = New System.Windows.Forms.RadioButton()
        Me.rbtAll = New System.Windows.Forms.RadioButton()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnICD10 = New System.Windows.Forms.Button()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnICD9 = New System.Windows.Forms.Button()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.pnltrICD9 = New System.Windows.Forms.Panel()
        Me.trICD9 = New System.Windows.Forms.TreeView()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnltxtsearchDrugs = New System.Windows.Forms.Panel()
        Me.txtsearchDrugs = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.rbICD9Desc = New System.Windows.Forms.RadioButton()
        Me.rbICD9Code = New System.Windows.Forms.RadioButton()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.UCtrvAssociates = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlbtnTemplate = New System.Windows.Forms.Panel()
        Me.btnTemplate = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.pnlbtnReferrals = New System.Windows.Forms.Panel()
        Me.btnReferrals = New System.Windows.Forms.Button()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.pnlbtnOrders = New System.Windows.Forms.Panel()
        Me.btnOrders = New System.Windows.Forms.Button()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.pnlbtnLabOrders = New System.Windows.Forms.Panel()
        Me.btnLabOrders = New System.Windows.Forms.Button()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.pnlFlowsheet = New System.Windows.Forms.Panel()
        Me.btnFlowsheet = New System.Windows.Forms.Button()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.pnlbtnTags = New System.Windows.Forms.Panel()
        Me.btnTags = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnlbtnDrugs = New System.Windows.Forms.Panel()
        Me.btnDrugs = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.pnltxtsearchAssociates = New System.Windows.Forms.Panel()
        Me.txtsearchAssociates = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnltrAssociates = New System.Windows.Forms.Panel()
        Me.trAssociates = New System.Windows.Forms.TreeView()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.pnlRightRadioBtnHeader = New System.Windows.Forms.Panel()
        Me.pnlRightRadioBtn = New System.Windows.Forms.Panel()
        Me.rbDescsearch = New System.Windows.Forms.RadioButton()
        Me.rbCodesearch = New System.Windows.Forms.RadioButton()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
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
        Me.pnlbtnCPT = New System.Windows.Forms.Panel()
        Me.btnCPT = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnltrICD9Association = New System.Windows.Forms.Panel()
        Me.trICD9Association = New System.Windows.Forms.TreeView()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnltblMedication = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblFinish = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.tblRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.tlsCodingRules = New System.Windows.Forms.ToolStripButton()
        Me.cntICD9Association = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.cntTags = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.pnlTopProvider = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.chkProvider = New System.Windows.Forms.CheckBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.C1SmartDX = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlGridDX = New System.Windows.Forms.Panel()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.CmnuDiagnosis = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.oMnuRemoveDiagnosis = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftRadioBtnTop.SuspendLayout()
        Me.pnlLeftRadioBtn.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnltrICD9.SuspendLayout()
        Me.pnltxtsearchDrugs.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRight.SuspendLayout()
        Me.pnlbtnTemplate.SuspendLayout()
        Me.pnlbtnReferrals.SuspendLayout()
        Me.pnlbtnOrders.SuspendLayout()
        Me.pnlbtnLabOrders.SuspendLayout()
        Me.pnlFlowsheet.SuspendLayout()
        Me.pnlbtnTags.SuspendLayout()
        Me.pnlbtnDrugs.SuspendLayout()
        Me.pnltxtsearchAssociates.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltrAssociates.SuspendLayout()
        Me.pnlRightRadioBtnHeader.SuspendLayout()
        Me.pnlRightRadioBtn.SuspendLayout()
        Me.pnlbtnPatientEducation.SuspendLayout()
        Me.pnlbtnMUPatientEducation.SuspendLayout()
        Me.pnlbtnCPT.SuspendLayout()
        Me.pnltrICD9Association.SuspendLayout()
        Me.pnltblMedication.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.pnlTopProvider.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.C1SmartDX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGridDX.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.CmnuDiagnosis.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.elementHostICD10)
        Me.pnlLeft.Controls.Add(Me.UCtrvICD9)
        Me.pnlLeft.Controls.Add(Me.pnlLeftRadioBtnTop)
        Me.pnlLeft.Controls.Add(Me.Panel2)
        Me.pnlLeft.Controls.Add(Me.Panel3)
        Me.pnlLeft.Controls.Add(Me.pnltrICD9)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLeft.Size = New System.Drawing.Size(267, 584)
        Me.pnlLeft.TabIndex = 1
        '
        'elementHostICD10
        '
        Me.elementHostICD10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHostICD10.Location = New System.Drawing.Point(3, 58)
        Me.elementHostICD10.Name = "elementHostICD10"
        Me.elementHostICD10.Size = New System.Drawing.Size(264, 495)
        Me.elementHostICD10.TabIndex = 46
        Me.elementHostICD10.Visible = False
        Me.elementHostICD10.Child = Nothing
        '
        'UCtrvICD9
        '
        Me.UCtrvICD9.BackColor = System.Drawing.Color.Transparent
        Me.UCtrvICD9.CheckBoxes = False
        Me.UCtrvICD9.CodeMember = Nothing
        Me.UCtrvICD9.ColonAsSeparator = False
        Me.UCtrvICD9.Comment = Nothing
        Me.UCtrvICD9.ConceptID = Nothing
        Me.UCtrvICD9.CPT = Nothing

        Me.UCtrvICD9.DescriptionMember = Nothing
        Me.UCtrvICD9.DisplayContextMenuStrip = Nothing
        Me.UCtrvICD9.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.UCtrvICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UCtrvICD9.DrugFlag = CType(16, Short)
        Me.UCtrvICD9.DrugFormMember = Nothing
        Me.UCtrvICD9.DrugQtyQualifierMember = Nothing
        Me.UCtrvICD9.DurationMember = Nothing
        Me.UCtrvICD9.EducationMappingSearchType = 1
        Me.UCtrvICD9.FrequencyMember = Nothing
        Me.UCtrvICD9.HistoryType = Nothing
        Me.UCtrvICD9.ICD9 = Nothing
        Me.UCtrvICD9.ICDRevision = Nothing
        Me.UCtrvICD9.ImageIndex = 7
        Me.UCtrvICD9.ImageList = Me.imgTreeView
        Me.UCtrvICD9.ImageObject = Nothing
        Me.UCtrvICD9.Indicator = Nothing
        Me.UCtrvICD9.IsCPTSearch = False
        Me.UCtrvICD9.IsDiagnosisSearch = False
        Me.UCtrvICD9.IsDrug = False
        Me.UCtrvICD9.IsNarcoticsMember = Nothing
        Me.UCtrvICD9.IsSearchForEducationMapping = False
        Me.UCtrvICD9.IsSystemCategory = Nothing
        Me.UCtrvICD9.Location = New System.Drawing.Point(3, 58)
        Me.UCtrvICD9.MaximumNodes = 1000
        Me.UCtrvICD9.Name = "UCtrvICD9"
        Me.UCtrvICD9.NDCCodeMember = Nothing
        Me.UCtrvICD9.ParentImageIndex = 0
        Me.UCtrvICD9.ParentMember = Nothing
        Me.UCtrvICD9.RouteMember = Nothing
        Me.UCtrvICD9.RowOrderMember = Nothing
        Me.UCtrvICD9.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.UCtrvICD9.SearchBox = True
        Me.UCtrvICD9.SearchText = Nothing
        Me.UCtrvICD9.SelectedImageIndex = 7
        Me.UCtrvICD9.SelectedNode = Nothing
        Me.UCtrvICD9.SelectedNodeIDs = CType(resources.GetObject("UCtrvICD9.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.UCtrvICD9.SelectedParentImageIndex = 0
        Me.UCtrvICD9.Size = New System.Drawing.Size(264, 495)
        Me.UCtrvICD9.SmartTreatmentId = Nothing
        Me.UCtrvICD9.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.UCtrvICD9.TabIndex = 39
        Me.UCtrvICD9.Tag = Nothing
        Me.UCtrvICD9.UnitMember = Nothing
        Me.UCtrvICD9.ValueMember = Nothing
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "ICD 9_01.ico")
        Me.imgTreeView.Images.SetKeyName(2, "CPT_01.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Drugs.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Tag.ico")
        Me.imgTreeView.Images.SetKeyName(5, "Pat Education.ico")
        Me.imgTreeView.Images.SetKeyName(6, "ICD9 Association.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(8, "FLow sheet.ico")
        Me.imgTreeView.Images.SetKeyName(9, "Lab orders.ico")
        Me.imgTreeView.Images.SetKeyName(10, "Radiology Orders.ico")
        Me.imgTreeView.Images.SetKeyName(11, "Refferal.ico")
        Me.imgTreeView.Images.SetKeyName(12, "Template.ico")
        Me.imgTreeView.Images.SetKeyName(13, "Remove Diagnosis.ico")
        '
        'pnlLeftRadioBtnTop
        '
        Me.pnlLeftRadioBtnTop.Controls.Add(Me.pnlLeftRadioBtn)
        Me.pnlLeftRadioBtnTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftRadioBtnTop.Location = New System.Drawing.Point(3, 28)
        Me.pnlLeftRadioBtnTop.Name = "pnlLeftRadioBtnTop"
        Me.pnlLeftRadioBtnTop.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlLeftRadioBtnTop.Size = New System.Drawing.Size(264, 30)
        Me.pnlLeftRadioBtnTop.TabIndex = 0
        '
        'pnlLeftRadioBtn
        '
        Me.pnlLeftRadioBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlLeftRadioBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftRadioBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtUnassociated)
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtAssociated)
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtAll)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label26)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label3)
        Me.pnlLeftRadioBtn.Controls.Add(Me.lbl_pnlRight)
        Me.pnlLeftRadioBtn.Controls.Add(Me.lbl_pnlTop)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label1)
        Me.pnlLeftRadioBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftRadioBtn.Location = New System.Drawing.Point(0, 3)
        Me.pnlLeftRadioBtn.Name = "pnlLeftRadioBtn"
        Me.pnlLeftRadioBtn.Size = New System.Drawing.Size(264, 24)
        Me.pnlLeftRadioBtn.TabIndex = 0
        '
        'rbtUnassociated
        '
        Me.rbtUnassociated.AutoSize = True
        Me.rbtUnassociated.BackColor = System.Drawing.Color.Transparent
        Me.rbtUnassociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtUnassociated.Location = New System.Drawing.Point(105, 3)
        Me.rbtUnassociated.Name = "rbtUnassociated"
        Me.rbtUnassociated.Size = New System.Drawing.Size(96, 18)
        Me.rbtUnassociated.TabIndex = 1
        Me.rbtUnassociated.TabStop = True
        Me.rbtUnassociated.Text = "Unassociated"
        Me.rbtUnassociated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtUnassociated.UseVisualStyleBackColor = False
        '
        'rbtAssociated
        '
        Me.rbtAssociated.AutoSize = True
        Me.rbtAssociated.BackColor = System.Drawing.Color.Transparent
        Me.rbtAssociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtAssociated.Location = New System.Drawing.Point(9, 3)
        Me.rbtAssociated.Name = "rbtAssociated"
        Me.rbtAssociated.Size = New System.Drawing.Size(91, 18)
        Me.rbtAssociated.TabIndex = 0
        Me.rbtAssociated.TabStop = True
        Me.rbtAssociated.Text = "Associated  "
        Me.rbtAssociated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtAssociated.UseVisualStyleBackColor = False
        '
        'rbtAll
        '
        Me.rbtAll.AutoSize = True
        Me.rbtAll.BackColor = System.Drawing.Color.Transparent
        Me.rbtAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtAll.Location = New System.Drawing.Point(213, 3)
        Me.rbtAll.Name = "rbtAll"
        Me.rbtAll.Size = New System.Drawing.Size(41, 18)
        Me.rbtAll.TabIndex = 2
        Me.rbtAll.TabStop = True
        Me.rbtAll.Text = "All "
        Me.rbtAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtAll.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(1, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(5, 22)
        Me.Label26.TabIndex = 38
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 22)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(263, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlRight.TabIndex = 19
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(264, 1)
        Me.lbl_pnlTop.TabIndex = 4
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(0, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(264, 1)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 553)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(264, 28)
        Me.Panel2.TabIndex = 41
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.btnICD10)
        Me.Panel4.Controls.Add(Me.Label55)
        Me.Panel4.Controls.Add(Me.Label76)
        Me.Panel4.Controls.Add(Me.Label77)
        Me.Panel4.Controls.Add(Me.Label78)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(0, 3)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(264, 25)
        Me.Panel4.TabIndex = 19
        '
        'btnICD10
        '
        Me.btnICD10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnICD10.BackgroundImage = CType(resources.GetObject("btnICD10.BackgroundImage"), System.Drawing.Image)
        Me.btnICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD10.FlatAppearance.BorderSize = 0
        Me.btnICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD10.Location = New System.Drawing.Point(1, 1)
        Me.btnICD10.Name = "btnICD10"
        Me.btnICD10.Size = New System.Drawing.Size(262, 23)
        Me.btnICD10.TabIndex = 9
        Me.btnICD10.Tag = "UnSelected"
        Me.btnICD10.Text = "ICD&10"
        Me.btnICD10.UseVisualStyleBackColor = False
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label55.Location = New System.Drawing.Point(1, 24)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(262, 1)
        Me.Label55.TabIndex = 8
        Me.Label55.Text = "label2"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(0, 1)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 24)
        Me.Label76.TabIndex = 7
        Me.Label76.Text = "label4"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label77.Location = New System.Drawing.Point(263, 1)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(1, 24)
        Me.Label77.TabIndex = 6
        Me.Label77.Text = "label3"
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(0, 0)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(264, 1)
        Me.Label78.TabIndex = 5
        Me.Label78.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(264, 28)
        Me.Panel3.TabIndex = 40
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.btnICD9)
        Me.Panel1.Controls.Add(Me.Label51)
        Me.Panel1.Controls.Add(Me.Label52)
        Me.Panel1.Controls.Add(Me.Label53)
        Me.Panel1.Controls.Add(Me.Label54)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 3)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(264, 25)
        Me.Panel1.TabIndex = 19
        '
        'btnICD9
        '
        Me.btnICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD9.FlatAppearance.BorderSize = 0
        Me.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD9.Location = New System.Drawing.Point(1, 1)
        Me.btnICD9.Name = "btnICD9"
        Me.btnICD9.Size = New System.Drawing.Size(262, 23)
        Me.btnICD9.TabIndex = 9
        Me.btnICD9.Tag = "Selected"
        Me.btnICD9.Text = "ICD&9"
        Me.btnICD9.UseVisualStyleBackColor = False
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label51.Location = New System.Drawing.Point(1, 24)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(262, 1)
        Me.Label51.TabIndex = 8
        Me.Label51.Text = "label2"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(0, 1)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 24)
        Me.Label52.TabIndex = 7
        Me.Label52.Text = "label4"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label53.Location = New System.Drawing.Point(263, 1)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 24)
        Me.Label53.TabIndex = 6
        Me.Label53.Text = "label3"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(0, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(264, 1)
        Me.Label54.TabIndex = 5
        Me.Label54.Text = "label1"
        '
        'pnltrICD9
        '
        Me.pnltrICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrICD9.Controls.Add(Me.trICD9)
        Me.pnltrICD9.Controls.Add(Me.Label23)
        Me.pnltrICD9.Controls.Add(Me.Label17)
        Me.pnltrICD9.Controls.Add(Me.Label18)
        Me.pnltrICD9.Controls.Add(Me.Label19)
        Me.pnltrICD9.Controls.Add(Me.Label22)
        Me.pnltrICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrICD9.Location = New System.Drawing.Point(178, 232)
        Me.pnltrICD9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrICD9.Name = "pnltrICD9"
        Me.pnltrICD9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrICD9.Size = New System.Drawing.Size(24, 53)
        Me.pnltrICD9.TabIndex = 2
        Me.pnltrICD9.Visible = False
        '
        'trICD9
        '
        Me.trICD9.BackColor = System.Drawing.Color.White
        Me.trICD9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trICD9.ForeColor = System.Drawing.Color.Black
        Me.trICD9.HideSelection = False
        Me.trICD9.ImageIndex = 7
        Me.trICD9.ImageList = Me.imgTreeView
        Me.trICD9.Indent = 20
        Me.trICD9.ItemHeight = 20
        Me.trICD9.Location = New System.Drawing.Point(4, 5)
        Me.trICD9.Name = "trICD9"
        Me.trICD9.SelectedImageIndex = 7
        Me.trICD9.ShowLines = False
        Me.trICD9.Size = New System.Drawing.Size(19, 44)
        Me.trICD9.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Location = New System.Drawing.Point(4, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(19, 4)
        Me.Label23.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 49)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(19, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 49)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(23, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 49)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(21, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnltxtsearchDrugs
        '
        Me.pnltxtsearchDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltxtsearchDrugs.Controls.Add(Me.txtsearchDrugs)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label20)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label21)
        Me.pnltxtsearchDrugs.Controls.Add(Me.PictureBox1)
        Me.pnltxtsearchDrugs.Controls.Add(Me.label9)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label12)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label24)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label25)
        Me.pnltxtsearchDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtsearchDrugs.ForeColor = System.Drawing.Color.Black
        Me.pnltxtsearchDrugs.Location = New System.Drawing.Point(362, 300)
        Me.pnltxtsearchDrugs.Name = "pnltxtsearchDrugs"
        Me.pnltxtsearchDrugs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltxtsearchDrugs.Size = New System.Drawing.Size(96, 26)
        Me.pnltxtsearchDrugs.TabIndex = 1
        Me.pnltxtsearchDrugs.Visible = False
        '
        'txtsearchDrugs
        '
        Me.txtsearchDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchDrugs.Location = New System.Drawing.Point(32, 5)
        Me.txtsearchDrugs.Name = "txtsearchDrugs"
        Me.txtsearchDrugs.Size = New System.Drawing.Size(63, 15)
        Me.txtsearchDrugs.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(63, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(63, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(4, 22)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(91, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Location = New System.Drawing.Point(3, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 39
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Location = New System.Drawing.Point(95, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 23)
        Me.Label25.TabIndex = 40
        Me.Label25.Text = "label4"
        '
        'rbICD9Desc
        '
        Me.rbICD9Desc.BackColor = System.Drawing.Color.Transparent
        Me.rbICD9Desc.Checked = True
        Me.rbICD9Desc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD9Desc.Location = New System.Drawing.Point(464, 177)
        Me.rbICD9Desc.Name = "rbICD9Desc"
        Me.rbICD9Desc.Size = New System.Drawing.Size(103, 22)
        Me.rbICD9Desc.TabIndex = 1
        Me.rbICD9Desc.TabStop = True
        Me.rbICD9Desc.Text = "Description"
        Me.rbICD9Desc.UseVisualStyleBackColor = False
        Me.rbICD9Desc.Visible = False
        '
        'rbICD9Code
        '
        Me.rbICD9Code.BackColor = System.Drawing.Color.Transparent
        Me.rbICD9Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD9Code.Location = New System.Drawing.Point(438, 198)
        Me.rbICD9Code.Name = "rbICD9Code"
        Me.rbICD9Code.Size = New System.Drawing.Size(97, 22)
        Me.rbICD9Code.TabIndex = 0
        Me.rbICD9Code.Text = "ICD9 Code"
        Me.rbICD9Code.UseVisualStyleBackColor = False
        Me.rbICD9Code.Visible = False
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.UCtrvAssociates)
        Me.pnlRight.Controls.Add(Me.pnlbtnTemplate)
        Me.pnlRight.Controls.Add(Me.pnlbtnReferrals)
        Me.pnlRight.Controls.Add(Me.pnlbtnOrders)
        Me.pnlRight.Controls.Add(Me.pnlbtnLabOrders)
        Me.pnlRight.Controls.Add(Me.pnlFlowsheet)
        Me.pnlRight.Controls.Add(Me.pnlbtnTags)
        Me.pnlRight.Controls.Add(Me.pnlbtnDrugs)
        Me.pnlRight.Controls.Add(Me.pnltxtsearchAssociates)
        Me.pnlRight.Controls.Add(Me.pnlRightRadioBtnHeader)
        Me.pnlRight.Controls.Add(Me.pnlbtnPatientEducation)
        Me.pnlRight.Controls.Add(Me.pnlbtnMUPatientEducation)
        Me.pnlRight.Controls.Add(Me.pnlbtnCPT)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(942, 54)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlRight.Size = New System.Drawing.Size(260, 584)
        Me.pnlRight.TabIndex = 3
        '
        'UCtrvAssociates
        '
        Me.UCtrvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.UCtrvAssociates.CheckBoxes = False
        Me.UCtrvAssociates.CodeMember = Nothing
        Me.UCtrvAssociates.ColonAsSeparator = False
        Me.UCtrvAssociates.Comment = Nothing
        Me.UCtrvAssociates.ConceptID = Nothing
        Me.UCtrvAssociates.CPT = Nothing
        Me.UCtrvAssociates.DescriptionMember = Nothing
        Me.UCtrvAssociates.DisplayContextMenuStrip = Nothing
        Me.UCtrvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.UCtrvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UCtrvAssociates.DrugFlag = CType(16, Short)
        Me.UCtrvAssociates.DrugFormMember = Nothing
        Me.UCtrvAssociates.DrugQtyQualifierMember = Nothing
        Me.UCtrvAssociates.DurationMember = Nothing
        Me.UCtrvAssociates.EducationMappingSearchType = 1
        Me.UCtrvAssociates.FrequencyMember = Nothing
        Me.UCtrvAssociates.HistoryType = Nothing
        Me.UCtrvAssociates.ICD9 = Nothing
        Me.UCtrvAssociates.ICDRevision = Nothing
        Me.UCtrvAssociates.ImageIndex = 7
        Me.UCtrvAssociates.ImageList = Me.imgTreeView
        Me.UCtrvAssociates.ImageObject = Nothing
        Me.UCtrvAssociates.Indicator = Nothing
        Me.UCtrvAssociates.IsCPTSearch = False
        Me.UCtrvAssociates.IsDiagnosisSearch = False
        Me.UCtrvAssociates.IsDrug = False
        Me.UCtrvAssociates.IsNarcoticsMember = Nothing
        Me.UCtrvAssociates.IsSearchForEducationMapping = False
        Me.UCtrvAssociates.IsSystemCategory = Nothing
        Me.UCtrvAssociates.Location = New System.Drawing.Point(0, 31)
        Me.UCtrvAssociates.MaximumNodes = 1000
        Me.UCtrvAssociates.Name = "UCtrvAssociates"
        Me.UCtrvAssociates.NDCCodeMember = Nothing
        Me.UCtrvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.UCtrvAssociates.ParentImageIndex = 0
        Me.UCtrvAssociates.ParentMember = Nothing
        Me.UCtrvAssociates.RouteMember = Nothing
        Me.UCtrvAssociates.RowOrderMember = Nothing
        Me.UCtrvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.UCtrvAssociates.SearchBox = True
        Me.UCtrvAssociates.SearchText = Nothing
        Me.UCtrvAssociates.SelectedImageIndex = 7
        Me.UCtrvAssociates.SelectedNode = Nothing
        Me.UCtrvAssociates.SelectedNodeIDs = CType(resources.GetObject("UCtrvAssociates.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.UCtrvAssociates.SelectedParentImageIndex = 0
        Me.UCtrvAssociates.Size = New System.Drawing.Size(260, 301)
        Me.UCtrvAssociates.SmartTreatmentId = Nothing
        Me.UCtrvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.UCtrvAssociates.TabIndex = 2
        Me.UCtrvAssociates.Tag = Nothing
        Me.UCtrvAssociates.UnitMember = Nothing
        Me.UCtrvAssociates.ValueMember = Nothing
        '
        'pnlbtnTemplate
        '
        Me.pnlbtnTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnTemplate.Controls.Add(Me.btnTemplate)
        Me.pnlbtnTemplate.Controls.Add(Me.Label56)
        Me.pnlbtnTemplate.Controls.Add(Me.Label57)
        Me.pnlbtnTemplate.Controls.Add(Me.Label58)
        Me.pnlbtnTemplate.Controls.Add(Me.Label59)
        Me.pnlbtnTemplate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnTemplate.Location = New System.Drawing.Point(0, 332)
        Me.pnlbtnTemplate.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnTemplate.Name = "pnlbtnTemplate"
        Me.pnlbtnTemplate.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnTemplate.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnTemplate.TabIndex = 50
        Me.pnlbtnTemplate.Visible = False
        '
        'btnTemplate
        '
        Me.btnTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnTemplate.BackgroundImage = CType(resources.GetObject("btnTemplate.BackgroundImage"), System.Drawing.Image)
        Me.btnTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTemplate.FlatAppearance.BorderSize = 0
        Me.btnTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTemplate.Location = New System.Drawing.Point(1, 1)
        Me.btnTemplate.Name = "btnTemplate"
        Me.btnTemplate.Size = New System.Drawing.Size(255, 23)
        Me.btnTemplate.TabIndex = 0
        Me.btnTemplate.Tag = "UnSelected"
        Me.btnTemplate.Text = "&Template"
        Me.btnTemplate.UseVisualStyleBackColor = False
        Me.btnTemplate.Visible = False
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label56.Location = New System.Drawing.Point(1, 24)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(255, 1)
        Me.Label56.TabIndex = 8
        Me.Label56.Text = "label2"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(0, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 24)
        Me.Label57.TabIndex = 7
        Me.Label57.Text = "label4"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label58.Location = New System.Drawing.Point(256, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 24)
        Me.Label58.TabIndex = 6
        Me.Label58.Text = "label3"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(0, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(257, 1)
        Me.Label59.TabIndex = 5
        Me.Label59.Text = "label1"
        '
        'pnlbtnReferrals
        '
        Me.pnlbtnReferrals.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnReferrals.Controls.Add(Me.btnReferrals)
        Me.pnlbtnReferrals.Controls.Add(Me.Label60)
        Me.pnlbtnReferrals.Controls.Add(Me.Label61)
        Me.pnlbtnReferrals.Controls.Add(Me.Label62)
        Me.pnlbtnReferrals.Controls.Add(Me.Label63)
        Me.pnlbtnReferrals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnReferrals.Location = New System.Drawing.Point(0, 360)
        Me.pnlbtnReferrals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnReferrals.Name = "pnlbtnReferrals"
        Me.pnlbtnReferrals.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnReferrals.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnReferrals.TabIndex = 49
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
        Me.btnReferrals.Size = New System.Drawing.Size(255, 23)
        Me.btnReferrals.TabIndex = 0
        Me.btnReferrals.Tag = "UnSelected"
        Me.btnReferrals.Text = "&Referral Letter"
        Me.btnReferrals.UseVisualStyleBackColor = False
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label60.Location = New System.Drawing.Point(1, 24)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(255, 1)
        Me.Label60.TabIndex = 8
        Me.Label60.Text = "label2"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(0, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 24)
        Me.Label61.TabIndex = 7
        Me.Label61.Text = "label4"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label62.Location = New System.Drawing.Point(256, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 24)
        Me.Label62.TabIndex = 6
        Me.Label62.Text = "label3"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(257, 1)
        Me.Label63.TabIndex = 5
        Me.Label63.Text = "label1"
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
        Me.pnlbtnOrders.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnOrders.TabIndex = 48
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
        Me.btnOrders.Size = New System.Drawing.Size(255, 23)
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
        Me.Label64.Size = New System.Drawing.Size(255, 1)
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
        Me.Label66.Location = New System.Drawing.Point(256, 1)
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
        Me.Label67.Size = New System.Drawing.Size(257, 1)
        Me.Label67.TabIndex = 5
        Me.Label67.Text = "label1"
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
        Me.pnlbtnLabOrders.Location = New System.Drawing.Point(0, 416)
        Me.pnlbtnLabOrders.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnLabOrders.Name = "pnlbtnLabOrders"
        Me.pnlbtnLabOrders.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLabOrders.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnLabOrders.TabIndex = 47
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
        Me.btnLabOrders.Size = New System.Drawing.Size(255, 23)
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
        Me.Label68.Size = New System.Drawing.Size(255, 1)
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
        Me.Label70.Location = New System.Drawing.Point(256, 1)
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
        Me.Label71.Size = New System.Drawing.Size(257, 1)
        Me.Label71.TabIndex = 5
        Me.Label71.Text = "label1"
        '
        'pnlFlowsheet
        '
        Me.pnlFlowsheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlFlowsheet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFlowsheet.Controls.Add(Me.btnFlowsheet)
        Me.pnlFlowsheet.Controls.Add(Me.Label72)
        Me.pnlFlowsheet.Controls.Add(Me.Label73)
        Me.pnlFlowsheet.Controls.Add(Me.Label74)
        Me.pnlFlowsheet.Controls.Add(Me.Label75)
        Me.pnlFlowsheet.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFlowsheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlFlowsheet.Location = New System.Drawing.Point(0, 444)
        Me.pnlFlowsheet.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlFlowsheet.Name = "pnlFlowsheet"
        Me.pnlFlowsheet.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlFlowsheet.Size = New System.Drawing.Size(260, 28)
        Me.pnlFlowsheet.TabIndex = 46
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
        Me.btnFlowsheet.Size = New System.Drawing.Size(255, 23)
        Me.btnFlowsheet.TabIndex = 0
        Me.btnFlowsheet.Tag = "UnSelected"
        Me.btnFlowsheet.Text = "&Flowsheet"
        Me.btnFlowsheet.UseVisualStyleBackColor = False
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label72.Location = New System.Drawing.Point(1, 24)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(255, 1)
        Me.Label72.TabIndex = 8
        Me.Label72.Text = "label2"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(0, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1, 24)
        Me.Label73.TabIndex = 7
        Me.Label73.Text = "label4"
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label74.Location = New System.Drawing.Point(256, 1)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(1, 24)
        Me.Label74.TabIndex = 6
        Me.Label74.Text = "label3"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(257, 1)
        Me.Label75.TabIndex = 5
        Me.Label75.Text = "label1"
        '
        'pnlbtnTags
        '
        Me.pnlbtnTags.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnTags.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnTags.Controls.Add(Me.btnTags)
        Me.pnlbtnTags.Controls.Add(Me.Label37)
        Me.pnlbtnTags.Controls.Add(Me.Label38)
        Me.pnlbtnTags.Controls.Add(Me.Label39)
        Me.pnlbtnTags.Controls.Add(Me.Label40)
        Me.pnlbtnTags.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnTags.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnTags.Location = New System.Drawing.Point(0, 472)
        Me.pnlbtnTags.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnTags.Name = "pnlbtnTags"
        Me.pnlbtnTags.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnTags.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnTags.TabIndex = 3
        '
        'btnTags
        '
        Me.btnTags.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnTags.BackgroundImage = CType(resources.GetObject("btnTags.BackgroundImage"), System.Drawing.Image)
        Me.btnTags.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTags.FlatAppearance.BorderSize = 0
        Me.btnTags.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTags.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTags.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTags.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTags.Location = New System.Drawing.Point(1, 1)
        Me.btnTags.Name = "btnTags"
        Me.btnTags.Size = New System.Drawing.Size(255, 23)
        Me.btnTags.TabIndex = 0
        Me.btnTags.Tag = "UnSelected"
        Me.btnTags.Text = "&Tags"
        Me.btnTags.UseVisualStyleBackColor = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(1, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(255, 1)
        Me.Label37.TabIndex = 8
        Me.Label37.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 24)
        Me.Label38.TabIndex = 7
        Me.Label38.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(256, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 24)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "label3"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(257, 1)
        Me.Label40.TabIndex = 5
        Me.Label40.Text = "label1"
        '
        'pnlbtnDrugs
        '
        Me.pnlbtnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnDrugs.Controls.Add(Me.btnDrugs)
        Me.pnlbtnDrugs.Controls.Add(Me.Label41)
        Me.pnlbtnDrugs.Controls.Add(Me.Label42)
        Me.pnlbtnDrugs.Controls.Add(Me.Label43)
        Me.pnlbtnDrugs.Controls.Add(Me.Label44)
        Me.pnlbtnDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnDrugs.Location = New System.Drawing.Point(0, 500)
        Me.pnlbtnDrugs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnDrugs.Name = "pnlbtnDrugs"
        Me.pnlbtnDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnDrugs.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnDrugs.TabIndex = 4
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDrugs.BackgroundImage = CType(resources.GetObject("btnDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDrugs.FlatAppearance.BorderSize = 0
        Me.btnDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugs.Location = New System.Drawing.Point(1, 1)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(255, 23)
        Me.btnDrugs.TabIndex = 0
        Me.btnDrugs.Tag = "UnSelected"
        Me.btnDrugs.Text = "&Drugs"
        Me.btnDrugs.UseVisualStyleBackColor = False
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(1, 24)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(255, 1)
        Me.Label41.TabIndex = 8
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 24)
        Me.Label42.TabIndex = 7
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(256, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 24)
        Me.Label43.TabIndex = 6
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(257, 1)
        Me.Label44.TabIndex = 5
        Me.Label44.Text = "label1"
        '
        'pnltxtsearchAssociates
        '
        Me.pnltxtsearchAssociates.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltxtsearchAssociates.Controls.Add(Me.txtsearchAssociates)
        Me.pnltxtsearchAssociates.Controls.Add(Me.Label6)
        Me.pnltxtsearchAssociates.Controls.Add(Me.Label7)
        Me.pnltxtsearchAssociates.Controls.Add(Me.PictureBox2)
        Me.pnltxtsearchAssociates.Controls.Add(Me.Label8)
        Me.pnltxtsearchAssociates.Controls.Add(Me.Label10)
        Me.pnltxtsearchAssociates.Controls.Add(Me.pnltrAssociates)
        Me.pnltxtsearchAssociates.Controls.Add(Me.Label15)
        Me.pnltxtsearchAssociates.Controls.Add(Me.Label27)
        Me.pnltxtsearchAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtsearchAssociates.ForeColor = System.Drawing.Color.Black
        Me.pnltxtsearchAssociates.Location = New System.Drawing.Point(0, 59)
        Me.pnltxtsearchAssociates.Name = "pnltxtsearchAssociates"
        Me.pnltxtsearchAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltxtsearchAssociates.Size = New System.Drawing.Size(240, 26)
        Me.pnltxtsearchAssociates.TabIndex = 1
        Me.pnltxtsearchAssociates.Visible = False
        '
        'txtsearchAssociates
        '
        Me.txtsearchAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchAssociates.ForeColor = System.Drawing.Color.Black
        Me.txtsearchAssociates.Location = New System.Drawing.Point(29, 5)
        Me.txtsearchAssociates.Name = "txtsearchAssociates"
        Me.txtsearchAssociates.Size = New System.Drawing.Size(207, 15)
        Me.txtsearchAssociates.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(29, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(207, 4)
        Me.Label6.TabIndex = 37
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(29, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(207, 2)
        Me.Label7.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(1, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(235, 1)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "label1"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(235, 1)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "label1"
        '
        'pnltrAssociates
        '
        Me.pnltrAssociates.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrAssociates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrAssociates.Controls.Add(Me.trAssociates)
        Me.pnltrAssociates.Controls.Add(Me.Label36)
        Me.pnltrAssociates.Controls.Add(Me.Label28)
        Me.pnltrAssociates.Controls.Add(Me.Label29)
        Me.pnltrAssociates.Controls.Add(Me.Label30)
        Me.pnltrAssociates.Controls.Add(Me.Label31)
        Me.pnltrAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrAssociates.Location = New System.Drawing.Point(18, 21)
        Me.pnltrAssociates.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrAssociates.Name = "pnltrAssociates"
        Me.pnltrAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrAssociates.Size = New System.Drawing.Size(240, 415)
        Me.pnltrAssociates.TabIndex = 2
        Me.pnltrAssociates.Visible = False
        '
        'trAssociates
        '
        Me.trAssociates.BackColor = System.Drawing.Color.White
        Me.trAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trAssociates.ForeColor = System.Drawing.Color.Black
        Me.trAssociates.HideSelection = False
        Me.trAssociates.ImageIndex = 7
        Me.trAssociates.ImageList = Me.imgTreeView
        Me.trAssociates.Indent = 20
        Me.trAssociates.ItemHeight = 20
        Me.trAssociates.Location = New System.Drawing.Point(1, 5)
        Me.trAssociates.Name = "trAssociates"
        Me.trAssociates.SelectedImageIndex = 7
        Me.trAssociates.ShowLines = False
        Me.trAssociates.Size = New System.Drawing.Size(235, 406)
        Me.trAssociates.TabIndex = 0
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.White
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Location = New System.Drawing.Point(1, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(235, 4)
        Me.Label36.TabIndex = 38
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(1, 411)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(235, 1)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "label2"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 411)
        Me.Label29.TabIndex = 7
        Me.Label29.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(236, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 411)
        Me.Label30.TabIndex = 6
        Me.Label30.Text = "label3"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(237, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(236, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 23)
        Me.Label15.TabIndex = 39
        Me.Label15.Text = "label3"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Location = New System.Drawing.Point(0, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 23)
        Me.Label27.TabIndex = 40
        Me.Label27.Text = "label3"
        '
        'pnlRightRadioBtnHeader
        '
        Me.pnlRightRadioBtnHeader.Controls.Add(Me.pnlRightRadioBtn)
        Me.pnlRightRadioBtnHeader.Location = New System.Drawing.Point(0, 31)
        Me.pnlRightRadioBtnHeader.Name = "pnlRightRadioBtnHeader"
        Me.pnlRightRadioBtnHeader.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlRightRadioBtnHeader.Size = New System.Drawing.Size(240, 28)
        Me.pnlRightRadioBtnHeader.TabIndex = 0
        Me.pnlRightRadioBtnHeader.Visible = False
        '
        'pnlRightRadioBtn
        '
        Me.pnlRightRadioBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlRightRadioBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlRightRadioBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRightRadioBtn.Controls.Add(Me.rbDescsearch)
        Me.pnlRightRadioBtn.Controls.Add(Me.rbCodesearch)
        Me.pnlRightRadioBtn.Controls.Add(Me.Label49)
        Me.pnlRightRadioBtn.Controls.Add(Me.Label14)
        Me.pnlRightRadioBtn.Controls.Add(Me.Label13)
        Me.pnlRightRadioBtn.Controls.Add(Me.Label11)
        Me.pnlRightRadioBtn.Controls.Add(Me.Label16)
        Me.pnlRightRadioBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRightRadioBtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlRightRadioBtn.Name = "pnlRightRadioBtn"
        Me.pnlRightRadioBtn.Size = New System.Drawing.Size(237, 25)
        Me.pnlRightRadioBtn.TabIndex = 6
        Me.pnlRightRadioBtn.Visible = False
        '
        'rbDescsearch
        '
        Me.rbDescsearch.BackColor = System.Drawing.Color.Transparent
        Me.rbDescsearch.Checked = True
        Me.rbDescsearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbDescsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDescsearch.Location = New System.Drawing.Point(127, 1)
        Me.rbDescsearch.Name = "rbDescsearch"
        Me.rbDescsearch.Size = New System.Drawing.Size(109, 23)
        Me.rbDescsearch.TabIndex = 1
        Me.rbDescsearch.TabStop = True
        Me.rbDescsearch.Text = "Description"
        Me.rbDescsearch.UseVisualStyleBackColor = False
        '
        'rbCodesearch
        '
        Me.rbCodesearch.BackColor = System.Drawing.Color.Transparent
        Me.rbCodesearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbCodesearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCodesearch.Location = New System.Drawing.Point(23, 1)
        Me.rbCodesearch.Name = "rbCodesearch"
        Me.rbCodesearch.Size = New System.Drawing.Size(85, 23)
        Me.rbCodesearch.TabIndex = 0
        Me.rbCodesearch.Text = " Code"
        Me.rbCodesearch.UseVisualStyleBackColor = False
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label49.Location = New System.Drawing.Point(1, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(22, 23)
        Me.Label49.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(236, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(237, 1)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Location = New System.Drawing.Point(0, 24)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(237, 1)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "label1"
        '
        'pnlbtnPatientEducation
        '
        Me.pnlbtnPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnPatientEducation.Controls.Add(Me.btnPatientEducation)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label45)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label46)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label47)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label48)
        Me.pnlbtnPatientEducation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnPatientEducation.Location = New System.Drawing.Point(0, 528)
        Me.pnlbtnPatientEducation.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnPatientEducation.Name = "pnlbtnPatientEducation"
        Me.pnlbtnPatientEducation.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnPatientEducation.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnPatientEducation.TabIndex = 5
        '
        'btnPatientEducation
        '
        Me.btnPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnPatientEducation.BackgroundImage = CType(resources.GetObject("btnPatientEducation.BackgroundImage"), System.Drawing.Image)
        Me.btnPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPatientEducation.FlatAppearance.BorderSize = 0
        Me.btnPatientEducation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPatientEducation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPatientEducation.Location = New System.Drawing.Point(1, 1)
        Me.btnPatientEducation.Name = "btnPatientEducation"
        Me.btnPatientEducation.Size = New System.Drawing.Size(255, 23)
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
        Me.Label45.Location = New System.Drawing.Point(1, 24)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(255, 1)
        Me.Label45.TabIndex = 8
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 24)
        Me.Label46.TabIndex = 7
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(256, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 24)
        Me.Label47.TabIndex = 6
        Me.Label47.Text = "label3"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(257, 1)
        Me.Label48.TabIndex = 5
        Me.Label48.Text = "label1"
        '
        'pnlbtnMUPatientEducation
        '
        Me.pnlbtnMUPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnMUPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.btnMUPatientEducation)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label79)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label80)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label81)
        Me.pnlbtnMUPatientEducation.Controls.Add(Me.Label82)
        Me.pnlbtnMUPatientEducation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnMUPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnMUPatientEducation.Location = New System.Drawing.Point(0, 556)
        Me.pnlbtnMUPatientEducation.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnMUPatientEducation.Name = "pnlbtnMUPatientEducation"
        Me.pnlbtnMUPatientEducation.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnMUPatientEducation.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnMUPatientEducation.TabIndex = 51
        '
        'btnMUPatientEducation
        '
        Me.btnMUPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnMUPatientEducation.BackgroundImage = CType(resources.GetObject("btnMUPatientEducation.BackgroundImage"), System.Drawing.Image)
        Me.btnMUPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMUPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMUPatientEducation.FlatAppearance.BorderSize = 0
        Me.btnMUPatientEducation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMUPatientEducation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMUPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMUPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMUPatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMUPatientEducation.Location = New System.Drawing.Point(1, 1)
        Me.btnMUPatientEducation.Name = "btnMUPatientEducation"
        Me.btnMUPatientEducation.Size = New System.Drawing.Size(255, 23)
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
        Me.Label79.Location = New System.Drawing.Point(1, 24)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(255, 1)
        Me.Label79.TabIndex = 8
        Me.Label79.Text = "label2"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(0, 1)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 24)
        Me.Label80.TabIndex = 7
        Me.Label80.Text = "label4"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label81.Location = New System.Drawing.Point(256, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 24)
        Me.Label81.TabIndex = 6
        Me.Label81.Text = "label3"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(0, 0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(257, 1)
        Me.Label82.TabIndex = 5
        Me.Label82.Text = "label1"
        '
        'pnlbtnCPT
        '
        Me.pnlbtnCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnCPT.Controls.Add(Me.btnCPT)
        Me.pnlbtnCPT.Controls.Add(Me.Label32)
        Me.pnlbtnCPT.Controls.Add(Me.Label33)
        Me.pnlbtnCPT.Controls.Add(Me.Label34)
        Me.pnlbtnCPT.Controls.Add(Me.Label35)
        Me.pnlbtnCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnCPT.Location = New System.Drawing.Point(0, 3)
        Me.pnlbtnCPT.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnCPT.Name = "pnlbtnCPT"
        Me.pnlbtnCPT.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnCPT.Size = New System.Drawing.Size(260, 28)
        Me.pnlbtnCPT.TabIndex = 6
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCPT.FlatAppearance.BorderSize = 0
        Me.btnCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCPT.Location = New System.Drawing.Point(1, 1)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(255, 23)
        Me.btnCPT.TabIndex = 0
        Me.btnCPT.Tag = "Selected"
        Me.btnCPT.Text = "&CPT"
        Me.btnCPT.UseVisualStyleBackColor = False
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(1, 24)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(255, 1)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "label2"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 24)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(256, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 24)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "label3"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(257, 1)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "label1"
        '
        'pnltrICD9Association
        '
        Me.pnltrICD9Association.Controls.Add(Me.trICD9Association)
        Me.pnltrICD9Association.Controls.Add(Me.Label50)
        Me.pnltrICD9Association.Controls.Add(Me.Label2)
        Me.pnltrICD9Association.Controls.Add(Me.lbl_pnlLeft)
        Me.pnltrICD9Association.Controls.Add(Me.Label4)
        Me.pnltrICD9Association.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrICD9Association.Location = New System.Drawing.Point(270, 270)
        Me.pnltrICD9Association.Name = "pnltrICD9Association"
        Me.pnltrICD9Association.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrICD9Association.Size = New System.Drawing.Size(669, 368)
        Me.pnltrICD9Association.TabIndex = 2
        '
        'trICD9Association
        '
        Me.trICD9Association.BackColor = System.Drawing.Color.White
        Me.trICD9Association.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trICD9Association.CheckBoxes = True
        Me.trICD9Association.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trICD9Association.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trICD9Association.HideSelection = False
        Me.trICD9Association.ImageIndex = 7
        Me.trICD9Association.ImageList = Me.imgTreeView
        Me.trICD9Association.Indent = 20
        Me.trICD9Association.ItemHeight = 20
        Me.trICD9Association.Location = New System.Drawing.Point(1, 4)
        Me.trICD9Association.Name = "trICD9Association"
        Me.trICD9Association.SelectedImageIndex = 7
        Me.trICD9Association.ShowLines = False
        Me.trICD9Association.Size = New System.Drawing.Size(667, 360)
        Me.trICD9Association.TabIndex = 0
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.White
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label50.Location = New System.Drawing.Point(1, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(667, 4)
        Me.Label50.TabIndex = 38
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(1, 364)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(667, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 365)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(668, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 365)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'pnltblMedication
        '
        Me.pnltblMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltblMedication.Controls.Add(Me.tblMedication)
        Me.pnltblMedication.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltblMedication.Location = New System.Drawing.Point(0, 0)
        Me.pnltblMedication.Name = "pnltblMedication"
        Me.pnltblMedication.Size = New System.Drawing.Size(1202, 54)
        Me.pnltblMedication.TabIndex = 0
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblNew, Me.tblSave, Me.tblFinish, Me.ts_gloCommunityDownload, Me.tlsCodingRules, Me.tblRefresh, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(1202, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblNew
        '
        Me.tblNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblNew.Image = CType(resources.GetObject("tblNew.Image"), System.Drawing.Image)
        Me.tblNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblNew.Name = "tblNew"
        Me.tblNew.Size = New System.Drawing.Size(37, 50)
        Me.tblNew.Tag = "New"
        Me.tblNew.Text = "&New"
        Me.tblNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblNew.ToolTipText = "New"
        Me.tblNew.Visible = False
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
        'tblFinish
        '
        Me.tblFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblFinish.Image = CType(resources.GetObject("tblFinish.Image"), System.Drawing.Image)
        Me.tblFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblFinish.Name = "tblFinish"
        Me.tblFinish.Size = New System.Drawing.Size(45, 50)
        Me.tblFinish.Tag = "Finish"
        Me.tblFinish.Text = "&Finish"
        Me.tblFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblFinish.ToolTipText = "Finish"
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
        'tblRefresh
        '
        Me.tblRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRefresh.Image = CType(resources.GetObject("tblRefresh.Image"), System.Drawing.Image)
        Me.tblRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblRefresh.Name = "tblRefresh"
        Me.tblRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tblRefresh.Text = "&Refresh"
        Me.tblRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblRefresh.Visible = False
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
        Me.tblClose.ToolTipText = "Close"
        '
        'tlsCodingRules
        '
        Me.tlsCodingRules.Image = CType(resources.GetObject("tlsCodingRules.Image"), System.Drawing.Image)
        Me.tlsCodingRules.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.tlsCodingRules.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsCodingRules.Name = "tlsCodingRules"
        Me.tlsCodingRules.Size = New System.Drawing.Size(85, 50)
        Me.tlsCodingRules.Tag = "CodingRules"
        Me.tlsCodingRules.Text = "Coding &Rule"
        Me.tlsCodingRules.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tlsCodingRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsCodingRules.Visible = False
        '
        'cntICD9Association
        '
        Me.cntICD9Association.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteICD9Item})
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 0
        Me.mnuDeleteICD9Item.Text = "Remove Associate"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(939, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 584)
        Me.Splitter2.TabIndex = 5
        Me.Splitter2.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(267, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 584)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'cntTags
        '
        Me.cntTags.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Remove Associate"
        '
        'pnlTopProvider
        '
        Me.pnlTopProvider.Controls.Add(Me.Panel6)
        Me.pnlTopProvider.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopProvider.Location = New System.Drawing.Point(270, 54)
        Me.pnlTopProvider.Name = "pnlTopProvider"
        Me.pnlTopProvider.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlTopProvider.Size = New System.Drawing.Size(669, 55)
        Me.pnlTopProvider.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button1
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label102)
        Me.Panel6.Controls.Add(Me.cmbProvider)
        Me.Panel6.Controls.Add(Me.Label89)
        Me.Panel6.Controls.Add(Me.chkProvider)
        Me.Panel6.Controls.Add(Me.txtName)
        Me.Panel6.Controls.Add(Me.Label88)
        Me.Panel6.Controls.Add(Me.Label83)
        Me.Panel6.Controls.Add(Me.Label84)
        Me.Panel6.Controls.Add(Me.Label85)
        Me.Panel6.Controls.Add(Me.Label86)
        Me.Panel6.Controls.Add(Me.Label87)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(669, 52)
        Me.Panel6.TabIndex = 0
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.ForeColor = System.Drawing.Color.Red
        Me.Label102.Location = New System.Drawing.Point(9, 20)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(14, 14)
        Me.Label102.TabIndex = 113
        Me.Label102.Text = "*"
        '
        'cmbProvider
        '
        Me.cmbProvider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(426, 16)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(230, 22)
        Me.cmbProvider.TabIndex = 43
        Me.cmbProvider.DropDownStyle = ComboBoxStyle.DropDownList
        '
        'Label89
        '
        Me.Label89.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label89.AutoSize = True
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.Location = New System.Drawing.Point(357, 20)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(66, 14)
        Me.Label89.TabIndex = 42
        Me.Label89.Text = "Provider :"
        '
        'chkProvider
        '
        Me.chkProvider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkProvider.AutoSize = True
        Me.chkProvider.Location = New System.Drawing.Point(338, 20)
        Me.chkProvider.Name = "chkProvider"
        Me.chkProvider.Size = New System.Drawing.Size(15, 14)
        Me.chkProvider.TabIndex = 1
        Me.chkProvider.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(73, 16)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(230, 22)
        Me.txtName.TabIndex = 0
        '
        'Label88
        '
        Me.Label88.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label88.AutoSize = True
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(21, 20)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(48, 14)
        Me.Label88.TabIndex = 39
        Me.Label88.Text = "Name :"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.Transparent
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label83.Location = New System.Drawing.Point(1, 1)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(5, 50)
        Me.Label83.TabIndex = 38
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label84.Location = New System.Drawing.Point(0, 1)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(1, 50)
        Me.Label84.TabIndex = 20
        Me.Label84.Text = "label4"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label85.Location = New System.Drawing.Point(668, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 50)
        Me.Label85.TabIndex = 19
        Me.Label85.Text = "label3"
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label86.Location = New System.Drawing.Point(0, 0)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(669, 1)
        Me.Label86.TabIndex = 4
        Me.Label86.Text = "label1"
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label87.Location = New System.Drawing.Point(0, 51)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(669, 1)
        Me.Label87.TabIndex = 5
        Me.Label87.Text = "label1"
        '
        'C1SmartDX
        '
        Me.C1SmartDX.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1SmartDX.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1SmartDX.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1SmartDX.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1SmartDX.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1SmartDX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SmartDX.ExtendLastCol = True
        Me.C1SmartDX.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1SmartDX.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1SmartDX.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1SmartDX.Location = New System.Drawing.Point(1, 0)
        Me.C1SmartDX.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1SmartDX.Name = "C1SmartDX"
        Me.C1SmartDX.Rows.Count = 0
        Me.C1SmartDX.Rows.DefaultSize = 21
        Me.C1SmartDX.Rows.Fixed = 0
        Me.C1SmartDX.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1SmartDX.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1SmartDX.ShowCellLabels = True
        Me.C1SmartDX.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1SmartDX.Size = New System.Drawing.Size(667, 104)
        Me.C1SmartDX.StyleInfo = resources.GetString("C1SmartDX.StyleInfo")
        Me.C1SmartDX.TabIndex = 1
        Me.C1SmartDX.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        '
        'pnlGridDX
        '
        Me.pnlGridDX.Controls.Add(Me.Label91)
        Me.pnlGridDX.Controls.Add(Me.C1SmartDX)
        Me.pnlGridDX.Controls.Add(Me.Label93)
        Me.pnlGridDX.Controls.Add(Me.Label94)
        Me.pnlGridDX.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGridDX.Location = New System.Drawing.Point(270, 137)
        Me.pnlGridDX.Name = "pnlGridDX"
        Me.pnlGridDX.Size = New System.Drawing.Size(669, 104)
        Me.pnlGridDX.TabIndex = 0
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label91.Location = New System.Drawing.Point(1, 103)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(667, 1)
        Me.Label91.TabIndex = 8
        Me.Label91.Text = "label2"
        '
        'Label93
        '
        Me.Label93.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label93.Location = New System.Drawing.Point(0, 0)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(1, 104)
        Me.Label93.TabIndex = 7
        Me.Label93.Text = "label4"
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label94.Location = New System.Drawing.Point(668, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1, 104)
        Me.Label94.TabIndex = 6
        Me.Label94.Text = "label3"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel7)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(270, 109)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel5.Size = New System.Drawing.Size(669, 28)
        Me.Panel5.TabIndex = 110
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue20071
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label5)
        Me.Panel7.Controls.Add(Me.Label90)
        Me.Panel7.Controls.Add(Me.Label95)
        Me.Panel7.Controls.Add(Me.Label96)
        Me.Panel7.Controls.Add(Me.Label97)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 3)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(669, 25)
        Me.Panel7.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(667, 23)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "  Diagnosis"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label90.Location = New System.Drawing.Point(1, 24)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(667, 1)
        Me.Label90.TabIndex = 8
        Me.Label90.Text = "label2"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(0, 1)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 24)
        Me.Label95.TabIndex = 7
        Me.Label95.Text = "label4"
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label96.Location = New System.Drawing.Point(668, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 24)
        Me.Label96.TabIndex = 6
        Me.Label96.Text = "label3"
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(0, 0)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(669, 1)
        Me.Label97.TabIndex = 5
        Me.Label97.Text = "label1"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(270, 244)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(669, 26)
        Me.Panel8.TabIndex = 111
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue20071
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label101)
        Me.Panel9.Controls.Add(Me.Label92)
        Me.Panel9.Controls.Add(Me.Label98)
        Me.Panel9.Controls.Add(Me.Label99)
        Me.Panel9.Controls.Add(Me.Label100)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(669, 26)
        Me.Panel9.TabIndex = 19
        '
        'Label101
        '
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.ForeColor = System.Drawing.Color.White
        Me.Label101.Location = New System.Drawing.Point(1, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(667, 24)
        Me.Label101.TabIndex = 41
        Me.Label101.Text = "  Treatment"
        Me.Label101.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label92.Location = New System.Drawing.Point(1, 25)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(667, 1)
        Me.Label92.TabIndex = 8
        Me.Label92.Text = "label2"
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(0, 1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(1, 25)
        Me.Label98.TabIndex = 7
        Me.Label98.Text = "label4"
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label99.Location = New System.Drawing.Point(668, 1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(1, 25)
        Me.Label99.TabIndex = 6
        Me.Label99.Text = "label3"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.Location = New System.Drawing.Point(0, 0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(669, 1)
        Me.Label100.TabIndex = 5
        Me.Label100.Text = "label1"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(270, 241)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(669, 3)
        Me.Splitter3.TabIndex = 112
        Me.Splitter3.TabStop = False
        '
        'CmnuDiagnosis
        '
        Me.CmnuDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.oMnuRemoveDiagnosis})
        Me.CmnuDiagnosis.Name = "CmnuDiagnosis"
        Me.CmnuDiagnosis.Size = New System.Drawing.Size(172, 26)
        '
        'oMnuRemoveDiagnosis
        '
        Me.oMnuRemoveDiagnosis.Name = "oMnuRemoveDiagnosis"
        Me.oMnuRemoveDiagnosis.Size = New System.Drawing.Size(171, 22)
        Me.oMnuRemoveDiagnosis.Text = "Remove Diagnosis"
        '
        'frmICD9Association
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1202, 638)
        Me.Controls.Add(Me.pnltrICD9Association)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.pnlGridDX)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.pnlTopProvider)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.rbICD9Desc)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.rbICD9Code)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnltblMedication)
        Me.Controls.Add(Me.pnltxtsearchDrugs)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmICD9Association"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Smart Diagnosis"
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftRadioBtnTop.ResumeLayout(False)
        Me.pnlLeftRadioBtn.ResumeLayout(False)
        Me.pnlLeftRadioBtn.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnltrICD9.ResumeLayout(False)
        Me.pnltxtsearchDrugs.ResumeLayout(False)
        Me.pnltxtsearchDrugs.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRight.ResumeLayout(False)
        Me.pnlbtnTemplate.ResumeLayout(False)
        Me.pnlbtnReferrals.ResumeLayout(False)
        Me.pnlbtnOrders.ResumeLayout(False)
        Me.pnlbtnLabOrders.ResumeLayout(False)
        Me.pnlFlowsheet.ResumeLayout(False)
        Me.pnlbtnTags.ResumeLayout(False)
        Me.pnlbtnDrugs.ResumeLayout(False)
        Me.pnltxtsearchAssociates.ResumeLayout(False)
        Me.pnltxtsearchAssociates.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltrAssociates.ResumeLayout(False)
        Me.pnlRightRadioBtnHeader.ResumeLayout(False)
        Me.pnlRightRadioBtn.ResumeLayout(False)
        Me.pnlbtnPatientEducation.ResumeLayout(False)
        Me.pnlbtnMUPatientEducation.ResumeLayout(False)
        Me.pnlbtnCPT.ResumeLayout(False)
        Me.pnltrICD9Association.ResumeLayout(False)
        Me.pnltblMedication.ResumeLayout(False)
        Me.pnltblMedication.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.pnlTopProvider.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.C1SmartDX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGridDX.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.CmnuDiagnosis.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmICD9Association_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ISICD9AssocOpen = False
        ICD9SelNodeKey = 0
        ICD9Code = ""
        If (IsNothing(objICD9AssociationDBLayer) = False) Then
            objICD9AssociationDBLayer.Dispose()
            objICD9AssociationDBLayer = Nothing
        End If

        If (IsNothing(dtOrderbyCode) = False) Then
            dtOrderbyCode.Dispose()
            dtOrderbyCode = Nothing
        End If
        If (IsNothing(dtOrderbyDesc) = False) Then
            dtOrderbyDesc.Dispose()
            dtOrderbyDesc = Nothing
        End If
        If (IsNothing(dtCPT) = False) Then
            dtCPT.Dispose()
            dtCPT = Nothing
        End If
        If (IsNothing(dtAssociates) = False) Then
            dtAssociates.Dispose()
            dtAssociates = Nothing
        End If

        eCurrentSelectedButton = Nothing
        If (IsNothing(tooltipnew) = False) Then
            tooltipnew.Dispose()
            tooltipnew = Nothing
        End If

        If elementHostICD10 IsNot Nothing Then
            elementHostICD10.Child = Nothing
            elementHostICD10 = Nothing
        End If

        If Me.wpfICD10UserControl IsNot Nothing Then
            RemoveHandler wpfICD10UserControl.CodeAddedToCurrent, AddressOf ICD10CodeAdded
            RemoveHandler wpfICD10UserControl.SearchFired, AddressOf ICD10SearchFired

            wpfICD10UserControl.Dispose()
            wpfICD10UserControl = Nothing
        End If

    End Sub
    'code added for optimization in 6020()
    Private Sub fill_CPTControl()
        Dim dt As DataTable = Nothing
        Dim i As Integer
        Try
            'If (InvokeRequired) Then
            '    Me.Invoke(New fill_ControlDelegate(AddressOf fill_CPTControl))
            'Else
            objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
            dt = objICD9AssociationDBLayer.FillControls(1, strSortByDesc)
            objICD9AssociationDBLayer.Dispose()
            objICD9AssociationDBLayer = Nothing
            '' For Instring Search
            '' To Fill CPT
            'dtCPT = New DataTable
            dtCPT = dt
            ' ''

            dtAssociates = dt
            '''''''''''''
            ''fill CPT treeview using user control
            UCtrvAssociates.DataSource = dtCPT
            If (IsNothing(dtCPT) = False) Then
                UCtrvAssociates.ValueMember = dtCPT.Columns("CPTID").ColumnName

                UCtrvAssociates.DescriptionMember = dtCPT.Columns("sDescription").ColumnName
                UCtrvAssociates.CodeMember = dtCPT.Columns("CPTCode").ColumnName

            End If
            UCtrvAssociates.FillTreeView()
            'fill_CPTControl()
            'Dim ocThread As Thread
            'ocThread = New Thread(New ThreadStart(AddressOf fill_CPTControl))
            'ocThread.Start()

            '''''''''''

            For i = 0 To dt.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dt.Rows(i)(2), dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
                trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
            Next
            trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            trAssociates.ExpandAll()

            'End If

        Catch ex As Exception

        End Try


    End Sub
    Private Sub frmICD9Association_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'gloAuditTrail.gloAuditTrail.UpdatePILog("ICD9 Statr")
        Me.SuspendLayout()
        _isFormLoading = True
        Try
            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            'Code end added by kanchan on 20120102 for gloCommunity integration

            'Bydefault Display all ICD9
            rbtAll.Checked = True
            trICD9Association.AllowDrop = True
            Dim rootnode As myTreeNode
            'Dim dt As New DataTable
            'Dim i As Integer
            'rootnode = New myTreeNode("ICD9", -1)
            'trICD9.Nodes.Add(rootnode)
            ''Added code on 20140213-To retrieve ICD9 and ICD10 revision


            ''dtOrderbyDesc = FillICD9(dtOrderbyDesc, strSortByDesc)

            'dt = objICD9AssociationDBLayer.FillControls(3)

            ''Populate ICD9 Data
            'For i = 0 To dt.Rows.Count - 1
            '    Dim mychildnode As myTreeNode
            '    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String))
            '    trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
            'Next
            'trICD9.ExpandAll()
            'trICD9.Select()
            Fill_Provider()
            cmbProvider.SelectedValue = nProviderID
            chkProvider.Visible = False
            rootnode = New myTreeNode("ICD9/10 Association", -1)
            rootnode.ImageIndex = 6
            rootnode.SelectedImageIndex = 6
            trICD9Association.Nodes.Add(rootnode)

            'Populate CPT data
            rootnode = New myTreeNode("CPT", -1)
            rootnode.ImageIndex = 2
            rootnode.SelectedImageIndex = 2
            trAssociates.Nodes.Add(rootnode)
            'code commented in 6020 for performance
            'dt = objICD9AssociationDBLayer.FillControls(1, strSortByDesc)

            ' '' For Instring Search
            ' '' To Fill CPT
            'dtCPT = New DataTable
            'dtCPT = dt
            '' ''

            'dtAssociates = dt
            ''''''''''''''
            ' ''fill CPT treeview using user control
            'UCtrvAssociates.DataSource = dtCPT
            'UCtrvAssociates.ValueMember = dtCPT.Columns("CPTID").ColumnName

            'UCtrvAssociates.DescriptionMember = dtCPT.Columns("sDescription").ColumnName
            'UCtrvAssociates.CodeMember = dtCPT.Columns("CPTCode").ColumnName
            'UCtrvAssociates.FillTreeView()

            ''''''''''''

            'For i = 0 To dt.Rows.Count - 1
            '    Dim mychildnode As myTreeNode
            '    mychildnode = New myTreeNode(dt.Rows(i)(2), dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
            '    trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
            'Next
            'trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            'trAssociates.ExpandAll()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'code added for optimization in 6020()
            'Dim oTS As New ThreadStart(AddressOf Me.fill_CPTControl)
            'Dim oThread As New Thread(oTS)
            'oThread.Start()
            fill_CPTControl()
            _IsCPT = True
            ' on form load visble the radion button for CPT
            pnlRightRadioBtn.Visible = True
            txtsearchDrugs.Text = ""
            txtsearchDrugs.Focus()
            txtsearchDrugs.Select()
            txtName.Text = ICDSmarDxName
            FillDiagnosis()
            ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
            If ISICD9AssocOpen = True Then
                'If ICD9SelNodeKey <> 0 Then
                If ICDRevision = gloGlobal.gloICD.CodeRevision.ICD9 Then
                    btnICD9_Click(Nothing, Nothing)
                ElseIf ICDRevision = gloGlobal.gloICD.CodeRevision.ICD10 Then
                    btnICD10_Click(Nothing, Nothing)
                End If
                trICD9Association.Nodes.Item(0).Nodes.Clear()

                FillICD9ICD10()
                AddNode()
                'Dim newnode As gloUserControlLibrary.myTreeNode
                'For Each newnode In UCtrvICD9.Nodes
                '    If newnode.ID = ICD9SelNodeKey Then
                '        AddNode(newnode)
                '        Exit For
                '    End If
                'Next
                trICD9Association.ExpandAll()
                'End If
            ElseIf gblnIcd10MasterTransition = True Then 'gblnIcd10Transition
                btnICD10_Click(Nothing, Nothing)
            Else
                btnICD9_Click(Nothing, Nothing)
            End If
            FillTagsAssociates() 'filling context menu for tags       
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            _isFormLoading = False
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub ICD10SearchFired(ByVal SearchText As String)
        Dim dt As DataTable = Nothing
        Dim oclsCPTICD9 As ClsICD9AssociationDBLayer = Nothing
        Dim IsAssociated As Int16 = 0
        Try
            oclsCPTICD9 = New ClsICD9AssociationDBLayer

            If rbtAssociated.Checked Then
                IsAssociated = 1
            ElseIf rbtUnassociated.Checked Then
                IsAssociated = 2
            End If

            dt = oclsCPTICD9.FetchAssociatedICD10(SearchText, IsAssociated)

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

    Private Sub ICD10CodeAdded()

        Dim dtBillableCodes As DataTable = Nothing
        Dim targetnode1 As myTreeNode = CType(trICD9Association.SelectedNode, myTreeNode)
        Dim SelectedICDCode As gloUIControlLibrary.Classes.ICD10.clsICD10 = Nothing

        Dim oclsCPTICD9 As ClsICD9AssociationDBLayer = Nothing
        Try


            SelectedICDCode = wpfICD10UserControl.GetSelectedICDCode()

            If SelectedICDCode.CodeType = 0 Then

                oclsCPTICD9 = New ClsICD9AssociationDBLayer
                dtBillableCodes = oclsCPTICD9.GetBillableICD10Codes(SelectedICDCode.ICD10Code)

                For Each element As DataRow In dtBillableCodes.Rows

                    If (C1SmartDX.Rows.Count = 0 Or C1SmartDX.FindRow(element("sICD10Code"), 1, 0, True) <= 0) Then
                        C1SmartDX.BeginUpdate()
                        C1SmartDX.Rows.Add()
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 0, element("sICD10Code"))
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 1, element("sDescriptionLong"))
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 2, element("nICD10ID"))
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 3, gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                        C1SmartDX.EndUpdate()
                        If txtName.Text.Trim() = "" Then
                            txtName.Text = SelectedICDCode.LongDescription
                        End If
                    End If
                Next

            Else

                If (C1SmartDX.Rows.Count = 0 Or C1SmartDX.FindRow(SelectedICDCode.ICD10Code, 1, 0, True) <= 0) Then
                    C1SmartDX.BeginUpdate()
                    C1SmartDX.Rows.Add()
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 0, SelectedICDCode.ICD10Code)
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 1, SelectedICDCode.LongDescription)
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 2, SelectedICDCode.ICDCodeID)
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 3, gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                    C1SmartDX.EndUpdate()
                    If txtName.Text.Trim() = "" Then
                        txtName.Text = SelectedICDCode.LongDescription
                    End If
                End If

            End If
            If C1SmartDX.Rows.Count > 0 Then
                If C1SmartDX.RowSel > 0 Then
                    If (Convert.ToInt16(C1SmartDX.GetData(C1SmartDX.RowSel, 3)) = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()) Then
                        tlsCodingRules.Visible = True
                    Else
                        tlsCodingRules.Visible = False
                    End If
                End If

            End If
            'lstCodesToSave = wpfICD10UserControl.GetCodesToSave()


        Catch ex As Exception

        End Try
    End Sub

    ''chetan added to Fill the Associates of Tags in ContaxMenu
    Private Sub FillTagsAssociates()
        Dim dtTags As DataTable = Nothing
        Try
            Dim objclsSmartDiagnosis As clsSmartDiagnosis
            objclsSmartDiagnosis = New clsSmartDiagnosis
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
            ''Fixed:#7827:Edit >> Smart Dignosis >> Tags >> Application is not showing Right click Option
            ' oChildItem.Dispose()
            oChildItem = Nothing

            For i = 0 To dtTags.Rows.Count - 1
                oChildItem = New MenuItem
                oChildItem.Text = dtTags.Rows(i)(1).ToString
                With cntTags.MenuItems
                    .Add(oChildItem)
                End With
                AddHandler oChildItem.Click, AddressOf cntTags_Click
                ''Fixed:#7827:Edit >> Smart Dignosis >> Tags >> Application is not showing Right click Option
                ' oChildItem.Dispose()
                oChildItem = Nothing
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ''Fixed:#7827:Edit >> Smart Dignosis >> Tags >> Application is not showing Right click Option
            If Not IsNothing(dtTags) Then
                dtTags.Dispose()
                dtTags = Nothing
            End If
        End Try
    End Sub

    ''''Chetan Added  Event Handler for cntTags.Click
    Public Sub cntTags_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        If oCurrentMenu.Text.Trim() = "Remove  Associate" Then
            If Not trICD9Association.SelectedNode Is trICD9Association.Nodes.Item(0) OrElse Not trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Then

                Dim mychildnode As myTreeNode
                'Dim key As Int64
                mychildnode = CType(trICD9Association.SelectedNode, myTreeNode)
                'objMedicationDBLayer.DeleteMedication(mychildnode.Index) 'delete from collection
                'key = mychildnode.Key
                mychildnode.Remove() 'delete from Medicationdetails treeview

                'Add the deleted node to Medication treeview

            End If
        Else

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
                        ' oCurrentMenu.Dispose()
                        oCurrentMenu = Nothing
                    End If
                End If
            Catch ex As Exception
                ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule., gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    'Private Function FillICD9(ByVal dt As DataTable, ByVal strSortBy As String) As DataTable
    '    ''''' dt = dtOrderbyCode Or dtOrderbyDesc 
    '    ''''' Flag  =0 then Pull ICD9 OrderBy ICD9Code
    '    ''''' Flag  =1 then Pull ICD9 OrderBy Description
    '    If IsNothing(dt) = True Then
    '        dt = New DataTable
    '    End If

    '    'Dim i As Integer
    '    'Dim dt As DataTable

    '    If dt.Rows.Count = 0 Then
    '        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer

    '        If eCurrentSelectedButton = DockingTags.ICD9Button Then
    '            dt = objICD9AssociationDBLayer.FillControls(3, UCtrvICD9.txtsearch.Text)
    '        ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then
    '            dt = objICD9AssociationDBLayer.FillControls(15, UCtrvICD9.txtsearch.Text)
    '        End If

    '        objICD9AssociationDBLayer.Dispose()
    '        objICD9AssociationDBLayer = Nothing
    '        '' 0 = nICD9ID ,
    '        '' 1 = sICD9code+'-'+sDescription, 
    '        '' 2 = sDescription AS sDescription, 
    '        '' 3 = sICD9code AS ICD9code    
    '        UCtrvICD9.DataSource = dt
    '        UCtrvICD9.ValueMember = dt.Columns("nICD9ID").ColumnName
    '        UCtrvICD9.Tag = dt.Columns(0).ColumnName
    '        UCtrvICD9.DescriptionMember = dt.Columns("sDescription").ColumnName
    '        UCtrvICD9.CodeMember = dt.Columns("sICD9Code").ColumnName
    '        UCtrvICD9.IsDiagnosisSearch = True
    '        UCtrvICD9.FillTreeView()
    '        ChangeIconForAssoication()
    '    End If

    '    '' COMMENT BY SUDHIR 20090513 '' NEW USER CONTROL USED ''
    '    'If IsNothing(dt) = False Then
    '    '    trICD9.Hide()
    '    '    trICD9.Nodes.Clear()
    '    '    Dim rootnode As TreeNode
    '    '    rootnode = New myTreeNode("ICD9", -1)
    '    '    rootnode.ImageIndex = 1
    '    '    rootnode.SelectedImageIndex = 1
    '    '    trICD9.Nodes.Add(rootnode)

    '    '    If dt.Rows.Count < 400 Then
    '    '        ICD9Count = dt.Rows.Count - 1
    '    '    Else
    '    '        ICD9Count = 400
    '    '    End If
    '    '    'Populate ICD9 Data

    '    '    For i = 0 To ICD9Count 'dt.Rows.Count - 1
    '    '        Dim mychildnode As myTreeNode
    '    '        mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String))
    '    '        trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
    '    '    Next
    '    'End If
    '    'trICD9.ExpandAll()
    '    'trICD9.Show()
    '    'trICD9.Select()
    '    Return dt

    'End Function

    Private Sub PopulateAssociates(ByVal id As Int16, Optional ByVal strsearch As String = "")
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        If id = 0 Then
            putallbuttondown()
            putselectedpanelontop("pnlbtnDrugs")
            ''With pnlbtnDrugs
            ''    pnlbtnDrugs.Dock = DockStyle.Top
            ''    pnlRightRadioBtnHeader.Visible = True
            ''    btnDrugs.ForeColor = Color.FromArgb(31, 73, 125)
            ''    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            ''    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnDrugs.Tag = "Selected"
            ''    btnDrugs.BringToFront()
            ''    trAssociates.BringToFront()

            ''    pnlbtnCPT.Dock = DockStyle.Bottom
            ''    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnCPT.Tag = "UnSelected"

            ''    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            ''    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnPatientEducation.Tag = "UnSelected"

            ''    pnlbtnTags.Dock = DockStyle.Bottom
            ''    btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnTags.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnTags.Tag = "UnSelected"

            ''End With

        ElseIf id = 1 Then
            putallbuttondown()
            putselectedpanelontop("pnlbtnCPT")
            ''With pnlbtnCPT
            ''    pnlbtnCPT.Dock = DockStyle.Top
            ''    btnCPT.ForeColor = Color.FromArgb(31, 73, 125)
            ''    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            ''    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnCPT.Tag = "Selected"

            ''    pnlbtnTags.Dock = DockStyle.Bottom
            ''    btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnTags.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnTags.Tag = "UnSelected"

            ''    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            ''    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnPatientEducation.Tag = "UnSelected"

            ''    pnlbtnDrugs.Dock = DockStyle.Bottom
            ''    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnDrugs.Tag = "UnSelected"


            ''End With


        ElseIf id = 2 Then
            putallbuttondown()
            putselectedpanelontop("pnlbtnPatientEducation")
            ''With pnlbtnPatientEducation
            ''    pnlbtnPatientEducation.Dock = DockStyle.Top
            ''    btnPatientEducation.ForeColor = Color.FromArgb(31, 73, 125)
            ''    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            ''    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnPatientEducation.Tag = "Selected"
            ''    btnPatientEducation.BringToFront()
            ''    trAssociates.BringToFront()

            ''    pnlbtnDrugs.Dock = DockStyle.Bottom
            ''    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnDrugs.Tag = "UnSelected"

            ''    pnlbtnTags.Dock = DockStyle.Bottom
            ''    btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnTags.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnTags.Tag = "UnSelected"

            ''    pnlbtnCPT.Dock = DockStyle.Bottom
            ''    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnCPT.Tag = "UnSelected"


            ''End With


        ElseIf id = 4 Then
            putallbuttondown()
            putselectedpanelontop("pnlbtnTags")
            ''With pnlbtnTags
            ''    pnlbtnTags.Dock = DockStyle.Top
            ''    btnTags.ForeColor = Color.FromArgb(31, 73, 125)
            ''    btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            ''    btnTags.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnTags.Tag = "Selected"
            ''    btnTags.BringToFront()
            ''    trAssociates.BringToFront()

            ''    pnlbtnCPT.Dock = DockStyle.Bottom
            ''    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnCPT.Tag = "UnSelected"

            ''    pnlbtnDrugs.Dock = DockStyle.Bottom
            ''    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnDrugs.Tag = "UnSelected"


            ''    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            ''    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            ''    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            ''    btnPatientEducation.Tag = "UnSelected"

            ''End With

        ElseIf id = 10 Then 'refferal letter
            putallbuttondown()
            putselectedpanelontop("pnlbtnReferrals")
            'With pnlbtnReferrals

            '    pnlbtnReferrals.Dock = DockStyle.Top
            '    btnReferrals.ForeColor = Color.FromArgb(31, 73, 125)
            '    btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            '    btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
            '    btnReferrals.Tag = "Selected"
            '    btnReferrals.BringToFront()
            '    trAssociates.BringToFront()

            '    pnlbtnCPT.Dock = DockStyle.Bottom
            '    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            '    btnCPT.Tag = "UnSelected"

            '    pnlbtnDrugs.Dock = DockStyle.Bottom
            '    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            '    btnDrugs.Tag = "UnSelected"


            '    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            '    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            '    btnPatientEducation.Tag = "UnSelected"

            '    End With
        ElseIf id = 11 Then ' flow sheet
            putallbuttondown()
            putselectedpanelontop("pnlFlowsheet")
            'With pnlFlowsheet

            '    pnlFlowsheet.Dock = DockStyle.Top
            '    btnFlowsheet.ForeColor = Color.FromArgb(31, 73, 125)
            '    btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            '    btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
            '    btnFlowsheet.Tag = "Selected"
            '    btnFlowsheet.BringToFront()
            '    trAssociates.BringToFront()

            '    pnlbtnCPT.Dock = DockStyle.Bottom
            '    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            '    btnCPT.Tag = "UnSelected"

            '    pnlbtnDrugs.Dock = DockStyle.Bottom
            '    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            '    btnDrugs.Tag = "UnSelected"


            '    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            '    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            '    btnPatientEducation.Tag = "UnSelected"

            '   End With
        ElseIf id = 12 Then ' Orders

            putallbuttondown()
            putselectedpanelontop("pnlbtnOrders")

            'With pnlbtnOrders

            '    pnlbtnOrders.Dock = DockStyle.Top
            '    btnOrders.ForeColor = Color.FromArgb(31, 73, 125)
            '    btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            '    btnOrders.BackgroundImageLayout = ImageLayout.Stretch
            '    btnOrders.Tag = "Selected"
            '    btnOrders.BringToFront()
            '    trAssociates.BringToFront()

            '    pnlbtnCPT.Dock = DockStyle.Bottom
            '    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            '    btnCPT.Tag = "UnSelected"

            '    pnlbtnDrugs.Dock = DockStyle.Bottom
            '    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            '    btnDrugs.Tag = "UnSelected"


            '    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            '    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            '    btnPatientEducation.Tag = "UnSelected"

            'End With

        ElseIf id = 13 Then ' Template

            putallbuttondown()
            putselectedpanelontop("pnlbtnTemplate")

            'With pnlbtnTemplate

            '    pnlbtnTemplate.Dock = DockStyle.Top
            '    btnTemplate.ForeColor = Color.FromArgb(31, 73, 125)
            '    btnTemplate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            '    btnTemplate.BackgroundImageLayout = ImageLayout.Stretch
            '    btnTemplate.Tag = "Selected"
            '    btnTemplate.BringToFront()
            '    trAssociates.BringToFront()

            '    pnlbtnCPT.Dock = DockStyle.Bottom
            '    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            '    btnCPT.Tag = "UnSelected"

            '    pnlbtnDrugs.Dock = DockStyle.Bottom
            '    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            '    btnDrugs.Tag = "UnSelected"


            '    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            '    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            '    btnPatientEducation.Tag = "UnSelected"

            'End With


        ElseIf id = 14 Then ' Lab Order 
            putallbuttondown()
            putselectedpanelontop("pnlbtnLabOrders")
            'With pnlbtnLabOrders

            '    pnlbtnLabOrders.Dock = DockStyle.Top
            '    btnLabOrders.ForeColor = Color.FromArgb(31, 73, 125)
            '    btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            '    btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
            '    btnLabOrders.Tag = "Selected"
            '    btnLabOrders.BringToFront()
            '    trAssociates.BringToFront()

            '    pnlbtnCPT.Dock = DockStyle.Bottom
            '    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            '    btnCPT.Tag = "UnSelected"

            '    pnlbtnDrugs.Dock = DockStyle.Bottom
            '    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
            '    btnDrugs.Tag = "UnSelected"


            '    pnlbtnPatientEducation.Dock = DockStyle.Bottom
            '    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            '    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
            '    btnPatientEducation.Tag = "UnSelected"

            '   End With
        ElseIf id = 16 Then ' Mu Patient Education
            putallbuttondown()
            putselectedpanelontop("pnlbtnMUPatientEducation")
        End If

        Dim dt As DataTable

        ''Sandip Darade 20091014
        If Not IsNothing(UCtrvAssociates.txtsearch.Text) Then
            strsearch = UCtrvAssociates.txtsearch.Text
        End If
        If gblnResetSearchTextBox = True Then
            strsearch = ""
        End If
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        dt = objICD9AssociationDBLayer.FillControls(id, strsearch)
        objICD9AssociationDBLayer.Dispose()
        objICD9AssociationDBLayer = Nothing
        dtAssociates = dt

        If id = 1 Then
            '' To Fill CPT
            'dtCPT = New DataTable
            dtCPT = dt
            UCtrvAssociates.Clear()
            UCtrvAssociates.DataSource = dt
            If (IsNothing(dt) = False) Then
                UCtrvAssociates.ValueMember = dt.Columns("CPTID").ColumnName
                UCtrvAssociates.DescriptionMember = dt.Columns("sDescription").ColumnName
                UCtrvAssociates.CodeMember = dt.Columns("CPTCode").ColumnName
            End If
            UCtrvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            UCtrvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
            UCtrvAssociates.FillTreeView()
        ElseIf id = 0 Then
            UCtrvAssociates.Clear()
            UCtrvAssociates.DataSource = dt
            UCtrvAssociates.IsDrug = True
            UCtrvAssociates.DrugFlag = 16 ''all drugs 
            If (IsNothing(dt) = False) Then
                UCtrvAssociates.ValueMember = dt.Columns("DrugsID").ColumnName
                UCtrvAssociates.DescriptionMember = dt.Columns("Dosage").ColumnName
                UCtrvAssociates.CodeMember = dt.Columns("DrugName").ColumnName
                UCtrvAssociates.DrugFormMember = dt.Columns("DrugForm").ColumnName
                UCtrvAssociates.RouteMember = Convert.ToString(dt.Columns("sRoute").ColumnName)
                UCtrvAssociates.NDCCodeMember = Convert.ToString(dt.Columns("sNDCCode").ColumnName) '''''bug fix for 6849
                UCtrvAssociates.IsNarcoticsMember = dt.Columns("nIsNarcotics").ColumnName
                UCtrvAssociates.FrequencyMember = dt.Columns("sFrequency").ColumnName
                UCtrvAssociates.DurationMember = dt.Columns("sDuration").ColumnName
                UCtrvAssociates.DrugQtyQualifierMember = dt.Columns("sDrugQtyQualifier").ColumnName
                UCtrvAssociates.mpidmember = dt.Columns("mpid").ColumnName
            End If

            UCtrvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
            UCtrvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
            UCtrvAssociates.FillTreeView()

        Else '' If Tags or Patient Education

            ''IF Tags clicked column 0 = TemplateID AND column 1 = TemplateName
            ''IF Patient Education clicked  column 0 = nTemplateID AND column 1 = sTemplateName
            UCtrvAssociates.Clear()
            UCtrvAssociates.DataSource = dt
            If (IsNothing(dt) = False) Then
                UCtrvAssociates.ValueMember = Convert.ToString(dt.Columns(0).ColumnName)
                UCtrvAssociates.DescriptionMember = Convert.ToString(dt.Columns(1).ColumnName)
                UCtrvAssociates.CodeMember = Convert.ToString(dt.Columns(1).ColumnName)
            End If

            UCtrvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            UCtrvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation

            UCtrvAssociates.FillTreeView()
        End If
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub

    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmICD9Association
    Public Shared IsOpen As Boolean = False

    Public Shared Function GetInstance() As frmICD9Association
        '_mu.WaitOne()
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmICD9Association" Then
                    ''If CType(f, frmICD9Association) = PatientID Then
                    IsOpen = True
                    frm = f
                    ''End If

                End If
            Next
            If (IsOpen = False) Then
                ''frm = New frmICD9Association(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
                frm = New frmICD9Association()
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


    Private Sub putallbuttondown()
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        pnlbtnCPT.Dock = DockStyle.Bottom
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        btnCPT.Tag = "UnSelected"

        pnlbtnDrugs.Dock = DockStyle.Bottom
        btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        btnDrugs.Tag = "UnSelected"

        pnlbtnPatientEducation.Dock = DockStyle.Bottom
        btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        btnPatientEducation.Tag = "UnSelected"

        pnlbtnMUPatientEducation.Dock = DockStyle.Bottom
        btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
        btnMUPatientEducation.Tag = "UnSelected"

        pnlbtnTags.Dock = DockStyle.Bottom

        btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnTags.BackgroundImageLayout = ImageLayout.Stretch
        btnTags.Tag = "UnSelected"

        ''Added Rahul on 20101013
        pnlbtnReferrals.Dock = DockStyle.Bottom

        btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        btnReferrals.Tag = "UnSelected"

        pnlFlowsheet.Dock = DockStyle.Bottom
        btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        btnFlowsheet.Tag = "UnSelected"

        pnlbtnOrders.Dock = DockStyle.Bottom
        btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        btnOrders.Tag = "UnSelected"

        pnlbtnTemplate.Dock = DockStyle.Bottom
        btnTemplate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnTemplate.BackgroundImageLayout = ImageLayout.Stretch
        btnTemplate.Tag = "UnSelected"

        pnlbtnLabOrders.Dock = DockStyle.Bottom
        btnLabOrders.ForeColor = Color.FromArgb(31, 73, 125)
        btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
        btnLabOrders.Tag = "UnSelected"
        ''
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011

    End Sub

    Private Sub putselectedpanelontop(ByVal panelname As String)
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        Try

            Select Case panelname
                Case "pnlbtnDrugs"
                    pnlbtnDrugs.Dock = DockStyle.Top
                    pnlRightRadioBtnHeader.Visible = True
                    btnDrugs.ForeColor = Color.FromArgb(31, 73, 125)
                    btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    btnDrugs.Tag = "Selected"
                    btnDrugs.BringToFront()
                    trAssociates.BringToFront()


                Case "pnlbtnCPT"


                    pnlbtnCPT.Dock = DockStyle.Top
                    btnCPT.ForeColor = Color.FromArgb(31, 73, 125)
                    btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
                    btnCPT.Tag = "Selected"

                Case "pnlbtnPatientEducation"



                    pnlbtnPatientEducation.Dock = DockStyle.Top
                    btnPatientEducation.ForeColor = Color.FromArgb(31, 73, 125)
                    btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
                    btnPatientEducation.Tag = "Selected"
                    btnPatientEducation.BringToFront()
                    trAssociates.BringToFront()


                Case "pnlbtnTags"
                    pnlbtnTags.Dock = DockStyle.Top
                    btnTags.ForeColor = Color.FromArgb(31, 73, 125)
                    btnTags.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnTags.BackgroundImageLayout = ImageLayout.Stretch
                    btnTags.Tag = "Selected"
                    btnTags.BringToFront()
                    trAssociates.BringToFront()

                    ''Added Rahul on 20101013
                Case "pnlbtnReferrals"

                    pnlbtnReferrals.Dock = DockStyle.Top
                    btnReferrals.ForeColor = Color.FromArgb(31, 73, 125)
                    btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
                    btnReferrals.Tag = "Selected"
                    btnReferrals.BringToFront()
                    trAssociates.BringToFront()

                Case "pnlFlowsheet"

                    pnlFlowsheet.Dock = DockStyle.Top
                    btnFlowsheet.ForeColor = Color.FromArgb(31, 73, 125)
                    btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
                    btnFlowsheet.Tag = "Selected"
                    btnFlowsheet.BringToFront()
                    trAssociates.BringToFront()

                Case "pnlbtnOrders"

                    pnlbtnOrders.Dock = DockStyle.Top
                    btnOrders.ForeColor = Color.FromArgb(31, 73, 125)
                    btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnOrders.BackgroundImageLayout = ImageLayout.Stretch
                    btnOrders.Tag = "Selected"
                    btnOrders.BringToFront()
                    trAssociates.BringToFront()

                Case "pnlbtnTemplate"
                    pnlbtnTemplate.Dock = DockStyle.Top
                    btnTemplate.ForeColor = Color.FromArgb(31, 73, 125)
                    btnTemplate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnTemplate.BackgroundImageLayout = ImageLayout.Stretch
                    btnTemplate.Tag = "Selected"
                    btnTemplate.BringToFront()
                    trAssociates.BringToFront()



                Case "pnlbtnLabOrders"

                    pnlbtnLabOrders.Dock = DockStyle.Top
                    btnLabOrders.ForeColor = Color.FromArgb(31, 73, 125)
                    btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
                    btnLabOrders.Tag = "Selected"
                    btnLabOrders.BringToFront()
                    trAssociates.BringToFront()
                Case "pnlbtnMUPatientEducation"

                    pnlbtnMUPatientEducation.Dock = DockStyle.Top
                    btnMUPatientEducation.ForeColor = Color.FromArgb(31, 73, 125)
                    btnMUPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                    btnMUPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
                    btnMUPatientEducation.Tag = "Selected"
                    btnMUPatientEducation.BringToFront()
                    trAssociates.BringToFront()

                    ''
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub

    Private Sub txtsearchDrugs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchDrugs.TextChanged
        'Try
        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trICD9.Nodes.Item(0).Nodes
        '        'compare selected node text and entered text
        '        Dim str As String
        '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchDrugs.Text))))
        '        If str = UCase(Trim(txtsearchDrugs.Text)) Then
        '            trICD9.SelectedNode = mychildnode
        '            txtsearchDrugs.Focus()
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        Try

            '''''''''''''''''''####################''''''''''''''''''''''''''
            '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
            Dim strSearchDetails As String
            If Trim(txtsearchDrugs.Text) <> "" Then
                strSearchDetails = Replace(txtsearchDrugs.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If
            '''''''''''''''''''####################''''''''''''''''''''''''''

            'child node collection
            If rbICD9Desc.Checked = True Then
                FillICD9TreeView(dtOrderbyDesc, strSearchDetails)
            Else
                FillICD9TreeView(dtOrderbyCode, strSearchDetails)
            End If
            'txtsearchDrugs.Focus()
            'Exit Sub

            If Trim(strSearchDetails) <> "" Then
                If trICD9.Nodes.Item(0).GetNodeCount(False) > 0 Then
                    Dim mychildnode As myTreeNode
                    For Each mychildnode In trICD9.Nodes.Item(0).Nodes
                        'search against Description
                        If rbICD9Desc.Checked = True Then
                            If UCase(Mid(mychildnode.Tag, 1, Len(Trim(strSearchDetails)))) = UCase(Trim(strSearchDetails)) Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trICD9.SelectedNode = mychildnode
                                txtsearchDrugs.Focus()
                                Exit Sub
                            End If
                        Else
                            'search against ICD9 Code
                            Dim str As String
                            str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
                            str = Mid(str, 1, Len(Trim(strSearchDetails)))
                            If UCase(str) = UCase(Trim(strSearchDetails)) Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trICD9.SelectedNode = mychildnode
                                txtsearchDrugs.Focus()
                                Exit Sub
                            End If
                        End If
                    Next


                    ' '' 20070922 MAhesh InString Search 
                    For Each mychildnode In trICD9.Nodes.Item(0).Nodes
                        'search against Description
                        If rbICD9Desc.Checked = True Then
                            If InStr(UCase(mychildnode.Tag), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trICD9.SelectedNode = mychildnode
                                txtsearchDrugs.Focus()
                                Exit Sub
                            End If
                        Else
                            'search against ICD9 Code
                            Dim str As String
                            str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
                            ' str = Mid(str, 1, Len(Trim(txtsearchDrugs.Text)))
                            If InStr(UCase(str), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                                '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                                '*************
                                'select matching node
                                trICD9.SelectedNode = mychildnode
                                txtsearchDrugs.Focus()
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If
            txtsearchDrugs.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub trICD9Association_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trICD9Association.DragOver
        Try
            'If IsNothing(trICD9Association.SelectedNode) = True Then
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trICD9_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trICD9.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trICD9_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trICD9.DragDrop, trICD9Association.DragDrop
        Try
            'If IsNothing(trICD9Association.SelectedNode) = True Then
            '    Exit Sub
            'End If

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
            If dropNode.TreeView Is trICD9 Then
                If dropNode.Parent Is trICD9.Nodes.Item(0) Then
                    Dim str As String
                    str = dropNode.Key
                    Dim mytragetnode As myTreeNode
                    For Each mytragetnode In trICD9Association.Nodes.Item(0).Nodes
                        If mytragetnode.Key = str Then
                            txtsearchDrugs.Text = ""
                            txtsearchDrugs.Focus()
                            Exit Sub
                        End If
                    Next
                    'dropNode.Remove()
                    'If PopulateMedication(dropNode.Key) Then
                    Dim associatenode As myTreeNode

                    associatenode = dropNode.Clone
                    associatenode.Key = dropNode.Key
                    associatenode.Text = dropNode.Text
                    associatenode.ImageIndex = 1
                    associatenode.SelectedImageIndex = 1

                    trICD9Association.Nodes.Item(0).Nodes.Add(associatenode)

                    Dim MyChild As New myTreeNode

                    MyChild.Text = "CPT"
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

                    'trICD9Association.Nodes.Item(0).Nodes.Add(associatenode)

                    'associatenode.Nodes.Add(New myTreeNode("CPT", -1))
                    'associatenode.Nodes.Add(New myTreeNode("Drugs", -1))
                    'associatenode.Nodes.Add(New myTreeNode("Patient Education", -1))
                    'associatenode.Nodes.Add(New myTreeNode("Tags", -1))


                    Dim dt As DataTable
                    If (IsNothing(objICD9AssociationDBLayer)) Then
                        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
                    End If
                    dt = objICD9AssociationDBLayer.FetchICD9forUpdate(associatenode.Key)
                    objICD9AssociationDBLayer.Dispose()
                    objICD9AssociationDBLayer = Nothing
                    Dim i As Integer
                    For i = 0 To dt.Rows.Count - 1
                        If dt.Rows(i).Item(2) = "c" Then
                            associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ElseIf dt.Rows(i).Item(2) = "d" Then
                            associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ElseIf dt.Rows(i).Item(2) = "p" Then
                            associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ElseIf dt.Rows(i).Item(2) = "t" Then
                            associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
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
                    'trICD9Association.ExpandAll()
                    ''trICD9Association.Select()
                    'selectedTreeview.SelectedNode = dropNode

                    trICD9Association.ExpandAll()
                    trICD9Association.Select()

                    'treeindex = -1
                    'End If

                    'Ensure the newly created node is visible to the user and select it
                    associatenode.EnsureVisible()
                    trICD9Association.SelectedNode = associatenode
                End If
            End If
            txtsearchDrugs.Text = ""
            txtsearchDrugs.Focus()
            'commented from 14/09/2005
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RefreshICD9()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshICD9()
        trICD9Association.Nodes.Item(0).Nodes.Clear()
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        trICD9Association.Nodes.Item(0).Checked = False
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        ''Call FillICD9(dtOrderbyCode, strSortByCode)
        ''txtsearchDrugs.Text = ""
        ''txtsearchDrugs.Focus()
        '' ''trICD9Association.Nodes.Item(0).Nodes.Clear()
        '' ''trICD9.Nodes.Item(0).Nodes.Clear()
        '' ''Dim dt As DataTable
        '' ''dt = objICD9AssociationDBLayer.FillControls(3)
        '' ''Dim i As Integer
        '' ''For i = 0 To dt.Rows.Count - 1
        '' ''    Dim mychildnode As myTreeNode
        '' ''    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
        '' ''    trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
        '' ''Next
        '' ''trICD9.ExpandAll()
        '' ''trICD9.Select()
        UCtrvICD9.Refresh()
    End Sub

    Private Sub SaveAssociation()
        'Get node count of child nodes in trICD9Associates
        Dim oSmartDx As smartDx = New smartDx()
        If (txtName.Text.Trim() = "") Then
            MessageBox.Show("Enter SmartDx name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return
        End If
        If C1SmartDX.Rows.Count > 1 Then
            If IsNothing(objICD9AssociationDBLayer) = True Then
                objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer()
            End If
            Dim i As Integer
            Dim strbuilICDIDs As New System.Text.StringBuilder()
            Dim strbuilICDsName As New System.Text.StringBuilder()
            Dim strICDIDs As String = String.Empty
            Dim strICDsName As String = String.Empty
            For i = 1 To C1SmartDX.Rows.Count - 1
                strbuilICDIDs.Append(",")
                strbuilICDIDs.Append(Convert.ToString(C1SmartDX.GetData(i, 2)))
                strbuilICDsName.Append(",")
                strbuilICDsName.Append(Convert.ToString(C1SmartDX.GetData(i, 0)))
            Next
            If strbuilICDIDs.ToString().Trim().Length > 1 Then
                strICDIDs = strbuilICDIDs.ToString().Substring(1, strbuilICDIDs.ToString().Length - 1)
            End If
            If strbuilICDsName.ToString().Trim().Length > 1 Then
                strICDsName = strbuilICDsName.ToString().Substring(1, strbuilICDsName.ToString().Length - 1)
            End If
            Dim ICD9Node As myTreeNode
            'get the ICD9Node associated sequentially
            ICD9Node = trICD9Association.Nodes.Item(0) ''.Nodes.Item(i)           
            If oSmartDx.IsSmartDxNameExist(ICD9Node.Key, txtName.Text.Trim(), ISCopyICDSmarDx) Then
                MessageBox.Show("Smart Diagnosis with selected name already exists. Enter another name to continue save.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Return
            End If
            If ICD9Node.GetNodeCount(True) > 0 Then
                Dim k As Integer
                Dim arrlist As New ArrayList
                For k = 0 To 7
                    Dim AssociateNode As myTreeNode
                    AssociateNode = ICD9Node.Nodes.Item(k)
                    Dim j As Integer
                    For j = 0 To AssociateNode.GetNodeCount(False) - 1
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes - checkbox - as on 20101011
                        If AssociateNode.Text = "CPT" Then
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "c", AssociateNode.Nodes.Item(j).Checked, 0))
                            'For De-Normalization
                        ElseIf AssociateNode.Text = "Drugs" Then                                                            '\\Added by Suraj on 20081227-for drugaName,Dosage, Drugform
                            'arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).Dosage, CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugForm))
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).NDCCode, CType(AssociateNode.Nodes.Item(j), myTreeNode).mpid, AssociateNode.Nodes.Item(j).Checked))
                            'For De-Normalization
                        ElseIf AssociateNode.Text = "Patient Education" Then
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "p", AssociateNode.Nodes.Item(j).Checked, 0))

                            ''''' Added By Mahesh
                        ElseIf AssociateNode.Text = "Tags" Then
                            ' arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "t", AssociateNode.Nodes.Item(j).Checked, 0))
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "t", AssociateNode.Nodes.Item(j).Checked, 0, CType(AssociateNode.Nodes.Item(j), myTreeNode).Text))
                            ''Added Rahul on 20101013
                        ElseIf AssociateNode.Text = "Flowsheet" Then
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "f", AssociateNode.Nodes.Item(j).Checked, 0))

                        ElseIf AssociateNode.Text = "Orders & Results" Then
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "l", AssociateNode.Nodes.Item(j).Checked, 0))

                        ElseIf AssociateNode.Text = "Order Templates" Then
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "o", AssociateNode.Nodes.Item(j).Checked, 0))

                        ElseIf AssociateNode.Text = "Referral Letter" Then
                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "r", AssociateNode.Nodes.Item(j).Checked, 0))
                            ''
                        End If
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes - checkbox - as on 20101011
                    Next

                Next

                objICD9AssociationDBLayer.AddData(ICD9Node.Key, ICD9Node.Tag, arrlist, strICDIDs, txtName.Text.Trim(), cmbProvider.SelectedValue, ISCopyICDSmarDx, strICDsName)
            End If
            '' Next
            RefreshICD9()
            If (IsNothing(objICD9AssociationDBLayer) = False) Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
            If (IsNothing(oSmartDx) = False) Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If
            strbuilICDIDs = Nothing
            strICDsName = Nothing
        Else
            MessageBox.Show("Select diagnosis code for setup.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return
        End If
        'Shubhangi 20091202
        '   Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub SaveAssociation_old()
        'Get node count of child nodes in trICD9Associates
        If trICD9Association.Nodes.Item(0).GetNodeCount(False) > 0 Then

            Dim i As Integer
            If IsNothing(objICD9AssociationDBLayer) = True Then
                objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer()
            End If
            For i = 0 To trICD9Association.Nodes.Item(0).GetNodeCount(False) - 1
                Dim ICD9Node As myTreeNode
                'get the ICD9Node associated sequentially
                ICD9Node = CType(trICD9Association.Nodes.Item(0).Nodes.Item(i), myTreeNode)
                If ICD9Node.GetNodeCount(True) > 0 Then
                    Dim k As Integer
                    Dim arrlist As New ArrayList
                    For k = 0 To 3
                        Dim AssociateNode As myTreeNode
                        AssociateNode = ICD9Node.Nodes.Item(k)
                        Dim j As Integer
                        For j = 0 To AssociateNode.GetNodeCount(False) - 1
                            If AssociateNode.Text = "CPT" Then
                                arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "c"))

                                'For De-Normalization
                            ElseIf AssociateNode.Text = "Drugs" Then                                                            '\\Added by Suraj on 20081227-for drugaName,Dosage, Drugform
                                'arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).Dosage, CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugForm))
                                arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), gloUserControlLibrary.myTreeNode).ID, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).NDCCode, CType(AssociateNode.Nodes.Item(j), myTreeNode).mpid))
                                'For De-Normalization
                            ElseIf AssociateNode.Text = "Patient Education" Then
                                arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), gloUserControlLibrary.myTreeNode).ID, "p"))

                                ''''' Added By Mahesh
                            ElseIf AssociateNode.Text = "Tags" Then
                                arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), gloUserControlLibrary.myTreeNode).ID, "t"))
                            End If
                        Next

                    Next
                    objICD9AssociationDBLayer.AddData(ICD9Node.Key, ICD9Node.Tag, arrlist)
                End If
            Next
            RefreshICD9()
            If (IsNothing(objICD9AssociationDBLayer) = False) Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End If
    End Sub
    '' ''Private Sub trAssociates_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trAssociates.DragDrop, trICD9Association.DragDrop
    '' ''    Try
    '' ''        'Check that there is a TreeNode being dragged

    '' ''        'commented on 30/08/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
    '' ''        If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

    '' ''        'Get the TreeView raising the event (incase multiple on form)
    '' ''        Dim selectedTreeview As TreeView = CType(sender, TreeView)

    '' ''        'Get the TreeNode being dragged
    '' ''        'commented on 30/08/2005 Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

    '' ''        Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

    '' ''        'The target node should be selected from the DragOver event

    '' ''        'commented on 30/08/2005 Dim targetNode As TreeNode = selectedTreeview.SelectedNode

    '' ''        Dim targetNode1 As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

    '' ''        'Remove the drop node from its current location

    '' ''        'If there is no targetNode add dropNode to the bottom of the TreeView root
    '' ''        'nodes, otherwise add it to the end of the dropNode child nodes
    '' ''        'If targetNode Is Nothing Then
    '' ''        '    dropNode.Remove()
    '' ''        '    selectedTreeview.Nodes.Add(dropNode)
    '' ''        '    AddControl()
    '' ''        '    PopulateMedication(dropNode.Key)

    '' ''        'targetnode is the node selected on which the dropnode is to be dropped.
    '' ''        'If targetNode Is selectedTreeview.Nodes.Item(0) Then

    '' ''        'check if dropnode is node selected from trassociates treeview
    '' ''        If dropNode.TreeView Is trAssociates Then
    '' ''            If trICD9Association.Nodes.Item(0).GetNodeCount(False) > 0 Then
    '' ''                If Not trICD9Association.SelectedNode Is trICD9Association.Nodes.Item(0) Then
    '' ''                    If Not IsNothing(targetNode1) And Not IsNothing(dropNode) Then
    '' ''                        AddAssociates(dropNode, targetNode1)
    '' ''                    End If
    '' ''                End If
    '' ''            End If
    '' ''        End If
    '' ''        'commented from 14/09/2005
    '' ''        'If dropNode.Parent Is trAssociates.Nodes.Item(0) Then
    '' ''        '    Dim targetnode As myTreeNode
    '' ''        '    'check if targetnode is node at second level in trICD9Association treeview
    '' ''        '    If targetNode1.Parent Is trICD9Association.Nodes.Item(0) Or (targetNode1.Key = -1) Then
    '' ''        '        If targetNode1.Parent Is trICD9Association.Nodes.Item(0) Then
    '' ''        '            targetnode = targetNode1
    '' ''        '        Else

    '' ''        '            targetnode = targetNode1.Parent
    '' ''        '        End If

    '' ''        '        Dim str As String
    '' ''        '        str = dropNode.Key
    '' ''        '        Dim mytragetnode As myTreeNode
    '' ''        '        Dim associatenode As myTreeNode

    '' ''        '        associatenode = dropNode.Clone
    '' ''        '        associatenode.Key = dropNode.Key
    '' ''        '        associatenode.Text = dropNode.Text

    '' ''        '        'if selected category is cpt, add node to cpt child node 
    '' ''        '        'in trICD9Associates
    '' ''        '        If btnCPT.Dock = DockStyle.Top Then
    '' ''        '            For Each mytragetnode In targetnode.Nodes.Item(0).Nodes
    '' ''        '                If mytragetnode.Key = str Then
    '' ''        '                    Exit Sub
    '' ''        '                End If
    '' ''        '            Next
    '' ''        '            targetnode.Nodes.Item(0).Nodes.Add(associatenode)
    '' ''        '            'if selected category is Drugs, add node to Drugs child node 
    '' ''        '            'in trICD9Associates
    '' ''        '        ElseIf btnDrugs.Dock = DockStyle.Top Then
    '' ''        '            For Each mytragetnode In targetnode.Nodes.Item(1).Nodes
    '' ''        '                If mytragetnode.Key = str Then
    '' ''        '                    Exit Sub
    '' ''        '                End If
    '' ''        '            Next
    '' ''        '            targetnode.Nodes.Item(1).Nodes.Add(associatenode)

    '' ''        '            'if selected category is PatientEducation, add node to PatientEducation child node 
    '' ''        '            'in trICD9Associates
    '' ''        '        ElseIf btnPatientEducation.Dock = DockStyle.Top Then
    '' ''        '            For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
    '' ''        '                If mytragetnode.Key = str Then
    '' ''        '                    Exit Sub
    '' ''        '                End If
    '' ''        '            Next
    '' ''        '            targetnode.Nodes.Item(2).Nodes.Add(associatenode)
    '' ''        '        End If
    '' ''        '        dropNode.EnsureVisible()
    '' ''        '        selectedTreeview.ExpandAll()
    '' ''        '        selectedTreeview.SelectedNode = dropNode

    '' ''        '    End If
    '' ''        'End If
    '' ''        'commendted from 14/09/2005
    '' ''        'Ensure the newley created node is visible to the user and select it
    '' ''    Catch ex As Exception
    '' ''        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '' ''        MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '' ''    End Try

    '' ''End Sub

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        Try
            'Populate CPT data
            btnCPT.Enabled = False

            If btnCPT.Tag = "UnSelected" Then

                If rbCodesearch.Checked = True Then
                    PopulateAssociates(1, strSortByCode)
                Else
                    PopulateAssociates(1, strSortByDesc)
                End If

                _IsCPT = True
                ' To view radio button when CPT button click
                pnlRightRadioBtnHeader.Visible = True

                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(0)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True
                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnCPT.Enabled = True
        End Try
    End Sub

    Private Sub btnTags_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTags.Click
        Try
            'Populate Tempalate of Type-'Tags' data
            btnTags.Enabled = False

            If btnTags.Tag = "UnSelected" Then

                PopulateAssociates(4)
                _IsCPT = False
                ' When drug is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnTags.SendToBack()


                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(3)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnTags.Enabled = True
        End Try
    End Sub

    Private Sub btnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.Click
        Try
            'Populate Drugs data
            btnDrugs.Enabled = False

            If btnDrugs.Tag = "UnSelected" Then

                PopulateAssociates(0)
                _IsCPT = False
                ' When drug is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnDrugs.SendToBack()


                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(1)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnDrugs.Enabled = True
        End Try
    End Sub

    Private Sub btnPatientEducation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatientEducation.Click
        Try
            btnPatientEducation.Enabled = False

            If btnPatientEducation.Tag = "UnSelected" Then


                'Populate Patient Education data
                PopulateAssociates(2)
                _IsCPT = False
                ' When drug is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False

                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(2)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnPatientEducation.Enabled = True
        End Try
    End Sub

    Private Sub trAssociates_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trAssociates.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trICD9Association_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trICD9Association.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim trvNode As TreeNode
                trvNode = trICD9Association.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trICD9Association.SelectedNode = trvNode
                End If

                If Not IsNothing(trICD9Association.SelectedNode) Then
                    If (trICD9Association.SelectedNode.Level <> 0) Then
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
                    End If
                    If trICD9Association.Nodes.Item(0).Text = trICD9Association.SelectedNode.Text Or trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Or (CType(trICD9Association.SelectedNode, myTreeNode).Key = -1) Then
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = cntICD9Association
                        'treeindex = trPrescriptionDetails.SelectedNode.Index
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If Not trICD9Association.SelectedNode Is trICD9Association.Nodes.Item(0) OrElse Not trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Then

                Dim mychildnode As myTreeNode
                Dim key As Int64 = 0
                mychildnode = CType(trICD9Association.SelectedNode, myTreeNode)
                'objMedicationDBLayer.DeleteMedication(mychildnode.Index) 'delete from collection
                'key = mychildnode.Key
                mychildnode.Remove() 'delete from Medicationdetails treeview

                'Add the deleted node to Medication treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddNode(Optional ByVal oNode As gloUserControlLibrary.myTreeNode = Nothing)

        If oNode IsNot Nothing Then
            Dim str As String
            str = oNode.ID
            Dim oTragetNode As myTreeNode
            For Each oTragetNode In trICD9Association.Nodes.Item(0).Nodes
                If oTragetNode.Key = str Then
                    Exit Sub
                End If
            Next
        End If
        Dim associatenode As New myTreeNode
        trICD9Association.Nodes.Clear()
        associatenode = New myTreeNode("ICD9/10 Association", -1)
        associatenode.ImageIndex = 6
        associatenode.SelectedImageIndex = 6
        associatenode.Key = ICD9SelNodeKey
        trICD9Association.Nodes.Add(associatenode)
        'Add CPT/Drugs/PE to icd9 node
        'trICD9.SelectedNode.Remove()



        'associatenode = oNode.Clone
        ''associatenode.Key = ICD9SelNodeKey
        ''associatenode.Text = ICDSmarDxName
        ''associatenode.ImageIndex = 1
        ''associatenode.SelectedImageIndex = 1

        ''trICD9Association.Nodes.Item(0).Nodes.Add(associatenode)
        Dim MyChild As New myTreeNode
        MyChild.Text = "CPT"
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

        ''Added Rahul on 20101013
        MyChild = New myTreeNode
        MyChild.Text = "Flowsheet"
        MyChild.Key = -1
        MyChild.ImageIndex = 8
        MyChild.SelectedImageIndex = 8
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Orders & Results"
        MyChild.Key = -1
        MyChild.ImageIndex = 9
        MyChild.SelectedImageIndex = 9
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Order Templates"
        MyChild.Key = -1
        MyChild.ImageIndex = 10
        MyChild.SelectedImageIndex = 10
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Referral Letter"
        MyChild.Key = -1
        MyChild.ImageIndex = 11
        MyChild.SelectedImageIndex = 11
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Template"
        MyChild.Key = -1
        MyChild.ImageIndex = 12
        MyChild.SelectedImageIndex = 12
        associatenode.Nodes.Add(MyChild)
        ''
        associatenode.Expand()

        Dim dt As DataTable = Nothing
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer()
        If associatenode.Key <> 0 Then
            dt = objICD9AssociationDBLayer.FetchICD9forUpdate(associatenode.Key)
        End If
        If (IsNothing(objICD9AssociationDBLayer) = False) Then
            objICD9AssociationDBLayer.Dispose()
            objICD9AssociationDBLayer = Nothing
        End If
        Dim i As Integer
        If Not IsNothing(dt) Then
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(1).ToString().Trim <> "" Then ''Checking Null condition of dt.Rows(i).Item(1) for Fixed Bug Id 6115
                    'add cpt items to cpt node in icd9
                    If dt.Rows(i).Item(2) = "c" Then
                        'CPT Description    CPTID

                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        ''associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        tempnode.Text = dt.Rows(i).Item(1) '''''Description
                        tempnode.Key = dt.Rows(i).Item(0) '''''CPT ID
                        associatenode.Nodes.Item(0).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(0).Expand()
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011

                        'add drug items to drug node in icd9
                    ElseIf dt.Rows(i).Item(2) = "d" Then                    'Drugname + Dosage                              DrugID              Drugname        
                        'associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(3), dt.Rows(i).Item(0), CType(dt.Rows(i).Item(1), String)))

                        'For De-Normalization '\\Added by suraj 20090128
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        'tempnode.Key = oNode.Key
                        'SHUBHANGI 20100805 ASSOCIATE THE ID OF DRUG TO THAT NODE AS KEY
                        tempnode.Key = dt.Rows(i).Item(0)
                        tempnode.DrugName = dt.Rows(i).Item(1)
                        tempnode.NDCCode = dt.Rows(i).Item(7)
                        tempnode.mpid = dt.Rows(i).Item(10)
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        'Code Added by Mayuri:20091009
                        'To check whether Drugform is blank                       
                        tempnode.Text = tempnode.DrugName

                        associatenode.Nodes.Item(1).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(1).Expand()

                        'add PE items to PE node in icd9
                    ElseIf dt.Rows(i).Item(2) = "p" Then
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        tempnode.Text = dt.Rows(i).Item(1) '''''Description
                        tempnode.Key = dt.Rows(i).Item(0) '''''PE ID
                        associatenode.Nodes.Item(2).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(2).Expand()

                        ''associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        'add Tags items to Tags node in icd9
                    ElseIf dt.Rows(i).Item(2) = "t" Then
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        Dim tempnode As myTreeNode
                        tempnode = New myTreeNode()
                        tempnode.Checked = dt.Rows(i).Item("Status")
                        tempnode.Text = dt.Rows(i).Item(1) '''''Description
                        tempnode.Key = dt.Rows(i).Item(0) '''''Tag ID
                        Dim strnodename As String = dt.Rows(i).Item(1) ''Description
                        Dim ind As Integer = strnodename.LastIndexOf("-")
                        If ind > -1 Then
                            strnodename = strnodename.Substring(0, ind)
                        End If
                        tempnode.NodeName = strnodename
                        'tempnode.NodeName = dt.Rows(i).Item(1) '''''Description
                        associatenode.Nodes.Item(3).Nodes.Add(tempnode)
                        associatenode.Nodes.Item(3).Expand()

                        ''associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011

                        ''Added Rahul on 20101013
                    ElseIf dt.Rows(i).Item(2) = "f" Then
                        ' associatenode.Nodes.Item(4).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(4).Nodes.Add(cptmynode)
                        associatenode.Nodes.Item(4).Expand()
                    ElseIf dt.Rows(i).Item(2) = "l" Then
                        '  associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(5).Nodes.Add(cptmynode)
                        associatenode.Nodes.Item(5).Expand()
                    ElseIf dt.Rows(i).Item(2) = "o" Then
                        ' associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(6).Nodes.Add(cptmynode)
                        associatenode.Nodes.Item(6).Expand()




                    ElseIf dt.Rows(i).Item(2) = "r" Then
                        ' associatenode.Nodes.Item(7).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(7).Nodes.Add(cptmynode)
                        associatenode.Nodes.Item(7).Expand()
                    ElseIf dt.Rows(i).Item(2) = "tm" Then
                        ' associatenode.Nodes.Item(8).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        Dim cptmynode As New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0))
                        cptmynode.Checked = dt.Rows(i).Item("Status")
                        associatenode.Nodes.Item(8).Nodes.Add(cptmynode)
                        associatenode.Nodes.Item(8).Expand()

                        'add Tags items to Tags node 
                        ''
                    End If

                End If ''End Checking Null condition of dt.Rows(i).Item(1)

            Next
        End If
        'trICD9Association.ExpandAll()
        trICD9Association.Select()
        ' code added for removing template tag from  TRICD9Association 30 january
        associatenode.Nodes.Item(8).Remove()
        'treeindex = -1
        'End If

        'Ensure the newly created node is visible to the user and select it
        associatenode.EnsureVisible()
        trICD9Association.SelectedNode = associatenode
        'treeindex = mynode.Index
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
        CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
    End Sub

    Private Sub txtsearchDrugs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchDrugs.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trICD9.Select()
        Else
            trICD9.SelectedNode = trICD9.Nodes.Item(0)
        End If
        ''--Added by Anil on 20071213
        mdlGeneral.ValidateText(txtsearchDrugs.Text, e)
        ''----
    End Sub
    '\\ added by suraj 20090128 - for allowed '-' char in searchbox 
    Public Sub ValidateTextSearch(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub txtsearchAssociates_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchAssociates.KeyPress
        'If (e.KeyChar = ChrW(13)) Then
        If (e.KeyChar = ChrW(13)) Then
            trAssociates.Select()
        Else
            trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
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

    Private Sub trICD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trICD9.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trICD9.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    'AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnCPT_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnCPT, "CPT List")

        btnCPT.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDrugs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnDrugs, "Drugs List")

        btnDrugs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnPatientEducation_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatientEducation.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnPatientEducation, "Patient Education List")

        btnPatientEducation.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnPatientEducation.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnTags_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTags.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnTags, "Tags List")

        btnTags.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnTags.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    '' ''Private Sub trAssociates_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trAssociates.DoubleClick
    '' ''    'MsgBox(CType(trICD9Association.SelectedNode, myTreeNode).Text)
    '' ''    Try
    '' ''        Dim mynode As myTreeNode
    '' ''        Dim targetnode1 As myTreeNode
    '' ''        targetnode1 = CType(trICD9Association.SelectedNode, myTreeNode)
    '' ''        mynode = CType(trAssociates.SelectedNode, myTreeNode)
    '' ''        'check if selected node is rootnode
    '' ''        If Not IsNothing(targetnode1) And Not IsNothing(mynode) Then
    '' ''            AddAssociates(mynode, targetnode1)
    '' ''        End If
    '' ''    Catch ex As Exception
    '' ''        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '' ''        MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '' ''    End Try
    '' ''End Sub

    '' ''Private Sub trAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trAssociates.KeyPress
    '' ''    If (e.KeyChar = ChrW(13)) Then
    '' ''        Try
    '' ''            Dim mynode As myTreeNode
    '' ''            Dim targetnode1 As myTreeNode
    '' ''            targetnode1 = CType(trICD9Association.SelectedNode, myTreeNode)
    '' ''            mynode = CType(trAssociates.SelectedNode, myTreeNode)
    '' ''            'check if selected node is rootnode
    '' ''            If Not IsNothing(targetnode1) And Not IsNothing(mynode) Then
    '' ''                AddAssociates(mynode, targetnode1)
    '' ''            End If
    '' ''        Catch ex As Exception
    '' ''            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '' ''            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '' ''        End Try
    '' ''    End If
    '' ''End Sub

    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal targetnode1 As myTreeNode)
        If Not mynode Is trAssociates.Nodes.Item(0) Then   'not root node
            Dim targetnode As myTreeNode
            ''If the Node is root node then do not add CPT or Drug 
            ''Added by Anil on 20071220
            'If targetnode1 Is trICD9Association.Nodes.Item(0) Then
            '    Exit Sub
            'End If
            ''
            'check if targetnode is node at second level in trICD9Association treeview
            'If targetnode1.Parent Is trICD9Association.Nodes.Item(0) Or (targetnode1.Key = -1) Then

            If targetnode1 Is trICD9Association.Nodes.Item(0) OrElse (targetnode1.Key = -1) OrElse (CType(targetnode1.Parent, myTreeNode).Key = -1) Then

                If targetnode1 Is trICD9Association.Nodes.Item(0) Then
                    targetnode = targetnode1
                ElseIf (CType(targetnode1.Parent, myTreeNode).Key = -1) Then
                    '' Made ICD9 Node as selected Node which is Grand-Parent of selected node
                    targetnode = targetnode1.Parent.Parent
                Else
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
                associatenode.NodeName = mynode.Text
                '' chetan added  13 nov 2010
                '\\addded by suraj on 20090128
                'For De-Normalization
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

                'For De-Normalization

                'If btnDrugs.Dock = DockStyle.Top Then
                If pnlbtnDrugs.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                    If Not IsNothing(mynode.Tag) Then
                        associatenode.Tag = mynode.Tag
                    End If
                End If
                'if selected category is cpt, add node to cpt child node 
                'in trICD9Associates
                'If btnCPT.Dock = DockStyle.Top Then
                ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                If pnlbtnCPT.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(0).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                        'For DeNormalization
                    Next
                    targetnode.Nodes.Item(0).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(0).ExpandAll()
                    ''  targetnode.Nodes.Item(0).Expand()
                    'if selected category is Drugs, add node to Drugs child node 
                    'in trICD9Associates
                    'ElseIf btnDrugs.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                ElseIf pnlbtnDrugs.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(1).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(1).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(1).ExpandAll()
                    ''  targetnode.Nodes.Item(1).Expand()
                    'if selected category is PatientEducation, add node to PatientEducation child node 
                    'in trICD9Associates
                    'ElseIf btnPatientEducation.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                ElseIf pnlbtnPatientEducation.Dock = DockStyle.Top Or pnlbtnMUPatientEducation.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(2).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(2).ExpandAll()
                    ''targetnode.Nodes.Item(2).Expand()

                    'if selected category is Tags, add node to Tags child node 
                    'in trICD9Associates
                    'ElseIf btnTags.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                ElseIf pnlbtnTags.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(3).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(3).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(3).ExpandAll()
                    ''Added Rahul on 20101013
                ElseIf pnlFlowsheet.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(4).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(4).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(4).ExpandAll()
                    ''targetnode.Nodes.Item(4).Expand()
                ElseIf pnlbtnLabOrders.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(5).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(5).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(5).ExpandAll()
                    '' targetnode.Nodes.Item(5).Expand()
                ElseIf pnlbtnOrders.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(6).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(6).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(6).ExpandAll()

                    '' targetnode.Nodes.Item(6).Expand()
                ElseIf pnlbtnReferrals.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(7).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    ' If targetnode.Nodes.Item(7).Nodes.Count = 0 Then   ' commented by chetan on 16 feb 2010 to allow more items to add in Referral_Letter

                    ' End If
                    targetnode.Nodes.Item(7).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(7).ExpandAll()
                    ''targetnode.Nodes.Item(7).Expand()
                ElseIf pnlbtnTemplate.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(8).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(8).Nodes.Add(associatenode)
                    targetnode.Nodes.Item(8).ExpandAll()
                    '' targetnode.Nodes.Item(8).Expand()
                    ''

                End If
                mynode.EnsureVisible()
                'trICD9Association.ExpandAll()
                trICD9Association.SelectedNode = mynode
                trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub AddAssociates_old(ByVal mynode As myTreeNode, ByVal targetnode1 As myTreeNode)
        If Not mynode Is trAssociates.Nodes.Item(0) Then   'not root node
            Dim targetnode As myTreeNode
            ''If the Node is root node then do not add CPT or Drug 
            ''Added by Anil on 20071220
            If targetnode1 Is trICD9Association.Nodes.Item(0) Then
                Exit Sub
            End If
            ''
            'check if targetnode is node at second level in trICD9Association treeview
            If targetnode1.Parent Is trICD9Association.Nodes.Item(0) Or (targetnode1.Key = -1) Then
                If targetnode1.Parent Is trICD9Association.Nodes.Item(0) Then
                    targetnode = targetnode1
                Else

                    targetnode = targetnode1.Parent
                End If

                Dim str As String
                str = mynode.Key
                Dim mytragetnode As myTreeNode
                Dim associatenode As myTreeNode

                associatenode = mynode.Clone
                associatenode.Key = mynode.Key
                associatenode.Text = mynode.Text

                'If btnDrugs.Dock = DockStyle.Top Then
                If pnlbtnDrugs.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                    If Not IsNothing(mynode.Tag) Then
                        associatenode.Tag = mynode.Tag
                    End If
                End If
                'if selected category is cpt, add node to cpt child node 
                'in trICD9Associates
                'If btnCPT.Dock = DockStyle.Top Then
                ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                If pnlbtnCPT.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(0).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                        'For DeNormalization
                    Next
                    targetnode.Nodes.Item(0).Nodes.Add(associatenode)
                    'if selected category is Drugs, add node to Drugs child node 
                    'in trICD9Associates
                    'ElseIf btnDrugs.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                ElseIf pnlbtnDrugs.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(1).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(1).Nodes.Add(associatenode)

                    'if selected category is PatientEducation, add node to PatientEducation child node 
                    'in trICD9Associates
                    'ElseIf btnPatientEducation.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                ElseIf pnlbtnPatientEducation.Dock = DockStyle.Top Then
                    ''For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
                    ''    'If mytragetnode.Key = str Then
                    ''    'For DeNormalization
                    ''    If CType(mytragetnode.Key, String) = str Then
                    ''        Exit Sub
                    ''    End If
                    ''Next
                    targetnode.Nodes.Item(2).Nodes.Add(associatenode)

                    'if selected category is Tags, add node to Tags child node 
                    'in trICD9Associates
                    'ElseIf btnTags.Dock = DockStyle.Top Then
                    ''COMMENTED BY SUDHIR - 20090202 - ACCORDING TO CHANGED DESIGN.. BUTTONS ARE ON PANEL 
                ElseIf pnlbtnTags.Dock = DockStyle.Top Then
                    For Each mytragetnode In targetnode.Nodes.Item(3).Nodes
                        'If mytragetnode.Key = str Then
                        'For DeNormalization
                        If CType(mytragetnode.Key, String) = str Then
                            Exit Sub
                        End If
                    Next
                    targetnode.Nodes.Item(3).Nodes.Add(associatenode)
                End If
                mynode.EnsureVisible()
                trICD9Association.ExpandAll()
                trICD9Association.SelectedNode = mynode
                trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            End If
        End If
    End Sub

    'Private Sub tblMedication_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&New"
    '                RefreshICD9()
    '            Case "&Save"
    '                SaveAssociation()
    '            Case "&Finish"
    '                SaveAssociation()
    '                Me.Close()
    '            Case "&Close"
    '                Me.Close()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub trAssociates_ContextMenuChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trAssociates.ContextMenuChanged
        txtsearchDrugs.Text = ""
    End Sub

    'Private Sub rbSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbICD9Code.Click
    '    Try
    '        dtOrderbyCode = FillICD9(dtOrderbyCode, strSortByCode)

    '        txtsearchDrugs.Text = ""
    '        txtsearchDrugs.Focus()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    'Private Sub rbSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbICD9Desc.Click
    '    Try
    '        dtOrderbyDesc = FillICD9(dtOrderbyDesc, strSortByDesc)

    '        txtsearchDrugs.Text = ""
    '        txtsearchDrugs.Focus()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub

    Private Sub frmICD9Association_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        txtsearchAssociates.Text = ""
        txtsearchAssociates.Tag = ""
    End Sub

    Private Sub tblMedication_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblMedication.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "New"
                    RefreshICD9()
                Case "Save"
                    SaveAssociation()
                Case "Finish"
                    SaveAssociation()
                    'Me.Close()
                    ' gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    'SLR: Save association, already have close in it.
                Case "Close"
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCPT_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseLeave
        'If btnCPT.Dock = DockStyle.Bottom Then
        '    btnCPT.BackgroundImage = gloEMR.My.Resources.Resources.okcancelbtn
        '    btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        'End If

        If btnCPT.Tag = "Selected" Then
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnDrugs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseLeave
        If btnDrugs.Tag = "Selected" Then
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        End If
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

    Private Sub btnTags_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTags.MouseLeave
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


            PopulateAssociates(1, strSortByCode)
            _IsCPT = True
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub rbDescsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbDescsearch.Click
        Try

            PopulateAssociates(1, strSortByDesc)
            _IsCPT = True
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtsearchAssociates_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearchAssociates.TextChanged
        Try
            '''''''''''''''''''####################''''''''''''''''''''''''''
            '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
            Dim strSearchDetails As String
            If Trim(txtsearchAssociates.Text.Trim) <> "" Then
                strSearchDetails = Replace(txtsearchAssociates.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If
            '''''''''''''''''''####################''''''''''''''''''''''''''

            If _IsCPT = True Then

                FillCPTTreeView(dtCPT, strSearchDetails)
                Exit Sub
                'SLR : 8/5/2014: Code review: What is the purpose of following code : i commented ?
                'If Trim(txtsearchAssociates.Text) <> "" Then
                '    If trAssociates.Nodes.Item(0).GetNodeCount(False) > 0 Then
                '        Dim mychildnode As myTreeNode
                '        Dim str As String
                '        'child node collection
                '        For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
                '            'search against Description
                '            If rbDescsearch.Checked = True Then
                '                If UCase(Mid(mychildnode.Tag, 1, Len(Trim(strSearchDetails)))) = UCase(Trim(strSearchDetails)) Then
                '                    'select matching node
                '                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                '                    trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                '                    '*************
                '                    trAssociates.SelectedNode = mychildnode
                '                    txtsearchAssociates.Focus()
                '                    Exit Sub
                '                End If
                '            Else
                '                'search against ICD9 Code

                '                str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
                '                str = Mid(str, 1, Len(Trim(strSearchDetails)))
                '                If UCase(str) = UCase(Trim(strSearchDetails)) Then
                '                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                '                    trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                '                    '*************
                '                    'select matching node
                '                    trAssociates.SelectedNode = mychildnode
                '                    txtsearchAssociates.Focus()
                '                    Exit Sub
                '                End If
                '            End If
                '        Next

                '        ' '' 20070922 - Mahesh - InString Searching ''  
                '        'child node collection
                '        For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
                '            'search against Description
                '            If rbDescsearch.Checked = True Then
                '                If InStr(UCase(mychildnode.Tag.ToString.Trim), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                '                    'select matching node
                '                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                '                    trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                '                    '*************
                '                    trAssociates.SelectedNode = mychildnode
                '                    txtsearchAssociates.Focus()
                '                    Exit Sub
                '                End If
                '            Else
                '                'search against ICD9 Code

                '                str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))

                '                If InStr(UCase(str), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                '                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                '                    trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                '                    '*************
                '                    'select matching node
                '                    trAssociates.SelectedNode = mychildnode
                '                    txtsearchAssociates.Focus()
                '                    Exit Sub
                '                End If
                '            End If
                '        Next
                '    End If
                'End If

            Else
                ''****************

                'sarika 26th sept 07

                'If btnDrugs.Dock = DockStyle.Top Then
                'AddSearchAssociates(Trim(txtsearchAssociates.Text), dtAssociates, 2)
                If btnTags.Dock = DockStyle.Top Then
                    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 3)
                    Exit Sub
                ElseIf btnPatientEducation.Dock = DockStyle.Top Then
                    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 4)
                    Exit Sub
                End If


                'If Trim(txtsearchAssociates.Text) <> "" Then
                If pnlbtnDrugs.Dock = DockStyle.Top Then
                    If Len(Trim(strSearchDetails)) <= 1 Then
                        If txtsearchAssociates.Tag <> Trim(strSearchDetails) Then

                            PopulateAssociates(0, Trim(strSearchDetails))
                            txtsearchAssociates.Tag = Trim(strSearchDetails)

                        End If
                    End If
                End If
                Dim mychildnode As myTreeNode
                'child node collection
                For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
                    'compare selected node text and entered text
                    Dim str As String
                    str = Mid(UCase(Trim(mychildnode.Text)), 1, Len(UCase(Trim(strSearchDetails))))
                    If str = UCase(Trim(strSearchDetails)) Then
                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                        trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                        '*************
                        trAssociates.SelectedNode = mychildnode
                        txtsearchAssociates.Focus()
                        Exit Sub
                    End If
                Next



                '' 20070922 - Mahesh - InString Searching 
                'child node collection
                For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
                    'compare selected node text and entered text
                    'Dim str As String

                    If InStr(UCase(Trim(mychildnode.Tag)), UCase(Trim(strSearchDetails)), CompareMethod.Text) > 0 Then
                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                        trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                        '*************
                        trAssociates.SelectedNode = mychildnode
                        txtsearchAssociates.Focus()
                        Exit Sub
                    End If
                Next
            End If
            'End If

            '---------------------------------------
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            'bug list sent on saagar email - 15 Jan 2009
            arrstring = Split(strsplittext, ":")
            'bug list sent on saagar email - 15 Jan 2009
            Return arrstring(0)
        Else
            Return ""
        End If
    End Function

    'sarika 26th sept 07
    Public Sub AddSearchAssociates(ByVal strSearch As String, ByVal dt As DataTable, ByVal type As Integer)

        Try
            Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            If btnCPT.Dock = DockStyle.Top Then

                ''cpt
                'If rbCodesearch.Checked = True Then
                '    ''code
                '    dv.RowFilter = "ICD9code Like '%" & strSearch & "%'"
                'Else
                '    ''description 
                '    dv.RowFilter = "sDescription Like '%" & strSearch & "%'"
                'End If

            ElseIf btnDrugs.Dock = DockStyle.Top Then

                'drugs
                'nDrugsID, sDrugNAme , isnull(sDosage,' ') 
                dv.RowFilter = "sDrugNAme Like '%" & strSearch & "%'"

            ElseIf btnTags.Dock = DockStyle.Top Then
                'tags

                'select nTemplateID AS TemplateID , sTemplateName AS TemplateName
                dv.RowFilter = "TemplateName Like '%" & strSearch & "%'"
            ElseIf btnPatientEducation.Dock = DockStyle.Top Then
                'Patient education

                dv.RowFilter = "sTemplateName Like '%" & strSearch & "%'"
            End If


            '   tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trAssociates.BeginUpdate()
            trAssociates.Nodes(0).Nodes.Clear()
            trAssociates.Visible = False

            For i = 0 To tdt.Rows.Count - 1
                'Dim mychildnode As myTreeNode
                If type = 1 Then
                    'ICD9
                    trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1), tdt.Rows(i)(0), CType(tdt.Rows(i)(2), String)))

                ElseIf type = 2 Then
                    'Drugs
                    trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1) & "-" & tdt.Rows(i)(2), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String)))

                Else
                    '3 and 4
                    'tags and PE
                    trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(tdt.Rows(i)(1), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String)))

                End If
                'rootnode.Nodes.Add(dt.Rows(i)(1))
            Next
            trAssociates.Visible = True
            trAssociates.ExpandAll()
            trAssociates.Select()
            trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            txtsearchAssociates.Focus()

        Catch ex As Exception
            'Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            'objex.ErrorMessage = ""
            'Throw objex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trAssociates.EndUpdate()
        End Try
    End Sub
    '-----------------------------------------------------------------------
    Private Sub FillCPTTreeView(ByVal dt As DataTable, ByVal strSearchDetails As String)

        If IsNothing(dt) = False Then
            Dim rootnode As myTreeNode = Nothing
            trAssociates.Nodes.Clear()
            rootnode = New myTreeNode("CPT", -1)
            rootnode.ImageIndex = 2
            rootnode.SelectedImageIndex = 2
            trAssociates.Nodes.Add(rootnode)
            ' ''Populate CPT Data
            '' 0 = nCPTID AS CPTID , 
            '' 1 = sDescription AS sDescription ,
            '' 2 = sCPTCode+'-'+sDescription, 
            '' 3 = sCPTCode AS CPTCode 


            Dim dv As DataView
            dv = dt.DefaultView
            If rbCodesearch.Checked = True Then
                '' Filter on ICD9 Code
                dv.RowFilter = dv.Table.Columns("CPTCode").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
            ElseIf rbDescsearch.Checked = True Then
                '' Filter on ICD9 Desc
                dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
            End If

            Dim dt1 As DataTable
            dt1 = dv.ToTable

            Dim i As Integer

            If dt1.Rows.Count < 400 Then
                CPTCount = dt1.Rows.Count - 1
            Else
                CPTCount = 400
            End If

            For i = 0 To CPTCount  'dt1.Rows.Count - 1
                'Dim mychildnode As myTreeNode
                'mychildnode = New myTreeNode(dt1.Rows(i)(1), dt1.Rows(i)(0), CType(dt1.Rows(i)(2), String))
                trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(dt1.Rows(i)(2), dt1.Rows(i)("CPTID"), CType(dt1.Rows(i)("sDescription"), String)))
                'trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
            Next

            If trAssociates.Nodes(0).GetNodeCount(False) > 0 Then
                trAssociates.SelectedNode = trAssociates.Nodes(0).Nodes(0)
                trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                trAssociates.SelectedNode = trAssociates.Nodes(0).Nodes(0)
                '*************
                ''select matching node
                'trICD9.SelectedNode = mychildnode
                txtsearchAssociates.Focus()
            End If

        End If
        trAssociates.ExpandAll()
        trAssociates.Show()
        trAssociates.Select()
        txtsearchAssociates.Focus()

    End Sub

    Private Sub FillICD9TreeView(ByVal dt As DataTable, ByVal strSearchDetails As String)

        If IsNothing(dt) = False Then
            trICD9.Hide()
            trICD9.Nodes.Clear()
            Dim rootnode As TreeNode
            rootnode = New myTreeNode("ICD9", -1)
            rootnode.ImageIndex = 1
            rootnode.SelectedImageIndex = 1
            trICD9.Nodes.Add(rootnode)

            ' ''Populate ICD9 Data
            '' 0 = nICD9ID ,
            '' 1 = sICD9code+'-'+sDescription, 
            '' 2 = sDescription AS sDescription, 
            '' 3 = sICD9code AS ICD9code    

            Dim dv As DataView
            dv = dt.DefaultView
            If rbICD9Code.Checked = True Then
                '' Filter on ICD9 Code
                dv.RowFilter = dv.Table.Columns("ICD9code").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
            Else
                '' Filter on ICD9 Desc
                dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
            End If

            Dim dt1 As DataTable
            dt1 = dv.ToTable

            Dim i As Integer

            If dt1.Rows.Count < 400 Then
                ICD9Count = dt1.Rows.Count - 1
            Else
                ICD9Count = 400
            End If

            For i = 0 To ICD9Count 'dt1.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dt1.Rows(i)(1), dt1.Rows(i)(0), CType(dt1.Rows(i)(2), String))
                trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
            Next

            If trICD9.Nodes(0).GetNodeCount(False) > 0 Then
                trICD9.SelectedNode = trICD9.Nodes(0).Nodes(0)
                trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                trICD9.SelectedNode = trICD9.Nodes(0).Nodes(0)
                '*************
                ''select matching node
                'trICD9.SelectedNode = mychildnode
                txtsearchDrugs.Focus()
            End If

        End If
        trICD9.ExpandAll()
        trICD9.Show()
        trICD9.Select()


    End Sub

    '''''Following code lines are addded by Anil 0n 27/09/07 at 10:45 a.m.
    '''''This code clears the search textboxes, gets the focus on the root of the TreeViews  on click of Refresh button.

    Private Sub tblRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblRefresh.Click
        txtsearchDrugs.Focus()
        txtsearchAssociates.Clear()
        txtsearchDrugs.Clear()
        trAssociates.CollapseAll()
        trAssociates.ExpandAll()
        trICD9Association.CollapseAll()
        trICD9Association.ExpandAll()
        'trICD9.CollapseAll()
        'trICD9.Focus()
        'trICD9.ExpandAll()
        UCtrvICD9.Refresh()
        rbICD9Desc.Checked = False
        rbICD9Desc.Checked = True
        rbCodesearch.Checked = False
        rbDescsearch.Checked = True
        '''''upto here the code is added
    End Sub

    '***************Ojeswini_08Jan09*****************************
    Private Sub rbICD9Code_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD9Code.CheckedChanged
        If rbICD9Code.Checked = True Then
            rbICD9Code.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbICD9Code.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbICD9Desc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbICD9Desc.CheckedChanged
        If rbICD9Desc.Checked = True Then
            rbICD9Desc.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbICD9Desc.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbDescsearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDescsearch.CheckedChanged
        If rbDescsearch.Checked = True Then
            rbDescsearch.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbDescsearch.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbCodesearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCodesearch.CheckedChanged
        If rbCodesearch.Checked = True Then
            rbCodesearch.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbCodesearch.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub UCtrvICD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UCtrvICD9.KeyPress
        If e.KeyChar = Chr(13) Then
            Try
                Dim mynode As gloUserControlLibrary.myTreeNode = CType(UCtrvICD9.SelectedNode, gloUserControlLibrary.myTreeNode)
                If Not IsNothing(mynode) Then
                    ''  AddNode(oNode)
                    If (C1SmartDX.Rows.Count = 0 Or C1SmartDX.FindRow(mynode.Code, 1, 0, True) <= 0) Then
                        C1SmartDX.BeginUpdate()
                        C1SmartDX.Rows.Add()
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 0, mynode.Code)
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 1, mynode.Description)
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 2, mynode.ID)
                        C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 3, gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                        C1SmartDX.EndUpdate()
                        If txtName.Text.Trim() = "" Then
                            txtName.Text = mynode.Description
                        End If
                    End If
                    UCtrvICD9.Focus()
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub UCtrvICD9_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles UCtrvICD9.NodeAdded

        'Try
        '    If _isFormLoading = False Then
        '        Dim dtAssociation As DataTable
        '        '' To Get Already Associated Template with Selected CPT
        '        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        '        dtAssociation = objICD9AssociationDBLayer.FetchICD9forUpdate(0) ''ChildNode.Tag
        '        objICD9AssociationDBLayer.Dispose()
        '        objICD9AssociationDBLayer = Nothing
        '        '' If Association found then change the Image of Treenode 
        '        If Not IsNothing(dtAssociation) Then
        '            If dtAssociation.Rows.Count > 0 Then
        '                ChildNode.ImageIndex = 6
        '                ChildNode.SelectedImageIndex = 6
        '            Else
        '                ChildNode.ImageIndex = 0
        '                ChildNode.SelectedImageIndex = 0

        '            End If

        '        End If
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub
    Private Sub ChangeIconForAssoication()
        Dim dtAssociation As DataTable
        Dim _drRowFilter As DataRow() = Nothing
        Try

            If rbtAll.Checked Then
                '' To Get Already Associated Template with Selected CPT
                objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
                dtAssociation = objICD9AssociationDBLayer.FetchICD9forUpdate(0)
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
                '' If Association found then change the Image of Treenode 

                If Not IsNothing(dtAssociation) Then
                    If dtAssociation.Rows.Count > 0 Then
                        Dim nICD9IDCol As String = Nothing
                        Dim nICD9ID As String = Nothing
                        Dim nCodeImageIndex As Integer = 0
                        For Each ElementNode As gloUserControlLibrary.myTreeNode In UCtrvICD9.Nodes
                            nICD9ID = ElementNode.ID

                            nCodeImageIndex = 6


                            _drRowFilter = dtAssociation.[Select]("nICD9ID" + "='" + nICD9ID + "'")

                            If _drRowFilter.Length > 0 Then
                                ElementNode.ImageIndex = 6
                                ElementNode.SelectedImageIndex = 6
                            Else
                                ElementNode.ImageIndex = 0
                                ElementNode.SelectedImageIndex = 0
                            End If
                        Next
                    End If
                End If
            ElseIf rbtAssociated.Checked Then
                UCtrvICD9.ImageIndex = 6
                UCtrvICD9.SelectedImageIndex = 6
            ElseIf rbtUnassociated.Checked Then
                UCtrvICD9.ImageIndex = 0
                UCtrvICD9.SelectedImageIndex = 0
            End If


        Catch ex As Exception
        Finally
            If _drRowFilter IsNot Nothing Then
                _drRowFilter = Nothing
            End If
        End Try
        'If Not IsNothing(dtAssociation) Then
        '    If dtAssociation.Rows.Count > 0 Then
        '        ChildNode.ImageIndex = 6
        '        ChildNode.SelectedImageIndex = 6
        '    Else
        '        ChildNode.ImageIndex = 0
        '        ChildNode.SelectedImageIndex = 0

        '    End If

        'End If
    End Sub
    '************************************************************
    Private Sub UCtrvICD9_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles UCtrvICD9.NodeMouseDoubleClick
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            If Not IsNothing(oNode) Then
                ''  AddNode(oNode)
                If (C1SmartDX.Rows.Count = 0 Or C1SmartDX.FindRow(oNode.Code, 1, 0, True) <= 0) Then
                    C1SmartDX.BeginUpdate()
                    C1SmartDX.Rows.Add()
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 0, oNode.Code)
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 1, oNode.Description)
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 2, oNode.ID)
                    C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 3, gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode())
                    C1SmartDX.EndUpdate()
                    If txtName.Text.Trim() = "" Then
                        txtName.Text = oNode.Description
                    End If
                End If

            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub UCtrvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles UCtrvAssociates.NodeMouseDoubleClick
        Try
            Dim targetnode1 As myTreeNode = CType(trICD9Association.SelectedNode, myTreeNode)

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

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

            ''oNodeToAdd.Checked =oNode.
            'check if selected node is rootnode
            If Not IsNothing(targetnode1) AndAlso Not IsNothing(oNode) Then
                If Not IsNothing(oNodeToAdd) Then
                    AddAssociates(oNodeToAdd, targetnode1)
                    'Shubhangi 20091208
                    'Check the setting Reset search text box after assiging category
                    If gblnResetSearchTextBox = True Then
                        UCtrvAssociates.txtsearch.ResetText()

                    End If
                End If
            End If
            If Not IsNothing(oNodeToAdd) Then
                oNodeToAdd.Dispose()
                oNodeToAdd = Nothing
            End If

            ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
            CheckAllParentNodes()
            ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UCtrvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UCtrvAssociates.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                Dim targetnode1 As myTreeNode = CType(trICD9Association.SelectedNode, myTreeNode)

                Dim oNode As gloUserControlLibrary.myTreeNode = CType(UCtrvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode)
                If IsNothing(oNode) Then
                    Exit Sub
                End If
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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trICD9Association_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trICD9Association.MouseDoubleClick

    End Sub
    'Shubhangi 20091211
    'Radio button to display only Associated ICD9 list
    Private Sub rbtAssociated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAssociated.Click
        FillAssociatedICD9ICD10()
    End Sub
    Private Sub FillAssociatedICD9ICD10()
        Dim dtAssociation As DataTable = Nothing
        '' To Get Already Associated Template with Selected CPT
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try


            Select Case eCurrentSelectedButton
                Case DockingTags.ICD9Button
                    If rbtAssociated.Checked Then
                        dtAssociation = objICD9AssociationDBLayer.FetchAssociatedICD9ICD10(gloGlobal.gloICD.CodeRevision.ICD9, UCtrvICD9.txtsearch.Text, 1)
                    ElseIf rbtUnassociated.Checked Then
                        dtAssociation = objICD9AssociationDBLayer.FetchAssociatedICD9ICD10(gloGlobal.gloICD.CodeRevision.ICD9, UCtrvICD9.txtsearch.Text, 0)
                    End If

                Case DockingTags.ICD10Button
                    If rbtAssociated.Checked Then
                        dtAssociation = objICD9AssociationDBLayer.FetchAssociatedICD10(String.Empty, 1)
                    ElseIf rbtUnassociated.Checked Then
                        dtAssociation = objICD9AssociationDBLayer.FetchAssociatedICD10(String.Empty, 2)
                    End If

            End Select


            Select Case eCurrentSelectedButton
                Case DockingTags.ICD9Button
                    UCtrvICD9.DataSource = dtAssociation
                    UCtrvICD9.ValueMember = dtAssociation.Columns("nICD9ID").ColumnName
                    UCtrvICD9.Tag = dtAssociation.Columns(0).ColumnName
                    UCtrvICD9.DescriptionMember = dtAssociation.Columns("sDescription").ColumnName
                    UCtrvICD9.CodeMember = dtAssociation.Columns("ICD9Code").ColumnName
                    UCtrvICD9.IsDiagnosisSearch = True
                    UCtrvICD9.FillTreeView()
                    ChangeIconForAssoication()
                Case DockingTags.ICD10Button
                    wpfICD10UserControl.btnClearSearch_Click(Me, New System.Windows.RoutedEventArgs())
                    wpfICD10UserControl.BindTreeNodes(dtAssociation)
            End Select



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowGraph, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(dtAssociation) Then
            '    dtAssociation.Dispose()
            '    dtAssociation = Nothing
            'End If
            If Not IsNothing(objICD9AssociationDBLayer) Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End Try
    End Sub
    'Shubhangi 20091211
    'Radio button to display only UnAssociated ICD9 list
    Private Sub rbtUnassociated_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtUnassociated.Click
        FillAssociatedICD9ICD10()
        'Dim dtAssociation As New DataTable
        ' '' To Get Already Associated Template with Selected CPT
        'objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        'Try
        '    Select Case eCurrentSelectedButton
        '        Case DockingTags.ICD9Button
        '            dtAssociation = objICD9AssociationDBLayer.FetchAssociatedICD9ICD10(gloGlobal.gloICD.CodeRevision.ICD9, UCtrvICD9.txtsearch.Text)
        '        Case DockingTags.ICD10Button
        '            dtAssociation = objICD9AssociationDBLayer.FetchAssociatedICD9ICD10(gloGlobal.gloICD.CodeRevision.ICD10, UCtrvICD9.txtsearch.Text)
        '    End Select
        '    Dim strAssociated As String = ""
        '    Dim dv As New DataView
        '    dv = dtAssociation.DefaultView
        '    '---
        '    'If UCtrvAssociates.txtsearch.Text.Trim() <> "" Then
        '    '    'Sanjog- Change Column name on 2011 Jan 25 to filter DT
        '    '    'strAssociated = "isCPTassociated = 'false' And isDRUGassociated= 'false' And isTagGassociated='false' And isPatientEducationGassociated='false' AND isReferralLetterGassociated = 'false' AND isOrdersGassociated = 'false' AND isLabOrderGassociated = 'false' AND isFlowsheetGassociated = 'false' And sICD9Code LIKE '" & UCtrvAssociates.txtsearch.Text.Trim() & "%'"
        '    '    strAssociated = "isCPTassociated = 'false' And isDRUGassociated= 'false' And isTagGassociated='false' And isPatientEducationGassociated='false' AND isReferralLetterGassociated = 'false' AND isOrdersGassociated = 'false' AND isLabOrderGassociated = 'false' AND isFlowsheetGassociated = 'false' And sICD9 LIKE '" & UCtrvAssociates.txtsearch.Text.Trim() & "%'"
        '    '    'Sanjog-
        '    'Else
        '    strAssociated = "isCPTassociated = 'false' And isDRUGassociated= 'false' And isTagGassociated='false' And isPatientEducationGassociated='false' AND isReferralLetterGassociated = 'false' AND isOrdersGassociated = 'false' AND isLabOrderGassociated = 'false' AND isFlowsheetGassociated = 'false'"
        '    'End If
        '    '---
        '    'strAssociated = "isCPTassociated = 'false' And isDRUGassociated= 'false' And isTagGassociated='false' And isPatientEducationGassociated='false'"
        '    If dtAssociation.Rows.Count > 0 Then
        '        dv.RowFilter = strAssociated
        '    End If
        '    Dim dt As New DataTable
        '    dt = dv.ToTable
        '    'Dim i As Integer


        '    UCtrvICD9.DataSource = dt
        '    UCtrvICD9.ValueMember = dt.Columns("nICD9ID").ColumnName
        '    UCtrvICD9.Tag = dt.Columns("nICD9ID").ColumnName
        '    UCtrvICD9.DescriptionMember = dt.Columns("sDescription").ColumnName
        '    UCtrvICD9.CodeMember = dt.Columns("ICD9Code").ColumnName
        '    UCtrvICD9.IsDiagnosisSearch = True
        '    UCtrvICD9.FillTreeView()
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowGraph, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'Finally
        '    If Not IsNothing(dtAssociation) Then
        '        dtAssociation.Dispose()
        '        dtAssociation = Nothing
        '    End If
        '    If Not IsNothing(objICD9AssociationDBLayer) Then
        '        objICD9AssociationDBLayer.Dispose()
        '        objICD9AssociationDBLayer = Nothing
        '    End If
        'End Try
    End Sub
    'Shubhangi 20091211
    'Radio button to display All ICD9 list
    Private Sub rbtAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtAll.Click
        Dim dt As DataTable = Nothing
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try

            If eCurrentSelectedButton = DockingTags.ICD9Button Then
                dt = objICD9AssociationDBLayer.FillControls(3, UCtrvICD9.txtsearch.Text)

                UCtrvICD9.DataSource = dt
                UCtrvICD9.ValueMember = dt.Columns("nICD9ID").ColumnName
                UCtrvICD9.Tag = dt.Columns(0).ColumnName
                UCtrvICD9.DescriptionMember = dt.Columns("sDescription").ColumnName
                UCtrvICD9.CodeMember = dt.Columns("sICD9Code").ColumnName
                UCtrvICD9.IsDiagnosisSearch = True
                UCtrvICD9.FillTreeView()
                ChangeIconForAssoication()

            ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then
                wpfICD10UserControl.btnClearSearch_Click(Me, New System.Windows.RoutedEventArgs())
                dt = objICD9AssociationDBLayer.FetchAssociatedICD10(String.Empty, 0)
                wpfICD10UserControl.BindTreeNodes(dt)
            End If

            '' 0 = nICD9ID ,
            '' 1 = sICD9code+'-'+sDescription, 
            '' 2 = sDescription AS sDescription, 
            '' 3 = sICD9code AS ICD9code    

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowGraph, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(objICD9AssociationDBLayer) Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End Try
    End Sub
    'Ojeswini 20100416
    Private Sub rbtAssociated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAssociated.CheckedChanged

        If rbtAssociated.Checked = True Then
            rbtAssociated.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
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

    Private Sub trICD9Association_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trICD9Association.Click

    End Sub

    Private Sub trICD9Association_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trICD9Association.AfterCheck
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        If bChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked)
        End If
        If bParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked)
        End If
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub

    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
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
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub

    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        For Each ctn As TreeNode In tn.Nodes
            ctn.Checked = bCheck
            CheckAllChildren(ctn, bCheck)
        Next
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub

    Private Sub CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        Dim innerchildflag As Boolean = False
        Dim outerchildflag As Boolean = False
        Dim parentflag As Boolean = False

        For Each ptn As TreeNode In trICD9Association.Nodes.Item(0).Nodes
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
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub

    Private Sub trICD9Association_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trICD9Association.NodeMouseClick
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        Dim nodeid As Integer = -1

        Select Case e.Node.Text

            Case "Drugs"
                If btnDrugs.Tag = "UnSelected" Then
                    nodeid = 0
                End If

            Case "CPT"

                If btnCPT.Tag = "UnSelected" Then
                    nodeid = 1
                End If


            Case "Patient Education"

                If btnPatientEducation.Tag = "UnSelected" Then
                    nodeid = 2
                End If

            Case "Tags"

                If btnTags.Tag = "UnSelected" Then
                    nodeid = 4
                End If

            Case "Referral Letter"

                If btnReferrals.Tag = "UnSelected" Then
                    nodeid = 10
                End If

            Case "Flowsheet"

                If btnFlowsheet.Tag = "UnSelected" Then
                    nodeid = 11
                End If

            Case "Order Templates"
                If btnOrders.Tag = "UnSelected" Then
                    nodeid = 12
                End If

            Case "Orders & Results"
                If btnLabOrders.Tag = "UnSelected" Then
                    nodeid = 14
                End If
        End Select

        If nodeid <> -1 Then
            PopulateAssociates(nodeid)
        End If
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
    End Sub
#Region "Added Rahul on 20101013"
    Private Sub btnFlowsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlowsheet.Click
        Try

            btnFlowsheet.Enabled = False

            If btnFlowsheet.Tag = "UnSelected" Then


                'Populate Flowsheet mst
                PopulateAssociates(11)
                _IsCPT = False
                ' When Flowsheet is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnDrugs.SendToBack()


                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(4)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""
                UCtrvAssociates.txtsearch.Text = ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnFlowsheet.Enabled = True
        End Try
    End Sub

    Private Sub btnFlowsheet_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlowsheet.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnFlowsheet, "Flow Sheet List")

        btnFlowsheet.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnFlowsheet_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlowsheet.MouseLeave
        If btnFlowsheet.Tag = "Selected" Then
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnLabOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabOrders.Click
        Try

            btnLabOrders.Enabled = False

            If btnLabOrders.Tag = "UnSelected" Then

                'Template
                PopulateAssociates(14)
                _IsCPT = False
                ' When Order is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnDrugs.SendToBack()


                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(5)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""
                UCtrvAssociates.txtsearch.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnLabOrders.Enabled = True
        End Try
    End Sub

    Private Sub btnLabOrders_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabOrders.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnLabOrders, "Orders & Results List")

        btnLabOrders.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnLabOrders_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabOrders.MouseLeave
        If btnLabOrders.Tag = "Selected" Then
            btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLabOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnLabOrders.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.Click
        Try
            btnOrders.Enabled = False

            If btnOrders.Tag = "UnSelected" Then

                'Populate Orders (Radiology Orders)
                PopulateAssociates(12)
                _IsCPT = False
                ' When Order is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnDrugs.SendToBack()


                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(6)
                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""
                UCtrvAssociates.txtsearch.Text = ""

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnOrders.Enabled = True
        End Try
    End Sub

    Private Sub btnOrders_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnOrders, "Orders List")

        btnOrders.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnOrders.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnOrders_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrders.MouseLeave
        If btnOrders.Tag = "Selected" Then
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnOrders.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnReferrals_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.Click
        Try
            btnReferrals.Enabled = False

            If btnReferrals.Tag = "UnSelected" Then


                'Populate Refferal Letter
                PopulateAssociates(10)
                _IsCPT = False
                ' When Refferal Letter is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnDrugs.SendToBack()

                trICD9Association.SelectedNode = trICD9Association.Nodes(0).Nodes(7)


                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""
                UCtrvAssociates.txtsearch.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnReferrals.Enabled = True
        End Try
    End Sub

    Private Sub btnReferrals_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnReferrals, "Referral Letter List")
        btnReferrals.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnReferrals_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferrals.MouseLeave
        If btnReferrals.Tag = "Selected" Then
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnReferrals.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnReferrals.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemplate.Click
        Try
            btnTemplate.Enabled = False

            If btnTemplate.Tag = "UnSelected" Then


                'Template
                PopulateAssociates(13)
                _IsCPT = False
                ' When Order is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False
                pnlbtnDrugs.SendToBack()



                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""
                UCtrvAssociates.txtsearch.Text = ""

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnTemplate.Enabled = True
        End Try
    End Sub

    Private Sub btnTemplate_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemplate.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnTemplate, "Template List")
        btnTemplate.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnTemplate.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnTemplate_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemplate.MouseLeave
        If btnTemplate.Tag = "Selected" Then
            btnTemplate.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnTemplate.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnTemplate.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnTemplate.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub
#End Region

    ''Sanjog- Added on 2011 Jan 17 to show Context Menu on Property key Press  
    Private Sub trICD9Association_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trICD9Association.KeyDown
        Try
            If e.KeyCode = Keys.Apps Then
                If Not IsNothing(trICD9Association.SelectedNode) Then
                    If (trICD9Association.SelectedNode.Level <> 0) Then
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
                    End If
                    If trICD9Association.Nodes.Item(0).Text = trICD9Association.SelectedNode.Text Or trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Or (CType(trICD9Association.SelectedNode, myTreeNode).Key = -1) Then
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trICD9Association.ContextMenu) = False) Then
                        '        trICD9Association.ContextMenu.Dispose()
                        '        trICD9Association.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trICD9Association.ContextMenu = cntICD9Association
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Sanjog- Added on 2011 Jan 17 to show Context Menu on Property key Press  
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("SmartDx", "Download")
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

    Private Sub UCtrvICD9_SearchFired() Handles UCtrvICD9.SearchFired
        If rbtAssociated.Checked Or rbtUnassociated.Checked Then
            FillAssociatedICD9ICD10()
        Else
            FillICD9ICD10()
        End If

    End Sub

    Private Sub btnICD10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD10.Click
        rbtAll.Checked = True
        Panel2.Dock = DockStyle.Bottom
        Panel3.Dock = DockStyle.Bottom
        eCurrentSelectedButton = DockingTags.ICD10Button
        btnICD10.Tag = "UnSelected"
        btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnICD10.BackgroundImageLayout = ImageLayout.Stretch


        btnICD9.Tag = "UnSelected"
        btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        '' btnICD10.Dock = DockStyle.Top
        btnICD10.Tag = "Selected"
        btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnICD10.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Dock = DockStyle.Top
        UCtrvICD9.txtsearch.Text = ""

        If wpfICD10UserControl Is Nothing Then
            wpfICD10UserControl = New gloUIControlLibrary.ICDSubCodeControl()

            If elementHostICD10 IsNot Nothing Then
                elementHostICD10.Child = wpfICD10UserControl
            End If

            wpfICD10UserControl.ShowRightArrowImage()

            AddHandler wpfICD10UserControl.CodeAddedToCurrent, AddressOf ICD10CodeAdded
            AddHandler wpfICD10UserControl.SearchFired, AddressOf ICD10SearchFired
        End If
        FillICD9ICD10()
        tlsCodingRules.Visible = True
    End Sub

    Private Sub btnICD10_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD10.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnICD10, "ICD10")

        btnICD10.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
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

    Private Sub btnICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.Click
        rbtAll.Checked = True
        ''btnICD9.Dock = DockStyle.Top
        Panel2.Dock = DockStyle.Bottom
        Panel3.Dock = DockStyle.Bottom
        btnICD9.Tag = "UnSelected"
        btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch

        btnICD10.Tag = "UnSelected"
        btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnICD10.BackgroundImageLayout = ImageLayout.Stretch
        eCurrentSelectedButton = DockingTags.ICD9Button


        btnICD9.Tag = "Selected"
        btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        Panel3.Dock = DockStyle.Top
        UCtrvICD9.txtsearch.Text = ""
        FillICD9ICD10()
        tlsCodingRules.Visible = False
    End Sub

    Private Sub btnICD9_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.MouseHover
        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnICD9, "ICD9")

        btnICD9.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnICD9.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnICD9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnICD9.MouseLeave
        If btnICD9.Tag = "Selected" Then
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnICD9.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub
    Public Sub FillICD9ICD10()




        Dim dt As DataTable = Nothing
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try
            If eCurrentSelectedButton = DockingTags.ICD9Button Then
                elementHostICD10.Visible = False
                elementHostICD10.SendToBack()

                UCtrvICD9.Visible = True
                UCtrvICD9.BringToFront()

                dt = objICD9AssociationDBLayer.FillControls(3, UCtrvICD9.txtsearch.Text)

                ' 0 = nICD9ID ,
                ' 1 = sICD9code+'-'+sDescription, 
                ' 2 = sDescription AS sDescription, 
                ' 3 = sICD9code AS ICD9code    
                UCtrvICD9.DataSource = dt
                UCtrvICD9.ValueMember = dt.Columns("nICD9ID").ColumnName
                UCtrvICD9.Tag = dt.Columns(0).ColumnName
                UCtrvICD9.DescriptionMember = dt.Columns("sDescription").ColumnName
                UCtrvICD9.CodeMember = dt.Columns("sICD9Code").ColumnName
                ' UCtrvICD9.ICDRevision = dt.Columns("nICDRevision").ColumnName
                If rbtAll.Checked Then
                    UCtrvICD9.IsDiagnosisSearch = True
                Else
                    UCtrvICD9.IsDiagnosisSearch = False
                End If

                UCtrvICD9.FillTreeView()
                ChangeIconForAssoication()

            ElseIf eCurrentSelectedButton = DockingTags.ICD10Button Then
                UCtrvICD9.Visible = False
                wpfICD10UserControl.btnClearSearch_Click(Me, New System.Windows.RoutedEventArgs())
                elementHostICD10.Visible = True
                elementHostICD10.BringToFront()

                objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
                dt = objICD9AssociationDBLayer.FetchAssociatedICD10(String.Empty, 0)

                wpfICD10UserControl.BindTreeNodes(dt)
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ShowGraph, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(objICD9AssociationDBLayer) Then
                objICD9AssociationDBLayer.Dispose()
                objICD9AssociationDBLayer = Nothing
            End If
        End Try
    End Sub

    Private Sub btnMUPatientEducation_Click(sender As System.Object, e As System.EventArgs) Handles btnMUPatientEducation.Click
        Try
            btnMUPatientEducation.Enabled = False

            If btnMUPatientEducation.Tag = "UnSelected" Then

                'Populate Patient Education data
                PopulateAssociates(16)
                _IsCPT = False
                ' When drug is click then radio button is visible
                pnlRightRadioBtnHeader.Visible = False


                rbCodesearch.Checked = False
                rbDescsearch.Checked = True

                txtsearchAssociates.Text = ""
                txtsearchAssociates.Tag = ""

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnMUPatientEducation.Enabled = True
        End Try
    End Sub
    Private Sub Fill_Provider()
        Try

            Dim dt As DataTable
            Dim oProvider As gloAppointmentBook.Books.Resource = New gloAppointmentBook.Books.Resource(GetConnectionString())
            dt = oProvider.GetProviders()

            If Not dt Is Nothing Then
                Dim dr As DataRow = dt.NewRow()
                dr("nProviderID") = 0
                dr("ProviderName") = "All"
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()

                cmbProvider.DataSource = dt.Copy()
                cmbProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbProvider.DisplayMember = dt.Columns("ProviderName").ColumnName
                cmbProvider.Refresh()

                dt.Dispose()
                dt = Nothing
            End If

            oProvider.Dispose()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
    Public Sub fillDianosisFromExam(ByVal sICDCode As String, ByVal sICDDescription As String, ByVal nICDID As Int64, ByVal nICDRevesion As Integer)
        Try
            C1SmartDX.ExtendLastCol = True
            C1SmartDX.Rows.Add()
            C1SmartDX.Rows.Fixed = 1
            C1SmartDX.Cols.Count = 4
            C1SmartDX.SetData(0, 0, "Code")
            C1SmartDX.SetData(0, 1, "Description")
            C1SmartDX.SetData(0, 2, "ICD9ID")
            C1SmartDX.SetData(0, 3, "nICDRevision")
            C1SmartDX.Cols(2).Width = 0
            C1SmartDX.Cols(3).Width = 0
            C1SmartDX.Cols(2).Visible = False
            C1SmartDX.Cols(3).Visible = False
            C1SmartDX.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1SmartDX.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1SmartDX.BeginUpdate()
            C1SmartDX.Rows.Add()
            C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 0, sICDCode.Trim())
            C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 1, sICDDescription.Trim())
            C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 2, nICDID)
            C1SmartDX.SetData(C1SmartDX.Rows.Count - 1, 3, nICDRevesion)
            C1SmartDX.EndUpdate()
            If C1SmartDX.RowSel > 0 Then
                If (Convert.ToInt16(C1SmartDX.GetData(C1SmartDX.RowSel, 3)) = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()) Then
                    tlsCodingRules.Visible = True
                Else
                    tlsCodingRules.Visible = False
                End If
            End If
            For i As Int16 = 0 To 3
                C1SmartDX.Cols(i).AllowEditing = False
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub FillDiagnosis()
        Dim dt As DataTable = Nothing
        objICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
        Try
            ''C1SmartDX.Rows.Add()
            ''C1SmartDX.Cols.Count = 2
            If (C1SmartDX Is Nothing OrElse C1SmartDX.Rows.Count <= 0 OrElse C1SmartDX.Cols.Count <= 0) Then
                If ICD9SelNodeKey > 0 Then
                    dt = objICD9AssociationDBLayer.FetchAssociatedICD9forSmartDx(ICD9SelNodeKey)
                End If

                If dt IsNot Nothing Then
                    'C1SmartDX.Clear()
                    C1SmartDX.DataSource = Nothing
                    C1SmartDX.BeginUpdate()
                    C1SmartDX.DataSource = dt.Copy().DefaultView
                    C1SmartDX.EndUpdate()
                    C1SmartDX.ExtendLastCol = True
                    C1SmartDX.Rows.Fixed = 1
                    C1SmartDX.SetData(0, 0, "Code")
                    C1SmartDX.SetData(0, 1, "Description")
                    C1SmartDX.SetData(0, 2, "ICD9ID")
                    C1SmartDX.SetData(0, 3, "nICDRevision")
                    C1SmartDX.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    C1SmartDX.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    C1SmartDX.Cols(2).Width = 0
                    C1SmartDX.Cols(2).Visible = False
                    C1SmartDX.Cols(3).Visible = False
                    C1SmartDX.Cols(3).Width = 0
                    If C1SmartDX.RowSel > 0 Then
                        If (Convert.ToInt16(C1SmartDX.GetData(C1SmartDX.RowSel, 3)) = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()) Then
                            tlsCodingRules.Visible = True
                        Else
                            tlsCodingRules.Visible = False
                        End If
                    End If
                Else
                    C1SmartDX.ExtendLastCol = True
                    C1SmartDX.Rows.Add()
                    C1SmartDX.Rows.Fixed = 1
                    C1SmartDX.Cols.Count = 4
                    C1SmartDX.SetData(0, 0, "Code")
                    C1SmartDX.SetData(0, 1, "Description")
                    C1SmartDX.SetData(0, 2, "ICD9ID")
                    C1SmartDX.SetData(0, 3, "nICDRevision")
                    C1SmartDX.Cols(2).Width = 0
                    C1SmartDX.Cols(3).Width = 0
                    C1SmartDX.Cols(2).Visible = False
                    C1SmartDX.Cols(3).Visible = False
                    C1SmartDX.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    C1SmartDX.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                End If
                For i As Int16 = 0 To 3
                    C1SmartDX.Cols(i).AllowEditing = False
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try



        '' C1SmartDX.Selection.Style.BackColor = Color.Transparent
    End Sub

    Private Sub oMnuRemoveDiagnosis_Click(sender As System.Object, e As System.EventArgs) Handles oMnuRemoveDiagnosis.Click
        If C1SmartDX.RowSel > 0 Then
            C1SmartDX.RemoveItem(C1SmartDX.RowSel)
            bGridFocused = False
        End If
    End Sub

    Private Sub C1SmartDX_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1SmartDX.MouseClick
        If C1SmartDX.RowSel > 0 Then
            If (Convert.ToInt16(C1SmartDX.GetData(C1SmartDX.RowSel, 3)) = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()) Then
                tlsCodingRules.Visible = True
            Else
                tlsCodingRules.Visible = False
            End If
        End If
    End Sub

    Private Sub C1SmartDX_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1SmartDX.MouseDown

        If e.Button = MouseButtons.Right Then
            Dim _nRow As Integer = C1SmartDX.HitTest(e.X, e.Y).Row
            If _nRow > 0 Then
                C1SmartDX.Row = _nRow
            End If
            Dim nRow As Integer
            nRow = C1SmartDX.HitTest(e.X, e.Y).Row
            If nRow > 0 Then
                oMnuRemoveDiagnosis.ForeColor = Color.FromArgb(31, 73, 125)
                oMnuRemoveDiagnosis.Image = imgTreeView.Images(13)
                C1SmartDX.ContextMenuStrip = CmnuDiagnosis

                Me.sSelectedICD10Code = C1SmartDX.GetData(nRow, 0)
                Me.sSelectedICD10Description = C1SmartDX.GetData(nRow, 1)
                Me.sSelectedICDRevision = C1SmartDX.GetData(nRow, 3)
                If (Convert.ToInt16(C1SmartDX.GetData(nRow, 3)) = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()) Then
                    tlsCodingRules.Visible = True
                Else
                    tlsCodingRules.Visible = False
                End If
            Else
                C1SmartDX.ContextMenuStrip = Nothing
            End If
        Else
            Dim _nRow As Integer = C1SmartDX.HitTest(e.X, e.Y).Row
            If _nRow > 0 Then
                If (Convert.ToInt16(C1SmartDX.GetData(_nRow, 3)) = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()) Then
                    tlsCodingRules.Visible = True
                Else
                    tlsCodingRules.Visible = False

                End If
                Me.sSelectedICD10Code = C1SmartDX.GetData(_nRow, 0)
                Me.sSelectedICD10Description = C1SmartDX.GetData(_nRow, 1)
                Me.sSelectedICDRevision = C1SmartDX.GetData(_nRow, 3)

            End If
        End If
        If (isFromexam) Then
            C1SmartDX.ContextMenuStrip = Nothing
        End If
        Me.bGridFocused = True
    End Sub


    Private Sub tlsCodingRules_Click(sender As System.Object, e As System.EventArgs) Handles tlsCodingRules.Click
        Dim sCode As String = Me.sSelectedICD10Code
        Dim sDescription As String = Me.sSelectedICD10Description
        Dim nICDRevision As Int16 = Me.sSelectedICDRevision
        Try
            If bGridFocused Then
                sCode = Me.sSelectedICD10Code
                sDescription = Me.sSelectedICD10Description
            Else
                If Me.wpfICD10UserControl IsNot Nothing AndAlso Me.wpfICD10UserControl.GetSelectedICDCode IsNot Nothing Then
                    sCode = Me.wpfICD10UserControl.GetSelectedICDCode.ICD10Code
                    sDescription = Me.wpfICD10UserControl.GetSelectedICDCode.LongDescription
                    nICDRevision = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode()
                End If
            End If

            If sCode <> String.Empty AndAlso sDescription <> String.Empty AndAlso nICDRevision = gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode() Then
                Dim objCodeRule As New gloUIControlLibrary.WPFForms.frmShowCodingRules(sCode, sDescription, GetConnectionString())

                Dim interopHelper As New System.Windows.Interop.WindowInteropHelper(objCodeRule)
                interopHelper.Owner = Me.Handle

                objCodeRule.LoadNotes()

                If objCodeRule.NoData Then
                    MessageBox.Show("No coding rules available for code " + sCode, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    objCodeRule.ShowDialog()
                    objCodeRule.Close()
                    objCodeRule = Nothing
                End If
                If interopHelper IsNot Nothing Then
                    interopHelper = Nothing
                End If
            Else
                MessageBox.Show("Please select ICD10 code or category to view coding rules.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub C1SmartDX_Leave(sender As System.Object, e As System.EventArgs) Handles C1SmartDX.Leave
        Me.bGridFocused = False
        tlsCodingRules.Visible = True
    End Sub
    'Private Shared Function IsSpecialCharInICDCode(ByVal sICDCode As String) As Boolean
    '    Dim regex As regex = New Regex("[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]")
    '    If regex.IsMatch(sICDCode.Trim()) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    
    'Private Sub txtName_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
    '    'If IsSpecialCharInICDCode(e.KeyChar.ToString()) Then
    '    '    e.Handled = True
    '    'End If

    'End Sub
End Class

